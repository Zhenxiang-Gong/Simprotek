using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsLiquidViscosityCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      private double b;
      private double c;
      private double d;

      //public double A {
      //   get { return a; }
      //}

      //public double B {
      //   get { return b; }
      //}

      //public double C {
      //   get { return c; }
      //}

      //public double D {
      //   get { return d; }
      //}

      public YawsLiquidViscosityCorrelation(string substanceName, double a, double b, double c, double d,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
      }

      //Yaws' Chemical Properties
      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public double GetViscosity(double t) {
         //viscosity unit of this formula is cnetipoise
         double logViscLiq = a + b / t + c * t + d * t * t;
         //Convert unit from cnetipoise to Pascal.Second 
         return 0.001 * Math.Pow(10, logViscLiq);
      }

      protected YawsLiquidViscosityCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsLiquidViscosityCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.b = info.GetDouble("B");
            this.c = info.GetDouble("C");
            this.d = info.GetDouble("D");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsLiquidViscosityCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
         info.AddValue("D", this.d, typeof(double));
      }
   }
}
