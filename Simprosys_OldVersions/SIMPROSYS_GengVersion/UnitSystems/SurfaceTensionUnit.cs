using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SurfaceTensionUnitType {NewtonPerMeter = 0, DynePerCentimeter, 
      GramfPerCentimeter, KgfPerMeter, IbfPerFoot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SurfaceTensionUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static SurfaceTensionUnit() {
         coeffTable.Add(SurfaceTensionUnitType.NewtonPerMeter, 1.0);           unitStringTable.Add(SurfaceTensionUnitType.NewtonPerMeter, "N/m");
         coeffTable.Add(SurfaceTensionUnitType.DynePerCentimeter, 1.0e-3);     unitStringTable.Add(SurfaceTensionUnitType.DynePerCentimeter, "dyne/cm");
         coeffTable.Add(SurfaceTensionUnitType.GramfPerCentimeter, 0.980665);  unitStringTable.Add(SurfaceTensionUnitType.GramfPerCentimeter, "gf/cm");
         coeffTable.Add(SurfaceTensionUnitType.KgfPerMeter, 9.80665);          unitStringTable.Add(SurfaceTensionUnitType.KgfPerMeter, "kgf/m");
         coeffTable.Add(SurfaceTensionUnitType.IbfPerFoot, 14.592);            unitStringTable.Add(SurfaceTensionUnitType.IbfPerFoot, "Ibf/ft");
      }

      public static double ConvertToSIValue(SurfaceTensionUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(SurfaceTensionUnitType unitType, double newtonMeterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return newtonMeterValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(SurfaceTensionUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static SurfaceTensionUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         SurfaceTensionUnitType type = SurfaceTensionUnitType.NewtonPerMeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (SurfaceTensionUnitType) myEnumerator.Key;
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
