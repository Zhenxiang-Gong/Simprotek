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
	/// Summary description for HXRatingPlateAndFrameValuesControl.
	/// </summary>
	public class HXRatingPlateAndFrameValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 280;

      private HeatExchanger heatExchanger;
      private ProsimoUI.ProcessVarTextBox textBoxNumberOfPlates;
      private ProsimoUI.ProcessVarTextBox textBoxEnlargementFactor;
      private ProsimoUI.ProcessVarTextBox textBoxProjectedChannelLength;
      private ProsimoUI.ProcessVarTextBox textBoxChannelWidth;
      private ProsimoUI.ProcessVarTextBox textBoxPortDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxHorizontalPortDistance;
      private ProsimoUI.ProcessVarTextBox textBoxVerticalPortDistance;
      private ProsimoUI.ProcessVarTextBox textBoxCompressedPlatePakcLength;
      private ProsimoUI.ProcessVarTextBox textBoxPlatePitch;
      private ProsimoUI.ProcessVarTextBox textBoxActualEffectivePlateArea;
      private ProsimoUI.ProcessVarTextBox textBoxProjectedPlateArea;
      private ProsimoUI.ProcessVarTextBox textBoxPlateWallThickness;
      private ProsimoUI.ProcessVarTextBox textBoxHotSidePasses;
      private ProsimoUI.ProcessVarTextBox textBoxColdSidePasses;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingPlateAndFrameValuesControl()
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
         this.textBoxNumberOfPlates = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEnlargementFactor = new ProsimoUI.ProcessVarTextBox();
         this.textBoxProjectedChannelLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxChannelWidth = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHotSidePasses = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPortDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHorizontalPortDistance = new ProsimoUI.ProcessVarTextBox();
         this.textBoxVerticalPortDistance = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCompressedPlatePakcLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxColdSidePasses = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPlatePitch = new ProsimoUI.ProcessVarTextBox();
         this.textBoxActualEffectivePlateArea = new ProsimoUI.ProcessVarTextBox();
         this.textBoxProjectedPlateArea = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPlateWallThickness = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxNumberOfPlates
         // 
         this.textBoxNumberOfPlates.Location = new System.Drawing.Point(0, 140);
         this.textBoxNumberOfPlates.Name = "textBoxNumberOfPlates";
         this.textBoxNumberOfPlates.Size = new System.Drawing.Size(80, 20);
         this.textBoxNumberOfPlates.TabIndex = 8;
         this.textBoxNumberOfPlates.Text = "";
         this.textBoxNumberOfPlates.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxNumberOfPlates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEnlargementFactor
         // 
         this.textBoxEnlargementFactor.Location = new System.Drawing.Point(0, 40);
         this.textBoxEnlargementFactor.Name = "textBoxEnlargementFactor";
         this.textBoxEnlargementFactor.Size = new System.Drawing.Size(80, 20);
         this.textBoxEnlargementFactor.TabIndex = 3;
         this.textBoxEnlargementFactor.Text = "";
         this.textBoxEnlargementFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEnlargementFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxProjectedChannelLength
         // 
         this.textBoxProjectedChannelLength.Location = new System.Drawing.Point(0, 20);
         this.textBoxProjectedChannelLength.Name = "textBoxProjectedChannelLength";
         this.textBoxProjectedChannelLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxProjectedChannelLength.TabIndex = 2;
         this.textBoxProjectedChannelLength.Text = "";
         this.textBoxProjectedChannelLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxProjectedChannelLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxChannelWidth
         // 
         this.textBoxChannelWidth.Location = new System.Drawing.Point(0, 0);
         this.textBoxChannelWidth.Name = "textBoxChannelWidth";
         this.textBoxChannelWidth.Size = new System.Drawing.Size(80, 20);
         this.textBoxChannelWidth.TabIndex = 1;
         this.textBoxChannelWidth.Text = "";
         this.textBoxChannelWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxChannelWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHotSidePasses
         // 
         this.textBoxHotSidePasses.Location = new System.Drawing.Point(0, 180);
         this.textBoxHotSidePasses.Name = "textBoxHotSidePasses";
         this.textBoxHotSidePasses.Size = new System.Drawing.Size(80, 20);
         this.textBoxHotSidePasses.TabIndex = 10;
         this.textBoxHotSidePasses.Text = "";
         this.textBoxHotSidePasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHotSidePasses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPortDiameter
         // 
         this.textBoxPortDiameter.Location = new System.Drawing.Point(0, 200);
         this.textBoxPortDiameter.Name = "textBoxPortDiameter";
         this.textBoxPortDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxPortDiameter.TabIndex = 11;
         this.textBoxPortDiameter.Text = "";
         this.textBoxPortDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPortDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHorizontalPortDistance
         // 
         this.textBoxHorizontalPortDistance.Location = new System.Drawing.Point(0, 220);
         this.textBoxHorizontalPortDistance.Name = "textBoxHorizontalPortDistance";
         this.textBoxHorizontalPortDistance.Size = new System.Drawing.Size(80, 20);
         this.textBoxHorizontalPortDistance.TabIndex = 12;
         this.textBoxHorizontalPortDistance.Text = "";
         this.textBoxHorizontalPortDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHorizontalPortDistance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxVerticalPortDistance
         // 
         this.textBoxVerticalPortDistance.Location = new System.Drawing.Point(0, 240);
         this.textBoxVerticalPortDistance.Name = "textBoxVerticalPortDistance";
         this.textBoxVerticalPortDistance.Size = new System.Drawing.Size(80, 20);
         this.textBoxVerticalPortDistance.TabIndex = 13;
         this.textBoxVerticalPortDistance.Text = "";
         this.textBoxVerticalPortDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxVerticalPortDistance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCompressedPlatePakcLength
         // 
         this.textBoxCompressedPlatePakcLength.Location = new System.Drawing.Point(0, 260);
         this.textBoxCompressedPlatePakcLength.Name = "textBoxCompressedPlatePakcLength";
         this.textBoxCompressedPlatePakcLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxCompressedPlatePakcLength.TabIndex = 14;
         this.textBoxCompressedPlatePakcLength.Text = "";
         this.textBoxCompressedPlatePakcLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCompressedPlatePakcLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxColdSidePasses
         // 
         this.textBoxColdSidePasses.Location = new System.Drawing.Point(0, 160);
         this.textBoxColdSidePasses.Name = "textBoxColdSidePasses";
         this.textBoxColdSidePasses.Size = new System.Drawing.Size(80, 20);
         this.textBoxColdSidePasses.TabIndex = 9;
         this.textBoxColdSidePasses.Text = "";
         this.textBoxColdSidePasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxColdSidePasses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPlatePitch
         // 
         this.textBoxPlatePitch.Location = new System.Drawing.Point(0, 120);
         this.textBoxPlatePitch.Name = "textBoxPlatePitch";
         this.textBoxPlatePitch.Size = new System.Drawing.Size(80, 20);
         this.textBoxPlatePitch.TabIndex = 7;
         this.textBoxPlatePitch.Text = "";
         this.textBoxPlatePitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPlatePitch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxActualEffectivePlateArea
         // 
         this.textBoxActualEffectivePlateArea.Location = new System.Drawing.Point(0, 80);
         this.textBoxActualEffectivePlateArea.Name = "textBoxActualEffectivePlateArea";
         this.textBoxActualEffectivePlateArea.Size = new System.Drawing.Size(80, 20);
         this.textBoxActualEffectivePlateArea.TabIndex = 5;
         this.textBoxActualEffectivePlateArea.Text = "";
         this.textBoxActualEffectivePlateArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxActualEffectivePlateArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxProjectedPlateArea
         // 
         this.textBoxProjectedPlateArea.Location = new System.Drawing.Point(0, 60);
         this.textBoxProjectedPlateArea.Name = "textBoxProjectedPlateArea";
         this.textBoxProjectedPlateArea.Size = new System.Drawing.Size(80, 20);
         this.textBoxProjectedPlateArea.TabIndex = 4;
         this.textBoxProjectedPlateArea.Text = "";
         this.textBoxProjectedPlateArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxProjectedPlateArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPlateWallThickness
         // 
         this.textBoxPlateWallThickness.Location = new System.Drawing.Point(0, 100);
         this.textBoxPlateWallThickness.Name = "textBoxPlateWallThickness";
         this.textBoxPlateWallThickness.Size = new System.Drawing.Size(80, 20);
         this.textBoxPlateWallThickness.TabIndex = 6;
         this.textBoxPlateWallThickness.Text = "";
         this.textBoxPlateWallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPlateWallThickness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HXRatingPlateAndFrameValuesControl
         // 
         this.Controls.Add(this.textBoxPlateWallThickness);
         this.Controls.Add(this.textBoxProjectedPlateArea);
         this.Controls.Add(this.textBoxActualEffectivePlateArea);
         this.Controls.Add(this.textBoxPlatePitch);
         this.Controls.Add(this.textBoxColdSidePasses);
         this.Controls.Add(this.textBoxCompressedPlatePakcLength);
         this.Controls.Add(this.textBoxVerticalPortDistance);
         this.Controls.Add(this.textBoxHorizontalPortDistance);
         this.Controls.Add(this.textBoxPortDiameter);
         this.Controls.Add(this.textBoxHotSidePasses);
         this.Controls.Add(this.textBoxNumberOfPlates);
         this.Controls.Add(this.textBoxEnlargementFactor);
         this.Controls.Add(this.textBoxProjectedChannelLength);
         this.Controls.Add(this.textBoxChannelWidth);
         this.Name = "HXRatingPlateAndFrameValuesControl";
         this.Size = new System.Drawing.Size(80, 280);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, HXRatingModelPlateAndFrame rating)
      {
         this.textBoxProjectedPlateArea.InitializeVariable(flowsheet.ApplicationPrefs, rating.ProjectedPlateArea);
         this.textBoxActualEffectivePlateArea.InitializeVariable(flowsheet.ApplicationPrefs, rating.ActualEffectivePlateArea);
         this.textBoxPlatePitch.InitializeVariable(flowsheet.ApplicationPrefs, rating.PlatePitch);
         this.textBoxColdSidePasses.InitializeVariable(flowsheet.ApplicationPrefs, rating.ColdSidePasses);
         this.textBoxCompressedPlatePakcLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.CompressedPlatePackLength);
         this.textBoxVerticalPortDistance.InitializeVariable(flowsheet.ApplicationPrefs, rating.VerticalPortDistance);
         this.textBoxHorizontalPortDistance.InitializeVariable(flowsheet.ApplicationPrefs, rating.HorizontalPortDistance);
         this.textBoxPortDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.PortDiameter);
         this.textBoxHotSidePasses.InitializeVariable(flowsheet.ApplicationPrefs, rating.HotSidePasses);
         this.textBoxNumberOfPlates.InitializeVariable(flowsheet.ApplicationPrefs, rating.NumberOfPlates);
         this.textBoxEnlargementFactor.InitializeVariable(flowsheet.ApplicationPrefs, rating.EnlargementFactor);
         this.textBoxProjectedChannelLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.ProjectedChannelLength);
         this.textBoxChannelWidth.InitializeVariable(flowsheet.ApplicationPrefs, rating.ChannelWidth);
         this.textBoxPlateWallThickness.InitializeVariable(flowsheet.ApplicationPrefs, rating.PlateWallThickness);
      }

      public void InitializeTheUI(Flowsheet flowsheet, HeatExchanger heatExchanger)
      {
         this.heatExchanger = heatExchanger;
         HXRatingModelPlateAndFrame plateAndFrameRating = heatExchanger.CurrentRatingModel as HXRatingModelPlateAndFrame;
         this.InitializeVariableTextBoxes(flowsheet, plateAndFrameRating);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxChannelWidth);
         list.Add(this.textBoxProjectedChannelLength);
         list.Add(this.textBoxEnlargementFactor);
         list.Add(this.textBoxProjectedPlateArea);
         list.Add(this.textBoxActualEffectivePlateArea);
         list.Add(this.textBoxPlateWallThickness);
         list.Add(this.textBoxPlatePitch);
         list.Add(this.textBoxNumberOfPlates);
         list.Add(this.textBoxColdSidePasses);
         list.Add(this.textBoxHotSidePasses);
         list.Add(this.textBoxPortDiameter);
         list.Add(this.textBoxHorizontalPortDistance);
         list.Add(this.textBoxVerticalPortDistance);
         list.Add(this.textBoxCompressedPlatePakcLength);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
