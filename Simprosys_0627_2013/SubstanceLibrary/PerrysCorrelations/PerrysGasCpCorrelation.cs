using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
  
   [Serializable]
   public class PerrysGasCpCorrelation : ThermalPropCorrelationBase {
      private double c1;
      private double c2;
      private double c3;
      private double c4;
      private double c5;

      private int correlationType = 1;

      public PerrysGasCpCorrelation(string substanceName, string casRegestryNo, double c1, double c2, double c3, double c4, double c5,
            double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
         this.c5 = c5;
      }

      public PerrysGasCpCorrelation(string substanceName, string casRegestryNo, double c1, double c2, double c3, double c4, double c5,
         double minTemperature, double maxTemperature, int correlationType)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
         this.c5 = c5;
         this.correlationType = correlationType;
      }

      //Perry's Gas Heat Capacity default formula--Equation 1
      //calculated value unit is J/kmol.K, t unit is K
      public double GetCp(double t) {
         double cp = 0;
         // this is due to the fact that the coefficients of c2, c3, c4 and c5 are all 0for some substances such as argon 
         // if we don't set the correlation type to 2 the for correlation 1 will be screwed up.
         if(c5 < 1.0e-6 && c4 < 1.0e-6 && c3 < 1.0e-6) 
         {
            correlationType = 2;
         }

         if (correlationType == 1) {
            double term2 = c3 / t;
            term2 = term2 / Math.Sinh(term2);
            term2 = term2 * term2;
            double term3 = c5 / t;
            term3 = term3 / Math.Cosh(term3);
            term3 = term3 * term3;
            cp = c1 + c2 * term2 + c4 * term3;
         }
         else if (correlationType == 2) {
            cp = c1 + c2 * t + c3 * t * t + c4 * Math.Pow(t, 3.0) + c5 * Math.Pow(t, 4.0);
         }
         else if (correlationType == 3) {
            cp = c1 + c2 * Math.Log(t) + c3 / t - c4 * t;
         }
         return cp;
      }

      //Perry's Gas Heat Capacity default formula--Equation 1
      //calculated value unit is J/kmol.K, t unit is K
      public double GetMeanCp(double t1, double t2) {
         double meanCp = 0;
         double h1 = 0;
         double h2 = 0;
         // this is due to the fact that the coefficients of c2, c3, c4 and c5 are all 0for some substances such as argon 
         // if we don't set the correlation type to 2 the for correlation 1 will be screwed up.
         if(c5 < 1.0e-6 && c4 < 1.0e-6 && c3 < 1.0e-6) 
         {
            correlationType = 2;
         }

         if (correlationType == 1) {
            h1 = c1 * t1 + c2 * c3 / Math.Tanh(c3 / t1) - c4 * c5 * Math.Tanh(c5 / t1);
            h2 = c1 * t2 + c2 * c3 / Math.Tanh(c3 / t2) - c4 * c5 * Math.Tanh(c5 / t2);
         }
         else if(correlationType == 2)
         {
            h1 = c1 * t1 + 0.5 * c2 * t1 * t1 + 1 / 3 * c3 * Math.Pow(t1, 3.0) + 0.25 * c4 * Math.Pow(t1, 4.0) + 0.2 * c5 * Math.Pow(t1, 5.0);
            h2 = c1 * t2 + 0.5 * c2 * t2 * t2 + 1 / 3 * c3 * Math.Pow(t2, 3.0) + 0.25 * c4 * Math.Pow(t2, 4.0) + 0.2 * c5 * Math.Pow(t2, 5.0);
         }
         else if(correlationType == 3)
         {
            h1 = c1 * t1 + c2 * t1 * (Math.Log(t1) -1) - c3  * Math.Log(t1) - 0.5 * c4 * t1 * t1;
            h2 = c1 * t2 + c2 * t2 * (Math.Log(t2) -1) - c3  * Math.Log(t2) - 0.5 * c4 * t2 * t2;
         }
         meanCp = (h2 - h1) / (t2 - t1);
         return meanCp;
      }

      protected PerrysGasCpCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.c1 = info.GetDouble("C1");
         this.c2 = info.GetDouble("C2");
         this.c3 = info.GetDouble("C3");
         this.c4 = info.GetDouble("C4");
         this.c5 = info.GetDouble("C5");
         this.correlationType = info.GetInt32("CorrelationType");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("C1", this.c1, typeof(double));
         info.AddValue("C2", this.c2, typeof(double));
         info.AddValue("C3", this.c3, typeof(double));
         info.AddValue("C4", this.c4, typeof(double));
         info.AddValue("C5", this.c5, typeof(double));
         info.AddValue("CorrelationType", this.correlationType, typeof(int));
      }
   }
}
