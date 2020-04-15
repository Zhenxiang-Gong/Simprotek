using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo;
using ProsimoUI.ProcVarsEditor;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanEditor.
	/// </summary>
	public class FanEditor : TwoStreamUnitOpEditor
	{
      public FanControl FanCtrl
      {
         get {return (FanControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private Fan fan;
      private FanDimensionsControl fanDimensionsCtrl;
      private System.Windows.Forms.CheckBox checkBoxIncludeDimensions;
      private bool inConstruction;

      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanEditor()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

		public FanEditor(FanControl fanCtrl) : base(fanCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.inConstruction = true;

         this.fan = this.FanCtrl.Fan;
         this.Text = "Fan: " + fan.Name;

         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 280);
         this.groupBoxTwoStreamUnitOp.Text = "Fan";

         //ProcessVarLabelsControl fanLabelsCtrl = new ProcessVarLabelsControl(fanCtrl.Fan.VarList);
         FanLabelsControl fanLabelsCtrl = new FanLabelsControl(fanCtrl.Fan);
         this.groupBoxTwoStreamUnitOp.Controls.Add(fanLabelsCtrl);
         fanLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //ProcessVarValuesControl fanValuesCtrl = new ProcessVarValuesControl(this.FanCtrl);
         FanValuesControl fanValuesCtrl = new FanValuesControl(this.FanCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(fanValuesCtrl);
         fanValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         //
         this.checkBoxIncludeDimensions = new CheckBox();
         this.checkBoxIncludeDimensions.Location = new System.Drawing.Point(4, 118);
         this.checkBoxIncludeDimensions.Name = "checkBoxIncludeDimensions";
         this.checkBoxIncludeDimensions.Text = "Include Outlet Velocity Effect";
         this.groupBoxTwoStreamUnitOp.Controls.Add(this.checkBoxIncludeDimensions);
         this.checkBoxIncludeDimensions.Checked = this.FanCtrl.Fan.IncludeOutletVelocityEffect;
         this.checkBoxIncludeDimensions.CheckedChanged += new EventHandler(checkBoxIncludeDimensions_CheckedChanged);
         this.checkBoxIncludeDimensions.Size = new Size(192, 20);
         
         this.fanDimensionsCtrl = new FanDimensionsControl(this.FanCtrl);
         this.fanDimensionsCtrl.Location = new Point(4, 150);
         this.groupBoxTwoStreamUnitOp.Controls.Add(this.fanDimensionsCtrl);

         this.UpdateTheUI();
         
         this.inConstruction = false;
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
         this.Text = "Fan";
      }
      #endregion

      private void checkBoxIncludeDimensions_CheckedChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = this.FanCtrl.Fan.SpecifyIncludeOutletVelocityEffect(this.checkBoxIncludeDimensions.Checked);
            if (error != null)
               UI.ShowError(error);
            else
            {
               this.UpdateTheUI();
            }
         }
      }

      private void UpdateTheUI()
      {
         if (this.checkBoxIncludeDimensions.Checked)
         {
            this.fanDimensionsCtrl.Visible = true;
         }
         else
         {
            this.fanDimensionsCtrl.Visible = false;
         }
      }
   }
}
