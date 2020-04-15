using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.HeatTransfer;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for BurnerEditor.
   /// </summary>
   public class BurnerEditor : UnitOpEditor {

      //private BurnerRatingEditor ratingEditor;
      private bool inConstruction;

      public BurnerControl BurnerCtrl {
         get { return (BurnerControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private MenuItem menuItemRating;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxBurner;
      private ProsimoUI.SolvableNameTextBox textBoxFuelInName;
      private ProsimoUI.SolvableNameTextBox textBoxAirInName;
      private ProsimoUI.SolvableNameTextBox textBoxFlueGasOutName;
      private System.Windows.Forms.GroupBox groupBoxAirInletOutlet;

      private System.Windows.Forms.GroupBox groupBoxFuelFlueGasInletOutlet;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public BurnerEditor(BurnerControl bCtrl)
         : base(bCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         Burner b = this.BurnerCtrl.Burner;
         this.Text = "Fired Heater: " + b.Name;

         this.UpdateStreamsUI();

         this.groupBoxBurner = new System.Windows.Forms.GroupBox();
         this.groupBoxBurner.Location = new System.Drawing.Point(644, 24);
         //this.groupBoxBurner.Location = new System.Drawing.Point(1010, 24);
         this.groupBoxBurner.Name = "groupBoxBurner";
         this.groupBoxBurner.Text = "Scrubber Condenser";
         this.groupBoxBurner.Size = new System.Drawing.Size(280, 240);
         this.groupBoxBurner.TabIndex = 127;
         this.groupBoxBurner.TabStop = false;
         this.panel.Controls.Add(this.groupBoxBurner);

         ProcessVarLabelsControl bLabelsCtrl = new ProcessVarLabelsControl(this.BurnerCtrl.Burner.VarList);
         this.groupBoxBurner.Controls.Add(bLabelsCtrl);
         bLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl bValuesCtrl = new ProcessVarValuesControl(this.BurnerCtrl);
         this.groupBoxBurner.Controls.Add(bValuesCtrl);
         bValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         bCtrl.Burner.StreamAttached += new StreamAttachedEventHandler(Burner_StreamAttached);
         bCtrl.Burner.StreamDetached += new StreamDetachedEventHandler(Burner_StreamDetached);

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
         this.SetCalculationType(this.BurnerCtrl.Burner.CalculationType);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.BurnerCtrl.Burner != null) {
            this.BurnerCtrl.Burner.StreamAttached -= new StreamAttachedEventHandler(Burner_StreamAttached);
            this.BurnerCtrl.Burner.StreamDetached -= new StreamDetachedEventHandler(Burner_StreamDetached);
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
         this.groupBoxAirInletOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxAirInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxFuelFlueGasInletOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxFlueGasOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxFuelInName = new ProsimoUI.SolvableNameTextBox();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.groupBoxFuelFlueGasInletOutlet.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxGasInletOutlet
         // 
         this.groupBoxAirInletOutlet.Controls.Add(this.textBoxAirInName);
         this.groupBoxAirInletOutlet.Location = new System.Drawing.Point(4, 24);
         this.groupBoxAirInletOutlet.Name = "groupBoxGasInletOutlet";
         this.groupBoxAirInletOutlet.Size = new System.Drawing.Size(360, 280);
         this.groupBoxAirInletOutlet.TabIndex = 118;
         this.groupBoxAirInletOutlet.TabStop = false;
         this.groupBoxAirInletOutlet.Text = "Gas Inlet/Outlet";
         // 
         // textBoxGasInName
         // 
         this.textBoxAirInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxAirInName.Name = "textBoxGasInName";
         this.textBoxAirInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxAirInName.TabIndex = 6;
         this.textBoxAirInName.Text = "";
         // 
         // groupBoxWaterInletOutlet
         // 
         this.groupBoxFuelFlueGasInletOutlet.Controls.Add(this.textBoxFlueGasOutName);
         this.groupBoxFuelFlueGasInletOutlet.Controls.Add(this.textBoxFuelInName);
         this.groupBoxFuelFlueGasInletOutlet.Location = new System.Drawing.Point(930, 24);
         this.groupBoxFuelFlueGasInletOutlet.Name = "groupBoxWaterInletOutlet";
         this.groupBoxFuelFlueGasInletOutlet.Size = new System.Drawing.Size(360, 3000);
         this.groupBoxFuelFlueGasInletOutlet.TabIndex = 118;
         this.groupBoxFuelFlueGasInletOutlet.TabStop = false;
         this.groupBoxFuelFlueGasInletOutlet.Text = "Water Inlet/Outlet";
         // 
         // textBoxWaterOutName
         // 
         this.textBoxFlueGasOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxFlueGasOutName.Name = "textBoxWaterOutName";
         this.textBoxFlueGasOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxFlueGasOutName.TabIndex = 7;
         this.textBoxFlueGasOutName.Text = "";
         // 
         // textBoxWaterInName
         // 
         this.textBoxFuelInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxFuelInName.Name = "textBoxGasInName";
         this.textBoxFuelInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxFuelInName.TabIndex = 6;
         this.textBoxFuelInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxAirInletOutlet);
         this.panel.Controls.Add(this.groupBoxFuelFlueGasInletOutlet);
         //this.panel.Size = new System.Drawing.Size(930, 329);
         // 
         // 
         // FiredHeaterEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(930, 351);
         this.Name = "ScrubberCondenserEditor";
         this.Text = "Scrubber Condenser";
         this.groupBoxAirInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.groupBoxFuelFlueGasInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
         Burner b = this.BurnerCtrl.Burner;
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
                  ErrorMessage error = b.SpecifyName(this.textBoxName.Text);
                  if (error != null)
                     UI.ShowError(error);
               }
            }
         }
      }

      protected override void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e) {
         ArrayList list = new ArrayList();
         list.Add(this.textBoxName);
         list.Add(this.textBoxAirInName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void Burner_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
         this.UpdateStreamsUI();
      }

      private void Burner_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI() {
         // clear the streams boxes and start again
         this.groupBoxAirInletOutlet.Controls.Clear();
         this.groupBoxFuelFlueGasInletOutlet.Controls.Clear();

         Burner burner = this.BurnerCtrl.Burner;
         bool hasAirIn = false;
         bool hasFuelIn = false;
         bool hasFlueGasOut = false;

         ProcessStreamBase airIn = burner.AirInlet;
         hasAirIn = airIn != null;

         ProcessStreamBase fuelIn = burner.FuelInlet;
         hasFuelIn = fuelIn != null;

         ProcessStreamBase flueGasOut = burner.FlueGasOutlet;
         hasFlueGasOut = flueGasOut != null;

         //TODO......

      }

      private void menuItemRating_Click(object sender, EventArgs e) {
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e) {
         if (!this.inConstruction) {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == BurnerEditor.INDEX_BALANCE) {
               error = this.BurnerCtrl.Burner.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null) {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == BurnerEditor.INDEX_RATING) {
               error = this.BurnerCtrl.Burner.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null) {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null) {
               UI.ShowError(error);
               this.SetCalculationType(this.BurnerCtrl.Burner.CalculationType);
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
