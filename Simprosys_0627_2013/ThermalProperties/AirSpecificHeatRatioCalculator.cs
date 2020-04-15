using System;
using System.Drawing;

using Prosimo.Plots;

namespace Prosimo.ThermalProperties {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class AirSpecificHeatRatioCalculator {
      /* Perry's
       * TABLE 2-200 Specific Heat Ratio, Cp/Cv, for Air Pressure, bar
      Temperature, K     1     10   20    40    60    80   100   150   200   250   300   400   500   600   800  1000
                  150 1.410 1.510 1.668 2.333 4.120 3.973 3.202 2.507 2.243 2.091 1.988 1.851 1.768 1.712 1.654 1.639
                  200 1.406 1.452 1.505 1.630 1.781 1.943 2.093 2.274 2.236 2.140 2.050 1.920 1.832 1.771 1.682 1.619
                  250 1.403 1.429 1.457 1.517 1.577 1.640 1.699 1.816 1.877 1.896 1.885 1.836 1.782 1.743 1.681 1.636
                  300 1.402 1.418 1.436 1.470 1.505 1.537 1.570 1.640 1.687 1.716 1.730 1.727 1.707 1.683 1.645 1.619
                  350 1.399 1.411 1.422 1.446 1.467 1.488 1.509 1.553 1.589 1.612 1.627 1.640 1.638 1.629 1.605 1.585
                  400 1.395 1.404 1.412 1.429 1.444 1.460 1.472 1.505 1.529 1.548 1.563 1.579 1.584 1.580 1.567 1.555
                  450 1.392 1.397 1.404 1.416 1.428 1.438 1.449 1.471 1.490 1.505 1.518 1.533 1.541 1.542 1.537 1.528
                  500 1.387 1.391 1.395 1.406 1.414 1.421 1.430 1.448 1.463 1.474 1.484 1.499 1.507 1.510 1.510 1.504
                  600 1.377 1.378 1.382 1.386 1.392 1.398 1.403 1.413 1.423 1.432 1.439 1.448 1.457 1.461 1.465 1.466
                  800 1.353 1.355 1.357 1.359 1.361 1.365 1.366 1.372 1.375 1.381 1.384 1.392 1.397 1.401 1.406 1.409
                  1000 1.336 1.337 1.338 1.339 1.342 1.343 1.343 1.345 1.348 1.350 1.354 1.358 1.361 1.365 1.368 1.372
      Calculated from Cp, Cv values of Sychev, V. V., A. A. Vasserman, et al., “Thermodynamic Properties of Air,” Standartov, Moscow, 1978 and Hemisphere, New York,*/

      static readonly CurveF[] ratioCurves = new CurveF[16];

