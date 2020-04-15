using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.ThermalProperties;
using Prosimo.Materials;
using Prosimo.SubstanceLibrary;

namespace Prosimo.UnitOperations.ProcessStreams {

   /// <summary>
   /// Summary description for DryingGasStream.
   /// </summary>
   [Serializable]
   public class DryingGasStream : DryingStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble wetBulbTemperature;
      private ProcessVarDouble dewPoint;
      private ProcessVarDouble relativeHumidity;
      protected ProcessVarDouble humidVolume;

      private HumidGasCalculator humidGasCalculator;

      #region public properties
      public ProcessVarDouble WetBulbTemperature {
         get { return wetBulbTemperature; }
      }

      public ProcessVarDouble DewPoint {
         get { return dewPoint; }
      }

      public ProcessVarDouble RelativeHumidity {
         get { return relativeHumidity; }
      }

      //humidity for drying gas is actually dry basis moisture content
      //Note: moistureContentDryBase is declared in DryingStream which is the base class of 
      //this class. Therefore, this data member does not need persistence in this class
      //It is persisted in its base class
      public ProcessVarDouble Humidity {
         get { return moistureContentDryBase; }
      }

      public ProcessVarDouble HumidVolume {
         get { return humidVolume; }
      }

      private HumidGasCalculator HumidGasCalculator
      {
         get
         {
            if(humidGasCalculator == null)
            {
               humidGasCalculator = GetHumidGasCalculator(this);
            }
            return humidGasCalculator;
         }
      }
      
      public DryingGasComponents GasComponents
      {
         get { return (materialComponents as DryingGasComponents); }
         set {
            materialComponents = value;
            humidGasCalculator = GetHumidGasCalculator(this);
         }
      }

      #endregion

