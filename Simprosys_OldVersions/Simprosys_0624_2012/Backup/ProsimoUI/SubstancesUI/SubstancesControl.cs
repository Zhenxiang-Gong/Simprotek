using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace ProsimoUI.SubstancesUI
{
	/// <summary>
	/// Summary description for SubstancesControl.
	/// </summary>
	public class SubstancesControl : System.Windows.Forms.UserControl
	{
      public const string STR_SUBSTANCE_TYPE_ORGANIC = "Organic";
      public const string STR_SUBSTANCE_TYPE_INORGANIC = "Inorganic";
      
      private bool inConstruction;

      private SubstancesToShow substancesToShow;
      public SubstancesToShow SubstancesToShow
      {
         get {return substancesToShow;}
         set
         {
            substancesToShow = value;
            this.PopulateSubstanceList();
         }
      }

      public ListView ListViewSubstances
      {
         get {return this.listViewSubstances;}
      }

      private System.Windows.Forms.Label labelFilterByType;
      private System.Windows.Forms.ComboBox comboBoxTypes;
      private System.Windows.Forms.ListView listViewSubstances;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderFormula;
      private System.Windows.Forms.ColumnHeader columnHeaderType;
      private System.Windows.Forms.Label labelFilterByUserDef;
      private System.Windows.Forms.ComboBox comboBoxUserDef;
      private System.Windows.Forms.ColumnHeader columnHeaderUserDef;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SubstancesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.inConstruction = true;

         this.comboBoxUserDef.Items.Add(UI.STR_ALL);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_USER_DEF);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_NOT_USER_DEF);
         this.comboBoxUserDef.SelectedIndex = 0;

         this.comboBoxTypes.Items.Add(UI.STR_ALL);
         this.comboBoxTypes.Items.Add(SubstancesControl.STR_SUBSTANCE_TYPE_INORGANIC);
         this.comboBoxTypes.Items.Add(SubstancesControl.STR_SUBSTANCE_TYPE_ORGANIC);
         this.comboBoxTypes.SelectedIndex = 0;

         this.substancesToShow = SubstancesToShow.All;
         this.PopulateSubstanceList();

         SubstanceCatalog.GetInstance().SubstanceAdded += new SubstanceAddedEventHandler(SubstancesControl_SubstanceAdded);
         SubstanceCatalog.GetInstance().SubstanceChanged += new SubstanceChangedEventHandler(SubstancesControl_SubstanceChanged);
         SubstanceCatalog.GetInstance().SubstanceDeleted += new SubstanceDeletedEventHandler(SubstancesControl_SubstanceDeleted);

         this.inConstruction = false;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         SubstanceCatalog.GetInstance().SubstanceAdded -= new SubstanceAddedEventHandler(SubstancesControl_SubstanceAdded);
         SubstanceCatalog.GetInstance().SubstanceChanged -= new SubstanceChangedEventHandler(SubstancesControl_SubstanceChanged);
         SubstanceCatalog.GetInstance().SubstanceDeleted -= new SubstanceDeletedEventHandler(SubstancesControl_SubstanceDeleted);
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
         this.labelFilterByType = new System.Windows.Forms.Label();
         this.comboBoxTypes = new System.Windows.Forms.ComboBox();
         this.listViewSubstances = new System.Windows.Forms.ListView();
         this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderUserDef = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderFormula = new System.Windows.Forms.ColumnHeader();
         this.labelFilterByUserDef = new System.Windows.Forms.Label();
         this.comboBoxUserDef = new System.Windows.Forms.ComboBox();
         this.SuspendLayout();
         // 
         // labelFilterByType
         // 
         this.labelFilterByType.Location = new System.Drawing.Point(4, 204);
         this.labelFilterByType.Name = "labelFilterByType";
         this.labelFilterByType.Size = new System.Drawing.Size(132, 20);
         this.labelFilterByType.TabIndex = 3;
         this.labelFilterByType.Text = "Filter by Substance Type:";
         this.labelFilterByType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxTypes
         // 
         this.comboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTypes.Location = new System.Drawing.Point(140, 204);
         this.comboBoxTypes.Name = "comboBoxTypes";
         this.comboBoxTypes.Size = new System.Drawing.Size(121, 21);
         this.comboBoxTypes.TabIndex = 2;
         this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
         // 
         // listViewSubstances
         // 
         this.listViewSubstances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                             this.columnHeaderName,
                                                                                             this.columnHeaderUserDef,
                                                                                             this.columnHeaderType,
                                                                                             this.columnHeaderFormula});
         this.listViewSubstances.FullRowSelect = true;
         this.listViewSubstances.HideSelection = false;
         this.listViewSubstances.Location = new System.Drawing.Point(0, 0);
         this.listViewSubstances.Name = "listViewSubstances";
         this.listViewSubstances.Size = new System.Drawing.Size(344, 176);
         this.listViewSubstances.TabIndex = 4;
         this.listViewSubstances.View = System.Windows.Forms.View.Details;
         this.listViewSubstances.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSubstances_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Name";
         this.columnHeaderName.Width = 91;
         // 
         // columnHeaderUserDef
         // 
         this.columnHeaderUserDef.Text = "User Def.";
         // 
         // columnHeaderType
         // 
         this.columnHeaderType.Text = "Type";
         this.columnHeaderType.Width = 78;
         // 
         // columnHeaderFormula
         // 
         this.columnHeaderFormula.Text = "Formula";
         this.columnHeaderFormula.Width = 92;
         // 
         // labelFilterByUserDef
         // 
         this.labelFilterByUserDef.Location = new System.Drawing.Point(4, 180);
         this.labelFilterByUserDef.Name = "labelFilterByUserDef";
         this.labelFilterByUserDef.Size = new System.Drawing.Size(132, 20);
         this.labelFilterByUserDef.TabIndex = 10;
         this.labelFilterByUserDef.Text = "Filter by User Defined:";
         this.labelFilterByUserDef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxUserDef
         // 
         this.comboBoxUserDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxUserDef.Location = new System.Drawing.Point(140, 180);
         this.comboBoxUserDef.Name = "comboBoxUserDef";
         this.comboBoxUserDef.Size = new System.Drawing.Size(121, 21);
         this.comboBoxUserDef.TabIndex = 9;
         this.comboBoxUserDef.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserDef_SelectedIndexChanged);
         // 
         // SubstancesControl
         // 
         this.Controls.Add(this.labelFilterByUserDef);
         this.Controls.Add(this.comboBoxUserDef);
         this.Controls.Add(this.listViewSubstances);
         this.Controls.Add(this.labelFilterByType);
         this.Controls.Add(this.comboBoxTypes);
         this.Name = "SubstancesControl";
         this.Size = new System.Drawing.Size(344, 228);
         this.ResumeLayout(false);

      }
		#endregion

      private void PopulateSubstanceList()
      {
         this.listViewSubstances.Items.Clear();
         string typeFilterStr = this.comboBoxTypes.SelectedItem.ToString();
         string userDefFilterStr = this.comboBoxUserDef.SelectedItem.ToString();

         IList list = null;
         if (typeFilterStr.Equals(UI.STR_ALL) && userDefFilterStr.Equals(UI.STR_ALL))
         {
            if (this.substancesToShow == SubstancesToShow.All)
               //list = SubstanceCatalog.GetInstance().GetSubstanceList();
               list = GetMaterialSubstanceList();
            else if (this.substancesToShow == SubstancesToShow.Material)
               //list = SubstanceCatalog.GetInstance().GetMaterialSubstanceList();
               list = GetMaterialSubstanceList();
            //else if (this.substancesToShow == SubstancesToShow.Gas)
            //   list = SubstanceCatalog.GetInstance().GetGasSubstanceList();
            //else if (this.substancesToShow == SubstancesToShow.Moisture)
            //   list = SubstanceCatalog.GetInstance().GetMoistureSubstanceList();
            this.PopulateIt(list);
         }
         else if (typeFilterStr.Equals(UI.STR_ALL) && !userDefFilterStr.Equals(UI.STR_ALL))
         {
            bool userDef = UI.GetUserDefinedAsBool(userDefFilterStr);
            if (this.substancesToShow == SubstancesToShow.All)
               //list = SubstanceCatalog.GetInstance().GetSubstanceList(userDef);
               list = GetMaterialSubstanceList();
            else if (this.substancesToShow == SubstancesToShow.Material)
               //list = SubstanceCatalog.GetInstance().GetMaterialSubstanceList(userDef);
               list = GetMaterialSubstanceList();
            //else if (this.substancesToShow == SubstancesToShow.Gas)
            //   list = SubstanceCatalog.GetInstance().GetGasSubstanceList(userDef);
            //else if (this.substancesToShow == SubstancesToShow.Moisture)
            //   list = SubstanceCatalog.GetInstance().GetMoistureSubstanceList(userDef);
            this.PopulateIt(list);
         }
         else if (!typeFilterStr.Equals(UI.STR_ALL) && userDefFilterStr.Equals(UI.STR_ALL))
         {
            SubstanceType type = SubstancesControl.GetSubstanceTypeAsEnum(typeFilterStr);
            if (this.substancesToShow == SubstancesToShow.All)
               //list = SubstanceCatalog.GetInstance().GetSubstanceList(type);
               list = GetMaterialSubstanceList();
            else if (this.substancesToShow == SubstancesToShow.Material)
               list = GetMaterialSubstanceList();
               //list = SubstanceCatalog.GetInstance().GetMaterialSubstanceList(type);
            //else if (this.substancesToShow == SubstancesToShow.Gas)
            //   list = SubstanceCatalog.GetInstance().GetGasSubstanceList(type);
            //else if (this.substancesToShow == SubstancesToShow.Moisture)
            //   list = SubstanceCatalog.GetInstance().GetMoistureSubstanceList(type);
            this.PopulateIt(list);
         }
         else
         {
            bool userDef = UI.GetUserDefinedAsBool(userDefFilterStr);
            SubstanceType type = SubstancesControl.GetSubstanceTypeAsEnum(typeFilterStr);
            if (this.substancesToShow == SubstancesToShow.All)
               //list = SubstanceCatalog.GetInstance().GetSubstanceList(userDef, type);
               list = GetMaterialSubstanceList();
            else if (this.substancesToShow == SubstancesToShow.Material)
            //   list = SubstanceCatalog.GetInstance().GetMaterialSubstanceList(userDef, type);
            //else if (this.substancesToShow == SubstancesToShow.Gas)
            //   list = SubstanceCatalog.GetInstance().GetGasSubstanceList(userDef, type);
            //else if (this.substancesToShow == SubstancesToShow.Moisture)
            //   list = SubstanceCatalog.GetInstance().GetMoistureSubstanceList(userDef, type);
            this.PopulateIt(list);
         }
      }

      private void PopulateIt(IList list)
      {
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            Substance subst = (Substance)en.Current;
            ListViewItem lvi = new ListViewItem();
                    
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, subst.Name);
            lvi.SubItems.Insert(0, lvsi);

            string userDefStr = UI.GetBoolAsYesNo(subst.IsUserDefined);
            lvsi = new ListViewItem.ListViewSubItem(lvi, userDefStr); 
            lvi.SubItems.Insert(1, lvsi);

            string typeStr = SubstancesControl.GetSubstanceTypeAsString(subst.SubstanceType);
            lvsi = new ListViewItem.ListViewSubItem(lvi, typeStr);
            lvi.SubItems.Insert(2, lvsi);

            lvsi = new ListViewItem.ListViewSubItem(lvi, subst.FormulaString);
            lvi.SubItems.Insert(3, lvsi);

            this.listViewSubstances.Items.Add(lvi);
         }
      }

      private IList GetMaterialSubstanceList() {
         IList materialSubstanceList = new ArrayList();
         IList allSubstanceList = SubstanceCatalog.GetInstance().GetSubstanceList();
         ICollection<string> solidList = ThermalPropCalculator.Instance.GetYawsSolidCpSubstanceNameList();
         foreach (Substance s in allSubstanceList) {
            if (solidList.Contains(s.Name)) {
               materialSubstanceList.Add(s);
            }
         }
         return materialSubstanceList;
      }

      private void comboBoxTypes_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            this.PopulateSubstanceList();
         }
      }

      private void comboBoxUserDef_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            this.PopulateSubstanceList();
         }
      }

      private void listViewSubstances_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewSubstances.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewSubstances.Sort();
      }

      public Substance GetSelectedSubstance()
      {
         Substance subst = null;
         if (this.listViewSubstances.SelectedItems.Count > 0)
         {
            ListViewItem lvi = (ListViewItem)this.listViewSubstances.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            subst = SubstanceCatalog.GetSubstance(lvsi.Text);
         }
         return subst;
      }

      public ArrayList GetSelectedSubstances()
      {
         ArrayList list = new ArrayList();
         IEnumerator en = this.listViewSubstances.SelectedItems.GetEnumerator();
         while (en.MoveNext())
         {
            ListViewItem lvi = (ListViewItem)en.Current;
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            Substance subst = SubstanceCatalog.GetSubstance(lvsi.Text);
            list.Add(subst);            
         }
         return list;
      }

      private void SubstancesControl_SubstanceAdded(Substance substance)
      {
         this.PopulateSubstanceList();
      }

      private void SubstancesControl_SubstanceChanged(Substance substance)
      {
         this.PopulateSubstanceList();
      }

      private void SubstancesControl_SubstanceDeleted(string name)
      {
         this.PopulateSubstanceList();
      }

      public static SubstanceType GetSubstanceTypeAsEnum(string typeStr)
      {
         SubstanceType typeEnum = SubstanceType.Unknown;
         if (typeStr.Equals(SubstancesControl.STR_SUBSTANCE_TYPE_ORGANIC))
            typeEnum = SubstanceType.Organic;
         else if (typeStr.Equals(SubstancesControl.STR_SUBSTANCE_TYPE_INORGANIC))
            typeEnum = SubstanceType.Inorganic;
         return typeEnum;
      }

      public static string GetSubstanceTypeAsString(SubstanceType typeEnum)
      {
         string typeStr = "";
         if (typeEnum == SubstanceType.Organic)
            typeStr = SubstancesControl.STR_SUBSTANCE_TYPE_ORGANIC;
         else if (typeEnum == SubstanceType.Inorganic)
            typeStr = SubstancesControl.STR_SUBSTANCE_TYPE_INORGANIC;
         return typeStr;
      }

      public void SelectSubstance(string substanceName)
      {
         if (substanceName == null || substanceName.Trim().Equals(""))
         {
            ((ListViewItem)this.listViewSubstances.Items[0]).Selected = true;
         }
         else
         {
            bool foundIt = false;
            for (int i=0; i<this.listViewSubstances.Items.Count; i++)
            {
               ListViewItem lvi = (ListViewItem)this.listViewSubstances.Items[i];
               ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
               if (substanceName.Equals(lvsi.Text))
               {
                  lvi.Selected = true;
                  foundIt = true;
                  break;
               }
            }
            if (!foundIt)
            {
               ((ListViewItem)this.listViewSubstances.Items[0]).Selected = true;
            }
         }
      }
   }
}
