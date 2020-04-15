using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;


namespace Prosimo.ThermalPropExtractor {

   public enum ThermPropType { CriticalProp, GasCp, LiquidCp, SolidCp, EvapHeat, VapPressure, LiquidDensity, GasVisc, LiquidVisc, GasK, LiquidK, SurfaceTension, EnthalpyOfFormation, EnthalpyOfCombustion };
   public enum ExtractorType { Perrys, Yaws, YawsOrganic };
   
   public partial class MainFormThermalPropExtractor : Form {
      private SubstanceType substanceType = SubstanceType.Organic;
      private ThermPropType thermPropType = ThermPropType.VapPressure;
      private ExtractorType extractorType = ExtractorType.Perrys;

      private string fileFilter = "htm files (*.html)|*.html";

      public MainFormThermalPropExtractor() {
         InitializeComponent();
      }

      //<summary>
      //The main entry point for the application.
      //</summary>
      [STAThread]
      static void Main() {
         Application.Run(new MainFormThermalPropExtractor());
      }

      private void buttonAddFiles_Click(object sender, EventArgs e) {
         openFileDialog.Filter = fileFilter;
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

      private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Close();
      }

      private void tabPageFiles_MouseClick(object sender, MouseEventArgs e) {
         listBoxFiles.ClearSelected();
      }

      private void comboBoxSubstanceType_SelectedIndexChanged(object sender, EventArgs e) {
         int idx = comboBoxSubstanceType.SelectedIndex;
         substanceType = (SubstanceType)idx;
      }

      private void comboBoxProp_SelectedIndexChanged(object sender, EventArgs e) {
         int idx = comboBoxProp.SelectedIndex;
         thermPropType = (ThermPropType)idx;
      }

      private void comboBoxExtractorType_SelectedIndexChanged(object sender, EventArgs e) {
         int idx = comboBoxExtractorType.SelectedIndex;
         extractorType = (ExtractorType)idx;
         if (extractorType == ExtractorType.Yaws) {
            fileFilter = "htm files (*.htm)|*.htm";
         }
         else {
            fileFilter = "htm files (*.html)|*.html";
         }

         if (extractorType == ExtractorType.Perrys) {
            comboBoxSubstanceType.Visible = false;
         }
         else if (extractorType == ExtractorType.YawsOrganic) {
            comboBoxSubstanceType.Visible = false;
            substanceType = SubstanceType.Organic;
            thermPropType = ThermPropType.CriticalProp;
         }
         else {
            comboBoxSubstanceType.Visible = true;
            thermPropType = ThermPropType.CriticalProp;
         }
      }

      private void ExtractCoeffs() {
         ThermalPropextractor extractor = null;
         if (extractorType == ExtractorType.Perrys) {
            extractor = new ThermalPropExtrractorPerrys();
         }
         else if (extractorType == ExtractorType.Yaws) {
            extractor = new ThermalPropExtrractorYaws();
         }
         else if (extractorType == ExtractorType.YawsOrganic) {
            extractor = new ThermalPropExtrractorYawsOrganic();
         }

         if (extractor != null) {
            extractor.ExtractCoeffs(listBoxFiles, substanceType, thermPropType);
         }
      }
   }
}

//public ExtractorType ExtractorType {
//   get { return extractorType; }
//   set { extractorType = value; }
//}

//public ThermPropType ThermPropType {
//   get { return thermPropType; }
//   set { thermPropType = value; }
//}

//public SubstanceType SubstanceType {
//   get { return substanceType; }
//   set { substanceType = value; }
//}

//if (idx == 0) {
//   thermProp = ThermPropType.GasCp;
//}
//else if (idx == 1) {
//   thermProp = ThermPropType.LiquidCp;
//}
//else if (idx == 2) {
//   thermProp = ThermPropType.SolidCp;
//}
//else if (idx == 3) {
//   thermProp = ThermPropType.EvapHeat;
//}
//else if (idx == 4) {
//   thermProp = ThermPropType.VapPressure;
//}
//else if (idx == 5) {
//   thermProp = ThermPropType.LiquidDensity;
//}
//else if (idx == 6) {
//   thermProp = ThermPropType.GasVisc;
//}
//else if (idx == 7) {
//   thermProp = ThermPropType.LiquidVisc;
//}
//else if (idx == 8) {
//   thermProp = ThermPropType.GasK;
//}
//else if (idx == 9) {
//   thermProp = ThermPropType.LiquidK;
//}
//else if (idx == 10) {
//   thermProp = ThermPropType.SurfaceTension;
//}
//else if (idx == 11) {
//   thermProp = ThermPropType.EnthalpyOfFormation;
//}
//else if (idx == 12) {
//   thermProp = ThermPropType.CriticalProp;
//}

