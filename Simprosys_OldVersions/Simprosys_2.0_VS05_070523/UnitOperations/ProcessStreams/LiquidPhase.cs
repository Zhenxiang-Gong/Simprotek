using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for LiquidPhase.
   /// </summary>
   [Serializable]
   public class LiquidPhase : FluidPhase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public LiquidPhase(string name, ArrayList components) : base(name, components) {
      }

      public LiquidPhase Clone() {
         LiquidPhase newP = (LiquidPhase) this.MemberwiseClone();
         newP.components = CloneComponents();
         return newP;
      }
      
      /*public override double GetViscosity(double temperature, double pressure) {
         //base.CalculateThermalProperties ();
         double viscosity = 0.0;
         double visc;
         double molarWt;
         double moleFrac;
         double numerator = 0.0;
         double denominator = 0.0;
         foreach (MaterialComponent sc in components) {
            Substance s = sc.Substance;
            ThermalPropsAndCoeffs tpc = s.ThermalPropsAndCoeffs;
            molarWt = s.MolarWeight;
            moleFrac = sc.GetMoleFractionValue();
            visc = ThermalPropCalculator.CalculateLiquidViscosity(temperature, tpc.LiqViscCoeffs);
            numerator += visc*moleFrac*Math.Sqrt(molarWt);
            denominator += moleFrac*Math.Sqrt(molarWt);
         }
         if (denominator > 1.0e-8) {
            viscosity = numerator/denominator;
         }

         return viscosity;
      }*/
      
      //public override void CalculateThermalProperties(double temperature, double pressure) {
      //   //base.CalculateThermalProperties ();
      //}

      protected LiquidPhase(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionLiquidPhase", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionLiquidPhase", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
