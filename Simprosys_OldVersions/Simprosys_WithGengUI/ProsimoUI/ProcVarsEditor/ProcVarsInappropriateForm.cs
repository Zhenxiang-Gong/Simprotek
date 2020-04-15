using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo;

namespace ProsimoUI.ProcVarsEditor
{
	/// <summary>
	/// Summary description for ProcVarsInappropriateForm.
	/// </summary>
	public class ProcVarsInappropriateForm : System.Windows.Forms.Form
	{
      private ProcessVar initialProcVar;
      private object initialVarNewValue;
      private ProcessVarsAndValues varsAndValues;

      private System.Windows.Forms.Panel panelNorth;
      private System.Windows.Forms.Label labelMessage;
      private System.Windows.Forms.Panel panelSouth;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Panel panelCenter;
      private System.Windows.Forms.TextBox textBoxProcVarNewValue;
      private ProsimoUI.ProcessVarNonEditableTextBox textBoxProcVarExistingValue;
      private ProsimoUI.ProcessVarLabel labelProcVarName;
      private System.Windows.Forms.Label labelName;
      private System.Windows.Forms.GroupBox groupBoxInitialProcVar;
      private System.Windows.Forms.Label labelNewValue;
      private System.Windows.Forms.Label labelCurrentValue;
      private ProsimoUI.ProcVarsEditor.RecommProcVarHeaderControl procVarHeaderControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProcVarsInappropriateForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public ProcVarsInappropriateForm(INumericFormat iNumericFormat, ProcessVar var, object varNewValue, ErrorMessage error)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
   
         this.Text = error.Title;

         this.initialProcVar = var;
         this.initialVarNewValue = varNewValue;
         this.varsAndValues = error.ProcessVarsAndValues;

         // display the message
         this.labelMessage.Text = error.Message;

         // dispaly the initial variable
         this.labelProcVarName.InitializeVariable(var);
         this.labelProcVarName.BorderStyle = BorderStyle.None;
         this.textBoxProcVarExistingValue.InitializeVariable(iNumericFormat, var);

         // note: the new value is already converted to the current unit system,
         //       it needs only to be formated
         if (var is ProcessVarDouble)
         {
            double val = (double)varNewValue;
            if (val == Constants.NO_VALUE)
               this.textBoxProcVarNewValue.Text = "";
            else
               this.textBoxProcVarNewValue.Text = val.ToString(iNumericFormat.NumericFormatString);
         }
         else if (var is ProcessVarInt)
         {
            int val = (int)varNewValue;
            if (val == Constants.NO_VALUE_INT)
               this.textBoxProcVarNewValue.Text = "";
            else
               this.textBoxProcVarNewValue.Text = val.ToString(UI.DECIMAL);
         }
         this.textBoxProcVarExistingValue.BackColor = Color.Gainsboro;
         this.textBoxProcVarNewValue.BackColor = Color.Gainsboro;

