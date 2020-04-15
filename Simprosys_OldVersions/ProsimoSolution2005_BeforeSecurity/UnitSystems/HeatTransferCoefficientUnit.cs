using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum HeatTransferCoefficientUnitType {WattPerSquareMeterKelvin = 0, WattPerSquareMeterCelsius, 
      KilocaloryPerHourSquareMeterCelsius, CaloryPerSecSquareCentimeterCelsius, 
      CaloryPerHourSquareCentimeterCelsius, BtuPerHourSquareFootFahrenheit, 
      BtuPerSecSquareFootFahrenheit, BtuPerHourSquareFootRankine};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class HeatTransferCoefficientUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static HeatTransferCoefficientUnit() {
         coeffTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, 1.0);                   unitStringTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, "W/m2.K");
         coeffTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, 1.0);                  unitStringTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, "W/m2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.KilocaloryPerHourSquareMeterCelsius, 1.162222);   unitStringTable.Add(HeatTransferCoefficientUnitType.KilocaloryPerHourSquareMeterCelsius, "kcal/hr.m2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.CaloryPerSecSquareCentimeterCelsius, 4.184e4);    unitStringTable.Add(HeatTransferCoefficientUnitType.CaloryPerSecSquareCentimeterCelsius, "cal/s.cm2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.CaloryPerHourSquareCentimeterCelsius, 11.162222); unitStringTable.Add(HeatTransferCoefficientUnitType.CaloryPerHourSquareCentimeterCelsius, "cal/hr.cm2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, 5.678263);        unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, "Btu/hr.ft2.°F");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerSecSquareFootFahrenheit, 2.044175e4);       unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerSecSquareFootFahrenheit, "Btu/s.ft2.°F");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootRankine, 5.678263);           unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootRankine, "Btu/hr.ft2.°R");
      }

      public static double ConvertToSIValue(HeatTransferCoefficientUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(HeatTransferCoefficientUnitType unitType, double wattPerSqMeterKValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return wattPerSqMeterKValue/convertionCoeff;
      }
         
      public static string GetUnitAsString(HeatTransferCoefficientUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static HeatTransferCoefficientUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         HeatTransferCoefficientUnitType type = HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (HeatTransferCoefficientUnitType) myEnumerator.Key;
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
