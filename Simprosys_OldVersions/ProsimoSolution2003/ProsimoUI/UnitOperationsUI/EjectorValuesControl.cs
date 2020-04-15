using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for EjectorValuesControl.
   /// </summary>
   public class EjectorValuesControl : System.Windows.Forms.UserControl
   {
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 60;

      private EjectorControl ejectorCtrl;
      private ProsimoUI.ProcessVarTextBox textBoxEntrainmentRatio;
      private ProsimoUI.ProcessVarTextBox textBoxSuctionMotivePressureRatio;
      private ProsimoUI.ProcessVarTextBox textBoxCompressionRatio;
      
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public EjectorValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public EjectorValuesControl(EjectorControl ejectorCtrl) : this()
      {
         this.ejectorCtrl = ejectorCtrl;
         this.InitializeVariableTextBoxes(ejectorCtrl);
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
         this.textBoxEntrainmentRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSuctionMotivePressureRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCompressionRatio = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxEntrainmentRatio
         // 
         this.textBoxEntrainmentRatio.Location = new System.Drawing.Point(0, 0);
         this.textBoxEntrainmentRatio.Name = "textBoxEntrainmentRatio";
         this.textBoxEntrainmentRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxEntrainmentRatio.TabIndex = 1;
         this.textBoxEntrainmentRatio.Text = "";
         this.textBoxEntrainmentRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEntrainmentRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSuctionMotivePressureRatio
         // 
         this.textBoxSuctionMotivePressureRatio.Location = new System.Drawing.Point(0, 40);
         this.textBoxSuctionMotivePressureRatio.Name = "textBoxSuctionMotivePressureRatio";
         this.textBoxSuctionMotivePressureRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxSuctionMotivePressureRatio.TabIndex = 3;
         this.textBoxSuctionMotivePressureRatio.Text = "";
         this.textBoxSuctionMotivePressureRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxSuctionMotivePressureRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCompressionRatio
         // 
         this.textBoxCompressionRatio.Location = new System.Drawing.Point(0, 20);
         this.textBoxCompressionRatio.Name = "textBoxCompressionRatio";
         this.textBoxCompressionRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxCompressionRatio.TabIndex = 2;
         this.textBoxCompressionRatio.Text = "";
         this.textBoxCompressionRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCompressionRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // EjectorValuesControl
         // 
         this.Controls.Add(this.textBoxEntrainmentRatio);
         this.Controls.Add(this.textBoxSuctionMotivePressureRatio);
         this.Controls.Add(this.textBoxCompressionRatio);
         this.Name = "EjectorValuesControl";
         this.Size = new System.Drawing.Size(80, 60);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableTextBoxes(EjectorControl ctrl)
      {
         this.textBoxEntrainmentRatio.InitializeVariable(ctrl.Flowsheet, ctrl.Ejector.EntrainmentRatio);
         this.textBoxCompressionRatio.InitializeVariable(ctrl.Flowsheet, ctrl.Ejector.CompressionRatio);
         this.textBoxSuctionMotivePressureRatio.InitializeVariable(ctrl.Flowsheet, ctrl.Ejector.SuctionMotivePressureRatio);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxEntrainmentRatio);
         list.Add(this.textBoxCompressionRatio);
         list.Add(this.textBoxSuctionMotivePressureRatio);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
