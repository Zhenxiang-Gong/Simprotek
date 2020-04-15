using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Materials {

   [Serializable]
   public class ParticleSizeAndFraction : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble diameter;
      private ProcessVarDouble weightFraction;

      public ParticleSizeAndFraction(IProcessVarOwner owner) {
         diameter = new ProcessVarDouble(StringConstants.DIAMETER, PhysicalQuantity.MicroLength, 2.0e-5, VarState.Specified, owner);
         weightFraction = new ProcessVarDouble(StringConstants.WEIGHT_FRACTION, PhysicalQuantity.Fraction, 1.0, VarState.Specified, owner);
      }

      public ParticleSizeAndFraction Clone() {
         ParticleSizeAndFraction psaf = (ParticleSizeAndFraction)this.MemberwiseClone();
         psaf.Diameter = diameter.Clone();
         psaf.WeightFraction = weightFraction.Clone();
         return psaf;
      }

      public ProcessVarDouble Diameter {
         get { return diameter; }
         set { diameter = value; }
      }

      public ProcessVarDouble WeightFraction {
         get { return weightFraction; }
         set { weightFraction = value; }
      }

      protected ParticleSizeAndFraction(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionParticleSizeAndFraction", typeof(int));
         if (persistedClassVersion == 1) {
            this.diameter = RecallStorableObject("Diameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.weightFraction = RecallStorableObject("WeightFraction", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionParticleSizeAndFraction", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Diameter", this.diameter, typeof(ProcessVarDouble));
         info.AddValue("WeightFraction", this.weightFraction, typeof(ProcessVarDouble));
      }
   }
}
