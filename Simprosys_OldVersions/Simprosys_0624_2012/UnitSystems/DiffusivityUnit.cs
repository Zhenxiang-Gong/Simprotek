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
   public class DiffusivityUnit : EngineeringUnit {
      public static DiffusivityUnit Instance = new DiffusivityUnit();

      private DiffusivityUnit() {
         coeffTable.Add(DiffusivityUnitType.SquareMeterPerSec, 1.0);         unitStringTable.Add(DiffusivityUnitType.SquareMeterPerSec, "m2/s");
         coeffTable.Add(DiffusivityUnitType.SquareMeterPerHour, 1.0/3600.0); unitStringTable.Add(DiffusivityUnitType.SquareMeterPerHour, "m2/h");
         coeffTable.Add(DiffusivityUnitType.SquareCentimeterPerSec, 1.0e-4); unitStringTable.Add(DiffusivityUnitType.SquareCentimeterPerSec, "cm2/s");
         coeffTable.Add(DiffusivityUnitType.SquareMillimeterPerSec, 1.0e-6); unitStringTable.Add(DiffusivityUnitType.SquareMillimeterPerSec, "mm2/s");
         coeffTable.Add(DiffusivityUnitType.SquareFootPerSec, 9.290304e-2);  unitStringTable.Add(DiffusivityUnitType.SquareFootPerSec, "ft2/s");
         coeffTable.Add(DiffusivityUnitType.SquareFootPerHour, 2.58064e-5);  unitStringTable.Add(DiffusivityUnitType.SquareFootPerHour, "ft2/h");
         coeffTable.Add(DiffusivityUnitType.SquareInchPerSec, 6.4516e-4);    unitStringTable.Add(DiffusivityUnitType.SquareInchPerSec, "in2/s");
      }
   }
}
