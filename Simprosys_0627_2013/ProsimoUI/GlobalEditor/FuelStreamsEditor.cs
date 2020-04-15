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
	/// Summary description for MaterialStreamsEditor.
	/// </summary>
	public class FuelStreamsEditor : StreamsEditor
	{
      private const string TITLE = "Fuel Streams:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FuelStreamsEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public FuelStreamsEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return TITLE;
      }

      protected override IList GetStreamList()
      {
         return this.flowsheet.EvaporationAndDryingSystem.GetSolvableList(typeof(DetailedFuelStream));
      }

      protected override UserControl GetNewStreamLabelsControl(Solvable solvable)
      {
         //return new MaterialStreamLabelsControl((DetailedFuelStream)solvable);
         return new ProcessVarLabelsControl(solvable.VarList);
      }

      protected override IList GetShowableInEditorStreamControls()
      {
         return this.flowsheet.StreamManager.GetShowableInEditorStreamControls<DetailedFuelStream>();
      }

      protected override UserControl GetNewStreamValuesControl(ProcessStreamBaseControl streamCtrl)
      {
         //return new MaterialStreamValuesControl((MaterialStreamControl)streamCtrl);
         return new ProcessVarValuesControl(streamCtrl);
      }
   }
}
