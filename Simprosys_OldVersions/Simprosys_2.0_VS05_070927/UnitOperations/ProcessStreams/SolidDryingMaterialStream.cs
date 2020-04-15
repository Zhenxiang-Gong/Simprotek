using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.ProcessStreams {

   /// <summary>
   /// Summary description for DryingMaterialStream.
   /// </summary>
   [Serializable]
   public class SolidDryingMaterialStream : DryingMaterialStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public SolidDryingMaterialStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys)
         : base(name, mComponents, MaterialStateType.Solid, uoSys) {
      }

      #region Persistence
      protected SolidDryingMaterialStream(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionLiquidDryingMaterialStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionLiquidDryingMaterialStream", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
      #endregion
   }
}

