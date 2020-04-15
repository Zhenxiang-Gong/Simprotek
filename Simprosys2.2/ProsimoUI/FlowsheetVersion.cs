using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;

using Prosimo;

namespace ProsimoUI {
   /// <summary>
   /// Summary description for FlowsheetVersion.
   /// </summary>
   [Serializable]
   //public class FlowsheetVersion : ISerializable
   public class FlowsheetVersion : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      //private StreamingContext streamingContext;
      public StreamingContext StreamingContext {
         get { return context; }
         set { context = value; }
      }

      //private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo {
         get { return info; }
         set { info = value; }
      }

      private int major;
      public int Major {
         get { return major; }
         set { major = value; }
      }

      private int minor;
      public int Minor {
         get { return minor; }
         set { minor = value; }
      }

      private int build;
      public int Build {
         get { return build; }
         set { build = value; }
      }

      private int revision;
      public int Revision {
         get { return revision; }
         set { revision = value; }
      }

      public FlowsheetVersion() {
         Version v = ApplicationInformation.ProductVersion;
         this.major = v.Major;
         this.minor = v.Minor;
         this.build = v.Build;
         this.revision = v.Revision;
      }

      protected FlowsheetVersion(SerializationInfo info, StreamingContext context)
         : base(info, context) {
         //this.SerializationInfo = info;
         //this.StreamingContext = context;
         //// don't restore anything here!
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      public override void SetObjectData() {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlowsheetVersion", typeof(int));
         this.major = info.GetInt32("Major");
         this.minor = info.GetInt32("Minor");
         this.build = info.GetInt32("Build");
         if (persistedClassVersion > 1) {
            this.revision = info.GetInt32("Revision");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionFlowsheetVersion", FlowsheetVersion.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Major", this.major, typeof(int));
         info.AddValue("Minor", this.minor, typeof(int));
         info.AddValue("Build", this.build, typeof(int));

         //version 2
         info.AddValue("Revision", this.Revision, typeof(int));
      }
   }
}
