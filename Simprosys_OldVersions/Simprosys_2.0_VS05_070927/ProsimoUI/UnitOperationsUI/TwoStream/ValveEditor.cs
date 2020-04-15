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

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for ValveEditor.
	/// </summary>
	public class ValveEditor : TwoStreamUnitOpEditor
	{
      public ValveControl ValveCtrl
      {
         get {return (ValveControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ValveEditor(ValveControl valveCtrl) : base(valveCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         Valve valve = this.ValveCtrl.Valve;
         this.Text = "Valve: " + valve.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 60);
         this.groupBoxTwoStreamUnitOp.Text = "Valve";

         ValveLabelsControl valveLabelsCtrl = new ValveLabelsControl(valveCtrl.Valve);
         this.groupBoxTwoStreamUnitOp.Controls.Add(valveLabelsCtrl);
         valveLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ValveValuesControl valveValuesCtrl = new ValveValuesControl(this.ValveCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(valveValuesCtrl);
         valveValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
         this.Text = "Valve";
      }
		#endregion
   }
}
