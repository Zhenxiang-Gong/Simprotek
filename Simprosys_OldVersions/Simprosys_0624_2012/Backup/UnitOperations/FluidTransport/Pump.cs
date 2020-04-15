using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.FluidTransport {
   
   [Serializable] 
   public class Pump : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private ProcessVarDouble capacity;
      private ProcessVarDouble staticSuctionHead;
      private ProcessVarDouble suctionFrictionHead;
      private ProcessVarDouble staticDischargeHead;
      private ProcessVarDouble dischargeFrictionHead;
      private ProcessVarDouble totalDynamicHead;
      private ProcessVarDouble efficiency;
      private ProcessVarDouble powerInput;
      
      private bool includeOutletVelocityEffect;
      private ProcessVarDouble outletDiameter;
      private ProcessVarDouble outletVelocity;

      #region public properties

      public ProcessVarDouble Capacity {
         get { return capacity; }
      }
      
      public ProcessVarDouble StaticSuctionHead {
         get { return staticSuctionHead; }
      }

      public ProcessVarDouble SuctionFrictionHead {
         get { return suctionFrictionHead; }
      }

      public ProcessVarDouble StaticDischargeHead {
         get { return staticDischargeHead; }
      }

      public ProcessVarDouble DischargeFrictionHead {
         get { return dischargeFrictionHead; }
      }

      public ProcessVarDouble TotalDynamicHead {
         get { return totalDynamicHead; }
      }
      
      public ProcessVarDouble PowerInput {
         get { return powerInput; }
      }

      public ProcessVarDouble Efficiency {
         get { return efficiency; }
      }
      
      public bool IncludeOutletVelocityEffect 
      {
         get {return includeOutletVelocityEffect;}
      }

      public ProcessVarDouble OutletDiameter 
      {
         get { return outletDiameter; }
      }

      public ProcessVarDouble OutletVelocity {
         get { return outletVelocity; }
      }
      #endregion

      public Pump(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         staticSuctionHead = new ProcessVarDouble(StringConstants.STATIC_SUCTION_HEAD, PhysicalQuantity.LiquidHead, VarState.Specified, this);
         suctionFrictionHead = new ProcessVarDouble(StringConstants.SUCTION_FRICTION_HEAD, PhysicalQuantity.LiquidHead, 0.0, VarState.Specified, this);
         staticDischargeHead = new ProcessVarDouble(StringConstants.STATIC_DICHARGE_HEAD, PhysicalQuantity.LiquidHead, VarState.Specified, this);
         dischargeFrictionHead = new ProcessVarDouble(StringConstants.DISCHARGE_FRICTION_HEAD, PhysicalQuantity.LiquidHead, 0.0, VarState.Specified, this);
         totalDynamicHead = new ProcessVarDouble(StringConstants.TOTAL_DYNAMIC_HEAD, PhysicalQuantity.LiquidHead, VarState.Specified, this);
         capacity = new ProcessVarDouble(StringConstants.CAPACITY, PhysicalQuantity.VolumeRateFlowLiquids, VarState.AlwaysCalculated, this);
         efficiency = new ProcessVarDouble(StringConstants.EFFICIENCY, PhysicalQuantity.Fraction, VarState.Specified, this);
         powerInput = new ProcessVarDouble(StringConstants.POWER_INPUT, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);
         //includeOutletVelocityEffect = true;
         outletDiameter = new ProcessVarDouble(StringConstants.OUTLET_DIAMETER, PhysicalQuantity.SmallLength, VarState.Specified, this);
         outletVelocity = new ProcessVarDouble(StringConstants.OUTLET_VELOCITY, PhysicalQuantity.Velocity, VarState.AlwaysCalculated, this);
         
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(staticSuctionHead);
         AddVarOnListAndRegisterInSystem(suctionFrictionHead);
         AddVarOnListAndRegisterInSystem(staticDischargeHead);
         AddVarOnListAndRegisterInSystem(dischargeFrictionHead);
         AddVarOnListAndRegisterInSystem(totalDynamicHead);
         AddVarOnListAndRegisterInSystem(capacity);
         AddVarOnListAndRegisterInSystem(efficiency);
         AddVarOnListAndRegisterInSystem(powerInput);
         AddVarOnListAndRegisterInSystem(outletDiameter);
         AddVarOnListAndRegisterInSystem(outletVelocity);
      }
      
      public ErrorMessage SpecifyIncludeOutletVelocityEffect(bool aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != includeOutletVelocityEffect) 
         {
            bool oldValue = includeOutletVelocityEffect;
            includeOutletVelocityEffect = aValue;
            
            try 
            {
               HasBeenModified(true);
            }
            catch (Exception e) 
            {
               includeOutletVelocityEffect = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) 
      {
         if (!IsStreamValid(ps, streamIndex)) {
            return false;
         }
         bool canAttach = false;
         bool isLiquidMaterial = false;
         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }
         
         if (isLiquidMaterial || ps is WaterStream) {
            if (streamIndex == INLET_INDEX && inlet == null) {
               if (outlet == null || (outlet != null && outlet.GetType() == ps.GetType())) {
                  canAttach = true;
               }
            }
            else if (streamIndex == OUTLET_INDEX && outlet == null) {
               if (inlet == null || (inlet != null && inlet.GetType() == ps.GetType())) {
                  canAttach = true;
               }
            }
         }

         return canAttach;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == outletDiameter) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == efficiency) {
            if (aValue < 0.2 || aValue > 1.0) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " cannot be out of the range of 0.2 to 1.");
            }
         }
         else if (pv == staticSuctionHead || pv == suctionFrictionHead || pv == staticDischargeHead || pv == dischargeFrictionHead || pv == totalDynamicHead) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue > outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the fan inlet cannot be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue < inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the fan outlet cannot be smaller than that of the inlet.");
         }

         return retValue;
      }
      
      public override void Execute(bool propagate) {
         PreSolve();
         BalanceSpecificEnthalpy(inlet, outlet);

         if (inlet.Density.HasValue && !outlet.Density.HasValue) {
            Calculate(outlet.Density, inlet.Density.Value);
         }
         else if (outlet.Density.HasValue && !inlet.Density.HasValue) {
            Calculate(inlet.Density, outlet.Density.Value);
         }

         //dry gas flow balance
         if (inlet is DryingMaterialStream) {
            DryingMaterialStream dmsInlet = inlet as DryingMaterialStream;
            DryingMaterialStream dmsOutlet = outlet as DryingMaterialStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dmsInlet, dmsOutlet);
            BalanceDryingMaterialStreamFlow(dmsInlet, dmsOutlet);
            //BalanceDryingMaterialStreamSpecificHeat(dmsInlet, dmsOutlet);
            //have to recalculate the streams so that the following balance calcualtion
            //can have all the latest balance calculated values taken into account
            UpdateStreamsIfNecessary();

            AdjustVarsStates(dmsInlet, dmsOutlet);
         }
         else if (inlet is WaterStream) {
            BalanceProcessStreamFlow(inlet, outlet);
         }

         Solve();
         PostSolve();
      }

      private void Solve() {
         double ssh = staticSuctionHead.Value;
         double sfh = suctionFrictionHead.Value;
         double sdh = staticDischargeHead.Value;
         double dfh = dischargeFrictionHead.Value;
         double eff = efficiency.Value;
         double tdh = totalDynamicHead.Value;
         double inletP = inlet.Pressure.Value;
         double outletP = outlet.Pressure.Value;

         if (sfh == Constants.NO_VALUE) {
            sfh = 0.0;
         }
         if (dfh == Constants.NO_VALUE) {
            dfh = 0.0;
         }

         double density = inlet.Density.Value;
         if (density == Constants.NO_VALUE) {
            density = outlet.Density.Value;
         }
         
         double capacityValue = inlet.VolumeFlowRate.Value;
         if (capacityValue == Constants.NO_VALUE) {
            capacityValue = outlet.VolumeFlowRate.Value;
         }

         if (capacityValue != Constants.NO_VALUE) {
            Calculate(capacity, capacityValue);
         }

         double deltaP = Constants.NO_VALUE;
         if (inletP != Constants.NO_VALUE && outletP != Constants.NO_VALUE) {
            deltaP = outletP - inletP;
         }

         if (ssh != Constants.NO_VALUE && sfh != Constants.NO_VALUE && sdh != Constants.NO_VALUE && dfh != Constants.NO_VALUE) {
            tdh = sdh + dfh - (ssh - sfh);
            Calculate(totalDynamicHead, tdh);
            //solveState = SolveState.PartiallySolved;
         }

         else if (tdh != Constants.NO_VALUE && sfh != Constants.NO_VALUE && sdh != Constants.NO_VALUE && dfh != Constants.NO_VALUE) {
            ssh = tdh - sfh - sdh - dfh;
            Calculate(staticSuctionHead, ssh);
            //solveState = SolveState.PartiallySolved;
         }
         else if (tdh != Constants.NO_VALUE && ssh != Constants.NO_VALUE && sfh != Constants.NO_VALUE && dfh != Constants.NO_VALUE) {
            sdh = tdh - dfh + sdh - sfh ;
            Calculate(staticDischargeHead, sdh);
            //solveState = SolveState.PartiallySolved;
         }

         double outletVelocityHead = Constants.NO_VALUE;
         if (includeOutletVelocityEffect) 
         {
            double volumeFlowRate = Constants.NO_VALUE;
            if (inlet.VolumeFlowRate.HasValue) 
            {
               volumeFlowRate = inlet.VolumeFlowRate.Value;
            }
            else 
            {
               volumeFlowRate = outlet.VolumeFlowRate.Value;
            }
         
            if (volumeFlowRate != Constants.NO_VALUE && density != Constants.NO_VALUE) 
            {
               if (outletDiameter.HasValue) 
               {
                  double d = outletDiameter.Value;
                  double outletArea = 0.25 * Math.PI * d * d;
                  double outletVelocityValue = volumeFlowRate/outletArea;
                  Calculate(outletVelocity, outletVelocityValue);
                  outletVelocityHead = outletVelocityValue * outletVelocityValue/(2.0 * Constants.G);
               }
            }
         }
         else 
         {
            outletVelocityHead = 0.0;
         }

         if (tdh != Constants.NO_VALUE && capacityValue != Constants.NO_VALUE && density != Constants.NO_VALUE && eff != Constants.NO_VALUE &&
            deltaP != Constants.NO_VALUE && outletVelocityHead != Constants.NO_VALUE) {
            double totalHead = tdh + outletVelocityHead;
            double pi = capacityValue * (totalHead * density * Constants.G + deltaP) / eff;
            Calculate(powerInput, pi);
            solveState = SolveState.Solved;
         }
      }
   
      protected Pump(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPump", typeof(int));
         if (persistedClassVersion == 1) {
            this.capacity = RecallStorableObject("Capacity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.staticSuctionHead = RecallStorableObject("StaticSuctionHead", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.suctionFrictionHead = RecallStorableObject("SuctionFrictionHead", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.staticDischargeHead = RecallStorableObject("StaticDischargeHead", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.dischargeFrictionHead = RecallStorableObject("DischargeFrictionHead", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.totalDynamicHead = RecallStorableObject("TotalDynamicHead", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.efficiency = RecallStorableObject("Efficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.powerInput = RecallStorableObject("PowerInput", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.includeOutletVelocityEffect = (bool) info.GetValue("IncludeOutletVelocityEffect", typeof(bool));
            this.outletDiameter = RecallStorableObject("OutletDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletVelocity = RecallStorableObject("OutletVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionPump", Pump.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Capacity", this.capacity, typeof(ProcessVarDouble));
         info.AddValue("StaticSuctionHead", this.staticSuctionHead, typeof(ProcessVarDouble));
         info.AddValue("SuctionFrictionHead", this.suctionFrictionHead, typeof(ProcessVarDouble));
         info.AddValue("StaticDischargeHead", this.staticDischargeHead, typeof(ProcessVarDouble));
         info.AddValue("DischargeFrictionHead", this.dischargeFrictionHead, typeof(ProcessVarDouble));
         info.AddValue("TotalDynamicHead", this.totalDynamicHead, typeof(ProcessVarDouble));
         info.AddValue("Efficiency", this.efficiency, typeof(ProcessVarDouble));
         info.AddValue("PowerInput", this.powerInput, typeof(ProcessVarDouble));
         info.AddValue("IncludeOutletVelocityEffect", this.includeOutletVelocityEffect, typeof(bool));
         info.AddValue("OutletDiameter", this.outletDiameter, typeof(ProcessVarDouble));
         info.AddValue("OutletVelocity", this.outletVelocity, typeof(ProcessVarDouble));
      }
   }
}

