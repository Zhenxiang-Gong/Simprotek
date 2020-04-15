using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.Materials;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.HeatTransfer;

namespace Prosimo.UnitOperations.Drying {
   //public enum DryerType {Rotary, Pneumatic, FluidizedBed, Spray, Tray, Unknown};
   public enum SolidDryerType { Unknown = 0, Rotary, Pneumatic, FluidizedBed, Tray };
   public enum LiquidDryerType { Unknown = 0, Spray, Drum };
   public enum DryerCalculationType { Balance = 0, Scoping, ScalingUp, Rating };

   //public delegate void ProcessTypeChangedEventHandler(Dryer dryer, ProcessType processType);

   /// <summary>
   /// Summary description for Dryer.
   /// </summary>
   [Serializable]
   public abstract class Dryer : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static int GAS_INLET_INDEX = 0;
      public static int GAS_OUTLET_INDEX = 1;
      public static int MATERIAL_INLET_INDEX = 2;
      public static int MATERIAL_OUTLET_INDEX = 3;

      protected DryingGasStream gasInlet;
      protected DryingGasStream gasOutlet;
      protected DryingMaterialStream materialInlet;
      protected DryingMaterialStream materialOutlet;

      //private ProcessType processType;
      private ProcessVarDouble thermalEfficiency;
      private ProcessVarDouble specificHeatConsumption;
      private ProcessVarDouble moistureEvaporationRate;
      private ProcessVarDouble heatLossByTransportDevice;
      private ProcessVarDouble gasPressureDrop;
      //fraction of materials entrianed in the gas stream
      //fractionOfMaterialLostToGasOutlet = MaterialLostToGasOutlet/TotalMaterial
      private ProcessVarDouble fractionOfMaterialLostToGasOutlet;
      private ProcessVarDouble gasOutletMaterialLoading;

      private DryerCalculationType dryerCalculationType;

      protected DryerScopingModel scopingModel;

      #region public properties
      //streams
      public DryingGasStream GasInlet {
         get { return gasInlet; }
      }

      public DryingGasStream GasOutlet {
         get { return gasOutlet; }
      }

      public DryingMaterialStream MaterialInlet {
         get { return materialInlet; }
      }

      public DryingMaterialStream MaterialOutlet {
         get { return materialOutlet; }
      }

      public ProcessVarDouble GasPressureDrop {
         get { return gasPressureDrop; }
      }

      //ProcessVariables
      public ProcessVarDouble HeatLossByTransportDevice {
         get { return heatLossByTransportDevice; }
      }
      public ProcessVarDouble ThermalEfficiency {
         get { return thermalEfficiency; }
      }

      public ProcessVarDouble SpecificHeatConsumption {
         get { return specificHeatConsumption; }
      }

      public ProcessVarDouble MoistureEvaporationRate {
         get { return moistureEvaporationRate; }
      }

      public ProcessVarDouble FractionOfMaterialLostToGasOutlet {
         get { return fractionOfMaterialLostToGasOutlet; }
      }

      public ProcessVarDouble GasOutletMaterialLoading {
         get { return gasOutletMaterialLoading; }
      }

      public DryerCalculationType DryerCalculationType {
         get { return dryerCalculationType; }
      }

      public DryerScopingModel ScopingModel {
         get { return scopingModel; }
      }

      #endregion

      private double ws, wg, x1, x2, y1, y2, tg1, tg2, tm1, tm2, fy2;

      protected Dryer(string name, UnitOperationSystem uoSys)
         : base(name, uoSys) {
         thermalEfficiency = new ProcessVarDouble(StringConstants.THERMAL_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, this);
         specificHeatConsumption = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_CONSUMPTION, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);
         moistureEvaporationRate = new ProcessVarDouble(StringConstants.MOISTURE_EVAPORATION_RATE, PhysicalQuantity.MassFlowRate, VarState.AlwaysCalculated, this);
         heatLossByTransportDevice = new ProcessVarDouble(StringConstants.HEAT_LOSS_BY_TRANSPORT_DEVICE, PhysicalQuantity.Power, 0.0, VarState.Specified, this);
         gasPressureDrop = new ProcessVarDouble(StringConstants.GAS_PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);

         fractionOfMaterialLostToGasOutlet = new ProcessVarDouble(StringConstants.FRACTION_OF_MATERIAL_LOST_TO_GAS_OUTLET, PhysicalQuantity.Fraction, 0.0, VarState.Specified, this);
         gasOutletMaterialLoading = new ProcessVarDouble(StringConstants.GAS_OUTLET_MATERIAL_LOADING, PhysicalQuantity.MassVolumeConcentration, VarState.AlwaysCalculated, this);

