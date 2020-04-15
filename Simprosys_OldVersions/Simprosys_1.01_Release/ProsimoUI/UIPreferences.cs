using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for UIPreferences.
	/// </summary>
   [Serializable]
   public class UIPreferences : ISerializable
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      private Point mainFormLocation;
      public Point MainFormLocation
      {
         get {return mainFormLocation;}
         set {mainFormLocation = value;}
      }

      private Size mainFormSize;
      public Size MainFormSize
      {
         get {return mainFormSize;}
         set {mainFormSize = value;}
      }

      private FormWindowState mainFormWindowState;
      public FormWindowState MainFormWindowState
      {
         get {return mainFormWindowState;}
         set {mainFormWindowState = value;}
      }

      private bool toolboxVisible;
      public bool ToolboxVisible
      {
         get {return toolboxVisible;}
         set {toolboxVisible = value;}
      }

      private Point toolboxLocation;
      public Point ToolboxLocation
      {
         get {return toolboxLocation;}
         set {toolboxLocation = value;}
      }

      public UIPreferences(Point mainFormLocation, Size mainFormSize, FormWindowState mainFormWindowState, bool toolboxVisible, Point toolboxLocation)
		{
         this.mainFormLocation = mainFormLocation;
         this.mainFormSize = mainFormSize;
         this.mainFormWindowState = mainFormWindowState;
         this.toolboxVisible = toolboxVisible;
         this.toolboxLocation = toolboxLocation;
		}

      protected UIPreferences(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionUIPreferences", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MainFormLocation = (Point)info.GetValue("MainFormLocation", typeof(Point));
               this.MainFormSize = (Size)info.GetValue("MainFormSize", typeof(Size));
               this.MainFormWindowState = (FormWindowState)info.GetValue("MainFormWindowState", typeof(FormWindowState));
               this.ToolboxVisible = (bool)info.GetValue("ToolboxVisible", typeof(bool));
               this.ToolboxLocation = (Point)info.GetValue("ToolboxLocation", typeof(Point));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionUIPreferences", UIPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("MainFormLocation", this.MainFormLocation, typeof(Point));
         info.AddValue("MainFormSize", this.MainFormSize, typeof(Size));
         info.AddValue("MainFormWindowState", this.MainFormWindowState, typeof(FormWindowState));
         info.AddValue("ToolboxVisible", this.ToolboxVisible, typeof(bool));
         info.AddValue("ToolboxLocation", this.ToolboxLocation, typeof(Point));
      }
	}
}
