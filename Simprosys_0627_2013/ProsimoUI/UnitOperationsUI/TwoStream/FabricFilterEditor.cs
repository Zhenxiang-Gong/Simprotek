using System.Drawing;
using System.Windows.Forms;
using System.Collections;

using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FabricFilterEditor.
	/// </summary>
	public class FabricFilterEditor : UnitOpEditor
	{
      private System.Windows.Forms.GroupBox groupBoxStreams;
      private ProsimoUI.SolvableNameTextBox textBoxStreamInName;
      private ProsimoUI.SolvableNameTextBox textBoxStreamOutName;
      private ProsimoUI.SolvableNameTextBox textBoxParticleOutName;
      private System.Windows.Forms.GroupBox groupBoxParticle;
      private System.Windows.Forms.GroupBox groupBoxFabricFilter;
      
      internal GroupBox FabricFilterGroupBox
      {
         get { return this.groupBoxFabricFilter; }
      }

      public FabricFilterControl FabricFilterCtrl
      {
         get {return (FabricFilterControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FabricFilterEditor(FabricFilterControl fabricFilterCtrl) : base(fabricFilterCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.UpdateStreamsUI();

         this.groupBoxFabricFilter.Location = new System.Drawing.Point(644, 24);
         this.groupBoxFabricFilter.Size = new System.Drawing.Size(280, 260);
         this.groupBoxFabricFilter.Controls.Clear();

         //BagFilterLabelsControl bagFilterLabelsCtrl = new BagFilterLabelsControl(bagFilterCtrl.BagFilter);
         ProcessVarLabelsControl fabricFilterLabelsCtrl = new ProcessVarLabelsControl(this.solvableCtrl.Solvable.VarList);
         this.groupBoxFabricFilter.Controls.Add(fabricFilterLabelsCtrl);
         fabricFilterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //BagFilterValuesControl bagFilterValuesCtrl = new BagFilterValuesControl(this.BagFilterCtrl);
         ProcessVarValuesControl fabricFilterValuesCtrl = new ProcessVarValuesControl(this.solvableCtrl);
         this.groupBoxFabricFilter.Controls.Add(fabricFilterValuesCtrl);
         fabricFilterValuesCtrl.Location = new Point(196, 12 + 20 + 2);
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
         this.groupBoxFabricFilter = new System.Windows.Forms.GroupBox();
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
         this.groupBoxFabricFilter.Location = new System.Drawing.Point(364, 24);
         this.groupBoxFabricFilter.Name = "groupBoxTwoStreamUnitOp";
         this.groupBoxFabricFilter.Size = new System.Drawing.Size(280, 280);
         this.groupBoxFabricFilter.TabIndex = 127;
         this.groupBoxFabricFilter.TabStop = false;
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
         this.panel.Controls.Add(this.groupBoxFabricFilter);
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

      //protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
      //   UnitOperation bagFilter = BagFilterCtrl.BagFilter;
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

      //private void BagFilter_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
      //   this.UpdateStreamsUI();
      //}

      //private void BagFilter_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
      //   this.UpdateStreamsUI();
      //}

      protected override void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxStreams.Controls.Clear();
         FabricFilter fabricFilter = this.FabricFilterCtrl.FabricFilter;
         bool hasStreamIn = false;
         bool hasStreamOut = false;
         bool hasParticleOut = false;

         ProcessStreamBase streamIn = fabricFilter.GasInlet;
         hasStreamIn = streamIn != null;

         ProcessStreamBase streamOut = fabricFilter.GasOutlet;
         hasStreamOut = streamOut != null;

         ProcessStreamBase particleOut = fabricFilter.ParticleOutlet;
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
            GasStreamControl gasInCtrl = (GasStreamControl)this.FabricFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FabricFilterCtrl.FabricFilter.GasInlet.Name);
            UserControl ctrl = new GasStreamValuesControl(gasInCtrl);

            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxStreamInName.SetSolvable(streamIn);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamInName);
            this.textBoxStreamInName.Text = fabricFilter.Inlet.Name;
            UI.SetStatusColor(this.textBoxStreamInName, fabricFilter.Inlet.SolveState);
         }

         if (hasStreamOut && streamOut is DryingGasStream) {
            ProcessStreamBaseControl baseControl = this.FabricFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FabricFilterCtrl.FabricFilter.GasOutlet.Name);

            GasStreamControl gasOutCtrl = (GasStreamControl)this.FabricFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FabricFilterCtrl.FabricFilter.GasOutlet.Name);
            UserControl ctrl = new GasStreamValuesControl(gasOutCtrl);

            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxStreamOutName.SetSolvable(streamOut);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamOutName);
            this.textBoxStreamOutName.Text = fabricFilter.Outlet.Name;
            UI.SetStatusColor(this.textBoxStreamOutName, fabricFilter.Outlet.SolveState);
         }

         if(hasParticleOut && particleOut is DryingMaterialStream)
         {
            // add the labels
            UserControl ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)particleOut);
            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);

            // add the values
            ProcessStreamBaseControl matOutCtrl = this.FabricFilterCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FabricFilterCtrl.FabricFilter.ParticleOutlet.Name);
            //ctrl = new ProcessVarValuesControl(matOutCtrl);
            ctrl = new MaterialStreamValuesControl((MaterialStreamControl)matOutCtrl);

            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxParticleOutName.SetSolvable(fabricFilter.ParticleOutlet);
            this.groupBoxParticle.Controls.Add(this.textBoxParticleOutName);
            this.textBoxParticleOutName.Text = fabricFilter.ParticleOutlet.Name;
            UI.SetStatusColor(this.textBoxParticleOutName, fabricFilter.ParticleOutlet.SolveState);
         }
      }
   }
}
