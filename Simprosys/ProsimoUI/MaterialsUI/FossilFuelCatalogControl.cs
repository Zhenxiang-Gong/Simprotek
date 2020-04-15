using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI {
   /// <summary>
   /// Summary description for FuelCatalogControl.
   /// </summary>
   public class FossilFuelCatalogControl : System.Windows.Forms.UserControl {
      public const string STR_FUEL_TYPE_GENERIC = "Generic";
      public const string STR_FUEL_TYPE_DETAILED = "Detailed";

      private bool _InConstruction;

      public ListView ListViewFossilFuelList {
         get { return _ListViewFossilFuelList; }
      }

      //private ComboBox _ComboBoxTypes;
      //private System.Windows.Forms.Label labelFilterByType;
      private ListView _ListViewFossilFuelList;
      private ColumnHeader _ColumnHeaderName;
      private ColumnHeader _ColumnHeaderType;

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private Container _Components = null;

      public FossilFuelCatalogControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         //Initialization();
      }

      internal void Initialization()
      {
         _InConstruction = true;

         PopulateFossilFuelList();
         FossilFuelCatalog.Instance.FossilFuelAdded += new FossilFuelAddedEventHandler(OnFossilFuelAdded);
         FossilFuelCatalog.Instance.FossilFuelChanged += new FossilFuelChangedEventHandler(OnFossilFuelChanged);
         FossilFuelCatalog.Instance.FossilFuelDeleted += new FossilFuelDeletedEventHandler(OnFossilFuelDeleted);

         this._InConstruction = false;
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         FossilFuelCatalog.Instance.FossilFuelAdded -= new FossilFuelAddedEventHandler(OnFossilFuelAdded);
         FossilFuelCatalog.Instance.FossilFuelChanged -= new FossilFuelChangedEventHandler(OnFossilFuelChanged);
         FossilFuelCatalog.Instance.FossilFuelDeleted -= new FossilFuelDeletedEventHandler(OnFossilFuelDeleted);
         if(disposing)
         {
            if (_Components != null) {
               _Components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this._ListViewFossilFuelList = new System.Windows.Forms.ListView();
         this._ColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this._ColumnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // _ListViewFossilFuelList
         // 
         this._ListViewFossilFuelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this._ListViewFossilFuelList.AutoArrange = false;
         this._ListViewFossilFuelList.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this._ListViewFossilFuelList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._ColumnHeaderName,
            this._ColumnHeaderType});
         this._ListViewFossilFuelList.FullRowSelect = true;
         this._ListViewFossilFuelList.HideSelection = false;
         this._ListViewFossilFuelList.Location = new System.Drawing.Point(0, 0);
         this._ListViewFossilFuelList.Name = "_ListViewFossilFuelList";
         this._ListViewFossilFuelList.Size = new System.Drawing.Size(368, 375);
         this._ListViewFossilFuelList.TabIndex = 2;
         this._ListViewFossilFuelList.UseCompatibleStateImageBehavior = false;
         this._ListViewFossilFuelList.View = System.Windows.Forms.View.Details;
         this._ListViewFossilFuelList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnListViewFossilFuel_ColumnClick);
         // 
         // _ColumnHeaderName
         // 
         this._ColumnHeaderName.Text = "Name";
         this._ColumnHeaderName.Width = 260;
         // 
         // _ColumnHeaderType
         // 
         this._ColumnHeaderType.Text = "Type";
         this._ColumnHeaderType.Width = 91;
         // 
         // FossilFuelCatalogControl
         // 
         this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.Controls.Add(this._ListViewFossilFuelList);
         this.Name = "FossilFuelCatalogControl";
         this.Size = new System.Drawing.Size(375, 378);
         this.ResumeLayout(false);

      }
      #endregion

      private void PopulateFossilFuelList() {
         _ListViewFossilFuelList.Items.Clear();
         PopulateIt(FossilFuelCatalog.Instance.GetFossilFuelList());
      }

      private void PopulateIt(IList list) {
         IEnumerator en = list.GetEnumerator();
         while(en.MoveNext())
         {
            FossilFuel ff = (FossilFuel)en.Current;
            ListViewItem lvi = new ListViewItem();

            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(lvi, ff.Name);
            lvi.SubItems.Insert(0, lvsi);

            //string userDefStr = UI.GetBoolAsYesNo(dm.IsUserDefined);
            //lvsi = new ListViewItem.ListViewSubItem(lvi, userDefStr);
            //lvi.SubItems.Insert(1, lvsi);

            string typeStr = ff.FossilFuelType.ToString();
            lvsi = new ListViewItem.ListViewSubItem(lvi, typeStr);
            lvi.SubItems.Insert(2, lvsi);

            _ListViewFossilFuelList.Items.Add(lvi);
         }
      }

      private void comboBoxTypes_SelectedIndexChanged(object sender, System.EventArgs e) {
         if(!_InConstruction)
         {
            PopulateFossilFuelList();
         }
      }

      private void comboBoxUserDef_SelectedIndexChanged(object sender, System.EventArgs e) {
         if(!_InConstruction)
         {
            PopulateFossilFuelList();
         }
      }

      private void OnListViewFossilFuel_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e) {
         _ListViewFossilFuelList.ListViewItemSorter = new ListViewItemComparer(e.Column);
         _ListViewFossilFuelList.Sort();
      }

      public FossilFuel GetSelectedFossilFuel()
      {
         FossilFuel ff = null;
         if (this._ListViewFossilFuelList.SelectedItems.Count == 1) {
            ListViewItem lvi = (ListViewItem)this._ListViewFossilFuelList.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            ff = FossilFuelCatalog.Instance.GetFossilFuel(lvsi.Text);
         }
         return ff;
      }

      private void OnFossilFuelAdded(FossilFuel fuel) {
         PopulateFossilFuelList();
      }

      private void OnFossilFuelChanged(object sender, FossilFuelChangedEventArgs eventArgs) {
         PopulateFossilFuelList();
      }

      private void OnFossilFuelDeleted(string name) {
         PopulateFossilFuelList();
      }

      public void SelectDryingMaterial(string dryingMaterialName) {
         if (dryingMaterialName == null || dryingMaterialName.Trim().Equals("")) {
            ((ListViewItem)this._ListViewFossilFuelList.Items[0]).Selected = true;
         }
         else if (this._ListViewFossilFuelList.Items.Count >0) {
            bool foundIt = false;
            for (int i = 0; i < this._ListViewFossilFuelList.Items.Count; i++) {
               ListViewItem lvi = (ListViewItem)this._ListViewFossilFuelList.Items[i];
               ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
               if (dryingMaterialName.Equals(lvsi.Text)) {
                  lvi.Selected = true;
                  foundIt = true;
                  break;
               }
            }
            if (!foundIt) {
               ((ListViewItem)this._ListViewFossilFuelList.Items[0]).Selected = true;
            }
         }
      }

      public void DeleteSelectedElements() {
         ArrayList toDeleteList = new ArrayList();

         IEnumerator en = this._ListViewFossilFuelList.SelectedItems.GetEnumerator();
         while (en.MoveNext()) {
            ListViewItem lvi = (ListViewItem)en.Current;
            ListViewItem.ListViewSubItem lvsi = (ListViewItem.ListViewSubItem)lvi.SubItems[0];
            toDeleteList.Add(lvsi.Text);
         }

         IEnumerator en2 = toDeleteList.GetEnumerator();
         while (en2.MoveNext()) {
            string name = (string)en2.Current;
            FossilFuelCatalog.Instance.RemoveFossilFuel(name);
         }
      }
   }
}
