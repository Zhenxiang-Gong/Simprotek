using System;
using Prosimo.UnitSystems;

namespace Prosimo.ThermalProperties {

   public struct SubstanceStatesAndProperties {
      public double pressure;
      public double temperature;
      public double vaporFraction;
      public double specificVolume;
      public double density;
      public double enthalpy;
      public double entropy;
      public double viscosity;
      public double thermalConductivity;
      public double specificHeat;
      public double surfaceTension;
      public double degreesOfSuperheat;
      public double degreesOfSubcooling;
   }

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class SteamTable {

      private const double ALPHA0 = 0.0;
      private const double ALPHA1 = 0.0;
      private const double PCA = 3208.23474;
      private const double VCA = 0.0507785287;
      private const double TCA = 1165.14;
      private const double TZA = 459.67;
      private const double PVO1O = 30.1463451;
      private const double PVOT = 0.0258735819;
      private const double I1 = 4.260321148;
      private const double T1 = 662.0;
      private const double TC = 705.47;
      private const double P1 = 2398.21683;
      private const double PMIN = 0.08865;  //about 611.22 Pa
      private const double PMAX = 6000.0;
      private const double P3MIN = 2002.78;
      private const double V3MIN = 0.0207;
      private const double V3MAX = 0.143;
      private const double TMIN = 32.018; //about 0.018 degree C
      private const double TMAX = 1600.0;
      private const double T1MAX = 682.0;
      private const double T2MIN = -200.0;
      private const double T3MIN = 636.0;
      private const double T3MAX = 1124.45;
      private const double HMIN = 0.0;
      private const double HMAX = 1860.0;
      private const double SMIN = 0.0;
      private const double SMAX = 3.0;
      private const double XMIN = 0.0;
      private const double XMAX = 100.0;
      private const double VMIN = 0.016;
      private const double VMAX = 3400.0;
      private const double AL0 = 15.74373327;
      private const double AL1 = -34.17061978;
      private const double AL2 = 19.31380707;
      private const double S4MAX = 1.065;
      private const double H4MAX = 906.1;

      private const double AK1 = -7.691234564;
      private const double AK2 = -26.08023696;
      private const double AK3 = -168.1706546;
      private const double AK4 = 64.23285504;
      private const double AK5 = -118.9646225;
      private const double AK6 = 4.167117320;
      private const double AK7 = 20.97506760;
      private const double AK8 = 1.0e9;
      private const double AK9 = 6.0;
      private const double AK2T2 = -52.16047392;      /*  ak2 times 2  */
      private const double AK3T3 = -504.5119638;      /*  ak3 times 3  */
      private const double AK4T4 = 256.93142016;      /*  ak4 times 4  */
      private const double AK5T5 = -594.8231125;      /*  ak5 times 5  */
      private const double AK7T2 = 41.9501352;        /*  ak7 times 2  */
      private const double AK8T2 = 2.0e9;             /*  ak8 times 2  */

      private const double AA0 = 6.824687741e3;
      private const double AA1 = -5.422063673e2;
      private const double AA3 = 3.941286787e4;
      private const double AA11 = 7.982692717;
      private const double AA12 = -2.616571843e-2;
      private const double AA14 = 2.284279054e-2;
      private const double AA15 = 2.421647003e2;
      private const double AA16 = 1.269716088e-10;
      private const double AA20 = 1.293441934e1;
      private const double AA21 = 1.308119072e-5;
      private const double A1 = 8.438375405e-1;
      private const double A4 = 7.342278489e-2;
      private const double AA8 = -4.511168742e4;
      private const double A6 = 6.5371543e-1;
      private const double AA9 = 1.418138926e4;
      private const double A7 = 1.15e-6;
      private const double AA10 = -2.017271113e3;
      private const double A8 = 1.5108e-5;
      private const double AA17 = 2.074838328e-7;
      private const double A12 = 2.04e-1;
      private const double AA18 = 2.17402035e-8;
      private const double AA19 = 1.105710498e-9;
      private const double A10 = 7.002753165;
      private const double A11 = 2.995284926e-4;
      private const double A3 = 1.72;
      private const double A5 = 4.97585887e-2;
      private const double A2 = 5.362162162e-4;
      private const double AA5 = 9.902381028e4;
      private const double AA2 = -2.096666205e4;
      private const double AA4 = -6.733277739e4;
      private const double AA13 = 1.52241179e-3;
      private const double AA6 = -1.093911774e5;
      private const double A9 = 1.4188e-1;
      private const double AA7 = 8.590841667e4;
      private const double AA22 = 6.047626338e-14;
      private const double A2T6 = 32.172972972e-4;          /*    a2 times 6          */
      private const double A1T2 = 16.876750810e-1;          /*    a1 times 2          */
      private const double M5D17 = -0.29411765;              /*    -5 divided by 17    */
      private const double A4T2 = 14.684556978e-2;          /*    a4 times 2          */
      private const double A5T2 = 9.95171774e-2;            /*    a5 times 2          */
      private const double AA22T20 = 120.952526760e-14;        /*    aa22 times 20, s    */
      private const double A9T18 = 25.5384e-1;               /*    a9 times 18, s      */
      private const double AA14T2 = 4.568558108e-2;           /*    aa14 times 2, s     */
      private const double AA15T10 = 24.21647003e2;            /*    aa15 times 10, s    */
      private const double AA16T19 = 24.124605672e-10;         /*    aa16 times 19, s    */
      private const double P5D12 = 0.41666667;               /*    5 divided by 12, s  */
      private const double A3M1 = 0.72;                     /*    a3 minus 1, s       */
      private const double AA3T2 = 7.882573574e4;            /*    aa3 times 2, s      */
      private const double AA4T3 = -20.199833217e4;          /*    aa4 times 3, s      */
      private const double AA5T4 = 39.609524112e4;           /*    aa5 times 4, s      */
      private const double AA6T5 = -5.469558870e5;           /*    aa6 times 5, s      */
      private const double AA7T6 = 51.545050002e4;           /*    aa7 times 6, s      */
      private const double AA8T7 = -31.578181194e4;          /*    aa8 times 7, s      */
      private const double AA9T8 = 11.345111408e4;           /*    aa9 times 8, s      */
      private const double AA10T9 = -18.155440017e3;          /*    aa10 times 9, s     */
      private const double AA21TA12 = 0.266856290688e-5;        /*    aa21 times a12      */
      private const double AA22T21 = 127.000153098e-14;        /*    aa22 times 21       */
      private const double A9T17 = 24.1196e-1;               /*    a9 times 17         */
      private const double P17D29 = 0.58620690;               /*    17 divided by 29    */
      private const double P17D12 = 1.41666667;               /*    17 divided by 12    */
      private const double AA4T2 = -13.466555478e4;          /*    aa4 times 2         */
      private const double AA5T3 = 29.707143084e4;           /*    aa5 times 3         */
      private const double AA6T4 = -4.375647096e5;           /*    aa6 times 4         */
      private const double AA7T5 = 42.954208335e4;           /*    aa7 times 5         */
      private const double AA8T6 = -27.067012452e4;          /*    aa8 times 6         */
      private const double AA9T7 = 9.926972482e4;            /*    aa9 times 7         */
      private const double AA10T8 = -16.138168904e3;          /*    aa10 times 8        */
      private const double AA21T3 = 3.924357216e-5;           /*    a21 times 3         */
      private const double AA22T4 = 24.190505352e-14;         /*    a22 times 4         */
      private const double AA18T2 = 4.34804070e-8;            /*    aa18 times 2        */
      private const double AA19T3 = 3.317131494e-9;           /*    aa19 times 3        */
      private const double AA11TA5 = 39720752362.3688e-11;     /*    aa11 times a5       */

      private const double B00 = 7.6333333333e-1;
      private const double B61 = 4.006073948e-1;
      private const double B71 = 8.636081627e-2;
      private const double B81 = -8.532322921e-1;
      private const double B82 = 3.460208861e-1;
      private const double BB11 = 6.670375918e-2;
      private const double BB12 = 1.388983801;
      private const double BB21 = 8.390104328e-2;
      private const double BB22 = 2.614670893e-2;
      private const double BB23 = -3.373439453e-2;
      private const double BB31 = 4.520918904e-1;
      private const double BB32 = 1.069036614e-1;
      private const double BB41 = -5.975336707e-1;
      private const double BB42 = -8.847535804e-2;
      private const double BB51 = 5.958051609e-1;
      private const double BB52 = -5.159303373e-1;
      private const double BB53 = 2.075021122e-1;
      private const double BB62 = -9.867174132e-2;
      private const double BB61 = 1.190610271e-1;
      private const double BB71 = 1.683998803e-1;
      private const double BB72 = -5.809438001e-2;
      private const double BB81 = 6.552390126e-3;
      private const double BB82 = 5.710218649e-4;
      private const double BB90 = 1.936587558e2;
      private const double BB91 = -1.388522425e3;
      private const double BB92 = 4.126607219e3;
      private const double BB93 = -6.508211677e3;
      private const double BB94 = 5.745984054e3;
      private const double BB95 = -2.693088365e3;
      private const double BB96 = 5.235718623e2;
      private const double BB00 = 1.683599274e1;
      private const double BB05 = 8.565182058e-2;
      private const double BB02 = -5.438923329e1;
      private const double BB04 = -6.547711697e-1;
      private const double BB01 = 2.856067796e1;
      private const double BB03 = 4.330662834e-1;
      private const double B61T14 = 56.085035272e-1;          /*  b61 times 14          */
      private const double B71T19 = 164.085550913e-2;         /*  b71 times 19          */
      private const double B81T2T27 = -460.745437734e-1;        /*  b81 times 2 times 27  */
      private const double B82T27 = 93.425639247e-1;          /*  b82 times 27          */
      private const double AL1T10 = -34.17061978e1;           /*  al1 times 10          */
      private const double AL2T2T10 = 38.62761414e1;            /*  al2 times 2 times 10  */
      private const double BB03T2 = 8.661325668e-1;           /*  bbo3 times 2          */
      private const double BB04T3 = -19.643135091e-1;         /*  bb04 times 3          */
      private const double BB05T4 = 34.260728232e-2;          /*  bb05 times 4          */
      private const double BB11T13 = 86.714886934e-2;          /*  bb11 times 13         */
      private const double BB12T3 = 4.166951403;              /*  bb12 times 3          */
      private const double BB21T18 = 151.021877904e-2;         /*  bb21 times 18         */
      private const double BB22T2 = 5.229341786e-2;           /*  bb22 times 2          */
      private const double BB31T18 = 81.376540272e-1;          /*  bb31 times 18         */
      private const double BB32T10 = 10.69036614e-1;           /*  bb32 times 10         */
      private const double BB41T25 = -149.383417675e-1;        /*  bb41 times 25         */
      private const double BB42T14 = -123.865501256e-2;        /*  bb42 times 14         */
      private const double BB51T32 = 1.906576515e1;            /*  bb51 times 32         */
      private const double BB52T28 = -1.444604944e1;           /*  bb52 times 28         */
      private const double BB53T24 = 4.980050693;              /*  bb53 times 24         */
      private const double B00T2 = 1.526666666667;           /*  b00 times 2           */
      private const double B00T3 = 2.29;                     /*  b00 times 3           */
      private const double B00T4 = 3.053333333333;           /*  b00 times 4           */
      private const double B00T5 = 3.816666666667;           /*  b00 times 5           */
      private const double B00T6 = 4.58;                     /*  b00 times 6           */
      private const double BB04T2 = -1.309542339;             /*  bb04 times 2          */
      private const double BB05T3 = 2.569554617e-1;           /*  bb05 times 3          */
      private const double BB21T2 = 16.780208656e-2;          /*  bb21 times 2          */
      private const double BB23T2 = -6.746878906e-2;          /*  bb23 times 2          */
      private const double BB31T3 = 13.562756712e-1;          /*  bb31 times 3          */
      private const double BB32T3 = 3.207109842e-1;           /*  bb32 times 3          */
      private const double BB41T4 = -23.901346828e-1;         /*  bb41 times 4          */
      private const double BB42T4 = -35.390143216e-2;         /*  bb42 times 4          */
      private const double BB51T5 = 29.790258045e-1;          /*  bb51 times 5          */
      private const double BB52T5 = -25.796516865e-1;         /*  bb52 times 5          */
      private const double BB53T5 = 10.375105610e-1;          /*  bb53 times 5          */

      private const double C00 = -6.8399;
      private const double C50 = 2.10636332e2;
      private const double C11 = 7.08636085e-1;
      private const double C12 = 1.23679455e1;
      private const double C13 = -1.20389004e1;
      private const double C14 = 5.40437422;
      private const double C15 = -9.93865043e-1;
      private const double C16 = 6.27523182e-2;
      private const double C17 = -7.74743016;
      private const double C22 = 4.31430538e1;
      private const double C23 = -1.41619313e1;
      private const double C24 = 4.04172459;
      private const double C25 = 1.55546326;
      private const double C26 = -1.66568935;
      private const double C27 = 3.24881158e-1;
      private const double C28 = 2.93655325e1;
      private const double C32 = 8.08859747e1;
      private const double C33 = -8.3615338e1;
      private const double C34 = 3.58636517e1;
      private const double C35 = 7.51895954;
      private const double C36 = -1.2616064e1;
      private const double C37 = 1.09717462;
      private const double C38 = 2.12145492;
      private const double C39 = -5.46529566e-1;
      private const double C310 = 8.32875413;
      private const double C62 = 3.69707142e-1;
      private const double C64 = 6.828087013e-2;
      private const double C70 = -2.571600553e2;
      private const double C71 = -1.518783715e2;
      private const double C72 = 2.220723208e1;
      private const double C73 = -1.80203957e2;
      private const double C74 = 2.35709622e3;
      private const double C75 = -1.462335698e4;
      private const double C76 = 4.54291663e4;
      private const double C77 = -7.053556432e4;
      private const double C78 = 4.381571428e4;
      private const double D30 = -1.717616747;
      private const double D31 = 3.526389875;
      private const double D32 = -2.690899373;
      private const double D33 = 9.070982605e-1;
      private const double D34 = -1.138791156e-1;
      private const double D40 = 1.301023613;
      private const double D41 = -2.642777743;
      private const double D42 = 1.996765362;
      private const double D43 = -6.661557013e-1;
      private const double D44 = 8.270860589e-2;
      private const double D50 = 3.426663535e-4;
      private const double D51 = -1.2365212578e-3;
      private const double D52 = 1.155018309e-3;
      private const double C21 = -4.29885092;
      private const double C31 = 7.94841842e-6;
      private const double C40 = 2.75971776e-6;
      private const double C41 = -5.09073985e-4;
      private const double C60 = 5.528935335e-2;
      private const double C03 = 4.20460752;
      private const double C61 = -2.336365955e-1;
      private const double C04 = -2.76807038;
      private const double C63 = -2.59641547e-1;
      private const double C05 = 2.10419707;
      private const double C012 = -4.311577033;
      private const double C06 = -1.14649588;
      private const double C01 = -1.7226042e-2;
      private const double C07 = 2.23138085e-1;
      private const double C02 = -7.77175039;
      private const double C08 = 1.16250363e-1;
      private const double C09 = -8.20900544e-2;
      private const double C010 = 1.94129239e-2;
      private const double C011 = -1.69470576e-3;
      private const double C60T2 = 1.105787067e-1;        /*  c60 times 2              */
      private const double C61T3 = -7.009097865e-1;       /*  c61 times 3              */
      private const double C62T4 = 1.478828568;           /*  c62 times 4              */
      private const double C63T5 = -1.298207735;          /*  c63 times 5              */
      private const double C64T6 = 4.096852208e-1;        /*  c64 times 6              */
      private const double C71T2 = -3.03756743e2;         /*  c71 times 2              */
      private const double C72T3 = 6.662169624e1;         /*  c72 times 3              */
      private const double C73T4 = -7.20815828e2;         /*  c73 times 4              */
      private const double C74T5 = 1.17854811e4;          /*  c74 times 5              */
      private const double C75T6 = -8.774014188e4;        /*  c75 times 6              */
      private const double C76T7 = 3.180041641e5;         /*  c76 times 7              */
      private const double C77T8 = -5.642845147e5;        /*  c77 times 8              */
      private const double C78T9 = 3.943414285e5;         /*  c78 times 9              */
      private const double C00MC012MC50 = -213.16465497;  /* c00 minus c012 minus c50, h */
      private const double C02T2 = -1.554350078e1;        /*  c02 times 2,h            */
      private const double C03T3 = 1.261382256e1;         /*  c03 times 3,h            */
      private const double C04T4 = -1.107228152e1;        /*  c04 times 4, h           */
      private const double C05T5 = 1.052098535e1;         /*  c05 times 5, h           */
      private const double C06T6 = -6.87897528;           /*  c06 times 6, h           */
      private const double C07T7 = 1.561966595;           /*  c07 times 7, h           */
      private const double C08T8 = 9.30002904e-1;         /*  c08 times 8, h           */
      private const double C09T9 = -7.388104896e-1;       /*  c09 times 9, h           */
      private const double C010T10 = 1.94129239e-1;         /*  c010 times 10, h         */
      private const double C011T11 = -1.864176336e-2;       /*  c011 times 11, h         */
      private const double C012MC17 = 3.43585313;            /*  c012 minus c17, h        */
      private const double MC17MC50 = -202.88890184;         /*  minus c17 minus c50, h   */
      private const double C21T2 = -8.59770184;           /*  c21 times 2, h           */
      private const double C13T2 = -2.40778008e1;         /*  c13 times 2, h           */
      private const double C14T3 = 1.621312266e1;         /*  c14 times 3, h           */
      private const double C15T4 = -3.975460172;          /*  c15 times 4, h           */
      private const double C16T5 = 3.13761591e-1;         /*  c16 times 5, h           */
      private const double C28T2 = 5.8731065e1;           /*  c28 times 2, h           */
      private const double C21T2PC31T3 = -8.59767799; /* c21 times 2 plus c31 times 3, h */
      private const double C24T2 = 8.08344918;            /*  c24 times 2, h           */
      private const double C25T3 = 4.66638978;            /*  c25 times 3, h           */
      private const double C26T4 = -6.6627574;            /*  c26 times 4, h           */
      private const double C27T5 = 1.62440579;            /*  c27 times 5, h           */
      private const double C28PC310T3 = 54.35179489;     /*  c28 plus (c310 times 3), h  */
      private const double C31T3 = 2.384525526e-5;        /*  c31 times 3, h           */
      private const double C35T2 = 1.503791908e1;         /*  c35 times 2, h           */
      private const double C36T3 = -3.7848192e1;          /*  c36 times 3,  h          */
      private const double C37T4 = 4.38869848;            /*  c37 times 4, h           */
      private const double C38T5 = 1.06072746e1;          /*  c38 times 5, h           */
      private const double C39T6 = -3.279177396;          /*  c39 times 6, h           */
      private const double C310T2 = 1.665750826e1;         /*  c310 times 2, h          */
      private const double C40T23 = 6.347350848e-5;        /*  c40 times 23, h          */
      private const double C41T28 = -1.425407158e-2;       /*  c41 times 28, h          */
      private const double C40T24 = 6.623322624e-5;        /*  c40 times 24, h          */
      private const double C41T29 = -1.476314557e-2;       /*  c41 times 29, h          */
      private const double C60T3 = 1.6586806e-1;          /*  c60 times 3, h           */
      private const double C61T2 = -4.67273191e-1;        /*  c61 times 2, h           */
      private const double C03T2 = 8.40921504;            /*  c03 times 2              */
      private const double C04T3 = -8.30421114;           /*  c04 times 3              */
      private const double C05T4 = 8.41678828;            /*  c05 times 4              */
      private const double C06T5 = -5.7324794;            /*  c06 times 5              */
      private const double C07T6 = 1.33882851;            /*  c07 times 6              */
      private const double C08T7 = 8.13752541e-1;         /*  c08 times 7              */
      private const double C09T8 = -6.567204352e-1;       /*  c09 times 8              */
      private const double C010T9 = 1.747163151e-1;        /*  c010 times 9             */
      private const double C011T10 = -1.69470576e-2;        /*  c011 times 10            */
      private const double C23T2 = -2.83238626e1;         /*  c23 times 2              */
      private const double C24T3 = 1.212517377e1;         /*  c24 times 3              */
      private const double C25T4 = 6.22185304;            /*  c25 times 4              */
      private const double C26T5 = -8.32844675;           /*  c26 times 5              */
      private const double C27T6 = 1.949286948;           /*  c27 times 6              */
      private const double C33T2 = -1.67230676e2;         /*  c33 times 2              */
      private const double C34T3 = 1.075909551e2;         /*  c34 times 3              */
      private const double C35T4 = 3.007583816e1;         /*  c35 times 4              */
      private const double C36T5 = -6.308032e1;           /*  c36 times 5              */
      private const double C37T6 = 6.58304772;            /*  c37 times 6              */
      private const double C38T7 = 1.485018444e1;         /*  c38 times 7              */
      private const double C39T8 = -4.372236528;          /*  c39 times 8              */
      private const double C41T5 = -2.545369925e-3;       /*  c41 times 5              */
      private const double D32T2 = -5.381798746;          /*  d32 times 2              */
      private const double D33T3 = 2.721294782;           /*  d33 times 3              */
      private const double D34T4 = -4.555164624e-1;       /*  d34 times 4              */
      private const double D42T2 = 3.993530724;           /*  d42 times 2              */
      private const double D43T3 = -1.998467104;          /*  d43 times 3              */
      private const double D44T4 = 3.308344236e-1;        /*  d44 times 4              */
      private const double D52T2 = 2.310036618e-3;        /*  d52 times 2              */

      private const double tolerance = 1.0e-5;

      private static double[] b1d = {1.52264682686, -0.682309517937, 0.164114951723, -2.02321648831e-3, 1.92391110748e-3,
                      -5.74549418696e-4, 6.84115542402e-5, 3.36500068426e-5, -1.23422483951e-5, 
                      1.48266501702e-6, -1.02116445578e-6,   -4.09080904092e-6};

      private static double[,] b2d = {{0.501938,   0.235622,    -0.274637,   0.145831,   -0.0270448}, { 
                               0.162888,   0.789393,    -0.743539,   0.263129,   -0.0253093}, { 
                               -0.130356,  0.673665,    -0.959456,   0.347247,   -0.0267758}, {
                               0.907919,   1.207552,    -0.687343,   0.213486,   -0.0822904}, {
                               -0.551119,  0.0670665,   -0.497089,   0.100754,   0.0602253}, { 
                               0.146543,   -0.0843370,  0.195286,    -0.032932,  -0.0202595}};
      private static double[] a = { -2.083333333, 4, -3, 1.33333333, -0.25, -0.83333333, 1.5, -0.5, 0.083333333, -0.6666667 };

      private static double vptx3_save_0, vptx3_save_100;
      //int     done, enthalpy_done, entropy_done, weight_done;
      //private double   dpdt = 0.0;

      /*  these are initialized because not all error routines use these,
             set the ERROR_MESSAGE pointers to the local global error message
             strings
         */

      //MAJOR_ERROR = false;

      /*  initialze the local variables  */

      bool done = false;
      bool enthalpy_done = false;
      bool entropy_done = false;
      bool weight_done = false;
      bool VPTX3_ALREADY_SET_0 = false;
      bool VPTX3_ALREADY_SET_100 = false;
      //int VPTX3_CALL = 0;

      private static SteamTable self = new SteamTable();

      private SteamTable() {
         //
         // TODO: Add constructor logic here
         //
      }

      public static SteamTable GetInstance() {
         return self;
      }

      public SubstanceStatesAndProperties GetPropertiesFromPressureAndTemperature(double pressure, double temperature) {
         temperature = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         pressure = PressureUnit.Instance.ConvertFromSIValue <PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);
         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }
         if (temperature > 0 && temperature < TMIN) {
            temperature = TMIN;
         }

