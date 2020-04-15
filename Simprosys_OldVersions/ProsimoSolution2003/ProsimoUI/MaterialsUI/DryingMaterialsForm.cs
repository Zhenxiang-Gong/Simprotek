using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DryingMaterialsForm.
	/// </summary>
	public class DryingMaterialsForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button buttonDetails;
      private System.Windows.Forms.Button buttonEdit;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonAdd;
      private ProsimoUI.MaterialsUI.DryingMaterialsControl dryingMaterialsControl;
      private System.Windows.Forms.Button buttonDuplicate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryingMaterialsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.EnableDisableButtons();
         this.dryingMaterialsControl.ListViewMaterials.SelectedIndexChanged += new EventHandler(ListViewMaterials_SelectedIndexChanged);
		}

      /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.dryingMaterialsControl.ListViewMaterials.SelectedIndexChanged -= new EventHandler(ListViewMaterials_SelectedIndexChanged);
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
         this.dryingMaterialsControl = new ProsimoUI.MaterialsUI.DryingMaterialsControl();
         this.buttonDetails = new System.Windows.Forms.Button();
         this.buttonEdit = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.buttonDuplicate = new System.Windows.Forms.Button();
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
         this.panel.Controls.Add(this.buttonDuplicate);
         this.panel.Controls.Add(this.dryingMaterialsControl);
         this.panel.Controls.Add(this.buttonDetails);
         this.panel.Controls.Add(this.buttonEdit);
         this.panel.Controls.Add(this.buttonDelete);
         this.panel.Controls.Add(this.buttonAdd);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(418, 251);
         this.panel.TabIndex = 0;
         // 
         // dryingMaterialsControl
         // 
         this.dryingMaterialsControl.Location = new System.Drawing.Point(4, 4);
         this.dryingMaterialsControl.Name = "dryingMaterialsControl";
         this.dryingMaterialsControl.Size = new System.Drawing.Size(324, 240);
         this.dryingMaterialsControl.TabIndex = 11;
         // 
         // buttonDetails
         // 
         this.buttonDetails.Location = new System.Drawing.Point(336, 124);
         this.buttonDetails.Name = "buttonDetails";
         this.buttonDetails.TabIndex = 10;
         this.buttonDetails.Text = "View Details";
         this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
         // 
         // buttonEdit
         // 
         this.buttonEdit.Location = new System.Drawing.Point(336, 92);
         this.buttonEdit.Name = "buttonEdit";
         this.buttonEdit.TabIndex = 9;
         this.buttonEdit.Text = "Edit";
         this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(336, 60);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.TabIndex = 8;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // buttonAdd
         // 
         this.buttonAdd.Location = new System.Drawing.Point(336, 28);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.TabIndex = 7;
         this.buttonAdd.Text = "Add";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // buttonDuplicate
         // 
         this.buttonDuplicate.Location = new System.Drawing.Point(336, 156);
         this.buttonDuplicate.Name = "buttonDuplicate";
         this.buttonDuplicate.TabIndex = 12;
         this.buttonDuplicate.Text = "Duplicate";
         this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
         // 
         // DryingMaterialsForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(418, 251);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "DryingMaterialsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Drying Materials";
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         AddOrEditDryingMaterialForm form = new AddOrEditDryingMaterialForm(null);
         form.ShowDialog();
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.dryingMaterialsControl.DeleteSelectedElements();
      }

      private void buttonEdit_Click(object sender, System.EventArgs e)
      {
         AddOrEditDryingMaterialForm form = new AddOrEditDryingMaterialForm(this.dryingMaterialsControl.GetSelectedDryingMaterial());
         form.ShowDialog();
      }

      private void buttonDetails_Click(object sender, System.EventArgs e)
      {
         DMDetailsForm form = new DMDetailsForm(this.dryingMaterialsControl.GetSelectedDryingMaterial());
         form.ShowDialog();
      }

      private void EnableDisableButtons()
      {
         if (this.dryingMaterialsControl.ListViewMaterials.SelectedItems.Count == 1)
         {
            this.buttonDuplicate.Enabled = true;
            this.buttonDetails.Enabled = true;
            if (this.dryingMaterialsControl.GetSelectedDryingMaterial().IsUserDefined)
            {
               this.buttonEdit.Enabled = true;
               this.buttonDelete.Enabled = true;
            }
            else
            {
               this.buttonEdit.Enabled = false;
               this.buttonDelete.Enabled = false;
            }
         }
         else if (this.dryingMaterialsControl.ListViewMaterials.SelectedItems.Count > 1)
         {
            this.buttonDelete.Enabled = true;
            this.buttonDuplicate.Enabled = false;
            this.buttonDetails.Enabled = false;
            this.buttonEdit.Enabled = true;
         }
         else
         {
            this.buttonDelete.Enabled = false;
            this.buttonDuplicate.Enabled = false;
            this.buttonDetails.Enabled = false;
            this.buttonEdit.Enabled = false;
         }
      }

      private void ListViewMaterials_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.EnableDisableButtons(); 
      }

      private void buttonDuplicate_Click(object sender, System.EventArgs e)
      {
         this.Duplicate();      
      }

      private void Duplicate()
      {
         if (this.dryingMaterialsControl.ListViewMaterials.SelectedItems.Count == 1)
         {
            DryingMaterial dryingMaterial = this.dryingMaterialsControl.GetSelectedDryingMaterial();
            DryingMaterial dm = (DryingMaterial)dryingMaterial.Clone();
            dm.IsUserDefined = true;
            dm.Name = "Copy of " + dryingMaterial.Name;
            DryingMaterialCatalog.GetInstance().AddDryingMaterial(dm);
         }
      }
   }
}
