using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;

namespace ProsimoUI.UnitSystemsUI
{
   public delegate void SelectedUnitSystemChangedEventHandler(object sender);

	/// <summary>
	/// Summary description for UnitSystemsControl.
	/// </summary>
	public class UnitSystemsControl : System.Windows.Forms.UserControl
	{
      public event SelectedUnitSystemChangedEventHandler SelectedUnitSystemChanged;

      private System.Windows.Forms.ListBox listBoxUnitSystems;
      private System.Windows.Forms.Button buttonEdit;
      private System.Windows.Forms.Button buttonDuplicate;
      private System.Windows.Forms.Button buttonDelete;
      private UnitSystemControl unitSystemCtrl;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UnitSystemsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         unitSystemCtrl = new UnitSystemControl();
         this.Controls.Add(this.unitSystemCtrl);
         this.unitSystemCtrl.Location = new Point(160, 5);

         IList unitSystems = UnitSystemService.GetInstance().GetUnitSystemCatalog().GetList();
         IEnumerator e = unitSystems.GetEnumerator();
         while (e.MoveNext())
         {
            this.listBoxUnitSystems.Items.Add((UnitSystem)e.Current);
         }
         this.listBoxUnitSystems.SetSelected(0, true);

         this.UpdateUnitSystemUI();
         UnitSystemCatalog unitSystemCatalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();
         unitSystemCatalog.UnitSystemAdded += new UnitSystemAddedEventHandler(unitSystemCatalog_UnitSystemAdded);
         unitSystemCatalog.UnitSystemDeleted += new UnitSystemDeletedEventHandler(unitSystemCatalog_UnitSystemDeleted);
         unitSystemCatalog.UnitSystemChanged += new UnitSystemChangedEventHandler(unitSystemCatalog_UnitSystemChanged); 
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         UnitSystemCatalog unitSystemCatalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();
         unitSystemCatalog.UnitSystemAdded -= new UnitSystemAddedEventHandler(unitSystemCatalog_UnitSystemAdded);
         unitSystemCatalog.UnitSystemDeleted -= new UnitSystemDeletedEventHandler(unitSystemCatalog_UnitSystemDeleted);
         unitSystemCatalog.UnitSystemChanged -= new UnitSystemChangedEventHandler(unitSystemCatalog_UnitSystemChanged); 
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
         this.listBoxUnitSystems = new System.Windows.Forms.ListBox();
         this.buttonEdit = new System.Windows.Forms.Button();
         this.buttonDuplicate = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // listBoxUnitSystems
         // 
         this.listBoxUnitSystems.Location = new System.Drawing.Point(4, 8);
         this.listBoxUnitSystems.Name = "listBoxUnitSystems";
         this.listBoxUnitSystems.Size = new System.Drawing.Size(152, 277);
         this.listBoxUnitSystems.TabIndex = 15;
         this.listBoxUnitSystems.SelectedIndexChanged += new System.EventHandler(this.listBoxUnitSystems_SelectedIndexChanged);
         // 
         // buttonEdit
         // 
         this.buttonEdit.Location = new System.Drawing.Point(308, 288);
         this.buttonEdit.Name = "buttonEdit";
         this.buttonEdit.TabIndex = 14;
         this.buttonEdit.Text = "Edit";
         this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
         // 
         // buttonDuplicate
         // 
         this.buttonDuplicate.Location = new System.Drawing.Point(52, 288);
         this.buttonDuplicate.Name = "buttonDuplicate";
         this.buttonDuplicate.Size = new System.Drawing.Size(72, 23);
         this.buttonDuplicate.TabIndex = 12;
         this.buttonDuplicate.Text = "Duplicate";
         this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(240, 288);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.Size = new System.Drawing.Size(56, 23);
         this.buttonDelete.TabIndex = 13;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // UnitSystemsControl
         // 
         this.Controls.Add(this.buttonEdit);
         this.Controls.Add(this.buttonDuplicate);
         this.Controls.Add(this.listBoxUnitSystems);
         this.Controls.Add(this.buttonDelete);
         this.Name = "UnitSystemsControl";
         this.Size = new System.Drawing.Size(412, 316);
         this.ResumeLayout(false);

      }
		#endregion

      public UnitSystem GetSelectedUnitSystem()
      {
         UnitSystem unitSystem = (UnitSystem)this.listBoxUnitSystems.SelectedItem;
         return unitSystem;
      }

      public int GetSelectedIndex()
      {
         int idx = this.listBoxUnitSystems.SelectedIndex;
         return idx;
      }

