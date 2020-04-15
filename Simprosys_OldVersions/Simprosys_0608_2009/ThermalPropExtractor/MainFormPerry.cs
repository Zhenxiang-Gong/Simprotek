using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.PerrysCorrelations;

namespace Prosimo.ThermalPropExtractor
{
   //public enum ThermPropPerry { GasCp, LiquidCp, EvapHeat, VapPressure, LiquidDensity};
   ///// <summary>
   ///// Summary description for MainForm.
   ///// </summary>
   //public class MainFormPerry : System.Windows.Forms.Form
   //{
   //   private const string ENCODED_DIR = "extracted";

   //   private System.Windows.Forms.Panel panel;
   //   private System.Windows.Forms.MainMenu mainMenu;
   //   private System.Windows.Forms.MenuItem menuItemClose;
   //   private OpenFileDialog openFileDialog;
   //   private TabControl fileTab;
   //   private TabPage tabPageFiles;
   //   private Button buttonExtractData;
   //   private Button buttonRemoveFiles;
   //   private Button buttonAddFiles;
   //   private ListBox listBoxFiles;
   //   private IContainer components;
   //   private ComboBox propComboBox;
   //   private ComboBox substanceTypeComboBox;

   //   //private SubstanceType substanceType;
   //   private ThermPropPerry thermProp;

   //   //ArrayList substanceList = new ArrayList();
   //   ArrayList gasCpCorrList = new ArrayList();
   //   ArrayList liquidCpCorrList = new ArrayList();
   //   ArrayList evapHeatCorrList = new ArrayList();
   //   ArrayList vapPressureCorrList = new ArrayList();
   //   ArrayList liquidDensityCorrList = new ArrayList();

   //   public MainFormPerry()
   //   {
   //      //
   //      // Required for Windows Form Designer support
   //      //
   //      InitializeComponent();
   //   }

   //   /// <summary>
   //   /// Clean up any resources being used.
   //   /// </summary>
   //   protected override void Dispose( bool disposing )
   //   {
   //      if( disposing )
   //      {
   //         if (components != null) 
   //         {
   //            components.Dispose();
   //         }
   //      }
   //      base.Dispose( disposing );
   //   }

