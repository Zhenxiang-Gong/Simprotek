using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SpecificEntropyUnitType {JoulePerKgKelvin = 0, KilojoulePerKgKelvin, JoulePerGramKelvin,  
      BtuPerPoundFahrenheit, BtuPerPoundRankine, CaloryPerGramKelvin, KilocaloryPerKgCelsius}; 
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SpecificEntropyUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static SpecificEntropyUnit() {
         coeffTable.Add(SpecificEntropyUnitType.JoulePerKgKelvin, 1.0);          unitStringTable.Add(SpecificEntropyUnitType.JoulePerKgKelvin, "J/kg.K");
         coeffTable.Add(SpecificEntropyUnitType.KilojoulePerKgKelvin, 1000.0);   unitStringTable.Add(SpecificEntropyUnitType.KilojoulePerKgKelvin, "kJ/kg.K");
         coeffTable.Add(SpecificEntropyUnitType.JoulePerGramKelvin, 1000.0);     unitStringTable.Add(SpecificEntropyUnitType.JoulePerGramKelvin, "J/g.K");
         coeffTable.Add(SpecificEntropyUnitType.BtuPerPoundFahrenheit, 4186.8);  unitStringTable.Add(SpecificEntropyUnitType.BtuPerPoundFahrenheit, "Btu/Ibm.°F");
         coeffTable.Add(SpecificEntropyUnitType.BtuPerPoundRankine, 4186.8);     unitStringTable.Add(SpecificEntropyUnitType.BtuPerPoundRankine, "Btu/Ibm.°R");
         coeffTable.Add(SpecificEntropyUnitType.CaloryPerGramKelvin, 4184.0);    unitStringTable.Add(SpecificEntropyUnitType.CaloryPerGramKelvin, "cal/g.K");
         coeffTable.Add(SpecificEntropyUnitType.KilocaloryPerKgCelsius, 4184.0); unitStringTable.Add(SpecificEntropyUnitType.KilocaloryPerKgCelsius, "kcal/kg.°C");
      }

      public static double ConvertToSIValue(SpecificEntropyUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(SpecificEntropyUnitType unitType, double joulePerKgKValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return joulePerKgKValue/convertionCoeff;
      }
      public static string GetUnitAsString(SpecificEntropyUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static SpecificEntropyUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         SpecificEntropyUnitType type = SpecificEntropyUnitType.JoulePerKgKelvin;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (SpecificEntropyUnitType) myEnumerator.Key;
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
