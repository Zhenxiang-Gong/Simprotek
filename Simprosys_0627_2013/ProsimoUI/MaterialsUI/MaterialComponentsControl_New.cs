using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for MaterialComponentsControl.
	/// </summary>
	public class ComponentsControl : System.Windows.Forms.UserControl
	{
      private INumericFormat iNumericFormat;

      private MaterialCache materialCache;
      public MaterialCache MaterialCache
      {
         get {return materialCache;}
      }

      private int baseIndex;

      private System.Windows.Forms.Panel panelHeader;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Label labelMassFraction;
      private System.Windows.Forms.Label labelSubstanceName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public ComponentsControl()
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
         if (this.materialCache != null)
            this.materialCache.MaterialComponentsChanged -= new MaterialComponentsChangedEventHandler(materialCache_MaterialComponentsChanged);

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
         this.panelHeader = new System.Windows.Forms.Panel();
         this.labelMassFraction = new System.Windows.Forms.Label();
         this.labelSubstanceName = new System.Windows.Forms.Label();
         this.panel = new System.Windows.Forms.Panel();
         this.panelHeader.SuspendLayout();
         this.SuspendLayout();
         // 
         // panelHeader
         // 
         this.panelHeader.Controls.Add(this.labelMassFraction);
         this.panelHeader.Controls.Add(this.labelSubstanceName);
         this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelHeader.Location = new System.Drawing.Point(0, 0);
         this.panelHeader.Name = "panelHeader";
         this.panelHeader.Size = new System.Drawing.Size(258, 20);
         this.panelHeader.TabIndex = 4;
         // 
         // labelMassFraction
         // 
         this.labelMassFraction.BackColor = System.Drawing.Color.DarkGray;
         this.labelMassFraction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelMassFraction.Location = new System.Drawing.Point(164, 0);
         this.labelMassFraction.Name = "labelMassFraction";
         this.labelMassFraction.Size = new System.Drawing.Size(94, 20);
         this.labelMassFraction.TabIndex = 1;
         this.labelMassFraction.Text = "Mass Fraction";
         this.labelMassFraction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // labelSubstanceName
         // 
         this.labelSubstanceName.BackColor = System.Drawing.Color.DarkGray;
         this.labelSubstanceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelSubstanceName.Location = new System.Drawing.Point(0, 0);
         this.labelSubstanceName.Name = "labelSubstanceName";
         this.labelSubstanceName.Size = new System.Drawing.Size(164, 20);
         this.labelSubstanceName.TabIndex = 0;
         this.labelSubstanceName.Text = "Substance Name";
         this.labelSubstanceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // panel
         // 
         this.panel.AutoScroll = true;
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 20);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(258, 348);
         this.panel.TabIndex = 5;
         // 
         // ComponentsControl
         // 
         this.Controls.Add(this.panel);
         this.Controls.Add(this.panelHeader);
         this.Name = "ComponentsControl";
         this.Size = new System.Drawing.Size(258, 368);
         this.panelHeader.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetMaterialComponents(MaterialCache materialCache, INumericFormat iNumericFormat)
      {
         this.materialCache = materialCache;
         this.iNumericFormat = iNumericFormat;
         this.UpdateTheUI(this.materialCache.MaterialComponentList, iNumericFormat);
         this.materialCache.MaterialComponentsChanged += new MaterialComponentsChangedEventHandler(materialCache_MaterialComponentsChanged);
      }

      public void UpdateTheUI(ArrayList materialComponentList, INumericFormat iNumericFormat)
      {
         this.panel.Controls.Clear();
         IEnumerator e = materialComponentList.GetEnumerator();
         int i = 0;
         while (e.MoveNext())
         {
            MaterialComponent mc = (MaterialComponent)e.Current;
            MaterialComponentNameAndFractionControl ctrl = new MaterialComponentNameAndFractionControl(this, mc, iNumericFormat);
            ctrl.Location = new Point(0, ctrl.Height*i++);
            this.panel.Controls.Add(ctrl);
         }
      }

      private ArrayList GetSelectedIndexes()
      {
         ArrayList list = new ArrayList();
         for (int i=this.panel.Controls.Count-1; i>=0; i--)
         {
            if ((this.panel.Controls[i] as MaterialComponentNameAndFractionControl).IsSelected)
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
            MaterialComponentNameAndFractionControl element = this.GetElement((int)e.Current);
            this.materialCache.RemoveMaterialComponent(element.MaterialComponent);
         }
      }

      private MaterialComponentNameAndFractionControl GetElement(int idx)
      {
         return this.panel.Controls[idx] as MaterialComponentNameAndFractionControl;
      }

      public void UnselectElements()
      {
         IEnumerator e = this.panel.Controls.GetEnumerator();
         while (e.MoveNext()) 
         {
            if (e.Current is MaterialComponentNameAndFractionControl)
            {
               ((MaterialComponentNameAndFractionControl)e.Current).IsSelected = false;
            }
         }
      }

      public void SetElementsSelection(MaterialComponentNameAndFractionControl elementCtrl, ModifierKey modKey)
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

      private int GetIndexOfElement(MaterialComponentNameAndFractionControl elementCtrl)
      {
         int index = -1;
         for (int i=0; i<this.panel.Controls.Count-1; i++)
         {
            if ((this.panel.Controls[i] as MaterialComponentNameAndFractionControl).Equals(elementCtrl))
            {
               index = i;
               break;
            }
         }
         return index;
      }

      private void SetElementsSelection(int minIdx, int maxIdx)
      {
         MaterialComponentNameAndFractionControl element = null;
         for (int i=0; i<this.panel.Controls.Count; i++)
         {
            if (i >= minIdx && i <= maxIdx)
            {
               element = this.panel.Controls[i] as MaterialComponentNameAndFractionControl;
               element.IsSelected = true;
            }
         }
      }

      private void panel_Click(object sender, EventArgs e)
      {
         this.UnselectElements();
      }

      private void materialCache_MaterialComponentsChanged(ArrayList materialComponentList)
      {
         if (this.iNumericFormat != null)
            this.UpdateTheUI(materialComponentList, this.iNumericFormat);
      }
   }
}
