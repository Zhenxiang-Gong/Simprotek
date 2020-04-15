using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for BagFilterEditor.
	/// </summary>
	public class BagFilterEditor : UnitOpEditor
	{
      private System.Windows.Forms.GroupBox groupBoxStreams;
      private ProsimoUI.SolvableNameTextBox textBoxStreamInName;
      private ProsimoUI.SolvableNameTextBox textBoxStreamOutName;
      private ProsimoUI.SolvableNameTextBox textBoxParticleOutName;
      private System.Windows.Forms.GroupBox groupBoxParticle;
      protected System.Windows.Forms.GroupBox groupBoxUnitOpVars;

      public BagFilterControl BagFilterCtrl
      {
         get {return (BagFilterControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BagFilterEditor(BagFilterControl bagFilterCtrl) : base(bagFilterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.UpdateStreamsUI();

         this.Text = "Bag Filter: " + UnitOpCtrl.UnitOperation.Name;

         this.groupBoxUnitOpVars.Location = new System.Drawing.Point(644, 24);
         this.groupBoxUnitOpVars.Size = new System.Drawing.Size(280, 260);
         this.groupBoxUnitOpVars.Controls.Clear();
         this.groupBoxUnitOpVars.Text = "Bag Filter";

         ProcessVarLabelsControl bagFilterLabelsCtrl = new ProcessVarLabelsControl(solvableCtrl.Solvable.VarList);
         this.groupBoxUnitOpVars.Controls.Add(bagFilterLabelsCtrl);
         bagFilterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //BagFilterValuesControl bagFilterValuesCtrl = new BagFilterValuesControl(this.BagFilterCtrl);
         ProcessVarValuesControl bagFilterValuesCtrl = new ProcessVarValuesControl(solvableCtrl);
         this.groupBoxUnitOpVars.Controls.Add(bagFilterValuesCtrl);
         bagFilterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
      private void InitializeComponent() {
         this.groupBoxStreams = new System.Windows.Forms.GroupBox();
         this.textBoxStreamOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxStreamInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxUnitOpVars = new System.Windows.Forms.GroupBox();
         this.groupBoxParticle = new System.Windows.Forms.GroupBox();
         this.textBoxParticleOutName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxStreams.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.groupBoxParticle.SuspendLayout();
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
         this.groupBoxStreams.Text = "Gas Inlet/Outlet";
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
         this.groupBoxUnitOpVars.Location = new System.Drawing.Point(364, 24);
         this.groupBoxUnitOpVars.Name = "groupBoxTwoStreamUnitOp";
         this.groupBoxUnitOpVars.Size = new System.Drawing.Size(280, 280);
         this.groupBoxUnitOpVars.TabIndex = 127;
         this.groupBoxUnitOpVars.TabStop = false;
         // 
         // groupBoxParticle
         // 
         this.groupBoxParticle.Controls.Add(this.textBoxParticleOutName);
         this.groupBoxParticle.Location = new System.Drawing.Point(364, 24);
         this.groupBoxParticle.Name = "groupBoxParticle";
         this.groupBoxParticle.Size = new System.Drawing.Size(280, 300);
         this.groupBoxParticle.TabIndex = 128;
         this.groupBoxParticle.TabStop = false;
         this.groupBoxParticle.Text = "Particle Outlet";
         // 
         // textBoxParticleOutName
         // 
         this.textBoxParticleOutName.Location = new System.Drawing.Point(196, 12);
         this.textBoxParticleOutName.Name = "textBoxParticleOutName";
         this.textBoxParticleOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxParticleOutName.TabIndex = 6;
         this.textBoxParticleOutName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxUnitOpVars);
         this.panel.Controls.Add(this.groupBoxStreams);
         this.panel.Controls.Add(this.groupBoxParticle);
         this.panel.Size = new System.Drawing.Size(960, 329);
         // 
         // TwoStreamUnitOpEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(930, 351);
         this.Name = "TwoStreamUnitOpEditor";
         this.Text = "TwoStreamUnitOp";
         this.groupBoxStreams.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxStreamInName);
         list.Add(this.textBoxStreamOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      protected override void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxStreams.Controls.Clear();
         BagFilter bagFilter = this.BagFilterCtrl.BagFilter;
         bool hasStreamIn = false;
         bool hasStreamOut = false;
         bool hasParticleOut = false;

         ProcessStreamBase streamIn = bagFilter.GasInlet;
         hasStreamIn = streamIn != null;

         ProcessStreamBase streamOut = bagFilter.GasOutlet;
         hasStreamOut = streamOut != null;

         ProcessStreamBase particleOut = bagFilter.ParticleOutlet;
         if(particleOut != null)
            hasParticleOut = true;
         
         if(hasStreamIn || hasStreamOut)
         {
            ProcessStreamBase labelsStream = hasStreamIn ? streamIn : streamOut;
            UserControl ctrl = null;
            if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }

            //UserControl ctrl = new ProcessVarLabelsControl(labelsStream.VarList);

            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if(hasStreamIn && streamIn is DryingGasStream)
         {
            GasStreamControl gasInCtrl = (GasStreamControl)this.BagFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BagFilterCtrl.BagFilter.GasInlet.Name);
            UserControl ctrl = new GasStreamValuesControl(gasInCtrl);

            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxStreamInName.SetSolvable(streamIn);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamInName);
            this.textBoxStreamInName.Text = bagFilter.Inlet.Name;
            UI.SetStatusColor(this.textBoxStreamInName, bagFilter.Inlet.SolveState);
         }

         if (hasStreamOut && streamOut is DryingGasStream) {
            ProcessStreamBaseControl baseControl = this.BagFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BagFilterCtrl.BagFilter.GasOutlet.Name);

            GasStreamControl gasOutCtrl = (GasStreamControl)this.BagFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BagFilterCtrl.BagFilter.GasOutlet.Name);
            UserControl ctrl = new GasStreamValuesControl(gasOutCtrl);

            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxStreamOutName.SetSolvable(streamOut);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamOutName);
            this.textBoxStreamOutName.Text = bagFilter.Outlet.Name;
            UI.SetStatusColor(this.textBoxStreamOutName, bagFilter.Outlet.SolveState);
         }

         if(hasParticleOut && particleOut is DryingMaterialStream)
         {
            // add the labels
            UserControl ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)particleOut);
            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);

            // add the values
            ProcessStreamBaseControl matOutCtrl = this.BagFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BagFilterCtrl.BagFilter.ParticleOutlet.Name);
            //ctrl = new ProcessVarValuesControl(matOutCtrl);
            ctrl = new MaterialStreamValuesControl((MaterialStreamControl)matOutCtrl);

            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxParticleOutName.SetSolvable(bagFilter.ParticleOutlet);
            this.groupBoxParticle.Controls.Add(this.textBoxParticleOutName);
            this.textBoxParticleOutName.Text = bagFilter.ParticleOutlet.Name;
            UI.SetStatusColor(this.textBoxParticleOutName, bagFilter.ParticleOutlet.SolveState);
         }
      }
   }
}

//protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
//   BagFilter bagFilter = BagFilterCtrl.BagFilter;
//   TextBox tb = (TextBox)sender;
//   if (tb.Text != null) {
//      if (tb.Text.Trim().Equals("")) {
//         if (sender == this.textBoxName) {
//            e.Cancel = true;
//            string message3 = "Please specify a name!";
//            MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//         }
//      }
//      else {
//         if (sender == this.textBoxName) {
//            ErrorMessage error = bagFilter.SpecifyName(this.textBoxName.Text);
//            if (error != null)
//               UI.ShowError(error);
//         }
//      }
//   }
//}
//private void BagFilter_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
//   this.UpdateStreamsUI();
//}

//private void BagFilter_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
//   this.UpdateStreamsUI();
//}