using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsLiquidThermalConductivityCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      private double b;
      private double c;

      public YawsLiquidThermalConductivityCorrelation(string substanceName, double a, double b, double c, 
         double minTemperature, double maxTemperature)
         : base (substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public double GetThermalConductivity(double t, SubstanceType substanceType) {
         double k = 0.0;
         if (substanceType == SubstanceType.Organic) {
            double logKLiq = a + b * Math.Pow((1.0 - t / c), 2.0 / 7.0);
            k = Math.Pow(10, logKLiq);
         }
         else if (substanceType == SubstanceType.Inorganic) {
            k = a + b * t + c * t * t;
         }

         return k;
      }

      protected YawsLiquidThermalConductivityCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsLiquidThermalConductivityCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.b = info.GetDouble("B");
            this.c = info.GetDouble("C");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsLiquidThermalConductivityCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
      }
}
}
