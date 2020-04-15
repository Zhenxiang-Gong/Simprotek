using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary.YawsCorrelations {
   
   [Serializable]
   public class YawsEnthalpyOfCombustionCorrelation : ThermalPropCorrelationBase {
      private double moleValue; //kJ/mole
      private double kgValue;  //kJ/kg
      //temperature
      private double t; //K
      private string state;

      public YawsEnthalpyOfCombustionCorrelation(string substanceName, string casRegestryNo, string state, double moleValue, double kgValue, double t)
         : base(substanceName, casRegestryNo, t, t) {
         this.moleValue = moleValue;
         this.kgValue = kgValue;
         this.t = t;
         this.state = state;
      }

      //calculated value unit is J/mol
      public double GetEnthalpyOfCombustion() {
         //convert to J/mol;
         return 1000 * moleValue;
      }

      protected YawsEnthalpyOfCombustionCorrelation(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         this.moleValue = info.GetDouble("MoleValue");
         this.kgValue = info.GetDouble("KgValue");
         this.t = info.GetDouble("T");
         this.state = info.GetString("State");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("MoleValue", this.moleValue, typeof(double));
         info.AddValue("KgValue", this.kgValue, typeof(double));
         info.AddValue("T", this.t, typeof(double));
         info.AddValue("State", this.state, typeof(string));
      }
   }
}
