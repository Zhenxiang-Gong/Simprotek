using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
   public class Class1
   {
      private string name;
      public string Name
      {
         get { return name; }
      }

      public Class1(string name)
      {
         this.name = name;
      }
   }
}
