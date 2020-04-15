using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MoleFlowRateUnitType { KmolePerSec = 0, KmolePerMin, KmolePerHour, KmolePerDay, IbMolePerSec, IbMolePerMin, IbMolePerHour, IbMolePerDay, MillionScfPerDay };
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MoleFlowRateUnit : EngineeringUnit {

      public static MoleFlowRateUnit Instance = new MoleFlowRateUnit();

      private MoleFlowRateUnit() {
         coeffTable.Add(MoleFlowRateUnitType.KmolePerSec, 1.0); unitStringTable.Add(MoleFlowRateUnitType.KmolePerSec, "kmole/s");
         coeffTable.Add(MoleFlowRateUnitType.KmolePerMin, 1/60.0); unitStringTable.Add(MoleFlowRateUnitType.KmolePerMin, "kmole/min");
         coeffTable.Add(MoleFlowRateUnitType.KmolePerHour, 1/3600.0); unitStringTable.Add(MoleFlowRateUnitType.KmolePerHour, "kmole/h");
         coeffTable.Add(MoleFlowRateUnitType.KmolePerDay, 1/86400.0); unitStringTable.Add(MoleFlowRateUnitType.KmolePerDay, "kmole/day");
         coeffTable.Add(MoleFlowRateUnitType.IbMolePerSec, 0.4535924); unitStringTable.Add(MoleFlowRateUnitType.IbMolePerSec, "Ib.mole/sec");
         coeffTable.Add(MoleFlowRateUnitType.IbMolePerMin, 0.4535924/60); unitStringTable.Add(MoleFlowRateUnitType.IbMolePerMin, "Ib.mole/Min");
         coeffTable.Add(MoleFlowRateUnitType.IbMolePerHour, 1.259979e-4); unitStringTable.Add(MoleFlowRateUnitType.IbMolePerHour, "Ib.mole/h");
         coeffTable.Add(MoleFlowRateUnitType.IbMolePerDay, 0.4535924 /86400.0); unitStringTable.Add(MoleFlowRateUnitType.IbMolePerDay, "Ib.mole/day");
         coeffTable.Add(MoleFlowRateUnitType.MillionScfPerDay, 1.38345e-2); unitStringTable.Add(MoleFlowRateUnitType.MillionScfPerDay, "million scf/day");
      }
   }
}
