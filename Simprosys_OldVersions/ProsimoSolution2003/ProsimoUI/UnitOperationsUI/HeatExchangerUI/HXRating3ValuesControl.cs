using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRating3ValuesControl.
	/// </summary>
	public class HXRating3ValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 80;
      private ProsimoUI.ProcessVarTextBox textBoxTubeSideEntranceNozzleDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxTubeSideExitNozzleDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxShellSideEntranceNozzleDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxShellSideExitNozzleDiameter;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRating3ValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
         this.textBoxShellSideEntranceNozzleDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeSideEntranceNozzleDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeSideExitNozzleDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxShellSideExitNozzleDiameter = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxShellSideEntranceNozzleDiameter
         // 
         this.textBoxShellSideEntranceNozzleDiameter.Location = new System.Drawing.Point(0, 40);
         this.textBoxShellSideEntranceNozzleDiameter.Name = "textBoxShellSideEntranceNozzleDiameter";
         this.textBoxShellSideEntranceNozzleDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxShellSideEntranceNozzleDiameter.TabIndex = 5;
         this.textBoxShellSideEntranceNozzleDiameter.Text = "";
         this.textBoxShellSideEntranceNozzleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxShellSideEntranceNozzleDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeSideEntranceNozzleDiameter
         // 
         this.textBoxTubeSideEntranceNozzleDiameter.Location = new System.Drawing.Point(0, 0);
         this.textBoxTubeSideEntranceNozzleDiameter.Name = "textBoxTubeSideEntranceNozzleDiameter";
         this.textBoxTubeSideEntranceNozzleDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeSideEntranceNozzleDiameter.TabIndex = 4;
         this.textBoxTubeSideEntranceNozzleDiameter.Text = "";
         this.textBoxTubeSideEntranceNozzleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeSideEntranceNozzleDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeSideExitNozzleDiameter
         // 
         this.textBoxTubeSideExitNozzleDiameter.Location = new System.Drawing.Point(0, 20);
         this.textBoxTubeSideExitNozzleDiameter.Name = "textBoxTubeSideExitNozzleDiameter";
         this.textBoxTubeSideExitNozzleDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeSideExitNozzleDiameter.TabIndex = 6;
         this.textBoxTubeSideExitNozzleDiameter.Text = "";
         this.textBoxTubeSideExitNozzleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeSideExitNozzleDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxShellSideExitNozzleDiameter
         // 
         this.textBoxShellSideExitNozzleDiameter.Location = new System.Drawing.Point(0, 60);
         this.textBoxShellSideExitNozzleDiameter.Name = "textBoxShellSideExitNozzleDiameter";
         this.textBoxShellSideExitNozzleDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxShellSideExitNozzleDiameter.TabIndex = 7;
         this.textBoxShellSideExitNozzleDiameter.Text = "";
         this.textBoxShellSideExitNozzleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxShellSideExitNozzleDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HXRating3ValuesControl
         // 
         this.Controls.Add(this.textBoxShellSideExitNozzleDiameter);
         this.Controls.Add(this.textBoxTubeSideExitNozzleDiameter);
         this.Controls.Add(this.textBoxShellSideEntranceNozzleDiameter);
         this.Controls.Add(this.textBoxTubeSideEntranceNozzleDiameter);
         this.Name = "HXRating3ValuesControl";
         this.Size = new System.Drawing.Size(80, 80);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, HXRatingModelShellAndTube rating)
      {
         this.textBoxTubeSideEntranceNozzleDiameter.InitializeVariable(flowsheet, rating.TubeSideEntranceNozzleDiameter);
         this.textBoxTubeSideExitNozzleDiameter.InitializeVariable(flowsheet, rating.TubeSideExitNozzleDiameter);
         this.textBoxShellSideEntranceNozzleDiameter.InitializeVariable(flowsheet, rating.ShellSideEntranceNozzleDiameter);
         this.textBoxShellSideExitNozzleDiameter.InitializeVariable(flowsheet, rating.ShellSideExitNozzleDiameter);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxTubeSideEntranceNozzleDiameter);
         list.Add(this.textBoxTubeSideExitNozzleDiameter);
         list.Add(this.textBoxShellSideEntranceNozzleDiameter);
         list.Add(this.textBoxShellSideExitNozzleDiameter);

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
