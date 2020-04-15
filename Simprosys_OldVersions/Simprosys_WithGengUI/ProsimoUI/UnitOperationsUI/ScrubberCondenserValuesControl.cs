using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for ScrubberCondenserValuesControl.
   /// </summary>
   public class ScrubberCondenserValuesControl : System.Windows.Forms.UserControl
   {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 200;

      private ScrubberCondenserControl scrubberCondenserCtrl;
      private ProsimoUI.ProcessVarTextBox textBoxGasPressureDrop;
      private ProsimoUI.ProcessVarTextBox textBoxCollectionEfficiency;
      private ProsimoUI.ProcessVarTextBox textBoxLiquidToGasRatio;
      private ProsimoUI.ProcessVarTextBox textBoxOutletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxInletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxMassFlowRateOfParticleLostToGasOutlet;
      private ProsimoUI.ProcessVarTextBox textBoxParticleCollectionRate;
      private ProcessVarTextBox textBoxCoolingDuty;
      private ProcessVarTextBox textBoxLiquidRecirculationMassFlowRate;
      private ProcessVarTextBox textBoxLiquidRecirculationVolumeFlowRate;

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ScrubberCondenserValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ScrubberCondenserValuesControl(ScrubberCondenserControl scrubberCondenserCtrl)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.scrubberCondenserCtrl = scrubberCondenserCtrl;
         this.InitializeVariableTextBoxes(scrubberCondenserCtrl);
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
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
      private void InitializeComponent()
      {
         this.textBoxGasPressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCollectionEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLiquidToGasRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarTextBox();
         this.textBoxParticleCollectionRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCoolingDuty = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLiquidRecirculationMassFlowRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLiquidRecirculationVolumeFlowRate = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxGasPressureDrop
         // 
         this.textBoxGasPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.textBoxGasPressureDrop.Name = "textBoxGasPressureDrop";
         this.textBoxGasPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasPressureDrop.TabIndex = 1;
         this.textBoxGasPressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletParticleLoading
         // 
         this.textBoxOutletParticleLoading.Location = new System.Drawing.Point(0, 60);
         this.textBoxOutletParticleLoading.Name = "textBoxOutletParticleLoading";
         this.textBoxOutletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletParticleLoading.TabIndex = 4;
         this.textBoxOutletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCollectionEfficiency
         // 
         this.textBoxCollectionEfficiency.Location = new System.Drawing.Point(0, 20);
         this.textBoxCollectionEfficiency.Name = "textBoxCollectionEfficiency";
         this.textBoxCollectionEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxCollectionEfficiency.TabIndex = 2;
         this.textBoxCollectionEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCollectionEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLiquidToGasRatio
         // 
         this.textBoxLiquidToGasRatio.Location = new System.Drawing.Point(0, 140);
         this.textBoxLiquidToGasRatio.Name = "textBoxLiquidToGasRatio";
         this.textBoxLiquidToGasRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidToGasRatio.TabIndex = 8;
         this.textBoxLiquidToGasRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLiquidToGasRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletParticleLoading
         // 
         this.textBoxInletParticleLoading.Location = new System.Drawing.Point(0, 40);
         this.textBoxInletParticleLoading.Name = "textBoxInletParticleLoading";
         this.textBoxInletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletParticleLoading.TabIndex = 3;
         this.textBoxInletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRateOfParticleLostToGasOutlet
         // 
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 100);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Name = "textBoxMassFlowRateOfParticleLostToGasOutlet";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TabIndex = 6;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxParticleCollectionRate
         // 
         this.textBoxParticleCollectionRate.Location = new System.Drawing.Point(0, 80);
         this.textBoxParticleCollectionRate.Name = "textBoxParticleCollectionRate";
         this.textBoxParticleCollectionRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleCollectionRate.TabIndex = 5;
         this.textBoxParticleCollectionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParticleCollectionRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCoolingDuty
         // 
         this.textBoxCoolingDuty.Location = new System.Drawing.Point(0, 120);
         this.textBoxCoolingDuty.Name = "textBoxCoolingDuty";
         this.textBoxCoolingDuty.Size = new System.Drawing.Size(80, 20);
         this.textBoxCoolingDuty.TabIndex = 7;
         this.textBoxCoolingDuty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCoolingDuty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLiquidRecirculationMassFlowRate
         // 
         this.textBoxLiquidRecirculationMassFlowRate.Location = new System.Drawing.Point(0, 160);
         this.textBoxLiquidRecirculationMassFlowRate.Name = "textBoxLiquidRecirculationMassFlowRate";
         this.textBoxLiquidRecirculationMassFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidRecirculationMassFlowRate.TabIndex = 9;
         this.textBoxLiquidRecirculationMassFlowRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLiquidRecirculationMassFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLiquidRecirculationVolumeFlowRate
         // 
         this.textBoxLiquidRecirculationVolumeFlowRate.Location = new System.Drawing.Point(0, 180);
         this.textBoxLiquidRecirculationVolumeFlowRate.Name = "textBoxLiquidRecirculationVolumeFlowRate";
         this.textBoxLiquidRecirculationVolumeFlowRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidRecirculationVolumeFlowRate.TabIndex = 10;
         this.textBoxLiquidRecirculationVolumeFlowRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLiquidRecirculationVolumeFlowRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // ScrubberCondenserValuesControl
         // 
         this.Controls.Add(this.textBoxLiquidRecirculationVolumeFlowRate);
         this.Controls.Add(this.textBoxLiquidRecirculationMassFlowRate);
         this.Controls.Add(this.textBoxCoolingDuty);
         this.Controls.Add(this.textBoxParticleCollectionRate);
         this.Controls.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.textBoxInletParticleLoading);
         this.Controls.Add(this.textBoxGasPressureDrop);
         this.Controls.Add(this.textBoxOutletParticleLoading);
         this.Controls.Add(this.textBoxCollectionEfficiency);
         this.Controls.Add(this.textBoxLiquidToGasRatio);
         this.Name = "ScrubberCondenserValuesControl";
         this.Size = new System.Drawing.Size(80, 200);
         this.ResumeLayout(false);
         this.PerformLayout();

      }
      #endregion

      public void InitializeVariableTextBoxes(ScrubberCondenserControl ctrl)
      {
         this.textBoxGasPressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.GasPressureDrop);
         this.textBoxCollectionEfficiency.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.CollectionEfficiency);
         this.textBoxInletParticleLoading.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.InletParticleLoading);
         this.textBoxOutletParticleLoading.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.OutletParticleLoading);
         this.textBoxParticleCollectionRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.ParticleCollectionRate);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet);
         this.textBoxLiquidToGasRatio.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.LiquidToGasRatio);
         this.textBoxCoolingDuty.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.CoolingDuty);
         this.textBoxLiquidRecirculationMassFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.LiquidRecirculationMassFlowRate);
         this.textBoxLiquidRecirculationVolumeFlowRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.ScrubberCondenser.LiquidRecirculationVolumeFlowRate);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxGasPressureDrop);
         list.Add(this.textBoxCollectionEfficiency);
         list.Add(this.textBoxInletParticleLoading);
         list.Add(this.textBoxOutletParticleLoading);
         list.Add(this.textBoxParticleCollectionRate);
         list.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         list.Add(this.textBoxCoolingDuty);
         list.Add(this.textBoxLiquidToGasRatio);
         list.Add(this.textBoxLiquidRecirculationMassFlowRate);
         list.Add(this.textBoxLiquidRecirculationVolumeFlowRate);

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
