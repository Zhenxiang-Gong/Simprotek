using System;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for TubeHTCAndDPCalculator.
   /// </summary>
//   public class TubeHTCAndDPCalculator {
//
//      public static double CalculateSinglePhaseHTC(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity, 
//                  double thermalCond, double specificHeat) {
//         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
//         double Re = diameter*massVelocity/bulkViscosity;
//
//         double h = 0;
//         if (Re <= 2100) {
//            h = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeLaminarHTC_Sieder_Tate(Re, diameter, length, bulkViscosity, wallViscosity, 
//                     thermalCond, specificHeat);
//         }
//         else if (Re > 2100 && Re < 10000) {
//            //h = SinglePhaseHeatTransferCoeffCalculator.CalculateTransitionHTC_Hausen(Re, diameter, length, bulkViscosity, wallViscosity, 
//            //         thermalCond, specificHeat);
//            double h1 = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeLaminarHTC_Sieder_Tate(2100, diameter, length, bulkViscosity, wallViscosity, 
//                    thermalCond, specificHeat);
//            double h2 = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeTurbulentHTC_Colburn(10000, diameter, bulkViscosity, wallViscosity, 
//                     thermalCond, specificHeat);
//            h = (h2 - h1)/(10000 - 2100) * (Re - 2100);
//         }
//         else if (Re >= 10000) {
//            h = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeTurbulentHTC_Colburn(Re, diameter, bulkViscosity, wallViscosity, 
//                     thermalCond, specificHeat);
//         }
//         return h;
//      }
//
//      public static double CalculateInTubeHorizontalCondensingHTC(double massFlowRate, double diameter, double length, double liqDensity, double vapDensity, double liqViscosity, double liqThermalCond, double liqSpecificHeat, double inVapQuality, double outVapQuality) {
//         double h = CondensationHeatTransferCoeffCalculator.CalculateInTubeHorizontalHTC_StratifiedFlow(massFlowRate, diameter, length, liqDensity, liqViscosity, liqThermalCond);
//         return h;
//      }
//      
//      public static double CalculateInTubeVerticalCondensingHTC(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double vapViscosity, double liqThermalCond, double liqSpecificHeat) {
//         double h = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, diameter, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat) ;
//         return h;
//      }
//
//      public static double CalculateInTubeNucleateBoilingHTC(double heatFlux, double pressure, double criticalPressure) {
//         double h = BoilingHeatTransferCoeffCalculator.CalculateNucleateBoilingHTC_Mostinski(heatFlux, pressure, criticalPressure);
//         return h;
//      }
//
//      public static double CalculateInTubeFallingFilmBoilingHTC(double outDiameter, double vapThermalCond, double liqDensity, double vapDensity, double vapViscosity, double wallBulkTempDiff) {
//         double h = BoilingHeatTransferCoeffCalculator.CalculateFilmBoilingHTC(outDiameter, vapThermalCond, liqDensity, vapDensity, vapViscosity, wallBulkTempDiff);
//         return h;
//      }
//      
//      public static double CalculateSinglePhaseDp(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity) {
//         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
//         double Re = diameter*massVelocity/bulkViscosity;
//         double f = FrictionFactorCalculator.CalculateFrictionFactor(Re);
//         
//         double velocity = massVelocity/density;
//         double dp = 2.0*f*length*density*velocity*velocity/(diameter*Math.Pow(bulkViscosity/wallViscosity, 0.14));
//         return dp;
//      }
//   }
}
