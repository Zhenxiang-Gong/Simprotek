using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Drying;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerLabelsControl.
	/// </summary>
	public class DryerLabelsControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 200;

      private ProcessVarLabel labelHeatLossByTransportDevice;
      private ProcessVarLabel labelWorkInput;
      private ProcessVarLabel labelHeatInput;
      private ProcessVarLabel labelHeatLoss;
      private ProcessVarLabel labelGasPressureDrop;
      private ProcessVarLabel labelThermalEfficiency;
      private ProcessVarLabel labelSpecificHeatConsumption;
      private ProcessVarLabel labelMoistureEvaporationRate;
      private ProcessVarLabel labelFractionOfMaterialLostToGasOutlet;
      private ProsimoUI.ProcessVarLabel labelGasOutletMaterialLoading;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public DryerLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public DryerLabelsControl(Dryer uo) : this()
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
         this.labelHeatLossByTransportDevice = new ProsimoUI.ProcessVarLabel();
         this.labelWorkInput = new ProsimoUI.ProcessVarLabel();
         this.labelHeatInput = new ProsimoUI.ProcessVarLabel();
         this.labelHeatLoss = new ProsimoUI.ProcessVarLabel();
         this.labelGasPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.labelThermalEfficiency = new ProsimoUI.ProcessVarLabel();
         this.labelSpecificHeatConsumption = new ProsimoUI.ProcessVarLabel();
         this.labelMoistureEvaporationRate = new ProsimoUI.ProcessVarLabel();
         this.labelFractionOfMaterialLostToGasOutlet = new ProsimoUI.ProcessVarLabel();
         this.labelGasOutletMaterialLoading = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelHeatLossByTransportDevice
         // 
         this.labelHeatLossByTransportDevice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeatLossByTransportDevice.Location = new System.Drawing.Point(0, 80);
         this.labelHeatLossByTransportDevice.Name = "labelHeatLossByTransportDevice";
         this.labelHeatLossByTransportDevice.Size = new System.Drawing.Size(192, 20);
         this.labelHeatLossByTransportDevice.TabIndex = 91;
         this.labelHeatLossByTransportDevice.Text = "HeatLossByTransportDevice";
         this.labelHeatLossByTransportDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelWorkInput
         // 
         this.labelWorkInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelWorkInput.Location = new System.Drawing.Point(0, 60);
         this.labelWorkInput.Name = "labelWorkInput";
         this.labelWorkInput.Size = new System.Drawing.Size(192, 20);
         this.labelWorkInput.TabIndex = 90;
         this.labelWorkInput.Text = "WorkInput";
         this.labelWorkInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHeatInput
         // 
         this.labelHeatInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeatInput.Location = new System.Drawing.Point(0, 40);
         this.labelHeatInput.Name = "labelHeatInput";
         this.labelHeatInput.Size = new System.Drawing.Size(192, 20);
         this.labelHeatInput.TabIndex = 89;
         this.labelHeatInput.Text = "HeatInput";
         this.labelHeatInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelHeatLoss
         // 
         this.labelHeatLoss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelHeatLoss.Location = new System.Drawing.Point(0, 20);
         this.labelHeatLoss.Name = "labelHeatLoss";
         this.labelHeatLoss.Size = new System.Drawing.Size(192, 20);
         this.labelHeatLoss.TabIndex = 88;
         this.labelHeatLoss.Text = "HeatLoss";
         this.labelHeatLoss.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelGasPressureDrop
         // 
         this.labelGasPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelGasPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelGasPressureDrop.Name = "labelGasPressureDrop";
         this.labelGasPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelGasPressureDrop.TabIndex = 87;
         this.labelGasPressureDrop.Text = "GasPressureDrop";
         this.labelGasPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelThermalEfficiency
         // 
         this.labelThermalEfficiency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelThermalEfficiency.Location = new System.Drawing.Point(0, 140);
         this.labelThermalEfficiency.Name = "labelThermalEfficiency";
         this.labelThermalEfficiency.Size = new System.Drawing.Size(192, 20);
         this.labelThermalEfficiency.TabIndex = 94;
         this.labelThermalEfficiency.Text = "ThermalEfficiency";
         this.labelThermalEfficiency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelSpecificHeatConsumption
         // 
         this.labelSpecificHeatConsumption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelSpecificHeatConsumption.Location = new System.Drawing.Point(0, 120);
         this.labelSpecificHeatConsumption.Name = "labelSpecificHeatConsumption";
         this.labelSpecificHeatConsumption.Size = new System.Drawing.Size(192, 20);
         this.labelSpecificHeatConsumption.TabIndex = 93;
         this.labelSpecificHeatConsumption.Text = "SpecificHeatConsumption";
         this.labelSpecificHeatConsumption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelMoistureEvaporationRate
         // 
         this.labelMoistureEvaporationRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelMoistureEvaporationRate.Location = new System.Drawing.Point(0, 100);
         this.labelMoistureEvaporationRate.Name = "labelMoistureEvaporationRate";
         this.labelMoistureEvaporationRate.Size = new System.Drawing.Size(192, 20);
         this.labelMoistureEvaporationRate.TabIndex = 92;
         this.labelMoistureEvaporationRate.Text = "MoistureEvaporationRate";
         this.labelMoistureEvaporationRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelFractionOfMaterialLostToGasOutlet
         // 
         this.labelFractionOfMaterialLostToGasOutlet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelFractionOfMaterialLostToGasOutlet.Location = new System.Drawing.Point(0, 160);
         this.labelFractionOfMaterialLostToGasOutlet.Name = "labelFractionOfMaterialLostToGasOutlet";
         this.labelFractionOfMaterialLostToGasOutlet.Size = new System.Drawing.Size(192, 20);
         this.labelFractionOfMaterialLostToGasOutlet.TabIndex = 92;
         this.labelFractionOfMaterialLostToGasOutlet.Text = "FractionOfMaterialLostToGasOutlet";
         this.labelFractionOfMaterialLostToGasOutlet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // labelGasOutletMaterialLoading
         // 
         this.labelGasOutletMaterialLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelGasOutletMaterialLoading.Location = new System.Drawing.Point(0, 180);
         this.labelGasOutletMaterialLoading.Name = "labelGasOutletMaterialLoading";
         this.labelGasOutletMaterialLoading.Size = new System.Drawing.Size(192, 20);
         this.labelGasOutletMaterialLoading.TabIndex = 95;
         this.labelGasOutletMaterialLoading.Text = "GasOutletMaterialLoading";
         this.labelGasOutletMaterialLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DryerLabelsControl
         // 
         this.Controls.Add(this.labelGasOutletMaterialLoading);
         this.Controls.Add(this.labelThermalEfficiency);
         this.Controls.Add(this.labelSpecificHeatConsumption);
         this.Controls.Add(this.labelMoistureEvaporationRate);
         this.Controls.Add(this.labelHeatLossByTransportDevice);
         this.Controls.Add(this.labelWorkInput);
         this.Controls.Add(this.labelHeatInput);
         this.Controls.Add(this.labelHeatLoss);
         this.Controls.Add(this.labelGasPressureDrop);
         this.Controls.Add(this.labelFractionOfMaterialLostToGasOutlet);
         this.Name = "DryerLabelsControl";
         this.Size = new System.Drawing.Size(192, 200);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Dryer uo)
      {
         this.labelMoistureEvaporationRate.InitializeVariable(uo.MoistureEvaporationRate);
         this.labelSpecificHeatConsumption.InitializeVariable(uo.SpecificHeatConsumption);
         this.labelThermalEfficiency.InitializeVariable(uo.ThermalEfficiency);
         this.labelGasPressureDrop.InitializeVariable(uo.GasPressureDrop);
         this.labelHeatLoss.InitializeVariable(uo.HeatLoss);
         this.labelHeatInput.InitializeVariable(uo.HeatInput);
         this.labelWorkInput.InitializeVariable(uo.WorkInput);
         this.labelHeatLossByTransportDevice.InitializeVariable(uo.HeatLossByTransportDevice);
         this.labelFractionOfMaterialLostToGasOutlet.InitializeVariable(uo.FractionOfMaterialLostToGasOutlet);
         this.labelGasOutletMaterialLoading.InitializeVariable(uo.GasOutletMaterialLoading);
      }
   }
}