         dryerCalculationType = DryerCalculationType.Balance;

         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         //base.InitializeVarListAndRegisterVars();
         AddVarOnListAndRegisterInSystem(gasPressureDrop);
         //vars from base class UnitOperation--begin
         AddVarOnListAndRegisterInSystem(heatLoss);
         AddVarOnListAndRegisterInSystem(heatInput);
         AddVarOnListAndRegisterInSystem(workInput);
         //vars from base class UnitOperation--end
         AddVarOnListAndRegisterInSystem(heatLossByTransportDevice);
         AddVarOnListAndRegisterInSystem(moistureEvaporationRate);
         AddVarOnListAndRegisterInSystem(specificHeatConsumption);
         AddVarOnListAndRegisterInSystem(thermalEfficiency);
         AddVarOnListAndRegisterInSystem(fractionOfMaterialLostToGasOutlet);
         AddVarOnListAndRegisterInSystem(gasOutletMaterialLoading);
      }

      public ErrorMessage SpecifyDryerCalculationType(DryerCalculationType calcType) {
         ErrorMessage retMsg = null;
         if (calcType != dryerCalculationType) {
            DryerCalculationType oldValue = dryerCalculationType;
            dryerCalculationType = calcType;
            CreateCalculationModel(dryerCalculationType);
            try {
               HasBeenModified(true);
            }
            catch (Exception e) {
               dryerCalculationType = oldValue;
               retMsg = HandleException(e);
            }

         }
         return retMsg;
      }

      public override bool CanConnect(int streamIndex) {
         bool retValue = false;
         if (streamIndex == GAS_INLET_INDEX && gasInlet == null) {
            retValue = true;
         }
         else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == MATERIAL_INLET_INDEX && materialInlet == null) {
            retValue = true;
         }
         else if (streamIndex == MATERIAL_OUTLET_INDEX && materialOutlet == null) {
            retValue = true;
         }

         return retValue;
      }

      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == GAS_INLET_INDEX || streamIndex == GAS_OUTLET_INDEX) {
            DryingGasStream dgs = ps as DryingGasStream;
            if (streamIndex == GAS_INLET_INDEX) {
               gasInlet = dgs;
               ps.DownStreamOwner = this;
               inletStreams.Add(ps);
            }
            else if (streamIndex == GAS_OUTLET_INDEX) {
               gasOutlet = dgs;
               ps.UpStreamOwner = this;
               outletStreams.Add(ps);
            }
         }
         else if (streamIndex == MATERIAL_INLET_INDEX || streamIndex == MATERIAL_OUTLET_INDEX) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            if (streamIndex == MATERIAL_INLET_INDEX) {
               materialInlet = dms;
               ps.DownStreamOwner = this;
               inletStreams.Add(ps);
            }
            else if (streamIndex == MATERIAL_OUTLET_INDEX) {
               materialOutlet = dms;
               ps.UpStreamOwner = this;
               outletStreams.Add(ps);
            }
         }
         else {
            attached = false;
         }

         return attached;
      }

      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = true;
         if (ps == gasInlet) {
            gasInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == gasOutlet) {
            gasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == materialInlet) {
            materialInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == materialOutlet) {
            materialOutlet = null;
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

      protected virtual void CreateCalculationModel(DryerCalculationType dryerCalculationType) {
         if (dryerCalculationType == DryerCalculationType.Scoping && scopingModel == null) {
            scopingModel = new DryerScopingModel(this);
         }
         //else if (dryerCalculationType == DryerCalculationType.Rating) {
         //   CreateRatingModel(dryerType);
         //}
         //else if (dryerCalculationType == DryerCalculationType.ScalingUp) {
         //}
      }

      //private void CreateRatingModel(DryerType dryerType) {
      //}

      //private void OnProcessTypeChanged(Dryer dryer, ProcessType processType) {
      //   if (ProcessTypeChanged != null) {
      //      ProcessTypeChanged(dryer, processType);
      //   }
      //}
            
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == gasPressureDrop) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == fractionOfMaterialLostToGasOutlet) {
            if (aValue < 0.0 || aValue > 1.0) {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
         }
         else if (pv == heatLossByTransportDevice) {
            if (aValue < 0.0) {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
         }

         if (retValue == null && dryerCalculationType == DryerCalculationType.Scoping) {
            retValue = scopingModel.CheckSpecifiedValueRange(pv, aValue);
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == gasInlet.Pressure && gasOutlet.Pressure.IsSpecifiedAndHasValue && aValue < gasOutlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of gas inlet cannot be less than that of gas outlet.");
         }
         else if (pv == gasOutlet.Pressure && gasInlet.Pressure.IsSpecifiedAndHasValue && aValue > gasInlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of gas outlet cannot be greater than that of gas inlet.");
         }
         else if (pv == gasInlet.Humidity && gasOutlet.Humidity.IsSpecifiedAndHasValue && aValue > gasOutlet.Humidity.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of gas inlet cannot be greater than that of gas outlet.");
         }
         else if (pv == gasOutlet.Humidity && gasInlet.Humidity.IsSpecifiedAndHasValue && aValue < gasInlet.Humidity.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of gas outlet cannot be less than than that of gas inlet.");
         }
         else if (pv == materialInlet.MoistureContentWetBase && materialOutlet.MoistureContentWetBase.IsSpecifiedAndHasValue && aValue < materialOutlet.MoistureContentWetBase.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of material inlet cannot be less than that of material outlet.");
         }
         else if (pv == materialOutlet.MoistureContentWetBase && materialInlet.MoistureContentWetBase.IsSpecifiedAndHasValue && aValue > materialInlet.MoistureContentWetBase.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of material outlet cannot be greater than than that of material inlet.");
         }
         return retValue;
      }

      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (gasInlet == null || gasOutlet == null || materialInlet == null || materialOutlet == null) {
            isReady = false;
         }

         return isReady;
      }

      protected override void BalanceDryingMaterialStreamFlow(DryingMaterialStream inlet, DryingMaterialStream outlet) {
         //dry materail flow balance
         if (fractionOfMaterialLostToGasOutlet.HasValue) {
            if (inlet.MassFlowRateDryBase.HasValue && !outlet.MassFlowRateDryBase.HasValue) {
               double outletMassFlowRateDryBase = inlet.MassFlowRateDryBase.Value * (1.0 - fractionOfMaterialLostToGasOutlet.Value);
               Calculate(outlet.MassFlowRateDryBase, outletMassFlowRateDryBase);
            }
            else if (outlet.MassFlowRateDryBase.HasValue && !inlet.MassFlowRateDryBase.HasValue) {
               double inletMassFlowRateDryBase = outlet.MassFlowRateDryBase.Value / (1.0 - fractionOfMaterialLostToGasOutlet.Value);
               Calculate(inlet.MassFlowRateDryBase, inletMassFlowRateDryBase);
            }
         }
      }

      private void CalculateMaterialOutletMassFlowRateDryBase(ProcessVarDouble pv, double ws) {
         if (fractionOfMaterialLostToGasOutlet.HasValue) {
            Calculate(pv, ws * (1.0 - fractionOfMaterialLostToGasOutlet.Value));
         }
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         
         if (!heatLoss.HasValue || !heatInput.HasValue || !heatLossByTransportDevice.HasValue || !workInput.HasValue) {
            return isReady;
         }

         wg = gasInlet.MassFlowRateDryBase.Value;
         if (wg == Constants.NO_VALUE) {
            wg = gasOutlet.MassFlowRateDryBase.Value;
         }
         tg1 = gasInlet.Temperature.Value;
         tg2 = gasOutlet.Temperature.Value;
         y1 = gasInlet.Humidity.Value;
         y2 = gasOutlet.Humidity.Value;
         fy2 = gasOutlet.RelativeHumidity.Value;

         tm1 = materialInlet.Temperature.Value;
         tm2 = materialOutlet.Temperature.Value;
         ws = materialInlet.MassFlowRateDryBase.Value;
         if (ws == Constants.NO_VALUE && fractionOfMaterialLostToGasOutlet.HasValue
            && materialOutlet.MassFlowRateDryBase.HasValue) {
            ws = materialOutlet.MassFlowRateDryBase.Value / (1.0 - fractionOfMaterialLostToGasOutlet.Value);
         }

         x1 = materialInlet.MoistureContentDryBase.Value;
         x2 = materialOutlet.MoistureContentDryBase.Value;

         if (!HasSolvedAlready &&
            //1-1
            (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //1-2
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE) ||
            //1-3
            (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //1-4
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //1-5
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //1-6
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //2-1
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //2-2
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //2-3
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //2-5
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //2-6
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && y1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //3-1
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //3-2
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //3-5
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //3-6
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-1
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //4-2
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-3
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && y1 != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-4
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-5
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-6
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //4-7
            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-1
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //5-2
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-3
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && y1 != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-4
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-5
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-6
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //5-7
            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //6-1
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE)
            //6-2
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tg2 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //6-4
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //6-5
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //6-6
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)
            //6-7
            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)

            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && fy2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)

            || (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && fy2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)

            || (ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && fy2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)

            || (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && y1 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && fy2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE)

            ) {

            isReady = true;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         //balance gas stream flow
         BalanceDryingGasStreamFlow(gasInlet, gasOutlet);
         //balance material stream flwow
         BalanceDryingMaterialStreamFlow(materialInlet, materialOutlet);

         //balance pressure
         BalancePressure(gasInlet, gasOutlet, gasPressureDrop);

         if (IsSolveReady()) {
            Solve();
         }

         if (HasSolvedAlready && dryerCalculationType == DryerCalculationType.Scoping) {
            gasInlet.Execute(false);
            scopingModel.DoScopingCalculation();
         }

         AdjustVarsStates(gasInlet, gasOutlet);
         AdjustVarsStates(materialInlet, materialOutlet);

         PostSolve();
      }

      private void Solve() {
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         double p2 = gasOutlet.Pressure.Value;
         double cs = materialInlet.GetCpOfAbsoluteDryMaterial();
         double cw = humidGasCalculator.GetSpecificHeatOfLiquid();
         double cgd = humidGasCalculator.GetSpecificHeatOfDryGas();
         double cv = humidGasCalculator.GetSpecificHeatOfVapor();
         double r0 = humidGasCalculator.GetEvaporationHeat(273.15);
         double q3 = heatLoss.Value + heatLossByTransportDevice.Value;
         double q4 = heatInput.Value;
         double q5 = workInput.Value;
         double w = Constants.NO_VALUE;
         double cm, q1, q2, cg1, i2, em;
         ////double td;
         int counter = 0;
         //tg2, y1; tg1, y1; tg2, y2; tg1, y2 (1-5, 1-6, 2-5, 2-6)
         if (ws != Constants.NO_VALUE && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE
            && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && cs != Constants.NO_VALUE && (y1 != Constants.NO_VALUE || y2 != Constants.NO_VALUE)
            && (tg1 != Constants.NO_VALUE || tg2 != Constants.NO_VALUE)) {

            w = ws * (x1 - x2);
            if (y1 != Constants.NO_VALUE) {
               y2 = y1 + w / wg;
               Calculate(gasOutlet.MoistureContentDryBase, y2);
            }
            else if (y2 != Constants.NO_VALUE) {
               y1 = y2 - w / wg;
               Calculate(gasInlet.MoistureContentDryBase, y1);
            }

            cm = cs + cw * x2;
            q2 = ws * cm * (tm2 - tm1);
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            if (tg1 != Constants.NO_VALUE) {
               tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
               Calculate(gasOutlet.Temperature, tg2);
               CorrectOutletHumidityIfNecessary(y2, tg2, p2);
               solveState = SolveState.Solved;
            }
            else if (tg2 != Constants.NO_VALUE) {
               i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
               q1 = w * (i2 - cw * (tm1 - 273.15));
               tg1 = CalculateTg1(q1, q2, q3 - q4 - q5, wg, cg1, tg2);
               Calculate(gasInlet.Temperature, tg1);
               solveState = SolveState.Solved;
            }
         }
         //tm1, y1; tm2, y1; tm1, y2; tm2, y2 (1-1, 1-2, 2-1, 2-2)
         else if (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && cs != Constants.NO_VALUE && (y1 != Constants.NO_VALUE || y2 != Constants.NO_VALUE)
            && (tm1 != Constants.NO_VALUE || tm2 != Constants.NO_VALUE)) {

            w = ws * (x1 - x2);
            if (y1 != Constants.NO_VALUE) {
               y2 = y1 + w / wg;
               Calculate(gasOutlet.MoistureContentDryBase, y2);
               CorrectOutletHumidityIfNecessary(y2, tg2, p2);
            }
            else if (y2 != Constants.NO_VALUE) {
               y1 = y2 - w / wg;
               Calculate(gasInlet.MoistureContentDryBase, y1);
            }

            cm = cs + cw * x2;
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);

            if (tm1 != Constants.NO_VALUE) {
               q1 = w * (i2 - cw * (tm1 - 273.15));
               q2 = wg * cg1 * (tg1 - tg2) - q1 - (q3 - q4 - q5);
               tm2 = tm1 + q2 / (ws * cm);
               Calculate(materialOutlet.Temperature, tm2);
               solveState = SolveState.Solved;
            }
            else if (tm2 != Constants.NO_VALUE) {
               tm1 = (w * i2 + w * cw * 273.15 + ws * cm * tm2 - wg * cg1 * (tg1 - tg2)) / (w * cw + ws * cm);
               Calculate(materialInlet.Temperature, tm1);
               solveState = SolveState.Solved;
            }
         }
         //tm1, x1; tm1, x2; tm2, x1; tm2, x2 (4-1, 4-2, 5-1, 5-2)
         else if (ws != Constants.NO_VALUE && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && cs != Constants.NO_VALUE && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)
            && (tm1 != Constants.NO_VALUE || tm2 != Constants.NO_VALUE)) {

            w = wg * (y2 - y1);
            if (x2 != Constants.NO_VALUE) {
               x1 = x2 + w / ws;
               Calculate(materialInlet.MoistureContentDryBase, x1);
            }
            else if (x1 != Constants.NO_VALUE) {
               x2 = x1 - w / ws;
               Calculate(materialOutlet.MoistureContentDryBase, x2);
            }

            cm = cs + cw * x2;
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);

            if (tm1 != Constants.NO_VALUE) {
               q1 = w * (i2 - cw * (tm1 - 273.15));
               q2 = wg * cg1 * (tg1 - tg2) - q1 - (q3 - q4 - q5);
               tm2 = tm1 + q2 / (ws * cm);
               Calculate(materialOutlet.Temperature, tm2);
               solveState = SolveState.Solved;
            }
            else if (tm2 != Constants.NO_VALUE) {
               tm1 = (w * i2 + w * cw * 273.15 + ws * cm * tm2 - wg * cg1 * (tg1 - tg2)) / (w * cw + ws * cm);
               Calculate(materialInlet.Temperature, tm1);
               solveState = SolveState.Solved;
            }
         }
         //tm1, ws; tm1, wg; tm2, ws; tm2, wg (3-1, 3-2, 6-1, 6-2)
         else if (y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && x1 != Constants.NO_VALUE
            && x2 != Constants.NO_VALUE && tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE
            && cs != Constants.NO_VALUE && (wg != Constants.NO_VALUE || ws != Constants.NO_VALUE)
            && (tm1 != Constants.NO_VALUE || tm2 != Constants.NO_VALUE)) {

            if (wg != Constants.NO_VALUE) {
               w = wg * (y2 - y1);
               ws = w / (x1 - x2);
               Calculate(materialInlet.MassFlowRateDryBase, ws);
               //Calculate(materialOutlet.MassFlowRateDryBase, ws);
               CalculateMaterialOutletMassFlowRateDryBase(materialOutlet.MassFlowRateDryBase, ws);
            }
            else if (ws != Constants.NO_VALUE) {
               w = ws * (x1 - x2);
               wg = w / (y2 - y1);
               Calculate(gasInlet.MassFlowRateDryBase, wg);
               Calculate(gasOutlet.MassFlowRateDryBase, wg);
            }

            cm = cs + cw * x2;
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);

            if (tm1 != Constants.NO_VALUE) {
               q1 = w * (i2 - cw * (tm1 - 273.15));
               q2 = wg * cg1 * (tg1 - tg2) - q1 - (q3 - q4 - q5);
               tm2 = tm1 + q2 / (ws * cm);
               Calculate(materialOutlet.Temperature, tm2);
               solveState = SolveState.Solved;
            }
            else if (tm2 != Constants.NO_VALUE) {
               tm1 = (w * i2 + w * cw * 273.15 + ws * cm * tm2 - wg * cg1 * (tg1 - tg2)) / (w * cw + ws * cm);
               Calculate(materialInlet.Temperature, tm1);
               solveState = SolveState.Solved;
            }
         }
         //wg, x1; wg, x2 (4-3, 5-3)
         else if (tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE && y1 != Constants.NO_VALUE
            && y2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE
            && ws != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)) {

            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            em = (i2 - cw * (tm1 - 273.15));

            //wg, x1 (5-3)
            if (x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               double wg_old;
               wg = 1.0;
               counter = 0;
               do {
                  counter++;
                  wg_old = wg;
                  w = wg * (y2 - y1);
                  q1 = w * em;
                  q2 = ws * cm * (tm2 - tm1);
                  wg = (q1 + q2 + q3 - q4 - q5) / (tg1 - tg2) / cg1;
               } while (Math.Abs(wg - wg_old) > 1.0e-6 && counter <= 500);

               if (counter < 500) {
                  x1 = w / ws + x2;
                  Calculate(materialInlet.MoistureContentDryBase, x1);
                  Calculate(gasInlet.MassFlowRateDryBase, wg);
                  Calculate(gasOutlet.MassFlowRateDryBase, wg);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }

            }
            //wg, x2 (4-3)
            else if (x1 != Constants.NO_VALUE) {
               double wg_old;
               wg = 1.0;
               cm = cs;
               counter = 0;
               do {
                  counter++;
                  wg_old = wg;
                  w = wg * (y2 - y1);
                  q1 = w * em;
                  q2 = ws * cm * (tm2 - tm1);
                  wg = (q1 + q2 + q3 - q4 - q5) / (tg1 - tg2) / cg1;
                  x2 = x1 - w / ws;
                  cm = cs + cw * x2;
               } while (Math.Abs(wg - wg_old) > 1.0e-6 && counter <= 500);

               if (counter < 500) {
                  Calculate(materialOutlet.MoistureContentDryBase, x2);
                  Calculate(gasInlet.MassFlowRateDryBase, wg);
                  Calculate(gasOutlet.MassFlowRateDryBase, wg);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
         }
         //ws, y1; ws, y2 (6-4, 6-7)
         else if (tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE
            && tm2 != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && wg != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (y1 != Constants.NO_VALUE || y2 != Constants.NO_VALUE)) {

            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            em = (i2 - cw * (tm1 - 273.15));
            cm = cs + cw * x2;
            double cq1 = wg * (i2 - cw * (tm1 - 273.15));
            double cq2 = wg * cm * (tm2 - tm1) / (x1 - x2);

            ///subcase 1-4  ws, y2 (6-7)
            if (y1 != Constants.NO_VALUE) {
               y2 = (wg * cg1 * (tg1 - tg2) - (q3 - q4 - q5) + y1 * (cq1 + cq2)) / (cq1 + cq2);
               Calculate(gasOutlet.MoistureContentDryBase, y2);
               CorrectOutletHumidityIfNecessary(y2, tg2, p2);
            }
            ///subcase 2-4  ws, y1 6-4
            else if (y2 != Constants.NO_VALUE) {
               y1 = (y2 * (cq1 + cq2) + (q3 - q4 - q5) - wg * (tg1 - tg2) * cgd) / (wg * (tg1 - tg2) * cv + cq1 + cq2);
               Calculate(gasInlet.MoistureContentDryBase, y1);
            }
            w = wg * (y2 - y1);
            ws = w / (x1 - x2);
            Calculate(materialInlet.MassFlowRateDryBase, ws);
            //Calculate(materialOutlet.MassFlowRateDryBase, ws);
            CalculateMaterialOutletMassFlowRateDryBase(materialOutlet.MassFlowRateDryBase, ws);
            solveState = SolveState.Solved;
         }
         //y1, x1; y2, x2 (4-7, 5-7)
         else if (tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE && y1 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && ws != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)) {

            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            em = (i2 - cw * (tm1 - 273.15));

            ///subcase 1-2 (y2, x2) 4-7 
            if (x1 != Constants.NO_VALUE) {
               double cm_old;
               counter = 0;
               cm = cs;
               do {
                  counter++;
                  q2 = ws * cm * (tm2 - tm1);
                  y2 = (wg * cg1 * (tg1 - tg2) - q2 - (q3 - q4 - q5)) / (wg * em) + y1;
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w) / ws;
                  cm_old = cm;
                  cm = cs + cw * x2;
               } while (Math.Abs(cm - cm_old) > 1.0e-6 && counter <= 50);

               if (counter < 50) {
                  Calculate(gasOutlet.MoistureContentDryBase, y2);
                  CorrectOutletHumidityIfNecessary(y2, tg2, p2);
                  Calculate(materialOutlet.MoistureContentDryBase, x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }

            }
            ///subcase 1-3  (y2, x1) 5-7
            else if (x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               q2 = ws * cm * (tm2 - tm1);
               y2 = (wg * cg1 * (tg1 - tg2) - q2 - (q3 - q4 - q5)) / (wg * em) + y1;
               w = wg * (y2 - y1);
               x1 = (w + ws * x2) / ws;

               Calculate(gasOutlet.MoistureContentDryBase, y2);
               CorrectOutletHumidityIfNecessary(y2, tg2, p2);
               Calculate(materialInlet.MoistureContentDryBase, x1);
               solveState = SolveState.Solved;
            }
         }
         //wg, y1; wg, y2 (1-3, 2-3)
         else if (tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE && ws != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && x1 != Constants.NO_VALUE
            && x2 != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (y1 != Constants.NO_VALUE || y2 != Constants.NO_VALUE)) {

            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            em = (i2 - cw * (tm1 - 273.15));
            cm = cs + cw * x2;
            w = ws * (x1 - x2);
            q1 = w * em;
            q2 = ws * cm * (tm2 - tm1);
            ///2-1  (wg, y1) 1-3
            if (y2 != Constants.NO_VALUE) {
               double qt = q1 + q2 + q3 - q4 - q5;
               y1 = (qt * y2 - (tg1 - tg2) * w * cgd) / ((tg1 - tg2) * w * cv + qt);
               wg = w / (y2 - y1);
               Calculate(gasInlet.MoistureContentDryBase, y1);
            }
            ///subcase 1-1 wg, y2 (2-3)
            if (y1 != Constants.NO_VALUE) {
               cg1 = humidGasCalculator.GetHumidHeat(y1);
               wg = (q1 + q2 + q3 - q4 - q5) / (tg1 - tg2) / cg1;
               y2 = y1 + w / wg;
               Calculate(gasOutlet.MoistureContentDryBase, y2);
               CorrectOutletHumidityIfNecessary(y2, tg2, p2);
            }
            Calculate(gasInlet.MassFlowRateDryBase, wg);
            Calculate(gasOutlet.MassFlowRateDryBase, wg);
            solveState = SolveState.Solved;
         }
         //y1, x1; y1, x2 (4-4, 5-4)
         else if (tg1 != Constants.NO_VALUE && tg2 != Constants.NO_VALUE && y2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && ws != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)) {

            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            em = (i2 - cw * (tm1 - 273.15));

            ///subcase 2-2  (y1, x2) 4-4 
            if (x1 != Constants.NO_VALUE) {
               double cm_old;
               counter = 0;
               cm = cs;
               do {
                  counter++;
                  q2 = ws * cm * (tm2 - tm1);
                  y1 = (wg * em * y2 + q2 + q3 - q4 - q5 - wg * (tg1 - tg2) * cgd) / (wg * (tg1 - tg2) * cv + wg * em);
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w) / ws;
                  cm_old = cm;
                  cm = cs + cw * x2;
               } while (Math.Abs(cm - cm_old) > 1.0e-6 && counter <= 50);

               if (counter < 50) {
                  Calculate(gasInlet.MoistureContentDryBase, y1);
                  Calculate(materialOutlet.MoistureContentDryBase, x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
            ///subcase 2-3  (y1, x1) 5-4
            else if (x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               q2 = ws * cm * (tm2 - tm1);
               y1 = (wg * em * y2 + q2 + q3 - q4 - q5 - wg * (tg1 - tg2) * cgd) / (wg * (tg1 - tg2) * cv + wg * em);
               w = wg * (y2 - y1);
               x1 = (ws * x2 + w) / ws;

               Calculate(gasInlet.MoistureContentDryBase, y1);
               Calculate(materialInlet.MoistureContentDryBase, x1);
               solveState = SolveState.Solved;
            }
         }
         //tg1, wg; tg2; wg, tg1, ws; tg2, ws (3-5, 3-6, 6-5, 6-6)
         else if (y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE
            && tm2 != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE
            && cs != Constants.NO_VALUE && (tg1 != Constants.NO_VALUE || tg2 != Constants.NO_VALUE)
            && (wg != Constants.NO_VALUE || ws != Constants.NO_VALUE)) {

            ///subcase 3-1  (tg2, wg) 3-5
            if (ws != Constants.NO_VALUE) {
               w = ws * (x1 - x2);
               wg = w / (y2 - y1);
               Calculate(gasInlet.MassFlowRateDryBase, wg);
               Calculate(gasOutlet.MassFlowRateDryBase, wg);
            }
            ///subcase 3-4 (tg2, ws) 6-5
            else if (wg != Constants.NO_VALUE) {
               w = wg * (y2 - y1);
               ws = w / (x1 - x2);
               Calculate(materialInlet.MassFlowRateDryBase, ws);
               //Calculate(materialOutlet.MassFlowRateDryBase, ws);
               CalculateMaterialOutletMassFlowRateDryBase(materialOutlet.MassFlowRateDryBase, ws);
            }

            cm = cs + cw * x2;
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            q2 = ws * cm * (tm2 - tm1);
            ///subcase 5-1 (tg2, wg) 6-6
            if (tg1 != Constants.NO_VALUE) {
               tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
               Calculate(gasOutlet.Temperature, tg2);
               solveState = SolveState.Solved;
            }
            ///subcase 5-4 (tg2, ws) 3-6
            else if (tg2 != Constants.NO_VALUE) {
               i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
               q1 = w * (i2 - cw * (tm1 - 273.15));
               tg1 = CalculateTg1(q1, q2, q3 - q4 - q5, wg, cg1, tg2);
               Calculate(gasInlet.Temperature, tg1);
               solveState = SolveState.Solved;
            }
         }
         //tg2, x1; tg2, x2 (4-5, 5-5)
         else if (tg1 != Constants.NO_VALUE && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && ws != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)) {

            cg1 = humidGasCalculator.GetHumidHeat(y1);

            ///subcase 3-2  (tg2, x2) 4-5 
            if (x1 != Constants.NO_VALUE) {
               double cm_old;
               counter = 0;
               cm = cs;
               do {
                  counter++;
                  q2 = ws * cm * (tm2 - tm1);
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w) / ws;
                  //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
                  tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
                  cm_old = cm;
                  cm = cs + cw * x2;
               } while (Math.Abs(cm - cm_old) < 1.0e-6 && counter <= 50);
               if (counter < 50) {
                  Calculate(gasOutlet.Temperature, tg2);
                  Calculate(materialOutlet.MoistureContentDryBase, x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
            ///subcase 3-3 (tg2, x1) 5-5
            else if (x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               q2 = ws * cm * (tm2 - tm1);
               w = wg * (y2 - y1);
               x1 = (ws * x2 + w) / ws;
               //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
               tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);

               Calculate(gasOutlet.Temperature, tg2);
               Calculate(materialInlet.MoistureContentDryBase, x1);
               solveState = SolveState.Solved;
            }
         }
         //tg1, x1; tg1, x2 (4-6, 5-6)
         else if (tg2 != Constants.NO_VALUE && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && wg != Constants.NO_VALUE
            && ws != Constants.NO_VALUE && cs != Constants.NO_VALUE
            && (x1 != Constants.NO_VALUE || x2 != Constants.NO_VALUE)) {

            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            w = wg * (y2 - y1);
            q1 = w * (i2 - cw * (tm1 - 273.15));

            ///subcase 5-2  (tg1, x2) 4-6 
            if (x1 != Constants.NO_VALUE) {
               x2 = (ws * x1 - w) / ws;
               Calculate(materialOutlet.MoistureContentDryBase, x2);
            }
            ///subcase 5-3  (tg1, x1) 5-6
            else if (x2 != Constants.NO_VALUE) {
               x1 = (ws * x2 + w) / ws;
               Calculate(materialInlet.MoistureContentDryBase, x1);
            }
            cm = cs + cw * x2;
            q2 = ws * cm * (tm2 - tm1);
            tg1 = CalculateTg1(q1, q2, q3 - q4 - q5, wg, cg1, tg2);
            Calculate(gasInlet.Temperature, tg1);
            solveState = SolveState.Solved;
         }
         ///case 8
         else if (tg1 != Constants.NO_VALUE && y1 != Constants.NO_VALUE && fy2 != Constants.NO_VALUE
            && p2 != Constants.NO_VALUE && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE
            && cs != Constants.NO_VALUE) {

            double tg2_old;
            double delta = 10.0;
            double totalDelta = delta;
            tg2 = tg1 - delta;
            bool negativeLastTime = false;

            cg1 = humidGasCalculator.GetHumidHeat(y1);
            ///subcase 4-1  (wg, tg2, y2) 8-1
            if (ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
               counter = 0;
               do {
                  counter++;
                  y2 = CalculateHumidity(tg2, fy2, p2);
                  cm = cs + cw * x2;
                  q2 = ws * cm * (tm2 - tm1);
                  w = ws * (x1 - x2);
                  wg = w / (y2 - y1);
                  tg2_old = tg2;
                  //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
                  tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
                  ModifyDelta(tg1, tg2_old, ref tg2, ref negativeLastTime, ref delta, ref totalDelta);
               } while (Math.Abs(tg2 - tg2_old)/tg2 > 1.0e-8 && counter <= 200);

               if (counter < 200) {
                  Calculate(gasOutlet.Temperature, tg2);
                  y2 = CalculateHumidity(tg2, fy2, p2);
                  Calculate(gasOutlet.MoistureContentDryBase, y2);
                  Calculate(gasInlet.MassFlowRateDryBase, wg);
                  Calculate(gasOutlet.MassFlowRateDryBase, wg);
                  w = ws * (x1 - x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
            ///subcase 4-2 (x2, tg2, y2) 8-2  
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE) {
               double cm_old;
               cm = cs;
               counter = 0;

               do {
                  counter++;
                  y2 = CalculateHumidity(tg2, fy2, p2);
                  q2 = ws * cm * (tm2 - tm1);
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w) / ws;
                  tg2_old = tg2;
                  //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
                  tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
                  cm_old = cm;
                  cm = cs + cw * x2;
                  ModifyDelta(tg1, tg2_old, ref tg2, ref negativeLastTime, ref delta, ref totalDelta);
               } while (Math.Abs(cm - cm_old) > 1.0e-6 && Math.Abs(tg2 - tg2_old) / tg2 > 1.0e-8 && counter <= 200);

               if (counter < 200) {
                  Calculate(gasOutlet.Temperature, tg2);
                  Calculate(gasOutlet.MoistureContentDryBase, y2);
                  Calculate(materialOutlet.MoistureContentDryBase, x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
            ///subcase 4-3  (x1, tg2, y2) 8-3
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
               tg2 = tg1 - 10.0;
               counter = 0;
               do {
                  counter++;
                  y2 = CalculateHumidity(tg2, fy2, p2);
                  cm = cs + cw * x2;
                  q2 = ws * cm * (tm2 - tm1);
                  w = wg * (y2 - y1);
                  x1 = (ws * x2 + w) / ws;
                  tg2_old = tg2;
                  //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
                  tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);

                  ModifyDelta(tg1, tg2_old, ref tg2, ref negativeLastTime, ref delta, ref totalDelta);
               } while (Math.Abs(tg2 - tg2_old) / tg2 > 1.0e-8 && counter <= 200);

               if (counter < 200) {
                  Calculate(gasOutlet.Temperature, tg2);
                  Calculate(gasOutlet.MoistureContentDryBase, y2);
                  Calculate(materialInlet.MoistureContentDryBase, x1);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
            ///subcase 4-4 (tg2, y2, ws) 8-4 
            else if (wg != Constants.NO_VALUE && x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
               tg2 = tg1 - 10.0;
               counter = 0;
               do {
                  counter++;
                  y2 = CalculateHumidity(tg2, fy2, p2);
                  cm = cs + cw * x2;
                  w = wg * (y2 - y1);
                  ws = w / (x1 - x2);
                  q2 = ws * cm * (tm2 - tm1);
                  tg2_old = tg2;
                  //tg2 = (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3)/(cv * w + wg * cg1);
                  tg2 = CalculateTg2(wg, cg1, tg1, w, r0, cv, cw, tm1, q2, q3 - q4 - q5);
                  ModifyDelta(tg1, tg2_old, ref tg2, ref negativeLastTime, ref delta, ref totalDelta);
               } while (Math.Abs(tg2 - tg2_old) / tg2 > 1.0e-8 && counter <= 50);

               if (counter < 50) {
                  Calculate(gasOutlet.Temperature, tg2);
                  Calculate(gasOutlet.MoistureContentDryBase, y2);
                  Calculate(materialInlet.MassFlowRateDryBase, ws);
                  //Calculate(materialOutlet.MassFlowRateDryBase, ws);
                  CalculateMaterialOutletMassFlowRateDryBase(materialOutlet.MassFlowRateDryBase, ws);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }
            }
         }

         if (solveState == SolveState.Solved) {
            y1 = gasInlet.Humidity.Value;
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            tg1 = gasInlet.Temperature.Value;
            wg = gasInlet.MassFlowRateDryBase.Value;
            double tg0 = 293.15;
            //UnitOperation uo = gasInlet.UpStreamOwner;
            //if (uo != null && uo is Heater) {
            //   Heater heater = uo as Heater;
            //   if (heater.Inlet.Temperature.HasValue) {
            //      tg0 = heater.Inlet.Temperature.Value;
            //   }
            //}
            double q0 = wg * cg1 * (tg1 - tg0);
            double totalHeat = q0 + q4;
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            q1 = w * (i2 - cw * (tm1 - 273.15));
            Calculate(specificHeatConsumption, totalHeat / w);
            Calculate(thermalEfficiency, q1 / totalHeat);
            Calculate(moistureEvaporationRate, w);
            CalculateMaterialOutletComponents();
         }
      }

      private void CalculateMaterialOutletComponents() {
         double massFlowRateOfMaterialLost = 0.0;
         DryingGasComponents dgc = gasOutlet.GasComponents;
         double materialOutMassFlow = materialOutlet.MassFlowRate.Value;
         double materialMoistureContent = materialOutlet.MoistureContentDryBase.Value;
         if (materialMoistureContent != Constants.NO_VALUE && ws != Constants.NO_VALUE) {
            materialOutMassFlow = ws * (1.0 + materialMoistureContent);
         }

         double gasOutMassFlow = gasOutlet.MassFlowRate.Value;
         double gasMoistureContent = gasOutlet.Humidity.Value;
         if (gasOutMassFlow == Constants.NO_VALUE && gasMoistureContent != Constants.NO_VALUE && wg != Constants.NO_VALUE) {
            gasOutMassFlow = wg * (1.0 + gasMoistureContent);
         }

         if (materialOutMassFlow != Constants.NO_VALUE && fractionOfMaterialLostToGasOutlet.HasValue) {
            massFlowRateOfMaterialLost = materialOutMassFlow * fractionOfMaterialLostToGasOutlet.Value;

            if (gasOutMassFlow != Constants.NO_VALUE) {
               Calculate(gasOutletMaterialLoading, massFlowRateOfMaterialLost / gasOutMassFlow);
            }

            if (dgc.NumberOfPhases <= 1) {
               ArrayList solidCompList = new ArrayList();
               MaterialComponent mc = new MaterialComponent(dgc.AbsoluteDryMaterial.Substance);
               mc.SetMassFractionValue(1.0 - materialOutlet.MoistureContentWetBase.Value);
               solidCompList.Add(mc);
               mc = new MaterialComponent(dgc.Moisture.Substance);
               mc.SetMassFractionValue(materialOutlet.MoistureContentWetBase.Value);
               solidCompList.Add(mc);
               SolidPhase sp = new SolidPhase("Drying Gas Solid Phase", solidCompList);
               sp.MassFraction = massFlowRateOfMaterialLost / gasOutMassFlow;
               dgc.AddPhase(sp);
            }
            else {
               SolidPhase sp = dgc.SolidPhase;
               sp.MassFraction = massFlowRateOfMaterialLost / gasOutMassFlow;
            }
         }
      }

      private void ModifyDelta(double tg1, double tg2_old, ref double tg2, ref bool negativeLastTime, ref double delta, ref double totalDelta) {
         if ((tg2 - tg2_old) < 0) {
            if (negativeLastTime) {
               delta /= 2.0; //testing finds delta/2.0 is almost optimal
            }
            totalDelta += delta;
            negativeLastTime = false;
         }
         else if ((tg2 - tg2_old) > 0) {
            delta /= 2.0; //testing finds delta/2.0 is almost optimal
            totalDelta -= delta;
            negativeLastTime = true;
         }
         tg2 = tg1 - totalDelta;
      }

      private double CalculateTg2(double wg, double cg1, double tg1, double w, double r0, double cv, double cw, double tm1, double q2, double q3) {
         return (wg * cg1 * tg1 - w * r0 + cv * w * 273.15 + w * cw * (tm1 - 273.15) - q2 - q3) / (cv * w + wg * cg1);
      }

      private double CalculateTg1(double q1, double q2, double q3, double wg, double cg1, double tg2) {
         return (q1 + q2 + q3 + wg * cg1 * tg2) / (wg * cg1);
      }

      private double CalculateHumidity(double tg, double fy, double p) {
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         double td = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(tg, fy);
         return humidGasCalculator.GetHumidityFromDewPointAndPressure(td, p);
      }

      private void CorrectOutletHumidityIfNecessary(double y2, double tg2, double p2) {
         double saturationHumidity = GetHumidGasCalculator().GetHumidityFromDewPointAndPressure(tg2, p2);
         if (y2 >= saturationHumidity) {
            gasOutlet.MoistureContentDryBase.Value = 0.99999 * saturationHumidity;
         }
      }

      protected Dryer(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryer", typeof(int));
         if (persistedClassVersion == 1) {
            this.gasInlet = (DryingGasStream)info.GetValue("GasInlet", typeof(DryingGasStream));
            this.gasOutlet = (DryingGasStream)info.GetValue("GasOutlet", typeof(DryingGasStream));
            this.materialInlet = (DryingMaterialStream)info.GetValue("MaterialInlet", typeof(DryingMaterialStream));
            this.materialOutlet = (DryingMaterialStream)info.GetValue("MaterialOutlet", typeof(DryingMaterialStream));

            this.gasPressureDrop = RecallStorableObject("GasPressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.thermalEfficiency = RecallStorableObject("ThermalEfficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.specificHeatConsumption = RecallStorableObject("SpecificHeatConsumption", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.moistureEvaporationRate = RecallStorableObject("MoistureEvaporationRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.heatLossByTransportDevice = RecallStorableObject("HeatLossByTransportDevice", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.fractionOfMaterialLostToGasOutlet = RecallStorableObject("FractionOfMaterialLostToGasOutlet", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.gasOutletMaterialLoading = RecallStorableObject("GasOutletMaterialLoading", typeof(ProcessVarDouble)) as ProcessVarDouble;

            //this.dryerType = (DryerType) info.GetValue("DryerType", typeof(DryerType));
            this.dryerCalculationType = (DryerCalculationType)info.GetValue("DryerCalculationType", typeof(DryerCalculationType));
            this.scopingModel = RecallStorableObject("ScopingModel", typeof(DryerScopingModel)) as DryerScopingModel;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryer", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("GasInlet", this.gasInlet, typeof(DryingGasStream));
         info.AddValue("GasOutlet", this.gasOutlet, typeof(DryingGasStream));
         info.AddValue("MaterialInlet", this.materialInlet, typeof(DryingMaterialStream));
         info.AddValue("MaterialOutlet", this.materialOutlet, typeof(DryingMaterialStream));

         info.AddValue("GasPressureDrop", this.gasPressureDrop, typeof(ProcessVarDouble));
         info.AddValue("ThermalEfficiency", this.thermalEfficiency, typeof(ProcessVarDouble));
         info.AddValue("SpecificHeatConsumption", this.specificHeatConsumption, typeof(ProcessVarDouble));
         info.AddValue("MoistureEvaporationRate", this.moistureEvaporationRate, typeof(ProcessVarDouble));
         info.AddValue("HeatLossByTransportDevice", this.heatLossByTransportDevice, typeof(ProcessVarDouble));
         info.AddValue("FractionOfMaterialLostToGasOutlet", this.fractionOfMaterialLostToGasOutlet, typeof(ProcessVarDouble));
         info.AddValue("GasOutletMaterialLoading", this.gasOutletMaterialLoading, typeof(ProcessVarDouble));
         //info.AddValue("DryerType", this.dryerType, typeof(DryerType));
         info.AddValue("DryerCalculationType", this.dryerCalculationType, typeof(DryerCalculationType));
         info.AddValue("ScopingModel", this.scopingModel, typeof(DryerScopingModel));
      }
   }
}

 /*else if (!double.IsNaN(tg2) && !double.IsNaN(y2) && !double.IsNaN(tm1) && 
            !double.IsNaN(tm2) && !double.IsNaN(ws) && !double.IsNaN(x1) && 
            !double.IsNaN(x2) && !double.IsNaN(cs)  && !double.IsNaN(wg)) {
            cm = cs + cw * x2;
            w = ws * (x1 - x2);
            y1 = y2 - w/wg;
            q2 = ws * cm * (tm2 - tm1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            q1 = w* (i2 - cw * (tm1 - 273.15));
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            tg1 = (q1 + q2 + q3 + wg * cg1 * tg2)/(wg * cg1);
               
            gasInlet.CalculateTemperature(tg1);
            gasInlet.CalculateHumidity(y1);
            CalculateMoistureEvaporationRate(w);
            solveState = SolveState.Solved;
         }*/
                     //wg, ws (4-3)
/*else if (x1 != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
   cm = cs + cw * x2;
   double wg_old = 1.0;
               //double diff = 0.0;
   wg = 1.0;
   counter = 0;
   do {
   counter++;
   wg_old = wg;
                  //wg = wg_old + 2.0 * diff;
   w = wg * (y2 - y1);
   ws = w/(x1 - x2);
   q1 = w * em;
   q2 = ws * cm * (tm2 - tm1);
   wg = (q1 + q2 + q3)/(tg1 - tg2)/cg1;
                  //diff = wg - wg_old;
   } while (Math.Abs(wg - wg_old) > 1.0e-6 && counter <= 10000);
               
   if (counter < 10000) {
                  //ws = w/(x1 - x2);
      materialInlet.CalculateMoistureContentDryBase(ws);
      materialOutlet.CalculateMoistureContentDryBase(ws);
      gasInlet.CalculateMassFlowRateDryBase(wg);
      gasOutlet.CalculateMassFlowRateDryBase(wg);
      solveState = SolveState.Solved;
   }
   else {
      solveState = SolveState.SolveFailed;
   }
}*/

/*
             ///subcase 1-3  (y2, x1)
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               q2 = ws * cm * (tm2 - tm1);
               y2 = (wg * cg1 * (tg1 - tg2) - q2 - q3)/(wg * em) + y1;
               w = wg * (y2 - y1);
               x1 = (w + ws * x2)/ws;
               
               gasOutlet.CalculateHumidity(y2);
               materialInlet.CalculateMoistureContentDryBase(x1);
               solveState = SolveState.Solved;
            }
            
            y1, x1
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x2 != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               q2 = ws * cm * (tm2 - tm1);
               y1 = (wg * em * y2 + q2 + q3 - wg * (tg1 - tg2) * cgd)/(wg * (tg1 - tg2) * cv + wg * em);
               w = wg * (y2 - y1);
               x1 = (ws * x2 + w)/ws;
               
               gasInlet.CalculateHumidity(y1);
               materialInlet.CalculateMoistureContentDryBase(x1);
               solveState = SolveState.Solved;
            }
            
               ///subcase 2-2  (y1, x2) 4-4 
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE) {
               double cm_old;
               counter = 0;
               cm = cs;
               do {
                  counter++;
                  q2 = ws * cm * (tm2 - tm1);
                  y1 = (wg * em * y2 + q2 + q3 - wg * (tg1 - tg2) * cgd)/(wg * (tg1 - tg2) * cv + wg * em);
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w)/ws;
                  cm_old = cm;
                  cm = cs + cw * x2;
               } while (Math.Abs(cm - cm_old) > 1.0e-6 && counter <= 50);
            
               if (counter < 50) {
                  gasInlet.CalculateHumidity(y1);
                  materialOutlet.CalculateMoistureContentDryBase(x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }

            ///subcase 1-2 (y2, x2)  
            else if (wg != Constants.NO_VALUE && ws != Constants.NO_VALUE && x1 != Constants.NO_VALUE) {
               double cm_old;
               counter = 0;
               cm = cs;
               do {
                  counter++;
                  q2 = ws * cm * (tm2 - tm1);
                  y2 = (wg * cg1 * (tg1 - tg2) - q2 - q3)/(wg * em) + y1;
                  w = wg * (y2 - y1);
                  x2 = (ws * x1 - w)/ws;
                  cm_old = cm;
                  cm = cs + cw * x2;
               } while (Math.Abs(cm - cm_old) > 1.0e-6 && counter <= 50);

               if (counter < 50) {
                  gasOutlet.CalculateHumidity(y2);
                  materialOutlet.CalculateMoistureContentDryBase(x2);
                  solveState = SolveState.Solved;
               }
               else {
                  solveState = SolveState.SolveFailed;
               }

             ///case 5    
         else if (tg2 != Constants.NO_VALUE && y1 != Constants.NO_VALUE && y2 != Constants.NO_VALUE
            && tm1 != Constants.NO_VALUE && tm2 != Constants.NO_VALUE && x1 != Constants.NO_VALUE 
            && x2 != Constants.NO_VALUE && cs != Constants.NO_VALUE) {
             
            cg1 = humidGasCalculator.GetHumidHeat(y1);
            i2 = humidGasCalculator.GetMoistureSpecificEnthalpy(tg2);
            ///subcase 5-1 (tg1, wg) 3-6
            if(ws != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               w = ws * (x1 - x2);
               wg = w/(y2 - y1);
               q1 = w * (i2 - cw * (tm1 - 273.15));
               q2 = ws * cm * (tm2 - tm1);
               //tg1 = (q1 + q2 + q3 + wg * cg1 * tg2)/(wg * cg1);
               tg1 = CalculateTg1(q1, q2, q3, wg, cg1, tg2);

               gasInlet.CalculateTemperature(tg1);
               gasInlet.CalculateMassFlowRateDryBase(wg);
               gasOutlet.CalculateMassFlowRateDryBase(wg);
               solveState = SolveState.Solved;
            }
               ///subcase 5-4 (tg1, ws) 6-6
            else if (wg != Constants.NO_VALUE) {
               cm = cs + cw * x2;
               w = wg * (y2 - y1);
               ws = w/(x1 - x2);
               q1 = w * (i2 - cw * (tm1 - 273.15));
               q2 = ws * cm * (tm2 - tm1);
               //tg1 = (q1 + q2 + q3 + wg * cg1 * tg2)/(wg * cg1);
               tg1 = CalculateTg1(q1, q2, q3, wg, cg1, tg2);

               gasInlet.CalculateTemperature(tg1);
               materialInlet.CalculateMassFlowRateDryBase(ws);
               materialOutlet.CalculateMassFlowRateDryBase(ws);
               solveState = SolveState.Solved;
            }
         }


*/

