using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingPlateAndFrameLabelsControl.
	/// </summary>
	public class HXRatingPlateAndFrameLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 280;
      private ProsimoUI.ProcessVarLabel labelNumberOfPlates;
      private ProsimoUI.ProcessVarLabel labelChannelWidth;
      private ProsimoUI.ProcessVarLabel labelEnlargementFactor;
      private ProsimoUI.ProcessVarLabel labelProjectedChannelLength;
      private ProsimoUI.ProcessVarLabel labelPortDiameter;
      private ProsimoUI.ProcessVarLabel labelPlatePitch;
      private ProsimoUI.ProcessVarLabel labelActualEffectivePlateArea;
      private ProsimoUI.ProcessVarLabel labelProjectedPlateArea;
      private ProsimoUI.ProcessVarLabel labelHorizontalPortDistance;
      private ProsimoUI.ProcessVarLabel labelVerticalPortDistance;
      private ProsimoUI.ProcessVarLabel labelCompressedPlatePakcLength;
      private ProsimoUI.ProcessVarLabel labelPlateWallThickness;
      private ProsimoUI.ProcessVarLabel labelHotSidePasses;
      private ProsimoUI.ProcessVarLabel labelColdSidePasses;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingPlateAndFrameLabelsControl()
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
         this.labelChannelWidth = new ProsimoUI.ProcessVarLabel();
         this.labelEnlargementFactor = new ProsimoUI.ProcessVarLabel();
         this.labelProjectedChannelLength = new ProsimoUI.ProcessVarLabel();
         this.labelNumberOfPlates = new ProsimoUI.ProcessVarLabel();
         this.labelHotSidePasses = new ProsimoUI.ProcessVarLabel();
         this.labelPortDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelHorizontalPortDistance = new ProsimoUI.ProcessVarLabel();
         this.labelVerticalPortDistance = new ProsimoUI.ProcessVarLabel();
         this.labelCompressedPlatePakcLength = new ProsimoUI.ProcessVarLabel();
         this.labelColdSidePasses = new ProsimoUI.ProcessVarLabel();
         this.labelPlatePitch = new ProsimoUI.ProcessVarLabel();
         this.labelActualEffectivePlateArea = new ProsimoUI.ProcessVarLabel();
         this.labelProjectedPlateArea = new ProsimoUI.ProcessVarLabel();
         this.labelPlateWallThickness = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelChannelWidth
         // 
         this.labelChannelWidth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelChannelWidth.Location = new System.Drawing.Point(0, 0);
         this.labelChannelWidth.Name = "labelChannelWidth";
         this.labelChannelWidth.Size = new System.Drawing.Size(292, 20);
         this.labelChannelWidth.TabIndex = 22;
         this.labelChannelWidth.Text = "ChannelWidth";
         this.labelChannelWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEnlargementFactor
         // 
         this.labelEnlargementFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEnlargementFactor.Location = new System.Drawing.Point(0, 40);
         this.labelEnlargementFactor.Name = "labelEnlargementFactor";
         this.labelEnlargementFactor.Size = new System.Drawing.Size(292, 20);
         this.labelEnlargementFactor.TabIndex = 23;
         this.labelEnlargementFactor.Text = "EnlargementFactor";
         this.labelEnlargementFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelProjectedChannelLength
         // 
         this.labelProjectedChannelLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelProjectedChannelLength.Location = new System.Drawing.Point(0, 20);
         this.labelProjectedChannelLength.Name = "labelProjectedChannelLength";
         this.labelProjectedChannelLength.Size = new System.Drawing.Size(292, 20);
         this.labelProjectedChannelLength.TabIndex = 24;
         this.labelProjectedChannelLength.Text = "ProjectedChannelLength";
         this.labelProjectedChannelLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNumberOfPlates
         // 
         this.labelNumberOfPlates.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfPlates.Location = new System.Drawing.Point(0, 140);
         this.labelNumberOfPlates.Name = "labelNumberOfPlates";
         this.labelNumberOfPlates.Size = new System.Drawing.Size(292, 20);
         this.labelNumberOfPlates.TabIndex = 25;
         this.labelNumberOfPlates.Text = "NumberOfPlates";
         this.labelNumberOfPlates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSidePasses
         // 
         this.labelHotSidePasses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSidePasses.Location = new System.Drawing.Point(0, 180);
         this.labelHotSidePasses.Name = "labelHotSidePasses";
         this.labelHotSidePasses.Size = new System.Drawing.Size(292, 20);
         this.labelHotSidePasses.TabIndex = 26;
         this.labelHotSidePasses.Text = "HotSidePasses";
         this.labelHotSidePasses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPortDiameter
         // 
         this.labelPortDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPortDiameter.Location = new System.Drawing.Point(0, 200);
         this.labelPortDiameter.Name = "labelPortDiameter";
         this.labelPortDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelPortDiameter.TabIndex = 27;
         this.labelPortDiameter.Text = "PortDiameter";
         this.labelPortDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHorizontalPortDistance
         // 
         this.labelHorizontalPortDistance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHorizontalPortDistance.Location = new System.Drawing.Point(0, 220);
         this.labelHorizontalPortDistance.Name = "labelHorizontalPortDistance";
         this.labelHorizontalPortDistance.Size = new System.Drawing.Size(292, 20);
         this.labelHorizontalPortDistance.TabIndex = 28;
         this.labelHorizontalPortDistance.Text = "HorizontalPortDistance";
         this.labelHorizontalPortDistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelVerticalPortDistance
         // 
         this.labelVerticalPortDistance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelVerticalPortDistance.Location = new System.Drawing.Point(0, 240);
         this.labelVerticalPortDistance.Name = "labelVerticalPortDistance";
         this.labelVerticalPortDistance.Size = new System.Drawing.Size(292, 20);
         this.labelVerticalPortDistance.TabIndex = 29;
         this.labelVerticalPortDistance.Text = "VerticalPortDistance";
         this.labelVerticalPortDistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCompressedPlatePakcLength
         // 
         this.labelCompressedPlatePakcLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCompressedPlatePakcLength.Location = new System.Drawing.Point(0, 260);
         this.labelCompressedPlatePakcLength.Name = "labelCompressedPlatePakcLength";
         this.labelCompressedPlatePakcLength.Size = new System.Drawing.Size(292, 20);
         this.labelCompressedPlatePakcLength.TabIndex = 30;
         this.labelCompressedPlatePakcLength.Text = "CompressedPlatePakcLength";
         this.labelCompressedPlatePakcLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSidePasses
         // 
         this.labelColdSidePasses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSidePasses.Location = new System.Drawing.Point(0, 160);
         this.labelColdSidePasses.Name = "labelColdSidePasses";
         this.labelColdSidePasses.Size = new System.Drawing.Size(292, 20);
         this.labelColdSidePasses.TabIndex = 31;
         this.labelColdSidePasses.Text = "ColdSidePasses";
         this.labelColdSidePasses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPlatePitch
         // 
         this.labelPlatePitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPlatePitch.Location = new System.Drawing.Point(0, 120);
         this.labelPlatePitch.Name = "labelPlatePitch";
         this.labelPlatePitch.Size = new System.Drawing.Size(292, 20);
         this.labelPlatePitch.TabIndex = 32;
         this.labelPlatePitch.Text = "PlatePitch";
         this.labelPlatePitch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelActualEffectivePlateArea
         // 
         this.labelActualEffectivePlateArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelActualEffectivePlateArea.Location = new System.Drawing.Point(0, 80);
         this.labelActualEffectivePlateArea.Name = "labelActualEffectivePlateArea";
         this.labelActualEffectivePlateArea.Size = new System.Drawing.Size(292, 20);
         this.labelActualEffectivePlateArea.TabIndex = 33;
         this.labelActualEffectivePlateArea.Text = "ActualEffectivePlateArea";
         this.labelActualEffectivePlateArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelProjectedPlateArea
         // 
         this.labelProjectedPlateArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelProjectedPlateArea.Location = new System.Drawing.Point(0, 60);
         this.labelProjectedPlateArea.Name = "labelProjectedPlateArea";
         this.labelProjectedPlateArea.Size = new System.Drawing.Size(292, 20);
         this.labelProjectedPlateArea.TabIndex = 34;
         this.labelProjectedPlateArea.Text = "ProjectedPlateArea";
         this.labelProjectedPlateArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPlateWallThickness
         // 
         this.labelPlateWallThickness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPlateWallThickness.Location = new System.Drawing.Point(0, 100);
         this.labelPlateWallThickness.Name = "labelPlateWallThickness";
         this.labelPlateWallThickness.Size = new System.Drawing.Size(292, 20);
         this.labelPlateWallThickness.TabIndex = 35;
         this.labelPlateWallThickness.Text = "PlateWallThickness";
         this.labelPlateWallThickness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRatingPlateAndFrameLabelsControl
         // 
         this.Controls.Add(this.labelPlateWallThickness);
         this.Controls.Add(this.labelProjectedPlateArea);
         this.Controls.Add(this.labelActualEffectivePlateArea);
         this.Controls.Add(this.labelPlatePitch);
         this.Controls.Add(this.labelColdSidePasses);
         this.Controls.Add(this.labelCompressedPlatePakcLength);
         this.Controls.Add(this.labelVerticalPortDistance);
         this.Controls.Add(this.labelHorizontalPortDistance);
         this.Controls.Add(this.labelPortDiameter);
         this.Controls.Add(this.labelHotSidePasses);
         this.Controls.Add(this.labelNumberOfPlates);
         this.Controls.Add(this.labelProjectedChannelLength);
         this.Controls.Add(this.labelEnlargementFactor);
         this.Controls.Add(this.labelChannelWidth);
         this.Name = "HXRatingPlateAndFrameLabelsControl";
         this.Size = new System.Drawing.Size(292, 280);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModelPlateAndFrame plateAndFrame)
      {
         this.labelProjectedPlateArea.InitializeVariable(plateAndFrame.ProjectedPlateArea);
         this.labelActualEffectivePlateArea.InitializeVariable(plateAndFrame.ActualEffectivePlateArea);
         this.labelPlatePitch.InitializeVariable(plateAndFrame.PlatePitch);
         this.labelColdSidePasses.InitializeVariable(plateAndFrame.ColdSidePasses);
         this.labelCompressedPlatePakcLength.InitializeVariable(plateAndFrame.CompressedPlatePackLength);
         this.labelVerticalPortDistance.InitializeVariable(plateAndFrame.VerticalPortDistance);
         this.labelHorizontalPortDistance.InitializeVariable(plateAndFrame.HorizontalPortDistance);
         this.labelPortDiameter.InitializeVariable(plateAndFrame.PortDiameter);
         this.labelHotSidePasses.InitializeVariable(plateAndFrame.HotSidePasses);
         this.labelNumberOfPlates.InitializeVariable(plateAndFrame.NumberOfPlates);
         this.labelProjectedChannelLength.InitializeVariable(plateAndFrame.ProjectedChannelLength);
         this.labelEnlargementFactor.InitializeVariable(plateAndFrame.EnlargementFactor);
         this.labelChannelWidth.InitializeVariable(plateAndFrame.ChannelWidth);
         this.labelPlateWallThickness.InitializeVariable(plateAndFrame.PlateWallThickness);
      }
	}
}
