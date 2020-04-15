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
   public class SpecificEnergyUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static SpecificEnergyUnit()  {
         coeffTable.Add(SpecificEnergyUnitType.JoulePerKg, 1.0);                      unitStringTable.Add(SpecificEnergyUnitType.JoulePerKg, "J/kg");
         coeffTable.Add(SpecificEnergyUnitType.KilojoulePerKg, 1.0e3);                unitStringTable.Add(SpecificEnergyUnitType.KilojoulePerKg, "kJ/kg");
         coeffTable.Add(SpecificEnergyUnitType.MegajoulePerKg, 1.0e6);                unitStringTable.Add(SpecificEnergyUnitType.MegajoulePerKg, "MJ/kg");
         coeffTable.Add(SpecificEnergyUnitType.JoulePerGram, 1.0e3);                  unitStringTable.Add(SpecificEnergyUnitType.JoulePerGram, "J/g");
         coeffTable.Add(SpecificEnergyUnitType.KilowattHourPerKg, 3.6e6);             unitStringTable.Add(SpecificEnergyUnitType.KilowattHourPerKg, "kW.hr/kg");
         coeffTable.Add(SpecificEnergyUnitType.KilowattHourPerPound, 7936648.0);      unitStringTable.Add(SpecificEnergyUnitType.KilowattHourPerPound, "kW.hr/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.KilojoulePerPound, 2204.62262);        unitStringTable.Add(SpecificEnergyUnitType.KilojoulePerPound, "kJ/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.CaloryPerGram, 4184.0);                unitStringTable.Add(SpecificEnergyUnitType.CaloryPerGram, "cal/g");
         coeffTable.Add(SpecificEnergyUnitType.KilocaloryPerKg, 4184.0);              unitStringTable.Add(SpecificEnergyUnitType.KilocaloryPerKg, "kcal/kg");
         coeffTable.Add(SpecificEnergyUnitType.CaloryPerPound, 9.224141);             unitStringTable.Add(SpecificEnergyUnitType.CaloryPerPound, "cal/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.KilocaloryPerPound, 9224.141);         unitStringTable.Add(SpecificEnergyUnitType.KilocaloryPerPound, "kcal/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.BtuPerPound, 2326.0);                  unitStringTable.Add(SpecificEnergyUnitType.BtuPerPound, "Btu/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.BtuPerKg, 1055.05585);                 unitStringTable.Add(SpecificEnergyUnitType.BtuPerKg, "Btu/kg");
         coeffTable.Add(SpecificEnergyUnitType.ErgPerGram, 0.0001);                   unitStringTable.Add(SpecificEnergyUnitType.ErgPerGram, "erg/g");
         coeffTable.Add(SpecificEnergyUnitType.ThermPerPound, 2.326e8);               unitStringTable.Add(SpecificEnergyUnitType.ThermPerPound, "therm/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.KgfMeterPerKg, 9.80665);               unitStringTable.Add(SpecificEnergyUnitType.KgfMeterPerKg, "kgf.m/kg");
         coeffTable.Add(SpecificEnergyUnitType.FootIbfPerPound, 2.98907);             unitStringTable.Add(SpecificEnergyUnitType.FootIbfPerPound, "ft.Ibf/Ibm");
         coeffTable.Add(SpecificEnergyUnitType.HorsepowerHourPerPound, 5918352.3357); unitStringTable.Add(SpecificEnergyUnitType.HorsepowerHourPerPound, "hp.hr/Ibm");
      }

      public static double ConvertToSIValue(SpecificEnergyUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(SpecificEnergyUnitType unitType, double joulePerKgValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return joulePerKgValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(SpecificEnergyUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static SpecificEnergyUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         SpecificEnergyUnitType type = SpecificEnergyUnitType.JoulePerKg;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (SpecificEnergyUnitType) myEnumerator.Key;
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
