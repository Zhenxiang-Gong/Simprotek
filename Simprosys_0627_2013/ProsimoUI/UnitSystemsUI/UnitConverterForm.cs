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
      private MainForm mainForm;

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
      private IContainer components;

		public UnitConverterForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public UnitConverterForm(MainForm mainForm)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.mainForm = mainForm;
         ArrayList physicalQuantities = new ArrayList(UnitSystemService.GetInstance().GetPhysicalQuantitiesAsStrings());
         physicalQuantities.Sort();
         IEnumerator e = physicalQuantities.GetEnumerator();
         while (e.MoveNext())
         {
            this.listBoxPhysicalQuantities.Items.Add(e.Current);
         }
         this.listBoxPhysicalQuantities.SelectedIndex = 0;
         this.ResizeEnd += new EventHandler(UnitConverterForm_ResizeEnd);
      }

      void UnitConverterForm_ResizeEnd(object sender, EventArgs e)
      {
         if (this.mainForm.Flowsheet != null)
         {
            this.mainForm.Flowsheet.ConnectionManager.DrawConnections();
         }
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
         this.components = new System.ComponentModel.Container();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
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
         this.panel.Size = new System.Drawing.Size(382, 487);
         this.panel.TabIndex = 0;
         // 
         // groupBoxSecondUnit
         // 
         this.groupBoxSecondUnit.Controls.Add(this.comboBoxSecondUnit);
         this.groupBoxSecondUnit.Controls.Add(this.textBoxSecondUnit);
         this.groupBoxSecondUnit.Location = new System.Drawing.Point(224, 140);
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
         this.textBoxSecondUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxSecondUnit_KeyUp);
         this.textBoxSecondUnit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSecondUnit_Validating);
         // 
         // groupBoxFirstUnit
         // 
         this.groupBoxFirstUnit.Controls.Add(this.comboBoxFirstUnit);
         this.groupBoxFirstUnit.Controls.Add(this.textBoxFirstUnit);
         this.groupBoxFirstUnit.Location = new System.Drawing.Point(224, 32);
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
         this.textBoxFirstUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFirstUnit_KeyUp);
         this.textBoxFirstUnit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFirstUnit_Validating);
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
         this.listBoxPhysicalQuantities.Size = new System.Drawing.Size(206, 446);
         this.listBoxPhysicalQuantities.TabIndex = 0;
         this.listBoxPhysicalQuantities.SelectedIndexChanged += new System.EventHandler(this.listBoxPhysicalQuantities_SelectedIndexChanged);
         // 
         // UnitConverterForm
         // 
         this.ClientSize = new System.Drawing.Size(382, 487);
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
         this.groupBoxSecondUnit.PerformLayout();
         this.groupBoxFirstUnit.ResumeLayout(false);
         this.groupBoxFirstUnit.PerformLayout();
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
         else if (physicalQuantity == PhysicalQuantity.MoleFlowRate) {
            this.InitializeComboBoxWithMoleFlowRateUnits(this.comboBoxFirstUnit);
            this.InitializeComboBoxWithMoleFlowRateUnits(this.comboBoxSecondUnit);
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
         IEnumerator e = AreaUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = AreaUnit.Instance.GetUnitAsString(us.AreaUnitType);
      }

      private void InitializeComboBoxWithDensityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DensityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DensityUnit.Instance.GetUnitAsString(us.DensityUnitType);
      }

      private void InitializeComboBoxWithDiffusivityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DiffusivityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DiffusivityUnit.Instance.GetUnitAsString(us.DiffusivityUnitType);
      }

      private void InitializeComboBoxWithDynamicViscosityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = DynamicViscosityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = DynamicViscosityUnit.Instance.GetUnitAsString(us.DynamicViscosityUnitType);
      }

      private void InitializeComboBoxWithEnergyUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = EnergyUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = EnergyUnit.Instance.GetUnitAsString(us.EnergyUnitType);
      }

      private void InitializeComboBoxWithFoulingFactorUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = FoulingFactorUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = FoulingFactorUnit.Instance.GetUnitAsString(us.FoulingFactorUnitType);
      }

      private void InitializeComboBoxWithFractionUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = FractionUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = FractionUnit.Instance.GetUnitAsString(us.FractionUnitType);
      }

      private void InitializeComboBoxWithHeatTransferCoefficientUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = HeatTransferCoefficientUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = HeatTransferCoefficientUnit.Instance.GetUnitAsString(us.HeatTransferCoefficientUnitType);
      }

      private void InitializeComboBoxWithKinematicViscosityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = KinematicViscosityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = KinematicViscosityUnit.Instance.GetUnitAsString(us.KinematicViscosityUnitType);
      }

      private void InitializeComboBoxWithLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = LengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = LengthUnit.Instance.GetUnitAsString(us.LengthUnitType);
      }

      private void InitializeComboBoxWithLiquidHeadUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = LiquidHeadUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = LiquidHeadUnit.Instance.GetUnitAsString(us.LiquidHeadUnitType);
      }

      private void InitializeComboBoxWithMassUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassUnit.Instance.GetUnitAsString(us.MassUnitType);
      }

      private void InitializeComboBoxWithMassFlowRateUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassFlowRateUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassFlowRateUnit.Instance.GetUnitAsString(us.MassFlowRateUnitType);
      }

      private void InitializeComboBoxWithMoleFlowRateUnits(ComboBox cb) {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MoleFlowRateUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext()) {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MoleFlowRateUnit.Instance.GetUnitAsString(us.MoleFlowRateUnitType);
      }
      
      private void InitializeComboBoxWithMassVolumeConcentrationUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MassVolumeConcentrationUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MassVolumeConcentrationUnit.Instance.GetUnitAsString(us.MassVolumeConcentrationUnitType);
      }

      private void InitializeComboBoxWithMicroLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MicroLengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MicroLengthUnit.Instance.GetUnitAsString(us.MicroLengthUnitType);
      }

      private void InitializeComboBoxWithMoistureContentUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = MoistureContentUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = MoistureContentUnit.Instance.GetUnitAsString(us.MoistureContentUnitType);
      }

      private void InitializeComboBoxWithPlaneAngleUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PlaneAngleUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PlaneAngleUnit.Instance.GetUnitAsString(us.PlaneAngleUnitType);
      }

      private void InitializeComboBoxWithPowerUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PowerUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PowerUnit.Instance.GetUnitAsString(us.PowerUnitType);
      }

      private void InitializeComboBoxWithPressureUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = PressureUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = PressureUnit.Instance.GetUnitAsString(us.PressureUnitType);
      }

      private void InitializeComboBoxWithSmallLengthUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SmallLengthUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SmallLengthUnit.Instance.GetUnitAsString(us.SmallLengthUnitType);
      }

      private void InitializeComboBoxWithSpecificHeatUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificHeatUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificHeatUnit.Instance.GetUnitAsString(us.SpecificHeatUnitType);
      }

      private void InitializeComboBoxWithSpecificEnergyUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificEnergyUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificEnergyUnit.Instance.GetUnitAsString(us.SpecificEnergyUnitType);
      }

      private void InitializeComboBoxWithSpecificVolumeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SpecificVolumeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SpecificVolumeUnit.Instance.GetUnitAsString(us.SpecificVolumeUnitType);
      }

      private void InitializeComboBoxWithSurfaceTensionUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = SurfaceTensionUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = SurfaceTensionUnit.Instance.GetUnitAsString(us.SurfaceTensionUnitType);
      }

      private void InitializeComboBoxWithTemperatureUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = TemperatureUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = TemperatureUnit.Instance.GetUnitAsString(us.TemperatureUnitType);
      }

      private void InitializeComboBoxWithThermalConductivityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = ThermalConductivityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = ThermalConductivityUnit.Instance.GetUnitAsString(us.ThermalConductivityUnitType);
      }

      private void InitializeComboBoxWithTimeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = TimeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = TimeUnit.Instance.GetUnitAsString(us.TimeUnitType);
      }

      private void InitializeComboBoxWithVelocityUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VelocityUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VelocityUnit.Instance.GetUnitAsString(us.VelocityUnitType);
      }

      private void InitializeComboBoxWithVolumeUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeUnit.Instance.GetUnitAsString(us.VolumeUnitType);
      }

      private void InitializeComboBoxWithVolumeFlowRateUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeFlowRateUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeFlowRateUnit.Instance.GetUnitAsString(us.VolumeFlowRateUnitType);
      }

      private void InitializeComboBoxWithVolumeRateFlowGasesUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeRateFlowGasesUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeRateFlowGasesUnit.Instance.GetUnitAsString(us.VolumeRateFlowGasesUnitType);
      }

      private void InitializeComboBoxWithVolumeRateFlowLiquidsUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeRateFlowLiquidsUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsString(us.VolumeRateFlowLiquidsUnitType);
      }
      
      private void InitializeComboBoxWithVolumeHeatTransferCoefficientUnits(ComboBox cb)
      {
         cb.Items.Clear();
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         IEnumerator e = VolumeHeatTransferCoefficientUnit.Instance.GetUnitsAsStrings().GetEnumerator();
         while (e.MoveNext())
         {
            cb.Items.Add(e.Current);
         }
         cb.SelectedItem = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsString(us.VolumeHeatTransferCoefficientUnitType);
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
         else if (physicalQuantity == PhysicalQuantity.MoleFlowRate) {
            double inVal = System.Convert.ToDouble(inValStr);
            double outVal = this.ConvertMoleFlowRate(inVal, inUnit, outUnit);
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
         AreaUnitType inAu = AreaUnit.Instance.GetUnitAsEnum<AreaUnitType>(inUnit);
         AreaUnitType outAu = AreaUnit.Instance.GetUnitAsEnum<AreaUnitType>(outUnit);
         double siValue = AreaUnit.Instance.ConvertToSIValue<AreaUnitType>(inAu, inValue);
         double outValue = AreaUnit.Instance.ConvertFromSIValue<AreaUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertDensity(double inValue, string inUnit, string outUnit)
      {
         DensityUnitType inAu = DensityUnit.Instance.GetUnitAsEnum<DensityUnitType>(inUnit);
         DensityUnitType outAu = DensityUnit.Instance.GetUnitAsEnum<DensityUnitType>(outUnit);
         double siValue = DensityUnit.Instance.ConvertToSIValue<DensityUnitType>(inAu, inValue);
         double outValue = DensityUnit.Instance.ConvertFromSIValue<DensityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertDiffusivity(double inValue, string inUnit, string outUnit)
      {
         DiffusivityUnitType inAu = DiffusivityUnit.Instance.GetUnitAsEnum<DiffusivityUnitType>(inUnit);
         DiffusivityUnitType outAu = DiffusivityUnit.Instance.GetUnitAsEnum<DiffusivityUnitType>(outUnit);
         double siValue = DiffusivityUnit.Instance.ConvertToSIValue<DiffusivityUnitType>(inAu, inValue);
         double outValue = DiffusivityUnit.Instance.ConvertFromSIValue<DiffusivityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertDynamicViscosity(double inValue, string inUnit, string outUnit)
      {
         DynamicViscosityUnitType inAu = DynamicViscosityUnit.Instance.GetUnitAsEnum<DynamicViscosityUnitType>(inUnit);
         DynamicViscosityUnitType outAu = DynamicViscosityUnit.Instance.GetUnitAsEnum<DynamicViscosityUnitType>(outUnit);
         double siValue = DynamicViscosityUnit.Instance.ConvertToSIValue<DynamicViscosityUnitType>(inAu, inValue);
         double outValue = DynamicViscosityUnit.Instance.ConvertFromSIValue<DynamicViscosityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertEnergy(double inValue, string inUnit, string outUnit)
      {
         EnergyUnitType inAu = EnergyUnit.Instance.GetUnitAsEnum<EnergyUnitType>(inUnit);
         EnergyUnitType outAu = EnergyUnit.Instance.GetUnitAsEnum<EnergyUnitType>(outUnit);
         double siValue = EnergyUnit.Instance.ConvertToSIValue<EnergyUnitType>(inAu, inValue);
         double outValue = EnergyUnit.Instance.ConvertFromSIValue<EnergyUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertFoulingFactor(double inValue, string inUnit, string outUnit)
      {
         FoulingFactorUnitType inAu = FoulingFactorUnit.Instance.GetUnitAsEnum<FoulingFactorUnitType>(inUnit);
         FoulingFactorUnitType outAu = FoulingFactorUnit.Instance.GetUnitAsEnum<FoulingFactorUnitType>(outUnit);
         double siValue = FoulingFactorUnit.Instance.ConvertToSIValue<FoulingFactorUnitType>(inAu, inValue);
         double outValue = FoulingFactorUnit.Instance.ConvertFromSIValue<FoulingFactorUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertFraction(double inValue, string inUnit, string outUnit)
      {
         FractionUnitType inAu = FractionUnit.Instance.GetUnitAsEnum<FractionUnitType>(inUnit);
         FractionUnitType outAu = FractionUnit.Instance.GetUnitAsEnum<FractionUnitType>(outUnit);
         double siValue = FractionUnit.Instance.ConvertToSIValue<FractionUnitType>(inAu, inValue);
         double outValue = FractionUnit.Instance.ConvertFromSIValue<FractionUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertHeatFlux(double inValue, string inUnit, string outUnit)
      {
         HeatFluxUnitType inAu = HeatFluxUnit.Instance.GetUnitAsEnum<HeatFluxUnitType>(inUnit);
         HeatFluxUnitType outAu = HeatFluxUnit.Instance.GetUnitAsEnum<HeatFluxUnitType>(outUnit);
         double siValue = HeatFluxUnit.Instance.ConvertToSIValue<HeatFluxUnitType>(inAu, inValue);
         double outValue = HeatFluxUnit.Instance.ConvertFromSIValue<HeatFluxUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertHeatTransferCoefficient(double inValue, string inUnit, string outUnit)
      {
         HeatTransferCoefficientUnitType inAu = HeatTransferCoefficientUnit.Instance.GetUnitAsEnum<HeatTransferCoefficientUnitType>(inUnit);
         HeatTransferCoefficientUnitType outAu = HeatTransferCoefficientUnit.Instance.GetUnitAsEnum<HeatTransferCoefficientUnitType>(outUnit);
         double siValue = HeatTransferCoefficientUnit.Instance.ConvertToSIValue<HeatTransferCoefficientUnitType>(inAu, inValue);
         double outValue = HeatTransferCoefficientUnit.Instance.ConvertFromSIValue<HeatTransferCoefficientUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertKinematicViscosity(double inValue, string inUnit, string outUnit)
      {
         KinematicViscosityUnitType inAu = KinematicViscosityUnit.Instance.GetUnitAsEnum<KinematicViscosityUnitType>(inUnit);
         KinematicViscosityUnitType outAu = KinematicViscosityUnit.Instance.GetUnitAsEnum<KinematicViscosityUnitType>(outUnit);
         double siValue = KinematicViscosityUnit.Instance.ConvertToSIValue<KinematicViscosityUnitType>(inAu, inValue);
         double outValue = KinematicViscosityUnit.Instance.ConvertFromSIValue<KinematicViscosityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertLength(double inValue, string inUnit, string outUnit)
      {
         LengthUnitType inAu = LengthUnit.Instance.GetUnitAsEnum<LengthUnitType>(inUnit);
         LengthUnitType outAu = LengthUnit.Instance.GetUnitAsEnum<LengthUnitType>(outUnit);
         double siValue = LengthUnit.Instance.ConvertToSIValue<LengthUnitType>(inAu, inValue);
         double outValue = LengthUnit.Instance.ConvertFromSIValue<LengthUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertLiquidHead(double inValue, string inUnit, string outUnit)
      {
         LiquidHeadUnitType inAu = LiquidHeadUnit.Instance.GetUnitAsEnum<LiquidHeadUnitType>(inUnit);
         LiquidHeadUnitType outAu = LiquidHeadUnit.Instance.GetUnitAsEnum<LiquidHeadUnitType>(outUnit);
         double siValue = LiquidHeadUnit.Instance.ConvertToSIValue<LiquidHeadUnitType>(inAu, inValue);
         double outValue = LiquidHeadUnit.Instance.ConvertFromSIValue<LiquidHeadUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertMass(double inValue, string inUnit, string outUnit)
      {
         MassUnitType inAu = MassUnit.Instance.GetUnitAsEnum<MassUnitType>(inUnit);
         MassUnitType outAu = MassUnit.Instance.GetUnitAsEnum<MassUnitType>(outUnit);
         double siValue = MassUnit.Instance.ConvertToSIValue<MassUnitType>(inAu, inValue);
         double outValue = MassUnit.Instance.ConvertFromSIValue<MassUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertMassFlowRate(double inValue, string inUnit, string outUnit)
      {
         MassFlowRateUnitType inAu = MassFlowRateUnit.Instance.GetUnitAsEnum <MassFlowRateUnitType>(inUnit);
         MassFlowRateUnitType outAu = MassFlowRateUnit.Instance.GetUnitAsEnum <MassFlowRateUnitType>(outUnit);
         double siValue = MassFlowRateUnit.Instance.ConvertToSIValue<MassFlowRateUnitType>(inAu, inValue);
         double outValue = MassFlowRateUnit.Instance.ConvertFromSIValue<MassFlowRateUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertMoleFlowRate(double inValue, string inUnit, string outUnit) {
         MoleFlowRateUnitType inAu = MoleFlowRateUnit.Instance.GetUnitAsEnum<MoleFlowRateUnitType>(inUnit);
         MoleFlowRateUnitType outAu = MoleFlowRateUnit.Instance.GetUnitAsEnum<MoleFlowRateUnitType>(outUnit);
         double siValue = MoleFlowRateUnit.Instance.ConvertToSIValue<MoleFlowRateUnitType>(inAu, inValue);
         double outValue = MoleFlowRateUnit.Instance.ConvertFromSIValue<MoleFlowRateUnitType>(outAu, siValue);
         return outValue;
      }
      
      private double ConvertMassVolumeConcentration(double inValue, string inUnit, string outUnit)
      {
         MassVolumeConcentrationUnitType inAu = MassVolumeConcentrationUnit.Instance.GetUnitAsEnum<MassVolumeConcentrationUnitType>(inUnit);
         MassVolumeConcentrationUnitType outAu = MassVolumeConcentrationUnit.Instance.GetUnitAsEnum<MassVolumeConcentrationUnitType>(outUnit);
         double siValue = MassVolumeConcentrationUnit.Instance.ConvertToSIValue<MassVolumeConcentrationUnitType>(inAu, inValue);
         double outValue = MassVolumeConcentrationUnit.Instance.ConvertFromSIValue<MassVolumeConcentrationUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertMicroLength(double inValue, string inUnit, string outUnit)
      {
         MicroLengthUnitType inAu = MicroLengthUnit.Instance.GetUnitAsEnum<MicroLengthUnitType>(inUnit);
         MicroLengthUnitType outAu = MicroLengthUnit.Instance.GetUnitAsEnum<MicroLengthUnitType>(outUnit);
         double siValue = MicroLengthUnit.Instance.ConvertToSIValue<MicroLengthUnitType>(inAu, inValue);
         double outValue = MicroLengthUnit.Instance.ConvertFromSIValue<MicroLengthUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertMoistureContent(double inValue, string inUnit, string outUnit)
      {
         MoistureContentUnitType inAu = MoistureContentUnit.Instance.GetUnitAsEnum<MoistureContentUnitType>(inUnit);
         MoistureContentUnitType outAu = MoistureContentUnit.Instance.GetUnitAsEnum<MoistureContentUnitType>(outUnit);
         double siValue = MoistureContentUnit.Instance.ConvertToSIValue<MoistureContentUnitType>(inAu, inValue);
         double outValue = MoistureContentUnit.Instance.ConvertFromSIValue<MoistureContentUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertPlaneAngle(double inValue, string inUnit, string outUnit)
      {
         PlaneAngleUnitType inAu = PlaneAngleUnit.Instance.GetUnitAsEnum<PlaneAngleUnitType>(inUnit);
         PlaneAngleUnitType outAu = PlaneAngleUnit.Instance.GetUnitAsEnum<PlaneAngleUnitType>(outUnit);
         double siValue = PlaneAngleUnit.Instance.ConvertToSIValue<PlaneAngleUnitType>(inAu, inValue);
         double outValue = PlaneAngleUnit.Instance.ConvertFromSIValue<PlaneAngleUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertPower(double inValue, string inUnit, string outUnit)
      {
         PowerUnitType inAu = PowerUnit.Instance.GetUnitAsEnum<PowerUnitType>(inUnit);
         PowerUnitType outAu = PowerUnit.Instance.GetUnitAsEnum<PowerUnitType>(outUnit);
         double siValue = PowerUnit.Instance.ConvertToSIValue<PowerUnitType>(inAu, inValue);
         double outValue = PowerUnit.Instance.ConvertFromSIValue<PowerUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertPressure(double inValue, string inUnit, string outUnit)
      {
         PressureUnitType inAu = PressureUnit.Instance.GetUnitAsEnum<PressureUnitType>(inUnit);
         PressureUnitType outAu = PressureUnit.Instance.GetUnitAsEnum<PressureUnitType>(outUnit);
         double siValue = PressureUnit.Instance.ConvertToSIValue<PressureUnitType>(inAu, inValue);
         double outValue = PressureUnit.Instance.ConvertFromSIValue<PressureUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertSmallLength(double inValue, string inUnit, string outUnit)
      {
         SmallLengthUnitType inAu = SmallLengthUnit.Instance.GetUnitAsEnum<SmallLengthUnitType>(inUnit);
         SmallLengthUnitType outAu = SmallLengthUnit.Instance.GetUnitAsEnum<SmallLengthUnitType>(outUnit);
         double siValue = SmallLengthUnit.Instance.ConvertToSIValue<SmallLengthUnitType>(inAu, inValue);
         double outValue = SmallLengthUnit.Instance.ConvertFromSIValue<SmallLengthUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificHeat(double inValue, string inUnit, string outUnit)
      {
         SpecificHeatUnitType inAu = SpecificHeatUnit.Instance.GetUnitAsEnum<SpecificHeatUnitType>(inUnit);
         SpecificHeatUnitType outAu = SpecificHeatUnit.Instance.GetUnitAsEnum<SpecificHeatUnitType>(outUnit);
         double siValue = SpecificHeatUnit.Instance.ConvertToSIValue<SpecificHeatUnitType>(inAu, inValue);
         double outValue = SpecificHeatUnit.Instance.ConvertFromSIValue<SpecificHeatUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificEnergy(double inValue, string inUnit, string outUnit)
      {
         SpecificEnergyUnitType inAu = SpecificEnergyUnit.Instance.GetUnitAsEnum<SpecificEnergyUnitType>(inUnit);
         SpecificEnergyUnitType outAu = SpecificEnergyUnit.Instance.GetUnitAsEnum<SpecificEnergyUnitType>(outUnit);
         double siValue = SpecificEnergyUnit.Instance.ConvertToSIValue<SpecificEnergyUnitType>(inAu, inValue);
         double outValue = SpecificEnergyUnit.Instance.ConvertFromSIValue<SpecificEnergyUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertSpecificVolume(double inValue, string inUnit, string outUnit)
      {
         SpecificVolumeUnitType inAu = SpecificVolumeUnit.Instance.GetUnitAsEnum<SpecificVolumeUnitType>(inUnit);
         SpecificVolumeUnitType outAu = SpecificVolumeUnit.Instance.GetUnitAsEnum<SpecificVolumeUnitType>(outUnit);
         double siValue = SpecificVolumeUnit.Instance.ConvertToSIValue<SpecificVolumeUnitType>(inAu, inValue);
         double outValue = SpecificVolumeUnit.Instance.ConvertFromSIValue<SpecificVolumeUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertSurfaceTension(double inValue, string inUnit, string outUnit)
      {
         SurfaceTensionUnitType inAu = SurfaceTensionUnit.Instance.GetUnitAsEnum<SurfaceTensionUnitType>(inUnit);
         SurfaceTensionUnitType outAu = SurfaceTensionUnit.Instance.GetUnitAsEnum<SurfaceTensionUnitType>(outUnit);
         double siValue = SurfaceTensionUnit.Instance.ConvertToSIValue<SurfaceTensionUnitType>(inAu, inValue);
         double outValue = SurfaceTensionUnit.Instance.ConvertFromSIValue<SurfaceTensionUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertTemperature(double inValue, string inUnit, string outUnit)
      {
         TemperatureUnitType inAu = TemperatureUnit.Instance.GetUnitAsEnum(inUnit);
         TemperatureUnitType outAu = TemperatureUnit.Instance.GetUnitAsEnum(outUnit);
         double siValue = TemperatureUnit.Instance.ConvertToSIValue(inAu, inValue);
         double outValue = TemperatureUnit.Instance.ConvertFromSIValue(outAu, siValue);
         return outValue;
      }

      private double ConvertThermalConductivity(double inValue, string inUnit, string outUnit)
      {
         ThermalConductivityUnitType inAu = ThermalConductivityUnit.Instance.GetUnitAsEnum<ThermalConductivityUnitType>(inUnit);
         ThermalConductivityUnitType outAu = ThermalConductivityUnit.Instance.GetUnitAsEnum<ThermalConductivityUnitType>(outUnit);
         double siValue = ThermalConductivityUnit.Instance.ConvertToSIValue<ThermalConductivityUnitType>(inAu, inValue);
         double outValue = ThermalConductivityUnit.Instance.ConvertFromSIValue<ThermalConductivityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertTime(double inValue, string inUnit, string outUnit)
      {
         TimeUnitType inAu = TimeUnit.Instance.GetUnitAsEnum<TimeUnitType>(inUnit);
         TimeUnitType outAu = TimeUnit.Instance.GetUnitAsEnum<TimeUnitType>(outUnit);
         double siValue = TimeUnit.Instance.ConvertToSIValue<TimeUnitType>(inAu, inValue);
         double outValue = TimeUnit.Instance.ConvertFromSIValue<TimeUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVelocity(double inValue, string inUnit, string outUnit)
      {
         VelocityUnitType inAu = VelocityUnit.Instance.GetUnitAsEnum<VelocityUnitType>(inUnit);
         VelocityUnitType outAu = VelocityUnit.Instance.GetUnitAsEnum<VelocityUnitType>(outUnit);
         double siValue = VelocityUnit.Instance.ConvertToSIValue<VelocityUnitType>(inAu, inValue);
         double outValue = VelocityUnit.Instance.ConvertFromSIValue<VelocityUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVolume(double inValue, string inUnit, string outUnit)
      {
         VolumeUnitType inAu = VolumeUnit.Instance.GetUnitAsEnum<VolumeUnitType>(inUnit);
         VolumeUnitType outAu = VolumeUnit.Instance.GetUnitAsEnum<VolumeUnitType>(outUnit);
         double siValue = VolumeUnit.Instance.ConvertToSIValue<VolumeUnitType>(inAu, inValue);
         double outValue = VolumeUnit.Instance.ConvertFromSIValue<VolumeUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeFlowRate(double inValue, string inUnit, string outUnit)
      {
         VolumeFlowRateUnitType inAu = VolumeFlowRateUnit.Instance.GetUnitAsEnum<VolumeFlowRateUnitType>(inUnit);
         VolumeFlowRateUnitType outAu = VolumeFlowRateUnit.Instance.GetUnitAsEnum<VolumeFlowRateUnitType>(outUnit);
         double siValue = VolumeFlowRateUnit.Instance.ConvertToSIValue<VolumeFlowRateUnitType>(inAu, inValue);
         double outValue = VolumeFlowRateUnit.Instance.ConvertFromSIValue<VolumeFlowRateUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeRateFlowGases(double inValue, string inUnit, string outUnit)
      {
         VolumeRateFlowGasesUnitType inAu = VolumeRateFlowGasesUnit.Instance.GetUnitAsEnum<VolumeRateFlowGasesUnitType>(inUnit);
         VolumeRateFlowGasesUnitType outAu = VolumeRateFlowGasesUnit.Instance.GetUnitAsEnum<VolumeRateFlowGasesUnitType>(outUnit);
         double siValue = VolumeRateFlowGasesUnit.Instance.ConvertToSIValue<VolumeRateFlowGasesUnitType>(inAu, inValue);
         double outValue = VolumeRateFlowGasesUnit.Instance.ConvertFromSIValue<VolumeRateFlowGasesUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeRateFlowLiquids(double inValue, string inUnit, string outUnit)
      {
         VolumeRateFlowLiquidsUnitType inAu = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsEnum<VolumeRateFlowLiquidsUnitType>(inUnit);
         VolumeRateFlowLiquidsUnitType outAu = VolumeRateFlowLiquidsUnit.Instance.GetUnitAsEnum<VolumeRateFlowLiquidsUnitType>(outUnit);
         double siValue = VolumeRateFlowLiquidsUnit.Instance.ConvertToSIValue<VolumeRateFlowLiquidsUnitType>(inAu, inValue);
         double outValue = VolumeRateFlowLiquidsUnit.Instance.ConvertFromSIValue<VolumeRateFlowLiquidsUnitType>(outAu, siValue);
         return outValue;
      }

      private double ConvertVolumeHeatTransferCoefficient(double inValue, string inUnit, string outUnit)
      {
         VolumeHeatTransferCoefficientUnitType inAu = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsEnum<VolumeHeatTransferCoefficientUnitType>(inUnit);
         VolumeHeatTransferCoefficientUnitType outAu = VolumeHeatTransferCoefficientUnit.Instance.GetUnitAsEnum<VolumeHeatTransferCoefficientUnitType>(outUnit);
         double siValue = VolumeHeatTransferCoefficientUnit.Instance.ConvertToSIValue<VolumeHeatTransferCoefficientUnitType>(inAu, inValue);
         double outValue = VolumeHeatTransferCoefficientUnit.Instance.ConvertFromSIValue<VolumeHeatTransferCoefficientUnitType>(outAu, siValue);
         return outValue;
      }
   }
}
