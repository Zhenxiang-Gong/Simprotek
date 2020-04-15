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
	/// Summary description for HXRatingShellAndTubeBellDelawareValuesControl.
	/// </summary>
	public class HXRatingShellAndTubeBellDelawareValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 400;

      // TubeLayout
      public const int INDEX_TRIANGULAR = 0;
      public const int INDEX_INLINE_SQUARE = 1;
      public const int INDEX_ROTATED_SQUARE = 2;
      
      // ShellType
      public const int INDEX_E = 0;
      public const int INDEX_F = 1;
      public const int INDEX_G = 2;
      public const int INDEX_H = 3;
      public const int INDEX_J = 4;
      public const int INDEX_K = 5;
      public const int INDEX_X = 6;

      private bool inConstruction;
      private HeatExchanger heatExchanger;

      private ProsimoUI.ProcessVarTextBox textBoxSealingStrips;
      private ProsimoUI.ProcessVarTextBox textBoxBundleToShellDiametralClearance;
      private ProsimoUI.ProcessVarTextBox textBoxShellInnerDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxShellToBaffleDiametralClearance;
      private ProsimoUI.ProcessVarTextBox textBoxBaffles;
      private ProsimoUI.ProcessVarTextBox textBoxBaffleSpacing;
      private ProsimoUI.ProcessVarTextBox textBoxBaffleCut;
      private System.Windows.Forms.ComboBox comboBoxTubeLayout;
      private System.Windows.Forms.ComboBox comboBoxShellType;
      private ProsimoUI.ProcessVarTextBox textBoxShellPasses;
      private ProsimoUI.ProcessVarTextBox textBoxTubePassesPerShellPass;
      private ProsimoUI.ProcessVarTextBox textBoxTubeLengthBetweenTubeSheets;
      private ProsimoUI.ProcessVarTextBox textBoxTubeWallThickness;
      private ProsimoUI.ProcessVarTextBox textBoxTubeOuterDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxTubePitch;
      private ProsimoUI.ProcessVarTextBox textBoxTubesPerTubePass;
      private ProsimoUI.ProcessVarTextBox textBoxTubeInnerDiameter;
      private ProsimoUI.ProcessVarTextBox textBoxEntranceBaffleSpacing;
      private ProsimoUI.ProcessVarTextBox textBoxExitBaffleSpacing;
      private ProsimoUI.ProcessVarTextBox textBoxTotalTubesInShell;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HXRatingShellAndTubeBellDelawareValuesControl()
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
         this.textBoxSealingStrips = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBundleToShellDiametralClearance = new ProsimoUI.ProcessVarTextBox();
         this.textBoxShellInnerDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxShellToBaffleDiametralClearance = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBaffles = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEntranceBaffleSpacing = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBaffleSpacing = new ProsimoUI.ProcessVarTextBox();
         this.textBoxBaffleCut = new ProsimoUI.ProcessVarTextBox();
         this.comboBoxTubeLayout = new System.Windows.Forms.ComboBox();
         this.comboBoxShellType = new System.Windows.Forms.ComboBox();
         this.textBoxShellPasses = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubePassesPerShellPass = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeLengthBetweenTubeSheets = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeWallThickness = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeOuterDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubePitch = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubesPerTubePass = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTubeInnerDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxExitBaffleSpacing = new ProsimoUI.ProcessVarTextBox();
         this.textBoxTotalTubesInShell = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxSealingStrips
         // 
         this.textBoxSealingStrips.Location = new System.Drawing.Point(0, 380);
         this.textBoxSealingStrips.Name = "textBoxSealingStrips";
         this.textBoxSealingStrips.Size = new System.Drawing.Size(80, 20);
         this.textBoxSealingStrips.TabIndex = 18;
         this.textBoxSealingStrips.Text = "";
         this.textBoxSealingStrips.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxSealingStrips.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBundleToShellDiametralClearance
         // 
         this.textBoxBundleToShellDiametralClearance.Location = new System.Drawing.Point(0, 360);
         this.textBoxBundleToShellDiametralClearance.Name = "textBoxBundleToShellDiametralClearance";
         this.textBoxBundleToShellDiametralClearance.Size = new System.Drawing.Size(80, 20);
         this.textBoxBundleToShellDiametralClearance.TabIndex = 17;
         this.textBoxBundleToShellDiametralClearance.Text = "";
         this.textBoxBundleToShellDiametralClearance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBundleToShellDiametralClearance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxShellInnerDiameter
         // 
         this.textBoxShellInnerDiameter.Location = new System.Drawing.Point(0, 320);
         this.textBoxShellInnerDiameter.Name = "textBoxShellInnerDiameter";
         this.textBoxShellInnerDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxShellInnerDiameter.TabIndex = 15;
         this.textBoxShellInnerDiameter.Text = "";
         this.textBoxShellInnerDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxShellInnerDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxShellToBaffleDiametralClearance
         // 
         this.textBoxShellToBaffleDiametralClearance.Location = new System.Drawing.Point(0, 340);
         this.textBoxShellToBaffleDiametralClearance.Name = "textBoxShellToBaffleDiametralClearance";
         this.textBoxShellToBaffleDiametralClearance.Size = new System.Drawing.Size(80, 20);
         this.textBoxShellToBaffleDiametralClearance.TabIndex = 16;
         this.textBoxShellToBaffleDiametralClearance.Text = "";
         this.textBoxShellToBaffleDiametralClearance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxShellToBaffleDiametralClearance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBaffles
         // 
         this.textBoxBaffles.Location = new System.Drawing.Point(0, 300);
         this.textBoxBaffles.Name = "textBoxBaffles";
         this.textBoxBaffles.Size = new System.Drawing.Size(80, 20);
         this.textBoxBaffles.TabIndex = 14;
         this.textBoxBaffles.Text = "";
         this.textBoxBaffles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBaffles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxEntranceBaffleSpacing
         // 
         this.textBoxEntranceBaffleSpacing.Location = new System.Drawing.Point(0, 260);
         this.textBoxEntranceBaffleSpacing.Name = "textBoxEntranceBaffleSpacing";
         this.textBoxEntranceBaffleSpacing.Size = new System.Drawing.Size(80, 20);
         this.textBoxEntranceBaffleSpacing.TabIndex = 13;
         this.textBoxEntranceBaffleSpacing.Text = "";
         this.textBoxEntranceBaffleSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEntranceBaffleSpacing.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBaffleSpacing
         // 
         this.textBoxBaffleSpacing.Location = new System.Drawing.Point(0, 240);
         this.textBoxBaffleSpacing.Name = "textBoxBaffleSpacing";
         this.textBoxBaffleSpacing.Size = new System.Drawing.Size(80, 20);
         this.textBoxBaffleSpacing.TabIndex = 12;
         this.textBoxBaffleSpacing.Text = "";
         this.textBoxBaffleSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBaffleSpacing.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxBaffleCut
         // 
         this.textBoxBaffleCut.Location = new System.Drawing.Point(0, 220);
         this.textBoxBaffleCut.Name = "textBoxBaffleCut";
         this.textBoxBaffleCut.Size = new System.Drawing.Size(80, 20);
         this.textBoxBaffleCut.TabIndex = 11;
         this.textBoxBaffleCut.Text = "";
         this.textBoxBaffleCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxBaffleCut.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxTubeLayout
         // 
         this.comboBoxTubeLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxTubeLayout.Items.AddRange(new object[] {
                                                                "Triangular",
                                                                "Inline Square",
                                                                "Rotated Square"});
         this.comboBoxTubeLayout.Location = new System.Drawing.Point(0, 180);
         this.comboBoxTubeLayout.Name = "comboBoxTubeLayout";
         this.comboBoxTubeLayout.Size = new System.Drawing.Size(80, 21);
         this.comboBoxTubeLayout.TabIndex = 9;
         this.comboBoxTubeLayout.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         this.comboBoxTubeLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxTubeLayout_SelectedIndexChanged);
         // 
         // comboBoxShellType
         // 
         this.comboBoxShellType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxShellType.Items.AddRange(new object[] {
                                                               "E",
                                                               "F",
                                                               "G",
                                                               "H",
                                                               "J",
                                                               "K",
                                                               "X"});
         this.comboBoxShellType.Location = new System.Drawing.Point(0, 160);
         this.comboBoxShellType.Name = "comboBoxShellType";
         this.comboBoxShellType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxShellType.TabIndex = 8;
         this.comboBoxShellType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         this.comboBoxShellType.SelectedIndexChanged += new System.EventHandler(this.comboBoxShellType_SelectedIndexChanged);
         // 
         // textBoxShellPasses
         // 
         this.textBoxShellPasses.Location = new System.Drawing.Point(0, 120);
         this.textBoxShellPasses.Name = "textBoxShellPasses";
         this.textBoxShellPasses.Size = new System.Drawing.Size(80, 20);
         this.textBoxShellPasses.TabIndex = 7;
         this.textBoxShellPasses.Text = "";
         this.textBoxShellPasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxShellPasses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubePassesPerShellPass
         // 
         this.textBoxTubePassesPerShellPass.Location = new System.Drawing.Point(0, 100);
         this.textBoxTubePassesPerShellPass.Name = "textBoxTubePassesPerShellPass";
         this.textBoxTubePassesPerShellPass.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubePassesPerShellPass.TabIndex = 6;
         this.textBoxTubePassesPerShellPass.Text = "";
         this.textBoxTubePassesPerShellPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubePassesPerShellPass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeLengthBetweenTubeSheets
         // 
         this.textBoxTubeLengthBetweenTubeSheets.Location = new System.Drawing.Point(0, 60);
         this.textBoxTubeLengthBetweenTubeSheets.Name = "textBoxTubeLengthBetweenTubeSheets";
         this.textBoxTubeLengthBetweenTubeSheets.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeLengthBetweenTubeSheets.TabIndex = 4;
         this.textBoxTubeLengthBetweenTubeSheets.Text = "";
         this.textBoxTubeLengthBetweenTubeSheets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeLengthBetweenTubeSheets.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeWallThickness
         // 
         this.textBoxTubeWallThickness.Location = new System.Drawing.Point(0, 20);
         this.textBoxTubeWallThickness.Name = "textBoxTubeWallThickness";
         this.textBoxTubeWallThickness.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeWallThickness.TabIndex = 2;
         this.textBoxTubeWallThickness.Text = "";
         this.textBoxTubeWallThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeWallThickness.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeOuterDiameter
         // 
         this.textBoxTubeOuterDiameter.Location = new System.Drawing.Point(0, 0);
         this.textBoxTubeOuterDiameter.Name = "textBoxTubeOuterDiameter";
         this.textBoxTubeOuterDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeOuterDiameter.TabIndex = 1;
         this.textBoxTubeOuterDiameter.Text = "";
         this.textBoxTubeOuterDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeOuterDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubePitch
         // 
         this.textBoxTubePitch.Location = new System.Drawing.Point(0, 200);
         this.textBoxTubePitch.Name = "textBoxTubePitch";
         this.textBoxTubePitch.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubePitch.TabIndex = 10;
         this.textBoxTubePitch.Text = "";
         this.textBoxTubePitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubePitch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubesPerTubePass
         // 
         this.textBoxTubesPerTubePass.Location = new System.Drawing.Point(0, 80);
         this.textBoxTubesPerTubePass.Name = "textBoxTubesPerTubePass";
         this.textBoxTubesPerTubePass.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubesPerTubePass.TabIndex = 5;
         this.textBoxTubesPerTubePass.Text = "";
         this.textBoxTubesPerTubePass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubesPerTubePass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTubeInnerDiameter
         // 
         this.textBoxTubeInnerDiameter.Location = new System.Drawing.Point(0, 40);
         this.textBoxTubeInnerDiameter.Name = "textBoxTubeInnerDiameter";
         this.textBoxTubeInnerDiameter.Size = new System.Drawing.Size(80, 20);
         this.textBoxTubeInnerDiameter.TabIndex = 3;
         this.textBoxTubeInnerDiameter.Text = "";
         this.textBoxTubeInnerDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTubeInnerDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxExitBaffleSpacing
         // 
         this.textBoxExitBaffleSpacing.Location = new System.Drawing.Point(0, 280);
         this.textBoxExitBaffleSpacing.Name = "textBoxExitBaffleSpacing";
         this.textBoxExitBaffleSpacing.Size = new System.Drawing.Size(80, 20);
         this.textBoxExitBaffleSpacing.TabIndex = 19;
         this.textBoxExitBaffleSpacing.Text = "";
         this.textBoxExitBaffleSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxExitBaffleSpacing.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxTotalTubesInShell
         // 
         this.textBoxTotalTubesInShell.Location = new System.Drawing.Point(0, 140);
         this.textBoxTotalTubesInShell.Name = "textBoxTotalTubesInShell";
         this.textBoxTotalTubesInShell.Size = new System.Drawing.Size(80, 20);
         this.textBoxTotalTubesInShell.TabIndex = 20;
         this.textBoxTotalTubesInShell.Text = "";
         this.textBoxTotalTubesInShell.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxTotalTubesInShell.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // HXRatingShellAndTubeBellDelawareValuesControl
         // 
         this.Controls.Add(this.textBoxTotalTubesInShell);
         this.Controls.Add(this.textBoxExitBaffleSpacing);
         this.Controls.Add(this.textBoxSealingStrips);
         this.Controls.Add(this.textBoxBundleToShellDiametralClearance);
         this.Controls.Add(this.textBoxShellInnerDiameter);
         this.Controls.Add(this.textBoxShellToBaffleDiametralClearance);
         this.Controls.Add(this.textBoxBaffles);
         this.Controls.Add(this.textBoxEntranceBaffleSpacing);
         this.Controls.Add(this.textBoxBaffleSpacing);
         this.Controls.Add(this.textBoxBaffleCut);
         this.Controls.Add(this.comboBoxTubeLayout);
         this.Controls.Add(this.comboBoxShellType);
         this.Controls.Add(this.textBoxShellPasses);
         this.Controls.Add(this.textBoxTubePassesPerShellPass);
         this.Controls.Add(this.textBoxTubeLengthBetweenTubeSheets);
         this.Controls.Add(this.textBoxTubeWallThickness);
         this.Controls.Add(this.textBoxTubeOuterDiameter);
         this.Controls.Add(this.textBoxTubePitch);
         this.Controls.Add(this.textBoxTubesPerTubePass);
         this.Controls.Add(this.textBoxTubeInnerDiameter);
         this.Name = "HXRatingShellAndTubeBellDelawareValuesControl";
         this.Size = new System.Drawing.Size(80, 400);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(Flowsheet flowsheet, HXRatingModelShellAndTube rating)
      {
         this.textBoxSealingStrips.InitializeVariable(flowsheet.ApplicationPrefs, rating.SealingStrips);
         this.textBoxBundleToShellDiametralClearance.InitializeVariable(flowsheet.ApplicationPrefs, rating.BundleToShellDiametralClearance);
         this.textBoxShellInnerDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.ShellInnerDiameter);
         this.textBoxShellToBaffleDiametralClearance.InitializeVariable(flowsheet.ApplicationPrefs, rating.ShellToBaffleDiametralClearance);
         this.textBoxBaffles.InitializeVariable(flowsheet.ApplicationPrefs, rating.NumberOfBaffles);
         this.textBoxEntranceBaffleSpacing.InitializeVariable(flowsheet.ApplicationPrefs, rating.EntranceBaffleSpacing);
         this.textBoxExitBaffleSpacing.InitializeVariable(flowsheet.ApplicationPrefs, rating.ExitBaffleSpacing);
         this.textBoxBaffleSpacing.InitializeVariable(flowsheet.ApplicationPrefs, rating.BaffleSpacing);
         this.textBoxBaffleCut.InitializeVariable(flowsheet.ApplicationPrefs, rating.BaffleCut);
         this.textBoxShellPasses.InitializeVariable(flowsheet.ApplicationPrefs, rating.ShellPasses);
         this.textBoxTubePassesPerShellPass.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubePassesPerShellPass);
         this.textBoxTubeLengthBetweenTubeSheets.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubeLengthBetweenTubeSheets);
         this.textBoxTubeWallThickness.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubeWallThickness);
         this.textBoxTubeOuterDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubeOuterDiameter);
         this.textBoxTubePitch.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubePitch);
         this.textBoxTubesPerTubePass.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubesPerTubePass);
         this.textBoxTubeInnerDiameter.InitializeVariable(flowsheet.ApplicationPrefs, rating.TubeInnerDiameter);
         this.textBoxTotalTubesInShell.InitializeVariable(flowsheet.ApplicationPrefs, rating.TotalTubesInShell);
      }

      public void InitializeTheUI(Flowsheet flowsheet, HeatExchanger heatExchanger)
      {
         this.inConstruction = true;
         this.heatExchanger = heatExchanger;
         HXRatingModelShellAndTube shellAndTubeRating = heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
         this.InitializeVariableTextBoxes(flowsheet, shellAndTubeRating);
         heatExchanger.SolveComplete += new SolveCompleteEventHandler(heatExchanger_SolveComplete);
         this.comboBoxShellType.SelectedIndex = -1;
         this.comboBoxTubeLayout.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetShellType(shellAndTubeRating.ShellType);
         this.SetTubeLayout(shellAndTubeRating.TubeLayout);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxTubeOuterDiameter);
         list.Add(this.textBoxTubeWallThickness);
         list.Add(this.textBoxTubeInnerDiameter);
         list.Add(this.textBoxTubeLengthBetweenTubeSheets);
         list.Add(this.textBoxTubesPerTubePass);
         list.Add(this.textBoxTubePassesPerShellPass);
         list.Add(this.textBoxShellPasses);
         list.Add(this.textBoxTotalTubesInShell);
         list.Add(this.comboBoxShellType);
         list.Add(this.comboBoxTubeLayout);
         list.Add(this.textBoxTubePitch);
         list.Add(this.textBoxBaffleCut);
         list.Add(this.textBoxBaffleSpacing);
         list.Add(this.textBoxEntranceBaffleSpacing);
         list.Add(this.textBoxExitBaffleSpacing);
         list.Add(this.textBoxBaffles);
         list.Add(this.textBoxShellInnerDiameter);
         list.Add(this.textBoxShellToBaffleDiametralClearance);
         list.Add(this.textBoxBundleToShellDiametralClearance);
         list.Add(this.textBoxSealingStrips);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      public void SetTubeLayout(TubeLayout tubeLayout)
      {
         if (tubeLayout == TubeLayout.InlineSquare)
            this.comboBoxTubeLayout.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_INLINE_SQUARE;
         else if (tubeLayout == TubeLayout.RotatedSquare)
            this.comboBoxTubeLayout.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_ROTATED_SQUARE;
         else if (tubeLayout == TubeLayout.Triangular)
            this.comboBoxTubeLayout.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_TRIANGULAR;
      }

      public void SetShellType(ShellType shellType)
      {
         if (shellType == ShellType.E)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_E;
         else if (shellType == ShellType.F)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_F;
         else if (shellType == ShellType.G)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_G;
         else if (shellType == ShellType.H)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_H;
         else if (shellType == ShellType.J)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_J;
         else if (shellType == ShellType.K)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_K;
         else if (shellType == ShellType.X)
            this.comboBoxShellType.SelectedIndex = HXRatingShellAndTubeBellDelawareValuesControl.INDEX_X;
      }

      private void comboBoxShellType_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            HXRatingModelShellAndTube shellAndTubeRating = this.heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
            int idx = this.comboBoxShellType.SelectedIndex;
            if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_E)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.E);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_F)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.F);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_G)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.G);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_H)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.H);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_J)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.J);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_K)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.K);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_X)
            {
               error = shellAndTubeRating.SpecifyShellType(ShellType.X);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetShellType(shellAndTubeRating.ShellType);
            }
         }
      }

      private void comboBoxTubeLayout_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            HXRatingModelShellAndTube shellAndTubeRating = this.heatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;
            int idx = this.comboBoxTubeLayout.SelectedIndex;
            if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_INLINE_SQUARE)
            {
               error = shellAndTubeRating.SpecifyTubeLayout(TubeLayout.InlineSquare);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_ROTATED_SQUARE)
            {
               error = shellAndTubeRating.SpecifyTubeLayout(TubeLayout.RotatedSquare);
            }
            else if (idx == HXRatingShellAndTubeBellDelawareValuesControl.INDEX_TRIANGULAR)
            {
               error = shellAndTubeRating.SpecifyTubeLayout(TubeLayout.Triangular);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetTubeLayout(shellAndTubeRating.TubeLayout);
            }
         }
      }

      private void heatExchanger_SolveComplete(object sender, SolveState solveState)
      {
         HXRatingModelShellAndTube rating = (sender as HeatExchanger).CurrentRatingModel as HXRatingModelShellAndTube;
         this.SetTubeLayout(rating.TubeLayout);
         this.SetShellType(rating.ShellType);
      }
   }
}
