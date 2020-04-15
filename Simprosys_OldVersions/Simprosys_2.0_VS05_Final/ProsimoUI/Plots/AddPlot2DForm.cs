using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Plots;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for AddPlot2DForm.
	/// </summary>
	public class AddPlot2DForm : System.Windows.Forms.Form
	{
      private Plot2DCache p2dCache;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.CheckBox checkBoxIncludeParamVar;
      private System.Windows.Forms.GroupBox groupBoxSelections;
      private System.Windows.Forms.Label labelIndependentVarSelected;
      private System.Windows.Forms.TextBox textBoxIndependentVarSelected;
      private System.Windows.Forms.TabControl tabControl;
      private System.Windows.Forms.TextBox textBoxParameterSelected;
      private System.Windows.Forms.Label labelParameterSelected;
      private System.Windows.Forms.TextBox textBoxDependentVarSelected;
      private System.Windows.Forms.Label labelDependentVarSelected;
      private System.Windows.Forms.TabPage tabPageIndependentVar;
      private System.Windows.Forms.TabPage tabPageDependentVar;
      private System.Windows.Forms.TabPage tabPageParameter;
      private ProsimoUI.SolvableVariablesControl solvableVariablesControlIndependentVar;
      private ProsimoUI.SolvableVariablesControl solvableVariablesControlDependentVar;
      private ProsimoUI.SolvableVariablesControl solvableVariablesControlParameter;
      private System.Windows.Forms.Button buttonSetProcessVariable;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddPlot2DForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
      }

      public AddPlot2DForm(EvaporationAndDryingSystem system)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.PopulateIndependentVarStreamList(system);
         this.PopulateDependentVarStreamList(system);
         this.PopulateParameterStreamList(system);

         this.Text = "New Plot";
         this.p2dCache = new Plot2DCache(system.PlotCatalog);
         this.textBoxName.Text = "New Plot";
         this.p2dCache.IncludeParameterVariable = this.checkBoxIncludeParamVar.Checked;
   
         this.UpdateTheUI();
         this.tabControl.SelectedTab = this.tabPageIndependentVar;
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
         this.panel = new System.Windows.Forms.Panel();
         this.buttonSetProcessVariable = new System.Windows.Forms.Button();
         this.groupBoxSelections = new System.Windows.Forms.GroupBox();
         this.textBoxDependentVarSelected = new System.Windows.Forms.TextBox();
         this.labelDependentVarSelected = new System.Windows.Forms.Label();
         this.textBoxParameterSelected = new System.Windows.Forms.TextBox();
         this.labelParameterSelected = new System.Windows.Forms.Label();
         this.textBoxIndependentVarSelected = new System.Windows.Forms.TextBox();
         this.labelIndependentVarSelected = new System.Windows.Forms.Label();
         this.tabControl = new System.Windows.Forms.TabControl();
         this.tabPageIndependentVar = new System.Windows.Forms.TabPage();
         this.solvableVariablesControlIndependentVar = new ProsimoUI.SolvableVariablesControl();
         this.tabPageDependentVar = new System.Windows.Forms.TabPage();
         this.solvableVariablesControlDependentVar = new ProsimoUI.SolvableVariablesControl();
         this.tabPageParameter = new System.Windows.Forms.TabPage();
         this.solvableVariablesControlParameter = new ProsimoUI.SolvableVariablesControl();
         this.checkBoxIncludeParamVar = new System.Windows.Forms.CheckBox();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.labelName = new System.Windows.Forms.Label();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panel.SuspendLayout();
         this.groupBoxSelections.SuspendLayout();
         this.tabControl.SuspendLayout();
         this.tabPageIndependentVar.SuspendLayout();
         this.tabPageDependentVar.SuspendLayout();
         this.tabPageParameter.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.buttonSetProcessVariable);
         this.panel.Controls.Add(this.groupBoxSelections);
         this.panel.Controls.Add(this.tabControl);
         this.panel.Controls.Add(this.checkBoxIncludeParamVar);
         this.panel.Controls.Add(this.textBoxName);
         this.panel.Controls.Add(this.labelName);
         this.panel.Controls.Add(this.buttonCancel);
         this.panel.Controls.Add(this.buttonOk);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(394, 468);
         this.panel.TabIndex = 0;
         // 
         // buttonSetProcessVariable
         // 
         this.buttonSetProcessVariable.Location = new System.Drawing.Point(128, 320);
         this.buttonSetProcessVariable.Name = "buttonSetProcessVariable";
         this.buttonSetProcessVariable.Size = new System.Drawing.Size(128, 23);
         this.buttonSetProcessVariable.TabIndex = 8;
         this.buttonSetProcessVariable.Text = "Set Process Variable";
         this.buttonSetProcessVariable.Click += new System.EventHandler(this.buttonSetProcessVariable_Click);
         // 
         // groupBoxSelections
         // 
         this.groupBoxSelections.Controls.Add(this.textBoxDependentVarSelected);
         this.groupBoxSelections.Controls.Add(this.labelDependentVarSelected);
         this.groupBoxSelections.Controls.Add(this.textBoxParameterSelected);
         this.groupBoxSelections.Controls.Add(this.labelParameterSelected);
         this.groupBoxSelections.Controls.Add(this.textBoxIndependentVarSelected);
         this.groupBoxSelections.Controls.Add(this.labelIndependentVarSelected);
         this.groupBoxSelections.Location = new System.Drawing.Point(4, 348);
         this.groupBoxSelections.Name = "groupBoxSelections";
         this.groupBoxSelections.Size = new System.Drawing.Size(384, 80);
         this.groupBoxSelections.TabIndex = 6;
         this.groupBoxSelections.TabStop = false;
         this.groupBoxSelections.Text = "Selections";
         // 
         // textBoxDependentVarSelected
         // 
         this.textBoxDependentVarSelected.BackColor = System.Drawing.Color.White;
         this.textBoxDependentVarSelected.Location = new System.Drawing.Point(136, 36);
         this.textBoxDependentVarSelected.Name = "textBoxDependentVarSelected";
         this.textBoxDependentVarSelected.ReadOnly = true;
         this.textBoxDependentVarSelected.Size = new System.Drawing.Size(244, 20);
         this.textBoxDependentVarSelected.TabIndex = 9;
         this.textBoxDependentVarSelected.Text = "";
         // 
         // labelDependentVarSelected
         // 
         this.labelDependentVarSelected.Location = new System.Drawing.Point(8, 40);
         this.labelDependentVarSelected.Name = "labelDependentVarSelected";
         this.labelDependentVarSelected.Size = new System.Drawing.Size(124, 16);
         this.labelDependentVarSelected.TabIndex = 8;
         this.labelDependentVarSelected.Text = "Dependent Variable:";
         this.labelDependentVarSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxParameterSelected
         // 
         this.textBoxParameterSelected.BackColor = System.Drawing.Color.White;
         this.textBoxParameterSelected.Location = new System.Drawing.Point(136, 56);
         this.textBoxParameterSelected.Name = "textBoxParameterSelected";
         this.textBoxParameterSelected.ReadOnly = true;
         this.textBoxParameterSelected.Size = new System.Drawing.Size(244, 20);
         this.textBoxParameterSelected.TabIndex = 7;
         this.textBoxParameterSelected.Text = "";
         // 
         // labelParameterSelected
         // 
         this.labelParameterSelected.Location = new System.Drawing.Point(8, 60);
         this.labelParameterSelected.Name = "labelParameterSelected";
         this.labelParameterSelected.Size = new System.Drawing.Size(124, 16);
         this.labelParameterSelected.TabIndex = 6;
         this.labelParameterSelected.Text = "Parameter :";
         this.labelParameterSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxIndependentVarSelected
         // 
         this.textBoxIndependentVarSelected.BackColor = System.Drawing.Color.White;
         this.textBoxIndependentVarSelected.Location = new System.Drawing.Point(136, 16);
         this.textBoxIndependentVarSelected.Name = "textBoxIndependentVarSelected";
         this.textBoxIndependentVarSelected.ReadOnly = true;
         this.textBoxIndependentVarSelected.Size = new System.Drawing.Size(244, 20);
         this.textBoxIndependentVarSelected.TabIndex = 5;
         this.textBoxIndependentVarSelected.Text = "";
         // 
         // labelIndependentVarSelected
         // 
         this.labelIndependentVarSelected.Location = new System.Drawing.Point(8, 20);
         this.labelIndependentVarSelected.Name = "labelIndependentVarSelected";
         this.labelIndependentVarSelected.Size = new System.Drawing.Size(124, 16);
         this.labelIndependentVarSelected.TabIndex = 4;
         this.labelIndependentVarSelected.Text = "Independent Variable:";
         this.labelIndependentVarSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // tabControl
         // 
         this.tabControl.Controls.Add(this.tabPageIndependentVar);
         this.tabControl.Controls.Add(this.tabPageDependentVar);
         this.tabControl.Controls.Add(this.tabPageParameter);
         this.tabControl.Location = new System.Drawing.Point(4, 40);
         this.tabControl.Name = "tabControl";
         this.tabControl.SelectedIndex = 0;
         this.tabControl.Size = new System.Drawing.Size(384, 276);
         this.tabControl.TabIndex = 5;
         // 
         // tabPageIndependentVar
         // 
         this.tabPageIndependentVar.Controls.Add(this.solvableVariablesControlIndependentVar);
         this.tabPageIndependentVar.Location = new System.Drawing.Point(4, 22);
         this.tabPageIndependentVar.Name = "tabPageIndependentVar";
         this.tabPageIndependentVar.Size = new System.Drawing.Size(376, 250);
         this.tabPageIndependentVar.TabIndex = 0;
         this.tabPageIndependentVar.Text = "Independent Variable";
         // 
         // solvableVariablesControlIndependentVar
         // 
         this.solvableVariablesControlIndependentVar.Location = new System.Drawing.Point(0, 0);
         this.solvableVariablesControlIndependentVar.Name = "solvableVariablesControlIndependentVar";
         this.solvableVariablesControlIndependentVar.Size = new System.Drawing.Size(376, 252);
         this.solvableVariablesControlIndependentVar.TabIndex = 0;
         // 
         // tabPageDependentVar
         // 
         this.tabPageDependentVar.Controls.Add(this.solvableVariablesControlDependentVar);
         this.tabPageDependentVar.Location = new System.Drawing.Point(4, 22);
         this.tabPageDependentVar.Name = "tabPageDependentVar";
         this.tabPageDependentVar.Size = new System.Drawing.Size(376, 250);
         this.tabPageDependentVar.TabIndex = 1;
         this.tabPageDependentVar.Text = "Dependent Variable";
         // 
         // solvableVariablesControlDependentVar
         // 
         this.solvableVariablesControlDependentVar.Location = new System.Drawing.Point(0, 0);
         this.solvableVariablesControlDependentVar.Name = "solvableVariablesControlDependentVar";
         this.solvableVariablesControlDependentVar.Size = new System.Drawing.Size(376, 252);
         this.solvableVariablesControlDependentVar.TabIndex = 0;
         // 
         // tabPageParameter
         // 
         this.tabPageParameter.Controls.Add(this.solvableVariablesControlParameter);
         this.tabPageParameter.Location = new System.Drawing.Point(4, 22);
         this.tabPageParameter.Name = "tabPageParameter";
         this.tabPageParameter.Size = new System.Drawing.Size(376, 250);
         this.tabPageParameter.TabIndex = 2;
         this.tabPageParameter.Text = "Parameter";
         // 
         // solvableVariablesControlParameter
         // 
         this.solvableVariablesControlParameter.Location = new System.Drawing.Point(0, 0);
         this.solvableVariablesControlParameter.Name = "solvableVariablesControlParameter";
         this.solvableVariablesControlParameter.Size = new System.Drawing.Size(376, 252);
         this.solvableVariablesControlParameter.TabIndex = 0;
         // 
         // checkBoxIncludeParamVar
         // 
         this.checkBoxIncludeParamVar.Location = new System.Drawing.Point(216, 8);
         this.checkBoxIncludeParamVar.Name = "checkBoxIncludeParamVar";
         this.checkBoxIncludeParamVar.Size = new System.Drawing.Size(172, 20);
         this.checkBoxIncludeParamVar.TabIndex = 4;
         this.checkBoxIncludeParamVar.Text = "Include Parameter Variable";
         this.checkBoxIncludeParamVar.CheckedChanged += new System.EventHandler(this.checkBoxIncludeParamVar_CheckedChanged);
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(56, 8);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(148, 20);
         this.textBoxName.TabIndex = 3;
         this.textBoxName.Text = "";
         // 
         // labelName
         // 
         this.labelName.Location = new System.Drawing.Point(0, 12);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(52, 16);
         this.labelName.TabIndex = 2;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(200, 436);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 1;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(116, 436);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 0;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // AddPlot2DForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(394, 468);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "AddPlot2DForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "AddPlot2DForm";
         this.panel.ResumeLayout(false);
         this.groupBoxSelections.ResumeLayout(false);
         this.tabControl.ResumeLayout(false);
         this.tabPageIndependentVar.ResumeLayout(false);
         this.tabPageDependentVar.ResumeLayout(false);
         this.tabPageParameter.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void PopulateIndependentVarStreamList(EvaporationAndDryingSystem system)
      {
         this.solvableVariablesControlIndependentVar.ProcessVarListType = ProcessVariableListType.Specified;
         IEnumerator e = system.GetStreamList().GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.solvableVariablesControlIndependentVar.ListBoxSolvable.Items.Add(psb);
         }
      }

      private void PopulateDependentVarStreamList(EvaporationAndDryingSystem system)
      {
         this.solvableVariablesControlDependentVar.ProcessVarListType = ProcessVariableListType.Calculated;
         IEnumerator e = system.GetStreamList().GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.solvableVariablesControlDependentVar.ListBoxSolvable.Items.Add(psb);
         }
      }

      private void PopulateParameterStreamList(EvaporationAndDryingSystem system)
      {
         this.solvableVariablesControlParameter.ProcessVarListType = ProcessVariableListType.Specified;
         IEnumerator e = system.GetStreamList().GetEnumerator();
         while (e.MoveNext())
         {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.solvableVariablesControlParameter.ListBoxSolvable.Items.Add(psb);
         }
      }

      private void UpdateTheUI()
      {
         this.textBoxParameterSelected.Visible = this.checkBoxIncludeParamVar.Checked;
         this.labelParameterSelected.Visible = this.checkBoxIncludeParamVar.Checked;
         if (this.checkBoxIncludeParamVar.Checked)
         {
            this.tabControl.Controls.Add(this.tabPageParameter);
         }
         else
         {
            this.tabControl.Controls.Remove(this.tabPageParameter);
            this.textBoxParameterSelected.Text = "";
         }
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.p2dCache.FinishSpecifications(this.textBoxName.Text);    
         if (error != null)
         {
            UI.ShowError(error);
         }
         else
            this.Close();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }

      private void checkBoxIncludeParamVar_CheckedChanged(object sender, System.EventArgs e)
      {
         this.p2dCache.IncludeParameterVariable = this.checkBoxIncludeParamVar.Checked;
         this.UpdateTheUI();
      }

      private void buttonSetProcessVariable_Click(object sender, System.EventArgs e)
      {
         ProcessVarDouble pVar = null;
         if (this.tabControl.SelectedIndex == 0)
         {
            pVar = (ProcessVarDouble)this.solvableVariablesControlIndependentVar.GetSelectedProcessVar();
            if (pVar != null)
            {
               this.p2dCache.XVar = pVar;
               this.textBoxIndependentVarSelected.Text = this.solvableVariablesControlIndependentVar.GetSelectedProcessVar().Name;
            }
         }
         else if (this.tabControl.SelectedIndex == 1)
         {
            pVar = (ProcessVarDouble)this.solvableVariablesControlDependentVar.GetSelectedProcessVar();
            if (pVar != null)
            {
               this.p2dCache.YVar = pVar;
               this.textBoxDependentVarSelected.Text = this.solvableVariablesControlDependentVar.GetSelectedProcessVar().Name;
            }
         }
         else if (this.tabControl.SelectedIndex == 2)
         {
            pVar = (ProcessVarDouble)this.solvableVariablesControlParameter.GetSelectedProcessVar();
            if (pVar != null)
            {
               this.p2dCache.PVar = pVar;
               this.textBoxParameterSelected.Text = this.solvableVariablesControlParameter.GetSelectedProcessVar().Name;
            }
         }
      }
	}
}
