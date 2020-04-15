using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   //1 bbl = 42 U.S. Gallon
   public enum MassVolumeConcentrationUnitType {KgPerCubicMeter = 0, GramPerCubicMeter, 
      GramPerLitre, GramPerCubicDecimeter, GramPerGallonUS, GramPerGallonUK, 
      MilligramPerCubicMeter, MilligramPerCubicDecimeter, MilligramPerGallonUS,  
      PoundPerKilogallonUS, PoundPerKilogallonUK, PoundPerBbl, PoundPerKilobbl,
      GrainPerGallonUS, GrainPerCubicFoot, GrainPerHundredCubicFoot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MassVolumeConcentrationUnit {
      private static Hashtable coeffTable = new Hashtable();
      private static Hashtable unitStringTable = new Hashtable();

      static MassVolumeConcentrationUnit() {
         coeffTable.Add(MassVolumeConcentrationUnitType.KgPerCubicMeter, 1.0);                    unitStringTable.Add(MassVolumeConcentrationUnitType.KgPerCubicMeter, "kg/m3");
         coeffTable.Add(MassVolumeConcentrationUnitType.GramPerCubicMeter, 1.0e-3);               unitStringTable.Add(MassVolumeConcentrationUnitType.GramPerCubicMeter, "g/m3");
         coeffTable.Add(MassVolumeConcentrationUnitType.GramPerLitre, 1.0);                       unitStringTable.Add(MassVolumeConcentrationUnitType.GramPerLitre, "g/L");
         coeffTable.Add(MassVolumeConcentrationUnitType.GramPerCubicDecimeter, 1.0);              unitStringTable.Add(MassVolumeConcentrationUnitType.GramPerCubicDecimeter, "g/dm3");
         coeffTable.Add(MassVolumeConcentrationUnitType.GramPerGallonUS, 1.0/3.78541178);         unitStringTable.Add(MassVolumeConcentrationUnitType.GramPerGallonUS, "g/US gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.GramPerGallonUK, 1.0/4.54609);            unitStringTable.Add(MassVolumeConcentrationUnitType.GramPerGallonUK, "g/UK gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.MilligramPerCubicMeter, 1.0e-6);          unitStringTable.Add(MassVolumeConcentrationUnitType.MilligramPerCubicMeter, "mg/m3");
         coeffTable.Add(MassVolumeConcentrationUnitType.MilligramPerCubicDecimeter, 1.0e-3);      unitStringTable.Add(MassVolumeConcentrationUnitType.MilligramPerCubicDecimeter, "mg/dm3");
         coeffTable.Add(MassVolumeConcentrationUnitType.MilligramPerGallonUS, 1.0/3.78541178e3);  unitStringTable.Add(MassVolumeConcentrationUnitType.MilligramPerGallonUS, "mg/US gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.PoundPerKilogallonUS, 0.1198264);         unitStringTable.Add(MassVolumeConcentrationUnitType.PoundPerKilogallonUS, "Ib/1000 US gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.PoundPerKilogallonUK, 0.09977633);        unitStringTable.Add(MassVolumeConcentrationUnitType.PoundPerKilogallonUK, "Ib/1000 UK gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.PoundPerBbl, 2.853010177);                unitStringTable.Add(MassVolumeConcentrationUnitType.PoundPerBbl, "Ib/bbl");
         coeffTable.Add(MassVolumeConcentrationUnitType.PoundPerKilobbl, 2.853010177e-3);         unitStringTable.Add(MassVolumeConcentrationUnitType.PoundPerKilobbl, "Ib/1000 bbl");
         coeffTable.Add(MassVolumeConcentrationUnitType.GrainPerGallonUS, 1.7118061e-2);          unitStringTable.Add(MassVolumeConcentrationUnitType.GrainPerGallonUS, "grain/US gal");
         coeffTable.Add(MassVolumeConcentrationUnitType.GrainPerCubicFoot, 2.28835191e-3);        unitStringTable.Add(MassVolumeConcentrationUnitType.GrainPerCubicFoot, "grain/ft3");
         coeffTable.Add(MassVolumeConcentrationUnitType.GrainPerHundredCubicFoot, 2.28835191e-5); unitStringTable.Add(MassVolumeConcentrationUnitType.GrainPerHundredCubicFoot, "grain/100 ft3");
      }

      public static double ConvertToSIValue(MassVolumeConcentrationUnitType unitType, double toBeConvertedValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return convertionCoeff * toBeConvertedValue;
      }
      
      public static double ConvertFromSIValue(MassVolumeConcentrationUnitType unitType, double kgPerCuMValue) {
         double convertionCoeff = (double) coeffTable[unitType];
         return kgPerCuMValue/convertionCoeff;
      }
      
      public static string GetUnitAsString(MassVolumeConcentrationUnitType unitType) {
         return unitStringTable[unitType] as String;
      }
      
      public static MassVolumeConcentrationUnitType GetUnitAsEnum(string unitString) {
         IDictionaryEnumerator myEnumerator = unitStringTable.GetEnumerator();
         String name;
         MassVolumeConcentrationUnitType type = MassVolumeConcentrationUnitType.KgPerCubicMeter;
         while (myEnumerator.MoveNext()) {
            name = myEnumerator.Value as String;
            if (name.Equals(unitString)) {
               type = (MassVolumeConcentrationUnitType) myEnumerator.Key;
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
