using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingCommonValuesControl.
	/// </summary>
	public class DryerScopingCommonValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 20;

      private ProsimoUI.ProcessVarTextBox textBoxGasVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingCommonValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
         this.textBoxGasVelocity = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxGasVelocity
         // 
         this.textBoxGasVelocity.Location = new System.Drawing.Point(0, 0);
         this.textBoxGasVelocity.Name = "textBoxGasVelocity";
         this.textBoxGasVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasVelocity.TabIndex = 3;
         this.textBoxGasVelocity.Text = "";
         this.textBoxGasVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // DryerScopingCommonValuesControl
         // 
         this.Controls.Add(this.textBoxGasVelocity);
         this.Name = "DryerScopingCommonValuesControl";
         this.Size = new System.Drawing.Size(80, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, DryerScopingModel scoping)
      {
         this.textBoxGasVelocity.InitializeVariable(flowsheet.ApplicationPrefs, scoping.GasVelocity);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
//            this.ActiveControl = null;
            this.ActiveControl = this.textBoxGasVelocity;
         }
      }
   }
}
