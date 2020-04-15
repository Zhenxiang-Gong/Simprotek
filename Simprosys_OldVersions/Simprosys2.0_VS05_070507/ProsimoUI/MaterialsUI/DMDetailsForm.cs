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
      private ProcessVarNonEditableTextBox textBoxSpecificHeat;
      private ProcessVarLabel labelSpecificHeat;
      private IContainer components;

		public DMDetailsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public DMDetailsForm(DryingMaterial dryingMaterial, INumericFormat numericFormat)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         DryingMaterialCache dryingMaterialCache = new DryingMaterialCache(dryingMaterial);
         this.dmDetailsControl.SetDryingMaterial(dryingMaterialCache, numericFormat);
         if (dryingMaterialCache.GetDuhringLinesCache() == null)
         {
            int w = this.Width - this.dmDetailsControl.Width;
            int h = this.Height;
            this.Size = new Size(w, h);
         }

         if (dryingMaterialCache.MaterialType == MaterialType.GenericMaterial)
         {
            this.labelSpecificHeat.Visible = true;
            this.textBoxSpecificHeat.Visible = true;
            this.labelSpecificHeat.InitializeVariable(dryingMaterialCache.SpecificHeatOfAbsoluteDryMaterial);
            this.textBoxSpecificHeat.InitializeVariable(numericFormat, dryingMaterialCache.SpecificHeatOfAbsoluteDryMaterial);
         }
         else
         {
            this.labelSpecificHeat.Visible = false;
            this.textBoxSpecificHeat.Visible = false;
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
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.textBoxSpecificHeat = new ProsimoUI.ProcessVarNonEditableTextBox();
         this.labelSpecificHeat = new ProsimoUI.ProcessVarLabel();
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
         this.panel.Controls.Add(this.textBoxSpecificHeat);
         this.panel.Controls.Add(this.labelSpecificHeat);
         this.panel.Controls.Add(this.dmDetailsControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(706, 275);
         this.panel.TabIndex = 1;
         // 
         // textBoxSpecificHeat
         // 
         this.textBoxSpecificHeat.BackColor = System.Drawing.Color.Gainsboro;
         this.textBoxSpecificHeat.Location = new System.Drawing.Point(617, 12);
         this.textBoxSpecificHeat.Name = "textBoxSpecificHeat";
         this.textBoxSpecificHeat.ReadOnly = true;
         this.textBoxSpecificHeat.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeat.TabIndex = 119;
         this.textBoxSpecificHeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelSpecificHeat
         // 
         this.labelSpecificHeat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeat.Location = new System.Drawing.Point(323, 12);
         this.labelSpecificHeat.Name = "labelSpecificHeat";
         this.labelSpecificHeat.Size = new System.Drawing.Size(294, 20);
         this.labelSpecificHeat.TabIndex = 118;
         this.labelSpecificHeat.Text = "SpecificHeat";
         this.labelSpecificHeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // dmDetailsControl
         // 
         this.dmDetailsControl.Location = new System.Drawing.Point(4, 12);
         this.dmDetailsControl.Name = "dmDetailsControl";
         this.dmDetailsControl.Size = new System.Drawing.Size(700, 248);
         this.dmDetailsControl.TabIndex = 0;
         // 
         // DMDetailsForm
         // 
         this.ClientSize = new System.Drawing.Size(706, 275);
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
