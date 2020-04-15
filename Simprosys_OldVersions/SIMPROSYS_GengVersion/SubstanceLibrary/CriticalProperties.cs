using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>

   [Serializable]
   public class CriticalProperties : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double criticalTemperature;
      private double criticalPressure;
      private double criticalVolume;
      private double criticalCompressibility;
      private double accentricFactor;
      //temperature in K, pressure in Pa, volume in m3/mol
      public CriticalProperties(double temperature, double presssure, double volume, double compressibility, double accentricFactor) {
         this.criticalTemperature = temperature;
         this.criticalPressure = presssure;
         this.criticalVolume = volume;
         this.criticalCompressibility = compressibility;
         this.accentricFactor = accentricFactor;
      }

      public double CriticalTemperature {
         get { return criticalTemperature; }
         set { criticalTemperature = value; }
      }

      public double CriticalPressure {
         get { return criticalPressure; }
         set { criticalPressure = value; }
      }

      public double CriticalVolume {
         get { return criticalVolume; }
         set { criticalVolume = value; }
      }

      public double CriticalCompressibility {
         get { return criticalCompressibility; }
         set { criticalCompressibility = value; }
      }

      public double AccentricFactor {
         get { return accentricFactor; }
         set { accentricFactor = value; }
      }

      protected CriticalProperties(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCriticalProperties", typeof(int));
         if (persistedClassVersion == 1) {
            this.criticalTemperature = (double)info.GetValue("CriticalTemperature", typeof(double));
            this.criticalPressure = (double)info.GetValue("CriticalPressure", typeof(double));
            this.criticalVolume = (double)info.GetValue("CriticalVolume", typeof(double));
            this.criticalCompressibility = (double)info.GetValue("CriticalCompressibility", typeof(double));
            this.accentricFactor = (double)info.GetValue("AccentricFactor", typeof(double));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCriticalProperties", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CriticalTemperature", this.criticalTemperature, typeof(double));
         info.AddValue("CriticalPressure", this.criticalPressure, typeof(double));
         info.AddValue("CriticalVolume", this.criticalVolume, typeof(double));
         info.AddValue("CriticalCompressibility", this.criticalCompressibility, typeof(double));
         info.AddValue("AccentricFactor", this.accentricFactor, typeof(double));
      }
   }
}
