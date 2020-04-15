using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI.Help
{
	/// <summary>
	/// Summary description for UserManualForm.
	/// </summary>
	public class UserManualForm : System.Windows.Forms.Form
	{
      public const string USER_MANUAL_FILE = "UserManual.htm";
      public const string HELP_DIRECTORY = "Help\\";

      private AxSHDocVw.AxWebBrowser axWebBrowser;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public UserManualForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         string exePathName = Application.StartupPath + Path.DirectorySeparatorChar;
         string link = exePathName + UserManualForm.HELP_DIRECTORY + UserManualForm.USER_MANUAL_FILE;
         this.Navigate(link);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UserManualForm));
         this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
         this.SuspendLayout();
         // 
         // axWebBrowser
         // 
         this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.axWebBrowser.Enabled = true;
         this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
         this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
         this.axWebBrowser.Size = new System.Drawing.Size(700, 606);
         this.axWebBrowser.TabIndex = 0;
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // UserManualForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(700, 606);
         this.Controls.Add(this.axWebBrowser);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mainMenu;
         this.Name = "UserManualForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "ProSimO - User\'s Manual";
         ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void Navigate(string link)
      {
         try
         {
            object na = null;
            this.axWebBrowser.Navigate(link, ref na, ref na, ref na, ref na);
         }
         catch (Exception)
         {
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
