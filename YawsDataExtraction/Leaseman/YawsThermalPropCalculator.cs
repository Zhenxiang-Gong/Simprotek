using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   //class YawsThermalPropCalculator {
   //   public YawsThermalPropCalculator() {
   //      //
   //      // TODO: Add constructor logic here
   //      //
   //   }

   //   //calculated value unit is J/mol.K, t unit is K
   //   public static double CalculateLiquidHeatCapacity(double t, YawsLiquidCpCoeffs coeffs) {
   //      return coeffs.A + coeffs.B * t + coeffs.C * Math.Pow(t, 2) + coeffs.D * Math.Pow(t, 3);
   //   }

   //   //calculated value unit is J/mol.K, t unit is K
   //   public static double CalculateGasHeatCapacity(double t, YawsGasCpCoeffs coeffs) {
   //      return coeffs.A + coeffs.B * t + coeffs.C * Math.Pow(t, 2) + coeffs.D * Math.Pow(t, 3) + coeffs.E * Math.Pow(t, 4);
   //   }

   //   //calculated value unit is J/mol.K, t unit is K
   //   public static double CalculateMeanGasHeatCapacity(double t1, double t2, YawsGasCpCoeffs coeffs) {
   //      double h1 = coeffs.A * t1 + 0.5 * coeffs.B * Math.Pow(t1, 2) + 1.0 / 3.0 * coeffs.C * Math.Pow(t1, 3) + 0.25 * coeffs.D * Math.Pow(t1, 4) + 0.2 * coeffs.E * Math.Pow(t1, 5);
   //      double h2 = coeffs.A * t2 + 0.5 * coeffs.B * Math.Pow(t2, 2) + 1.0 / 3.0 * coeffs.C * Math.Pow(t2, 3) + 0.25 * coeffs.D * Math.Pow(t2, 4) + 0.2 * coeffs.E * Math.Pow(t2, 5);
   //      double meanCp = (h2 - h1) / (t2 - t1);
   //      return meanCp;
   //   }

   //   //calculated value unit is J/mol, t unit is K
   //   public static double CalculateEvaporationHeat(double t, YawsEvaporationHeatCoeffs coeffs) {
   //      double tr = t / coeffs.Tc;
   //      //calculated value unit is kJ/mol
   //      double r = coeffs.A * Math.Pow((1 - tr), coeffs.N);
   //      //convert to J/mol;
   //      return 1000* r;
   //   }

   //   //calculated value unit is Pa, t unit is K
   //   public static double CalculateVaporPressure(double t, YawsVaporPressureCoeffs coeffs) {
   //      // p is in mmHg
   //      double logP = coeffs.A + coeffs.B / t + coeffs.C * Math.Log10(t) + coeffs.D * t + coeffs.E * t *t;
   //      double p = Math.Pow(10, logP);
   //      //convert mmHg to Pa
   //      p = 133.3223684 * p;
   //      return p;
   //   }

   //   //calculated value unit is Pa, t unit is K
   //   public static double CalculateSaturationTemperature(double p, YawsVaporPressureCoeffs coeffs) {
   //      double temperature = 298.15;
   //      double old_temp;
   //      double fx;
   //      double dfx;
   //      int i = 0;
   //      do {
   //         i++;
   //         old_temp = temperature;
   //         //direct iterative method
   //         //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
   //         //   - 4.1653e-6 * old_temp * old_temp);
   //         //Newton iterative method--much better convergence speed
   //         fx = coeffs.A + coeffs.B / old_temp + coeffs.C * Math.Log10(old_temp) + coeffs.D * old_temp + coeffs.E * old_temp * old_temp - Math.Log10(p);
   //         dfx = -coeffs.B / (old_temp * old_temp) + coeffs.C * Math.Log10(Math.E) / old_temp + coeffs.D + 2.0 * coeffs.E * old_temp;
   //         temperature = old_temp - fx / dfx;
   //      } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

   //      return temperature;
   //   }

   //   //Yaws' Chemical Properties
   //   //calculated value unit is w/m.K, t unit is K
   //   public static double CalculateLiquidOrganicThermalConductivity(double t, YawsThermalConductivityCoeffs coeffs) {
   //      double logKLiq = coeffs.A + coeffs.B * Math.Pow((1.0 - t / coeffs.C), 2.0 / 7.0);
   //      return Math.Pow(10, logKLiq);
   //   }

   //   //Yaws' Chemical Properties
   //   //calculated value unit is w/m.K, t unit is K
   //   public static double CalculateLiquidInorganicThermalConductivity(double t, YawsThermalConductivityCoeffs coeffs) {
   //      double k = coeffs.A + coeffs.B * t + coeffs.C * t * t;
   //      return k;
   //   }

   //   //Yaws' Chemical Properties
   //   //calculated value unit is w/m.K, t unit is K
   //   public static double CalculateGasThermalConductivity(double t, YawsThermalConductivityCoeffs coeffs) {
   //      double k = coeffs.A + coeffs.B * t + coeffs.C * t * t;
   //      return k;
   //   }

   //   //Yaws' Chemical Properties
   //   //calculated value unit is Poiseuille == Pascal.Second, t unit is K
   //   public static double CalculateLiquidViscosity(double t, YawsLiquidViscosityCoeffs coeffs) {
   //      //viscosity unit of this formula is cnetipoise
   //      double logViscLiq = coeffs.A + coeffs.B / t + coeffs.C * t + coeffs.D * t * t;
   //      //Convert unit from cnetipoise to Pascal.Second 
   //      return 0.001 * Math.Pow(10, logViscLiq);
   //   }

   //   //Yaws' Chemical Properties
   //   //calculated value unit is Poiseuille == Pascal.Second, t unit is K
   //   public static double CalculateGasViscosity(double t, YawsGasViscosityCoeffs coeffs) {
   //      //viscosity unit of this formula is micropoise
   //      double visc = coeffs.A + coeffs.B * t + coeffs.C * t * t;
   //      //Convert unit from micropoise to Pascal.Second 
   //      return 1.0e-6 * visc;
   //   }

   //   //calculated value unit is kg/m3, t unit is K
   //   public static double CalculateLiquidDensity(double t, YawsLiquidDensityCoeffs coeffs) {
   //      double temp = Math.Pow((1.0 - t / coeffs.Tc), coeffs.N);
   //      //unit is g/ml
   //      double density = coeffs.A * Math.Pow(coeffs.B, -temp);
   //      //convert from g/ml to kg/m3
   //      return 1000 * density;
   //   }
   //}
}