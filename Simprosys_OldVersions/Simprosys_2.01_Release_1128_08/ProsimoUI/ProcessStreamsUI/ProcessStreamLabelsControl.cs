using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for ProcessStreamLabelsControl.
	/// </summary>
	public class ProcessStreamLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 240;

      private ProcessVarLabel labelVolumeFlowRate;
      private ProcessVarLabel labelMassFlowRate;
      private ProcessVarLabel labelTemperature;
      private ProcessVarLabel labelDensity;
      private ProcessVarLabel labelEnthalpy;
      private ProcessVarLabel labelPressure;
      private ProcessVarLabel labelSpecificHeat;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public ProcessStreamLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ProcessStreamLabelsControl(ProcessStream stream) : this()
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
         this.labelVolumeFlowRate = new ProcessVarLabel();
         this.labelMassFlowRate = new ProcessVarLabel();
         this.labelTemperature = new ProcessVarLabel();
         this.labelDensity = new ProcessVarLabel();
         this.labelEnthalpy = new ProcessVarLabel();
         this.labelPressure = new ProcessVarLabel();
         this.labelSpecificHeat = new ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelVolumeFlowRate
         // 
         this.labelVolumeFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelVolumeFlowRate.Location = new System.Drawing.Point(0, 40);
         this.labelVolumeFlowRate.Name = "labelVolumeFlowRate";
         this.labelVolumeFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelVolumeFlowRate.TabIndex = 107;
         this.labelVolumeFlowRate.Text = "VOLUME_FLOW_RATE";
         this.labelVolumeFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRate
         // 
         this.labelMassFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRate.Location = new System.Drawing.Point(0, 20);
         this.labelMassFlowRate.Name = "labelMassFlowRate";
         this.labelMassFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRate.TabIndex = 106;
         this.labelMassFlowRate.Text = "MASS_FLOW_RATE";
         this.labelMassFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTemperature
         // 
         this.labelTemperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTemperature.Location = new System.Drawing.Point(0, 80);
         this.labelTemperature.Name = "labelTemperature";
         this.labelTemperature.Size = new System.Drawing.Size(192, 20);
         this.labelTemperature.TabIndex = 109;
         this.labelTemperature.Text = "TEMPERATURE";
         this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDensity
         // 
         this.labelDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDensity.Location = new System.Drawing.Point(0, 220);
         this.labelDensity.Name = "labelDensity";
         this.labelDensity.Size = new System.Drawing.Size(192, 20);
         this.labelDensity.TabIndex = 116;
         this.labelDensity.Text = "DENSITY";
         this.labelDensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEnthalpy
         // 
         this.labelEnthalpy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEnthalpy.Location = new System.Drawing.Point(0, 180);
         this.labelEnthalpy.Name = "labelEnthalpy";
         this.labelEnthalpy.Size = new System.Drawing.Size(192, 20);
         this.labelEnthalpy.TabIndex = 114;
         this.labelEnthalpy.Text = "ENTHALPY";
         this.labelEnthalpy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPressure
         // 
         this.labelPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressure.Location = new System.Drawing.Point(0, 60);
         this.labelPressure.Name = "labelPressure";
         this.labelPressure.Size = new System.Drawing.Size(192, 20);
         this.labelPressure.TabIndex = 108;
         this.labelPressure.Text = "PRESSURE";
         this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSpecificHeat
         // 
         this.labelSpecificHeat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeat.Location = new System.Drawing.Point(0, 200);
         this.labelSpecificHeat.Name = "labelSpecificHeat";
         this.labelSpecificHeat.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeat.TabIndex = 115;
         this.labelSpecificHeat.Text = "SPECIFIC_HEAT";
         this.labelSpecificHeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ProcessStreamLabelsControl
         // 
         this.Controls.Add(this.labelVolumeFlowRate);
         this.Controls.Add(this.labelMassFlowRate);
         this.Controls.Add(this.labelTemperature);
         this.Controls.Add(this.labelDensity);
         this.Controls.Add(this.labelEnthalpy);
         this.Controls.Add(this.labelPressure);
         this.Controls.Add(this.labelSpecificHeat);
         this.Name = "ProcessStreamLabelsControl";
         this.Size = new System.Drawing.Size(192, 240);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(ProcessStream stream)
      {
         this.labelMassFlowRate.InitializeVariable(stream.MassFlowRate);
         this.labelVolumeFlowRate.InitializeVariable(stream.VolumeFlowRate);
         this.labelPressure.InitializeVariable(stream.Pressure);
         this.labelTemperature.InitializeVariable(stream.Temperature);
         this.labelEnthalpy.InitializeVariable(stream.SpecificEnthalpy);
         this.labelSpecificHeat.InitializeVariable(stream.SpecificHeat);
         this.labelDensity.InitializeVariable(stream.Density);
      }
   }
}
