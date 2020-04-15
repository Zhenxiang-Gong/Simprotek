using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Plots;
using Prosimo;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for PlotsControl.
	/// </summary>
	public class PlotsControl : System.Windows.Forms.UserControl
	{
      private PlotCatalog plotCatalog;

      public ListView ListViewPlots
      {
         get {return this.listViewPlots;}
      }

      private System.Windows.Forms.ListView listViewPlots;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderIsValid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public PlotsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.plotCatalog != null)
         {
            this.plotCatalog.Plot2DAdded -= new Plot2DAddedEventHandler(plotCatalog_Plot2DAdded);
            this.plotCatalog.Plot2DDeleted -= new Plot2DDeletedEventHandler(plotCatalog_Plot2DDeleted);
            this.plotCatalog.Plot2DChanged -= new Plot2DChangedEventHandler(plotCatalog_Plot2DChanged);
         }

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
         this.listViewPlots = new System.Windows.Forms.ListView();
         this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderIsValid = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // listViewPlots
         // 
         this.listViewPlots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                        this.columnHeaderName,
                                                                                        this.columnHeaderIsValid});
         this.listViewPlots.FullRowSelect = true;
         this.listViewPlots.HideSelection = false;
         this.listViewPlots.Location = new System.Drawing.Point(0, 0);
         this.listViewPlots.Name = "listViewPlots";
         this.listViewPlots.Size = new System.Drawing.Size(216, 176);
         this.listViewPlots.TabIndex = 3;
         this.listViewPlots.View = System.Windows.Forms.View.Details;
         this.listViewPlots.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPlots_ColumnClick);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Name";
         this.columnHeaderName.Width = 134;
         // 
         // columnHeaderIsValid
         // 
         this.columnHeaderIsValid.Text = "Is Valid";
         // 
         // PlotsControl
         // 
         this.Controls.Add(this.listViewPlots);
         this.Name = "PlotsControl";
         this.Size = new System.Drawing.Size(216, 176);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeTheControl(PlotCatalog plotCatalog)
      {
         this.plotCatalog = plotCatalog;
         this.PopulatePlotList();

         this.plotCatalog.Plot2DAdded += new Plot2DAddedEventHandler(plotCatalog_Plot2DAdded);
         this.plotCatalog.Plot2DDeleted += new Plot2DDeletedEventHandler(plotCatalog_Plot2DDeleted);
         this.plotCatalog.Plot2DChanged += new Plot2DChangedEventHandler(plotCatalog_Plot2DChanged);
      }

      private void PopulatePlotList()
      {
         if (this.plotCatalog != null)
         {
            this.listViewPlots.Items.Clear();
            IList list = plotCatalog.Plot2DList;
            IEnumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
               Plot2D p2d = (Plot2D)en.Current;
               ListViewItem lvi = new ListViewItem();
                    
               ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, p2d.Name);
               lvi.SubItems.Insert(0, lvsi);

               lvsi = new ListViewItem.ListViewSubItem(lvi, UI.GetBoolAsYesNo(p2d.IsValid));
               lvi.SubItems.Insert(1, lvsi);

               this.listViewPlots.Items.Add(lvi);
            }
         }
      }

      public Plot2D GetSelectedPlot()
      {
         Plot2D p2d = null;
         if (this.listViewPlots.SelectedItems.Count == 1)
         {
            ListViewItem lvi = (ListViewItem)this.listViewPlots.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            p2d = this.plotCatalog.GetPlot2D(lvsi.Text);
         }
         return p2d;
      }

      public ArrayList GetValidSelectedPlots()
      {
         ArrayList list = new ArrayList();
         IEnumerator en = this.listViewPlots.SelectedItems.GetEnumerator();
         while (en.MoveNext())
         {
            ListViewItem lvi = (ListViewItem)en.Current;
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            Plot2D p2d = this.plotCatalog.GetPlot2D(lvsi.Text);
            if (p2d.IsValid)
               list.Add(p2d);
         }
         return list;
      }

      public void DeleteSelectedElements()
      {
         ArrayList toDeleteList = new ArrayList();

         IEnumerator en = this.ListViewPlots.SelectedItems.GetEnumerator();
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
            this.plotCatalog.RemovePlot2D(name);
         }
      }

      private void listViewPlots_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
      {
         this.listViewPlots.ListViewItemSorter = new ListViewItemComparer(e.Column);
         this.listViewPlots.Sort();
      }

      private void plotCatalog_Plot2DAdded(Plot2D plot2D)
      {
         this.PopulatePlotList();
      }

      private void plotCatalog_Plot2DDeleted(string name)
      {
         this.PopulatePlotList();
      }

      private void plotCatalog_Plot2DChanged(Plot2D plot2D)
      {
         this.PopulatePlotList();
      }
   }
}
