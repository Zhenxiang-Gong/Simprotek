using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SpecificEntropyUnitType {JoulePerKgKelvin = 0, KilojoulePerKgKelvin, JoulePerGramKelvin,  
      BtuPerPoundFahrenheit, BtuPerPoundRankine, CaloryPerGramKelvin, KilocaloryPerKgCelsius}; 
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SpecificEntropyUnit : EngineeringUnit {
      public static SpecificEntropyUnit Instance = new SpecificEntropyUnit();

      private SpecificEntropyUnit() {
         coeffTable.Add(SpecificEntropyUnitType.JoulePerKgKelvin, 1.0);          unitStringTable.Add(SpecificEntropyUnitType.JoulePerKgKelvin, "J/kg.K");
         coeffTable.Add(SpecificEntropyUnitType.KilojoulePerKgKelvin, 1000.0);   unitStringTable.Add(SpecificEntropyUnitType.KilojoulePerKgKelvin, "kJ/kg.K");
         coeffTable.Add(SpecificEntropyUnitType.JoulePerGramKelvin, 1000.0);     unitStringTable.Add(SpecificEntropyUnitType.JoulePerGramKelvin, "J/g.K");
         coeffTable.Add(SpecificEntropyUnitType.BtuPerPoundFahrenheit, 4186.8);  unitStringTable.Add(SpecificEntropyUnitType.BtuPerPoundFahrenheit, "Btu/Ib.°F");
         coeffTable.Add(SpecificEntropyUnitType.BtuPerPoundRankine, 4186.8);     unitStringTable.Add(SpecificEntropyUnitType.BtuPerPoundRankine, "Btu/Ib.°R");
         coeffTable.Add(SpecificEntropyUnitType.CaloryPerGramKelvin, 4184.0);    unitStringTable.Add(SpecificEntropyUnitType.CaloryPerGramKelvin, "cal/g.K");
         coeffTable.Add(SpecificEntropyUnitType.KilocaloryPerKgCelsius, 4184.0); unitStringTable.Add(SpecificEntropyUnitType.KilocaloryPerKgCelsius, "kcal/kg.°C");
      }
   }
}
