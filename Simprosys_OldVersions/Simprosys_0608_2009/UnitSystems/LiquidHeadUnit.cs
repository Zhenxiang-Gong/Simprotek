using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum LiquidHeadUnitType {Meter = 0, Millimeter, Centimeter, Inch, Foot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class LiquidHeadUnit : EngineeringUnit {
      public static LiquidHeadUnit Instance = new LiquidHeadUnit();

      private LiquidHeadUnit() {
         coeffTable.Add(LiquidHeadUnitType.Meter, 1.0);         unitStringTable.Add(LiquidHeadUnitType.Meter, "m");
         coeffTable.Add(LiquidHeadUnitType.Millimeter, 1.0e-3); unitStringTable.Add(LiquidHeadUnitType.Millimeter, "mm");
         coeffTable.Add(LiquidHeadUnitType.Centimeter, 0.01);   unitStringTable.Add(LiquidHeadUnitType.Centimeter, "cm");
         coeffTable.Add(LiquidHeadUnitType.Inch, 0.0254);       unitStringTable.Add(LiquidHeadUnitType.Inch, "in");
         coeffTable.Add(LiquidHeadUnitType.Foot, 0.3048);       unitStringTable.Add(LiquidHeadUnitType.Foot, "ft");
      }
   }
}
