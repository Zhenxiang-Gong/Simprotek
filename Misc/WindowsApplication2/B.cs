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
	/// Summary description for B.
	/// </summary>
   [Serializable]
   public class B : ISerializable
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

      private int id;
      public int Id
      {
         get {return id;}
         set {id = value;}
      }

      public B(int id)
      {
         this.id = id;
      }

      protected B(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassVersionB", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.Id = (int)info.GetValue("Id", typeof(int));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionB", B.CLASS_VERSION, typeof(int));
         info.AddValue("Id", this.Id, typeof(int));
      }
   }
}
