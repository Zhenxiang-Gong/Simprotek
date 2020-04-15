using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
   /// <summary>
   /// Summary description for Furnace.
   /// </summary>
   [Serializable] 
   public class Furnace : UnitOperation {
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

      public Furnace(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         excessAir = new ProcessVarDouble(StringConstants.EXCESS_AIR, PhysicalQuantity.Fraction, VarState.Specified, this);
      }

      public override bool CanConnect(int streamIndex) {
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
            ps.UpStreamOwner = this;
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
            ps.UpStreamOwner = null;
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
         if (IsSolveReady()) {
            Solve();
         }
            
         PostSolve();
      }
               
      private void Solve() {
         Substance carbon = SubstanceCatalog.GetInstance().GetSubstance("carbon");
         Substance hydrogen = SubstanceCatalog.GetInstance().GetSubstance("hydrogen");
         Substance oxygen = SubstanceCatalog.GetInstance().GetSubstance("oxygen");
         Substance sulfur = SubstanceCatalog.GetInstance().GetSubstance("sulfur");
         Substance air = SubstanceCatalog.GetInstance().GetSubstance("air");
         Substance carbonDioxide = SubstanceCatalog.GetInstance().GetSubstance("carbon dioxide");
         Substance sulfurDioxide = SubstanceCatalog.GetInstance().GetSubstance("sulfur dioxide");
         Substance water = SubstanceCatalog.GetInstance().GetSubstance("water");
         Substance nitrogen = SubstanceCatalog.GetInstance().GetSubstance("nitrogen");
         Substance argon = SubstanceCatalog.GetInstance().GetSubstance("argon");
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

            double totalEenthalpyOfProductCarbonDioxide;
            double totalEenthalpyOfProductWater;
            double totalEenthalpyOfProductSulfer;
            double totalEenthalpyOfProduct;
            double totalFlueGasSpecificEnthalpy;
            
            double t = fuelInlet.Temperature.Value; ;
            totalEenthalpyOfReactantInFuelInlet *= fuelMassFlowRate;
            double totalEnthalpyOfReactantOxygen = totalExactOxygenMassNeeded * ThermalPropCalculator.Instance.CalculateEnthalpyOfFormation(t, oxygen);
            double totalEenthalpyOfReactants = totalEenthalpyOfReactantInFuelInlet + totalEnthalpyOfReactantOxygen;

            double p = flueGasOutlet.Pressure.Value;
            double tNew = t;
            int counter = 0;
            do {
               counter++;
               t = tNew;
               totalEenthalpyOfProductCarbonDioxide = fuelMassFlowRate * moleFractionCarbon * ThermalPropCalculator.Instance.CalculateEnthalpyOfFormation(t, carbonDioxide);
               totalEenthalpyOfProductWater = fuelMassFlowRate * moleFractionHydrogen * ThermalPropCalculator.Instance.CalculateEnthalpyOfFormation(t, water);
               totalEenthalpyOfProductSulfer = fuelMassFlowRate * moleFractionSulfur * ThermalPropCalculator.Instance.CalculateEnthalpyOfFormation(t, sulfurDioxide);
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

      protected Furnace(SerializationInfo info, StreamingContext context)
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

