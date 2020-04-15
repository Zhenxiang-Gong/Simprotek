using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for ProcessStreamBaseEditor.
   /// </summary>
   public class ProcessStreamBaseEditor : SolvableEditor {
      public ProcessStreamBaseControl ProcessStreamBaseCtrl {
         get { return (ProcessStreamBaseControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcessStreamBaseEditor() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public ProcessStreamBaseEditor(ProcessStreamBaseControl processStreamBaseCtrl)
         : base(processStreamBaseCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         ArrayList varList = processStreamBaseCtrl.ProcessStreamBase.VarList;

         ProcessVarLabelsControl gasLabelsCtrl = new ProcessVarLabelsControl(varList);
         this.panel.Controls.Add(gasLabelsCtrl);
         gasLabelsCtrl.Location = new Point(0, 24);

         ProcessVarValuesControl gasValuesCtrl = new ProcessVarValuesControl(processStreamBaseCtrl);
         this.panel.Controls.Add(gasValuesCtrl);
         gasValuesCtrl.Location = new Point(192, 24);

         this.Text = processStreamBaseCtrl.Name;
         this.ClientSize = new System.Drawing.Size(274, varList.Count * 20 + 47);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessStreamBaseEditor));
         this.SuspendLayout();
         // 
         // ProcessStreamBaseEditor
         // 
         //this.ClientSize = new System.Drawing.Size(292, 266);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProcessStreamBaseEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         //this.Text = "ProcessStreamBaseEditor";
         this.ResumeLayout(false);

      }
      #endregion
   }
}
