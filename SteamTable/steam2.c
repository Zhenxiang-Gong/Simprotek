//  steam2.c


double vptx3 (double *pressure, double *temperature, double *quality)

     /*  given pressure in psia, temperature in degrees F, and steam quality
         (0-100), calculates specific volume in cuft/lbm, sub region 3 }  */

{
     const double         tolerance = 1.0e-5;

     double         b_vptx3, b_vptx3a, v, dv, pcalc, pct, vprev;
     static double  vptx3_save_0, vptx3_save_100;
     double         df, pprev, pctp, vup, vlow;
     int            vptx3_counter;

     if (MAJOR_ERROR) return 0.0;
     VPTX3_CALL++;
     if (*temperature >= 705.0 && *temperature <= 705.47) {
        v = 0.05078 + (100.0 - *quality) / 100.0 * (0.05078 - 0.04427) *
            (*temperature - 705.47) / 0.47 +
            *quality / 100.0 * (0.05078 - 0.05730) *
            (*temperature - 705.47) / 0.47;
        vup = V3MAX;
        vlow = V3MIN;
        }
     else {
        if (*quality == 0) {
           if (VPTX3_ALREADY_SET_0) return vptx3_save_0;
           v = (0.077 + 92.8 / *pressure) * (*temperature - 482.0) /
                 (*temperature + 58.0);
           b_vptx3 = *temperature / TC;
           b_vptx3a = *pressure / PCA;
           if (b_vptx3a > b_vptx3) b_vptx3 = b_vptx3a;
           vup = VCA * b_vptx3 * b_vptx3;
           vlow = V3MIN;
           }
        else {
           if (VPTX3_ALREADY_SET_100) return vptx3_save_100;
           v = 0.3 * (*temperature + TZA) / *pressure;
           vup = V3MAX;
           vlow = V3MIN;
           }
        }
     if ((*pressure > PMAX) || (*pressure < P3MIN)) 
	 {
         char str [256];
 	  
         sprintf (str,
                  "Vptx3 - the pressure (%10.3f psia) is out of range",
                  *pressure);
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 12;
         return 0.0;
     }
     df = 0.25;
     vptx3_counter = 0;

     do {
        vptx3_counter ++;
        if (vptx3_counter > 100) 
		{
           char str [256];
 	  
           sprintf (str,
              "Vptx3 - 100 iteration maximum limit exceeded (Call number %d)\n",
              "Actual pressure = %11.3f psia <> Calculated pressure = %11.3 psia\n",
              "Temperature = %11.3f F   <===>   Quality = %11.6f %",
              VPTX3_CALL, *pressure, pcalc, *temperature, *quality);
           MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
           MAJOR_ERROR = TRUE;
           MAJOR_ERROR_TYPE = 13;
           return 0.0;
        }
        if (v > vup) {
            v = vup;
            df = 1;
            }
        if (v < vlow) {
            v = vlow;
            df = 1;
            }
        pcalc = shpvt3 (&v, temperature, 3);
	if (MAJOR_ERROR) return 0.0;
        pct = (pcalc - *pressure) / *pressure;
        if (vptx3_counter == 1) dv = v * pct * df;
        else {
             if ((pctp * pct) > 0) {
                if (fabs (pct) > fabs (0.3 * pctp)) {
                   df = 1.5 * df;
                   dv = v * pct * df;
                   }
                else dv = (v - vprev) * (*pressure - pcalc) / (pcalc - pprev);
                }
             else {
                  df = df * 0.67;
                  dv = (v - vprev) * (*pressure - pcalc) / (pcalc - pprev);
                  }
             }
        if (fabs (pct) >= tolerance) {
           vprev = v;
           pprev = pcalc;
           pctp = pct;
           v = v + dv;
           }
        } while (fabs (pct) >= tolerance);

     if (quality == 0) {
         VPTX3_ALREADY_SET_0 = TRUE;
         vptx3_save_0 = v;
         }
     else {
          VPTX3_ALREADY_SET_100 = TRUE;
          vptx3_save_100 = v;
          }
     return v;
}            /*  vptx3  */


