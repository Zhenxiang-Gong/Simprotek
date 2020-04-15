using System;
using System.Runtime.Serialization;
using System.Security.Permissions;                                            

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public class AirFilter : FabricFilter {
      private const int CLASS_PERSISTENCE_VERSION = 1;               

      public AirFilter(string name, UnitOpSystem uoSys) : base(name, uoSys) {
         InitializeVarListAndRegisterVars();
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

