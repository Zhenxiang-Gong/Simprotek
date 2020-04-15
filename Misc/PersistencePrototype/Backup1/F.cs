using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace pers
{
	/// <summary>
	/// Summary description for F.
	/// </summary>
   [Serializable]
   public class F : ISerializable
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

      private AA aaRef;
      public AA AARef
      {
         get 
         {
            return aaRef;
         }
         set
         {
            aaRef = value;
         }
      }

      AAA aaaRef;
      public AAA AAARef
      {
         get 
         {
            return aaaRef;
         }
         set
         {
            aaaRef = value;
         }
      }
      
      public F()
		{
         this.classVersion = F.CLASS_VERSION;

         BB bbAA = new BB();
         this.AARef = new AA(bbAA);

         this.AAARef = new AAA();
         BB bbAAA = new BB();
         this.AAARef.BBRef = bbAAA;
      }

      protected F(SerializationInfo info, StreamingContext ctxt)
      {
         this.classVersion = F.CLASS_VERSION;
         int persistedClassVersion = (int)info.GetValue("ClassVersionF", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               // the indirect containment (BB referencing AA) is set automatically by deserialization 
               this.AARef = (AA)info.GetValue("AARef", typeof(AA));

               // the indirect containment (BB referencing AAA) is set automatically by deserialization 
               this.AAARef = (AAA)info.GetValue("AAARef", typeof(AAA));

               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter =true)]
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionF", this.ClassVersion, typeof(int));
         info.AddValue("AARef", this.AARef, typeof(AA));
         info.AddValue("AAARef", this.AAARef, typeof(AAA));
      }
	}
}
