using System;
using System.Collections;
using System.ComponentModel;
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
	/// Summary description for FanLabelsControl.
	/// </summary>
	public class FanLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 80;

      private ProcessVarLabel labelStaticPressure;
      private ProcessVarLabel labelTotalDischargePressure;
      private ProcessVarLabel labelEfficiency;
      private ProcessVarLabel labelPowerInput;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanLabelsControl(Fan uo) : this()
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
         this.labelPowerInput = new ProsimoUI.ProcessVarLabel();
         this.labelStaticPressure = new ProsimoUI.ProcessVarLabel();
         this.labelTotalDischargePressure = new ProsimoUI.ProcessVarLabel();
         this.labelEfficiency = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelPowerInput
         // 
         this.labelPowerInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPowerInput.Location = new System.Drawing.Point(0, 60);
         this.labelPowerInput.Name = "labelPowerInput";
         this.labelPowerInput.Size = new System.Drawing.Size(192, 20);
         this.labelPowerInput.TabIndex = 96;
         this.labelPowerInput.Text = "PowerInput";
         this.labelPowerInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelStaticPressure
         // 
         this.labelStaticPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelStaticPressure.Location = new System.Drawing.Point(0, 0);
         this.labelStaticPressure.Name = "labelStaticPressure";
         this.labelStaticPressure.Size = new System.Drawing.Size(192, 20);
         this.labelStaticPressure.TabIndex = 92;
         this.labelStaticPressure.Text = "StaticPressure";
         this.labelStaticPressure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelTotalDischargePressure
         // 
         this.labelTotalDischargePressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelTotalDischargePressure.Location = new System.Drawing.Point(0, 20);
         this.labelTotalDischargePressure.Name = "labelTotalDischargePressure";
         this.labelTotalDischargePressure.Size = new System.Drawing.Size(192, 20);
         this.labelTotalDischargePressure.TabIndex = 97;
         this.labelTotalDischargePressure.Text = "TotalPressure";
         this.labelTotalDischargePressure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelEfficiency
         // 
         this.labelEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelEfficiency.Location = new System.Drawing.Point(0, 40);
         this.labelEfficiency.Name = "labelEfficiency";
         this.labelEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelEfficiency.TabIndex = 93;
         this.labelEfficiency.Text = "Efficiency";
         this.labelEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FanLabelsControl
         // 
         this.Controls.Add(this.labelPowerInput);
         this.Controls.Add(this.labelStaticPressure);
         this.Controls.Add(this.labelTotalDischargePressure);
         this.Controls.Add(this.labelEfficiency);
         this.Name = "FanLabelsControl";
         this.Size = new System.Drawing.Size(192, 80);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Fan uo)
      {
         this.labelStaticPressure.InitializeVariable(uo.StaticPressure);
         this.labelEfficiency.InitializeVariable(uo.Efficiency);
         this.labelTotalDischargePressure.InitializeVariable(uo.TotalDischargePressure);
         this.labelPowerInput.InitializeVariable(uo.PowerInput);
      }
   }
}
