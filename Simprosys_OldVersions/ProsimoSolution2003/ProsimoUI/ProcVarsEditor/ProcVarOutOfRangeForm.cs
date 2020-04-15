using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI.ProcVarsEditor
{
   /// <summary>
   /// Summary description for ProcVarOutOfRangeForm.
   /// </summary>
   public class ProcVarOutOfRangeForm : System.Windows.Forms.Form
   {
      private ProcessVar var;

      private System.Windows.Forms.Panel panelNorth;
      private System.Windows.Forms.Label labelMessage;
      private System.Windows.Forms.Panel panelSouth;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private ProsimoUI.ProcessVarNonEditableTextBox textBoxExistingValue;
      private ProsimoUI.ProcessVarLabel labelProcVarName;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.GroupBox groupBoxInitialProcVar;
      private System.Windows.Forms.Label labelNewValue;
      private System.Windows.Forms.Label labelCurrentValue;
      private System.Windows.Forms.TextBox textBoxMinValue;
      private System.Windows.Forms.TextBox textBoxMaxValue;
      private System.Windows.Forms.Label labelMinValue;
      private System.Windows.Forms.Label labelMaxValue;
      private System.Windows.Forms.TextBox textBoxNewValue;
      private System.Windows.Forms.Label labelRespecifyValue;
      private System.Windows.Forms.TextBox textBoxSpecifiedValue;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcVarOutOfRangeForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public ProcVarOutOfRangeForm(INumericFormat iNumericFormat, ProcessVar var, object varSpecifiedValue, ErrorMessage error)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
   
         this.Text = error.Title;

         this.var = var;

         // display the message
         this.labelMessage.Text = error.Message;

         // dispaly the variable
         this.labelProcVarName.InitializeVariable(var);
         this.labelProcVarName.BorderStyle = BorderStyle.None;
         this.textBoxExistingValue.InitializeVariable(iNumericFormat, var);

         // note: the specified value is already converted to the current unit system,
         //       it needs only to be formated
         if (var is ProcessVarDouble)
         {
            double val = (double)varSpecifiedValue;
            if (val == Constants.NO_VALUE)
               this.textBoxSpecifiedValue.Text = "";
            else
               this.textBoxSpecifiedValue.Text = val.ToString(iNumericFormat.NumericFormatString);
         }
         else if (var is ProcessVarInt)
         {
            int val = (int)varSpecifiedValue;
            if (val == Constants.NO_VALUE_INT)
               this.textBoxSpecifiedValue.Text = "";
            else
               this.textBoxSpecifiedValue.Text = val.ToString(UI.DECIMAL);
         }
        
         string averageValStr = "";
         object minMaxValues = error.ProcessVarsAndValues.GetVarRange(var);
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
         this.textBoxSpecifiedValue.BackColor = Color.Gainsboro;
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

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.panelNorth = new System.Windows.Forms.Panel();
         this.groupBoxInitialProcVar = new System.Windows.Forms.GroupBox();
         this.labelRespecifyValue = new System.Windows.Forms.Label();
         this.textBoxNewValue = new System.Windows.Forms.TextBox();
         this.labelMaxValue = new System.Windows.Forms.Label();
         this.labelMinValue = new System.Windows.Forms.Label();
         this.textBoxMaxValue = new System.Windows.Forms.TextBox();
         this.textBoxMinValue = new System.Windows.Forms.TextBox();
         this.labelNewValue = new System.Windows.Forms.Label();
         this.labelCurrentValue = new System.Windows.Forms.Label();
         this.labelName = new System.Windows.Forms.Label();
         this.labelProcVarName = new ProsimoUI.ProcessVarLabel();
         this.textBoxExistingValue = new ProsimoUI.ProcessVarNonEditableTextBox();
         this.textBoxSpecifiedValue = new System.Windows.Forms.TextBox();
         this.labelMessage = new System.Windows.Forms.Label();
         this.panelSouth = new System.Windows.Forms.Panel();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panelNorth.SuspendLayout();
         this.groupBoxInitialProcVar.SuspendLayout();
         this.panelSouth.SuspendLayout();
         this.SuspendLayout();
         // 
         // panelNorth
         // 
         this.panelNorth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelNorth.Controls.Add(this.groupBoxInitialProcVar);
         this.panelNorth.Controls.Add(this.labelMessage);
         this.panelNorth.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelNorth.Location = new System.Drawing.Point(0, 0);
         this.panelNorth.Name = "panelNorth";
         this.panelNorth.Size = new System.Drawing.Size(466, 264);
         this.panelNorth.TabIndex = 0;
         // 
         // groupBoxInitialProcVar
         // 
         this.groupBoxInitialProcVar.Controls.Add(this.labelRespecifyValue);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxNewValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelMaxValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelMinValue);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxMaxValue);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxMinValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelNewValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelCurrentValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelName);
         this.groupBoxInitialProcVar.Controls.Add(this.labelProcVarName);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxExistingValue);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxSpecifiedValue);
         this.groupBoxInitialProcVar.Location = new System.Drawing.Point(8, 68);
         this.groupBoxInitialProcVar.Name = "groupBoxInitialProcVar";
         this.groupBoxInitialProcVar.Size = new System.Drawing.Size(448, 156);
         this.groupBoxInitialProcVar.TabIndex = 26;
         this.groupBoxInitialProcVar.TabStop = false;
         this.groupBoxInitialProcVar.Text = "Variable Being Specified";
         // 
         // labelRespecifyValue
         // 
         this.labelRespecifyValue.Location = new System.Drawing.Point(12, 132);
         this.labelRespecifyValue.Name = "labelRespecifyValue";
         this.labelRespecifyValue.Size = new System.Drawing.Size(120, 16);
         this.labelRespecifyValue.TabIndex = 33;
         this.labelRespecifyValue.Text = "Respecify Value:";
         this.labelRespecifyValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxNewValue
         // 
         this.textBoxNewValue.Location = new System.Drawing.Point(136, 128);
         this.textBoxNewValue.Name = "textBoxNewValue";
         this.textBoxNewValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxNewValue.TabIndex = 32;
         this.textBoxNewValue.Text = "";
         this.textBoxNewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelMaxValue
         // 
         this.labelMaxValue.Location = new System.Drawing.Point(12, 112);
         this.labelMaxValue.Name = "labelMaxValue";
         this.labelMaxValue.Size = new System.Drawing.Size(120, 16);
         this.labelMaxValue.TabIndex = 31;
         this.labelMaxValue.Text = "Max. Value:";
         this.labelMaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelMinValue
         // 
         this.labelMinValue.Location = new System.Drawing.Point(12, 92);
         this.labelMinValue.Name = "labelMinValue";
         this.labelMinValue.Size = new System.Drawing.Size(120, 16);
         this.labelMinValue.TabIndex = 30;
         this.labelMinValue.Text = "Min. Value:";
         this.labelMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxMaxValue
         // 
         this.textBoxMaxValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMaxValue.Location = new System.Drawing.Point(136, 108);
         this.textBoxMaxValue.Name = "textBoxMaxValue";
         this.textBoxMaxValue.ReadOnly = true;
         this.textBoxMaxValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxMaxValue.TabIndex = 29;
         this.textBoxMaxValue.TabStop = false;
         this.textBoxMaxValue.Text = "";
         this.textBoxMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxMinValue
         // 
         this.textBoxMinValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMinValue.Location = new System.Drawing.Point(136, 88);
         this.textBoxMinValue.Name = "textBoxMinValue";
         this.textBoxMinValue.ReadOnly = true;
         this.textBoxMinValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxMinValue.TabIndex = 28;
         this.textBoxMinValue.TabStop = false;
         this.textBoxMinValue.Text = "";
         this.textBoxMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelNewValue
         // 
         this.labelNewValue.Location = new System.Drawing.Point(12, 72);
         this.labelNewValue.Name = "labelNewValue";
         this.labelNewValue.Size = new System.Drawing.Size(120, 16);
         this.labelNewValue.TabIndex = 27;
         this.labelNewValue.Text = "Last Specified Value:";
         this.labelNewValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelCurrentValue
         // 
         this.labelCurrentValue.Location = new System.Drawing.Point(12, 52);
         this.labelCurrentValue.Name = "labelCurrentValue";
         this.labelCurrentValue.Size = new System.Drawing.Size(120, 16);
         this.labelCurrentValue.TabIndex = 26;
         this.labelCurrentValue.Text = "Existing Value:";
         this.labelCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelName
         // 
         this.labelName.Location = new System.Drawing.Point(12, 24);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(120, 16);
         this.labelName.TabIndex = 25;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // labelProcVarName
         // 
         this.labelProcVarName.Location = new System.Drawing.Point(136, 24);
         this.labelProcVarName.Name = "labelProcVarName";
         this.labelProcVarName.Size = new System.Drawing.Size(304, 16);
         this.labelProcVarName.TabIndex = 22;
         this.labelProcVarName.Text = "ProcVar Name";
         this.labelProcVarName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxExistingValue
         // 
         this.textBoxExistingValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxExistingValue.Location = new System.Drawing.Point(136, 48);
         this.textBoxExistingValue.Name = "textBoxExistingValue";
         this.textBoxExistingValue.ReadOnly = true;
         this.textBoxExistingValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxExistingValue.TabIndex = 23;
         this.textBoxExistingValue.TabStop = false;
         this.textBoxExistingValue.Text = "";
         this.textBoxExistingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxSpecifiedValue
         // 
         this.textBoxSpecifiedValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxSpecifiedValue.Location = new System.Drawing.Point(136, 68);
         this.textBoxSpecifiedValue.Name = "textBoxSpecifiedValue";
         this.textBoxSpecifiedValue.ReadOnly = true;
         this.textBoxSpecifiedValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecifiedValue.TabIndex = 24;
         this.textBoxSpecifiedValue.TabStop = false;
         this.textBoxSpecifiedValue.Text = "";
         this.textBoxSpecifiedValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelMessage
         // 
         this.labelMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelMessage.Location = new System.Drawing.Point(8, 8);
         this.labelMessage.Name = "labelMessage";
         this.labelMessage.Size = new System.Drawing.Size(448, 48);
         this.labelMessage.TabIndex = 0;
         // 
         // panelSouth
         // 
         this.panelSouth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelSouth.Controls.Add(this.buttonCancel);
         this.panelSouth.Controls.Add(this.buttonOk);
         this.panelSouth.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelSouth.Location = new System.Drawing.Point(0, 232);
         this.panelSouth.Name = "panelSouth";
         this.panelSouth.Size = new System.Drawing.Size(466, 32);
         this.panelSouth.TabIndex = 1;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(240, 4);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 1;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(152, 4);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 0;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // ProcVarOutOfRangeForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(466, 264);
         this.Controls.Add(this.panelSouth);
         this.Controls.Add(this.panelNorth);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProcVarOutOfRangeForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Process Variables";
         this.panelNorth.ResumeLayout(false);
         this.groupBoxInitialProcVar.ResumeLayout(false);
         this.panelSouth.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         if (NewValueValid())
         {
            // build the hashtable with procVars and values
            Hashtable hashtable = new Hashtable();
            hashtable.Add(this.var, GetNewValue());

            ErrorMessage error = this.var.Owner.Specify(hashtable);
            if (error != null)
            {
               UI.ShowError(error);
            }
            this.Close();
         }
         else
         {
            string message = "Please enter numeric values!";
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
      }

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

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }
   }
}
