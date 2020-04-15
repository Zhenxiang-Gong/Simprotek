//  steam1.c


#define   AK1      -7.691234564
#define   AK2      -26.08023696
#define   AK3      -168.1706546
#define   AK4      64.23285504
#define   AK5      -118.9646225
#define   AK6      4.167117320
#define   AK7      20.97506760
#define   AK8      1.0e9
#define   AK9      6.0
#define   AK2T2    -52.16047392      /*  ak2 times 2  */
#define   AK3T3    -504.5119638      /*  ak3 times 3  */
#define   AK4T4    256.93142016      /*  ak4 times 4  */
#define   AK5T5    -594.8231125      /*  ak5 times 5  */
#define   AK7T2    41.9501352        /*  ak7 times 2  */
#define   AK8T2    2.0e9             /*  ak8 times 2  */


double psl (double *temperature, double *dpdt)

     /*     saturation pressure as a function of temperature,
            arguement is temperature in degrees fahrenheit,
            returns with pressure in psia (as the function PSL) and
            the passed thru variable Dpdt which is used in iterating
            for temperature.
     */

   {
   double    y, x1p, ppsl, den1, den2, dsdt, b, dbdt, dbbdt, theta;

   if (MAJOR_ERROR) return 0.0;
   if (*temperature > (TC + 0.1)) 
   {
      char str [256];
	  
      sprintf (str, "Psl - %10.3f F out of temperature range",
               *temperature);
      MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
      MAJOR_ERROR = TRUE;
      MAJOR_ERROR_TYPE = 3;
      return 0.0;
   }
   theta = (*temperature + TZA) / TCA;
   x1p = 1.0 - theta;
   y = x1p * (AK1 + x1p * (AK2 + x1p * (AK3 + x1p * (AK4 + x1p * AK5))));
   den1 = 1.0 + x1p * (AK6 + x1p * AK7);
   den2 = AK8 * x1p * x1p + AK9;
   b = y / theta / den1 - x1p / den2;
   ppsl = PCA * exp (b);
   dsdt = -(AK1 + x1p * (AK2T2 + x1p * (AK3T3 + x1p * (AK4T4 + AK5T5 * x1p))));
   b = theta * den1;
   dbdt = den1 - theta * (AK6 + AK7T2 * x1p);
   dbbdt = -AK8T2 * x1p;
   *dpdt = ppsl / TCA * ((b * dsdt - y * dbdt) / b / b) + (den2 + x1p *
           dbbdt / den2 / den2) / 1e11;
   return ppsl;
   }   /*  psl  */


double p23t (double *temperature)

   /* given temperature in degrees F, calculates the pressure
      in psia for the boundary between regions 2 and 3  */

{
     double theta;

     if (MAJOR_ERROR) return 0.0;
     theta = (*temperature + TZA) / TCA;
     return (AL0 + theta * (AL1 + theta * AL2)) * PCA;
}          /*  p23t  */


double tsl (double *pressure)

     /*  given pressure in psia, gives saturation temperature in degrees F  */

