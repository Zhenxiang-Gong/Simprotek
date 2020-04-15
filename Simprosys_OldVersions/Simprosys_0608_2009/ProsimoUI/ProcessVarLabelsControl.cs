using System.Collections;
using Prosimo;
using Prosimo.UnitOperations;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for ProcessStreamLabelsControl.
   /// </summary>
   public class ProcessVarLabelsControl : System.Windows.Forms.UserControl {
      // these need to be in sync with the dimensions of this control
      //public const int WIDTH = 192;
      //public const int HEIGHT = 240;

      //private ArrayList varList;
      private ProcessVarLabel[] varLabels;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcessVarLabelsControl() {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public ProcessVarLabelsControl(ArrayList varList) {
         InitializeVarLabels(varList);
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

      #region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.Size = new System.Drawing.Size(192, 120);
      }

      #endregion

      public void InitializeVarLabels(ArrayList varList) {
         this.varLabels = new ProcessVarLabel[varList.Count];
         this.SuspendLayout();
         ProcessVarLabel varLabel;
         for (int i = 0; i < varLabels.Length; i++) {
            varLabel = new ProcessVarLabel();
            varLabels[i] = varLabel;
            varLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            varLabel.Location = new System.Drawing.Point(0, i * 20);
            //varLabel.Name = "label" + ((ProcessVar)varList[i]).Name;
            varLabel.Size = new System.Drawing.Size(192, 20);
            varLabel.TabIndex = 100 + i;
            //varLabel.Text = "VOLUME_FLOW_RATE";
            varLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            varLabel.InitializeVariable((ProcessVar)varList[i]);
            this.Controls.Add(varLabel);
         }
         this.Size = new System.Drawing.Size(192, varLabels.Length * 20);

         this.ResumeLayout(false);
      }
   }
}
