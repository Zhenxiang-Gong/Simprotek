using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneValuesControl.
	/// </summary>
	public class CycloneValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 100;

      private CycloneControl cycloneCtrl;

      private ProcessVarTextBox textBoxPressureDrop;
      private ProcessVarTextBox textBoxMassFlowRateOfParticleLostToGasOutlet;
      private ProcessVarTextBox textBoxInletParticleLoading;
      private ProcessVarTextBox textBoxCollectionEfficiency;
      private ProsimoUI.ProcessVarTextBox textBoxOutletParticleLoading;
      
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CycloneValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public CycloneValuesControl(CycloneControl cycloneCtrl) : this()
		{
         this.cycloneCtrl = cycloneCtrl;
         this.InitializeVariableTextBoxes(cycloneCtrl);
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
         this.textBoxMassFlowRateOfParticleLostToGasOutlet = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletParticleLoading = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCollectionEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletParticleLoading = new ProsimoUI.ProcessVarTextBox();
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
         // textBoxMassFlowRateOfParticleLostToGasOutlet
         // 
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Location = new System.Drawing.Point(0, 80);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Name = "textBoxMassFlowRateOfParticleLostToGasOutlet";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Size = new System.Drawing.Size(80, 20);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TabIndex = 5;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Text = "";
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
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
         // CycloneValuesControl
         // 
         this.Controls.Add(this.textBoxOutletParticleLoading);
         this.Controls.Add(this.textBoxPressureDrop);
         this.Controls.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);
         this.Controls.Add(this.textBoxInletParticleLoading);
         this.Controls.Add(this.textBoxCollectionEfficiency);
         this.Name = "CycloneValuesControl";
         this.Size = new System.Drawing.Size(80, 100);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(CycloneControl ctrl)
      {
         this.textBoxPressureDrop.InitializeVariable(ctrl.Flowsheet, ctrl.Cyclone.GasPressureDrop);
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.InitializeVariable(ctrl.Flowsheet, ctrl.Cyclone.MassFlowRateOfParticleLostToGasOutlet);
         this.textBoxInletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.Cyclone.InletParticleLoading);
         this.textBoxOutletParticleLoading.InitializeVariable(ctrl.Flowsheet, ctrl.Cyclone.OutletParticleLoading);
         this.textBoxCollectionEfficiency.InitializeVariable(ctrl.Flowsheet, ctrl.Cyclone.CollectionEfficiency);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxPressureDrop);
         list.Add(this.textBoxCollectionEfficiency);
         list.Add(this.textBoxInletParticleLoading);
         list.Add(this.textBoxOutletParticleLoading);
         list.Add(this.textBoxMassFlowRateOfParticleLostToGasOutlet);

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
