using System;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for ComponentsPropCalculator.
   /// </summary>
   public class MaterialPropCalculator {
      enum SubstanceState {Gas, Liquid};
      
      public static double GetGasViscosity(ArrayList materialComponents, double temperature) {
         return CalculateViscosity(materialComponents, temperature, SubstanceState.Gas);
      }
      
      public static double GetLiquidViscosity(ArrayList materialComponents, double temperature) {
         return CalculateViscosity(materialComponents, temperature, SubstanceState.Liquid);
      }

      public static double GetGasCp(ArrayList materialComponents, double temperature) {
         return CalculateCp(materialComponents, temperature, SubstanceState.Gas);
      }
      
      public static double GetSpecificHeatRatio(ArrayList materialComponents, double temperature) 
      {
         double heatCapacity = 0.0;
         double cp = 0;
         //double molarWeight = 0.0;
         double w;
         double molarFrac;
         double totalMole = 1.0;
         foreach (MaterialComponent mc in materialComponents) 
         {
            Substance s = mc.Substance;
            if (s.Name == "Generic Dry Material" || s is CompositeSubstance) 
            {
               continue;
            }
            w = s.MolarWeight;
            molarFrac = mc.GetMassFractionValue()/w;
            totalMole += molarFrac;
         }

         foreach (MaterialComponent mc in materialComponents) 
         {
            Substance s = mc.Substance;
            if (s.Name == "Generic Dry Material" || s is CompositeSubstance) 
            {
               continue;
            }
            w = s.MolarWeight;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            molarFrac = mc.GetMassFractionValue()/w/totalMole;
            heatCapacity = ThermalPropCalculator.CalculateGasHeatCapacity1(temperature, tpc.GasCpCoeffs);
            cp += heatCapacity*molarFrac;
         }

         return cp/(cp - Constants.R);
      }
      
      public static double GetLiquidCp(ArrayList materialComponents, double temperature) 
      {
         return CalculateCp(materialComponents, temperature, SubstanceState.Liquid);
      }
      
      public static double GetGasDensity(ArrayList materialComponents, double temperature, double pressure) {
         return 1.0;
      }
      
      public static double GetLiquidDensity(ArrayList materialComponents, double temperature) {
         double density = 0.0;
         double den = 0;
         double massFrac;
         foreach (MaterialComponent mc in materialComponents) {
            Substance s = mc.Substance;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            massFrac = mc.GetMassFractionValue();
            den = ThermalPropCalculator.CalculateLiquidDensity(temperature, tpc.LiqDensityCoeffs);
            density += den*massFrac;
         }

         return density;
      }
      

      public static double GetGasThermalConductivity(ArrayList materialComponents, double temperature) {
         return CalculateThermalConductivity(materialComponents, temperature, SubstanceState.Gas);
      }
      
      public static double GetLiquidThermalConductivity(ArrayList materialComponents, double temperature) {
         return CalculateThermalConductivity(materialComponents, temperature, SubstanceState.Liquid);
      }
      
      private static double CalculateViscosity(ArrayList materialComponents, double temperature, SubstanceState state) {
         double viscosity = 0.0;
         double visc;
         double molarWt;
         double moleFrac;
         double massFrac;
         double numerator = 0.0;
         double denominator = 0.0;
         foreach (MaterialComponent mc in materialComponents) {
            Substance s = mc.Substance;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            molarWt = s.MolarWeight;
            moleFrac = mc.GetMoleFractionValue();
            massFrac = mc.GetMassFractionValue();
            if (moleFrac == Constants.NO_VALUE) {
               moleFrac = massFrac;
            }
            if (state == SubstanceState.Gas) {
               if (s.Name == "Air") {
                  visc = ThermalPropCalculator.CalculateAirGasViscosity(temperature);
               }
               else {
                  visc = ThermalPropCalculator.CalculateGasViscosity(temperature, tpc.GasViscCoeffs);
               }
               numerator += visc*moleFrac*Math.Sqrt(molarWt);
               denominator += moleFrac*Math.Sqrt(molarWt);
            }
            else if (state == SubstanceState.Liquid) {
               visc = ThermalPropCalculator.CalculateLiquidViscosity(temperature, tpc.LiqViscCoeffs);
               numerator += moleFrac*Math.Log10(visc);
            }
         }

         if (state == SubstanceState.Gas) {
            if (denominator > 1.0e-8) {
               viscosity = numerator/denominator;
            }
         }
         else if (state == SubstanceState.Liquid) {
            viscosity = Math.Pow(10, numerator);
         }

         return viscosity;
      }

      //cp unit is J/kg.k
      private static double CalculateCp(ArrayList materialComponents, double temperature, SubstanceState state) {
         double heatCapacity = 0.0;
         double cp = 0;
         double molarWeight;
         double massFrac;
         foreach (MaterialComponent mc in materialComponents) {
            Substance s = mc.Substance;
            molarWeight = s.MolarWeight;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            massFrac = mc.GetMassFractionValue();
            if (state == SubstanceState.Gas) {
               cp = ThermalPropCalculator.CalculateGasHeatCapacity1(temperature, tpc.GasCpCoeffs)/molarWeight;
            }
            else if (state == SubstanceState.Liquid) {
               cp = ThermalPropCalculator.CalculateLiquidHeatCapacity1(temperature, tpc.LiqCpCoeffs)/molarWeight;
            }
            heatCapacity += cp*massFrac;
         }

         return heatCapacity;
      }
      
      private static double CalculateThermalConductivity(ArrayList materialComponents, double temperature, SubstanceState state) {
         double thermalCond = 0.0;
         double k = 0;
         double massFrac;
         foreach (MaterialComponent mc in materialComponents) {
            Substance s = mc.Substance;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            massFrac = mc.GetMassFractionValue();
            if (state == SubstanceState.Gas) {
               if (s.Name == "Air") {
                  k = ThermalPropCalculator.CalculateAirGasThermalConductivity(temperature);
               }
               else {
                  k = ThermalPropCalculator.CalculateGasThermalConductivity(temperature, tpc.GasKCoeffs);
               }
            }
            else if (state == SubstanceState.Liquid) {
               if (s.SubstanceType == SubstanceType.Inorganic) {
                  k = ThermalPropCalculator.CalculateLiquidInorganicThermalConductivity(temperature, tpc.LiqKCoeffs);
               }
               else if (s.SubstanceType == SubstanceType.Organic) {
                  k = ThermalPropCalculator.CalculateLiquidOrganicThermalConductivity(temperature, tpc.LiqKCoeffs);
               }
            }
            thermalCond += k*massFrac;
         }

         return thermalCond;
      }
   }
}
