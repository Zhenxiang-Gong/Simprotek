using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsGasCpCorrelation : ThermalPropCorrelationBase {
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

      internal YawsGasCpCorrelation(string substanceName, double a, double b, double c, double d, double e,
         double minTemperature, double maxTemperature)
         : base (substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
         this.e = e;
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetCp(double t) {
         return a + b * t + c * Math.Pow(t, 2) + d * Math.Pow(t, 3) + e * Math.Pow(t, 4);
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetMeanCp(double t1, double t2) {
         double h1 = a * t1 + 0.5 * b * Math.Pow(t1, 2) + 1.0 / 3.0 * c * Math.Pow(t1, 3) + 0.25 * d * Math.Pow(t1, 4) + 0.2 * e * Math.Pow(t1, 5);
         double h2 = a * t2 + 0.5 * b * Math.Pow(t2, 2) + 1.0 / 3.0 * c * Math.Pow(t2, 3) + 0.25 * d * Math.Pow(t2, 4) + 0.2 * e * Math.Pow(t2, 5);
         double meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }
   }
}
