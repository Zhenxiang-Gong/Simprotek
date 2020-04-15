using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo;
using Prosimo.UnitOperations;

namespace ProsimoUI.ProcVarsEditor
{
   /// <summary>
   /// Summary description for ProcVarsMoreThenOneOutOfRangeForm.
   /// </summary>
   public class ProcVarsMoreThenOneOutOfRangeForm : System.Windows.Forms.Form
   {
      private IProcessVarOwner solvable;

      private System.Windows.Forms.Panel panelNorth;
      private System.Windows.Forms.Label labelMessage;
      private System.Windows.Forms.Panel panelSouth;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel panelCenter;
      private ProsimoUI.ProcVarsEditor.MinMaxProcVarHeaderControl procVarHeaderControl;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcVarsMoreThenOneOutOfRangeForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public ProcVarsMoreThenOneOutOfRangeForm(INumericFormat iNumericFormat, IProcessVarOwner solvable, ErrorMessage error)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
   
         this.solvable = solvable;
         this.Text = error.Title;

         // display the message
         this.labelMessage.Text = error.Message;

         IEnumerator en = error.ProcessVarsAndValues.ProcessVarList.GetEnumerator();
         while (en.MoveNext())
         {
            ProcessVar pv = (ProcessVar)en.Current;
            object minMaxVals = error.ProcessVarsAndValues.GetVarRange(pv);
            MinMaxProcVarElementControl ctrl = new MinMaxProcVarElementControl(iNumericFormat, pv, minMaxVals);
            ctrl.Dock = DockStyle.Top;
            this.panelCenter.Controls.Add(ctrl);
            ctrl.BringToFront();
         }
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
         this.labelMessage = new System.Windows.Forms.Label();
         this.panelSouth = new System.Windows.Forms.Panel();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panelCenter = new System.Windows.Forms.Panel();
         this.procVarHeaderControl = new ProsimoUI.ProcVarsEditor.MinMaxProcVarHeaderControl();
         this.panelNorth.SuspendLayout();
         this.panelSouth.SuspendLayout();
         this.panelCenter.SuspendLayout();
         this.SuspendLayout();
         // 
         // panelNorth
         // 
         this.panelNorth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelNorth.Controls.Add(this.labelMessage);
         this.panelNorth.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelNorth.Location = new System.Drawing.Point(0, 0);
         this.panelNorth.Name = "panelNorth";
         this.panelNorth.Size = new System.Drawing.Size(558, 68);
         this.panelNorth.TabIndex = 0;
         // 
         // labelMessage
         // 
         this.labelMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelMessage.Location = new System.Drawing.Point(8, 8);
         this.labelMessage.Name = "labelMessage";
         this.labelMessage.Size = new System.Drawing.Size(540, 48);
         this.labelMessage.TabIndex = 0;
         // 
         // panelSouth
         // 
         this.panelSouth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelSouth.Controls.Add(this.buttonCancel);
         this.panelSouth.Controls.Add(this.buttonOk);
         this.panelSouth.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelSouth.Location = new System.Drawing.Point(0, 196);
         this.panelSouth.Name = "panelSouth";
         this.panelSouth.Size = new System.Drawing.Size(558, 32);
         this.panelSouth.TabIndex = 1;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(284, 4);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 1;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(196, 4);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.TabIndex = 0;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // panelCenter
         // 
         this.panelCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelCenter.Controls.Add(this.procVarHeaderControl);
         this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelCenter.Location = new System.Drawing.Point(0, 68);
         this.panelCenter.Name = "panelCenter";
         this.panelCenter.Size = new System.Drawing.Size(558, 128);
         this.panelCenter.TabIndex = 2;
         // 
         // procVarHeaderControl
         // 
         this.procVarHeaderControl.Dock = System.Windows.Forms.DockStyle.Top;
         this.procVarHeaderControl.Location = new System.Drawing.Point(0, 0);
         this.procVarHeaderControl.Name = "procVarHeaderControl";
         this.procVarHeaderControl.Size = new System.Drawing.Size(554, 20);
         this.procVarHeaderControl.TabIndex = 0;
         // 
         // ProcVarsMoreThenOneOutOfRangeForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(558, 228);
         this.Controls.Add(this.panelCenter);
         this.Controls.Add(this.panelSouth);
         this.Controls.Add(this.panelNorth);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProcVarsMoreThenOneOutOfRangeForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Process Variables";
         this.panelNorth.ResumeLayout(false);
         this.panelSouth.ResumeLayout(false);
         this.panelCenter.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         bool numericFormatOk = true;

         IEnumerator en = this.panelCenter.Controls.GetEnumerator();
         while (en.MoveNext())
         {
            Control ctrl = (Control)en.Current;
            if (ctrl is MinMaxProcVarElementControl)
            {
               MinMaxProcVarElementControl pveCtrl = ctrl as MinMaxProcVarElementControl;
               if (!pveCtrl.IsNewValueValid)
               {
                  numericFormatOk = false;
                  break;
               }
            }
         }

         if (numericFormatOk)
         {
            // build the hashtable with procVars and values
            Hashtable hashtable = new Hashtable();

            IEnumerator en2 = this.panelCenter.Controls.GetEnumerator();
            while (en2.MoveNext())
            {
               Control ctrl = (Control)en2.Current;
               if (ctrl is MinMaxProcVarElementControl)
               {
                  MinMaxProcVarElementControl pveCtrl = ctrl as MinMaxProcVarElementControl;
                  hashtable.Add(pveCtrl.Variable, pveCtrl.NewValue);
               }
            }

            ErrorMessage error = this.solvable.Specify(hashtable);
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

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }
   }
}