      public DryingGasStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys)
         : base(name, mComponents, uoSys) {
         temperature.Name = StringConstants.DRY_BULB_TEMPERATURE;
         wetBulbTemperature = new ProcessVarDouble(StringConstants.WET_BULB_TEMPERATURE, PhysicalQuantity.Temperature, VarState.Specified, this);
         dewPoint = new ProcessVarDouble(StringConstants.DEW_POINT_TEMPERATURE, PhysicalQuantity.Temperature, VarState.Specified, this);
         moistureContentDryBase.Name = StringConstants.ABSOLUTE_HUMIDITY;
         relativeHumidity = new ProcessVarDouble(StringConstants.RELATIVE_HUMIDITY, PhysicalQuantity.Fraction, VarState.Specified, this);
         humidVolume = new ProcessVarDouble(StringConstants.HUMID_VOLUME, PhysicalQuantity.SpecificVolume, VarState.AlwaysCalculated, this);
         specificHeatDryBase.Name = StringConstants.HUMID_HEAT;
         specificEnthalpyDryBase.Name = StringConstants.HUMID_ENTHALPY;
         volumeFlowRate.Type = PhysicalQuantity.VolumeRateFlowGases;
         density.State = VarState.AlwaysCalculated;
         specificHeatDryBase.State = VarState.AlwaysCalculated;
         vaporFraction.Value = 1.0;
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         AddVarOnListAndRegisterInSystem(massFlowRateDryBase);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         AddVarOnListAndRegisterInSystem(pressure);
         AddVarOnListAndRegisterInSystem(temperature);
         AddVarOnListAndRegisterInSystem(wetBulbTemperature);
         AddVarOnListAndRegisterInSystem(dewPoint);
         AddVarOnListAndRegisterInSystem(moistureContentDryBase);
         AddVarOnListAndRegisterInSystem(relativeHumidity);
         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificHeatDryBase);
         AddVarOnListAndRegisterInSystem(density);
         AddVarOnListAndRegisterInSystem(humidVolume);
         AddVarOnListAndRegisterInSystem(specificEnthalpyDryBase);
         AddVarOnListAndRegisterInSystem(specificHeat);

         //The following vars should not be included in drying gas stream and should not appear on the 
         //var list: vaporFraction, specificHeatAbsDry, moistureContentWetBase.
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         string valueString = aValue.ToString();
         if (pv == temperature) {
            if (wetBulbTemperature.IsSpecifiedAndHasValue && aValue < wetBulbTemperature.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is less than " + wetBulbTemperature.VarTypeName + " and therefore cannot be committed.");
            }
            else if (dewPoint.IsSpecifiedAndHasValue && aValue < dewPoint.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is less than " + dewPoint.VarTypeName + " and therefore cannot be committed.");
            }
         }
         else if (pv == wetBulbTemperature) {
            if (temperature.IsSpecifiedAndHasValue && aValue > temperature.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is greater than " + temperature.VarTypeName + " and therefore cannot be committed.");
            }
            else if (dewPoint.IsSpecifiedAndHasValue && aValue < dewPoint.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is less than than " + dewPoint.VarTypeName + " and therefore cannot be committed.");
            }
         }
         else if (pv == dewPoint) {
            if (temperature.IsSpecifiedAndHasValue && aValue > temperature.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is greater than " + temperature.VarTypeName + " and therefore cannot be committed.");
            }
            else if (wetBulbTemperature.IsSpecifiedAndHasValue && aValue > wetBulbTemperature.Value) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Specified " + pv.VarTypeName + " value is greater than " + wetBulbTemperature.VarTypeName + " and therefore cannot be committed.");
            }
         }
         else if (pv == Humidity) {
            if (aValue < 0) {
               retValue = CreateLessThanZeroErrorMessage(pv);
            }
            else if (pressure.HasValue) {
               double maxHumidity = 1000.0;

               //HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
               if (temperature.HasValue) {
                  maxHumidity = HumidGasCalculator.GetHumidityFromDewPointAndPressure(temperature.Value, pressure.Value);
               }
               else if (wetBulbTemperature.HasValue) {
                  maxHumidity = HumidGasCalculator.GetHumidityFromDewPointAndPressure(wetBulbTemperature.Value, pressure.Value);
               }
               else if (dewPoint.HasValue) {
                  maxHumidity = HumidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint.Value, pressure.Value);
               }

               if (aValue > maxHumidity) {
                  aValue = maxHumidity;
                  retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("The maximum " + pv.VarTypeName + " value under current conditions is " + aValue.ToString() + ".\n Specified value " + valueString + " is greater than the maximum and therefore cannot be committed.");
               }
            }
         }
         else if (pv == relativeHumidity) {
            if (aValue < 0 || aValue > 1.0) {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
         }

         if (retValue == null) {
            retValue = CheckSpecifiedValueInContextOfOwner(pv, aValue);
         }

         return retValue;
      }

      private void CalculateFlow() {
         if (massFlowRateDryBase.HasValue) {
            //if (humidity.HasValue && !(massFlowRate.IsSpecified && massFlowRate.HasValue)) {
            if (moistureContentDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue) {
               Calculate(massFlowRate, massFlowRateDryBase.Value * (1.0 + moistureContentDryBase.Value));
            }
            if (humidVolume.HasValue && !volumeFlowRate.IsSpecifiedAndHasValue) {
               Calculate(volumeFlowRate, massFlowRateDryBase.Value * humidVolume.Value);
            }
         }
         else if (massFlowRate.HasValue) {
            if (moistureContentDryBase.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue) {
               Calculate(massFlowRateDryBase, massFlowRate.Value / (1.0 + moistureContentDryBase.Value));
            }
            if (humidVolume.HasValue && !volumeFlowRate.IsSpecifiedAndHasValue) {
               Calculate(volumeFlowRate, massFlowRateDryBase.Value * humidVolume.Value);
            }
         }
         else if (volumeFlowRate.HasValue) {
            if (humidVolume.HasValue && !massFlowRateDryBase.IsSpecifiedAndHasValue) {
               Calculate(massFlowRateDryBase, volumeFlowRate.Value / humidVolume.Value);
            }
            if (moistureContentDryBase.HasValue && !massFlowRate.IsSpecifiedAndHasValue) {
               Calculate(massFlowRate, massFlowRateDryBase.Value * (1.0 + moistureContentDryBase.Value));
            }
         }
      }

      public override void Execute(bool propagate) {
         //if dew point is known
         //HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         if (dewPoint.HasValue) {
            if (relativeHumidity.HasValue && !temperature.IsSpecifiedAndHasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromDewPointAndRelativeHumidity(dewPoint.Value, relativeHumidity.Value));
            }
            else if (pressure.HasValue) {
               if (dewPoint.Value <= 1.0e-10 && !moistureContentDryBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentDryBase, 0);
               }
               else if (!moistureContentDryBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentDryBase, HumidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint.Value, pressure.Value));
               }
            }
            //Pressure should always be known. So it should not be calculated 
            else if (moistureContentDryBase.HasValue) {
               //Calculate(pressure, humidGasCalculator.GetPressureFromDewPointAndHumidity(dewPoint.Value, humidity.Value));
            }
         }

         //if humidity is known
         if (moistureContentDryBase.HasValue) {
            if (pressure.HasValue && !dewPoint.IsSpecifiedAndHasValue) {
               Calculate(dewPoint, HumidGasCalculator.GetDewPointFromHumidityAndPressure(moistureContentDryBase.Value, pressure.Value));
            }

            //to prevent repeated calculation of the same variable
            //if (relativeHumidity.HasValue && dewPoint.HasValue && !temperature.IsSpecifiedAndHasValue) {
            if (relativeHumidity.HasValue && dewPoint.HasValue && !temperature.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromDewPointAndRelativeHumidity(dewPoint.Value, relativeHumidity.Value));
            }

            //to prevent repeated calculation of the same variable
            //if (wetBulbTemperature.HasValue && pressure.HasValue && !temperature.IsSpecifiedAndHasValue) {
            if (wetBulbTemperature.HasValue && pressure.HasValue && !temperature.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromWetBulbHumidityAndPressure(wetBulbTemperature.Value, moistureContentDryBase.Value, pressure.Value));
            }
         }

         //to prevent repeated calculation of the same variable
         //if (wetBulbTemperature.HasValue && relativeHumidity.HasValue && pressure.HasValue 
         //   && !temperature.IsSpecifiedAndHasValue) {
         if (wetBulbTemperature.HasValue && relativeHumidity.HasValue && pressure.HasValue
            && !temperature.HasValue) {
            Calculate(temperature, HumidGasCalculator.GetDryBulbFromWetBulbRelativeHumidityAndPressure(wetBulbTemperature.Value, relativeHumidity.Value, pressure.Value));
         }

         double humidEnthalpyValue;
         double mcDryBase = moistureContentDryBase.Value;
         if (specificEnthalpy.HasValue && moistureContentDryBase.HasValue && !specificEnthalpyDryBase.HasValue) {
            Calculate(specificEnthalpyDryBase, specificEnthalpy.Value * (1.0 + mcDryBase));
         }

         //if (specificEnthalpyDryBase.HasValue && pressure.HasValue && !temperature.IsSpecifiedAndHasValue) {
         if (specificEnthalpyDryBase.HasValue && pressure.HasValue && !temperature.HasValue) {
            if (moistureContentDryBase.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(specificEnthalpyDryBase.Value, moistureContentDryBase.Value, pressure.Value));
            }
            else if (relativeHumidity.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromHumidEnthalpyRelativeHumidityAndPressure(specificEnthalpyDryBase.Value, relativeHumidity.Value, pressure.Value));
            }
            else if (dewPoint.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromHumidEnthalpyDewPointAndPressure(specificEnthalpyDryBase.Value, relativeHumidity.Value, pressure.Value));
            }
            else if (wetBulbTemperature.HasValue) {
               Calculate(temperature, HumidGasCalculator.GetDryBulbFromHumidEnthalpyWetBulbAndPressure(specificEnthalpyDryBase.Value, wetBulbTemperature.Value, pressure.Value));
            }
         }

         //if temperature is first known
         if (temperature.HasValue) {
            if (dewPoint.HasValue || relativeHumidity.HasValue) {
               if (dewPoint.HasValue && !relativeHumidity.IsSpecifiedAndHasValue) {
                  Calculate(relativeHumidity, HumidGasCalculator.GetRelativeHumidityFromDryBulbAndDewPoint(temperature.Value, dewPoint.Value));
               }
               else if (relativeHumidity.HasValue && !dewPoint.IsSpecifiedAndHasValue) {
                  Calculate(dewPoint, HumidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature.Value, relativeHumidity.Value));
               }

               if (pressure.HasValue) {
                  if (!moistureContentDryBase.IsSpecifiedAndHasValue) {
                     Calculate(moistureContentDryBase, HumidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint.Value, pressure.Value));
                  }
                  if (!wetBulbTemperature.IsSpecifiedAndHasValue) {
                     Calculate(wetBulbTemperature, HumidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value));
                     //double satTemp = humidGasCalculator.GetSatTempFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value);
                  }
               }
               else if (moistureContentDryBase.HasValue) {
                  if (!pressure.IsSpecifiedAndHasValue) {
                     Calculate(pressure, HumidGasCalculator.GetPressureFromDewPointAndHumidity(dewPoint.Value, moistureContentDryBase.Value));
                  }
                  if (!wetBulbTemperature.IsSpecifiedAndHasValue) {
                     Calculate(wetBulbTemperature, HumidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value));
                     //double satTemp = humidGasCalculator.GetSatTempFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value);
                  }
               }
            }
            else if (wetBulbTemperature.HasValue && pressure.HasValue) {
               if (!moistureContentDryBase.IsSpecifiedAndHasValue) {
                  Calculate(moistureContentDryBase, HumidGasCalculator.GetHumidityFromDryBulbWetBulbAndPressure(temperature.Value, wetBulbTemperature.Value, pressure.Value));
               }
               if (!relativeHumidity.IsSpecifiedAndHasValue) {
                  Calculate(relativeHumidity, HumidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value));
               }
               if (!dewPoint.IsSpecifiedAndHasValue) {
                  Calculate(dewPoint, HumidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature.Value, relativeHumidity.Value));
               }
            }

            else if (moistureContentDryBase.HasValue && pressure.HasValue) {
               if (!wetBulbTemperature.IsSpecifiedAndHasValue) {
                  Calculate(wetBulbTemperature, HumidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value));
                  //double satTemp = humidGasCalculator.GetSatTempFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value);
               }

               if (!relativeHumidity.IsSpecifiedAndHasValue) {
                  Calculate(relativeHumidity, HumidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value));
               }
               if (!dewPoint.IsSpecifiedAndHasValue) {
                  Calculate(dewPoint, HumidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature.Value, relativeHumidity.Value));
               }
            }

            else if (wetBulbTemperature.HasValue && moistureContentDryBase.HasValue) {
               //if (!pressure.IsSpecifiedAndHasValue) {
               //   Calculate(pressure, humidGasCalculator.GetPressureFromDryBulbWetBulbAndHumidity(temperature.Value, wetBulbTemperature.Value, moistureContentDryBase.Value));
               //}
               if (!dewPoint.IsSpecifiedAndHasValue) {
                  Calculate(dewPoint, HumidGasCalculator.GetDewPointFromHumidityAndPressure(temperature.Value, pressure.Value));
               }
               if (!relativeHumidity.IsSpecifiedAndHasValue) {
                  Calculate(relativeHumidity, HumidGasCalculator.GetRelativeHumidityFromDryBulbAndDewPoint(temperature.Value, dewPoint.Value));
               }
            }
         }

         mcDryBase = moistureContentDryBase.Value;
         if (moistureContentDryBase.HasValue && temperature.HasValue) {
            double cpDryBase = HumidGasCalculator.GetHumidHeat(moistureContentDryBase.Value, temperature.Value);
            Calculate(specificHeatDryBase, cpDryBase);
            Calculate(specificHeat, cpDryBase / (1.0 + mcDryBase));
            if (pressure.HasValue) {
               double humidVolumeValue = HumidGasCalculator.GetHumidVolume(temperature.Value, moistureContentDryBase.Value, pressure.Value);
               Calculate(humidVolume, humidVolumeValue);
               humidEnthalpyValue = HumidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(temperature.Value, moistureContentDryBase.Value, pressure.Value);
               Calculate(specificEnthalpyDryBase, humidEnthalpyValue);
               Calculate(specificEnthalpy, humidEnthalpyValue / (1.0 + mcDryBase));
               Calculate(density, (1.0 + mcDryBase) / humidVolumeValue);
            }
         }

         CalculateFlow();

         //if (temperature.HasValue && pressure.HasValue && massFlowRate.HasValue &&
         //   volumeFlowRate.HasValue && massFlowRateDryBase.HasValue && wetBulbTemperature.HasValue &&
         //   dewPoint.HasValue && moistureContentDryBase.HasValue && relativeHumidity.HasValue &&
         //   density.HasValue && specificEnthalpy.HasValue && specificHeatDryBase.HasValue) {
         //   solveState = SolveState.Solved;
         //}

         bool hasUnsolvedVar = false;
         foreach (ProcessVarDouble pv in varList) {
            if (!pv.HasValue) {
               hasUnsolvedVar = true;
               break;
            }
         }
         if (!hasUnsolvedVar) {
            solveState = SolveState.Solved;
         }
         //else  
         //{
         //   foreach (ProcessVarDouble pv in varList) 
         //   {
         //      if (!pv.IsSpecified && pv.HasValue) 
         //      {
         //         solveState = SolveState.PartiallySolved;
         //         break;
         //      }
         //   }
         //}

         if (HasSolvedAlready) {
            DryingGasComponents dgc = (DryingGasComponents)materialComponents;
            //we cannot do a calculate operation since these variables are not in the
            //varList and they cannot be erased after they are initially calculated
            dgc.DryMedium.SetMassFractionValue(1.0 / (1.0 + Humidity.Value));
            dgc.Moisture.SetMassFractionValue(Humidity.Value / (1.0 + Humidity.Value));
            dgc.ComponentsFractionsChanged();
         }
         AdjustVarsStates();
         OnSolveComplete();
      }

      protected override void AdjustVarsStates() {
         base.AdjustVarsStates();

         if (temperature.IsSpecifiedAndHasValue && wetBulbTemperature.IsSpecifiedAndHasValue) {
            dewPoint.State = VarState.Calculated;
         }
         else if (temperature.IsSpecifiedAndHasValue && dewPoint.IsSpecifiedAndHasValue) {
            wetBulbTemperature.State = VarState.Calculated;
         }
         else if (wetBulbTemperature.IsSpecifiedAndHasValue && dewPoint.IsSpecifiedAndHasValue) {
            temperature.State = VarState.Calculated;
         }

         if (dewPoint.IsSpecifiedAndHasValue) {
            moistureContentDryBase.State = VarState.Calculated;
         }
         else if (moistureContentDryBase.IsSpecifiedAndHasValue) {
            dewPoint.State = VarState.Calculated;
            //relativeHumidity.State = VarState.Calculated;
         }
         //else if (relativeHumidity.IsSpecified && relativeHumidity.HasValue) {
         //   moistureContentDryBase.State = VarState.Calculated;
         //}
      }

      internal override double GetBoilingPoint(double pressure) {
         return 0.0;
      }

      internal override double GetEvaporationHeat(double temperature) {
         double evapHeat = 2500000.0;
         /*Phase cmp = streamComponents.MajorPhase;
         if (cmp is FluidPhase) {
            FluidPhase fp = cmp as FluidPhase;
            fp.CalculateThermalProperties(temperature.Value, pressure.Value);
            evapHeat = fp.EvaporationHeat;
         }*/
         return evapHeat;
      }

      internal override double GetGasCp(double temperature) {
         double mcDryBase = moistureContentDryBase.Value;
         //double cpDryBase = GetHumidGasCalculator().GetHumidHeat(mcDryBase, temperature);
         double cpDryBase = HumidGasCalculator.GetHumidHeat(mcDryBase, temperature);
         return cpDryBase / (1.0 + mcDryBase);
      }

      internal override double GetGasViscosity(double temperature) {
         return materialPropCalculator.GetGasViscosity(materialComponents.MajorPhase.Components, temperature);
      }

      internal override double GetGasThermalConductivity(double temperature) {
         return materialPropCalculator.GetGasThermalConductivity(materialComponents.MajorPhase.Components, temperature);
      }

      internal override double GetGasDensity(double temperature, double pressure) {
         double mcDryBase = moistureContentDryBase.Value;
         //double humidVolumeValue = GetHumidGasCalculator().GetHumidVolume(temperature, mcDryBase, pressure);
         double humidVolumeValue = HumidGasCalculator.GetHumidVolume(temperature, mcDryBase, pressure);
         return (1.0 + mcDryBase) / humidVolumeValue;
      }

      //private HumidGasCalculator GetHumidGasCalculator()
      //{
      //   return new HumidGasCalculator((Components as DryingGasComponents).DryMedium.Substance, (unitOpSystem as EvaporationAndDryingSystem).DryingMaterial.Moisture);
      //}

      protected DryingGasStream(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionDryingGasStream");
         this.wetBulbTemperature = RecallStorableObject("WetBulbTemperature", typeof(ProcessVarDouble)) as ProcessVarDouble;
         this.dewPoint = RecallStorableObject("DewPoint", typeof(ProcessVarDouble)) as ProcessVarDouble;
         this.relativeHumidity = RecallStorableObject("RelativeHumidity", typeof(ProcessVarDouble)) as ProcessVarDouble;
         this.humidVolume = RecallStorableObject("HumidVolume", typeof(ProcessVarDouble)) as ProcessVarDouble;
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingGasStream", DryingGasStream.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("WetBulbTemperature", this.wetBulbTemperature, typeof(ProcessVarDouble));
         info.AddValue("DewPoint", this.dewPoint, typeof(ProcessVarDouble));
         info.AddValue("RelativeHumidity", this.relativeHumidity, typeof(ProcessVarDouble));
         info.AddValue("HumidVolume", this.humidVolume, typeof(ProcessVarDouble));
      }
   }
}

            //if (aHasValue()) {
               //if (wetBulbTemperature.IsSpecified && wetBulbTemperature.HasValue &&
               //   dewPoint.IsSpecified && dewPoint.HasValue) {
               //   WarnSpecifiedValueConflict("Wet bulb temperature and dew point have already been specified.\nYou may need to specify pressure to get the stream solve.");
               //   aValue = Constants.NO_VALUE;
               //}
               //else if ((wetBulbTemperature.IsSpecified && wetBulbTemperature.HasValue && aValue < wetBulbTemperature.Value) ||
               //   (dewPoint.IsSpecified && dewPoint.HasValue && aValue < dewPoint.Value)) {
               //   WarnSpecifiedValueConflict("Specified dry bulb temperature must be greater than specified wet buld temperature and specified dew point");
               //}
               //else if (!IsEqual(temperature, aValue)) {
               //   Specify(temperature, aValue);
               //}
            //else {
            
            //if (aHasValue()) {
            //   if (temperature.IsSpecified && temperature.HasValue &&
            //      dewPoint.IsSpecified && dewPoint.HasValue) {
            //      WarnSpecifiedValueConflict("Dry bulb temperature and dew point have already been specified.\nYou may need to specify pressure to get the stream solve.");
            //      aValue = Constants.NO_VALUE;
            //   }
            //   else if ((temperature.IsSpecified && temperature.HasValue && aValue > temperature.Value) ||
            //   (dewPoint.IsSpecified && dewPoint.Value == Constants.NO_VALUE && aValue < dewPoint.Value)) {
            //      WarnSpecifiedValueConflict("Specified wet bulb temperature must be smaller than specified dry buld temperature and greater than specified dew point");
            //   }
            //   else if (!IsEqual(wetBulbTemperature, aValue)) {
            //      Specify(wetBulbTemperature, aValue);
            //   }
            //}
            //else {
                       //if (aHasValue()) {
            //   if (temperature.IsSpecified && temperature.HasValue &&
            //      wetBulbTemperature.IsSpecified && wetBulbTemperature.HasValue) {
            //      WarnSpecifiedValueConflict("Dry bulb temperature and wet bulb temperature have already been specified.\nYou may need to specify pressure to get the stream solve.");
            //      aValue = Constants.NO_VALUE;
            //   }
            //   else if ((temperature.IsSpecified && temperature.HasValue && aValue > temperature.Value) ||
            //      (wetBulbTemperature.IsSpecified && wetBulbTemperature.HasValue && aValue > wetBulbTemperature.Value)) {
            //      WarnSpecifiedValueConflict("Specified wet bulb temperature must be smaller than specified dry buld temperature and greater than specified dew point");
            //   }
            //   else if (!IsEqual(dewPoint, aValue)) {
            //      Specify(dewPoint, aValue);
            //   }
            //}
            //else {

            //if (aHasValue() && massFlowRateDryBase.IsSpecified && massFlowRateDryBase.HasValue) {
            //   WarnSpecifiedValueConflict("Dry base and wet base mass flow rate cannot be specified at the same time");
            //   aValue = Constants.NO_VALUE;
            //}
            //else if (volumeFlowRate.IsSpecified && volumeFlowRate.HasValue) {
            //   WarnSpecifiedValueConflict("Wet base mass flow rate and volume flow rate cannot be specified at the same time");
            //   aValue = Constants.NO_VALUE;
            //}
            
            //if (aHasValue()) {
            //   if (massFlowRate.IsSpecified && massFlowRate.HasValue) {
            //      WarnSpecifiedValueConflict("Dry base and wet base mass flow rate cannot be specified at the same time");
            //      aValue = Constants.NO_VALUE;
            //   }               
            //   else if (volumeFlowRate.IsSpecified && volumeFlowRate.HasValue) {
            //      WarnSpecifiedValueConflict("Dry base mass flow rate and volume flow rate cannot be specified at the same time");
            //      aValue = Constants.NO_VALUE;
            //   }
            //}

            //if (aHasValue()) {
            //   if (massFlowRateDryBase.IsSpecified && massFlowRateDryBase.HasValue) {
            //      WarnSpecifiedValueConflict("Volume flow rate and dry base mass flow rate cannot be specified at the same time");
            //      aValue = Constants.NO_VALUE;
            //   }
            //   if (massFlowRate.IsSpecified && massFlowRate.HasValue) {
            //      WarnSpecifiedValueConflict("Volume flow rate and dry base mass flow rate cannot be specified at the same time");
            //      aValue = Constants.NO_VALUE;
            //   }               
            //}
