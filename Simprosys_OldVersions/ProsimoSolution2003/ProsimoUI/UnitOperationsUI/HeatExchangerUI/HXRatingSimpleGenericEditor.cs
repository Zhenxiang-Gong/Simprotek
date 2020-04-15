using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingSimpleGenericEditor.
	/// </summary>
	public class HXRatingSimpleGenericEditor : HeatExchangerRatingEditor
	{
      private bool inConstruction;
      private HeatExchanger heatExchanger;

      private System.Windows.Forms.GroupBox groupBoxCommonRating;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericLabelsControl hxRatingSimpleGenericLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericValuesControl hxRatingSimpleGenericValuesControl;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.CheckBox checkBoxIncludeWallEffect;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl hxRatingSimpleGeneric2ValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl hxRatingSimpleGeneric2LabelsControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HXRatingSimpleGenericEditor()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public HXRatingSimpleGenericEditor(HeatExchangerControl heatExchangerCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.inConstruction = true;
         this.Text = "Simple Generic Rating: " + heatExchangerCtrl.HeatExchanger.Name;
         this.heatExchanger = heatExchangerCtrl.HeatExchanger;

         HXRatingModel ratingModel = this.heatExchanger.CurrentRatingModel as HXRatingModel;
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
         this.hxRatingSimpleGeneric2LabelsControl.Visible = this.checkBoxIncludeWallEffect.Checked;
         this.hxRatingSimpleGeneric2ValuesControl.Visible = this.checkBoxIncludeWallEffect.Checked;

         this.hxRatingSimpleGenericLabelsControl.InitializeVariableLabels(heatExchangerCtrl.HeatExchanger.CurrentRatingModel);
         this.hxRatingSimpleGenericValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, heatExchangerCtrl.HeatExchanger);

         this.hxRatingSimpleGeneric2LabelsControl.InitializeVariableLabels(heatExchangerCtrl.HeatExchanger.CurrentRatingModel);
         this.hxRatingSimpleGeneric2ValuesControl.InitializeVariableTextBoxes(heatExchangerCtrl.Flowsheet, heatExchangerCtrl.HeatExchanger.CurrentRatingModel);

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
         this.groupBoxCommonRating = new System.Windows.Forms.GroupBox();
         this.checkBoxIncludeWallEffect = new System.Windows.Forms.CheckBox();
         this.hxRatingSimpleGeneric2ValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl();
         this.hxRatingSimpleGeneric2LabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl();
         this.hxRatingSimpleGenericLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericLabelsControl();
         this.hxRatingSimpleGenericValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericValuesControl();
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxCommonRating.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxCommonRating
         // 
         this.groupBoxCommonRating.Controls.Add(this.checkBoxIncludeWallEffect);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGeneric2ValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGeneric2LabelsControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericLabelsControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericValuesControl);
         this.groupBoxCommonRating.Location = new System.Drawing.Point(4, 4);
         this.groupBoxCommonRating.Name = "groupBoxCommonRating";
         this.groupBoxCommonRating.Size = new System.Drawing.Size(388, 272);
         this.groupBoxCommonRating.TabIndex = 2;
         this.groupBoxCommonRating.TabStop = false;
         // 
         // checkBoxIncludeWallEffect
         // 
         this.checkBoxIncludeWallEffect.Location = new System.Drawing.Point(8, 200);
         this.checkBoxIncludeWallEffect.Name = "checkBoxIncludeWallEffect";
         this.checkBoxIncludeWallEffect.Size = new System.Drawing.Size(372, 20);
         this.checkBoxIncludeWallEffect.TabIndex = 5;
         this.checkBoxIncludeWallEffect.Text = "Include Wall Effect In Total Heat Transfer Coefficient";
         this.checkBoxIncludeWallEffect.CheckedChanged += new System.EventHandler(this.checkBoxIncludeWallEffect_CheckedChanged);
         // 
         // hxRatingSimpleGeneric2ValuesControl
         // 
         this.hxRatingSimpleGeneric2ValuesControl.Location = new System.Drawing.Point(300, 224);
         this.hxRatingSimpleGeneric2ValuesControl.Name = "hxRatingSimpleGeneric2ValuesControl";
         this.hxRatingSimpleGeneric2ValuesControl.Size = new System.Drawing.Size(80, 40);
         this.hxRatingSimpleGeneric2ValuesControl.TabIndex = 4;
         // 
         // hxRatingSimpleGeneric2LabelsControl
         // 
         this.hxRatingSimpleGeneric2LabelsControl.Location = new System.Drawing.Point(8, 224);
         this.hxRatingSimpleGeneric2LabelsControl.Name = "hxRatingSimpleGeneric2LabelsControl";
         this.hxRatingSimpleGeneric2LabelsControl.Size = new System.Drawing.Size(292, 40);
         this.hxRatingSimpleGeneric2LabelsControl.TabIndex = 3;
         // 
         // hxRatingSimpleGenericLabelsControl
         // 
         this.hxRatingSimpleGenericLabelsControl.Location = new System.Drawing.Point(8, 12);
         this.hxRatingSimpleGenericLabelsControl.Name = "hxRatingSimpleGenericLabelsControl";
         this.hxRatingSimpleGenericLabelsControl.Size = new System.Drawing.Size(292, 180);
         this.hxRatingSimpleGenericLabelsControl.TabIndex = 2;
         // 
         // hxRatingSimpleGenericValuesControl
         // 
         this.hxRatingSimpleGenericValuesControl.Location = new System.Drawing.Point(300, 12);
         this.hxRatingSimpleGenericValuesControl.Name = "hxRatingSimpleGenericValuesControl";
         this.hxRatingSimpleGenericValuesControl.Size = new System.Drawing.Size(80, 180);
         this.hxRatingSimpleGenericValuesControl.TabIndex = 1;
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxCommonRating);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(398, 283);
         this.panel.TabIndex = 0;
         // 
         // HXRatingSimpleGenericEditor
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(398, 283);
         this.Controls.Add(this.panel);
         this.Name = "HXRatingSimpleGenericEditor";
         this.groupBoxCommonRating.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void checkBoxIncludeWallEffect_CheckedChanged(object sender, System.EventArgs e)
      {
         this.hxRatingSimpleGeneric2LabelsControl.Visible = this.checkBoxIncludeWallEffect.Checked;
         this.hxRatingSimpleGeneric2ValuesControl.Visible = this.checkBoxIncludeWallEffect.Checked;

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

      private void heatExchanger_SolveComplete(object sender, SolveState solveState)
      {
         HXRatingModel ratingModel = (sender as HeatExchanger).CurrentRatingModel as HXRatingModel;
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
      }
   }
}
