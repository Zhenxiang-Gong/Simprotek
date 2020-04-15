using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.Drying {

   /// <summary>
   /// Summary description for Dryer.
   /// </summary>
   [Serializable]
   public class SolidDryer : Dryer {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      protected SolidDryerType dryerType;
      protected FlowDirection flowDirection;

      //private DryerRatingModel currentRatingModel;

      //private Hashtable ratingModelTable = new Hashtable();
 
      #region public properties
      public SolidDryerType DryerType {
         get { return dryerType; }
      }
      
//      public FlowDirection FlowDirection 
//      {
//         get {return flowDirection;}
//      }
//
//      public ProcessType ProcessType 
//      {
//         get { return processType; }
//      }
      #endregion
   
      //public Dryer(string name, ProcessType processType, UnitOpSystem uoSys) : base(name, uoSys) 
      public SolidDryer(string name, UnitOpSystem uoSys) : base(name, uoSys) 
      {
         dryerType = SolidDryerType.Unknown;
      }

      protected override void CreateCalculationModel(DryerCalculationType dryerCalculationType) 
      {
         if (dryerCalculationType == DryerCalculationType.Scoping && scopingModel == null) 
         {
            scopingModel = new DryerScopingModel(this);
         }
//         else if (dryerCalculationType == DryerCalculationType.Rating) {
//            CreateRatingModel(dryerType);
//         }
//         else if (dryerCalculationType == DryerCalculationType.ScalingUp) {
//         }
      }

//      private void CreateRatingModel(SolidDryerType dryerType) {
//      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         return retValue;
      }

      protected SolidDryer(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolidDryer", typeof(int));
         if (persistedClassVersion == 1) {
            this.dryerType = (SolidDryerType) info.GetValue("DryerType", typeof(SolidDryerType));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionSolidDryer", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("DryerType", this.dryerType, typeof(SolidDryerType));
      }
   }
}
