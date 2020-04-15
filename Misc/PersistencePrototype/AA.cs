using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for AA.
	/// </summary>
   [Serializable]
   /*[DataContract()]*/
   public class AA : A
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

      [DataMember()]
      private B b;

		public AA(BB bbRef) : base(bbRef)
		{
         this.classVersion = AA.CLASS_VERSION;
         this.MyBool = false;
         this.b = new B();

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
