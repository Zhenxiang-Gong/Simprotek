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
   /// Summary description for FiredHeaterEditor.
   /// </summary>
   public class FiredHeaterEditor : UnitOpEditor {

      //private FiredHeaterRatingEditor ratingEditor;
      private bool inConstruction;

      public FiredHeaterControl FiredHeaterCtrl {
         get { return (FiredHeaterControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private MenuItem menuItemRating;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxFiredHeater;
      private ProsimoUI.SolvableNameTextBox textBoxAirOutName;
      private ProsimoUI.SolvableNameTextBox textBoxAirInName;
      //private ProsimoUI.SolvableNameTextBox textBoxLiquidOutName;
      private System.Windows.Forms.GroupBox groupBoxAirInletOutlet;

      private ProsimoUI.SolvableNameTextBox textBoxFlueGasOutName;
      private ProsimoUI.SolvableNameTextBox textBoxFuelInName;
      private System.Windows.Forms.GroupBox groupBoxFuelFlueGasInletOutlet;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public FiredHeaterEditor(FiredHeaterControl firedHeaterCtrl)
         : base(firedHeaterCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         FiredHeater firedHeater = this.FiredHeaterCtrl.FiredHeater;
         this.Text = "Fired Heater: " + firedHeater.Name;

         this.UpdateStreamsUI();

         this.groupBoxFiredHeater = new System.Windows.Forms.GroupBox();
         this.groupBoxFiredHeater.Location = new System.Drawing.Point(644, 24);
         //this.groupBoxFiredHeater.Location = new System.Drawing.Point(1010, 24);
         this.groupBoxFiredHeater.Name = "groupBoxFiredHeater";
         this.groupBoxFiredHeater.Text = "Fired Heater";
         this.groupBoxFiredHeater.Size = new System.Drawing.Size(280, 240);
         this.groupBoxFiredHeater.TabIndex = 127;
         this.groupBoxFiredHeater.TabStop = false;
         this.panel.Controls.Add(this.groupBoxFiredHeater);

         ProcessVarLabelsControl firedHeaterLabelsCtrl = new ProcessVarLabelsControl(this.FiredHeaterCtrl.FiredHeater.VarList);
         this.groupBoxFiredHeater.Controls.Add(firedHeaterLabelsCtrl);
         firedHeaterLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ProcessVarValuesControl firedHeaterValuesCtrl = new ProcessVarValuesControl(this.FiredHeaterCtrl);
         this.groupBoxFiredHeater.Controls.Add(firedHeaterValuesCtrl);
         firedHeaterValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         //firedHeaterCtrl.FiredHeater.StreamAttached += new StreamAttachedEventHandler(FiredHeater_StreamAttached);
         //firedHeaterCtrl.FiredHeater.StreamDetached += new StreamDetachedEventHandler(FiredHeater_StreamDetached);

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
         this.SetCalculationType(this.FiredHeaterCtrl.FiredHeater.CalculationType);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         //if (this.FiredHeaterCtrl.FiredHeater != null) {
         //   this.FiredHeaterCtrl.FiredHeater.StreamAttached -= new StreamAttachedEventHandler(FiredHeater_StreamAttached);
         //   this.FiredHeaterCtrl.FiredHeater.StreamDetached -= new StreamDetachedEventHandler(FiredHeater_StreamDetached);
         //}

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
         this.textBoxAirOutName = new ProsimoUI.SolvableNameTextBox();
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
         this.groupBoxAirInletOutlet.Controls.Add(this.textBoxAirOutName);
         this.groupBoxAirInletOutlet.Controls.Add(this.textBoxAirInName);
         this.groupBoxAirInletOutlet.Location = new System.Drawing.Point(4, 24);
         this.groupBoxAirInletOutlet.Name = "groupBoxGasInletOutlet";
         this.groupBoxAirInletOutlet.Size = new System.Drawing.Size(360, 280);
         this.groupBoxAirInletOutlet.TabIndex = 118;
         this.groupBoxAirInletOutlet.TabStop = false;
         this.groupBoxAirInletOutlet.Text = "Gas Inlet/Outlet";
         // 
         // textBoxGasOutName
         // 
         this.textBoxAirOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxAirOutName.Name = "textBoxGasOutName";
         this.textBoxAirOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxAirOutName.TabIndex = 7;
         this.textBoxAirOutName.Text = "";
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
         this.Name = "FiredHeaterEditor";
         this.Text = "Fired Heater";
         this.groupBoxAirInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.groupBoxFuelFlueGasInletOutlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
         FiredHeater firedHeater = this.FiredHeaterCtrl.FiredHeater;
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
                  ErrorMessage error = firedHeater.SpecifyName(this.textBoxName.Text);
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
         list.Add(this.textBoxAirOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      //private void FiredHeater_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
      //   this.UpdateStreamsUI();
      //}

      //private void FiredHeater_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
      //   this.UpdateStreamsUI();
      //}

      protected override void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxAirInletOutlet.Controls.Clear();
         this.groupBoxFuelFlueGasInletOutlet.Controls.Clear();

         FiredHeater firedHeater = this.FiredHeaterCtrl.FiredHeater;
         bool hasFuelIn = false;
         bool hasAirIn = false;
         bool hasFlueGasOut = false;
         bool hasHeatedIn = false;
         bool hasHeatedOut = false;

         ProcessStreamBase airIn = firedHeater.AirInlet;
         hasAirIn = airIn != null;

         ProcessStreamBase fuelIn = firedHeater.FuelInlet;
         hasFuelIn = fuelIn != null;

         ProcessStreamBase flueGasOut = firedHeater.FlueGasOutlet;
         hasFlueGasOut = flueGasOut != null;

         ProcessStreamBase heatedIn = firedHeater.HeatedInlet;
         hasHeatedIn = heatedIn != null;

         ProcessStreamBase heatedOut = firedHeater.HeatedOutlet;
         hasHeatedOut = heatedOut != null;

         //TODO.....

      }

      private void menuItemRating_Click(object sender, EventArgs e) {
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e) {
         if (!this.inConstruction) {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == INDEX_BALANCE) {
               error = this.FiredHeaterCtrl.FiredHeater.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null) {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == INDEX_RATING) {
               error = this.FiredHeaterCtrl.FiredHeater.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null) {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null) {
               UI.ShowError(error);
               this.SetCalculationType(this.FiredHeaterCtrl.FiredHeater.CalculationType);
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
