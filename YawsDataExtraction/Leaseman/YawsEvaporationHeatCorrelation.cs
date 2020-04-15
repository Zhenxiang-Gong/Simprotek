using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class YawsEvaporationHeatCorrelation : ThermalPropCorrelationBase {
      private double a;
      //critical temperature
      private double tc;
      private double n;
      private double boilingPoint;

      //public double A {
      //   get { return a; }
      //}

      //public double Tc {
      //   get { return tc; }
      //}

      //public double N {
      //   get { return n; }
      //}

      //public double BoilingPoint {
      //   get { return boilingPoint; }
      //}

      internal YawsEvaporationHeatCorrelation(string substanceName, double a, double tc, double n, double boilingPoint,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.tc = tc;
         this.n = n;
         this.boilingPoint = boilingPoint;
      }

      //calculated value unit is J/mol, t unit is K
      public double GetEvaporationHeat(double t) {
         double tr = t / tc;
         //calculated value unit is kJ/mol
         double r = a * Math.Pow((1 - tr), n);
         //convert to J/mol;
         return 1000 * r;
      }
   }
}
