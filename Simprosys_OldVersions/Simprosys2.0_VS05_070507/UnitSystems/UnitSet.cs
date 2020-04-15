using System;

namespace Drying.UnitSystems
{
   public enum PhysicalQuantity {Length, Area, Volume, Mass, Temperature, Density, 
      Pressure, Power, Energy, MassFlowRate, VolumeFlowRate, SpecificEnergy, SpecifcHeat, 
      DynamicViscosity, KinematicViscosity, ThermalConductivity, HeatTransferCoefficient,
      SurfaceTension, HeatFluxDensity, MoistureContent, RelativeHumidity, Diffusivity, 
      Time, Fraction, SepcificVolume, Unknown};
   
   
   public enum LengthUnit {Meter, Micrometer, Millimeter, Centimeter, Decimter, Killometer,
                  Inch, Foot, Yard, Chain, Furlong, Mile, Fathom, Nauticalmile};
   public enum AreaUnit {SquareMeter, SquareMillimeter, SquareCentimeter, Are, Hectare, SquareKillometer,
                  Barn, CircularMil, CircularInch, SquareInch, SquareFoot, SquareYard, Acre, SquareMile};
   public enum VolumeUnit {CubicMeter, Millilitre, Centilitre, Decilitre, Litre, FluidOunceUS, 
                  PintLiquidUS, GallonLiquidUS, PintDryUS, GallonDryUS, BushelDryUS,
                  CubicInch, CubicFoot, CubicYard, FluidOunceUK, GillUK, PintUK, GallonUK, Barrel};
   public enum MassUnit {Kilogram, Carat, Microgram, Milligram, Gram, Tonne, TroyOunce, Grain, Slug, 
                  Ounce, Pound, Stone, Hundredweight, TonUK, TonUS};
   public enum TemperatureUnit {Kelvin, Celsius, Fahrenheit, Reaumur, Rankine};
   public enum PressureUnit {Pascal, Kilopascal, Megapascal, MillimeterOfWater, MillimeterOfMercury, 
                  KgfPerSquareCentimeter, TonnefPerSquareMeter, NewtonPerSquareCentimeter, NewtonPerSquareMeter, 
                  KilonewtonPerSquareMeter, Millibar, Bar, Atmosphere, InchOfWater, InchOfMercury, 
                  FootOfWater, IbfPerSquareInch, IbfPerSquareFoot, TonfUKPerSquareFoot, TonfUSPerSquareFoot};
   public enum DensityUnit {KgPerCubicMeter, GramPerMillilitre, KgPerLitre, TonnePerCubicMeter,
                  GramPerLitre, OuncePerCubicInch, PoundPerCubicInch, PoundPerCubicFoot,
                  TonUKPerCubicYard, PoundPerCubiCubicYard, OuncePerGallonUK, PoundPerGallonUK, TonUSPerCubicYard,
                  OuncePerGallonUS, PoundPerGallonUS};
   public enum PowerUnit {Watt, Milliwatt, Kilowatt, Megawatt, Gigawatt, Terawatt, JoulePerSec, 
                  KiloJoulePerMin, MegaJoulePerHour, KgfMeterPerSec, FootIbfPerSec, BtuPerSec, 
                  BtuPerMin, BtuPerHour, ThermPerHour, CaloryPerSec, CaloryPerMin, KilocaloryPerSec, 
                  KilocaloryPerMin, KilocaloryPerHour, HorsepowerElectric, HorsepowerMectric};
   public enum EnergyUnit {Joule, Kilojoule, Megajoule, Gigajoule, KgfMeter, Calory, KiloCalory, Erg,
                  WattHour, KilowattHour, MegawattHour, Btu, Therm, FootIbf, FootPundal,
                  HorsePowerHour}; 
   public enum MassFlowRateUnit {KgPerSec, GramPerSec, GramPerMin, KgPerMin, KgPerHour, KgPerDay, 
                  TonnePerHour, TonnePerDay, OuncePerSec, OuncePerMin, PoundPerSec, PoundPerMin, PoundPerHour,
                  TonUKPerHour, TonUKPerDay, TonUSPerHour, TonUSPerDay};
   public enum VolumeFlowRateUnit {CubicMeterPerSec, LitrePerSec, LitrePerMin, LitrePerHour,
                  LitrePerDay, CubicMeterPerMin, CubicMeterPerHour, CubicMeterPerDay,
                  CubicInchPerSec, CubicInchPerMin, CubicFootPerSec, CubicFootPerMin, CubicFootPerHour, 
                  CubicYardPerHour, CubicYardPerDay, GallonUKPerMin, GallonUKPerHour, GallonUKPerDay, 
                  GallonUSPerMin, GallonUSPerHour, GallonUSPerDay, BarrelPerMin, BarrelPerHour, 
                  BarrelPerDay};
   public enum SpecificEnergyUnit {JoulePerKg, KilojoulePerKg, JoulePerGram, KgfMeterPerKg, 
                  CaloryPerGram, KilocaloryPerKg, KilojoulePerPound, ErgPerGram, KilowattHourPerPound,
                  BtuPerPound, ThermPerPound, FootIbfPerPound, KilocaloryPerPound, 
                  HorsePowerHourPerPound, KilowattHourPerKg, BtuPerKg}; 
   public enum SpecificHeatUnit {JoulePerKgKelvin, JoulePerGramKelvin, KilojoulePerKgKelvin, 
                  MegajoulePerKgKelvin, CaloryPerGramKelvin, KilocaloryPerKgKelvin, 
                  BtuPerPoundFahrenheit, BtuPerPoundCelcius, FootIbfPerPoundFahrenheit, 
                  FootIbfPerPoundCelsius, KgfMeterPerKgCelsius, JoulePerPoundCelsius};
   public enum DynamicViscosityUnit {PascalSecond, Poiseuille, Poise, Centipoise, KgfSecPerSquareMeter, 
                  GramfSecPerSquareCentimeter, KgPerMeterHour, DynSecPerSquareCentimeter, 
                  IbfSecPerSquareInch, IbfSecPerSquareFoot, IbfHourPerSquareFoot, PoundPerFootSec, 
                  PoundPerFootHour, SlugPerFootSec, SlugHourPerFootSquareSec, PoundalSecPerSquareFoot}; 
   public enum KinematicViscosityUnit {SquareMeterPerSec, Centistoke, Stoke, SquareCentimeterPerMin, 
                  SquareCentimeterPerHour, SquareMeterPerMin, SquareMeterPerHour, SquareInchPerSec,
                  SquareInchPerMin, SquareInchPerHour, SquareFootPerSec, SquareFootPerMin, SquareFootPerHour, 
                  PoiseCubicFootPerPound};
   public enum ThermalConductivityUnit {WattPerMeterKelvin, WattPerCentimeterKelvin, JoulePerCentimeterSecKelvin, 
                  CaloryPerCentimeterSecKelvin, KilocaloryPerCentimeterMinKelvin, KilocaloryPerMeterHourKelvin, 
                  WattPerInchFahrenheit, WattPerFootFahrenheit, 
                  BtuInchPerSqrtFootHourFahrenheit, BtuPerInchHourFahrenheit, BtuPerFootSecFahrenheit, 
                  BtuPerFootHourFahrenheit};
   public enum HeatTransferCoefficientUnit {WattPerSquareMeterKelvin, KilocaloryPerSquareMHourCelsius, 
                  CaloryPerSquareCentimeterSecCelsius, BtuPerSquareFootHourFahrenheit};
   public enum SurfaceTensionUnit {NewtonPerMeter, DynePerCentimeter, GramfPerCentimeter, KgfPerMeter, IbfPerFoot};
   public enum DiffusivityUnit {SquareMeterPerSec, SqrtCetimeterPerSec, SqMeterPerHour, SquareFootPerHour,
                  SquareInchPerSec};
   public enum MoistureContentUnit {KgPerKg, IbPerIb};
   public enum RatioUnit {Percent, Decimal};
   public enum ForceUnit {Newton, Kilonewton, Meganewton, Gramf, Kgf, Tonnef, Sthene, Ouncef, 
                  Poundf, Poundal, TonfUK, TonfUS, Kip, Dyne};
   public enum TimeUnit {Second, Minute, Hour, Day, Month, Year};
   public enum SpeedUnit {MeterPerSec, MillimeterPerSec, CentimeterPerSec, CentimeterPerMin, MeterPerMin,
                  MeterPerHour, MeterPerDay, KilometerPerSec, KilometerPerMin, 
                  KilometerPerHour, KilometerPerDay, InchPerSec, InchPerMin,
                  InchPerHour, FootPerSec, FootPerMin, FootPerHour, FootPerDay, MilePerSec, MilePerMin,
                  MilePerHour, MilePerDay, Knot};
   public enum HeatFluxDensityUnit {WattPerSquareMeter, WattPerSquareInch, WattPerSquareFoot, KilowattPerSquareFoot, 
                  BtuPerSquareFootSec, BtuPerSquareFootMin, BtuPerSquareFootHour, WattPerSquareCm, 
                  CaloryPerSquareCentimeterSec, KilocaloryPerSquareMeterMin, KilocaloryPerSquareMeterHour};
   public enum AmountOfSubstanceUnit {Mole, KiloMole};


   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class UnitSystem {
      string name;
      bool readOnly;
      TemperatureUnit temperature;
      PressureUnit pressure;
      MassFlowRateUnit massFlowRate;
      VolumeFlowRateUnit volumeFlowRate;
      MoistureContentUnit moistureContent;
      RatioUnit relativeHumidity;
      SpecificEnergyUnit enthalpy;
      SpecificHeatUnit specificHeat;
      EnergyUnit energy;
      PowerUnit power;
      DensityUnit density;
      DynamicViscosityUnit dynamicViscosity;
      KinematicViscosityUnit kinematicViscosity;
      ThermalConductivityUnit conductivity;
      DiffusivityUnit diffusivity;
      MassUnit mass;
      LengthUnit length;
      AreaUnit area;
      VolumeUnit volume;
      TimeUnit time;
      //HeatFluxDensityUnit heatTransferCoefficient;
      //ForceUnit force;
      //SurfaceTensionUnit surfaceTension;

      public UnitSystem(TemperatureUnit temperature, PressureUnit pressure, MassFlowRateUnit massFlowRate, 
            VolumeFlowRateUnit volumeFlowRate, MoistureContentUnit moistureContent, 
            RatioUnit relativeHumidity, SpecificEnergyUnit enthalpy, 
            SpecificHeatUnit specificHeat, EnergyUnit energy, PowerUnit power, DensityUnit density,
            DynamicViscosityUnit dynamicViscosity, KinematicViscosityUnit kinematicViscosity,
            ThermalConductivityUnit conductivity, DiffusivityUnit diffusivity, MassUnit mass,
            LengthUnit length, AreaUnit area, VolumeUnit volume, TimeUnit Time, string name) { //ep
         this.temperature = temperature;
         this.pressure = pressure;
         this.massFlowRate = massFlowRate;
         this.volumeFlowRate = volumeFlowRate;
         this.moistureContent = moistureContent;
         this.relativeHumidity = relativeHumidity;
         this.enthalpy = enthalpy;
         this.specificHeat = specificHeat;
         this.energy = energy;
         this.power = power;
         this.density = density;
         this.dynamicViscosity = dynamicViscosity;
         this.kinematicViscosity = kinematicViscosity;
         this.conductivity = conductivity;
         this.diffusivity = diffusivity;
         this.mass = mass;
         this.length = length;
         this.area = area;
         this.volume = volume;
         this.time = time;
         this.Name = name; //ep
         this.readOnly = false; //ep
      }

      public UnitSystem() {
         this.temperature = TemperatureUnit.Celsius;
         this.pressure = PressureUnit.Pascal;
         this.massFlowRate = MassFlowRateUnit.KgPerSec;
         this.volumeFlowRate = VolumeFlowRateUnit.CubicMeterPerSec;
         this.moistureContent = MoistureContentUnit.KgPerKg;
         this.relativeHumidity = RatioUnit.Percent;
         this.enthalpy = SpecificEnergyUnit.JoulePerKg;
         this.specificHeat = SpecificHeatUnit.JoulePerKgKelvin;
         this.energy = EnergyUnit.Joule;
         this.power = PowerUnit.JoulePerSec;
         this.density = DensityUnit.KgPerCubicMeter;
         this.dynamicViscosity = DynamicViscosityUnit.PascalSecond;
         this.kinematicViscosity = KinematicViscosityUnit.SquareMeterPerSec;
         this.conductivity = ThermalConductivityUnit.WattPerMeterKelvin;
         this.diffusivity = DiffusivityUnit.SquareMeterPerSec;
         this.mass = MassUnit.Kilogram;
         this.length = LengthUnit.Meter;
         this.area = AreaUnit.SquareMeter;
         this.volume = VolumeUnit.CubicMeter;
         this.time = TimeUnit.Second;
         this.Name = "SI"; //ep
         this.readOnly = true;
      }
      
      public override string ToString() //ep
      {
         return this.Name;
      }

      public string Name {
         get {return name;}
         set {name = value;}
      }

      public bool IsReadOnly //ep
      {
         get {return readOnly;}
         set {readOnly = value;}
      }

      public TemperatureUnit TemperatureUnit 
      {
         get {return temperature;}
         set {temperature = value;}
      }
      
      public PressureUnit PressureUnit {
         get {return pressure;}
         set {pressure = value;}
      }

      public MassFlowRateUnit MassFlowRateUnit {
         get {return massFlowRate;}
         set {massFlowRate = value;}
      }
      
      public VolumeFlowRateUnit VolumeFlowRateUnit {
         get {return volumeFlowRate;}
         set {volumeFlowRate = value;}
      }

      public MoistureContentUnit MoistureContentUnit {
         get {return moistureContent;}
         set {moistureContent = value;}
      }

      public RatioUnit RelativeHumidityUnit {
         get {return relativeHumidity;}
         set {relativeHumidity = value;}
      }

      public SpecificEnergyUnit SpecificEnergyUnit {
         get {return enthalpy;}
         set {enthalpy = value;}
      }

      public SpecificHeatUnit SpecificHeatUnit {
         get {return specificHeat;}
         set {specificHeat = value;}
      }

      public EnergyUnit EnergyUnit {
         get {return energy;}
         set {energy = value;}
      }

      public PowerUnit PowerUnit {
         get {return power;}
         set {power = value;}
      }

      public DensityUnit DensityUnit {
         get {return density;}
         set {density = value;}
      }
      
      public DynamicViscosityUnit DynamicViscosityUnit {
         get {return dynamicViscosity;}
         set {dynamicViscosity = value;}
      }
      
      public KinematicViscosityUnit KinematicViscosityUnit {
         get {return kinematicViscosity;}
         set {kinematicViscosity = value;}
      }

      public ThermalConductivityUnit ThermalConductivityUnit {
         get {return conductivity;}
         set {conductivity = value;}
      }

      public DiffusivityUnit DiffusivityUnit {
         get {return diffusivity;}
         set {diffusivity = value;}
      }
      
      public LengthUnit LengthUnit {
         get {return length;}
         set {length = value;}
      }

      public MassUnit MassUnit {
         get {return mass;}
         set {mass = value;}
      }

      public TimeUnit TimeUnit {
         get {return time;}
         set {time = value;}
      }

      public AreaUnit AreaUnit {
         get {return area;}
         set {area = value;}
      }
      
      public VolumeUnit VolumeUnit {
         get {return volume;}
         set {volume = value;}
      }
      
      //public AmountOfSubstanceUnit AmountOfSubstanceUnit {
      //   get {return amountOfSubstance;}
      //   set {amountOfSubstance = value;}
      //}
   }
}
