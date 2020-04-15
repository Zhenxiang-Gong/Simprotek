using System;
using System.Collections;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public abstract class MaterialCache : IProcessVarOwner {
      
      protected string name;
      protected ArrayList materialComponentList;
      protected bool isUserDefined;

      public event NameChangedEventHandler MaterialNameChanged;
      
      public event ProcessVarValueCommittedEventHandler ProcessVarValueCommitted;
      public event MaterialComponentsChangedEventHandler MaterialComponentsChanged;

      public string Name {
         get { return name; }
      }

      public bool IsUserDefined {
         get { return isUserDefined;}
      }

      public ArrayList MaterialComponentList {
         get {return materialComponentList;}
      }

      protected MaterialCache() {
         materialComponentList = new ArrayList();
      }
      
      protected MaterialCache(string name, ArrayList materialComponentList) {
         this.name = name;
         this.materialComponentList = materialComponentList;
      }

      public ErrorMessage Specify(ProcessVarDouble pv, double aValue) {
         if (!pv.HasValueOf(aValue)) {
            pv.Value = aValue;
            pv.State = VarState.Specified;
            OnProcessVarValueCommitted(pv);
         }
         return null;
      }

      public ErrorMessage Specify(ProcessVarInt pv, int aValue) {
         if (pv.Value != aValue) {
            pv.Value = aValue;
            pv.State = VarState.Specified;
            OnProcessVarValueCommitted(pv);
         }

         return null;
      }

      public ErrorMessage Specify(Hashtable procVarAndValueTable) {
         return null;
      }

      private void OnProcessVarValueCommitted(ProcessVar var) {
         if (ProcessVarValueCommitted != null) {
            ProcessVarValueCommitted(var);
         }
      }

      public virtual ErrorMessage FinishSpecifications(string name, bool isNew)
      {
         return null;
      }

      public void AddMaterialComponents(ArrayList substanceList) {
         IList newSubstanceList = new ArrayList();
         foreach(Substance s in substanceList)
         {
            if (!IsInComponentList(s)) {
               newSubstanceList.Add(s);
            }
         }

         if(newSubstanceList.Count > 0)
         {
            foreach(Substance s in newSubstanceList)
            {
               MaterialComponent mc = new MaterialComponent(s);
               materialComponentList.Add(mc);
            }
            OnMaterialComponentsChanged();
         }
      }

      public void AddMaterialComponent(Substance substance) {
         if(!IsInComponentList(substance))
         {
            MaterialComponent mc = new MaterialComponent(substance);
            materialComponentList.Add(mc);
            OnMaterialComponentsChanged();
         }
      }

      private bool IsInComponentList(Substance s)
      {
         bool isIn = false;
         foreach(MaterialComponent mc in materialComponentList)
         {
            if(mc.Substance == s)
            {
               isIn = true;
               break;
            }
         }

         return isIn;
      }

      public void RemoveMaterialComponent(MaterialComponent mc) {
         if (materialComponentList.Count > 0) {
            materialComponentList.Remove(mc);
            OnMaterialComponentsChanged();
         }
      }

      //public void RemoveMaterialComponents(int startIndex, int numOfRows) {
      //   if (startIndex > 0 && (startIndex + numOfRows) < (materialComponentList.Count - 1)) {
      //      materialComponentList.RemoveRange(startIndex, numOfRows);
      //   }
      //}

      public void RemoveAllMaterialComponents() {
         materialComponentList.Clear();
      }

      public void RemoveMaterialComponentAt(int index) {
         if (materialComponentList.Count > 0) {
            if (index > 0 && index < materialComponentList.Count) {
               MaterialComponent mc = (MaterialComponent) materialComponentList[index];
               materialComponentList.RemoveAt(index);
               OnMaterialComponentsChanged();
            }
         }
      }

      public void Normalize() {
         DoNormalization();
         OnMaterialComponentsChanged();
      }

      protected void DoNormalization() {
         double total = 0.0;
         foreach(MaterialComponent mc in materialComponentList) {
            total += mc.MassFraction.Value;
         }
         
         if (total > 1.0e-8) {
            foreach(MaterialComponent mc in materialComponentList) {
               mc.MassFraction.Value /= total;
            }
         }
      }

      private void OnMaterialNameChanged(string name, string oldName) {
         if (MaterialNameChanged != null) {
            MaterialNameChanged(this, name, oldName);
         }
      }

      protected virtual void OnMaterialComponentsChanged()
      {
         if(MaterialComponentsChanged != null)
         {
            MaterialComponentsChanged(materialComponentList);
         }
      }
   }                     
}