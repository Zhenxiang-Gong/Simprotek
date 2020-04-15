using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ProsimoUI.FormulaEditor
{
	/// <summary>
	/// Summary description for FormulaEditorHeaderControl.
	/// </summary>
	public class FormulaEditorHeaderControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelVarFormula;
      private System.Windows.Forms.Label labelVarName;
      private System.Windows.Forms.Label labelVarId;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormulaEditorHeaderControl()
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
         this.labelVarFormula = new System.Windows.Forms.Label();
         this.labelVarName = new System.Windows.Forms.Label();
         this.labelVarId = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelVarFormula
         // 
         this.labelVarFormula.BackColor = System.Drawing.Color.DarkGray;
         this.labelVarFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelVarFormula.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labelVarFormula.Location = new System.Drawing.Point(320, 0);
         this.labelVarFormula.Name = "labelVarFormula";
         this.labelVarFormula.Size = new System.Drawing.Size(217, 20);
         this.labelVarFormula.TabIndex = 5;
         this.labelVarFormula.Text = "Formula";
         this.labelVarFormula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelVarName
         // 
         this.labelVarName.BackColor = System.Drawing.Color.DarkGray;
         this.labelVarName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelVarName.Dock = System.Windows.Forms.DockStyle.Left;
         this.labelVarName.Location = new System.Drawing.Point(80, 0);
         this.labelVarName.Name = "labelVarName";
         this.labelVarName.Size = new System.Drawing.Size(240, 20);
         this.labelVarName.TabIndex = 4;
         this.labelVarName.Text = "Variable";
         this.labelVarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelVarId
         // 
         this.labelVarId.BackColor = System.Drawing.Color.DarkGray;
         this.labelVarId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelVarId.Dock = System.Windows.Forms.DockStyle.Left;
         this.labelVarId.Location = new System.Drawing.Point(0, 0);
         this.labelVarId.Name = "labelVarId";
         this.labelVarId.Size = new System.Drawing.Size(80, 20);
         this.labelVarId.TabIndex = 3;
         this.labelVarId.Text = "ID";
         this.labelVarId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // FormulaEditorHeaderControl
         // 
         this.Controls.Add(this.labelVarFormula);
         this.Controls.Add(this.labelVarName);
         this.Controls.Add(this.labelVarId);
         this.Name = "FormulaEditorHeaderControl";
         this.Size = new System.Drawing.Size(537, 20);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
