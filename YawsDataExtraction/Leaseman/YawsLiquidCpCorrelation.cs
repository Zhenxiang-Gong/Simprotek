using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsLiquidCpCorrelation : ThermalPropCorrelationBase {
      double a;
      double b;
      double c;
      double d;

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

      internal YawsLiquidCpCorrelation(string substanceName, double a, double b, double c, double d, 
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetCp(double t) {
         return a + b * t + c * Math.Pow(t, 2) + d * Math.Pow(t, 3);
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetMeanCp(double t1, double t2) {
         double h1 = a * t1 + 0.5 * b * Math.Pow(t1, 2) + 1.0 / 3.0 * c * Math.Pow(t1, 3) + 0.25 * d * Math.Pow(t1, 4);
         double h2 = a * t2 + 0.5 * b * Math.Pow(t2, 2) + 1.0 / 3.0 * c * Math.Pow(t2, 3) + 0.25 * d * Math.Pow(t2, 4);
         double meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }
   }
}
