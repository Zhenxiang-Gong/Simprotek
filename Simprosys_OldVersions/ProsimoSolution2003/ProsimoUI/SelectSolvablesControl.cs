using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI.UnitOperationsUI.TwoStream;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SelectSolvablesControl.
	/// </summary>
	public class SelectSolvablesControl : System.Windows.Forms.UserControl
	{
      private Flowsheet flowsheet;
      // this flag is set to true for individual update
      // and is set to false for bulk updates
      private bool updateFlag;

      public CheckedListBox StreamsCheckControl
      {
         get
         {
            return this.checkedListBoxStreams;
         }
      }

      public CheckedListBox UnitOpsCheckControl
      {
         get
         {
            return this.checkedListBoxUnitOps;
         }
      }

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxShow;
      private System.Windows.Forms.GroupBox groupBoxUnitOps;
      private System.Windows.Forms.Button buttonUnitOpsUncheck;
      private System.Windows.Forms.Button buttonUnitOpsCheck;
      private System.Windows.Forms.CheckedListBox checkedListBoxUnitOps;
      private System.Windows.Forms.GroupBox groupBoxStreams;
      private System.Windows.Forms.Button buttonStreamsUncheck;
      private System.Windows.Forms.Button buttonStreamsCheck;
      private System.Windows.Forms.CheckedListBox checkedListBoxStreams;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public SelectSolvablesControl()
      {
      }

		public SelectSolvablesControl(Flowsheet flowsheet)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.flowsheet = flowsheet;
         this.updateFlag = true;
         this.InitializeStreamList();
         this.InitializeUnitOpList();
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
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxShow = new System.Windows.Forms.GroupBox();
         this.groupBoxUnitOps = new System.Windows.Forms.GroupBox();
         this.buttonUnitOpsUncheck = new System.Windows.Forms.Button();
         this.buttonUnitOpsCheck = new System.Windows.Forms.Button();
         this.checkedListBoxUnitOps = new System.Windows.Forms.CheckedListBox();
         this.groupBoxStreams = new System.Windows.Forms.GroupBox();
         this.buttonStreamsUncheck = new System.Windows.Forms.Button();
         this.buttonStreamsCheck = new System.Windows.Forms.Button();
         this.checkedListBoxStreams = new System.Windows.Forms.CheckedListBox();
         this.panel.SuspendLayout();
         this.groupBoxShow.SuspendLayout();
         this.groupBoxUnitOps.SuspendLayout();
         this.groupBoxStreams.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxShow);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(388, 364);
         this.panel.TabIndex = 0;
         // 
         // groupBoxShow
         // 
         this.groupBoxShow.Controls.Add(this.groupBoxUnitOps);
         this.groupBoxShow.Controls.Add(this.groupBoxStreams);
         this.groupBoxShow.Location = new System.Drawing.Point(4, 8);
         this.groupBoxShow.Name = "groupBoxShow";
         this.groupBoxShow.Size = new System.Drawing.Size(380, 352);
         this.groupBoxShow.TabIndex = 1;
         this.groupBoxShow.TabStop = false;
         this.groupBoxShow.Text = "Show and Print";
         // 
         // groupBoxUnitOps
         // 
         this.groupBoxUnitOps.Controls.Add(this.buttonUnitOpsUncheck);
         this.groupBoxUnitOps.Controls.Add(this.buttonUnitOpsCheck);
         this.groupBoxUnitOps.Controls.Add(this.checkedListBoxUnitOps);
         this.groupBoxUnitOps.Location = new System.Drawing.Point(192, 24);
         this.groupBoxUnitOps.Name = "groupBoxUnitOps";
         this.groupBoxUnitOps.Size = new System.Drawing.Size(180, 320);
         this.groupBoxUnitOps.TabIndex = 1;
         this.groupBoxUnitOps.TabStop = false;
         this.groupBoxUnitOps.Text = "Unit Operations";
         // 
         // buttonUnitOpsUncheck
         // 
         this.buttonUnitOpsUncheck.Location = new System.Drawing.Point(92, 288);
         this.buttonUnitOpsUncheck.Name = "buttonUnitOpsUncheck";
         this.buttonUnitOpsUncheck.TabIndex = 4;
         this.buttonUnitOpsUncheck.Text = "None";
         this.buttonUnitOpsUncheck.Click += new System.EventHandler(this.buttonUnitOpsUncheck_Click);
         // 
         // buttonUnitOpsCheck
         // 
         this.buttonUnitOpsCheck.Location = new System.Drawing.Point(12, 288);
         this.buttonUnitOpsCheck.Name = "buttonUnitOpsCheck";
         this.buttonUnitOpsCheck.Size = new System.Drawing.Size(68, 23);
         this.buttonUnitOpsCheck.TabIndex = 3;
         this.buttonUnitOpsCheck.Text = "All";
         this.buttonUnitOpsCheck.Click += new System.EventHandler(this.buttonUnitOpsCheck_Click);
         // 
         // checkedListBoxUnitOps
         // 
         this.checkedListBoxUnitOps.CheckOnClick = true;
         this.checkedListBoxUnitOps.HorizontalScrollbar = true;
         this.checkedListBoxUnitOps.Location = new System.Drawing.Point(8, 20);
         this.checkedListBoxUnitOps.Name = "checkedListBoxUnitOps";
         this.checkedListBoxUnitOps.Size = new System.Drawing.Size(164, 259);
         this.checkedListBoxUnitOps.Sorted = true;
         this.checkedListBoxUnitOps.TabIndex = 0;
         this.checkedListBoxUnitOps.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxUnitOps_ItemCheck);
         // 
         // groupBoxStreams
         // 
         this.groupBoxStreams.Controls.Add(this.buttonStreamsUncheck);
         this.groupBoxStreams.Controls.Add(this.buttonStreamsCheck);
         this.groupBoxStreams.Controls.Add(this.checkedListBoxStreams);
         this.groupBoxStreams.Location = new System.Drawing.Point(8, 24);
         this.groupBoxStreams.Name = "groupBoxStreams";
         this.groupBoxStreams.Size = new System.Drawing.Size(180, 320);
         this.groupBoxStreams.TabIndex = 0;
         this.groupBoxStreams.TabStop = false;
         this.groupBoxStreams.Text = "Streams";
         // 
         // buttonStreamsUncheck
         // 
         this.buttonStreamsUncheck.Location = new System.Drawing.Point(92, 288);
         this.buttonStreamsUncheck.Name = "buttonStreamsUncheck";
         this.buttonStreamsUncheck.TabIndex = 2;
         this.buttonStreamsUncheck.Text = "None";
         this.buttonStreamsUncheck.Click += new System.EventHandler(this.buttonStreamsUncheck_Click);
         // 
         // buttonStreamsCheck
         // 
         this.buttonStreamsCheck.Location = new System.Drawing.Point(12, 288);
         this.buttonStreamsCheck.Name = "buttonStreamsCheck";
         this.buttonStreamsCheck.Size = new System.Drawing.Size(68, 23);
         this.buttonStreamsCheck.TabIndex = 1;
         this.buttonStreamsCheck.Text = "All";
         this.buttonStreamsCheck.Click += new System.EventHandler(this.buttonStreamsCheck_Click);
         // 
         // checkedListBoxStreams
         // 
         this.checkedListBoxStreams.CheckOnClick = true;
         this.checkedListBoxStreams.HorizontalScrollbar = true;
         this.checkedListBoxStreams.Location = new System.Drawing.Point(8, 20);
         this.checkedListBoxStreams.Name = "checkedListBoxStreams";
         this.checkedListBoxStreams.Size = new System.Drawing.Size(164, 259);
         this.checkedListBoxStreams.Sorted = true;
         this.checkedListBoxStreams.TabIndex = 0;
         this.checkedListBoxStreams.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxStreams_ItemCheck);
         // 
         // SelectSolvablesControl
         // 
         this.Controls.Add(this.panel);
         this.Name = "SelectSolvablesControl";
         this.Size = new System.Drawing.Size(388, 364);
         this.panel.ResumeLayout(false);
         this.groupBoxShow.ResumeLayout(false);
         this.groupBoxUnitOps.ResumeLayout(false);
         this.groupBoxStreams.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void InitializeStreamList()
      {
         this.updateFlag = false;
         IList list = this.flowsheet.StreamManager.GetStreamControls();
         IEnumerator e = list.GetEnumerator();
         while (e.MoveNext())
         {
            SolvableControl ctrl = (SolvableControl)e.Current;
            string s = ctrl.Solvable.Name;
            this.checkedListBoxStreams.Items.Add(s);
            int i = this.checkedListBoxStreams.Items.IndexOf(s);
            this.checkedListBoxStreams.SetItemChecked(i, ctrl.IsShownInEditor);
         }
         this.updateFlag = true;
      }

      private void InitializeUnitOpList()
      {
         this.updateFlag = false;
         IList list = this.flowsheet.UnitOpManager.GetUnitOpControls();
         IEnumerator e = list.GetEnumerator();
         while (e.MoveNext())
         {
            SolvableControl ctrl = (SolvableControl)e.Current;
            if (!(ctrl is RecycleControl))
            {
               string s = ctrl.Solvable.Name;
               this.checkedListBoxUnitOps.Items.Add(s);
               int i = this.checkedListBoxUnitOps.Items.IndexOf(s);
               this.checkedListBoxUnitOps.SetItemChecked(i, ctrl.IsShownInEditor);
            }
         }
         this.updateFlag = true;
      }

      private void buttonStreamsCheck_Click(object sender, System.EventArgs e)
      {
         this.SetCheckStateForList(this.checkedListBoxStreams, true);
      }

      private void buttonStreamsUncheck_Click(object sender, System.EventArgs e)
      {
         this.SetCheckStateForList(this.checkedListBoxStreams, false);
      }

      private void buttonUnitOpsCheck_Click(object sender, System.EventArgs e)
      {
         this.SetCheckStateForList(this.checkedListBoxUnitOps, true);
      }

      private void buttonUnitOpsUncheck_Click(object sender, System.EventArgs e)
      {
         this.SetCheckStateForList(this.checkedListBoxUnitOps, false);
      }

      private void SetCheckStateForList(CheckedListBox list, bool state)
      {
         // bulk update
         this.updateFlag = false;
         int size = list.Items.Count;
         for (int i=0; i<size; i++)
         {
            list.SetItemChecked(i, state);
         }

         // the checkboxes have been modified in bulk, so update now the editor
         if (this.flowsheet != null)
         {
            if (this.flowsheet.Editor != null)
            {
               if (list == this.checkedListBoxStreams)
                  this.flowsheet.Editor.UpdateStreamsUI();
               else if (list == this.checkedListBoxUnitOps)
                  this.flowsheet.Editor.UpdateUnitOpsUI();
            }
         }

         this.updateFlag = true;
      }

      private void checkedListBoxStreams_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         // individual update
         bool isChecked = false;
         if (e.NewValue.Equals(CheckState.Checked))
            isChecked = true;
         int idx = e.Index;
         string s = (string)this.checkedListBoxStreams.Items[idx];
         this.flowsheet.StreamManager.GetProcessStreamBaseControl(s).IsShownInEditor = isChecked;

         if (this.updateFlag)
         {
            if (this.flowsheet != null)
            {
               if (this.flowsheet.Editor != null)
                  this.flowsheet.Editor.UpdateStreamsUI();
            }
         }
      }

      private void checkedListBoxUnitOps_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
      {
         // individual update
         bool isChecked = false;
         if (e.NewValue.Equals(CheckState.Checked))
            isChecked = true;
         int idx = e.Index;
         string s = (string)this.checkedListBoxUnitOps.Items[idx];
         this.flowsheet.UnitOpManager.GetUnitOpControl(s).IsShownInEditor = isChecked;

         if (this.updateFlag)
         {
            if (this.flowsheet != null)
            {
               if (this.flowsheet.Editor != null)
                  this.flowsheet.Editor.UpdateUnitOpsUI();
            }
         }
      }
	}
}
