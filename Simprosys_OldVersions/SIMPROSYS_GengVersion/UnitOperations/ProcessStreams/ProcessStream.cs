using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public class ProcessStream : ProcessStreamBase {
      private const int CLASS_PERSISTENCE_VERSION = 1;
 
      //public ProcessStream(DryingSystem dryingSystem) : base(dryingSystem) {
      //}
      
      //public ProcessStream(string name, DryingSystem dryingSystem) : base(name, dryingSystem) {
      public ProcessStream(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, mComponents, uoSys) {
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(massFlowRate);
         AddVarOnListAndRegisterInSystem(volumeFlowRate);
         AddVarOnListAndRegisterInSystem(pressure);
         AddVarOnListAndRegisterInSystem(temperature);
         AddVarOnListAndRegisterInSystem(specificEnthalpy);
         AddVarOnListAndRegisterInSystem(specificHeat);
         AddVarOnListAndRegisterInSystem(density);
         //AddVarOnListAndRegisterInSystem(specificVolume);
         //AddVarOnListAndRegisterInSystem(dynamicViscosity);
         //AddVarOnListAndRegisterInSystem(thermalConductivity);
         //vapor fraction is not used in drying gas stream and solid drying material stream
         //therefore, we should only add this var on the list of the liquid material stream
         AddVarOnListAndRegisterInSystem(vaporFraction);
      }

      internal override double GetBoilingPoint(double pressureValue) {
         double tEvap = 373.15;
         /*Phase cmp = streamComponents.MajorPhase;
         if (cmp is FluidPhase) {
            FluidPhase fp = cmp as FluidPhase;
            fp.CalculateThermalProperties(temperature.Value, pressure.Value);
            tEvap = fp.BoilingPoint;
         } */
         return tEvap;
      }
      
      internal override double GetEvaporationHeat(double temperature) {
         double evapHeat = 2500000.0;
         /*Phase cmp = streamComponents.MajorPhase;
         if (cmp is FluidPhase) {
            FluidPhase fp = cmp as FluidPhase;
            fp.CalculateThermalProperties(temperature.Value, pressure.Value);
            evapHeat = fp.EvaporationHeat;
         }*/
         return evapHeat;
      }
 
      internal override double GetSolidCp(double tempValue) {
         return 2500;
      }
      internal override double GetLiquidCp(double tempValue) {
         return 2500;
      }
      internal override double GetGasCp(double tempValue) {
         return 2500;
      }

      protected ProcessStream(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
         //RecallInitialization();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionProcessStream", ProcessStream.CLASS_PERSISTENCE_VERSION, typeof(int));
      }

      //protected override void RecallInitialization() {
      //   Init();
      //   base.RecallInitialization();
      //}
   }
}
