using System;
using System.Collections;
using System.Drawing;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;
using Prosimo.Plots;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for GenericFoodPropCalculator.
   /// </summary>
   public class MilkPropCalculator {

      static readonly PointF[] skimMilkA = {new PointF(5.0f, 1.0f), new PointF(20.0f, 0.9982f), 
             new PointF(35.0f, 0.9941f), new PointF(50.0f, 0.9881f), new PointF(60.0f, 0.9806f)};

      static readonly PointF[] skimMilkB = {new PointF(5.0f, 3.616e-3f), new PointF(20.0f, 3.519e-3f), 
             new PointF(35.0f, 3.504e-3f), new PointF(50.0f, 3.568e-3f), new PointF(60.0f, 3.601e-3f)};

      static readonly PointF[] skimMilkC = {new PointF(5.0f, 1.827e-5f), new PointF(20.0f, 1.782e-5f), 
             new PointF(35.0f, 1.664e-5f), new PointF(50.0f, 1.366e-5f), new PointF(60.0f, 1.308e-5f)};

      static readonly PointF[] wholeMilkA = {new PointF(5.0f, 1.001f), new PointF(20.0f, 1.008f), 
             new PointF(35.0f, 1.0137f), new PointF(50.0f, 0.9953f)};

      static readonly PointF[] wholeMilkB = {new PointF(5.0f, 2.55e-3f), new PointF(20.0f, 2.09e-3f), 
             new PointF(35.0f, 1.66e-3f), new PointF(50.0f, 2.11e-3f)};

      public static double GetViscosity(double massConcentraiton, double temperature) {
         //not implemented yet
         return 1.0;
      }

      public static double GetThermalCond(double massConcentration, double temperature) {
         temperature = temperature - 273.15;

         //MaterialComponent moistureComponent = materialComponents[1] as MaterialComponent;
         //double moistureFraction = moistureComponent.GetMassFractionValue();

         double thermalCond = (326.8 + 1.0412 * temperature - 0.00337 * temperature * temperature)
            * (0.44 + 0.54 * (1 - massConcentration)) * 1.73e-3;

         return thermalCond;
      }

      public static double GetCp(double massConcentraiton, double temperature) {
         temperature = temperature - 273.15;

         //MaterialComponent dryMatComponent = materialComponents[0] as MaterialComponent;
         //double dryMatFraction = dryMatComponent.GetMassFractionValue();

         double cp = 4.187 * (1.0 - massConcentraiton) + (0.238 + 0.0027 * temperature) * massConcentraiton;

         return 1000 * cp;
      }


      public static double GetDensity(double massConcentraiton, double temperature, MilkType milkType) {
         temperature = temperature - 273.15;

         //MaterialComponent dryMatComponent = materialComponents[0] as MaterialComponent;
         //double dryMatFraction = dryMatComponent.GetMassFractionValue();
         double a = 0;
         double b = 0;
         double c = 0;
         if (milkType == MilkType.SkimMilk) {
            a = ChartUtil.GetInterpolateValue(skimMilkA, temperature);
            b = ChartUtil.GetInterpolateValue(skimMilkB, temperature);
            c = ChartUtil.GetInterpolateValue(skimMilkC, temperature);
         }
         else if (milkType == MilkType.WholeMilk) {
            a = ChartUtil.GetInterpolateValue(skimMilkA, temperature);
            b = ChartUtil.GetInterpolateValue(skimMilkB, temperature);
         }

         double density = a + b * massConcentraiton + c * massConcentraiton * massConcentraiton;

         return 1000 * density;
      }
   }
}
