using System;
using System.Threading;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   public enum SubstanceType { Organic = 0, Inorganic, Unknown };
   /// <summary>
   /// Substance is a class that holds the properties of a substance.
   /// </summary>
   [Serializable]
   public class Substance : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 2;
      protected string name;
      //protected string htmlTaggedName; //version 2
      private string casRegistryNo = ""; //In version 1, casRegistryNo is an int
      private SubstanceFormula formula; //In version 1, formula is a string
      protected double molarWeight;

      protected bool isUserDefined = false;
      protected SubstanceType type = SubstanceType.Unknown;

      //private ThermalPropsAndCoeffs propsAndCoeffs; //version 1
      //private CriticalProperties criticalProps; //version 1

      private CriticalPropsAndAcentricFactor criticalPropsAndAcentricFactor; //version 2

      public const string WATER = "water";
      public const string AIR = "Air";
      public const string NITROGEN = "nitrogen";
      public const string CARBON_TETRACHLORIDE = "carbon tetrachloride";
      public const string BENZENE = "benzene";
      public const string TOLUENE = "toluene";
      public const string ACETONE = "acetone";
      public const string METHANOL = "methanol";
      public const string ETHANOL = "ethanol";
      public const string N_PROPANOL = "n-propanol";
      public const string ISO_PROPANOL = "isopropanol";
      public const string N_BUTANOL = "n-butanol";
      public const string ISO_BUTANOL = "isobutanol";
      public const string ACETIC_ACID = "acetic acid";
      public const string CARBON = "carbon";
      public const string HYDROGEN = "hydrogen";
      public const string OXYGEN = "oxygen";
      public const string SULFUR = "sulfur";
      public const string CARBON_DIOXIDE = "carbon dioxide";
      public const string SULFUR_DIOXIDE = "sulfur dioxide";
      public const string ARGON = "argon";

      public Substance(string name) {
         this.name = name;
         this.isUserDefined = true;
      }

      public Substance(string name, SubstanceType type, bool isUserDefined) {
         this.name = name;
         this.type = type;
         this.isUserDefined = isUserDefined;
      }

      public Substance(string name, SubstanceType type, string casNo, SubstanceFormula formula, double molWt,
            CriticalPropsAndAcentricFactor criticalPropsAndAcentricFactor) {
         this.name = name;
         //htmlTaggedName = null;
         this.type = type;
         this.casRegistryNo = casNo;
         this.formula = formula;
         this.molarWeight = molWt;
         this.IsUserDefined = false;
         this.criticalPropsAndAcentricFactor = criticalPropsAndAcentricFactor;
      }
      
      //public Substance(string name, string htmlTaggedName, SubstanceType type, string casNo, SubstanceFormula formula, double molWt,
      //   CriticalPropsAndAccentricFactor criticalPropsAndAccentricFactor) {
      //   this.name = name;
      //   this.htmlTaggedName = htmlTaggedName;
      //   this.type = type;
      //   this.casRegistryNo = casNo;
      //   this.formula = formula;
      //   this.molarWeight = molWt;
      //   this.IsUserDefined = false;
      //   this.criticalPropsAndAccentricFactor = criticalPropsAndAccentricFactor;
      //}

      public string Name {
         get { return name; }
         set { name = value; }
      }

      //public string HtmlTaggedName {
      //   get {
      //      string taggedName = htmlTaggedName;
      //      if (taggedName == null || taggedName.Equals("")) {
      //         taggedName = "<TD class=BorderHelper noWrap>" + name + " </TD>";
      //      }
      //      return taggedName;
      //   }
      //   set { htmlTaggedName = value; }
      //}
      
      public SubstanceType SubstanceType {
         get { return type; }
         set { type = value; }
      }

      public string CASRegistryNo {
         get { return casRegistryNo; }
         set { casRegistryNo = value; }
      }

      public string FormulaString {
         get {
            if (formula != null) {
               return formula.ToString();
            }
            else {
               return name;
            }
         }
         //set { formula = value; }
      }

      public SubstanceFormula Formula {
         get {return formula; }
      }

      public double MolarWeight {
         get { return molarWeight; }
         set { molarWeight = value; }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
         set { isUserDefined = value; }
      }

      public bool IsAir {
         get { return this.name == Substance.AIR; }
      }

      public bool IsWater {
         get { return this.name == Substance.WATER; }
      }

      //public ThermalPropsAndCoeffs ThermalPropsAndCoeffs {
      //   get { return propsAndCoeffs; }
      //}

      //public CriticalProperties CriticalProperties {
      //   get { return criticalProps; }
      //   set { criticalProps = value; }
      //}

      public CriticalPropsAndAcentricFactor CriticalPropsAndAcentricFactor {
         get { return criticalPropsAndAcentricFactor; }
         set { criticalPropsAndAcentricFactor = value; }
      }
      
      public override string ToString() {
         CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
         TextInfo textInfo = cultureInfo.TextInfo;
         return textInfo.ToTitleCase(name);
      }

      public static Substance RecallSubstance(SerializationInfo info, string name) {
         string substanceName = info.GetValue(name, typeof(string)) as string;
         if (substanceName.Equals("Dry Material")) { //Simprosys version 1.0 saved substance Dry Material is now called Generic Substance.
            substanceName = "Generic Substance";
         }
         return SubstanceCatalog.GetInstance().GetSubstance(substanceName);
      }

      protected Substance(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionSubstance");
         this.name = info.GetString("Name");
         this.molarWeight = info.GetDouble("MolarWeight");
         this.isUserDefined = info.GetBoolean("IsUserDefined");
         this.type = (SubstanceType)info.GetValue("Type", typeof(SubstanceType));
         //this.casRegistryNo = (int)info.GetValue("CasRegistryNo", typeof(int));
         //this.formula = (string)info.GetValue("Formula", typeof(string));
         //this.propsAndCoeffs = info.GetValue("PropsAndCoeffs", typeof(ThermalPropsAndCoeffs)) as ThermalPropsAndCoeffs;
         //this.criticalProps = info.GetValue("CriticalProps", typeof(CriticalProperties)) as CriticalProperties;
         if (persistedClassVersion == 1) {
            int casNo = info.GetInt32("CasRegistryNo");
            this.casRegistryNo = casNo.ToString();
         }

         if (persistedClassVersion >= 2) {
            this.casRegistryNo = info.GetString("CasRegistryNo");
            //this.htmlTaggedName = info.GetString("HtmlTaggedName");
            this.formula = (SubstanceFormula)RecallStorableObject("Formula", typeof(SubstanceFormula));
            this.criticalPropsAndAcentricFactor = (CriticalPropsAndAcentricFactor)RecallStorableObject("CriticalPropsAndAcentricFactor", typeof(CriticalPropsAndAcentricFactor));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionSubstance", CLASS_PERSISTENCE_VERSION, typeof(int));
         //version 1--begin
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("MolarWeight", this.molarWeight, typeof(double));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));
         info.AddValue("Type", this.type, typeof(SubstanceType));
         //info.AddValue("CasRegistryNo", this.casRegistryNo, typeof(int));
         //info.AddValue("Formula", this.formula, typeof(string));
         //info.AddValue("PropsAndCoeffs", this.propsAndCoeffs, typeof(ThermalPropsAndCoeffs));
         //info.AddValue("CriticalProps", this.criticalProps, typeof(CriticalProperties));
         //version 1--end

         //version 2
         info.AddValue("CasRegistryNo", this.casRegistryNo, typeof(string));
         info.AddValue("Formula", this.formula, typeof(SubstanceFormula));
         info.AddValue("CriticalPropsAndAcentricFactor", this.criticalPropsAndAcentricFactor, typeof(CriticalPropsAndAcentricFactor));
         //info.AddValue("HtmlTaggedName", this.htmlTaggedName, typeof(string));
         //version 2
      }
   }
}
