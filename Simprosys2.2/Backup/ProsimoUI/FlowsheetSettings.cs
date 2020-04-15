using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.UnitOperations;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for NewProcessSettings.
   /// This is actually "new flowsheet settings"
   /// </summary>
   [Serializable]
   //public class NewProcessSettings : ISerializable
   public class FlowsheetSettings : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private string dryingGasName;
      public string DryingGasName {
         get { return dryingGasName; }
         set { dryingGasName = value; }
      }

      private string dryingMaterialName;
      public string DryingMaterialName {
         get { return dryingMaterialName; }
         set { dryingMaterialName = value; }
      }

      public FlowsheetSettings() {
         this.dryingGasName = Substance.AIR;
         this.dryingMaterialName = "Generic Drying Material";
      }

      protected FlowsheetSettings(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context) 
      public override void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionNewProcessSettings");
         this.dryingGasName = info.GetString("DryingGasName");
         this.dryingMaterialName = info.GetString("DryingMaterialName");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionNewProcessSettings", FlowsheetSettings.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("DryingGasName", this.dryingGasName, typeof(string));
         info.AddValue("DryingMaterialName", this.dryingMaterialName, typeof(string));
      }
   }
}
