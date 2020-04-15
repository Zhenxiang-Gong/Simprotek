using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for BagFiltersEditor.
	/// </summary>
	public class BagFiltersEditor : UnitOpsEditor
	{
      private const string TITLE = "Bag Filters:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BagFiltersEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public BagFiltersEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return BagFiltersEditor.TITLE;
      }

      protected override IList GetUnitOpList()
      {
         return this.flowsheet.EvaporationAndDryingSystem.GetUnitOpList(typeof(BagFilter));
      }

      protected override UserControl GetNewUnitOpLabelsControl(Solvable solvable)
      {
         return new BagFilterLabelsControl((BagFilter)solvable);
      }

      protected override IList GetShowableInEditorUnitOpControls()
      {
         return this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<BagFilter>();
      }

      protected override UserControl GetNewUnitOpValuesControl(UnitOpControl unitOpCtrl)
      {
         return new BagFilterValuesControl((BagFilterControl)unitOpCtrl);
      }
   }
}
