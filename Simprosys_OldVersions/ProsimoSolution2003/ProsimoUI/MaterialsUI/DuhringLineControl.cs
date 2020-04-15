using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI;
using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DuhringLineControl.
	/// </summary>
	public class DuhringLineControl : System.Windows.Forms.UserControl
	{
      private DuhringLinesControl duhringLinesControl;

      private DuhringLine duhringLine;
      public DuhringLine DuhringLine
      {
         get {return duhringLine;}
      }

      private bool isSelected;
      public bool IsSelected
      {
         get {return isSelected;}
         set
         {
            isSelected = value;
            if (isSelected)
            {
               this.labelHook.BackColor = SystemColors.Highlight;
//               this.labelHook.ForeColor = SystemColors.HighlightText;
            }
            else
            {
               this.labelHook.BackColor = SystemColors.Control;
//               this.labelHook.ForeColor = SystemColors.ControlText;
            }
         }
      }

      private System.Windows.Forms.Label labelHook;
      private ProcessVarTextBox textBoxConcentration;
      private ProcessVarTextBox textBoxStartSolventBoilingPoint;
      private ProcessVarTextBox textBoxStartSolutionBoilingPoint;
      private ProcessVarTextBox textBoxEndSolventBoilingPoint;
      private ProcessVarTextBox textBoxEndSolutionBoilingPoint;
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DuhringLineControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public DuhringLineControl(DuhringLinesControl dlCtrl, INumericFormat iNumericFormat, DuhringLine duhringLine, bool readOnly)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.duhringLinesControl = dlCtrl;
         this.duhringLine = duhringLine;
         this.IsSelected = false;
         this.InitializeTheUI(iNumericFormat, this.duhringLine, readOnly);
      }

      public DuhringLineControl(INumericFormat iNumericFormat, DuhringLine duhringLine, bool readOnly)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.duhringLine = duhringLine;
         this.IsSelected = false;
         this.InitializeTheUI(iNumericFormat, this.duhringLine, readOnly);
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
         this.labelHook = new System.Windows.Forms.Label();
         this.textBoxConcentration = new ProsimoUI.ProcessVarTextBox();
         this.textBoxStartSolventBoilingPoint = new ProsimoUI.ProcessVarTextBox();
         this.textBoxStartSolutionBoilingPoint = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEndSolventBoilingPoint = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEndSolutionBoilingPoint = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // labelHook
         // 
         this.labelHook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHook.ForeColor = System.Drawing.Color.DarkGray;
         this.labelHook.Location = new System.Drawing.Point(0, 0);
         this.labelHook.Name = "labelHook";
         this.labelHook.Size = new System.Drawing.Size(60, 20);
         this.labelHook.TabIndex = 0;
         this.labelHook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.labelHook.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelHook_MouseDown);
         // 
         // textBoxConcentration
         // 
         this.textBoxConcentration.Location = new System.Drawing.Point(60, 0);
         this.textBoxConcentration.Name = "textBoxConcentration";
         this.textBoxConcentration.Size = new System.Drawing.Size(90, 20);
         this.textBoxConcentration.TabIndex = 1;
         this.textBoxConcentration.Text = "";
         this.textBoxConcentration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxConcentration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxStartSolventBoilingPoint
         // 
         this.textBoxStartSolventBoilingPoint.Location = new System.Drawing.Point(150, 0);
         this.textBoxStartSolventBoilingPoint.Name = "textBoxStartSolventBoilingPoint";
         this.textBoxStartSolventBoilingPoint.Size = new System.Drawing.Size(90, 20);
         this.textBoxStartSolventBoilingPoint.TabIndex = 2;
         this.textBoxStartSolventBoilingPoint.Text = "";
         this.textBoxStartSolventBoilingPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxStartSolventBoilingPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxStartSolutionBoilingPoint
         // 
         this.textBoxStartSolutionBoilingPoint.Location = new System.Drawing.Point(240, 0);
         this.textBoxStartSolutionBoilingPoint.Name = "textBoxStartSolutionBoilingPoint";
         this.textBoxStartSolutionBoilingPoint.Size = new System.Drawing.Size(90, 20);
         this.textBoxStartSolutionBoilingPoint.TabIndex = 3;
         this.textBoxStartSolutionBoilingPoint.Text = "";
         this.textBoxStartSolutionBoilingPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxStartSolutionBoilingPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxEndSolventBoilingPoint
         // 
         this.textBoxEndSolventBoilingPoint.Location = new System.Drawing.Point(330, 0);
         this.textBoxEndSolventBoilingPoint.Name = "textBoxEndSolventBoilingPoint";
         this.textBoxEndSolventBoilingPoint.Size = new System.Drawing.Size(90, 20);
         this.textBoxEndSolventBoilingPoint.TabIndex = 4;
         this.textBoxEndSolventBoilingPoint.Text = "";
         this.textBoxEndSolventBoilingPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEndSolventBoilingPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxEndSolutionBoilingPoint
         // 
         this.textBoxEndSolutionBoilingPoint.Location = new System.Drawing.Point(420, 0);
         this.textBoxEndSolutionBoilingPoint.Name = "textBoxEndSolutionBoilingPoint";
         this.textBoxEndSolutionBoilingPoint.Size = new System.Drawing.Size(90, 20);
         this.textBoxEndSolutionBoilingPoint.TabIndex = 5;
         this.textBoxEndSolutionBoilingPoint.Text = "";
         this.textBoxEndSolutionBoilingPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEndSolutionBoilingPoint.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // DuhringLineControl
         // 
         this.Controls.Add(this.textBoxEndSolventBoilingPoint);
         this.Controls.Add(this.textBoxStartSolutionBoilingPoint);
         this.Controls.Add(this.textBoxStartSolventBoilingPoint);
         this.Controls.Add(this.textBoxConcentration);
         this.Controls.Add(this.textBoxEndSolutionBoilingPoint);
         this.Controls.Add(this.labelHook);
         this.Name = "DuhringLineControl";
         this.Size = new System.Drawing.Size(510, 20);
         this.ResumeLayout(false);

      }
		#endregion

      private void InitializeTheUI(INumericFormat iNumericFormat, DuhringLine duhringLine, bool readOnly)
      {
         this.textBoxConcentration.InitializeVariable(iNumericFormat, duhringLine.Concentration);
         this.textBoxStartSolventBoilingPoint.InitializeVariable(iNumericFormat, duhringLine.StartSolventBoilingPoint);
         this.textBoxStartSolutionBoilingPoint.InitializeVariable(iNumericFormat, duhringLine.StartSolutionBoilingPoint);
         this.textBoxEndSolventBoilingPoint.InitializeVariable(iNumericFormat, duhringLine.EndSolventBoilingPoint);
         this.textBoxEndSolutionBoilingPoint.InitializeVariable(iNumericFormat, duhringLine.EndSolutionBoilingPoint);
         if (readOnly)
         {
            this.Controls.Remove(this.labelHook);
            this.Size = new Size(this.Width-60, 20);

            this.textBoxConcentration.Location = new Point(this.textBoxConcentration.Location.X-60, 0);
            this.textBoxStartSolventBoilingPoint.Location = new Point(this.textBoxStartSolventBoilingPoint.Location.X-60, 0);
            this.textBoxStartSolutionBoilingPoint.Location = new Point(this.textBoxStartSolutionBoilingPoint.Location.X-60, 0);
            this.textBoxEndSolventBoilingPoint.Location = new Point(this.textBoxEndSolventBoilingPoint.Location.X-60, 0);
            this.textBoxEndSolutionBoilingPoint.Location = new Point(this.textBoxEndSolutionBoilingPoint.Location.X-60, 0);

            this.textBoxConcentration.ReadOnly = true;
            this.textBoxStartSolventBoilingPoint.ReadOnly = true;
            this.textBoxStartSolutionBoilingPoint.ReadOnly = true;
            this.textBoxEndSolventBoilingPoint.ReadOnly = true;
            this.textBoxEndSolutionBoilingPoint.ReadOnly = true;
         }
      }

      private void labelHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void ProcessClick(MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) 
            {
               this.duhringLinesControl.SetElementsSelection(this, ModifierKey.Shift);
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
               this.duhringLinesControl.SetElementsSelection(this, ModifierKey.Ctrl);
            }
            else
            {
               this.duhringLinesControl.SetElementsSelection(this, ModifierKey.None);
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.duhringLinesControl.UnselectElements();
         }
      }

      private void KeyUpEventHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxConcentration);
         list.Add(this.textBoxStartSolventBoilingPoint);
         list.Add(this.textBoxStartSolutionBoilingPoint);
         list.Add(this.textBoxEndSolventBoilingPoint);
         list.Add(this.textBoxEndSolutionBoilingPoint);

         if (e.KeyCode == Keys.Enter)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
      }
   }
}
