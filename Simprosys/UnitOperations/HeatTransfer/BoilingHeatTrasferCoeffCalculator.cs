using System;

namespace Prosimo.UnitOperations.HeatTransfer {

   public enum SurfaceType {Steel, Copper, StainlessSteel, PolishedSurface} 
   /// <summary>
   /// Summary description for BoilingHeatTransferCoeffCalculator.
   /// </summary>
   public class BoilingHeatTransferCoeffCalculator {

      //Perry's Equation 5-102
      public static double CalculateNucleateBoilingHTC_Mostinski(double heatFlux, double pressure, double criticalPressure) {
         double pr = pressure/criticalPressure;
         double h = 3.75e-5*Math.Pow(criticalPressure, 0.69) * Math.Pow(heatFlux, 0.7) * (1.8*Math.Pow(pr, 0.17) + 4*Math.Pow(pr, 1.2) + 10*Math.Pow(pr, 10));
         return h;
      }

      //Perry's Equation 5-103
      public static double CalculateNucleateBoilingHTC_McNelly(double heatFlux, double pressure, double latentHeat, double liqSpecificHeat, double liqThermalCond, double liqDensity, double vapDensity, double surfTension) {
         double h = 0.225 * Math.Pow(heatFlux*liqSpecificHeat/latentHeat, 0.69)*Math.Pow(pressure*liqThermalCond/surfTension, 0.31)*Math.Pow((liqDensity/vapDensity-1.0), 0.33);
         return h;
      }

      //Perry's Equation 5-110
      public static double CalculateFilmBoilingHTC(double outDiameter, double vapThermalCond, double liqDensity, double vapDensity, double vapViscosity, double wallBulkTempDiff) {
         double tempValue = vapThermalCond*vapThermalCond*vapThermalCond*(liqDensity-vapDensity)*vapDensity*9.8065/(vapViscosity*outDiameter*wallBulkTempDiff);
         double h = 4.306 * Math.Pow(tempValue, 0.25);
         return h;
      }

      //Handbook of Chemical Engineering Calculations 7-30
      //vaporFlowRate must be per tube based
      public static double CalculateNucleateBoilingHTC_Nusselt(double vaporFlowRate, double diameter, double length, double pressure, double liqSpecificHeat, double liqThermalCond, double liqDensity, double vapDensity, double liqViscosity, double surfTension, SurfaceType surfaceType) {
         double surfaceArea = Math.PI * diameter * length;
         double massVelocity = vaporFlowRate * liqDensity/(surfaceArea * vapDensity);
         double fy = 0.001;
         if (surfaceType == SurfaceType.Copper || surfaceType == SurfaceType.Steel) {
            fy = 0.001;
         }
         else if (surfaceType == SurfaceType.StainlessSteel) {
            fy = 0.006;
         }
         else if (surfaceType == SurfaceType.PolishedSurface) {
            fy = 0.004;
         }

         double h = fy * liqSpecificHeat * massVelocity/(Math.Pow(liqSpecificHeat*liqViscosity/liqThermalCond, 0.6)*Math.Pow(diameter*massVelocity/liqViscosity, 0.3)*Math.Pow(liqDensity*surfTension/(pressure*pressure), 0.425));
         return h;
      }

      //Perry's Figure 5-10, also see section 7-25 of Handbook of Chemical Engineering Calculations
      //Fist correlation to use to calculate condensing heat transfer coefficient in vertical tubes
      //This correlation can also be used for falling film heat transfer coeeficient. But the result
      //should be multiplied by 0.75
      public static double CalculateFallingFilmHTC_Dukler(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double vapViscosity, double liqThermalCond, double liqSpecificHeat) 
      {
         return 0.75 * DuklerHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, diameter, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
      }
   }
}                                                                  

