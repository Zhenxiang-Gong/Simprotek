using System;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for GenericFoodPropCalculator.
   /// </summary>
   public class SpecialFoodPropCalculator {

      public static double GetViscosity(string foodName, double massConcentration, double t) {
         //not implemented yet
         return 1.0;
      }

      public static double GetThermalConductivity(string foodName, double massConcentration, double temperature) {
         double thermalCond = 0.0;
         if (foodName == "Milk") {
            thermalCond = MilkPropCalculator.GetThermalCond(massConcentration, temperature);
         }

         return thermalCond;
      }

      public static double GetCp(string foodName, double massConcentration, double temperature) {
         double cp = 0.0;
         if (foodName == "Milk") {
            cp = MilkPropCalculator.GetCp(massConcentration, temperature);
         }
         return cp;
      }

      public static double GetDensity(string foodName, double massConcentration, double temperature) {
         double density = 0;
         if (foodName == "Milk") {
            density = MilkPropCalculator.GetDensity(massConcentration, temperature, MilkType.WholeMilk);
         }

         return density;
      }
   }
}
