using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
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
   public class Burner : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int FUEL_INLET_INDEX = 0;
      public static int AIR_INLET_INDEX = 1;
      public static int FLUE_GAS_OUTLET_INDEX = 2;

      private FuelStream fuelInlet;
      private DryingGasStream airInlet;
      private DryingGasStream flueGasOutlet;

      private ProcessVarDouble excessAir;
      
      #region public properties

      public FuelStream FuelInlet {
         get { return fuelInlet; }
      }

      public DryingGasStream AirInlet {
         get { return airInlet; }
      }

      public DryingGasStream FlueGasOutlet {
         get { return flueGasOutlet; }
      }

      public ProcessVarDouble ExcessAir {
         get { return excessAir; }
      }

      #endregion

      public Burner(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         excessAir = new ProcessVarDouble(StringConstants.EXCESS_AIR, PhysicalQuantity.Fraction, VarState.Specified, this);
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
         if (excessAir.HasValue) {
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
               
      private void Solve() {
         Substance carbon = SubstanceCatalog.GetSubstance(Substance.CARBON);
         Substance hydrogen = SubstanceCatalog.GetSubstance(Substance.HYDROGEN);
         Substance oxygen = SubstanceCatalog.GetSubstance(Substance.OXYGEN);
         Substance sulfur = SubstanceCatalog.GetSubstance(Substance.SULFUR);
         Substance air = SubstanceCatalog.GetSubstance(Substance.AIR);
         Substance carbonDioxide = SubstanceCatalog.GetSubstance(Substance.CARBON_DIOXIDE);
         Substance sulfurDioxide = SubstanceCatalog.GetSubstance(Substance.SULFUR_DIOXIDE);
         Substance water = SubstanceCatalog.GetSubstance(Substance.WATER);
         Substance nitrogen = SubstanceCatalog.GetSubstance(Substance.NITROGEN);
         Substance argon = SubstanceCatalog.GetSubstance(Substance.ARGON);
         //Substance ash = SubstanceCatalog.GetInstance().GetSubstance("ash");

         double totalEenthalpyOfReactantInFuelInlet = 0;
         double moleFractionCarbon = 0;
         double moleFractionHydrogen = 0;
         double moleFractionSulfur = 0;
         double moleFractionOxygen = 0;
         //double massAsh = 0;
         double moleFractionCarbonDioxide = 0;
         double moleFractionNitrogen = 0;
         MaterialComponents components = fuelInlet.Components;
         MaterialComponent component;
         Substance mySubstance;
         double myMassFraction;

         if (fuelInlet is GenericFuelStream) {
            for (int i = 0; i < components.Count; i++) {
               component = components[i];
               mySubstance = component.Substance;
               myMassFraction = component.MassFraction.Value;
               if (mySubstance == carbon) {
                  moleFractionCarbon = myMassFraction / carbon.MolarWeight;
               }
               else if (mySubstance == hydrogen) {
                  moleFractionHydrogen = 0.5 * myMassFraction / hydrogen.MolarWeight;
               }
               else if (mySubstance == oxygen) {
                  moleFractionOxygen = myMassFraction / oxygen.MolarWeight;
               }
               else if (mySubstance == sulfur) {
                  moleFractionSulfur = myMassFraction / sulfur.MolarWeight;
               }
               //else if (component.Substance == ash) {
               //   massAsh = myMassFraction;
               //}
            }
         }
         else if (fuelInlet is DetailedFuelStream) {
            totalEenthalpyOfReactantInFuelInlet = 0;
            moleFractionCarbon = 0;
            moleFractionHydrogen = 0;
            moleFractionOxygen = 0;
            moleFractionSulfur = 0;
            SubstanceFormula formula;
            string[] elements;
            int elementCount;
            double t = fuelInlet.Temperature.Value;
            for (int i = 0; i < components.Count; i++) {
               component = components[i];
               mySubstance = component.Substance;
               myMassFraction = component.MassFraction.Value;

               if (mySubstance == carbonDioxide) {
                  moleFractionCarbonDioxide = myMassFraction / carbonDioxide.MolarWeight;
               }
               else if (mySubstance == nitrogen) {
                  moleFractionNitrogen = myMassFraction / nitrogen.MolarWeight;
               }
               else {
                  totalEenthalpyOfReactantInFuelInlet += myMassFraction * ThermalPropCalculator.Instance.CalculateEnthalpyOfFormation(t, mySubstance);
                  formula = mySubstance.Formula;
                  elements = formula.Elements;
                  foreach (string element in elements) {
                     elementCount = formula.GetElementCount(element);
                     if (element == "C") {
                        moleFractionCarbon += elementCount * myMassFraction / carbon.MolarWeight;
                     }
                     else if (element == "H") {
                        moleFractionHydrogen += elementCount * 0.25 * myMassFraction / hydrogen.MolarWeight;
                     }
                     else if (element == "O") {
                        moleFractionOxygen = elementCount * 0.5 * myMassFraction / oxygen.MolarWeight;
                     }
                     else if (element == "S") {
                        moleFractionSulfur += elementCount * myMassFraction / sulfur.MolarWeight;
                     }
                  }
               }
            }
         }

         double fuelMassFlowRate = fuelInlet.MassFlowRate.Value;
         double moleFractionOxygenNeeded = moleFractionCarbon + moleFractionHydrogen + moleFractionSulfur - moleFractionOxygen;
         double totalExactOxygenMassNeeded = fuelMassFlowRate * moleFractionOxygenNeeded * oxygen.MolarWeight;
         double totalOxygenMoleNeeded = fuelMassFlowRate * moleFractionOxygenNeeded * (1.0 + excessAir.Value);
         double totalDryAirMassNeeded = totalOxygenMoleNeeded * air.MolarWeight / 0.21;
         double totalMoistureMassCarriedByInletAir = totalDryAirMassNeeded * airInlet.Humidity.Value;
         double totalAirMassNeeded = totalDryAirMassNeeded + totalMoistureMassCarriedByInletAir;
         
         Calculate(airInlet.MassFlowRate, totalAirMassNeeded);

         double totalMoitureGeneratedByReaction = fuelMassFlowRate * moleFractionHydrogen * water.MolarWeight;
         double totalMoistureInFlueGas = totalMoitureGeneratedByReaction + totalMoistureMassCarriedByInletAir;

         double totalDryFlueGas = totalDryAirMassNeeded + fuelMassFlowRate - totalMoitureGeneratedByReaction;
         double totalFlueGasGenerated = totalDryFlueGas + totalMoistureInFlueGas;
         
         Calculate(flueGasOutlet.MassFlowRate, totalFlueGasGenerated);

         double flueGasMoistureContentDryBase = totalMoistureInFlueGas / totalDryFlueGas;
         
         Calculate(flueGasOutlet.Humidity, flueGasMoistureContentDryBase);

         ArrayList componentList = new ArrayList();
         double massFractionCarbonDioxide = fuelMassFlowRate * (moleFractionCarbon + moleFractionCarbonDioxide) * carbonDioxide.MolarWeight / totalDryFlueGas;
         componentList.Add(new MaterialComponent(carbonDioxide, massFractionCarbonDioxide));

         double massFractionNitrogen = (totalDryAirMassNeeded * 0.78 + fuelMassFlowRate * moleFractionNitrogen) / totalDryFlueGas;
         componentList.Add(new MaterialComponent(nitrogen, massFractionNitrogen));
         
         double massFractionSulfurDioxide = fuelMassFlowRate * moleFractionSulfur * sulfurDioxide.MolarWeight / totalDryFlueGas;
         componentList.Add(new MaterialComponent(sulfurDioxide, massFractionSulfurDioxide));

         double massFractionOxygen = fuelMassFlowRate * moleFractionOxygenNeeded * excessAir.Value * oxygen.MolarWeight / totalDryFlueGas;
         componentList.Add(new MaterialComponent(oxygen, massFractionOxygen));

         double massFractionArgon = totalDryAirMassNeeded * 0.01 / totalDryFlueGas;
         componentList.Add(new MaterialComponent(argon, massFractionArgon));

         CompositeSubstance flueGas = new CompositeSubstance("Flue Gas", componentList);

         MaterialComponents flueGasComponents = new MaterialComponents();
         flueGasComponents.Add(new MaterialComponent(flueGas, 1/(1+flueGasMoistureContentDryBase)));
         flueGasComponents.Add(new MaterialComponent(water, flueGasMoistureContentDryBase/(1 + flueGasMoistureContentDryBase)));

         flueGasOutlet.Components = flueGasComponents;

         double fuelInletEnthalpy = fuelMassFlowRate * fuelInlet.SpecificEnthalpy.Value;
         double airInletEnthalpy = totalAirMassNeeded * airInlet.SpecificEnthalpy.Value;
         double totalHeatGenerated = Constants.NO_VALUE;
         if (fuelInlet is GenericFuelStream) {
            GenericFuelStream gfs = fuelInlet as GenericFuelStream;
            if (gfs.HeatValue.HasValue) {
               //total heat genrate eaquls to heat value of the fuel times fuelMassFlowRate
               totalHeatGenerated = gfs.HeatValue.Value * fuelMassFlowRate;
               double totalFlueGasSpecificEnthalpy = (fuelInletEnthalpy + airInletEnthalpy + totalHeatGenerated) / totalFlueGasGenerated;
               Calculate(flueGasOutlet.SpecificEnthalpy, totalFlueGasSpecificEnthalpy);
            }
         }
         else if (fuelInlet is DetailedFuelStream) {
            HumidGasCalculator humidGasCalculator = new HumidGasCalculator(flueGas, water);
            ThermalPropCalculator propCalculator = ThermalPropCalculator.Instance;

            double totalEenthalpyOfProductCarbonDioxide;
            double totalEenthalpyOfProductWater;
            double totalEenthalpyOfProductSulfer;
            double totalEenthalpyOfProduct;
            double totalFlueGasSpecificEnthalpy;
            
            double t = fuelInlet.Temperature.Value; ;
            totalEenthalpyOfReactantInFuelInlet *= fuelMassFlowRate;
            double totalEnthalpyOfReactantOxygen = totalExactOxygenMassNeeded * propCalculator.CalculateEnthalpyOfFormation(t, oxygen);
            double totalEenthalpyOfReactants = totalEenthalpyOfReactantInFuelInlet + totalEnthalpyOfReactantOxygen;

            double p = flueGasOutlet.Pressure.Value;
            double tNew = t;
            int counter = 0;
            do {
               counter++;
               t = tNew;
               totalEenthalpyOfProductCarbonDioxide = fuelMassFlowRate * moleFractionCarbon * propCalculator.CalculateEnthalpyOfFormation(t, carbonDioxide);
               totalEenthalpyOfProductWater = fuelMassFlowRate * moleFractionHydrogen * propCalculator.CalculateEnthalpyOfFormation(t, water);
               totalEenthalpyOfProductSulfer = fuelMassFlowRate * moleFractionSulfur * propCalculator.CalculateEnthalpyOfFormation(t, sulfurDioxide);
               totalEenthalpyOfProduct = totalEenthalpyOfProductCarbonDioxide + totalEenthalpyOfProductWater + totalEenthalpyOfProductSulfer;
               totalHeatGenerated = totalEenthalpyOfProduct - totalEenthalpyOfReactants;
               totalFlueGasSpecificEnthalpy = (fuelInletEnthalpy + airInletEnthalpy + totalHeatGenerated) / totalFlueGasGenerated;

               tNew = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(totalFlueGasSpecificEnthalpy, flueGasMoistureContentDryBase, p);
            } while (Math.Abs(tNew - t) < 1.0e-6 && counter < 100);

            if (counter == 100) {
            }

            Calculate(flueGasOutlet.Temperature, tNew);
         }
      }

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
      }
   }
}

