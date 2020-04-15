using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   /// <summary>
   /// Summary description for ShellHTCAndDPCalculatorSimple.
   /// </summary>
   [Serializable]
   public class ShellHTCAndDPCalculatorKern : ShellHTCAndDPCalculator 
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      //Unit Operations in Food Engineering, Page 397, Fig 13-12
      //Curve: (10, 6.0), (20, 3.0), (50, 1.45), (100, 0.95), (200, 0.708), (400, 0.6), (1.0e6, 0.15)
//      static readonly PointF[] frictionFactorCurve = {new PointF((float)Math.Log10(10), (float)Math.Log10(6.0)), 
//                                                      new PointF((float)Math.Log10(20), (float)Math.Log10(3.0)),
//                                                      new PointF((float)Math.Log10(50), (float)Math.Log10(1.45)), 
//                                                      new PointF((float)Math.Log10(100), (float)Math.Log10(0.95)),
//                                                      new PointF((float)Math.Log10(200), (float)Math.Log10(0.708)),
//                                                      new PointF((float)Math.Log10(400), (float)Math.Log10(0.6)),
//                                                      new PointF((float)Math.Log10(1.0e6), (float)Math.Log10(0.15))};
      public ShellHTCAndDPCalculatorKern(HXRatingModelShellAndTube ratingModel) : base (ratingModel)
      {
         InitializeVarList();
         InitializeGeometryParams();
      }

      private void InitializeVarList() 
      {
         procVarList.Add(ratingModel.TubePitch);
         procVarList.Add(ratingModel.BaffleSpacing);
         procVarList.Add(ratingModel.NumberOfBaffles);
         procVarList.Add(ratingModel.ShellInnerDiameter);
         ratingModel.ProcVarList.AddRange(procVarList);
         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      internal override double GetVelocity(double massFlowRate, double density) 
      {
         double v = massFlowRate/(density*CalculateFlowArea());
         return v;
      }

      internal override double GetReynoldsNumber(double massFlowRate, double bulkVisc) 
      {
         double De = CalculateEquivalentDiameter();
         double Ss = CalculateFlowArea();
         double massVelocity = massFlowRate/Ss;
         double Re = De*massVelocity/bulkVisc;
         return Re;
      }
      
      private void InitializeGeometryParams() 
      {
         CalculateTubeDiameters();
         CalculateBaffleSpacing();
      }

      internal override void PrepareGeometry() 
      {
         ratingModel.BaffleCut.Value = 0.25;
         CalculateTubeDiameters();
         CalculateBaffleSpacing();
         CalculateHeatTransferArea();
      }

      private double CalculateEquivalentDiameter() 
      {
         double Do = ratingModel.TubeOuterDiameter.Value;
         double Pt = ratingModel.TubePitch.Value;
         double De = Do;
         if (ratingModel.TubeLayout == TubeLayout.InlineSquare) 
         {
            De = 4.0 * (Pt*Pt - Math.PI/4.0 * Do * Do)/(Math.PI * Do);
         }
         else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare) 
         {
            De = 4.0 * (0.707*Pt*Pt - Math.PI/4.0 * Do * Do)/(Math.PI * Do);
         }
         else if (ratingModel.TubeLayout == TubeLayout.Triangular) 
         {
            De = 4.0 * (0.866*Pt*Pt - Math.PI/4.0 * Do * Do)/(Math.PI * Do);
         }
         return De;
      }

      private double CalculateFlowArea() 
      {
         double Pt = ratingModel.TubePitch.Value;
         double Do = ratingModel.TubeOuterDiameter.Value;
         double Ds = ratingModel.ShellInnerDiameter.Value;
         double Lb = ratingModel.BaffleSpacing.Value;
         double Ss = Ds * (Pt - Do) * Lb/Pt;
         return Ss;
      }

      
      internal override double CalculateSinglePhaseHTC(double massFlowRate, double density, double bulkVisc, double wallVisc, 
         double thermalCond, double specificHeat) 
      { 
         double De = CalculateEquivalentDiameter();
         double Ss = CalculateFlowArea();
         double massVelocity = massFlowRate/Ss;
         double Re = De*massVelocity/bulkVisc;
         double Pr = specificHeat*bulkVisc/thermalCond;
         //correlation valid for Re in the range of 2100 to 1.0e6
         double Nut = 0.36*Math.Pow(Re, 0.55)*Math.Pow(Pr, 0.33)*Math.Pow(bulkVisc/wallVisc, 0.14);
         return Nut*thermalCond/De;
      }

      internal override double CalculateSinglePhaseDP(double massFlowRate, double density, double bulkVisc, double wallViscosity) 
      {
         int Nb = ratingModel.NumberOfBaffles.Value + 1;
         double Ds = ratingModel.ShellInnerDiameter.Value;
         double Ss = CalculateFlowArea();
         double De = CalculateEquivalentDiameter();
         double massVelocity = massFlowRate/Ss;
         double Re = De*massVelocity/bulkVisc;
//         double f = ChartUtil.GetInterpolateValue(frictionFactorCurve, Math.Log10(Re));
//         f = Math.Pow(10, f);
         double f = Math.Exp(0.576 - 0.19 * Math.Log(Re)); //Re valid in the range of 400 to 1.0e6 
         double dp = f*massVelocity*massVelocity*Ds*(Nb+1)/(2.0*density*De);
         return dp;
      }
      
      protected ShellHTCAndDPCalculatorKern (SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);        
      }
   }
}
