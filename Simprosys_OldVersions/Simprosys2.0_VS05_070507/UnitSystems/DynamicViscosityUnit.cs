using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum DynamicViscosityUnitType {PascalSecond = 0, Poiseuille, Poise, Centipoise, 
      KgPerMeterHour, KgfSecPerSquareMeter, GramfSecPerSquareCentimeter, 
      DyneSecPerSquareCentimeter, IbfSecPerSquareInch, IbfSecPerSquareFoot, 
      IbfHourPerSquareFoot, PoundPerFootSec, PoundPerFootHour, PoundalSecPerSquareFoot,
      SlugPerFootSec, SlugHourPerFootSquareSec}; 

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class DynamicViscosityUnit : EngineeringUnit {
      public static DynamicViscosityUnit Instance = new DynamicViscosityUnit();

      public DynamicViscosityUnit() {
         coeffTable.Add(DynamicViscosityUnitType.PascalSecond, 1.0);                     unitStringTable.Add(DynamicViscosityUnitType.PascalSecond, "Pa.s");
         coeffTable.Add(DynamicViscosityUnitType.Poiseuille, 1.0);                       unitStringTable.Add(DynamicViscosityUnitType.Poiseuille, "poiseuille");
         coeffTable.Add(DynamicViscosityUnitType.Poise, 0.1);                            unitStringTable.Add(DynamicViscosityUnitType.Poise, "poise");
         coeffTable.Add(DynamicViscosityUnitType.Centipoise, 0.001);                     unitStringTable.Add(DynamicViscosityUnitType.Centipoise, "cP");
         coeffTable.Add(DynamicViscosityUnitType.KgPerMeterHour, 2.77777778e-4);         unitStringTable.Add(DynamicViscosityUnitType.KgPerMeterHour, "kg/m.h");
         coeffTable.Add(DynamicViscosityUnitType.KgfSecPerSquareMeter, 9.80665);         unitStringTable.Add(DynamicViscosityUnitType.KgfSecPerSquareMeter, "kgf.s/m2");
         coeffTable.Add(DynamicViscosityUnitType.GramfSecPerSquareCentimeter,  98.0665); unitStringTable.Add(DynamicViscosityUnitType.GramfSecPerSquareCentimeter, "gf.s/cm2");
         coeffTable.Add(DynamicViscosityUnitType.DyneSecPerSquareCentimeter, 0.1);       unitStringTable.Add(DynamicViscosityUnitType.DyneSecPerSquareCentimeter, "dyne.s/cm2");
         coeffTable.Add(DynamicViscosityUnitType.IbfSecPerSquareInch, 6894.757);         unitStringTable.Add(DynamicViscosityUnitType.IbfSecPerSquareInch, "Ibf.s/in2");
         coeffTable.Add(DynamicViscosityUnitType.IbfSecPerSquareFoot, 47.88026);         unitStringTable.Add(DynamicViscosityUnitType.IbfSecPerSquareFoot, "Ibf.s/ft2");
         coeffTable.Add(DynamicViscosityUnitType.IbfHourPerSquareFoot, 172369.0);        unitStringTable.Add(DynamicViscosityUnitType.IbfHourPerSquareFoot, "Ibf.h/ft2");
         coeffTable.Add(DynamicViscosityUnitType.PoundPerFootSec, 1.488164);             unitStringTable.Add(DynamicViscosityUnitType.PoundPerFootSec, "Ib/ft.s");
         coeffTable.Add(DynamicViscosityUnitType.PoundPerFootHour, 4.133789e-4);         unitStringTable.Add(DynamicViscosityUnitType.PoundPerFootHour, "Ib/ft.h");
         coeffTable.Add(DynamicViscosityUnitType.PoundalSecPerSquareFoot, 1.48816);      unitStringTable.Add(DynamicViscosityUnitType.PoundalSecPerSquareFoot, "poundal.s/ft2");
         coeffTable.Add(DynamicViscosityUnitType.SlugPerFootSec, 47.8803);               unitStringTable.Add(DynamicViscosityUnitType.SlugPerFootSec, "slug/ft.s");
         coeffTable.Add(DynamicViscosityUnitType.SlugHourPerFootSquareSec, 172369);      unitStringTable.Add(DynamicViscosityUnitType.SlugHourPerFootSquareSec, "slug.h/ft.s2");
      }
   }
}
