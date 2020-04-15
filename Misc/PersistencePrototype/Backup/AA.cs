using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for AA.
	/// </summary>
   [Serializable]
   public class AA : A
	{
      private const int CLASS_VERSION = 1; 

      private int classVersion;
      public new int ClassVersion
      {
         get
         {
            return classVersion;
         }
      }

      private bool myBool;
      public bool MyBool
      {
         get 
         {
            return myBool;
         }
         set
         {
            myBool = value;
         }
      }

		public AA(BB bbRef) : base(bbRef)
		{
         this.classVersion = AA.CLASS_VERSION;
         this.MyBool = false;
		}

      protected AA(SerializationInfo info, StreamingContext context) : base(info, context)
      {
         this.classVersion = AA.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionAA", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyBool = (bool)info.GetValue("MyBool", typeof(bool));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassVersionAA", this.ClassVersion, typeof(int));
         info.AddValue("MyBool", this.MyBool, typeof(bool));
      }
	}
}
