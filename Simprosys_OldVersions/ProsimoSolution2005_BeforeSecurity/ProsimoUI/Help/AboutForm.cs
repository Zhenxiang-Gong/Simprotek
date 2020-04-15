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
      private System.Windows.Forms.Label labelAlphaBeta;
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
         this.labelAlphaBeta.Text = UI.GetReleaseType();
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
         this.panel = new System.Windows.Forms.Panel();
         this.informationControl = new ProsimoUI.Help.InformationControl();
         this.labelAlphaBeta = new System.Windows.Forms.Label();
         this.textBoxAbout = new System.Windows.Forms.TextBox();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.informationControl);
         this.panel.Controls.Add(this.labelAlphaBeta);
         this.panel.Controls.Add(this.textBoxAbout);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(304, 311);
         this.panel.TabIndex = 0;
         // 
         // informationControl
         // 
         this.informationControl.Location = new System.Drawing.Point(0, 0);
         this.informationControl.Name = "informationControl";
         this.informationControl.Size = new System.Drawing.Size(300, 96);
         this.informationControl.TabIndex = 16;
         // 
         // labelAlphaBeta
         // 
         this.labelAlphaBeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelAlphaBeta.Location = new System.Drawing.Point(76, 100);
         this.labelAlphaBeta.Name = "labelAlphaBeta";
         this.labelAlphaBeta.Size = new System.Drawing.Size(144, 20);
         this.labelAlphaBeta.TabIndex = 15;
         this.labelAlphaBeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // textBoxAbout
         // 
         this.textBoxAbout.Location = new System.Drawing.Point(4, 124);
         this.textBoxAbout.Multiline = true;
         this.textBoxAbout.Name = "textBoxAbout";
         this.textBoxAbout.ReadOnly = true;
         this.textBoxAbout.Size = new System.Drawing.Size(292, 176);
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
         // AboutForm
         // 
         this.ClientSize = new System.Drawing.Size(304, 311);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "AboutForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "About ProSimO";
         this.panel.ResumeLayout(false);
         this.panel.PerformLayout();
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
