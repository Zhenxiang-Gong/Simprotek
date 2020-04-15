using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum VolumeRateFlowGasesUnitType {CubicMeterPerSec = 0, CubicDecimeterPerSec, LitrePerSec, 
      LitrePerMin, LitrePerHour, LitrePerDay, CubicMeterPerMin, CubicMeterPerHour, 
      CubicMeterPerDay, BblPerDay, CubicFootPerDay, BblPerHour, 
      CubicInchPerSec, CubicInchPerMin, CubicFootPerSec, CubicFootPerMin, 
      CubicFootPerHour, CubicYardPerHour, CubicYardPerDay, GallonUKPerMin, 
      GallonUKPerHour,  GallonUKPerDay, GallonUSPerMin, GallonUSPerHour, 
      GallonUSPerDay, BarrelPerMin, BarrelPerHour, BarrelPerDay};
   
   /// <summary>
	/// See Perry's 7th edition, Chapter 10 Transport and Storage of Fluid, page 10-3
	/// US Customary unit has liquid volume flow and gas volume flow different. This is
	/// why we have Volume Flow Rate and Volume Rate of Flow Gases and Volume Rate of Flow Liquids
	/// </summary>
   public class VolumeRateFlowGasesUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static VolumeRateFlowGasesUnit() {
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerSec, 1.0);           unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerSec, "m3/s");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerMin, 1.0/60.0);      unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerMin, "m3/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerHour, 1.0/3600.0);   unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerHour, "m3/h");
         //1 day = 86400 sec
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerDay, 1.0/86400.0);   unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicMeterPerDay, "m3/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicDecimeterPerSec, 1.0e-3);    unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicDecimeterPerSec, "dm3/s");
         coeffTable.Add(VolumeRateFlowGasesUnitType.LitrePerSec, 1.0e-3);             unitStringTable.Add(VolumeRateFlowGasesUnitType.LitrePerSec, "L/s");
         coeffTable.Add(VolumeRateFlowGasesUnitType.LitrePerMin, 1.0/6.0e4);          unitStringTable.Add(VolumeRateFlowGasesUnitType.LitrePerMin, "L/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.LitrePerHour, 1.0/3.6e6);         unitStringTable.Add(VolumeRateFlowGasesUnitType.LitrePerHour, "L/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.LitrePerDay, 1.0/8.64e7);         unitStringTable.Add(VolumeRateFlowGasesUnitType.LitrePerDay, "L/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.BblPerHour, 4.416314e-5);         unitStringTable.Add(VolumeRateFlowGasesUnitType.BblPerHour, "bbl/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.BblPerDay, 1.8401308e-6);         unitStringTable.Add(VolumeRateFlowGasesUnitType.BblPerDay, "bbl/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicInchPerSec, 1.6387064e-5);   unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicInchPerSec, "in3/s");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicInchPerMin, 2.73117733e-7);  unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicInchPerMin, "in3/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerSec, 2.83168466e-2);  unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerSec, "ft3/s");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerMin, 4.71947443e-4);  unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerMin, "ft3/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerHour, 7.86579072e-6); unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerHour, "ft3/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerDay, 1.31096512e-7);  unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicFootPerDay, "ft3/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicYardPerHour, 2.12376349e-4); unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicYardPerHour, "yd3/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.CubicYardPerDay, 8.84901456e-6);  unitStringTable.Add(VolumeRateFlowGasesUnitType.CubicYardPerDay, "yd3/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerMin, 7.57681667e-5);   unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerMin, "UK gal/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerHour, 1.26280278e-6);  unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerHour, "UK gal/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerDay, 5.26167824e-8);   unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUKPerDay, "UK gal/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerMin, 6.30901964e-5);   unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerMin, "US gal/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerHour, 1.05150327e-6);  unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerHour, "US gal/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerDay, 4.38126384e-8);   unitStringTable.Add(VolumeRateFlowGasesUnitType.GallonUSPerDay, "US gal/day");
         coeffTable.Add(VolumeRateFlowGasesUnitType.BarrelPerMin, 0.00264978825);     unitStringTable.Add(VolumeRateFlowGasesUnitType.BarrelPerMin, "barrel/min");
         coeffTable.Add(VolumeRateFlowGasesUnitType.BarrelPerHour, 4.41631375e-5);    unitStringTable.Add(VolumeRateFlowGasesUnitType.BarrelPerHour, "barrel/h");
         coeffTable.Add(VolumeRateFlowGasesUnitType.BarrelPerDay, 1.84013073e-6);     unitStringTable.Add(VolumeRateFlowGasesUnitType.BarrelPerDay, "barrel/day");
      }

      public static double ConvertToSIValue(VolumeRateFlowGasesUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(VolumeRateFlowGasesUnitType unitType, double cubicMeterPerSecValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return cubicMeterPerSecValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(VolumeRateFlowGasesUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static VolumeRateFlowGasesUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         VolumeRateFlowGasesUnitType type = VolumeRateFlowGasesUnitType.CubicMeterPerSec;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (VolumeRateFlowGasesUnitType) myEnumerator.Key;
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
