using System;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for GenericFoodPropCalculator.
   /// </summary>
   public class GenericFoodPropCalculator {
      
      public static double GetViscosity(ArrayList materialComponents, double t) {
         //not implemented yet
         return 1.0;
      }

      public static double GetThermalConductivity(ArrayList materialComponents, double t) {
         t = t - 273.15;
         double thermalCond = 0.0;
         double k = 0;
         double den = 0;
         double massFrac;
         
         double total = 0.0;

         MaterialComponent dryMatComponent = materialComponents[0] as MaterialComponent;
         CompositeSubstance drySubstance = dryMatComponent.Substance as CompositeSubstance;
         double dryTotalFraction = dryMatComponent.GetMassFractionValue();

         ArrayList components = drySubstance.Components;
         foreach (MaterialComponent mc in components) {
            Substance s = mc.Substance;
            massFrac = mc.GetMassFractionValue();
            if (s.Name == "Carbohydrate") {
               den = GetCarbohydrateDensity(t);
            }
            else if (s.Name == "Ash") {
               den = GetAshDensity(t);
            }
            else if (s.Name == "Fiber") {
               den = GetFiberDensity(t);
            }
            else if (s.Name == "Fat") {
               den = GetFatDensity(t);
            }
            else if (s.Name == "Protein") {
               den = GetProteinDensity(t);
            }

            total += dryTotalFraction * massFrac/den;
         }
            
         double volumeFraction;
         double moistureDensity;
         MaterialComponent moistureComponent = materialComponents[1] as MaterialComponent;
         double moistureFraction = moistureComponent.GetMassFractionValue();
         
         moistureDensity = GetWaterDensity(t);

         total += moistureFraction/moistureDensity;
         
         foreach (MaterialComponent mc in components) {
            Substance s = mc.Substance;
            massFrac = mc.GetMassFractionValue();
            if (s.Name == "") {
               k = GetCarbohydrateThermalConductivity(t);
               den = GetCarbohydrateDensity(t);
            }
            else if (s.Name == "Ash") {
               k = GetAshThermalConductivity(t);
               den = GetAshDensity(t);
            }
            else if (s.Name == "Fiber") {
               k = GetFiberThermalConductivity(t);
               den = GetFiberDensity(t);
            }
            else if (s.Name == "Fat") {
               k = GetFatThermalConductivity(t);
               den = GetFatDensity(t);
            }
            else if (s.Name == "Protein") {
               k = GetProteinThermalConductivity(t);
               den = GetProteinDensity(t);
            }

            volumeFraction = dryTotalFraction * massFrac/den/total;
            thermalCond += k*volumeFraction;
         }

         //water conductivity
         k = GetWaterThermalConductivity(t);

         volumeFraction = dryTotalFraction * moistureFraction/moistureDensity/total;
         thermalCond += k*volumeFraction;

         return thermalCond;
      }

      public static double GetCp(ArrayList materialComponents, double t) {
         t = t - 273.15;
         double heatCapacity = 0.0;
         double cp = 0;
         double massFrac;
         
         MaterialComponent dryMatComponent = materialComponents[0] as MaterialComponent;
         CompositeSubstance drySubstance = dryMatComponent.Substance as CompositeSubstance;
         double dryTotalFraction = dryMatComponent.GetMassFractionValue();

         ArrayList components = drySubstance.Components;
         foreach (MaterialComponent mc in components) {
            Substance s = mc.Substance;
            massFrac = mc.GetMassFractionValue();
            if (s.Name == "Carbohydrate") {
               cp = GetCarbohydrateCp(t);
            }
            else if (s.Name == "Ash") {
               cp = GetAshCp(t);
            }
            else if (s.Name == "Fiber") {
               cp = GetFiberCp(t);
            }
            else if (s.Name == "Fat") {
               cp = GetFatCp(t);
            }
            else if (s.Name == "Protein") {
               cp = GetProteinCp(t);
            }

            heatCapacity += cp * massFrac * dryTotalFraction;
         }

         MaterialComponent moistureComponent = materialComponents[1] as MaterialComponent;
         double moistureFraction = moistureComponent.GetMassFractionValue();
         cp = GetWaterCp(t);

         heatCapacity += cp * moistureFraction;
         
         return 1000*heatCapacity;
      }

      public static double GetDensity(ArrayList materialComponents, double t) {
         t = t - 273.15;
         double total = 0.0;
         double den = 0;
         double massFrac;
         MaterialComponent dryMatComponent = materialComponents[0] as MaterialComponent;
         CompositeSubstance drySubstance = dryMatComponent.Substance as CompositeSubstance;
         double dryTotalFraction = dryMatComponent.GetMassFractionValue();

         ArrayList components = drySubstance.Components;
         foreach (MaterialComponent mc in components) {
            Substance s = mc.Substance;
            massFrac = mc.GetMassFractionValue();
            if (s.Name == "Carbohydrate") {
               den = GetCarbohydrateDensity(t);
            }
            else if (s.Name == "Ash") {
               den = GetAshDensity(t);
            }
            else if (s.Name == "Fiber") {
               den = GetFiberDensity(t);
            }
            else if (s.Name == "Fat") {
               den = GetFatDensity(t);
            }
            else if (s.Name == "Protein") {
               den = GetProteinDensity(t);
            }

            total += dryTotalFraction*massFrac/den;
         }

         MaterialComponent moistureComponent = materialComponents[1] as MaterialComponent;
         double moistureFraction = moistureComponent.GetMassFractionValue();
         double moistureDensity = GetWaterDensity(t);
         total += moistureFraction/moistureDensity;
         
         double density = 1.0/total;

         return density;
      }

      public static double GetCarbohydrateCp(double t) {
         return (1.5488 + 1.9625e-3 * t - 5.9399e-6 * t * t);
      }
      
      public static double GetAshCp(double t) {
         return (1.0926 + 1.8896e-3 * t - 3.6817e-6 * t * t);
      }
      
      public static double GetFiberCp(double t) {
         return (1.8549 + 1.8306e-3 * t - 4.6509e-6 * t * t);
      }
      
      public static double GetFatCp(double t) {
         return (1.9842 + 1.4733e-3 * t - 4.8008e-6 * t * t);
      }
      
      public static double GetProteinCp(double t) {
         return (2.0082 + 1.2089e-3 * t - 1.3129e-6 * t * t);
      }
   
      public static double GetWaterCp(double t) {
         double cp = 0.0;
         if (t >= 0.0) {
            cp = 4.1762 - 9.0864e-5 * t + 5.4731e-6 * t * t;
         }
         else {
            cp = 4.0817 - 5.3062e-3 * t + 9.9516e-4 * t * t;
         }
         return cp;
      }

      public static double GetCarbohydrateDensity(double t) {
         return (1.5991e3 - 0.31046 * t);
      }

      public static double GetAshDensity(double t) {
         return (2.4238e3 - 0.28063 * t);
      }

      public static double GetFiberDensity(double t) {
         return (1.3115e3 - 0.36589 * t);
      }

      public static double GetFatDensity(double t) {
         return (9.2559e2 - 0.51757 * t);
      }

      public static double GetProteinDensity(double t) {
         return (1.3229e3 - 0.51840 * t);
      }

      public static double GetWaterDensity(double t) {
         return (997.18 + 3.1439e-3 * t - 3.7574e-3 * t * t);
      }
   
      public static double GetCarbohydrateThermalConductivity(double t) {
         return (0.20141 + 1.3874e-3 * t - 4.3312e-6 * t * t);
      }
      
      public static double GetAshThermalConductivity(double t) {
         return (0.32962 + 1.4011e-3 * t - 2.9069e-6 * t * t);
      }
      
      public static double GetFiberThermalConductivity(double t) {
         return (0.18331 + 1.2497e-3 * t - 3.1683e-6 * t * t);
      }
      
      public static double GetFatThermalConductivity(double t) {
         return (0.18071 + 2.7604e-3 * t - 1.7749e-7 * t * t);
      }
      
      public static double GetProteinThermalConductivity(double t) {
         return (0.17881 + 1.1958e-3 * t - 2.7178e-6 * t * t);
      }

      public static double GetWaterThermalConductivity(double t) {
         return (0.57109 + 1.7625e-3 * t - 6.7036e-6 * t * t);
      }
   }
}
