using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum TimeUnitType {Second = 0, Minute, Hour, Day, Week, Year};

   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class TimeUnit : EngineeringUnit {
      public static TimeUnit Instance = new TimeUnit();

      private TimeUnit() {
         coeffTable.Add(TimeUnitType.Second, 1.0);    unitStringTable.Add(TimeUnitType.Second, "s");
         coeffTable.Add(TimeUnitType.Minute, 60.0);   unitStringTable.Add(TimeUnitType.Minute, "min");
         coeffTable.Add(TimeUnitType.Hour, 3600.0);   unitStringTable.Add(TimeUnitType.Hour, "h");
         coeffTable.Add(TimeUnitType.Day, 86400.0);   unitStringTable.Add(TimeUnitType.Day, "day");
         coeffTable.Add(TimeUnitType.Week, 604800.0); unitStringTable.Add(TimeUnitType.Week, "week");
         coeffTable.Add(TimeUnitType.Year, 3.1536e7); unitStringTable.Add(TimeUnitType.Year, "year");
      }
   }
}
