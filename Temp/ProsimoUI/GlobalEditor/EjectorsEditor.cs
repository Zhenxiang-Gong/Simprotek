using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;
using ProsimoUI.UnitOperationsUI;

namespace ProsimoUI.GlobalEditor
{
   /// <summary>
   /// Summary description for EjectorsEditor.
   /// </summary>
   public class EjectorsEditor : UnitOpsEditor
   {
      private const string TITLE = "Ejectors:";

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public EjectorsEditor() : base()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public EjectorsEditor(Flowsheet flowsheet) : base(flowsheet)
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
         return EjectorsEditor.TITLE;
      }

      protected override IList GetUnitOpList()
      {
         return this.flowsheet.EvaporationAndDryingSystem.GetUnitOpList(typeof(Ejector));
      }

      protected override UserControl GetNewUnitOpLabelsControl(Solvable solvable)
      {
         return new EjectorLabelsControl((Ejector)solvable);
      }

      protected override IList GetShowableInEditorUnitOpControls()
      {
         return this.flowsheet.UnitOpManager.GetShowableInEditorUnitOpControls<Ejector>();
      }

      protected override UserControl GetNewUnitOpValuesControl(UnitOpControl unitOpCtrl)
      {
         return new EjectorValuesControl((EjectorControl)unitOpCtrl);
      }
   }
}
