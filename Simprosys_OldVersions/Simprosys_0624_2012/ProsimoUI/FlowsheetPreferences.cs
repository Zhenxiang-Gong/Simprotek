using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Drawing;

using Prosimo;
using ProsimoUI.GlobalEditor;
using ProsimoUI.CustomEditor;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for FlowsheetPreferences.
   /// </summary>
   [Serializable]
   //public class FlowsheetPreferences : ISerializable
   public class FlowsheetPreferences : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 2;

      private Color backColor;
      public Color BackColor {
         get { return backColor; }
         set { backColor = value; }
      }

      //version 2 addition
      private bool hasGlobalEditorPrefs;
      internal bool HasGlobalEditorPrefs {
         get { return hasGlobalEditorPrefs; }
      }
      private Point globalEditorFormLocation;
      private Size globalEditorFormSize;
      private int globalEditorSplitterPosition;

      private bool hasCustomEditorPrefs;
      internal bool HasCustomEditorPrefs {
         get { return hasCustomEditorPrefs; }
         //get { return flowsheet.Editor != null; }
      }
      private Point customEditorFormLocation;
      private Size customEditorFormSize;
      //version 2 addition

      public FlowsheetPreferences(Flowsheet flowsheet) {
         this.backColor = flowsheet.BackColor;

         SetFlowsheetPrefs(flowsheet);
      }

      internal void SetFlowsheetPrefs(Flowsheet flowsheet) {
         if (flowsheet.Editor != null) {
            hasGlobalEditorPrefs = true;
            this.globalEditorFormLocation = flowsheet.Editor.Location;
            this.globalEditorFormSize = flowsheet.Editor.Size;
            this.globalEditorSplitterPosition = flowsheet.Editor.SplitterPosition;
         }

         if (flowsheet.CustomEditorForm != null) {
            this.hasCustomEditorPrefs = true;
            this.customEditorFormLocation = flowsheet.CustomEditorForm.Location;
            this.customEditorFormSize = flowsheet.CustomEditorForm.Size;
         }
      }

      internal void RestoreFlowsheetPrefs(Flowsheet flowsheet) {
         flowsheet.BackColor = this.backColor;
         if (flowsheet.Editor != null) {
            RestoreGlobalEditorPrefs(flowsheet.Editor);
         }
         if (flowsheet.CustomEditorForm != null) {
            RestoreCustomEditorPrefs(flowsheet.CustomEditorForm);
         }
      }
      
      internal void RestoreGlobalEditorPrefs(SystemEditor globalEditor) {
         if (hasGlobalEditorPrefs) {
            globalEditor.Location = globalEditorFormLocation;
            globalEditor.Size = globalEditorFormSize;
            globalEditor.SplitterPosition = globalEditorSplitterPosition;
         }
      }

      internal void RestoreCustomEditorPrefs(CustomEditorForm customEditor) {
         if (hasCustomEditorPrefs) {
            customEditor.Location = customEditorFormLocation;
            customEditor.Size = customEditorFormSize;
         }
      }

      protected FlowsheetPreferences(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      public override void SetObjectData() {
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionFlowsheetPreferences");
         this.BackColor = (Color)info.GetValue("BackColor", typeof(Color));
         if (persistedClassVersion >= 2) {
            this.hasGlobalEditorPrefs = info.GetBoolean("HasGlobalEditorPrefs");
            if (hasGlobalEditorPrefs) {
               this.globalEditorFormLocation = (Point)info.GetValue("GlobalEditorFormLocation", typeof(Point));
               this.globalEditorFormSize = (Size)info.GetValue("GlobalEditorFormSize", typeof(Size));
               this.globalEditorSplitterPosition = info.GetInt32("GlobalEditorSplitterPosition");
            }

            this.hasCustomEditorPrefs = info.GetBoolean("HasCustomEditorPrefs");
            if (hasCustomEditorPrefs) {
               this.customEditorFormLocation = (Point)info.GetValue("CustomEditorFormLocation", typeof(Point));
               this.customEditorFormSize = (Size)info.GetValue("CustomEditorFormSize", typeof(Size));
            }
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionFlowsheetPreferences", FlowsheetPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("BackColor", this.BackColor, typeof(Color));

         //version 2 addition
         info.AddValue("HasGlobalEditorPrefs", this.hasGlobalEditorPrefs, typeof(bool));
         if (this.hasGlobalEditorPrefs) {
            info.AddValue("GlobalEditorFormLocation", this.globalEditorFormLocation, typeof(Point));
            info.AddValue("GlobalEditorFormSize", this.globalEditorFormSize, typeof(Size));
            info.AddValue("GlobalEditorSplitterPosition", this.globalEditorSplitterPosition, typeof(int));
         }

         info.AddValue("HasCustomEditorPrefs", this.hasCustomEditorPrefs, typeof(bool));
         if (this.hasCustomEditorPrefs) {
            info.AddValue("CustomEditorFormLocation", this.customEditorFormLocation, typeof(Point));
            info.AddValue("CustomEditorFormSize", this.customEditorFormSize, typeof(Size));
         }
      }
   }
}

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

