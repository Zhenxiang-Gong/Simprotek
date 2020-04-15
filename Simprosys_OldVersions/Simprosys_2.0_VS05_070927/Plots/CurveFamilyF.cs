using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots {
   [Serializable]
   public class CurveFamilyF : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private string name;
      private PhysicalQuantity physicalQuantity;
      //private double varValue;
      //private ProcessVarDouble variable;
      private CurveF[] curves;

      public string Name {
         get { return name; }
         set { name = value; }
      }

      /*public double VarValue {
         get {return variable.Value;}
         set {variable.Value = value;}
      }*/

      public PhysicalQuantity PhysicalQuantity {
         get { return physicalQuantity; }
         set { physicalQuantity = value; }
      }

      /*public ProcessVarDouble Variable {
         get {return variable;}
         //set {variable = value;}
      }*/

      public CurveF[] Curves {
         get { return curves; }
         set { curves = value; }
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
      protected CurveFamilyF(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCurveFamilyF", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.physicalQuantity = (PhysicalQuantity)info.GetValue("PhysicalQuantity", typeof(PhysicalQuantity));
            //this.curves = (CurveF[]) info.GetValue("Curves", typeof(CurveF[]));
            this.curves = (CurveF[])RecallArrayObject("Curves", typeof(CurveF[]));
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
