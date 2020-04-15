using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneRatingLabelsControl.
	/// </summary>
	public class CycloneRatingLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 292;
      public const int HEIGHT = 440;
      private ProsimoUI.ProcessVarLabel labelNumberOfCyclones;
      private ProsimoUI.ProcessVarLabel labelInletWidth;
      private ProsimoUI.ProcessVarLabel labelCycloneDiameter;
      private ProsimoUI.ProcessVarLabel labelParticleDensity;
      private ProsimoUI.ProcessVarLabel labelParticleBulkDensity;
      private ProsimoUI.ProcessVarLabel labelCutParticleDiameter;
      private ProsimoUI.ProcessVarLabel labelInletHeight;
      private ProsimoUI.ProcessVarLabel labelInletHeightToWidthRatio;
      private System.Windows.Forms.Label labelParticleTypeGroup; //
      private System.Windows.Forms.Label labelInletConfiguration; //
      private ProsimoUI.ProcessVarLabel labelOutletInnerDiameter;
      private ProsimoUI.ProcessVarLabel labelOutletWallThickness;
      private ProsimoUI.ProcessVarLabel labelDiplegDiameter;
      private ProsimoUI.ProcessVarLabel labelExternalVesselDiameter;
      private ProsimoUI.ProcessVarLabel labelInletVelocity;
      private ProsimoUI.ProcessVarLabel labelOutletTubeLengthBelowRoof;
      private ProsimoUI.ProcessVarLabel labelOutletBelowRoofToInletHeightRatio;
      private ProsimoUI.ProcessVarLabel labelConeAngle;
      private ProsimoUI.ProcessVarLabel labelBarrelLength;
      private ProsimoUI.ProcessVarLabel labelConeLength;
      private ProsimoUI.ProcessVarLabel labelBarrelPlusConeLength;
      private ProsimoUI.ProcessVarLabel labelNaturalVortexLength;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneRatingLabelsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public CycloneRatingLabelsControl(CycloneRatingModel rating)
      {
         // NOTE: this constructor is not used
         InitializeComponent();
         this.InitializeVariableLabels(rating);
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
         this.labelNumberOfCyclones = new ProsimoUI.ProcessVarLabel();
         this.labelInletWidth = new ProsimoUI.ProcessVarLabel();
         this.labelCycloneDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelParticleDensity = new ProsimoUI.ProcessVarLabel();
         this.labelParticleBulkDensity = new ProsimoUI.ProcessVarLabel();
         this.labelCutParticleDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelInletHeight = new ProsimoUI.ProcessVarLabel();
         this.labelInletHeightToWidthRatio = new ProsimoUI.ProcessVarLabel();
         this.labelParticleTypeGroup = new System.Windows.Forms.Label();
         this.labelInletConfiguration = new System.Windows.Forms.Label();
         this.labelOutletInnerDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelOutletWallThickness = new ProsimoUI.ProcessVarLabel();
         this.labelDiplegDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelExternalVesselDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelInletVelocity = new ProsimoUI.ProcessVarLabel();
         this.labelBarrelPlusConeLength = new ProsimoUI.ProcessVarLabel();
         this.labelConeLength = new ProsimoUI.ProcessVarLabel();
         this.labelBarrelLength = new ProsimoUI.ProcessVarLabel();
         this.labelConeAngle = new ProsimoUI.ProcessVarLabel();
         this.labelOutletBelowRoofToInletHeightRatio = new ProsimoUI.ProcessVarLabel();
         this.labelOutletTubeLengthBelowRoof = new ProsimoUI.ProcessVarLabel();
         this.labelNaturalVortexLength = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelNumberOfCyclones
         // 
         this.labelNumberOfCyclones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNumberOfCyclones.Location = new System.Drawing.Point(0, 0);
         this.labelNumberOfCyclones.Name = "labelNumberOfCyclones";
         this.labelNumberOfCyclones.Size = new System.Drawing.Size(292, 20);
         this.labelNumberOfCyclones.TabIndex = 0;
         this.labelNumberOfCyclones.Text = "NumberOfCyclones";
         this.labelNumberOfCyclones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletWidth
         // 
         this.labelInletWidth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletWidth.Location = new System.Drawing.Point(0, 120);
         this.labelInletWidth.Name = "labelInletWidth";
         this.labelInletWidth.Size = new System.Drawing.Size(292, 20);
         this.labelInletWidth.TabIndex = 2;
         this.labelInletWidth.Text = "InletWidth";
         this.labelInletWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCycloneDiameter
         // 
         this.labelCycloneDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCycloneDiameter.Location = new System.Drawing.Point(0, 180);
         this.labelCycloneDiameter.Name = "labelCycloneDiameter";
         this.labelCycloneDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelCycloneDiameter.TabIndex = 4;
         this.labelCycloneDiameter.Text = "CycloneDiameter";
         this.labelCycloneDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelParticleDensity
         // 
         this.labelParticleDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelParticleDensity.Location = new System.Drawing.Point(0, 40);
         this.labelParticleDensity.Name = "labelParticleDensity";
         this.labelParticleDensity.Size = new System.Drawing.Size(292, 20);
         this.labelParticleDensity.TabIndex = 6;
         this.labelParticleDensity.Text = "ParticleDensity";
         this.labelParticleDensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelParticleBulkDensity
         // 
         this.labelParticleBulkDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelParticleBulkDensity.Location = new System.Drawing.Point(0, 60);
         this.labelParticleBulkDensity.Name = "labelParticleBulkDensity";
         this.labelParticleBulkDensity.Size = new System.Drawing.Size(292, 20);
         this.labelParticleBulkDensity.TabIndex = 8;
         this.labelParticleBulkDensity.Text = "ParticleBulkDensity";
         this.labelParticleBulkDensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCutParticleDiameter
         // 
         this.labelCutParticleDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCutParticleDiameter.Location = new System.Drawing.Point(0, 80);
         this.labelCutParticleDiameter.Name = "labelCutParticleDiameter";
         this.labelCutParticleDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelCutParticleDiameter.TabIndex = 10;
         this.labelCutParticleDiameter.Text = "CutParticleDiameter";
         this.labelCutParticleDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletHeight
         // 
         this.labelInletHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletHeight.Location = new System.Drawing.Point(0, 140);
         this.labelInletHeight.Name = "labelInletHeight";
         this.labelInletHeight.Size = new System.Drawing.Size(292, 20);
         this.labelInletHeight.TabIndex = 12;
         this.labelInletHeight.Text = "InletHeight";
         this.labelInletHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletHeightToWidthRatio
         // 
         this.labelInletHeightToWidthRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletHeightToWidthRatio.Location = new System.Drawing.Point(0, 160);
         this.labelInletHeightToWidthRatio.Name = "labelInletHeightToWidthRatio";
         this.labelInletHeightToWidthRatio.Size = new System.Drawing.Size(292, 20);
         this.labelInletHeightToWidthRatio.TabIndex = 14;
         this.labelInletHeightToWidthRatio.Text = "InletHeightToWidthRatio";
         this.labelInletHeightToWidthRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelParticleTypeGroup
         // 
         this.labelParticleTypeGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelParticleTypeGroup.Location = new System.Drawing.Point(0, 20);
         this.labelParticleTypeGroup.Name = "labelParticleTypeGroup";
         this.labelParticleTypeGroup.Size = new System.Drawing.Size(292, 20);
         this.labelParticleTypeGroup.TabIndex = 33;
         this.labelParticleTypeGroup.Text = "Particle Type Group:";
         this.labelParticleTypeGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletConfiguration
         // 
         this.labelInletConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletConfiguration.Location = new System.Drawing.Point(0, 100);
         this.labelInletConfiguration.Name = "labelInletConfiguration";
         this.labelInletConfiguration.Size = new System.Drawing.Size(292, 20);
         this.labelInletConfiguration.TabIndex = 34;
         this.labelInletConfiguration.Text = "Inlet Configuration:";
         this.labelInletConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletInnerDiameter
         // 
         this.labelOutletInnerDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletInnerDiameter.Location = new System.Drawing.Point(0, 200);
         this.labelOutletInnerDiameter.Name = "labelOutletInnerDiameter";
         this.labelOutletInnerDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelOutletInnerDiameter.TabIndex = 35;
         this.labelOutletInnerDiameter.Text = "OutletInnerDiameter";
         this.labelOutletInnerDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletWallThickness
         // 
         this.labelOutletWallThickness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletWallThickness.Location = new System.Drawing.Point(0, 220);
         this.labelOutletWallThickness.Name = "labelOutletWallThickness";
         this.labelOutletWallThickness.Size = new System.Drawing.Size(292, 20);
         this.labelOutletWallThickness.TabIndex = 36;
         this.labelOutletWallThickness.Text = "OutletWallThickness";
         this.labelOutletWallThickness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDiplegDiameter
         // 
         this.labelDiplegDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDiplegDiameter.Location = new System.Drawing.Point(0, 280);
         this.labelDiplegDiameter.Name = "labelDiplegDiameter";
         this.labelDiplegDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelDiplegDiameter.TabIndex = 37;
         this.labelDiplegDiameter.Text = "DiplegDiameter";
         this.labelDiplegDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelExternalVesselDiameter
         // 
         this.labelExternalVesselDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelExternalVesselDiameter.Location = new System.Drawing.Point(0, 300);
         this.labelExternalVesselDiameter.Name = "labelExternalVesselDiameter";
         this.labelExternalVesselDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelExternalVesselDiameter.TabIndex = 38;
         this.labelExternalVesselDiameter.Text = "ExternalVesselDiameter";
         this.labelExternalVesselDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelInletVelocity
         // 
         this.labelInletVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelInletVelocity.Location = new System.Drawing.Point(0, 320);
         this.labelInletVelocity.Name = "labelInletVelocity";
         this.labelInletVelocity.Size = new System.Drawing.Size(292, 20);
         this.labelInletVelocity.TabIndex = 39;
         this.labelInletVelocity.Text = "InletVelocity";
         this.labelInletVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBarrelPlusConeLength
         // 
         this.labelBarrelPlusConeLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBarrelPlusConeLength.Location = new System.Drawing.Point(0, 420);
         this.labelBarrelPlusConeLength.Name = "labelBarrelPlusConeLength";
         this.labelBarrelPlusConeLength.Size = new System.Drawing.Size(292, 20);
         this.labelBarrelPlusConeLength.TabIndex = 40;
         this.labelBarrelPlusConeLength.Text = "Barrel Plus Cone Length";
         this.labelBarrelPlusConeLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelConeLength
         // 
         this.labelConeLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelConeLength.Location = new System.Drawing.Point(0, 400);
         this.labelConeLength.Name = "labelConeLength";
         this.labelConeLength.Size = new System.Drawing.Size(292, 20);
         this.labelConeLength.TabIndex = 41;
         this.labelConeLength.Text = "Cone Length";
         this.labelConeLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBarrelLength
         // 
         this.labelBarrelLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBarrelLength.Location = new System.Drawing.Point(0, 380);
         this.labelBarrelLength.Name = "labelBarrelLength";
         this.labelBarrelLength.Size = new System.Drawing.Size(292, 20);
         this.labelBarrelLength.TabIndex = 42;
         this.labelBarrelLength.Text = "Barrel Length";
         this.labelBarrelLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelConeAngle
         // 
         this.labelConeAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelConeAngle.Location = new System.Drawing.Point(0, 340);
         this.labelConeAngle.Name = "labelConeAngle";
         this.labelConeAngle.Size = new System.Drawing.Size(292, 20);
         this.labelConeAngle.TabIndex = 43;
         this.labelConeAngle.Text = "Cone Angle";
         this.labelConeAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletBelowRoofToInletHeightRatio
         // 
         this.labelOutletBelowRoofToInletHeightRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletBelowRoofToInletHeightRatio.Location = new System.Drawing.Point(0, 260);
         this.labelOutletBelowRoofToInletHeightRatio.Name = "labelOutletBelowRoofToInletHeightRatio";
         this.labelOutletBelowRoofToInletHeightRatio.Size = new System.Drawing.Size(292, 20);
         this.labelOutletBelowRoofToInletHeightRatio.TabIndex = 44;
         this.labelOutletBelowRoofToInletHeightRatio.Text = "Outlet Below Roof To Inlet Height Ratio";
         this.labelOutletBelowRoofToInletHeightRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelOutletTubeLengthBelowRoof
         // 
         this.labelOutletTubeLengthBelowRoof.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletTubeLengthBelowRoof.Location = new System.Drawing.Point(0, 240);
         this.labelOutletTubeLengthBelowRoof.Name = "labelOutletTubeLengthBelowRoof";
         this.labelOutletTubeLengthBelowRoof.Size = new System.Drawing.Size(292, 20);
         this.labelOutletTubeLengthBelowRoof.TabIndex = 45;
         this.labelOutletTubeLengthBelowRoof.Text = "Outlet Tube Length Below Roof";
         this.labelOutletTubeLengthBelowRoof.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelNaturalVortexLength
         // 
         this.labelNaturalVortexLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelNaturalVortexLength.Location = new System.Drawing.Point(0, 360);
         this.labelNaturalVortexLength.Name = "labelNaturalVortexLength";
         this.labelNaturalVortexLength.Size = new System.Drawing.Size(292, 20);
         this.labelNaturalVortexLength.TabIndex = 46;
         this.labelNaturalVortexLength.Text = "NaturalVortexLength";
         this.labelNaturalVortexLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CycloneRatingLabelsControl
         // 
         this.Controls.Add(this.labelNaturalVortexLength);
         this.Controls.Add(this.labelOutletTubeLengthBelowRoof);
         this.Controls.Add(this.labelOutletBelowRoofToInletHeightRatio);
         this.Controls.Add(this.labelConeAngle);
         this.Controls.Add(this.labelBarrelLength);
         this.Controls.Add(this.labelConeLength);
         this.Controls.Add(this.labelBarrelPlusConeLength);
         this.Controls.Add(this.labelInletVelocity);
         this.Controls.Add(this.labelExternalVesselDiameter);
         this.Controls.Add(this.labelDiplegDiameter);
         this.Controls.Add(this.labelOutletWallThickness);
         this.Controls.Add(this.labelOutletInnerDiameter);
         this.Controls.Add(this.labelInletConfiguration);
         this.Controls.Add(this.labelParticleTypeGroup);
         this.Controls.Add(this.labelInletHeightToWidthRatio);
         this.Controls.Add(this.labelInletHeight);
         this.Controls.Add(this.labelCutParticleDiameter);
         this.Controls.Add(this.labelParticleBulkDensity);
         this.Controls.Add(this.labelParticleDensity);
         this.Controls.Add(this.labelCycloneDiameter);
         this.Controls.Add(this.labelInletWidth);
         this.Controls.Add(this.labelNumberOfCyclones);
         this.Name = "CycloneRatingLabelsControl";
         this.Size = new System.Drawing.Size(292, 440);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(CycloneRatingModel rating)
      {
         this.labelNumberOfCyclones.InitializeVariable(rating.NumberOfCyclones);
         this.labelInletWidth.InitializeVariable(rating.InletWidth);
         this.labelCycloneDiameter.InitializeVariable(rating.CycloneDiameter);
         this.labelParticleDensity.InitializeVariable(rating.ParticleDensity);
         this.labelParticleBulkDensity.InitializeVariable(rating.ParticleBulkDensity);
         this.labelCutParticleDiameter.InitializeVariable(rating.CutParticleDiameter);
         this.labelInletHeight.InitializeVariable(rating.InletHeight);
         this.labelInletHeightToWidthRatio.InitializeVariable(rating.InletHeightToWidthRatio);
         this.labelOutletInnerDiameter.InitializeVariable(rating.OutletInnerDiameter);
         this.labelOutletWallThickness.InitializeVariable(rating.OutletWallThickness);
         this.labelOutletTubeLengthBelowRoof.InitializeVariable(rating.OutletTubeLengthBelowRoof);
         this.labelOutletBelowRoofToInletHeightRatio.InitializeVariable(rating.OutletBelowRoofToInletHeightRatio);
         this.labelDiplegDiameter.InitializeVariable(rating.DiplegDiameter);
         this.labelExternalVesselDiameter.InitializeVariable(rating.ExternalVesselDiameter);
         this.labelInletVelocity.InitializeVariable(rating.InletVelocity);
         this.labelConeAngle.InitializeVariable(rating.ConeAngle);
         this.labelBarrelLength.InitializeVariable(rating.BarrelLength);
         this.labelConeLength.InitializeVariable(rating.ConeLength);
         this.labelBarrelPlusConeLength.InitializeVariable(rating.BarrelPlusConeLength);
         this.labelNaturalVortexLength.InitializeVariable(rating.NaturalVortexLength);
      }
	}
}
