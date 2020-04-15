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
   /// Summary description for RecommProcVarElementControl.
   /// </summary>
   public class RecommProcVarElementControl : System.Windows.Forms.UserControl
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
      private System.Windows.Forms.TextBox textBoxRecommendedValue;
      private System.Windows.Forms.TextBox textBoxNewValue;
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public RecommProcVarElementControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public RecommProcVarElementControl(INumericFormat iNumericFormat, ProcessVar var, object recommValue)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.var = var;

         this.labelName.InitializeVariable(var);
         this.textBoxExistingValue.InitializeVariable(iNumericFormat, var);

         // note: the recommended value is not converted to the current unit system,
         //       and it also needs formating
         if (var is ProcessVarDouble)
         {
            double val = (double)recommValue;
            double val2 = 0.0;
            val2 = UnitSystemService.GetInstance().ConvertFromSIValue(var.Type, val);
            if (val2 == Constants.NO_VALUE)
               this.textBoxRecommendedValue.Text = "";
            else
               this.textBoxRecommendedValue.Text = val2.ToString(iNumericFormat.NumericFormatString);
         }
         else if (var is ProcessVarInt)
         {
            int val = (int)recommValue;
            if (val == Constants.NO_VALUE_INT)
               this.textBoxRecommendedValue.Text = "";
            else
               this.textBoxRecommendedValue.Text = val.ToString(UI.DECIMAL);
         }

         this.textBoxExistingValue.BackColor = Color.Gainsboro;
         this.textBoxRecommendedValue.BackColor = Color.Gainsboro;

         // as convenience, initialize the new value with the recommended value
         this.textBoxNewValue.Text = this.textBoxRecommendedValue.Text;
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
         this.textBoxRecommendedValue = new System.Windows.Forms.TextBox();
         this.textBoxNewValue = new System.Windows.Forms.TextBox();
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
         // textBoxRecommendedValue
         // 
         this.textBoxRecommendedValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxRecommendedValue.Location = new System.Drawing.Point(282, 0);
         this.textBoxRecommendedValue.Name = "textBoxRecommendedValue";
         this.textBoxRecommendedValue.ReadOnly = true;
         this.textBoxRecommendedValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxRecommendedValue.TabIndex = 20;
         this.textBoxRecommendedValue.TabStop = false;
         this.textBoxRecommendedValue.Text = "";
         this.textBoxRecommendedValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxNewValue
         // 
         this.textBoxNewValue.Location = new System.Drawing.Point(372, 0);
         this.textBoxNewValue.Name = "textBoxNewValue";
         this.textBoxNewValue.Size = new System.Drawing.Size(90, 20);
         this.textBoxNewValue.TabIndex = 21;
         this.textBoxNewValue.Text = "";
         this.textBoxNewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // RecommProcVarElementControl
         // 
         this.Controls.Add(this.textBoxNewValue);
         this.Controls.Add(this.textBoxRecommendedValue);
         this.Controls.Add(this.textBoxExistingValue);
         this.Controls.Add(this.labelName);
         this.Name = "RecommProcVarElementControl";
         this.Size = new System.Drawing.Size(464, 20);
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
