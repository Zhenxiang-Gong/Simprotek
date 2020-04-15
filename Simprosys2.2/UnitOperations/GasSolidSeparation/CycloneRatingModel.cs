using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.HeatTransfer;

namespace Prosimo.UnitOperations.GasSolidSeparation {
   public enum CycloneInletConfiguration {Tangential = 0, Scroll, Volute};
   public enum ParticleTypeGroup {A = 0, B, C, D}; 


   [Serializable] 
   public class CycloneRatingModel : GasSolidSeparatorRatingModel {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      //private Cyclone cycloneOwner;
      protected ArrayList procVarList = new ArrayList();
      private ProcessVarInt numberOfCyclones;
      
      private ParticleTypeGroup particleType;
      //private ProcessVarDouble particleDensity;
      //private ProcessVarDouble particleBulkDensity;
      private ProcessVarDouble cutParticleDiameter;

      private CycloneInletConfiguration inletConfiguration;
      private ProcessVarDouble inletWidth;
      private ProcessVarDouble inletHeight;
      private ProcessVarDouble inletHeightToWidthRatio;
      private ProcessVarDouble inletVelocity;

      private ProcessVarDouble cycloneDiameter;
      private ProcessVarDouble outletInnerDiameter;
      private ProcessVarDouble outletWallThickness;
      
      private ProcessVarDouble outletTubeLengthBelowRoof;
      private ProcessVarDouble outletBelowRoofToInletHeightRatio;
      
      private ProcessVarDouble naturalVortexLength;
      private ProcessVarDouble diplegDiameter;
      private ProcessVarDouble externalVesselDiameter;

      private ProcessVarDouble coneAngle;
      private ProcessVarDouble barrelLength;
      private ProcessVarDouble coneLength;
      private ProcessVarDouble barrelPlusConeLength;

      //private ArrayList particleSizeFractionAndEfficiencyList = new ArrayList(); 

      #region public properties

      /*internal Cyclone Owner {
         get {return owner;}
         //set {owner = value;}
      }*/
      //ProcessVariables
      public ProcessVarInt NumberOfCyclones {
         get { return numberOfCyclones; }
      }
      
      public ParticleTypeGroup ParticleTypeGroup {
         get {return particleType;}
//         set {
//            particleType = value;
//            ownerUnitOp.HasBeenModified(true);
//         }
      }

      /*public ProcessVarDouble ParticleDensity {
         get { return particleDensity; }
      }

      public ProcessVarDouble ParticleBulkDensity {
         get { return particleBulkDensity; }
      } */

      public ProcessVarDouble CutParticleDiameter {
         get { return cutParticleDiameter; }
      }

      public CycloneInletConfiguration InletConfiguration {
         get {return inletConfiguration;}
//         set {
//            inletConfiguration = value;
//            ownerUnitOp.HasBeenModified(true);
//         }
      }

      public ProcessVarDouble InletWidth {
         get { return inletWidth; }
      }
      
      public ProcessVarDouble InletHeight {
         get { return inletHeight; }
      }
      
      public ProcessVarDouble InletHeightToWidthRatio {
         get { return inletHeightToWidthRatio; }
      }

      public ProcessVarDouble CycloneDiameter {
         get { return cycloneDiameter; }
      }
      
      public ProcessVarDouble OutletInnerDiameter {
         get { return outletInnerDiameter; }
      }

      public ProcessVarDouble OutletWallThickness {
         get { return outletWallThickness; }
      }

      public ProcessVarDouble OutletBelowRoofToInletHeightRatio {
         get { return outletBelowRoofToInletHeightRatio; }
      }
      
      public ProcessVarDouble OutletTubeLengthBelowRoof {
         get { return outletTubeLengthBelowRoof; }
      }
      
      public ProcessVarDouble DiplegDiameter {
         get { return diplegDiameter; }
      }

      public ProcessVarDouble ExternalVesselDiameter {
         get { return externalVesselDiameter; }
      }

      public ProcessVarDouble InletVelocity {
         get { return inletVelocity; }
      }
      
      public ProcessVarDouble NaturalVortexLength {
         get { return naturalVortexLength; }
      }
      
      public ProcessVarDouble ConeAngle {
         get { return coneAngle; }
      }
      
      public ProcessVarDouble BarrelLength {
         get { return barrelLength; }
      }
      
      public ProcessVarDouble ConeLength {
         get { return coneLength; }
      }
      
      public ProcessVarDouble BarrelPlusConeLength {
         get { return barrelPlusConeLength; }
      }
            