   //   #region Windows Form Designer generated code
   //   /// <summary>
   //   /// Required method for Designer support - do not modify
   //   /// the contents of this method with the code editor.
   //   /// </summary>
   //   private void InitializeComponent()
   //   {
   //      this.components = new System.ComponentModel.Container();
   //      this.panel = new System.Windows.Forms.Panel();
   //      this.fileTab = new System.Windows.Forms.TabControl();
   //      this.tabPageFiles = new System.Windows.Forms.TabPage();
   //      this.substanceTypeComboBox = new System.Windows.Forms.ComboBox();
   //      this.propComboBox = new System.Windows.Forms.ComboBox();
   //      this.buttonExtractData = new System.Windows.Forms.Button();
   //      this.buttonRemoveFiles = new System.Windows.Forms.Button();
   //      this.buttonAddFiles = new System.Windows.Forms.Button();
   //      this.listBoxFiles = new System.Windows.Forms.ListBox();
   //      this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
   //      this.menuItemClose = new System.Windows.Forms.MenuItem();
   //      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
   //      this.panel.SuspendLayout();
   //      this.fileTab.SuspendLayout();
   //      this.tabPageFiles.SuspendLayout();
   //      this.SuspendLayout();
   //      // 
   //      // panel
   //      // 
   //      this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
   //      this.panel.Controls.Add(this.fileTab);
   //      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
   //      this.panel.Location = new System.Drawing.Point(0, 0);
   //      this.panel.Name = "panel";
   //      this.panel.Size = new System.Drawing.Size(579, 398);
   //      this.panel.TabIndex = 1;
   //      // 
   //      // fileTab
   //      // 
   //      this.fileTab.Controls.Add(this.tabPageFiles);
   //      this.fileTab.Dock = System.Windows.Forms.DockStyle.Fill;
   //      this.fileTab.Location = new System.Drawing.Point(0, 0);
   //      this.fileTab.Name = "fileTab";
   //      this.fileTab.SelectedIndex = 0;
   //      this.fileTab.Size = new System.Drawing.Size(575, 394);
   //      this.fileTab.TabIndex = 5;
   //      // 
   //      // tabPageFiles
   //      // 
   //      this.tabPageFiles.Controls.Add(this.substanceTypeComboBox);
   //      this.tabPageFiles.Controls.Add(this.propComboBox);
   //      this.tabPageFiles.Controls.Add(this.buttonExtractData);
   //      this.tabPageFiles.Controls.Add(this.buttonRemoveFiles);
   //      this.tabPageFiles.Controls.Add(this.buttonAddFiles);
   //      this.tabPageFiles.Controls.Add(this.listBoxFiles);
   //      this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
   //      this.tabPageFiles.Name = "tabPageFiles";
   //      this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
   //      this.tabPageFiles.Size = new System.Drawing.Size(567, 368);
   //      this.tabPageFiles.TabIndex = 1;
   //      this.tabPageFiles.Text = "File Panel";
   //      this.tabPageFiles.UseVisualStyleBackColor = true;
   //      this.tabPageFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageFiles_MouseClick);
   //      // 
   //      // substanceTypeComboBox
   //      // 
   //      this.substanceTypeComboBox.FormattingEnabled = true;
   //      this.substanceTypeComboBox.Items.AddRange(new object[] {
   //         "Inorganic",
   //         "Organic"});
   //      this.substanceTypeComboBox.Location = new System.Drawing.Point(187, 339);
   //      this.substanceTypeComboBox.Name = "substanceTypeComboBox";
   //      this.substanceTypeComboBox.Size = new System.Drawing.Size(121, 21);
   //      this.substanceTypeComboBox.TabIndex = 5;
   //      this.substanceTypeComboBox.Visible = false;
   //      this.substanceTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.substanceTypeComboBox_SelectedIndexChanged);
   //      // 
   //      // propComboBox
   //      // 
   //      this.propComboBox.FormattingEnabled = true;
   //      this.propComboBox.Items.AddRange(new object[] {
   //         "GasCp",
   //         "LiquidCp",
   //         "EvapHeat",
   //         "VapPressure",
   //         "LiquidDensity"});
   //      this.propComboBox.Location = new System.Drawing.Point(329, 341);
   //      this.propComboBox.Name = "propComboBox";
   //      this.propComboBox.Size = new System.Drawing.Size(121, 21);
   //      this.propComboBox.TabIndex = 4;
   //      this.propComboBox.SelectedIndexChanged += new System.EventHandler(this.propComboBox_SelectedIndexChanged);
   //      // 
   //      // buttonExtractData
   //      // 
   //      this.buttonExtractData.AllowDrop = true;
   //      this.buttonExtractData.Location = new System.Drawing.Point(466, 339);
   //      this.buttonExtractData.Name = "buttonExtractData";
   //      this.buttonExtractData.Size = new System.Drawing.Size(98, 23);
   //      this.buttonExtractData.TabIndex = 3;
   //      this.buttonExtractData.Text = "Extract Data";
   //      this.buttonExtractData.UseVisualStyleBackColor = true;
   //      this.buttonExtractData.Click += new System.EventHandler(this.buttonExtractData_Click);
   //      // 
   //      // buttonRemoveFiles
   //      // 
   //      this.buttonRemoveFiles.Location = new System.Drawing.Point(84, 339);
   //      this.buttonRemoveFiles.Name = "buttonRemoveFiles";
   //      this.buttonRemoveFiles.Size = new System.Drawing.Size(75, 23);
   //      this.buttonRemoveFiles.TabIndex = 2;
   //      this.buttonRemoveFiles.Text = "Remove";
   //      this.buttonRemoveFiles.UseVisualStyleBackColor = true;
   //      this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
   //      // 
   //      // buttonAddFiles
   //      // 
   //      this.buttonAddFiles.Location = new System.Drawing.Point(3, 339);
   //      this.buttonAddFiles.Name = "buttonAddFiles";
   //      this.buttonAddFiles.Size = new System.Drawing.Size(75, 23);
   //      this.buttonAddFiles.TabIndex = 1;
   //      this.buttonAddFiles.Text = "Add";
   //      this.buttonAddFiles.UseVisualStyleBackColor = true;
   //      this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
   //      // 
   //      // listBoxFiles
   //      // 
   //      this.listBoxFiles.FormattingEnabled = true;
   //      this.listBoxFiles.HorizontalScrollbar = true;
   //      this.listBoxFiles.Location = new System.Drawing.Point(6, 6);
   //      this.listBoxFiles.Name = "listBoxFiles";
   //      this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
   //      this.listBoxFiles.Size = new System.Drawing.Size(555, 316);
   //      this.listBoxFiles.TabIndex = 0;
   //      // 
   //      // mainMenu
   //      // 
   //      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
   //         this.menuItemClose});
   //      // 
   //      // menuItemClose
   //      // 
   //      this.menuItemClose.Index = 0;
   //      this.menuItemClose.Text = "Close";
   //      this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
   //      // 
   //      // openFileDialog
   //      // 
   //      this.openFileDialog.Multiselect = true;
   //      // 
   //      // MainForm
   //      // 
   //      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
   //      this.ClientSize = new System.Drawing.Size(579, 398);
   //      this.Controls.Add(this.panel);
   //      this.Menu = this.mainMenu;
   //      this.Name = "MainForm";
   //      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
   //      this.Text = "Data Extraction Manager";
   //      this.panel.ResumeLayout(false);
   //      this.fileTab.ResumeLayout(false);
   //      this.tabPageFiles.ResumeLayout(false);
   //      this.ResumeLayout(false);

