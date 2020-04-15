using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerScopingEditor.
	/// </summary>
	public class DryerScopingEditor : System.Windows.Forms.Form
	{
      private bool inConstruction;
      private Dryer dryer;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxCommon;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCommonValuesControl dryerScopingCommonValuesControl;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCommonLabelsControl dryerScopingCommonLabelsControl;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCircularLabelsControl dryerScopingCircularLabelsControl;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCircularValuesControl dryerScopingCircularValuesControl;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingRectangularValuesControl dryerScopingRectangularValuesControl;
      private ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingRectangularLabelsControl dryerScopingRectangularLabelsControl;
      private System.Windows.Forms.GroupBox groupBoxCrossSectionType;
      private System.Windows.Forms.RadioButton radioButtonCrossSectionTypeCircle;
      private System.Windows.Forms.RadioButton radioButtonCrossSectionTypeRectangle;
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryerScopingEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public DryerScopingEditor(DryerControl dryerCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;

         this.dryer = dryerCtrl.Dryer;
         this.Text = "Dryer Scoping: " + dryerCtrl.Dryer.Name;

         this.dryerScopingCommonLabelsControl.InitializeVariableLabels(this.dryer.ScopingModel);
         this.dryerScopingCommonValuesControl.InitializeVariableTextBoxes(dryerCtrl.Flowsheet, this.dryer.ScopingModel);
         this.SetCrossSectionType(this.dryer.ScopingModel.CrossSectionType);

         this.dryerScopingCircularLabelsControl.InitializeVariableLabels(this.dryer.ScopingModel);
         this.dryerScopingCircularValuesControl.InitializeVariableTextBoxes(dryerCtrl.Flowsheet, this.dryer.ScopingModel);
         this.dryerScopingRectangularLabelsControl.InitializeVariableLabels(this.dryer.ScopingModel);
         this.dryerScopingRectangularValuesControl.InitializeVariableTextBoxes(dryerCtrl.Flowsheet, this.dryer.ScopingModel);

         this.dryer.SolveComplete += new SolveCompleteEventHandler(dryer_SolveComplete);

         this.inConstruction = false;
      }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.dryer != null)
            this.dryer.SolveComplete -= new SolveCompleteEventHandler(dryer_SolveComplete);

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
         this.groupBoxCommon = new System.Windows.Forms.GroupBox();
         this.groupBoxCrossSectionType = new System.Windows.Forms.GroupBox();
         this.radioButtonCrossSectionTypeRectangle = new System.Windows.Forms.RadioButton();
         this.radioButtonCrossSectionTypeCircle = new System.Windows.Forms.RadioButton();
         this.dryerScopingRectangularLabelsControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingRectangularLabelsControl();
         this.dryerScopingRectangularValuesControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingRectangularValuesControl();
         this.dryerScopingCircularValuesControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCircularValuesControl();
         this.dryerScopingCircularLabelsControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCircularLabelsControl();
         this.dryerScopingCommonValuesControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCommonValuesControl();
         this.dryerScopingCommonLabelsControl = new ProsimoUI.UnitOperationsUI.DryerUI.DryerScopingCommonLabelsControl();
         this.panel.SuspendLayout();
         this.groupBoxCommon.SuspendLayout();
         this.groupBoxCrossSectionType.SuspendLayout();
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
         this.panel.Controls.Add(this.groupBoxCommon);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(398, 191);
         this.panel.TabIndex = 0;
         // 
         // groupBoxCommon
         // 
         this.groupBoxCommon.Controls.Add(this.groupBoxCrossSectionType);
         this.groupBoxCommon.Controls.Add(this.dryerScopingRectangularLabelsControl);
         this.groupBoxCommon.Controls.Add(this.dryerScopingRectangularValuesControl);
         this.groupBoxCommon.Controls.Add(this.dryerScopingCircularValuesControl);
         this.groupBoxCommon.Controls.Add(this.dryerScopingCircularLabelsControl);
         this.groupBoxCommon.Controls.Add(this.dryerScopingCommonValuesControl);
         this.groupBoxCommon.Controls.Add(this.dryerScopingCommonLabelsControl);
         this.groupBoxCommon.Location = new System.Drawing.Point(4, 4);
         this.groupBoxCommon.Name = "groupBoxCommon";
         this.groupBoxCommon.Size = new System.Drawing.Size(388, 180);
         this.groupBoxCommon.TabIndex = 0;
         this.groupBoxCommon.TabStop = false;
         // 
         // groupBoxCrossSectionType
         // 
         this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeRectangle);
         this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeCircle);
         this.groupBoxCrossSectionType.Location = new System.Drawing.Point(8, 12);
         this.groupBoxCrossSectionType.Name = "groupBoxCrossSectionType";
         this.groupBoxCrossSectionType.Size = new System.Drawing.Size(194, 40);
         this.groupBoxCrossSectionType.TabIndex = 11;
         this.groupBoxCrossSectionType.TabStop = false;
         this.groupBoxCrossSectionType.Text = "Cross Section Type";
         // 
         // radioButtonCrossSectionTypeRectangle
         // 
         this.radioButtonCrossSectionTypeRectangle.Location = new System.Drawing.Point(112, 20);
         this.radioButtonCrossSectionTypeRectangle.Name = "radioButtonCrossSectionTypeRectangle";
         this.radioButtonCrossSectionTypeRectangle.Size = new System.Drawing.Size(80, 16);
         this.radioButtonCrossSectionTypeRectangle.TabIndex = 1;
         this.radioButtonCrossSectionTypeRectangle.Text = "Rectangle";
         this.radioButtonCrossSectionTypeRectangle.CheckedChanged += new System.EventHandler(this.radioButtonCrossSectionType_CheckedChanged);
         // 
         // radioButtonCrossSectionTypeCircle
         // 
         this.radioButtonCrossSectionTypeCircle.Location = new System.Drawing.Point(28, 20);
         this.radioButtonCrossSectionTypeCircle.Name = "radioButtonCrossSectionTypeCircle";
         this.radioButtonCrossSectionTypeCircle.Size = new System.Drawing.Size(80, 16);
         this.radioButtonCrossSectionTypeCircle.TabIndex = 0;
         this.radioButtonCrossSectionTypeCircle.Text = "Circle";
         this.radioButtonCrossSectionTypeCircle.CheckedChanged += new System.EventHandler(this.radioButtonCrossSectionType_CheckedChanged);
         // 
         // dryerScopingRectangularLabelsControl
         // 
         this.dryerScopingRectangularLabelsControl.Location = new System.Drawing.Point(8, 76);
         this.dryerScopingRectangularLabelsControl.Name = "dryerScopingRectangularLabelsControl";
         this.dryerScopingRectangularLabelsControl.Size = new System.Drawing.Size(292, 100);
         this.dryerScopingRectangularLabelsControl.TabIndex = 10;
         // 
         // dryerScopingRectangularValuesControl
         // 
         this.dryerScopingRectangularValuesControl.Location = new System.Drawing.Point(300, 76);
         this.dryerScopingRectangularValuesControl.Name = "dryerScopingRectangularValuesControl";
         this.dryerScopingRectangularValuesControl.Size = new System.Drawing.Size(80, 100);
         this.dryerScopingRectangularValuesControl.TabIndex = 9;
         // 
         // dryerScopingCircularValuesControl
         // 
         this.dryerScopingCircularValuesControl.Location = new System.Drawing.Point(300, 76);
         this.dryerScopingCircularValuesControl.Name = "dryerScopingCircularValuesControl";
         this.dryerScopingCircularValuesControl.Size = new System.Drawing.Size(80, 60);
         this.dryerScopingCircularValuesControl.TabIndex = 8;
         // 
         // dryerScopingCircularLabelsControl
         // 
         this.dryerScopingCircularLabelsControl.Location = new System.Drawing.Point(8, 76);
         this.dryerScopingCircularLabelsControl.Name = "dryerScopingCircularLabelsControl";
         this.dryerScopingCircularLabelsControl.Size = new System.Drawing.Size(292, 60);
         this.dryerScopingCircularLabelsControl.TabIndex = 7;
         // 
         // dryerScopingCommonValuesControl
         // 
         this.dryerScopingCommonValuesControl.Location = new System.Drawing.Point(300, 56);
         this.dryerScopingCommonValuesControl.Name = "dryerScopingCommonValuesControl";
         this.dryerScopingCommonValuesControl.Size = new System.Drawing.Size(80, 20);
         this.dryerScopingCommonValuesControl.TabIndex = 0;
         // 
         // dryerScopingCommonLabelsControl
         // 
         this.dryerScopingCommonLabelsControl.Location = new System.Drawing.Point(8, 56);
         this.dryerScopingCommonLabelsControl.Name = "dryerScopingCommonLabelsControl";
         this.dryerScopingCommonLabelsControl.Size = new System.Drawing.Size(292, 20);
         this.dryerScopingCommonLabelsControl.TabIndex = 1;
         // 
         // DryerScopingEditor
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(398, 191);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Menu = this.mainMenu;
         this.Name = "DryerScopingEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Dryer Scoping";
         this.panel.ResumeLayout(false);
         this.groupBoxCommon.ResumeLayout(false);
         this.groupBoxCrossSectionType.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      public void SetCrossSectionType(CrossSectionType type)
      {
         if (type == CrossSectionType.Circle)
         {
            this.radioButtonCrossSectionTypeCircle.Checked = true;
            this.SetCircularSectionUI(); 
         }
         else if (type == CrossSectionType.Rectangle)
         {
            this.radioButtonCrossSectionTypeRectangle.Checked = true;
            this.SetRectangularSectionUI(); 
         }
      }

      private void SetCircularSectionUI()
      {
         this.dryerScopingCircularLabelsControl.Visible = true;
         this.dryerScopingCircularValuesControl.Visible = true;
         this.dryerScopingRectangularLabelsControl.Visible = false;
         this.dryerScopingRectangularValuesControl.Visible = false;
      }

      private void SetRectangularSectionUI()
      {
         this.dryerScopingCircularLabelsControl.Visible = false;
         this.dryerScopingCircularValuesControl.Visible = false;
         this.dryerScopingRectangularLabelsControl.Visible = true;
         this.dryerScopingRectangularValuesControl.Visible = true;
      }

      private void dryer_SolveComplete(object sender, SolveState solveState)
      {
         this.inConstruction = true;
         this.SetCrossSectionType(this.dryer.ScopingModel.CrossSectionType);
         this.inConstruction = false;
      }

      private void radioButtonCrossSectionType_CheckedChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;

            if (this.radioButtonCrossSectionTypeCircle.Checked)
               error = this.dryer.ScopingModel.SpecifyCrossSectionType(CrossSectionType.Circle);
            else if (this.radioButtonCrossSectionTypeRectangle.Checked)
               error = this.dryer.ScopingModel.SpecifyCrossSectionType(CrossSectionType.Rectangle);

            if (error != null)
            {
               UI.ShowError(error);
               this.inConstruction = true;
               this.SetCrossSectionType(this.dryer.ScopingModel.CrossSectionType);
               this.inConstruction = false;
            }
            else
            {
               if (this.radioButtonCrossSectionTypeCircle.Checked)
                  this.SetCircularSectionUI();
               else if (this.radioButtonCrossSectionTypeRectangle.Checked)
                 this.SetRectangularSectionUI();
            }
         }
      }
   }
}
