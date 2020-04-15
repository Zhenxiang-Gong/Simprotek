using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsVaporPressureCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;
      private double d;
      private double e;

      //public double A {
      //   get { return a; }
      //}

      //public double B {
      //   get { return b; }
      //}

      //public double C {
      //   get { return c; }
      //}

      //public double D {
      //   get { return d; }
      //}

      //public double E {
      //   get { return e; }
      //}

      internal YawsVaporPressureCorrelation(string substanceName, double a, double b, double c, double d, double e,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
         this.e = e;
      }

      //calculated value unit is Pa, t unit is K
      public double GetVaporPressure(double t) {
         // p is in mmHg
         double logP = a + b / t + c * Math.Log10(t) + d * t + e * t * t;
         double p = Math.Pow(10, logP);
         //convert mmHg to Pa
         p = 133.3223684 * p;
         return p;
      }

      //calculated value unit is Pa, t unit is K
      public double CalculateSaturationTemperature(double p) {
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
            fx = a + b / old_temp + c * Math.Log10(old_temp) + d * old_temp + e * old_temp * old_temp - Math.Log10(p);
            dfx = -b / (old_temp * old_temp) + c * Math.Log10(Math.E) / old_temp + d + 2.0 * e * old_temp;
            temperature = old_temp - fx / dfx;
         } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

         return temperature;
      }
   }
}
