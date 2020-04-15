using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.HeatExchangerUI;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for HeatExchangersEditor.
	/// </summary>
	public class HeatExchangersEditor : UnitOpsEditor
	{
      private const string TITLE = "Heat Exchangers:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeatExchangersEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public HeatExchangersEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return HeatExchangersEditor.TITLE;
      }

      protected override IList GetUnitOpList()
      {
         return this.flowsheet.EvaporationAndDryingSystem.GetUnitOpList(typeof(HeatExchanger));
      }

      protected override UserControl GetNewUnitOpLabelsControl(Solvable solvable)
      {
         return new HeatExchangerLabelsControl((HeatExchanger)solvable);
      }

      protected override IList GetShowableInEditorUnitOpControls()
      {
         return this.flowsheet.UnitOpManager.GetShowableInEditorHeatExchangerControls();
      }

      protected override UserControl GetNewUnitOpValuesControl(UnitOpControl unitOpCtrl)
      {
         return new HeatExchangerValuesControl((HeatExchangerControl)unitOpCtrl);
      }
   }
}
