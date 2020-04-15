using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingSimpleGenericShellAndTubeLabelsControl.
	/// </summary>
	public class HXRatingSimpleGenericShellAndTubeLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 280;
      private ProsimoUI.ProcessVarLabel labelHotSideRe;
      private ProsimoUI.ProcessVarLabel labelColdSideRe;
      private System.Windows.Forms.Label labelFlowDirection;
      private ProsimoUI.ProcessVarLabel labelNumberOfHeatTransferUnits;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferArea;
      private ProsimoUI.ProcessVarLabel labelHotSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelColdSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelHotSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelExchangerEffectiveness;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelColdSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelFtFactor;
      private ProsimoUI.ProcessVarLabel labelTubeSideVelocity;
      private ProsimoUI.ProcessVarLabel labelShellSideVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingSimpleGenericShellAndTubeLabelsControl()
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
         this.labelHotSideRe = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideRe = new ProsimoUI.ProcessVarLabel();
         this.labelFlowDirection = new System.Windows.Forms.Label();
         this.labelNumberOfHeatTransferUnits = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferArea = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelExchangerEffectiveness = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelFtFactor = new ProsimoUI.ProcessVarLabel();
         this.labelTubeSideVelocity = new ProsimoUI.ProcessVarLabel();
         this.labelShellSideVelocity = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelHotSideRe
         // 
         this.labelHotSideRe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideRe.Location = new System.Drawing.Point(0, 260);
         this.labelHotSideRe.Name = "labelHotSideRe";
         this.labelHotSideRe.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideRe.TabIndex = 40;
         this.labelHotSideRe.Text = "HotSideRe";
         this.labelHotSideRe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideRe
         // 
         this.labelColdSideRe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideRe.Location = new System.Drawing.Point(0, 220);
         this.labelColdSideRe.Name = "labelColdSideRe";
         this.labelColdSideRe.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideRe.TabIndex = 38;
         this.labelColdSideRe.Text = "ColdSideRe";
         this.labelColdSideRe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelFlowDirection
         // 
         this.labelFlowDirection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelFlowDirection.Location = new System.Drawing.Point(0, 0);
         this.labelFlowDirection.Name = "labelFlowDirection";
         this.labelFlowDirection.Size = new System.Drawing.Size(292, 20);
         this.labelFlowDirection.TabIndex = 37;
         this.labelFlowDirection.Text = "Flow Direction:";
         this.labelFlowDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNumberOfHeatTransferUnits
         // 
         this.labelNumberOfHeatTransferUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfHeatTransferUnits.Location = new System.Drawing.Point(0, 140);
         this.labelNumberOfHeatTransferUnits.Name = "labelNumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.Size = new System.Drawing.Size(292, 20);
         this.labelNumberOfHeatTransferUnits.TabIndex = 36;
         this.labelNumberOfHeatTransferUnits.Text = "NumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferArea
         // 
         this.labelTotalHeatTransferArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferArea.Location = new System.Drawing.Point(0, 120);
         this.labelTotalHeatTransferArea.Name = "labelTotalHeatTransferArea";
         this.labelTotalHeatTransferArea.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferArea.TabIndex = 35;
         this.labelTotalHeatTransferArea.Text = "TotalHeatTransferArea";
         this.labelTotalHeatTransferArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideFoulingFactor
         // 
         this.labelHotSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideFoulingFactor.Location = new System.Drawing.Point(0, 80);
         this.labelHotSideFoulingFactor.Name = "labelHotSideFoulingFactor";
         this.labelHotSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideFoulingFactor.TabIndex = 34;
         this.labelHotSideFoulingFactor.Text = "HotSideFoulingFactor";
         this.labelHotSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideFoulingFactor
         // 
         this.labelColdSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideFoulingFactor.Location = new System.Drawing.Point(0, 60);
         this.labelColdSideFoulingFactor.Name = "labelColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideFoulingFactor.TabIndex = 33;
         this.labelColdSideFoulingFactor.Text = "ColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideHeatTransferCoefficient
         // 
         this.labelHotSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 40);
         this.labelHotSideHeatTransferCoefficient.Name = "labelHotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideHeatTransferCoefficient.TabIndex = 32;
         this.labelHotSideHeatTransferCoefficient.Text = "HotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelExchangerEffectiveness
         // 
         this.labelExchangerEffectiveness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelExchangerEffectiveness.Location = new System.Drawing.Point(0, 160);
         this.labelExchangerEffectiveness.Name = "labelExchangerEffectiveness";
         this.labelExchangerEffectiveness.Size = new System.Drawing.Size(292, 20);
         this.labelExchangerEffectiveness.TabIndex = 31;
         this.labelExchangerEffectiveness.Text = "ExchangerEffectiveness";
         this.labelExchangerEffectiveness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferCoefficient
         // 
         this.labelTotalHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferCoefficient.Location = new System.Drawing.Point(0, 100);
         this.labelTotalHeatTransferCoefficient.Name = "labelTotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferCoefficient.TabIndex = 30;
         this.labelTotalHeatTransferCoefficient.Text = "TotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideHeatTransferCoefficient
         // 
         this.labelColdSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 20);
         this.labelColdSideHeatTransferCoefficient.Name = "labelColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideHeatTransferCoefficient.TabIndex = 29;
         this.labelColdSideHeatTransferCoefficient.Text = "ColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelFtFactor
         // 
         this.labelFtFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelFtFactor.Location = new System.Drawing.Point(0, 180);
         this.labelFtFactor.Name = "labelFtFactor";
         this.labelFtFactor.Size = new System.Drawing.Size(292, 20);
         this.labelFtFactor.TabIndex = 42;
         this.labelFtFactor.Text = "FtFactor";
         this.labelFtFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeSideVelocity
         // 
         this.labelTubeSideVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeSideVelocity.Location = new System.Drawing.Point(0, 200);
         this.labelTubeSideVelocity.Name = "labelTubeSideVelocity";
         this.labelTubeSideVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelTubeSideVelocity.TabIndex = 43;
         this.labelTubeSideVelocity.Text = "TubeSideVelocity";
         this.labelTubeSideVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelShellSideVelocity
         // 
         this.labelShellSideVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellSideVelocity.Location = new System.Drawing.Point(0, 240);
         this.labelShellSideVelocity.Name = "labelShellSideVelocity";
         this.labelShellSideVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelShellSideVelocity.TabIndex = 44;
         this.labelShellSideVelocity.Text = "ShellSideVelocity";
         this.labelShellSideVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRatingSimpleGenericShellAndTubeLabelsControl
         // 
         this.Controls.Add(this.labelShellSideVelocity);
         this.Controls.Add(this.labelTubeSideVelocity);
         this.Controls.Add(this.labelFtFactor);
         this.Controls.Add(this.labelHotSideRe);
         this.Controls.Add(this.labelColdSideRe);
         this.Controls.Add(this.labelFlowDirection);
         this.Controls.Add(this.labelNumberOfHeatTransferUnits);
         this.Controls.Add(this.labelTotalHeatTransferArea);
         this.Controls.Add(this.labelHotSideFoulingFactor);
         this.Controls.Add(this.labelColdSideFoulingFactor);
         this.Controls.Add(this.labelHotSideHeatTransferCoefficient);
         this.Controls.Add(this.labelExchangerEffectiveness);
         this.Controls.Add(this.labelTotalHeatTransferCoefficient);
         this.Controls.Add(this.labelColdSideHeatTransferCoefficient);
         this.Name = "HXRatingSimpleGenericShellAndTubeLabelsControl";
         this.Size = new System.Drawing.Size(292, 280);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModelShellAndTube rating)
      {
         this.labelColdSideHeatTransferCoefficient.InitializeVariable(rating.ColdSideHeatTransferCoefficient);
         this.labelHotSideHeatTransferCoefficient.InitializeVariable(rating.HotSideHeatTransferCoefficient);
         this.labelColdSideFoulingFactor.InitializeVariable(rating.ColdSideFoulingFactor);
         this.labelHotSideFoulingFactor.InitializeVariable(rating.HotSideFoulingFactor);
         this.labelTotalHeatTransferCoefficient.InitializeVariable(rating.TotalHeatTransferCoefficient);
         this.labelTotalHeatTransferArea.InitializeVariable(rating.TotalHeatTransferArea);
         this.labelNumberOfHeatTransferUnits.InitializeVariable(rating.NumberOfHeatTransferUnits);
         this.labelExchangerEffectiveness.InitializeVariable(rating.ExchangerEffectiveness);
         this.labelColdSideRe.InitializeVariable(rating.ColdSideRe);
         this.labelHotSideRe.InitializeVariable(rating.HotSideRe);
         this.labelFtFactor.InitializeVariable(rating.FtFactor);
         this.labelTubeSideVelocity.InitializeVariable(rating.TubeSideVelocity);
         this.labelShellSideVelocity.InitializeVariable(rating.ShellSideVelocity);
      }
   }
}
