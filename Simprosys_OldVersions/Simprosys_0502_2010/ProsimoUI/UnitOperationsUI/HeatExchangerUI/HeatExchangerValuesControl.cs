using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HeatExchangerValuesControl.
	/// </summary>
	public class HeatExchangerValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 60;

      private ProcessVarTextBox textBoxTotalHeatTransfer;
      private ProcessVarTextBox textBoxColdSidePressureDrop;
      private ProcessVarTextBox textBoxHotSidePressureDrop;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HeatExchangerValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public HeatExchangerValuesControl(HeatExchangerControl heatExchangerCtrl) : this()
		{
         this.InitializeVariableTextBoxes(heatExchangerCtrl);
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
         this.textBoxTotalHeatTransfer = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSidePressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSidePressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxTotalHeatTransfer
         // 
         this.textBoxTotalHeatTransfer.Location = new System.Drawing.Point(0, 0);
         this.textBoxTotalHeatTransfer.Name = "textBoxTotalHeatTransfer";
         this.textBoxTotalHeatTransfer.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalHeatTransfer.TabIndex = 9;
         this.textBoxTotalHeatTransfer.Text = "";
         this.textBoxTotalHeatTransfer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalHeatTransfer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSidePressureDrop
         // 
         this.textBoxColdSidePressureDrop.Location = new System.Drawing.Point(0, 20);
         this.textBoxColdSidePressureDrop.Name = "textBoxColdSidePressureDrop";
         this.textBoxColdSidePressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSidePressureDrop.TabIndex = 10;
         this.textBoxColdSidePressureDrop.Text = "";
         this.textBoxColdSidePressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSidePressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSidePressureDrop
         // 
         this.textBoxHotSidePressureDrop.Location = new System.Drawing.Point(0, 40);
         this.textBoxHotSidePressureDrop.Name = "textBoxHotSidePressureDrop";
         this.textBoxHotSidePressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSidePressureDrop.TabIndex = 11;
         this.textBoxHotSidePressureDrop.Text = "";
         this.textBoxHotSidePressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSidePressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HeatExchangerValuesControl
         // 
         this.Controls.Add(this.textBoxTotalHeatTransfer);
         this.Controls.Add(this.textBoxColdSidePressureDrop);
         this.Controls.Add(this.textBoxHotSidePressureDrop);
         this.Name = "HeatExchangerValuesControl";
         this.Size = new System.Drawing.Size(80, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(HeatExchangerControl ctrl)
      {
         this.textBoxTotalHeatTransfer.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.HeatExchanger.TotalHeatTransfer);
         this.textBoxColdSidePressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.HeatExchanger.ColdSidePressureDrop);
         this.textBoxHotSidePressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.HeatExchanger.HotSidePressureDrop);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxTotalHeatTransfer);
         list.Add(this.textBoxColdSidePressureDrop);
         list.Add(this.textBoxHotSidePressureDrop);

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
