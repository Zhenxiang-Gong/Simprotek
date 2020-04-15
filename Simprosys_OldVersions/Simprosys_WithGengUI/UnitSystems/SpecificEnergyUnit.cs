using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SpecificEnergyUnitType {JoulePerKg = 0, KilojoulePerKg, MegajoulePerKg, JoulePerGram,
      KilowattHourPerKg, KilowattHourPerPound, KilojoulePerPound, CaloryPerGram, 
      KilocaloryPerKg, CaloryPerPound, KilocaloryPerPound, BtuPerKg, BtuPerPound, 
      ErgPerGram, ThermPerPound, KgfMeterPerKg, FootIbfPerPound, HorsepowerHourPerPound}; 
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SpecificEnergyUnit : EngineeringUnit {
      public static SpecificEnergyUnit Instance = new SpecificEnergyUnit();

      public SpecificEnergyUnit()  {
         coeffTable.Add(SpecificEnergyUnitType.JoulePerKg, 1.0);                      unitStringTable.Add(SpecificEnergyUnitType.JoulePerKg, "J/kg");
         coeffTable.Add(SpecificEnergyUnitType.KilojoulePerKg, 1.0e3);                unitStringTable.Add(SpecificEnergyUnitType.KilojoulePerKg, "kJ/kg");
         coeffTable.Add(SpecificEnergyUnitType.MegajoulePerKg, 1.0e6);                unitStringTable.Add(SpecificEnergyUnitType.MegajoulePerKg, "MJ/kg");
         coeffTable.Add(SpecificEnergyUnitType.JoulePerGram, 1.0e3);                  unitStringTable.Add(SpecificEnergyUnitType.JoulePerGram, "J/g");
         coeffTable.Add(SpecificEnergyUnitType.KilowattHourPerKg, 3.6e6);             unitStringTable.Add(SpecificEnergyUnitType.KilowattHourPerKg, "kWh/kg");
         coeffTable.Add(SpecificEnergyUnitType.KilowattHourPerPound, 7936648.0);      unitStringTable.Add(SpecificEnergyUnitType.KilowattHourPerPound, "kWh/Ib");
         coeffTable.Add(SpecificEnergyUnitType.KilojoulePerPound, 2204.62262);        unitStringTable.Add(SpecificEnergyUnitType.KilojoulePerPound, "kJ/Ib");
         coeffTable.Add(SpecificEnergyUnitType.CaloryPerGram, 4184.0);                unitStringTable.Add(SpecificEnergyUnitType.CaloryPerGram, "cal/g");
         coeffTable.Add(SpecificEnergyUnitType.KilocaloryPerKg, 4184.0);              unitStringTable.Add(SpecificEnergyUnitType.KilocaloryPerKg, "kcal/kg");
         coeffTable.Add(SpecificEnergyUnitType.CaloryPerPound, 9.224141);             unitStringTable.Add(SpecificEnergyUnitType.CaloryPerPound, "cal/Ib");
         coeffTable.Add(SpecificEnergyUnitType.KilocaloryPerPound, 9224.141);         unitStringTable.Add(SpecificEnergyUnitType.KilocaloryPerPound, "kcal/Ib");
         coeffTable.Add(SpecificEnergyUnitType.BtuPerPound, 2326.0);                  unitStringTable.Add(SpecificEnergyUnitType.BtuPerPound, "Btu/Ib");
         coeffTable.Add(SpecificEnergyUnitType.BtuPerKg, 1055.05585);                 unitStringTable.Add(SpecificEnergyUnitType.BtuPerKg, "Btu/kg");
         coeffTable.Add(SpecificEnergyUnitType.ErgPerGram, 0.0001);                   unitStringTable.Add(SpecificEnergyUnitType.ErgPerGram, "erg/g");
         coeffTable.Add(SpecificEnergyUnitType.ThermPerPound, 2.326e8);               unitStringTable.Add(SpecificEnergyUnitType.ThermPerPound, "therm/Ib");
         coeffTable.Add(SpecificEnergyUnitType.KgfMeterPerKg, 9.80665);               unitStringTable.Add(SpecificEnergyUnitType.KgfMeterPerKg, "kgf.m/kg");
         coeffTable.Add(SpecificEnergyUnitType.FootIbfPerPound, 2.98907);             unitStringTable.Add(SpecificEnergyUnitType.FootIbfPerPound, "ft.Ibf/Ib");
         coeffTable.Add(SpecificEnergyUnitType.HorsepowerHourPerPound, 5918352.3357); unitStringTable.Add(SpecificEnergyUnitType.HorsepowerHourPerPound, "hp.hr/Ib");
      }
   }
}
