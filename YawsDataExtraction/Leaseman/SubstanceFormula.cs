using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class SubstanceFormula {
      private IList<ElementAndCount> elementAndCountList;

      public string[] Elements {
         get {
            string[] elements = new string[elementAndCountList.Count];
            for (int i = 0; i < elementAndCountList.Count; i++) {
               elements[i] = elementAndCountList[i].ElementName;
            }
            return elements;
         }
      }

      internal SubstanceFormula(IList<ElementAndCount> elementAndCountList) {
         this.elementAndCountList = elementAndCountList;
      }

      internal SubstanceFormula() {
         this.elementAndCountList = new List<ElementAndCount>();
      }

      internal void AddElement(string elementName, int elementCount) {
         elementAndCountList.Add(new ElementAndCount(elementName, elementCount));
      }

      public int GetElementCount(string elementName) {
         int count = 0;
         foreach (ElementAndCount elementAndCount in elementAndCountList) {
            if (elementName.Equals(elementAndCount.ElementName)) {
               count = elementAndCount.ElementCount;
            }
         }
         return count;
      }

      public double GetMolarWeight() {
         double totalWeight = 0.0;
         foreach (ElementAndCount elementAndCount in elementAndCountList) {
            totalWeight += elementAndCount.ElementCount * PeriodicTable.GetElementWeight(elementAndCount.ElementName);
         }
         return totalWeight;
      }

      public override string ToString() {
         return GetFormulaString();
      }

      private string GetFormulaString() {
         StringBuilder sb = new StringBuilder();
         foreach (ElementAndCount elementAndCount in elementAndCountList) {
            sb.Append(elementAndCount.ElementName);
            if (elementAndCount.ElementCount > 1) {
               sb.Append(elementAndCount.ElementCount);
            }
         }
         return sb.ToString();
      }
   }
}
