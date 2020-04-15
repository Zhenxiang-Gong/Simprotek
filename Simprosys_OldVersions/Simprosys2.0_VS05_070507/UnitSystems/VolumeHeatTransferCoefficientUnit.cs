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
   public class VolumeHeatTransferCoefficientUnit : EngineeringUnit {
      public static VolumeHeatTransferCoefficientUnit Instance = new VolumeHeatTransferCoefficientUnit();

      public VolumeHeatTransferCoefficientUnit() {
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin, 1.0);                   unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin, "W/m3.K");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, 1.0);                  unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, "W/m3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.KilocaloryPerHourCubicMeterCelsius, 1.162222);   unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.KilocaloryPerHourCubicMeterCelsius, "kcal/h.m3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerSecCubicCentimeterCelsius, 4.184e6);    unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerSecCubicCentimeterCelsius, "cal/s.cm3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerHourCubicCentimeterCelsius, 1.1162222e3); unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.CaloryPerHourCubicCentimeterCelsius, "cal/h.cm3.°C");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit, 18.62947286);        unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit, "Btu/h.ft3.°F");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerSecCubicFootFahrenheit, 6.70661023e4);       unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerSecCubicFootFahrenheit, "Btu/s.ft3.°F");
         coeffTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootRankine, 18.62947286);           unitStringTable.Add(VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootRankine, "Btu/h.ft3.°R");
      }
   }
}
