using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for FlowsheetVersion.
	/// </summary>
   [Serializable]
   public class FlowsheetVersion : ISerializable
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

      private int major;
      public int Major
      {
         get {return major;}
         set {major = value;}
      }

      private int minor;
      public int Minor
      {
         get {return minor;}
         set {minor = value;}
      }

      private int build;
      public int Build
      {
         get {return build;}
         set {build = value;}
      }

      public FlowsheetVersion()
		{
         Version v = new Version(ApplicationInformation.VERSION);
         this.major = v.Major;
         this.minor = v.Minor;
         this.build = v.Build;
		}

      protected FlowsheetVersion(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlowsheetVersion", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.Major = (int)info.GetValue("Major", typeof(int));
               this.Minor = (int)info.GetValue("Minor", typeof(int));
               this.Build = (int)info.GetValue("Build", typeof(int));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionFlowsheetVersion", FlowsheetVersion.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Major", this.Major, typeof(int));
         info.AddValue("Minor", this.Minor, typeof(int));
         info.AddValue("Build", this.Build, typeof(int));
      }
	}
}
