using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI;

namespace ProsimoUI.ProcVarsEditor
{
	/// <summary>
	/// Summary description for RecommProcVarHeaderControl.
	/// </summary>
	public class RecommProcVarHeaderControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.Label labelExistingValue;
      private System.Windows.Forms.Label labelRecommendedValue;
      private System.Windows.Forms.Label labelNewValue;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RecommProcVarHeaderControl()
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
         this.labelName = new System.Windows.Forms.Label();
         this.labelExistingValue = new System.Windows.Forms.Label();
         this.labelRecommendedValue = new System.Windows.Forms.Label();
         this.labelNewValue = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelName
         // 
         this.labelName.BackColor = System.Drawing.Color.DarkGray;
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelName.Location = new System.Drawing.Point(0, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(192, 20);
         this.labelName.TabIndex = 0;
         this.labelName.Text = "Variable";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelExistingValue
         // 
         this.labelExistingValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelExistingValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelExistingValue.Location = new System.Drawing.Point(192, 0);
         this.labelExistingValue.Name = "labelExistingValue";
         this.labelExistingValue.Size = new System.Drawing.Size(90, 20);
         this.labelExistingValue.TabIndex = 1;
         this.labelExistingValue.Text = "Existing Value";
         this.labelExistingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelRecommendedValue
         // 
         this.labelRecommendedValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelRecommendedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelRecommendedValue.Location = new System.Drawing.Point(282, 0);
         this.labelRecommendedValue.Name = "labelRecommendedValue";
         this.labelRecommendedValue.Size = new System.Drawing.Size(90, 20);
         this.labelRecommendedValue.TabIndex = 2;
         this.labelRecommendedValue.Text = "Recomm. Value";
         this.labelRecommendedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelNewValue
         // 
         this.labelNewValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelNewValue.Location = new System.Drawing.Point(372, 0);
         this.labelNewValue.Name = "labelNewValue";
         this.labelNewValue.Size = new System.Drawing.Size(90, 20);
         this.labelNewValue.TabIndex = 3;
         this.labelNewValue.Text = "New Value";
         this.labelNewValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // RecommProcVarHeaderControl
         // 
         this.Controls.Add(this.labelNewValue);
         this.Controls.Add(this.labelRecommendedValue);
         this.Controls.Add(this.labelExistingValue);
         this.Controls.Add(this.labelName);
         this.Name = "RecommProcVarHeaderControl";
         this.Size = new System.Drawing.Size(464, 20);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
