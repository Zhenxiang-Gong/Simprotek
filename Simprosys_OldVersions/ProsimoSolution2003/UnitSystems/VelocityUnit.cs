using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VelocityUnitType {MeterPerSec = 0, MeterPerMin, MeterPerHour, 
      MeterPerDay, MillimeterPerSec, CentimeterPerSec, CentimeterPerMin,
      KilometerPerSec, KilometerPerMin, KilometerPerHour, KilometerPerDay, 
      InchPerSec, InchPerMin, InchPerHour, FootPerSec, FootPerMin, FootPerHour, 
      FootPerDay, MilePerSec, MilePerMin, MilePerHour, MilePerDay, Knot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class VelocityUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VelocityUnit() {
         coeffTable.Add(VelocityUnitType.MeterPerSec, 1.0);             unitStringTable.Add(VelocityUnitType.MeterPerSec, "m/s");
         coeffTable.Add(VelocityUnitType.MeterPerMin, 1.0/60.0);        unitStringTable.Add(VelocityUnitType.MeterPerMin, "m/min");
         coeffTable.Add(VelocityUnitType.MeterPerHour, 1.0/3600.0);     unitStringTable.Add(VelocityUnitType.MeterPerHour, "m/hr");
         coeffTable.Add(VelocityUnitType.MeterPerDay, 1.0/86400.0);     unitStringTable.Add(VelocityUnitType.MeterPerDay, "m/day");
         coeffTable.Add(VelocityUnitType.MillimeterPerSec, 1.0e-3);     unitStringTable.Add(VelocityUnitType.MillimeterPerSec, "mm/s");
         coeffTable.Add(VelocityUnitType.CentimeterPerSec, 1.0e-2);     unitStringTable.Add(VelocityUnitType.CentimeterPerSec, "cm/s");
         coeffTable.Add(VelocityUnitType.CentimeterPerMin, 1.0/6000.0); unitStringTable.Add(VelocityUnitType.CentimeterPerMin, "cm/min");
         coeffTable.Add(VelocityUnitType.KilometerPerSec, 1.0e3);       unitStringTable.Add(VelocityUnitType.KilometerPerSec, "km/s");
         coeffTable.Add(VelocityUnitType.KilometerPerMin, 1000.0/60.0); unitStringTable.Add(VelocityUnitType.KilometerPerMin, "km/min");
         coeffTable.Add(VelocityUnitType.KilometerPerHour, 1.0/3.6);    unitStringTable.Add(VelocityUnitType.KilometerPerHour, "km/hr");
         coeffTable.Add(VelocityUnitType.KilometerPerDay, 1.0/86.4);    unitStringTable.Add(VelocityUnitType.KilometerPerDay, "km/day");
         coeffTable.Add(VelocityUnitType.InchPerSec, 0.0254);           unitStringTable.Add(VelocityUnitType.InchPerSec, "in/s");
         coeffTable.Add(VelocityUnitType.InchPerMin, 4.23333333e-4);    unitStringTable.Add(VelocityUnitType.InchPerMin, "in/min");
         coeffTable.Add(VelocityUnitType.InchPerHour, 7.05555556e-6);   unitStringTable.Add(VelocityUnitType.InchPerHour, "in/hr");
         coeffTable.Add(VelocityUnitType.FootPerSec, 0.3048);           unitStringTable.Add(VelocityUnitType.FootPerSec, "ft/s");
         coeffTable.Add(VelocityUnitType.FootPerMin, 0.00508);          unitStringTable.Add(VelocityUnitType.FootPerMin, "ft/min");
         coeffTable.Add(VelocityUnitType.FootPerHour, 8.46666667E-5);   unitStringTable.Add(VelocityUnitType.FootPerHour, "ft/hr");
         coeffTable.Add(VelocityUnitType.FootPerDay, 3.52777778E-6);    unitStringTable.Add(VelocityUnitType.FootPerDay, "ft/day");
         coeffTable.Add(VelocityUnitType.MilePerSec, 1609.344);         unitStringTable.Add(VelocityUnitType.MilePerSec, "mi/s");
         coeffTable.Add(VelocityUnitType.MilePerMin, 26.8224);          unitStringTable.Add(VelocityUnitType.MilePerMin, "mi/min");
         coeffTable.Add(VelocityUnitType.MilePerHour, 0.44704);         unitStringTable.Add(VelocityUnitType.MilePerHour, "mi/hr");
         coeffTable.Add(VelocityUnitType.MilePerDay, 0.0186266667);     unitStringTable.Add(VelocityUnitType.MilePerDay, "mi/day");
         coeffTable.Add(VelocityUnitType.Knot, 0.514444444);            unitStringTable.Add(VelocityUnitType.Knot, "knot");
      }

      public static double ConvertToSIValue(VelocityUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VelocityUnitType unitType, double meterPerSecValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return meterPerSecValue/convertionCoeff;
      }

      public static string GetUnitAsString(VelocityUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VelocityUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VelocityUnitType type = VelocityUnitType.MeterPerSec;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VelocityUnitType) myEnumerator.Key;
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
