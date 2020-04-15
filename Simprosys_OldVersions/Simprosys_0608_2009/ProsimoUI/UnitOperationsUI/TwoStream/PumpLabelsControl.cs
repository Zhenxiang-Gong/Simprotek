using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for PumpLabelsControl.
	/// </summary>
	public class PumpLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 160;

      private ProcessVarLabel labelCapacity;
      private ProcessVarLabel labelStaticSuctionHead;
      private ProcessVarLabel labelSuctionFrictionHead;
      private ProcessVarLabel labelStaticDischargeHead;
      private ProcessVarLabel labelDischargeFrictionHead;
      private ProcessVarLabel labelTotalDynamicHead;
      private ProcessVarLabel labelEfficiency;
      private ProcessVarLabel labelPowerInput;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public PumpLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public PumpLabelsControl(Pump uo) : this()
		{
         this.InitializeVariableLabels(uo);
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
         this.labelCapacity = new ProsimoUI.ProcessVarLabel();
         this.labelDischargeFrictionHead = new ProsimoUI.ProcessVarLabel();
         this.labelEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelPowerInput = new ProsimoUI.ProcessVarLabel();
         this.labelStaticDischargeHead = new ProsimoUI.ProcessVarLabel();
         this.labelStaticSuctionHead = new ProsimoUI.ProcessVarLabel();
         this.labelSuctionFrictionHead = new ProsimoUI.ProcessVarLabel();
         this.labelTotalDynamicHead = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelCapacity
         // 
         this.labelCapacity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCapacity.Location = new System.Drawing.Point(0, 0);
         this.labelCapacity.Name = "labelCapacity";
         this.labelCapacity.Size = new System.Drawing.Size(192, 20);
         this.labelCapacity.TabIndex = 92;
         this.labelCapacity.Text = "Capacity";
         this.labelCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelDischargeFrictionHead
         // 
         this.labelDischargeFrictionHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelDischargeFrictionHead.Location = new System.Drawing.Point(0, 80);
         this.labelDischargeFrictionHead.Name = "labelDischargeFrictionHead";
         this.labelDischargeFrictionHead.Size = new System.Drawing.Size(192, 20);
         this.labelDischargeFrictionHead.TabIndex = 93;
         this.labelDischargeFrictionHead.Text = "DischargeFrictionHead";
         this.labelDischargeFrictionHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEfficiency
         // 
         this.labelEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEfficiency.Location = new System.Drawing.Point(0, 120);
         this.labelEfficiency.Name = "labelEfficiency";
         this.labelEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelEfficiency.TabIndex = 93;
         this.labelEfficiency.Text = "EFFICIENCY";
         this.labelEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPowerInput
         // 
         this.labelPowerInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPowerInput.Location = new System.Drawing.Point(0, 140);
         this.labelPowerInput.Name = "labelPowerInput";
         this.labelPowerInput.Size = new System.Drawing.Size(192, 20);
         this.labelPowerInput.TabIndex = 96;
         this.labelPowerInput.Text = "POWER_INPUT";
         this.labelPowerInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelStaticDischargeHead
         // 
         this.labelStaticDischargeHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelStaticDischargeHead.Location = new System.Drawing.Point(0, 60);
         this.labelStaticDischargeHead.Name = "labelStaticDischargeHead";
         this.labelStaticDischargeHead.Size = new System.Drawing.Size(192, 20);
         this.labelStaticDischargeHead.TabIndex = 92;
         this.labelStaticDischargeHead.Text = "StaticDischargeHead";
         this.labelStaticDischargeHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelStaticSuctionHead
         // 
         this.labelStaticSuctionHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelStaticSuctionHead.Location = new System.Drawing.Point(0, 20);
         this.labelStaticSuctionHead.Name = "labelStaticSuctionHead";
         this.labelStaticSuctionHead.Size = new System.Drawing.Size(192, 20);
         this.labelStaticSuctionHead.TabIndex = 94;
         this.labelStaticSuctionHead.Text = "StaticSuctionHead";
         this.labelStaticSuctionHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSuctionFrictionHead
         // 
         this.labelSuctionFrictionHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSuctionFrictionHead.Location = new System.Drawing.Point(0, 40);
         this.labelSuctionFrictionHead.Name = "labelSuctionFrictionHead";
         this.labelSuctionFrictionHead.Size = new System.Drawing.Size(192, 20);
         this.labelSuctionFrictionHead.TabIndex = 97;
         this.labelSuctionFrictionHead.Text = "SuctionFrictionHead";
         this.labelSuctionFrictionHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalDynamicHead
         // 
         this.labelTotalDynamicHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalDynamicHead.Location = new System.Drawing.Point(0, 100);
         this.labelTotalDynamicHead.Name = "labelTotalDynamicHead";
         this.labelTotalDynamicHead.Size = new System.Drawing.Size(192, 20);
         this.labelTotalDynamicHead.TabIndex = 92;
         this.labelTotalDynamicHead.Text = "TotalDynamicHead";
         this.labelTotalDynamicHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // PumpLabelsControl
         // 
         this.Controls.Add(this.labelCapacity);
         this.Controls.Add(this.labelDischargeFrictionHead);
         this.Controls.Add(this.labelEfficiency);
         this.Controls.Add(this.labelPowerInput);
         this.Controls.Add(this.labelStaticDischargeHead);
         this.Controls.Add(this.labelStaticSuctionHead);
         this.Controls.Add(this.labelSuctionFrictionHead);
         this.Controls.Add(this.labelTotalDynamicHead);
         this.Name = "PumpLabelsControl";
         this.Size = new System.Drawing.Size(192, 160);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Pump uo)
      {
         this.labelCapacity.InitializeVariable(uo.Capacity);
         this.labelDischargeFrictionHead.InitializeVariable(uo.DischargeFrictionHead);
         this.labelEfficiency.InitializeVariable(uo.Efficiency);
         this.labelPowerInput.InitializeVariable(uo.PowerInput);
         this.labelStaticDischargeHead.InitializeVariable(uo.StaticDischargeHead);
         this.labelStaticSuctionHead.InitializeVariable(uo.StaticSuctionHead);
         this.labelSuctionFrictionHead.InitializeVariable(uo.SuctionFrictionHead);
         this.labelTotalDynamicHead.InitializeVariable(uo.TotalDynamicHead);
      }
   }
}
