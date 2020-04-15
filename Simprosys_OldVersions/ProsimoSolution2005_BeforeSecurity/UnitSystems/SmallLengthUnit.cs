using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SmallLengthUnitType {Meter = 0, Micrometer, Millimeter, Centimeter, Decimeter,
      Inch, Foot, Yard};

   /// <summary>
	/// Summary description .
	/// </summary>
   public class SmallLengthUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static SmallLengthUnit() {
         coeffTable.Add(SmallLengthUnitType.Meter, 1.0);         unitStringTable.Add(SmallLengthUnitType.Meter, "m");
         coeffTable.Add(SmallLengthUnitType.Micrometer, 1.0e-6); unitStringTable.Add(SmallLengthUnitType.Micrometer, "µm");
         coeffTable.Add(SmallLengthUnitType.Millimeter, 1.0e-3); unitStringTable.Add(SmallLengthUnitType.Millimeter, "mm");
         coeffTable.Add(SmallLengthUnitType.Centimeter, 0.01);   unitStringTable.Add(SmallLengthUnitType.Centimeter, "cm");
         coeffTable.Add(SmallLengthUnitType.Decimeter, 0.1);     unitStringTable.Add(SmallLengthUnitType.Decimeter, "dm");
         coeffTable.Add(SmallLengthUnitType.Inch, 0.0254);       unitStringTable.Add(SmallLengthUnitType.Inch, "in");
         coeffTable.Add(SmallLengthUnitType.Foot, 0.3048);       unitStringTable.Add(SmallLengthUnitType.Foot, "ft");
         coeffTable.Add(SmallLengthUnitType.Yard, 0.9144);       unitStringTable.Add(SmallLengthUnitType.Yard, "yd");
      }

      public static double ConvertToSIValue(SmallLengthUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(SmallLengthUnitType unitType, double meterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return meterValue/convertionCoeff;
      }

      public static string GetUnitAsString(SmallLengthUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
      
      public static SmallLengthUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         SmallLengthUnitType type = SmallLengthUnitType.Centimeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (SmallLengthUnitType) myEnumerator.Key;
               break;
            }
         }
         return type;
      }

      public static ICollection GetUnitsAsStrings() {
         return unitStringTable.Values;
      }
   }
}
