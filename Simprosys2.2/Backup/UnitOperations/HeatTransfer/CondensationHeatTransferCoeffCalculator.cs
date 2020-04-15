using System;
using System.Drawing;

using Prosimo.Plots;

namespace Prosimo.UnitOperations.HeatTransfer 
{
   /// <summary>
   /// Summary description for CondensationHeatTransferCoeffCalculator.
   /// </summary>
   public class CondensationHeatTransferCoeffCalculator {
      
      //Handbook of Chemical Engineering Calculations--page 7-54 table 7-3(the only table on this page)
      private static readonly PointF[] stratifiedFlowCorrectionCurve =
      { 
         new PointF(0f, 1.30f), new PointF(50f, 1.25f), new PointF(200f, 1.20f), new PointF(500f, 1.15f),
         new PointF(750f, 1.10f), new PointF(1500f, 1.05f), new PointF(2650f, 1.0f), new PointF(4000f, 0.95f),
         new PointF(6000f, 0.9f), new PointF(7000f, 0.85f), new PointF(9000f, 0.80f), new PointF(10000f, 0.75f),
         new PointF(12000f, 0.7f), new PointF(17000f, 0.6f), new PointF(20000f, 0.5f), new PointF(25000f, 0.4f),
         new PointF(30000f, 0.250f), new PointF(35000f, 0.0f)
      };

      //Perry's Figure 5-10, also see section 7-25 of Handbook of Chemical Engineering Calculations
      //Fist correlation to use to calculate condensing heat transfer coefficient in vertical tubes
      //This correlation can also be used for falling film heat transfer coeeficient. But the result
      //should be multiplied by 0.75
      public static double CalculateVerticalTubeHTC_Dukler(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double vapViscosity, double liqThermalCond, double liqSpecificHeat)
      {
         return DuklerHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, diameter, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
      }
      
      //Section 7-26 of Handbook of Chemical Engineering Calculations
      //This correlation is very similar to Perry's Equation 5-101
      //massFlowRate is the condensate flow rate
      public static double CalculateInTubeHorizontalHTC_StratifiedFlow(double massFlowRate, double diameter, double length, double liqDensity, double liqViscosity, double liqThermalCond) {
         double tempValue = liqDensity * liqDensity * 9.8065 * length/(liqViscosity*massFlowRate);
         double h = 0.767 * liqThermalCond * Math.Pow(tempValue, 1.0/3.0);
         //need to do correction according to W/(rho * Math.Pow(Di, 2.56))
         //W is in Ibm/hr, rho is in Ibm/ft3, Di is in ft 
         tempValue = (massFlowRate/1.25997881e-4)/(liqDensity/16.0184634 * Math.Pow(diameter/0.3048, 2.56));
         double correctionFactor = ChartUtil.GetInterpolateValue(stratifiedFlowCorrectionCurve, tempValue);
         return h * correctionFactor;
      }

      //Section 7-26 of Handbook of Chemical Engineering Calculations
      public static double CalculateInTubeHorizontalHTC_AnnularFlow(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double liqThermalCond, double liqSpecificHeat, double inVapQuality) {
         double MassVelocityLiq = massFlowRate * (1 - inVapQuality)/(4.0*Math.PI*diameter*diameter);
         double MassVelocityVap = massFlowRate * inVapQuality/(4.0*Math.PI*diameter*diameter);
         double ReLiq = diameter * MassVelocityLiq/liqViscosity;
         double ReVap = diameter * MassVelocityVap * inVapQuality/liqViscosity * Math.Sqrt(liqDensity/vapDensity);

         double MassVelocityEquivalent = MassVelocityLiq + MassVelocityVap * Math.Sqrt(liqDensity/vapDensity);
         double Re = diameter * MassVelocityEquivalent/liqViscosity;
         double Pr  = liqSpecificHeat * liqViscosity/liqThermalCond;
         double h = 0.0;
         if (ReLiq > 5000 && ReVap > 200000) {
            h = 0.0265 * Math.Pow(Pr, 1.0/3.0) * Math.Pow(Re, 0.8);
         }
         else {
            h = 5.03 * Math.Pow(Pr, 1.0/3.0) * Math.Pow(Re, 1.0/3.0);
         }

         return h*liqThermalCond/diameter;
      }

