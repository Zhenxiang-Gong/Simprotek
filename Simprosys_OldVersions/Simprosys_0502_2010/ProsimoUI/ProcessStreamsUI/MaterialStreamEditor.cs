using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI;
using Prosimo.Materials;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for MaterialStreamEditor.
   /// </summary>
   public class MaterialStreamEditor : SolvableEditor {
      public MaterialStreamControl MaterialStreamCtrl {
         get { return (MaterialStreamControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public MaterialStreamEditor(MaterialStreamControl materialStreamCtrl)
         : base(materialStreamCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         MaterialStreamLabelsControl materialLabelsCtrl = new MaterialStreamLabelsControl(this.MaterialStreamCtrl.MaterialStream);
         //ArrayList varList = this.MaterialStreamCtrl.MaterialStream.VarList;
         //ProcessVarLabelsControl materialLabelsCtrl = new ProcessVarLabelsControl(varList);
         this.panel.Controls.Add(materialLabelsCtrl);
         materialLabelsCtrl.Location = new Point(0, 24);

         MaterialStreamValuesControl materialValuesCtrl = new MaterialStreamValuesControl(this.MaterialStreamCtrl);
         //ProcessVarValuesControl materialValuesCtrl = new ProcessVarValuesControl(varList, MaterialStreamCtrl.Flowsheet.ApplicationPrefs);
         this.panel.Controls.Add(materialValuesCtrl);
         materialValuesCtrl.Location = new Point(192, 24);

         if (this.MaterialStreamCtrl.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
            this.Text = "Material Stream (Liquid): " + this.solvableCtrl.Solvable.Name;
         else if (this.MaterialStreamCtrl.MaterialStream.MaterialStateType == MaterialStateType.Solid)
            this.Text = "Material Stream (Solid): " + this.solvableCtrl.Solvable.Name;
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
         // MaterialStreamEditor
         // 
         this.Name = "MaterialStreamEditor";
         this.Text = "Material Stream";
         this.ClientSize = new System.Drawing.Size(274, 307);
      }
      #endregion

   }
}
