using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for DryingGas.
   /// </summary>
   [Serializable]
   public class DryingGasComponents : MaterialComponents {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public DryingGasComponents(ArrayList components) : base(components) {
      }

      public ProcessVarDouble DryMediumMassFraction {
         get {return this.DryMedium.MassFraction;}
      }

      public ProcessVarDouble MoistureMassFraction {
         get {return this.Moisture.MassFraction;}
      }
      
      public ProcessVarDouble AbsoluteDryMaterialMassFraction {
         get {return this.AbsoluteDryMaterial.MassFraction;}
      }
      
      public MaterialComponent DryMedium {
         get {
            MaterialComponent sp = (MaterialComponent)components[0];
            return sp;
         }

         set { components[0] = value;}
      }
   
      public MaterialComponent Moisture {
         get {
            MaterialComponent sp = (MaterialComponent)components[1];
            return sp;
         }

         set { components[1] = value;}
      }
   
      public MaterialComponent AbsoluteDryMaterial {
         get {
            MaterialComponent sp = (MaterialComponent)components[2];
            return sp;
         }

         set { components[2] = value;}
      }

      public GasPhase GasPhase {
         get {return phases[0] as GasPhase;}
         set {phases[0] = value;}
      }
   
      public SolidPhase SolidPhase {
         get {
            SolidPhase sp = null;
            if (phases.Count > 1) {
               sp = phases[1] as SolidPhase;
            }
            return sp;
         }
         set {
            if (phases.Count > 1) {
               phases[1] = value;
            }
         }
      }
      
      public override void ComponentsFractionsChanged() {
         Phase gasPhase = this.phases[0] as Phase;
         MaterialComponent sc = gasPhase[0];
         sc.SetMassFractionValue(this.DryMedium.GetMassFractionValue());
         sc = gasPhase[1];
         sc.SetMassFractionValue(this.Moisture.GetMassFractionValue());
         gasPhase.Normalize();
         //gasPhase.CalculateThermalProperties(temperature, pressure);
      }
      
      /*public override void UpdateThermalProperties(double temperature, double pressure) {
         Phase gasPhase = this.phases[0] as Phase;
         MaterialComponent sc = gasPhase[0];
         sc.SetMassFractionValue(this.DryMedium.GetMassFractionValue());
         sc = gasPhase[1];
         sc.SetMassFractionValue(this.Moisture.GetMassFractionValue());
         gasPhase.Normalize();
         //gasPhase.CalculateThermalProperties(temperature, pressure);
      }*/

      protected DryingGasComponents(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

       public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingGasComponents", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingGasComponents", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
