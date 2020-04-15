using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneRatingValuesControl.
	/// </summary>
	public class CycloneRatingValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 440;

      // CycloneInletConfiguration
      public const int INDEX_TANGENTIAL = 0;
      public const int INDEX_SCROLL = 1;
      public const int INDEX_VOLUTE = 2;
      
      // ParticleTypeGroup
      public const int INDEX_A = 0;
      public const int INDEX_B = 1;
      public const int INDEX_C = 2;
      public const int INDEX_D = 3;

      private bool inConstruction;

      private Cyclone cyclone;

      private System.Windows.Forms.ComboBox comboBoxParticleTypeGroup;
      private ProsimoUI.ProcessVarTextBox textBoxNumberOfCyclones;
      private ProsimoUI.ProcessVarTextBox textBoxCutParticleDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxParticleBulkDensity;
      private System.Windows.Forms.ComboBox comboBoxInletConfiguration;
      private ProsimoUI.ProcessVarTextBox textBoxParticleDensity;
      private ProsimoUI.ProcessVarTextBox textBoxOutletWallThickness;
      private ProsimoUI.ProcessVarTextBox textBoxOutletInnerDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxCycloneDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxInletHeightToWidthRatio;
      private ProsimoUI.ProcessVarTextBox textBoxInletHeight;
      private ProsimoUI.ProcessVarTextBox textBoxInletWidth;
      private ProsimoUI.ProcessVarTextBox textBoxInletVelocity;
      private ProsimoUI.ProcessVarTextBox textBoxExternalVesselDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxDiplegDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxOutletTubeLengthBelowRoof;
      private ProsimoUI.ProcessVarTextBox textBoxOutletBelowRoofToInletHeightRatio;
      private ProsimoUI.ProcessVarTextBox textBoxConeAngle;
      private ProsimoUI.ProcessVarTextBox textBoxBarrelLength;
      private ProsimoUI.ProcessVarTextBox textBoxConeLength;
      private ProsimoUI.ProcessVarTextBox textBoxBarrelPlusConeLength;
      private ProsimoUI.ProcessVarTextBox textBoxNaturalVortexLength;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneRatingValuesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.cyclone != null)
            this.cyclone.SolveComplete -= new SolveCompleteEventHandler(cyclone_SolveComplete);
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
         this.textBoxOutletWallThickness = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletInnerDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCycloneDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletHeightToWidthRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletHeight = new ProsimoUI.ProcessVarTextBox();
         this.textBoxInletWidth = new ProsimoUI.ProcessVarTextBox();
         this.textBoxCutParticleDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxParticleBulkDensity = new ProsimoUI.ProcessVarTextBox();
         this.comboBoxInletConfiguration = new System.Windows.Forms.ComboBox();
         this.comboBoxParticleTypeGroup = new System.Windows.Forms.ComboBox();
         this.textBoxInletVelocity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxExternalVesselDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxDiplegDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxParticleDensity = new ProsimoUI.ProcessVarTextBox();
         this.textBoxNumberOfCyclones = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletTubeLengthBelowRoof = new ProsimoUI.ProcessVarTextBox();
         this.textBoxOutletBelowRoofToInletHeightRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxConeAngle = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBarrelLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxConeLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBarrelPlusConeLength = new ProsimoUI.ProcessVarTextBox();
         this.textBoxNaturalVortexLength = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxOutletWallThickness
         // 
         this.textBoxOutletWallThickness.Location = new System.Drawing.Point(0, 220);
         this.textBoxOutletWallThickness.Name = "textBoxOutletWallThickness";
         this.textBoxOutletWallThickness.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletWallThickness.TabIndex = 12;
         this.textBoxOutletWallThickness.Text = "";
         this.textBoxOutletWallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletWallThickness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletInnerDiameter
         // 
         this.textBoxOutletInnerDiameter.Location = new System.Drawing.Point(0, 200);
         this.textBoxOutletInnerDiameter.Name = "textBoxOutletInnerDiameter";
         this.textBoxOutletInnerDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletInnerDiameter.TabIndex = 11;
         this.textBoxOutletInnerDiameter.Text = "";
         this.textBoxOutletInnerDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletInnerDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCycloneDiameter
         // 
         this.textBoxCycloneDiameter.Location = new System.Drawing.Point(0, 180);
         this.textBoxCycloneDiameter.Name = "textBoxCycloneDiameter";
         this.textBoxCycloneDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxCycloneDiameter.TabIndex = 10;
         this.textBoxCycloneDiameter.Text = "";
         this.textBoxCycloneDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCycloneDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletHeightToWidthRatio
         // 
         this.textBoxInletHeightToWidthRatio.Location = new System.Drawing.Point(0, 160);
         this.textBoxInletHeightToWidthRatio.Name = "textBoxInletHeightToWidthRatio";
         this.textBoxInletHeightToWidthRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletHeightToWidthRatio.TabIndex = 9;
         this.textBoxInletHeightToWidthRatio.Text = "";
         this.textBoxInletHeightToWidthRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletHeightToWidthRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletHeight
         // 
         this.textBoxInletHeight.Location = new System.Drawing.Point(0, 140);
         this.textBoxInletHeight.Name = "textBoxInletHeight";
         this.textBoxInletHeight.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletHeight.TabIndex = 8;
         this.textBoxInletHeight.Text = "";
         this.textBoxInletHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxInletWidth
         // 
         this.textBoxInletWidth.Location = new System.Drawing.Point(0, 120);
         this.textBoxInletWidth.Name = "textBoxInletWidth";
         this.textBoxInletWidth.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletWidth.TabIndex = 7;
         this.textBoxInletWidth.Text = "";
         this.textBoxInletWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxCutParticleDiameter
         // 
         this.textBoxCutParticleDiameter.Location = new System.Drawing.Point(0, 80);
         this.textBoxCutParticleDiameter.Name = "textBoxCutParticleDiameter";
         this.textBoxCutParticleDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxCutParticleDiameter.TabIndex = 5;
         this.textBoxCutParticleDiameter.Text = "";
         this.textBoxCutParticleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxCutParticleDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxParticleBulkDensity
         // 
         this.textBoxParticleBulkDensity.Location = new System.Drawing.Point(0, 60);
         this.textBoxParticleBulkDensity.Name = "textBoxParticleBulkDensity";
         this.textBoxParticleBulkDensity.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleBulkDensity.TabIndex = 4;
         this.textBoxParticleBulkDensity.Text = "";
         this.textBoxParticleBulkDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParticleBulkDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxInletConfiguration
         // 
         this.comboBoxInletConfiguration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxInletConfiguration.Items.AddRange(new object[] {
                                                                        "Tangential",
                                                                        "Scroll",
                                                                        "Volute"});
         this.comboBoxInletConfiguration.Location = new System.Drawing.Point(0, 100);
         this.comboBoxInletConfiguration.Name = "comboBoxInletConfiguration";
         this.comboBoxInletConfiguration.Size = new System.Drawing.Size(80, 21);
         this.comboBoxInletConfiguration.TabIndex = 6;
         this.comboBoxInletConfiguration.SelectedIndexChanged += new System.EventHandler(this.comboBoxInletConfiguration_SelectedIndexChanged);
         // 
         // comboBoxParticleTypeGroup
         // 
         this.comboBoxParticleTypeGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxParticleTypeGroup.Items.AddRange(new object[] {
                                                                       "A",
                                                                       "B",
                                                                       "C",
                                                                       "D"});
         this.comboBoxParticleTypeGroup.Location = new System.Drawing.Point(0, 20);
         this.comboBoxParticleTypeGroup.Name = "comboBoxParticleTypeGroup";
         this.comboBoxParticleTypeGroup.Size = new System.Drawing.Size(80, 21);
         this.comboBoxParticleTypeGroup.TabIndex = 2;
         this.comboBoxParticleTypeGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxParticleTypeGroup_SelectedIndexChanged);
         // 
         // textBoxInletVelocity
         // 
         this.textBoxInletVelocity.Location = new System.Drawing.Point(0, 320);
         this.textBoxInletVelocity.Name = "textBoxInletVelocity";
         this.textBoxInletVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxInletVelocity.TabIndex = 17;
         this.textBoxInletVelocity.Text = "";
         this.textBoxInletVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxInletVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxExternalVesselDiameter
         // 
         this.textBoxExternalVesselDiameter.Location = new System.Drawing.Point(0, 300);
         this.textBoxExternalVesselDiameter.Name = "textBoxExternalVesselDiameter";
         this.textBoxExternalVesselDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxExternalVesselDiameter.TabIndex = 16;
         this.textBoxExternalVesselDiameter.Text = "";
         this.textBoxExternalVesselDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxExternalVesselDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxDiplegDiameter
         // 
         this.textBoxDiplegDiameter.Location = new System.Drawing.Point(0, 280);
         this.textBoxDiplegDiameter.Name = "textBoxDiplegDiameter";
         this.textBoxDiplegDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxDiplegDiameter.TabIndex = 15;
         this.textBoxDiplegDiameter.Text = "";
         this.textBoxDiplegDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxDiplegDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxParticleDensity
         // 
         this.textBoxParticleDensity.Location = new System.Drawing.Point(0, 40);
         this.textBoxParticleDensity.Name = "textBoxParticleDensity";
         this.textBoxParticleDensity.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleDensity.TabIndex = 3;
         this.textBoxParticleDensity.Text = "";
         this.textBoxParticleDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxParticleDensity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxNumberOfCyclones
         // 
         this.textBoxNumberOfCyclones.Location = new System.Drawing.Point(0, 0);
         this.textBoxNumberOfCyclones.Name = "textBoxNumberOfCyclones";
         this.textBoxNumberOfCyclones.Size = new System.Drawing.Size(80, 20);
         this.textBoxNumberOfCyclones.TabIndex = 1;
         this.textBoxNumberOfCyclones.Text = "";
         this.textBoxNumberOfCyclones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxNumberOfCyclones.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletTubeLengthBelowRoof
         // 
         this.textBoxOutletTubeLengthBelowRoof.Location = new System.Drawing.Point(0, 240);
         this.textBoxOutletTubeLengthBelowRoof.Name = "textBoxOutletTubeLengthBelowRoof";
         this.textBoxOutletTubeLengthBelowRoof.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletTubeLengthBelowRoof.TabIndex = 13;
         this.textBoxOutletTubeLengthBelowRoof.Text = "";
         this.textBoxOutletTubeLengthBelowRoof.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletTubeLengthBelowRoof.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxOutletBelowRoofToInletHeightRatio
         // 
         this.textBoxOutletBelowRoofToInletHeightRatio.Location = new System.Drawing.Point(0, 260);
         this.textBoxOutletBelowRoofToInletHeightRatio.Name = "textBoxOutletBelowRoofToInletHeightRatio";
         this.textBoxOutletBelowRoofToInletHeightRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletBelowRoofToInletHeightRatio.TabIndex = 14;
         this.textBoxOutletBelowRoofToInletHeightRatio.Text = "";
         this.textBoxOutletBelowRoofToInletHeightRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletBelowRoofToInletHeightRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxConeAngle
         // 
         this.textBoxConeAngle.Location = new System.Drawing.Point(0, 340);
         this.textBoxConeAngle.Name = "textBoxConeAngle";
         this.textBoxConeAngle.Size = new System.Drawing.Size(80, 20);
         this.textBoxConeAngle.TabIndex = 18;
         this.textBoxConeAngle.Text = "";
         this.textBoxConeAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxConeAngle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBarrelLength
         // 
         this.textBoxBarrelLength.Location = new System.Drawing.Point(0, 380);
         this.textBoxBarrelLength.Name = "textBoxBarrelLength";
         this.textBoxBarrelLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxBarrelLength.TabIndex = 20;
         this.textBoxBarrelLength.Text = "";
         this.textBoxBarrelLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBarrelLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxConeLength
         // 
         this.textBoxConeLength.Location = new System.Drawing.Point(0, 400);
         this.textBoxConeLength.Name = "textBoxConeLength";
         this.textBoxConeLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxConeLength.TabIndex = 21;
         this.textBoxConeLength.Text = "";
         this.textBoxConeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxConeLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBarrelPlusConeLength
         // 
         this.textBoxBarrelPlusConeLength.Location = new System.Drawing.Point(0, 420);
         this.textBoxBarrelPlusConeLength.Name = "textBoxBarrelPlusConeLength";
         this.textBoxBarrelPlusConeLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxBarrelPlusConeLength.TabIndex = 22;
         this.textBoxBarrelPlusConeLength.Text = "";
         this.textBoxBarrelPlusConeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBarrelPlusConeLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxNaturalVortexLength
         // 
         this.textBoxNaturalVortexLength.Location = new System.Drawing.Point(0, 360);
         this.textBoxNaturalVortexLength.Name = "textBoxNaturalVortexLength";
         this.textBoxNaturalVortexLength.Size = new System.Drawing.Size(80, 20);
         this.textBoxNaturalVortexLength.TabIndex = 19;
         this.textBoxNaturalVortexLength.Text = "";
         this.textBoxNaturalVortexLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxNaturalVortexLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // CycloneRatingValuesControl
         // 
         this.Controls.Add(this.textBoxNaturalVortexLength);
         this.Controls.Add(this.textBoxBarrelPlusConeLength);
         this.Controls.Add(this.textBoxConeLength);
         this.Controls.Add(this.textBoxBarrelLength);
         this.Controls.Add(this.textBoxConeAngle);
         this.Controls.Add(this.textBoxOutletBelowRoofToInletHeightRatio);
         this.Controls.Add(this.textBoxOutletTubeLengthBelowRoof);
         this.Controls.Add(this.textBoxOutletWallThickness);
         this.Controls.Add(this.textBoxOutletInnerDiameter);
         this.Controls.Add(this.textBoxCycloneDiameter);
         this.Controls.Add(this.textBoxInletHeightToWidthRatio);
         this.Controls.Add(this.textBoxInletHeight);
         this.Controls.Add(this.textBoxInletWidth);
         this.Controls.Add(this.textBoxCutParticleDiameter);
         this.Controls.Add(this.textBoxParticleBulkDensity);
         this.Controls.Add(this.comboBoxInletConfiguration);
         this.Controls.Add(this.comboBoxParticleTypeGroup);
         this.Controls.Add(this.textBoxInletVelocity);
         this.Controls.Add(this.textBoxExternalVesselDiameter);
         this.Controls.Add(this.textBoxDiplegDiameter);
         this.Controls.Add(this.textBoxParticleDensity);
         this.Controls.Add(this.textBoxNumberOfCyclones);
         this.Name = "CycloneRatingValuesControl";
         this.Size = new System.Drawing.Size(80, 440);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, CycloneRatingModel rating)
      {
         this.textBoxNumberOfCyclones.InitializeVariable(flowsheet.ApplicationPrefs, rating.NumberOfCyclones);
         this.textBoxCutParticleDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.CutParticleDiameter);
         this.textBoxParticleBulkDensity.InitializeVariable(flowsheet.ApplicationPrefs, rating.ParticleBulkDensity);
         this.textBoxParticleDensity.InitializeVariable(flowsheet.ApplicationPrefs, rating.ParticleDensity);
         this.textBoxOutletWallThickness.InitializeVariable(flowsheet.ApplicationPrefs, rating.OutletWallThickness);
         this.textBoxOutletInnerDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.OutletInnerDiameter);
         this.textBoxCycloneDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.CycloneDiameter);
         this.textBoxInletHeightToWidthRatio.InitializeVariable(flowsheet.ApplicationPrefs, rating.InletHeightToWidthRatio);
         this.textBoxInletHeight.InitializeVariable(flowsheet.ApplicationPrefs, rating.InletHeight);
         this.textBoxInletWidth.InitializeVariable(flowsheet.ApplicationPrefs, rating.InletWidth);
         this.textBoxInletVelocity.InitializeVariable(flowsheet.ApplicationPrefs, rating.InletVelocity);
         this.textBoxExternalVesselDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.ExternalVesselDiameter);
         this.textBoxDiplegDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.DiplegDiameter);
         this.textBoxOutletTubeLengthBelowRoof.InitializeVariable(flowsheet.ApplicationPrefs, rating.OutletTubeLengthBelowRoof);
         this.textBoxOutletBelowRoofToInletHeightRatio.InitializeVariable(flowsheet.ApplicationPrefs, rating.OutletBelowRoofToInletHeightRatio);
         this.textBoxConeAngle.InitializeVariable(flowsheet.ApplicationPrefs, rating.ConeAngle);
         this.textBoxBarrelLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.BarrelLength);
         this.textBoxConeLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.ConeLength);
         this.textBoxBarrelPlusConeLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.BarrelPlusConeLength);
         this.textBoxNaturalVortexLength.InitializeVariable(flowsheet.ApplicationPrefs, rating.NaturalVortexLength);
      }

      public void InitializeTheUI(Flowsheet flowsheet, Cyclone cyclone)
      {
         this.inConstruction = true;
         this.cyclone = cyclone;
         CycloneRatingModel cycloneRating = cyclone.CurrentRatingModel;
         this.InitializeVariableTextBoxes(flowsheet, cycloneRating);
         cyclone.SolveComplete += new SolveCompleteEventHandler(cyclone_SolveComplete);
         this.comboBoxInletConfiguration.SelectedIndex = -1;
         this.comboBoxParticleTypeGroup.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetInletConfiguration(cycloneRating.InletConfiguration);
         this.SetParticleTypeGroup(cycloneRating.ParticleTypeGroup);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxNumberOfCyclones);
         list.Add(this.comboBoxParticleTypeGroup);
         list.Add(this.textBoxParticleDensity);
         list.Add(this.textBoxParticleBulkDensity);
         list.Add(this.textBoxCutParticleDiameter);
         list.Add(this.comboBoxInletConfiguration);
         list.Add(this.textBoxInletWidth);
         list.Add(this.textBoxInletHeight);
         list.Add(this.textBoxInletHeightToWidthRatio);
         list.Add(this.textBoxCycloneDiameter);
         list.Add(this.textBoxOutletInnerDiameter);
         list.Add(this.textBoxOutletWallThickness);
         list.Add(this.textBoxOutletTubeLengthBelowRoof);
         list.Add(this.textBoxOutletBelowRoofToInletHeightRatio);
         list.Add(this.textBoxDiplegDiameter);
         list.Add(this.textBoxExternalVesselDiameter);
         list.Add(this.textBoxInletVelocity);
         list.Add(this.textBoxConeAngle);
         list.Add(this.textBoxNaturalVortexLength);
         list.Add(this.textBoxBarrelLength);
         list.Add(this.textBoxConeLength);
         list.Add(this.textBoxBarrelPlusConeLength);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      public void SetInletConfiguration(CycloneInletConfiguration config)
      {
         if (config == CycloneInletConfiguration.Scroll)
            this.comboBoxInletConfiguration.SelectedIndex = CycloneRatingValuesControl.INDEX_SCROLL;
         else if (config == CycloneInletConfiguration.Tangential)
            this.comboBoxInletConfiguration.SelectedIndex = CycloneRatingValuesControl.INDEX_TANGENTIAL;
         else if (config == CycloneInletConfiguration.Volute)
            this.comboBoxInletConfiguration.SelectedIndex = CycloneRatingValuesControl.INDEX_VOLUTE;
      }

      public void SetParticleTypeGroup(ParticleTypeGroup group)
      {
         if (group == ParticleTypeGroup.A)
            this.comboBoxParticleTypeGroup.SelectedIndex = CycloneRatingValuesControl.INDEX_A;
         else if (group == ParticleTypeGroup.B)
            this.comboBoxParticleTypeGroup.SelectedIndex = CycloneRatingValuesControl.INDEX_B;
         else if (group == ParticleTypeGroup.C)
            this.comboBoxParticleTypeGroup.SelectedIndex = CycloneRatingValuesControl.INDEX_C;
         else if (group == ParticleTypeGroup.D)
            this.comboBoxParticleTypeGroup.SelectedIndex = CycloneRatingValuesControl.INDEX_D;
      }

      private void cyclone_SolveComplete(object sender, SolveState solveState)
      {
         CycloneRatingModel rating = (sender as Cyclone).CurrentRatingModel;
         this.SetInletConfiguration(rating.InletConfiguration);
         this.SetParticleTypeGroup(rating.ParticleTypeGroup);
      }

      private void comboBoxParticleTypeGroup_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxParticleTypeGroup.SelectedIndex;
            if (idx == CycloneRatingValuesControl.INDEX_A)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyParticleTypeGroup(ParticleTypeGroup.A);
            }
            else if (idx == CycloneRatingValuesControl.INDEX_B)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyParticleTypeGroup(ParticleTypeGroup.B);
            }
            else if (idx == CycloneRatingValuesControl.INDEX_C)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyParticleTypeGroup(ParticleTypeGroup.C);
            }
            else if (idx == CycloneRatingValuesControl.INDEX_D)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyParticleTypeGroup(ParticleTypeGroup.D);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetParticleTypeGroup(this.cyclone.CurrentRatingModel.ParticleTypeGroup);
            }
         }
      }

      private void comboBoxInletConfiguration_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxInletConfiguration.SelectedIndex;
            if (idx == CycloneRatingValuesControl.INDEX_SCROLL)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyInletConfiguration(CycloneInletConfiguration.Scroll);
            }
            else if (idx == CycloneRatingValuesControl.INDEX_TANGENTIAL)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyInletConfiguration(CycloneInletConfiguration.Tangential);
            }
            else if (idx == CycloneRatingValuesControl.INDEX_VOLUTE)
            {
               error = this.cyclone.CurrentRatingModel.SpecifyInletConfiguration(CycloneInletConfiguration.Volute);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetInletConfiguration(this.cyclone.CurrentRatingModel.InletConfiguration);
            }
         }
      }
   }
}
