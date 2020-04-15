//  steam.c


      /*  "T H E   1 9 6 7   A S M E   S T E A M   T A B L E S"

      Michael Lynn McGuire         7/2/85  --- ??/??/????

      Taken from:       "Formulations and Iterative Procedures for
                         the Calculation of Properties of Steam" by
                         R. B. McClintock (General Electric Co.)
                         and G. J. Silvestri (Westinghouse Electric),
                         The American Society of Mechanical
                         Engineers, 1968.

                        "Some Improved Steam Property Calculation
                         Procedures", McClintock and Silvestri,
                         Journal of Engineering for Power, April 1970.   */


    /*  The steam tables will set the variable major_error to TRUE when there
        is/was a problem.  A variable named major_error_type is also set with a
        code:

          major_error_type            problem
          ----------------       -------------------------------------------
                  0              no error
                  1              not enough variables were input
                  2              the input variables were out of range with
                                      each other

    */


   //  this is the DLL and it exports certain symbols
#define  EXPORTING 


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <windows.h>
#include "..\steam67.h"


   /*  local global variables for building error messages  */


int  VPTX3_ALREADY_SET_0, VPTX3_ALREADY_SET_100, VPTX3_CALL;

int MAJOR_ERROR = FALSE;
int MAJOR_ERROR_TYPE = 0;


#define  SteamBoxTitle  "STEAM67"


#include "steam1.c"
#include "steam2.c"


/*   
	action VALUE	program work done
    ------------	--------------------------------------------
		0			calculate balance around temperature, pressure,
					steam quality, enthalpy, entropy,
					specific weight, saturation pressure,
					saturation temperature, degrees superheat,
					degrees subcooling
		1			action 0 plus viscosity
		2			action 1 plus critical velocity
*/

