using System;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for TubeHTCAndDPCalculator.
   /// </summary>
   public class TwoPhasePressureDropCalculator 
   {

      public static double CalculateCondensingInTubeDp_Lockhart_Martinelli(double massFlowRateLiq, double massFlowRateVap, double diameter, double densityLiq, double densityVap, double viscosityLiq, double viscosityVap, double dpLiq) 
      {
         double x = massFlowRateVap/(massFlowRateLiq + massFlowRateVap);
         double Xtt = Math.Pow((1.0-x)/x, 0.9)*Math.Pow(densityVap/densityLiq, 0.5)*Math.Pow(viscosityLiq/viscosityVap, 0.1);
         double c = 8.0;
         double Rel = 4.0*massFlowRateLiq/(Math.PI*diameter*viscosityLiq);
         double Rev = 4.0*massFlowRateVap/(Math.PI*diameter*viscosityVap);
         if (Rel > 1000 && Rev > 1000) 
         {
            c = 20.0;
         }
         else if (Rel <= 1000 && Rev > 1000) 
         {
            c = 12.0;
         }
         else if (Rel > 1000 && Rev <= 1000) 
         {
            c = 10.0;
         }
         else if (Rel <= 1000 && Rev <= 1000) 
         {
            c = 5.0;
         }

         double dp = dpLiq * (1.0 + c/Xtt + 1.0/(Xtt*Xtt));
         return dp;
      }

      public static double CalculateCondensingInShellDp_Lockhart_Martinelli(double massFlowRateLiq, double massFlowRateVap, double diameter, double densityLiq, double densityVap, double viscosityLiq, double viscosityVap, double dpLiq) 
      {
         double x = massFlowRateVap/(massFlowRateLiq + massFlowRateVap);
         double Xtt = Math.Pow((1.0-x)/x, 0.9)*Math.Pow(densityVap/densityLiq, 0.5)*Math.Pow(viscosityLiq/viscosityVap, 0.1);
         double c = 8.0;
         double dp = dpLiq * (1.0 + c/Xtt + 1.0/(Xtt*Xtt));
         return dp;
      }
   }
}
