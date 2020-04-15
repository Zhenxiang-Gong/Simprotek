using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsSurfaceTensionCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double tc;
      private double n;

      public YawsSurfaceTensionCorrelation(string substanceName, string casRegestryNo, double a, double tc, double n,
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.a = a;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is kg/m3, t unit is K
      public double GetSurfaceTension(double t) {
         //unit is dynes/cm
         double surfaceTension = a * Math.Pow((1.0 - t / tc), n);
         //convert from dyne/cm to N/m
         return 1.0e-3 * surfaceTension;
      }

      protected YawsSurfaceTensionCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.a = info.GetDouble("A");
         this.tc = info.GetDouble("Tc");
         this.n = info.GetDouble("N");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("Tc", this.tc, typeof(double));
         info.AddValue("N", this.n, typeof(double));
      }
   }
}
