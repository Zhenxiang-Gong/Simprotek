using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.Materials;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for MaterialStreamValuesControl.
	/// </summary>
	public class MaterialStreamValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 260;

      private MaterialStreamControl materialStreamCtrl;

      private ProsimoUI.ProcessVarTextBox textBoxSpecificHeat;
      private ProsimoUI.ProcessVarTextBox textBoxSpecificHeatDryBase;
      private ProsimoUI.ProcessVarTextBox textBoxSpecificEnthalpy;
      private ProsimoUI.ProcessVarTextBox textBoxMoistureContentDryBase;
      private ProsimoUI.ProcessVarTextBox textBoxTemperature;
      private ProsimoUI.ProcessVarTextBox textBoxPressure;
      private ProsimoUI.ProcessVarTextBox textBoxMassFlowRateDryBase;
      private ProsimoUI.ProcessVarTextBox textBoxMassFlowRate;
      private ProsimoUI.ProcessVarTextBox textBoxVolumeFlowRate;
      private ProsimoUI.ProcessVarTextBox textBoxConcentration;
      private ProsimoUI.ProcessVarTextBox textBoxDensity;
      private ProsimoUI.ProcessVarTextBox textBoxMoistureContentWetBase;
      private ProsimoUI.ProcessVarTextBox textBoxVaporFraction;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public MaterialStreamValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public MaterialStreamValuesControl(MaterialStreamControl materialStreamCtrl) : this()
		{
         this.materialStreamCtrl = materialStreamCtrl;
         this.InitializeVariableTextBoxes(materialStreamCtrl);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.textBoxSpecificHeat = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSpecificHeatDryBase = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSpecificEnthalpy = new ProsimoUI.ProcessVarTextBox();
         this.textBoxConcentration = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMoistureContentDryBase = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTemperature = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPressure = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDensity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMassFlowRateDryBase = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMassFlowRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxVolumeFlowRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMoistureContentWetBase = new ProsimoUI.ProcessVarTextBox();
         this.textBoxVaporFraction = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxSpecificEnthalpy
         // 
         this.textBoxSpecificEnthalpy.Location = new System.Drawing.Point(0, 180);
         this.textBoxSpecificEnthalpy.Name = "textBoxSpecificEnthalpy";
         this.textBoxSpecificEnthalpy.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificEnthalpy.TabIndex = 9;
         this.textBoxSpecificEnthalpy.Text = "";
         this.textBoxSpecificEnthalpy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSpecificHeat
         // 
         this.textBoxSpecificHeat.Location = new System.Drawing.Point(0, 200);
         this.textBoxSpecificHeat.Name = "textBoxSpecificHeat";
         this.textBoxSpecificHeat.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeat.TabIndex = 10;
         this.textBoxSpecificHeat.Text = "";
         this.textBoxSpecificHeat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSpecificHeatDryBase
         // 
         this.textBoxSpecificHeatDryBase.Location = new System.Drawing.Point(0, 220);
         this.textBoxSpecificHeatDryBase.Name = "textBoxSpecificHeatDryBase";
         this.textBoxSpecificHeatDryBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeatDryBase.TabIndex = 11;
         this.textBoxSpecificHeatDryBase.Text = "";
         this.textBoxSpecificHeatDryBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxConcentration
         // 
         this.textBoxConcentration.Location = new System.Drawing.Point(0, 160);
         this.textBoxConcentration.Name = "textBoxConcentration";
         this.textBoxConcentration.Size = new System.Drawing.Size(80, 20);
         this.textBoxConcentration.TabIndex = 8;
         this.textBoxConcentration.Text = "";
         this.textBoxConcentration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMoistureContentDryBase
         // 
         this.textBoxMoistureContentDryBase.Location = new System.Drawing.Point(0, 140);
         this.textBoxMoistureContentDryBase.Name = "textBoxMoistureContentDryBase";
         this.textBoxMoistureContentDryBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxMoistureContentDryBase.TabIndex = 7;
         this.textBoxMoistureContentDryBase.Text = "";
         this.textBoxMoistureContentDryBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMoistureContentWetBase
         // 
         this.textBoxMoistureContentWetBase.Location = new System.Drawing.Point(0, 120);
         this.textBoxMoistureContentWetBase.Name = "textBoxMoistureContentWetBase";
         this.textBoxMoistureContentWetBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxMoistureContentWetBase.TabIndex = 6;
         this.textBoxMoistureContentWetBase.Text = "";
         this.textBoxMoistureContentWetBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTemperature
         // 
         this.textBoxTemperature.Location = new System.Drawing.Point(0, 80);
         this.textBoxTemperature.Name = "textBoxTemperature";
         this.textBoxTemperature.Size = new System.Drawing.Size(80, 20);
         this.textBoxTemperature.TabIndex = 5;
         this.textBoxTemperature.Text = "";
         this.textBoxTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPressure
         // 
         this.textBoxPressure.Location = new System.Drawing.Point(0, 60);
         this.textBoxPressure.Name = "textBoxPressure";
         this.textBoxPressure.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressure.TabIndex = 4;
         this.textBoxPressure.Text = "";
         this.textBoxPressure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDensity
         // 
         this.textBoxDensity.Location = new System.Drawing.Point(0, 240);
         this.textBoxDensity.Name = "textBoxDensity";
         this.textBoxDensity.Size = new System.Drawing.Size(80, 20);
         this.textBoxDensity.TabIndex = 12;
         this.textBoxDensity.Text = "";
         this.textBoxDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRateDryBase
         // 
         this.textBoxMassFlowRateDryBase.Location = new System.Drawing.Point(0, 20);
         this.textBoxMassFlowRateDryBase.Name = "textBoxMassFlowRateDryBase";
         this.textBoxMassFlowRateDryBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateDryBase.TabIndex = 2;
         this.textBoxMassFlowRateDryBase.Text = "";
         this.textBoxMassFlowRateDryBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRate
         // 
         this.textBoxMassFlowRate.Location = new System.Drawing.Point(0, 0);
         this.textBoxMassFlowRate.Name = "textBoxMassFlowRate";
         this.textBoxMassFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRate.TabIndex = 1;
         this.textBoxMassFlowRate.Text = "";
         this.textBoxMassFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxVolumeFlowRate
         // 
         this.textBoxVolumeFlowRate.Location = new System.Drawing.Point(0, 40);
         this.textBoxVolumeFlowRate.Name = "textBoxVolumeFlowRate";
         this.textBoxVolumeFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxVolumeFlowRate.TabIndex = 3;
         this.textBoxVolumeFlowRate.Text = "";
         this.textBoxVolumeFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxVaporFraction
         // 
         this.textBoxVaporFraction.Location = new System.Drawing.Point(0, 100);
         this.textBoxVaporFraction.Name = "textBoxVaporFraction";
         this.textBoxVaporFraction.Size = new System.Drawing.Size(80, 20);
         this.textBoxVaporFraction.TabIndex = 13;
         this.textBoxVaporFraction.Text = "";
         this.textBoxVaporFraction.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // MaterialStreamValuesControl
         // 
         this.Controls.Add(this.textBoxVaporFraction);
         this.Controls.Add(this.textBoxMoistureContentWetBase);
         this.Controls.Add(this.textBoxSpecificHeat);
         this.Controls.Add(this.textBoxSpecificHeatDryBase);
         this.Controls.Add(this.textBoxSpecificEnthalpy);
         this.Controls.Add(this.textBoxConcentration);
         this.Controls.Add(this.textBoxMoistureContentDryBase);
         this.Controls.Add(this.textBoxTemperature);
         this.Controls.Add(this.textBoxPressure);
         this.Controls.Add(this.textBoxDensity);
         this.Controls.Add(this.textBoxMassFlowRateDryBase);
         this.Controls.Add(this.textBoxMassFlowRate);
         this.Controls.Add(this.textBoxVolumeFlowRate);
         this.Name = "MaterialStreamValuesControl";
         this.Size = new System.Drawing.Size(80, 260);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(MaterialStreamControl ctrl)
      {
         this.textBoxMassFlowRateDryBase.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.MassFlowRateDryBase);
         this.textBoxMassFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.MassFlowRate);
         this.textBoxVolumeFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.VolumeFlowRate);
         this.textBoxPressure.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.Pressure);
         this.textBoxTemperature.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.Temperature);
         this.textBoxMoistureContentDryBase.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.MoistureContentDryBase);
         this.textBoxMoistureContentWetBase.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.MoistureContentWetBase);
         this.textBoxConcentration.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.MassConcentration);
         this.textBoxSpecificHeat.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.SpecificHeat);
         this.textBoxSpecificHeatDryBase.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.SpecificHeatDryBase);
         this.textBoxDensity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.Density);
         this.textBoxSpecificEnthalpy.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.SpecificEnthalpy);
         this.textBoxVaporFraction.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.MaterialStream.VaporFraction);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         bool isLiquid = this.materialStreamCtrl.MaterialStream.MaterialStateType.Equals(MaterialStateType.Liquid);

         ArrayList list = new ArrayList();
         list.Add(this.textBoxMassFlowRate);
         list.Add(this.textBoxMassFlowRateDryBase);
         list.Add(this.textBoxVolumeFlowRate);
         if (isLiquid)
            list.Add(this.textBoxPressure);
         list.Add(this.textBoxTemperature);
         if (isLiquid)
            list.Add(this.textBoxVaporFraction);
         list.Add(this.textBoxMoistureContentWetBase);
         list.Add(this.textBoxMoistureContentDryBase);
         if (isLiquid)
            list.Add(this.textBoxConcentration);
         list.Add(this.textBoxSpecificEnthalpy);
         list.Add(this.textBoxSpecificHeat);
         if (!isLiquid)
            list.Add(this.textBoxSpecificHeatDryBase);
         list.Add(this.textBoxDensity);
         
         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
