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
      private INumericFormat _NumericFormat;
      private MainForm _MainForm;
      private ListView _ListViewMaterials;

      private System.Windows.Forms.MainMenu _MainMenu;
      private System.Windows.Forms.MenuItem _MenuItemClose;
      //private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button _ButtonDetails;
      private System.Windows.Forms.Button _ButtonEdit;
      private System.Windows.Forms.Button _ButtonDelete;
      private System.Windows.Forms.Button _ButtonAdd;
      private System.Windows.Forms.Button _ButtonDuplicate;
      private DryingMaterialsControl _DryingMaterialsControl;
      private GroupBox _GroupBox1;
      private GroupBox _GroupBox2;
      private IContainer components;
      //private IContainer _Components;

		public DryingMaterialsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public DryingMaterialsForm(MainForm mainForm, INumericFormat numericFormat)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         _DryingMaterialsControl.Initialization();
         _MainForm = mainForm;
         _NumericFormat = numericFormat;
         _ListViewMaterials = _DryingMaterialsControl.ListViewMaterials;
         _ListViewMaterials.SelectedIndexChanged += new EventHandler(ListViewMaterials_SelectedIndexChanged);
         EnableDisableButtons();
         //ResizeEnd += new EventHandler(DryingMaterialsForm_ResizeEnd);
      }

      //void DryingMaterialsForm_ResizeEnd(object sender, EventArgs e)
      //{
      //   if (mainForm.Flowsheet != null) {
      //      mainForm.Flowsheet.ConnectionManager.DrawConnections();
      //   }
      //}

      /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         _ListViewMaterials.SelectedIndexChanged -= new EventHandler(ListViewMaterials_SelectedIndexChanged);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DryingMaterialsForm));
         this._MainMenu = new System.Windows.Forms.MainMenu(this.components);
         this._MenuItemClose = new System.Windows.Forms.MenuItem();
         this._ButtonDuplicate = new System.Windows.Forms.Button();
         this._ButtonDetails = new System.Windows.Forms.Button();
         this._ButtonEdit = new System.Windows.Forms.Button();
         this._ButtonDelete = new System.Windows.Forms.Button();
         this._ButtonAdd = new System.Windows.Forms.Button();
         this._GroupBox1 = new System.Windows.Forms.GroupBox();
         this._GroupBox2 = new System.Windows.Forms.GroupBox();
         this._DryingMaterialsControl = new ProsimoUI.MaterialsUI.DryingMaterialsControl();
         this._GroupBox1.SuspendLayout();
         this._GroupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // _MainMenu
         // 
         this._MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._MenuItemClose});
         // 
         // _MenuItemClose
         // 
         this._MenuItemClose.Index = 0;
         this._MenuItemClose.Text = "Close";
         this._MenuItemClose.Click += new System.EventHandler(this.OnMenuItemClose_Click);
         // 
         // _ButtonDuplicate
         // 
         this._ButtonDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this._ButtonDuplicate.Location = new System.Drawing.Point(30, 124);
         this._ButtonDuplicate.Name = "_ButtonDuplicate";
         this._ButtonDuplicate.Size = new System.Drawing.Size(75, 23);
         this._ButtonDuplicate.TabIndex = 12;
         this._ButtonDuplicate.Text = "Duplicate";
         this._ButtonDuplicate.Click += new System.EventHandler(this.OnButtonDuplicate_Click);
         // 
         // _ButtonDetails
         // 
         this._ButtonDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this._ButtonDetails.Location = new System.Drawing.Point(30, 237);
         this._ButtonDetails.Name = "_ButtonDetails";
         this._ButtonDetails.Size = new System.Drawing.Size(75, 23);
         this._ButtonDetails.TabIndex = 10;
         this._ButtonDetails.Text = "View Details";
         this._ButtonDetails.Click += new System.EventHandler(this.OnButtonDetails_Click);
         // 
         // _ButtonEdit
         // 
         this._ButtonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this._ButtonEdit.Location = new System.Drawing.Point(30, 182);
         this._ButtonEdit.Name = "_ButtonEdit";
         this._ButtonEdit.Size = new System.Drawing.Size(75, 23);
         this._ButtonEdit.TabIndex = 9;
         this._ButtonEdit.Text = "Edit";
         this._ButtonEdit.Click += new System.EventHandler(this.OnButtonEdit_Click);
         // 
         // _ButtonDelete
         // 
         this._ButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this._ButtonDelete.Location = new System.Drawing.Point(30, 70);
         this._ButtonDelete.Name = "_ButtonDelete";
         this._ButtonDelete.Size = new System.Drawing.Size(75, 23);
         this._ButtonDelete.TabIndex = 8;
         this._ButtonDelete.Text = "Delete";
         this._ButtonDelete.Click += new System.EventHandler(this.OnButtonDelete_Click);
         // 
         // _ButtonAdd
         // 
         this._ButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this._ButtonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this._ButtonAdd.Location = new System.Drawing.Point(30, 16);
         this._ButtonAdd.Name = "_ButtonAdd";
         this._ButtonAdd.Size = new System.Drawing.Size(75, 23);
         this._ButtonAdd.TabIndex = 7;
         this._ButtonAdd.Text = "Add";
         this._ButtonAdd.Click += new System.EventHandler(this.OnButtonAdd_Click);
         // 
         // _GroupBox1
         // 
         this._GroupBox1.Controls.Add(this._ButtonDuplicate);
         this._GroupBox1.Controls.Add(this._ButtonDetails);
         this._GroupBox1.Controls.Add(this._ButtonAdd);
         this._GroupBox1.Controls.Add(this._ButtonEdit);
         this._GroupBox1.Controls.Add(this._ButtonDelete);
         this._GroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
         this._GroupBox1.Location = new System.Drawing.Point(464, 0);
         this._GroupBox1.Margin = new System.Windows.Forms.Padding(0);
         this._GroupBox1.Name = "_GroupBox1";
         this._GroupBox1.Padding = new System.Windows.Forms.Padding(0);
         this._GroupBox1.Size = new System.Drawing.Size(126, 415);
         this._GroupBox1.TabIndex = 14;
         this._GroupBox1.TabStop = false;
         // 
         // _GroupBox2
         // 
         this._GroupBox2.Controls.Add(this._DryingMaterialsControl);
         this._GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
         this._GroupBox2.Location = new System.Drawing.Point(0, 0);
         this._GroupBox2.Name = "_GroupBox2";
         this._GroupBox2.Size = new System.Drawing.Size(464, 415);
         this._GroupBox2.TabIndex = 15;
         this._GroupBox2.TabStop = false;
         // 
         // _DryingMaterialsControl
         // 
         this._DryingMaterialsControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this._DryingMaterialsControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this._DryingMaterialsControl.Location = new System.Drawing.Point(3, 16);
         this._DryingMaterialsControl.Name = "_DryingMaterialsControl";
         this._DryingMaterialsControl.Size = new System.Drawing.Size(458, 396);
         this._DryingMaterialsControl.TabIndex = 13;
         // 
         // DryingMaterialsForm
         // 
         this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.ClientSize = new System.Drawing.Size(590, 415);
         this.Controls.Add(this._GroupBox2);
         this.Controls.Add(this._GroupBox1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this._MainMenu;
         this.MinimizeBox = false;
         this.Name = "DryingMaterialsForm";
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Drying Material Catalog";
         this._GroupBox1.ResumeLayout(false);
         this._GroupBox2.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void OnMenuItemClose_Click(object sender, System.EventArgs e)
      {
         Close();
      }

      private void OnButtonAdd_Click(object sender, System.EventArgs e)
      {
         AddOrEditDryingMaterialForm form = new AddOrEditDryingMaterialForm(null, _NumericFormat);
         form.ShowDialog();
      }

      private void OnButtonDelete_Click(object sender, System.EventArgs e)
      {
         _DryingMaterialsControl.DeleteSelectedElements();
         if(_ListViewMaterials.Items.Count > 0)
         {
            _ListViewMaterials.Items[0].Selected = true;
         }
         else
         {
            EnableDisableButtons();
         }
      }

      private void OnButtonEdit_Click(object sender, System.EventArgs e)
      {
         int selectedIndex = 0;
         for(int i = 0; i < _ListViewMaterials.Items.Count; i++)
         {
            if(_ListViewMaterials.Items[i].Selected)
            {
               selectedIndex = i;
               break;
            }
         }
         AddOrEditDryingMaterialForm form = new AddOrEditDryingMaterialForm(_DryingMaterialsControl.GetSelectedDryingMaterial(), _NumericFormat);
         form.ShowDialog();
         _ListViewMaterials.Items[selectedIndex].Selected = true;
      }

      private void OnButtonDetails_Click(object sender, System.EventArgs e)
      {
         DMDetailsForm form = new DMDetailsForm(_DryingMaterialsControl.GetSelectedDryingMaterial(), _NumericFormat);
         form.ShowDialog();
      }

      private void EnableDisableButtons()
      {
         if (_ListViewMaterials.SelectedItems.Count == 1)
         {
            _ButtonDuplicate.Enabled = true;
            _ButtonDetails.Enabled = true;
            if (_DryingMaterialsControl.GetSelectedDryingMaterial().IsUserDefined)
            {
               _ButtonEdit.Enabled = true;
               _ButtonDelete.Enabled = true;
            }
            else
            {
               _ButtonEdit.Enabled = false;
               _ButtonDelete.Enabled = false;
            }
         }
         else if (_ListViewMaterials.SelectedItems.Count > 1)
         {
            _ButtonDelete.Enabled = true;
            _ButtonDuplicate.Enabled = false;
            _ButtonDetails.Enabled = false;
            _ButtonEdit.Enabled = true;
         }
         else
         {
            _ButtonDelete.Enabled = false;
            _ButtonDuplicate.Enabled = false;
            _ButtonDetails.Enabled = false;
            _ButtonEdit.Enabled = false;
         }
      }

      private void ListViewMaterials_SelectedIndexChanged(object sender, EventArgs e)
      {
         EnableDisableButtons(); 
      }

      private void OnButtonDuplicate_Click(object sender, System.EventArgs e)
      {
         Duplicate();
         _ListViewMaterials.Items[_ListViewMaterials.Items.Count-1].Selected = true;
      }

      private void Duplicate()
      {
         if (_ListViewMaterials.SelectedItems.Count == 1)
         {
            DryingMaterial dryingMaterial = _DryingMaterialsControl.GetSelectedDryingMaterial();
            DryingMaterial dm = (DryingMaterial)dryingMaterial.Clone();
            dm.IsUserDefined = true;
            dm.Name = "Copy of " + dryingMaterial.Name;
            DryingMaterialCatalog.Instance.AddDryingMaterial(dm);
         }
      }
   }
}
