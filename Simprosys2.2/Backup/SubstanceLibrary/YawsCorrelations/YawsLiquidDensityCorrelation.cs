using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   [Serializable]
   public class YawsLiquidDensityCorrelation : ThermalPropCorrelationBase {
      private double a;
      private double b;
      private double tc;
      private double n;

      public YawsLiquidDensityCorrelation(string substanceName, string casRegestryNo, double a, double b, double n, double tc, 
         double minTemperature, double maxTemperature)
         : base(substanceName, casRegestryNo, minTemperature, maxTemperature) {
         this.a = a;
         this.b = b;
         this.tc = tc;
         this.n = n;
      }

      //calculated value unit is kg/m3, t unit is K
      public double GetDensity(double t) {
         double temp = Math.Pow((1.0 - t / tc), n);
         //unit is g/ml
         double density = a * Math.Pow(b, -temp);
         //convert from g/ml to kg/m3
         return 1000 * density;
      }

      protected YawsLiquidDensityCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.a = info.GetDouble("A");
         this.b = info.GetDouble("B");
         this.tc = info.GetDouble("Tc");
         this.n = info.GetDouble("N");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("A", this.a, typeof(double));
         info.AddValue("B", this.b, typeof(double));
         info.AddValue("Tc", this.tc, typeof(double));
         info.AddValue("N", this.n, typeof(double));
      }
   }
}
