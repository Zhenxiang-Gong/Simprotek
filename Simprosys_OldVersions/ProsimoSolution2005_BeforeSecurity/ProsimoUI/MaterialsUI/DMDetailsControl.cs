using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DMDetailsControl.
	/// </summary>
	public class DMDetailsControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelDMName;
      private System.Windows.Forms.TextBox textBoxDMName;
      private System.Windows.Forms.TextBox textBoxUserDef;
      private System.Windows.Forms.Label labelUserDef;
      private System.Windows.Forms.TextBox textBoxMaterialType;
      private System.Windows.Forms.Label labelMaterialType;
      private System.Windows.Forms.TextBox textBoxMoisture;
      private System.Windows.Forms.Label labelMoisture;
      private System.Windows.Forms.GroupBox groupBoxMaterialComponents;
      private System.Windows.Forms.ListView listViewMaterialComponents;
      private System.Windows.Forms.ColumnHeader columnHeaderSubstanceName;
      private System.Windows.Forms.ColumnHeader columnHeaderMassFraction;
      private ProsimoUI.MaterialsUI.ReadOnlyDuhringLinesControl readOnlyDuhringLinesControl;
      private System.Windows.Forms.GroupBox groupBoxDuhringLines;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DMDetailsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.labelDMName = new System.Windows.Forms.Label();
         this.textBoxDMName = new System.Windows.Forms.TextBox();
         this.textBoxMoisture = new System.Windows.Forms.TextBox();
         this.labelMoisture = new System.Windows.Forms.Label();
         this.textBoxMaterialType = new System.Windows.Forms.TextBox();
         this.labelMaterialType = new System.Windows.Forms.Label();
         this.textBoxUserDef = new System.Windows.Forms.TextBox();
         this.labelUserDef = new System.Windows.Forms.Label();
         this.groupBoxMaterialComponents = new System.Windows.Forms.GroupBox();
         this.listViewMaterialComponents = new System.Windows.Forms.ListView();
         this.columnHeaderSubstanceName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderMassFraction = new System.Windows.Forms.ColumnHeader();
         this.readOnlyDuhringLinesControl = new ProsimoUI.MaterialsUI.ReadOnlyDuhringLinesControl();
         this.groupBoxDuhringLines = new System.Windows.Forms.GroupBox();
         this.groupBoxMaterialComponents.SuspendLayout();
         this.groupBoxDuhringLines.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelDMName
         // 
         this.labelDMName.Location = new System.Drawing.Point(4, 4);
         this.labelDMName.Name = "labelDMName";
         this.labelDMName.Size = new System.Drawing.Size(44, 16);
         this.labelDMName.TabIndex = 0;
         this.labelDMName.Text = "Name:";
         this.labelDMName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxDMName
         // 
         this.textBoxDMName.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDMName.Location = new System.Drawing.Point(52, 0);
         this.textBoxDMName.Name = "textBoxDMName";
         this.textBoxDMName.ReadOnly = true;
         this.textBoxDMName.Size = new System.Drawing.Size(164, 20);
         this.textBoxDMName.TabIndex = 1;
         // 
         // textBoxMoisture
         // 
         this.textBoxMoisture.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMoisture.Location = new System.Drawing.Point(104, 72);
         this.textBoxMoisture.Name = "textBoxMoisture";
         this.textBoxMoisture.ReadOnly = true;
         this.textBoxMoisture.Size = new System.Drawing.Size(112, 20);
         this.textBoxMoisture.TabIndex = 5;
         // 
         // labelMoisture
         // 
         this.labelMoisture.Location = new System.Drawing.Point(4, 76);
         this.labelMoisture.Name = "labelMoisture";
         this.labelMoisture.Size = new System.Drawing.Size(100, 16);
         this.labelMoisture.TabIndex = 4;
         this.labelMoisture.Text = "Moisture:";
         this.labelMoisture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxMaterialType
         // 
         this.textBoxMaterialType.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMaterialType.Location = new System.Drawing.Point(104, 52);
         this.textBoxMaterialType.Name = "textBoxMaterialType";
         this.textBoxMaterialType.ReadOnly = true;
         this.textBoxMaterialType.Size = new System.Drawing.Size(112, 20);
         this.textBoxMaterialType.TabIndex = 7;
         // 
         // labelMaterialType
         // 
         this.labelMaterialType.Location = new System.Drawing.Point(4, 56);
         this.labelMaterialType.Name = "labelMaterialType";
         this.labelMaterialType.Size = new System.Drawing.Size(100, 16);
         this.labelMaterialType.TabIndex = 6;
         this.labelMaterialType.Text = "Material Type:";
         this.labelMaterialType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxUserDef
         // 
         this.textBoxUserDef.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxUserDef.Location = new System.Drawing.Point(104, 32);
         this.textBoxUserDef.Name = "textBoxUserDef";
         this.textBoxUserDef.ReadOnly = true;
         this.textBoxUserDef.Size = new System.Drawing.Size(112, 20);
         this.textBoxUserDef.TabIndex = 9;
         // 
         // labelUserDef
         // 
         this.labelUserDef.Location = new System.Drawing.Point(4, 36);
         this.labelUserDef.Name = "labelUserDef";
         this.labelUserDef.Size = new System.Drawing.Size(100, 16);
         this.labelUserDef.TabIndex = 8;
         this.labelUserDef.Text = "User Defined:";
         this.labelUserDef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // groupBoxMaterialComponents
         // 
         this.groupBoxMaterialComponents.Controls.Add(this.listViewMaterialComponents);
         this.groupBoxMaterialComponents.Location = new System.Drawing.Point(0, 104);
         this.groupBoxMaterialComponents.Name = "groupBoxMaterialComponents";
         this.groupBoxMaterialComponents.Size = new System.Drawing.Size(220, 144);
         this.groupBoxMaterialComponents.TabIndex = 10;
         this.groupBoxMaterialComponents.TabStop = false;
         this.groupBoxMaterialComponents.Text = "Material Components";
         // 
         // listViewMaterialComponents
         // 
         this.listViewMaterialComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSubstanceName,
            this.columnHeaderMassFraction});
         this.listViewMaterialComponents.Location = new System.Drawing.Point(8, 24);
         this.listViewMaterialComponents.MultiSelect = false;
         this.listViewMaterialComponents.Name = "listViewMaterialComponents";
         this.listViewMaterialComponents.Size = new System.Drawing.Size(204, 112);
         this.listViewMaterialComponents.TabIndex = 0;
         this.listViewMaterialComponents.UseCompatibleStateImageBehavior = false;
         this.listViewMaterialComponents.View = System.Windows.Forms.View.Details;
         this.listViewMaterialComponents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewMaterialComponents_ColumnClick);
         // 
         // columnHeaderSubstanceName
         // 
         this.columnHeaderSubstanceName.Text = "Substance Name";
         this.columnHeaderSubstanceName.Width = 100;
         // 
         // columnHeaderMassFraction
         // 
         this.columnHeaderMassFraction.Text = "Mass Fraction";
         this.columnHeaderMassFraction.Width = 80;
         // 
         // readOnlyDuhringLinesControl
         // 
         this.readOnlyDuhringLinesControl.Location = new System.Drawing.Point(4, 20);
         this.readOnlyDuhringLinesControl.Name = "readOnlyDuhringLinesControl";
         this.readOnlyDuhringLinesControl.Size = new System.Drawing.Size(468, 196);
         this.readOnlyDuhringLinesControl.TabIndex = 11;
         // 
         // groupBoxDuhringLines
         // 
         this.groupBoxDuhringLines.Controls.Add(this.readOnlyDuhringLinesControl);
         this.groupBoxDuhringLines.Location = new System.Drawing.Point(220, 24);
         this.groupBoxDuhringLines.Name = "groupBoxDuhringLines";
         this.groupBoxDuhringLines.Size = new System.Drawing.Size(476, 224);
         this.groupBoxDuhringLines.TabIndex = 12;
         this.groupBoxDuhringLines.TabStop = false;
         this.groupBoxDuhringLines.Text = "Duhring Lines";
         // 
         // DMDetailsControl
         // 
         this.Controls.Add(this.groupBoxDuhringLines);
         this.Controls.Add(this.groupBoxMaterialComponents);
         this.Controls.Add(this.textBoxUserDef);
         this.Controls.Add(this.labelUserDef);
         this.Controls.Add(this.textBoxMaterialType);
         this.Controls.Add(this.labelMaterialType);
         this.Controls.Add(this.textBoxMoisture);
         this.Controls.Add(this.labelMoisture);
         this.Controls.Add(this.textBoxDMName);
         this.Controls.Add(this.labelDMName);
         this.Name = "DMDetailsControl";
         this.Size = new System.Drawing.Size(700, 248);
         this.groupBoxMaterialComponents.ResumeLayout(false);
         this.groupBoxDuhringLines.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }
		#endregion

      public void SetDryingMaterial(DryingMaterialCache dryingMaterialCache, INumericFormat iNumericFormat)
      {
         this.textBoxDMName.Text = dryingMaterialCache.Name;
         this.textBoxUserDef.Text = UI.GetBoolAsYesNo(dryingMaterialCache.IsUserDefined);
         this.textBoxMaterialType.Text = DryingMaterialsControl.GetDryingMaterialTypeAsString(dryingMaterialCache.MaterialType);
         this.textBoxMoisture.Text = dryingMaterialCache.MoistureSubstance.ToString();
         this.SetMaterialComponents(dryingMaterialCache, iNumericFormat);
         this.readOnlyDuhringLinesControl.SetDuhringLinesCache(dryingMaterialCache, iNumericFormat);
         if (dryingMaterialCache.GetDuhringLinesCache() == null)
         {
            this.HideDuhringLinesUI();
         }
      }

      private void SetMaterialComponents(DryingMaterialCache cache, INumericFormat iNumericFormat)
      {
         IEnumerator en = cache.MaterialComponentList.GetEnumerator();
         while (en.MoveNext())
         {
            MaterialComponent mc = (MaterialComponent)en.Current;
            ListViewItem lvi = new ListViewItem();
                    
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, mc.Name);
            lvi.SubItems.Insert(0, lvsi);
            
            lvsi = new ListViewItem.ListViewSubItem(lvi, mc.MassFraction.Value.ToString(iNumericFormat.NumericFormatString));
            lvi.SubItems.Insert(1, lvsi);

            this.listViewMaterialComponents.Items.Add(lvi);
         }
      }

      public void HideDuhringLinesUI()
      {
         int w = this.Width - this.groupBoxDuhringLines.Width;
         int h = this.Height;
         this.groupBoxDuhringLines.Visible = false;
         this.Size = new Size(w, h);
      }

      private void listViewMaterialComponents_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewMaterialComponents.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewMaterialComponents.Sort();
      }
	}
}
