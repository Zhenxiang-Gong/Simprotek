using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.ThermalProperties;
using Prosimo.Materials;

namespace Prosimo.UnitOperations.GasSolidSeparation 
{

   //public enum ScrubberType {Condensing = 0, General};
   
   /// <summary>
   /// Summary description for WetScrubber.
   /// </summary>
   [Serializable]
   public class WetScrubber : UnitOperation, IGasSolidSeparator 
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int GAS_INLET_INDEX = 0;
      public static int GAS_OUTLET_INDEX = 1;
      public static int LIQUID_INLET_INDEX = 2;
      public static int LIQUID_OUTLET_INDEX = 3;
      
      protected ProcessStreamBase gasInlet;
      protected ProcessStreamBase gasOutlet;
      protected ProcessStreamBase liquidInlet;
      protected ProcessStreamBase liquidOutlet;

      //private ScrubberType scrubberType;
      
      private GasSolidSeparatorBalanceModel balanceModel;
      
      private ProcessVarDouble liquidToGasVolumeRatio;
      //private ProcessVarDouble liquidRecirculationMassFlowRate;
      //private ProcessVarDouble liquidRecirculationVolumeFlowRate;


      #region public properties

      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasInlet 
      {
         get { return gasInlet; }
      }
      
      //implement interface IGasSolidSeparator
      public ProcessStreamBase GasOutlet 
      {
         get { return gasOutlet; }
      }

      public ProcessStreamBase LiquidInlet 
      {
         get { return liquidInlet; }
      }
      
      public ProcessStreamBase LiquidOutlet 
      {
         get { return liquidOutlet; }
      }

      //public ScrubberType ScrubberType 
      //{
      //   get {return scrubberType;}
      //}

