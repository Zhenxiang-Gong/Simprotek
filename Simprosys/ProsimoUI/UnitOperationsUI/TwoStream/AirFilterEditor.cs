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
	/// Summary description for AirFilterEditor.
	/// </summary>
	public class AirFilterEditor : TwoStreamUnitOpEditor
	{
      public GroupBox AirFilterGroupBox
      {
         get {return this.groupBoxTwoStreamUnitOp;}
      }

      //public AirFilterControl AirFilterCtrl
      //{
      //   get {return (AirFilterControl)this.solvableCtrl;}
      //   set {this.solvableCtrl = value;}
      //}
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AirFilterEditor(AirFilterControl airFilterCtrl) : base(airFilterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.Text = "Air Filter: " + airFilterCtrl.AirFilter.Name;
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 200);
         this.groupBoxTwoStreamUnitOp.Text = "Air Filter";

         ProcessVarLabelsControl airFilterLabelsCtrl = new ProcessVarLabelsControl(airFilterCtrl.Solvable.VarList);
         this.groupBoxTwoStreamUnitOp.Controls.Add(airFilterLabelsCtrl);
         airFilterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl airFilterValuesCtrl = new ProcessVarValuesControl(airFilterCtrl);
         this.groupBoxTwoStreamUnitOp.Controls.Add(airFilterValuesCtrl);
         airFilterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
