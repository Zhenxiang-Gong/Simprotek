using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.UnitOperations;
using Prosimo.Materials;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for NewProcessSettings.
   /// This is actually "new flowsheet settings"
   /// </summary>
   [Serializable]
   //public class NewProcessSettings : ISerializable
   public class FlowsheetSettings : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 2;

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

      private string fossilFuelName;
      public string FossilFuelName
      {
         get { return fossilFuelName; }
         set { fossilFuelName = value; }
      }


      public FlowsheetSettings() {
         this.dryingGasName = Substance.AIR;
         this.dryingMaterialName = DryingMaterial.GENERIC_DRYING_MATERIAL;
         this.fossilFuelName = FossilFuel.NATURAL_GAS;
      }

      protected FlowsheetSettings(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context) 
      public override void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionNewProcessSettings");
         this.dryingGasName = info.GetString("DryingGasName");
         this.dryingMaterialName = info.GetString("DryingMaterialName");
         this.fossilFuelName = info.GetString("FossilFuelName");
         //for version 1 setfossilFuelName to "Natural Gas" 
         if (this.fossilFuelName == null)
         {
            this.fossilFuelName = FossilFuel.NATURAL_GAS;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionNewProcessSettings", FlowsheetSettings.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("DryingGasName", this.dryingGasName, typeof(string));
         info.AddValue("DryingMaterialName", this.dryingMaterialName, typeof(string));
         //class version 2, software version 3.0
         info.AddValue("FossilFuelName", this.fossilFuelName, typeof(string));
      }
   }
}
