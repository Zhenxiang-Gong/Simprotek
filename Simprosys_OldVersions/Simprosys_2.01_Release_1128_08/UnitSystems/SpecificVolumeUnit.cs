using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SpecificVolumeUnitType {CubicMeterPerKg = 0, CubicMeterPerGram, CubicMeterPerTonne,
      CubicDecimeterPerKg, CubicCentimeterPerKg, MillilitrePerGram, LitrePerKg, 
      LitrePerGram, CubicInchPerOunce, CubicInchPerPound, CubicFootPerPound,
      CubicYardPerTonUK, CubicYardPerTonUS, GallonUKPerOunce, GallonUKPerPound,
      GallonUSPerOunce, GallonUSPerPound};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SpecificVolumeUnit : EngineeringUnit {
      public static SpecificVolumeUnit Instance = new SpecificVolumeUnit();

      private SpecificVolumeUnit() {
         coeffTable.Add(SpecificVolumeUnitType.CubicMeterPerKg, 1.0);              unitStringTable.Add(SpecificVolumeUnitType.CubicMeterPerKg, "m3/kg");
         coeffTable.Add(SpecificVolumeUnitType.CubicMeterPerGram, 1000.0);         unitStringTable.Add(SpecificVolumeUnitType.CubicMeterPerGram, "m3/g");
         coeffTable.Add(SpecificVolumeUnitType.CubicMeterPerTonne, 1.0e-3);        unitStringTable.Add(SpecificVolumeUnitType.CubicMeterPerTonne, "m3/tonne");
         coeffTable.Add(SpecificVolumeUnitType.CubicDecimeterPerKg, 1.0e-3);       unitStringTable.Add(SpecificVolumeUnitType.CubicDecimeterPerKg, "dm3/kg");
         coeffTable.Add(SpecificVolumeUnitType.CubicCentimeterPerKg, 1.0e-6);      unitStringTable.Add(SpecificVolumeUnitType.CubicCentimeterPerKg, "cm3/kg");
         coeffTable.Add(SpecificVolumeUnitType.MillilitrePerGram, 1.0e-3);         unitStringTable.Add(SpecificVolumeUnitType.MillilitrePerGram, "mL/g");
         coeffTable.Add(SpecificVolumeUnitType.LitrePerKg, 1.0e-3);                unitStringTable.Add(SpecificVolumeUnitType.LitrePerKg, "L/kg");
         coeffTable.Add(SpecificVolumeUnitType.LitrePerGram, 1.0);                 unitStringTable.Add(SpecificVolumeUnitType.LitrePerGram, "L/g");
         coeffTable.Add(SpecificVolumeUnitType.CubicInchPerOunce, 1.0/1729.99404); unitStringTable.Add(SpecificVolumeUnitType.CubicInchPerOunce, "in3/oz");
         coeffTable.Add(SpecificVolumeUnitType.CubicInchPerPound, 1.0/27679.9047); unitStringTable.Add(SpecificVolumeUnitType.CubicInchPerPound, "in3/Ib");
         coeffTable.Add(SpecificVolumeUnitType.CubicFootPerPound, 1.0/16.0184634); unitStringTable.Add(SpecificVolumeUnitType.CubicFootPerPound, "ft3/Ib");
         coeffTable.Add(SpecificVolumeUnitType.CubicYardPerTonUK, 1.0/1328.93918); unitStringTable.Add(SpecificVolumeUnitType.CubicYardPerTonUK, "yd3/UK ton");
         coeffTable.Add(SpecificVolumeUnitType.CubicYardPerTonUS, 1.0/1186.55284); unitStringTable.Add(SpecificVolumeUnitType.CubicYardPerTonUS, "yd3/US ton");
         coeffTable.Add(SpecificVolumeUnitType.GallonUKPerOunce, 1.0/6.23602329);  unitStringTable.Add(SpecificVolumeUnitType.GallonUKPerOunce, "UK gal/oz");
         coeffTable.Add(SpecificVolumeUnitType.GallonUKPerPound, 1.0/99.7763288);  unitStringTable.Add(SpecificVolumeUnitType.GallonUKPerPound, "UK gal/Ib");
         coeffTable.Add(SpecificVolumeUnitType.GallonUSPerOunce, 1.0/7.48915171);  unitStringTable.Add(SpecificVolumeUnitType.GallonUSPerOunce, "US gal/oz");
         coeffTable.Add(SpecificVolumeUnitType.GallonUSPerPound, 1.0/119.826427);  unitStringTable.Add(SpecificVolumeUnitType.GallonUSPerPound, "US gal/Ib");
      }
   }
}
