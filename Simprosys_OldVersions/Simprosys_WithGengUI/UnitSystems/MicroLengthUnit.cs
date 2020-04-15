using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MicroLengthUnitType {Micrometer = 0, Angstrom, Nanometer, Millimeter, Centimeter, 
      Decimeter, Inch};

   /// <summary>
	/// Summary description .
	/// </summary>
   public class MicroLengthUnit : EngineeringUnit {
      public static MicroLengthUnit Instance = new MicroLengthUnit();

      public MicroLengthUnit() {
         coeffTable.Add(MicroLengthUnitType.Angstrom, 1.0e-10);   unitStringTable.Add(MicroLengthUnitType.Angstrom, "angstrom");
         coeffTable.Add(MicroLengthUnitType.Nanometer, 1.0e-9);   unitStringTable.Add(MicroLengthUnitType.Nanometer, "nanometer");
         coeffTable.Add(MicroLengthUnitType.Micrometer, 1.0e-6);  unitStringTable.Add(MicroLengthUnitType.Micrometer, "µm");
         coeffTable.Add(MicroLengthUnitType.Millimeter, 1.0e-3);  unitStringTable.Add(MicroLengthUnitType.Millimeter, "mm");
         coeffTable.Add(MicroLengthUnitType.Centimeter, 0.01);    unitStringTable.Add(MicroLengthUnitType.Centimeter, "cm");
         coeffTable.Add(MicroLengthUnitType.Decimeter, 0.1);      unitStringTable.Add(MicroLengthUnitType.Decimeter, "dm");
         coeffTable.Add(MicroLengthUnitType.Inch, 0.0254);        unitStringTable.Add(MicroLengthUnitType.Inch, "in");
      }
   }
}
