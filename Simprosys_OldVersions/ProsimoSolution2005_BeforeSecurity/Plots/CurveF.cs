using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   [Serializable]
   public class CurveF : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private string name;
      private float val;
      private PhysicalQuantity pq;
      private PointF[] data;

      public string Name {
         get { return name; }
         set { name = value; }
      }

      public float Value {
         get { return val; }
      }

      public PhysicalQuantity PhysicalQuantity {
         get { return pq; }
         set { pq = value; }
      }

      public PointF[] Data {
         get { return data; }
         set { data = value; }
      }

      public CurveF(string name, float val, PointF[] data) {
         this.name = name;
         this.val = val;
         this.data = data;
      }

      public CurveF(float val, PhysicalQuantity pq, PointF[] data) {
         this.name = "";
         this.val = val;
         this.pq = pq;
         this.data = data;
      }

      public CurveF(float val, PointF[] data) {
         this.name = "";
         this.val = val;
         this.data = data;
      }

      public CurveF Clone() {
         CurveF c = (CurveF)this.MemberwiseClone();
         c.name = (string)name.Clone();
         c.data = (PointF[])data.Clone();
         return c;
      }

      public override bool Equals(object obj) {
         bool isEqual = false;
         CurveF cf = obj as CurveF;
         if (this.name.Equals(cf.Name) && this.pq == cf.PhysicalQuantity && this.val == cf.Value &&
            data.Length == cf.Data.Length) {
            for (int i = 0; i < data.Length; i++) {
               if (data[i].X == cf.Data[i].X && data[i].Y == cf.Data[i].Y) {
                  isEqual = true;
               }
               else {
                  isEqual = false;
                  break;
               }
            }
         }
         return isEqual;
      }

      public override int GetHashCode() {
         return this.name.GetHashCode();
      }

      public override string ToString() {
         return name;
      }

      //persistence
      protected CurveF(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCurveF", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.val = (float)info.GetValue("Val", typeof(float));
            this.pq = (PhysicalQuantity)info.GetValue("Pq", typeof(PhysicalQuantity));
            this.data = (PointF[])info.GetValue("Data", typeof(PointF[]));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCurveF", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("Val", this.val, typeof(float));
         info.AddValue("Pq", this.pq, typeof(PhysicalQuantity));
         info.AddValue("Data", this.data, typeof(PointF[]));
      }
   }
}
