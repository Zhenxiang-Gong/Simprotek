using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.FormulaEditor
{
	/// <summary>
	/// Summary description for FormulaEditorElementControl.
	/// </summary>
	public class FormulaEditorElementControl : System.Windows.Forms.UserControl
	{
      private ProcessVar var;
      private Hashtable formulae;

      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.Label labelId;
      private System.Windows.Forms.TextBox textBoxFormula;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FormulaEditorElementControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FormulaEditorElementControl(ProcessVar var, Hashtable formulae)
		{
			InitializeComponent();

         this.var = var;
         this.formulae = formulae;
         this.labelId.Text = "v" + var.ID.ToString();
         this.labelName.Text = var.Name;
         this.textBoxFormula.Text = this.formulae[var.ID] as string;
         if (var.IsSpecified)
            this.textBoxFormula.Enabled = true;
         else
            this.textBoxFormula.Enabled = false;
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
         this.labelName = new System.Windows.Forms.Label();
         this.labelId = new System.Windows.Forms.Label();
         this.textBoxFormula = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // labelName
         // 
         this.labelName.BackColor = System.Drawing.SystemColors.Control;
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelName.Dock = System.Windows.Forms.DockStyle.Left;
         this.labelName.Location = new System.Drawing.Point(80, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(240, 20);
         this.labelName.TabIndex = 3;
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelId
         // 
         this.labelId.BackColor = System.Drawing.SystemColors.Control;
         this.labelId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelId.Dock = System.Windows.Forms.DockStyle.Left;
         this.labelId.Location = new System.Drawing.Point(0, 0);
         this.labelId.Name = "labelId";
         this.labelId.Size = new System.Drawing.Size(80, 20);
         this.labelId.TabIndex = 2;
         this.labelId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxFormula
         // 
         this.textBoxFormula.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBoxFormula.Location = new System.Drawing.Point(320, 0);
         this.textBoxFormula.Name = "textBoxFormula";
         this.textBoxFormula.Size = new System.Drawing.Size(217, 20);
         this.textBoxFormula.TabIndex = 4;
         this.textBoxFormula.Text = "";
         this.textBoxFormula.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFormula_Validating);
         this.textBoxFormula.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFormula_KeyUp);
         // 
         // FormulaEditorElementControl
         // 
         this.Controls.Add(this.textBoxFormula);
         this.Controls.Add(this.labelName);
         this.Controls.Add(this.labelId);
         this.Name = "FormulaEditorElementControl";
         this.Size = new System.Drawing.Size(537, 20);
         this.ResumeLayout(false);

      }
		#endregion

      private void textBoxFormula_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxFormula;
            this.textBoxFormula.SelectAll();
         }
      }

      private void textBoxFormula_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TextBox tb = (TextBox)sender;
         string formula = tb.Text.Trim();
         if (FormulaParser.GetInstance().Validate(formula))
         {
            this.formulae[this.var.ID] = formula;
         }
         else
         {
            string message = "Wrong Formula!"; 
            MessageBox.Show(message, "Formula Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
         }
      }
	}
}
