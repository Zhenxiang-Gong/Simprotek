using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo;
using ProsimoUI;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for VariableSelectorControl.
	/// </summary>
	public class VariableSelectorControl : System.Windows.Forms.UserControl
	{
      private CustomEditor editor;
      public CustomEditor Editor
      {
         get {return editor;}
         set {editor = value;}
      }

      private System.Windows.Forms.TabControl tabControl;
      private System.Windows.Forms.TabPage tabPageUnitOps;
      private System.Windows.Forms.TabPage tabPageStreams;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Panel panelAdd;
      private ProsimoUI.SolvableVariablesControl solvableVariablesControlUnitOp;
      private ProsimoUI.SolvableVariablesControl solvableVariablesControlStream;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public VariableSelectorControl()
		{
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         // need to set the editor and initialize the lists outside of the constructor
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
         this.tabControl = new System.Windows.Forms.TabControl();
         this.tabPageUnitOps = new System.Windows.Forms.TabPage();
         this.solvableVariablesControlUnitOp = new ProsimoUI.SolvableVariablesControl();
         this.tabPageStreams = new System.Windows.Forms.TabPage();
         this.solvableVariablesControlStream = new ProsimoUI.SolvableVariablesControl();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.panel = new System.Windows.Forms.Panel();
         this.panelAdd = new System.Windows.Forms.Panel();
         this.tabControl.SuspendLayout();
         this.tabPageUnitOps.SuspendLayout();
         this.panel.SuspendLayout();
         this.panelAdd.SuspendLayout();
         this.SuspendLayout();
         // 
         // tabControl
         // 
         this.tabControl.Controls.Add(this.tabPageUnitOps);
         this.tabControl.Controls.Add(this.tabPageStreams);
         this.tabControl.Location = new System.Drawing.Point(0, 0);
         this.tabControl.Name = "tabControl";
         this.tabControl.SelectedIndex = 0;
         this.tabControl.Size = new System.Drawing.Size(384, 276);
         this.tabControl.TabIndex = 2;
         // 
         // tabPageUnitOps
         // 
         this.tabPageUnitOps.Controls.Add(this.solvableVariablesControlUnitOp);
         this.tabPageUnitOps.Location = new System.Drawing.Point(4, 22);
         this.tabPageUnitOps.Name = "tabPageUnitOps";
         this.tabPageUnitOps.Size = new System.Drawing.Size(376, 250);
         this.tabPageUnitOps.TabIndex = 0;
         this.tabPageUnitOps.Text = "Unit Operations";
         // 
         // solvableVariablesControlUnitOp
         // 
         this.solvableVariablesControlUnitOp.Location = new System.Drawing.Point(0, 0);
         this.solvableVariablesControlUnitOp.Name = "solvableVariablesControlUnitOp";
         this.solvableVariablesControlUnitOp.Size = new System.Drawing.Size(376, 252);
         this.solvableVariablesControlUnitOp.TabIndex = 0;
         // 
         // tabPageStreams
         // 
         this.tabPageStreams.Controls.Add(this.solvableVariablesControlStream);
         this.tabPageStreams.Location = new System.Drawing.Point(4, 22);
         this.tabPageStreams.Name = "tabPageStreams";
         this.tabPageStreams.Size = new System.Drawing.Size(376, 254);
         this.tabPageStreams.TabIndex = 1;
         this.tabPageStreams.Text = "Streams";
         this.tabPageStreams.Visible = false;
         // 
         // solvableVariablesControlStream
         // 
         this.solvableVariablesControlStream.Location = new System.Drawing.Point(0, 0);
         this.solvableVariablesControlStream.Name = "solvableVariablesControlStream";
         this.solvableVariablesControlStream.Size = new System.Drawing.Size(376, 252);
         this.solvableVariablesControlStream.TabIndex = 0;
         // 
         // buttonAdd
         // 
         this.buttonAdd.Location = new System.Drawing.Point(148, 0);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.TabIndex = 46;
         this.buttonAdd.Text = "Add";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.panelAdd);
         this.panel.Controls.Add(this.tabControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(388, 308);
         this.panel.TabIndex = 47;
         // 
         // panelAdd
         // 
         this.panelAdd.Controls.Add(this.buttonAdd);
         this.panelAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelAdd.Location = new System.Drawing.Point(0, 280);
         this.panelAdd.Name = "panelAdd";
         this.panelAdd.Size = new System.Drawing.Size(384, 26);
         this.panelAdd.TabIndex = 47;
         // 
         // VariableSelectorControl
         // 
         this.Controls.Add(this.panel);
         this.Name = "VariableSelectorControl";
         this.Size = new System.Drawing.Size(388, 308);
         this.tabControl.ResumeLayout(false);
         this.tabPageUnitOps.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.panelAdd.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeSolvableLists()
      {
         this.PopulateUnitOpList(this.editor.flowsheet.EvaporationAndDryingSystem);
         this.PopulateStreamList(this.editor.flowsheet.EvaporationAndDryingSystem);

         ListView listViewUnitOpVariables = this.solvableVariablesControlUnitOp.ListViewVariable;
         listViewUnitOpVariables.DoubleClick += new EventHandler(listViewUnitOpVariables_DoubleClick);
         ListView listViewStreamVariables = this.solvableVariablesControlStream.ListViewVariable;
         listViewStreamVariables.DoubleClick += new EventHandler(listViewStreamVariables_DoubleClick);
      }

      private void PopulateUnitOpList(EvaporationAndDryingSystem evapAndDryingSystem)
      {
         IEnumerator e = evapAndDryingSystem.GetUnitOpList().GetEnumerator();
         while (e.MoveNext())
         {
            UnitOperation uo = (UnitOperation)e.Current;
            if (!(uo is Recycle))
               this.solvableVariablesControlUnitOp.ListBoxSolvable.Items.Add(uo);
         }
      }

      private void PopulateStreamList(EvaporationAndDryingSystem evapAndDryingSystem)
      {
         IEnumerator e = evapAndDryingSystem.GetStreamList().GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.solvableVariablesControlStream.ListBoxSolvable.Items.Add(psb);
         }
      }

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         ArrayList vars = new ArrayList();
         if (this.tabControl.SelectedIndex == 0)
         {
            IEnumerator en = this.solvableVariablesControlUnitOp.ListViewVariable.SelectedItems.GetEnumerator();
            while (en.MoveNext())
            {
               ListViewVariableItem item = (ListViewVariableItem)en.Current;
               vars.Add(item.Variable);
            }
         }
         else if (this.tabControl.SelectedIndex == 1)
         {
            IEnumerator en = this.solvableVariablesControlStream.ListViewVariable.SelectedItems.GetEnumerator();
            while (en.MoveNext())
            {
               ListViewVariableItem item = (ListViewVariableItem)en.Current;
               vars.Add(item.Variable);
            }
         }
         this.editor.AddProcessVars(vars);
      }

      private void listViewUnitOpVariables_DoubleClick(object sender, EventArgs e)
      {
         ListViewVariableItem item = (ListViewVariableItem)this.solvableVariablesControlUnitOp.ListViewVariable.SelectedItems[0];
         ProcessVar var = item.Variable;
         if (var != null)
            this.editor.AddProcessVar(var);
      }

      private void listViewStreamVariables_DoubleClick(object sender, EventArgs e)
      {
         ListViewVariableItem item = (ListViewVariableItem)this.solvableVariablesControlStream.ListViewVariable.SelectedItems[0];
         ProcessVar var = item.Variable;
         if (var != null)
            this.editor.AddProcessVar(var);
      }
   }
}