__declspec(dllexport) 
int steam67 (double *temperature,
             double *pressure,
             double *quality,
             double *weight,
             double *enthalpy,
             double *entropy,
             double *saturation_temperature,
             double *saturation_pressure,
             double *degrees_superheat,
             double *degrees_subcooling,
             double *viscosity,
             double *critical_velocity,
             int action)
{
   int     done, enthalpy_done, entropy_done, weight_done;
   double   dpdt;

      /*  these are initialized because not all error routines use these,
          set the ERROR_MESSAGE pointers to the local global error message
          strings
      */

   MAJOR_ERROR = FALSE;

      /*  initialze the local variables  */

   done = FALSE;
   enthalpy_done = FALSE;
   entropy_done = FALSE;
   weight_done = FALSE;
   VPTX3_ALREADY_SET_0 = FALSE;
   VPTX3_ALREADY_SET_100 = FALSE;
   VPTX3_CALL = 0;

      /*  fix this to cure some problem -- I am not sure why it happens  */

   if (*pressure > 0 && *pressure < PMIN) *pressure = PMIN;
   if (*temperature > 0 && *temperature < TMIN) *temperature = TMIN;

   if (*pressure > 0) 
   {
      if (*pressure > PCA) *saturation_temperature = TC;
      else *saturation_temperature = tsl (pressure);
   }
   else *saturation_temperature = 0;

   if (*temperature > 0 && *temperature <= TC)
      *saturation_pressure = psl (temperature, &dpdt);
   else *saturation_pressure = 0;

      /*  check to see if both temperature and pressure are present,
          if so, check to see if the pressure and saturation pressure are
             within .01% of each other,
          if so, set the pressure to zero and let the program calculate the
             properties using the temperature and specific volume, enthalpy,
             entropy, or quality in that order
      */

   if (*temperature > 0 && *pressure > 0 && *saturation_pressure > 0)
      if (fabs (*pressure - *saturation_pressure) / *saturation_pressure <=
         0.0001) *pressure = 0;

     /*  start cycle of quality, volume, enthalpy, entropy calculations  */

   if (*temperature > 0) 
   {
         /*  temperature and pressure available, possibly quality  */
      if (*pressure > 0) 
	  {
         *quality = x_pt (pressure, temperature);
         done = TRUE;
      }
      else
           /*  temperature and specific volume available  */
         if (*weight > 0) 
		 {
             *quality = xtshv (temperature, weight, 3);
             *pressure = tpshvx (temperature, weight, quality, 3, 2);
             done = TRUE;
             weight_done = TRUE;
         }
         else
               /*  temperature and enthalpy available  */
            if (*enthalpy > 0) 
			{
               *quality = xtshv (temperature, enthalpy, 2);
               *pressure = tpshvx (temperature, enthalpy, quality, 2, 2);
               done = TRUE;
               enthalpy_done = TRUE;
            }
            else
                  /*  temperature and entropy available  */
               if (*entropy > 0) 
			   {
                  *quality = xtshv (temperature, entropy, 1);
                  *pressure = tpshvx (temperature, entropy, quality, 1, 2);
                  done = TRUE;
                  entropy_done = TRUE;
               }
               else
                     /*  temperature and quality available, assume
                         pressure is saturation pressure  */
                  if (*temperature <= TC) 
				  {
                     *pressure = *saturation_pressure;
                     done = TRUE;
                  }
   }

   if (MAJOR_ERROR) return FALSE;

   if (!done)
      if (*pressure > 0) 
	  {
           /*  pressure and specific volume available  */
         if (*weight > 0) 
		 {
            *quality = xpshv (pressure, weight, 3);
            *temperature = tpshvx (pressure, weight, quality, 3, 1);
            done = TRUE;
            weight_done = TRUE;
         }
         else
             /*  pressure and enthalpy available  */
            if (*enthalpy > 0) 
			{
               *quality = xpshv (pressure, enthalpy, 2);
               *temperature = tpshvx (pressure, enthalpy, quality, 2, 1);
               done = TRUE;
               enthalpy_done = TRUE;
            }
            else
                 /*  pressure and entropy available  */
               if (*entropy > 0) 
			   {
                  *quality = xpshv (pressure, entropy, 1);
                  *temperature = tpshvx (pressure, entropy, quality, 1, 1);
                  done = TRUE;
                  entropy_done = TRUE;
               }
               else
                      /*  pressure and quality available, assume temperature
                          is saturation temperature  */
                  if (*pressure <= PCA) 
				  {
                     *temperature = *saturation_temperature;
                     done = TRUE;
                  }
      }

    /*  if the steam properties have not been yet calculated see if entropy
        and enthalpy are present so we can try to calculate pressure  */

   if (!done) 
   {
      if (*enthalpy > 0 && *entropy > 0) *pressure = phs (enthalpy, entropy);
      enthalpy_done = TRUE;
      entropy_done = TRUE;
         /*  check if was able to calculate pressure, if so calculate quality
             and set ...done... to TRUE  */
      if (*pressure > 0) 
	  {
            /*  calculate the saturation temperature based on pressure  */
         if (*pressure <= PCA) *saturation_temperature = tsl (pressure);
         else *saturation_temperature = TC;

            /*  find out if enthalpy is present, then calculate quality and
                temperature ELSE calculate quality and temperature as a
                function of entropy (this is based on the fact that
                either entropy or enthalpy is present if the program
                got this far)  */

         *quality = xpshv (pressure, enthalpy, 2);
         *temperature = tpshvx (pressure, enthalpy, quality, 2, 1);
         done = TRUE;
      }
   }

   /*  check to see if steam was able to calculate properties, if not
       then write error message and quit, else calculate rest of properties  */

   if (! done) 
   {
      MessageBox (NULL,
         "There is not enough input data specified to calculate\n"
	     "the property(s).  Or, the input data is not in range\n"
		 "with itself.  At minimum, a temperature or pressure in\n"
		 "the saturated steam or liquid region is required.",
         SteamBoxTitle, MB_ICONSTOP & MB_ICONHAND);
      return FALSE;
   }

     /*  check and see if properties calculated, if not calculate  */

   if (!weight_done) *weight = shvptx (pressure, temperature, quality, 3);
   if (!enthalpy_done) *enthalpy = shvptx (pressure, temperature, quality, 2);
   if (!entropy_done) *entropy = shvptx (pressure, temperature, quality, 1);

   if (MAJOR_ERROR) return FALSE;

   if (*saturation_temperature < TMIN)  /*  check to see if already defined  */
      if (*pressure <= PCA) *saturation_temperature = tsl (pressure);
      else *saturation_temperature = TC;

   if ((*saturation_pressure < PMIN))   /*  check to see if already defined  */
      if (*temperature <= TC) *saturation_pressure = psl (temperature, &dpdt);
      else *saturation_pressure = 0;

   if (*quality == 100)
      *degrees_superheat = *temperature - *saturation_temperature;
   else *degrees_superheat = 0;
   if (*degrees_superheat < 0.0001) *degrees_superheat = 0;

   if (*quality == 0)
      *degrees_subcooling = *saturation_temperature - *temperature;
   else *degrees_subcooling = 0;
   if (*degrees_subcooling < 0.0001) *degrees_subcooling = 0;

   if (action > 0) *viscosity = vistv (temperature, 1 / *weight);

   if (action > 1)
      *critical_velocity = critical_velocity_of_tpqhs_satp
                             (temperature, pressure, quality, enthalpy,
                              entropy, saturation_pressure);

   if (MAJOR_ERROR)
	  return FALSE;
   else
	  return TRUE;
}
