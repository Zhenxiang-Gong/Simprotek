using System;

namespace Drying.ThermalProperties {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class Compound {
      private uint cmpdNo;
      private string name;
      private uint caseNo;
      private double molWt;
      private double cp;
      private double k;
      private double visc;
      private double density;

      public Compound(uint cmpdNo, string name, uint caseNo, double molWt, double cp, double k, double visc, double density) {
         this.cmpdNo = cmpdNo;
         this.name = name;
         this.caseNo = caseNo;
         this.molWt = molWt;
         this.cp = cp;
         this.k = k;
         this.visc = visc;
         this.density = density;
      }

      public uint CompoundNo {
         get {return cmpdNo;}
         set {cmpdNo = value;}
      }

      public string Name {
         get {return name;}
         set {name = name;}
      }

      public uint CaseNo {
         get { return caseNo;}
         set { caseNo = value;}
      }

      public double MolarWeight {
         get { return molWt;}
         set { molWt = value;}
      }

      public double SpecificHeat {
         get { return cp;}
         set { cp = value;}
      }

      public double ThermalConductivity {
         get { return k;}
         set { k = value;}
      }

      public double Viscosity {
         get { return visc;}
         set { visc = value;}
      }

      public double Density {
         get {return density;}
         set {density = value;}
      }
   }
}
