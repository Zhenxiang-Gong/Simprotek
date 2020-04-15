using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingRectangularLabelsControl.
	/// </summary>
	public class DryerScopingRectangularLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 292;
      public const int HEIGHT = 100;
      private ProsimoUI.ProcessVarLabel labelWidth;
      private ProsimoUI.ProcessVarLabel labelLength;
      private ProsimoUI.ProcessVarLabel labelHeight;
      private ProsimoUI.ProcessVarLabel labelLengthWidthRatio;
      private ProsimoUI.ProcessVarLabel labelHeightWidthRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingRectangularLabelsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
         this.labelLengthWidthRatio = new ProsimoUI.ProcessVarLabel();
         this.labelWidth = new ProsimoUI.ProcessVarLabel();
         this.labelLength = new ProsimoUI.ProcessVarLabel();
         this.labelHeight = new ProsimoUI.ProcessVarLabel();
         this.labelHeightWidthRatio = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelLengthWidthRatio
         // 
         this.labelLengthWidthRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLengthWidthRatio.Location = new System.Drawing.Point(0, 60);
         this.labelLengthWidthRatio.Name = "labelLengthWidthRatio";
         this.labelLengthWidthRatio.Size = new System.Drawing.Size(292, 20);
         this.labelLengthWidthRatio.TabIndex = 2;
         this.labelLengthWidthRatio.Text = "LengthWidthRatio";
         this.labelLengthWidthRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelWidth
         // 
         this.labelWidth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWidth.Location = new System.Drawing.Point(0, 0);
         this.labelWidth.Name = "labelWidth";
         this.labelWidth.Size = new System.Drawing.Size(292, 20);
         this.labelWidth.TabIndex = 6;
         this.labelWidth.Text = "Width";
         this.labelWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelLength
         // 
         this.labelLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLength.Location = new System.Drawing.Point(0, 20);
         this.labelLength.Name = "labelLength";
         this.labelLength.Size = new System.Drawing.Size(292, 20);
         this.labelLength.TabIndex = 8;
         this.labelLength.Text = "Length";
         this.labelLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHeight
         // 
         this.labelHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeight.Location = new System.Drawing.Point(0, 40);
         this.labelHeight.Name = "labelHeight";
         this.labelHeight.Size = new System.Drawing.Size(292, 20);
         this.labelHeight.TabIndex = 10;
         this.labelHeight.Text = "Height";
         this.labelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHeightWidthRatio
         // 
         this.labelHeightWidthRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeightWidthRatio.Location = new System.Drawing.Point(0, 80);
         this.labelHeightWidthRatio.Name = "labelHeightWidthRatio";
         this.labelHeightWidthRatio.Size = new System.Drawing.Size(292, 20);
         this.labelHeightWidthRatio.TabIndex = 12;
         this.labelHeightWidthRatio.Text = "HeightWidthRatio";
         this.labelHeightWidthRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DryerScopingRectangularLabelsControl
         // 
         this.Controls.Add(this.labelHeightWidthRatio);
         this.Controls.Add(this.labelHeight);
         this.Controls.Add(this.labelLength);
         this.Controls.Add(this.labelWidth);
         this.Controls.Add(this.labelLengthWidthRatio);
         this.Name = "DryerScopingRectangularLabelsControl";
         this.Size = new System.Drawing.Size(292, 100);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(DryerScopingModel scoping)
      {
         this.labelWidth.InitializeVariable(scoping.Width);
         this.labelLength.InitializeVariable(scoping.Length);
         this.labelHeight.InitializeVariable(scoping.Height);
         this.labelLengthWidthRatio.InitializeVariable(scoping.LengthWidthRatio);
         this.labelHeightWidthRatio.InitializeVariable(scoping.HeightWidthRatio);
      }
	}
}