      /*public ParticleDistributions GetParticleDistributions() {
         return new ParticleDistributions(this);
      }

      internal ArrayList ParticleSizeFractionAndEfficiencyList {
         get {return particleSizeFractionAndEfficiencyList;}
         set {particleSizeFractionAndEfficiencyList = value;}
      }*/

      #endregion
      
      public CycloneRatingModel(IGasSolidSeparator owner) : base (owner) {
         //this.cycloneOwner = (Cyclone) owner;
         numberOfCyclones = new ProcessVarInt(StringConstants.NUMBER_OF_CYCLONES, PhysicalQuantity.Unknown, 1, VarState.Specified, ownerUnitOp);
         
         //particleDensity = new ProcessVarDouble(StringConstants.PARTICLE_DENSITY, PhysicalQuantity.Density, VarState.Specified, ownerUnitOp);
         //particleBulkDensity = new ProcessVarDouble(StringConstants.PARTICLE_BULK_DENSITY, PhysicalQuantity.Density, VarState.Specified, ownerUnitOp);
         cutParticleDiameter = new ProcessVarDouble(StringConstants.CUT_PARTICLE_DIAMETER, PhysicalQuantity.MicroLength, VarState.Specified, ownerUnitOp);

         inletConfiguration = CycloneInletConfiguration.Tangential;
         inletWidth = new ProcessVarDouble(StringConstants.INLET_WIDTH, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         inletHeight = new ProcessVarDouble(StringConstants.INLET_HEIGHT, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         inletHeightToWidthRatio = new ProcessVarDouble(StringConstants.INLET_HEIGHT_TO_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, ownerUnitOp);
         inletVelocity = new ProcessVarDouble(StringConstants.INLET_VELOCITY, PhysicalQuantity.Velocity, VarState.Specified, ownerUnitOp);
         cycloneDiameter = new ProcessVarDouble(StringConstants.CYCLONE_DIAMETER, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         outletInnerDiameter = new ProcessVarDouble(StringConstants.OUTLET_INNER_DIAMETER, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         outletWallThickness = new ProcessVarDouble(StringConstants.OUTLET_WALL_THICKNESS, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         diplegDiameter = new ProcessVarDouble(StringConstants.DIPLEG_DIAMETER, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         outletTubeLengthBelowRoof = new ProcessVarDouble(StringConstants.OUTLET_TUBE_LENGTH_BELOW_ROOF, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         outletBelowRoofToInletHeightRatio = new ProcessVarDouble(StringConstants.OUTLET_BELOW_ROOF_TO_INLET_HEIGHT_RATIO, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         naturalVortexLength = new ProcessVarDouble(StringConstants.NATURAL_VORTEX_LENGTH, PhysicalQuantity.Length, VarState.AlwaysCalculated, ownerUnitOp);
         externalVesselDiameter = new ProcessVarDouble(StringConstants.EXTERNAL_VESSEL_DIAMETER, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         
         coneAngle = new ProcessVarDouble(StringConstants.CONE_ANGLE, PhysicalQuantity.PlaneAngle, VarState.Specified, ownerUnitOp);
         barrelLength = new ProcessVarDouble(StringConstants.BARREL_LENGTH, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         coneLength = new ProcessVarDouble(StringConstants.CONE_LENGTH, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);
         barrelPlusConeLength = new ProcessVarDouble(StringConstants.BARREL_PLUS_CONE_LENGTH, PhysicalQuantity.Length, VarState.Specified, ownerUnitOp);

         //particleSizeFractionAndEfficiencyList.Add(new ParticleSizeFractionAndEfficiency(ownerUnitOp));
         //particleSizeFractionAndEfficiencyList.Add(new ParticleSizeFractionAndEfficiency());
         particleType = ParticleTypeGroup.A;

         InitializeVarListAndRegisterVars();
      }

      protected void InitializeVarListAndRegisterVars() {
         procVarList.Add(numberOfCyclones);
         //procVarList.Add(particleDensity);
         //procVarList.Add(particleBulkDensity);
         procVarList.Add(cutParticleDiameter);
         procVarList.Add(inletWidth);
         procVarList.Add(inletHeight);
         procVarList.Add(inletHeightToWidthRatio);
         procVarList.Add(inletVelocity);
         procVarList.Add(cycloneDiameter);
         procVarList.Add(outletInnerDiameter);
         procVarList.Add(outletWallThickness);
         procVarList.Add(diplegDiameter);
         procVarList.Add(outletTubeLengthBelowRoof);
         procVarList.Add(outletBelowRoofToInletHeightRatio);
         procVarList.Add(naturalVortexLength);
         procVarList.Add(externalVesselDiameter);
         procVarList.Add(coneAngle);
         procVarList.Add(barrelLength);
         procVarList.Add(coneLength);
         procVarList.Add(barrelPlusConeLength);

         ownerUnitOp.AddVarsOnListAndRegisterInSystem(procVarList);
      }
      
      public ErrorMessage SpecifyParticleTypeGroup(ParticleTypeGroup aValue) {
         if (aValue != particleType) {
            particleType = aValue;
            ownerUnitOp.HasBeenModified(true);
         }
         return null;
      }

      public ErrorMessage SpecifyInletConfiguration(CycloneInletConfiguration aValue) {
         if (aValue != inletConfiguration) {
            inletConfiguration = aValue;
            ownerUnitOp.HasBeenModified(true);
         }
         return null;
      }

      internal ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         if (aValue == Constants.NO_VALUE) {
            return null;
         }

         ErrorMessage retValue = null;
         if (pv == inletWidth)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == inletHeight)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == inletHeightToWidthRatio) 
         {
            if (aValue < 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanZeroErrorMessage(pv);;
            }
         }
         else if (pv == outletInnerDiameter) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == outletWallThickness) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == cycloneDiameter) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == diplegDiameter)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
            else if (cycloneDiameter.HasValue && aValue > cycloneDiameter.Value) 
            {
               retValue = ownerUnitOp.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " cannot be greater than " + cycloneDiameter.VarTypeName);
            }

         }
         else if (pv == coneLength) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == outletTubeLengthBelowRoof)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == outletBelowRoofToInletHeightRatio)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == externalVesselDiameter)
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == coneAngle) 
         {
            if (aValue <= 0.0 || aValue >= Math.PI/2) 
            {
               retValue = ownerUnitOp.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the cyclone must be in the range of 0 to 90 degrees");
            }
         }
         else if (pv == barrelLength) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == cutParticleDiameter) 
         {
            if (aValue <= 0.0 && aValue > 0.001) 
            {
               retValue = ownerUnitOp.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " must be in the range of 0 to 1000 µm");
            }
         }
         else if (pv == ParticleDensity) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == ParticleBulkDensity) 
         {
            if (aValue <= 0.0) 
            {
               retValue = ownerUnitOp.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         return retValue;
      }

      public override bool IsRatingCalcReady() 
      {
         //return true;
         bool retValue = false;
         if (owner.GasInlet.Temperature.HasValue && owner.GasInlet.VolumeFlowRate.HasValue) {
            retValue = true;
         }
         return retValue;
      }

   
      public void DoRatingCalculation() {
         double volumeFlow = owner.GasInlet.VolumeFlowRate.Value;
         
         double inletW = inletWidth.Value;
         double inletH = inletHeight.Value;
         double inletHToWRatio = inletHeightToWidthRatio.Value;
         double diamOutlet = outletInnerDiameter.Value;
         double wallThicknessOutlet = outletWallThickness.Value;
         double diamCyclone = cycloneDiameter.Value;
         int numOfCyclones = numberOfCyclones.Value;
         volumeFlow /= numOfCyclones;

         double outletTL = outletTubeLengthBelowRoof.Value;
         double outletTLToInletHRatio = outletBelowRoofToInletHeightRatio.Value;

         double loading = owner.InletParticleLoading.Value;
         if (loading == Constants.NO_VALUE) {
            loading = owner.CalculateParticleLoading(owner.GasInlet);
         }

         if (inletW != Constants.NO_VALUE && inletHToWRatio != Constants.NO_VALUE ) {
            inletH = inletW * inletHToWRatio;
            ownerUnitOp.Calculate(inletHeight, inletH);
         }
         else if (inletW != Constants.NO_VALUE && inletH != Constants.NO_VALUE ) {
            inletHToWRatio = inletH/inletW;
            ownerUnitOp.Calculate(inletHeightToWidthRatio, inletHToWRatio);
         }
         else if (inletH != Constants.NO_VALUE && inletHToWRatio != Constants.NO_VALUE ) {
            inletW = inletH/inletHToWRatio;
            ownerUnitOp.Calculate(inletWidth, inletW);
         }

         if (inletH != Constants.NO_VALUE && outletTLToInletHRatio != Constants.NO_VALUE ) {
            outletTL = inletH * outletTLToInletHRatio;
            ownerUnitOp.Calculate(outletTubeLengthBelowRoof, outletTL);
         }
         else if (inletH != Constants.NO_VALUE && outletTL != Constants.NO_VALUE ) {
            outletTLToInletHRatio = outletTL/inletH;
            ownerUnitOp.Calculate(outletBelowRoofToInletHeightRatio, outletTLToInletHRatio);
         }
         else if (outletTL != Constants.NO_VALUE && outletTLToInletHRatio != Constants.NO_VALUE ) {
            inletH = outletTL/outletTLToInletHRatio;
            ownerUnitOp.Calculate(inletHeight, inletH);
         }

         if (diamOutlet != Constants.NO_VALUE && wallThicknessOutlet != Constants.NO_VALUE && inletW != Constants.NO_VALUE
            && diamCyclone == Constants.NO_VALUE) {
            diamCyclone = 2.0 * inletW + diamOutlet + 2.0 * wallThicknessOutlet;
            ownerUnitOp.Calculate(cycloneDiameter, diamCyclone);
         }

         double inletArea = Constants.NO_VALUE;
         double outletArea = Constants.NO_VALUE;
         double inletV = Constants.NO_VALUE;
         double numOfTurns = Constants.NO_VALUE;
         double viscGas = owner.GasInlet.GetGasViscosity(owner.GasInlet.Temperature.Value);
         double densityParticle = ParticleDensity.Value;
         double densityGas = owner.GasInlet.Density.Value;
         double cutDiameter = cutParticleDiameter.Value;
         double collectionEff = owner.CollectionEfficiency.Value;
         double temp;
         //if (volumeFlow != Constants.NO_VALUE && viscGas != Constants.NO_VALUE) {
         //the above condition has already been guarded by IsRatingCalcReady()
         if (inletW != Constants.NO_VALUE && inletH != Constants.NO_VALUE) {
            inletArea = inletW * inletH;
            if (diamOutlet != Constants.NO_VALUE) {
               outletArea = Math.PI * diamOutlet * diamOutlet/4.0;
               inletArea = inletArea < outletArea ? inletArea : outletArea;
            }
            inletV = volumeFlow/inletArea;
            numOfTurns = CalculateNumOfTurns(inletV);
            cutDiameter = Math.Sqrt(9 * viscGas * inletW /(Math.PI * numOfTurns * inletV * (densityParticle - densityGas)));
            ownerUnitOp.Calculate(cutParticleDiameter, cutDiameter);
            ownerUnitOp.Calculate(inletVelocity, inletV);
            
            collectionEff = CalculateTotalCollectionEfficiency(particleSizeFractionAndEfficiencyList);
            ownerUnitOp.Calculate(owner.CollectionEfficiency, collectionEff);

            //solveState = SolveState.Solved;
            //if (dp != Constants.NO_VALUE) {
            //   double diamRatio = cutDiameter/dp;
            //   collectionEff = 1.0/(1.0 + diamRatio * diamRatio);
            //   Calculate(collectionEfficiency, ce);
            //   solveState = SolveState.Solved;
            //}
         }
         else if (cutDiameter != Constants.NO_VALUE && inletHToWRatio != Constants.NO_VALUE && inletW == Constants.NO_VALUE) {
            //double diamRatio = cutDiameter/dp;
            //collectionEff = 1.0/(1.0 + diamRatio * diamRatio);
            collectionEff = CalculateTotalCollectionEfficiency(particleSizeFractionAndEfficiencyList);
            ownerUnitOp.Calculate(owner.CollectionEfficiency, collectionEff);

            //Assume initail number of spirals is 3.5 
            numOfTurns = 3.5;
            int i = 0;
            double inletWOld = 0;
            double diff;
            do  {
               i++;
               temp = cutDiameter * cutDiameter * Math.PI * numOfTurns * volumeFlow * (densityParticle - densityGas)/(9.0 * viscGas * inletHToWRatio);
               inletW = Math.Pow(temp, 1.0/3.0);
               inletV = volumeFlow/(inletW * inletW * inletHToWRatio);
               numOfTurns = CalculateNumOfTurns(inletV); 
               diff  = inletW - inletWOld;
               inletWOld = inletW;
            } while (i <= 500 && Math.Abs(diff) > 1.0e-6);

            if (i < 500) {
               
               ownerUnitOp.Calculate(inletWidth, inletW);
               ownerUnitOp.Calculate(inletHeight, inletW * inletHToWRatio);
               ownerUnitOp.Calculate(inletVelocity, inletV);
            }

            if (diamOutlet != Constants.NO_VALUE && wallThicknessOutlet != Constants.NO_VALUE && inletW != Constants.NO_VALUE
               && diamCyclone == Constants.NO_VALUE) {
               diamCyclone = 2.0 * inletW + diamOutlet + 2.0 * wallThicknessOutlet;
               ownerUnitOp.Calculate(cycloneDiameter, diamCyclone);
            }

         }
         //}

         double vesselDiam = externalVesselDiameter.Value;
         if(loading != Constants.NO_VALUE && inletV != Constants.NO_VALUE &&
            densityGas != Constants.NO_VALUE && vesselDiam != Constants.NO_VALUE 
            && inletW != Constants.NO_VALUE && inletH != Constants.NO_VALUE) {

            double vesselArea = Math.PI * vesselDiam * vesselDiam/4.0;
            double vesselV = volumeFlow/vesselArea;
            vesselArea /= numOfCyclones;
            double areaRatio = inletArea/vesselArea;
            if (areaRatio > 1.0) {
               //something is wrong
            }

            double contractionFactor = CycloneUtil.CalculateDPContractionFactor(areaRatio);
            double dpInletContraction = 0.5 * densityGas * ((1.0 + contractionFactor) * inletV * inletV - vesselV * vesselV);
               
            double dpParticleAcceleration = loading * inletV * (inletV - vesselV);
               
            double diamInlet = 4.0 * inletW * inletH/(2.0*(inletW + inletH));
            double Re = diamInlet*inletV*densityGas/viscGas;
            double frictionFactor = FrictionFactorCalculator.CalculateFrictionFactor(Re);
            double dpBarrelFriction = 2.0 * frictionFactor * densityGas * inletV * inletV * Math.PI * diamCyclone * numOfTurns/diamInlet;
               
            double dpGasFlowReversal = densityGas*inletV*inletV/2.0;
               
            double barrelArea = Math.PI * diamCyclone * diamCyclone/4.0;
            double barrelV = volumeFlow/barrelArea;
            double outletV = volumeFlow/outletArea;
            areaRatio = inletArea/barrelArea;
            if (areaRatio > 1.0) {
               //something is wrong
            }
            contractionFactor = CycloneUtil.CalculateDPContractionFactor(areaRatio);
            double dpOutletContraction = 0.5 * densityGas * ((1.0 + contractionFactor) * outletV * outletV - barrelV * barrelV);

            double dpTotal = dpInletContraction + dpParticleAcceleration + dpBarrelFriction + dpGasFlowReversal + dpOutletContraction;
            double loadingCorrectionFactor = CycloneUtil.CalculateDPLoadingCorrectionFactor(loading);
            dpTotal *= loadingCorrectionFactor;
            ownerUnitOp.Calculate(owner.GasPressureDrop, dpTotal);
            //ownerUnitOp.BalancePressure(ownerUnitOp.MixtureInlet, ownerUnitOp.FluidOutlet, ownerUnitOp.PressureDrop);
         }
         
         double diamDipleg = Constants.NO_VALUE;
         double densityBulk = ParticleBulkDensity.Value;
         if(loading != Constants.NO_VALUE && volumeFlow != Constants.NO_VALUE &&
            densityGas != Constants.NO_VALUE && densityBulk != Constants.NO_VALUE) {
            temp = volumeFlow * loading * Math.Sqrt(Math.Tan(Math.PI*62.0/180))/(Math.Sqrt(Constants.G) * densityBulk * Math.Sqrt((densityBulk - densityGas)/densityBulk));
            diamDipleg = Math.Pow(temp, 0.4);
            if (diamDipleg < 0.0254 * 4.0) {
               diamDipleg = 0.0254 * 4.0;
            }
            ownerUnitOp.Calculate(diplegDiameter, diamDipleg);
         }
         double vortexLength = Constants.NO_VALUE;
         if (diamOutlet != Constants.NO_VALUE && volumeFlow != Constants.NO_VALUE) {
            //FLUIDIZATION, SOLIDS HANDLING, AND PROCESSING--Industrial Applications
            //Edited by Wen-Ching Yang, Siemens Westinghouse Power Corporation, Pittsburgh, Pennsylvania
            //Chapter 12--Cyclone Desgn, page 779
            vortexLength = volumeFlow/(Math.PI * diamOutlet * 5.0);
            ownerUnitOp.Calculate(naturalVortexLength, vortexLength);
         }
         double coneAngleValue = coneAngle.Value/2.0;
         double coneLengthValue = coneLength.Value;
         double coneLengthUnderVortex = Constants.NO_VALUE;
         if (coneAngleValue != Constants.NO_VALUE && diamDipleg != Constants.NO_VALUE) {
            if (diamCyclone != Constants.NO_VALUE) {
               coneLengthValue = Math.Tan(coneAngleValue) * (diamCyclone - diamDipleg)/2.0;
               ownerUnitOp.Calculate(coneLength, coneLengthValue);
            }

            if (diamOutlet != Constants.NO_VALUE) {
               double vortexGap = 0.0254 * 2/Math.Sin(coneAngleValue);
               coneLengthUnderVortex = ((diamOutlet - diamDipleg)/2.0 + vortexGap) * Math.Tan(coneAngleValue);
            }
         }

         if (vortexLength != Constants.NO_VALUE && coneLengthUnderVortex != Constants.NO_VALUE 
            && outletTL != Constants.NO_VALUE) {
            double totalLength = vortexLength + coneLengthUnderVortex + outletTL;
            ownerUnitOp.Calculate(barrelPlusConeLength, totalLength);
            double barrelL = totalLength - coneLengthValue;
            ownerUnitOp.Calculate(barrelLength, barrelL);
         }
      }

      internal override double CalculateCollectionEfficiencies(ArrayList particleDistributionList) {
         double totalEff = Constants.NO_VALUE;
         if (cutParticleDiameter.HasValue && owner.InletParticleLoading.HasValue) {
            totalEff = CalculateTotalCollectionEfficiency(particleDistributionList);
         }
         return totalEff;
      }

      private double CalculateTotalCollectionEfficiency(ArrayList particleDistributionList) {
         double cutDiameter = cutParticleDiameter.Value;
         double collectionEff;
         double pDiameter;
         double diamRatio;
         double totalEfficiency = 0.0;
         double loading = owner.InletParticleLoading.Value;
         if (loading == Constants.NO_VALUE) {
            loading = owner.CalculateParticleLoading(owner.GasInlet);
         }
         foreach (ParticleSizeFractionAndEfficiency psf in particleDistributionList) {
            pDiameter = psf.Diameter.Value;
            diamRatio = pDiameter/cutDiameter;
            collectionEff = CycloneUtil.CalculateCollectionEfficiency(diamRatio, inletConfiguration); 
            collectionEff = CycloneUtil.CorrectedCollectionEfficiency(collectionEff, particleType, loading);
            totalEfficiency += collectionEff * psf.WeightFraction.Value;
            //psf.ToCutDiameterRatio.Value = diamRatio;
            psf.Efficiency.Value = collectionEff;
         }

         return totalEfficiency;
      }

      private double CalculateNumOfTurns(double inletVelocity) {
         //coming from Perry's (7th edition) Fig 17.38
         return (0.4614 * inletVelocity - 0.0231783 * inletVelocity * inletVelocity + 6.05594e-4 * Math.Pow(inletVelocity, 3.0) - 5.174825e-6 * Math.Pow(inletVelocity, 4.0));
      }

      /*protected override void PostSolve(bool propagate) {
         Object obj = mixtureInlet.UpStreamOwner;
         if (obj is Dryer) {
            Dryer dryer = obj as Dryer;
            dryer.Update();
         }
         base.PostSolve(propagate);
      }*/

   
      protected CycloneRatingModel(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCycloneRatingModel", typeof(int));
         if (persistedClassVersion == 1) {
            this.owner = info.GetValue("Owner", typeof(Cyclone)) as Cyclone;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;
            this.numberOfCyclones = RecallStorableObject("NumberOfCyclones", typeof(ProcessVarInt)) as ProcessVarInt;
               
            this.particleType = (ParticleTypeGroup) info.GetValue("ParticleType", typeof(ParticleTypeGroup));
            
            //this.particleDensity = RecallStorableObject("ParticleDensity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            //this.particleBulkDensity = RecallStorableObject("ParticleBulkDensity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.cutParticleDiameter = RecallStorableObject("CutParticleDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;

            this.inletConfiguration = (CycloneInletConfiguration) info.GetValue("InletConfiguration", typeof(CycloneInletConfiguration));
            this.inletWidth = RecallStorableObject("InletWidth", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.inletHeight = RecallStorableObject("InletHeight", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.inletHeightToWidthRatio = RecallStorableObject("InletHeightToWidthRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.inletVelocity = RecallStorableObject("InletVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.cycloneDiameter = RecallStorableObject("CycloneDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletInnerDiameter = RecallStorableObject("OutletInnerDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletWallThickness = RecallStorableObject("OutletWallThickness", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            this.outletTubeLengthBelowRoof = RecallStorableObject("OutletTubeLengthBelowRoof", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletBelowRoofToInletHeightRatio = RecallStorableObject("OutletBelowRoofToInletHeightRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            this.naturalVortexLength = RecallStorableObject("NaturalVortexLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.diplegDiameter = RecallStorableObject("DiplegDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.externalVesselDiameter = RecallStorableObject("ExternalVesselDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coneAngle = RecallStorableObject("ConeAngle", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.barrelLength = RecallStorableObject("BarrelLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coneLength = RecallStorableObject("ConeLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.barrelPlusConeLength = RecallStorableObject("BarrelPlusConeLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
         
            //this.particleSizeFractionAndEfficiencyList = RecallArrayListObject("ParticleSizeFractionAndEfficiencyList");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCycloneRatingModel", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("Owner", this.owner, typeof(Cyclone));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         info.AddValue("NumberOfCyclones", this.numberOfCyclones, typeof(ProcessVarInt));

         info.AddValue("ParticleType", this.particleType, typeof(ParticleTypeGroup));
         //info.AddValue("ParticleDensity", this.particleDensity, typeof(ProcessVarDouble));
         //info.AddValue("ParticleBulkDensity", this.particleBulkDensity, typeof(ProcessVarDouble));
         info.AddValue("CutParticleDiameter", this.cutParticleDiameter, typeof(ProcessVarDouble));

         info.AddValue("InletConfiguration", this.inletConfiguration, typeof(CycloneInletConfiguration));
         info.AddValue("InletWidth", this.inletWidth, typeof(ProcessVarDouble));
         info.AddValue("InletHeight", this.inletHeight, typeof(ProcessVarDouble));
         info.AddValue("InletHeightToWidthRatio", this.inletHeightToWidthRatio, typeof(ProcessVarDouble));
         info.AddValue("InletVelocity", this.inletVelocity, typeof(ProcessVarDouble));
         info.AddValue("CycloneDiameter", this.cycloneDiameter, typeof(ProcessVarDouble));
         info.AddValue("OutletInnerDiameter", this.outletInnerDiameter, typeof(ProcessVarDouble));
         info.AddValue("OutletWallThickness", this.OutletWallThickness, typeof(ProcessVarDouble));
         info.AddValue("OutletTubeLengthBelowRoof", this.outletTubeLengthBelowRoof, typeof(ProcessVarDouble));
         info.AddValue("OutletBelowRoofToInletHeightRatio", this.outletBelowRoofToInletHeightRatio, typeof(ProcessVarDouble));
         
         info.AddValue("NaturalVortexLength", this.naturalVortexLength, typeof(ProcessVarDouble));
         info.AddValue("DiplegDiameter", this.diplegDiameter, typeof(ProcessVarDouble));
         info.AddValue("ExternalVesselDiameter", this.externalVesselDiameter, typeof(ProcessVarDouble));
         info.AddValue("ConeAngle", this.coneAngle, typeof(ProcessVarDouble));
         info.AddValue("BarrelLength", this.barrelLength, typeof(ProcessVarDouble));
         info.AddValue("ConeLength", this.coneLength, typeof(ProcessVarDouble));
         info.AddValue("BarrelPlusConeLength", this.barrelPlusConeLength, typeof(ProcessVarDouble));
         
         //info.AddValue("ParticleSizeFractionAndEfficiencyList", this.particleSizeFractionAndEfficiencyList, typeof(ArrayList));
      }                                                 
   }

   //public delegate void ParticleDistributionsChangedEventHandler(ParticleDistributions particleDistributions);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   /*public class ParticleDistributions {
      private CycloneRatingModel owner;
      //private ParticleTypeGroup particleType;
      private ProcessVarDouble totalEfficiency;
      private ArrayList sizeFractionAndEfficiencyList = new ArrayList();
      
      public event ParticleDistributionsChangedEventHandler ParticleDistributionsChanged;

      private void OnParticleDistributionsChanged() {
         if (ParticleDistributionsChanged != null) {
            ParticleDistributionsChanged(this);
         }
      }

      public ParticleDistributions(CycloneRatingModel owner) {
         this.owner = owner;
         totalEfficiency = owner.CollectionEfficiency;
         foreach (ParticleSizeFractionAndEfficiency psf in owner.ParticleSizeFractionAndEfficiencyList) {
            ParticleSizeFractionAndEfficiency aCopy = (ParticleSizeFractionAndEfficiency) psf.Clone();
            sizeFractionAndEfficiencyList.Add(aCopy);
         }
      }

      public ProcessVarDouble TotalEfficiency {
         get { return totalEfficiency; }
      }                 

      //public ParticleTypeGroup ParticleTypeGroup {
      //   get {return particleType;}
      //   set {
      //      particleType = value;
      //      owner.HasBeenModified(true);
      //   }
      //}

      public ArrayList SizeFractionAndEfficiencyList {
         get {return sizeFractionAndEfficiencyList;}
      }

      public void AddNewRow() {
         ParticleSizeFractionAndEfficiency psf = new ParticleSizeFractionAndEfficiency(owner.Owner);
         sizeFractionAndEfficiencyList.Add(psf);
         OnParticleDistributionsChanged();
      }

      public void RemoveRow(ParticleSizeFractionAndEfficiency psf) {
         if (sizeFractionAndEfficiencyList.Count > 1) {
            sizeFractionAndEfficiencyList.Remove(psf);
            OnParticleDistributionsChanged();
         }
      }

      public void RemoveRows(int startIndex, int numOfRows) {
         if (startIndex > 0 && (startIndex + numOfRows) < (sizeFractionAndEfficiencyList.Count -1)) {
            sizeFractionAndEfficiencyList.RemoveRange(startIndex, numOfRows);
         }
      }

      public void RemoveAt(int index) {
         if (sizeFractionAndEfficiencyList.Count > 1) {
            if (index > 0 && index < sizeFractionAndEfficiencyList.Count) {
               ParticleSizeFractionAndEfficiency psf = (ParticleSizeFractionAndEfficiency) sizeFractionAndEfficiencyList[index];
               sizeFractionAndEfficiencyList.RemoveAt(index);
               OnParticleDistributionsChanged();
            }
         }
      }

      public void Normalize() {
         DoNormalization();
         double totalEff = owner.CalculateCollectionEfficiencies(sizeFractionAndEfficiencyList);
         totalEfficiency.Value = totalEff;
         OnParticleDistributionsChanged();
      }

      public void FinishSpecifications() {
         owner.ParticleSizeFractionAndEfficiencyList = sizeFractionAndEfficiencyList;
         DoNormalization();
         owner.Owner.HasBeenModified(true);
      }
      
      private void DoNormalization() {
         double total = 0.0;
         foreach(ParticleSizeFractionAndEfficiency psf in sizeFractionAndEfficiencyList) {
            total += psf.WeightFraction.Value;
         }
         
         if (total > 1.0e-8) {
            foreach(ParticleSizeFractionAndEfficiency psf in sizeFractionAndEfficiencyList) {
               psf.WeightFraction.Value /= total;
            }
         }
      }
   }*/
   
   /*[Serializable]
   public class ParticleSizeAndFraction : Storable, ICloneable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ProcessVarDouble diameter;
      private ProcessVarDouble weightFraction;
      private ProcessVarDouble toCutDiameterRatio;
      private ProcessVarDouble efficiency;

      public ParticleSizeAndFraction(Solvable owner) {
         //diameter = 20;
         //fraction = 1.0;
         //efficiency = Constants.NO_VALUE;
         diameter = new ProcessVarDouble(StringConstants.DIAMETER, PhysicalQuantity.MicroLength, 2.0e-5, VarState.Specified, owner);
         weightFraction = new ProcessVarDouble(StringConstants.WEIGHT_FRACTION, PhysicalQuantity.Fraction, 1.0, VarState.Specified, owner);
         toCutDiameterRatio = new ProcessVarDouble(StringConstants.FRACTION, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         efficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, owner);
      }

      public ProcessVarDouble Diameter {
         get {return diameter;}
         set {diameter = value;}
      }

      public ProcessVarDouble WeightFraction {
         get {return weightFraction;}
         set {weightFraction = value;}
      }

      public ProcessVarDouble ToCutDiameterRatio {
         get {return toCutDiameterRatio;}
         set {toCutDiameterRatio = value;}
      }
      
      public ProcessVarDouble Efficiency {
         get {return efficiency;}
         set {efficiency = value;}
      }
      
      public Object Clone() {
         ParticleSizeAndFraction psaf = (ParticleSizeAndFraction) this.MemberwiseClone();
         psaf.Diameter = diameter.Clone() as ProcessVarDouble;
         psaf.WeightFraction = weightFraction.Clone() as ProcessVarDouble;
         psaf.Efficiency = efficiency.Clone() as ProcessVarDouble;
         psaf.ToCutDiameterRatio = ToCutDiameterRatio.Clone() as ProcessVarDouble;
         return psaf;
      }

      protected ParticleSizeAndFraction (SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionParticleSizeAndFraction", typeof(int));
         if (persistedClassVersion == 1) {
            this.diameter = RecallStorableObject("Diameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.weightFraction = RecallStorableObject("WeightFraction", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.toCutDiameterRatio = RecallStorableObject("ToCutDiameterRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.efficiency = RecallStorableObject("Efficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionParticleSizeAndFraction", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Diameter", this.diameter, typeof(ProcessVarDouble));
         info.AddValue("WeightFraction", this.weightFraction, typeof(ProcessVarDouble));
         info.AddValue("ToCutDiameterRatio", this.toCutDiameterRatio, typeof(ProcessVarDouble));
         info.AddValue("Efficiency", this.efficiency, typeof(ProcessVarDouble));
      }

   }*/
}
