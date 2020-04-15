using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {

   [Serializable] 
   public class Cyclone : UnitOperation, IGasSolidSeparator {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public static int GAS_INLET_INDEX = 1;
      public static int GAS_OUTLET_INDEX = 2;
      public static int PARTICLE_OUTLET_INDEX = 3;

      private ProcessStreamBase gasInlet;
      private ProcessStreamBase gasOutlet;
      private ProcessStreamBase particleOutlet;
      
      /*private ProcessVarDouble pressureDrop;
      private ProcessVarDouble collectionEfficiency;
      private ProcessVarDouble inletParticleLoading;
      private ProcessVarDouble outletParticleLoading;
      private ProcessVarDouble massFlowRateOfParticleLostToGasOutlet;*/

      private GasSolidSeparatorBalanceModel balanceModel;

      private CycloneRatingModel currentRatingModel;

      #region public properties
      //streams
      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasInlet {
         get { return gasInlet; }
      }
      
      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasOutlet {
         get { return gasOutlet; }
      }

      public ProcessStreamBase ParticleOutlet {
         get { return particleOutlet; }
      }
      
      //ProcessVariables
      /*public ProcessVarDouble PressureDrop {
         get { return pressureDrop; }
      }

      public ProcessVarDouble CollectionEfficiency {
         get { return collectionEfficiency; }
      }

      public ProcessVarDouble InletParticleLoading {
         get { return inletParticleLoading; }
      }
      
      public ProcessVarDouble OutletParticleLoading {
         get { return outletParticleLoading; }
      }
      
      public ProcessVarDouble ParticleCollectionRate {
         get { return particleCollectionRate; }
      }
      
      public ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet {
         get { return massFlowRateOfParticleLostToGasOutlet; }
      }*/
      
      //interface IGasSolidSeparator--begin
      public ProcessVarDouble GasPressureDrop {
         get { return balanceModel.GasPressureDrop; }
      }

      public ProcessVarDouble CollectionEfficiency {
         get { return balanceModel.CollectionEfficiency; }
      }

      public ProcessVarDouble InletParticleLoading {
         get { return balanceModel.InletParticleLoading; }
      }
      
      public ProcessVarDouble OutletParticleLoading {
         get { return balanceModel.OutletParticleLoading; }
      }
      
      public ProcessVarDouble ParticleCollectionRate {
         get { return balanceModel.ParticleCollectionRate; }
      }
      
      public ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet {
         get { return balanceModel.MassFlowRateOfParticleLostToGasOutlet; }
      }
      
      public UnitOperation MyUnitOperation {
         get { return this as UnitOperation; }
      }
      //interface IGasSolidSeparator--end

      public CycloneRatingModel CurrentRatingModel {
         get {return currentRatingModel;}
      }

      #endregion
      
      public Cyclone(string name, UnitOpSystem uoSys) : base(name, uoSys) {
         /*pressureDrop = new ProcessVarDouble(StringConstants.PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         collectionEfficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.Specified, this);
         inletParticleLoading = new ProcessVarDouble(StringConstants.INLET_PARTICLE_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, this);
         outletParticleLoading = new ProcessVarDouble(StringConstants.OUTLET_PARTICLE_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, this);
         massFlowRateOfParticleLostToGasOutlet = new ProcessVarDouble(StringConstants.PARTICLE_LOSS_TO_GAS_OUTLET, PhysicalQuantity.MassFlowRate, VarState.Specified, this);
         InitializeVarListAndRegisterVars();*/

         balanceModel = new GasSolidSeparatorBalanceModel(this);
      }

      /*protected override void InitializeVarListAndRegisterVars() {
         base.InitializeVarListAndRegisterVars();

         AddVarOnListAndRegisterInSystem(pressureDrop);
         AddVarOnListAndRegisterInSystem(collectionEfficiency);
         AddVarOnListAndRegisterInSystem(inletParticleLoading);
         AddVarOnListAndRegisterInSystem(outletParticleLoading);
         AddVarOnListAndRegisterInSystem(massFlowRateOfParticleLostToGasOutlet);
      }*/
      
      public override bool CanConnect(int streamIndex) {
         bool retValue = false;
         if (streamIndex == GAS_INLET_INDEX && gasInlet == null) {
            retValue = true;
         }
         else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == PARTICLE_OUTLET_INDEX) {
            retValue = true;
         }

         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (streamIndex == GAS_INLET_INDEX && ps.DownStreamOwner != null
            || ((streamIndex == GAS_OUTLET_INDEX || streamIndex == PARTICLE_OUTLET_INDEX) && ps.UpStreamOwner != null)) {
            return false;
         }
         bool canAttach = false;
         if (streamIndex == GAS_INLET_INDEX && gasInlet == null) {
            if (ps is DryingGasStream || ps is ProcessStream) {
               if (gasOutlet == null || (gasOutlet != null && ps.GetType() == gasOutlet.GetType())) {
                  canAttach = true;
               }
            }
         }
         else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) {
            if (ps is DryingGasStream || ps is ProcessStream) {
               if (gasInlet == null || (gasInlet != null && ps.GetType() == gasInlet.GetType())) {
                  canAttach = true;
               }
            }
         }
         else if (streamIndex == PARTICLE_OUTLET_INDEX && particleOutlet == null) {
            if (gasInlet != null && gasInlet is DryingGasStream && ps is DryingMaterialStream) {
               canAttach = true;
            }
            else if (gasOutlet != null && gasOutlet is DryingGasStream && ps is DryingMaterialStream) {
               canAttach = true;
            }
            else if (gasInlet == null && gasOutlet == null && (ps is DryingMaterialStream || ps is ProcessStream)) {
               canAttach = true;
            }
            else if (gasInlet != null && gasInlet is ProcessStream && ps is ProcessStream) {
               canAttach = true;
            }
            else if (gasOutlet != null && gasOutlet is ProcessStream && ps is ProcessStream) {
               canAttach = true;
            }
         }

         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == GAS_INLET_INDEX) {
            gasInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == GAS_OUTLET_INDEX) {
            gasOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == PARTICLE_OUTLET_INDEX) {
            particleOutlet = ps;
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
         if (ps == gasInlet) 
         {
            inletStreams.Remove(ps);
            gasInlet = null;
            ps.DownStreamOwner = null;
         }
         else if (ps == gasOutlet) 
         {
            outletStreams.Remove(ps);
            gasOutlet = null;
            ps.UpStreamOwner = null;
         }
         
         else if (ps == particleOutlet) 
         {
            outletStreams.Remove(ps);
            particleOutlet = null;
            ps.UpStreamOwner = null;
         }
         else 
         {
            detached = false;
         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      protected override void EnableRatingModel() {
         //in cyclone rating collection efficiency can not be specified
         CollectionEfficiency.State = VarState.AlwaysCalculated;
         
         if (currentRatingModel == null) {
            currentRatingModel = new CycloneRatingModel(this);
         }
      }
      
      protected override void EnableBalanceModel() {
         CollectionEfficiency.State = VarState.Specified;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (retValue == null && calculationType == UnitOpCalculationType.Rating) 
         {
            retValue = currentRatingModel.CheckSpecifiedValueRange(pv, aValue);
         }
         
         return retValue;
      }

      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (gasInlet == null || gasOutlet == null || particleOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         
         //BalanceSpecificEnthalpy(gasInlet, gasOutlet);
         BalanceAdiabaticProcess(gasInlet, gasOutlet);

         //dry gas flow balance
         if (gasInlet is DryingGasStream) 
         {
            DryingGasStream inlet = gasInlet as DryingGasStream;
            DryingGasStream outlet = gasOutlet as DryingGasStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(inlet, outlet);
            BalanceDryingGasStreamFlow(inlet, outlet);
            AdjustVarsStates(inlet, outlet);
         }
         else if (gasInlet is ProcessStream) {
            BalanceProcessStreamFlow(gasInlet, gasOutlet);
         }

         if (calculationType == UnitOpCalculationType.Balance) {
            //balance presssure
            BalancePressure(gasInlet, gasOutlet, GasPressureDrop);
            UpdateStreamsIfNecessary();

            balanceModel.DoBalanceCalculation();
            if (solveState == SolveState.PartiallySolved && GasPressureDrop.HasValue
               && gasInlet.Pressure.HasValue && gasOutlet.Pressure.HasValue) {
               solveState = SolveState.Solved;
            }
            if (solveState == SolveState.Solved) {
               Calculate(particleOutlet.MassFlowRate, ParticleCollectionRate.Value);
               balanceModel.PostBalanceCalculation();
            }
         }
         else if (calculationType == UnitOpCalculationType.Rating) {
            if (currentRatingModel.IsRatingCalcReady()) {
               currentRatingModel.DoRatingCalculation();
               //balance presssure
               BalancePressure(gasInlet, gasOutlet, GasPressureDrop);
               if (solveState == SolveState.PartiallySolved && GasPressureDrop.HasValue
                  && gasInlet.Pressure.HasValue && gasOutlet.Pressure.HasValue) {
                  solveState = SolveState.Solved;
               }
               if (HasSolvedAlready) {
                  UpdateStreamsIfNecessary();
                  balanceModel.DoBalanceCalculation();
               }
               balanceModel.PostBalanceCalculation();
            }
         }

         PostSolve();
      }

      //implement interface IGasSolidSeparator
      public double CalculateParticleLoading(ProcessStreamBase psb) {
         DryingGasStream stream = psb as DryingGasStream;
         /*DryingGasStreamComponents dgsc = stream.DryingGasStreamComponents;
         SolidPhase sp = dgsc.SolidPhase;
         double massFlow = stream.MassFlowRate.Value;
         double volumeFlow = psb.VolumeFlowRate.Value;
         double loading = Constants.NO_VALUE;
         if (massFlow != Constants.NO_VALUE && volumeFlow != Constants.NO_VALUE) {
            double massFlowOfParticle = sp.MassFraction * massFlow;
            loading = massFlowOfParticle/volumeFlow;
         }
         return loading;*/
         return balanceModel.CalculateParticleLoading(psb);
      }

      protected Cyclone(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCyclone", typeof(int));
         if (persistedClassVersion == 1) {
            this.gasInlet = info.GetValue("GasInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.gasOutlet = info.GetValue("GasOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.particleOutlet = info.GetValue("ParticleOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;

            /*this.pressureDrop = RecallStorableObject("PressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.collectionEfficiency = RecallStorableObject("CollectionEfficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.massFlowRateOfParticleLostToGasOutlet = RecallStorableObject("MassFlowRateOfParticleLostToGasOutlet", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.inletParticleLoading = RecallStorableObject("InletParticleLoading", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletParticleLoading = RecallStorableObject("OutletParticleLoading", typeof(ProcessVarDouble)) as ProcessVarDouble;*/
            
            this.balanceModel = RecallStorableObject("BalanceModel", typeof(GasSolidSeparatorBalanceModel)) as GasSolidSeparatorBalanceModel;
            this.currentRatingModel = RecallStorableObject("CurrentRatingModel", typeof(CycloneRatingModel)) as CycloneRatingModel;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionCyclone", Cyclone.CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("GasInlet", this.gasInlet, typeof(ProcessStreamBase));
         info.AddValue("GasOutlet", this.gasOutlet, typeof(ProcessStreamBase));
         info.AddValue("ParticleOutlet", this.particleOutlet, typeof(ProcessStreamBase));
         
         /*info.AddValue("PressureDrop", this.pressureDrop, typeof(ProcessVarDouble));
         info.AddValue("CollectionEfficiency", this.collectionEfficiency, typeof(ProcessVarDouble));
         info.AddValue("InletParticleLoading", this.inletParticleLoading, typeof(ProcessVarDouble));
         info.AddValue("OutletParticleLoading", this.outletParticleLoading, typeof(ProcessVarDouble));
         info.AddValue("MassFlowRateOfParticleLostToGasOutlet", this.massFlowRateOfParticleLostToGasOutlet, typeof(ProcessVarDouble));*/
         
         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));
         info.AddValue("CurrentRatingModel", this.currentRatingModel, typeof(CycloneRatingModel));
      }
   }
}

      /*private void DoBalanceCalculation() {
         double inletVolumeFlow = gasInlet.VolumeFlowRate.Value;
         double outletVolumeFlow = gasOutlet.VolumeFlowRate.Value;
         double inletLoading = inletParticleLoading.Value;
         double outletLoading = outletParticleLoading.Value;
         double collectionRate = particleOutlet.MassFlowRate.Value;
         double lossRate = massFlowRateOfParticleLostToGasOutlet.Value;
         double efficiency = collectionEfficiency.Value;

         DryingGasStream inlet = gasInlet as DryingGasStream;
         //double massFlowOfInletParticle = Constants.NO_VALUE; 
         double inletMassFlow = inlet.MassFlowRate.Value;
         
         if (inletMassFlow == Constants.NO_VALUE) {
            double inletMoistureContent = inlet.Humidity.Value;
            double wg = inlet.MassFlowRateDryBase.Value;
            if (inletMoistureContent != Constants.NO_VALUE && wg != Constants.NO_VALUE) {
               inletMassFlow = wg * (1.0 + inletMoistureContent);
            }
         }

         DryingGasStreamComponents dgsc = inlet.DryingGasStreamComponents;
         SolidPhase sp = dgsc.SolidPhase;
         if (inletVolumeFlow != Constants.NO_VALUE && outletVolumeFlow != Constants.NO_VALUE) {
            if (sp != null && inletMassFlow != Constants.NO_VALUE) {
               inletLoading = CalculateParticleLoading(gasInlet);
               Calculate(inletParticleLoading, inletLoading);
               solveState = SolveState.PartiallySolved;
            }
            
            if (inletLoading != Constants.NO_VALUE && efficiency != Constants.NO_VALUE) {
               collectionRate = inletLoading * inletVolumeFlow * efficiency;
               Calculate(particleOutlet.MassFlowRate, collectionRate);
               
               lossRate = inletLoading * inletVolumeFlow * (1.0 - efficiency);
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);

               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               solveState = SolveState.PartiallySolved;
            }
            else if (inletLoading != Constants.NO_VALUE && outletLoading != Constants.NO_VALUE) {
               collectionRate = inletLoading * inletVolumeFlow - outletLoading * outletVolumeFlow;
               Calculate(particleOutlet.MassFlowRate, collectionRate);
               
               efficiency = collectionRate/(inletLoading * inletVolumeFlow);
               Calculate(collectionEfficiency, efficiency);

               lossRate = outletLoading * outletVolumeFlow;
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);
               
               solveState = SolveState.PartiallySolved;
            }
            else if (outletLoading != Constants.NO_VALUE && efficiency != Constants.NO_VALUE) {
               lossRate = outletLoading * outletVolumeFlow;
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);

               inletLoading = lossRate/(inletVolumeFlow * (1.0 - efficiency));
               Calculate(inletParticleLoading, inletLoading);

               collectionRate = inletLoading * inletVolumeFlow * efficiency;
               Calculate(particleOutlet.MassFlowRate, collectionRate);
               
               solveState = SolveState.PartiallySolved;
            }
            else if (lossRate != Constants.NO_VALUE && collectionRate != Constants.NO_VALUE) {
               inletLoading = (lossRate + collectionRate)/inletVolumeFlow;
               Calculate(inletParticleLoading, inletLoading);
               
               efficiency = collectionRate/(lossRate + collectionRate);
               Calculate(collectionEfficiency, efficiency);

               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               solveState = SolveState.PartiallySolved;
            }
            else if (lossRate != Constants.NO_VALUE && efficiency != Constants.NO_VALUE && efficiency < 1.0) {
               inletLoading = lossRate/(inletVolumeFlow * (1.0 - efficiency));
               Calculate(inletParticleLoading, inletLoading);
               
               collectionRate = lossRate * efficiency/(1.0 - efficiency);
               Calculate(particleOutlet.MassFlowRate, collectionRate);
               
               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               solveState = SolveState.PartiallySolved;
            }
         }
         if (solveState == SolveState.PartiallySolved && pressureDrop.HasValue
            && gasInlet.Pressure.HasValue && gasOutlet.Pressure.HasValue) {
            solveState = SolveState.Solved;
         }
      }

      private void PostBalanceCalculation() {
         if (solveState == SolveState.Solved) {
            DryingGasStreamComponents dgsc;
            DryingGasStream gasInlet = null;
            DryingGasStream gasOutlet = null;
            if (gasInlet is DryingGasStream) {
               gasInlet = gasInlet as DryingGasStream;
               gasOutlet = gasOutlet as DryingGasStream;
            }
            if (gasInlet != null) {
               double gasOutMassFlow = gasOutlet.MassFlowRate.Value;
               double gasMoistureContent = gasOutlet.Humidity.Value;
               double wg = gasOutlet.MassFlowRateDryBase.Value;
               if (gasOutMassFlow == Constants.NO_VALUE && gasMoistureContent != Constants.NO_VALUE && wg != Constants.NO_VALUE) {
                  gasOutMassFlow = wg * (1.0 + gasMoistureContent);
               }

               if (gasOutMassFlow != Constants.NO_VALUE) {
                  SolidPhase sp;
                  dgsc = gasInlet.DryingGasStreamComponents;
                  double massFlowRateOfEntrainedMaterial;
                  if (dgsc.NumberOfPhases <= 1) {
                     ArrayList solidCompList = new ArrayList();
                     StreamComponent pc = new StreamComponent(dgsc.DryMaterial.Substance);
                     solidCompList.Add(pc);
                     pc = new StreamComponent(dgsc.Moisture.Substance);
                     solidCompList.Add(pc);
                     sp = new SolidPhase("Cyclone Outlet Solid Phase", solidCompList);
                  }
                  else {
                     sp = (SolidPhase) dgsc.SolidPhase.Clone();
                  }
                  double volumeFlow = gasInlet.VolumeFlowRate.Value;
                  double inletLoading = inletParticleLoading.Value;
                  double efficiency = collectionEfficiency.Value;
                  massFlowRateOfEntrainedMaterial = volumeFlow * inletLoading * (1.0 - efficiency);
                  Calculate(massFlowRateOfParticleLostToGasOutlet, massFlowRateOfEntrainedMaterial);
                  sp.MassFraction = massFlowRateOfEntrainedMaterial/gasOutMassFlow;
                  dgsc = gasOutlet.DryingGasStreamComponents;
                  if (dgsc.SolidPhase == null) {
                     dgsc.AddPhase(sp);
                  }
                  else {
                     dgsc.SolidPhase = sp;
                  }
               }
            }
         }
      }*/

