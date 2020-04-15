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

namespace Prosimo.ThermalPropExtractor {
   /// <summary>
   /// Summary description for MainForm.
   /// </summary>
   public class MainFormYawsOrganic : System.Windows.Forms.Form {
      private const string ENCODED_DIR = "extracted";

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private OpenFileDialog openFileDialog;
      private TabControl fileTab;
      private TabPage tabPageFiles;
      private Button buttonExtractData;
      private Button buttonRemoveFiles;
      private Button buttonRemoveAll;
      private Button buttonAddFiles;
      private ListBox listBoxFiles;
      private IContainer components;
      private ComboBox propComboBox;
      private ComboBox substanceTypeComboBox;

      private SubstanceType substanceType = SubstanceType.Organic;
      private ThermPropType thermProp;
      private SubstanceFormula substanceFormula;

      ArrayList substanceList;
      ArrayList gasCpCorrList;
      ArrayList liquidCpCorrList;
      ArrayList solidCpCorrList;
      ArrayList evapHeatCorrList;
      ArrayList vapPressureCorrList;
      ArrayList liquidDensityCorrList;
      ArrayList gasKCorrList;
      ArrayList liquidKCorrList;
      ArrayList gasViscCorrList;
      ArrayList liquidViscCorrList;
      ArrayList surfaceTensionCorrList;
      ArrayList enthalpyOfFormationCorrList;

      public MainFormYawsOrganic() {
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
         this.buttonRemoveAll = new System.Windows.Forms.Button();
         this.substanceTypeComboBox = new System.Windows.Forms.ComboBox();
         this.propComboBox = new System.Windows.Forms.ComboBox();
         this.buttonExtractData = new System.Windows.Forms.Button();
         this.buttonRemoveFiles = new System.Windows.Forms.Button();
         this.buttonAddFiles = new System.Windows.Forms.Button();
         this.listBoxFiles = new System.Windows.Forms.ListBox();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
         this.panel.Size = new System.Drawing.Size(787, 398);
         this.panel.TabIndex = 1;
         // 
         // fileTab
         // 
         this.fileTab.Controls.Add(this.tabPageFiles);
         this.fileTab.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fileTab.Location = new System.Drawing.Point(0, 0);
         this.fileTab.Name = "fileTab";
         this.fileTab.SelectedIndex = 0;
         this.fileTab.Size = new System.Drawing.Size(783, 394);
         this.fileTab.TabIndex = 5;
         // 
         // tabPageFiles
         // 
         this.tabPageFiles.Controls.Add(this.buttonRemoveAll);
         this.tabPageFiles.Controls.Add(this.substanceTypeComboBox);
         this.tabPageFiles.Controls.Add(this.propComboBox);
         this.tabPageFiles.Controls.Add(this.buttonExtractData);
         this.tabPageFiles.Controls.Add(this.buttonRemoveFiles);
         this.tabPageFiles.Controls.Add(this.buttonAddFiles);
         this.tabPageFiles.Controls.Add(this.listBoxFiles);
         this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
         this.tabPageFiles.Name = "tabPageFiles";
         this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageFiles.Size = new System.Drawing.Size(775, 368);
         this.tabPageFiles.TabIndex = 1;
         this.tabPageFiles.Text = "File Panel";
         this.tabPageFiles.UseVisualStyleBackColor = true;
         this.tabPageFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageFiles_MouseClick);
         // 
         // buttonRemoveAll
         // 
         this.buttonRemoveAll.Location = new System.Drawing.Point(134, 339);
         this.buttonRemoveAll.Name = "buttonRemoveAll";
         this.buttonRemoveAll.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveAll.TabIndex = 6;
         this.buttonRemoveAll.Text = "Remove All";
         this.buttonRemoveAll.UseVisualStyleBackColor = true;
         this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
         // 
         // substanceTypeComboBox
         // 
         this.substanceTypeComboBox.FormattingEnabled = true;
         this.substanceTypeComboBox.Items.AddRange(new object[] {
            "Inorganic",
            "Organic"});
         this.substanceTypeComboBox.Location = new System.Drawing.Point(340, 339);
         this.substanceTypeComboBox.Name = "substanceTypeComboBox";
         this.substanceTypeComboBox.Size = new System.Drawing.Size(121, 21);
         this.substanceTypeComboBox.TabIndex = 5;
         this.substanceTypeComboBox.Visible = false;
         this.substanceTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.substanceTypeComboBox_SelectedIndexChanged);
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
         this.propComboBox.Location = new System.Drawing.Point(482, 341);
         this.propComboBox.Name = "propComboBox";
         this.propComboBox.Size = new System.Drawing.Size(121, 21);
         this.propComboBox.TabIndex = 4;
         this.propComboBox.SelectedIndexChanged += new System.EventHandler(this.propComboBox_SelectedIndexChanged);
         // 
         // buttonExtractData
         // 
         this.buttonExtractData.AllowDrop = true;
         this.buttonExtractData.Location = new System.Drawing.Point(619, 339);
         this.buttonExtractData.Name = "buttonExtractData";
         this.buttonExtractData.Size = new System.Drawing.Size(98, 23);
         this.buttonExtractData.TabIndex = 3;
         this.buttonExtractData.Text = "Extract Data";
         this.buttonExtractData.UseVisualStyleBackColor = true;
         this.buttonExtractData.Click += new System.EventHandler(this.buttonExtractData_Click);
         // 
         // buttonRemoveFiles
         // 
         this.buttonRemoveFiles.Location = new System.Drawing.Point(237, 339);
         this.buttonRemoveFiles.Name = "buttonRemoveFiles";
         this.buttonRemoveFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveFiles.TabIndex = 2;
         this.buttonRemoveFiles.Text = "Remove";
         this.buttonRemoveFiles.UseVisualStyleBackColor = true;
         this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
         // 
         // buttonAddFiles
         // 
         this.buttonAddFiles.Location = new System.Drawing.Point(34, 339);
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
         // MainFormYawsOrganic
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(787, 398);
         this.Controls.Add(this.panel);
         this.Menu = this.mainMenu;
         this.Name = "MainFormYawsOrganic";
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
      //   Application.Run(new MainFormYawsOrganic());
      //}

