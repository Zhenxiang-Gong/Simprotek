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
	/// Summary description for HumidityChartCurrentStateEditor.
	/// </summary>
	public class HumidityChartCurrentStateEditor : System.Windows.Forms.UserControl
	{
      private HumidityChartStreamLabelsControl gasLabelsCtrl;
      private HumidityChartStreamValuesControl gasValuesCtrl;

      private DryingGasStream stream;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBox;
      private System.Windows.Forms.TextBox textBoxGasName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HumidityChartCurrentStateEditor(Flowsheet flowsheet, DryingGasStream stream)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.UpdateStreamsUI(flowsheet, stream);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.stream != null)
            stream.SolveComplete -= new SolveCompleteEventHandler(stream_SolveComplete);
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
         this.textBoxGasName = new System.Windows.Forms.TextBox();
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
         this.groupBox.Controls.Add(this.textBoxGasName);
         this.groupBox.Location = new System.Drawing.Point(4, 4);
         this.groupBox.Name = "groupBox";
         this.groupBox.Size = new System.Drawing.Size(360, 200);
         this.groupBox.TabIndex = 119;
         this.groupBox.TabStop = false;
         this.groupBox.Text = "Gas State";
         // 
         // textBoxGasName
         // 
         this.textBoxGasName.Location = new System.Drawing.Point(196, 12);
         this.textBoxGasName.Name = "textBoxGasName";
         this.textBoxGasName.ReadOnly = true;
         this.textBoxGasName.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasName.TabIndex = 12;
         this.textBoxGasName.Text = "";
         // 
         // HumidityChartCurrentStateEditor
         // 
         this.Controls.Add(this.panel);
         this.Name = "HumidityChartCurrentStateEditor";
         this.Size = new System.Drawing.Size(368, 208);
         this.panel.ResumeLayout(false);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void UpdateStreamsUI(Flowsheet flowsheet, DryingGasStream stream)
      {
         this.stream = stream;

         gasLabelsCtrl = new HumidityChartStreamLabelsControl(stream);
         this.groupBox.Controls.Add(gasLabelsCtrl);
         gasLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         gasValuesCtrl = new HumidityChartStreamValuesControl(flowsheet, stream);
         this.groupBox.Controls.Add(gasValuesCtrl);
         gasValuesCtrl.Location = new Point(196, 12 + 20 + 2);
         this.textBoxGasName.Text = stream.Name;
         UI.SetStatusColor(this.textBoxGasName, stream.SolveState);
         stream.SolveComplete += new SolveCompleteEventHandler(stream_SolveComplete);
      }

      private void stream_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusColor(this.textBoxGasName, solveState);
      }
	}
}
