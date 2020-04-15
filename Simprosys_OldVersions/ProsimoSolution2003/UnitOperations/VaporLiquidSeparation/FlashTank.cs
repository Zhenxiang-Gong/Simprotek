using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.VaporLiquidSeparation {
   /// <summary>
   /// Summary description for FlashTank.
   /// </summary>
   [Serializable] 
   public class FlashTank : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int INLET_INDEX = 0;
      public static int VAPOR_OUTLET_INDEX = 1;
      public static int LIQUID_OUTLET_INDEX = 2;
      
      private ProcessStreamBase inlet;
      private ProcessStreamBase vaporOutlet;
      private ProcessStreamBase liquidOutlet;
      
      #region public properties
      public ProcessStreamBase Inlet {
         get { return inlet; }
      }
      
      public ProcessStreamBase VaporOutlet {
         get { return vaporOutlet; }
      }

      public ProcessStreamBase LiquidOutlet {
         get { return liquidOutlet; }
      }
      
      #endregion

      public FlashTank(string name, UnitOpSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanConnect(int streamIndex) {
         bool retValue = false;
         if (streamIndex == INLET_INDEX && inlet == null) {
            retValue = true;
         }
         else if (streamIndex == VAPOR_OUTLET_INDEX && vaporOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX && liquidOutlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         bool canAttach = false;
         bool isLiquidMaterial = false;
         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }
         
         if (streamIndex == INLET_INDEX && inlet == null && ps.DownStreamOwner == null) {
            if (liquidOutlet != null && liquidOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            if (vaporOutlet != null && vaporOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (liquidOutlet == null && vaporOutlet == null && (isLiquidMaterial || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == VAPOR_OUTLET_INDEX && vaporOutlet == null && ps.UpStreamOwner == null) {
            if (inlet != null && inlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            if (vaporOutlet != null && vaporOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (inlet == null && vaporOutlet == null && (isLiquidMaterial || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX && liquidOutlet == null && ps.UpStreamOwner == null) {
            if (inlet != null && inlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            if (liquidOutlet != null && liquidOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (inlet == null && liquidOutlet == null && (isLiquidMaterial || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         
         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == INLET_INDEX) {
            inlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == VAPOR_OUTLET_INDEX) {
            vaporOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX) {
            liquidOutlet = ps;
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
         if (ps == inlet) {
            inlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == vaporOutlet) {
            vaporOutlet = null;
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
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (inlet == null || vaporOutlet == null || liquidOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         if (IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }
               
      private void Solve() {
         if (vaporOutlet.Pressure.HasValue) {
            Calculate(liquidOutlet.Pressure, vaporOutlet.Pressure.Value);
         }
         else if (liquidOutlet.Pressure.HasValue) {
            Calculate(vaporOutlet.Pressure, liquidOutlet.Pressure.Value);
         }

         if (vaporOutlet.Temperature.HasValue) {
            Calculate(liquidOutlet.Temperature, vaporOutlet.Temperature.Value);
         }
         else if (liquidOutlet.Temperature.HasValue) {
            Calculate(vaporOutlet.Temperature, liquidOutlet.Temperature.Value);
         }
         
         //drying material
         if (inlet is DryingMaterialStream) {
            DryingMaterialStream dmsInlet = inlet as DryingMaterialStream;
            DryingMaterialStream dmsVaporOutlet = vaporOutlet as DryingMaterialStream;
            DryingMaterialStream dmsLiquidOutlet = liquidOutlet as DryingMaterialStream;
            
            if (inlet.VaporFraction.HasValue && inlet.MassFlowRate.HasValue) {
               //double vaporFraction = inlet.VaporFraction.Value;
               double tBoilingPoint = inlet.GetBoilingPoint(vaporOutlet.Pressure.Value);
               double evapHeat = inlet.GetEvaporationHeat(tBoilingPoint);
               double cpLiquid = inlet.SpecificHeat.Value;
               double hBoilingPoint = cpLiquid * (tBoilingPoint - 273.15);
               double h = inlet.SpecificEnthalpy.Value;
               double vaporFraction = (h - hBoilingPoint)/evapHeat;

               double massFlowRate = inlet.MassFlowRate.Value;
               double vaporMassFlowRate = massFlowRate * vaporFraction;
               double liquidMassFlowRate = massFlowRate - vaporMassFlowRate;
               Calculate(vaporOutlet.MassFlowRate, vaporMassFlowRate);
               Calculate(liquidOutlet.MassFlowRate, liquidMassFlowRate);
               Calculate(vaporOutlet.VaporFraction, 1.0);
               Calculate(liquidOutlet.VaporFraction, 0.0);
               
               if (dmsInlet.MassConcentration.HasValue) {
                  double liquidConcentration = (massFlowRate * dmsInlet.MassConcentration.Value)/liquidMassFlowRate;
                  Calculate(dmsLiquidOutlet.MassConcentration, liquidConcentration);
                  Calculate(dmsVaporOutlet.MassConcentration, 0);
               }

               if (dmsLiquidOutlet.MassConcentration.HasValue && dmsVaporOutlet.MassConcentration.HasValue &&
                  dmsLiquidOutlet.Pressure.HasValue) {
                  solveState = SolveState.Solved;
               }
            }
         }
      }

      protected FlashTank(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlashTank", typeof(int));
         if (persistedClassVersion == 1) {
            this.inlet = info.GetValue("Inlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.vaporOutlet = info.GetValue("VaporOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.liquidOutlet = info.GetValue("LiquidOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFlashTank", FlashTank.CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Inlet", this.inlet, typeof(ProcessStreamBase));
         info.AddValue("VaporOutlet", this.vaporOutlet, typeof(ProcessStreamBase));
         info.AddValue("LiquidOutlet", this.liquidOutlet, typeof(ProcessStreamBase));
      }
   }
}

