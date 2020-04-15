using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;


namespace Prosimo.UnitOperations.GasSolidSeparation {
   [Serializable] 
   public abstract class GasSolidSeparatorRatingModel : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      
      protected UnitOperation ownerUnitOp;
      protected IGasSolidSeparator owner;

      protected ParticleProperties particleProperties;
      protected ArrayList particleSizeFractionAndEfficiencyList = new ArrayList(); 
                                                                  
      #region public properties
      internal ProcessVarDouble CollectionEfficiency {
         get {return owner.CollectionEfficiency;}
      }

      public ParticleProperties ParticleProperties {
         get { return particleProperties; }
      }

      public ProcessVarDouble ParticleDensity {
         get { return particleProperties.ParticleDensity; }
      }

      public ProcessVarDouble ParticleBulkDensity {
         get { return particleProperties.ParticleBulkDensity; }
      }

       internal ArrayList ParticleSizeFractionAndEfficiencyList {
         get {return particleSizeFractionAndEfficiencyList;}
      }

      public ParticleDistributionCache GetParticleDistributionCache() {
         return new ParticleDistributionCache(this);
      }

      #endregion
      
      protected GasSolidSeparatorRatingModel(IGasSolidSeparator owner) : base() {
         this.owner = owner;
         this.ownerUnitOp = owner.MyUnitOperation;
         DryingGasStream dgs = owner.GasInlet as DryingGasStream;
         DryingGasComponents dgc = dgs.GasComponents;
         SolidPhase sp = dgc.SolidPhase;
         if (sp == null) {
            particleProperties = new ParticleProperties();
         } else {
            ParticleSizeFractionAndEfficiency sfe;
            particleProperties = sp.ParticleProperties;
            ArrayList sizeAndFractionList = particleProperties.ParticleSizeAndFractionList;
            if (particleProperties.IsParticleDistributionsCalculated) {
               foreach (ParticleSizeAndFraction psf in sizeAndFractionList) {
                  sfe = new ParticleSizeFractionAndEfficiency(psf);
                  particleSizeFractionAndEfficiencyList.Add(sfe);
               }
            }
         }
         
         this.ParticleDensity.Owner = ownerUnitOp;
         this.ParticleBulkDensity.Owner = ownerUnitOp;
      }

      internal void CommittParticleDistributions (ArrayList sizeFractionAndEfficiencyList) {
         particleSizeFractionAndEfficiencyList = sizeFractionAndEfficiencyList;
         ArrayList aList = new ArrayList();
         foreach (ParticleSizeFractionAndEfficiency sfe in sizeFractionAndEfficiencyList) {
            aList.Add(sfe.ParticleSizeAndFraction);
         }
         particleProperties.ParticleSizeAndFractionList = aList;
         ownerUnitOp.HasBeenModified(true);
      }

      public virtual bool IsRatingCalcReady() {
         return true;
      }

      internal abstract double CalculateCollectionEfficiencies(ArrayList particleDistributionList);

      protected GasSolidSeparatorRatingModel(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCyclone", typeof(int));
         if (persistedClassVersion == 1) {
            this.particleProperties = RecallStorableObject("ParticleProperties", typeof(ParticleProperties)) as ParticleProperties;
            this.particleSizeFractionAndEfficiencyList = RecallArrayListObject("ParticleSizeFractionAndEfficiencyList");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCyclone", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("ParticleProperties", this.particleProperties, typeof(ParticleProperties));
         info.AddValue("ParticleSizeFractionAndEfficiencyList", this.particleSizeFractionAndEfficiencyList, typeof(ArrayList));
      }                                                 
   }
}
