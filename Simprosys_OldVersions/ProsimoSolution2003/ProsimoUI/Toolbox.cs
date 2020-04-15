using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for Toolbox.
	/// </summary>
	public class Toolbox : System.Windows.Forms.Form
	{
      private Flowsheet flowsheet;
      private System.Windows.Forms.RadioButton radioButtonPump;
      private System.Windows.Forms.RadioButton radioButtonHeater;
      private System.Windows.Forms.RadioButton radioButtonFan;
      private System.Windows.Forms.RadioButton radioButtonCooler;
      private System.Windows.Forms.RadioButton radioButtonCompressor;
      private System.Windows.Forms.RadioButton radioButtonCyclone;
      private System.Windows.Forms.RadioButton radioButtonGasStream;
      private System.Windows.Forms.Panel panel;
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public Toolbox()
      {
      }

		public Toolbox(Flowsheet flowsheet)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.flowsheet = flowsheet;
         this.SetToolTips();

//         this.flowsheet.MouseDown += new MouseEventHandler(flowsheet_MouseDown);
         this.LocationChanged += new EventHandler(Toolbox_LocationChanged);
         this.flowsheet.ActivityChanged += new ActivityChangedEventHandler(flowsheet_ActivityChanged);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.flowsheet != null)
            this.flowsheet.ActivityChanged -= new ActivityChangedEventHandler(flowsheet_ActivityChanged);
         if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Toolbox));
         this.radioButtonCyclone = new System.Windows.Forms.RadioButton();
         this.radioButtonCompressor = new System.Windows.Forms.RadioButton();
         this.radioButtonCooler = new System.Windows.Forms.RadioButton();
         this.radioButtonFan = new System.Windows.Forms.RadioButton();
         this.radioButtonHeater = new System.Windows.Forms.RadioButton();
         this.radioButtonPump = new System.Windows.Forms.RadioButton();
         this.radioButtonDryerLiquid = new System.Windows.Forms.RadioButton();
         this.radioButtonGasStream = new System.Windows.Forms.RadioButton();
         this.radioButtonSolidMaterialStream = new System.Windows.Forms.RadioButton();
         this.panel = new System.Windows.Forms.Panel();
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
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // radioButtonCyclone
         // 
         this.radioButtonCyclone.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCyclone.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonCyclone.Image")));
         this.radioButtonCyclone.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonCyclone.Location = new System.Drawing.Point(0, 170);
         this.radioButtonCyclone.Name = "radioButtonCyclone";
         this.radioButtonCyclone.Size = new System.Drawing.Size(34, 34);
         this.radioButtonCyclone.TabIndex = 6;
         this.radioButtonCyclone.CheckedChanged += new System.EventHandler(this.radioButtonCyclone_CheckedChanged);
         // 
         // radioButtonCompressor
         // 
         this.radioButtonCompressor.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCompressor.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonCompressor.Image")));
         this.radioButtonCompressor.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonCompressor.Location = new System.Drawing.Point(0, 102);
         this.radioButtonCompressor.Name = "radioButtonCompressor";
         this.radioButtonCompressor.Size = new System.Drawing.Size(34, 34);
         this.radioButtonCompressor.TabIndex = 5;
         this.radioButtonCompressor.CheckedChanged += new System.EventHandler(this.radioButtonCompressor_CheckedChanged);
         // 
         // radioButtonCooler
         // 
         this.radioButtonCooler.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonCooler.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonCooler.Image")));
         this.radioButtonCooler.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonCooler.Location = new System.Drawing.Point(0, 272);
         this.radioButtonCooler.Name = "radioButtonCooler";
         this.radioButtonCooler.Size = new System.Drawing.Size(34, 34);
         this.radioButtonCooler.TabIndex = 4;
         this.radioButtonCooler.CheckedChanged += new System.EventHandler(this.radioButtonCooler_CheckedChanged);
         // 
         // radioButtonFan
         // 
         this.radioButtonFan.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonFan.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonFan.Image")));
         this.radioButtonFan.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonFan.Location = new System.Drawing.Point(34, 68);
         this.radioButtonFan.Name = "radioButtonFan";
         this.radioButtonFan.Size = new System.Drawing.Size(34, 34);
         this.radioButtonFan.TabIndex = 3;
         this.radioButtonFan.CheckedChanged += new System.EventHandler(this.radioButtonFan_CheckedChanged);
         // 
         // radioButtonHeater
         // 
         this.radioButtonHeater.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonHeater.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonHeater.Image")));
         this.radioButtonHeater.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonHeater.Location = new System.Drawing.Point(34, 238);
         this.radioButtonHeater.Name = "radioButtonHeater";
         this.radioButtonHeater.Size = new System.Drawing.Size(34, 34);
         this.radioButtonHeater.TabIndex = 2;
         this.radioButtonHeater.CheckedChanged += new System.EventHandler(this.radioButtonHeater_CheckedChanged);
         // 
         // radioButtonPump
         // 
         this.radioButtonPump.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonPump.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonPump.Image")));
         this.radioButtonPump.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonPump.Location = new System.Drawing.Point(34, 102);
         this.radioButtonPump.Name = "radioButtonPump";
         this.radioButtonPump.Size = new System.Drawing.Size(34, 34);
         this.radioButtonPump.TabIndex = 1;
         this.radioButtonPump.CheckedChanged += new System.EventHandler(this.radioButtonPump_CheckedChanged);
         // 
         // radioButtonDryerLiquid
         // 
         this.radioButtonDryerLiquid.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonDryerLiquid.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonDryerLiquid.Image")));
         this.radioButtonDryerLiquid.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonDryerLiquid.Location = new System.Drawing.Point(0, 68);
         this.radioButtonDryerLiquid.Name = "radioButtonDryerLiquid";
         this.radioButtonDryerLiquid.Size = new System.Drawing.Size(34, 34);
         this.radioButtonDryerLiquid.TabIndex = 0;
         this.radioButtonDryerLiquid.CheckedChanged += new System.EventHandler(this.radioButtonDryerLiquid_CheckedChanged);
         // 
         // radioButtonGasStream
         // 
         this.radioButtonGasStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonGasStream.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonGasStream.Image")));
         this.radioButtonGasStream.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonGasStream.Location = new System.Drawing.Point(0, 0);
         this.radioButtonGasStream.Name = "radioButtonGasStream";
         this.radioButtonGasStream.Size = new System.Drawing.Size(34, 34);
         this.radioButtonGasStream.TabIndex = 3;
         this.radioButtonGasStream.CheckedChanged += new System.EventHandler(this.radioButtonGasStream_CheckedChanged);
         // 
         // radioButtonSolidMaterialStream
         // 
         this.radioButtonSolidMaterialStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonSolidMaterialStream.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonSolidMaterialStream.Image")));
         this.radioButtonSolidMaterialStream.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonSolidMaterialStream.Location = new System.Drawing.Point(34, 0);
         this.radioButtonSolidMaterialStream.Name = "radioButtonSolidMaterialStream";
         this.radioButtonSolidMaterialStream.Size = new System.Drawing.Size(34, 34);
         this.radioButtonSolidMaterialStream.TabIndex = 2;
         this.radioButtonSolidMaterialStream.CheckedChanged += new System.EventHandler(this.radioButtonSolidMaterialStream_CheckedChanged);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel.Controls.Add(this.radioButtonDryerSolid);
         this.panel.Controls.Add(this.radioButtonLiquidMaterialStream);
         this.panel.Controls.Add(this.radioButtonWetScrubber);
         this.panel.Controls.Add(this.radioButtonEjector);
         this.panel.Controls.Add(this.radioButtonElectrostaticPrecipitator);
         this.panel.Controls.Add(this.radioButtonRecycle);
         this.panel.Controls.Add(this.radioButtonHeatExchanger);
         this.panel.Controls.Add(this.radioButtonTee);
         this.panel.Controls.Add(this.radioButtonMixer);
         this.panel.Controls.Add(this.radioButtonAirFilter);
         this.panel.Controls.Add(this.radioButtonFlashTank);
         this.panel.Controls.Add(this.radioButtonValve);
         this.panel.Controls.Add(this.radioButtonBagFilter);
         this.panel.Controls.Add(this.radioButtonSolidMaterialStream);
         this.panel.Controls.Add(this.radioButtonGasStream);
         this.panel.Controls.Add(this.radioButtonDryerLiquid);
         this.panel.Controls.Add(this.radioButtonCyclone);
         this.panel.Controls.Add(this.radioButtonPump);
         this.panel.Controls.Add(this.radioButtonCompressor);
         this.panel.Controls.Add(this.radioButtonFan);
         this.panel.Controls.Add(this.radioButtonHeater);
         this.panel.Controls.Add(this.radioButtonCooler);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(70, 388);
         this.panel.TabIndex = 7;
         this.panel.Click += new System.EventHandler(this.panel_Click);
         // 
         // radioButtonDryerSolid
         // 
         this.radioButtonDryerSolid.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonDryerSolid.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonDryerSolid.Image")));
         this.radioButtonDryerSolid.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonDryerSolid.Location = new System.Drawing.Point(34, 34);
         this.radioButtonDryerSolid.Name = "radioButtonDryerSolid";
         this.radioButtonDryerSolid.Size = new System.Drawing.Size(34, 34);
         this.radioButtonDryerSolid.TabIndex = 20;
         this.radioButtonDryerSolid.CheckedChanged += new System.EventHandler(this.radioButtonDryerSolid_CheckedChanged);
         // 
         // radioButtonLiquidMaterialStream
         // 
         this.radioButtonLiquidMaterialStream.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonLiquidMaterialStream.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonLiquidMaterialStream.Image")));
         this.radioButtonLiquidMaterialStream.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonLiquidMaterialStream.Location = new System.Drawing.Point(0, 34);
         this.radioButtonLiquidMaterialStream.Name = "radioButtonLiquidMaterialStream";
         this.radioButtonLiquidMaterialStream.Size = new System.Drawing.Size(34, 34);
         this.radioButtonLiquidMaterialStream.TabIndex = 19;
         this.radioButtonLiquidMaterialStream.CheckedChanged += new System.EventHandler(this.radioButtonLiquidMaterialStream_CheckedChanged);
         // 
         // radioButtonWetScrubber
         // 
         this.radioButtonWetScrubber.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonWetScrubber.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonWetScrubber.Image")));
         this.radioButtonWetScrubber.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonWetScrubber.Location = new System.Drawing.Point(0, 238);
         this.radioButtonWetScrubber.Name = "radioButtonWetScrubber";
         this.radioButtonWetScrubber.Size = new System.Drawing.Size(34, 34);
         this.radioButtonWetScrubber.TabIndex = 18;
         this.radioButtonWetScrubber.CheckedChanged += new System.EventHandler(this.radioButtonWetScrubber_CheckedChanged);
         // 
         // radioButtonEjector
         // 
         this.radioButtonEjector.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonEjector.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonEjector.Image")));
         this.radioButtonEjector.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonEjector.Location = new System.Drawing.Point(34, 136);
         this.radioButtonEjector.Name = "radioButtonEjector";
         this.radioButtonEjector.Size = new System.Drawing.Size(34, 34);
         this.radioButtonEjector.TabIndex = 17;
         this.radioButtonEjector.CheckedChanged += new System.EventHandler(this.radioButtonEjector_CheckedChanged);
         // 
         // radioButtonElectrostaticPrecipitator
         // 
         this.radioButtonElectrostaticPrecipitator.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonElectrostaticPrecipitator.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonElectrostaticPrecipitator.Image")));
         this.radioButtonElectrostaticPrecipitator.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonElectrostaticPrecipitator.Location = new System.Drawing.Point(34, 204);
         this.radioButtonElectrostaticPrecipitator.Name = "radioButtonElectrostaticPrecipitator";
         this.radioButtonElectrostaticPrecipitator.Size = new System.Drawing.Size(34, 34);
         this.radioButtonElectrostaticPrecipitator.TabIndex = 16;
         this.radioButtonElectrostaticPrecipitator.CheckedChanged += new System.EventHandler(this.radioButtonElectrostaticPrecipitator_CheckedChanged);
         // 
         // radioButtonRecycle
         // 
         this.radioButtonRecycle.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonRecycle.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonRecycle.Image")));
         this.radioButtonRecycle.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonRecycle.Location = new System.Drawing.Point(34, 340);
         this.radioButtonRecycle.Name = "radioButtonRecycle";
         this.radioButtonRecycle.Size = new System.Drawing.Size(34, 34);
         this.radioButtonRecycle.TabIndex = 15;
         this.radioButtonRecycle.CheckedChanged += new System.EventHandler(this.radioButtonRecycle_CheckedChanged);
         // 
         // radioButtonHeatExchanger
         // 
         this.radioButtonHeatExchanger.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonHeatExchanger.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonHeatExchanger.Image")));
         this.radioButtonHeatExchanger.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonHeatExchanger.Location = new System.Drawing.Point(34, 272);
         this.radioButtonHeatExchanger.Name = "radioButtonHeatExchanger";
         this.radioButtonHeatExchanger.Size = new System.Drawing.Size(34, 34);
         this.radioButtonHeatExchanger.TabIndex = 14;
         this.radioButtonHeatExchanger.CheckedChanged += new System.EventHandler(this.radioButtonHeatExchanger_CheckedChanged);
         // 
         // radioButtonTee
         // 
         this.radioButtonTee.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonTee.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonTee.Image")));
         this.radioButtonTee.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonTee.Location = new System.Drawing.Point(0, 306);
         this.radioButtonTee.Name = "radioButtonTee";
         this.radioButtonTee.Size = new System.Drawing.Size(34, 34);
         this.radioButtonTee.TabIndex = 13;
         this.radioButtonTee.CheckedChanged += new System.EventHandler(this.radioButtonTee_CheckedChanged);
         // 
         // radioButtonMixer
         // 
         this.radioButtonMixer.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonMixer.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonMixer.Image")));
         this.radioButtonMixer.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonMixer.Location = new System.Drawing.Point(34, 306);
         this.radioButtonMixer.Name = "radioButtonMixer";
         this.radioButtonMixer.Size = new System.Drawing.Size(34, 34);
         this.radioButtonMixer.TabIndex = 12;
         this.radioButtonMixer.CheckedChanged += new System.EventHandler(this.radioButtonMixer_CheckedChanged);
         // 
         // radioButtonAirFilter
         // 
         this.radioButtonAirFilter.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonAirFilter.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonAirFilter.Image")));
         this.radioButtonAirFilter.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonAirFilter.Location = new System.Drawing.Point(0, 204);
         this.radioButtonAirFilter.Name = "radioButtonAirFilter";
         this.radioButtonAirFilter.Size = new System.Drawing.Size(34, 34);
         this.radioButtonAirFilter.TabIndex = 11;
         this.radioButtonAirFilter.CheckedChanged += new System.EventHandler(this.radioButtonAirFilter_CheckedChanged);
         // 
         // radioButtonFlashTank
         // 
         this.radioButtonFlashTank.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonFlashTank.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonFlashTank.Image")));
         this.radioButtonFlashTank.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonFlashTank.Location = new System.Drawing.Point(0, 340);
         this.radioButtonFlashTank.Name = "radioButtonFlashTank";
         this.radioButtonFlashTank.Size = new System.Drawing.Size(34, 34);
         this.radioButtonFlashTank.TabIndex = 10;
         this.radioButtonFlashTank.CheckedChanged += new System.EventHandler(this.radioButtonFlashTank_CheckedChanged);
         // 
         // radioButtonValve
         // 
         this.radioButtonValve.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonValve.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonValve.Image")));
         this.radioButtonValve.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonValve.Location = new System.Drawing.Point(0, 136);
         this.radioButtonValve.Name = "radioButtonValve";
         this.radioButtonValve.Size = new System.Drawing.Size(34, 34);
         this.radioButtonValve.TabIndex = 9;
         this.radioButtonValve.CheckedChanged += new System.EventHandler(this.radioButtonValve_CheckedChanged);
         // 
         // radioButtonBagFilter
         // 
         this.radioButtonBagFilter.Appearance = System.Windows.Forms.Appearance.Button;
         this.radioButtonBagFilter.Image = ((System.Drawing.Image)(resources.GetObject("radioButtonBagFilter.Image")));
         this.radioButtonBagFilter.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
         this.radioButtonBagFilter.Location = new System.Drawing.Point(34, 170);
         this.radioButtonBagFilter.Name = "radioButtonBagFilter";
         this.radioButtonBagFilter.Size = new System.Drawing.Size(34, 34);
         this.radioButtonBagFilter.TabIndex = 8;
         this.radioButtonBagFilter.CheckedChanged += new System.EventHandler(this.radioButtonBagFilter_CheckedChanged);
         // 
         // Toolbox
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.AutoScroll = true;
         this.ClientSize = new System.Drawing.Size(70, 388);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "Toolbox";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Toolbox";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.Toolbox_Closing);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbox_MouseUp);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void radioButtonCyclone_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonCyclone.Checked)
            this.flowsheet.AddCyclone();
      }

      private void radioButtonEjector_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonEjector.Checked)
            this.flowsheet.AddEjector();
      }

      private void radioButtonWetScrubber_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonWetScrubber.Checked)
            this.flowsheet.AddWetScrubber();
      }

      private void radioButtonMixer_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonMixer.Checked)
            this.flowsheet.AddMixer();
      }

      private void radioButtonTee_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonTee.Checked)
            this.flowsheet.AddTee();
      }

      private void radioButtonFlashTank_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonFlashTank.Checked)
            this.flowsheet.AddFlashTank();
      }

      private void radioButtonCompressor_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonCompressor.Checked)
            this.flowsheet.AddCompressor();
      }

      private void radioButtonPump_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonPump.Checked)
            this.flowsheet.AddPump();
      }

      private void radioButtonCooler_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonCooler.Checked)
            this.flowsheet.AddCooler();
      }

      private void radioButtonElectrostaticPrecipitator_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonElectrostaticPrecipitator.Checked)
            this.flowsheet.AddElectrostaticPrecipitator();
      }

      private void radioButtonFan_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonFan.Checked)
            this.flowsheet.AddFan();
      }

      private void radioButtonValve_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonValve.Checked)
            this.flowsheet.AddValve();
      }

      private void radioButtonHeater_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonHeater.Checked)
            this.flowsheet.AddHeater();
      }

      private void radioButtonGasStream_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonGasStream.Checked)
            this.flowsheet.AddGasStream();
      }

      private void radioButtonBagFilter_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonBagFilter.Checked)
            this.flowsheet.AddBagFilter();
      }

      private void radioButtonAirFilter_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonAirFilter.Checked)
            this.flowsheet.AddAirFilter();
      }

      private void radioButtonHeatExchanger_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonHeatExchanger.Checked)
            this.flowsheet.AddHeatExchanger();
      }

      private void Toolbox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.flowsheet.ResetActivity();
      }

      private void UncheckAllButtons()
      {
         IEnumerator e = this.panel.Controls.GetEnumerator();
         while (e.MoveNext())
         {
            RadioButton rb = (RadioButton)e.Current;
            rb.Checked = false;
         }
      }

      private void SetToolTips()
      {
         ToolTip toolTip;
         
         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCompressor, "Compressor");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCooler, "Cooler");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonElectrostaticPrecipitator, "Electrostatic Precipitator");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonCyclone, "Cyclone");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonEjector, "Ejector");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonWetScrubber, "Wet Scrubber");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonMixer, "Mixer");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonTee, "Tee");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonFlashTank, "Flash Tank");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonDryerLiquid, "Liquid Material Dryer");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonDryerSolid, "Solid Material Dryer");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonHeatExchanger, "Heat Exchanger");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonFan, "Fan");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonValve, "Valve");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonBagFilter, "Bag Filter");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonAirFilter, "Air Filter");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonGasStream, "Gas Stream");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonHeater, "Heater");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonSolidMaterialStream, "Solid Material Stream");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonLiquidMaterialStream, "Liquid Material Stream");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonPump, "Pump");

         toolTip = new ToolTip();
         toolTip.SetToolTip(this.radioButtonRecycle, "Recycle");
      }

      private void Toolbox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         this.flowsheet.Toolbox = null;
      }

      private void flowsheet_ActivityChanged(FlowsheetActivity flowsheetActivity)
      {
         if (flowsheetActivity == FlowsheetActivity.Default ||
            flowsheetActivity == FlowsheetActivity.AddingConnStepOne ||
            flowsheetActivity == FlowsheetActivity.AddingConnStepTwo ||
            flowsheetActivity == FlowsheetActivity.DeletingConnection ||
            flowsheetActivity == FlowsheetActivity.SelectingSnapshot
            )
            this.UncheckAllButtons();

         else if (flowsheetActivity == FlowsheetActivity.AddingCompressor)
         {
            if (!this.radioButtonCompressor.Checked)
            this.radioButtonCompressor.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingLiquidDryer)
         {
            if (!this.radioButtonDryerLiquid.Checked)
            this.radioButtonDryerLiquid.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingSolidDryer)
         {
            if (!this.radioButtonDryerSolid.Checked)
               this.radioButtonDryerSolid.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingHeatExchanger)
         {
            if (!this.radioButtonHeatExchanger.Checked)
               this.radioButtonHeatExchanger.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingCooler)
         {
            if (!this.radioButtonCooler.Checked)
            this.radioButtonCooler.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingElectrostaticPrecipitator)
         {
            if (!this.radioButtonElectrostaticPrecipitator.Checked)
               this.radioButtonElectrostaticPrecipitator.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingCyclone)
         {
            if (!this.radioButtonCyclone.Checked)
            this.radioButtonCyclone.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingEjector)
         {
            if (!this.radioButtonEjector.Checked)
               this.radioButtonEjector.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingWetScrubber)
         {
            if (!this.radioButtonWetScrubber.Checked)
               this.radioButtonWetScrubber.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingMixer)
         {
            if (!this.radioButtonMixer.Checked)
               this.radioButtonMixer.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingTee)
         {
            if (!this.radioButtonTee.Checked)
               this.radioButtonTee.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingFlashTank)
         {
            if (!this.radioButtonFlashTank.Checked)
               this.radioButtonFlashTank.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingFan)
         {
            if (!this.radioButtonFan.Checked)
            this.radioButtonFan.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingValve)
         {
            if (!this.radioButtonValve.Checked)
               this.radioButtonValve.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingBagFilter)
         {
            if (!this.radioButtonBagFilter.Checked)
               this.radioButtonBagFilter.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingAirFilter)
         {
            if (!this.radioButtonAirFilter.Checked)
               this.radioButtonAirFilter.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingGasStream)
         {
            if (!this.radioButtonGasStream.Checked)
            this.radioButtonGasStream.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingHeater)
         {
            if (!this.radioButtonHeater.Checked)
            this.radioButtonHeater.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingSolidMaterialStream)
         {
            if (!this.radioButtonSolidMaterialStream.Checked)
            this.radioButtonSolidMaterialStream.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingLiquidMaterialStream)
         {
            if (!this.radioButtonLiquidMaterialStream.Checked)
               this.radioButtonLiquidMaterialStream.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingPump)
         {
            if (!this.radioButtonPump.Checked)
            this.radioButtonPump.Checked = true;
         }
         else if (flowsheetActivity == FlowsheetActivity.AddingRecycle)
         {
            if (!this.radioButtonRecycle.Checked)
               this.radioButtonRecycle.Checked = true;
         }
      }

      private void panel_Click(object sender, System.EventArgs e)
      {
         this.flowsheet.ResetActivity();
      }

      private void radioButtonRecycle_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonRecycle.Checked)
            this.flowsheet.AddRecycle();
      }

      private void Toolbox_LocationChanged(object sender, EventArgs e)
      {
         this.flowsheet.ToolboxLocation = this.Location;
      }

      private void radioButtonLiquidMaterialStream_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonLiquidMaterialStream.Checked)
            this.flowsheet.AddLiquidMaterialStream();
      }

      private void radioButtonSolidMaterialStream_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonSolidMaterialStream.Checked)
            this.flowsheet.AddSolidMaterialStream();
      }

      private void radioButtonDryerLiquid_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonDryerLiquid.Checked)
            this.flowsheet.AddLiquidDryer();
      }

      private void radioButtonDryerSolid_CheckedChanged(object sender, System.EventArgs e)
      {
         if (this.radioButtonDryerSolid.Checked)
            this.flowsheet.AddSolidDryer();
      }
   }
}
