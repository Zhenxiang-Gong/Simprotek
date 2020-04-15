using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   /// <summary>
   /// Summary description for HXRatingModel.
   /// </summary>
   [Serializable]
   public abstract class HXRatingModel : Storable{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      protected HeatExchanger owner;
      protected ArrayList procVarList = new ArrayList();

      protected FlowDirectionType flowDirection;
      protected ProcessVarDouble coldSideHeatTransferCoefficient;
      protected ProcessVarDouble coldSideFoulingFactor;
      protected ProcessVarDouble hotSideHeatTransferCoefficient;
      protected ProcessVarDouble hotSideFoulingFactor;
      protected ProcessVarDouble totalHeatTransferCoefficient;
      protected ProcessVarDouble totalHeatTransferArea;
      protected ProcessVarDouble numberOfHeatTransferUnits; // NTU = UA/Cmin, Cmin = Min (m1*Cp1, m2*Cp2)
      protected ProcessVarDouble exchangerEffectiveness; // P = (hotOut - hotIn)/(coldIn - hotIn)

      protected ProcessVarDouble coldSideRe;
      protected ProcessVarDouble hotSideRe;
      
      protected bool includeWallEffect;
      protected ProcessVarDouble wallThermalConductivity;
      protected ProcessVarDouble wallThickness;

      #region public properties
      internal ArrayList ProcVarList 
      {
         get {return procVarList;}
      }

      public ProcessVarDouble ColdSideHeatTransferCoefficient {
         get { return coldSideHeatTransferCoefficient; }
      }
      
      public ProcessVarDouble HotSideHeatTransferCoefficient {
         get { return hotSideHeatTransferCoefficient; }
      }
      
      public ProcessVarDouble ColdSideFoulingFactor {
         get { return coldSideFoulingFactor; }
      }
      
      public ProcessVarDouble HotSideFoulingFactor {
         get { return hotSideFoulingFactor; }
      }
      
      public ProcessVarDouble TotalHeatTransferCoefficient {
         get { return totalHeatTransferCoefficient; }
      }
      
      public ProcessVarDouble TotalHeatTransferArea {
         get { return totalHeatTransferArea; }
      }

      public ProcessVarDouble NumberOfHeatTransferUnits {
         get { return numberOfHeatTransferUnits; }
      }
      
      public ProcessVarDouble ExchangerEffectiveness {
         get { return exchangerEffectiveness; }
      }
      
      public FlowDirectionType FlowDirection {
         get {return flowDirection;}
      }

      public bool IncludeWallEffect 
      {
         get {return includeWallEffect;}
      }

      public ProcessVarDouble WallThermalConductivity 
      {
         get {return wallThermalConductivity;}
      }

      public ProcessVarDouble WallThickness 
      {
         get {return wallThickness;}
      }

      public ProcessVarDouble HotSideRe 
      {
         get {return hotSideRe;}
      }

      public ProcessVarDouble ColdSideRe 
      {
         get {return coldSideRe;}
      }

      internal HeatExchanger Owner 
      {
         get {return owner;}
      }
      #endregion      
 
      protected HXRatingModel(HeatExchanger heatExchanger) {
         this.owner = heatExchanger;
         flowDirection = FlowDirectionType.Counter;
         hotSideHeatTransferCoefficient = new ProcessVarDouble(StringConstants.HOT_SIDE_HEAT_TRANSFER_COEFFICIENT, PhysicalQuantity.HeatTransferCoefficient, VarState.Specified, owner);
         coldSideHeatTransferCoefficient = new ProcessVarDouble(StringConstants.COLD_SIDE_HEAT_TRANSFER_COEFFICIENT, PhysicalQuantity.HeatTransferCoefficient, VarState.Specified, owner);
         hotSideFoulingFactor = new ProcessVarDouble(StringConstants.HOT_SIDE_FOULING_FACTOR, PhysicalQuantity.FoulingFactor, 0.0, VarState.Specified, owner);
         coldSideFoulingFactor = new ProcessVarDouble(StringConstants.COLD_SIDE_FOULING_FACTOR, PhysicalQuantity.FoulingFactor, 0.0, VarState.Specified, owner);
         totalHeatTransferCoefficient = new ProcessVarDouble(StringConstants.TOTAL_HEAT_TRANSFER_COEFFICIENT, PhysicalQuantity.HeatTransferCoefficient, VarState.Specified, owner);
         totalHeatTransferArea = new ProcessVarDouble(StringConstants.TOTAL_HEAT_TRANSFER_AREA, PhysicalQuantity.Area, VarState.Specified, owner);
         numberOfHeatTransferUnits = new ProcessVarDouble(StringConstants.NUMBER_OF_HEAT_TRANSFER_UNITS, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, owner);
         exchangerEffectiveness = new ProcessVarDouble(StringConstants.EXCHANGER_EFFECTIVENESS, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, owner);
         includeWallEffect = false;
         wallThermalConductivity = new ProcessVarDouble(StringConstants.WALL_THERMAL_CONDUCTIVITY, PhysicalQuantity.ThermalConductivity, 17.5, VarState.Specified, owner);
         wallThickness = new ProcessVarDouble(StringConstants.WALL_THICKNESS, PhysicalQuantity.SmallLength, 0.002, VarState.Specified, heatExchanger);
         
         hotSideRe = new ProcessVarDouble(StringConstants.HOT_SIDE_RE, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         coldSideRe = new ProcessVarDouble(StringConstants.COLD_SIDE_RE, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
      }

      protected virtual void InitializeVarListAndRegisterVars() {
         procVarList.Add(hotSideHeatTransferCoefficient);
         procVarList.Add(coldSideHeatTransferCoefficient);
         procVarList.Add(hotSideFoulingFactor);
         procVarList.Add(coldSideFoulingFactor);
         procVarList.Add(totalHeatTransferCoefficient);
         procVarList.Add(totalHeatTransferArea);
         procVarList.Add(numberOfHeatTransferUnits);
         procVarList.Add(exchangerEffectiveness);
         
         procVarList.Add(hotSideRe);
         procVarList.Add(coldSideRe);

         procVarList.Add(wallThermalConductivity);
         procVarList.Add(wallThickness);
         
         //owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      public ErrorMessage SpecifyFlowDirection(FlowDirectionType aValue) {
         ErrorMessage retMsg = null;
         if (aValue != flowDirection) 
         {   
            FlowDirectionType oldValue = flowDirection;
            flowDirection = aValue;
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               flowDirection = oldValue;
               retMsg = owner.HandleException(e);
            }
         }
         return retMsg;
      }

      public ErrorMessage SpecifyIncludeWallEffect(bool aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != includeWallEffect) 
         {
            bool oldValue = includeWallEffect;
            includeWallEffect = aValue;
            
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               includeWallEffect = oldValue;
               retMsg = owner.HandleException(e);
            }
         }
         return retMsg;
      }

      internal virtual void SwitchToMe() 
      {
         owner.HotSidePressureDrop.Name = StringConstants.HOT_SIDE_PRESSURE_DROP;
         owner.ColdSidePressureDrop.Name = StringConstants.COLD_SIDE_PRESSURE_DROP;
      }

      internal virtual void PostRating() 
      {
         CalculateHTUAndExchEffectiveness();
      }

      internal virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         ErrorMessage retValue = null;
         if (pv.Type == PhysicalQuantity.HeatTransferCoefficient)
         {
            if (aValue <= 0.0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv.Type == PhysicalQuantity.FoulingFactor) 
         {
            if (aValue < 0.0) {
               retValue = owner.CreateLessThanZeroErrorMessage(pv);
            }
         }
         return retValue;
      }
      
      internal virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarInt pv, int aValue) {
         return null;
      }
      
      internal virtual void PrepareGeometry() {
      }

      public virtual bool IsRatingCalcReady() {
         return true;
      }

      public virtual bool IsParallelFlow() 
      {
         return flowDirection == FlowDirectionType.Parallel;
      }

      //Calculate hot side heat transfer coefficient
      public abstract double GetHotSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate);
      
      //Calculate hot side heat transfer coefficient
      public virtual double GetHotSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) {
         return 0.0;
      }
      
      //Calculate hot side heat transfer coefficient
      public abstract double GetColdSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate);
      
      //Calculate hot side heat transfer coefficient
      public virtual double GetColdSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) {
         return 0.0;
      }

      //Calculate hot side heat transfer coefficient
      public virtual double GetHotSideCondensingHeatTransferCoeff (double temperature, double pressure, double massFlowRate, double inVapQuality, double outVapQuality) {
         return 0.0;
      }
      
      //Calculate hot side heat transfer coefficient
      public virtual double GetColdSideBoilingHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate, double heatFlux) {
         return 0.0;
      }
      
      //Calculate hot side heat transfer coefficient
      public abstract double GetHotSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate);
      
      //Calculate hot side heat transfer coefficient
      public virtual double GetHotSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate){
         return 0.0;
      }

      //Calculate hot side heat transfer coefficient
      public abstract double GetColdSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate);

      //Calculate hot side heat transfer coefficient
      public virtual double GetColdSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate) {
         return 0.0;
      }
      
      //Calculate hot side heat transfer coefficient
      public virtual double GetHotSideCondensingPressureDrop (double tBulk, double tWall, double pressure, double massFlowRate, double inletVaporQuality, double outletVaporQuality) {
         return 0.0;
      }

      //Calculate hot side heat transfer coefficient
      public virtual double GetColdSideBoilingPressureDrop (double tBulk, double tWall, double pressure, double massFlowRate, double inletVaporQuality, double outletVaporQuality) {
         return 0.0;
      }

      //Calculate the correction factor given the shell passes and temperatures:
      public virtual double GetFtFactor (double hotIn, double hotOut, double coldIn, double coldOut) {
         return 1.0;
      }

      public virtual double GetTotalHeatTransferCoeff(double htcHot, double htcCold) 
      {
         double tempValue = 1.0/htcHot + 1.0/htcCold + hotSideFoulingFactor.Value + coldSideFoulingFactor.Value;
         if (includeWallEffect && wallThickness.HasValue && wallThermalConductivity.HasValue) 
         {
            tempValue = tempValue + wallThickness.Value/wallThermalConductivity.Value;
         }
         return 1.0/tempValue;
      }

      public virtual double GetHotSideWallTemperature(double htcHot, double tHot, double htcCold, double tCold) 
      {
         double rTotal = 1.0/GetTotalHeatTransferCoeff (htcHot, htcCold);
         double tHotWall = tHot - 1.0/htcHot/rTotal * (tHot - tCold);

         return tHotWall;
      }
      
      public virtual double GetColdSideWallTemperature(double htcHot, double tHot, double htcCold, double tCold) 
      {
         double rTotal = 1.0/GetTotalHeatTransferCoeff (htcHot, htcCold);
         double tColdWall = tCold + 1.0/htcCold/rTotal * (tHot - tCold);

         return tColdWall;
      }
      
      protected void CalculateHTUAndExchEffectiveness() 
      {
         //number of heat tranfer units
         //Chemical Equipment Chapter 8, page 179
         double cpCold = MathUtility.Average(owner.ColdSideInlet.SpecificHeat.Value, owner.ColdSideOutlet.SpecificHeat.Value);
         double cpHot = MathUtility.Average(owner.HotSideInlet.SpecificHeat.Value, owner.HotSideOutlet.SpecificHeat.Value);
         double mcCold = owner.ColdSideInlet.MassFlowRate.Value*cpCold;
         double mcHot = owner.HotSideInlet.MassFlowRate.Value*cpHot;
         //double massFlowCold = coldSideInlet.MassFlowRate.Value;
         //double massFlowHot = hotSideInlet.MassFlowRate.Value;
         double mcMin = mcCold < mcHot ? mcCold : mcHot;
         //double htArea = totalHeatTransferArea.Value;
         double totalHtc = totalHeatTransferCoefficient.Value;
         double totalHeat = owner.TotalHeatTransfer.Value;
         
         double tColdIn = owner.ColdSideInlet.Temperature.Value;
         double tColdOut = owner.ColdSideOutlet.Temperature.Value;
         double tHotIn = owner.HotSideInlet.Temperature.Value;
         double tHotOut = owner.HotSideOutlet.Temperature.Value;
         //hCold = coldSideHeatTransferCoefficient.Value;
         //hHot = hotSideHeatTransferCoefficient.Value;

         double lmtd = CalculateLmtd(tHotIn, tHotOut, tColdIn, tColdOut);
         
         double ft = GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         if (this is HXRatingModelShellAndTube) 
         {
            HXRatingModelShellAndTube model = this as HXRatingModelShellAndTube;
            owner.Calculate(model.FtFactor, ft);
         }

         if (Math.Abs(lmtd) > 1.0e-8) {
            double ua = totalHeat/(ft*lmtd);
            //owner.Calculate(totalHeatTransferArea, totalArea);
            double ntu = ua/mcMin;
            double c = mcCold < mcHot ? mcCold/mcHot : mcHot/mcCold;
            double p;
            if (IsParallelFlow()) {
               p = (1.0 - Math.Exp(-ntu*(1.0+c)))/(1.0+c);
            }
            else {
               p = (1.0 - Math.Exp(-ntu*(1.0-c)))/(1.0-c*Math.Exp(-ntu*(1.0-c)));
            }
            
            owner.Calculate(numberOfHeatTransferUnits, ntu);
            owner.Calculate(exchangerEffectiveness, p);
         }
      }

      public double CalculateLmtd(double tHotIn, double tHotOut, double tColdIn, double tColdOut) {
         double lmtd = 0.0;
         double deltaT1 = 0.0;
         double deltaT2 = 0.0;
         if (flowDirection == FlowDirectionType.Parallel) {
            deltaT1 = tHotIn - tColdIn;
            deltaT2 = tHotOut - tColdOut;
         }
         else if (flowDirection == FlowDirectionType.Counter) {
            deltaT1 = tHotIn - tColdOut;
            deltaT2 = tHotOut - tColdIn;
         }

         if (Math.Abs(deltaT1 - deltaT2) < 1.0e-8) {
            lmtd = deltaT1;
         }
         else if (deltaT1 != 0.0 && deltaT2 != 0.0) {
            lmtd = (deltaT1 - deltaT2)/Math.Log(deltaT1/deltaT2);
         }

         return lmtd;
      }

      protected HXRatingModel(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHXRatingModel", typeof(int));
         if (persistedClassVersion == 1) {
            this.owner = info.GetValue("Owner", typeof(HeatExchanger)) as HeatExchanger;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;
            this.flowDirection = (FlowDirectionType) info.GetValue("FlowDirection", typeof(FlowDirectionType));
            this.coldSideHeatTransferCoefficient = RecallStorableObject("ColdSideHeatTransferCoefficient", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.hotSideHeatTransferCoefficient = RecallStorableObject("HotSideHeatTransferCoefficient", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coldSideFoulingFactor = RecallStorableObject("ColdSideFoulingFactor", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.hotSideFoulingFactor = RecallStorableObject("HotSideFoulingFactor", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.includeWallEffect = (bool) info.GetValue("IncludeWallEffect", typeof(bool));
            this.wallThermalConductivity = RecallStorableObject("WallThermalConductivity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.wallThickness = RecallStorableObject("WallThickness", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.totalHeatTransferCoefficient = RecallStorableObject("TotalHeatTransferCoefficient", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.totalHeatTransferArea = RecallStorableObject("TotalHeatTransferArea", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.numberOfHeatTransferUnits = RecallStorableObject("NumberOfHeatTransferUnits", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.exchangerEffectiveness = RecallStorableObject("ExchangerEffectiveness", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.hotSideRe = RecallStorableObject("HotSideRe", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coldSideRe = RecallStorableObject("ColdSideRe", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionHXRatingModel", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Owner", this.owner, typeof(HeatExchanger));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         info.AddValue("FlowDirection", this.flowDirection, typeof(FlowDirectionType));
         info.AddValue("ColdSideHeatTransferCoefficient", this.coldSideHeatTransferCoefficient, typeof(ProcessVarDouble));
         info.AddValue("HotSideHeatTransferCoefficient", this.hotSideHeatTransferCoefficient, typeof(ProcessVarDouble));
         info.AddValue("ColdSideFoulingFactor", this.coldSideFoulingFactor, typeof(ProcessVarDouble));
         info.AddValue("HotSideFoulingFactor", this.hotSideFoulingFactor, typeof(ProcessVarDouble));
         info.AddValue("IncludeWallEffect", this.includeWallEffect, typeof(bool));
         info.AddValue("WallThermalConductivity", this.wallThermalConductivity, typeof(ProcessVarDouble));
         info.AddValue("WallThickness", this.wallThickness, typeof(ProcessVarDouble));
         info.AddValue("TotalHeatTransferCoefficient", this.totalHeatTransferCoefficient, typeof(ProcessVarDouble));
         info.AddValue("TotalHeatTransferArea", this.totalHeatTransferArea, typeof(ProcessVarDouble));
         info.AddValue("NumberOfHeatTransferUnits", this.numberOfHeatTransferUnits, typeof(ProcessVarDouble));
         info.AddValue("ExchangerEffectiveness", this.exchangerEffectiveness, typeof(ProcessVarDouble));

         info.AddValue("HotSideRe", this.hotSideRe, typeof(ProcessVarDouble));
         info.AddValue("ColdSideRe", this.coldSideRe, typeof(ProcessVarDouble));
      }
   }
}

