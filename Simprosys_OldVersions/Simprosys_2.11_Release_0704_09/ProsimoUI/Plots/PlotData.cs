using System;
using System.Collections;
using System.Drawing;

namespace ProsimoUI.Plots
{
   /// <summary>
   /// Summary description for PlotData.
   /// </summary>
   public class PlotData
   {
      private string name;
      public string Name
      {
         get {return name;}
         set {name = value;}
      }

      private CurveFamily[] curveFamilies;
      public CurveFamily[] CurveFamilies
      {
         get {return curveFamilies;}
         set {curveFamilies = value;}
      }

      public PlotData()
      {
         this.name = null;
         this.curveFamilies = null;
      }

      public PlotData(string name, CurveFamily[] curveFamilies)
      {
         this.name = name;
         this.curveFamilies = curveFamilies;
      }

      public CurveFamily GetCurveFamily(string name)
      {
         CurveFamily curveFamily = null;
         for (int i=0; i<curveFamilies.Length; i++)
         {
            curveFamily = curveFamilies[i];
            if (curveFamily.Name.Equals(name))
            {
               break;
            }
         }
         return curveFamily;
      }
   }
}