{
   double b [12] = {1.52264682686,       -0.682309517937,
                    0.164114951723,      -2.02321648831e-3,
                    1.92391110748e-3,    -5.74549418696e-4,
                    6.84115542402e-5,    3.36500068426e-5,
                    -1.23422483951e-5,   1.48266501702e-6,
                    -1.02116445578e-6,   -4.09080904092e-6};
   double pcat1p1 = 3529.058214;            /*   pca times 1.1  */

   double       pa, y, tz, ty, tx, w, ttsl, tol, dpdt, dp, pr, f;
   int          tsl_counter;

   if (MAJOR_ERROR) return 0.0;
   if ((*pressure > PCA) || (*pressure < PMIN)) 
   {
      char str [256];
	  
      sprintf (str, "Tsl - %10.3f psia out of pressure range",
               *pressure);
      MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
      MAJOR_ERROR = TRUE;
      MAJOR_ERROR_TYPE = 4;
      return 0.0;
   }
   tol = 1.0e-6;
   if (*pressure > 2700) tol = 1.0e-9;
   if (*pressure > 3200) tol = 1.0e-10;
   f = 1.0;
   tx = 1.0;
   ty = (exp (0.4 * log (log (pcat1p1 / *pressure))) - 1.4804712555) /
             -1.089944005;
   y = 2.0 * ty;
   w = b [0] + ty * b [1];
   for (tsl_counter = 2; tsl_counter <= 11; tsl_counter++) {
      tz = y * ty - tx;
      w = w + tz * b [tsl_counter];
      tx = ty;
      ty = tz;
      }
   ttsl = TCA / w - TZA;
   ty = 0.01;
   if (ttsl > TC) ttsl = TC;
   tsl_counter = 1;
   do {
      pa = psl (&ttsl, &dpdt);
      dp = *pressure - pa;
      if (*pressure > 0) pr = dp / *pressure;
      else pr = dp;
      ttsl = ttsl + f * dp / dpdt;
      f = f * 0.99;
      if (ttsl > TC) {
         ty = 0.9 * ty;
         ttsl = ttsl - ty;
         }
      if (tsl_counter > 200) 
	  {
         char str [256];
 	  
         strcpy (str, "Tsl - 200 iterations (too many for convergence)");
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 5;
         return 0.0;
      }
      tsl_counter ++;
      } while (fabs (pr) >= tol);
   return ttsl;
}               /*  tsl  */


#define   AA0        6.824687741e3
#define   AA1        -5.422063673e2
#define   AA3        3.941286787e4
#define   AA11       7.982692717
#define   AA12       -2.616571843e-2
#define   AA14       2.284279054e-2
#define   AA15       2.421647003e2
#define   AA16       1.269716088e-10
#define   AA20       1.293441934e1
#define   AA21       1.308119072e-5
#define   A1         8.438375405e-1
#define   A4         7.342278489e-2
#define   AA8        -4.511168742e4
#define   A6         6.5371543e-1
#define   AA9        1.418138926e4
#define   A7         1.15e-6
#define   AA10       -2.017271113e3
#define   A8         1.5108e-5
#define   AA17       2.074838328e-7
#define   A12        2.04e-1
#define   AA18       2.17402035e-8
#define   AA19       1.105710498e-9
#define   A10        7.002753165
#define   A11        2.995284926e-4
#define   A3         1.72
#define   A5         4.97585887e-2
#define   A2         5.362162162e-4
#define   AA5        9.902381028e4
#define   AA2        -2.096666205e4
#define   AA4        -6.733277739e4
#define   AA13       1.52241179e-3
#define   AA6        -1.093911774e5
#define   A9         1.4188e-1
#define   AA7        8.590841667e4
#define   AA22       6.047626338e-14
#define   A2T6       32.172972972e-4          /*    a2 times 6          */
#define   A1T2       16.876750810e-1          /*    a1 times 2          */
#define   M5D17      -0.29411765              /*    -5 divided by 17    */
#define   A4T2       14.684556978e-2          /*    a4 times 2          */
#define   A5T2       9.95171774e-2            /*    a5 times 2          */
#define   AA22T20    120.952526760e-14        /*    aa22 times 20, s    */
#define   A9T18      25.5384e-1               /*    a9 times 18, s      */
#define   AA14T2     4.568558108e-2           /*    aa14 times 2, s     */
#define   AA15T10    24.21647003e2            /*    aa15 times 10, s    */
#define   AA16T19    24.124605672e-10         /*    aa16 times 19, s    */
#define   P5D12      0.41666667               /*    5 divided by 12, s  */
#define   A3M1       0.72                     /*    a3 minus 1, s       */
#define   AA3T2      7.882573574e4            /*    aa3 times 2, s      */
#define   AA4T3      -20.199833217e4          /*    aa4 times 3, s      */
#define   AA5T4      39.609524112e4           /*    aa5 times 4, s      */
#define   AA6T5      -5.469558870e5           /*    aa6 times 5, s      */
#define   AA7T6      51.545050002e4           /*    aa7 times 6, s      */
#define   AA8T7      -31.578181194e4          /*    aa8 times 7, s      */
#define   AA9T8      11.345111408e4           /*    aa9 times 8, s      */
#define   AA10T9     -18.155440017e3          /*    aa10 times 9, s     */
#define   AA21TA12   0.266856290688e-5        /*    aa21 times a12      */
#define   AA22T21    127.000153098e-14        /*    aa22 times 21       */
#define   A9T17      24.1196e-1               /*    a9 times 17         */
#define   P17D29     0.58620690               /*    17 divided by 29    */
#define   P17D12     1.41666667               /*    17 divided by 12    */
#define   AA4T2      -13.466555478e4          /*    aa4 times 2         */
#define   AA5T3      29.707143084e4           /*    aa5 times 3         */
#define   AA6T4      -4.375647096e5           /*    aa6 times 4         */
#define   AA7T5      42.954208335e4           /*    aa7 times 5         */
#define   AA8T6      -27.067012452e4          /*    aa8 times 6         */
#define   AA9T7      9.926972482e4            /*    aa9 times 7         */
#define   AA10T8     -16.138168904e3          /*    aa10 times 8        */
#define   AA21T3     3.924357216e-5           /*    a21 times 3         */
#define   AA22T4     24.190505352e-14         /*    a22 times 4         */
#define   AA18T2     4.34804070e-8            /*    aa18 times 2        */
#define   AA19T3     3.317131494e-9           /*    aa19 times 3        */
#define   AA11TA5    39720752362.3688e-11     /*    aa11 times a5       */


