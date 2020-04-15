using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.LiquidSolidSeparation;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.VaporLiquidSeparation;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for Toolbox.
   /// </summary>
   public class Toolbox : System.Windows.Forms.Form {
      private Flowsheet flowsheet;
      private System.Windows.Forms.RadioButton radioButtonPump;
      private System.Windows.Forms.RadioButton radioButtonHeater;
      private System.Windows.Forms.RadioButton radioButtonFan;
      private System.Windows.Forms.RadioButton radioButtonCooler;
      private System.Windows.Forms.RadioButton radioButtonCompressor;
      private System.Windows.Forms.RadioButton radioButtonCyclone;
      private System.Windows.Forms.RadioButton radioButtonGasStream;
      private System.Windows.Forms.RadioButton radioButtonBagFilter;
      private System.Windows.Forms.RadioButton radioButtonValve;
      private System.Windows.Forms.RadioButton radioButtonFlashTank;
      private System.Windows.Forms.RadioButton radioButtonAirFilter;
      private System.Windows.Forms.RadioButton radioButtonMixer;
      private System.Windows.Forms.RadioButton radioButtonTee;
      private System.Windows.Forms.RadioButton radioButtonHeatExchanger;
      private System.Windows.Forms.RadioButton radioButtonRecycle;
      private System.Windows.Forms.RadioButton radioButtonElectrostaticPrecipitator;
      private System.Windows.Forms.RadioButton radioButtonEjector;
      private System.Windows.Forms.RadioButton radioButtonWetScrubber;
      private System.Windows.Forms.RadioButton radioButtonLiquidMaterialStream;
      private System.Windows.Forms.RadioButton radioButtonSolidMaterialStream;
      private System.Windows.Forms.RadioButton radioButtonDryerLiquid;
      private System.Windows.Forms.RadioButton radioButtonDryerSolid;
      private RadioButton radioButtonScrubberCondenser;
      public FlowLayoutPanel toolBoxPanel;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public Toolbox() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public Toolbox(MainForm mf) {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.Owner = mf;
         this.SetToolTips();

         //this.flowsheet.MouseDown += new MouseEventHandler(flowsheet_MouseDown);
         //this.LocationChanged += new EventHandler(Toolbox_LocationChanged);
         //this.flowsheet.ActivityChanged += new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
         this.ResizeEnd += new EventHandler(Toolbox_ResizeEnd);
      }

      internal void SetFlowsheet(Flowsheet fs) {
         if (this.flowsheet != null) {
            this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
         }
         this.flowsheet = fs;
         this.flowsheet.ActivityChanged += new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
      }

      void Toolbox_ResizeEnd(object sender, EventArgs e) {
         if (this.flowsheet != null) {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (this.flowsheet != null)
            this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler<ActivityChangedEventArgs>(flowsheet_ActivityChanged);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbox));
         this.radioButtonCyclone = new System.Windows.Forms.RadioButton();
         this.radioButtonCompressor = new System.Windows.Forms.RadioButton();
         this.radioButtonCooler = new System.Windows.Forms.RadioButton();
         this.radioButtonFan = new System.Windows.Forms.RadioButton();
         this.radioButtonHeater = new System.Windows.Forms.RadioButton();
         this.radioButtonPump = new System.Windows.Forms.RadioButton();
         this.radioButtonDryerLiquid = new System.Windows.Forms.RadioButton();
         this.radioButtonGasStream = new System.Windows.Forms.RadioButton();
         this.radioButtonSolidMaterialStream = new System.Windows.Forms.RadioButton();
         this.radioButtonScrubberCondenser = new System.Windows.Forms.RadioButton();
         this.radioButtonDryerSolid = new System.Windows.Forms.RadioButton();
         this.radioButtonLiquidMaterialStream = new System.Windows.Forms.RadioButton();
         this.radioButtonWetScrubber = new System.Windows.Forms.RadioButton();
         this.radioButtonEjector = new System.Windows.Forms.RadioButton();
         this.radioButtonElectrostaticPrecipitator = new System.Windows.Forms.RadioButton();
         this.radioButtonRecycle = new System.Windows.Forms.RadioButton();
         this.radioButtonHeatExchanger = new System.Windows.Forms.RadioButton();
         this.radioButtonTee = new System.Windows.Forms.RadioButton();
         this.radioButtonMixer = new System.Windows.Forms.RadioButton();
         this.radioButtonAirFilter = new System.Windows.Forms.RadioButton();
         this.radioButtonFlashTank = new System.Windows.Forms.RadioButton();
         this.radioButtonValve = new System.Windows.Forms.RadioButton();
         this.radioButtonBagFilter = new System.Windows.Forms.RadioButton();
         this.toolBoxPanel = new System.Windows.Forms.FlowLayoutPanel();
         this.toolBoxPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // radioButtonCyclone
         // 
         this.radioButtonCyclone.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCyclone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonCyclone.BackgroundImage")));
         this.radioButtonCyclone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonCyclone.Location = new System.Drawing.Point(35, 175);
         this.radioButtonCyclone.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonCyclone.Name = "radioButtonCyclone";
         this.radioButtonCyclone.Size = new System.Drawing.Size(35, 35);
         this.radioButtonCyclone.TabIndex = 6;
         this.radioButtonCyclone.CheckedChanged += new System.EventHandler(this.radioButtonCyclone_CheckedChanged);
         // 
         // radioButtonCompressor
         // 
         this.radioButtonCompressor.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCompressor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonCompressor.BackgroundImage")));
         this.radioButtonCompressor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonCompressor.Location = new System.Drawing.Point(35, 105);
         this.radioButtonCompressor.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonCompressor.Name = "radioButtonCompressor";
         this.radioButtonCompressor.Size = new System.Drawing.Size(35, 35);
         this.radioButtonCompressor.TabIndex = 5;
         this.radioButtonCompressor.CheckedChanged += new System.EventHandler(this.radioButtonCompressor_CheckedChanged);
         // 
         // radioButtonCooler
         // 
         this.radioButtonCooler.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCooler.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonCooler.BackgroundImage")));
         this.radioButtonCooler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonCooler.Location = new System.Drawing.Point(0, 280);
         this.radioButtonCooler.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonCooler.Name = "radioButtonCooler";
         this.radioButtonCooler.Size = new System.Drawing.Size(35, 35);
         this.radioButtonCooler.TabIndex = 4;
         this.radioButtonCooler.CheckedChanged += new System.EventHandler(this.radioButtonCooler_CheckedChanged);
         // 
         // radioButtonFan
         // 
         this.radioButtonFan.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonFan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonFan.BackgroundImage")));
         this.radioButtonFan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonFan.Location = new System.Drawing.Point(35, 70);
         this.radioButtonFan.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonFan.Name = "radioButtonFan";
         this.radioButtonFan.Size = new System.Drawing.Size(35, 35);
         this.radioButtonFan.TabIndex = 3;
         this.radioButtonFan.CheckedChanged += new System.EventHandler(this.radioButtonFan_CheckedChanged);
         // 
         // radioButtonHeater
         // 
         this.radioButtonHeater.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonHeater.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonHeater.BackgroundImage")));
         this.radioButtonHeater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonHeater.Location = new System.Drawing.Point(35, 280);
         this.radioButtonHeater.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonHeater.Name = "radioButtonHeater";
         this.radioButtonHeater.Size = new System.Drawing.Size(35, 35);
         this.radioButtonHeater.TabIndex = 2;
         this.radioButtonHeater.CheckedChanged += new System.EventHandler(this.radioButtonHeater_CheckedChanged);
         // 
         // radioButtonPump
         // 
         this.radioButtonPump.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonPump.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonPump.BackgroundImage")));
         this.radioButtonPump.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonPump.Location = new System.Drawing.Point(0, 105);
         this.radioButtonPump.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonPump.Name = "radioButtonPump";
         this.radioButtonPump.Size = new System.Drawing.Size(35, 35);
         this.radioButtonPump.TabIndex = 1;
         this.radioButtonPump.CheckedChanged += new System.EventHandler(this.radioButtonPump_CheckedChanged);
         // 
         // radioButtonDryerLiquid
         // 
         this.radioButtonDryerLiquid.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonDryerLiquid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonDryerLiquid.BackgroundImage")));
         this.radioButtonDryerLiquid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonDryerLiquid.Location = new System.Drawing.Point(0, 70);
         this.radioButtonDryerLiquid.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonDryerLiquid.Name = "radioButtonDryerLiquid";
         this.radioButtonDryerLiquid.Size = new System.Drawing.Size(35, 35);
         this.radioButtonDryerLiquid.TabIndex = 0;
         this.radioButtonDryerLiquid.CheckedChanged += new System.EventHandler(this.radioButtonDryerLiquid_CheckedChanged);
         // 
         // radioButtonGasStream
         // 
         this.radioButtonGasStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonGasStream.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonGasStream.BackgroundImage")));
         this.radioButtonGasStream.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonGasStream.Location = new System.Drawing.Point(0, 0);
         this.radioButtonGasStream.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonGasStream.Name = "radioButtonGasStream";
         this.radioButtonGasStream.Size = new System.Drawing.Size(35, 35);
         this.radioButtonGasStream.TabIndex = 3;
         this.radioButtonGasStream.CheckedChanged += new System.EventHandler(this.radioButtonGasStream_CheckedChanged);
         // 
         // radioButtonSolidMaterialStream
         // 
         this.radioButtonSolidMaterialStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonSolidMaterialStream.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonSolidMaterialStream.BackgroundImage")));
         this.radioButtonSolidMaterialStream.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonSolidMaterialStream.Location = new System.Drawing.Point(35, 0);
         this.radioButtonSolidMaterialStream.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonSolidMaterialStream.Name = "radioButtonSolidMaterialStream";
         this.radioButtonSolidMaterialStream.Size = new System.Drawing.Size(35, 35);
         this.radioButtonSolidMaterialStream.TabIndex = 2;
         this.radioButtonSolidMaterialStream.CheckedChanged += new System.EventHandler(this.radioButtonSolidMaterialStream_CheckedChanged);
         // 
         // radioButtonScrubberCondenser
         // 
         this.radioButtonScrubberCondenser.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonScrubberCondenser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonScrubberCondenser.BackgroundImage")));
         this.radioButtonScrubberCondenser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonScrubberCondenser.Location = new System.Drawing.Point(0, 245);
         this.radioButtonScrubberCondenser.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonScrubberCondenser.Name = "radioButtonScrubberCondenser";
         this.radioButtonScrubberCondenser.Size = new System.Drawing.Size(35, 35);
         this.radioButtonScrubberCondenser.TabIndex = 21;
         this.radioButtonScrubberCondenser.CheckedChanged += new System.EventHandler(this.radioButtonScrubberCondenser_CheckedChanged);
         // 
         // radioButtonDryerSolid
         // 
         this.radioButtonDryerSolid.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonDryerSolid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonDryerSolid.BackgroundImage")));
         this.radioButtonDryerSolid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonDryerSolid.Location = new System.Drawing.Point(35, 35);
         this.radioButtonDryerSolid.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonDryerSolid.Name = "radioButtonDryerSolid";
         this.radioButtonDryerSolid.Size = new System.Drawing.Size(35, 35);
         this.radioButtonDryerSolid.TabIndex = 20;
         this.radioButtonDryerSolid.CheckedChanged += new System.EventHandler(this.radioButtonDryerSolid_CheckedChanged);
         // 
         // radioButtonLiquidMaterialStream
         // 
         this.radioButtonLiquidMaterialStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonLiquidMaterialStream.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonLiquidMaterialStream.BackgroundImage")));
         this.radioButtonLiquidMaterialStream.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonLiquidMaterialStream.Location = new System.Drawing.Point(0, 35);
         this.radioButtonLiquidMaterialStream.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonLiquidMaterialStream.Name = "radioButtonLiquidMaterialStream";
         this.radioButtonLiquidMaterialStream.Size = new System.Drawing.Size(35, 35);
         this.radioButtonLiquidMaterialStream.TabIndex = 19;
         this.radioButtonLiquidMaterialStream.CheckedChanged += new System.EventHandler(this.radioButtonLiquidMaterialStream_CheckedChanged);
         // 
         // radioButtonWetScrubber
         // 
         this.radioButtonWetScrubber.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonWetScrubber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonWetScrubber.BackgroundImage")));
         this.radioButtonWetScrubber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonWetScrubber.Location = new System.Drawing.Point(35, 245);
         this.radioButtonWetScrubber.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonWetScrubber.Name = "radioButtonWetScrubber";
         this.radioButtonWetScrubber.Size = new System.Drawing.Size(35, 35);
         this.radioButtonWetScrubber.TabIndex = 18;
         this.radioButtonWetScrubber.CheckedChanged += new System.EventHandler(this.radioButtonWetScrubber_CheckedChanged);
         // 
         // radioButtonEjector
         // 
         this.radioButtonEjector.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonEjector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonEjector.BackgroundImage")));
         this.radioButtonEjector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonEjector.Location = new System.Drawing.Point(0, 140);
         this.radioButtonEjector.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonEjector.Name = "radioButtonEjector";
         this.radioButtonEjector.Size = new System.Drawing.Size(35, 35);
         this.radioButtonEjector.TabIndex = 17;
         this.radioButtonEjector.CheckedChanged += new System.EventHandler(this.radioButtonEjector_CheckedChanged);
         // 
         // radioButtonElectrostaticPrecipitator
         // 
         this.radioButtonElectrostaticPrecipitator.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonElectrostaticPrecipitator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonElectrostaticPrecipitator.BackgroundImage")));
         this.radioButtonElectrostaticPrecipitator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonElectrostaticPrecipitator.Location = new System.Drawing.Point(0, 210);
         this.radioButtonElectrostaticPrecipitator.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonElectrostaticPrecipitator.Name = "radioButtonElectrostaticPrecipitator";
         this.radioButtonElectrostaticPrecipitator.Size = new System.Drawing.Size(35, 35);
         this.radioButtonElectrostaticPrecipitator.TabIndex = 16;
         this.radioButtonElectrostaticPrecipitator.CheckedChanged += new System.EventHandler(this.radioButtonElectrostaticPrecipitator_CheckedChanged);
         // 
         // radioButtonRecycle
         // 
         this.radioButtonRecycle.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonRecycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonRecycle.BackgroundImage")));
         this.radioButtonRecycle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonRecycle.Location = new System.Drawing.Point(0, 385);
         this.radioButtonRecycle.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonRecycle.Name = "radioButtonRecycle";
         this.radioButtonRecycle.Size = new System.Drawing.Size(35, 35);
         this.radioButtonRecycle.TabIndex = 15;
         this.radioButtonRecycle.CheckedChanged += new System.EventHandler(this.radioButtonRecycle_CheckedChanged);
         // 
         // radioButtonHeatExchanger
         // 
         this.radioButtonHeatExchanger.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonHeatExchanger.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonHeatExchanger.BackgroundImage")));
         this.radioButtonHeatExchanger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonHeatExchanger.Location = new System.Drawing.Point(35, 315);
         this.radioButtonHeatExchanger.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonHeatExchanger.Name = "radioButtonHeatExchanger";
         this.radioButtonHeatExchanger.Size = new System.Drawing.Size(35, 35);
         this.radioButtonHeatExchanger.TabIndex = 14;
         this.radioButtonHeatExchanger.CheckedChanged += new System.EventHandler(this.radioButtonHeatExchanger_CheckedChanged);
         // 
         // radioButtonTee
         // 
         this.radioButtonTee.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonTee.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonTee.BackgroundImage")));
         this.radioButtonTee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonTee.Location = new System.Drawing.Point(0, 315);
         this.radioButtonTee.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonTee.Name = "radioButtonTee";
         this.radioButtonTee.Size = new System.Drawing.Size(35, 35);
         this.radioButtonTee.TabIndex = 13;
         this.radioButtonTee.CheckedChanged += new System.EventHandler(this.radioButtonTee_CheckedChanged);
         // 
         // radioButtonMixer
         // 
         this.radioButtonMixer.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonMixer.BackgroundImage")));
         this.radioButtonMixer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonMixer.Location = new System.Drawing.Point(35, 350);
         this.radioButtonMixer.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonMixer.Name = "radioButtonMixer";
         this.radioButtonMixer.Size = new System.Drawing.Size(35, 35);
         this.radioButtonMixer.TabIndex = 12;
         this.radioButtonMixer.CheckedChanged += new System.EventHandler(this.radioButtonMixer_CheckedChanged);
         // 
         // radioButtonAirFilter
         // 
         this.radioButtonAirFilter.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonAirFilter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonAirFilter.BackgroundImage")));
         this.radioButtonAirFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonAirFilter.Location = new System.Drawing.Point(35, 210);
         this.radioButtonAirFilter.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonAirFilter.Name = "radioButtonAirFilter";
         this.radioButtonAirFilter.Size = new System.Drawing.Size(35, 35);
         this.radioButtonAirFilter.TabIndex = 11;
         this.radioButtonAirFilter.CheckedChanged += new System.EventHandler(this.radioButtonAirFilter_CheckedChanged);
         // 
         // radioButtonFlashTank
         // 
         this.radioButtonFlashTank.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonFlashTank.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonFlashTank.BackgroundImage")));
         this.radioButtonFlashTank.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonFlashTank.Location = new System.Drawing.Point(0, 350);
         this.radioButtonFlashTank.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonFlashTank.Name = "radioButtonFlashTank";
         this.radioButtonFlashTank.Size = new System.Drawing.Size(35, 35);
         this.radioButtonFlashTank.TabIndex = 10;
         this.radioButtonFlashTank.CheckedChanged += new System.EventHandler(this.radioButtonFlashTank_CheckedChanged);
         // 
         // radioButtonValve
         // 
         this.radioButtonValve.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonValve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonValve.BackgroundImage")));
         this.radioButtonValve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonValve.Location = new System.Drawing.Point(35, 140);
         this.radioButtonValve.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonValve.Name = "radioButtonValve";
         this.radioButtonValve.Size = new System.Drawing.Size(35, 35);
         this.radioButtonValve.TabIndex = 9;
         this.radioButtonValve.CheckedChanged += new System.EventHandler(this.radioButtonValve_CheckedChanged);
         // 
         // radioButtonBagFilter
         // 
         this.radioButtonBagFilter.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonBagFilter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButtonBagFilter.BackgroundImage")));
         this.radioButtonBagFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
         this.radioButtonBagFilter.Location = new System.Drawing.Point(0, 175);
         this.radioButtonBagFilter.Margin = new System.Windows.Forms.Padding(0);
         this.radioButtonBagFilter.Name = "radioButtonBagFilter";
         this.radioButtonBagFilter.Size = new System.Drawing.Size(35, 35);
         this.radioButtonBagFilter.TabIndex = 8;
         this.radioButtonBagFilter.CheckedChanged += new System.EventHandler(this.radioButtonBagFilter_CheckedChanged);
         // 
         // toolBoxPanel
         // 
         this.toolBoxPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
         this.toolBoxPanel.Controls.Add(this.radioButtonGasStream);
         this.toolBoxPanel.Controls.Add(this.radioButtonSolidMaterialStream);
         this.toolBoxPanel.Controls.Add(this.radioButtonLiquidMaterialStream);
         this.toolBoxPanel.Controls.Add(this.radioButtonDryerSolid);
         this.toolBoxPanel.Controls.Add(this.radioButtonDryerLiquid);
         this.toolBoxPanel.Controls.Add(this.radioButtonFan);
         this.toolBoxPanel.Controls.Add(this.radioButtonPump);
         this.toolBoxPanel.Controls.Add(this.radioButtonCompressor);
         this.toolBoxPanel.Controls.Add(this.radioButtonEjector);
         this.toolBoxPanel.Controls.Add(this.radioButtonValve);
         this.toolBoxPanel.Controls.Add(this.radioButtonBagFilter);
         this.toolBoxPanel.Controls.Add(this.radioButtonCyclone);
         this.toolBoxPanel.Controls.Add(this.radioButtonElectrostaticPrecipitator);
         this.toolBoxPanel.Controls.Add(this.radioButtonAirFilter);
         this.toolBoxPanel.Controls.Add(this.radioButtonScrubberCondenser);
         this.toolBoxPanel.Controls.Add(this.radioButtonWetScrubber);
         this.toolBoxPanel.Controls.Add(this.radioButtonCooler);
         this.toolBoxPanel.Controls.Add(this.radioButtonHeater);
         this.toolBoxPanel.Controls.Add(this.radioButtonTee);
         this.toolBoxPanel.Controls.Add(this.radioButtonHeatExchanger);
         this.toolBoxPanel.Controls.Add(this.radioButtonFlashTank);
         this.toolBoxPanel.Controls.Add(this.radioButtonMixer);
         this.toolBoxPanel.Controls.Add(this.radioButtonRecycle);
         this.toolBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolBoxPanel.Location = new System.Drawing.Point(0, 0);
         this.toolBoxPanel.Margin = new System.Windows.Forms.Padding(0);
         this.toolBoxPanel.Name = "toolBoxPanel";
         this.toolBoxPanel.Size = new System.Drawing.Size(73, 420);
         this.toolBoxPanel.TabIndex = 9;
         // 
         // Toolbox
         // 
         this.AutoScroll = true;
         this.ClientSize = new System.Drawing.Size(73, 420);
         this.Controls.Add(this.toolBoxPanel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "Toolbox";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Toolbox";
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbox_MouseUp);
         //this.Closing += new System.ComponentModel.CancelEventHandler(this.Toolbox_Closing);
         this.toolBoxPanel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
      #endregion

      private void radioButtonGasStream_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonGasStream.Checked)
            this.flowsheet.AddSolvable(typeof(DryingGasStream));
      }

      private void radioButtonCyclone_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonCyclone.Checked)
            this.flowsheet.AddSolvable(typeof(Cyclone));
      }

      private void radioButtonEjector_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonEjector.Checked)
            this.flowsheet.AddSolvable(typeof(Ejector));
      }

      private void radioButtonWetScrubber_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonWetScrubber.Checked)
            this.flowsheet.AddSolvable(typeof(WetScrubber));
      }

      private void radioButtonMixer_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonMixer.Checked)
            this.flowsheet.AddSolvable(typeof(Mixer));
      }

      private void radioButtonTee_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonTee.Checked)
            this.flowsheet.AddSolvable(typeof(Tee));
      }

      private void radioButtonFlashTank_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonFlashTank.Checked)
            this.flowsheet.AddSolvable(typeof(FlashTank));
      }

      private void radioButtonCompressor_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonCompressor.Checked)
            this.flowsheet.AddSolvable(typeof(Compressor));
      }

      private void radioButtonPump_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonPump.Checked)
            this.flowsheet.AddSolvable(typeof(Pump));
      }

      private void radioButtonCooler_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonCooler.Checked)
            this.flowsheet.AddSolvable(typeof(Cooler));
      }

      private void radioButtonElectrostaticPrecipitator_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonElectrostaticPrecipitator.Checked)
            this.flowsheet.AddSolvable(typeof(ElectrostaticPrecipitator));
      }

      private void radioButtonFan_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonFan.Checked)
            this.flowsheet.AddSolvable(typeof(Fan));
      }

      private void radioButtonValve_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonValve.Checked)
            this.flowsheet.AddSolvable(typeof(Valve));
      }

      private void radioButtonHeater_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonHeater.Checked)
            this.flowsheet.AddSolvable(typeof(Heater));
      }

      private void radioButtonBagFilter_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonBagFilter.Checked)
            this.flowsheet.AddSolvable(typeof(BagFilter));
      }

      private void radioButtonAirFilter_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonAirFilter.Checked)
            this.flowsheet.AddSolvable(typeof(AirFilter));
      }

      private void radioButtonHeatExchanger_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonHeatExchanger.Checked)
            this.flowsheet.AddSolvable(typeof(HeatExchanger));
      }

      private void Toolbox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
         this.flowsheet.ResetActivity();
      }

      private void UncheckAllButtons() {
         IEnumerator e = this.toolBoxPanel.Controls.GetEnumerator();
         while (e.MoveNext()) {
            RadioButton rb = (RadioButton)e.Current;
            rb.Checked = false;
         }
      }

      private void SetToolTips() {
         ToolTip toolTip;

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCompressor, UI.SolvableNameTable[typeof(Compressor)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonHeater, UI.SolvableNameTable[typeof(Heater)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCooler, UI.SolvableNameTable[typeof(Cooler)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonElectrostaticPrecipitator, UI.SolvableNameTable[typeof(ElectrostaticPrecipitator)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCyclone, UI.SolvableNameTable[typeof(Cyclone)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonEjector, UI.SolvableNameTable[typeof(Ejector)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonWetScrubber, UI.SolvableNameTable[typeof(WetScrubber)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonScrubberCondenser, UI.SolvableNameTable[typeof(ScrubberCondenser)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonMixer, UI.SolvableNameTable[typeof(Mixer)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonTee, UI.SolvableNameTable[typeof(Tee)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonFlashTank, UI.SolvableNameTable[typeof(FlashTank)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonDryerLiquid, UI.SolvableNameTable[typeof(LiquidDryer)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonDryerSolid, UI.SolvableNameTable[typeof(SolidDryer)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonHeatExchanger, UI.SolvableNameTable[typeof(HeatExchanger)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonFan, UI.SolvableNameTable[typeof(Fan)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonValve, UI.SolvableNameTable[typeof(Valve)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonBagFilter, UI.SolvableNameTable[typeof(BagFilter)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonAirFilter, UI.SolvableNameTable[typeof(AirFilter)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonPump, UI.SolvableNameTable[typeof(Pump)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonRecycle, UI.SolvableNameTable[typeof(Recycle)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonGasStream, UI.SolvableNameTable[typeof(DryingGasStream)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonSolidMaterialStream, UI.SolvableNameTable[typeof(SolidDryingMaterialStream)]);

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonLiquidMaterialStream, UI.SolvableNameTable[typeof(LiquidDryingMaterialStream)]);
      }

      private void flowsheet_ActivityChanged(Object sender, ActivityChangedEventArgs eventArgs) {
         if (eventArgs.FlowsheetActivity == FlowsheetActivity.Default ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.AddingConnStepOne ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.AddingConnStepTwo ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.DeletingConnection ||
            eventArgs.FlowsheetActivity == FlowsheetActivity.SelectingSnapshot) {
            this.UncheckAllButtons();
         }
         else if (eventArgs.FlowsheetActivity == FlowsheetActivity.AddingSolvable) {
            if (eventArgs.SolvableType == typeof(DryingGasStream)) {
               if (!this.radioButtonGasStream.Checked) {
                  this.radioButtonGasStream.Checked = true;
               }
            }
            else if (eventArgs.SolvableType == typeof(SolidDryingMaterialStream)) {
               if (!this.radioButtonSolidMaterialStream.Checked) {
                  this.radioButtonSolidMaterialStream.Checked = true;
               }
            }
            else if (eventArgs.SolvableType == typeof(LiquidDryingMaterialStream)) {
               if (!this.radioButtonLiquidMaterialStream.Checked) {
                  this.radioButtonLiquidMaterialStream.Checked = true;
               }
            }
            else if (eventArgs.SolvableType == typeof(Compressor)) {
               if (!this.radioButtonCompressor.Checked)
                  this.radioButtonCompressor.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(LiquidDryer)) {
               if (!this.radioButtonDryerLiquid.Checked)
                  this.radioButtonDryerLiquid.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(SolidDryer)) {
               if (!this.radioButtonDryerSolid.Checked)
                  this.radioButtonDryerSolid.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(HeatExchanger)) {
               if (!this.radioButtonHeatExchanger.Checked)
                  this.radioButtonHeatExchanger.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Cooler)) {
               if (!this.radioButtonCooler.Checked)
                  this.radioButtonCooler.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(ElectrostaticPrecipitator)) {
               if (!this.radioButtonElectrostaticPrecipitator.Checked)
                  this.radioButtonElectrostaticPrecipitator.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Cyclone)) {
               if (!this.radioButtonCyclone.Checked)
                  this.radioButtonCyclone.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Ejector)) {
               if (!this.radioButtonEjector.Checked)
                  this.radioButtonEjector.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(WetScrubber)) {
               if (!this.radioButtonWetScrubber.Checked)
                  this.radioButtonWetScrubber.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(ScrubberCondenser)) {
               if (!this.radioButtonScrubberCondenser.Checked)
                  this.radioButtonScrubberCondenser.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Mixer)) {
               if (!this.radioButtonMixer.Checked)
                  this.radioButtonMixer.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Tee)) {
               if (!this.radioButtonTee.Checked)
                  this.radioButtonTee.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(FlashTank)) {
               if (!this.radioButtonFlashTank.Checked)
                  this.radioButtonFlashTank.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Fan)) {
               if (!this.radioButtonFan.Checked)
                  this.radioButtonFan.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Valve)) {
               if (!this.radioButtonValve.Checked)
                  this.radioButtonValve.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(BagFilter)) {
               if (!this.radioButtonBagFilter.Checked)
                  this.radioButtonBagFilter.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(AirFilter)) {
               if (!this.radioButtonAirFilter.Checked)
                  this.radioButtonAirFilter.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Pump)) {
               if (!this.radioButtonPump.Checked)
                  this.radioButtonPump.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Recycle)) {
               if (!this.radioButtonRecycle.Checked)
                  this.radioButtonRecycle.Checked = true;
            }
            else if (eventArgs.SolvableType == typeof(Heater)) {
               if (!this.radioButtonHeater.Checked)
                  this.radioButtonHeater.Checked = true;
            }
         }
      }

      private void panel_Click(object sender, System.EventArgs e) {
         this.flowsheet.ResetActivity();
      }

      private void radioButtonRecycle_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonRecycle.Checked)
            this.flowsheet.AddSolvable(typeof(Recycle));
      }

      private void radioButtonLiquidMaterialStream_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonLiquidMaterialStream.Checked)
            this.flowsheet.AddSolvable(typeof(LiquidDryingMaterialStream));
      }

      private void radioButtonSolidMaterialStream_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonSolidMaterialStream.Checked)
            this.flowsheet.AddSolvable(typeof(SolidDryingMaterialStream));
      }

      private void radioButtonDryerLiquid_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonDryerLiquid.Checked)
            this.flowsheet.AddSolvable(typeof(LiquidDryer));
      }

      private void radioButtonDryerSolid_CheckedChanged(object sender, System.EventArgs e) {
         if (this.radioButtonDryerSolid.Checked)
            this.flowsheet.AddSolvable(typeof(SolidDryer));
      }

      private void radioButtonScrubberCondenser_CheckedChanged(object sender, EventArgs e) {
         if (this.radioButtonScrubberCondenser.Checked)
            this.flowsheet.AddSolvable(typeof(ScrubberCondenser));
      }
   }
}

//using Crownwood.Magic.Common;
//using Crownwood.Magic.Controls;
//using Crownwood.Magic.Docking;
//using Crownwood.Magic.Menus;
//public void Toolbox_FormClosed(Content c, EventArgs cea) {
//   //this.flowsheet.Toolbox = null;
//   this.flowsheet.HideToolbox();
//}
//private void Toolbox_LocationChanged(object sender, EventArgs e) {
//   //this.flowsheet.ToolboxLocation = this.Location;
//   (this.Parent as MainForm).ToolboxLocation = this.Location;
//}
//private void Toolbox_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
//   this.flowsheet.Toolbox = null;
//}



