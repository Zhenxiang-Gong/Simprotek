using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   [Serializable]
   public class CompositeSubstance : Substance {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ArrayList components;

      public CompositeSubstance(string name, ArrayList components)
         : base(name) {
         this.components = components;
      }

      public CompositeSubstance Clone() {
         CompositeSubstance newCS = (CompositeSubstance)this.MemberwiseClone();
         ArrayList newComponents = new ArrayList();
         MaterialComponent newPC;
         foreach (MaterialComponent mc in this.components) {
            newPC = mc.Clone();
            newComponents.Add(newPC);
         }
         newCS.components = newComponents;
         return newCS;
      }

      public override bool Equals(object obj) {
         bool isEqual = false;
         CompositeSubstance cs = obj as CompositeSubstance;
         if (name.Equals(cs.Name) && this.components.Count == cs.Components.Count) {
            MaterialComponent mc1;
            MaterialComponent mc2;
            for (int i = 0; i < this.components.Count; i++) {
               mc1 = (MaterialComponent) (this.components[i]);
               mc2 = (MaterialComponent) (cs.components[i]);
               if (mc1.Substance == mc2.Substance && Math.Abs(mc1.GetMassFractionValue() - mc2.GetMassFractionValue()) < 1.0e-6) {
                  isEqual = true;
               }
               else {
                  isEqual = false;
                  break;
               }
            }
         }
         return isEqual;
      }

      public override int GetHashCode() {
         return this.name.GetHashCode();
      }

      public override string ToString() {
         return name;
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

      public ArrayList Components {
         get { return components; }
         set { components = value; }
      }

      public void Add(MaterialComponent component) {
         components.Add(component);
      }

      public void Remove(MaterialComponent component) {
         components.Remove(component);
      }

      public void Remove(string name) {
         MaterialComponent mc = null;
         IEnumerator e = components.GetEnumerator();
         while (e.MoveNext()) {
            mc = (e.Current) as MaterialComponent;
            if (mc.Substance.Name.Equals(name)) {
               break;
            }
         }
         if (mc != null) {
            components.Remove(mc);
         }
      }

      public void Remove(int index) {
         if (index >= 0 && index < components.Count) {
            components.RemoveAt(index);
         }
      }

      public void Normalize() {
         double total = 0.0;
         foreach (MaterialComponent sc in components) {
            total += sc.MassFraction.Value;
         }

         if (total > 1.0e-8) {
            foreach (MaterialComponent sc in components) {
               sc.MassFraction.Value /= total;
            }
         }

         foreach (MaterialComponent sc in components) {
            total += sc.MassFraction.Value / sc.Substance.MolarWeight;
         }

         if (total > 1.0e-8) {
            foreach (MaterialComponent sc in components) {
               sc.MoleFraction.Value /= total;
            }
         }
      }

      public static void StoreSubstance(SerializationInfo info, Substance substance) {
         //have to save this flag so as to tell whether substance is a composite substance in recall
         //if (substance == null) return;
         bool isComposite = substance is CompositeSubstance;
         info.AddValue("IsCompositeSubstance", isComposite, typeof(bool));
         if (isComposite) {
            info.AddValue("Substance", substance, typeof(CompositeSubstance));
         }
         else {
            info.AddValue("SubstanceName", substance.Name, typeof(string));
         }
      }

      public static Substance RecallSubstance(SerializationInfo info) {
         bool isComposite = (bool)info.GetValue("IsCompositeSubstance", typeof(bool));
         Substance s;
         if (isComposite) {
            s = Storable.RecallStorableObject(info, "Substance", typeof(CompositeSubstance)) as CompositeSubstance;
         }
         else {
            s = Substance.RecallSubstance(info, "SubstanceName");
         }
         return s;
      }

      protected CompositeSubstance(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCompositeSubstance", typeof(int));
         if (persistedClassVersion == 1) {
            this.components = RecallArrayListObject("Components");
            //this.components = info.GetValue("Components", typeof (ArrayList)) as ArrayList;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCompositeSubstance", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Components", this.components, typeof(ArrayList));
      }
   }
}
