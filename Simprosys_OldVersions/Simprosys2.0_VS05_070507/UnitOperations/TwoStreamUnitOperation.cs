using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations {
   /// <summary>
   /// Summary description TwoStreamUnitOperation
   /// </summary>
   [Serializable]
   public abstract class TwoStreamUnitOperation : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public static int INLET_INDEX = 0;
      public static int OUTLET_INDEX = 1;
      
      protected ProcessStreamBase inlet;
      protected ProcessStreamBase outlet;

      //protected ProcessVarDouble pressureDrop;
      #region public properties
      public ProcessStreamBase Inlet {
         get { return inlet; }
      }
      
      public ProcessStreamBase Outlet {
         get { return outlet; }
      }
      #endregion

      protected TwoStreamUnitOperation(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanConnect(int streamIndex) {
         bool retValue = false;
         if (streamIndex == INLET_INDEX && inlet == null) {
            retValue = true;
         }
         else if (streamIndex == OUTLET_INDEX && outlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      protected bool IsStreamValid(ProcessStreamBase ps, int streamIndex) {
         if ((streamIndex == INLET_INDEX && ps.DownStreamOwner != null) 
            || (streamIndex == OUTLET_INDEX && ps.UpStreamOwner != null)) {
            return false;
         }
         else {
            return true;
         }
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (!IsStreamValid(ps, streamIndex)) {
            return false;
         }
         bool canAttach = false;
         
         if (ps is DryingGasStream || ps is ProcessStream) {
            if (streamIndex == INLET_INDEX && inlet == null) {
               if (outlet == null || (outlet != null && outlet.GetType() == ps.GetType())) {
                  canAttach = true;
               }
            }
            else if (streamIndex == OUTLET_INDEX && outlet == null) {
               if (inlet == null || (inlet != null && inlet.GetType() == ps.GetType())) {
                  canAttach = true;
               }
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
         else if (streamIndex == OUTLET_INDEX) {
            outlet = ps;
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
            inletStreams.Remove(ps);
            inlet = null;
            ps.DownStreamOwner = null;
         }
         else if (ps == outlet) {
            outletStreams.Remove(ps);
            outlet = null;
            ps.UpStreamOwner = null;
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
         if (inlet == null || outlet == null) {
            isReady = false;
         }
         return isReady;
      }

      protected TwoStreamUnitOperation(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionTwoStreamUnitOperation", typeof(int));
         if (persistedClassVersion == 1) {
            this.inlet = info.GetValue("Inlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.outlet = info.GetValue("Outlet", typeof(ProcessStreamBase)) as ProcessStreamBase; 
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionTwoStreamUnitOperation", TwoStreamUnitOperation.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Inlet", this.inlet, typeof(ProcessStreamBase));
         info.AddValue("Outlet", this.outlet, typeof(ProcessStreamBase));
      }
   }
}

