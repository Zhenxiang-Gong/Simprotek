using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SolvableVariablesControl.
	/// </summary>
	public class SolvableVariablesControl : System.Windows.Forms.UserControl
	{
      public ListBox ListBoxSolvable
      {
         get {return listBoxSolvable;}
      }
      
      public ListView ListViewVariable
      {
         get {return listViewVariable;}
      }

      private ProcessVariableListType variableListType;
      public ProcessVariableListType ProcessVarListType
      {
         set {variableListType = value;}
      }

      private System.Windows.Forms.Splitter splitter1;
      private System.Windows.Forms.ListBox listBoxSolvable;
      private System.Windows.Forms.ListView listViewVariable;
      private System.Windows.Forms.ColumnHeader columnHeaderVariable;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SolvableVariablesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.variableListType = ProcessVariableListType.All;
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
         this.listBoxSolvable = new System.Windows.Forms.ListBox();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.listViewVariable = new System.Windows.Forms.ListView();
         this.columnHeaderVariable = new System.Windows.Forms.ColumnHeader();
         this.SuspendLayout();
         // 
         // listBoxSolvable
         // 
         this.listBoxSolvable.Dock = System.Windows.Forms.DockStyle.Left;
         this.listBoxSolvable.HorizontalScrollbar = true;
         this.listBoxSolvable.Location = new System.Drawing.Point(0, 0);
         this.listBoxSolvable.Name = "listBoxSolvable";
         this.listBoxSolvable.Size = new System.Drawing.Size(112, 251);
         this.listBoxSolvable.TabIndex = 0;
         this.listBoxSolvable.SelectedIndexChanged += new System.EventHandler(this.listBoxSolvable_SelectedIndexChanged);
         // 
         // splitter1
         // 
         this.splitter1.Enabled = false;
         this.splitter1.Location = new System.Drawing.Point(112, 0);
         this.splitter1.Name = "splitter1";
         this.splitter1.Size = new System.Drawing.Size(2, 252);
         this.splitter1.TabIndex = 1;
         this.splitter1.TabStop = false;
         // 
         // listViewVariable
         // 
         this.listViewVariable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                           this.columnHeaderVariable});
         this.listViewVariable.FullRowSelect = true;
         this.listViewVariable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.listViewVariable.LabelWrap = false;
         this.listViewVariable.Location = new System.Drawing.Point(115, 0);
         this.listViewVariable.Name = "listViewVariable";
         this.listViewVariable.Size = new System.Drawing.Size(261, 252);
         this.listViewVariable.TabIndex = 2;
         this.listViewVariable.View = System.Windows.Forms.View.Details;
         // 
         // columnHeaderVariable
         // 
         this.columnHeaderVariable.Text = "Process Variable";
         this.columnHeaderVariable.Width = 237;
         // 
         // SolvableVariablesControl
         // 
         this.Controls.Add(this.listViewVariable);
         this.Controls.Add(this.splitter1);
         this.Controls.Add(this.listBoxSolvable);
         this.Name = "SolvableVariablesControl";
         this.Size = new System.Drawing.Size(376, 252);
         this.ResumeLayout(false);

      }
		#endregion

      public void SelectProcessVariable(ProcessVar procVar)
      {
         for (int i=0; i<this.listViewVariable.Items.Count; i++)
         {
            ListViewVariableItem lvvi = this.listViewVariable.Items[i] as ListViewVariableItem ;
            if (lvvi.Variable.Equals(procVar))
            {
               IProcessVarOwner iProcVarOwner = procVar.Owner;
               this.SelectProcessVarOwner(iProcVarOwner);
               lvvi.Selected = true;
               break;
            }
         }
      }

      public void SelectProcessVarOwner(IProcessVarOwner iProcVarOwner)
      {
         for (int i=0; i<this.listBoxSolvable.Items.Count; i++)
         {
            IProcessVarOwner ipvOwner = (IProcessVarOwner)this.listBoxSolvable.Items[i];
            if (ipvOwner.Name.Equals(iProcVarOwner.Name))
            {
               this.listBoxSolvable.SelectedIndex = i;
               break;
            }
         }
      }

      private void listBoxSolvable_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         this.listViewVariable.Items.Clear();
         Solvable s = (Solvable)this.listBoxSolvable.SelectedItem;

         IList list = null;
         if (this.variableListType == ProcessVariableListType.All)
            list = s.VarList;
         else if (this.variableListType == ProcessVariableListType.Calculated)
            list = s.CalculatedVarList;
         else if (this.variableListType == ProcessVariableListType.Specified)
            list = s.SpecifiedVarList;
         
         IEnumerator en = list.GetEnumerator();
         while (en.MoveNext())
         {
            ProcessVar pv = (ProcessVar)en.Current;
            this.listViewVariable.Items.Add(new ListViewVariableItem(pv));
         }
      }

      public ProcessVar GetSelectedProcessVar()
      {
         ProcessVar pVar = null;
         if (this.listViewVariable.SelectedItems.Count == 1)
         {
            ListViewVariableItem lvvi = this.listViewVariable.SelectedItems[0] as ListViewVariableItem ;
            pVar = lvvi.Variable;
         }
         return pVar;
      }

      public IProcessVarOwner GetSelectedProcessVarOwner()
      {
         IProcessVarOwner varOwner = null;
         if (this.listBoxSolvable.SelectedItems.Count == 1)
         {
            varOwner = this.listBoxSolvable.SelectedItem as IProcessVarOwner;
         }
         return varOwner;
      }
	}
}
