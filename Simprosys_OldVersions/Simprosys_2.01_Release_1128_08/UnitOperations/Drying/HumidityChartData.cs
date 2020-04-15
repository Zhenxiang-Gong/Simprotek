using System;

using Prosimo.Plots;

namespace Prosimo.UnitOperations.Drying
{
   public class HumidityChartData {
      private string name;
      public string Name {
         get {return name;}
      }

      private CurveFamilyF[] curveFamilies;
      public CurveFamilyF[] CurveFamilies {
         get {return curveFamilies;}
      }

      public HumidityChartData(string name, CurveFamilyF[] curveFamilies) {
         this.name = name;
         this.curveFamilies = curveFamilies;
      }

      public CurveFamilyF GetCurveFamily(string name) {
         CurveFamilyF curveFamily = null;
         for (int i=0; i< curveFamilies.Length; i++) {
            curveFamily = curveFamilies[i];
            if (curveFamily.Name.Equals(name)) {
               break;
            }
         }
         return curveFamily;
      }
   }
}
