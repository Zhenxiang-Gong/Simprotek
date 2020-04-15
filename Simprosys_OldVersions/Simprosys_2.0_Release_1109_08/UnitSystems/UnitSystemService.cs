using System;
using System.Collections;

namespace Prosimo.UnitSystems
{
   public delegate void CurrentUnitSystemChangedEventHandler(UnitSystem unitSystem);

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class UnitSystemService 
   {
      public event CurrentUnitSystemChangedEventHandler CurrentUnitSystemChanged;

		private static UnitSystemService self;
      private UnitSystemCatalog catalog;
      private UnitConverter unitConverter;

      public UnitSystem CurrentUnitSystem
      {
         get {return this.unitConverter.UnitSystem;}
         set {
            //set the current unit system directly to the unit converter
            this.unitConverter.UnitSystem = value;
            OnCurrentUnitSystemChanged(this.unitConverter.UnitSystem);
         }
      }

      private UnitSystemService() {
         catalog = new UnitSystemCatalog();
         unitConverter = new UnitConverter();
      }

      public static UnitSystemService GetInstance() {
         if (self == null) {
            self = new UnitSystemService();
         }
         return self;
      }

      public UnitSystemCatalog GetUnitSystemCatalog() {
         return catalog;
      }

      public double ConvertToSIValue(PhysicalQuantity varType, double toBeConverted) {
         return unitConverter.ConvertToSIValue (varType, toBeConverted);
      }

      public double ConvertFromSIValue(PhysicalQuantity varType, double toBeConverted) {
         return unitConverter.ConvertFromSIValue (varType, toBeConverted);
      }

      public string GetUnitAsString(PhysicalQuantity varType) {
         return unitConverter.GetUnitAsString(varType);
      }

      public ICollection GetUnitsAsStrings(PhysicalQuantity varType) {
         return unitConverter.GetUnitsAsStrings(varType);
      }

      public ICollection GetPhysicalQuantitiesAsStrings() {
         return unitConverter.GetPhysicalQuantitiesAsStrings();
      }

      public string GetPhysicalQuantityAsString(PhysicalQuantity physicalQuantity) {
         return unitConverter.GetPhysicalQuantityAsString(physicalQuantity);
      }
      
      public PhysicalQuantity GetPhysicalQuantityAsEnum(string physicalQuantityStr) {
         return unitConverter.GetPhysicalQuantityAsEnum(physicalQuantityStr);
      }

      private void OnCurrentUnitSystemChanged(UnitSystem unitSystem) {
         if (CurrentUnitSystemChanged != null)
            CurrentUnitSystemChanged(unitSystem);
      }
	}
}

      //public ICollection GetUnitsAsStrings(PhysicalQuantity varType) {
      //   Type myType = pqTypeTable[varType] as Type;
      //   return myType.InvokeMember("GetUnitsAsStrings", BindingFlags.Static, null, null, null) as ICollection;
         /*ICollection list = null;
         if (varType == PhysicalQuantity.Area) {              
            list = AreaUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Density) {
            list = DensityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Diffusivity) {
            list = DiffusivityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.DynamicViscosity) {
            list = DynamicViscosityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Energy) {
            list = EnergyUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.FoulingFactor) {
            list = FoulingFactorUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Fraction) {
            list = FractionUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.HeatTransferCoefficient) {
            list = HeatTransferCoefficientUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.KinematicViscosity) {
            list = KinematicViscosityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Length) {
            list = LengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.LiquidHead) {
            list = LiquidHeadUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Mass) {
            list = MassUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MassFlowRate) {
            list = MassFlowRateUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MassVolumeConcentration) {
            list = MassVolumeConcentrationUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MicroLength) {
            list = MicroLengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.MoistureContent) {
            list = MoistureContentUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.PlaneAngle) {
            list = PlaneAngleUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Power) {
            list = PowerUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Pressure) {
            list = PressureUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SmallLength) {
            list = SmallLengthUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificHeat) {
            list = SpecificHeatUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificEnergy) {
            list = SpecificEnergyUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SpecificVolume) {
            list = SpecificVolumeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.SurfaceTension) {
            list = SurfaceTensionUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Temperature) {
            list = TemperatureUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.ThermalConductivity) {
            list = ThermalConductivityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Time) {
            list = TimeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Velocity) {
            list = VelocityUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.Volume) {
            list = VolumeUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeFlowRate) {
            list = VolumeFlowRateUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowGases) {
            list = VolumeRateFlowGasesUnit.GetUnitsAsStrings();
         }
         else if (varType == PhysicalQuantity.VolumeRateFlowLiquids) {
            list = VolumeRateFlowLiquidsUnit.GetUnitsAsStrings();
         }

         return list;*/
      //}


