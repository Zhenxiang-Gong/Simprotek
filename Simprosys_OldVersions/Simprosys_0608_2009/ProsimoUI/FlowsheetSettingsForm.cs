using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Materials;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for NewProcessSettingsForm.
   /// </summary>
   public class FlowsheetSettingsForm : System.Windows.Forms.Form {

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.TextBox textBoxDryingMaterial;
      private System.Windows.Forms.Label labelDryingMaterial;
      private System.Windows.Forms.TextBox textBoxDryingGas;
      private System.Windows.Forms.Label labelDryingGas;
      private GroupBox groupBoxSelected;
      private TextBox textBoxMoisture;
      private Label labelMoisture;
      private Button buttonCancel;
      //private IContainer components;

      public FlowsheetSettingsForm() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public FlowsheetSettingsForm(DryingGas dryingGas, DryingMaterial dryingMaterial)
      {
          //
          // Required for Windows Form Designer support
          //
          InitializeComponent();

          string moistureName = dryingMaterial.Moisture.ToString();
          this.textBoxMoisture.Text = moistureName;
          this.textBoxDryingGas.Text = dryingGas.Name;
          this.textBoxDryingMaterial.Text = dryingMaterial.Name;
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultFlowsheetSettingsForm));
         this.panel = new System.Windows.Forms.Panel();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.groupBoxSelected = new System.Windows.Forms.GroupBox();
         this.textBoxMoisture = new System.Windows.Forms.TextBox();
         this.labelMoisture = new System.Windows.Forms.Label();
         this.textBoxDryingMaterial = new System.Windows.Forms.TextBox();
         this.labelDryingGas = new System.Windows.Forms.Label();
         this.textBoxDryingGas = new System.Windows.Forms.TextBox();
         this.labelDryingMaterial = new System.Windows.Forms.Label();
         this.panel.SuspendLayout();
         this.groupBoxSelected.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.Controls.Add(this.buttonCancel);
         this.panel.Controls.Add(this.groupBoxSelected);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Margin = new System.Windows.Forms.Padding(5);
         this.panel.Name = "panel";
         this.panel.Padding = new System.Windows.Forms.Padding(3);
         this.panel.Size = new System.Drawing.Size(477, 155);
         this.panel.TabIndex = 0;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(353, 120);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(75, 23);
         this.buttonCancel.TabIndex = 6;
         this.buttonCancel.Text = "Close";
         this.buttonCancel.Click += new System.EventHandler(this.buttonClose_Click);
         // 
         // groupBoxSelected
         // 
         this.groupBoxSelected.Controls.Add(this.textBoxMoisture);
         this.groupBoxSelected.Controls.Add(this.labelMoisture);
         this.groupBoxSelected.Controls.Add(this.textBoxDryingMaterial);
         this.groupBoxSelected.Controls.Add(this.labelDryingGas);
         this.groupBoxSelected.Controls.Add(this.textBoxDryingGas);
         this.groupBoxSelected.Controls.Add(this.labelDryingMaterial);
         this.groupBoxSelected.Location = new System.Drawing.Point(6, 12);
         this.groupBoxSelected.Name = "groupBoxSelected";
         this.groupBoxSelected.Size = new System.Drawing.Size(462, 91);
         this.groupBoxSelected.TabIndex = 5;
         this.groupBoxSelected.TabStop = false;
         this.groupBoxSelected.Text = "Current Flowsheet Settings";
         // 
         // textBoxMoisture
         // 
         this.textBoxMoisture.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxMoisture.Location = new System.Drawing.Point(322, 25);
         this.textBoxMoisture.Name = "textBoxMoisture";
         this.textBoxMoisture.ReadOnly = true;
         this.textBoxMoisture.Size = new System.Drawing.Size(100, 20);
         this.textBoxMoisture.TabIndex = 18;
         // 
         // labelMoisture
         // 
         this.labelMoisture.Location = new System.Drawing.Point(260, 26);
         this.labelMoisture.Name = "labelMoisture";
         this.labelMoisture.Size = new System.Drawing.Size(56, 19);
         this.labelMoisture.TabIndex = 17;
         this.labelMoisture.Text = "Moisture:";
         this.labelMoisture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxDryingMaterial
         // 
         this.textBoxDryingMaterial.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingMaterial.Location = new System.Drawing.Point(103, 57);
         this.textBoxDryingMaterial.Name = "textBoxDryingMaterial";
         this.textBoxDryingMaterial.ReadOnly = true;
         this.textBoxDryingMaterial.Size = new System.Drawing.Size(319, 20);
         this.textBoxDryingMaterial.TabIndex = 16;
         // 
         // labelDryingGas
         // 
         this.labelDryingGas.Location = new System.Drawing.Point(15, 27);
         this.labelDryingGas.Name = "labelDryingGas";
         this.labelDryingGas.Size = new System.Drawing.Size(84, 16);
         this.labelDryingGas.TabIndex = 13;
         this.labelDryingGas.Text = "Drying Gas:";
         this.labelDryingGas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxDryingGas
         // 
         this.textBoxDryingGas.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingGas.Location = new System.Drawing.Point(103, 25);
         this.textBoxDryingGas.Name = "textBoxDryingGas";
         this.textBoxDryingGas.ReadOnly = true;
         this.textBoxDryingGas.Size = new System.Drawing.Size(100, 20);
         this.textBoxDryingGas.TabIndex = 14;
         // 
         // labelDryingMaterial
         // 
         this.labelDryingMaterial.Location = new System.Drawing.Point(15, 57);
         this.labelDryingMaterial.Name = "labelDryingMaterial";
         this.labelDryingMaterial.Size = new System.Drawing.Size(84, 16);
         this.labelDryingMaterial.TabIndex = 15;
         this.labelDryingMaterial.Text = "Drying Material:";
         this.labelDryingMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // NewFlowsheetSettingsForm
         // 
         this.ClientSize = new System.Drawing.Size(477, 155);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "NewFlowsheetSettingsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Current Flowsheet Settings";
         this.panel.ResumeLayout(false);
         this.groupBoxSelected.ResumeLayout(false);
         this.groupBoxSelected.PerformLayout();
         this.ResumeLayout(false);

      }
      #endregion

      private void menuItemClose_Click(object sender, System.EventArgs e) {
         this.Close();
      }

      private void buttonClose_Click(object sender, EventArgs e) {
         this.Close();
      }
   }
}
