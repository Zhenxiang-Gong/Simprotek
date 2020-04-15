using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Materials {

   [Serializable]
   public class ParticleProperties : NonSolvableProcessVarOwner {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble particleDensity;
      private ProcessVarDouble particleBulkDensity;
      private ArrayList particleSizeAndFractionList = new ArrayList();

      private bool isParticleDistributionsCalculated = false;

      #region public properties
      public ProcessVarDouble ParticleDensity {
         get { return particleDensity; }
      }

      public ProcessVarDouble ParticleBulkDensity {
         get { return particleBulkDensity; }
      }

      public ArrayList ParticleSizeAndFractionList {
         get { return particleSizeAndFractionList; }
         set { particleSizeAndFractionList = value; }
      }

      public bool IsParticleDistributionsCalculated {
         get {
            return isParticleDistributionsCalculated
               && particleSizeAndFractionList != null
               && particleSizeAndFractionList.Count > 1;
         }

         set { isParticleDistributionsCalculated = value; }
      }
      #endregion

      public ParticleProperties() {

         particleDensity = new ProcessVarDouble(StringConstants.PARTICLE_DENSITY, PhysicalQuantity.Density, VarState.Specified, this);
         particleBulkDensity = new ProcessVarDouble(StringConstants.PARTICLE_BULK_DENSITY, PhysicalQuantity.Density, VarState.Specified, this);
      }

      public ParticleProperties Clone() {
         ParticleProperties pp = (ParticleProperties)this.MemberwiseClone();
         pp.particleDensity = particleDensity.Clone();
         pp.particleBulkDensity = particleBulkDensity.Clone();
         pp.particleDensity.Value = Constants.NO_VALUE;
         pp.particleBulkDensity.Value = Constants.NO_VALUE;
         this.particleSizeAndFractionList = CloneParticleSizeAndFractionList();
         return pp;
      }

      protected ArrayList CloneParticleSizeAndFractionList() {
         ArrayList newList = null;
         if (particleSizeAndFractionList != null) {
            newList = new ArrayList();
            foreach (ParticleSizeAndFraction psf in this.particleSizeAndFractionList) {
               Object newPSF = psf.Clone();
               newList.Add(newPSF);
            }
         }
         return newList;
      }

      protected ParticleProperties(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCyclone", typeof(int));
         if (persistedClassVersion == 1) {

            this.particleDensity = RecallStorableObject("ParticleDensity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.particleBulkDensity = RecallStorableObject("ParticleBulkDensity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.particleSizeAndFractionList = RecallArrayListObject("ParticleSizeAndFractionList");
            this.isParticleDistributionsCalculated = (bool)info.GetValue("IsParticleDistributionsCalculated", typeof(bool));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCyclone", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("ParticleDensity", this.particleDensity, typeof(ProcessVarDouble));
         info.AddValue("ParticleBulkDensity", this.particleBulkDensity, typeof(ProcessVarDouble));
         info.AddValue("ParticleSizeAndFractionList", this.particleSizeAndFractionList, typeof(ArrayList));
         info.AddValue("IsParticleDistributionsCalculated", this.isParticleDistributionsCalculated, typeof(bool));
      }
   }
}
