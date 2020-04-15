using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanValuesControl.
	/// </summary>
	public class FanValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 80;

      private ProcessVarTextBox textBoxStaticPressure;
      private ProcessVarTextBox textBoxEfficiency;
      private ProcessVarTextBox textBoxPowerInput;
      private ProcessVarTextBox textBoxTotalDischargePressure;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanValuesControl(FanControl fanCtrl) : this()
		{
         this.InitializeVariableTextBoxes(fanCtrl);
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
         this.textBoxStaticPressure = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPowerInput = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalDischargePressure = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxStaticPressure
         // 
         this.textBoxStaticPressure.Location = new System.Drawing.Point(0, 0);
         this.textBoxStaticPressure.Name = "textBoxStaticPressure";
         this.textBoxStaticPressure.Size = new System.Drawing.Size(80, 20);
         this.textBoxStaticPressure.TabIndex = 1;
         this.textBoxStaticPressure.Text = "";
         this.textBoxStaticPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxStaticPressure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEfficiency
         // 
         this.textBoxEfficiency.Location = new System.Drawing.Point(0, 40);
         this.textBoxEfficiency.Name = "textBoxEfficiency";
         this.textBoxEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxEfficiency.TabIndex = 4;
         this.textBoxEfficiency.Text = "";
         this.textBoxEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPowerInput
         // 
         this.textBoxPowerInput.Location = new System.Drawing.Point(0, 60);
         this.textBoxPowerInput.Name = "textBoxPowerInput";
         this.textBoxPowerInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxPowerInput.TabIndex = 5;
         this.textBoxPowerInput.Text = "";
         this.textBoxPowerInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPowerInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalDischargePressure
         // 
         this.textBoxTotalDischargePressure.Location = new System.Drawing.Point(0, 20);
         this.textBoxTotalDischargePressure.Name = "textBoxTotalDischargePressure";
         this.textBoxTotalDischargePressure.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalDischargePressure.TabIndex = 3;
         this.textBoxTotalDischargePressure.Text = "";
         this.textBoxTotalDischargePressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalDischargePressure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // FanValuesControl
         // 
         this.Controls.Add(this.textBoxPowerInput);
         this.Controls.Add(this.textBoxTotalDischargePressure);
         this.Controls.Add(this.textBoxStaticPressure);
         this.Controls.Add(this.textBoxEfficiency);
         this.Name = "FanValuesControl";
         this.Size = new System.Drawing.Size(80, 80);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(FanControl ctrl)
      {
         this.textBoxStaticPressure.InitializeVariable(ctrl.Flowsheet, ctrl.Fan.StaticPressure);
         this.textBoxEfficiency.InitializeVariable(ctrl.Flowsheet, ctrl.Fan.Efficiency);
         this.textBoxTotalDischargePressure.InitializeVariable(ctrl.Flowsheet, ctrl.Fan.TotalDischargePressure);
         this.textBoxPowerInput.InitializeVariable(ctrl.Flowsheet, ctrl.Fan.PowerInput);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxStaticPressure);
         list.Add(this.textBoxTotalDischargePressure);
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
