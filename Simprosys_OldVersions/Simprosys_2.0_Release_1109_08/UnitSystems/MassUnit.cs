using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MassUnitType {Kilogram = 0, Microgram, Milligram, Gram, Tonne, 
      Carat, TroyOunce, Grain, Slug, Ounce, Pound, Stone, Hundredweight, 
      TonUK, TonUS};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MassUnit : EngineeringUnit {
      public static MassUnit Instance = new MassUnit();

      private MassUnit() {
         coeffTable.Add(MassUnitType.Kilogram, 1.0);             unitStringTable.Add(MassUnitType.Kilogram, "kg");
         coeffTable.Add(MassUnitType.Microgram, 1.0e-9);         unitStringTable.Add(MassUnitType.Microgram, "�g");
         coeffTable.Add(MassUnitType.Milligram, 1.0e-6);         unitStringTable.Add(MassUnitType.Milligram, "mg");
         coeffTable.Add(MassUnitType.Gram, 1.0e-3);              unitStringTable.Add(MassUnitType.Gram, "g");
         coeffTable.Add(MassUnitType.Tonne, 1.0e3);              unitStringTable.Add(MassUnitType.Tonne, "tonne");
         coeffTable.Add(MassUnitType.Carat, 0.0002);             unitStringTable.Add(MassUnitType.Carat, "carat");
         coeffTable.Add(MassUnitType.TroyOunce, 0.0311034768);   unitStringTable.Add(MassUnitType.TroyOunce, "oz(troy)");
         coeffTable.Add(MassUnitType.Grain, 6.479891e-5);        unitStringTable.Add(MassUnitType.Grain, "grain");
         coeffTable.Add(MassUnitType.Slug, 14.5939029);          unitStringTable.Add(MassUnitType.Slug, "slug");
         coeffTable.Add(MassUnitType.Ounce, 0.0283495231);       unitStringTable.Add(MassUnitType.Ounce, "oz(av)");
         coeffTable.Add(MassUnitType.Pound, 0.45359237);         unitStringTable.Add(MassUnitType.Pound, "Ib");
         coeffTable.Add(MassUnitType.Stone, 6.35029318);         unitStringTable.Add(MassUnitType.Stone, "stone");
         coeffTable.Add(MassUnitType.Hundredweight, 50.8023454); unitStringTable.Add(MassUnitType.Hundredweight, "hundred wieght");
         coeffTable.Add(MassUnitType.TonUK, 1016.04691);         unitStringTable.Add(MassUnitType.TonUK, "UK ton");
         coeffTable.Add(MassUnitType.TonUS, 907.18474);          unitStringTable.Add(MassUnitType.TonUS, "US ton");
      }
   }
}
