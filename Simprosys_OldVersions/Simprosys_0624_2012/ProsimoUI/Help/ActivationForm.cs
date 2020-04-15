using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace ProsimoUI.Help
{
	/// <summary>
	/// Summary description for ActivationForm.
	/// </summary>
	public class ActivationForm : System.Windows.Forms.Form
	{
      public string Message
      {
         set
         {
            this.textBoxMessage.Text = value;
         }
      }
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.TextBox textBoxMessage;
      private ProsimoUI.Help.InformationControl informationControl;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private IContainer components;

		public ActivationForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         //this.labelAlphaBeta.Text = UI.GetReleaseType();
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
         this.textBoxMessage = new System.Windows.Forms.TextBox();
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
         this.panel.Controls.Add(this.textBoxMessage);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(304, 274);
         this.panel.TabIndex = 1;
         // 
         // textBoxMessage
         // 
         this.textBoxMessage.AcceptsReturn = true;
         this.textBoxMessage.Location = new System.Drawing.Point(4, 102);
         this.textBoxMessage.MaxLength = 50;
         this.textBoxMessage.Multiline = true;
         this.textBoxMessage.Name = "textBoxMessage";
         this.textBoxMessage.ReadOnly = true;
         this.textBoxMessage.Size = new System.Drawing.Size(292, 164);
         this.textBoxMessage.TabIndex = 2;
         this.textBoxMessage.TabStop = false;
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
         this.informationControl.Location = new System.Drawing.Point(0, 0);
         this.informationControl.Name = "informationControl";
         this.informationControl.Size = new System.Drawing.Size(300, 96);
         this.informationControl.TabIndex = 22;
         // 
         // ActivationForm
         // 
         this.ClientSize = new System.Drawing.Size(304, 274);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "ActivationForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Activation Information";
         this.TopMost = true;
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
