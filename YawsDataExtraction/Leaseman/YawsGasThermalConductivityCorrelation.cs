using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsGasThermalConductivityCorrelation : ThermalPropCorrelationBase {
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

      internal YawsGasThermalConductivityCorrelation(string substanceName, double a, double b, double c, 
         double minTemperature, double maxTemperature)
         : base (substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public double GetThermalConductivity(double t) {
         double k = a + b * t + c * t * t;
         return k;
      }
   }
}
