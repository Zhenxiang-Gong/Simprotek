using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Prosimo.SubstanceLibrary
{
   public enum ThermProp { GasCp, LiquidCp, EvapHeat, VapPressure, LiquidDensity, GasVisc, LiquidVisc, GasK, LiquidK, SurfaceTension, CriticalProp};
   /// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private const string ENCODED_DIR = "extracted";

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private OpenFileDialog openFileDialog;
      private TabControl fileTab;
      private TabPage tabPageFiles;
      private Button buttonExtractData;
      private Button buttonRemoveFiles;
      private Button buttonAddFiles;
      private ListBox listBoxFiles;
      private IContainer components;
      private ComboBox propComboBox;
      private ComboBox substanceTypeComboBox;

      private SubstanceType substanceType;
      private ThermProp thermProp;
      private SubstanceFormula substanceFormula;

      IList<Substance> substanceList = new List<Substance>();
      IList<YawsGasCpCorrelation> gasCpCorrList = new List<YawsGasCpCorrelation>();
      IList<YawsLiquidCpCorrelation> liquidCpCorrList = new List<YawsLiquidCpCorrelation>();
      IList<YawsGasThermalConductivityCorrelation> gasKCorrList = new List<YawsGasThermalConductivityCorrelation>();
      IList<YawsLiquidThermalConductivityCorrelation> liquidKCorrList = new List<YawsLiquidThermalConductivityCorrelation>();
      IList<YawsGasViscosityCorrelation> gasViscCorrList = new List<YawsGasViscosityCorrelation>();
      IList<YawsLiquidViscosityCorrelation> liquidViscCorrList = new List<YawsLiquidViscosityCorrelation>();
      IList<YawsLiquidDensityCorrelation> liquidDensityCorrList = new List<YawsLiquidDensityCorrelation>();
      IList<YawsEvaporationHeatCorrelation> evapHeatCorrList = new List<YawsEvaporationHeatCorrelation>();
      IList<YawsVaporPressureCorrelation> vapPressureCorrList = new List<YawsVaporPressureCorrelation>();
      IList<YawsSurfaceTensionCorrelation> surfaceTensionCorrList = new List<YawsSurfaceTensionCorrelation>();

      IDictionary<string, YawsGasCpCorrelation> gasCpTable = new Dictionary<string, YawsGasCpCorrelation>();
      IDictionary<string, YawsGasThermalConductivityCorrelation> gasKTable = new Dictionary<string, YawsGasThermalConductivityCorrelation>();
      IDictionary<string, YawsGasViscosityCorrelation> gasViscTable = new Dictionary<string, YawsGasViscosityCorrelation>();
      IDictionary<string, YawsLiquidCpCorrelation> liquidCpTable = new Dictionary<string, YawsLiquidCpCorrelation>();
      IDictionary<string, YawsLiquidThermalConductivityCorrelation> liquidKTable = new Dictionary<string, YawsLiquidThermalConductivityCorrelation>();
      IDictionary<string, YawsLiquidViscosityCorrelation> liquidViscTable = new Dictionary<string, YawsLiquidViscosityCorrelation>();
      IDictionary<string, YawsLiquidDensityCorrelation> liquidDensityTable = new Dictionary<string, YawsLiquidDensityCorrelation>();
      IDictionary<string, YawsEvaporationHeatCorrelation> evapHeatable = new Dictionary<string, YawsEvaporationHeatCorrelation>();
      IDictionary<string, YawsVaporPressureCorrelation> vapPressureTable = new Dictionary<string, YawsVaporPressureCorrelation>();
      IDictionary<string, YawsSurfaceTensionCorrelation>surfaceTensionTable = new Dictionary<string, YawsSurfaceTensionCorrelation>();

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
         this.components = new System.ComponentModel.Container();
         this.panel = new System.Windows.Forms.Panel();
         this.fileTab = new System.Windows.Forms.TabControl();
         this.tabPageFiles = new System.Windows.Forms.TabPage();
         this.buttonExtractData = new System.Windows.Forms.Button();
         this.buttonRemoveFiles = new System.Windows.Forms.Button();
         this.buttonAddFiles = new System.Windows.Forms.Button();
         this.listBoxFiles = new System.Windows.Forms.ListBox();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.propComboBox = new System.Windows.Forms.ComboBox();
         this.substanceTypeComboBox = new System.Windows.Forms.ComboBox();
         this.panel.SuspendLayout();
         this.fileTab.SuspendLayout();
         this.tabPageFiles.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.fileTab);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(579, 398);
         this.panel.TabIndex = 1;
         // 
         // fileTab
         // 
         this.fileTab.Controls.Add(this.tabPageFiles);
         this.fileTab.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fileTab.Location = new System.Drawing.Point(0, 0);
         this.fileTab.Name = "fileTab";
         this.fileTab.SelectedIndex = 0;
         this.fileTab.Size = new System.Drawing.Size(575, 394);
         this.fileTab.TabIndex = 5;
         // 
         // tabPageFiles
         // 
         this.tabPageFiles.Controls.Add(this.substanceTypeComboBox);
         this.tabPageFiles.Controls.Add(this.propComboBox);
         this.tabPageFiles.Controls.Add(this.buttonExtractData);
         this.tabPageFiles.Controls.Add(this.buttonRemoveFiles);
         this.tabPageFiles.Controls.Add(this.buttonAddFiles);
         this.tabPageFiles.Controls.Add(this.listBoxFiles);
         this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
         this.tabPageFiles.Name = "tabPageFiles";
         this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageFiles.Size = new System.Drawing.Size(567, 368);
         this.tabPageFiles.TabIndex = 1;
         this.tabPageFiles.Text = "File Panel";
         this.tabPageFiles.UseVisualStyleBackColor = true;
         this.tabPageFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageFiles_MouseClick);
         // 
         // buttonExtractData
         // 
         this.buttonExtractData.AllowDrop = true;
         this.buttonExtractData.Location = new System.Drawing.Point(466, 339);
         this.buttonExtractData.Name = "buttonExtractData";
         this.buttonExtractData.Size = new System.Drawing.Size(98, 23);
         this.buttonExtractData.TabIndex = 3;
         this.buttonExtractData.Text = "Extract Data";
         this.buttonExtractData.UseVisualStyleBackColor = true;
         this.buttonExtractData.Click += new System.EventHandler(this.buttonExtractData_Click);
         // 
         // buttonRemoveFiles
         // 
         this.buttonRemoveFiles.Location = new System.Drawing.Point(84, 339);
         this.buttonRemoveFiles.Name = "buttonRemoveFiles";
         this.buttonRemoveFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveFiles.TabIndex = 2;
         this.buttonRemoveFiles.Text = "Remove";
         this.buttonRemoveFiles.UseVisualStyleBackColor = true;
         this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
         // 
         // buttonAddFiles
         // 
         this.buttonAddFiles.Location = new System.Drawing.Point(3, 339);
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
         this.listBoxFiles.Size = new System.Drawing.Size(555, 316);
         this.listBoxFiles.TabIndex = 0;
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // openFileDialog
         // 
         this.openFileDialog.Multiselect = true;
         // 
         // propComboBox
         // 
         this.propComboBox.FormattingEnabled = true;
         this.propComboBox.Items.AddRange(new object[] {
            "GasCp",
            "LiquidCp",
            "EvapHeat",
            "VapPressure",
            "LiquidDensity",
            "GasVisc",
            "LiquidVisc",
            "GasK",
            "LiquidK",
            "SurfaceTension",
            "CriticalProp"});
         this.propComboBox.Location = new System.Drawing.Point(329, 341);
         this.propComboBox.Name = "propComboBox";
         this.propComboBox.Size = new System.Drawing.Size(121, 21);
         this.propComboBox.TabIndex = 4;
         this.propComboBox.SelectedIndexChanged += new System.EventHandler(this.propComboBox_SelectedIndexChanged);
         // 
         // substanceTypeComboBox
         // 
         this.substanceTypeComboBox.FormattingEnabled = true;
         this.substanceTypeComboBox.Items.AddRange(new object[] {
            "Inorganic",
            "Organic"});
         this.substanceTypeComboBox.Location = new System.Drawing.Point(187, 339);
         this.substanceTypeComboBox.Name = "substanceTypeComboBox";
         this.substanceTypeComboBox.Size = new System.Drawing.Size(121, 21);
         this.substanceTypeComboBox.TabIndex = 5;
         this.substanceTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.substanceTypeComboBox_SelectedIndexChanged);
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(579, 398);
         this.Controls.Add(this.panel);
         this.Menu = this.mainMenu;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Data Extraction Manager";
         this.panel.ResumeLayout(false);
         this.fileTab.ResumeLayout(false);
         this.tabPageFiles.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonAddFiles_Click(object sender, EventArgs e)
      {
         openFileDialog.Filter = "htm files (*.htm)|*.htm";
         openFileDialog.FilterIndex = 2;

         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            listBoxFiles.BeginUpdate();
            for (int i = 0; i < openFileDialog.FileNames.Length; i++)
            {
               string fileName = openFileDialog.FileNames[i];
               if (!listBoxFiles.Items.Contains(fileName))
                  listBoxFiles.Items.Add(openFileDialog.FileNames[i]);
            }
            listBoxFiles.EndUpdate();
         }

      }

      private void buttonRemoveFiles_Click(object sender, EventArgs e)
      {
         ArrayList list = new ArrayList();
         foreach (object fileName in listBoxFiles.SelectedItems)
         {
            list.Add(fileName);
         }

         foreach (object fileName in list)
         {
            listBoxFiles.Items.Remove(fileName);
         }
      }

      private void buttonExtractData_Click(object sender, EventArgs evtArgs)
      {
         listBoxFiles.ClearSelected();
         this.Cursor = Cursors.WaitCursor;

         ExtractCoeffs();
         this.Cursor = Cursors.Default;
      }

      private void ExtractCoeffs() {
         FileStream fsRead = null;
         try {
            char[] separator1 = { '>' };
            char[] separator2 = { '<' };
            char[] emptySeparator = { ' ' };
            char[] separators = { '>', '<' };
            string[] subStrs;
            string[] subSubStrs;
            string formula;
            string name;
            string casRegestryNo;
            double a, b, c, d = 1, e = 1, f, g, bp = 1;
            double molarWeight, freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, accentricF;
            molarWeight = freezingPoint = boilingPoint = criticalT = criticalP = criticalV = criticalComp = accentricF = -2147483D;
            double minTemp = 1, maxTemp = 1;
            StringBuilder sb;
            CriticalPropertiesAndAccentricFactor criticalProps;
            Substance substance;
            YawsGasCpCorrelation gasCpCorrelation;
            YawsLiquidCpCorrelation liquidCpCorrelation;
            YawsGasThermalConductivityCorrelation gasKCorrelation;
            YawsLiquidThermalConductivityCorrelation liquidKCorrelation;
            YawsGasViscosityCorrelation gasViscCorrelation;
            YawsLiquidViscosityCorrelation liquidViscCorrelation;
            YawsLiquidDensityCorrelation liquidDensityCorrelation;
            YawsEvaporationHeatCorrelation evapHeatCorrelation;
            YawsVaporPressureCorrelation vapPressureCorrelation;
            YawsSurfaceTensionCorrelation surfaceTensionCorrelation;

            string line1, line2, line3, line4, line5, line6, line7, line8, line9, line10, line11, lineTemp;
            string tmpStrOld = "";
            string tmpStr;
            int number;
            StreamReader reader;

            foreach (object fullFileName in listBoxFiles.Items) {
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               line1 = reader.ReadLine();
               while (!reader.EndOfStream) {
                  line2 = reader.ReadLine();
                  if (line1.Contains("<TD class=BorderHelper>") && line2.Contains("<TD class=BorderHelper noWrap>")) {
                     substanceFormula = new SubstanceFormula();

                     subStrs = line1.Split(separator1);
                     sb = new StringBuilder();
                     for (int i = 1; i < subStrs.Length - 1; i++) {
                        subSubStrs = subStrs[i].Split(separator2);
                        sb.Append(subSubStrs[0]);
                        tmpStr = subSubStrs[0];
                        if (char.IsDigit(tmpStr[0])) {
                           number = int.Parse(tmpStr);
                           substanceFormula.AddElement(tmpStrOld, number);
                        }
                        else {
                           if (!tmpStr.Equals(" ")) {
                              tmpStr = Parse(tmpStr.TrimEnd());
                           }
                           if (i == (subStrs.Length - 2) && !tmpStr.Equals(" ") && !tmpStr.Equals("")) {
                              substanceFormula.AddElement(tmpStr, 1);
                           }
                        }
                        tmpStrOld = tmpStr;
                     }
                     formula = sb.ToString();

                     if (!line2.Contains("</TD>")) {
                        lineTemp = reader.ReadLine();
                        line2 = line2 + lineTemp;
                     }

                     if (!line2.Contains("<I>")) {
                        subStrs = line2.Split(separators);
                        name = subStrs[2];
                     }
                     else {
                        subStrs = line2.Split(separators);
                        sb = new StringBuilder();
                        for (int i = 2; i < subStrs.Length; i++) {
                           if (!(subStrs[i].Equals("") || subStrs[i].Equals("I") || subStrs[i].Equals("/I") || subStrs[i].Equals("/TD"))) {
                              sb.Append(subStrs[i]);
                           }
                        }
                        name = sb.ToString();
                     }
                     name = name.TrimEnd(emptySeparator);

                     line3 = reader.ReadLine();
                     subStrs = line3.Split(separators);
                     if (subStrs[2].Contains("&")) {
                        casRegestryNo = "";
                     }
                     else {
                        casRegestryNo = subStrs[2];
                     }

                     line4 = reader.ReadLine();
                     subStrs = line4.Split(separators);
                     a = double.Parse(subStrs[2]);
                     if (thermProp == ThermProp.CriticalProp) {
                        molarWeight = a;
                     }

                     line5 = reader.ReadLine();
                     subStrs = line5.Split(separators);
                     if (subStrs[2].Contains("&")) {
                        b = -2147483D;
                     }
                     else {
                        b = double.Parse(subStrs[2]);
                     }
                     if (thermProp == ThermProp.CriticalProp) {
                        freezingPoint = b;
                     }

                     line6 = reader.ReadLine();
                     subStrs = line6.Split(separators);
                     if (subStrs[2].Contains("&")) {
                        c = -2147483D; ;
                     }
                     else {
                        c = double.Parse(subStrs[2]);
                     }
                     if (thermProp == ThermProp.CriticalProp) {
                        boilingPoint = c;
                     }

                     line7 = reader.ReadLine();
                     subStrs = line7.Split(separators);
                     if (subStrs[2].Contains("&")) {
                        d = -2147483D;
                     }
                     else {
                        d = double.Parse(subStrs[2]);
                     }
                     if (thermProp == ThermProp.EvapHeat || thermProp == ThermProp.GasVisc ||
                        thermProp == ThermProp.GasK || thermProp == ThermProp.LiquidK || 
                        thermProp == ThermProp.SurfaceTension) {
                        minTemp = d;
                     }
                     else if (thermProp == ThermProp.CriticalProp) {
                        criticalT = d;
                     }

                     line8 = reader.ReadLine();
                     subStrs = line8.Split(separators);
                     if (subStrs[2].Contains("&")) {
                        e = -2147483D;
                     }
                     else {
                        e = double.Parse(subStrs[2]);
                     }

                     if (thermProp == ThermProp.EvapHeat || thermProp == ThermProp.GasVisc || 
                        thermProp == ThermProp.GasK || thermProp == ThermProp.LiquidK ||
                        thermProp == ThermProp.SurfaceTension) {
                        maxTemp = e;
                     }
                     else if (thermProp == ThermProp.LiquidCp || thermProp == ThermProp.LiquidDensity || thermProp == ThermProp.LiquidVisc) {
                        minTemp = e;
                     }
                     else if (thermProp == ThermProp.CriticalProp) {
                        criticalP = 1.0e5 * e;//convert from bar to Pa.sec
                     }

                     line9 = reader.ReadLine();
                     subStrs = line9.Split(separators);
                     if (!subStrs[2].Contains("&")) {
                        f = double.Parse(subStrs[2]);
                     }
                     else {
                        f = -2147483D;
                     }
                     if (thermProp == ThermProp.EvapHeat) {
                        bp = f;
                     }
                     else if (thermProp == ThermProp.CriticalProp) {
                        criticalV = f * 1.0e-6; //convert from cm3/mol to m3/mol
                        line9 = reader.ReadLine();//skip critical density
                     }
                     else if (thermProp == ThermProp.GasCp || thermProp == ThermProp.VapPressure) {
                        minTemp = f;
                     }
                     else if (thermProp == ThermProp.LiquidCp || thermProp == ThermProp.LiquidDensity || thermProp == ThermProp.LiquidVisc) {
                        maxTemp = f;
                     }

                     line10 = reader.ReadLine();
                     subStrs = line10.Split(separators);
                     if (!subStrs[2].Contains("&")) {
                        g = double.Parse(subStrs[2]);
                     }
                     else {
                        g = -2147483D;
                     }

                     if (thermProp == ThermProp.CriticalProp) {
                        criticalComp = g;
                     }
                     else if (thermProp == ThermProp.GasCp || thermProp == ThermProp.VapPressure) {
                        maxTemp = g;
                     }

                     line11 = reader.ReadLine();
                     subStrs = line11.Split(separators);
                     if (thermProp == ThermProp.CriticalProp) {
                        if (!subStrs[2].Contains("&")) {
                           accentricF = double.Parse(subStrs[2]);
                        }
                        else {
                           accentricF = -2147483D;
                        }
                     }

                     if (thermProp == ThermProp.CriticalProp) {
                        criticalProps = new CriticalPropertiesAndAccentricFactor(freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, accentricF);
                        substance = new Substance(name, substanceType, casRegestryNo, substanceFormula, molarWeight, criticalProps);
                        substanceList.Add(substance);
                     }
                     else if (thermProp == ThermProp.GasCp) {
                        gasCpCorrelation = new YawsGasCpCorrelation(name, a, b, c, d, e, minTemp, maxTemp);
                        gasCpCorrList.Add(gasCpCorrelation);
                     }
                     else if (thermProp == ThermProp.LiquidCp) {
                        liquidCpCorrelation = new YawsLiquidCpCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidCpCorrList.Add(liquidCpCorrelation);
                     }
                     else if (thermProp == ThermProp.EvapHeat) {
                        evapHeatCorrelation = new YawsEvaporationHeatCorrelation(name, a, b, c, minTemp, maxTemp, bp);
                        evapHeatCorrList.Add(evapHeatCorrelation);
                     }
                     else if (thermProp == ThermProp.VapPressure) {
                        vapPressureCorrelation = new YawsVaporPressureCorrelation(name, a, b, c, d, e, minTemp, maxTemp);
                        vapPressureCorrList.Add(vapPressureCorrelation);
                     }
                     else if (thermProp == ThermProp.LiquidDensity) {
                        liquidDensityCorrelation = new YawsLiquidDensityCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidDensityCorrList.Add(liquidDensityCorrelation);
                     }
                     else if (thermProp == ThermProp.GasVisc) {
                        gasViscCorrelation = new YawsGasViscosityCorrelation(name, a, b, c, minTemp, maxTemp);
                        gasViscCorrList.Add(gasViscCorrelation);
                     }
                     else if (thermProp == ThermProp.LiquidVisc) {
                        liquidViscCorrelation = new YawsLiquidViscosityCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidViscCorrList.Add(liquidViscCorrelation);
                     }
                     else if (thermProp == ThermProp.GasK) {
                        gasKCorrelation = new YawsGasThermalConductivityCorrelation(name, a, b, c, minTemp, maxTemp);
                        gasKCorrList.Add(gasKCorrelation);
                     }
                     else if (thermProp == ThermProp.LiquidK) {
                        liquidKCorrelation = new YawsLiquidThermalConductivityCorrelation(name, a, b, c, minTemp, maxTemp);
                        liquidKCorrList.Add(liquidKCorrelation);
                     }
                     else if (thermProp == ThermProp.SurfaceTension) {
                        surfaceTensionCorrelation = new YawsSurfaceTensionCorrelation(name, a, b, c, minTemp, maxTemp);
                        surfaceTensionCorrList.Add(surfaceTensionCorrelation);
                     }
                  }
                  line1 = line2;
               }
               reader.Close();
               fsRead.Close();
            }
         }
         catch (Exception ex) {
            Console.WriteLine("The process failed: {0}", ex.ToString());
         }
         finally {
            if (fsRead != null) {
               fsRead.Close();
            }
         }
      }

      private void tabPageFiles_MouseClick(object sender, MouseEventArgs e)
      {
         listBoxFiles.ClearSelected();
      }

      private void substanceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
         int idx = substanceTypeComboBox.SelectedIndex;
         if (idx == 0) {
            substanceType = SubstanceType.Inorganic;
         }
         else if (idx == 1) {
            substanceType = SubstanceType.Organic;
         }
      }

      private void propComboBox_SelectedIndexChanged(object sender, EventArgs e) {
         int idx = propComboBox.SelectedIndex;
         if (idx == 0) {
            thermProp = ThermProp.GasCp;
         }
         else if (idx == 1) {
            thermProp = ThermProp.LiquidCp;
         }
         else if (idx == 2) {
            thermProp = ThermProp.EvapHeat;
         }
         else if (idx == 3) {
            thermProp = ThermProp.VapPressure;
         }
         else if (idx == 4) {
            thermProp = ThermProp.LiquidDensity;
         }
         else if (idx == 5) {
            thermProp = ThermProp.GasVisc;
         }
         else if (idx == 6) {
            thermProp = ThermProp.LiquidVisc;
         }
         else if (idx == 7) {
            thermProp = ThermProp.GasK;
         }
         else if (idx == 8) {
            thermProp = ThermProp.LiquidK;
         }
         else if (idx == 9) {
            thermProp = ThermProp.SurfaceTension;
         }
         else if (idx == 10) {
            thermProp = ThermProp.CriticalProp;
         }
      }

      private string Parse(string tmpStr) {
         string retStr = "";
         if (tmpStr.Length == 1 || (tmpStr.Length == 2 && char.IsUpper(tmpStr[0]) && char.IsLower(tmpStr[1]))) {
            return tmpStr;
         }
         else {
            if (char.IsUpper(tmpStr[0]) && char.IsLower(tmpStr[1])) {
               substanceFormula.AddElement(tmpStr.Substring(0, 2), 1);
               retStr = Parse(tmpStr.Substring(2));
            }
            else if (char.IsUpper(tmpStr[0]) && char.IsUpper(tmpStr[1])) {
               substanceFormula.AddElement(tmpStr.Substring(0, 1), 1);
               retStr = Parse(tmpStr.Substring(1));
            }
         }
         return retStr;
      }

	}
}
