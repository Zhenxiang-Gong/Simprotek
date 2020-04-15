using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum DensityUnitType {KgPerCubicMeter = 0, KgPerLitre, GramPerMillilitre,  
      GramPerLitre, GramPerCubicCentimeter, GramPerCubicMeter, OuncePerCubicInch,
      OuncePerGallonUK, OuncePerGallonUS, PoundPerCubicInch, PoundPerCubicFoot, 
      PoundPerGallonUK, PoundPerGallonUS, TonnePerCubicMeter, TonUKPerCubicYard, 
      TonUSPerCubicYard};
   

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class DensityUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();
      
      static DensityUnit() {
         coeffTable.Add(DensityUnitType.KgPerCubicMeter, 1.0);           unitStringTable.Add(DensityUnitType.KgPerCubicMeter, "kg/m3");
         coeffTable.Add(DensityUnitType.KgPerLitre, 1000.0);             unitStringTable.Add(DensityUnitType.KgPerLitre, "kg/L");
         coeffTable.Add(DensityUnitType.GramPerMillilitre, 1000.0);      unitStringTable.Add(DensityUnitType.GramPerMillilitre, "g/mL");
         coeffTable.Add(DensityUnitType.GramPerLitre, 1.0);              unitStringTable.Add(DensityUnitType.GramPerLitre, "g/L");
         coeffTable.Add(DensityUnitType.GramPerCubicCentimeter, 1000.0); unitStringTable.Add(DensityUnitType.GramPerCubicCentimeter, "g/cm3");
         coeffTable.Add(DensityUnitType.GramPerCubicMeter, 1.0e-3);      unitStringTable.Add(DensityUnitType.GramPerCubicMeter, "g/m3");
         coeffTable.Add(DensityUnitType.OuncePerCubicInch, 1729.99404);  unitStringTable.Add(DensityUnitType.OuncePerCubicInch, "oz/in3");
         coeffTable.Add(DensityUnitType.OuncePerGallonUK, 6.23602329);   unitStringTable.Add(DensityUnitType.OuncePerGallonUK, "oz/UK gal");
         coeffTable.Add(DensityUnitType.OuncePerGallonUS, 7.48915171);   unitStringTable.Add(DensityUnitType.OuncePerGallonUS, "oz/US gal");
         coeffTable.Add(DensityUnitType.PoundPerCubicInch, 27679.9047);  unitStringTable.Add(DensityUnitType.PoundPerCubicInch, "Ib/in3");
         coeffTable.Add(DensityUnitType.PoundPerCubicFoot, 16.0184634);  unitStringTable.Add(DensityUnitType.PoundPerCubicFoot, "Ib/ft3");
         coeffTable.Add(DensityUnitType.PoundPerGallonUK, 99.7763288);   unitStringTable.Add(DensityUnitType.PoundPerGallonUK, "Ib/UK gal");
         coeffTable.Add(DensityUnitType.PoundPerGallonUS, 119.826427);   unitStringTable.Add(DensityUnitType.PoundPerGallonUS, "Ib/US gal");
         coeffTable.Add(DensityUnitType.TonnePerCubicMeter, 1000.0);     unitStringTable.Add(DensityUnitType.TonnePerCubicMeter, "tonne/m3");
         coeffTable.Add(DensityUnitType.TonUKPerCubicYard, 1328.93918);  unitStringTable.Add(DensityUnitType.TonUKPerCubicYard, "UK ton/yd3");
         coeffTable.Add(DensityUnitType.TonUSPerCubicYard, 1186.55284);  unitStringTable.Add(DensityUnitType.TonUSPerCubicYard, "US ton/yd3");
      }

      public static double ConvertToSIValue(DensityUnitType unitType, double toBeConvertedValue) {
         double conversionCoeff = (double) coeffTable[unitType];
         return conversionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(DensityUnitType unitType, double kgPerCubicMeterValue) {
         double conversionCoeff = (double) coeffTable[unitType];
         return kgPerCubicMeterValue/conversionCoeff;
      }
      
      public static string GetUnitAsString(DensityUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
      
      public static DensityUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         DensityUnitType type = DensityUnitType.KgPerCubicMeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (DensityUnitType) myEnumerator.Key;
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
