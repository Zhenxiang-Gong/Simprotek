using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.DryerUI;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for DryersEditor.
	/// </summary>
	public class DryersEditor : UnitOpsEditor
	{
      private const string TITLE = "Dryers:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DryersEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public DryersEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return DryersEditor.TITLE;
      }

      protected override Type GetUnitOpType() {
         return typeof(Dryer);
      }

      protected override UserControl GetNewUnitOpLabelsControl(Solvable solvable)
      {
         return new DryerLabelsControl((Dryer)solvable);
      }

      protected override IList GetShowableInEditorUnitOpControls()
      {
         return this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Dryer>();
      }

      protected override UserControl GetNewUnitOpValuesControl(UnitOpControl unitOpCtrl)
      {
         return new DryerValuesControl((DryerControl)unitOpCtrl);
      }
   }
}
