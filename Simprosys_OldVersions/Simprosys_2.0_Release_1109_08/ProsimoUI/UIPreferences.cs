using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for UIPreferences.
   /// </summary>
   [Serializable]
   public class UIPreferences : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private Point mainFormLocation;
      private Size mainFormSize;
      private FormWindowState mainFormWindowState;

      private bool toolboxVisible;
      
      //Revison 1 
      //private Point toolboxLocation;
      private FlowsheetSettings newProcessSettings;
      private ApplicationPreferences appPrefs;
      private FlowsheetPreferences flowsheetPrefs;

      //public UIPreferences(Point mainFormLocation, Size mainFormSize, FormWindowState mainFormWindowState, bool toolboxVisible, Point toolboxLocation) {
      public UIPreferences(MainForm mainForm) {
         this.mainFormLocation = mainForm.Location;
         this.mainFormSize = mainForm.Size;
         this.mainFormWindowState = mainForm.WindowState;
         this.toolboxVisible = mainForm.ToolboxVisible;
         //this.toolboxLocation = toolboxLocation;
         this.newProcessSettings = mainForm.NewProcessSettings;
         this.appPrefs = mainForm.ApplicationPrefs;
         this.flowsheetPrefs = mainForm.FlowsheetPrefs;
      }

      public void RestorePreferences(MainForm mainForm) {
         SetObjectData();
         mainForm.Location = mainFormLocation;
         mainForm.Size = mainFormSize;
         mainForm.WindowState = mainFormWindowState;
         mainForm.ToolboxVisible = toolboxVisible;
         mainForm.NewProcessSettings = newProcessSettings;
         mainForm.ApplicationPrefs = appPrefs;
         appPrefs.InitializeCurrentUnitSystem();
         mainForm.FlowsheetPrefs = flowsheetPrefs;
      }

      protected UIPreferences(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context) {
      public override void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionUIPreferences");
         this.mainFormLocation = (Point)info.GetValue("MainFormLocation", typeof(Point));
         this.mainFormSize = (Size)info.GetValue("MainFormSize", typeof(Size));
         this.mainFormWindowState = (FormWindowState)info.GetValue("MainFormWindowState", typeof(FormWindowState));
         this.toolboxVisible = info.GetBoolean("ToolboxVisible");
         //this.ToolboxLocation = (Point)info.GetValue("ToolboxLocation", typeof(Point));
         this.appPrefs = (ApplicationPreferences)RecallStorableObject("ApplicationPreferences", typeof(ApplicationPreferences));
         this.newProcessSettings = (FlowsheetSettings)RecallStorableObject("NewProcessSettings", typeof(FlowsheetSettings));
         this.flowsheetPrefs = (FlowsheetPreferences)RecallStorableObject("FlowsheetPreferences", typeof(FlowsheetPreferences));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionUIPreferences", UIPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         
         //Originaly persisted data
         info.AddValue("MainFormLocation", this.mainFormLocation, typeof(Point));
         info.AddValue("MainFormSize", this.mainFormSize, typeof(Size));
         info.AddValue("MainFormWindowState", this.mainFormWindowState, typeof(FormWindowState));
         info.AddValue("ToolboxVisible", this.toolboxVisible, typeof(bool));

         //Fisrt revision
         //info.AddValue("ToolboxLocation", this.ToolboxLocation, typeof(Point));
         info.AddValue("ApplicationPreferences", this.appPrefs, typeof(ApplicationPreferences));
         info.AddValue("NewProcessSettings", this.newProcessSettings, typeof(FlowsheetSettings));
         info.AddValue("FlowsheetPreferences", this.flowsheetPrefs, typeof(FlowsheetPreferences));
      }
   }
}

//public Point MainFormLocation {
//   get { return mainFormLocation; }
//   set { mainFormLocation = value; }
//}

//public Size MainFormSize {
//   get { return mainFormSize; }
//   set { mainFormSize = value; }
//}

//public FormWindowState MainFormWindowState {
//   get { return mainFormWindowState; }
//   set { mainFormWindowState = value; }
//}

//public bool ToolboxVisible {
//   get { return toolboxVisible; }
//   set { toolboxVisible = value; }
//}

////private Point toolboxLocation;
////Version 2 additions--start
//private Point globalEditorFormLocation;
//public Point GlobalEditorFormLocation {
//   get { return globalEditorFormLocation; }
//   set { globalEditorFormLocation = value; }
//}

//private Size globalEditorFormSize;
//public Size GlobalEditorFormSize {
//   get { return globalEditorFormSize; }
//   set { globalEditorFormSize = value; }
//}

//private Point globalEditorSplitterLocation;
//public Point GlobalEditorSplitterLocation {
//   get { return globalEditorSplitterLocation; }
//   set { globalEditorSplitterLocation = value; }
//}

//private Size globalEditorSplitterSize;
//public Size GlobalEditorSplitterSize {
//   get { return globalEditorSplitterSize; }
//   set { globalEditorSplitterSize = value; }
//}

//private Point customEditorFormLocation;
//public Point CustomEditorFormLocation {
//   get { return customEditorFormLocation; }
//   set { customEditorFormLocation = value; }
//}

//private Size customEditorFormSize;
//public Size CustomEditorFormSize {
//   get { return customEditorFormSize; }
//   set { customEditorFormSize = value; }
//}
////Version 2 additions--end

//if (persistedClassVersion >= 2) {
//   this.globalEditorFormLocation = (Point)info.GetValue("GlobalEditorFormLocation", typeof(Point));
//   this.globalEditorFormSize = (Size)info.GetValue("GlobalEditorFormSize", typeof(Size));
//   this.globalEditorSplitterLocation = (Point)info.GetValue("GlobalEditorSplitterLocation", typeof(Point));
//   this.globalEditorSplitterSize = (Size)info.GetValue("GlobalEditorSplitterSize", typeof(Size));
//   this.customEditorFormLocation = (Point)info.GetValue("CustomEditorFormLocation", typeof(Point));
//   this.customEditorFormSize = (Size)info.GetValue("CustomEditorFormSize", typeof(Size));
//}

//info.AddValue("GlobalEditorFormLocation", this.globalEditorFormLocation, typeof(Point));
//info.AddValue("GlobalEditorFormSize", this.globalEditorFormSize, typeof(Size));
//info.AddValue("GlobalEditorSplitterLocation", this.globalEditorSplitterLocation, typeof(Point));
//info.AddValue("GlobalEditorSplitterSize", this.globalEditorSplitterSize, typeof(Size));
//info.AddValue("CustomEditorFormLocation", this.customEditorFormLocation, typeof(Point));
//info.AddValue("CustomEditorFormSize", this.customEditorFormSize, typeof(Size));

//this.globalEditorFormLocation = globalEditorFormLocation;
//this.globalEditorFormSize = globalEditorFormSize;
//this.globalEditorSplitterLocation = globalEditorSplitterLocation;
//this.globalEditorSplitterSize = globalEditorSplitterSize;
//this.customEditorFormLocation = customEditorFormLocation;
//this.customEditorFormSize = customEditorFormSize;

//private StreamingContext streamingContext;
//public StreamingContext StreamingContext
//{
//   get {return streamingContext;}
//   set {streamingContext = value;}
//}

//private SerializationInfo serializationInfo;
//public SerializationInfo SerializationInfo
//{
//   get {return serializationInfo;}
//   set {serializationInfo = value;}
//}

//private Point toolboxLocation;
//public Point ToolboxLocation
//{
//   get {return toolboxLocation;}
//   set {toolboxLocation = value;}
//}


