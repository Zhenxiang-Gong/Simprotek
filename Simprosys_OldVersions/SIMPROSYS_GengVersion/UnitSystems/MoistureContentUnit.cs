using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MoistureContentUnitType {KgPerKg = 0, GramPerGram, IbPerIb, GrPerIb};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MoistureContentUnit {
      private static Hashtable unitStringTable = new Hashtable();
      private static Hashtable coeffTable = new Hashtable();

      static MoistureContentUnit() {
         coeffTable.Add(MoistureContentUnitType.KgPerKg, 1.0); unitStringTable.Add(MoistureContentUnitType.KgPerKg, "kg/kg");
         coeffTable.Add(MoistureContentUnitType.GramPerGram, 1.0); unitStringTable.Add(MoistureContentUnitType.GramPerGram, "g/g");
         coeffTable.Add(MoistureContentUnitType.IbPerIb, 1.0); unitStringTable.Add(MoistureContentUnitType.IbPerIb, "Ib/Ib");
         coeffTable.Add(MoistureContentUnitType.GrPerIb, 7000.0); unitStringTable.Add(MoistureContentUnitType.GrPerIb, "gr/Ib");
      }

      public static double ConvertToSIValue(MoistureContentUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double)coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(MoistureContentUnitType unitType, double kgPerKgValue) {
         double convertionCoeff = (double)coeffTable[unitType];
         return kgPerKgValue / convertionCoeff;
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
