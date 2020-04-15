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
      }

      public double ConvertToSIValue(PhysicalQuantity varType, double toBeConverted) {
         //Type myType = pqTypeTable[varType] as Type;
         //return (double)myType.InvokeMember("ConvertToSIValue", BindingFlags.Static, null, null, new object[] {varType, toBeConverted});
         
         double siValue = 0;
         if (varType == PhysicalQuantity.Temperature) {
            siValue = TemperatureUnit.ConvertToSIValue(currentUnitSystem.TemperatureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            siValue = PressureUnit.ConvertToSIValue(currentUnitSystem.PressureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            siValue = MassFlowRateUnit.ConvertToSIValue(currentUnitSystem.MassFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            siValue = VolumeFlowRateUnit.ConvertToSIValue(currentUnitSystem.VolumeFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            siValue = VolumeRateFlowGasesUnit.ConvertToSIValue(currentUnitSystem.VolumeRateFlowGasesUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            siValue = VolumeRateFlowLiquidsUnit.ConvertToSIValue(currentUnitSystem.VolumeRateFlowLiquidsUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            siValue = MoistureContentUnit.ConvertToSIValue(currentUnitSystem.MoistureContentUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            siValue = SpecificEnergyUnit.ConvertToSIValue(currentUnitSystem.SpecificEnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            siValue = SpecificHeatUnit.ConvertToSIValue(currentUnitSystem.SpecificHeatUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            siValue = EnergyUnit.ConvertToSIValue(currentUnitSystem.EnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Power) {
            siValue = PowerUnit.ConvertToSIValue(currentUnitSystem.PowerUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Density) {
            siValue = DensityUnit.ConvertToSIValue(currentUnitSystem.DensityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            siValue = SpecificVolumeUnit.ConvertToSIValue(currentUnitSystem.SpecificVolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            siValue = DynamicViscosityUnit.ConvertToSIValue(currentUnitSystem.DynamicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            siValue = KinematicViscosityUnit.ConvertToSIValue(currentUnitSystem.KinematicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            siValue = ThermalConductivityUnit.ConvertToSIValue(currentUnitSystem.ThermalConductivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            siValue = HeatTransferCoefficientUnit.ConvertToSIValue(currentUnitSystem.HeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            siValue = VolumeHeatTransferCoefficientUnit.ConvertToSIValue(currentUnitSystem.VolumeHeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            siValue = FoulingFactorUnit.ConvertToSIValue(currentUnitSystem.FoulingFactorUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            siValue = DiffusivityUnit.ConvertToSIValue(currentUnitSystem.DiffusivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            siValue = VelocityUnit.ConvertToSIValue(currentUnitSystem.VelocityUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Mass) {
            siValue = MassUnit.ConvertToSIValue(currentUnitSystem.MassUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Length) {
            siValue = LengthUnit.ConvertToSIValue(currentUnitSystem.LengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            siValue = SmallLengthUnit.ConvertToSIValue(currentUnitSystem.SmallLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            siValue = MicroLengthUnit.ConvertToSIValue(currentUnitSystem.MicroLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Area) {
            siValue = AreaUnit.ConvertToSIValue(currentUnitSystem.AreaUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            siValue = VolumeUnit.ConvertToSIValue(currentUnitSystem.VolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Time) {
            siValue = TimeUnit.ConvertToSIValue(currentUnitSystem.TimeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            siValue = FractionUnit.ConvertToSIValue(currentUnitSystem.FractionUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            siValue = LiquidHeadUnit.ConvertToSIValue(currentUnitSystem.LiquidHeadUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            siValue = MassVolumeConcentrationUnit.ConvertToSIValue(currentUnitSystem.MassVolumeConcentrationUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            siValue = PlaneAngleUnit.ConvertToSIValue(currentUnitSystem.PlaneAngleUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            siValue = HeatFluxUnit.ConvertToSIValue(currentUnitSystem.HeatFluxUnitType, toBeConverted); 
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
            convertedValue = TemperatureUnit.ConvertFromSIValue(currentUnitSystem.TemperatureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            convertedValue = PressureUnit.ConvertFromSIValue(currentUnitSystem.PressureUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            convertedValue = MassFlowRateUnit.ConvertFromSIValue(currentUnitSystem.MassFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            convertedValue = VolumeFlowRateUnit.ConvertFromSIValue(currentUnitSystem.VolumeFlowRateUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            convertedValue = VolumeRateFlowGasesUnit.ConvertFromSIValue(currentUnitSystem.VolumeRateFlowGasesUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            convertedValue = VolumeRateFlowLiquidsUnit.ConvertFromSIValue(currentUnitSystem.VolumeRateFlowLiquidsUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            convertedValue = MoistureContentUnit.ConvertFromSIValue(currentUnitSystem.MoistureContentUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            convertedValue = SpecificEnergyUnit.ConvertFromSIValue(currentUnitSystem.SpecificEnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            convertedValue = SpecificHeatUnit.ConvertFromSIValue(currentUnitSystem.SpecificHeatUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            convertedValue = EnergyUnit.ConvertFromSIValue(currentUnitSystem.EnergyUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Power) {
            convertedValue = PowerUnit.ConvertFromSIValue(currentUnitSystem.PowerUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Density) {
            convertedValue = DensityUnit.ConvertFromSIValue(currentUnitSystem.DensityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            convertedValue = SpecificVolumeUnit.ConvertFromSIValue(currentUnitSystem.SpecificVolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            convertedValue = DynamicViscosityUnit.ConvertFromSIValue(currentUnitSystem.DynamicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            convertedValue = KinematicViscosityUnit.ConvertFromSIValue(currentUnitSystem.KinematicViscosityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            convertedValue = ThermalConductivityUnit.ConvertFromSIValue(currentUnitSystem.ThermalConductivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            convertedValue = HeatTransferCoefficientUnit.ConvertFromSIValue(currentUnitSystem.HeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            convertedValue = VolumeHeatTransferCoefficientUnit.ConvertFromSIValue(currentUnitSystem.VolumeHeatTransferCoefficientUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            convertedValue = FoulingFactorUnit.ConvertFromSIValue(currentUnitSystem.FoulingFactorUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            convertedValue = DiffusivityUnit.ConvertFromSIValue(currentUnitSystem.DiffusivityUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            convertedValue = VelocityUnit.ConvertFromSIValue(currentUnitSystem.VelocityUnitType, toBeConverted);
         }
         else if (varType == PhysicalQuantity.Mass) {
            convertedValue = MassUnit.ConvertFromSIValue(currentUnitSystem.MassUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Length) {
            convertedValue = LengthUnit.ConvertFromSIValue(currentUnitSystem.LengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            convertedValue = SmallLengthUnit.ConvertFromSIValue(currentUnitSystem.SmallLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            convertedValue = MicroLengthUnit.ConvertFromSIValue(currentUnitSystem.MicroLengthUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Area) {
            convertedValue = AreaUnit.ConvertFromSIValue(currentUnitSystem.AreaUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            convertedValue = VolumeUnit.ConvertFromSIValue(currentUnitSystem.VolumeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Time) {
            convertedValue = TimeUnit.ConvertFromSIValue(currentUnitSystem.TimeUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            convertedValue = FractionUnit.ConvertFromSIValue(currentUnitSystem.FractionUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            convertedValue = LiquidHeadUnit.ConvertFromSIValue(currentUnitSystem.LiquidHeadUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            convertedValue = MassVolumeConcentrationUnit.ConvertFromSIValue(currentUnitSystem.MassVolumeConcentrationUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            convertedValue = PlaneAngleUnit.ConvertFromSIValue(currentUnitSystem.PlaneAngleUnitType, toBeConverted); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            convertedValue = HeatFluxUnit.ConvertFromSIValue(currentUnitSystem.HeatFluxUnitType, toBeConverted); 
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
            unitString = TemperatureUnit.GetUnitAsString(currentUnitSystem.TemperatureUnitType);
         }
         else if (varType == PhysicalQuantity.Pressure) {
            unitString = PressureUnit.GetUnitAsString(currentUnitSystem.PressureUnitType);
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            unitString = MassFlowRateUnit.GetUnitAsString(currentUnitSystem.MassFlowRateUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            unitString = VolumeFlowRateUnit.GetUnitAsString(currentUnitSystem.VolumeFlowRateUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            unitString = VolumeRateFlowGasesUnit.GetUnitAsString(currentUnitSystem.VolumeRateFlowGasesUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            unitString = VolumeRateFlowLiquidsUnit.GetUnitAsString(currentUnitSystem.VolumeRateFlowLiquidsUnitType); 
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            unitString = MoistureContentUnit.GetUnitAsString(currentUnitSystem.MoistureContentUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            unitString = SpecificEnergyUnit.GetUnitAsString(currentUnitSystem.SpecificEnergyUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            unitString = SpecificHeatUnit.GetUnitAsString(currentUnitSystem.SpecificHeatUnitType); 
         }
         else if (varType == PhysicalQuantity.Energy) {
            unitString = EnergyUnit.GetUnitAsString(currentUnitSystem.EnergyUnitType); 
         }
         else if (varType == PhysicalQuantity.Power) {
            unitString = PowerUnit.GetUnitAsString(currentUnitSystem.PowerUnitType); 
         }
         else if (varType == PhysicalQuantity.Density) {
            unitString = DensityUnit.GetUnitAsString(currentUnitSystem.DensityUnitType); 
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            unitString = SpecificVolumeUnit.GetUnitAsString(currentUnitSystem.SpecificVolumeUnitType); 
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            unitString = DynamicViscosityUnit.GetUnitAsString(currentUnitSystem.DynamicViscosityUnitType); 
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            unitString = KinematicViscosityUnit.GetUnitAsString(currentUnitSystem.KinematicViscosityUnitType); 
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            unitString = ThermalConductivityUnit.GetUnitAsString(currentUnitSystem.ThermalConductivityUnitType); 
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            unitString = HeatTransferCoefficientUnit.GetUnitAsString(currentUnitSystem.HeatTransferCoefficientUnitType); 
         }
         else if (varType == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            unitString = VolumeHeatTransferCoefficientUnit.GetUnitAsString(currentUnitSystem.VolumeHeatTransferCoefficientUnitType); 
         }
         else if (varType == PhysicalQuantity.FoulingFactor) 
         {
            unitString = FoulingFactorUnit.GetUnitAsString(currentUnitSystem.FoulingFactorUnitType); 
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            unitString = DiffusivityUnit.GetUnitAsString(currentUnitSystem.DiffusivityUnitType); 
         }
         else if (varType == PhysicalQuantity.Velocity) {
            unitString = VelocityUnit.GetUnitAsString(currentUnitSystem.VelocityUnitType); 
         }
         else if (varType == PhysicalQuantity.Mass) {
            unitString = MassUnit.GetUnitAsString(currentUnitSystem.MassUnitType); 
         }
         else if (varType == PhysicalQuantity.Length) {
            unitString = LengthUnit.GetUnitAsString(currentUnitSystem.LengthUnitType); 
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            unitString = SmallLengthUnit.GetUnitAsString(currentUnitSystem.SmallLengthUnitType); 
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            unitString = MicroLengthUnit.GetUnitAsString(currentUnitSystem.MicroLengthUnitType); 
         }
         else if (varType == PhysicalQuantity.Area) {
            unitString = AreaUnit.GetUnitAsString(currentUnitSystem.AreaUnitType); 
         }
         else if (varType == PhysicalQuantity.Volume) {
            unitString = VolumeUnit.GetUnitAsString(currentUnitSystem.VolumeUnitType); 
         }
         else if (varType == PhysicalQuantity.Time) {
            unitString = TimeUnit.GetUnitAsString(currentUnitSystem.TimeUnitType); 
         }
         else if (varType == PhysicalQuantity.Fraction) {
            unitString = FractionUnit.GetUnitAsString(currentUnitSystem.FractionUnitType); 
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            unitString = LiquidHeadUnit.GetUnitAsString(currentUnitSystem.LiquidHeadUnitType); 
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            unitString = MassVolumeConcentrationUnit.GetUnitAsString(currentUnitSystem.MassVolumeConcentrationUnitType); 
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            unitString = PlaneAngleUnit.GetUnitAsString(currentUnitSystem.PlaneAngleUnitType); 
         }
         else if (varType == PhysicalQuantity.HeatFlux) {
            unitString = HeatFluxUnit.GetUnitAsString(currentUnitSystem.HeatFluxUnitType); 
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

         /*ICollection list = null;
         if (varType == PhysicalQuantity.Area) {              
            list = AreaUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Density) {
            list = DensityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            list = DiffusivityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            list = DynamicViscosityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Energy) {
            list = EnergyUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.FoulingFactor) {
            list = FoulingFactorUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Fraction) {
            list = FractionUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            list = HeatTransferCoefficientUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            list = KinematicViscosityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Length) {
            list = LengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            list = LiquidHeadUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Mass) {
            list = MassUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            list = MassFlowRateUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            list = MassVolumeConcentrationUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            list = MicroLengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            list = MoistureContentUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            list = PlaneAngleUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Power) {
            list = PowerUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Pressure) {
            list = PressureUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            list = SmallLengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            list = SpecificHeatUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            list = SpecificEnergyUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            list = SpecificVolumeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SurfaceTension) {
            list = SurfaceTensionUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Temperature) {
            list = TemperatureUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            list = ThermalConductivityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Time) {
            list = TimeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Velocity) {
            list = VelocityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Volume) {
            list = VolumeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            list = VolumeFlowRateUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            list = VolumeRateFlowGasesUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            list = VolumeRateFlowLiquidsUnit.GetUnitsAsStrings();
         }

         return list;*/