double shvptx (double *pressure, double *temperature,
               double *quality, int option)

    /* function of pressure (psia), temperature (F), steam quality (0-100 %)

                  option                          shvptx
                  ------          -------------------------------------------
                     1            specific entropy (btu/lbm/f)
                     2            specific enthalpy (btu/lbm)
                     3            specific volume (cuft/lbm)

      */

{
     double    shvptd, shvptl, temp;

     if (MAJOR_ERROR) return 0.0;
     if (*quality > 0) {
        if (*pressure > p23t (temperature)) {
           temp = 100.0;
           shvptd = vptx3 (pressure, temperature, &temp);
           if (option == 1 || option == 2)
              shvptd = shpvt3 (&shvptd, temperature, option);
           }
        else shvptd = shvpt2 (pressure, temperature, option);
        }
     if (*quality < 100.0) {
        if (*temperature > T1) {
           temp = 0.0;
           shvptl = vptx3 (pressure, temperature, &temp);
           if (option == 1 || option == 2)
              shvptl = shpvt3 (&shvptl, temperature, option);
           }
        else shvptl = shvpt1 (pressure, temperature, option);
        }
     return (100.0 - *quality) / 100.0 * shvptl + *quality / 100.0 * shvptd;
}   /*  shvptx  */


double pascal near x_pt (double *pressure, double *temperature)

        /*  quality as a function of pressure and temperature  */

{
     double temp_sat;

     if (MAJOR_ERROR) return 0.0;
        /* if above crit temp then superheated */
     if (*temperature > TC) return 100.0;
     else {
          if (*pressure <= PCA) {  /* only if less than critical pres */
             temp_sat = tsl (pressure);
             if (*temperature > temp_sat) return 100.0;
             if (*temperature < temp_sat) return 0.0;
             }
               /*  compressed supercritical liquid  */
          else return 0.0;
          }
}   /*  x_pt  */


double xpshv (double *pressure, double *shv, int option)

     /*  quality as a function of pressure and shv
            option      shv
            ------    -------
               1      entropy
               2      enthalpy
               3      specific volume
     */

{
     double   saturation_temperature, shvg, shvf, temp;

     if (MAJOR_ERROR) return 0.0;
        /*  if above critical pressure,use critical temperature  */
     if (*pressure <= PCA)  {
         saturation_temperature = tsl (pressure);
         temp = 100.0;
         shvg = shvptx (pressure, &saturation_temperature, &temp, option);
         if (*shv < shvg) {
                /*  point is wet  */
             temp = 0.0;
             shvf = shvptx (pressure, &saturation_temperature, &temp, option);
             if (*shv > shvf) return (*shv - shvf) / (shvg - shvf) * 100;
             else return 0.0;
             }
         else return 100.0;
         }
     else {
          shvf = TC;    /*  cannot use constant in function call  */
             /*  must use quality of 0 since the fluid is always pressurized
                 liquid at 705.47 F from 3208.2 psia to 16000 psia  */
          temp = 0;
          shvf = shvptx (pressure, &shvf, &temp, option);
          if (*shv >= shvf) return 100.0;
          else return 0.0;
          }
}           /*  xpshv  */


double xtshv (double *temperature, double *shv, int option)

     /*  quality as a function of temperature and shv
             option      shv
             ------    --------
                1      entropy
                2      enthalpy
                3      specific volume  */

{
     double       shvg, shvf,  saturation_pressure, dpdt, temp;

     if (MAJOR_ERROR) return 0.0;
        /*  if above critical temperature, quality is 100 %  */
     if (*temperature <= TC) {
        saturation_pressure = psl (temperature, &dpdt);
        temp = 100.0;
        shvg = shvptx (&saturation_pressure, temperature, &temp, option);
        if (*shv < shvg) {   /*  point is wet  */
           temp = 0.0;
           shvf = shvptx (&saturation_pressure, temperature, &temp, option);
           if (*shv > shvf) return (*shv - shvf) / (shvg - shvf) * 100.0;
           else return 0.0;
           }
        else return 0.0;
        }
     else return 100.0;
}    /*  xtshv  */


