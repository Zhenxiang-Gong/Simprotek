using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI.CycloneUI {
   /// <summary>
   /// Summary description for CycloneEditor.
   /// </summary>
   public class CycloneEditor : UnitOpEditor {

      private CycloneRatingEditor ratingEditor;
      private bool inConstruction;

      public CycloneControl CycloneCtrl {
         get { return (CycloneControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      private MenuItem menuItemRating;
      private System.Windows.Forms.Label labelCalculationType;
      private System.Windows.Forms.ComboBox comboBoxCalculationType;

      private System.Windows.Forms.GroupBox groupBoxCyclone;
      private ProsimoUI.SolvableNameTextBox textBoxGasOutName;
      private ProsimoUI.SolvableNameTextBox textBoxGasInName;
      private ProsimoUI.SolvableNameTextBox textBoxParticleOutName;
      private System.Windows.Forms.GroupBox groupBoxMixtureFluid;
      private System.Windows.Forms.GroupBox groupBoxParticle;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public CycloneEditor(CycloneControl cycloneCtrl)
         : base(cycloneCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.inConstruction = true;
         Cyclone cyclone = this.CycloneCtrl.Cyclone;
         this.Text = "Cyclone: " + cyclone.Name;

         this.UpdateStreamsUI();

         this.groupBoxCyclone = new System.Windows.Forms.GroupBox();
         this.groupBoxCyclone.Location = new System.Drawing.Point(644, 24);
         this.groupBoxCyclone.Name = "groupBoxCyclone";
         this.groupBoxCyclone.Text = "Cyclone";
         this.groupBoxCyclone.Size = new System.Drawing.Size(280, 160);
         this.groupBoxCyclone.TabIndex = 127;
         this.groupBoxCyclone.TabStop = false;
         this.panel.Controls.Add(this.groupBoxCyclone);

         if (cyclone.ParticleOutlet is DryingGasStream) {
            this.groupBoxParticle.Size = new System.Drawing.Size(280, 300);
            this.panel.Size = new System.Drawing.Size(930, 329);
            this.ClientSize = new System.Drawing.Size(930, 351);
         }
         else if (cyclone.ParticleOutlet is DryingMaterialStream) {
            this.groupBoxParticle.Size = new System.Drawing.Size(280, 300);
            this.panel.Size = new System.Drawing.Size(930, 329);
            this.ClientSize = new System.Drawing.Size(930, 351);
         }

         //ProcessVarLabelsControl cycloneLabelsCtrl = new ProcessVarLabelsControl(this.CycloneCtrl.Cyclone.VarList);
         CycloneLabelsControl cycloneLabelsCtrl = new CycloneLabelsControl(this.CycloneCtrl.Cyclone);
         this.groupBoxCyclone.Controls.Add(cycloneLabelsCtrl);
         cycloneLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         //ProcessVarValuesControl cycloneValuesCtrl = new ProcessVarValuesControl(this.CycloneCtrl);
         CycloneValuesControl cycloneValuesCtrl = new CycloneValuesControl(this.CycloneCtrl);
         this.groupBoxCyclone.Controls.Add(cycloneValuesCtrl);
         cycloneValuesCtrl.Location = new Point(196, 12 + 20 + 2);

         cycloneCtrl.Cyclone.StreamAttached += new StreamAttachedEventHandler(Cyclone_StreamAttached);
         cycloneCtrl.Cyclone.StreamDetached += new StreamDetachedEventHandler(Cyclone_StreamDetached);

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
         this.inConstruction = false;
         this.SetCalculationType(this.CycloneCtrl.Cyclone.CalculationType);
         this.comboBoxCalculationType.Enabled = true;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.CycloneCtrl.Cyclone != null) {
            this.CycloneCtrl.Cyclone.StreamAttached -= new StreamAttachedEventHandler(Cyclone_StreamAttached);
            this.CycloneCtrl.Cyclone.StreamDetached -= new StreamDetachedEventHandler(Cyclone_StreamDetached);
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
         this.groupBoxMixtureFluid = new System.Windows.Forms.GroupBox();
         this.textBoxGasOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxGasInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxParticle = new System.Windows.Forms.GroupBox();
         this.textBoxParticleOutName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxMixtureFluid.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.groupBoxParticle.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBoxMixtureFluid
         // 
         this.groupBoxMixtureFluid.Controls.Add(this.textBoxGasOutName);
         this.groupBoxMixtureFluid.Controls.Add(this.textBoxGasInName);
         this.groupBoxMixtureFluid.Location = new System.Drawing.Point(4, 24);
         this.groupBoxMixtureFluid.Name = "groupBoxMixtureFluid";
         this.groupBoxMixtureFluid.Size = new System.Drawing.Size(360, 280);
         this.groupBoxMixtureFluid.TabIndex = 118;
         this.groupBoxMixtureFluid.TabStop = false;
         this.groupBoxMixtureFluid.Text = "Gas Inlet/Outlet";
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
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxParticle);
         this.panel.Controls.Add(this.groupBoxMixtureFluid);
         this.panel.Size = new System.Drawing.Size(930, 329);
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
         // CycloneEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(930, 351);
         this.Name = "CycloneEditor";
         this.Text = "Cyclone";
         this.groupBoxMixtureFluid.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.groupBoxParticle.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e) {
         Cyclone cyclone = this.CycloneCtrl.Cyclone;
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
                  ErrorMessage error = cyclone.SpecifyName(this.textBoxName.Text);
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
         list.Add(this.textBoxParticleOutName);

         if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Down);
         }
         else if (e.KeyCode == Keys.Up) {
            UI.NavigateKeyboard(list, sender, this, KeyboardNavigation.Up);
         }
      }

      private void Cyclone_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc) {
         this.UpdateStreamsUI();
      }

      private void Cyclone_StreamDetached(UnitOperation uo, ProcessStreamBase ps) {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI() {
         // clear the streams boxes and start again
         this.groupBoxMixtureFluid.Controls.Clear();
         this.groupBoxParticle.Controls.Clear();

         Cyclone cyclone = this.CycloneCtrl.Cyclone;
         bool hasGasIn = false;
         bool hasGasOut = false;
         bool hasParticleOut = false;

         ProcessStreamBase mixtureIn = cyclone.GasInlet;
         hasGasIn = mixtureIn != null;

         ProcessStreamBase fluidOut = cyclone.GasOutlet;
         hasGasOut = fluidOut != null;

         ProcessStreamBase particleOut = cyclone.ParticleOutlet;
         if (particleOut != null)
            hasParticleOut = true;

         if (hasGasIn || hasGasOut) {
            ProcessStreamBase labelsStream = hasGasIn ? mixtureIn : fluidOut;

            UserControl ctrl = null;
            //UserControl ctrl = new ProcessVarLabelsControl(labelsStream.VarList); 
            if (labelsStream is DryingGasStream) {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            this.groupBoxMixtureFluid.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasGasIn) {
            ProcessStreamBaseControl gasInCtrl = this.CycloneCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.CycloneCtrl.Cyclone.GasInlet.Name);
            //UserControl ctrl = new ProcessVarValuesControl(gasInCtrl);
            UserControl ctrl = new GasStreamValuesControl((GasStreamControl)gasInCtrl);
            
            this.groupBoxMixtureFluid.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxGasInName.SetSolvable(cyclone.GasInlet);
            this.groupBoxMixtureFluid.Controls.Add(this.textBoxGasInName);
            this.textBoxGasInName.Text = cyclone.GasInlet.Name;
            UI.SetStatusColor(this.textBoxGasInName, cyclone.GasInlet.SolveState);
         }

         if (hasGasOut) {
            ProcessStreamBaseControl gasOutCtrl = this.CycloneCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.CycloneCtrl.Cyclone.GasOutlet.Name);
            //UserControl ctrl = new ProcessVarValuesControl(gasOutCtrl);
            UserControl ctrl = new GasStreamValuesControl((GasStreamControl)gasOutCtrl);

            this.groupBoxMixtureFluid.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxGasOutName.SetSolvable(cyclone.GasOutlet);
            this.groupBoxMixtureFluid.Controls.Add(this.textBoxGasOutName);
            this.textBoxGasOutName.Text = cyclone.GasOutlet.Name;
            UI.SetStatusColor(this.textBoxGasOutName, cyclone.GasOutlet.SolveState);
         }

         if (hasParticleOut) {
            // add the labels
            UserControl ctrl = null;
            if (particleOut is DryingMaterialStream) {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)particleOut);
               //ctrl = new ProcessVarLabelsControl(particleOut.VarList);
            }
            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);

            // add the values
            ProcessStreamBaseControl matOutCtrl = this.CycloneCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.CycloneCtrl.Cyclone.ParticleOutlet.Name);
            if (particleOut is DryingMaterialStream) {
               //ctrl = new ProcessVarValuesControl(matOutCtrl);
               ctrl = new MaterialStreamValuesControl((MaterialStreamControl)matOutCtrl);
            }

            this.groupBoxParticle.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxParticleOutName.SetSolvable(cyclone.ParticleOutlet);
            this.groupBoxParticle.Controls.Add(this.textBoxParticleOutName);
            this.textBoxParticleOutName.Text = cyclone.ParticleOutlet.Name;
            UI.SetStatusColor(this.textBoxParticleOutName, cyclone.ParticleOutlet.SolveState);
         }
      }

      private void menuItemRating_Click(object sender, EventArgs e) {
         if (this.CycloneCtrl.Cyclone.CurrentRatingModel != null) {
            this.comboBoxCalculationType.Enabled = false;

            if (this.ratingEditor == null) {
               this.ratingEditor = new CycloneRatingEditor(this.CycloneCtrl);
               this.ratingEditor.Owner = this;
               this.ratingEditor.Closed += new EventHandler(ratingEditor_Closed);
               this.ratingEditor.Show();
            }
            else {
               if (this.ratingEditor.WindowState.Equals(FormWindowState.Minimized))
                  this.ratingEditor.WindowState = FormWindowState.Normal;
               this.ratingEditor.Activate();
            }
         }
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e) {
         if (!this.inConstruction) {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == INDEX_BALANCE) {
               error = this.CycloneCtrl.Cyclone.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null) {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == INDEX_RATING) {
               error = this.CycloneCtrl.Cyclone.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null) {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null) {
               UI.ShowError(error);
               this.SetCalculationType(this.CycloneCtrl.Cyclone.CalculationType);
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
         this.ratingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
      }
   }
}
