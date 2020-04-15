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
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for FlashTankEditor.
	/// </summary>
	public class FlashTankEditor : UnitOpEditor
	{
      public FlashTankControl FlashTankCtrl
      {
         get {return (FlashTankControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

      private ProsimoUI.SolvableNameTextBox textBoxVaporOutName;
      private ProsimoUI.SolvableNameTextBox textBoxInName;
      private ProsimoUI.SolvableNameTextBox textBoxLiquidOutName;
      private System.Windows.Forms.GroupBox groupBoxStreams;
      
      /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FlashTankEditor(FlashTankControl flashTankCtrl) : base(flashTankCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         FlashTank flashTank = this.FlashTankCtrl.FlashTank;
         this.Text = "Flash Tank: " + flashTank.Name;
         
         this.UpdateStreamsUI();

         if (flashTank.Inlet is DryingGasStream)
         {
            this.groupBoxStreams.Size = new System.Drawing.Size(440, 280);
            this.panel.Size = new System.Drawing.Size(448, 269);
            this.ClientSize = new System.Drawing.Size(448, 331);
         }
         else if (flashTank.Inlet is DryingMaterialStream)
         {
            this.groupBoxStreams.Size = new System.Drawing.Size(440, 300);
            this.panel.Size = new System.Drawing.Size(448, 289);
            this.ClientSize = new System.Drawing.Size(448, 351);
         }

         flashTankCtrl.FlashTank.StreamAttached += new StreamAttachedEventHandler(FlashTank_StreamAttached);
         flashTankCtrl.FlashTank.StreamDetached += new StreamDetachedEventHandler(FlashTank_StreamDetached);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.FlashTankCtrl.FlashTank != null)
         {
            this.FlashTankCtrl.FlashTank.StreamAttached -= new StreamAttachedEventHandler(FlashTank_StreamAttached);
            this.FlashTankCtrl.FlashTank.StreamDetached -= new StreamDetachedEventHandler(FlashTank_StreamDetached);
         }

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
         this.textBoxLiquidOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxVaporOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxInName = new ProsimoUI.SolvableNameTextBox();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.groupBoxStreams.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxStreams
         // 
         this.groupBoxStreams.Controls.Add(this.textBoxLiquidOutName);
         this.groupBoxStreams.Controls.Add(this.textBoxVaporOutName);
         this.groupBoxStreams.Controls.Add(this.textBoxInName);
         this.groupBoxStreams.Location = new System.Drawing.Point(4, 24);
         this.groupBoxStreams.Name = "groupBoxStreams";
         this.groupBoxStreams.Size = new System.Drawing.Size(440, 300);
         this.groupBoxStreams.TabIndex = 118;
         this.groupBoxStreams.TabStop = false;
         this.groupBoxStreams.Text = "Inlet/Vapor Outlet/Liquid Outlet";
         // 
         // textBoxLiquidOutName
         // 
         this.textBoxLiquidOutName.Location = new System.Drawing.Point(356, 12);
         this.textBoxLiquidOutName.Name = "textBoxLiquidOutName";
         this.textBoxLiquidOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidOutName.TabIndex = 6;
         this.textBoxLiquidOutName.Text = "";
         // 
         // textBoxVaporOutName
         // 
         this.textBoxVaporOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxVaporOutName.Name = "textBoxVaporOutName";
         this.textBoxVaporOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxVaporOutName.TabIndex = 7;
         this.textBoxVaporOutName.Text = "";
         // 
         // textBoxInName
         // 
         this.textBoxInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxInName.Name = "textBoxInName";
         this.textBoxInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxInName.TabIndex = 6;
         this.textBoxInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxStreams);
         this.panel.Size = new System.Drawing.Size(448, 289);
         // 
         // FlashTankEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(448, 351);
         this.Name = "FlashTankEditor";
         this.Text = "FlashTank";
         this.groupBoxStreams.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
		#endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         FlashTank flashTank = this.FlashTankCtrl.FlashTank;
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
                  ErrorMessage error = flashTank.SpecifyName(this.textBoxName.Text);
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
         list.Add(this.textBoxInName);
         list.Add(this.textBoxVaporOutName);
         list.Add(this.textBoxLiquidOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up)
         {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void FlashTank_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void FlashTank_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxStreams.Controls.Clear();
         
         FlashTank flashTank = this.FlashTankCtrl.FlashTank;
         bool hasInlet = false;
         bool hasVaporOut = false;
         bool hasLiquidOut = false;

         ProcessStreamBase inlet = flashTank.Inlet;
         if (inlet != null)
            hasInlet = true;

         ProcessStreamBase vaporOut = flashTank.VaporOutlet;
         if (vaporOut != null)
            hasVaporOut = true;

         ProcessStreamBase liquidOut = flashTank.LiquidOutlet;
         if (liquidOut != null)
            hasLiquidOut = true;

         if (hasInlet || hasVaporOut || hasLiquidOut)
         {
            ProcessStreamBase labelsStream = null;
            if (hasInlet)
               labelsStream = inlet;
            else if (hasVaporOut)
               labelsStream = vaporOut;
            else if (hasLiquidOut)
               labelsStream = liquidOut;
            
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

         if (hasInlet)
         {
            UserControl ctrl = null;
            if (inlet is ProcessStream)
            {
               ProcessStreamControl processInCtrl = (ProcessStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.Inlet.Name);
               ctrl = new ProcessStreamValuesControl(processInCtrl);
            }
            else if (inlet is DryingGasStream)
            {
               GasStreamControl gasInCtrl = (GasStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.Inlet.Name);
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            else if (inlet is DryingMaterialStream)
            {
               MaterialStreamControl matInCtrl = (MaterialStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.Inlet.Name);
               ctrl = new MaterialStreamValuesControl(matInCtrl);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxInName.SetSolvable(flashTank.Inlet);
            this.groupBoxStreams.Controls.Add(this.textBoxInName);
            this.textBoxInName.Text = flashTank.Inlet.Name;
            UI.SetStatusColor(this.textBoxInName, flashTank.Inlet.SolveState);
         }

         if (hasVaporOut)
         {
            UserControl ctrl = null;
            if (vaporOut is ProcessStream)
            {
               ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.VaporOutlet.Name);
               ctrl = new ProcessStreamValuesControl(processOutCtrl);
            }
            else if (vaporOut is DryingGasStream)
            {
               GasStreamControl gasOutCtrl = (GasStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.VaporOutlet.Name);
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            else if (vaporOut is DryingMaterialStream)
            {
               MaterialStreamControl matOutCtrl = (MaterialStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.VaporOutlet.Name);
               ctrl = new MaterialStreamValuesControl(matOutCtrl);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxVaporOutName.SetSolvable(flashTank.VaporOutlet);
            this.groupBoxStreams.Controls.Add(this.textBoxVaporOutName);
            this.textBoxVaporOutName.Text = flashTank.VaporOutlet.Name;
            UI.SetStatusColor(this.textBoxVaporOutName, flashTank.VaporOutlet.SolveState);
         }

         if (hasLiquidOut)
         {
            UserControl ctrl = null;
            if (liquidOut is ProcessStream)
            {
               ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.LiquidOutlet.Name);
               ctrl = new ProcessStreamValuesControl(processOutCtrl);
            }
            else if (liquidOut is DryingGasStream)
            {
               GasStreamControl gasOutCtrl = (GasStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.LiquidOutlet.Name);
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            else if (liquidOut is DryingMaterialStream)
            {
               MaterialStreamControl matOutCtrl = (MaterialStreamControl)this.FlashTankCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.FlashTankCtrl.FlashTank.LiquidOutlet.Name);
               ctrl = new MaterialStreamValuesControl(matOutCtrl);
            }
            this.groupBoxStreams.Controls.Add(ctrl);
            ctrl.Location = new Point(356, 12 + 20 + 2);

            this.textBoxLiquidOutName.SetSolvable(flashTank.LiquidOutlet);
            this.groupBoxStreams.Controls.Add(this.textBoxLiquidOutName);
            this.textBoxLiquidOutName.Text = flashTank.LiquidOutlet.Name;
            UI.SetStatusColor(this.textBoxLiquidOutName, flashTank.LiquidOutlet.SolveState);
         }
      }
   }
}
