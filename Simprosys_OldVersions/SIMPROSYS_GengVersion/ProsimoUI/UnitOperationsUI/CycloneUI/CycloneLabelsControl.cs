using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneLabelsControl.
	/// </summary>
	public class CycloneLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 100;

      private ProcessVarLabel labelPressureDrop;
      private ProcessVarLabel labelCollectionEfficiency;
      private ProcessVarLabel labelInletParticleLoading;
      private ProcessVarLabel labelMassFlowRateOfParticleLostToGasOutlet;
      private ProsimoUI.ProcessVarLabel labelOutletParticleLoading;

      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CycloneLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public CycloneLabelsControl(Cyclone uo) : this()
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
         this.labelPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.labelMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarLabel();
         this.labelInletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.labelCollectionEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelOutletParticleLoading = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelPressureDrop
         // 
         this.labelPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelPressureDrop.Name = "labelPressureDrop";
         this.labelPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelPressureDrop.TabIndex = 96;
         this.labelPressureDrop.Text = "PressureDrop";
         this.labelPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMassFlowRateOfParticleLostToGasOutlet
         // 
         this.labelMassFlowRateOfParticleLostToGasOutlet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 80);
         this.labelMassFlowRateOfParticleLostToGasOutlet.Name = "labelMassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(192, 20);
         this.labelMassFlowRateOfParticleLostToGasOutlet.TabIndex = 92;
         this.labelMassFlowRateOfParticleLostToGasOutlet.Text = "MassFlowRateOfParticleLostToGasOutlet";
         this.labelMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         // CycloneLabelsControl
         // 
         this.Controls.Add(this.labelOutletParticleLoading);
         this.Controls.Add(this.labelPressureDrop);
         this.Controls.Add(this.labelMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.labelInletParticleLoading);
         this.Controls.Add(this.labelCollectionEfficiency);
         this.Name = "CycloneLabelsControl";
         this.Size = new System.Drawing.Size(192, 100);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Cyclone uo)
      {
         this.labelPressureDrop.InitializeVariable(uo.GasPressureDrop);
         this.labelMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(uo.MassFlowRateOfParticleLostToGasOutlet);
         this.labelInletParticleLoading.InitializeVariable(uo.InletParticleLoading);
         this.labelOutletParticleLoading.InitializeVariable(uo.OutletParticleLoading);
         this.labelCollectionEfficiency.InitializeVariable(uo.CollectionEfficiency);
      }
   }
}
