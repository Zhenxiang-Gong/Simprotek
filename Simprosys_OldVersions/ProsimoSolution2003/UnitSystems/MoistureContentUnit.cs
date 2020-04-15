using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MoistureContentUnitType {KgPerKg = 0, GramPerGram, IbPerIb, OzPerOz};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MoistureContentUnit {
      private static Hashtable unitStringTable = new Hashtable();

      static MoistureContentUnit() {
         unitStringTable.Add(MoistureContentUnitType.KgPerKg, "kg/kg");
         unitStringTable.Add(MoistureContentUnitType.GramPerGram, "g/g");
         unitStringTable.Add(MoistureContentUnitType.IbPerIb, "Ibm/Ibm");
         unitStringTable.Add(MoistureContentUnitType.OzPerOz, "oz/oz");
      }

      public static double ConvertToSIValue(MoistureContentUnitType unitType, double toBeConvertedValue) {
         return toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(MoistureContentUnitType unitType, double kgPerKgValue) {
         return kgPerKgValue;
      }
      
      public static string GetUnitAsString(MoistureContentUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
   
      public static MoistureContentUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         MoistureContentUnitType type = MoistureContentUnitType.KgPerKg;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (MoistureContentUnitType) myEnumerator.Key;
               break;
            }
         }
         return type;
      }

      public static ICollection GetUnitsAsStrings() {
         return unitStringTable.Values;
      }
   }
}
