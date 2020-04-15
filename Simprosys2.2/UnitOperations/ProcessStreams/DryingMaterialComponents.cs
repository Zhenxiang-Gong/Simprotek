using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   /// <summary>
   /// Summary description for DryingMaterial.
   /// </summary>
   [Serializable]
   public class DryingMaterialComponents : MaterialComponents {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      
      public DryingMaterialComponents(ArrayList components) : base(components) {
      }

      public ProcessVarDouble DriedMaterialMassFraction {
         get {return this.AbsoluteDryMaterial.MassFraction;}
      }

      public ProcessVarDouble MoistureMassFraction {
         get {return this.AbsoluteDryMaterial.MassFraction;}
      }

      public MaterialComponent AbsoluteDryMaterial {
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

      public Phase LiquidPhase {
         get {return phases[0] as Phase;}
         set {phases[0] = value;}
      }
   
      public Phase SolidPhase {
         get {
            Phase sp = null;
            if (phases.Count > 1) {
               sp = phases[1] as Phase;
            }
            return sp;
         }
         set {phases[1] = value;}
      }
      
      protected DryingMaterialComponents(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingMaterialComponents", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingMaterialComponents", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
