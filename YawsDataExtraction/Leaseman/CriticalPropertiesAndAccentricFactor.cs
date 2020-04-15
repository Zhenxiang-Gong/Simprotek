using System;
using System.Collections.Generic;
using System.Text;

namespace Prosimo.SubstanceLibrary {
   public class CriticalPropertiesAndAccentricFactor {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private double freezingPoint;
      private double boilingPoint;
      private double criticalTemperature;
      private double criticalPressure;
      private double criticalVolume;
      private double criticalCompressibility;
      private double accentricFactor;
      //temperature in K, pressure in Pa, volume in m3/mol
      public CriticalPropertiesAndAccentricFactor(double freezingPoint, double boilingPoint, double temperature, double presssure, double volume, double compressibility, double accentricFactor) {
         this.freezingPoint = freezingPoint;
         this.boilingPoint = boilingPoint;
         this.criticalTemperature = temperature;
         this.criticalPressure = presssure;
         this.criticalVolume = volume;
         this.criticalCompressibility = compressibility;
         this.accentricFactor = accentricFactor;
      }

      public double FreezingPoint {
         get { return freezingPoint; }
         set { freezingPoint = value; }
      }

      public double BoilingPoint {
         get { return boilingPoint; }
         set { boilingPoint = value; }
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
   }
}
