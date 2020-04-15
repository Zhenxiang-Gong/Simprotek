
namespace Prosimo.ThermalPropExtractor {
   partial class MainFormThermalPropExtractor {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.menuStrip = new System.Windows.Forms.MenuStrip();
         this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.fileTab = new System.Windows.Forms.TabControl();
         this.tabPageFiles = new System.Windows.Forms.TabPage();
         this.comboBoxExtractorType = new System.Windows.Forms.ComboBox();
         this.buttonRemoveAll = new System.Windows.Forms.Button();
         this.comboBoxSubstanceType = new System.Windows.Forms.ComboBox();
         this.comboBoxProp = new System.Windows.Forms.ComboBox();
         this.buttonExtractData = new System.Windows.Forms.Button();
         this.buttonRemoveFiles = new System.Windows.Forms.Button();
         this.buttonAddFiles = new System.Windows.Forms.Button();
         this.listBoxFiles = new System.Windows.Forms.ListBox();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.menuStrip.SuspendLayout();
         this.fileTab.SuspendLayout();
         this.tabPageFiles.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip
         // 
         this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
         this.menuStrip.Location = new System.Drawing.Point(0, 0);
         this.menuStrip.Name = "menuStrip";
         this.menuStrip.Size = new System.Drawing.Size(781, 24);
         this.menuStrip.TabIndex = 0;
         this.menuStrip.Text = "menuStrip1";
         // 
         // closeToolStripMenuItem
         // 
         this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         this.closeToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
         this.closeToolStripMenuItem.Text = "Close";
         this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
         // 
         // fileTab
         // 
         this.fileTab.Controls.Add(this.tabPageFiles);
         this.fileTab.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fileTab.Location = new System.Drawing.Point(0, 24);
         this.fileTab.Name = "fileTab";
         this.fileTab.SelectedIndex = 0;
         this.fileTab.Size = new System.Drawing.Size(781, 406);
         this.fileTab.TabIndex = 6;
         this.fileTab.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageFiles_MouseClick);
         // 
         // tabPageFiles
         // 
         this.tabPageFiles.Controls.Add(this.comboBoxExtractorType);
         this.tabPageFiles.Controls.Add(this.buttonRemoveAll);
         this.tabPageFiles.Controls.Add(this.comboBoxSubstanceType);
         this.tabPageFiles.Controls.Add(this.comboBoxProp);
         this.tabPageFiles.Controls.Add(this.buttonExtractData);
         this.tabPageFiles.Controls.Add(this.buttonRemoveFiles);
         this.tabPageFiles.Controls.Add(this.buttonAddFiles);
         this.tabPageFiles.Controls.Add(this.listBoxFiles);
         this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
         this.tabPageFiles.Name = "tabPageFiles";
         this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageFiles.Size = new System.Drawing.Size(773, 380);
         this.tabPageFiles.TabIndex = 1;
         this.tabPageFiles.Text = "File Panel";
         this.tabPageFiles.UseVisualStyleBackColor = true;
         this.tabPageFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageFiles_MouseClick);
         // 
         // comboBoxExtractorType
         // 
         this.comboBoxExtractorType.FormattingEnabled = true;
         this.comboBoxExtractorType.Items.AddRange(new object[] {
            "Perrys",
            "Yaws",
            "Yaws Organic"});
         this.comboBoxExtractorType.Location = new System.Drawing.Point(256, 339);
         this.comboBoxExtractorType.Name = "comboBoxExtractorType";
         this.comboBoxExtractorType.Size = new System.Drawing.Size(121, 21);
         this.comboBoxExtractorType.TabIndex = 7;
         this.comboBoxExtractorType.Text = "Perrys";
         this.comboBoxExtractorType.SelectedIndexChanged += new System.EventHandler(this.comboBoxExtractorType_SelectedIndexChanged);
         // 
         // buttonRemoveAll
         // 
         this.buttonRemoveAll.Location = new System.Drawing.Point(89, 339);
         this.buttonRemoveAll.Name = "buttonRemoveAll";
         this.buttonRemoveAll.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveAll.TabIndex = 6;
         this.buttonRemoveAll.Text = "Remove All";
         this.buttonRemoveAll.UseVisualStyleBackColor = true;
         this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
         // 
         // comboBoxSubstanceType
         // 
         this.comboBoxSubstanceType.FormattingEnabled = true;
         this.comboBoxSubstanceType.Items.AddRange(new object[] {
            "Organic",
            "Inorganic"});
         this.comboBoxSubstanceType.Location = new System.Drawing.Point(400, 339);
         this.comboBoxSubstanceType.Name = "comboBoxSubstanceType";
         this.comboBoxSubstanceType.Size = new System.Drawing.Size(121, 21);
         this.comboBoxSubstanceType.TabIndex = 5;
         this.comboBoxSubstanceType.Text = "Organic";
         this.comboBoxSubstanceType.Visible = false;
         this.comboBoxSubstanceType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSubstanceType_SelectedIndexChanged);
         // 
         // comboBoxProp
         // 
         this.comboBoxProp.FormattingEnabled = true;
         this.comboBoxProp.Items.AddRange(new object[] {
            "Critical Prop",
            "Gas Cp",
            "Liquid Cp",
            "Solid Cp",
            "Evap Heat",
            "Vapor Pressure",
            "Liquid Density",
            "Gas Viscosity",
            "Liquid Viscosity",
            "Gas K",
            "Liquid K",
            "Surface Tension",
            "Enthalpy of Formation"});
         this.comboBoxProp.Location = new System.Drawing.Point(540, 339);
         this.comboBoxProp.Name = "comboBoxProp";
         this.comboBoxProp.Size = new System.Drawing.Size(121, 21);
         this.comboBoxProp.TabIndex = 4;
         this.comboBoxProp.Text = "Critical Prop";
         this.comboBoxProp.SelectedIndexChanged += new System.EventHandler(this.comboBoxProp_SelectedIndexChanged);
         // 
         // buttonExtractData
         // 
         this.buttonExtractData.AllowDrop = true;
         this.buttonExtractData.Location = new System.Drawing.Point(667, 339);
         this.buttonExtractData.Name = "buttonExtractData";
         this.buttonExtractData.Size = new System.Drawing.Size(98, 23);
         this.buttonExtractData.TabIndex = 3;
         this.buttonExtractData.Text = "Extract Data";
         this.buttonExtractData.UseVisualStyleBackColor = true;
         this.buttonExtractData.Click += new System.EventHandler(this.buttonExtractData_Click);
         // 
         // buttonRemoveFiles
         // 
         this.buttonRemoveFiles.Location = new System.Drawing.Point(170, 339);
         this.buttonRemoveFiles.Name = "buttonRemoveFiles";
         this.buttonRemoveFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveFiles.TabIndex = 2;
         this.buttonRemoveFiles.Text = "Remove";
         this.buttonRemoveFiles.UseVisualStyleBackColor = true;
         this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
         // 
         // buttonAddFiles
         // 
         this.buttonAddFiles.Location = new System.Drawing.Point(8, 339);
         this.buttonAddFiles.Name = "buttonAddFiles";
         this.buttonAddFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonAddFiles.TabIndex = 1;
         this.buttonAddFiles.Text = "Add";
         this.buttonAddFiles.UseVisualStyleBackColor = true;
         this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
         // 
         // listBoxFiles
         // 
         this.listBoxFiles.FormattingEnabled = true;
         this.listBoxFiles.HorizontalScrollbar = true;
         this.listBoxFiles.Location = new System.Drawing.Point(6, 6);
         this.listBoxFiles.Name = "listBoxFiles";
         this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
         this.listBoxFiles.Size = new System.Drawing.Size(763, 316);
         this.listBoxFiles.TabIndex = 0;
         // 
         // openFileDialog
         // 
         this.openFileDialog.FileName = "openFileDialog";
         this.openFileDialog.Multiselect = true;
         // 
         // MainFormThermalPropExtractor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(781, 430);
         this.Controls.Add(this.fileTab);
         this.Controls.Add(this.menuStrip);
         this.MainMenuStrip = this.menuStrip;
         this.Name = "MainFormThermalPropExtractor";
         this.Text = "Thermal Property Extractor";
         this.menuStrip.ResumeLayout(false);
         this.menuStrip.PerformLayout();
         this.fileTab.ResumeLayout(false);
         this.tabPageFiles.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip;
      private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
      private System.Windows.Forms.TabControl fileTab;
      private System.Windows.Forms.TabPage tabPageFiles;
      private System.Windows.Forms.Button buttonRemoveAll;
      private System.Windows.Forms.ComboBox comboBoxSubstanceType;
      private System.Windows.Forms.ComboBox comboBoxProp;
      private System.Windows.Forms.Button buttonExtractData;
      private System.Windows.Forms.Button buttonRemoveFiles;
      private System.Windows.Forms.Button buttonAddFiles;
      private System.Windows.Forms.ListBox listBoxFiles;
      private System.Windows.Forms.OpenFileDialog openFileDialog;
      private System.Windows.Forms.ComboBox comboBoxExtractorType;
   }
}