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

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for HeaterLabelsControl.
	/// </summary>
	public class HeaterLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 60;

      private ProcessVarLabel labelHeatInput;
      private ProcessVarLabel labelHeatLoss;
      private ProcessVarLabel labelPressureDrop;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public HeaterLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

      public HeaterLabelsControl(Heater uo) : this()
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
         this.labelHeatInput = new ProsimoUI.ProcessVarLabel();
         this.labelHeatLoss = new ProsimoUI.ProcessVarLabel();
         this.labelPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelHeatInput
         // 
         this.labelHeatInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeatInput.Location = new System.Drawing.Point(0, 40);
         this.labelHeatInput.Name = "labelHeatInput";
         this.labelHeatInput.Size = new System.Drawing.Size(192, 20);
         this.labelHeatInput.TabIndex = 116;
         this.labelHeatInput.Text = "HeatInput";
         this.labelHeatInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHeatLoss
         // 
         this.labelHeatLoss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeatLoss.Location = new System.Drawing.Point(0, 20);
         this.labelHeatLoss.Name = "labelHeatLoss";
         this.labelHeatLoss.Size = new System.Drawing.Size(192, 20);
         this.labelHeatLoss.TabIndex = 117;
         this.labelHeatLoss.Text = "HeatLoss";
         this.labelHeatLoss.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPressureDrop
         // 
         this.labelPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelPressureDrop.Name = "labelPressureDrop";
         this.labelPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelPressureDrop.TabIndex = 118;
         this.labelPressureDrop.Text = "PressureDrop";
         this.labelPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HeaterLabelsControl
         // 
         this.Controls.Add(this.labelPressureDrop);
         this.Controls.Add(this.labelHeatLoss);
         this.Controls.Add(this.labelHeatInput);
         this.Name = "HeaterLabelsControl";
         this.Size = new System.Drawing.Size(192, 60);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Heater uo)
      {
         this.labelPressureDrop.InitializeVariable(uo.PressureDrop);
         this.labelHeatLoss.InitializeVariable(uo.HeatLoss);
         this.labelHeatInput.InitializeVariable(uo.HeatInput);
      }
   }
}
