using System;
using System.Runtime.Serialization;
using System.Security.Permissions;                                            

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public abstract class FabricFilter : TwoStreamUnitOperation, IGasSolidSeparator {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      /*protected ProcessVarDouble pressureDrop;
      protected ProcessVarDouble collectionEfficiency;
      protected ProcessVarDouble inletDustLoading;
      protected ProcessVarDouble outletDustLoading;
      protected ProcessVarDouble dustAccumulationRate;*/
      private GasSolidSeparatorBalanceModel balanceModel;

      protected ProcessVarDouble gasToClothRatio;
      protected ProcessVarDouble totalFilteringArea;

      #region public properties
      /*public ProcessVarDouble PressureDrop {
         get {return pressureDrop;}
      }

      public ProcessVarDouble CollectionEfficiency {
         get { return collectionEfficiency; }
      }

      public ProcessVarDouble InletDustLoading {
         get { return inletDustLoading; }
      }

      public ProcessVarDouble OutletDustLoading {
         get { return outletDustLoading; }
      }

      public ProcessVarDouble DustAccumulationRate {
         get { return dustAccumulationRate; }
      }*/
      
      //interface IGasSolidSeparator--begin
      public ProcessStreamBase GasInlet {
         get { return inlet; }
      }
      
      public ProcessStreamBase GasOutlet {
         get { return outlet; }
      }

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
         get { return this; }
      }
      //interface IGasSolidSeparator--end
      
      public ProcessVarDouble GasToClothRatio {
         get {return gasToClothRatio;}
      }

      public ProcessVarDouble TotalFilteringArea {
         get {return totalFilteringArea;}
      }

      #endregion
      
      protected FabricFilter(string name, UnitOpSystem uoSys) : base(name, uoSys) {
         /*pressureDrop = new ProcessVarDouble(StringConstants.PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         collectionEfficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.Specified, this);
         inletDustLoading = new ProcessVarDouble(StringConstants.INLET_DUST_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, this);
         outletDustLoading = new ProcessVarDouble(StringConstants.OUTLET_DUST_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, this);
         dustAccumulationRate = new ProcessVarDouble(StringConstants.DUST_ACCUMULATION_RATE, PhysicalQuantity.MassFlowRate, VarState.AlwaysCalculated, this);*/
         balanceModel = new GasSolidSeparatorBalanceModel(this);
         
         gasToClothRatio = new ProcessVarDouble(StringConstants.FILTRATION_VELOCITY, PhysicalQuantity.Velocity, VarState.Specified, this);
         totalFilteringArea = new ProcessVarDouble(StringConstants.TOTAL_FILTERING_AREA, PhysicalQuantity.Area, VarState.AlwaysCalculated, this);
      }

      protected virtual void InitializeVarListAndRegisterVars() {
         /*base.InitializeVarListAndRegisterVars();

         AddVarOnListAndRegisterInSystem(pressureDrop);
         AddVarOnListAndRegisterInSystem(collectionEfficiency);
         AddVarOnListAndRegisterInSystem(inletDustLoading);
         AddVarOnListAndRegisterInSystem(outletDustLoading);
         AddVarOnListAndRegisterInSystem(dustAccumulationRate);*/
         AddVarOnListAndRegisterInSystem(gasToClothRatio);
         AddVarOnListAndRegisterInSystem(totalFilteringArea);
      }
      
      public override void Execute(bool propagate) {
         PreSolve();
         //balance presssure
         BalancePressure(inlet, outlet, GasPressureDrop);
         BalanceSpecificEnthalpy(inlet, outlet);
         //BalanceAdiabaticProcess(inlet, outlet);
         //dry gas flow balance
         if (inlet is DryingGasStream) 
         {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingGasStreamFlow(dsInlet, dsOutlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }
         else if (inlet is ProcessStream) {
            BalanceProcessStreamFlow(inlet, outlet);
         }
         UpdateStreamsIfNecessary();

         Solve();
         //PostSolve();
      }

      private void Solve() {
         balanceModel.DoBalanceCalculation();

         /*double inletVolumeFlow = inlet.VolumeFlowRate.Value;
         double outletVolumeFlow = outlet.VolumeFlowRate.Value;
         //if (!inlet.VolumeFlowRate.HasValue) {
         //   volumeFlowRate = outlet.VolumeFlowRate.Value;
         //}

         double inletMassFlow = inlet.MassFlowRate.Value;
         //if (!inlet.MassFlowRate.HasValue) {
         //   inletMassFlow = outlet.MassFlowRate.Value;
         //}
         
         double inletDustMassFlowRate = Constants.NO_VALUE; 
         if (inlet is DryingGasStream) {
            DryingGasStream dgsInlet = inlet as DryingGasStream;
         
            DryingGasStreamComponents dgsc = dgsInlet.DryingGasStreamComponents;
         
            SolidPhase sp = dgsc.SolidPhase;
            if (sp != null && inletMassFlow != Constants.NO_VALUE && inletVolumeFlow != Constants.NO_VALUE) {
               inletDustMassFlowRate = sp.MassFraction * inletMassFlow;
               Calculate(inletDustLoading, inletDustMassFlowRate/inletVolumeFlow);

               solveState = SolveState.PartiallySolved;
            }

            if (inletDustLoading.HasValue && collectionEfficiency.HasValue && inletVolumeFlow != Constants.NO_VALUE) {
               inletDustMassFlowRate = inletDustLoading.Value * inletVolumeFlow;

               if (outletVolumeFlow != Constants.NO_VALUE) {
                  double outletLoading = inletDustMassFlowRate * (1.0 - collectionEfficiency.Value)/outletVolumeFlow;
                  Calculate(outletDustLoading, outletLoading);
               }
            }
            else if (outletDustLoading.IsSpecifiedAndHasValue && collectionEfficiency.HasValue && outletVolumeFlow != Constants.NO_VALUE) {
               inletDustMassFlowRate = outletDustLoading.Value * outletVolumeFlow/(1.0 - collectionEfficiency.Value);
               
               if (inletVolumeFlow != Constants.NO_VALUE) {
                  double inletLoading = inletDustMassFlowRate * collectionEfficiency.Value/inletVolumeFlow;
                  Calculate(inletDustLoading, inletLoading);
               }
            }
            else if (inletDustLoading.HasValue && outletDustLoading.HasValue && inletVolumeFlow != Constants.NO_VALUE) {
               inletDustMassFlowRate = inletDustLoading.Value * inletVolumeFlow;

               if (outletVolumeFlow != Constants.NO_VALUE) {
                  double efficiency = 1.0 - outletDustLoading.Value * outletVolumeFlow/inletDustMassFlowRate;
                  Calculate(collectionEfficiency, efficiency);
               }
            }

            if (inletDustMassFlowRate != Constants.NO_VALUE && collectionEfficiency.HasValue) {
               Calculate(dustAccumulationRate, inletDustMassFlowRate * collectionEfficiency.Value);
               solveState = SolveState.PartiallySolved;
            }
         }

         if (inletDustMassFlowRate != Constants.NO_VALUE && collectionEfficiency.HasValue) {
            Calculate(dustAccumulationRate, inletDustMassFlowRate * collectionEfficiency.Value);
            solveState = SolveState.PartiallySolved;
         }*/
         
         if (inlet.VolumeFlowRate.HasValue && gasToClothRatio.HasValue) {
            double filteringArea = inlet.VolumeFlowRate.Value * gasToClothRatio.Value;
            Calculate(totalFilteringArea, filteringArea);
         }
         if (ParticleCollectionRate.HasValue && inlet.Temperature.HasValue
            && outlet.Temperature.HasValue && inlet.Pressure.HasValue
            && outlet.Pressure.HasValue && totalFilteringArea.HasValue) {
            solveState = SolveState.Solved;
         }
      }
   
      //implement interface IGasSolidSeparator
      public double CalculateParticleLoading(ProcessStreamBase psb) {
         DryingGasStream stream = psb as DryingGasStream;
         return balanceModel.CalculateParticleLoading(stream);
      }
      
      protected FabricFilter(SerializationInfo info, StreamingContext context) : base(info, context) {
         //outletDustLoading = new ProcessVarDouble(StringConstants.OUTLET_DUST_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, this);
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int classVersion = (int)info.GetValue("ClassPersistenceVersion", typeof(int));
         if (classVersion == 1) {
            /*this.pressureDrop = (ProcessVarDouble)RecallStorableObject("PressureDrop", typeof(ProcessVarDouble));
            this.collectionEfficiency = (ProcessVarDouble)RecallStorableObject("CollectionEfficiency", typeof(ProcessVarDouble));
            this.inletDustLoading = (ProcessVarDouble)RecallStorableObject("InletDustLoading", typeof(ProcessVarDouble));
            this.outletDustLoading = (ProcessVarDouble)RecallStorableObject("OutletDustLoading", typeof(ProcessVarDouble));
            this.dustAccumulationRate = (ProcessVarDouble)RecallStorableObject("DustAccumulationRate", typeof(ProcessVarDouble));*/
            this.balanceModel = RecallStorableObject("BalanceModel", typeof(GasSolidSeparatorBalanceModel)) as GasSolidSeparatorBalanceModel;
            
            this.gasToClothRatio = (ProcessVarDouble)RecallStorableObject("GasToClothRatio", typeof(ProcessVarDouble));
            this.totalFilteringArea = (ProcessVarDouble)RecallStorableObject("TotalFilteringArea", typeof(ProcessVarDouble));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersion", CLASS_PERSISTENCE_VERSION, typeof(int));
         /*info.AddValue("PressureDrop", this.pressureDrop, typeof(ProcessVarDouble));
         info.AddValue("CollectionEfficiency", this.collectionEfficiency, typeof(ProcessVarDouble));
         info.AddValue("InletDustLoading", this.inletDustLoading, typeof(ProcessVarDouble));
         info.AddValue("OutletDustLoading", this.outletDustLoading, typeof(ProcessVarDouble));
         info.AddValue("DustAccumulationRate", this.dustAccumulationRate, typeof(ProcessVarDouble));*/
         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));
         
         info.AddValue("GasToClothRatio", this.gasToClothRatio, typeof(ProcessVarDouble));
         info.AddValue("TotalFilteringArea", this.totalFilteringArea, typeof(ProcessVarDouble));
      }
   }
}