double tpshvx (double *tp, double *shv, double *quality,
               int option_shv, int option_tp)

     /* temperature or pressure as a function of pressure or temperature,
        shv, quality

          option_shv       shv                   option_tp    calculates
          ----------    ---------------          ---------    -----------
              1         entropy                      1        temperature
              2         enthalpy                     2        pressure
              3         volume

      */

{
     const double    tolerance = 1.0e-5;

     double    saturation_tp, dpdt;
     double    d_temp_press, tpshvx_shvcalc, tpshvx_percent, tpshvx_tp,
                   previous_tp, tp_up, tp_low, tp_df, tpshvx_prev_pct,
                   prev_tpshvx_shvcalc;
     int       tpshvx_counter;

     if (MAJOR_ERROR) return 0.0;

         /*  calculate the saturation temperature  */

     switch (option_tp) {
        case 1 :
           if (*tp >= PCA) saturation_tp = TC;
           else saturation_tp = tsl (tp);
           break;
        case 2 :
           if (*tp >= TC) saturation_tp = PCA;
           else saturation_tp = psl (tp, &dpdt);
        }

       /* if the quality is between 0 and 100, the pressure or temperature
          is the saturation pressure or temperature   */

     if ((*quality > 0) && (*quality < 100)) return saturation_tp;

     switch (option_tp) {
        case 1 :
           if (*quality == 0) {
              tp_up = saturation_tp;
              tp_low = TMIN;
              }
         /* for program to reach this point the quality must be 100 percent  */
           else {
                tp_low = saturation_tp;
                tp_up = TMAX;
                }
           break;
        case 2 :
           if (*quality == 0) {
              tp_low = saturation_tp;
              tp_up = PMAX;
              }
           /* for program to reach this point the quality must be 100 percent */
           else {
                tp_up = saturation_tp;
                tp_low = PMIN;
                }
        }

     tpshvx_tp = (tp_low + tp_up) / 2.0;
     tpshvx_counter = 0;
     tp_df = 0.50;

     do {
        tpshvx_counter ++;
        if (tpshvx_counter > 100) 
		{
           char str [1024];
		   char str2 [256];
 	  
           switch (option_tp) 
		   {
              case 1 :
                 sprintf (str2,
             "Calculated temperature = %9.2f F....Input pressure = %9.2f psia",
                         tpshvx_tp, *tp);
                 break;
              case 2 :
                 sprintf (str2,
             "Calculated pressure = %9.2f psia....Input temperature = %9.2f F",
                         tpshvx_tp, *tp);
           }
           sprintf (str,
                    "TPSHVX - 100 iteration maximum limit exceeded\n"
					"%s\n"
                    "Steam Quality = %9.2f %....Property = %12.6",
                    str2, *quality, *shv);
           MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
           MAJOR_ERROR = TRUE;
           MAJOR_ERROR_TYPE = 14;
           return 0.0;
        }
        if (tpshvx_tp > tp_up) {
               tpshvx_tp = tp_up;
               tp_df = 1.0;
               }
        if (tpshvx_tp < tp_low) {
               tpshvx_tp = tp_low;
               tp_df = 1.0;
               }
        VPTX3_ALREADY_SET_0 = FALSE;
        VPTX3_ALREADY_SET_100 = FALSE;
        switch (option_tp) {
           case 1 : tpshvx_shvcalc = shvptx (tp, &tpshvx_tp, quality,
                                             option_shv);
              break;
           case 2 : tpshvx_shvcalc = shvptx (&tpshvx_tp, tp, quality,
                                             option_shv);
           }
	if (MAJOR_ERROR) return 0.0;
        switch (option_tp) {
              case 1 : tpshvx_percent = (*shv - tpshvx_shvcalc) / *shv; break;
              case 2 : tpshvx_percent = (tpshvx_shvcalc - *shv) / *shv;
              }
        if (tpshvx_counter == 1)
              d_temp_press = tpshvx_tp * tpshvx_percent * tp_df;
        else {
             if ((tpshvx_prev_pct * tpshvx_percent) > 0) {
                  if (fabs (tpshvx_percent) > fabs (0.3 * tpshvx_prev_pct)) {
                      tp_df = 1.5 * tp_df;
                      d_temp_press = tpshvx_tp * tpshvx_percent * tp_df;
                      }
                  else d_temp_press = (tpshvx_tp - previous_tp) *
                                       (*shv - tpshvx_shvcalc) /
                                       (tpshvx_shvcalc - prev_tpshvx_shvcalc);
                  }
             else {
                  tp_df = tp_df * 0.67;
                  d_temp_press = (tpshvx_tp - previous_tp) *
                                    (*shv - tpshvx_shvcalc) /
                                    (tpshvx_shvcalc - prev_tpshvx_shvcalc);
                  }
             }
        if (fabs (tpshvx_percent) >= tolerance) {
              previous_tp = tpshvx_tp;
              prev_tpshvx_shvcalc = tpshvx_shvcalc;
              tpshvx_prev_pct = tpshvx_percent;
              tpshvx_tp = tpshvx_tp + d_temp_press;
              }
        } while (fabs (tpshvx_percent) >= tolerance);

     return tpshvx_tp;
}        /*  tpshvx  */


