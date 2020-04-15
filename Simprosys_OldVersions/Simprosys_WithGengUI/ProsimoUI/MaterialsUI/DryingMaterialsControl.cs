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
	/// Summary description for DryingMaterialsControl.
	/// </summary>
	public class DryingMaterialsControl : System.Windows.Forms.UserControl
	{
      public const string STR_MAT_TYPE_GENERIC_FOOD = "Generic Food";
      public const string STR_MAT_TYPE_SPECIAL_FOOD = "Special Food";
      public const string STR_MAT_TYPE_GENERIC_MATERIAL = "Generic Material";

      private bool inConstruction;

      public ListView ListViewMaterials
      {
         get {return this.listViewMaterials;}
      }

      private System.Windows.Forms.ComboBox comboBoxTypes;
      private System.Windows.Forms.Label labelFilterByType;
      private System.Windows.Forms.ListView listViewMaterials;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderType;
      private System.Windows.Forms.ColumnHeader columnHeaderUserDefined;
      private System.Windows.Forms.ComboBox comboBoxUserDef;
      private System.Windows.Forms.Label labelFilterByUserDef;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryingMaterialsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.inConstruction = true;

         this.comboBoxUserDef.Items.Add(UI.STR_ALL);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_USER_DEF);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_NOT_USER_DEF);
         this.comboBoxUserDef.SelectedIndex = 0;

         this.comboBoxTypes.Items.Add(UI.STR_ALL);
         this.comboBoxTypes.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_GENERIC_FOOD);
         this.comboBoxTypes.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_GENERIC_MATERIAL);
