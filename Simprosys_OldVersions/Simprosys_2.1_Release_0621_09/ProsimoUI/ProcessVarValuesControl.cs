using System.Collections;
using System.Windows.Forms;

using Prosimo;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for ProcessStreamValuesControl.
   /// </summary>
   public class ProcessVarValuesControl : System.Windows.Forms.UserControl {
      // these need to be in sync with the dimensions of this control
      //public const int WIDTH = 80;
      //public const int HEIGHT = 240;

      //private ArrayList varList;
      private ArrayList varTextBoxeList;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcessVarValuesControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ProcessVarValuesControl(SolvableControl solvableCtrl) {
         InitializeVarValuesControl(solvableCtrl);
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
         //this.textBoxDensity = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxTemperature = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxPressure  new ProsimoUI.ProcessVarTextBox();
         //this.textBoxSpecificHeat = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxMassFlowRate = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxVaporFraction = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxEnthalpy = new ProsimoUI.ProcessVarTextBox();
         //this.textBoxVolumeFlowRate = new ProsimoUI.ProcessVarTextBox();
         //this.SuspendLayout();
         //// 
         //// textBoxDensity
         //// 
         //this.textBoxDensity.Location = new System.Drawing.Point(0, 140);
         //this.textBoxDensity.Name = "textBoxDensity";
         //this.textBoxDensity.Size = new System.Drawing.Size(80, 20);
         //this.textBoxDensity.TabIndex = 115;
         //this.textBoxDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxTemperature
         //// 
         //this.textBoxTemperature.Location = new System.Drawing.Point(0, 60);
         //this.textBoxTemperature.Name = "textBoxTemperature";
         //this.textBoxTemperature.Size = new System.Drawing.Size(80, 20);
         //this.textBoxTemperature.TabIndex = 108;
         //this.textBoxTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxTemperature.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxPressure
         //// 
         //this.textBoxPressure.Location = new System.Drawing.Point(0, 40);
         //this.textBoxPressure.Name = "textBoxPressure";
         //this.textBoxPressure.Size = new System.Drawing.Size(80, 20);
         //this.textBoxPressure.TabIndex = 107;
         //this.textBoxPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxPressure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxSpecificHeat
         //// 
         //this.textBoxSpecificHeat.Location = new System.Drawing.Point(0, 120);
         //this.textBoxSpecificHeat.Name = "textBoxSpecificHeat";
         //this.textBoxSpecificHeat.Size = new System.Drawing.Size(80, 20);
         //this.textBoxSpecificHeat.TabIndex = 114;
         //this.textBoxSpecificHeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxSpecificHeat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxMassFlowRate
         //// 
         //this.textBoxMassFlowRate.Location = new System.Drawing.Point(0, 0);
         //this.textBoxMassFlowRate.Name = "textBoxMassFlowRate";
         //this.textBoxMassFlowRate.Size = new System.Drawing.Size(80, 20);
         //this.textBoxMassFlowRate.TabIndex = 105;
         //this.textBoxMassFlowRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxMassFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxEnthalpy
         //// 
         //this.textBoxEnthalpy.Location = new System.Drawing.Point(0, 100);
         //this.textBoxEnthalpy.Name = "textBoxEnthalpy";
         //this.textBoxEnthalpy.Size = new System.Drawing.Size(80, 20);
         //this.textBoxEnthalpy.TabIndex = 113;
         //this.textBoxEnthalpy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxEnthalpy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxVaporFraction
         //// 
         //this.textBoxVaporFraction.Location = new System.Drawing.Point(0, 80);
         //this.textBoxVaporFraction.Name = "textBoxVaporFraction";
         //this.textBoxVaporFraction.Size = new System.Drawing.Size(80, 20);
         //this.textBoxVaporFraction.TabIndex = 113;
         //this.textBoxVaporFraction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxVaporFraction.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// textBoxVolumeFlowRate
         //// 
         //this.textBoxVolumeFlowRate.Location = new System.Drawing.Point(0, 20);
         //this.textBoxVolumeFlowRate.Name = "textBoxVolumeFlowRate";
         //this.textBoxVolumeFlowRate.Size = new System.Drawing.Size(80, 20);
         //this.textBoxVolumeFlowRate.TabIndex = 106;
         //this.textBoxVolumeFlowRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         //this.textBoxVolumeFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         //// 
         //// WaterStreamValuesControl
         //// 
         //this.Controls.Add(this.textBoxDensity);
         //this.Controls.Add(this.textBoxTemperature);
         //this.Controls.Add(this.textBoxPressure);
         //this.Controls.Add(this.textBoxSpecificHeat);
         //this.Controls.Add(this.textBoxMassFlowRate);
         //this.Controls.Add(this.textBoxVaporFraction);
         //this.Controls.Add(this.textBoxEnthalpy);
         //this.Controls.Add(this.textBoxVolumeFlowRate);
         //this.Name = "WaterStreamValuesControl";
         //this.Size = new System.Drawing.Size(80, 160);
         //this.ResumeLayout(false);
         //this.PerformLayout();
      }
      #endregion

      public void InitializeVarValuesControl(SolvableControl sovableCtrl) {
         ArrayList varList = sovableCtrl.Solvable.VarList;
         ProcessVarTextBox varTextBox;
         varTextBoxeList = new ArrayList();
         this.SuspendLayout();
         for (int i = 0; i < varList.Count; i++) {
            varTextBox = new ProcessVarTextBox();
            varTextBoxeList.Add(varTextBox);
            varTextBox.Location = new System.Drawing.Point(0, i * 20);
            varTextBox.Size = new System.Drawing.Size(80, 20);
            varTextBox.TabIndex = 110 + i;
            varTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            varTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
            this.Controls.Add(varTextBox);
            varTextBox.InitializeVariable(sovableCtrl.Flowsheet.ApplicationPrefs, ((ProcessVar)varList[i]));
         }

         this.Size = new System.Drawing.Size(80, varTextBoxeList.Count * 20);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(varTextBoxeList, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(varTextBoxeList, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
