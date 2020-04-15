using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitSystems;

namespace ProsimoUI.UnitSystemsUI
{
	/// <summary>
	/// Summary description for UnitSystemEditor.
	/// </summary>
	public class UnitSystemEditor : System.Windows.Forms.Form
	{
      private UnitSystem unitSystemCache;
      private UnitSystemsControl unitSystemsCtrl;

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
      private System.Windows.Forms.Label labelPower;
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
      private System.Windows.Forms.ComboBox comboBoxPower;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.Label labelFraction;
      private System.Windows.Forms.ComboBox comboBoxFraction;
      private System.Windows.Forms.GroupBox groupBoxUnits;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Label labelSmallLength;
      private System.Windows.Forms.ComboBox comboBoxSmallLength;
      private System.Windows.Forms.Label labelMicroLength;
      private System.Windows.Forms.ComboBox comboBoxMicroLength;
      private System.Windows.Forms.Label labelLiquidHead;
      private System.Windows.Forms.ComboBox comboBoxLiquidHead;
      private System.Windows.Forms.Label labelVolumeRateFlowLiquids;
      private System.Windows.Forms.ComboBox comboBoxVolumeRateFlowLiquids;
      private System.Windows.Forms.Label labelVelocity;
      private System.Windows.Forms.ComboBox comboBoxVelocity;
      private System.Windows.Forms.Label labelSurfaceTension;
      private System.Windows.Forms.ComboBox comboBoxSurfaceTension;
      private System.Windows.Forms.Label labelHeatTransferCoeff;
      private System.Windows.Forms.ComboBox comboBoxHeatTransferCoeff;
      private System.Windows.Forms.Label labelVolumeRateFlowGases;
      private System.Windows.Forms.ComboBox comboBoxVolumeRateFlowGases;
      private System.Windows.Forms.Label labelFoulingFactor;
      private System.Windows.Forms.ComboBox comboBoxFoulingFactor;
      private System.Windows.Forms.Label labelSpecificVolume;
      private System.Windows.Forms.ComboBox comboBoxSpecificVolume;
      private System.Windows.Forms.Label labelMassVolumeConcentration;
      private System.Windows.Forms.ComboBox comboBoxMassVolumeConcentration;
      private System.Windows.Forms.Label labelPlaneAngle;
      private System.Windows.Forms.ComboBox comboBoxPlaneAngle;
      private System.Windows.Forms.Label labelVolumeHeatTransferCoefficient;
      private System.Windows.Forms.ComboBox comboBoxVolumeHeatTransferCoefficient;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public UnitSystemEditor(UnitSystemsControl unitSystemsCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.unitSystemsCtrl = unitSystemsCtrl;
         UnitSystem unitSystemOriginal = unitSystemsCtrl.GetSelectedUnitSystem();
         this.textBoxName.Text = unitSystemOriginal.Name;
         this.unitSystemCache = unitSystemOriginal.Clone();

         IEnumerator e = TemperatureUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxTemperature.Items.Add(e.Current);
         }
         this.comboBoxTemperature.SelectedItem = TemperatureUnit.Instance.GetUnitAsString(this.unitSystemCache.TemperatureUnitType);

         e = PressureUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxPressure.Items.Add(e.Current);
         }
         this.comboBoxPressure.SelectedItem = PressureUnit.Instance.GetUnitAsString(this.unitSystemCache.PressureUnitType);

