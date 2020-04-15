using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Materials;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for ReadOnlyDuhringLinesControl.
	/// </summary>
	public class ReadOnlyDuhringLinesControl : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.Label labelEnd;
      private System.Windows.Forms.Label labelStart;
      private ProsimoUI.ProcessVarLabel labelEndSolutionBP;
      private ProsimoUI.ProcessVarLabel labelEndSolventBP;
      private ProsimoUI.ProcessVarLabel labelStartSolutionBP;
      private ProsimoUI.ProcessVarLabel labelStartSolventBP;
      private ProsimoUI.ProcessVarLabel labelConcentration;
      private System.Windows.Forms.Panel panelHeader;
      private System.Windows.Forms.Panel panel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReadOnlyDuhringLinesControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
         this.labelEnd = new System.Windows.Forms.Label();
         this.labelStart = new System.Windows.Forms.Label();
         this.labelEndSolutionBP = new ProsimoUI.ProcessVarLabel();
         this.labelEndSolventBP = new ProsimoUI.ProcessVarLabel();
         this.labelStartSolutionBP = new ProsimoUI.ProcessVarLabel();
         this.labelStartSolventBP = new ProsimoUI.ProcessVarLabel();
         this.labelConcentration = new ProsimoUI.ProcessVarLabel();
         this.panelHeader = new System.Windows.Forms.Panel();
         this.panel = new System.Windows.Forms.Panel();
         this.panelHeader.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelEnd
         // 
         this.labelEnd.BackColor = System.Drawing.Color.DarkGray;
         this.labelEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEnd.Location = new System.Drawing.Point(270, 0);
         this.labelEnd.Name = "labelEnd";
         this.labelEnd.Size = new System.Drawing.Size(180, 20);
         this.labelEnd.TabIndex = 14;
         this.labelEnd.Text = "End Boiling Point";
         this.labelEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStart
         // 
         this.labelStart.BackColor = System.Drawing.Color.DarkGray;
         this.labelStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStart.Location = new System.Drawing.Point(90, 0);
         this.labelStart.Name = "labelStart";
         this.labelStart.Size = new System.Drawing.Size(180, 20);
         this.labelStart.TabIndex = 13;
         this.labelStart.Text = "Start Boiling Point";
         this.labelStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelEndSolutionBP
         // 
         this.labelEndSolutionBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelEndSolutionBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEndSolutionBP.Location = new System.Drawing.Point(360, 20);
         this.labelEndSolutionBP.Name = "labelEndSolutionBP";
         this.labelEndSolutionBP.Size = new System.Drawing.Size(90, 20);
         this.labelEndSolutionBP.TabIndex = 12;
         this.labelEndSolutionBP.Text = "Solution";
         this.labelEndSolutionBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelEndSolventBP
         // 
         this.labelEndSolventBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelEndSolventBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEndSolventBP.Location = new System.Drawing.Point(270, 20);
         this.labelEndSolventBP.Name = "labelEndSolventBP";
         this.labelEndSolventBP.Size = new System.Drawing.Size(90, 20);
         this.labelEndSolventBP.TabIndex = 11;
         this.labelEndSolventBP.Text = "Solvent";
         this.labelEndSolventBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStartSolutionBP
         // 
         this.labelStartSolutionBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelStartSolutionBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStartSolutionBP.Location = new System.Drawing.Point(180, 20);
         this.labelStartSolutionBP.Name = "labelStartSolutionBP";
         this.labelStartSolutionBP.Size = new System.Drawing.Size(90, 20);
         this.labelStartSolutionBP.TabIndex = 10;
         this.labelStartSolutionBP.Text = "Solution";
         this.labelStartSolutionBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStartSolventBP
         // 
         this.labelStartSolventBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelStartSolventBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStartSolventBP.Location = new System.Drawing.Point(90, 20);
         this.labelStartSolventBP.Name = "labelStartSolventBP";
         this.labelStartSolventBP.Size = new System.Drawing.Size(90, 20);
         this.labelStartSolventBP.TabIndex = 9;
         this.labelStartSolventBP.Text = "Solvent";
         this.labelStartSolventBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelConcentration
         // 
         this.labelConcentration.BackColor = System.Drawing.Color.DarkGray;
         this.labelConcentration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelConcentration.Location = new System.Drawing.Point(0, 0);
         this.labelConcentration.Name = "labelConcentration";
         this.labelConcentration.Size = new System.Drawing.Size(90, 40);
         this.labelConcentration.TabIndex = 8;
         this.labelConcentration.Text = "Concentration";
         this.labelConcentration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panelHeader
         // 
         this.panelHeader.Controls.Add(this.labelStartSolventBP);
         this.panelHeader.Controls.Add(this.labelEnd);
         this.panelHeader.Controls.Add(this.labelConcentration);
         this.panelHeader.Controls.Add(this.labelStart);
         this.panelHeader.Controls.Add(this.labelEndSolutionBP);
         this.panelHeader.Controls.Add(this.labelEndSolventBP);
         this.panelHeader.Controls.Add(this.labelStartSolutionBP);
         this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(452, 40);
         this.panelHeader.TabIndex = 15;
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 40);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(452, 132);
         this.panel.TabIndex = 16;
         // 
         // ReadOnlyDuhringLinesControl
         // 
         this.Controls.Add(this.panel);
         this.Controls.Add(this.panelHeader);
         this.Name = "ReadOnlyDuhringLinesControl";
         this.Size = new System.Drawing.Size(452, 172);
         this.panelHeader.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetDuhringLinesCache(DryingMaterialCache dryingMaterialCache, INumericFormat iNumericFormat)
      {
         this.SetUnits(UnitSystemService.GetInstance().CurrentUnitSystem);

         DuhringLinesCache duhringLinesCache = new DuhringLinesCache(dryingMaterialCache);
         this.UpdateTheUI(iNumericFormat, duhringLinesCache);
      }

      private void UpdateTheUI(INumericFormat iNumericFormat, DuhringLinesCache duhringLinesCache)
      {
         this.panel.Controls.Clear();
         IEnumerator e = duhringLinesCache.DuhringLines.GetEnumerator();
         int i = 0;
         while (e.MoveNext())
         {
            DuhringLine duhringLine = (DuhringLine)e.Current;
            DuhringLineControl ctrl = new DuhringLineControl(iNumericFormat, duhringLine, true);
            ctrl.Location = new Point(0, ctrl.Height*i++);
            this.panel.Controls.Add(ctrl);
         }
      }

      private void SetUnits(UnitSystem unitSystem)
      {
         string unitTemp = UnitSystemService.GetInstance().GetUnitAsString(PhysicalQuantity.Temperature);
         if (unitTemp != null && !unitTemp.Trim().Equals(""))
         {
            this.labelStart.Text = "Start Boiling Point" + " (" + unitTemp + ")";
            this.labelEnd.Text = "End Boiling Point" + " (" + unitTemp + ")";
         }
         else
         {
            this.labelStart.Text = "Start Boiling Point";
            this.labelEnd.Text = "End Boiling Point";
         }

         string unitMoistureCont = UnitSystemService.GetInstance().GetUnitAsString(PhysicalQuantity.MoistureContent);
         if (unitMoistureCont != null && !unitMoistureCont.Trim().Equals(""))
         {
            this.labelConcentration.Text = "Concentration" + " (" + unitMoistureCont + ")";
         }
         else
         {
            this.labelConcentration.Text = "Concentration";
         }
      }
	}
}
