using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VolumeFlowRateUnitType {CubicMeterPerSec = 0, CubicMeterPerMin, CubicMeterPerHour,
      CubicDecimeterPerSec, LitrePerSec, LitrePerMin, LitrePerHour, LitrePerDay,  
      CubicMeterPerDay, BblPerHour, BblPerDay, CubicInchPerSec, CubicInchPerMin, 
      CubicFootPerSec, CubicFootPerMin, CubicFootPerHour, CubicFootPerDay, 
      CubicYardPerHour, CubicYardPerDay, GallonUKPerMin,  GallonUKPerHour, 
      GallonUKPerDay, GallonUSPerMin, GallonUSPerHour, GallonUSPerDay, 
      BarrelPerMin, BarrelPerHour, BarrelPerDay};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class VolumeFlowRateUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VolumeFlowRateUnit() {
         coeffTable.Add(VolumeFlowRateUnitType.CubicMeterPerSec, 1.0);           unitStringTable.Add(VolumeFlowRateUnitType.CubicMeterPerSec, "m3/s");
         coeffTable.Add(VolumeFlowRateUnitType.CubicMeterPerMin, 1.0/60.0);      unitStringTable.Add(VolumeFlowRateUnitType.CubicMeterPerMin, "m3/min");
         coeffTable.Add(VolumeFlowRateUnitType.CubicMeterPerHour, 1.0/3600.0);   unitStringTable.Add(VolumeFlowRateUnitType.CubicMeterPerHour, "m3/h");
         //1 day = 86400 sec
         coeffTable.Add(VolumeFlowRateUnitType.CubicMeterPerDay, 1.0/86400.0);   unitStringTable.Add(VolumeFlowRateUnitType.CubicMeterPerDay, "m3/day");
         coeffTable.Add(VolumeFlowRateUnitType.CubicDecimeterPerSec, 1.0e-3);    unitStringTable.Add(VolumeFlowRateUnitType.CubicDecimeterPerSec, "dm3/s");
         coeffTable.Add(VolumeFlowRateUnitType.LitrePerSec, 1.0e-3);             unitStringTable.Add(VolumeFlowRateUnitType.LitrePerSec, "L/s");
         coeffTable.Add(VolumeFlowRateUnitType.LitrePerMin, 1.0/6.0e4);          unitStringTable.Add(VolumeFlowRateUnitType.LitrePerMin, "L/min");
         coeffTable.Add(VolumeFlowRateUnitType.LitrePerHour, 1.0/3.6e6);         unitStringTable.Add(VolumeFlowRateUnitType.LitrePerHour, "L/h");
         coeffTable.Add(VolumeFlowRateUnitType.LitrePerDay, 1.0/8.64e7);         unitStringTable.Add(VolumeFlowRateUnitType.LitrePerDay, "L/day");
         coeffTable.Add(VolumeFlowRateUnitType.BblPerHour, 4.416314e-5);         unitStringTable.Add(VolumeFlowRateUnitType.BblPerHour, "bbl/h");
         coeffTable.Add(VolumeFlowRateUnitType.BblPerDay, 1.8401308e-6);         unitStringTable.Add(VolumeFlowRateUnitType.BblPerDay, "bbl/day");
         coeffTable.Add(VolumeFlowRateUnitType.CubicInchPerSec, 1.6387064e-5);   unitStringTable.Add(VolumeFlowRateUnitType.CubicInchPerSec, "in3/s");
         coeffTable.Add(VolumeFlowRateUnitType.CubicInchPerMin, 2.73117733e-7);  unitStringTable.Add(VolumeFlowRateUnitType.CubicInchPerMin, "in3/min");
         coeffTable.Add(VolumeFlowRateUnitType.CubicFootPerSec, 2.83168466e-2);  unitStringTable.Add(VolumeFlowRateUnitType.CubicFootPerSec, "ft3/s");
         coeffTable.Add(VolumeFlowRateUnitType.CubicFootPerMin, 4.71947443e-4);  unitStringTable.Add(VolumeFlowRateUnitType.CubicFootPerMin, "ft3/min");
         coeffTable.Add(VolumeFlowRateUnitType.CubicFootPerHour, 7.86579072e-6); unitStringTable.Add(VolumeFlowRateUnitType.CubicFootPerHour, "ft3/h");
         coeffTable.Add(VolumeFlowRateUnitType.CubicFootPerDay, 1.31096512e-7);  unitStringTable.Add(VolumeFlowRateUnitType.CubicFootPerDay, "ft3/day");
         coeffTable.Add(VolumeFlowRateUnitType.CubicYardPerHour, 2.12376349e-4); unitStringTable.Add(VolumeFlowRateUnitType.CubicYardPerHour, "yd3/h");
         coeffTable.Add(VolumeFlowRateUnitType.CubicYardPerDay, 8.84901456e-6);  unitStringTable.Add(VolumeFlowRateUnitType.CubicYardPerDay, "yd3/day");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUKPerMin, 7.57681667e-5);   unitStringTable.Add(VolumeFlowRateUnitType.GallonUKPerMin, "UK gal/min");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUKPerHour, 1.26280278e-6);  unitStringTable.Add(VolumeFlowRateUnitType.GallonUKPerHour, "UK gal/h");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUKPerDay, 5.26167824e-8);   unitStringTable.Add(VolumeFlowRateUnitType.GallonUKPerDay, "UK gal/day");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUSPerMin, 6.30901964e-5);   unitStringTable.Add(VolumeFlowRateUnitType.GallonUSPerMin, "US gal/min");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUSPerHour, 1.05150327e-6);  unitStringTable.Add(VolumeFlowRateUnitType.GallonUSPerHour, "US gal/h");
         coeffTable.Add(VolumeFlowRateUnitType.GallonUSPerDay, 4.38126384e-8);   unitStringTable.Add(VolumeFlowRateUnitType.GallonUSPerDay, "US gal/day");
         coeffTable.Add(VolumeFlowRateUnitType.BarrelPerMin, 0.00264978825);     unitStringTable.Add(VolumeFlowRateUnitType.BarrelPerMin, "barrel/min");
         coeffTable.Add(VolumeFlowRateUnitType.BarrelPerHour, 4.41631375e-5);    unitStringTable.Add(VolumeFlowRateUnitType.BarrelPerHour, "barrel/h");
         coeffTable.Add(VolumeFlowRateUnitType.BarrelPerDay, 1.84013073e-6);     unitStringTable.Add(VolumeFlowRateUnitType.BarrelPerDay, "barrel/day");
      }

      public static double ConvertToSIValue(VolumeFlowRateUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VolumeFlowRateUnitType unitType, double cubicMeterPerSecValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return cubicMeterPerSecValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(VolumeFlowRateUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VolumeFlowRateUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VolumeFlowRateUnitType type = VolumeFlowRateUnitType.CubicMeterPerSec;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VolumeFlowRateUnitType) myEnumerator.Key;
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
