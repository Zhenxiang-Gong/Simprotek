using System;
using System.Runtime.Serialization;
using System.Security.Permissions;                                            

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public class ElectrostaticPrecipitator : TwoStreamUnitOperation, IGasSolidSeparator {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

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
         //BalanceSpecificEnthalpy(inlet, outlet);
         BalanceAdiabaticProcess(inlet, outlet);

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
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersion", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));
         
         info.AddValue("DriftVelocity", this.driftVelocity, typeof(ProcessVarDouble));
         info.AddValue("TotalSurfaceArea", this.totalSurfaceArea, typeof(ProcessVarDouble));
      }
   }
}

