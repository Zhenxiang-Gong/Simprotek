using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots
{
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
         get {return name;}
         set {name = value;}
      }

      public float Value {
         get {return val;}
      }

      public PhysicalQuantity PhysicalQuantity {
         get {return pq;}
         set {pq = value;}
      }

      public PointF[] Data {
         get {return data;}
         set {data = value;}
      }

      public CurveF(string name, float val, PointF[] data) {
         this.name = name;
         this.val = val;
         this.pq = pq;
         this.data = data;
      }

      public CurveF(float val, PhysicalQuantity pq, PointF[] data) {
         this.val = val;
         this.pq = pq;
         this.data = data;
      }

      public CurveF(float val, PointF[] data) {
         this.val = val;
         this.data = data;
      }

      public CurveF Clone() {
         CurveF c = (CurveF) this.MemberwiseClone();
         c.name = (string) name.Clone();
         c.data = (PointF[]) data.Clone();
         return c;
      }
      
      //persistence
      protected CurveF(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCurveF", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.val = (float)info.GetValue("Val", typeof(float));
            this.pq = (PhysicalQuantity) info.GetValue("Pq", typeof(PhysicalQuantity)); 
            this.data = (PointF[]) info.GetValue("Data", typeof(PointF[]));
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

   [Serializable]
   public class CurveFamilyF : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      private string name;
      private PhysicalQuantity physicalQuantity;
      //private double varValue;
      //private ProcessVarDouble variable;
      private CurveF[] curves;
      
      public string Name {
         get {return name;}
         set {name = value;}
      }
      
      /*public double VarValue {
         get {return variable.Value;}
         set {variable.Value = value;}
      }*/
      
      public PhysicalQuantity PhysicalQuantity {
         get {return physicalQuantity;}
         set {physicalQuantity = value;}
      }

      /*public ProcessVarDouble Variable {
         get {return variable;}
         //set {variable = value;}
      }*/
      
      public CurveF[] Curves {
         get {return curves;}
         set {curves = value;}
      }

      public CurveFamilyF(string name, PhysicalQuantity pq, CurveF[] curves) {
         this.name = name;
         this.physicalQuantity = pq;
         this.curves = curves;
      }

      /*public CurveFamilyF(string name, PhysicalQuantity pq, double varValue, CurveF[] curves) {
         this.name = name;
         this.pq = pq;
         this.varValue = varValue;
         this.curves = curves;
      }

      public CurveFamilyF(ProcessVarDouble variable, CurveF[] curves) {
         this.variable = variable;
         this.curves = curves;
      }*/

      //persistence
      protected CurveFamilyF(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCurveFamilyF", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string) info.GetValue("Name", typeof(string));
            this.physicalQuantity = (PhysicalQuantity) info.GetValue("PhysicalQuantity", typeof(PhysicalQuantity));
            //this.curves = (CurveF[]) info.GetValue("Curves", typeof(CurveF[]));
            this.curves = (CurveF[]) RecallArrayObject("Curves", typeof(CurveF[]));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCurveFamilyF", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("PhysicalQuantity", this.physicalQuantity, typeof(PhysicalQuantity));
         info.AddValue("Curves", this.curves, typeof(CurveF[]));
      }

   }
}
