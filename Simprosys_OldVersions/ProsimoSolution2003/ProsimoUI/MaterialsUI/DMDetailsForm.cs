using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DMDetailsForm.
	/// </summary>
	public class DMDetailsForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private ProsimoUI.MaterialsUI.DMDetailsControl dmDetailsControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DMDetailsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public DMDetailsForm(DryingMaterial dryingMaterial)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         DryingMaterialCache dryingMaterialCache = new DryingMaterialCache(dryingMaterial);
         this.dmDetailsControl.SetDryingMaterial(dryingMaterialCache);
         if (dryingMaterialCache.GetDuhringLinesCache() == null)
         {
            int w = this.Width - this.dmDetailsControl.Width;
            int h = this.Height;
            this.Size = new Size(w, h);
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
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.dmDetailsControl = new ProsimoUI.MaterialsUI.DMDetailsControl();
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
         this.panel.Controls.Add(this.dmDetailsControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(706, 251);
         this.panel.TabIndex = 1;
         // 
         // dmDetailsControl
         // 
         this.dmDetailsControl.Location = new System.Drawing.Point(4, 12);
         this.dmDetailsControl.Name = "dmDetailsControl";
         this.dmDetailsControl.Size = new System.Drawing.Size(700, 224);
         this.dmDetailsControl.TabIndex = 0;
         // 
         // DMDetailsForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(706, 251);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "DMDetailsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Drying Material Details";
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
