using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for BagFilterEditor.
	/// </summary>
	public class BagFilterEditor : FabricFilterEditor
	{
      public BagFilterControl BagFilterCtrl
      {
         get {return (BagFilterControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BagFilterEditor(BagFilterControl bagFilterCtrl) : base(bagFilterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.Text = "Bag Filter: " + bagFilterCtrl.BagFilter.Name;

         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 260);
         this.groupBoxTwoStreamUnitOp.Controls.Clear();
         this.groupBoxTwoStreamUnitOp.Text = "Bag Filter";

         //BagFilterLabelsControl bagFilterLabelsCtrl = new BagFilterLabelsControl(bagFilterCtrl.BagFilter);
         ProcessVarLabelsControl bagFilterLabelsCtrl = new ProcessVarLabelsControl(bagFilterCtrl.BagFilter.VarList);
         this.groupBoxTwoStreamUnitOp.Controls.Add(bagFilterLabelsCtrl);
         bagFilterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //BagFilterValuesControl bagFilterValuesCtrl = new BagFilterValuesControl(this.BagFilterCtrl);
         ProcessVarValuesControl bagFilterValuesCtrl = new ProcessVarValuesControl(this.BagFilterCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(bagFilterValuesCtrl);
         bagFilterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
      }
		#endregion

   }
}
