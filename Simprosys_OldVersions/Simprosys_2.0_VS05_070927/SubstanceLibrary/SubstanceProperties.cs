using System;

namespace Drying.SubstanceLibrary {
   /// <summary>
   /// Summary description for MoistureProperties.
   /// </summary>
   public class SubstanceProperties  {
      //private double meltingPoint;
      //private double enthalpyOfFusion;

      private double[] liqCpCoeffs;
      private double[] gasCpCoeffs;
      private double[] evapHeatCoeffs;
      private double[] vapPressureCoeffs;
      private double[] liqKCoeffs;
      private double[] gasKCoeffs;
      private double[] liqViscCoeffs;
      private double[] gasViscCoeffs;
      private double[] liqDensityCoeffs;

      public SubstanceProperties() {
         //
         // TODO: Add constructor logic here
         //
      }

      public double MeltingPoint {
         get { return meltingPoint;}
         set {meltingPoint = value;}
      }

      public double EnthalpyOfFusion {
         get { return enthalpyOfFusion;}
         set { enthalpyOfFusion = value;}
      }

      public double[] LiqCpCoeffs {
         get {return liqCpCoeffs;}
         set {liqCpCoeffs = value;}
      }

      public double[] GasCpCoeffs {
         get {return gasCpCoeffs;}
         set {gasCpCoeffs = value;}
      }

      public double[] EvapHeatCoeffs {
         get {return evapHeatCoeffs;}
         set {evapHeatCoeffs = value;}
      }

      public double[] VapPressureCoeffs {
         get {return VapPressureCoeffs;}
         set {vapPressureCoeffs = value;}
      }

      public double[] LiqKCoeffs {
         get {return liqKCoeffs;}
         set {liqKCoeffs = value;}
      }

      public double[] GasKCoeffs {
         get {return gasKCoeffs;}
         set {gasKCoeffs = value;}
      }

      public double[] LiqViscCoeffs {
         get {return liqViscCoeffs;}
         set {liqViscCoeffs = value;}
      }

      public double[] GasViscCoeffs {
         get {return gasViscCoeffs;}
         set {gasViscCoeffs = value;}
      }

      public double[] LiqDensityCoeffs {
         get { return liqDensityCoeffs;}
         set { liqDensityCoeffs = value;}
      }

   }
}