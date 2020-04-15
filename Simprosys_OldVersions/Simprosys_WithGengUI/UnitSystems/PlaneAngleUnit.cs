using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum PlaneAngleUnitType {Degree = 0, Radian, Min, Sec};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class PlaneAngleUnit : EngineeringUnit {
      public static PlaneAngleUnit Instance = new PlaneAngleUnit();

      public PlaneAngleUnit() {
         coeffTable.Add(PlaneAngleUnitType.Radian, 1.0);         unitStringTable.Add(PlaneAngleUnitType.Radian, "rad");
         coeffTable.Add(PlaneAngleUnitType.Degree, 1.745329e-2); unitStringTable.Add(PlaneAngleUnitType.Degree, "°");
         coeffTable.Add(PlaneAngleUnitType.Min, 2.908882e-4);    unitStringTable.Add(PlaneAngleUnitType.Min, "min");
         coeffTable.Add(PlaneAngleUnitType.Sec, 4.848137e-6);    unitStringTable.Add(PlaneAngleUnitType.Sec, "sec");
      }
   }
}
