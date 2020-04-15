using System;

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitSystems {
   //public enum AmountOfSubstanceUnit {Mole = 0, KiloMole};
   
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   [Serializable]
   public class UnitSystem : ISerializable
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      string name;
      bool readOnly;
      TemperatureUnitType temperature;
      PressureUnitType pressure;
      LiquidHeadUnitType liquidHead;
      MassFlowRateUnitType massFlowRate;
      VolumeFlowRateUnitType volumeFlowRate;
      VolumeRateFlowLiquidsUnitType volumeRateFlowLiquids;
      VolumeRateFlowGasesUnitType volumeRateFlowGases;
      MoistureContentUnitType moistureContent;
      FractionUnitType fraction;
      SpecificEnergyUnitType enthalpy;
      SpecificEntropyUnitType entropy;
      SpecificHeatUnitType specificHeat;
      EnergyUnitType energy;
      PowerUnitType power;
      DensityUnitType density;
      SpecificVolumeUnitType specificVolume;
      DynamicViscosityUnitType dynamicViscosity;
      KinematicViscosityUnitType kinematicViscosity;
      ThermalConductivityUnitType conductivity;
      DiffusivityUnitType diffusivity;
      MassUnitType mass;
      LengthUnitType length;
      SmallLengthUnitType smallLength;
      MicroLengthUnitType microLength;
      AreaUnitType area;
      VolumeUnitType volume;
      VelocityUnitType velocity;
      MassVolumeConcentrationUnitType massVolumeConcentration;
      TimeUnitType time;
      HeatTransferCoefficientUnitType heatTransferCoefficient;
      VolumeHeatTransferCoefficientUnitType volumeHeatTransferCoefficient;
      FoulingFactorUnitType foulingFactor;
      SurfaceTensionUnitType surfaceTension;
      HeatFluxUnitType heatFlux;
      PlaneAngleUnitType planeAngle;

      public event UnitSystemChangedEventHandler UnitSystemChanged;

      public UnitSystem() 
      {
         this.name = "SI"; //ep
         this.temperature = TemperatureUnitType.Kelvin;
         this.pressure = PressureUnitType.Kilopascal;
         this.liquidHead = LiquidHeadUnitType.Meter;
         this.massFlowRate = MassFlowRateUnitType.KgPerHour;
         this.volumeFlowRate = VolumeFlowRateUnitType.CubicMeterPerHour;
         this.volumeRateFlowLiquids = VolumeRateFlowLiquidsUnitType.CubicMeterPerHour;
         this.volumeRateFlowGases = VolumeRateFlowGasesUnitType.CubicMeterPerHour;
         this.moistureContent = MoistureContentUnitType.KgPerKg;
         this.fraction = FractionUnitType.Percent;
         this.enthalpy = SpecificEnergyUnitType.KilojoulePerKg;
         this.entropy = SpecificEntropyUnitType.KilojoulePerKgKelvin;
         this.specificHeat = SpecificHeatUnitType.KilojoulePerKgKelvin;
         this.energy = EnergyUnitType.Kilojoule;
         this.power = PowerUnitType.Kilowatt;
         this.density = DensityUnitType.KgPerCubicMeter;
         this.specificVolume = SpecificVolumeUnitType.CubicMeterPerKg;
         this.dynamicViscosity = DynamicViscosityUnitType.PascalSecond;
         this.kinematicViscosity = KinematicViscosityUnitType.SquareMeterPerSec;
         this.conductivity = ThermalConductivityUnitType.WattPerMeterKelvin;
         this.diffusivity = DiffusivityUnitType.SquareMeterPerSec;
         this.velocity = VelocityUnitType.MeterPerSec;
         this.mass = MassUnitType.Kilogram;
         this.length = LengthUnitType.Meter;
         this.smallLength = SmallLengthUnitType.Centimeter;
         this.microLength = MicroLengthUnitType.Micrometer;
         this.heatTransferCoefficient = HeatTransferCoefficientUnitType.WattPerSquareMeterKelvin;
         this.volumeHeatTransferCoefficient = VolumeHeatTransferCoefficientUnitType.WattPerCubicMeterKelvin;
         this.foulingFactor = FoulingFactorUnitType.SquareMeterKelvinPerWatt;
         this.surfaceTension = SurfaceTensionUnitType.NewtonPerMeter;
         this.area = AreaUnitType.SquareMeter;
         this.volume = VolumeUnitType.CubicMeter;
         this.massVolumeConcentration = MassVolumeConcentrationUnitType.KgPerCubicMeter;
         this.time = TimeUnitType.Second;
         this.heatFlux = HeatFluxUnitType.WattPerSquareMeter;
         this.planeAngle = PlaneAngleUnitType.Radian;
         this.readOnly = true;
      }
      
      public UnitSystem(string name, bool readOnly, TemperatureUnitType temperature, 
            PressureUnitType pressure, LiquidHeadUnitType liquidHead, MassFlowRateUnitType massFlowRate, 
            VolumeFlowRateUnitType volumeFlowRate, VolumeRateFlowLiquidsUnitType volumeRateFlowLiquids, 
            VolumeRateFlowGasesUnitType volumeRateFlowGases, MoistureContentUnitType moistureContent, 
            FractionUnitType fraction, SpecificEnergyUnitType enthalpy, SpecificEntropyUnitType entropy,
            SpecificHeatUnitType specificHeat, EnergyUnitType energy, PowerUnitType power, DensityUnitType density,
            SpecificVolumeUnitType specificVolume, 
            DynamicViscosityUnitType dynamicViscosity, KinematicViscosityUnitType kinematicViscosity,
            ThermalConductivityUnitType conductivity, HeatTransferCoefficientUnitType heatTransferCoefficient, 
            VolumeHeatTransferCoefficientUnitType volumeHeatTransferCoefficient, FoulingFactorUnitType foulingFactor, HeatFluxUnitType heatFlux,
            DiffusivityUnitType diffusivity, SurfaceTensionUnitType surfaceTension, VelocityUnitType velocity, 
            MassUnitType mass, LengthUnitType length, SmallLengthUnitType smallLength, MicroLengthUnitType microLength, 
            AreaUnitType area, VolumeUnitType volume, MassVolumeConcentrationUnitType massVolumeConcentration, TimeUnitType time,
            PlaneAngleUnitType planeAngle) {
         this.name = name;
         this.temperature = temperature;
         this.pressure = pressure;
         this.liquidHead = liquidHead;
         this.massFlowRate = massFlowRate;
         this.volumeFlowRate = volumeFlowRate;
         this.volumeRateFlowLiquids = volumeRateFlowLiquids;
         this.volumeRateFlowGases = volumeRateFlowGases;
         this.moistureContent = moistureContent;
         this.fraction = fraction;
         this.enthalpy = enthalpy;
         this.entropy = entropy;
         this.specificHeat = specificHeat;
         this.energy = energy;
         this.power = power;
         this.density = density;
         this.specificVolume = specificVolume;
         this.dynamicViscosity = dynamicViscosity;
         this.kinematicViscosity = kinematicViscosity;
         this.conductivity = conductivity;
         this.diffusivity = diffusivity;
         this.heatTransferCoefficient = heatTransferCoefficient;
         this.volumeHeatTransferCoefficient = volumeHeatTransferCoefficient;
         this.foulingFactor = foulingFactor;
         this.heatFlux = heatFlux;
         this.surfaceTension = surfaceTension;
         this.velocity = velocity;
         this.mass = mass;
         this.length = length;
         this.smallLength = smallLength;
         this.microLength = microLength;
         this.area = area;
         this.volume = volume;
         this.massVolumeConcentration = massVolumeConcentration;
         this.time = time;
         this.planeAngle = planeAngle;
         this.readOnly = readOnly;
      }

      public UnitSystem Clone()
      {
         UnitSystem us = new UnitSystem();

         us.Name = this.name;
         us.temperature = this.temperature;
         us.pressure = this.pressure;
         us.liquidHead = this.liquidHead;
         us.massFlowRate = this.massFlowRate;
         us.volumeFlowRate = this.volumeFlowRate;
         us.volumeRateFlowLiquids = this.volumeRateFlowLiquids;
         us.volumeRateFlowGases = this.volumeRateFlowGases;
         us.moistureContent = this.moistureContent;
         us.fraction = this.fraction;
         us.enthalpy = this.enthalpy;
         us.entropy = this.entropy;
         us.specificHeat = this.specificHeat;
         us.energy = this.energy;
         us.power = this.power;
         us.density = this.density;
         us.specificVolume = this.specificVolume;
         us.dynamicViscosity = this.dynamicViscosity;
         us.kinematicViscosity = this.kinematicViscosity;
         us.conductivity = this.conductivity;
         us.diffusivity = this.diffusivity;
         us.heatTransferCoefficient = this.heatTransferCoefficient;
         us.volumeHeatTransferCoefficient = this.volumeHeatTransferCoefficient;
         us.foulingFactor = this.foulingFactor;
         us.heatFlux = this.heatFlux;
         us.surfaceTension = this.surfaceTension;
         us.velocity = this.velocity;
         us.mass = this.mass;
         us.length = this.length;
         us.smallLength = this.smallLength;
         us.microLength = this.microLength;
         us.area = this.area;
         us.volume = this.volume;
         us.massVolumeConcentration = this.massVolumeConcentration;
         us.time = this.time;
         us.planeAngle = this.planeAngle;
         us.readOnly = this.readOnly;

         return us;
      }

      public override string ToString() 
      {
         return this.Name;
      }

      public string Name {
         get {return name;}
         set {name = value;}
      }

      public bool IsReadOnly 
      {
         get {return readOnly;}
         set {readOnly = value;}
      }

      public TemperatureUnitType TemperatureUnitType 
      {
         get {return temperature;}
         set {temperature = value;}
      }
      
      public PressureUnitType PressureUnitType {
         get {return pressure;}
         set {pressure = value;}
      }

      public LiquidHeadUnitType LiquidHeadUnitType {
         get {return liquidHead;}
         set {liquidHead = value;}
      }

      public MassFlowRateUnitType MassFlowRateUnitType {
         get {return massFlowRate;}
         set {massFlowRate = value;}
      }
      
      public VolumeFlowRateUnitType VolumeFlowRateUnitType {
         get {return volumeFlowRate;}
         set {volumeFlowRate = value;}
      }
      
      public VolumeRateFlowLiquidsUnitType VolumeRateFlowLiquidsUnitType {
         get {return volumeRateFlowLiquids;}
         set {volumeRateFlowLiquids = value;}
      }

      public VolumeRateFlowGasesUnitType VolumeRateFlowGasesUnitType {
         get {return volumeRateFlowGases;}
         set {volumeRateFlowGases = value;}
      }

      public MoistureContentUnitType MoistureContentUnitType {
         get {return moistureContent;}
         set {moistureContent = value;}
      }

      public FractionUnitType FractionUnitType {
         get {return fraction;}
         set {fraction = value;}
      }

      public SpecificEnergyUnitType SpecificEnergyUnitType {
         get {return enthalpy;}
         set {enthalpy = value;}
      }

      public SpecificEntropyUnitType SpecificEntropyUnitType {
         get {return entropy;}
         set {entropy = value;}
      }

      public SpecificHeatUnitType SpecificHeatUnitType {
         get {return specificHeat;}
         set {specificHeat = value;}
      }

      public EnergyUnitType EnergyUnitType {
         get {return energy;}
         set {energy = value;}
      }

      public PowerUnitType PowerUnitType {
         get {return power;}
         set {power = value;}
      }

      public DensityUnitType DensityUnitType {
         get {return density;}
         set {density = value;}
      }

      public SpecificVolumeUnitType SpecificVolumeUnitType {
         get {return specificVolume;}
         set {specificVolume = value;}
      }
      
      public DynamicViscosityUnitType DynamicViscosityUnitType {
         get {return dynamicViscosity;}
         set {dynamicViscosity = value;}
      }
      
      public KinematicViscosityUnitType KinematicViscosityUnitType {
         get {return kinematicViscosity;}
         set {kinematicViscosity = value;}
      }

      public ThermalConductivityUnitType ThermalConductivityUnitType {
         get {return conductivity;}
         set {conductivity = value;}
      }

      public DiffusivityUnitType DiffusivityUnitType {
         get {return diffusivity;}
         set {diffusivity = value;}
      }
      
      public HeatTransferCoefficientUnitType HeatTransferCoefficientUnitType {
         get {return heatTransferCoefficient;}
         set {heatTransferCoefficient = value;}
      }

      public VolumeHeatTransferCoefficientUnitType VolumeHeatTransferCoefficientUnitType 
      {
         get {return volumeHeatTransferCoefficient;}
         set {volumeHeatTransferCoefficient = value;}
      }

      public FoulingFactorUnitType FoulingFactorUnitType 
      {
         get {return foulingFactor;}
         set {foulingFactor = value;}
      }

      public HeatFluxUnitType HeatFluxUnitType {
         get {return heatFlux;}
         set {heatFlux = value;}
      }

      public SurfaceTensionUnitType SurfaceTensionUnitType {
         get {return surfaceTension;}
         set {surfaceTension = value;}
      }
      
      public VelocityUnitType VelocityUnitType {
         get {return velocity;}
         set {velocity = value;}
      }
      
      public LengthUnitType LengthUnitType {
         get {return length;}
         set {length = value;}
      }
      
      public SmallLengthUnitType SmallLengthUnitType {
         get {return smallLength;}
         set {smallLength = value;}
      }

      public MicroLengthUnitType MicroLengthUnitType {
         get {return microLength;}
         set {microLength = value;}
      }
      
      public MassUnitType MassUnitType {
         get {return mass;}
         set {mass = value;}
      }

      public TimeUnitType TimeUnitType {
         get {return time;}
         set {time = value;}
      }

      public PlaneAngleUnitType PlaneAngleUnitType {
         get {return planeAngle;}
         set {planeAngle = value;}
      }
      
      public AreaUnitType AreaUnitType {
         get {return area;}
         set {area = value;}
      }
      
      public VolumeUnitType VolumeUnitType {
         get {return volume;}
         set {volume = value;}
      }
      
      public MassVolumeConcentrationUnitType MassVolumeConcentrationUnitType {
         get {return massVolumeConcentration;}
         set {massVolumeConcentration = value;}
      }
      
      //public AmountOfSubstanceUnit AmountOfSubstanceUnit {
      //   get {return amountOfSubstance;}
      //   set {amountOfSubstance = value;}
      //}

      public void Commit(UnitSystem us) {
         this.name = us.Name; //ep
         this.temperature = us.temperature;
         this.pressure = us.pressure;
         this.liquidHead = us.liquidHead;
         this.massFlowRate = us.massFlowRate;
         this.volumeFlowRate = us.volumeFlowRate;
         this.volumeRateFlowLiquids = us.volumeRateFlowLiquids;
         this.volumeRateFlowGases = us.volumeRateFlowGases;
         this.moistureContent = us.moistureContent;
         this.fraction = us.fraction;
         this.enthalpy = us.enthalpy;
         this.entropy = us.entropy;
         this.specificHeat = us.specificHeat;
         this.energy = us.energy;
         this.power = us.power;
         this.density = us.density;
         this.specificVolume = us.specificVolume;
         this.dynamicViscosity = us.dynamicViscosity;
         this.kinematicViscosity = us.kinematicViscosity;
         this.conductivity = us.conductivity;
         this.diffusivity = us.diffusivity;
         this.heatTransferCoefficient = us.heatTransferCoefficient;
         this.volumeHeatTransferCoefficient = us.volumeHeatTransferCoefficient;
         this.foulingFactor = us.foulingFactor;
         this.heatFlux = us.heatFlux;
         this.surfaceTension = us.surfaceTension;
         this.velocity = us.velocity;
         this.mass = us.mass;
         this.length = us.length;
         this.smallLength = us.smallLength;
         this.microLength = us.microLength;
         this.area = us.area;
         this.volume = us.volume;
         this.massVolumeConcentration = us.massVolumeConcentration;

         this.time = us.time;
         this.planeAngle = us.planeAngle;

         this.OnUnitSystemChanged(this);
      }

      private void OnUnitSystemChanged(UnitSystem unitSystem) {
         if (UnitSystemChanged != null)
            UnitSystemChanged(unitSystem);
      }

      protected UnitSystem(SerializationInfo info, StreamingContext context) {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context) {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionUnitSystem", typeof(int));
         switch (persistedClassVersion) {
            case 1:
               this.name = (string)info.GetValue("Name", typeof(string));
               this.readOnly = (bool)info.GetValue("IsReadOnly", typeof(bool));
               this.area = (AreaUnitType)info.GetValue("AreaUnit", typeof(AreaUnitType));
               this.density = (DensityUnitType)info.GetValue("DensityUnit", typeof(DensityUnitType));
               this.diffusivity = (DiffusivityUnitType)info.GetValue("DiffusivityUnit", typeof(DiffusivityUnitType));
               this.dynamicViscosity = (DynamicViscosityUnitType)info.GetValue("DynamicViscosityUnit", typeof(DynamicViscosityUnitType));
               this.energy = (EnergyUnitType)info.GetValue("EnergyUnit", typeof(EnergyUnitType));
               this.foulingFactor = (FoulingFactorUnitType)info.GetValue("FoulingFactorUnit", typeof(FoulingFactorUnitType));
               this.fraction = (FractionUnitType)info.GetValue("FractionUnit", typeof(FractionUnitType));
               this.heatFlux = (HeatFluxUnitType)info.GetValue("HeatFluxUnit", typeof(HeatFluxUnitType));
               this.heatTransferCoefficient = (HeatTransferCoefficientUnitType)info.GetValue("HeatTransferCoefficientUnit", typeof(HeatTransferCoefficientUnitType));
               this.volumeHeatTransferCoefficient = (VolumeHeatTransferCoefficientUnitType)info.GetValue("VolumeHeatTransferCoefficientUnit", typeof(VolumeHeatTransferCoefficientUnitType));
               this.kinematicViscosity = (KinematicViscosityUnitType)info.GetValue("KinematicViscosityUnit", typeof(KinematicViscosityUnitType));
               this.length = (LengthUnitType)info.GetValue("LengthUnit", typeof(LengthUnitType));
               this.liquidHead = (LiquidHeadUnitType)info.GetValue("LiquidHeadUnit", typeof(LiquidHeadUnitType));
               this.massFlowRate = (MassFlowRateUnitType)info.GetValue("MassFlowRateUnit", typeof(MassFlowRateUnitType));
               this.mass = (MassUnitType)info.GetValue("MassUnit", typeof(MassUnitType));
               this.massVolumeConcentration = (MassVolumeConcentrationUnitType)info.GetValue("MassVolumeConcentrationUnit", typeof(MassVolumeConcentrationUnitType));
               this.microLength = (MicroLengthUnitType)info.GetValue("MicroLengthUnit", typeof(MicroLengthUnitType));
               this.moistureContent = (MoistureContentUnitType)info.GetValue("MoistureContentUnit", typeof(MoistureContentUnitType));
               this.planeAngle = (PlaneAngleUnitType)info.GetValue("PlaneAngleUnit", typeof(PlaneAngleUnitType));
               this.power = (PowerUnitType)info.GetValue("PowerUnit", typeof(PowerUnitType));
               this.pressure = (PressureUnitType)info.GetValue("PressureUnit", typeof(PressureUnitType));
               this.smallLength = (SmallLengthUnitType)info.GetValue("SmallLengthUnit", typeof(SmallLengthUnitType));
               this.enthalpy = (SpecificEnergyUnitType)info.GetValue("SpecificEnergyUnit", typeof(SpecificEnergyUnitType));
               this.entropy = (SpecificEntropyUnitType)info.GetValue("SpecificEntropyUnit", typeof(SpecificEntropyUnitType));
               this.specificHeat = (SpecificHeatUnitType)info.GetValue("SpecificHeatUnit", typeof(SpecificHeatUnitType));
               this.specificVolume = (SpecificVolumeUnitType)info.GetValue("SpecificVolumeUnit", typeof(SpecificVolumeUnitType));
               this.surfaceTension = (SurfaceTensionUnitType)info.GetValue("SurfaceTensionUnit", typeof(SurfaceTensionUnitType));
               this.temperature = (TemperatureUnitType)info.GetValue("TemperatureUnit", typeof(TemperatureUnitType));
               this.conductivity = (ThermalConductivityUnitType)info.GetValue("ThermalConductivityUnit", typeof(ThermalConductivityUnitType));
               this.time = (TimeUnitType)info.GetValue("TimeUnit", typeof(TimeUnitType));
               this.velocity = (VelocityUnitType)info.GetValue("VelocityUnit", typeof(VelocityUnitType));
               this.volumeFlowRate = (VolumeFlowRateUnitType)info.GetValue("VolumeFlowRateUnit", typeof(VolumeFlowRateUnitType));
               this.volumeRateFlowGases = (VolumeRateFlowGasesUnitType)info.GetValue("VolumeRateFlowGasesUnit", typeof(VolumeRateFlowGasesUnitType));
               this.volumeRateFlowLiquids = (VolumeRateFlowLiquidsUnitType)info.GetValue("VolumeRateFlowLiquidsUnit", typeof(VolumeRateFlowLiquidsUnitType));
               this.volume = (VolumeUnitType)info.GetValue("VolumeUnit", typeof(VolumeUnitType));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionUnitSystem", UnitSystem.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("IsReadOnly", this.readOnly, typeof(bool));
         info.AddValue("AreaUnit", this.area, typeof(AreaUnitType));
         info.AddValue("DensityUnit", this.density, typeof(DensityUnitType));
         info.AddValue("DiffusivityUnit", this.diffusivity, typeof(DiffusivityUnitType));
         info.AddValue("DynamicViscosityUnit", this.dynamicViscosity, typeof(DynamicViscosityUnitType));
         info.AddValue("EnergyUnit", this.energy, typeof(EnergyUnitType));
         info.AddValue("FoulingFactorUnit", this.foulingFactor, typeof(FoulingFactorUnitType));
         info.AddValue("FractionUnit", this.fraction, typeof(FractionUnitType));
         info.AddValue("HeatFluxUnit", this.heatFlux, typeof(HeatFluxUnitType));
         info.AddValue("HeatTransferCoefficientUnit", this.heatTransferCoefficient, typeof(HeatTransferCoefficientUnitType));
         info.AddValue("VolumeHeatTransferCoefficientUnit", this.heatTransferCoefficient, typeof(VolumeHeatTransferCoefficientUnitType));
         info.AddValue("KinematicViscosityUnit", this.kinematicViscosity, typeof(KinematicViscosityUnitType));
         info.AddValue("LengthUnit", this.length, typeof(LengthUnitType));
         info.AddValue("LiquidHeadUnit", this.liquidHead, typeof(LiquidHeadUnitType));
         info.AddValue("MassFlowRateUnit", this.massFlowRate, typeof(MassFlowRateUnitType));
         info.AddValue("MassUnit", this.mass, typeof(MassUnitType));
         info.AddValue("MassVolumeConcentrationUnit", this.massVolumeConcentration, typeof(MassVolumeConcentrationUnitType));
         info.AddValue("MicroLengthUnit", this.microLength, typeof(MicroLengthUnitType));
         info.AddValue("MoistureContentUnit", this.moistureContent, typeof(MoistureContentUnitType));
         info.AddValue("PlaneAngleUnit", this.planeAngle, typeof(PlaneAngleUnitType));
         info.AddValue("PowerUnit", this.power, typeof(PowerUnitType));
         info.AddValue("PressureUnit", this.pressure, typeof(PressureUnitType));
         info.AddValue("SmallLengthUnit", this.smallLength, typeof(SmallLengthUnitType));
         info.AddValue("SpecificEnergyUnit", this.enthalpy, typeof(SpecificEnergyUnitType));
         info.AddValue("SpecificEntropyUnit", this.entropy, typeof(SpecificEntropyUnitType));
         info.AddValue("SpecificHeatUnit", this.specificHeat, typeof(SpecificHeatUnitType));
         info.AddValue("SpecificVolumeUnit", this.specificVolume, typeof(SpecificVolumeUnitType));
         info.AddValue("SurfaceTensionUnit", this.surfaceTension, typeof(SurfaceTensionUnitType));
         info.AddValue("TemperatureUnit", this.temperature, typeof(TemperatureUnitType));
         info.AddValue("ThermalConductivityUnit", this.conductivity, typeof(ThermalConductivityUnitType));
         info.AddValue("TimeUnit", this.time, typeof(TimeUnitType));
         info.AddValue("VelocityUnit", this.velocity, typeof(VelocityUnitType));
         info.AddValue("VolumeFlowRateUnit", this.volumeFlowRate, typeof(VolumeFlowRateUnitType));
         info.AddValue("VolumeRateFlowGasesUnit", this.volumeRateFlowGases, typeof(VolumeRateFlowGasesUnitType));
         info.AddValue("VolumeRateFlowLiquidsUnit", this.volumeRateFlowLiquids, typeof(VolumeRateFlowLiquidsUnitType));
         info.AddValue("VolumeUnit", this.volume, typeof(VolumeUnitType));
      }
   }
}
