using System;
using System.Collections;
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
         set { name = value;}
      }

      public Substance Substance {
         get {return substance;}
      }

      public bool IsUserDefined {
         get { return isUserDefined;}
      }

      public DryingGas(string name, Substance substance, bool isUserDefined) {
         this.name = name;
         this.substance = substance;
         this.isUserDefined = isUserDefined;
      }

      protected DryingGas(SerializationInfo info, StreamingContext context) : base(info, context){
      }

      public override string ToString() {
         return name;
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionMaterial", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string) info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool) info.GetValue("IsUserDefined", typeof(bool));
            string substanceName = info.GetValue("SubstanceName", typeof(string)) as string;
            this.substance = SubstanceCatalog.GetInstance().GetSubstance(substanceName);
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterial", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool)); 
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("SubstanceName", this.substance.Name, typeof(string));
      }
   }        
}
   /*[Serializable]
   public class DryingGas : Material {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public DryingGas(string name, ArrayList substances) : base(name, substances) {
      }

      public Substance DryMedium {
         get {
            Substance s = (Substance)substanceList[0];
            return s;
         }

         set { 
            substanceList[0] = value;
         }
      }
   
      protected DryingGas(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

       public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingGas", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingGas", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }*/
