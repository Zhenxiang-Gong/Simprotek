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
	/// Summary description for A.
	/// </summary>
   [Serializable]
   public class A : ISerializable
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

      private ArrayList myList;
      public ArrayList MyList
      {
         get {return myList;}
         set {myList = value;}
      }

      private Hashtable myTable;
      public Hashtable MyTable
      {
         get {return myTable;}
         set {myTable = value;}
      }

      public A()
      {
         this.myList = new ArrayList();
         this.myList.Add(new B(11));
         this.myList.Add(new B(22));
         this.myList.Add(new B(33));

         this.myTable = new Hashtable();
         this.myTable.Add(MyEnum.Zero, new C("zero"));
         this.myTable.Add(MyEnum.One, new C("one"));
         this.myTable.Add(MyEnum.Two, new C("two"));
      }

      protected A(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassVersionA", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.MyList = (ArrayList)info.GetValue("MyList", typeof(ArrayList));
               this.MyTable = (Hashtable)info.GetValue("MyTable", typeof(Hashtable));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassVersionA", A.CLASS_VERSION, typeof(int));
         info.AddValue("MyList", this.MyList, typeof(ArrayList));
         info.AddValue("MyTable", this.MyTable, typeof(Hashtable));
      }
   }
}
