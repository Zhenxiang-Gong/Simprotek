using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for NewProcessSettingsForm.
	/// </summary>
	public class NewProcessSettingsForm : System.Windows.Forms.Form
	{
      private NewProcessSettings newProcessSettings;
      private MainForm mainForm;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.GroupBox groupBoxSelectDryingMaterial;
      private System.Windows.Forms.GroupBox groupBoxSelectDryingGas;
      private ProsimoUI.MaterialsUI.DryingGasesControl dryingGasesControl;
      private ProsimoUI.MaterialsUI.DryingMaterialsControl dryingMaterialsControl;
      private System.Windows.Forms.TextBox textBoxDryingMaterial;
      private System.Windows.Forms.Label labelDryingMaterial;
      private System.Windows.Forms.TextBox textBoxDryingGas;
      private System.Windows.Forms.Label labelDryingGas;
      private System.Windows.Forms.Button buttonSetDryingGas;
      private System.Windows.Forms.Button buttonSetDryingMaterial;
      private IContainer components;

		public NewProcessSettingsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public NewProcessSettingsForm(MainForm mainForm, NewProcessSettings newProcessSettings)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.mainForm = mainForm;
         this.newProcessSettings = newProcessSettings;

         this.dryingGasesControl.ListViewGases.MultiSelect = false;
         this.dryingMaterialsControl.ListViewMaterials.MultiSelect = false;

         this.dryingGasesControl.SelectDryingGas(this.newProcessSettings.DryingGasName);
         this.dryingMaterialsControl.SelectDryingMaterial(this.newProcessSettings.DryingMaterialName);

         this.textBoxDryingGas.Text = this.newProcessSettings.DryingGasName;
         this.textBoxDryingMaterial.Text = this.newProcessSettings.DryingMaterialName;

         this.ResizeEnd += new EventHandler(NewProcessSettingsForm_ResizeEnd);
      }

      void NewProcessSettingsForm_ResizeEnd(object sender, EventArgs e)
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProcessSettingsForm));
         this.panel = new System.Windows.Forms.Panel();
         this.textBoxDryingMaterial = new System.Windows.Forms.TextBox();
         this.labelDryingMaterial = new System.Windows.Forms.Label();
         this.textBoxDryingGas = new System.Windows.Forms.TextBox();
         this.labelDryingGas = new System.Windows.Forms.Label();
         this.groupBoxSelectDryingGas = new System.Windows.Forms.GroupBox();
         this.buttonSetDryingGas = new System.Windows.Forms.Button();
         this.dryingGasesControl = new ProsimoUI.MaterialsUI.DryingGasesControl();
         this.groupBoxSelectDryingMaterial = new System.Windows.Forms.GroupBox();
         this.buttonSetDryingMaterial = new System.Windows.Forms.Button();
         this.dryingMaterialsControl = new ProsimoUI.MaterialsUI.DryingMaterialsControl();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel.SuspendLayout();
         this.groupBoxSelectDryingGas.SuspendLayout();
         this.groupBoxSelectDryingMaterial.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.Controls.Add(this.textBoxDryingMaterial);
         this.panel.Controls.Add(this.labelDryingMaterial);
         this.panel.Controls.Add(this.textBoxDryingGas);
         this.panel.Controls.Add(this.labelDryingGas);
         this.panel.Controls.Add(this.groupBoxSelectDryingGas);
         this.panel.Controls.Add(this.groupBoxSelectDryingMaterial);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(781, 337);
         this.panel.TabIndex = 0;
         // 
         // textBoxDryingMaterial
         // 
         this.textBoxDryingMaterial.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingMaterial.Location = new System.Drawing.Point(96, 44);
         this.textBoxDryingMaterial.Name = "textBoxDryingMaterial";
         this.textBoxDryingMaterial.ReadOnly = true;
         this.textBoxDryingMaterial.Size = new System.Drawing.Size(100, 20);
         this.textBoxDryingMaterial.TabIndex = 16;
         // 
         // labelDryingMaterial
         // 
         this.labelDryingMaterial.Location = new System.Drawing.Point(8, 48);
         this.labelDryingMaterial.Name = "labelDryingMaterial";
         this.labelDryingMaterial.Size = new System.Drawing.Size(84, 16);
         this.labelDryingMaterial.TabIndex = 15;
         this.labelDryingMaterial.Text = "Drying Material:";
         this.labelDryingMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxDryingGas
         // 
         this.textBoxDryingGas.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingGas.Location = new System.Drawing.Point(96, 16);
         this.textBoxDryingGas.Name = "textBoxDryingGas";
         this.textBoxDryingGas.ReadOnly = true;
         this.textBoxDryingGas.Size = new System.Drawing.Size(100, 20);
         this.textBoxDryingGas.TabIndex = 14;
         // 
         // labelDryingGas
         // 
         this.labelDryingGas.Location = new System.Drawing.Point(8, 20);
         this.labelDryingGas.Name = "labelDryingGas";
         this.labelDryingGas.Size = new System.Drawing.Size(84, 16);
         this.labelDryingGas.TabIndex = 13;
         this.labelDryingGas.Text = "Drying Gas:";
         this.labelDryingGas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBoxSelectDryingGas
         // 
         this.groupBoxSelectDryingGas.Controls.Add(this.buttonSetDryingGas);
         this.groupBoxSelectDryingGas.Controls.Add(this.dryingGasesControl);
         this.groupBoxSelectDryingGas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.groupBoxSelectDryingGas.Location = new System.Drawing.Point(2, 73);
         this.groupBoxSelectDryingGas.Name = "groupBoxSelectDryingGas";
         this.groupBoxSelectDryingGas.Size = new System.Drawing.Size(303, 259);
         this.groupBoxSelectDryingGas.TabIndex = 5;
         this.groupBoxSelectDryingGas.TabStop = false;
         this.groupBoxSelectDryingGas.Text = "Select Drying Gas";
         // 
         // buttonSetDryingGas
         // 
         this.buttonSetDryingGas.Location = new System.Drawing.Point(94, 227);
         this.buttonSetDryingGas.Name = "buttonSetDryingGas";
         this.buttonSetDryingGas.Size = new System.Drawing.Size(75, 23);
         this.buttonSetDryingGas.TabIndex = 1;
         this.buttonSetDryingGas.Text = "Set";
         this.buttonSetDryingGas.Click += new System.EventHandler(this.buttonSetDryingGas_Click);
         // 
         // dryingGasesControl
         // 
         this.dryingGasesControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.dryingGasesControl.Location = new System.Drawing.Point(1, 23);
         this.dryingGasesControl.Name = "dryingGasesControl";
         this.dryingGasesControl.Size = new System.Drawing.Size(291, 198);
         this.dryingGasesControl.TabIndex = 0;
         // 
         // groupBoxSelectDryingMaterial
         // 
         this.groupBoxSelectDryingMaterial.Controls.Add(this.buttonSetDryingMaterial);
         this.groupBoxSelectDryingMaterial.Controls.Add(this.dryingMaterialsControl);
         this.groupBoxSelectDryingMaterial.Location = new System.Drawing.Point(311, 16);
         this.groupBoxSelectDryingMaterial.Name = "groupBoxSelectDryingMaterial";
         this.groupBoxSelectDryingMaterial.Size = new System.Drawing.Size(464, 316);
         this.groupBoxSelectDryingMaterial.TabIndex = 4;
         this.groupBoxSelectDryingMaterial.TabStop = false;
         this.groupBoxSelectDryingMaterial.Text = "Select Drying Material";
         // 
         // buttonSetDryingMaterial
         // 
         this.buttonSetDryingMaterial.Location = new System.Drawing.Point(176, 284);
         this.buttonSetDryingMaterial.Name = "buttonSetDryingMaterial";
         this.buttonSetDryingMaterial.Size = new System.Drawing.Size(75, 23);
         this.buttonSetDryingMaterial.TabIndex = 2;
         this.buttonSetDryingMaterial.Text = "Set";
         this.buttonSetDryingMaterial.Click += new System.EventHandler(this.buttonSetDryingMaterial_Click);
         // 
         // dryingMaterialsControl
         // 
         this.dryingMaterialsControl.Anchor = System.Windows.Forms.AnchorStyles.Left;
         this.dryingMaterialsControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.dryingMaterialsControl.Location = new System.Drawing.Point(2, 19);
         this.dryingMaterialsControl.Name = "dryingMaterialsControl";
         this.dryingMaterialsControl.Size = new System.Drawing.Size(452, 253);
         this.dryingMaterialsControl.TabIndex = 0;
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
         // NewProcessSettingsForm
         // 
         this.ClientSize = new System.Drawing.Size(781, 337);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "NewProcessSettingsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "New Flowsheet Settings";
         this.panel.ResumeLayout(false);
         this.panel.PerformLayout();
         this.groupBoxSelectDryingGas.ResumeLayout(false);
         this.groupBoxSelectDryingMaterial.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonSetDryingGas_Click(object sender, System.EventArgs e)
      {
         if (this.dryingGasesControl.GetSelectedDryingGas() != null)
         {
            this.newProcessSettings.DryingGasName = this.dryingGasesControl.GetSelectedDryingGas().Name;
            this.textBoxDryingGas.Text = this.newProcessSettings.DryingGasName;
         }
      }

      private void buttonSetDryingMaterial_Click(object sender, System.EventArgs e)
      {
         if (this.dryingMaterialsControl.GetSelectedDryingMaterial() != null)
         {
            this.newProcessSettings.DryingMaterialName = this.dryingMaterialsControl.GetSelectedDryingMaterial().Name;
            this.textBoxDryingMaterial.Text = this.newProcessSettings.DryingMaterialName;
         }
      }
   }
}
