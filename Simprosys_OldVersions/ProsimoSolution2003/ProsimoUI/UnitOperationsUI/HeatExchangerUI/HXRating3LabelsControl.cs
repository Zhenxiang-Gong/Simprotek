using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRating3LabelsControl.
	/// </summary>
	public class HXRating3LabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 80;
      private ProsimoUI.ProcessVarLabel labelTubeSideEntranceNozzleDiameter;
      private ProsimoUI.ProcessVarLabel labelTubeSideExitNozzleDiameter;
      private ProsimoUI.ProcessVarLabel labelShellSideEntranceNozzleDiameter;
      private ProsimoUI.ProcessVarLabel labelShellSideExitNozzleDiameter;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRating3LabelsControl()
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
         this.labelShellSideEntranceNozzleDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelTubeSideEntranceNozzleDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelTubeSideExitNozzleDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelShellSideExitNozzleDiameter = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelShellSideEntranceNozzleDiameter
         // 
         this.labelShellSideEntranceNozzleDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellSideEntranceNozzleDiameter.Location = new System.Drawing.Point(0, 40);
         this.labelShellSideEntranceNozzleDiameter.Name = "labelShellSideEntranceNozzleDiameter";
         this.labelShellSideEntranceNozzleDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelShellSideEntranceNozzleDiameter.TabIndex = 26;
         this.labelShellSideEntranceNozzleDiameter.Text = "ShellSideEntranceNozzleDiameter";
         this.labelShellSideEntranceNozzleDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeSideEntranceNozzleDiameter
         // 
         this.labelTubeSideEntranceNozzleDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeSideEntranceNozzleDiameter.Location = new System.Drawing.Point(0, 0);
         this.labelTubeSideEntranceNozzleDiameter.Name = "labelTubeSideEntranceNozzleDiameter";
         this.labelTubeSideEntranceNozzleDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelTubeSideEntranceNozzleDiameter.TabIndex = 25;
         this.labelTubeSideEntranceNozzleDiameter.Text = "TubeSideEntranceNozzleDiameter";
         this.labelTubeSideEntranceNozzleDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeSideExitNozzleDiameter
         // 
         this.labelTubeSideExitNozzleDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeSideExitNozzleDiameter.Location = new System.Drawing.Point(0, 20);
         this.labelTubeSideExitNozzleDiameter.Name = "labelTubeSideExitNozzleDiameter";
         this.labelTubeSideExitNozzleDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelTubeSideExitNozzleDiameter.TabIndex = 27;
         this.labelTubeSideExitNozzleDiameter.Text = "TubeSideExitNozzleDiameter";
         this.labelTubeSideExitNozzleDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelShellSideExitNozzleDiameter
         // 
         this.labelShellSideExitNozzleDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellSideExitNozzleDiameter.Location = new System.Drawing.Point(0, 60);
         this.labelShellSideExitNozzleDiameter.Name = "labelShellSideExitNozzleDiameter";
         this.labelShellSideExitNozzleDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelShellSideExitNozzleDiameter.TabIndex = 28;
         this.labelShellSideExitNozzleDiameter.Text = "ShellSideExitNozzleDiameter";
         this.labelShellSideExitNozzleDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRating3LabelsControl
         // 
         this.Controls.Add(this.labelShellSideExitNozzleDiameter);
         this.Controls.Add(this.labelTubeSideExitNozzleDiameter);
         this.Controls.Add(this.labelShellSideEntranceNozzleDiameter);
         this.Controls.Add(this.labelTubeSideEntranceNozzleDiameter);
         this.Name = "HXRating3LabelsControl";
         this.Size = new System.Drawing.Size(292, 80);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModelShellAndTube rating)
      {
         this.labelTubeSideEntranceNozzleDiameter.InitializeVariable(rating.TubeSideEntranceNozzleDiameter);
         this.labelTubeSideExitNozzleDiameter.InitializeVariable(rating.TubeSideExitNozzleDiameter);
         this.labelShellSideEntranceNozzleDiameter.InitializeVariable(rating.ShellSideEntranceNozzleDiameter);
         this.labelShellSideExitNozzleDiameter.InitializeVariable(rating.ShellSideExitNozzleDiameter);
      }
	}
}
