using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for SubstancesControl.
	/// </summary>
	public class SubstanceListControl : System.Windows.Forms.UserControl
	{
      //public const string STR_SUBSTANCE_TYPE_ORGANIC = "Organic";
      //public const string STR_SUBSTANCE_TYPE_INORGANIC = "Inorganic";
      
      //private bool inConstruction;

      //private SubstancesToShow substancesToShow;
      //public SubstancesToShow SubstancesToShow
      //{
      //   get {return substancesToShow;}
      //   set
      //   {
      //      substancesToShow = value;
      //      this.PopulateSubstanceList();
      //   }
      //}

      public ListView ListViewSubstances
      {
         get {return this.listViewSubstances;}
      }

      private System.Windows.Forms.ListView listViewSubstances;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderFormula;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SubstanceListControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         //this.inConstruction = true;
         //this.PopulateSubstanceList();
         //this.inConstruction = false;
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
         this.listViewSubstances = new System.Windows.Forms.ListView();
         this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeaderFormula = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // listViewSubstances
         // 
         this.listViewSubstances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderFormula});
         this.listViewSubstances.FullRowSelect = true;
         this.listViewSubstances.HideSelection = false;
         this.listViewSubstances.Location = new System.Drawing.Point(0, 0);
         this.listViewSubstances.Name = "listViewSubstances";
         this.listViewSubstances.Size = new System.Drawing.Size(300, 432);
         this.listViewSubstances.TabIndex = 4;
         this.listViewSubstances.UseCompatibleStateImageBehavior = false;
         this.listViewSubstances.View = System.Windows.Forms.View.Details;
         this.listViewSubstances.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSubstances_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Component Name";
         this.columnHeaderName.Width = 170;
         // 
         // columnHeaderFormula
         // 
         this.columnHeaderFormula.Text = "Formula";
         this.columnHeaderFormula.Width = 116;
         // 
         // SubstanceListControl
         // 
         this.Controls.Add(this.listViewSubstances);
         this.Name = "SubstanceListControl";
         this.Size = new System.Drawing.Size(303, 435);
         this.ResumeLayout(false);

      }
		#endregion

      internal void PopulateSubstanceList()
      {
         this.listViewSubstances.Items.Clear();
         IList list = GetFuelSubstanceList();
         this.PopulateIt(list);
      }

      private IList GetFuelSubstanceList()
      {
         SubstanceCatalog substanceCatalog = SubstanceCatalog.GetInstance();
         IList naturalGasSubstancelist = substanceCatalog.GetNaturalGasSubstanceList();
         IList list = new ArrayList(naturalGasSubstancelist);

         IList allSubstanceList = substanceCatalog.GetSubstanceList();
         IList perrySubstanceList = ThermalPropCalculator.Instance.GetPerrySubstanceNameList();
         foreach (Substance s in allSubstanceList)
         {
            if (perrySubstanceList.Contains(s.Name))
            {
               if (s.Formula.Elements.Length == 2 && s.Formula.Elements[0] == "C" && s.Formula.Elements[1] == "H" && !list.Contains(s))
               {
                  list.Add(s);
               }
            }
         }

         list.Add(substanceCatalog.GetSubstance(Substance.ETHANOL));

         return list;
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

            lvsi = new ListViewItem.ListViewSubItem(lvi, subst.FormulaString);
            lvi.SubItems.Insert(1, lvsi);

            this.listViewSubstances.Items.Add(lvi);
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
            subst = SubstanceCatalog.GetInstance().GetSubstance(lvsi.Text);
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
            Substance subst = SubstanceCatalog.GetInstance().GetSubstance(lvsi.Text);
            list.Add(subst);            
         }
         return list;
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
