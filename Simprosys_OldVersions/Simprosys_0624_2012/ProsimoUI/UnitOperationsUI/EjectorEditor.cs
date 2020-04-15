using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for EjectorEditor.
   /// </summary>
   public class EjectorEditor : UnitOpEditor {

      //      private EjectorRatingEditor ratingEditor;
      private bool inConstruction;

      public EjectorControl EjectorCtrl {
         get { return (EjectorControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private MenuItem menuItemRating;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxEjector;
      private ProsimoUI.SolvableNameTextBox textBoxSuctionInName;
      private ProsimoUI.SolvableNameTextBox textBoxMotiveInName;
      private ProsimoUI.SolvableNameTextBox textBoxDischargeOutName;
      private System.Windows.Forms.GroupBox groupBoxMotiveSuction;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public EjectorEditor(EjectorControl ejectorCtrl)
         : base(ejectorCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         Ejector ejector = this.EjectorCtrl.Ejector;
         this.Text = "Ejector: " + ejector.Name;

         this.UpdateStreamsUI();

         this.groupBoxEjector = new System.Windows.Forms.GroupBox();
         this.groupBoxEjector.Location = new System.Drawing.Point(452, 24);
         this.groupBoxEjector.Name = "groupBoxEjector";
         this.groupBoxEjector.Text = "Ejector";
         this.groupBoxEjector.Size = new System.Drawing.Size(280, 100);
         this.groupBoxEjector.TabIndex = 127;
         this.groupBoxEjector.TabStop = false;
         this.panel.Controls.Add(this.groupBoxEjector);

         //         this.panel.Size = new System.Drawing.Size(930, 329);
         //         this.ClientSize = new System.Drawing.Size(930, 351);

         //EjectorLabelsControl ejectorLabelsCtrl = new EjectorLabelsControl(this.EjectorCtrl.Ejector);
         ProcessVarLabelsControl ejectorLabelsCtrl = new ProcessVarLabelsControl(this.EjectorCtrl.Ejector.VarList);
         this.groupBoxEjector.Controls.Add(ejectorLabelsCtrl);
         ejectorLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //EjectorValuesControl ejectorValuesCtrl = new EjectorValuesControl(this.EjectorCtrl);
         ProcessVarValuesControl ejectorValuesCtrl = new ProcessVarValuesControl(this.EjectorCtrl);
         this.groupBoxEjector.Controls.Add(ejectorValuesCtrl);
         ejectorValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         ejectorCtrl.Ejector.StreamAttached += new StreamAttachedEventHandler(Ejector_StreamAttached);
         ejectorCtrl.Ejector.StreamDetached += new StreamDetachedEventHandler(Ejector_StreamDetached);

         this.menuItemRating = new MenuItem();
         this.menuItemRating.Index = this.menuItemReport.Index + 1;
         this.menuItemRating.Text = "Rating";
         this.menuItemRating.Click += new EventHandler(menuItemRating_Click);
         this.mainMenu.MenuItems.Add(this.menuItemRating);

         this.labelCalculationType = new System.Windows.Forms.Label();
         this.comboBoxCalculationType = new System.Windows.Forms.ComboBox();

         // labelCalculationType
         this.labelCalculationType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelCalculationType.Location = new System.Drawing.Point(300, 0);
         this.labelCalculationType.Name = "labelCalculationType";
         this.labelCalculationType.BackColor = Color.DarkGray;
         this.labelCalculationType.Size = new System.Drawing.Size(192, 20);
         this.labelCalculationType.TabIndex = 5;
         this.labelCalculationType.Text = "Calculation Type:";
         this.labelCalculationType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

         // comboBoxCalculationType
         this.comboBoxCalculationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxCalculationType.Items.AddRange(new object[] {
                                                                     "Balance",
                                                                     "Rating"});
         this.comboBoxCalculationType.Location = new System.Drawing.Point(492, 0);
         this.comboBoxCalculationType.Name = "comboBoxCalculationType";
         this.comboBoxCalculationType.Size = new System.Drawing.Size(80, 21);
         this.comboBoxCalculationType.TabIndex = 7;
         this.comboBoxCalculationType.SelectedIndexChanged += new EventHandler(comboBoxCalculationType_SelectedIndexChanged);

         this.panel.Controls.Add(this.labelCalculationType);
         this.panel.Controls.Add(this.comboBoxCalculationType);

         this.comboBoxCalculationType.SelectedIndex = -1;
         comboBoxCalculationType.Enabled = false; // TODO: remove later
         this.inConstruction = false;
         this.SetCalculationType(this.EjectorCtrl.Ejector.CalculationType);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.EjectorCtrl.Ejector != null) {
            this.EjectorCtrl.Ejector.StreamAttached -= new StreamAttachedEventHandler(Ejector_StreamAttached);
            this.EjectorCtrl.Ejector.StreamDetached -= new StreamDetachedEventHandler(Ejector_StreamDetached);
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
         this.groupBoxMotiveSuction = new System.Windows.Forms.GroupBox();
         this.textBoxSuctionInName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxMotiveInName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxDischargeOutName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxMotiveSuction.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxMotiveSuction
         // 
         this.groupBoxMotiveSuction.Controls.Add(this.textBoxSuctionInName);
         this.groupBoxMotiveSuction.Controls.Add(this.textBoxMotiveInName);
         this.groupBoxMotiveSuction.Location = new System.Drawing.Point(4, 24);
         this.groupBoxMotiveSuction.Name = "groupBoxMotiveSuction";
         this.groupBoxMotiveSuction.Size = new System.Drawing.Size(440, 300);
         this.groupBoxMotiveSuction.TabIndex = 118;
         this.groupBoxMotiveSuction.TabStop = false;
         this.groupBoxMotiveSuction.Text = "Motive/Suction Inlets, Discharge Outlet";
         // 
         // textBoxSuctionInName
         // 
         this.textBoxSuctionInName.Location = new System.Drawing.Point(276, 12);
         this.textBoxSuctionInName.Name = "textBoxSuctionInName";
         this.textBoxSuctionInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxSuctionInName.TabIndex = 7;
         this.textBoxSuctionInName.Text = "";
         // 
         // textBoxMotiveInName
         // 
         this.textBoxMotiveInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxMotiveInName.Name = "textBoxMotiveInName";
         this.textBoxMotiveInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxMotiveInName.TabIndex = 6;
         this.textBoxMotiveInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxMotiveSuction);
         this.panel.Size = new System.Drawing.Size(930, 329);
         // 
         // textBoxDischargeOutName
         // 
         this.textBoxDischargeOutName.Location = new System.Drawing.Point(356, 12);
         this.textBoxDischargeOutName.Name = "textBoxDischargeOutName";
         this.textBoxDischargeOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxDischargeOutName.TabIndex = 6;
         this.textBoxDischargeOutName.Text = "";
         // 
         // EjectorEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(738, 351);
         this.Name = "EjectorEditor";
         this.Text = "Ejector";
         this.groupBoxMotiveSuction.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
         Ejector ejector = this.EjectorCtrl.Ejector;
         TextBox tb = (TextBox)sender;
         if (tb.Text != null) {
            if (tb.Text.Trim().Equals("")) {
               if (sender == this.textBoxName) {
                  e.Cancel = true;
                  string message3 = "Please specify a name!";
                  MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
            }
            else {
               if (sender == this.textBoxName) {
                  ErrorMessage error = ejector.SpecifyName(this.textBoxName.Text);
                  if (error != null)
                     UI.ShowError(error);
               }
            }
         }
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxMotiveInName);
         list.Add(this.textBoxSuctionInName);
         list.Add(this.textBoxDischargeOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void Ejector_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
         this.UpdateStreamsUI();
      }

      private void Ejector_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI() {
         // clear the streams boxes and start again
         this.groupBoxMotiveSuction.Controls.Clear();

         Ejector ejector = this.EjectorCtrl.Ejector;
         bool hasMotiveIn = false;
         bool hasSuctionIn = false;
         bool hasDischargeOut = false;

         ProcessStreamBase motiveIn = ejector.MotiveInlet;
         hasMotiveIn = motiveIn != null;

         ProcessStreamBase suctionIn = ejector.SuctionInlet;
         hasSuctionIn = suctionIn != null;

         ProcessStreamBase dischargeOut = ejector.DischargeOutlet;
         hasDischargeOut = dischargeOut != null;

         if (hasMotiveIn || hasSuctionIn || hasDischargeOut) {
            ProcessStreamBase labelsStream = null;
            if (hasMotiveIn)
               labelsStream = motiveIn;
            else if (hasSuctionIn)
               labelsStream = suctionIn;
            else if (hasDischargeOut)
               labelsStream = dischargeOut;

            UserControl ctrl = null;
            if (labelsStream is WaterStream) {
               ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            }
            else if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            else if (labelsStream is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }
            //UserControl ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            this.groupBoxMotiveSuction.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasMotiveIn) {
            ProcessStreamBaseControl baseCtrl = this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.MotiveInlet.Name);
            UserControl ctrl = null;
            if (motiveIn is WaterStream) {
               ctrl = new ProcessVarValuesControl(baseCtrl);
            }
            else if (motiveIn is DryingGasStream) {
               ctrl = new GasStreamValuesControl((GasStreamControl)baseCtrl);
            }
            else if (motiveIn is DryingMaterialStream) {
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)baseCtrl);
            }
            //UserControl ctrl = new ProcessVarValuesControl(baseCtrl);

            this.groupBoxMotiveSuction.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxMotiveInName.SetSolvable(ejector.MotiveInlet);
            this.groupBoxMotiveSuction.Controls.Add(this.textBoxMotiveInName);
            this.textBoxMotiveInName.Text = ejector.MotiveInlet.Name;
            UI.SetStatusColor(this.textBoxMotiveInName, ejector.MotiveInlet.SolveState);
         }

         if (hasSuctionIn) {
            ProcessStreamBaseControl baseCtrl = this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.SuctionInlet.Name);
            UserControl ctrl = null;
            if (suctionIn is WaterStream) {
               ctrl = new ProcessVarValuesControl(baseCtrl);
            }
            else if (suctionIn is DryingGasStream) {
               ctrl = new GasStreamValuesControl((GasStreamControl)baseCtrl);
            }
            else if (suctionIn is DryingMaterialStream) {
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)baseCtrl);
            }
            //UserControl ctrl = new ProcessVarValuesControl(baseCtrl);
            
            this.groupBoxMotiveSuction.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxSuctionInName.SetSolvable(ejector.SuctionInlet);
            this.groupBoxMotiveSuction.Controls.Add(this.textBoxSuctionInName);
            this.textBoxSuctionInName.Text = ejector.SuctionInlet.Name;
            UI.SetStatusColor(this.textBoxSuctionInName, ejector.SuctionInlet.SolveState);
         }

         if (hasDischargeOut) {
            ProcessStreamBaseControl baseCtrl = this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.DischargeOutlet.Name);
            UserControl ctrl = null;
            if (dischargeOut is WaterStream) {
               ctrl = new ProcessVarValuesControl(baseCtrl);
            }
            else if (dischargeOut is DryingMaterialStream) {
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)baseCtrl);
            }
            else if (dischargeOut is DryingGasStream) {
               ctrl = new GasStreamValuesControl((GasStreamControl)baseCtrl);
            }
            //UserControl ctrl = new ProcessVarValuesControl(baseCtrl);
            this.groupBoxMotiveSuction.Controls.Add(ctrl);
            ctrl.Location = new Point(356, 12 + 20 + 2);

            this.textBoxDischargeOutName.SetSolvable(ejector.DischargeOutlet);
            this.groupBoxMotiveSuction.Controls.Add(this.textBoxDischargeOutName);
            this.textBoxDischargeOutName.Text = ejector.DischargeOutlet.Name;
            UI.SetStatusColor(this.textBoxDischargeOutName, ejector.DischargeOutlet.SolveState);
         }
      }

      private void menuItemRating_Click(object sender, EventArgs e) {
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e) {
         if (!this.inConstruction) {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == INDEX_BALANCE) {
               error = this.EjectorCtrl.Ejector.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null) {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == INDEX_RATING) {
               error = this.EjectorCtrl.Ejector.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null) {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null) {
               UI.ShowError(error);
               this.SetCalculationType(this.EjectorCtrl.Ejector.CalculationType);
            }
         }
      }

      public void SetCalculationType(UnitOpCalculationType type) {
         if (type == UnitOpCalculationType.Balance)
            this.comboBoxCalculationType.SelectedIndex = INDEX_BALANCE;
         else if (type == UnitOpCalculationType.Rating)
            this.comboBoxCalculationType.SelectedIndex = INDEX_RATING;
      }

      private void ratingEditor_Closed(object sender, EventArgs e) {
         //         this.ratingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
      }
   }
}

