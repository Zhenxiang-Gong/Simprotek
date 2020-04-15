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
      public static int FEED_INLET_INDEX = 0;
      public static int PRODUCT_OUTLET_INDEX = 2;

      private ProcessStreamBase fuelInlet;
      private ProcessStreamBase airInlet;
      private ProcessStreamBase flueGasOutlet;
      private ProcessStreamBase feedInlet;
      private ProcessStreamBase productOutlet;

      #region public properties
      public ProcessStreamBase FuelInlet {
         get { return fuelInlet; }
      }

      public ProcessStreamBase AirInlet {
         get { return airInlet; }
      }

      public ProcessStreamBase FlueGasOutlet {
         get { return flueGasOutlet; }
      }

      public ProcessStreamBase FeedInlet {
         get { return feedInlet; }
      }

      public ProcessStreamBase ProductOutlet {
         get { return productOutlet; }
      }
      #endregion

      public FiredHeater(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanConnect(int streamIndex) {
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
         else if (streamIndex == FEED_INLET_INDEX && feedInlet == null) {
            retValue = true;
         }
         else if (streamIndex == PRODUCT_OUTLET_INDEX && productOutlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         bool canAttach = false;
         
         if (streamIndex == FUEL_INLET_INDEX && fuelInlet == null && ps.DownStreamOwner == null) {
         }
         else if (streamIndex == AIR_INLET_INDEX && airInlet == null && ps.UpStreamOwner == null) {
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX && flueGasOutlet == null && ps.UpStreamOwner == null) {
         }
         else if (streamIndex == FEED_INLET_INDEX && feedInlet == null && ps.DownStreamOwner == null) {
         }
         else if (streamIndex == PRODUCT_OUTLET_INDEX && productOutlet == null && ps.UpStreamOwner == null) {
         }

         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == FUEL_INLET_INDEX) {
            fuelInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == AIR_INLET_INDEX) {
            airInlet = ps;
            ps.UpStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX) {
            flueGasOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == FEED_INLET_INDEX) {
            feedInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == PRODUCT_OUTLET_INDEX) {
            productOutlet = ps;
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
         else if (ps == flueGasOutlet) {
            flueGasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == airInlet) {
            airInlet = null;
            ps.UpStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == feedInlet) {
            feedInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == productOutlet) {
            productOutlet = null;
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
         if (fuelInlet == null || flueGasOutlet == null || airInlet == null || feedInlet == null || productOutlet == null) {
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
         if (IsSolveReady()) {
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
            this.fuelInlet = info.GetValue("FuelInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.airInlet = info.GetValue("AirOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.flueGasOutlet = info.GetValue("FlueGasOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.feedInlet = info.GetValue("FeedInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.productOutlet = info.GetValue("ProductOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFiredHeater", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("FuelInlet", this.fuelInlet, typeof(ProcessStreamBase));
         info.AddValue("AirInlet", this.airInlet, typeof(ProcessStreamBase));
         info.AddValue("FlueGasOutlet", this.flueGasOutlet, typeof(ProcessStreamBase));
         info.AddValue("FeedInlet", this.feedInlet, typeof(ProcessStreamBase));
         info.AddValue("ProductOutlet", this.productOutlet, typeof(ProcessStreamBase));
      }
   }
}

