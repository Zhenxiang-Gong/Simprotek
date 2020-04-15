using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public abstract class DryingStream : ProcessStreamBase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected ProcessVarDouble massFlowRateDryBase;
      protected ProcessVarDouble moistureContentDryBase;
      protected ProcessVarDouble moistureContentWetBase;
      protected ProcessVarDouble specificHeatDryBase;
      protected ProcessVarDouble specificEnthalpyDryBase;
      //specific heat of absolute dry material
      //protected ProcessVarDouble specificHeatAbsDry;
      
      #region public properties
      public ProcessVarDouble MassFlowRateDryBase {
         get { return massFlowRateDryBase; }
      }

      public ProcessVarDouble MoistureContentDryBase {
         get { return moistureContentDryBase; }
      }

      public ProcessVarDouble MoistureContentWetBase {
         get { return moistureContentWetBase; }
      }

      public ProcessVarDouble SpecificHeatDryBase {
         get { return specificHeatDryBase; }
      }

      public ProcessVarDouble SpecificEnthalpyDryBase {
         get { return specificEnthalpyDryBase; }
      }

      //public ProcessVarDouble SpecificHeatAbsDry {
      //   get { return specificHeatAbsDry; }
      //}
      #endregion 


      //public DryingStream(DryingSystem dryingSystem) : base(dryingSystem) {
      //}
      
      //public DryingStream(string name, UnitOperation upStreamOwner, UnitOperation downStreamOwner, DryingSystem dryingSystem) : base(name, upStreamOwner, downStreamOwner, dryingSystem) {
      protected DryingStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, mComponents, uoSys) {
         massFlowRateDryBase = new ProcessVarDouble(StringConstants.MASS_FLOW_RATE_DRY, PhysicalQuantity.MassFlowRate, VarState.Specified, this);
         moistureContentDryBase = new ProcessVarDouble(StringConstants.MOISTURE_CONTENT_DRY, PhysicalQuantity.MoistureContent, VarState.Specified, this);
         moistureContentWetBase = new ProcessVarDouble(StringConstants.MOISTURE_CONTENT_WET, PhysicalQuantity.MoistureContent, VarState.Specified, this);
         specificHeatDryBase = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_DRY, PhysicalQuantity.SpecificHeat, VarState.AlwaysCalculated, this);
         specificEnthalpyDryBase = new ProcessVarDouble(StringConstants.SPECIFIC_ENTHALPY_DRY, PhysicalQuantity.SpecificHeat, VarState.Specified, this);
         //specificHeatAbsDry = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_ABS_DRY, PhysicalQuantity.SpecificHeat, VarState.Specified, this);
      }
      
      /*protected override void InitializeVarListAndRegisterVars() {
         base.InitializeVarListAndRegisterVars();

         AddVarOnListAndRegisterInSystem(massFlowRateDryBase);
         AddVarOnListAndRegisterInSystem(moistureContentDryBase);
         AddVarOnListAndRegisterInSystem(moistureContentWetBase);
         AddVarOnListAndRegisterInSystem(specificHeatDryBase);
         AddVarOnListAndRegisterInSystem(specificEnthalpyDryBase);
         AddVarOnListAndRegisterInSystem(specificHeatAbsDry);
      }*/
      
      protected override void AdjustVarsStates() {
         //if (massFlowRate.IsSpecifiedAndHasValue) {
         if (massFlowRate.HasValue) {
            if (!massFlowRateDryBase.IsSpecifiedAndHasValue) {
               massFlowRateDryBase.State = VarState.Calculated;
            }
            if (!volumeFlowRate.IsSpecifiedAndHasValue) {
               volumeFlowRate.State = VarState.Calculated;
            }
         }
         //else if (massFlowRateDryBase.IsSpecifiedAndHasValue) {
         else if (massFlowRateDryBase.HasValue) {
            if (!massFlowRate.IsSpecifiedAndHasValue) {
               massFlowRate.State = VarState.Calculated;
            }
            if (!volumeFlowRate.IsSpecifiedAndHasValue) {
               volumeFlowRate.State = VarState.Calculated;
            }
         }
         //else if (volumeFlowRate.IsSpecifiedAndHasValue) {
         else if (volumeFlowRate.HasValue) {
            if (!massFlowRate.IsSpecifiedAndHasValue) {
               massFlowRate.State = VarState.Calculated;
            }
            if (!massFlowRateDryBase.IsSpecifiedAndHasValue) {
               massFlowRateDryBase.State = VarState.Calculated;
            }
         }
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) 
         {
            return retValue;
         }

         if (pv == massFlowRateDryBase)  
         {
            if (aValue != Constants.NO_VALUE && aValue <= 0)  
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == moistureContentDryBase)
         {
            if (aValue != Constants.NO_VALUE && aValue < 0) 
            {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
         }
         else if (pv == moistureContentWetBase)
         {
            if (aValue != Constants.NO_VALUE && (aValue < 0 || aValue > 1.0)) 
            {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
         }
         //else if (pv == specificHeatDryBase || pv == specificHeatAbsDry)
         else if (pv == specificHeatDryBase) {
            if (aValue != Constants.NO_VALUE && aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }

         return retValue ;
      }
      
      protected DryingStream(SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingStream", typeof(int));
         if (persistedClassVersion == 1) {
            this.massFlowRateDryBase = RecallStorableObject("MassFlowRateDryBase", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.moistureContentDryBase = RecallStorableObject("MoistureContentDryBase", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.moistureContentWetBase = RecallStorableObject("MoistureContentWetBase", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.specificHeatDryBase = RecallStorableObject("SpecificHeatDryBase", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.specificEnthalpyDryBase = RecallStorableObject("SpecificEnthalpyDryBase", typeof(ProcessVarDouble)) as ProcessVarDouble;
            //this.specificHeatAbsDry = RecallStorableObject("SpecificHeatAbsDry", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionDryingStream", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("MassFlowRateDryBase", this.massFlowRateDryBase, typeof(ProcessVarDouble));
         info.AddValue("MoistureContentDryBase", this.moistureContentDryBase, typeof(ProcessVarDouble));
         info.AddValue("MoistureContentWetBase", this.moistureContentWetBase, typeof(ProcessVarDouble));
         info.AddValue("SpecificHeatDryBase", this.specificHeatDryBase, typeof(ProcessVarDouble));
         info.AddValue("SpecificEnthalpyDryBase", this.specificEnthalpyDryBase, typeof(ProcessVarDouble));
         //info.AddValue("SpecificHeatAbsDry", this.specificHeatAbsDry, typeof(ProcessVarDouble));
      }
   }
}
