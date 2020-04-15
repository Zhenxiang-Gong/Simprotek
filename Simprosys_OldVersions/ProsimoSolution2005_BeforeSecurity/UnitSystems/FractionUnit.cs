using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum FractionUnitType {Percent = 0, Decimal};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class FractionUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static FractionUnit() {
         coeffTable.Add(FractionUnitType.Percent, 0.01); unitStringTable.Add(FractionUnitType.Percent, "%");
         coeffTable.Add(FractionUnitType.Decimal, 1.0);  unitStringTable.Add(FractionUnitType.Decimal, "");
      }

      public static double ConvertToSIValue(FractionUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(FractionUnitType unitType, double percentValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return percentValue/convertionCoeff;
      }
   
      public static string GetUnitAsString(FractionUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static FractionUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         FractionUnitType type = FractionUnitType.Decimal;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (FractionUnitType) myEnumerator.Key;
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
