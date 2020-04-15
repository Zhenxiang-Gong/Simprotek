using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanRectangularLabelsControl.
	/// </summary>
	public class FanRectangularLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 60;
      private ProsimoUI.ProcessVarLabel labelOutletWidth;
      private ProsimoUI.ProcessVarLabel labelOutletHeight;
      private ProsimoUI.ProcessVarLabel labelOutletHeightWidthRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanRectangularLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanRectangularLabelsControl(Fan uo) : this()
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
         this.labelOutletWidth = new ProsimoUI.ProcessVarLabel();
         this.labelOutletHeight = new ProsimoUI.ProcessVarLabel();
         this.labelOutletHeightWidthRatio = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelOutletWidth
         // 
         this.labelOutletWidth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletWidth.Location = new System.Drawing.Point(0, 0);
         this.labelOutletWidth.Name = "labelOutletWidth";
         this.labelOutletWidth.Size = new System.Drawing.Size(192, 20);
         this.labelOutletWidth.TabIndex = 92;
         this.labelOutletWidth.Text = "OutletWidth";
         this.labelOutletWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletHeight
         // 
         this.labelOutletHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletHeight.Location = new System.Drawing.Point(0, 20);
         this.labelOutletHeight.Name = "labelOutletHeight";
         this.labelOutletHeight.Size = new System.Drawing.Size(192, 20);
         this.labelOutletHeight.TabIndex = 94;
         this.labelOutletHeight.Text = "OutletHeight";
         this.labelOutletHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletHeightWidthRatio
         // 
         this.labelOutletHeightWidthRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletHeightWidthRatio.Location = new System.Drawing.Point(0, 40);
         this.labelOutletHeightWidthRatio.Name = "labelOutletHeightWidthRatio";
         this.labelOutletHeightWidthRatio.Size = new System.Drawing.Size(192, 20);
         this.labelOutletHeightWidthRatio.TabIndex = 93;
         this.labelOutletHeightWidthRatio.Text = "OutletHeightWidthRatio";
         this.labelOutletHeightWidthRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FanRectangularLabelsControl
         // 
         this.Controls.Add(this.labelOutletWidth);
         this.Controls.Add(this.labelOutletHeight);
         this.Controls.Add(this.labelOutletHeightWidthRatio);
         this.Name = "FanRectangularLabelsControl";
         this.Size = new System.Drawing.Size(192, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Fan uo)
      {
         this.labelOutletWidth.InitializeVariable(uo.OutletWidth);
         this.labelOutletHeight.InitializeVariable(uo.OutletHeight);
         this.labelOutletHeightWidthRatio.InitializeVariable(uo.OutletHeightWidthRatio);
      }
   }
}
