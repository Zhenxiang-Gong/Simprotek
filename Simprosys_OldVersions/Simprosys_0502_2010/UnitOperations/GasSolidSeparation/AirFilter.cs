using System;
using System.Runtime.Serialization;
using System.Security.Permissions;                                            

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public class AirFilter : FabricFilter {
      private const int CLASS_PERSISTENCE_VERSION = 1;               

      public AirFilter(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         InitializeVarListAndRegisterVars();
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue < outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the air filter inlet must be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue > inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the air filter outlet cannot be greater than that of the inlet.");
         }

         return retValue;
      }

      public override void Execute(bool propagate) {
         base.Execute(propagate);
         PostSolve();
      }
      
      protected AirFilter(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
      }
   }
}

