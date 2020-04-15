using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsEnthalpyOfFormationCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      private double b;
      private double c;

      public YawsEnthalpyOfFormationCorrelation(string substanceName, double a, double b, double c, 
         double minTemperature, double maxTemperature)
         : base (substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.c = c;
      }

      //Yaws' Chemical Properties
      //calculated value unit is w/m.K, t unit is K
      public double GetEnthalpyOfFormation(double t) {
         double enthalpy = 0.0;
         enthalpy = a + b * t + c * t * t;

         return enthalpy;
      }

      protected YawsEnthalpyOfFormationCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsEnthalpyOfFormationCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.b = info.GetDouble("B");
            this.c = info.GetDouble("C");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsEnthalpyOfFormationCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("C", this.c, typeof(double));
      }
}
}
