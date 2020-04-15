using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI.UnitSystemsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.Materials;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for CustomizeFlowsheetForm.
   /// </summary>
   public class CustomizeFlowsheetForm : System.Windows.Forms.Form {
      private Flowsheet flowsheet;
      private System.Windows.Forms.MainMenu mainMenu1;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private IContainer components;

      public CustomizeFlowsheetForm(Flowsheet flowsheet) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         if (flowsheet != null) {
            this.flowsheet = flowsheet;
            EvaporationAndDryingSystem system = this.flowsheet.EvaporationAndDryingSystem;

            //
            SelectSolvablesControl selectSolvablesControl = new SelectSolvablesControl(flowsheet);
            selectSolvablesControl.Location = new Point(4, 4);
            this.panel.Controls.Add(selectSolvablesControl);

            this.ResizeEnd += new EventHandler(CustomizeFlowsheetForm_ResizeEnd);
         }
      }

      void CustomizeFlowsheetForm_ResizeEnd(object sender, EventArgs e) {
         if (this.flowsheet != null) {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
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
         this.components = new System.ComponentModel.Container();
         this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.SuspendLayout();
         // 
         // mainMenu1
         // 
         this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(397, 377);
         this.panel.TabIndex = 5;
         // 
         // CustomizeFlowsheetForm
         // 
         this.ClientSize = new System.Drawing.Size(397, 377);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu1;
         this.MinimizeBox = false;
         this.Name = "CustomizeFlowsheetForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Customize Flowsheet Data";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.CustomizeFlowsheetForm_Closing);
         this.ResumeLayout(false);

      }
      #endregion

      private void menuItemClose_Click(object sender, System.EventArgs e) {
         this.Close();
         UpdateFlowsheetEditorUI();
      }

      private void CustomizeFlowsheetForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
         // nothing at this moment
         UpdateFlowsheetEditorUI();
      }

      private void UpdateFlowsheetEditorUI() {
         if (this.flowsheet != null && this.flowsheet.Editor != null) {
            this.flowsheet.Editor.UpdateStreamsUI();
            this.flowsheet.Editor.UpdateUnitOpsUI();
         }
      }
   }
}
