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
	/// Summary description for HXRatingSimpleGenericLabelsControl.
	/// </summary>
	public class HXRatingSimpleGenericLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 180;

      private ProsimoUI.ProcessVarLabel labelColdSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelHotSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelColdSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelHotSideFoulingFactor;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferCoefficient;
      private ProsimoUI.ProcessVarLabel labelTotalHeatTransferArea;
      private ProsimoUI.ProcessVarLabel labelNumberOfHeatTransferUnits;
      private ProsimoUI.ProcessVarLabel labelExchangerEffectiveness;
      private System.Windows.Forms.Label labelFlowDirection;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingSimpleGenericLabelsControl()
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
         this.labelColdSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelExchangerEffectiveness = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideHeatTransferCoefficient = new ProsimoUI.ProcessVarLabel();
         this.labelColdSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelHotSideFoulingFactor = new ProsimoUI.ProcessVarLabel();
         this.labelTotalHeatTransferArea = new ProsimoUI.ProcessVarLabel();
         this.labelNumberOfHeatTransferUnits = new ProsimoUI.ProcessVarLabel();
         this.labelFlowDirection = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelColdSideHeatTransferCoefficient
         // 
         this.labelColdSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 20);
         this.labelColdSideHeatTransferCoefficient.Name = "labelColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideHeatTransferCoefficient.TabIndex = 0;
         this.labelColdSideHeatTransferCoefficient.Text = "ColdSideHeatTransferCoefficient";
         this.labelColdSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferCoefficient
         // 
         this.labelTotalHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferCoefficient.Location = new System.Drawing.Point(0, 100);
         this.labelTotalHeatTransferCoefficient.Name = "labelTotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferCoefficient.TabIndex = 2;
         this.labelTotalHeatTransferCoefficient.Text = "TotalHeatTransferCoefficient";
         this.labelTotalHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelExchangerEffectiveness
         // 
         this.labelExchangerEffectiveness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelExchangerEffectiveness.Location = new System.Drawing.Point(0, 160);
         this.labelExchangerEffectiveness.Name = "labelExchangerEffectiveness";
         this.labelExchangerEffectiveness.Size = new System.Drawing.Size(292, 20);
         this.labelExchangerEffectiveness.TabIndex = 4;
         this.labelExchangerEffectiveness.Text = "ExchangerEffectiveness";
         this.labelExchangerEffectiveness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideHeatTransferCoefficient
         // 
         this.labelHotSideHeatTransferCoefficient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 40);
         this.labelHotSideHeatTransferCoefficient.Name = "labelHotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideHeatTransferCoefficient.TabIndex = 6;
         this.labelHotSideHeatTransferCoefficient.Text = "HotSideHeatTransferCoefficient";
         this.labelHotSideHeatTransferCoefficient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSideFoulingFactor
         // 
         this.labelColdSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSideFoulingFactor.Location = new System.Drawing.Point(0, 60);
         this.labelColdSideFoulingFactor.Name = "labelColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelColdSideFoulingFactor.TabIndex = 8;
         this.labelColdSideFoulingFactor.Text = "ColdSideFoulingFactor";
         this.labelColdSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSideFoulingFactor
         // 
         this.labelHotSideFoulingFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSideFoulingFactor.Location = new System.Drawing.Point(0, 80);
         this.labelHotSideFoulingFactor.Name = "labelHotSideFoulingFactor";
         this.labelHotSideFoulingFactor.Size = new System.Drawing.Size(292, 20);
         this.labelHotSideFoulingFactor.TabIndex = 10;
         this.labelHotSideFoulingFactor.Text = "HotSideFoulingFactor";
         this.labelHotSideFoulingFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalHeatTransferArea
         // 
         this.labelTotalHeatTransferArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransferArea.Location = new System.Drawing.Point(0, 120);
         this.labelTotalHeatTransferArea.Name = "labelTotalHeatTransferArea";
         this.labelTotalHeatTransferArea.Size = new System.Drawing.Size(292, 20);
         this.labelTotalHeatTransferArea.TabIndex = 12;
         this.labelTotalHeatTransferArea.Text = "TotalHeatTransferArea";
         this.labelTotalHeatTransferArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNumberOfHeatTransferUnits
         // 
         this.labelNumberOfHeatTransferUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfHeatTransferUnits.Location = new System.Drawing.Point(0, 140);
         this.labelNumberOfHeatTransferUnits.Name = "labelNumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.Size = new System.Drawing.Size(292, 20);
         this.labelNumberOfHeatTransferUnits.TabIndex = 14;
         this.labelNumberOfHeatTransferUnits.Text = "NumberOfHeatTransferUnits";
         this.labelNumberOfHeatTransferUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelFlowDirection
         // 
         this.labelFlowDirection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelFlowDirection.Location = new System.Drawing.Point(0, 0);
         this.labelFlowDirection.Name = "labelFlowDirection";
         this.labelFlowDirection.Size = new System.Drawing.Size(292, 20);
         this.labelFlowDirection.TabIndex = 15;
         this.labelFlowDirection.Text = "Flow Direction:";
         this.labelFlowDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRatingSimpleGenericLabelsControl
         // 
         this.Controls.Add(this.labelFlowDirection);
         this.Controls.Add(this.labelNumberOfHeatTransferUnits);
         this.Controls.Add(this.labelTotalHeatTransferArea);
         this.Controls.Add(this.labelHotSideFoulingFactor);
         this.Controls.Add(this.labelColdSideFoulingFactor);
         this.Controls.Add(this.labelHotSideHeatTransferCoefficient);
         this.Controls.Add(this.labelExchangerEffectiveness);
         this.Controls.Add(this.labelTotalHeatTransferCoefficient);
         this.Controls.Add(this.labelColdSideHeatTransferCoefficient);
         this.Name = "HXRatingSimpleGenericLabelsControl";
         this.Size = new System.Drawing.Size(292, 180);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModel rating)
      {
         this.labelColdSideHeatTransferCoefficient.InitializeVariable(rating.ColdSideHeatTransferCoefficient);
         this.labelHotSideHeatTransferCoefficient.InitializeVariable(rating.HotSideHeatTransferCoefficient);
         this.labelColdSideFoulingFactor.InitializeVariable(rating.ColdSideFoulingFactor);
         this.labelHotSideFoulingFactor.InitializeVariable(rating.HotSideFoulingFactor);
         this.labelTotalHeatTransferCoefficient.InitializeVariable(rating.TotalHeatTransferCoefficient);
         this.labelTotalHeatTransferArea.InitializeVariable(rating.TotalHeatTransferArea);
         this.labelNumberOfHeatTransferUnits.InitializeVariable(rating.NumberOfHeatTransferUnits);
         this.labelExchangerEffectiveness.InitializeVariable(rating.ExchangerEffectiveness);
      }
	}
}