double phsd (double *h, double *s)

       /* calculates pressure as a function of enthalpy and entropy in the
          dry superheat region   */

{
   const double   a0 = -1.5789006e1;
   const double   a1 = 4.4537925e-2;
   const double   a2 = -4.7685218e-5;
   const double   a3 = 2.1794554e-8;
   const double   a4 = -3.0128279e-12;
   const double   b1 = -1.8413685e-3;
   const double   b2 = 9.7990702e-7;
   const double   tolerance = 1.0e-5;

   double   p, t, ds, hp, temp;
   int      counter;

   if (MAJOR_ERROR) return 0.0;
   counter = 0;
   p = exp ((a0 + *h * (a1 + *h * (a2 + *h * (a3 + *h * a4)))) / (1.0 + *h *
            (b1 + *h * b2)));
   ds = 1.9 - *s;
   hp = *h + 750.0;
   do {
      counter++;
      if (counter > 100) 
	  {
         char str [256];

         strcpy (str,
                 "PHSD - Maximum limit of 100 iterations exceeded\n"
                 "The program was able to calculate the pressure\n"
				 "from the input numbers");
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 15;
         return 0.0;
      }
      p = p * exp (21738.0 * ds / hp);
      temp = 100.0;
      t = tpshvx (&p, h, &temp, 2, 1);
      ds = shvptx (&p, &t, &temp, 1) - *s;
      if (MAJOR_ERROR) return 0.0;
      } while (fabs (ds / *s) >= tolerance);
   return p;
}       /*  phsd  */


double phsw (double *h, double *s)

       /* calculates pressure as a function of enthalpy and entropy in the
          wet region  */

