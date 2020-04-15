using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HXRatingShellAndTubeBellDelawareLabelsControl.
	/// </summary>
	public class HXRatingShellAndTubeBellDelawareLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 400;

      private ProsimoUI.ProcessVarLabel labelShellPasses;
      private ProsimoUI.ProcessVarLabel labelTubePassesPerShellPass;
      private ProsimoUI.ProcessVarLabel labelTubeLengthBetweenTubeSheets;
      private ProsimoUI.ProcessVarLabel labelTubeWallThickness;
      private ProsimoUI.ProcessVarLabel labelTubeOuterDiameter;
      private ProsimoUI.ProcessVarLabel labelTubesPerTubePass;
      private ProsimoUI.ProcessVarLabel labelTubeInnerDiameter;
      private System.Windows.Forms.Label labelShellType;
      private System.Windows.Forms.Label labelTubeLayout;
      private ProsimoUI.ProcessVarLabel labelTubePitch;
      private ProsimoUI.ProcessVarLabel labelBaffleCut;
      private ProsimoUI.ProcessVarLabel labelBaffleSpacing;
      private ProsimoUI.ProcessVarLabel labelBaffles;
      private ProsimoUI.ProcessVarLabel labelShellToBaffleDiametralClearance;
      private ProsimoUI.ProcessVarLabel labelShellInnerDiameter;
      private ProsimoUI.ProcessVarLabel labelBundleToShellDiametralClearance;
      private ProsimoUI.ProcessVarLabel labelSealingStrips;
      private ProsimoUI.ProcessVarLabel labelEntranceBaffleSpacing;
      private ProsimoUI.ProcessVarLabel labelExitBaffleSpacing;
      private ProsimoUI.ProcessVarLabel labelTotalTubesInShell;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingShellAndTubeBellDelawareLabelsControl()
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
         this.labelShellPasses = new ProsimoUI.ProcessVarLabel();
         this.labelTubePassesPerShellPass = new ProsimoUI.ProcessVarLabel();
         this.labelTubeLengthBetweenTubeSheets = new ProsimoUI.ProcessVarLabel();
         this.labelTubeWallThickness = new ProsimoUI.ProcessVarLabel();
         this.labelTubeOuterDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelTubePitch = new ProsimoUI.ProcessVarLabel();
         this.labelTubesPerTubePass = new ProsimoUI.ProcessVarLabel();
         this.labelTubeInnerDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelShellType = new System.Windows.Forms.Label();
         this.labelTubeLayout = new System.Windows.Forms.Label();
         this.labelBaffleCut = new ProsimoUI.ProcessVarLabel();
         this.labelBaffleSpacing = new ProsimoUI.ProcessVarLabel();
         this.labelEntranceBaffleSpacing = new ProsimoUI.ProcessVarLabel();
         this.labelBaffles = new ProsimoUI.ProcessVarLabel();
         this.labelShellToBaffleDiametralClearance = new ProsimoUI.ProcessVarLabel();
         this.labelShellInnerDiameter = new ProsimoUI.ProcessVarLabel();
         this.labelBundleToShellDiametralClearance = new ProsimoUI.ProcessVarLabel();
         this.labelSealingStrips = new ProsimoUI.ProcessVarLabel();
         this.labelExitBaffleSpacing = new ProsimoUI.ProcessVarLabel();
         this.labelTotalTubesInShell = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelShellPasses
         // 
         this.labelShellPasses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellPasses.Location = new System.Drawing.Point(0, 120);
         this.labelShellPasses.Name = "labelShellPasses";
         this.labelShellPasses.Size = new System.Drawing.Size(292, 20);
         this.labelShellPasses.TabIndex = 22;
         this.labelShellPasses.Text = "ShellPasses";
         this.labelShellPasses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubePassesPerShellPass
         // 
         this.labelTubePassesPerShellPass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubePassesPerShellPass.Location = new System.Drawing.Point(0, 100);
         this.labelTubePassesPerShellPass.Name = "labelTubePassesPerShellPass";
         this.labelTubePassesPerShellPass.Size = new System.Drawing.Size(292, 20);
         this.labelTubePassesPerShellPass.TabIndex = 21;
         this.labelTubePassesPerShellPass.Text = "TubePassesPerShellPass";
         this.labelTubePassesPerShellPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeLengthBetweenTubeSheets
         // 
         this.labelTubeLengthBetweenTubeSheets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeLengthBetweenTubeSheets.Location = new System.Drawing.Point(0, 60);
         this.labelTubeLengthBetweenTubeSheets.Name = "labelTubeLengthBetweenTubeSheets";
         this.labelTubeLengthBetweenTubeSheets.Size = new System.Drawing.Size(292, 20);
         this.labelTubeLengthBetweenTubeSheets.TabIndex = 20;
         this.labelTubeLengthBetweenTubeSheets.Text = "TubeLengthBetweenTubeSheets";
         this.labelTubeLengthBetweenTubeSheets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeWallThickness
         // 
         this.labelTubeWallThickness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeWallThickness.Location = new System.Drawing.Point(0, 20);
         this.labelTubeWallThickness.Name = "labelTubeWallThickness";
         this.labelTubeWallThickness.Size = new System.Drawing.Size(292, 20);
         this.labelTubeWallThickness.TabIndex = 19;
         this.labelTubeWallThickness.Text = "TubeWallThickness";
         this.labelTubeWallThickness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeOuterDiameter
         // 
         this.labelTubeOuterDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeOuterDiameter.Location = new System.Drawing.Point(0, 0);
         this.labelTubeOuterDiameter.Name = "labelTubeOuterDiameter";
         this.labelTubeOuterDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelTubeOuterDiameter.TabIndex = 18;
         this.labelTubeOuterDiameter.Text = "TubeOuterDiameter ";
         this.labelTubeOuterDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubePitch
         // 
         this.labelTubePitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubePitch.Location = new System.Drawing.Point(0, 200);
         this.labelTubePitch.Name = "labelTubePitch";
         this.labelTubePitch.Size = new System.Drawing.Size(292, 20);
         this.labelTubePitch.TabIndex = 17;
         this.labelTubePitch.Text = "TubePitch";
         this.labelTubePitch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubesPerTubePass
         // 
         this.labelTubesPerTubePass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubesPerTubePass.Location = new System.Drawing.Point(0, 80);
         this.labelTubesPerTubePass.Name = "labelTubesPerTubePass";
         this.labelTubesPerTubePass.Size = new System.Drawing.Size(292, 20);
         this.labelTubesPerTubePass.TabIndex = 16;
         this.labelTubesPerTubePass.Text = "TubesPerTubePass";
         this.labelTubesPerTubePass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeInnerDiameter
         // 
         this.labelTubeInnerDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeInnerDiameter.Location = new System.Drawing.Point(0, 40);
         this.labelTubeInnerDiameter.Name = "labelTubeInnerDiameter";
         this.labelTubeInnerDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelTubeInnerDiameter.TabIndex = 15;
         this.labelTubeInnerDiameter.Text = "TubeInnerDiameter";
         this.labelTubeInnerDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelShellType
         // 
         this.labelShellType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellType.Location = new System.Drawing.Point(0, 160);
         this.labelShellType.Name = "labelShellType";
         this.labelShellType.Size = new System.Drawing.Size(292, 20);
         this.labelShellType.TabIndex = 32;
         this.labelShellType.Text = "Shell Type:";
         this.labelShellType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTubeLayout
         // 
         this.labelTubeLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTubeLayout.Location = new System.Drawing.Point(0, 180);
         this.labelTubeLayout.Name = "labelTubeLayout";
         this.labelTubeLayout.Size = new System.Drawing.Size(292, 20);
         this.labelTubeLayout.TabIndex = 34;
         this.labelTubeLayout.Text = "Tube Layout:";
         this.labelTubeLayout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBaffleCut
         // 
         this.labelBaffleCut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBaffleCut.Location = new System.Drawing.Point(0, 220);
         this.labelBaffleCut.Name = "labelBaffleCut";
         this.labelBaffleCut.Size = new System.Drawing.Size(292, 20);
         this.labelBaffleCut.TabIndex = 36;
         this.labelBaffleCut.Text = "BaffleCut";
         this.labelBaffleCut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBaffleSpacing
         // 
         this.labelBaffleSpacing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBaffleSpacing.Location = new System.Drawing.Point(0, 240);
         this.labelBaffleSpacing.Name = "labelBaffleSpacing";
         this.labelBaffleSpacing.Size = new System.Drawing.Size(292, 20);
         this.labelBaffleSpacing.TabIndex = 38;
         this.labelBaffleSpacing.Text = "BaffleSpacing";
         this.labelBaffleSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEntranceBaffleSpacing
         // 
         this.labelEntranceBaffleSpacing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEntranceBaffleSpacing.Location = new System.Drawing.Point(0, 260);
         this.labelEntranceBaffleSpacing.Name = "labelEntranceBaffleSpacing";
         this.labelEntranceBaffleSpacing.Size = new System.Drawing.Size(292, 20);
         this.labelEntranceBaffleSpacing.TabIndex = 40;
         this.labelEntranceBaffleSpacing.Text = "EntranceBaffleSpacing";
         this.labelEntranceBaffleSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBaffles
         // 
         this.labelBaffles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBaffles.Location = new System.Drawing.Point(0, 300);
         this.labelBaffles.Name = "labelBaffles";
         this.labelBaffles.Size = new System.Drawing.Size(292, 20);
         this.labelBaffles.TabIndex = 42;
         this.labelBaffles.Text = "Baffles";
         this.labelBaffles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelShellToBaffleDiametralClearance
         // 
         this.labelShellToBaffleDiametralClearance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellToBaffleDiametralClearance.Location = new System.Drawing.Point(0, 340);
         this.labelShellToBaffleDiametralClearance.Name = "labelShellToBaffleDiametralClearance";
         this.labelShellToBaffleDiametralClearance.Size = new System.Drawing.Size(292, 20);
         this.labelShellToBaffleDiametralClearance.TabIndex = 44;
         this.labelShellToBaffleDiametralClearance.Text = "ShellToBaffleDiametralClearance";
         this.labelShellToBaffleDiametralClearance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelShellInnerDiameter
         // 
         this.labelShellInnerDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelShellInnerDiameter.Location = new System.Drawing.Point(0, 320);
         this.labelShellInnerDiameter.Name = "labelShellInnerDiameter";
         this.labelShellInnerDiameter.Size = new System.Drawing.Size(292, 20);
         this.labelShellInnerDiameter.TabIndex = 46;
         this.labelShellInnerDiameter.Text = "ShellInnerDiameter";
         this.labelShellInnerDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelBundleToShellDiametralClearance
         // 
         this.labelBundleToShellDiametralClearance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelBundleToShellDiametralClearance.Location = new System.Drawing.Point(0, 360);
         this.labelBundleToShellDiametralClearance.Name = "labelBundleToShellDiametralClearance";
         this.labelBundleToShellDiametralClearance.Size = new System.Drawing.Size(292, 20);
         this.labelBundleToShellDiametralClearance.TabIndex = 48;
         this.labelBundleToShellDiametralClearance.Text = "BundleToShellDiametralClearance";
         this.labelBundleToShellDiametralClearance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSealingStrips
         // 
         this.labelSealingStrips.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSealingStrips.Location = new System.Drawing.Point(0, 380);
         this.labelSealingStrips.Name = "labelSealingStrips";
         this.labelSealingStrips.Size = new System.Drawing.Size(292, 20);
         this.labelSealingStrips.TabIndex = 50;
         this.labelSealingStrips.Text = "SealingStrips";
         this.labelSealingStrips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelExitBaffleSpacing
         // 
         this.labelExitBaffleSpacing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelExitBaffleSpacing.Location = new System.Drawing.Point(0, 280);
         this.labelExitBaffleSpacing.Name = "labelExitBaffleSpacing";
         this.labelExitBaffleSpacing.Size = new System.Drawing.Size(292, 20);
         this.labelExitBaffleSpacing.TabIndex = 51;
         this.labelExitBaffleSpacing.Text = "ExitBaffleSpacing";
         this.labelExitBaffleSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalTubesInShell
         // 
         this.labelTotalTubesInShell.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalTubesInShell.Location = new System.Drawing.Point(0, 140);
         this.labelTotalTubesInShell.Name = "labelTotalTubesInShell";
         this.labelTotalTubesInShell.Size = new System.Drawing.Size(292, 20);
         this.labelTotalTubesInShell.TabIndex = 52;
         this.labelTotalTubesInShell.Text = "TotalTubesInShell";
         this.labelTotalTubesInShell.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HXRatingShellAndTubeBellDelawareLabelsControl
         // 
         this.Controls.Add(this.labelTotalTubesInShell);
         this.Controls.Add(this.labelExitBaffleSpacing);
         this.Controls.Add(this.labelSealingStrips);
         this.Controls.Add(this.labelBundleToShellDiametralClearance);
         this.Controls.Add(this.labelShellInnerDiameter);
         this.Controls.Add(this.labelShellToBaffleDiametralClearance);
         this.Controls.Add(this.labelBaffles);
         this.Controls.Add(this.labelEntranceBaffleSpacing);
         this.Controls.Add(this.labelBaffleSpacing);
         this.Controls.Add(this.labelBaffleCut);
         this.Controls.Add(this.labelTubeLayout);
         this.Controls.Add(this.labelShellType);
         this.Controls.Add(this.labelShellPasses);
         this.Controls.Add(this.labelTubePassesPerShellPass);
         this.Controls.Add(this.labelTubeLengthBetweenTubeSheets);
         this.Controls.Add(this.labelTubeWallThickness);
         this.Controls.Add(this.labelTubeOuterDiameter);
         this.Controls.Add(this.labelTubePitch);
         this.Controls.Add(this.labelTubesPerTubePass);
         this.Controls.Add(this.labelTubeInnerDiameter);
         this.Name = "HXRatingShellAndTubeBellDelawareLabelsControl";
         this.Size = new System.Drawing.Size(292, 400);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HXRatingModelShellAndTube shellAndTube)
      {
         this.labelShellPasses.InitializeVariable(shellAndTube.ShellPasses);
         this.labelTubePassesPerShellPass.InitializeVariable(shellAndTube.TubePassesPerShellPass);
         this.labelTubeLengthBetweenTubeSheets.InitializeVariable(shellAndTube.TubeLengthBetweenTubeSheets);
         this.labelTubeWallThickness.InitializeVariable(shellAndTube.TubeWallThickness);
         this.labelTubeOuterDiameter.InitializeVariable(shellAndTube.TubeOuterDiameter);
         this.labelTubesPerTubePass.InitializeVariable(shellAndTube.TubesPerTubePass);
         this.labelTubeInnerDiameter.InitializeVariable(shellAndTube.TubeInnerDiameter);
         this.labelTubePitch.InitializeVariable(shellAndTube.TubePitch);
         this.labelBaffleCut.InitializeVariable(shellAndTube.BaffleCut);
         this.labelBaffleSpacing.InitializeVariable(shellAndTube.BaffleSpacing);
         this.labelEntranceBaffleSpacing.InitializeVariable(shellAndTube.EntranceBaffleSpacing);
         this.labelExitBaffleSpacing.InitializeVariable(shellAndTube.ExitBaffleSpacing);
         this.labelBaffles.InitializeVariable(shellAndTube.NumberOfBaffles);
         this.labelShellToBaffleDiametralClearance.InitializeVariable(shellAndTube.ShellToBaffleDiametralClearance);
         this.labelShellInnerDiameter.InitializeVariable(shellAndTube.ShellInnerDiameter);
         this.labelBundleToShellDiametralClearance.InitializeVariable(shellAndTube.BundleToShellDiametralClearance);
         this.labelSealingStrips.InitializeVariable(shellAndTube.SealingStrips);
         this.labelTotalTubesInShell.InitializeVariable(shellAndTube.TotalTubesInShell);
      }
	}
}