//         this.comboBoxTypes.Items.Add(DryingMaterialsControl.STR_MAT_TYPE_SPECIAL_FOOD);
         this.comboBoxTypes.SelectedIndex = 0;

         this.PopulateDryingMaterialList();

         DryingMaterialCatalog.GetInstance().DryingMaterialAdded += new DryingMaterialAddedEventHandler(DryingMaterialsControl_DryingMaterialAdded);
         DryingMaterialCatalog.GetInstance().DryingMaterialChanged += new DryingMaterialChangedEventHandler(DryingMaterialsControl_DryingMaterialChanged);
         DryingMaterialCatalog.GetInstance().DryingMaterialDeleted += new DryingMaterialDeletedEventHandler(DryingMaterialsControl_DryingMaterialDeleted);

         this.inConstruction = false;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         DryingMaterialCatalog.GetInstance().DryingMaterialAdded -= new DryingMaterialAddedEventHandler(DryingMaterialsControl_DryingMaterialAdded);
         DryingMaterialCatalog.GetInstance().DryingMaterialChanged -= new DryingMaterialChangedEventHandler(DryingMaterialsControl_DryingMaterialChanged);
         DryingMaterialCatalog.GetInstance().DryingMaterialDeleted -= new DryingMaterialDeletedEventHandler(DryingMaterialsControl_DryingMaterialDeleted);
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
         this.comboBoxTypes = new System.Windows.Forms.ComboBox();
         this.labelFilterByType = new System.Windows.Forms.Label();
         this.listViewMaterials = new System.Windows.Forms.ListView();
         this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderUserDefined = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
         this.comboBoxUserDef = new System.Windows.Forms.ComboBox();
         this.labelFilterByUserDef = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // comboBoxTypes
         // 
         this.comboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTypes.Location = new System.Drawing.Point(140, 216);
         this.comboBoxTypes.Name = "comboBoxTypes";
         this.comboBoxTypes.Size = new System.Drawing.Size(121, 21);
         this.comboBoxTypes.TabIndex = 0;
         this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
         // 
         // labelFilterByType
         // 
         this.labelFilterByType.Location = new System.Drawing.Point(4, 216);
         this.labelFilterByType.Name = "labelFilterByType";
         this.labelFilterByType.Size = new System.Drawing.Size(132, 20);
         this.labelFilterByType.TabIndex = 1;
         this.labelFilterByType.Text = "Filter by Material Type:";
         this.labelFilterByType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // listViewMaterials
         // 
         this.listViewMaterials.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.columnHeaderName,
                                                                                            this.columnHeaderUserDefined,
                                                                                            this.columnHeaderType});
         this.listViewMaterials.FullRowSelect = true;
         this.listViewMaterials.HideSelection = false;
         this.listViewMaterials.Location = new System.Drawing.Point(0, 0);
         this.listViewMaterials.Name = "listViewMaterials";
         this.listViewMaterials.Size = new System.Drawing.Size(322, 188);
         this.listViewMaterials.TabIndex = 2;
         this.listViewMaterials.View = System.Windows.Forms.View.Details;
         this.listViewMaterials.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewMaterials_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Name";
         this.columnHeaderName.Width = 134;
         // 
         // columnHeaderUserDefined
         // 
         this.columnHeaderUserDefined.Text = "User Def.";
         // 
         // columnHeaderType
         // 
         this.columnHeaderType.Text = "Type";
         this.columnHeaderType.Width = 97;
         // 
         // comboBoxUserDef
         // 
         this.comboBoxUserDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxUserDef.Location = new System.Drawing.Point(140, 192);
         this.comboBoxUserDef.Name = "comboBoxUserDef";
         this.comboBoxUserDef.Size = new System.Drawing.Size(121, 21);
         this.comboBoxUserDef.TabIndex = 7;
         this.comboBoxUserDef.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserDef_SelectedIndexChanged);
         // 
         // labelFilterByUserDef
         // 
         this.labelFilterByUserDef.Location = new System.Drawing.Point(4, 192);
         this.labelFilterByUserDef.Name = "labelFilterByUserDef";
         this.labelFilterByUserDef.Size = new System.Drawing.Size(132, 20);
         this.labelFilterByUserDef.TabIndex = 8;
         this.labelFilterByUserDef.Text = "Filter by User Defined:";
         this.labelFilterByUserDef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // DryingMaterialsControl
         // 
         this.Controls.Add(this.labelFilterByUserDef);
         this.Controls.Add(this.comboBoxUserDef);
         this.Controls.Add(this.listViewMaterials);
         this.Controls.Add(this.labelFilterByType);
         this.Controls.Add(this.comboBoxTypes);
         this.Name = "DryingMaterialsControl";
         this.Size = new System.Drawing.Size(324, 240);
         this.ResumeLayout(false);

      }
		#endregion

      private void PopulateDryingMaterialList()
      {
         this.listViewMaterials.Items.Clear();
         string typeFilterStr = this.comboBoxTypes.SelectedItem.ToString();
         string userDefFilterStr = this.comboBoxUserDef.SelectedItem.ToString();

         if (typeFilterStr.Equals(UI.STR_ALL) && userDefFilterStr.Equals(UI.STR_ALL))
         {
            this.PopulateIt(DryingMaterialCatalog.GetInstance().GetDryingMaterialList());
         }
         else if (typeFilterStr.Equals(UI.STR_ALL) && !userDefFilterStr.Equals(UI.STR_ALL))
         {
            bool userDef = UI.GetUserDefinedAsBool(userDefFilterStr);
            this.PopulateIt(DryingMaterialCatalog.GetInstance().GetDryingMaterialList(userDef));
         }
         else if (!typeFilterStr.Equals(UI.STR_ALL) && userDefFilterStr.Equals(UI.STR_ALL))
         {
            MaterialType type = DryingMaterialsControl.GetDryingMaterialTypeAsEnum(typeFilterStr);
            this.PopulateIt(DryingMaterialCatalog.GetInstance().GetDryingMaterialList(type));
         }
         else
         {
            bool userDef = UI.GetUserDefinedAsBool(userDefFilterStr);
            MaterialType type = DryingMaterialsControl.GetDryingMaterialTypeAsEnum(typeFilterStr);
            this.PopulateIt(DryingMaterialCatalog.GetInstance().GetDryingMaterialList(userDef, type));
         }
      }

      private void PopulateIt(IList list)
      {
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            DryingMaterial dm = (DryingMaterial)en.Current;
            ListViewItem lvi = new ListViewItem();
                    
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, dm.Name);
            lvi.SubItems.Insert(0, lvsi);

            string userDefStr = UI.GetBoolAsYesNo(dm.IsUserDefined);
            lvsi = new ListViewItem.ListViewSubItem(lvi, userDefStr); 
            lvi.SubItems.Insert(1, lvsi);

            string typeStr = DryingMaterialsControl.GetDryingMaterialTypeAsString(dm.MaterialType);
            lvsi = new ListViewItem.ListViewSubItem(lvi, typeStr);
            lvi.SubItems.Insert(2, lvsi);

            this.listViewMaterials.Items.Add(lvi);
         }
      }

      private void comboBoxTypes_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            this.PopulateDryingMaterialList();
         }
      }

      private void comboBoxUserDef_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            this.PopulateDryingMaterialList();
         }
      }

      private void listViewMaterials_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewMaterials.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewMaterials.Sort();
      }

      public DryingMaterial GetSelectedDryingMaterial()
      {
         DryingMaterial dm = null;
         if (this.listViewMaterials.SelectedItems.Count == 1)
         {
            ListViewItem lvi = (ListViewItem)this.listViewMaterials.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            dm = DryingMaterialCatalog.GetInstance().GetDryingMaterial(lvsi.Text);
         }
         return dm;
      }

      private void DryingMaterialsControl_DryingMaterialAdded(DryingMaterial material)
      {
         this.PopulateDryingMaterialList();
      }

      private void DryingMaterialsControl_DryingMaterialChanged(object sender, DryingMaterialChangedEventArgs eventArgs)
      {
         this.PopulateDryingMaterialList();
      }

      private void DryingMaterialsControl_DryingMaterialDeleted(string name)
      {
         this.PopulateDryingMaterialList();
      }

      public static MaterialType GetDryingMaterialTypeAsEnum(string typeStr)
      {
         MaterialType typeEnum = MaterialType.GenericMaterial;
         if (typeStr.Equals(DryingMaterialsControl.STR_MAT_TYPE_GENERIC_FOOD))
            typeEnum = MaterialType.GenericFood;
         else if (typeStr.Equals(DryingMaterialsControl.STR_MAT_TYPE_GENERIC_MATERIAL))
            typeEnum = MaterialType.GenericMaterial;
         else if (typeStr.Equals(DryingMaterialsControl.STR_MAT_TYPE_SPECIAL_FOOD))
            typeEnum = MaterialType.SpecialFood;
         return typeEnum;
      }

      public static string GetDryingMaterialTypeAsString(MaterialType typeEnum)
      {
         string typeStr = "";
         if (typeEnum == MaterialType.GenericFood)
            typeStr = DryingMaterialsControl.STR_MAT_TYPE_GENERIC_FOOD;
         else if (typeEnum == MaterialType.GenericMaterial)
            typeStr = DryingMaterialsControl.STR_MAT_TYPE_GENERIC_MATERIAL;
         else if (typeEnum == MaterialType.SpecialFood)
            typeStr = DryingMaterialsControl.STR_MAT_TYPE_SPECIAL_FOOD;
         return typeStr;
      }

      public void SelectDryingMaterial(string dryingMaterialName)
      {
         if (dryingMaterialName == null || dryingMaterialName.Trim().Equals(""))
         {
            ((ListViewItem)this.listViewMaterials.Items[0]).Selected = true;
         }
         else
         {
            bool foundIt = false;
            for (int i=0; i<this.listViewMaterials.Items.Count; i++)
            {
               ListViewItem lvi = (ListViewItem)this.listViewMaterials.Items[i];
               ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
               if (dryingMaterialName.Equals(lvsi.Text))
               {
                  lvi.Selected = true;
                  foundIt = true;
                  break;
               }
            }
            if (!foundIt)
            {
               ((ListViewItem)this.listViewMaterials.Items[0]).Selected = true;
            }
         }
      }

      public void DeleteSelectedElements()
      {
         ArrayList toDeleteList = new ArrayList();

         IEnumerator en = this.listViewMaterials.SelectedItems.GetEnumerator();
         while (en.MoveNext())
         {
            ListViewItem lvi = (ListViewItem)en.Current;
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            toDeleteList.Add(lvsi.Text);
         }

         IEnumerator en2 = toDeleteList.GetEnumerator();
         while (en2.MoveNext())
         {
            string name = (string)en2.Current;
            DryingMaterialCatalog.GetInstance().RemoveDryingMaterial(name);
         }
      }
   }
}
