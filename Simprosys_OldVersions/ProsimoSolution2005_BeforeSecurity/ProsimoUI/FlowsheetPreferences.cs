using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Drawing;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for FlowsheetPreferences.
	/// </summary>
   [Serializable]
   public class FlowsheetPreferences : ISerializable
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

      private Color backColor;
      public Color BackColor
      {
         get { return backColor; }
         set { backColor = value; }
      }

      public FlowsheetPreferences(Flowsheet flowsheet)
		{
         this.backColor = flowsheet.BackColor;
		}

      protected FlowsheetPreferences(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlowsheetPreferences", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.BackColor = (Color)info.GetValue("BackColor", typeof(Color));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionFlowsheetPreferences", FlowsheetPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("BackColor", this.BackColor, typeof(Color));
      }
	}
}
