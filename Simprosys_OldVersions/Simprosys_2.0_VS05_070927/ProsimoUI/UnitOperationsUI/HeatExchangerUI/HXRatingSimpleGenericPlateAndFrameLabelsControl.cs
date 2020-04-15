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
	/// Summary description for HXRatingSimpleGenericPlateAndFrameLabelsControl.
	/// </summary>
	public class HXRatingSimpleGenericPlateAndFrameLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 260;

      private System.Windows.Forms.Label labelFlowDirection;
      private ProsimoUI.ProcessVarLabel labelNumberOfHeatTransferUnits;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferArea;
      private ProsimoUI.ProcessVarLabel labelHotSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelColdSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelHotSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelExchangerEffectiveness;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelColdSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelColdSideRe;
      private ProsimoUI.ProcessVarLabel labelHotSideRe;
      private ProsimoUI.ProcessVarLabel labelColdSideVelocity;
      private ProsimoUI.ProcessVarLabel labelHotSideVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingSimpleGenericPlateAndFrameLabelsControl()
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
         this.labelFlowDirection = new System.Windows.Forms.Label();
         this.labelNumberOfHeatTransferUnits = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferArea = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelExchangerEffectiveness = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideRe = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideRe = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideVelocity = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideVelocity = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelFlowDirection
         // 
         this.labelFlowDirection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelFlowDirection.Location = new System.Drawing.Point(0, 0);
         this.labelFlowDirection.Name = "labelFlowDirection";
         this.labelFlowDirection.Size = new System.Drawing.Size(292, 20);
         this.labelFlowDirection.TabIndex = 24;
         this.labelFlowDirection.Text = "Flow Direction:";
         this.labelFlowDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNumberOfHeatTransferUnits
         // 
         this.labelNumberOfHeatTransferUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfHeatTransferUnits.Location = new System.Drawing.Point(0, 140);
         this.labelNumberOfHeatTransferUnits.Name = "labelNumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.Size = new System.Drawing.Size(292, 20);
         this.labelNumberOfHeatTransferUnits.TabIndex = 23;
         this.labelNumberOfHeatTransferUnits.Text = "NumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferArea
         // 
         this.labelTotalHeatTransferArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferArea.Location = new System.Drawing.Point(0, 120);
         this.labelTotalHeatTransferArea.Name = "labelTotalHeatTransferArea";
         this.labelTotalHeatTransferArea.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferArea.TabIndex = 22;
         this.labelTotalHeatTransferArea.Text = "TotalHeatTransferArea";
         this.labelTotalHeatTransferArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideFoulingFactor
         // 
         this.labelHotSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideFoulingFactor.Location = new System.Drawing.Point(0, 80);
         this.labelHotSideFoulingFactor.Name = "labelHotSideFoulingFactor";
         this.labelHotSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideFoulingFactor.TabIndex = 21;
         this.labelHotSideFoulingFactor.Text = "HotSideFoulingFactor";
         this.labelHotSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideFoulingFactor
         // 
         this.labelColdSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideFoulingFactor.Location = new System.Drawing.Point(0, 60);
         this.labelColdSideFoulingFactor.Name = "labelColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideFoulingFactor.TabIndex = 20;
         this.labelColdSideFoulingFactor.Text = "ColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideHeatTransferCoefficient
         // 
         this.labelHotSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 40);
         this.labelHotSideHeatTransferCoefficient.Name = "labelHotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideHeatTransferCoefficient.TabIndex = 19;
         this.labelHotSideHeatTransferCoefficient.Text = "HotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelExchangerEffectiveness
         // 
         this.labelExchangerEffectiveness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelExchangerEffectiveness.Location = new System.Drawing.Point(0, 160);
         this.labelExchangerEffectiveness.Name = "labelExchangerEffectiveness";
         this.labelExchangerEffectiveness.Size = new System.Drawing.Size(292, 20);
         this.labelExchangerEffectiveness.TabIndex = 18;
         this.labelExchangerEffectiveness.Text = "ExchangerEffectiveness";
         this.labelExchangerEffectiveness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferCoefficient
         // 
         this.labelTotalHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferCoefficient.Location = new System.Drawing.Point(0, 100);
         this.labelTotalHeatTransferCoefficient.Name = "labelTotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferCoefficient.TabIndex = 17;
         this.labelTotalHeatTransferCoefficient.Text = "TotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideHeatTransferCoefficient
         // 
         this.labelColdSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 20);
         this.labelColdSideHeatTransferCoefficient.Name = "labelColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideHeatTransferCoefficient.TabIndex = 16;
         this.labelColdSideHeatTransferCoefficient.Text = "ColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideRe
         // 
         this.labelColdSideRe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideRe.Location = new System.Drawing.Point(0, 200);
         this.labelColdSideRe.Name = "labelColdSideRe";
         this.labelColdSideRe.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideRe.TabIndex = 25;
         this.labelColdSideRe.Text = "ColdSideRe";
         this.labelColdSideRe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideRe
         // 
         this.labelHotSideRe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideRe.Location = new System.Drawing.Point(0, 240);
         this.labelHotSideRe.Name = "labelHotSideRe";
         this.labelHotSideRe.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideRe.TabIndex = 27;
         this.labelHotSideRe.Text = "HotSideRe";
         this.labelHotSideRe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideVelocity
         // 
         this.labelColdSideVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideVelocity.Location = new System.Drawing.Point(0, 180);
         this.labelColdSideVelocity.Name = "labelColdSideVelocity";
         this.labelColdSideVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideVelocity.TabIndex = 28;
         this.labelColdSideVelocity.Text = "ColdSideVelocity";
         this.labelColdSideVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideVelocity
         // 
         this.labelHotSideVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideVelocity.Location = new System.Drawing.Point(0, 220);
         this.labelHotSideVelocity.Name = "labelHotSideVelocity";
         this.labelHotSideVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideVelocity.TabIndex = 29;
         this.labelHotSideVelocity.Text = "HotSideVelocity";
         this.labelHotSideVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRatingSimpleGenericPlateAndFrameLabelsControl
         // 
         this.Controls.Add(this.labelHotSideVelocity);
         this.Controls.Add(this.labelColdSideVelocity);
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
         this.Name = "HXRatingSimpleGenericPlateAndFrameLabelsControl";
         this.Size = new System.Drawing.Size(292, 260);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModelPlateAndFrame rating)
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
         this.labelHotSideVelocity.InitializeVariable(rating.HotSideVelocity);
         this.labelColdSideVelocity.InitializeVariable(rating.ColdSideVelocity);
      }
   }
}
