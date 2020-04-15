using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum ThermalConductivityUnitType {WattPerMeterKelvin = 0, WattPerMeterCelsius, 
      WattPerCentimeterKelvin, JoulePerCentimeterSecKelvin, KilojoulePerHourMeterKelvin,  
      CaloryPerCentimeterHourCelsius, CaloryPerCentimeterSecCelsius,  
      KilocaloryPerCentimeterMinCelsius, KilocaloryPerMeterHourCelsius, WattPerInchFahrenheit, 
      WattPerFootFahrenheit, BtuInchPerSquareFootHourFahrenheit, BtuPerInchHourFahrenheit, 
      BtuPerFootSecFahrenheit, BtuPerFootHourFahrenheit};
   

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class ThermalConductivityUnit : EngineeringUnit {
      public static ThermalConductivityUnit Instance = new ThermalConductivityUnit();

      public ThermalConductivityUnit() {
         coeffTable.Add(ThermalConductivityUnitType.WattPerMeterKelvin, 1.0);                         unitStringTable.Add(ThermalConductivityUnitType.WattPerMeterKelvin, "W/m.K");
         coeffTable.Add(ThermalConductivityUnitType.WattPerMeterCelsius, 1.0);                        unitStringTable.Add(ThermalConductivityUnitType.WattPerMeterCelsius, "W/m.°C");
         coeffTable.Add(ThermalConductivityUnitType.WattPerCentimeterKelvin, 100.0);                  unitStringTable.Add(ThermalConductivityUnitType.WattPerCentimeterKelvin, "W/cm.K");
         coeffTable.Add(ThermalConductivityUnitType.JoulePerCentimeterSecKelvin, 100.0);              unitStringTable.Add(ThermalConductivityUnitType.JoulePerCentimeterSecKelvin, "J/cm.s.K");
         coeffTable.Add(ThermalConductivityUnitType.KilojoulePerHourMeterKelvin, 1.0/3.6);            unitStringTable.Add(ThermalConductivityUnitType.KilojoulePerHourMeterKelvin, "kJ/h.m.K");
         coeffTable.Add(ThermalConductivityUnitType.CaloryPerCentimeterHourCelsius, 0.116222222);     unitStringTable.Add(ThermalConductivityUnitType.CaloryPerCentimeterHourCelsius, "cal/cm.h.°C");
         coeffTable.Add(ThermalConductivityUnitType.CaloryPerCentimeterSecCelsius, 418.4);            unitStringTable.Add(ThermalConductivityUnitType.CaloryPerCentimeterSecCelsius, "cal/cm.s.°C");
         coeffTable.Add(ThermalConductivityUnitType.KilocaloryPerCentimeterMinCelsius, 6973.333333);  unitStringTable.Add(ThermalConductivityUnitType.KilocaloryPerCentimeterMinCelsius, "kcal/cm.min.°C");
         coeffTable.Add(ThermalConductivityUnitType.KilocaloryPerMeterHourCelsius, 1.16222222);       unitStringTable.Add(ThermalConductivityUnitType.KilocaloryPerMeterHourCelsius, "kcal/m.h.°C");
         coeffTable.Add(ThermalConductivityUnitType.WattPerInchFahrenheit, 70.8661417);               unitStringTable.Add(ThermalConductivityUnitType.WattPerInchFahrenheit, "W/in.°F");
         coeffTable.Add(ThermalConductivityUnitType.WattPerFootFahrenheit, 5.90551181);               unitStringTable.Add(ThermalConductivityUnitType.WattPerFootFahrenheit, "W/ft.°F");
         coeffTable.Add(ThermalConductivityUnitType.BtuInchPerSquareFootHourFahrenheit, 0.144227889); unitStringTable.Add(ThermalConductivityUnitType.BtuInchPerSquareFootHourFahrenheit, "Btu.in/ft2.h.°F");
         coeffTable.Add(ThermalConductivityUnitType.BtuPerInchHourFahrenheit, 20.768816);             unitStringTable.Add(ThermalConductivityUnitType.BtuPerInchHourFahrenheit, "Btu/in.h.°F");
         coeffTable.Add(ThermalConductivityUnitType.BtuPerFootSecFahrenheit, 6230.6448);              unitStringTable.Add(ThermalConductivityUnitType.BtuPerFootSecFahrenheit, "Btu/ft.s.°F");
         coeffTable.Add(ThermalConductivityUnitType.BtuPerFootHourFahrenheit, 1.73073467);            unitStringTable.Add(ThermalConductivityUnitType.BtuPerFootHourFahrenheit, "Btu/ft.h.°F");
      }
   }
}
