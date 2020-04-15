using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum FractionUnitType {Percent = 0, Decimal};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class FractionUnit : EngineeringUnit {
      public static FractionUnit Instance = new FractionUnit();

      public FractionUnit() {
         coeffTable.Add(FractionUnitType.Percent, 0.01); unitStringTable.Add(FractionUnitType.Percent, "%");
         coeffTable.Add(FractionUnitType.Decimal, 1.0);  unitStringTable.Add(FractionUnitType.Decimal, "");
      }
   }
}
