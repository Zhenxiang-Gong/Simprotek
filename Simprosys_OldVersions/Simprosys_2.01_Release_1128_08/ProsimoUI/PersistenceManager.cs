using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Text;

using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations;
using Prosimo.Materials;
using ProsimoUI.UnitOperationsUI.TwoStream;
using ProsimoUI.UnitOperationsUI;
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo.UnitSystems;
using ProsimoUI.CustomEditor;
using ProsimoUI.UnitOperationsUI.HeatExchangerUI;
using ProsimoUI.UnitOperationsUI.CycloneUI;
using ProsimoUI.UnitOperationsUI.DryerUI;

namespace ProsimoUI {
   
   /// <summary>
   /// Summary description for PersistenceManager.
   /// </summary>
   public class PersistenceManager {

      private enum FlowsheetVersionStatus {Ok, NotOk, Upgraded}

      internal static PersistenceManager Instance = new PersistenceManager();

      private PersistenceManager() {
      }

      //public Flowsheet UnpersistFlowsheet(NewProcessSettings newProcessSettings, ApplicationPreferences appPrefs, string fileName) {
      public Flowsheet UnpersistFlowsheet(string fileName, ApplicationPreferences appPrefs) {
         Flowsheet flowsheet = null;
         //Stream stream = null;
         try {
            //stream = new FileStream(fileName, FileMode.Open);
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //IFormatter serializer = GetPersistenceSerializer();
            //ArrayList persistedItems = (ArrayList)serializer.Deserialize(stream);
            ArrayList persistedItems = (ArrayList)UnpersistFile(fileName);
            string flowsheetName = Path.GetFileNameWithoutExtension(fileName);
            //flowsheet = this.SetFlowsheetContent(newProcessSettings, appPrefs, items, flowsheetName);
            flowsheet = this.SetFlowsheetContent(persistedItems, flowsheetName, appPrefs);
            if (flowsheet != null)
               flowsheet.Text = fileName;
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   stream.Close();
         //}
         return flowsheet;
      }

      //private Flowsheet SetFlowsheetContent(NewProcessSettings newProcessSettings, ApplicationPreferences appPrefs, ArrayList persistedItems, string flowsheetName) {
      private Flowsheet SetFlowsheetContent(ArrayList persistedItems, string flowsheetName, ApplicationPreferences appPrefs) {
         Flowsheet flowsheet = null;
         FlowsheetVersion flowsheetVersion = GetFlowSheetVersion(persistedItems);

         if (flowsheetVersion.Major > 1 || (flowsheetVersion.Major == 1 && flowsheetVersion.Minor > 0)) {
            foreach (object obj in persistedItems) {
               if (obj is Flowsheet) {
                  flowsheet = obj as Flowsheet;
                  flowsheet.ApplicationPrefs = appPrefs;
                  //flowsheet.NewProcessSettings = newProcessSettings;
                  flowsheet.SetObjectData();
               }
            }
         }
         else if (flowsheetVersion.Major == 1 && flowsheetVersion.Minor == 0) {
            //flowsheet = UnpersistFlowsheet1_01(newProcessSettings, appPrefs, persistedItems, flowsheetName);
            flowsheet = UnpersistFlowsheet1_01(persistedItems, flowsheetName, appPrefs);
         }

         FlowsheetVersionStatus flowsheetVersionStatus = CheckFlowsheetVersion(flowsheet);
         if (flowsheetVersionStatus == FlowsheetVersionStatus.Ok) {
            flowsheet.IsDirty = false;
         }
         else if (flowsheetVersionStatus == FlowsheetVersionStatus.Upgraded) {
            flowsheet.IsDirty = true;
         }
         else if (flowsheetVersionStatus == FlowsheetVersionStatus.NotOk) {
            flowsheet = null;
         }

         flowsheet.Version = new FlowsheetVersion();
         //if (flowsheetVersion != null) {
         //   flowsheet.Version = flowsheetVersion;
         //}

         return flowsheet;
      }

      //private static Flowsheet UnpersistFlowsheet1_01(NewProcessSettings newProcessSettings, ApplicationPreferences appPrefs, ArrayList persistedItems, string flowsheetName) {
      private static Flowsheet UnpersistFlowsheet1_01(ArrayList persistedItems, string flowsheetName, ApplicationPreferences appPrefs) {
         Flowsheet flowsheet = null;
         foreach (object obj in persistedItems) {
            if (obj is EvaporationAndDryingSystem) {
               EvaporationAndDryingSystem persisted = (EvaporationAndDryingSystem)obj;
               persisted.SetSystemFileName(flowsheetName); // call this before SetObjectData()
               persisted.SetObjectData();
               //flowsheet = new Flowsheet(newProcessSettings, appPrefs, persisted);
               flowsheet = new Flowsheet(persisted, appPrefs);
            }
            else if (obj is SolvableControl) {
               SolvableControl persistedCtrl = (SolvableControl)obj;

               persistedCtrl.Flowsheet = flowsheet;
               persistedCtrl.SetObjectData();

               //string solvableName = (string)persistedCtrl.NameControl.Text;
               //Solvable solvable = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName);
               //persistedCtrl.Solvable = solvable;
               flowsheet.Controls.Add(persistedCtrl);
            }
            else if (obj is SolvableConnection) {
               SolvableConnection persistedDc = (SolvableConnection)obj;
               persistedDc.Flowsheet = flowsheet;
               persistedDc.SetObjectData();
               persistedDc.UpdateConnection();
               flowsheet.ConnectionManager.AddConnection(persistedDc);
            }
            else if (obj is FlowsheetPreferences) {
               FlowsheetPreferences flowsheetPrefs = obj as FlowsheetPreferences;
               flowsheetPrefs.SetObjectData();
               flowsheet.BackColor = flowsheetPrefs.BackColor;
            }
            else if (obj is ProsimoUI.CustomEditor.CustomEditor) {
               ProsimoUI.CustomEditor.CustomEditor persistedEditor = (ProsimoUI.CustomEditor.CustomEditor)obj;
               persistedEditor.Flowsheet = flowsheet;
               persistedEditor.SetObjectData();
            }
         }
         return flowsheet;
      }

      private FlowsheetVersion GetFlowSheetVersion(ArrayList persistedItems) {
         FlowsheetVersion flowsheetVersion = null;
         foreach (object obj in persistedItems) {
            if (obj is FlowsheetVersion) {
               flowsheetVersion = obj as FlowsheetVersion;
               //flowsheetVersion.SetObjectData(flowsheetVersion.SerializationInfo, flowsheetVersion.StreamingContext);
               flowsheetVersion.SetObjectData();
            }
         }
         return flowsheetVersion;
      }

      private FlowsheetVersionStatus CheckFlowsheetVersion(Flowsheet flowsheet) {
         FlowsheetVersionStatus versionStatus = FlowsheetVersionStatus.Ok;

         string messageObsolete1 = "You are trying to open a newer version of the flowsheet with an older version of the software!";
         string messageObsolete2 = "Please use Version " + flowsheet.Version.Major.ToString() + "." + flowsheet.Version.Minor.ToString();
         string messageObsolete3 = " (Build " + flowsheet.Version.Build.ToString() + ") or higher of the software, to open this flowsheet.";
         StringBuilder sb = new StringBuilder();
         sb.Append(messageObsolete1);
         sb.Append("\r\n");
         sb.Append(messageObsolete2);
         sb.Append(messageObsolete3);
         string messageObsolete = sb.ToString();

         Version softwareVersion = ApplicationInformation.ProductVersion;
         if (flowsheet.Version.Major > softwareVersion.Major) {
            // goodbye
            versionStatus = FlowsheetVersionStatus.NotOk;
            MessageBox.Show(messageObsolete, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         else if (flowsheet.Version.Major < softwareVersion.Major) {
            // convert
            if (this.DoYouWantToConvertOlderFlowsheet()) {
               // DO THE CONVERSION
               flowsheet.Version.Major = softwareVersion.Major;
               flowsheet.Version.Minor = softwareVersion.Minor;
               flowsheet.Version.Build = softwareVersion.Build;
               versionStatus = FlowsheetVersionStatus.Upgraded;
            }
         }
         else if (flowsheet.Version.Major == softwareVersion.Major) {
            if (flowsheet.Version.Minor > softwareVersion.Minor) {
               // goodbye
               versionStatus = FlowsheetVersionStatus.NotOk;
               MessageBox.Show(messageObsolete, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (flowsheet.Version.Minor < softwareVersion.Minor) {
               // convert
               if (this.DoYouWantToConvertOlderFlowsheet()) {
                  // DO THE CONVERSION

                  flowsheet.Version.Minor = softwareVersion.Minor;
                  flowsheet.Version.Build = softwareVersion.Build;
                  versionStatus = FlowsheetVersionStatus.Upgraded;
               }
            }
            else if (flowsheet.Version.Minor == softwareVersion.Minor) {
               if (flowsheet.Version.Build > softwareVersion.Build) {
                  // goodbye
                  versionStatus = FlowsheetVersionStatus.NotOk;
                  MessageBox.Show(messageObsolete, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               }
               else if (flowsheet.Version.Build < softwareVersion.Build) {
                  // convert
                  if (this.DoYouWantToConvertOlderFlowsheet()) {
                     // DO THE CONVERSION

                     flowsheet.Version.Build = softwareVersion.Build;
                     versionStatus = FlowsheetVersionStatus.Upgraded;
                  }
               }
               else if (flowsheet.Version.Build == softwareVersion.Build) {
                  // ok
               }
            }
         }

         return versionStatus;
      }

      private bool DoYouWantToConvertOlderFlowsheet() {
         bool ok = false;
         string messageConvert1 = "You are trying to open an older version of the flowsheet with a newer version of the software!";
         string messageConvert2 = "To open this flowsheet we need to convert it to a newer version.";
         string messageConvert3 = "After the conversion, the flowsheet cannot be open with older versions of the software.";
         string messageConvert4 = "Do you want to do that?";
         StringBuilder sb = new StringBuilder();
         sb.Append(messageConvert1);
         sb.Append("\r\n");
         sb.Append(messageConvert2);
         sb.Append("\r\n");
         sb.Append(messageConvert3);
         sb.Append("\r\n");
         sb.Append(messageConvert4);
         string messageConvert = sb.ToString();
         DialogResult dr = MessageBox.Show(messageConvert, "Convert Old Flowsheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
         switch (dr) {
            case System.Windows.Forms.DialogResult.Yes:
               ok = true;
               break;
            case System.Windows.Forms.DialogResult.No:
               break;
         }
         return ok;
      }

      public void PersistFlowsheet(Flowsheet flowsheet, string fileName) {
         //FileStream fs = null;
         try {
            //if (File.Exists(fileName)) {
            //   fs = new FileStream(fileName, FileMode.Open);
            //}
            //else {
            //   fs = new FileStream(fileName, FileMode.Create);
            //}

            ArrayList toBeSerializeItems = this.GetFlowsheetContent(flowsheet);
            PersistFile(fileName, toBeSerializeItems);
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //IFormatter serializer = GetPersistenceSerializer();
            //serializer.Serialize(fs, toSerializeItems);

            // not dirty anymore
            flowsheet.MakeNondirtyAll();
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (fs != null) {
         //      fs.Flush();
         //      fs.Close();
         //   }
         //}
      }

      private ArrayList GetFlowsheetContent(Flowsheet flowsheet) {
         ArrayList toSerializeItems = new ArrayList();
         
         #region Persisted Contents in Verson 1.0 & 1.01--Commented Out
         //toSerializeItems.Add(flowsheet.EvaporationAndDryingSystem);

         //foreach (Object obj in flowsheet.Controls) {
         //   if (obj is SolvableControl) {
         //      toSerializeItems.Add((SolvableControl)obj);
         //   }
         //}

         //foreach (SolvableConnection dc in flowsheet.ConnectionManager.Connections) {
         //   toSerializeItems.Add(dc);
         //}

         //// get the flowsheet preferences
         //FlowsheetPreferences flowsheetPrefs = new FlowsheetPreferences(flowsheet);
         //toSerializeItems.Add(flowsheetPrefs);

         //// get the custom editor (we persist only the ID of the variables)
         //toSerializeItems.Add(flowsheet.CustomEditor);
         #endregion

         // get the flowsheet version
         toSerializeItems.Add(flowsheet.Version);
         toSerializeItems.Add(flowsheet);

         return toSerializeItems;
      }

      public void PersistUnitSystemCatalog(string fileName) {
         //FileStream fs = null;
         try {
            //if (File.Exists(fileName)) {
            //   File.Copy(fileName, bakFileName, true);
            //   File.Delete(fileName);
            //}

            //fs = new FileStream(fileName, FileMode.Create);

            ArrayList customUnitSystems = UnitSystemService.GetInstance().GetUnitSystemCatalog().GetCustomUnitList();
            PersistFile(fileName, customUnitSystems); 
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //IFormatter serializer = GetPersistenceSerializer();
            //serializer.Serialize(fs, customUnitSystems);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Error Saving Unit System Catalog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (fs != null) {
         //      fs.Flush();
         //      fs.Close();
         //   }
         //}
      }

      public bool UnpersistUnitSystemCatalog(string fileName) {
         bool ok = true;
         //Stream stream = null;
         try {
            if (!File.Exists(fileName)) {
               fileName = GetBackupFileName(fileName);
            }

            if (File.Exists(fileName)) {
            //if (File.Exists(fileName) || File.Exists(bakFileName)) {
               //if (File.Exists(fileName)) {
               //   stream = new FileStream(fileName, FileMode.Open);
               //}
               //else if (File.Exists(bakFileName)) {
               //   stream = new FileStream(bakFileName, FileMode.Open);
               //}
               //SoapFormatter serializer = new SoapFormatter();
               //BinaryFormatter serializer = new BinaryFormatter();
               //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;


               //IFormatter serializer = GetPersistenceSerializer();
               //ArrayList customUnitList = (ArrayList)serializer.Deserialize(stream);
               ArrayList customUnitList = (ArrayList)UnpersistFile(fileName);

               //IEnumerator e = customUnitSystems.GetEnumerator();
               //while (e.MoveNext()) {
               //   UnitSystem us = (UnitSystem)e.Current;
               //   us.SetObjectData(us.SerializationInfo, us.StreamingContext);
               
               UnitSystemCatalog unitSystemCatalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();

               foreach (UnitSystem us in customUnitList) {
                  us.SetObjectData();
                  unitSystemCatalog.Add(us);
               }
            }
         }
         catch (Exception e) {
            ok = false;
            string message = e.ToString();
            MessageBox.Show(message, "Error Retrieving Unit System Catalog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (stream != null)
         //      stream.Close();
         //}
         return ok;
      }

      public void PersistAppPreferences(MainForm mainForm) {
         //string fileName = mainForm.ExePathName + MainForm.APP_PREFS_XML;
         string fileName = ApplicationInformation.ApplicationStartupPath + MainForm.APP_PREFS_XML;
         //string fileName = mainForm.ExePathName + MainForm.APP_PREFS_DAT;
         //FileStream fs = null;
         try {
            //if (File.Exists(fileName)) {
            //   fs = new FileStream(fileName, FileMode.Open);
            //}
            //else {
            //   fs = new FileStream(fileName, FileMode.Create);
            //}

            //ArrayList toSerializeItems = new ArrayList();

            //UIPreferences uiPrefs = new UIPreferences(mainForm.Location, mainForm.Size, mainForm.WindowState, mainForm.ToolboxVisible, mainForm.ToolboxLocation);
            UIPreferences uiPrefs = new UIPreferences(mainForm);
            PersistFile(fileName, uiPrefs);
            //toSerializeItems.Add(uiPrefs);
            //toSerializeItems.Add(mainForm.NewProcessSettings);
            //toSerializeItems.Add(mainForm.ApplicationPrefs);
            
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //serializer.Serialize(fs, toSerializeItems);
            //IFormatter serializer = GetPersistenceSerializer();
            //serializer.Serialize(fs, uiPrefs);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Error Saving Application Preferences", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (fs != null) {
         //      fs.Flush();
         //      fs.Close();
         //   }
         //}
      }

      public void UnpersistAppPreferences(MainForm mainForm) {
         string fileName = ApplicationInformation.ApplicationStartupPath + MainForm.APP_PREFS_XML;
         //Stream stream = null;
         try {
            //stream = new FileStream(fileName, FileMode.Open);
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //ArrayList items = (ArrayList)serializer.Deserialize(stream); 
            //IFormatter serializer = GetPersistenceSerializer();
            //object persistedObj = serializer.Deserialize(stream);
            object persistedObj = UnpersistFile(fileName);

            if (persistedObj is UIPreferences) {
               ((UIPreferences)persistedObj).RestorePreferences(mainForm);
            }
            else {
               SetDefaultPrefs(mainForm);
            }
            //IEnumerator e = items.GetEnumerator();
            //while (e.MoveNext()) {
            //   object obj = e.Current;

            //   if (obj is UIPreferences) {
            //      UIPreferences uiPrefs = (UIPreferences)obj;
            //      uiPrefs.SetObjectData(uiPrefs.SerializationInfo, uiPrefs.StreamingContext);
            //      uiPrefs.SetObjectData();
            //      mainForm.Location = uiPrefs.MainFormLocation;
            //      mainForm.Size = uiPrefs.MainFormSize;
            //      mainForm.WindowState = uiPrefs.MainFormWindowState;
            //      mainForm.ToolboxVisible = uiPrefs.ToolboxVisible;
            //      mainForm.ToolboxLocation = uiPrefs.ToolboxLocation;
            //   }
            //   else if (obj is NewProcessSettings) {
            //      NewProcessSettings newProcessSettings = (NewProcessSettings)obj;
            //      //newProcessSettings.SetObjectData(newProcessSettings.SerializationInfo, newProcessSettings.StreamingContext);
            //      newProcessSettings.SetObjectData();
            //      if (!DryingGasCatalog.GetInstance().IsInCatalog(newProcessSettings.DryingGasName)) {
            //         newProcessSettings.DryingGasName = DryingGasCatalog.GetInstance().GetDefaultDryingGas().Name;
            //      }
            //      if (!DryingMaterialCatalog.GetInstance().IsInCatalog(newProcessSettings.DryingMaterialName)) {
            //         newProcessSettings.DryingMaterialName = DryingMaterialCatalog.GetInstance().GetDefaultDryingMaterial().Name;
            //      }

            //      // 
            //      if (newProcessSettings.DryingGasName == null || newProcessSettings.DryingGasName.Trim().Equals("")) {
            //         newProcessSettings.DryingGasName = "Air";
            //      }
            //      mainForm.NewProcessSettings = newProcessSettings;
            //   }
            //   else if (obj is ApplicationPreferences) {
            //      ApplicationPreferences appPrefs = (ApplicationPreferences)obj;
            //      //appPrefs.SetObjectData(appPrefs.SerializationInfo, appPrefs.StreamingContext);
            //      appPrefs.SetObjectData();
            //      appPrefs.InitializeCurrentUnitSystem();
            //      mainForm.ApplicationPrefs = appPrefs;
            //   }
            //}
         }
         catch (Exception) {
            SetDefaultPrefs(mainForm);
         }
         //finally {
         //   if (stream != null)
         //      stream.Close();
         //}
      }

      private static void SetDefaultPrefs(MainForm mainForm) {
         FlowsheetSettings newProcessSettings = new FlowsheetSettings();
         mainForm.NewProcessSettings = newProcessSettings;
         ApplicationPreferences appPrefs = new ApplicationPreferences();
         mainForm.ApplicationPrefs = appPrefs;
      }

      public void PersistMaterialCatalog(string fileName) {
         //FileStream fs = null;
         try {
            //if (File.Exists(fileName)) {
            //   File.Copy(fileName, bakFileName, true);
            //   File.Delete(fileName);
            //}
            //fs = new FileStream(fileName, FileMode.Create);

            IList customMaterials = DryingMaterialCatalog.Instance.GetDryingMaterialList(true);
            PersistFile(fileName, customMaterials);
            //SoapFormatter serializer = new SoapFormatter();
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //IFormatter serializer = GetPersistenceSerializer();
            //serializer.Serialize(fs, customMaterials);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Error Saving Material Catalog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (fs != null) {
         //      fs.Flush();
         //      fs.Close();
         //   }
         //}
      }

      public bool UnpersistMaterialCatalog(string fileName) {
         bool ok = true;
         //Stream stream = null;
         try {
            if (!File.Exists(fileName)) {
               fileName = GetBackupFileName(fileName);
            }
            if (File.Exists(fileName)) {
               //if (File.Exists(fileName)) {
               //   stream = new FileStream(fileName, FileMode.Open);
               //}
               //else if (File.Exists(bakFileName)) {
               //   stream = new FileStream(bakFileName, FileMode.Open);
               //}
               //SoapFormatter serializer = new SoapFormatter();
               //BinaryFormatter serializer = new BinaryFormatter();
               //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
               //IFormatter serializer = GetPersistenceSerializer();
               //IList customMaterials = (IList)serializer.Deserialize(stream);
               IList customMaterials = (IList)UnpersistFile(fileName);

               foreach (DryingMaterial dm in customMaterials) {
                  dm.SetObjectData();
                  DryingMaterialCatalog.Instance.AddDryingMaterial(dm);
               }
            }
         }
         catch (Exception e) {
            ok = false;
            string message = e.ToString();
            MessageBox.Show(message, "Error Retrieving Material Catalog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         //finally {
         //   if (stream != null)
         //      stream.Close();
         //}
         return ok;
      }

      private static IFormatter GetPersistenceSerializer() {
         SoapFormatter serializer = new SoapFormatter();
         //BinaryFormatter serializer = new BinaryFormatter();
         serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;

         return serializer;
      }

      private static void PersistFile(string fileName, object graph) {
         FileStream fileStream = null;
         try {
            if (File.Exists(fileName)) {
               File.Copy(fileName, GetBackupFileName(fileName), true);
               File.Delete(fileName);
            }

            fileStream = new FileStream(fileName, FileMode.Create);
            IFormatter serializer = GetPersistenceSerializer();
            serializer.Serialize(fileStream, graph);
         }
         catch (Exception) {
            throw;
         }
         finally {
            if (fileStream != null) {
               fileStream.Flush();
               fileStream.Close();
            }
         }
      }

      private static object UnpersistFile(string fileName) {
         Stream stream = null;
         try {
            stream = new FileStream(fileName, FileMode.Open);
            IFormatter serializer = GetPersistenceSerializer();
            return serializer.Deserialize(stream);
         }
         catch (Exception) {
            throw;
         }
         finally {
            if (stream != null) {
               stream.Close();
            }
         }
      }

      private static string GetBackupFileName(string fileName) {
         FileInfo fileInfo = new FileInfo(fileName);
         string[] separators = { fileInfo.Extension };
         string[] strs = fileName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
         return strs[0] + ".bak";
      }
   }
}

//else if (obj is GasStreamControl) {
//   GasStreamControl persistedCtrl = (GasStreamControl)obj;

//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   DryingGasStream stream = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as DryingGasStream;
//   GasStreamControl newCtrl = new GasStreamControl(flowsheet, new Point(0, 0), stream);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is MaterialStreamControl) {
//   MaterialStreamControl persistedCtrl = (MaterialStreamControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   DryingMaterialStream stream = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as DryingMaterialStream;
//   MaterialStreamControl newCtrl = new MaterialStreamControl(flowsheet, new Point(0, 0), stream);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is DryerControl) {
//   DryerControl persistedCtrl = (DryerControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Dryer uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Dryer;
//   DryerControl newCtrl = new DryerControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is HeatExchangerControl) {
//   HeatExchangerControl persistedCtrl = (HeatExchangerControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   HeatExchanger uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as HeatExchanger;
//   HeatExchangerControl newCtrl = new HeatExchangerControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is CycloneControl) {
//   CycloneControl persistedCtrl = (CycloneControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Cyclone uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Cyclone;
//   CycloneControl newCtrl = new CycloneControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is EjectorControl) {
//   EjectorControl persistedCtrl = (EjectorControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Ejector uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Ejector;
//   EjectorControl newCtrl = new EjectorControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is WetScrubberControl) {
//   WetScrubberControl persistedCtrl = (WetScrubberControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   WetScrubber uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as WetScrubber;
//   WetScrubberControl newCtrl = new WetScrubberControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is ScrubberCondenserControl) {
//   ScrubberCondenserControl persistedCtrl = (ScrubberCondenserControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   ScrubberCondenser uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as ScrubberCondenser;
//   ScrubberCondenserControl newCtrl = new ScrubberCondenserControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is MixerControl) {
//   MixerControl persistedCtrl = (MixerControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Mixer uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Mixer;
//   MixerControl newCtrl = new MixerControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is TeeControl) {
//   TeeControl persistedCtrl = (TeeControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Tee uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Tee;
//   TeeControl newCtrl = new TeeControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is FlashTankControl) {
//   FlashTankControl persistedCtrl = (FlashTankControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   FlashTank uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as FlashTank;
//   FlashTankControl newCtrl = new FlashTankControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is FanControl) {
//   FanControl persistedCtrl = (FanControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Fan uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Fan;
//   FanControl newCtrl = new FanControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is ValveControl) {
//   ValveControl persistedCtrl = (ValveControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Valve uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Valve;
//   ValveControl newCtrl = new ValveControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is BagFilterControl) {
//   BagFilterControl persistedCtrl = (BagFilterControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   BagFilter uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as BagFilter;
//   BagFilterControl newCtrl = new BagFilterControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is AirFilterControl) {
//   AirFilterControl persistedCtrl = (AirFilterControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   AirFilter uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as AirFilter;
//   AirFilterControl newCtrl = new AirFilterControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is CompressorControl) {
//   CompressorControl persistedCtrl = (CompressorControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Compressor uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Compressor;
//   CompressorControl newCtrl = new CompressorControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is HeaterControl) {
//   HeaterControl persistedCtrl = (HeaterControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Heater uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Heater;
//   HeaterControl newCtrl = new HeaterControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is CoolerControl) {
//   CoolerControl persistedCtrl = (CoolerControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Cooler uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Cooler;
//   CoolerControl newCtrl = new CoolerControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is ElectrostaticPrecipitatorControl) {
//   ElectrostaticPrecipitatorControl persistedCtrl = (ElectrostaticPrecipitatorControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   ElectrostaticPrecipitator uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as ElectrostaticPrecipitator;
//   ElectrostaticPrecipitatorControl newCtrl = new ElectrostaticPrecipitatorControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is PumpControl) {
//   PumpControl persistedCtrl = (PumpControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Pump uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Pump;
//   PumpControl newCtrl = new PumpControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is RecycleControl) {
//   RecycleControl persistedCtrl = (RecycleControl)obj;
//   string solvableName = (string)persistedCtrl.SerializationInfo.GetValue("SolvableName", typeof(string));
//   Recycle uo = flowsheet.EvaporationAndDryingSystem.GetSolvable(solvableName) as Recycle;
//   RecycleControl newCtrl = new RecycleControl(flowsheet, new Point(0, 0), uo);
//   newCtrl.SetObjectData(persistedCtrl.SerializationInfo, persistedCtrl.StreamingContext);
//   flowsheet.Controls.Add(newCtrl);
//}
//else if (obj is SolvableConnection) {
//   SolvableConnection persistedDc = (SolvableConnection)obj;
//   SolvableConnection dc = new SolvableConnection(flowsheet);
//   dc.SetObjectData(persistedDc.SerializationInfo, persistedDc.StreamingContext);
//   dc.UpdateConnection();
//   flowsheet.ConnectionManager.AddConnection(dc);
//}
//else if (obj is FlowsheetPreferences) {
//   FlowsheetPreferences flowsheetPrefs = obj as FlowsheetPreferences;
//   flowsheetPrefs.SetObjectData(flowsheetPrefs.SerializationInfo, flowsheetPrefs.StreamingContext);
//   flowsheet.BackColor = flowsheetPrefs.BackColor;
//}
//else if (obj is ProsimoUI.CustomEditor.CustomEditor) {
//   ProsimoUI.CustomEditor.CustomEditor persistedEditor = (ProsimoUI.CustomEditor.CustomEditor)obj;
//   flowsheet.CustomEditor.SetObjectData(persistedEditor.SerializationInfo, persistedEditor.StreamingContext); 
//}

//// get all the flowsheet element controls
//IEnumerator e = flowsheet.Controls.GetEnumerator();
//while (e.MoveNext()) 
//{
//   object obj = e.Current;
//   if (obj is GasStreamControl)
//   {
//      GasStreamControl ctrl = (GasStreamControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is MaterialStreamControl)
//   {
//      MaterialStreamControl ctrl = (MaterialStreamControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is DryerControl)
//   {
//      DryerControl ctrl = (DryerControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is HeatExchangerControl)
//   {
//      HeatExchangerControl ctrl = (HeatExchangerControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is CycloneControl)
//   {
//      CycloneControl ctrl = (CycloneControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is EjectorControl)
//   {
//      EjectorControl ctrl = (EjectorControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is WetScrubberControl)
//   {
//      WetScrubberControl ctrl = (WetScrubberControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is ScrubberCondenserControl)
//   {
//      ScrubberCondenserControl ctrl = (ScrubberCondenserControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is MixerControl)
//   {
//      MixerControl ctrl = (MixerControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is TeeControl)
//   {
//      TeeControl ctrl = (TeeControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is FlashTankControl)
//   {
//      FlashTankControl ctrl = (FlashTankControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is FanControl)
//   {
//      FanControl ctrl = (FanControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is ValveControl)
//   {
//      ValveControl ctrl = (ValveControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is BagFilterControl)
//   {
//      BagFilterControl ctrl = (BagFilterControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is AirFilterControl)
//   {
//      AirFilterControl ctrl = (AirFilterControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is CompressorControl)
//   {
//      CompressorControl ctrl = (CompressorControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is HeaterControl)
//   {
//      HeaterControl ctrl = (HeaterControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is CoolerControl)
//   {
//      CoolerControl ctrl = (CoolerControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is ElectrostaticPrecipitatorControl)
//   {
//      ElectrostaticPrecipitatorControl ctrl = (ElectrostaticPrecipitatorControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is PumpControl)
//   {
//      PumpControl ctrl = (PumpControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//   else if (obj is RecycleControl)
//   {
//      RecycleControl ctrl = (RecycleControl)obj;
//      toSerializeItems.Add(ctrl);
//   }
//}
// get all the connections
//e = flowsheet.ConnectionManager.Connections.GetEnumerator();
//while (e.MoveNext()) 
//{
//   SolvableConnection dc = (SolvableConnection)e.Current;
//   toSerializeItems.Add(dc);
//}
