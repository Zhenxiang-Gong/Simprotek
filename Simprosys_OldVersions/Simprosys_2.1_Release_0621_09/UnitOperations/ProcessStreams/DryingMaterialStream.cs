using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.ProcessStreams {

   //public delegate void MaterialStateTypeChangedEventHandler(DryingMaterialStream materialStream, MaterialStateType materialStateType);


   /// <summary>
   /// Summary description for DryingMaterialStream.
   /// </summary>
   [Serializable]
   public class DryingMaterialStream : DryingStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private const double TOLERANCE = 1.0e-6;

      private ProcessVarDouble massConcentration;
      private MaterialStateType materialStateType;

      //public event MaterialStateTypeChangedEventHandler MaterialStateTypeChanged;

      #region public properties
      public ProcessVarDouble MassConcentration {
         get { return massConcentration; }
      }

      public MaterialStateType MaterialStateType {
         get { return materialStateType; }
      }

      public DryingMaterialComponents MaterialComponents {
         get { return (materialComponents as DryingMaterialComponents); }
      }

      #endregion

      public DryingMaterialStream(string name, MaterialComponents mComponents, MaterialStateType materialStateType, UnitOperationSystem uoSys)
         : base(name, mComponents, uoSys) {
         this.materialStateType = materialStateType;
         massConcentration = new ProcessVarDouble(StringConstants.MASS_CONCENTRATION, PhysicalQuantity.MoistureContent, VarState.Specified, this);
         if (materialStateType == MaterialStateType.Liquid) {
            volumeFlowRate.Type = PhysicalQuantity.VolumeRateFlowLiquids;
         }
         else if (materialStateType == MaterialStateType.Solid) {
            pressure.Enabled = false;
            vaporFraction.Enabled = false;
            massConcentration.Enabled = false;
         }

         InitializeVarListAndRegisterVars();
      }

      private MoistureProperties MoistureProperties {
         get { return (this.unitOpSystem as EvaporationAndDryingSystem).GetMoistureProperties(MaterialComponents.Moisture.Substance); }
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         AddVarOnListAndRegisterInSystem(massFlowRateDryBase);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         //if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(pressure);
         //}
         AddVarOnListAndRegisterInSystem(temperature);
         //if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(vaporFraction);
         //}

         AddVarOnListAndRegisterInSystem(moistureContentWetBase);
         AddVarOnListAndRegisterInSystem(moistureContentDryBase);
         //if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(massConcentration);
         //}

         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificEnthalpyDryBase);
         AddVarOnListAndRegisterInSystem(specificHeat);
         AddVarOnListAndRegisterInSystem(specificHeatDryBase);
         AddVarOnListAndRegisterInSystem(density);
      }

      private DryingMaterial DryingMaterial {
         get { return (this.unitOpSystem as EvaporationAndDryingSystem).DryingMaterial; }
      }

      //private void OnMaterialStateTypeChanged(DryingMaterialStream materialStream, MaterialStateType materialStateType) {
      //   if (MaterialStateTypeChanged != null) {
      //      MaterialStateTypeChanged(materialStream, materialStateType);
      //   }
      //}

      internal override double GetBoilingPoint(double pressureValue) {
         if (!massConcentration.HasValue && materialStateType == MaterialStateType.Liquid) {
            throw new IllegalFunctionCallException(this.name + ": Mass concentration has no value. Therefore, there is no way to calculate a boiling point.");
         }

         double massConcentrationValue = 0.0;
         if (materialStateType == MaterialStateType.Liquid) {
            massConcentrationValue = massConcentration.Value;
            double vaporFractionValue = vaporFraction.Value;
            if (vaporFractionValue != Constants.NO_VALUE && vaporFractionValue > 1.0e-4 && vaporFractionValue < 0.9999) {
               massConcentrationValue = massConcentrationValue / (1.0 - vaporFractionValue);
            }
         }

         return GetBoilingPoint(pressureValue, massConcentrationValue);
      }
      
      internal double GetBoilingPoint(double pressureValue, double massConcentrationValue) {
         //if (materialStateType != MaterialStateType.Liquid) {
         //   throw new IllegalFunctionCallException("Only liquid material type can have a boiling point.");
         //}

         double tEvap = MoistureProperties.GetSaturationTemperature(pressureValue);

         //modify boiling point for concentration effect
         double bpe = 0;
         if (massConcentrationValue > 1.0e-4) {
            DryingMaterial dm = (unitOpSystem as EvaporationAndDryingSystem).DryingMaterial;

            bpe = BoilingPointRiseCalculator.CalculateBoilingPointElevation(dm, massConcentrationValue, pressureValue);
         }

         return (tEvap + bpe);
      }

      internal override double GetEvaporationHeat(double temperature) {
         //if (materialStateType != MaterialStateType.Liquid) {
         //   throw new IllegalFunctionCallException("Only liquid material type can have a boiling point.");
         //}

         double evapHeat = MoistureProperties.GetEvaporationHeat(temperature);
         return evapHeat;
      }

      internal double GetCpOfAbsoluteDryMaterial() {
         //return GetCpOfAbsoluteDryMaterial(Constants.NO_VALUE);
         return GetCpOfAbsoluteDryMaterial(temperature.Value);
      }

      internal double GetCpOfAbsoluteDryMaterial(double tempValue) {
         EvaporationAndDryingSystem mySystem = this.unitOpSystem as EvaporationAndDryingSystem;
         DryingMaterial dryingMat = mySystem.DryingMaterial;
         double cs = Constants.NO_VALUE;

         if (dryingMat.MaterialType == MaterialType.GenericMaterial) {
            cs = dryingMat.SpecificHeatOfAbsoluteDryMaterial;
         }
         else if (dryingMat.MaterialType == MaterialType.GenericFood) {
            cs = GenericFoodPropCalculator.GetAbsoluteDryCp((CompositeSubstance)dryingMat.AbsoluteDryMaterial, tempValue);
         }
         else {
            throw new ApplicationException("Material Type has not been supported yet!"); 
         }
         
         if (cs == Constants.NO_VALUE && specificHeat.HasValue && moistureContentDryBase.HasValue) {
            double x = moistureContentDryBase.Value;
            double cw = MoistureProperties.GetSpecificHeatOfLiquid();
            if (tempValue != Constants.NO_VALUE) {
               cw = MoistureProperties.GetSpecificHeatOfLiquid(tempValue);
            }
            cs = (specificHeat.Value * (1.0 + x) - x * cw);
         }

         return cs;
      }

      internal override double GetSolidCp(double tempValue) {
         return CalculateCp(tempValue);
      }

      internal override double GetLiquidCp(double tempValue) {
         return CalculateCp(tempValue);
      }

      internal override double GetGasCp(double tempValue) {
         double cp = MoistureProperties.GetSpecificHeatOfVapor(tempValue);
         return cp;
      }

      private double CalculateCp(double temperature) {
         double cp = Constants.NO_VALUE;
         if (massConcentration.HasValue && massConcentration.Value <= TOLERANCE) {
            if (vaporFraction.HasValue && vaporFraction.Value > 0.9999) {
               cp = GetGasCp(temperature);
            }
            else if (temperature != Constants.NO_VALUE && pressure.HasValue) {
               if (temperature < GetBoilingPoint(pressure.Value)) {
                  cp = MoistureProperties.GetSpecificHeatOfLiquid(temperature);
               }
               else if (temperature > GetBoilingPoint(pressure.Value)) {
                  cp = MoistureProperties.GetSpecificHeatOfVapor(temperature);
               }
            }
         }
         else if (DryingMaterial.MaterialType == MaterialType.GenericFood) {
         //if (DryingMaterial.MaterialType == MaterialType.GenericFood) {
            cp = GenericFoodPropCalculator.GetCp(materialComponents.Components, temperature);
         }
         else if (DryingMaterial.MaterialType == MaterialType.SpecialFood && massConcentration.HasValue) {
            cp = SpecialFoodPropCalculator.GetCp(DryingMaterial.Name, massConcentration.Value, temperature);
         }

         if (cp == Constants.NO_VALUE && moistureContentDryBase.HasValue) {
            EvaporationAndDryingSystem mySystem = this.unitOpSystem as EvaporationAndDryingSystem;
            DryingMaterial dryingMat = mySystem.DryingMaterial;
            if (dryingMat.SpecificHeatOfAbsoluteDryMaterial != Constants.NO_VALUE) {
               double moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(temperature);
               cp = dryingMat.SpecificHeatOfAbsoluteDryMaterial + moistureContentDryBase.Value * moistureLiquidCp;
               cp = cp / (1.0 + moistureContentDryBase.Value);
            }
         }

         return cp;
      }

      internal override double GetLiquidViscosity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return materialPropCalculator.GetLiquidViscosity(compList, temperature);
      }

      internal override double GetGasViscosity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return materialPropCalculator.GetGasViscosity(compList, temperature);
      }

      internal override double GetLiquidThermalConductivity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return materialPropCalculator.GetLiquidThermalConductivity(compList, temperature);
      }

      internal override double GetGasThermalConductivity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return materialPropCalculator.GetGasThermalConductivity(compList, temperature);
      }

      internal override double GetLiquidDensity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         Substance s = dmc.Moisture.Substance;
         double molarWt = s.MolarWeight;
         double densityValue = Constants.NO_VALUE;
         if (density.IsSpecifiedAndHasValue) {
            densityValue = density.Value;
         }
         else if (massConcentration.Value <= TOLERANCE && pressure.HasValue) {
            densityValue = MoistureProperties.GetDensity(pressure.Value, temperature);
         }
         return densityValue;
      }

      private double CalculateDensity(double tempValue) {
         double densityValue = Constants.NO_VALUE;
         if (DryingMaterial.MaterialType == MaterialType.GenericFood) {
            densityValue = GenericFoodPropCalculator.GetDensity(materialComponents.Components, tempValue);
         }
         else if (DryingMaterial.MaterialType == MaterialType.SpecialFood) {
            densityValue = SpecialFoodPropCalculator.GetDensity(DryingMaterial.Name, massConcentration.Value, tempValue);
         }
         else {
            if (density.IsSpecifiedAndHasValue) {
               densityValue = density.Value;
            }
            else if (massConcentration.Value <= TOLERANCE && pressure.HasValue) {
               densityValue = MoistureProperties.GetDensity(pressure.Value, tempValue);
            }
         }
         return densityValue;
      }

      internal override double GetGasDensity(double temperature, double pressure) {
         //DryingMaterialStreamComponents dmsc = streamComponents as DryingMaterialStreamComponents;
         //Substance s = dmsc.Moisture.Substance;
         //double molarWt = s.MolarWeight;
         //double density = (molarWt * pressure/1000)/(8.314*tempValue);
         double density = MoistureProperties.GetDensity(pressure, temperature + 0.001);
         return density;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == massConcentration) {
            if (materialStateType != MaterialStateType.Liquid) {
               throw new IllegalFunctionCallException("Solid material stream should not have its mass concentration specified.");
            }
            if (!vaporFraction.HasValue && (aValue < 0 || aValue > 1.0)) {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
            else if (vaporFraction.HasValue && (aValue < 0 || aValue > (1.0 - vaporFraction.Value))) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " cannot be out of the range of 0 to " + (1.0 - vaporFraction.Value) + " under current conditions");
            }
         }
         else if (pv == vaporFraction) {
            if (materialStateType != MaterialStateType.Liquid) {
               throw new IllegalFunctionCallException("Solid material stream should not have its vapor fraction specified.");
            }

            if (massConcentration.HasValue && aValue > (1.0 - massConcentration.Value)) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " must be less than " + moistureContentWetBase.VarTypeName + " under current conditions");
            }
         }

         if (retValue == null) {
            retValue = CheckSpecifiedValueInContextOfOwner(pv, aValue);
         }

         return retValue;
      }

      //protected override bool IsSolveReady() {
      //   if (HasSolvedAlready) {
      //      return false;
      //   }

      //   bool retValue = false;

      //   if ((moistureContentDryBase.HasValue && !moistureContentWetBase.IsSpecifiedAndHasValue)
      //      || (moistureContentWetBase.HasValue && !moistureContentDryBase.IsSpecifiedAndHasValue)
      //      || concentration.HasValue && (!moistureContentDryBase.IsSpecifiedAndHasValue || !moistureContentWetBase.IsSpecifiedAndHasValue)) {
      //      retValue = true;
      //   }

      //   if (moistureContentDryBase.HasValue || moistureContentWetBase.HasValue || concentration.HasValue) {
      //      if ((specificHeatDryBase.HasValue && !specificHeat.IsSpecifiedAndHasValue)
      //         || (specificHeat.HasValue && !specificHeatDryBase.IsSpecifiedAndHasValue)) {
      //         retValue = true;
      //      }

      //      if ((massFlowRateDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue)
      //         || (massFlowRate.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue)) {
      //         retValue = true;
      //      }

      //      if ((massFlowRateDryBase.HasValue || massFlowRate.HasValue)
      //         && (density.HasValue && !volumeFlowRate.IsSpecifiedAndHasValue)) {
      //         retValue = true;
      //      }
      //   }

      //   return retValue;
      //}

      public override void Execute(bool propagate) {
         if (HasSolvedAlready) {
            return;
         }

         if (moistureContentDryBase.HasValue && !moistureContentWetBase.IsSpecifiedAndHasValue) {
            Calculate(moistureContentWetBase, moistureContentDryBase.Value / (1.0 + moistureContentDryBase.Value));
         }
         else if (moistureContentWetBase.HasValue && !moistureContentDryBase.IsSpecifiedAndHasValue) {
            if (moistureContentWetBase.Value != 1.0) {
               Calculate(moistureContentDryBase, moistureContentWetBase.Value / (1.0 - moistureContentWetBase.Value));
            }
            else {
               Calculate(moistureContentDryBase, Constants.NO_VALUE);
            }
         }

         if (materialStateType == MaterialStateType.Liquid) {
            if (massConcentration.HasValue && massConcentration.Value > TOLERANCE) {
               if (!moistureContentDryBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentDryBase, (1.0 - massConcentration.Value) / massConcentration.Value);
               }
               if (!moistureContentWetBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentWetBase, 1.0 - massConcentration.Value);
               }
            }
            else if (massConcentration.HasValue && massConcentration.Value <= TOLERANCE) {
               Calculate(moistureContentDryBase, Constants.NO_VALUE);
               Calculate(moistureContentWetBase, 1.0);
               Calculate(massFlowRateDryBase, Constants.NO_VALUE);
            }
            else if (moistureContentDryBase.HasValue && !massConcentration.IsSpecifiedAndHasValue) {
               Calculate(massConcentration, 1.0 / (1.0 + moistureContentDryBase.Value));
            }
            else if (moistureContentWetBase.HasValue && !massConcentration.IsSpecifiedAndHasValue) {
               Calculate(massConcentration, 1.0 - moistureContentWetBase.Value);
            }
         }

         if (moistureContentWetBase.HasValue) {
            DryingMaterialComponents dmc = MaterialComponents;
            dmc.Moisture.SetMassFractionValue(moistureContentWetBase.Value);
            dmc.AbsoluteDryMaterial.SetMassFractionValue(1.0 - moistureContentWetBase.Value);
            dmc.ComponentsFractionsChanged();
         }

         //if it is a known meterial in the material database, specific heat can be calculated
         double moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid();
         if (temperature.HasValue) {
            moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(temperature.Value);
         }

         double cs = GetCpOfAbsoluteDryMaterial();
         if (temperature.HasValue && moistureContentWetBase.HasValue) {
            double cp = CalculateCp(temperature.Value);
            if (cp != Constants.NO_VALUE) {
               Calculate(specificHeat, cp);

               if (moistureContentDryBase.HasValue) {
                  Calculate(specificHeatDryBase, cp * (1.0 + moistureContentDryBase.Value));
               }
            }
            else if (specificHeat.HasValue && moistureContentDryBase.HasValue) {
               Calculate(specificHeatDryBase, specificHeat.Value * (1.0 + moistureContentDryBase.Value));
            }
         }
         //need this when temperature is not not known
         //else if (cs != Constants.NO_VALUE && moistureContentDryBase.HasValue && !specificHeat.IsSpecifiedAndHasValue) {
         else if (specificEnthalpy.HasValue && cs != Constants.NO_VALUE && moistureContentDryBase.HasValue && !specificHeat.IsSpecifiedAndHasValue) {
            double cpMaterialDryBase = cs + moistureContentDryBase.Value * moistureLiquidCp;
            Calculate(specificHeatDryBase, cpMaterialDryBase);
            Calculate(specificHeat, cpMaterialDryBase / (1.0 + moistureContentDryBase.Value));
         }
         //else if (moistureContentWetBase.Value == 1.0 && !specificHeat.IsSpecifiedAndHasValue) {
         //   Calculate(specificHeatDryBase, Constants.NO_VALUE);
         //   if (temperature.HasValue && moistureContentWetBase.HasValue) {
         //      moistureLiquidCp = CalculateCp(temperature.Value);
         //      Calculate(specificHeat, moistureLiquidCp);
         //   }
         //}

         if (materialStateType == MaterialStateType.Solid && specificHeat.HasValue) {
            if (temperature.HasValue) {
               //Calculate(specificEnthalpy, specificHeat.Value * (temperature.Value - 273.15));
               CalculateEnthalpyFromTemperature(Constants.ONE_ATM, temperature.Value, moistureContentWetBase.Value);
            }
            else if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) {
               //Calculate(temperature, specificEnthalpy.Value / specificHeat.Value + 273.15);
               double tValue = CalculateTemperatureFromEnthalpyForSolid(specificEnthalpy.Value, moistureContentWetBase.Value);
               Calculate(temperature, tValue);
            }
         }
         else if (materialStateType == MaterialStateType.Liquid && pressure.HasValue && moistureContentWetBase.HasValue) {
            if (massConcentration.Value > TOLERANCE) {
               if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) {
                  CalculateTemperatureFromEnthalpy(pressure.Value, specificEnthalpy.Value, moistureContentWetBase.Value);
               }
               else if (temperature.HasValue) {
                  CalculateEnthalpyFromTemperature(pressure.Value, temperature.Value, moistureContentWetBase.Value);
               }
               else if (vaporFraction.HasValue) {
                  CalculateEnthalpyFromVaporFraction(pressure.Value, vaporFraction.Value, moistureContentWetBase.Value);
               }
            }
            else {
               double h = Constants.NO_VALUE;
               if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) {
                  double t = MoistureProperties.GetTemperatureFromPressureAndEnthalpy(pressure.Value, specificEnthalpy.Value);
                  Calculate(temperature, t);
                  CalculateVaporFractionIfNeeded();
               }
               else if (temperature.HasValue) {
                  h = MoistureProperties.GetEnthalpyFromPressureAndTemperature(pressure.Value, temperature.Value);
                  Calculate(specificEnthalpy, h);
                  CalculateVaporFractionIfNeeded();
               }
               else if (vaporFraction.HasValue) {
                  double boilingPoint = MoistureProperties.GetSaturationTemperature(pressure.Value);
                  h = MoistureProperties.GetEnthalpyFromPressureAndVaporFraction(pressure.Value, vaporFraction.Value);
                  Calculate(temperature, boilingPoint);
                  Calculate(specificEnthalpy, h);
               }

               //CalculateCpOfPureMoisture();
               if (specificEnthalpy.HasValue) {
                  CalculateCpOfPureMoisture();
               }
            }
         }

         if (massFlowRateDryBase.HasValue && moistureContentDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue) {
            Calculate(massFlowRate, massFlowRateDryBase.Value * (1.0 + moistureContentDryBase.Value));
         }
         else if (massFlowRate.HasValue && moistureContentDryBase.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue) {
            Calculate(massFlowRateDryBase, massFlowRate.Value / (1.0 + moistureContentDryBase.Value));
         }

         if (temperature.HasValue && massConcentration.HasValue) {
            CalculateDensity();
         }

         if (massFlowRate.HasValue && density.HasValue) {
            Calculate(volumeFlowRate, massFlowRate.Value / density.Value);
         }

         if (materialStateType == MaterialStateType.Solid && temperature.HasValue && massFlowRate.HasValue &&
            moistureContentWetBase.HasValue && specificHeat.HasValue && specificEnthalpy.HasValue) {
            solveState = SolveState.Solved;
         }
         else if (materialStateType == MaterialStateType.Liquid && pressure.HasValue && temperature.HasValue &&
            massFlowRate.HasValue && moistureContentWetBase.HasValue && specificEnthalpy.HasValue &&
            vaporFraction.HasValue) {
            solveState = SolveState.Solved;
         }

         AdjustVarsStates();
         //if (HasSolvedAlready) {
         //   DryingMaterialComponents dmc = MaterialComponents;
         //   dmc.Moisture.SetMassFractionValue(moistureContentWetBase.Value);
         //   dmc.AbsoluteDryMaterial.SetMassFractionValue(1.0 - moistureContentWetBase.Value);
         //   dmc.ComponentsFractionsChanged();
         //}
         OnSolveComplete();
      }

      private void CalculateVaporFractionIfNeeded() {
         if (!vaporFraction.IsSpecifiedAndHasValue) {
            double vf = 0;
            double bolingPoint = MoistureProperties.GetSaturationTemperature(pressure.Value);
            //if (specificEnthalpy.HasValue && specificEnthalpy.Value >= MoistureProperties.GetEnthalpyFromPressureAndTemperature(pressure.Value, bolingPoint)) {
            //   vf = 1.0;
            //}
            //else if (temperature.HasValue && temperature.Value > bolingPoint) {
            if (temperature.HasValue && temperature.Value > bolingPoint) {
               vf = 1.0;
            }
            Calculate(vaporFraction, vf);
         }
      }

      protected override void AdjustVarsStates() {
         base.AdjustVarsStates();

         if (materialStateType == MaterialStateType.Liquid) {
            if (pressure.IsSpecifiedAndHasValue && temperature.IsSpecifiedAndHasValue) {
               vaporFraction.State = VarState.Calculated;
            }
            else if (pressure.IsSpecifiedAndHasValue && vaporFraction.IsSpecifiedAndHasValue) {
               temperature.State = VarState.Calculated;
            }
         }
      }

      private void CalculateEnthalpyFromVaporFraction(double pValue, double vfValue, double moistureContentValue) {
         double massConcentrationValue = 1.0 - moistureContentValue;
         double tBoilingPointOfSolution = GetBoilingPoint(pValue, massConcentrationValue);
         massConcentrationValue = massConcentrationValue / (1.0 - vfValue);
         double tBoilingPointOfSolutionFinal = GetBoilingPoint(pValue, massConcentrationValue);
         double evapHeat = GetEvaporationHeat(MathUtility.Average(tBoilingPointOfSolution, tBoilingPointOfSolutionFinal));
         double moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(tBoilingPointOfSolution, tBoilingPointOfSolutionFinal));
         double cs = GetCpOfAbsoluteDryMaterial();
         double hBubble = GetBubblePointEnthalpy(pValue, moistureContentValue);
         double hBoilingPointRise = (moistureContentValue * moistureLiquidCp + (1.0 - moistureContentValue) * cs) * (tBoilingPointOfSolutionFinal - tBoilingPointOfSolution);
         double h = hBubble + hBoilingPointRise + vfValue * evapHeat;

         Calculate(specificEnthalpy, h);
         Calculate(temperature, tBoilingPointOfSolution);

         if (!specificHeat.HasValue) {
            double cp;
            if (Math.Abs(vfValue - 1.0) < 1.0e-5) {
               cp = GetGasCp(tBoilingPointOfSolution);
            }
            else {
               cp = CalculateCp(tBoilingPointOfSolution);
            }

            Calculate(specificHeat, cp);
         }
      }

      private void CalculateEnthalpyFromTemperature(double pValue, double tValue, double moistureContentValue) {
         //double tBoilingPointOfSolvent = MoistureProperties.GetSaturationTemperature(pValue);
         double tBoilingPointOfSolution = GetBoilingPoint(pValue);
         double hValue;
         if (tValue <= tBoilingPointOfSolution) {
            hValue = CalculateLiquidEnthalpy(pValue, tValue, moistureContentValue);
            Calculate(specificEnthalpy, hValue);
            Calculate(vaporFraction, 0.0);
         }
         else if (tValue > tBoilingPointOfSolution) {
            double hBubble = GetBubblePointEnthalpy(pValue, moistureContentValue);
            double evapHeat = GetEvaporationHeat(tBoilingPointOfSolution);
            double hVapor1 = MoistureProperties.GetEnthalpyFromPressureAndTemperature(pValue, tBoilingPointOfSolution + TOLERANCE);
            double hVapor2 = hVapor2 = MoistureProperties.GetEnthalpyFromPressureAndTemperature(pValue, tValue); 
            //if (DryingMaterial.Moisture.IsWater) {
            //   SteamTable steamTable = SteamTable.GetInstance();
            //   SubstanceStatesAndProperties props = steamTable.GetPropertiesFromPressureAndTemperature(pValue, tBoilingPointOfSolution + TOLERANCE);
            //   hVapor1 = props.enthalpy;
            //   props = steamTable.GetPropertiesFromPressureAndTemperature(pValue, tValue);
            //   hVapor2 = props.enthalpy;
            //}
            //else {
            //   hVapor1 = MoistureProperties.GetEnthalpy(pValue, tBoilingPointOfSolution + TOLERANCE);
            //   hVapor2 = MoistureProperties.GetEnthalpy(pValue, tValue);
            //}

            double cs = GetCpOfAbsoluteDryMaterial(tValue);
            hValue = hBubble + moistureContentValue * (evapHeat + (hVapor2 - hVapor1)) + (1.0 - moistureContentValue) * cs * (tValue - tBoilingPointOfSolution);

            Calculate(specificEnthalpy, hValue);
            if (materialStateType == MaterialStateType.Liquid) {
               Calculate(vaporFraction, moistureContentValue);
            }
         }
      }

      private void CalculateTemperatureFromEnthalpy(double pValue, double hValue, double moistureContentValue) {
         double tValue = Constants.NO_VALUE;
         double tValueOld = Constants.NO_VALUE;
         double vfValue = Constants.NO_VALUE;

         double tBoilingPointOfSolvent = MoistureProperties.GetSaturationTemperature(pValue);
         double tBoilingPointOfSolution = GetBoilingPoint(pValue);
         double cpLiquid = specificHeat.Value;
         if (cpLiquid == Constants.NO_VALUE) {
            cpLiquid = GetLiquidCp(MathUtility.Average(273.15, tBoilingPointOfSolution));
         }

         if (cpLiquid == Constants.NO_VALUE) {
            return;
         }

         //SteamTable steamTable = SteamTable.GetInstance();
         //SubstanceStatesAndProperties props;

         double hBoilingPointOfSolvent = CalculateLiquidEnthalpy(pValue, tBoilingPointOfSolvent, moistureContentValue);
         double hBubble = GetBubblePointEnthalpy(pValue, moistureContentValue);
         double hDew = GetDewPointEnthalpy(pValue, moistureContentValue);

         double cs = GetCpOfAbsoluteDryMaterial();
         int counter = 0;
         if (hValue <= hBoilingPointOfSolvent) {
            double tempH;
            //tValue = hValue / cpLiquid + 273.15;
            tValue = tBoilingPointOfSolvent - (hBoilingPointOfSolvent - hValue) / cpLiquid;
            do {
               counter++;
               tValueOld = tValue;
               tempH = (hValue - (1.0 - moistureContentValue) * cs * (tValue - 273.15)) / moistureContentValue;
               //props = steamTable.GetPropertiesFromPressureAndEnthalpy(pValue, tempH);
               //tValue = props.temperature;
               tValue = MoistureProperties.GetTemperatureFromPressureAndEnthalpy(pValue, tempH);
            } while (Math.Abs(tValue - tValueOld) > TOLERANCE && counter < 200);

            if (counter == 200) {
               string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream");
               throw new CalculationFailedException(msg);
            }
            vfValue = 0.0;
         }
         else if (hValue <= hBubble) {
            double moistureLiquidCp;
            //double hBoilingPointOfSolvent = CalculateLiquidEnthalpy(pValue, tBoilingPointOfSolvent - TOLERANCE);
            //tValue = hValue / cpLiquid + 273.15;
            tValue = (hValue - hBoilingPointOfSolvent)/cpLiquid + tBoilingPointOfSolvent;
            do {
               counter++;
               tValueOld = tValue;
               moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(tBoilingPointOfSolvent, tValue));
               tValue = (hValue - hBoilingPointOfSolvent) / (moistureContentValue * moistureLiquidCp + (1.0 - moistureContentValue) * cs) + tBoilingPointOfSolvent;
            } while (Math.Abs(tValue - tValueOld) > TOLERANCE && counter < 200);

            if (counter == 200) {
               string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream");
               throw new CalculationFailedException(msg);
            }
            vfValue = 0.0;
         }
         else if (hValue <= hDew) {
            double tBoilingPointOfSolutionFinal = tBoilingPointOfSolution;
            double evapHeat = GetEvaporationHeat(tBoilingPointOfSolution);
            double extraHeat = (hValue - hBubble);
            if (moistureContentValue > 0.9999) {
               vfValue = extraHeat / evapHeat;
            }
            else {
               vfValue = 0;
               double vfValueOld;
               double moistureLiquidCp;
               double massConcentrationValue = 1.0 - moistureContentValue;
               do {
                  counter++;
                  vfValueOld = vfValue;
                  vfValue = extraHeat / evapHeat;
                  massConcentrationValue = (1.0 - moistureContentValue) / (1.0 - vfValue);
                  tBoilingPointOfSolutionFinal = GetBoilingPoint(pValue, massConcentrationValue);
                  evapHeat = GetEvaporationHeat(MathUtility.Average(tBoilingPointOfSolution, tBoilingPointOfSolutionFinal));
                  moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(tBoilingPointOfSolution, tBoilingPointOfSolutionFinal));
                  extraHeat = hValue - hBubble - (moistureContentValue * moistureLiquidCp + (1.0 - moistureContentValue) * cs) * (tBoilingPointOfSolutionFinal - tBoilingPointOfSolution);
               } while (Math.Abs(vfValue - vfValueOld) > TOLERANCE && counter < 200);

               if (counter == 200) {
                  string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream");
                  throw new CalculationFailedException(msg);
               }
            }

            if (vfValue < 0.0) {
               vfValue = 0.0;
            }

            tValue = tBoilingPointOfSolutionFinal;
         }
         else if (hValue > hDew) {
            double apparentHeat;
            double hVapor;
            double cpGas = GetGasCp(tBoilingPointOfSolution);
            tValue = (hValue - hDew) / cpGas + tBoilingPointOfSolution;

            do {
               apparentHeat = (hValue - hDew - (1.0 - moistureContentValue) * cs * (tValue - tBoilingPointOfSolution)) / moistureContentValue;
               //props = steamTable.GetPropertiesFromPressureAndTemperature(pValue, tBoilingPointOfSolution + TOLERANCE);
               //hVapor = props.enthalpy + apparentHeat;
               hVapor = apparentHeat + MoistureProperties.GetEnthalpyFromPressureAndTemperature(pValue, tBoilingPointOfSolution + TOLERANCE);
               //props = steamTable.GetPropertiesFromPressureAndEnthalpy(pressure.Value, hVapor);
               tValueOld = tValue;
               //tValue = props.temperature;
               tValue = MoistureProperties.GetTemperatureFromPressureAndEnthalpy(pressure.Value, hVapor);
            } while (Math.Abs(tValue - tValueOld) > TOLERANCE && counter < 200);

            if (counter == 200) {
               string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream");
               throw new CalculationFailedException(msg);
            }

            vfValue = moistureContentValue;
         }

         Calculate(temperature, tValue);
         Calculate(vaporFraction, vfValue);

         if (!specificHeat.HasValue) {
            double cp;
            if (hValue <= hBubble) {
               cp = GetLiquidCp(tValue);
            }
            else {
               cp = GetGasCp(tValue);
            }
            Calculate(specificHeat, cp);
         }
      }

      private double CalculateTemperatureFromEnthalpyForSolid(double hValue, double moistureContentValue) {

         double cpLiquid = specificHeat.Value;
         double tValue = hValue / cpLiquid + 273.15;
         double tValueOld;
         double moistureDeltaH;
         double cs = GetCpOfAbsoluteDryMaterial();
         //SteamTable steamTable = SteamTable.GetInstance();
         //SubstanceStatesAndProperties props;
         //double moistureRefEnthalpy = steamTable.GetPropertiesFromPressureAndTemperature(Constants.ONE_ATM, 273.15).enthalpy;
         double moistureRefEnthalpy = MoistureProperties.GetEnthalpyFromPressureAndTemperature(Constants.ONE_ATM, 273.15);

         int counter = 0;
         do {
            counter++;
            tValueOld = tValue;
            //props = steamTable.GetPropertiesFromPressureAndTemperature(Constants.ONE_ATM, tValue);
            //moistureDeltaH = props.enthalpy - moistureRefEnthalpy;
            moistureDeltaH = MoistureProperties.GetEnthalpyFromPressureAndTemperature(Constants.ONE_ATM, tValue) - moistureRefEnthalpy; 
            tValue = (hValue - moistureContentValue * moistureDeltaH) / ((1.0 - moistureContentValue) * cs) + 273.15;
            tValue = tValueOld + 0.5 * (tValue - tValueOld);
         } while (Math.Abs(tValue - tValueOld) > TOLERANCE && counter < 200);

         if (counter == 200) {
            string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream");
            throw new CalculationFailedException(msg);
         }

         return tValue;
      }


      public double GetBubblePointEnthalpy(double pValue, double moistureContentValue) {
         //double pValue = pressure.Value;
         double tBoilingPointOfSolution = GetBoilingPoint(pValue, (1.0 - moistureContentValue));
         double hBubble = CalculateLiquidEnthalpy(pValue, tBoilingPointOfSolution, moistureContentValue);
         return hBubble;
      }

      private double GetDewPointEnthalpy(double pressureValue, double moistureContentValue) {
         double hBubble = GetBubblePointEnthalpy(pressureValue, moistureContentValue);
         double tBoilingPointOfSolution = GetBoilingPoint(pressureValue);
         double evapHeat = GetEvaporationHeat(tBoilingPointOfSolution);
         double hDew = hBubble + moistureContentWetBase.Value * evapHeat;

         return hDew;
      }

      private double CalculateLiquidEnthalpy(double pValue, double tValue, double moistureContentValue) {
         double tBoilingPointOfSolvent = MoistureProperties.GetSaturationTemperature(pValue);
         double tBoilingPointOfSolution = GetBoilingPoint(pValue, (1 - moistureContentValue));
         double cs = GetCpOfAbsoluteDryMaterial();
         double hValue = Constants.NO_VALUE;
         double hMoisture = Constants.NO_VALUE;

         //SteamTable steamTable = SteamTable.GetInstance();
         //SubstanceStatesAndProperties props;
         //double mc = moistureContentWetBase.Value;
         if (tValue < tBoilingPointOfSolvent) {
            //props = steamTable.GetPropertiesFromPressureAndTemperature(pValue, tValue);
            hMoisture = MoistureProperties.GetEnthalpyFromPressureAndTemperature(pValue, tValue);
            hValue = moistureContentValue * hMoisture + (1.0 - moistureContentValue) * cs * (tValue - 273.15);
         }
         else if (tValue <= tBoilingPointOfSolution) {
            //props = steamTable.GetPropertiesFromPressureAndVaporFraction(pValue, 0); //P-V flash is more appropriate for buble point enthalpy of pure moisture
            hMoisture = MoistureProperties.GetEnthalpyFromPressureAndTemperature(pValue, tBoilingPointOfSolvent-TOLERANCE);
            double hBoilingPointOfSolvent = moistureContentValue * hMoisture + (1.0 - moistureContentValue) * cs * (tValue - 273.15);
            double moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(MathUtility.Average(tBoilingPointOfSolvent, tBoilingPointOfSolution));
            hValue = hBoilingPointOfSolvent + (moistureContentValue * moistureLiquidCp + (1.0 - moistureContentValue) * cs) * (tValue - tBoilingPointOfSolvent);
         }
         
         return hValue;
      }

      private void CalculateCpOfPureMoisture() {
         double cp = Constants.NO_VALUE;
         double tempValue = temperature.Value;
         if (vaporFraction.HasValue && vaporFraction.Value >= 0.999999) {
            //tempValue += 0.001;
            cp = MoistureProperties.GetSpecificHeatOfVapor(tempValue);
         }
         else if (vaporFraction.HasValue && vaporFraction.Value <= 0.000001) {
            //tempValue -= 0.001;
            cp = MoistureProperties.GetSpecificHeatOfLiquid(tempValue);
         }

         if (cp != Constants.NO_VALUE) {
            Calculate(specificHeat, cp);
         }
      }
      
      private void CalculateDensity() {
         double densityValue = Constants.NO_VALUE;
         if (materialStateType == MaterialStateType.Liquid && pressure.HasValue && massConcentration.Value < 1.0e-3) {
            double tempValue = temperature.Value;
            if (vaporFraction.HasValue && vaporFraction.Value >= 0.999999) {
               tempValue += TOLERANCE;
            }
            else if (vaporFraction.HasValue && vaporFraction.Value <= TOLERANCE) {
               tempValue -= TOLERANCE;
            }
            densityValue = MoistureProperties.GetDensity(pressure.Value, tempValue);
         }
         else {
            densityValue = CalculateDensity(temperature.Value);
         }

         if (densityValue != Constants.NO_VALUE) {
            Calculate(density, densityValue);
         }
      }

      #region Persistence
      protected DryingMaterialStream(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryingMaterialStream", typeof(int));
         if (persistedClassVersion == 1) {
            this.massConcentration = RecallStorableObject("MassConcentration", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.materialStateType = (MaterialStateType)info.GetValue("MaterialStateType", typeof(MaterialStateType));
            //since moistureProperties is not stored we need to initialize it here so that 
            //it is as if it is stored and recalled back
            //InitializeMoistureProperties();
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingMaterialStream", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("MassConcentration", massConcentration, typeof(ProcessVarDouble));
         info.AddValue("MaterialStateType", materialStateType, typeof(MaterialStateType));
      }

      #endregion
   }
}

