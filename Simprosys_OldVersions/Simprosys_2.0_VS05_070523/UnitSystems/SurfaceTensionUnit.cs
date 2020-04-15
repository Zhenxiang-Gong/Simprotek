using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public enum SurfaceTensionUnitType {NewtonPerMeter = 0, DynePerCentimeter, 
      GramfPerCentimeter, KgfPerMeter, IbfPerFoot};
   
   /// <summary>
	/// Summary description for Class2.
	/// </summary>
   public class SurfaceTensionUnit : EngineeringUnit {
      public static SurfaceTensionUnit Instance = new SurfaceTensionUnit();

      public SurfaceTensionUnit() {
         coeffTable.Add(SurfaceTensionUnitType.NewtonPerMeter, 1.0);           unitStringTable.Add(SurfaceTensionUnitType.NewtonPerMeter, "N/m");
         coeffTable.Add(SurfaceTensionUnitType.DynePerCentimeter, 1.0e-3);     unitStringTable.Add(SurfaceTensionUnitType.DynePerCentimeter, "dyne/cm");
         coeffTable.Add(SurfaceTensionUnitType.GramfPerCentimeter, 0.980665);  unitStringTable.Add(SurfaceTensionUnitType.GramfPerCentimeter, "gf/cm");
         coeffTable.Add(SurfaceTensionUnitType.KgfPerMeter, 9.80665);          unitStringTable.Add(SurfaceTensionUnitType.KgfPerMeter, "kgf/m");
         coeffTable.Add(SurfaceTensionUnitType.IbfPerFoot, 14.592);            unitStringTable.Add(SurfaceTensionUnitType.IbfPerFoot, "Ibf/ft");
      }
   }
}
