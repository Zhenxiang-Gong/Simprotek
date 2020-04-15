using System;
using System.Drawing;

namespace Prosimo.Plots {

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class ChartUtil {

      public static double GetInterpolateValue(PointF[] ps, double x) {
         double retValue = 1.0;
         int length = ps.Length;
         if (x < ps[0].X) {
            retValue = ps[0].Y;
         }
         else if (x > ps[length - 1].X) {
            retValue = ps[length - 1].Y;
         }
         else {
            for (int i = 0; i < length - 1; i++) {
               if (x >= ps[i].X && x <= ps[i + 1].X) {
                  retValue = ps[i].Y + (ps[i + 1].Y - ps[i].Y) / (ps[i + 1].X - ps[i].X) * (x - ps[i].X);
                  break;
               }
            }
         }
         return retValue;
      }

      public static double GetInterpolatedValue(CurveF[] cs, double x, double y) {
         double x1, x2, y1, y2;
         double retValue = 0.0;
         if (x < cs[0].Value) {
            retValue = GetInterpolateValue(cs[0].Data, y);
         }
         else if (x > cs[cs.Length - 1].Value) {
            retValue = GetInterpolateValue(cs[cs.Length - 1].Data, y);
         }
         else {
            for (int i = 0; i < cs.Length - 1; i++) {
               x1 = cs[i].Value;
               x2 = cs[i + 1].Value;
               if (x >= x1 && x <= x2) {
                  y1 = GetInterpolateValue(cs[i].Data, y);
                  y2 = GetInterpolateValue(cs[i + 1].Data, y);
                  retValue = y1 + (y2 - y1) / (x2 - x1) * (x - x1);
                  break;
               }
            }
         }
         return retValue;
      }
   }
}
