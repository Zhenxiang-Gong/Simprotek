using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for TwoStreamUnitOpEditor.
	/// </summary>
	public class TwoStreamUnitOpEditor : UnitOpEditor
	{
      public TwoStreamUnitOpControl TwoStreamUnitOpCtrl
      {
         get {return (TwoStreamUnitOpControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      private System.Windows.Forms.GroupBox groupBoxStreams;
      private ProsimoUI.SolvableNameTextBox textBoxStreamInName;
      private ProsimoUI.SolvableNameTextBox textBoxStreamOutName;
      protected System.Windows.Forms.GroupBox groupBoxTwoStreamUnitOp;
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public TwoStreamUnitOpEditor(TwoStreamUnitOpControl twoStreamUnitOpCtrl) : base(twoStreamUnitOpCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.UpdateStreamsUI();

         if (twoStreamUnitOpCtrl.TwoStreamUnitOp.Inlet is DryingGasStream)
         {
            this.groupBoxStreams.Size = new System.Drawing.Size(360, 280);
            this.panel.Size = new System.Drawing.Size(650, 309);
            this.ClientSize = new System.Drawing.Size(650, 331);
         }
         else if (twoStreamUnitOpCtrl.TwoStreamUnitOp.Inlet is DryingMaterialStream)
         {
            this.groupBoxStreams.Size = new System.Drawing.Size(360, 300);
            this.panel.Size = new System.Drawing.Size(650, 329);
            this.ClientSize = new System.Drawing.Size(650, 351);
         }

         twoStreamUnitOpCtrl.TwoStreamUnitOp.StreamAttached += new StreamAttachedEventHandler(TwoStreamUnitOp_StreamAttached);
         twoStreamUnitOpCtrl.TwoStreamUnitOp.StreamDetached += new StreamDetachedEventHandler(TwoStreamUnitOp_StreamDetached);
      }

		public TwoStreamUnitOpEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.groupBoxStreams = new System.Windows.Forms.GroupBox();
         this.textBoxStreamOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxStreamInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxTwoStreamUnitOp = new System.Windows.Forms.GroupBox();
         this.groupBoxStreams.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxStreams
         // 
         this.groupBoxStreams.Controls.Add(this.textBoxStreamOutName);
         this.groupBoxStreams.Controls.Add(this.textBoxStreamInName);
         this.groupBoxStreams.Location = new System.Drawing.Point(4, 24);
         this.groupBoxStreams.Name = "groupBoxStreams";
         this.groupBoxStreams.Size = new System.Drawing.Size(360, 280);
         this.groupBoxStreams.TabIndex = 118;
         this.groupBoxStreams.TabStop = false;
         this.groupBoxStreams.Text = "Inlet/Outlet";
         // 
         // textBoxStreamOutName
         // 
         this.textBoxStreamOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxStreamOutName.Name = "textBoxStreamOutName";
         this.textBoxStreamOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxStreamOutName.TabIndex = 7;
         this.textBoxStreamOutName.Text = "";
         // 
         // textBoxStreamInName
         // 
         this.textBoxStreamInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxStreamInName.Name = "textBoxStreamInName";
         this.textBoxStreamInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxStreamInName.TabIndex = 6;
         this.textBoxStreamInName.Text = "";
         // 
         // groupBoxTwoStreamUnitOp
         // 
         this.groupBoxTwoStreamUnitOp.Location = new System.Drawing.Point(364, 24);
         this.groupBoxTwoStreamUnitOp.Name = "groupBoxTwoStreamUnitOp";
         this.groupBoxTwoStreamUnitOp.Size = new System.Drawing.Size(280, 280);
         this.groupBoxTwoStreamUnitOp.TabIndex = 127;
         this.groupBoxTwoStreamUnitOp.TabStop = false;
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxTwoStreamUnitOp);
         this.panel.Controls.Add(this.groupBoxStreams);
         this.panel.Size = new System.Drawing.Size(650, 309);
         // 
         // TwoStreamUnitOpEditor
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(650, 331);
         this.Name = "TwoStreamUnitOpEditor";
         this.Text = "TwoStreamUnitOp";
         this.groupBoxStreams.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         TwoStreamUnitOperation twoStreamUnitOp = this.TwoStreamUnitOpCtrl.TwoStreamUnitOp;
         TextBox tb = (TextBox)sender;
         if (tb.Text != null)
         {
            if (tb.Text.Trim().Equals(""))
            {
               if (sender == this.textBoxName)
               {
                  e.Cancel = true;
                  string message3 = "Please specify a name!";
                  MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            else
            {
               if (sender == this.textBoxName)
               {
                  ErrorMessage error = twoStreamUnitOp.SpecifyName(this.textBoxName.Text);
                  if (error != null)
                     UI.ShowError(error);
               }
            }
         }
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxStreamInName);
         list.Add(this.textBoxStreamOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void TwoStreamUnitOp_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void TwoStreamUnitOp_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxStreams.Controls.Clear();
         TwoStreamUnitOperation twoStreamUnitOp = this.TwoStreamUnitOpCtrl.TwoStreamUnitOp;
         bool hasStreamIn = false;
         bool hasStreamOut = false;

         ProcessStreamBase streamIn = twoStreamUnitOp.Inlet;
         if (streamIn != null)
            hasStreamIn = true;

         ProcessStreamBase streamOut = twoStreamUnitOp.Outlet;
         if (streamOut != null)
            hasStreamOut = true;

         if (hasStreamIn || hasStreamOut)
         {
            ProcessStreamBase labelsStream = null;
            if (hasStreamIn)
               labelsStream = streamIn;
            else if (hasStreamOut)
               labelsStream = streamOut;
            
            UserControl ctrl = null;
            if (labelsStream is ProcessStream)
            {
               ctrl = new ProcessStreamLabelsControl((ProcessStream)labelsStream);
            }
            else if (labelsStream is DryingGasStream)
            {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            else if (labelsStream is DryingMaterialStream)
            {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasStreamIn)
         {
            UserControl ctrl = null;
            if (streamIn is ProcessStream)
            {
               ProcessStreamControl processInCtrl = (ProcessStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Inlet.Name);
               ctrl = new ProcessStreamValuesControl(processInCtrl);
            }
            else if (streamIn is DryingGasStream)
            {
               GasStreamControl gasInCtrl = (GasStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Inlet.Name);
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            else if (streamIn is DryingMaterialStream)
            {
               MaterialStreamControl materialInCtrl = (MaterialStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Inlet.Name);
               ctrl = new MaterialStreamValuesControl(materialInCtrl);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxStreamInName.SetSolvable(streamIn);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamInName);
            this.textBoxStreamInName.Text = twoStreamUnitOp.Inlet.Name;
            UI.SetStatusColor(this.textBoxStreamInName, twoStreamUnitOp.Inlet.SolveState);
         }

         if (hasStreamOut)
         {
            UserControl ctrl = null;
            if (streamOut is ProcessStream)
            {
               ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Outlet.Name);
               ctrl = new ProcessStreamValuesControl(processOutCtrl);
            }
            else if (streamOut is DryingGasStream)
            {
               GasStreamControl gasOutCtrl = (GasStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Outlet.Name);
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            else if (streamOut is DryingMaterialStream)
            {
               MaterialStreamControl materialOutCtrl = (MaterialStreamControl)this.TwoStreamUnitOpCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.TwoStreamUnitOpCtrl.TwoStreamUnitOp.Outlet.Name);
               ctrl = new MaterialStreamValuesControl(materialOutCtrl);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxStreamOutName.SetSolvable(streamOut);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamOutName);
            this.textBoxStreamOutName.Text = twoStreamUnitOp.Outlet.Name;
            UI.SetStatusColor(this.textBoxStreamOutName, twoStreamUnitOp.Outlet.SolveState);
         }
      }
   }
}
