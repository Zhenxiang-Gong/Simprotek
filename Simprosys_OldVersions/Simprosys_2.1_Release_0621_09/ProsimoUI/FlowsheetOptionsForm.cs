using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitSystems;
using ProsimoUI.UnitSystemsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.Materials;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for FlowsheetOptionsForm.
   /// </summary>
   public class FlowsheetOptionsForm : System.Windows.Forms.Form {
      private Flowsheet flowsheet;
      private System.Windows.Forms.TabControl tabControlPrefs;
      private System.Windows.Forms.TabPage tabPageTypes;
      private System.Windows.Forms.MainMenu mainMenu1;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxUnitOpCreation;
      private System.Windows.Forms.RadioButton radioButtonUnitOpWithoutInputAndOutput;
      private System.Windows.Forms.RadioButton radioButtonUnitOpWithInputAndOutput;
      private System.Windows.Forms.RadioButton radioButtonUnitOpWithInput;
      private System.Windows.Forms.GroupBox groupBoxHeatExchangerInletType;
      private System.Windows.Forms.RadioButton radioButtonHeatExchangerColdSideMaterial;
      private System.Windows.Forms.RadioButton radioButtonHeatExchangerColdSideGas;
      private System.Windows.Forms.GroupBox groupBoxHeatExchangerColdSide;
      private System.Windows.Forms.GroupBox groupBoxHeatExchangerHotSide;
      private System.Windows.Forms.RadioButton radioButtonHeatExchangerHotSideMaterial;
      private System.Windows.Forms.RadioButton radioButtonHeatExchangerHotSideGas;
      private System.Windows.Forms.GroupBox groupBoxTeeStreamsType;
      private System.Windows.Forms.RadioButton radioButtonTeeStreamsTypeLiquidMaterial;
      private System.Windows.Forms.RadioButton radioButtonTeeStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxMixerStreamsType;
      private System.Windows.Forms.RadioButton radioButtonMixerStreamsTypeLiquidMaterial;
      private System.Windows.Forms.RadioButton radioButtonMixerStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxValveStreamsType;
      private System.Windows.Forms.RadioButton radioButtonValveStreamsTypeMaterial;
      private System.Windows.Forms.RadioButton radioButtonValveStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxTeeMixerValveStreamsType;
      private System.Windows.Forms.Label labelDryingGas;
      private System.Windows.Forms.TextBox textBoxDryingGas;
      private System.Windows.Forms.TextBox textBoxDryingMaterial;
      private System.Windows.Forms.Label labelDryingMaterial;
      private RadioButton radioButtonMixerStreamsTypeSolidMaterial;
      private RadioButton radioButtonTeeStreamsTypeSolidMaterial;
      private GroupBox groupBoxHeaterStreamsType;
      private RadioButton radioButtonHeaterStreamsTypeMaterial;
      private RadioButton radioButtonHeaterStreamsTypeGas;
      private GroupBox groupBoxCoolerStreamsType;
      private RadioButton radioButtonCoolerStreamsTypeMaterial;
      private RadioButton radioButtonCoolerStreamsTypeGas;
      private IContainer components;

      public FlowsheetOptionsForm(Flowsheet flowsheet) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         if (flowsheet != null) {
            this.flowsheet = flowsheet;
            EvaporationAndDryingSystem system = this.flowsheet.EvaporationAndDryingSystem;
            this.textBoxDryingGas.Text = system.DryingGas.ToString();
            this.textBoxDryingMaterial.Text = system.DryingMaterial.ToString();
            this.SetPreferences();

            this.ResizeEnd += new EventHandler(FlowsheetOptionsForm_ResizeEnd);
         }
      }

      void FlowsheetOptionsForm_ResizeEnd(object sender, EventArgs e) {
         if (this.flowsheet != null) {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
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
         this.components = new System.ComponentModel.Container();
         this.tabControlPrefs = new System.Windows.Forms.TabControl();
         this.tabPageTypes = new System.Windows.Forms.TabPage();
         this.textBoxDryingMaterial = new System.Windows.Forms.TextBox();
         this.labelDryingMaterial = new System.Windows.Forms.Label();
         this.textBoxDryingGas = new System.Windows.Forms.TextBox();
         this.labelDryingGas = new System.Windows.Forms.Label();
         this.groupBoxTeeMixerValveStreamsType = new System.Windows.Forms.GroupBox();
         this.groupBoxCoolerStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonCoolerStreamsTypeMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonCoolerStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxHeaterStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonHeaterStreamsTypeMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonHeaterStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxTeeStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonTeeStreamsTypeSolidMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonTeeStreamsTypeLiquidMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonTeeStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxMixerStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonMixerStreamsTypeSolidMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonMixerStreamsTypeLiquidMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonMixerStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxValveStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonValveStreamsTypeMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonValveStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxHeatExchangerInletType = new System.Windows.Forms.GroupBox();
         this.groupBoxHeatExchangerHotSide = new System.Windows.Forms.GroupBox();
         this.radioButtonHeatExchangerHotSideMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonHeatExchangerHotSideGas = new System.Windows.Forms.RadioButton();
         this.groupBoxHeatExchangerColdSide = new System.Windows.Forms.GroupBox();
         this.radioButtonHeatExchangerColdSideMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonHeatExchangerColdSideGas = new System.Windows.Forms.RadioButton();
         this.groupBoxUnitOpCreation = new System.Windows.Forms.GroupBox();
         this.radioButtonUnitOpWithInput = new System.Windows.Forms.RadioButton();
         this.radioButtonUnitOpWithInputAndOutput = new System.Windows.Forms.RadioButton();
         this.radioButtonUnitOpWithoutInputAndOutput = new System.Windows.Forms.RadioButton();
         this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.tabControlPrefs.SuspendLayout();
         this.tabPageTypes.SuspendLayout();
         this.groupBoxTeeMixerValveStreamsType.SuspendLayout();
         this.groupBoxCoolerStreamsType.SuspendLayout();
         this.groupBoxHeaterStreamsType.SuspendLayout();
         this.groupBoxTeeStreamsType.SuspendLayout();
         this.groupBoxMixerStreamsType.SuspendLayout();
         this.groupBoxValveStreamsType.SuspendLayout();
         this.groupBoxHeatExchangerInletType.SuspendLayout();
         this.groupBoxHeatExchangerHotSide.SuspendLayout();
         this.groupBoxHeatExchangerColdSide.SuspendLayout();
         this.groupBoxUnitOpCreation.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // tabControlPrefs
         // 
         this.tabControlPrefs.Controls.Add(this.tabPageTypes);
         this.tabControlPrefs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControlPrefs.Location = new System.Drawing.Point(0, 0);
         this.tabControlPrefs.Name = "tabControlPrefs";
         this.tabControlPrefs.SelectedIndex = 0;
         this.tabControlPrefs.Size = new System.Drawing.Size(414, 418);
         this.tabControlPrefs.TabIndex = 4;
         // 
         // tabPageTypes
         // 
         this.tabPageTypes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageTypes.Controls.Add(this.textBoxDryingMaterial);
         this.tabPageTypes.Controls.Add(this.labelDryingMaterial);
         this.tabPageTypes.Controls.Add(this.textBoxDryingGas);
         this.tabPageTypes.Controls.Add(this.labelDryingGas);
         this.tabPageTypes.Controls.Add(this.groupBoxTeeMixerValveStreamsType);
         this.tabPageTypes.Controls.Add(this.groupBoxHeatExchangerInletType);
         this.tabPageTypes.Controls.Add(this.groupBoxUnitOpCreation);
         this.tabPageTypes.Location = new System.Drawing.Point(4, 22);
         this.tabPageTypes.Name = "tabPageTypes";
         this.tabPageTypes.Size = new System.Drawing.Size(406, 392);
         this.tabPageTypes.TabIndex = 0;
         // 
         // textBoxDryingMaterial
         // 
         this.textBoxDryingMaterial.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingMaterial.Location = new System.Drawing.Point(100, 52);
         this.textBoxDryingMaterial.Name = "textBoxDryingMaterial";
         this.textBoxDryingMaterial.ReadOnly = true;
         this.textBoxDryingMaterial.Size = new System.Drawing.Size(100, 20);
         this.textBoxDryingMaterial.TabIndex = 12;
         // 
         // labelDryingMaterial
         // 
         this.labelDryingMaterial.Location = new System.Drawing.Point(12, 56);
         this.labelDryingMaterial.Name = "labelDryingMaterial";
         this.labelDryingMaterial.Size = new System.Drawing.Size(84, 16);
         this.labelDryingMaterial.TabIndex = 11;
         this.labelDryingMaterial.Text = "Drying Material:";
         this.labelDryingMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxDryingGas
         // 
         this.textBoxDryingGas.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDryingGas.Location = new System.Drawing.Point(100, 24);
         this.textBoxDryingGas.Name = "textBoxDryingGas";
         this.textBoxDryingGas.ReadOnly = true;
         this.textBoxDryingGas.Size = new System.Drawing.Size(100, 20);
         this.textBoxDryingGas.TabIndex = 10;
         // 
         // labelDryingGas
         // 
         this.labelDryingGas.Location = new System.Drawing.Point(12, 28);
         this.labelDryingGas.Name = "labelDryingGas";
         this.labelDryingGas.Size = new System.Drawing.Size(84, 16);
         this.labelDryingGas.TabIndex = 9;
         this.labelDryingGas.Text = "Drying Gas:";
         this.labelDryingGas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBoxTeeMixerValveStreamsType
         // 
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxCoolerStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxHeaterStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxTeeStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxMixerStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxValveStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Location = new System.Drawing.Point(224, 4);
         this.groupBoxTeeMixerValveStreamsType.Name = "groupBoxTeeMixerValveStreamsType";
         this.groupBoxTeeMixerValveStreamsType.Size = new System.Drawing.Size(174, 381);
         this.groupBoxTeeMixerValveStreamsType.TabIndex = 8;
         this.groupBoxTeeMixerValveStreamsType.TabStop = false;
         this.groupBoxTeeMixerValveStreamsType.Text = "Inlet Stream Type";
         // 
         // groupBoxCoolerStreamsType
         // 
         this.groupBoxCoolerStreamsType.Controls.Add(this.radioButtonCoolerStreamsTypeMaterial);
         this.groupBoxCoolerStreamsType.Controls.Add(this.radioButtonCoolerStreamsTypeGas);
         this.groupBoxCoolerStreamsType.Location = new System.Drawing.Point(16, 146);
         this.groupBoxCoolerStreamsType.Name = "groupBoxCoolerStreamsType";
         this.groupBoxCoolerStreamsType.Size = new System.Drawing.Size(131, 56);
         this.groupBoxCoolerStreamsType.TabIndex = 9;
         this.groupBoxCoolerStreamsType.TabStop = false;
         this.groupBoxCoolerStreamsType.Text = "Cooler";
         // 
         // radioButtonCoolerStreamsTypeMaterial
         // 
         this.radioButtonCoolerStreamsTypeMaterial.Location = new System.Drawing.Point(8, 36);
         this.radioButtonCoolerStreamsTypeMaterial.Name = "radioButtonCoolerStreamsTypeMaterial";
         this.radioButtonCoolerStreamsTypeMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonCoolerStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonCoolerStreamsTypeMaterial.Text = "Material";
         this.radioButtonCoolerStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.CoolerStreamsTypeHandler);
         // 
         // radioButtonCoolerStreamsTypeGas
         // 
         this.radioButtonCoolerStreamsTypeGas.Location = new System.Drawing.Point(8, 16);
         this.radioButtonCoolerStreamsTypeGas.Name = "radioButtonCoolerStreamsTypeGas";
         this.radioButtonCoolerStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonCoolerStreamsTypeGas.TabIndex = 0;
         this.radioButtonCoolerStreamsTypeGas.Text = "Gas";
         this.radioButtonCoolerStreamsTypeGas.CheckedChanged += new System.EventHandler(this.CoolerStreamsTypeHandler);
         // 
         // groupBoxHeaterStreamsType
         // 
         this.groupBoxHeaterStreamsType.Controls.Add(this.radioButtonHeaterStreamsTypeMaterial);
         this.groupBoxHeaterStreamsType.Controls.Add(this.radioButtonHeaterStreamsTypeGas);
         this.groupBoxHeaterStreamsType.Location = new System.Drawing.Point(16, 84);
         this.groupBoxHeaterStreamsType.Name = "groupBoxHeaterStreamsType";
         this.groupBoxHeaterStreamsType.Size = new System.Drawing.Size(131, 56);
         this.groupBoxHeaterStreamsType.TabIndex = 8;
         this.groupBoxHeaterStreamsType.TabStop = false;
         this.groupBoxHeaterStreamsType.Text = "Heater";
         // 
         // radioButtonHeaterStreamsTypeMaterial
         // 
         this.radioButtonHeaterStreamsTypeMaterial.Location = new System.Drawing.Point(8, 36);
         this.radioButtonHeaterStreamsTypeMaterial.Name = "radioButtonHeaterStreamsTypeMaterial";
         this.radioButtonHeaterStreamsTypeMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonHeaterStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonHeaterStreamsTypeMaterial.Text = "Material";
         this.radioButtonHeaterStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.HeaterStreamsTypeHandler);
         // 
         // radioButtonHeaterStreamsTypeGas
         // 
         this.radioButtonHeaterStreamsTypeGas.Location = new System.Drawing.Point(8, 16);
         this.radioButtonHeaterStreamsTypeGas.Name = "radioButtonHeaterStreamsTypeGas";
         this.radioButtonHeaterStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonHeaterStreamsTypeGas.TabIndex = 0;
         this.radioButtonHeaterStreamsTypeGas.Text = "Gas";
         this.radioButtonHeaterStreamsTypeGas.CheckedChanged += new System.EventHandler(this.HeaterStreamsTypeHandler);
         // 
         // groupBoxTeeStreamsType
         // 
         this.groupBoxTeeStreamsType.Controls.Add(this.radioButtonTeeStreamsTypeSolidMaterial);
         this.groupBoxTeeStreamsType.Controls.Add(this.radioButtonTeeStreamsTypeLiquidMaterial);
         this.groupBoxTeeStreamsType.Controls.Add(this.radioButtonTeeStreamsTypeGas);
         this.groupBoxTeeStreamsType.Location = new System.Drawing.Point(16, 210);
         this.groupBoxTeeStreamsType.Name = "groupBoxTeeStreamsType";
         this.groupBoxTeeStreamsType.Size = new System.Drawing.Size(131, 78);
         this.groupBoxTeeStreamsType.TabIndex = 5;
         this.groupBoxTeeStreamsType.TabStop = false;
         this.groupBoxTeeStreamsType.Text = "Tee";
         // 
         // radioButtonTeeStreamsTypeSolidMaterial
         // 
         this.radioButtonTeeStreamsTypeSolidMaterial.Location = new System.Drawing.Point(8, 56);
         this.radioButtonTeeStreamsTypeSolidMaterial.Name = "radioButtonTeeStreamsTypeSolidMaterial";
         this.radioButtonTeeStreamsTypeSolidMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonTeeStreamsTypeSolidMaterial.TabIndex = 2;
         this.radioButtonTeeStreamsTypeSolidMaterial.Text = "Solid Material";
         this.radioButtonTeeStreamsTypeSolidMaterial.CheckedChanged += new System.EventHandler(this.TeeStreamsTypeHandler);
         // 
         // radioButtonTeeStreamsTypeLiquidMaterial
         // 
         this.radioButtonTeeStreamsTypeLiquidMaterial.Location = new System.Drawing.Point(8, 36);
         this.radioButtonTeeStreamsTypeLiquidMaterial.Name = "radioButtonTeeStreamsTypeLiquidMaterial";
         this.radioButtonTeeStreamsTypeLiquidMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonTeeStreamsTypeLiquidMaterial.TabIndex = 1;
         this.radioButtonTeeStreamsTypeLiquidMaterial.Text = "Liquid Material";
         this.radioButtonTeeStreamsTypeLiquidMaterial.CheckedChanged += new System.EventHandler(this.TeeStreamsTypeHandler);
         // 
         // radioButtonTeeStreamsTypeGas
         // 
         this.radioButtonTeeStreamsTypeGas.Location = new System.Drawing.Point(8, 16);
         this.radioButtonTeeStreamsTypeGas.Name = "radioButtonTeeStreamsTypeGas";
         this.radioButtonTeeStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonTeeStreamsTypeGas.TabIndex = 0;
         this.radioButtonTeeStreamsTypeGas.Text = "Gas";
         this.radioButtonTeeStreamsTypeGas.CheckedChanged += new System.EventHandler(this.TeeStreamsTypeHandler);
         // 
         // groupBoxMixerStreamsType
         // 
         this.groupBoxMixerStreamsType.Controls.Add(this.radioButtonMixerStreamsTypeSolidMaterial);
         this.groupBoxMixerStreamsType.Controls.Add(this.radioButtonMixerStreamsTypeLiquidMaterial);
         this.groupBoxMixerStreamsType.Controls.Add(this.radioButtonMixerStreamsTypeGas);
         this.groupBoxMixerStreamsType.Location = new System.Drawing.Point(16, 296);
         this.groupBoxMixerStreamsType.Name = "groupBoxMixerStreamsType";
         this.groupBoxMixerStreamsType.Size = new System.Drawing.Size(131, 78);
         this.groupBoxMixerStreamsType.TabIndex = 6;
         this.groupBoxMixerStreamsType.TabStop = false;
         this.groupBoxMixerStreamsType.Text = "Mixer";
         // 
         // radioButtonMixerStreamsTypeSolidMaterial
         // 
         this.radioButtonMixerStreamsTypeSolidMaterial.Location = new System.Drawing.Point(8, 56);
         this.radioButtonMixerStreamsTypeSolidMaterial.Name = "radioButtonMixerStreamsTypeSolidMaterial";
         this.radioButtonMixerStreamsTypeSolidMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonMixerStreamsTypeSolidMaterial.TabIndex = 2;
         this.radioButtonMixerStreamsTypeSolidMaterial.Text = "Solid Material";
         this.radioButtonMixerStreamsTypeSolidMaterial.CheckedChanged += new System.EventHandler(this.MixerStreamsTypeHandler);
         // 
         // radioButtonMixerStreamsTypeLiquidMaterial
         // 
         this.radioButtonMixerStreamsTypeLiquidMaterial.Location = new System.Drawing.Point(8, 36);
         this.radioButtonMixerStreamsTypeLiquidMaterial.Name = "radioButtonMixerStreamsTypeLiquidMaterial";
         this.radioButtonMixerStreamsTypeLiquidMaterial.Size = new System.Drawing.Size(100, 16);
         this.radioButtonMixerStreamsTypeLiquidMaterial.TabIndex = 1;
         this.radioButtonMixerStreamsTypeLiquidMaterial.Text = "Liquid Material";
         this.radioButtonMixerStreamsTypeLiquidMaterial.CheckedChanged += new System.EventHandler(this.MixerStreamsTypeHandler);
         // 
         // radioButtonMixerStreamsTypeGas
         // 
         this.radioButtonMixerStreamsTypeGas.Location = new System.Drawing.Point(8, 16);
         this.radioButtonMixerStreamsTypeGas.Name = "radioButtonMixerStreamsTypeGas";
         this.radioButtonMixerStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonMixerStreamsTypeGas.TabIndex = 0;
         this.radioButtonMixerStreamsTypeGas.Text = "Gas";
         this.radioButtonMixerStreamsTypeGas.CheckedChanged += new System.EventHandler(this.MixerStreamsTypeHandler);
         // 
         // groupBoxValveStreamsType
         // 
         this.groupBoxValveStreamsType.Controls.Add(this.radioButtonValveStreamsTypeMaterial);
         this.groupBoxValveStreamsType.Controls.Add(this.radioButtonValveStreamsTypeGas);
         this.groupBoxValveStreamsType.Location = new System.Drawing.Point(16, 22);
         this.groupBoxValveStreamsType.Name = "groupBoxValveStreamsType";
         this.groupBoxValveStreamsType.Size = new System.Drawing.Size(131, 56);
         this.groupBoxValveStreamsType.TabIndex = 7;
         this.groupBoxValveStreamsType.TabStop = false;
         this.groupBoxValveStreamsType.Text = "Valve";
         // 
         // radioButtonValveStreamsTypeMaterial
         // 
         this.radioButtonValveStreamsTypeMaterial.Location = new System.Drawing.Point(8, 36);
         this.radioButtonValveStreamsTypeMaterial.Name = "radioButtonValveStreamsTypeMaterial";
         this.radioButtonValveStreamsTypeMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonValveStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonValveStreamsTypeMaterial.Text = "Material";
         this.radioButtonValveStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.ValveStreamsTypeHandler);
         // 
         // radioButtonValveStreamsTypeGas
         // 
         this.radioButtonValveStreamsTypeGas.Location = new System.Drawing.Point(8, 16);
         this.radioButtonValveStreamsTypeGas.Name = "radioButtonValveStreamsTypeGas";
         this.radioButtonValveStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonValveStreamsTypeGas.TabIndex = 0;
         this.radioButtonValveStreamsTypeGas.Text = "Gas";
         this.radioButtonValveStreamsTypeGas.CheckedChanged += new System.EventHandler(this.ValveStreamsTypeHandler);
         // 
         // groupBoxHeatExchangerInletType
         // 
         this.groupBoxHeatExchangerInletType.Controls.Add(this.groupBoxHeatExchangerHotSide);
         this.groupBoxHeatExchangerInletType.Controls.Add(this.groupBoxHeatExchangerColdSide);
         this.groupBoxHeatExchangerInletType.Location = new System.Drawing.Point(12, 206);
         this.groupBoxHeatExchangerInletType.Name = "groupBoxHeatExchangerInletType";
         this.groupBoxHeatExchangerInletType.Size = new System.Drawing.Size(188, 88);
         this.groupBoxHeatExchangerInletType.TabIndex = 4;
         this.groupBoxHeatExchangerInletType.TabStop = false;
         this.groupBoxHeatExchangerInletType.Text = "Heat Exchanger Inlet Type";
         // 
         // groupBoxHeatExchangerHotSide
         // 
         this.groupBoxHeatExchangerHotSide.Controls.Add(this.radioButtonHeatExchangerHotSideMaterial);
         this.groupBoxHeatExchangerHotSide.Controls.Add(this.radioButtonHeatExchangerHotSideGas);
         this.groupBoxHeatExchangerHotSide.Location = new System.Drawing.Point(92, 20);
         this.groupBoxHeatExchangerHotSide.Name = "groupBoxHeatExchangerHotSide";
         this.groupBoxHeatExchangerHotSide.Size = new System.Drawing.Size(80, 60);
         this.groupBoxHeatExchangerHotSide.TabIndex = 3;
         this.groupBoxHeatExchangerHotSide.TabStop = false;
         this.groupBoxHeatExchangerHotSide.Text = "Hot Side";
         // 
         // radioButtonHeatExchangerHotSideMaterial
         // 
         this.radioButtonHeatExchangerHotSideMaterial.Location = new System.Drawing.Point(8, 40);
         this.radioButtonHeatExchangerHotSideMaterial.Name = "radioButtonHeatExchangerHotSideMaterial";
         this.radioButtonHeatExchangerHotSideMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonHeatExchangerHotSideMaterial.TabIndex = 1;
         this.radioButtonHeatExchangerHotSideMaterial.Text = "Material";
         this.radioButtonHeatExchangerHotSideMaterial.CheckedChanged += new System.EventHandler(this.HeatExchangerHotInletHandler);
         // 
         // radioButtonHeatExchangerHotSideGas
         // 
         this.radioButtonHeatExchangerHotSideGas.Location = new System.Drawing.Point(8, 20);
         this.radioButtonHeatExchangerHotSideGas.Name = "radioButtonHeatExchangerHotSideGas";
         this.radioButtonHeatExchangerHotSideGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonHeatExchangerHotSideGas.TabIndex = 0;
         this.radioButtonHeatExchangerHotSideGas.Text = "Gas";
         this.radioButtonHeatExchangerHotSideGas.CheckedChanged += new System.EventHandler(this.HeatExchangerHotInletHandler);
         // 
         // groupBoxHeatExchangerColdSide
         // 
         this.groupBoxHeatExchangerColdSide.Controls.Add(this.radioButtonHeatExchangerColdSideMaterial);
         this.groupBoxHeatExchangerColdSide.Controls.Add(this.radioButtonHeatExchangerColdSideGas);
         this.groupBoxHeatExchangerColdSide.Location = new System.Drawing.Point(8, 20);
         this.groupBoxHeatExchangerColdSide.Name = "groupBoxHeatExchangerColdSide";
         this.groupBoxHeatExchangerColdSide.Size = new System.Drawing.Size(80, 60);
         this.groupBoxHeatExchangerColdSide.TabIndex = 2;
         this.groupBoxHeatExchangerColdSide.TabStop = false;
         this.groupBoxHeatExchangerColdSide.Text = "Cold Side";
         // 
         // radioButtonHeatExchangerColdSideMaterial
         // 
         this.radioButtonHeatExchangerColdSideMaterial.Location = new System.Drawing.Point(8, 40);
         this.radioButtonHeatExchangerColdSideMaterial.Name = "radioButtonHeatExchangerColdSideMaterial";
         this.radioButtonHeatExchangerColdSideMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonHeatExchangerColdSideMaterial.TabIndex = 1;
         this.radioButtonHeatExchangerColdSideMaterial.Text = "Material";
         this.radioButtonHeatExchangerColdSideMaterial.CheckedChanged += new System.EventHandler(this.HeatExchangerColdInletHandler);
         // 
         // radioButtonHeatExchangerColdSideGas
         // 
         this.radioButtonHeatExchangerColdSideGas.Location = new System.Drawing.Point(8, 20);
         this.radioButtonHeatExchangerColdSideGas.Name = "radioButtonHeatExchangerColdSideGas";
         this.radioButtonHeatExchangerColdSideGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonHeatExchangerColdSideGas.TabIndex = 0;
         this.radioButtonHeatExchangerColdSideGas.Text = "Gas";
         this.radioButtonHeatExchangerColdSideGas.CheckedChanged += new System.EventHandler(this.HeatExchangerColdInletHandler);
         // 
         // groupBoxUnitOpCreation
         // 
         this.groupBoxUnitOpCreation.Controls.Add(this.radioButtonUnitOpWithInput);
         this.groupBoxUnitOpCreation.Controls.Add(this.radioButtonUnitOpWithInputAndOutput);
         this.groupBoxUnitOpCreation.Controls.Add(this.radioButtonUnitOpWithoutInputAndOutput);
         this.groupBoxUnitOpCreation.Location = new System.Drawing.Point(13, 108);
         this.groupBoxUnitOpCreation.Name = "groupBoxUnitOpCreation";
         this.groupBoxUnitOpCreation.Size = new System.Drawing.Size(187, 80);
         this.groupBoxUnitOpCreation.TabIndex = 3;
         this.groupBoxUnitOpCreation.TabStop = false;
         this.groupBoxUnitOpCreation.Text = "Unit Operation Creation";
         // 
         // radioButtonUnitOpWithInput
         // 
         this.radioButtonUnitOpWithInput.Location = new System.Drawing.Point(8, 40);
         this.radioButtonUnitOpWithInput.Name = "radioButtonUnitOpWithInput";
         this.radioButtonUnitOpWithInput.Size = new System.Drawing.Size(152, 16);
         this.radioButtonUnitOpWithInput.TabIndex = 2;
         this.radioButtonUnitOpWithInput.Text = "With Input Only";
         this.radioButtonUnitOpWithInput.CheckedChanged += new System.EventHandler(this.UnitOperationCreationHandler);
         // 
         // radioButtonUnitOpWithInputAndOutput
         // 
         this.radioButtonUnitOpWithInputAndOutput.Location = new System.Drawing.Point(8, 20);
         this.radioButtonUnitOpWithInputAndOutput.Name = "radioButtonUnitOpWithInputAndOutput";
         this.radioButtonUnitOpWithInputAndOutput.Size = new System.Drawing.Size(152, 16);
         this.radioButtonUnitOpWithInputAndOutput.TabIndex = 1;
         this.radioButtonUnitOpWithInputAndOutput.Text = "With Input and Output";
         this.radioButtonUnitOpWithInputAndOutput.CheckedChanged += new System.EventHandler(this.UnitOperationCreationHandler);
         // 
         // radioButtonUnitOpWithoutInputAndOutput
         // 
         this.radioButtonUnitOpWithoutInputAndOutput.Location = new System.Drawing.Point(8, 60);
         this.radioButtonUnitOpWithoutInputAndOutput.Name = "radioButtonUnitOpWithoutInputAndOutput";
         this.radioButtonUnitOpWithoutInputAndOutput.Size = new System.Drawing.Size(152, 16);
         this.radioButtonUnitOpWithoutInputAndOutput.TabIndex = 0;
         this.radioButtonUnitOpWithoutInputAndOutput.Text = "Without Input and Output";
         this.radioButtonUnitOpWithoutInputAndOutput.CheckedChanged += new System.EventHandler(this.UnitOperationCreationHandler);
         // 
         // mainMenu1
         // 
         this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.tabControlPrefs);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(418, 422);
         this.panel.TabIndex = 5;
         // 
         // FlowsheetOptionsForm
         // 
         this.ClientSize = new System.Drawing.Size(418, 422);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu1;
         this.MinimizeBox = false;
         this.Name = "FlowsheetOptionsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Flowsheet Options";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.FlowsheetOptionsForm_Closing);
         this.tabControlPrefs.ResumeLayout(false);
         this.tabPageTypes.ResumeLayout(false);
         this.tabPageTypes.PerformLayout();
         this.groupBoxTeeMixerValveStreamsType.ResumeLayout(false);
         this.groupBoxCoolerStreamsType.ResumeLayout(false);
         this.groupBoxHeaterStreamsType.ResumeLayout(false);
         this.groupBoxTeeStreamsType.ResumeLayout(false);
         this.groupBoxMixerStreamsType.ResumeLayout(false);
         this.groupBoxValveStreamsType.ResumeLayout(false);
         this.groupBoxHeatExchangerInletType.ResumeLayout(false);
         this.groupBoxHeatExchangerHotSide.ResumeLayout(false);
         this.groupBoxHeatExchangerColdSide.ResumeLayout(false);
         this.groupBoxUnitOpCreation.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      public void SetPreferences() {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;

         if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithInputOnly)
            this.radioButtonUnitOpWithInput.Checked = true;
         else if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithInputAndOutput)
            this.radioButtonUnitOpWithInputAndOutput.Checked = true;
         else if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithoutInputAndOutput)
            this.radioButtonUnitOpWithoutInputAndOutput.Checked = true;

         if (sysPrefs.HeatExchangerColdInletType == typeof(DryingGasStream))
            this.radioButtonHeatExchangerColdSideGas.Checked = true;
         else if (sysPrefs.HeatExchangerColdInletType == typeof(LiquidDryingMaterialStream))
            this.radioButtonHeatExchangerColdSideMaterial.Checked = true;

         if (sysPrefs.HeatExchangerHotInletType == typeof(DryingGasStream))
            this.radioButtonHeatExchangerHotSideGas.Checked = true;
         else if (sysPrefs.HeatExchangerHotInletType == typeof(LiquidDryingMaterialStream))
            this.radioButtonHeatExchangerHotSideMaterial.Checked = true;

         if (sysPrefs.MixerInletStreamType == typeof(DryingGasStream))
            this.radioButtonMixerStreamsTypeGas.Checked = true;
         else if (sysPrefs.MixerInletStreamType == typeof(LiquidDryingMaterialStream))
            this.radioButtonMixerStreamsTypeLiquidMaterial.Checked = true;
         else if (sysPrefs.MixerInletStreamType == typeof(SolidDryingMaterialStream))
            this.radioButtonMixerStreamsTypeSolidMaterial.Checked = true;

         if (sysPrefs.ValveInletStreamType == typeof(DryingGasStream))
            this.radioButtonValveStreamsTypeGas.Checked = true;
         else if (sysPrefs.ValveInletStreamType == typeof(LiquidDryingMaterialStream))
            this.radioButtonValveStreamsTypeMaterial.Checked = true;

         if (sysPrefs.TeeInletStreamType == typeof(DryingGasStream))
            this.radioButtonTeeStreamsTypeGas.Checked = true;
         else if (sysPrefs.TeeInletStreamType == typeof(LiquidDryingMaterialStream))
            this.radioButtonTeeStreamsTypeLiquidMaterial.Checked = true;
         else if (sysPrefs.TeeInletStreamType == typeof(SolidDryingMaterialStream))
            this.radioButtonTeeStreamsTypeSolidMaterial.Checked = true;

         if (sysPrefs.HeaterInletStreamType == typeof(DryingGasStream))
            this.radioButtonHeaterStreamsTypeGas.Checked = true;
         else if (sysPrefs.HeaterInletStreamType == typeof(LiquidDryingMaterialStream))
            this.radioButtonHeaterStreamsTypeMaterial.Checked = true;

         if (sysPrefs.CoolerInletStreamType == typeof(DryingGasStream))
            this.radioButtonCoolerStreamsTypeGas.Checked = true;
         else if (sysPrefs.CoolerInletStreamType == typeof(LiquidDryingMaterialStream))
            this.radioButtonCoolerStreamsTypeMaterial.Checked = true;
      }


      private void menuItemClose_Click(object sender, System.EventArgs e) {
         this.Close();
      }

      private void FlowsheetOptionsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
         // nothing at this moment
      }

      private void UnitOperationCreationHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonUnitOpWithInput) {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithInputOnly;
         }
         else if (rb == this.radioButtonUnitOpWithInputAndOutput) {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithInputAndOutput;
         }
         else if (rb == this.radioButtonUnitOpWithoutInputAndOutput) {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithoutInputAndOutput;
         }
      }

      private void HeatExchangerColdInletHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonHeatExchangerColdSideGas) {
            if (rb.Checked)
               sysPrefs.HeatExchangerColdInletType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonHeatExchangerColdSideMaterial) {
            if (rb.Checked)
               sysPrefs.HeatExchangerColdInletType = typeof(LiquidDryingMaterialStream);
         }
      }

      private void HeatExchangerHotInletHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonHeatExchangerHotSideGas) {
            if (rb.Checked)
               sysPrefs.HeatExchangerHotInletType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonHeatExchangerHotSideMaterial) {
            if (rb.Checked)
               sysPrefs.HeatExchangerHotInletType = typeof(LiquidDryingMaterialStream);
         }
      }

      private void TeeStreamsTypeHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonTeeStreamsTypeGas) {
            if (rb.Checked) {
               sysPrefs.TeeInletStreamType = typeof(DryingGasStream);
            }
         }
         else if (rb == this.radioButtonTeeStreamsTypeLiquidMaterial) {
            if (rb.Checked) {
               sysPrefs.TeeInletStreamType = typeof(LiquidDryingMaterialStream);
            }
         }
         else if (rb == this.radioButtonTeeStreamsTypeSolidMaterial) {
            if (rb.Checked) {
               sysPrefs.TeeInletStreamType = typeof(SolidDryingMaterialStream);
            }
         }
      }

      private void MixerStreamsTypeHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonMixerStreamsTypeGas) {
            if (rb.Checked) {
               sysPrefs.MixerInletStreamType = typeof(DryingGasStream);
            }
         }
         else if (rb == this.radioButtonMixerStreamsTypeLiquidMaterial) {
            if (rb.Checked) {
               sysPrefs.MixerInletStreamType = typeof(LiquidDryingMaterialStream);
            }
         }
         else if (rb == this.radioButtonMixerStreamsTypeSolidMaterial) {
            if (rb.Checked) {
               sysPrefs.MixerInletStreamType = typeof(SolidDryingMaterialStream);
            }
         }
      }

      private void ValveStreamsTypeHandler(object sender, System.EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonValveStreamsTypeGas) {
            if (rb.Checked) {
               sysPrefs.ValveInletStreamType = typeof(DryingGasStream);
            }
         }
         else if (rb == this.radioButtonValveStreamsTypeMaterial) {
            if (rb.Checked) {
               sysPrefs.ValveInletStreamType = typeof(LiquidDryingMaterialStream);
            }
         }
      }

      private void HeaterStreamsTypeHandler(object sender, EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonHeaterStreamsTypeGas) {
            if (rb.Checked) {
               sysPrefs.HeaterInletStreamType = typeof(DryingGasStream);
            }
         }
         else if (rb == this.radioButtonHeaterStreamsTypeMaterial) {
            if (rb.Checked) {
               sysPrefs.HeaterInletStreamType = typeof(LiquidDryingMaterialStream);
            }
         }
      }

      private void CoolerStreamsTypeHandler(object sender, EventArgs e) {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonCoolerStreamsTypeGas) {
            if (rb.Checked) {
               sysPrefs.CoolerInletStreamType = typeof(DryingGasStream);
            }
         }
         else if (rb == this.radioButtonCoolerStreamsTypeMaterial) {
            if (rb.Checked) {
               sysPrefs.CoolerInletStreamType = typeof(LiquidDryingMaterialStream);
            }
         }
      }
   }
}
