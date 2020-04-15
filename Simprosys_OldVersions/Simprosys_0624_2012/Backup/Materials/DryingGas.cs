using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {

   /// <summary>
   /// Summary description for StreamComponents.
   /// </summary>
   [Serializable]
   public class DryingGas : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      string name;
      bool isUserDefined;

      private Substance substance;

      public string Name {
         get { return name; }
         set { name = value; }
      }

      public Substance Substance {
         get { return substance; }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
      }

      public DryingGas(string name, Substance substance, bool isUserDefined) {
         this.name = name;
         this.substance = substance;
         this.isUserDefined = isUserDefined;
      }

      public override bool Equals(object obj) {
         DryingGas dg = obj as DryingGas;
         bool isEqual = false;
         if (dg != null && this.name == dg.Name && this.isUserDefined == dg.IsUserDefined && this.substance == dg.Substance) {
            isEqual = true;
         }
         return isEqual;
      }

      public override int GetHashCode() {
         return name.GetHashCode();
      }

      public override string ToString() {
         return this.substance.ToString();
      }

      protected DryingGas(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryingGas", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool)info.GetValue("IsUserDefined", typeof(bool));
            string substanceName = info.GetValue("SubstanceName", typeof(string)) as string;
            this.substance = SubstanceCatalog.GetSubstance(substanceName);
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingGas", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("SubstanceName", this.substance.Name, typeof(string));
      }
   }
}
