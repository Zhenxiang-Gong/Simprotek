using System;

namespace Drying.UnitSystems
{
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class RatioConverter {
      public RatioConverter() {
      }

      public static double ConvertToSIValue(RatioUnit unitType, double toBeConvertedValue) {
         double percentValue = 0;
         if (unitType == RatioUnit.Decimal) {
            percentValue = 100 * toBeConvertedValue;
         }
         if (unitType == RatioUnit.Percent) {
            percentValue = toBeConvertedValue;
         }

         return percentValue;
      }
      
      public static double ConvertFromSIValue(RatioUnit unitType, double percentValue) {
         double convertedValue = 0;
         if (unitType == RatioUnit.Decimal) {
            convertedValue = percentValue/100;
         }
         if (unitType == RatioUnit.Percent) {
            percentValue = percentValue;
         }
         return convertedValue;
      }
   
      public static string GetUnit(RatioUnit unitType) {
         string unitString = "";
         if (unitType == RatioUnit.Decimal) {
            unitString = "";
         }
         if (unitType == RatioUnit.Percent) {
            unitString = "%";
         }

         return unitString;
      }
   }
}
