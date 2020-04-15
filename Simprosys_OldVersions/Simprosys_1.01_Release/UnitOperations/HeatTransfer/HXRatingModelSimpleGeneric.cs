using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   /// <summary>
   /// Summary description for HXRatingModelSimpleGeneric.
   /// </summary>
   [Serializable]
   public class HXRatingModelSimpleGeneric : HXRatingModel {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public HXRatingModelSimpleGeneric(HeatExchanger heatExchanger) : base(heatExchanger) {
         InitializeVarListAndRegisterVars();
      }
                                                                                                  
      protected override void InitializeVarListAndRegisterVars() {
         base.InitializeVarListAndRegisterVars();
         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }


      public override bool IsRatingCalcReady() {
         bool isReady = false;
         if (hotSideHeatTransferCoefficient.HasValue && coldSideHeatTransferCoefficient.HasValue
            && totalHeatTransferArea.HasValue && owner.HotSidePressureDrop.HasValue 
            && owner.ColdSidePressureDrop.HasValue)  
         {
            if (owner.HotSideInlet.MassFlowRate.HasValue && owner.ColdSideInlet.MassFlowRate.HasValue &&
               ((owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue) 
               || (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue)
               )) 
            {
               isReady = true;
            }
         }
         return isReady;
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) {
         return hotSideHeatTransferCoefficient.Value;
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) {
         return hotSideHeatTransferCoefficient.Value;
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) {
         return coldSideHeatTransferCoefficient.Value;
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) {
         return coldSideHeatTransferCoefficient.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideCondensingHeatTransferCoeff (double temperature, double pressure, double massFlowRate, double inVapQuality, double outVapQuality) {
         return hotSideHeatTransferCoefficient.Value;
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideBoilingHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate, double heatFlux) {
         return coldSideHeatTransferCoefficient.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) {
         return owner.HotSidePressureDrop.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate) {
         return owner.HotSidePressureDrop.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) {
         return owner.ColdSidePressureDrop.Value;
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate) {
         return owner.ColdSidePressureDrop.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideCondensingPressureDrop (double tBulk, double tWall, double pressure, double massFlowRate, double inletVaporQuality, double outletVaporQuality) {
         return owner.HotSidePressureDrop.Value;
      }
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideBoilingPressureDrop (double tBulk, double tWall, double pressure, double massFlowRate, double inletVaporQuality, double outletVaporQuality) {
         return owner.ColdSidePressureDrop.Value;
      }

      protected HXRatingModelSimpleGeneric (SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHXRatingModelSimpleGeneric", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionHXRatingModelSimpleGeneric", CLASS_PERSISTENCE_VERSION, typeof(int));
      }

   }
}