      static AirSpecificHeatRatioCalculator() {
         PointF[] ps0 = { new PointF(150f, 1.410f), new PointF(200f, 1.406f), new PointF(250f, 1.403f), new PointF(300f, 1.402f), new PointF(350f, 1.399f), new PointF(400f, 1.395f), new PointF(450f, 1.392f), new PointF(500f, 1.387f), new PointF(600f, 1.377f), new PointF(800f, 1.353f), new PointF(1000f, 1.336f) };
         PointF[] ps1 = { new PointF(150f, 1.510f), new PointF(200f, 1.452f), new PointF(250f, 1.429f), new PointF(300f, 1.418f), new PointF(350f, 1.411f), new PointF(400f, 1.404f), new PointF(450f, 1.397f), new PointF(500f, 1.391f), new PointF(600f, 1.378f), new PointF(800f, 1.355f), new PointF(1000f, 1.337f) };
         PointF[] ps2 = { new PointF(150f, 1.668f), new PointF(200f, 1.505f), new PointF(250f, 1.457f), new PointF(300f, 1.436f), new PointF(350f, 1.422f), new PointF(400f, 1.412f), new PointF(450f, 1.404f), new PointF(500f, 1.395f), new PointF(600f, 1.382f), new PointF(800f, 1.357f), new PointF(1000f, 1.338f) };
         PointF[] ps3 = { new PointF(150f, 2.333f), new PointF(200f, 1.630f), new PointF(250f, 1.517f), new PointF(300f, 1.470f), new PointF(350f, 1.446f), new PointF(400f, 1.429f), new PointF(450f, 1.416f), new PointF(500f, 1.406f), new PointF(600f, 1.386f), new PointF(800f, 1.359f), new PointF(1000f, 1.339f) };
         PointF[] ps4 = { new PointF(150f, 4.120f), new PointF(200f, 1.781f), new PointF(250f, 1.577f), new PointF(300f, 1.505f), new PointF(350f, 1.467f), new PointF(400f, 1.444f), new PointF(450f, 1.428f), new PointF(500f, 1.414f), new PointF(600f, 1.392f), new PointF(800f, 1.361f), new PointF(1000f, 1.342f) };
         PointF[] ps5 = { new PointF(150f, 3.973f), new PointF(200f, 1.943f), new PointF(250f, 1.640f), new PointF(300f, 1.537f), new PointF(350f, 1.488f), new PointF(400f, 1.460f), new PointF(450f, 1.438f), new PointF(500f, 1.421f), new PointF(600f, 1.398f), new PointF(800f, 1.365f), new PointF(1000f, 1.343f) };
         PointF[] ps6 = { new PointF(150f, 3.202f), new PointF(200f, 2.093f), new PointF(250f, 1.699f), new PointF(300f, 1.570f), new PointF(350f, 1.509f), new PointF(400f, 1.472f), new PointF(450f, 1.449f), new PointF(500f, 1.430f), new PointF(600f, 1.403f), new PointF(800f, 1.366f), new PointF(1000f, 1.343f) };
         PointF[] ps7 = { new PointF(150f, 2.507f), new PointF(200f, 2.274f), new PointF(250f, 1.816f), new PointF(300f, 1.640f), new PointF(350f, 1.553f), new PointF(400f, 1.505f), new PointF(450f, 1.471f), new PointF(500f, 1.448f), new PointF(600f, 1.413f), new PointF(800f, 1.372f), new PointF(1000f, 1.345f) };
         PointF[] ps8 = { new PointF(150f, 2.243f), new PointF(200f, 2.236f), new PointF(250f, 1.877f), new PointF(300f, 1.687f), new PointF(350f, 1.589f), new PointF(400f, 1.529f), new PointF(450f, 1.490f), new PointF(500f, 1.463f), new PointF(600f, 1.423f), new PointF(800f, 1.375f), new PointF(1000f, 1.348f) };
         PointF[] ps9 = { new PointF(150f, 2.091f), new PointF(200f, 2.140f), new PointF(250f, 1.896f), new PointF(300f, 1.716f), new PointF(350f, 1.612f), new PointF(400f, 1.548f), new PointF(450f, 1.505f), new PointF(500f, 1.474f), new PointF(600f, 1.432f), new PointF(800f, 1.381f), new PointF(1000f, 1.350f) };
         PointF[] ps10 = { new PointF(150f, 1.988f), new PointF(200f, 2.050f), new PointF(250f, 1.885f), new PointF(300f, 1.730f), new PointF(350f, 1.627f), new PointF(400f, 1.563f), new PointF(450f, 1.518f), new PointF(500f, 1.484f), new PointF(600f, 1.439f), new PointF(800f, 1.384f), new PointF(1000f, 1.354f) };
         PointF[] ps11 = { new PointF(150f, 1.851f), new PointF(200f, 1.920f), new PointF(250f, 1.836f), new PointF(300f, 1.727f), new PointF(350f, 1.640f), new PointF(400f, 1.579f), new PointF(450f, 1.533f), new PointF(500f, 1.499f), new PointF(600f, 1.448f), new PointF(800f, 1.392f), new PointF(1000f, 1.358f) };
         PointF[] ps12 = { new PointF(150f, 1.768f), new PointF(200f, 1.832f), new PointF(250f, 1.782f), new PointF(300f, 1.707f), new PointF(350f, 1.638f), new PointF(400f, 1.584f), new PointF(450f, 1.541f), new PointF(500f, 1.507f), new PointF(600f, 1.457f), new PointF(800f, 1.397f), new PointF(1000f, 1.361f) };
         PointF[] ps13 = { new PointF(150f, 1.712f), new PointF(200f, 1.771f), new PointF(250f, 1.743f), new PointF(300f, 1.683f), new PointF(350f, 1.629f), new PointF(400f, 1.580f), new PointF(450f, 1.542f), new PointF(500f, 1.510f), new PointF(600f, 1.461f), new PointF(800f, 1.401f), new PointF(1000f, 1.365f) };
         PointF[] ps14 = { new PointF(150f, 1.654f), new PointF(200f, 1.682f), new PointF(250f, 1.681f), new PointF(300f, 1.645f), new PointF(350f, 1.605f), new PointF(400f, 1.567f), new PointF(450f, 1.537f), new PointF(500f, 1.510f), new PointF(600f, 1.465f), new PointF(800f, 1.406f), new PointF(1000f, 1.368f) };
         PointF[] ps15 = { new PointF(150f, 1.639f), new PointF(200f, 1.619f), new PointF(250f, 1.636f), new PointF(300f, 1.619f), new PointF(350f, 1.585f), new PointF(400f, 1.555f), new PointF(450f, 1.528f), new PointF(500f, 1.504f), new PointF(600f, 1.466f), new PointF(800f, 1.409f), new PointF(1000f, 1.372f) };

         ratioCurves[0] = new CurveF(1.0e5f, ps0);
         ratioCurves[1] = new CurveF(10.0e5f, ps1);
         ratioCurves[2] = new CurveF(20.0e5f, ps2);
         ratioCurves[0] = new CurveF(40.0e5f, ps3);
         ratioCurves[0] = new CurveF(60.0e5f, ps4);
         ratioCurves[0] = new CurveF(80.0e5f, ps5);
         ratioCurves[0] = new CurveF(100.0e5f, ps6);
         ratioCurves[0] = new CurveF(150.0e5f, ps7);
         ratioCurves[0] = new CurveF(200.0e5f, ps8);
         ratioCurves[0] = new CurveF(250.0e5f, ps9);
         ratioCurves[0] = new CurveF(300e5f, ps10);
         ratioCurves[0] = new CurveF(400.0e5f, ps11);
         ratioCurves[0] = new CurveF(500.0e5f, ps12);
         ratioCurves[0] = new CurveF(600.0e5f, ps13);
         ratioCurves[0] = new CurveF(800.0e5f, ps14);
         ratioCurves[0] = new CurveF(1000.0e5f, ps15);
      }

      public static double GetSpecificHeatRatio(double t, double p) {
         return ChartUtil.GetInterpolatedValue(ratioCurves, p, t);
      }
   }
}
