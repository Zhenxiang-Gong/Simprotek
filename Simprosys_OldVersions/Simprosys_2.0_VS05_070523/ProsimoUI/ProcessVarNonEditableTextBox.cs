using System;
using System.Windows.Forms;
using System.Drawing;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI
{
   /// <summary>
   /// Summary description for ProcessVarNonEditableTextBox.
   /// </summary>
   public class ProcessVarNonEditableTextBox : TextBox
   {
      private INumericFormat iNumericFormat;
      private ProcessVar var;

      public ProcessVarNonEditableTextBox()
      {
         this.TextAlign = HorizontalAlignment.Right;
         this.Size = new System.Drawing.Size(80, 20);
         this.ReadOnly = true;
         this.BackColor = Color.Gainsboro;
      }

      public void InitializeVariable(INumericFormat iNumericFormat, ProcessVar var)
      {
         if (var.Enabled)
         {
            this.iNumericFormat = iNumericFormat;
            this.var = var;
            this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
            if (this.var.Owner is Solvable)
            {
               Solvable solvable = this.var.Owner as Solvable;
               solvable.SolveComplete += new SolveCompleteEventHandler(solvable_SolveComplete);
            }
            else
               this.var.Owner.ProcessVarValueCommitted += new ProcessVarValueCommittedEventHandler(Owner_ProcessVarValueCommitted);
            
            UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(ProcessVarNonEditableTextBox_CurrentUnitSystemChanged);
            this.iNumericFormat.NumericFormatStringChanged += new NumericFormatStringChangedEventHandler(iNumericFormat_NumericFormatStringChanged);
         }
         else
         {
            this.Enabled = false;
         }
      }

      private void Owner_ProcessVarValueCommitted(ProcessVar var)
      {
         this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
      }

      private void ProcessVarNonEditableTextBox_CurrentUnitSystemChanged(UnitSystem unitSystem)
      {
         this.UpdateVariableValue(unitSystem);
      }

      private void iNumericFormat_NumericFormatStringChanged(INumericFormat iNumericFormat)
      {
         this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
      }

      private void UpdateVariableValue(UnitSystem unitSystem)
      {
         if (this.var is ProcessVarDouble)
         {
            ProcessVarDouble varDouble = (ProcessVarDouble)this.var;
            double val = UnitSystemService.GetInstance().ConvertFromSIValue(this.var.Type, varDouble.Value);
            if (varDouble.Value == Constants.NO_VALUE)
               this.Text = "";
            else
               this.Text = val.ToString(this.iNumericFormat.NumericFormatString);
         }
         else if (this.var is ProcessVarInt)
         {
            ProcessVarInt varInt = (ProcessVarInt)this.var;
            if (varInt.Value == Constants.NO_VALUE_INT)
               this.Text = "";
            else
               this.Text = varInt.Value.ToString(UI.DECIMAL);
         }
      }

      protected override void Dispose( bool disposing )
      {
         if (this.var != null && this.var.Owner != null)
         {
            if (this.var.Owner is Solvable)
            {
               Solvable solvable = this.var.Owner as Solvable;
               solvable.SolveComplete -= new SolveCompleteEventHandler(solvable_SolveComplete);
            }
            else
               this.var.Owner.ProcessVarValueCommitted -= new ProcessVarValueCommittedEventHandler(Owner_ProcessVarValueCommitted);
         }
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(ProcessVarNonEditableTextBox_CurrentUnitSystemChanged);
         if (this.iNumericFormat != null)
            this.iNumericFormat.NumericFormatStringChanged -= new NumericFormatStringChangedEventHandler(iNumericFormat_NumericFormatStringChanged);
         base.Dispose( disposing );
      }

      private void solvable_SolveComplete(object sender, SolveState solveState)
      {
         this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
      }
   }
}
