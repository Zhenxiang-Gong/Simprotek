using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanCircularValuesControl.
	/// </summary>
	public class FanCircularValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 20;
      private ProsimoUI.ProcessVarTextBox textBoxOutletDiameter;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanCircularValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanCircularValuesControl(FanControl fanCtrl) : this()
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
         this.textBoxOutletDiameter = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxOutletDiameter
         // 
         this.textBoxOutletDiameter.Location = new System.Drawing.Point(0, 0);
         this.textBoxOutletDiameter.Name = "textBoxOutletDiameter";
         this.textBoxOutletDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletDiameter.TabIndex = 4;
         this.textBoxOutletDiameter.Text = "";
         this.textBoxOutletDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // FanCircularValuesControl
         // 
         this.Controls.Add(this.textBoxOutletDiameter);
         this.Name = "FanCircularValuesControl";
         this.Size = new System.Drawing.Size(80, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(FanControl ctrl)
      {
         this.textBoxOutletDiameter.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletDiameter);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxOutletDiameter;
         }
      }
   }
}