   //   }
   //   #endregion

   //    //<summary>
   //    //The main entry point for the application.
   //    //</summary>
   //   //[STAThread]
   //   //static void Main() {
   //   //   Application.Run(new MainFormPerry());
   //   //}

   //   private void menuItemClose_Click(object sender, System.EventArgs e)
   //   {
   //      this.Close();
   //   }

   //   private void buttonAddFiles_Click(object sender, EventArgs e)
   //   {
   //      openFileDialog.Filter = "html files (*.html)|*.html";
   //      openFileDialog.FilterIndex = 2;

   //      if (openFileDialog.ShowDialog() == DialogResult.OK)
   //      {
   //         listBoxFiles.BeginUpdate();
   //         for (int i = 0; i < openFileDialog.FileNames.Length; i++)
   //         {
   //            string fileName = openFileDialog.FileNames[i];
   //            if (!listBoxFiles.Items.Contains(fileName))
   //               listBoxFiles.Items.Add(openFileDialog.FileNames[i]);
   //         }
   //         listBoxFiles.EndUpdate();
   //      }

   //   }

   //   private void buttonRemoveFiles_Click(object sender, EventArgs e)
   //   {
   //      ArrayList list = new ArrayList();
   //      foreach (object fileName in listBoxFiles.SelectedItems)
   //      {
   //         list.Add(fileName);
   //      }

   //      foreach (object fileName in list)
   //      {
   //         listBoxFiles.Items.Remove(fileName);
   //      }
   //   }

   //   private void buttonExtractData_Click(object sender, EventArgs evtArgs)
   //   {
   //      listBoxFiles.ClearSelected();
   //      this.Cursor = Cursors.WaitCursor;

   //      ExtractCoeffs();
   //      this.Cursor = Cursors.Default;
   //   }

   //   private void ExtractCoeffs() {
   //      FileStream fsRead = null;
   //      try {
   //         //char[] separators = { '>', '<' };
   //         string[] separatorsStr1 = { "</td>" };
   //         string[] separatorsStr2 = { "<i>", "</i>" };
   //         //string[] subStrs;
   //         string name;
   //         double a, b, c, d = 1, e = 1;
   //         double tc = 1;
   //         double minTemp = 1, maxTemp = 1;

   //         PerrysGasCpCorrelation gasCpCorrelation;
   //         PerrysLiquidCpCorrelation liquidCpCorrelation;
   //         PerrysLiquidDensityCorrelation liquidDensityCorrelation;
   //         PerrysEvaporationHeatCorrelation evapHeatCorrelation;
   //         PerrysVaporPressureCorrelation vapPressureCorrelation;

   //         string line1, line2, lineTemp;
   //         int correlationType = 1;
   //         int counter;
   //         StreamReader reader;

   //         foreach (object fullFileName in listBoxFiles.Items) {
   //            counter = 0;
   //            fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
   //            reader = new StreamReader(fsRead);
   //            line1 = reader.ReadLine();
   //            while (!reader.EndOfStream && counter < 50) {
   //               //name = "";
   //               line2 = reader.ReadLine();
   //               if (line1.Contains("&nbsp") || line1.Contains("<a border")) {
   //                  counter++;
   //                  //subStrs = line2.Trim().Split(separatorsStr1, StringSplitOptions.RemoveEmptyEntries);

   //                  //subStrs = subStrs[0].Trim().Split(separatorsStr2, StringSplitOptions.RemoveEmptyEntries);
   //                  //foreach (string s in subStrs) {
   //                  //   name += s;
   //                  //}
   //                  //if (subStrs[1] == "i") {
   //                  //   name = subStrs[2] + subStrs[4];
   //                  //}
   //                  //else {
   //                  //   name = subStrs[0];
   //                  //}
   //                  name = MainFormYaws.ExtractName(line2, separatorsStr1, separatorsStr2);

