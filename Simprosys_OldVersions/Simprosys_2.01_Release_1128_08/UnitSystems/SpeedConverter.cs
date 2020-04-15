using System;

namespace Drying.UnitSystems
{
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class VelocityConverter {
      public VelocityConverter() {
      }

      public static double ConvertToSIValue(VelocityUnit unitType, double toBeConvertedValue) {
         double meterPerSecValue = 0;
         if (unitType == VelocityUnit.MillimeterPerSec) {
            meterPerSecValue =  1.0e-3 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.CentimeterPerSec) {
            meterPerSecValue =  1.0e-2 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.CentimeterPerMin) {
            meterPerSecValue =  toBeConvertedValue/6000;
         }
         else if (unitType == VelocityUnit.MeterPerSec) {
            meterPerSecValue =  toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.MeterPerMin) {
            meterPerSecValue =  toBeConvertedValue/60;
         }
         else if (unitType == VelocityUnit.MeterPerHour) {
            meterPerSecValue =  toBeConvertedValue/3600;
         }
         else if (unitType == VelocityUnit.MeterPerDay) {
            meterPerSecValue =  toBeConvertedValue/86400;
         }
         else if (unitType == VelocityUnit.KilometerPerSec) {
            meterPerSecValue =  1000 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            meterPerSecValue =  1000/60 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.KilometerPerHour) {
            meterPerSecValue =  toBeConvertedValue/3.6;
         }
         else if (unitType == VelocityUnit.KilometerPerDay) {
            meterPerSecValue =  toBeConvertedValue/86.4;
         }
         else if (unitType == VelocityUnit.InchPerSec) {
            meterPerSecValue =  0.0254 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.InchPerMin) {
            meterPerSecValue =  4.23333333e-4 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.InchPerHour) {
            meterPerSecValue =  7.05555556e-6 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.FootPerSec) {
            meterPerSecValue =  0.3048 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.FootPerMin) {
            meterPerSecValue =  0.00508 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.FootPerHour) {
            meterPerSecValue =  8.46666667E-5 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.FootPerDay) {
            meterPerSecValue =  3.52777778E-5 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.MilePerSec) {
            meterPerSecValue =  1609.344 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            meterPerSecValue =  26.8224 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.MilePerHour) {
            meterPerSecValue =  0.44704 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.MilePerDay) {
            meterPerSecValue =  0.0186266667 * toBeConvertedValue;
         }
         else if (unitType == VelocityUnit.Knot) {
            meterPerSecValue =  0.514444444 * toBeConvertedValue;
         }

         return meterPerSecValue;
      }
      
      public static double ConvertFromSIValue(VelocityUnit unitType, double meterPerSecValue) {
         double convertedValue = 0;
         if (unitType == VelocityUnit.MillimeterPerSec) {
            convertedValue = 1000 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.CentimeterPerSec) {
            convertedValue = 100 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.CentimeterPerMin) {
            convertedValue = 6000 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.MeterPerSec) {
            convertedValue = meterPerSecValue;
         }
         else if (unitType == VelocityUnit.MeterPerMin) {
            convertedValue = 60 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.MeterPerHour) {
            convertedValue = 3600 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.MeterPerDay) {
            convertedValue = 86400 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.KilometerPerSec) {
            convertedValue = meterPerSecValue/1000;
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            convertedValue = 0.06 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.KilometerPerHour) {
            convertedValue = 3.6 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.KilometerPerDay) {
            convertedValue = 86.4 * meterPerSecValue;
         }
         else if (unitType == VelocityUnit.InchPerSec) {
            convertedValue = meterPerSecValue/0.0254;
         }
         else if (unitType == VelocityUnit.InchPerMin) {
            convertedValue = meterPerSecValue/4.23333333e-4;
         }
         else if (unitType == VelocityUnit.InchPerHour) {
            convertedValue = meterPerSecValue/7.05555556e-6;
         }
         else if (unitType == VelocityUnit.FootPerSec) {
            convertedValue = meterPerSecValue/0.3048;
         }
         else if (unitType == VelocityUnit.FootPerMin) {
            convertedValue = meterPerSecValue/0.00508;
         }
         else if (unitType == VelocityUnit.FootPerHour) {
            convertedValue = meterPerSecValue/8.46666667E-5;
         }
         else if (unitType == VelocityUnit.FootPerDay) {
            convertedValue = meterPerSecValue/3.52777778E-5;
         }
         else if (unitType == VelocityUnit.MilePerSec) {
            convertedValue = meterPerSecValue/1609.344;
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            convertedValue = meterPerSecValue/26.8224;
         }
         else if (unitType == VelocityUnit.MilePerHour) {
            convertedValue = meterPerSecValue/0.44704;
         }
         else if (unitType == VelocityUnit.MilePerDay) {
            convertedValue = meterPerSecValue/0.0186266667;
         }
         else if (unitType == VelocityUnit.Knot) {
            convertedValue = meterPerSecValue/0.514444444;
         }

         return convertedValue;
      }
      
      public static string GetUnitAsString(VelocityUnit unitType) {
         string unitString = "";
         if (unitType == VelocityUnit.MillimeterPerSec) {
            unitString = "mm/s";
         }
         else if (unitType == VelocityUnit.CentimeterPerSec) {
            unitString = "cm/s";
         }
         else if (unitType == VelocityUnit.CentimeterPerMin) {
            unitString = "cm/min";
         }
         else if (unitType == VelocityUnit.MeterPerSec) {
            unitString = "m/s";
         }
         else if (unitType == VelocityUnit.MeterPerMin) {
            unitString = "m/min";
         }
         else if (unitType == VelocityUnit.MeterPerHour) {
            unitString = "m/hr";
         }
         else if (unitType == VelocityUnit.MeterPerDay) {
            unitString = "m/day";
         }
         else if (unitType == VelocityUnit.KilometerPerSec) {
            unitString = "km/s";
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            unitString = "km/min";
         }
         else if (unitType == VelocityUnit.KilometerPerHour) {
            unitString = "km/hr";
         }
         else if (unitType == VelocityUnit.KilometerPerDay) {
            unitString = "km/day";
         }
         else if (unitType == VelocityUnit.InchPerSec) {
            unitString = "in/s";
         }
         else if (unitType == VelocityUnit.InchPerMin) {
            unitString = "in/min";
         }
         else if (unitType == VelocityUnit.InchPerHour) {
            unitString = "in/hr";
         }
         else if (unitType == VelocityUnit.FootPerSec) {
            unitString = "ft/s";
         }
         else if (unitType == VelocityUnit.FootPerMin) {
            unitString = "ft/min";
         }
         else if (unitType == VelocityUnit.FootPerHour) {
            unitString = "ft/hr";
         }
         else if (unitType == VelocityUnit.FootPerDay) {
            unitString = "ft/day";
         }
         else if (unitType == VelocityUnit.MilePerSec) {
            unitString = "mile/s";
         }
         else if (unitType == VelocityUnit.KilometerPerMin) {
            unitString = "km/min";
         }
         else if (unitType == VelocityUnit.MilePerHour) {
            unitString = "mile/hr";
         }
         else if (unitType == VelocityUnit.MilePerDay) {
            unitString = "mile/day";
         }
         else if (unitType == VelocityUnit.Knot) {
            unitString = "knot";
         }

         return unitString;
      }
   
      public static VelocityUnit GetUnitAsEnum(string unitString) {
         VelocityUnit unitType = VelocityUnit.MeterPerSec;
         if (unitString.Equals("mm/s")) {
            unitType = VelocityUnit.MillimeterPerSec;
         }
         else if (unitString.Equals("cm/s")) {
            unitType = VelocityUnit.CentimeterPerSec;
         }
         else if (unitString.Equals("cm/min")) {
            unitType = VelocityUnit.CentimeterPerMin;
         }
         else if (unitString.Equals("m/s")) {
            unitType = VelocityUnit.MeterPerSec;
         }
         else if (unitString.Equals("m/min")) {
            unitType = VelocityUnit.MeterPerMin;
         }
         else if (unitString.Equals("m/hr")) {
            unitType = VelocityUnit.MeterPerHour;
         }
         else if (unitString.Equals("m/day")) {
            unitType = VelocityUnit.MeterPerDay;
         }
         else if (unitString.Equals("km/s")) {
            unitType = VelocityUnit.KilometerPerSec;
         }
         else if (unitString.Equals("km/min")) {
            unitType = VelocityUnit.KilometerPerMin;
         }
         else if (unitString.Equals("km/hr")) {
            unitType = VelocityUnit.KilometerPerHour;
         }
         else if (unitString.Equals("km/day")) {
            unitType = VelocityUnit.KilometerPerDay;
         }
         else if (unitString.Equals("in/s")) {
            unitType = VelocityUnit.InchPerSec;
         }
         else if (unitString.Equals("in/min")) {
            unitType = VelocityUnit.InchPerMin;
         }
         else if (unitString.Equals("in/hr")) {
            unitType = VelocityUnit.InchPerHour;
         }
         else if (unitString.Equals("ft/s")) {
            unitType = VelocityUnit.FootPerSec;
         }
         else if (unitString.Equals("ft/min")) {
            unitType = VelocityUnit.FootPerMin;
         }
         else if (unitString.Equals("ft/hr")) {
            unitType = VelocityUnit.FootPerHour;
         }
         else if (unitString.Equals("ft/day")) {
            unitType = VelocityUnit.FootPerDay;
         }
         else if (unitString.Equals("mile/s")) {
            unitType = VelocityUnit.MilePerSec;
         }
         else if (unitString.Equals("km/min")) {
            unitType = VelocityUnit.KilometerPerMin;
         }
         else if (unitString.Equals("mile/hr")) {
            unitType = VelocityUnit.MilePerHour;
         }
         else if (unitString.Equals("mile/day")) {
            unitType = VelocityUnit.MilePerDay;
         }
         else if (unitString.Equals("knot")) {
            unitType = VelocityUnit.Knot;
         }

         return unitType;
      }
   }
}
