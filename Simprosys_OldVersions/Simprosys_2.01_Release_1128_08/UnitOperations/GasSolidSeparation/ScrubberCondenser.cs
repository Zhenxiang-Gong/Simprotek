using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.ThermalProperties;
using Prosimo.Materials;

namespace Prosimo.UnitOperations.GasSolidSeparation {

   //public enum ScrubberType {Condensing = 0, General};

   /// <summary>
   /// Summary description for WetScrubber.
   /// </summary>
   [Serializable]
   public class ScrubberCondenser : UnitOperation, IGasSolidSeparator {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static int GAS_INLET_INDEX = 0;
      public static int GAS_OUTLET_INDEX = 1;
      public static int LIQUID_OUTLET_INDEX = 2;

      protected ProcessStreamBase gasInlet;
      protected ProcessStreamBase gasOutlet;
      protected ProcessStreamBase liquidOutlet;

      private GasSolidSeparatorBalanceModel balanceModel;

      private ProcessVarDouble coolingDuty;
      private ProcessVarDouble liquidToGasVolumeRatio;
      private ProcessVarDouble liquidRecirculationMassFlowRate;
      private ProcessVarDouble liquidRecirculationVolumeFlowRate;


      #region public properties

      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasInlet {
         get { return gasInlet; }
      }

      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasOutlet {
         get { return gasOutlet; }
      }

