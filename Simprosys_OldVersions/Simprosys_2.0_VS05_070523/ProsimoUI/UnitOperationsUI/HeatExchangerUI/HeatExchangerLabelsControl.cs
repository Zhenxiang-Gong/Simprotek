using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HeatExchangerLabelsControl.
	/// </summary>
	public class HeatExchangerLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 60;

      private ProcessVarLabel labelTotalHeatTransfer;
      private ProcessVarLabel labelColdSidePressureDrop;
      private ProcessVarLabel labelHotSidePressureDrop;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HeatExchangerLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public HeatExchangerLabelsControl(HeatExchanger uo) : this()
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
         this.labelTotalHeatTransfer = new ProsimoUI.ProcessVarLabel();
         this.labelColdSidePressureDrop = new ProsimoUI.ProcessVarLabel();
         this.labelHotSidePressureDrop = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelTotalHeatTransfer
         // 
         this.labelTotalHeatTransfer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalHeatTransfer.Location = new System.Drawing.Point(0, 0);
         this.labelTotalHeatTransfer.Name = "labelTotalHeatTransfer";
         this.labelTotalHeatTransfer.Size = new System.Drawing.Size(192, 20);
         this.labelTotalHeatTransfer.TabIndex = 91;
         this.labelTotalHeatTransfer.Text = "TotalHeatTransfer";
         this.labelTotalHeatTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelColdSidePressureDrop
         // 
         this.labelColdSidePressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelColdSidePressureDrop.Location = new System.Drawing.Point(0, 20);
         this.labelColdSidePressureDrop.Name = "labelColdSidePressureDrop";
         this.labelColdSidePressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelColdSidePressureDrop.TabIndex = 90;
         this.labelColdSidePressureDrop.Text = "ColdSidePressureDrop";
         this.labelColdSidePressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHotSidePressureDrop
         // 
         this.labelHotSidePressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHotSidePressureDrop.Location = new System.Drawing.Point(0, 40);
         this.labelHotSidePressureDrop.Name = "labelHotSidePressureDrop";
         this.labelHotSidePressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelHotSidePressureDrop.TabIndex = 89;
         this.labelHotSidePressureDrop.Text = "HotSidePressureDrop";
         this.labelHotSidePressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HeatExchangerLabelsControl
         // 
         this.Controls.Add(this.labelTotalHeatTransfer);
         this.Controls.Add(this.labelColdSidePressureDrop);
         this.Controls.Add(this.labelHotSidePressureDrop);
         this.Name = "HeatExchangerLabelsControl";
         this.Size = new System.Drawing.Size(192, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(HeatExchanger uo)
      {
         this.labelTotalHeatTransfer.InitializeVariable(uo.TotalHeatTransfer);
         this.labelColdSidePressureDrop.InitializeVariable(uo.ColdSidePressureDrop);
         this.labelHotSidePressureDrop.InitializeVariable(uo.HotSidePressureDrop);
      }
   }
}
