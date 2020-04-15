using System;
using System.Collections;
using System.Reflection;

namespace Prosimo.UnitSystems
{
   
   /// <summary>
	/// Summary description for UnitConverter.
	/// </summary>
   public class UnitConverter {
      private UnitSystem currentUnitSystem;
      private static Hashtable pqStringTable = new Hashtable();
      private static Hashtable pqTypeTable = new Hashtable();

      public UnitConverter() {
         //InitializePhysicalQuqntityTables();
      }

      public UnitConverter(UnitSystem unitSys) {
         currentUnitSystem = unitSys;
      }

      internal UnitSystem UnitSystem {
         get { return currentUnitSystem; }
         set { currentUnitSystem = value; }
      }

      static UnitConverter() {
         pqStringTable.Add(PhysicalQuantity.Area, "Area");
         pqTypeTable.Add(PhysicalQuantity.Area, typeof(AreaUnit));

         pqStringTable.Add(PhysicalQuantity.Density, "Density");
         pqTypeTable.Add(PhysicalQuantity.Density, typeof(DensityUnit));

         pqStringTable.Add(PhysicalQuantity.Diffusivity, "Diffusivity");
         pqTypeTable.Add(PhysicalQuantity.Diffusivity, typeof(DiffusivityUnit));

         pqStringTable.Add(PhysicalQuantity.DynamicViscosity, "Dynamic Viscosity");
         pqTypeTable.Add(PhysicalQuantity.DynamicViscosity, typeof(DynamicViscosityUnit));

         pqStringTable.Add(PhysicalQuantity.Energy, "Energy");
         pqTypeTable.Add(PhysicalQuantity.Energy, typeof(EnergyUnit));

         pqStringTable.Add(PhysicalQuantity.FoulingFactor, "Fouling Factor");
         pqTypeTable.Add(PhysicalQuantity.FoulingFactor, typeof(FoulingFactorUnit));

         pqStringTable.Add(PhysicalQuantity.Fraction, "Fraction");
         pqTypeTable.Add(PhysicalQuantity.Fraction, typeof(FractionUnit));

         pqStringTable.Add(PhysicalQuantity.VolumeHeatTransferCoefficient, "Volume Heat Transfer Coefficient");
         pqTypeTable.Add(PhysicalQuantity.VolumeHeatTransferCoefficient, typeof(VolumeHeatTransferCoefficientUnit));

         pqStringTable.Add(PhysicalQuantity.HeatTransferCoefficient, "Heat Transfer Coefficient");
         pqTypeTable.Add(PhysicalQuantity.HeatTransferCoefficient, typeof(HeatTransferCoefficientUnit));
         
         pqStringTable.Add(PhysicalQuantity.KinematicViscosity, "Kinematic Viscosity");
         pqTypeTable.Add(PhysicalQuantity.KinematicViscosity, typeof(KinematicViscosityUnit));

         pqStringTable.Add(PhysicalQuantity.Length, "Length");
         pqTypeTable.Add(PhysicalQuantity.Length, typeof(LengthUnit));

         pqStringTable.Add(PhysicalQuantity.LiquidHead, "Liquid Head");
         pqTypeTable.Add(PhysicalQuantity.LiquidHead, typeof(LiquidHeadUnit));

         pqStringTable.Add(PhysicalQuantity.Mass, "Mass");
         pqTypeTable.Add(PhysicalQuantity.Mass, typeof(MassUnit));

         pqStringTable.Add(PhysicalQuantity.MassFlowRate, "Mass Flow Rate");
         pqTypeTable.Add(PhysicalQuantity.MassFlowRate, typeof(MassFlowRateUnit));

         pqStringTable.Add(PhysicalQuantity.MassVolumeConcentration, "Mass Volume Concentration");
         pqTypeTable.Add(PhysicalQuantity.MassVolumeConcentration, typeof(MassVolumeConcentrationUnit));

         pqStringTable.Add(PhysicalQuantity.MicroLength, "Micro Length");
         pqTypeTable.Add(PhysicalQuantity.MicroLength, typeof(MicroLengthUnit));

         pqStringTable.Add(PhysicalQuantity.MoistureContent, "Moisture Content");
         pqTypeTable.Add(PhysicalQuantity.MoistureContent, typeof(MoistureContentUnit));
         
         pqStringTable.Add(PhysicalQuantity.PlaneAngle, "Plane Angle");
         pqTypeTable.Add(PhysicalQuantity.PlaneAngle, typeof(PlaneAngleUnit));
         
         pqStringTable.Add(PhysicalQuantity.Power, "Power");
         pqTypeTable.Add(PhysicalQuantity.Power, typeof(PowerUnit));
         
         pqStringTable.Add(PhysicalQuantity.Pressure, "Pressure");
         pqTypeTable.Add(PhysicalQuantity.Pressure, typeof(PressureUnit));
         
         pqStringTable.Add(PhysicalQuantity.SmallLength, "Small Length");
         pqTypeTable.Add(PhysicalQuantity.SmallLength, typeof(SmallLengthUnit));
         
         pqStringTable.Add(PhysicalQuantity.SpecificHeat, "Specific Heat");
         pqTypeTable.Add(PhysicalQuantity.SpecificHeat, typeof(SpecificHeatUnit));
         
         pqStringTable.Add(PhysicalQuantity.SpecificEnergy, "Specific Energy");
         pqTypeTable.Add(PhysicalQuantity.SpecificEnergy, typeof(SpecificEnergyUnit));
         
         pqStringTable.Add(PhysicalQuantity.SpecificVolume, "Specific Volume");
         pqTypeTable.Add(PhysicalQuantity.SpecificVolume, typeof(SpecificVolumeUnit));
         
         pqStringTable.Add(PhysicalQuantity.SurfaceTension, "Surface Tension");
         pqTypeTable.Add(PhysicalQuantity.SurfaceTension, typeof(SurfaceTensionUnit));
         
         pqStringTable.Add(PhysicalQuantity.Temperature, "Temperature");
         pqTypeTable.Add(PhysicalQuantity.Temperature, typeof(TemperatureUnit));
         
         pqStringTable.Add(PhysicalQuantity.ThermalConductivity, "Thermal Conductivity");
         pqTypeTable.Add(PhysicalQuantity.ThermalConductivity, typeof(ThermalConductivityUnit));
         
         pqStringTable.Add(PhysicalQuantity.Time, "Time");
         pqTypeTable.Add(PhysicalQuantity.Time, typeof(TimeUnit));
         
         pqStringTable.Add(PhysicalQuantity.Velocity, "Velocity");
         pqTypeTable.Add(PhysicalQuantity.Velocity, typeof(VelocityUnit));
         
         pqStringTable.Add(PhysicalQuantity.Volume, "Volume");
         pqTypeTable.Add(PhysicalQuantity.Volume, typeof(VolumeUnit));
         
         pqStringTable.Add(PhysicalQuantity.VolumeFlowRate, "Volume Flow Rate");
         pqTypeTable.Add(PhysicalQuantity.VolumeFlowRate, typeof(VolumeFlowRateUnit));
         
         pqStringTable.Add(PhysicalQuantity.VolumeRateFlowGases, "Volume Rate Flow Gases");
         pqTypeTable.Add(PhysicalQuantity.VolumeRateFlowGases, typeof(VolumeRateFlowGasesUnit));

         pqStringTable.Add(PhysicalQuantity.VolumeRateFlowLiquids, "Volume Rate Flow Liquids");
         pqTypeTable.Add(PhysicalQuantity.VolumeRateFlowLiquids, typeof(VolumeRateFlowLiquidsUnit));
         
         pqStringTable.Add(PhysicalQuantity.MoleFlowRate, "Mole Flow Rate");
         pqTypeTable.Add(PhysicalQuantity.MoleFlowRate, typeof(MoleFlowRateUnit));
      }

