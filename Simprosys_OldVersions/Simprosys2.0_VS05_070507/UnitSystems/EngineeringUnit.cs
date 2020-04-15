using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
   public abstract class EngineeringUnit {
      protected Hashtable coeffTable = new Hashtable();
      protected Hashtable unitStringTable = new Hashtable();

      public double ConvertToSIValue<UnitType>(UnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }

      public double ConvertFromSIValue<UnitType>(UnitType unitType, double sqMeterValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return sqMeterValue/convertionCoeff;
      }

      public string GetUnitAsString<UnitType>(UnitType unitType) {
         return unitStringTable[unitType] as String;
      }

      public UnitType GetUnitAsEnum<UnitType>(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         UnitType type = default(UnitType);
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (UnitType)myEnumerator.Key;
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