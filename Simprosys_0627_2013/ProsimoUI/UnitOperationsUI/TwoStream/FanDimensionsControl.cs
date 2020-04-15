using System;
using System.Collections;
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
	public class FanDimensionsControl : UserControl
	{
      private bool inConstruction;
      private Fan fan;

      private ProcessVarLabel labelOutletVelocity;
      private ProcessVarTextBox textBoxOutletVelocity;
      private GroupBox groupBoxCrossSectionType;
      private RadioButton radioButtonCrossSectionTypeRectangle;
      private RadioButton radioButtonCrossSectionTypeCircle;
      private FanCircularLabelsControl fanCircularLabelsControl;
      private FanRectangularLabelsControl fanRectangularLabelsControl;
      private FanCircularValuesControl fanCircularValuesControl;
      private FanRectangularValuesControl fanRectangularValuesControl;
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

         this.fanCircularLabelsControl.InitializeVariableLabels(fanCtrl.Fan);
         this.fanCircularValuesControl.InitializeVariableTextBoxes(fanCtrl);
         this.fanRectangularLabelsControl.InitializeVariableLabels(fanCtrl.Fan);
         this.fanRectangularValuesControl.InitializeVariableTextBoxes(fanCtrl);

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
         this.labelOutletVelocity = new ProsimoUI.ProcessVarLabel();
         this.textBoxOutletVelocity = new ProsimoUI.ProcessVarTextBox();
         this.groupBoxCrossSectionType = new System.Windows.Forms.GroupBox();
         this.radioButtonCrossSectionTypeRectangle = new System.Windows.Forms.RadioButton();
         this.radioButtonCrossSectionTypeCircle = new System.Windows.Forms.RadioButton();
         this.fanCircularLabelsControl = new ProsimoUI.UnitOperationsUI.TwoStream.FanCircularLabelsControl();
         this.fanRectangularLabelsControl = new ProsimoUI.UnitOperationsUI.TwoStream.FanRectangularLabelsControl();
         this.fanCircularValuesControl = new ProsimoUI.UnitOperationsUI.TwoStream.FanCircularValuesControl();
         this.fanRectangularValuesControl = new ProsimoUI.UnitOperationsUI.TwoStream.FanRectangularValuesControl();
         this.groupBoxCrossSectionType.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelOutletVelocity
         // 
         this.labelOutletVelocity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletVelocity.Location = new System.Drawing.Point(0, 104);
         this.labelOutletVelocity.Name = "labelOutletVelocity";
         this.labelOutletVelocity.Size = new System.Drawing.Size(192, 20);
         this.labelOutletVelocity.TabIndex = 101;
         this.labelOutletVelocity.Text = "OutletVelocity";
         this.labelOutletVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxOutletVelocity
         // 
         this.textBoxOutletVelocity.Location = new System.Drawing.Point(192, 104);
         this.textBoxOutletVelocity.Name = "textBoxOutletVelocity";
         this.textBoxOutletVelocity.Size = new System.Drawing.Size(80, 20);
         this.textBoxOutletVelocity.TabIndex = 102;
         this.textBoxOutletVelocity.Text = "";
         this.textBoxOutletVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxOutletVelocity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // groupBoxCrossSectionType
         // 
         this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeRectangle);
         this.groupBoxCrossSectionType.Controls.Add(this.radioButtonCrossSectionTypeCircle);
         this.groupBoxCrossSectionType.Location = new System.Drawing.Point(0, 0);
         this.groupBoxCrossSectionType.Name = "groupBoxCrossSectionType";
         this.groupBoxCrossSectionType.Size = new System.Drawing.Size(194, 40);
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
         // fanCircularLabelsControl
         // 
         this.fanCircularLabelsControl.Location = new System.Drawing.Point(0, 44);
         this.fanCircularLabelsControl.Name = "fanCircularLabelsControl";
         this.fanCircularLabelsControl.Size = new System.Drawing.Size(192, 20);
         this.fanCircularLabelsControl.TabIndex = 104;
         // 
         // fanRectangularLabelsControl
         // 
         this.fanRectangularLabelsControl.Location = new System.Drawing.Point(0, 44);
         this.fanRectangularLabelsControl.Name = "fanRectangularLabelsControl";
         this.fanRectangularLabelsControl.Size = new System.Drawing.Size(192, 60);
         this.fanRectangularLabelsControl.TabIndex = 105;
         // 
         // fanCircularValuesControl
         // 
         this.fanCircularValuesControl.Location = new System.Drawing.Point(192, 44);
         this.fanCircularValuesControl.Name = "fanCircularValuesControl";
         this.fanCircularValuesControl.Size = new System.Drawing.Size(80, 20);
         this.fanCircularValuesControl.TabIndex = 106;
         // 
         // fanRectangularValuesControl
         // 
         this.fanRectangularValuesControl.Location = new System.Drawing.Point(192, 44);
         this.fanRectangularValuesControl.Name = "fanRectangularValuesControl";
         this.fanRectangularValuesControl.Size = new System.Drawing.Size(80, 60);
         this.fanRectangularValuesControl.TabIndex = 107;
         // 
         // FanDimensionsControl
         // 
         this.Controls.Add(this.fanRectangularValuesControl);
         this.Controls.Add(this.fanCircularValuesControl);
         this.Controls.Add(this.fanRectangularLabelsControl);
         this.Controls.Add(this.fanCircularLabelsControl);
         this.Controls.Add(this.groupBoxCrossSectionType);
         this.Controls.Add(this.textBoxOutletVelocity);
         this.Controls.Add(this.labelOutletVelocity);
         this.Name = "FanDimensionsControl";
         this.Size = new System.Drawing.Size(272, 124);
         this.groupBoxCrossSectionType.ResumeLayout(false);
         this.ResumeLayout(false);

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
         this.fanCircularLabelsControl.Visible = true;
         this.fanCircularValuesControl.Visible = true;
         this.fanRectangularLabelsControl.Visible = false;
         this.fanRectangularValuesControl.Visible = false;
         this.labelOutletVelocity.Location = new Point(0,64);
         this.textBoxOutletVelocity.Location = new Point(192,64);
      }

      private void SetRectangularSectionUI()
      {
         this.fanCircularLabelsControl.Visible = false;
         this.fanCircularValuesControl.Visible = false;
         this.fanRectangularLabelsControl.Visible = true;
         this.fanRectangularValuesControl.Visible = true;
         this.labelOutletVelocity.Location = new Point(0,104);
         this.textBoxOutletVelocity.Location = new Point(192,104);
      }

      private void fan_SolveComplete(object sender, SolveState solveState)
      {
         this.inConstruction = true;
         this.SetCrossSectionType(this.fan.OutletCrossSectionType);
         this.inConstruction = false;
      }

      public void InitializeVariableLabels(Fan uo)
      {
         this.labelOutletVelocity.InitializeVariable(uo.OutletVelocity);
      }

      public void InitializeVariableTextBoxes(FanControl ctrl)
      {
         this.textBoxOutletVelocity.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Fan.OutletVelocity);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxOutletVelocity;
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
