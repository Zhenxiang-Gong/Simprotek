using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using Prosimo;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.VaporLiquidSeparation;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for ScrubberCondenserEditor.
   /// </summary>
   public class ScrubberCondenserEditor : UnitOpEditor
   {
      public const int INDEX_BALANCE = 0;
      public const int INDEX_RATING = 1;

//      private ScrubberCondenserRatingEditor ratingEditor;
      private bool inConstruction;

      public ScrubberCondenserControl ScrubberCondenserCtrl
      {
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

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ScrubberCondenserEditor(ScrubberCondenserControl scrubberCondenserCtrl)
         : base(scrubberCondenserCtrl)
      {
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
         this.groupBoxScrubberCondenser.Name = "groupBoxScrubberCondenser";
         this.groupBoxScrubberCondenser.Text = "Scrubber Condenser";
         this.groupBoxScrubberCondenser.Size = new System.Drawing.Size(280, 240);
         this.groupBoxScrubberCondenser.TabIndex = 127;
         this.groupBoxScrubberCondenser.TabStop = false;
         this.panel.Controls.Add(this.groupBoxScrubberCondenser);

         this.groupBoxLiquidOutlet.Size = new System.Drawing.Size(280, 300);
         this.panel.Size = new System.Drawing.Size(930, 329);
         this.ClientSize = new System.Drawing.Size(930, 351);

         ScrubberCondenserLabelsControl scrubberCondenserLabelsCtrl = new ScrubberCondenserLabelsControl(this.ScrubberCondenserCtrl.ScrubberCondenser);
         this.groupBoxScrubberCondenser.Controls.Add(scrubberCondenserLabelsCtrl);
         scrubberCondenserLabelsCtrl.Location = new Point(4, 12 + 20 + 2);

         ScrubberCondenserValuesControl scrubberCondenserValuesCtrl = new ScrubberCondenserValuesControl(this.ScrubberCondenserCtrl);
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
         this.inConstruction = false;
         this.SetCalculationType(this.ScrubberCondenserCtrl.ScrubberCondenser.CalculationType);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if (this.ScrubberCondenserCtrl.ScrubberCondenser != null)
         {
            this.ScrubberCondenserCtrl.ScrubberCondenser.StreamAttached -= new StreamAttachedEventHandler(ScrubberCondenser_StreamAttached);
            this.ScrubberCondenserCtrl.ScrubberCondenser.StreamDetached -= new StreamDetachedEventHandler(ScrubberCondenser_StreamDetached);
         }

         if (disposing)
         {
            if (components != null)
            {
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
      private void InitializeComponent()
      {
         this.groupBoxGasInletOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxGasOutName = new ProsimoUI.SolvableNameTextBox();
         this.textBoxGasInName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxLiquidOutlet = new System.Windows.Forms.GroupBox();
         this.textBoxLiquidOutName = new ProsimoUI.SolvableNameTextBox();
         this.groupBoxGasInletOutlet.SuspendLayout();
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
         // panel
         // 
         this.panel.Controls.Add(this.groupBoxLiquidOutlet);
         this.panel.Controls.Add(this.groupBoxGasInletOutlet);
         this.panel.Size = new System.Drawing.Size(930, 329);
         // 
         // groupBoxLiquidOutlet
         // 
         this.groupBoxLiquidOutlet.Controls.Add(this.textBoxLiquidOutName);
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
         this.panel.ResumeLayout(false);
         this.groupBoxLiquidOutlet.ResumeLayout(false);
         this.ResumeLayout(false);
      }
      #endregion

      protected override void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         ScrubberCondenser scrubberCondenser = this.ScrubberCondenserCtrl.ScrubberCondenser;
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
                  ErrorMessage error = scrubberCondenser.SpecifyName(this.textBoxName.Text);
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
         list.Add(this.textBoxGasInName);
         list.Add(this.textBoxGasOutName);
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

      private void ScrubberCondenser_StreamAttached(UnitOperation uo, ProcessStreamBase ps, int desc)
      {
         this.UpdateStreamsUI();
      }

      private void ScrubberCondenser_StreamDetached(UnitOperation uo, ProcessStreamBase ps)
      {
         this.UpdateStreamsUI();
      }

      private void UpdateStreamsUI()
      {
         // clear the streams boxes and start again
         this.groupBoxGasInletOutlet.Controls.Clear();
         this.groupBoxLiquidOutlet.Controls.Clear();

         ScrubberCondenser scrubberCondenser = this.ScrubberCondenserCtrl.ScrubberCondenser;
         bool hasGasIn = false;
         bool hasGasOut = false;
         bool hasLiquidOut = false;

         ProcessStreamBase gasIn = scrubberCondenser.GasInlet;
         if (gasIn != null)
            hasGasIn = true;

         ProcessStreamBase gasOut = scrubberCondenser.GasOutlet;
         if (gasOut != null)
            hasGasOut = true;

         ProcessStreamBase liquidOut = scrubberCondenser.LiquidOutlet;
         if (liquidOut != null)
            hasLiquidOut = true;

         if (hasGasIn || hasGasOut)
         {
            ProcessStreamBase labelsStream = null;
            if (hasGasIn)
               labelsStream = gasIn;
            else if (hasGasOut)
               labelsStream = gasOut;

            UserControl ctrl = null;
            if (labelsStream is ProcessStream)
            {
               ctrl = new ProcessStreamLabelsControl((ProcessStream)labelsStream);
            }
            else if (labelsStream is DryingGasStream)
            {
               ctrl = new GasStreamLabelsControl((DryingGasStream)labelsStream);
            }
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);
         }

         if (hasGasIn)
         {
            UserControl ctrl = null;
            if (gasIn is ProcessStream)
            {
               ProcessStreamControl processInCtrl = (ProcessStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasInlet.Name);
               ctrl = new ProcessStreamValuesControl(processInCtrl);
            }
            else if (gasIn is DryingGasStream)
            {
               GasStreamControl gasInCtrl = (GasStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasInlet.Name);
               ctrl = new GasStreamValuesControl(gasInCtrl);
            }
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxGasInName.SetSolvable(scrubberCondenser.GasInlet);
            this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasInName);
            this.textBoxGasInName.Text = scrubberCondenser.GasInlet.Name;
            UI.SetStatusColor(this.textBoxGasInName, scrubberCondenser.GasInlet.SolveState);
         }

         if (hasGasOut)
         {
            UserControl ctrl = null;
            if (gasOut is ProcessStream)
            {
               ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasOutlet.Name);
               ctrl = new ProcessStreamValuesControl(processOutCtrl);
            }
            else if (gasOut is DryingGasStream)
            {
               GasStreamControl gasOutCtrl = (GasStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.GasOutlet.Name);
               ctrl = new GasStreamValuesControl(gasOutCtrl);
            }
            this.groupBoxGasInletOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(276, 12 + 20 + 2);

            this.textBoxGasOutName.SetSolvable(scrubberCondenser.GasOutlet);
            this.groupBoxGasInletOutlet.Controls.Add(this.textBoxGasOutName);
            this.textBoxGasOutName.Text = scrubberCondenser.GasOutlet.Name;
            UI.SetStatusColor(this.textBoxGasOutName, scrubberCondenser.GasOutlet.SolveState);
         }

         if (hasLiquidOut)
         {
            // add the labels
            UserControl ctrl = null;
            if (liquidOut is ProcessStream)
            {
               ctrl = new ProcessStreamLabelsControl((ProcessStream)liquidOut);
            }
            else if (liquidOut is DryingMaterialStream)
            {
               ctrl = new MaterialStreamLabelsControl((DryingMaterialStream)liquidOut);
            }
            this.groupBoxLiquidOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(4, 12 + 20 + 2);

            // add the values
            ctrl = null;
            if (liquidOut is ProcessStream)
            {
               ProcessStreamControl processOutCtrl = (ProcessStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.LiquidOutlet.Name);
               ctrl = new ProcessStreamValuesControl(processOutCtrl);
            }
            else if (liquidOut is DryingMaterialStream)
            {
               MaterialStreamControl matOutCtrl = (MaterialStreamControl)this.ScrubberCondenserCtrl.Flowsheet.StreamManager.GetProcessStreamBaseControl(this.ScrubberCondenserCtrl.ScrubberCondenser.LiquidOutlet.Name);
               ctrl = new MaterialStreamValuesControl(matOutCtrl);
            }
            this.groupBoxLiquidOutlet.Controls.Add(ctrl);
            ctrl.Location = new Point(196, 12 + 20 + 2);

            this.textBoxLiquidOutName.SetSolvable(scrubberCondenser.LiquidOutlet);
            this.groupBoxLiquidOutlet.Controls.Add(this.textBoxLiquidOutName);
            this.textBoxLiquidOutName.Text = scrubberCondenser.LiquidOutlet.Name;
            UI.SetStatusColor(this.textBoxLiquidOutName, scrubberCondenser.LiquidOutlet.SolveState);
         }
      }

      private void menuItemRating_Click(object sender, EventArgs e)
      {
/*
         if (this.ScrubberCondenserCtrl.ScrubberCondenser.CurrentRatingModel != null)
         {
            this.comboBoxCalculationType.Enabled = false;

            if (this.ratingEditor == null)
            {
               this.ratingEditor = new ScrubberCondenserRatingEditor(this.ScrubberCondenserCtrl);
               this.ratingEditor.Owner = this;
               this.ratingEditor.Closed += new EventHandler(ratingEditor_Closed);
               this.ratingEditor.Show();
            }
            else
            {
               if (this.ratingEditor.WindowState.Equals(FormWindowState.Minimized))
                  this.ratingEditor.WindowState = FormWindowState.Normal;
               this.ratingEditor.Activate();
            }
         }
*/
      }

      private void comboBoxCalculationType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (!this.inConstruction)
         {
            ErrorMessage error = null;
            int idx = this.comboBoxCalculationType.SelectedIndex;
            if (idx == ScrubberCondenserEditor.INDEX_BALANCE)
            {
               error = this.ScrubberCondenserCtrl.ScrubberCondenser.SpecifyCalculationType(UnitOpCalculationType.Balance);
               if (error == null)
               {
                  this.menuItemRating.Enabled = false;
               }
            }
            else if (idx == ScrubberCondenserEditor.INDEX_RATING)
            {
               error = this.ScrubberCondenserCtrl.ScrubberCondenser.SpecifyCalculationType(UnitOpCalculationType.Rating);
               if (error == null)
               {
                  this.menuItemRating.Enabled = true;
               }
            }
            if (error != null)
            {
               UI.ShowError(error);
               this.SetCalculationType(this.ScrubberCondenserCtrl.ScrubberCondenser.CalculationType);
            }
         }
      }

      public void SetCalculationType(UnitOpCalculationType type)
      {
         if (type == UnitOpCalculationType.Balance)
            this.comboBoxCalculationType.SelectedIndex = INDEX_BALANCE;
         else if (type == UnitOpCalculationType.Rating)
            this.comboBoxCalculationType.SelectedIndex = INDEX_RATING;
      }

      private void ratingEditor_Closed(object sender, EventArgs e)
      {
//         this.ratingEditor = null;
         this.comboBoxCalculationType.Enabled = true;
      }
   }
}
