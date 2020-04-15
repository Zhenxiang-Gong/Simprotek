using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for ScrubberCondenserEditor.
   /// </summary>
   public class ScrubberCondenserEditor : UnitOpEditor {

      //private ScrubberCondenserRatingEditor ratingEditor;
      private bool inConstruction;

      public ScrubberCondenserControl ScrubberCondenserCtrl {
         get { return (ScrubberCondenserControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private MenuItem menuItemRating;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxScrubberCondenser;
      private ProsimoUI.SolvableNameTextBox textBoxGasOutName;
      private ProsimoUI.SolvableNameTextBox textBoxGasInName;
      private ProsimoUI.SolvableNameTextBox textBoxLiquidOutName;
      private System.Windows.Forms.GroupBox groupBoxGasInletOutlet;
      private System.Windows.Forms.GroupBox groupBoxLiquidOutlet;

      private ProsimoUI.SolvableNameTextBox textBoxWaterOutName;
      private ProsimoUI.SolvableNameTextBox textBoxWaterInName;
      private System.Windows.Forms.GroupBox groupBoxWaterInletOutlet;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ScrubberCondenserEditor(ScrubberCondenserControl scrubberCondenserCtrl)
         : base(scrubberCondenserCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         ScrubberCondenser scrubberCondenser = this.ScrubberCondenserCtrl.ScrubberCondenser;
         this.Text = "Scrubber Condenser: " + scrubberCondenser.Name;

         this.UpdateStreamsUI();

         this.groupBoxScrubberCondenser = new System.Windows.Forms.GroupBox();
         this.groupBoxScrubberCondenser.Location = new System.Drawing.Point(644, 24);
         //this.groupBoxScrubberCondenser.Location = new System.Drawing.Point(1010, 24);
         this.groupBoxScrubberCondenser.Name = "groupBoxScrubberCondenser";
         this.groupBoxScrubberCondenser.Text = "Scrubber Condenser";
         this.groupBoxScrubberCondenser.Size = new System.Drawing.Size(280, 240);
         this.groupBoxScrubberCondenser.TabIndex = 127;
         this.groupBoxScrubberCondenser.TabStop = false;
         this.panel.Controls.Add(this.groupBoxScrubberCondenser);

         this.groupBoxLiquidOutlet.Size = new System.Drawing.Size(280, 300);
         //this.panel.Size = new System.Drawing.Size(1010, 329);
         //this.ClientSize = new System.Drawing.Size(1010, 351);
         //   this.panel.Size = new System.Drawing.Size(1292, 329);
         //   this.ClientSize = new System.Drawing.Size(1292, 351);

         ProcessVarLabelsControl scrubberCondenserLabelsCtrl = new ProcessVarLabelsControl(this.ScrubberCondenserCtrl.ScrubberCondenser.VarList);
         this.groupBoxScrubberCondenser.Controls.Add(scrubberCondenserLabelsCtrl);
         scrubberCondenserLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl scrubberCondenserValuesCtrl = new ProcessVarValuesControl(this.ScrubberCondenserCtrl);
         this.groupBoxScrubberCondenser.Controls.Add(scrubberCondenserValuesCtrl);
         scrubberCondenserValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         scrubberCondenserCtrl.ScrubberCondenser.StreamAttached += new StreamAttachedEventHandler(ScrubberCondenser_StreamAttached);
         scrubberCondenserCtrl.ScrubberCondenser.StreamDetached += new StreamDetachedEventHandler(ScrubberCondenser_StreamDetached);

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
         this.SetCalculationType(this.ScrubberCondenserCtrl.ScrubberCondenser.CalculationType);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.ScrubberCondenserCtrl.ScrubberCondenser != null) {
            this.ScrubberCondenserCtrl.ScrubberCondenser.StreamAttached -= new StreamAttachedEventHandler(ScrubberCondenser_StreamAttached);
            this.ScrubberCondenserCtrl.ScrubberCondenser.StreamDetached -= new StreamDetachedEventHandler(ScrubberCondenser_StreamDetached);
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
         this.groupBoxGasInletOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxGasOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxGasInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxWaterInletOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxWaterOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxWaterInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxLiquidOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxLiquidOutName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxGasInletOutlet.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.groupBoxWaterInletOutlet.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.groupBoxLiquidOutlet.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxGasInletOutlet
         // 
         this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasOutName);
         this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasInName);
         this.groupBoxGasInletOutlet.Location = new System.Drawing.Point(4, 24);
         this.groupBoxGasInletOutlet.Name = "groupBoxGasInletOutlet";
         this.groupBoxGasInletOutlet.Size = new System.Drawing.Size(360, 280);
         this.groupBoxGasInletOutlet.TabIndex = 118;
         this.groupBoxGasInletOutlet.TabStop = false;
         this.groupBoxGasInletOutlet.Text = "Gas Inlet/Outlet";
         // 
         // textBoxGasOutName
         // 
         this.textBoxGasOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxGasOutName.Name = "textBoxGasOutName";
         this.textBoxGasOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasOutName.TabIndex = 7;
         this.textBoxGasOutName.Text = "";
         // 
         // textBoxGasInName
         // 
         this.textBoxGasInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxGasInName.Name = "textBoxGasInName";
         this.textBoxGasInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxGasInName.TabIndex = 6;
         this.textBoxGasInName.Text = "";
         // 
         // groupBoxWaterInletOutlet
         // 
         this.groupBoxWaterInletOutlet.Controls.Add(this.textBoxWaterOutName);
         this.groupBoxWaterInletOutlet.Controls.Add(this.textBoxWaterInName);
         this.groupBoxWaterInletOutlet.Location = new System.Drawing.Point(930, 24);
         this.groupBoxWaterInletOutlet.Name = "groupBoxWaterInletOutlet";
         this.groupBoxWaterInletOutlet.Size = new System.Drawing.Size(360, 3000);
         this.groupBoxWaterInletOutlet.TabIndex = 118;
         this.groupBoxWaterInletOutlet.TabStop = false;
         this.groupBoxWaterInletOutlet.Text = "Water Inlet/Outlet";
         // 
         // textBoxWaterOutName
         // 
         this.textBoxWaterOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxWaterOutName.Name = "textBoxWaterOutName";
         this.textBoxWaterOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxWaterOutName.TabIndex = 7;
         this.textBoxWaterOutName.Text = "";
         // 
         // textBoxWaterInName
         // 
         this.textBoxWaterInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxWaterInName.Name = "textBoxGasInName";
         this.textBoxWaterInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxWaterInName.TabIndex = 6;
         this.textBoxWaterInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxLiquidOutlet);
         this.panel.Controls.Add(this.groupBoxGasInletOutlet);
         this.panel.Controls.Add(this.groupBoxWaterInletOutlet);
         //this.panel.Size = new System.Drawing.Size(930, 329);
         // 
         // groupBoxLiquidOutlet
         // 
         this.groupBoxLiquidOutlet.Controls.Add(this.textBoxLiquidOutName);
         //this.groupBoxLiquidOutlet.Location = new System.Drawing.Point(724, 24);
         this.groupBoxLiquidOutlet.Location = new System.Drawing.Point(364, 24);
         this.groupBoxLiquidOutlet.Name = "groupBoxLiquidOutlet";
         this.groupBoxLiquidOutlet.Size = new System.Drawing.Size(280, 300);
         this.groupBoxLiquidOutlet.TabIndex = 128;
         this.groupBoxLiquidOutlet.TabStop = false;
         this.groupBoxLiquidOutlet.Text = "Liquid Outlet";
         // 
         // textBoxLiquidOutName
         // 
         this.textBoxLiquidOutName.Location = new System.Drawing.Point(196, 12);
         this.textBoxLiquidOutName.Name = "textBoxLiquidOutName";
         this.textBoxLiquidOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxLiquidOutName.TabIndex = 6;
         this.textBoxLiquidOutName.Text = "";
         // 
         // ScrubberCondenserEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(930, 351);
         this.Name = "ScrubberCondenserEditor";
         this.Text = "Scrubber Condenser";
         this.groupBoxGasInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.groupBoxWaterInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.groupBoxLiquidOutlet.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
         ScrubberCondenser scrubberCondenser = this.ScrubberCondenserCtrl.ScrubberCondenser;
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
                  ErrorMessage error = scrubberCondenser.SpecifyName(this.textBoxName.Text);
                  if (error != null)
                     UI.ShowError(error);
               }
            }
         }
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxGasInName);
         list.Add(this.textBoxGasOutName);
         list.Add(this.textBoxLiquidOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void ScrubberCondenser_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
         this.UpdateStreamsUI();
      }

      private void ScrubberCondenser_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI() {
         // clear the streams boxes and start again
         this.groupBoxGasInletOutlet.Controls.Clear();
         this.groupBoxLiquidOutlet.Controls.Clear();
         this.groupBoxWaterInletOutlet.Controls.Clear();

         ScrubberCondenser scrubberCondenser = this.ScrubberCondenserCtrl.ScrubberCondenser;
         bool hasGasIn = false;
         bool hasGasOut = false;
         bool hasLiquidOut = false;
         bool hasWaterIn = false;
         bool hasWaterOut = false;

         ProcessStreamBase gasIn = scrubberCondenser.GasInlet;
         hasGasIn = gasIn != null;

         ProcessStreamBase gasOut = scrubberCondenser.GasOutlet;
         hasGasOut = gasOut != null;

         ProcessStreamBase liquidOut = scrubberCondenser.LiquidOutlet;
         hasLiquidOut = liquidOut != null;

         ProcessStreamBase waterIn = scrubberCondenser.WaterInlet;
         hasWaterIn = waterIn != null;

         ProcessStreamBase waterOut = scrubberCondenser.WaterOutlet;
         hasWaterOut = waterOut != null;

         if (hasGasIn || hasGasOut) {
            ProcessStreamBase labelsStream = hasGasIn ? gasIn : gasOut;

            //UserControl ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            UserControl ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasGasIn) {
            ProcessStreamBaseControl gasInCtrl = this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasInlet.Name);
            //UserControl ctrl = new ProcessVarValuesControl(gasInCtrl);
            UserControl ctrl = new GasStreamValuesControl((GasStreamControl)gasInCtrl);
            
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxGasInName.SetSolvable(scrubberCondenser.GasInlet);
            this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasInName);
            this.textBoxGasInName.Text = scrubberCondenser.GasInlet.Name;
            UI.SetStatusColor(this.textBoxGasInName, scrubberCondenser.GasInlet.SolveState);
         }

         if (hasGasOut) {
            ProcessStreamBaseControl gasOutCtrl = this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasOutlet.Name);
            //UserControl ctrl = new ProcessVarValuesControl(gasOutCtrl);
            UserControl ctrl = new GasStreamValuesControl((GasStreamControl)gasOutCtrl);
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxGasOutName.SetSolvable(scrubberCondenser.GasOutlet);
            this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasOutName);
            this.textBoxGasOutName.Text = scrubberCondenser.GasOutlet.Name;
            UI.SetStatusColor(this.textBoxGasOutName, scrubberCondenser.GasOutlet.SolveState);
         }

         if (hasLiquidOut) {
            // add the labels
            //UserControl ctrl = new ProcessVarLabelsControl(liquidOut.VarList);
            UserControl ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)liquidOut);
            this.groupBoxLiquidOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);

            ProcessStreamBaseControl matOutCtrl = this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.LiquidOutlet.Name);
            //ctrl = new ProcessVarValuesControl(matOutCtrl);
            ctrl = new MaterialStreamValuesControl((MaterialStreamControl)matOutCtrl);
            this.groupBoxLiquidOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxLiquidOutName.SetSolvable(scrubberCondenser.LiquidOutlet);
            this.groupBoxLiquidOutlet.Controls.Add(this.textBoxLiquidOutName);
            this.textBoxLiquidOutName.Text = scrubberCondenser.LiquidOutlet.Name;
            UI.SetStatusColor(this.textBoxLiquidOutName, scrubberCondenser.LiquidOutlet.SolveState);
         }

         if (hasWaterIn || hasWaterOut) {
            UserControl ctrl = null;
            ProcessStreamBase labelsStream = hasWaterIn ? waterIn : waterOut;
            if (labelsStream is WaterStream) {
               ctrl = new ProcessVarLabelsControl(labelsStream.VarList);
            }
            else if (waterIn is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)labelsStream);
            }

            if (ctrl != null) {
               this.groupBoxWaterInletOutlet.Controls.Add(ctrl);
               ctrl.Location = new Point(4, 12 + 20 + 2);
            }

            this.panel.Size = new System.Drawing.Size(1292, 329);
            this.ClientSize = new System.Drawing.Size(1292, 351);
         }
         else {
            this.panel.Size = new System.Drawing.Size(932, 329);
            this.ClientSize = new System.Drawing.Size(932, 351);
         }

         if (hasWaterIn) {
            UserControl ctrl = null;
            ProcessStreamBaseControl waterInCtrl = this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.WaterInlet.Name);
            if (waterIn is WaterStream) {
               ctrl = new ProcessVarValuesControl(waterInCtrl);
            }
            else if (waterIn is DryingMaterialStream) {
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)waterInCtrl);
            }

            if (ctrl != null) {
               this.groupBoxWaterInletOutlet.Controls.Add(ctrl);
               ctrl.Location = new Point(196, 12 + 20 + 2);
            }

            this.textBoxWaterInName.SetSolvable(scrubberCondenser.WaterInlet);
            this.groupBoxWaterInletOutlet.Controls.Add(this.textBoxWaterInName);
            this.textBoxWaterInName.Text = scrubberCondenser.WaterInlet.Name;
            UI.SetStatusColor(this.textBoxWaterInName, scrubberCondenser.WaterInlet.SolveState);
         }

         if (hasWaterOut) {
            UserControl ctrl = null;
            ProcessStreamBaseControl waterOutCtrl = this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.WaterOutlet.Name);
            if (waterOut is WaterStream) {
               ctrl = new ProcessVarValuesControl(waterOutCtrl);
            }
            else if (waterOut is DryingMaterialStream) {
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)waterOutCtrl);
            }

            if (ctrl != null) {
               this.groupBoxWaterInletOutlet.Controls.Add(ctrl);
               ctrl.Location = new Point(276, 12 + 20 + 2);
            }

            this.textBoxWaterOutName.SetSolvable(scrubberCondenser.WaterOutlet);
            this.groupBoxWaterInletOutlet.Controls.Add(this.textBoxWaterOutName);
            this.textBoxWaterOutName.Text = scrubberCondenser.WaterOutlet.Name;
            UI.SetStatusColor(this.textBoxWaterOutName, scrubberCondenser.WaterOutlet.SolveState);
         }
      }

      private void menuItemRating_Click(object sender, EventArgs e) {
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e) {
         if (!this.inConstruction) {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == INDEX_BALANCE) {
               error = this.ScrubberCondenserCtrl.ScrubberCondenser.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null) {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == INDEX_RATING) {
               error = this.ScrubberCondenserCtrl.ScrubberCondenser.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null) {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null) {
               UI.ShowError(error);
               this.SetCalculationType(this.ScrubberCondenserCtrl.ScrubberCondenser.CalculationType);
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
         //this.ratingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
      }
   }
}
