using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   [Serializable]
   public class ThermalPropCorrelationBase : Storable {

      protected string substanceName;

      protected string casRegistryNo = "";

      private double minTemperature;
      private double maxTemperature;

      public string SubstanceName {
         get { return substanceName; }
      }

      public string CASRegistryNo {
         get { return casRegistryNo; }
      }

      public double MinTemperature {
         get { return minTemperature; }
      }

      public double MaxTemperature {
         get { return maxTemperature; }
      }

      internal ThermalPropCorrelationBase(string substanceName, string casRegistryNo, double minTemperature, double maxTemperature) {
         this.substanceName = substanceName;
         this.casRegistryNo = casRegistryNo;
         this.minTemperature = minTemperature;
         this.maxTemperature = maxTemperature;
      }

      protected ThermalPropCorrelationBase(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.substanceName = info.GetString("SubstanceName");
         this.casRegistryNo = info.GetString("CasRegistryNo");
         this.minTemperature = info.GetDouble("MinTemperature");
         this.maxTemperature = info.GetDouble("MaxTemperature");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("SubstanceName", this.substanceName, typeof(string));
         info.AddValue("CasRegistryNo", this.casRegistryNo, typeof(string));
         info.AddValue("MinTemperature", this.minTemperature, typeof(double));
         info.AddValue("MaxTemperature", this.maxTemperature, typeof(double));
      }
   }
}