      private void buttonEdit_Click(object sender, System.EventArgs e)
      {
         this.Edit();
      }

      private void Edit()
      {
         if (this.listBoxUnitSystems.SelectedItem != null)
         {
            UnitSystem unitSystem = (UnitSystem)this.listBoxUnitSystems.SelectedItem;
            if (!unitSystem.IsReadOnly)
            {
               UnitSystemEditor unitSystemEditor = new UnitSystemEditor(this);
               unitSystemEditor.ShowDialog();
            }
         }
      }

      private void buttonDuplicate_Click(object sender, System.EventArgs e)
      {
         this.Duplicate();
      }

      private void Duplicate()
      {
         if (this.listBoxUnitSystems.SelectedItem != null)
         {
            UnitSystem unitSystem = (UnitSystem)this.listBoxUnitSystems.SelectedItem;
            UnitSystem us = unitSystem.Clone();
            us.IsReadOnly = false;
            us.Name = "Copy of " + unitSystem.Name;
            UnitSystemService.GetInstance().GetUnitSystemCatalog().Add(us);
         }
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.Delete();
      }

      private void Delete()
      {
         if (this.listBoxUnitSystems.SelectedItem != null)
         {
            UnitSystem unitSystem = (UnitSystem)this.listBoxUnitSystems.SelectedItem;
            if (!unitSystem.IsReadOnly)
            {
               string message = "Are you sure that you want to delete the selected Unit System?";
               DialogResult dr = MessageBox.Show(this, message, "Delete Unit System",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               switch (dr)
               {
                  case System.Windows.Forms.DialogResult.Yes:
                     UnitSystemService.GetInstance().GetUnitSystemCatalog().Remove(unitSystem);
                     break;
                  case System.Windows.Forms.DialogResult.No:
                     break;
               }
            }
         }
      }

      private void listBoxUnitSystems_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         UpdateUnitSystemUI();
         OnSelectedUnitSystemChanged(this);
      }

      private void UpdateUnitSystemUI()
      {
         UnitSystem unitSystem = (UnitSystem)this.listBoxUnitSystems.SelectedItem;
         int selIdx = this.listBoxUnitSystems.SelectedIndex;
         this.buttonDelete.Enabled = false;
         this.buttonEdit.Enabled = false;
         if (selIdx >= 0)
         {
            if (unitSystem != null && !unitSystem.IsReadOnly)
            {
               this.buttonDelete.Enabled = true;
               this.buttonEdit.Enabled = true;
            }
         }
         this.unitSystemCtrl.UpdateUnitSystem(unitSystem);
      }

      private void OnSelectedUnitSystemChanged(object sender)
      {
         if (SelectedUnitSystemChanged != null)
            SelectedUnitSystemChanged(sender);
      }

      private void unitSystemCatalog_UnitSystemAdded(UnitSystem unitSystem)
      {
         this.listBoxUnitSystems.Items.Clear();

         IList unitSystems = UnitSystemService.GetInstance().GetUnitSystemCatalog().GetList();
         int idx = 0;
         int i = 0;
         IEnumerator e = unitSystems.GetEnumerator();
         while (e.MoveNext())
         {
            UnitSystem us = (UnitSystem)e.Current;
            this.listBoxUnitSystems.Items.Add(us);
            if (us.Equals(unitSystem))
               idx = i;
            i++;
         }
         this.listBoxUnitSystems.SetSelected(idx, true);
         this.UpdateUnitSystemUI();
      }

      private void unitSystemCatalog_UnitSystemDeleted(string name)
      {
         this.listBoxUnitSystems.Items.Clear();
         IList unitSystems = UnitSystemService.GetInstance().GetUnitSystemCatalog().GetList();
         IEnumerator e = unitSystems.GetEnumerator();
         while (e.MoveNext())
         {
            UnitSystem us = (UnitSystem)e.Current;
            this.listBoxUnitSystems.Items.Add(us);
         }
         this.listBoxUnitSystems.SetSelected(0, true);
         this.UpdateUnitSystemUI();
      }

      private void unitSystemCatalog_UnitSystemChanged(UnitSystem unitSystem)
      {
         int idx = this.listBoxUnitSystems.SelectedIndex;
         this.listBoxUnitSystems.Items.RemoveAt(idx);
         this.listBoxUnitSystems.Items.Insert(idx, unitSystem);
         this.listBoxUnitSystems.SetSelected(idx, true);
         this.UpdateUnitSystemUI();
      }
   }
}
