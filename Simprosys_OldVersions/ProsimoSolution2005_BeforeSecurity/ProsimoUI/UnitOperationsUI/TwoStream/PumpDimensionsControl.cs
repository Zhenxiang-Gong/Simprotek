using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for PumpDimensionsControl.
	/// </summary>
	public class PumpDimensionsControl : System.Windows.Forms.UserControl
	{
      private Pump pump;

      private ProsimoUI.ProcessVarLabel labelOutletDiameter;
      private ProsimoUI.ProcessVarLabel labelOutletVelocity;
      private ProsimoUI.ProcessVarTextBox textBoxOutletDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxOutletVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PumpDimensionsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public PumpDimensionsControl(PumpControl pumpCtrl)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.pump = pumpCtrl.Pump;
         this.InitializeVariableLabels(pumpCtrl.Pump);
         this.InitializeVariableTextBoxes(pumpCtrl);
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
         this.labelOutletDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelOutletVelocity = new ProsimoUI.ProcessVarLabel();
         this.textBoxOutletDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletVelocity = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // labelOutletDiameter
         // 
         this.labelOutletDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletDiameter.Location = new System.Drawing.Point(0, 0);
         this.labelOutletDiameter.Name = "labelOutletDiameter";
         this.labelOutletDiameter.Size = new System.Drawing.Size(192, 20);
         this.labelOutletDiameter.TabIndex = 94;
         this.labelOutletDiameter.Text = "OutletDiameter";
         this.labelOutletDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletVelocity
         // 
         this.labelOutletVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletVelocity.Location = new System.Drawing.Point(0, 20);
         this.labelOutletVelocity.Name = "labelOutletVelocity";
         this.labelOutletVelocity.Size = new System.Drawing.Size(192, 20);
         this.labelOutletVelocity.TabIndex = 93;
         this.labelOutletVelocity.Text = "OutletVelocity";
         this.labelOutletVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxOutletDiameter
         // 
         this.textBoxOutletDiameter.Location = new System.Drawing.Point(192, 0);
         this.textBoxOutletDiameter.Name = "textBoxOutletDiameter";
         this.textBoxOutletDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletDiameter.TabIndex = 95;
         this.textBoxOutletDiameter.Text = "";
         this.textBoxOutletDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletVelocity
         // 
         this.textBoxOutletVelocity.Location = new System.Drawing.Point(192, 20);
         this.textBoxOutletVelocity.Name = "textBoxOutletVelocity";
         this.textBoxOutletVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletVelocity.TabIndex = 96;
         this.textBoxOutletVelocity.Text = "";
         this.textBoxOutletVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // PumpDimensionsControl
         // 
         this.Controls.Add(this.textBoxOutletDiameter);
         this.Controls.Add(this.textBoxOutletVelocity);
         this.Controls.Add(this.labelOutletDiameter);
         this.Controls.Add(this.labelOutletVelocity);
         this.Name = "PumpDimensionsControl";
         this.Size = new System.Drawing.Size(272, 40);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Pump uo)
      {
         this.labelOutletDiameter.InitializeVariable(uo.OutletDiameter);
         this.labelOutletVelocity.InitializeVariable(uo.OutletVelocity);
      }

      public void InitializeVariableTextBoxes(PumpControl ctrl)
      {
         this.textBoxOutletDiameter.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Pump.OutletDiameter);
         this.textBoxOutletVelocity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Pump.OutletVelocity);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxOutletDiameter);
         list.Add(this.textBoxOutletVelocity);

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
