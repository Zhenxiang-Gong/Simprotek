using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer
{
   /// <summary>
   /// Summary description for ShellHTCAndDPCalculator.
   /// </summary>
   [Serializable]
   public abstract class ShellHTCAndDPCalculator : Storable
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected HXRatingModelShellAndTube ratingModel;
      protected ArrayList procVarList = new ArrayList();
      protected HeatExchanger owner;

      internal ArrayList ProcVarList 
      {
         get {return procVarList;}
      }

      protected ShellHTCAndDPCalculator(HXRatingModelShellAndTube ratingModel) 
      {
         this.ratingModel = ratingModel;
         this.owner = ratingModel.Owner;
         //InitializeVarList();
      }

      internal abstract double CalculateSinglePhaseHTC(double massFlowRate, double density, double bulkViscosity, double wallViscosity, double thermCond, double specificHeat);

      internal abstract double CalculateSinglePhaseDP(double massFlowRate, double density, double bulkViscosity, double wallViscosity);
      
      internal abstract void PrepareGeometry();

      internal abstract double GetVelocity(double massFlowRate, double density); 
      internal abstract double GetReynoldsNumber(double massFlowRate, double bulkViscosity);
      
      protected virtual void CalculateCrossFlowArea() 
      {
      }
      
      //internal virtual void TubeLayoutChanged() 
      //{
      //}

      protected virtual void CalculateBaffleSpacing() 
      {
         double bs = ratingModel.TubeLengthBetweenTubeSheets.Value / (ratingModel.NumberOfBaffles.Value - 1);
         owner.Calculate(ratingModel.BaffleSpacing, bs);
         //CalculateCrossFlowArea();
      }

      //protected abstract void TubeOuterDiameterChanged() ;

      internal double CalculateVerticalCondensingHTC_Colburn(double massFlowRate, double diameter, double liqDensity, double liqViscosity, double liqThermalCond) 
      {
         /*double gamma = massFlowRate/(Math.PI*diameter);
         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
         double h = 5.35*tempValue*thermalCond/(4.0*gamma);
         return h;*/
         double h = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Colburn(massFlowRate, diameter, liqDensity, liqViscosity, liqThermalCond);
         return h;
      }

      internal double CalculateVerticalCondensingHTC_Nusselt(double massFlowRate, double diameter, double length, double liqDensity, double liqViscosity, double liqThermalCond) 
      {
         /*double gamma = massFlowRate/(Math.PI*diameter);
         double tempValue = length * length * length * density * density * 9.8065/(viscosity*gamma);
         double h = thermalCond/length * 0.925 * Math.Pow(tempValue, 1/3);
         return h;*/
         double h = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Nusselt(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
         return h;
      }

      internal double CalculateHorizontalCondensingHTC_Colburn(double massFlowRate, double length, double liqDensity, double liqViscosity, double liqThermalCond) 
      {
         /*double gamma = massFlowRate/(2.0*length);
         double tempValue = gamma/Math.Pow(3.0*viscosity*gamma/(density*density*9.8065), 1.0/3.0);
         double h = 4.4*tempValue*thermalCond/(4.0*gamma);
         return h;*/
         double h = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Colburn(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
         return h;
      }

      internal double CalculateHorizontalCondensingHTC(double massFlowRate, double diameter, double length, double liqDensity, double liqViscosity, double liqThermalCond) 
      {
         /*double gamma = massFlowRate/(2.0*length);
         double tempValue = diameter * diameter * diameter * density * density * 9.8065/(viscosity*gamma);
         double h = thermalCond/diameter * 0.73 * Math.Pow(tempValue, 1/3);
         return h;*/
         double h = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Colburn(massFlowRate, length, liqDensity, liqViscosity, liqThermalCond);
         return h;
      }

      internal double CalculateNucleateBoilingHTC(double heatFlux, double pressure, double criticalPressure) 
      {
         /*double gamma = massFlowRate/(2.0*length);
         double tempValue = diameter * diameter * diameter * density * density * 9.8065/(viscosity*gamma);
         double h = thermalCond/diameter * 0.73 * Math.Pow(tempValue, 1/3);
         return h;*/
         double h = BoilingHeatTransferCoeffCalculator.CalculateNucleateBoilingHTC_Mostinski(heatFlux, pressure, criticalPressure);
         return h;
      }
      
      protected void CalculateTubeDiameters() 
      {
         if (ratingModel.TubeOuterDiameter.HasValue && ratingModel.TubeInnerDiameter.HasValue) 
         {
            owner.Calculate(ratingModel.TubeWallThickness, 0.5* (ratingModel.TubeOuterDiameter.Value - ratingModel.TubeInnerDiameter.Value));
         }
         else if (ratingModel.TubeWallThickness.HasValue && ratingModel.TubeInnerDiameter.HasValue) 
         {
            owner.Calculate(ratingModel.TubeOuterDiameter, (ratingModel.TubeInnerDiameter.Value + 2.0*ratingModel.TubeWallThickness.Value));
         }
         else if (ratingModel.TubeWallThickness.HasValue && ratingModel.TubeOuterDiameter.HasValue) 
         {
            owner.Calculate(ratingModel.TubeInnerDiameter, ratingModel.TubeOuterDiameter.Value - 2.0*ratingModel.TubeWallThickness.Value);
         }
      }
   
      protected void CalculateHeatTransferArea() 
      {
         int totalTubes = ratingModel.TubesPerTubePass.Value*ratingModel.TubePassesPerShellPass.Value*ratingModel.ShellPasses.Value;
         ratingModel.TotalTubesInShell.Value = totalTubes;
         double htArea = Math.PI * ratingModel.TubeOuterDiameter.Value * totalTubes * ratingModel.TubeLengthBetweenTubeSheets.Value;
         owner.Calculate(ratingModel.TotalHeatTransferArea, htArea);
      }

      protected ShellHTCAndDPCalculator(SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionShellHTCAndDPCalculator", typeof(int));
         if (persistedClassVersion == 1) 
         {
            this.owner = info.GetValue("Owner", typeof(HeatExchanger)) as HeatExchanger;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;
            this.ratingModel = (HXRatingModelShellAndTube) info.GetValue("RatingModel", typeof(HXRatingModelShellAndTube));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionShellHTCAndDPCalculator", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Owner", this.owner, typeof(HeatExchanger));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         info.AddValue("RatingModel", this.ratingModel, typeof(HXRatingModelShellAndTube));
      }
   }
}

