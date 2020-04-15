using System;
using System.Windows.Forms;
using System.Drawing;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for ProcessVarLabel.
   /// </summary>
   public class ProcessVarLabel : Label {
      private ProcessVar var;
      public ProcessVar Variable {
         get { return var; }
      }

      public ProcessVarLabel() {
         this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.Size = new System.Drawing.Size(192, 20);
         this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      }

      public void InitializeVariable(ProcessVar var) {
         this.var = var;
         this.Text = SolvableControl.GetVariableName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
         this.var.NameChanged += new NameChangedEventHandler(var_NameChanged);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(ProcessVarLabel_CurrentUnitSystemChanged);
      }

      private void ProcessVarLabel_CurrentUnitSystemChanged(UnitSystem unitSystem) {
         this.Text = SolvableControl.GetVariableName(this.var, unitSystem);
      }

      protected override void Dispose(bool disposing) {
         if (this.var != null)
            this.var.NameChanged -= new NameChangedEventHandler(var_NameChanged);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(ProcessVarLabel_CurrentUnitSystemChanged);
         base.Dispose(disposing);
      }

      private void var_NameChanged(object sender, string name, string oldName) {
         this.Text = SolvableControl.GetVariableName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
      }
   }
}
