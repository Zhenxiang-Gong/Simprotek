using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsVaporPressureCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      private double b;
      private double c;
      private double d;
      private double e;

      public YawsVaporPressureCorrelation(string substanceName, double a, double b, double c, double d, double e,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
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
         double temperature = 298.15;
         double old_temp;
         double fx;
         double dfx;
         int i = 0;
         do {
            i++;
            old_temp = temperature;
            //direct iterative method
            //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
            //   - 4.1653e-6 * old_temp * old_temp);
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
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsVaporPressureCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.b = info.GetDouble("B");
            this.c = info.GetDouble("C");
            this.d = info.GetDouble("D");
            this.e = info.GetDouble("E");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsVaporPressureCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
         info.AddValue("D", this.d, typeof(double));
         info.AddValue("E", this.e, typeof(double));
      }
   }
}
