using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum AreaUnitType {SquareMeter = 0, SquareMillimeter, SquareCentimeter, 
      SquareKilometer, SquareInch, SquareFoot, SquareYard, SquareMile, 
      Are, Hectare, Barn, CircularMil, CircularInch, Acre, Section};

	/// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class AreaUnit : EngineeringUnit {
      public static AreaUnit Instance = new AreaUnit();

      public AreaUnit() {
         coeffTable.Add(AreaUnitType.SquareMeter, 1.0);               unitStringTable.Add(AreaUnitType.SquareMeter, "m2");
         coeffTable.Add(AreaUnitType.SquareMillimeter, 1.0e-6);       unitStringTable.Add(AreaUnitType.SquareMillimeter, "mm2");
         coeffTable.Add(AreaUnitType.SquareCentimeter, 1.0e-4);       unitStringTable.Add(AreaUnitType.SquareCentimeter, "cm2");
         coeffTable.Add(AreaUnitType.SquareKilometer, 1.0e6);         unitStringTable.Add(AreaUnitType.SquareKilometer, "km2");
         coeffTable.Add(AreaUnitType.SquareInch, 6.4516e-4);          unitStringTable.Add(AreaUnitType.SquareInch, "in2");
         coeffTable.Add(AreaUnitType.SquareFoot, 9.290304e-2);        unitStringTable.Add(AreaUnitType.SquareFoot, "ft2");
         coeffTable.Add(AreaUnitType.SquareYard, 0.83612736);         unitStringTable.Add(AreaUnitType.SquareYard, "yd2");
         coeffTable.Add(AreaUnitType.SquareMile, 2589988.110336);     unitStringTable.Add(AreaUnitType.SquareMile, "mi2");
         coeffTable.Add(AreaUnitType.Are, 100.0);                     unitStringTable.Add(AreaUnitType.Are, "are");
         coeffTable.Add(AreaUnitType.Hectare, 1.0e4);                 unitStringTable.Add(AreaUnitType.Hectare, "hectare");
         coeffTable.Add(AreaUnitType.Barn, 1.0e-28);                  unitStringTable.Add(AreaUnitType.Barn, "barn");
         coeffTable.Add(AreaUnitType.CircularMil, 5.0670747884e-10);  unitStringTable.Add(AreaUnitType.CircularMil, "circular mil");
         coeffTable.Add(AreaUnitType.CircularInch, 5.0670747884e-4);  unitStringTable.Add(AreaUnitType.CircularInch, "circular in");
         coeffTable.Add(AreaUnitType.Acre, 4046.85642);               unitStringTable.Add(AreaUnitType.Acre, "acre");
         coeffTable.Add(AreaUnitType.Section, 2589988.110336);        unitStringTable.Add(AreaUnitType.Section, "section");
      }
   }
}