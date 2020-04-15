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
	/// Summary description for CompressorLabelsControl.
	/// </summary>
	public class CompressorLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 140;

      private ProcessVarLabel labelPressureRatio;
      private ProcessVarLabel labelAdiabaticExponent;
      private ProcessVarLabel labelAdiabaticEfficiency;
      private ProcessVarLabel labelPolytropicExponent;
      private ProcessVarLabel labelPolytropicEfficiency;
      private ProcessVarLabel labelPowerInput;
      private System.Windows.Forms.Label labelCompressionProcess;
      
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CompressorLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public CompressorLabelsControl(Compressor uo) : this()
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
         this.labelPressureRatio = new ProsimoUI.ProcessVarLabel();
         this.labelAdiabaticExponent = new ProsimoUI.ProcessVarLabel();
         this.labelAdiabaticEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelPolytropicExponent = new ProsimoUI.ProcessVarLabel();
         this.labelPolytropicEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelPowerInput = new ProsimoUI.ProcessVarLabel();
         this.labelCompressionProcess = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelPressureRatio
         // 
         this.labelPressureRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureRatio.Location = new System.Drawing.Point(0, 0);
         this.labelPressureRatio.Name = "labelPressureRatio";
         this.labelPressureRatio.Size = new System.Drawing.Size(192, 20);
         this.labelPressureRatio.TabIndex = 96;
         this.labelPressureRatio.Text = "PressureRatio";
         this.labelPressureRatio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelAdiabaticExponent
         // 
         this.labelAdiabaticExponent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelAdiabaticExponent.Location = new System.Drawing.Point(0, 20);
         this.labelAdiabaticExponent.Name = "labelAdiabaticExponent";
         this.labelAdiabaticExponent.Size = new System.Drawing.Size(192, 20);
         this.labelAdiabaticExponent.TabIndex = 92;
         this.labelAdiabaticExponent.Text = "AdiabaticExponent";
         this.labelAdiabaticExponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelAdiabaticEfficiency
         // 
         this.labelAdiabaticEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelAdiabaticEfficiency.Location = new System.Drawing.Point(0, 40);
         this.labelAdiabaticEfficiency.Name = "labelAdiabaticEfficiency";
         this.labelAdiabaticEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelAdiabaticEfficiency.TabIndex = 94;
         this.labelAdiabaticEfficiency.Text = "AdiabaticEfficiency";
         this.labelAdiabaticEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPolytropicExponent
         // 
         this.labelPolytropicExponent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPolytropicExponent.Location = new System.Drawing.Point(0, 60);
         this.labelPolytropicExponent.Name = "labelPolytropicExponent";
         this.labelPolytropicExponent.Size = new System.Drawing.Size(192, 20);
         this.labelPolytropicExponent.TabIndex = 97;
         this.labelPolytropicExponent.Text = "PolytropicExponent";
         this.labelPolytropicExponent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPolytropicEfficiency
         // 
         this.labelPolytropicEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPolytropicEfficiency.Location = new System.Drawing.Point(0, 80);
         this.labelPolytropicEfficiency.Name = "labelPolytropicEfficiency";
         this.labelPolytropicEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelPolytropicEfficiency.TabIndex = 93;
         this.labelPolytropicEfficiency.Text = "PolytropicEfficiency";
         this.labelPolytropicEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelPowerInput
         // 
         this.labelPowerInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPowerInput.Location = new System.Drawing.Point(0, 100);
         this.labelPowerInput.Name = "labelPowerInput";
         this.labelPowerInput.Size = new System.Drawing.Size(192, 20);
         this.labelPowerInput.TabIndex = 93;
         this.labelPowerInput.Text = "PowerInput";
         this.labelPowerInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelCompressionProcess
         // 
         this.labelCompressionProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelCompressionProcess.Location = new System.Drawing.Point(0, 120);
         this.labelCompressionProcess.Name = "labelCompressionProcess";
         this.labelCompressionProcess.Size = new System.Drawing.Size(192, 20);
         this.labelCompressionProcess.TabIndex = 98;
         this.labelCompressionProcess.Text = "Compression Process:";
         this.labelCompressionProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // CompressorLabelsControl
         // 
         this.Controls.Add(this.labelCompressionProcess);
         this.Controls.Add(this.labelPressureRatio);
         this.Controls.Add(this.labelAdiabaticExponent);
         this.Controls.Add(this.labelAdiabaticEfficiency);
         this.Controls.Add(this.labelPolytropicExponent);
         this.Controls.Add(this.labelPolytropicEfficiency);
         this.Controls.Add(this.labelPowerInput);
         this.Name = "CompressorLabelsControl";
         this.Size = new System.Drawing.Size(192, 140);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Compressor uo)
      {
         this.labelPressureRatio.InitializeVariable(uo.OutletInletPressureRatio);
         this.labelAdiabaticExponent.InitializeVariable(uo.AdiabaticExponent);
         this.labelAdiabaticEfficiency.InitializeVariable(uo.AdiabaticEfficiency);
         this.labelPolytropicExponent.InitializeVariable(uo.PolytropicExponent);
         this.labelPolytropicEfficiency.InitializeVariable(uo.PolytropicEfficiency);
         this.labelPowerInput.InitializeVariable(uo.PowerInput);
      }
   }
}
