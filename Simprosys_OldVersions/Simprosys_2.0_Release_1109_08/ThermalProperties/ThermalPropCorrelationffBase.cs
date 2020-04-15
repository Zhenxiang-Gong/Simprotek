using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   [Serializable]
   public class ThermalPropCorrelationBase : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      protected string substanceName;

      private double minTemperature;
      private double maxTemperature;

      public string SubstanceName {
         get { return substanceName; }
      }

      double MinTemperature {
         get { return minTemperature; }
      }

      double MaxTemperature {
         get { return maxTemperature; }
      }

      internal ThermalPropCorrelationBase(string substanceName, double minTemperature, double maxTemperature) {
         this.substanceName = substanceName;
         this.minTemperature = minTemperature;
         this.maxTemperature = maxTemperature;
      }
      protected ThermalPropCorrelationBase(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionThermalPropCorrelationBase");
         if (persistedClassVersion == 1) {
            this.substanceName = info.GetString("SubstanceName");
            this.minTemperature = info.GetDouble("MinTemperature");
            this.maxTemperature = info.GetDouble("MaxTemperature");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionThermalPropCorrelationBase", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("SubstanceName", this.substanceName, typeof(string));
         info.AddValue("MinTemperature", this.minTemperature, typeof(double));
         info.AddValue("MaxTemperature", this.maxTemperature, typeof(double));
      }
   }
}
