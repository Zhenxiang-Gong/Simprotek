using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerValuesControl.
	/// </summary>
	public class DryerValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 200;

      private ProcessVarTextBox textBoxWorkInput;
      private ProcessVarTextBox textBoxHeatLossByTransportDevice;
      private ProcessVarTextBox textBoxGasPressureDrop;
      private ProcessVarTextBox textBoxHeatLoss;
      private ProcessVarTextBox textBoxHeatInput;
      private ProcessVarTextBox textBoxMoistureEvaporationRate;
      private ProcessVarTextBox textBoxSpecificHeatConsumption;
      private ProcessVarTextBox textBoxThermalEfficiency;
      private ProcessVarTextBox textFractionOfMaterialLostToGasOutlet;
      private ProsimoUI.ProcessVarTextBox textBoxGasOutletMaterialLoading;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public DryerValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public DryerValuesControl(DryerControl dryerCtrl) : this()
		{
         this.InitializeVariableTextBoxes(dryerCtrl);
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
         this.textBoxWorkInput = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHeatLossByTransportDevice = new ProsimoUI.ProcessVarTextBox();
         this.textBoxGasPressureDrop = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHeatLoss = new ProsimoUI.ProcessVarTextBox();
         this.textBoxHeatInput = new ProsimoUI.ProcessVarTextBox();
         this.textBoxMoistureEvaporationRate = new ProsimoUI.ProcessVarTextBox();
         this.textBoxSpecificHeatConsumption = new ProsimoUI.ProcessVarTextBox();
         this.textBoxThermalEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textFractionOfMaterialLostToGasOutlet = new ProsimoUI.ProcessVarTextBox();
         this.textBoxGasOutletMaterialLoading = new ProsimoUI.ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // textBoxWorkInput
         // 
         this.textBoxWorkInput.Location = new System.Drawing.Point(0, 60);
         this.textBoxWorkInput.Name = "textBoxWorkInput";
         this.textBoxWorkInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxWorkInput.TabIndex = 9;
         this.textBoxWorkInput.Text = "";
         this.textBoxWorkInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxWorkInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeatLossByTransportDevice
         // 
         this.textBoxHeatLossByTransportDevice.Location = new System.Drawing.Point(0, 80);
         this.textBoxHeatLossByTransportDevice.Name = "textBoxHeatLossByTransportDevice";
         this.textBoxHeatLossByTransportDevice.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeatLossByTransportDevice.TabIndex = 10;
         this.textBoxHeatLossByTransportDevice.Text = "";
         this.textBoxHeatLossByTransportDevice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHeatLossByTransportDevice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxGasPressureDrop
         // 
         this.textBoxGasPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.textBoxGasPressureDrop.Name = "textBoxGasPressureDrop";
         this.textBoxGasPressureDrop.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasPressureDrop.TabIndex = 6;
         this.textBoxGasPressureDrop.Text = "";
         this.textBoxGasPressureDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasPressureDrop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeatLoss
         // 
         this.textBoxHeatLoss.Location = new System.Drawing.Point(0, 20);
         this.textBoxHeatLoss.Name = "textBoxHeatLoss";
         this.textBoxHeatLoss.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeatLoss.TabIndex = 7;
         this.textBoxHeatLoss.Text = "";
         this.textBoxHeatLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHeatLoss.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxHeatInput
         // 
         this.textBoxHeatInput.Location = new System.Drawing.Point(0, 40);
         this.textBoxHeatInput.Name = "textBoxHeatInput";
         this.textBoxHeatInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxHeatInput.TabIndex = 8;
         this.textBoxHeatInput.Text = "";
         this.textBoxHeatInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxHeatInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxMoistureEvaporationRate
         // 
         this.textBoxMoistureEvaporationRate.Location = new System.Drawing.Point(0, 100);
         this.textBoxMoistureEvaporationRate.Name = "textBoxMoistureEvaporationRate";
         this.textBoxMoistureEvaporationRate.Size = new System.Drawing.Size(80, 20);
         this.textBoxMoistureEvaporationRate.TabIndex = 11;
         this.textBoxMoistureEvaporationRate.Text = "";
         this.textBoxMoistureEvaporationRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxMoistureEvaporationRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxSpecificHeatConsumption
         // 
         this.textBoxSpecificHeatConsumption.Location = new System.Drawing.Point(0, 120);
         this.textBoxSpecificHeatConsumption.Name = "textBoxSpecificHeatConsumption";
         this.textBoxSpecificHeatConsumption.Size = new System.Drawing.Size(80, 20);
         this.textBoxSpecificHeatConsumption.TabIndex = 12;
         this.textBoxSpecificHeatConsumption.Text = "";
         this.textBoxSpecificHeatConsumption.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxSpecificHeatConsumption.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxThermalEfficiency
         // 
         this.textBoxThermalEfficiency.Location = new System.Drawing.Point(0, 140);
         this.textBoxThermalEfficiency.Name = "textBoxThermalEfficiency";
         this.textBoxThermalEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxThermalEfficiency.TabIndex = 13;
         this.textBoxThermalEfficiency.Text = "";
         this.textBoxThermalEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxThermalEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textFractionOfMaterialLostToGasOutlet
         // 
         this.textFractionOfMaterialLostToGasOutlet.Location = new System.Drawing.Point(0, 160);
         this.textFractionOfMaterialLostToGasOutlet.Name = "textFractionOfMaterialLostToGasOutlet";
         this.textFractionOfMaterialLostToGasOutlet.Size = new System.Drawing.Size(80, 20);
         this.textFractionOfMaterialLostToGasOutlet.TabIndex = 14;
         this.textFractionOfMaterialLostToGasOutlet.Text = "";
         this.textFractionOfMaterialLostToGasOutlet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textFractionOfMaterialLostToGasOutlet.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxGasOutletMaterialLoading
         // 
         this.textBoxGasOutletMaterialLoading.Location = new System.Drawing.Point(0, 180);
         this.textBoxGasOutletMaterialLoading.Name = "textBoxGasOutletMaterialLoading";
         this.textBoxGasOutletMaterialLoading.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasOutletMaterialLoading.TabIndex = 15;
         this.textBoxGasOutletMaterialLoading.Text = "";
         this.textBoxGasOutletMaterialLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxGasOutletMaterialLoading.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // DryerValuesControl
         // 
         this.Controls.Add(this.textBoxGasOutletMaterialLoading);
         this.Controls.Add(this.textBoxMoistureEvaporationRate);
         this.Controls.Add(this.textBoxSpecificHeatConsumption);
         this.Controls.Add(this.textBoxThermalEfficiency);
         this.Controls.Add(this.textBoxWorkInput);
         this.Controls.Add(this.textBoxHeatLossByTransportDevice);
         this.Controls.Add(this.textBoxGasPressureDrop);
         this.Controls.Add(this.textBoxHeatLoss);
         this.Controls.Add(this.textBoxHeatInput);
         this.Controls.Add(this.textFractionOfMaterialLostToGasOutlet);
         this.Name = "DryerValuesControl";
         this.Size = new System.Drawing.Size(80, 200);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(DryerControl ctrl)
      {
         this.textBoxMoistureEvaporationRate.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.MoistureEvaporationRate);
         this.textBoxSpecificHeatConsumption.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.SpecificHeatConsumption);
         this.textBoxThermalEfficiency.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.ThermalEfficiency);
         this.textBoxGasPressureDrop.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.GasPressureDrop);
         this.textBoxHeatLoss.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.HeatLoss);
         this.textBoxHeatInput.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.HeatInput);
         this.textBoxWorkInput.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.WorkInput);
         this.textBoxHeatLossByTransportDevice.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.HeatLossByTransportDevice);
         this.textFractionOfMaterialLostToGasOutlet.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.FractionOfMaterialLostToGasOutlet);
         this.textBoxGasOutletMaterialLoading.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Dryer.GasOutletMaterialLoading);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxGasPressureDrop);
         list.Add(this.textBoxHeatLoss);
         list.Add(this.textBoxHeatInput);
         list.Add(this.textBoxWorkInput);
         list.Add(this.textBoxHeatLossByTransportDevice);
         list.Add(this.textBoxMoistureEvaporationRate);
         list.Add(this.textBoxSpecificHeatConsumption);
         list.Add(this.textBoxThermalEfficiency);
         list.Add(this.textFractionOfMaterialLostToGasOutlet);
         list.Add(this.textBoxGasOutletMaterialLoading);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }
   }
}
