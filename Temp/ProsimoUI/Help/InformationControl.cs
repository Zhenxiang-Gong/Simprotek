using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ProsimoUI.Help
{
	/// <summary>
	/// Summary description for InformationControl.
	/// </summary>
	public class InformationControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelFullProductName;
      private System.Windows.Forms.Label labelVersion;
      private System.Windows.Forms.Label labelProductName;
      private System.Windows.Forms.Label labelCopyright1;
      private System.Windows.Forms.Label labelCopyright2;
      private System.Windows.Forms.Label labelCompanyName;
      private System.Windows.Forms.Label labelAppico;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InformationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.labelCompanyName.Text = ApplicationInformation.COMPANY;
         this.labelProductName.Text = ApplicationInformation.PRODUCT;
         Version v = new Version(ApplicationInformation.VERSION);
         this.labelVersion.Text = String.Format(this.labelVersion.Text, v.Major, v.Minor);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationControl));
         this.labelFullProductName = new System.Windows.Forms.Label();
         this.labelVersion = new System.Windows.Forms.Label();
         this.labelProductName = new System.Windows.Forms.Label();
         this.labelCopyright1 = new System.Windows.Forms.Label();
         this.labelCopyright2 = new System.Windows.Forms.Label();
         this.labelCompanyName = new System.Windows.Forms.Label();
         this.labelAppico = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelFullProductName
         // 
         this.labelFullProductName.BackColor = System.Drawing.Color.Transparent;
         this.labelFullProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelFullProductName.Location = new System.Drawing.Point(8, 36);
         this.labelFullProductName.Name = "labelFullProductName";
         this.labelFullProductName.Size = new System.Drawing.Size(284, 16);
         this.labelFullProductName.TabIndex = 23;
         this.labelFullProductName.Text = "Process Simulation and Optimization";
         this.labelFullProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelVersion
         // 
         this.labelVersion.BackColor = System.Drawing.Color.Transparent;
         this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelVersion.ForeColor = System.Drawing.Color.Black;
         this.labelVersion.Location = new System.Drawing.Point(182, 11);
         this.labelVersion.Name = "labelVersion";
         this.labelVersion.Size = new System.Drawing.Size(118, 20);
         this.labelVersion.TabIndex = 22;
         this.labelVersion.Text = "Version {0}.{1}";
         this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelProductName
         // 
         this.labelProductName.BackColor = System.Drawing.Color.Transparent;
         this.labelProductName.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelProductName.ForeColor = System.Drawing.Color.Black;
         this.labelProductName.Location = new System.Drawing.Point(44, 8);
         this.labelProductName.Name = "labelProductName";
         this.labelProductName.Size = new System.Drawing.Size(132, 22);
         this.labelProductName.TabIndex = 21;
         this.labelProductName.Text = "{0}";
         this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelCopyright1
         // 
         this.labelCopyright1.BackColor = System.Drawing.Color.Transparent;
         this.labelCopyright1.ForeColor = System.Drawing.Color.Black;
         this.labelCopyright1.Location = new System.Drawing.Point(4, 56);
         this.labelCopyright1.Name = "labelCopyright1";
         this.labelCopyright1.Size = new System.Drawing.Size(76, 16);
         this.labelCopyright1.TabIndex = 20;
         this.labelCopyright1.Text = "Copyright (c)";
         this.labelCopyright1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCopyright2
         // 
         this.labelCopyright2.BackColor = System.Drawing.Color.Transparent;
         this.labelCopyright2.ForeColor = System.Drawing.Color.Black;
         this.labelCopyright2.Location = new System.Drawing.Point(228, 56);
         this.labelCopyright2.Name = "labelCopyright2";
         this.labelCopyright2.Size = new System.Drawing.Size(64, 16);
         this.labelCopyright2.TabIndex = 19;
         this.labelCopyright2.Text = "2006";
         this.labelCopyright2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCompanyName
         // 
         this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
         this.labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCompanyName.ForeColor = System.Drawing.Color.Black;
         this.labelCompanyName.Location = new System.Drawing.Point(84, 56);
         this.labelCompanyName.Name = "labelCompanyName";
         this.labelCompanyName.Size = new System.Drawing.Size(140, 16);
         this.labelCompanyName.TabIndex = 18;
         this.labelCompanyName.Text = "{0}";
         this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelAppico
         // 
         this.labelAppico.BackColor = System.Drawing.Color.Transparent;
         this.labelAppico.Image = ((System.Drawing.Image)(resources.GetObject("labelAppico.Image")));
         this.labelAppico.Location = new System.Drawing.Point(4, 4);
         this.labelAppico.Name = "labelAppico";
         this.labelAppico.Size = new System.Drawing.Size(32, 32);
         this.labelAppico.TabIndex = 17;
         // 
         // InformationControl
         // 
         this.Controls.Add(this.labelFullProductName);
         this.Controls.Add(this.labelVersion);
         this.Controls.Add(this.labelProductName);
         this.Controls.Add(this.labelCopyright1);
         this.Controls.Add(this.labelCopyright2);
         this.Controls.Add(this.labelCompanyName);
         this.Controls.Add(this.labelAppico);
         this.Name = "InformationControl";
         this.Size = new System.Drawing.Size(300, 96);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
