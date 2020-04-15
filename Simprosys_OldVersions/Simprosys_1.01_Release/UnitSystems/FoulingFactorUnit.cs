using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum FoulingFactorUnitType {SquareMeterKelvinPerWatt = 0, SquareMeterCelsiusPerWatt, 
      SquareMeterHourCelsiusPerKilocalory, SquareCentimeterSecCelsiusPerCalory, 
      SquareCentimeterHourCelsiusPerCalory, SquareFootHourFahrenheitPerBtu,
      SquareFootSecFahrenheitPerBtu, SquareFootHourRankinePerBtu};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class FoulingFactorUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static FoulingFactorUnit() {
         coeffTable.Add(FoulingFactorUnitType.SquareMeterKelvinPerWatt, 1.0);                      unitStringTable.Add(FoulingFactorUnitType.SquareMeterKelvinPerWatt, "m2.K/W");
         coeffTable.Add(FoulingFactorUnitType.SquareMeterCelsiusPerWatt, 1.0);                     unitStringTable.Add(FoulingFactorUnitType.SquareMeterCelsiusPerWatt, "m2.°C/W");
         coeffTable.Add(FoulingFactorUnitType.SquareMeterHourCelsiusPerKilocalory, 1.0/1.162222);  unitStringTable.Add(FoulingFactorUnitType.SquareMeterHourCelsiusPerKilocalory, "m2.h.°C/kcal");
         coeffTable.Add(FoulingFactorUnitType.SquareCentimeterSecCelsiusPerCalory, 1.0/4.184e4);   unitStringTable.Add(FoulingFactorUnitType.SquareCentimeterSecCelsiusPerCalory, "cm2.s.°C/cal");
         coeffTable.Add(FoulingFactorUnitType.SquareCentimeterHourCelsiusPerCalory, 1.0/11.62222); unitStringTable.Add(FoulingFactorUnitType.SquareCentimeterHourCelsiusPerCalory, "cm2.h.°C/cal");
         coeffTable.Add(FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, 1.0/5.678263);       unitStringTable.Add(FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, "ft2.h.°F/Btu");
         coeffTable.Add(FoulingFactorUnitType.SquareFootSecFahrenheitPerBtu, 1.0/2.044175e4);      unitStringTable.Add(FoulingFactorUnitType.SquareFootSecFahrenheitPerBtu, "ft2.s.°F/Btu");
         coeffTable.Add(FoulingFactorUnitType.SquareFootHourRankinePerBtu, 1.0/5.678263);          unitStringTable.Add(FoulingFactorUnitType.SquareFootHourRankinePerBtu, "ft2.h.°R/Btu");
      }

   public static double ConvertToSIValue(FoulingFactorUnitType unitType, double toBeConvertedValue) {
      double convertionCoeff = (double) coeffTable[unitType];
      return convertionCoeff * toBeConvertedValue;
   }
      
      public static double ConvertFromSIValue(FoulingFactorUnitType unitType, double sqMeterKPerWattValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return sqMeterKPerWattValue/convertionCoeff;
      }
         
      public static string GetUnitAsString(FoulingFactorUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static FoulingFactorUnitType GetUnitAsEnum(string unitString) {           
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         FoulingFactorUnitType type = FoulingFactorUnitType.SquareMeterKelvinPerWatt;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (FoulingFactorUnitType) myEnumerator.Key;
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
