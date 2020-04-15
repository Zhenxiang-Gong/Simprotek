using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   /// <summary>
   /// Summary description for StreamComponent.
   /// </summary>
   [Serializable]
   public class MaterialComponent : NonSolvableProcessVarOwner {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private Substance substance;
      private ProcessVarDouble massFraction;
      private ProcessVarDouble moleFraction;

      public MaterialComponent(Substance substance) {
         this.substance = substance;
         this.massFraction = new ProcessVarDouble(StringConstants.MASS_FRACTION, PhysicalQuantity.Fraction, 1.0, VarState.Specified, this);
         this.moleFraction = new ProcessVarDouble(StringConstants.MOLE_FRACTION, PhysicalQuantity.Fraction, 1.0, VarState.Specified, this);
      }

      public MaterialComponent(Substance substance, double massFraction) {
         this.substance = substance;
         this.massFraction = new ProcessVarDouble(StringConstants.MASS_FRACTION, PhysicalQuantity.Fraction, massFraction, VarState.Specified, this);
         this.moleFraction = new ProcessVarDouble(StringConstants.MOLE_FRACTION, PhysicalQuantity.Fraction, 1.0, VarState.Specified, this);
      }

      public MaterialComponent Clone() {
         MaterialComponent sc = this.MemberwiseClone() as MaterialComponent;
         sc.MassFraction = massFraction.Clone();
         sc.MoleFraction = moleFraction.Clone();
         return sc;
      }

      public Substance Substance {
         get { return substance; }
      }

      public override string Name {
         get { return substance.Name; }
      }

      public double GetMassFractionValue() {
         return massFraction.Value;
      }

      public double GetMoleFractionValue() {
         return moleFraction.Value;
      }

      public void SetMoleFractionValue(double v) {
         moleFraction.Value = v;
      }

      public void SetMassFractionValue(double v) {
         massFraction.Value = v;
      }

      public ProcessVarDouble MassFraction {
         get { return massFraction; }
         set { massFraction = value; }
      }

      public ProcessVarDouble MoleFraction {
         get { return moleFraction; }
         set { moleFraction = value; }
      }

      protected MaterialComponent(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMaterialComponent", typeof(int));
         if (persistedClassVersion == 1) {

            this.substance = CompositeSubstance.RecallSubstance(info);

            this.massFraction = RecallStorableObject("MassFraction", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.moleFraction = RecallStorableObject("MoleFraction", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterialComponent", CLASS_PERSISTENCE_VERSION, typeof(int));

         //has to store substance in a special way since it may be a compisite substance
         CompositeSubstance.StoreSubstance(info, substance);

         info.AddValue("MassFraction", massFraction, typeof(ProcessVarDouble));
         info.AddValue("MoleFraction", moleFraction, typeof(ProcessVarDouble));
      }
   }
}
