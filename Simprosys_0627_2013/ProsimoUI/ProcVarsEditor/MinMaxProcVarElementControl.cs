using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.ProcVarsEditor
{
   /// <summary>
   /// Summary description for MinMaxProcVarElementControl.
   /// </summary>
   public class MinMaxProcVarElementControl : System.Windows.Forms.UserControl
   {
      private ProcessVar var;
      public ProcessVar Variable
      {
         get {return var;}
      }

      public object NewValue
      {
         get {return this.GetNewValue();}
      }

      public bool IsNewValueValid
      {
         get {return this.NewValueValid();}
      }

      private ProsimoUI.ProcessVarLabel labelName;
      private ProsimoUI.ProcessVarNonEditableTextBox textBoxExistingValue;
      private System.Windows.Forms.TextBox textBoxNewValue;
      private System.Windows.Forms.TextBox textBoxMinValue;
      private System.Windows.Forms.TextBox textBoxMaxValue;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public MinMaxProcVarElementControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public MinMaxProcVarElementControl(INumericFormat iNumericFormat, ProcessVar var, object minMaxValues)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.var = var;

         this.labelName.InitializeVariable(var);
         this.textBoxExistingValue.InitializeVariable(iNumericFormat, var);

         string averageValStr = "";

         // note: the min and max values are not converted to the current unit system,
         //       and they also needs formating
         if (var is ProcessVarDouble)
         {
            DoubleRange doubleRange = (DoubleRange)minMaxValues;

            double valMin = doubleRange.MinValue;
            double val2Min = 0.0;
            val2Min = UnitSystemService.GetInstance().ConvertFromSIValue(var.Type, valMin);
            if (val2Min == Constants.NO_VALUE)
               this.textBoxMinValue.Text = "";
            else
               this.textBoxMinValue.Text = val2Min.ToString(iNumericFormat.NumericFormatString);

            double valMax = doubleRange.MaxValue;
            double val2Max = 0.0;
            val2Max = UnitSystemService.GetInstance().ConvertFromSIValue(var.Type, valMax);
            if (val2Max == Constants.NO_VALUE)
               this.textBoxMaxValue.Text = "";
            else
               this.textBoxMaxValue.Text = val2Max.ToString(iNumericFormat.NumericFormatString);

            double averageVal = (val2Min + val2Max)/2;
            averageValStr = averageVal.ToString(iNumericFormat.NumericFormatString);
         }
         else if (var is ProcessVarInt)
         {
            IntRange intRange = (IntRange)minMaxValues; 

            int valMin = intRange.MinValue;
            if (valMin == Constants.NO_VALUE_INT)
               this.textBoxMinValue.Text = "";
            else
               this.textBoxMinValue.Text = valMin.ToString(UI.DECIMAL);

            int valMax = intRange.MaxValue;
            if (valMax == Constants.NO_VALUE_INT)
               this.textBoxMaxValue.Text = "";
            else
               this.textBoxMaxValue.Text = valMax.ToString(UI.DECIMAL);

            int averageVal = (int)((valMin + valMax)/2);
            averageValStr = averageVal.ToString(iNumericFormat.NumericFormatString);
         }

         // as convenience, initialize the new value with the recommended value
         this.textBoxNewValue.Text = averageValStr;

         this.textBoxExistingValue.BackColor = Color.Gainsboro;
         this.textBoxMinValue.BackColor = Color.Gainsboro;
         this.textBoxMaxValue.BackColor = Color.Gainsboro;
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
         this.labelName = new ProsimoUI.ProcessVarLabel();
         this.textBoxExistingValue = new ProsimoUI.ProcessVarNonEditableTextBox();
         this.textBoxNewValue = new System.Windows.Forms.TextBox();
         this.textBoxMinValue = new System.Windows.Forms.TextBox();
         this.textBoxMaxValue = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // labelName
         // 
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelName.Location = new System.Drawing.Point(0, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(192, 20);
         this.labelName.TabIndex = 18;
         this.labelName.Text = "ProcVar Name";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxExistingValue
         // 
         this.textBoxExistingValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxExistingValue.Location = new System.Drawing.Point(192, 0);
         this.textBoxExistingValue.Name = "textBoxExistingValue";
         this.textBoxExistingValue.ReadOnly = true;
         this.textBoxExistingValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxExistingValue.TabIndex = 19;
         this.textBoxExistingValue.TabStop = false;
         this.textBoxExistingValue.Text = "";
         this.textBoxExistingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxNewValue
         // 
         this.textBoxNewValue.Location = new System.Drawing.Point(462, 0);
         this.textBoxNewValue.Name = "textBoxNewValue";
         this.textBoxNewValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxNewValue.TabIndex = 21;
         this.textBoxNewValue.Text = "";
         this.textBoxNewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxMinValue
         // 
         this.textBoxMinValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMinValue.Location = new System.Drawing.Point(282, 0);
         this.textBoxMinValue.Name = "textBoxMinValue";
         this.textBoxMinValue.ReadOnly = true;
         this.textBoxMinValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxMinValue.TabIndex = 22;
         this.textBoxMinValue.TabStop = false;
         this.textBoxMinValue.Text = "";
         this.textBoxMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxMaxValue
         // 
         this.textBoxMaxValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMaxValue.Location = new System.Drawing.Point(372, 0);
         this.textBoxMaxValue.Name = "textBoxMaxValue";
         this.textBoxMaxValue.ReadOnly = true;
         this.textBoxMaxValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxMaxValue.TabIndex = 23;
         this.textBoxMaxValue.TabStop = false;
         this.textBoxMaxValue.Text = "";
         this.textBoxMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // MinMaxProcVarElementControl
         // 
         this.Controls.Add(this.textBoxMaxValue);
         this.Controls.Add(this.textBoxMinValue);
         this.Controls.Add(this.textBoxNewValue);
         this.Controls.Add(this.textBoxExistingValue);
         this.Controls.Add(this.labelName);
         this.Name = "MinMaxProcVarElementControl";
         this.Size = new System.Drawing.Size(554, 20);
         this.ResumeLayout(false);

      }
      #endregion

      private bool NewValueValid()
      {
         bool isValid = false;
         if (!this.textBoxNewValue.Text.Trim().Equals(""))
         {
            try
            {
               if (this.var is ProcessVarDouble)
               {
                  Double.Parse(this.textBoxNewValue.Text);
                  isValid = true;
               }
               else if (this.var is ProcessVarInt)
               {
                  Int32.Parse(this.textBoxNewValue.Text);
                  isValid = true;
               }
            }
            catch (FormatException)
            {
            }
         }
         return isValid;
      }

      private object GetNewValue()
      {
         object newVal = null;
         if (this.textBoxNewValue.Text.Trim().Equals(""))
         {
            if (this.var is ProcessVarDouble)
            {
               newVal = Constants.NO_VALUE;
            }
            else if (this.var is ProcessVarInt)
            {
               newVal = Constants.NO_VALUE_INT;
            }
         }
         else
         {
            if (this.var is ProcessVarDouble)
            {
               try
               {
                  ProcessVarDouble varDouble = (ProcessVarDouble)this.var;
                  double val = Double.Parse(this.textBoxNewValue.Text);
                  double val2 = UnitSystemService.GetInstance().ConvertToSIValue(this.var.Type, val);
                  newVal = val2;
               }
               catch (FormatException)
               {
                  newVal = Constants.NO_VALUE;
               }
            }
            else if (this.var is ProcessVarInt)
            {
               try
               {
                  ProcessVarInt varInt = (ProcessVarInt)this.var;
                  int val = Int32.Parse(this.textBoxNewValue.Text);
                  newVal = val;
               }
               catch (FormatException)
               {
                  newVal = Constants.NO_VALUE_INT;
               }
            }
         }
         return newVal;
      }
   }
}