{
   const double   a0 = -4.9071297e1;
   const double   a1 = 1.2819008e-1;
   const double   a2 = -1.2481385e-4;
   const double   a3 = 6.2486862e-8;
   const double   a4 = -1.2600857e-11;
   const double   b1 = -163.409;
   const double   b2 = 1.32775;
   const double   b3 = -9.48775e-4;
   const double   b4 = 4.74595e-7;
   const double   tolerance = 5.0e-5;

   double   ds, h15, dslope, slope2, slope, pa, p2, h1, h2, p3, h3, t, quality;
   int      counter;

   if (MAJOR_ERROR) return 0.0;
   ds = 1.5 - *s;
   slope = 750.0;
   counter = 0;
   do {
      counter++;
      if (counter > 100) 
	  {
		 char str [256];

         strcpy (str, 
			     "PHSW - Maximum limit of 100 iterations exceeded\n"
                 "The program was not able to calculate the slope\n"
				 "(then pressure)");
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 16;
         return 0.0;
      }
      h15 = *h + slope * ds;
      slope2 = b1 + h15 * (b2 + h15 * (b3 + h15 * b4));
      dslope = fabs (slope2 - slope);
      slope = slope2;
      } while (dslope >= 10.0);
   pa = exp (a0 + h15 * (a1 + h15 * (a2 + h15 * (a3 + h15 * a4))));
   if (pa > PCA) pa = PCA;
   counter = 0;
   quality = xpshv (&pa, s, 1);
   t = tpshvx (&pa, s, &quality, 1, 1);
   h1 = shvptx (&pa, &t, &quality, 2);
   if (quality > 25.0) p2 = 0.95 * pa;
   else p2 = 0.75 * pa;
   quality = xpshv (&p2, s, 1);
   t = tpshvx (&p2, s, &quality, 1, 1);
   h2 = shvptx (&p2, &t, &quality, 2);
   counter = 0;
   do {
      counter++;
      if (counter > 100) 
	  {
		 char str [256];

         strcpy (str,
                 "PHSW - Maximum limit of 100 iterations exceeded\n"
                 "The program was not able to calculate the pressure");
         MessageBox (NULL, str, SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
         MAJOR_ERROR = TRUE;
         MAJOR_ERROR_TYPE = 17;
         return 0.0;
      }
      p3 = pa * pow (p2 / pa, (*h - h1) / (h2 - h1));
      if (p3 > PMAX) p3 = PMAX;
      if (p3 < PMIN) p3 = PMIN;
      quality = xpshv (&p3, s, 1);
      t = tpshvx (&p3, s, &quality, 1, 1);
      h3 = shvptx (&p3, &t, &quality, 2);
      pa = p2;
      h1 = h2;
      p2 = p3;
      h2 = h3;
      } while (fabs ((*h - h3) / *h) >= tolerance);
   return p3;
}     /*  phsw  */


double phs (double *h, double *s)

        /*  calculates pressure as a function of enthalpy and entropy  */

{
   const double   a0 = -2.7645397e3;
   const double   a1 = 8.6094758e3;
   const double   a2 = -1.1059326e4;
   const double   a3 = 7.5042872e3;
   const double   a4 = -2.8272377e3;
   const double   a5 = 5.6093094e2;
   const double   a6 = -4.5863755e1;
   const double   b1 = -1.3806761;
   const double   b2 = 5.1378357e-1;

   double   hg;

   if (MAJOR_ERROR) return 0.0;
   hg = 1000.0 + (a0 + *s * (a1 + *s * (a2 + *s * (a3 + *s * (a4 + *s *
        (a5 + *s * a6)))))) / (1.0 + *s * (b1 + *s * b2));
   if (*h > hg && *h > 906.0) return phsd (h, s);
   else return phsw (h, s);
}           /*  phs  */


double vistv (double *temperature, double density)

   /* viscosity (lbfs/ft/ft) as a function of temperature (F), pressure (psia),
       and density (lbm/cuft), uses September 1975 eigth international
       conference on the properties of steam in Giens, France  */

{
     const double    t_star = 647.27;        /*   K           */
     const double    den_star = 317.763;     /*   kg/m^3      */
     const double    a0 = 0.0181583;
     const double    a1 = 0.0177624;
     const double    a2 = 0.0105287;
     const double    a3 = -0.0036744;
     const double    b [6] [5] =
              {{0.501938,   0.235622,    -0.274637,   0.145831,   -0.0270448},
               {0.162888,   0.789393,    -0.743539,   0.263129,   -0.0253093},
               {-0.130356,  0.673665,    -0.959456,   0.347247,   -0.0267758},
               {0.907919,   1.207552,    -0.687343,   0.213486,   -0.0822904},
               {-0.551119,  0.0670665,   -0.497089,   0.100754,   0.0602253},
               {0.146543,   -0.0843370,  0.195286,    -0.032932,  -0.0202595}};
     const double    vis_pa_s = 1e-6;            /*  kg/m/s  */

     int       i, j;
     double    t_over_t_star, t_star_over_t_m1, den_over_den_star,
               den_over_den_star_m1, vis_zero, visptv_temp, t_star_over_t;

     if (MAJOR_ERROR) return 0.0;
       /*  must convert temperature from degrees F to degrees K  */
     t_over_t_star = (*temperature + TZA) / 1.8 / t_star;
     t_star_over_t = 1.0 / t_over_t_star;
     t_star_over_t_m1 = t_star_over_t - 1.0;
       /*  must convert density from lbm/ft^3 to kg/m^3  */
     den_over_den_star = density * 16.018 / den_star;
     den_over_den_star_m1 = den_over_den_star - 1.0;
     vis_zero = vis_pa_s * sqrt (t_over_t_star) / (a0 + t_star_over_t * (a1
                 + t_star_over_t * (a2 + t_star_over_t * a3)));
     visptv_temp = 0;
     for (i = 0; i <= 5; i++) for (j = 0; j <= 4; j++)
          visptv_temp = visptv_temp + b [i] [j] * pow (t_star_over_t_m1, i) *
                        pow (den_over_den_star_m1, j);
     visptv_temp = vis_zero * exp (den_over_den_star * visptv_temp);
         /*  change units from kg/m/s to lbfs/ft/ft  */
     return visptv_temp * 0.0208854342;
}     /*  vistv  */


double critical_velocity_of_tpqhs_satp
          (double *temperature, double *pressure, double *quality,
           double *enthalpy, double *entropy, double *saturation_pressure)

     /*  critical velocity (ft/s) as a function of pressure (psia),
         enthalpy (btu/lbm), entropy (btu/lbm/R), saturation_pressure (psia),
         temperature (F), quality (%)   */

{
     const double   a [10] = {-2.083333333, 4, -3, 1.33333333, -0.25,
                              -0.83333333, 1.5, -0.5, 0.083333333, -0.6666667};

     int      i;
     double   c_quality, psat, x, gamma, t, pk [9], v_critvel [9];

     if (MAJOR_ERROR) return 0.0;
     x = 0;
     if (*entropy < 1.062279) psat = 0.0;
     else {
          if ((*pressure > 2500) && (*enthalpy > 1114))
             psat = p23t (temperature);
          else psat = *saturation_pressure;
          }
     for (i = 0; i <= 8; i++) {
          pk [i] = *pressure * (1.0 + 0.01 * (i - 4.0));
           /*  this is to keep from getting error messages at low pressures  */
          if (pk [i] < PMIN) pk [i] = PMIN;
          c_quality = x_pt (&pk [i], temperature);
          if (c_quality != *quality) pk [i] = *saturation_pressure;
          v_critvel [i] = shvptx (&pk [i], temperature, quality, 3);
          }
     pk [0] = 0.01 / v_critvel [4];
     if (*pressure <= PCA || (*pressure > PCA && (pk [4] > psat &&
          pk [6] > psat) || (pk [4] < psat && pk [2] <= psat)))
              x = a [8] / v_critvel [2] - a [8] / v_critvel [6] +
                  a [9] / v_critvel [3] - a [9] / v_critvel [5];
     else {
          if (pk [4] > psat && pk [6] <= psat) {
             pk [0] = - pk [0];
             t = v_critvel [5];
             v_critvel [5] = v_critvel [3];
             v_critvel [6] = v_critvel [2];
             v_critvel [7] = v_critvel [1];
             if (pk [5] <= psat) {
                v_critvel [8] = v_critvel [0];
                for (i = 0; i <= 4; i ++) x = x + a [i] / v_critvel [i + 4];
                }
             else {
                  v_critvel [3] = t;
                  for (i = 4; i <= 8; i ++) x = x + a [i] / v_critvel [i - 1];
                  }
             }
          else {
               if (pk [4] == psat || (pk [4] < psat && pk [2] > psat
                      && pk [3] > psat))
                  for (i = 0; i <= 4; i++) x = x + a [i] / v_critvel [i + 4];
               else if (pk [4] < psat && pk [2] > psat && pk [3] <= psat)
                       for (i = 4; i <= 8; i++)
                          x = x + a [i] / v_critvel [i - 1];
               }
          }
     if (x == 0) return 0.0;
     else {
	gamma = pk [0] / x;
	return sqrt (4633.06 * gamma * *pressure * v_critvel [4]);
	}
}           /*  critical_velocity_of_tpqhs_satp  */