      //Section 7-27 of Handbook of Chemical Engineering Calculations
      public static double CalculateTubeBanksHorizontalHTC_LaminarFlow(double massFlowRate, double length, double liqDensity, double liqViscosity, double liqThermalCond, 
         TubeLayout tubeLayout, double tubePitch, double shellDiameter) {
         double a = 1.0;
         if (tubeLayout == TubeLayout.Triangular) {
            a = 0.951;
         }
         else if (tubeLayout == TubeLayout.RotatedSquare) {
            a = 0.904;
         }
         else if (tubeLayout == TubeLayout.InlineSquare) {
            a = 0.856;
         }

         double m = 1.0;
         if (tubeLayout == TubeLayout.Triangular) {
            m = 1.155;
         }
         else if (tubeLayout == TubeLayout.RotatedSquare) {
            m = 0.707;
         }
         else if (tubeLayout == TubeLayout.InlineSquare) {
            m = 1.0;
         }
         double Nr = m * shellDiameter/tubePitch;

         double tempValue = liqDensity * liqDensity * 9.8065 * length/(liqViscosity*massFlowRate);
         double h = a* liqThermalCond * Math.Pow(tempValue, 1.0/3.0) * Math.Pow(Nr, -1.0/6.0);

         return h;
      }

      //Section 7-27 of Handbook of Chemical Engineering Calculations
      public static double CalculateTubeBanksHorizontalHTC_VaporShearDominated(double massFlowRate, double diameter, double length, double liqDensity, double vapDensity, double liqViscosity, double liqThermalCond, 
         double vaporQuality, TubeLayout tubeLayout, double tubePitch, double baffleSpacing, double shellDiameter) {
         double b = 1.0;
         if (tubeLayout == TubeLayout.Triangular) {
            b = 0.42;
         }
         else if (tubeLayout == TubeLayout.RotatedSquare) {
            b = 0.43;
         }
         else if (tubeLayout == TubeLayout.InlineSquare) {
            b = 0.39;
         }

         double m = 1.0;
         if (tubeLayout == TubeLayout.Triangular) {
            m = 1.155;
         }
         else if (tubeLayout == TubeLayout.RotatedSquare) {
            m = 0.707;
         }
         else if (tubeLayout == TubeLayout.InlineSquare) {
            m = 1.0;
         }
         double Nr = m * shellDiameter/tubePitch;

         double ac = baffleSpacing * shellDiameter * (tubePitch - diameter)/tubePitch;
         if (tubeLayout == TubeLayout.RotatedSquare) {
            ac = 1.5 * ac;
         }

         double vapFlowRate = massFlowRate * vaporQuality;
         double vc = vapFlowRate/(vapDensity * ac);

         double tempValue = diameter * liqDensity * vc/(liqViscosity);
         double h = b * liqThermalCond / diameter * Math.Pow(tempValue, 1.0/2.0) * Math.Pow(Nr, -1.0/6.0);

         return h;
      }
      
      //Perry's Equation 5-88
      //Reynolds number must be < 2100
      //For low values of Reynolds (4 * gama/viscosity) number this correlation can be used
      //to predict condensing HTC for vertical tube
      public static double CalculateVerticalTubeHTC_Nusselt(double massFlowRate, double diameter, double density, double viscosity, double thermalCond) {
         double gamma = massFlowRate/(Math.PI*diameter);
         double tempValue = density * density * 9.8065/(viscosity * gamma);
         double h = thermalCond * 0.925 * Math.Pow(tempValue, 1/3);
         return h;
      }

