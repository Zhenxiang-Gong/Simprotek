using System;
using Prosimo.SubstanceLibrary;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for MoistureProperties.
   /// </summary>
   public class MoistureProperties {
      //private const double r0 = 2501.0;
      //private const double n1 = 0.3298;
      //private const double n2 = 0.38;
      //private const double tc = 647.3;
      //private const double t0 = 273.15;
      private Substance moisture;
      private const double minimumTemperature = 1.0e-10;
      //water properties-------------------
      //Perry's
      /*private const double tc = 647.13;
      private double[] liqCpCoeffs = {2.7637e5, -2.0901e3, 8.125, -1.4116e-2, 9.3701e-6};
      private double[] gasCpCoeffs = {0.3336e5, 0.2679e5, 2.6105e3, 0.089e5, 1169.0};
      private double[] evapHeatCoeffs = {5.2053e7, 0.3199, -0.212, 0.25795};
      private double[] vapPressureCoeffs = {73.649, -7258.2, -7.3037, 4.1653e-6, 2.0};
      private double molarWeight = 18.015; */
      private double criticalTemperature;
      //private double[] liqCpCoeffs;
      //private double[] gasCpCoeffs;
      //private double[] liqDensityCoeffs;
      //private double[] evapHeatCoeffs;
      //private double[] vapPressureCoeffs;
      private double molarWeight;
      //water properties-------------------

      private ThermalPropCalculator thermalPropCalculator = ThermalPropCalculator.Instance;

      public MoistureProperties(Substance moisture) {
         this.moisture = moisture;
         //ThermalPropsAndCoeffs tpc = moisture.ThermalPropsAndCoeffs;
         criticalTemperature = moisture.CriticalPropsAndAcentricFactor.CriticalTemperature;
         //liqCpCoeffs = tpc.LiqCpCoeffs;
         //gasCpCoeffs = tpc.GasCpCoeffs;
         //liqDensityCoeffs = tpc.LiqDensityCoeffs;
         //evapHeatCoeffs = tpc.EvapHeatCoeffs;
         //vapPressureCoeffs = tpc.VapPressureCoeffs;
         molarWeight = moisture.MolarWeight;
      }

      public double GetEvaporationHeat(double temperature) {
         if (temperature < minimumTemperature || temperature > criticalTemperature) {
            throw new IllegalVarValueException("Temperature out of range.");
         }
         //double n = n1;
         //if (temperature > 563.15 && temperature <= tc) {
         //   n = n2;
         //}
         //double c = Math.Pow((tc-temperature)/(tc-t0), n);
         //return r0 * Math.Pow((tc-temperature)/(tc-t0), n);
         //this correlation comes from Perry's Chemical Engineers Handbook, 6th edition
         //double tr = temperature/PropConstants.Tc;
         //double tempValue = 0.3199 - 0.212 * tr + 0.25795 * tr * tr;
         //double r = 5.2053e7/18.015 * Math.Pow((1-tr), tempValue);
         //return r;
         //double r = ThermalPropCalculator.CalculateEvaporationHeat(temperature, criticalTemperature, evapHeatCoeffs);
         double r = thermalPropCalculator.CalculateEvaporationHeat(temperature, moisture);
         //return r / molarWeight;
         return r;
      }

      public double GetSaturationPressure(double temperature) {
         if (temperature < minimumTemperature || temperature > criticalTemperature) {
            throw new IllegalVarValueException("Temperature out of range");
         }

         double pSaturation = 1.0;

         //if (moisture.Name.Equals("Water")) {
         //   pSaturation = ThermalPropCalculator.CalculateWaterSaturationPressure(temperature);
         //}
         //else {
         //   pSaturation = ThermalPropCalculator.CalculateSaturationPressure(temperature, moisture);
         //}
         pSaturation = thermalPropCalculator.CalculateSaturationPressure(temperature, moisture);

         return pSaturation;
      }

      public double GetSaturationTemperature(double pressure) {
         double temperature = 1.0e-10;
         //if (moisture.Name.Equals("Water")) {
         //   temperature = ThermalPropCalculator.CalculateWaterSaturationTemperature(pressure);
         //}
         //else {
         //   temperature = ThermalPropCalculator.CalculateSaturationTemperature(pressure, moisture);
         //}
         temperature = thermalPropCalculator.CalculateSaturationTemperature(pressure, moisture);

         return temperature;
      }

      public double GetDensity(double pressure, double temperature) {
         //double density = 1;
         //if (moisture.Name.Equals("Water")) {
         //   density = ThermalPropCalculator.CalculateWaterDensity(pressure, temperature);
         //}
         //else {
         //   double satTemperature = GetSaturationTemperature(pressure);
         //   if (temperature < satTemperature) {
         //      density = ThermalPropCalculator.CalculateLiquidDensity(temperature, moisture);
         //   }
         //   else if (temperature > satTemperature) {
         //      //density = ThermalPropCalculator.CalculateLiquidDensity(temperature);
         //      //need to use state equation to calculate state
         //   }
         //}

         return thermalPropCalculator.CalculateLiquidDensity(pressure, temperature, moisture);
      }

      public double GetSpecificHeatOfLiquid(double temperature) {
         //this correlation comes from Perry's Chemical Engineers Handbook
         //double cp = 2.7637e5 - 2.0901e3 * temperature + 8.125 * temperature * temperature
         //            -1.4116e-2 * Math.Pow(temperature, 3) + 9.3701e-6 * Math.Pow(temperature, 4);
         //return cp / molarWeight;
         return thermalPropCalculator.CalculateLiquidHeatCapacity(temperature, moisture);
      }

      public double GetMeanSpecificHeatOfLiquid(double t1, double t2) {
         //this correlation comes from Perry's Chemical Engineers Handbook
         //double cp = 2.7637e5 - 2.0901e3 * temperature + 8.125 * temperature * temperature
         //            -1.4116e-2 * Math.Pow(temperature, 3) + 9.3701e-6 * Math.Pow(temperature, 4);
         //return cp / molarWeight;
         return thermalPropCalculator.CalculateLiquidMeanHeatCapacity(t1, t2, moisture);
      }

      public double GetMeanSpecificHeatOfLiquid(double p, double t1, double t2) {
         return thermalPropCalculator.CalculateLiquidMeanHeatCapacity(p, t1, t2, moisture);
      }

      public double GetEnthalpyFromPressureAndTemperature(double p, double t) {
         return thermalPropCalculator.CalculateEnthalpyFromPressureAndTemperature(p, t, moisture);
      }

      public double GetEnthalpyFromPressureAndVaporFraction(double p, double vf) {
         return thermalPropCalculator.CalculateEnthalpyFromPressureAndVaporFraction(p, vf, moisture);
      }

      public double GetTemperatureFromPressureAndEnthalpy(double p, double h) {
         return thermalPropCalculator.CalculateTemperatureFromPressureAndEnthalpy(p, h, moisture);
      }

      public double GetSpecificHeatOfLiquid() {
         //return 4187.0;
         return GetSpecificHeatOfLiquid(293.15);
      }

      public double GetSpecificHeatOfVapor(double temperature) {
         //return 1880.0;
         double cp = thermalPropCalculator.CalculateGasHeatCapacity(temperature, moisture);
         //return cp / molarWeight;
         return cp;
      }

      public double GetMeanSpecificHeatOfVapor(double t1, double t2) {
         //return 1880.0;
         double cp;
         if (Math.Abs(t1 - t2) > 1.0e-4) {
            cp = thermalPropCalculator.CalculateGasMeanHeatCapacity(t1, t2, moisture);
         }
         else {
            cp = thermalPropCalculator.CalculateGasHeatCapacity(t1, moisture);
         }
         //return cp / molarWeight;
         return cp;
      }

      public double GetMeanSpecificHeatOfVapor(double p, double t1, double t2) {
         //double cp;
         //if (moisture.Name.Equals("Water")) {
         //   if (Math.Abs(t1 - t2) > 1.0e-4) {
         //      cp = ThermalPropCalculator.CalculateWaterMeanHeatCapacity(p, t1, t2);
         //   }
         //   else {
         //      cp = ThermalPropCalculator.CalculateWaterMeanHeatCapacity(p, t1, t1 + 1);
         //   }
         //}
         //else {
         //   cp = ThermalPropCalculator.CalculateMeanGasHeatCapacity(t1, t2, moisture);
         //   cp = cp / molarWeight;
         //}
         double cp = thermalPropCalculator.CalculateGasMeanHeatCapacity(p, t1, t2, moisture);
         return cp;
      }

      public double GetSpecificHeatOfVapor() {
         //return 1880.0;
         return GetSpecificHeatOfVapor(293.15);
      }

      //public double GetMolarWeight() {
      //   return molarWeight;
      //}
   }
}