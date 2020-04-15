using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FabricFilterValuesControl.
	/// </summary>
	public class FabricFilterValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 160;

      protected ProcessVarTextBox textBoxPressureDrop;
      protected ProcessVarTextBox textBoxCollectionEfficiency;
      protected ProcessVarTextBox textBoxGasToClothRatio;
      protected ProcessVarTextBox textBoxTotalFilteringArea;
      protected ProsimoUI.ProcessVarTextBox textBoxInletParticleLoading;
      protected ProsimoUI.ProcessVarTextBox textBoxOutletParticleLoading;
      protected ProsimoUI.ProcessVarTextBox textBoxParticleCollectionRate;
      protected ProsimoUI.ProcessVarTextBox textBoxMassFlowRateOfParticleLostToGasOutlet;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FabricFilterValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FabricFilterValuesControl(FabricFilterControl fabricFilterCtrl) : this()
		{
         this.InitializeVariableTextBoxes(fabricFilterCtrl);
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
         this.textBoxParticleCollectionRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCollectionEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxGasToClothRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalFilteringArea = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletParticleLoading = new ProsimoUI.ProcessVarTextBox();
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
         // textBoxGasToClothRatio
         // 
         this.textBoxGasToClothRatio.Location = new System.Drawing.Point(0, 120);
         this.textBoxGasToClothRatio.Name = "textBoxGasToClothRatio";
         this.textBoxGasToClothRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasToClothRatio.TabIndex = 7;
         this.textBoxGasToClothRatio.Text = "";
         this.textBoxGasToClothRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasToClothRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalFilteringArea
         // 
         this.textBoxTotalFilteringArea.Location = new System.Drawing.Point(0, 140);
         this.textBoxTotalFilteringArea.Name = "textBoxTotalFilteringArea";
         this.textBoxTotalFilteringArea.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalFilteringArea.TabIndex = 8;
         this.textBoxTotalFilteringArea.Text = "";
         this.textBoxTotalFilteringArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalFilteringArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
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
         // FabricFilterValuesControl
         // 
         this.Controls.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.textBoxOutletParticleLoading);
         this.Controls.Add(this.textBoxPressureDrop);
         this.Controls.Add(this.textBoxInletParticleLoading);
         this.Controls.Add(this.textBoxParticleCollectionRate);
         this.Controls.Add(this.textBoxCollectionEfficiency);
         this.Controls.Add(this.textBoxGasToClothRatio);
         this.Controls.Add(this.textBoxTotalFilteringArea);
         this.Name = "FabricFilterValuesControl";
         this.Size = new System.Drawing.Size(80, 160);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(FabricFilterControl ctrl)
      {
         this.textBoxPressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.GasPressureDrop);
         this.textBoxCollectionEfficiency.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.CollectionEfficiency);
         this.textBoxInletParticleLoading.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.InletParticleLoading);
         this.textBoxOutletParticleLoading.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.OutletParticleLoading);
         this.textBoxParticleCollectionRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.ParticleCollectionRate);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.MassFlowRateOfParticleLostToGasOutlet);
         this.textBoxGasToClothRatio.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.GasToClothRatio);
         this.textBoxTotalFilteringArea.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.FabricFilter.TotalFilteringArea);
      }

      protected virtual void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxPressureDrop);
         list.Add(this.textBoxCollectionEfficiency);
         list.Add(this.textBoxInletParticleLoading);
         list.Add(this.textBoxOutletParticleLoading);
         list.Add(this.textBoxParticleCollectionRate);
         list.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         list.Add(this.textBoxGasToClothRatio);
         list.Add(this.textBoxTotalFilteringArea);

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
