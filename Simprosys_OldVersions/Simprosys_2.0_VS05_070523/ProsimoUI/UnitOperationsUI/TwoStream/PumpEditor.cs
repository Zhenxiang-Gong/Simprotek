using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for PumpEditor.
	/// </summary>
	public class PumpEditor : TwoStreamUnitOpEditor
	{
      public PumpControl PumpCtrl
      {
         get {return (PumpControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private PumpDimensionsControl pumpDimensionsCtrl;
      private System.Windows.Forms.Label labelDimensions;
      private System.Windows.Forms.CheckBox checkBoxIncludeDimensions;
      private bool inConstruction;

      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PumpEditor(PumpControl pumpCtrl) : base(pumpCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.inConstruction = true;

         Pump pump = this.PumpCtrl.Pump;
         this.Text = "Pump: " + pump.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 300);
         this.groupBoxTwoStreamUnitOp.Text = "Pump";

         PumpLabelsControl pumpLabelsCtrl = new PumpLabelsControl(pumpCtrl.Pump);
         this.groupBoxTwoStreamUnitOp.Controls.Add(pumpLabelsCtrl);
         pumpLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         PumpValuesControl pumpValuesCtrl = new PumpValuesControl(this.PumpCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(pumpValuesCtrl);
         pumpValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         //
         this.checkBoxIncludeDimensions = new CheckBox();
         this.checkBoxIncludeDimensions.Location = new System.Drawing.Point(4, 198);
         this.checkBoxIncludeDimensions.Name = "checkBoxIncludeDimensions";
         this.checkBoxIncludeDimensions.Text = "Include Outlet Velocity Effect";
         this.groupBoxTwoStreamUnitOp.Controls.Add(this.checkBoxIncludeDimensions);
         this.checkBoxIncludeDimensions.Checked = this.PumpCtrl.Pump.IncludeOutletVelocityEffect;
         this.checkBoxIncludeDimensions.CheckedChanged += new EventHandler(checkBoxIncludeDimensions_CheckedChanged);
         this.checkBoxIncludeDimensions.Size = new Size(192, 20);

         this.labelDimensions = new System.Windows.Forms.Label();
         this.labelDimensions.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.labelDimensions.Location = new System.Drawing.Point(4, 234);
         this.labelDimensions.Name = "labelDimensions";
         this.labelDimensions.Size = new System.Drawing.Size(192, 20);
         this.labelDimensions.Text = "Dimensions:";
         this.labelDimensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.groupBoxTwoStreamUnitOp.Controls.Add(this.labelDimensions);

         this.pumpDimensionsCtrl = new PumpDimensionsControl(this.PumpCtrl);
         this.pumpDimensionsCtrl.Location = new Point(4, 254);  
         this.groupBoxTwoStreamUnitOp.Controls.Add(this.pumpDimensionsCtrl);

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
         this.Text = "Pump";
      }
      #endregion

      private void checkBoxIncludeDimensions_CheckedChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = this.PumpCtrl.Pump.SpecifyIncludeOutletVelocityEffect(this.checkBoxIncludeDimensions.Checked);
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
            this.pumpDimensionsCtrl.Visible = true;
            this.labelDimensions.Visible = true;
         }
         else
         {
            this.pumpDimensionsCtrl.Visible = false;
            this.labelDimensions.Visible = false;
         }
      }
   }
}
