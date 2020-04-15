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
	/// Summary description for HeaterValuesControl.
	/// </summary>
	public class HeaterValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 60;

      private ProcessVarTextBox textBoxPressureDrop;
      private ProcessVarTextBox textBoxHeatLoss;
      private ProcessVarTextBox textBoxHeatInput;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HeaterValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public HeaterValuesControl(HeaterControl heaterCtrl) : this()
		{
         this.InitializeVariableTextBoxes(heaterCtrl);
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
         this.textBoxPressureDrop = new ProcessVarTextBox();
         this.textBoxHeatLoss = new ProcessVarTextBox();
         this.textBoxHeatInput = new ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxPressureDrop
         // 
         this.textBoxPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.textBoxPressureDrop.Name = "textBoxPressureDrop";
         this.textBoxPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressureDrop.TabIndex = 4;
         this.textBoxPressureDrop.Text = "";
         this.textBoxPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeatLoss
         // 
         this.textBoxHeatLoss.Location = new System.Drawing.Point(0, 20);
         this.textBoxHeatLoss.Name = "textBoxHeatLoss";
         this.textBoxHeatLoss.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeatLoss.TabIndex = 5;
         this.textBoxHeatLoss.Text = "";
         this.textBoxHeatLoss.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeatInput
         // 
         this.textBoxHeatInput.Location = new System.Drawing.Point(0, 40);
         this.textBoxHeatInput.Name = "textBoxHeatInput";
         this.textBoxHeatInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeatInput.TabIndex = 6;
         this.textBoxHeatInput.Text = "";
         this.textBoxHeatInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HeaterValuesControl
         // 
         this.Controls.Add(this.textBoxPressureDrop);
         this.Controls.Add(this.textBoxHeatLoss);
         this.Controls.Add(this.textBoxHeatInput);
         this.Name = "HeaterValuesControl";
         this.Size = new System.Drawing.Size(80, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(HeaterControl ctrl)
      {
         this.textBoxPressureDrop.InitializeVariable(ctrl.Flowsheet, ctrl.Heater.PressureDrop);
         this.textBoxHeatLoss.InitializeVariable(ctrl.Flowsheet, ctrl.Heater.HeatLoss);
         this.textBoxHeatInput.InitializeVariable(ctrl.Flowsheet, ctrl.Heater.HeatInput);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxPressureDrop);
         list.Add(this.textBoxHeatLoss);
         list.Add(this.textBoxHeatInput);

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
