using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;

namespace ProsimoUI.GlobalEditor
{
	/// <summary>
	/// Summary description for FansEditor.
	/// </summary>
	public class FansEditor : UnitOpsEditor
	{
      private const string TITLE = "Fans:";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FansEditor() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public FansEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return FansEditor.TITLE;
      }

      protected override Type GetUnitOpType() {
         return typeof(Fan);
      }

      protected override UserControl GetNewUnitOpLabelsControl(Solvable solvable)
      {
         return new FanLabelsControl((Fan)solvable);
      }

      protected override IList GetShowableInEditorUnitOpControls()
      {
         return this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Fan>();
      }

      protected override UserControl GetNewUnitOpValuesControl(UnitOpControl unitOpCtrl)
      {
         return new FanValuesControl((FanControl)unitOpCtrl);
      }
   }
}
