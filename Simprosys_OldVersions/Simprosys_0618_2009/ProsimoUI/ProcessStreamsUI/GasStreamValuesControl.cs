using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for GasStreamValuesControl.
   /// </summary>
   public class GasStreamValuesControl : System.Windows.Forms.UserControl {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 240;

      private ProcessVarTextBox textBoxDensity;
      private ProcessVarTextBox textBoxRelativeHumidity;
      private ProcessVarTextBox textBoxDewPoint;
      private ProcessVarTextBox textBoxWetBulbTemperature;
      private ProcessVarTextBox textBoxTemperature;
      private ProcessVarTextBox textBoxPressure;
      private ProcessVarTextBox textBoxMassFlowRateDry;
      private ProcessVarTextBox textBoxSpecificHeatDryBase;
      private ProcessVarTextBox textBoxMassFlowRate;
      private ProcessVarTextBox textBoxEnthalpy;
      private ProcessVarTextBox textBoxVolumeFlowRate;
      private ProcessVarTextBox textBoxHumidity;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public GasStreamValuesControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public GasStreamValuesControl(GasStreamControl gasStreamCtrl)
         : this() {
         this.InitializeVariableTextBoxes(gasStreamCtrl);
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
         this.textBoxDensity = new ProcessVarTextBox();
         this.textBoxRelativeHumidity = new ProcessVarTextBox();
         this.textBoxDewPoint = new ProcessVarTextBox();
         this.textBoxWetBulbTemperature = new ProcessVarTextBox();
         this.textBoxTemperature = new ProcessVarTextBox();
         this.textBoxPressure = new ProcessVarTextBox();
         this.textBoxMassFlowRateDry = new ProcessVarTextBox();
         this.textBoxSpecificHeatDryBase = new ProcessVarTextBox();
         this.textBoxMassFlowRate = new ProcessVarTextBox();
         this.textBoxEnthalpy = new ProcessVarTextBox();
         this.textBoxVolumeFlowRate = new ProcessVarTextBox();
         this.textBoxHumidity = new ProcessVarTextBox();
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
         // textBoxRelativeHumidity
         // 
         this.textBoxRelativeHumidity.Location = new System.Drawing.Point(0, 160);
         this.textBoxRelativeHumidity.Name = "textBoxRelativeHumidity";
         this.textBoxRelativeHumidity.Size = new System.Drawing.Size(80, 20);
         this.textBoxRelativeHumidity.TabIndex = 112;
         this.textBoxRelativeHumidity.Text = "";
         this.textBoxRelativeHumidity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDewPoint
         // 
         this.textBoxDewPoint.Location = new System.Drawing.Point(0, 120);
         this.textBoxDewPoint.Name = "textBoxDewPoint";
         this.textBoxDewPoint.Size = new System.Drawing.Size(80, 20);
         this.textBoxDewPoint.TabIndex = 110;
         this.textBoxDewPoint.Text = "";
         this.textBoxDewPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxWetBulbTemperature
         // 
         this.textBoxWetBulbTemperature.Location = new System.Drawing.Point(0, 100);
         this.textBoxWetBulbTemperature.Name = "textBoxWetBulbTemperature";
         this.textBoxWetBulbTemperature.Size = new System.Drawing.Size(80, 20);
         this.textBoxWetBulbTemperature.TabIndex = 109;
         this.textBoxWetBulbTemperature.Text = "";
         this.textBoxWetBulbTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
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
         // textBoxSpecificHeatDryBase
         // 
         this.textBoxSpecificHeatDryBase.Location = new System.Drawing.Point(0, 200);
         this.textBoxSpecificHeatDryBase.Name = "textBoxSpecificHeatDryBase";
         this.textBoxSpecificHeatDryBase.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeatDryBase.TabIndex = 114;
         this.textBoxSpecificHeatDryBase.Text = "";
         this.textBoxSpecificHeatDryBase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRateDry
         // 
         this.textBoxMassFlowRateDry.Location = new System.Drawing.Point(0, 20);
         this.textBoxMassFlowRateDry.Name = "textBoxMassFlowRateDry";
         this.textBoxMassFlowRateDry.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateDry.TabIndex = 105;
         this.textBoxMassFlowRateDry.Text = "";
         this.textBoxMassFlowRateDry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRate
         // 
         this.textBoxMassFlowRate.Location = new System.Drawing.Point(0, 0);
         this.textBoxMassFlowRate.Name = "textBoxMassFlowRate";
         this.textBoxMassFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRate.TabIndex = 104;
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
         // textBoxHumidity
         // 
         this.textBoxHumidity.Location = new System.Drawing.Point(0, 140);
         this.textBoxHumidity.Name = "textBoxHumidity";
         this.textBoxHumidity.Size = new System.Drawing.Size(80, 20);
         this.textBoxHumidity.TabIndex = 111;
         this.textBoxHumidity.Text = "";
         this.textBoxHumidity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // GasStreamValuesControl
         // 
         this.Controls.Add(this.textBoxDensity);
         this.Controls.Add(this.textBoxRelativeHumidity);
         this.Controls.Add(this.textBoxDewPoint);
         this.Controls.Add(this.textBoxWetBulbTemperature);
         this.Controls.Add(this.textBoxTemperature);
         this.Controls.Add(this.textBoxPressure);
         this.Controls.Add(this.textBoxMassFlowRateDry);
         this.Controls.Add(this.textBoxSpecificHeatDryBase);
         this.Controls.Add(this.textBoxMassFlowRate);
         this.Controls.Add(this.textBoxEnthalpy);
         this.Controls.Add(this.textBoxVolumeFlowRate);
         this.Controls.Add(this.textBoxHumidity);
         this.Name = "GasStreamValuesControl";
         this.Size = new System.Drawing.Size(80, 240);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableTextBoxes(GasStreamControl ctrl) {
         this.textBoxMassFlowRateDry.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.MassFlowRateDryBase);
         this.textBoxMassFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.MassFlowRate);
         this.textBoxVolumeFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.VolumeFlowRate);
         this.textBoxPressure.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.Pressure);
         this.textBoxTemperature.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.Temperature);
         this.textBoxWetBulbTemperature.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.WetBulbTemperature);
         this.textBoxDewPoint.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.DewPoint);
         this.textBoxHumidity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.Humidity);
         this.textBoxRelativeHumidity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.RelativeHumidity);
         this.textBoxEnthalpy.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.SpecificEnthalpy);
         this.textBoxSpecificHeatDryBase.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.SpecificHeatDryBase);
         this.textBoxDensity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.GasStream.Density);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxMassFlowRate);
         list.Add(this.textBoxMassFlowRateDry);
         list.Add(this.textBoxVolumeFlowRate);
         list.Add(this.textBoxPressure);
         list.Add(this.textBoxTemperature);
         list.Add(this.textBoxWetBulbTemperature);
         list.Add(this.textBoxDewPoint);
         list.Add(this.textBoxHumidity);
         list.Add(this.textBoxRelativeHumidity);
         list.Add(this.textBoxEnthalpy);
         list.Add(this.textBoxSpecificHeatDryBase);
         list.Add(this.textBoxDensity);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
