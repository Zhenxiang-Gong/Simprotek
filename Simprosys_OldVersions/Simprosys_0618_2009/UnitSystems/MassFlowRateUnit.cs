using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum MassFlowRateUnitType {KgPerSec = 0, KgPerMin, KgPerHour, KgPerDay, 
      GramPerSec, GramPerMin, TonnePerHour, TonnePerDay, OuncePerSec, OuncePerMin, 
      PoundPerSec, PoundPerMin, PoundPerHour, TonUKPerMin, TonUKPerHour, TonUKPerDay, 
      TonUSPerMin, TonUSPerHour, TonUSPerDay, TonUKPerYear, TonUSPerYear,  
      MillionPoundPerYear};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class MassFlowRateUnit : EngineeringUnit {
      public static MassFlowRateUnit Instance = new MassFlowRateUnit();

      private MassFlowRateUnit() {
         coeffTable.Add(MassFlowRateUnitType.KgPerSec, 1.0); unitStringTable.Add(MassFlowRateUnitType.KgPerSec, "kg/s");
         coeffTable.Add(MassFlowRateUnitType.KgPerMin, 1.0/60.0);            unitStringTable.Add(MassFlowRateUnitType.KgPerMin, "kg/min");
         coeffTable.Add(MassFlowRateUnitType.KgPerHour, 1.0/3600.0);         unitStringTable.Add(MassFlowRateUnitType.KgPerHour, "kg/h");
         coeffTable.Add(MassFlowRateUnitType.KgPerDay, 1.0/86400);           unitStringTable.Add(MassFlowRateUnitType.KgPerDay, "kg/day");
         coeffTable.Add(MassFlowRateUnitType.GramPerSec, 1.0e-3);            unitStringTable.Add(MassFlowRateUnitType.GramPerSec, "g/s");
         coeffTable.Add(MassFlowRateUnitType.GramPerMin, 1.0/60000);         unitStringTable.Add(MassFlowRateUnitType.GramPerMin, "g/min");
         coeffTable.Add(MassFlowRateUnitType.TonnePerHour, 1.0/3.6);         unitStringTable.Add(MassFlowRateUnitType.TonnePerHour, "tonne/h");
         coeffTable.Add(MassFlowRateUnitType.TonnePerDay, 1.0/86.4);         unitStringTable.Add(MassFlowRateUnitType.TonnePerDay, "tonne/day");
         coeffTable.Add(MassFlowRateUnitType.OuncePerSec, 0.0283495231);     unitStringTable.Add(MassFlowRateUnitType.OuncePerSec, "oz/s");
         coeffTable.Add(MassFlowRateUnitType.OuncePerMin, 4.72492052e-4);    unitStringTable.Add(MassFlowRateUnitType.OuncePerMin, "oz/min");
         coeffTable.Add(MassFlowRateUnitType.PoundPerSec, 0.45359237);       unitStringTable.Add(MassFlowRateUnitType.PoundPerSec, "Ib/s");
         coeffTable.Add(MassFlowRateUnitType.PoundPerMin, 7.55987283e-3);    unitStringTable.Add(MassFlowRateUnitType.PoundPerMin, "Ib/min");
         coeffTable.Add(MassFlowRateUnitType.PoundPerHour, 1.25997881e-4);   unitStringTable.Add(MassFlowRateUnitType.PoundPerHour, "Ib/h");
         coeffTable.Add(MassFlowRateUnitType.TonUKPerMin, 0.00470392);       unitStringTable.Add(MassFlowRateUnitType.TonUKPerMin, "UK ton/min");
         coeffTable.Add(MassFlowRateUnitType.TonUKPerHour, 0.282235252);     unitStringTable.Add(MassFlowRateUnitType.TonUKPerHour, "UK ton/h");
         coeffTable.Add(MassFlowRateUnitType.TonUKPerDay, 0.0117598022);     unitStringTable.Add(MassFlowRateUnitType.TonUKPerDay, "UK ton/day");
         coeffTable.Add(MassFlowRateUnitType.TonUSPerMin, 0.004199929);      unitStringTable.Add(MassFlowRateUnitType.TonUSPerMin, "US ton/min");
         coeffTable.Add(MassFlowRateUnitType.TonUSPerHour, 0.251995761);     unitStringTable.Add(MassFlowRateUnitType.TonUSPerHour, "US ton/h");
         coeffTable.Add(MassFlowRateUnitType.TonUSPerDay, 0.0104998234);     unitStringTable.Add(MassFlowRateUnitType.TonUSPerDay, "US ton/day");
         coeffTable.Add(MassFlowRateUnitType.TonUKPerYear, 3.221864e-5);     unitStringTable.Add(MassFlowRateUnitType.TonUKPerYear, "UK ton/year");
         coeffTable.Add(MassFlowRateUnitType.TonUSPerYear, 2.876664e-5);     unitStringTable.Add(MassFlowRateUnitType.TonUSPerYear, "US ton/year");
         coeffTable.Add(MassFlowRateUnitType.MillionPoundPerYear, 5.249912); unitStringTable.Add(MassFlowRateUnitType.MillionPoundPerYear, "million Ib/year");
      }
   }
}
