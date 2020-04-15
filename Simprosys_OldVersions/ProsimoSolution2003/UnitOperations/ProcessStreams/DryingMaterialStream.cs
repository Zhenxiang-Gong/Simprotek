using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.ProcessStreams {
   
   public delegate void MaterialStateTypeChangedEventHandler(DryingMaterialStream materialStream, MaterialStateType materialStateType);
                   
   
   /// <summary>
   /// Summary description for DryingMaterialStream.
   /// </summary>
   [Serializable]
   public class DryingMaterialStream : DryingStream {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private ProcessVarDouble massConcentration;
      private MaterialStateType materialStateType;

      public event MaterialStateTypeChangedEventHandler MaterialStateTypeChanged;

      #region public properties
      public ProcessVarDouble MassConcentration {
         get { return massConcentration;}
      }
      
      public MaterialStateType MaterialStateType {
         get { return materialStateType; }
      }
      
      public DryingMaterialComponents MaterialComponents {
         get { return (materialComponents as DryingMaterialComponents); }
      }
      
      #endregion
      
      public DryingMaterialStream(string name, MaterialComponents mComponents, MaterialStateType materialStateType, UnitOpSystem uoSys) : base(name, mComponents, uoSys) { 
         this.materialStateType = materialStateType;
         massConcentration = new ProcessVarDouble(StringConstants.MASS_CONCENTRATION, PhysicalQuantity.MoistureContent, VarState.Specified, this);
         if (materialStateType == MaterialStateType.Liquid) {
            volumeFlowRate.Type = PhysicalQuantity.VolumeRateFlowLiquids;
            specificHeatAbsDry.Enabled = false;
         }
         else if (materialStateType == MaterialStateType.Solid) {
            pressure.Enabled = false;
            vaporFraction.Enabled = false;
            massConcentration.Enabled = false;
         }
         
         InitializeVarListAndRegisterVars();
      }
      
      private MoistureProperties MoistureProperties {
         get {return (this.unitOpSystem as EvaporationAndDryingSystem).MoistureProperties;}
      }
      
      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         AddVarOnListAndRegisterInSystem(massFlowRateDryBase);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(pressure);
         }
         AddVarOnListAndRegisterInSystem(temperature);
         if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(vaporFraction);
         }

         AddVarOnListAndRegisterInSystem(moistureContentWetBase);
         AddVarOnListAndRegisterInSystem(moistureContentDryBase);
         if (materialStateType == MaterialStateType.Liquid) {
            AddVarOnListAndRegisterInSystem(massConcentration);
         }

         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificEnthalpyDryBase);
         AddVarOnListAndRegisterInSystem(specificHeat);
         AddVarOnListAndRegisterInSystem(specificHeatDryBase);
         //if (materialStateType == MaterialStateType.Solid) {
            AddVarOnListAndRegisterInSystem(specificHeatAbsDry);
         //}
         AddVarOnListAndRegisterInSystem(density);
      }
   
      private DryingMaterial DryingMaterial 
      {
         get {return (this.unitOpSystem as EvaporationAndDryingSystem).DryingMaterial;}
      }

      private void OnMaterialStateTypeChanged(DryingMaterialStream materialStream, MaterialStateType materialStateType) {
         if (MaterialStateTypeChanged != null) {
            MaterialStateTypeChanged(materialStream, materialStateType);
         }
      }
      
      internal override double GetBoilingPoint(double pressureValue) {
         if (materialStateType != MaterialStateType.Liquid) {
            throw new IllegalFunctionCallException("Only liquid material type can have a boiling point.");
         }

         double tEvap = MoistureProperties.GetSaturationTemperature(pressureValue);

         //modify boiling point for concentration effect
         double bpe = 0;
         if (massConcentration.Value > 1.0e-6) {
            DryingMaterial dm = (unitOpSystem as EvaporationAndDryingSystem).DryingMaterial;

            bpe = BoilingPointRiseCalculator.CalculateBoilingPointElevation(dm, massConcentration.Value, pressureValue);
         }

         return (tEvap + bpe);
      }
      
      internal override double GetEvaporationHeat(double temperature) {
         if (materialStateType != MaterialStateType.Liquid) {
            throw new IllegalFunctionCallException("Only liquid material type can have a boiling point.");
         }

         double evapHeat = MoistureProperties.GetEvaporationHeat(temperature);
         return evapHeat;
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
      
      private double CalculateCp(double tempValue) {
         double cp = Constants.NO_VALUE;
         if (massConcentration.HasValue) 
         {
            if(massConcentration.Value < 1.0e-6) 
            {
               cp = MoistureProperties.GetSpecificHeatOfLiquid(tempValue);
            }
            else if (massConcentration.Value > 1.0e-6 && specificHeatDryBase.HasValue) 
            {
               //double cm = specificHeatAbsDry.Value + moistureContentDryBase.Value * liquidCp;
               cp = specificHeatDryBase.Value/(1.0+moistureContentDryBase.Value);
            }
         }
         else if (DryingMaterial.MaterialType == MaterialType.GenericFood) 
         {
            cp = GenericFoodPropCalculator.GetCp(materialComponents.Components, tempValue);
         }
         else if (DryingMaterial.MaterialType == MaterialType.SpecialFood && massConcentration.HasValue) 
         {
            cp = SpecialFoodPropCalculator.GetCp(DryingMaterial.Name, massConcentration.Value, tempValue);
         }

         return cp;
      }
      
      internal override double GetLiquidViscosity(double temperature) {
         //return 1.0e-5;
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return MaterialPropCalculator.GetLiquidViscosity(compList, temperature);
      }

      internal override double GetGasViscosity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return MaterialPropCalculator.GetGasViscosity(compList, temperature);
      }
      
      internal override double GetLiquidThermalConductivity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return MaterialPropCalculator.GetLiquidThermalConductivity(compList, temperature);
      }

      internal override double GetGasThermalConductivity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         ArrayList compList = new ArrayList();
         compList.Add(dmc.Moisture);
         return MaterialPropCalculator.GetGasThermalConductivity(compList, temperature);
      }
      
      internal override double GetLiquidDensity(double temperature) {
         DryingMaterialComponents dmc = MaterialComponents;
         Substance s = dmc.Moisture.Substance;
         double molarWt = s.MolarWeight;
         double densityValue = Constants.NO_VALUE;
         if (density.IsSpecifiedAndHasValue) {
            densityValue = density.Value;
         }
         else if (massConcentration.Value <= 1.0e-6 && pressure.HasValue) {
            densityValue = MoistureProperties.GetDensity(pressure.Value, temperature);
         }
         return densityValue;
      }

      private double CalculateDensity(double tempValue) 
      {
         double densityValue = Constants.NO_VALUE;
         if (DryingMaterial.MaterialType == MaterialType.GenericFood) 
         {
            densityValue = GenericFoodPropCalculator.GetDensity(materialComponents.Components, tempValue);
         }
         else if (DryingMaterial.MaterialType == MaterialType.SpecialFood) 
         {
            densityValue = SpecialFoodPropCalculator.GetDensity(DryingMaterial.Name, massConcentration.Value, tempValue);
         }
         else 
         {
            if (density.IsSpecifiedAndHasValue) 
            {
               densityValue = density.Value;
            }
            else if (massConcentration.Value <= 1.0e-6 && pressure.HasValue) 
            {
               densityValue = MoistureProperties.GetDensity(pressure.Value, tempValue);
            }
         }
         return densityValue;
      }
      
      internal override double GetGasDensity(double temperature, double pressure) 
      {
         //DryingMaterialStreamComponents dmsc = streamComponents as DryingMaterialStreamComponents;
         //Substance s = dmsc.Moisture.Substance;
         //double molarWt = s.MolarWeight;
         //double density = (molarWt * pressure/1000)/(8.314*tempValue);
         double density = MoistureProperties.GetDensity(pressure, temperature+0.001);
         return density;
      }
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == massConcentration) 
         {
            if (materialStateType != MaterialStateType.Liquid) 
            {
               throw new IllegalFunctionCallException("Solid material stream should not have its mass concentration variable specified");
            }
            if (aValue != Constants.NO_VALUE && !vaporFraction.HasValue && (aValue < 0 || aValue > 1.0))
            {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
            else if (aValue != Constants.NO_VALUE && vaporFraction.HasValue && (aValue < 0 || aValue > (1.0 - vaporFraction.Value))) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, StringConstants.GetTypeName(StringConstants.MASS_CONCENTRATION) + " can not be out of the range of 0 to " + (1.0 - vaporFraction.Value) + " under current conditions");
            }
         }
         else if (pv == vaporFraction) 
         {
            if (materialStateType != MaterialStateType.Liquid) 
            {
               throw new IllegalFunctionCallException("Solid material stream should not have its vapor fraction variable specified");
            }

            if (aValue != Constants.NO_VALUE && massConcentration.HasValue && aValue > (1.0 - massConcentration.Value)) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, StringConstants.GetTypeName(StringConstants.VAPOR_FRACTION) + " must be less than " + StringConstants.GetTypeName(StringConstants.MOISTURE_CONTENT_WET) + " under current conditions");
            }
         }

         return retValue;
      }

