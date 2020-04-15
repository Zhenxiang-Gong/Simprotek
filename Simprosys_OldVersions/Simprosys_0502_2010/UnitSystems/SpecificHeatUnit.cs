using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SpecificHeatUnitType {JoulePerKgKelvin = 0, JoulePerGramKelvin, KilojoulePerKgKelvin, 
      JoulePerKgCelsius, KilojoulePerKgCelsius, MegajoulePerKgKelvin, KilowattHourPerKgCelsius, 
      JoulePerPoundCelsius, CaloryPerGramKelvin, KilocaloryPerKgCelsius, KilocaloryPerKgKelvin, 
      BtuPerPoundFahrenheit, BtuPerPoundCelcius, FootIbfPerPoundFahrenheit, 
      FootIbfPerPoundCelsius, KgfMeterPerKgCelsius};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SpecificHeatUnit : EngineeringUnit {
      public static SpecificHeatUnit Instance = new SpecificHeatUnit();

      private SpecificHeatUnit() {
         coeffTable.Add(SpecificHeatUnitType.JoulePerKgKelvin, 1.0);                 unitStringTable.Add(SpecificHeatUnitType.JoulePerKgKelvin, "J/kg.K");
         coeffTable.Add(SpecificHeatUnitType.JoulePerGramKelvin, 1000.0);            unitStringTable.Add(SpecificHeatUnitType.JoulePerGramKelvin, "J/g.K");
         coeffTable.Add(SpecificHeatUnitType.KilojoulePerKgKelvin, 1000.0);          unitStringTable.Add(SpecificHeatUnitType.KilojoulePerKgKelvin, "kJ/kg.K");
         coeffTable.Add(SpecificHeatUnitType.JoulePerKgCelsius, 1.0);                unitStringTable.Add(SpecificHeatUnitType.JoulePerKgCelsius, "J/kg.°C");
         coeffTable.Add(SpecificHeatUnitType.KilojoulePerKgCelsius, 1000.0);         unitStringTable.Add(SpecificHeatUnitType.KilojoulePerKgCelsius, "kJ/kg.°C");
         coeffTable.Add(SpecificHeatUnitType.MegajoulePerKgKelvin, 1.0e6);           unitStringTable.Add(SpecificHeatUnitType.MegajoulePerKgKelvin, "MJ/kg.K");
         coeffTable.Add(SpecificHeatUnitType.KilowattHourPerKgCelsius, 3.6e6);       unitStringTable.Add(SpecificHeatUnitType.KilowattHourPerKgCelsius, "kWh/kg.°C");
         coeffTable.Add(SpecificHeatUnitType.JoulePerPoundCelsius, 2.20462262);      unitStringTable.Add(SpecificHeatUnitType.JoulePerPoundCelsius, "J/Ib.°C");
         coeffTable.Add(SpecificHeatUnitType.CaloryPerGramKelvin, 4184.0);           unitStringTable.Add(SpecificHeatUnitType.CaloryPerGramKelvin, "cal/g.K");
         coeffTable.Add(SpecificHeatUnitType.KilocaloryPerKgCelsius, 4184.0);        unitStringTable.Add(SpecificHeatUnitType.KilocaloryPerKgCelsius, "kcal/kg.°C");
         coeffTable.Add(SpecificHeatUnitType.KilocaloryPerKgKelvin, 4184.0);         unitStringTable.Add(SpecificHeatUnitType.KilocaloryPerKgKelvin, "kcal/kg.K");
         coeffTable.Add(SpecificHeatUnitType.BtuPerPoundFahrenheit, 4186.8);         unitStringTable.Add(SpecificHeatUnitType.BtuPerPoundFahrenheit, "Btu/Ib.°F");
         coeffTable.Add(SpecificHeatUnitType.BtuPerPoundCelcius, 2326.0);            unitStringTable.Add(SpecificHeatUnitType.BtuPerPoundCelcius, "Btu/Ib.°C");
         coeffTable.Add(SpecificHeatUnitType.FootIbfPerPoundFahrenheit, 5.38032046); unitStringTable.Add(SpecificHeatUnitType.FootIbfPerPoundFahrenheit, "ft.Ibf/Ib.°F");
         coeffTable.Add(SpecificHeatUnitType.FootIbfPerPoundCelsius, 2.98906692);    unitStringTable.Add(SpecificHeatUnitType.FootIbfPerPoundCelsius, "ft.Ibf/Ib.°C");
         coeffTable.Add(SpecificHeatUnitType.KgfMeterPerKgCelsius, 9.80665);         unitStringTable.Add(SpecificHeatUnitType.KgfMeterPerKgCelsius, "kgf.m/kg.°C");
      }
   }
}
