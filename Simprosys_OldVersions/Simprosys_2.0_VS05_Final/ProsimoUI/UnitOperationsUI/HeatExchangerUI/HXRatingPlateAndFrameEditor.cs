using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingPlateAndFrameEditor.
	/// </summary>
	public class HXRatingPlateAndFrameEditor : HeatExchangerRatingEditor
	{
      private bool inConstruction;
      private HeatExchanger heatExchanger;
      
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxCommonRating;
      private System.Windows.Forms.CheckBox checkBoxIncludeWallEffect;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl hxRating2ValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl hxRating2LabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericPlateAndFrameLabelsControl hxRatingSimpleGenericPlateAndFrameLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericPlateAndFrameValuesControl hxRatingSimpleGenericPlateAndFrameValuesControl;
      private System.Windows.Forms.GroupBox groupBoxPlateAndFrame;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingPlateAndFrameLabelsControl hxRatingPlateAndFrameLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingPlateAndFrameValuesControl hxRatingPlateAndFrameValuesControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingPlateAndFrameEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public HXRatingPlateAndFrameEditor(HeatExchangerControl heatExchangerCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         this.Text = "Plate And Frame Rating: " + heatExchangerCtrl.HeatExchanger.Name;
         this.heatExchanger = heatExchangerCtrl.HeatExchanger;

         HXRatingModelPlateAndFrame ratingModel = this.heatExchanger.CurrentRatingModel as HXRatingModelPlateAndFrame;
         
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
         this.hxRating2LabelsControl.Visible = this.checkBoxIncludeWallEffect.Checked;
         this.hxRating2ValuesControl.Visible = this.checkBoxIncludeWallEffect.Checked;

         this.hxRatingSimpleGenericPlateAndFrameLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingSimpleGenericPlateAndFrameValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, heatExchangerCtrl.HeatExchanger);

         this.hxRating2LabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRating2ValuesControl.InitializeVariableTextBoxes(heatExchangerCtrl.Flowsheet, ratingModel);

         this.hxRatingPlateAndFrameLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingPlateAndFrameValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, this.heatExchanger);

         this.heatExchanger.SolveComplete += new SolveCompleteEventHandler(heatExchanger_SolveComplete);

         this.inConstruction = false;
      }

      /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.heatExchanger != null)
            this.heatExchanger.SolveComplete -= new SolveCompleteEventHandler(heatExchanger_SolveComplete);

         if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxPlateAndFrame = new System.Windows.Forms.GroupBox();
         this.hxRatingPlateAndFrameValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingPlateAndFrameValuesControl();
         this.hxRatingPlateAndFrameLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingPlateAndFrameLabelsControl();
         this.groupBoxCommonRating = new System.Windows.Forms.GroupBox();
         this.hxRatingSimpleGenericPlateAndFrameValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericPlateAndFrameValuesControl();
         this.hxRatingSimpleGenericPlateAndFrameLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericPlateAndFrameLabelsControl();
         this.checkBoxIncludeWallEffect = new System.Windows.Forms.CheckBox();
         this.hxRating2ValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl();
         this.hxRating2LabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl();
         this.panel.SuspendLayout();
         this.groupBoxPlateAndFrame.SuspendLayout();
         this.groupBoxCommonRating.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxPlateAndFrame);
         this.panel.Controls.Add(this.groupBoxCommonRating);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(790, 363);
         this.panel.TabIndex = 0;
         // 
         // groupBoxPlateAndFrame
         // 
         this.groupBoxPlateAndFrame.Controls.Add(this.hxRatingPlateAndFrameValuesControl);
         this.groupBoxPlateAndFrame.Controls.Add(this.hxRatingPlateAndFrameLabelsControl);
         this.groupBoxPlateAndFrame.Location = new System.Drawing.Point(396, 4);
         this.groupBoxPlateAndFrame.Name = "groupBoxPlateAndFrame";
         this.groupBoxPlateAndFrame.Size = new System.Drawing.Size(388, 300);
         this.groupBoxPlateAndFrame.TabIndex = 1;
         this.groupBoxPlateAndFrame.TabStop = false;
         // 
         // hxRatingPlateAndFrameValuesControl
         // 
         this.hxRatingPlateAndFrameValuesControl.Location = new System.Drawing.Point(300, 12);
         this.hxRatingPlateAndFrameValuesControl.Name = "hxRatingPlateAndFrameValuesControl";
         this.hxRatingPlateAndFrameValuesControl.Size = new System.Drawing.Size(80, 280);
         this.hxRatingPlateAndFrameValuesControl.TabIndex = 1;
         // 
         // hxRatingPlateAndFrameLabelsControl
         // 
         this.hxRatingPlateAndFrameLabelsControl.Location = new System.Drawing.Point(8, 12);
         this.hxRatingPlateAndFrameLabelsControl.Name = "hxRatingPlateAndFrameLabelsControl";
         this.hxRatingPlateAndFrameLabelsControl.Size = new System.Drawing.Size(292, 280);
         this.hxRatingPlateAndFrameLabelsControl.TabIndex = 0;
         // 
         // groupBoxCommonRating
         // 
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericPlateAndFrameValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericPlateAndFrameLabelsControl);
         this.groupBoxCommonRating.Controls.Add(this.checkBoxIncludeWallEffect);
         this.groupBoxCommonRating.Controls.Add(this.hxRating2ValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRating2LabelsControl);
         this.groupBoxCommonRating.Location = new System.Drawing.Point(4, 4);
         this.groupBoxCommonRating.Name = "groupBoxCommonRating";
         this.groupBoxCommonRating.Size = new System.Drawing.Size(388, 352);
         this.groupBoxCommonRating.TabIndex = 0;
         this.groupBoxCommonRating.TabStop = false;
         // 
         // hxRatingSimpleGenericPlateAndFrameValuesControl
         // 
         this.hxRatingSimpleGenericPlateAndFrameValuesControl.Location = new System.Drawing.Point(300, 12);
         this.hxRatingSimpleGenericPlateAndFrameValuesControl.Name = "hxRatingSimpleGenericPlateAndFrameValuesControl";
         this.hxRatingSimpleGenericPlateAndFrameValuesControl.Size = new System.Drawing.Size(80, 260);
         this.hxRatingSimpleGenericPlateAndFrameValuesControl.TabIndex = 10;
         // 
         // hxRatingSimpleGenericPlateAndFrameLabelsControl
         // 
         this.hxRatingSimpleGenericPlateAndFrameLabelsControl.Location = new System.Drawing.Point(8, 12);
         this.hxRatingSimpleGenericPlateAndFrameLabelsControl.Name = "hxRatingSimpleGenericPlateAndFrameLabelsControl";
         this.hxRatingSimpleGenericPlateAndFrameLabelsControl.Size = new System.Drawing.Size(292, 260);
         this.hxRatingSimpleGenericPlateAndFrameLabelsControl.TabIndex = 9;
         // 
         // checkBoxIncludeWallEffect
         // 
         this.checkBoxIncludeWallEffect.Location = new System.Drawing.Point(8, 280);
         this.checkBoxIncludeWallEffect.Name = "checkBoxIncludeWallEffect";
         this.checkBoxIncludeWallEffect.Size = new System.Drawing.Size(368, 20);
         this.checkBoxIncludeWallEffect.TabIndex = 8;
         this.checkBoxIncludeWallEffect.Text = "Include Wall Effect In Total Heat Transfer Coefficient";
         this.checkBoxIncludeWallEffect.CheckedChanged += new System.EventHandler(this.checkBoxIncludeWallEffect_CheckedChanged);
         // 
         // hxRating2ValuesControl
         // 
         this.hxRating2ValuesControl.Location = new System.Drawing.Point(300, 304);
         this.hxRating2ValuesControl.Name = "hxRating2ValuesControl";
         this.hxRating2ValuesControl.Size = new System.Drawing.Size(80, 40);
         this.hxRating2ValuesControl.TabIndex = 7;
         // 
         // hxRating2LabelsControl
         // 
         this.hxRating2LabelsControl.Location = new System.Drawing.Point(8, 304);
         this.hxRating2LabelsControl.Name = "hxRating2LabelsControl";
         this.hxRating2LabelsControl.Size = new System.Drawing.Size(292, 40);
         this.hxRating2LabelsControl.TabIndex = 6;
         // 
         // HXRatingPlateAndFrameEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(790, 363);
         this.Controls.Add(this.panel);
         this.Name = "HXRatingPlateAndFrameEditor";
         this.Text = "HXRatingPlateAndFrameEditor";
         this.panel.ResumeLayout(false);
         this.groupBoxPlateAndFrame.ResumeLayout(false);
         this.groupBoxCommonRating.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void heatExchanger_SolveComplete(object sender, SolveState solveState)
      {
         HXRatingModel ratingModel = (sender as HeatExchanger).CurrentRatingModel as HXRatingModel;
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
      }

      private void checkBoxIncludeWallEffect_CheckedChanged(object sender, System.EventArgs e)
      {
         this.hxRating2LabelsControl.Visible = this.checkBoxIncludeWallEffect.Checked;
         this.hxRating2ValuesControl.Visible = this.checkBoxIncludeWallEffect.Checked;

         if (!this.inConstruction)
         {
            HXRatingModel ratingModel = heatExchanger.CurrentRatingModel as HXRatingModel;
            ErrorMessage error = ratingModel.SpecifyIncludeWallEffect(this.checkBoxIncludeWallEffect.Checked);
            if (error != null)
            {
               UI.ShowError(error);
            }
         }
      }
	}
}
