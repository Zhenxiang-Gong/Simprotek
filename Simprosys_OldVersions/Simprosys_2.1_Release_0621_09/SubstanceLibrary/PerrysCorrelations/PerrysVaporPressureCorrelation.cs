using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
   [Serializable]
   public class PerrysVaporPressureCorrelation : ThermalPropCorrelationBase {
      private double c1;
      private double c2;
      private double c3;
      private double c4;
      private double c5;

      public PerrysVaporPressureCorrelation(string substanceName, string casRegestryNo, double c1, double c2, double c3, double c4, double c5,
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
         this.c5 = c5;
      }

      //Perry's
      //calculated value unit is Pa, t unit is K
      public double GetSaturationPressure(double t) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         double p = Math.Exp(c1 + c2 / t + c3 * Math.Log(t) + c4 * Math.Pow(t, c5));
         return p;
      }

      //Perry's
      //calculated value unit is Pa, t unit is K
      public double GetSaturationTemperature(double p) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         //double p = Math.Exp(c[0] + c[1]/t + c[2] * Math.Log(t) + c[3] * Math.Pow(t, c[4]));
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
            fx = c1 + c2 / old_temp + c3 * Math.Log(old_temp) + c4 * Math.Pow(old_temp, c5) - Math.Log(p);
            dfx = -c2 / (old_temp * old_temp) - c3 / old_temp + c4 * c5 * Math.Pow(old_temp, c5 - 1);
            temperature = old_temp - fx / dfx;
         } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

         return temperature;
      }

      protected PerrysVaporPressureCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.c1 = info.GetDouble("C1");
         this.c2 = info.GetDouble("C2");
         this.c3 = info.GetDouble("C3");
         this.c4 = info.GetDouble("C4");
         this.c5 = info.GetDouble("C5");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("C1", this.c1, typeof(double));
         info.AddValue("C2", this.c2, typeof(double));
         info.AddValue("C3", this.c3, typeof(double));
         info.AddValue("C4", this.c4, typeof(double));
         info.AddValue("C5", this.c5, typeof(double));
      }
   }
}
