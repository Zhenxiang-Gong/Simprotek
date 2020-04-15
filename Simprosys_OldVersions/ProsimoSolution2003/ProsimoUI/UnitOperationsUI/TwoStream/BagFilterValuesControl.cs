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
	/// Summary description for BagFilterValuesControl.
	/// </summary>
	public class BagFilterValuesControl : FabricFilterValuesControl
	{
      // these need to be in sync with the dimensions of this control
      public new const int WIDTH = 80;
      public new const int HEIGHT = 220;

      private ProcessVarTextBox textBoxBagDiameter;
      private ProcessVarTextBox textBoxBagLength;
      private ProcessVarTextBox textBoxNumberOfBags;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public BagFilterValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public BagFilterValuesControl(BagFilterControl bagFilterCtrl) : base(bagFilterCtrl)
		{
         InitializeComponent();
         this.InitializeVariableTextBoxes(bagFilterCtrl);
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
         this.textBoxBagDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBagLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxNumberOfBags = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxPressureDrop
         // 
         this.textBoxPressureDrop.Name = "textBoxPressureDrop";
         // 
         // textBoxCollectionEfficiency
         // 
         this.textBoxCollectionEfficiency.Name = "textBoxCollectionEfficiency";
         // 
         // textBoxGasToClothRatio
         // 
         this.textBoxGasToClothRatio.Name = "textBoxGasToClothRatio";
         // 
         // textBoxTotalFilteringArea
         // 
         this.textBoxTotalFilteringArea.Name = "textBoxTotalFilteringArea";
         // 
         // textBoxInletParticleLoading
         // 
         this.textBoxInletParticleLoading.Name = "textBoxInletParticleLoading";
         // 
         // textBoxOutletParticleLoading
         // 
         this.textBoxOutletParticleLoading.Name = "textBoxOutletParticleLoading";
         // 
         // textBoxParticleCollectionRate
         // 
         this.textBoxParticleCollectionRate.Name = "textBoxParticleCollectionRate";
         // 
         // textBoxMassFlowRateOfParticleLostToGasOutlet
         // 
         this.textBoxMassFlowRateOfParticleLostToGasOutlet.Name = "textBoxMassFlowRateOfParticleLostToGasOutlet";
         // 
         // textBoxBagDiameter
         // 
         this.textBoxBagDiameter.Location = new System.Drawing.Point(0, 160);
         this.textBoxBagDiameter.Name = "textBoxBagDiameter";
         this.textBoxBagDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxBagDiameter.TabIndex = 10;
         this.textBoxBagDiameter.Text = "";
         this.textBoxBagDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBagDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBagLength
         // 
         this.textBoxBagLength.Location = new System.Drawing.Point(0, 180);
         this.textBoxBagLength.Name = "textBoxBagLength";
         this.textBoxBagLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxBagLength.TabIndex = 11;
         this.textBoxBagLength.Text = "";
         this.textBoxBagLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBagLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxNumberOfBags
         // 
         this.textBoxNumberOfBags.Location = new System.Drawing.Point(0, 200);
         this.textBoxNumberOfBags.Name = "textBoxNumberOfBags";
         this.textBoxNumberOfBags.Size = new System.Drawing.Size(80, 20);
         this.textBoxNumberOfBags.TabIndex = 12;
         this.textBoxNumberOfBags.Text = "";
         this.textBoxNumberOfBags.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxNumberOfBags.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // BagFilterValuesControl
         // 
         this.Controls.Add(this.textBoxBagDiameter);
         this.Controls.Add(this.textBoxBagLength);
         this.Controls.Add(this.textBoxNumberOfBags);
         this.Name = "BagFilterValuesControl";
         this.Size = new System.Drawing.Size(80, 220);
         this.Controls.SetChildIndex(this.textBoxNumberOfBags, 0);
         this.Controls.SetChildIndex(this.textBoxBagLength, 0);
         this.Controls.SetChildIndex(this.textBoxBagDiameter, 0);
         this.Controls.SetChildIndex(this.textBoxTotalFilteringArea, 0);
         this.Controls.SetChildIndex(this.textBoxGasToClothRatio, 0);
         this.Controls.SetChildIndex(this.textBoxCollectionEfficiency, 0);
         this.Controls.SetChildIndex(this.textBoxParticleCollectionRate, 0);
         this.Controls.SetChildIndex(this.textBoxInletParticleLoading, 0);
         this.Controls.SetChildIndex(this.textBoxPressureDrop, 0);
         this.Controls.SetChildIndex(this.textBoxOutletParticleLoading, 0);
         this.Controls.SetChildIndex(this.textBoxMassFlowRateOfParticleLostToGasOutlet, 0);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(BagFilterControl ctrl)
      {
         this.textBoxBagDiameter.InitializeVariable(ctrl.Flowsheet, ctrl.BagFilter.BagDiameter);
         this.textBoxBagLength.InitializeVariable(ctrl.Flowsheet, ctrl.BagFilter.BagLength);
         this.textBoxNumberOfBags.InitializeVariable(ctrl.Flowsheet, ctrl.BagFilter.NumberOfBags);
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
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
         list.Add(this.textBoxBagDiameter);
         list.Add(this.textBoxBagLength);
         list.Add(this.textBoxNumberOfBags);

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
