using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for ProcessStreamValuesControl.
	/// </summary>
	public class ProcessStreamValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 240;

      private ProcessVarTextBox textBoxDensity;
      private ProcessVarTextBox textBoxTemperature;
      private ProcessVarTextBox textBoxPressure;
      private ProcessVarTextBox textBoxSpecificHeat;
      private ProcessVarTextBox textBoxMassFlowRate;
      private ProcessVarTextBox textBoxEnthalpy;
      private ProcessVarTextBox textBoxVolumeFlowRate;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public ProcessStreamValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public ProcessStreamValuesControl(ProcessStreamControl processStreamCtrl) : this()
		{
         this.InitializeVariableTextBoxes(processStreamCtrl);
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
         this.textBoxDensity = new ProcessVarTextBox();
         this.textBoxTemperature = new ProcessVarTextBox();
         this.textBoxPressure = new ProcessVarTextBox();
         this.textBoxSpecificHeat = new ProcessVarTextBox();
         this.textBoxMassFlowRate = new ProcessVarTextBox();
         this.textBoxEnthalpy = new ProcessVarTextBox();
         this.textBoxVolumeFlowRate = new ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxDensity
         // 
         this.textBoxDensity.Location = new System.Drawing.Point(0, 220);
         this.textBoxDensity.Name = "textBoxDensity";
         this.textBoxDensity.Size = new System.Drawing.Size(80, 20);
         this.textBoxDensity.TabIndex = 115;
         this.textBoxDensity.Text = "";
         this.textBoxDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTemperature
         // 
         this.textBoxTemperature.Location = new System.Drawing.Point(0, 80);
         this.textBoxTemperature.Name = "textBoxTemperature";
         this.textBoxTemperature.Size = new System.Drawing.Size(80, 20);
         this.textBoxTemperature.TabIndex = 108;
         this.textBoxTemperature.Text = "";
         this.textBoxTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPressure
         // 
         this.textBoxPressure.Location = new System.Drawing.Point(0, 60);
         this.textBoxPressure.Name = "textBoxPressure";
         this.textBoxPressure.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressure.TabIndex = 107;
         this.textBoxPressure.Text = "";
         this.textBoxPressure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSpecificHeat
         // 
         this.textBoxSpecificHeat.Location = new System.Drawing.Point(0, 200);
         this.textBoxSpecificHeat.Name = "textBoxSpecificHeat";
         this.textBoxSpecificHeat.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeat.TabIndex = 114;
         this.textBoxSpecificHeat.Text = "";
         this.textBoxSpecificHeat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRate
         // 
         this.textBoxMassFlowRate.Location = new System.Drawing.Point(0, 20);
         this.textBoxMassFlowRate.Name = "textBoxMassFlowRate";
         this.textBoxMassFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRate.TabIndex = 105;
         this.textBoxMassFlowRate.Text = "";
         this.textBoxMassFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEnthalpy
         // 
         this.textBoxEnthalpy.Location = new System.Drawing.Point(0, 180);
         this.textBoxEnthalpy.Name = "textBoxEnthalpy";
         this.textBoxEnthalpy.Size = new System.Drawing.Size(80, 20);
         this.textBoxEnthalpy.TabIndex = 113;
         this.textBoxEnthalpy.Text = "";
         this.textBoxEnthalpy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxVolumeFlowRate
         // 
         this.textBoxVolumeFlowRate.Location = new System.Drawing.Point(0, 40);
         this.textBoxVolumeFlowRate.Name = "textBoxVolumeFlowRate";
         this.textBoxVolumeFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxVolumeFlowRate.TabIndex = 106;
         this.textBoxVolumeFlowRate.Text = "";
         this.textBoxVolumeFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // ProcessStreamValuesControl
         // 
         this.Controls.Add(this.textBoxDensity);
         this.Controls.Add(this.textBoxTemperature);
         this.Controls.Add(this.textBoxPressure);
         this.Controls.Add(this.textBoxSpecificHeat);
         this.Controls.Add(this.textBoxMassFlowRate);
         this.Controls.Add(this.textBoxEnthalpy);
         this.Controls.Add(this.textBoxVolumeFlowRate);
         this.Name = "ProcessStreamValuesControl";
         this.Size = new System.Drawing.Size(80, 240);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(ProcessStreamControl ctrl)
      {
         this.textBoxMassFlowRate.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.MassFlowRate);
         this.textBoxVolumeFlowRate.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.VolumeFlowRate);
         this.textBoxPressure.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.Pressure);
         this.textBoxTemperature.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.Temperature);
         this.textBoxEnthalpy.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.SpecificEnthalpy);
         this.textBoxSpecificHeat.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.SpecificHeat);
         this.textBoxDensity.InitializeVariable(ctrl.Flowsheet, ctrl.ProcessStream.Density);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxMassFlowRate);
         list.Add(this.textBoxVolumeFlowRate);
         list.Add(this.textBoxPressure);
         list.Add(this.textBoxTemperature);
         list.Add(this.textBoxSpecificHeat);
         list.Add(this.textBoxEnthalpy);
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
