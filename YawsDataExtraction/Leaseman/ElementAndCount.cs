using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class ElementAndCount {
      private string elementName;
      private int elementCount;

      public string ElementName {
         get { return elementName; }
      }

      public int ElementCount {
         get { return elementCount; }
      }

      public ElementAndCount(string elementName, int elementCount) {
         this.elementName = elementName;
         this.elementCount = elementCount;
      }
   }
}
