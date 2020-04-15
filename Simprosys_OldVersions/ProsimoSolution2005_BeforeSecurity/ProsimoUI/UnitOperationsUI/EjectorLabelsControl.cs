using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for EjectorLabelsControl.
   /// </summary>
   public class EjectorLabelsControl : System.Windows.Forms.UserControl
   {
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 60;

      private ProcessVarLabel labelEntrainmentRatio;
      private ProcessVarLabel labelCompressionRatio;
      private ProcessVarLabel labelSuctionMotivePressureRatio;

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public EjectorLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public EjectorLabelsControl(Ejector uo) : this()
      {
         this.InitializeVariableLabels(uo);
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
         this.labelEntrainmentRatio = new ProsimoUI.ProcessVarLabel();
         this.labelCompressionRatio = new ProsimoUI.ProcessVarLabel();
         this.labelSuctionMotivePressureRatio = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelEntrainmentRatio
         // 
         this.labelEntrainmentRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEntrainmentRatio.Location = new System.Drawing.Point(0, 0);
         this.labelEntrainmentRatio.Name = "labelEntrainmentRatio";
         this.labelEntrainmentRatio.Size = new System.Drawing.Size(192, 20);
         this.labelEntrainmentRatio.TabIndex = 96;
         this.labelEntrainmentRatio.Text = "EntrainmentRatio";
         this.labelEntrainmentRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCompressionRatio
         // 
         this.labelCompressionRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCompressionRatio.Location = new System.Drawing.Point(0, 20);
         this.labelCompressionRatio.Name = "labelCompressionRatio";
         this.labelCompressionRatio.Size = new System.Drawing.Size(192, 20);
         this.labelCompressionRatio.TabIndex = 92;
         this.labelCompressionRatio.Text = "CompressionRatio";
         this.labelCompressionRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSuctionMotivePressureRatio
         // 
         this.labelSuctionMotivePressureRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSuctionMotivePressureRatio.Location = new System.Drawing.Point(0, 40);
         this.labelSuctionMotivePressureRatio.Name = "labelSuctionMotivePressureRatio";
         this.labelSuctionMotivePressureRatio.Size = new System.Drawing.Size(192, 20);
         this.labelSuctionMotivePressureRatio.TabIndex = 97;
         this.labelSuctionMotivePressureRatio.Text = "SuctionMotivePressureRatio";
         this.labelSuctionMotivePressureRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // EjectorLabelsControl
         // 
         this.Controls.Add(this.labelEntrainmentRatio);
         this.Controls.Add(this.labelCompressionRatio);
         this.Controls.Add(this.labelSuctionMotivePressureRatio);
         this.Name = "EjectorLabelsControl";
         this.Size = new System.Drawing.Size(192, 60);
         this.ResumeLayout(false);

      }
      #endregion

      public void InitializeVariableLabels(Ejector uo)
      {
         this.labelEntrainmentRatio.InitializeVariable(uo.EntrainmentRatio);
         this.labelCompressionRatio.InitializeVariable(uo.CompressionRatio);
         this.labelSuctionMotivePressureRatio.InitializeVariable(uo.SuctionMotivePressureRatio);
      }
   }
}
