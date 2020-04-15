using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsOrganicLiquidThermalConductivityCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;

      public YawsOrganicLiquidThermalConductivityCorrelation(string substanceName, string casRegestryNo, double a, double b, double c,
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public double GetThermalConductivity(double t) {
         double k = a + b * t + c * t * t;
         return k;
      }

      protected YawsOrganicLiquidThermalConductivityCorrelation(SerializationInfo info, StreamingContext context)
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
