using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
   /// <summary>
   /// Summary description for B.
   /// </summary>
   [Serializable]
   public class B : ISerializable 
   {
      private const int CLASS_VERSION = 1;

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

      private A aRef;
      public A ARef
      {
         get 
         {
            return aRef;
         }
         set
         {
            aRef = value;
         }
      }

      public B()
      {
         this.classVersion = B.CLASS_VERSION;
         this.MyInt = 0;
         this.ARef = null;
      }

      protected B(SerializationInfo info, StreamingContext ctxt)
      {
         this.classVersion = B.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionB", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyInt = (int)info.GetValue("MyInt", typeof(int));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionB", this.ClassVersion, typeof(int));
         info.AddValue("MyInt", this.MyInt, typeof(int));
      }
   }
}
