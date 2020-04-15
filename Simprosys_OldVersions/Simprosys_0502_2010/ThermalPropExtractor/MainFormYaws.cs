using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.YawsCorrelations;

namespace Prosimo.ThermalPropExtractor
{
   /// <summary>
   /// Summary description for MainForm.
   /// </summary>
   public class MainFormYaws : System.Windows.Forms.Form {
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
      private ThermPropType thermProp;
      private SubstanceFormula substanceFormula;

      ArrayList substanceList = new ArrayList();
      ArrayList gasCpCorrList = new ArrayList();
      ArrayList liquidCpCorrList = new ArrayList();
      ArrayList solidCpCorrList = new ArrayList();
      ArrayList evapHeatCorrList = new ArrayList();
      ArrayList vapPressureCorrList = new ArrayList();
      ArrayList liquidDensityCorrList = new ArrayList();
      ArrayList gasKCorrList = new ArrayList();
      ArrayList liquidKCorrList = new ArrayList();
      ArrayList gasViscCorrList = new ArrayList();
      ArrayList liquidViscCorrList = new ArrayList();
      ArrayList surfaceTensionCorrList = new ArrayList();
      ArrayList enthalpyOfFormationCorrList = new ArrayList();

      public MainFormYaws() {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
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
            "SolidCp",
            "EvapHeat",
            "VapPressure",
            "LiquidDensity",
            "GasVisc",
            "LiquidVisc",
            "GasK",
            "LiquidK",
            "SurfaceTension",
            "EnthalpyOfFormation",
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
      //[STAThread]
      //static void Main() {
      //   Application.Run(new MainFormYaws());
      //}

      private void menuItemClose_Click(object sender, System.EventArgs e) {
         this.Close();
      }

      private void buttonAddFiles_Click(object sender, EventArgs e) {
         openFileDialog.Filter = "html files (*.htm)|*.htm";
         openFileDialog.FilterIndex = 2;

         if (openFileDialog.ShowDialog() == DialogResult.OK) {
            listBoxFiles.BeginUpdate();
            for (int i = 0; i < openFileDialog.FileNames.Length; i++) {
               string fileName = openFileDialog.FileNames[i];
               if (!listBoxFiles.Items.Contains(fileName))
                  listBoxFiles.Items.Add(openFileDialog.FileNames[i]);
            }
            listBoxFiles.EndUpdate();
         }

      }

      private void buttonRemoveFiles_Click(object sender, EventArgs e) {
         ArrayList list = new ArrayList();
         foreach (object fileName in listBoxFiles.SelectedItems) {
            list.Add(fileName);
         }

         foreach (object fileName in list) {
            listBoxFiles.Items.Remove(fileName);
         }
      }

      private void buttonExtractData_Click(object sender, EventArgs evtArgs) {
         listBoxFiles.ClearSelected();
         this.Cursor = Cursors.WaitCursor;

         ExtractCoeffs();
         this.Cursor = Cursors.Default;
      }

      private void ExtractCoeffs() {
         FileStream fsRead = null;
         try {
            //char[] separator1 = { '>' };
            //char[] separator2 = { '<' };
            //char[] emptySeparator = { ' ' };
            //char[] separators = { '>', '<' };
            string[] separatorsStr1 = { "<TD class=BorderHelper noWrap>", "</TD>" };
            string[] separatorsStr2 = { "<I>", "</I>" };
            string[] separatorsStr3 = { "<TD class=BorderHelper>", "</TD>" };
            string[] separatorsStr4 = { "<SUB>", "</SUB>" };
            
            //string[] subStrs;
            //string[] subSubStrs;
            //string formula;
            //StringBuilder sb;
            string name;
            //string htmlTaggedName;
            string casRegestryNo;
            double a, b, c, d = 1, e = 1, f, g; //, bp = 1;
            double molarWeight, freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, acentricF;
            molarWeight = freezingPoint = boilingPoint = criticalT = criticalP = criticalV = criticalComp = acentricF = -2147483D;
            double minTemp = 1, maxTemp = 1;
            CriticalPropsAndAcentricFactor criticalProps;
            Substance substance;
            YawsGasCpCorrelation gasCpCorrelation;
            YawsLiquidCpCorrelation liquidCpCorrelation;
            YawsSolidCpCorrelation solidCpCorrelation;
            YawsGasThermalConductivityCorrelation gasKCorrelation;
            YawsLiquidThermalConductivityCorrelation liquidKCorrelation;
            YawsGasViscosityCorrelation gasViscCorrelation;
            YawsLiquidViscosityCorrelation liquidViscCorrelation;
            YawsLiquidDensityCorrelation liquidDensityCorrelation;
            YawsEvaporationHeatCorrelation evapHeatCorrelation;
            YawsVaporPressureCorrelation vapPressureCorrelation;
            YawsSurfaceTensionCorrelation surfaceTensionCorrelation;
            YawsEnthalpyOfFormationCorrelation enthalpyOfFormationCorrelation;

            //string line1, line2, line3, line4, line5, line6, line7, line8, line9, line10, line11, lineTemp;
            string line1, line2, line3, lineTemp;
            //string tmpStrOld = "";
            //string tmpStr;
            //string elementStr;
            //int elementCount;
            //bool isLine3ExtensionOfLine2;
            StreamReader reader;

            foreach (object fullFileName in listBoxFiles.Items) {
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               line1 = reader.ReadLine();
               while (!reader.EndOfStream) {
                  line2 = reader.ReadLine();
                  if (line2.StartsWith("<TD class=BorderHelper noWrap>") && !line2.EndsWith("</TD>")) {
                     line2 += reader.ReadLine();
                  }
                  if (line1.Contains("<TD class=BorderHelper>") && line2.Contains("<TD class=BorderHelper noWrap>")) {

                     ExtractFormula(line1, separatorsStr3, separatorsStr4);

                     name = ExtractName(line2, separatorsStr1, separatorsStr2);
                     
                     line3 = reader.ReadLine();
                     casRegestryNo = ExtractCasNo(line3, separatorsStr3);

                     lineTemp = reader.ReadLine();
                     a = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.CriticalProp) {
                        molarWeight = a;
                     }

                     lineTemp = reader.ReadLine();
                     b = ExtractValue(lineTemp, separatorsStr3);

                     if (thermProp == ThermPropType.CriticalProp) {
                        freezingPoint = b;
                     }

                     lineTemp = reader.ReadLine();
                     c = ExtractValue(lineTemp, separatorsStr3);

                     if (thermProp == ThermPropType.CriticalProp) {
                        boilingPoint = c;
                     }

                     lineTemp = reader.ReadLine();
                     d = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.SolidCp || thermProp == ThermPropType.EvapHeat ||
                        thermProp == ThermPropType.GasVisc || thermProp == ThermPropType.GasK ||
                        thermProp == ThermPropType.LiquidK || thermProp == ThermPropType.SurfaceTension ||
                        thermProp == ThermPropType.EnthalpyOfFormation) {
                        minTemp = d;
                     }
                     else if (thermProp == ThermPropType.CriticalProp) {
                        criticalT = d;
                     }

                     lineTemp = reader.ReadLine();
                     e = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.SolidCp || thermProp == ThermPropType.EvapHeat ||
                        thermProp == ThermPropType.GasVisc || thermProp == ThermPropType.GasK ||
                        thermProp == ThermPropType.LiquidK || thermProp == ThermPropType.SurfaceTension ||
                        thermProp == ThermPropType.EnthalpyOfFormation) {
                        maxTemp = e;
                     }
                     else if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {
                        minTemp = e;
                     }
                     else if (thermProp == ThermPropType.CriticalProp) {
                        criticalP = 1.0e5 * e;//convert from bar to Pa
                     }

                     if (thermProp == ThermPropType.EvapHeat || thermProp == ThermPropType.CriticalProp ||
                        thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure ||
                        thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity ||
                        thermProp == ThermPropType.LiquidVisc) {
                        lineTemp = reader.ReadLine();
                        f = ExtractValue(lineTemp, separatorsStr3);

                        //if (thermProp == ThermProp.EvapHeat) {
                        //   bp = f;
                        //}
                        if (thermProp == ThermPropType.CriticalProp) {
                           criticalV = f * 1.0e-6; //convert from cm3/mol to m3/mol
                           lineTemp = reader.ReadLine();//skip critical density
                        }
                        else if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           minTemp = f;
                        }
                        else if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {
                           maxTemp = f;
                        }
                     }

                     if (thermProp == ThermPropType.CriticalProp || thermProp == ThermPropType.GasCp ||
                        thermProp == ThermPropType.VapPressure) {
                        lineTemp = reader.ReadLine();
                        g = ExtractValue(lineTemp, separatorsStr3);

                        if (thermProp == ThermPropType.CriticalProp) {
                           criticalComp = g;
                        }
                        else if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           maxTemp = g;
                        }
                     }

                     if (thermProp == ThermPropType.CriticalProp) {
                        lineTemp = reader.ReadLine();
                        acentricF = ExtractValue(lineTemp, separatorsStr3);
                     }

                     if (thermProp == ThermPropType.CriticalProp) {
                        criticalProps = new CriticalPropsAndAcentricFactor(freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, acentricF);
                        substance = new Substance(name, substanceType, casRegestryNo, substanceFormula, molarWeight, criticalProps);
                        substanceList.Add(substance);
                     }
                     else if (thermProp == ThermPropType.GasCp) {
                        gasCpCorrelation = new YawsGasCpCorrelation(name, a, b, c, d, e, minTemp, maxTemp);
                        gasCpCorrList.Add(gasCpCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidCp) {
                        liquidCpCorrelation = new YawsLiquidCpCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidCpCorrList.Add(liquidCpCorrelation);
                     }
                     else if (thermProp == ThermPropType.SolidCp) {
                        solidCpCorrelation = new YawsSolidCpCorrelation(name, a, b, c, minTemp, maxTemp);
                        solidCpCorrList.Add(solidCpCorrelation);
                     }
                     else if (thermProp == ThermPropType.EvapHeat) {
                        evapHeatCorrelation = new YawsEvaporationHeatCorrelation(name, a, b, c, minTemp, maxTemp);
                        evapHeatCorrList.Add(evapHeatCorrelation);
                     }
                     else if (thermProp == ThermPropType.VapPressure) {
                        vapPressureCorrelation = new YawsVaporPressureCorrelation(name, a, b, c, d, e, minTemp, maxTemp);
                        vapPressureCorrList.Add(vapPressureCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidDensity) {
                        liquidDensityCorrelation = new YawsLiquidDensityCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidDensityCorrList.Add(liquidDensityCorrelation);
                     }
                     else if (thermProp == ThermPropType.GasVisc) {
                        gasViscCorrelation = new YawsGasViscosityCorrelation(name, a, b, c, minTemp, maxTemp);
                        gasViscCorrList.Add(gasViscCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidVisc) {
                        liquidViscCorrelation = new YawsLiquidViscosityCorrelation(name, a, b, c, d, minTemp, maxTemp);
                        liquidViscCorrList.Add(liquidViscCorrelation);
                     }
                     else if (thermProp == ThermPropType.GasK) {
                        gasKCorrelation = new YawsGasThermalConductivityCorrelation(name, a, b, c, minTemp, maxTemp);
                        gasKCorrList.Add(gasKCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidK) {
                        liquidKCorrelation = new YawsLiquidThermalConductivityCorrelation(name, a, b, c, minTemp, maxTemp);
                        liquidKCorrList.Add(liquidKCorrelation);
                     }
                     else if (thermProp == ThermPropType.SurfaceTension) {
                        surfaceTensionCorrelation = new YawsSurfaceTensionCorrelation(name, a, b, c, minTemp, maxTemp);
                        surfaceTensionCorrList.Add(surfaceTensionCorrelation);
                     }
                     else if (thermProp == ThermPropType.EnthalpyOfFormation) {
                        enthalpyOfFormationCorrelation = new YawsEnthalpyOfFormationCorrelation(name, a, b, c, minTemp, maxTemp);
                        enthalpyOfFormationCorrList.Add(enthalpyOfFormationCorrelation);
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

         if (substanceType == SubstanceType.Inorganic && thermProp == ThermPropType.CriticalProp) {
            PersistSubstances();
         }
         else if (substanceType == SubstanceType.Organic && thermProp == ThermPropType.EnthalpyOfFormation) {
            PersistProp(thermProp);
            UnpersistProp(thermProp);
         }
         else if (substanceType == SubstanceType.Inorganic) {
            PersistProp(thermProp);
            UnpersistProp(thermProp);
         }
      }

      public static string ExtractCasNo(string line, string[] separatorsStr) {
         string[] subStrs = line.Trim().Split(separatorsStr, StringSplitOptions.RemoveEmptyEntries);
         string casRegestryNo = "";
         if (!subStrs[0].Contains("&nbsp")) {
            casRegestryNo = subStrs[0].Trim();
         }

         return casRegestryNo;
      }

      public static string ExtractName(string line, string[] separatorsStr1, string[] separatorsStr2) {
         
         string name = "";

         string[] subStrs = line.Trim().Split(separatorsStr1, StringSplitOptions.RemoveEmptyEntries);
         subStrs = subStrs[0].Trim().Split(separatorsStr2, StringSplitOptions.RemoveEmptyEntries);
         foreach (string s in subStrs) {
            name += s;
         }

         return name;
      }

      private SubstanceFormula ExtractFormula(string aLine, string[] separatorsStr3, string[] separatorsStr4) {
         string tmpStr;
         string elementStr;
         int elementCount;
         substanceFormula = new SubstanceFormula();
         string[] subStrs = aLine.Trim().Split(separatorsStr3, StringSplitOptions.RemoveEmptyEntries);
         subStrs = subStrs[0].Trim().Split(separatorsStr4, StringSplitOptions.RemoveEmptyEntries);
         for (int i = 0; i < subStrs.Length; i++) {
            if (!char.IsDigit(subStrs[i][0])) {
               elementStr = subStrs[i];
               elementCount = 1;
               i++;
               if (i < subStrs.Length && char.IsDigit(subStrs[i][0])) {
                  tmpStr = subStrs[i];
                  elementCount = int.Parse(tmpStr);
               }

               substanceFormula.AddElement(elementStr, elementCount);
            }
         }
         return substanceFormula;
      }

      public static double ExtractValue(string lineTemp, string[] separatorsStr) {
         double a;
         string[] subStrs = lineTemp.Trim().Split(separatorsStr, StringSplitOptions.RemoveEmptyEntries);
         if (subStrs[0].Contains("&nbsp")) {
            a = -2147483D;
         }
         else {
            a = double.Parse(subStrs[0]);
         }
         return a;
      }

      private void tabPageFiles_MouseClick(object sender, MouseEventArgs e) {
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
            thermProp = ThermPropType.GasCp;
         }
         else if (idx == 1) {
            thermProp = ThermPropType.LiquidCp;
         }
         else if (idx == 2) {
            thermProp = ThermPropType.SolidCp;
         }
         else if (idx == 3) {
            thermProp = ThermPropType.EvapHeat;
         }
         else if (idx == 4) {
            thermProp = ThermPropType.VapPressure;
         }
         else if (idx == 5) {
            thermProp = ThermPropType.LiquidDensity;
         }
         else if (idx == 6) {
            thermProp = ThermPropType.GasVisc;
         }
         else if (idx == 7) {
            thermProp = ThermPropType.LiquidVisc;
         }
         else if (idx == 8) {
            thermProp = ThermPropType.GasK;
         }
         else if (idx == 9) {
            thermProp = ThermPropType.LiquidK;
         }
         else if (idx == 10) {
            thermProp = ThermPropType.SurfaceTension;
         }
         else if (idx == 11) {
            thermProp = ThermPropType.EnthalpyOfFormation;
         }
         else if (idx == 12) {
            thermProp = ThermPropType.CriticalProp;
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

      private void PersistSubstances() {
         FileStream fs = null;
         try {
            string fileName = "c:\\Temp\\Substances.dat";
            if (File.Exists(fileName)) {
               fs = new FileStream(fileName, FileMode.Open);
            }
            else {
               fs = new FileStream(fileName, FileMode.Create);
            }

            SoapFormatter serializer = new SoapFormatter();
            serializer.Serialize(fs, substanceList);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally {
            if (fs != null) {
               fs.Flush();
               fs.Close();
            }
            //Unpersist();
         }
      }

      public void UnpersistSubstances() {
         Stream stream = null;
         try {
            stream = new FileStream("c:\\temp\\Substances.dat", FileMode.Open);
            SoapFormatter serializer = new SoapFormatter();
            ArrayList substanceList = (ArrayList)serializer.Deserialize(stream);
            foreach (Storable s in substanceList) {
               s.SetObjectData();
            }
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally {
            stream.Close();
         }
      }

      private void PersistProp(ThermPropType prop) {
         FileStream fs = null;
         try {
            string fileName = "c:\\temp\\YawsGasCpCorrelations.dat";
            IList listToPersist = gasCpCorrList;
            if (prop == ThermPropType.LiquidCp) {
               fileName = "c:\\temp\\YawsLiquidCpCorrelations.dat";
               listToPersist = liquidCpCorrList;
            }
            if (prop == ThermPropType.SolidCp) {
               fileName = "c:\\temp\\YawsSolidCpCorrelations.dat";
               listToPersist = solidCpCorrList;
            }
            else if (prop == ThermPropType.EvapHeat) {
               fileName = "c:\\temp\\YawsEvaporationHeatCorrelations.dat";
               listToPersist = evapHeatCorrList;
            }
            else if (prop == ThermPropType.VapPressure) {
               fileName = "c:\\temp\\YawsVaporPressureCorrelations.dat";
               listToPersist = vapPressureCorrList;
            }
            else if (prop == ThermPropType.LiquidDensity) {
               fileName = "c:\\temp\\YawsLiquidDensityCorrelations.dat";
               listToPersist = liquidDensityCorrList;
            }
            else if (prop == ThermPropType.GasVisc) {
               fileName = "c:\\temp\\YawsGasViscosityCorrelations.dat";
               listToPersist = gasViscCorrList;
            }
            else if (prop == ThermPropType.LiquidVisc) {
               fileName = "c:\\temp\\YawsLiquidViscosityCorrelations.dat";
               listToPersist = liquidViscCorrList;
            }
            else if (prop == ThermPropType.GasK) {
               fileName = "c:\\temp\\YawsGasThermalConductivityCorrelations.dat";
               listToPersist = gasKCorrList;
            }
            else if (prop == ThermPropType.LiquidK) {
               fileName = "c:\\temp\\YawsLiquidThermalConductivityCorrelations.dat";
               listToPersist = liquidKCorrList;
            }
            else if (prop == ThermPropType.SurfaceTension) {
               fileName = "c:\\temp\\YawsSurfaceTensionCorrelations.dat";
               listToPersist = surfaceTensionCorrList;
            }
            else if (prop == ThermPropType.EnthalpyOfFormation) {
               fileName = "c:\\temp\\YawsEnthalpyOfFormationCorrelations.dat";
               listToPersist = enthalpyOfFormationCorrList;
            }

            if (File.Exists(fileName)) {
               fs = new FileStream(fileName, FileMode.Open);
            }
            else {
               fs = new FileStream(fileName, FileMode.Create);
            }

            SoapFormatter serializer = new SoapFormatter();
            serializer.Serialize(fs, listToPersist);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally {
            if (fs != null) {
               fs.Flush();
               fs.Close();
            }
         }
      }

      private void UnpersistProp(ThermPropType prop) {
         Stream stream = null;
         try {
            string fileName = "c:\\temp\\YawsGasCpCorrelations.dat";
            if (prop == ThermPropType.LiquidCp) {
               fileName = "c:\\temp\\YawsLiquidCpCorrelations.dat";
            }
            if (prop == ThermPropType.SolidCp) {
               fileName = "c:\\temp\\YawsSolidCpCorrelations.dat";
            }
            else if (prop == ThermPropType.EvapHeat) {
               fileName = "c:\\temp\\YawsEvaporationHeatCorrelations.dat";
            }
            else if (prop == ThermPropType.VapPressure) {
               fileName = "c:\\temp\\YawsVaporPressureCorrelations.dat";
            }
            else if (prop == ThermPropType.LiquidDensity) {
               fileName = "c:\\temp\\YawsLiquidDensityCorrelations.dat";
            }
            else if (prop == ThermPropType.GasVisc) {
               fileName = "c:\\temp\\YawsGasViscosityCorrelations.dat";
            }
            else if (prop == ThermPropType.LiquidVisc) {
               fileName = "c:\\temp\\YawsLiquidViscosityCorrelations.dat";
            }
            else if (prop == ThermPropType.GasK) {
               fileName = "c:\\temp\\YawsGasThermalConductivityCorrelations.dat";
            }
            else if (prop == ThermPropType.LiquidK) {
               fileName = "c:\\temp\\YawsLiquidThermalConductivityCorrelations.dat";
            }
            else if (prop == ThermPropType.SurfaceTension) {
               fileName = "c:\\temp\\YawsSurfaceTensionCorrelations.dat";
            }
            else if (prop == ThermPropType.EnthalpyOfFormation) {
               fileName = "c:\\temp\\YawsEnthalpyOfFormationCorrelations.dat";
            }

            stream = new FileStream(fileName, FileMode.Open);

            SoapFormatter serializer = new SoapFormatter();
            IList thermalPropCorrelationList = (IList)serializer.Deserialize(stream);

            foreach (Storable s in thermalPropCorrelationList) {
               s.SetObjectData();
            }
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            throw;
         }
         finally {
            stream.Close();
         }
      }
   }
}


//subStrs = line1.Split(separator1);
//sb = new StringBuilder();
//for (int i = 1; i < subStrs.Length - 1; i++) {
//   subSubStrs = subStrs[i].Split(separator2);
//   sb.Append(subSubStrs[0]);
//   tmpStr = subSubStrs[0];
//   if (char.IsDigit(tmpStr[0])) {
//      number = int.Parse(tmpStr);
//      substanceFormula.AddElement(tmpStrOld, number);
//   }
//   else {
//      if (!tmpStr.Equals(" ")) {
//         tmpStr = Parse(tmpStr.TrimEnd());
//      }
//      if (i == (subStrs.Length - 2) && !tmpStr.Equals(" ") && !tmpStr.Equals("")) {
//         substanceFormula.AddElement(tmpStr, 1);
//      }
//   }
//   tmpStrOld = tmpStr;
//}
//formula = sb.ToString();

//if (!line2.Contains("</TD>")) {
//   lineTemp = reader.ReadLine();
//   lineTemp = lineTemp.TrimStart();
//   if (!lineTemp.StartsWith("</TD>")) {
//      line2 = line2 + " " + lineTemp;
//   }
//   else {
//      line2 = line2 + lineTemp;
//   }
//}
//htmlTaggedName = line2;

//if (!(line2.Contains("<I>") || line2.Contains("</SPAN>"))) {
//   subStrs = line2.Split(separators);
//   name = subStrs[2];
//}
//else {
//   subStrs = line2.Split(separators);
//   sb = new StringBuilder();
//   for (int i = 2; i < subStrs.Length; i++) {
//      if (!(subStrs[i].Equals("") || subStrs[i].Equals("I") || subStrs[i].Equals("/I") ||
//         subStrs[i].Equals("SPAN class=Greek") || subStrs[i].Equals("/SPAN") ||
//         subStrs[i].Equals("/TD"))) {
//         sb.Append(subStrs[i]);
//      }
//   }
//   name = sb.ToString();
//}
//name = name.TrimEnd(emptySeparator);