         e = MassFlowRateUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxMassFlowRate.Items.Add(e.Current);
         }
         this.comboBoxMassFlowRate.SelectedItem = MassFlowRateUnit.Instance.GetUnitAsString(this.unitSystemCache.MassFlowRateUnitType);

         e = VolumeFlowRateUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVolumeFlowRate.Items.Add(e.Current);
         }
         this.comboBoxVolumeFlowRate.SelectedItem = VolumeFlowRateUnit.Instance.GetUnitAsString(this.unitSystemCache.VolumeFlowRateUnitType);

         e = MoistureContentUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxMoistureContent.Items.Add(e.Current);
         }
         this.comboBoxMoistureContent.SelectedItem = MoistureContentUnit.Instance.GetUnitAsString(this.unitSystemCache.MoistureContentUnitType);

         e = FractionUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxFraction.Items.Add(e.Current);
         }
         this.comboBoxFraction.SelectedItem = FractionUnit.Instance.GetUnitAsString(this.unitSystemCache.FractionUnitType);

         e = SpecificEnergyUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxSpecificEnergy.Items.Add(e.Current);
         }
         this.comboBoxSpecificEnergy.SelectedItem = SpecificEnergyUnit.Instance.GetUnitAsString(this.unitSystemCache.SpecificEnergyUnitType);

         e = SpecificHeatUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxSpecificHeat.Items.Add(e.Current);
         }
         this.comboBoxSpecificHeat.SelectedItem = SpecificHeatUnit.Instance.GetUnitAsString(this.unitSystemCache.SpecificHeatUnitType);

         e = EnergyUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxEnergy.Items.Add(e.Current);
         }
         this.comboBoxEnergy.SelectedItem = EnergyUnit.Instance.GetUnitAsString(this.unitSystemCache.EnergyUnitType);

         e = PowerUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxPower.Items.Add(e.Current);
         }
         this.comboBoxPower.SelectedItem = PowerUnit.Instance.GetUnitAsString(this.unitSystemCache.PowerUnitType);

         e = DensityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxDensity.Items.Add(e.Current);
         }
         this.comboBoxDensity.SelectedItem = DensityUnit.Instance.GetUnitAsString(this.unitSystemCache.DensityUnitType);

         e = DynamicViscosityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxDynamicViscosity.Items.Add(e.Current);
         }
         this.comboBoxDynamicViscosity.SelectedItem = DynamicViscosityUnit.Instance.GetUnitAsString(this.unitSystemCache.DynamicViscosityUnitType);

         e = KinematicViscosityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxKinematicViscosity.Items.Add(e.Current);
         }
         this.comboBoxKinematicViscosity.SelectedItem = KinematicViscosityUnit.Instance.GetUnitAsString(this.unitSystemCache.KinematicViscosityUnitType);

         e = ThermalConductivityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxThermalConductivity.Items.Add(e.Current);
         }
         this.comboBoxThermalConductivity.SelectedItem = ThermalConductivityUnit.Instance.GetUnitAsString(this.unitSystemCache.ThermalConductivityUnitType);

         e = DiffusivityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxDiffusivity.Items.Add(e.Current);
         }
         this.comboBoxDiffusivity.SelectedItem = DiffusivityUnit.Instance.GetUnitAsString(this.unitSystemCache.DiffusivityUnitType);

         e = MassUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxMass.Items.Add(e.Current);
         }
         this.comboBoxMass.SelectedItem = MassUnit.Instance.GetUnitAsString(this.unitSystemCache.MassUnitType);

         e = LengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxLength.Items.Add(e.Current);
         }
         this.comboBoxLength.SelectedItem = LengthUnit.Instance.GetUnitAsString(this.unitSystemCache.LengthUnitType);

         e = AreaUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxArea.Items.Add(e.Current);
         }
         this.comboBoxArea.SelectedItem = AreaUnit.Instance.GetUnitAsString(this.unitSystemCache.AreaUnitType);

         e = VolumeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVolume.Items.Add(e.Current);
         }
         this.comboBoxVolume.SelectedItem = VolumeUnit.Instance.GetUnitAsString(this.unitSystemCache.VolumeUnitType);

         e = TimeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxTime.Items.Add(e.Current);
         }
         this.comboBoxTime.SelectedItem = TimeUnit.Instance.GetUnitAsString(this.unitSystemCache.TimeUnitType);

         e = SmallLengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxSmallLength.Items.Add(e.Current);
         }
         this.comboBoxSmallLength.SelectedItem = SmallLengthUnit.Instance.GetUnitAsString(this.unitSystemCache.SmallLengthUnitType);

         e = MicroLengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxMicroLength.Items.Add(e.Current);
         }
         this.comboBoxMicroLength.SelectedItem = MicroLengthUnit.Instance.GetUnitAsString(this.unitSystemCache.MicroLengthUnitType);

         e = LiquidHeadUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxLiquidHead.Items.Add(e.Current);
         }
         this.comboBoxLiquidHead.SelectedItem = LiquidHeadUnit.Instance.GetUnitAsString(this.unitSystemCache.LiquidHeadUnitType);

         e = VolumeRateFlowLiquidsUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVolumeRateFlowLiquids.Items.Add(e.Current);
         }
         this.comboBoxVolumeRateFlowLiquids.SelectedItem = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsString(this.unitSystemCache.VolumeRateFlowLiquidsUnitType);

         e = VolumeRateFlowGasesUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVolumeRateFlowGases.Items.Add(e.Current);
         }
         this.comboBoxVolumeRateFlowGases.SelectedItem = VolumeRateFlowGasesUnit.Instance.GetUnitAsString(this.unitSystemCache.VolumeRateFlowGasesUnitType);
      
         e = HeatTransferCoefficientUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxHeatTransferCoeff.Items.Add(e.Current);
         }
         this.comboBoxHeatTransferCoeff.SelectedItem = HeatTransferCoefficientUnit.Instance.GetUnitAsString(this.unitSystemCache.HeatTransferCoefficientUnitType);
      
         e = SurfaceTensionUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxSurfaceTension.Items.Add(e.Current);
         }
         this.comboBoxSurfaceTension.SelectedItem = SurfaceTensionUnit.Instance.GetUnitAsString(this.unitSystemCache.SurfaceTensionUnitType);
      
         e = VelocityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVelocity.Items.Add(e.Current);
         }
         this.comboBoxVelocity.SelectedItem = VelocityUnit.Instance.GetUnitAsString(this.unitSystemCache.VelocityUnitType);
      
         e = FoulingFactorUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxFoulingFactor.Items.Add(e.Current);
         }
         this.comboBoxFoulingFactor.SelectedItem = FoulingFactorUnit.Instance.GetUnitAsString(this.unitSystemCache.FoulingFactorUnitType);
      
         e = SpecificVolumeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxSpecificVolume.Items.Add(e.Current);
         }
         this.comboBoxSpecificVolume.SelectedItem = SpecificVolumeUnit.Instance.GetUnitAsString(this.unitSystemCache.SpecificVolumeUnitType);

         e = MassVolumeConcentrationUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxMassVolumeConcentration.Items.Add(e.Current);
         }
         this.comboBoxMassVolumeConcentration.SelectedItem = MassVolumeConcentrationUnit.Instance.GetUnitAsString(this.unitSystemCache.MassVolumeConcentrationUnitType);

         e = PlaneAngleUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxPlaneAngle.Items.Add(e.Current);
         }
         this.comboBoxPlaneAngle.SelectedItem = PlaneAngleUnit.Instance.GetUnitAsString(this.unitSystemCache.PlaneAngleUnitType);

         e = VolumeHeatTransferCoefficientUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            this.comboBoxVolumeHeatTransferCoefficient.Items.Add(e.Current);
         }
         this.comboBoxVolumeHeatTransferCoefficient.SelectedItem = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsString(this.unitSystemCache.VolumeHeatTransferCoefficientUnitType);
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
         this.labelPower = new System.Windows.Forms.Label();
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
         this.buttonOk = new System.Windows.Forms.Button();
         this.labelName = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.groupBoxUnits = new System.Windows.Forms.GroupBox();
         this.labelPlaneAngle = new System.Windows.Forms.Label();
         this.comboBoxPlaneAngle = new System.Windows.Forms.ComboBox();
         this.labelMassVolumeConcentration = new System.Windows.Forms.Label();
         this.comboBoxMassVolumeConcentration = new System.Windows.Forms.ComboBox();
         this.labelSpecificVolume = new System.Windows.Forms.Label();
         this.comboBoxSpecificVolume = new System.Windows.Forms.ComboBox();
         this.labelFoulingFactor = new System.Windows.Forms.Label();
         this.comboBoxFoulingFactor = new System.Windows.Forms.ComboBox();
         this.labelVelocity = new System.Windows.Forms.Label();
         this.comboBoxVelocity = new System.Windows.Forms.ComboBox();
         this.labelSurfaceTension = new System.Windows.Forms.Label();
         this.comboBoxSurfaceTension = new System.Windows.Forms.ComboBox();
         this.labelHeatTransferCoeff = new System.Windows.Forms.Label();
         this.comboBoxHeatTransferCoeff = new System.Windows.Forms.ComboBox();
         this.labelVolumeRateFlowGases = new System.Windows.Forms.Label();
         this.comboBoxVolumeRateFlowGases = new System.Windows.Forms.ComboBox();
         this.labelVolumeRateFlowLiquids = new System.Windows.Forms.Label();
         this.comboBoxVolumeRateFlowLiquids = new System.Windows.Forms.ComboBox();
         this.labelLiquidHead = new System.Windows.Forms.Label();
         this.comboBoxLiquidHead = new System.Windows.Forms.ComboBox();
         this.labelMicroLength = new System.Windows.Forms.Label();
         this.comboBoxMicroLength = new System.Windows.Forms.ComboBox();
         this.labelSmallLength = new System.Windows.Forms.Label();
         this.comboBoxSmallLength = new System.Windows.Forms.ComboBox();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.panel = new System.Windows.Forms.Panel();
         this.labelVolumeHeatTransferCoefficient = new System.Windows.Forms.Label();
         this.comboBoxVolumeHeatTransferCoefficient = new System.Windows.Forms.ComboBox();
         this.groupBoxUnits.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelTemperature
         // 
         this.labelTemperature.Location = new System.Drawing.Point(6, 24);
         this.labelTemperature.Name = "labelTemperature";
         this.labelTemperature.Size = new System.Drawing.Size(152, 16);
         this.labelTemperature.TabIndex = 0;
         this.labelTemperature.Text = "Temperature:";
         this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelPressure
         // 
         this.labelPressure.Location = new System.Drawing.Point(6, 46);
         this.labelPressure.Name = "labelPressure";
         this.labelPressure.Size = new System.Drawing.Size(152, 16);
         this.labelPressure.TabIndex = 1;
         this.labelPressure.Text = "Pressure:";
         this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMassFlowRate
         // 
         this.labelMassFlowRate.Location = new System.Drawing.Point(6, 68);
         this.labelMassFlowRate.Name = "labelMassFlowRate";
         this.labelMassFlowRate.Size = new System.Drawing.Size(152, 16);
         this.labelMassFlowRate.TabIndex = 2;
         this.labelMassFlowRate.Text = "Mass Flow Rate:";
         this.labelMassFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelVolumeFlowRate
         // 
         this.labelVolumeFlowRate.Location = new System.Drawing.Point(6, 90);
         this.labelVolumeFlowRate.Name = "labelVolumeFlowRate";
         this.labelVolumeFlowRate.Size = new System.Drawing.Size(152, 16);
         this.labelVolumeFlowRate.TabIndex = 3;
         this.labelVolumeFlowRate.Text = "Volume Flow Rate:";
         this.labelVolumeFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMoistureContent
         // 
         this.labelMoistureContent.Location = new System.Drawing.Point(6, 112);
         this.labelMoistureContent.Name = "labelMoistureContent";
         this.labelMoistureContent.Size = new System.Drawing.Size(152, 16);
         this.labelMoistureContent.TabIndex = 4;
         this.labelMoistureContent.Text = "Moisture Content:";
         this.labelMoistureContent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelFraction
         // 
         this.labelFraction.Location = new System.Drawing.Point(6, 134);
         this.labelFraction.Name = "labelFraction";
         this.labelFraction.Size = new System.Drawing.Size(152, 16);
         this.labelFraction.TabIndex = 5;
         this.labelFraction.Text = "Fraction:";
         this.labelFraction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelSpecificEnergy
         // 
         this.labelSpecificEnergy.Location = new System.Drawing.Point(6, 156);
         this.labelSpecificEnergy.Name = "labelSpecificEnergy";
         this.labelSpecificEnergy.Size = new System.Drawing.Size(152, 16);
         this.labelSpecificEnergy.TabIndex = 6;
         this.labelSpecificEnergy.Text = "Specific Energy:";
         this.labelSpecificEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelSpecificHeat
         // 
         this.labelSpecificHeat.Location = new System.Drawing.Point(6, 178);
         this.labelSpecificHeat.Name = "labelSpecificHeat";
         this.labelSpecificHeat.Size = new System.Drawing.Size(152, 16);
         this.labelSpecificHeat.TabIndex = 7;
         this.labelSpecificHeat.Text = "Specific Heat:";
         this.labelSpecificHeat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelEnergy
         // 
         this.labelEnergy.Location = new System.Drawing.Point(6, 200);
         this.labelEnergy.Name = "labelEnergy";
         this.labelEnergy.Size = new System.Drawing.Size(152, 16);
         this.labelEnergy.TabIndex = 8;
         this.labelEnergy.Text = "Energy:";
         this.labelEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelPower
         // 
         this.labelPower.Location = new System.Drawing.Point(6, 222);
         this.labelPower.Name = "labelPower";
         this.labelPower.Size = new System.Drawing.Size(152, 16);
         this.labelPower.TabIndex = 9;
         this.labelPower.Text = "Power:";
         this.labelPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelDensity
         // 
         this.labelDensity.Location = new System.Drawing.Point(6, 244);
         this.labelDensity.Name = "labelDensity";
         this.labelDensity.Size = new System.Drawing.Size(152, 16);
         this.labelDensity.TabIndex = 10;
         this.labelDensity.Text = "Density:";
         this.labelDensity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelDynamicViscosity
         // 
         this.labelDynamicViscosity.Location = new System.Drawing.Point(6, 266);
         this.labelDynamicViscosity.Name = "labelDynamicViscosity";
         this.labelDynamicViscosity.Size = new System.Drawing.Size(152, 16);
         this.labelDynamicViscosity.TabIndex = 11;
         this.labelDynamicViscosity.Text = "Dynamic Viscosity:";
         this.labelDynamicViscosity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelKinematicViscosity
         // 
         this.labelKinematicViscosity.Location = new System.Drawing.Point(6, 288);
         this.labelKinematicViscosity.Name = "labelKinematicViscosity";
         this.labelKinematicViscosity.Size = new System.Drawing.Size(152, 16);
         this.labelKinematicViscosity.TabIndex = 12;
         this.labelKinematicViscosity.Text = "Kinematic Viscosity:";
         this.labelKinematicViscosity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelThermalConductivity
         // 
         this.labelThermalConductivity.Location = new System.Drawing.Point(6, 310);
         this.labelThermalConductivity.Name = "labelThermalConductivity";
         this.labelThermalConductivity.Size = new System.Drawing.Size(152, 16);
         this.labelThermalConductivity.TabIndex = 13;
         this.labelThermalConductivity.Text = "Thermal Conductivity:";
         this.labelThermalConductivity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelDiffusivity
         // 
         this.labelDiffusivity.Location = new System.Drawing.Point(6, 332);
         this.labelDiffusivity.Name = "labelDiffusivity";
         this.labelDiffusivity.Size = new System.Drawing.Size(152, 16);
         this.labelDiffusivity.TabIndex = 14;
         this.labelDiffusivity.Text = "Diffusivity:";
         this.labelDiffusivity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMass
         // 
         this.labelMass.Location = new System.Drawing.Point(6, 354);
         this.labelMass.Name = "labelMass";
         this.labelMass.Size = new System.Drawing.Size(152, 16);
         this.labelMass.TabIndex = 15;
         this.labelMass.Text = "Mass:";
         this.labelMass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelLength
         // 
         this.labelLength.Location = new System.Drawing.Point(290, 24);
         this.labelLength.Name = "labelLength";
         this.labelLength.Size = new System.Drawing.Size(152, 16);
         this.labelLength.TabIndex = 16;
         this.labelLength.Text = "Length:";
         this.labelLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelArea
         // 
         this.labelArea.Location = new System.Drawing.Point(290, 46);
         this.labelArea.Name = "labelArea";
         this.labelArea.Size = new System.Drawing.Size(152, 16);
         this.labelArea.TabIndex = 17;
         this.labelArea.Text = "Area:";
         this.labelArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelVolume
         // 
         this.labelVolume.Location = new System.Drawing.Point(290, 68);
         this.labelVolume.Name = "labelVolume";
         this.labelVolume.Size = new System.Drawing.Size(152, 16);
         this.labelVolume.TabIndex = 18;
         this.labelVolume.Text = "Volume:";
         this.labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelTime
         // 
         this.labelTime.Location = new System.Drawing.Point(290, 90);
         this.labelTime.Name = "labelTime";
         this.labelTime.Size = new System.Drawing.Size(152, 16);
         this.labelTime.TabIndex = 19;
         this.labelTime.Text = "Time:";
         this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxTemperature
         // 
         this.comboBoxTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTemperature.ItemHeight = 13;
         this.comboBoxTemperature.Location = new System.Drawing.Point(160, 20);
         this.comboBoxTemperature.Name = "comboBoxTemperature";
         this.comboBoxTemperature.Size = new System.Drawing.Size(112, 21);
         this.comboBoxTemperature.TabIndex = 20;
         this.comboBoxTemperature.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemperature_SelectedIndexChanged);
         // 
         // comboBoxPressure
         // 
         this.comboBoxPressure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPressure.Location = new System.Drawing.Point(160, 42);
         this.comboBoxPressure.Name = "comboBoxPressure";
         this.comboBoxPressure.Size = new System.Drawing.Size(112, 21);
         this.comboBoxPressure.TabIndex = 21;
         this.comboBoxPressure.SelectedIndexChanged += new System.EventHandler(this.comboBoxPressure_SelectedIndexChanged);
         // 
         // comboBoxMassFlowRate
         // 
         this.comboBoxMassFlowRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMassFlowRate.Location = new System.Drawing.Point(160, 64);
         this.comboBoxMassFlowRate.Name = "comboBoxMassFlowRate";
         this.comboBoxMassFlowRate.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMassFlowRate.TabIndex = 22;
         this.comboBoxMassFlowRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxMassFlowRate_SelectedIndexChanged);
         // 
         // comboBoxVolumeFlowRate
         // 
         this.comboBoxVolumeFlowRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolumeFlowRate.Location = new System.Drawing.Point(160, 86);
         this.comboBoxVolumeFlowRate.Name = "comboBoxVolumeFlowRate";
         this.comboBoxVolumeFlowRate.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolumeFlowRate.TabIndex = 23;
         this.comboBoxVolumeFlowRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolumeFlowRate_SelectedIndexChanged);
         // 
         // comboBoxMoistureContent
         // 
         this.comboBoxMoistureContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMoistureContent.Location = new System.Drawing.Point(160, 108);
         this.comboBoxMoistureContent.Name = "comboBoxMoistureContent";
         this.comboBoxMoistureContent.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMoistureContent.TabIndex = 24;
         this.comboBoxMoistureContent.SelectedIndexChanged += new System.EventHandler(this.comboBoxMoistureContent_SelectedIndexChanged);
         // 
         // comboBoxFraction
         // 
         this.comboBoxFraction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFraction.Location = new System.Drawing.Point(160, 130);
         this.comboBoxFraction.Name = "comboBoxFraction";
         this.comboBoxFraction.Size = new System.Drawing.Size(112, 21);
         this.comboBoxFraction.TabIndex = 25;
         this.comboBoxFraction.SelectedIndexChanged += new System.EventHandler(this.comboBoxFraction_SelectedIndexChanged);
         // 
         // comboBoxSpecificEnergy
         // 
         this.comboBoxSpecificEnergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSpecificEnergy.Location = new System.Drawing.Point(160, 152);
         this.comboBoxSpecificEnergy.Name = "comboBoxSpecificEnergy";
         this.comboBoxSpecificEnergy.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSpecificEnergy.TabIndex = 26;
         this.comboBoxSpecificEnergy.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecificEnergy_SelectedIndexChanged);
         // 
         // comboBoxSpecificHeat
         // 
         this.comboBoxSpecificHeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSpecificHeat.Location = new System.Drawing.Point(160, 174);
         this.comboBoxSpecificHeat.Name = "comboBoxSpecificHeat";
         this.comboBoxSpecificHeat.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSpecificHeat.TabIndex = 27;
         this.comboBoxSpecificHeat.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecificHeat_SelectedIndexChanged);
         // 
         // comboBoxEnergy
         // 
         this.comboBoxEnergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxEnergy.Location = new System.Drawing.Point(160, 196);
         this.comboBoxEnergy.Name = "comboBoxEnergy";
         this.comboBoxEnergy.Size = new System.Drawing.Size(112, 21);
         this.comboBoxEnergy.TabIndex = 28;
         this.comboBoxEnergy.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnergy_SelectedIndexChanged);
         // 
         // comboBoxPower
         // 
         this.comboBoxPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPower.Location = new System.Drawing.Point(160, 218);
         this.comboBoxPower.Name = "comboBoxPower";
         this.comboBoxPower.Size = new System.Drawing.Size(112, 21);
         this.comboBoxPower.TabIndex = 29;
         this.comboBoxPower.SelectedIndexChanged += new System.EventHandler(this.comboBoxPower_SelectedIndexChanged);
         // 
         // comboBoxDensity
         // 
         this.comboBoxDensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDensity.Location = new System.Drawing.Point(160, 240);
         this.comboBoxDensity.Name = "comboBoxDensity";
         this.comboBoxDensity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDensity.TabIndex = 30;
         this.comboBoxDensity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDensity_SelectedIndexChanged);
         // 
         // comboBoxDynamicViscosity
         // 
         this.comboBoxDynamicViscosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDynamicViscosity.Location = new System.Drawing.Point(160, 262);
         this.comboBoxDynamicViscosity.Name = "comboBoxDynamicViscosity";
         this.comboBoxDynamicViscosity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDynamicViscosity.TabIndex = 31;
         this.comboBoxDynamicViscosity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDynamicViscosity_SelectedIndexChanged);
         // 
         // comboBoxKinematicViscosity
         // 
         this.comboBoxKinematicViscosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxKinematicViscosity.Location = new System.Drawing.Point(160, 284);
         this.comboBoxKinematicViscosity.Name = "comboBoxKinematicViscosity";
         this.comboBoxKinematicViscosity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxKinematicViscosity.TabIndex = 32;
         this.comboBoxKinematicViscosity.SelectedIndexChanged += new System.EventHandler(this.comboBoxKinematicViscosity_SelectedIndexChanged);
         // 
         // comboBoxThermalConductivity
         // 
         this.comboBoxThermalConductivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxThermalConductivity.Location = new System.Drawing.Point(160, 306);
         this.comboBoxThermalConductivity.Name = "comboBoxThermalConductivity";
         this.comboBoxThermalConductivity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxThermalConductivity.TabIndex = 33;
         this.comboBoxThermalConductivity.SelectedIndexChanged += new System.EventHandler(this.comboBoxThermalConductivity_SelectedIndexChanged);
         // 
         // comboBoxDiffusivity
         // 
         this.comboBoxDiffusivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDiffusivity.Location = new System.Drawing.Point(160, 328);
         this.comboBoxDiffusivity.Name = "comboBoxDiffusivity";
         this.comboBoxDiffusivity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxDiffusivity.TabIndex = 34;
         this.comboBoxDiffusivity.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiffusivity_SelectedIndexChanged);
         // 
         // comboBoxMass
         // 
         this.comboBoxMass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMass.Location = new System.Drawing.Point(160, 350);
         this.comboBoxMass.Name = "comboBoxMass";
         this.comboBoxMass.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMass.TabIndex = 35;
         this.comboBoxMass.SelectedIndexChanged += new System.EventHandler(this.comboBoxMass_SelectedIndexChanged);
         // 
         // comboBoxLength
         // 
         this.comboBoxLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxLength.Location = new System.Drawing.Point(444, 20);
         this.comboBoxLength.Name = "comboBoxLength";
         this.comboBoxLength.Size = new System.Drawing.Size(112, 21);
         this.comboBoxLength.TabIndex = 36;
         this.comboBoxLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxLength_SelectedIndexChanged);
         // 
         // comboBoxArea
         // 
         this.comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxArea.Location = new System.Drawing.Point(444, 42);
         this.comboBoxArea.Name = "comboBoxArea";
         this.comboBoxArea.Size = new System.Drawing.Size(112, 21);
         this.comboBoxArea.TabIndex = 37;
         this.comboBoxArea.SelectedIndexChanged += new System.EventHandler(this.comboBoxArea_SelectedIndexChanged);
         // 
         // comboBoxVolume
         // 
         this.comboBoxVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolume.Location = new System.Drawing.Point(444, 64);
         this.comboBoxVolume.Name = "comboBoxVolume";
         this.comboBoxVolume.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolume.TabIndex = 38;
         this.comboBoxVolume.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolume_SelectedIndexChanged);
         // 
         // comboBoxTime
         // 
         this.comboBoxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTime.Location = new System.Drawing.Point(444, 86);
         this.comboBoxTime.Name = "comboBoxTime";
         this.comboBoxTime.Size = new System.Drawing.Size(112, 21);
         this.comboBoxTime.TabIndex = 39;
         this.comboBoxTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxTime_SelectedIndexChanged);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(204, 440);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 40;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // labelName
         // 
         this.labelName.BackColor = System.Drawing.Color.DarkGray;
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelName.Location = new System.Drawing.Point(8, 8);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(60, 20);
         this.labelName.TabIndex = 41;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(68, 8);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(184, 20);
         this.textBoxName.TabIndex = 42;
         this.textBoxName.Text = "";
         this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
         this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
         // 
         // groupBoxUnits
         // 
         this.groupBoxUnits.Controls.Add(this.labelVolumeHeatTransferCoefficient);
         this.groupBoxUnits.Controls.Add(this.comboBoxVolumeHeatTransferCoefficient);
         this.groupBoxUnits.Controls.Add(this.labelPlaneAngle);
         this.groupBoxUnits.Controls.Add(this.comboBoxPlaneAngle);
         this.groupBoxUnits.Controls.Add(this.labelMassVolumeConcentration);
         this.groupBoxUnits.Controls.Add(this.comboBoxMassVolumeConcentration);
         this.groupBoxUnits.Controls.Add(this.labelSpecificVolume);
         this.groupBoxUnits.Controls.Add(this.comboBoxSpecificVolume);
         this.groupBoxUnits.Controls.Add(this.labelFoulingFactor);
         this.groupBoxUnits.Controls.Add(this.comboBoxFoulingFactor);
         this.groupBoxUnits.Controls.Add(this.labelVelocity);
         this.groupBoxUnits.Controls.Add(this.comboBoxVelocity);
         this.groupBoxUnits.Controls.Add(this.labelSurfaceTension);
         this.groupBoxUnits.Controls.Add(this.comboBoxSurfaceTension);
         this.groupBoxUnits.Controls.Add(this.labelHeatTransferCoeff);
         this.groupBoxUnits.Controls.Add(this.comboBoxHeatTransferCoeff);
         this.groupBoxUnits.Controls.Add(this.labelVolumeRateFlowGases);
         this.groupBoxUnits.Controls.Add(this.comboBoxVolumeRateFlowGases);
         this.groupBoxUnits.Controls.Add(this.labelVolumeRateFlowLiquids);
         this.groupBoxUnits.Controls.Add(this.comboBoxVolumeRateFlowLiquids);
         this.groupBoxUnits.Controls.Add(this.labelLiquidHead);
         this.groupBoxUnits.Controls.Add(this.comboBoxLiquidHead);
         this.groupBoxUnits.Controls.Add(this.labelMicroLength);
         this.groupBoxUnits.Controls.Add(this.comboBoxMicroLength);
         this.groupBoxUnits.Controls.Add(this.labelSmallLength);
         this.groupBoxUnits.Controls.Add(this.comboBoxSmallLength);
         this.groupBoxUnits.Controls.Add(this.labelTemperature);
         this.groupBoxUnits.Controls.Add(this.labelPressure);
         this.groupBoxUnits.Controls.Add(this.labelMassFlowRate);
         this.groupBoxUnits.Controls.Add(this.labelVolumeFlowRate);
         this.groupBoxUnits.Controls.Add(this.labelMoistureContent);
         this.groupBoxUnits.Controls.Add(this.labelFraction);
         this.groupBoxUnits.Controls.Add(this.labelSpecificEnergy);
         this.groupBoxUnits.Controls.Add(this.labelSpecificHeat);
         this.groupBoxUnits.Controls.Add(this.labelEnergy);
         this.groupBoxUnits.Controls.Add(this.labelPower);
         this.groupBoxUnits.Controls.Add(this.labelDensity);
         this.groupBoxUnits.Controls.Add(this.labelDynamicViscosity);
         this.groupBoxUnits.Controls.Add(this.labelKinematicViscosity);
         this.groupBoxUnits.Controls.Add(this.labelThermalConductivity);
         this.groupBoxUnits.Controls.Add(this.labelDiffusivity);
         this.groupBoxUnits.Controls.Add(this.labelMass);
         this.groupBoxUnits.Controls.Add(this.labelLength);
         this.groupBoxUnits.Controls.Add(this.labelArea);
         this.groupBoxUnits.Controls.Add(this.labelVolume);
         this.groupBoxUnits.Controls.Add(this.labelTime);
         this.groupBoxUnits.Controls.Add(this.comboBoxTemperature);
         this.groupBoxUnits.Controls.Add(this.comboBoxPressure);
         this.groupBoxUnits.Controls.Add(this.comboBoxMassFlowRate);
         this.groupBoxUnits.Controls.Add(this.comboBoxVolumeFlowRate);
         this.groupBoxUnits.Controls.Add(this.comboBoxMoistureContent);
         this.groupBoxUnits.Controls.Add(this.comboBoxFraction);
         this.groupBoxUnits.Controls.Add(this.comboBoxSpecificEnergy);
         this.groupBoxUnits.Controls.Add(this.comboBoxSpecificHeat);
         this.groupBoxUnits.Controls.Add(this.comboBoxEnergy);
         this.groupBoxUnits.Controls.Add(this.comboBoxPower);
         this.groupBoxUnits.Controls.Add(this.comboBoxDensity);
         this.groupBoxUnits.Controls.Add(this.comboBoxDynamicViscosity);
         this.groupBoxUnits.Controls.Add(this.comboBoxKinematicViscosity);
         this.groupBoxUnits.Controls.Add(this.comboBoxThermalConductivity);
         this.groupBoxUnits.Controls.Add(this.comboBoxDiffusivity);
         this.groupBoxUnits.Controls.Add(this.comboBoxMass);
         this.groupBoxUnits.Controls.Add(this.comboBoxLength);
         this.groupBoxUnits.Controls.Add(this.comboBoxArea);
         this.groupBoxUnits.Controls.Add(this.comboBoxVolume);
         this.groupBoxUnits.Controls.Add(this.comboBoxTime);
         this.groupBoxUnits.Location = new System.Drawing.Point(8, 32);
         this.groupBoxUnits.Name = "groupBoxUnits";
         this.groupBoxUnits.Size = new System.Drawing.Size(564, 400);
         this.groupBoxUnits.TabIndex = 43;
         this.groupBoxUnits.TabStop = false;
         // 
         // labelPlaneAngle
         // 
         this.labelPlaneAngle.Location = new System.Drawing.Point(290, 354);
         this.labelPlaneAngle.Name = "labelPlaneAngle";
         this.labelPlaneAngle.Size = new System.Drawing.Size(152, 16);
         this.labelPlaneAngle.TabIndex = 62;
         this.labelPlaneAngle.Text = "Plane Angle:";
         this.labelPlaneAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxPlaneAngle
         // 
         this.comboBoxPlaneAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPlaneAngle.Location = new System.Drawing.Point(444, 350);
         this.comboBoxPlaneAngle.Name = "comboBoxPlaneAngle";
         this.comboBoxPlaneAngle.Size = new System.Drawing.Size(112, 21);
         this.comboBoxPlaneAngle.TabIndex = 63;
         this.comboBoxPlaneAngle.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlaneAngle_SelectedIndexChanged);
         // 
         // labelMassVolumeConcentration
         // 
         this.labelMassVolumeConcentration.Location = new System.Drawing.Point(290, 332);
         this.labelMassVolumeConcentration.Name = "labelMassVolumeConcentration";
         this.labelMassVolumeConcentration.Size = new System.Drawing.Size(152, 16);
         this.labelMassVolumeConcentration.TabIndex = 60;
         this.labelMassVolumeConcentration.Text = "Mass Volume Concentration:";
         this.labelMassVolumeConcentration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxMassVolumeConcentration
         // 
         this.comboBoxMassVolumeConcentration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMassVolumeConcentration.Location = new System.Drawing.Point(444, 328);
         this.comboBoxMassVolumeConcentration.Name = "comboBoxMassVolumeConcentration";
         this.comboBoxMassVolumeConcentration.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMassVolumeConcentration.TabIndex = 61;
         this.comboBoxMassVolumeConcentration.SelectedIndexChanged += new System.EventHandler(this.comboBoxMassVolumeConcentration_SelectedIndexChanged);
         // 
         // labelSpecificVolume
         // 
         this.labelSpecificVolume.Location = new System.Drawing.Point(290, 310);
         this.labelSpecificVolume.Name = "labelSpecificVolume";
         this.labelSpecificVolume.Size = new System.Drawing.Size(152, 16);
         this.labelSpecificVolume.TabIndex = 58;
         this.labelSpecificVolume.Text = "Specific Volume:";
         this.labelSpecificVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxSpecificVolume
         // 
         this.comboBoxSpecificVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSpecificVolume.Location = new System.Drawing.Point(444, 306);
         this.comboBoxSpecificVolume.Name = "comboBoxSpecificVolume";
         this.comboBoxSpecificVolume.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSpecificVolume.TabIndex = 59;
         this.comboBoxSpecificVolume.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecificVolume_SelectedIndexChanged);
         // 
         // labelFoulingFactor
         // 
         this.labelFoulingFactor.Location = new System.Drawing.Point(290, 288);
         this.labelFoulingFactor.Name = "labelFoulingFactor";
         this.labelFoulingFactor.Size = new System.Drawing.Size(152, 16);
         this.labelFoulingFactor.TabIndex = 56;
         this.labelFoulingFactor.Text = "Fouling Factor:";
         this.labelFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxFoulingFactor
         // 
         this.comboBoxFoulingFactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFoulingFactor.Location = new System.Drawing.Point(444, 284);
         this.comboBoxFoulingFactor.Name = "comboBoxFoulingFactor";
         this.comboBoxFoulingFactor.Size = new System.Drawing.Size(112, 21);
         this.comboBoxFoulingFactor.TabIndex = 57;
         this.comboBoxFoulingFactor.SelectedIndexChanged += new System.EventHandler(this.comboBoxFoulingFactor_SelectedIndexChanged);
         // 
         // labelVelocity
         // 
         this.labelVelocity.Location = new System.Drawing.Point(290, 266);
         this.labelVelocity.Name = "labelVelocity";
         this.labelVelocity.Size = new System.Drawing.Size(152, 16);
         this.labelVelocity.TabIndex = 54;
         this.labelVelocity.Text = "Velocity:";
         this.labelVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxVelocity
         // 
         this.comboBoxVelocity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVelocity.Location = new System.Drawing.Point(444, 262);
         this.comboBoxVelocity.Name = "comboBoxVelocity";
         this.comboBoxVelocity.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVelocity.TabIndex = 55;
         this.comboBoxVelocity.SelectedIndexChanged += new System.EventHandler(this.comboBoxVelocity_SelectedIndexChanged);
         // 
         // labelSurfaceTension
         // 
         this.labelSurfaceTension.Location = new System.Drawing.Point(290, 244);
         this.labelSurfaceTension.Name = "labelSurfaceTension";
         this.labelSurfaceTension.Size = new System.Drawing.Size(152, 16);
         this.labelSurfaceTension.TabIndex = 52;
         this.labelSurfaceTension.Text = "Surface Tension:";
         this.labelSurfaceTension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxSurfaceTension
         // 
         this.comboBoxSurfaceTension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSurfaceTension.Location = new System.Drawing.Point(444, 240);
         this.comboBoxSurfaceTension.Name = "comboBoxSurfaceTension";
         this.comboBoxSurfaceTension.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSurfaceTension.TabIndex = 53;
         this.comboBoxSurfaceTension.SelectedIndexChanged += new System.EventHandler(this.comboBoxSurfaceTension_SelectedIndexChanged);
         // 
         // labelHeatTransferCoeff
         // 
         this.labelHeatTransferCoeff.Location = new System.Drawing.Point(290, 222);
         this.labelHeatTransferCoeff.Name = "labelHeatTransferCoeff";
         this.labelHeatTransferCoeff.Size = new System.Drawing.Size(152, 16);
         this.labelHeatTransferCoeff.TabIndex = 50;
         this.labelHeatTransferCoeff.Text = "Heat Transfer Coeff.:";
         this.labelHeatTransferCoeff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxHeatTransferCoeff
         // 
         this.comboBoxHeatTransferCoeff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxHeatTransferCoeff.Location = new System.Drawing.Point(444, 218);
         this.comboBoxHeatTransferCoeff.Name = "comboBoxHeatTransferCoeff";
         this.comboBoxHeatTransferCoeff.Size = new System.Drawing.Size(112, 21);
         this.comboBoxHeatTransferCoeff.TabIndex = 51;
         this.comboBoxHeatTransferCoeff.SelectedIndexChanged += new System.EventHandler(this.comboBoxHeatTransferCoeff_SelectedIndexChanged);
         // 
         // labelVolumeRateFlowGases
         // 
         this.labelVolumeRateFlowGases.Location = new System.Drawing.Point(290, 200);
         this.labelVolumeRateFlowGases.Name = "labelVolumeRateFlowGases";
         this.labelVolumeRateFlowGases.Size = new System.Drawing.Size(152, 16);
         this.labelVolumeRateFlowGases.TabIndex = 48;
         this.labelVolumeRateFlowGases.Text = "Volume Rate Flow Gases:";
         this.labelVolumeRateFlowGases.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxVolumeRateFlowGases
         // 
         this.comboBoxVolumeRateFlowGases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolumeRateFlowGases.Location = new System.Drawing.Point(444, 196);
         this.comboBoxVolumeRateFlowGases.Name = "comboBoxVolumeRateFlowGases";
         this.comboBoxVolumeRateFlowGases.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolumeRateFlowGases.TabIndex = 49;
         this.comboBoxVolumeRateFlowGases.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolumeRateFlowGases_SelectedIndexChanged);
         // 
         // labelVolumeRateFlowLiquids
         // 
         this.labelVolumeRateFlowLiquids.Location = new System.Drawing.Point(290, 178);
         this.labelVolumeRateFlowLiquids.Name = "labelVolumeRateFlowLiquids";
         this.labelVolumeRateFlowLiquids.Size = new System.Drawing.Size(152, 16);
         this.labelVolumeRateFlowLiquids.TabIndex = 46;
         this.labelVolumeRateFlowLiquids.Text = "Volume Rate Flow Liquids:";
         this.labelVolumeRateFlowLiquids.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxVolumeRateFlowLiquids
         // 
         this.comboBoxVolumeRateFlowLiquids.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolumeRateFlowLiquids.Location = new System.Drawing.Point(444, 174);
         this.comboBoxVolumeRateFlowLiquids.Name = "comboBoxVolumeRateFlowLiquids";
         this.comboBoxVolumeRateFlowLiquids.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolumeRateFlowLiquids.TabIndex = 47;
         this.comboBoxVolumeRateFlowLiquids.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolumeRateFlowLiquids_SelectedIndexChanged);
         // 
         // labelLiquidHead
         // 
         this.labelLiquidHead.Location = new System.Drawing.Point(290, 156);
         this.labelLiquidHead.Name = "labelLiquidHead";
         this.labelLiquidHead.Size = new System.Drawing.Size(152, 16);
         this.labelLiquidHead.TabIndex = 44;
         this.labelLiquidHead.Text = "Liquid Head:";
         this.labelLiquidHead.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxLiquidHead
         // 
         this.comboBoxLiquidHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxLiquidHead.Location = new System.Drawing.Point(444, 152);
         this.comboBoxLiquidHead.Name = "comboBoxLiquidHead";
         this.comboBoxLiquidHead.Size = new System.Drawing.Size(112, 21);
         this.comboBoxLiquidHead.TabIndex = 45;
         this.comboBoxLiquidHead.SelectedIndexChanged += new System.EventHandler(this.comboBoxLiquidHead_SelectedIndexChanged);
         // 
         // labelMicroLength
         // 
         this.labelMicroLength.Location = new System.Drawing.Point(290, 134);
         this.labelMicroLength.Name = "labelMicroLength";
         this.labelMicroLength.Size = new System.Drawing.Size(152, 16);
         this.labelMicroLength.TabIndex = 42;
         this.labelMicroLength.Text = "Micro Length:";
         this.labelMicroLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxMicroLength
         // 
         this.comboBoxMicroLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMicroLength.Location = new System.Drawing.Point(444, 130);
         this.comboBoxMicroLength.Name = "comboBoxMicroLength";
         this.comboBoxMicroLength.Size = new System.Drawing.Size(112, 21);
         this.comboBoxMicroLength.TabIndex = 43;
         this.comboBoxMicroLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxMicroLength_SelectedIndexChanged);
         // 
         // labelSmallLength
         // 
         this.labelSmallLength.Location = new System.Drawing.Point(290, 112);
         this.labelSmallLength.Name = "labelSmallLength";
         this.labelSmallLength.Size = new System.Drawing.Size(152, 16);
         this.labelSmallLength.TabIndex = 40;
         this.labelSmallLength.Text = "Small Length:";
         this.labelSmallLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxSmallLength
         // 
         this.comboBoxSmallLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSmallLength.Location = new System.Drawing.Point(444, 108);
         this.comboBoxSmallLength.Name = "comboBoxSmallLength";
         this.comboBoxSmallLength.Size = new System.Drawing.Size(112, 21);
         this.comboBoxSmallLength.TabIndex = 41;
         this.comboBoxSmallLength.SelectedIndexChanged += new System.EventHandler(this.comboBoxSmallLength_SelectedIndexChanged);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(300, 440);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 44;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.textBoxName);
         this.panel.Controls.Add(this.groupBoxUnits);
         this.panel.Controls.Add(this.buttonCancel);
         this.panel.Controls.Add(this.buttonOk);
         this.panel.Controls.Add(this.labelName);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(582, 472);
         this.panel.TabIndex = 45;
         // 
         // labelVolumeHeatTransferCoefficient
         // 
         this.labelVolumeHeatTransferCoefficient.Location = new System.Drawing.Point(290, 376);
         this.labelVolumeHeatTransferCoefficient.Name = "labelVolumeHeatTransferCoefficient";
         this.labelVolumeHeatTransferCoefficient.Size = new System.Drawing.Size(152, 16);
         this.labelVolumeHeatTransferCoefficient.TabIndex = 64;
         this.labelVolumeHeatTransferCoefficient.Text = "Volume Heat Transfer Coeff.:";
         this.labelVolumeHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxVolumeHeatTransferCoefficient
         // 
         this.comboBoxVolumeHeatTransferCoefficient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxVolumeHeatTransferCoefficient.Location = new System.Drawing.Point(444, 372);
         this.comboBoxVolumeHeatTransferCoefficient.Name = "comboBoxVolumeHeatTransferCoefficient";
         this.comboBoxVolumeHeatTransferCoefficient.Size = new System.Drawing.Size(112, 21);
         this.comboBoxVolumeHeatTransferCoefficient.TabIndex = 65;
         this.comboBoxVolumeHeatTransferCoefficient.SelectedIndexChanged += new System.EventHandler(this.comboBoxVolumeHeatTransferCoefficient_SelectedIndexChanged);
         // 
         // UnitSystemEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(582, 472);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "UnitSystemEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Unit System";
         this.groupBoxUnits.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

