using System;

namespace Prosimo.UnitOperations.HeatTransfer {
   //public enum HeatTransferType {SinglePhase, Boiling, Condensing};
   //public enum Orientation {Horizontal, Vertical};
   /// <summary>
   /// Summary description for FrictionFactorCalculator.
   /// </summary>
   public class FrictionFactorCalculator {

      public static double CalculateFrictionFactor(double Re) {
         double f = 0.0;
         if (Re <= 2100) {
            f = CalculateLaminarFlowFactor(Re);
         }
         //else if (Re > 4000 && Re < 100000) 
         else if (Re > 4000) {
            f = CalculateTurbulentFlowFactor(Re);
         }
         else if (Re > 2100 && Re <= 4000) {
            double f1 = CalculateLaminarFlowFactor(2100);
            double f2 = CalculateTurbulentFlowFactor(4000);
            f = f1 + (f2 - f1)*(Re-2100)/(4000 - 2100);
         }

         return f;
      }

      public static double CalculateFrictionFactor(double Re, double pipeRoughnessToDiameterRatio) {
         double f = 0.0;
         if (Re <= 2100) {
            f = CalculateLaminarFlowFactor(Re);
         }
         else if (Re > 4000 && Re < 100000) {
            f = CalculateTurbulentFlowFactor(Re, pipeRoughnessToDiameterRatio);
         }
         else if (Re > 2100 && Re <= 4000) {
            double f1 = CalculateLaminarFlowFactor(2100);
            double f2 = CalculateTurbulentFlowFactor(4000, pipeRoughnessToDiameterRatio);
            f = f1 + (f2 - f1)*(Re-2100)/(4000 - 2100);
         }

         return f;
      }
      
      private static double CalculateLaminarFlowFactor(double Re) {
         double f = 0.0;
         //if (Re < 2100) {
            f = 16.0/Re;
         //}

         return f;
      }
      
      private static double CalculateTurbulentFlowFactor(double Re) {
         double f = 0;
         //if (Re > 4000) {
            f = 0.079/(Math.Pow(Re, 0.25));
         //}
         
         return f;
      }

      private static double CalculateTurbulentFlowFactor(double Re, double pipeRoughnessToDiameterRatio) {
         double f = 0;
         //if (Re > 4000) {
            double temp = -4.0*Math.Log10(0.27*pipeRoughnessToDiameterRatio + Math.Pow(7.0/Re, 0.9));
            temp = (1/temp);
            f = temp * temp;
         //}
         
         return f;
      }
   }
}
