using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations {
   public enum UnitOpCalculationType { Balance = 0, Rating };
   public enum FlowDirectionType { Counter = 0, Parallel, Cross };
   public enum Orientation { Horizontal = 0, Vertical };
   public enum CrossSectionType { Circle = 0, Rectangle };

   public delegate void StreamAttachedEventHandler(UnitOperation uo, ProcessStreamBase ps, int streamIndex);
   public delegate void StreamDetachedEventHandler(UnitOperation uo, ProcessStreamBase ps);

   /// <summary>
   /// Summary description for UnitOperation.
   /// </summary>
   [Serializable]
   public abstract class UnitOperation : Solvable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      protected UnitOpCalculationType calculationType;

      protected int solvingPriority = 500;
      protected ProcessVarDouble heatLoss;
      protected ProcessVarDouble heatInput;
      protected ProcessVarDouble workInput;

      protected ArrayList inletStreams = new ArrayList();
      protected ArrayList outletStreams = new ArrayList();

      #region public properties

      public ProcessVarDouble HeatLoss {
         get { return heatLoss; }
         //set { heatLoss = value;}
      }

      public ProcessVarDouble HeatInput {
         get { return heatInput; }
         //set { heatInput = value;}
      }

      public ProcessVarDouble WorkInput {
         get { return workInput; }
         //set { workInput = value;}
      }

      public UnitOpCalculationType CalculationType {
         get { return calculationType; }
         //         set {
         //            if (value != calculationType) {
         //               calculationType = value;
         //               if (calculationType == UnitOpCalculationType.Rating) {
         //                  EnableRatingModel();
         //               }
         //               else if (calculationType == UnitOpCalculationType.Balance) {
         //                  EnableBalanceModel();
         //               }
         //
         //               HasBeenModified(true);
         //            }
         //         }
      }

      public int SolvingPriority {
         get { return solvingPriority; }
      }

      public ArrayList InletStreams {
         get { return inletStreams; }
      }

      public ArrayList OutletStreams {
         get { return outletStreams; }
      }

      public ArrayList InOutletStreams {
         get {
            ArrayList list = new ArrayList();
            list.AddRange(inletStreams);
            list.AddRange(outletStreams);
            return list;
         }
         //set { outletStreams = value; }
      }
      #endregion

      public event StreamAttachedEventHandler StreamAttached;
      public event StreamDetachedEventHandler StreamDetached;

      protected void OnStreamAttached(UnitOperation uo, ProcessStreamBase ps, int streamIndex) {
         if (StreamAttached != null) {
            StreamAttached(uo, ps, streamIndex);
         }
      }

      protected void OnStreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         if (StreamDetached != null) {
            StreamDetached(uo, ps);
         }
      }

      protected UnitOperation(string name, UnitOperationSystem uoSys)
         : base(name, uoSys) {
         heatLoss = new ProcessVarDouble(StringConstants.HEAT_LOSS, PhysicalQuantity.Power, 0.0, VarState.Specified, this);
         heatInput = new ProcessVarDouble(StringConstants.HEAT_INPUT, PhysicalQuantity.Power, 0.0, VarState.Specified, this);
         workInput = new ProcessVarDouble(StringConstants.WORK_INPUT, PhysicalQuantity.Power, 0.0, VarState.Specified, this);
         calculationType = UnitOpCalculationType.Balance;
      }

      public virtual bool CanConnect(int streamIndex) {
         return false;
      }

      public virtual bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         return true;
      }

      public virtual ErrorMessage AttachStream(ProcessStreamBase ps, int streamIndex) {
         ErrorMessage retMsg = null;
         try {
            if (DoAttach(ps, streamIndex)) {
               HasBeenModified(true);
               OnStreamAttached(this, ps, streamIndex);
            }
         }
         catch (Exception e) {
            DetachStream(ps);
            string msg = "Stream " + ps.Name + " cannot be attached to " + this.name + "\n" + e.Message;
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Attach Failed", msg);
         }
         return retMsg;
      }

      public virtual ErrorMessage DetachStream(ProcessStreamBase ps) {
         ErrorMessage retMsg = null;
         try {
            DoDetach(ps);
         }
         catch (Exception) {
            OnStreamDetached(this, ps);
            //string msg = "Stream " + ps.Name + " cannot be detached." + "\n" + e.Message; 
            //retMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Attach Failed", msg);
         }

         return retMsg;
      }

      internal virtual bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         return true;
      }

      internal virtual bool DoDetach(ProcessStreamBase ps) {
         return true;
      }

      public virtual ErrorMessage SpecifyCalculationType(UnitOpCalculationType aValue) {
         ErrorMessage retMsg = null;
         if (aValue != calculationType) {
            UnitOpCalculationType oldValue = calculationType;
            calculationType = aValue;
            EnableBalanceOrRatingModel(calculationType);

            try {
               HasBeenModified(true);
            }
            catch (Exception e) {
               calculationType = oldValue;
               EnableBalanceOrRatingModel(calculationType);
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }


      protected void EnableBalanceOrRatingModel(UnitOpCalculationType calcType) {
         if (calcType == UnitOpCalculationType.Rating) {
            EnableRatingModel();
         }
         else if (calcType == UnitOpCalculationType.Balance) {
            EnableBalanceModel();
         }
      }

      protected virtual void EnableRatingModel() {
      }

      protected virtual void EnableBalanceModel() {
      }

      public ArrayList GetAllVariables() {
         ArrayList list = new ArrayList();
         list.AddRange(varList);
         foreach (ProcessStreamBase psb in InOutletStreams) {
            list.AddRange(psb.VarList);
         }
         return list;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == heatLoss) {
            if (aValue < 0.0) {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
         }
         else if (pv == heatInput) {
            if (aValue < 0.0) {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
         }
         else if (pv == workInput) {
            if (aValue < 0.0) {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
         }

         return retValue;
      }

      internal virtual ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         return null;
      }

      internal virtual bool IsBalanceCalcReady() {
         return true;
      }

      protected virtual void BalancePressure(ProcessStreamBase inletStream, ProcessStreamBase outletStream, ProcessVarDouble pressureDrop) {
         //pressure balance
         if (inletStream.Pressure.HasValue && outletStream.Pressure.HasValue && !pressureDrop.HasValue) {
            double pressDiff = inletStream.Pressure.Value - outletStream.Pressure.Value;
            Calculate(pressureDrop, pressDiff);
         }
         else if (inletStream.Pressure.HasValue && pressureDrop.HasValue && !outletStream.Pressure.HasValue) {
            double pressOutlet = inletStream.Pressure.Value - pressureDrop.Value;
            Calculate(outletStream.Pressure, pressOutlet);
         }
         else if (outletStream.Pressure.HasValue && pressureDrop.HasValue && !inletStream.Pressure.HasValue) {
            double pressInlet = outletStream.Pressure.Value + pressureDrop.Value;
            Calculate(inletStream.Pressure, pressInlet);
         }
         else if (inletStream.Pressure.HasValue && outletStream.Pressure.HasValue && pressureDrop.HasValue) {
            //over specification!!!!
         }
      }

      protected void BalancePressure(ProcessStreamBase inletStream, ProcessStreamBase outletStream, double pressureDrop) {
         //pressure balance
         if (inletStream.Pressure.HasValue && !outletStream.Pressure.HasValue) {
            double pressOutlet = inletStream.Pressure.Value - pressureDrop;
            Calculate(outletStream.Pressure, pressOutlet);
         }
         else if (outletStream.Pressure.HasValue && !inletStream.Pressure.HasValue) {
            double pressInlet = outletStream.Pressure.Value + pressureDrop;
            Calculate(inletStream.Pressure, pressInlet);
         }
      }

      protected void BalanceSpecificEnthalpy(ProcessStreamBase inletStream, ProcessStreamBase outletStream) {
         if (inletStream is DryingGasStream) {
            DryingStream inlet = inletStream as DryingStream;
            DryingStream outlet = outletStream as DryingStream;
            if (inlet.SpecificEnthalpyDryBase.HasValue && !outlet.SpecificEnthalpyDryBase.HasValue) {
               Calculate(outlet.SpecificEnthalpyDryBase, inlet.SpecificEnthalpyDryBase.Value);
            }
            else if (outlet.SpecificEnthalpyDryBase.HasValue && !inlet.SpecificEnthalpyDryBase.HasValue) {
               Calculate(inlet.SpecificEnthalpyDryBase, outlet.SpecificEnthalpyDryBase.Value);
            }
         }
         else {
            if (inletStream.SpecificEnthalpy.HasValue && !outletStream.SpecificEnthalpy.HasValue) {
               Calculate(outletStream.SpecificEnthalpy, inletStream.SpecificEnthalpy.Value);
            }
            else if (outletStream.SpecificEnthalpy.HasValue && !inletStream.SpecificEnthalpy.HasValue) {
               Calculate(inletStream.SpecificEnthalpy, outletStream.SpecificEnthalpy.Value);
            }
         }
      }

      protected void BalanceProcessStreamFlow(ProcessStreamBase inlet, ProcessStreamBase outlet) {
         //flow balance
         ///if (inlet.MassFlowRate.Value != Constants.NO_VALUE && !(outlet.MassFlowRate.IsSpecified && outlet.MassFlowRate.Value != Constants.NO_VALUE)) {
         if (inlet.MassFlowRate.HasValue && !outlet.MassFlowRate.HasValue) {
            Calculate(outlet.MassFlowRate, inlet.MassFlowRate.Value);
         }
         //else if (outlet.MassFlowRate.Value != Constants.NO_VALUE && !(inlet.MassFlowRate.IsSpecified && inlet.MassFlowRate.Value != Constants.NO_VALUE)) {
         else if (outlet.MassFlowRate.HasValue && !inlet.MassFlowRate.HasValue) {
            Calculate(inlet.MassFlowRate, outlet.MassFlowRate.Value);
         }
      }

      protected void BalanceDryingGasStreamFlow(DryingGasStream inlet, DryingGasStream outlet) {
         //dry material and dry gas flow balances
         //dry gas flow balance
         //if (inlet.MassFlowRateDryBase.Value != Constants.NO_VALUE && !(outlet.MassFlowRateDryBase.IsSpecified && outlet.MassFlowRateDryBase.Value != Constants.NO_VALUE)) {
         if (inlet.MassFlowRateDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
            Calculate(outlet.MassFlowRateDryBase, inlet.MassFlowRateDryBase.Value);
            //if (inlet.Humidity.Value != Constants.NO_VALUE && outlet.MassFlowRate.Value == Constants.NO_VALUE) {
            //   outlet.CalculateMassFlowRate(inlet.MassFlowRateDryBase.Value * (1.0 + inlet.Humidity.Value));
            //}
         }

         //else if (outlet.MassFlowRateDryBase.Value != Constants.NO_VALUE && !(inlet.MassFlowRateDryBase.IsSpecified && inlet.MassFlowRateDryBase.Value != Constants.NO_VALUE)) {
         else if (outlet.MassFlowRateDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
            Calculate(inlet.MassFlowRateDryBase, outlet.MassFlowRateDryBase.Value);
            //if (outlet.Humidity.Value != Constants.NO_VALUE && inlet.MassFlowRate.Value == Constants.NO_VALUE) {
            //   inlet.CalculateMassFlowRate(outlet.MassFlowRateDryBase.Value * (1.0 + outlet.Humidity.Value));
            //}
         }
         else if (inlet.MassFlowRate.HasValue && inlet.MoistureContentDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
            Calculate(outlet.MassFlowRateDryBase, inlet.MassFlowRate.Value / (1.0 + inlet.MoistureContentDryBase.Value));
         }
         else if (outlet.MassFlowRate.HasValue && outlet.MoistureContentDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
            Calculate(inlet.MassFlowRateDryBase, outlet.MassFlowRate.Value / (1.0 + outlet.MoistureContentDryBase.Value));
         }
      }

      protected void BalanceStreamComponents(ProcessStreamBase inlet, ProcessStreamBase outlet) {
         if (inlet is DryingGasStream) {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;
            DryingGasComponents inletDgc = dsInlet.GasComponents;
            DryingGasComponents outletDgc = dsOutlet.GasComponents;
            SolidPhase inletSolidPhase = inletDgc.SolidPhase;
            SolidPhase outletSolidPhase = outletDgc.SolidPhase;
            if (inletSolidPhase != null) {
               if (outletSolidPhase != null) {
                  outletSolidPhase.MassFraction = inletSolidPhase.MassFraction;
               }
               else {
                  outletDgc.AddPhase(inletSolidPhase);
               }
            }
         }
      }

      protected virtual void BalanceDryingMaterialStreamFlow(DryingMaterialStream inlet, DryingMaterialStream outlet) {
         //dry materail flow balance
         if (inlet.MaterialStateType == MaterialStateType.Liquid && outlet.MaterialStateType == MaterialStateType.Liquid) {
            if (inlet.MassFlowRate.HasValue && !outlet.MassFlowRate.HasValue) {
               Calculate(outlet.MassFlowRate, inlet.MassFlowRate.Value);
            }
            else if (outlet.MassFlowRate.HasValue && !inlet.MassFlowRate.HasValue) {
               Calculate(inlet.MassFlowRate, outlet.MassFlowRate.Value);
            }

            if (inlet.MassFlowRateDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
               Calculate(outlet.MassFlowRateDryBase, inlet.MassFlowRateDryBase.Value);
            }
            else if (outlet.MassFlowRateDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
               Calculate(inlet.MassFlowRateDryBase, outlet.MassFlowRateDryBase.Value);
            }
         }
         else {
            if (inlet.MassFlowRateDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
               Calculate(outlet.MassFlowRateDryBase, inlet.MassFlowRateDryBase.Value);
            }
            else if (outlet.MassFlowRateDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
               Calculate(inlet.MassFlowRateDryBase, outlet.MassFlowRateDryBase.Value);
            }
            else if (inlet.MassFlowRate.HasValue && inlet.MoistureContentDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
               Calculate(outlet.MassFlowRateDryBase, inlet.MassFlowRate.Value / (1.0 + inlet.MoistureContentDryBase.Value));
            }
            else if (outlet.MassFlowRate.HasValue && outlet.MoistureContentDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
               Calculate(inlet.MassFlowRateDryBase, outlet.MassFlowRate.Value / (1.0 + outlet.MoistureContentDryBase.Value));
            }
         }
      }

      /*protected void BalanceDryingGasStreamHumidity(DryingGasStream inlet, DryingGasStream outlet) {
         //For a drying gas stream going through a heater, absolute humidity does not change from inlet to outlet 
         //if (inlet.Humidity.Value != Constants.NO_VALUE && !(outlet.Humidity.IsSpecified && outlet.Humidity.Value != Constants.NO_VALUE)) {
         if (inlet.Humidity.Value != Constants.NO_VALUE && outlet.Humidity.Value == Constants.NO_VALUE) {
            Calculate(outlet.MoistureContentDryBase, inlet.Humidity.Value);
         }
         //else if (outlet.Humidity.Value != Constants.NO_VALUE && !(inlet.Humidity.IsSpecified && inlet.Humidity.Value != Constants.NO_VALUE)) {
         else if (outlet.Humidity.Value != Constants.NO_VALUE && inlet.Humidity.Value == Constants.NO_VALUE) {
            Calculate(inlet.MoistureContentDryBase, outlet.Humidity.Value);
         }
      }*/

      protected void BalanceDryingStreamMoistureContent(DryingStream inlet, DryingStream outlet) {
         //For a drying gas stream going through a heater, absolute humidity does not change from inlet to outlet 
         //if (inlet.Humidity.Value != Constants.NO_VALUE && !(outlet.Humidity.IsSpecified && outlet.Humidity.Value != Constants.NO_VALUE)) {
         if (inlet.MoistureContentDryBase.HasValue && !outlet.MoistureContentDryBase.HasValue) {
            Calculate(outlet.MoistureContentDryBase, inlet.MoistureContentDryBase.Value);
         }
         //else if (outlet.Humidity.Value != Constants.NO_VALUE && !(inlet.Humidity.IsSpecified && inlet.Humidity.Value != Constants.NO_VALUE)) {
         else if (outlet.MoistureContentDryBase.HasValue && !inlet.MoistureContentDryBase.HasValue) {
            Calculate(inlet.MoistureContentDryBase, outlet.MoistureContentDryBase.Value);
         }
         else if (inlet.MoistureContentWetBase.HasValue && !outlet.MoistureContentWetBase.HasValue) {
            Calculate(outlet.MoistureContentWetBase, inlet.MoistureContentWetBase.Value);
         }
         else if (outlet.MoistureContentWetBase.HasValue && !inlet.MoistureContentWetBase.HasValue) {
            Calculate(inlet.MoistureContentWetBase, outlet.MoistureContentWetBase.Value);
         }
         /*if (inlet.Concentration.Value != Constants.NO_VALUE && outlet.Concentration.Value == Constants.NO_VALUE) {
            Calculate(outlet.Concentration, inlet.Concentration.Value);
         }
         else if (outlet.MoistureContentWetBase.Value != Constants.NO_VALUE && inlet.Concentration.Value == Constants.NO_VALUE) {
            Calculate(inlet.Concentration, outlet.Concentration.Value);
         }*/

      }

      //protected void BalanceDryingMaterialStreamSpecificHeat(DryingMaterialStream inlet, DryingMaterialStream outlet) {
      //   if (inlet.SpecificHeatAbsDry.HasValue && !outlet.SpecificHeatAbsDry.HasValue) {
      //      Calculate(outlet.SpecificHeatAbsDry, inlet.SpecificHeatAbsDry.Value);
      //   }
      //   else if (outlet.SpecificHeatAbsDry.HasValue && !inlet.SpecificHeatAbsDry.HasValue) {
      //      Calculate(inlet.SpecificHeatAbsDry, outlet.SpecificHeatAbsDry.Value);
      //   }
      //}

      protected void AdjustVarsStates(DryingStream dsInlet, DryingStream dsOutlet) {
         if (dsInlet.MassFlowRate.IsSpecifiedAndHasValue)
         //if (dsInlet.MassFlowRate.HasValue) 
         {
            dsOutlet.MassFlowRate.State = VarState.Calculated;
            dsOutlet.MassFlowRateDryBase.State = VarState.Calculated;
            dsOutlet.VolumeFlowRate.State = VarState.Calculated;
            dsOutlet.HasVarStateChanged = true;
         }
         else if (dsInlet.MassFlowRateDryBase.IsSpecifiedAndHasValue)
         //else if (dsInlet.MassFlowRateDryBase.HasValue) 
         {
            dsOutlet.MassFlowRate.State = VarState.Calculated;
            dsOutlet.VolumeFlowRate.State = VarState.Calculated;
            dsOutlet.HasVarStateChanged = true;
         }
         else if (dsInlet.VolumeFlowRate.IsSpecifiedAndHasValue)
         //else if (dsInlet.VolumeFlowRate.HasValue)
         {
            dsOutlet.MassFlowRate.State = VarState.Calculated;
            dsOutlet.MassFlowRateDryBase.State = VarState.Calculated;
            dsOutlet.VolumeFlowRate.State = VarState.Calculated;
            dsOutlet.HasVarStateChanged = true;
         }
         else if (dsOutlet.MassFlowRate.IsSpecifiedAndHasValue)
         //else if (dsOutlet.MassFlowRate.HasValue)
         {
            dsInlet.MassFlowRate.State = VarState.Calculated;
            dsInlet.MassFlowRateDryBase.State = VarState.Calculated;
            dsInlet.VolumeFlowRate.State = VarState.Calculated;
            dsInlet.HasVarStateChanged = true;
         }
         else if (dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasValue)
         //else if (dsOutlet.MassFlowRateDryBase.HasValue)
         {
            dsInlet.MassFlowRate.State = VarState.Calculated;
            dsInlet.VolumeFlowRate.State = VarState.Calculated;
            dsInlet.HasVarStateChanged = true;
         }
         else if (dsOutlet.VolumeFlowRate.IsSpecifiedAndHasValue)
         //else if (dsOutlet.VolumeFlowRate.HasValue)
         {
            dsInlet.MassFlowRate.State = VarState.Calculated;
            dsInlet.MassFlowRateDryBase.State = VarState.Calculated;
            dsInlet.VolumeFlowRate.State = VarState.Calculated;
            dsInlet.HasVarStateChanged = true;
         }
      }

      protected void PreSolve() {
         //initialize the varChanged flag
         isBeingExecuted = true;
         ArrayList streamList = this.InOutletStreams;
         foreach (ProcessStreamBase ps in streamList) {
            ps.HasVarCalculated = false;
            ps.HasVarStateChanged = false;
         }
      }

      protected virtual void PostSolve() {
         UpdateStreams();
         isBeingExecuted = false;
         OnSolveComplete();
      }

      protected void UpdateStreams() {
         ArrayList streamList = this.InOutletStreams;
         foreach (ProcessStreamBase ps in streamList) {
            if (ps.HasVarCalculated) {
               //ps.Update(this);
               ps.HasBeenModified(false);
            }
            else if (ps.HasVarStateChanged) {
               ps.OnSolveComplete();
            }
         }
      }

      protected void UpdateStreamsIfNecessary() {
         ArrayList streamList = this.InOutletStreams;
         foreach (ProcessStreamBase ps in streamList) {
            if (ps.HasVarCalculated) {
               ps.Execute(false);
            }
         }
      }

      public override void Execute(bool propagate) {
         base.Execute(propagate);
      }

      protected UnitOperation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionUnitOperation", typeof(int));
         if (persistedClassVersion == 1) {
            this.calculationType = (UnitOpCalculationType)info.GetValue("CalculationType", typeof(UnitOpCalculationType));
            //this.calculationLevel = (int) info.GetValue("CalculationLevel", typeof(int));
            this.heatLoss = RecallStorableObject("HeatLoss", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.heatInput = RecallStorableObject("HeatInput", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.workInput = RecallStorableObject("WorkInput", typeof(ProcessVarDouble)) as ProcessVarDouble;

            this.inletStreams = info.GetValue("InletStreams", typeof(ArrayList)) as ArrayList;
            this.outletStreams = info.GetValue("OutletStreams", typeof(ArrayList)) as ArrayList;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionUnitOperation", UnitOperation.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CalculationType", calculationType, typeof(UnitOpCalculationType));
         //info.AddValue("CalculationLevel", calculationLevel, typeof(int));
         info.AddValue("HeatLoss", this.heatLoss, typeof(ProcessVarDouble));
         info.AddValue("HeatInput", this.heatInput, typeof(ProcessVarDouble));
         info.AddValue("WorkInput", this.workInput, typeof(ProcessVarDouble));

         info.AddValue("InletStreams", this.inletStreams, typeof(ArrayList));
         info.AddValue("OutletStreams", this.outletStreams, typeof(ArrayList));
      }
   }
}
