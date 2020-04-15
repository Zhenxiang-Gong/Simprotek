using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum PowerUnitType {Watt = 0, Milliwatt, Kilowatt, Megawatt, Gigawatt, Terawatt, 
      JoulePerSec, KiloJoulePerMin, MegaJoulePerHour, BtuPerSec, BtuPerMin, BtuPerHour, 
      CaloryPerSec, CaloryPerMin, KilocaloryPerSec, KilocaloryPerMin, KilocaloryPerHour,
      KgfMeterPerSec, FootIbfPerSec, FootIbfPerMin, ThermPerHour, HorsepowerElectric, 
      Horsepower, HydraulicHorsepower, ChOrCV, MillionBtuPerHour, TonOfRefrigeration};
   
   /// <summary>
	/// Summary description for PowerUnit.
	/// </summary>
   public class PowerUnit : EngineeringUnit {
      public static PowerUnit Instance = new PowerUnit();

      public PowerUnit() {
         coeffTable.Add(PowerUnitType.Watt, 1.0);                      unitStringTable.Add(PowerUnitType.Watt, "W");
         coeffTable.Add(PowerUnitType.Milliwatt, 1.0e-3);              unitStringTable.Add(PowerUnitType.Milliwatt, "mW");
         coeffTable.Add(PowerUnitType.Kilowatt, 1.0e3);                unitStringTable.Add(PowerUnitType.Kilowatt, "kW");
         coeffTable.Add(PowerUnitType.Megawatt, 1.0e6);                unitStringTable.Add(PowerUnitType.Megawatt, "MW");
         coeffTable.Add(PowerUnitType.Gigawatt, 1.0e9);                unitStringTable.Add(PowerUnitType.Gigawatt, "GW");
         coeffTable.Add(PowerUnitType.Terawatt, 1.0e12);               unitStringTable.Add(PowerUnitType.Terawatt, "TW");
         coeffTable.Add(PowerUnitType.JoulePerSec, 1.0);               unitStringTable.Add(PowerUnitType.JoulePerSec, "J/s");
         coeffTable.Add(PowerUnitType.KiloJoulePerMin, 1000.0/60.0);   unitStringTable.Add(PowerUnitType.KiloJoulePerMin, "kJ/min");
         coeffTable.Add(PowerUnitType.MegaJoulePerHour, 1.0e6/3600.0);   unitStringTable.Add(PowerUnitType.MegaJoulePerHour, "MJ/h");
         coeffTable.Add(PowerUnitType.BtuPerSec, 1055.05583);          unitStringTable.Add(PowerUnitType.BtuPerSec, "Btu/s");
         coeffTable.Add(PowerUnitType.BtuPerMin, 17.5842638);          unitStringTable.Add(PowerUnitType.BtuPerMin, "Btu/min");
         coeffTable.Add(PowerUnitType.BtuPerHour, 0.293071063);        unitStringTable.Add(PowerUnitType.BtuPerHour, "Btu/h");
         coeffTable.Add(PowerUnitType.CaloryPerSec, 4.184);            unitStringTable.Add(PowerUnitType.CaloryPerSec, "cal/s");
         coeffTable.Add(PowerUnitType.CaloryPerMin, 0.06973333);       unitStringTable.Add(PowerUnitType.CaloryPerMin, "cal/min");
         coeffTable.Add(PowerUnitType.KilocaloryPerSec, 4184.0);       unitStringTable.Add(PowerUnitType.KilocaloryPerSec, "kcal/s");
         coeffTable.Add(PowerUnitType.KilocaloryPerMin, 69.73333);     unitStringTable.Add(PowerUnitType.KilocaloryPerMin, "kcal/min");
         coeffTable.Add(PowerUnitType.KilocaloryPerHour, 1.162222);    unitStringTable.Add(PowerUnitType.KilocaloryPerHour, "kcal/h");
         coeffTable.Add(PowerUnitType.KgfMeterPerSec, 9.80665);        unitStringTable.Add(PowerUnitType.KgfMeterPerSec, "kgf.m/s");
         coeffTable.Add(PowerUnitType.FootIbfPerSec, 1.35581795);      unitStringTable.Add(PowerUnitType.FootIbfPerSec, "ft.Ibf/s");
         coeffTable.Add(PowerUnitType.FootIbfPerMin, 0.0225969658);    unitStringTable.Add(PowerUnitType.FootIbfPerMin, "ft.Ibf/min");
         coeffTable.Add(PowerUnitType.ThermPerHour, 29307.1063);       unitStringTable.Add(PowerUnitType.ThermPerHour, "therm/h");
         coeffTable.Add(PowerUnitType.HorsepowerElectric, 746.0);        unitStringTable.Add(PowerUnitType.HorsepowerElectric, "hp (electric)");
         coeffTable.Add(PowerUnitType.Horsepower, 745.6999);           unitStringTable.Add(PowerUnitType.Horsepower, "hp");
         coeffTable.Add(PowerUnitType.HydraulicHorsepower, 746.043);   unitStringTable.Add(PowerUnitType.HydraulicHorsepower, "hhp");
         coeffTable.Add(PowerUnitType.ChOrCV, 735.4999);               unitStringTable.Add(PowerUnitType.ChOrCV, "ch or CV");
         coeffTable.Add(PowerUnitType.MillionBtuPerHour, 2.930711e5);  unitStringTable.Add(PowerUnitType.MillionBtuPerHour, "million Btu/h");
         coeffTable.Add(PowerUnitType.TonOfRefrigeration, 3.516853e3); unitStringTable.Add(PowerUnitType.TonOfRefrigeration, "ton of refrigeration");
      }
   }
}
