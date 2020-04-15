using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SmallLengthUnitType {Meter = 0, Micrometer, Millimeter, Centimeter, Decimeter,
      Inch, Foot, Yard};

   /// <summary>
	/// Summary description .
	/// </summary>
   public class SmallLengthUnit : EngineeringUnit {
      public static SmallLengthUnit Instance = new SmallLengthUnit();

      private SmallLengthUnit() {
         coeffTable.Add(SmallLengthUnitType.Meter, 1.0);         unitStringTable.Add(SmallLengthUnitType.Meter, "m");
         coeffTable.Add(SmallLengthUnitType.Micrometer, 1.0e-6); unitStringTable.Add(SmallLengthUnitType.Micrometer, "µm");
         coeffTable.Add(SmallLengthUnitType.Millimeter, 1.0e-3); unitStringTable.Add(SmallLengthUnitType.Millimeter, "mm");
         coeffTable.Add(SmallLengthUnitType.Centimeter, 0.01);   unitStringTable.Add(SmallLengthUnitType.Centimeter, "cm");
         coeffTable.Add(SmallLengthUnitType.Decimeter, 0.1);     unitStringTable.Add(SmallLengthUnitType.Decimeter, "dm");
         coeffTable.Add(SmallLengthUnitType.Inch, 0.0254);       unitStringTable.Add(SmallLengthUnitType.Inch, "in");
         coeffTable.Add(SmallLengthUnitType.Foot, 0.3048);       unitStringTable.Add(SmallLengthUnitType.Foot, "ft");
         coeffTable.Add(SmallLengthUnitType.Yard, 0.9144);       unitStringTable.Add(SmallLengthUnitType.Yard, "yd");
      }
   }
}
