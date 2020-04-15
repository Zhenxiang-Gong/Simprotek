using System;
using System.Windows.Forms;
using System.Drawing;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using Prosimo;
using ProsimoUI.ProcVarsEditor;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ProcessVarTextBox.
	/// </summary>
   public class ProcessVarTextBox : TextBox
   {
      private INumericFormat iNumericFormat;
      private ProcessVar var;

      public ProcessVarTextBox()
      {
         this.TextAlign = HorizontalAlignment.Right;
         this.Size = new System.Drawing.Size(80, 20);
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
            
            UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(ProcessVarTextBox_CurrentUnitSystemChanged);
            this.iNumericFormat.NumericFormatStringChanged += new NumericFormatStringChangedEventHandler(iNumericFormat_NumericFormatStringChanged);
            this.Validating += new System.ComponentModel.CancelEventHandler(ProcessVarTextBox_Validating);
            // NOTE: the subscription to the KeyUp event is done in the context where this textBox is used
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

      private void ProcessVarTextBox_CurrentUnitSystemChanged(UnitSystem unitSystem)
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

            if (this.var.IsSpecified)
            {
               this.ReadOnly = false;
               this.BackColor = Color.White;
            }
            else
            {
               this.ReadOnly = true;
               this.BackColor = Color.Gainsboro;
            }
         }
         else if (this.var is ProcessVarInt)
         {
            ProcessVarInt varInt = (ProcessVarInt)this.var;
            if (varInt.Value == Constants.NO_VALUE_INT)
               this.Text = "";
            else
               this.Text = varInt.Value.ToString(UI.DECIMAL);

            if (this.var.IsSpecified)
            {
               this.ReadOnly = false;
               this.BackColor = Color.White;
            }
            else
            {
               this.ReadOnly = true;
               this.BackColor = Color.Gainsboro;
            }
         }
      }

      private void ProcessVarTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         ErrorMessage error = null;
         if (this.Text.Trim().Equals(""))
         {
            if (this.var.IsSpecified)
            {
               if (this.var is ProcessVarDouble)
               {
                  ProcessVarDouble varDouble = (ProcessVarDouble)this.var;
                  error = this.var.Owner.Specify(varDouble, Constants.NO_VALUE);
               }
               else if (this.var is ProcessVarInt)
               {
                  ProcessVarInt varInt = (ProcessVarInt)this.var;
                  error = this.var.Owner.Specify(varInt, Constants.NO_VALUE_INT);
               }
               if (error != null)
               {
                  this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                  UI.ShowError(error);
               }
            }
         }
         else
         {
            try
            {
               if (this.var is ProcessVarDouble)
               {
                  ProcessVarDouble varDouble = (ProcessVarDouble)this.var;
                  double val = Double.Parse(this.Text);
                  double val2 = 0.0;
                  try
                  {
                     if (this.var.IsSpecified)
                     {
                        val2 = UnitSystemService.GetInstance().ConvertToSIValue(this.var.Type, val);
                        error = this.var.Owner.Specify(varDouble, val2);
                        if (error != null)
                        {
                           if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsInappropriate)
                           {
                              ProcVarsInappropriateForm form = new ProcVarsInappropriateForm(this.iNumericFormat, this.var, val, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsOutOfRange &&
                              error.ProcessVarsAndValues.ProcessVarList.Count == 1)
                           {
                              ProcVarsOnlyOneOutOfRangeForm form = new ProcVarsOnlyOneOutOfRangeForm(this.iNumericFormat, this.var.Owner, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsOutOfRange &&
                              error.ProcessVarsAndValues.ProcessVarList.Count > 1)
                           {
                              ProcVarsMoreThenOneOutOfRangeForm form = new ProcVarsMoreThenOneOutOfRangeForm(this.iNumericFormat, this.var.Owner, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueOutOfRange)
                           {
                              ProcVarOutOfRangeForm form = new ProcVarOutOfRangeForm(this.iNumericFormat, this.var, val, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else
                           {
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                              UI.ShowError(error);
                           }
                        }
                     }
                  }
                  catch (Exception ex1)
                  {
                     e.Cancel = true;
                     string message1 = ex1.ToString();
                     MessageBox.Show(message1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                  }
               }
               else if (this.var is ProcessVarInt)
               {
                  ProcessVarInt varInt = (ProcessVarInt)this.var;
                  int val = Int32.Parse(this.Text);
                  try
                  {
                     if (this.var.IsSpecified)
                     {
                        error = this.var.Owner.Specify(varInt, val);
                        if (error != null)
                        {
                           if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsInappropriate)
                           {
                              ProcVarsInappropriateForm form = new ProcVarsInappropriateForm(this.iNumericFormat, this.var, val, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsOutOfRange &&
                              error.ProcessVarsAndValues.ProcessVarList.Count == 1)
                           {
                              ProcVarsOnlyOneOutOfRangeForm form = new ProcVarsOnlyOneOutOfRangeForm(this.iNumericFormat, this.var.Owner, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueCausingOtherVarsOutOfRange &&
                              error.ProcessVarsAndValues.ProcessVarList.Count > 1)
                           {
                              ProcVarsMoreThenOneOutOfRangeForm form = new ProcVarsMoreThenOneOutOfRangeForm(this.iNumericFormat, this.var.Owner, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else if (error.Type == ErrorType.SpecifiedValueOutOfRange)
                           {
                              ProcVarOutOfRangeForm form = new ProcVarOutOfRangeForm(this.iNumericFormat, this.var, val, error);
                              form.ShowDialog();
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                           }
                           else
                           {
                              this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                              UI.ShowError(error);
                           }
                        }
                     }
                  }
                  catch (Exception ex1)
                  {
                     e.Cancel = true;
                     string message1 = ex1.ToString();
                     MessageBox.Show(message1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                     this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
                  }
               }
            }
            catch (FormatException)
            {
               e.Cancel = true;
               string message2 = "Please enter a numeric value!";
               MessageBox.Show(message2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               this.UpdateVariableValue(UnitSystemService.GetInstance().CurrentUnitSystem);
            }
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
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(ProcessVarTextBox_CurrentUnitSystemChanged);
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
