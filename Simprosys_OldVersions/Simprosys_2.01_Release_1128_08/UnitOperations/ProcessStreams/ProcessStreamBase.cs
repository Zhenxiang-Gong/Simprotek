using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.ProcessStreams {
   /// <summary>
   /// Summary description for ProcessStreamBase.
   /// </summary>
   [Serializable]
   public abstract class ProcessStreamBase : Solvable {
      private const int CLASS_PERSISTENCE_VERSION = 2; 
      
      protected MaterialComponents materialComponents;

      protected UnitOperation upStreamOwner;
      protected UnitOperation downStreamOwner;
      
      protected ProcessVarDouble massFlowRate;
      protected ProcessVarDouble moleFlowRate;
      protected ProcessVarDouble volumeFlowRate;
      protected ProcessVarDouble vaporFraction;
      protected ProcessVarDouble pressure;
      protected ProcessVarDouble temperature;
      protected ProcessVarDouble specificEnthalpy;
      protected ProcessVarDouble specificHeat;
      protected ProcessVarDouble density;
      //protected ProcessVarDouble dynamicViscosity;
      //protected ProcessVarDouble thermalConductivity;

      //protected ProcessVarDouble specificVolume;
      //protected ProcessVarDouble boilingPoint;
      //protected ProcessVarDouble evaporationHeat;
      protected MaterialPropCalculator materialPropCalculator = MaterialPropCalculator.GetInstance();

      
      #region public properties
      
      public ProcessVarDouble MassFlowRate {
         get { return massFlowRate; }
      }
  
      public ProcessVarDouble MoleFlowRate {
         get { return moleFlowRate; }
      }

      public ProcessVarDouble VolumeFlowRate {
         get { return volumeFlowRate; }
      }
      
      public ProcessVarDouble VaporFraction {
         get { return vaporFraction; }
      }

      public ProcessVarDouble Pressure {
         get { return pressure;} 
         //this set is used only by PsychrometricChartModel
         set {pressure = value;}
      }
      
      public ProcessVarDouble Temperature {
         get { return temperature; }
      }

      public ProcessVarDouble SpecificEnthalpy {
         get { return specificEnthalpy; }
      }
      
      public ProcessVarDouble SpecificHeat {
         get { return specificHeat; }
      }
      
      public ProcessVarDouble Density {
         get { return density; }
      }
      
      //public ProcessVarDouble SpecificVolume {
      //   get { return specificVolume; }
      //}
      
      /*public ProcessVarDouble DynamicViscosity {
         get { 
            FluidPhase fp = (FluidPhase) StreamComponents.Phases[0];
            if (fp != null) {
               dynamicViscosity.Value = fp.GetViscosity(temperature.Value, pressure.Value);
            }
            return dynamicViscosity;
         }
      }
      
      public ProcessVarDouble ThermalConductivity {
         get { return thermalConductivity; }
      }*/
      
      //should not persist the following two properties since doing so
      //will result in circular reference
      public UnitOperation UpStreamOwner {
         get { return upStreamOwner; }
         set { upStreamOwner = value; }
      }

      public UnitOperation DownStreamOwner {
         get { return downStreamOwner; }
         set { downStreamOwner = value; }
      }

      public MaterialComponents Components {
         get {return materialComponents;}
         set {materialComponents = value;}
      }

      #endregion
      
      protected ProcessStreamBase(string name, MaterialComponents mComponents, UnitOperationSystem uoSys) : base(name, uoSys) {
         this.materialComponents  = mComponents;
         massFlowRate = new ProcessVarDouble(StringConstants.MASS_FLOW_RATE, PhysicalQuantity.MassFlowRate, VarState.Specified, this);
         moleFlowRate = new ProcessVarDouble(StringConstants.MOLE_FLOW_RATE, PhysicalQuantity.MoleFlowRate, VarState.Specified, this);
         volumeFlowRate = new ProcessVarDouble(StringConstants.VOLUME_FLOW_RATE, PhysicalQuantity.VolumeFlowRate, VarState.Specified, this);
         vaporFraction = new ProcessVarDouble(StringConstants.VAPOR_FRACTION, PhysicalQuantity.Fraction, VarState.Specified, this);
         pressure = new ProcessVarDouble(StringConstants.PRESSURE, PhysicalQuantity.Pressure, VarState.Specified, this);
         temperature = new ProcessVarDouble(StringConstants.TEMPERATURE, PhysicalQuantity.Temperature, VarState.Specified, this);
         specificEnthalpy = new ProcessVarDouble(StringConstants.SPECIFIC_ENTHALPY, PhysicalQuantity.SpecificEnergy, VarState.AlwaysCalculated, this);
         density = new ProcessVarDouble(StringConstants.DENSITY, PhysicalQuantity.Density, VarState.Specified, this);
         specificHeat = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT, PhysicalQuantity.SpecificHeat, VarState.AlwaysCalculated, this);
         //specificVolume = new ProcessVarDouble(StringConstants.SPECIFIC_VOLUME, PhysicalQuantity.SpecificVolume, VarState.AlwaysCalculated, this);
         //dynamicViscosity = new ProcessVarDouble(StringConstants.DYNAMIC_VISCOSITY, PhysicalQuantity.DynamicViscosity, VarState.Specified, this);
         //thermalConductivity = new ProcessVarDouble(StringConstants.THERMAL_CONDUCTIVITY, PhysicalQuantity.ThermalConductivity, VarState.Specified, this);
      }
      
      public bool CanConnect() {
         bool retValue = true;
         if (upStreamOwner != null && downStreamOwner != null) {
            retValue = false;
         }
         return retValue;
      }
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }
         if (pv == massFlowRate)  
         {
            if (aValue <= 0)  
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == volumeFlowRate)
         {
            if (aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == pressure) 
         {
            if (aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == temperature) 
         {
            if (aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == vaporFraction) 
         {
            if (aValue < 0.0 || aValue > 1.0) 
            {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
         }
         else if (pv == specificHeat) 
         {
            if (aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == density) 
         {
            if (aValue <= 0) 
            {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }

         return retValue;
      }

      protected ErrorMessage CheckSpecifiedValueInContextOfOwner(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         UnitOperation uo = this.downStreamOwner;
         if (uo != null && uo.IsBalanceCalcReady()) {
            retValue = uo.CheckSpecifiedValueInContext(pv, aValue);
         }
         if (retValue == null) {
            uo = this.upStreamOwner;
            if (uo != null && uo.IsBalanceCalcReady()) {
               retValue = uo.CheckSpecifiedValueInContext(pv, aValue);
            }
         }
         return retValue;
      }

      protected virtual void AdjustVarsStates() {
      }

      /*public virtual void Update(UnitOperation uo) {
         if (upStreamOwner != null && upStreamOwner != uo && upStreamOwner.SolveState != SolveState.Solved) {
            //if (upStreamOwner != null && upStreamOwner.SolveState != SolveState.Solved) {
            //upStreamOwner.Execute(false);
            //upStreamOwner.AllDone = false;
         }
         if (downStreamOwner != null && downStreamOwner != uo && downStreamOwner.SolveState != SolveState.Solved) {
            //if (downStreamOwner != null && downStreamOwner.SolveState != SolveState.Solved) {
            //downStreamOwner.Execute(false);
            //downStreamOwner.AllDone = false;
         }
         
         HasBeenModified(false);
      }*/

      internal virtual double GetBoilingPoint(double pressure) {
         return 0.0;
      }
      internal virtual double GetEvaporationHeat(double temperature) {
         return 0.0;
      }

      internal virtual double GetSolidCp(double temperature) {
         return 0.0;
      }

      internal virtual double GetLiquidCp(double temperature) {
         return materialPropCalculator.GetLiquidCp(materialComponents.Components, temperature);
      }

      internal virtual double GetGasCp(double temperature) {
         return materialPropCalculator.GetGasCp(materialComponents.Components, temperature);
      }
      
      internal virtual double GetSpecificHeatRatio(double temperature) 
      {
         return materialPropCalculator.GetSpecificHeatRatio(materialComponents.Components, temperature);
      }
      
      internal virtual double GetLiquidViscosity(double temperature) 
      {
         return materialPropCalculator.GetLiquidViscosity(materialComponents.Components, temperature);
      }

      internal virtual double GetGasViscosity(double temperature) {
         return materialPropCalculator.GetGasViscosity(materialComponents.Components, temperature);
      }
      
      internal virtual double GetLiquidThermalConductivity(double temperature) {
         return materialPropCalculator.GetLiquidThermalConductivity(materialComponents.Components, temperature);
      }

      internal virtual double GetGasThermalConductivity(double temperature) {
         return materialPropCalculator.GetGasThermalConductivity(materialComponents.Components, temperature);
      }
      
      internal virtual double GetLiquidDensity(double temperature) {
         return materialPropCalculator.GetLiquidDensity(materialComponents.Components, temperature);
      }

      internal virtual double GetGasDensity(double temperature, double pressure) {
         return materialPropCalculator.GetGasDensity(materialComponents.Components, temperature, pressure);
      }
      
      protected ProcessStreamBase(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData()
      {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionProcessStreamBase", typeof(int));
         if (persistedClassVersion >= 1) {
            this.materialComponents = RecallStorableObject("MaterialComponents", typeof(MaterialComponents)) as MaterialComponents;

            this.upStreamOwner = info.GetValue("UpStreamOwner", typeof(UnitOperation)) as UnitOperation;
            this.downStreamOwner = info.GetValue("DownStreamOwner", typeof(UnitOperation)) as UnitOperation;

            //this.specificVolume = (ProcessVarDouble)info.GetValue("SpecificVolume", typeof(ProcessVarDouble));
            //this.dynamicViscosity = info.GetValue("DynamicViscosity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            //this.thermalConductivity = info.GetValue("ThermalConductivity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.massFlowRate = RecallStorableObject("MassFlowRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.volumeFlowRate = RecallStorableObject("VolumeFlowRate", typeof(ProcessVarDouble)) as ProcessVarDouble; 
            this.vaporFraction = RecallStorableObject("VaporFraction", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.pressure = RecallStorableObject("Pressure", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.temperature = RecallStorableObject("Temperature", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.specificEnthalpy = RecallStorableObject("SpecificEnthalpy", typeof(ProcessVarDouble))as ProcessVarDouble;
            this.specificHeat = RecallStorableObject("SpecificHeat", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.density = RecallStorableObject("Density", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
         if (persistedClassVersion >= 2) {
            this.moleFlowRate = RecallStorableObject("MoleFlowRate", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }

      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionProcessStreamBase", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("MaterialComponents", this.materialComponents, typeof(MaterialComponents));
         
         info.AddValue("UpStreamOwner", this.upStreamOwner, typeof(UnitOperation));
         info.AddValue("DownStreamOwner", this.downStreamOwner, typeof(UnitOperation));
         
         info.AddValue("MassFlowRate", this.MassFlowRate, typeof(ProcessVarDouble));
         info.AddValue("VolumeFlowRate", this.VolumeFlowRate, typeof(ProcessVarDouble));
         info.AddValue("VaporFraction", this.VaporFraction, typeof(ProcessVarDouble));
         info.AddValue("Pressure", this.Pressure, typeof(ProcessVarDouble));
         info.AddValue("Temperature", this.Temperature, typeof(ProcessVarDouble));
         info.AddValue("SpecificEnthalpy", this.SpecificEnthalpy, typeof(ProcessVarDouble));
         info.AddValue("SpecificHeat", this.SpecificHeat, typeof(ProcessVarDouble));
         info.AddValue("Density", this.Density, typeof(ProcessVarDouble));
         //info.AddValue("SpecificVolume", this.SpecificVolume, typeof(ProcessVarDouble));
         //info.AddValue("DynamicViscosity", this.dynamicViscosity, typeof(ProcessVarDouble));
         //info.AddValue("ThermalConductivity", this.thermalConductivity, typeof(ProcessVarDouble));

         //version 2
         info.AddValue("MoleFlowRate", this.moleFlowRate, typeof(ProcessVarDouble));
      }
   }
}
