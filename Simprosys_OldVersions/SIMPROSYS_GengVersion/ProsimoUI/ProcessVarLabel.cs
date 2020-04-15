using System;
using System.Windows.Forms;
using System.Drawing;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ProcessVarLabel.
	/// </summary>
   public class ProcessVarLabel : RichTextBox
   {
       private ContentAlignment textAlign;
       public ContentAlignment TextAlign
       {
           get { return textAlign; }
           set{}
       }
      private ProcessVar var;
      public ProcessVar Variable
      {
         get {return var;}
      }

      public ProcessVarLabel()
      {
        this.BorderStyle = BorderStyle.None;
        //this.Enabled = false;
        this.BackColor = System.Drawing.Color.AliceBlue;
        this.SelectionBackColor = System.Drawing.Color.AliceBlue;
        //this.CanFocus = false;
        this.ReadOnly = true;
         //this.Size = new System.Drawing.Size(192, 20);
          this.AutoSize = true;
        // this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      }

      public void InitializeVariable(ProcessVar var)
      {
         string    unitName;
         this.var = var;

         this.Clear();
         this.SelectedText = var.VarTypeName;
         unitName = UI.GetUnitName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
         if (unitName != null)
         {
             this.SelectionCharOffset = UI.SUB_CHAR_OFFSET; //superscript
             this.SelectedText = (" (" + unitName + ")"); 
             //this.AppendText(" (" + unitName + ")");
             
         }
//         this.Text = UI.GetVariableName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
         this.var.NameChanged += new NameChangedEventHandler(var_NameChanged);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged += new CurrentUnitSystemChangedEventHandler(ProcessVarLabel_CurrentUnitSystemChanged);
      }

      private void ProcessVarLabel_CurrentUnitSystemChanged(UnitSystem unitSystem)
      {
//         this.Text = UI.GetVariableName(this.var, unitSystem);
          string unitName;

          this.Clear();
          this.SelectedText = var.VarTypeName;
          unitName = UI.GetUnitName(this.var, unitSystem);
          if (unitName != null)
          {
              this.SelectionCharOffset = UI.SUB_CHAR_OFFSET; //superscript
              this.SelectedText = (" (" + unitName + ")");
             // this.AppendText(unitName);
          }

      }

      protected override void Dispose( bool disposing )
      {
         if (this.var != null)
            this.var.NameChanged -= new NameChangedEventHandler(var_NameChanged);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -= new CurrentUnitSystemChangedEventHandler(ProcessVarLabel_CurrentUnitSystemChanged);
         base.Dispose( disposing );
      }

      private void var_NameChanged(object sender, string name, string oldName)
      {
//         this.Text = UI.GetVariableName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
          string unitName;

          this.Clear();
          this.SelectedText = this.var.VarTypeName;
          unitName = UI.GetUnitName(this.var, UnitSystemService.GetInstance().CurrentUnitSystem);
          if (unitName != null)
          {
              this.SelectionCharOffset = UI.SUB_CHAR_OFFSET; //superscript
              this.SelectedText = (" (" + unitName + ")");
          }

      }
   }
}
