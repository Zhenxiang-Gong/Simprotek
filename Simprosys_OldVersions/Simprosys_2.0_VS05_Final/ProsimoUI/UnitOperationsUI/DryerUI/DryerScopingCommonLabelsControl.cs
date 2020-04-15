using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingCommonLabelsControl.
	/// </summary>
	public class DryerScopingCommonLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 292;
      public const int HEIGHT = 20;
      private ProsimoUI.ProcessVarLabel labelGasVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingCommonLabelsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public DryerScopingCommonLabelsControl(DryerScopingModel scoping)
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
         this.labelGasVelocity = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelGasVelocity
         // 
         this.labelGasVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelGasVelocity.Location = new System.Drawing.Point(0, 0);
         this.labelGasVelocity.Name = "labelGasVelocity";
         this.labelGasVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelGasVelocity.TabIndex = 3;
         this.labelGasVelocity.Text = "GasVelocity";
         this.labelGasVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DryerScopingCommonLabelsControl
         // 
         this.Controls.Add(this.labelGasVelocity);
         this.Name = "DryerScopingCommonLabelsControl";
         this.Size = new System.Drawing.Size(292, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(DryerScopingModel scoping)
      {
         this.labelGasVelocity.InitializeVariable(scoping.GasVelocity);
      }
	}
}
