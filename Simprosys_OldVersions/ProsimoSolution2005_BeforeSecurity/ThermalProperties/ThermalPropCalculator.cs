using System;

namespace Prosimo.ThermalProperties {

   /// <summary>
   /// Summary description for HeatCapacityCalculator.
   /// </summary>
   public class ThermalPropCalculator {
      public ThermalPropCalculator() {
         //
         // TODO: Add constructor logic here
         //
      }

      //Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
      //calculated value unit is J/kmol.K, t unit is K
      public static double CalculateLiquidHeatCapacity1(double t, double[] c) {
         return c[0] + c[1] * t + c[2] * t * t + c[3] * Math.Pow(t, 3.0) + c[4] * Math.Pow(t, 4.0);
      }

      //Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
      //calculated value unit is J/kmol.K, t unit is K
      public static double CalculateMeanLiquidHeatCapacity1(double t1, double t2, double[] c) {
         double h1 = c[0] * t1 + c[1] * t1 * t1 / 2.0 + c[2] * Math.Pow(t1, 3.0) / 3.0 + c[3] * Math.Pow(t1, 4.0) / 4.0 + c[4] * Math.Pow(t1, 5.0) / 5.0;
         double h2 = c[0] * t2 + c[1] * t2 * t2 / 2.0 + c[2] * Math.Pow(t2, 3.0) / 3.0 + c[3] * Math.Pow(t2, 4.0) / 4.0 + c[4] * Math.Pow(t2, 5.0) / 5.0;
         double meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }

      //Perry's Liquid Heat Capacity equation 2
      //calculated value is J/kmol.K, t unit is K
      public static double CalculateLiquidHeatCapacity2(double t, double tc, double[] c) {
         t = 1.0 - t / tc;
         return c[0] * c[0] / t + c[1] - 2.0 * c[0] * c[2] * t - c[0] * c[3] * t * t - Math.Pow(c[2], 2.0 / 3.0) * Math.Pow(t, 3.0) - c[2] * c[3] / 2.0 * Math.Pow(t, 4.0) - Math.Pow(c[3], 2.0 / 5.0) * Math.Pow(t, 5.0);
      }

      //Perry's Gas Heat Capacity default formula--Equation 1
      //calculated value unit is J/kmol.K, t unit is K
      public static double CalculateGasHeatCapacity1(double t, double[] c) {
         double term2 = c[2] / t;
         term2 = term2 / Math.Sinh(term2);
         term2 = term2 * term2;
         double term3 = c[4] / t;
         term3 = term3 / Math.Cosh(term3);
         term3 = term3 * term3;
         return c[0] + c[1] * term2 + c[3] * term3;
      }

      //Perry's Gas Heat Capacity default formula--Equation 1
      //calculated value unit is J/kmol.K, t unit is K
      public static double CalculateMeanGasHeatCapacity1(double t1, double t2, double[] c) {
         double h1 = c[0] * t1 + c[1] * c[2] / Math.Tanh(c[2] / t1) - c[3] * c[4] * Math.Tanh(c[4] / t1);
         double h2 = c[0] * t2 + c[1] * c[2] / Math.Tanh(c[2] / t2) - c[3] * c[4] * Math.Tanh(c[4] / t2);
         double meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }

      //Perry's Gas Heat Capacity--Equation 2
      //calculated value unit is J/kmol.K, t unit is K
      public static double CalculateGasHeatCapacity2(double t, double[] c) {
         return c[1] + c[1] * t + c[2] * t * t + c[3] * Math.Pow(t, 3.0) + c[4] * Math.Pow(t, 5.0);
      }

      //Perry's Gas Heat Capacity--Equation 2
      //calculated value is J/kmol.K, t unit is K
      public static double CalculateGasHeatCapacity3(double t, double[] c) {
         return c[0] + c[1] * Math.Log(t) + c[2] / t - c[3] * t;
      }

      //Perry's
      //calculated value unit is J/kmol, t unit is K
      public static double CalculateEvaporationHeat(double t, double tc, double[] c) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         double tr = t / tc;
         double tempValue = c[1] + c[2] * tr + c[3] * tr * tr;
         double r = c[0] * Math.Pow((1 - tr), tempValue);
         return r;
      }

