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

      string _Name;
      bool _IsUserDefined = true;

      protected ArrayList _ComponentList;

      private FossilFuelType _FossilFuelType = FossilFuelType.Detailed;

      public static string NATURAL_GAS = "Natural Gas";

      public event FossilFuelChangedEventHandler FossilFuelChanged;

      public string Name {
         get { return _Name; }
         set { _Name = value; }
      }

      public bool IsUserDefined {
         get { return _IsUserDefined; }
         set { _IsUserDefined = value; }
      }

      public ArrayList ComponentList {
         get { return _ComponentList; }
      }

      public FossilFuelType FossilFuelType {
         get { return _FossilFuelType; }
      }

      public FossilFuel(string name, ArrayList componentList, FossilFuelType fossilFuelType, bool isUserDefined) {
         this._Name = name;
         this._ComponentList = componentList;
         this._FossilFuelType = fossilFuelType;
         this._IsUserDefined = isUserDefined;
      }

      private void OnFossilFuelChanged(bool isNameChangeOnly) {
         if (FossilFuelChanged != null) {
            FossilFuelChanged(this, new FossilFuelChangedEventArgs(this, isNameChangeOnly));
         }
      }
      
      public MaterialComponent this[int index] {
         get {
            MaterialComponent c = null;
            if (index >= 0 && index < _ComponentList.Count) {
               c = _ComponentList[index] as MaterialComponent;
            }
            return c;
         }
      }

      public override string ToString() {
         return _Name;
      }

      public void Add(MaterialComponent component) {
         _ComponentList.Add(component);
      }

      public void Remove(MaterialComponent component) {
         _ComponentList.Remove(component);
      }

      public void Remove(string name) {
         MaterialComponent c = null;
         IEnumerator e = _ComponentList.GetEnumerator();
         while (e.MoveNext()) {
            c = (e.Current) as MaterialComponent;
            if (c.Substance.Name.Equals(name)) {
               break;
            }
         }
         if (c != null) _ComponentList.Remove(c);
      }

      internal void Update(string name, ArrayList componentList) {
         this._Name = name;
         this._ComponentList = componentList;
         OnFossilFuelChanged(false);
      }

      public override bool Equals(object obj)
      {
         FossilFuel ff = obj as FossilFuel;
         bool isEqual = false;
         if (ff != null && this.Name == ff.Name && this.ComponentList.Count == ff.ComponentList.Count) {
            bool isMaterialComponentEqual = true;
            for (int i = 0; i < this.ComponentList.Count; i++)
            {
               MaterialComponent thisMC = this.ComponentList[i] as MaterialComponent;
               MaterialComponent thatMC = ff.ComponentList[i] as MaterialComponent;
               if (thisMC.Substance != thatMC.Substance || Math.Abs(thisMC.MassFraction.Value - thatMC.MassFraction.Value) > 1.0e-4)
               {
                  isMaterialComponentEqual = false;
                  break;
               }
            }
            isEqual = isMaterialComponentEqual;
         }
         return isEqual;
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

      public FossilFuel Clone()
      {         
         ArrayList newComponents = new ArrayList();
         MaterialComponent newPC;
         foreach(MaterialComponent mc in _ComponentList)
         {
            newPC = mc.Clone();
            newComponents.Add(newPC);
         }

         return new FossilFuel(_Name, newComponents, _FossilFuelType, true);
      }

      protected FossilFuel(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFossilFuel", typeof(int));
         if (persistedClassVersion == 1) {
            this._Name = (string)info.GetValue("Name", typeof(string));
            this._IsUserDefined = (bool)info.GetValue("IsUserDefined", typeof(bool));
            this._FossilFuelType = (FossilFuelType)info.GetValue("FossilFuelType", typeof(FossilFuelType));
            this._ComponentList = RecallArrayListObject("ComponentList");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFossilFuel", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this._Name, typeof(string));
         info.AddValue("IsUserDefined", this._IsUserDefined, typeof(bool));
         info.AddValue("FossilFuelType", this._FossilFuelType, typeof(FossilFuelType));
         info.AddValue("ComponentList", this._ComponentList, typeof(ArrayList));
      }
   }
}
