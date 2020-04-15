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

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for FlowsheetPreferencesForm.
	/// </summary>
	public class FlowsheetPreferencesForm : System.Windows.Forms.Form
	{
      private string CURRENT_UNIT_SYS = "";

      private Flowsheet flowsheet;
      private UnitSystemsControl unitSystemsCtrl;
      private System.Windows.Forms.TabControl tabControlPrefs;
      private System.Windows.Forms.TabPage tabPageTypes;
      private System.Windows.Forms.TabPage tabPageUnits;
      private System.Windows.Forms.Button buttonCurrentUnitSys;
      private System.Windows.Forms.Label labelCurrent;
      private System.Windows.Forms.GroupBox groupBoxCurrentUnitSystem;
      private System.Windows.Forms.MainMenu mainMenu1;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.TabPage tabPageEditor;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.TabPage tabPageNumberFormat;
      private System.Windows.Forms.DomainUpDown domainUpDownDecPlaces;
      private System.Windows.Forms.RadioButton radioButtonFixedPoint;
      private System.Windows.Forms.RadioButton radioButtonScientific;
      private System.Windows.Forms.Label labelDecimalPlaces;
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
      private System.Windows.Forms.RadioButton radioButtonTeeStreamsTypeMaterial;
      private System.Windows.Forms.RadioButton radioButtonTeeStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxMixerStreamsType;
      private System.Windows.Forms.RadioButton radioButtonMixerStreamsTypeMaterial;
      private System.Windows.Forms.RadioButton radioButtonMixerStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxValveStreamsType;
      private System.Windows.Forms.RadioButton radioButtonValveStreamsTypeMaterial;
      private System.Windows.Forms.RadioButton radioButtonValveStreamsTypeGas;
      private System.Windows.Forms.GroupBox groupBoxTeeMixerValveStreamsType;
      private System.Windows.Forms.Label labelDryingGas;
      private System.Windows.Forms.TextBox textBoxDryingGas;
      private System.Windows.Forms.TextBox textBoxDryingMaterial;
      private System.Windows.Forms.Label labelDryingMaterial;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FlowsheetPreferencesForm(Flowsheet flowsheet)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         if (flowsheet != null)
         {
            this.flowsheet = flowsheet;
            EvaporationAndDryingSystem system = this.flowsheet.EvaporationAndDryingSystem;
            this.textBoxDryingGas.Text = system.DryingGas.ToString();
            this.textBoxDryingMaterial.Text = system.DryingMaterial.ToString();
            this.SetPreferences();

            // 
            unitSystemsCtrl = new UnitSystemsControl();
            this.tabPageUnits.Controls.Add(unitSystemsCtrl);
            unitSystemsCtrl.Location = new Point(0, 0);

            UnitSystem unitSystem = unitSystemsCtrl.GetSelectedUnitSystem();
            int selIdx = unitSystemsCtrl.GetSelectedIndex();
            this.buttonCurrentUnitSys.Enabled = false;
            if (selIdx >= 0)
            {
               if (unitSystem != null)
               {
                  this.buttonCurrentUnitSys.Enabled = true;
               }
            }

            UnitSystem currentUnitSystem = UnitSystemService.GetInstance().CurrentUnitSystem;
            this.labelCurrent.Text = this.CURRENT_UNIT_SYS + currentUnitSystem.Name;
            unitSystemsCtrl.SelectedUnitSystemChanged += new SelectedUnitSystemChangedEventHandler(unitSystemsCtrl_SelectedUnitSystemChanged);

            //
            SelectSolvablesControl selectSolvablesControl = new SelectSolvablesControl(flowsheet);
            selectSolvablesControl.Location = new Point(4,4);
            this.tabPageEditor.Controls.Add(selectSolvablesControl);

            this.domainUpDownDecPlaces.SelectedItem = this.flowsheet.DecimalPlaces;
            if (this.flowsheet.NumericFormat.Equals(NumericFormat.FixedPoint))
            {
               this.radioButtonFixedPoint.Checked = true;
            }
            else if (this.flowsheet.NumericFormat.Equals(NumericFormat.Scientific))
            {
               this.radioButtonScientific.Checked = true;
            }
         }
/*
         else
         {
            // 0 = types
            // 1 = unit systems
            // 2 = show in editor
            // 3 = numeric format
            this.tabControlPrefs.Controls.RemoveAt(3);
            this.tabControlPrefs.Controls.RemoveAt(2);
            this.tabControlPrefs.Controls.RemoveAt(1);

            this.groupBoxHeatExchangerInletType.Visible = false;
            this.groupBoxUnitOpCreation.Visible = false;
            this.groupBoxTeeMixerValveStreamsType.Visible = false;
         }
*/
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.unitSystemsCtrl != null)
            this.unitSystemsCtrl.SelectedUnitSystemChanged -= new SelectedUnitSystemChangedEventHandler(unitSystemsCtrl_SelectedUnitSystemChanged);
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
         this.tabControlPrefs = new System.Windows.Forms.TabControl();
         this.tabPageTypes = new System.Windows.Forms.TabPage();
         this.textBoxDryingMaterial = new System.Windows.Forms.TextBox();
         this.labelDryingMaterial = new System.Windows.Forms.Label();
         this.textBoxDryingGas = new System.Windows.Forms.TextBox();
         this.labelDryingGas = new System.Windows.Forms.Label();
         this.groupBoxTeeMixerValveStreamsType = new System.Windows.Forms.GroupBox();
         this.groupBoxTeeStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonTeeStreamsTypeMaterial = new System.Windows.Forms.RadioButton();
         this.radioButtonTeeStreamsTypeGas = new System.Windows.Forms.RadioButton();
         this.groupBoxMixerStreamsType = new System.Windows.Forms.GroupBox();
         this.radioButtonMixerStreamsTypeMaterial = new System.Windows.Forms.RadioButton();
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
         this.tabPageUnits = new System.Windows.Forms.TabPage();
         this.groupBoxCurrentUnitSystem = new System.Windows.Forms.GroupBox();
         this.buttonCurrentUnitSys = new System.Windows.Forms.Button();
         this.labelCurrent = new System.Windows.Forms.Label();
         this.tabPageEditor = new System.Windows.Forms.TabPage();
         this.tabPageNumberFormat = new System.Windows.Forms.TabPage();
         this.radioButtonScientific = new System.Windows.Forms.RadioButton();
         this.radioButtonFixedPoint = new System.Windows.Forms.RadioButton();
         this.domainUpDownDecPlaces = new System.Windows.Forms.DomainUpDown();
         this.labelDecimalPlaces = new System.Windows.Forms.Label();
         this.mainMenu1 = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.tabControlPrefs.SuspendLayout();
         this.tabPageTypes.SuspendLayout();
         this.groupBoxTeeMixerValveStreamsType.SuspendLayout();
         this.groupBoxTeeStreamsType.SuspendLayout();
         this.groupBoxMixerStreamsType.SuspendLayout();
         this.groupBoxValveStreamsType.SuspendLayout();
         this.groupBoxHeatExchangerInletType.SuspendLayout();
         this.groupBoxHeatExchangerHotSide.SuspendLayout();
         this.groupBoxHeatExchangerColdSide.SuspendLayout();
         this.groupBoxUnitOpCreation.SuspendLayout();
         this.tabPageUnits.SuspendLayout();
         this.groupBoxCurrentUnitSystem.SuspendLayout();
         this.tabPageNumberFormat.SuspendLayout();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // tabControlPrefs
         // 
         this.tabControlPrefs.Controls.Add(this.tabPageTypes);
         this.tabControlPrefs.Controls.Add(this.tabPageUnits);
         this.tabControlPrefs.Controls.Add(this.tabPageEditor);
         this.tabControlPrefs.Controls.Add(this.tabPageNumberFormat);
         this.tabControlPrefs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControlPrefs.Location = new System.Drawing.Point(0, 0);
         this.tabControlPrefs.Name = "tabControlPrefs";
         this.tabControlPrefs.SelectedIndex = 0;
         this.tabControlPrefs.Size = new System.Drawing.Size(414, 415);
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
         this.tabPageTypes.Size = new System.Drawing.Size(406, 389);
         this.tabPageTypes.TabIndex = 0;
         this.tabPageTypes.Text = "Stream Types";
         // 
         // textBoxDryingMaterial
         // 
         this.textBoxDryingMaterial.BackColor = System.Drawing.Color.White;
         this.textBoxDryingMaterial.Location = new System.Drawing.Point(100, 36);
         this.textBoxDryingMaterial.Name = "textBoxDryingMaterial";
         this.textBoxDryingMaterial.ReadOnly = true;
         this.textBoxDryingMaterial.TabIndex = 12;
         this.textBoxDryingMaterial.Text = "";
         // 
         // labelDryingMaterial
         // 
         this.labelDryingMaterial.Location = new System.Drawing.Point(12, 40);
         this.labelDryingMaterial.Name = "labelDryingMaterial";
         this.labelDryingMaterial.Size = new System.Drawing.Size(84, 16);
         this.labelDryingMaterial.TabIndex = 11;
         this.labelDryingMaterial.Text = "Drying Material:";
         this.labelDryingMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxDryingGas
         // 
         this.textBoxDryingGas.BackColor = System.Drawing.Color.White;
         this.textBoxDryingGas.Location = new System.Drawing.Point(100, 8);
         this.textBoxDryingGas.Name = "textBoxDryingGas";
         this.textBoxDryingGas.ReadOnly = true;
         this.textBoxDryingGas.TabIndex = 10;
         this.textBoxDryingGas.Text = "";
         // 
         // labelDryingGas
         // 
         this.labelDryingGas.Location = new System.Drawing.Point(12, 12);
         this.labelDryingGas.Name = "labelDryingGas";
         this.labelDryingGas.Size = new System.Drawing.Size(84, 16);
         this.labelDryingGas.TabIndex = 9;
         this.labelDryingGas.Text = "Drying Gas:";
         this.labelDryingGas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBoxTeeMixerValveStreamsType
         // 
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxTeeStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxMixerStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Controls.Add(this.groupBoxValveStreamsType);
         this.groupBoxTeeMixerValveStreamsType.Location = new System.Drawing.Point(16, 276);
         this.groupBoxTeeMixerValveStreamsType.Name = "groupBoxTeeMixerValveStreamsType";
         this.groupBoxTeeMixerValveStreamsType.Size = new System.Drawing.Size(264, 88);
         this.groupBoxTeeMixerValveStreamsType.TabIndex = 8;
         this.groupBoxTeeMixerValveStreamsType.TabStop = false;
         this.groupBoxTeeMixerValveStreamsType.Text = "Streams Type";
         // 
         // groupBoxTeeStreamsType
         // 
         this.groupBoxTeeStreamsType.Controls.Add(this.radioButtonTeeStreamsTypeMaterial);
         this.groupBoxTeeStreamsType.Controls.Add(this.radioButtonTeeStreamsTypeGas);
         this.groupBoxTeeStreamsType.Location = new System.Drawing.Point(8, 20);
         this.groupBoxTeeStreamsType.Name = "groupBoxTeeStreamsType";
         this.groupBoxTeeStreamsType.Size = new System.Drawing.Size(80, 60);
         this.groupBoxTeeStreamsType.TabIndex = 5;
         this.groupBoxTeeStreamsType.TabStop = false;
         this.groupBoxTeeStreamsType.Text = "Tee";
         // 
         // radioButtonTeeStreamsTypeMaterial
         // 
         this.radioButtonTeeStreamsTypeMaterial.Location = new System.Drawing.Point(8, 40);
         this.radioButtonTeeStreamsTypeMaterial.Name = "radioButtonTeeStreamsTypeMaterial";
         this.radioButtonTeeStreamsTypeMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonTeeStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonTeeStreamsTypeMaterial.Text = "Material";
         this.radioButtonTeeStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.TeeStreamsTypeHandler);
         // 
         // radioButtonTeeStreamsTypeGas
         // 
         this.radioButtonTeeStreamsTypeGas.Location = new System.Drawing.Point(8, 20);
         this.radioButtonTeeStreamsTypeGas.Name = "radioButtonTeeStreamsTypeGas";
         this.radioButtonTeeStreamsTypeGas.Size = new System.Drawing.Size(68, 16);
         this.radioButtonTeeStreamsTypeGas.TabIndex = 0;
         this.radioButtonTeeStreamsTypeGas.Text = "Gas";
         this.radioButtonTeeStreamsTypeGas.CheckedChanged += new System.EventHandler(this.TeeStreamsTypeHandler);
         // 
         // groupBoxMixerStreamsType
         // 
         this.groupBoxMixerStreamsType.Controls.Add(this.radioButtonMixerStreamsTypeMaterial);
         this.groupBoxMixerStreamsType.Controls.Add(this.radioButtonMixerStreamsTypeGas);
         this.groupBoxMixerStreamsType.Location = new System.Drawing.Point(92, 20);
         this.groupBoxMixerStreamsType.Name = "groupBoxMixerStreamsType";
         this.groupBoxMixerStreamsType.Size = new System.Drawing.Size(80, 60);
         this.groupBoxMixerStreamsType.TabIndex = 6;
         this.groupBoxMixerStreamsType.TabStop = false;
         this.groupBoxMixerStreamsType.Text = "Mixer";
         // 
         // radioButtonMixerStreamsTypeMaterial
         // 
         this.radioButtonMixerStreamsTypeMaterial.Location = new System.Drawing.Point(8, 40);
         this.radioButtonMixerStreamsTypeMaterial.Name = "radioButtonMixerStreamsTypeMaterial";
         this.radioButtonMixerStreamsTypeMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonMixerStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonMixerStreamsTypeMaterial.Text = "Material";
         this.radioButtonMixerStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.MixerStreamsTypeHandler);
         // 
         // radioButtonMixerStreamsTypeGas
         // 
         this.radioButtonMixerStreamsTypeGas.Location = new System.Drawing.Point(8, 20);
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
         this.groupBoxValveStreamsType.Location = new System.Drawing.Point(176, 20);
         this.groupBoxValveStreamsType.Name = "groupBoxValveStreamsType";
         this.groupBoxValveStreamsType.Size = new System.Drawing.Size(80, 60);
         this.groupBoxValveStreamsType.TabIndex = 7;
         this.groupBoxValveStreamsType.TabStop = false;
         this.groupBoxValveStreamsType.Text = "Valve";
         // 
         // radioButtonValveStreamsTypeMaterial
         // 
         this.radioButtonValveStreamsTypeMaterial.Location = new System.Drawing.Point(8, 40);
         this.radioButtonValveStreamsTypeMaterial.Name = "radioButtonValveStreamsTypeMaterial";
         this.radioButtonValveStreamsTypeMaterial.Size = new System.Drawing.Size(68, 16);
         this.radioButtonValveStreamsTypeMaterial.TabIndex = 1;
         this.radioButtonValveStreamsTypeMaterial.Text = "Material";
         this.radioButtonValveStreamsTypeMaterial.CheckedChanged += new System.EventHandler(this.ValveStreamsTypeHandler);
         // 
         // radioButtonValveStreamsTypeGas
         // 
         this.radioButtonValveStreamsTypeGas.Location = new System.Drawing.Point(8, 20);
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
         this.groupBoxHeatExchangerInletType.Location = new System.Drawing.Point(16, 176);
         this.groupBoxHeatExchangerInletType.Name = "groupBoxHeatExchangerInletType";
         this.groupBoxHeatExchangerInletType.Size = new System.Drawing.Size(180, 88);
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
         this.groupBoxUnitOpCreation.Location = new System.Drawing.Point(16, 84);
         this.groupBoxUnitOpCreation.Name = "groupBoxUnitOpCreation";
         this.groupBoxUnitOpCreation.Size = new System.Drawing.Size(180, 80);
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
         // tabPageUnits
         // 
         this.tabPageUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageUnits.Controls.Add(this.groupBoxCurrentUnitSystem);
         this.tabPageUnits.Location = new System.Drawing.Point(4, 22);
         this.tabPageUnits.Name = "tabPageUnits";
         this.tabPageUnits.Size = new System.Drawing.Size(406, 389);
         this.tabPageUnits.TabIndex = 1;
         this.tabPageUnits.Text = "Unit Systems";
         // 
         // groupBoxCurrentUnitSystem
         // 
         this.groupBoxCurrentUnitSystem.Controls.Add(this.buttonCurrentUnitSys);
         this.groupBoxCurrentUnitSystem.Controls.Add(this.labelCurrent);
         this.groupBoxCurrentUnitSystem.Location = new System.Drawing.Point(4, 340);
         this.groupBoxCurrentUnitSystem.Name = "groupBoxCurrentUnitSystem";
         this.groupBoxCurrentUnitSystem.Size = new System.Drawing.Size(396, 40);
         this.groupBoxCurrentUnitSystem.TabIndex = 7;
         this.groupBoxCurrentUnitSystem.TabStop = false;
         this.groupBoxCurrentUnitSystem.Text = "Current Unit System";
         // 
         // buttonCurrentUnitSys
         // 
         this.buttonCurrentUnitSys.Location = new System.Drawing.Point(308, 12);
         this.buttonCurrentUnitSys.Name = "buttonCurrentUnitSys";
         this.buttonCurrentUnitSys.TabIndex = 5;
         this.buttonCurrentUnitSys.Text = "Set Current";
         this.buttonCurrentUnitSys.Click += new System.EventHandler(this.buttonCurrentUnitSys_Click);
         // 
         // labelCurrent
         // 
         this.labelCurrent.Location = new System.Drawing.Point(128, 12);
         this.labelCurrent.Name = "labelCurrent";
         this.labelCurrent.Size = new System.Drawing.Size(168, 23);
         this.labelCurrent.TabIndex = 6;
         this.labelCurrent.Text = "Current Unit System:";
         this.labelCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // tabPageEditor
         // 
         this.tabPageEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageEditor.Location = new System.Drawing.Point(4, 22);
         this.tabPageEditor.Name = "tabPageEditor";
         this.tabPageEditor.Size = new System.Drawing.Size(406, 389);
         this.tabPageEditor.TabIndex = 2;
         this.tabPageEditor.Text = "Editor Prefs.";
         // 
         // tabPageNumberFormat
         // 
         this.tabPageNumberFormat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageNumberFormat.Controls.Add(this.radioButtonScientific);
         this.tabPageNumberFormat.Controls.Add(this.radioButtonFixedPoint);
         this.tabPageNumberFormat.Controls.Add(this.domainUpDownDecPlaces);
         this.tabPageNumberFormat.Controls.Add(this.labelDecimalPlaces);
         this.tabPageNumberFormat.Location = new System.Drawing.Point(4, 22);
         this.tabPageNumberFormat.Name = "tabPageNumberFormat";
         this.tabPageNumberFormat.Size = new System.Drawing.Size(406, 389);
         this.tabPageNumberFormat.TabIndex = 3;
         this.tabPageNumberFormat.Text = "Numeric Format";
         // 
         // radioButtonScientific
         // 
         this.radioButtonScientific.Location = new System.Drawing.Point(24, 52);
         this.radioButtonScientific.Name = "radioButtonScientific";
         this.radioButtonScientific.TabIndex = 3;
         this.radioButtonScientific.Text = "Scientific";
         this.radioButtonScientific.CheckedChanged += new System.EventHandler(this.radioButtonScientific_CheckedChanged);
         // 
         // radioButtonFixedPoint
         // 
         this.radioButtonFixedPoint.Checked = true;
         this.radioButtonFixedPoint.Location = new System.Drawing.Point(24, 24);
         this.radioButtonFixedPoint.Name = "radioButtonFixedPoint";
         this.radioButtonFixedPoint.TabIndex = 2;
         this.radioButtonFixedPoint.TabStop = true;
         this.radioButtonFixedPoint.Text = "Fixed Point";
         this.radioButtonFixedPoint.CheckedChanged += new System.EventHandler(this.radioButtonFixedPoint_CheckedChanged);
         // 
         // domainUpDownDecPlaces
         // 
         this.domainUpDownDecPlaces.BackColor = System.Drawing.Color.White;
         this.domainUpDownDecPlaces.Items.Add("20");
         this.domainUpDownDecPlaces.Items.Add("19");
         this.domainUpDownDecPlaces.Items.Add("18");
         this.domainUpDownDecPlaces.Items.Add("17");
         this.domainUpDownDecPlaces.Items.Add("16");
         this.domainUpDownDecPlaces.Items.Add("15");
         this.domainUpDownDecPlaces.Items.Add("14");
         this.domainUpDownDecPlaces.Items.Add("13");
         this.domainUpDownDecPlaces.Items.Add("12");
         this.domainUpDownDecPlaces.Items.Add("11");
         this.domainUpDownDecPlaces.Items.Add("10");
         this.domainUpDownDecPlaces.Items.Add("9");
         this.domainUpDownDecPlaces.Items.Add("8");
         this.domainUpDownDecPlaces.Items.Add("7");
         this.domainUpDownDecPlaces.Items.Add("6");
         this.domainUpDownDecPlaces.Items.Add("5");
         this.domainUpDownDecPlaces.Items.Add("4");
         this.domainUpDownDecPlaces.Items.Add("3");
         this.domainUpDownDecPlaces.Items.Add("2");
         this.domainUpDownDecPlaces.Items.Add("1");
         this.domainUpDownDecPlaces.Location = new System.Drawing.Point(252, 24);
         this.domainUpDownDecPlaces.Name = "domainUpDownDecPlaces";
         this.domainUpDownDecPlaces.ReadOnly = true;
         this.domainUpDownDecPlaces.Size = new System.Drawing.Size(48, 20);
         this.domainUpDownDecPlaces.TabIndex = 1;
         this.domainUpDownDecPlaces.SelectedItemChanged += new System.EventHandler(this.domainUpDownDecPlaces_SelectedItemChanged);
         // 
         // labelDecimalPlaces
         // 
         this.labelDecimalPlaces.Location = new System.Drawing.Point(148, 24);
         this.labelDecimalPlaces.Name = "labelDecimalPlaces";
         this.labelDecimalPlaces.TabIndex = 0;
         this.labelDecimalPlaces.Text = "Decimal Places:";
         this.labelDecimalPlaces.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
         this.panel.Size = new System.Drawing.Size(418, 419);
         this.panel.TabIndex = 5;
         // 
         // FlowsheetPreferencesForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(418, 419);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu1;
         this.MinimizeBox = false;
         this.Name = "FlowsheetPreferencesForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Flowsheet Preferences";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.FlowsheetPreferencesForm_Closing);
         this.tabControlPrefs.ResumeLayout(false);
         this.tabPageTypes.ResumeLayout(false);
         this.groupBoxTeeMixerValveStreamsType.ResumeLayout(false);
         this.groupBoxTeeStreamsType.ResumeLayout(false);
         this.groupBoxMixerStreamsType.ResumeLayout(false);
         this.groupBoxValveStreamsType.ResumeLayout(false);
         this.groupBoxHeatExchangerInletType.ResumeLayout(false);
         this.groupBoxHeatExchangerHotSide.ResumeLayout(false);
         this.groupBoxHeatExchangerColdSide.ResumeLayout(false);
         this.groupBoxUnitOpCreation.ResumeLayout(false);
         this.tabPageUnits.ResumeLayout(false);
         this.groupBoxCurrentUnitSystem.ResumeLayout(false);
         this.tabPageNumberFormat.ResumeLayout(false);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      public void SetPreferences()
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;

         if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithInputOnly)
            this.radioButtonUnitOpWithInput.Checked = true;
         else if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithInputAndOutput)
            this.radioButtonUnitOpWithInputAndOutput.Checked = true;
         else if (sysPrefs.UnitOpCreationType == UnitOpCreationType.WithoutInputAndOutput)
            this.radioButtonUnitOpWithoutInputAndOutput.Checked = true;

         if (sysPrefs.HeatExchangerColdInletType == typeof(DryingGasStream))
            this.radioButtonHeatExchangerColdSideGas.Checked = true;
         else if (sysPrefs.HeatExchangerColdInletType == typeof(DryingMaterialStream))
            this.radioButtonHeatExchangerColdSideMaterial.Checked = true;

         if (sysPrefs.HeatExchangerHotInletType == typeof(DryingGasStream))
            this.radioButtonHeatExchangerHotSideGas.Checked = true;
         else if (sysPrefs.HeatExchangerHotInletType == typeof(DryingMaterialStream))
            this.radioButtonHeatExchangerHotSideMaterial.Checked = true;

         if (sysPrefs.MixerStreamsType == typeof(DryingGasStream))
            this.radioButtonMixerStreamsTypeGas.Checked = true;
         else if (sysPrefs.MixerStreamsType == typeof(DryingMaterialStream))
            this.radioButtonMixerStreamsTypeMaterial.Checked = true;

         if (sysPrefs.ValveStreamsType == typeof(DryingGasStream))
            this.radioButtonValveStreamsTypeGas.Checked = true;
         else if (sysPrefs.ValveStreamsType == typeof(DryingMaterialStream))
            this.radioButtonValveStreamsTypeMaterial.Checked = true;

         if (sysPrefs.TeeStreamsType == typeof(DryingGasStream))
            this.radioButtonTeeStreamsTypeGas.Checked = true;
         else if (sysPrefs.TeeStreamsType == typeof(DryingMaterialStream))
            this.radioButtonTeeStreamsTypeMaterial.Checked = true;
      }

      private void buttonCurrentUnitSys_Click(object sender, System.EventArgs e)
      {
         UnitSystem unitSystem = this.unitSystemsCtrl.GetSelectedUnitSystem();
         if (unitSystem != null && unitSystem != UnitSystemService.GetInstance().CurrentUnitSystem)
         {
            UnitSystemService.GetInstance().CurrentUnitSystem = unitSystem;
            this.labelCurrent.Text = this.CURRENT_UNIT_SYS + unitSystem.Name;
         }
      }

      private void unitSystemsCtrl_SelectedUnitSystemChanged(object sender)
      {
         UnitSystemsControl usc = (UnitSystemsControl)sender;
         UnitSystem unitSystem = usc.GetSelectedUnitSystem();
         int selIdx = usc.GetSelectedIndex();
         this.buttonCurrentUnitSys.Enabled = false;
         if (selIdx >= 0)
         {
            if (unitSystem != null)
            {
               this.buttonCurrentUnitSys.Enabled = true;
            }
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }

      private void FlowsheetPreferencesForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // nothing at this moment
      }

      private void domainUpDownDecPlaces_SelectedItemChanged(object sender, System.EventArgs e)
      {
         this.flowsheet.DecimalPlaces = (string)this.domainUpDownDecPlaces.SelectedItem;
      }

      private void radioButtonFixedPoint_CheckedChanged(object sender, System.EventArgs e)
      {
         this.flowsheet.NumericFormat = NumericFormat.FixedPoint;
      }

      private void radioButtonScientific_CheckedChanged(object sender, System.EventArgs e)
      {
         this.flowsheet.NumericFormat = NumericFormat.Scientific;
      }

      private void UnitOperationCreationHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonUnitOpWithInput)
         {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithInputOnly;
         }
         else if (rb == this.radioButtonUnitOpWithInputAndOutput)
         {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithInputAndOutput;
         }
         else if (rb == this.radioButtonUnitOpWithoutInputAndOutput)
         {
            if (rb.Checked)
               sysPrefs.UnitOpCreationType = UnitOpCreationType.WithoutInputAndOutput;
         }
      }

      private void HeatExchangerColdInletHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonHeatExchangerColdSideGas)
         {
            if (rb.Checked)
               sysPrefs.HeatExchangerColdInletType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonHeatExchangerColdSideMaterial)
         {
            if (rb.Checked)
               sysPrefs.HeatExchangerColdInletType = typeof(DryingMaterialStream);
         }
      }

      private void HeatExchangerHotInletHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonHeatExchangerHotSideGas)
         {
            if (rb.Checked)
               sysPrefs.HeatExchangerHotInletType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonHeatExchangerHotSideMaterial)
         {
            if (rb.Checked)
               sysPrefs.HeatExchangerHotInletType = typeof(DryingMaterialStream);
         }
      }

      private void TeeStreamsTypeHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonTeeStreamsTypeGas)
         {
            if (rb.Checked)
               sysPrefs.TeeStreamsType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonTeeStreamsTypeMaterial)
         {
            if (rb.Checked)
               sysPrefs.TeeStreamsType = typeof(DryingMaterialStream);
         }
      }

      private void MixerStreamsTypeHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonMixerStreamsTypeGas)
         {
            if (rb.Checked)
               sysPrefs.MixerStreamsType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonMixerStreamsTypeMaterial)
         {
            if (rb.Checked)
               sysPrefs.MixerStreamsType = typeof(DryingMaterialStream);
         }
      }

      private void ValveStreamsTypeHandler(object sender, System.EventArgs e)
      {
         EvaporationAndDryingSystemPreferences sysPrefs = this.flowsheet.EvaporationAndDryingSystem.Preferences;
         RadioButton rb = (RadioButton)sender;
         if (rb == this.radioButtonValveStreamsTypeGas)
         {
            if (rb.Checked)
               sysPrefs.ValveStreamsType = typeof(DryingGasStream);
         }
         else if (rb == this.radioButtonValveStreamsTypeMaterial)
         {
            if (rb.Checked)
               sysPrefs.ValveStreamsType = typeof(DryingMaterialStream);
         }
      }
   }
}
