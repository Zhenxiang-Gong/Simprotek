using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MoistureContentUnitType { KgPerKg = 0, GramPerGram, IbPerIb, GrPerIb, GramPerKg };
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MoistureContentUnit : EngineeringUnit {
      public static MoistureContentUnit Instance = new MoistureContentUnit();

      public MoistureContentUnit() {
         coeffTable.Add(MoistureContentUnitType.KgPerKg, 1.0); unitStringTable.Add(MoistureContentUnitType.KgPerKg, "kg/kg");
         coeffTable.Add(MoistureContentUnitType.GramPerKg, 1.0e-3); unitStringTable.Add(MoistureContentUnitType.GramPerKg, "g/kg");
         coeffTable.Add(MoistureContentUnitType.GramPerGram, 1.0); unitStringTable.Add(MoistureContentUnitType.GramPerGram, "g/g");
         coeffTable.Add(MoistureContentUnitType.IbPerIb, 1.0); unitStringTable.Add(MoistureContentUnitType.IbPerIb, "Ib/Ib");
         coeffTable.Add(MoistureContentUnitType.GrPerIb, 1.0/7000.0); unitStringTable.Add(MoistureContentUnitType.GrPerIb, "gr/Ib");
      }
   }
}
