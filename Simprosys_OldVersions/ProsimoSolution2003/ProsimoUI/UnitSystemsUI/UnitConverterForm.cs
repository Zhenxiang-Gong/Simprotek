using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitSystemsUI
{
	/// <summary>
	/// Summary description for UnitConverterForm.
	/// </summary>
	public class UnitConverterForm : System.Windows.Forms.Form
	{
      private bool inConstruction;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.ListBox listBoxPhysicalQuantities;
      private System.Windows.Forms.Label labelPhysicalQuantities;
      private System.Windows.Forms.GroupBox groupBoxFirstUnit;
      private System.Windows.Forms.TextBox textBoxFirstUnit;
      private System.Windows.Forms.ComboBox comboBoxFirstUnit;
      private System.Windows.Forms.GroupBox groupBoxSecondUnit;
      private System.Windows.Forms.ComboBox comboBoxSecondUnit;
      private System.Windows.Forms.TextBox textBoxSecondUnit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UnitConverterForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         ArrayList physicalQuantities = new ArrayList(UnitSystemService.GetInstance().GetPhysicalQuantitiesAsStrings());
         physicalQuantities.Sort();
         IEnumerator e = physicalQuantities.GetEnumerator();
         while (e.MoveNext())
         {
            this.listBoxPhysicalQuantities.Items.Add(e.Current);
         }
         this.listBoxPhysicalQuantities.SelectedIndex = 0;
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
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxSecondUnit = new System.Windows.Forms.GroupBox();
         this.comboBoxSecondUnit = new System.Windows.Forms.ComboBox();
         this.textBoxSecondUnit = new System.Windows.Forms.TextBox();
         this.groupBoxFirstUnit = new System.Windows.Forms.GroupBox();
         this.comboBoxFirstUnit = new System.Windows.Forms.ComboBox();
         this.textBoxFirstUnit = new System.Windows.Forms.TextBox();
         this.labelPhysicalQuantities = new System.Windows.Forms.Label();
         this.listBoxPhysicalQuantities = new System.Windows.Forms.ListBox();
         this.panel.SuspendLayout();
         this.groupBoxSecondUnit.SuspendLayout();
         this.groupBoxFirstUnit.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxSecondUnit);
         this.panel.Controls.Add(this.groupBoxFirstUnit);
         this.panel.Controls.Add(this.labelPhysicalQuantities);
         this.panel.Controls.Add(this.listBoxPhysicalQuantities);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(358, 243);
         this.panel.TabIndex = 0;
         // 
         // groupBoxSecondUnit
         // 
         this.groupBoxSecondUnit.Controls.Add(this.comboBoxSecondUnit);
         this.groupBoxSecondUnit.Controls.Add(this.textBoxSecondUnit);
         this.groupBoxSecondUnit.Location = new System.Drawing.Point(200, 140);
         this.groupBoxSecondUnit.Name = "groupBoxSecondUnit";
         this.groupBoxSecondUnit.Size = new System.Drawing.Size(144, 92);
         this.groupBoxSecondUnit.TabIndex = 3;
         this.groupBoxSecondUnit.TabStop = false;
         this.groupBoxSecondUnit.Text = "Second Value and Unit";
         // 
         // comboBoxSecondUnit
         // 
         this.comboBoxSecondUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxSecondUnit.Location = new System.Drawing.Point(12, 56);
         this.comboBoxSecondUnit.Name = "comboBoxSecondUnit";
         this.comboBoxSecondUnit.Size = new System.Drawing.Size(120, 21);
         this.comboBoxSecondUnit.TabIndex = 1;
         this.comboBoxSecondUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxSecondUnit_SelectedIndexChanged);
         // 
         // textBoxSecondUnit
         // 
         this.textBoxSecondUnit.Location = new System.Drawing.Point(12, 28);
         this.textBoxSecondUnit.Name = "textBoxSecondUnit";
         this.textBoxSecondUnit.Size = new System.Drawing.Size(120, 20);
         this.textBoxSecondUnit.TabIndex = 0;
         this.textBoxSecondUnit.Text = "";
         this.textBoxSecondUnit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSecondUnit_Validating);
         this.textBoxSecondUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxSecondUnit_KeyUp);
         // 
         // groupBoxFirstUnit
         // 
         this.groupBoxFirstUnit.Controls.Add(this.comboBoxFirstUnit);
         this.groupBoxFirstUnit.Controls.Add(this.textBoxFirstUnit);
         this.groupBoxFirstUnit.Location = new System.Drawing.Point(200, 28);
         this.groupBoxFirstUnit.Name = "groupBoxFirstUnit";
         this.groupBoxFirstUnit.Size = new System.Drawing.Size(144, 92);
         this.groupBoxFirstUnit.TabIndex = 2;
         this.groupBoxFirstUnit.TabStop = false;
         this.groupBoxFirstUnit.Text = "First Value and Unit";
         // 
         // comboBoxFirstUnit
         // 
         this.comboBoxFirstUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFirstUnit.Location = new System.Drawing.Point(12, 56);
         this.comboBoxFirstUnit.Name = "comboBoxFirstUnit";
         this.comboBoxFirstUnit.Size = new System.Drawing.Size(120, 21);
         this.comboBoxFirstUnit.TabIndex = 1;
         this.comboBoxFirstUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxFirstUnit_SelectedIndexChanged);
         // 
         // textBoxFirstUnit
         // 
         this.textBoxFirstUnit.Location = new System.Drawing.Point(12, 28);
         this.textBoxFirstUnit.Name = "textBoxFirstUnit";
         this.textBoxFirstUnit.Size = new System.Drawing.Size(120, 20);
         this.textBoxFirstUnit.TabIndex = 0;
         this.textBoxFirstUnit.Text = "";
         this.textBoxFirstUnit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFirstUnit_Validating);
         this.textBoxFirstUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFirstUnit_KeyUp);
         // 
         // labelPhysicalQuantities
         // 
         this.labelPhysicalQuantities.Location = new System.Drawing.Point(12, 12);
         this.labelPhysicalQuantities.Name = "labelPhysicalQuantities";
         this.labelPhysicalQuantities.Size = new System.Drawing.Size(172, 16);
         this.labelPhysicalQuantities.TabIndex = 1;
         this.labelPhysicalQuantities.Text = "Physical Quantities:";
         this.labelPhysicalQuantities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // listBoxPhysicalQuantities
         // 
         this.listBoxPhysicalQuantities.Location = new System.Drawing.Point(12, 32);
         this.listBoxPhysicalQuantities.Name = "listBoxPhysicalQuantities";
         this.listBoxPhysicalQuantities.ScrollAlwaysVisible = true;
         this.listBoxPhysicalQuantities.Size = new System.Drawing.Size(172, 199);
         this.listBoxPhysicalQuantities.TabIndex = 0;
         this.listBoxPhysicalQuantities.SelectedIndexChanged += new System.EventHandler(this.listBoxPhysicalQuantities_SelectedIndexChanged);
         // 
         // UnitConverterForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(358, 243);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "UnitConverterForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Unit Converter";
         this.panel.ResumeLayout(false);
         this.groupBoxSecondUnit.ResumeLayout(false);
         this.groupBoxFirstUnit.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void listBoxPhysicalQuantities_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         this.inConstruction = true;
         string physicalQuantityStr = (string)this.listBoxPhysicalQuantities.SelectedItem;
         PhysicalQuantity physicalQuantity = UnitSystemService.GetInstance().GetPhysicalQuantityAsEnum(physicalQuantityStr);

         this.textBoxFirstUnit.Text = "1";
         this.textBoxSecondUnit.Text = "1";
         if (physicalQuantity == PhysicalQuantity.Area) 
         {
            this.InitializeComboBoxWithAreaUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithAreaUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Density) 
         {
            this.InitializeComboBoxWithDensityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithDensityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Diffusivity) 
         {
            this.InitializeComboBoxWithDiffusivityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithDiffusivityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.DynamicViscosity) 
         {
            this.InitializeComboBoxWithDynamicViscosityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithDynamicViscosityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Energy) 
         {
            this.InitializeComboBoxWithEnergyUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithEnergyUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.FoulingFactor) 
         {
            this.InitializeComboBoxWithFoulingFactorUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithFoulingFactorUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Fraction) 
         {
            this.InitializeComboBoxWithFractionUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithFractionUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.HeatTransferCoefficient) 
         {
            this.InitializeComboBoxWithHeatTransferCoefficientUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithHeatTransferCoefficientUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.KinematicViscosity) 
         {
            this.InitializeComboBoxWithKinematicViscosityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithKinematicViscosityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Length) 
         {
            this.InitializeComboBoxWithLengthUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithLengthUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.LiquidHead) 
         {
            this.InitializeComboBoxWithLiquidHeadUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithLiquidHeadUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Mass) 
         {
            this.InitializeComboBoxWithMassUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMassUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.MassFlowRate) 
         {
            this.InitializeComboBoxWithMassFlowRateUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMassFlowRateUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.MassVolumeConcentration) 
         {
            this.InitializeComboBoxWithMassVolumeConcentrationUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMassVolumeConcentrationUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.MicroLength) 
         {
            this.InitializeComboBoxWithMicroLengthUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMicroLengthUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.MoistureContent) 
         {
            this.InitializeComboBoxWithMoistureContentUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMoistureContentUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.PlaneAngle) 
         {
            this.InitializeComboBoxWithPlaneAngleUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithPlaneAngleUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Power) 
         {
            this.InitializeComboBoxWithPowerUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithPowerUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Pressure) 
         {
            this.InitializeComboBoxWithPressureUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithPressureUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.SmallLength) 
         {
            this.InitializeComboBoxWithSmallLengthUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithSmallLengthUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificHeat) 
         {
            this.InitializeComboBoxWithSpecificHeatUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithSpecificHeatUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificEnergy) 
         {
            this.InitializeComboBoxWithSpecificEnergyUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithSpecificEnergyUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificVolume) 
         {
            this.InitializeComboBoxWithSpecificVolumeUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithSpecificVolumeUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.SurfaceTension) 
         {
            this.InitializeComboBoxWithSurfaceTensionUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithSurfaceTensionUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Temperature) 
         {
            this.InitializeComboBoxWithTemperatureUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithTemperatureUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.ThermalConductivity) 
         {
            this.InitializeComboBoxWithThermalConductivityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithThermalConductivityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Time) 
         {
            this.InitializeComboBoxWithTimeUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithTimeUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Velocity) 
         {
            this.InitializeComboBoxWithVelocityUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVelocityUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.Volume) 
         {
            this.InitializeComboBoxWithVolumeUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVolumeUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeFlowRate) 
         {
            this.InitializeComboBoxWithVolumeFlowRateUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVolumeFlowRateUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeRateFlowGases) 
         {
            this.InitializeComboBoxWithVolumeRateFlowGasesUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVolumeRateFlowGasesUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeRateFlowLiquids)
         {
            this.InitializeComboBoxWithVolumeRateFlowLiquidsUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVolumeRateFlowLiquidsUnits(this.comboBoxSecondUnit);
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeHeatTransferCoefficient)
         {
            this.InitializeComboBoxWithVolumeHeatTransferCoefficientUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithVolumeHeatTransferCoefficientUnits(this.comboBoxSecondUnit);
         }
         this.inConstruction = false;
      }

      private void InitializeComboBoxWithAreaUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = AreaUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = AreaUnit.GetUnitAsString(us.AreaUnitType);
      }

      private void InitializeComboBoxWithDensityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DensityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DensityUnit.GetUnitAsString(us.DensityUnitType);
      }

      private void InitializeComboBoxWithDiffusivityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DiffusivityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DiffusivityUnit.GetUnitAsString(us.DiffusivityUnitType);
      }

      private void InitializeComboBoxWithDynamicViscosityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DynamicViscosityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DynamicViscosityUnit.GetUnitAsString(us.DynamicViscosityUnitType);
      }

      private void InitializeComboBoxWithEnergyUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = EnergyUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = EnergyUnit.GetUnitAsString(us.EnergyUnitType);
      }

      private void InitializeComboBoxWithFoulingFactorUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = FoulingFactorUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = FoulingFactorUnit.GetUnitAsString(us.FoulingFactorUnitType);
      }

      private void InitializeComboBoxWithFractionUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = FractionUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = FractionUnit.GetUnitAsString(us.FractionUnitType);
      }

      private void InitializeComboBoxWithHeatTransferCoefficientUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = HeatTransferCoefficientUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = HeatTransferCoefficientUnit.GetUnitAsString(us.HeatTransferCoefficientUnitType);
      }

      private void InitializeComboBoxWithKinematicViscosityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = KinematicViscosityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = KinematicViscosityUnit.GetUnitAsString(us.KinematicViscosityUnitType);
      }

      private void InitializeComboBoxWithLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = LengthUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = LengthUnit.GetUnitAsString(us.LengthUnitType);
      }

      private void InitializeComboBoxWithLiquidHeadUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = LiquidHeadUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = LiquidHeadUnit.GetUnitAsString(us.LiquidHeadUnitType);
      }

      private void InitializeComboBoxWithMassUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassUnit.GetUnitAsString(us.MassUnitType);
      }

      private void InitializeComboBoxWithMassFlowRateUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassFlowRateUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassFlowRateUnit.GetUnitAsString(us.MassFlowRateUnitType);
      }

      private void InitializeComboBoxWithMassVolumeConcentrationUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassVolumeConcentrationUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassVolumeConcentrationUnit.GetUnitAsString(us.MassVolumeConcentrationUnitType);
      }

      private void InitializeComboBoxWithMicroLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MicroLengthUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MicroLengthUnit.GetUnitAsString(us.MicroLengthUnitType);
      }

      private void InitializeComboBoxWithMoistureContentUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MoistureContentUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MoistureContentUnit.GetUnitAsString(us.MoistureContentUnitType);
      }

      private void InitializeComboBoxWithPlaneAngleUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PlaneAngleUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PlaneAngleUnit.GetUnitAsString(us.PlaneAngleUnitType);
      }

      private void InitializeComboBoxWithPowerUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PowerUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PowerUnit.GetUnitAsString(us.PowerUnitType);
      }

      private void InitializeComboBoxWithPressureUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PressureUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PressureUnit.GetUnitAsString(us.PressureUnitType);
      }

      private void InitializeComboBoxWithSmallLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SmallLengthUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SmallLengthUnit.GetUnitAsString(us.SmallLengthUnitType);
      }

      private void InitializeComboBoxWithSpecificHeatUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificHeatUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificHeatUnit.GetUnitAsString(us.SpecificHeatUnitType);
      }

      private void InitializeComboBoxWithSpecificEnergyUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificEnergyUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificEnergyUnit.GetUnitAsString(us.SpecificEnergyUnitType);
      }

      private void InitializeComboBoxWithSpecificVolumeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificVolumeUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificVolumeUnit.GetUnitAsString(us.SpecificVolumeUnitType);
      }

      private void InitializeComboBoxWithSurfaceTensionUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SurfaceTensionUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SurfaceTensionUnit.GetUnitAsString(us.SurfaceTensionUnitType);
      }

      private void InitializeComboBoxWithTemperatureUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = TemperatureUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = TemperatureUnit.GetUnitAsString(us.TemperatureUnitType);
      }

      private void InitializeComboBoxWithThermalConductivityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = ThermalConductivityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = ThermalConductivityUnit.GetUnitAsString(us.ThermalConductivityUnitType);
      }

      private void InitializeComboBoxWithTimeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = TimeUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = TimeUnit.GetUnitAsString(us.TimeUnitType);
      }

      private void InitializeComboBoxWithVelocityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VelocityUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VelocityUnit.GetUnitAsString(us.VelocityUnitType);
      }

      private void InitializeComboBoxWithVolumeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeUnit.GetUnitAsString(us.VolumeUnitType);
      }

      private void InitializeComboBoxWithVolumeFlowRateUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeFlowRateUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeFlowRateUnit.GetUnitAsString(us.VolumeFlowRateUnitType);
      }

      private void InitializeComboBoxWithVolumeRateFlowGasesUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeRateFlowGasesUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeRateFlowGasesUnit.GetUnitAsString(us.VolumeRateFlowGasesUnitType);
      }

      private void InitializeComboBoxWithVolumeRateFlowLiquidsUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeRateFlowLiquidsUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeRateFlowLiquidsUnit.GetUnitAsString(us.VolumeRateFlowLiquidsUnitType);
      }
      
      private void InitializeComboBoxWithVolumeHeatTransferCoefficientUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeHeatTransferCoefficientUnit.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeHeatTransferCoefficientUnit.GetUnitAsString(us.VolumeHeatTransferCoefficientUnitType);
      }

      private void textBoxFirstUnit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            string message = "Please enter a numeric value!";
            if (this.textBoxFirstUnit.Text.Trim().Equals(""))
            {
               MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               try
               {
                  // we need to recalculate the second unit value
                  this.CalculateSecondUnitValue();
               }
               catch (FormatException)
               {
                  e.Cancel = true;
                  MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
         }
      }

      private void textBoxSecondUnit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!this.inConstruction)
         {
            string message = "Please enter a numeric value!";
            if (this.textBoxSecondUnit.Text.Trim().Equals(""))
            {
               MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               try
               {
                  // we need to recalculate the first unit value
                  this.CalculateFirstUnitValue();
               }
               catch (FormatException)
               {
                  e.Cancel = true;
                  MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
         }
      }

      private void textBoxFirstUnit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = this.listBoxPhysicalQuantities;
            this.ActiveControl = this.textBoxFirstUnit;
         }
      }

      private void textBoxSecondUnit_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = this.listBoxPhysicalQuantities;
            this.ActiveControl = this.textBoxSecondUnit;
         }
      }

      private void comboBoxFirstUnit_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            // we need to recalculate the first unit value
            this.CalculateFirstUnitValue();
         }
      }

      private void comboBoxSecondUnit_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            // we need to recalculate the second unit value
            this.CalculateSecondUnitValue();
         }
      }

      private void CalculateFirstUnitValue()
      {
         string inValStr = this.textBoxSecondUnit.Text;
         string inUnit = (string)this.comboBoxSecondUnit.SelectedItem;
         string outUnit = (string)this.comboBoxFirstUnit.SelectedItem;
         this.textBoxFirstUnit.Text = this.Convert(inValStr, inUnit, outUnit);
      }

      private void CalculateSecondUnitValue()
      {
         string inValStr = this.textBoxFirstUnit.Text;
         string inUnit = (string)this.comboBoxFirstUnit.SelectedItem;
         string outUnit = (string)this.comboBoxSecondUnit.SelectedItem;
         this.textBoxSecondUnit.Text = this.Convert(inValStr, inUnit, outUnit);
      }

      private string Convert(string inValStr, string inUnit, string outUnit)
      {
         string outValStr = "?";
         string physicalQuantityStr = (string)this.listBoxPhysicalQuantities.SelectedItem;
         PhysicalQuantity physicalQuantity = UnitSystemService.GetInstance().GetPhysicalQuantityAsEnum(physicalQuantityStr);
         if (physicalQuantity == PhysicalQuantity.Area) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertArea(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Density) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertDensity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Diffusivity) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertDiffusivity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.DynamicViscosity) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertDynamicViscosity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Energy) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertEnergy(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.FoulingFactor) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertFoulingFactor(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Fraction) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertFraction(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.HeatFlux) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertHeatFlux(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.HeatTransferCoefficient) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertHeatTransferCoefficient(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.KinematicViscosity) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertKinematicViscosity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Length) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertLength(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.LiquidHead) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertLiquidHead(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Mass) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMass(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.MassFlowRate) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMassFlowRate(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.MassVolumeConcentration) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMassVolumeConcentration(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.MicroLength) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMicroLength(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.MoistureContent) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMoistureContent(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.PlaneAngle) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertPlaneAngle(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Power) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertPower(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Pressure) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertPressure(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.SmallLength) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertSmallLength(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificHeat) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertSpecificHeat(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificEnergy) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertSpecificEnergy(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.SpecificVolume) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertSpecificVolume(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.SurfaceTension) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertSurfaceTension(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Temperature) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertTemperature(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.ThermalConductivity) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertThermalConductivity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Time) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertTime(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Velocity) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVelocity(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.Volume) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVolume(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeFlowRate) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVolumeFlowRate(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeRateFlowGases) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVolumeRateFlowGases(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeRateFlowLiquids) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVolumeRateFlowLiquids(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         else if (physicalQuantity == PhysicalQuantity.VolumeHeatTransferCoefficient) 
         {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertVolumeHeatTransferCoefficient(inVal, inUnit, outUnit);
            outValStr = outVal.ToString();
         }
         return outValStr;
      }

      private double ConvertArea(double inValue, string inUnit, string outUnit)
      {
         AreaUnitType inAu = AreaUnit.GetUnitAsEnum(inUnit);
         AreaUnitType outAu = AreaUnit.GetUnitAsEnum(outUnit);
         double siValue = AreaUnit.ConvertToSIValue(inAu, inValue);
         double outValue = AreaUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertDensity(double inValue, string inUnit, string outUnit)
      {
         DensityUnitType inAu = DensityUnit.GetUnitAsEnum(inUnit);
         DensityUnitType outAu = DensityUnit.GetUnitAsEnum(outUnit);
         double siValue = DensityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = DensityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertDiffusivity(double inValue, string inUnit, string outUnit)
      {
         DiffusivityUnitType inAu = DiffusivityUnit.GetUnitAsEnum(inUnit);
         DiffusivityUnitType outAu = DiffusivityUnit.GetUnitAsEnum(outUnit);
         double siValue = DiffusivityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = DiffusivityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertDynamicViscosity(double inValue, string inUnit, string outUnit)
      {
         DynamicViscosityUnitType inAu = DynamicViscosityUnit.GetUnitAsEnum(inUnit);
         DynamicViscosityUnitType outAu = DynamicViscosityUnit.GetUnitAsEnum(outUnit);
         double siValue = DynamicViscosityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = DynamicViscosityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertEnergy(double inValue, string inUnit, string outUnit)
      {
         EnergyUnitType inAu = EnergyUnit.GetUnitAsEnum(inUnit);
         EnergyUnitType outAu = EnergyUnit.GetUnitAsEnum(outUnit);
         double siValue = EnergyUnit.ConvertToSIValue(inAu, inValue);
         double outValue = EnergyUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertFoulingFactor(double inValue, string inUnit, string outUnit)
      {
         FoulingFactorUnitType inAu = FoulingFactorUnit.GetUnitAsEnum(inUnit);
         FoulingFactorUnitType outAu = FoulingFactorUnit.GetUnitAsEnum(outUnit);
         double siValue = FoulingFactorUnit.ConvertToSIValue(inAu, inValue);
         double outValue = FoulingFactorUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertFraction(double inValue, string inUnit, string outUnit)
      {
         FractionUnitType inAu = FractionUnit.GetUnitAsEnum(inUnit);
         FractionUnitType outAu = FractionUnit.GetUnitAsEnum(outUnit);
         double siValue = FractionUnit.ConvertToSIValue(inAu, inValue);
         double outValue = FractionUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertHeatFlux(double inValue, string inUnit, string outUnit)
      {
         HeatFluxUnitType inAu = HeatFluxUnit.GetUnitAsEnum(inUnit);
         HeatFluxUnitType outAu = HeatFluxUnit.GetUnitAsEnum(outUnit);
         double siValue = HeatFluxUnit.ConvertToSIValue(inAu, inValue);
         double outValue = HeatFluxUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertHeatTransferCoefficient(double inValue, string inUnit, string outUnit)
      {
         HeatTransferCoefficientUnitType inAu = HeatTransferCoefficientUnit.GetUnitAsEnum(inUnit);
         HeatTransferCoefficientUnitType outAu = HeatTransferCoefficientUnit.GetUnitAsEnum(outUnit);
         double siValue = HeatTransferCoefficientUnit.ConvertToSIValue(inAu, inValue);
         double outValue = HeatTransferCoefficientUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertKinematicViscosity(double inValue, string inUnit, string outUnit)
      {
         KinematicViscosityUnitType inAu = KinematicViscosityUnit.GetUnitAsEnum(inUnit);
         KinematicViscosityUnitType outAu = KinematicViscosityUnit.GetUnitAsEnum(outUnit);
         double siValue = KinematicViscosityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = KinematicViscosityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertLength(double inValue, string inUnit, string outUnit)
      {
         LengthUnitType inAu = LengthUnit.GetUnitAsEnum(inUnit);
         LengthUnitType outAu = LengthUnit.GetUnitAsEnum(outUnit);
         double siValue = LengthUnit.ConvertToSIValue(inAu, inValue);
         double outValue = LengthUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertLiquidHead(double inValue, string inUnit, string outUnit)
      {
         LiquidHeadUnitType inAu = LiquidHeadUnit.GetUnitAsEnum(inUnit);
         LiquidHeadUnitType outAu = LiquidHeadUnit.GetUnitAsEnum(outUnit);
         double siValue = LiquidHeadUnit.ConvertToSIValue(inAu, inValue);
         double outValue = LiquidHeadUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertMass(double inValue, string inUnit, string outUnit)
      {
         MassUnitType inAu = MassUnit.GetUnitAsEnum(inUnit);
         MassUnitType outAu = MassUnit.GetUnitAsEnum(outUnit);
         double siValue = MassUnit.ConvertToSIValue(inAu, inValue);
         double outValue = MassUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertMassFlowRate(double inValue, string inUnit, string outUnit)
      {
         MassFlowRateUnitType inAu = MassFlowRateUnit.GetUnitAsEnum(inUnit);
         MassFlowRateUnitType outAu = MassFlowRateUnit.GetUnitAsEnum(outUnit);
         double siValue = MassFlowRateUnit.ConvertToSIValue(inAu, inValue);
         double outValue = MassFlowRateUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertMassVolumeConcentration(double inValue, string inUnit, string outUnit)
      {
         MassVolumeConcentrationUnitType inAu = MassVolumeConcentrationUnit.GetUnitAsEnum(inUnit);
         MassVolumeConcentrationUnitType outAu = MassVolumeConcentrationUnit.GetUnitAsEnum(outUnit);
         double siValue = MassVolumeConcentrationUnit.ConvertToSIValue(inAu, inValue);
         double outValue = MassVolumeConcentrationUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertMicroLength(double inValue, string inUnit, string outUnit)
      {
         MicroLengthUnitType inAu = MicroLengthUnit.GetUnitAsEnum(inUnit);
         MicroLengthUnitType outAu = MicroLengthUnit.GetUnitAsEnum(outUnit);
         double siValue = MicroLengthUnit.ConvertToSIValue(inAu, inValue);
         double outValue = MicroLengthUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertMoistureContent(double inValue, string inUnit, string outUnit)
      {
         MoistureContentUnitType inAu = MoistureContentUnit.GetUnitAsEnum(inUnit);
         MoistureContentUnitType outAu = MoistureContentUnit.GetUnitAsEnum(outUnit);
         double siValue = MoistureContentUnit.ConvertToSIValue(inAu, inValue);
         double outValue = MoistureContentUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertPlaneAngle(double inValue, string inUnit, string outUnit)
      {
         PlaneAngleUnitType inAu = PlaneAngleUnit.GetUnitAsEnum(inUnit);
         PlaneAngleUnitType outAu = PlaneAngleUnit.GetUnitAsEnum(outUnit);
         double siValue = PlaneAngleUnit.ConvertToSIValue(inAu, inValue);
         double outValue = PlaneAngleUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertPower(double inValue, string inUnit, string outUnit)
      {
         PowerUnitType inAu = PowerUnit.GetUnitAsEnum(inUnit);
         PowerUnitType outAu = PowerUnit.GetUnitAsEnum(outUnit);
         double siValue = PowerUnit.ConvertToSIValue(inAu, inValue);
         double outValue = PowerUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertPressure(double inValue, string inUnit, string outUnit)
      {
         PressureUnitType inAu = PressureUnit.GetUnitAsEnum(inUnit);
         PressureUnitType outAu = PressureUnit.GetUnitAsEnum(outUnit);
         double siValue = PressureUnit.ConvertToSIValue(inAu, inValue);
         double outValue = PressureUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertSmallLength(double inValue, string inUnit, string outUnit)
      {
         SmallLengthUnitType inAu = SmallLengthUnit.GetUnitAsEnum(inUnit);
         SmallLengthUnitType outAu = SmallLengthUnit.GetUnitAsEnum(outUnit);
         double siValue = SmallLengthUnit.ConvertToSIValue(inAu, inValue);
         double outValue = SmallLengthUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificHeat(double inValue, string inUnit, string outUnit)
      {
         SpecificHeatUnitType inAu = SpecificHeatUnit.GetUnitAsEnum(inUnit);
         SpecificHeatUnitType outAu = SpecificHeatUnit.GetUnitAsEnum(outUnit);
         double siValue = SpecificHeatUnit.ConvertToSIValue(inAu, inValue);
         double outValue = SpecificHeatUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificEnergy(double inValue, string inUnit, string outUnit)
      {
         SpecificEnergyUnitType inAu = SpecificEnergyUnit.GetUnitAsEnum(inUnit);
         SpecificEnergyUnitType outAu = SpecificEnergyUnit.GetUnitAsEnum(outUnit);
         double siValue = SpecificEnergyUnit.ConvertToSIValue(inAu, inValue);
         double outValue = SpecificEnergyUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificVolume(double inValue, string inUnit, string outUnit)
      {
         SpecificVolumeUnitType inAu = SpecificVolumeUnit.GetUnitAsEnum(inUnit);
         SpecificVolumeUnitType outAu = SpecificVolumeUnit.GetUnitAsEnum(outUnit);
         double siValue = SpecificVolumeUnit.ConvertToSIValue(inAu, inValue);
         double outValue = SpecificVolumeUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertSurfaceTension(double inValue, string inUnit, string outUnit)
      {
         SurfaceTensionUnitType inAu = SurfaceTensionUnit.GetUnitAsEnum(inUnit);
         SurfaceTensionUnitType outAu = SurfaceTensionUnit.GetUnitAsEnum(outUnit);
         double siValue = SurfaceTensionUnit.ConvertToSIValue(inAu, inValue);
         double outValue = SurfaceTensionUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertTemperature(double inValue, string inUnit, string outUnit)
      {
         TemperatureUnitType inAu = TemperatureUnit.GetUnitAsEnum(inUnit);
         TemperatureUnitType outAu = TemperatureUnit.GetUnitAsEnum(outUnit);
         double siValue = TemperatureUnit.ConvertToSIValue(inAu, inValue);
         double outValue = TemperatureUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertThermalConductivity(double inValue, string inUnit, string outUnit)
      {
         ThermalConductivityUnitType inAu = ThermalConductivityUnit.GetUnitAsEnum(inUnit);
         ThermalConductivityUnitType outAu = ThermalConductivityUnit.GetUnitAsEnum(outUnit);
         double siValue = ThermalConductivityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = ThermalConductivityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertTime(double inValue, string inUnit, string outUnit)
      {
         TimeUnitType inAu = TimeUnit.GetUnitAsEnum(inUnit);
         TimeUnitType outAu = TimeUnit.GetUnitAsEnum(outUnit);
         double siValue = TimeUnit.ConvertToSIValue(inAu, inValue);
         double outValue = TimeUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVelocity(double inValue, string inUnit, string outUnit)
      {
         VelocityUnitType inAu = VelocityUnit.GetUnitAsEnum(inUnit);
         VelocityUnitType outAu = VelocityUnit.GetUnitAsEnum(outUnit);
         double siValue = VelocityUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VelocityUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVolume(double inValue, string inUnit, string outUnit)
      {
         VolumeUnitType inAu = VolumeUnit.GetUnitAsEnum(inUnit);
         VolumeUnitType outAu = VolumeUnit.GetUnitAsEnum(outUnit);
         double siValue = VolumeUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VolumeUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeFlowRate(double inValue, string inUnit, string outUnit)
      {
         VolumeFlowRateUnitType inAu = VolumeFlowRateUnit.GetUnitAsEnum(inUnit);
         VolumeFlowRateUnitType outAu = VolumeFlowRateUnit.GetUnitAsEnum(outUnit);
         double siValue = VolumeFlowRateUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VolumeFlowRateUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeRateFlowGases(double inValue, string inUnit, string outUnit)
      {
         VolumeRateFlowGasesUnitType inAu = VolumeRateFlowGasesUnit.GetUnitAsEnum(inUnit);
         VolumeRateFlowGasesUnitType outAu = VolumeRateFlowGasesUnit.GetUnitAsEnum(outUnit);
         double siValue = VolumeRateFlowGasesUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VolumeRateFlowGasesUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeRateFlowLiquids(double inValue, string inUnit, string outUnit)
      {
         VolumeRateFlowLiquidsUnitType inAu = VolumeRateFlowLiquidsUnit.GetUnitAsEnum(inUnit);
         VolumeRateFlowLiquidsUnitType outAu = VolumeRateFlowLiquidsUnit.GetUnitAsEnum(outUnit);
         double siValue = VolumeRateFlowLiquidsUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VolumeRateFlowLiquidsUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeHeatTransferCoefficient(double inValue, string inUnit, string outUnit)
      {
         VolumeHeatTransferCoefficientUnitType inAu = VolumeHeatTransferCoefficientUnit.GetUnitAsEnum(inUnit);
         VolumeHeatTransferCoefficientUnitType outAu = VolumeHeatTransferCoefficientUnit.GetUnitAsEnum(outUnit);
         double siValue = VolumeHeatTransferCoefficientUnit.ConvertToSIValue(inAu, inValue);
         double outValue = VolumeHeatTransferCoefficientUnit.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }
   }
}
