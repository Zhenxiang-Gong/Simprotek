using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.Materials;
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {

   [Serializable]
   public class ParticleSizeFractionAndEfficiency : NonSolvableProcessVarOwner {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ParticleSizeAndFraction sizeAndFraction;
      private ProcessVarDouble efficiency;

      internal ParticleSizeAndFraction ParticleSizeAndFraction {
         get {return sizeAndFraction;}
      }

      public ParticleSizeFractionAndEfficiency() {
         this.sizeAndFraction = new ParticleSizeAndFraction(this);
         efficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, this);
      }

      public ParticleSizeFractionAndEfficiency(ParticleSizeAndFraction sizeAndFraction) {
         this.sizeAndFraction = sizeAndFraction;
         efficiency = new ProcessVarDouble(StringConstants.COLLECTION_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, this);
      }

      public Object Clone() {
         ParticleSizeFractionAndEfficiency psaf = (ParticleSizeFractionAndEfficiency) this.MemberwiseClone();
         psaf.sizeAndFraction = sizeAndFraction.Clone() as ParticleSizeAndFraction;
         psaf.efficiency = efficiency.Clone() as ProcessVarDouble;
         efficiency.Value = Constants.NO_VALUE;
         return psaf;
      }

      public ProcessVarDouble Diameter {
         get {return sizeAndFraction.Diameter;}
      }

      public ProcessVarDouble WeightFraction {
         get {return sizeAndFraction.WeightFraction;}
      }

      public ProcessVarDouble Efficiency {
         get {return efficiency;}
      }
      
      protected ParticleSizeFractionAndEfficiency (SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionParticleSizeAndFraction", typeof(int));
         if (persistedClassVersion == 1) {
            this.sizeAndFraction = info.GetValue("SizeAndFraction", typeof(ParticleSizeAndFraction)) as ParticleSizeAndFraction;
            this.efficiency = RecallStorableObject("Efficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionParticleSizeAndFraction", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("SizeAndFraction", this.sizeAndFraction, typeof(ParticleSizeAndFraction));
         info.AddValue("Efficiency", this.efficiency, typeof(ProcessVarDouble));
      }
   }
}
