using System;
using System.Drawing;

using Prosimo.Plots;

namespace Prosimo.UnitOperations.HeatTransfer 
{
   /// <summary>
   /// Summary description for DuklerHeatTransferCoeffCalculator.
   /// </summary>
   public class DuklerHeatTransferCoeffCalculator {

      private static readonly CurveF[] duklerCurves = new CurveF[6];
      private static readonly CurveF[] duklerCorrectionCurves = new CurveF[6];

      static DuklerHeatTransferCoeffCalculator() {
         //Perry's Chemical Engineers Handbook, Page 5-32, Fig 5-10
         //curve 1 - 0.1:  (200, 0.26), (400, 0.204), (600, 0.173),  (1000, 0.11), (2000, 0.0575), (3000, 0.036), (5000, 0.04), (10000, 0.026), (20000, 0.025), (30000, 0.026), (100000, 0.041) 
         //curve 2 - 0.5:  (200, 0.265), (400, 0.205), (600, 0.175),  (1000, 0.165), (2000, 0.13), (3000, 0.12), (5000, 0.11), (10000, 0.105), (20000, 0.11), (30000, 0.12), (100000, 0.175)
         //curve 3 - 1.0:  (200, 0.27), (400, 0.206), (600, 0.20),  (1000, 0.185), (2000, 0.182), (3000, 0.175), (5000, 0.18), (10000, 0.183), (20000, 0.19), (30000, 0.2), (100000, 0.24)
         //curve 4 - 2.0:  (200, 0.28), (400, 0.23), (600, 0.22), (1000, 0.215),  (2000, 0.215), (3000, 0.22), (5000, 0.23), (10000, 0.27), (20000, 0.29), (30000, 0.31), (100000, 0.4)
         //curve 5 - 5.0:  (200, 0.3), (400, 0.295), (600, 0.29), (1000, 0.285), (2000, 0.30), (3000, 0.305), (5000, 0.335), (10000, 0.395), (20000, 0.43), (30000, 0.48), (100000, 0.52)
         //curve 6 - 10.0: (200, 0.365), (400, 0.32), (600, 0.33), (1000, 0.35), (2000, 0.38), (3000, 0.4), (5000, 0.42), (10000, 0.5), (20000, 0.6), (30000, 0.64), (100000, 0.91)
         
         float log200 = (float)Math.Log10(200), log400 = (float)Math.Log10(400), log600 = (float)Math.Log10(600), log1000 = (float)Math.Log10(1000), log2000 = (float)Math.Log10(2000), log3000 = (float)Math.Log10(3000), log5000 = (float)Math.Log10(5000), log10000 = (float)Math.Log10(10000), log20000 = (float)Math.Log10(20000), log30000 = (float)Math.Log10(30000), log100000 = (float)Math.Log10(100000);
         PointF[] ps0 = {new PointF(log200, (float)Math.Log10(0.26)), new PointF(log400, (float)Math.Log10(0.204)), new PointF(log600, (float)Math.Log10(0.173)),  new PointF(log1000, (float)Math.Log10(0.11)), new PointF(log2000, (float)Math.Log10(0.0575)), new PointF(log3000, (float)Math.Log10(0.036)), new PointF(log5000, (float)Math.Log10(0.04)), new PointF(log10000, (float)Math.Log10(0.026)), new PointF(log20000, (float)Math.Log10(0.025)), new PointF(log30000, (float)Math.Log10(0.026)), new PointF(log100000, (float)Math.Log10(0.041))};
         PointF[] ps1 = {new PointF(log200, (float)Math.Log10(0.265)), new PointF(log400, (float)Math.Log10(0.205)), new PointF(log600, (float)Math.Log10(0.175)),  new PointF(log1000, (float)Math.Log10(0.165)), new PointF(log2000, (float)Math.Log10(0.13)), new PointF(log3000, (float)Math.Log10(0.12)), new PointF(log5000, (float)Math.Log10(0.11)), new PointF(log10000, (float)Math.Log10(0.105)), new PointF(log20000, (float)Math.Log10(0.11)), new PointF(log30000, (float)Math.Log10(0.12)), new PointF(log100000, (float)Math.Log10(0.175))};
         PointF[] ps2 = {new PointF(log200, (float)Math.Log10(0.27)), new PointF(log400, (float)Math.Log10(0.206)), new PointF(log600, (float)Math.Log10(0.20)),  new PointF(log1000, (float)Math.Log10(0.185)), new PointF(log2000, (float)Math.Log10(0.182)), new PointF(log3000, (float)Math.Log10(0.175)), new PointF(log5000, (float)Math.Log10(0.18)), new PointF(log10000, (float)Math.Log10(0.183)), new PointF(log20000, (float)Math.Log10(0.19)), new PointF(log30000, (float)Math.Log10(0.2)), new PointF(log100000, (float)Math.Log10(0.24))};
         PointF[] ps3 = {new PointF(log200, (float)Math.Log10(0.28)), new PointF(log400, (float)Math.Log10(0.23)), new PointF(log600, (float)Math.Log10(0.22)), new PointF(log1000, (float)Math.Log10(0.215)),  new PointF(log2000, (float)Math.Log10(0.215)), new PointF(log3000, (float)Math.Log10(0.22)), new PointF(log5000, (float)Math.Log10(0.23)), new PointF(log10000, (float)Math.Log10(0.27)), new PointF(log20000, (float)Math.Log10(0.29)), new PointF(log30000, (float)Math.Log10(0.31)), new PointF(log100000, (float)Math.Log10(0.4))};
         PointF[] ps4 = {new PointF(log200, (float)Math.Log10(0.3)), new PointF(log400, (float)Math.Log10(0.295)), new PointF(log600, (float)Math.Log10(0.29)), new PointF(log1000, (float)Math.Log10(0.285)), new PointF(log2000, (float)Math.Log10(0.30)), new PointF(log3000, (float)Math.Log10(0.305)), new PointF(log5000, (float)Math.Log10(0.335)), new PointF(log10000, (float)Math.Log10(0.395)), new PointF(log20000, (float)Math.Log10(0.43)), new PointF(log30000, (float)Math.Log10(0.48)), new PointF(log100000, (float)Math.Log10(0.52))};
         PointF[] ps5 = {new PointF(log200, (float)Math.Log10(0.365)), new PointF(log400, (float)Math.Log10(0.32)), new PointF(log600, (float)Math.Log10(0.33)), new PointF(log1000, (float)Math.Log10(0.35)), new PointF(log2000, (float)Math.Log10(0.38)), new PointF(log3000, (float)Math.Log10(0.4)), new PointF(log5000, (float)Math.Log10(0.42)), new PointF(log10000, (float)Math.Log10(0.5)), new PointF(log20000, (float)Math.Log10(0.6)), new PointF(log30000, (float)Math.Log10(0.64)), new PointF(log100000, (float)Math.Log10(0.91))};
         duklerCurves[0] = new CurveF(0.1f, ps0);
         duklerCurves[1] = new CurveF(0.5f, ps1);
         duklerCurves[2] = new CurveF(1f, ps2);
         duklerCurves[3] = new CurveF(2f, ps3);
         duklerCurves[4] = new CurveF(5f, ps4);
         duklerCurves[5] = new CurveF(10f, ps5);

         //Handbook of Chemical Engineering Calculations--page 7-50 table (the only table on this page)
         PointF[] pt0 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 1.00f), new PointF(1.0e-4f, 1.03f),  new PointF(2.0e-4f, 1.05f)};
         PointF[] pt1 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 1.03f), new PointF(1.0e-4f, 1.15f),  new PointF(2.0e-4f, 1.28f)};
         PointF[] pt2 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 1.07f), new PointF(1.0e-4f, 1.40f),  new PointF(2.0e-4f, 1.68f)};
         PointF[] pt3 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 1.25f), new PointF(1.0e-4f, 2.25f),  new PointF(2.0e-4f, 2.80f)};
         PointF[] pt4 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 1.90f), new PointF(1.0e-4f, 4.35f),  new PointF(2.0e-4f, 6.00f)};
         PointF[] pt5 = {new PointF(0.0f, 1.0f), new PointF(1.0e-5f, 3.30f), new PointF(1.0e-4f, 8.75f),  new PointF(2.0e-4f, 13.00f)};
         duklerCorrectionCurves[0] = new CurveF(200f, pt0);
         duklerCorrectionCurves[1] = new CurveF(500f, pt1);
         duklerCorrectionCurves[2] = new CurveF(1000f, pt2);
         duklerCorrectionCurves[3] = new CurveF(3000f, pt3);
         duklerCorrectionCurves[4] = new CurveF(10000f, pt4);
         duklerCorrectionCurves[5] = new CurveF(30000f, pt5);
      }
      
      //Perry's Figure 5-10, also see section 7-25 of Handbook of Chemical Engineering Calculations
      //Fist correlation to use to calculate condensing heat transfer coefficient in vertical tubes
      //This correlation can also be used for falling film heat transfer coeeficient. But the result
      //should be multiplied by 0.75
      public static double CalculateVerticalTubeHTC_Dukler(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double vapViscosity, double liqThermalCond, double liqSpecificHeat) {
         double gamma = massFlowRate/(Math.PI*diameter);
         double Re = 4 * gamma/liqViscosity;
         double Pr  = liqSpecificHeat * liqViscosity/liqThermalCond;
         double temp = Math.Pow(liqViscosity*liqViscosity/(liqDensity*liqDensity*liqThermalCond*liqThermalCond*liqThermalCond*9.8065), 1/3);
         double h = ChartUtil.GetInterpolatedValue(duklerCurves, Re, Pr);
         h = h/temp;
         //Handbook of Chemical Engineering Calculations--page 7-50 table (the only table on this page)
         double Ad = 0.25 * Math.Pow(liqViscosity, 1.173) * Math.Pow(vapViscosity, 0.16)/(Math.Pow(9.8065, 2.0/3.0) * diameter * diameter * Math.Pow(liqDensity, 0.553) * Math.Pow(vapDensity, 0.78));
         double correctionCoeff = ChartUtil.GetInterpolatedValue(duklerCorrectionCurves, Re, Ad);
         return correctionCoeff * h;
      }
   }
}

