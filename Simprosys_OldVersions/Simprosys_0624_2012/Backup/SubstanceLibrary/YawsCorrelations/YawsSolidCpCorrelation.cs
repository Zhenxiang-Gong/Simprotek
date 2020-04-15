using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsSolidCpCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;

      public YawsSolidCpCorrelation(string substanceName, string casRegestryNo, double a, double b, double c,
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetCp(double t) {
         return a + b * t + c * t * t;
      }

      //calculated value unit is J/mol.K, t unit is K
      public double GetMeanCp(double t1, double t2) {
         double h1 = a * t1 + 0.5 * b * Math.Pow(t1, 2) + 1.0 / 3.0 * c * Math.Pow(t1, 3);
         double h2 = a * t2 + 0.5 * b * Math.Pow(t2, 2) + 1.0 / 3.0 * c * Math.Pow(t2, 3);
         double meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }

      protected YawsSolidCpCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.a = info.GetDouble("A");
         this.b = info.GetDouble("B");
         this.c = info.GetDouble("C");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
      }
   }
}