         IEnumerator en = varsAndValues.ProcessVarList.GetEnumerator();
         while (en.MoveNext())
         {
            ProcessVar pv = (ProcessVar)en.Current;
            object recommVal = varsAndValues.GetRecommendedValue(pv);
            RecommProcVarElementControl ctrl = new RecommProcVarElementControl(iNumericFormat, pv, recommVal);
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
         this.groupBoxInitialProcVar = new System.Windows.Forms.GroupBox();
         this.labelNewValue = new System.Windows.Forms.Label();
         this.labelCurrentValue = new System.Windows.Forms.Label();
         this.labelName = new System.Windows.Forms.Label();
         this.labelProcVarName = new ProsimoUI.ProcessVarLabel();
         this.textBoxProcVarExistingValue = new ProsimoUI.ProcessVarNonEditableTextBox();
         this.textBoxProcVarNewValue = new System.Windows.Forms.TextBox();
         this.labelMessage = new System.Windows.Forms.Label();
         this.panelSouth = new System.Windows.Forms.Panel();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panelCenter = new System.Windows.Forms.Panel();
         this.procVarHeaderControl = new ProsimoUI.ProcVarsEditor.RecommProcVarHeaderControl();
         this.panelNorth.SuspendLayout();
         this.groupBoxInitialProcVar.SuspendLayout();
         this.panelSouth.SuspendLayout();
         this.panelCenter.SuspendLayout();
         this.SuspendLayout();
         // 
         // panelNorth
         // 
         this.panelNorth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelNorth.Controls.Add(this.groupBoxInitialProcVar);
         this.panelNorth.Controls.Add(this.labelMessage);
         this.panelNorth.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelNorth.Location = new System.Drawing.Point(0, 0);
         this.panelNorth.Name = "panelNorth";
         this.panelNorth.Size = new System.Drawing.Size(466, 172);
         this.panelNorth.TabIndex = 0;
         // 
         // groupBoxInitialProcVar
         // 
         this.groupBoxInitialProcVar.Controls.Add(this.labelNewValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelCurrentValue);
         this.groupBoxInitialProcVar.Controls.Add(this.labelName);
         this.groupBoxInitialProcVar.Controls.Add(this.labelProcVarName);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxProcVarExistingValue);
         this.groupBoxInitialProcVar.Controls.Add(this.textBoxProcVarNewValue);
         this.groupBoxInitialProcVar.Location = new System.Drawing.Point(8, 68);
         this.groupBoxInitialProcVar.Name = "groupBoxInitialProcVar";
         this.groupBoxInitialProcVar.Size = new System.Drawing.Size(448, 96);
         this.groupBoxInitialProcVar.TabIndex = 26;
         this.groupBoxInitialProcVar.TabStop = false;
         this.groupBoxInitialProcVar.Text = "Initial Variable Being Specified";
         // 
         // labelNewValue
         // 
         this.labelNewValue.Location = new System.Drawing.Point(12, 72);
         this.labelNewValue.Name = "labelNewValue";
         this.labelNewValue.Size = new System.Drawing.Size(120, 16);
         this.labelNewValue.TabIndex = 27;
         this.labelNewValue.Text = "Newly Specified Value:";
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
         // textBoxProcVarExistingValue
         // 
         this.textBoxProcVarExistingValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxProcVarExistingValue.Location = new System.Drawing.Point(136, 48);
         this.textBoxProcVarExistingValue.Name = "textBoxProcVarExistingValue";
         this.textBoxProcVarExistingValue.ReadOnly = true;
         this.textBoxProcVarExistingValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxProcVarExistingValue.TabIndex = 23;
         this.textBoxProcVarExistingValue.TabStop = false;
         this.textBoxProcVarExistingValue.Text = "";
         this.textBoxProcVarExistingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxProcVarNewValue
         // 
         this.textBoxProcVarNewValue.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxProcVarNewValue.Location = new System.Drawing.Point(136, 68);
         this.textBoxProcVarNewValue.Name = "textBoxProcVarNewValue";
         this.textBoxProcVarNewValue.ReadOnly = true;
         this.textBoxProcVarNewValue.Size = new System.Drawing.Size(80, 20);
         this.textBoxProcVarNewValue.TabIndex = 24;
         this.textBoxProcVarNewValue.TabStop = false;
         this.textBoxProcVarNewValue.Text = "";
         this.textBoxProcVarNewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
         this.panelSouth.Location = new System.Drawing.Point(0, 296);
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
         // panelCenter
         // 
         this.panelCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelCenter.Controls.Add(this.procVarHeaderControl);
         this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelCenter.Location = new System.Drawing.Point(0, 172);
         this.panelCenter.Name = "panelCenter";
         this.panelCenter.Size = new System.Drawing.Size(466, 124);
         this.panelCenter.TabIndex = 2;
         // 
         // procVarHeaderControl
         // 
         this.procVarHeaderControl.Dock = System.Windows.Forms.DockStyle.Top;
         this.procVarHeaderControl.Location = new System.Drawing.Point(0, 0);
         this.procVarHeaderControl.Name = "procVarHeaderControl";
         this.procVarHeaderControl.Size = new System.Drawing.Size(462, 20);
         this.procVarHeaderControl.TabIndex = 0;
         // 
         // ProcVarsInappropriateForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(466, 328);
         this.Controls.Add(this.panelCenter);
         this.Controls.Add(this.panelSouth);
         this.Controls.Add(this.panelNorth);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProcVarsInappropriateForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Process Variables";
         this.panelNorth.ResumeLayout(false);
         this.groupBoxInitialProcVar.ResumeLayout(false);
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
            if (ctrl is RecommProcVarElementControl)
            {
               RecommProcVarElementControl pveCtrl = ctrl as RecommProcVarElementControl;
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
            hashtable.Add(this.initialProcVar, this.initialVarNewValue);

            IEnumerator en2 = this.panelCenter.Controls.GetEnumerator();
            while (en2.MoveNext())
            {
               Control ctrl = (Control)en2.Current;
               if (ctrl is RecommProcVarElementControl)
               {
                  RecommProcVarElementControl pveCtrl = ctrl as RecommProcVarElementControl;
                  hashtable.Add(pveCtrl.Variable, pveCtrl.NewValue);
               }
            }

            ErrorMessage error = this.initialProcVar.Owner.Specify(hashtable);
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
