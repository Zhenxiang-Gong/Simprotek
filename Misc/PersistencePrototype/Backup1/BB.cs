using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for BB.
	/// </summary>
   [Serializable]
   public class BB : B
	{
      private const int CLASS_VERSION = 2;
      
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


      private string myStr;
      public string MyStr
      {
         get 
         {
            return myStr;
         }
         set
         {
            myStr = value;
         }
      }

		public BB()
		{
         this.classVersion = BB.CLASS_VERSION;
         this.MyBool = false;
         this.MyStr = "zero";
      }

      protected BB(SerializationInfo info, StreamingContext context) : base (info, context)
      {
         this.classVersion = BB.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionBB", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyBool = (bool)info.GetValue("MyBool", typeof(bool));
               this.MyStr = "zero";
               break;
            case 2:
               this.MyBool = (bool)info.GetValue("MyBool", typeof(bool));
               this.MyStr = (string)info.GetValue("MyStr", typeof(string));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);
         info.AddValue("ClassVersionBB", this.ClassVersion, typeof(int));
         info.AddValue("MyBool", this.MyBool, typeof(bool));
         info.AddValue("MyStr", this.MyStr, typeof(string));
      }
	}
}
