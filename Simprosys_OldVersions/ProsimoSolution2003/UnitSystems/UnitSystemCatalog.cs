using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public delegate void UnitSystemAddedEventHandler(UnitSystem unitSystem);
   public delegate void UnitSystemDeletedEventHandler(string name);
   public delegate void UnitSystemChangedEventHandler(UnitSystem unitSystem);

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class UnitSystemCatalog
   {
      public event UnitSystemAddedEventHandler UnitSystemAdded;
      public event UnitSystemDeletedEventHandler UnitSystemDeleted;
      public event UnitSystemChangedEventHandler UnitSystemChanged;

		private ArrayList unitSystemList;
      public UnitSystemCatalog() {
         unitSystemList = new ArrayList();
         InitializeCatalog();
      }
      
      private void InitializeCatalog() {
         //standard SI
         CreateDefaultUnit("SI", TemperatureUnitType.Kelvin, PressureUnitType.NewtonPerSquareMeter, 
            LiquidHeadUnitType.Meter, MassFlowRateUnitType.KgPerSec, VolumeFlowRateUnitType.CubicMeterPerSec, 
            VolumeRateFlowLiquidsUnitType.CubicMeterPerSec, VolumeRateFlowGasesUnitType.CubicMeterPerSec, 
            MoistureContentUnitType.KgPerKg, FractionUnitType.Decimal, SpecificEnergyUnitType.JoulePerKg, 
            SpecificEntropyUnitType.JoulePerKgKelvin, SpecificHeatUnitType.JoulePerKgKelvin, 
            EnergyUnitType.Joule, PowerUnitType.Watt, DensityUnitType.KgPerCubicMeter,  
            SpecificVolumeUnitType.CubicMeterPerKg, DynamicViscosityUnitType.PascalSecond, 
            KinematicViscosityUnitType.SquareMeterPerSec, ThermalConductivityUnitType.WattPerMeterKelvin,
            HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin,
            FoulingFactorUnitType.SquareMeterKelvinPerWatt, 
            HeatFluxUnitType.WattPerSquareMeter, DiffusivityUnitType.SquareMeterPerSec, 
            SurfaceTensionUnitType.NewtonPerMeter, VelocityUnitType.MeterPerSec, MassUnitType.Kilogram,  
            LengthUnitType.Meter, SmallLengthUnitType.Centimeter, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareMeter, VolumeUnitType.CubicMeter, MassVolumeConcentrationUnitType.KgPerCubicMeter, 
            TimeUnitType.Second, PlaneAngleUnitType.Radian);
         
         //SI-1
         CreateDefaultUnit("SI-1", TemperatureUnitType.Kelvin, PressureUnitType.Kilopascal, 
            LiquidHeadUnitType.Meter, MassFlowRateUnitType.KgPerHour, VolumeFlowRateUnitType.CubicMeterPerHour, 
            VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, VolumeRateFlowGasesUnitType.CubicMeterPerHour, 
            MoistureContentUnitType.KgPerKg, FractionUnitType.Decimal, SpecificEnergyUnitType.KilojoulePerKg, 
            SpecificEntropyUnitType.JoulePerKgKelvin, SpecificHeatUnitType.JoulePerKgKelvin, 
            EnergyUnitType.Joule, PowerUnitType.Watt, DensityUnitType.KgPerCubicMeter,  
            SpecificVolumeUnitType.CubicMeterPerKg, DynamicViscosityUnitType.PascalSecond, 
            KinematicViscosityUnitType.SquareMeterPerSec, ThermalConductivityUnitType.WattPerMeterKelvin,
            HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin, VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin, 
            FoulingFactorUnitType.SquareMeterKelvinPerWatt, 
            HeatFluxUnitType.WattPerSquareMeter, DiffusivityUnitType.SquareMeterPerSec, 
            SurfaceTensionUnitType.NewtonPerMeter, VelocityUnitType.MeterPerSec, MassUnitType.Kilogram,  
            LengthUnitType.Meter, SmallLengthUnitType.Centimeter, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareMeter, VolumeUnitType.CubicMeter, MassVolumeConcentrationUnitType.KgPerCubicMeter, 
            TimeUnitType.Second, PlaneAngleUnitType.Radian);
         
         //SI-2
         CreateDefaultUnit("SI-2", TemperatureUnitType.Celsius, PressureUnitType.Kilopascal, 
            LiquidHeadUnitType.Meter, MassFlowRateUnitType.KgPerHour, VolumeFlowRateUnitType.CubicMeterPerHour, 
            VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, VolumeRateFlowGasesUnitType.CubicMeterPerHour, 
            MoistureContentUnitType.KgPerKg, FractionUnitType.Decimal, SpecificEnergyUnitType.KilojoulePerKg, 
            SpecificEntropyUnitType.KilojoulePerKgKelvin, SpecificHeatUnitType.KilojoulePerKgCelsius,  
            EnergyUnitType.Kilojoule, PowerUnitType.Kilowatt, DensityUnitType.KgPerCubicMeter, 
            SpecificVolumeUnitType.CubicMeterPerKg, DynamicViscosityUnitType.PascalSecond, 
            KinematicViscosityUnitType.SquareMeterPerSec, ThermalConductivityUnitType.WattPerMeterCelsius,
            HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, 
            FoulingFactorUnitType.SquareMeterCelsiusPerWatt, 
            HeatFluxUnitType.WattPerSquareMeter, DiffusivityUnitType.SquareMeterPerSec,
            SurfaceTensionUnitType.NewtonPerMeter, VelocityUnitType.MeterPerSec, MassUnitType.Kilogram,  
            LengthUnitType.Meter, SmallLengthUnitType.Centimeter, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareMeter, VolumeUnitType.CubicMeter, MassVolumeConcentrationUnitType.KgPerCubicMeter, 
            TimeUnitType.Second, PlaneAngleUnitType.Degree);
         
         //SI-3
         CreateDefaultUnit("SI-3", TemperatureUnitType.Celsius, PressureUnitType.Atmosphere, 
            LiquidHeadUnitType.Meter, MassFlowRateUnitType.KgPerHour, VolumeFlowRateUnitType.CubicMeterPerHour, 
            VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, VolumeRateFlowGasesUnitType.CubicMeterPerHour, 
            MoistureContentUnitType.KgPerKg, FractionUnitType.Decimal, SpecificEnergyUnitType.KilojoulePerKg, 
            SpecificEntropyUnitType.KilojoulePerKgKelvin, SpecificHeatUnitType.KilojoulePerKgCelsius,  
            EnergyUnitType.Kilojoule, PowerUnitType.Kilowatt, DensityUnitType.KgPerCubicMeter, 
            SpecificVolumeUnitType.CubicMeterPerKg, DynamicViscosityUnitType.PascalSecond, 
            KinematicViscosityUnitType.SquareMeterPerSec, ThermalConductivityUnitType.WattPerMeterCelsius,
            HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterCelsius, 
            FoulingFactorUnitType.SquareMeterCelsiusPerWatt, 
            HeatFluxUnitType.WattPerSquareMeter, DiffusivityUnitType.SquareMeterPerSec,
            SurfaceTensionUnitType.NewtonPerMeter, VelocityUnitType.MeterPerSec, MassUnitType.Kilogram,  
            LengthUnitType.Meter, SmallLengthUnitType.Centimeter, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareMeter, VolumeUnitType.CubicMeter, MassVolumeConcentrationUnitType.KgPerCubicMeter, 
            TimeUnitType.Second, PlaneAngleUnitType.Degree);
         
         //SI-4
         /*CreateDefaultUnit("SI-4", TemperatureUnitType.Celsius, PressureUnitType.Kilopascal, 
            LiquidHeadUnitType.Meter, MassFlowRateUnitType.KgPerHour, VolumeFlowRateUnitType.CubicMeterPerHour, 
            VolumeRateFlowLiquidsUnitType.CubicMeterPerHour, VolumeRateFlowGasesUnitType.CubicMeterPerHour, 
            MoistureContentUnitType.KgPerKg, FractionUnitType.Decimal, SpecificEnergyUnitType.JoulePerKg, 
            SpecificEntropyUnitType.JoulePerKgKelvin, SpecificHeatUnitType.JoulePerKgCelsius,  
            EnergyUnitType.Joule, PowerUnitType.Watt, DensityUnitType.KgPerCubicMeter, 
            SpecificVolumeUnitType.CubicMeterPerKg, DynamicViscosityUnitType.PascalSecond, 
            KinematicViscosityUnitType.SquareMeterPerSec, ThermalConductivityUnitType.WattPerMeterCelsius,
            HeatTransferCoefficientUnitType.WattPerSquareMeterCelsius, FoulingFactorUnitType.SquareMeterCelsiusPerWatt, 
            HeatFluxUnitType.WattPerSquareMeter, DiffusivityUnitType.SquareMeterPerSec,
            SurfaceTensionUnitType.NewtonPerMeter, VelocityUnitType.MeterPerSec, MassUnitType.Kilogram,  
            LengthUnitType.Meter, SmallLengthUnitType.Centimeter, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareMeter, VolumeUnitType.CubicMeter, MassVolumeConcentrationUnitType.KgPerCubicMeter, 
            TimeUnitType.Second, PlaneAngleUnitType.Degree);*/
         
         //US Customery Unit
         CreateDefaultUnit("Field", TemperatureUnitType.Fahrenheit, PressureUnitType.IbfPerSquareInch, 
            LiquidHeadUnitType.Foot, MassFlowRateUnitType.PoundPerHour, VolumeFlowRateUnitType.CubicFootPerHour, 
            VolumeRateFlowLiquidsUnitType.GallonUSPerHour, VolumeRateFlowGasesUnitType.CubicFootPerHour, 
            MoistureContentUnitType.IbPerIb, FractionUnitType.Decimal, SpecificEnergyUnitType.BtuPerPound, 
            SpecificEntropyUnitType.BtuPerPoundFahrenheit, SpecificHeatUnitType.BtuPerPoundFahrenheit, 
            EnergyUnitType.Btu, PowerUnitType.Kilowatt, DensityUnitType.PoundPerCubicFoot, 
            SpecificVolumeUnitType.CubicFootPerPound, DynamicViscosityUnitType.PoundPerFootHour, 
            KinematicViscosityUnitType.SquareFootPerHour, ThermalConductivityUnitType.BtuPerFootHourFahrenheit,
            HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit,
            FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, HeatFluxUnitType.BtuPerSecSquareFoot,
            DiffusivityUnitType.SquareFootPerHour, SurfaceTensionUnitType.IbfPerFoot, VelocityUnitType.FootPerSec, 
            MassUnitType.Pound, LengthUnitType.Foot, SmallLengthUnitType.Inch, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareFoot, VolumeUnitType.CubicFoot, MassVolumeConcentrationUnitType.PoundPerKilogallonUS,
            TimeUnitType.Second, PlaneAngleUnitType.Degree);
         
         //English Unit
         CreateDefaultUnit("English", TemperatureUnitType.Fahrenheit, PressureUnitType.IbfPerSquareInch, 
            LiquidHeadUnitType.Foot, MassFlowRateUnitType.PoundPerHour, VolumeFlowRateUnitType.CubicFootPerHour, 
            VolumeRateFlowLiquidsUnitType.GallonUSPerHour, VolumeRateFlowGasesUnitType.CubicFootPerHour, 
            MoistureContentUnitType.IbPerIb, FractionUnitType.Decimal, SpecificEnergyUnitType.BtuPerPound, 
            SpecificEntropyUnitType.BtuPerPoundFahrenheit, SpecificHeatUnitType.BtuPerPoundFahrenheit, 
            EnergyUnitType.Btu, PowerUnitType.Kilowatt, DensityUnitType.PoundPerCubicFoot, 
            SpecificVolumeUnitType.CubicFootPerPound, DynamicViscosityUnitType.PoundPerFootHour, 
            KinematicViscosityUnitType.SquareFootPerHour, ThermalConductivityUnitType.BtuPerFootHourFahrenheit,
            HeatTransferCoefficientUnitType.BtuPerHourSquareFootFahrenheit, VolumeHeatTransferCoefficientUnitType.BtuPerHourCubicFootFahrenheit,
            FoulingFactorUnitType.SquareFootHourFahrenheitPerBtu, HeatFluxUnitType.BtuPerSecSquareFoot,
            DiffusivityUnitType.SquareFootPerHour, SurfaceTensionUnitType.IbfPerFoot, VelocityUnitType.FootPerSec, 
            MassUnitType.Pound, LengthUnitType.Foot, SmallLengthUnitType.Inch, MicroLengthUnitType.Micrometer, 
            AreaUnitType.SquareFoot, VolumeUnitType.CubicFoot, MassVolumeConcentrationUnitType.PoundPerKilogallonUS,
            TimeUnitType.Second, PlaneAngleUnitType.Degree);
      }
      
      private void CreateDefaultUnit(string name, TemperatureUnitType temperature, PressureUnitType pressure, 
            LiquidHeadUnitType liquidHead, MassFlowRateUnitType massFlowRate, VolumeFlowRateUnitType volumeFlowRate, 
            VolumeRateFlowLiquidsUnitType volumeRateFlowLiquids, VolumeRateFlowGasesUnitType volumeRateFlowGases, 
            MoistureContentUnitType moistureContent, FractionUnitType fraction, SpecificEnergyUnitType specificEnergy, 
            SpecificEntropyUnitType specificEntropy, SpecificHeatUnitType specificHeat, 
            EnergyUnitType energy, PowerUnitType power, DensityUnitType density,  
            SpecificVolumeUnitType specificVolume, DynamicViscosityUnitType dynamicViscosity, 
            KinematicViscosityUnitType kinematicViscosity, ThermalConductivityUnitType thermalConductivity,
            HeatTransferCoefficientUnitType heatTransferCoefficient, VolumeHeatTransferCoefficientUnitType volumeHeatTransferCoefficient, 
            FoulingFactorUnitType foulingFactor, 
            HeatFluxUnitType heatFluxDensity, DiffusivityUnitType diffusivity, 
            SurfaceTensionUnitType surfaceTension, VelocityUnitType velocity, MassUnitType mass,  
            LengthUnitType length, SmallLengthUnitType smallLength, MicroLengthUnitType microLength, 
            AreaUnitType area, VolumeUnitType volume, MassVolumeConcentrationUnitType massVolumeConcentration, 
            TimeUnitType time, PlaneAngleUnitType planeAngle) {

         UnitSystem us = new UnitSystem(name, true, temperature, pressure, liquidHead, massFlowRate,
            volumeFlowRate, volumeRateFlowLiquids, volumeRateFlowGases, moistureContent,
            fraction, specificEnergy, specificEntropy, specificHeat, energy, power,
            density, specificVolume, dynamicViscosity, kinematicViscosity, thermalConductivity,
            heatTransferCoefficient, volumeHeatTransferCoefficient, foulingFactor, heatFluxDensity,
            diffusivity, surfaceTension, velocity, mass, length, smallLength, microLength, area, volume,
            massVolumeConcentration, time, planeAngle);
         
         unitSystemList.Add(us);
         if (!us.IsReadOnly) {
            us.UnitSystemChanged += new UnitSystemChangedEventHandler(unitSystem_UnitSystemChanged);
         }
      }

      public UnitSystemCatalog(ArrayList list) {
		   unitSystemList = list;
      }

      public void Add(UnitSystem unitSystem) {
         if (!IsInCatalog(unitSystem)) { 
            unitSystemList.Add(unitSystem);
            OnUnitSystemAdded(unitSystem);
            if (!unitSystem.IsReadOnly)
            {
               unitSystem.UnitSystemChanged += new UnitSystemChangedEventHandler(unitSystem_UnitSystemChanged);
            }
         }
      }
      
      public void Remove(string name) {
         foreach (UnitSystem unitSystem in unitSystemList) {
            if (unitSystem.Name.Equals(name) && !unitSystem.IsReadOnly) {
               unitSystem.UnitSystemChanged -= new UnitSystemChangedEventHandler(unitSystem_UnitSystemChanged);
               unitSystemList.Remove(unitSystem);
               OnUnitSystemDeleted(name);
            }
         }
      }
      
      public bool IsInCatalog(UnitSystem unitSystem) {
         bool isInCatalog = false;
         foreach (UnitSystem us in unitSystemList) {
            if (us.Name.Equals(unitSystem.Name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }
      
      public void Remove(UnitSystem unitSystem) {
         if (!unitSystem.IsReadOnly) {
            string name = unitSystem.Name;
            unitSystem.UnitSystemChanged -= new UnitSystemChangedEventHandler(unitSystem_UnitSystemChanged);
            unitSystemList.Remove(unitSystem);
            OnUnitSystemDeleted(name);
         }
      }
      
      public void Remove(int index) {
         if (index < unitSystemList.Count && index >= 0) {
            UnitSystem unitSystem = (UnitSystem) unitSystemList[index];
            if (!unitSystem.IsReadOnly) {
               string name = unitSystem.Name;
               unitSystem.UnitSystemChanged -= new UnitSystemChangedEventHandler(unitSystem_UnitSystemChanged);
               unitSystemList.RemoveAt(index);
               OnUnitSystemDeleted(name);
            }
         }
      }

      public UnitSystem Get(string name) {
         UnitSystem ret = null;
         foreach (UnitSystem unitSystem in unitSystemList) {
            if (unitSystem.Name.Equals(name)) {
               ret = unitSystem;
               break;
            }
         }
         return ret;
      }

      public ArrayList GetList()
      {
         return this.unitSystemList;
      }

      private void OnUnitSystemAdded(UnitSystem unitSystem)
      {
         if (UnitSystemAdded != null)
            UnitSystemAdded(unitSystem);
      }

      private void OnUnitSystemDeleted(string name)
      {
         if (UnitSystemDeleted != null)
            UnitSystemDeleted(name);
      }

      private void OnUnitSystemChanged(UnitSystem unitSystem)
      {
         if (UnitSystemChanged != null)
            UnitSystemChanged(unitSystem);
      }

      private void unitSystem_UnitSystemChanged(UnitSystem unitSystem)
      {
         this.OnUnitSystemChanged(unitSystem);
      }
   }
}
