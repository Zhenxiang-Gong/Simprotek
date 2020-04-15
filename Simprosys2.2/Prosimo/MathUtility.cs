using System;

namespace Prosimo {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class MathUtility {
      public MathUtility() {
         //
         // TODO: Add constructor logic here
         //
      }

      public static double Average(double a, double b) {
         double average = 0;
         if (a != Constants.NO_VALUE && b != Constants.NO_VALUE) {
            average = (a + b) / 2.0;
         }
         else if (a != Constants.NO_VALUE) {
            average = a;
         }
         else if (b != Constants.NO_VALUE) {
            average = b;
         }
         else {
         }

         return average;
      }
   }
}
