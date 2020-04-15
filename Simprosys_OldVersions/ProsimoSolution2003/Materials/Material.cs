using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   
   public enum MaterialStateType {Solid = 0, Liquid, Sludge, Unknown}

   /// <summary>
   /// Summary description for StreamComponents.
   /// </summary>
   [Serializable]
   public class Material : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      string name;
      bool isUserDefined;

      protected ArrayList substanceList;
      
      public string Name {
         get { return name; }
         set { name = value;}
      }

      public bool IsUserDefined {
         get { return isUserDefined;}
      }

      public ArrayList SubstanceList {
         get {return substanceList;}
      }

      public Material(string name, ArrayList substanceList, bool isUserDefined) {
         this.name = name;
         this.substanceList = substanceList;
         this.isUserDefined = isUserDefined;
      }

      public Substance this[int index] {
         get {
            Substance s = null;
            if (index >= 0 && index < substanceList.Count) {
               s = substanceList[index] as Substance;
            }
            return s;
         }
      }

      public override string ToString() {
         return name;
      }

//      public int Count {
//         get { return substanceList.Count;}
//      }
//      
      public void Add(Substance substance) {
         substanceList.Add(substance);
      }

      public void Remove(Substance substance) {
         substanceList.Remove(substance);
      }

      public void Remove(string name) {
         Substance s = null;
         IEnumerator e = substanceList.GetEnumerator();
         while (e.MoveNext()) {
            s = (e.Current) as Substance;
            if (s.Name.Equals(name)) {
               break;
            }
         }
         if (s != null) substanceList.Remove(s);
      }

//      public void Remove(int index) {
//         if (index >= 0 && index < substanceList.Count) {
//            substanceList.RemoveAt(index);
//         }
//      }
//
      protected Material(SerializationInfo info, StreamingContext context) : base(info, context){
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionMaterial", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string) info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool) info.GetValue("IsUserDefined", typeof(bool));
            //this.substanceList = RecallSubstanceList("SubstanceList");
            //this.substanceList = info.GetValue("SubstanceList", typeof(ArrayList)) as ArrayList;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterial", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));
         info.AddValue("SubstanceList", this.substanceList, typeof(ArrayList));
         //StoreSubstanceList(info, "SubstanceList", this.substanceList);
      }

      private void StoreSubstanceList(SerializationInfo info, string name, ArrayList substanceList) {
         ArrayList nameList = new ArrayList();
         foreach (Substance substance in substanceList) {
            nameList.Add(substance.Name);
         }

         info.AddValue(name, nameList, typeof(ArrayList));
      }
      
      private ArrayList RecallSubstanceList(string name) {
         ArrayList nameList = RecallArrayListObject(name);
         ArrayList substanceList = new ArrayList();
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         foreach (string substanceName in nameList) {
            substanceList.Add(sc.GetSubstance(substanceName));
         }

         return substanceList;
      }
   }        
}
