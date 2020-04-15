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
      //private bool inConstruction;

      public BurnerControl BurnerCtrl {
         get { return (BurnerControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      //private MenuItem menuItemRating;
      //private System.Windows.Forms.Label labelCalculationType;
      //private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxBurner;
      private ProsimoUI.SolvableNameTextBox textBoxFuelInName;
      private ProsimoUI.SolvableNameTextBox textBoxAirInName;
      private ProsimoUI.SolvableNameTextBox textBoxFlueGasOutName;
      private System.Windows.Forms.GroupBox groupBoxFuelInlet;
      private System.Windows.Forms.GroupBox groupBoxAirInlet;
      //private System.Windows.Forms.GroupBox groupBoxFlueGasOutlet;

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

         //this.inConstruction = true;
         //Burner b = this.BurnerCtrl.Burner;
         //this.Text = "Combustor: " + b.Name;
         this.Text = this.solvableCtrl.SolvableDispalyTitle;

         this.UpdateStreamsUI();

         this.groupBoxBurner = new System.Windows.Forms.GroupBox();
         this.groupBoxBurner.Location = new System.Drawing.Point(644, 24);
         this.groupBoxBurner.Name = "groupBoxBurner";
         this.groupBoxBurner.Text = this.solvableCtrl.SolvableTypeName;
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
         this.ClientSize = new System.Drawing.Size(930, 330);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
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
         this.groupBoxFuelInlet = new System.Windows.Forms.GroupBox();
         this.textBoxFuelInName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxFlueGasOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxAirInName = new ProsimoUI.SolvableNameTextBox();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.groupBoxFuelInlet.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxFuelInlet
         // 
         this.groupBoxFuelInlet.Controls.Add(this.textBoxFuelInName);
         this.groupBoxFuelInlet.Location = new System.Drawing.Point(4, 24);
         this.groupBoxFuelInlet.Name = "groupBoxFuelInlet";
         this.groupBoxFuelInlet.Size = new System.Drawing.Size(280, 280);
         this.groupBoxFuelInlet.TabIndex = 118;
         this.groupBoxFuelInlet.TabStop = false;
         this.groupBoxFuelInlet.Text = "Fuel Inlet";
          
         // textBoxAirInName
         // 
         this.groupBoxAirInlet = new System.Windows.Forms.GroupBox();
         this.groupBoxAirInlet.Controls.Add(this.textBoxAirInName);
         this.groupBoxAirInlet.Controls.Add(this.textBoxFlueGasOutName);
         this.groupBoxAirInlet.Location = new System.Drawing.Point(284, 24);
         this.groupBoxAirInlet.Name = "groupBoxAirInlet";
         this.groupBoxAirInlet.Size = new System.Drawing.Size(360, 280);
         this.groupBoxAirInlet.TabIndex = 118;
         this.groupBoxAirInlet.TabStop = false;
         this.groupBoxAirInlet.Text = "Air Inlet/Flue Gas Outlet";

         this.textBoxAirInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxAirInName.Name = "textBoxAirInName";
         this.textBoxAirInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxAirInName.TabIndex = 6;
         this.textBoxAirInName.Text = "";
         // 
         // textBoxWaterOutName
         // 
         this.textBoxFlueGasOutName.Location = new System.Drawing.Point(276, 12);
         this.textBoxFlueGasOutName.Name = "textBoxFlueGasOutName";
         this.textBoxFlueGasOutName.Size = new System.Drawing.Size(80, 20);
         this.textBoxFlueGasOutName.TabIndex = 7;
         this.textBoxFlueGasOutName.Text = "";

         this.textBoxFuelInName.Location = new System.Drawing.Point(196, 12);
         this.textBoxFuelInName.Name = "textBoxFuelInletName";
         this.textBoxFuelInName.Size = new System.Drawing.Size(80, 20);
         this.textBoxFuelInName.TabIndex = 8;
         this.textBoxFuelInName.Text = "";
         // 
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxFuelInlet);
         this.panel.Controls.Add(this.groupBoxAirInlet);
         this.panel.Size = new System.Drawing.Size(930, 329);
         // 
         // FiredHeaterEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         //this.ClientSize = new System.Drawing.Size(930, 351);
         this.Name = "CombustorEditor";
         this.Text = this.solvableCtrl.SolvableTypeName;
         this.groupBoxAirInlet.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
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

      protected override void UpdateStreamsUI() {
         // clear the streams boxes and start again
         this.groupBoxAirInlet.Controls.Clear();
         this.groupBoxFuelInlet.Controls.Clear();

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

         if(fuelIn != null)
         {
            //ProcessVarLabelsControl ctrl = new ProcessVarLabelsControl(fuelIn.VarList);
            //ctrl.Location = new Point(4, 12 + 20 + 2);
            //this.groupBoxFuelInlet.Controls.Add(ctrl);

            //ProcessStreamBaseControl baseCtrl = this.BurnerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BurnerCtrl.Burner.FuelInlet.Name);
            //ProcessVarValuesControl valuesCtrl = new ProcessVarValuesControl(baseCtrl);
            //this.groupBoxFuelInlet.Controls.Add(valuesCtrl);
            //valuesCtrl.Location = new Point(196, 12 + 20 + 2);
            
            ProcessStreamBaseControl baseCtrl = this.BurnerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BurnerCtrl.Burner.FuelInlet.Name);
            DetailedFuelStreamLabelAndValuesControl fuelLableValuesControl = new DetailedFuelStreamLabelAndValuesControl(baseCtrl as DetailedFuelStreamControl);
            fuelLableValuesControl.Location = new Point(4, 12 + 20 + 2);
            this.groupBoxFuelInlet.Controls.Add(fuelLableValuesControl);
            this.textBoxFuelInName.SetSolvable(burner.FuelInlet);
            this.groupBoxFuelInlet.Controls.Add(this.textBoxFuelInName);
            this.textBoxAirInName.Text = burner.FuelInlet.Name;
            UI.SetStatusColor(this.textBoxFuelInName, burner.FuelInlet.SolveState);
         }
         
         if(hasAirIn || hasFlueGasOut)
         {
            ProcessStreamBase labelsStream = hasAirIn ? airIn : flueGasOut;

            GasStreamLabelsControl gasLabelsCtrl = new GasStreamLabelsControl(labelsStream as DryingGasStream);
            this.groupBoxAirInlet.Controls.Add(gasLabelsCtrl);
            gasLabelsCtrl.Location = new Point(4, 12 + 20 + 2);
         }

         if(hasAirIn)
         {
            ProcessStreamBaseControl airInCtrl = this.BurnerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BurnerCtrl.Burner.AirInlet.Name);
            GasStreamValuesControl airInValuesCtrl = new GasStreamValuesControl((GasStreamControl)airInCtrl);
            this.groupBoxAirInlet.Controls.Add(airInValuesCtrl);
            airInValuesCtrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxAirInName.SetSolvable(burner.AirInlet);
            this.groupBoxAirInlet.Controls.Add(this.textBoxAirInName);
            this.textBoxAirInName.Text = burner.AirInlet.Name;
            UI.SetStatusColor(this.textBoxAirInName, burner.AirInlet.SolveState);
         }

         if(hasFlueGasOut)
         {
            ProcessStreamBaseControl flueGasOutCtrl = this.BurnerCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.BurnerCtrl.Burner.FlueGasOutlet.Name);
            GasStreamValuesControl flueGasOutValuesCtrl = new GasStreamValuesControl((GasStreamControl)flueGasOutCtrl);
            this.groupBoxAirInlet.Controls.Add(flueGasOutValuesCtrl);
            flueGasOutValuesCtrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxFlueGasOutName.SetSolvable(burner.FlueGasOutlet);
            this.groupBoxAirInlet.Controls.Add(this.textBoxFlueGasOutName);
            this.textBoxFlueGasOutName.Text = burner.FlueGasOutlet.Name;
            UI.SetStatusColor(this.textBoxFlueGasOutName, burner.FlueGasOutlet.SolveState);
         }
      }
   }
}
