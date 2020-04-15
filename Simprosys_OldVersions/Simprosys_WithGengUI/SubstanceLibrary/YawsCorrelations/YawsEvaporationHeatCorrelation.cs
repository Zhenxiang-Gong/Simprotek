using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   
   [Serializable]
   public class YawsEvaporationHeatCorrelation : ThermalPropCorrelationBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double a;
      //critical temperature
      private double tc;
      private double n;
      
      public YawsEvaporationHeatCorrelation(string substanceName, double a, double tc, double n,
         double minTemperature, double maxTemperature)
         : base(substanceName, minTemperature, maxTemperature) {
         this.a = a;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is J/mol, t unit is K
      public double GetEvaporationHeat(double t) {
         double tr = t / tc;
         //calculated value unit is kJ/mol
         double r = a * Math.Pow((1 - tr), n);
         //convert to J/mol;
         return 1000 * r;
      }

      protected YawsEvaporationHeatCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionYawsEvaporationHeatCorrelation");
         if (persistedClassVersion == 1) {
            this.a = info.GetDouble("A");
            this.tc = info.GetDouble("Tc");
            this.n = info.GetDouble("N");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionYawsEvaporationHeatCorrelation", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("Tc", this.tc, typeof(double));
         info.AddValue("N", this.n, typeof(double));
      }
   }
}
