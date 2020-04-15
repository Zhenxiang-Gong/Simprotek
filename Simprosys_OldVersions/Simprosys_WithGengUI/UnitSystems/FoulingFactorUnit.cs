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
   public class FoulingFactorUnit : EngineeringUnit {
      public static FoulingFactorUnit Instance = new FoulingFactorUnit();

      public FoulingFactorUnit() {
         coeffTable.Add(FoulingFactorUnitType.SquareMeterKelvinPerWatt, 1.0);                      unitStringTable.Add(FoulingFactorUnitType.SquareMeterKelvinPerWatt, "m2.K/W");
         coeffTable.Add(FoulingFactorUnitType.SquareMeterCelsiusPerWatt, 1.0);                     unitStringTable.Add(FoulingFactorUnitType.SquareMeterCelsiusPerWatt, "m2.°C/W");
         coeffTable.Add(FoulingFactorUnitType.SquareMeterHourCelsiusPerKilocalory, 1.0/1.162222);  unitStringTable.Add(FoulingFactorUnitType.SquareMeterHourCelsiusPerKilocalory, "m2.h.°C/kcal");
         coeffTable.Add(FoulingFactorUnitType.SquareCentimeterSecCelsiusPerCalory, 1.0/4.184e4);   unitStringTable.Add(FoulingFactorUnitType.SquareCentimeterSecCelsiusPerCalory, "cm2.s.°C/cal");
         coeffTable.Add(FoulingFactorUnitType.SquareCentimeterHourCelsiusPerCalory, 1.0/11.62222); unitStringTable.Add(FoulingFactorUnitType.SquareCentimeterHourCelsiusPerCalory, "cm2.h.°C/cal");
         coeffTable.Add(FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, 1.0/5.678263);       unitStringTable.Add(FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, "ft2.h.°F/Btu");
         coeffTable.Add(FoulingFactorUnitType.SquareFootSecFahrenheitPerBtu, 1.0/2.044175e4);      unitStringTable.Add(FoulingFactorUnitType.SquareFootSecFahrenheitPerBtu, "ft2.s.°F/Btu");
         coeffTable.Add(FoulingFactorUnitType.SquareFootHourRankinePerBtu, 1.0/5.678263);          unitStringTable.Add(FoulingFactorUnitType.SquareFootHourRankinePerBtu, "ft2.h.°R/Btu");
      }
   }
}
