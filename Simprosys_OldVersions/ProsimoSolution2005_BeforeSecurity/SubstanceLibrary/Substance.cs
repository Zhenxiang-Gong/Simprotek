using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   public enum SubstanceType { Organic = 0, Inorganic, Unknown };
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   [Serializable]
   public class Substance : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      protected string name;
      private int casRegistryNo;
      private string formula;
      protected double molarWeight;

      protected bool isUserDefined = false;
      protected SubstanceType type;

      private ThermalPropsAndCoeffs propsAndCoeffs;

      private CriticalProperties criticalProps;

      public Substance(string name) {
         this.name = name;
      }

      public Substance(string name, SubstanceType type, bool isUserDefined) {
         this.name = name;
         this.type = type;
         this.isUserDefined = isUserDefined;
      }

      public Substance(string name, SubstanceType type, int casNo, string formula, double molWt,
         ThermalPropsAndCoeffs propsAndCoeffs, CriticalProperties criticalProps) {
         this.name = name;
         this.type = type;
         this.casRegistryNo = casNo;
         this.formula = formula;
         this.molarWeight = molWt;
         this.IsUserDefined = false;
         this.propsAndCoeffs = propsAndCoeffs;
         this.criticalProps = criticalProps;
      }

      public string Name {
         get { return name; }
         set { name = value; }
      }

      public SubstanceType SubstanceType {
         get { return type; }
         set { type = value; }
      }

      public int CASRegistryNo {
         get { return casRegistryNo; }
         set { casRegistryNo = value; }
      }

      public string Formula {
         get { return formula; }
         set { formula = value; }
      }

      public double MolarWeight {
         get { return molarWeight; }
         set { molarWeight = value; }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
         set { isUserDefined = value; }
      }

      public ThermalPropsAndCoeffs ThermalPropsAndCoeffs {
         get { return propsAndCoeffs; }
      }

      public CriticalProperties CriticalProperties {
         get { return criticalProps; }
         set { criticalProps = value; }
      }

      public override string ToString() {
         return name;
      }

      public static Substance RecallSubstance(SerializationInfo info, string name) {
         string substanceName = info.GetValue(name, typeof(string)) as string;
         return SubstanceCatalog.GetInstance().GetSubstance(substanceName);
      }

      protected Substance(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSubstance", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.casRegistryNo = (int)info.GetValue("CasRegistryNo", typeof(int));
            this.formula = (string)info.GetValue("Formula", typeof(string));
            this.molarWeight = (double)info.GetValue("MolarWeight", typeof(double));
            this.isUserDefined = (bool)info.GetValue("IsUserDefined", typeof(bool));
            this.type = (SubstanceType)info.GetValue("Type", typeof(SubstanceType));
            this.propsAndCoeffs = info.GetValue("PropsAndCoeffs", typeof(ThermalPropsAndCoeffs)) as ThermalPropsAndCoeffs;
            this.criticalProps = info.GetValue("CriticalProps", typeof(CriticalProperties)) as CriticalProperties;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionSubstance", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("CasRegistryNo", this.casRegistryNo, typeof(int));
         info.AddValue("Formula", this.formula, typeof(string));
         info.AddValue("MolarWeight", this.molarWeight, typeof(double));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));
         info.AddValue("Type", this.type, typeof(SubstanceType));
         info.AddValue("PropsAndCoeffs", this.propsAndCoeffs, typeof(ThermalPropsAndCoeffs));
         info.AddValue("CriticalProps", this.criticalProps, typeof(CriticalProperties));
      }
   }
}
