using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorValuesControl.
   /// </summary>
   public class ElectrostaticPrecipitatorValuesControl : System.Windows.Forms.UserControl
   {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 160;

      private ProcessVarTextBox textBoxPressureDrop;
      private ProsimoUI.ProcessVarTextBox textBoxCollectionEfficiency;
      private ProsimoUI.ProcessVarTextBox textBoxDriftVelocity;
      private ProsimoUI.ProcessVarTextBox textBoxTotalSurfaceArea;
      private ProsimoUI.ProcessVarTextBox textBoxInletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxOutletParticleLoading;
      private ProsimoUI.ProcessVarTextBox textBoxParticleCollectionRate;
      private ProsimoUI.ProcessVarTextBox textBoxMassFlowRateOfParticleLostToGasOutlet;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ElectrostaticPrecipitatorValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ElectrostaticPrecipitatorValuesControl(ElectrostaticPrecipitatorControl electrostaticPrecipitatorCtrl) : this()
      {
         this.InitializeVariableTextBoxes(electrostaticPrecipitatorCtrl);
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
         this.textBoxPressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxParticleCollectionRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCollectionEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDriftVelocity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalSurfaceArea = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxPressureDrop
         // 
         this.textBoxPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.textBoxPressureDrop.Name = "textBoxPressureDrop";
         this.textBoxPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressureDrop.TabIndex = 1;
         this.textBoxPressureDrop.Text = "";
         this.textBoxPressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletParticleLoading
         // 
         this.textBoxInletParticleLoading.Location = new System.Drawing.Point(0, 40);
         this.textBoxInletParticleLoading.Name = "textBoxInletParticleLoading";
         this.textBoxInletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletParticleLoading.TabIndex = 3;
         this.textBoxInletParticleLoading.Text = "";
         this.textBoxInletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletParticleLoading
         // 
         this.textBoxOutletParticleLoading.Location = new System.Drawing.Point(0, 60);
         this.textBoxOutletParticleLoading.Name = "textBoxOutletParticleLoading";
         this.textBoxOutletParticleLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletParticleLoading.TabIndex = 4;
         this.textBoxOutletParticleLoading.Text = "";
         this.textBoxOutletParticleLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletParticleLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxParticleCollectionRate
         // 
         this.textBoxParticleCollectionRate.Location = new System.Drawing.Point(0, 80);
         this.textBoxParticleCollectionRate.Name = "textBoxParticleCollectionRate";
         this.textBoxParticleCollectionRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleCollectionRate.TabIndex = 5;
         this.textBoxParticleCollectionRate.Text = "";
         this.textBoxParticleCollectionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParticleCollectionRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCollectionEfficiency
         // 
         this.textBoxCollectionEfficiency.Location = new System.Drawing.Point(0, 20);
         this.textBoxCollectionEfficiency.Name = "textBoxCollectionEfficiency";
         this.textBoxCollectionEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxCollectionEfficiency.TabIndex = 2;
         this.textBoxCollectionEfficiency.Text = "";
         this.textBoxCollectionEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCollectionEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDriftVelocity
         // 
         this.textBoxDriftVelocity.Location = new System.Drawing.Point(0, 120);
         this.textBoxDriftVelocity.Name = "textBoxDriftVelocity";
         this.textBoxDriftVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxDriftVelocity.TabIndex = 7;
         this.textBoxDriftVelocity.Text = "";
         this.textBoxDriftVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxDriftVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalSurfaceArea
         // 
         this.textBoxTotalSurfaceArea.Location = new System.Drawing.Point(0, 140);
         this.textBoxTotalSurfaceArea.Name = "textBoxTotalSurfaceArea";
         this.textBoxTotalSurfaceArea.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalSurfaceArea.TabIndex = 8;
         this.textBoxTotalSurfaceArea.Text = "";
         this.textBoxTotalSurfaceArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalSurfaceArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMassFlowRateOfParticleLostToGasOutlet
         // 
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 100);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Name = "textBoxMassFlowRateOfParticleLostToGasOutlet";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TabIndex = 6;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Text = "";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // ElectrostaticPrecipitatorValuesControl
         // 
         this.Controls.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.textBoxTotalSurfaceArea);
         this.Controls.Add(this.textBoxDriftVelocity);
         this.Controls.Add(this.textBoxCollectionEfficiency);
         this.Controls.Add(this.textBoxParticleCollectionRate);
         this.Controls.Add(this.textBoxPressureDrop);
         this.Controls.Add(this.textBoxInletParticleLoading);
         this.Controls.Add(this.textBoxOutletParticleLoading);
         this.Name = "ElectrostaticPrecipitatorValuesControl";
         this.Size = new System.Drawing.Size(80, 160);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableTextBoxes(ElectrostaticPrecipitatorControl ctrl)
      {
         this.textBoxPressureDrop.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.GasPressureDrop);
         this.textBoxInletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.InletParticleLoading);
         this.textBoxOutletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.OutletParticleLoading);
         this.textBoxParticleCollectionRate.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.ParticleCollectionRate);
         this.textBoxCollectionEfficiency.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.CollectionEfficiency);
         this.textBoxDriftVelocity.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.DriftVelocity);
         this.textBoxTotalSurfaceArea.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.TotalSurfaceArea);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(ctrl.Flowsheet, ctrl.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxPressureDrop);
         list.Add(this.textBoxCollectionEfficiency);
         list.Add(this.textBoxInletParticleLoading);
         list.Add(this.textBoxOutletParticleLoading);
         list.Add(this.textBoxParticleCollectionRate);
         list.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         list.Add(this.textBoxDriftVelocity);
         list.Add(this.textBoxTotalSurfaceArea);

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
