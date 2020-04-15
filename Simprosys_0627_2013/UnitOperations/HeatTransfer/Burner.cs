using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
   //Air Composition--Detailed
   //                         By Volume         By Weight
   //Nitrogen        N2       78.084 %          75.47%
   //Oxygen          O2       20.9476 %         23.20%
   //Argon           Ar       0.934 %           1.28%
   //Carbon Dioxide  CO2      0.0314 %          0.046%
   //Neon            Ne       0.001818 %        0.0012%
   //Methane         CH4      0.0002 % 
   //Helium          He       0.000524 %        0.00007
   //Krypton         Kr       0.000114 %        0.0003%
   //Hydrogen        H2       0.00005 %         ~0%
   //Xenon           Xe       0.0000087 %       0.00004

   //Air Composition by volume--Simple
   //Nitrogen        N2       78% 
   //Oxygen          O2       21% 
   //Argon           Ar       1 % 

   //Natural gas compostioin
   //Methane               CH4             70-90% 
   //Ethane                C2H6            0-20% 
   //Propane               C3H8 
   //Butane                C4H10 
   //Carbon Dioxide        CO2             0-8% 
   //Oxygen                O2              0-0.2% 
   //Nitrogen              N2              0-5% 
   //Hydrogen sulphide     H2S             0-5% 
   //Rare gases            A, He, Ne, Xe   trace 

   //Component Typical Analysis      (mole %) Range(mole %)
   //Methane                          94.9     87.0 - 96.0
   //Ethane                           2.5      1.8 - 5.1
   //Propane                          0.2      0.1 - 1.5
   //iso-Butane                       0.03     0.01 - 0.3
   //normal-Butane                    0.03     0.01 - 0.3
   //iso-Pentane                      0.01     trace - 0.14
   //normal-Pentane                   0.01     trace - 0.04
   //Hexanes plus                     0.01     trace - 0.06
   //Nitrogen                         1.6      1.3 - 5.6
   //Carbon Dioxide                   0.7      0.1 - 1.0
   //Oxygen                           0.02     0.01 - 0.1
   //Hydrogen                         trace    trace - 0.02

   /// <summary>
   /// Summary description for Furnace.
   /// </summary>
   /// 
   [Serializable] 
   public class Burner : UnitOperation
   {
      #region Fields
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int FUEL_INLET_INDEX = 0;
      public static int AIR_INLET_INDEX = 1;
      public static int FLUE_GAS_OUTLET_INDEX = 2;

      private FuelStream fuelInlet;
      private DryingGasStream airInlet;
      private DryingGasStream flueGasOutlet;

      private ProcessVarDouble excessAir;
      private ProcessVarDouble totalHeatGeneration;
      private ProcessVarDouble percentageHeatLoss;
      private ProcessVarDouble oxygenMassFractionFlueGas;
      private ProcessVarDouble oxygenMoleFractionFlueGas;

      //convenient data members needed for calcuations
      private static SubstanceCatalog catalog = SubstanceCatalog.GetInstance();
      private static Substance carbon = catalog.GetSubstance(Substance.CARBON);
      private static Substance hydrogen = catalog.GetSubstance(Substance.HYDROGEN);
      private static Substance oxygen = catalog.GetSubstance(Substance.OXYGEN);
      private static Substance sulfur = catalog.GetSubstance(Substance.SULFUR);
      private static Substance air = catalog.GetSubstance(Substance.AIR);
      private static Substance carbonDioxide = catalog.GetSubstance(Substance.CARBON_DIOXIDE);
      private static Substance sulfurDioxide = catalog.GetSubstance(Substance.SULFUR_DIOXIDE);
      private static Substance water = catalog.GetSubstance(Substance.WATER);
      private static Substance nitrogen = catalog.GetSubstance(Substance.NITROGEN);
      private static Substance argon = catalog.GetSubstance(Substance.ARGON);
      //Substance ash = SubstanceCatalog.GetInstance().GetSubstance("ash");
      private static ThermalPropCalculator propCalculator = ThermalPropCalculator.Instance;
 
      private const double NITROGEN_MOLE_FRACTION_IN_AIR = 0.78084;
      private const double OXYGEN_MOLE_FRACTION_IN_AIR = 0.209476;
      private const double ARGON_MOLE_FRACTION_IN_AIR = 0.00934;
      //private const double AIR_MOLAR_MASS = 28.9626;
      #endregion

      #region Public Properties

      public FuelStream FuelInlet {
         get { return fuelInlet; }
      }

      public DryingGasStream AirInlet {
         get { return airInlet; }
      }

      public DryingGasStream FlueGasOutlet {
         get { return flueGasOutlet; }
      }

      //public ProcessVarDouble ExcessAir {
      //   get { return excessAir; }
      //}

      #endregion

      #region Constructors
      public Burner(string name, UnitOperationSystem uoSys)
         : base(name, uoSys)
      {
         excessAir = new ProcessVarDouble(StringConstants.EXCESS_AIR, PhysicalQuantity.Fraction, 0, VarState.Specified, this);
         totalHeatGeneration = new ProcessVarDouble(StringConstants.TOTAL_HEAT_GENERATED, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);
         percentageHeatLoss = new ProcessVarDouble(StringConstants.PERCENT_HEAT_LOSS, PhysicalQuantity.Fraction, 0, VarState.Specified, this);
         heatLoss.State = VarState.AlwaysCalculated;

         oxygenMassFractionFlueGas = new ProcessVarDouble(StringConstants.FLUE_GAS_OXYGEN_MASS_FRACTION, PhysicalQuantity.Fraction, 0, VarState.AlwaysCalculated, this);
         oxygenMoleFractionFlueGas = new ProcessVarDouble(StringConstants.FLUE_GAS_OXYGEN_MOLE_FRACTION, PhysicalQuantity.Fraction, 0, VarState.AlwaysCalculated, this);

         InitializeVarListAndRegisterVars();
      }
      #endregion

      #region Methods
      private void InitializeVarListAndRegisterVars()
      {
         AddVarOnListAndRegisterInSystem(excessAir);
         AddVarOnListAndRegisterInSystem(totalHeatGeneration);
         AddVarOnListAndRegisterInSystem(percentageHeatLoss);
         AddVarOnListAndRegisterInSystem(heatLoss);
         AddVarOnListAndRegisterInSystem(oxygenMassFractionFlueGas);
         AddVarOnListAndRegisterInSystem(oxygenMoleFractionFlueGas);
      }

      public override bool CanAttach(int streamIndex) {
         bool retValue = false;
         if (streamIndex == FUEL_INLET_INDEX && fuelInlet == null) {
            retValue = true;
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX && flueGasOutlet == null) {
            retValue = true;
         }
         else if (streamIndex == AIR_INLET_INDEX && airInlet == null) {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (((streamIndex == FUEL_INLET_INDEX || streamIndex == AIR_INLET_INDEX) && ps.DownStreamOwner != null)
            || ((streamIndex == FLUE_GAS_OUTLET_INDEX) && ps.UpStreamOwner != null)) {
            return false;
         }

         bool canAttach = false;
         if (ps is FuelStream && streamIndex == FUEL_INLET_INDEX) {
            canAttach = true;
         }
         else if (ps is DryingGasStream && streamIndex == FLUE_GAS_OUTLET_INDEX && flueGasOutlet == null) {
            canAttach = true;
         }
         else if (ps is DryingGasStream && streamIndex == AIR_INLET_INDEX && airInlet == null) {
            canAttach = true;
         }
         
         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = true;
         if (streamIndex == FUEL_INLET_INDEX) {
            fuelInlet = ps as FuelStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == AIR_INLET_INDEX) {
            airInlet = ps as DryingGasStream;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
         }
         else if (streamIndex == FLUE_GAS_OUTLET_INDEX) {
            flueGasOutlet = ps as DryingGasStream;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
         }
         else {
            attached = false;
         }
         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = true;
         if (ps == fuelInlet) {
            fuelInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == airInlet) {
            airInlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
         }
         else if (ps == flueGasOutlet) {
            flueGasOutlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
         }
         else {
            detached = false;
         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (fuelInlet == null || airInlet == null || flueGasOutlet == null) {
            isReady = false;
         }
         return isReady;
      }

      protected override bool IsSolveReady() {
         bool isReady = false;
         //AdjustVarsStates();

         if (airInlet.Temperature.HasValue && airInlet.WetBulbTemperature.HasValue &&
            (fuelInlet.MassFlowRate.HasValue || flueGasOutlet.MassFlowRate.HasValue) && fuelInlet.Pressure.HasValue && fuelInlet.Temperature.HasValue && 
            flueGasOutlet.Pressure.IsSpecifiedAndHasValue && 
            ((flueGasOutlet.Temperature.IsSpecifiedAndHasValue && 
            (percentageHeatLoss.IsSpecifiedAndHasValue || excessAir.IsSpecifiedAndHasValue))
            || (percentageHeatLoss.IsSpecifiedAndHasValue && excessAir.IsSpecifiedAndHasValue)))
         {
            isReady = true;
         }
         return isReady;
      }

      public override void Execute(bool propagate) {
         PreSolve();
         if (IsBalanceCalcReady() && IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }

      private void Solve()
      {
         double totalEenthalpyOfReactantInFuelInlet = 0;
         double moleFractionCarbon = 0;
         double moleFractionHydrogen = 0;
         double moleFractionSulfur = 0;
         double moleFractionOxygen = 0;
         //double massAsh = 0;
         double moleFractionCarbonDioxide = 0;
         double moleFractionNitrogen = 0; //mole fraction of nitrogen in original gas fuel
         MaterialComponents components = fuelInlet.Components;

         if (fuelInlet is GenericFuelStream)
         {
            for (int i = 0; i < components.Count; i++)
            {
               MaterialComponent component = components[i];
               Substance mySubstance = component.Substance;
               //myMassFraction = component.MassFraction.Value;
               double myMoleFraction = component.MoleFraction.Value;
               if (mySubstance == carbon)
               {
                  moleFractionCarbon = myMoleFraction;
               }
               else if (mySubstance == hydrogen)
               {
                  moleFractionHydrogen = myMoleFraction;
               }
               else if (mySubstance == oxygen)
               {
                  moleFractionOxygen = myMoleFraction;
               }
               else if (mySubstance == sulfur)
               {
                  moleFractionSulfur = myMoleFraction;
               }
               //else if (component.Substance == ash) {
               //   massAsh = myMassFraction;
               //}
            }
         }
         else if (fuelInlet is DetailedFuelStream)
         {
            totalEenthalpyOfReactantInFuelInlet = 0;
            moleFractionCarbon = 0;
            moleFractionHydrogen = 0;
            moleFractionOxygen = 0;
            moleFractionSulfur = 0;
            double t = fuelInlet.Temperature.Value;
            for (int i = 0; i < components.Count; i++)
            {
               MaterialComponent component = components[i];
               Substance mySubstance = component.Substance;
               double myMoleFraction = component.MoleFraction.Value;

               if (mySubstance == carbonDioxide)
               {
                  moleFractionCarbonDioxide = myMoleFraction;
               }
               else if (mySubstance == nitrogen)
               {
                  moleFractionNitrogen = myMoleFraction;
               }
               else
               {
                  totalEenthalpyOfReactantInFuelInlet += myMoleFraction * propCalculator.CalculateEnthalpyOfFormation(t, mySubstance);
                  SubstanceFormula formula = mySubstance.Formula;
                  string[] elements = formula.Elements;
                  foreach (string element in elements)
                  {
                     int elementCount = formula.GetElementCount(element);
                     if (element == "C")
                     {
                        moleFractionCarbon += elementCount * myMoleFraction;
                     }
                     else if (element == "H")
                     {
                        moleFractionHydrogen += elementCount * myMoleFraction;
                     }
                     else if (element == "O")
                     {
                        moleFractionOxygen += elementCount * myMoleFraction;
                     }
                     else if (element == "S")
                     {
                        moleFractionSulfur += elementCount * myMoleFraction;
                     }
                  }
               }
            }
         }

         moleFractionHydrogen = 0.5 * moleFractionHydrogen; //convert from H to H2
         moleFractionOxygen = 0.5 * moleFractionOxygen; //convert from O to O2

         //multiply 0.5 for moleFractionHydrogen because 1 mole of H2 only needs 0.5 mole of O2
         double moleFractionOxygenNeeded = moleFractionCarbon + 0.5 * moleFractionHydrogen + moleFractionSulfur - moleFractionOxygen;
         double exactDryAirMassNeeded = moleFractionOxygenNeeded / OXYGEN_MOLE_FRACTION_IN_AIR * air.MolarWeight;

         double excessAirValue = excessAir.HasValue ? excessAir.Value/100 : 0;
         double excessDryAirNeeded = exactDryAirMassNeeded * excessAirValue;
         double dryAirMassNeeded = exactDryAirMassNeeded + excessDryAirNeeded;
         double moistureMassCarriedByInletAir = dryAirMassNeeded * airInlet.Humidity.Value;
         double airMassNeeded = dryAirMassNeeded + moistureMassCarriedByInletAir;

         double moitureGeneratedByReaction = moleFractionHydrogen * water.MolarWeight; //since 1 mole of H2 generates 1 mole of water
         double totalMoistureInFlueGas = moitureGeneratedByReaction + moistureMassCarriedByInletAir;

         double flueGasTotal = airMassNeeded + fuelInlet.Components.MolarWeight;
         double dryFlueGas = flueGasTotal - totalMoistureInFlueGas;
         double flueGasMoistureContentDryBase = totalMoistureInFlueGas / dryFlueGas;

         CompositeSubstance flueGas = CreateDryFlueGasSubstance(moleFractionCarbon, moleFractionSulfur, moleFractionCarbonDioxide, moleFractionNitrogen, moleFractionOxygenNeeded, dryAirMassNeeded, dryFlueGas, excessAirValue);
         DryingGasComponents flueGasComponents = CreateDryingGasComponents(flueGasMoistureContentDryBase, flueGas);
         flueGasOutlet.GasComponents = flueGasComponents;

         double fuelMoleFlowRate = fuelInlet.MoleFlowRate.Value;
         if (excessAir.HasValue)
         {
            Calculate(flueGasOutlet.Humidity, flueGasMoistureContentDryBase);

            if (fuelInlet.MoleFlowRate.HasValue)
            {
               Calculate(airInlet.MassFlowRateDryBase, dryAirMassNeeded * fuelMoleFlowRate);
               Calculate(flueGasOutlet.MassFlowRate, flueGasTotal * fuelMoleFlowRate);
            }
            else if (flueGasOutlet.MassFlowRate.HasValue)
            {
               fuelMoleFlowRate = flueGasOutlet.MassFlowRate.Value / flueGasTotal;
               Calculate(fuelInlet.MoleFlowRate, fuelMoleFlowRate);
               Calculate(airInlet.MassFlowRateDryBase, dryAirMassNeeded * fuelMoleFlowRate);
            }
         }

         double fuelInletEnthalpy = fuelInlet.SpecificEnthalpy.Value;
         double airInletEnthalpy = airMassNeeded * airInlet.SpecificEnthalpy.Value;
         double totalHeatGenerated = Constants.NO_VALUE;
         double heatLossValue = 0;

         if (fuelInlet is GenericFuelStream)
         {
            //GenericFuelStream gfs = fuelInlet as GenericFuelStream;
            //if (gfs.HeatValue.HasValue) {
            //   //total heat genrate eaquls to heat value of the fuel times fuelMassFlowRate
            //   totalHeatGenerated = gfs.HeatValue.Value * fuelMoleFlowRate;

            //   heatLossValue = totalHeatGenerated * percentageHeatLoss.Value / 100;

            //   double totalFlueGasSpecificEnthalpy = (fuelInletEnthalpy + airInletEnthalpy + totalHeatGenerated - heatLossValue) / flueGasTotal;
            //   Calculate(flueGasOutlet.SpecificEnthalpy, totalFlueGasSpecificEnthalpy);
            //}
         }
         else if (fuelInlet is DetailedFuelStream)
         {
            HumidGasCalculator humidGasCalculator = new HumidGasCalculator(flueGas, water);

            //evaporation heat of 2 moles of water
            double evaporationHeat = 2.0 * water.MolarWeight * humidGasCalculator.GetEvaporationHeat(273.15);
            double p = flueGasOutlet.Pressure.Value;
            //double originalTemperature = airInlet.Temperature.Value;
            //double initialHumidEnthalpy = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(originalTemperature, flueGasMoistureContentDryBase, p);
            double initialHumidEnthalpy = airInlet.SpecificEnthalpyDryBase.Value;
            initialHumidEnthalpy = initialHumidEnthalpy * dryAirMassNeeded / dryFlueGas;
            double t = airInlet.Temperature.Value;
            double totalEnthalpyOfReactantOxygen = moleFractionOxygenNeeded * propCalculator.CalculateEnthalpyOfFormation(t, oxygen);
            double totalEenthalpyOfReactants = totalEenthalpyOfReactantInFuelInlet + totalEnthalpyOfReactantOxygen;

            double tNew = t;
            int counter = 0;
            if (excessAir.HasValue && percentageHeatLoss.IsSpecifiedAndHasValue)
            {
               do
               {
                  counter++;
                  t = tNew;
                  double totalEenthalpyOfProduct = CalculateTotalEntalpyOfProduct(moleFractionCarbon, moleFractionHydrogen, moleFractionSulfur, t);
                  totalHeatGenerated = totalEenthalpyOfReactants - totalEenthalpyOfProduct + evaporationHeat;

                  heatLossValue = totalHeatGenerated * percentageHeatLoss.Value / 100;

                  double flueGasHumidEnthalpy = initialHumidEnthalpy + (totalHeatGenerated - heatLossValue) / dryFlueGas;
                  tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(flueGasHumidEnthalpy, flueGasMoistureContentDryBase, p);

               } while (Math.Abs(tNew - t) / tNew > 1.0e-8 && counter < 100);

               if (counter == 100)
               {
                  throw new CalculationFailedException("Calculation of flame temperature failed.");
               }

               Calculate(flueGasOutlet.Temperature, tNew);
               totalHeatGenerated *= fuelMoleFlowRate;

               Calculate(heatLoss, totalHeatGenerated * percentageHeatLoss.Value / 100);
            }
            else if (excessAir.HasValue && flueGasOutlet.Temperature.IsSpecifiedAndHasValue)
            {
               double flueGasTemp = flueGasOutlet.Temperature.Value;
               double totalEenthalpyOfProduct = CalculateTotalEntalpyOfProduct(moleFractionCarbon, moleFractionHydrogen, moleFractionSulfur, flueGasTemp);
               totalHeatGenerated = totalEenthalpyOfReactants - totalEenthalpyOfProduct + evaporationHeat;
               heatLossValue = totalHeatGenerated * percentageHeatLoss.Value / 100;
               double flueGasHumidEnthalpy = initialHumidEnthalpy + (totalHeatGenerated - heatLossValue) / dryFlueGas;
               tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(flueGasHumidEnthalpy, flueGasMoistureContentDryBase, p);
               
               if (tNew < flueGasTemp)
               {
                  throw new CalculationFailedException("Specified flue gas temperature cannot be reached.");
               }

               counter = 0;
               double humidEnthanlpy0 = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(flueGasTemp, flueGasMoistureContentDryBase, p);
               do
               {
                  counter++;
                  double humidEnthanlpy1 = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tNew, flueGasMoistureContentDryBase, p);
                  heatLossValue += dryFlueGas * (humidEnthanlpy1 - humidEnthanlpy0);
                  flueGasHumidEnthalpy = initialHumidEnthalpy + (totalHeatGenerated - heatLossValue) / dryFlueGas;
                  tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(flueGasHumidEnthalpy, flueGasMoistureContentDryBase, p);

               } while (Math.Abs(tNew - flueGasTemp) / flueGasTemp > 1.0e-8 && counter < 100);

               if (counter == 100)
               {
                  throw new CalculationFailedException("Calculation of flame temperature failed.");
               }

               totalHeatGenerated *= fuelMoleFlowRate;
               heatLossValue *= fuelMoleFlowRate;

               Calculate(heatLoss, heatLossValue);
               Calculate(percentageHeatLoss, heatLossValue / totalHeatGenerated * 100);
            }
            else if (flueGasOutlet.Temperature.IsSpecifiedAndHasValue && percentageHeatLoss.IsSpecifiedAndHasValue)
            {
               double flueGasTemp = flueGasOutlet.Temperature.Value;
               double totalEenthalpyOfProduct = CalculateTotalEntalpyOfProduct(moleFractionCarbon, moleFractionHydrogen, moleFractionSulfur, flueGasTemp);
               totalHeatGenerated = totalEenthalpyOfReactants - totalEenthalpyOfProduct + evaporationHeat;
               
               heatLossValue = totalHeatGenerated * percentageHeatLoss.Value / 100;
               double flueGasHumidEnthalpy = initialHumidEnthalpy + (totalHeatGenerated - heatLossValue) / dryFlueGas;
               tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(flueGasHumidEnthalpy, flueGasMoistureContentDryBase, p);

               if (tNew < flueGasTemp)
               {
                  throw new CalculationFailedException("Specified flue gas temperature cannot be reached.");
               }

               //double excessDryAirNeededOld;
               do
               {
                  counter++;
                  t = tNew;
                  //excessDryAirNeededOld = excessDryAirNeeded;
                  //initialHumidEnthalpy = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(originalTemperature, flueGasMoistureContentDryBase, p); 
                  flueGasHumidEnthalpy = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(flueGasTemp, flueGasMoistureContentDryBase, p);
                  dryFlueGas = (totalHeatGenerated - heatLossValue) / (flueGasHumidEnthalpy - initialHumidEnthalpy);

                  flueGasTotal = dryFlueGas + totalMoistureInFlueGas;
                  airMassNeeded = flueGasTotal - fuelInlet.Components.MolarWeight;

                  dryAirMassNeeded = airMassNeeded / (1 + airInlet.Humidity.Value);
                  totalMoistureInFlueGas = moitureGeneratedByReaction +dryAirMassNeeded * airInlet.Humidity.Value;
                  excessDryAirNeeded = dryAirMassNeeded - exactDryAirMassNeeded;
                  excessAirValue = excessDryAirNeeded / exactDryAirMassNeeded;

                  flueGasMoistureContentDryBase = totalMoistureInFlueGas / dryFlueGas;

                  flueGas = CreateDryFlueGasSubstance(moleFractionCarbon, moleFractionSulfur, moleFractionCarbonDioxide, moleFractionNitrogen, moleFractionOxygenNeeded, dryAirMassNeeded, dryFlueGas, excessAirValue);
                  humidGasCalculator = new HumidGasCalculator(flueGas, water);

                  tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(flueGasHumidEnthalpy, flueGasMoistureContentDryBase, p);
               } while (Math.Abs(tNew - flueGasTemp) / flueGasTemp > 1.0e-8 && counter < 100);
               //} while (Math.Abs(excessDryAirNeeded - excessDryAirNeededOld) > 1.0e-6 && counter < 100);
               
               if (counter == 100)
               {
                  throw new CalculationFailedException("Calculation of flame temperature failed.");
               }

               Calculate(flueGasOutlet.Humidity, flueGasMoistureContentDryBase);
               Calculate(excessAir, excessAirValue * 100);
               flueGasComponents = CreateDryingGasComponents(flueGasMoistureContentDryBase, flueGas);
               flueGasOutlet.GasComponents = flueGasComponents;
                
               if (flueGasOutlet.MassFlowRate.IsSpecifiedAndHasValue)
               {
                  fuelMoleFlowRate = flueGasOutlet.MassFlowRate.Value / flueGasTotal;
                  Calculate(fuelInlet.MoleFlowRate, fuelMoleFlowRate);
               }
               else
               {
                  Calculate(flueGasOutlet.MassFlowRate, flueGasTotal * fuelMoleFlowRate);
               }

               Calculate(airInlet.MassFlowRateDryBase, dryAirMassNeeded * fuelMoleFlowRate);
               Calculate(heatLoss, heatLossValue * fuelMoleFlowRate);

               totalHeatGenerated *= fuelMoleFlowRate;
            }

            Calculate(totalHeatGeneration, totalHeatGenerated);
         }

         if (flueGasOutlet.Temperature.HasValue && totalHeatGeneration.HasValue)
         {
            solveState = SolveState.Solved;
            double oxygenMassFraction = moleFractionOxygenNeeded * excessAirValue * oxygen.MolarWeight / flueGasTotal;
            double oxygenMoleFraction = moleFractionOxygenNeeded * excessAirValue / (1 + airMassNeeded / air.MolarWeight);
            Calculate(oxygenMassFractionFlueGas, oxygenMassFraction);
            Calculate(oxygenMoleFractionFlueGas, oxygenMoleFraction);
         }
      }

      private CompositeSubstance CreateDryFlueGasSubstance(double moleFractionCarbon, double moleFractionSulfur, double moleFractionCarbonDioxide, double moleFractionNitrogen, double moleFractionOxygenNeeded, double dryAirMassNeeded, double dryFlueGas, double excessAirValue)
      {
          ArrayList componentList = new ArrayList();
          double massFractionCarbonDioxide = (moleFractionCarbon + moleFractionCarbonDioxide) * carbonDioxide.MolarWeight / dryFlueGas;
          componentList.Add(new MaterialComponent(carbonDioxide, massFractionCarbonDioxide));

          //moleFractionNitrogen is the mole fraction of nitrogen in original gas fuel
          double massFractionNitrogen = (dryAirMassNeeded * NITROGEN_MOLE_FRACTION_IN_AIR * nitrogen.MolarWeight / air.MolarWeight + moleFractionNitrogen * nitrogen.MolarWeight) / dryFlueGas;
          componentList.Add(new MaterialComponent(nitrogen, massFractionNitrogen));

          double massFractionSulfurDioxide = moleFractionSulfur * sulfurDioxide.MolarWeight / dryFlueGas;
          componentList.Add(new MaterialComponent(sulfurDioxide, massFractionSulfurDioxide));

          double massFractionOxygen = moleFractionOxygenNeeded * excessAirValue * oxygen.MolarWeight / dryFlueGas;
          componentList.Add(new MaterialComponent(oxygen, massFractionOxygen));

          double massFractionArgon = dryAirMassNeeded * ARGON_MOLE_FRACTION_IN_AIR * argon.MolarWeight / air.MolarWeight / dryFlueGas;
          componentList.Add(new MaterialComponent(argon, massFractionArgon));

          CompositeSubstance flueGas = new CompositeSubstance("Flue Gas", componentList);
          return flueGas;
      }

      private static DryingGasComponents CreateDryingGasComponents(double flueGasMoistureContentDryBase, CompositeSubstance flueGas)
      {
         ArrayList compList = new ArrayList();
         compList.Add(new MaterialComponent(flueGas, 1 / (1 + flueGasMoistureContentDryBase)));
         compList.Add(new MaterialComponent(water, flueGasMoistureContentDryBase / (1 + flueGasMoistureContentDryBase)));

         DryingGasComponents flueGasComponents = new DryingGasComponents(compList);

         ArrayList gasCompList = new ArrayList();
         MaterialComponent pc = new MaterialComponent(flueGas);
         gasCompList.Add(pc);
         pc = new MaterialComponent(water);
         gasCompList.Add(pc);
         GasPhase gp = new GasPhase("Drying Gas Gas Phase", gasCompList);
         flueGasComponents.AddPhase(gp);

         return flueGasComponents;
      }

      private static double CalculateTotalEntalpyOfProduct(double moleFractionCarbon, double moleFractionHydrogen, double moleFractionSulfur, double t)
      {
         ThermalPropCalculator propCalculator = ThermalPropCalculator.Instance;
         double totalEenthalpyOfProductCarbonDioxide = moleFractionCarbon * propCalculator.CalculateEnthalpyOfFormation(t, carbonDioxide);
         double totalEenthalpyOfProductWater = moleFractionHydrogen * propCalculator.CalculateEnthalpyOfFormation(t, water);
         double totalEenthalpyOfProductSulfer = moleFractionSulfur * propCalculator.CalculateEnthalpyOfFormation(t, sulfurDioxide);
         double totalEenthalpyOfProduct = totalEenthalpyOfProductCarbonDioxide + totalEenthalpyOfProductWater + totalEenthalpyOfProductSulfer;
         return totalEenthalpyOfProduct;
      }

      //protected void AdjustVarsStates()
      //{
      //   if(percentageHeatLoss.IsSpecifiedAndHasValue)
      //   {
      //      heatLoss.State = VarState.Calculated;
      //   }
      //   else if(heatLoss.IsSpecifiedAndHasValue)
      //   {
      //      percentageHeatLoss.State = VarState.Calculated;
      //   }
      //}

      #endregion

      #region Persistence
      protected Burner(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFurnace", typeof(int));
         if (persistedClassVersion == 1) {
            this.fuelInlet = info.GetValue("FuelInlet", typeof(FuelStream)) as FuelStream;
            this.airInlet = info.GetValue("AirInlet", typeof(DryingGasStream)) as DryingGasStream;
            this.flueGasOutlet = info.GetValue("FlueGasOutlet", typeof(DryingGasStream)) as DryingGasStream;
            this.excessAir = RecallStorableObject("ExcessAir", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.totalHeatGeneration = RecallStorableObject("TotalHeatGeneration", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.percentageHeatLoss = RecallStorableObject("PercentHeatLoss", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.oxygenMassFractionFlueGas = RecallStorableObject("OxygenMassFractionFlueGas", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.oxygenMoleFractionFlueGas = RecallStorableObject("OxygenMoleFractionFlueGas", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionFurnace", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("FuelInlet", this.fuelInlet, typeof(DryingGasStream));
         info.AddValue("AirInlet", this.airInlet, typeof(DryingGasStream));
         info.AddValue("FlueGasOutlet", this.flueGasOutlet, typeof(DryingGasStream));
         info.AddValue("ExcessAir", this.excessAir, typeof(ProcessVarDouble));
         info.AddValue("TotalHeatGeneration", this.totalHeatGeneration, typeof(ProcessVarDouble));
         info.AddValue("PercentHeatLoss", this.percentageHeatLoss, typeof(ProcessVarDouble));
         info.AddValue("OxygenMassFractionFlueGas", this.oxygenMassFractionFlueGas, typeof(ProcessVarDouble));
         info.AddValue("OxygenMoleFractionFlueGas", this.oxygenMoleFractionFlueGas, typeof(ProcessVarDouble));
      }
      #endregion
   }
}

//do
//{
//   counter++;
//   t = tNew;
//   double totalEenthalpyOfProduct = CalculateTotalEntalpyOfProduct(moleFractionCarbon, moleFractionHydrogen, moleFractionSulfur, t);
//   totalHeatGenerated = totalEenthalpyOfReactants - totalEenthalpyOfProduct;

//   double totalFlueGasSpecificEnthalpy = specificEnthalpy + (totalHeatGenerated - heatLossValue) / flueGasTotal;
//   tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(totalFlueGasSpecificEnthalpy, flueGasMoistureContentDryBase, p);

//} while (Math.Abs(tNew - t) / tNew > 1.0e-8 && counter < 100);

//if (counter == 100)
//{
//   throw new CalculationFailedException("Calculation of flame temperature failed.");
//}

//if (tNew < flueGasTemp && outerLoopCounter == 1)
//{
//   throw new CalculationFailedException("Specified flue gas temperature cannot be reached.");
//}
//else
//{
//   double humidEnthanlpy0 = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(flueGasTemp, flueGasMoistureContentDryBase, p);
//   double humidEnthanlpy1 = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tNew, flueGasMoistureContentDryBase, p);
//   heatLossValue += dryFlueGas * (humidEnthanlpy1 - humidEnthanlpy0);
//}
//double exactOxygenMassNeeded = moleFractionOxygenNeeded * oxygen.MolarWeight;
//double exactDryAirMoleNeeded = moleFractionOxygenNeeded / OXYGEN_MOLE_FRACTION_IN_AIR;
//double exactDryAirMassNeeded = exactDryAirMoleNeeded * air.MolarWeight;
//double flueGasTotal = airMassNeeded + fuelInlet.MassFlowRate.Value / fuelInlet.MoleFlowRate.Value;

//double exactDryAirMassNeeded = moleFractionOxygenNeeded * air.MolarWeight / OXYGEN_MASS_FRACTION_IN_AIR;
//double totalDryFlueGas = totalDryAirMassNeeded + fuelMoleFlowRate - totalMoitureGeneratedByReaction;
//double dryFlueGas = moleFractionCarbon * carbonDioxide.MolarWeight + moleFractionSulfur * sulfurDioxide.MolarWeight
//   + exactDryAirMoleNeeded * (air.MolarWeight - OXYGEN_MOLE_FRACTION_IN_AIR * oxygen.MolarWeight) + excessDryAirNeeded;
//double flueGasGenerated = dryFlueGas + totalMoistureInFlueGas;
//Calculate(flueGasOutlet.MassFlowRateDryBase, dryFlueGas * fuelMoleFlowRate);