      //implement interface IGasSolidSeparator
      public ProcessVarDouble GasPressureDrop 
      {
         get { return balanceModel.GasPressureDrop; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble CollectionEfficiency 
      {
         get { return balanceModel.CollectionEfficiency; }
      }

      //implement interface IGasSolidSeparator
      public ProcessVarDouble InletParticleLoading 
      {
         get { return balanceModel.InletParticleLoading; }
      }
      
      //implement interface IGasSolidSeparator
      public ProcessVarDouble OutletParticleLoading 
      {
         get { return balanceModel.OutletParticleLoading; }
      }
      
      //implement interface IGasSolidSeparator
      public ProcessVarDouble ParticleCollectionRate 
      {
         get { return balanceModel.ParticleCollectionRate; }
      }
      
      //implement interface IGasSolidSeparator
      public ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet 
      {
         get { return balanceModel.MassFlowRateOfParticleLostToGasOutlet; }
      }
      
      //implement interface IGasSolidSeparator
      public UnitOperation MyUnitOperation 
      {
         get { return this; }
      }
      
      public ProcessVarDouble LiquidToGasRatio 
      {
         get { return liquidToGasVolumeRatio;}
      }

      //public ProcessVarDouble LiquidRecirculationMassFlowRate 
      //{
      //   get { return liquidRecirculationMassFlowRate;}
      //}

      //public ProcessVarDouble LiquidRecirculationVolumeFlowRate 
      //{
      //   get { return liquidRecirculationVolumeFlowRate;}
      //}

      #endregion
      
      public WetScrubber(string name, UnitOperationSystem uoSys) : base(name, uoSys) 
      {
         //scrubberType = ScrubberType.Condensing;
         balanceModel = new GasSolidSeparatorBalanceModel(this);

         liquidToGasVolumeRatio = new ProcessVarDouble(StringConstants.LIQUID_GAS_RATIO, PhysicalQuantity.Unknown, VarState.Specified, this);
         //liquidRecirculationMassFlowRate = new ProcessVarDouble(StringConstants.LIQUID_RECIRCULATION_MASS_FLOW_RATE, PhysicalQuantity.MassFlowRate, VarState.Specified, this);
         //liquidRecirculationVolumeFlowRate = new ProcessVarDouble(StringConstants.LIQUID_RECIRCULATION_VOLUME_FLOW_RATE, PhysicalQuantity.VolumeFlowRate, VarState.Specified, this);
         
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() 
      {
         AddVarOnListAndRegisterInSystem(liquidToGasVolumeRatio);
         //AddVarOnListAndRegisterInSystem(liquidRecirculationMassFlowRate);
         //AddVarOnListAndRegisterInSystem(liquidRecirculationVolumeFlowRate);
      }

      //public ErrorMessage SpecifyScrubberType(ScrubberType type) 
      //{
      //   ErrorMessage retMsg = null;
      //   if (scrubberType != type) 
      //   {
      //      ScrubberType oldValue = scrubberType;
      //      scrubberType = type;
      //      try 
      //      {
      //         HasBeenModified(true);
      //      }
      //      catch (Exception e) 
      //      {
      //         scrubberType = oldValue;
      //         retMsg = HandleException(e);
      //      }
      //   }
      //   return retMsg;
      //}

      public override bool CanConnect(int streamIndex) 
      {
         bool retValue = false;
         if (streamIndex == GAS_INLET_INDEX && gasInlet == null) 
         {
            retValue = true;
         }
         else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) 
         {
            retValue = true;
         }
         else if (streamIndex == LIQUID_INLET_INDEX && liquidInlet == null) 
         {
            retValue = true;
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX && liquidOutlet == null) 
         {
            retValue = true;
         }
         
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) 
      {
         if (((streamIndex == GAS_INLET_INDEX || streamIndex == LIQUID_INLET_INDEX) && ps.DownStreamOwner != null)
            || ((streamIndex == GAS_OUTLET_INDEX || streamIndex == LIQUID_OUTLET_INDEX) && ps.UpStreamOwner != null)) 
         {
            return false;
         }

         bool canAttach = false;
         if (ps is DryingGasStream && (streamIndex == GAS_INLET_INDEX || streamIndex == GAS_OUTLET_INDEX)) 
         {
            if (streamIndex == GAS_INLET_INDEX && gasInlet == null) 
            {
               canAttach = true;
            }
            else if (streamIndex == GAS_OUTLET_INDEX && gasOutlet == null) 
            {
               canAttach = true;
            }
         }
         else if (ps is DryingMaterialStream && (streamIndex == LIQUID_INLET_INDEX || streamIndex == LIQUID_OUTLET_INDEX)) 
         {
            if (streamIndex == LIQUID_INLET_INDEX && liquidInlet == null) 
            {
               canAttach = true;
            }
            else if (streamIndex == LIQUID_OUTLET_INDEX && liquidOutlet == null) 
            {
               canAttach = true;
            }
         }

         return canAttach;
      }

      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) 
      {
         bool attached = true;
         if (streamIndex == GAS_INLET_INDEX) 
         {
            gasInlet = ps as DryingGasStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == GAS_OUTLET_INDEX) 
         {
            gasOutlet = ps as DryingGasStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else if (streamIndex == LIQUID_INLET_INDEX) 
         {
            liquidInlet = ps as DryingMaterialStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == LIQUID_OUTLET_INDEX) 
         {
            liquidOutlet = ps as DryingMaterialStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else 
         {
            attached = false;
         }
         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) 
      {
         bool detached = true;
         if (ps == gasInlet) 
         {
            gasInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == gasOutlet) 
         {
            gasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else if (ps == liquidInlet) 
         {
            liquidInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == liquidOutlet) 
         {
            liquidOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         } 
         else 
         {
            detached = false;
         }

         if (detached) 
         {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      //implement interface IGasSolidSeparator
      public double CalculateParticleLoading(ProcessStreamBase psb) 
      {
         DryingGasStream stream = psb as DryingGasStream;
         return balanceModel.CalculateParticleLoading(stream);
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (retValue == null) {
            if (calculationType == UnitOpCalculationType.Balance) {
               retValue = balanceModel.CheckSpecifiedValueRange(pv, aValue);
            }
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == gasInlet.Pressure && gasOutlet.Pressure.IsSpecifiedAndHasValue && aValue < gasOutlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the wet scrubber's gas inlet must be greater than that of the outlet.");
         }
         else if (pv == gasOutlet.Pressure && gasInlet.Pressure.IsSpecifiedAndHasValue && aValue > gasInlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the wet scrubber's gas outlet cannot be greater than that of the inlet.");
         }

         return retValue;
      }
      
      internal override bool IsBalanceCalcReady() 
      {
         bool isReady = true;
         if (gasInlet == null || gasOutlet == null || liquidInlet == null || liquidOutlet == null) 
         {
            isReady = false;
         }
         return isReady;
      }

      //protected override bool IsSolveReady() {
      //   return true;
      //}

      public override void Execute(bool propagate) 
      {
         PreSolve();
         //balance pressure
         BalancePressure(gasInlet, gasOutlet, GasPressureDrop);
         //balance gas stream flow
         if (gasInlet is DryingGasStream) 
         {
            DryingGasStream inlet = gasInlet as DryingGasStream;
            DryingGasStream outlet = gasOutlet as DryingGasStream;
            BalanceDryingGasStreamFlow(inlet, outlet);
         }

         UpdateStreamsIfNecessary();
         
         if (IsSolveReady()) 
         {
            Solve();
         }

         AdjustVarsStates(gasInlet as DryingGasStream, gasOutlet  as DryingGasStream);
         AdjustVarsStates(liquidInlet as DryingMaterialStream, liquidOutlet as DryingMaterialStream);

         PostSolve();
      }

      private void Solve() {
         //Mass Transfer--material particles transfer from gas stream to liquid stream
         //Mass Transfer--moisture transfers from liquid stream to gas stream
         //by an adiabaitc saturation process if ScrubberType is General. 
         DryingMaterialStream dmsInlet = liquidInlet as DryingMaterialStream;
         DryingMaterialStream dmsOutlet = liquidOutlet as DryingMaterialStream;

         DryingGasStream dgsInlet = gasInlet as DryingGasStream;
         DryingGasStream dgsOutlet = gasOutlet as DryingGasStream;

         //gas stream goes through an adiabatic saturation process
         double tg1 = dgsInlet.Temperature.Value;
         double y1 = dgsInlet.Humidity.Value;
         double tw1 = dgsInlet.WetBulbTemperature.Value;
         double td1 = dgsInlet.DewPoint.Value;
         double fy1 = dgsInlet.RelativeHumidity.Value;

         double tg2 = dgsOutlet.Temperature.Value;
         double y2 = dgsOutlet.Humidity.Value;
         double tw2 = dgsOutlet.WetBulbTemperature.Value;
         double td2 = dgsOutlet.DewPoint.Value;
         double fy2 = dgsOutlet.RelativeHumidity.Value;

         double ih = 0;
         double p1 = dgsInlet.Pressure.Value;
         double p2 = dgsOutlet.Pressure.Value;

         if (p1 == Constants.NO_VALUE || p2 == Constants.NO_VALUE) {
            return;
         }
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         if (tg1 != Constants.NO_VALUE && y1 != Constants.NO_VALUE) {
            ih = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tg1, y1, p1);
            if (tg2 != Constants.NO_VALUE) {
               y2 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg2, p2);
               if (y2 <= 0.0) {
                  y2 = 1.0e-6;
               }
               Calculate(dgsOutlet.MoistureContentDryBase, y2);
               solveState = SolveState.Solved;
            }
            else if (y2 != Constants.NO_VALUE) {
               tg2 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y2, p2);
               Calculate(dgsOutlet.Temperature, tg2);
               solveState = SolveState.Solved;
            }
            else if (td2 != Constants.NO_VALUE) {
               y2 = humidGasCalculator.GetHumidityFromDewPointAndPressure(td2, p2);
               tg2 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y2, p2);
               Calculate(dgsOutlet.Temperature, tg2);
               solveState = SolveState.Solved;
            }
            else if (fy2 != Constants.NO_VALUE) {
               double fy_temp = 0;
               double delta = 10.0;
               double totalDelta = delta;
               tg2 = tg1 - delta;
               bool negativeLastTime = false;

               int counter = 0;
               do {
                  counter++;
                  y2 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg2, p2);
                  fy_temp = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg2, y2, p2);
                  if (fy2 > fy_temp) {
                     if (negativeLastTime) {
                        delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     }
                     totalDelta += delta;
                     negativeLastTime = false;
                  }
                  else if (fy2 < fy_temp) {
                     delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     totalDelta -= delta;
                     negativeLastTime = true;
                  }
                  tg2 = tg1 - totalDelta;
               } while (Math.Abs(fy2 - fy_temp) > 1.0e-6 && counter <= 200);

               if (counter < 200) {
                  Calculate(dgsOutlet.Temperature, tg2);
                  solveState = SolveState.Solved;
               }
            }

            if (solveState == SolveState.Solved) {
               double fy = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg2, y2, p2);
               if (fy > 1.0) {
                  solveState = SolveState.NotSolved;
                  string msg = "Specified gas inlet state makes the relative humidity of the outlet greater than 1.0.";
                  throw new InappropriateSpecifiedValueException(msg);
               }
            }
         }
         else if (tg2 != Constants.NO_VALUE && y2 != Constants.NO_VALUE) {
            ih = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tg2, y2, p2);
            if (tg1 != Constants.NO_VALUE) {
               y1 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg1, p1);
               Calculate(dgsInlet.MoistureContentDryBase, y1);
               solveState = SolveState.Solved;
            }
            else if (y1 != Constants.NO_VALUE) {
               tg1 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y1, p1);
               Calculate(dgsInlet.Temperature, tg1);
               solveState = SolveState.Solved;
            }
            else if (td1 != Constants.NO_VALUE) {
               y1 = humidGasCalculator.GetHumidityFromDewPointAndPressure(td1, p1);
               tg1 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y1, p1);
               Calculate(dgsInlet.Temperature, tg1);
               solveState = SolveState.Solved;
            }
            else if (fy1 != Constants.NO_VALUE) {
               double fy_temp = 0;
               double delta = 10.0;
               double totalDelta = delta;
               tg1 = tg2 + delta;
               bool negativeLastTime = false;

               int counter = 0;
               do {
                  counter++;
                  y1 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg1, p1);
                  fy_temp = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg1, y1, p1);
                  if (fy1 < fy_temp) {
                     if (negativeLastTime) {
                        delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     }
                     totalDelta += delta;
                     negativeLastTime = false;
                  }
                  else if (fy1 > fy_temp) {
                     delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     totalDelta -= delta;
                     negativeLastTime = true;
                  }
                  tg1 = tg2 + totalDelta;
               } while (Math.Abs(fy1 - fy_temp) > 1.0e-6 && counter <= 200);

               if (counter < 200) {
                  Calculate(dgsInlet.Temperature, tg1);
                  solveState = SolveState.Solved;
               }
            }
         }
         //end of adiabatic saturation process calculatioin


         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         //PostSolve(false);
         UpdateStreamsIfNecessary();

         balanceModel.DoBalanceCalculation();

         double inletDustMassFlowRate = Constants.NO_VALUE;
         double outletDustMassFlowRate = Constants.NO_VALUE;
         double inletDustMoistureFraction = 0.0;
         double outletDustMoistureFraction = 0.0;

         DryingGasComponents dgc;
         if (InletParticleLoading.HasValue && gasInlet.VolumeFlowRate.HasValue) {
            inletDustMassFlowRate = InletParticleLoading.Value * gasInlet.VolumeFlowRate.Value;
            dgc = dgsInlet.GasComponents;
            if (dgc.SolidPhase != null) {
               SolidPhase sp = dgc.SolidPhase;
               MaterialComponent mc = sp[1];
               inletDustMoistureFraction = mc.GetMassFractionValue();
            }
         }

         if (OutletParticleLoading.HasValue && gasOutlet.VolumeFlowRate.HasValue) {
            outletDustMassFlowRate = OutletParticleLoading.Value * gasOutlet.VolumeFlowRate.Value;
            dgc = dgsOutlet.GasComponents;
            if (dgc.SolidPhase != null) {
               SolidPhase sp = dgc.SolidPhase;
               MaterialComponent mc = sp[1];
               inletDustMoistureFraction = mc.GetMassFractionValue();
            }
         }

         double inletMoistureFlowRate = Constants.NO_VALUE;
         double outletMoistureFlowRate = Constants.NO_VALUE;
         if (dgsInlet.MassFlowRateDryBase.HasValue && dgsInlet.MoistureContentDryBase.HasValue) {
            inletMoistureFlowRate = dgsInlet.MassFlowRateDryBase.Value * dgsInlet.MoistureContentDryBase.Value;
         }

         if (dgsOutlet.MassFlowRateDryBase.HasValue && dgsOutlet.MoistureContentDryBase.HasValue) {
            outletMoistureFlowRate = dgsOutlet.MassFlowRateDryBase.Value * dgsOutlet.MoistureContentDryBase.Value;
         }

         double materialFromGas = 0.0;
         if (inletDustMassFlowRate != Constants.NO_VALUE && outletDustMassFlowRate != Constants.NO_VALUE &&
            inletMoistureFlowRate != Constants.NO_VALUE && outletMoistureFlowRate != Constants.NO_VALUE) {

            double moistureToGas = outletMoistureFlowRate - inletMoistureFlowRate;
            materialFromGas = inletDustMassFlowRate - outletDustMassFlowRate;
            double moistureOfMaterialFromGas = inletDustMassFlowRate * inletDustMoistureFraction - outletDustMassFlowRate * outletDustMoistureFraction;

            if (dmsInlet.MassFlowRate.HasValue) {
               double outletMassFlowRate = dmsInlet.MassFlowRate.Value + materialFromGas - moistureToGas;
               Calculate(dmsOutlet.MassFlowRate, outletMassFlowRate);

               if (dmsInlet.MoistureContentWetBase.HasValue) {
                  double inletMaterialMoistureFlowRate = dmsInlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
                  double outletMaterialMoistureFlowRate = inletMaterialMoistureFlowRate - moistureToGas + moistureOfMaterialFromGas;
                  double outletMoistureContentWetBase = outletMaterialMoistureFlowRate / outletMassFlowRate;
                  Calculate(dmsOutlet.MoistureContentWetBase, outletMoistureContentWetBase);
                  solveState = SolveState.Solved;
               }
               else if (dmsOutlet.MoistureContentWetBase.HasValue) {
                  double outletMaterialMoistureFlowRate = dmsOutlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
                  double inletMaterialMoistureFlowRate = outletMaterialMoistureFlowRate + moistureToGas - moistureOfMaterialFromGas;
                  double inletMoistureContentWetBase = inletMaterialMoistureFlowRate / dmsInlet.MassFlowRate.Value;
                  Calculate(dmsInlet.MoistureContentWetBase, inletMoistureContentWetBase);
                  solveState = SolveState.Solved;
               }
            }
            else if (dmsOutlet.MassFlowRate.HasValue) {
               double inletMassFlowRate = dmsOutlet.MassFlowRate.Value - materialFromGas + moistureToGas;
               Calculate(dmsInlet.MassFlowRate, inletMassFlowRate);

               if (dmsInlet.MoistureContentWetBase.HasValue) {
                  double inletMaterialMoistureFlowRate = dmsInlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
                  double outletMaterialMoistureFlowRate = inletMaterialMoistureFlowRate - moistureToGas + moistureOfMaterialFromGas;
                  double outletMoistureContentWetBase = outletMaterialMoistureFlowRate / dmsOutlet.MassFlowRate.Value;
                  Calculate(dmsOutlet.MoistureContentWetBase, outletMoistureContentWetBase);
                  solveState = SolveState.Solved;
               }
               else if (dmsOutlet.MoistureContentWetBase.HasValue) {
                  double outletMaterialMoistureFlowRate = dmsOutlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
                  double inletMaterialMoistureFlowRate = outletMaterialMoistureFlowRate + moistureToGas - moistureOfMaterialFromGas;
                  double inletMoistureContentWetBase = inletMaterialMoistureFlowRate / inletMassFlowRate;
                  Calculate(dmsInlet.MoistureContentWetBase, inletMoistureContentWetBase);
                  solveState = SolveState.Solved;
               }
            }
            else if (dmsOutlet.MassConcentration.HasValue) {
               double cValue = dmsOutlet.MassConcentration.Value;
               double inletMassFlowRate = (materialFromGas * (1 - cValue) + moistureToGas * cValue) / cValue;
               Calculate(dmsInlet.MassFlowRate, inletMassFlowRate);
               double outletMassFlowRate = inletMassFlowRate + materialFromGas - moistureToGas;
               Calculate(dmsOutlet.MassFlowRate, outletMassFlowRate);
               solveState = SolveState.Solved;
            }
         }

         MoistureProperties moistureProperties = (this.unitOpSystem as EvaporationAndDryingSystem).MoistureProperties;
         double enthalpyOfMaterialFromGas = 0.0;
         if (dmsOutlet.GetCpOfAbsoluteDryMaterial() != Constants.NO_VALUE && inletDustMoistureFraction != Constants.NO_VALUE && gasInlet.Temperature.HasValue) {
            double tempValue = gasInlet.Temperature.Value;
            double liquidCp = moistureProperties.GetSpecificHeatOfLiquid(tempValue);
            double specificHeatOfSolidPhase = (1.0 - inletDustMoistureFraction) * dmsOutlet.GetCpOfAbsoluteDryMaterial() + inletDustMoistureFraction * liquidCp;
            enthalpyOfMaterialFromGas = materialFromGas * specificHeatOfSolidPhase * (tempValue - 273.15);
         }

         if (gasInlet.SpecificEnthalpy.HasValue && gasInlet.MassFlowRate.HasValue &&
            gasOutlet.SpecificEnthalpy.HasValue && gasOutlet.MassFlowRate.HasValue) {
            double gasEnthalpyLoss = gasInlet.SpecificEnthalpy.Value * gasInlet.MassFlowRate.Value -
               gasOutlet.SpecificEnthalpy.Value * gasOutlet.MassFlowRate.Value;

            if (liquidInlet.SpecificEnthalpy.HasValue && liquidInlet.MassFlowRate.HasValue &&
               liquidOutlet.MassFlowRate.HasValue) {
               double totalLiquidOutletEnthalpy = gasEnthalpyLoss + enthalpyOfMaterialFromGas + liquidInlet.SpecificEnthalpy.Value * liquidInlet.MassFlowRate.Value;
               double specificLiquidOutletEnthalpy = totalLiquidOutletEnthalpy / liquidOutlet.MassFlowRate.Value;
               Calculate(liquidOutlet.SpecificEnthalpy, specificLiquidOutletEnthalpy);
            }
            //else if (gasInlet.SpecificEnthalpy.HasValue && gasInlet.MassFlowRate.HasValue &&
            //               gasOutlet.SpecificEnthalpy.HasValue && gasOutlet.MassFlowRate.HasValue &&
            //               liquidOutlet.SpecificEnthalpy.HasValue && liquidOutlet.MassFlowRate.HasValue &&
            //               liquidInlet.MassFlowRate.HasValue) {
            //   double totalLiquidInletEnthalpy = liquidOutlet.SpecificEnthalpy.Value * liquidOutlet.MassFlowRate.Value - gasEnthalpyLoss - enthalpyOfMaterialFromGas;
            //   double specificLiquidInletEnthalpy = totalLiquidInletEnthalpy / liquidInlet.MassFlowRate.Value;
            //   Calculate(liquidInlet.SpecificEnthalpy, specificLiquidInletEnthalpy);
            //}
         }
         else if (liquidInlet.SpecificEnthalpy.HasValue && liquidInlet.MassFlowRate.HasValue &&
            liquidOutlet.SpecificEnthalpy.HasValue && liquidOutlet.MassFlowRate.HasValue) {
            double liquidEnthalpyLoss = liquidInlet.SpecificEnthalpy.Value * liquidInlet.MassFlowRate.Value -
               liquidOutlet.SpecificEnthalpy.Value * liquidOutlet.MassFlowRate.Value;

            if (gasInlet.SpecificEnthalpy.HasValue && gasInlet.MassFlowRate.HasValue &&
               gasOutlet.MassFlowRate.HasValue) {
               double totalGasOutletEnthalpy = liquidEnthalpyLoss + gasInlet.SpecificEnthalpy.Value * gasInlet.MassFlowRate.Value + enthalpyOfMaterialFromGas;
               double specificGasOutletEnthalpy = totalGasOutletEnthalpy / gasOutlet.MassFlowRate.Value;
               Calculate(gasOutlet.SpecificEnthalpy, specificGasOutletEnthalpy);
            }
            //else if (gasOutlet.SpecificEnthalpy.HasValue && gasOutlet.MassFlowRate.HasValue &&
            //               gasInlet.MassFlowRate.HasValue) {
            //   double totalGasInletEnthalpy = gasOutlet.SpecificEnthalpy.Value * gasOutlet.MassFlowRate.Value - liquidEnthalpyLoss;
            //   double specificGasInletEnthalpy = totalGasInletEnthalpy / gasInlet.MassFlowRate.Value;
            //   Calculate(gasInlet.SpecificEnthalpy, specificGasInletEnthalpy);
            //}
         }

         if (liquidToGasVolumeRatio.HasValue && gasInlet.VolumeFlowRate.HasValue) {
            //double recirculationVolumeFlow = liquidToGasVolumeRatio.Value * gasInlet.VolumeFlowRate.Value;
            //Calculate(liquidRecirculationVolumeFlowRate, recirculationVolumeFlow);
            //if (liquidOutlet.Density.HasValue) {
            //   double recirculationMassFlow = recirculationVolumeFlow / liquidOutlet.Density.Value;
            //   Calculate(liquidRecirculationMassFlowRate, recirculationMassFlow);
            //}
         }
      }

      protected WetScrubber(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionWetScrubber", typeof(int));
         if (persistedClassVersion == 1) 
         {
            this.gasInlet = (DryingGasStream)info.GetValue("GasInlet", typeof(DryingGasStream));
            this.gasOutlet = (DryingGasStream)info.GetValue("GasOutlet", typeof(DryingGasStream));
            this.liquidInlet = (ProcessStreamBase)info.GetValue("LiquidInlet", typeof(ProcessStreamBase));
            this.liquidOutlet = (ProcessStreamBase)info.GetValue("LiquidOutlet", typeof(ProcessStreamBase));

            //this.scrubberType = (ScrubberType) info.GetValue("ScrubberType", typeof(ScrubberType)) ;
            this.balanceModel = RecallStorableObject("BalanceModel", typeof(GasSolidSeparatorBalanceModel)) as GasSolidSeparatorBalanceModel;
            
            this.liquidToGasVolumeRatio = (ProcessVarDouble)RecallStorableObject("LiquidToGasVolumeRatio", typeof(ProcessVarDouble));
            //this.liquidRecirculationVolumeFlowRate = (ProcessVarDouble)RecallStorableObject("LiquidRecirculationVolumeFlowRate", typeof(ProcessVarDouble));
            //this.liquidRecirculationMassFlowRate = (ProcessVarDouble)RecallStorableObject("LiquidRecirculationMassFlowRate", typeof(ProcessVarDouble));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionWetScrubber", WetScrubber.CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("GasInlet", this.gasInlet, typeof(ProcessStreamBase));
         info.AddValue("GasOutlet", this.gasOutlet, typeof(ProcessStreamBase));
         info.AddValue("LiquidInlet", this.liquidInlet, typeof(ProcessStreamBase));
         info.AddValue("LiquidOutlet", this.liquidOutlet, typeof(ProcessStreamBase));

         //info.AddValue("ScrubberType", this.scrubberType, typeof(ScrubberType));
         info.AddValue("BalanceModel", this.balanceModel, typeof(GasSolidSeparatorBalanceModel));
         
         info.AddValue("LiquidToGasVolumeRatio", this.liquidToGasVolumeRatio, typeof(ProcessVarDouble));
         //info.AddValue("LiquidRecirculationVolumeFlowRate", this.liquidRecirculationVolumeFlowRate, typeof(ProcessVarDouble));
         //info.AddValue("LiquidRecirculationMassFlowRate", this.liquidRecirculationMassFlowRate, typeof(ProcessVarDouble));
      }
   }
}

