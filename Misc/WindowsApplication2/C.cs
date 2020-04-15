using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WindowsApplication2
{
	/// <summary>
	/// Summary description for C.
	/// </summary>
   [Serializable]
   public class C : ISerializable
	{
      private const int CLASS_VERSION = 1; 

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

      private string name;
      public string Name
      {
         get {return name;}
         set {name = value;}
      }

      public C(string name)
      {
         this.name = name;
      }

      protected C(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassVersionC", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.Name = (string)info.GetValue("Name", typeof(string));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionC", C.CLASS_VERSION, typeof(int));
         info.AddValue("Name", this.Name, typeof(string));
      }
   }
}
