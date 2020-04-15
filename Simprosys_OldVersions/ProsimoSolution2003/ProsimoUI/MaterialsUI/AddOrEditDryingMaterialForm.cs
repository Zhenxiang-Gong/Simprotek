using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using ProsimoUI.SubstancesUI;
using Prosimo.SubstanceLibrary;
using Prosimo.Materials;
using Prosimo;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for AddOrEditDryingMaterialForm.
	/// </summary>
	public class AddOrEditDryingMaterialForm : System.Windows.Forms.Form
	{
      private bool inConstruction;
      private DryingMaterialCache dryingMaterialCache;
      private bool isNew;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.Label labelMoistureSubstance;
      private System.Windows.Forms.Label labelType;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.ComboBox comboBoxType;
      private System.Windows.Forms.ComboBox comboBoxMoistureSubstance;
      private System.Windows.Forms.GroupBox groupBoxChooseSubstances;
      private System.Windows.Forms.GroupBox groupBoxSubstances;
      private System.Windows.Forms.GroupBox groupBoxDMComponents;
      private ProsimoUI.SubstancesUI.SubstancesControl substancesControl;
      private System.Windows.Forms.Button buttonNormalize;
      private System.Windows.Forms.Button buttonAddSubstance;
      private System.Windows.Forms.Button buttonRemoveSubstance;
      private System.Windows.Forms.Button buttonDuhringLines;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private ProsimoUI.MaterialsUI.MaterialComponentsControl dmComponentsControl;
      private System.Windows.Forms.Button buttonSubstanceDetails;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public AddOrEditDryingMaterialForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public AddOrEditDryingMaterialForm(DryingMaterial dryingMaterial)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;

         // populate the material type combobox
         this.comboBoxType.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_GENERIC_FOOD);
         this.comboBoxType.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_SPECIAL_FOOD);
         this.comboBoxType.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_INORGANIC);

         // populate the moisture substance combobox
         IEnumerator en = SubstanceCatalog.GetInstance().GetMoistureSubstanceList().GetEnumerator();
         while (en.MoveNext())
         {
            Substance substance = (Substance)en.Current;
            this.comboBoxMoistureSubstance.Items.Add(substance);
         }
         
         this.comboBoxType.SelectedIndex = 0;
         this.comboBoxMoistureSubstance.SelectedIndex = 0;

         if (dryingMaterial == null)
         {
            // new
            this.isNew = true;
            this.Text = "New Drying Material";

            this.dryingMaterialCache = new DryingMaterialCache(DryingMaterialCatalog.GetInstance());
            this.textBoxName.Text = "New Drying Material";

            string matTypeStr = this.comboBoxType.SelectedItem as string;
            MaterialType matTypeEnum = DryingMaterialsControl.GetDryingMaterialTypeAsEnum(matTypeStr);
            this.dryingMaterialCache.MaterialType = matTypeEnum;

            Substance subst = this.comboBoxMoistureSubstance.SelectedItem as Substance;
            this.dryingMaterialCache.MoistureSubstance = subst;
         }
         else
         {
            // edit
            this.isNew = false;
            this.Text = "Edit Drying Material";
            
            this.dryingMaterialCache = new DryingMaterialCache(dryingMaterial, DryingMaterialCatalog.GetInstance());
            this.textBoxName.Text = this.dryingMaterialCache.Name;
            
            this.comboBoxType.SelectedItem = DryingMaterialsControl.GetDryingMaterialTypeAsString(this.dryingMaterialCache.MaterialType);
            this.comboBoxMoistureSubstance.SelectedItem = this.dryingMaterialCache.MoistureSubstance;
         }

         this.substancesControl.SubstancesToShow = SubstancesToShow.Material;
         this.dmComponentsControl.SetMaterialComponents(this.dryingMaterialCache);
         this.UpdateTheUI(this.dryingMaterialCache.MaterialType);

         this.dryingMaterialCache.MaterialTypeChanged += new MaterialTypeChangedEventHandler(dryingMaterialCache_MaterialTypeChanged);
         this.dryingMaterialCache.MoistureSubstanceChanged += new MoistureSubstanceChangedEventHandler(dryingMaterialCache_MoistureSubstanceChanged);
         this.substancesControl.ListViewSubstances.SelectedIndexChanged += new EventHandler(ListViewSubstances_SelectedIndexChanged);

         this.inConstruction = false;                  
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.dryingMaterialCache.MaterialTypeChanged -= new MaterialTypeChangedEventHandler(dryingMaterialCache_MaterialTypeChanged);
         this.dryingMaterialCache.MoistureSubstanceChanged -= new MoistureSubstanceChangedEventHandler(dryingMaterialCache_MoistureSubstanceChanged);
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
         this.panel = new System.Windows.Forms.Panel();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.buttonDuhringLines = new System.Windows.Forms.Button();
         this.groupBoxChooseSubstances = new System.Windows.Forms.GroupBox();
         this.buttonAddSubstance = new System.Windows.Forms.Button();
         this.groupBoxDMComponents = new System.Windows.Forms.GroupBox();
         this.buttonNormalize = new System.Windows.Forms.Button();
         this.dmComponentsControl = new ProsimoUI.MaterialsUI.MaterialComponentsControl();
         this.buttonRemoveSubstance = new System.Windows.Forms.Button();
         this.groupBoxSubstances = new System.Windows.Forms.GroupBox();
         this.buttonSubstanceDetails = new System.Windows.Forms.Button();
         this.substancesControl = new ProsimoUI.SubstancesUI.SubstancesControl();
         this.comboBoxMoistureSubstance = new System.Windows.Forms.ComboBox();
         this.comboBoxType = new System.Windows.Forms.ComboBox();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.labelType = new System.Windows.Forms.Label();
         this.labelMoistureSubstance = new System.Windows.Forms.Label();
         this.labelName = new System.Windows.Forms.Label();
         this.panel.SuspendLayout();
         this.groupBoxChooseSubstances.SuspendLayout();
         this.groupBoxDMComponents.SuspendLayout();
         this.groupBoxSubstances.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.buttonCancel);
         this.panel.Controls.Add(this.buttonOk);
         this.panel.Controls.Add(this.buttonDuhringLines);
         this.panel.Controls.Add(this.groupBoxChooseSubstances);
         this.panel.Controls.Add(this.comboBoxMoistureSubstance);
         this.panel.Controls.Add(this.comboBoxType);
         this.panel.Controls.Add(this.textBoxName);
         this.panel.Controls.Add(this.labelType);
         this.panel.Controls.Add(this.labelMoistureSubstance);
         this.panel.Controls.Add(this.labelName);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(754, 376);
         this.panel.TabIndex = 0;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(380, 344);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 9;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(300, 344);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 8;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // buttonDuhringLines
         // 
         this.buttonDuhringLines.Location = new System.Drawing.Point(636, 8);
         this.buttonDuhringLines.Name = "buttonDuhringLines";
         this.buttonDuhringLines.Size = new System.Drawing.Size(96, 23);
         this.buttonDuhringLines.TabIndex = 7;
         this.buttonDuhringLines.Text = "Duhring Lines";
         this.buttonDuhringLines.Click += new System.EventHandler(this.buttonDuhringLines_Click);
         // 
         // groupBoxChooseSubstances
         // 
         this.groupBoxChooseSubstances.Controls.Add(this.buttonAddSubstance);
         this.groupBoxChooseSubstances.Controls.Add(this.groupBoxDMComponents);
         this.groupBoxChooseSubstances.Controls.Add(this.groupBoxSubstances);
         this.groupBoxChooseSubstances.Location = new System.Drawing.Point(4, 40);
         this.groupBoxChooseSubstances.Name = "groupBoxChooseSubstances";
         this.groupBoxChooseSubstances.Size = new System.Drawing.Size(744, 300);
         this.groupBoxChooseSubstances.TabIndex = 6;
         this.groupBoxChooseSubstances.TabStop = false;
         // 
         // buttonAddSubstance
         // 
         this.buttonAddSubstance.Location = new System.Drawing.Point(368, 120);
         this.buttonAddSubstance.Name = "buttonAddSubstance";
         this.buttonAddSubstance.TabIndex = 2;
         this.buttonAddSubstance.Text = "Add >>";
         this.buttonAddSubstance.Click += new System.EventHandler(this.buttonAddSubstance_Click);
         // 
         // groupBoxDMComponents
         // 
         this.groupBoxDMComponents.Controls.Add(this.buttonNormalize);
         this.groupBoxDMComponents.Controls.Add(this.dmComponentsControl);
         this.groupBoxDMComponents.Controls.Add(this.buttonRemoveSubstance);
         this.groupBoxDMComponents.Location = new System.Drawing.Point(448, 16);
         this.groupBoxDMComponents.Name = "groupBoxDMComponents";
         this.groupBoxDMComponents.Size = new System.Drawing.Size(292, 236);
         this.groupBoxDMComponents.TabIndex = 1;
         this.groupBoxDMComponents.TabStop = false;
         this.groupBoxDMComponents.Text = "Drying Material Components";
         // 
         // buttonNormalize
         // 
         this.buttonNormalize.Location = new System.Drawing.Point(192, 208);
         this.buttonNormalize.Name = "buttonNormalize";
         this.buttonNormalize.TabIndex = 1;
         this.buttonNormalize.Text = "Normalize";
         this.buttonNormalize.Click += new System.EventHandler(this.buttonNormalize_Click);
         // 
         // dmComponentsControl
         // 
         this.dmComponentsControl.Location = new System.Drawing.Point(8, 20);
         this.dmComponentsControl.Name = "dmComponentsControl";
         this.dmComponentsControl.Size = new System.Drawing.Size(280, 184);
         this.dmComponentsControl.TabIndex = 0;
         // 
         // buttonRemoveSubstance
         // 
         this.buttonRemoveSubstance.Location = new System.Drawing.Point(20, 208);
         this.buttonRemoveSubstance.Name = "buttonRemoveSubstance";
         this.buttonRemoveSubstance.TabIndex = 3;
         this.buttonRemoveSubstance.Text = "Remove";
         this.buttonRemoveSubstance.Click += new System.EventHandler(this.buttonRemoveSubstance_Click);
         // 
         // groupBoxSubstances
         // 
         this.groupBoxSubstances.Controls.Add(this.buttonSubstanceDetails);
         this.groupBoxSubstances.Controls.Add(this.substancesControl);
         this.groupBoxSubstances.Location = new System.Drawing.Point(8, 16);
         this.groupBoxSubstances.Name = "groupBoxSubstances";
         this.groupBoxSubstances.Size = new System.Drawing.Size(356, 280);
         this.groupBoxSubstances.TabIndex = 0;
         this.groupBoxSubstances.TabStop = false;
         this.groupBoxSubstances.Text = "Substances";
         // 
         // buttonSubstanceDetails
         // 
         this.buttonSubstanceDetails.Location = new System.Drawing.Point(8, 252);
         this.buttonSubstanceDetails.Name = "buttonSubstanceDetails";
         this.buttonSubstanceDetails.TabIndex = 1;
         this.buttonSubstanceDetails.Text = "Details";
         this.buttonSubstanceDetails.Click += new System.EventHandler(this.buttonSubstanceDetails_Click);
         // 
         // substancesControl
         // 
         this.substancesControl.Location = new System.Drawing.Point(8, 20);
         this.substancesControl.Name = "substancesControl";
         this.substancesControl.Size = new System.Drawing.Size(344, 228);
         this.substancesControl.SubstancesToShow = ProsimoUI.SubstancesToShow.All;
         this.substancesControl.TabIndex = 0;
         // 
         // comboBoxMoistureSubstance
         // 
         this.comboBoxMoistureSubstance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMoistureSubstance.Location = new System.Drawing.Point(492, 8);
         this.comboBoxMoistureSubstance.Name = "comboBoxMoistureSubstance";
         this.comboBoxMoistureSubstance.Size = new System.Drawing.Size(121, 21);
         this.comboBoxMoistureSubstance.TabIndex = 5;
         this.comboBoxMoistureSubstance.SelectedIndexChanged += new System.EventHandler(this.comboBoxMoistureSubstance_SelectedIndexChanged);
         // 
         // comboBoxType
         // 
         this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxType.Location = new System.Drawing.Point(236, 8);
         this.comboBoxType.Name = "comboBoxType";
         this.comboBoxType.Size = new System.Drawing.Size(121, 21);
         this.comboBoxType.TabIndex = 4;
         this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(56, 8);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(120, 20);
         this.textBoxName.TabIndex = 3;
         this.textBoxName.Text = "";
         // 
         // labelType
         // 
         this.labelType.Location = new System.Drawing.Point(192, 12);
         this.labelType.Name = "labelType";
         this.labelType.Size = new System.Drawing.Size(40, 16);
         this.labelType.TabIndex = 2;
         this.labelType.Text = "Type:";
         this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMoistureSubstance
         // 
         this.labelMoistureSubstance.Location = new System.Drawing.Point(372, 12);
         this.labelMoistureSubstance.Name = "labelMoistureSubstance";
         this.labelMoistureSubstance.Size = new System.Drawing.Size(116, 16);
         this.labelMoistureSubstance.TabIndex = 1;
         this.labelMoistureSubstance.Text = "Moisture Substance:";
         this.labelMoistureSubstance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelName
         // 
         this.labelName.Location = new System.Drawing.Point(8, 12);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(44, 16);
         this.labelName.TabIndex = 0;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // AddOrEditDryingMaterialForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(754, 376);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "AddOrEditDryingMaterialForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "New Drying Material";
         this.panel.ResumeLayout(false);
         this.groupBoxChooseSubstances.ResumeLayout(false);
         this.groupBoxDMComponents.ResumeLayout(false);
         this.groupBoxSubstances.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonNormalize_Click(object sender, System.EventArgs e)
      {
         this.dmComponentsControl.DryingMaterialCache.Normalize();      
      }

      private void buttonDuhringLines_Click(object sender, System.EventArgs e)
      {
         DuhringLinesForm form = new DuhringLinesForm(dryingMaterialCache);
         form.ShowDialog();
      }

      private void buttonAddSubstance_Click(object sender, System.EventArgs e)
      {
         ArrayList list = this.substancesControl.GetSelectedSubstances();
         this.dryingMaterialCache.AddMaterialComponents(list);
      }

      private void buttonRemoveSubstance_Click(object sender, System.EventArgs e)
      {
         this.dmComponentsControl.DeleteSelectedElements();
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         Substance substance = (Substance)this.comboBoxMoistureSubstance.SelectedItem;         
         ErrorMessage error = this.dryingMaterialCache.FinishSpecifications(this.textBoxName.Text, this.isNew);
         if (error != null)
            UI.ShowError(error);
         else
            this.Close();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void dryingMaterialCache_MaterialTypeChanged(MaterialType newType, MaterialType oldType)
      {
         if (newType != oldType)
         {
            this.inConstruction = true;
            this.UpdateTheUI(newType);
            this.inConstruction = false;
         }
      }

      private void UpdateTheUI(MaterialType newType)
      {
         if (newType == MaterialType.GenericFood)
         {
            this.substancesControl.Enabled = false;
            this.buttonAddSubstance.Enabled = false;
            this.buttonRemoveSubstance.Enabled = false;
            this.buttonSubstanceDetails.Enabled = false;
            this.comboBoxMoistureSubstance.Enabled = false;
         }
         else
         {
            this.substancesControl.Enabled = true;
            this.buttonAddSubstance.Enabled = true;
            this.buttonRemoveSubstance.Enabled = true;
            this.buttonSubstanceDetails.Enabled = true;
            this.comboBoxMoistureSubstance.Enabled = true;
         }
         this.EnableDisableSubstanceDetailsButton();
      }

      private void comboBoxType_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            string matTypeStr = this.comboBoxType.SelectedItem as string;
            MaterialType matTypeEnum = DryingMaterialsControl.GetDryingMaterialTypeAsEnum(matTypeStr);
            this.dryingMaterialCache.MaterialType = matTypeEnum;
         }
      }

      private void comboBoxMoistureSubstance_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            Substance subst = this.comboBoxMoistureSubstance.SelectedItem as Substance;
            this.dryingMaterialCache.MoistureSubstance = subst;
         }
      }

      private void buttonSubstanceDetails_Click(object sender, System.EventArgs e)
      {
         ListViewItem lvi = (ListViewItem)this.substancesControl.ListViewSubstances.SelectedItems[0];
         ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
         Substance substance = SubstanceCatalog.GetInstance().GetSubstance(lvsi.Text);
         SubstanceDetailsForm form = new SubstanceDetailsForm(substance);
         form.ShowDialog();
      }

      private void EnableDisableSubstanceDetailsButton()
      {
         if (this.substancesControl.ListViewSubstances.SelectedItems.Count == 1)
            this.buttonSubstanceDetails.Enabled = true;
         else
            this.buttonSubstanceDetails.Enabled = false;
      }

      private void ListViewSubstances_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.EnableDisableSubstanceDetailsButton();
      }

      private void dryingMaterialCache_MoistureSubstanceChanged(Substance newSubstance, Substance oldSubstance)
      {
         if (!newSubstance.Name.Equals(oldSubstance.Name))
         {
            this.inConstruction = true;
            this.comboBoxMoistureSubstance.SelectedItem = newSubstance;
            this.inConstruction = false;
         }
      }

   }
}
