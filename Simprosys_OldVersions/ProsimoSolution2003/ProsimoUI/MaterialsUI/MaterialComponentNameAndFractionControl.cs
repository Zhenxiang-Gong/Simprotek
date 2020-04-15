using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Materials;
using ProsimoUI;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for MaterialComponentNameAndFractionControl.
	/// </summary>
	public class MaterialComponentNameAndFractionControl : System.Windows.Forms.UserControl
	{
      private MaterialComponentsControl dmComponentsControl;

      private MaterialComponent materialComponent;
      public MaterialComponent MaterialComponent
      {
         get {return materialComponent;}
      }

      private bool isSelected;
      public bool IsSelected
      {
         get {return isSelected;}
         set
         {
            isSelected = value;
            if (isSelected)
            {
               this.labelHook.BackColor = SystemColors.Highlight;
               this.labelName.BackColor = SystemColors.Highlight;
//               this.labelHook.ForeColor = SystemColors.HighlightText;
            }
            else
            {
               this.labelHook.BackColor = SystemColors.Control;
               this.labelName.BackColor = SystemColors.Control;
//               this.labelHook.ForeColor = SystemColors.ControlText;
            }
         }
      }

      private System.Windows.Forms.Label labelHook;
      private System.Windows.Forms.Label labelName;
      private ProsimoUI.ProcessVarTextBox textBoxFraction;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MaterialComponentNameAndFractionControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public MaterialComponentNameAndFractionControl(MaterialComponentsControl dmSubstancesCtrl, MaterialComponent matComponent)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.dmComponentsControl = dmSubstancesCtrl;
         this.materialComponent = matComponent;
         this.IsSelected = false;
         this.InitializeTheUI(this.materialComponent);
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
         this.labelHook = new System.Windows.Forms.Label();
         this.labelName = new System.Windows.Forms.Label();
         this.textBoxFraction = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // labelHook
         // 
         this.labelHook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHook.ForeColor = System.Drawing.Color.DarkGray;
         this.labelHook.Location = new System.Drawing.Point(0, 0);
         this.labelHook.Name = "labelHook";
         this.labelHook.Size = new System.Drawing.Size(60, 20);
         this.labelHook.TabIndex = 1;
         this.labelHook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.labelHook.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelHook_MouseDown);
         // 
         // labelName
         // 
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelName.ForeColor = System.Drawing.Color.DarkGray;
         this.labelName.Location = new System.Drawing.Point(60, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(108, 20);
         this.labelName.TabIndex = 2;
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.labelName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelName_MouseDown);
         // 
         // textBoxFraction
         // 
         this.textBoxFraction.Location = new System.Drawing.Point(168, 0);
         this.textBoxFraction.Name = "textBoxFraction";
         this.textBoxFraction.Size = new System.Drawing.Size(90, 20);
         this.textBoxFraction.TabIndex = 3;
         this.textBoxFraction.Text = "";
         this.textBoxFraction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxFraction.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFraction_KeyUp);
         // 
         // MaterialComponentNameAndFractionControl
         // 
         this.Controls.Add(this.textBoxFraction);
         this.Controls.Add(this.labelName);
         this.Controls.Add(this.labelHook);
         this.Name = "MaterialComponentNameAndFractionControl";
         this.Size = new System.Drawing.Size(260, 20);
         this.ResumeLayout(false);

      }
		#endregion

      private void InitializeTheUI(MaterialComponent materialComponent)
      {
         this.labelName.Text = materialComponent.Name;
         // TODO: from where to get the numeric format?
         MainNumericFormat iNumericFormat = new MainNumericFormat();
         this.textBoxFraction.InitializeVariable(iNumericFormat, materialComponent.MassFraction);
      }

      private void labelHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void labelName_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void ProcessClick(MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) 
            {
               this.dmComponentsControl.SetElementsSelection(this, ModifierKey.Shift);
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
               this.dmComponentsControl.SetElementsSelection(this, ModifierKey.Ctrl);
            }
            else
            {
               this.dmComponentsControl.SetElementsSelection(this, ModifierKey.None);
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.dmComponentsControl.UnselectElements();
         }
      }

      private void textBoxFraction_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ParentForm.ActiveControl = null;
            this.ActiveControl = this.textBoxFraction;
         }
      }
	}
}
