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
	/// Summary description for FanRectangularValuesControl.
	/// </summary>
	public class FanRectangularValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 60;
      private ProsimoUI.ProcessVarTextBox textBoxOutletWidth;
      private ProsimoUI.ProcessVarTextBox textBoxOutletHeight;
      private ProsimoUI.ProcessVarTextBox textBoxOutletHeightWidthRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanRectangularValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanRectangularValuesControl(FanControl fanCtrl) : this()
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
         this.textBoxOutletWidth = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletHeight = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletHeightWidthRatio = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxOutletWidth
         // 
         this.textBoxOutletWidth.Location = new System.Drawing.Point(0, 0);
         this.textBoxOutletWidth.Name = "textBoxOutletWidth";
         this.textBoxOutletWidth.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletWidth.TabIndex = 1;
         this.textBoxOutletWidth.Text = "";
         this.textBoxOutletWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletHeight
         // 
         this.textBoxOutletHeight.Location = new System.Drawing.Point(0, 20);
         this.textBoxOutletHeight.Name = "textBoxOutletHeight";
         this.textBoxOutletHeight.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletHeight.TabIndex = 2;
         this.textBoxOutletHeight.Text = "";
         this.textBoxOutletHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletHeightWidthRatio
         // 
         this.textBoxOutletHeightWidthRatio.Location = new System.Drawing.Point(0, 40);
         this.textBoxOutletHeightWidthRatio.Name = "textBoxOutletHeightWidthRatio";
         this.textBoxOutletHeightWidthRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletHeightWidthRatio.TabIndex = 3;
         this.textBoxOutletHeightWidthRatio.Text = "";
         this.textBoxOutletHeightWidthRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletHeightWidthRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // FanRectangularValuesControl
         // 
         this.Controls.Add(this.textBoxOutletWidth);
         this.Controls.Add(this.textBoxOutletHeight);
         this.Controls.Add(this.textBoxOutletHeightWidthRatio);
         this.Name = "FanRectangularValuesControl";
         this.Size = new System.Drawing.Size(80, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(FanControl ctrl)
      {
         this.textBoxOutletWidth.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletWidth);
         this.textBoxOutletHeight.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletHeight);
         this.textBoxOutletHeightWidthRatio.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletHeightWidthRatio);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxOutletWidth);
         list.Add(this.textBoxOutletHeight);
         list.Add(this.textBoxOutletHeightWidthRatio);

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
