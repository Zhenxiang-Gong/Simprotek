using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VolumeRateFlowLiquidsUnitType {CubicMeterPerSec = 0, CubicDecimeterPerSec, LitrePerSec, 
      LitrePerMin, LitrePerHour, LitrePerDay, CubicMeterPerMin, CubicMeterPerHour, 
      CubicMeterPerDay, BblPerDay, CubicFootPerDay, BblPerHour, 
      CubicInchPerSec, CubicInchPerMin, CubicFootPerSec, CubicFootPerMin, 
      CubicFootPerHour, CubicYardPerHour, CubicYardPerDay, GallonUKPerMin,
      GallonUKPerHour, GallonUKPerDay, GallonUSPerMin, GallonUSPerHour, 
      GallonUSPerDay, BarrelPerMin, BarrelPerHour, BarrelPerDay};
   
   /// <summary>
   /// See Perry's 7th edition, Chapter 10 Transport and Storage of Fluid, page 10-3
   /// US Customary unit has liquid volume flow and gas volume flow different. This is
   /// why we have Volume Flow Rate and Volume Rate of Flow Gases and Volume Rate of Flow Liquids
   /// </summary>
   public class VolumeRateFlowLiquidsUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VolumeRateFlowLiquidsUnit() {
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerSec, 1.0);           unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerSec, "m3/s");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerMin, 1.0/60.0);      unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerMin, "m3/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, 1.0/3600.0);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, "m3/h");
         //1 day = 86400 sec                                                            
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerDay, 1.0/86400.0);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicMeterPerDay, "m3/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicDecimeterPerSec, 1.0e-3);    unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicDecimeterPerSec, "dm3/s");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerSec, 1.0e-3);             unitStringTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerSec, "L/s");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerMin, 1.0/6.0e4);          unitStringTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerMin, "L/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerHour, 1.0/3.6e6);         unitStringTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerHour, "L/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerDay, 1.0/8.64e7);         unitStringTable.Add(VolumeRateFlowLiquidsUnitType.LitrePerDay, "L/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.BblPerHour, 4.416314e-5);         unitStringTable.Add(VolumeRateFlowLiquidsUnitType.BblPerHour, "bbl/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.BblPerDay, 1.8401308e-6);         unitStringTable.Add(VolumeRateFlowLiquidsUnitType.BblPerDay, "bbl/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicInchPerSec, 1.6387064e-5);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicInchPerSec, "in3/s");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicInchPerMin, 2.73117733e-7);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicInchPerMin, "in3/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerSec, 2.83168466e-2);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerSec, "ft3/s");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerMin, 4.71947443e-4);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerMin, "ft3/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerHour, 7.86579072e-6); unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerHour, "ft3/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerDay, 1.31096512e-7);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicFootPerDay, "ft3/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicYardPerHour, 2.12376349e-4); unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicYardPerHour, "yd3/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.CubicYardPerDay, 8.84901456e-6);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.CubicYardPerDay, "yd3/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerMin, 7.57681667e-5);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerMin, "UK gal/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerHour, 1.26280278e-6);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerHour, "UK gal/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerDay, 5.26167824e-8);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUKPerDay, "UK gal/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerMin, 6.30901964e-5);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerMin, "US gal/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerHour, 1.05150327e-6);  unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerHour, "US gal/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerDay, 4.38126384e-8);   unitStringTable.Add(VolumeRateFlowLiquidsUnitType.GallonUSPerDay, "US gal/day");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerMin, 0.00264978825);     unitStringTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerMin, "barrel/min");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerHour, 4.41631375e-5);    unitStringTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerHour, "barrel/h");
         coeffTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerDay, 1.84013073e-6);     unitStringTable.Add(VolumeRateFlowLiquidsUnitType.BarrelPerDay, "barrel/day");
      }

      public static double ConvertToSIValue(VolumeRateFlowLiquidsUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VolumeRateFlowLiquidsUnitType unitType, double cubicMeterPerSecValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return cubicMeterPerSecValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(VolumeRateFlowLiquidsUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VolumeRateFlowLiquidsUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VolumeRateFlowLiquidsUnitType type = VolumeRateFlowLiquidsUnitType.CubicMeterPerSec;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VolumeRateFlowLiquidsUnitType) myEnumerator.Key;
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
