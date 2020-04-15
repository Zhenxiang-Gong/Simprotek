using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   public enum FossilFuelType { Generic = 0, Detailed, Unknown }

   /// <summary>
   /// Summary description for StreamComponents.
   /// </summary>
   [Serializable]
   public class FossilFuel : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      string name;
      bool isUserDefined;

      protected ArrayList componentList;

      private FossilFuelType fossilFuelType;

      public event FossilFuelChangedEventHandler FossilFuelChanged;

      public string Name {
         get { return name; }
         set { name = value; }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
      }

      public ArrayList ComponentList {
         get { return componentList; }
      }

      public FossilFuelType FossilFuelType {
         get { return fossilFuelType; }
      }

      public FossilFuel(string name, ArrayList componentList, FossilFuelType fossilFuelType, bool isUserDefined) {
         this.name = name;
         this.componentList = componentList;
         this.fossilFuelType = fossilFuelType;
         this.isUserDefined = isUserDefined;
      }

      private void OnFossilFuelChanged(bool isNameChangeOnly) {
         if (FossilFuelChanged != null) {
            FossilFuelChanged(this, new FossilFuelChangedEventArgs(this, isNameChangeOnly));
         }
      }
      
      public MaterialComponent this[int index] {
         get {
            MaterialComponent c = null;
            if (index >= 0 && index < componentList.Count) {
               c = componentList[index] as MaterialComponent;
            }
            return c;
         }
      }

      public override string ToString() {
         return name;
      }

      public void Add(MaterialComponent component) {
         componentList.Add(component);
      }

      public void Remove(MaterialComponent component) {
         componentList.Remove(component);
      }

      public void Remove(string name) {
         MaterialComponent c = null;
         IEnumerator e = componentList.GetEnumerator();
         while (e.MoveNext()) {
            c = (e.Current) as MaterialComponent;
            if (c.Substance.Name.Equals(name)) {
               break;
            }
         }
         if (c != null) componentList.Remove(c);
      }

      internal void Update(string name, ArrayList componentList) {
         this.name = name;
         this.componentList = componentList;
         OnFossilFuelChanged(false);
      }
      
      protected FossilFuel(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMaterial", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool)info.GetValue("IsUserDefined", typeof(bool));
            this.componentList = RecallArrayListObject("ComponentList");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterial", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));
         info.AddValue("ComponentList", this.componentList, typeof(ArrayList));
      }
   }
}
