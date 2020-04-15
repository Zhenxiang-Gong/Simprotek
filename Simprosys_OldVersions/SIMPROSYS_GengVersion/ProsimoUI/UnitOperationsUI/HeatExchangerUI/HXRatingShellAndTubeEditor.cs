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
	/// Summary description for HXRatingShellAndTubeEditor.
	/// </summary>
	public class HXRatingShellAndTubeEditor : HeatExchangerRatingEditor
	{
      public const int INDEX_BELL_DELAWARE = 0;
      public const int INDEX_KERN = 1;
      public const int INDEX_DONOHUE = 2;
      
      private bool inConstruction;
      private HeatExchanger heatExchanger;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxCommonRating;
      private System.Windows.Forms.CheckBox checkBoxIncludeWallEffect;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl hxRating2ValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl hxRating2LabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericShellAndTubeLabelsControl hxRatingSimpleGenericShellAndTubeLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericShellAndTubeValuesControl hxRatingSimpleGenericShellAndTubeValuesControl;
      private System.Windows.Forms.GroupBox groupBoxShellAndTube;
      public System.Windows.Forms.ComboBox comboBoxRatingType;
      private System.Windows.Forms.Label labelRatingType;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeBellDelawareLabelsControl hxRatingShellAndTubeBellDelawareLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeDonohueLabelsControl hxRatingShellAndTubeDonohueLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeBellDelawareValuesControl hxRatingShellAndTubeBellDelawareValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeDonohueValuesControl hxRatingShellAndTubeDonohueValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating3ValuesControl hxRating3ValuesControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating3LabelsControl hxRating3LabelsControl;
      private System.Windows.Forms.CheckBox checkBoxIncludeNozzleEffect;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeKernLabelsControl hxRatingShellAndTubeKernLabelsControl;
      private ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeKernValuesControl hxRatingShellAndTubeKernValuesControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingShellAndTubeEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public HXRatingShellAndTubeEditor(HeatExchangerControl heatExchangerCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         this.Text = "Shell And Tube Rating: " + heatExchangerCtrl.HeatExchanger.Name;
         this.heatExchanger = heatExchangerCtrl.HeatExchanger;

         HXRatingModelShellAndTube ratingModel = this.heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
         
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
         this.hxRating2LabelsControl.Visible = this.checkBoxIncludeWallEffect.Checked;
         this.hxRating2ValuesControl.Visible = this.checkBoxIncludeWallEffect.Checked;

         this.checkBoxIncludeNozzleEffect.Checked = ratingModel.IncludeNozzleEffect;
         this.hxRating3LabelsControl.Visible = this.checkBoxIncludeNozzleEffect.Checked;
         this.hxRating3ValuesControl.Visible = this.checkBoxIncludeNozzleEffect.Checked;

         this.hxRatingSimpleGenericShellAndTubeLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingSimpleGenericShellAndTubeValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, heatExchangerCtrl.HeatExchanger);

         this.hxRating2LabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRating2ValuesControl.InitializeVariableTextBoxes(heatExchangerCtrl.Flowsheet, ratingModel);

         this.hxRatingShellAndTubeBellDelawareLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingShellAndTubeBellDelawareValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, this.heatExchanger);

         this.hxRatingShellAndTubeDonohueLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingShellAndTubeDonohueValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, this.heatExchanger);

         this.hxRatingShellAndTubeKernLabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRatingShellAndTubeKernValuesControl.InitializeTheUI(heatExchangerCtrl.Flowsheet, this.heatExchanger);

         this.hxRating3LabelsControl.InitializeVariableLabels(ratingModel);
         this.hxRating3ValuesControl.InitializeVariableTextBoxes(heatExchangerCtrl.Flowsheet, ratingModel);


         this.heatExchanger.SolveComplete += new SolveCompleteEventHandler(heatExchanger_SolveComplete);

         this.comboBoxRatingType.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetRatingType(ratingModel.ShellRatingType);
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
         this.groupBoxShellAndTube = new System.Windows.Forms.GroupBox();
         this.hxRatingShellAndTubeKernValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeKernValuesControl();
         this.hxRatingShellAndTubeKernLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeKernLabelsControl();
         this.hxRating3ValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating3ValuesControl();
         this.hxRating3LabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating3LabelsControl();
         this.checkBoxIncludeNozzleEffect = new System.Windows.Forms.CheckBox();
         this.hxRatingShellAndTubeDonohueValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeDonohueValuesControl();
         this.hxRatingShellAndTubeBellDelawareValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeBellDelawareValuesControl();
         this.hxRatingShellAndTubeDonohueLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeDonohueLabelsControl();
         this.hxRatingShellAndTubeBellDelawareLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingShellAndTubeBellDelawareLabelsControl();
         this.labelRatingType = new System.Windows.Forms.Label();
         this.comboBoxRatingType = new System.Windows.Forms.ComboBox();
         this.groupBoxCommonRating = new System.Windows.Forms.GroupBox();
         this.checkBoxIncludeWallEffect = new System.Windows.Forms.CheckBox();
         this.hxRating2ValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2ValuesControl();
         this.hxRating2LabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRating2LabelsControl();
         this.hxRatingSimpleGenericShellAndTubeValuesControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericShellAndTubeValuesControl();
         this.hxRatingSimpleGenericShellAndTubeLabelsControl = new ProsimoUI.UnitOperationsUI.HeatExchangerUI.HXRatingSimpleGenericShellAndTubeLabelsControl();
         this.panel.SuspendLayout();
         this.groupBoxShellAndTube.SuspendLayout();
         this.groupBoxCommonRating.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxShellAndTube);
         this.panel.Controls.Add(this.groupBoxCommonRating);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(790, 563);
         this.panel.TabIndex = 0;
         // 
         // groupBoxShellAndTube
         // 
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeKernValuesControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeKernLabelsControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRating3ValuesControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRating3LabelsControl);
         this.groupBoxShellAndTube.Controls.Add(this.checkBoxIncludeNozzleEffect);
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeDonohueValuesControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeBellDelawareValuesControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeDonohueLabelsControl);
         this.groupBoxShellAndTube.Controls.Add(this.hxRatingShellAndTubeBellDelawareLabelsControl);
         this.groupBoxShellAndTube.Controls.Add(this.labelRatingType);
         this.groupBoxShellAndTube.Controls.Add(this.comboBoxRatingType);
         this.groupBoxShellAndTube.Location = new System.Drawing.Point(396, 4);
         this.groupBoxShellAndTube.Name = "groupBoxShellAndTube";
         this.groupBoxShellAndTube.Size = new System.Drawing.Size(388, 552);
         this.groupBoxShellAndTube.TabIndex = 1;
         this.groupBoxShellAndTube.TabStop = false;
         // 
         // hxRatingShellAndTubeKernValuesControl
         // 
         this.hxRatingShellAndTubeKernValuesControl.Location = new System.Drawing.Point(300, 32);
         this.hxRatingShellAndTubeKernValuesControl.Name = "hxRatingShellAndTubeKernValuesControl";
         this.hxRatingShellAndTubeKernValuesControl.Size = new System.Drawing.Size(80, 300);
         this.hxRatingShellAndTubeKernValuesControl.TabIndex = 34;
         // 
         // hxRatingShellAndTubeKernLabelsControl
         // 
         this.hxRatingShellAndTubeKernLabelsControl.Location = new System.Drawing.Point(8, 32);
         this.hxRatingShellAndTubeKernLabelsControl.Name = "hxRatingShellAndTubeKernLabelsControl";
         this.hxRatingShellAndTubeKernLabelsControl.Size = new System.Drawing.Size(292, 300);
         this.hxRatingShellAndTubeKernLabelsControl.TabIndex = 33;
         // 
         // hxRating3ValuesControl
         // 
         this.hxRating3ValuesControl.Location = new System.Drawing.Point(300, 464);
         this.hxRating3ValuesControl.Name = "hxRating3ValuesControl";
         this.hxRating3ValuesControl.Size = new System.Drawing.Size(80, 80);
         this.hxRating3ValuesControl.TabIndex = 32;
         // 
         // hxRating3LabelsControl
         // 
         this.hxRating3LabelsControl.Location = new System.Drawing.Point(8, 464);
         this.hxRating3LabelsControl.Name = "hxRating3LabelsControl";
         this.hxRating3LabelsControl.Size = new System.Drawing.Size(292, 80);
         this.hxRating3LabelsControl.TabIndex = 31;
         // 
         // checkBoxIncludeNozzleEffect
         // 
         this.checkBoxIncludeNozzleEffect.Location = new System.Drawing.Point(8, 440);
         this.checkBoxIncludeNozzleEffect.Name = "checkBoxIncludeNozzleEffect";
         this.checkBoxIncludeNozzleEffect.Size = new System.Drawing.Size(368, 20);
         this.checkBoxIncludeNozzleEffect.TabIndex = 30;
         this.checkBoxIncludeNozzleEffect.Text = "Include Nozzle Effect In Pressure Drop";
         this.checkBoxIncludeNozzleEffect.CheckedChanged += new System.EventHandler(this.checkBoxIncludeNozzleEffect_CheckedChanged);
         // 
         // hxRatingShellAndTubeDonohueValuesControl
         // 
         this.hxRatingShellAndTubeDonohueValuesControl.Location = new System.Drawing.Point(300, 32);
         this.hxRatingShellAndTubeDonohueValuesControl.Name = "hxRatingShellAndTubeDonohueValuesControl";
         this.hxRatingShellAndTubeDonohueValuesControl.Size = new System.Drawing.Size(80, 280);
         this.hxRatingShellAndTubeDonohueValuesControl.TabIndex = 29;
         // 
         // hxRatingShellAndTubeBellDelawareValuesControl
         // 
         this.hxRatingShellAndTubeBellDelawareValuesControl.Location = new System.Drawing.Point(300, 32);
         this.hxRatingShellAndTubeBellDelawareValuesControl.Name = "hxRatingShellAndTubeBellDelawareValuesControl";
         this.hxRatingShellAndTubeBellDelawareValuesControl.Size = new System.Drawing.Size(80, 400);
         this.hxRatingShellAndTubeBellDelawareValuesControl.TabIndex = 28;
         // 
         // hxRatingShellAndTubeDonohueLabelsControl
         // 
         this.hxRatingShellAndTubeDonohueLabelsControl.Location = new System.Drawing.Point(8, 32);
         this.hxRatingShellAndTubeDonohueLabelsControl.Name = "hxRatingShellAndTubeDonohueLabelsControl";
         this.hxRatingShellAndTubeDonohueLabelsControl.Size = new System.Drawing.Size(292, 280);
         this.hxRatingShellAndTubeDonohueLabelsControl.TabIndex = 27;
         // 
         // hxRatingShellAndTubeBellDelawareLabelsControl
         // 
         this.hxRatingShellAndTubeBellDelawareLabelsControl.Location = new System.Drawing.Point(8, 32);
         this.hxRatingShellAndTubeBellDelawareLabelsControl.Name = "hxRatingShellAndTubeBellDelawareLabelsControl";
         this.hxRatingShellAndTubeBellDelawareLabelsControl.Size = new System.Drawing.Size(292, 400);
         this.hxRatingShellAndTubeBellDelawareLabelsControl.TabIndex = 26;
         // 
         // labelRatingType
         // 
         this.labelRatingType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelRatingType.Location = new System.Drawing.Point(8, 12);
         this.labelRatingType.Name = "labelRatingType";
         this.labelRatingType.Size = new System.Drawing.Size(292, 20);
         this.labelRatingType.TabIndex = 25;
         this.labelRatingType.Text = "Rating Type:";
         this.labelRatingType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // comboBoxRatingType
         // 
         this.comboBoxRatingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxRatingType.Items.AddRange(new object[] {
                                                                "Bell Delaware",
                                                                "Kern",
                                                                "Donohue"});
         this.comboBoxRatingType.Location = new System.Drawing.Point(300, 12);
         this.comboBoxRatingType.Name = "comboBoxRatingType";
         this.comboBoxRatingType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxRatingType.TabIndex = 11;
         this.comboBoxRatingType.SelectedIndexChanged += new System.EventHandler(this.comboBoxRatingType_SelectedIndexChanged);
         // 
         // groupBoxCommonRating
         // 
         this.groupBoxCommonRating.Controls.Add(this.checkBoxIncludeWallEffect);
         this.groupBoxCommonRating.Controls.Add(this.hxRating2ValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRating2LabelsControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericShellAndTubeValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.hxRatingSimpleGenericShellAndTubeLabelsControl);
         this.groupBoxCommonRating.Location = new System.Drawing.Point(4, 4);
         this.groupBoxCommonRating.Name = "groupBoxCommonRating";
         this.groupBoxCommonRating.Size = new System.Drawing.Size(388, 372);
         this.groupBoxCommonRating.TabIndex = 0;
         this.groupBoxCommonRating.TabStop = false;
         // 
         // checkBoxIncludeWallEffect
         // 
         this.checkBoxIncludeWallEffect.Location = new System.Drawing.Point(8, 300);
         this.checkBoxIncludeWallEffect.Name = "checkBoxIncludeWallEffect";
         this.checkBoxIncludeWallEffect.Size = new System.Drawing.Size(368, 20);
         this.checkBoxIncludeWallEffect.TabIndex = 11;
         this.checkBoxIncludeWallEffect.Text = "Include Wall Effect In Total Heat Transfer Coefficient";
         this.checkBoxIncludeWallEffect.CheckedChanged += new System.EventHandler(this.checkBoxIncludeWallEffect_CheckedChanged);
         // 
         // hxRating2ValuesControl
         // 
         this.hxRating2ValuesControl.Location = new System.Drawing.Point(300, 324);
         this.hxRating2ValuesControl.Name = "hxRating2ValuesControl";
         this.hxRating2ValuesControl.Size = new System.Drawing.Size(80, 40);
         this.hxRating2ValuesControl.TabIndex = 10;
         // 
         // hxRating2LabelsControl
         // 
         this.hxRating2LabelsControl.Location = new System.Drawing.Point(8, 324);
         this.hxRating2LabelsControl.Name = "hxRating2LabelsControl";
         this.hxRating2LabelsControl.Size = new System.Drawing.Size(292, 40);
         this.hxRating2LabelsControl.TabIndex = 9;
         // 
         // hxRatingSimpleGenericShellAndTubeValuesControl
         // 
         this.hxRatingSimpleGenericShellAndTubeValuesControl.Location = new System.Drawing.Point(300, 12);
         this.hxRatingSimpleGenericShellAndTubeValuesControl.Name = "hxRatingSimpleGenericShellAndTubeValuesControl";
         this.hxRatingSimpleGenericShellAndTubeValuesControl.Size = new System.Drawing.Size(80, 280);
         this.hxRatingSimpleGenericShellAndTubeValuesControl.TabIndex = 1;
         // 
         // hxRatingSimpleGenericShellAndTubeLabelsControl
         // 
         this.hxRatingSimpleGenericShellAndTubeLabelsControl.Location = new System.Drawing.Point(8, 12);
         this.hxRatingSimpleGenericShellAndTubeLabelsControl.Name = "hxRatingSimpleGenericShellAndTubeLabelsControl";
         this.hxRatingSimpleGenericShellAndTubeLabelsControl.Size = new System.Drawing.Size(292, 280);
         this.hxRatingSimpleGenericShellAndTubeLabelsControl.TabIndex = 0;
         // 
         // HXRatingShellAndTubeEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(790, 563);
         this.Controls.Add(this.panel);
         this.Name = "HXRatingShellAndTubeEditor";
         this.Text = "HXRatingShellAndTubeEditor";
         this.panel.ResumeLayout(false);
         this.groupBoxShellAndTube.ResumeLayout(false);
         this.groupBoxCommonRating.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void SetRatingType(ShellRatingType ratingType)
      {
         if (ratingType == ShellRatingType.BellDelaware)
            this.comboBoxRatingType.SelectedIndex = HXRatingShellAndTubeEditor.INDEX_BELL_DELAWARE;
         else if (ratingType == ShellRatingType.Donohue)
            this.comboBoxRatingType.SelectedIndex = HXRatingShellAndTubeEditor.INDEX_DONOHUE;
         else if (ratingType == ShellRatingType.Kern)
            this.comboBoxRatingType.SelectedIndex = HXRatingShellAndTubeEditor.INDEX_KERN;
      }

      private void comboBoxRatingType_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxRatingType.SelectedIndex;

            HXRatingModelShellAndTube ratingModel = heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
            if (idx == HXRatingShellAndTubeEditor.INDEX_BELL_DELAWARE)
            {
               error = ratingModel.SpecifyShellRatingType(ShellRatingType.BellDelaware);
               if (error == null)
               {
                  this.hxRatingShellAndTubeBellDelawareLabelsControl.Visible = true;
                  this.hxRatingShellAndTubeBellDelawareValuesControl.Visible = true;
                  this.hxRatingShellAndTubeDonohueLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeDonohueValuesControl.Visible = false;
                  this.hxRatingShellAndTubeKernLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeKernValuesControl.Visible = false;
               }
            }
            else if (idx == HXRatingShellAndTubeEditor.INDEX_DONOHUE)
            {
               error = ratingModel.SpecifyShellRatingType(ShellRatingType.Donohue);
               if (error == null)
               {
                  this.hxRatingShellAndTubeBellDelawareLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeBellDelawareValuesControl.Visible = false;
                  this.hxRatingShellAndTubeDonohueLabelsControl.Visible = true;
                  this.hxRatingShellAndTubeDonohueValuesControl.Visible = true;
                  this.hxRatingShellAndTubeKernLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeKernValuesControl.Visible = false;
               }
            }
            else if (idx == HXRatingShellAndTubeEditor.INDEX_KERN)
            {
               error = ratingModel.SpecifyShellRatingType(ShellRatingType.Kern);
               if (error == null)
               {
                  this.hxRatingShellAndTubeBellDelawareLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeBellDelawareValuesControl.Visible = false;
                  this.hxRatingShellAndTubeDonohueLabelsControl.Visible = false;
                  this.hxRatingShellAndTubeDonohueValuesControl.Visible = false;
                  this.hxRatingShellAndTubeKernLabelsControl.Visible = true;
                  this.hxRatingShellAndTubeKernValuesControl.Visible = true;
               }
            }

            if (error != null)
            {
               UI.ShowError(error);
               this.SetRatingType(ratingModel.ShellRatingType);
            }
         }
      }

      private void heatExchanger_SolveComplete(object sender, SolveState solveState)
      {
         HXRatingModelShellAndTube ratingModel = (sender as HeatExchanger).CurrentRatingModel as HXRatingModelShellAndTube;
         this.checkBoxIncludeWallEffect.Checked = ratingModel.IncludeWallEffect;
         this.checkBoxIncludeNozzleEffect.Checked = ratingModel.IncludeNozzleEffect;
         this.SetRatingType(ratingModel.ShellRatingType);
      }

      private void checkBoxIncludeNozzleEffect_CheckedChanged(object sender, System.EventArgs e)
      {
         this.hxRating3LabelsControl.Visible = this.checkBoxIncludeNozzleEffect.Checked;
         this.hxRating3ValuesControl.Visible = this.checkBoxIncludeNozzleEffect.Checked;

         if (!this.inConstruction)
         {
            HXRatingModelShellAndTube ratingModel = heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
            ErrorMessage error = ratingModel.SpecifyIncludeNozzleEffect(this.checkBoxIncludeNozzleEffect.Checked);
            if (error != null)
            {
               UI.ShowError(error);
            }
         }
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
