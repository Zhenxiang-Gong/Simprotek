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
	/// Summary description for FabricFilterEditor.
	/// </summary>
	public class FabricFilterEditor : TwoStreamUnitOpEditor
	{
      public GroupBox FabricFilterGroupBox
      {
         get {return this.groupBoxTwoStreamUnitOp;}
      }

      public FabricFilterControl FabricFilterCtrl
      {
         get {return (FabricFilterControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FabricFilterEditor(FabricFilterControl fabricFilterCtrl) : base(fabricFilterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.Text = "Fabric Filter: " + fabricFilterCtrl.FabricFilter.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 200);
         this.groupBoxTwoStreamUnitOp.Text = "Fabric Filter";

         ProcessVarLabelsControl fabricFilterLabelsCtrl = new ProcessVarLabelsControl(fabricFilterCtrl.FabricFilter.VarList);
         this.groupBoxTwoStreamUnitOp.Controls.Add(fabricFilterLabelsCtrl);
         fabricFilterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl fabricFilterValuesCtrl = new ProcessVarValuesControl(this.FabricFilterCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(fabricFilterValuesCtrl);
         fabricFilterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
