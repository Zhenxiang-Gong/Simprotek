using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;

namespace Prosimo.SubstanceLibrary {
   
   [Serializable]
   public class CriticalPropsAndAcentricFactor : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double freezingPoint;
      private double boilingPoint;
      private double criticalTemperature;
      private double criticalPressure;
      private double criticalVolume;
      private double criticalCompressibility;
      private double acentricFactor;
      //temperature in K, pressure in Pa, volume in m3/mol
      public CriticalPropsAndAcentricFactor(double freezingPoint, double boilingPoint, double temperature,
            double presssure, double volume, double compressibility, double acentricFactor) {
         this.freezingPoint = freezingPoint;
         this.boilingPoint = boilingPoint;
         this.criticalTemperature = temperature;
         this.criticalPressure = presssure;
         this.criticalVolume = volume;
         this.criticalCompressibility = compressibility;
         this.acentricFactor = acentricFactor;
      }

      public CriticalPropsAndAcentricFactor(double temperature,
            double presssure, double volume, double compressibility, double acentricFactor) {
         this.freezingPoint = Constants.NO_VALUE;
         this.boilingPoint = Constants.NO_VALUE;
         this.criticalTemperature = temperature;
         this.criticalPressure = presssure;
         this.criticalVolume = volume;
         this.criticalCompressibility = compressibility;
         this.acentricFactor = acentricFactor;
      }
      
      public double FreezingPoint {
         get { return freezingPoint; }
         //set { freezingPoint = value; }
      }

      public double BoilingPoint {
         get { return boilingPoint; }
         //set { boilingPoint = value; }
      }
      
      public double CriticalTemperature {
         get { return criticalTemperature; }
         //set { criticalTemperature = value; }
      }

      public double CriticalPressure {
         get { return criticalPressure; }
         //set { criticalPressure = value; }
      }

      public double CriticalVolume {
         get { return criticalVolume; }
         //set { criticalVolume = value; }
      }

      public double CriticalCompressibility {
         get { return criticalCompressibility; }
         //set { criticalCompressibility = value; }
      }

      public double AcentricFactor {
         get { return acentricFactor; }
         //set { accentricFactor = value; }
      }

      protected CriticalPropsAndAcentricFactor(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionCriticalPropsAndAcentricFactor");
         if (persistedClassVersion == 1) {
            this.freezingPoint = info.GetDouble("FreezingPoint");
            this.boilingPoint = info.GetDouble("BoilingPoint");
            this.criticalTemperature = info.GetDouble("CriticalTemperature");
            this.criticalPressure = info.GetDouble("CriticalPressure");
            this.criticalVolume = info.GetDouble("CriticalVolume");
            this.criticalCompressibility = info.GetDouble("CriticalCompressibility");
            this.acentricFactor = info.GetDouble("AcentricFactor");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCriticalPropsAndAcentricFactor", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("FreezingPoint", this.freezingPoint, typeof(double));
         info.AddValue("BoilingPoint", this.boilingPoint, typeof(double));
         info.AddValue("CriticalTemperature", this.criticalTemperature, typeof(double));
         info.AddValue("CriticalPressure", this.criticalPressure, typeof(double));
         info.AddValue("CriticalVolume", this.criticalVolume, typeof(double));
         info.AddValue("CriticalCompressibility", this.criticalCompressibility, typeof(double));
         info.AddValue("AcentricFactor", this.acentricFactor, typeof(double));
      }
   }
}