//      protected override bool IsSolveReady() {
//         if (HasSolvedAlready) {
//            return false;
//         }
//
//         bool retValue = false;
//
//         if ((moistureContentDryBase.HasValue && !moistureContentWetBase.IsSpecifiedAndHasValue)
//            || (moistureContentWetBase.HasValue && !moistureContentDryBase.IsSpecifiedAndHasValue)
//            || concentration.HasValue && (!moistureContentDryBase.IsSpecifiedAndHasValue || !moistureContentWetBase.IsSpecifiedAndHasValue)) {
//            retValue = true;
//         }
//
//         if (moistureContentDryBase.HasValue || moistureContentWetBase.HasValue || concentration.HasValue) {
//            if ((specificHeatDryBase.HasValue && !specificHeat.IsSpecifiedAndHasValue)
//               || (specificHeatAbsDry.HasValue && !specificHeat.IsSpecifiedAndHasValue)
//               || (specificHeat.HasValue && !specificHeatDryBase.IsSpecifiedAndHasValue)) {
//               retValue = true;
//            }
//            
//            if((massFlowRateDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue)
//               || (massFlowRate.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue)) {
//               retValue = true;
//            }
//
//            if((massFlowRateDryBase.HasValue || massFlowRate.HasValue)
//               && (density.HasValue && !volumeFlowRate.IsSpecifiedAndHasValue)) {
//               retValue = true;
//            }
//         }
//         
//         return retValue;
//      }

      public override void Execute(bool propagate) {
         if (HasSolvedAlready) {
            return;
         }

         if (moistureContentDryBase.HasValue && !moistureContentWetBase.IsSpecifiedAndHasValue) {
            Calculate(moistureContentWetBase, moistureContentDryBase.Value/(1.0 + moistureContentDryBase.Value));
         }
         else if (moistureContentWetBase.HasValue && !moistureContentDryBase.IsSpecifiedAndHasValue) {
            if (moistureContentWetBase.Value != 1.0) {
               Calculate(moistureContentDryBase, moistureContentWetBase.Value/(1.0 - moistureContentWetBase.Value));
            }
            else {              
               Calculate(moistureContentDryBase, Constants.NO_VALUE);
            }
         }

         if (materialStateType == MaterialStateType.Liquid) {
            if (massConcentration.HasValue && massConcentration.Value > 1.0e-6) {
               if (!moistureContentDryBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentDryBase, (1.0 - massConcentration.Value)/massConcentration.Value);
               }
               if (!moistureContentWetBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentWetBase, 1.0 - massConcentration.Value);
               }
            }
            else if (massConcentration.HasValue && massConcentration.Value <= 1.0e-6) {
               Calculate(moistureContentDryBase, Constants.NO_VALUE);
               Calculate(moistureContentWetBase, 1.0);
               Calculate(massFlowRateDryBase, Constants.NO_VALUE);
            }
            else if (moistureContentDryBase.HasValue && !massConcentration.IsSpecifiedAndHasValue) {
               Calculate(massConcentration, 1.0/(1.0 + moistureContentDryBase.Value));
            }
            else if (moistureContentWetBase.HasValue && !massConcentration.IsSpecifiedAndHasValue) {
               Calculate(massConcentration, 1.0 - moistureContentWetBase.Value);
            }
         }

         //if it is a known meterial in the material database, specific heat can be calculated
         double moistureLiquidCp = Constants.NO_VALUE;
         if (temperature.HasValue && moistureContentWetBase.HasValue) 
         {
            double cp = CalculateCp(temperature.Value);
            moistureLiquidCp = MoistureProperties.GetSpecificHeatOfLiquid(temperature.Value);
            if (cp != Constants.NO_VALUE) 
            {
               Calculate(specificHeat, cp);
            
               if (moistureContentDryBase.HasValue) 
               {
                  Calculate(specificHeatDryBase, cp * (1.0+moistureContentDryBase.Value));
                  double cpAbsDry = specificHeatDryBase.Value - moistureContentDryBase.Value * moistureLiquidCp;
                  Calculate(specificHeatAbsDry, cpAbsDry);
               }
            }
            else if (specificHeatAbsDry.HasValue && !specificHeat.IsSpecifiedAndHasValue) 
            {
               double cpMaterialDryBase = specificHeatAbsDry.Value + moistureContentDryBase.Value * moistureLiquidCp;
               Calculate(specificHeatDryBase, cpMaterialDryBase);
               Calculate(specificHeat, cpMaterialDryBase/(1.0+moistureContentDryBase.Value));
            }
            else if (specificHeat.HasValue && moistureContentDryBase.HasValue && temperature.HasValue &&
               !specificHeatAbsDry.IsSpecifiedAndHasValue) 
            {
               Calculate(specificHeatDryBase, specificHeat.Value/(1.0-moistureContentWetBase.Value));
               Calculate(specificHeatAbsDry, specificHeatDryBase.Value - moistureContentDryBase.Value * moistureLiquidCp);
            }
         }
         else if (moistureContentWetBase.Value == 1.0 && !specificHeat.IsSpecifiedAndHasValue) 
         {
            Calculate(specificHeatDryBase, Constants.NO_VALUE);
            Calculate(specificHeatAbsDry, Constants.NO_VALUE);
            if (temperature.HasValue && moistureContentWetBase.HasValue) 
            {
               moistureLiquidCp = CalculateCp(temperature.Value);
               Calculate(specificHeat, moistureLiquidCp);
            }
         }
      
         if (materialStateType == MaterialStateType.Solid && specificHeat.HasValue) 
         {
            if (temperature.HasValue) 
            {
               Calculate(specificEnthalpy, specificHeat.Value * (temperature.Value - 273.15));
            }
            else if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) 
            {
               Calculate(temperature, specificEnthalpy.Value/specificHeat.Value + 273.15);
            }
         }
         else if (materialStateType == MaterialStateType.Liquid && pressure.HasValue) 
         {
            if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) 
            {
               CalculateTemperatureFromEnthalpy();
            }
            else if (temperature.HasValue && massConcentration.HasValue) 
            {
               double tBoilingPoint = GetBoilingPoint(pressure.Value);
               if (temperature.Value < tBoilingPoint) 
               {
                  Calculate(vaporFraction, 0.0);
                  if (specificHeat.HasValue) 
                  {
                     Calculate(specificEnthalpy, specificHeat.Value * (temperature.Value - 273.15));
                  }
               }
               else if (temperature.Value > tBoilingPoint) 
               {
                  Calculate(vaporFraction, 1.0);
                  if (specificHeat.HasValue) 
                  {
                     double evapHeat = GetEvaporationHeat(tBoilingPoint);
                     double gasCp = GetLiquidCp(tBoilingPoint);
                     double h = moistureLiquidCp * (tBoilingPoint - 273.15) + evapHeat + gasCp * (temperature.Value - tBoilingPoint); 
                     Calculate(specificEnthalpy, h);
                  }
               }
               else if (vaporFraction.HasValue) 
               {
                  double liquidCp = specificHeat.Value;
                  if (!specificHeat.HasValue)
                  {
                     liquidCp = CalculateCp(tBoilingPoint);
                  }

                  double evapHeat = GetEvaporationHeat(tBoilingPoint);
                  double h = liquidCp * (tBoilingPoint - 273.15) + vaporFraction.Value * evapHeat;
                  Calculate(specificEnthalpy, h);
               }
            }
            else if (vaporFraction.HasValue && massConcentration.HasValue) 
            {
               double tBoilingPoint = GetBoilingPoint(pressure.Value);
               Calculate(temperature, tBoilingPoint);
               double liquidCp = specificHeat.Value;
               if (!specificHeat.HasValue)
               {
                  liquidCp = CalculateCp(tBoilingPoint);
               }

               double evapHeat = GetEvaporationHeat(tBoilingPoint);
               double h = liquidCp * (tBoilingPoint - 273.15) + vaporFraction.Value * evapHeat;
               Calculate(specificEnthalpy, h);
               if (Math.Abs(vaporFraction.Value - 1.0) < 1.0e-5) 
               {
                  Calculate(specificHeat, GetGasCp(tBoilingPoint));
               }
               else if (Math.Abs(vaporFraction.Value - 0.0) < 1.0e-5) 
               {
                  Calculate(specificHeat, liquidCp);
               }
            }
         }
         
         if (massFlowRateDryBase.HasValue && moistureContentDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue) 
         {
            Calculate(massFlowRate, massFlowRateDryBase.Value * (1.0 + moistureContentDryBase.Value));
         }
         else if (massFlowRate.HasValue && moistureContentDryBase.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue) 
         {
            Calculate(massFlowRateDryBase, massFlowRate.Value/(1.0 + moistureContentDryBase.Value));
         }

         if (temperature.HasValue && massConcentration.HasValue) 
         {
            CalculateDensity();
         }

         if (massFlowRate.HasValue && density.HasValue) 
         {
            Calculate(volumeFlowRate, massFlowRate.Value/density.Value);
         }

         if (materialStateType == MaterialStateType.Solid && temperature.HasValue && massFlowRate.HasValue && 
            moistureContentWetBase.HasValue && specificHeat.HasValue && specificEnthalpy.HasValue) 
         {
            solveState = SolveState.Solved;
         }
         else if (materialStateType == MaterialStateType.Liquid && pressure.HasValue && temperature.HasValue && 
            massFlowRate.HasValue && moistureContentWetBase.HasValue && specificEnthalpy.HasValue && 
            vaporFraction.HasValue) {
             solveState = SolveState.Solved;
         }
         else if ((!massFlowRate.IsSpecified && massFlowRate.HasValue) || (!massFlowRateDryBase.IsSpecified && massFlowRateDryBase.HasValue) ||
            (!moistureContentWetBase.IsSpecified && moistureContentWetBase.HasValue) || (!moistureContentDryBase.IsSpecified && moistureContentDryBase.HasValue) ||
            (!massConcentration.IsSpecified && massConcentration.HasValue) || (!specificHeat.IsSpecified && specificHeat.HasValue) ||
            (!specificHeatAbsDry.IsSpecified && specificHeatAbsDry.HasValue) || (!specificEnthalpy.IsSpecified && specificEnthalpy.HasValue))
         {
            solveState = SolveState.PartiallySolved;
         }

         
         AdjustVarsStates();
         if (HasSolvedAlready) {
            DryingMaterialComponents dmc = MaterialComponents;
            dmc.Moisture.SetMassFractionValue(moistureContentWetBase.Value);
            dmc.AbsoluteDryMaterial.SetMassFractionValue(1.0 - moistureContentWetBase.Value);
            dmc.ComponentsFractionsChanged();
         }
         OnSolveComplete(solveState);
      }

      protected override void AdjustVarsStates() 
      {
         base.AdjustVarsStates();

         if (materialStateType == MaterialStateType.Liquid) 
         {
            if (pressure.IsSpecifiedAndHasValue && temperature.IsSpecifiedAndHasValue) 
            {
               vaporFraction.State = VarState.Calculated;
            }
            else if (pressure.IsSpecifiedAndHasValue && vaporFraction.IsSpecifiedAndHasValue) 
            {
               temperature.State = VarState.Calculated;
            }
         }
      }

      private void CalculateTemperatureFromEnthalpy() 
      {
         double tempValue = Constants.NO_VALUE;
         double tempValueOld = Constants.NO_VALUE;
         double h = specificEnthalpy.Value;
         double tBoilingPoint = GetBoilingPoint(pressure.Value);
         //??????? need work
         double cpLiquid = specificHeat.Value;
         if (cpLiquid == Constants.NO_VALUE) 
         {
            cpLiquid = GetLiquidCp((273.15 + tBoilingPoint)/2.0);
         }

         double evapHeat = GetEvaporationHeat(tBoilingPoint);
         double hBoilingPoint = cpLiquid * (tBoilingPoint - 273.15);
         double hDewPoint = cpLiquid * (tBoilingPoint - 273.15) + evapHeat;
         if (h < hBoilingPoint) 
         {
            cpLiquid = GetLiquidCp(293.15);
            do 
            {
               tempValueOld = tempValue;
               tempValue = h/cpLiquid + 273.15;
               cpLiquid = GetLiquidCp((273.15 + tempValue)/2.0);
            } while (Math.Abs(tempValue - tempValueOld) < 1.0e-6);

            Calculate(temperature, tempValue);
            Calculate(vaporFraction, 0.0);
            Calculate(specificHeat, cpLiquid);
         }
         else if (h >= hBoilingPoint && h <= hDewPoint)
         {
            double vapFracValue = (h - hBoilingPoint)/evapHeat;
            Calculate(temperature, tBoilingPoint);
            Calculate(vaporFraction, vapFracValue);
         }
         else if (h > hDewPoint) 
         {
            double cpGas = GetGasCp(tBoilingPoint);
            do 
            {
               tempValueOld = tempValue;
               tempValue = (h - hDewPoint)/cpGas + tBoilingPoint;
               cpGas = GetGasCp((tBoilingPoint + tempValue)/2.0);
            } while (Math.Abs(tempValue - tempValueOld) < 1.0e-6);

            Calculate(temperature, tempValue);
            Calculate(vaporFraction, 1.0);
            Calculate(specificHeat, cpGas);
         }
      }

      private void CalculateDensity() 
      {
         double densityValue = Constants.NO_VALUE;
         if (materialStateType == MaterialStateType.Liquid && pressure.HasValue && massConcentration.Value < 1.0e-3) 
         {
            double tempValue = temperature.Value;
            if (vaporFraction.HasValue && vaporFraction.Value >= 0.999999) 
            {
               tempValue += 0.001;
            }
            else if (vaporFraction.HasValue && vaporFraction.Value <= 0.000001) 
            {
               tempValue -= 0.001;
            }
            densityValue = MoistureProperties.GetDensity(pressure.Value, tempValue);
         }
         else 
         {
            densityValue = CalculateDensity(temperature.Value);
         }
         
         if (densityValue != Constants.NO_VALUE) 
         {
            Calculate(density, densityValue);
         }
      }

      #region Persistence
      protected DryingMaterialStream(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingMaterialStream", typeof(int));
         if (persistedClassVersion == 1) {
            this.massConcentration = RecallStorableObject("MassConcentration", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.materialStateType = (MaterialStateType) info.GetValue("MaterialStateType", typeof(MaterialStateType));
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

