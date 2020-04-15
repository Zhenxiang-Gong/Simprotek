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
	/// Summary description for CompressorEditor.
	/// </summary>
	public class CompressorEditor : TwoStreamUnitOpEditor
	{
      public CompressorControl CompressorCtrl
      {
         get {return (CompressorControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CompressorEditor()
      {
         //
         // Required for Windows Form Designer support
         //
         //InitializeComponent();
      }

		public CompressorEditor(CompressorControl compressorCtrl) : base(compressorCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			//InitializeComponent();

         Compressor compressor = this.CompressorCtrl.Compressor;
         this.Text = "Compressor: " + compressor.Name;
         //this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 180);
         //this.groupBoxTwoStreamUnitOp.Text = "Compressor";

         //CompressorLabelsControl compressorLabelsCtrl = new CompressorLabelsControl(this.CompressorCtrl.Compressor);
         //this.groupBoxTwoStreamUnitOp.Controls.Add(compressorLabelsCtrl);
         //compressorLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //CompressorValuesControl compressorValuesCtrl = new CompressorValuesControl(this.CompressorCtrl);
         //this.groupBoxTwoStreamUnitOp.Controls.Add(compressorValuesCtrl);
         //compressorValuesCtrl.Location = new Point(196, 12 + 20 + 2);
            initializeGrid(compressorCtrl, columnIndex, false, "Compressor");
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
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // CompressorEditor
         // 
         this.Text = "Compressor";
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
		#endregion
   }
}
