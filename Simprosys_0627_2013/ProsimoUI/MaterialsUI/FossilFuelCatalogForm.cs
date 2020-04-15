using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for FuelCatalogForm.
	/// </summary>
	public class FossilFuelCatalogForm : System.Windows.Forms.Form
	{
      private INumericFormat numericFormat;
      private MainForm mainForm;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      //private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button buttonDetails;
      private System.Windows.Forms.Button buttonEdit;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Button buttonDuplicate;
      private FossilFuelCatalogControl fossilFuelCatalogControl;
      private ListView listViewFossilFuel;
      private GroupBox groupBox1;
      private GroupBox groupBox2;
      private IContainer components;

		public FossilFuelCatalogForm()
		{
         //
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public FossilFuelCatalogForm(MainForm mainForm, INumericFormat numericFormat)
      {
         this.mainForm = mainForm;
         this.numericFormat = numericFormat;
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
         this.fossilFuelCatalogControl.Initialization();
         this.EnableDisableButtons();
         this.listViewFossilFuel = fossilFuelCatalogControl.ListViewFossilFuelList;
         this.listViewFossilFuel.SelectedIndexChanged += new EventHandler(ListViewFossilFuelCatalog_SelectedIndexChanged);
         //this.ResizeEnd += new EventHandler(DryingMaterialsForm_ResizeEnd);
      }

      //void DryingMaterialsForm_ResizeEnd(object sender, EventArgs e)
      //{
      //   if (this.mainForm.Flowsheet != null) {
      //      this.mainForm.Flowsheet.ConnectionManager.DrawConnections();
      //   }
      //}

      /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.listViewFossilFuel.SelectedIndexChanged -= new EventHandler(ListViewFossilFuelCatalog_SelectedIndexChanged);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FossilFuelCatalogForm));
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.buttonDuplicate = new System.Windows.Forms.Button();
         this.buttonDetails = new System.Windows.Forms.Button();
         this.buttonEdit = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.fossilFuelCatalogControl = new ProsimoUI.MaterialsUI.FossilFuelCatalogControl();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
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
         // buttonDuplicate
         // 
         this.buttonDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonDuplicate.Location = new System.Drawing.Point(30, 124);
         this.buttonDuplicate.Name = "buttonDuplicate";
         this.buttonDuplicate.Size = new System.Drawing.Size(75, 23);
         this.buttonDuplicate.TabIndex = 12;
         this.buttonDuplicate.Text = "Duplicate";
         this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
         // 
         // buttonDetails
         // 
         this.buttonDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonDetails.Location = new System.Drawing.Point(30, 237);
         this.buttonDetails.Name = "buttonDetails";
         this.buttonDetails.Size = new System.Drawing.Size(75, 23);
         this.buttonDetails.TabIndex = 10;
         this.buttonDetails.Text = "View Details";
         this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
         // 
         // buttonEdit
         // 
         this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonEdit.Location = new System.Drawing.Point(30, 182);
         this.buttonEdit.Name = "buttonEdit";
         this.buttonEdit.Size = new System.Drawing.Size(75, 23);
         this.buttonEdit.TabIndex = 9;
         this.buttonEdit.Text = "Edit";
         this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
         // 
         // buttonDelete
         // 
         this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonDelete.Location = new System.Drawing.Point(30, 70);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.Size = new System.Drawing.Size(75, 23);
         this.buttonDelete.TabIndex = 8;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // buttonAdd
         // 
         this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.buttonAdd.Location = new System.Drawing.Point(30, 16);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.Size = new System.Drawing.Size(75, 23);
         this.buttonAdd.TabIndex = 7;
         this.buttonAdd.Text = "Add";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.buttonDuplicate);
         this.groupBox1.Controls.Add(this.buttonDetails);
         this.groupBox1.Controls.Add(this.buttonAdd);
         this.groupBox1.Controls.Add(this.buttonEdit);
         this.groupBox1.Controls.Add(this.buttonDelete);
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
         this.groupBox1.Location = new System.Drawing.Point(464, 0);
         this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
         this.groupBox1.Size = new System.Drawing.Size(126, 415);
         this.groupBox1.TabIndex = 14;
         this.groupBox1.TabStop = false;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.fossilFuelCatalogControl);
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox2.Location = new System.Drawing.Point(0, 0);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(464, 415);
         this.groupBox2.TabIndex = 15;
         this.groupBox2.TabStop = false;
         // 
         // fossilFuelCatalogControl
         // 
         this.fossilFuelCatalogControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.fossilFuelCatalogControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fossilFuelCatalogControl.Location = new System.Drawing.Point(3, 16);
         this.fossilFuelCatalogControl.Name = "fossilFuelCatalogControl";
         this.fossilFuelCatalogControl.Size = new System.Drawing.Size(458, 396);
         this.fossilFuelCatalogControl.TabIndex = 13;
         // 
         // FossilFuelCatalogForm
         // 
         this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.ClientSize = new System.Drawing.Size(590, 415);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "FossilFuelCatalogForm";
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Drying Fuel Catalog";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         FossilFuelSpecificationForm form = new FossilFuelSpecificationForm(null, this.numericFormat);
         form.PopulateSubstanceList();
         form.ShowDialog();
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.fossilFuelCatalogControl.DeleteSelectedElements();
         if(listViewFossilFuel.Items.Count > 0)
         {
            listViewFossilFuel.Items[0].Selected = true;
         }
         else
         {
            EnableDisableButtons();
         }
      }

      private void buttonEdit_Click(object sender, System.EventArgs e)
      {
         int selectedIndex = 0;
         for(int i = 0; i < listViewFossilFuel.Items.Count; i++)
         {
            if(listViewFossilFuel.Items[i].Selected)
            {
               selectedIndex = i;
               break;
            }
         }
         FossilFuel fossilFuel = fossilFuelCatalogControl.GetSelectedFossilFuel();
         FossilFuelCache fossilFuelCache = new FossilFuelCache(fossilFuel, FossilFuelCatalog.Instance);
         FossilFuelSpecificationForm form = new FossilFuelSpecificationForm(fossilFuelCache, this.numericFormat);
         form.PopulateSubstanceList();
         form.ShowDialog();
         listViewFossilFuel.Items[selectedIndex].Selected = true;
      }

      private void buttonDetails_Click(object sender, System.EventArgs e)
      {
         FossilFuelDetailsForm form = new FossilFuelDetailsForm(fossilFuelCatalogControl.GetSelectedFossilFuel());
         form.ShowDialog();
      }

      private void EnableDisableButtons()
      {
         if (this.fossilFuelCatalogControl.ListViewFossilFuelList.SelectedItems.Count == 1)
         {
            this.buttonDuplicate.Enabled = true;
            this.buttonDetails.Enabled = true;
            if (this.fossilFuelCatalogControl.GetSelectedFossilFuel().IsUserDefined)
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
         else if (this.fossilFuelCatalogControl.ListViewFossilFuelList.SelectedItems.Count > 1)
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

      private void ListViewFossilFuelCatalog_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.EnableDisableButtons(); 
      }

      private void buttonDuplicate_Click(object sender, System.EventArgs e)
      {
         Duplicate();
         listViewFossilFuel.Items[listViewFossilFuel.Items.Count - 1].Selected = true;
      }

      private void Duplicate()
      {
         if (listViewFossilFuel.SelectedItems.Count == 1)
         {
            FossilFuel fossilFuel = this.fossilFuelCatalogControl.GetSelectedFossilFuel();
            FossilFuel ff = (FossilFuel)fossilFuel.Clone();
            ff.Name = "Copy of " + fossilFuel.Name;
            FossilFuelCatalog.Instance.AddFossilFule(ff);
         }
      }
   }
}
