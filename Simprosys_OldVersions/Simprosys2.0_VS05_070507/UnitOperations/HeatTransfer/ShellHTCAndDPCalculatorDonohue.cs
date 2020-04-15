using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   /// <summary>
   /// Summary description for ShellHTCAndDPCalculatorSimple.
   /// </summary>
   [Serializable]
   public class ShellHTCAndDPCalculatorDonohue : ShellHTCAndDPCalculator 
   {
      //private const int CLASS_PERSISTENCE_VERSION = 1; 

      //Calculated variables
      private double baffleWindowArea;
      private double transversalFlowArea;

      public ShellHTCAndDPCalculatorDonohue(HXRatingModelShellAndTube ratingModel) : base (ratingModel)
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
         procVarList.Add(ratingModel.ShellInnerDiameter);
         ratingModel.ProcVarList.AddRange(procVarList);
         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      internal override double GetVelocity(double massFlowRate, double density) 
      {
         double v = massFlowRate/(density*transversalFlowArea);
         return v;
      }

      internal override double GetReynoldsNumber(double massFlowRate, double viscosity) 
      {
         double Do = ratingModel.TubeOuterDiameter.Value;
         double massVelocity = CalculateAverageMassVelocity(massFlowRate);
         double Re = Do*massVelocity/viscosity;
         return Re;
      }
      
      //private void InitializeGeometryParams() 
      //{
      //   CalculateTubeDiameters();
      //   CalculateBaffleSpacing();
      //   CalculateBaffleWindowArea();
      //}

      internal override void PrepareGeometry() 
      {
         CalculateTubeDiameters();
         CalculateBaffleSpacing();

         CalculateBaffleWindowArea();
         CalculateCrossFlowArea();
         CalculateHeatTransferArea();

         //if (owner.BeingSpecifiedProcessVar is ProcessVarDouble) 
         //{
         //   ProcessVarDouble pv = owner.BeingSpecifiedProcessVar as ProcessVarDouble; 
         //   if (pv == ratingModel.TubeInnerDiameter) 
         //   {
         //      if (ratingModel.TubeWallThickness.HasValue && ratingModel.TubeWallThickness.IsSpecified && pv.Value != Constants.NO_VALUE) 
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
         //      if (ratingModel.TubeInnerDiameter.HasValue && ratingModel.TubeInnerDiameter.IsSpecified && pv.Value != Constants.NO_VALUE) 
         //      {
         //         TubeOuterDiameterChanged();
         //      }
         //   }
         //   else if (pv == ratingModel.TubePitch) 
         //   {
         //      CalculateCrossFlowArea();
         //   }
         //   else if (pv == ratingModel.ShellInnerDiameter) 
         //   {
         //      CalculateBaffleWindowArea();
         //      CalculateCrossFlowArea();
         //   }
         //   else if (pv == ratingModel.BaffleCut) 
         //   {
         //      CalculateBaffleWindowArea();
         //   }
         //}
         //else if (owner.BeingSpecifiedProcessVar is ProcessVarInt) 
         //{
         //   ProcessVarInt pv = owner.BeingSpecifiedProcessVar as ProcessVarInt; 
         //   if (pv == ratingModel.TubesPerTubePass) 
         //   {
         //      CalculateBaffleWindowArea();
         //   }
         //}

         //CalculateHeatTransferArea();
      }

      internal override double CalculateSinglePhaseHTC(double massFlowRate, double density, double bulkVisc, double wallVisc, 
         double thermalCond, double specificHeat) 
      { 
         //Donohue equation from Unit Operations of Chemical Engineering Eq.15.6
         double Do = ratingModel.TubeOuterDiameter.Value;
         //double massVelocity = CalculateAverageMassVelocity(massFlowRate);
         //double Re = Do*massVelocity/bulkVisc;
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc); 
         double Pr = specificHeat*bulkVisc/thermalCond;
         double Nut = 0.2*Math.Pow(Re, 0.6)*Math.Pow(Pr, 0.33)*Math.Pow(bulkVisc/wallVisc, 0.14);
         return Nut*thermalCond/Do;
      }

      internal override double CalculateSinglePhaseDP(double massFlowRate, double density, double bulkVisc, double wallViscosity) 
      {
         double Ds = ratingModel.ShellInnerDiameter.Value;
         int Nb = ratingModel.NumberOfBaffles.Value + 1;
         //double Do = ratingModel.TubeOuterDiameter.Value;         
         double De = CalculateEquivalentDiameter();
         double massVelocity = CalculateAverageMassVelocity(massFlowRate);
         //double Re = Do*massVelocity/bulkVisc;
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc);
         //double f = ChartUtil.GetInterpolateValue(frictionFactorCurve, Math.Log10(Re));
         //f = Math.Pow(10, f);
         double f = Math.Exp(0.576 - 0.19 * Math.Log(Re));
         double dp = f*massVelocity*massVelocity*Ds*(Nb+1)/(2.0*density*De);
         return dp;
      }

      private double CalculateEquivalentDiameter() {
         double Do = ratingModel.TubeOuterDiameter.Value;
         double Pt = ratingModel.TubePitch.Value;
         double De = Do;
         if (ratingModel.TubeLayout == TubeLayout.InlineSquare) {
            De = 4.0 * (Pt * Pt - Math.PI / 4.0 * Do * Do) / (Math.PI * Do);
         }
         else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare) {
            De = 4.0 * (0.707 * Pt * Pt - Math.PI / 4.0 * Do * Do) / (Math.PI * Do);
         }
         else if (ratingModel.TubeLayout == TubeLayout.Triangular) {
            De = 4.0 * (0.866 * Pt * Pt - Math.PI / 4.0 * Do * Do) / (Math.PI * Do);
         }
         return De;
      }
      
      //private void TubeOuterDiameterChanged() 
      //{
      //   CalculateBaffleWindowArea();
      //   CalculateCrossFlowArea();
      //}

      //friction of baffle window area
      private double CalculateFractionOfBaffleWindowArea() 
      {
         double bcValue = ratingModel.BaffleCut.Value;
         double temp =  Math.Sqrt(bcValue*(1.0-bcValue));
         double angle = Math.Asin(2.0*temp);
         double fractionOfCrossSectionArea = 1.0/Math.PI * angle - 4.0/Math.PI*temp*(0.5-bcValue);
         return fractionOfCrossSectionArea;
      }

      //baffle window area
      private void CalculateBaffleWindowArea() 
      {
         double fb = CalculateFractionOfBaffleWindowArea();
         int nb = (int) fb * ratingModel.TubesPerTubePass.Value;
         double ds = ratingModel.ShellInnerDiameter.Value;
         double d0 = ratingModel.TubeOuterDiameter.Value;
         baffleWindowArea = 0.25*Math.PI*(fb*ds*ds - nb*d0*d0);
      }
      
      protected override void CalculateCrossFlowArea() 
      {
         double bs = ratingModel.BaffleSpacing.Value;
         double ds = ratingModel.ShellInnerDiameter.Value;
         double d0 = ratingModel.TubeOuterDiameter.Value;
         double p = ratingModel.TubePitch.Value;
         transversalFlowArea = bs*ds*(1.0-d0/p);
      }

      private double CalculateAverageMassVelocity(double massFlowRate) 
      {
         double gb = massFlowRate/baffleWindowArea;
         double gc = massFlowRate/transversalFlowArea;
         double average = Math.Sqrt(gb*gc);
         return average;
      }

      protected ShellHTCAndDPCalculatorDonohue (SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
//         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionShellHTCAndDPCalculatorSimple", typeof(int));
//         if (persistedClassVersion == 1) 
//         {
//            this.baffleWindowArea = (double) info.GetValue("BaffleWindowArea", typeof(double));
//            this.transversalFlowArea = (double) info.GetValue("TransversalFlowArea", typeof(double));
//         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);        
//         info.AddValue("ClassPersistenceVersionShellHTCAndDPCalculatorSimple", CLASS_PERSISTENCE_VERSION, typeof(int));
//         info.AddValue("BaffleWindowArea", this.baffleWindowArea, typeof(double));
//         info.AddValue("TransversalFlowArea", this.transversalFlowArea, typeof(double));
      }
   }
}
