using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for CompressorValuesControl.
	/// </summary>
	public class CompressorValuesControl : System.Windows.Forms.UserControl
	{
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 80;
      public const int HEIGHT = 140;

      public const int INDEX_ISOTHERMAL = 0;
      public const int INDEX_POLYTROPIC = 2;
      public const int INDEX_ADIABATIC = 1;

      private bool inConstruction;
      private CompressorControl compressorCtrl;

      private ProcessVarTextBox textBoxPressureRatio;
      private ProcessVarTextBox textBoxAdiabaticExponent;
      private ProcessVarTextBox textBoxAdiabaticEfficiency;
      private ProcessVarTextBox textBoxPolytropicExponent;
      private ProcessVarTextBox textBoxPolytropicEfficiency;
      private ProcessVarTextBox textBoxPowerInput;
      public System.Windows.Forms.ComboBox comboBoxCompressionProcess;
      
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public CompressorValuesControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public CompressorValuesControl(CompressorControl compressorCtrl) : this()
		{
         this.inConstruction = true;
         this.compressorCtrl = compressorCtrl;
         this.InitializeVariableTextBoxes(compressorCtrl);
         this.compressorCtrl.Compressor.SolveComplete += new SolveCompleteEventHandler(Compressor_SolveComplete);
         this.comboBoxCompressionProcess.SelectedIndex = -1;
         this.inConstruction = false;
         this.SetCompressionProcess(compressorCtrl.Compressor.CompressionProcess);
      }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.compressorCtrl.Compressor != null)
            this.compressorCtrl.Compressor.SolveComplete -= new SolveCompleteEventHandler(Compressor_SolveComplete);
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
         this.textBoxPressureRatio = new ProsimoUI.ProcessVarTextBox();
         this.textBoxAdiabaticExponent = new ProsimoUI.ProcessVarTextBox();
         this.textBoxAdiabaticEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPolytropicExponent = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPolytropicEfficiency = new ProsimoUI.ProcessVarTextBox();
         this.textBoxPowerInput = new ProsimoUI.ProcessVarTextBox();
         this.comboBoxCompressionProcess = new System.Windows.Forms.ComboBox();
         this.SuspendLayout();
         // 
         // textBoxPressureRatio
         // 
         this.textBoxPressureRatio.Location = new System.Drawing.Point(0, 0);
         this.textBoxPressureRatio.Name = "textBoxPressureRatio";
         this.textBoxPressureRatio.Size = new System.Drawing.Size(80, 20);
         this.textBoxPressureRatio.TabIndex = 1;
         this.textBoxPressureRatio.Text = "";
         this.textBoxPressureRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPressureRatio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxAdiabaticExponent
         // 
         this.textBoxAdiabaticExponent.Location = new System.Drawing.Point(0, 20);
         this.textBoxAdiabaticExponent.Name = "textBoxAdiabaticExponent";
         this.textBoxAdiabaticExponent.Size = new System.Drawing.Size(80, 20);
         this.textBoxAdiabaticExponent.TabIndex = 2;
         this.textBoxAdiabaticExponent.Text = "";
         this.textBoxAdiabaticExponent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxAdiabaticExponent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxAdiabaticEfficiency
         // 
         this.textBoxAdiabaticEfficiency.Location = new System.Drawing.Point(0, 40);
         this.textBoxAdiabaticEfficiency.Name = "textBoxAdiabaticEfficiency";
         this.textBoxAdiabaticEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxAdiabaticEfficiency.TabIndex = 3;
         this.textBoxAdiabaticEfficiency.Text = "";
         this.textBoxAdiabaticEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxAdiabaticEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPolytropicExponent
         // 
         this.textBoxPolytropicExponent.Location = new System.Drawing.Point(0, 60);
         this.textBoxPolytropicExponent.Name = "textBoxPolytropicExponent";
         this.textBoxPolytropicExponent.Size = new System.Drawing.Size(80, 20);
         this.textBoxPolytropicExponent.TabIndex = 4;
         this.textBoxPolytropicExponent.Text = "";
         this.textBoxPolytropicExponent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPolytropicExponent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPolytropicEfficiency
         // 
         this.textBoxPolytropicEfficiency.Location = new System.Drawing.Point(0, 80);
         this.textBoxPolytropicEfficiency.Name = "textBoxPolytropicEfficiency";
         this.textBoxPolytropicEfficiency.Size = new System.Drawing.Size(80, 20);
         this.textBoxPolytropicEfficiency.TabIndex = 5;
         this.textBoxPolytropicEfficiency.Text = "";
         this.textBoxPolytropicEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPolytropicEfficiency.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // textBoxPowerInput
         // 
         this.textBoxPowerInput.Location = new System.Drawing.Point(0, 100);
         this.textBoxPowerInput.Name = "textBoxPowerInput";
         this.textBoxPowerInput.Size = new System.Drawing.Size(80, 20);
         this.textBoxPowerInput.TabIndex = 6;
         this.textBoxPowerInput.Text = "";
         this.textBoxPowerInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.textBoxPowerInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // comboBoxCompressionProcess
         // 
         this.comboBoxCompressionProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxCompressionProcess.Items.AddRange(new object[] {
                                                                        "Isothermal",
                                                                        "Adiabatic",
                                                                        "Polytropic"});
         this.comboBoxCompressionProcess.Location = new System.Drawing.Point(0, 120);
         this.comboBoxCompressionProcess.Name = "comboBoxCompressionProcess";
         this.comboBoxCompressionProcess.Size = new System.Drawing.Size(80, 21);
         this.comboBoxCompressionProcess.TabIndex = 7;
         this.comboBoxCompressionProcess.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompressionProcess_SelectedIndexChanged);
         // 
         // CompressorValuesControl
         // 
         this.Controls.Add(this.comboBoxCompressionProcess);
         this.Controls.Add(this.textBoxPressureRatio);
         this.Controls.Add(this.textBoxAdiabaticExponent);
         this.Controls.Add(this.textBoxAdiabaticEfficiency);
         this.Controls.Add(this.textBoxPolytropicExponent);
         this.Controls.Add(this.textBoxPolytropicEfficiency);
         this.Controls.Add(this.textBoxPowerInput);
         this.Name = "CompressorValuesControl";
         this.Size = new System.Drawing.Size(80, 140);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableTextBoxes(CompressorControl ctrl)
      {
         this.textBoxPressureRatio.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.OutletInletPressureRatio);
         this.textBoxAdiabaticExponent.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.AdiabaticExponent);
         this.textBoxAdiabaticEfficiency.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.AdiabaticEfficiency);
         this.textBoxPolytropicExponent.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.PolytropicExponent);
         this.textBoxPolytropicEfficiency.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.PolytropicEfficiency);
         this.textBoxPowerInput.InitializeVariable(ctrl.Flowsheet.ApplicationPrefs, ctrl.Compressor.PowerInput);
      }

      private void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxPressureRatio);
         list.Add(this.textBoxAdiabaticExponent);
         list.Add(this.textBoxAdiabaticEfficiency);
         list.Add(this.textBoxPolytropicExponent);
         list.Add(this.textBoxPolytropicEfficiency);
         list.Add(this.textBoxPowerInput);
         list.Add(this.comboBoxCompressionProcess);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      public void SetCompressionProcess(CompressionProcess compressionProcess)
      {
         if (compressionProcess == CompressionProcess.Adiabatic)
            this.comboBoxCompressionProcess.SelectedIndex = CompressorValuesControl.INDEX_ADIABATIC;
         else if (compressionProcess == CompressionProcess.Isothermal)
            this.comboBoxCompressionProcess.SelectedIndex = CompressorValuesControl.INDEX_ISOTHERMAL;
         else if (compressionProcess == CompressionProcess.Polytropic)
            this.comboBoxCompressionProcess.SelectedIndex = CompressorValuesControl.INDEX_POLYTROPIC;
      }

      private void comboBoxCompressionProcess_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxCompressionProcess.SelectedIndex;
            if (idx == CompressorValuesControl.INDEX_ADIABATIC)
            {
               error = this.compressorCtrl.Compressor.SpecifyCompressionProcess(CompressionProcess.Adiabatic);
            }
            else if (idx == CompressorValuesControl.INDEX_ISOTHERMAL)
            {
               error = this.compressorCtrl.Compressor.SpecifyCompressionProcess(CompressionProcess.Isothermal);
            }
            else if (idx == CompressorValuesControl.INDEX_POLYTROPIC)
            {
               error = this.compressorCtrl.Compressor.SpecifyCompressionProcess(CompressionProcess.Polytropic);
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetCompressionProcess(this.compressorCtrl.Compressor.CompressionProcess);
            }
         }
      }

      private void Compressor_SolveComplete(object sender, SolveState solveState)
      {
         CompressionProcess compressionProcess = this.compressorCtrl.Compressor.CompressionProcess;
         this.SetCompressionProcess(compressionProcess);
      }
   }
}
