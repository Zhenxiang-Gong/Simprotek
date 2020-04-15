using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI;

namespace ProsimoUI.ProcVarsEditor
{
   /// <summary>
   /// Summary description for MinMaxProcVarHeaderControl.
   /// </summary>
   public class MinMaxProcVarHeaderControl : System.Windows.Forms.UserControl
   {
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.Label labelExistingValue;
      private System.Windows.Forms.Label labelNewValue;
      private System.Windows.Forms.Label labelMinValue;
      private System.Windows.Forms.Label labelMaxValue;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public MinMaxProcVarHeaderControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         // TODO: Add any initialization after the InitializeComponent call

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
         this.labelName = new System.Windows.Forms.Label();
         this.labelExistingValue = new System.Windows.Forms.Label();
         this.labelNewValue = new System.Windows.Forms.Label();
         this.labelMinValue = new System.Windows.Forms.Label();
         this.labelMaxValue = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelName
         // 
         this.labelName.BackColor = System.Drawing.Color.DarkGray;
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelName.Location = new System.Drawing.Point(0, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(192, 20);
         this.labelName.TabIndex = 0;
         this.labelName.Text = "Out of Range Variable";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelExistingValue
         // 
         this.labelExistingValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelExistingValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelExistingValue.Location = new System.Drawing.Point(192, 0);
         this.labelExistingValue.Name = "labelExistingValue";
         this.labelExistingValue.Size = new System.Drawing.Size(90, 20);
         this.labelExistingValue.TabIndex = 1;
         this.labelExistingValue.Text = "Existing Value";
         this.labelExistingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelNewValue
         // 
         this.labelNewValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelNewValue.Location = new System.Drawing.Point(462, 0);
         this.labelNewValue.Name = "labelNewValue";
         this.labelNewValue.Size = new System.Drawing.Size(90, 20);
         this.labelNewValue.TabIndex = 3;
         this.labelNewValue.Text = "New Value";
         this.labelNewValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelMinValue
         // 
         this.labelMinValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelMinValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelMinValue.Location = new System.Drawing.Point(282, 0);
         this.labelMinValue.Name = "labelMinValue";
         this.labelMinValue.Size = new System.Drawing.Size(90, 20);
         this.labelMinValue.TabIndex = 4;
         this.labelMinValue.Text = "Min. Value";
         this.labelMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelMaxValue
         // 
         this.labelMaxValue.BackColor = System.Drawing.Color.DarkGray;
         this.labelMaxValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelMaxValue.Location = new System.Drawing.Point(372, 0);
         this.labelMaxValue.Name = "labelMaxValue";
         this.labelMaxValue.Size = new System.Drawing.Size(90, 20);
         this.labelMaxValue.TabIndex = 5;
         this.labelMaxValue.Text = "Max. Value";
         this.labelMaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // MinMaxProcVarHeaderControl
         // 
         this.Controls.Add(this.labelMaxValue);
         this.Controls.Add(this.labelMinValue);
         this.Controls.Add(this.labelNewValue);
         this.Controls.Add(this.labelExistingValue);
         this.Controls.Add(this.labelName);
         this.Name = "MinMaxProcVarHeaderControl";
         this.Size = new System.Drawing.Size(554, 20);
         this.ResumeLayout(false);

      }
      #endregion
   }
}
