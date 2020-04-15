using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for Cooler.
   /// </summary>
   [Serializable]
   public class Cooler : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ProcessVarDouble pressureDrop;

      #region public properties
      public ProcessVarDouble PressureDrop {
         get { return pressureDrop; }
      }
      #endregion

      //private double hLoss, hInput, cp, t1, t2, wb;  //for temperary use only
      //private double hLoss, hInput, h1, h2, wb;  //for temperary use only
      
      public Cooler(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         pressureDrop = new ProcessVarDouble(StringConstants.PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         heatInput.Name = StringConstants.COOLING_DUTY;
         heatInput.Value = Constants.NO_VALUE;
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         //base.InitializeVarListAndRegisterVars();
         AddVarOnListAndRegisterInSystem(pressureDrop);
         //vars from base class UnitOperation--begin
         AddVarOnListAndRegisterInSystem(heatInput);
         //vars from base class UnitOperation--end
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (!IsStreamValid(ps, streamIndex)) {
            return false;
         }
         bool canAttach = false;
         if (streamIndex == INLET_INDEX && inlet == null) {
            if (outlet != null && outlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (outlet == null && (ps is DryingGasStream || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == OUTLET_INDEX && outlet == null) {
            if (inlet != null && inlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (inlet == null && (ps is DryingGasStream || ps is ProcessStream)) {
               canAttach = true;
            }
         }

         return canAttach;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == pressureDrop) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue < outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the cooler inlet must be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue > inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the cooler outlet cannot be greater than that of the inlet.");
         }

         return retValue;
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         if ((inlet.MassFlowRate.HasValue && inlet.SpecificEnthalpy.HasValue && outlet.SpecificEnthalpy.HasValue && heatLoss.HasValue) || 
             (inlet.MassFlowRate.HasValue && inlet.SpecificEnthalpy.HasValue && heatInput.HasValue && heatLoss.HasValue) || 
             (inlet.MassFlowRate.HasValue && outlet.SpecificEnthalpy.HasValue && heatInput.HasValue && heatLoss.HasValue) ||  
             (inlet.SpecificEnthalpy.HasValue && outlet.SpecificEnthalpy.HasValue && heatInput.HasValue && heatLoss.HasValue)) {
            isReady = true;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         BalancePressure(inlet, outlet, pressureDrop);
         //dry gas flow balance
         if (inlet is DryingGasStream) 
         {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingGasStreamFlow(dsInlet, dsOutlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }
         else if (inlet is ProcessStream) 
         {
            //flow balance
            BalanceProcessStreamFlow(inlet, outlet);
         }

         if (IsSolveReady()) 
         {
            Solve();
         }
         
         PostSolve();
      }

      private void Solve() {
         double wb = inlet.MassFlowRate.Value;
         double hLoss = heatLoss.Value;
         double hInput = heatInput.Value;
         double h1 = inlet.SpecificEnthalpy.Value;
         double h2 = outlet.SpecificEnthalpy.Value;

         if (wb != Constants.NO_VALUE && h1 != Constants.NO_VALUE && h2 != Constants.NO_VALUE && hLoss!= Constants.NO_VALUE) {
            hInput = wb * (h1 - h2) + hLoss;
            Calculate(heatInput, hInput);
            solveState = SolveState.Solved;
         }
         else if (wb != Constants.NO_VALUE && h1 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE) {
            h2 = h1 - (hInput + hLoss)/wb;
            Calculate(outlet.SpecificEnthalpy, h2);
            solveState = SolveState.Solved;
         }
         else if (wb != Constants.NO_VALUE && h2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE) {
            h1 = h2 + (hInput + hLoss)/wb;
            Calculate(inlet.SpecificEnthalpy, h1);
            solveState = SolveState.Solved;
         }
         else if (h1 != Constants.NO_VALUE && h2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE) {
            wb = (hInput + hLoss)/(h1 - h2);
            Calculate(inlet.MassFlowRate, wb);
            Calculate(outlet.MassFlowRate, wb);
            solveState = SolveState.Solved;
         }
      }
      
      protected Cooler(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCooler", typeof(int));
         if (persistedClassVersion == 1) {
            this.pressureDrop = (ProcessVarDouble)RecallStorableObject("PressureDrop", typeof(ProcessVarDouble));
         }
         //RecallInitialization();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionCooler", Cooler.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("PressureDrop", this.pressureDrop, typeof(ProcessVarDouble));
      }
      
      //protected override void RecallInitialization() {
      //   Init();
      //   base.RecallInitialization();
      //}

      //public static void Main() {
   //      ContinuousSolidDryer csd = new ContinuousSolidDryer();
   //      double pressure = csd.DryingGasInlet.PressureValue;
         //WaterProperties ha = new WaterProperties();
         //double tw = HumidGasCalculator.GetWetBulbTemperature(353.98, 0.1, 1.013e5);
         //double ps = WaterProperties.GetSaturationPressure(283.15);
         //Console.WriteLine(tw.ToString(), tw.ToString());
      //}
   }
}

/*private void Solve() {
   double hLoss = heatLoss.Value;
   double hInput = heatInput.Value;
   double cp = inlet.SpecificHeat.Value;
   double t1 = inlet.Temperature.Value;
   double t2 = outlet.Temperature.Value;
         
   if (cp == Constants.NO_VALUE) {
      cp = outlet.SpecificHeat.Value;
   }
         
   //dry gas flow balance
   if (inlet.Type == StreamType.DryingGasStream) {
      DryingGasStream gasInlet = (DryingGasStream) inlet;
      DryingGasStream gasOutlet = (DryingGasStream) outlet;
            
      //balance gas stream flow
      BalanceDryingGasStreamFlow(gasInlet, gasOutlet);
            
      //dry gas flow balance
      //if (gasInlet.MassFlowRateDryBase.Value != Constants.NO_VALUE && !(gasOutlet.MassFlowRateDryBase.IsSpecified && gasOutlet.MassFlowRateDryBase.Value != Constants.NO_VALUE)) {
      //   gasOutlet.CalculateMassFlowRateDryBase(gasInlet.MassFlowRateDryBase.Value);
      //}
      //if (gasOutlet.MassFlowRateDryBase.Value != Constants.NO_VALUE && !(gasInlet.MassFlowRateDryBase.IsSpecified && gasInlet.MassFlowRateDryBase.Value != Constants.NO_VALUE)) {
      //   gasInlet.CalculateMassFlowRateDryBase(gasOutlet.MassFlowRateDryBase.Value);
      //}
            
      //For a drying gas stream going through a heater, absolute humidity does not change from inlet to outlet 
      if (gasInlet.Humidity.Value != Constants.NO_VALUE && !(gasOutlet.Humidity.IsSpecified && gasOutlet.Humidity.Value != Constants.NO_VALUE)) {
         gasOutlet.CalculateHumidity(gasInlet.Humidity.Value);
      }
      if (gasOutlet.Humidity.Value != Constants.NO_VALUE && !(gasInlet.Humidity.IsSpecified && gasInlet.Humidity.Value != Constants.NO_VALUE)) {
         gasInlet.CalculateHumidity(gasOutlet.Humidity.Value);
      }
         
      double wb = gasInlet.MassFlowRateDryBase.Value;
      //case 1
      if (wb != Constants.NO_VALUE && t1 != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hLoss!= Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         hInput = wb * cp * (t2 - t1) + hLoss;
         CalculateHeatInput(hInput);
         solveState = SolveState.Solved;
      }
      else if (wb != Constants.NO_VALUE && t1 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         t2 = (hInput - hLoss)/(wb * cp) + t1;
         gasOutlet.CalculateTemperature(t2);
         solveState = SolveState.Solved;
      }
      else if (wb != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         t1 = t2 - (hInput - hLoss)/(wb * cp);
         gasInlet.CalculateTemperature(t1);
         solveState = SolveState.Solved;
      }
      else if (t1 != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE) {
         wb = (hInput - hLoss)/(cp * (t2 - t1));
         gasInlet.CalculateMassFlowRateDryBase(wb);
         gasOutlet.CalculateMassFlowRateDryBase(wb);
         solveState = SolveState.Solved;
      }
   }
   else if (inlet.Type == StreamType.ProcessStream) {
      //flow balance
      BalanceProcessStreamFlow(inlet, outlet);
         
      double wb = inlet.MassFlowRate.Value;
      //case 1
      if (wb != Constants.NO_VALUE && t1 != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         hInput = wb * cp * (t2 - t1) + hLoss;
         CalculateHeatInput(hInput);
         solveState = SolveState.Solved;
      }
      else if (wb != Constants.NO_VALUE && t1 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         t2 = (hInput - hLoss)/(wb * cp) + t1;
         outlet.CalculateTemperature(t2);
         solveState = SolveState.Solved;
      }
      else if (wb != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         t1 = t2 - (hInput - hLoss)/(wb * cp);
         inlet.CalculateTemperature(t1);
         solveState = SolveState.Solved;
      }
      else if (t1 != Constants.NO_VALUE && t2 != Constants.NO_VALUE && hInput != Constants.NO_VALUE && hLoss != Constants.NO_VALUE && cp != Constants.NO_VALUE) {
         wb = (hInput - hLoss)/(cp * (t2 - t1));
         inlet.CalculateMassFlowRate(wb);
         outlet.CalculateMassFlowRate(wb);
         solveState = SolveState.Solved;
      }
   }
}*/


