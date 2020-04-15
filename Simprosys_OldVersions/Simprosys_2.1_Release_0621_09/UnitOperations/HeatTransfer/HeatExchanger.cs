using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {

   public delegate void HXHotSideChangedEventHandler(HeatExchanger hx);

   public enum ExchangerType { SimpleGeneric = 0, ShellAndTube, PlateAndFrame };

   /// <summary>
   /// Summary description for HeatExchanger.
   /// </summary>
   [Serializable]
   public class HeatExchanger : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static int COLD_SIDE_INLET_INDEX = 0;
      public static int COLD_SIDE_OUTLET_INDEX = 1;
      public static int HOT_SIDE_INLET_INDEX = 2;
      public static int HOT_SIDE_OUTLET_INDEX = 3;

      private static string RATING_CALC_FAILED = "Heat exchanger rating calculation failed.";

      protected ProcessStreamBase hotSideInlet;
      protected ProcessStreamBase hotSideOutlet;
      protected ProcessStreamBase coldSideInlet;
      protected ProcessStreamBase coldSideOutlet;

      protected ProcessVarDouble totalHeatTransfer;
      protected ProcessVarDouble coldSidePressureDrop;
      protected ProcessVarDouble hotSidePressureDrop;

      //protected CurveF hotSideHeatCurve;
      //protected CurveF coldSideHeatCurve;

      private ExchangerType exchangerType;

      private HXRatingModel currentRatingModel;

      private Hashtable ratingModelTable = new Hashtable();
      public event HXHotSideChangedEventHandler HXHotSideChanged;

      //private double totalHeat, hHotIn, hHotOut, hColdIn, hColdOut, coldSideMassFlow, hotSideMassFlow;
      private double tColdIn, tColdOut, tHotIn, tHotOut, tEvapHotIn, tEvapColdIn, tEvapHotOut, tEvapColdOut, vfHotIn, vfHotOut, vfColdIn, vfColdOut;
      private ProcessVarDouble hHotIn, hHotOut, hColdIn, hColdOut, coldSideMassFlow, hotSideMassFlow;

      #region public properties
      public ProcessStreamBase ColdSideInlet {
         get { return coldSideInlet; }
      }

      public ProcessStreamBase ColdSideOutlet {
         get { return coldSideOutlet; }
      }

      public ProcessStreamBase HotSideInlet {
         get { return hotSideInlet; }
      }

      public ProcessStreamBase HotSideOutlet {
         get { return hotSideOutlet; }
      }

      public ProcessVarDouble TotalHeatTransfer {
         get { return totalHeatTransfer; }
      }

      public ProcessVarDouble ColdSidePressureDrop {
         get { return coldSidePressureDrop; }
      }

      public ProcessVarDouble HotSidePressureDrop {
         get { return hotSidePressureDrop; }
      }

      public ExchangerType ExchangerType {
         get { return exchangerType; }
      }

      public HXRatingModel CurrentRatingModel {
         get { return currentRatingModel; }
      }

      #endregion

      public HeatExchanger(string name, UnitOperationSystem uoSys)
         : base(name, uoSys) {
         totalHeatTransfer = new ProcessVarDouble(StringConstants.TOTAL_HEAT_TRANSFER, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);
         hotSidePressureDrop = new ProcessVarDouble(StringConstants.HOT_SIDE_PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         coldSidePressureDrop = new ProcessVarDouble(StringConstants.COLD_SIDE_PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         //exchangerType = ExchangerType.SimpleGeneric;
         //CreateRatingModel(exchangerType);

         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         //base.InitializeVarListAndRegisterVars();
         AddVarOnListAndRegisterInSystem(totalHeatTransfer);
         AddVarOnListAndRegisterInSystem(hotSidePressureDrop);
         AddVarOnListAndRegisterInSystem(coldSidePressureDrop);
      }

      public override bool CanAttach(int streamIndex) {
         bool retValue = false;
         if (streamIndex == COLD_SIDE_INLET_INDEX && coldSideInlet == null) {
            retValue = true;
         }
         else if (streamIndex == COLD_SIDE_OUTLET_INDEX && coldSideOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == HOT_SIDE_INLET_INDEX && hotSideInlet == null) {
            retValue = true;
         }
         else if (streamIndex == HOT_SIDE_OUTLET_INDEX && hotSideOutlet == null) {
            retValue = true;
         }

         return retValue;
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (((streamIndex == COLD_SIDE_INLET_INDEX || streamIndex == HOT_SIDE_INLET_INDEX) && ps.DownStreamOwner != null)
            || ((streamIndex == COLD_SIDE_OUTLET_INDEX || streamIndex == HOT_SIDE_OUTLET_INDEX) && ps.UpStreamOwner != null)) {
            return false;
         }
         bool canAttach = false;
         bool isLiquidMaterial = false;

         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }

         if (streamIndex == COLD_SIDE_INLET_INDEX && coldSideInlet == null) {
            if (coldSideOutlet != null && coldSideOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (coldSideOutlet == null && (isLiquidMaterial || ps is WaterStream || ps is DryingGasStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == COLD_SIDE_OUTLET_INDEX && coldSideOutlet == null) {
            if (coldSideInlet != null && coldSideInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (coldSideInlet == null && (isLiquidMaterial || ps is WaterStream || ps is DryingGasStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == HOT_SIDE_INLET_INDEX && hotSideInlet == null) {
            if (hotSideOutlet != null && hotSideOutlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (hotSideOutlet == null && (isLiquidMaterial || ps is WaterStream || ps is DryingGasStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex == HOT_SIDE_OUTLET_INDEX && hotSideOutlet == null) {
            if (hotSideInlet != null && hotSideInlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (hotSideInlet == null && (isLiquidMaterial || ps is WaterStream || ps is DryingGasStream)) {
               canAttach = true;
            }
         }

         return canAttach;
      }

      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == COLD_SIDE_INLET_INDEX) {
            coldSideInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == COLD_SIDE_OUTLET_INDEX) {
            coldSideOutlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == HOT_SIDE_INLET_INDEX) {
            hotSideInlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == HOT_SIDE_OUTLET_INDEX) {
            hotSideOutlet = ps;
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
         if (ps == coldSideInlet) {
            coldSideInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == coldSideOutlet) {
            coldSideOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == hotSideInlet) {
            hotSideInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == hotSideOutlet) {
            hotSideOutlet = null;
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

      public override ErrorMessage SpecifyCalculationType(UnitOpCalculationType aValue) {
         if (aValue == calculationType) {
            return null;
         }

         ErrorMessage retMsg = null;

         //check if material stream's mass concentration is greater then 0
         //if yes, return an error since this is not supported yet
         if (aValue == UnitOpCalculationType.Rating) {
            if (OneSideIsLiquidAndNotPure()) {
               retMsg = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Heat exchanger rating is not supported yet when material stream is not pure water");
               return retMsg;
            }
         }

         UnitOpCalculationType oldValue = calculationType;
         calculationType = aValue;
         EnableBalanceOrRatingModel(calculationType);

         try {
            HasBeenModified(true);
         }
         catch (Exception e) {
            calculationType = oldValue;
            EnableBalanceOrRatingModel(calculationType);
            retMsg = HandleException(e);
         }

         return retMsg;
      }

      private bool OneSideIsLiquidAndNotPure() {
         bool retValue = false;
         if (hotSideInlet is DryingMaterialStream) {
            DryingMaterialStream hotInlet = hotSideInlet as DryingMaterialStream;
            if (hotInlet.MassConcentration.HasValue && hotInlet.MassConcentration.Value > 1.0e-6) {
               retValue = true;
            }
         }

         if (coldSideInlet is DryingMaterialStream) {
            DryingMaterialStream coldInlet = coldSideInlet as DryingMaterialStream;
            if (coldInlet.MassConcentration.HasValue && coldInlet.MassConcentration.Value > 1.0e-6) {
               retValue = true;
            }
         }
         return retValue;
      }

      public ErrorMessage SpecifyExchangerType(ExchangerType aValue) {
         ErrorMessage retMsg = null;
         if (aValue != exchangerType) {
            ExchangerType oldValue = exchangerType;
            exchangerType = aValue;
            try {
               CreateRatingModel(exchangerType);
               HasBeenModified(true);
            }
            catch (Exception e) {
               exchangerType = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (calculationType == UnitOpCalculationType.Balance) {
            if (pv == coldSidePressureDrop || pv == hotSidePressureDrop) {
               if (aValue <= 0.0) {
                  retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
               }
            }
         }
         else if (calculationType == UnitOpCalculationType.Rating) {
            retValue = currentRatingModel.CheckSpecifiedValueRange(pv, aValue);
         }
         return retValue;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarInt pv, int aValue) {
         ErrorMessage retValue = null;
         if (calculationType == UnitOpCalculationType.Rating) {
            retValue = currentRatingModel.CheckSpecifiedValueRange(pv, aValue);
         }
         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == hotSideInlet.Pressure && hotSideOutlet.Pressure.IsSpecifiedAndHasValue && aValue < hotSideOutlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's hot side inlet must be greater than that of the outlet.");
         }
         else if (pv == hotSideOutlet.Pressure && hotSideInlet.Pressure.IsSpecifiedAndHasValue && aValue > hotSideInlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's hot side outlet cannot be greater than that of the inlet.");
         }
         else if (pv == coldSideInlet.Pressure && coldSideOutlet.Pressure.IsSpecifiedAndHasValue && aValue < coldSideOutlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's cold side inlet must be greater than that of the outlet.");
         }
         else if (pv == coldSideOutlet.Pressure && coldSideInlet.Pressure.IsSpecifiedAndHasValue && aValue > coldSideInlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's cold side outlet cannot be greater than that of the inlet.");
         }
         else if (pv == hotSideInlet.Temperature && hotSideOutlet.Temperature.IsSpecifiedAndHasValue && aValue < hotSideOutlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's hot side inlet cannot be smaller than that of the outlet.");
         }
         else if (pv == hotSideOutlet.Temperature && hotSideInlet.Temperature.IsSpecifiedAndHasValue && aValue > hotSideInlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's hot side outlet cannot be greater than that of the inlet.");
         }
         else if (pv == coldSideInlet.Temperature && coldSideOutlet.Temperature.IsSpecifiedAndHasValue && aValue > coldSideOutlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's cold side inlet cannot be greater than that of the outlet.");
         }
         else if (pv == coldSideOutlet.Temperature && coldSideInlet.Temperature.IsSpecifiedAndHasValue && aValue < coldSideInlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's cold side outlet cannot be smaller than that of the inlet.");
         }
         else if (pv == hotSideInlet.Temperature && coldSideOutlet.Temperature.IsSpecifiedAndHasValue && aValue < coldSideOutlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's hot side inlet cannot be smaller than that of the cold side outlet.");
         }
         else if (pv == coldSideOutlet.Temperature && hotSideInlet.Temperature.IsSpecifiedAndHasValue && aValue > hotSideInlet.Temperature.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the heat exchanger's cold side outlet cannot be greater than that of the hot side outlet.");
         }

         return retValue;
      }

      protected override void EnableRatingModel() {
         CreateRatingModel(exchangerType);
      }

      protected override void EnableBalanceModel() {
         if (currentRatingModel != null) {
            RemoveVarsOnListAndUnregisterInSystem(currentRatingModel.ProcVarList);
         }

         HotSidePressureDrop.Name = StringConstants.HOT_SIDE_PRESSURE_DROP;
         ColdSidePressureDrop.Name = StringConstants.COLD_SIDE_PRESSURE_DROP;
      }

      private void CreateRatingModel(ExchangerType type) {
         if (currentRatingModel != null) {
            RemoveVarsOnListAndUnregisterInSystem(currentRatingModel.ProcVarList);
         }

         currentRatingModel = ratingModelTable[type] as HXRatingModel;
         if (currentRatingModel == null) {
            if (type == ExchangerType.SimpleGeneric) {
               currentRatingModel = new HXRatingModelSimpleGeneric(this);
            }
            else if (type == ExchangerType.ShellAndTube) {
               currentRatingModel = new HXRatingModelShellAndTube(this);
               //(currentRatingModel as HXRatingModelShellAndTube).HotSideChanged += new HotSideChangedEventHandler(HeatExchanger_HotSideChanged);
            }
            else if (type == ExchangerType.PlateAndFrame) {
               currentRatingModel = new HXRatingModelPlateAndFrame(this);
            }
            ratingModelTable.Add(type, currentRatingModel);
         }
         else {
            AddVarsOnListAndRegisterInSystem(currentRatingModel.ProcVarList);
         }

         currentRatingModel.SwitchToMe();
      }

      internal void OnHXHotSideChanged() {
         if (HXHotSideChanged != null) {
            HXHotSideChanged(this);
         }
      }

      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (coldSideInlet == null || coldSideOutlet == null || hotSideInlet == null || hotSideOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         if (coldSideMassFlow.HasValue && hotSideMassFlow.HasValue) {
            if ((hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue) || (hColdIn.HasValue && hColdOut.HasValue && hHotOut.HasValue) ||
                 (hColdIn.HasValue && hHotIn.HasValue && hHotOut.HasValue) || (hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue)) {
               isReady = true;
            }
         }
         else if (hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
            if (coldSideMassFlow.HasValue || hotSideMassFlow.HasValue) {
               isReady = true;
            }
         }
         else if (coldSideMassFlow.HasValue) {
            if (hColdIn.HasValue && hColdOut.HasValue) {
               isReady = true;
            }
         }
         else if (hotSideMassFlow.HasValue) {
            if (hHotIn.HasValue && hHotOut.HasValue) {
               isReady = true;
            }
         }

         return isReady;
      }

      private void InitilaizeVariables() {
         hColdIn = coldSideInlet.SpecificEnthalpy;
         hColdOut = coldSideOutlet.SpecificEnthalpy;
         hHotIn = hotSideInlet.SpecificEnthalpy;
         hHotOut = hotSideOutlet.SpecificEnthalpy;
         coldSideMassFlow = coldSideInlet.MassFlowRate;
         hotSideMassFlow = hotSideInlet.MassFlowRate;
      }

      public override void Execute(bool propagate) {
         if (OneSideIsLiquidAndNotPure() && calculationType == UnitOpCalculationType.Rating) {
            throw new InappropriateSpecifiedValueException("Heat exchanger rating is not supported yet when material feed is not pure water");
         }

         PreSolve();

         //balance cold stream flow
         if (coldSideInlet is DryingStream) {

            if (coldSideInlet is DryingMaterialStream) {
               DryingMaterialStream coldInlet = coldSideInlet as DryingMaterialStream;
               DryingMaterialStream coldOutlet = coldSideOutlet as DryingMaterialStream;
               //balance gas stream flow
               BalanceDryingStreamMoistureContent(coldInlet, coldOutlet);
               BalanceDryingMaterialStreamFlow(coldInlet, coldOutlet);
               //BalanceDryingMaterialStreamSpecificHeat(coldInlet, coldOutlet);
               AdjustVarsStates(coldInlet, coldOutlet);
            }
            else if (coldSideInlet is DryingGasStream) {
               DryingGasStream coldInlet = coldSideInlet as DryingGasStream;
               DryingGasStream coldOutlet = coldSideOutlet as DryingGasStream;
               //balance gas stream flow
               BalanceDryingStreamMoistureContent(coldInlet, coldOutlet);
               BalanceDryingGasStreamFlow(coldInlet, coldOutlet);
               AdjustVarsStates(coldInlet, coldOutlet);
            }
         }
         else if (coldSideInlet is WaterStream) {
            BalanceProcessStreamFlow(coldSideInlet, coldSideOutlet);
            AdjustVarsStates(coldSideInlet, coldSideOutlet);
         }

         if (hotSideInlet is DryingMaterialStream) {
            DryingMaterialStream hotInlet = hotSideInlet as DryingMaterialStream;
            DryingMaterialStream hotOutlet = hotSideOutlet as DryingMaterialStream;

            //balance gas stream flow
            BalanceDryingStreamMoistureContent(hotInlet, hotOutlet);
            BalanceDryingMaterialStreamFlow(hotInlet, hotOutlet);
            //BalanceDryingMaterialStreamSpecificHeat(hotInlet, hotOutlet);
            AdjustVarsStates(hotInlet, hotOutlet);
         }
         else if (hotSideInlet is DryingGasStream) {
            DryingGasStream hotInlet = hotSideInlet as DryingGasStream;
            DryingGasStream hotOutlet = hotSideOutlet as DryingGasStream;

            //balance gas stream flow
            BalanceDryingStreamMoistureContent(hotInlet, hotOutlet);
            BalanceDryingGasStreamFlow(hotInlet, hotOutlet);
            AdjustVarsStates(hotInlet, hotOutlet);
         }
         else if (hotSideInlet is WaterStream) {
            //balance hot stream flwow
            BalanceProcessStreamFlow(hotSideInlet, hotSideOutlet);
         }

         //balance cold pressure
         BalancePressure(coldSideInlet, coldSideOutlet, coldSidePressureDrop);
         //balance hot pressure
         BalancePressure(hotSideInlet, hotSideOutlet, hotSidePressureDrop);

         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         UpdateStreamsIfNecessary();

         InitilaizeVariables();
         if (calculationType == UnitOpCalculationType.Balance) {
            if (IsSolveReady()) {
               DoHeatBalanceCalculation();
            }
         }
         else if (calculationType == UnitOpCalculationType.Rating) {
            currentRatingModel.PrepareGeometry();
            if (currentRatingModel.IsRatingCalcReady()) {
               DoRatingCalculation();
            }
            if (HasSolvedAlready) {
               currentRatingModel.PostRating();
            }
         }

         PostSolve();
      }

      protected void DoHeatBalanceCalculation() {
         double specificEnthalpyValue;
         double hotSideMassFlowValue;
         double coldSideMassFlowValue;
         double totalHeat = Constants.NO_VALUE;

         if (coldSideMassFlow.HasValue && hColdIn.HasValue && hColdOut.HasValue) {
            totalHeat = coldSideMassFlow.Value * (hColdOut.Value - hColdIn.Value);
            if (hotSideMassFlow.HasValue) {
               if (hHotIn.HasValue) {
                  specificEnthalpyValue = hHotIn.Value - totalHeat / hotSideMassFlow.Value;
                  Calculate(hotSideOutlet.SpecificEnthalpy, specificEnthalpyValue);
               }
               else if (hHotOut.HasValue) {
                  specificEnthalpyValue = hHotOut.Value + totalHeat / hotSideMassFlow.Value;
                  Calculate(hotSideInlet.SpecificEnthalpy, specificEnthalpyValue);
               }
            }
            else if (hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
               hotSideMassFlowValue = totalHeat / (hHotIn.Value - hHotOut.Value);
               Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlowValue);
               Calculate(hotSideInlet.MassFlowRate, hotSideMassFlowValue);
            }
         }
         else if (hotSideMassFlow.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
            totalHeat = hotSideMassFlow.Value * (hHotIn.Value - hHotOut.Value);
            if (coldSideMassFlow.HasValue) {
               if (hColdIn.HasValue) {
                  specificEnthalpyValue = hColdIn.Value + totalHeat / coldSideMassFlow.Value;
                  Calculate(coldSideOutlet.SpecificEnthalpy, specificEnthalpyValue);
               }
               else if (hColdOut.HasValue) {
                  specificEnthalpyValue = hColdOut.Value - totalHeat / coldSideMassFlow.Value;
                  Calculate(coldSideInlet.SpecificEnthalpy, specificEnthalpyValue);
               }
            }
            else if (hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
               coldSideMassFlowValue = totalHeat / (hColdOut.Value - hColdIn.Value);
               Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlowValue);
               Calculate(coldSideInlet.MassFlowRate, coldSideMassFlowValue);
            }
         }
         else if (hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
            if (coldSideMassFlow.HasValue) {
               totalHeat = coldSideMassFlow.Value * (hColdOut.Value - hColdIn.Value);
               hotSideMassFlowValue = totalHeat / (hHotIn.Value - hHotOut.Value);
               Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlowValue);
               Calculate(hotSideInlet.MassFlowRate, hotSideMassFlowValue);
            }
            else if (hotSideMassFlow.HasValue) {
               totalHeat = hotSideMassFlow.Value * (hHotIn.Value - hHotOut.Value);
               coldSideMassFlowValue = totalHeat / (hColdOut.Value - hColdIn.Value);
               Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlowValue);
               Calculate(coldSideInlet.MassFlowRate, coldSideMassFlowValue);
            }
         }
         else if (hColdIn.HasValue && hColdOut.HasValue && hHotIn.HasValue && hHotOut.HasValue) {
            if (coldSideMassFlow.HasValue) {
               totalHeat = coldSideMassFlow.Value * (hColdOut.Value - hColdIn.Value);
               hotSideMassFlowValue = totalHeat / (hHotIn.Value - hHotOut.Value);
               Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlowValue);
               Calculate(hotSideInlet.MassFlowRate, hotSideMassFlowValue);
            }
            else if (hotSideMassFlow.HasValue) {
               totalHeat = hotSideMassFlow.Value * (hHotIn.Value - hHotOut.Value);
               coldSideMassFlowValue = totalHeat / (hColdOut.Value - hColdIn.Value);
               Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlowValue);
               Calculate(coldSideInlet.MassFlowRate, coldSideMassFlowValue);
            }
         }

         if (totalHeat != Constants.NO_VALUE) {
            Calculate(totalHeatTransfer, totalHeat);
         }

         if (hotSideInlet.MassFlowRate.HasValue && hotSideInlet.SpecificEnthalpy.HasValue && hotSideOutlet.SpecificEnthalpy.HasValue
            && coldSideInlet.MassFlowRate.HasValue && coldSideInlet.SpecificEnthalpy.HasValue && coldSideOutlet.SpecificEnthalpy.HasValue) {
            solveState = SolveState.Solved;
         }

      }

      private void DoRatingCalculation() {
         double htcHot = currentRatingModel.HotSideHeatTransferCoefficient.Value;
         double htcCold = currentRatingModel.HotSideHeatTransferCoefficient.Value;
         double totalHtc = 0.0;
         double totalHeat = 0.0;

         double totalArea = currentRatingModel.TotalHeatTransferArea.Value;

         double coldSideFlowRate = coldSideMassFlow.Value;
         double hotSideFlowRate = hotSideMassFlow.Value;
         tColdIn = coldSideInlet.Temperature.Value;
         tColdOut = coldSideOutlet.Temperature.Value;
         tHotIn = hotSideInlet.Temperature.Value;
         tHotOut = hotSideOutlet.Temperature.Value;

         vfColdIn = coldSideInlet.VaporFraction.Value;
         vfColdOut = coldSideOutlet.VaporFraction.Value;
         vfHotIn = hotSideInlet.VaporFraction.Value;
         vfHotOut = hotSideOutlet.VaporFraction.Value;

         double pHotIn = 0, pHotOut = 0, pColdIn = 0, pColdOut = 0;
         double cpHot, cpCold;
         double evapHeatHot, evapHeatCold;
         double tEvapHot, tEvapCold;

         tEvapHotIn = tEvapHotOut = tEvapColdIn = tEvapColdOut = Constants.NO_VALUE;

         if (hotSideInlet.Pressure.HasValue) {
            pHotIn = hotSideInlet.Pressure.Value;
            tEvapHotIn = GetHotSideBoilingPoint(pHotIn);
            evapHeatHot = GetHotSideEvaporationHeat(tEvapHotIn);
         }
         if (hotSideOutlet.Pressure.HasValue) {
            pHotOut = hotSideOutlet.Pressure.Value;
            tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
            evapHeatHot = GetHotSideEvaporationHeat(tEvapHotOut);
         }

         if (coldSideInlet.Pressure.HasValue) {
            pColdIn = coldSideInlet.Pressure.Value;
            tEvapColdIn = GetColdSideBoilingPoint(pColdIn);
            evapHeatCold = GetColdSideEvaporationHeat(tEvapColdIn);
         }

         if (coldSideOutlet.Pressure.HasValue) {
            pColdOut = coldSideOutlet.Pressure.Value;
            tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
            evapHeatCold = GetColdSideEvaporationHeat(tEvapColdOut);
         }

         if (tEvapColdOut == Constants.NO_VALUE && tEvapColdIn != Constants.NO_VALUE) {
            tEvapColdOut = tEvapColdIn;
         }
         else if (tEvapColdIn == Constants.NO_VALUE && tEvapColdOut != Constants.NO_VALUE) {
            tEvapColdIn = tEvapColdOut;
         }

         if (tEvapHotOut == Constants.NO_VALUE && tEvapHotIn != Constants.NO_VALUE) {
            tEvapHotOut = tEvapHotIn;
         }
         else if (tEvapHotIn == Constants.NO_VALUE && tEvapHotOut != Constants.NO_VALUE) {
            tEvapHotIn = tEvapHotOut;
         }

         tEvapHot = tEvapHotIn;
         tEvapCold = tEvapColdIn;

         double dpHot, dpCold; 
         double subHeatHot, subHeatCold;
         double dpHot1, dpHot2, dpHot3;
         double dpCold1, dpCold2, dpCold3;
         dpHot = dpCold = 0;
         dpHot1 = dpHot2 = dpHot3 = 0;
         dpCold1 = dpCold2 = dpCold3 = 0;
         double subHeat1, subHeat2, subHeat3;
         double subHtc1, subHtc2, subHtc3;
         double htcCold1, htcCold2, htcCold3; 
         double htcHot1, htcHot2, htcHot3;
         htcCold1 = htcCold2 = htcCold3 = Constants.NO_VALUE; 
         htcHot1 = htcHot2 = htcHot3 = Constants.NO_VALUE;
         double subArea1, subArea2, subArea3;
         double heatFlux1, heatFlux2, heatFlux3;

         double heatFlux;
         double tBulkHot, tBulkCold, tWallHot, tWallCold;
         double tHot, tCold;
         double tBulkHot1, tBulkHot2, tBulkHot3, tBulkCold1, tBulkCold2, tBulkCold3;
         double tWallHot1, tWallHot2, tWallHot3, tWallCold1, tWallCold2, tWallCold3;
         double vfHotOut1, vfHotOut2, vfHotOut3, vfColdOut1, vfColdOut2, vfColdOut3;
         double lmtd;

         double totalHeat_Old = 0;
         double tColdIn_Old = 0;
         double tColdOut_Old = 0;
         double tHotIn_Old = 0;
         double tHotOut_Old = 0;
         //double hotSideFlowRate_Old = 0;
         //double coldSideFlowRate_Old = 0;
         double c1, c2, c3, c4, c5, c6;

         double ftFactor = 0.6;
         double ftFactorOld;
         int counter = 0;

         if (hotSideMassFlow.HasValue && coldSideMassFlow.HasValue) {
            
            #region 1.1 hot in and cold in known
            // case 1.1--hot inlet temperature and cold inlet temperature are known
            if (IsHotInletSinglePhase() && IsColdInletSinglePhase()) {
               pHotOut = pHotIn;
               pColdOut = pColdIn;

               tHotOut = tHotIn;
               tColdOut = tColdIn;

               // hot side single phase cooling, cold side single phase heating, counter flow--case e
               // applies only to single phase heat transfer on both sides of the heat exchanger
               do {
                  counter++;
                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
                     pColdOut, hotSideFlowRate, coldSideFlowRate, out cpHot, out cpCold, ref htcHot, ref htcCold);
                  totalHeat_Old = totalHeat;
                  tHotOut_Old = tHotOut;
                  tColdOut_Old = tColdOut;

                  if (currentRatingModel.IsParallelFlow()) {
                     totalHeat = CalcCoolingInletHeatingInletParallel(totalHtc, totalArea, hotSideFlowRate, coldSideFlowRate,
                        cpHot, cpCold, tHotIn, tColdIn, out tHotOut, out tColdOut);
                  }
                  else {
                     totalHeat = CalcCoolingInletHeatingInletCounter(totalHtc, totalArea, ftFactor, hotSideFlowRate,
                        coldSideFlowRate, cpHot, cpCold, tHotIn, tColdIn, out tHotOut, out tColdOut);
                  }

                  ftFactorOld = ftFactor;
                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  if (currentRatingModel is HXRatingModelShellAndTube) {
                     ftFactor = ftFactorOld + 0.1 * (ftFactor - ftFactorOld);
                  }

                  tHotOut = tHotOut_Old + 0.1 * (tHotOut - tHotOut_Old);
                  if (IsTemperatureOutOfRange(tHotOut)) {
                     throw new CalculationFailedException("Calculated hot side temperature " + tHotOut + " is out of range");
                  }
                  tColdOut = tColdOut_Old + 0.1 * (tColdOut - tColdOut_Old);
                  if (IsTemperatureOutOfRange(tColdOut)) {
                     throw new CalculationFailedException("Calculated hot side temperature " + tColdOut + " is out of range");
                  }

                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, pColdOut,
                     hotSideFlowRate, coldSideFlowRate, htcHot, htcCold, out dpHot, out dpCold);
                  pHotOut = pHotIn - dpHot;

                  if (IsPressureTooLow(pHotOut)) {
                     throw new CalculationFailedException("Calculated hot side pressure drop " + dpHot + " is too large.");
                  }
                  pColdOut = pColdIn - dpCold;
                  if (IsPressureTooLow(pColdOut)) {
                     throw new CalculationFailedException("Calculated cold side pressure drop " + dpCold + " is too large.");
                  }

               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);

               // there if no phase change on both sides
               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
                  Calculate(coldSideOutlet.Temperature, tColdOut);
                  Calculate(hotSideOutlet.Temperature, tHotOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               #region phase change
               else {
                  subHeatHot = Constants.NO_VALUE;
                  subHeatCold = Constants.NO_VALUE;
                  htcCold1 = Constants.NO_VALUE;
                  htcHot1 = Constants.NO_VALUE;
                  htcCold2 = Constants.NO_VALUE;
                  htcHot2 = Constants.NO_VALUE;
                  // if hot side there is phase change
                  if (HasPhaseChangeHotSide()) {
                     cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tEvapHotIn));
                     subHeatHot = hotSideFlowRate * cpHot * (tHotIn - tEvapHotIn);
                  }
                  // if cold side there is phase change
                  if (HasPhaseChangeColdSide()) {
                     cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapColdIn));
                     subHeatCold = coldSideFlowRate * cpCold * (tEvapColdIn - tColdIn);
                  }

                  if (currentRatingModel.FlowDirection == FlowDirectionType.Counter) {

                     // hot side phase change, cold side single phase change
                     if (subHeatHot != Constants.NO_VALUE && subHeatCold == Constants.NO_VALUE) {
                        //initial guess of the cold side outlet temperature
                        tColdOut = tEvapHot - 5;
                        //initial guess of tCold
                        tCold = tColdOut + 2;
                        vfHotOut = vfHotIn;

                        //subHeat2 = subHeatHot;
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        counter = 0;
                        do {
                           counter++;
                           // condensing heat transfer coefficient
                           tBulkHot1 = tEvapHot;
                           tBulkCold1 = MathUtility.Average(tColdIn, tCold);
                           htcHot1 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot2 - dpHot1 / 2.0, hotSideFlowRate, vfHotIn, vfHotOut);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2.0, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = MathUtility.Average(tEvapHot, tHotIn);
                           tBulkCold2 = MathUtility.Average(tCold, tColdOut);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot2 / 2.0, hotSideFlowRate);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2.0, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tColdOut));
                           cpHot = GetHotSideCp(tBulkHot2);
                           subHeat2 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                           //   c1 = coldSideFlowRate * cpCold / subHtc1;
                           //   c2 = subHeat2 / (coldSideFlowRate * cpCold);
                           //   c3 = 2 * tEvapHot - tColdIn + c2;
                           //   c4 = subHeat2 / subHtc2;
                           //   c5 = tHotIn + tEvapHot + c2;
                           //   c6 = c1 * tColdIn + subHeat2 / subHtc1;
                           //   double a = totalArea + 2 * c1;
                           //   double b = c4 - 0.5 * totalArea * c5 - totalArea * c3 - c1 * c5 - 2 * c6;
                           //   double c = 0.5 * totalArea * c5 * c3 + c5 * c6 - c3 * c4;
                           //   //tColdOut = (-b + Math.Sqrt(b * b - 4.0 * a * c)) / (2 * a);
                           //   tColdOut = (-b - Math.Sqrt(b * b - 4.0 * a * c)) / (2 * a);
                           //   tCold = tColdOut - c2;
                           c1 = subHeat2 / (coldSideFlowRate * cpCold);
                           c2 = coldSideFlowRate * cpCold / subHtc1;
                           c3 = subHeat2 / (subHtc2 * (tHotIn - tEvapHot - c1));
                           c4 = c2 * Math.Log(tEvapHot - tColdIn) + c3 * Math.Log(tHotIn - tColdOut) - totalArea;
                           c5 = Math.Exp(c4 / (c2 + c3));
                           tColdOut = tEvapHot + c1 - c5;
                           tCold = tColdOut - c1;

                           subHeat1 = coldSideFlowRate * cpCold * (tCold - tColdIn);
                           vfHotOut = 1.0 - subHeat1 / (hotSideFlowRate * evapHeatHot);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           subArea2 = c3 * Math.Log((tHotIn - tColdOut)/(tEvapHot - tColdOut + c1));
                           subArea1 = totalArea - subArea2;

                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           dpHot1 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot2 - dpHot1 / 2.0, hotSideFlowRate, 0, vfHotOut);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2.0, coldSideFlowRate);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2.0, coldSideFlowRate);
                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot2);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && !HasPhaseChangeColdSide()) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideOutlet.Temperature, tColdOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (tColdIn < tEvapColdOut && tColdOut > tEvapColdOut) {
                           //to be developed
                        }
                        else if (vfHotOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // hot side single phase, cold side phase change
                     else if (subHeatCold != Constants.NO_VALUE && subHeatHot == Constants.NO_VALUE) {

                        cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapCold));
                        tEvapCold = tEvapColdIn;
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                        tHot = tHotIn + 2;
                        tHotOut = tColdIn + 5;
                        vfColdOut = vfColdIn;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2.0, hotSideFlowRate);
                           //evaporation heat transfer coefficienct???
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           heatFlux1 = CalculateHeatFlux(htcHot1, tBulkHot1, tEvapCold);
                           htcCold1 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold1, pColdIn - dpCold2 - dpCold1 / 2.0, coldSideFlowRate, heatFlux1);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = MathUtility.Average(tHot, tHotOut);
                           tBulkCold2 = MathUtility.Average(tColdIn, tEvapCold);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2.0, hotSideFlowRate);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold2 / 2.0, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpCold = GetColdSideCp(tBulkCold2);
                           subHeat2 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);

                           cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));
                           c1 = subHeat2 / (hotSideFlowRate * cpHot);
                           c2 = hotSideFlowRate * cpHot / subHtc1;
                           c3 = subHeat2 / (subHtc2 * (tEvapCold - tColdIn - c1));
                           c4 = c2 * Math.Log(tHotIn - tEvapCold) + c3 * Math.Log(tHotOut - tColdIn) - totalArea;
                           c5 = Math.Exp(c4 / (c2 + c3));
                           tHotOut = tEvapCold + c5 - c1;
                           tHot = tHotOut + c1;

                           lmtd = currentRatingModel.CalculateLmtd(tHot, tHotOut, tEvapCold, tColdIn);
                           subArea2 = subHeat2 / (subHtc2 * lmtd);
                           subArea1 = totalArea - subArea2;
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tHot);
                           vfColdOut = subHeat1 / (coldSideFlowRate * evapHeatCold);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           dpCold1 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold2 - dpCold1 / 2, coldSideFlowRate, 0, vfColdOut);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold2 / 2, coldSideFlowRate);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold2);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);

                        if (vfColdOut <= 1.0 && (tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot) {
                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                           Calculate(hotSideOutlet.Temperature, tHotOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (tHotIn > tEvapCold && tHotOut < tEvapCold) {
                           //to be developed
                        }
                        else if (vfColdOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // both sides there is phase change--only support phase change happening withing the first
                     // half of the the heat transfer area for each side
                     else if (subHeatHot != Constants.NO_VALUE && subHeatCold != Constants.NO_VALUE) {
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        tEvapCold = GetHotSideBoilingPoint(pColdIn);
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        vfHotIn = vfHotOut = 1.0;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1,tWallHot1 ,pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           //evaporation heat transfer coefficienct???
                           heatFlux1 = CalculateHeatFlux(htcCold1, tEvapCold, tWallCold1);
                           htcCold1 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1 / 2, coldSideFlowRate, heatFlux1);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = tEvapHot;
                           tBulkCold2 = tEvapCold;
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           heatFlux2 = CalculateHeatFlux(htcCold2, tEvapCold, tWallCold2);
                           htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdIn - dpCold3 - dpCold2 / 2, coldSideFlowRate, heatFlux2);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                           tBulkHot3 = tEvapHot;
                           tBulkCold3 = MathUtility.Average(tEvapCold, tColdIn);
                           tWallCold3 = CalculateColdSideWallTemperature(tBulkHot3, htcHot3, tBulkCold3, htcCold3);
                           htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                           htcCold3 = CalculateColdSideSinglePhaseHtc(tBulkCold3, tWallCold3, pColdIn - dpCold3 / 2, coldSideFlowRate);
                           subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                           cpHot = GetHotSideCp(tBulkHot1);
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);
                           subArea1 = hotSideFlowRate * cpHot / subHtc1 * Math.Log((tHotIn - tEvapCold) / (tEvapHot - tEvapCold));

                           cpCold = GetColdSideCp(tBulkCold3);
                           subHeat3 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);
                           subArea3 = coldSideFlowRate * cpCold / subHtc3 * Math.Log((tEvapHot - tColdIn) / (tEvapHot - tEvapCold));

                           subArea2 = totalArea - subArea1 - subArea3;
                           subHeat2 = subHtc2 * subArea2 * (tEvapHot - tEvapCold);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2 + subHeat3;
                           vfHotOut = 1.0 - (subHeat2 + subHeat3) / (hotSideFlowRate * evapHeatHot);
                           vfColdOut = (subHeat1 + subHeat2) / (coldSideFlowRate * evapHeatCold);

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           vfHotOut2 = 1.0 - subHeat2 / (hotSideFlowRate * evapHeatHot);
                           dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut2);
                           vfHotOut3 = vfHotOut2 - subHeat3 / (hotSideFlowRate * evapHeatHot);
                           tWallHot3 = CalculateHotSideWallTemperature(tBulkHot3, htcHot3, tBulkCold3, htcCold3);
                           dpHot3 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotOut2, vfHotOut3);

                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot3 = dpHot3 * subArea3 / totalArea;
                           dpHot = dpHot1 + dpHot2 + dpHot3;

                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                           vfColdOut1 = subHeat1 / (coldSideFlowRate * evapHeatCold);
                           dpCold1 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1 / 2, coldSideFlowRate, 0, vfColdOut1);
                           vfColdOut2 = subHeat2 / (coldSideFlowRate * evapHeatCold);
                           dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdIn - dpCold3 - dpCold2 / 2, coldSideFlowRate, vfColdOut1, vfColdOut2);
                           dpCold3 = CalculateColdSideSinglePhasePressureDrop(tBulkCold3, tWallCold3, pColdIn - dpCold3 / 2, coldSideFlowRate);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold3 = dpCold3 * subArea3 / totalArea;
                           dpCold = dpCold1 + dpCold2 + dpCold3;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold3);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                  }
                  else if (currentRatingModel.FlowDirection == FlowDirectionType.Parallel) {
                     // hot side phase change, cold side single phase
                     if (subHeatHot != Constants.NO_VALUE && subHeatCold == Constants.NO_VALUE) {
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        subHeat1 = subHeatHot;
                        tCold = tColdIn + subHeat1 / (coldSideFlowRate * cpCold);
                        tColdOut = tCold + 5; ;
                        tHotOut = tHotIn;
                        vfHotIn = vfHotOut = 1.0;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                           tBulkCold1 = MathUtility.Average(tColdIn, tCold);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                           cpCold = GetColdSideCp(tBulkCold1);
                           cpHot = GetHotSideCp(tBulkHot1);
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                           tCold = tColdIn + subHeat1 / (coldSideFlowRate * cpCold);
                           lmtd = currentRatingModel.CalculateLmtd(tHotIn, tEvapHot, tColdIn, tCold);
                           subArea1 = subHeat1 / (subHtc1 * lmtd);

                           subArea2 = totalArea - subArea1;

                           tBulkHot2 = tEvapHot;
                           tBulkCold2 = MathUtility.Average(tCold, tColdOut);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                           subHeat2 = CalcCondensingAndHeating(subHtc2, subArea2, coldSideFlowRate,
                              tEvapHot, tCold, cpCold);

                           tColdOut = tCold + subHeat1 / (coldSideFlowRate * cpCold);
                           vfHotOut = 1.0 - subHeat1 / (hotSideFlowRate * evapHeatHot);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;
                           
                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;
                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate);
                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && !HasPhaseChangeColdSide()) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideOutlet.Temperature, tColdOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (tColdIn < tEvapCold && tColdOut > tEvapCold) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                        else if (vfHotOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // hot side single phase, cold side there is phase change
                     else if (subHeatCold != Constants.NO_VALUE && subHeatHot == Constants.NO_VALUE) {
                        subHeat1 = subHeatCold;
                        tEvapCold = GetColdSideBoilingPoint(pColdIn);
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        tHot = tHotIn - subHeat1 / (hotSideFlowRate * cpHot);
                        tHotOut = tHot - 5.0;
                        tColdOut = tColdIn;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                           tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                           cpCold = GetColdSideCp(tBulkCold1);
                           subHeat1 = coldSideFlowRate * cpCold * (tEvapColdIn - tColdIn);

                           lmtd = currentRatingModel.CalculateLmtd(tHotIn, tHot, tColdIn, tEvapCold);
                           subArea1 = subHeat1 / (subHtc1 * lmtd);
                           subArea2 = totalArea - subArea1;

                           tBulkHot2 = MathUtility.Average(tHot, tHotOut);
                           tBulkCold2 = tEvapCold;
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);

                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           heatFlux2 = CalculateHeatFlux(htcHot2, tBulkHot2, tWallHot2);
                           htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate, heatFlux2);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));

                           subHeat2 = CalcCoolingAndEvaporating(subHtc2, subArea2, hotSideFlowRate,
                              tEvapCold, tHot, cpHot);

                           vfColdOut = subHeat2 / (coldSideFlowRate * evapHeatCold);
                           tHotOut = tHot - subHeat2 / (hotSideFlowRate * cpHot);

                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;
                           
                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                           dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate, 0, vfColdOut);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfColdOut <= 1.0 && !HasPhaseChangeHotSide()) {
                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                           Calculate(hotSideOutlet.Temperature, tHotOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // both sides there is phase change
                     else if (subHeatHot != Constants.NO_VALUE && subHeatCold != Constants.NO_VALUE) {
                        //hot side phase change first
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        tEvapCold = GetHotSideBoilingPoint(pColdIn);
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        tColdOut = tColdIn;
                        tHotOut = tHotIn;

                        //hot side phase change first
                        if (subHeatHot <= subHeatCold) {
                           subHeat1 = subHeatHot;
                           tCold = tColdIn + subHeat1 / (coldSideFlowRate * cpCold);
                           vfHotIn = vfHotOut = 1.0;

                           counter = 0;
                           do {
                              counter++;
                              tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                              tBulkCold1 = MathUtility.Average(tColdIn, tCold);
                              tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                              htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                              tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                              htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                              subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                              cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotIn));
                              subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                              lmtd = currentRatingModel.CalculateLmtd(tHotIn, tEvapHot, tColdIn, tCold);
                              subArea1 = subHeat1 / (subHtc1 * lmtd);
                              cpCold = GetColdSideCp(tBulkHot1);
                              tCold = tColdIn + subHeat1 / (coldSideFlowRate * cpCold);

                              htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                              tBulkCold2 = MathUtility.Average(tCold, tEvapCold);
                              tWallCold2 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
                              htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate);
                              subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                              subHeat2 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn) - subHeat1;

                              lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tCold, tEvapCold);
                              subArea2 = subHeat2 / (subHtc2 * lmtd);

                              subArea3 = totalArea - subArea1 - subArea2;

                              htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                              tWallCold3 = CalculateColdSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                              heatFlux3 = CalculateHeatFlux(htcCold3, tEvapCold, tWallCold3);
                              htcCold3 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, heatFlux3);
                              subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                              subHeat3 = CalcCondensingAndEvaporating(subHtc3, subArea3, tEvapHot, tEvapCold);
                              vfColdOut = subHeat3 / (coldSideFlowRate * evapHeatCold);

                              dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                              tWallHot2 = CalculateHotSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
                              vfHotOut2 = 1.0 - subHeat2 / (hotSideFlowRate * evapHeatHot);
                              dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut2);
                              tWallHot3 = CalculateHotSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                              vfHotOut3 = vfHotOut2 - subHeat3 / (hotSideFlowRate * evapHeatHot);
                              dpHot3 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotOut2, vfHotOut3);

                              dpHot1 = dpHot1 * subArea1 / totalArea;
                              dpHot2 = dpHot2 * subArea2 / totalArea;
                              dpHot3 = dpHot3 * subArea3 / totalArea;
                              dpHot = dpHot1 + dpHot2 + dpHot3;

                              tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                              tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                              tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                              evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                              dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                              dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate);
                              dpCold3 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, 0, vfColdOut);

                              dpCold1 = dpCold1 * subArea1 / totalArea;
                              dpCold2 = dpCold2 * subArea2 / totalArea;
                              dpCold3 = dpCold3 * subArea3 / totalArea;

                              dpCold = dpCold1 + dpCold2 + dpCold3;

                              tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1 - dpCold2);
                              tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                              tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                              evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                              vfHotOut = 1.0 - (subHeat2 + subHeat3) / (hotSideFlowRate * evapHeatHot);
                              totalHeat_Old = totalHeat;
                              totalHeat = subHeat1 + subHeat2 + subHeat3;
                           } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                           if (counter == 500) {
                              throw new CalculationFailedException(RATING_CALC_FAILED);
                           }

                           if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                              Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                              Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                              htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                              htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                              CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                           }
                           else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                              throw new CalculationFailedException(RATING_CALC_FAILED);
                           }
                        }
                        //cold side phase change first
                        else {
                           subHeat1 = subHeatCold;
                           tHot = tHotIn - subHeat1 / (hotSideFlowRate * cpHot);

                           tColdOut = tColdIn;
                           tHotOut = tHotIn;
                           pHotOut = pHotIn;
                           vfHotIn = vfHotOut = 1.0;

                           counter = 0;
                           do {
                              counter++;
                              tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                              tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
                              tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                              htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                              tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                              htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                              subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                              cpCold = GetColdSideCp(tBulkCold1);
                              subHeat1 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);
                              lmtd = currentRatingModel.CalculateLmtd(tHotIn, tHot, tColdIn, tEvapCold);
                              subArea1 = subHeat1 / (subHtc1 * lmtd);

                              tBulkHot2 = MathUtility.Average(tHot, tEvapHot);
                              tBulkCold2 = tEvapCold;
                              tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                              htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                              tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                              heatFlux2 = CalculateHeatFlux(htcCold2, tEvapCold, tWallCold2);
                              htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate, heatFlux2);
                              subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                              cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotIn));
                              subHeat2 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot) - subHeat1;
                              lmtd = currentRatingModel.CalculateLmtd(tHot, tEvapHot, tEvapCold, tEvapCold);
                              subArea2 = subHeat2 / (subHtc2 * lmtd);

                              subArea3 = totalArea - subArea1 - subArea2;

                              htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                              tWallCold3 = CalculateColdSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                              heatFlux3 = CalculateHeatFlux(htcCold3, tEvapCold, tWallCold3);
                              htcCold3 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, heatFlux3);
                              subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                              subHeat3 = CalcCondensingAndEvaporating(subHtc3, subArea3, tEvapHot, tEvapCold);

                              dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                              dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                              tWallHot3 = CalculateHotSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                              dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, 0, vfHotOut);

                              dpHot1 = dpHot1 * subArea1 / totalArea;
                              dpHot2 = dpHot2 * subArea2 / totalArea;
                              dpHot3 = dpHot3 * subArea3 / totalArea;
                              dpHot = dpHot1 + dpHot2 + dpHot3;

                              tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1 - dpHot2);
                              tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                              tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                              evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                              dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                              vfColdOut2 = subHeat2 / (coldSideFlowRate * evapHeatCold);
                              dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate, 0, vfColdOut2);
                              vfColdOut3 = vfColdOut2 + subHeat2 / (coldSideFlowRate * evapHeatCold);
                              dpCold3 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, vfColdOut2, vfColdOut3);

                              dpCold1 = dpCold1 * subArea1 / totalArea;
                              dpCold2 = dpCold2 * subArea2 / totalArea;
                              dpCold3 = dpCold3 * subArea3 / totalArea;
                              dpCold = dpCold1 + dpCold2 + dpCold3;

                              tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
                              tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                              tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                              evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                              vfColdOut = (subHeat2 + subHeat3) / (coldSideFlowRate * evapHeatCold);
                              vfHotOut = 1.0 - subHeat3 / (hotSideFlowRate * evapHeatHot);
                              totalHeat_Old = totalHeat;
                              totalHeat = subHeat1 + subHeat2 + subHeat3;
                           } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                           if (counter == 500) {
                              throw new CalculationFailedException(RATING_CALC_FAILED);
                           }

                           if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                              Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                              Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                              htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                              htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                              CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                           }
                           else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                              throw new CalculationFailedException(RATING_CALC_FAILED);
                           }
                        }
                     }
                  }
               }
               #endregion phase change
            }
            #endregion 1.1 hot in and cold in known

            #region 1.2 hot in and cold out known
            // case 1.2--hot inlet temperature and cold outlet temperature are known
            else if (IsHotInletSinglePhase() && IsColdOutletSinglePhase()) {
               pHotOut = pHotIn;
               pColdIn = pColdOut;
               tHotOut = tHotIn;
               tColdIn = tColdOut;

               // hot side single phase cooling, cold side single phase heating, counter flow--case e
               counter = 0;
               do {
                  counter++;
                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
                     pColdOut, hotSideFlowRate, coldSideFlowRate, out cpHot, out cpCold, ref htcHot, ref htcCold);
                  totalHeat_Old = totalHeat;
                  tHotOut_Old = tHotOut;
                  tColdIn_Old = tColdIn;

                  if (currentRatingModel.IsParallelFlow()) {
                     totalHeat = CalcCoolingInletHeatingOutletParallel(totalHtc, totalArea, hotSideFlowRate, coldSideFlowRate,
                        cpHot, cpCold, tHotIn, tColdOut, out tHotOut, out tColdIn);
                  }
                  else {
                     totalHeat = CalcCoolingInletHeatingOutletCounter(totalHtc, totalArea, ftFactor, hotSideFlowRate,
                        coldSideFlowRate, cpHot, cpCold, tHotIn, tColdOut, out tHotOut, out tColdIn);
                  }
                  ftFactorOld = ftFactor;
                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  if (currentRatingModel is HXRatingModelShellAndTube) {
                     ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld);
                  }

                  tHotOut = tHotOut_Old + 0.2 * (tHotOut - tHotOut_Old);
                  tColdIn = tColdIn_Old + 0.2 * (tColdIn - tColdIn_Old);

                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut,
                     pColdIn, pColdOut, hotSideFlowRate, coldSideFlowRate, htcHot, htcCold, out dpHot, out dpCold);
                  pHotOut = pHotIn - dpHot;
                  pColdIn = pColdOut + dpCold;
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  // solving strategy has failed
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
               tEvapColdIn = GetColdSideBoilingPoint(pColdIn);

               // there if no phase change on both sides
               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
                  Calculate(coldSideInlet.Temperature, tColdIn);
                  Calculate(hotSideOutlet.Temperature, tHotOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
                  subHeatHot = Constants.NO_VALUE;
                  subHeatCold = Constants.NO_VALUE;
                  htcCold1 = Constants.NO_VALUE;
                  htcHot1 = Constants.NO_VALUE;
                  htcCold2 = Constants.NO_VALUE;
                  htcHot2 = Constants.NO_VALUE;
                  // if hot side there is phase change
                  if (HasPhaseChangeHotSide()) {
                     cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tEvapHotIn));
                     subHeatHot = hotSideFlowRate * cpHot * (tHotIn - tEvapHotIn);
                  }
                  // if cold side there is phase change
                  if (HasPhaseChangeColdSide()) {
                     cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapColdIn));
                     subHeatCold = coldSideFlowRate * cpCold * (tColdOut - tEvapCold);
                  }

                  if (currentRatingModel.FlowDirection == FlowDirectionType.Counter) {

                     // hot side phase change, cold side single phase change
                     if (subHeatHot != Constants.NO_VALUE && subHeatCold == Constants.NO_VALUE) {
                        //initial guess of the cold side outlet temperature
                        tColdIn = tEvapHot - 5;
                        //initial guess of tCold
                        tCold = tColdOut - subHeatHot / (coldSideFlowRate * cpCold);
                        vfHotOut = vfHotIn;

                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                           tBulkCold1 = MathUtility.Average(tColdOut, tCold);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                           cpCold = GetColdSideCp(tBulkCold1);
                           cpHot = GetHotSideCp(tBulkHot1);
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                           tCold = tColdOut - subHeat1 / (coldSideFlowRate * cpCold);
                           lmtd = currentRatingModel.CalculateLmtd(tHotIn, tEvapHot, tColdOut, tCold);
                           subArea1 = subHeat1 / (subHtc1 * lmtd);

                           subArea2 = totalArea - subArea1;

                           tBulkHot2 = tEvapHot;
                           tBulkCold2 = MathUtility.Average(tCold, tColdIn);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                           subHeat2 = CalcCondensingAndHeating(subHtc2, subArea2, coldSideFlowRate,
                              tEvapHot, tCold, cpCold);

                           tColdIn = tCold - subHeat2 / (coldSideFlowRate * cpCold);
                           vfHotOut = subHeat2 / (hotSideFlowRate * evapHeatHot);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;
                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2 , coldSideFlowRate);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate);
                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && !HasPhaseChangeColdSide()) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideInlet.Temperature, tColdIn);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (HasPhaseChangeColdSide()) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                        else if (vfHotOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // hot side single phase, cold side there is phase change
                     else if (subHeatCold != Constants.NO_VALUE && subHeatHot == Constants.NO_VALUE) {
                        subHeat1 = subHeatCold;
                        tEvapCold = GetColdSideBoilingPoint(pColdOut);
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        tHot = tHotIn - subHeatCold / (hotSideFlowRate * cpHot);
                        tHotOut = tHot - 5.0;
                        vfColdIn = 1.0 - vfColdOut;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                           tBulkCold1 = MathUtility.Average(tColdOut, tEvapCold);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                           cpCold = GetColdSideCp(tBulkCold1);
                           subHeat1 = coldSideFlowRate * cpCold * (tColdOut - tEvapCold);

                           lmtd = currentRatingModel.CalculateLmtd(tHotIn, tHot, tColdOut, tEvapCold);
                           subArea1 = subHeat1 / (subHtc1 * lmtd);
                           subArea2 = totalArea - subArea1;

                           tBulkHot2 = MathUtility.Average(tHot, tHotOut);
                           tBulkCold2 = tEvapCold;
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);

                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           heatFlux2 = CalculateHeatFlux(htcHot2, tBulkHot2, tWallHot2);
                           htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate, heatFlux2);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));

                           subHeat2 = CalcCoolingAndEvaporating(subHtc2, subArea2, hotSideFlowRate,
                              tEvapCold, tHot, cpHot);

                           vfColdIn = 1.0 - subHeat2 / (coldSideFlowRate * evapHeatCold);
                           tHotOut = tHot - subHeat2 / (hotSideFlowRate * cpHot);

                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                           dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate, vfColdIn, 1.0);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfColdOut <= 1.0 && !HasPhaseChangeHotSide()) {
                           Calculate(coldSideOutlet.Temperature, tHotOut);
                           Calculate(hotSideInlet.VaporFraction, vfColdIn);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     //// both sides there is phase change
                     //else if (subHeatHot != Constants.NO_VALUE && subHeatCold != Constants.NO_VALUE) {
                     //   //hot side phase change first
                     //   tEvapHot = GetHotSideBoilingPoint(pHotIn);
                     //   evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                     //   tEvapCold = GetHotSideBoilingPoint(pColdOut);
                     //   evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                     //   tColdOut = tColdIn;
                     //   tHotOut = tHotIn;

                     //   //hot side phase change first
                     //   if (subHeatHot <= subHeatCold) {
                     //      tCold = tColdOut - subHeatHot / (coldSideFlowRate * cpCold);
                     //      vfHotIn2 = 0;
                     //      vfHotOut2 = 0.5;
                     //      vfHotIn3 = 0.5;
                     //      vfHotOut3 = 1.0;

                     //      counter = 0;
                     //      do {
                     //         counter++;
                     //         tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                     //         tBulkCold1 = MathUtility.Average(tColdIn, tCold);
                     //         tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                     //         htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                     //         tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                     //         htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                     //         subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
                     //         cpHot = GetHotSideCp(tBulkHot1);
                     //         subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                     //         lmtd = currentRatingModel.CalculateLmtd(tHotIn, tEvapHot, tColdIn, tCold);
                     //         subArea1 = subHeat1 / (subHtc1 * lmtd);
                     //         cpCold = GetColdSideCp(tBulkHot1);
                     //         tCold = tColdIn + subHeat1 / (coldSideFlowRate * cpCold);

                     //         htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, vfHotIn2, vfHotOut2);
                     //         tBulkCold2 = MathUtility.Average(tCold, tEvapCold);
                     //         tWallCold2 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
                     //         htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate);
                     //         subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                     //         subHeat2 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn) - subHeat1;
                     //         vfHotOut2 = subHeat2 / (hotSideFlowRate * evapHeatHot);

                     //         lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tCold, tEvapCold);
                     //         subArea2 = subHeat2 / (subHtc2 * lmtd);

                     //         subArea3 = totalArea - subArea1 - subArea2;

                     //         htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotOut2, vfHotOut3);
                     //         tWallCold3 = CalculateColdSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                     //         heatFlux3 = CalculateHeatFlux(htcCold3, tEvapCold, tWallCold3);
                     //         htcCold3 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold3, pColdOut + dpCold1 + dpCold2 + dpCold3 / 2, coldSideFlowRate, heatFlux3);
                     //         subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                     //         subHeat3 = CalcCondensingAndEvaporating(subHtc3, subArea3, tEvapHot, tEvapCold);
                     //         vfColdIn = 1.0 - subHeat3 / (coldSideFlowRate * evapHeatCold);

                     //         dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                     //         tWallHot2 = CalculateHotSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
                     //         vfHotOut2 = 1.0 - subHeat2 / (hotSideFlowRate * evapHeatHot);
                     //         dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 0, vfHotOut2);
                     //         tWallHot3 = CalculateHotSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                     //         vfHotOut3 = vfHotOut2 + subHeat3 / (hotSideFlowRate * evapHeatHot);
                     //         dpHot3 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotOut2, vfHotOut3);

                     //         dpHot1 = dpHot1 * subArea1 / totalArea;
                     //         dpHot2 = dpHot2 * subArea2 / totalArea;
                     //         dpHot3 = dpHot3 * subArea3 / totalArea;
                     //         dpHot = dpHot1 + dpHot2 + dpHot3;

                     //         tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                     //         tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                     //         tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                     //         evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                     //         dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                     //         dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate);
                     //         dpCold3 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold3, pColdOut + dpCold1 + dpCold2 + dpCold3 / 2, coldSideFlowRate, 1.0, vfColdIn);

                     //         dpCold1 = dpCold1 * subArea1 / totalArea;
                     //         dpCold2 = dpCold2 * subArea2 / totalArea;
                     //         dpCold3 = dpCold3 * subArea3 / totalArea;

                     //         dpCold = dpCold1 + dpCold2 + dpCold3;

                     //         tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1 - dpCold2);
                     //         tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                     //         tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                     //         evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                     //         vfHotOut = vfHotOut3;
                     //         totalHeat_Old = totalHeat;
                     //         totalHeat = subHeat1 + subHeat2 + subHeat3;
                     //      } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                     //      if (counter == 500) {
                     //         throw new CalculationFailedException(RATING_CALC_FAILED);
                     //      }

                     //      if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                     //         Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                     //         Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                     //         htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                     //         htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                     //         CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                     //      }
                     //      else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                     //         throw new CalculationFailedException(RATING_CALC_FAILED);
                     //      }
                     //   }
                     //   //cold side phase change first
                     //   else {
                     //      tHot = tHotIn -subHeatCold / (hotSideFlowRate * cpHot);

                     //      vfColdIn2 = 0;
                     //      vfColdOut2 = 0.5;
                     //      vfColdIn3 = 0.5;
                     //      vfColdOut3 = 1.0;

                     //      counter = 0;
                     //      do {
                     //         counter++;
                     //         tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                     //         tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
                     //         tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                     //         htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                     //         tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                     //         htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdOut + dpCold1 / 2, coldSideFlowRate);
                     //         subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                     //         cpCold = GetColdSideCp(tBulkCold1);
                     //         subHeat1 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);
                     //         lmtd = currentRatingModel.CalculateLmtd(tHotIn, tHot, tColdIn, tEvapCold);
                     //         subArea1 = subHeat1 / (subHtc1 * lmtd);

                     //         tBulkHot2 = MathUtility.Average(tHot, tEvapHot);
                     //         tBulkCold2 = tEvapCold;
                     //         tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                     //         htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                     //         tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                     //         heatFlux2 = CalculateHeatFlux(htcCold2, tEvapCold, tWallCold2);
                     //         htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdOut + dpCold1 + dpCold2 / 2, coldSideFlowRate, heatFlux2);
                     //         subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                     //         cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotIn));
                     //         subHeat2 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot) - subHeat1;
                     //         lmtd = currentRatingModel.CalculateLmtd(tHot, tEvapHot, tEvapCold, tEvapCold);
                     //         subArea2 = subHeat2 / (subHtc2 * lmtd);

                     //         subArea3 = totalArea - subArea1 - subArea2;

                     //         htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotIn, 1.0);
                     //         tWallCold3 = CalculateColdSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                     //         heatFlux3 = CalculateHeatFlux(htcCold3, tEvapCold, tWallCold3);
                     //         htcCold3 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, heatFlux3);
                     //         subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                     //         subHeat3 = CalcCondensingAndEvaporating(subHtc3, subArea3, tEvapHot, tEvapCold);

                     //         dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                     //         dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                     //         tWallHot3 = CalculateHotSideWallTemperature(tEvapHot, htcHot3, tEvapCold, htcCold3);
                     //         dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, 0, vfHotOut);

                     //         dpHot1 = dpHot1 * subArea1 / totalArea;
                     //         dpHot2 = dpHot2 * subArea2 / totalArea;
                     //         dpHot3 = dpHot3 * subArea3 / totalArea;
                     //         dpHot = dpHot1 + dpHot2 + dpHot3;

                     //         tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1 - dpHot2);
                     //         tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                     //         tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                     //         evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                     //         dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2, coldSideFlowRate);
                     //         vfColdOut2 = subHeat2 / (coldSideFlowRate * evapHeatCold);
                     //         dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2, coldSideFlowRate, 0, vfColdOut2);
                     //         vfColdOut3 = vfColdOut2 + subHeat2 / (coldSideFlowRate * evapHeatCold);
                     //         dpCold3 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold3, pColdIn - dpCold1 - dpCold2 - dpCold3 / 2, coldSideFlowRate, vfColdOut2, vfColdOut3);

                     //         dpCold1 = dpCold1 * subArea1 / totalArea;
                     //         dpCold2 = dpCold2 * subArea2 / totalArea;
                     //         dpCold3 = dpCold3 * subArea3 / totalArea;
                     //         dpCold = dpCold1 + dpCold2 + dpCold3;

                     //         tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
                     //         tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                     //         tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                     //         evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                     //         vfColdOut = (subHeat2 + subHeat3) / (coldSideFlowRate * evapHeatCold);
                     //         vfHotIn = 1.0 - subHeat3 / (hotSideFlowRate * evapHeatHot);
                     //         totalHeat_Old = totalHeat;
                     //         totalHeat = subHeat1 + subHeat2 + subHeat3;
                     //      } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                     //      if (counter == 500) {
                     //         throw new CalculationFailedException(RATING_CALC_FAILED);
                     //      }

                     //      if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                     //         Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                     //         Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                     //         htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                     //         htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                     //         CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                     //      }
                     //      else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                     //         throw new CalculationFailedException(RATING_CALC_FAILED);
                     //      }
                     //   }
                     //}
                  }
                  else if (currentRatingModel.FlowDirection == FlowDirectionType.Parallel) {
                     // hot side phase change, cold side single phase change
                     if (subHeatHot != Constants.NO_VALUE && subHeatCold == Constants.NO_VALUE) {
                        //initial guess of the cold side outlet temperature
                        tColdOut = tEvapHot - 5;
                        //initial guess of tCold
                        tCold = tColdOut + 2;
                        vfHotOut = vfHotIn;

                        //subHeat2 = subHeatHot;
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        counter = 0;
                        do {
                           counter++;
                           // condensing heat transfer coefficient
                           tBulkHot1 = tEvapHot;
                           tBulkCold1 = MathUtility.Average(tColdIn, tCold);
                           htcHot1 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot2 - dpHot1 / 2.0, hotSideFlowRate, vfHotIn, vfHotOut);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           htcCold1 = CalculateColdSideSinglePhaseHtc(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2.0, coldSideFlowRate);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = MathUtility.Average(tEvapHot, tHotIn);
                           tBulkCold2 = MathUtility.Average(tCold, tColdOut);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot2, tWallHot2, pHotIn - dpHot2 / 2.0, hotSideFlowRate);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2.0, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tColdOut));
                           cpHot = GetHotSideCp(tBulkHot2);
                           subHeat2 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);

                           //   c1 = coldSideFlowRate * cpCold / subHtc1;
                           //   c2 = subHeat2 / (coldSideFlowRate * cpCold);
                           //   c3 = 2 * tEvapHot - tColdIn + c2;
                           //   c4 = subHeat2 / subHtc2;
                           //   c5 = tHotIn + tEvapHot + c2;
                           //   c6 = c1 * tColdIn + subHeat2 / subHtc1;
                           //   double a = totalArea + 2 * c1;
                           //   double b = c4 - 0.5 * totalArea * c5 - totalArea * c3 - c1 * c5 - 2 * c6;
                           //   double c = 0.5 * totalArea * c5 * c3 + c5 * c6 - c3 * c4;
                           //   //tColdOut = (-b + Math.Sqrt(b * b - 4.0 * a * c)) / (2 * a);
                           //   tColdOut = (-b - Math.Sqrt(b * b - 4.0 * a * c)) / (2 * a);
                           //   tCold = tColdOut - c2;
                           c1 = subHeat2 / (coldSideFlowRate * cpCold);
                           c2 = coldSideFlowRate * cpCold / subHtc1;
                           c3 = subHeat2 / (subHtc2 * (tHotIn - tEvapHot - c1));
                           c4 = c2 * Math.Log(tEvapHot - tColdIn) + c3 * Math.Log(tHotIn - tColdOut) - totalArea;
                           c5 = Math.Exp(c4 / (c2 + c3));
                           tColdOut = tEvapHot + c1 - c5;
                           tCold = tColdOut - c1;

                           subHeat1 = coldSideFlowRate * cpCold * (tCold - tColdIn);
                           vfHotOut = 1.0 - subHeat1 / (hotSideFlowRate * evapHeatHot);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           subArea2 = c3 * Math.Log((tHotIn - tColdOut)/(tEvapHot - tColdOut + c1));
                           subArea1 = totalArea - subArea2;

                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tBulkCold1, htcCold1);
                           dpHot1 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot2 - dpHot1 / 2.0, hotSideFlowRate, 0, vfHotOut);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                           dpCold1 = CalculateColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1 / 2.0, coldSideFlowRate);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold1 - dpCold2 / 2.0, coldSideFlowRate);
                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot2);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && !HasPhaseChangeColdSide()) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideOutlet.Temperature, tColdOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (tColdIn < tEvapColdOut && tColdOut > tEvapColdOut) {
                           //to be developed
                        }
                        else if (vfHotOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // hot side single phase, cold side phase change
                     else if (subHeatCold != Constants.NO_VALUE && subHeatHot == Constants.NO_VALUE) {

                        cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapCold));
                        tEvapCold = tEvapColdIn;
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                        tHot = tHotIn + 2;
                        tHotOut = tColdIn + 5;
                        vfColdOut = vfColdIn;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tHot);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2.0, hotSideFlowRate);
                           //evaporation heat transfer coefficienct???
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           heatFlux1 = CalculateHeatFlux(htcHot1, tBulkHot1, tEvapCold);
                           htcCold1 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold1, pColdIn - dpCold2 - dpCold1 / 2.0, coldSideFlowRate, heatFlux1);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = MathUtility.Average(tHot, tHotOut);
                           tBulkCold2 = MathUtility.Average(tColdIn, tEvapCold);
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideSinglePhaseHtc(tBulkHot1, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2.0, hotSideFlowRate);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcCold2 = CalculateColdSideSinglePhaseHtc(tBulkCold2, tWallCold2, pColdIn - dpCold2 / 2.0, coldSideFlowRate);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
                           cpCold = GetColdSideCp(tBulkCold2);
                           subHeat2 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);

                           cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));
                           c1 = subHeat2 / (hotSideFlowRate * cpHot);
                           c2 = hotSideFlowRate * cpHot / subHtc1;
                           c3 = subHeat2 / (subHtc2 * (tEvapCold - tColdIn - c1));
                           c4 = c2 * Math.Log(tHotIn - tEvapCold) + c3 * Math.Log(tHotOut - tColdIn) - totalArea;
                           c5 = Math.Exp(c4 / (c2 + c3));
                           tHotOut = tEvapCold + c5 - c1;
                           tHot = tHotOut + c1;

                           lmtd = currentRatingModel.CalculateLmtd(tHot, tHotOut, tEvapCold, tColdIn);
                           subArea2 = subHeat2 / (subHtc2 * lmtd);
                           subArea1 = totalArea - subArea2;
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tHot);
                           vfColdOut = subHeat1 / (coldSideFlowRate * evapHeatCold);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2;

                           dpCold1 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold2 - dpCold1 / 2, coldSideFlowRate, 0, vfColdOut);
                           dpCold2 = CalculateColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - dpCold2 / 2, coldSideFlowRate);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold = dpCold1 + dpCold2;

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           dpHot2 = CalculateHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate);
                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot = dpHot1 + dpHot2;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold2);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);

                        if (vfColdOut <= 1.0 && (tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot) {
                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                           Calculate(hotSideOutlet.Temperature, tHotOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (tHotIn > tEvapCold && tHotOut < tEvapCold) {
                           //to be developed
                        }
                        else if (vfColdOut > 1.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                     // both sides there is phase change--only support phase change happening withing the first
                     // half of the the heat transfer area for each side
                     else if (subHeatHot != Constants.NO_VALUE && subHeatCold != Constants.NO_VALUE) {
                        tEvapHot = GetHotSideBoilingPoint(pHotIn);
                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
                        tEvapCold = GetHotSideBoilingPoint(pColdIn);
                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        vfHotIn = vfHotOut = 1.0;

                        counter = 0;
                        do {
                           counter++;

                           tBulkHot1 = MathUtility.Average(tHotIn, tEvapHot);
                           tWallHot1 = CalculateHotSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           htcHot1 = CalculateHotSideSinglePhaseHtc(tBulkHot1,tWallHot1 ,pHotIn - dpHot1 / 2, hotSideFlowRate);
                           tWallCold1 = CalculateColdSideWallTemperature(tBulkHot1, htcHot1, tEvapCold, htcCold1);
                           //evaporation heat transfer coefficienct???
                           heatFlux1 = CalculateHeatFlux(htcCold1, tEvapCold, tWallCold1);
                           htcCold1 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1 / 2, coldSideFlowRate, heatFlux1);
                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);

                           tBulkHot2 = tEvapHot;
                           tBulkCold2 = tEvapCold;
                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           htcHot2 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
                           heatFlux2 = CalculateHeatFlux(htcCold2, tEvapCold, tWallCold2);
                           htcCold2 = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold2, pColdIn - dpCold3 - dpCold2 / 2, coldSideFlowRate, heatFlux2);
                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);

                           tBulkHot3 = tEvapHot;
                           tBulkCold3 = MathUtility.Average(tEvapCold, tColdIn);
                           tWallCold3 = CalculateColdSideWallTemperature(tBulkHot3, htcHot3, tBulkCold3, htcCold3);
                           htcHot3 = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotIn, vfHotOut);
                           htcCold3 = CalculateColdSideSinglePhaseHtc(tBulkCold3, tWallCold3, pColdIn - dpCold3 / 2, coldSideFlowRate);
                           subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);

                           cpHot = GetHotSideCp(tBulkHot1);
                           subHeat1 = hotSideFlowRate * cpHot * (tHotIn - tEvapHot);
                           subArea1 = hotSideFlowRate * cpHot / subHtc1 * Math.Log((tHotIn - tEvapCold) / (tEvapHot - tEvapCold));

                           cpCold = GetColdSideCp(tBulkCold3);
                           subHeat3 = coldSideFlowRate * cpCold * (tEvapCold - tColdIn);
                           subArea3 = coldSideFlowRate * cpCold / subHtc3 * Math.Log((tEvapHot - tColdIn) / (tEvapHot - tEvapCold));

                           subArea2 = totalArea - subArea1 - subArea3;
                           subHeat2 = subHtc2 * subArea2 * (tEvapHot - tEvapCold);
                           totalHeat_Old = totalHeat;
                           totalHeat = subHeat1 + subHeat2 + subHeat3;
                           vfHotOut = 1.0 - (subHeat2 + subHeat3) / (hotSideFlowRate * evapHeatHot);
                           vfColdOut = (subHeat1 + subHeat2) / (coldSideFlowRate * evapHeatCold);

                           dpHot1 = CalculateHotSideSinglePhasePressureDrop(tBulkHot1, tWallHot1, pHotIn - dpHot1 / 2, hotSideFlowRate);
                           vfHotOut2 = 1.0 - subHeat2 / (hotSideFlowRate * evapHeatHot);
                           dpHot2 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - dpHot2 / 2, hotSideFlowRate, 1.0, vfHotOut2);
                           vfHotOut3 = vfHotOut2 - subHeat3 / (hotSideFlowRate * evapHeatHot);
                           tWallHot3 = CalculateHotSideWallTemperature(tBulkHot3, htcHot3, tBulkCold3, htcCold3);
                           dpHot3 = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3 / 2, hotSideFlowRate, vfHotOut2, vfHotOut3);

                           dpHot1 = dpHot1 * subArea1 / totalArea;
                           dpHot2 = dpHot2 * subArea2 / totalArea;
                           dpHot3 = dpHot3 * subArea3 / totalArea;
                           dpHot = dpHot1 + dpHot2 + dpHot3;

                           tEvapHotIn = GetHotSideBoilingPoint(pHotIn - dpHot1);
                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                           tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                           vfColdOut1 = subHeat1 / (coldSideFlowRate * evapHeatCold);
                           dpCold1 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1 / 2, coldSideFlowRate, 0, vfColdOut1);
                           vfColdOut2 = subHeat2 / (coldSideFlowRate * evapHeatCold);
                           dpCold2 = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold2, pColdIn - dpCold3 - dpCold2 / 2, coldSideFlowRate, vfColdOut1, vfColdOut2);
                           dpCold3 = CalculateColdSideSinglePhasePressureDrop(tBulkCold3, tWallCold3, pColdIn - dpCold3 / 2, coldSideFlowRate);

                           dpCold1 = dpCold1 * subArea1 / totalArea;
                           dpCold2 = dpCold2 * subArea2 / totalArea;
                           dpCold3 = dpCold3 * subArea3 / totalArea;
                           dpCold = dpCold1 + dpCold2 + dpCold3;

                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold3);
                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                           tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

                        if (counter == 500) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }

                        if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                           Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);

                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3) / totalArea;
                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcCold3 * subArea3) / totalArea;
                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
                        }
                        else if (vfHotOut > 1.0 || vfHotOut < 0.0 || vfColdOut > 1.0 || vfColdOut < 0.0) {
                           throw new CalculationFailedException(RATING_CALC_FAILED);
                        }
                     }
                  }
               }
            }
            #endregion 1.2 hot in and cold out known

            //case 1.3--hot inlet temperature and cold inlet temperature are known
            else if (IsHotOutletSinglePhase() && IsColdInletSinglePhase()) {
               pHotIn = pHotOut;
               pColdOut = pColdIn;
               tHotIn = tHotOut;
               tColdOut = tColdIn;

               //applies only to single phase heat transfer on both sides of the heat exchanger
               counter = 0;
               do {
                  counter++;
                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
                     pColdOut, hotSideFlowRate, coldSideFlowRate, out cpHot, out cpCold, ref htcHot, ref htcCold);
                  totalHeat_Old = totalHeat;
                  tHotIn_Old = tHotIn;
                  tColdOut_Old = tColdOut;

                  if (currentRatingModel.IsParallelFlow()) {
                     totalHeat = CalcCoolingOutletHeatingInletParallel(totalHtc, totalArea, hotSideFlowRate, coldSideFlowRate,
                        cpHot, cpCold, tHotOut, tColdIn, out tHotIn, out tColdOut);
                  }
                  else {
                     totalHeat = CalcCoolingOutletHeatingInletCounter(totalHtc, totalArea, ftFactor, hotSideFlowRate,
                        coldSideFlowRate, cpHot, cpCold, tHotOut, tColdIn, out tHotIn, out tColdOut);
                  }

                  ftFactorOld = ftFactor;
                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  if (currentRatingModel is HXRatingModelShellAndTube) {
                     ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld);
                  }

                  tHotIn = tHotIn_Old + 0.2 * (tHotIn - tHotIn_Old);
                  tColdOut = tColdOut_Old + 0.2 * (tColdOut - tColdOut_Old);

                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut,
                     pColdIn, pColdOut, hotSideFlowRate, coldSideFlowRate, htcHot, htcCold, out dpHot, out dpCold);
                  pHotIn = pHotOut + dpHot;
                  pColdOut = pColdIn - dpCold;
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               tEvapHotIn = GetHotSideBoilingPoint(pHotIn);
               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);

               //there if no phase change on both sides
               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
                  Calculate(hotSideInlet.Temperature, tHotIn);
                  Calculate(coldSideOutlet.Temperature, tColdOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
                  //to be developed
               }
            }
            //case 1.4--hot inlet temperature and cold inlet temperature are known
            else if (IsHotOutletSinglePhase() && IsColdOutletSinglePhase()) {
               pHotIn = pHotOut;
               pColdIn = pColdOut;

               tHotIn = tHotOut;
               tColdIn = tColdOut;

               //applies only to single phase heat transfer on both sides of the heat exchanger
               counter = 0;
               do {
                  counter++;
                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
                     pColdOut, hotSideFlowRate, coldSideFlowRate, out cpHot, out cpCold, ref htcHot, ref htcCold);
                  totalHeat_Old = totalHeat;
                  tHotIn_Old = tHotIn;
                  tColdIn_Old = tColdIn;

                  if (currentRatingModel.IsParallelFlow()) {
                     totalHeat = CalcCoolingOutletHeatingOutletParallel(totalHtc, totalArea, hotSideFlowRate, coldSideFlowRate,
                        cpHot, cpCold, tHotOut, tColdOut, out tHotIn, out tColdIn);
                  }
                  else {
                     totalHeat = CalcCoolingOutletHeatingOutletCounter(totalHtc, totalArea, ftFactor, hotSideFlowRate,
                        coldSideFlowRate, cpHot, cpCold, tHotOut, tColdOut, out tHotIn, out tColdIn);
                  }

                  ftFactorOld = ftFactor;
                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  if (currentRatingModel is HXRatingModelShellAndTube) {
                     ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld);
                  }

                  tHotIn = tHotIn_Old + 0.2 * (tHotIn - tHotIn_Old);
                  tColdIn = tColdIn_Old + 0.2 * (tColdIn - tColdIn_Old);

                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut,
                     pColdIn, pColdOut, hotSideFlowRate, coldSideFlowRate, htcHot, htcCold, out dpHot, out dpCold);
                  pHotIn = pHotOut + dpHot;
                  pColdIn = pColdOut + dpCold;
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               tEvapHotIn = GetHotSideBoilingPoint(pHotIn);
               tEvapColdIn = GetColdSideBoilingPoint(pColdIn);

               //there if no phase change on both sides
               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
                  Calculate(hotSideInlet.Temperature, tHotIn);
                  Calculate(coldSideInlet.Temperature, tColdIn);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
                  //to be developed
               }
            }
            // case 1.5--hot side inlet condensing, cold side inlet evaporating--case a
            else if (IsHotInletCondensing() && IsColdInletEvaporating()) {
               pHotOut = pHotIn;
               pColdOut = pColdIn;
               tEvapHot = tEvapHotIn;
               tEvapCold = tEvapColdIn;
               evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               vfHotOut = vfHotIn;
               heatFlux = 1000 * (tEvapHot - tEvapCold);

               counter = 0;
               do {
                  counter++;
                  htcHot = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot / 2, hotSideFlowRate, vfHotIn, vfHotOut);

                  tWallCold = CalculateColdSideWallTemperature(tEvapHot, htcHot, tEvapCold, htcCold);
                  htcCold = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate, heatFlux);
                  //totalHtc = 1.0/(1.0/htcHot + 1.0/htcCold + foulingHot + foulingCold);
                  totalHtc = currentRatingModel.GetTotalHeatTransferCoeff(htcHot, htcCold);

                  totalHeat_Old = totalHeat;
                  totalHeat = CalcCondensingAndEvaporating(totalHtc, totalArea, tEvapHot, tEvapCold);
                  heatFlux = totalHeat / totalArea;

                  vfHotOut = vfHotIn - totalHeat / (hotSideFlowRate * evapHeatHot);
                  vfColdOut = vfColdIn + totalHeat / (coldSideFlowRate * evapHeatCold);

                  tWallHot = CalculateHotSideWallTemperature(tEvapHot, htcHot, tEvapCold, htcCold);
                  dpHot = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallCold, pHotIn - dpHot / 2, hotSideFlowRate, vfHotIn, vfHotOut);

                  tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                  tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                  evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                  dpCold = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate, vfColdIn, vfColdOut);

                  tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                  tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                  evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                  Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                  Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                  Calculate(hotSideOutlet.Temperature, tEvapHotOut);
                  Calculate(coldSideOutlet.Temperature, tEvapColdOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
               }
               #region To Be Developed
               //               //hot side sub cooling
               //               else if (vfColdOut >= 0.0 && vfColdOut <= 1.0 && vfHotOut < 0.0) {
               //                  tHotOut = tEvapHot - 2.0;
               //                  //vfHotOut = 0;
               //                  heatFlux1 = heatFlux;
               //                  heatFlux2 = heatFlux;
               //
               //                  counter = 0;
               //               
               //                  if (currentRatingModel.IsParallelFlow()) {
               //                     do {
               //                        counter++;
               //                        htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0);
               //                        
               //                        tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                        htcCold1 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold1, pColdIn - dpCold/2, coldSideMassFlow, heatFlux1);
               //                        //subHtc1 = 1.0/(1.0/htcHot1 + 1.0/htcCold1 + foulingHot + foulingCold);
               //                        subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                        subHeat1 = hotSideMassFlow*vfHotIn*evapHeatHot;
               //                        subArea1 = subHeat1/(subHtc1*(tEvapHot - tEvapCold));
               //                        heatFlux1 = subHtc1*(tEvapHot - tEvapCold);
               //                     
               //                        subArea2 = totalArea - subArea1;
               //                        
               //                        tBulkHot2 = MathUtility.Average(tEvapHot, tHotOut);
               //                        tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                        htcHot2 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2/2, hotSideMassFlow);
               //                        tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                        htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold/2, coldSideMassFlow, heatFlux2);
               //                        
               //                        subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                        cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotOut));
               //                        subHeat2 = CalcCoolingAndEvaporating(subHtc2, subArea2, hotSideMassFlow, 
               //                           tEvapCold, tEvapHot, cpHot);
               //                     
               //                        heatFlux2 = subHeat2/subArea2;
               //                        
               //                        totalHeat_Old = totalHeat;
               //                        totalHeat = subHeat1 + subHeat2;
               //                        tHotOut = tEvapHot - subHeat2/(hotSideMassFlow*cpHot);
               //                        vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);
               //
               //                        tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                        dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0); 
               //                        dpHot2 = GetHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2/2, hotSideMassFlow); 
               //
               //                        dpHot1 = dpHot1*subArea1/totalArea;
               //                        dpHot2 = dpHot2*subArea2/totalArea;
               //                        dpHot = dpHot1 + dpHot2;
               //
               //                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot1);
               //                        tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                        dpCold1 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold/2.0, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                        dpCold2 = dpCold1;
               //                        dpCold1 = dpCold1*subArea1/totalArea;
               //                        dpCold2 = dpCold2*subArea2/totalArea;
               //                        dpCold = dpCold1 + dpCold2;
               //
               //                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                        tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                     } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);
               //                  }
               //                  else {
               //                     do {
               //                        counter++;
               //                        htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0);
               //                        
               //                        tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                        htcCold1 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold1, pColdIn - dpCold/2, coldSideMassFlow, heatFlux1);
               //                        subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                        subHeat1 = hotSideMassFlow*vfHotIn*evapHeatHot;
               //                        subArea1 = subHeat1/(subHtc1*(tEvapHot - tEvapCold));
               //                        heatFlux1 = subHtc1*(tEvapHot - tEvapCold);
               //
               //                        subArea2 = totalArea - subArea1;
               //                  
               //                        tBulkHot2 = MathUtility.Average(tEvapHot, tHotOut);
               //                        tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                        htcHot2 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2/2, hotSideMassFlow);
               //                        tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                        htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold/2, coldSideMassFlow, heatFlux2);
               //                        subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                        cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotOut));
               //
               //                        subHeat2 = CalcCoolingAndEvaporating(subHtc2, subArea2, hotSideMassFlow, 
               //                           tEvapCold, tEvapHot, cpHot);
               //                     
               //                        heatFlux2 = subHeat2/subArea2;
               //                        
               //                        totalHeat_Old = totalHeat;
               //                        totalHeat = subHeat1 + subHeat2;
               //                        tHotOut = tEvapHot - subHeat2/(hotSideMassFlow*cpHot);
               //                        vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);
               //
               //                        tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                        dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0); 
               //                        dpHot2 = GetHotSideSinglePhasePressureDrop(tBulkHot2, tWallHot2, pHotIn - dpHot1 - dpHot2/2, hotSideMassFlow); 
               //
               //                        dpHot1 = dpHot1*subArea1/totalArea;
               //                        dpHot2 = dpHot2*subArea2/totalArea;
               //                        dpHot = dpHot1 + dpHot2;
               //
               //                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot1);
               //                        tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                        dpCold1 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold/2.0, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                        dpCold2 = dpCold1; 
               //                        dpCold1 = dpCold1*subArea1/totalArea;
               //                        dpCold2 = dpCold2*subArea2/totalArea;
               //                        dpCold = dpCold1 + dpCold2;
               //
               //                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                        tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                     } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);
               //                  }
               //                     
               //                  if (counter == 500) {
               //                     throw new CalculationFailedException(RATING_CALC_FAILED);
               //                  }
               //
               //                  if (vfColdOut >= 0.0 && vfColdOut <= 1.0) {
               //                     Calculate(hotSideOutlet.Temperature, tHotOut);
               //                     Calculate(coldSideOutlet.VaporFraction, vfColdOut);
               //                     Calculate(coldSideOutlet.Temperature, tEvapColdOut);
               //                  
               //                     htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2)/totalArea;
               //                     htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2)/totalArea;
               //                     CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               //                  }
               //               }
               #endregion
            }
            // case 1.6--hot side inlet condensing, cold side inlet single phase heating--case b
            else if (IsHotInletCondensing() && IsColdInletSinglePhaseHeating()) {
               //assume hot outlet is the same single phase as the inlet and the hot outlet is still condensing
               pHotOut = pHotIn;
               pColdOut = pColdIn;
               tEvapHot = tEvapHotIn;
               tEvapCold = tEvapColdIn;
               vfHotOut = vfHotIn;
               tHotOut = tHotIn;
               tColdOut = tColdIn;
               evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

               counter = 0;
               do {
                  counter++;
                  htcHot = CalculateHotSideCondensingHtc(tEvapHot, pHotIn - dpHot / 2, hotSideFlowRate, vfHotIn, vfHotOut);

                  tBulkCold = MathUtility.Average(tColdIn, tColdOut);
                  tWallCold = CalculateColdSideWallTemperature(tEvapHot, htcHot, tBulkCold, htcCold);
                  htcCold = CalculateColdSideSinglePhaseHtc(tBulkCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate);
                  totalHtc = currentRatingModel.GetTotalHeatTransferCoeff(htcHot, htcCold);
                  cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tColdOut));
                  totalHeat_Old = totalHeat;

                  totalHeat = CalcCondensingAndHeating(totalHtc, totalArea, coldSideFlowRate,
                     tEvapHot, tColdIn, cpCold);

                  totalHeat = totalHeat_Old + 0.1 * (totalHeat - totalHeat_Old);
                  vfHotOut = vfHotIn - totalHeat / (hotSideFlowRate * evapHeatHot);
                  tColdOut = tColdIn + totalHeat / (coldSideFlowRate * cpCold);

                  tWallHot = CalculateHotSideWallTemperature(tEvapHot, htcHot, tBulkCold, htcCold);
                  dpHot = CalculateHotSideCondensingPressureDrop(tEvapHot, tWallHot, pHotIn - dpHot / 2, hotSideFlowRate, vfHotIn, vfHotOut);

                  tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
                  tEvapHot = (tEvapHotIn + tEvapHotOut) / 2.0;
                  evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);

                  dpCold = CalculateColdSideSinglePhasePressureDrop(tBulkCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate);
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);

               //if assumption is right then
               if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && !HasPhaseChangeColdSide()) {
                  Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                  //Calculate(hotSideOutlet.Temperature, tEvapHotOut);
                  Calculate(coldSideOutlet.Temperature, tColdOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
               }

               #region To Be Developed
               //               else if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && HasPhaseChangeColdSide()) {
               //                  //assume cold outlet is evaporating and hot outlet is still condensing
               //                  heatFlux2 = 1000 * (tEvapHot - tEvapCold);
               //                  vfHotOut = vfHotIn;
               //                  counter = 0;
               //                  if (currentRatingModel.IsParallelFlow()) 
               //                  {
               //                     do 
               //                     {
               //                        counter++;
               //                        htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - dpHot/2, hotSideMassFlow, vfHotIn, vfHotOut);
               //                        
               //                        tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
               //                        tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tBulkCold, htcCold1);
               //                        htcCold1 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold1, tWallCold1, pColdIn - dpCold1/2, coldSideMassFlow);
               //                        
               //                        subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                        cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapCold));
               //                        subHeat1 = coldSideMassFlow*cpCold * (tEvapCold - tColdIn);
               //
               //                        lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tColdIn, tEvapCold);
               //                        subArea1 = subHeat1/(subHtc1*lmtd);
               //
               //                        subArea2 = totalArea - subArea1;
               //                        htcHot2 = htcHot1;
               //                        tWallCold2 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
               //                        htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2/2, coldSideMassFlow, heatFlux2);
               //                        subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                        subHeat2 = CalcCondensingAndEvaporating(subHtc2, subArea2, tEvapHot, tEvapCold);  
               //                        heatFlux2 = subHeat2/subHtc2;
               //                  
               //                        totalHeat_Old = totalHeat;
               //                        totalHeat = subHeat1 + subHeat2;
               //                        vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
               //                        vfColdOut = subHeat2/(coldSideMassFlow*evapHeatCold);
               //
               //                        tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tBulkCold1, htcCold1);
               //                        dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot/2, hotSideMassFlow, vfHotIn, vfHotOut); 
               //                        dpHot2 = dpHot1; 
               //                        dpHot1 = dpHot1 * subArea1/totalArea;
               //                        dpHot2 = dpHot2 * subArea2/totalArea;
               //                        dpHot = dpHot1 + dpHot2;
               //                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
               //                        tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                        dpCold1 = GetColdSideSinglePhasePressureDrop(tBulkCold, tWallCold1, pColdIn - dpHot1/2, coldSideMassFlow); 
               //                        dpCold2 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold1 - dpCold2/2, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                        dpCold1 = dpCold1 * subArea1/totalArea;
               //                        dpCold2 = dpCold2 * subArea2/totalArea;
               //                        dpCold = dpCold1 + dpCold2;
               //
               //                        tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
               //                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                        tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                     } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
               //                  }
               //                  else  
               //                  {
               //                     do {
               //                        counter++;
               //                        htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - dpHot/2, hotSideMassFlow, vfHotIn, vfHotOut);
               //                        
               //                        tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
               //                        tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tBulkCold1, htcCold1);
               //                        htcCold1 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold1, tWallCold1, pColdIn - dpCold1/2, coldSideMassFlow);
               //                        
               //                        subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                        cpCold = GetColdSideCp(tBulkCold1);
               //                        subHeat1 = coldSideMassFlow*cpCold * (tEvapCold - tColdIn);
               //
               //                        lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tColdIn, tEvapCold);
               //                        subArea1 = subHeat1/(subHtc1*lmtd);
               //
               //                        subArea2 = totalArea - subArea1;
               //                        htcHot2 = htcHot1;
               //                        tWallCold2 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
               //                        htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold1 - dpCold2/2.0, coldSideMassFlow, heatFlux2);
               //                        subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                        subHeat2 = CalcCondensingAndEvaporating(subHtc2, subArea2, tEvapHot, tEvapCold);  
               //                        heatFlux2 = subHeat2/subHtc2;
               //                  
               //                        totalHeat_Old = totalHeat;
               //                        totalHeat = subHeat1 + subHeat2;
               //                        vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
               //                        vfColdOut = subHeat2/(coldSideMassFlow*evapHeatCold);
               //
               //                        tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tBulkCold1, htcCold1);
               //                        dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot/2, hotSideMassFlow, vfHotIn, vfHotOut); 
               //                        dpHot2 = dpHot1; 
               //                        dpHot1 = dpHot1 * subArea1/totalArea;
               //                        dpHot2 = dpHot2 * subArea2/totalArea;
               //                        dpHot = dpHot1 + dpHot2;
               //                        tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);
               //                        tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                        evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                        dpCold1 = GetColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1/2, coldSideMassFlow); 
               //                        dpCold2 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold1 - dpCold2/2.0, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                        dpCold1 = dpCold1 * subArea1/totalArea;
               //                        dpCold2 = dpCold2 * subArea2/totalArea;
               //                        dpCold = dpCold1 + dpCold2;
               //
               //                        tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
               //                        tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                        tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                        evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                     } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
               //                  }
               //                     
               //                  if (counter == 500) {
               //                     throw new CalculationFailedException(RATING_CALC_FAILED);
               //                  }
               //
               //                  if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
               //                     Calculate(hotSideOutlet.VaporFraction, vfHotOut);
               //                     Calculate(coldSideOutlet.VaporFraction, vfColdOut);
               //                     Calculate(hotSideOutlet.Temperature, tEvapHotOut);
               //                     Calculate(coldSideOutlet.Temperature, tEvapColdOut);
               //
               //                     htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2)/totalArea;
               //                     htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2)/totalArea;
               //                     CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               //                  }
               //                  //cold outlet is still evaporating but hot outlet is sub cooling
               //                  else if (vfHotOut < 0.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
               //                     //assume cold outlet is still evaporating (not super heating)
               //                     heatFlux3 = heatFlux2;
               //                     vfHotOut = vfHotIn;
               //                     if (currentRatingModel.FlowDirection == FlowDirection.Parallel) {
               //                        tHotOut = tEvapHot;
               //                        counter = 0;
               //                        do {
               //                           counter++;
               //
               //                           htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - (dpHot1+dpHot2)/2, hotSideMassFlow, vfHotIn, 0);
               //                           
               //                           tBulkCold1 = MathUtility.Average(tColdIn, tEvapCold);
               //                           tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tBulkCold1, htcCold1);
               //                           htcCold1 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold, tWallCold1, pColdIn - dpCold1/2, coldSideMassFlow);
               //                           
               //                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                           cpCold = GetColdSideCp(tBulkCold);
               //                           subHeat1 = coldSideMassFlow*cpCold * (tEvapCold - tColdIn);
               //                           
               //                           lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tColdIn, tEvapCold);
               //                           subArea1 = subHeat1/(subHtc1*lmtd);
               //                           
               //                           htcHot2 = htcHot1;
               //                           tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tEvapCold, htcCold2);
               //                           htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold1 - (dpCold1 + dpCold2)/2, coldSideMassFlow, heatFlux2);
               //                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                           subHeat2 = hotSideMassFlow*evapHeatHot*(1.0 - vfHotIn);
               //                           subArea2 = subHeat2/(subHtc2*(tEvapHot-tEvapCold));
               //                           heatFlux2 = subHeat2/subArea2;
               //
               //                           subArea3 = totalArea - subArea1 - subArea2;
               //                           
               //                           tBulkHot3 = MathUtility.Average(tEvapHot, tHotOut);
               //                           tWallHot3 = CalculateHotSideWallTemperature(tBulkHot3, htcHot3, tEvapCold, htcCold3);
               //                           htcHot3 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot3, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3/2, hotSideMassFlow);
               //                           tWallCold3 = CalculateColdSideWallTemperature(tBulkHot3, htcHot3, tEvapCold, htcCold3);
               //                           htcCold3 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold3, pColdIn - dpCold1 - (dpCold1 + dpCold2)/2, coldSideMassFlow, heatFlux3);
               //                           subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);
               //                     
               //                           cpHot = GetHotSideCp(tBulkHot3);
               //                           subHeat3 = CalcCoolingAndEvaporating(subHtc3, subArea3, hotSideMassFlow, 
               //                              tEvapCold, tEvapHot, cpHot);
               //
               //                           heatFlux3 = subHeat3/subArea3;
               //
               //                           tHotOut = tEvapHot - subHeat3/(cpHot*hotSideMassFlow);
               //                           vfColdOut = (subHeat2 + subHeat3)/(hotSideMassFlow*evapHeatHot);
               //                           totalHeat_Old = totalHeat;
               //                           totalHeat = subHeat1 + subHeat2 + subHeat3;
               //
               //                           tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tBulkCold1, htcCold1);
               //                           dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - (dpHot1 + dpHot2)/2, hotSideMassFlow, vfHotIn, 0); 
               //                           dpHot2 = dpHot1; 
               //                           dpHot3 = GetHotSideSinglePhasePressureDrop(tBulkHot3, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3/2, hotSideMassFlow); 
               //                           dpHot1 = dpHot1*subArea1/totalArea;
               //                           dpHot2 = dpHot2*subArea2/totalArea;
               //                           dpHot3 = dpHot3*subArea3/totalArea;
               //                           dpHot = dpHot1 + dpHot2 + dpHot3;
               //                           tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot1 - dpHot2);
               //                           tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                           evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                           dpCold1 = GetColdSideSinglePhasePressureDrop(tBulkCold1, tWallCold1, pColdIn - dpCold1/2, coldSideMassFlow); 
               //                           dpCold2 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold1 - (dpCold2 + dpCold3)/2, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                           dpCold3 = dpCold2; 
               //                           dpCold1 = dpCold1 * subArea1/totalArea;
               //                           dpCold2 = dpCold2 * subArea2/totalArea;
               //                           dpCold3 = dpCold3 * subArea3/totalArea;
               //                           dpCold = dpCold1 + dpCold2 + dpCold3;
               //
               //                           tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
               //                           tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                           tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                           evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                        } while (Math.Abs(totalHeat - totalHeat_Old) < 1.0e-6 && counter < 500);
               //
               //                        if (counter == 500) {
               //                           throw new CalculationFailedException(RATING_CALC_FAILED);
               //                        }
               //
               //                        totalHeat = subHeat1 + subHeat2 + subHeat3;
               //                        vfColdOut = (subHeat2+subHeat3)/(coldSideMassFlow*evapHeatCold);
               //                        if (vfColdOut < 1.0) {
               //                           Calculate(coldSideOutlet.VaporFraction, vfColdOut);
               //                           Calculate(coldSideOutlet.Temperature, tEvapColdOut);
               //                           Calculate(hotSideOutlet.Temperature, tHotOut);
               //
               //                           htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                           htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                           CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               //                        }
               //                        else {
               //                           //to be developed
               //                        }
               //                     }
               //                     else if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
               //                        counter = 0;
               //                        heatFlux1 = heatFlux2;
               //                        do {
               //                           subHeat1 = hotSideMassFlow*vfHotIn*evapHeatHot;
               //                        
               //                           htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn, hotSideMassFlow, vfHotIn, 0);
               //                           tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                           htcCold1 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold1, pColdIn, coldSideMassFlow, heatFlux1);
               //                           subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //                        
               //                           subArea1 = subHeat1/(subHtc1*(tEvapHot - tEvapCold));
               //                           heatFlux = subHtc1*(tEvapHot - tEvapCold);
               //                     
               //                           cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapCold));
               //                           subHeat2 = coldSideMassFlow*cpCold*(tEvapCold - tColdIn);
               //                        
               //                           cpHot = GetHotSideCp(tEvapHot);
               //                           tHotOut = tEvapHot - coldSideMassFlow*cpCold/(hotSideMassFlow*cpHot)*(tEvapCold - tColdIn);
               //
               //                           tBulkHot2 = MathUtility.Average(tEvapHot, tHotOut);
               //                           tBulkCold2 = MathUtility.Average(tEvapCold, tColdIn);
               //                           tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
               //                           htcHot2 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot2, tWallHot2, pHotIn, hotSideMassFlow);
               //                           tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tBulkCold2, htcCold2);
               //                           htcCold2 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold2, tWallCold2, pColdIn, coldSideMassFlow);
               //                           subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                           lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tHotOut, tEvapCold, tColdIn);
               //                           subArea2 = subHeat2/(subHtc2*lmtd);
               //                           totalHeat_Old = totalHeat;
               //                           totalHeat = subHeat1 + subHeat2;
               //                        } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);
               //
               //                        double totalAreaEstimation = subArea1 + subArea2;
               //                        
               //                        if (totalAreaEstimation < totalArea) {
               //                           tHotOut = tHotOut - 2.0;
               //                           tHot = (tHotOut + tEvapHot)/2.0;
               //                           counter = 0;
               //                           do {
               //                              counter++;
               //                              
               //                              htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0);
               //                              tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                              htcCold1 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold1, pColdIn - dpCold3 - (dpCold1 + dpCold2)/2, coldSideMassFlow, heatFlux1);
               //                              subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //
               //                              subHeat1 = hotSideMassFlow*vfHotIn*evapHeatHot;
               //                              subArea1 = subHeat1/(subHtc1*(tEvapHot - tEvapCold));  
               //                              heatFlux1 = subHtc1*(tEvapHot - tEvapCold);
               //
               //                              cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tEvapCold));
               //                              subHeat3 = coldSideMassFlow*cpCold*(tEvapCold - tColdIn);
               //                              
               //                              tBulkHot2 = MathUtility.Average(tEvapHot, tHotOut);
               //                              tWallHot2 = CalculateHotSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                              htcHot2 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot2, tWallHot2, pHotIn - dpHot1 - (dpHot2+dpHot3)/2, hotSideMassFlow);
               //                              tWallCold2 = CalculateColdSideWallTemperature(tBulkHot2, htcHot2, tEvapCold, htcCold2);
               //                              htcCold2 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold2, pColdIn - dpCold3 - (dpCold1 + dpCold2)/2, coldSideMassFlow, heatFlux2);
               //                              subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                           
               //                              htcHot3 = htcHot2;
               //                              //evaporation heat transfer coefficienct???
               //                              tBulkCold3 = MathUtility.Average(tEvapCold, tColdIn);
               //                              tWallCold3 = CalculateColdSideWallTemperature(tBulkHot2, htcHot3, tBulkCold, htcCold3);
               //                              htcCold3 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold3, tWallCold3, pColdIn - dpCold3/2, coldSideMassFlow);
               //                              subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);
               //                              
               //                              cpHot = GetHotSideCp(MathUtility.Average(tEvapHot, tHotOut));
               //                              c1 = subHeat3/hotSideMassFlow * cpHot;
               //                              
               //                              cpHot = GetColdSideCp(MathUtility.Average(tEvapHot, tHot));
               //                              c2 = hotSideMassFlow * cpHot/subHtc2;
               //                              c3 = subHeat3/(subHtc3 * (tEvapCold - tColdIn - c2));
               //                              c4 = c2 * Math.Log(tEvapHot - tEvapCold) + c3 * Math.Log(tHotOut - tColdIn) - (totalArea - subArea1);
               //                              c5 = Math.Exp(c4/(c2+c3));
               //                              tHotOut = tEvapCold + c5 - c1;
               //                              tHot = tHotOut + c1;
               //                              
               //                              subHeat2 = hotSideMassFlow*cpHot*(tEvapHot - tHot);
               //                              lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tHotOut, tEvapCold, tEvapCold);
               //                              subArea2 = subHeat2/(subHtc2*lmtd);
               //                              heatFlux2 = subHtc2*lmtd;
               //
               //                              subArea3 = totalArea - subArea1 - subArea2;
               //
               //                              totalHeat_Old = totalHeat; 
               //                              totalHeat = subHeat1 + subHeat2 + subHeat3;
               //                              vfColdOut = (subHeat1+subHeat2)/(coldSideMassFlow*evapHeatCold);
               //
               //                              subArea3 = totalArea - subArea1 - subHeat2;
               //                              tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                              dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - dpHot1/2, hotSideMassFlow, vfHotIn, 0); 
               //                              dpHot2 = GetHotSideSinglePhasePressureDrop(tEvapHot, tWallHot2, pHotIn - dpHot1 - (dpHot2+dpHot3)/2, hotSideMassFlow); 
               //                              dpHot3 = dpHot2; 
               //                              dpHot1 = dpHot1*subArea1/totalArea;
               //                              dpHot2 = dpHot2*subHeat2/totalArea;
               //                              dpHot3 = dpHot3*subArea3/totalArea;
               //                              dpHot = dpHot1+dpHot2+dpHot3;
               //                              tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot1);
               //                              tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                              evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                              dpCold1 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold3 - (dpCold2 + dpCold1)/2, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                              dpCold2 = dpCold1; 
               //                              dpCold3 = GetColdSideSinglePhasePressureDrop(tBulkCold3, tWallCold3, pColdIn - dpCold3/2, coldSideMassFlow); 
               //                              dpCold1 = dpCold1 * subArea1/totalArea;
               //                              dpCold2 = dpCold2 * subArea2/totalArea;
               //                              dpCold3 = dpCold3 * subArea3/totalArea;
               //                              dpCold = dpCold1 + dpCold2 + dpCold3;
               //
               //                              tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold1);
               //                              tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                              tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                              evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                           } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);
               //
               //                           if (counter == 500) {
               //                              throw new CalculationFailedException(RATING_CALC_FAILED);
               //                           }
               //
               //                           if (vfColdOut < 1.0) {
               //                              Calculate(coldSideOutlet.VaporFraction, vfColdOut);
               //                              Calculate(hotSideOutlet.Temperature, tHotOut);
               //                              
               //                              htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                              htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                              CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               //                           }
               //                           else {
               //                              //to be developed
               //                           }
               //                        }
               //                        else if (totalAreaEstimation > totalArea) {
               //                           tHotOut = tHotOut + 2.0;
               //                           tCold = (tColdIn + tEvapCold)/2.0;
               //                           cpCold = GetColdSideCp(MathUtility.Average(tEvapCold, tColdIn));
               //                           double subHeat2PlusSubHeat3 = coldSideMassFlow*cpCold*(tEvapCold - tColdIn);
               //                           double subHeat1PlusSubHeat2 = hotSideMassFlow*vfHotIn*evapHeatHot;
               //                           double subHeat1MinusSubHeat3 = subHeat1PlusSubHeat2 - subHeat2PlusSubHeat3;
               //                           counter = 0;
               //                           do {
               //                              counter++;
               //                              htcHot1 = GetHotSideCondensingHeatTransferCoeff(tEvapHot, pHotIn - (dpHot1 + dpHot2)/2, hotSideMassFlow, vfHotIn, 0);
               //                              tWallCold1 = CalculateColdSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                              htcCold1 = GetColdSideEvaporatingHeatTransferCoeff(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1/2, coldSideMassFlow, heatFlux1);
               //                              subHtc1 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot1, htcCold1);
               //
               //                              c1 = subHtc1*(tEvapHot - tEvapCold);
               //
               //                              htcHot2 = htcHot1;
               //                              //evaporation heat transfer coefficienct???
               //                              tBulkCold2 = MathUtility.Average(tEvapCold, tColdIn);
               //                              tWallCold2 = CalculateColdSideWallTemperature(tEvapHot, htcHot2, tBulkCold, htcCold2);
               //                              htcCold2 = GetColdSideSinglePhaseHeatTransferCoeff(tBulkCold2, tWallCold2, pColdIn - (dpCold3 + dpCold2)/2, coldSideMassFlow);
               //                              subHtc2 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot2, htcCold2);
               //                              
               //                              cpCold = GetColdSideCp(tBulkCold1);
               //                              c2 = coldSideMassFlow * cpCold/subHtc2;
               //                              
               //                              tBulkHot3 = MathUtility.Average(tEvapHot, tHotOut);
               //                              tWallHot3 = CalculateHotSideWallTemperature(tBulkHot3, htcHot3, tBulkCold, htcCold3);
               //                              htcHot3 = GetHotSideSinglePhaseHeatTransferCoeff(tBulkHot3, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpCold3/3, hotSideMassFlow);
               //                              htcCold3 = htcCold2;
               //                              subHtc3 = currentRatingModel.GetTotalHeatTransferCoeff(htcHot3, htcCold3);
               //                              
               //                              cpHot = GetHotSideCp(tBulkHot3);
               //                              c3 = hotSideMassFlow * cpHot/subHtc3;
               //                              
               //                              c4 = subHeat1MinusSubHeat3 /(subHtc1*(tEvapHot-tEvapCold));
               //                              subArea2 = c2*Math.Log((tEvapHot - tCold)/(tEvapHot - tEvapCold));
               //                              c5 = c3 * Math.Log((tEvapHot - tCold)/(tHotOut - tColdIn))/(tEvapHot - tCold - tHotOut + tColdIn);
               //                              c6 = (totalArea - subArea2 - c4)/(c1/(tEvapHot - tEvapCold) + c5);
               //                              tHotOut = tEvapCold - c5;
               //                              tCold = tEvapCold - (subHeat2PlusSubHeat3 - hotSideMassFlow * cpHot * c5)/(coldSideMassFlow*cpCold);
               //
               //                              subArea1 = (subHeat1MinusSubHeat3 + hotSideMassFlow * cpHot *(tEvapHot - tHotOut))/(subHtc1*(tEvapHot-tEvapCold));
               //                              subHeat1 = subHtc1 * subArea1 * (tEvapHot - tEvapCold);
               //                              heatFlux1 = subHtc1 * (tEvapHot - tEvapCold);
               //
               //                              subHeat2 = coldSideMassFlow*cpCold*(tEvapCold - tCold);
               //                              subHeat3 = hotSideMassFlow*cpHot*(tEvapCold - tHotOut);
               //                              lmtd = currentRatingModel.CalculateLmtd(tEvapHot, tEvapHot, tEvapCold, tCold);
               //                              subArea2 = subHeat2/(subHtc2*lmtd);
               //                              
               //                              subArea3 = totalArea - subArea1 - subArea2;
               //
               //                              totalHeat_Old = totalHeat; 
               //                              totalHeat = subHeat1 + subHeat2 + subHeat3;
               //                              vfColdOut = (subHeat1)/(coldSideMassFlow*evapHeatCold);
               //                              
               //                              tWallHot1 = CalculateHotSideWallTemperature(tEvapHot, htcHot1, tEvapCold, htcCold1);
               //                              dpHot1 = GetHotSideCondensingPressureDrop(tEvapHot, tWallHot1, pHotIn - (dpHot1 + dpHot2)/2, hotSideMassFlow, vfHotIn, 0); 
               //                              dpHot2 = dpHot1; 
               //                              dpHot3 = GetHotSideSinglePhasePressureDrop(tBulkHot3, tWallHot3, pHotIn - dpHot1 - dpHot2 - dpHot3/2, hotSideMassFlow); 
               //                              dpHot1 = dpHot1*subArea1/totalArea;
               //                              dpHot2 = dpHot2*subHeat2/totalArea;
               //                              dpHot3 = dpHot3*subArea3/totalArea;
               //                              dpHot = dpHot1+dpHot2+dpHot3;
               //                              tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot1 - dpHot2);
               //                              tEvapHot = (tEvapHotIn + tEvapHotOut)/2.0;
               //                              evapHeatHot = GetHotSideEvaporationHeat(tEvapHot);
               //                           
               //                              dpCold1 = GetColdSideEvaporatingPressureDrop(tEvapCold, tWallCold1, pColdIn - dpCold3 - dpCold2 - dpCold1/2, coldSideMassFlow, vfColdIn, vfColdOut); 
               //                              
               //                              dpCold2 = GetColdSideSinglePhasePressureDrop(tBulkCold2, tWallCold2, pColdIn - (dpCold3 + dpCold2)/2, coldSideMassFlow); 
               //                              dpCold3 = dpCold2; 
               //                              dpCold1 = dpCold1*subArea1/totalArea;
               //                              dpCold2 = dpCold2*subArea2/totalArea;
               //                              dpCold3 = dpCold3*subArea2/totalArea;
               //                              dpCold = dpCold1 + dpCold2 + dpCold3;
               //
               //                              tEvapColdIn = GetColdSideBoilingPoint(pColdIn - dpCold3 - dpCold2);
               //                              tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
               //                              tEvapCold = (tEvapColdIn + tEvapColdOut)/2.0;
               //                              evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               //                           } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);
               //
               //                           if (counter == 500) {
               //                              throw new CalculationFailedException(RATING_CALC_FAILED);
               //                           }
               //
               //                           if (vfColdOut < 1.0) {
               //                              Calculate(coldSideOutlet.VaporFraction, vfColdOut);
               //                              Calculate(hotSideOutlet.Temperature, tHotOut);
               //                           
               //                              htcHot = (htcHot1 * subArea1 + htcHot2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                              htcCold = (htcCold1 * subArea1 + htcCold2 * subArea2 + htcHot3 * subArea3)/totalArea;
               //                              CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               //                           }
               //                           else {
               //                              //to be developed
               //                           }
               //                        }
               //                     }
               //                  }
               //               }
               #endregion
            }
            // case 1.7--hot side inlet single phase cooling, cold side inlet evaporating--case c
            else if (IsHotInletSinglePhaseCooling() && IsColdInletEvaporating()) {
               pHotOut = pHotIn;
               pColdOut = pColdIn;
               tEvapHot = tEvapHotIn;
               tEvapCold = tEvapColdIn;
               tHotOut = tHotIn;
               tColdOut = tColdIn;
               evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);
               heatFlux = 1000;

               counter = 0;
               do {
                  counter++;
                  tBulkHot = MathUtility.Average(tHotIn, tHotOut);
                  tWallHot = CalculateHotSideWallTemperature(tBulkHot, htcHot, tEvapCold, htcCold);
                  htcHot = CalculateHotSideSinglePhaseHtc(tBulkHot, tWallHot, pHotIn - dpHot / 2, hotSideFlowRate);
                  tWallCold = CalculateColdSideWallTemperature(tBulkHot, htcHot, tEvapCold, htcCold);
                  htcCold = CalculateColdSideEvaporatingHtc(tEvapCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate, heatFlux);
                  totalHtc = currentRatingModel.GetTotalHeatTransferCoeff(htcHot, htcCold);
                  cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));

                  totalHeat_Old = totalHeat;
                  totalHeat = CalcCoolingAndEvaporating(totalHtc, totalArea, hotSideFlowRate,
                     tEvapCold, tHotIn, cpHot);

                  totalHeat = totalHeat_Old + 0.5 * (totalHeat - totalHeat_Old);

                  heatFlux = totalHeat / totalArea;

                  vfColdOut = vfColdIn + totalHeat / (coldSideFlowRate * evapHeatCold);
                  tHotOut = tHotIn - totalHeat / (hotSideFlowRate * cpHot);

                  dpCold = CalculateColdSideEvaporatingPressureDrop(tEvapCold, tWallCold, pColdIn - dpCold / 2, coldSideFlowRate, vfColdIn, vfColdOut);

                  tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
                  tEvapCold = (tEvapColdIn + tEvapColdOut) / 2.0;
                  evapHeatCold = GetColdSideEvaporationHeat(tEvapCold);

                  dpHot = CalculateHotSideSinglePhasePressureDrop(tBulkHot, tWallHot, pHotIn - dpHot / 2, hotSideFlowRate);
               } while (Math.Abs(totalHeat - totalHeat_Old) > 1.0e-6 && counter < 500);

               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }

               if (dpHot > 5.0e4) {
                  throw new CalculationFailedException("Calculated hot side pressure drop " + dpHot / 1000 + " kPa is too large.");
               }
               else if (dpCold > 5.0e4) {
                  throw new CalculationFailedException("Calculated cold side pressure drop " + dpCold / 1000 + " kPa is too large.");
               }

               tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);

               if (vfColdOut >= 0.0 && vfColdOut <= 1.0 && (tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot) {
                  Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                  Calculate(coldSideOutlet.Temperature, tEvapCold);
                  Calculate(hotSideOutlet.Temperature, tHotOut);

                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
               }
               else {
               }
            }
         }

         #region To Be Developed
         //         else if (hotSideMassFlow != Constants.NO_VALUE) 
         //         {
         //            //case 1--hot inlet, outlet, cold inlet are known
         //            if (tHotIn != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE && tColdIn != Constants.NO_VALUE) {
         //               totalHeat = hotSideMassFlow * (hotSideInlet.SpecificEnthalpy.Value - hotSideOutlet.SpecificEnthalpy.Value);
         //               coldSideMassFlow = hotSideMassFlow;
         //               cpCold = GetColdSideCp(tColdIn);
         //               tColdOut = tColdIn + totalHeat/(cpCold*coldSideMassFlow);
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tColdOut = tHotIn - tHotOut + tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tColdOut = tColdIn - tHotIn + tHotOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //
         //                  coldSideMassFlow_Old = coldSideMassFlow;
         //                  coldSideMassFlow = totalHeat/(cpCold*(tColdOut - tColdIn));
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, 
         //                     out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotIn - dpHot);;
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdIn - dpCold);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(coldSideOutlet.Temperature, tColdOut);
         //                  Calculate(coldSideInlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlow);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //            //case 2--hot inlet, outlet, cold outlet are known
         //            else if (tHotIn != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE) {
         //               totalHeat = hotSideMassFlow * (hotSideOutlet.SpecificEnthalpy.Value - hotSideInlet.SpecificEnthalpy.Value);
         //               coldSideMassFlow = hotSideMassFlow;
         //               cpCold = GetColdSideCp(tColdIn);
         //               tColdIn = tColdOut + totalHeat/(cpCold*coldSideMassFlow);
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tColdIn = tColdOut - tHotIn + tHotOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tColdIn = tColdOut + tHotIn - tHotOut - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //                  
         //                  coldSideMassFlow_Old = coldSideMassFlow;
         //                  coldSideMassFlow = totalHeat/(cpCold*(tColdOut - tColdIn));
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdIn = pColdOut + dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
         //               tEvapColdIn = GetColdSideBoilingPoint(pColdIn);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(coldSideInlet.Temperature, tColdIn);
         //                  Calculate(coldSideInlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlow);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //            //case 3--cold inlet, outlet, hot inlet are known
         //            else if (tColdIn != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE && tHotIn != Constants.NO_VALUE) {
         //               coldSideMassFlow = hotSideMassFlow;
         //               cpHot = GetHotSideCp(tHotIn);
         //               cpCold = GetColdSideCp(tColdIn);
         //               tHotOut = tHotIn - coldSideMassFlow * cpCold * (tColdOut - tColdIn)/(hotSideMassFlow*cpHot);
         //               
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //
         //                     tHotOut = tHotIn - tColdOut + tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                     totalHeat = totalHtc*ftFactor*totalArea * (tHotIn - tColdOut - tHotOut + tColdIn)/Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tHotOut = tHotIn + tColdOut - tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //
         //                  coldSideMassFlow_Old = coldSideMassFlow;
         //                  coldSideMassFlow = totalHeat/((tColdOut - tColdIn)*cpCold);
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(coldSideInlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(hotSideOutlet.Temperature, tHotOut);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //            //case 4--cold inlet, outlet, hot inlet are known
         //            else if (tColdIn != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE) {
         //               coldSideMassFlow = hotSideMassFlow;
         //               cpHot = GetHotSideCp(tHotOut);
         //               cpCold = GetColdSideCp(tColdIn);
         //               tHotIn = tHotOut + coldSideMassFlow * cpCold * (tColdOut - tColdIn)/(hotSideMassFlow*cpHot);
         //               
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tHotIn = tHotOut + tColdOut - tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                     totalHeat = totalHtc*ftFactor*totalArea * (tHotIn - tColdOut - tHotOut + tColdIn)/Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tHotIn = tHotOut + tColdIn - tColdOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //                  
         //                  coldSideMassFlow_Old = coldSideMassFlow;
         //                  coldSideMassFlow = totalHeat/((tColdOut - tColdIn)*cpCold);
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotIn = pHotOut + dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotIn = GetHotSideBoilingPoint(pHotIn);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(coldSideInlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(coldSideOutlet.MassFlowRate, coldSideMassFlow);
         //                  Calculate(hotSideInlet.Temperature, tHotIn);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //         }
         //         else if (coldSideMassFlow != Constants.NO_VALUE) {
         //            //case 1--cold inlet, outlet, hot inlet are known
         //            if (tColdIn != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE && tHotIn != Constants.NO_VALUE) {
         //               totalHeat = coldSideMassFlow * (coldSideOutlet.SpecificEnthalpy.Value - coldSideInlet.SpecificEnthalpy.Value);
         //               hotSideMassFlow = coldSideMassFlow;
         //               cpHot = GetHotSideCp(tHotIn);
         //               tHotOut = tHotIn - totalHeat/(cpHot*hotSideMassFlow);
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tHotOut = tHotIn - tColdOut + tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tHotOut = tHotIn + tColdOut - tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //                  
         //                  hotSideMassFlow_Old = hotSideMassFlow;
         //                  hotSideMassFlow = totalHeat/(cpHot*(tHotIn - tHotOut));
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(hotSideMassFlow - hotSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(hotSideOutlet.Temperature, tHotOut);
         //                  Calculate(hotSideInlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlow);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //
         //            }
         //            //case 2--cold inlet, outlet, hot outlet are known
         //            else if (tColdIn != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE) {
         //               totalHeat = coldSideMassFlow * (coldSideOutlet.SpecificEnthalpy.Value - coldSideInlet.SpecificEnthalpy.Value);
         //               hotSideMassFlow = coldSideMassFlow;
         //               cpHot = GetHotSideCp(tHotOut);
         //               tHotIn = tHotOut + totalHeat/(cpHot*hotSideMassFlow);
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn, 
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tHotIn = tHotOut + tColdOut - tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tHotIn = tHotOut - tColdOut + tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  ftFactorOld = ftFactor;
         //                  ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
         //                  ftFactor = ftFactorOld + 0.2 * (ftFactor - ftFactorOld); 
         //                  
         //                  hotSideMassFlow_Old = hotSideMassFlow;
         //                  hotSideMassFlow = totalHeat/(cpHot*(tHotIn - tHotOut));
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotIn = pHotOut + dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotIn = GetHotSideBoilingPoint(pHotIn);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(hotSideInlet.Temperature, tHotIn);
         //                  Calculate(hotSideInlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlow);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //            //case 3--hot inlet, outlet, cold inlet are known            
         //            else if (tHotIn != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE && tColdIn != Constants.NO_VALUE) {
         //               hotSideMassFlow = coldSideMassFlow;
         //               cpHot = GetHotSideCp(tHotIn);
         //               cpCold = GetColdSideCp(tColdIn);
         //               tColdOut = tColdIn + hotSideMassFlow * cpHot * (tHotIn - tHotOut)/(coldSideMassFlow*cpCold);
         //               
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tColdOut = tHotIn - tHotOut + tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                     totalHeat = totalHtc*ftFactor*totalArea * (tHotIn - tColdOut - tHotOut + tColdIn)/Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tColdOut = tHotOut - tHotIn + tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  hotSideMassFlow_Old = hotSideMassFlow;
         //                  hotSideMassFlow = totalHeat/((tHotIn - tHotOut)*cpHot);
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(hotSideInlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(coldSideOutlet.Temperature, tColdOut);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //            //case 4--hot inlet, outlet, cold outlet are known            
         //            else if (tHotIn != Constants.NO_VALUE && tHotOut != Constants.NO_VALUE && tColdOut != Constants.NO_VALUE) {
         //               hotSideMassFlow = coldSideMassFlow;
         //               cpHot = GetHotSideCp(tHotOut);
         //               cpCold = GetColdSideCp(tColdIn);
         //               tColdIn = tColdOut - hotSideMassFlow * cpHot * (tHotIn - tHotOut)/(coldSideMassFlow*cpCold);
         //               
         //               do {
         //                  counter++;
         //                  totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, pColdIn,
         //                     pColdOut, hotSideMassFlow, coldSideMassFlow, out cpHot, out cpCold, ref htcHot, ref htcCold);
         //                     
         //                  if (currentRatingModel.FlowDirection == FlowDirection.Counter) {
         //                     tColdIn = tHotOut - tHotIn + tColdOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                     totalHeat = totalHtc*ftFactor*totalArea * (tHotIn - tColdOut - tHotOut + tColdIn)/Math.Log((tHotIn - tColdOut)/(tHotOut - tColdIn));
         //                  }
         //                  else {
         //                     tColdIn  = tHotIn - tHotOut + tColdOut - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
         //                  }
         //
         //                  hotSideMassFlow_Old = hotSideMassFlow;
         //                  hotSideMassFlow = totalHeat/((tHotIn - tHotOut)*cpHot);
         //                  CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
         //                     pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, htcHot, htcCold, out dpHot, out dpCold);
         //                  pHotOut = pHotIn - dpHot;
         //                  pColdOut = pColdIn - dpCold;
         //               } while (Math.Abs(hotSideMassFlow - hotSideMassFlow_Old) > 1.0e-6 && counter < 500);
         //                  
         //               if (counter == 500) {
         //                  throw new CalculationFailedException(RATING_CALC_FAILED);
         //               }
         //               
         //               tEvapHotOut = GetHotSideBoilingPoint(pHotOut);
         //               tEvapColdOut = GetColdSideBoilingPoint(pColdOut);
         //               
         //               // there if no phase change on both sides
         //               if (!HasPhaseChangeHotSide() && !HasPhaseChangeColdSide()) {
         //                  Calculate(hotSideInlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(hotSideOutlet.MassFlowRate, hotSideMassFlow);
         //                  Calculate(coldSideInlet.Temperature, tColdIn);
         //                     
         //                  CommitHeatTransferCoeffsAndPressureDrops(totalHeat, htcHot, htcCold, totalHtc, dpHot, dpCold);
         //               }
         //            }
         //         }
         #endregion
      }

      private bool IsTemperatureOutOfRange(double t) {
         bool isOutOfRange = false;
         if (t < 0.018 || t > 871) {
            isOutOfRange = true;
         }
         return isOutOfRange;
      }

      private bool IsPressureTooLow(double p) {
         bool isTooLow = false;
         if (p < 611.22) {
            isTooLow = true;
         }
         return isTooLow;
      }

      private static double CalculateHeatFlux(double htc, double tHot, double tCold) {
         if (htc == Constants.NO_VALUE) {
            htc = 1.0;
         }
         double tempDiff = tHot - tCold;
         if (tempDiff <= 0.0) {
            tempDiff = 2.0;
         }
         double q = htc * tempDiff;
         return q;
      }

      private double CalculateColdSideSinglePhaseHtc(double tBulk, double tWall, double pressure, double massFlowRate) {
         double retValue = 0;

         if (tEvapColdIn != Constants.NO_VALUE && tBulk < tEvapColdIn || tEvapColdOut != Constants.NO_VALUE && tBulk < tEvapColdOut) {
            retValue = currentRatingModel.GetColdSideLiquidPhaseHeatTransferCoeff(tBulk, tWall, massFlowRate);
         }
         else {
            retValue = currentRatingModel.GetColdSideVaporPhaseHeatTransferCoeff(tBulk, tWall, pressure, massFlowRate);
         }
         return retValue;
      }

      private double CalculateHotSideSinglePhaseHtc(double tBulk, double tWall, double pressure, double massFlowRate) {
         double retValue = 0;

         if (tEvapHotIn != Constants.NO_VALUE && tBulk < tEvapHotIn || tEvapHotOut != Constants.NO_VALUE && tBulk < tEvapHotOut) {
            retValue = currentRatingModel.GetHotSideLiquidPhaseHeatTransferCoeff(tBulk, tWall, massFlowRate);
         }
         else {
            retValue = currentRatingModel.GetHotSideVaporPhaseHeatTransferCoeff(tBulk, tWall, pressure, massFlowRate);
         }
         return retValue;
      }

      private double CalculateColdSideEvaporatingHtc(double tBulk, double tWall, double pressure, double massFlowRate, double heatFlux) {
         return currentRatingModel.GetColdSideBoilingHeatTransferCoeff(tBulk, tWall, pressure, massFlowRate, heatFlux);
      }

      private double CalculateHotSideCondensingHtc(double aveTemp, double pressure, double massFlowRate, double inVapQuality, double outVapQuality) {
         return currentRatingModel.GetHotSideCondensingHeatTransferCoeff(aveTemp, pressure, massFlowRate, inVapQuality, outVapQuality);
      }

      private double GetColdSideBoilingPoint(double pressure) {
         double retValue = coldSideInlet.GetBoilingPoint(pressure);
         if (retValue == Constants.NO_VALUE) {
            coldSideOutlet.GetBoilingPoint(pressure);
         }
         return retValue;
      }

      private double GetHotSideBoilingPoint(double pressure) {
         double retValue = hotSideInlet.GetBoilingPoint(pressure);
         if (retValue == Constants.NO_VALUE) {
            hotSideOutlet.GetBoilingPoint(pressure);
         }
         return retValue;
      }

      private double GetColdSideEvaporationHeat(double temperature) {
         return coldSideInlet.GetEvaporationHeat(temperature);
      }

      private double GetHotSideEvaporationHeat(double temperature) {
         return hotSideInlet.GetEvaporationHeat(temperature);
      }

      private double GetColdSideCp(double aveTemp) {
         double retValue = 0;
         if (aveTemp < tEvapColdIn) {
            retValue = coldSideInlet.GetLiquidCp(aveTemp);
         }
         else {
            retValue = coldSideInlet.GetGasCp(aveTemp);
         }
         return retValue;
      }

      private double GetHotSideCp(double aveTemp) {
         double retValue = 0;
         if (aveTemp < tEvapHotIn) {
            retValue = hotSideInlet.GetLiquidCp(aveTemp);
         }
         else {
            retValue = hotSideInlet.GetGasCp(aveTemp);
         }
         return retValue;
      }

      private double CalculateColdSideSinglePhasePressureDrop(double tBulk, double tWall, double pressure, double massFlowRate) {
         double retValue = 0;

         if (tEvapColdIn != Constants.NO_VALUE && tBulk < tEvapColdIn || tEvapColdOut != Constants.NO_VALUE && tBulk < tEvapColdOut) {
            retValue = currentRatingModel.GetColdSideLiquidPhasePressureDrop(tBulk, tWall, massFlowRate);
         }
         else {
            retValue = currentRatingModel.GetColdSideVaporPhasePressureDrop(tBulk, tWall, pressure, massFlowRate);
         }
         return retValue;
      }

      private double CalculateHotSideSinglePhasePressureDrop(double tBulk, double tWall, double pressure, double massFlowRate) {
         double retValue = 0;

         if (tEvapHotIn != Constants.NO_VALUE && tBulk < tEvapHotIn || tEvapHotOut != Constants.NO_VALUE && tBulk < tEvapHotOut) {
            retValue = currentRatingModel.GetHotSideLiquidPhasePressureDrop(tBulk, tWall, massFlowRate);
         }
         else {
            retValue = currentRatingModel.GetHotSideVaporPhasePressureDrop(tBulk, tWall, pressure, massFlowRate);
         }
         return retValue;
      }

      private double CalculateColdSideEvaporatingPressureDrop(double tBulk, double tWall, double pressure, double massFlowRate, double vfIn, double vfOut) {
         return currentRatingModel.GetColdSideBoilingPressureDrop(tBulk, tWall, pressure, massFlowRate, vfIn, vfOut);
      }

      private double CalculateHotSideCondensingPressureDrop(double tBulk, double tWall, double pressure, double massFlowRate, double vfIn, double vfOut) {
         return currentRatingModel.GetHotSideCondensingPressureDrop(tBulk, tWall, pressure, massFlowRate, vfIn, vfOut);
      }


      private void CalculatePressureDrops(double tHotIn, double tHotOut, double tColdIn, double tColdOut,
         double pHotIn, double pHotOut, double pColdIn, double pColdOut, double hotSideMassFlow,
         double coldSideMassFlow, double htcHot, double htcCold, out double dpHot, out double dpCold) {
         dpHot = hotSidePressureDrop.Value;

         double tBulkHot = MathUtility.Average(tHotIn, tHotOut);
         double tBulkCold = MathUtility.Average(tColdIn, tColdOut);
         if (dpHot == Constants.NO_VALUE) {

            double tWallHot = CalculateHotSideWallTemperature(tBulkHot, htcHot, tBulkCold, htcCold);
            dpHot = CalculateHotSideSinglePhasePressureDrop(tBulkHot, tWallHot, MathUtility.Average(pHotIn, pHotOut), hotSideMassFlow);
         }
         dpCold = coldSidePressureDrop.Value;
         if (dpCold == Constants.NO_VALUE) {
            double tWallCold = CalculateColdSideWallTemperature(tBulkHot, htcHot, tBulkCold, htcCold);
            dpCold = CalculateColdSideSinglePhasePressureDrop(tBulkCold, tWallCold, MathUtility.Average(pColdIn, pColdOut), coldSideMassFlow);
         }
      }

      private void CommitHeatTransferCoeffsAndPressureDrops(double totalHeat, double htcHot, double htcCold, double totalHtc, double dpHot, double dpCold) {
         if (!currentRatingModel.HotSideHeatTransferCoefficient.HasValue) {
            Calculate(currentRatingModel.HotSideHeatTransferCoefficient, htcHot);
         }

         if (!currentRatingModel.ColdSideHeatTransferCoefficient.HasValue) {
            Calculate(currentRatingModel.ColdSideHeatTransferCoefficient, htcCold);
         }

         if (!currentRatingModel.TotalHeatTransferCoefficient.HasValue) {
            Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
         }

         if (!hotSidePressureDrop.HasValue) {
            Calculate(hotSidePressureDrop, dpHot);
            BalancePressure(hotSideInlet, hotSideOutlet, dpHot);
         }
         if (!coldSidePressureDrop.HasValue) {
            Calculate(coldSidePressureDrop, dpCold);
            BalancePressure(coldSideInlet, coldSideOutlet, dpCold);
         }

         Calculate(totalHeatTransfer, totalHeat);

         solveState = SolveState.Solved;
      }

      private double CalculateCpsAndHtcs(double tHotIn, double tHotOut, double tColdIn, double tColdOut,
         double pHotIn, double pHotOut, double pColdIn, double pColdOut, double hotSideMassFlow,
         double coldSideMassFlow, out double cpHot, out double cpCold, ref double htcHot, ref double htcCold) {
         cpCold = GetColdSideCp(MathUtility.Average(tColdIn, tColdOut));
         cpHot = GetHotSideCp(MathUtility.Average(tHotIn, tHotOut));

         double tBulkHot = MathUtility.Average(tHotIn, tHotOut);
         double tBulkCold = MathUtility.Average(tColdIn, tColdOut);

         double tWallHot = CalculateHotSideWallTemperature(tBulkHot, htcHot, tBulkCold, htcCold);
         htcHot = CalculateHotSideSinglePhaseHtc(tBulkHot, tWallHot, MathUtility.Average(pHotIn, pHotOut), hotSideMassFlow);

         double tWallCold = CalculateColdSideWallTemperature(tBulkHot, htcHot, tBulkCold, htcCold);
         htcCold = CalculateColdSideSinglePhaseHtc(tBulkCold, tWallCold, MathUtility.Average(tColdIn, tColdOut), coldSideMassFlow);

         double totalHtc = currentRatingModel.GetTotalHeatTransferCoeff(htcHot, htcCold);
         return totalHtc;
      }

      private double CalculateHotSideWallTemperature(double tBulkHot, double htcHot, double tBulkCold, double htcCold) {
         double tWallHot = tBulkHot;
         if (htcHot != Constants.NO_VALUE && htcCold != Constants.NO_VALUE) {
            tWallHot = currentRatingModel.GetHotSideWallTemperature(htcHot, tBulkHot, htcCold, tBulkCold);
         }
         return tWallHot;
      }

      private double CalculateColdSideWallTemperature(double tBulkHot, double htcHot, double tBulkCold, double htcCold) {
         double tWallCold = tBulkCold;
         if (htcHot != Constants.NO_VALUE && htcCold != Constants.NO_VALUE) {
            tWallCold = currentRatingModel.GetColdSideWallTemperature(htcHot, tBulkHot, htcCold, tBulkCold);
         }
         return tWallCold;
      }

      private bool HasPhaseChangeHotSide() {
         bool retValue = false;
         if (tHotIn > tEvapHotIn && tHotOut < tEvapHotOut) {
            retValue = true;
         }
         return retValue;
      }

      private bool HasPhaseChangeColdSide() {
         bool retValue = false;
         if (tColdIn < tEvapColdIn && tColdOut > tEvapColdOut) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsHotInletSinglePhase() {
         bool retValue = false;
         if (tHotIn != Constants.NO_VALUE && Math.Abs(tHotIn - tEvapHotIn) > 1.0e-6) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsHotOutletSinglePhase() {
         bool retValue = false;
         if (tHotOut != Constants.NO_VALUE && Math.Abs(tHotOut - tEvapHotOut) > 1.0e-6) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdInletSinglePhase() {
         bool retValue = false;
         if (tColdIn != Constants.NO_VALUE && Math.Abs(tColdIn - tEvapColdIn) > 1.0e-6) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdOutletSinglePhase() {
         bool retValue = false;
         if (tColdOut != Constants.NO_VALUE && Math.Abs(tColdOut - tEvapColdOut) > 1.0e-6) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsHotInletCondensing() {
         bool retValue = false;
         if (tHotIn != Constants.NO_VALUE && Math.Abs(tHotIn - tEvapHotIn) < 1.0e-6 && vfHotIn > 1.0e-6) {
            retValue = true;
         }

         return retValue;
      }

      private bool IsHotOutletCondensing() {
         bool retValue = false;
         if (tHotOut != Constants.NO_VALUE && Math.Abs(tHotOut - tEvapHotOut) < 1.0e-6 && vfHotOut >= 0.0) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdInletEvaporating() {
         bool retValue = false;
         if (tColdIn != Constants.NO_VALUE && Math.Abs(tColdIn - tEvapColdIn) < 1.0e-6 && vfColdIn >= 0.0) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdOutletEvaporating() {
         bool retValue = false;
         if (tColdOut != Constants.NO_VALUE && Math.Abs(tColdOut - tEvapColdOut) < 1.0e-6 && vfColdIn >= 0.0) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdInletSinglePhaseHeating() {
         bool retValue = false;
         if (tColdIn != Constants.NO_VALUE && (tColdIn > tEvapColdIn || tColdIn < tEvapColdIn)) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsColdOutletSinglePhaseHeating() {
         bool retValue = false;
         if (tColdOut != Constants.NO_VALUE && (tColdOut > tEvapColdOut || tColdOut < tEvapColdOut)) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsHotInletSinglePhaseCooling() {
         bool retValue = false;
         if (tHotIn != Constants.NO_VALUE && (tHotIn < tEvapHotIn || tHotIn > tEvapHotIn)) {
            retValue = true;
         }
         return retValue;
      }

      private bool IsHotOutletSinglePhaseCooling() {
         bool retValue = false;
         if (tHotIn != Constants.NO_VALUE && (tHotOut < tEvapHotOut || tHotOut > tEvapHotOut)) {
            retValue = true;
         }
         return retValue;
      }

      private double CalcCondensingAndEvaporating(double totalHtc, double totalArea, double tEvapHot, double tEvapCold) {
         return totalHtc * totalArea * (tEvapHot - tEvapCold);
      }

      private double CalcCondensingAndHeating(double totalHtc, double totalArea, double coldSideMassFlow,
         double tEvapHot, double tColdIn, double cpCold) {
         double tempValue = Math.Exp(totalHtc * totalArea / (coldSideMassFlow * cpCold));
         double totalHeat = coldSideMassFlow * cpCold * (tEvapHot - tColdIn) * (tempValue - 1.0) / tempValue;

         return totalHeat;
      }

      private double CalcCoolingAndEvaporating(double totalHtc, double totalArea, double hotSideMassFlow,
         double tEvapCold, double tHotIn, double cpHot) {
         double tempValue = Math.Exp(totalHtc * totalArea / (hotSideMassFlow * cpHot));
         double totalHeat = hotSideMassFlow * cpHot * (tHotIn - tEvapCold) * (tempValue - 1.0) / tempValue;

         return totalHeat;
      }

      private double CalcCoolingInletHeatingInletCounter(double totalHtc, double totalArea, double ftFactor,
         double hotSideMassFlow, double coldSideMassFlow, double cpHot, double cpCold,
         double tHotIn, double tColdIn, out double tHotOut, out double tColdOut) {
         double tempValue = Math.Exp(totalHtc * totalArea * ftFactor * (1.0 / (hotSideMassFlow * cpHot) - 1.0 / (coldSideMassFlow * cpCold)));
         double totalHeat = (tHotIn - tColdIn) * (1.0 - tempValue) / (1.0 / (coldSideMassFlow * cpCold) - tempValue / (hotSideMassFlow * cpHot));
         tHotOut = tHotIn - totalHeat / (hotSideMassFlow * cpHot);
         tColdOut = tColdIn + totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingInletHeatingInletParallel(double totalHtc, double totalArea, double hotSideMassFlow,
         double coldSideMassFlow, double cpHot, double cpCold, double tHotIn, double tColdIn,
         out double tHotOut, out double tColdOut) {
         double tempValue = 1.0 / (hotSideMassFlow * cpHot) + 1.0 / (coldSideMassFlow * cpCold);
         double totalHeat = (tHotIn - tColdIn) * (1.0 - Math.Exp(-totalHtc * totalArea * tempValue)) / tempValue;
         tHotOut = tHotIn - totalHeat / (hotSideMassFlow * cpHot);
         tColdOut = tColdIn + totalHeat / (coldSideMassFlow * cpCold);
         return totalHeat;
      }

      private double CalcCoolingInletHeatingOutletCounter(double totalHtc, double totalArea, double ftFactor,
         double hotSideMassFlow, double coldSideMassFlow, double cpHot, double cpCold,
         double tHotIn, double tColdOut, out double tHotOut, out double tColdIn) {
         double tempValue = Math.Exp(totalHtc * totalArea * ftFactor * (1.0 / (hotSideMassFlow * cpHot) - 1.0 / (coldSideMassFlow * cpCold)));
         double totalHeat = (tHotIn - tColdOut) * (1.0 - tempValue) / (tempValue * (1.0 / (coldSideMassFlow * cpCold) - 1.0 / (hotSideMassFlow * cpHot)));
         tHotOut = tHotIn - totalHeat / (hotSideMassFlow * cpHot);
         tColdIn = tColdOut - totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingInletHeatingOutletParallel(double totalHtc, double totalArea, double hotSideMassFlow,
         double coldSideMassFlow, double cpHot, double cpCold, double tHotIn, double tColdOut,
         out double tHotOut, out double tColdIn) {
         double tempValue = Math.Exp(totalHtc * totalArea * (1.0 / (coldSideMassFlow * cpCold) + 1.0 / (hotSideMassFlow * cpHot)));
         double totalHeat = (tHotIn - tColdOut) * (tempValue - 1.0) / (1.0 / (coldSideMassFlow * cpCold) + tempValue / (hotSideMassFlow * cpHot));
         tHotOut = tHotIn - totalHeat / (hotSideMassFlow * cpHot);
         tColdIn = tColdOut - totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingOutletHeatingInletCounter(double totalHtc, double totalArea, double ftFactor,
         double hotSideMassFlow, double coldSideMassFlow, double cpHot, double cpCold,
         double tHotOut, double tColdIn, out double tHotIn, out double tColdOut) {
         double tempValue = Math.Exp(totalHtc * totalArea * ftFactor * (1.0 / (hotSideMassFlow * cpHot) - 1.0 / (coldSideMassFlow * cpCold)));
         double totalHeat = (tHotOut - tColdIn) * (tempValue - 1.0) / (1.0 / (hotSideMassFlow * cpHot) - 1.0 / (coldSideMassFlow * cpCold));
         tHotIn = tHotOut + totalHeat / (hotSideMassFlow * cpHot);
         tColdOut = tColdIn + totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingOutletHeatingInletParallel(double totalHtc, double totalArea, double hotSideMassFlow,
         double coldSideMassFlow, double cpHot, double cpCold, double tHotOut, double tColdIn,
         out double tHotIn, out double tColdOut) {
         double tempValue = Math.Exp(totalHtc * totalArea * (1.0 / (hotSideMassFlow * cpHot) + 1.0 / (coldSideMassFlow * cpCold)));
         double totalHeat = (tHotOut - tColdIn) * (tempValue - 1.0) / (1.0 / (hotSideMassFlow * cpHot) + tempValue / (coldSideMassFlow * cpCold));
         tHotIn = tHotOut + totalHeat / (hotSideMassFlow * cpHot);
         tColdOut = tColdIn + totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingOutletHeatingOutletCounter(double totalHtc, double totalArea, double ftFactor,
         double hotSideMassFlow, double coldSideMassFlow, double cpHot, double cpCold,
         double tHotOut, double tColdOut, out double tHotIn, out double tColdIn) {
         double tempValue = Math.Exp(totalHtc * totalArea * ftFactor * (1.0 / (hotSideMassFlow * cpHot) - 1.0 / (coldSideMassFlow * cpCold)));
         double totalHeat = (tHotOut - tColdOut) * (tempValue - 1.0) / (1.0 / (hotSideMassFlow * cpHot) - tempValue / (coldSideMassFlow * cpCold));
         tHotIn = tHotOut + totalHeat / (hotSideMassFlow * cpHot);
         tColdIn = tColdOut - totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      private double CalcCoolingOutletHeatingOutletParallel(double totalHtc, double totalArea, double hotSideMassFlow,
         double coldSideMassFlow, double cpHot, double cpCold, double tHotOut, double tColdOut,
         out double tHotIn, out double tColdIn) {
         double tempValue = 1.0 / (hotSideMassFlow * cpHot) + 1.0 / (coldSideMassFlow * cpCold);
         double totalHeat = (tHotOut - tColdOut) * (Math.Exp(totalHtc * totalArea * tempValue) - 1.0) / tempValue;
         tHotIn = tHotOut + totalHeat / (hotSideMassFlow * cpHot);
         tColdIn = tColdOut - totalHeat / (coldSideMassFlow * cpCold);

         return totalHeat;
      }

      protected HeatExchanger(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHeatExchanger", typeof(int));
         if (persistedClassVersion == 1) {
            this.hotSideInlet = info.GetValue("HotSideInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.hotSideOutlet = info.GetValue("HotSideOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.coldSideInlet = info.GetValue("ColdSideInlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.coldSideOutlet = info.GetValue("ColdSideOutlet", typeof(ProcessStreamBase)) as ProcessStreamBase;

            this.totalHeatTransfer = RecallStorableObject("TotalHeatTransfer", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coldSidePressureDrop = RecallStorableObject("ColdSidePressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.hotSidePressureDrop = RecallStorableObject("HotSidePressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.exchangerType = (ExchangerType)info.GetValue("ExchangerType", typeof(ExchangerType));
            this.ratingModelTable = RecallHashtableObject("RatingModelTable");
            this.currentRatingModel = info.GetValue("CurrentRatingModel", typeof(HXRatingModel)) as HXRatingModel;
         }
         //RecallInitialization();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionHeatExchanger", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("HotSideInlet", hotSideInlet, typeof(ProcessStreamBase));
         info.AddValue("HotSideOutlet", hotSideOutlet, typeof(ProcessStreamBase));
         info.AddValue("ColdSideInlet", coldSideInlet, typeof(ProcessStreamBase));
         info.AddValue("ColdSideOutlet", coldSideOutlet, typeof(ProcessStreamBase));
         info.AddValue("TotalHeatTransfer", totalHeatTransfer, typeof(ProcessVarDouble));
         info.AddValue("ColdSidePressureDrop", coldSidePressureDrop, typeof(ProcessVarDouble));
         info.AddValue("HotSidePressureDrop", hotSidePressureDrop, typeof(ProcessVarDouble));
         info.AddValue("ExchangerType", exchangerType, typeof(ExchangerType));
         info.AddValue("RatingModelTable", ratingModelTable, typeof(Hashtable));
         info.AddValue("CurrentRatingModel", currentRatingModel, typeof(HXRatingModel));
      }

      //private void HeatExchanger_HotSideChanged(HXRatingModelShellAndTube hxRatingModelShellAndTube) {
      //   this.OnHXHotSideChanged();
      //}
   }
}

      /*private double CalculateCpsAndHtcs(double tHotIn, double tHotOut, double tColdIn, double tColdOut,
         double foulingHot, double foulingCold, out double cpHot, out double cpCold, out double htcHot, out double htcCold) {
         double tHotAve = (tHotIn + tHotOut)/2.0;
         double tColdAve = (tColdIn + tColdOut)/2.0;
         cpCold = GetColdSideCp(tColdAve, coldSideInlet.Pressure.Value);
         cpHot = GetHotSideCp(tHotAve, hotSideInlet.Pressure.Value);

         htcHot = GetHotSideSinglePhaseHeatTransferCoeff(tHotAve, 1);
         htcCold = GetColdSideSinglePhaseHeatTransferCoeff(tColdAve, 1);
         double totalHtc = 1.0/(1.0/htcHot + 1.0/htcCold + foulingHot + foulingCold);
         return totalHtc;
      }*/


      /*private double CalcCondensingInletEvaporatingInlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double tEvapHot, double tEvapCold, double evapHeatHot, double evapHeatCold, 
         double vfHotIn, double vfColdIn, out double vfHotOut, out double vfColdOut) { 
         double totalHeat = totalHtc * totalArea * (tEvapHot - tEvapCold);
         vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
         vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);
         return totalHeat;
      }

      private double CalcCondensingInletEvaporatingOutlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double tEvapHot, double tEvapCold, double evapHeatHot, double evapHeatCold, 
         double vfHotIn, double vfColdOut, out double vfHotOut, out double vfColdIn) { 
         double totalHeat = totalHtc*totalArea * (tEvapHot - tEvapCold);
         vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
         vfColdIn = vfColdOut - totalHeat/(coldSideMassFlow*evapHeatCold);
         return totalHeat;
      }

      private double CalcCondensingInletHeatingInlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double evapHeatHot, double tEvapHot, double vfHotIn, double tColdIn, double cpCold, 
         out double vfHotOut, out double tColdOut) {
         double tempValue = Math.Exp(totalHtc*totalArea/(coldSideMassFlow*cpCold));
         totalHeat = coldSideMassFlow*cpCold * (tEvapHot - tColdIn)*(tempValue - 1.0)/tempValue;
         vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
         tColdOut = tColdIn + totalHeat/(coldSideMassFlow*cpCold);
         return totalHeat;
      }
   
      private double CalcCondensingInletHeatingOutlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double evapHeatHot, double tEvapHot, double vfHotIn, double tColdOut, double cpCold, 
         out double vfHotOut, out double tColdIn) {
         double tempValue = Math.Exp(totalHtc*totalArea/(coldSideMassFlow*cpCold));
         totalHeat = coldSideMassFlow*cpCold * (tEvapHot - tColdOut)*(tempValue - 1.0);
         vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
         tColdIn = tColdOut - totalHeat/(coldSideMassFlow*cpCold);
         return totalHeat;
      }

      private double CalcHeatingInletEvaporatingInlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double evapHeatCold, double tEvapCold, double tHotIn, double vfColdIn, double cpHot, 
         out double vfColdOut, out double tHotOut) {
         double tempValue = Math.Exp(totalHtc*totalArea/(hotSideMassFlow*cpHot));
         totalHeat = hotSideMassFlow*cpHot * (tHotIn - tEvapCold)*(tempValue - 1.0)/tempValue;
         vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);
         tHotOut = tHotIn - totalHeat/(hotSideMassFlow*cpHot);
         return totalHeat;
      }

      private double CalcHeatingInletEvaporatingOutlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double evapHeatCold, double tEvapCold, double tHotIn, double vfColdIn, double cpHot, 
         out double vfColdOut, out double tHotOut) {
         double tempValue = Math.Exp(totalHtc*totalArea/(hotSideMassFlow*cpHot));
         totalHeat = hotSideMassFlow*cpHot * (tHotIn - tEvapCold)*(tempValue - 1.0)/tempValue;
         vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);
         tHotOut = tHotIn - totalHeat/(hotSideMassFlow*cpHot);
         return totalHeat;
      }

      private double CalcHeatingOutletEvaporatingInlet(double totalHtc, double totalArea, double hotSideMassFlow, 
         double coldSideMassFlow, double evapHeatCold, double tEvapCold, double tHotOut, double cpHot, 
         out double vfColdOut, out double tHotIn) {
         double tempValue = Math.Exp(totalHtc*totalArea/(hotSideMassFlow*cpHot));
         totalHeat = hotSideMassFlow*cpHot * (tHotOut - tEvapCold)*(tempValue - 1.0);
         vfColdOut = totalHeat/(coldSideMassFlow*evapHeatCold);
         tHotIn = tHotOut + totalHeat/(hotSideMassFlow*cpHot);
         return totalHeat;
      }*/

                     /*else if (!simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Counter) {
                  //applies only to single phase heat transfer on both sides of the heat exchanger
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow,  foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingInletHeatingInletCounter(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotIn, tColdIn, out tHotOut, out tColdOut);
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     //solving stratigy has failed
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }

                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideOutlet.Temperature, tColdOut);
                     Calculate(hotSideOutlet.Temperature, tHotOut);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                     //to be developed
                  }
               }
               else if (!simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Parallel) {
                  //applies only to single phase heat transfer on both sides of the heat exchanger
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow,  foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingInletHeatingInletCounter(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotIn, tColdIn, out tHotOut, out tColdOut);
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     //solving stratigy has failed
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }

                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideOutlet.Temperature, tColdOut);
                     Calculate(hotSideOutlet.Temperature, tHotOut);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                     //to be developed
                  }
               }*/
               /*else if (!simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Counter) {
                  //applies only to single phase heat transfer on both sides of the heat exchanger
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow,  foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingInletHeatingOutletCounter(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotIn, tColdOut, out tHotOut, out tColdIn);
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     //solving stratigy has failed
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideOutlet.Temperature, tColdOut);
                     Calculate(hotSideOutlet.Temperature, tHotOut);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                     //to be developed
                  }
               }
               else if (!simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Parallel) {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow, foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingInletHeatingOutletParallel(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotIn, tColdOut, out tHotOut, out tColdIn);
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(hotSideOutlet.Temperature, tHotOut);
                     Calculate(coldSideInlet.Temperature, tColdIn);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                     //to be developed
                  }
               }*/
               /*if (simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Counter) {
                  //hot side single phase cooling, cold side single phase heating, counter flow--case e
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow, foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingOutletHeatingInletCounter(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotOut, tColdIn, out tHotIn, out tColdIn);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     //solving stratigy has failed
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }

                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(hotSideInlet.Temperature, tHotIn);
                     Calculate(coldSideOutlet.Temperature, tColdOut);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                  }
               }
               else if (simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Parallel) {
                  //hot side single phase cooling, cold side single phase heating, parallel flow--case d
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow, foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingOutletHeatingInletParallel(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotOut, tColdIn, out tHotIn, out tColdOut);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     //solving stratigy has failed
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
   
                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideOutlet.Temperature, tColdOut);
                     Calculate(hotSideInlet.Temperature, tHotIn);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                  }
               }*/

               /*if (simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Counter) {
                  //hot side single phase cooling, cold side single phase heating, counter flow--case e
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow, foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingOutletHeatingOutletCounter(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotOut, tColdOut, out tHotIn, out tColdIn);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }

                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideInlet.Temperature, tColdIn);
                     Calculate(hotSideInlet.Temperature, tHotIn);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                  }
               }
               else if (simpleParallelOrCounterFlow && currentRatingModel.FlowDirection == FlowDirection.Parallel) {
                  //hot side single phase cooling, cold side single phase heating, parallel flow--case d
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, hotSideMassFlow, 
                        coldSideMassFlow, foulingHot, foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     totalHeat_Old = totalHeat;
                     totalHeat = CalcCoolingOutletHeatingOutletParallel(totalHtc, totalArea, ftFactor, hotSideMassFlow, 
                        coldSideMassFlow, cpHot, cpCold, tHotOut, tColdOut, out tHotIn, out tColdIn);
                  } while (Math.Abs(totalHeat-totalHeat_Old) > 1.0e-6 && counter < 500);
 
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
   
                  //there if no phase change on both sides
                  if (((tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot)
                     && ((tColdIn < tEvapCold && tColdOut < tEvapCold) || tColdIn > tEvapCold)) {
                     Calculate(coldSideInlet.Temperature, tColdIn);
                     Calculate(hotSideInlet.Temperature, tHotIn);
                     Calculate(currentRatingModel.TotalHeatTransferCoefficient, totalHtc);
                  }
                  else {
                  }
               }*/


                     // hot side condensing, cold side evaporating--case a
            /*else if (IsHotInletCondensing() && IsColdOutletEvaporating()) {
                  htcHot = GetHotSideSinglePhaseHeatTransferCoeff(tEvapHot, 1);
                  htcCold = GetColdSideSinglePhaseHeatTransferCoeff(tEvapCold, 1);
                  totalHtc = 1.0/(1.0/htcHot + 1.0/htcCold + foulingHot + foulingCold);

                  totalHeat = CalcCondensingAndEvaporating(totalHtc, totalArea, tEvapHot, tEvapCold);  
                  
                  vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
                  vfColdIn = vfColdOut - totalHeat/(coldSideMassFlow*evapHeatCold);

                  if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && vfColdOut >= 0.0 && vfColdOut <= 1.0) {
                     Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                     Calculate(coldSideInlet.VaporFraction, vfHotIn);
                     Calculate(hotSideOutlet.Temperature, tEvapHot);
                     Calculate(coldSideInlet.Temperature, tEvapCold);
                  }
                  else {
                     //to be developed
                  }
               }
                  // hot side condensing, cold side single phase heating--case b
               else if (IsHotInletCondensing() && IsColdOutletSinglePhaseHeating()) {
                  
                  htcHot = GetHotSideSinglePhaseHeatTransferCoeff(tEvapHot, 1);
                  htcCold = GetColdSideSinglePhaseHeatTransferCoeff(tColdOut, 1);
                  totalHtc = 1.0/(1.0/htcHot + 1.0/htcCold + foulingHot + foulingCold);

                  totalHeat = CalcCondensingAndHeating(totalHtc, totalArea, coldSideMassFlow, 
                     tEvapHot, tColdIn, cpCold);
                  
                  vfHotOut = vfHotIn - totalHeat/(hotSideMassFlow*evapHeatHot);
                  tColdIn = tColdOut - totalHeat/(coldSideMassFlow*cpCold);

                  if (vfHotOut >= 0.0 && vfHotOut <= 1.0 && (tColdIn < tEvapCold && tColdOut <= tEvapCold) || (tColdIn > tEvapCold)) {
                     Calculate(hotSideOutlet.VaporFraction, vfHotOut);
                     Calculate(hotSideOutlet.Temperature, tEvapHot);
                     Calculate(coldSideInlet.Temperature, tColdIn);
                  }
                  else {
                     //to be developed
                  }
               }
                  // hot side single phase cooling, cold side Evaporating--case c
               else if (IsHotInletSinglePhaseCooling() && IsColdOutletEvaporating()) {
                  
                  htcHot = GetHotSideSinglePhaseHeatTransferCoeff(tHotIn, 1);
                  htcCold = GetColdSideSinglePhaseHeatTransferCoeff(tEvapCold, 1);
                  totalHtc = 1.0/(1.0/htcHot + 1.0/htcCold + foulingHot + foulingCold);
                  
                  totalHeat = CalcCoolingAndEvaporating(totalHtc, totalArea, hotSideMassFlow, 
                     tEvapCold, tHotIn, cpHot);
                  
                  tHotOut = tHotIn - totalHeat/(hotSideMassFlow*cpHot);
                  vfColdOut = vfColdIn + totalHeat/(coldSideMassFlow*evapHeatCold);

                  if (vfColdOut >= 0.0 && vfColdOut <= 1.0 && (tHotIn > tEvapHot && tHotOut > tEvapHot) || tHotIn < tEvapHot) {
                     Calculate(hotSideOutlet.Temperature, tHotOut);
                     Calculate(coldSideOutlet.VaporFraction, vfColdOut);
                     Calculate(coldSideOutlet.Temperature, tEvapCold);
                  }
                  else {
                     //to be developed
                  }
               }*/


               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     
                     coldSideMassFlow_Old = coldSideMassFlow;
                     tColdOut = tColdIn - tHotIn + tHotOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                     coldSideMassFlow = totalHeat/(cpCold*(tColdOut - tColdIn));
                     
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow,
                        out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/
               

               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tColdIn = tColdOut + tHotIn - tHotOut - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                     coldSideMassFlow_Old = coldSideMassFlow;
                     coldSideMassFlow = totalHeat/(cpCold*(tColdOut - tColdIn));
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdIn = pColdOut + dpCold;
                  }
                  while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/
               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tHotOut = tHotIn + tColdOut - tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);

                     coldSideMassFlow_Old = coldSideMassFlow;
                     coldSideMassFlow = Math.Abs(totalHeat/((tColdOut - tColdIn)*cpCold));
                     
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/
               
               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tHotIn = tHotOut + tColdIn - tColdOut + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     coldSideMassFlow_Old = coldSideMassFlow;
                     coldSideMassFlow = totalHeat/((tColdOut - tColdIn)*cpCold);
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotIn = pHotOut + dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/

               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tHotOut = tHotIn + tColdOut - tColdIn - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                     hotSideMassFlow_Old = hotSideMassFlow;
                     hotSideMassFlow = totalHeat/(cpHot*(tHotIn - tHotOut));
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(hotSideMassFlow - hotSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/
               
               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tHotIn = tHotOut - tColdOut + tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     ftFactor = currentRatingModel.GetFtFactor(tHotIn, tHotOut, tColdIn, tColdOut);
                     hotSideMassFlow_Old = hotSideMassFlow;
                     hotSideMassFlow = totalHeat/(cpHot*(tHotIn - tHotOut));
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotIn = pHotOut + dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(hotSideMassFlow - hotSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/

               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tColdOut = tHotOut - tHotIn + tColdIn + totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     hotSideMassFlow_Old = hotSideMassFlow;
                     hotSideMassFlow = totalHeat/((tHotIn - tHotOut)*cpHot);
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdOut = pColdIn - dpCold;
                  }
                  while (Math.Abs(coldSideMassFlow - coldSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
                  if (counter == 500) {
                     throw new CalculationFailedException(RATING_CALC_FAILED);
                  }
               }*/
               /*else {
                  do {
                     counter++;
                     totalHtc = CalculateCpsAndHtcs(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, foulingHot, 
                        foulingCold, out cpHot, out cpCold, out htcHot, out htcCold);
                     tColdIn  = tHotIn - tHotOut + tColdOut - totalHeat/(totalHtc*totalArea*ftFactor) * Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     totalHeat = totalHtc*totalArea * (tHotIn - tColdIn - tHotOut + tColdOut)/Math.Log((tHotIn - tColdIn)/(tHotOut - tColdOut));
                     hotSideMassFlow_Old = hotSideMassFlow;
                     hotSideMassFlow = totalHeat/((tHotIn - tHotOut)*cpHot);
                     CalculatePressureDrops(tHotIn, tHotOut, tColdIn, tColdOut, pHotIn, pHotOut, 
                        pColdIn, pColdOut, hotSideMassFlow, coldSideMassFlow, out dpHot, out dpCold);
                     pHotOut = pHotIn - dpHot;
                     pColdOut = pColdIn - dpCold;
                  } while (Math.Abs(hotSideMassFlow - hotSideMassFlow_Old) > 1.0e-6 && counter < 500);
                  
               if (counter == 500) {
                  throw new CalculationFailedException(RATING_CALC_FAILED);
               }*/

//protected override void PostSolve() {
//   UpdateStreams();

//   isBeingExecuted = false;

//   OnSolveComplete(solveState);
//}
//private bool InletsArePureLiquids() {
//   bool retValue = false;
//   DryingMaterialStream dms = hotSideInlet as DryingMaterialStream;
//   if (dms != null && (!dms.MassConcentration.HasValue || dms.MassConcentration.HasValue && dms.MassConcentration.Value > 1.0e-6)) {
//      retValue = true;
//   }
//   else {
//      dms = coldSideInlet as DryingMaterialStream;
//      if (dms != null && (!dms.MassConcentration.HasValue || dms.MassConcentration.HasValue && dms.MassConcentration.Value > 1.0e-6)) {
//         retValue = true;
//      }
//   }
//   return retValue;
//}

               
               
