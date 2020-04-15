using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   /// <summary>
   /// Summary description for MoistureProperties.
   /// </summary>
   [Serializable]
   public class ThermalPropsAndCoeffs : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
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

      public ThermalPropsAndCoeffs(double[] liqCpCoeffs, double[] gasCpCoeffs, double[] evapHeatCoeffs, 
         double[] vapPressureCoeffs, double[] liqKCoeffs, double[] gasKCoeffs, double[] liqViscCoeffs, 
         double[] gasViscCoeffs, double[] liqDensityCoeffs) {
         this.liqCpCoeffs = liqCpCoeffs;
         this.gasCpCoeffs = gasCpCoeffs;
         this.evapHeatCoeffs = evapHeatCoeffs;
         this.vapPressureCoeffs = vapPressureCoeffs;
         this.liqKCoeffs = liqKCoeffs;
         this.gasKCoeffs = gasKCoeffs;
         this.liqViscCoeffs = liqViscCoeffs;
         this.gasViscCoeffs = gasViscCoeffs;
         this.liqDensityCoeffs = liqDensityCoeffs;
      }

      //public double MeltingPoint {
      //   get { return meltingPoint;}
      //   set {meltingPoint = value;}
      //}

      //public double EnthalpyOfFusion {
      //   get { return enthalpyOfFusion;}
      //   set { enthalpyOfFusion = value;}
      //}

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
         get {return vapPressureCoeffs;}
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

      protected ThermalPropsAndCoeffs (SerializationInfo info, StreamingContext context) : base(info, context){
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionThermalPropsAndCoeffs", typeof(int));
         if (persistedClassVersion == 1) {
            this.liqCpCoeffs = (double[])info.GetValue("LiqCpCoeffs", typeof(double[]));
            this.gasCpCoeffs = (double[])info.GetValue("GasCpCoeffs", typeof(double[]));
            this.evapHeatCoeffs = (double[])info.GetValue("EvapHeatCoeffs", typeof(double[]));
            this.vapPressureCoeffs = (double[])info.GetValue("VapPressureCoeffs", typeof(double[]));
            this.liqKCoeffs = (double[])info.GetValue("LiqKCoeffs", typeof(double[]));
            this.gasKCoeffs = (double[])info.GetValue("GasKCoeffs", typeof(double[]));
            this.liqViscCoeffs = (double[])info.GetValue("LiqViscCoeffs", typeof(double[]));
            this.gasViscCoeffs = (double[])info.GetValue("GasViscCoeffs", typeof(double[]));
            this.liqDensityCoeffs = (double[])info.GetValue("LiqDensityCoeffs", typeof(double[]));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionThermalPropsAndCoeffs", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("LiqCpCoeffs", this.liqCpCoeffs, typeof(double[]));
         info.AddValue("GasCpCoeffs", this.gasCpCoeffs, typeof(double[]));
         info.AddValue("EvapHeatCoeffs", this.evapHeatCoeffs, typeof(double[]));
         info.AddValue("VapPressureCoeffs", this.vapPressureCoeffs, typeof(double[]));
         info.AddValue("LiqKCoeffs", this.liqKCoeffs, typeof(double[]));
         info.AddValue("GasKCoeffs", this.gasKCoeffs, typeof(double[]));
         info.AddValue("LiqViscCoeffs", this.liqViscCoeffs, typeof(double[]));
         info.AddValue("GasViscCoeffs", this.gasViscCoeffs, typeof(double[]));
         info.AddValue("LiqDensityCoeffs", this.liqDensityCoeffs, typeof(double[]));
      }
   }
}