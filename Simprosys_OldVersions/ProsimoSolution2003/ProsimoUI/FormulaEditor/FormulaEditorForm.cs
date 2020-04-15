using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo;

namespace ProsimoUI.FormulaEditor
{
	/// <summary>
	/// Summary description for FormulaEditorForm.
	/// </summary>
	public class FormulaEditorForm : System.Windows.Forms.Form
	{
      private EvaporationAndDryingSystem evapAndDryingSystem;
      
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.ListView listViewUnitOps;
      private System.Windows.Forms.ColumnHeader columnHeaderUnitOp;
      private System.Windows.Forms.Splitter splitter1;
      private System.Windows.Forms.Panel panelVariables;
      private ProsimoUI.FormulaEditor.FormulaEditorHeaderControl formulaEditorHeaderControl;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FormulaEditorForm()
      {
      }

		public FormulaEditorForm(EvaporationAndDryingSystem evapAndDryingSystem)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
         
         this.evapAndDryingSystem = evapAndDryingSystem;
         this.PopulateUnitOpList(evapAndDryingSystem);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormulaEditorForm));
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.listViewUnitOps = new System.Windows.Forms.ListView();
         this.columnHeaderUnitOp = new System.Windows.Forms.ColumnHeader();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.panelVariables = new System.Windows.Forms.Panel();
         this.formulaEditorHeaderControl = new ProsimoUI.FormulaEditor.FormulaEditorHeaderControl();
         this.panelVariables.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // listViewUnitOps
         // 
         this.listViewUnitOps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                          this.columnHeaderUnitOp});
         this.listViewUnitOps.Dock = System.Windows.Forms.DockStyle.Left;
         this.listViewUnitOps.FullRowSelect = true;
         this.listViewUnitOps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.listViewUnitOps.LabelWrap = false;
         this.listViewUnitOps.Location = new System.Drawing.Point(0, 0);
         this.listViewUnitOps.MultiSelect = false;
         this.listViewUnitOps.Name = "listViewUnitOps";
         this.listViewUnitOps.Size = new System.Drawing.Size(124, 447);
         this.listViewUnitOps.TabIndex = 0;
         this.listViewUnitOps.View = System.Windows.Forms.View.Details;
         this.listViewUnitOps.SelectedIndexChanged += new System.EventHandler(this.listViewUnitOps_SelectedIndexChanged);
         // 
         // columnHeaderUnitOp
         // 
         this.columnHeaderUnitOp.Text = "Unit Operation";
         this.columnHeaderUnitOp.Width = 119;
         // 
         // splitter1
         // 
         this.splitter1.Location = new System.Drawing.Point(124, 0);
         this.splitter1.Name = "splitter1";
         this.splitter1.Size = new System.Drawing.Size(2, 447);
         this.splitter1.TabIndex = 1;
         this.splitter1.TabStop = false;
         // 
         // panelVariables
         // 
         this.panelVariables.AutoScroll = true;
         this.panelVariables.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelVariables.Controls.Add(this.formulaEditorHeaderControl);
         this.panelVariables.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelVariables.Location = new System.Drawing.Point(126, 0);
         this.panelVariables.Name = "panelVariables";
         this.panelVariables.Size = new System.Drawing.Size(558, 447);
         this.panelVariables.TabIndex = 2;
         // 
         // formulaEditorHeaderControl
         // 
         this.formulaEditorHeaderControl.Dock = System.Windows.Forms.DockStyle.Top;
         this.formulaEditorHeaderControl.Location = new System.Drawing.Point(0, 0);
         this.formulaEditorHeaderControl.Name = "formulaEditorHeaderControl";
         this.formulaEditorHeaderControl.Size = new System.Drawing.Size(554, 20);
         this.formulaEditorHeaderControl.TabIndex = 0;
         // 
         // FormulaEditorForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(684, 447);
         this.Controls.Add(this.panelVariables);
         this.Controls.Add(this.splitter1);
         this.Controls.Add(this.listViewUnitOps);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "FormulaEditorForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Formula Editor";
         this.panelVariables.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void PopulateUnitOpList(EvaporationAndDryingSystem evapAndDryingSystem)
      {
         IEnumerator e = evapAndDryingSystem.GetUnitOpList().GetEnumerator();
         while (e.MoveNext())
         {
            Solvable solvable = (Solvable)e.Current;
            if (!(solvable is Recycle))
            {
               ListViewSolvableItem item = new ListViewSolvableItem(solvable);
               this.listViewUnitOps.Items.Add(item);
            }
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void listViewUnitOps_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (this.listViewUnitOps.SelectedItems.Count >0)
         {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            this.panelVariables.Visible = false;
            this.panelVariables.Controls.Clear();
            this.panelVariables.Controls.Add(this.formulaEditorHeaderControl);
            ListViewSolvableItem item = (ListViewSolvableItem)this.listViewUnitOps.SelectedItems[0];
            UnitOperation uo = item.Solvable as UnitOperation;
            IEnumerator en = uo.GetAllVariables().GetEnumerator();
            while (en.MoveNext())
            {
               ProcessVar pv = (ProcessVar)en.Current;
               FormulaEditorElementControl ctrl = new FormulaEditorElementControl(pv, this.evapAndDryingSystem.FormulaTable);
               ctrl.Dock = DockStyle.Top;
               this.panelVariables.Controls.Add(ctrl);
               ctrl.BringToFront();
            }
            this.panelVariables.Visible = true;
            this.Enabled = true;
            this.Cursor = Cursors.Default;
         }
      }
	}
}
