using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum PressureUnitType {Pascal = 0, Kilopascal, Megapascal, Millibar, Bar, Microbar,
      NewtonPerSquareCentimeter, NewtonPerSquareMeter, KilonewtonPerSquareMeter, 
      Atmosphere, MeterOfMercury, MillimeterOfMercury, MicrometerOfMercury, MillimeterOfWater, 
      CentimeterOfWater, InchOfWater, InchOfMercury, FootOfWater, 
      DynePerSquareCentimeter, KgfPerSquareCentimeter, TonnefPerSquareMeter, 
      IbfPerSquareInch, IbfPerSquareFoot, TonfUKPerSquareFoot, TonfUSPerSquareFoot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class PressureUnit : EngineeringUnit {
      public static PressureUnit Instance = new PressureUnit();

      public PressureUnit() {
         coeffTable.Add(PressureUnitType.Pascal, 1.0);                       unitStringTable.Add(PressureUnitType.Pascal, "Pa");
         coeffTable.Add(PressureUnitType.Kilopascal, 1.0e3);                 unitStringTable.Add(PressureUnitType.Kilopascal, "kPa");
         coeffTable.Add(PressureUnitType.Megapascal, 1.0e6);                 unitStringTable.Add(PressureUnitType.Megapascal, "MPa");
         coeffTable.Add(PressureUnitType.Millibar, 100.0);                   unitStringTable.Add(PressureUnitType.Millibar, "hPa");
         coeffTable.Add(PressureUnitType.Bar, 1.0e5);                        unitStringTable.Add(PressureUnitType.Bar,"bar");
         coeffTable.Add(PressureUnitType.Microbar, 0.1);                     unitStringTable.Add(PressureUnitType.Microbar, "µPa");
         coeffTable.Add(PressureUnitType.NewtonPerSquareCentimeter, 1.0e4);  unitStringTable.Add(PressureUnitType.NewtonPerSquareCentimeter, "N/cm2");
         coeffTable.Add(PressureUnitType.NewtonPerSquareMeter, 1.0);         unitStringTable.Add(PressureUnitType.NewtonPerSquareMeter, "N/m2");
         coeffTable.Add(PressureUnitType.KilonewtonPerSquareMeter, 1000.0);  unitStringTable.Add(PressureUnitType.KilonewtonPerSquareMeter, "kN/m2");
         coeffTable.Add(PressureUnitType.Atmosphere, 1.01325e5);             unitStringTable.Add(PressureUnitType.Atmosphere, "atm");
         coeffTable.Add(PressureUnitType.MeterOfMercury, 1.333223684e5);     unitStringTable.Add(PressureUnitType.MeterOfMercury, "mHg");
         coeffTable.Add(PressureUnitType.MillimeterOfMercury, 133.3223684);  unitStringTable.Add(PressureUnitType.MillimeterOfMercury, "mmHg"); //0 °C Hg
         coeffTable.Add(PressureUnitType.MicrometerOfMercury, 0.1333223684); unitStringTable.Add(PressureUnitType.MicrometerOfMercury, "µmHg");
         coeffTable.Add(PressureUnitType.MillimeterOfWater, 9.80638);        unitStringTable.Add(PressureUnitType.MillimeterOfWater, "mmH2O"); //4 °C water
         coeffTable.Add(PressureUnitType.CentimeterOfWater, 98.0638);        unitStringTable.Add(PressureUnitType.CentimeterOfWater, "cmH2O");
         coeffTable.Add(PressureUnitType.InchOfWater, 249.082052);           unitStringTable.Add(PressureUnitType.InchOfWater, "inH2O");
         coeffTable.Add(PressureUnitType.InchOfMercury, 3386.38864);         unitStringTable.Add(PressureUnitType.InchOfMercury, "inHg");
         coeffTable.Add(PressureUnitType.FootOfWater, 2988.984624);          unitStringTable.Add(PressureUnitType.FootOfWater, "ftH2O");
         coeffTable.Add(PressureUnitType.DynePerSquareCentimeter, 0.1);      unitStringTable.Add(PressureUnitType.DynePerSquareCentimeter, "dyne/cm2");
         coeffTable.Add(PressureUnitType.KgfPerSquareCentimeter, 98066.5);   unitStringTable.Add(PressureUnitType.KgfPerSquareCentimeter, "Ibf/cm2");
         coeffTable.Add(PressureUnitType.TonnefPerSquareMeter, 9806.65);     unitStringTable.Add(PressureUnitType.TonnefPerSquareMeter, "tonnef/m2");
         coeffTable.Add(PressureUnitType.IbfPerSquareInch, 6894.75729);      unitStringTable.Add(PressureUnitType.IbfPerSquareInch, "psi");
         coeffTable.Add(PressureUnitType.IbfPerSquareFoot, 47.880259);       unitStringTable.Add(PressureUnitType.IbfPerSquareFoot, "Ibf/ft2");
         coeffTable.Add(PressureUnitType.TonfUKPerSquareFoot, 107251.78);    unitStringTable.Add(PressureUnitType.TonfUKPerSquareFoot, "UK tonf/ft2");
         coeffTable.Add(PressureUnitType.TonfUSPerSquareFoot, 95760.918);    unitStringTable.Add(PressureUnitType.TonfUSPerSquareFoot, "US tonf/ft2");
      }                                                       
   }
}
