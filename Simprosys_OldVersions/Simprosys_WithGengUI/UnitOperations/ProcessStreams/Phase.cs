using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   /// <summary>
   /// Summary description for Phase.
   /// </summary>
   [Serializable]
   public class Phase : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private string name;
      
      protected ArrayList components;
      
      protected double massFraction;

      //protected double temperature;
      
      //protected double cp;
      //protected double k;

      public Phase(string name, ArrayList components) {
         this.name = name;
         this.components = components;
      }

      protected ArrayList CloneComponents() {
         ArrayList newComponents = new ArrayList();
         MaterialComponent newPC;
         foreach (MaterialComponent sc in this.components) {
            newPC = sc.Clone();
            newComponents.Add(newPC);
         }
         return newComponents;
      }

      public ArrayList Components {
         get {return components;}
         set {components = value;}
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

      public string Name {
         get {return name;}
         set { name = value;}
      }
      
      public int NumberOfComponents {
         get { return components.Count;}
      }

      public double MassFraction {
         get {return massFraction;}
         set {
            massFraction = value;
         }
      }

      public void Add(MaterialComponent component) {
         components.Add(component);
      }

      public void Remove(MaterialComponent component) {
         components.Remove(component);
      }

      public void Remove(string name) {
         MaterialComponent scToBeDeleted = null;
         foreach (MaterialComponent sc in components) {
            if (sc.Name.Equals(name)) {
               scToBeDeleted = sc;
               break;
            }
         }
         if (scToBeDeleted != null) {
            components.Remove(scToBeDeleted);
         }
      }

      public void Remove(int index) {
         if (index >= 0 && index < components.Count) {
            components.RemoveAt(index);
         }
      }

      public void Normalize() {
         double total = 0.0;
         foreach(MaterialComponent sc in components) {
            total += sc.GetMassFractionValue();
         }
         
         if (total > 1.0e-8) {
            foreach(MaterialComponent sc in components) {
               sc.SetMassFractionValue(sc.GetMassFractionValue()/total);
            }
         }

         total = 0.0;
         //if (!sc.IsUserDefined) {
         foreach(MaterialComponent sc in components) {
            sc.SetMoleFractionValue(sc.GetMassFractionValue()/sc.Substance.MolarWeight);
            total += sc.GetMassFractionValue()/sc.Substance.MolarWeight;
         }
         
         if (total > 1.0e-8) {
            foreach(MaterialComponent sc in components) {
               sc.SetMoleFractionValue(sc.GetMoleFractionValue()/total);
            }
         }
      }
      
      protected Phase(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPhase", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.components = RecallArrayListObject("Components");
            this.massFraction = (double)info.GetValue("MassFraction", typeof(double));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionPhase", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("Components", this.components, typeof(ArrayList));
         info.AddValue("MassFraction", this.massFraction, typeof(double));
      }
   }
}
