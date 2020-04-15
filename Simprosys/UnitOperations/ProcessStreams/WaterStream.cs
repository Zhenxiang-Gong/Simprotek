using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections;

using Prosimo.Materials;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public class WaterStream : ProcessStreamBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      
      public WaterStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, mComponents, uoSys) {
         InitializeVarListAndRegisterVars();
         density.State = VarState.AlwaysCalculated;
         specificHeat.State = VarState.AlwaysCalculated;
         specificEnthalpy.State = VarState.AlwaysCalculated;
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         //AddVarOnListAndRegisterInSystem(moleFlowRate);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         AddVarOnListAndRegisterInSystem(pressure);
         AddVarOnListAndRegisterInSystem(temperature);
         AddVarOnListAndRegisterInSystem(vaporFraction);
         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificHeat);
         AddVarOnListAndRegisterInSystem(density);
         //AddVarOnListAndRegisterInSystem(specificVolume);
         //AddVarOnListAndRegisterInSystem(dynamicViscosity);
         //AddVarOnListAndRegisterInSystem(thermalConductivity);
      }

      internal override double GetBoilingPoint(double pressureValue) {
         return MoistureProperties.GetSaturationTemperature(pressureValue);
      }

      internal override double GetEvaporationHeat(double temperature) {
         return MoistureProperties.GetEvaporationHeat(temperature);
      }

      internal override double GetLiquidCp(double tempValue) {
         return MoistureProperties.GetSpecificHeatOfLiquid(tempValue);
      }

      internal override double GetGasCp(double tempValue) {
         return MoistureProperties.GetSpecificHeatOfVapor(tempValue);
      }

      internal override double GetLiquidViscosity(double temperature) {
         return materialPropCalculator.GetLiquidViscosity(materialComponents.Components, temperature);
      }

      internal override double GetGasViscosity(double temperature) {
         return materialPropCalculator.GetGasViscosity(materialComponents.Components, temperature);
      }

      internal override double GetLiquidThermalConductivity(double temperature) {
         return materialPropCalculator.GetLiquidThermalConductivity(materialComponents.Components, temperature);
      }

      internal override double GetGasThermalConductivity(double temperature) {
         return materialPropCalculator.GetGasThermalConductivity(materialComponents.Components, temperature);
      }

      internal override double GetLiquidDensity(double temperature) {
         return MoistureProperties.GetDensity(pressure.Value, temperature);
      }

      internal override double GetGasDensity(double temperature, double pressure) {
         return MoistureProperties.GetDensity(pressure, temperature + 0.001);
      }

      private MoistureProperties MoistureProperties {
         get { return (this.unitOpSystem as EvaporationAndDryingSystem).GetMoistureProperties(materialComponents[0].Substance); }
      }

      public override void Execute(bool propagate) {
         if (HasSolvedAlready) {
            return;
         }

         SteamTable steamTable = SteamTable.GetInstance();
         SubstanceStatesAndProperties props;
         if (pressure.HasValue) {
            if (specificEnthalpy.HasValue && !temperature.IsSpecifiedAndHasValue) {
               try {
                  props = steamTable.GetPropertiesFromPressureAndEnthalpy(pressure.Value, specificEnthalpy.Value);
               }
               catch (CalculationFailedException) {
                  string msg = BuildProperErrorMessage(this.name + ": Calculation of temperature from pressure and enthalpy failed.\nPlease make sure each specified value in this stream");
                  throw new CalculationFailedException(msg);
               }

               Calculate(temperature, props.temperature);
               Calculate(vaporFraction, props.vaporFraction);
            }
            else if (temperature.HasValue) {
               try {
                  props = steamTable.GetPropertiesFromPressureAndTemperature(pressure.Value, temperature.Value);
               }
               catch (CalculationFailedException) {
                  string msg = BuildProperErrorMessage(this.name + ": Calculation of enthalpy from pressure and temperature failed.\nPlease make sure each specified value in this stream");
                  throw new CalculationFailedException(msg);
               }

               Calculate(specificEnthalpy, props.enthalpy);
               Calculate(vaporFraction, props.vaporFraction);
            }
            else if (vaporFraction.HasValue) {
               try {
                  props = steamTable.GetPropertiesFromPressureAndVaporFraction(pressure.Value, vaporFraction.Value);
               }
               catch (CalculationFailedException) {
                  string msg = BuildProperErrorMessage(this.name + "Calculation of enthalpy from pressure and vapor fraction failed.\nPlease make sure each specified value in this stream");
                  throw new CalculationFailedException(msg);
               }
               Calculate(temperature, props.temperature);
               Calculate(specificEnthalpy, props.enthalpy);
            }
         }
         CalculateCpOfPureMoisture();

         if (temperature.HasValue) {
            CalculateDensity();
         }

         if (massFlowRate.HasValue && density.HasValue) {
            Calculate(volumeFlowRate, massFlowRate.Value / density.Value);
         }

         if (pressure.HasValue && temperature.HasValue && specificEnthalpy.HasValue && vaporFraction.HasValue
            && massFlowRate.HasValue) {
            solveState = SolveState.Solved;
         }

         AdjustVarsStates();
         OnSolveComplete();
      }

      protected override void AdjustVarsStates() {
         base.AdjustVarsStates();

         if (massFlowRate.HasValue) {
            if (!volumeFlowRate.IsSpecifiedAndHasValue) {
               volumeFlowRate.State = VarState.Calculated;
            }
         }
         if (pressure.IsSpecifiedAndHasValue && temperature.IsSpecifiedAndHasValue) {
            vaporFraction.State = VarState.Calculated;
         }
         else if (pressure.IsSpecifiedAndHasValue && vaporFraction.IsSpecifiedAndHasValue) {
            temperature.State = VarState.Calculated;
         }
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
         Calculate(specificHeat, cp);
      }

      private void CalculateDensity() {
         double densityValue = Constants.NO_VALUE;
         if (pressure.HasValue) {
            double tempValue = temperature.Value;
            if (vaporFraction.HasValue && vaporFraction.Value >= 0.999999) {
               tempValue += 0.001;
            }
            else if (vaporFraction.HasValue && vaporFraction.Value <= 0.000001) {
               tempValue -= 0.001;
            }
            densityValue = MoistureProperties.GetDensity(pressure.Value, tempValue);
         }

         if (densityValue != Constants.NO_VALUE) {
            Calculate(density, densityValue);
         }
      }
      
      protected WaterStream(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionProcessStream", WaterStream.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
