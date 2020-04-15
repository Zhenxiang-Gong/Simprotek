using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for HeaterEditor.
	/// </summary>
	public class HeaterEditor : TwoStreamUnitOpEditor
	{
      public HeaterControl HeaterCtrl
      {
         get {return (HeaterControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeaterEditor(HeaterControl heaterCtrl) : base(heaterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
         //   InitializeComponent();

            Heater heater = this.HeaterCtrl.Heater;
            this.Text = "Heater: " + heater.Name;
         //this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 100);
         //this.groupBoxTwoStreamUnitOp.Text = "Heater";
         
         //HeaterLabelsControl heaterLabelsCtrl = new HeaterLabelsControl(heaterCtrl.Heater);
         //this.groupBoxTwoStreamUnitOp.Controls.Add(heaterLabelsCtrl);
         //heaterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //HeaterValuesControl heaterValuesCtrl = new HeaterValuesControl(this.HeaterCtrl);
         //this.groupBoxTwoStreamUnitOp.Controls.Add(heaterValuesCtrl);
         //heaterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
            initializeGrid(heaterCtrl, columnIndex, false, "Heater");
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
         this.Text = "Heater";
      }
		#endregion

   }
}
