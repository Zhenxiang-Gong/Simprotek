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
	/// Summary description for PumpValuesControl.
	/// </summary>
	public class PumpValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 160;

      private ProcessVarTextBox textBoxCapacity;
      private ProcessVarTextBox textBoxDischargeFrictionHead;
      private ProcessVarTextBox textBoxEfficiency;
      private ProcessVarTextBox textBoxPowerInput;
      private ProcessVarTextBox textBoxStaticDischargeHead;
      private ProcessVarTextBox textBoxStaticSuctionHead;
      private ProcessVarTextBox textBoxSuctionFrictionHead;
      private ProcessVarTextBox textBoxTotalDynamicHead;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public PumpValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public PumpValuesControl(PumpControl pumpCtrl) : this()
		{
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
         this.textBoxCapacity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDischargeFrictionHead = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPowerInput = new ProsimoUI.ProcessVarTextBox();
         this.textBoxStaticDischargeHead = new ProsimoUI.ProcessVarTextBox();
         this.textBoxStaticSuctionHead = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSuctionFrictionHead = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalDynamicHead = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxCapacity
         // 
         this.textBoxCapacity.Location = new System.Drawing.Point(0, 0);
         this.textBoxCapacity.Name = "textBoxCapacity";
         this.textBoxCapacity.Size = new System.Drawing.Size(80, 20);
         this.textBoxCapacity.TabIndex = 1;
         this.textBoxCapacity.Text = "";
         this.textBoxCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCapacity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDischargeFrictionHead
         // 
         this.textBoxDischargeFrictionHead.Location = new System.Drawing.Point(0, 80);
         this.textBoxDischargeFrictionHead.Name = "textBoxDischargeFrictionHead";
         this.textBoxDischargeFrictionHead.Size = new System.Drawing.Size(80, 20);
         this.textBoxDischargeFrictionHead.TabIndex = 5;
         this.textBoxDischargeFrictionHead.Text = "";
         this.textBoxDischargeFrictionHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxDischargeFrictionHead.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEfficiency
         // 
         this.textBoxEfficiency.Location = new System.Drawing.Point(0, 120);
         this.textBoxEfficiency.Name = "textBoxEfficiency";
         this.textBoxEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxEfficiency.TabIndex = 7;
         this.textBoxEfficiency.Text = "";
         this.textBoxEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPowerInput
         // 
         this.textBoxPowerInput.Location = new System.Drawing.Point(0, 140);
         this.textBoxPowerInput.Name = "textBoxPowerInput";
         this.textBoxPowerInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxPowerInput.TabIndex = 8;
         this.textBoxPowerInput.Text = "";
         this.textBoxPowerInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPowerInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxStaticDischargeHead
         // 
         this.textBoxStaticDischargeHead.Location = new System.Drawing.Point(0, 60);
         this.textBoxStaticDischargeHead.Name = "textBoxStaticDischargeHead";
         this.textBoxStaticDischargeHead.Size = new System.Drawing.Size(80, 20);
         this.textBoxStaticDischargeHead.TabIndex = 4;
         this.textBoxStaticDischargeHead.Text = "";
         this.textBoxStaticDischargeHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxStaticDischargeHead.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxStaticSuctionHead
         // 
         this.textBoxStaticSuctionHead.Location = new System.Drawing.Point(0, 20);
         this.textBoxStaticSuctionHead.Name = "textBoxStaticSuctionHead";
         this.textBoxStaticSuctionHead.Size = new System.Drawing.Size(80, 20);
         this.textBoxStaticSuctionHead.TabIndex = 2;
         this.textBoxStaticSuctionHead.Text = "";
         this.textBoxStaticSuctionHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxStaticSuctionHead.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSuctionFrictionHead
         // 
         this.textBoxSuctionFrictionHead.Location = new System.Drawing.Point(0, 40);
         this.textBoxSuctionFrictionHead.Name = "textBoxSuctionFrictionHead";
         this.textBoxSuctionFrictionHead.Size = new System.Drawing.Size(80, 20);
         this.textBoxSuctionFrictionHead.TabIndex = 3;
         this.textBoxSuctionFrictionHead.Text = "";
         this.textBoxSuctionFrictionHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxSuctionFrictionHead.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalDynamicHead
         // 
         this.textBoxTotalDynamicHead.Location = new System.Drawing.Point(0, 100);
         this.textBoxTotalDynamicHead.Name = "textBoxTotalDynamicHead";
         this.textBoxTotalDynamicHead.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalDynamicHead.TabIndex = 6;
         this.textBoxTotalDynamicHead.Text = "";
         this.textBoxTotalDynamicHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalDynamicHead.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // PumpValuesControl
         // 
         this.Controls.Add(this.textBoxCapacity);
         this.Controls.Add(this.textBoxDischargeFrictionHead);
         this.Controls.Add(this.textBoxEfficiency);
         this.Controls.Add(this.textBoxPowerInput);
         this.Controls.Add(this.textBoxStaticDischargeHead);
         this.Controls.Add(this.textBoxStaticSuctionHead);
         this.Controls.Add(this.textBoxSuctionFrictionHead);
         this.Controls.Add(this.textBoxTotalDynamicHead);
         this.Name = "PumpValuesControl";
         this.Size = new System.Drawing.Size(80, 160);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(PumpControl ctrl)
      {
         this.textBoxCapacity.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.Capacity);
         this.textBoxDischargeFrictionHead.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.DischargeFrictionHead);
         this.textBoxEfficiency.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.Efficiency);
         this.textBoxPowerInput.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.PowerInput);
         this.textBoxStaticDischargeHead.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.StaticDischargeHead);
         this.textBoxStaticSuctionHead.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.StaticSuctionHead);
         this.textBoxSuctionFrictionHead.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.SuctionFrictionHead);
         this.textBoxTotalDynamicHead.InitializeVariable(ctrl.Flowsheet, ctrl.Pump.TotalDynamicHead);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxCapacity);
         list.Add(this.textBoxStaticSuctionHead);
         list.Add(this.textBoxSuctionFrictionHead);
         list.Add(this.textBoxStaticDischargeHead);
         list.Add(this.textBoxDischargeFrictionHead);
         list.Add(this.textBoxTotalDynamicHead);
         list.Add(this.textBoxEfficiency);
         list.Add(this.textBoxPowerInput);

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
