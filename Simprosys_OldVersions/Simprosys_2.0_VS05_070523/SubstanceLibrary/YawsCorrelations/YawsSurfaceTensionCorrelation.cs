using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsSurfaceTensionCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      private double tc;
      private double n;

      public YawsSurfaceTensionCorrelation(string substanceName, double a, double tc, double n,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is kg/m3, t unit is K
      public double GetSurfaceTension(double t) {
         //unit is dyne/cm
         double surfaceTension = a * Math.Pow((1.0 - t / tc), n);
         //convert from dyne/cm to N/m
         return 1.0e-3 * surfaceTension;
      }

      protected YawsSurfaceTensionCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsSurfaceTensionCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.tc = info.GetDouble("Tc");
            this.n = info.GetDouble("N");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsSurfaceTensionCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("Tc", this.tc, typeof(double));
         info.AddValue("N", this.n, typeof(double));
      }
   }
}
