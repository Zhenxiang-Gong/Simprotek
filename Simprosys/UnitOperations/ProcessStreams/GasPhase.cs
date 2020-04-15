using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for GasPhase.
   /// </summary>
   [Serializable]
   public class GasPhase : FluidPhase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public GasPhase(string name, ArrayList components) : base(name, components) {
      }

      public GasPhase Clone() {
         GasPhase newP = (GasPhase) this.MemberwiseClone();
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
            if (s.Name == "Air") {
               visc = ThermalPropCalculator.CalculateAirGasViscosity(temperature);
            }
            else {
               visc = ThermalPropCalculator.CalculateGasViscosity(temperature, tpc.GasViscCoeffs);
            }
            numerator += visc*moleFrac*Math.Sqrt(molarWt);
            denominator += moleFrac*Math.Sqrt(molarWt);
         }
         if (denominator > 1.0e-8) {
            viscosity = numerator/denominator;
         }

         return viscosity;
      }*/

      protected GasPhase(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionGasPhase", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionGasPhase", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
