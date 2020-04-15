using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for Toolbox.
   /// </summary>
   public class Toolbox : System.Windows.Forms.Form {
      private Flowsheet flowsheet;
      private RadioButton[] buttons;
      private IDictionary<RadioButton, Type> buttonTypeTable;

      private FlowLayoutPanel toolBoxPanel;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public Toolbox() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public Toolbox(MainForm mf, Flowsheet flowsheet) {
         this.Owner = mf;
         this.buttonTypeTable = new Dictionary<RadioButton, Type>();
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
         Initialization(flowsheet);
         //this.flowsheet.MouseDown += new MouseEventHandler(flowsheet_MouseDown);
         //this.LocationChanged += new EventHandler(Toolbox_LocationChanged);
         //this.flowsheet.ActivityChanged += new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
         this.ResizeEnd += new EventHandler(Toolbox_ResizeEnd);
      }

      private void Initialization(Flowsheet flowsheet) {
         this.flowsheet = flowsheet;
         this.buttonTypeTable.Clear();

         this.toolBoxPanel.SuspendLayout();
         this.SuspendLayout();

         ComponentResourceManager resources = new ComponentResourceManager(typeof(Toolbox));
         IList<Type> solvableTypes = flowsheet.EvaporationAndDryingSystem.GetSolvableTypeList();
         this.buttons = new RadioButton[solvableTypes.Count];
         RadioButton btn;
         Type solvableType;
         for (int i = 0; i < solvableTypes.Count; i++) {
            solvableType = solvableTypes[i];
            btn = new System.Windows.Forms.RadioButton();
            buttons[i] = btn;
            btn.Appearance = System.Windows.Forms.Appearance.Button;
            btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject(solvableType.Name)));
            btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            int remainder;
            int row = Math.DivRem(i, 2, out remainder);
            btn.Location = new System.Drawing.Point(remainder * 35, row * 35);
            btn.Margin = new System.Windows.Forms.Padding(0);
            btn.Name = solvableType.Name;
            btn.Size = new System.Drawing.Size(35, 35);
            btn.TabIndex = i;
            btn.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            this.toolBoxPanel.Controls.Add(btn);
            this.buttonTypeTable.Add(btn, solvableType);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btn, UI.SolvableNameTable[solvableType]);
         }

         this.toolBoxPanel.ResumeLayout(false);
         this.ResumeLayout(false);

         RegisterFlowSheetActivity(flowsheet);
      }

      private void RegisterFlowSheetActivity(Flowsheet fs) {
         if (this.flowsheet != null) {
            this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
         }
         this.flowsheet.ActivityChanged += new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
      }

      internal void SetFlowsheet(Flowsheet fs) {
         foreach (RadioButton b in buttons) {
            this.toolBoxPanel.Controls.Remove(b);
         }
         Initialization(fs);
      }

      private void Toolbox_ResizeEnd(object sender, EventArgs e) {
         if (this.flowsheet != null) {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.flowsheet != null)
            this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.toolBoxPanel = new System.Windows.Forms.FlowLayoutPanel();
         this.toolBoxPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolBoxPanel
         // 
         this.toolBoxPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

         this.toolBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolBoxPanel.Location = new System.Drawing.Point(0, 0);
         this.toolBoxPanel.Margin = new System.Windows.Forms.Padding(0);
         this.toolBoxPanel.Name = "toolBoxPanel";
         this.toolBoxPanel.Size = new System.Drawing.Size(73, 420);
         this.toolBoxPanel.TabIndex = 9;
         // 
         // Toolbox
         // 
         this.AutoScroll = true;
         this.ClientSize = new System.Drawing.Size(73, 420);
         this.Controls.Add(this.toolBoxPanel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "Toolbox";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Toolbox";
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbox_MouseUp);
         this.toolBoxPanel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      private void radioButton_CheckedChanged(object sender, System.EventArgs e) {
         RadioButton btn = sender as RadioButton;
         if (btn.Checked)
            this.flowsheet.AddSolvable(this.buttonTypeTable[btn]);
      }
      
      private void Toolbox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
         this.flowsheet.ResetActivity();
      }

      private void UncheckAllButtons() {
         IEnumerator e = this.toolBoxPanel.Controls.GetEnumerator();
         while (e.MoveNext()) {
            RadioButton rb = (RadioButton)e.Current;
            rb.Checked = false;
         }
      }

      private void flowsheet_ActivityChanged(Object sender, ActivityChangedEventArgs eventArgs) {
         if (eventArgs.FlowsheetActivity == FlowsheetActivity.Default ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.AddingConnStepOne ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.AddingConnStepTwo ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.DeletingConnection ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.SelectingSnapshot) {
            this.UncheckAllButtons();
         }
         else if (eventArgs.FlowsheetActivity == FlowsheetActivity.AddingSolvable) {
            RadioButton btn = GetRadioButton(eventArgs.SolvableType);
            if (btn != null && !btn.Checked) {
               btn.Checked = true;
            }
         }
      }

      private void panel_Click(object sender, System.EventArgs e) {
         this.flowsheet.ResetActivity();
      }

      private RadioButton GetRadioButton(Type solvableType) {
         RadioButton button = null;
         foreach (RadioButton btn in buttonTypeTable.Keys) {
            if (buttonTypeTable[btn] == solvableType) {
               button = btn;
            }
         }
         return button;
      }
   }
}