//if (labelsStream is DryingGasStream) {
//   ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
//}
//else if (labelsStream is DryingMaterialStream) {
//   ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
//}
//UserControl ctrl = null;
//if (motiveIn is DryingGasStream) {
//   GasStreamControl gasInCtrl = (GasStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.MotiveInlet.Name);
//   ctrl = new GasStreamValuesControl(gasInCtrl);
//}
//else if (motiveIn is DryingMaterialStream) {
//   MaterialStreamControl materialInCtrl = (MaterialStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.MotiveInlet.Name);
//   ctrl = new MaterialStreamValuesControl(materialInCtrl);
//}
//UserControl ctrl = null;
//if (suctionIn is DryingGasStream) {
//   GasStreamControl gasInCtrl = (GasStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.SuctionInlet.Name);
//   ctrl = new GasStreamValuesControl(gasInCtrl);
//}
//else if (suctionIn is DryingMaterialStream) {
//   MaterialStreamControl materialInCtrl = (MaterialStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.SuctionInlet.Name);
//   ctrl = new MaterialStreamValuesControl(materialInCtrl);
//}
//UserControl ctrl = null;
//if (dischargeOut is DryingMaterialStream) {
//   MaterialStreamControl matOutCtrl = (MaterialStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.DischargeOutlet.Name);
//   ctrl = new MaterialStreamValuesControl(matOutCtrl);
//}
//else if (dischargeOut is DryingGasStream) {
//   GasStreamControl gasOutCtrl = (GasStreamControl)this.EjectorCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.EjectorCtrl.Ejector.DischargeOutlet.Name);
//   ctrl = new GasStreamValuesControl(gasOutCtrl);
//}