//if (DryingMaterial.Moisture.IsWater) {
//   SteamTable steamTable = SteamTable.GetInstance();
//   SubstanceStatesAndProperties props;
//   if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) {
//      try {
//         props = steamTable.GetPropertiesFromPressureAndEnthalpy(pressure.Value, specificEnthalpy.Value);
//      }
//      catch (CalculationFailedException) {
//         string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from pressure and enthalpy failed.\nPlease make sure each specified value in this stream");
//         throw new CalculationFailedException(msg);
//      }

//      Calculate(temperature, props.temperature);
//      Calculate(vaporFraction, props.vaporFraction);
//   }
//   else if (temperature.HasValue) {
//      try {
//         props = steamTable.GetPropertiesFromPressureAndTemperature(pressure.Value, temperature.Value);
//      }
//      catch (CalculationFailedException) {
//         string msg = BuildProperErrorMessage(this.name + ": Calculation of enthalpy from pressure and temperature failed.\nPlease make sure each specified value in this stream");
//         throw new CalculationFailedException(msg);
//      }

//      Calculate(specificEnthalpy, props.enthalpy);
//      Calculate(vaporFraction, props.vaporFraction);
//   }
//   else if (vaporFraction.HasValue) {
//      try {
//         props = steamTable.GetPropertiesFromPressureAndVaporFraction(pressure.Value, vaporFraction.Value);
//      }
//      catch (CalculationFailedException) {
//         string msg = BuildProperErrorMessage(this.name + "Calculation of enthalpy from pressure and vapor fraction failed.\nPlease make sure each specified value in this stream");
//         throw new CalculationFailedException(msg);
//      }
//      Calculate(temperature, props.temperature);
//      Calculate(specificEnthalpy, props.enthalpy);
//   }
//}


