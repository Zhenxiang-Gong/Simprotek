using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.FluidTransport {
   /// <summary>
   /// Summary description for Ejector.
   /// </summary>
   [Serializable]
   public class Ejector : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int MOTIVE_INLET_INDEX = 0;
      public static int SUCTION_INLET_INDEX = 1;
      public static int DISCHARGE_OUTLET_INDEX = 2;

      private ProcessStreamBase motiveInlet;
      private ProcessStreamBase suctionInlet;
      private ProcessStreamBase dischargeOutlet;
      
      //suction/motive mass flow ratio
      private ProcessVarDouble entrainmentRatio;
      //discharge/suction pressure ratio
      private ProcessVarDouble compressionRatio;
      //suction/motive pressure ratio
      private ProcessVarDouble suctionMotivePressureRatio;

      #region public properties
      public ProcessStreamBase MotiveInlet {
         get { return motiveInlet; }
      }
      
      public ProcessStreamBase DischargeOutlet {
         get { return dischargeOutlet; }
      }

      public ProcessStreamBase SuctionInlet {
         get { return suctionInlet; }
      }
      
      public ProcessVarDouble EntrainmentRatio {
         get { return entrainmentRatio; }
      }
      
      public ProcessVarDouble CompressionRatio {
         get { return compressionRatio; }
      }

      public ProcessVarDouble SuctionMotivePressureRatio {
         get { return suctionMotivePressureRatio; }
      }
      #endregion

      public Ejector(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         entrainmentRatio = new ProcessVarDouble(StringConstants.ENTRAINMENT_RATIO, PhysicalQuantity.Fraction, VarState.Specified, this);
         compressionRatio = new ProcessVarDouble(StringConstants.COMPRESSION_RATIO, PhysicalQuantity.Fraction, VarState.Specified, this);
         suctionMotivePressureRatio = new ProcessVarDouble(StringConstants.SUCTION_MOTIVE_PRESSURE_RATIO, PhysicalQuantity.Fraction, VarState.Specified, this);
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(entrainmentRatio);
         AddVarOnListAndRegisterInSystem(compressionRatio);
         AddVarOnListAndRegisterInSystem(suctionMotivePressureRatio);
      }

      public override bool CanConnect(int streamIndex) 
      {
         bool retValue = false;
         if (streamIndex == MOTIVE_INLET_INDEX && motiveInlet == null) 
         {
            retValue = true;
         }
         else if (streamIndex == SUCTION_INLET_INDEX && suctionInlet == null) 
         {
            retValue = true;
         }
         else if (streamIndex == DISCHARGE_OUTLET_INDEX && dischargeOutlet == null) 
         {
            retValue = true;
         }

         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) 
      {
         bool canAttach = false;
         bool isLiquidMaterial = false;
         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }
         
         if (streamIndex == MOTIVE_INLET_INDEX && motiveInlet == null && ps.DownStreamOwner == null && (isLiquidMaterial || ps is ProcessStream)) {
            if (dischargeOutlet != null && dischargeOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (suctionInlet != null && suctionInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (dischargeOutlet == null && suctionInlet == null) {
               canAttach = true;
            }
         }
         else if (streamIndex == SUCTION_INLET_INDEX && suctionInlet == null && ps.DownStreamOwner == null && (isLiquidMaterial || ps is ProcessStream)) {
            if (dischargeOutlet != null && dischargeOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (motiveInlet != null && motiveInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (dischargeOutlet == null && motiveInlet == null ) {
               canAttach = true;
            }
         }
         else if (streamIndex == DISCHARGE_OUTLET_INDEX && dischargeOutlet == null && ps.UpStreamOwner == null && (isLiquidMaterial || ps is ProcessStream)) {
            if (motiveInlet != null && motiveInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            if (suctionInlet != null && suctionInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (motiveInlet == null && suctionInlet == null) {
               canAttach = true;
            }
         }
         
         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == MOTIVE_INLET_INDEX) {
            motiveInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == DISCHARGE_OUTLET_INDEX) {
            dischargeOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         
         else if (streamIndex == SUCTION_INLET_INDEX) {
            suctionInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else {
            attached = false;
         }
         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = true;
         if (ps == motiveInlet) {
            motiveInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == dischargeOutlet) {
            dischargeOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == suctionInlet) {
            suctionInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
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
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == entrainmentRatio && aValue <= 0.0) {
            retValue = CreateLessThanZeroErrorMessage(pv);
         }
         else if (pv == compressionRatio) {
            if (aValue <= 1.0) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(compressionRatio.VarTypeName + " cannot be smaller than 1.0.");
            }
            else if (suctionInlet.Pressure.HasValue && motiveInlet.Pressure.HasValue) {
               double maxRatio = motiveInlet.Pressure.Value/suctionInlet.Pressure.Value;
               if (aValue > maxRatio) {
                  retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(compressionRatio.VarTypeName + " cannot be greater than " + maxRatio + ".");
               }
            }
         }
         else if (pv == suctionMotivePressureRatio && aValue >= 1.0) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(suctionMotivePressureRatio.VarTypeName + " cannot be greater than 1.0.");
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv.VarTypeName == StringConstants.GetTypeName(StringConstants.VAPOR_FRACTION) && aValue < 0.9999) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of an inlet or outlet in a steam jet ejector cannot be less than 1.0.");
         }
         else if (pv == dischargeOutlet.Pressure) {
            if (motiveInlet.Pressure.IsSpecifiedAndHasValue && aValue >= motiveInlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of discharge outlet cannot be greater than that of motive inlet.");
            }
            else if (suctionInlet.Pressure.IsSpecifiedAndHasValue && aValue <= suctionInlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of discharge outlet cannot be smaller than that of suction inlet.");
            }
         }
         else if (pv == suctionInlet.Pressure) {
            if (motiveInlet.Pressure.IsSpecifiedAndHasValue && aValue >= motiveInlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of suction inlet cannot be greater than that of motive inlet.");
            }
            else if (dischargeOutlet.Pressure.IsSpecifiedAndHasValue && aValue <= dischargeOutlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of suction inlet cannot be greater than that of discharge outlet.");
            }
         }
         else if (pv == motiveInlet.Pressure) {
            if (dischargeOutlet.Pressure.IsSpecifiedAndHasValue && aValue <= dischargeOutlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of motive inlet cannot be smaller than that of discharge outlet.");
            }
            else if (suctionInlet.Pressure.IsSpecifiedAndHasValue && aValue <= suctionInlet.Pressure.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of motive inlet cannot be smaller than that of suction inlet.");
            }
         }

         return retValue;
      }

      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (motiveInlet == null || dischargeOutlet == null || suctionInlet == null) {
            isReady = false;
         }
         return isReady;
      }

//      protected override bool IsSolveReady() {
//         return true;
//      }
//
      public override void Execute(bool propagate) {
         PreSolve();
         if (IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }
               
      private void Solve() {
         //flow balance
         if (entrainmentRatio.HasValue) {
            if (motiveInlet.MassFlowRate.HasValue) {
               Calculate(suctionInlet.MassFlowRate, motiveInlet.MassFlowRate.Value * entrainmentRatio.Value);
               Calculate(dischargeOutlet.MassFlowRate, (motiveInlet.MassFlowRate.Value + suctionInlet.MassFlowRate.Value));
            }
            else if (suctionInlet.MassFlowRate.HasValue) {
               Calculate(motiveInlet.MassFlowRate, suctionInlet.MassFlowRate.Value/entrainmentRatio.Value);
               Calculate(dischargeOutlet.MassFlowRate, (motiveInlet.MassFlowRate.Value + suctionInlet.MassFlowRate.Value));
            }
            else if (dischargeOutlet.MassFlowRate.HasValue) {
               Calculate(motiveInlet.MassFlowRate, dischargeOutlet.MassFlowRate.Value/(1.0+entrainmentRatio.Value));
               Calculate(suctionInlet.MassFlowRate, motiveInlet.MassFlowRate.Value * entrainmentRatio.Value);
            }
         }
         else if (motiveInlet.MassFlowRate.HasValue) {
            if (suctionInlet.MassFlowRate.HasValue) {
               Calculate(entrainmentRatio, suctionInlet.MassFlowRate.Value/motiveInlet.MassFlowRate.Value);
               Calculate(dischargeOutlet.MassFlowRate, (motiveInlet.MassFlowRate.Value + suctionInlet.MassFlowRate.Value));
            }
            else if (dischargeOutlet.MassFlowRate.HasValue) {
               Calculate(suctionInlet.MassFlowRate, dischargeOutlet.MassFlowRate.Value - motiveInlet.MassFlowRate.Value);
               Calculate(entrainmentRatio, suctionInlet.MassFlowRate.Value/motiveInlet.MassFlowRate.Value);
            }
         }
         else if (suctionInlet.MassFlowRate.HasValue) {
            if (dischargeOutlet.MassFlowRate.HasValue) {
               Calculate(motiveInlet.MassFlowRate, dischargeOutlet.MassFlowRate.Value - suctionInlet.MassFlowRate.Value);
               Calculate(entrainmentRatio, suctionInlet.MassFlowRate.Value/motiveInlet.MassFlowRate.Value);
            }
         }

         //material balance if necessary
         if (motiveInlet is DryingMaterialStream) 
         {
            DryingMaterialStream mInlet = motiveInlet as DryingMaterialStream;
            DryingMaterialStream sInlet = suctionInlet as DryingMaterialStream;
            DryingMaterialStream dOutlet = dischargeOutlet as DryingMaterialStream;
            if (mInlet.MassFlowRate.HasValue && mInlet.MassConcentration.HasValue && 
               sInlet.MassFlowRate.HasValue && sInlet.MassConcentration.HasValue && dOutlet.MassFlowRate.HasValue)
            {
               double dOutletConcentration = (mInlet.MassFlowRate.Value * mInlet.MassConcentration.Value + 
                     sInlet.MassFlowRate.Value * sInlet.MassConcentration.Value)/dOutlet.MassFlowRate.Value;
               Calculate(dOutlet.MassConcentration, dOutletConcentration); 
            }
            else if (mInlet.MassFlowRate.HasValue && mInlet.MassConcentration.HasValue && 
               dOutlet.MassFlowRate.HasValue && dOutlet.MassConcentration.HasValue && sInlet.MassFlowRate.HasValue)
            {
               double sInletConcentration = (dOutlet.MassFlowRate.Value * dOutlet.MassConcentration.Value - 
                  mInlet.MassFlowRate.Value * mInlet.MassConcentration.Value)/sInlet.MassFlowRate.Value;
               Calculate(sInlet.MassConcentration, sInletConcentration); 
            }
            else if (sInlet.MassFlowRate.HasValue && sInlet.MassConcentration.HasValue && 
               dOutlet.MassFlowRate.HasValue && dOutlet.MassConcentration.HasValue && mInlet.MassFlowRate.HasValue)
            {
               double sInletConcentration = (dOutlet.MassFlowRate.Value * dOutlet.MassConcentration.Value - 
                  sInlet.MassFlowRate.Value * sInlet.MassConcentration.Value)/sInlet.MassFlowRate.Value;
               Calculate(mInlet.MassConcentration, sInletConcentration); 
            }
         }

         //pressure balance
         if (suctionInlet.Pressure.HasValue) {
            if (compressionRatio.HasValue) {
               Calculate(dischargeOutlet.Pressure, suctionInlet.Pressure.Value * compressionRatio.Value);
            }
            else if (dischargeOutlet.Pressure.HasValue) {
               Calculate(compressionRatio, dischargeOutlet.Pressure.Value/suctionInlet.Pressure.Value);
            }
            
            if (suctionMotivePressureRatio.HasValue) {
               Calculate(motiveInlet.Pressure, suctionInlet.Pressure.Value/suctionMotivePressureRatio.Value);
            }
            else if (motiveInlet.Pressure.HasValue) {
               Calculate(suctionMotivePressureRatio, suctionInlet.Pressure.Value/motiveInlet.Pressure.Value);
            }
         }
         else if (motiveInlet.Pressure.HasValue) {
            if (suctionMotivePressureRatio.HasValue) {
               Calculate(suctionInlet.Pressure, motiveInlet.Pressure.Value * suctionMotivePressureRatio.Value);
            }
            else if (suctionInlet.Pressure.HasValue) {
               Calculate(suctionMotivePressureRatio, suctionInlet.Pressure.Value/motiveInlet.Pressure.Value);
            }

         }
         else if (dischargeOutlet.Pressure.HasValue) {
            if (compressionRatio.HasValue && dischargeOutlet.Pressure.HasValue) {
               Calculate(suctionInlet.Pressure, dischargeOutlet.Pressure.Value/compressionRatio.Value);
            }
         }

         //energy balance
         if (motiveInlet.SpecificEnthalpy.HasValue && motiveInlet.MassFlowRate.HasValue && 
            suctionInlet.SpecificEnthalpy.HasValue && suctionInlet.MassFlowRate.HasValue && 
            dischargeOutlet.MassFlowRate.HasValue) 
         {
            double dischargeSpecificEnthalpy = (motiveInlet.SpecificEnthalpy.Value * motiveInlet.MassFlowRate.Value +
                                       suctionInlet.SpecificEnthalpy.Value * suctionInlet.MassFlowRate.Value)/dischargeOutlet.MassFlowRate.Value;
            Calculate(dischargeOutlet.SpecificEnthalpy, dischargeSpecificEnthalpy);
         }
         else if (motiveInlet.SpecificEnthalpy.HasValue && motiveInlet.MassFlowRate.HasValue && 
            dischargeOutlet.SpecificEnthalpy.HasValue && dischargeOutlet.MassFlowRate.HasValue && 
            suctionInlet.MassFlowRate.HasValue) 
         {
            double suctionSpecificEnthalpy = (dischargeOutlet.SpecificEnthalpy.Value * dischargeOutlet.MassFlowRate.Value -
               motiveInlet.SpecificEnthalpy.Value * motiveInlet.MassFlowRate.Value)/suctionInlet.MassFlowRate.Value;
            Calculate(suctionInlet.SpecificEnthalpy, suctionSpecificEnthalpy);
         }
         else if (suctionInlet.SpecificEnthalpy.HasValue && suctionInlet.MassFlowRate.HasValue && 
            dischargeOutlet.SpecificEnthalpy.HasValue && dischargeOutlet.MassFlowRate.HasValue && 
            motiveInlet.MassFlowRate.HasValue) 
         {
            double motiveSpecificEnthalpy = (dischargeOutlet.SpecificEnthalpy.Value * dischargeOutlet.MassFlowRate.Value -
               suctionInlet.SpecificEnthalpy.Value * suctionInlet.MassFlowRate.Value)/motiveInlet.MassFlowRate.Value;
            Calculate(motiveInlet.SpecificEnthalpy, motiveSpecificEnthalpy);
         }

         if (motiveInlet.MassFlowRate.HasValue && suctionInlet.MassFlowRate.HasValue 
            && dischargeOutlet.MassFlowRate.HasValue && motiveInlet.Pressure.HasValue
            && suctionInlet.Pressure.HasValue && dischargeOutlet.Pressure.HasValue 
            && motiveInlet.SpecificEnthalpy.HasValue && suctionInlet.SpecificEnthalpy.HasValue 
            && dischargeOutlet.SpecificEnthalpy.HasValue) 
         {
            solveState = SolveState.Solved;
         }

      }

      protected Ejector(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionEjector", typeof(int));
         if (persistedClassVersion == 1) {
            this.motiveInlet = info.GetValue("MotiveInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.suctionInlet = info.GetValue("SuctionInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.dischargeOutlet = info.GetValue("DischargeOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase; 
            
            this.entrainmentRatio = RecallStorableObject("EntrainmentRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.compressionRatio = RecallStorableObject("CompressionRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.suctionMotivePressureRatio = RecallStorableObject("SuctionMotivePressureRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionEjector", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("MotiveInlet", this.motiveInlet, typeof(ProcessStreamBase));
         info.AddValue("SuctionInlet", this.suctionInlet, typeof(ProcessStreamBase));
         info.AddValue("DischargeOutlet", this.dischargeOutlet, typeof(ProcessStreamBase));
         
         info.AddValue("EntrainmentRatio", this.entrainmentRatio, typeof(ProcessVarDouble));
         info.AddValue("CompressionRatio", this.compressionRatio, typeof(ProcessVarDouble));
         info.AddValue("SuctionMotivePressureRatio", this.suctionMotivePressureRatio, typeof(ProcessVarDouble));
      }
   }
}