double shvpt1 (double *pressure, double *temperature, int option)

     /*  given pressure in psia, temperature in F, produces sub region 1
         properties

           option          property
           ------         ----------------------------
             1            entropy (btu/lbm/R)
             2            enthalpy (btu/lbm)
             3            volume (cuft/lbm)                */

     {
     double       beta, beta2, beta3;
     double       theta, th2, th6, th7, th10, th11, th17, th18, th19, th20,
                       th21;
     double       shv0, shv1, shv2, shv3, shv4, shv5;
     double       ua, ua9, ua10, ub, ub2, uc, u3t, ud, ud3, ud4, up, y,
                       z, yp, zp;

     if (MAJOR_ERROR) return 0.0;
     if ((*pressure > PMAX) || (*pressure < PMIN)) 
	 {
         char str [256];
 	  
         sprintf (str, "SHVpt1 - %15.8f psia out of range",
                  *pressure);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 6;
         return 0.0;
     }
     if ((*temperature > T1MAX) || (*temperature < TMIN)) 
	 {
         char str [256];
 	  
         sprintf (str, "SHVpt1 - %15.8f F out of range",
                  *temperature);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 7;
         return 0.0;
     }
     beta = *pressure / PCA;
     beta2 = beta * beta;
     beta3 = beta2 * beta;
     theta = (*temperature + TZA) / TCA;
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
     ua9 = pow (ua, 9);
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
     z = y + sqrt (A3 * y * y + A5T2 * beta - A4T2 * theta);
     yp = A2T6 / th7 - theta * A1T2;
     zp = exp (M5D17 * log (z));
     switch (option) {
        case 1 : {
            shv5 = beta3 * (AA21 + AA22T20 * beta / th21);
            shv4 = AA20 * th17 * (A9T18 + 20.0 * th2) * up;
            shv3 = 11.0 * th10 * u3t;
            shv2 = (-AA13 - AA14T2 * theta + AA15T10 * ua9 +
                  AA16T19 * th18 / ub2) * beta;
            shv1 = AA11 * ((P5D12 * z - A3M1 * y) * yp + A4) * zp;
            shv0 = AA0 * log (theta) - (AA2 + theta * (AA3T2 +
                  theta * (AA4T3 +
                  theta * (AA5T4 + theta * (AA6T5 +
                  theta * (AA7T6 + theta *
                  (AA8T7 + theta * (AA9T8 + theta * AA10T9))))))));
            return ((shv0 + shv1 + shv2 - shv3 + shv4 + shv5 - ALPHA1) * PVOT);
            }
        case 2 : {
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
        case 3 : {
            shv4 = beta2 * (AA21T3 * (A12 - theta) + AA22T4 * beta / th20);
            shv3 = AA20 * th18 * (A9 + th2) * (A11 - 3.0 / ud4);
            shv2 = (AA17 + beta * (AA18T2 + AA19T3 * beta)) / uc;
            shv1 = AA12 + theta * (AA13 + theta * AA14) + AA15 * ua10 +
                 AA16 / ub;
            return ((AA11TA5 * zp + shv1 - shv2 - shv3 + shv4) * VCA);
            }
        }
     }           /*  shvpt1   */


#define   B00        7.6333333333e-1
#define   B61        4.006073948e-1
#define   B71        8.636081627e-2
#define   B81        -8.532322921e-1
#define   B82        3.460208861e-1
#define   BB11       6.670375918e-2
#define   BB12       1.388983801
#define   BB21       8.390104328e-2
#define   BB22       2.614670893e-2
#define   BB23       -3.373439453e-2
#define   BB31       4.520918904e-1
#define   BB32       1.069036614e-1
#define   BB41       -5.975336707e-1
#define   BB42       -8.847535804e-2
#define   BB51       5.958051609e-1
#define   BB52       -5.159303373e-1
#define   BB53       2.075021122e-1
#define   BB62       -9.867174132e-2
#define   BB61       1.190610271e-1
#define   BB71       1.683998803e-1
#define   BB72       -5.809438001e-2
#define   BB81       6.552390126e-3
#define   BB82       5.710218649e-4
#define   BB90       1.936587558e2
#define   BB91       -1.388522425e3
#define   BB92       4.126607219e3
#define   BB93       -6.508211677e3
#define   BB94       5.745984054e3
#define   BB95       -2.693088365e3
#define   BB96       5.235718623e2
#define   BB00       1.683599274e1
#define   BB05       8.565182058e-2
#define   BB02       -5.438923329e1
#define   BB04       -6.547711697e-1
#define   BB01       2.856067796e1
#define   BB03       4.330662834e-1
#define   B61T14     56.085035272e-1          /*  b61 times 14          */
#define   B71T19     164.085550913e-2         /*  b71 times 19          */
#define   B81T2T27   -460.745437734e-1        /*  b81 times 2 times 27  */
#define   B82T27     93.425639247e-1          /*  b82 times 27          */
#define   AL1T10     -34.17061978e1           /*  al1 times 10          */
#define   AL2T2T10   38.62761414e1            /*  al2 times 2 times 10  */
#define   BB03T2     8.661325668e-1           /*  bbo3 times 2          */
#define   BB04T3     -19.643135091e-1         /*  bb04 times 3          */
#define   BB05T4     34.260728232e-2          /*  bb05 times 4          */
#define   BB11T13    86.714886934e-2          /*  bb11 times 13         */
#define   BB12T3     4.166951403              /*  bb12 times 3          */
#define   BB21T18    151.021877904e-2         /*  bb21 times 18         */
#define   BB22T2     5.229341786e-2           /*  bb22 times 2          */
#define   BB31T18    81.376540272e-1          /*  bb31 times 18         */
#define   BB32T10    10.69036614e-1           /*  bb32 times 10         */
#define   BB41T25    -149.383417675e-1        /*  bb41 times 25         */
#define   BB42T14    -123.865501256e-2        /*  bb42 times 14         */
#define   BB51T32    1.906576515e1            /*  bb51 times 32         */
#define   BB52T28    -1.444604944e1           /*  bb52 times 28         */
#define   BB53T24    4.980050693              /*  bb53 times 24         */
#define   B00T2      1.526666666667           /*  b00 times 2           */
#define   B00T3      2.29                     /*  b00 times 3           */
#define   B00T4      3.053333333333           /*  b00 times 4           */
#define   B00T5      3.816666666667           /*  b00 times 5           */
#define   B00T6      4.58                     /*  b00 times 6           */
#define   BB04T2     -1.309542339             /*  bb04 times 2          */
#define   BB05T3     2.569554617e-1           /*  bb05 times 3          */
#define   BB21T2     16.780208656e-2          /*  bb21 times 2          */
#define   BB23T2     -6.746878906e-2          /*  bb23 times 2          */
#define   BB31T3     13.562756712e-1          /*  bb31 times 3          */
#define   BB32T3     3.207109842e-1           /*  bb32 times 3          */
#define   BB41T4     -23.901346828e-1         /*  bb41 times 4          */
#define   BB42T4     -35.390143216e-2         /*  bb42 times 4          */
#define   BB51T5     29.790258045e-1          /*  bb51 times 5          */
#define   BB52T5     -25.796516865e-1         /*  bb52 times 5          */
#define   BB53T5     10.375105610e-1          /*  bb53 times 5          */


double shvpt2 (double *pressure, double *temperature, int option)

      /* sub region 2, given pressure in psia and temperature in degrees F,

                   option      property
                   ------      --------------------------------
                      1        entropy (btu/lbm/R)
                      2        enthalpy (btu/lbm)
                      3        volume (cuft/lbm)                     */

     {
     double    d2, d3, d4, both, betal, t2, t3, t4, fb, bb61f, bb71f,
               theta, th2, bb81f, boblp;
     double    shv2a, shv3a, shv4a, shv9, shv4, shv3, shv2, shv1, shv0;
     double    x1, x2, x3, x4, x6, x8, x10, x11, x13, x14, x17, x18,
               x19, x24, x25, x27, x28, x32;
     double    beta, beta4, beta5, beta6, beta7;

     if (MAJOR_ERROR) return 0.0;
     if ((*pressure > PMAX) || (*pressure < PMIN)) 
	 {
         char str [256];
 	  
         sprintf (str, "SHVpt2 - %10.3f psia out of range",
                  *pressure);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 8;
         return 0.0;
     }
     if ((*temperature > TMAX) || (*temperature < T2MIN)) 
	 {
         char str [256];
 	  
         sprintf (str, "SHVpt2 - %10.3f F out of range",
                  *temperature);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 9;
         return 0.0;
     }
     beta = *pressure / PCA;
     theta = (*temperature + TZA) / TCA;
     th2 = theta * theta;
     x1 = exp (B00 * (1.0 - theta));
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
     beta4 = pow (beta, 4);
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
     boblp = pow (beta / betal, 10);
     switch (option) {
        case 1 : {
            shv0 = BB00 * log (theta) - I1 * log (beta) - BB02 - theta *
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
        case 2 : {
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
        case 3 : {
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
     }               /*  shvpt2   */


#define    C00       -6.8399
#define    C50       2.10636332e2
#define    C11       7.08636085e-1
#define    C12       1.23679455e1
#define    C13       -1.20389004e1
#define    C14       5.40437422
#define    C15       -9.93865043e-1
#define    C16       6.27523182e-2
#define    C17       -7.74743016
#define    C22       4.31430538e1
#define    C23       -1.41619313e1
#define    C24       4.04172459
#define    C25       1.55546326
#define    C26       -1.66568935
#define    C27       3.24881158e-1
#define    C28       2.93655325e1
#define    C32       8.08859747e1
#define    C33       -8.3615338e1
#define    C34       3.58636517e1
#define    C35       7.51895954
#define    C36       -1.2616064e1
#define    C37       1.09717462
#define    C38       2.12145492
#define    C39       -5.46529566e-1
#define    C310      8.32875413
#define    C62       3.69707142e-1
#define    C64       6.828087013e-2
#define    C70       -2.571600553e2
#define    C71       -1.518783715e2
#define    C72       2.220723208e1
#define    C73       -1.80203957e2
#define    C74       2.35709622e3
#define    C75       -1.462335698e4
#define    C76       4.54291663e4
#define    C77       -7.053556432e4
#define    C78       4.381571428e4
#define    D30       -1.717616747
#define    D31       3.526389875
#define    D32       -2.690899373
#define    D33       9.070982605e-1
#define    D34       -1.138791156e-1
#define    D40       1.301023613
#define    D41       -2.642777743
#define    D42       1.996765362
#define    D43       -6.661557013e-1
#define    D44       8.270860589e-2
#define    D50       3.426663535e-4
#define    D51       -1.2365212578e-3
#define    D52       1.155018309e-3
#define    C21       -4.29885092
#define    C31       7.94841842e-6
#define    C40       2.75971776e-6
#define    C41       -5.09073985e-4
#define    C60       5.528935335e-2
#define    C03       4.20460752
#define    C61       -2.336365955e-1
#define    C04       -2.76807038
#define    C63       -2.59641547e-1
#define    C05       2.10419707
#define    C012      -4.311577033
#define    C06       -1.14649588
#define    C01       -1.7226042e-2
#define    C07       2.23138085e-1
#define    C02       -7.77175039
#define    C08       1.16250363e-1
#define    C09       -8.20900544e-2
#define    C010      1.94129239e-2
#define    C011      -1.69470576e-3
#define    C60T2     1.105787067e-1        /*  c60 times 2              */
#define    C61T3     -7.009097865e-1       /*  c61 times 3              */
#define    C62T4     1.478828568           /*  c62 times 4              */
#define    C63T5     -1.298207735          /*  c63 times 5              */
#define    C64T6     4.096852208e-1        /*  c64 times 6              */
#define    C71T2     -3.03756743e2         /*  c71 times 2              */
#define    C72T3     6.662169624e1         /*  c72 times 3              */
#define    C73T4     -7.20815828e2         /*  c73 times 4              */
#define    C74T5     1.17854811e4          /*  c74 times 5              */
#define    C75T6     -8.774014188e4        /*  c75 times 6              */
#define    C76T7     3.180041641e5         /*  c76 times 7              */
#define    C77T8     -5.642845147e5        /*  c77 times 8              */
#define    C78T9     3.943414285e5         /*  c78 times 9              */
#define    C00MC012MC50   -213.16465497  /* c00 minus c012 minus c50, h */
#define    C02T2     -1.554350078e1        /*  c02 times 2,h            */
#define    C03T3     1.261382256e1         /*  c03 times 3,h            */
#define    C04T4     -1.107228152e1        /*  c04 times 4, h           */
#define    C05T5     1.052098535e1         /*  c05 times 5, h           */
#define    C06T6     -6.87897528           /*  c06 times 6, h           */
#define    C07T7     1.561966595           /*  c07 times 7, h           */
#define    C08T8     9.30002904e-1         /*  c08 times 8, h           */
#define    C09T9     -7.388104896e-1       /*  c09 times 9, h           */
#define    C010T10   1.94129239e-1         /*  c010 times 10, h         */
#define    C011T11   -1.864176336e-2       /*  c011 times 11, h         */
#define    C012MC17  3.43585313            /*  c012 minus c17, h        */
#define    MC17MC50  -202.88890184         /*  minus c17 minus c50, h   */
#define    C21T2     -8.59770184           /*  c21 times 2, h           */
#define    C13T2     -2.40778008e1         /*  c13 times 2, h           */
#define    C14T3     1.621312266e1         /*  c14 times 3, h           */
#define    C15T4     -3.975460172          /*  c15 times 4, h           */
#define    C16T5     3.13761591e-1         /*  c16 times 5, h           */
#define    C28T2     5.8731065e1           /*  c28 times 2, h           */
#define    C21T2PC31T3   -8.59767799 /* c21 times 2 plus c31 times 3, h */
#define    C24T2     8.08344918            /*  c24 times 2, h           */
#define    C25T3     4.66638978            /*  c25 times 3, h           */
#define    C26T4     -6.6627574            /*  c26 times 4, h           */
#define    C27T5     1.62440579            /*  c27 times 5, h           */
#define    C28PC310T3   54.35179489     /*  c28 plus (c310 times 3), h  */
#define    C31T3     2.384525526e-5        /*  c31 times 3, h           */
#define    C35T2     1.503791908e1         /*  c35 times 2, h           */
#define    C36T3     -3.7848192e1          /*  c36 times 3,  h          */
#define    C37T4     4.38869848            /*  c37 times 4, h           */
#define    C38T5     1.06072746e1          /*  c38 times 5, h           */
#define    C39T6     -3.279177396          /*  c39 times 6, h           */
#define    C310T2    1.665750826e1         /*  c310 times 2, h          */
#define    C40T23    6.347350848e-5        /*  c40 times 23, h          */
#define    C41T28    -1.425407158e-2       /*  c41 times 28, h          */
#define    C40T24    6.623322624e-5        /*  c40 times 24, h          */
#define    C41T29    -1.476314557e-2       /*  c41 times 29, h          */
#define    C60T3     1.6586806e-1          /*  c60 times 3, h           */
#define    C61T2     -4.67273191e-1        /*  c61 times 2, h           */
#define    C03T2     8.40921504            /*  c03 times 2              */
#define    C04T3     -8.30421114           /*  c04 times 3              */
#define    C05T4     8.41678828            /*  c05 times 4              */
#define    C06T5     -5.7324794            /*  c06 times 5              */
#define    C07T6     1.33882851            /*  c07 times 6              */
#define    C08T7     8.13752541e-1         /*  c08 times 7              */
#define    C09T8     -6.567204352e-1       /*  c09 times 8              */
#define    C010T9    1.747163151e-1        /*  c010 times 9             */
#define    C011T10   -1.69470576e-2        /*  c011 times 10            */
#define    C23T2     -2.83238626e1         /*  c23 times 2              */
#define    C24T3     1.212517377e1         /*  c24 times 3              */
#define    C25T4     6.22185304            /*  c25 times 4              */
#define    C26T5     -8.32844675           /*  c26 times 5              */
#define    C27T6     1.949286948           /*  c27 times 6              */
#define    C33T2     -1.67230676e2         /*  c33 times 2              */
#define    C34T3     1.075909551e2         /*  c34 times 3              */
#define    C35T4     3.007583816e1         /*  c35 times 4              */
#define    C36T5     -6.308032e1           /*  c36 times 5              */
#define    C37T6     6.58304772            /*  c37 times 6              */
#define    C38T7     1.485018444e1         /*  c38 times 7              */
#define    C39T8     -4.372236528          /*  c39 times 8              */
#define    C41T5     -2.545369925e-3       /*  c41 times 5              */
#define    D32T2     -5.381798746          /*  d32 times 2              */
#define    D33T3     2.721294782           /*  d33 times 3              */
#define    D34T4     -4.555164624e-1       /*  d34 times 4              */
#define    D42T2     3.993530724           /*  d42 times 2              */
#define    D43T3     -1.998467104          /*  d43 times 3              */
#define    D44T4     3.308344236e-1        /*  d44 times 4              */
#define    D52T2     2.310036618e-3        /*  d52 times 2              */


double shpvt3 (double *volume, double *temperature, int option)

     /*  given volume in cuft/lbm and temperature in degrees F, sub region 3

             option         calculates property
             ------         -----------------------
               1            entropy (btu/lbm/R)
               2            enthalpy (btu/lbm)
               3            pressure (psia)           */

{
     double    ex, exm1, exlog, exm5, thm1, thm22, thl1, omth1, y2, y3,
                   thm23, exm6, x50th2, x60th2, y31, theta1, exm2,
                   fth3, fth4, fth32;
     double    shp4, shp3, shp2, shp1, shp0,
                  shp6, shp7, shp8, shp8a, shp8b, shp8c, shp7a, shp7b, shpa;
     double    theta, y;

     if (MAJOR_ERROR) return 0.0;
     if ((*volume > V3MAX) || (*volume < V3MIN)) 
	 {
         char str [256];
 	  
         sprintf (str, "SHPvt3 - %10.3f cuft/lbm out of range",
                  *volume);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 10;
         return 0.0;
     }
     if ((*temperature > T3MAX) || (*temperature < T3MIN)) 
	 {
         char str [256];
 	  
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         sprintf (str, "SHPvt3 - %10.3f F out of range",
                  *temperature);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 11;
         return 0.0;
     }
     ex = *volume / VCA;
     theta = (*temperature + TZA) / TCA;
     thl1 = theta - 1.0;
     theta1 = (T1 + TZA) / TCA;
     omth1 = 1.0 - theta1;
     y = (1.0 - theta) / omth1;
     y2 = y * y;
     y3 = y * y2;
     y31 = pow (y, 31);
     exm1 = VCA / *volume;
     exm2 = exm1 * exm1;
     exm5 = exm2 * exm2 * exm1;
     exm6 = exm5 * exm1;
     exlog = log (ex);
     thm1 = 1.0 / theta;
     thm22 = pow (thm1, 22);
     thm23 = thm22 * thm1;
     x50th2 = thm1 * thm1 / exm5;
     x60th2 = ex * x50th2;
     switch (option) {
        case 1 : {
            shp1 = C11 * ex + exm1 * (C12 + exm1 * (C13 +
                  exm1 * (C14 + exm1 * (C15
                  + exm1 * C16)))) + C17 * exlog + C50;
            shp2 = 2.0 * (C21 * ex + exm1 * (C22 + exm1 *
                  (C23 + exm1 * (C24 + exm1 *
                  (C25 + exm1 * (C26 + exm1 * C27))))) + C28 * exlog);
            shp3 = 3.0 * (C31 * ex + exm1 * (C32 + exm1 *
                  (C33 + exm1 * (C34 + exm1 *
                  (C35 + exm1 * (C36 + exm1 * (C37 + exm1 * (C38 + exm1 *
                  C39))))))) + C310 * exlog);
            shp4 = (C40 + C41 * exm5) * (22.0 - 23.0 * thm1) * thm23 - C50 *
                  log (theta);
            shp6 = (x60th2 / theta) * (C60T2 + thm1 * (C61T3 + thm1 * (C62T4 +
                  thm1 * (C63T5 + thm1 * C64T6))));
            shp7 = C70 + thl1 * (C71T2 + thl1 * (C72T3 +
                  thl1 * (C73T4 + thl1 *
                  (C74T5 + thl1 * (C75T6 + thl1 * (C76T7 +
                  thl1 * (C77T8 + thl1 * C78T9)))))));
	    if (theta >= 1.0 || ex >= 1.0) shp8 = 0.0;
            else {
		 shp8a = y2 * (3.0 * (D30 + exm1 * (D31 + exm1 * (D32 +
                         exm1 * (D33 + exm1 * D34)))) +
                         4.0 * y * (D40 + exm1 * (D41 + exm1 * (D42 +
                         exm1 * (D43 + exm1 * D44)))));
                 shp8b = 32.0 * y31 * (D50 + ex * (D51 + ex * D52));
                 shp8 = (shp8a + shp8b) / omth1;
                 }
            return (-shp1 - thl1 * (shp2 + shp3 * thl1) + shp4 + shp6 -
                    shp7 + shp8 - ALPHA1) * PVOT;
            }
        case 2 : {
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
            if ((theta >= 1.0) || (ex >= 1.0)) shp8 = 0.0;
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
            return (shp0 + thl1 * (shp1 + thl1 * (shp2 + thl1 * shp3)) + shp4 +
                    shp6 - shp7 + shp8 + ALPHA0) * PVO1O;
            }
        case 3 : {
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
            if ((theta >= 1.0) || (ex >= 1.0)) shp7 = 0.0;
            else {
                 shp7a = y3 * (exm2 * (D31 + exm1 * (D32T2 +
                       exm1 * (D33T3 + exm1 * D34T4))) +
                       y * exm2 * (D41 + exm1 * (D42T2 +
                       exm1 * (D43T3 + exm1 * D44T4))));
                 shp7b = y * y31 * (D51 + D52T2 * ex);
                 shp7 = shp7a - shp7b;
                 }
            return (-shp0 + thl1 * (-shpa - thl1 * (shp2 + shp3 * thl1) + shp4)
                      - shp6 + shp7) * PCA;
            }
        }
}            /*  shpvt3  */
