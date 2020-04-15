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
   /// Summary description for WetScrubberValuesControl.
   /// </summary>
   public class WetScrubberValuesControl : System.Windows.Forms.UserControl
   {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 160;

      // ScrubberType
      public const int INDEX_GENERAL = 0;
      public const int INDEX_CONDENSING = 1;

      private bool inConstruction;

      private WetScrubberControl wetScrubberCtrl;
      private ProsimoUI.ProcessVarTextBox textBoxGasPressureDrop;
      private ProsimoUI.ProcessVarTextBox textBoxCollectionEfficiency;
      private ProsimoUI.ProcessVarTextBox textBoxLiquidToGasRatio;
      private ProsimoUI.ProcessVarTextBox textBoxOutletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxInletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxMassFlowRateOfParticleLostToGasOutlet;
      private ProsimoUI.ProcessVarTextBox textBoxParticleCollectionRate;
      private System.Windows.Forms.ComboBox comboBoxScrubberType;
      
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public WetScrubberValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public WetScrubberValuesControl(WetScrubberControl wetScrubberCtrl)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.wetScrubberCtrl = wetScrubberCtrl;
         this.InitializeTheUI(wetScrubberCtrl);
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose( bool disposing )
      {
         if (this.wetScrubberCtrl != null)
            this.wetScrubberCtrl.WetScrubber.SolveComplete -= new SolveCompleteEventHandler(WetScrubber_SolveComplete);
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
         this.textBoxGasPressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCollectionEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLiquidToGasRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarTextBox();
         this.textBoxParticleCollectionRate = new ProsimoUI.ProcessVarTextBox();
         this.comboBoxScrubberType = new System.Windows.Forms.ComboBox();
         this.SuspendLayout();
         // 
         // textBoxGasPressureDrop
         // 
         this.textBoxGasPressureDrop.Location = new System.Drawing.Point(0, 20);
         this.textBoxGasPressureDrop.Name = "textBoxGasPressureDrop";
         this.textBoxGasPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasPressureDrop.TabIndex = 1;
         this.textBoxGasPressureDrop.Text = "";
         this.textBoxGasPressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletParticleLoading
         // 
         this.textBoxOutletParticleLoading.Location = new System.Drawing.Point(0, 80);
         this.textBoxOutletParticleLoading.Name = "textBoxOutletParticleLoading";
         this.textBoxOutletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletParticleLoading.TabIndex = 4;
         this.textBoxOutletParticleLoading.Text = "";
         this.textBoxOutletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCollectionEfficiency
         // 
         this.textBoxCollectionEfficiency.Location = new System.Drawing.Point(0, 40);
         this.textBoxCollectionEfficiency.Name = "textBoxCollectionEfficiency";
         this.textBoxCollectionEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxCollectionEfficiency.TabIndex = 2;
         this.textBoxCollectionEfficiency.Text = "";
         this.textBoxCollectionEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCollectionEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLiquidToGasRatio
         // 
         this.textBoxLiquidToGasRatio.Location = new System.Drawing.Point(0, 140);
         this.textBoxLiquidToGasRatio.Name = "textBoxLiquidToGasRatio";
         this.textBoxLiquidToGasRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidToGasRatio.TabIndex = 7;
         this.textBoxLiquidToGasRatio.Text = "";
         this.textBoxLiquidToGasRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLiquidToGasRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletParticleLoading
         // 
         this.textBoxInletParticleLoading.Location = new System.Drawing.Point(0, 60);
         this.textBoxInletParticleLoading.Name = "textBoxInletParticleLoading";
         this.textBoxInletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletParticleLoading.TabIndex = 3;
         this.textBoxInletParticleLoading.Text = "";
         this.textBoxInletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRateOfParticleLostToGasOutlet
         // 
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 120);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Name = "textBoxMassFlowRateOfParticleLostToGasOutlet";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TabIndex = 6;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Text = "";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxParticleCollectionRate
         // 
         this.textBoxParticleCollectionRate.Location = new System.Drawing.Point(0, 100);
         this.textBoxParticleCollectionRate.Name = "textBoxParticleCollectionRate";
         this.textBoxParticleCollectionRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleCollectionRate.TabIndex = 5;
         this.textBoxParticleCollectionRate.Text = "";
         this.textBoxParticleCollectionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParticleCollectionRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxScrubberType
         // 
         this.comboBoxScrubberType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxScrubberType.Items.AddRange(new object[] {
                                                                  "General",
                                                                  "Condensing"});
         this.comboBoxScrubberType.Location = new System.Drawing.Point(0, 0);
         this.comboBoxScrubberType.Name = "comboBoxScrubberType";
         this.comboBoxScrubberType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxScrubberType.TabIndex = 8;
         this.comboBoxScrubberType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScrubberType_SelectedIndexChanged);
         // 
         // WetScrubberValuesControl
         // 
         this.Controls.Add(this.comboBoxScrubberType);
         this.Controls.Add(this.textBoxParticleCollectionRate);
         this.Controls.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.textBoxInletParticleLoading);
         this.Controls.Add(this.textBoxGasPressureDrop);
         this.Controls.Add(this.textBoxOutletParticleLoading);
         this.Controls.Add(this.textBoxCollectionEfficiency);
         this.Controls.Add(this.textBoxLiquidToGasRatio);
         this.Name = "WetScrubberValuesControl";
         this.Size = new System.Drawing.Size(80, 160);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeTheUI(WetScrubberControl wetScrubberCtrl)
      {
         this.inConstruction = true;
         this.InitializeVariableTextBoxes(wetScrubberCtrl);
         this.wetScrubberCtrl.WetScrubber.SolveComplete += new SolveCompleteEventHandler(WetScrubber_SolveComplete);
         this.comboBoxScrubberType.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetScrubberType(wetScrubberCtrl.WetScrubber.ScrubberType);
      }

      public void InitializeVariableTextBoxes(WetScrubberControl ctrl)
      {
         this.textBoxGasPressureDrop.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.GasPressureDrop);
         this.textBoxCollectionEfficiency.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.CollectionEfficiency);
         this.textBoxInletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.InletParticleLoading);
         this.textBoxOutletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.OutletParticleLoading);
         this.textBoxParticleCollectionRate.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.ParticleCollectionRate);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.MassFlowRateOfParticleLostToGasOutlet);
         this.textBoxLiquidToGasRatio.InitializeVariable(ctrl.Flowsheet, ctrl.WetScrubber.LiquidToGasRatio);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.comboBoxScrubberType);
         list.Add(this.textBoxGasPressureDrop);
         list.Add(this.textBoxCollectionEfficiency);
         list.Add(this.textBoxInletParticleLoading);
         list.Add(this.textBoxOutletParticleLoading);
         list.Add(this.textBoxParticleCollectionRate);
         list.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         list.Add(this.textBoxLiquidToGasRatio);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      public void SetScrubberType(ScrubberType config)
      {
         if (config == ScrubberType.Condensing)
            this.comboBoxScrubberType.SelectedIndex = WetScrubberValuesControl.INDEX_CONDENSING;
         else if (config == ScrubberType.General)
            this.comboBoxScrubberType.SelectedIndex = WetScrubberValuesControl.INDEX_GENERAL;
      }

      private void comboBoxScrubberType_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxScrubberType.SelectedIndex;
            if (idx == WetScrubberValuesControl.INDEX_CONDENSING)
            {
               error = this.wetScrubberCtrl.WetScrubber.SpecifyScrubberType(ScrubberType.Condensing);
            }
            else if (idx == WetScrubberValuesControl.INDEX_GENERAL)
            {
               error = this.wetScrubberCtrl.WetScrubber.SpecifyScrubberType(ScrubberType.General);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetScrubberType(this.wetScrubberCtrl.WetScrubber.ScrubberType);
            }
         }
      }

      private void WetScrubber_SolveComplete(object sender, SolveState solveState)
      {
         WetScrubber wetScrubber = sender as WetScrubber;
         this.SetScrubberType( wetScrubber.ScrubberType);
      }
   }
}
