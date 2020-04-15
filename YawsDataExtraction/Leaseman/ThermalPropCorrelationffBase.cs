using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class ThermalPropCorrelationBase {
      protected string substanceName;
      //protected SubstanceType substanceType;
      //protected string casRegestryNo;
      //protected string formula;

      private double minTemperature;
      private double maxTemperature;

      public string SubstanceName {
         get { return substanceName; }
      }

      //public SubstanceType SubstanceType {
      //   get { return substanceType; }
      //}

      //public string CasRegestryNo {
      //   get { return casRegestryNo; }
      //}

      //public string Formula {
      //   get { return formula; }
      //}

      double MinTemperature {
         get { return minTemperature; }
      }

      double MaxTemperature {
         get { return maxTemperature; }
      }

      //internal ThermalPropCorrelationBase(string substanceName, SubstanceType substanceType, 
      //   string casRegestryNo, string formula, double minTemperature, double maxTemperature) {
      internal ThermalPropCorrelationBase(string substanceName, double minTemperature, double maxTemperature) {
         this.substanceName = substanceName;
         //this.substanceType = substanceType;
         //this.casRegestryNo = casRegestryNo;
         //this.formula = formula;
         this.minTemperature = minTemperature;
         this.maxTemperature = maxTemperature;
      }
   }
}
