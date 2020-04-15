using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum HeatFluxUnitType {WattPerSquareMeter = 0, KilowattPerSquareMeter, 
      BtuPerSecSquareFoot, BtuPerHourSquareFoot, CaloryPerSecSquareCentimeter};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class HeatFluxUnit : EngineeringUnit {
      public static HeatFluxUnit Instance = new HeatFluxUnit();

      public HeatFluxUnit() {
         coeffTable.Add(HeatFluxUnitType.WattPerSquareMeter, 1.0);               unitStringTable.Add(HeatFluxUnitType.WattPerSquareMeter, "W/m2");
         coeffTable.Add(HeatFluxUnitType.KilowattPerSquareMeter, 1000.0);        unitStringTable.Add(HeatFluxUnitType.KilowattPerSquareMeter, "kW/m2");
         coeffTable.Add(HeatFluxUnitType.BtuPerSecSquareFoot, 1.135e4);          unitStringTable.Add(HeatFluxUnitType.BtuPerSecSquareFoot, "Btu/s.ft2");
         coeffTable.Add(HeatFluxUnitType.BtuPerHourSquareFoot, 3.152);           unitStringTable.Add(HeatFluxUnitType.BtuPerHourSquareFoot, "Btu/h.ft2");
         coeffTable.Add(HeatFluxUnitType.CaloryPerSecSquareCentimeter, 4.184e4); unitStringTable.Add(HeatFluxUnitType.CaloryPerSecSquareCentimeter, "cal/s.cm2");
      }
   }
}
