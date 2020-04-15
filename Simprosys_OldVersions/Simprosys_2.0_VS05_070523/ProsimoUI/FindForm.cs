using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for FindForm.
	/// </summary>
	public class FindForm : System.Windows.Forms.Form
	{
      private Flowsheet flowsheet;

      private System.Windows.Forms.Label labelFindWhat;
      private System.Windows.Forms.TextBox textBoxFindWhat;
      private System.Windows.Forms.CheckBox checkBoxMatchCase;
      private System.Windows.Forms.GroupBox groupBoxSearch;
      private System.Windows.Forms.RadioButton radioButtonAll;
      private System.Windows.Forms.RadioButton radioButtonUnitOps;
      private System.Windows.Forms.RadioButton radioButtonStreams;
      private System.Windows.Forms.Button buttonFind;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public FindForm(Flowsheet flowsheet)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.flowsheet = flowsheet;
         this.flowsheet.SetSolvableControlsSelection(false);
         this.flowsheet.ResetActivity();
         this.ResizeEnd += new EventHandler(FindForm_ResizeEnd);
      }

      void FindForm_ResizeEnd(object sender, EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
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
         this.labelFindWhat = new System.Windows.Forms.Label();
         this.textBoxFindWhat = new System.Windows.Forms.TextBox();
         this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
         this.groupBoxSearch = new System.Windows.Forms.GroupBox();
         this.radioButtonStreams = new System.Windows.Forms.RadioButton();
         this.radioButtonUnitOps = new System.Windows.Forms.RadioButton();
         this.radioButtonAll = new System.Windows.Forms.RadioButton();
         this.buttonFind = new System.Windows.Forms.Button();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxSearch.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelFindWhat
         // 
         this.labelFindWhat.Location = new System.Drawing.Point(8, 16);
         this.labelFindWhat.Name = "labelFindWhat";
         this.labelFindWhat.Size = new System.Drawing.Size(68, 16);
         this.labelFindWhat.TabIndex = 0;
         this.labelFindWhat.Text = "Find what:";
         this.labelFindWhat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxFindWhat
         // 
         this.textBoxFindWhat.Location = new System.Drawing.Point(80, 16);
         this.textBoxFindWhat.Name = "textBoxFindWhat";
         this.textBoxFindWhat.Size = new System.Drawing.Size(200, 20);
         this.textBoxFindWhat.TabIndex = 1;
         this.textBoxFindWhat.Text = "";
         this.textBoxFindWhat.WordWrap = false;
         this.textBoxFindWhat.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFindWhat_Validating);
         this.textBoxFindWhat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFindWhat_KeyUp);
         // 
         // checkBoxMatchCase
         // 
         this.checkBoxMatchCase.Checked = true;
         this.checkBoxMatchCase.CheckState = System.Windows.Forms.CheckState.Checked;
         this.checkBoxMatchCase.Location = new System.Drawing.Point(8, 40);
         this.checkBoxMatchCase.Name = "checkBoxMatchCase";
         this.checkBoxMatchCase.Size = new System.Drawing.Size(92, 20);
         this.checkBoxMatchCase.TabIndex = 4;
         this.checkBoxMatchCase.Text = "Match case";
         // 
         // groupBoxSearch
         // 
         this.groupBoxSearch.Controls.Add(this.radioButtonStreams);
         this.groupBoxSearch.Controls.Add(this.radioButtonUnitOps);
         this.groupBoxSearch.Controls.Add(this.radioButtonAll);
         this.groupBoxSearch.Location = new System.Drawing.Point(108, 40);
         this.groupBoxSearch.Name = "groupBoxSearch";
         this.groupBoxSearch.Size = new System.Drawing.Size(168, 80);
         this.groupBoxSearch.TabIndex = 3;
         this.groupBoxSearch.TabStop = false;
         this.groupBoxSearch.Text = "Search";
         // 
         // radioButtonStreams
         // 
         this.radioButtonStreams.Location = new System.Drawing.Point(8, 60);
         this.radioButtonStreams.Name = "radioButtonStreams";
         this.radioButtonStreams.Size = new System.Drawing.Size(156, 16);
         this.radioButtonStreams.TabIndex = 2;
         this.radioButtonStreams.Text = "Streams";
         // 
         // radioButtonUnitOps
         // 
         this.radioButtonUnitOps.Location = new System.Drawing.Point(8, 40);
         this.radioButtonUnitOps.Name = "radioButtonUnitOps";
         this.radioButtonUnitOps.Size = new System.Drawing.Size(156, 16);
         this.radioButtonUnitOps.TabIndex = 1;
         this.radioButtonUnitOps.Text = "Unit Operations";
         // 
         // radioButtonAll
         // 
         this.radioButtonAll.Checked = true;
         this.radioButtonAll.Location = new System.Drawing.Point(8, 20);
         this.radioButtonAll.Name = "radioButtonAll";
         this.radioButtonAll.Size = new System.Drawing.Size(156, 16);
         this.radioButtonAll.TabIndex = 0;
         this.radioButtonAll.TabStop = true;
         this.radioButtonAll.Text = "All flowsheet elements";
         // 
         // buttonFind
         // 
         this.buttonFind.Location = new System.Drawing.Point(288, 16);
         this.buttonFind.Name = "buttonFind";
         this.buttonFind.TabIndex = 2;
         this.buttonFind.Text = "Find";
         this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
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
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.textBoxFindWhat);
         this.panel.Controls.Add(this.checkBoxMatchCase);
         this.panel.Controls.Add(this.groupBoxSearch);
         this.panel.Controls.Add(this.buttonFind);
         this.panel.Controls.Add(this.labelFindWhat);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(374, 133);
         this.panel.TabIndex = 5;
         // 
         // FindForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.AutoScroll = true;
         this.ClientSize = new System.Drawing.Size(374, 133);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "FindForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Find";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.FindForm_Closing);
         this.groupBoxSearch.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void Find()
      {
         string findStr = this.textBoxFindWhat.Text.Trim();
         if (findStr.Length > 0)
         {
            this.flowsheet.SetSolvableControlsSelection(false);
            this.flowsheet.ResetActivity();
            ArrayList solvables = null;

            if (this.radioButtonAll.Checked)
               solvables = this.flowsheet.EvaporationAndDryingSystem.GetSolvableList();
            else if (this.radioButtonStreams.Checked)
               solvables = this.flowsheet.EvaporationAndDryingSystem.GetStreamList();
            else if (this.radioButtonUnitOps.Checked)
               solvables = this.flowsheet.EvaporationAndDryingSystem.GetUnitOpList();
      
            IEnumerator en = solvables.GetEnumerator();
            while (en.MoveNext())
            {
               Solvable sol = en.Current as Solvable;
               string name = sol.Name;
               if (UI.StringContainsString(name, findStr, !this.checkBoxMatchCase.Checked))
               {
                  SolvableControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(name);
                  if (ctrl == null)
                     ctrl = this.flowsheet.UnitOpManager.GetUnitOpControl(name);
                  ctrl.IsSelected = true;
               }
            }
         }
      }

      private void buttonFind_Click(object sender, System.EventArgs e)
      {
         this.Find();
      }

      private void FindForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         this.flowsheet.FindBox = null;
      }

      private void textBoxFindWhat_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         this.Find();
      }

      private void textBoxFindWhat_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = this.buttonFind;
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
