using System;
using Prosimo.SubstanceLibrary;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for MoistureProperties.
   /// </summary>
   public class GasProperties  {
      //air properties------------
      //Perry's
      //private double[] gasCpC = {0.2896e5, 0.0939e5, 3.012e3, 0.0758e5, 1484.0};
      //private double[] liqCpC = {-2.1446e5, 9.1851e3, -1.0612e2, 4.1616e-1, 0};
      //private double molWt = 28.951;
      private double[] gasCpCoeffs;
      private double[] liqCpCoeffs;
      private double molarWeight;
      //air properties------------

      public GasProperties(Substance gas) {
         gasCpCoeffs = gas.ThermalPropsAndCoeffs.GasCpCoeffs;
         liqCpCoeffs = gas.ThermalPropsAndCoeffs.LiqCpCoeffs;
         molarWeight = gas.MolarWeight;
      }

      public double GetSpecificHeatOfDryGas(double temperature) {
         double cp = ThermalPropCalculator.CalculateGasHeatCapacity1(temperature, gasCpCoeffs); 
         return cp/molarWeight;
      }

      public double GetMeanSpecificHeatOfDryGas(double t1, double t2) {
         double cp;
         if (Math.Abs(t1 - t2) > 1.0e-4) {
            cp = ThermalPropCalculator.CalculateMeanGasHeatCapacity1(t1, t2, gasCpCoeffs); 
         }
         else {
            cp = ThermalPropCalculator.CalculateGasHeatCapacity1(t1, gasCpCoeffs); 
         }
         return cp/molarWeight;
      }
      
      public double GetSpecificHeatOfDryGas() {
         return 1010.0;
      }
      
      //public double GetMolarWeight() {
      //   return molWt;
      //}
   }
}