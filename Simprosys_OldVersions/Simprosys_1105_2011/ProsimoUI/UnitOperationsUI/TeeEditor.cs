using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for TeeEditor.
   /// </summary>
   public class TeeEditor : UnitOpEditor {
      private int formHeight = 368;
      private int groupBoxHeight = 280;

      private const int INITIAL_LOCATION = 196;
      private const int VALUE_WIDTH = 80;
      private const int INITIAL_GROUPBOX_WIDTH = 200;
      private const int INITIAL_FORM_WIDTH = 216;

      public TeeControl TeeCtrl {
         get { return (TeeControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private UserControl userCtrlFractions;
      private GroupBox groupBoxFractions;
      private GroupBox groupBoxStreams;
      private Hashtable outletControls;
      private ProsimoUI.SolvableNameTextBox textBoxStreamInName;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public TeeEditor(TeeControl teeCtrl)
         : base(teeCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.Size = new Size(296, this.formHeight);
         this.FormBorderStyle = FormBorderStyle.Sizable;
         this.Text = "Tee";
         this.panel.AutoScroll = true;
         Tee tee = this.TeeCtrl.Tee;
         this.Text = "Tee: " + tee.Name;

         this.userCtrlFractions = new UserControl();
         this.userCtrlFractions.AutoScroll = true;
         this.userCtrlFractions.Size = new Size(242, this.groupBoxHeight - 40);
         this.userCtrlFractions.Location = new Point(4, 12 + 20 + 2);

         this.groupBoxStreams = new GroupBox();
         this.groupBoxStreams.Text = "Inlet/Outlets";
         this.groupBoxStreams.Location = new System.Drawing.Point(4, 24);
         this.groupBoxStreams.Size = new System.Drawing.Size(280, this.groupBoxHeight);
         this.panel.Controls.Add(this.groupBoxStreams);

         this.groupBoxFractions = new GroupBox();
         this.groupBoxFractions.Text = "Outlet Fractions";
         this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
         this.groupBoxFractions.Size = new System.Drawing.Size(250, this.groupBoxHeight);
         this.groupBoxFractions.Controls.Add(this.userCtrlFractions);
         this.panel.Controls.Add(this.groupBoxFractions);

         textBoxStreamInName = new ProsimoUI.SolvableNameTextBox();

         this.outletControls = new Hashtable();

         if (tee.Inlet is DryingGasStream) {
            this.formHeight = 368;
            this.groupBoxHeight = 280;
            this.ClientSize = new System.Drawing.Size(292, 253);
         }
         else if (tee.Inlet is DryingMaterialStream) {
            this.formHeight = 388;
            this.groupBoxHeight = 300;
            this.ClientSize = new System.Drawing.Size(292, 273);
         }
         this.groupBoxStreams.Size = new System.Drawing.Size(360, this.groupBoxHeight);
         this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
         this.groupBoxFractions.Size = new System.Drawing.Size(250, this.groupBoxHeight);

         this.UpdateTheUI();

         teeCtrl.Tee.StreamAttached += new StreamAttachedEventHandler(Tee_StreamAttached);
         teeCtrl.Tee.StreamDetached += new StreamDetachedEventHandler(Tee_StreamDetached);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.TeeCtrl != null && this.TeeCtrl.Tee != null) {
            this.TeeCtrl.Tee.StreamAttached -= new StreamAttachedEventHandler(Tee_StreamAttached);
            this.TeeCtrl.Tee.StreamDetached -= new StreamDetachedEventHandler(Tee_StreamDetached);
         }
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.SuspendLayout();
         // 
         // TeeEditor
         // 
         this.ClientSize = new System.Drawing.Size(292, 273);
         this.Name = "TeeEditor";
         this.ResumeLayout(false);

      }
      #endregion

      private void Tee_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
         this.UpdateTheUI();
      }

      private void Tee_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         this.UpdateTheUI();
      }

      private void UpdateTheUI() {
         this.panel.Visible = false;

         this.userCtrlFractions.Visible = false;
         this.userCtrlFractions.Controls.Clear();

         this.groupBoxStreams.Visible = false;
         this.groupBoxStreams.Controls.Clear();
         this.outletControls.Clear();

         int x = INITIAL_LOCATION;
         int w = VALUE_WIDTH;
         int s = INITIAL_GROUPBOX_WIDTH;
         int p = INITIAL_FORM_WIDTH;

         Tee tee = this.TeeCtrl.Tee;
         bool hasStreamIn = false;
         bool hasStreamOut = false;

         ProcessStreamBase streamOut = null;
         if (tee.OutletStreams.Count > 0) {
            hasStreamOut = true;
            streamOut = (ProcessStreamBase)tee.OutletStreams[0];
            this.userCtrlFractions.Visible = true;
         }

         ProcessStreamBase streamIn = tee.Inlet;
         hasStreamIn = streamIn != null;

         if (hasStreamIn || hasStreamOut) {
            this.groupBoxStreams.Visible = true;
            ProcessStreamBase labelsStream = hasStreamIn ? streamIn : streamOut;
            UserControl ctrl = null;
            if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            else if (labelsStream is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }
            //UserControl ctrl = new ProcessVarLabelsControl(labelsStream.VarList); ;

            ctrl.Location = new Point(4, 12 + 20 + 2);
            this.groupBoxStreams.Size = new Size(s, this.groupBoxHeight);
            this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
            this.groupBoxStreams.Controls.Add(ctrl);
            s = s + w;
            p = p + w;
         }

         if (hasStreamIn) {
            UserControl ctrl = null;
            if (streamIn is DryingGasStream) {
               GasStreamControl gasInCtrl = (GasStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            else if (streamIn is DryingMaterialStream) {
               MaterialStreamControl materialInCtrl = (MaterialStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
               ctrl = new MaterialStreamValuesControl(materialInCtrl);
            }
            //ProcessStreamBaseControl baseCtrl = this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
            //UserControl ctrl = new ProcessVarValuesControl(baseCtrl);

            ctrl.Location = new Point(x, 12 + 20 + 2);
            this.groupBoxStreams.Size = new Size(s, this.groupBoxHeight);
            this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
            this.groupBoxStreams.Controls.Add(ctrl);
            this.Size = new Size(p, this.formHeight);

            this.textBoxStreamInName.Text = streamIn.Name;
            this.textBoxStreamInName.Location = new Point(x, 12);

            this.textBoxStreamInName.SetSolvable(streamIn);
            this.groupBoxStreams.Controls.Add(this.textBoxStreamInName);
            UI.SetStatusColor(this.textBoxStreamInName, streamIn.SolveState);
         }

         if (hasStreamOut) {
            IEnumerator e = tee.OutletStreams.GetEnumerator();
            while (e.MoveNext()) {
               s = s + w;
               x = x + w;
               p = p + w;

               ProcessStreamBase processStreamBase = (ProcessStreamBase)e.Current;
               UserControl ctrl = null;
               if (processStreamBase is DryingGasStream) {
                  GasStreamControl gasOutCtrl = (GasStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
                  ctrl = new GasStreamValuesControl(gasOutCtrl);
               }
               else if (processStreamBase is DryingMaterialStream) {
                  MaterialStreamControl materialOutCtrl = (MaterialStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
                  ctrl = new MaterialStreamValuesControl(materialOutCtrl);
               }

               //ProcessStreamBaseControl baseCtrl = this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
               //UserControl ctrl = new ProcessVarValuesControl(baseCtrl);

               ctrl.Location = new Point(x, 12 + 20 + 2);
               this.groupBoxStreams.Size = new Size(s, this.groupBoxHeight);
               this.groupBoxFractions.Location = new System.Drawing.Point(this.groupBoxStreams.Width + 4, 24);
               this.groupBoxStreams.Controls.Add(ctrl);
               this.Size = new Size(p, this.formHeight);

               ProsimoUI.SolvableNameTextBox textBoxStreamOutName = new ProsimoUI.SolvableNameTextBox(processStreamBase);
               textBoxStreamOutName.Width = 80;
               textBoxStreamOutName.Height = 20;
               textBoxStreamOutName.Text = processStreamBase.Name;
               textBoxStreamOutName.Location = new Point(x, 12);
               this.outletControls.Add(textBoxStreamOutName, textBoxStreamOutName.Text);

               this.groupBoxStreams.Controls.Add(textBoxStreamOutName);
               UI.SetStatusColor(textBoxStreamOutName, processStreamBase.SolveState);
            }

            // build the fractions
            for (int i = 0; i < this.TeeCtrl.Tee.OutletStreamAndFractions.Count; i++) {
               StreamAndFraction sf = (StreamAndFraction)this.TeeCtrl.Tee.OutletStreamAndFractions[i];
               TeeStreamAndFractionControl sfCtrl = new TeeStreamAndFractionControl(this.TeeCtrl.Flowsheet, sf);
               sfCtrl.Location = new Point(0, i * sfCtrl.Height);
               this.userCtrlFractions.Controls.Add(sfCtrl);
               sfCtrl.textBoxFraction.KeyUp += new KeyEventHandler(KeyUpNavigator);
            }
         }
         this.Size = new Size(p + this.groupBoxFractions.Width, this.formHeight);
         this.panel.Visible = true;
      }

      private void KeyUpNavigator(object sender, KeyEventArgs e) {
         ArrayList list = new ArrayList();
         IEnumerator en = this.userCtrlFractions.Controls.GetEnumerator();
         while (en.MoveNext()) {
            TeeStreamAndFractionControl sfCtrl = (TeeStreamAndFractionControl)en.Current;
            list.Add(sfCtrl.textBoxFraction);
         }

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this.userCtrlFractions, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this.userCtrlFractions, KeyboardNavigation.Up);
         }
      }
   }
}

//if (hasStreamIn)
//   labelsStream = streamIn;
//else if (hasStreamOut)
//   labelsStream = streamOut;

//if (labelsStream is DryingGasStream) {
//   ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
//}
//else if (labelsStream is DryingMaterialStream) {
//   ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
//}

//if (streamIn is DryingGasStream) {
//   GasStreamControl gasInCtrl = (GasStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
//   ctrl = new GasStreamValuesControl(gasInCtrl);
//}
//else if (streamIn is DryingMaterialStream) {
//   MaterialStreamControl materialInCtrl = (MaterialStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(streamIn.Name);
//   ctrl = new MaterialStreamValuesControl(materialInCtrl);
//}
//if (processStreamBase is DryingGasStream) {
//   GasStreamControl gasOutCtrl = (GasStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
//   ctrl = new GasStreamValuesControl(gasOutCtrl);
//}
//else if (processStreamBase is DryingMaterialStream) {
//   MaterialStreamControl materialOutCtrl = (MaterialStreamControl)this.TeeCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(processStreamBase.Name);
//   ctrl = new MaterialStreamValuesControl(materialOutCtrl);
//}
               

            


