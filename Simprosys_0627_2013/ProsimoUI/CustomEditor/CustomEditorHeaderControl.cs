using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for CustomEditorHeaderControl.
	/// </summary>
	public class CustomEditorHeaderControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelHeaderVariable;
      private System.Windows.Forms.Label labelHeaderValue;
      private System.Windows.Forms.Label labelHeaderSolvable;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomEditorHeaderControl()
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
         this.labelHeaderVariable = new System.Windows.Forms.Label();
         this.labelHeaderValue = new System.Windows.Forms.Label();
         this.labelHeaderSolvable = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelHeaderVariable
         // 
         this.labelHeaderVariable.BackColor = System.Drawing.Color.DarkGray;
         this.labelHeaderVariable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelHeaderVariable.Location = new System.Drawing.Point(84, 0);
         this.labelHeaderVariable.Name = "labelHeaderVariable";
         this.labelHeaderVariable.Size = new System.Drawing.Size(192, 20);
         this.labelHeaderVariable.TabIndex = 9;
         this.labelHeaderVariable.Text = "Variable";
         this.labelHeaderVariable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelHeaderValue
         // 
         this.labelHeaderValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelHeaderValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelHeaderValue.Location = new System.Drawing.Point(276, 0);
         this.labelHeaderValue.Name = "labelHeaderValue";
         this.labelHeaderValue.Size = new System.Drawing.Size(80, 20);
         this.labelHeaderValue.TabIndex = 8;
         this.labelHeaderValue.Text = "Value";
         this.labelHeaderValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelHeaderSolvable
         // 
         this.labelHeaderSolvable.BackColor = System.Drawing.Color.DarkGray;
         this.labelHeaderSolvable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelHeaderSolvable.Location = new System.Drawing.Point(0, 0);
         this.labelHeaderSolvable.Name = "labelHeaderSolvable";
         this.labelHeaderSolvable.Size = new System.Drawing.Size(84, 20);
         this.labelHeaderSolvable.TabIndex = 7;
         this.labelHeaderSolvable.Text = "Unit Op/Stream";
         this.labelHeaderSolvable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // CustomEditorHeaderControl
         // 
         this.Controls.Add(this.labelHeaderVariable);
         this.Controls.Add(this.labelHeaderValue);
         this.Controls.Add(this.labelHeaderSolvable);
         this.Name = "CustomEditorHeaderControl";
         this.Size = new System.Drawing.Size(356, 20);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
