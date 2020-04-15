using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Drying.UnitSystems;

namespace Drying
{
	/// <summary>
	/// Summary description for UnitSystemEditDialog.
	/// </summary>
	public class UnitSystemEditDialog : System.Windows.Forms.Form
	{
      private UnitSystem unitSystem;

      private System.Windows.Forms.Label labelTemperature;
      private System.Windows.Forms.ComboBox comboBoxTemperature;
      private System.Windows.Forms.Label labelPressure;
      private System.Windows.Forms.ComboBox comboBoxPressure;
      private System.Windows.Forms.Label labelMassFlowRate;
      private System.Windows.Forms.ComboBox comboBoxMassFlowRate;
      private System.Windows.Forms.Label labelVolumeFlowRate;
      private System.Windows.Forms.ComboBox comboBoxVolumeFlowRate;
      private System.Windows.Forms.Label labelMoistureContent;
      private System.Windows.Forms.ComboBox comboBoxMoistureContent;
      private System.Windows.Forms.Label labelSpecificEnergy;
      private System.Windows.Forms.ComboBox comboBoxSpecificEnergy;
      private System.Windows.Forms.Label labelSpecificHeat;
      private System.Windows.Forms.ComboBox comboBoxSpecificHeat;
      private System.Windows.Forms.Label labelEnergy;
      private System.Windows.Forms.ComboBox comboBoxEnergy;
      private System.Windows.Forms.Label label1Power;
      private System.Windows.Forms.Label labelDensity;
      private System.Windows.Forms.ComboBox comboBoxDensity;
      private System.Windows.Forms.Label labelDynamicViscosity;
      private System.Windows.Forms.ComboBox comboBoxDynamicViscosity;
      private System.Windows.Forms.Label labelKinematicViscosity;
      private System.Windows.Forms.ComboBox comboBoxKinematicViscosity;
      private System.Windows.Forms.Label labelThermalConductivity;
      private System.Windows.Forms.ComboBox comboBoxThermalConductivity;
      private System.Windows.Forms.Label labelDiffusivity;
      private System.Windows.Forms.ComboBox comboBoxDiffusivity;
      private System.Windows.Forms.Label labelMass;
      private System.Windows.Forms.ComboBox comboBoxMass;
      private System.Windows.Forms.Label labelLength;
      private System.Windows.Forms.ComboBox comboBoxLength;
      private System.Windows.Forms.Label labelArea;
      private System.Windows.Forms.ComboBox comboBoxArea;
      private System.Windows.Forms.Label labelVolume;
      private System.Windows.Forms.ComboBox comboBoxVolume;
      private System.Windows.Forms.Label labelTime;
      private System.Windows.Forms.ComboBox comboBoxTime;
      private System.Windows.Forms.Button buttonClose;
      private System.Windows.Forms.ComboBox comboBoxPower;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.Label labelFraction;
      private System.Windows.Forms.ComboBox comboBoxFraction;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public UnitSystemEditDialog(UnitSystem unitSystem)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.unitSystem = unitSystem;
         this.textBoxName.Text = unitSystem.Name;