      //Perry's
      //calculated value unit is Pa, t unit is K
      public static double CalculateSaturationPressure(double t, double[] c) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         double p = Math.Exp(c[0] + c[1] / t + c[2] * Math.Log(t) + c[3] * Math.Pow(t, c[4]));
         return p;
      }

      //Perry's
      //calculated value unit is Pa, t unit is K
      public static double CalculateSaturationTemperature(double p, double[] c) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         //double p = Math.Exp(c[0] + c[1]/t + c[2] * Math.Log(t) + c[3] * Math.Pow(t, c[4]));
         double temperature = 298.15;
         double old_temp;
         double fx;
         double dfx;
         int i = 0;
         do {
            i++;
            old_temp = temperature;
            //direct iterative method
            //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
            //   - 4.1653e-6 * old_temp * old_temp);
            //Newton iterative method--much better convergence speed
            fx = c[0] + c[1] / old_temp + c[2] * Math.Log(old_temp) + c[3] * Math.Pow(old_temp, c[4]) - Math.Log(p);
            dfx = -c[1] / (old_temp * old_temp) - c[2] / old_temp + c[3] * c[4] * Math.Pow(old_temp, c[4] - 1);
            temperature = old_temp - fx / dfx;
         } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

         return temperature;
      }


      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public static double CalculateLiquidOrganicThermalConductivity(double t, double[] c) {
         double logKLiq = c[0] + c[1] * Math.Pow((1.0 - t / c[2]), 2.0 / 7.0);
         return Math.Pow(10, logKLiq);
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public static double CalculateLiquidInorganicThermalConductivity(double t, double[] c) {
         double k = c[0] + c[1] * t + c[2] * t * t;
         return k;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public static double CalculateGasThermalConductivity(double t, double[] c) {
         double k = c[0] + c[1] * t + c[2] * t * t;
         return k;
      }

      //Yaws' Chemical Properties
      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public static double CalculateLiquidViscosity(double t, double[] c) {
         double logViscLiq = c[0] + c[1] / t + c[2] * t + c[3] * t * t;//viscosity unit of this formula is cnetipoise
         return 0.001 * Math.Pow(10, logViscLiq);
      }

      //Yaws' Chemical Properties
      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public static double CalculateGasViscosity(double t, double[] c) {
         double visc = c[0] + c[1] * t + c[2] * t * t;//viscosity unit of this formula is micropoise
         return 1.0e-6 * visc;
      }

      //Perry's
      //calculated value unit is kmol/m3, t unit is K
      public static double CalculateLiquidDensity(double t, double[] c) {
         double tempValue = 1.0 + Math.Pow((1.0 - t / c[2]), c[3]);
         return c[0] / Math.Pow(c[1], tempValue);
      }

      //Perry's
      //Calculated value unit is kmol/m3, t unit is K
      public static double CalculateWaterLiquidDensity(double t) {
         double[] c = new double[4] { 5.459, 0.30542, 647.13, 0.081 };
         if (t > 333.15 && t <= 403.15) {
            c[0] = 4.9669;
            c[1] = 2.7788e-1;
            c[2] = 6.4713e2;
            c[3] = 1.8740e-1;
         }
         else if (t > 403.15 && t <= 6471.3) {
            c[0] = 4.391;
            c[1] = 2.487e-1;
            c[2] = 6.4713e2;
            c[3] = 2.5340e-1;
         }
         return CalculateLiquidDensity(t, c);
      }

      //t unit is K
      public static double CalculateWaterSaturationPressureBelowFreezingPoint(double t) {
         t = t - 273.15;
         double p = 100.0 * 6.1121 * Math.Exp(17.502 * t / (240.97 + t));
         return p;
      }

      public static double CalculateWaterSaturationTemperature(double pressure) {
         double temperature = 1.0e-6;
         if (pressure < 1.0e-6) {
            pressure = 1.0e-6;
         }

         if (pressure <= 611.22) { //temperature below the steam table can deal with
            //temperature = 193.03 + 28.633 * Math.Pow(pressure, 0.1609);
            double a = Math.Log(pressure / 611.21);
            temperature = a * 240.97 / (17.502 - a) + 273.15;
         }
         else if (pressure > 611.22) {
            SteamTable steamTable = SteamTable.GetInstance();
            temperature = steamTable.GetSaturationTemperature(pressure);
         }
         /*//use correlation from Perry's to finalize
         else {
            //use simple correlation to do a rough estimation
            temperature = 3816.44/(23.197 - Math.Log(pressure)) + 46.13;
            double old_temp;
            double fx;
            double dfx;
            int i = 0;
            do {
               i++;
               old_temp = temperature;
               //direct iterative method
               //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
               //   - 4.1653e-6 * old_temp * old_temp);
               //Newton iterative method--much better convergence speed
               fx = 73.649 - 7258.2/old_temp - 7.3037 * Math.Log(old_temp) + 4.1653e-6 * old_temp * old_temp - Math.Log(pressure); 
               dfx = 7258.2/(old_temp * old_temp) - 7.3037/old_temp + 2.0 * 4.1653e-6 * old_temp;
               temperature = old_temp - fx/dfx;
            } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6); 
         }*/

         return temperature;
      }

      public static double CalculateWaterSaturationPressure(double temperature) {
         double pSaturation = 1.0;
         //if (temperature >= 253.15 && temperature <= 283.15) {
         //   pSaturation = 8.8365E-10 * Math.Pow((temperature -193.03), 6.2145);
         //}
         //else if (temperature <= 453.15) {
         //   pSaturation = Math.Exp(23.197 - 3816.44/(temperature - 46.13));
         //}
         if (temperature <= 273.33) { //temperature belown which steam table can not deal
            //double t = temperature - 273.15;
            //pSaturation = 100.0 * 6.1121 * Math.Exp(17.502 * t/(240.97 + t));
            //To Do: When system is not air-water, need to change!!!
            pSaturation = ThermalPropCalculator.CalculateWaterSaturationPressureBelowFreezingPoint(temperature);
         }
         else {
            //formulation coming from Perry's Chemical Engineer's Handbook
            //pSaturation = Math.Exp(73.649 - 7258.2/temperature - 7.3037 * Math.Log(temperature) 
            //   + 4.1653e-6 * temperature * temperature);
            //pSaturation = ThermalPropCalculator.CalculateVaporPressure(temperature, vapPressureCoeffs); 
            SteamTable steamTable = SteamTable.GetInstance();
            pSaturation = steamTable.GetSaturationPressure(temperature);
         }

         return pSaturation;
      }

      //Steam Table
      //calculated value unit is kmol/m3, t unit is K
      public static double CalculateWaterDensity(double p, double t) {
         SteamTable steamTable = SteamTable.GetInstance();
         double density = steamTable.GetDensityFromPressureAndTemperature(p, t);
         return density;
      }

      //Steam Table
      //calculated value unit is J/kg.K, t unit is K
      public static double CalculateWaterMeanHeatCapacity(double p, double t1, double t2) {
         SteamTable steamTable = SteamTable.GetInstance();
         double h1 = steamTable.GetEnthalpyFromPressureAndTemperature(p, t1);
         double h2 = steamTable.GetEnthalpyFromPressureAndTemperature(p, t2);
         double cp = (h2 - h1) / (t2 - t1);
         return cp;
      }

      //Steam Table
      //calculated value unit is J/kg.K, t unit is K
      public static double CalculateWaterEnthalpy(double p, double t) {
         SteamTable steamTable = SteamTable.GetInstance();
         double h = steamTable.GetEnthalpyFromPressureAndTemperature(p, t);
         return h;
      }

      //formula comes from a web site
      //calculated value unit is kg/m3, t unit is K
      public static double CalculateAirGasDensity(double t) {
         return 360.77819 * Math.Pow(t, -1.00336);
      }

      //calculated value unit is w/m.K, t unit is K
      public static double CalculateAirGasThermalConductivity(double t) {
         return -3.9333e-4 + 1.0184e-4 * t - 4.8574e-8 * t * t + 1.5207e-11 * t * t * t;
      }

      //calculated value unit is w/m.K, t unit is K
      public static double CalculateAirGasViscosity(double t) {
         double kinematicVisc = -3.4484e-6 + 3.7604e-8 * t + 9.5728e-11 * t * t - 1.1555e-14 * t * t * t;
         double density = CalculateAirGasDensity(t);
         return kinematicVisc * density;
      }
   }
}
