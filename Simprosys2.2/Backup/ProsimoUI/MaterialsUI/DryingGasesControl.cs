using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DryingGasesControl.
	/// </summary>
	public class DryingGasesControl : System.Windows.Forms.UserControl
	{
      private bool inConstruction;

      public ListView ListViewGases
      {
         get {return this.listViewGases;}
      }
      
      private System.Windows.Forms.Label labelFilterByUserDef;
      private System.Windows.Forms.ComboBox comboBoxUserDef;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderUserDefined;
      private System.Windows.Forms.ListView listViewGases;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryingGasesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.inConstruction = true;

         this.comboBoxUserDef.Items.Add(UI.STR_ALL);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_USER_DEF);
         this.comboBoxUserDef.Items.Add(UI.STR_USER_DEF_NOT_USER_DEF);
         this.comboBoxUserDef.SelectedIndex = 0;

         this.PopulateDryingGasList();

         DryingGasCatalog.Instance.DryingGasAdded += new DryingGasAddedEventHandler(DryingGasesControl_DryingGasAdded);
         DryingGasCatalog.Instance.DryingGasChanged += new DryingGasChangedEventHandler(DryingGasesControl_DryingGasChanged);
         DryingGasCatalog.Instance.DryingGasDeleted += new DryingGasDeletedEventHandler(DryingGasesControl_DryingGasDeleted);

         this.inConstruction = false;
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         DryingGasCatalog.Instance.DryingGasAdded -= new DryingGasAddedEventHandler(DryingGasesControl_DryingGasAdded);
         DryingGasCatalog.Instance.DryingGasChanged -= new DryingGasChangedEventHandler(DryingGasesControl_DryingGasChanged);
         DryingGasCatalog.Instance.DryingGasDeleted -= new DryingGasDeletedEventHandler(DryingGasesControl_DryingGasDeleted);
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
         this.labelFilterByUserDef = new System.Windows.Forms.Label();
         this.comboBoxUserDef = new System.Windows.Forms.ComboBox();
         this.listViewGases = new System.Windows.Forms.ListView();
         this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderUserDefined = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // labelFilterByUserDef
         // 
         this.labelFilterByUserDef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.labelFilterByUserDef.Location = new System.Drawing.Point(1, 164);
         this.labelFilterByUserDef.Name = "labelFilterByUserDef";
         this.labelFilterByUserDef.Size = new System.Drawing.Size(120, 20);
         this.labelFilterByUserDef.TabIndex = 11;
         this.labelFilterByUserDef.Text = "Filter by User Defined:";
         this.labelFilterByUserDef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // comboBoxUserDef
         // 
         this.comboBoxUserDef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.comboBoxUserDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxUserDef.Location = new System.Drawing.Point(127, 163);
         this.comboBoxUserDef.Name = "comboBoxUserDef";
         this.comboBoxUserDef.Size = new System.Drawing.Size(161, 21);
         this.comboBoxUserDef.TabIndex = 10;
         this.comboBoxUserDef.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserDef_SelectedIndexChanged);
         // 
         // listViewGases
         // 
         this.listViewGases.AutoArrange = false;
         this.listViewGases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderUserDefined});
         this.listViewGases.FullRowSelect = true;
         this.listViewGases.HideSelection = false;
         this.listViewGases.Location = new System.Drawing.Point(0, 0);
         this.listViewGases.Name = "listViewGases";
         this.listViewGases.Size = new System.Drawing.Size(294, 148);
         this.listViewGases.TabIndex = 9;
         this.listViewGases.UseCompatibleStateImageBehavior = false;
         this.listViewGases.View = System.Windows.Forms.View.Details;
         this.listViewGases.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewGases_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Name";
         this.columnHeaderName.Width = 166;
         // 
         // columnHeaderUserDefined
         // 
         this.columnHeaderUserDefined.Text = "User Defined";
         this.columnHeaderUserDefined.Width = 118;
         // 
         // DryingGasesControl
         // 
         this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.Controls.Add(this.labelFilterByUserDef);
         this.Controls.Add(this.comboBoxUserDef);
         this.Controls.Add(this.listViewGases);
         this.Name = "DryingGasesControl";
         this.Size = new System.Drawing.Size(299, 200);
         this.ResumeLayout(false);

      }
		#endregion

      private void PopulateIt(IList gasList)
      {
         IEnumerator en = gasList.GetEnumerator();
         while (en.MoveNext())
         {
            DryingGas dg = (DryingGas)en.Current;
            ListViewItem lvi = new ListViewItem();
                    
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, dg.Name);
            lvi.SubItems.Insert(0, lvsi);

            string userDefStr = UI.GetBoolAsYesNo(dg.IsUserDefined);
            lvsi = new ListViewItem.ListViewSubItem(lvi, userDefStr); 
            lvi.SubItems.Insert(1, lvsi);

            this.listViewGases.Items.Add(lvi);
         }
      }

      private void PopulateDryingGasList()
      {
         this.listViewGases.Items.Clear();
         string userDefFilterStr = this.comboBoxUserDef.SelectedItem.ToString();

         if (userDefFilterStr.Equals(UI.STR_ALL))
         {
            this.PopulateIt(DryingGasCatalog.Instance.GetDryingGasList());
         }
         else
         {
            bool userDef = UI.GetUserDefinedAsBool(userDefFilterStr);
            this.PopulateIt(DryingGasCatalog.Instance.GetDryingGasList(userDef));
         }
      }

      private void comboBoxUserDef_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            this.PopulateDryingGasList();
         }
      }

      private void listViewGases_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewGases.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewGases.Sort();
      }

      public DryingGas GetSelectedDryingGas()
      {
         DryingGas dg = null;
         if (this.listViewGases.SelectedItems.Count > 0)
         {
            ListViewItem lvi = (ListViewItem)this.listViewGases.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            dg = DryingGasCatalog.Instance.GetDryingGas(lvsi.Text);
         }
         return dg;
      }

      private void DryingGasesControl_DryingGasAdded(DryingGas gas)
      {
         this.PopulateDryingGasList();
      }

      private void DryingGasesControl_DryingGasChanged(DryingGas gas)
      {
         this.PopulateDryingGasList();
      }

      private void DryingGasesControl_DryingGasDeleted(string name)
      {
         this.PopulateDryingGasList();
      }

      public void SelectDryingGas(string dryingGasName)
      {
         if (dryingGasName == null || dryingGasName.Trim().Equals(""))
         {
            ((ListViewItem)this.listViewGases.Items[0]).Selected = true;
         }
         else
         {
            bool foundIt = false;
            for (int i=0; i<this.listViewGases.Items.Count; i++)
            {
               ListViewItem lvi = (ListViewItem)this.listViewGases.Items[i];
               ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
               if (dryingGasName.Equals(lvsi.Text))
               {
                  lvi.Selected = true;
                  foundIt = true;
                  break;
               }
            }
            if (!foundIt)
            {
               ((ListViewItem)this.listViewGases.Items[0]).Selected = true;
            }
         }
      }
   }
}
