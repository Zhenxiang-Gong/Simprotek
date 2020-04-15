using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsGasViscosityCorrelation : ThermalPropCorrelationBase {
      double a;
      double b;
      double c;

      //public double A {
      //   get { return a; }
      //}

      //public double B {
      //   get { return b; }
      //}

      //public double C {
      //   get { return c; }
      //}

      internal YawsGasViscosityCorrelation(string substanceName, double a, double b, double c, 
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public double GetViscosity(double t) {
         //viscosity unit of this formula is micropoise
         double visc = a + b * t + c * t * t;
         //Convert unit from micropoise to Pascal.Second 
         return 1.0e-6 * visc;
      }
   }
}