      private void menuItemClose_Click(object sender, System.EventArgs e) {
         this.Close();
      }

      private void buttonAddFiles_Click(object sender, EventArgs e) {
         openFileDialog.Filter = "htm files (*.html)|*.html";
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

      private void buttonRemoveAll_Click(object sender, EventArgs e) {
         listBoxFiles.Items.Clear();
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
            //string[] separator1 = { "</td>" };
            //char[] emptySeparator = { ' ' };
            //char[] separators = { '>', '<' };
            string[] separatorsStr1 = { "</td>" };
            string[] separatorsStr2 = { "<i>", "</i>" };
            //string[] subStrs;
            //string formula;
            string name;
            string casRegestryNo;
            double a, b, c, d = 1, e = 1;
            double molarWeight, criticalRho, criticalT, criticalP, criticalV, criticalComp, accentricF;
            molarWeight = criticalRho = criticalT = criticalP = criticalV = criticalComp = accentricF = -2147483D;
            double minTemp = 1, maxTemp = 1;
            //StringBuilder sb;
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

            substanceList = new ArrayList();
            gasCpCorrList = new ArrayList();
            liquidCpCorrList = new ArrayList();
            solidCpCorrList = new ArrayList();
            evapHeatCorrList = new ArrayList();
            vapPressureCorrList = new ArrayList();
            liquidDensityCorrList = new ArrayList();
            gasKCorrList = new ArrayList();
            liquidKCorrList = new ArrayList();
            gasViscCorrList = new ArrayList();
            liquidViscCorrList = new ArrayList();
            surfaceTensionCorrList = new ArrayList();
            enthalpyOfFormationCorrList = new ArrayList();

            string line1, line2, lineTemp;
            //string elementStr;
            //string tmpStr;
            //int elementCount;
            int fileNo = 0;
            int substanceNo = 0;

            int counter;
            int totalCounter = 0;
            StreamReader reader;

            foreach (object fullFileName in listBoxFiles.Items) {
               fileNo++;
               counter = 0;
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               line1 = reader.ReadLine();
               while (!reader.EndOfStream && counter < 50) {
                  line2 = reader.ReadLine();
                  name = "";
                  if (thermProp != ThermPropType.CriticalProp) {
                     if (line1.Contains("<a border")) {
                        counter++;

                        name = MainFormYaws.ExtractName(line2, separatorsStr1, separatorsStr2);

                        lineTemp = reader.ReadLine();
                        lineTemp = reader.ReadLine();
                        lineTemp = reader.ReadLine();

                        lineTemp = reader.ReadLine();
                        a = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
                        if (a == -2147483D) {
                           continue;
                        }

                        lineTemp = reader.ReadLine();
                        b = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        c = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.LiquidCp ||
                           thermProp == ThermPropType.VapPressure || thermProp == ThermPropType.LiquidDensity ||
                           thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {

                           lineTemp = reader.ReadLine();
                           d = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
                        }

                        if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           lineTemp = reader.ReadLine();
                           e = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
                        }

                        lineTemp = reader.ReadLine();
                        minTemp = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        maxTemp = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        if (thermProp == ThermPropType.GasCp) {
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
                  else {
                     substanceNo = totalCounter + counter + 1;
                     if ((line1.StartsWith(substanceNo.ToString() + "</td><td  align=") || 
                        //since there is a duplicate for No. 3976 this condition must be added to handle it
                        line1.StartsWith((substanceNo-1).ToString() + "</td><td  align=")) && counter < 50) {
                        counter++;

                        name = MainFormYaws.ExtractName(line2, separatorsStr1, separatorsStr2);

                        lineTemp = reader.ReadLine();
                        //if (lineTemp.StartsWith("&nbsp")) {
                        //   casRegestryNo = "";
                        //}
                        //else {
                        //   subStrs = lineTemp.Trim().Split(separatorsStr1, StringSplitOptions.RemoveEmptyEntries);
                        //   casRegestryNo = subStrs[0].Trim();
                        //}
                        casRegestryNo = MainFormYaws.ExtractCasNo(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        substanceFormula = ExtractSubstanceFormula(lineTemp);

                        //for debugging use only
                        //formula = sb.ToString();

                        lineTemp = reader.ReadLine();
                        molarWeight = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        criticalT = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        criticalP = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        criticalV = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine(); //skip critical density since it can be obtained from critical volume and molar weight
                        //subStrs = lineTemp.Trim().Split(separators);
                        //criticalRho = double.Parse(subStrs[0]);

                        lineTemp = reader.ReadLine();
                        criticalComp = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        accentricF = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
                        
                        criticalProps = new CriticalPropsAndAcentricFactor(criticalT, criticalP, criticalV, criticalComp, accentricF);
                        substance = new Substance(name, substanceType, casRegestryNo, substanceFormula, molarWeight, criticalProps);
                        substanceList.Add(substance);
                     }

                     line1 = line2;
                  }
               }

               totalCounter = totalCounter + counter;

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

         if (thermProp == ThermPropType.CriticalProp) {
            PersistSubstances();
         }
         else {
            PersistProp(thermProp);
            UnpersistProp(thermProp);
         }
      }

      private SubstanceFormula ExtractSubstanceFormula(string lineTemp) {
         string elementStr;
         string tmpStr;
         int elementCount;
         string[] separatorsS = { "<sub>", "</sub>" };
         string[] separator1 = { "</td>" };

         string[] subStrs = lineTemp.Trim().Split(separatorsS, StringSplitOptions.RemoveEmptyEntries);
         SubstanceFormula substanceFormula = new SubstanceFormula();
         //sb = new StringBuilder();
         for (int i = 0; i < subStrs.Length - 1; i++) {
            //sb.Append(subStrs[i]);
            if (!char.IsDigit(subStrs[i][0])) {
               elementStr = subStrs[i];
               i++;
               if (char.IsDigit(subStrs[i][0])) {
                  //sb.Append(subStrs[i]);
                  tmpStr = subStrs[i];
                  if (char.IsDigit(subStrs[i + 1][0])) {
                     i++;
                     tmpStr += subStrs[i];
                     //sb.Append(subStrs[i]);
                  }

                  elementCount = int.Parse(tmpStr);
                  substanceFormula.AddElement(elementStr, elementCount);
               }
            }
         }

         if (!subStrs[subStrs.Length - 1].StartsWith("</td>")) {
            subStrs = subStrs[subStrs.Length - 1].Trim().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            elementStr = subStrs[0];
            substanceFormula.AddElement(elementStr, 1);
         }

         return substanceFormula;
      }


      //private static double ExtractValue(string lineTemp, char[] separators) {
      //   double a;
      //   if (lineTemp.StartsWith("&nbsp")) {
      //      a = -2147483D;
      //   }
      //   else {
      //      string[] subStrs = lineTemp.Trim().Split(separators);
      //      a = double.Parse(subStrs[0]);
      //   }
      //   return a;
      //}

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
