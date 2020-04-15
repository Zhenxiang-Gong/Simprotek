using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneParticleDistributionControl.
	/// </summary>
	public class CycloneParticleDistributionControl : System.Windows.Forms.UserControl
	{
      private ParticleDistributionCache particleDistributionCache;
      public ParticleDistributionCache ParticleDistributionCache
      {
         get {return particleDistributionCache;}
      }

      private Flowsheet flowsheet;
      private int baseIndex;
      private CycloneTotalEfficiencyControl cycloneTotalEfficiencyControl;

      private System.Windows.Forms.Panel panelHeader;
      private System.Windows.Forms.Label labelHook;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Label labelDiameter;
      private System.Windows.Forms.Label labelWeightFraction;
      private System.Windows.Forms.Label labelEfficiency;
   
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneParticleDistributionControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.baseIndex = 0;
         this.panel.Click += new EventHandler(panel_Click);
		}

      /// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.particleDistributionCache != null)
            this.particleDistributionCache.ParticleDistributionChanged -= new ParticleDistributionChangedEventHandler(particleDistributionCache_ParticleDistributionChanged);

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
         this.labelDiameter = new System.Windows.Forms.Label();
         this.labelWeightFraction = new System.Windows.Forms.Label();
         this.panelHeader = new System.Windows.Forms.Panel();
         this.labelEfficiency = new System.Windows.Forms.Label();
         this.labelHook = new System.Windows.Forms.Label();
         this.panel = new System.Windows.Forms.Panel();
         this.panelHeader.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelDiameter
         // 
         this.labelDiameter.BackColor = System.Drawing.Color.DarkGray;
         this.labelDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelDiameter.Location = new System.Drawing.Point(60, 0);
         this.labelDiameter.Name = "labelDiameter";
         this.labelDiameter.Size = new System.Drawing.Size(90, 20);
         this.labelDiameter.TabIndex = 0;
         this.labelDiameter.Text = "Diameter (Dp)";
         this.labelDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelWeightFraction
         // 
         this.labelWeightFraction.BackColor = System.Drawing.Color.DarkGray;
         this.labelWeightFraction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelWeightFraction.Location = new System.Drawing.Point(150, 0);
         this.labelWeightFraction.Name = "labelWeightFraction";
         this.labelWeightFraction.Size = new System.Drawing.Size(90, 20);
         this.labelWeightFraction.TabIndex = 1;
         this.labelWeightFraction.Text = "Weight Fraction";
         this.labelWeightFraction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panelHeader
         // 
         this.panelHeader.Controls.Add(this.labelEfficiency);
         this.panelHeader.Controls.Add(this.labelHook);
         this.panelHeader.Controls.Add(this.labelWeightFraction);
         this.panelHeader.Controls.Add(this.labelDiameter);
         this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(332, 20);
         this.panelHeader.TabIndex = 3;
         // 
         // labelEfficiency
         // 
         this.labelEfficiency.BackColor = System.Drawing.Color.DarkGray;
         this.labelEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelEfficiency.Location = new System.Drawing.Point(240, 0);
         this.labelEfficiency.Name = "labelEfficiency";
         this.labelEfficiency.Size = new System.Drawing.Size(90, 20);
         this.labelEfficiency.TabIndex = 4;
         this.labelEfficiency.Text = "Efficiency";
         this.labelEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelHook
         // 
         this.labelHook.BackColor = System.Drawing.Color.DarkGray;
         this.labelHook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelHook.Location = new System.Drawing.Point(0, 0);
         this.labelHook.Name = "labelHook";
         this.labelHook.Size = new System.Drawing.Size(60, 20);
         this.labelHook.TabIndex = 2;
         this.labelHook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 20);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(332, 136);
         this.panel.TabIndex = 4;
         // 
         // CycloneParticleDistributionControl
         // 
         this.Controls.Add(this.panel);
         this.Controls.Add(this.panelHeader);
         this.Name = "CycloneParticleDistributionControl";
         this.Size = new System.Drawing.Size(332, 156);
         this.panelHeader.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetParticleDistribution(CycloneControl cycloneCtrl)
      {
         this.flowsheet = cycloneCtrl.Flowsheet;
         this.particleDistributionCache = cycloneCtrl.Cyclone.CurrentRatingModel.GetParticleDistributionCache();
         this.cycloneTotalEfficiencyControl = new CycloneTotalEfficiencyControl(this.flowsheet, this.particleDistributionCache);
         this.UpdateTheUI(cycloneCtrl.Flowsheet, particleDistributionCache);
         this.particleDistributionCache.ParticleDistributionChanged += new ParticleDistributionChangedEventHandler(particleDistributionCache_ParticleDistributionChanged);
      }

      public void UpdateTheUI(Flowsheet flowsheet, ParticleDistributionCache particleDistributionModel)
      {
         this.panel.Controls.Clear();
         IEnumerator e = particleDistributionCache.SizeFractionAndEfficiencyList.GetEnumerator();
         int i = 0;
         while (e.MoveNext())
         {
            ParticleSizeFractionAndEfficiency psf = (ParticleSizeFractionAndEfficiency)e.Current;
            CycloneParticleSizeAndFractionControl ctrl = new CycloneParticleSizeAndFractionControl(this, flowsheet, psf);
            ctrl.Location = new Point(0, ctrl.Height*i++);
            this.panel.Controls.Add(ctrl);
         }
         this.cycloneTotalEfficiencyControl.InitializeTheUI(flowsheet, particleDistributionCache);
         this.cycloneTotalEfficiencyControl.Location = new Point(0, this.cycloneTotalEfficiencyControl.Height*i);
         this.panel.Controls.Add(this.cycloneTotalEfficiencyControl);
      }

      private void particleDistributionCache_ParticleDistributionChanged(ParticleDistributionCache particleDistributionModel)
      {
         this.UpdateTheUI(this.flowsheet, particleDistributionModel);
      }

      private ArrayList GetSelectedIndexes()
      {
         ArrayList list = new ArrayList();
         for (int i=this.panel.Controls.Count-2; i>=0; i--)
         {
            if ((this.panel.Controls[i] as CycloneParticleSizeAndFractionControl).IsSelected)
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
            CycloneParticleSizeAndFractionControl element = this.GetElement((int)e.Current);
            this.particleDistributionCache.RemoveParticleSizeFractionAndEfficiency(element.SizeAndFraction);
         }
      }

      private CycloneParticleSizeAndFractionControl GetElement(int idx)
      {
         return this.panel.Controls[idx] as CycloneParticleSizeAndFractionControl;
      }

      public void UnselectElements()
      {
         IEnumerator e = this.panel.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is CycloneParticleSizeAndFractionControl)
            {
               ((CycloneParticleSizeAndFractionControl)e.Current).IsSelected = false;
            }
         }
      }

      public void SetElementsSelection(CycloneParticleSizeAndFractionControl elementCtrl, ModifierKey modKey)
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

      private int GetIndexOfElement(CycloneParticleSizeAndFractionControl elementCtrl)
      {
         int index = -1;
         for (int i=0; i<this.panel.Controls.Count-1; i++)
         {
            if ((this.panel.Controls[i] as CycloneParticleSizeAndFractionControl).Equals(elementCtrl))
            {
               index = i;
               break;
            }
         }
         return index;
      }

      private void SetElementsSelection(int minIdx, int maxIdx)
      {
         CycloneParticleSizeAndFractionControl element = null;
         for (int i=0; i<this.panel.Controls.Count; i++)
         {
            if (i >= minIdx && i <= maxIdx)
            {
               element = this.panel.Controls[i] as CycloneParticleSizeAndFractionControl;
               element.IsSelected = true;
            }
         }
      }

      private void panel_Click(object sender, EventArgs e)
      {
         this.UnselectElements();
      }
   }
}
