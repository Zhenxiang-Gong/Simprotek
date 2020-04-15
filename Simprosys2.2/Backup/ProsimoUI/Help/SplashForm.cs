using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace ProsimoUI.Help
{
   /// <summary>
   /// This delegate enables asynchronous calls for setting
   /// the text property on a TextBox control.
   /// </summary>
   /// <param name="text"></param>
   delegate void SetTextCallback(string text);

	/// <summary>
	/// Summary description for SplashForm.
	/// </summary>
	public class SplashForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Label labelStatus;
      private System.Windows.Forms.Label labelProductName;
      private System.Windows.Forms.Label labelCompanyName;
      //private System.Windows.Forms.Label labelAppico;
      private System.Windows.Forms.Label labelCopyright2;
      private System.Windows.Forms.Label labelCopyright1;
      private System.Windows.Forms.Label labelVersion;
      private System.Windows.Forms.Label labelFullProductName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SplashForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.Cursor = Cursors.AppStarting;
         this.labelCompanyName.Text = ApplicationInformation.COMPANY;
         this.labelProductName.Text = ApplicationInformation.PRODUCT;
         //Version v = ApplicationInformation.ProductVersion;
         this.labelVersion.Text += ApplicationInformation.ProductVersionString;
         //this.labelBuild.Text = String.Format(this.labelBuild.Text, v.Build);
         //this.labelVersion.Text = UI.GetReleaseType();
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.Cursor = Cursors.Default;
         if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
         this.labelStatus = new System.Windows.Forms.Label();
         this.labelProductName = new System.Windows.Forms.Label();
         this.labelCompanyName = new System.Windows.Forms.Label();
         this.labelCopyright2 = new System.Windows.Forms.Label();
         this.labelCopyright1 = new System.Windows.Forms.Label();
         this.labelVersion = new System.Windows.Forms.Label();
         this.labelFullProductName = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelStatus
         // 
         this.labelStatus.BackColor = System.Drawing.Color.DarkGray;
         this.labelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelStatus.ForeColor = System.Drawing.Color.White;
         this.labelStatus.Location = new System.Drawing.Point(0, 280);
         this.labelStatus.Name = "labelStatus";
         this.labelStatus.Size = new System.Drawing.Size(360, 20);
         this.labelStatus.TabIndex = 1;
         this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelProductName
         // 
         this.labelProductName.BackColor = System.Drawing.Color.Transparent;
         this.labelProductName.Font = new System.Drawing.Font("Comic Sans MS", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelProductName.ForeColor = System.Drawing.Color.Red;
         this.labelProductName.Location = new System.Drawing.Point(49, 51);
         this.labelProductName.Name = "labelProductName";
         this.labelProductName.Size = new System.Drawing.Size(274, 52);
         this.labelProductName.TabIndex = 2;
         this.labelProductName.Text = "{0}";
         this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelCompanyName
         // 
         this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
         this.labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCompanyName.ForeColor = System.Drawing.Color.Red;
         this.labelCompanyName.Location = new System.Drawing.Point(125, 231);
         this.labelCompanyName.Name = "labelCompanyName";
         this.labelCompanyName.Size = new System.Drawing.Size(140, 39);
         this.labelCompanyName.TabIndex = 3;
         this.labelCompanyName.Text = "{0}";
         this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelCopyright2
         // 
         this.labelCopyright2.BackColor = System.Drawing.Color.Transparent;
         this.labelCopyright2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCopyright2.ForeColor = System.Drawing.Color.Red;
         this.labelCopyright2.Location = new System.Drawing.Point(269, 240);
         this.labelCopyright2.Name = "labelCopyright2";
         this.labelCopyright2.Size = new System.Drawing.Size(54, 20);
         this.labelCopyright2.TabIndex = 4;
         this.labelCopyright2.Text = "2006";
         this.labelCopyright2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCopyright1
         // 
         this.labelCopyright1.BackColor = System.Drawing.Color.Transparent;
         this.labelCopyright1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCopyright1.ForeColor = System.Drawing.Color.Red;
         this.labelCopyright1.Location = new System.Drawing.Point(43, 240);
         this.labelCopyright1.Name = "labelCopyright1";
         this.labelCopyright1.Size = new System.Drawing.Size(85, 20);
         this.labelCopyright1.TabIndex = 9;
         this.labelCopyright1.Text = "Copyright (c)";
         this.labelCopyright1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelAlphaBeta
         // 
         this.labelVersion.BackColor = System.Drawing.Color.Transparent;
         this.labelVersion.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelVersion.ForeColor = System.Drawing.Color.Red;
         this.labelVersion.Location = new System.Drawing.Point(114, 180);
         this.labelVersion.Name = "labelVersion";
         this.labelVersion.Size = new System.Drawing.Size(140, 32);
         this.labelVersion.TabIndex = 16;
         this.labelVersion.Text = "Version ";
         this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelFullProductName
         // 
         this.labelFullProductName.BackColor = System.Drawing.Color.Transparent;
         this.labelFullProductName.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelFullProductName.ForeColor = System.Drawing.Color.Red;
         this.labelFullProductName.Location = new System.Drawing.Point(7, 136);
         this.labelFullProductName.Name = "labelFullProductName";
         this.labelFullProductName.Size = new System.Drawing.Size(352, 20);
         this.labelFullProductName.TabIndex = 23;
         this.labelFullProductName.Text = ApplicationInformation.SLOGAN;
         this.labelFullProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // SplashForm
         // 
         this.BackColor = System.Drawing.SystemColors.Control;
         this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
         this.ClientSize = new System.Drawing.Size(360, 300);
         this.Controls.Add(this.labelFullProductName);
         this.Controls.Add(this.labelVersion);
         this.Controls.Add(this.labelCopyright1);
         this.Controls.Add(this.labelCopyright2);
         this.Controls.Add(this.labelCompanyName);
         this.Controls.Add(this.labelProductName);
         this.Controls.Add(this.labelStatus);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "SplashForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = ApplicationInformation.PRODUCT;
         this.ResumeLayout(false);

      }
		#endregion

      public void KillMe(object o, EventArgs e)
      {
         this.Close();
      }

      // This method demonstrates a pattern for making thread-safe
      // calls on a Windows Forms control. 
      //
      // If the calling thread is different from the thread that
      // created the TextBox control, this method creates a
      // SetTextCallback and calls itself asynchronously using the
      // Invoke method.
      //
      // If the calling thread is the same as the thread that created
      // the TextBox control, the Text property is set directly. 
      public void DisplayMessage(string text)
      {
         // InvokeRequired required compares the thread ID of the
         // calling thread to the thread ID of the creating thread.
         // If these threads are different, it returns true.
         if (this.labelStatus.InvokeRequired)
         {
            SetTextCallback d = new SetTextCallback(DisplayMessage);
            this.Invoke(d, new object[] { text });
         }
         else
         {
            this.labelStatus.Text = text;
         }
      }
	}
}
