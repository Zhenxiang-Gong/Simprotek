using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for GasStreamEditor.
   /// </summary>
   public class GasStreamEditor : ProcessStreamBaseEditor {
      public GasStreamControl GasStreamCtrl {
         get { return (GasStreamControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public GasStreamEditor(GasStreamControl gasStreamCtrl)
         : base(gasStreamCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         GasStreamLabelsControl gasLabelsCtrl = new GasStreamLabelsControl(this.GasStreamCtrl.GasStream);
         this.panel.Controls.Add(gasLabelsCtrl);
         gasLabelsCtrl.Location = new Point(0, 24);

         GasStreamValuesControl gasValuesCtrl = new GasStreamValuesControl(this.GasStreamCtrl);
         this.panel.Controls.Add(gasValuesCtrl);
         gasValuesCtrl.Location = new Point(192, 24);

         this.Text = "Gas Stream: " + this.solvableCtrl.Solvable.Name;
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
         // 
         // GasStreamEditor
         // 
         this.Name = "GasStreamEditor";
         this.Text = "Gas Stream";
         this.ClientSize = new System.Drawing.Size(274, 287);
      }
      #endregion
   }
}
