using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsLiquidDensityCorrelation : ThermalPropCorrelationBase {
      double a;
      double b;
      double tc;
      double n;

      //public double A {
      //   get { return a; }
      //}

      //public double B {
      //   get { return b; }
      //}
      
      //public double Tc {
      //   get { return tc; }
      //}

      //public double N {
      //   get { return n; }
      //}

      internal YawsLiquidDensityCorrelation(string substanceName, double a, double b, double tc, double n, 
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is kg/m3, t unit is K
      public double GetDensity(double t) {
         double temp = Math.Pow((1.0 - t / tc), n);
         //unit is g/ml
         double density = a * Math.Pow(b, -temp);
         //convert from g/ml to kg/m3
         return 1000 * density;
      }
   }
}