   //                  lineTemp = reader.ReadLine();
   //                  lineTemp = reader.ReadLine();
                     
   //                  if (thermProp != ThermPropPerry.VapPressure) {
   //                     lineTemp = reader.ReadLine();
   //                  }

   //                  lineTemp = reader.ReadLine();
   //                  a = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  lineTemp = reader.ReadLine();
   //                  b = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  lineTemp = reader.ReadLine();
   //                  c = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  lineTemp = reader.ReadLine();
   //                  d = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  if (thermProp == ThermPropPerry.GasCp || thermProp == ThermPropPerry.LiquidCp
   //                     || thermProp == ThermPropPerry.VapPressure) {
   //                     lineTemp = reader.ReadLine();
   //                     e = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
   //                  }

   //                  lineTemp = reader.ReadLine();
   //                  minTemp = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  lineTemp = reader.ReadLine();

   //                  lineTemp = reader.ReadLine();
   //                  maxTemp = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);

   //                  if (thermProp == ThermPropPerry.LiquidCp || thermProp == ThermPropPerry.EvapHeat) {
   //                     lineTemp = reader.ReadLine();
                        
   //                     lineTemp = reader.ReadLine();
   //                     tc = MainFormYaws.ExtractValue(lineTemp, separatorsStr1);
   //                  }

   //                  if (thermProp == ThermPropPerry.GasCp) {
   //                     correlationType = 1;
   //                     if (name == "helium-4" || name == "nitric oxide") {
   //                        correlationType = 2;
   //                     }
   //                     else if (name == "propylbenzene") {
   //                        correlationType = 3;
   //                     }
   //                     gasCpCorrelation = new PerrysGasCpCorrelation(name, a * 1e5, b * 1e5, c * 1e3, d * 1e5, e, minTemp, maxTemp, correlationType);
   //                     gasCpCorrList.Add(gasCpCorrelation);
   //                  }
   //                  else if (thermProp == ThermPropPerry.LiquidCp) {
   //                     correlationType = 1;
   //                     if (name == "methane" || name == "ethane" || name == "propane" || name == "n-butane"
   //                         || name == "n-heptane" || name == "hydrogen" || name == "ammonia" || name == "carbon monoxide"
   //                         || name == "hydrogen sulfide") {
   //                        correlationType = 2;
   //                     }
   //                     liquidCpCorrelation = new PerrysLiquidCpCorrelation(name, a, b, c, d, e, tc, minTemp, maxTemp, correlationType);
   //                     liquidCpCorrList.Add(liquidCpCorrelation);
   //                  }
   //                  else if (thermProp == ThermPropPerry.EvapHeat) {
   //                     evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(name, a * 1.0e7, b, c, d, tc, minTemp, maxTemp);
   //                     evapHeatCorrList.Add(evapHeatCorrelation);
   //                  }
   //                  else if (thermProp == ThermPropPerry.VapPressure) {
   //                     vapPressureCorrelation = new PerrysVaporPressureCorrelation(name, a, b, c, d, e, minTemp, maxTemp);
   //                     vapPressureCorrList.Add(vapPressureCorrelation);
   //                  }
   //                  else if (thermProp == ThermPropPerry.LiquidDensity) {
   //                     liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(name, a, b, c, d, minTemp, maxTemp);
   //                     liquidDensityCorrList.Add(liquidDensityCorrelation);
   //                  }
   //               }

   //               line1 = line2;
   //            }
   //            reader.Close();
   //            fsRead.Close();
   //         }
   //      }
   //      catch (Exception ex) {
   //         Console.WriteLine("The process failed: {0}", ex.ToString());
   //      }
   //      finally {
   //         if (fsRead != null) {
   //            fsRead.Close();
   //         }
   //      }

   //      PersistProp(thermProp);
   //      UnpersistProp(thermProp);
   //   }

   //   private void tabPageFiles_MouseClick(object sender, MouseEventArgs e)
   //   {
   //      listBoxFiles.ClearSelected();
   //   }

   //   private void substanceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
   //      //int idx = substanceTypeComboBox.SelectedIndex;
   //      //if (idx == 0) {
   //      //   substanceType = SubstanceType.Inorganic;
   //      //}
   //      //else if (idx == 1) {
   //      //   substanceType = SubstanceType.Organic;
   //      //}
   //   }

