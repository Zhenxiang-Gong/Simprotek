using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsVaporPressureCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double c;
      private double d;
      private double e;

      public YawsVaporPressureCorrelation(string substanceName, string casRegestryNo, double a, double b, double c, double d, double e,
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
         this.d = d;
         this.e = e;
      }

      //calculated value unit is Pa, t unit is K
      public double GetSaturationPressure(double t) {
         // p is in mmHg
         double logP = a + b / t + c * Math.Log10(t) + d * t + e * t * t;
         double p = Math.Pow(10, logP);
         //convert mmHg to Pa
         p = 133.3223684 * p;
         return p;
      }

      //calculated value unit is Pa, t unit is K
      public double GetSaturationTemperature(double p) {
         //convert Pa to mmHg
         p /= 133.3223684;
         double temperature = 298.15;
         double old_temp;
         double fx;
         double dfx;
         int i = 0;
         do {
            i++;
            old_temp = temperature;
            //Newton iterative method--much better convergence speed
            fx = a + b / old_temp + c * Math.Log10(old_temp) + d * old_temp + e * old_temp * old_temp - Math.Log10(p);
            dfx = -b / (old_temp * old_temp) + c * Math.Log10(Math.E) / old_temp + d + 2.0 * e * old_temp;
            temperature = old_temp - fx / dfx;
         } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

         return temperature;
      }

      protected YawsVaporPressureCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.a = info.GetDouble("A");
         this.b = info.GetDouble("B");
         this.c = info.GetDouble("C");
         this.d = info.GetDouble("D");
         this.e = info.GetDouble("E");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
         info.AddValue("D", this.d, typeof(double));
         info.AddValue("E", this.e, typeof(double));
      }
   }
}