//      public UnitSystem GetUnitSystem()
//      {
//         return this.unitSystemCache;
//      }

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
                  this.unitSystemCache.Name = this.textBoxName.Text;
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
         this.unitSystemCache.TemperatureUnitType = TemperatureUnit.Instance.GetUnitAsEnum((string)cb.SelectedItem);
      }

      private void comboBoxPressure_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.PressureUnitType = PressureUnit.Instance.GetUnitAsEnum<PressureUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxMassFlowRate_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.MassFlowRateUnitType = MassFlowRateUnit.Instance.GetUnitAsEnum <MassFlowRateUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVolumeFlowRate_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VolumeFlowRateUnitType = VolumeFlowRateUnit.Instance.GetUnitAsEnum<VolumeFlowRateUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxMoistureContent_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.MoistureContentUnitType = MoistureContentUnit.Instance.GetUnitAsEnum<MoistureContentUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxFraction_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.FractionUnitType = FractionUnit.Instance.GetUnitAsEnum<FractionUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxSpecificEnergy_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.SpecificEnergyUnitType = SpecificEnergyUnit.Instance.GetUnitAsEnum<SpecificEnergyUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxSpecificHeat_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.SpecificHeatUnitType = SpecificHeatUnit.Instance.GetUnitAsEnum<SpecificHeatUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxEnergy_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.EnergyUnitType = EnergyUnit.Instance.GetUnitAsEnum<EnergyUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxPower_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.PowerUnitType = PowerUnit.Instance.GetUnitAsEnum<PowerUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxDensity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.DensityUnitType = DensityUnit.Instance.GetUnitAsEnum<DensityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxDynamicViscosity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.DynamicViscosityUnitType = DynamicViscosityUnit.Instance.GetUnitAsEnum<DynamicViscosityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxKinematicViscosity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.KinematicViscosityUnitType = KinematicViscosityUnit.Instance.GetUnitAsEnum<KinematicViscosityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxThermalConductivity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.ThermalConductivityUnitType = ThermalConductivityUnit.Instance.GetUnitAsEnum<ThermalConductivityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxDiffusivity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.DiffusivityUnitType = DiffusivityUnit.Instance.GetUnitAsEnum<DiffusivityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxMass_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.MassUnitType = MassUnit.Instance.GetUnitAsEnum<MassUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxLength_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.LengthUnitType = LengthUnit.Instance.GetUnitAsEnum<LengthUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxArea_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.AreaUnitType = AreaUnit.Instance.GetUnitAsEnum<AreaUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVolume_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VolumeUnitType = VolumeUnit.Instance.GetUnitAsEnum<VolumeUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxTime_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.TimeUnitType = TimeUnit.Instance.GetUnitAsEnum<TimeUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxSmallLength_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.SmallLengthUnitType = SmallLengthUnit.Instance.GetUnitAsEnum<SmallLengthUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxMicroLength_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.MicroLengthUnitType = MicroLengthUnit.Instance.GetUnitAsEnum<MicroLengthUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxLiquidHead_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.LiquidHeadUnitType = LiquidHeadUnit.Instance.GetUnitAsEnum<LiquidHeadUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVolumeRateFlowLiquids_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VolumeRateFlowLiquidsUnitType = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsEnum<VolumeRateFlowLiquidsUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVolumeRateFlowGases_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VolumeRateFlowGasesUnitType = VolumeRateFlowGasesUnit.Instance.GetUnitAsEnum<VolumeRateFlowGasesUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxHeatTransferCoeff_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.HeatTransferCoefficientUnitType = HeatTransferCoefficientUnit.Instance.GetUnitAsEnum<HeatTransferCoefficientUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxSurfaceTension_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.SurfaceTensionUnitType = SurfaceTensionUnit.Instance.GetUnitAsEnum<SurfaceTensionUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVelocity_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VelocityUnitType = VelocityUnit.Instance.GetUnitAsEnum<VelocityUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxFoulingFactor_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.FoulingFactorUnitType = FoulingFactorUnit.Instance.GetUnitAsEnum<FoulingFactorUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxSpecificVolume_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.SpecificVolumeUnitType = SpecificVolumeUnit.Instance.GetUnitAsEnum<SpecificVolumeUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxMassVolumeConcentration_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.MassVolumeConcentrationUnitType = MassVolumeConcentrationUnit.Instance.GetUnitAsEnum<MassVolumeConcentrationUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxPlaneAngle_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.PlaneAngleUnitType = PlaneAngleUnit.Instance.GetUnitAsEnum<PlaneAngleUnitType>((string)cb.SelectedItem);
      }

      private void comboBoxVolumeHeatTransferCoefficient_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         ComboBox cb = (ComboBox)sender;
         this.unitSystemCache.VolumeHeatTransferCoefficientUnitType = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsEnum<VolumeHeatTransferCoefficientUnitType>((string)cb.SelectedItem);
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         UnitSystem unitSystemOriginal = unitSystemsCtrl.GetSelectedUnitSystem();
         unitSystemOriginal.Commit(this.unitSystemCache);
         this.Close();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
