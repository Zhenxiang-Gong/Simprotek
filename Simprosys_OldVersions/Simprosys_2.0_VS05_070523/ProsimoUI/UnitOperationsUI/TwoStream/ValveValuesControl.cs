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
	/// Summary description for ValveValuesControl.
	/// </summary>
	public class ValveValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 20;

      private ProcessVarTextBox textBoxPressureDrop;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public ValveValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public ValveValuesControl(ValveControl valveCtrl) : this()
		{
         this.InitializeVariableTextBoxes(valveCtrl);
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
         this.SuspendLayout();
         // 
         // textBoxPressureDrop
         // 
         this.textBoxPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.textBoxPressureDrop.Name = "textBoxPressureDrop";
         this.textBoxPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressureDrop.TabIndex = 4;
         this.textBoxPressureDrop.Text = "";
         this.textBoxPressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // ValveValuesControl
         // 
         this.Controls.Add(this.textBoxPressureDrop);
         this.Name = "ValveValuesControl";
         this.Size = new System.Drawing.Size(80, 20);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(ValveControl ctrl)
      {
         this.textBoxPressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Valve.PressureDrop);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxPressureDrop;
         }
      }
   }
}
