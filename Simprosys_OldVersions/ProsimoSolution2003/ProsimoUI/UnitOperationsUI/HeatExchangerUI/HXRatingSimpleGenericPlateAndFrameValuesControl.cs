using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingSimpleGenericPlateAndFrameValuesControl.
	/// </summary>
	public class HXRatingSimpleGenericPlateAndFrameValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 260;

      private bool inConstruction;
      private HeatExchanger heatExchanger;

      public System.Windows.Forms.ComboBox comboBoxFlowDirection;
      private ProsimoUI.ProcessVarTextBox textBoxNumberOfHeatTransferUnits;
      private ProsimoUI.ProcessVarTextBox textBoxTotalHeatTransferArea;
      private ProsimoUI.ProcessVarTextBox textBoxHotSideFoulingFactor;
      private ProsimoUI.ProcessVarTextBox textBoxColdSideFoulingFactor;
      private ProsimoUI.ProcessVarTextBox textBoxHotSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarTextBox textBoxExchangerEffectiveness;
      private ProsimoUI.ProcessVarTextBox textBoxTotalHeatTransferCoefficient;
      private ProsimoUI.ProcessVarTextBox textBoxColdSideHeatTransferCoefficient;
      private ProsimoUI.ProcessVarTextBox textBoxColdSideRe;
      private ProsimoUI.ProcessVarTextBox textBoxHotSideRe;
      private ProsimoUI.ProcessVarTextBox textBoxColdSideVelocity;
      private ProsimoUI.ProcessVarTextBox textBoxHotSideVelocity;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingSimpleGenericPlateAndFrameValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.heatExchanger != null)
            heatExchanger.SolveComplete -= new SolveCompleteEventHandler(heatExchanger_SolveComplete);
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
         this.comboBoxFlowDirection = new System.Windows.Forms.ComboBox();
         this.textBoxNumberOfHeatTransferUnits = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalHeatTransferArea = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSideFoulingFactor = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSideFoulingFactor = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSideHeatTransferCoefficient = new ProsimoUI.ProcessVarTextBox();
         this.textBoxExchangerEffectiveness = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalHeatTransferCoefficient = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSideHeatTransferCoefficient = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSideRe = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSideRe = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSideVelocity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSideVelocity = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // comboBoxFlowDirection
         // 
         this.comboBoxFlowDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxFlowDirection.Items.AddRange(new object[] {
                                                                   "Parallel",
                                                                   "Counter",
                                                                   "Cross"});
         this.comboBoxFlowDirection.Location = new System.Drawing.Point(0, 0);
         this.comboBoxFlowDirection.Name = "comboBoxFlowDirection";
         this.comboBoxFlowDirection.Size = new System.Drawing.Size(80, 21);
         this.comboBoxFlowDirection.TabIndex = 10;
         this.comboBoxFlowDirection.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         this.comboBoxFlowDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxFlowDirection_SelectedIndexChanged);
         // 
         // textBoxNumberOfHeatTransferUnits
         // 
         this.textBoxNumberOfHeatTransferUnits.Location = new System.Drawing.Point(0, 140);
         this.textBoxNumberOfHeatTransferUnits.Name = "textBoxNumberOfHeatTransferUnits";
         this.textBoxNumberOfHeatTransferUnits.Size = new System.Drawing.Size(80, 20);
         this.textBoxNumberOfHeatTransferUnits.TabIndex = 17;
         this.textBoxNumberOfHeatTransferUnits.Text = "";
         this.textBoxNumberOfHeatTransferUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxNumberOfHeatTransferUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalHeatTransferArea
         // 
         this.textBoxTotalHeatTransferArea.Location = new System.Drawing.Point(0, 120);
         this.textBoxTotalHeatTransferArea.Name = "textBoxTotalHeatTransferArea";
         this.textBoxTotalHeatTransferArea.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalHeatTransferArea.TabIndex = 16;
         this.textBoxTotalHeatTransferArea.Text = "";
         this.textBoxTotalHeatTransferArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalHeatTransferArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSideFoulingFactor
         // 
         this.textBoxHotSideFoulingFactor.Location = new System.Drawing.Point(0, 80);
         this.textBoxHotSideFoulingFactor.Name = "textBoxHotSideFoulingFactor";
         this.textBoxHotSideFoulingFactor.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSideFoulingFactor.TabIndex = 14;
         this.textBoxHotSideFoulingFactor.Text = "";
         this.textBoxHotSideFoulingFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSideFoulingFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSideFoulingFactor
         // 
         this.textBoxColdSideFoulingFactor.Location = new System.Drawing.Point(0, 60);
         this.textBoxColdSideFoulingFactor.Name = "textBoxColdSideFoulingFactor";
         this.textBoxColdSideFoulingFactor.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSideFoulingFactor.TabIndex = 13;
         this.textBoxColdSideFoulingFactor.Text = "";
         this.textBoxColdSideFoulingFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSideFoulingFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSideHeatTransferCoefficient
         // 
         this.textBoxHotSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 40);
         this.textBoxHotSideHeatTransferCoefficient.Name = "textBoxHotSideHeatTransferCoefficient";
         this.textBoxHotSideHeatTransferCoefficient.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSideHeatTransferCoefficient.TabIndex = 12;
         this.textBoxHotSideHeatTransferCoefficient.Text = "";
         this.textBoxHotSideHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSideHeatTransferCoefficient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxExchangerEffectiveness
         // 
         this.textBoxExchangerEffectiveness.Location = new System.Drawing.Point(0, 160);
         this.textBoxExchangerEffectiveness.Name = "textBoxExchangerEffectiveness";
         this.textBoxExchangerEffectiveness.Size = new System.Drawing.Size(80, 20);
         this.textBoxExchangerEffectiveness.TabIndex = 18;
         this.textBoxExchangerEffectiveness.Text = "";
         this.textBoxExchangerEffectiveness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxExchangerEffectiveness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalHeatTransferCoefficient
         // 
         this.textBoxTotalHeatTransferCoefficient.Location = new System.Drawing.Point(0, 100);
         this.textBoxTotalHeatTransferCoefficient.Name = "textBoxTotalHeatTransferCoefficient";
         this.textBoxTotalHeatTransferCoefficient.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalHeatTransferCoefficient.TabIndex = 15;
         this.textBoxTotalHeatTransferCoefficient.Text = "";
         this.textBoxTotalHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalHeatTransferCoefficient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSideHeatTransferCoefficient
         // 
         this.textBoxColdSideHeatTransferCoefficient.Location = new System.Drawing.Point(0, 20);
         this.textBoxColdSideHeatTransferCoefficient.Name = "textBoxColdSideHeatTransferCoefficient";
         this.textBoxColdSideHeatTransferCoefficient.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSideHeatTransferCoefficient.TabIndex = 11;
         this.textBoxColdSideHeatTransferCoefficient.Text = "";
         this.textBoxColdSideHeatTransferCoefficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSideHeatTransferCoefficient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSideRe
         // 
         this.textBoxColdSideRe.Location = new System.Drawing.Point(0, 200);
         this.textBoxColdSideRe.Name = "textBoxColdSideRe";
         this.textBoxColdSideRe.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSideRe.TabIndex = 20;
         this.textBoxColdSideRe.Text = "";
         this.textBoxColdSideRe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSideRe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSideRe
         // 
         this.textBoxHotSideRe.Location = new System.Drawing.Point(0, 240);
         this.textBoxHotSideRe.Name = "textBoxHotSideRe";
         this.textBoxHotSideRe.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSideRe.TabIndex = 22;
         this.textBoxHotSideRe.Text = "";
         this.textBoxHotSideRe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSideRe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSideVelocity
         // 
         this.textBoxColdSideVelocity.Location = new System.Drawing.Point(0, 180);
         this.textBoxColdSideVelocity.Name = "textBoxColdSideVelocity";
         this.textBoxColdSideVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSideVelocity.TabIndex = 19;
         this.textBoxColdSideVelocity.Text = "";
         this.textBoxColdSideVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSideVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSideVelocity
         // 
         this.textBoxHotSideVelocity.Location = new System.Drawing.Point(0, 220);
         this.textBoxHotSideVelocity.Name = "textBoxHotSideVelocity";
         this.textBoxHotSideVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSideVelocity.TabIndex = 21;
         this.textBoxHotSideVelocity.Text = "";
         this.textBoxHotSideVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSideVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HXRatingSimpleGenericPlateAndFrameValuesControl
         // 
         this.Controls.Add(this.textBoxHotSideVelocity);
         this.Controls.Add(this.textBoxColdSideVelocity);
         this.Controls.Add(this.textBoxHotSideRe);
         this.Controls.Add(this.textBoxColdSideRe);
         this.Controls.Add(this.comboBoxFlowDirection);
         this.Controls.Add(this.textBoxNumberOfHeatTransferUnits);
         this.Controls.Add(this.textBoxTotalHeatTransferArea);
         this.Controls.Add(this.textBoxHotSideFoulingFactor);
         this.Controls.Add(this.textBoxColdSideFoulingFactor);
         this.Controls.Add(this.textBoxHotSideHeatTransferCoefficient);
         this.Controls.Add(this.textBoxExchangerEffectiveness);
         this.Controls.Add(this.textBoxTotalHeatTransferCoefficient);
         this.Controls.Add(this.textBoxColdSideHeatTransferCoefficient);
         this.Name = "HXRatingSimpleGenericPlateAndFrameValuesControl";
         this.Size = new System.Drawing.Size(80, 260);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, HXRatingModelPlateAndFrame rating)
      {
         this.textBoxNumberOfHeatTransferUnits.InitializeVariable(flowsheet, rating.NumberOfHeatTransferUnits);
         this.textBoxTotalHeatTransferArea.InitializeVariable(flowsheet, rating.TotalHeatTransferArea);
         this.textBoxHotSideFoulingFactor.InitializeVariable(flowsheet, rating.HotSideFoulingFactor);
         this.textBoxColdSideFoulingFactor.InitializeVariable(flowsheet, rating.ColdSideFoulingFactor);
         this.textBoxHotSideHeatTransferCoefficient.InitializeVariable(flowsheet, rating.HotSideHeatTransferCoefficient);
         this.textBoxExchangerEffectiveness.InitializeVariable(flowsheet, rating.ExchangerEffectiveness);
         this.textBoxTotalHeatTransferCoefficient.InitializeVariable(flowsheet, rating.TotalHeatTransferCoefficient);
         this.textBoxColdSideHeatTransferCoefficient.InitializeVariable(flowsheet, rating.ColdSideHeatTransferCoefficient);
         this.textBoxColdSideRe.InitializeVariable(flowsheet, rating.ColdSideRe);
         this.textBoxHotSideRe.InitializeVariable(flowsheet, rating.HotSideRe);
         this.textBoxColdSideVelocity.InitializeVariable(flowsheet, rating.ColdSideVelocity);
         this.textBoxHotSideVelocity.InitializeVariable(flowsheet, rating.HotSideVelocity);
      }

      public void InitializeTheUI(Flowsheet flowsheet, HeatExchanger heatExchanger)
      {
         this.inConstruction = true;
         this.heatExchanger = heatExchanger;
         HXRatingModelPlateAndFrame ratingModel = heatExchanger.CurrentRatingModel as HXRatingModelPlateAndFrame;
         this.InitializeVariableTextBoxes(flowsheet, ratingModel);
         heatExchanger.SolveComplete += new SolveCompleteEventHandler(heatExchanger_SolveComplete);
         this.inConstruction = false;
         this.SetFlowDirection(ratingModel.FlowDirection);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.comboBoxFlowDirection);
         list.Add(this.textBoxColdSideHeatTransferCoefficient);
         list.Add(this.textBoxHotSideHeatTransferCoefficient);
         list.Add(this.textBoxColdSideFoulingFactor);
         list.Add(this.textBoxHotSideFoulingFactor);
         list.Add(this.textBoxTotalHeatTransferCoefficient);
         list.Add(this.textBoxTotalHeatTransferArea);
         list.Add(this.textBoxNumberOfHeatTransferUnits);
         list.Add(this.textBoxExchangerEffectiveness);
         list.Add(this.textBoxColdSideVelocity);
         list.Add(this.textBoxColdSideRe);
         list.Add(this.textBoxHotSideVelocity);
         list.Add(this.textBoxHotSideRe);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void SetFlowDirection(FlowDirection direction)
      {
         if (direction == FlowDirection.Counter)
            this.comboBoxFlowDirection.SelectedIndex = HXRatingSimpleGenericValuesControl.INDEX_COUNTER;
         else if (direction == FlowDirection.Cross)
            this.comboBoxFlowDirection.SelectedIndex = HXRatingSimpleGenericValuesControl.INDEX_CROSS;
         else if (direction == FlowDirection.Parallel)
            this.comboBoxFlowDirection.SelectedIndex = HXRatingSimpleGenericValuesControl.INDEX_PARALLEL;
      }

      private void comboBoxFlowDirection_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;

            HXRatingModel ratingModel = heatExchanger.CurrentRatingModel as HXRatingModel;
            int idx = this.comboBoxFlowDirection.SelectedIndex;
            if (idx == HXRatingSimpleGenericValuesControl.INDEX_PARALLEL)
               error = ratingModel.SpecifyFlowDirection(FlowDirection.Parallel);
            else if (idx == HXRatingSimpleGenericValuesControl.INDEX_COUNTER)
               error = ratingModel.SpecifyFlowDirection(FlowDirection.Counter);
            else if (idx == HXRatingSimpleGenericValuesControl.INDEX_CROSS)
               error = ratingModel.SpecifyFlowDirection(FlowDirection.Cross);

            if (error != null)
            {
               UI.ShowError(error);
               this.SetFlowDirection(ratingModel.FlowDirection);
            }
         }
      }

      private void heatExchanger_SolveComplete(object sender, SolveState solveState)
      {
         HXRatingModel ratingModel = (sender as HeatExchanger).CurrentRatingModel as HXRatingModel;
         FlowDirection flowDirection = ratingModel.FlowDirection;
         this.SetFlowDirection(flowDirection);
      }
	}
}
