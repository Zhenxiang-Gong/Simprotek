using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for ScrubberCondenserLabelsControl.
   /// </summary>
   public class ScrubberCondenserLabelsControl : System.Windows.Forms.UserControl
   {

      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 200;
      private ProcessVarLabel labelCollectionEfficiency;
      private ProsimoUI.ProcessVarLabel labelGasPressureDrop;
      private ProsimoUI.ProcessVarLabel labelCoolingDuty;
      private ProsimoUI.ProcessVarLabel labelInletParticleLoading;
      private ProsimoUI.ProcessVarLabel labelOutletParticleLoading;
      private ProsimoUI.ProcessVarLabel labelParticleCollectionRate;
      private ProsimoUI.ProcessVarLabel labelMassFlowRateOfParticleLostToGasOutlet;
      private ProcessVarLabel labelLiquidToGasRatio;
      private ProcessVarLabel labelLiquidRecirculationMassFlowRate;
      private ProcessVarLabel labelLiquidRecirculationVolumeFlowRate;

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ScrubberCondenserLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ScrubberCondenserLabelsControl(ScrubberCondenser uo)
         : this()
      {
         this.InitializeVariableLabels(uo);
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
         this.labelGasPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.labelCoolingDuty = new ProsimoUI.ProcessVarLabel();
         this.labelInletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.labelCollectionEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelOutletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.labelParticleCollectionRate = new ProsimoUI.ProcessVarLabel();
         this.labelMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarLabel();
         this.labelLiquidToGasRatio = new ProsimoUI.ProcessVarLabel();
         this.labelLiquidRecirculationMassFlowRate = new ProsimoUI.ProcessVarLabel();
         this.labelLiquidRecirculationVolumeFlowRate = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelGasPressureDrop
         // 
         this.labelGasPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelGasPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelGasPressureDrop.Name = "labelGasPressureDrop";
         this.labelGasPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelGasPressureDrop.TabIndex = 96;
         this.labelGasPressureDrop.Text = "GasPressureDrop";
         this.labelGasPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCoolingDuty
         // 
         this.labelCoolingDuty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCoolingDuty.Location = new System.Drawing.Point(0, 120);
         this.labelCoolingDuty.Name = "labelCoolingDuty";
         this.labelCoolingDuty.Size = new System.Drawing.Size(192, 20);
         this.labelCoolingDuty.TabIndex = 92;
         this.labelCoolingDuty.Text = "CoolingDuty";
         this.labelCoolingDuty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletParticleLoading
         // 
         this.labelInletParticleLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletParticleLoading.Location = new System.Drawing.Point(0, 40);
         this.labelInletParticleLoading.Name = "labelInletParticleLoading";
         this.labelInletParticleLoading.Size = new System.Drawing.Size(192, 20);
         this.labelInletParticleLoading.TabIndex = 97;
         this.labelInletParticleLoading.Text = "InletParticleLoading";
         this.labelInletParticleLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCollectionEfficiency
         // 
         this.labelCollectionEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCollectionEfficiency.Location = new System.Drawing.Point(0, 20);
         this.labelCollectionEfficiency.Name = "labelCollectionEfficiency";
         this.labelCollectionEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelCollectionEfficiency.TabIndex = 96;
         this.labelCollectionEfficiency.Text = "CollectionEfficiency";
         this.labelCollectionEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletParticleLoading
         // 
         this.labelOutletParticleLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletParticleLoading.Location = new System.Drawing.Point(0, 60);
         this.labelOutletParticleLoading.Name = "labelOutletParticleLoading";
         this.labelOutletParticleLoading.Size = new System.Drawing.Size(192, 20);
         this.labelOutletParticleLoading.TabIndex = 98;
         this.labelOutletParticleLoading.Text = "OutletParticleLoading";
         this.labelOutletParticleLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelParticleCollectionRate
         // 
         this.labelParticleCollectionRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelParticleCollectionRate.Location = new System.Drawing.Point(0, 80);
         this.labelParticleCollectionRate.Name = "labelParticleCollectionRate";
         this.labelParticleCollectionRate.Size = new System.Drawing.Size(192, 20);
         this.labelParticleCollectionRate.TabIndex = 99;
         this.labelParticleCollectionRate.Text = "ParticleCollectionRate";
         this.labelParticleCollectionRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRateOfParticleLostToGasOutlet
         // 
         this.labelMassFlowRateOfParticleLostToGasOutlet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 100);
         this.labelMassFlowRateOfParticleLostToGasOutlet.Name = "labelMassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRateOfParticleLostToGasOutlet.TabIndex = 100;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Text = "MassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelLiquidToGasRatio
         // 
         this.labelLiquidToGasRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLiquidToGasRatio.Location = new System.Drawing.Point(0, 140);
         this.labelLiquidToGasRatio.Name = "labelLiquidToGasRatio";
         this.labelLiquidToGasRatio.Size = new System.Drawing.Size(192, 20);
         this.labelLiquidToGasRatio.TabIndex = 101;
         this.labelLiquidToGasRatio.Text = "LiquidToGasRatio";
         this.labelLiquidToGasRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelLiquidRecirculationMassFlowRate
         // 
         this.labelLiquidRecirculationMassFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLiquidRecirculationMassFlowRate.Location = new System.Drawing.Point(0, 160);
         this.labelLiquidRecirculationMassFlowRate.Name = "labelLiquidRecirculationMassFlowRate";
         this.labelLiquidRecirculationMassFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelLiquidRecirculationMassFlowRate.TabIndex = 102;
         this.labelLiquidRecirculationMassFlowRate.Text = "LiquidRecirculationMassFlowRate";
         this.labelLiquidRecirculationMassFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelLiquidRecirculationVolumeFlowRate
         // 
         this.labelLiquidRecirculationVolumeFlowRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLiquidRecirculationVolumeFlowRate.Location = new System.Drawing.Point(0, 180);
         this.labelLiquidRecirculationVolumeFlowRate.Name = "labelLiquidRecirculationVolumeFlowRate";
         this.labelLiquidRecirculationVolumeFlowRate.Size = new System.Drawing.Size(192, 20);
         this.labelLiquidRecirculationVolumeFlowRate.TabIndex = 103;
         this.labelLiquidRecirculationVolumeFlowRate.Text = "LiquidRecirculationVolumeFlowRate";
         this.labelLiquidRecirculationVolumeFlowRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ScrubberCondenserLabelsControl
         // 
         this.Controls.Add(this.labelLiquidRecirculationVolumeFlowRate);
         this.Controls.Add(this.labelLiquidRecirculationMassFlowRate);
         this.Controls.Add(this.labelLiquidToGasRatio);
         this.Controls.Add(this.labelMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.labelParticleCollectionRate);
         this.Controls.Add(this.labelOutletParticleLoading);
         this.Controls.Add(this.labelGasPressureDrop);
         this.Controls.Add(this.labelCoolingDuty);
         this.Controls.Add(this.labelInletParticleLoading);
         this.Controls.Add(this.labelCollectionEfficiency);
         this.Name = "ScrubberCondenserLabelsControl";
         this.Size = new System.Drawing.Size(192, 200);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableLabels(ScrubberCondenser uo)
      {
         this.labelGasPressureDrop.InitializeVariable(uo.GasPressureDrop);
         this.labelCollectionEfficiency.InitializeVariable(uo.CollectionEfficiency);
         this.labelInletParticleLoading.InitializeVariable(uo.InletParticleLoading);
         this.labelOutletParticleLoading.InitializeVariable(uo.OutletParticleLoading);
         this.labelParticleCollectionRate.InitializeVariable(uo.ParticleCollectionRate);
         this.labelMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(uo.MassFlowRateOfParticleLostToGasOutlet);
         this.labelCoolingDuty.InitializeVariable(uo.CoolingDuty);
         this.labelLiquidToGasRatio.InitializeVariable(uo.LiquidToGasRatio);
         this.labelLiquidRecirculationMassFlowRate.InitializeVariable(uo.LiquidRecirculationMassFlowRate);
         this.labelLiquidRecirculationVolumeFlowRate.InitializeVariable(uo.LiquidRecirculationVolumeFlowRate);
      }
   }
}
