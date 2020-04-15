using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
   [Serializable]
   public class PerrysLiquidDensityCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double c1;
      private double c2;
      private double c3;
      private double c4;

      public PerrysLiquidDensityCorrelation(string substanceName, double c1, double c2, double c3, double c4,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
      }

      //Perry's
      //calculated value unit is kmol/m3, t unit is K
      public double GetDensity(double t) {
         double tempValue = 1.0 + Math.Pow((1.0 - t / c3), c4);
         return c1 / Math.Pow(c2, tempValue);
      }

      protected PerrysLiquidDensityCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionPerrysLiquidDensityCorrelation");
         if (persistedClassVersion == 1) {
            this.c1 = info.GetDouble("C1");
            this.c2 = info.GetDouble("C2");
            this.c3 = info.GetDouble("C3");
            this.c4 = info.GetDouble("C4");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionPerrysLiquidDensityCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("C1", this.c1, typeof(double));
         info.AddValue("C2", this.c2, typeof(double));
         info.AddValue("C3", this.c3, typeof(double));
         info.AddValue("C4", this.c4, typeof(double));
      }
   }
}
