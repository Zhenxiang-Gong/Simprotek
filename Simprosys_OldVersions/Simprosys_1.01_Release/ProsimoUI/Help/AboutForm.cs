using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace ProsimoUI.Help
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
      private MainForm mainForm;

      public string Message
      {
         set
         {
            this.textBoxAbout.Text = value;
         }
      }

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.TextBox textBoxAbout;
      private ProsimoUI.Help.InformationControl informationControl;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private IContainer components;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
      }

      public AboutForm(MainForm mainForm)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.mainForm = mainForm;
         //this.labelAlphaBeta.Text = UI.GetReleaseType();
         this.ResizeEnd += new EventHandler(AboutForm_ResizeEnd);
      }

      void AboutForm_ResizeEnd(object sender, EventArgs e)
      {
         if (this.mainForm.Flowsheet != null)
         {
            this.mainForm.Flowsheet.ConnectionManager.DrawConnections();
         }
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
         this.panel = new System.Windows.Forms.Panel();
         this.textBoxAbout = new System.Windows.Forms.TextBox();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.informationControl = new ProsimoUI.Help.InformationControl();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.informationControl);
         this.panel.Controls.Add(this.textBoxAbout);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(321, 230);
         this.panel.TabIndex = 0;
         // 
         // textBoxAbout
         // 
         this.textBoxAbout.Location = new System.Drawing.Point(4, 103);
         this.textBoxAbout.Multiline = true;
         this.textBoxAbout.Name = "textBoxAbout";
         this.textBoxAbout.ReadOnly = true;
         this.textBoxAbout.Size = new System.Drawing.Size(310, 176);
         this.textBoxAbout.TabIndex = 1;
         this.textBoxAbout.TabStop = false;
         this.textBoxAbout.WordWrap = false;
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
         // informationControl
         // 
         this.informationControl.Location = new System.Drawing.Point(0, 1);
         this.informationControl.Name = "informationControl";
         this.informationControl.Size = new System.Drawing.Size(314, 96);
         this.informationControl.TabIndex = 16;
         this.informationControl.Load += new System.EventHandler(this.informationControl_Load);
         // 
         // AboutForm
         // 
         this.ClientSize = new System.Drawing.Size(321, 230);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "AboutForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "About Simprosys";
         this.panel.ResumeLayout(false);
         this.panel.PerformLayout();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void informationControl_Load(object sender, EventArgs e) {

      }
	}
}
