using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class Substance {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      protected string name;
      private string casRegistryNo;
      private SubstanceFormula formula;
      protected double molarWeight;

      protected bool isUserDefined = false;
      protected SubstanceType type;

      private CriticalPropertiesAndAccentricFactor criticalProps;

      public Substance(string name) {
         this.name = name;
      }

      public Substance(string name, SubstanceType type, bool isUserDefined) {
         this.name = name;
         this.type = type;
         this.isUserDefined = isUserDefined;
      }

      public Substance(string name, SubstanceType type, string casNo, SubstanceFormula formula, double molarWeight, CriticalPropertiesAndAccentricFactor criticalProps) {
         this.name = name;
         this.type = type;
         this.casRegistryNo = casNo;
         this.formula = formula;
         this.molarWeight = molarWeight;
         this.IsUserDefined = false;
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

      public string CASRegistryNo {
         get { return casRegistryNo; }
         set { casRegistryNo = value; }
      }

      public string Formula {
         get { return formula.ToString(); }
         //set { formula = value; }
      }

      public double MolarWeight {
         get { return molarWeight; }
         //set { molarWeight = value; }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
         set { isUserDefined = value; }
      }

      //public ThermalPropsAndCoeffs ThermalPropsAndCoeffs {
      //   get { return propsAndCoeffs; }
      //}

      public CriticalPropertiesAndAccentricFactor CriticalProperties {
         get { return criticalProps; }
         set { criticalProps = value; }
      }

      public override string ToString() {
         return name;
      }
   }
}