         double saturation_temperature = 0;
         if (pressure > 0) {
            if (pressure > PCA) {
               saturation_temperature = TC;
            }
            else {
               saturation_temperature = tsl(pressure);
            }
         }

         /*  fix this to cure some problem -- I am not sure why it happens  */
         /*  temperature and pressure available, possibly quality  */
         double quality = x_pt(pressure, temperature);
         double specificVolume = shvptx(pressure, temperature, quality, 3);
         double density = 1 / specificVolume;
         double enthalpy = shvptx(pressure, temperature, quality, 2);
         double entropy = shvptx(pressure, temperature, quality, 1);
         double viscosity = vistv(temperature, 1.0 / specificVolume);

         double degrees_superheat = 0;
         if (quality == 100) {
            degrees_superheat = temperature - saturation_temperature;
         }

         double degrees_subcooling = 0;
         if (quality == 0) {
            degrees_subcooling = saturation_temperature - temperature;
         }

         SubstanceStatesAndProperties waterStatesAndProps = new SubstanceStatesAndProperties();
         waterStatesAndProps.vaporFraction = quality / 100;
         waterStatesAndProps.specificVolume = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(SpecificVolumeUnitType.CubicFootPerPound, specificVolume);
         waterStatesAndProps.density = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(DensityUnitType.PoundPerCubicFoot, 1 / specificVolume);
         waterStatesAndProps.enthalpy = SpecificEnergyUnit.Instance.ConvertToSIValue<SpecificEnergyUnitType>(SpecificEnergyUnitType.BtuPerPound, enthalpy);
         waterStatesAndProps.entropy = SpecificEntropyUnit.Instance.ConvertToSIValue<SpecificEntropyUnitType>(SpecificEntropyUnitType.BtuPerPoundFahrenheit, entropy);
         waterStatesAndProps.viscosity = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(DynamicViscosityUnitType.IbfSecPerSquareFoot, viscosity);
         waterStatesAndProps.degreesOfSuperheat = degrees_superheat / 1.8;
         waterStatesAndProps.degreesOfSubcooling = degrees_subcooling / 1.8;
         return waterStatesAndProps;
      }

      /*pressure and vapor fraction available*/
      public SubstanceStatesAndProperties GetPropertiesFromPressureAndVaporFraction(double pressure, double vaporFraction) {
         if (vaporFraction < 0 || vaporFraction > 1.0) {
            throw new IllegalVarValueException("Vapor fraction vlaue is out of range.");
         }

         pressure = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);
         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }
         double quality = vaporFraction * 100;

         double saturation_temperature = 0;
         if (pressure > 0) {
            if (pressure > PCA) {
               saturation_temperature = TC;
            }
            else {
               saturation_temperature = tsl(pressure);
            }
         }

         double temperature = saturation_temperature;
         double specificVolume = shvptx(pressure, temperature, quality, 3);
         double density = 1 / specificVolume;
         double enthalpy = shvptx(pressure, temperature, quality, 2);
         double entropy = shvptx(pressure, temperature, quality, 1);
         double viscosity = vistv(temperature, 1.0 / specificVolume);

         SubstanceStatesAndProperties waterStatesAndProps = new SubstanceStatesAndProperties();
         waterStatesAndProps.temperature = TemperatureUnit.Instance.ConvertToSIValue(TemperatureUnitType.Fahrenheit, temperature);
         //waterStatesAndProps.vaporFraction = quality/100;
         waterStatesAndProps.specificVolume = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(SpecificVolumeUnitType.CubicFootPerPound, specificVolume);
         waterStatesAndProps.density = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(DensityUnitType.PoundPerCubicFoot, 1 / specificVolume);
         waterStatesAndProps.enthalpy = SpecificEnergyUnit.Instance.ConvertToSIValue<SpecificEnergyUnitType>(SpecificEnergyUnitType.BtuPerPound, enthalpy);
         waterStatesAndProps.entropy = SpecificEntropyUnit.Instance.ConvertToSIValue<SpecificEntropyUnitType>(SpecificEntropyUnitType.BtuPerPoundFahrenheit, entropy);
         waterStatesAndProps.viscosity = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(DynamicViscosityUnitType.IbfSecPerSquareFoot, viscosity);
         //waterStatesAndProps.degreesOfSuperheat = 0;
         //waterStatesAndProps.degreesOfSubcooling = 0;
         return waterStatesAndProps;
      }

      /*  temperature and vapor fraction are available */
      public SubstanceStatesAndProperties GetPropertiesFromTemperatureAndVaporFraction(double temperature, double vaporFraction) {
         if (vaporFraction < 0 || vaporFraction > 1.0) {
            throw new IllegalVarValueException("Vapor fraction vlaue is out of range.");
         }

         temperature = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         if (temperature > 0 && temperature < TMIN) {
            temperature = TMIN;
         }
         double quality = vaporFraction * 100;

         double saturation_pressure = 0;
         if (temperature > 0 && temperature <= TC) {
            double dpdt = 0;
            saturation_pressure = psl(temperature, ref dpdt);
         }

         double pressure = saturation_pressure;
         double specificVolume = shvptx(pressure, temperature, quality, 3);
         double density = 1 / specificVolume;
         double enthalpy = shvptx(pressure, temperature, quality, 2);
         double entropy = shvptx(pressure, temperature, quality, 1);
         double viscosity = vistv(temperature, 1.0 / specificVolume);

         SubstanceStatesAndProperties waterStatesAndProps = new SubstanceStatesAndProperties();
         waterStatesAndProps.pressure = PressureUnit.Instance.ConvertToSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);
         //waterStatesAndProps.vaporFraction = quality/100;
         waterStatesAndProps.specificVolume = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(SpecificVolumeUnitType.CubicFootPerPound, specificVolume);
         waterStatesAndProps.density = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(DensityUnitType.PoundPerCubicFoot, 1 / specificVolume);
         waterStatesAndProps.enthalpy = SpecificEnergyUnit.Instance.ConvertToSIValue<SpecificEnergyUnitType>(SpecificEnergyUnitType.BtuPerPound, enthalpy);
         waterStatesAndProps.entropy = SpecificEntropyUnit.Instance.ConvertToSIValue<SpecificEntropyUnitType>(SpecificEntropyUnitType.BtuPerPoundFahrenheit, entropy);
         waterStatesAndProps.viscosity = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(DynamicViscosityUnitType.IbfSecPerSquareFoot, viscosity);
         //waterStatesAndProps.degreesOfSuperheat = 0;
         //waterStatesAndProps.degreesOfSubcooling = 0;
         return waterStatesAndProps;
      }

      /* pressure and enthalpy available*/
      public SubstanceStatesAndProperties GetPropertiesFromPressureAndEnthalpy (double pressure, double enthalpy) {
         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }

         pressure = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);
         enthalpy = SpecificEnergyUnit.Instance.ConvertFromSIValue<SpecificEnergyUnitType>(SpecificEnergyUnitType.BtuPerPound, enthalpy);
         
         double saturation_temperature = 0;
         if (pressure > 0) {
            if (pressure > PCA) {
               saturation_temperature = TC;
            }
            else {
               saturation_temperature = tsl (pressure);
            }
         }
         
         double quality = xpshv (pressure, enthalpy, 2);
         double temperature = tpshvx (pressure, enthalpy, quality, 2, 1);
         double specificVolume = shvptx (pressure, temperature, quality, 3);
         double density = 1/specificVolume;
         //double enthalpy = shvptx (pressure, temperature, quality, 2);
         double entropy = shvptx (pressure, temperature, quality, 1);
         double viscosity = vistv (temperature, 1.0/specificVolume);
         
         SubstanceStatesAndProperties waterStatesAndProps = new SubstanceStatesAndProperties();
         waterStatesAndProps.temperature = TemperatureUnit.Instance.ConvertToSIValue(TemperatureUnitType.Fahrenheit, temperature);
         waterStatesAndProps.vaporFraction = quality / 100;
         waterStatesAndProps.specificVolume = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(SpecificVolumeUnitType.CubicFootPerPound, specificVolume);
         waterStatesAndProps.density = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(DensityUnitType.PoundPerCubicFoot, 1 / specificVolume);
         //waterStatesAndProps.enthalpy = enthalpy;
         waterStatesAndProps.entropy = SpecificEntropyUnit.Instance.ConvertToSIValue<SpecificEntropyUnitType>(SpecificEntropyUnitType.BtuPerPoundFahrenheit, entropy);
         waterStatesAndProps.viscosity = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(DynamicViscosityUnitType.IbfSecPerSquareFoot, viscosity);
         //waterStatesAndProps.degreesOfSuperheat = 0;
         //waterStatesAndProps.degreesOfSubcooling = 0;

         return waterStatesAndProps;
      }

      public double GetEnthalpyFromPressureAndTemperature(double pressure, double temperature) {
         temperature = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         pressure = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);

         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }
         if (temperature > 0 && temperature < TMIN) {
            temperature = TMIN;
         }

         double quality = x_pt(pressure, temperature);
         double enthalpy = shvptx(pressure, temperature, quality, 2);
         return SpecificEnergyUnit.Instance.ConvertToSIValue<SpecificEnergyUnitType>(SpecificEnergyUnitType.BtuPerPound, enthalpy);
      }

      public double GetDensityFromPressureAndTemperature(double pressure, double temperature) {
         temperature = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         pressure = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);

         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }
         if (temperature > 0 && temperature < TMIN) {
            temperature = TMIN;
         }

         /*  temperature and pressure available, possibly quality  */
         double quality = x_pt(pressure, temperature);
         double specificVolume = shvptx(pressure, temperature, quality, 3);
         double density = 1 / specificVolume;

         return DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(DensityUnitType.PoundPerCubicFoot, density);
      }

      //pressure is in SI unit Pa, return value is in SI unit K
      public double GetSaturationTemperature(double pressure) {
         //double psiValue = pressure/6894.75729;
         double psiValue = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);
         double fahrenheitValue = tsl(psiValue);
         //return (fahrenheitValue + 459.67)/1.8;
         return TemperatureUnit.Instance.ConvertToSIValue(TemperatureUnitType.Fahrenheit, fahrenheitValue);
      }

      //temperature is in SI unit K, return pressure is in SI unit Pa
      public double GetSaturationPressure(double temperature) {
         //double fahrenheitValue = temperature = 1.8 * temperature - 459.67;
         double fahrenheitValue = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         double dpdt = 0;
         double psiValue = psl(fahrenheitValue, ref dpdt);
         //return psiValue * 6894.75729;
         return PressureUnit.Instance.ConvertToSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, psiValue);
      }

      public double GetViscosityFromPressureAndTemperature(double pressure, double temperature) {
         temperature = TemperatureUnit.Instance.ConvertFromSIValue(TemperatureUnitType.Fahrenheit, temperature);
         pressure = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(PressureUnitType.IbfPerSquareInch, pressure);

         if (pressure > 0 && pressure < PMIN) {
            pressure = PMIN;
         }
         if (temperature > 0 && temperature < TMIN) {
            temperature = TMIN;
         }


         /*  fix this to cure some problem -- I am not sure why it happens  */
         /*  temperature and pressure available, possibly quality  */
         double quality = x_pt(pressure, temperature);
         double specificVolume = shvptx(pressure, temperature, quality, 3);
         double viscosity = vistv(temperature, 1.0 / specificVolume);

         return DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(DynamicViscosityUnitType.IbfSecPerSquareFoot, viscosity);
      }

      //   action VALUE         program work done
      //   ------------      -------------------------------------------------
      //         0           calculate balance around temperature, pressure,
      //                        steam quality, enthalpy, entropy,
      //                        specific weight, saturation pressure,
      //                        saturation temperature, degrees superheat,
      //                        degrees subcooling
      //         1           action 0 plus viscosity
      //         2           action 1 plus critical velocity

      //1            temperature (degrees fahrenheit)
      //2            pressure (psia)
      //3            specific entropy (btu/lbm/f)
      //4            specific enthalpy (btu/lbm)
      //5            specific volume (cuft/lbm)
      //6            viscosity (lbfs/ft/ft) 
      //7            density (lbm/cuft)
      //8            weight is specific volume (cuft/lbm)

      private bool steam67(ref double temperature, ref double pressure, ref double quality, ref double weight,
         ref double enthalpy, ref double entropy, ref double saturation_temperature, ref double saturation_pressure,
         ref double degrees_superheat, ref double degrees_subcooling, ref double viscosity, ref double critical_velocity,
         int action) {

         double dpdt = 0.0;

         /*  fix this to cure some problem -- I am not sure why it happens  */
         try {
            if (pressure > 0 && pressure < PMIN) {
               pressure = PMIN;
            }
            if (temperature > 0 && temperature < TMIN) {
               temperature = TMIN;
            }

            if (pressure > 0) {
               if (pressure > PCA) {
                  saturation_temperature = TC;
               }
               else {
                  saturation_temperature = tsl(pressure);
               }
            }
            else {
               saturation_temperature = 0;
            }

            if (temperature > 0 && temperature <= TC) {
               saturation_pressure = psl(temperature, ref dpdt);
            }
            else {
               saturation_pressure = 0;
            }

            /*  check to see if both temperature and pressure are present,
                if so, check to see if the pressure and saturation pressure are
                   within .01% of each other,
                if so, set the pressure to zero and let the program calculate the
                   properties using the temperature and specific volume, enthalpy,
                   entropy, or quality in that order
            */

            if (temperature > 0 && pressure > 0 && saturation_pressure > 0)
               if (Math.Abs(pressure - saturation_pressure) / saturation_pressure <= 0.0001) {
                  pressure = 0;
               }

            /*  start cycle of quality, volume, enthalpy, entropy calculations  */

            if (temperature > 0) {
               /*  temperature and pressure available, possibly quality  */
               if (pressure > 0) {
                  quality = x_pt(pressure, temperature);
                  done = true;
               }
               else if (weight > 0) {
                  /*  temperature and specific volume available  */
                  quality = xtshv(temperature, weight, 3);
                  pressure = tpshvx(temperature, weight, quality, 3, 2);
                  done = true;
                  weight_done = true;
               }
               else if (enthalpy > 0) {
                  /*  temperature and enthalpy available  */
                  quality = xtshv(temperature, enthalpy, 2);
                  pressure = tpshvx(temperature, enthalpy, quality, 2, 2);
                  done = true;
                  enthalpy_done = true;
               }
               else if (entropy > 0) {
                  /*  temperature and entropy available  */
                  quality = xtshv(temperature, entropy, 1);
                  pressure = tpshvx(temperature, entropy, quality, 1, 2);
                  done = true;
                  entropy_done = true;
               }
               else if (temperature <= TC) {
                  /*  temperature and quality available, assume
                            pressure is saturation pressure  */
                  pressure = saturation_pressure;
                  done = true;
               }
            }

            //if (MAJOR_ERROR) return false;

            if (!done)
               if (pressure > 0) {
                  /*  pressure and specific volume available  */
                  if (weight > 0) {
                     quality = xpshv(pressure, weight, 3);
                     temperature = tpshvx(pressure, weight, quality, 3, 1);
                     done = true;
                     weight_done = true;
                  }
                  else if (enthalpy > 0) {
                     /*  pressure and enthalpy available  */
                     quality = xpshv(pressure, enthalpy, 2);
                     temperature = tpshvx(pressure, enthalpy, quality, 2, 1);
                     done = true;
                     enthalpy_done = true;
                  }
                  else if (entropy > 0) {
                     /*  pressure and entropy available  */
                     quality = xpshv(pressure, entropy, 1);
                     temperature = tpshvx(pressure, entropy, quality, 1, 1);
                     done = true;
                     entropy_done = true;
                  }
                  else if (pressure <= PCA) {
                     /*  pressure and quality available, assume temperature
                             is saturation temperature  */
                     temperature = saturation_temperature;
                     done = true;
                  }
               }

            /*  if the steam properties have not been yet calculated see if entropy
                and enthalpy are present so we can try to calculate pressure  */

            if (!done) {
               if (enthalpy > 0 && entropy > 0) {
                  pressure = phs(enthalpy, entropy);
               }
               enthalpy_done = true;
               entropy_done = true;
               /*  check if was able to calculate pressure, if so calculate quality
                   and set ...done... to true  */
               if (pressure > 0) {
                  /*  calculate the saturation temperature based on pressure  */
                  if (pressure <= PCA) {
                     saturation_temperature = tsl(pressure);
                  }
                  else {
                     saturation_temperature = TC;
                  }

                  /*  find out if enthalpy is present, then calculate quality and
                      temperature ELSE calculate quality and temperature as a
                      function of entropy (this is based on the fact that
                      either entropy or enthalpy is present if the program
                      got this far)  */

                  quality = xpshv(pressure, enthalpy, 2);
                  temperature = tpshvx(pressure, enthalpy, quality, 2, 1);
                  done = true;
               }
            }

            /*  check to see if steam was able to calculate properties, if not
                then write error message and quit, else calculate rest of properties  */

            if (!done) {
               /*MessageBox (NULL,
                  "There is not enough input data specified to calculate\n"
                  "the property(s).  Or, the input data is not in range\n"
                  "with itself.  At minimum, a temperature or pressure in\n"
                     "the saturated steam or liquid region is required.",
                   SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);*/
               return false;
            }

            /*  check and see if properties calculated, if not calculate  */

            if (!weight_done) {
               weight = shvptx(pressure, temperature, quality, 3);
            }
            if (!enthalpy_done) {
               enthalpy = shvptx(pressure, temperature, quality, 2);
            }
            if (!entropy_done) {
               entropy = shvptx(pressure, temperature, quality, 1);
            }

            //if (MAJOR_ERROR) return false;

            if (saturation_temperature < TMIN) { /*  check to see if already defined  */
               if (pressure <= PCA) {
                  saturation_temperature = tsl(pressure);
               }
               else {
                  saturation_temperature = TC;
               }
            }

            if (saturation_pressure < PMIN) {   /*  check to see if already defined  */
               if (temperature <= TC) {
                  saturation_pressure = psl(temperature, ref dpdt);
               }
               else {
                  saturation_pressure = 0;
               }
            }

            if (quality == 100) {
               degrees_superheat = temperature - saturation_temperature;
            }
            else {
               degrees_superheat = 0;
            }
            if (degrees_superheat < 0.0001) {
               degrees_superheat = 0;
            }

            if (quality == 0) {
               degrees_subcooling = saturation_temperature - temperature;
            }
            else {
               degrees_subcooling = 0;
            }
            if (degrees_subcooling < 0.0001) {
               degrees_subcooling = 0;
            }

            if (action > 0) {
               viscosity = vistv(temperature, 1 / weight);
            }

            if (action > 1) {
               critical_velocity = critical_velocity_of_tpqhs_satp(temperature, pressure, quality, enthalpy,
                  entropy, saturation_pressure);
            }
         }
         catch (Exception) {
            return false;
         }

         return true;
      }

      private double psl(double temperature, ref double dpdt) {
         /*     saturation pressure as a function of temperature,
                arguement is temperature in degrees fahrenheit,
                returns with pressure in psia (as the function PSL) and
                the passed thru variable Dpdt which is used in iterating
                for temperature.
         */

         double y, x1p, ppsl, den1, den2, dsdt, b, dbdt, dbbdt, theta;

         //if (MAJOR_ERROR) return 0.0;
         if (temperature > (TC + 0.1)) {
            throw new IllegalVarValueException("Temperature value out of range.");
         }
         theta = (temperature + TZA) / TCA;
         x1p = 1.0 - theta;
         y = x1p * (AK1 + x1p * (AK2 + x1p * (AK3 + x1p * (AK4 + x1p * AK5))));
         den1 = 1.0 + x1p * (AK6 + x1p * AK7);
         den2 = AK8 * x1p * x1p + AK9;
         b = y / theta / den1 - x1p / den2;
         ppsl = PCA * Math.Exp(b);
         dsdt = -(AK1 + x1p * (AK2T2 + x1p * (AK3T3 + x1p * (AK4T4 + AK5T5 * x1p))));
         b = theta * den1;
         dbdt = den1 - theta * (AK6 + AK7T2 * x1p);
         dbbdt = -AK8T2 * x1p;
         dpdt = ppsl / TCA * ((b * dsdt - y * dbdt) / b / b) + (den2 + x1p * dbbdt / den2 / den2) / 1e11;
         return ppsl;
      }   /*  psl  */

      private double p23t(double temperature) {

         /* given temperature in degrees F, calculates the pressure
            in psia for the boundary between regions 2 and 3  */
         double theta = (temperature + TZA) / TCA;
         return (AL0 + theta * (AL1 + theta * AL2)) * PCA;
      }          /*  p23t  */

      private double tsl(double pressure) {
         /*  given pressure in psia, gives saturation temperature in degrees F  */
         const double pcat1p1 = 3529.058214;            /*   pca times 1.1  */

         double pa, y, tz, ty, tx, w, ttsl, tol, dpdt = 0, dp, pr, f;
         int tsl_counter;

         if (pressure > PCA || pressure < PMIN) {
            throw new IllegalVarValueException("Pressure value out of range.");
         }
         tol = 1.0e-6;
         if (pressure > 2700) {
            tol = 1.0e-9;
         }
         if (pressure > 3200) {
            tol = 1.0e-10;
         }
         f = 1.0;
         tx = 1.0;
         ty = (Math.Exp(0.4 * Math.Log(Math.Log(pcat1p1 / pressure))) - 1.4804712555) / -1.089944005;
         y = 2.0 * ty;
         w = b1d[0] + ty * b1d[1];
         for (tsl_counter = 2; tsl_counter <= 11; tsl_counter++) {
            tz = y * ty - tx;
            w = w + tz * b1d[tsl_counter];
            tx = ty;
            ty = tz;
         }
         ttsl = TCA / w - TZA;
         ty = 0.01;
         if (ttsl > TC) {
            ttsl = TC;
         }
         tsl_counter = 1;
         do {
            //pa = psl (&ttsl, &dpdt);
            pa = psl(ttsl, ref dpdt);
            dp = pressure - pa;
            if (pressure > 0) {
               pr = dp / pressure;
            }
            else {
               pr = dp;
            }
            ttsl = ttsl + f * dp / dpdt;
            f = f * 0.99;
            if (ttsl > TC) {
               ty = 0.9 * ty;
               ttsl = ttsl - ty;
            }
            tsl_counter++;
         } while (Math.Abs(pr) >= tol && tsl_counter < 200);

         if (tsl_counter == 200) {
            throw new CalculationFailedException("Calculation of pressure from temperature for the boundary between regions 2 and 3 has failed.");
         }
         return ttsl;
      }               /*  tsl  */

      private double shvpt1(double pressure, double temperature, int option) {

         /*  given pressure in psia, temperature in F, produces sub region 1
             properties

               option          property
               ------         ----------------------------
                 1            entropy (btu/lbm/R)
                 2            enthalpy (btu/lbm)
                 3            volume (cuft/lbm)                */

         double beta, beta2, beta3;
         double theta, th2, th6, th7, th10, th11, th17, th18, th19, th20, th21;
         double shv0, shv1, shv2, shv3, shv4, shv5;
         double ua, ua9, ua10, ub, ub2, uc, u3t, ud, ud3, ud4, up, y, z, yp, zp;

         if (pressure > PMAX || pressure < PMIN) {
            throw new IllegalVarValueException("Pressure value out of range.");
         }
         if (temperature > T1MAX || temperature < TMIN) {
            throw new IllegalVarValueException("Temperature value out of range.");
         }
         beta = pressure / PCA;
         beta2 = beta * beta;
         beta3 = beta2 * beta;
         theta = (temperature + TZA) / TCA;
         th2 = theta * theta;
         th6 = th2 * th2 * th2;
         th7 = th6 * theta;
         th10 = th7 * th2 * theta;
         th11 = th10 * theta;
         th17 = th11 * th6;
         th18 = th11 * th7;
         th19 = th18 * theta;
         th20 = th19 * theta;
         th21 = th20 * theta;
         ua = A6 - theta;
         ua9 = Math.Pow(ua, 9);
         ua10 = ua9 * ua;
         ub = A7 + th19;
         ub2 = ub * ub;
         uc = A8 + th11;
         u3t = beta * (AA17 + beta * (AA18 + AA19 * beta)) / uc / uc;
         ud = A10 + beta;
         ud3 = ud * ud * ud;
         ud4 = ud3 * ud;
         up = 1.0 / ud3 + A11 * beta;
         y = 1.0 - A1 * th2 - A2 / th6;
         z = y + Math.Sqrt(A3 * y * y + A5T2 * beta - A4T2 * theta);
         yp = A2T6 / th7 - theta * A1T2;
         zp = Math.Exp(M5D17 * Math.Log(z));
         switch (option) {
            case 1: {
                  shv5 = beta3 * (AA21 + AA22T20 * beta / th21);
                  shv4 = AA20 * th17 * (A9T18 + 20.0 * th2) * up;
                  shv3 = 11.0 * th10 * u3t;
                  shv2 = (-AA13 - AA14T2 * theta + AA15T10 * ua9 +
                     AA16T19 * th18 / ub2) * beta;
                  shv1 = AA11 * ((P5D12 * z - A3M1 * y) * yp + A4) * zp;
                  shv0 = AA0 * Math.Log(theta) - (AA2 + theta * (AA3T2 +
                     theta * (AA4T3 +
                     theta * (AA5T4 + theta * (AA6T5 +
                     theta * (AA7T6 + theta *
                     (AA8T7 + theta * (AA9T8 + theta * AA10T9))))))));
                  return ((shv0 + shv1 + shv2 - shv3 + shv4 + shv5 - ALPHA1) * PVOT);
               }
            case 2: {
                  shv5 = beta3 * (AA21TA12 + AA22T21 * beta / th20);
                  shv4 = AA20 * th18 * (A9T17 + 19.0 * th2) * up;
                  shv3 = (12.0 * th11 + A8) * u3t;
                  shv2 = (AA12 - AA14 * th2 + AA15 * (9.0 * theta + A6) * ua9 +
                     AA16 * (20.0 * th19 + A7) / ub2) * beta;
                  shv1 = AA11 * (z * (z * P17D29 - y * P17D12 + P5D12 * theta * yp) +
                     A4 * theta - A3M1 * theta * y * yp) * zp;
                  shv0 = AA0 * theta + AA1 - th2 * (AA3 + theta * (AA4T2 + theta *
                     (AA5T3 + theta * (AA6T4 +
                     theta * (AA7T5 + theta * (AA8T6 +
                     theta * (AA9T7 + theta * AA10T8)))))));
                  /*  constant added to give exact value at triple point  */
                  return ((shv0 + shv1 + shv2 - shv3 + shv4 + shv5 + ALPHA0) * PVO1O);
               }
            case 3: {
                  shv4 = beta2 * (AA21T3 * (A12 - theta) + AA22T4 * beta / th20);
                  shv3 = AA20 * th18 * (A9 + th2) * (A11 - 3.0 / ud4);
                  shv2 = (AA17 + beta * (AA18T2 + AA19T3 * beta)) / uc;
                  shv1 = AA12 + theta * (AA13 + theta * AA14) + AA15 * ua10 +
                     AA16 / ub;
                  return ((AA11TA5 * zp + shv1 - shv2 - shv3 + shv4) * VCA);
               }
         }
         return 0.0;
      } /*  shvpt1   */

      private double shvpt2(double pressure, double temperature, int option) {

         /* sub region 2, given pressure in psia and temperature in degrees F,

                      option      property
                      ------      --------------------------------
                         1        entropy (btu/lbm/R)
                         2        enthalpy (btu/lbm)
                         3        volume (cuft/lbm)                     */

         double d2, d3, d4, both, betal, t2, t3, t4, fb, bb61f, bb71f, theta, th2, bb81f, boblp;
         double shv2a, shv3a, shv4a, shv9, shv4, shv3, shv2, shv1, shv0;
         double x1, x2, x3, x4, x6, x8, x10, x11, x13, x14, x17, x18, x19, x24, x25, x27, x28, x32;
         double beta, beta4, beta5, beta6, beta7;

         if ((pressure > PMAX) || (pressure < PMIN)) {
            throw new IllegalVarValueException("Pressure value out of range.");
         }
         if ((temperature > TMAX) || (temperature < T2MIN)) {
            throw new IllegalVarValueException("Temperature value out of range.");
         }
         beta = pressure / PCA;
         theta = (temperature + TZA) / TCA;
         th2 = theta * theta;
         x1 = Math.Exp(B00 * (1.0 - theta));
         x2 = x1 * x1;
         x3 = x1 * x2;
         x4 = x3 * x1;
         x6 = x3 * x3;
         x8 = x4 * x4;
         x10 = x8 * x2;
         x11 = x10 * x1;
         x13 = x11 * x2;
         x14 = x10 * x4;
         x17 = x14 * x3;
         x18 = x17 * x1;
         x19 = x11 * x8;
         x24 = x14 * x10;
         x25 = x24 * x1;
         x27 = x24 * x3;
         x28 = x27 * x1;
         x32 = x28 * x4;
         bb61f = BB61 * x1;
         bb71f = BB71 * x6;
         bb81f = BB81 * x10;
         beta4 = Math.Pow(beta, 4);
         beta5 = beta4 * beta;
         beta6 = beta5 * beta;
         beta7 = beta6 * beta;
         d4 = 1.0 / beta6 + x27 * (B81 * x27 + B82);
         d3 = 1.0 / beta5 + B71 * x19;
         d2 = 1.0 / beta4 + B61 * x14;
         betal = AL0 + theta * (AL1 + theta * AL2);
         /*  temporary fix for problem with vpt2 wanting al1 / 10  */
         if (option == 3) betal = AL0 + theta * (AL1 / 10.0 + theta * AL2);
         t2 = B61T14 * x14 / d2;
         t3 = B71T19 * x19 / d3;
         t4 = x27 * (B81T2T27 * x27 - B82T27) / d4;
         fb = (AL1T10 + theta * AL2T2T10) / betal;
         boblp = Math.Pow(beta / betal, 10);
         switch (option) {
            case 1: {
                  shv0 = BB00 * Math.Log(theta) - I1 * Math.Log(beta) - BB02 - theta *
                     (BB03T2 + theta * (BB04T3 + theta * BB05T4));
                  shv1 = beta * (x3 * (BB11T13 * x10 + BB12T3) +
                     beta * (x1 * (BB21T18 * x17 + BB22T2 * x1 + BB23) +
                     beta * (x10 * (BB31T18 * x8 + BB32T10) +
                     beta * (x14 * (BB41T25 * x11 + BB42T14) +
                     beta * (x24 * (BB51T32 * x8 + BB52T28 * x4 +
                     BB53T24))))));
                  shv2 = x11 * (bb61f * (12.0 - t2) + BB62 * (11.0 - t2)) / d2;
                  shv3 = x18 * (bb71f * (24.0 - t3) + BB72 * (18.0 - t3)) / d3;
                  shv4 = x14 * (bb81f * (24.0 - t4) + BB82 * (14.0 - t4)) / d4;
                  shv9 = beta * boblp * (fb * BB90 + x1 * ((fb + B00) * BB91 +
                     x1 * ((fb + B00T2) * BB92 + x1 * ((fb + B00T3) * BB93 +
                     x1 * ((fb + B00T4) * BB94 + x1 * ((fb + B00T5) * BB95 +
                     x1 * ((fb + B00T6) * BB96)))))));
                  return ((shv0 - B00 * (shv1 + shv2 + shv3 + shv4) + shv9 - ALPHA1)
                     * PVOT);
               }
            case 2: {
                  both = B00 * theta;
                  shv0 = BB00 * theta + BB01 - th2 * (BB03 + theta * (BB04T2 +
                     BB05T3 * theta));
                  shv1 = beta * (BB11 * x13 * (1.0 + 13.0 * both) +
                     (BB12 * x3 * (1.0 + 3.0 * both) +
                     beta * (BB21 * x18 * (1.0 + 18.0 * both) +
                     BB22 * x2 * (1.0 + 2.0 * both) +
                     BB23 * x1 * (1.0 + both) +
                     beta * (BB31 * x18 * (1.0 + 18.0 * both) +
                     BB32 * x10 * (1.0 + 10.0 * both) +
                     beta * (BB41 * x25 * (1.0 + 25.0 * both) +
                     BB42 * x14 * (1.0 + 14.0 * both) +
                     beta * (BB51 * x32 * (1.0 + 32.0 * both) +
                     BB52 * x28 * (1.0 + 28.0 * both) +
                     BB53 * x24 * (1.0 + 24.0 * both)))))));
                  shv2a = 1.0 - both * t2;
                  shv2 = x11 * (bb61f * (shv2a + 12.0 * both) +
                     BB62 * (shv2a + 11.0 * both)) / d2;
                  shv3a = 1.0 - both * t3;
                  shv3 = x18 * (bb71f * (shv3a + 24.0 * both) +
                     BB72 * (shv3a + 18.0 * both)) / d3;
                  shv4a = 1.0 - both * t4;
                  shv4 = x14 * (bb81f * (shv4a + 24.0 * both) +
                     BB82 * (shv4a + 14.0 * both)) / d4;
                  shv9 = beta * boblp * ((1.0 + theta * fb) * BB90 +
                     x1 * ((1.0 + theta * (fb + B00)) * BB91 +
                     x1 * ((1.0 + theta * (fb + B00T2)) * BB92 +
                     x1 * ((1.0 + theta * (fb + B00T3)) * BB93 +
                     x1 * ((1.0 + theta * (fb + B00T4)) * BB94 +
                     x1 * ((1.0 + theta * (fb + B00T5)) * BB95 +
                     x1 * ((1.0 + theta * (fb + B00T6)) * BB96)))))));
                  return ((shv0 - shv1 - shv2 - shv3 - shv4 + shv9 + ALPHA0)
                     * PVO1O);
               }
            case 3: {
                  shv1 = x3 * (BB11 * x10 + BB12) +
                     beta * (x1 * (BB21T2 * x17 + BB22T2 * x1 + BB23T2) +
                     beta * (x10 * (BB31T3 * x8 + BB32T3) +
                     beta * (x14 * (BB41T4 * x11 + BB42T4) +
                     beta * (x24 * (BB51T5 * x8 + BB52T5 * x4 + BB53T5)))));
                  /*  v4, v3, and v2 must be expressed in this way to avoid
                            overflows at low pressures  */
                  shv2 = 4.0 * x11 * (bb61f + BB62) / (d2 * beta5 * d2);
                  shv3 = 5.0 * x18 * (bb71f + BB72) / (d3 * beta6 * d3);
                  shv4 = 6.0 * x14 * (bb81f + BB82) / (d4 * beta7 * d4);
                  shv9 = 11.0 * boblp * (BB90 + x1 * (BB91 + x1 * (BB92 + x1 + (BB93
                     + x1 * (BB94 + x1 * (BB95 + x1 * BB96))))));
                  return ((I1 * theta / beta - shv1 - shv2 - shv3 - shv4 + shv9)
                     * VCA);
               }
         }
         return 0.0;
      }               /*  shvpt2   */

      public double shpvt3(double volume, double temperature, int option) {

         /*  given volume in cuft/lbm and temperature in degrees F, sub region 3

                 option         calculates property
                 ------         -----------------------
                   1            entropy (btu/lbm/R)
                   2            enthalpy (btu/lbm)
                   3            pressure (psia)           */

         double ex, exm1, exlog, exm5, thm1, thm22, thl1, omth1, y2, y3,
            thm23, exm6, x50th2, x60th2, y31, theta1, exm2, fth3, fth4, fth32;
         double shp4, shp3, shp2, shp1, shp0,
            shp6, shp7, shp8, shp8a, shp8b, shp8c, shp7a, shp7b, shpa;
         double theta, y;

         if (volume > V3MAX || volume < V3MIN) {
            throw new IllegalVarValueException("Volume value out of range.");
         }
         if ((temperature > T3MAX) || (temperature < T3MIN)) {
            throw new IllegalVarValueException("Temperature value out of range.");
         }
         ex = volume / VCA;
         theta = (temperature + TZA) / TCA;
         thl1 = theta - 1.0;
         theta1 = (T1 + TZA) / TCA;
         omth1 = 1.0 - theta1;
         y = (1.0 - theta) / omth1;
         y2 = y * y;
         y3 = y * y2;
         y31 = Math.Pow(y, 31);
         exm1 = VCA / volume;
         exm2 = exm1 * exm1;
         exm5 = exm2 * exm2 * exm1;
         exm6 = exm5 * exm1;
         exlog = Math.Log(ex);
         thm1 = 1.0 / theta;
         thm22 = Math.Pow(thm1, 22);
         thm23 = thm22 * thm1;
         x50th2 = thm1 * thm1 / exm5;
         x60th2 = ex * x50th2;

         switch (option) {
            case 1: {
                  shp1 = C11 * ex + exm1 * (C12 + exm1 * (C13 + exm1 * (C14 + exm1 * (C15 + exm1 * C16)))) + C17 * exlog + C50;
                  shp2 = 2.0 * (C21 * ex + exm1 * (C22 + exm1 * (C23 + exm1 * (C24 + exm1 * (C25 + exm1 * (C26 + exm1 * C27))))) + C28 * exlog);
                  shp3 = 3.0 * (C31 * ex + exm1 * (C32 + exm1 * (C33 + exm1 * (C34 + exm1 * (C35 + exm1 * (C36 + exm1 * (C37 + exm1 * (C38 + exm1 *
                     C39))))))) + C310 * exlog);
                  shp4 = (C40 + C41 * exm5) * (22.0 - 23.0 * thm1) * thm23 - C50 * Math.Log(theta);
                  shp6 = (x60th2 / theta) * (C60T2 + thm1 * (C61T3 + thm1 * (C62T4 + thm1 * (C63T5 + thm1 * C64T6))));
                  shp7 = C70 + thl1 * (C71T2 + thl1 * (C72T3 + thl1 * (C73T4 + thl1 *
                     (C74T5 + thl1 * (C75T6 + thl1 * (C76T7 + thl1 * (C77T8 + thl1 * C78T9)))))));
                  if (theta >= 1.0 || ex >= 1.0) {
                     shp8 = 0.0;
                  }
                  else {
                     shp8a = y2 * (3.0 * (D30 + exm1 * (D31 + exm1 * (D32 +
                        exm1 * (D33 + exm1 * D34)))) + 4.0 * y * (D40 + exm1 * (D41 + exm1 * (D42 +
                        exm1 * (D43 + exm1 * D44)))));
                     shp8b = 32.0 * y31 * (D50 + ex * (D51 + ex * D52));
                     shp8 = (shp8a + shp8b) / omth1;
                  }
                  return (-shp1 - thl1 * (shp2 + shp3 * thl1) + shp4 + shp6 - shp7 + shp8 - ALPHA1) * PVOT;
               }

            case 2: {
                  shp0 = C00MC012MC50 - C11 * ex + exm1 * (C02T2 +
                     exm1 * (C03T3 + exm1 * (C04T4 + exm1 * (C05T5 +
                     exm1 * (C06T6 + exm1 * (C07T7 + exm1
                     * (C08T8 + exm1 * (C09T9 + exm1 * (C010T10 + exm1 *
                     C011T11))))))))) - exm1 * (C12 + exm1 * (C13 + exm1 * (C14 +
                     exm1 * (C15 + C16 * exm1)))) + C012MC17 * exlog;
                  shp1 = MC17MC50 - ex * (C11 + C21T2) + exm1 * (C12 + exm1 * (C13T2 +
                     exm1 * (C14T3 + exm1 * (C15T4 + exm1 * C16T5))))
                     - 2.0 * exm1 * (C22 + exm1 * (C23 + exm1 * (C24 +
                     exm1 * (C25 + exm1 * (C26 + exm1 * C27))))) - C28T2 * exlog;
                  shp2 = -C28 - ex * C21T2PC31T3 + exm2 * (C23 +
                     exm1 * (C24T2 + exm1 * (C25T3 + exm1 * (C26T4 +
                     exm1 * C27T5)))) - 3.0 * exm1 *
                     (C32 + exm1 * (C33 + exm1 * (C34 +
                     exm1 * (C35 + exm1 * (C36 +
                     exm1 * (C37 + exm1 * (C38 + exm1 * C39)))))))
                     - C28PC310T3 * exlog;
                  shp3 = -C310 - C31T3 * ex + exm1 * (-C32 + exm2 * (C34 + exm1 *
                     (C35T2 + exm1 * (C36T3 + exm1 *
                     (C37T4 + exm1 * (C38T5 + exm1 *
                     C39T6)))))) - C310T2 * exlog;
                  shp4 = (C40T23 + C41T28 * exm5 -
                     (C40T24 + C41T29 * exm5) * thm1) * thm22;
                  shp6 = -x60th2 * (C60T3 + thm1 * (C61T2 +
                     thm1 * (C62 + thm1 * (-C64 * thm1))));
                  shp7 = C70 + thl1 * (C71 * (1.0 + theta) +
                     thl1 * (C72 * (1.0 + 2.0 * theta) +
                     thl1 * (C73 * (1.0 + 3.0 * theta) +
                     thl1 * (C74 * (1.0 + 4.0 * theta) +
                     thl1 * (C75 * (1.0 + 5.0 * theta) +
                     thl1 * (C76 * (1.0 + 6.0 * theta) +
                     thl1 * (C77 * (1.0 + 7.0 * theta) +
                     thl1 * (C78 * (1.0 + 8.0 * theta)))))))));
                  if ((theta >= 1.0) || (ex >= 1.0)) {
                     shp8 = 0.0;
                  }
                  else {
                     fth3 = 3.0 / omth1;
                     shp8a = (D30 * (-2.0 * y + fth3) + exm1 * (D31 * (-y + fth3) +
                        exm1 * (D32 * fth3 + exm1 * (D33 * (y + fth3) + exm1 *
                        (D34 * (2.0 * y + fth3)))))) * y2;
                     fth4 = 4.0 / omth1;
                     shp8b = (D40 * (-3.0 * y + fth4) + exm1 *
                        (D41 * (-2.0 * y + fth4) +
                        exm1 * (D42 * (-y + fth4) + exm1 * (D43 * fth4 + exm1 *
                        (D44 * (y + fth4)))))) * y3;
                     fth32 = 32.0 / omth1;
                     shp8c = y31 * (D50 * (31.0 * y - fth32) +
                        ex * (D51 * (32.0 * y - fth32)
                        + ex * D52 * (33.0 * y - fth32)));
                     shp8 = shp8a + shp8b - shp8c;
                  }
                  return (shp0 + thl1 * (shp1 + thl1 * (shp2 + thl1 * shp3)) + shp4 + shp6 - shp7 + shp8 + ALPHA0) * PVO1O;
               }

            case 3: {
                  shp0 = C01 - exm2 * (C02 + exm1 * (C03T2 +
                     exm1 * (C04T3 + exm1 * (C05T4 + exm1 * (C06T5 +
                     exm1 * (C07T6 + exm1 * (C08T7 + exm1 * (C09T8
                     + exm1 * (C010T9 + exm1 * C011T10))))))))) + C012 * exm1;
                  shpa = C11 - exm2 * (C12 + exm1 * (C13T2 + exm1 *
                     (C14T3 + exm1 * (C15T4 + exm1 * C16T5)))) + C17 * exm1;
                  shp2 = C21 - exm2 * (C22 + exm1 * (C23T2 + exm1 *
                     (C24T3 + exm1 * (C25T4
                     + exm1 * (C26T5 + exm1 * C27T6))))) + C28 * exm1;
                  shp3 = C31 - exm2 * (C32 + exm1 * (C33T2 +
                     exm1 * (C34T3 + exm1 * (C35T4
                     + exm1 * (C36T5 + exm1 * (C37T6 + exm1 * (C38T7 + exm1 *
                     C39T8))))))) + C310 * exm1;
                  shp4 = C41T5 * exm6 * thm23;
                  shp6 = 6.0 * x50th2 * (C60 + thm1 * (C61 + thm1 *
                     (C62 + thm1 * (C63 + thm1 * C64))));
                  if ((theta >= 1.0) || (ex >= 1.0)) {
                     shp7 = 0.0;
                  }
                  else {
                     shp7a = y3 * (exm2 * (D31 + exm1 * (D32T2 +
                        exm1 * (D33T3 + exm1 * D34T4))) +
                        y * exm2 * (D41 + exm1 * (D42T2 +
                        exm1 * (D43T3 + exm1 * D44T4))));
                     shp7b = y * y31 * (D51 + D52T2 * ex);
                     shp7 = shp7a - shp7b;
                  }
                  return (-shp0 + thl1 * (-shpa - thl1 * (shp2 + shp3 * thl1) + shp4) - shp6 + shp7) * PCA;
               }
         }
         return 0.0;
      }

      /*  shpvt3  */
      private double vptx3(double pressure, double temperature, double quality) {

         /*  given pressure in psia, temperature in degrees F, and steam quality
             (0-100), calculates specific volume in cuft/lbm, sub region 3 }  */


         //static double  vptx3_save_0, vptx3_save_100;

         double b_vptx3, b_vptx3a, v, dv, pcalc, pct, vprev = 0;
         double df, pprev = 0, pctp = 0, vup, vlow;
         int vptx3_counter;

         //if (MAJOR_ERROR) return 0.0;
         //VPTX3_CALL++;
         if (temperature >= 705.0 && temperature <= 705.47) {
            v = 0.05078 + (100.0 - quality) / 100.0 * (0.05078 - 0.04427) *
               (temperature - 705.47) / 0.47 +
               quality / 100.0 * (0.05078 - 0.05730) *
               (temperature - 705.47) / 0.47;
            vup = V3MAX;
            vlow = V3MIN;
         }
         else {
            if (quality == 0) {
               if (VPTX3_ALREADY_SET_0) {
                  return vptx3_save_0;
               }
               v = (0.077 + 92.8 / pressure) * (temperature - 482.0) /
                  (temperature + 58.0);
               b_vptx3 = temperature / TC;
               b_vptx3a = pressure / PCA;
               if (b_vptx3a > b_vptx3) {
                  b_vptx3 = b_vptx3a;
               }
               vup = VCA * b_vptx3 * b_vptx3;
               vlow = V3MIN;
            }
            else {
               if (VPTX3_ALREADY_SET_100) {
                  return vptx3_save_100;
               }
               v = 0.3 * (temperature + TZA) / pressure;
               vup = V3MAX;
               vlow = V3MIN;
            }
         }
         if (pressure > PMAX || pressure < P3MIN) {
            throw new CalculationFailedException("Pressure calculation failed.");
         }
         df = 0.25;
         vptx3_counter = 0;

         do {
            vptx3_counter++;
            //if (vptx3_counter > 100) {
            //   return 0.0;
            //}
            if (v > vup) {
               v = vup;
               df = 1;
            }
            if (v < vlow) {
               v = vlow;
               df = 1;
            }
            pcalc = shpvt3(v, temperature, 3);
            //if (MAJOR_ERROR) return 0.0;
            pct = (pcalc - pressure) / pressure;
            if (vptx3_counter == 1) {
               dv = v * pct * df;
            }
            else {
               if ((pctp * pct) > 0) {
                  if (Math.Abs(pct) > Math.Abs(0.3 * pctp)) {
                     df = 1.5 * df;
                     dv = v * pct * df;
                  }
                  else dv = (v - vprev) * (pressure - pcalc) / (pcalc - pprev);
               }
               else {
                  df = df * 0.67;
                  dv = (v - vprev) * (pressure - pcalc) / (pcalc - pprev);
               }
            }
            if (Math.Abs(pct) >= tolerance) {
               vprev = v;
               pprev = pcalc;
               pctp = pct;
               v = v + dv;
            }
         } while (Math.Abs(pct) >= tolerance && vptx3_counter < 100);

         if (vptx3_counter == 100) {
            throw new CalculationFailedException("Volume calculation failed.");
         }

         if (quality == 0) {
            VPTX3_ALREADY_SET_0 = true;
            vptx3_save_0 = v;
         }
         else {
            VPTX3_ALREADY_SET_100 = true;
            vptx3_save_100 = v;
         }
         return v;
      }

      private double shvptx(double pressure, double temperature, double quality, int option) {

         /* function of pressure (psia), temperature (F), steam quality (0-100 %)

                       option                          shvptx
                       ------          -------------------------------------------
                          1            specific entropy (btu/lbm/f)
                          2            specific enthalpy (btu/lbm)
                          3            specific volume (cuft/lbm)

           */

         double shvptd = 0, shvptl = 0, temp = 0;

         //if (MAJOR_ERROR) return 0.0;
         if (quality > 0) {
            if (pressure > p23t(temperature)) {
               temp = 100.0;
               shvptd = vptx3(pressure, temperature, temp);
               if (option == 1 || option == 2)
                  shvptd = shpvt3(shvptd, temperature, option);
            }
            else {
               shvptd = shvpt2(pressure, temperature, option);
            }
         }
         if (quality < 100.0) {
            if (temperature > T1) {
               temp = 0.0;
               shvptl = vptx3(pressure, temperature, temp);
               if (option == 1 || option == 2)
                  shvptl = shpvt3(shvptl, temperature, option);
            }
            else {
               shvptl = shvpt1(pressure, temperature, option);
            }
         }
         return (100.0 - quality) / 100.0 * shvptl + quality / 100.0 * shvptd;
      }   /*  shvptx  */

      private double x_pt(double pressure, double temperature) {

         /*  quality as a function of pressure and temperature  */

         double temp_sat;

         //if (MAJOR_ERROR) return 0.0;
         /* if above crit temp then superheated */
         if (temperature > TC) {
            return 100.0;
         }
         else if (pressure <= PCA) {  /* only if less than critical pres */
            temp_sat = tsl(pressure);
            if (temperature > temp_sat) {
               return 100.0;
            }
            if (temperature < temp_sat) {
               return 0.0;
            }
         }
         /*  compressed supercritical liquid  */
         else {
            return 0.0;
         }

         return 0.0;
      }   /*  x_pt  */

      private double xpshv(double pressure, double shv, int option) {

         /*  quality as a function of pressure and shv
                option      shv
                ------    -------
                   1      entropy
                   2      enthalpy
                   3      specific volume
         */

         double saturation_temperature, shvg, shvf, temp;

         //if (MAJOR_ERROR) return 0.0;
         /*  if above critical pressure,use critical temperature  */
         if (pressure <= PCA) {
            saturation_temperature = tsl(pressure);
            temp = 100.0;
            shvg = shvptx(pressure, saturation_temperature, temp, option);
            if (shv < shvg) {
               /*  point is wet  */
               temp = 0.0;
               shvf = shvptx(pressure, saturation_temperature, temp, option);
               if (shv > shvf) {
                  return (shv - shvf) / (shvg - shvf) * 100;
               }
               else {
                  return 0.0;
               }
            }
            else {
               return 100.0;
            }
         }
         else {
            shvf = TC;    /*  cannot use constant in function call  */
            /*  must use quality of 0 since the fluid is always pressurized
                 liquid at 705.47 F from 3208.2 psia to 16000 psia  */
            temp = 0;
            shvf = shvptx(pressure, shvf, temp, option);
            if (shv >= shvf) {
               return 100.0;
            }
            else {
               return 0.0;
            }
         }
      }           /*  xpshv  */

      private double xtshv(double temperature, double shv, int option) {

         /*  quality as a function of temperature and shv
                 option      shv
                 ------    --------
                    1      entropy
                    2      enthalpy
                    3      specific volume  */

         double shvg = 0, shvf = 0, saturation_pressure = 0, dpdt = 0, temp = 0;

         //if (MAJOR_ERROR) return 0.0;
         /*  if above critical temperature, quality is 100 %  */
         if (temperature <= TC) {
            saturation_pressure = psl(temperature, ref dpdt);
            temp = 100.0;
            shvg = shvptx(saturation_pressure, temperature, temp, option);
            if (shv < shvg) {   /*  point is wet  */
               temp = 0.0;
               shvf = shvptx(saturation_pressure, temperature, temp, option);
               if (shv > shvf) {
                  return (shv - shvf) / (shvg - shvf) * 100.0;
               }
               else {
                  return 0.0;
               }
            }
            else {
               return 0.0;
            }
         }
         else {
            return 100.0;
         }
      }    /*  xtshv  */

      private double tpshvx(double tp, double shv, double quality, int option_shv, int option_tp) {

         /* temperature or pressure as a function of pressure or temperature,
            shv, quality

              option_shv       shv                   option_tp    calculates
              ----------    ---------------          ---------    -----------
                  1         entropy                      1        temperature
                  2         enthalpy                     2        pressure
                  3         volume

          */

         //const double    tolerance = 1.0e-5;

         double saturation_tp = 0, dpdt = 0;
         double d_temp_press, tpshvx_shvcalc = 0, tpshvx_percent = 0, tpshvx_tp,
            previous_tp = 0, tp_up = 0, tp_low = 0, tp_df, tpshvx_prev_pct = 0,
            prev_tpshvx_shvcalc = 0;
         int tpshvx_counter;

         //if (MAJOR_ERROR) return 0.0;

         /*  calculate the saturation temperature  */

         switch (option_tp) {
            case 1:
               if (tp >= PCA) {
                  saturation_tp = TC;
               }
               else {
                  saturation_tp = tsl(tp);
               }
               break;
            case 2:
               if (tp >= TC) {
                  saturation_tp = PCA;
               }
               else {
                  saturation_tp = psl(tp, ref dpdt);
               }
               break;
         }

         /* if the quality is between 0 and 100, the pressure or temperature
            is the saturation pressure or temperature   */

         if (quality > 0 && quality < 100) {
            return saturation_tp;
         }

         switch (option_tp) {
            case 1:
               if (quality == 0) {
                  tp_up = saturation_tp;
                  tp_low = TMIN;
               }
               /* for program to reach this point the quality must be 100 percent  */
               else {
                  tp_low = saturation_tp;
                  tp_up = TMAX;
               }
               break;
            case 2:
               if (quality == 0) {
                  tp_low = saturation_tp;
                  tp_up = PMAX;
               }
               /* for program to reach this point the quality must be 100 percent */
               else {
                  tp_up = saturation_tp;
                  tp_low = PMIN;
               }
               break;
         }

         tpshvx_tp = (tp_low + tp_up) / 2.0;
         tpshvx_counter = 0;
         tp_df = 0.50;

         do {
            tpshvx_counter++;
            //if (tpshvx_counter > 100) {
            //   return 0.0;
            //}
            if (tpshvx_tp > tp_up) {
               tpshvx_tp = tp_up;
               tp_df = 1.0;
            }
            if (tpshvx_tp < tp_low) {
               tpshvx_tp = tp_low;
               tp_df = 1.0;
            }
            VPTX3_ALREADY_SET_0 = false;
            VPTX3_ALREADY_SET_100 = false;
            switch (option_tp) {
               case 1: tpshvx_shvcalc = shvptx(tp, tpshvx_tp, quality, option_shv);
                  break;
               case 2: tpshvx_shvcalc = shvptx(tpshvx_tp, tp, quality, option_shv);
                  break;
            }
            //if (MAJOR_ERROR) return 0.0;
            switch (option_tp) {
               case 1: tpshvx_percent = (shv - tpshvx_shvcalc) / shv;
                  break;
               case 2: tpshvx_percent = (tpshvx_shvcalc - shv) / shv;
                  break;
            }
            if (tpshvx_counter == 1)
               d_temp_press = tpshvx_tp * tpshvx_percent * tp_df;
            else {
               if ((tpshvx_prev_pct * tpshvx_percent) > 0) {
                  if (Math.Abs(tpshvx_percent) > Math.Abs(0.3 * tpshvx_prev_pct)) {
                     tp_df = 1.5 * tp_df;
                     d_temp_press = tpshvx_tp * tpshvx_percent * tp_df;
                  }
                  else d_temp_press = (tpshvx_tp - previous_tp) *
                          (shv - tpshvx_shvcalc) /
                          (tpshvx_shvcalc - prev_tpshvx_shvcalc);
               }
               else {
                  tp_df = tp_df * 0.67;
                  d_temp_press = (tpshvx_tp - previous_tp) *
                     (shv - tpshvx_shvcalc) /
                     (tpshvx_shvcalc - prev_tpshvx_shvcalc);
               }
            }
            if (Math.Abs(tpshvx_percent) >= tolerance) {
               previous_tp = tpshvx_tp;
               prev_tpshvx_shvcalc = tpshvx_shvcalc;
               tpshvx_prev_pct = tpshvx_percent;
               tpshvx_tp = tpshvx_tp + d_temp_press;
            }
         } while (Math.Abs(tpshvx_percent) >= tolerance && tpshvx_counter < 100);

         if (tpshvx_counter == 100) {
            throw new CalculationFailedException("tpshvx_tp calculation failed.");
         }

         return tpshvx_tp;
      }        /*  tpshvx  */

      private double phsd(double h, double s) {

         /* calculates pressure as a function of enthalpy and entropy in the
            dry superheat region   */

         const double a0 = -1.5789006e1;
         const double a1 = 4.4537925e-2;
         const double a2 = -4.7685218e-5;
         const double a3 = 2.1794554e-8;
         const double a4 = -3.0128279e-12;
         const double b1 = -1.8413685e-3;
         const double b2 = 9.7990702e-7;
         //const double   tolerance = 1.0e-5;

         double p, t, ds, hp, temp;
         int counter = 0;

         //if (MAJOR_ERROR) return 0.0;
         //counter = 0;
         p = Math.Exp((a0 + h * (a1 + h * (a2 + h * (a3 + h * a4)))) / (1.0 + h *
            (b1 + h * b2)));
         ds = 1.9 - s;
         hp = h + 750.0;
         do {
            counter++;
            //if (counter > 100) {
            //   return 0.0;
            //}
            p = p * Math.Exp(21738.0 * ds / hp);
            temp = 100.0;
            t = tpshvx(p, h, temp, 2, 1);
            ds = shvptx(p, t, temp, 1) - s;
            //if (MAJOR_ERROR) return 0.0;
         } while (Math.Abs(ds / s) >= tolerance && counter < 100);

         if (counter == 100) {
            throw new CalculationFailedException("phsd calculation failed.");
         }

         return p;
      }       /*  phsd  */

      private double phsw(double h, double s) {

         /* calculates pressure as a function of enthalpy and entropy in the
            wet region  */

         const double a0 = -4.9071297e1;
         const double a1 = 1.2819008e-1;
         const double a2 = -1.2481385e-4;
         const double a3 = 6.2486862e-8;
         const double a4 = -1.2600857e-11;
         const double b1 = -163.409;
         const double b2 = 1.32775;
         const double b3 = -9.48775e-4;
         const double b4 = 4.74595e-7;
         //const double   tolerance = 5.0e-5;

         double ds, h15, dslope, slope2, slope, pa, p2, h1, h2, p3, h3, t, quality;
         int counter;

         //if (MAJOR_ERROR) return 0.0;
         ds = 1.5 - s;
         slope = 750.0;
         counter = 0;
         do {
            counter++;
            //if (counter > 100) {
            //   return 0.0;
            //}
            h15 = h + slope * ds;
            slope2 = b1 + h15 * (b2 + h15 * (b3 + h15 * b4));
            dslope = Math.Abs(slope2 - slope);
            slope = slope2;
         } while (dslope >= 10.0 && counter < 100);

         if (counter == 100) {
            throw new CalculationFailedException("slope calculation failed.");
         }

         pa = Math.Exp(a0 + h15 * (a1 + h15 * (a2 + h15 * (a3 + h15 * a4))));
         if (pa > PCA) {
            pa = PCA;
         }
         counter = 0;
         quality = xpshv(pa, s, 1);
         t = tpshvx(pa, s, quality, 1, 1);
         h1 = shvptx(pa, t, quality, 2);
         if (quality > 25.0) {
            p2 = 0.95 * pa;
         }
         else {
            p2 = 0.75 * pa;
         }
         quality = xpshv(p2, s, 1);
         t = tpshvx(p2, s, quality, 1, 1);
         h2 = shvptx(p2, t, quality, 2);
         counter = 0;
         do {
            counter++;
            //if (counter > 100) {
            //   return 0.0;
            //}
            p3 = pa * Math.Pow(p2 / pa, (h - h1) / (h2 - h1));
            if (p3 > PMAX) {
               p3 = PMAX;
            }
            if (p3 < PMIN) {
               p3 = PMIN;
            }
            quality = xpshv(p3, s, 1);
            t = tpshvx(p3, s, quality, 1, 1);
            h3 = shvptx(p3, t, quality, 2);
            pa = p2;
            h1 = h2;
            p2 = p3;
            h2 = h3;
         } while (Math.Abs((h - h3) / h) >= 5.0 * tolerance && counter < 100);

         if (counter == 100) {
            throw new CalculationFailedException("phsw calculation failed.");
         }

         return p3;
      }     /*  phsw  */

      private double phs(double h, double s) {

         /*  calculates pressure as a function of enthalpy and entropy  */

         const double a0 = -2.7645397e3;
         const double a1 = 8.6094758e3;
         const double a2 = -1.1059326e4;
         const double a3 = 7.5042872e3;
         const double a4 = -2.8272377e3;
         const double a5 = 5.6093094e2;
         const double a6 = -4.5863755e1;
         const double b1 = -1.3806761;
         const double b2 = 5.1378357e-1;

         double hg;

         //if (MAJOR_ERROR) return 0.0;
         hg = 1000.0 + (a0 + s * (a1 + s * (a2 + s * (a3 + s * (a4 + s *
            (a5 + s * a6)))))) / (1.0 + s * (b1 + s * b2));
         if (h > hg && h > 906.0) {
            return phsd(h, s);
         }
         else {
            return phsw(h, s);
         }
      }           /*  phs  */

      private double vistv(double temperature, double density) {

         /* viscosity (lbfs/ft/ft) as a function of temperature (F), pressure (psia),
             and density (lbm/cuft), uses September 1975 eigth international
             conference on the properties of steam in Giens, France  */

         const double t_star = 647.27;        /*   K           */
         const double den_star = 317.763;     /*   kg/m^3      */
         const double a0 = 0.0181583;
         const double a1 = 0.0177624;
         const double a2 = 0.0105287;
         const double a3 = -0.0036744;
         const double vis_pa_s = 1e-6;            /*  kg/m/s  */

         int i, j;
         double t_over_t_star, t_star_over_t_m1, den_over_den_star,
            den_over_den_star_m1, vis_zero, visptv_temp, t_star_over_t;

         //if (MAJOR_ERROR) return 0.0;
         /*  must convert temperature from degrees F to degrees K  */
         t_over_t_star = (temperature + TZA) / 1.8 / t_star;
         t_star_over_t = 1.0 / t_over_t_star;
         t_star_over_t_m1 = t_star_over_t - 1.0;
         /*  must convert density from lbm/ft^3 to kg/m^3  */
         den_over_den_star = density * 16.018 / den_star;
         den_over_den_star_m1 = den_over_den_star - 1.0;
         vis_zero = vis_pa_s * Math.Sqrt(t_over_t_star) / (a0 + t_star_over_t * (a1
            + t_star_over_t * (a2 + t_star_over_t * a3)));
         visptv_temp = 0;
         for (i = 0; i <= 5; i++) for (j = 0; j <= 4; j++)
               visptv_temp = visptv_temp + b2d[i, j] * Math.Pow(t_star_over_t_m1, i) *
                  Math.Pow(den_over_den_star_m1, j);
         visptv_temp = vis_zero * Math.Exp(den_over_den_star * visptv_temp);
         /*  change units from kg/m/s to lbfs/ft/ft  */
         return visptv_temp * 0.0208854342;
      }     /*  vistv  */

      public double critical_velocity_of_tpqhs_satp(double temperature, double pressure, double quality,
         double enthalpy, double entropy, double saturation_pressure) {

         /*  critical velocity (ft/s) as a function of pressure (psia),
             enthalpy (btu/lbm), entropy (btu/lbm/R), saturation_pressure (psia),
             temperature (F), quality (%)   */

         //const double[]   a = {-2.083333333, 4, -3, 1.33333333, -0.25, -0.83333333, 1.5, -0.5, 0.083333333, -0.6666667};

         int i;
         double c_quality, psat, x, gamma, t;
         double[] pk = new double[9], v_critvel = new double[9];

         //if (MAJOR_ERROR) return 0.0;
         x = 0;
         if (entropy < 1.062279) {
            psat = 0.0;
         }
         else {
            if ((pressure > 2500) && (enthalpy > 1114)) {
               psat = p23t(temperature);
            }
            else {
               psat = saturation_pressure;
            }
         }
         for (i = 0; i <= 8; i++) {
            pk[i] = pressure * (1.0 + 0.01 * (i - 4.0));
            /*  this is to keep from getting error messages at low pressures  */
            if (pk[i] < PMIN) {
               pk[i] = PMIN;
            }
            c_quality = x_pt(pk[i], temperature);
            if (c_quality != quality) {
               pk[i] = saturation_pressure;
            }
            v_critvel[i] = shvptx(pk[i], temperature, quality, 3);
         }
         pk[0] = 0.01 / v_critvel[4];
         if (pressure <= PCA || (pressure > PCA && (pk[4] > psat && pk[6] > psat) || (pk[4] < psat && pk[2] <= psat))) {
            x = a[8] / v_critvel[2] - a[8] / v_critvel[6] +
               a[9] / v_critvel[3] - a[9] / v_critvel[5];
         }
         else {
            if (pk[4] > psat && pk[6] <= psat) {
               pk[0] = -pk[0];
               t = v_critvel[5];
               v_critvel[5] = v_critvel[3];
               v_critvel[6] = v_critvel[2];
               v_critvel[7] = v_critvel[1];
               if (pk[5] <= psat) {
                  v_critvel[8] = v_critvel[0];
                  for (i = 0; i <= 4; i++) {
                     x = x + a[i] / v_critvel[i + 4];
                  }
               }
               else {
                  v_critvel[3] = t;
                  for (i = 4; i <= 8; i++) {
                     x = x + a[i] / v_critvel[i - 1];
                  }
               }
            }
            else {
               if (pk[4] == psat || (pk[4] < psat && pk[2] > psat && pk[3] > psat)) {
                  for (i = 0; i <= 4; i++) {
                     x = x + a[i] / v_critvel[i + 4];
                  }
               }
               else if (pk[4] < psat && pk[2] > psat && pk[3] <= psat) {
                  for (i = 4; i <= 8; i++) {
                     x = x + a[i] / v_critvel[i - 1];
                  }
               }
            }
         }
         if (x == 0) {
            return 0.0;
         }
         else {
            gamma = pk[0] / x;
            return Math.Sqrt(4633.06 * gamma * pressure * v_critvel[4]);
         }
      }           /*  critical_velocity_of_tpqhs_satp  */
   }
}
