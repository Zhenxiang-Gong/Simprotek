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
   public class DetailedFuelStreamEditor : SolvableEditor {
      //private FossilFuelCatalog fuelCatalog = FossilFuelCatalog.Instance;
      //private System.Windows.Forms.Label labelDryingFuelSelection;
      //private System.Windows.Forms.ComboBox comboBoxFossilFuelSelection;

      public DetailedFuelStreamControl DetailedFuelStreamCtrl {
         get { return (DetailedFuelStreamControl)this.solvableCtrl; }
         set { this.solvableCtrl = value; }
      }

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public DetailedFuelStreamEditor() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public DetailedFuelStreamEditor(DetailedFuelStreamControl streamCtrl)
         : base(streamCtrl) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         //ArrayList varList = solvableCtrl.Solvable.VarList;

         //ProcessVarLabelsControl gasLabelsCtrl = new ProcessVarLabelsControl(varList);
         //this.panel.Controls.Add(gasLabelsCtrl);
         //gasLabelsCtrl.Location = new Point(0, 44);

         //ProcessVarValuesControl gasValuesCtrl = new ProcessVarValuesControl(streamCtrl);
         //this.panel.Controls.Add(gasValuesCtrl);
         //gasValuesCtrl.Location = new Point(192, 44);

         //this.Text = streamCtrl.Name;
         //this.ClientSize = new System.Drawing.Size(274, varList.Count * 20 + 67);

         //IList dryingFuels = fuelCatalog.GetFossilFuelArray();
         //int index = dryingFuels.IndexOf(streamCtrl.DetailedFuelStream.DryingFuel);
         //this.comboBoxFossilFuelSelection.SelectedIndex = index;

         //fuelCatalog.FossilFuelAdded += DetailedFuelStreamEditor_FossilFuelAdded;
         //fuelCatalog.FossilFuelDeleted += DetailedFuelStreamEditor_FossilFuelDeleted;

         DetailedFuelStreamLabelAndValuesControl fuelLableValuesControl = new DetailedFuelStreamLabelAndValuesControl(streamCtrl);
         this.panel.Controls.Add(fuelLableValuesControl);
         fuelLableValuesControl.Location = new Point(0, 24);

         this.Text = streamCtrl.Name;
         this.ClientSize = new System.Drawing.Size(274, solvableCtrl.Solvable.VarList.Count * 20 + 73);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessStreamBaseEditor));
         this.SuspendLayout();
         //this.labelDryingFuelSelection = new System.Windows.Forms.Label();
         //this.comboBoxFossilFuelSelection = new System.Windows.Forms.ComboBox();

         //// labelCalculationType
         //this.labelDryingFuelSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         //this.labelDryingFuelSelection.Location = new System.Drawing.Point(0, 20);
         //this.labelDryingFuelSelection.Name = "labelDryingFuelSelection";
         //this.labelDryingFuelSelection.BackColor = Color.DarkGray;
         //this.labelDryingFuelSelection.Size = new System.Drawing.Size(74, 20);
         //this.labelDryingFuelSelection.TabIndex = 5;
         //this.labelDryingFuelSelection.Text = "Select Fuel:";
         //this.labelDryingFuelSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

         //// comboBoxCalculationType
         //this.comboBoxFossilFuelSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         //this.comboBoxFossilFuelSelection.Items.AddRange(fuelCatalog.GetFossilFuelArray());
         //this.comboBoxFossilFuelSelection.Location = new System.Drawing.Point(74, 20);
         //this.comboBoxFossilFuelSelection.Name = "comboBoxCalculationType";
         //this.comboBoxFossilFuelSelection.Size = new System.Drawing.Size(200, 21);
         //this.comboBoxFossilFuelSelection.TabIndex = 7;
         //this.comboBoxFossilFuelSelection.SelectedIndexChanged += new EventHandler(comboBoxFossilFuelSelection_SelectedIndexChanged);

         //this.panel.Controls.Add(this.labelDryingFuelSelection);
         //this.panel.Controls.Add(this.comboBoxFossilFuelSelection);

         //this.comboBoxFossilFuelSelection.SelectedIndex = -1;
         //comboBoxFossilFuelSelection.Enabled = true;

         // 
         // ProcessStreamBaseEditor
         // 
         //this.ClientSize = new System.Drawing.Size(292, 266);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ProcessStreamBaseEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         //this.Text = "ProcessStreamBaseEditor";
         this.ResumeLayout(false);
      }

      //private void comboBoxFossilFuelSelection_SelectedIndexChanged(object sender, EventArgs e)
      //{
      //   string fossilFuelName = comboBoxFossilFuelSelection.SelectedItem.ToString();
      //   FossilFuel fossilFuel = fuelCatalog.GetFossilFuel(fossilFuelName);
      //   DetailedFuelStream myFuleStream = DetailedFuelStreamCtrl.DetailedFuelStream;
      //   myFuleStream.DryingFuel = fossilFuel;
      //}

      //private void DetailedFuelStreamEditor_FossilFuelAdded(FossilFuel fuel)
      //{
      //   FossilFuelCatalogChanged();
      //}

      //private void DetailedFuelStreamEditor_FossilFuelDeleted(string fuleName)
      //{
      //   FossilFuelCatalogChanged();
      //}

      //private void FossilFuelCatalogChanged()
      //{
      //   this.comboBoxFossilFuelSelection.Items.Clear();
      //   this.comboBoxFossilFuelSelection.Items.AddRange(fuelCatalog.GetFossilFuelArray());
      //}
      #endregion
   }
}
