using System;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for SinglePhaseHeatTransferCoeffCalculator.
   /// </summary>
   public class SinglePhaseHeatTransferCoeffCalculator {

      //Perry's Equation 5-39, 0.1 < Graetz number < 10000 
      //public static double CalculateTubeLaminarHTC_Hausen(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity, 
      public static double CalculateTubeLaminarHTC_Hausen(double Re, double diameter, double length, double bulkViscosity, double wallViscosity, 
            double thermalCond, double specificHeat) {
         //double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         //double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Gz = Re*Pr*diameter/length;
         double Nut = 0.0;
         if (Re <= 2100) {
            //this correlation is for horizontal tubes. we don't have correlations for vertical ones.
            Nut = 3.66 + 0.19*Math.Pow(Gz, 0.8)/(1 + 0.117 * Math.Pow(Gz, 0.467)) * Math.Pow(bulkViscosity/wallViscosity,0.14);
         }
         return Nut*thermalCond/diameter;
      }

      //Perry's Equation 5-40, Graetz number > 100
      public static double CalculateTubeLaminarHTC_Sieder_Tate(double Re, double diameter, double length, 
         double bulkViscosity, double wallViscosity, double thermalCond, double specificHeat) {
         //double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         //double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Gz = Re*Pr*diameter/length;
//         double Gr = length*length*length*density*density*9.8065*thermalExpansion*tempDiff/(bulkViscosity*bulkViscosity);
         double Nut = 0.0;
         if (Re <= 2100) {
            //this correlation is for horizontal tubes. we don't have correlations for vertical ones.
            //Nut = 1.86*Math.Pow(Gz,1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity,0.14) * 0.87*(1.0 + 0.015 * Math.Pow(Gr, 1.0/3.0));
            if (Gz > 100) 
            {
               Nut = 1.86*Math.Pow(Gz, 1.0/3.0);
            }
            else 
            {
               Nut = 3.66 + 0.085 * Gz/(1.0 + 0.047 * Math.Pow(Gz, 2.0/3.0));
            }

            Nut = Nut * Math.Pow(bulkViscosity/wallViscosity,0.14);
         }
         return Nut*thermalCond/diameter;
      }

      //Perry's Equation 5-41
      public static double CalculateAnnuliLaminarHTC_Chen_Hawkins_Solberg(double massFlowRate, double innerDiameter, double outerDiameter, double length, double density, double bulkViscosity, double innerWallViscosity, 
         double thermalCond, double specificHeat, double thermalExpansion, double tempDiff) {
         double massVelocity = 4.0*massFlowRate/(Math.PI*(outerDiameter*outerDiameter - innerDiameter*innerDiameter));
         double De = outerDiameter - innerDiameter;
         double Re = De*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re <= 2100) {
            //this correlation is for horizontal tubes. we don't have correlations for vertical ones.
            Nut = 1.02*Math.Pow(Re,0.45)*Math.Pow(Pr, 0.5) * Math.Pow(De/length, 0.4) * Math.Pow(outerDiameter/innerDiameter, 0.8) * Math.Pow(bulkViscosity/innerWallViscosity,0.14);
         }
         return Nut*thermalCond/De;
      }
      
      //Perry's Equation 5-43
      public static double CalculateParalellPlateLaminarHTC_Norris_Streid(double massFlowRate, double length, double width, double plateDistance, double density, double bulkViscosity, double innerWallViscosity, 
         double thermalCond, double specificHeat) {
         double massVelocity = 4.0*massFlowRate/(width * plateDistance);
         double De = 4 * width*plateDistance/(2*(width + plateDistance));
         double Re = De*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Gz = Re*Pr*plateDistance/length;
         double Nut = 0.0;
         if (Re <= 2100) {
            //this correlation is for horizontal tubes. we don't have correlations for vertical ones.
            Nut = 1.85*Math.Pow(Gz, 1/3);
         }
         return Nut*thermalCond/De;
      }

      //Perry's Equation 5-47a and 5-47b
      public static double CalculateFallingFilmHTC(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity,   
         double thermalCond, double specificHeat) {
         double gamma = massFlowRate/(Math.PI*diameter);
         double Re = 4* gamma/bulkViscosity;
         double h = 0.0;
         if (Re <= 2100) {
            double term1 = Math.Pow(thermalCond*thermalCond*Math.Pow(density, 4.0/3.0)*specificHeat*Math.Pow(9.806, 2.0/3.0)/(length*Math.Pow(bulkViscosity, 1.0/3.0)), 1.0/3.0);
            double term2 = Math.Pow(bulkViscosity/wallViscosity, 1.0/4.0);
            double term3 = Math.Pow(Re, 1.0/9.0);
            h = 0.5 * term1 * term2 * term3;
         }
         else if (Re > 2100) {
            double Pr = specificHeat*bulkViscosity/thermalCond;
            h = 0.01*Math.Pow(thermalCond*thermalCond*thermalCond*density*density*9.0865/(bulkViscosity*bulkViscosity), 1.0/3.0)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(Re, 1.0/3.0);
         }
         return h;
      }
      
      //Perry's Equation 5-49
      public static double CalculateTransitionHTC_Hausen(double Re, double diameter, double length, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) {
         //double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         //double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re > 2100 && Re < 10000) {
            Nut = 0.116*(Math.Pow(Re, 2.0/3.0) - 125.0)*Math.Pow(Pr, 1.0/3.0) * (1.0 + Math.Pow(diameter/length, 2.0/3.0)) * Math.Pow(bulkViscosity/wallViscosity, 0.14);
         }
         return Nut*thermalCond/diameter;
      }
      
      //Perry's Equation 5-50a, Re > 10000, 0.7 < Pr < 170
      public static double CalculateTubeTurbulentHeatingHTC_Dittus_Boelter(double massFlowRate, double diameter, double density, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) {
         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re > 10000) {
            Nut = 0.0243*Math.Pow(Re, 0.8)*Math.Pow(Pr, 0.4)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
         }
         return Nut*thermalCond/diameter;
      }
      
      //Perry's Equation 5-50b, Re > 10000, 0.7 < Pr < 170
      public static double CalculateTubeTurbulentCoolingHTC_Dittus_Boelter(double massFlowRate, double diameter, double density, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) {
         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re > 10000) {
            Nut = 0.0265*Math.Pow(Re, 0.8)*Math.Pow(Pr, 0.3)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
         }
         return Nut*thermalCond/diameter;
      }
      
      //Perry's Equation 5-50c
      public static double CalculateTubeTurbulentHTC_Colburn(double Re, double diameter, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) {
         //double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         //double Re = diameter*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re >= 10000) {
            Nut = 0.023*Math.Pow(Re, 0.8)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
         }
         return Nut*thermalCond/diameter;
      }
      
      //predicts the results in the range 1.0e4 and 5.0e6 and Pr of 0.5 to 200 with 5 to 6% error, and in the range of 0.5 to 2000 with 10% error.
      public static double CalculateTubeTurbulentHTC_Petukhov_Kirillov(double Re, double diameter, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) 
      {
         //double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         //double Re = diameter*massVelocity/bulkViscosity;
         double f = Math.Pow((1.58 * Math.Log(Re) - 3.28), -2.0);
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re >= 10000) 
         {
            Nut = 0.5*f*Re*Pr/(1.07+12.7*Math.Pow(0.5*f, 0.5)*(Math.Pow(Pr, 2.0/3.0) - 1.0)) ;
         }
         return Nut*thermalCond/diameter;
      }

      //Section 7-20 of Handbook of Chemical Engineering Calculations
      public static double CalculateTubeBanksHTC_Colburn(double massFlowRate, double diameter, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat, TubeLayout tubeLayout, double tubePitch, double baffleSpacing, double shellDiameter, bool withFouling) {
         
         double ac = baffleSpacing * shellDiameter * (tubePitch - diameter)/tubePitch;
         if (tubeLayout == TubeLayout.RotatedSquare) {
            ac = 1.5 * ac;
         }
         double massVelocity = massFlowRate/ac;
         double Re = diameter*massVelocity/bulkViscosity;

         double m = 1.0;
         double a = 1.0;
         if (Re > 200000) {
            if (tubeLayout == TubeLayout.Triangular || tubeLayout == TubeLayout.RotatedSquare) {
               m = 0.3;
               a = 0.166;
            }
            else if (tubeLayout == TubeLayout.InlineSquare) {
               m = 0.3;
               a = 0.124;
            }
         }
         else if (Re > 300 && Re <= 200000) {
            if (tubeLayout == TubeLayout.Triangular || tubeLayout == TubeLayout.RotatedSquare) {
               m = 0.365;
               a = 0.273;
            }
            else if (tubeLayout == TubeLayout.InlineSquare) {
               m = 0.349;
               a = 0.211;
            }
         }
         else if (Re < 300) {
            if (tubeLayout == TubeLayout.Triangular || tubeLayout == TubeLayout.RotatedSquare) {
               m = 0.64;
               a = 1.309;
            }
            else if (tubeLayout == TubeLayout.InlineSquare) {
               m = 0.569;
               a = 0.742;
            }
         }

         double Pr = specificHeat*bulkViscosity/thermalCond;
         double h = a * specificHeat * massVelocity/(Math.Pow(Re, m)*Math.Pow(Pr, 2.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14));

         double Fl = 0.8 * Math.Pow(baffleSpacing/shellDiameter, 1.0/4.0);
         if (withFouling) {
            Fl = 0.8 * Math.Pow(baffleSpacing/shellDiameter, 1.0/6.0);
         }
         double Fr = 1.0;
         if (Re < 100) {
            Fr = 0.2 * Math.Pow(Re, 1.0/3.0);
         }
         return h * Fl * Fr;
      }

      //Perry's Equation 5-60a
      public static double CalculateAnnuliTurbulentHTC_Monrad_Pelton(double massFlowRate, double innerDiameter, double outerDiameter, double length, double density, double bulkViscosity, double innerWallViscosity, 
         double thermalCond, double specificHeat, double thermalExpansion, double wallTempDiff) {
         double massVelocity = 4.0*massFlowRate/(Math.PI*(outerDiameter*outerDiameter - innerDiameter*innerDiameter));
         double Re = (outerDiameter-innerDiameter)*massVelocity/bulkViscosity;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nut = 0.0;
         if (Re > 2100) {
            Nut = 0.020*Math.Pow(Re, 0.8)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(outerDiameter/innerDiameter, 0.53);
         }
         return Nut*thermalCond/(outerDiameter-innerDiameter);
      }
   }
}
