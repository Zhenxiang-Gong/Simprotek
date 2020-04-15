using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum TimeUnitType {Second = 0, Minute, Hour, Day, Week, Year};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class TimeUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();
      
      static TimeUnit() {
         coeffTable.Add(TimeUnitType.Second, 1.0);    unitStringTable.Add(TimeUnitType.Second, "s");
         coeffTable.Add(TimeUnitType.Minute, 60.0);   unitStringTable.Add(TimeUnitType.Minute, "min");
         coeffTable.Add(TimeUnitType.Hour, 3600.0);   unitStringTable.Add(TimeUnitType.Hour, "h");
         coeffTable.Add(TimeUnitType.Day, 86400.0);   unitStringTable.Add(TimeUnitType.Day, "day");
         coeffTable.Add(TimeUnitType.Week, 604800.0); unitStringTable.Add(TimeUnitType.Week, "week");
         coeffTable.Add(TimeUnitType.Year, 3.1536e7); unitStringTable.Add(TimeUnitType.Year, "year");
      }

      public static double ConvertToSIValue(TimeUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(TimeUnitType unitType, double secValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return secValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(TimeUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static TimeUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         TimeUnitType type = TimeUnitType.Second;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (TimeUnitType) myEnumerator.Key;
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
