using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsLiquidViscosityCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;
      private double d;

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

      internal YawsLiquidViscosityCorrelation(string substanceName, double a, double b, double c, double d, 
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
      }

      //Yaws' Chemical Properties
      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public double GetViscosity(double t) {
         //viscosity unit of this formula is cnetipoise
         double logViscLiq = a + b / t + c * t + d * t * t;
         //Convert unit from cnetipoise to Pascal.Second 
         return 0.001 * Math.Pow(10, logViscLiq);
      }
   }
}
