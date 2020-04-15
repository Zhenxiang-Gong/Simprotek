using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VolumeHeatTransferCoefficientUnitType {WattPerCubicMeterKelvin = 0, WattPerCubicMeterCelsius, 
      KilocaloryPerHourCubicMeterCelsius, CaloryPerSecCubicCentimeterCelsius, 
      CaloryPerHourCubicCentimeterCelsius, BtuPerHourCubicFootFahrenheit, 
      BtuPerSecCubicFootFahrenheit, BtuPerHourCubicFootRankine};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class VolumeHeatTransferCoefficientUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VolumeHeatTransferCoefficientUnit() {
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin, 1.0);                   unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin, "W/m3.K");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, 1.0);                  unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, "W/m3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.KilocaloryPerHourCubicMeterCelsius, 1.162222);   unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.KilocaloryPerHourCubicMeterCelsius, "kcal/hr.m3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerSecCubicCentimeterCelsius, 4.184e6);    unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerSecCubicCentimeterCelsius, "cal/s.cm3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerHourCubicCentimeterCelsius, 1.1162222e3); unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerHourCubicCentimeterCelsius, "cal/hr.cm3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit, 18.62947286);        unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit, "Btu/hr.ft3.°F");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerSecCubicFootFahrenheit, 6.70661023e4);       unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerSecCubicFootFahrenheit, "Btu/s.ft3.°F");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootRankine, 18.62947286);           unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootRankine, "Btu/hr.ft3.°R");
      }

      public static double ConvertToSIValue(VolumeHeatTransferCoefficientUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VolumeHeatTransferCoefficientUnitType unitType, double wattPerSqMeterKValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return wattPerSqMeterKValue/convertionCoeff;
      }
         
      public static string GetUnitAsString(VolumeHeatTransferCoefficientUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VolumeHeatTransferCoefficientUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VolumeHeatTransferCoefficientUnitType type = VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VolumeHeatTransferCoefficientUnitType) myEnumerator.Key;
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
