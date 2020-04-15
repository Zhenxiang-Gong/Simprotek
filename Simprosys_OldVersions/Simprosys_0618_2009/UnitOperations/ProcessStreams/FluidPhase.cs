using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for FluidPhase.
   /// </summary>
   
   [Serializable]
   public class FluidPhase : Phase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public FluidPhase(string name, ArrayList components) : base(name, components) {
      }

      protected FluidPhase(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFluidPhase", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFluidPhase", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