   //   private void propComboBox_SelectedIndexChanged(object sender, EventArgs e) {
   //      int idx = propComboBox.SelectedIndex;
   //      if (idx == 0) {
   //         thermProp = ThermPropPerry.GasCp;
   //      }
   //      else if (idx == 1) {
   //         thermProp = ThermPropPerry.LiquidCp;
   //      }
   //      else if (idx == 2) {
   //         thermProp = ThermPropPerry.EvapHeat;
   //      }
   //      else if (idx == 3) {
   //         thermProp = ThermPropPerry.VapPressure;
   //      }
   //      else if (idx == 4) {
   //         thermProp = ThermPropPerry.LiquidDensity;
   //      }
   //   }

   //   private void PersistProp(ThermPropPerry prop) {
   //      Stream fs = null;
   //      try {
   //         string fileName = "c:\\temp\\PerrysGasCpCorrelations.dat";
   //         IList listToPersist = gasCpCorrList;
   //         if (prop == ThermPropPerry.LiquidCp) {
   //            fileName = "c:\\temp\\PerrysLiquidCpCorrelations.dat";
   //            listToPersist = liquidCpCorrList;
   //         }
   //         else if (prop == ThermPropPerry.EvapHeat) {
   //            fileName = "c:\\temp\\PerrysEvaporationHeatCorrelations.dat";
   //            listToPersist = evapHeatCorrList;
   //         }
   //         else if (prop == ThermPropPerry.VapPressure) {
   //            fileName = "c:\\temp\\PerrysVaporPressureCorrelations.dat";
   //            listToPersist = vapPressureCorrList;
   //         }
   //         else if (prop == ThermPropPerry.LiquidDensity) {
   //            fileName = "c:\\temp\\PerrysLiquidDensityCorrelations.dat";
   //            listToPersist = liquidDensityCorrList;
   //         }

   //         if (File.Exists(fileName)) {
   //            File.Delete(fileName);
   //         }

   //         fs = File.Create(fileName);
   //         //XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8);

   //         //IList<Type> knownTypes = new List<Type>();
   //         //knownTypes.Add(typeof(PerrysGasCpCorrelation));
   //         //DataContractSerializer serializer = new DataContractSerializer(typeof(ArrayList), knownTypes);
   //         //writer.WriteStartDocument();
   //         //serializer.WriteObject(fs, listToPersist);
   //         //writer.WriteEndDocument();
   //         //BinaryFormatter serializer = new BinaryFormatter();
   //         SoapFormatter serializer = new SoapFormatter();
   //         serializer.Serialize(fs, listToPersist);
   //      }
   //      catch (Exception e) {
   //         string message = e.ToString();
   //         MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
   //      }
   //      finally {
   //         if (fs != null) {
   //            fs.Flush();
   //            fs.Close();
   //         }
   //      }
   //   }

   //   private void UnpersistProp(ThermPropPerry prop) {
   //      Stream stream = null;
   //      try {
   //         string fileName = "c:\\temp\\PerrysGasCpCorrelations.dat";
   //         if (prop == ThermPropPerry.LiquidCp) {
   //            fileName = "c:\\temp\\PerrysLiquidCpCorrelations.dat";
   //         }
   //         else if (prop == ThermPropPerry.EvapHeat) {
   //            fileName = "c:\\temp\\PerrysEvaporationHeatCorrelations.dat";
   //         }
   //         else if (prop == ThermPropPerry.VapPressure) {
   //            fileName = "c:\\temp\\PerrysVaporPressureCorrelations.dat";
   //         }
   //         else if (prop == ThermPropPerry.LiquidDensity) {
   //            fileName = "c:\\temp\\PerrysLiquidDensityCorrelations.dat";
   //         }

   //         stream = new FileStream(fileName, FileMode.Open);

   //         //IList<Type> knownTypes = new List<Type>();
   //         //knownTypes.Add(typeof(PerrysGasCpCorrelation));
   //         //DataContractSerializer serializer = new DataContractSerializer(typeof(ArrayList), knownTypes);
   //         //IList thermalPropCorrelationList = (IList)serializer.ReadObject(stream);
   //         //BinaryFormatter serializer = new BinaryFormatter();
   //         SoapFormatter serializer = new SoapFormatter();
   //         IList thermalPropCorrelationList = (IList)serializer.Deserialize(stream);

   //         foreach (Storable s in thermalPropCorrelationList) {
   //            s.SetObjectData();
   //         }
   //      }
   //      catch (Exception e) {
   //         string message = e.ToString();
   //         MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
   //         throw;
   //      }
   //      finally {
   //         stream.Close();
   //      }
   //   }
   //}
}
