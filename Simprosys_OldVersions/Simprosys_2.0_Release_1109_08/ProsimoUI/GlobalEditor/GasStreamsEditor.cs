using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.GlobalEditor
{
   /// <summary>
	/// Summary description for GasStreamsEditor.
	/// </summary>
	public class GasStreamsEditor : StreamsEditor
	{
      private const string TITLE = "Gas Streams:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GasStreamsEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public GasStreamsEditor(Flowsheet flowsheet) : base(flowsheet)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

      protected override string GetTitle()
      {
         return GasStreamsEditor.TITLE;
      }

      protected override IList GetStreamList()
      {
         return this.flowsheet.EvaporationAndDryingSystem.GetSolvableList(typeof(DryingGasStream));
      }

      protected override UserControl GetNewStreamLabelsControl(Solvable solvable)
      {
         return new GasStreamLabelsControl((DryingGasStream)solvable);
      }

      protected override IList GetShowableInEditorStreamControls()
      {
         return this.flowsheet.StreamManager.GetShowableInEditorStreamControls<DryingGasStream>();
      }

      protected override UserControl GetNewStreamValuesControl(ProcessStreamBaseControl streamCtrl)
      {
         return new GasStreamValuesControl((GasStreamControl)streamCtrl);
      }
   }
}
