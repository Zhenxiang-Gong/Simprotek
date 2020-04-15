using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for CoolerEditor.
	/// </summary>
	public class CoolerEditor : TwoStreamUnitOpEditor
	{
      public CoolerControl CoolerCtrl
      {
         get {return (CoolerControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CoolerEditor(CoolerControl coolerCtrl) : base(coolerCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         Cooler cooler = this.CoolerCtrl.Cooler;
         this.Text = "Cooler: " + cooler.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 100);
         this.groupBoxTwoStreamUnitOp.Text = "Cooler";

         ProcessVarLabelsControl coolerLabelsCtrl = new ProcessVarLabelsControl(this.CoolerCtrl.Cooler.VarList);
         this.groupBoxTwoStreamUnitOp.Controls.Add(coolerLabelsCtrl);
         coolerLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl coolerValuesCtrl = new ProcessVarValuesControl(this.CoolerCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(coolerValuesCtrl);
         coolerValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
         this.Text = "Cooler";
      }
		#endregion

   }
}
