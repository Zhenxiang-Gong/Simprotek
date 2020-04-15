using System;
using System.Collections;

namespace Drying.SubstanceLibrary {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class CompositeSubstance : Substance{

      private ArrayList substances;

      public CompositeSubstance(string name, ArrayList substances) :base (name) {
         this.substances = substances;
      }

      public ArrayList Substances {
         get {return substances;}
         set {substances = value;}
      }

      public void Add(Substance substance) {
         substances.Add(substance);
      }

      public void Remove(Substance substance) {
         substances.Remove(substance);
      }

      public void Remove(string name) {
         Substance s = null;
         IEnumerator e = substances.GetEnumerator();
         while (e.MoveNext()) {
            s = (e.Current) as Substance;
            if (s.Name.Equals(name)) {
               break;
            }
         }
         if (s != null) substances.Remove(s);
      }

      public void Remove(int index) {
         if (index >= 0 && index < substances.Count) {
            substances.RemoveAt(index);
         }
      }
   }
}
