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
   public class HeatTransferCoefficientUnit : EngineeringUnit {
      public static HeatTransferCoefficientUnit Instance = new HeatTransferCoefficientUnit();

      public HeatTransferCoefficientUnit() {
         coeffTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, 1.0);                   unitStringTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, "W/m2.K");
         coeffTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, 1.0);                  unitStringTable.Add(HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, "W/m2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.KilocaloryPerHourSquareMeterCelsius, 1.162222);   unitStringTable.Add(HeatTransferCoefficientUnitType.KilocaloryPerHourSquareMeterCelsius, "kcal/h.m2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.CaloryPerSecSquareCentimeterCelsius, 4.184e4);    unitStringTable.Add(HeatTransferCoefficientUnitType.CaloryPerSecSquareCentimeterCelsius, "cal/s.cm2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.CaloryPerHourSquareCentimeterCelsius, 11.162222); unitStringTable.Add(HeatTransferCoefficientUnitType.CaloryPerHourSquareCentimeterCelsius, "cal/h.cm2.°C");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, 5.678263);        unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, "Btu/h.ft2.°F");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerSecSquareFootFahrenheit, 2.044175e4);       unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerSecSquareFootFahrenheit, "Btu/s.ft2.°F");
         coeffTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootRankine, 5.678263);           unitStringTable.Add(HeatTransferCoefficientUnitType.BtuPerHourSquareFootRankine, "Btu/h.ft2.°R");
      }
   }
}
