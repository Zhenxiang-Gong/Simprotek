using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.Materials;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {

   [Serializable] 
   internal class GasSolidSeparatorBalanceModel : Storable{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      protected ArrayList procVarList = new ArrayList();
      private IGasSolidSeparator owner;
      private UnitOperation ownerUnitOp;
      
      private ProcessVarDouble gasPressureDrop;
      private ProcessVarDouble collectionEfficiency;
      private ProcessVarDouble inletParticleLoading;
      private ProcessVarDouble outletParticleLoading;
      private ProcessVarDouble particleCollectionRate;
      private ProcessVarDouble massFlowRateOfParticleLostToGasOutlet;

      #region public properties
      internal ProcessVarDouble GasPressureDrop {
         get { return gasPressureDrop; }
      }

      internal ProcessVarDouble CollectionEfficiency {
         get { return collectionEfficiency; }
      }

      internal ProcessVarDouble InletParticleLoading {
         get { return inletParticleLoading; }
      }
      
      internal ProcessVarDouble OutletParticleLoading {
         get { return outletParticleLoading; }
      }
      
      internal ProcessVarDouble ParticleCollectionRate {
         get { return particleCollectionRate; }
      }
      
      internal ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet {
         get { return massFlowRateOfParticleLostToGasOutlet; }
      }
      #endregion
      
      public GasSolidSeparatorBalanceModel(IGasSolidSeparator owner) : base() {
         this.owner = owner;
         this.ownerUnitOp = owner.MyUnitOperation;

         gasPressureDrop = new ProcessVarDouble(StringConstants.GAS_PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, ownerUnitOp);
         collectionEfficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.Specified, ownerUnitOp);
         inletParticleLoading = new ProcessVarDouble(StringConstants.INLET_PARTICLE_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, ownerUnitOp);
         outletParticleLoading = new ProcessVarDouble(StringConstants.OUTLET_PARTICLE_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.Specified, ownerUnitOp);
         particleCollectionRate = new ProcessVarDouble(StringConstants.PARTICLE_COLLECTION_RATE, PhysicalQuantity.MassFlowRate, VarState.Specified, ownerUnitOp);
         massFlowRateOfParticleLostToGasOutlet = new ProcessVarDouble(StringConstants.PARTICLE_LOSS_TO_GAS_OUTLET, PhysicalQuantity.MassFlowRate, VarState.Specified, ownerUnitOp);

         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         procVarList.Add(gasPressureDrop);
         procVarList.Add(collectionEfficiency);
         procVarList.Add(inletParticleLoading);
         procVarList.Add(outletParticleLoading);
         procVarList.Add(particleCollectionRate);
         procVarList.Add(massFlowRateOfParticleLostToGasOutlet);
         ownerUnitOp.AddVarsOnListAndRegisterInSystem(procVarList);
      }
      
      private void Calculate(ProcessVarDouble pv, double aValue) {
         ownerUnitOp.Calculate(pv, aValue);
      }

      internal ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         return retValue;
      }

      internal void DoBalanceCalculation() {
         DryingGasStream gasInlet = owner.GasInlet as DryingGasStream;
         DryingGasStream gasOutlet = owner.GasOutlet as DryingGasStream;
         double inletVolumeFlow = gasInlet.VolumeFlowRate.Value;
         double outletVolumeFlow = gasOutlet.VolumeFlowRate.Value;
         double inletLoading = inletParticleLoading.Value;
         double outletLoading = outletParticleLoading.Value;
         double collectionRate = particleCollectionRate.Value;
         double lossRate = massFlowRateOfParticleLostToGasOutlet.Value;
         double efficiency = collectionEfficiency.Value;

         double inletMassFlow = gasInlet.MassFlowRate.Value;
         double inletMoistureContent = gasInlet.Humidity.Value;
         double wg = gasInlet.MassFlowRateDryBase.Value;

         if (inletMassFlow == Constants.NO_VALUE && inletMoistureContent != Constants.NO_VALUE && 
             wg != Constants.NO_VALUE) {
            inletMassFlow = wg * (1.0 + inletMoistureContent);
         }

         DryingGasComponents dgc = gasInlet.GasComponents;
         SolidPhase sp = dgc.SolidPhase;
         if (inletVolumeFlow != Constants.NO_VALUE && outletVolumeFlow != Constants.NO_VALUE) {
            if (sp != null && inletMassFlow != Constants.NO_VALUE) {
               inletLoading = CalculateParticleLoading(gasInlet);
               Calculate(inletParticleLoading, inletLoading);
               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
            
            if (inletLoading != Constants.NO_VALUE && efficiency != Constants.NO_VALUE) {
               collectionRate = inletLoading * inletVolumeFlow * efficiency;
               Calculate(particleCollectionRate, collectionRate);
               
               lossRate = inletLoading * inletVolumeFlow * (1.0 - efficiency);
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);

               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
            else if (inletLoading != Constants.NO_VALUE && outletLoading != Constants.NO_VALUE) {
               collectionRate = inletLoading * inletVolumeFlow - outletLoading * outletVolumeFlow;
               Calculate(particleCollectionRate, collectionRate);
               
               efficiency = collectionRate/(inletLoading * inletVolumeFlow);
               Calculate(collectionEfficiency, efficiency);

               lossRate = outletLoading * outletVolumeFlow;
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);
               
               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
            else if (outletLoading != Constants.NO_VALUE && efficiency != Constants.NO_VALUE) {
               lossRate = outletLoading * outletVolumeFlow;
               Calculate(massFlowRateOfParticleLostToGasOutlet, lossRate);

               inletLoading = lossRate/(inletVolumeFlow * (1.0 - efficiency));
               Calculate(inletParticleLoading, inletLoading);

               collectionRate = inletLoading * inletVolumeFlow * efficiency;
               Calculate(particleCollectionRate, collectionRate);
               
               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
            else if (lossRate != Constants.NO_VALUE && collectionRate != Constants.NO_VALUE) {
               inletLoading = (lossRate + collectionRate)/inletVolumeFlow;
               Calculate(inletParticleLoading, inletLoading);
               
               efficiency = collectionRate/(lossRate + collectionRate);
               Calculate(collectionEfficiency, efficiency);

               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
            else if (lossRate != Constants.NO_VALUE && efficiency != Constants.NO_VALUE && efficiency < 1.0) {
               inletLoading = lossRate/(inletVolumeFlow * (1.0 - efficiency));
               Calculate(inletParticleLoading, inletLoading);
               
               collectionRate = lossRate * efficiency/(1.0 - efficiency);
               Calculate(particleCollectionRate, collectionRate);
               
               outletLoading = lossRate/outletVolumeFlow;
               Calculate(outletParticleLoading, outletLoading);

               ownerUnitOp.SolveState = SolveState.PartiallySolved;
            }
         }
      }

      internal void PostBalanceCalculation() {
         DryingGasStream gasInlet = owner.GasInlet as DryingGasStream;
         DryingGasStream gasOutlet = owner.GasOutlet as DryingGasStream;
         double gasOutMassFlow = gasOutlet.MassFlowRate.Value;
         double gasMoistureContent = gasOutlet.Humidity.Value;
         double wg = gasOutlet.MassFlowRateDryBase.Value;
         if (gasOutMassFlow == Constants.NO_VALUE && gasMoistureContent != Constants.NO_VALUE && wg != Constants.NO_VALUE) {
            gasOutMassFlow = wg * (1.0 + gasMoistureContent);
         }

         if (gasOutMassFlow != Constants.NO_VALUE) {
            SolidPhase sp = null;
            DryingGasComponents inletDgc = gasInlet.GasComponents;
            DryingGasComponents outletDgc = gasOutlet.GasComponents;
            double massFlowRateOfEntrainedMaterial;
            sp = outletDgc.SolidPhase;
            if (sp == null) 
            {
               if (inletDgc.NumberOfPhases <= 1) 
               {
                  ArrayList solidCompList = new ArrayList();
                  MaterialComponent mc = new MaterialComponent(inletDgc.AbsoluteDryMaterial.Substance);
                  solidCompList.Add(mc);
                  mc = new MaterialComponent(inletDgc.Moisture.Substance);
                  solidCompList.Add(mc);
                  sp = new SolidPhase("Cyclone Outlet Solid Phase", solidCompList);
               }
               else 
               {
                  sp = (SolidPhase) inletDgc.SolidPhase.Clone();
               }

               outletDgc.AddPhase(sp);
            }

            double volumeFlow = gasInlet.VolumeFlowRate.Value;
            double inletLoading = inletParticleLoading.Value;
            double efficiency = collectionEfficiency.Value;
            massFlowRateOfEntrainedMaterial = volumeFlow * inletLoading * (1.0 - efficiency);
            Calculate(massFlowRateOfParticleLostToGasOutlet, massFlowRateOfEntrainedMaterial);
            sp.MassFraction = massFlowRateOfEntrainedMaterial/gasOutMassFlow;
         }
      }

      internal double CalculateParticleLoading(ProcessStreamBase psb) {
         DryingGasStream stream = psb as DryingGasStream;
         DryingGasComponents dgc = stream.GasComponents;
         SolidPhase sp = dgc.SolidPhase;
         double massFlow = stream.MassFlowRate.Value;
         double volumeFlow = stream.VolumeFlowRate.Value;
         double loading = Constants.NO_VALUE;
         if (massFlow != Constants.NO_VALUE && volumeFlow != Constants.NO_VALUE) {
            double massFlowOfParticle = sp.MassFraction * massFlow;
            loading = massFlowOfParticle/volumeFlow;
         }
         return loading;
      }

      protected GasSolidSeparatorBalanceModel(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionGasSolidSeparatorBalanceModel", typeof(int));
         if (persistedClassVersion == 1) {
            this.owner = info.GetValue("Owner", typeof(IGasSolidSeparator)) as IGasSolidSeparator;
            this.ownerUnitOp = info.GetValue("OwnerUnitOp", typeof(UnitOperation)) as UnitOperation;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;

            this.gasPressureDrop = RecallStorableObject("GasPressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.collectionEfficiency = RecallStorableObject("CollectionEfficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.inletParticleLoading = RecallStorableObject("InletParticleLoading", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletParticleLoading = RecallStorableObject("OutletParticleLoading", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.particleCollectionRate = RecallStorableObject("ParticleCollectionRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.massFlowRateOfParticleLostToGasOutlet = RecallStorableObject("MassFlowRateOfParticleLostToGasOutlet", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionGasSolidSeparatorBalanceModel", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("Owner", this.owner, typeof(IGasSolidSeparator));
         info.AddValue("OwnerUnitOp", this.ownerUnitOp, typeof(UnitOperation));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         
         info.AddValue("GasPressureDrop", this.gasPressureDrop, typeof(ProcessVarDouble));
         info.AddValue("CollectionEfficiency", this.collectionEfficiency, typeof(ProcessVarDouble));
         info.AddValue("InletParticleLoading", this.inletParticleLoading, typeof(ProcessVarDouble));
         info.AddValue("OutletParticleLoading", this.outletParticleLoading, typeof(ProcessVarDouble));
         info.AddValue("ParticleCollectionRate", this.particleCollectionRate, typeof(ProcessVarDouble));
         info.AddValue("MassFlowRateOfParticleLostToGasOutlet", this.massFlowRateOfParticleLostToGasOutlet, typeof(ProcessVarDouble));
      }
   }
}