      public double ConvertToSIValue(PhysicalQuantity varType, double toBeConverted) {
         //Type myType = pqTypeTable[varType] as Type;
         //return (double)myType.InvokeMember("ConvertToSIValue", BindingFlags.Static, null, null, new object[] {varType, toBeConverted});
         
         double siValue = 0;
         if (varType == PhysicalQuantity.Temperature) {
            siValue = TemperatureUnit.Instance.ConvertToSIValue(currentUnitSystem.TemperatureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            siValue = PressureUnit.Instance.ConvertToSIValue <PressureUnitType>(currentUnitSystem.PressureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            siValue = MassFlowRateUnit.Instance.ConvertToSIValue <MassFlowRateUnitType>(currentUnitSystem.MassFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            siValue = VolumeFlowRateUnit.Instance.ConvertToSIValue <VolumeFlowRateUnitType>(currentUnitSystem.VolumeFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            siValue = VolumeRateFlowGasesUnit.Instance.ConvertToSIValue <VolumeRateFlowGasesUnitType>(currentUnitSystem.VolumeRateFlowGasesUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            siValue = VolumeRateFlowLiquidsUnit.Instance.ConvertToSIValue <VolumeRateFlowLiquidsUnitType>(currentUnitSystem.VolumeRateFlowLiquidsUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            siValue = MoistureContentUnit.Instance.ConvertToSIValue <MoistureContentUnitType>(currentUnitSystem.MoistureContentUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            siValue = SpecificEnergyUnit.Instance.ConvertToSIValue <SpecificEnergyUnitType>(currentUnitSystem.SpecificEnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            siValue = SpecificHeatUnit.Instance.ConvertToSIValue<SpecificHeatUnitType>(currentUnitSystem.SpecificHeatUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            siValue = EnergyUnit.Instance.ConvertToSIValue<EnergyUnitType>(currentUnitSystem.EnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Power) {
            siValue = PowerUnit.Instance.ConvertToSIValue <PowerUnitType>(currentUnitSystem.PowerUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Density) {
            siValue = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(currentUnitSystem.DensityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            siValue = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(currentUnitSystem.SpecificVolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            siValue = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(currentUnitSystem.DynamicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            siValue = KinematicViscosityUnit.Instance.ConvertToSIValue<KinematicViscosityUnitType>(currentUnitSystem.KinematicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            siValue = ThermalConductivityUnit.Instance.ConvertToSIValue<ThermalConductivityUnitType>(currentUnitSystem.ThermalConductivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            siValue = HeatTransferCoefficientUnit.Instance.ConvertToSIValue<HeatTransferCoefficientUnitType>(currentUnitSystem.HeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            siValue = VolumeHeatTransferCoefficientUnit.Instance.ConvertToSIValue<VolumeHeatTransferCoefficientUnitType>(currentUnitSystem.VolumeHeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            siValue = FoulingFactorUnit.Instance.ConvertToSIValue<FoulingFactorUnitType>(currentUnitSystem.FoulingFactorUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            siValue = DiffusivityUnit.Instance.ConvertToSIValue<DiffusivityUnitType>(currentUnitSystem.DiffusivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            siValue = VelocityUnit.Instance.ConvertToSIValue<VelocityUnitType>(currentUnitSystem.VelocityUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Mass) {
            siValue = MassUnit.Instance.ConvertToSIValue<MassUnitType>(currentUnitSystem.MassUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Length) {
            siValue = LengthUnit.Instance.ConvertToSIValue<LengthUnitType>(currentUnitSystem.LengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            siValue = SmallLengthUnit.Instance.ConvertToSIValue<SmallLengthUnitType>(currentUnitSystem.SmallLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            siValue = MicroLengthUnit.Instance.ConvertToSIValue<MicroLengthUnitType>(currentUnitSystem.MicroLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Area) {
            siValue = AreaUnit.Instance.ConvertToSIValue<AreaUnitType>(currentUnitSystem.AreaUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            siValue = VolumeUnit.Instance.ConvertToSIValue<VolumeUnitType>(currentUnitSystem.VolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Time) {
            siValue = TimeUnit.Instance.ConvertToSIValue<TimeUnitType>(currentUnitSystem.TimeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            siValue = FractionUnit.Instance.ConvertToSIValue<FractionUnitType>(currentUnitSystem.FractionUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            siValue = LiquidHeadUnit.Instance.ConvertToSIValue<LiquidHeadUnitType>(currentUnitSystem.LiquidHeadUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            siValue = MassVolumeConcentrationUnit.Instance.ConvertToSIValue<MassVolumeConcentrationUnitType>(currentUnitSystem.MassVolumeConcentrationUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            siValue = PlaneAngleUnit.Instance.ConvertToSIValue<PlaneAngleUnitType>(currentUnitSystem.PlaneAngleUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            siValue = HeatFluxUnit.Instance.ConvertToSIValue<HeatFluxUnitType>(currentUnitSystem.HeatFluxUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MoleFlowRate) {
            siValue = MoleFlowRateUnit.Instance.ConvertToSIValue<MoleFlowRateUnitType>(currentUnitSystem.MoleFlowRateUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Unknown) {
            siValue = toBeConverted; 
         }

         return siValue;
      }
      
      public double ConvertFromSIValue(PhysicalQuantity varType, double toBeConverted) {
         ///Type myType = pqTypeTable[varType] as Type;
         //return (double) myType.InvokeMember("ConvertFromSIValue", BindingFlags.Static, null, null, new object[] {varType, toBeConverted});
         
         double convertedValue = 0;
         if (varType == PhysicalQuantity.Temperature) {
            convertedValue = TemperatureUnit.Instance.ConvertFromSIValue(currentUnitSystem.TemperatureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            convertedValue = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(currentUnitSystem.PressureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            convertedValue = MassFlowRateUnit.Instance.ConvertFromSIValue <MassFlowRateUnitType>(currentUnitSystem.MassFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            convertedValue = VolumeFlowRateUnit.Instance.ConvertFromSIValue<VolumeFlowRateUnitType>(currentUnitSystem.VolumeFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            convertedValue = VolumeRateFlowGasesUnit.Instance.ConvertFromSIValue<VolumeRateFlowGasesUnitType>(currentUnitSystem.VolumeRateFlowGasesUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            convertedValue = VolumeRateFlowLiquidsUnit.Instance.ConvertFromSIValue<VolumeRateFlowLiquidsUnitType>(currentUnitSystem.VolumeRateFlowLiquidsUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            convertedValue = MoistureContentUnit.Instance.ConvertFromSIValue<MoistureContentUnitType>(currentUnitSystem.MoistureContentUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            convertedValue = SpecificEnergyUnit.Instance.ConvertFromSIValue<SpecificEnergyUnitType>(currentUnitSystem.SpecificEnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            convertedValue = SpecificHeatUnit.Instance.ConvertFromSIValue<SpecificHeatUnitType>(currentUnitSystem.SpecificHeatUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            convertedValue = EnergyUnit.Instance.ConvertFromSIValue<EnergyUnitType>(currentUnitSystem.EnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Power) {
            convertedValue = PowerUnit.Instance.ConvertFromSIValue<PowerUnitType>(currentUnitSystem.PowerUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Density) {
            convertedValue = DensityUnit.Instance.ConvertFromSIValue<DensityUnitType>(currentUnitSystem.DensityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            convertedValue = SpecificVolumeUnit.Instance.ConvertFromSIValue<SpecificVolumeUnitType>(currentUnitSystem.SpecificVolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            convertedValue = DynamicViscosityUnit.Instance.ConvertFromSIValue<DynamicViscosityUnitType>(currentUnitSystem.DynamicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            convertedValue = KinematicViscosityUnit.Instance.ConvertFromSIValue<KinematicViscosityUnitType>(currentUnitSystem.KinematicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            convertedValue = ThermalConductivityUnit.Instance.ConvertFromSIValue<ThermalConductivityUnitType>(currentUnitSystem.ThermalConductivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            convertedValue = HeatTransferCoefficientUnit.Instance.ConvertFromSIValue<HeatTransferCoefficientUnitType>(currentUnitSystem.HeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            convertedValue = VolumeHeatTransferCoefficientUnit.Instance.ConvertFromSIValue<VolumeHeatTransferCoefficientUnitType>(currentUnitSystem.VolumeHeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            convertedValue = FoulingFactorUnit.Instance.ConvertFromSIValue<FoulingFactorUnitType>(currentUnitSystem.FoulingFactorUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            convertedValue = DiffusivityUnit.Instance.ConvertFromSIValue<DiffusivityUnitType>(currentUnitSystem.DiffusivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            convertedValue = VelocityUnit.Instance.ConvertFromSIValue<VelocityUnitType>(currentUnitSystem.VelocityUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Mass) {
            convertedValue = MassUnit.Instance.ConvertFromSIValue<MassUnitType>(currentUnitSystem.MassUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Length) {
            convertedValue = LengthUnit.Instance.ConvertFromSIValue<LengthUnitType>(currentUnitSystem.LengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            convertedValue = SmallLengthUnit.Instance.ConvertFromSIValue<SmallLengthUnitType>(currentUnitSystem.SmallLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            convertedValue = MicroLengthUnit.Instance.ConvertFromSIValue<MicroLengthUnitType>(currentUnitSystem.MicroLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Area) {
            convertedValue = AreaUnit.Instance.ConvertFromSIValue<AreaUnitType>(currentUnitSystem.AreaUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            convertedValue = VolumeUnit.Instance.ConvertFromSIValue<VolumeUnitType>(currentUnitSystem.VolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Time) {
            convertedValue = TimeUnit.Instance.ConvertFromSIValue<TimeUnitType>(currentUnitSystem.TimeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            convertedValue = FractionUnit.Instance.ConvertFromSIValue<FractionUnitType>(currentUnitSystem.FractionUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            convertedValue = LiquidHeadUnit.Instance.ConvertFromSIValue<LiquidHeadUnitType>(currentUnitSystem.LiquidHeadUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            convertedValue = MassVolumeConcentrationUnit.Instance.ConvertFromSIValue<MassVolumeConcentrationUnitType>(currentUnitSystem.MassVolumeConcentrationUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            convertedValue = PlaneAngleUnit.Instance.ConvertFromSIValue<PlaneAngleUnitType>(currentUnitSystem.PlaneAngleUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            convertedValue = HeatFluxUnit.Instance.ConvertFromSIValue<HeatFluxUnitType>(currentUnitSystem.HeatFluxUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.MoleFlowRate) {
            convertedValue = MoleFlowRateUnit.Instance.ConvertFromSIValue<MoleFlowRateUnitType>(currentUnitSystem.MoleFlowRateUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Unknown) {
            convertedValue = toBeConverted; 
         }

         return convertedValue;
      }
      
      public string GetUnitAsString(PhysicalQuantity varType) {
         //Type myType = pqTypeTable[varType] as Type;
         //return myType.InvokeMember("GetUnitAsString", BindingFlags.Static, null, null, new object[] {varType}) as String;
         
         string unitString = "";
         if (varType == PhysicalQuantity.Temperature) {
            unitString = TemperatureUnit.Instance.GetUnitAsString(currentUnitSystem.TemperatureUnitType);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            unitString = PressureUnit.Instance.GetUnitAsString<PressureUnitType>(currentUnitSystem.PressureUnitType);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            unitString = MassFlowRateUnit.Instance.GetUnitAsString <MassFlowRateUnitType>(currentUnitSystem.MassFlowRateUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            unitString = VolumeFlowRateUnit.Instance.GetUnitAsString<VolumeFlowRateUnitType>(currentUnitSystem.VolumeFlowRateUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            unitString = VolumeRateFlowGasesUnit.Instance.GetUnitAsString<VolumeRateFlowGasesUnitType>(currentUnitSystem.VolumeRateFlowGasesUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            unitString = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsString<VolumeRateFlowLiquidsUnitType>(currentUnitSystem.VolumeRateFlowLiquidsUnitType); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            unitString = MoistureContentUnit.Instance.GetUnitAsString<MoistureContentUnitType>(currentUnitSystem.MoistureContentUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            unitString = SpecificEnergyUnit.Instance.GetUnitAsString<SpecificEnergyUnitType>(currentUnitSystem.SpecificEnergyUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            unitString = SpecificHeatUnit.Instance.GetUnitAsString<SpecificHeatUnitType>(currentUnitSystem.SpecificHeatUnitType); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            unitString = EnergyUnit.Instance.GetUnitAsString<EnergyUnitType>(currentUnitSystem.EnergyUnitType); 
         }
         else if (varType == PhysicalQuantity.Power) {
            unitString = PowerUnit.Instance.GetUnitAsString<PowerUnitType>(currentUnitSystem.PowerUnitType); 
         }
         else if (varType == PhysicalQuantity.Density) {
            unitString = DensityUnit.Instance.GetUnitAsString<DensityUnitType>(currentUnitSystem.DensityUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            unitString = SpecificVolumeUnit.Instance.GetUnitAsString<SpecificVolumeUnitType>(currentUnitSystem.SpecificVolumeUnitType); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            unitString = DynamicViscosityUnit.Instance.GetUnitAsString<DynamicViscosityUnitType>(currentUnitSystem.DynamicViscosityUnitType); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            unitString = KinematicViscosityUnit.Instance.GetUnitAsString<KinematicViscosityUnitType>(currentUnitSystem.KinematicViscosityUnitType); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            unitString = ThermalConductivityUnit.Instance.GetUnitAsString<ThermalConductivityUnitType>(currentUnitSystem.ThermalConductivityUnitType); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            unitString = HeatTransferCoefficientUnit.Instance.GetUnitAsString<HeatTransferCoefficientUnitType>(currentUnitSystem.HeatTransferCoefficientUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            unitString = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsString<VolumeHeatTransferCoefficientUnitType>(currentUnitSystem.VolumeHeatTransferCoefficientUnitType); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            unitString = FoulingFactorUnit.Instance.GetUnitAsString<FoulingFactorUnitType>(currentUnitSystem.FoulingFactorUnitType); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            unitString = DiffusivityUnit.Instance.GetUnitAsString<DiffusivityUnitType>(currentUnitSystem.DiffusivityUnitType); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            unitString = VelocityUnit.Instance.GetUnitAsString<VelocityUnitType>(currentUnitSystem.VelocityUnitType); 
         }
         else if (varType == PhysicalQuantity.Mass) {
            unitString = MassUnit.Instance.GetUnitAsString<MassUnitType>(currentUnitSystem.MassUnitType); 
         }
         else if (varType == PhysicalQuantity.Length) {
            unitString = LengthUnit.Instance.GetUnitAsString<LengthUnitType>(currentUnitSystem.LengthUnitType); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            unitString = SmallLengthUnit.Instance.GetUnitAsString<SmallLengthUnitType>(currentUnitSystem.SmallLengthUnitType); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            unitString = MicroLengthUnit.Instance.GetUnitAsString<MicroLengthUnitType>(currentUnitSystem.MicroLengthUnitType); 
         }
         else if (varType == PhysicalQuantity.Area) {
            unitString = AreaUnit.Instance.GetUnitAsString<AreaUnitType>(currentUnitSystem.AreaUnitType); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            unitString = VolumeUnit.Instance.GetUnitAsString<VolumeUnitType>(currentUnitSystem.VolumeUnitType); 
         }
         else if (varType == PhysicalQuantity.Time) {
            unitString = TimeUnit.Instance.GetUnitAsString<TimeUnitType>(currentUnitSystem.TimeUnitType); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            unitString = FractionUnit.Instance.GetUnitAsString<FractionUnitType>(currentUnitSystem.FractionUnitType); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            unitString = LiquidHeadUnit.Instance.GetUnitAsString<LiquidHeadUnitType>(currentUnitSystem.LiquidHeadUnitType); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            unitString = MassVolumeConcentrationUnit.Instance.GetUnitAsString<MassVolumeConcentrationUnitType>(currentUnitSystem.MassVolumeConcentrationUnitType); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            unitString = PlaneAngleUnit.Instance.GetUnitAsString<PlaneAngleUnitType>(currentUnitSystem.PlaneAngleUnitType); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            unitString = HeatFluxUnit.Instance.GetUnitAsString<HeatFluxUnitType>(currentUnitSystem.HeatFluxUnitType); 
         }
         else if (varType == PhysicalQuantity.MoleFlowRate) {
            unitString = MoleFlowRateUnit.Instance.GetUnitAsString<MoleFlowRateUnitType>(currentUnitSystem.MoleFlowRateUnitType);
         }
         else if (varType == PhysicalQuantity.Unknown) {
            unitString = ""; 
         }

         return unitString;
      }
      public ICollection GetUnitsAsStrings(PhysicalQuantity varType) {
         Type myType = pqTypeTable[varType] as Type;
         return myType.InvokeMember("GetUnitsAsStrings", BindingFlags.Static, null, null, null) as ICollection;
      }

      public ICollection GetPhysicalQuantitiesAsStrings() {
         return pqStringTable.Values;
      }

      public string GetPhysicalQuantityAsString(PhysicalQuantity physicalQuantity) {
         return pqStringTable[physicalQuantity] as String;
      }
      
      public PhysicalQuantity GetPhysicalQuantityAsEnum(string physicalQuantityStr) {
         PhysicalQuantity physicalQuantity = PhysicalQuantity.Unknown;
         IDictionaryEnumerator myEnumerator = pqStringTable.GetEnumerator();
         String name;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(physicalQuantityStr)) {
               physicalQuantity = (PhysicalQuantity) myEnumerator.Key;
               break;
            }
         }

         return physicalQuantity;
      }
   }
}
