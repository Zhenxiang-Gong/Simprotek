using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneTotalEfficiencyControl.
	/// </summary>
	public class CycloneTotalEfficiencyControl : System.Windows.Forms.UserControl
	{
      private ProsimoUI.ProcessVarTextBox textBoxEfficiency;
      private System.Windows.Forms.Label labelEfficiency;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneTotalEfficiencyControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public CycloneTotalEfficiencyControl(Flowsheet flowsheet, ParticleDistributionCache particleDistribution)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.InitializeTheUI(flowsheet, particleDistribution);
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
         this.textBoxEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.labelEfficiency = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // textBoxEfficiency
         // 
         this.textBoxEfficiency.BackColor = System.Drawing.Color.White;
         this.textBoxEfficiency.Location = new System.Drawing.Point(240, 0);
         this.textBoxEfficiency.Name = "textBoxEfficiency";
         this.textBoxEfficiency.ReadOnly = true;
         this.textBoxEfficiency.Size = new System.Drawing.Size(90, 20);
         this.textBoxEfficiency.TabIndex = 5;
         this.textBoxEfficiency.TabStop = false;
         this.textBoxEfficiency.Text = "";
         this.textBoxEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // labelEfficiency
         // 
         this.labelEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEfficiency.ForeColor = System.Drawing.SystemColors.ControlText;
         this.labelEfficiency.Location = new System.Drawing.Point(0, 0);
         this.labelEfficiency.Name = "labelEfficiency";
         this.labelEfficiency.Size = new System.Drawing.Size(240, 20);
         this.labelEfficiency.TabIndex = 6;
         this.labelEfficiency.Text = "Total Efficiency: ";
         this.labelEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // CycloneTotalEfficiencyControl
         // 
         this.Controls.Add(this.labelEfficiency);
         this.Controls.Add(this.textBoxEfficiency);
         this.Name = "CycloneTotalEfficiencyControl";
         this.Size = new System.Drawing.Size(332, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeTheUI(Flowsheet flowsheet, ParticleDistributionCache particleDistribution)
      {
         this.textBoxEfficiency.InitializeVariable(flowsheet.ApplicationPrefs, particleDistribution.TotalEfficiency);
      }
	}
}
