using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingCircularValuesControl.
	/// </summary>
	public class DryerScopingCircularValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 60;

      private ProsimoUI.ProcessVarTextBox textBoxLength;
      private ProsimoUI.ProcessVarTextBox textBoxDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxLengthDiameterRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingCircularValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public DryerScopingCircularValuesControl(Flowsheet flowsheet, Dryer dryer)
      {
         // NOTE: this constructor is not used
         InitializeComponent();

         this.InitializeVariableTextBoxes(flowsheet, dryer.ScopingModel);
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
         this.textBoxLengthDiameterRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDiameter = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxLengthDiameterRatio
         // 
         this.textBoxLengthDiameterRatio.Location = new System.Drawing.Point(0, 40);
         this.textBoxLengthDiameterRatio.Name = "textBoxLengthDiameterRatio";
         this.textBoxLengthDiameterRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxLengthDiameterRatio.TabIndex = 3;
         this.textBoxLengthDiameterRatio.Text = "";
         this.textBoxLengthDiameterRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLengthDiameterRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLength
         // 
         this.textBoxLength.Location = new System.Drawing.Point(0, 20);
         this.textBoxLength.Name = "textBoxLength";
         this.textBoxLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxLength.TabIndex = 2;
         this.textBoxLength.Text = "";
         this.textBoxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDiameter
         // 
         this.textBoxDiameter.Location = new System.Drawing.Point(0, 0);
         this.textBoxDiameter.Name = "textBoxDiameter";
         this.textBoxDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxDiameter.TabIndex = 1;
         this.textBoxDiameter.Text = "";
         this.textBoxDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // DryerScopingCircularValuesControl
         // 
         this.Controls.Add(this.textBoxLengthDiameterRatio);
         this.Controls.Add(this.textBoxLength);
         this.Controls.Add(this.textBoxDiameter);
         this.Name = "DryerScopingCircularValuesControl";
         this.Size = new System.Drawing.Size(80, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, DryerScopingModel scoping)
      {
         this.textBoxLength.InitializeVariable(flowsheet.ApplicationPrefs, scoping.Length);
         this.textBoxDiameter.InitializeVariable(flowsheet.ApplicationPrefs, scoping.Diameter);
         this.textBoxLengthDiameterRatio.InitializeVariable(flowsheet.ApplicationPrefs, scoping.LengthDiameterRatio);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxDiameter);
         list.Add(this.textBoxLength);
         list.Add(this.textBoxLengthDiameterRatio);

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
