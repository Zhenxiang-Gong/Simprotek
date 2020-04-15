using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {

   [Serializable]
   public class SubstanceFormula : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private IList elementAndCountList;

      public string[] Elements {
         get {
            string[] elements = new string[elementAndCountList.Count];
            for (int i = 0; i < elementAndCountList.Count; i++) {
               elements[i] = ((ElementAndCount)elementAndCountList[i]).ElementName;
            }
            return elements;
         }
      }

      public SubstanceFormula(IList elementAndCountList) {
         this.elementAndCountList = elementAndCountList;
      }

      public SubstanceFormula() {
         this.elementAndCountList = new ArrayList();
      }

      public void AddElement(string elementName, int elementCount) {
         elementAndCountList.Add(new ElementAndCount(elementName, elementCount));
      }

      public int GetElementCount(string elementName) {
         int count = 0;
         foreach (ElementAndCount elementAndCount in elementAndCountList) {
            if (elementName.Equals(elementAndCount.ElementName)) {
               count = elementAndCount.ElementCount;
               break;
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

      protected SubstanceFormula(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionSubstanceFormula");
         if (persistedClassVersion == 1) {
            this.elementAndCountList = RecallArrayListObject("ElementAndCountList");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionSubstanceFormula", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("ElementAndCountList", this.elementAndCountList, typeof(IList));
      }
   }
}
