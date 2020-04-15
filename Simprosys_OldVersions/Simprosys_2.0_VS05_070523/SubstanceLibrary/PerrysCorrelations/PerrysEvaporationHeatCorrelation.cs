using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.PerrysCorrelations {
   
   [Serializable]
   public class PerrysEvaporationHeatCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double c1;
      private double c2;
      private double c3;
      private double c4;

      public PerrysEvaporationHeatCorrelation(string substanceName, double c1, double c2, double c3, double c4,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.c1 = c1;
         this.c2 = c2;
         this.c3 = c3;
         this.c4 = c4;
      }

      //Perry's
      //calculated value unit is J/kmol, t unit is K
      public double GetEvaporationHeat(double t, double tc) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         double tr = t / tc;
         double tempValue = c2 + c3 * tr + c4 * tr * tr;
         double r = c1 * Math.Pow((1 - tr), tempValue);
         return r;
      }

      protected PerrysEvaporationHeatCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionPerrysEvaporationHeatCorrelation");
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
         info.AddValue("ClassPersistenceVersionPerrysEvaporationHeatCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("C1", this.c1, typeof(double));
         info.AddValue("C2", this.c2, typeof(double));
         info.AddValue("C3", this.c3, typeof(double));
         info.AddValue("C4", this.c4, typeof(double));
      }
   }
}