      public ProcessStreamBase LiquidOutlet {
         get { return liquidOutlet; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble GasPressureDrop {
         get { return balanceModel.GasPressureDrop; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble CollectionEfficiency {
         get { return balanceModel.CollectionEfficiency; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble InletParticleLoading {
         get { return balanceModel.InletParticleLoading; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble OutletParticleLoading {
         get { return balanceModel.OutletParticleLoading; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble ParticleCollectionRate {
         get { return balanceModel.ParticleCollectionRate; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet {
         get { return balanceModel.MassFlowRateOfParticleLostToGasOutlet; }
      }

      //implement interface IGasSolidSeparator
      public UnitOperation MyUnitOperation {
         get { return this; }
      }

      public ProcessVarDouble CoolingDuty {
         get { return coolingDuty; }
      }

      public ProcessVarDouble LiquidToGasRatio {
         get { return liquidToGasVolumeRatio; }
      }

      public ProcessVarDouble LiquidRecirculationMassFlowRate {
         get { return liquidRecirculationMassFlowRate; }
      }

      public ProcessVarDouble LiquidRecirculationVolumeFlowRate {
         get { return liquidRecirculationVolumeFlowRate; }
      }

      #endregion

      public ScrubberCondenser(string name, UnitOperationSystem uoSys)
         : base(name, uoSys) {
         balanceModel = new GasSolidSeparatorBalanceModel(this);

         coolingDuty = new ProcessVarDouble(StringConstants.COOLING_DUTY, PhysicalQuantity.Power, VarState.Specified, this);
         liquidToGasVolumeRatio = new ProcessVarDouble(StringConstants.LIQUID_GAS_RATIO, PhysicalQuantity.Unknown, VarState.Specified, this);
         liquidRecirculationMassFlowRate = new ProcessVarDouble(StringConstants.LIQUID_RECIRCULATION_MASS_FLOW_RATE, PhysicalQuantity.MassFlowRate, VarState.Specified, this);
         liquidRecirculationVolumeFlowRate = new ProcessVarDouble(StringConstants.LIQUID_RECIRCULATION_VOLUME_FLOW_RATE, PhysicalQuantity.VolumeFlowRate, VarState.Specified, this);

         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(coolingDuty);
         AddVarOnListAndRegisterInSystem(liquidToGasVolumeRatio);
         AddVarOnListAndRegisterInSystem(liquidRecirculationMassFlowRate);
         AddVarOnListAndRegisterInSystem(liquidRecirculationVolumeFlowRate);
      }

      public override bool CanAttach(int streamIndex) {
         bool retValue = false;
         if (streamIndex == GAS_INLET_INDEX && gasInlet == null) {
            retValue = true;
         }
         else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX && liquidOutlet == null) {
            retValue = true;
         }

         return retValue;
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if ((streamIndex == GAS_INLET_INDEX && ps.DownStreamOwner != null)
            || ((streamIndex == GAS_OUTLET_INDEX || streamIndex == LIQUID_OUTLET_INDEX) && ps.UpStreamOwner != null)) {
            return false;
         }

         bool canAttach = false;
         if (ps is DryingGasStream && (streamIndex == GAS_INLET_INDEX || streamIndex == GAS_OUTLET_INDEX)) {
            if (streamIndex == GAS_INLET_INDEX && gasInlet == null) {
               canAttach = true;
            }
            else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) {
               canAttach = true;
            }
         }
         else if (ps is DryingMaterialStream && streamIndex == LIQUID_OUTLET_INDEX) {
            canAttach = true;
         }

         return canAttach;
      }

      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == GAS_INLET_INDEX) {
            gasInlet = ps as DryingGasStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == GAS_OUTLET_INDEX) {
            gasOutlet = ps as DryingGasStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX) {
            liquidOutlet = ps as DryingMaterialStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else {
            attached = false;
         }
         return attached;
      }

      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = true;
         if (ps == gasInlet) {
            gasInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == gasOutlet) {
            gasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == liquidOutlet) {
            liquidOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else {
            detached = false;
         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }

      //implement interface IGasSolidSeparator
      public double CalculateParticleLoading(ProcessStreamBase psb) {
         DryingGasStream stream = psb as DryingGasStream;
         return balanceModel.CalculateParticleLoading(stream);
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (retValue == null) {
            if (calculationType == UnitOpCalculationType.Balance) {
               retValue = balanceModel.CheckSpecifiedValueRange(pv, aValue);
            }
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == gasInlet.Pressure && gasOutlet.Pressure.IsSpecifiedAndHasValue && aValue < gasOutlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the scrubber/condenser's gas inlet must be greater than that of the outlet.");
         }
         else if (pv == gasOutlet.Pressure && gasInlet.Pressure.IsSpecifiedAndHasValue && aValue > gasInlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the scrubber/condenser's gas outlet cannot be greater than that of the inlet.");
         }

         return retValue;
      }
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (gasInlet == null || gasOutlet == null || liquidOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      //protected override bool IsSolveReady() {
      //   return true;
      //}

      public override void Execute(bool propagate) {
         PreSolve();
         //balance pressure
         BalancePressure(gasInlet, gasOutlet, GasPressureDrop);
         //balance gas stream flow
         if (gasInlet is DryingGasStream) {
            DryingGasStream inlet = gasInlet as DryingGasStream;
            DryingGasStream outlet = gasOutlet as DryingGasStream;
            BalanceDryingGasStreamFlow(inlet, outlet);
         }

         UpdateStreamsIfNecessary();

         if (IsSolveReady()) {
            Solve();
         }

         AdjustVarsStates(gasInlet as DryingGasStream, gasOutlet as DryingGasStream);
         //AdjustVarsStates(liquidInlet as DryingMaterialStream, liquidOutlet as DryingMaterialStream);

         PostSolve();
      }

      private void Solve() {
         //Mass Transfer--gas moisture and material particles transfer from gas stream to liquid stream
         DryingMaterialStream dmsOutlet = liquidOutlet as DryingMaterialStream;

         DryingGasStream dgsInlet = gasInlet as DryingGasStream;
         DryingGasStream dgsOutlet = gasOutlet as DryingGasStream;

         //if (dgsInlet.DewPoint.HasValue && dgsOutlet.Temperature.HasValue && dgsOutlet.Temperature.Value > dgsInlet.DewPoint.Value) {
         //   throw new InappropriateSpecifiedValueException("Gas outlet temperature is not low enough to reach satuation");
         //}

         Calculate(dgsOutlet.RelativeHumidity, 0.9999999);

         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         if (dgsOutlet.Temperature.HasValue || dgsOutlet.WetBulbTemperature.HasValue) {
            UpdateStreamsIfNecessary();
         }

         balanceModel.DoBalanceCalculation();

         double inletDustMassFlowRate = Constants.NO_VALUE;
         double outletDustMassFlowRate = Constants.NO_VALUE;
         double inletDustMoistureFraction = 0.0;
         double outletDustMoistureFraction = 0.0;

         DryingGasComponents dgc;
         if (InletParticleLoading.HasValue && gasInlet.VolumeFlowRate.HasValue) {
            inletDustMassFlowRate = InletParticleLoading.Value * gasInlet.VolumeFlowRate.Value;
            dgc = dgsInlet.GasComponents;
            if (dgc.SolidPhase != null) {
               SolidPhase sp = dgc.SolidPhase;
               MaterialComponent mc = sp[1];
               inletDustMoistureFraction = mc.GetMassFractionValue();
            }
         }

         if (OutletParticleLoading.HasValue && gasOutlet.VolumeFlowRate.HasValue) {
            outletDustMassFlowRate = OutletParticleLoading.Value * gasOutlet.VolumeFlowRate.Value;
            dgc = dgsOutlet.GasComponents;
            if (dgc.SolidPhase != null) {
               SolidPhase sp = dgc.SolidPhase;
               MaterialComponent mc = sp[1];
               inletDustMoistureFraction = mc.GetMassFractionValue();
            }
         }

         double materialFromGas = ParticleCollectionRate.Value;
         if (inletDustMassFlowRate != Constants.NO_VALUE && materialFromGas != Constants.NO_VALUE && outletDustMassFlowRate == Constants.NO_VALUE) {
            outletDustMassFlowRate = inletDustMassFlowRate - materialFromGas;
         }

         MoistureProperties moistureProperties = (this.unitOpSystem as EvaporationAndDryingSystem).MoistureProperties;
         double materialEnthalpyLoss;
         double gasEnthalpyLoss;
         double gatTempValue;
         double matTempValue;
         double liquidCp;
         double specificHeatOfSolidPhase;
         double totalEnthapyLoss;

         if (dmsOutlet.Temperature.HasValue && dgsInlet.SpecificEnthalpyDryBase.HasValue && coolingDuty.HasValue
            && dgsInlet.MassFlowRateDryBase.HasValue && inletDustMoistureFraction != Constants.NO_VALUE
            && materialFromGas != Constants.NO_VALUE) {
            
            gatTempValue = gasInlet.Temperature.Value;
            matTempValue = dmsOutlet.Temperature.Value;

            liquidCp = moistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(gatTempValue, matTempValue));
            specificHeatOfSolidPhase = (1.0 - inletDustMoistureFraction) * dmsOutlet.GetCpOfAbsoluteDryMaterial() + inletDustMoistureFraction * liquidCp;
            materialEnthalpyLoss = materialFromGas * specificHeatOfSolidPhase * (matTempValue - gatTempValue);
            gasEnthalpyLoss = coolingDuty.Value - materialEnthalpyLoss;
            //double outletEnthalpy = gasInlet.SpecificEnthalpy.Value - gasEnthalpyLoss;
            //Calculate(gasOutlet.SpecificEnthalpy, outletEnthalpy);
            double outletEnthalpy = dgsInlet.SpecificEnthalpyDryBase.Value - gasEnthalpyLoss / dgsInlet.MassFlowRateDryBase.Value;
            Calculate(dgsOutlet.SpecificEnthalpyDryBase, outletEnthalpy);
            UpdateStreamsIfNecessary();
            if (dgsOutlet.VolumeFlowRate.HasValue) {
               double outletLoading = outletDustMassFlowRate / dgsOutlet.VolumeFlowRate.Value;
               Calculate(OutletParticleLoading, outletLoading);
            }
         }
         else if (dmsOutlet.Temperature.HasValue && dgsInlet.SpecificEnthalpyDryBase.HasValue && dgsOutlet.Temperature.HasValue
            && dgsInlet.MassFlowRateDryBase.HasValue && dgsOutlet.SpecificEnthalpyDryBase.HasValue && inletDustMoistureFraction != Constants.NO_VALUE) {
            gatTempValue = dgsInlet.Temperature.Value;
            matTempValue = dmsOutlet.Temperature.Value;

            liquidCp = moistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(gatTempValue, matTempValue));
            specificHeatOfSolidPhase = (1.0 - inletDustMoistureFraction) * dmsOutlet.GetCpOfAbsoluteDryMaterial() + inletDustMoistureFraction * liquidCp;
            materialEnthalpyLoss = materialFromGas * specificHeatOfSolidPhase * (matTempValue - gatTempValue);
            gasEnthalpyLoss = dgsInlet.MassFlowRateDryBase.Value * (dgsInlet.SpecificEnthalpyDryBase.Value - dgsOutlet.SpecificEnthalpyDryBase.Value);
            totalEnthapyLoss = materialEnthalpyLoss + gasEnthalpyLoss;
            Calculate(coolingDuty, totalEnthapyLoss);
         }
         //else if (gasInlet.SpecificEnthalpy.HasValue && gasOutlet.SpecificEnthalpy.HasValue && coolingDuty.HasValue 
         //   && gasInlet.MassFlowRate.HasValue && inletDustMoistureFraction != Constants.NO_VALUE) {
            
         //   gasEnthalpyLoss = gasInlet.SpecificEnthalpy.Value * gasInlet.MassFlowRate.Value - gasOutlet.SpecificEnthalpy.Value * gasOutlet.MassFlowRate.Value;
         //   materialEnthalpyLoss = coolingDuty.Value - gasEnthalpyLoss;
         //   gatTempValue = gasInlet.Temperature.Value;
         //   //double matTempValue = dmsOutlet.Temperature.Value;

         //   liquidCp = moistureProperties.GetSpecificHeatOfLiquid(gatTempValue);
         //   specificHeatOfSolidPhase = (1.0 - inletDustMoistureFraction) * dmsOutlet.GetCpOfAbsoluteDryMaterial() + inletDustMoistureFraction * liquidCp;
         //   matTempValue = gatTempValue + materialEnthalpyLoss / (materialFromGas * specificHeatOfSolidPhase);

         //   Calculate(liquidOutlet.Temperature, matTempValue);
         //}

         double inletMoistureFlowRate = Constants.NO_VALUE;
         double outletMoistureFlowRate = Constants.NO_VALUE; 
         if (dgsInlet.MassFlowRateDryBase.HasValue && dgsInlet.MoistureContentDryBase.HasValue) {
            inletMoistureFlowRate = dgsInlet.MassFlowRateDryBase.Value * dgsInlet.MoistureContentDryBase.Value;
         }

         if (dgsOutlet.MassFlowRateDryBase.HasValue && dgsOutlet.MoistureContentDryBase.HasValue) {
            outletMoistureFlowRate = dgsOutlet.MassFlowRateDryBase.Value * dgsOutlet.MoistureContentDryBase.Value;
         }

         if (materialFromGas != Constants.NO_VALUE &&
             inletMoistureFlowRate != Constants.NO_VALUE && outletMoistureFlowRate != Constants.NO_VALUE) {

            double moistureFromGas = inletMoistureFlowRate - outletMoistureFlowRate;
           // materialFromGas = inletDustMassFlowRate - outletDustMassFlowRate;
            double moistureOfMaterialFromGas = inletDustMassFlowRate * inletDustMoistureFraction - outletDustMassFlowRate * outletDustMoistureFraction;

            double outletMassFlowRate = materialFromGas + moistureFromGas;
            Calculate(dmsOutlet.MassFlowRate, outletMassFlowRate);

            double outletMaterialMoistureFlowRate = moistureFromGas + moistureOfMaterialFromGas;
            double outletMoistureContentWetBase = outletMaterialMoistureFlowRate / outletMassFlowRate;
            Calculate(dmsOutlet.MoistureContentWetBase, outletMoistureContentWetBase);
            //solveState = SolveState.Solved;
         }

         if (liquidToGasVolumeRatio.HasValue && gasInlet.VolumeFlowRate.HasValue) {
            double recirculationVolumeFlow = liquidToGasVolumeRatio.Value * gasInlet.VolumeFlowRate.Value;
            Calculate(liquidRecirculationVolumeFlowRate, recirculationVolumeFlow);
            if (liquidOutlet.Density.HasValue) {
               double recirculationMassFlow = recirculationVolumeFlow/liquidOutlet.Density.Value;
               Calculate(liquidRecirculationMassFlowRate, recirculationMassFlow);
            }
         }

         if (dgsInlet.DewPoint.HasValue && dgsOutlet.Temperature.HasValue && dgsOutlet.Temperature.Value > dgsInlet.DewPoint.Value) {
            solveState = SolveState.SolvedWithWarning;
         }
         else if (gasInlet.Pressure.HasValue && gasOutlet.Pressure.HasValue
            && gasInlet.Temperature.HasValue && gasOutlet.Temperature.HasValue
            && gasInlet.SpecificEnthalpy.HasValue && gasOutlet.SpecificEnthalpy.HasValue
            && liquidOutlet.Pressure.HasValue && liquidOutlet.Temperature.HasValue
            && dmsOutlet.MoistureContentWetBase.HasValue) {
            solveState = SolveState.Solved;
         }

      }

      protected ScrubberCondenser(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionScrubberCondenser", typeof(int));
         if (persistedClassVersion == 1) {
            this.gasInlet = info.GetValue("GasInlet", typeof(DryingGasStream)) as ProcessStreamBase;
            this.gasOutlet = info.GetValue("GasOutlet", typeof(DryingGasStream)) as ProcessStreamBase;
            this.liquidOutlet = info.GetValue("LiquidOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;

            this.balanceModel = RecallStorableObject("BalanceModel", typeof(GasSolidSeparatorBalanceModel)) as GasSolidSeparatorBalanceModel;

            this.coolingDuty = RecallStorableObject("CoolingDuty", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.liquidToGasVolumeRatio = (ProcessVarDouble)RecallStorableObject("LiquidToGasVolumeRatio", typeof(ProcessVarDouble));
            this.liquidRecirculationVolumeFlowRate = RecallStorableObject("LiquidRecirculationVolumeFlowRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.liquidRecirculationMassFlowRate = RecallStorableObject("LiquidRecirculationMassFlowRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionScrubberCondenser", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("GasInlet", this.gasInlet, typeof(ProcessStreamBase));
         info.AddValue("GasOutlet", this.gasOutlet, typeof(ProcessStreamBase));
         info.AddValue("LiquidOutlet", this.liquidOutlet, typeof(ProcessStreamBase));

         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));

         info.AddValue("CoolingDuty", this.coolingDuty, typeof(ProcessVarDouble));
         info.AddValue("LiquidToGasVolumeRatio", this.liquidToGasVolumeRatio, typeof(ProcessVarDouble));
         info.AddValue("LiquidRecirculationVolumeFlowRate", this.liquidRecirculationVolumeFlowRate, typeof(ProcessVarDouble));
         info.AddValue("LiquidRecirculationMassFlowRate", this.liquidRecirculationMassFlowRate, typeof(ProcessVarDouble));
      }
   }
}

