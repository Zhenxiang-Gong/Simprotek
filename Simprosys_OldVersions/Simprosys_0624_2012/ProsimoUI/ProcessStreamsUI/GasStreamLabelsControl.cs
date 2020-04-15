using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for GasStreamLabelsControl.
   /// </summary>
   public class GasStreamLabelsControl : System.Windows.Forms.UserControl {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 240;

      private ProcessVarLabel labelDewPoint;
      private ProcessVarLabel labelHumidity;
      private ProcessVarLabel labelRelativeHumidity;
      private ProcessVarLabel labelVolumeFlowRate;
      private ProcessVarLabel labelWetBulbTemperature;
      private ProcessVarLabel labelMassFlowRate;
      private ProcessVarLabel labelTemperature;
      private ProcessVarLabel labelDensity;
      private ProcessVarLabel labelEnthalpy;
      private ProcessVarLabel labelMassFlowRateDry;
      private ProcessVarLabel labelPressure;
      private ProcessVarLabel labelSpecificHeatDryBase;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public GasStreamLabelsControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public GasStreamLabelsControl(DryingGasStream stream)
         : this() {
         this.InitializeVariableLabels(stream);
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.labelDewPoint = new ProcessVarLabel();
         this.labelHumidity = new ProcessVarLabel();
         this.labelRelativeHumidity = new ProcessVarLabel();
         this.labelVolumeFlowRate = new ProcessVarLabel();
         this.labelWetBulbTemperature = new ProcessVarLabel();
         this.labelMassFlowRate = new ProcessVarLabel();
         this.labelTemperature = new ProcessVarLabel();
         this.labelDensity = new ProcessVarLabel();
         this.labelEnthalpy = new ProcessVarLabel();
         this.labelMassFlowRateDry = new ProcessVarLabel();
         this.labelPressure = new ProcessVarLabel();
         this.labelSpecificHeatDryBase = new ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelDewPoint
         // 
         this.labelDewPoint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDewPoint.Location = new System.Drawing.Point(0, 120);
         this.labelDewPoint.Name = "labelDewPoint";
         this.labelDewPoint.Size = new System.Drawing.Size(192, 20);
         this.labelDewPoint.TabIndex = 111;
         this.labelDewPoint.Text = "DEW_POINT";
         this.labelDewPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHumidity
         // 
         this.labelHumidity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHumidity.Location = new System.Drawing.Point(0, 140);
         this.labelHumidity.Name = "labelHumidity";
         this.labelHumidity.Size = new System.Drawing.Size(192, 20);
         this.labelHumidity.TabIndex = 112;
         this.labelHumidity.Text = "HUMIDITY";
         this.labelHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelRelativeHumidity
         // 
         this.labelRelativeHumidity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelRelativeHumidity.Location = new System.Drawing.Point(0, 160);
         this.labelRelativeHumidity.Name = "labelRelativeHumidity";
         this.labelRelativeHumidity.Size = new System.Drawing.Size(192, 20);
         this.labelRelativeHumidity.TabIndex = 113;
         this.labelRelativeHumidity.Text = "RELATIVE_HUMIDITY";
         this.labelRelativeHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         // labelWetBulbTemperature
         // 
         this.labelWetBulbTemperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWetBulbTemperature.Location = new System.Drawing.Point(0, 100);
         this.labelWetBulbTemperature.Name = "labelWetBulbTemperature";
         this.labelWetBulbTemperature.Size = new System.Drawing.Size(192, 20);
         this.labelWetBulbTemperature.TabIndex = 110;
         this.labelWetBulbTemperature.Text = "WET_BULB_TEMPERATURE";
         this.labelWetBulbTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         // labelMassFlowRateDry
         // 
         this.labelMassFlowRateDry.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRateDry.Location = new System.Drawing.Point(0, 20);
         this.labelMassFlowRateDry.Name = "labelMassFlowRateDry";
         this.labelMassFlowRateDry.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRateDry.TabIndex = 106;
         this.labelMassFlowRateDry.Text = "MASS_FLOW_RATE_DRY";
         this.labelMassFlowRateDry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRate
         // 
         this.labelMassFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRate.Location = new System.Drawing.Point(0, 0);
         this.labelMassFlowRate.Name = "labelMassFlowRate";
         this.labelMassFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRate.TabIndex = 105;
         this.labelMassFlowRate.Text = "MASS_FLOW_RATE";
         this.labelMassFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         // labelSpecificHeatDryBase
         // 
         this.labelSpecificHeatDryBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeatDryBase.Location = new System.Drawing.Point(0, 200);
         this.labelSpecificHeatDryBase.Name = "labelSpecificHeatDryBase";
         this.labelSpecificHeatDryBase.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeatDryBase.TabIndex = 115;
         this.labelSpecificHeatDryBase.Text = "SPECIFIC_HEAT_DRY_BASE";
         this.labelSpecificHeatDryBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // GasStreamLabelsControl
         // 
         this.Controls.Add(this.labelDewPoint);
         this.Controls.Add(this.labelHumidity);
         this.Controls.Add(this.labelRelativeHumidity);
         this.Controls.Add(this.labelVolumeFlowRate);
         this.Controls.Add(this.labelWetBulbTemperature);
         this.Controls.Add(this.labelMassFlowRate);
         this.Controls.Add(this.labelTemperature);
         this.Controls.Add(this.labelDensity);
         this.Controls.Add(this.labelEnthalpy);
         this.Controls.Add(this.labelMassFlowRateDry);
         this.Controls.Add(this.labelPressure);
         this.Controls.Add(this.labelSpecificHeatDryBase);
         this.Name = "GasStreamLabelsControl";
         this.Size = new System.Drawing.Size(192, 240);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableLabels(DryingGasStream stream) {
         this.labelMassFlowRateDry.InitializeVariable(stream.MassFlowRateDryBase);
         this.labelMassFlowRate.InitializeVariable(stream.MassFlowRate);
         this.labelVolumeFlowRate.InitializeVariable(stream.VolumeFlowRate);
         this.labelPressure.InitializeVariable(stream.Pressure);
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
