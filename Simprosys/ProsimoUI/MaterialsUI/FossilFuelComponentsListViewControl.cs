using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;
using Prosimo.ThermalProperties;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for SubstancesControl.
	/// </summary>
	public class FossilFuelComponentsListViewControl : System.Windows.Forms.UserControl
	{
      //private bool inConstruction;

      public ListView ListViewSubstances
      {
         get {return this.listViewComponents;}
      }

      private System.Windows.Forms.ListView listViewComponents;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderMassFraction;

      //private IList materialComponents;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FossilFuelComponentsListViewControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      //public FossilFuelComponentsListViewControl(IList matComponents)
      //{
      //   // This call is required by the Windows.Forms Form Designer.
      //   InitializeComponent();
      //   //this.materialComponents = matComponents;
      //   Populate(matComponents);
      //}

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
         this.listViewComponents = new System.Windows.Forms.ListView();
         this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeaderMassFraction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // listViewSubstances
         // 
         this.listViewComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderMassFraction});
         this.listViewComponents.FullRowSelect = true;
         this.listViewComponents.HideSelection = false;
         this.listViewComponents.Location = new System.Drawing.Point(0, 0);
         this.listViewComponents.Name = "listViewMaterialComponents";
         this.listViewComponents.Size = new System.Drawing.Size(300, 432);
         this.listViewComponents.TabIndex = 4;
         this.listViewComponents.UseCompatibleStateImageBehavior = false;
         this.listViewComponents.View = System.Windows.Forms.View.Details;
         this.listViewComponents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSubstances_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Component Name";
         this.columnHeaderName.Width = 170;
         // 
         // columnHeaderFormula
         // 
         this.columnHeaderMassFraction.Text = "Mass Fraction";
         this.columnHeaderMassFraction.Width = 116;
         // 
         // SubstanceListControl
         // 
         this.Controls.Add(this.listViewComponents);
         this.Name = "MaterialComponentsControl";
         this.Size = new System.Drawing.Size(303, 435);
         this.ResumeLayout(false);

      }
		#endregion

      internal void SetMaterialcomponents(IList componentList)
      {
         Populate(componentList);
      }

      private void Populate(IList list)
      {
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            MaterialComponent mc = (MaterialComponent)en.Current;
            ListViewItem lvi = new ListViewItem();
                    
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, mc.Name);
            lvi.SubItems.Insert(0, lvsi);

            lvsi = new ListViewItem.ListViewSubItem(lvi, mc.MassFraction.Value.ToString("0.000"));
            lvi.SubItems.Insert(1, lvsi);

            this.listViewComponents.Items.Add(lvi);
         }
      }

      private void listViewSubstances_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewComponents.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewComponents.Sort();
      }
   }
}
