using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingRectangularValuesControl.
	/// </summary>
	public class DryerScopingRectangularValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 100;

      private ProsimoUI.ProcessVarTextBox textBoxWidth;
      private ProsimoUI.ProcessVarTextBox textBoxLength;
      private ProsimoUI.ProcessVarTextBox textBoxHeight;
      private ProsimoUI.ProcessVarTextBox textBoxLengthWidthRatio;
      private ProsimoUI.ProcessVarTextBox textBoxHeightWidthRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingRectangularValuesControl()
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
         this.textBoxHeightWidthRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLengthWidthRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHeight = new ProsimoUI.ProcessVarTextBox();
         this.textBoxLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxWidth = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxHeightWidthRatio
         // 
         this.textBoxHeightWidthRatio.Location = new System.Drawing.Point(0, 80);
         this.textBoxHeightWidthRatio.Name = "textBoxHeightWidthRatio";
         this.textBoxHeightWidthRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeightWidthRatio.TabIndex = 8;
         this.textBoxHeightWidthRatio.Text = "";
         this.textBoxHeightWidthRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHeightWidthRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLengthWidthRatio
         // 
         this.textBoxLengthWidthRatio.Location = new System.Drawing.Point(0, 60);
         this.textBoxLengthWidthRatio.Name = "textBoxLengthWidthRatio";
         this.textBoxLengthWidthRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxLengthWidthRatio.TabIndex = 7;
         this.textBoxLengthWidthRatio.Text = "";
         this.textBoxLengthWidthRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLengthWidthRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeight
         // 
         this.textBoxHeight.Location = new System.Drawing.Point(0, 40);
         this.textBoxHeight.Name = "textBoxHeight";
         this.textBoxHeight.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeight.TabIndex = 5;
         this.textBoxHeight.Text = "";
         this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxLength
         // 
         this.textBoxLength.Location = new System.Drawing.Point(0, 20);
         this.textBoxLength.Name = "textBoxLength";
         this.textBoxLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxLength.TabIndex = 4;
         this.textBoxLength.Text = "";
         this.textBoxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxWidth
         // 
         this.textBoxWidth.Location = new System.Drawing.Point(0, 0);
         this.textBoxWidth.Name = "textBoxWidth";
         this.textBoxWidth.Size = new System.Drawing.Size(80, 20);
         this.textBoxWidth.TabIndex = 3;
         this.textBoxWidth.Text = "";
         this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // DryerScopingRectangularValuesControl
         // 
         this.Controls.Add(this.textBoxHeightWidthRatio);
         this.Controls.Add(this.textBoxLengthWidthRatio);
         this.Controls.Add(this.textBoxHeight);
         this.Controls.Add(this.textBoxLength);
         this.Controls.Add(this.textBoxWidth);
         this.Name = "DryerScopingRectangularValuesControl";
         this.Size = new System.Drawing.Size(80, 100);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, DryerScopingModel scoping)
      {
         this.textBoxWidth.InitializeVariable(flowsheet.ApplicationPrefs, scoping.Width);
         this.textBoxLength.InitializeVariable(flowsheet.ApplicationPrefs, scoping.Length);
         this.textBoxHeight.InitializeVariable(flowsheet.ApplicationPrefs, scoping.Height);
         this.textBoxLengthWidthRatio.InitializeVariable(flowsheet.ApplicationPrefs, scoping.LengthWidthRatio);
         this.textBoxHeightWidthRatio.InitializeVariable(flowsheet.ApplicationPrefs, scoping.HeightWidthRatio);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxWidth);
         list.Add(this.textBoxLength);
         list.Add(this.textBoxHeight);
         list.Add(this.textBoxLengthWidthRatio);
         list.Add(this.textBoxHeightWidthRatio);

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
