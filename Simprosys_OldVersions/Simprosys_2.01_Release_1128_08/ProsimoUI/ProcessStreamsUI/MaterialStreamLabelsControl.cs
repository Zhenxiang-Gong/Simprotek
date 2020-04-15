using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.Materials;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for MaterialStreamLabelsControl.
	/// </summary>
	public class MaterialStreamLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 260;

      private ProsimoUI.ProcessVarLabel labelMoistureContentDryBase;
      private ProsimoUI.ProcessVarLabel labelVolumeFlowRate;
      private ProsimoUI.ProcessVarLabel labelTemperature;
      private ProsimoUI.ProcessVarLabel labelPressure;
      private ProsimoUI.ProcessVarLabel labelSpecificHeat;
      private ProsimoUI.ProcessVarLabel labelMassFlowRateDryBase;
      private ProsimoUI.ProcessVarLabel labelConcentration;
      private ProsimoUI.ProcessVarLabel labelDensity;
      private ProsimoUI.ProcessVarLabel labelSpecificEnthalpy;
      private ProsimoUI.ProcessVarLabel labelMoistureContentWetBase;
      private ProsimoUI.ProcessVarLabel labelMassFlowRate;
      private ProsimoUI.ProcessVarLabel labelVaporFraction;
      private ProsimoUI.ProcessVarLabel labelSpecificHeatDryBase;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public MaterialStreamLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public MaterialStreamLabelsControl(DryingMaterialStream stream) : this()
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
         this.labelDensity = new ProsimoUI.ProcessVarLabel();
         this.labelMoistureContentDryBase = new ProsimoUI.ProcessVarLabel();
         this.labelVolumeFlowRate = new ProsimoUI.ProcessVarLabel();
         this.labelTemperature = new ProsimoUI.ProcessVarLabel();
         this.labelMassFlowRate = new ProsimoUI.ProcessVarLabel();
         this.labelPressure = new ProsimoUI.ProcessVarLabel();
         this.labelSpecificHeat = new ProsimoUI.ProcessVarLabel();
         this.labelConcentration = new ProsimoUI.ProcessVarLabel();
         this.labelMassFlowRateDryBase = new ProsimoUI.ProcessVarLabel();
         this.labelMoistureContentWetBase = new ProsimoUI.ProcessVarLabel();
         this.labelSpecificEnthalpy = new ProsimoUI.ProcessVarLabel();
         this.labelVaporFraction = new ProsimoUI.ProcessVarLabel();
         this.labelSpecificHeatDryBase = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelSpecificEnthalpy
         // 
         this.labelSpecificEnthalpy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificEnthalpy.Location = new System.Drawing.Point(0, 180);
         this.labelSpecificEnthalpy.Name = "labelSpecificEnthalpy";
         this.labelSpecificEnthalpy.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificEnthalpy.TabIndex = 117;
         this.labelSpecificEnthalpy.Text = "Enthalpy";
         this.labelSpecificEnthalpy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDensity
         // 
         this.labelDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDensity.Location = new System.Drawing.Point(0, 240);
         this.labelDensity.Name = "labelDensity";
         this.labelDensity.Size = new System.Drawing.Size(192, 20);
         this.labelDensity.TabIndex = 114;
         this.labelDensity.Text = "Density";
         this.labelDensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMoistureContentDryBase
         // 
         this.labelMoistureContentDryBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMoistureContentDryBase.Location = new System.Drawing.Point(0, 140);
         this.labelMoistureContentDryBase.Name = "labelMoistureContentDryBase";
         this.labelMoistureContentDryBase.Size = new System.Drawing.Size(192, 20);
         this.labelMoistureContentDryBase.TabIndex = 109;
         this.labelMoistureContentDryBase.Text = "MoistureContentDry";
         this.labelMoistureContentDryBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMoistureContentWetBase
         // 
         this.labelMoistureContentWetBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMoistureContentWetBase.Location = new System.Drawing.Point(0, 120);
         this.labelMoistureContentWetBase.Name = "labelMoistureContentWetBase";
         this.labelMoistureContentWetBase.Size = new System.Drawing.Size(192, 20);
         this.labelMoistureContentWetBase.TabIndex = 112;
         this.labelMoistureContentWetBase.Text = "MoistureContentWet";
         this.labelMoistureContentWetBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelVolumeFlowRate
         // 
         this.labelVolumeFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelVolumeFlowRate.Location = new System.Drawing.Point(0, 40);
         this.labelVolumeFlowRate.Name = "labelVolumeFlowRate";
         this.labelVolumeFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelVolumeFlowRate.TabIndex = 108;
         this.labelVolumeFlowRate.Text = "VolumeFlowRate";
         this.labelVolumeFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTemperature
         // 
         this.labelTemperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTemperature.Location = new System.Drawing.Point(0, 80);
         this.labelTemperature.Name = "labelTemperature";
         this.labelTemperature.Size = new System.Drawing.Size(192, 20);
         this.labelTemperature.TabIndex = 111;
         this.labelTemperature.Text = "Temperature";
         this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPressure
         // 
         this.labelPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressure.Location = new System.Drawing.Point(0, 60);
         this.labelPressure.Name = "labelPressure";
         this.labelPressure.Size = new System.Drawing.Size(192, 20);
         this.labelPressure.TabIndex = 110;
         this.labelPressure.Text = "Pressure";
         this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSpecificHeat
         // 
         this.labelSpecificHeat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeat.Location = new System.Drawing.Point(0, 200);
         this.labelSpecificHeat.Name = "labelSpecificHeat";
         this.labelSpecificHeat.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeat.TabIndex = 115;
         this.labelSpecificHeat.Text = "SpecificHeat";
         this.labelSpecificHeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSpecificHeatDryBase
         // 
         this.labelSpecificHeatDryBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeatDryBase.Location = new System.Drawing.Point(0, 220);
         this.labelSpecificHeatDryBase.Name = "labelSpecificHeatDryBase";
         this.labelSpecificHeatDryBase.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeatDryBase.TabIndex = 116;
         this.labelSpecificHeatDryBase.Text = "SpecificHeatDryBase";
         this.labelSpecificHeatDryBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelConcentration
         // 
         this.labelConcentration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelConcentration.Location = new System.Drawing.Point(0, 160);
         this.labelConcentration.Name = "labelConcentration";
         this.labelConcentration.Size = new System.Drawing.Size(192, 20);
         this.labelConcentration.TabIndex = 113;
         this.labelConcentration.Text = "Concentration";
         this.labelConcentration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRateDryBase
         // 
         this.labelMassFlowRateDryBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRateDryBase.Location = new System.Drawing.Point(0, 20);
         this.labelMassFlowRateDryBase.Name = "labelMassFlowRateDryBase";
         this.labelMassFlowRateDryBase.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRateDryBase.TabIndex = 118;
         this.labelMassFlowRateDryBase.Text = "MassFlowRateDry";
         this.labelMassFlowRateDryBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRate
         // 
         this.labelMassFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRate.Location = new System.Drawing.Point(0, 0);
         this.labelMassFlowRate.Name = "labelMassFlowRate";
         this.labelMassFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRate.TabIndex = 106;
         this.labelMassFlowRate.Text = "MassFlowRateWet";
         this.labelMassFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelVaporFraction
         // 
         this.labelVaporFraction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelVaporFraction.Location = new System.Drawing.Point(0, 100);
         this.labelVaporFraction.Name = "labelVaporFraction";
         this.labelVaporFraction.Size = new System.Drawing.Size(192, 20);
         this.labelVaporFraction.TabIndex = 119;
         this.labelVaporFraction.Text = "VaporFraction";
         this.labelVaporFraction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // MaterialStreamLabelsControl
         // 
         this.Controls.Add(this.labelVaporFraction);
         this.Controls.Add(this.labelSpecificEnthalpy);
         this.Controls.Add(this.labelDensity);
         this.Controls.Add(this.labelMoistureContentDryBase);
         this.Controls.Add(this.labelVolumeFlowRate);
         this.Controls.Add(this.labelTemperature);
         this.Controls.Add(this.labelMassFlowRate);
         this.Controls.Add(this.labelPressure);
         this.Controls.Add(this.labelSpecificHeat);
         this.Controls.Add(this.labelSpecificHeatDryBase);
         this.Controls.Add(this.labelConcentration);
         this.Controls.Add(this.labelMassFlowRateDryBase);
         this.Controls.Add(this.labelMoistureContentWetBase);
         this.Name = "MaterialStreamLabelsControl";
         this.Size = new System.Drawing.Size(192, 260);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(DryingMaterialStream stream)
      {
         this.labelMassFlowRateDryBase.InitializeVariable(stream.MassFlowRateDryBase);
         this.labelMassFlowRate.InitializeVariable(stream.MassFlowRate);
         this.labelVolumeFlowRate.InitializeVariable(stream.VolumeFlowRate);
         this.labelPressure.InitializeVariable(stream.Pressure);
         this.labelTemperature.InitializeVariable(stream.Temperature);
         this.labelMoistureContentDryBase.InitializeVariable(stream.MoistureContentDryBase);
         this.labelMoistureContentWetBase.InitializeVariable(stream.MoistureContentWetBase);
         this.labelConcentration.InitializeVariable(stream.MassConcentration);
         this.labelSpecificHeat.InitializeVariable(stream.SpecificHeat);
         this.labelSpecificHeatDryBase.InitializeVariable(stream.SpecificHeatDryBase);
         this.labelDensity.InitializeVariable(stream.Density);
         this.labelSpecificEnthalpy.InitializeVariable(stream.SpecificEnthalpy);
         this.labelVaporFraction.InitializeVariable(stream.VaporFraction);
      }
   }
}
