using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorLabelsControl.
   /// </summary>
   public class ElectrostaticPrecipitatorLabelsControl : System.Windows.Forms.UserControl
   {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 160;
      private ProsimoUI.ProcessVarLabel labelPressureDrop;
      private ProsimoUI.ProcessVarLabel labelCollectionEfficiency;
      private ProsimoUI.ProcessVarLabel labelDriftVelocity;
      private ProsimoUI.ProcessVarLabel labelTotalSurfaceArea;
      private ProsimoUI.ProcessVarLabel labelOutletParticleLoading;
      private ProsimoUI.ProcessVarLabel labelInletParticleLoading;
      private ProsimoUI.ProcessVarLabel labelParticleCollectionRate;
      private ProsimoUI.ProcessVarLabel labelMassFlowRateOfParticleLostToGasOutlet;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ElectrostaticPrecipitatorLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ElectrostaticPrecipitatorLabelsControl(ElectrostaticPrecipitator uo) : this()
      {
         this.InitializeVariableLabels(uo);
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
         this.labelOutletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.labelInletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.labelPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.labelParticleCollectionRate = new ProsimoUI.ProcessVarLabel();
         this.labelCollectionEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelDriftVelocity = new ProsimoUI.ProcessVarLabel();
         this.labelTotalSurfaceArea = new ProsimoUI.ProcessVarLabel();
         this.labelMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelOutletParticleLoading
         // 
         this.labelOutletParticleLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletParticleLoading.Location = new System.Drawing.Point(0, 60);
         this.labelOutletParticleLoading.Name = "labelOutletParticleLoading";
         this.labelOutletParticleLoading.Size = new System.Drawing.Size(192, 20);
         this.labelOutletParticleLoading.TabIndex = 116;
         this.labelOutletParticleLoading.Text = "OutletParticleLoading";
         this.labelOutletParticleLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletParticleLoading
         // 
         this.labelInletParticleLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletParticleLoading.Location = new System.Drawing.Point(0, 40);
         this.labelInletParticleLoading.Name = "labelInletParticleLoading";
         this.labelInletParticleLoading.Size = new System.Drawing.Size(192, 20);
         this.labelInletParticleLoading.TabIndex = 117;
         this.labelInletParticleLoading.Text = "InletParticleLoading";
         this.labelInletParticleLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPressureDrop
         // 
         this.labelPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelPressureDrop.Name = "labelPressureDrop";
         this.labelPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelPressureDrop.TabIndex = 118;
         this.labelPressureDrop.Text = "PressureDrop";
         this.labelPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelParticleCollectionRate
         // 
         this.labelParticleCollectionRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelParticleCollectionRate.Location = new System.Drawing.Point(0, 80);
         this.labelParticleCollectionRate.Name = "labelParticleCollectionRate";
         this.labelParticleCollectionRate.Size = new System.Drawing.Size(192, 20);
         this.labelParticleCollectionRate.TabIndex = 119;
         this.labelParticleCollectionRate.Text = "ParticleCollectionRate";
         this.labelParticleCollectionRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCollectionEfficiency
         // 
         this.labelCollectionEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCollectionEfficiency.Location = new System.Drawing.Point(0, 20);
         this.labelCollectionEfficiency.Name = "labelCollectionEfficiency";
         this.labelCollectionEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelCollectionEfficiency.TabIndex = 120;
         this.labelCollectionEfficiency.Text = "CollectionEfficiency";
         this.labelCollectionEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDriftVelocity
         // 
         this.labelDriftVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDriftVelocity.Location = new System.Drawing.Point(0, 120);
         this.labelDriftVelocity.Name = "labelDriftVelocity";
         this.labelDriftVelocity.Size = new System.Drawing.Size(192, 20);
         this.labelDriftVelocity.TabIndex = 121;
         this.labelDriftVelocity.Text = "DriftVelocity";
         this.labelDriftVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalSurfaceArea
         // 
         this.labelTotalSurfaceArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalSurfaceArea.Location = new System.Drawing.Point(0, 140);
         this.labelTotalSurfaceArea.Name = "labelTotalSurfaceArea";
         this.labelTotalSurfaceArea.Size = new System.Drawing.Size(192, 20);
         this.labelTotalSurfaceArea.TabIndex = 122;
         this.labelTotalSurfaceArea.Text = "TotalSurfaceArea";
         this.labelTotalSurfaceArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRateOfParticleLostToGasOutlet
         // 
         this.labelMassFlowRateOfParticleLostToGasOutlet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 100);
         this.labelMassFlowRateOfParticleLostToGasOutlet.Name = "labelMassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRateOfParticleLostToGasOutlet.TabIndex = 123;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Text = "MassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ElectrostaticPrecipitatorLabelsControl
         // 
         this.Controls.Add(this.labelMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.labelTotalSurfaceArea);
         this.Controls.Add(this.labelDriftVelocity);
         this.Controls.Add(this.labelCollectionEfficiency);
         this.Controls.Add(this.labelParticleCollectionRate);
         this.Controls.Add(this.labelPressureDrop);
         this.Controls.Add(this.labelInletParticleLoading);
         this.Controls.Add(this.labelOutletParticleLoading);
         this.Name = "ElectrostaticPrecipitatorLabelsControl";
         this.Size = new System.Drawing.Size(192, 160);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableLabels(ElectrostaticPrecipitator uo)
      {
         this.labelPressureDrop.InitializeVariable(uo.GasPressureDrop);
         this.labelCollectionEfficiency.InitializeVariable(uo.CollectionEfficiency);
         this.labelInletParticleLoading.InitializeVariable(uo.InletParticleLoading);
         this.labelOutletParticleLoading.InitializeVariable(uo.OutletParticleLoading);
         this.labelParticleCollectionRate.InitializeVariable(uo.ParticleCollectionRate);
         this.labelMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(uo.MassFlowRateOfParticleLostToGasOutlet);
         this.labelDriftVelocity.InitializeVariable(uo.DriftVelocity);
         this.labelTotalSurfaceArea.InitializeVariable(uo.TotalSurfaceArea);
      }
   }
}
