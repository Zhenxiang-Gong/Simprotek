using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
  
   [Serializable]
   public class PerrysGasCpCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double c1;
      private double c2;
      private double c3;
      private double c4;
      private double c5;

      private int correlationType = 1;

      public PerrysGasCpCorrelation(string substanceName, double c1, double c2, double c3, double c4, double c5,
            double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
         this.c5 = c5;
      }

      public PerrysGasCpCorrelation(string substanceName, double c1, double c2, double c3, double c4, double c5,
         double minTemperature, double maxTemperature, int correlationType)
         : base (substanceName, minTemperature, maxTemperature) {
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
            cp = c1 + c2 * t + c3 * t * t + c4 * Math.Pow(t, 3.0) + c5 * Math.Pow(t, 5.0);
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
         if (correlationType == 1) {
            double h1 = c1 * t1 + c2 * c3 / Math.Tanh(c3 / t1) - c4 * c5 * Math.Tanh(c5 / t1);
            double h2 = c1 * t2 + c2 * c3 / Math.Tanh(c3 / t2) - c4 * c5 * Math.Tanh(c5 / t2);
            meanCp = (h2 - h1) / (t2 - t1);
         }
         return meanCp;
      }

      protected PerrysGasCpCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionPerrysGasCpCorrelation");
         if (persistedClassVersion == 1) {
            this.c1 = info.GetDouble("C1");
            this.c2 = info.GetDouble("C2");
            this.c3 = info.GetDouble("C3");
            this.c4 = info.GetDouble("C4");
            this.c5 = info.GetDouble("C5");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionPerrysGasCpCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("C1", this.c1, typeof(double));
         info.AddValue("C2", this.c2, typeof(double));
         info.AddValue("C2", this.c3, typeof(double));
         info.AddValue("C4", this.c4, typeof(double));
         info.AddValue("C5", this.c5, typeof(double));
      }
   }
}
