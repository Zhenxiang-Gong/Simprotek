using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRating2LabelsControl.
	/// </summary>
	public class HXRating2LabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 40;

      private ProsimoUI.ProcessVarLabel labelWallThickness;
      private ProsimoUI.ProcessVarLabel labelWallThermalConductivity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRating2LabelsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public HXRating2LabelsControl(HXRatingModel rating)
      {
         // NOTE: this constructor is not used
         InitializeComponent();
         this.InitializeVariableLabels(rating);
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
         this.labelWallThickness = new ProsimoUI.ProcessVarLabel();
         this.labelWallThermalConductivity = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelWallThickness
         // 
         this.labelWallThickness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWallThickness.Location = new System.Drawing.Point(0, 20);
         this.labelWallThickness.Name = "labelWallThickness";
         this.labelWallThickness.Size = new System.Drawing.Size(292, 20);
         this.labelWallThickness.TabIndex = 16;
         this.labelWallThickness.Text = "Wall Thickness";
         this.labelWallThickness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelWallThermalConductivity
         // 
         this.labelWallThermalConductivity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWallThermalConductivity.Location = new System.Drawing.Point(0, 0);
         this.labelWallThermalConductivity.Name = "labelWallThermalConductivity";
         this.labelWallThermalConductivity.Size = new System.Drawing.Size(292, 20);
         this.labelWallThermalConductivity.TabIndex = 17;
         this.labelWallThermalConductivity.Text = "Wall Thermal Conductivity";
         this.labelWallThermalConductivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRating2LabelsControl
         // 
         this.Controls.Add(this.labelWallThermalConductivity);
         this.Controls.Add(this.labelWallThickness);
         this.Name = "HXRating2LabelsControl";
         this.Size = new System.Drawing.Size(292, 40);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModel rating)
      {
         this.labelWallThermalConductivity.InitializeVariable(rating.WallThermalConductivity);
         this.labelWallThickness.InitializeVariable(rating.WallThickness);
      }
	}
}
