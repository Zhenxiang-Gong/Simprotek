using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneRatingEditor.
	/// </summary>
	public class CycloneRatingEditor : System.Windows.Forms.Form
	{
      private CycloneControl cycloneCtrl;
      private MenuItem menuItemParticleDistribution;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxCommonRating;
      private ProsimoUI.UnitOperationsUI.CycloneUI.CycloneRatingValuesControl cycloneRatingValuesControl;
      private ProsimoUI.UnitOperationsUI.CycloneUI.CycloneRatingLabelsControl cycloneRatingLabelsControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneRatingEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public CycloneRatingEditor(CycloneControl cycloneCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.ShowInTaskbar = false;
         this.cycloneCtrl = cycloneCtrl;
         CycloneRatingModel rating = cycloneCtrl.Cyclone.CurrentRatingModel;

         this.cycloneRatingLabelsControl.InitializeVariableLabels(rating);
         this.cycloneRatingValuesControl.InitializeTheUI(this.cycloneCtrl.Flowsheet, cycloneCtrl.Cyclone);
         
         this.Text = "Cyclone Rating: " + this.cycloneCtrl.Cyclone.Name;

         this.menuItemParticleDistribution = new MenuItem();
         this.menuItemParticleDistribution.Index = 1;
         this.menuItemParticleDistribution.Text = "Particle Distributions";
         this.menuItemParticleDistribution.Click += new EventHandler(menuItemParticleDistribution_Click);
         this.mainMenu.MenuItems.Add(this.menuItemParticleDistribution);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxCommonRating = new System.Windows.Forms.GroupBox();
         this.cycloneRatingValuesControl = new ProsimoUI.UnitOperationsUI.CycloneUI.CycloneRatingValuesControl();
         this.cycloneRatingLabelsControl = new ProsimoUI.UnitOperationsUI.CycloneUI.CycloneRatingLabelsControl();
         this.panel.SuspendLayout();
         this.groupBoxCommonRating.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxCommonRating);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(398, 469);
         this.panel.TabIndex = 0;
         // 
         // groupBoxCommonRating
         // 
         this.groupBoxCommonRating.Controls.Add(this.cycloneRatingValuesControl);
         this.groupBoxCommonRating.Controls.Add(this.cycloneRatingLabelsControl);
         this.groupBoxCommonRating.Location = new System.Drawing.Point(4, 4);
         this.groupBoxCommonRating.Name = "groupBoxCommonRating";
         this.groupBoxCommonRating.Size = new System.Drawing.Size(388, 460);
         this.groupBoxCommonRating.TabIndex = 0;
         this.groupBoxCommonRating.TabStop = false;
         // 
         // cycloneRatingValuesControl
         // 
         this.cycloneRatingValuesControl.Location = new System.Drawing.Point(300, 12);
         this.cycloneRatingValuesControl.Name = "cycloneRatingValuesControl";
         this.cycloneRatingValuesControl.Size = new System.Drawing.Size(80, 440);
         this.cycloneRatingValuesControl.TabIndex = 3;
         // 
         // cycloneRatingLabelsControl
         // 
         this.cycloneRatingLabelsControl.Location = new System.Drawing.Point(8, 12);
         this.cycloneRatingLabelsControl.Name = "cycloneRatingLabelsControl";
         this.cycloneRatingLabelsControl.Size = new System.Drawing.Size(292, 440);
         this.cycloneRatingLabelsControl.TabIndex = 2;
         // 
         // CycloneRatingEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(398, 469);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Menu = this.mainMenu;
         this.Name = "CycloneRatingEditor";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Cyclone Rating";
         this.panel.ResumeLayout(false);
         this.groupBoxCommonRating.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void menuItemParticleDistribution_Click(object sender, EventArgs e)
      {
         CycloneParticleDistributionEditor pde = new CycloneParticleDistributionEditor(this.cycloneCtrl);
         pde.ShowDialog();
      }
	}
}