         this.comboBoxTemperature.Items.Add(TemperatureConverter.GetUnitAsString(TemperatureUnit.Celsius));
         this.comboBoxTemperature.Items.Add(TemperatureConverter.GetUnitAsString(TemperatureUnit.Fahrenheit));
         this.comboBoxTemperature.Items.Add(TemperatureConverter.GetUnitAsString(TemperatureUnit.Kelvin));
         this.comboBoxTemperature.Items.Add(TemperatureConverter.GetUnitAsString(TemperatureUnit.Rankine));
         this.comboBoxTemperature.Items.Add(TemperatureConverter.GetUnitAsString(TemperatureUnit.Reaumur));
         this.comboBoxTemperature.SelectedItem = TemperatureConverter.GetUnitAsString(this.unitSystem.TemperatureUnit);

         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Atmosphere));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Bar));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.FootOfWater));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.IbfPerSquareFoot));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.IbfPerSquareInch));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.InchOfMercury));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.InchOfWater));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.KgfPerSquareCentimeter));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.KilonewtonPerSquareMeter));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Kilopascal));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Megapascal));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Millibar));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.MillimeterOfMercury));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.MillimeterOfWater));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.NewtonPerSquareCentimeter));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.NewtonPerSquareMeter));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.Pascal));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.TonfUKPerSquareFoot));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.TonfUSPerSquareFoot));
         this.comboBoxPressure.Items.Add(PressureConverter.GetUnitAsString(PressureUnit.TonnefPerSquareMeter));
         this.comboBoxPressure.SelectedItem = PressureConverter.GetUnitAsString(this.unitSystem.PressureUnit);

         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.GramPerMin));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.GramPerSec));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.KgPerDay));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.KgPerHour));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.KgPerMin));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.KgPerSec));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.OuncePerMin));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.OuncePerSec));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.PoundPerHour));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.PoundPerMin));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.PoundPerSec));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonnePerDay));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonnePerHour));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonUKPerDay));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonUKPerHour));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonUSPerDay));
         this.comboBoxMassFlowRate.Items.Add(MassFlowRateConverter.GetUnitAsString(MassFlowRateUnit.TonUSPerHour));
         this.comboBoxMassFlowRate.SelectedItem = MassFlowRateConverter.GetUnitAsString(this.unitSystem.MassFlowRateUnit);

         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.BarrelPerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.BarrelPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.BarrelPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicFootPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicFootPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicFootPerSec));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicInchPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicInchPerSec));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicMeterPerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicMeterPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicMeterPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicMeterPerSec));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicYardPerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.CubicYardPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUKPerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUKPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUKPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUSPerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUSPerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.GallonUSPerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.LitrePerDay));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.LitrePerHour));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.LitrePerMin));
         this.comboBoxVolumeFlowRate.Items.Add(VolumeFlowRateConverter.GetUnitAsString(VolumeFlowRateUnit.LitrePerSec));
         this.comboBoxVolumeFlowRate.SelectedItem = VolumeFlowRateConverter.GetUnitAsString(this.unitSystem.VolumeFlowRateUnit);

         this.comboBoxMoistureContent.Items.Add(MoistureContentConverter.GetUnitAsString(MoistureContentUnit.IbPerIb));
         this.comboBoxMoistureContent.Items.Add(MoistureContentConverter.GetUnitAsString(MoistureContentUnit.KgPerKg));
         this.comboBoxMoistureContent.SelectedItem = MoistureContentConverter.GetUnitAsString(this.unitSystem.MoistureContentUnit);

         this.comboBoxFraction.Items.Add(FractionConverter.GetUnitAsString(FractionUnit.Decimal));
         this.comboBoxFraction.Items.Add(FractionConverter.GetUnitAsString(FractionUnit.Percent));
         this.comboBoxFraction.SelectedItem = FractionConverter.GetUnitAsString(this.unitSystem.FractionUnit);

         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.BtuPerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.BtuPerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.CaloryPerGram));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.ErgPerGram));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.FootIbfPerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.HorsePowerHourPerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.JoulePerGram));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.JoulePerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KgfMeterPerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilocaloryPerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilocaloryPerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilojoulePerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilojoulePerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilowattHourPerKg));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.KilowattHourPerPound));
         this.comboBoxSpecificEnergy.Items.Add(SpecificEnergyConverter.GetUnitAsString(SpecificEnergyUnit.ThermPerPound));
         this.comboBoxSpecificEnergy.SelectedItem = SpecificEnergyConverter.GetUnitAsString(this.unitSystem.SpecificEnergyUnit);

         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.BtuPerPoundCelcius));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.BtuPerPoundFahrenheit));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.CaloryPerGramKelvin));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.FootIbfPerPoundCelsius));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.FootIbfPerPoundFahrenheit));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.JoulePerGramKelvin));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.JoulePerKgKelvin));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.JoulePerPoundCelsius));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.KgfMeterPerKgCelsius));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.KilocaloryPerKgKelvin));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.KilojoulePerKgKelvin));
         this.comboBoxSpecificHeat.Items.Add(SpecificHeatConverter.GetUnitAsString(SpecificHeatUnit.MegajoulePerKgKelvin));
         this.comboBoxSpecificHeat.SelectedItem = SpecificHeatConverter.GetUnitAsString(this.unitSystem.SpecificHeatUnit);

         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Btu));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Calory));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Erg));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.FootIbf));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.FootPundal));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Gigajoule));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.HorsePowerHour));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Joule));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.KgfMeter));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.KiloCalory));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Kilojoule));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.KilowattHour));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Megajoule));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.MegawattHour));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.Therm));
         this.comboBoxEnergy.Items.Add(EnergyConverter.GetUnitAsString(EnergyUnit.WattHour));
         this.comboBoxEnergy.SelectedItem = EnergyConverter.GetUnitAsString(this.unitSystem.EnergyUnit);

         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.BtuPerHour));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.BtuPerMin));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.BtuPerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.CaloryPerMin));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.CaloryPerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.FootIbfPerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Gigawatt));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.HorsepowerElectric));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.HorsepowerMectric));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.JoulePerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.KgfMeterPerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.KilocaloryPerHour));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.KilocaloryPerMin));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.KilocaloryPerSec));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.KiloJoulePerMin));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Kilowatt));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.MegaJoulePerHour));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Megawatt));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Milliwatt));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Terawatt));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.ThermPerHour));
         this.comboBoxPower.Items.Add(PowerConverter.GetUnitAsString(PowerUnit.Watt));
         this.comboBoxPower.SelectedItem = PowerConverter.GetUnitAsString(this.unitSystem.PowerUnit);

         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.GramPerLitre));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.GramPerMillilitre));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.KgPerCubicMeter));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.KgPerLitre));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.OuncePerCubicInch));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.OuncePerGallonUK));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.OuncePerGallonUS));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.PoundPerCubicFoot));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.PoundPerCubicInch));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.PoundPerCubiCubicYard));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.PoundPerGallonUK));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.PoundPerGallonUS));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.TonnePerCubicMeter));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.TonUKPerCubicYard));
         this.comboBoxDensity.Items.Add(DensityConverter.GetUnitAsString(DensityUnit.TonUSPerCubicYard));
         this.comboBoxDensity.SelectedItem = DensityConverter.GetUnitAsString(this.unitSystem.DensityUnit);

         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.Centipoise));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.DynSecPerSquareCentimeter));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.GramfSecPerSquareCentimeter));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.IbfHourPerSquareFoot));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.IbfSecPerSquareFoot));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.IbfSecPerSquareInch));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.KgfSecPerSquareMeter));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.KgPerMeterHour));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.PascalSecond));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.Poise));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.Poiseuille));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.PoundalSecPerSquareFoot));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.PoundPerFootHour));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.PoundPerFootSec));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.SlugHourPerFootSquareSec));
         this.comboBoxDynamicViscosity.Items.Add(DynamicViscosityConverter.GetUnitAsString(DynamicViscosityUnit.SlugPerFootSec));
         this.comboBoxDynamicViscosity.SelectedItem = DynamicViscosityConverter.GetUnitAsString(this.unitSystem.DynamicViscosityUnit);

         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.Centistoke));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.PoiseCubicFootPerPound));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareCentimeterPerHour));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareCentimeterPerMin));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareFootPerHour));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareFootPerMin));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareFootPerSec));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareInchPerHour));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareInchPerMin));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareInchPerSec));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareMeterPerHour));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareMeterPerMin));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.SquareMeterPerSec));
         this.comboBoxKinematicViscosity.Items.Add(KinematicViscosityConverter.GetUnitAsString(KinematicViscosityUnit.Stoke));
         this.comboBoxKinematicViscosity.SelectedItem = KinematicViscosityConverter.GetUnitAsString(this.unitSystem.KinematicViscosityUnit);

         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.BtuInchPerSqrtFootHourFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.BtuPerFootHourFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.BtuPerFootSecFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.BtuPerInchHourFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.CaloryPerCentimeterSecKelvin));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.JoulePerCentimeterSecKelvin));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.KilocaloryPerCentimeterMinKelvin));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.KilocaloryPerMeterHourKelvin));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.WattPerCentimeterKelvin));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.WattPerFootFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.WattPerInchFahrenheit));
         this.comboBoxThermalConductivity.Items.Add(ThermalConductivityConverter.GetUnitAsString(ThermalConductivityUnit.WattPerMeterKelvin));
         this.comboBoxThermalConductivity.SelectedItem = ThermalConductivityConverter.GetUnitAsString(this.unitSystem.ThermalConductivityUnit);

         this.comboBoxDiffusivity.Items.Add(DiffusivityConverter.GetUnitAsString(DiffusivityUnit.SqMeterPerHour));
         this.comboBoxDiffusivity.Items.Add(DiffusivityConverter.GetUnitAsString(DiffusivityUnit.SqrtCetimeterPerSec));
         this.comboBoxDiffusivity.Items.Add(DiffusivityConverter.GetUnitAsString(DiffusivityUnit.SquareFootPerHour));
         this.comboBoxDiffusivity.Items.Add(DiffusivityConverter.GetUnitAsString(DiffusivityUnit.SquareInchPerSec));
         this.comboBoxDiffusivity.Items.Add(DiffusivityConverter.GetUnitAsString(DiffusivityUnit.SquareMeterPerSec));
         this.comboBoxDiffusivity.SelectedItem = DiffusivityConverter.GetUnitAsString(this.unitSystem.DiffusivityUnit);

         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Carat));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Grain));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Gram));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Hundredweight));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Kilogram));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Microgram));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Milligram));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Ounce));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Pound));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Slug));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Stone));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.Tonne));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.TonUK));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.TonUS));
         this.comboBoxMass.Items.Add(MassConverter.GetUnitAsString(MassUnit.TroyOunce));
         this.comboBoxMass.SelectedItem = MassConverter.GetUnitAsString(this.unitSystem.MassUnit);

         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Centimeter));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Chain));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Decimter));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Fathom));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Foot));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Furlong));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Inch));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Killometer));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Meter));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Micrometer));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Mile));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Millimeter));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Nauticalmile));
         this.comboBoxLength.Items.Add(LengthConverter.GetUnitAsString(LengthUnit.Yard));
         this.comboBoxLength.SelectedItem = LengthConverter.GetUnitAsString(this.unitSystem.LengthUnit);

         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.Acre));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.Are));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.Barn));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.CircularInch));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.CircularMil));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.Hectare));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareCentimeter));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareFoot));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareInch));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareKillometer));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareMeter));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareMile));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareMillimeter));
         this.comboBoxArea.Items.Add(AreaConverter.GetUnitAsString(AreaUnit.SquareYard));
         this.comboBoxArea.SelectedItem = AreaConverter.GetUnitAsString(this.unitSystem.AreaUnit);

         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.Barrel));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.BushelDryUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.Centilitre));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.CubicFoot));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.CubicInch));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.CubicMeter));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.CubicYard));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.Decilitre));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.FluidOunceUK));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.FluidOunceUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.GallonDryUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.GallonLiquidUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.GallonUK));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.GillUK));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.Litre));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.Millilitre));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.PintDryUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.PintLiquidUS));
         this.comboBoxVolume.Items.Add(VolumeConverter.GetUnitAsString(VolumeUnit.PintUK));
         this.comboBoxVolume.SelectedItem = VolumeConverter.GetUnitAsString(this.unitSystem.VolumeUnit);

         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Day));
         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Hour));
         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Minute));
         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Month));
         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Second));
         this.comboBoxTime.Items.Add(TimeConverter.GetUnitAsString(TimeUnit.Year));
         this.comboBoxTime.SelectedItem = TimeConverter.GetUnitAsString(this.unitSystem.TimeUnit);


      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.labelTemperature = new System.Windows.Forms.Label();
         this.labelPressure = new System.Windows.Forms.Label();
         this.labelMassFlowRate = new System.Windows.Forms.Label();
         this.labelVolumeFlowRate = new System.Windows.Forms.Label();
         this.labelMoistureContent = new System.Windows.Forms.Label();
         this.labelFraction = new System.Windows.Forms.Label();
         this.labelSpecificEnergy = new System.Windows.Forms.Label();
         this.labelSpecificHeat = new System.Windows.Forms.Label();
         this.labelEnergy = new System.Windows.Forms.Label();
         this.label1Power = new System.Windows.Forms.Label();
         this.labelDensity = new System.Windows.Forms.Label();
         this.labelDynamicViscosity = new System.Windows.Forms.Label();
         this.labelKinematicViscosity = new System.Windows.Forms.Label();
         this.labelThermalConductivity = new System.Windows.Forms.Label();
         this.labelDiffusivity = new System.Windows.Forms.Label();
         this.labelMass = new System.Windows.Forms.Label();
         this.labelLength = new System.Windows.Forms.Label();
         this.labelArea = new System.Windows.Forms.Label();
         this.labelVolume = new System.Windows.Forms.Label();
         this.labelTime = new System.Windows.Forms.Label();
         this.comboBoxTemperature = new System.Windows.Forms.ComboBox();
         this.comboBoxPressure = new System.Windows.Forms.ComboBox();
         this.comboBoxMassFlowRate = new System.Windows.Forms.ComboBox();
         this.comboBoxVolumeFlowRate = new System.Windows.Forms.ComboBox();
         this.comboBoxMoistureContent = new System.Windows.Forms.ComboBox();
         this.comboBoxFraction = new System.Windows.Forms.ComboBox();
         this.comboBoxSpecificEnergy = new System.Windows.Forms.ComboBox();
         this.comboBoxSpecificHeat = new System.Windows.Forms.ComboBox();
         this.comboBoxEnergy = new System.Windows.Forms.ComboBox();
         this.comboBoxPower = new System.Windows.Forms.ComboBox();
         this.comboBoxDensity = new System.Windows.Forms.ComboBox();
         this.comboBoxDynamicViscosity = new System.Windows.Forms.ComboBox();
         this.comboBoxKinematicViscosity = new System.Windows.Forms.ComboBox();
         this.comboBoxThermalConductivity = new System.Windows.Forms.ComboBox();
         this.comboBoxDiffusivity = new System.Windows.Forms.ComboBox();
         this.comboBoxMass = new System.Windows.Forms.ComboBox();
         this.comboBoxLength = new System.Windows.Forms.ComboBox();
         this.comboBoxArea = new System.Windows.Forms.ComboBox();
         this.comboBoxVolume = new System.Windows.Forms.ComboBox();
         this.comboBoxTime = new System.Windows.Forms.ComboBox();
         this.buttonClose = new System.Windows.Forms.Button();
         this.labelName = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // labelTemperature
         // 
         this.labelTemperature.Location = new System.Drawing.Point(8, 8);
         this.labelTemperature.Name = "labelTemperature";
         this.labelTemperature.Size = new System.Drawing.Size(120, 24);
         this.labelTemperature.TabIndex = 0;
         this.labelTemperature.Text = "Temperature:";
         // 
         // labelPressure
         // 
         this.labelPressure.Location = new System.Drawing.Point(8, 32);
         this.labelPressure.Name = "labelPressure";
         this.labelPressure.Size = new System.Drawing.Size(120, 24);
         this.labelPressure.TabIndex = 1;
         this.labelPressure.Text = "Pressure:";
         // 
         // labelMassFlowRate
         // 
         this.labelMassFlowRate.Location = new System.Drawing.Point(8, 56);
         this.labelMassFlowRate.Name = "labelMassFlowRate";
         this.labelMassFlowRate.Size = new System.Drawing.Size(120, 24);
         this.labelMassFlowRate.TabIndex = 2;
         this.labelMassFlowRate.Text = "Mass Flow Rate:";
         // 
         // labelVolumeFlowRate
         // 
         this.labelVolumeFlowRate.Location = new System.Drawing.Point(8, 80);
         this.labelVolumeFlowRate.Name = "labelVolumeFlowRate";
         this.labelVolumeFlowRate.Size = new System.Drawing.Size(120, 24);
         this.labelVolumeFlowRate.TabIndex = 3;
         this.labelVolumeFlowRate.Text = "Volume Flow Rate:";
         // 
         // labelMoistureContent
         // 
         this.labelMoistureContent.Location = new System.Drawing.Point(8, 104);
         this.labelMoistureContent.Name = "labelMoistureContent";
         this.labelMoistureContent.Size = new System.Drawing.Size(120, 24);
         this.labelMoistureContent.TabIndex = 4;
         this.labelMoistureContent.Text = "Moisture Content:";
         // 
         // labelFraction
         // 
         this.labelFraction.Location = new System.Drawing.Point(8, 128);
         this.labelFraction.Name = "labelFraction";
         this.labelFraction.Size = new System.Drawing.Size(120, 24);
         this.labelFraction.TabIndex = 5;
         this.labelFraction.Text = "Fraction:";
         // 
         // labelSpecificEnergy
         // 
         this.labelSpecificEnergy.Location = new System.Drawing.Point(8, 152);
         this.labelSpecificEnergy.Name = "labelSpecificEnergy";
         this.labelSpecificEnergy.Size = new System.Drawing.Size(120, 24);
         this.labelSpecificEnergy.TabIndex = 6;
         this.labelSpecificEnergy.Text = "Specific Energy:";
         // 
         // labelSpecificHeat
         // 
         this.labelSpecificHeat.Location = new System.Drawing.Point(8, 176);
         this.labelSpecificHeat.Name = "labelSpecificHeat";
         this.labelSpecificHeat.Size = new System.Drawing.Size(120, 24);
         this.labelSpecificHeat.TabIndex = 7;
         this.labelSpecificHeat.Text = "Specific Heat:";
         // 
         // labelEnergy
         // 
         this.labelEnergy.Location = new System.Drawing.Point(8, 200);
         this.labelEnergy.Name = "labelEnergy";
         this.labelEnergy.Size = new System.Drawing.Size(120, 24);
         this.labelEnergy.TabIndex = 8;
         this.labelEnergy.Text = "Energy:";
         // 
         // label1Power
         // 
         this.label1Power.Location = new System.Drawing.Point(8, 224);
         this.label1Power.Name = "label1Power";
         this.label1Power.Size = new System.Drawing.Size(120, 24);
         this.label1Power.TabIndex = 9;
         this.label1Power.Text = "Power:";
         // 
         // labelDensity
         // 
         this.labelDensity.Location = new System.Drawing.Point(256, 8);
         this.labelDensity.Name = "labelDensity";
         this.labelDensity.Size = new System.Drawing.Size(120, 24);
         this.labelDensity.TabIndex = 10;
         this.labelDensity.Text = "Density:";
         // 
         // labelDynamicViscosity
         // 
         this.labelDynamicViscosity.Location = new System.Drawing.Point(256, 32);
         this.labelDynamicViscosity.Name = "labelDynamicViscosity";
         this.labelDynamicViscosity.Size = new System.Drawing.Size(120, 24);
         this.labelDynamicViscosity.TabIndex = 11;
         this.labelDynamicViscosity.Text = "Dynamic Viscosity:";
         // 
         // labelKinematicViscosity
         // 
         this.labelKinematicViscosity.Location = new System.Drawing.Point(256, 56);
         this.labelKinematicViscosity.Name = "labelKinematicViscosity";
         this.labelKinematicViscosity.Size = new System.Drawing.Size(120, 24);
         this.labelKinematicViscosity.TabIndex = 12;
         this.labelKinematicViscosity.Text = "Kinematic Viscosity:";
         // 
         // labelThermalConductivity
         // 
         this.labelThermalConductivity.Location = new System.Drawing.Point(256, 80);
         this.labelThermalConductivity.Name = "labelThermalConductivity";
         this.labelThermalConductivity.Size = new System.Drawing.Size(120, 24);
         this.labelThermalConductivity.TabIndex = 13;
         this.labelThermalConductivity.Text = "Thermal Conductivity:";
         // 
         // labelDiffusivity
         // 
         this.labelDiffusivity.Location = new System.Drawing.Point(256, 104);
         this.labelDiffusivity.Name = "labelDiffusivity";
         this.labelDiffusivity.Size = new System.Drawing.Size(120, 24);
         this.labelDiffusivity.TabIndex = 14;
         this.labelDiffusivity.Text = "Diffusivity:";
         // 
         // labelMass
         // 
         this.labelMass.Location = new System.Drawing.Point(256, 128);
         this.labelMass.Name = "labelMass";
         this.labelMass.Size = new System.Drawing.Size(120, 24);
         this.labelMass.TabIndex = 15;
         this.labelMass.Text = "Mass:";
         // 
         // labelLength
         // 
         this.labelLength.Location = new System.Drawing.Point(256, 152);
         this.labelLength.Name = "labelLength";
         this.labelLength.Size = new System.Drawing.Size(120, 24);
         this.labelLength.TabIndex = 16;
         this.labelLength.Text = "Length:";
         // 
         // labelArea
         // 
         this.labelArea.Location = new System.Drawing.Point(256, 176);
         this.labelArea.Name = "labelArea";
         this.labelArea.Size = new System.Drawing.Size(120, 24);
         this.labelArea.TabIndex = 17;
         this.labelArea.Text = "Area:";
         // 
         // labelVolume
         // 
         this.labelVolume.Location = new System.Drawing.Point(256, 200);
         this.labelVolume.Name = "labelVolume";
         this.labelVolume.Size = new System.Drawing.Size(120, 24);
         this.labelVolume.TabIndex = 18;
         this.labelVolume.Text = "Volume:";
         // 
         // labelTime
         // 
         this.labelTime.Location = new System.Drawing.Point(256, 224);
         this.labelTime.Name = "labelTime";
         this.labelTime.Size = new System.Drawing.Size(120, 24);
         this.labelTime.TabIndex = 19;
         this.labelTime.Text = "Time:";
         // 
         // comboBoxTemperature
         // 
         this.comboBoxTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTemperature.Location = new System.Drawing.Point(128, 8);
         this.comboBoxTemperature.Name = "comboBoxTemperature";
         this.comboBoxTemperature.Size = new System.Drawing.Size(112, 21);
         this.comboBoxTemperature.TabIndex = 20;
         this.comboBoxTemperature.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemperature_SelectedIndexChanged);
         // 
         // comboBoxPressure
         // 
         this.comboBoxPressure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPressure.Location = new System.Drawing.Point(128, 32);
         this.comboBoxPressure.Name = "comboBoxPressure";
         this.comboBoxPressure.Size = new System.Drawing.Size(112, 21);
         this.comboBoxPressure.TabIndex = 21;
         this.comboBoxPressure.SelectedIndexChanged += new System.EventHandler(this.comboBoxPressure_SelectedIndexChanged);
         // 
         // comboBoxMassFlowRate
         // 
         this.comboBoxMassFlowRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMassFlowRate.Location = new System.Drawing.Point(128, 56);
         this.comboBoxMassFlowRate.Name = "comboBoxMassFlowRate";
         this.comboBoxMassFlowRate.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMassFlowRate.TabIndex = 22;
         this.comboBoxMassFlowRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxMassFlowRate_SelectedIndexChanged);
         // 
         // comboBoxVolumeFlowRate
         // 
         this.comboBoxVolumeFlowRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolumeFlowRate.Location = new System.Drawing.Point(128, 80);
         this.comboBoxVolumeFlowRate.Name = "comboBoxVolumeFlowRate";
         this.comboBoxVolumeFlowRate.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolumeFlowRate.TabIndex = 23;
         this.comboBoxVolumeFlowRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolumeFlowRate_SelectedIndexChanged);
         // 
         // comboBoxMoistureContent
         // 
         this.comboBoxMoistureContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMoistureContent.Location = new System.Drawing.Point(128, 104);
         this.comboBoxMoistureContent.Name = "comboBoxMoistureContent";
         this.comboBoxMoistureContent.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMoistureContent.TabIndex = 24;
         this.comboBoxMoistureContent.SelectedIndexChanged += new System.EventHandler(this.comboBoxMoistureContent_SelectedIndexChanged);
         // 
         // comboBoxFraction
         // 
         this.comboBoxFraction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFraction.Location = new System.Drawing.Point(128, 128);
         this.comboBoxFraction.Name = "comboBoxFraction";
         this.comboBoxFraction.Size = new System.Drawing.Size(112, 21);
         this.comboBoxFraction.TabIndex = 25;
         this.comboBoxFraction.SelectedIndexChanged += new System.EventHandler(this.comboBoxFraction_SelectedIndexChanged);
         // 
         // comboBoxSpecificEnergy
         // 
         this.comboBoxSpecificEnergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSpecificEnergy.Location = new System.Drawing.Point(128, 152);
         this.comboBoxSpecificEnergy.Name = "comboBoxSpecificEnergy";
         this.comboBoxSpecificEnergy.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSpecificEnergy.TabIndex = 26;
         this.comboBoxSpecificEnergy.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecificEnergy_SelectedIndexChanged);
         // 
         // comboBoxSpecificHeat
         // 
         this.comboBoxSpecificHeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSpecificHeat.Location = new System.Drawing.Point(128, 176);
         this.comboBoxSpecificHeat.Name = "comboBoxSpecificHeat";
         this.comboBoxSpecificHeat.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSpecificHeat.TabIndex = 27;
         this.comboBoxSpecificHeat.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecificHeat_SelectedIndexChanged);
         // 
         // comboBoxEnergy
         // 
         this.comboBoxEnergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxEnergy.Location = new System.Drawing.Point(128, 200);
         this.comboBoxEnergy.Name = "comboBoxEnergy";
         this.comboBoxEnergy.Size = new System.Drawing.Size(112, 21);
         this.comboBoxEnergy.TabIndex = 28;
         this.comboBoxEnergy.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnergy_SelectedIndexChanged);
         // 
         // comboBoxPower
         // 
         this.comboBoxPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPower.Location = new System.Drawing.Point(128, 224);
         this.comboBoxPower.Name = "comboBoxPower";
         this.comboBoxPower.Size = new System.Drawing.Size(112, 21);
         this.comboBoxPower.TabIndex = 29;
         this.comboBoxPower.SelectedIndexChanged += new System.EventHandler(this.comboBoxPower_SelectedIndexChanged);
         // 
         // comboBoxDensity
         // 
         this.comboBoxDensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDensity.Location = new System.Drawing.Point(376, 8);
         this.comboBoxDensity.Name = "comboBoxDensity";
         this.comboBoxDensity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDensity.TabIndex = 30;
         this.comboBoxDensity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDensity_SelectedIndexChanged);
         // 
         // comboBoxDynamicViscosity
         // 
         this.comboBoxDynamicViscosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDynamicViscosity.Location = new System.Drawing.Point(376, 32);
         this.comboBoxDynamicViscosity.Name = "comboBoxDynamicViscosity";
         this.comboBoxDynamicViscosity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDynamicViscosity.TabIndex = 31;
         this.comboBoxDynamicViscosity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDynamicViscosity_SelectedIndexChanged);
         // 
         // comboBoxKinematicViscosity
         // 
         this.comboBoxKinematicViscosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxKinematicViscosity.Location = new System.Drawing.Point(376, 56);
         this.comboBoxKinematicViscosity.Name = "comboBoxKinematicViscosity";
         this.comboBoxKinematicViscosity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxKinematicViscosity.TabIndex = 32;
         this.comboBoxKinematicViscosity.SelectedIndexChanged += new System.EventHandler(this.comboBoxKinematicViscosity_SelectedIndexChanged);
         // 
         // comboBoxThermalConductivity
         // 
         this.comboBoxThermalConductivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxThermalConductivity.Location = new System.Drawing.Point(376, 80);
         this.comboBoxThermalConductivity.Name = "comboBoxThermalConductivity";
         this.comboBoxThermalConductivity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxThermalConductivity.TabIndex = 33;
         this.comboBoxThermalConductivity.SelectedIndexChanged += new System.EventHandler(this.comboBoxThermalConductivity_SelectedIndexChanged);
         // 
         // comboBoxDiffusivity
         // 
         this.comboBoxDiffusivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDiffusivity.Location = new System.Drawing.Point(376, 104);
         this.comboBoxDiffusivity.Name = "comboBoxDiffusivity";
         this.comboBoxDiffusivity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDiffusivity.TabIndex = 34;
         this.comboBoxDiffusivity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiffusivity_SelectedIndexChanged);
         // 
         // comboBoxMass
         // 
         this.comboBoxMass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMass.Location = new System.Drawing.Point(376, 128);
         this.comboBoxMass.Name = "comboBoxMass";
         this.comboBoxMass.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMass.TabIndex = 35;
         this.comboBoxMass.SelectedIndexChanged += new System.EventHandler(this.comboBoxMass_SelectedIndexChanged);
         // 
         // comboBoxLength
         // 
         this.comboBoxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxLength.Location = new System.Drawing.Point(376, 152);
         this.comboBoxLength.Name = "comboBoxLength";
         this.comboBoxLength.Size = new System.Drawing.Size(112, 21);
         this.comboBoxLength.TabIndex = 36;
         this.comboBoxLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxLength_SelectedIndexChanged);
         // 
         // comboBoxArea
         // 
         this.comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxArea.Location = new System.Drawing.Point(376, 176);
         this.comboBoxArea.Name = "comboBoxArea";
         this.comboBoxArea.Size = new System.Drawing.Size(112, 21);
         this.comboBoxArea.TabIndex = 37;
         this.comboBoxArea.SelectedIndexChanged += new System.EventHandler(this.comboBoxArea_SelectedIndexChanged);
         // 
         // comboBoxVolume
         // 
         this.comboBoxVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolume.Location = new System.Drawing.Point(376, 200);
         this.comboBoxVolume.Name = "comboBoxVolume";
         this.comboBoxVolume.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolume.TabIndex = 38;
         this.comboBoxVolume.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolume_SelectedIndexChanged);
         // 
         // comboBoxTime
         // 
         this.comboBoxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTime.Location = new System.Drawing.Point(376, 224);
         this.comboBoxTime.Name = "comboBoxTime";
         this.comboBoxTime.Size = new System.Drawing.Size(112, 21);
         this.comboBoxTime.TabIndex = 39;
         this.comboBoxTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxTime_SelectedIndexChanged);
         // 
         // buttonClose
         // 
         this.buttonClose.Location = new System.Drawing.Point(376, 264);
         this.buttonClose.Name = "buttonClose";
         this.buttonClose.TabIndex = 40;
         this.buttonClose.Text = "Close";
         this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
         // 
         // labelName
         // 
         this.labelName.Location = new System.Drawing.Point(8, 264);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(48, 23);
         this.labelName.TabIndex = 41;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(64, 264);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(176, 20);
         this.textBoxName.TabIndex = 42;
         this.textBoxName.Text = "";
         this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
         this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
         // 
         // UnitSystemEditDialog
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(498, 295);
         this.Controls.Add(this.textBoxName);
         this.Controls.Add(this.labelName);
         this.Controls.Add(this.buttonClose);
         this.Controls.Add(this.comboBoxTime);
         this.Controls.Add(this.comboBoxVolume);
         this.Controls.Add(this.comboBoxArea);
         this.Controls.Add(this.comboBoxLength);
         this.Controls.Add(this.comboBoxMass);
         this.Controls.Add(this.comboBoxDiffusivity);
         this.Controls.Add(this.comboBoxThermalConductivity);
         this.Controls.Add(this.comboBoxKinematicViscosity);
         this.Controls.Add(this.comboBoxDynamicViscosity);
         this.Controls.Add(this.comboBoxDensity);
         this.Controls.Add(this.comboBoxPower);
         this.Controls.Add(this.comboBoxEnergy);
         this.Controls.Add(this.comboBoxSpecificHeat);
         this.Controls.Add(this.comboBoxSpecificEnergy);
         this.Controls.Add(this.comboBoxFraction);
         this.Controls.Add(this.comboBoxMoistureContent);
         this.Controls.Add(this.comboBoxVolumeFlowRate);
         this.Controls.Add(this.comboBoxMassFlowRate);
         this.Controls.Add(this.comboBoxPressure);
         this.Controls.Add(this.comboBoxTemperature);
         this.Controls.Add(this.labelTime);
         this.Controls.Add(this.labelVolume);
         this.Controls.Add(this.labelArea);
         this.Controls.Add(this.labelLength);
         this.Controls.Add(this.labelMass);
         this.Controls.Add(this.labelDiffusivity);
         this.Controls.Add(this.labelThermalConductivity);
         this.Controls.Add(this.labelKinematicViscosity);
         this.Controls.Add(this.labelDynamicViscosity);
         this.Controls.Add(this.labelDensity);
         this.Controls.Add(this.label1Power);
         this.Controls.Add(this.labelEnergy);
         this.Controls.Add(this.labelSpecificHeat);
         this.Controls.Add(this.labelSpecificEnergy);
         this.Controls.Add(this.labelFraction);
         this.Controls.Add(this.labelMoistureContent);
         this.Controls.Add(this.labelVolumeFlowRate);
         this.Controls.Add(this.labelMassFlowRate);
         this.Controls.Add(this.labelPressure);
         this.Controls.Add(this.labelTemperature);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "UnitSystemEditDialog";
         this.Text = "Edit Unit System";
         this.ResumeLayout(false);

      }
		#endregion

      public UnitSystem GetUnitSystem()
      {
         return this.unitSystem;
      }

      private void buttonClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void textBoxName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         if (tb.Text != null)
         {
            if (tb.Text.Trim().Equals(""))
            {
               if (sender == this.textBoxName)
               {
                  e.Cancel = true;
                  string message3 = "Please specify a name!";
                  MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            else
            {
               if (sender == this.textBoxName)
               {
                  this.unitSystem.Name = this.textBoxName.Text;
               }
            }
         }
      }

      private void textBoxName_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            if (sender == this.textBoxName)
            {
               this.ActiveControl = null;
            }
         }
      }

      private void comboBoxTemperature_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.TemperatureUnit = TemperatureConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxPressure_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.PressureUnit = PressureConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxMassFlowRate_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.MassFlowRateUnit = MassFlowRateConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxVolumeFlowRate_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.VolumeFlowRateUnit = VolumeFlowRateConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxMoistureContent_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.MoistureContentUnit = MoistureContentConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxFraction_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.FractionUnit = FractionConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxSpecificEnergy_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.SpecificEnergyUnit = SpecificEnergyConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxSpecificHeat_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.SpecificHeatUnit = SpecificHeatConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxEnergy_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.EnergyUnit = EnergyConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxPower_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.PowerUnit = PowerConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxDensity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.DensityUnit = DensityConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxDynamicViscosity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.DynamicViscosityUnit = DynamicViscosityConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxKinematicViscosity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.KinematicViscosityUnit = KinematicViscosityConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxThermalConductivity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.ThermalConductivityUnit = ThermalConductivityConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxDiffusivity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.DiffusivityUnit = DiffusivityConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxMass_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.MassUnit = MassConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxLength_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.LengthUnit = LengthConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxArea_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.AreaUnit = AreaConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxVolume_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.VolumeUnit = VolumeConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxTime_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystem.TimeUnit = TimeConverter.GetUnitAsEnum((string)cb.SelectedItem);
      }

	}
}
