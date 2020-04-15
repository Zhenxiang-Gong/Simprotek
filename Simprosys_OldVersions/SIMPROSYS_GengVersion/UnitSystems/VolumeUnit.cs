using System; 
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VolumeUnitType {CubicMeter = 0, Millilitre, Centilitre, Decilitre, Litre, 
      FluidOunceUS, FluidOunceUK, PintLiquidUS, PintDryUS, PintUK, GallonLiquidUS, 
      GallonDryUS, GallonUK, BushelDryUS, CubicInch, CubicFoot, CubicYard, GillUK, 
      Barrel, Bbl, QuartUS, QuartUK, AcreFeet};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class VolumeUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VolumeUnit() {
         coeffTable.Add(VolumeUnitType.CubicMeter, 1.0);               unitStringTable.Add(VolumeUnitType.CubicMeter, "m3");
         coeffTable.Add(VolumeUnitType.Millilitre, 1.0e-6);            unitStringTable.Add(VolumeUnitType.Millilitre, "mL");
         coeffTable.Add(VolumeUnitType.Centilitre, 1.0e-5);            unitStringTable.Add(VolumeUnitType.Centilitre, "cL");
         coeffTable.Add(VolumeUnitType.Decilitre, 1.0e-4);             unitStringTable.Add(VolumeUnitType.Decilitre, "dL");
         coeffTable.Add(VolumeUnitType.Litre, 1.0e-3);                 unitStringTable.Add(VolumeUnitType.Litre, "L");
         coeffTable.Add(VolumeUnitType.FluidOunceUS, 2.95735296e-5);   unitStringTable.Add(VolumeUnitType.FluidOunceUS, "US fl oz");
         /*base on Perry's*/
         coeffTable.Add(VolumeUnitType.FluidOunceUK, 2.841307e-5);     unitStringTable.Add(VolumeUnitType.FluidOunceUK, "UK fl oz");
         coeffTable.Add(VolumeUnitType.PintLiquidUS, 4.731765e-4);     unitStringTable.Add(VolumeUnitType.PintLiquidUS, "US pt");
         coeffTable.Add(VolumeUnitType.PintDryUS, 5.50610471e-4);      unitStringTable.Add(VolumeUnitType.PintDryUS, "US pt(dry)");
         coeffTable.Add(VolumeUnitType.PintUK, 5.6826125e-4);          unitStringTable.Add(VolumeUnitType.PintUK, "UK pt");
         coeffTable.Add(VolumeUnitType.GallonLiquidUS, 3.78541178e-3); unitStringTable.Add(VolumeUnitType.GallonLiquidUS, "US gal");
         coeffTable.Add(VolumeUnitType.GallonDryUS, 4.40488377e-3);    unitStringTable.Add(VolumeUnitType.GallonDryUS, "US gal(dry)");
         coeffTable.Add(VolumeUnitType.GallonUK, 4.546092e-3);         unitStringTable.Add(VolumeUnitType.GallonUK, "UK gal");
         coeffTable.Add(VolumeUnitType.BushelDryUS, 3.52390702e-2);    unitStringTable.Add(VolumeUnitType.BushelDryUS, "US bushel");
         coeffTable.Add(VolumeUnitType.CubicInch, 1.6387064e-5);       unitStringTable.Add(VolumeUnitType.CubicInch, "in3");
         coeffTable.Add(VolumeUnitType.CubicFoot, 2.83168466e-2);      unitStringTable.Add(VolumeUnitType.CubicFoot, "ft3");
         coeffTable.Add(VolumeUnitType.CubicYard, 0.764554858);        unitStringTable.Add(VolumeUnitType.CubicYard, "yd3");
         coeffTable.Add(VolumeUnitType.GillUK, 1.42065312e-4);         unitStringTable.Add(VolumeUnitType.GillUK, "UK gill");
         coeffTable.Add(VolumeUnitType.Barrel, 0.158987295);           unitStringTable.Add(VolumeUnitType.Barrel, "barrel");
         coeffTable.Add(VolumeUnitType.Bbl, 1.589873e-1);              unitStringTable.Add(VolumeUnitType.Bbl, "bbl");
         coeffTable.Add(VolumeUnitType.QuartUS, 9.463529e-4);          unitStringTable.Add(VolumeUnitType.QuartUS, "US qt");
         coeffTable.Add(VolumeUnitType.QuartUK, 1.136523e-3);          unitStringTable.Add(VolumeUnitType.QuartUK, "UK qt");
         coeffTable.Add(VolumeUnitType.AcreFeet, 1.233482e3);          unitStringTable.Add(VolumeUnitType.AcreFeet, "acre-ft");
      }

      public static double ConvertToSIValue(VolumeUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VolumeUnitType unitType, double cuMeterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return cuMeterValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(VolumeUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VolumeUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VolumeUnitType type = VolumeUnitType.CubicMeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VolumeUnitType) myEnumerator.Key;
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
