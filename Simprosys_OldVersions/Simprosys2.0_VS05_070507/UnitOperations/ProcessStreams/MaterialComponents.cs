using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   /// <summary>
   /// Summary description for StreamComponents.
   /// </summary>
   [Serializable]
   public class MaterialComponents : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected ArrayList components;
      protected ArrayList phases;
      
      public ArrayList Components {
         get {return components;}
      }

      public MaterialComponents() {
         this.components = new ArrayList();
         this.phases = new ArrayList();
      }

      public MaterialComponents(ArrayList components) {
         this.components = components;
         this.phases = new ArrayList();
      }

      public MaterialComponent this[int index] {
         get {
            MaterialComponent sc = null;
            if (index >= 0 && index < components.Count) {
               sc = components[index] as MaterialComponent;
            }
            return sc;
         }
      }

      public void AddPhase(Phase p) {
         phases.Add(p);
      }

      public ArrayList Phases {
         get { return phases; }
         set { phases = value;}
      }

      public Phase MajorPhase {
         get { return (Phase)phases[0];}
         set { 
            phases[0] = value;
         }
      }

      public int NumberOfPhases {
         get { return phases.Count;}
      }

      public int Count {
         get { return components.Count;}
      }
      
      public void Add(MaterialComponent component) {
         components.Add(component);
      }

      public void Remove(MaterialComponent component) {
         components.Remove(component);
      }

      public void Remove(string name) {
         MaterialComponent sc = null;
         IEnumerator e = components.GetEnumerator();
         while (e.MoveNext()) {
            sc = (e.Current) as MaterialComponent;
            if (sc.Name.Equals(name)) {
               break;
            }
         }
         if (sc != null) components.Remove(sc);
      }

      public void Remove(int index) {
         if (index >= 0 && index < components.Count) {
            components.RemoveAt(index);
         }
      }

      public void Normalize() {
         double total = 0.0;
         foreach(MaterialComponent sc in components) {
            total += sc.MassFraction.Value;
         }
         
         if (total > 1.0e-8) {
            foreach(MaterialComponent sc in components) {
               sc.MassFraction.Value /= total;
            }
         }

         foreach(MaterialComponent sc in components) {
            total += sc.MassFraction.Value/sc.Substance.MolarWeight;
         }
         
         if (total > 1.0e-8) {
            foreach(MaterialComponent sc in components) {
               sc.MoleFraction.Value /= total;
            }
         }
      }

      public virtual void ComponentsFractionsChanged() {
      }

      protected MaterialComponents(SerializationInfo info, StreamingContext context) : base(info, context){
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionMaterialComponents", typeof(int));
         if (persistedClassVersion == 1) {
            this.components = RecallArrayListObject("Components");
            this.phases = RecallArrayListObject("Phases");  //???????
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterialComponents", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Components", this.components, typeof(ArrayList));
         info.AddValue("Phases", this.phases, typeof(ArrayList));
      }
   }
}
