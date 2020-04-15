using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum AmountOfSubstanceUnitType {Mole = 0, Kmole, IbmMole, StdCubicMeter, StdCubicFeet};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class AmountOfSubstanceUnit : EngineeringUnit {

      public static AmountOfSubstanceUnit Instance = new AmountOfSubstanceUnit();

      private AmountOfSubstanceUnit() {
         coeffTable.Add(AmountOfSubstanceUnitType.Mole, 1.0);              unitStringTable.Add(AmountOfSubstanceUnitType.Mole, "mole");
         coeffTable.Add(AmountOfSubstanceUnitType.Kmole, 1.0e3);           unitStringTable.Add(AmountOfSubstanceUnitType.Kmole, "kmole");
         coeffTable.Add(AmountOfSubstanceUnitType.IbmMole, 453.5924);      unitStringTable.Add(AmountOfSubstanceUnitType.IbmMole, "Ibm.mole");
         coeffTable.Add(AmountOfSubstanceUnitType.StdCubicMeter, 44.6158); unitStringTable.Add(AmountOfSubstanceUnitType.StdCubicMeter, "std m3");
         coeffTable.Add(AmountOfSubstanceUnitType.StdCubicFeet, 1.1953);   unitStringTable.Add(AmountOfSubstanceUnitType.StdCubicFeet, "std ft3");
      }
   }
}
