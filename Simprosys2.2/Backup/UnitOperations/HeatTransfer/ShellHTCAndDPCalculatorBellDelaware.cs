using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer
{
	/// <summary>
	/// Summary description for ShellHTCAndDPCalculator.
	/// </summary>
   [Serializable]
   public class ShellHTCAndDPCalculatorBellDelaware : ShellHTCAndDPCalculator 
   {
      //Calculated variables
      private double baffleCutLengthValue;
      private double tubeBundleDiameter;
      private double tubePitchParalellToFlow;
      private double tubePitchNormalToFlow;
      private double fractionOfTotalTubesInCrossFlow;
      private int crossFlowRowsInEachWindow;
      private int tubeRowsInOneCrossFlowSetion;
      private double crossFlowArea;
      private double tubeToBaffleLeakageArea;
      private double shellToBaffleLeakageArea;
      private double fractionOfCrossFlowAreaForBypass;
      private double areaForFlowThroughWindow;
      private double equivalentDiameterOfWindow;

      public ShellHTCAndDPCalculatorBellDelaware(HXRatingModelShellAndTube ratingModel) : base (ratingModel) 
      {
         InitializeVarList();
         //InitializeGeometryParams();
      }

      private void InitializeVarList() 
      {
         procVarList.Add(ratingModel.TubePitch);
         procVarList.Add(ratingModel.BaffleCut);
         procVarList.Add(ratingModel.BaffleSpacing);
         procVarList.Add(ratingModel.NumberOfBaffles);
         procVarList.Add(ratingModel.EntranceBaffleSpacing);
         procVarList.Add(ratingModel.ExitBaffleSpacing);
         procVarList.Add(ratingModel.ShellInnerDiameter);
         procVarList.Add(ratingModel.BundleToShellDiametralClearance);
         procVarList.Add(ratingModel.ShellToBaffleDiametralClearance);
         procVarList.Add(ratingModel.SealingStrips);

         ratingModel.ProcVarList.AddRange(procVarList);
         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      internal override double GetVelocity(double massFlowRate, double density) 
      {
         double v = massFlowRate/(density*crossFlowArea);
         return v;
      }

      internal override double GetReynoldsNumber(double massFlowRate, double bulkViscosity) 
      {
         double Do = ratingModel.TubeOuterDiameter.Value;
         double Re = Do*massFlowRate/(bulkViscosity*crossFlowArea);
         return Re;
      }

      internal override double CalculateSinglePhaseHTC(double massFlowRate, double density, double bulkViscosity, double wallViscosity, double thermCond, double specificHeat) 
      {
         double p = ratingModel.TubePitch.Value;

         //double ls = ratingModel.BaffleSpacing.Value;
         double lin = ratingModel.EntranceBaffleSpacing.Value;
         double lout = ratingModel.ExitBaffleSpacing.Value;
         double lc = baffleCutLengthValue;
         double Nc = tubeRowsInOneCrossFlowSetion;
         double Fc = fractionOfTotalTubesInCrossFlow;
         double Ncw = crossFlowRowsInEachWindow;
         double Sm = crossFlowArea;
         double Fbp = fractionOfCrossFlowAreaForBypass;
         double Stb = tubeToBaffleLeakageArea;
         double Ssb = shellToBaffleLeakageArea;
         double Sw = areaForFlowThroughWindow;
         double Dw = equivalentDiameterOfWindow;
         int Nb = ratingModel.NumberOfBaffles.Value;
         double Do = ratingModel.TubeOuterDiameter.Value;
         int Nss = ratingModel.SealingStrips.Value;
         TubeLayout layout = ratingModel.TubeLayout;

         double Re = Do*massFlowRate/(bulkViscosity*Sm);
         double jh = CalculateJh(Re, layout, Do, p);
         double h = specificHeat*massFlowRate/Sm*Math.Pow(thermCond/(specificHeat*bulkViscosity), 2.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
         double ho = jh*h;
         double Jc = CalculateBaffleConfigFactor(Fc);
         double Jl = CalculateBaffleLeakageFactor(Sm, Ssb, Stb);
         double Jb = CalculateBundleBypassFactor(Re, Fbp, Nss, Nc);
         double Jr = CalculateAdverseTempGradientFactor(Re, Nb, Nc, Ncw);
         double Js = CalculateUneaqualBaffleSpacingFactor(Re, Nb, lin, lout, lc);  
         double hs = ho*Jc*Jl*Jb*Jr*Js;

         return hs;
      }

      internal override double CalculateSinglePhaseDP(double massFlowRate, double density, double bulkViscosity, double wallViscosity) {
         double p = ratingModel.TubePitch.Value;

         double ls = ratingModel.BaffleSpacing.Value;
         double lc = baffleCutLengthValue;
         double lin = ratingModel.EntranceBaffleSpacing.Value;
         double lout = ratingModel.ExitBaffleSpacing.Value;
         double Nc = tubeRowsInOneCrossFlowSetion;
         double Ncw = crossFlowRowsInEachWindow;
         double Sm = crossFlowArea;
         double Fbp = fractionOfCrossFlowAreaForBypass;
         double Stb = tubeToBaffleLeakageArea;
         double Ssb = shellToBaffleLeakageArea;
         double Sw = areaForFlowThroughWindow;
         double Dw = equivalentDiameterOfWindow;
         int Nb = ratingModel.NumberOfBaffles.Value;
         double Do = ratingModel.TubeOuterDiameter.Value;
         int Nss = ratingModel.SealingStrips.Value;
         TubeLayout layout = ratingModel.TubeLayout;

         double Re = Do*massFlowRate/(bulkViscosity*Sm);
         if (Re <= 0.0) {
            Re = 1.0;
         }

         double Go = massFlowRate/Sm;
         double fs = CalculateTubeBankFrictionFactor(Re, layout, Do, p);
         double dPb = 2.0*fs*Go*Go*Nc/density * Math.Pow(wallViscosity/bulkViscosity, 0.14);
         
         double Gw = massFlowRate/Math.Sqrt(Sm*Sw);
         double dPw = (0.026*bulkViscosity*(Ncw/(p-Do) + ls/(Dw*Dw)) + Gw)*Gw/density;  //for Re <= 100
         if (Re > 100) 
         {
            dPw = 0.5*Gw*Gw*(2.0+0.6*Ncw)/density;
         }

         double Rl = CalculateBaffleLeakageDpFactor(Sm, Ssb, Stb);
         double Rb = CalculateBundleBypassDpFactor(Re, Fbp, Nss, Nc);
         double Rs = CalculateUneaqualBaffleSpacingDpFactor(Re, Nb, lin, lout, lc);  

         double dp = Rb*dPb*((Nb - 1.0)*Rl+(1.0+Ncw/Nc)*Rs) + Nb*Rl*dPw;

         return dp;
      }

      private double CalculateJh(double Re, TubeLayout layout, double tubeOuterDiam, double tubePitch) 
      {
         double a1 = 0.321;
         double a2 = -0.388;
         double a3 = 1.45;
         double a4 = 0.519;
         if (layout == TubeLayout.Triangular) 
         {
            a3 = 1.45;
            a4 = 0.519;
            if (Re > 1.0e3) 
            {
               a1 = 0.321;
               a2 = -0.388;
            }
            else if (Re > 1.0e2) 
            {
               a1 = 0.593;
               a2 = -0.477;
            }
            else if (Re > 10.0)
            {
               a1 = 1.36;
               a2 = -0.657;
            }
            else 
            {
               a1 = 1.4;
               a2 = -0.667;
            }
         }
         else if (layout == TubeLayout.InlineSquare) 
         { 
            a3 = 1.187;
            a4 = 0.370;
            if (Re > 1.0e4) 
            {
               a1 = 0.37;
               a2 = -0.395;
            }
            if (Re > 1.0e3) 
            {
               a1 = 0.107;
               a2 = -0.266;
            }
            else if (Re > 1.0e2) 
            {
               a1 = 0.408;
               a2 = -0.46;
            }
            else if (Re > 10.0)
            {
               a1 = 0.9;
               a2 = -0.631;
            }
            else 
            {
               a1 = 0.97;
               a2 = -0.667;
            }
         }
         else if (layout == TubeLayout.RotatedSquare) 
         { 
            a3 = 1.93;
            a4 = 0.5;
            if (Re > 1.0e3) 
            {
               a1 = 0.37;
               a2 = -0.396;
            }
            else if (Re > 1.0e2) 
            {
               a1 = 0.73;
               a2 = -0.5;
            }
            else if (Re > 10.0)
            {
               a1 = 0.498;
               a2 = -0.656;
            }
            else 
            {
               a1 = 1.55;
               a2 = -0.667;
            }
         }

         double a = a3/(1.0+0.14*Math.Pow(Re, a4));
         double jh = a1 * Math.Pow(Re, a2) * Math.Pow((1.33 * tubeOuterDiam/tubePitch), a);
         return jh;
      }
      
      private double CalculateBaffleConfigFactor(double Fc) {
         return (Fc + 0.54 * Math.Pow((1.0 - Fc), 0.345));
      }

      private double CalculateBaffleLeakageFactor(double Sm, double Ssb, double Stb) {
         double alpha = 0.44 * (1.0 - Ssb/(Stb + Ssb));
         return alpha + (1.0 - alpha) * Math.Exp(-2.2 * (Stb + Ssb)/Sm);
      }

      private double CalculateBundleBypassFactor(double Re, double Fbp, int Nss, double Nc) {
         double y = (double)Nss/(double)Nc;
         double retValue = 1.0;
         if (y < 0.5) {
            double Cbh = 1.35;
            if (Re > 100) {
               Cbh = 1.25;
            }
            retValue = Math.Exp(-Cbh * Fbp * (1.0-2.0*Math.Pow(y, 1.0/3.0)));
         }
         return retValue;
      }

      private double CalculateAdverseTempGradientFactor(double Re, double Nb, double Nc, double Ncw) {
         double retValue = 1.0;
         double jStar = 3.76 * Math.Pow(Math.Log(Nc + Ncw), -0.15) * Math.Pow(Nb, -0.174);
         if (Re >= 20 && Re <= 100) 
         {
            retValue = 1.0 - (1.0 - jStar)*(1.24 - 0.0124 * Re);
         }
         else if (Re < 20) 
         {
            retValue = jStar;
         }

         return retValue;
      }

      private double CalculateUneaqualBaffleSpacingFactor(double Re, int Nb, double lin, double lout, double lc) 
      {
         double linStar = lc/lin;
         double loutStar = lc/lout;
         double tempValue = Nb - 1;
         double n = 0.333;
         if (Re > 100)
         {
            n = 0.6;
         }
         n = 1.0 - n;
         double retValue = (tempValue + Math.Pow(linStar, n) + Math.Pow(loutStar, n))/(tempValue + linStar + loutStar);
         return retValue;
      }

      private double CalculateTubeBankFrictionFactor(double Re, TubeLayout layout, double tubeOuterDiam, double tubePitch) {
         double b1 = 0.372;
         double b2 = -0.123;
         double b3 = 7.0;
         double b4 = 0.5;

         if (layout == TubeLayout.Triangular) {
            b3 = 7.0;
            b4 = 0.5;
            if (Re > 1.0e4) 
            {
               b1 = 0.372;
               b2 = -0.123;
            }
            if (Re > 1.0e3) 
            {
               b1 = 0.486;
               b2 = -0.152;
            }
            else if (Re > 1.0e2) 
            {
               b1 = 0.457;
               b2 = -0.476;
            }
            else if (Re > 10.0)
            {
               b1 = 45.1;
               b2 = -0.973;
            }
            else 
            {
               b1 = 48.0;
               b2 = -1.0;
            }
         }
         else if (layout == TubeLayout.InlineSquare) 
         { 
            b3 = 6.3;
            b4 = 0.378;
            if (Re > 1.0e4) 
            {
               b1 = 0.391;
               b2 = -0.148;
            }
            if (Re > 1.0e3) 
            {
               b1 = 0.0815;
               b2 = -0.022;
            }
            else if (Re > 1.0e2) 
            {
               b1 = 6.09;
               b2 = -0.602;
            }
            else if (Re > 10.0)
            {
               b1 = 32.1;
               b2 = -0.963;
            }
            else 
            {
               b1 = 35.0;
               b2 = -1.0;
            }
         }
         else if (layout == TubeLayout.RotatedSquare) 
         { 
            b3 = 6.59;
            b4 = 0.520;
            if (Re > 1.0e4) 
            {
               b1 = 0.303;
               b2 = -0.126;
            }
            if (Re > 1.0e3) 
            {
               b1 = 0.333;
               b2 = -0.136;
            }
            else if (Re > 1.0e2) 
            {
               b1 = 3.50;
               b2 = -0.476;
            }
            else if (Re > 10.0)
            {
               b1 = 26.2;
               b2 = -0.913;
            }
            else 
            {
               b1 = 32.0;
               b2 = -1.0;
            }
         }
         double b = b3/(1.0 + 0.14*Math.Pow(Re, b4));
         double jFactor = b1 * Math.Pow(Re, b2) * Math.Pow(1.33*tubeOuterDiam/tubePitch, b);

         return jFactor;
      }

      private double CalculateBaffleLeakageDpFactor(double Sm, double Ssb, double Stb) {
         double tempValue = (Ssb+Stb)/Sm;
         double b = 1.0 + Ssb/(Ssb+Stb);
         double m = -0.15 * b + 0.8;
         return Math.Exp(-1.33 * b * Math.Pow(tempValue, m));
      }

      private double CalculateBundleBypassDpFactor(double Re, double Fbp, int Nss, double Nc) {
         double tempValue = (double)Nss/(double)Nc;
         double Cbp = 4.5;
         if (Re > 100) 
         {
            Cbp = 3.7;
         }
         return Math.Exp(-Cbp * Fbp * (1.0-2.0*Math.Pow(tempValue, 1.0/3.0)));
      }

      private double CalculateUneaqualBaffleSpacingDpFactor(double Re, int Nb, double lin, double lout, double lc) 
      {
         double linStar = lc/lin;
         double loutStar = lc/lout;
         double n = 1.0;
         if (Re > 100)
         {
            n = 0.2;
         }
         n = 2.0 - n;
         return (Math.Pow(linStar, n) + Math.Pow(loutStar, n));
      }

      //private void InitializeGeometryParams() 
      //{
      //   CalculateTubeBundleDiameter();
      //   CalculateTubePitchesAndRelated();
      //   CalculateFractionOfTotalTubesInCrossFlow();  
      //   CalculateShellToBaffleLeakageArea();
      //}

      internal override void PrepareGeometry() 
      {
         CalculateTubeBundleDiameter();
         CalculateTubeDiameters();
         CalculateTubePitches();
         CalculateBaffleSpacing();

         CalculateCrossFlowArea();
         CalculateTubeRowsInOneCrossFlowSetion();
         CalculateCrossFlowRowsInEachWindow();

         CalculateFractionOfTotalTubesInCrossFlow();
         CalculateShellToBaffleLeakageArea();
         CalculateHeatTransferArea();

         //if (owner.BeingSpecifiedProcessVar is ProcessVarDouble) 
         //{
         //   ProcessVarDouble pv = owner.BeingSpecifiedProcessVar as ProcessVarDouble; 
         //   if (pv == ratingModel.TubeInnerDiameter) 
         //   {
         //      if (ratingModel.TubeWallThickness.Value != Constants.NO_VALUE && ratingModel.TubeWallThickness.IsSpecified && pv.Value != Constants.NO_VALUE) 
         //      {
         //         TubeOuterDiameterChanged();
         //      }
         //   }
         //   else if (pv == ratingModel.TubeOuterDiameter) 
         //   {
         //      TubeOuterDiameterChanged();
         //   }
         //   else if (pv == ratingModel.TubeWallThickness) 
         //   {
         //      if (ratingModel.TubeInnerDiameter.Value != Constants.NO_VALUE && ratingModel.TubeInnerDiameter.IsSpecified && pv.Value != Constants.NO_VALUE) 
         //      {
         //         TubeOuterDiameterChanged();
         //      }
         //   }

         //   else if (pv == ratingModel.TubePitch) 
         //   {
         //      CalculateTubePitchesAndRelated();
         //      //above method includes the flowing calls
         //      //CalculateTubeRowsInOneCrossFlowSetion();
         //      //CalculateCrossFlowRowsInEachWindow();
         //      //CalculateCrossFlowArea();
         //   }
         //   else if (pv == ratingModel.ShellInnerDiameter) 
         //   {
         //      CalculateTubeRowsInOneCrossFlowSetion();
         //      //the following methods includs
         //      //CalculateTubeToBaffleLeakageArea(); and
         //      //CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
         //      CalculateFractionOfTotalTubesInCrossFlow();  
         //      CalculateCrossFlowArea();
         //      CalculateShellToBaffleLeakageArea();
         //   }
         //   else if (pv == ratingModel.BundleToShellDiametralClearance) 
         //   {
         //      CalculateFractionOfTotalTubesInCrossFlow();
         //      CalculateCrossFlowArea();
         //   }
         //   else if (pv == ratingModel.BaffleCut) 
         //   {
         //      CalculateTubeRowsInOneCrossFlowSetion();
         //      CalculateFractionOfTotalTubesInCrossFlow();
         //      CalculateCrossFlowRowsInEachWindow();
         //      CalculateShellToBaffleLeakageArea();
         //   }
         //   else if (pv == ratingModel.ShellToBaffleDiametralClearance) 
         //   {
         //      CalculateShellToBaffleLeakageArea();
         //   }
         //}
         //else if (owner.BeingSpecifiedProcessVar is ProcessVarInt) 
         //{
         //   ProcessVarInt pv = owner.BeingSpecifiedProcessVar as ProcessVarInt; 
         //   if (pv == ratingModel.TubesPerTubePass || pv == ratingModel.TubePassesPerShellPass) 
         //   {
         //      CalculateTubeToBaffleLeakageArea();
         //      CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
         //   }
         //}

         //CalculateHeatTransferArea();
      }

      //internal override void TubeLayoutChanged() 
      //{
      //   CalculateTubePitchesAndRelated();
      //}
      
      //private void TubeOuterDiameterChanged() 
      //{
      //   CalculateCrossFlowArea();
      //   CalculateTubeToBaffleLeakageArea();
      //   CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
      //}

      //private void CalculateTubePitchesAndRelated() 
      //{
      //   CalculateTubePitches();

      //   CalculateTubeRowsInOneCrossFlowSetion();
      //   CalculateCrossFlowRowsInEachWindow();
      //   CalculateCrossFlowArea();
      //}

      protected override void CalculateBaffleSpacing() {
         double bs = (ratingModel.TubeLengthBetweenTubeSheets.Value - ratingModel.EntranceBaffleSpacing.Value - ratingModel.ExitBaffleSpacing.Value) / (ratingModel.NumberOfBaffles.Value - 1);
         owner.Calculate(ratingModel.BaffleSpacing, bs);
         //CalculateCrossFlowArea();
      }

      private void CalculateTubeBundleDiameter() 
      {
         tubeBundleDiameter = ratingModel.ShellInnerDiameter.Value - 2.0 * ratingModel.BundleToShellDiametralClearance.Value;
      }

      private void CalculateTubePitches() 
      {
         if (ratingModel.TubeLayout == TubeLayout.InlineSquare) 
         {
            tubePitchParalellToFlow = ratingModel.TubePitch.Value;
            tubePitchNormalToFlow = ratingModel.TubePitch.Value;
         }
         else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare) 
         {
            tubePitchParalellToFlow = 0.707 * ratingModel.TubePitch.Value;
            tubePitchNormalToFlow = 0.707 * ratingModel.TubePitch.Value;
         }
         else if (ratingModel.TubeLayout == TubeLayout.Triangular) 
         {
            tubePitchParalellToFlow = 0.866 * ratingModel.TubePitch.Value;
            tubePitchNormalToFlow = 0.5 * ratingModel.TubePitch.Value;
         }
      }
    
      //Number of tube rows in one cross flow setion
      private void CalculateTubeRowsInOneCrossFlowSetion() 
      {
         double shellDiam = ratingModel.ShellInnerDiameter.Value;
         baffleCutLengthValue = ratingModel.BaffleCut.Value*shellDiam;
         tubeRowsInOneCrossFlowSetion = (int) (shellDiam*(1.0-2.0*baffleCutLengthValue*shellDiam)/tubePitchParalellToFlow);
      }

      private void CalculateFractionOfTotalTubesInCrossFlow() 
      {
         double tempValue = (ratingModel.ShellInnerDiameter.Value-2.0*baffleCutLengthValue)/tubeBundleDiameter;
         double acosValue = Math.Acos(tempValue);
         fractionOfTotalTubesInCrossFlow = (Math.PI+2.0*tempValue*Math.Sin(acosValue) - 2.0*acosValue)/Math.PI;
         CalculateTubeToBaffleLeakageArea();
         CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
      }

      private void CalculateCrossFlowRowsInEachWindow() 
      {
         crossFlowRowsInEachWindow = (int) (0.8*baffleCutLengthValue/tubePitchParalellToFlow);
      }

      protected override void CalculateCrossFlowArea() 
      {
         if (ratingModel.TubeLayout == TubeLayout.InlineSquare || ratingModel.TubeLayout == TubeLayout.RotatedSquare) 
         {
            crossFlowArea = ratingModel.BaffleSpacing.Value*(ratingModel.ShellInnerDiameter.Value-tubeBundleDiameter+(tubeBundleDiameter-ratingModel.TubeOuterDiameter.Value)/tubePitchNormalToFlow*(ratingModel.TubePitch.Value-ratingModel.TubeOuterDiameter.Value));
         }
         else if (ratingModel.TubeLayout == TubeLayout.Triangular) 
         {
            crossFlowArea = ratingModel.BaffleSpacing.Value*(ratingModel.ShellInnerDiameter.Value-tubeBundleDiameter+(tubeBundleDiameter-ratingModel.TubeOuterDiameter.Value)/ratingModel.TubePitch.Value*(ratingModel.TubePitch.Value-ratingModel.TubeOuterDiameter.Value));
         }

         if (crossFlowArea <= 0) {
            throw new CalculationFailedException("Calculated cross flow area is less than  0");
         }

         CalculateFractionOfCrossFlowAreaForBypass();
      }

      private void CalculateFractionOfCrossFlowAreaForBypass() 
      {
         fractionOfCrossFlowAreaForBypass = (ratingModel.ShellInnerDiameter.Value - tubeBundleDiameter) * ratingModel.BaffleSpacing.Value/crossFlowArea;
      }

      private void CalculateTubeToBaffleLeakageArea() 
      {
         tubeToBaffleLeakageArea = 6.223e-4*ratingModel.TubeOuterDiameter.Value*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0+fractionOfTotalTubesInCrossFlow);
      }

      private void CalculateShellToBaffleLeakageArea() 
      {
         shellToBaffleLeakageArea = 0.5*ratingModel.ShellInnerDiameter.Value*ratingModel.ShellToBaffleDiametralClearance.Value*(Math.PI-Math.Acos(1.0-2.0*baffleCutLengthValue/ratingModel.ShellInnerDiameter.Value));
      }

      private void CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow() 
      {
         double tempValue = 1.0-2.0*baffleCutLengthValue/ratingModel.ShellInnerDiameter.Value;
         double Swg = 0.25*ratingModel.ShellInnerDiameter.Value*ratingModel.ShellInnerDiameter.Value*(Math.Acos(tempValue)-tempValue*Math.Sqrt(1.0-tempValue*tempValue));
         double Swt = 0.125*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0-fractionOfTotalTubesInCrossFlow)*Math.PI*ratingModel.TubeOuterDiameter.Value*ratingModel.TubeOuterDiameter.Value;
         areaForFlowThroughWindow = Swg - Swt;

         tempValue = 2.0*Math.Acos(tempValue);
         equivalentDiameterOfWindow = 4.0*areaForFlowThroughWindow/(0.5*Math.PI*ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*(1.0-fractionOfTotalTubesInCrossFlow)*ratingModel.TubeOuterDiameter.Value*tempValue);
      }

      protected ShellHTCAndDPCalculatorBellDelaware (SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
         //InitializeGeometryParams();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);        
      }
   }
}
