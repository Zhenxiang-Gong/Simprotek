using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanDimensionsControl.
	/// </summary>
	public class FanDimensionsControl : System.Windows.Forms.UserControl
	{
      private bool inConstruction;
        private Fan fan;
      private System.Windows.Forms.GroupBox groupBoxCrossSectionType;
      private System.Windows.Forms.RadioButton radioButtonCrossSectionTypeRectangle;
        private System.Windows.Forms.RadioButton radioButtonCrossSectionTypeCircle;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FanDimensionsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public FanDimensionsControl(FanControl fanCtrl)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.inConstruction = true;
         this.fan = fanCtrl.Fan;

         this.SetCrossSectionType(fan.OutletCrossSectionType);

         //this.fanCircularLabelsControl.InitializeVariableLabels(fanCtrl.Fan);
         //this.fanCircularValuesControl.InitializeVariableTextBoxes(fanCtrl);
         //this.fanRectangularLabelsControl.InitializeVariableLabels(fanCtrl.Fan);
         //this.fanRectangularValuesControl.InitializeVariableTextBoxes(fanCtrl);

         this.InitializeVariableLabels(fanCtrl.Fan);
         this.InitializeVariableTextBoxes(fanCtrl);

         this.fan.SolveComplete += new SolveCompleteEventHandler(fan_SolveComplete);
         this.inConstruction = false;
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.fan != null)
            this.fan.SolveComplete -= new SolveCompleteEventHandler(fan_SolveComplete);
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
            this.groupBoxCrossSectionType = new System.Windows.Forms.GroupBox();
            this.radioButtonCrossSectionTypeRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonCrossSectionTypeCircle = new System.Windows.Forms.RadioButton();
            this.groupBoxCrossSectionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCrossSectionType
            // 
            this.groupBoxCrossSectionType.AutoSize = true;
            this.groupBoxCrossSectionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeRectangle);
            this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeCircle);
            this.groupBoxCrossSectionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCrossSectionType.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCrossSectionType.Name = "groupBoxCrossSectionType";
            this.groupBoxCrossSectionType.Size = new System.Drawing.Size(200, 48);
            this.groupBoxCrossSectionType.TabIndex = 103;
            this.groupBoxCrossSectionType.TabStop = false;
            this.groupBoxCrossSectionType.Text = "Outlet Cross Section Type";
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
            // FanDimensionsControl
            // 
            this.Controls.Add(this.groupBoxCrossSectionType);
            this.Name = "FanDimensionsControl";
            this.Size = new System.Drawing.Size(200, 48);
            this.groupBoxCrossSectionType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

      }
		#endregion

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
         //this.fanCircularLabelsControl.Visible = true;
         //this.fanCircularValuesControl.Visible = true;
         //this.fanRectangularLabelsControl.Visible = false;
         //this.fanRectangularValuesControl.Visible = false;
         //this.labelOutletVelocity.Location = new Point(0,64);
         //this.textBoxOutletVelocity.Location = new Point(192,64);
      }

      private void SetRectangularSectionUI()
      {
         //this.fanCircularLabelsControl.Visible = false;
         //this.fanCircularValuesControl.Visible = false;
         //this.fanRectangularLabelsControl.Visible = true;
         //this.fanRectangularValuesControl.Visible = true;
         //this.labelOutletVelocity.Location = new Point(0,104);
         //this.textBoxOutletVelocity.Location = new Point(192,104);
      }

      private void fan_SolveComplete(object sender, SolveState solveState)
      {
         this.inConstruction = true;
         this.SetCrossSectionType(this.fan.OutletCrossSectionType);
         this.inConstruction = false;
      }

      public void InitializeVariableLabels(Fan uo)
      {
         //this.labelOutletVelocity.InitializeVariable(uo.OutletVelocity);
      }

      public void InitializeVariableTextBoxes(FanControl ctrl)
      {
         //this.textBoxOutletVelocity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletVelocity);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            //this.ActiveControl = this.textBoxOutletVelocity;
         }
      }

      private void radioButtonCrossSectionType_CheckedChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;

            if (this.radioButtonCrossSectionTypeCircle.Checked)
               error = this.fan.SpecifyOutletCrossSectionType(CrossSectionType.Circle);
            else if (this.radioButtonCrossSectionTypeRectangle.Checked)
               error = this.fan.SpecifyOutletCrossSectionType(CrossSectionType.Rectangle);

            if (error != null)
            {
               UI.ShowError(error);
               this.inConstruction = true;
               this.SetCrossSectionType(this.fan.OutletCrossSectionType);
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
