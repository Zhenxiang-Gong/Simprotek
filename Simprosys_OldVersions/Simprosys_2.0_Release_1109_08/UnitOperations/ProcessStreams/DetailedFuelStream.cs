using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public class DetailedFuelStream : FuelStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;
 
      public DetailedFuelStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, mComponents, uoSys) {
         InitializeVarListAndRegisterVars();
      }

      protected DetailedFuelStream(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDetailedFuelStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionDetailedFuelStream", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
