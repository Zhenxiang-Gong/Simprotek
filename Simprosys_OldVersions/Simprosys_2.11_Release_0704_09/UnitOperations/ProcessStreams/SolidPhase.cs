using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for SolidPhase.
   /// </summary>
   [Serializable]
   //public class SolidPhase : Phase, ICloneable 
   public class SolidPhase : Phase {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ParticleProperties particleProperties;

      public SolidPhase(string name, ArrayList components) : base(name, components) {
         particleProperties = new ParticleProperties();
      }                               

      public ParticleProperties ParticleProperties {
         get {return particleProperties;}
         set {particleProperties = value;}
      }
      
      public SolidPhase Clone() {
         SolidPhase newP = (SolidPhase) this.MemberwiseClone();
         newP.components = CloneComponents();
         newP.particleProperties = particleProperties.Clone() as ParticleProperties;
         return newP;
      }

      protected SolidPhase(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolidPhase", typeof(int));
         if (persistedClassVersion == 1) {
            this.particleProperties = RecallStorableObject("ParticleProperties", typeof(ParticleProperties)) as ParticleProperties;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionSolidPhase", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("ParticleProperties", this.particleProperties, typeof(ParticleProperties));
      }
   }
}
