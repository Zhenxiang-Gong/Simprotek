using System;
using Prosimo.SubstanceLibrary;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for MoistureProperties.
   /// </summary>
   public class GasProperties {
      //air properties------------
      //Perry's
      //private double[] gasCpC = {0.2896e5, 0.0939e5, 3.012e3, 0.0758e5, 1484.0};
      //private double[] liqCpC = {-2.1446e5, 9.1851e3, -1.0612e2, 4.1616e-1, 0};
      //private double molWt = 28.951;
      //private double[] gasCpCoeffs;
      //private double[] liqCpCoeffs;
      private double molarWeight;
      //air properties------------
      Substance gas;

      public GasProperties(Substance gas) {
         //gasCpCoeffs = gas.ThermalPropsAndCoeffs.GasCpCoeffs;
         //liqCpCoeffs = gas.ThermalPropsAndCoeffs.LiqCpCoeffs;
         this.gas = gas;
         molarWeight = gas.MolarWeight;
      }

      public double GetSpecificHeatOfDryGas(double temperature) {
         double cp = ThermalPropCalculator.Instance.CalculateGasHeatCapacity(temperature, gas);
         //return cp / molarWeight;
         return cp;
      }

      public double GetMeanSpecificHeatOfDryGas(double t1, double t2) {
         double cp;
         if (Math.Abs(t1 - t2) > 1.0e-4) {
            cp = ThermalPropCalculator.Instance.CalculateGasMeanHeatCapacity(t1, t2, gas);
         }
         else {
            cp = ThermalPropCalculator.Instance.CalculateGasHeatCapacity(t1, gas);
         }
         //return cp / molarWeight;
         return cp;
      }

      public double GetThermalConductivityOfDryGas(double temperature) {
         double k = ThermalPropCalculator.Instance.CalculateGasThermalConductivity(temperature, gas);
         return k;
      }
      
      //public double GetSpecificHeatOfDryGas() {
      //   return 1010.0;
      //}

      //public double GetMolarWeight() {
      //   return molWt;
      //}
   }
}