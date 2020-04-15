using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum KinematicViscosityUnitType {SquareMeterPerSec = 0, SquareMeterPerMin, 
      SquareMeterPerHour, Centistoke, Stoke, SquareCentimeterPerMin, 
      SquareCentimeterPerHour, SquareInchPerSec, SquareInchPerMin, SquareInchPerHour, 
      SquareFootPerSec, SquareFootPerMin, SquareFootPerHour, PoiseCubicFootPerPound};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class KinematicViscosityUnit : EngineeringUnit {
      public static KinematicViscosityUnit Instance = new KinematicViscosityUnit();

      public KinematicViscosityUnit() {
         coeffTable.Add(KinematicViscosityUnitType.SquareMeterPerSec, 1.0);                 unitStringTable.Add(KinematicViscosityUnitType.SquareMeterPerSec, "m2/s");
         coeffTable.Add(KinematicViscosityUnitType.SquareMeterPerMin, 1.0/60.0);            unitStringTable.Add(KinematicViscosityUnitType.SquareMeterPerMin, "m2/min");
         coeffTable.Add(KinematicViscosityUnitType.SquareMeterPerHour, 1.0/3600.0);         unitStringTable.Add(KinematicViscosityUnitType.SquareMeterPerHour, "m2/h");
         coeffTable.Add(KinematicViscosityUnitType.Centistoke, 1.0e-6);                     unitStringTable.Add(KinematicViscosityUnitType.Centistoke, "cSt");
         coeffTable.Add(KinematicViscosityUnitType.Stoke, 1.0e-4);                          unitStringTable.Add(KinematicViscosityUnitType.Stoke, "St");
         coeffTable.Add(KinematicViscosityUnitType.SquareCentimeterPerMin, 1.66666667e-6);  unitStringTable.Add(KinematicViscosityUnitType.SquareCentimeterPerMin, "cm2/min");
         coeffTable.Add(KinematicViscosityUnitType.SquareCentimeterPerHour, 2.77777778e-8); unitStringTable.Add(KinematicViscosityUnitType.SquareCentimeterPerHour, "cm2/h");
         coeffTable.Add(KinematicViscosityUnitType.SquareInchPerSec, 6.4516e-4);            unitStringTable.Add(KinematicViscosityUnitType.SquareInchPerSec, "in2/s");
         coeffTable.Add(KinematicViscosityUnitType.SquareInchPerMin, 1.07526667e-5);        unitStringTable.Add(KinematicViscosityUnitType.SquareInchPerMin, "in2/min");
         coeffTable.Add(KinematicViscosityUnitType.SquareInchPerHour, 1.79211111e-7);       unitStringTable.Add(KinematicViscosityUnitType.SquareInchPerHour, "in2/h");
         coeffTable.Add(KinematicViscosityUnitType.SquareFootPerSec, 0.09290304);           unitStringTable.Add(KinematicViscosityUnitType.SquareFootPerSec, "ft2/s");
         coeffTable.Add(KinematicViscosityUnitType.SquareFootPerMin, 0.001548384);          unitStringTable.Add(KinematicViscosityUnitType.SquareFootPerMin, "ft2/min");
         coeffTable.Add(KinematicViscosityUnitType.SquareFootPerHour, 2.58064e-5);          unitStringTable.Add(KinematicViscosityUnitType.SquareFootPerHour, "ft2/h");
         coeffTable.Add(KinematicViscosityUnitType.PoiseCubicFootPerPound, 0.00624279606);  unitStringTable.Add(KinematicViscosityUnitType.PoiseCubicFootPerPound, "poise.ft3/Ib");
      }
   }
}
