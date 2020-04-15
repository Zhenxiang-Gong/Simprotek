using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for A.
	/// </summary>
   [Serializable]
   public class A : ISerializable
	{
      private const int CLASS_VERSION = 2; 

      private int classVersion;
      public int ClassVersion
      {
         get
         {
            return classVersion;
         }
      }

      private int myInt;
      public int MyInt
      {
         get 
         {
            return myInt;
         }
         set
         {
            myInt = value;
         }
      }

      private BB bbRef;
      public BB BBRef
      {
         get 
         {
            return bbRef;
         }
         set
         {
            bbRef = value;
            // the indirect containment is set manually
            if (bbRef != null)
            {
               bbRef.ARef = this;
            }
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

		public A(BB bbRef)
		{
         this.classVersion = A.CLASS_VERSION;
         this.MyInt = 0;
         this.BBRef = bbRef;
         // the indirect containment is set manually
         if (bbRef != null)
         {
            bbRef.ARef = this;
         }
         this.MyStr = "zero";
		}

      protected A(SerializationInfo info, StreamingContext ctxt)
      {
         this.classVersion = A.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionA", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyInt = (int)info.GetValue("MyInt", typeof(int));
               this.BBRef = (BB)info.GetValue("BBRef", typeof(BB));
               this.MyStr = "zero";
               break;
            case 2:
               this.MyInt = (int)info.GetValue("MyInt", typeof(int));
               this.MyStr = (string)info.GetValue("MyStr", typeof(string));
               this.BBRef = (BB)info.GetValue("BBRef", typeof(BB));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionA", this.ClassVersion, typeof(int));
         info.AddValue("MyInt", this.MyInt, typeof(int));
         info.AddValue("BBRef", this.BBRef, typeof(BB));
         info.AddValue("MyStr", this.MyStr, typeof(string));
      }
   }
}
