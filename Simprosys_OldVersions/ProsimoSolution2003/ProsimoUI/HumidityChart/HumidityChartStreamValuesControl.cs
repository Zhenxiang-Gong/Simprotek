using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for HumidityChartStreamValuesControl.
	/// </summary>
	public class HumidityChartStreamValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 160;

      private ProcessVarTextBox textBoxDensity;
      private ProcessVarTextBox textBoxRelativeHumidity;
      private ProcessVarTextBox textBoxDewPoint;
      private ProcessVarTextBox textBoxWetBulbTemperature;
      private ProcessVarTextBox textBoxTemperature;
      private ProcessVarTextBox textBoxSpecificHeatDryBase;
      private ProcessVarTextBox textBoxEnthalpy;
      private ProcessVarTextBox textBoxHumidity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HumidityChartStreamValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public HumidityChartStreamValuesControl(Flowsheet flowsheet, DryingGasStream gasStream) : this()
		{
         this.InitializeVariableTextBoxes(flowsheet, gasStream);
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
         this.textBoxDensity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxRelativeHumidity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDewPoint = new ProsimoUI.ProcessVarTextBox();
         this.textBoxWetBulbTemperature = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTemperature = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSpecificHeatDryBase = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEnthalpy = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHumidity = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxDensity
         // 
         this.textBoxDensity.Location = new System.Drawing.Point(0, 140);
         this.textBoxDensity.Name = "textBoxDensity";
         this.textBoxDensity.Size = new System.Drawing.Size(80, 20);
         this.textBoxDensity.TabIndex = 115;
         this.textBoxDensity.Text = "";
         this.textBoxDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxRelativeHumidity
         // 
         this.textBoxRelativeHumidity.Location = new System.Drawing.Point(0, 80);
         this.textBoxRelativeHumidity.Name = "textBoxRelativeHumidity";
         this.textBoxRelativeHumidity.Size = new System.Drawing.Size(80, 20);
         this.textBoxRelativeHumidity.TabIndex = 112;
         this.textBoxRelativeHumidity.Text = "";
         this.textBoxRelativeHumidity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDewPoint
         // 
         this.textBoxDewPoint.Location = new System.Drawing.Point(0, 40);
         this.textBoxDewPoint.Name = "textBoxDewPoint";
         this.textBoxDewPoint.Size = new System.Drawing.Size(80, 20);
         this.textBoxDewPoint.TabIndex = 110;
         this.textBoxDewPoint.Text = "";
         this.textBoxDewPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxWetBulbTemperature
         // 
         this.textBoxWetBulbTemperature.Location = new System.Drawing.Point(0, 20);
         this.textBoxWetBulbTemperature.Name = "textBoxWetBulbTemperature";
         this.textBoxWetBulbTemperature.Size = new System.Drawing.Size(80, 20);
         this.textBoxWetBulbTemperature.TabIndex = 109;
         this.textBoxWetBulbTemperature.Text = "";
         this.textBoxWetBulbTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTemperature
         // 
         this.textBoxTemperature.Location = new System.Drawing.Point(0, 0);
         this.textBoxTemperature.Name = "textBoxTemperature";
         this.textBoxTemperature.Size = new System.Drawing.Size(80, 20);
         this.textBoxTemperature.TabIndex = 108;
         this.textBoxTemperature.Text = "";
         this.textBoxTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSpecificHeatDryBase
         // 
         this.textBoxSpecificHeatDryBase.Location = new System.Drawing.Point(0, 120);
         this.textBoxSpecificHeatDryBase.Name = "textBoxSpecificHeatDryBase";
         this.textBoxSpecificHeatDryBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeatDryBase.TabIndex = 114;
         this.textBoxSpecificHeatDryBase.Text = "";
         this.textBoxSpecificHeatDryBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEnthalpy
         // 
         this.textBoxEnthalpy.Location = new System.Drawing.Point(0, 100);
         this.textBoxEnthalpy.Name = "textBoxEnthalpy";
         this.textBoxEnthalpy.Size = new System.Drawing.Size(80, 20);
         this.textBoxEnthalpy.TabIndex = 113;
         this.textBoxEnthalpy.Text = "";
         this.textBoxEnthalpy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHumidity
         // 
         this.textBoxHumidity.Location = new System.Drawing.Point(0, 60);
         this.textBoxHumidity.Name = "textBoxHumidity";
         this.textBoxHumidity.Size = new System.Drawing.Size(80, 20);
         this.textBoxHumidity.TabIndex = 111;
         this.textBoxHumidity.Text = "";
         this.textBoxHumidity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HumidityChartStreamValuesControl
         // 
         this.Controls.Add(this.textBoxDensity);
         this.Controls.Add(this.textBoxRelativeHumidity);
         this.Controls.Add(this.textBoxDewPoint);
         this.Controls.Add(this.textBoxWetBulbTemperature);
         this.Controls.Add(this.textBoxTemperature);
         this.Controls.Add(this.textBoxSpecificHeatDryBase);
         this.Controls.Add(this.textBoxEnthalpy);
         this.Controls.Add(this.textBoxHumidity);
         this.Name = "HumidityChartStreamValuesControl";
         this.Size = new System.Drawing.Size(80, 160);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, DryingGasStream stream)
      {
         this.textBoxTemperature.InitializeVariable(flowsheet, stream.Temperature);
         this.textBoxWetBulbTemperature.InitializeVariable(flowsheet, stream.WetBulbTemperature);
         this.textBoxDewPoint.InitializeVariable(flowsheet, stream.DewPoint);
         this.textBoxHumidity.InitializeVariable(flowsheet, stream.Humidity);
         this.textBoxRelativeHumidity.InitializeVariable(flowsheet, stream.RelativeHumidity);
         this.textBoxEnthalpy.InitializeVariable(flowsheet, stream.SpecificEnthalpy);
         this.textBoxSpecificHeatDryBase.InitializeVariable(flowsheet, stream.SpecificHeatDryBase);
         this.textBoxDensity.InitializeVariable(flowsheet, stream.Density);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxTemperature);
         list.Add(this.textBoxWetBulbTemperature);
         list.Add(this.textBoxDewPoint);
         list.Add(this.textBoxHumidity);
         list.Add(this.textBoxRelativeHumidity);
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
