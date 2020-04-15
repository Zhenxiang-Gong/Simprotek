using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI;
using Prosimo.Materials;
using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DuhringLinesControl.
	/// </summary>
	public class DuhringLinesControl : System.Windows.Forms.UserControl
	{
      private DuhringLinesCache duhringLinesCache;
      public DuhringLinesCache DuhringLinesCache
      {
         get {return duhringLinesCache;}
      }

      private int baseIndex;
      private INumericFormat iNumericFormat;

      private System.Windows.Forms.Panel panelHeader;
      private System.Windows.Forms.Label labelHook;
      private System.Windows.Forms.Panel panel;
      private ProsimoUI.ProcessVarLabel labelConcentration;
      private ProsimoUI.ProcessVarLabel labelStartSolventBP;
      private ProsimoUI.ProcessVarLabel labelEndSolventBP;
      private ProsimoUI.ProcessVarLabel labelStartSolutionBP;
      private ProsimoUI.ProcessVarLabel labelEndSolutionBP;
      private System.Windows.Forms.Label labelStart;
      private System.Windows.Forms.Label labelEnd;
   
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public DuhringLinesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      /// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         UnitSystemService.GetInstance().CurrentUnitSystemChanged -=new CurrentUnitSystemChangedEventHandler(DuhringLinesControl_CurrentUnitSystemChanged);
         if (this.duhringLinesCache != null)
            this.duhringLinesCache.DuhringLinesChanged -= new DuhringLinesChangedEventHandler(duhringLinesCache_DuhringLinesChanged);

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
         this.labelConcentration = new ProsimoUI.ProcessVarLabel();
         this.labelStartSolventBP = new ProsimoUI.ProcessVarLabel();
         this.panelHeader = new System.Windows.Forms.Panel();
         this.labelEnd = new System.Windows.Forms.Label();
         this.labelStart = new System.Windows.Forms.Label();
         this.labelEndSolutionBP = new ProsimoUI.ProcessVarLabel();
         this.labelEndSolventBP = new ProsimoUI.ProcessVarLabel();
         this.labelStartSolutionBP = new ProsimoUI.ProcessVarLabel();
         this.labelHook = new System.Windows.Forms.Label();
         this.panel = new System.Windows.Forms.Panel();
         this.panelHeader.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelConcentration
         // 
         this.labelConcentration.BackColor = System.Drawing.Color.DarkGray;
         this.labelConcentration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelConcentration.Location = new System.Drawing.Point(60, 0);
         this.labelConcentration.Name = "labelConcentration";
         this.labelConcentration.Size = new System.Drawing.Size(90, 40);
         this.labelConcentration.TabIndex = 0;
         this.labelConcentration.Text = "Concentration";
         this.labelConcentration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStartSolventBP
         // 
         this.labelStartSolventBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelStartSolventBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStartSolventBP.Location = new System.Drawing.Point(150, 20);
         this.labelStartSolventBP.Name = "labelStartSolventBP";
         this.labelStartSolventBP.Size = new System.Drawing.Size(90, 20);
         this.labelStartSolventBP.TabIndex = 1;
         this.labelStartSolventBP.Text = "Solvent";
         this.labelStartSolventBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panelHeader
         // 
         this.panelHeader.Controls.Add(this.labelEnd);
         this.panelHeader.Controls.Add(this.labelStart);
         this.panelHeader.Controls.Add(this.labelEndSolutionBP);
         this.panelHeader.Controls.Add(this.labelEndSolventBP);
         this.panelHeader.Controls.Add(this.labelStartSolutionBP);
         this.panelHeader.Controls.Add(this.labelHook);
         this.panelHeader.Controls.Add(this.labelStartSolventBP);
         this.panelHeader.Controls.Add(this.labelConcentration);
         this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(510, 40);
         this.panelHeader.TabIndex = 3;
         // 
         // labelEnd
         // 
         this.labelEnd.BackColor = System.Drawing.Color.DarkGray;
         this.labelEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEnd.Location = new System.Drawing.Point(330, 0);
         this.labelEnd.Name = "labelEnd";
         this.labelEnd.Size = new System.Drawing.Size(180, 20);
         this.labelEnd.TabIndex = 7;
         this.labelEnd.Text = "End Boiling Point";
         this.labelEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStart
         // 
         this.labelStart.BackColor = System.Drawing.Color.DarkGray;
         this.labelStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStart.Location = new System.Drawing.Point(150, 0);
         this.labelStart.Name = "labelStart";
         this.labelStart.Size = new System.Drawing.Size(180, 20);
         this.labelStart.TabIndex = 6;
         this.labelStart.Text = "Start Boiling Point";
         this.labelStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelEndSolutionBP
         // 
         this.labelEndSolutionBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelEndSolutionBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEndSolutionBP.Location = new System.Drawing.Point(420, 20);
         this.labelEndSolutionBP.Name = "labelEndSolutionBP";
         this.labelEndSolutionBP.Size = new System.Drawing.Size(90, 20);
         this.labelEndSolutionBP.TabIndex = 5;
         this.labelEndSolutionBP.Text = "Solution";
         this.labelEndSolutionBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelEndSolventBP
         // 
         this.labelEndSolventBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelEndSolventBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEndSolventBP.Location = new System.Drawing.Point(330, 20);
         this.labelEndSolventBP.Name = "labelEndSolventBP";
         this.labelEndSolventBP.Size = new System.Drawing.Size(90, 20);
         this.labelEndSolventBP.TabIndex = 4;
         this.labelEndSolventBP.Text = "Solvent";
         this.labelEndSolventBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelStartSolutionBP
         // 
         this.labelStartSolutionBP.BackColor = System.Drawing.Color.DarkGray;
         this.labelStartSolutionBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelStartSolutionBP.Location = new System.Drawing.Point(240, 20);
         this.labelStartSolutionBP.Name = "labelStartSolutionBP";
         this.labelStartSolutionBP.Size = new System.Drawing.Size(90, 20);
         this.labelStartSolutionBP.TabIndex = 3;
         this.labelStartSolutionBP.Text = "Solution";
         this.labelStartSolutionBP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelHook
         // 
         this.labelHook.BackColor = System.Drawing.Color.DarkGray;
         this.labelHook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelHook.Location = new System.Drawing.Point(0, 0);
         this.labelHook.Name = "labelHook";
         this.labelHook.Size = new System.Drawing.Size(60, 40);
         this.labelHook.TabIndex = 2;
         this.labelHook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 40);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(510, 232);
         this.panel.TabIndex = 4;
         // 
         // DuhringLinesControl
         // 
         this.Controls.Add(this.panel);
         this.Controls.Add(this.panelHeader);
         this.Name = "DuhringLinesControl";
         this.Size = new System.Drawing.Size(510, 272);
         this.panelHeader.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetDuhringLinesCache(DryingMaterialCache dryingMaterialCache, INumericFormat iNumericFormat)
      {
         this.iNumericFormat = iNumericFormat;

         this.baseIndex = 0;
         this.panel.Click += new EventHandler(panel_Click);

         this.SetUnits(UnitSystemService.GetInstance().CurrentUnitSystem);
         UnitSystemService.GetInstance().CurrentUnitSystemChanged +=new CurrentUnitSystemChangedEventHandler(DuhringLinesControl_CurrentUnitSystemChanged);

         this.duhringLinesCache = new DuhringLinesCache(dryingMaterialCache);
         this.UpdateTheUI(iNumericFormat, this.duhringLinesCache);
         this.duhringLinesCache.DuhringLinesChanged += new DuhringLinesChangedEventHandler(duhringLinesCache_DuhringLinesChanged);
      }

      public void UpdateTheUI(INumericFormat iNumericFormat, DuhringLinesCache duhringLinesCache)
      {
         this.panel.Controls.Clear();
         IEnumerator e = duhringLinesCache.DuhringLines.GetEnumerator();
         int i = 0;
         while (e.MoveNext())
         {
            DuhringLine duhringLine = (DuhringLine)e.Current;
            DuhringLineControl ctrl = new DuhringLineControl(this, iNumericFormat, duhringLine, false);
            ctrl.Location = new Point(0, ctrl.Height*i++);
            this.panel.Controls.Add(ctrl);
         }
      }

      private ArrayList GetSelectedIndexes()
      {
         ArrayList list = new ArrayList();
         for (int i=this.panel.Controls.Count-1; i>=0; i--)
         {
            if ((this.panel.Controls[i] as DuhringLineControl).IsSelected)
            {
               list.Add(i);
            }
         }
         // the list should be sorted in descending order
         return list;
      }

      public void DeleteSelectedElements()
      {
         IEnumerator e = this.GetSelectedIndexes().GetEnumerator();
         while (e.MoveNext()) 
         { 
            DuhringLineControl element = this.GetElement((int)e.Current);
            this.DuhringLinesCache.RemoveDuhringLine(element.DuhringLine);
         }
      }

      private DuhringLineControl GetElement(int idx)
      {
         return this.panel.Controls[idx] as DuhringLineControl;
      }

      public void UnselectElements()
      {
         IEnumerator e = this.panel.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is DuhringLineControl)
            {
               ((DuhringLineControl)e.Current).IsSelected = false;
            }
         }
      }

      public void SetElementsSelection(DuhringLineControl elementCtrl, ModifierKey modKey)
      {
         if (modKey == ModifierKey.Ctrl)
         {
            elementCtrl.IsSelected = !elementCtrl.IsSelected;
            this.baseIndex = this.GetIndexOfElement(elementCtrl);
         }
         else if (modKey == ModifierKey.Shift)
         {
            int currentIdx = this.GetIndexOfElement(elementCtrl);
            if (currentIdx >= 0 && this.baseIndex >= 0)
            {
               this.UnselectElements();
               int minIdx = System.Math.Min(currentIdx, this.baseIndex);
               int maxIdx = System.Math.Max(currentIdx, this.baseIndex);
               this.SetElementsSelection(minIdx, maxIdx);
            }
         }
         else
         {
            this.UnselectElements();
            elementCtrl.IsSelected = true;
            this.baseIndex = this.GetIndexOfElement(elementCtrl);
         }
      }

      private int GetIndexOfElement(DuhringLineControl elementCtrl)
      {
         int index = -1;
         for (int i=0; i<this.panel.Controls.Count-1; i++)
         {
            if ((this.panel.Controls[i] as DuhringLineControl).Equals(elementCtrl))
            {
               index = i;
               break;
            }
         }
         return index;
      }

      private void SetElementsSelection(int minIdx, int maxIdx)
      {
         DuhringLineControl element = null;
         for (int i=0; i<this.panel.Controls.Count; i++)
         {
            if (i >= minIdx && i <= maxIdx)
            {
               element = this.panel.Controls[i] as DuhringLineControl;
               element.IsSelected = true;
            }
         }
      }

      private void panel_Click(object sender, EventArgs e)
      {
         this.UnselectElements();
      }

      private void duhringLinesCache_DuhringLinesChanged(DuhringLinesCache duhringLinesCache)
      {
         this.UpdateTheUI(this.iNumericFormat, duhringLinesCache);
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

      private void DuhringLinesControl_CurrentUnitSystemChanged(UnitSystem unitSystem)
      {
         this.SetUnits(unitSystem);
      }
   }
}
