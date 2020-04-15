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
	/// Summary description for DryerScopingCircularLabelsControl.
	/// </summary>
	public class DryerScopingCircularLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 292;
      public const int HEIGHT = 60;
      private ProsimoUI.ProcessVarLabel labelDiameter;
      private ProsimoUI.ProcessVarLabel labelLength;
      private ProsimoUI.ProcessVarLabel labelLengthDiameterRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingCircularLabelsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public DryerScopingCircularLabelsControl(DryerScopingModel scoping)
      {
         // NOTE: this constructor is not used
         InitializeComponent();
         this.InitializeVariableLabels(scoping);
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
         this.labelDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelLength = new ProsimoUI.ProcessVarLabel();
         this.labelLengthDiameterRatio = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelDiameter
         // 
         this.labelDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDiameter.Location = new System.Drawing.Point(0, 0);
         this.labelDiameter.Name = "labelDiameter";
         this.labelDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelDiameter.TabIndex = 6;
         this.labelDiameter.Text = "Diameter";
         this.labelDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
         // labelLengthDiameterRatio
         // 
         this.labelLengthDiameterRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelLengthDiameterRatio.Location = new System.Drawing.Point(0, 40);
         this.labelLengthDiameterRatio.Name = "labelLengthDiameterRatio";
         this.labelLengthDiameterRatio.Size = new System.Drawing.Size(292, 20);
         this.labelLengthDiameterRatio.TabIndex = 10;
         this.labelLengthDiameterRatio.Text = "LengthDiameterRatio";
         this.labelLengthDiameterRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DryerScopingCircularLabelsControl
         // 
         this.Controls.Add(this.labelLengthDiameterRatio);
         this.Controls.Add(this.labelLength);
         this.Controls.Add(this.labelDiameter);
         this.Name = "DryerScopingCircularLabelsControl";
         this.Size = new System.Drawing.Size(292, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(DryerScopingModel scoping)
      {
         this.labelDiameter.InitializeVariable(scoping.Diameter);
         this.labelLength.InitializeVariable(scoping.Length);
         this.labelLengthDiameterRatio.InitializeVariable(scoping.LengthDiameterRatio);
      }
	}
}
