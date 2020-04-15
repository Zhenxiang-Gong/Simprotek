using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for HumidityChartProcessEditor.
	/// </summary>
	public class HumidityChartProcessEditor : System.Windows.Forms.UserControl
	{
      private HumidityChartStreamLabelsControl gasLabelsCtrl;
      private HumidityChartStreamValuesControl gasValuesInCtrl;
      private HumidityChartStreamValuesControl gasValuesOutCtrl;

      private DryingGasStream gasIn;
      private DryingGasStream gasOut;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBox;
      private System.Windows.Forms.TextBox textBoxGasOutName;
      private System.Windows.Forms.TextBox textBoxGasInName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HumidityChartProcessEditor(Flowsheet flowsheet, DryingGasStream gasIn, DryingGasStream gasOut)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.UpdateStreamsUI(flowsheet, gasIn, gasOut);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.gasIn != null)
            gasIn.SolveComplete -= new SolveCompleteEventHandler(gasIn_SolveComplete);
         if (this.gasOut != null)
            gasOut.SolveComplete -= new SolveCompleteEventHandler(gasOut_SolveComplete);
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
         this.panel = new System.Windows.Forms.Panel();
         this.groupBox = new System.Windows.Forms.GroupBox();
         this.textBoxGasOutName = new System.Windows.Forms.TextBox();
         this.textBoxGasInName = new System.Windows.Forms.TextBox();
         this.panel.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBox);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(368, 208);
         this.panel.TabIndex = 0;
         // 
         // groupBox
         // 
         this.groupBox.Controls.Add(this.textBoxGasOutName);
         this.groupBox.Controls.Add(this.textBoxGasInName);
         this.groupBox.Location = new System.Drawing.Point(4, 4);
         this.groupBox.Name = "groupBox";
         this.groupBox.Size = new System.Drawing.Size(360, 200);
         this.groupBox.TabIndex = 119;
         this.groupBox.TabStop = false;
         this.groupBox.Text = "Isenthalpic Process";
         // 
         // textBoxGasOutName
         // 
         this.textBoxGasOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxGasOutName.Name = "textBoxGasOutName";
         this.textBoxGasOutName.ReadOnly = true;
         this.textBoxGasOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasOutName.TabIndex = 13;
         this.textBoxGasOutName.Text = "";
         // 
         // textBoxGasInName
         // 
         this.textBoxGasInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxGasInName.Name = "textBoxGasInName";
         this.textBoxGasInName.ReadOnly = true;
         this.textBoxGasInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasInName.TabIndex = 12;
         this.textBoxGasInName.Text = "";
         // 
         // HumidityChartProcessEditor
         // 
         this.Controls.Add(this.panel);
         this.Name = "HumidityChartProcessEditor";
         this.Size = new System.Drawing.Size(368, 208);
         this.panel.ResumeLayout(false);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void UpdateStreamsUI(Flowsheet flowsheet, DryingGasStream gasIn, DryingGasStream gasOut)
      {
         this.gasIn = gasIn;
         this.gasOut = gasOut;

         gasLabelsCtrl = new HumidityChartStreamLabelsControl(gasIn);
         this.groupBox.Controls.Add(gasLabelsCtrl);
         gasLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         gasValuesInCtrl = new HumidityChartStreamValuesControl(flowsheet, gasIn);
         this.groupBox.Controls.Add(gasValuesInCtrl);
         gasValuesInCtrl.Location = new Point(196, 12 + 20 + 2);
         this.textBoxGasInName.Text = gasIn.Name;
         UI.SetStatusColor(this.textBoxGasInName, gasIn.SolveState);
         gasIn.SolveComplete += new SolveCompleteEventHandler(gasIn_SolveComplete);

         gasValuesOutCtrl = new HumidityChartStreamValuesControl(flowsheet, gasOut);
         this.groupBox.Controls.Add(gasValuesOutCtrl);
         gasValuesOutCtrl.Location = new Point(276, 12 + 20 + 2);
         this.textBoxGasOutName.Text = gasOut.Name;
         UI.SetStatusColor(this.textBoxGasOutName, gasOut.SolveState);
         gasOut.SolveComplete += new SolveCompleteEventHandler(gasOut_SolveComplete);
      }

      private void gasIn_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusColor(this.textBoxGasInName, solveState);
      }

      private void gasOut_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusColor(this.textBoxGasOutName, solveState);
      }
   }
}
