using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum HeatFluxUnitType {WattPerSquareMeter = 0, KilowattPerSquareMeter, 
      BtuPerSecSquareFoot, BtuPerHourSquareFoot, CaloryPerSecSquareCentimeter};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class HeatFluxUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static HeatFluxUnit() {
         coeffTable.Add(HeatFluxUnitType.WattPerSquareMeter, 1.0);               unitStringTable.Add(HeatFluxUnitType.WattPerSquareMeter, "W/m2");
         coeffTable.Add(HeatFluxUnitType.KilowattPerSquareMeter, 1000.0);        unitStringTable.Add(HeatFluxUnitType.KilowattPerSquareMeter, "kW/m2");
         coeffTable.Add(HeatFluxUnitType.BtuPerSecSquareFoot, 1.135e4);          unitStringTable.Add(HeatFluxUnitType.BtuPerSecSquareFoot, "Btu/s.ft2");
         coeffTable.Add(HeatFluxUnitType.BtuPerHourSquareFoot, 3.152);           unitStringTable.Add(HeatFluxUnitType.BtuPerHourSquareFoot, "Btu/hr.ft2");
         coeffTable.Add(HeatFluxUnitType.CaloryPerSecSquareCentimeter, 4.184e4); unitStringTable.Add(HeatFluxUnitType.CaloryPerSecSquareCentimeter, "cal/s.cm2");
      }

      public static double ConvertToSIValue(HeatFluxUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(HeatFluxUnitType unitType, double wattPerSqMeterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return wattPerSqMeterValue/convertionCoeff;
      }
         
      public static string GetUnitAsString(HeatFluxUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static HeatFluxUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         HeatFluxUnitType type = HeatFluxUnitType.WattPerSquareMeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (HeatFluxUnitType) myEnumerator.Key;
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
