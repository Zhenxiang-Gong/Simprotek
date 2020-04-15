using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum DiffusivityUnitType {SquareMeterPerSec = 0, SquareMeterPerHour, 
      SquareCentimeterPerSec, SquareMillimeterPerSec, 
      SquareFootPerSec, SquareFootPerHour, SquareInchPerSec};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class DiffusivityUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static DiffusivityUnit() {
         coeffTable.Add(DiffusivityUnitType.SquareMeterPerSec, 1.0);         unitStringTable.Add(DiffusivityUnitType.SquareMeterPerSec, "m2/s");
         coeffTable.Add(DiffusivityUnitType.SquareMeterPerHour, 1.0/3600.0); unitStringTable.Add(DiffusivityUnitType.SquareMeterPerHour, "m2/hr");
         coeffTable.Add(DiffusivityUnitType.SquareCentimeterPerSec, 1.0e-4); unitStringTable.Add(DiffusivityUnitType.SquareCentimeterPerSec, "cm2/s");
         coeffTable.Add(DiffusivityUnitType.SquareMillimeterPerSec, 1.0e-6); unitStringTable.Add(DiffusivityUnitType.SquareMillimeterPerSec, "mm2/s");
         coeffTable.Add(DiffusivityUnitType.SquareFootPerSec, 9.290304e-2);  unitStringTable.Add(DiffusivityUnitType.SquareFootPerSec, "ft2/s");
         coeffTable.Add(DiffusivityUnitType.SquareFootPerHour, 2.58064e-5);  unitStringTable.Add(DiffusivityUnitType.SquareFootPerHour, "ft2/hr");
         coeffTable.Add(DiffusivityUnitType.SquareInchPerSec, 6.4516e-4);    unitStringTable.Add(DiffusivityUnitType.SquareInchPerSec, "in2/s");
      }

      public static double ConvertToSIValue(DiffusivityUnitType unitType, double toBeConvertedValue) {
         double conversionCoeff = (double) coeffTable[unitType];
         return conversionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(DiffusivityUnitType unitType, double sqMeterPerSecValue) {
         double conversionCoeff = (double) coeffTable[unitType];
         return sqMeterPerSecValue/conversionCoeff;
      }
      
      public static string GetUnitAsString(DiffusivityUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
      
      public static DiffusivityUnitType GetUnitAsEnum (string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         DiffusivityUnitType type = DiffusivityUnitType.SquareMeterPerSec;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (DiffusivityUnitType) myEnumerator.Key;
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
