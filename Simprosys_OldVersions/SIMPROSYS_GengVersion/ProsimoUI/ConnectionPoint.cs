using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for ConnectionPoint.
	/// </summary>
   [Serializable]
   public class ConnectionPoint : ISerializable
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

      private int idx;
      public int Index
      {
         get {return idx;}
         set {idx = value;}
      }

      private string name;
      public string Name
      {
         get {return name;}
         set {name = value;}
      }

      // this point is referenced to the flowsheet
      private Point point;
      public Point Point
      {
         get {return point;}
         set {point = value;}
      }

      private PointOrientation orientation;
      public PointOrientation Orientation
      {
         get {return orientation;}
         set {orientation = value;}
      }
       public ConnectionPoint()
       {

       }
      public ConnectionPoint(int idx, string name, Point p, PointOrientation orientation)
		{
         this.idx = idx;
         this.name = name;
         this.point = p;
         this.orientation = orientation;
		}

      public bool Equals(ConnectionPoint connPoint)
      {
         bool isEqual = false;
         if (this.idx == connPoint.Index && this.name.Equals(connPoint.Name))
         {
            isEqual = true;
         }
         return isEqual;
      }

      protected ConnectionPoint(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionConnectionPoint", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.Index = (int)info.GetValue("Index", typeof(int));
               this.Name = (string)info.GetValue("Name", typeof(string));
               this.Point = (Point)info.GetValue("Point", typeof(Point));
               this.Orientation = (PointOrientation)info.GetValue("Orientation", typeof(PointOrientation));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionConnectionPoint", ConnectionPoint.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Index", this.Index, typeof(int));
         info.AddValue("Name", this.Name, typeof(string));
         info.AddValue("Point", this.Point, typeof(Point));
         info.AddValue("Orientation", this.Orientation, typeof(PointOrientation));
      }
	}
}
