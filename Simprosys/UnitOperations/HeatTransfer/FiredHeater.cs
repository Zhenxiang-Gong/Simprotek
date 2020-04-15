using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for Filter.
   /// </summary>
   [Serializable] 
   public class FiredHeater : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int FUEL_INLET_INDEX = 0;
      public static int AIR_INLET_INDEX = 1;
      public static int FLUE_GAS_OUTLET_INDEX = 2;
      public static int HEATED_INLET_INDEX = 3;
      public static int HEATED_OUTLET_INDEX = 4;

      private FuelStream fuelInlet;
      private DryingGasStream airInlet;
      private DryingGasStream flueGasOutlet;
      private DryingGasStream heatedInlet;
      private DryingGasStream heatedOutlet;

      #region public properties
      public FuelStream FuelInlet {
         get { return fuelInlet; }
      }

      public DryingGasStream AirInlet {
         get { return airInlet; }
      }

      public DryingGasStream FlueGasOutlet {
         get { return flueGasOutlet; }
      }

      public DryingGasStream HeatedInlet {
         get { return heatedOutlet; }
      }

      public DryingGasStream HeatedOutlet {
         get { return heatedOutlet; }
      }

      #endregion

      public FiredHeater(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanAttach(int streamIndex) {
         bool retValue = false;
         if (streamIndex == FUEL_INLET_INDEX && fuelInlet == null) {
            retValue = true;
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX && flueGasOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == AIR_INLET_INDEX && airInlet == null) {
            retValue = true;
         }
         else if (streamIndex == HEATED_INLET_INDEX && heatedInlet == null) {
            retValue = true;
         }
         else if (streamIndex == HEATED_OUTLET_INDEX && heatedOutlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         bool canAttach = false;
         
         if (streamIndex == FUEL_INLET_INDEX && fuelInlet == null && ps.DownStreamOwner == null) {
            if (ps is FuelStream) {
               canAttach = true;
            }
         }
         else if (streamIndex == AIR_INLET_INDEX && airInlet == null && ps.DownStreamOwner == null) {
            if (ps is DryingGasStream) {
               canAttach = true;
            }
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX && flueGasOutlet == null && ps.UpStreamOwner == null) {
            if (ps is DryingGasStream) {
               canAttach = true;
            }
         }
         else if (streamIndex == HEATED_INLET_INDEX && heatedOutlet == null && ps.DownStreamOwner == null) {
            if (ps is DryingGasStream) {
               canAttach = true;
            }
         }
         else if (streamIndex == HEATED_OUTLET_INDEX && heatedOutlet == null && ps.UpStreamOwner == null) {
            if (ps is DryingGasStream) {
               canAttach = true;
            }
         }
         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == FUEL_INLET_INDEX) {
            fuelInlet = ps as FuelStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == AIR_INLET_INDEX) {
            airInlet = ps as DryingGasStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX) {
            flueGasOutlet = ps as DryingGasStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == HEATED_INLET_INDEX) {
            heatedInlet = ps as DryingGasStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == HEATED_OUTLET_INDEX) {
            heatedOutlet = ps as DryingGasStream;
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
         if (ps == fuelInlet) {
            fuelInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == airInlet) {
            airInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == flueGasOutlet) {
            flueGasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == heatedInlet) {
            heatedInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == heatedOutlet) {
            heatedOutlet = null;
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
         if (fuelInlet == null || flueGasOutlet == null || airInlet == null || heatedInlet == null || heatedOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         if (IsBalanceCalcReady() && IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }
               
      private void Solve() {
      }

      protected FiredHeater(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFiredHeater", typeof(int));
         if (persistedClassVersion == 1) {
            this.fuelInlet = info.GetValue("FuelInlet", typeof(FuelStream)) as FuelStream;
            this.airInlet = info.GetValue("AirInlet", typeof(DryingGasStream)) as DryingGasStream;
            this.flueGasOutlet = info.GetValue("FlueGasOutlet", typeof(DryingGasStream)) as DryingGasStream;
            this.heatedInlet = info.GetValue("HeatedInlet", typeof(DryingGasStream)) as DryingGasStream;
            this.heatedOutlet = info.GetValue("HeatedOutlet", typeof(DryingGasStream)) as DryingGasStream;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFiredHeater", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("FuelInlet", this.fuelInlet, typeof(FuelStream));
         info.AddValue("AirInlet", this.airInlet, typeof(DryingGasStream));
         info.AddValue("FlueGasOutlet", this.flueGasOutlet, typeof(DryingGasStream));
         info.AddValue("HeatedInlet", this.heatedInlet, typeof(DryingGasStream));
         info.AddValue("HeatedOutlet", this.heatedOutlet, typeof(DryingGasStream));
      }
   }
}

