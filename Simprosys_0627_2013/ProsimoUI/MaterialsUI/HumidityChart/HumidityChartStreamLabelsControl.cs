using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for HumidityChartStreamLabelsControl.
	/// </summary>
	public class HumidityChartStreamLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 160;

      private ProcessVarLabel labelDewPoint;
      private ProcessVarLabel labelHumidity;
      private ProcessVarLabel labelRelativeHumidity;
      private ProcessVarLabel labelWetBulbTemperature;
      private ProcessVarLabel labelTemperature;
      private ProcessVarLabel labelDensity;
      private ProcessVarLabel labelEnthalpy;
      private ProcessVarLabel labelSpecificHeatDryBase;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HumidityChartStreamLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public HumidityChartStreamLabelsControl(DryingGasStream stream) : this()
		{
         this.InitializeVariableLabels(stream);
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
         this.labelDewPoint = new ProsimoUI.ProcessVarLabel();
         this.labelHumidity = new ProsimoUI.ProcessVarLabel();
         this.labelRelativeHumidity = new ProsimoUI.ProcessVarLabel();
         this.labelWetBulbTemperature = new ProsimoUI.ProcessVarLabel();
         this.labelTemperature = new ProsimoUI.ProcessVarLabel();
         this.labelDensity = new ProsimoUI.ProcessVarLabel();
         this.labelEnthalpy = new ProsimoUI.ProcessVarLabel();
         this.labelSpecificHeatDryBase = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelDewPoint
         // 
         this.labelDewPoint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDewPoint.Location = new System.Drawing.Point(0, 40);
         this.labelDewPoint.Name = "labelDewPoint";
         this.labelDewPoint.Size = new System.Drawing.Size(192, 20);
         this.labelDewPoint.TabIndex = 111;
         this.labelDewPoint.Text = "DEW_POINT";
         this.labelDewPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHumidity
         // 
         this.labelHumidity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHumidity.Location = new System.Drawing.Point(0, 60);
         this.labelHumidity.Name = "labelHumidity";
         this.labelHumidity.Size = new System.Drawing.Size(192, 20);
         this.labelHumidity.TabIndex = 112;
         this.labelHumidity.Text = "HUMIDITY";
         this.labelHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelRelativeHumidity
         // 
         this.labelRelativeHumidity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelRelativeHumidity.Location = new System.Drawing.Point(0, 80);
         this.labelRelativeHumidity.Name = "labelRelativeHumidity";
         this.labelRelativeHumidity.Size = new System.Drawing.Size(192, 20);
         this.labelRelativeHumidity.TabIndex = 113;
         this.labelRelativeHumidity.Text = "RELATIVE_HUMIDITY";
         this.labelRelativeHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelWetBulbTemperature
         // 
         this.labelWetBulbTemperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWetBulbTemperature.Location = new System.Drawing.Point(0, 20);
         this.labelWetBulbTemperature.Name = "labelWetBulbTemperature";
         this.labelWetBulbTemperature.Size = new System.Drawing.Size(192, 20);
         this.labelWetBulbTemperature.TabIndex = 110;
         this.labelWetBulbTemperature.Text = "WET_BULB_TEMPERATURE";
         this.labelWetBulbTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTemperature
         // 
         this.labelTemperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTemperature.Location = new System.Drawing.Point(0, 0);
         this.labelTemperature.Name = "labelTemperature";
         this.labelTemperature.Size = new System.Drawing.Size(192, 20);
         this.labelTemperature.TabIndex = 109;
         this.labelTemperature.Text = "TEMPERATURE";
         this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDensity
         // 
         this.labelDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDensity.Location = new System.Drawing.Point(0, 140);
         this.labelDensity.Name = "labelDensity";
         this.labelDensity.Size = new System.Drawing.Size(192, 20);
         this.labelDensity.TabIndex = 116;
         this.labelDensity.Text = "DENSITY";
         this.labelDensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEnthalpy
         // 
         this.labelEnthalpy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEnthalpy.Location = new System.Drawing.Point(0, 100);
         this.labelEnthalpy.Name = "labelEnthalpy";
         this.labelEnthalpy.Size = new System.Drawing.Size(192, 20);
         this.labelEnthalpy.TabIndex = 114;
         this.labelEnthalpy.Text = "ENTHALPY";
         this.labelEnthalpy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSpecificHeatDryBase
         // 
         this.labelSpecificHeatDryBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeatDryBase.Location = new System.Drawing.Point(0, 120);
         this.labelSpecificHeatDryBase.Name = "labelSpecificHeatDryBase";
         this.labelSpecificHeatDryBase.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeatDryBase.TabIndex = 115;
         this.labelSpecificHeatDryBase.Text = "SPECIFIC_HEAT_DRY_BASE";
         this.labelSpecificHeatDryBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HumidityChartStreamLabelsControl
         // 
         this.Controls.Add(this.labelDewPoint);
         this.Controls.Add(this.labelHumidity);
         this.Controls.Add(this.labelRelativeHumidity);
         this.Controls.Add(this.labelWetBulbTemperature);
         this.Controls.Add(this.labelTemperature);
         this.Controls.Add(this.labelDensity);
         this.Controls.Add(this.labelEnthalpy);
         this.Controls.Add(this.labelSpecificHeatDryBase);
         this.Name = "HumidityChartStreamLabelsControl";
         this.Size = new System.Drawing.Size(192, 160);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(DryingGasStream stream)
      {
         this.labelTemperature.InitializeVariable(stream.Temperature);
         this.labelWetBulbTemperature.InitializeVariable(stream.WetBulbTemperature);
         this.labelDewPoint.InitializeVariable(stream.DewPoint);
         this.labelHumidity.InitializeVariable(stream.Humidity);
         this.labelRelativeHumidity.InitializeVariable(stream.RelativeHumidity);
         this.labelEnthalpy.InitializeVariable(stream.SpecificEnthalpy);
         this.labelSpecificHeatDryBase.InitializeVariable(stream.SpecificHeatDryBase);
         this.labelDensity.InitializeVariable(stream.Density);
      }
   }
}
