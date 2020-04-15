using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.Drying {

   /// <summary>
   /// Summary description for Dryer.
   /// </summary>
   [Serializable]
   public class LiquidDryer : Dryer {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      protected LiquidDryerType dryerType;

      //private DryerRatingModel currentRatingModel;

      //private Hashtable ratingModelTable = new Hashtable();
 
      #region public properties
      public LiquidDryerType DryerType {
         get { return dryerType; }
      }
      #endregion
   
      //public Dryer(string name, ProcessType processType, UnitOpSystem uoSys) : base(name, uoSys) 
      public LiquidDryer(string name, UnitOpSystem uoSys) : base(name, uoSys) 
      {
         dryerType = LiquidDryerType.Unknown;
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

      protected LiquidDryer(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionLiquidDryer", typeof(int));
         if (persistedClassVersion == 1) {
            this.dryerType = (LiquidDryerType) info.GetValue("DryerType", typeof(LiquidDryerType));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionLiquidDryer", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("DryerType", this.dryerType, typeof(LiquidDryerType));
      }
   }
}
