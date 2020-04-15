using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsLiquidThermalConductivityCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;

      //public double A {
      //   get { return a; }
      //}

      //public double B {
      //   get { return b; }
      //}

      //public double C {
      //   get { return c; }
      //}

      internal YawsLiquidThermalConductivityCorrelation(string substanceName, double a, double b, double c, 
         double minTemperature, double maxTemperature)
         : base (substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public double GetThermalConductivity(double t, SubstanceType substanceType) {
         double k = 0.0;
         if (substanceType == SubstanceType.Organic) {
            double logKLiq = a + b * Math.Pow((1.0 - t / c), 2.0 / 7.0);
            k = Math.Pow(10, logKLiq);
         }
         else if (substanceType == SubstanceType.Inorganic) {
            k = a + b * t + c * t * t;
         }

         return k;
      }
   }
}
