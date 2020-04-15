using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for ProcessStreamBaseEditor.
   /// </summary>
   public class DetailedFuelStreamLabelAndValuesControl : UserControl {
      private FossilFuelCatalog fuelCatalog = FossilFuelCatalog.Instance;
      private System.Windows.Forms.Label labelDryingFuelSelection;
      private System.Windows.Forms.ComboBox comboBoxDryingFuelSelection;
      private DetailedFuelStreamControl streamCtrl;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public DetailedFuelStreamLabelAndValuesControl() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public DetailedFuelStreamLabelAndValuesControl(DetailedFuelStreamControl streamCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.comboBoxDryingFuelSelection.Items.AddRange(fuelCatalog.GetFossilFuelArray());
         this.comboBoxDryingFuelSelection.SelectedIndexChanged += comboBoxDryingFuelSelection_SelectedIndexChanged;

         this.streamCtrl = streamCtrl;
         ArrayList varList = streamCtrl.Solvable.VarList;

         ProcessVarLabelsControl gasLabelsCtrl = new ProcessVarLabelsControl(varList);
         this.Controls.Add(gasLabelsCtrl);
         gasLabelsCtrl.Location = new Point(0, 24);

         ProcessVarValuesControl gasValuesCtrl = new ProcessVarValuesControl(streamCtrl);
         this.Controls.Add(gasValuesCtrl);
         gasValuesCtrl.Location = new Point(192, 24);

         this.Text = streamCtrl.Name;
         this.ClientSize = new System.Drawing.Size(274, varList.Count * 20 + 67);

         InitializeDryingFuelComboBox(streamCtrl);

         fuelCatalog.FossilFuelAdded += DetailedFuelStreamEditor_FossilFuelAdded;
         fuelCatalog.FossilFuelDeleted += DetailedFuelStreamEditor_FossilFuelDeleted;
      }

      private void InitializeDryingFuelComboBox(DetailedFuelStreamControl streamCtrl)
      {
         ArrayList dryingFuels = fuelCatalog.GetFossilFuelList();
         int index = 0;
         for (int i = 0; i < dryingFuels.Count; i++)
         {
            FossilFuel fuel = dryingFuels[i] as FossilFuel;
            if (fuel.Name == streamCtrl.DetailedFuelStream.DryingFuel.Name)
            {
               index = i;
               break;
            }
         }
         this.comboBoxDryingFuelSelection.SelectedIndex = index;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         fuelCatalog.FossilFuelAdded -= DetailedFuelStreamEditor_FossilFuelAdded;
         fuelCatalog.FossilFuelDeleted -= DetailedFuelStreamEditor_FossilFuelDeleted;

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
         this.labelDryingFuelSelection = new System.Windows.Forms.Label();
         this.comboBoxDryingFuelSelection = new System.Windows.Forms.ComboBox();
         this.SuspendLayout();
         // 
         // labelDryingFuelSelection
         // 
         this.labelDryingFuelSelection.BackColor = System.Drawing.Color.DarkGray;
         this.labelDryingFuelSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelDryingFuelSelection.Location = new System.Drawing.Point(0, 0);
         this.labelDryingFuelSelection.Name = "labelDryingFuelSelection";
         this.labelDryingFuelSelection.Size = new System.Drawing.Size(74, 20);
         this.labelDryingFuelSelection.TabIndex = 5;
         this.labelDryingFuelSelection.Text = "Drying Fuel:";
         this.labelDryingFuelSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // comboBoxDryingFuelSelection
         // 
         this.comboBoxDryingFuelSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDryingFuelSelection.Location = new System.Drawing.Point(74, 0);
         this.comboBoxDryingFuelSelection.Name = "comboBoxDryingFuelSelection";
         this.comboBoxDryingFuelSelection.Size = new System.Drawing.Size(200, 21);
         this.comboBoxDryingFuelSelection.TabIndex = 7;
         // 
         // DetailedFuelStreamLabelAndValuesControl
         // 
         this.Controls.Add(this.labelDryingFuelSelection);
         this.Controls.Add(this.comboBoxDryingFuelSelection);
         this.Name = "DetailedFuelStreamLabelAndValuesControl";
         this.Size = new System.Drawing.Size(279, 150);
         this.ResumeLayout(false);

      }

      private void comboBoxDryingFuelSelection_SelectedIndexChanged(object sender, EventArgs e)
      {
         string fossilFuelName = comboBoxDryingFuelSelection.SelectedItem.ToString();
         FossilFuel fossilFuel = fuelCatalog.GetFossilFuel(fossilFuelName);
         DetailedFuelStream myFuleStream = streamCtrl.DetailedFuelStream;
         myFuleStream.DryingFuel = fossilFuel;
      }

      private void DetailedFuelStreamEditor_FossilFuelAdded(FossilFuel fuel)
      {
         FossilFuelCatalogChanged();
      }

      private void DetailedFuelStreamEditor_FossilFuelDeleted(string fuleName)
      {
         FossilFuelCatalogChanged();
      }

      private void FossilFuelCatalogChanged()
      {
         this.comboBoxDryingFuelSelection.Items.Clear();
         this.comboBoxDryingFuelSelection.Items.AddRange(fuelCatalog.GetFossilFuelArray());
      }
      #endregion
   }
}
