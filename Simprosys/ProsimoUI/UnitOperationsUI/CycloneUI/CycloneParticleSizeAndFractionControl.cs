using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneParticleSizeAndFractionControl.
	/// </summary>
	public class CycloneParticleSizeAndFractionControl : System.Windows.Forms.UserControl
	{
      private CycloneParticleDistributionControl particleDistributionControl;

      private ParticleSizeFractionAndEfficiency sizeAndFraction;
      public ParticleSizeFractionAndEfficiency SizeAndFraction
      {
         get {return sizeAndFraction;}
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
//               this.labelHook.ForeColor = SystemColors.HighlightText;
            }
            else
            {
               this.labelHook.BackColor = SystemColors.Control;
//               this.labelHook.ForeColor = SystemColors.ControlText;
            }
         }
      }

      private System.Windows.Forms.Label labelHook;
      private ProcessVarTextBox textBoxDiameter;
      private ProcessVarTextBox textBoxWeightFraction;
      private ProcessVarTextBox textBoxEfficiency;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneParticleSizeAndFractionControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public CycloneParticleSizeAndFractionControl(CycloneParticleDistributionControl pdCtrl,
         Flowsheet flowsheet, ParticleSizeFractionAndEfficiency sizeAndFraction)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.particleDistributionControl = pdCtrl;
         this.sizeAndFraction = sizeAndFraction;
         this.IsSelected = false;
         this.InitializeTheUI(flowsheet, this.sizeAndFraction);
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
         this.textBoxDiameter = new ProsimoUI.ProcessVarTextBox();
         this.textBoxWeightFraction = new ProsimoUI.ProcessVarTextBox();
         this.textBoxEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // labelHook
         // 
         this.labelHook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHook.ForeColor = System.Drawing.Color.DarkGray;
         this.labelHook.Location = new System.Drawing.Point(0, 0);
         this.labelHook.Name = "labelHook";
         this.labelHook.Size = new System.Drawing.Size(60, 20);
         this.labelHook.TabIndex = 0;
         this.labelHook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.labelHook.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelHook_MouseDown);
         // 
         // textBoxDiameter
         // 
         this.textBoxDiameter.Location = new System.Drawing.Point(60, 0);
         this.textBoxDiameter.Name = "textBoxDiameter";
         this.textBoxDiameter.Size = new System.Drawing.Size(90, 20);
         this.textBoxDiameter.TabIndex = 1;
         this.textBoxDiameter.Text = "";
         this.textBoxDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxDiameter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxWeightFraction
         // 
         this.textBoxWeightFraction.Location = new System.Drawing.Point(150, 0);
         this.textBoxWeightFraction.Name = "textBoxWeightFraction";
         this.textBoxWeightFraction.Size = new System.Drawing.Size(90, 20);
         this.textBoxWeightFraction.TabIndex = 2;
         this.textBoxWeightFraction.Text = "";
         this.textBoxWeightFraction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxWeightFraction.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // textBoxEfficiency
         // 
         this.textBoxEfficiency.Location = new System.Drawing.Point(240, 0);
         this.textBoxEfficiency.Name = "textBoxEfficiency";
         this.textBoxEfficiency.Size = new System.Drawing.Size(90, 20);
         this.textBoxEfficiency.TabIndex = 4;
         this.textBoxEfficiency.TabStop = false;
         this.textBoxEfficiency.Text = "";
         this.textBoxEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpEventHandler);
         // 
         // CycloneParticleSizeAndFractionControl
         // 
         this.Controls.Add(this.textBoxEfficiency);
         this.Controls.Add(this.textBoxWeightFraction);
         this.Controls.Add(this.textBoxDiameter);
         this.Controls.Add(this.labelHook);
         this.Name = "CycloneParticleSizeAndFractionControl";
         this.Size = new System.Drawing.Size(332, 20);
         this.ResumeLayout(false);

      }
		#endregion

      private void InitializeTheUI(Flowsheet flowsheet, ParticleSizeFractionAndEfficiency sizeAndFraction)
      {
         this.textBoxDiameter.InitializeVariable(flowsheet.ApplicationPrefs, sizeAndFraction.Diameter);
         this.textBoxWeightFraction.InitializeVariable(flowsheet.ApplicationPrefs, sizeAndFraction.WeightFraction);
         this.textBoxEfficiency.InitializeVariable(flowsheet.ApplicationPrefs, sizeAndFraction.Efficiency);
      }

      private void labelHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.ProcessClick(e);
      }

      private void ProcessClick(MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) 
            {
               this.particleDistributionControl.SetElementsSelection(this, ModifierKey.Shift);
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
               this.particleDistributionControl.SetElementsSelection(this, ModifierKey.Ctrl);
            }
            else
            {
               this.particleDistributionControl.SetElementsSelection(this, ModifierKey.None);
            }
         }
         else if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            this.particleDistributionControl.UnselectElements();
         }
      }

      private void KeyUpEventHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxDiameter);
         list.Add(this.textBoxWeightFraction);

         if (e.KeyCode == Keys.Enter)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
      }
   }
}
