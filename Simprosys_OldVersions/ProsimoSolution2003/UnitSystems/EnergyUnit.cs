using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum EnergyUnitType {Joule = 0, Kilojoule, Megajoule, Gigajoule, Calory, 
      Kilocalory, WattHour, KilowattHour, MegawattHour, Btu, Erg, 
      Therm, KgfMeter, FootIbf, FootPoundal, HorsepowerHour}; 
   
   /// <summary>
	/// Summary description for EnergyConverte.
	/// </summary>
   public class EnergyUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static EnergyUnit() {
         coeffTable.Add(EnergyUnitType.Joule, 1.0);                 unitStringTable.Add(EnergyUnitType.Joule, "J");
         coeffTable.Add(EnergyUnitType.Kilojoule, 1000.0);          unitStringTable.Add(EnergyUnitType.Kilojoule, "kJ");
         coeffTable.Add(EnergyUnitType.Megajoule, 1.0e6);           unitStringTable.Add(EnergyUnitType.Megajoule, "MJ");
         coeffTable.Add(EnergyUnitType.Gigajoule, 1.0e9);           unitStringTable.Add(EnergyUnitType.Gigajoule, "GJ");
         coeffTable.Add(EnergyUnitType.Calory, 4.184);              unitStringTable.Add(EnergyUnitType.Calory, "cal");
         coeffTable.Add(EnergyUnitType.Kilocalory, 4184.0);           unitStringTable.Add(EnergyUnitType.Kilocalory, "kcal");
         coeffTable.Add(EnergyUnitType.WattHour, 3600.0);             unitStringTable.Add(EnergyUnitType.WattHour, "W.hr");
         coeffTable.Add(EnergyUnitType.KilowattHour, 3.6e6);        unitStringTable.Add(EnergyUnitType.KilowattHour, "kW.hr");
         coeffTable.Add(EnergyUnitType.MegawattHour, 3.6e9);        unitStringTable.Add(EnergyUnitType.MegawattHour, "MW.hr");
         coeffTable.Add(EnergyUnitType.Btu, 1055.05585);            unitStringTable.Add(EnergyUnitType.Btu, "Btu");
         coeffTable.Add(EnergyUnitType.Erg, 1.0e-7);                unitStringTable.Add(EnergyUnitType.Erg, "erg");
         coeffTable.Add(EnergyUnitType.Therm, 105505585.0);           unitStringTable.Add(EnergyUnitType.Therm, "therm");
         coeffTable.Add(EnergyUnitType.KgfMeter, 9.80665);          unitStringTable.Add(EnergyUnitType.KgfMeter, "kgf.m");
         coeffTable.Add(EnergyUnitType.FootIbf, 1.35581795);        unitStringTable.Add(EnergyUnitType.FootIbf, "ft.Ibf");
         coeffTable.Add(EnergyUnitType.FootPoundal, 0.0421401101);  unitStringTable.Add(EnergyUnitType.FootPoundal, "ft.poundal");
         coeffTable.Add(EnergyUnitType.HorsepowerHour, 2684519.54); unitStringTable.Add(EnergyUnitType.HorsepowerHour, "hp.hr");
      }

      public static double ConvertToSIValue(EnergyUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(EnergyUnitType unitType, double jouleValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return jouleValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(EnergyUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static EnergyUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         EnergyUnitType type = EnergyUnitType.Joule;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (EnergyUnitType) myEnumerator.Key;
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
