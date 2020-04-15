using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for AAA.
	/// </summary>
   [Serializable]
   /*[DataContract()]*/
   public class AAA : A
	{
      private const int CLASS_VERSION = 1;

      [DataMember()]
      private int classVersion;
      public new int ClassVersion
      {
         get
         {
            return classVersion;
         }
      }

      [DataMember()]
      private int myInt2;
      public int MyInt2
      {
         get 
         {
            return myInt2;
         }
         set
         {
            myInt2 = value;
         }
      }

      public AAA() : base(null)
      {
         this.classVersion = AAA.CLASS_VERSION;
         this.MyInt2 = 0;
      }

      protected AAA(SerializationInfo info, StreamingContext context) : base(info, context)
      {
         this.classVersion = AAA.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionAAA", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyInt2 = (int)info.GetValue("MyInt2", typeof(int));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassVersionAAA", this.ClassVersion, typeof(int));
         info.AddValue("MyInt2", this.MyInt2, typeof(int));
      }
   }
}
