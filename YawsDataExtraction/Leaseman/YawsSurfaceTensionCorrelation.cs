using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsSurfaceTensionCorrelation : ThermalPropCorrelationBase {
      double a;
      double tc;
      double n;

      //public double A {
      //   get { return a; }
      //}

      //public double Tc {
      //   get { return tc; }
      //}

      //public double N {
      //   get { return n; }
      //}

      internal YawsSurfaceTensionCorrelation(string substanceName, double a, double tc, double n, 
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is kg/m3, t unit is K
      public double GetSurfaceTension(double t) {
         //unit is dyne/cm
         double surfaceTension = a * Math.Pow((1.0 - t / tc), n);
         //convert from dyne/cm to N/m
         return 1.0e-3 * surfaceTension;
      }
   }
}
