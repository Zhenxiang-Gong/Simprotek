using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.Drying {
   
   /// <summary>
   /// Summary description for DryerRatingModel.
   /// </summary>
   [Serializable]
   public abstract class DryerRatingModel : Storable{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected Dryer owner;
      protected ArrayList procVarList = new ArrayList();
      protected FlowDirection flowDirection;
      protected ProcessVarDouble gasVelocity;

      #region public properties
      internal ArrayList ProcVarList 
      {
         get {return procVarList;}
      }

      public FlowDirection FlowDirection {
         get {return flowDirection;}
      }

      public ProcessVarDouble GasVelocity 
      {
         get { return gasVelocity; }
      }

      #endregion      
 
      protected DryerRatingModel(Dryer dryer) 
      {
         this.owner = dryer;
         flowDirection = FlowDirection.Counter;
         gasVelocity = new ProcessVarDouble(StringConstants.GAS_VELOCITY, PhysicalQuantity.Velocity, 2.0, VarState.Specified, dryer);
      }

      protected virtual void InitializeVarListAndRegisterVars() 
      {
         procVarList.Add(gasVelocity);
      }

      public ErrorMessage SpecifyFlowDirection (FlowDirection aValue) 
      {
         if (aValue != flowDirection) {   
            flowDirection = aValue;
            owner.HasBeenModified(true);
         }
         return null;
      }
      
      public virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         return null;
      }
      
      internal virtual void PrepareGeometry() {
      }

      public virtual bool IsRatingCalcReady() {
         return true;
      }

      protected DryerRatingModel(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryerRatingModel", typeof(int));
         if (persistedClassVersion == 1) {
            this.owner = info.GetValue("Owner", typeof(Dryer)) as Dryer;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;
            this.flowDirection = (FlowDirection) info.GetValue("FlowDirection", typeof(FlowDirection));
            this.gasVelocity = (ProcessVarDouble) RecallStorableObject("GasVelocity", typeof(ProcessVarDouble));
         }
      }
      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryerRatingModel", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Owner", this.owner, typeof(Dryer));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         info.AddValue("FlowDirection", this.flowDirection, typeof(FlowDirection));
         info.AddValue("GasVelocity", this.gasVelocity, typeof(ProcessVarDouble));
      }
   }
}

