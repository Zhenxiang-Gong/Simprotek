using System;

namespace Prosimo.UnitOperations.HeatTransfer {
   //public enum HeatTransferType {SinglePhase, Boiling, Condensing};
   //public enum Orientation {Horizontal, Vertical};
   /// <summary>
   /// Summary description for PlateHTCAndDPCalculator.
   /// </summary>
   public class PlateHTCAndDPCalculator {

      public static double CalculateSinglePhaseHTC(double Re, double De, double bulkViscosity, double wallViscosity, 
                  double thermalCond, double specificHeat) {
         //Eq. 5.15
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.37*Math.Pow(Re, 0.67)*Math.Pow(Pr, 0.33);
         return Nut*thermalCond/De;
      }

      public static double CalculateSinglePhaseDP(double Re, double massVelocity, double De, double density, double bulkViscosity) {
         //Eq.5.16
         double f = 2.50 * Math.Pow(Re, -0.3);
         
         double velocity = massVelocity/density;
         double dp = f/De*density*velocity*velocity/2;
         return dp;
      }
   }
}
