using System;
using System.Runtime.Serialization;
using System.Security.Permissions;                                            

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public class ElectrostaticPrecipitator : TwoStreamUnitOperation, IGasSolidSeparator {
      private const int CLASS_PERSISTENCE_VERSION = 2;

      //public static int PARTICLE_OUTLET_INDEX = 3;
      
      protected ProcessStreamBase particleOutlet;

      private GasSolidSeparatorBalanceModel balanceModel;

      protected ProcessVarDouble driftVelocity;
      protected ProcessVarDouble totalSurfaceArea;

      #region public properties
      //interface IGasSolidSeparator--begin
      public ProcessStreamBase GasInlet {
         get { return inlet; }
      }
      
      public ProcessStreamBase GasOutlet {
         get { return outlet; }
      }

      public ProcessStreamBase ParticleOutlet
      {
         get { return particleOutlet; }
      }
      
      public ProcessVarDouble GasPressureDrop
      {
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

      public ProcessVarDouble DriftVelocity {
         get {return driftVelocity;}
      }

      public ProcessVarDouble TotalSurfaceArea {
         get {return totalSurfaceArea;}
      }

      #endregion
      
      public ElectrostaticPrecipitator(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         balanceModel = new GasSolidSeparatorBalanceModel(this);
         
         driftVelocity = new ProcessVarDouble(StringConstants.DRIFT_VELOCITY, PhysicalQuantity.Velocity, VarState.Specified, this);
         totalSurfaceArea = new ProcessVarDouble(StringConstants.TOTAL_SURFACE_AREA, PhysicalQuantity.Area, VarState.AlwaysCalculated, this);
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(driftVelocity);
         AddVarOnListAndRegisterInSystem(totalSurfaceArea);
      }

      public override bool CanAttach(int streamIndex)
      {
         bool retValue = false;
         if(streamIndex == INLET_INDEX && inlet == null)
         {
            retValue = true;
         }
         else if(streamIndex == OUTLET_INDEX && outlet == null)
         {
            retValue = true;
         }
         else if(streamIndex == BagFilter.PARTICLE_OUTLET_INDEX && particleOutlet == null)
         {
            retValue = true;
         }
         return retValue;
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex)
      {
         if((streamIndex == INLET_INDEX && ps.DownStreamOwner != null)
            || (streamIndex == OUTLET_INDEX && ps.UpStreamOwner != null)
            || (streamIndex == BagFilter.PARTICLE_OUTLET_INDEX && ps.UpStreamOwner != null))
         {
            return false;
         }

         bool canAttach = false;

         if(ps is DryingGasStream)
         {
            if(streamIndex == INLET_INDEX && inlet == null)
            {
               canAttach = true;
            }
            else if(streamIndex == OUTLET_INDEX && outlet == null)
            {
               canAttach = true;
            }
         }
         else if(ps is DryingMaterialStream)
         {
            if(streamIndex == BagFilter.PARTICLE_OUTLET_INDEX && particleOutlet == null)
            {
               canAttach = true;
            }
         }
         return canAttach;
      }

      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex)
      {
         bool attached = true;
         if(streamIndex == INLET_INDEX)
         {
            inlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if(streamIndex == OUTLET_INDEX)
         {
            outlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if(streamIndex == BagFilter.PARTICLE_OUTLET_INDEX)
         {
            particleOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else
         {
            attached = false;
         }

         return attached;
      }

      internal override bool DoDetach(ProcessStreamBase ps)
      {
         bool detached = true;
         if(ps == inlet)
         {
            inletStreams.Remove(ps);
            inlet = null;
            ps.DownStreamOwner = null;
         }
         else if(ps == outlet)
         {
            outletStreams.Remove(ps);
            outlet = null;
            ps.UpStreamOwner = null;
         }
         else if(ps == particleOutlet)
         {
            outletStreams.Remove(ps);
            particleOutlet = null;
            ps.UpStreamOwner = null;
         }
         else
         {
            detached = false;
         }

         if(detached)
         {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }

      internal override bool IsBalanceCalcReady()
      {
         bool isReady = true;
         //if(inlet == null || outlet == null || particleOutlet == null)
         if(inlet == null || outlet == null)
         {
            isReady = false;
         }
         return isReady;
      }
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue)
      {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (retValue == null) {
            if (calculationType == UnitOpCalculationType.Balance) {
               retValue = balanceModel.CheckSpecifiedValueRange(pv, aValue);
            }
         }
         
         if (pv == driftVelocity && aValue <= 0.0) {
            retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;

         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue < outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the electrostatic precipitator inlet must be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue > inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the electrostatic precipitator outlet cannot be greater than that of the inlet.");
         }

         return retValue;
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

         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         //PostSolve(false);
         UpdateStreamsIfNecessary();

         Solve();
         PostSolve();
      }

      private void Solve() {
         balanceModel.DoBalanceCalculation();
         
         if (CollectionEfficiency.HasValue && driftVelocity.HasValue && inlet.VolumeFlowRate.HasValue) {
            if (driftVelocity.HasValue) {
               double surfaceArea = - Math.Log(1 - CollectionEfficiency.Value)/driftVelocity.Value * inlet.VolumeFlowRate.Value;
               Calculate(totalSurfaceArea, surfaceArea);
            }
            else if (totalSurfaceArea.HasValue) {
               double velocity = - Math.Log(1 - CollectionEfficiency.Value)/totalSurfaceArea.Value * inlet.VolumeFlowRate.Value;
               Calculate(driftVelocity, velocity);
            }
         }

         if (ParticleCollectionRate.HasValue && inlet.Temperature.HasValue
            && outlet.Temperature.HasValue && inlet.Pressure.HasValue
            && outlet.Pressure.HasValue && totalSurfaceArea.HasValue 
            && driftVelocity.HasValue && CollectionEfficiency.HasValue) {
            solveState = SolveState.Solved;
         }

         if(solveState == SolveState.Solved && particleOutlet != null)
         {
            Calculate(particleOutlet.MassFlowRate, ParticleCollectionRate.Value);
         }

      }
   
      //implement interface IGasSolidSeparator
      public double CalculateParticleLoading(ProcessStreamBase psb) {
         DryingGasStream stream = psb as DryingGasStream;
         return balanceModel.CalculateParticleLoading(stream);
      }
      
      protected ElectrostaticPrecipitator(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int classVersion = (int)info.GetValue("ClassPersistenceVersion", typeof(int));
         if (classVersion == 1) {
            this.balanceModel = RecallStorableObject("BalanceModel", typeof(GasSolidSeparatorBalanceModel)) as GasSolidSeparatorBalanceModel;
            
            this.driftVelocity = (ProcessVarDouble)RecallStorableObject("DriftVelocity", typeof(ProcessVarDouble));
            this.totalSurfaceArea = (ProcessVarDouble)RecallStorableObject("TotalSurfaceArea", typeof(ProcessVarDouble));
         }

         if(classVersion >= 2)
         {
            this.particleOutlet = info.GetValue("ParticleOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersion", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));
         
         info.AddValue("DriftVelocity", this.driftVelocity, typeof(ProcessVarDouble));
         info.AddValue("TotalSurfaceArea", this.totalSurfaceArea, typeof(ProcessVarDouble));
         //version 2
         info.AddValue("ParticleOutlet", this.particleOutlet, typeof(ProcessStreamBase));
      }
   }
}

