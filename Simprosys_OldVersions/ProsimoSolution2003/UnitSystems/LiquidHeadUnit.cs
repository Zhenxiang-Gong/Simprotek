using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum LiquidHeadUnitType {Meter = 0, Millimeter, Centimeter, Inch, Foot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class LiquidHeadUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static LiquidHeadUnit() {
         coeffTable.Add(LiquidHeadUnitType.Meter, 1.0);         unitStringTable.Add(LiquidHeadUnitType.Meter, "m");
         coeffTable.Add(LiquidHeadUnitType.Millimeter, 1.0e-3); unitStringTable.Add(LiquidHeadUnitType.Millimeter, "mm");
         coeffTable.Add(LiquidHeadUnitType.Centimeter, 0.01);   unitStringTable.Add(LiquidHeadUnitType.Centimeter, "cm");
         coeffTable.Add(LiquidHeadUnitType.Inch, 0.0254);       unitStringTable.Add(LiquidHeadUnitType.Inch, "in");
         coeffTable.Add(LiquidHeadUnitType.Foot, 0.3048);       unitStringTable.Add(LiquidHeadUnitType.Foot, "ft");
      }

      public static double ConvertToSIValue(LiquidHeadUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(LiquidHeadUnitType unitType, double meterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return meterValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(LiquidHeadUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
      
      public static LiquidHeadUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         LiquidHeadUnitType type = LiquidHeadUnitType.Meter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (LiquidHeadUnitType) myEnumerator.Key;
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
