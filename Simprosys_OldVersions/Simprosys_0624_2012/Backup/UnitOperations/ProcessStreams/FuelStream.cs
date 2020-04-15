using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public abstract class FuelStream : ProcessStreamBase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected FuelStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, mComponents, uoSys) {
      }

      protected void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         AddVarOnListAndRegisterInSystem(moleFlowRate);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         AddVarOnListAndRegisterInSystem(pressure);
         AddVarOnListAndRegisterInSystem(temperature);
         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificHeat);
         AddVarOnListAndRegisterInSystem(density);
         AddVarOnListAndRegisterInSystem(vaporFraction);
      }

      protected override void AdjustVarsStates() {
         if (massFlowRate.HasValue) {
            if (!moleFlowRate.IsSpecifiedAndHasValue) {
               moleFlowRate.State = VarState.Calculated;
            }
            if (!volumeFlowRate.IsSpecifiedAndHasValue) {
               volumeFlowRate.State = VarState.Calculated;
            }
         }
         else if (moleFlowRate.HasValue) {
            if (!massFlowRate.IsSpecifiedAndHasValue) {
               massFlowRate.State = VarState.Calculated;
            }
            if (!volumeFlowRate.IsSpecifiedAndHasValue) {
               volumeFlowRate.State = VarState.Calculated;
            }
         }
         else if (volumeFlowRate.HasValue) {
            if (!massFlowRate.IsSpecifiedAndHasValue) {
               massFlowRate.State = VarState.Calculated;
            }
            if (!moleFlowRate.IsSpecifiedAndHasValue) {
               moleFlowRate.State = VarState.Calculated;
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

         if (pv == moleFlowRate)  
         {
            if (aValue <= 0)  
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }

         return retValue ;
      }
      
      protected FuelStream(SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionFuelStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFuelStream", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
