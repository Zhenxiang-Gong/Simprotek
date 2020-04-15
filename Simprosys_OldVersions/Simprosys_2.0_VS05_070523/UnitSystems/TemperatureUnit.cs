using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum TemperatureUnitType {Kelvin = 0, Celsius, Fahrenheit, Rankine, Reaumur};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class TemperatureUnit {
      protected Hashtable coeffTable = new Hashtable();
      protected Hashtable unitStringTable = new Hashtable();
      public static TemperatureUnit Instance = new TemperatureUnit();

      public TemperatureUnit() {
         unitStringTable.Add(TemperatureUnitType.Kelvin, "K");
         unitStringTable.Add(TemperatureUnitType.Celsius, "°C");
         unitStringTable.Add(TemperatureUnitType.Fahrenheit, "°F");
         unitStringTable.Add(TemperatureUnitType.Rankine, "°R");
         unitStringTable.Add(TemperatureUnitType.Reaumur, "Reaumur");
      }

      public double ConvertToSIValue(TemperatureUnitType unitType, double toBeConvertedValue) {
         double kelvinValue = 0;
         if (unitType == TemperatureUnitType.Celsius) {
            kelvinValue = toBeConvertedValue + 273.15;
         }
         else if (unitType == TemperatureUnitType.Fahrenheit) {
            kelvinValue = (toBeConvertedValue + 459.67)/1.8;
         }
         else if (unitType == TemperatureUnitType.Kelvin) {
            kelvinValue = toBeConvertedValue;
         }
         else if (unitType == TemperatureUnitType.Rankine) {
            kelvinValue = toBeConvertedValue/1.8;
         }
         else if (unitType == TemperatureUnitType.Reaumur) {
            kelvinValue = 0.8 * toBeConvertedValue + 273.15;
         }

         return kelvinValue;
      }
      
      public double ConvertFromSIValue(TemperatureUnitType unitType, double kelvinValue) {
         double convertedValue = 0;
         if (unitType == TemperatureUnitType.Celsius) {
            convertedValue = kelvinValue - 273.15;
         }
         else if (unitType == TemperatureUnitType.Fahrenheit) {
            convertedValue = 1.8 * kelvinValue - 459.67;
         }
         else if (unitType == TemperatureUnitType.Kelvin) {
            convertedValue = kelvinValue;
         }
         else if (unitType == TemperatureUnitType.Rankine) {
            convertedValue = 1.8 * kelvinValue;
         }
         else if (unitType == TemperatureUnitType.Reaumur) {
            convertedValue = (kelvinValue - 273.15)/0.8;
         }
         return convertedValue;
      }

      public string GetUnitAsString(TemperatureUnitType unitType) {
         return unitStringTable[unitType] as String;
      }

      public TemperatureUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         TemperatureUnitType type = TemperatureUnitType.Kelvin;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (TemperatureUnitType)myEnumerator.Key;
               break;
            }
         }
         return type;
      }

      public ICollection GetUnitsAsStrings() {
         return unitStringTable.Values;
      }
   }
}
