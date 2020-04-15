using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;

namespace ProsimoUI.SubstancesUI
{
	/// <summary>
	/// Summary description for SubstanceDetailsForm.
	/// </summary>
	public class SubstanceDetailsForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private ProsimoUI.SubstancesUI.SubstanceDetailsControl substanceDetailsControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SubstanceDetailsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public SubstanceDetailsForm(Substance substance)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.substanceDetailsControl.SetSubstance(substance);
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
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.substanceDetailsControl = new ProsimoUI.SubstancesUI.SubstanceDetailsControl();
         this.panel.SuspendLayout();
         this.SuspendLayout();
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
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.substanceDetailsControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(490, 155);
         this.panel.TabIndex = 0;
         // 
         // substanceDetailsControl
         // 
         this.substanceDetailsControl.Location = new System.Drawing.Point(4, 4);
         this.substanceDetailsControl.Name = "substanceDetailsControl";
         this.substanceDetailsControl.Size = new System.Drawing.Size(484, 132);
         this.substanceDetailsControl.TabIndex = 0;
         // 
         // SubstanceDetailsForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(490, 155);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "SubstanceDetailsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Substance Details";
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
