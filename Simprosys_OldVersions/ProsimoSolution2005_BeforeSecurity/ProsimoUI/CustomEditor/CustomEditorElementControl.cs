using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for CustomEditorElementControl.
	/// </summary>
	public class CustomEditorElementControl : System.Windows.Forms.UserControl
	{
      private CustomEditorForm editorForm;

      private ProcessVar var;
      public ProcessVar Variable
      {
         get {return var;}
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
               this.labelVariable.BackColor = SystemColors.Highlight;
               this.labelVariable.ForeColor = SystemColors.HighlightText;
            }
            else
            {
               this.labelVariable.BackColor = SystemColors.Control;
               this.labelVariable.ForeColor = SystemColors.ControlText;
            }
         }
      }

      private SolvableLabel labelSolvable;
      private ProcessVarLabel labelVariable;
      private ProcessVarTextBox textBoxValue;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CustomEditorElementControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public CustomEditorElementControl(CustomEditorForm editorForm, Flowsheet flowsheet, ProcessVar var)
		{
			InitializeComponent();

         this.editorForm = editorForm;
         this.var = var;
         this.labelSolvable.InitializeSolvable(var.Owner as Solvable);
         this.labelVariable.InitializeVariable(var);
         this.textBoxValue.InitializeVariable(flowsheet.ApplicationPrefs, var);
         this.IsSelected = false;
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
         this.labelSolvable = new ProsimoUI.SolvableLabel();
         this.textBoxValue = new ProsimoUI.ProcessVarTextBox();
         this.labelVariable = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelSolvable
         // 
         this.labelSolvable.BackColor = System.Drawing.SystemColors.Control;
         this.labelSolvable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSolvable.Location = new System.Drawing.Point(0, 0);
         this.labelSolvable.Name = "labelSolvable";
         this.labelSolvable.Size = new System.Drawing.Size(84, 20);
         this.labelSolvable.TabIndex = 3;
         this.labelSolvable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.labelSolvable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelSolvable_MouseDown);
         // 
         // textBoxValue
         // 
         this.textBoxValue.Location = new System.Drawing.Point(276, 0);
         this.textBoxValue.Name = "textBoxValue";
         this.textBoxValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxValue.TabIndex = 5;
         this.textBoxValue.Text = "";
         this.textBoxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxValue.KeyUp += new KeyEventHandler(textBoxValue_KeyUp);
         // 
         // labelVariable
         // 
         this.labelVariable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelVariable.Location = new System.Drawing.Point(84, 0);
         this.labelVariable.Name = "labelVariable";
         this.labelVariable.Size = new System.Drawing.Size(192, 20);
         this.labelVariable.TabIndex = 6;
         this.labelVariable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.labelVariable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelVariable_MouseDown);
         // 
         // CustomEditorElementControl
         // 
         this.Controls.Add(this.labelVariable);
         this.Controls.Add(this.textBoxValue);
         this.Controls.Add(this.labelSolvable);
         this.Name = "CustomEditorElementControl";
         this.Size = new System.Drawing.Size(356, 20);
         this.ResumeLayout(false);

      }
		#endregion

      private void ProcessClick(MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) 
            {
               this.editorForm.SetElementsSelection(this, ModifierKey.Shift);
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
               this.editorForm.SetElementsSelection(this, ModifierKey.Ctrl);
            }
            else
            {
               this.editorForm.SetElementsSelection(this, ModifierKey.None);
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.editorForm.UnselectElements();
         }
      }

      private void labelVariable_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void labelSolvable_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void textBoxValue_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxValue;
         }
      }
   }
}
