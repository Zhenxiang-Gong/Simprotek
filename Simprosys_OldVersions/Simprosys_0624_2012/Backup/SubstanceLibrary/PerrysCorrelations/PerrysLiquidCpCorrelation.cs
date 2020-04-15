using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
   [Serializable]
   public class PerrysLiquidCpCorrelation : ThermalPropCorrelationBase {
      private double c1;
      private double c2;
      private double c3;
      private double c4;
      private double c5;
      private double tc;

      private int correlationType = 1;

      //public PerrysLiquidCpCorrelation(string substanceName, string casRegestryNo, double c1, double c2, double c3, double c4, 
      //   double c5, double tc, double minTemperature, double maxTemperature)
      //   : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
      //   this.c1 = c1;
      //   this.c2 = c2;
      //   this.c3 = c3;
      //   this.c4 = c4;
      //   this.c5 = c5;
      //   this.tc = tc;
      //}

      public PerrysLiquidCpCorrelation(string substanceName, string casRegestryNo, double c1, double c2, double c3, double c4, 
         double c5, double tc, double minTemperature, double maxTemperature, int correlationType) 
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
         this.c5 = c5;
         this.tc = tc;
         this.correlationType = correlationType;
      }

      //Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
      //calculated value unit is J/kmol.K, t unit is K
      public double GetCp(double t) {
         double cp = 0;
         if (correlationType == 1) {
            cp = c1 + c2 * t + c3 * t * t + c4 * Math.Pow(t, 3.0) + c5 * Math.Pow(t, 4.0);
         }
         else if (correlationType == 2) {
            t = 1.0 - t / tc;
            cp = c1 * c1 / t + c2 - 2.0 * c1 * c3 * t - c1 * c4 * t * t - Math.Pow(c3, 2.0 / 3.0) * Math.Pow(t, 3.0) - c3 * c4 / 2.0 * Math.Pow(t, 4.0) - Math.Pow(c4, 2.0 / 5.0) * Math.Pow(t, 5.0);
         }
         return cp;
      }

      //Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
      //calculated value unit is J/kmol.K, t unit is K
      public double GetMeanCp(double t1, double t2) {
         double meanCp = 0;
         if (correlationType == 1) {
            double h1 = c1 * t1 + c2 * t1 * t1 / 2.0 + c3 * Math.Pow(t1, 3.0) / 3.0 + c4 * Math.Pow(t1, 4.0) / 4.0 + c5 * Math.Pow(t1, 5.0) / 5.0;
            double h2 = c1 * t2 + c2 * t2 * t2 / 2.0 + c3 * Math.Pow(t2, 3.0) / 3.0 + c4 * Math.Pow(t2, 4.0) / 4.0 + c5 * Math.Pow(t2, 5.0) / 5.0;
            meanCp = (h2 - h1) / (t2 - t1);
         }
         if (correlationType == 2) {
         }
         return meanCp;
      }

      ////Perry's Liquid Heat Capacity equation 2
      ////calculated value is J/kmol.K, t unit is K
      //public double GetCp2(double t, double tc) {
      //   t = 1.0 - t / tc;
      //   return c1 * c1 / t + c2 - 2.0 * c1 * c3 * t - c1 * c4 * t * t - Math.Pow(c3, 2.0 / 3.0) * Math.Pow(t, 3.0) - c3 * c4 / 2.0 * Math.Pow(t, 4.0) - Math.Pow(c4, 2.0 / 5.0) * Math.Pow(t, 5.0);
      //}

      protected PerrysLiquidCpCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.c1 = info.GetDouble("C1");
         this.c2 = info.GetDouble("C2");
         this.c3 = info.GetDouble("C3");
         this.c4 = info.GetDouble("C4");
         this.c5 = info.GetDouble("C5");
         this.tc = info.GetDouble("TC");
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
         info.AddValue("TC", this.tc, typeof(double));
         info.AddValue("CorrelationType", this.correlationType, typeof(int));
      }
   }
}