      //Perry's Equation 5-87
      //Reynolds number must be < 2100
      public static double CalculateVerticalTubeHTC_Colburn(double massFlowRate, double diameter, double density, double viscosity, double thermalCond) {
         double gamma = massFlowRate/(Math.PI*diameter);
         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
         double h = 5.35*tempValue*thermalCond/(4.0*gamma);
         return h;
      }

      //Perry's Equation 5-92
      //Reynolds number must be < 2100
      public static double CalculateHorizontalTubeHTC_Colburn(double massFlowRate, double length, double density, double viscosity, double thermalCond) {
         double gamma = massFlowRate/(2.0*length);
         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
         double h = 4.4*tempValue*thermalCond/(4.0*gamma);
         return h;
      }

      //Perry's Equation 5-93
      //Reynolds number must be < 2100
      //This collelation is known conservitavely for predicting the steam and organic vapor
      public static double CalculateHorizontalTubeHTC_Nusselt(double massFlowRate, double length, double density, double viscosity, double thermalCond) {
         double gamma = massFlowRate/(2.0*length);
         double tempValue = density * density * 9.8065/(viscosity*gamma);
         double h = thermalCond * 0.76 * Math.Pow(tempValue, 1/3);
         return h;
      }

      //vapor shear controlled flow within a tube 
      //Perry's Equation 5-99e
      public static double CalculateInTubeVerticalHTC_Carpenter_Colburn(double inMassFlowRate, double outMassFlowRate, double diameter, double liqDensity, double liqViscosity, double liqThermalCond, double liqSpecificHeat, double vapDensity, double vapViscosity) {
         double massFluxIn = 4.0*inMassFlowRate/(Math.PI*diameter*diameter);
         double massFluxOut = 4.0*outMassFlowRate/(Math.PI*diameter*diameter);
         double massFluxAve = Math.Sqrt((massFluxIn * massFluxIn + massFluxIn * massFluxOut + massFluxOut * massFluxOut)/3);
         double Re = diameter*massFluxAve/vapViscosity;

         double f = FrictionFactorCalculator.CalculateFrictionFactor(Re);
         double h = 0.065 * Math.Sqrt(liqSpecificHeat*liqDensity*liqThermalCond*f/(2.0*liqViscosity*vapDensity))*massFluxAve;
         return h;
      }
      
      //Perry's Equation 5-100a
      public static double CalculateInTubeVerticalHTC_Boyko_Kruzhilin(double massFlowRate, double diameter, double liqDensity, double vapDensity, double liqViscosity, double liqThermalCond, double liqSpecificHeat, double inVapQuality, double outVapQuality) {
         double massFlux = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         double Re = diameter*massFlux/liqViscosity;
         double Pr = liqSpecificHeat*liqViscosity/liqThermalCond;
         double rhoRhomi = 1.0 + (liqDensity-vapDensity)/vapDensity*inVapQuality;
         double rhoRhomo = 1.0 + (liqDensity-vapDensity)/vapDensity*outVapQuality;
         double Nut = 0.024*Math.Pow(Re, 0.8)*Math.Pow(Pr, 0.43)*(rhoRhomi+rhoRhomo)/2.0;
         double h = liqThermalCond*Nut/diameter;
         return h;
      }

      //Perry's Equation 5-101
      public static double CalculateInTubeHorizontalHTC_Nusselt_Kerns_Modified(double massFlowRate, double length, double liqDensity, double vapDensity, double liqViscosity, double liqThermalCond, double inVapQuality, double outVapQuality) {
         double totalCondensFlow = massFlowRate * (inVapQuality-outVapQuality);
         double tempValue = length*liqThermalCond*liqThermalCond*liqThermalCond*liqDensity*(liqDensity-vapDensity)*9.8065/(totalCondensFlow*liqViscosity);
         double h = 0.761*Math.Pow(tempValue, 1.0/3.0);
         return h;
      }
   }
}

