//using System;
//using System.Drawing;
//using System.Runtime.Serialization;
//using System.Security.Permissions;
//
////using Prosimo;
//using Prosimo.Plots;
//using Prosimo.Materials;
//using Prosimo.SubstanceLibrary;
//using Prosimo.UnitOperations.ProcessStreams;
//
//namespace Prosimo.UnitOperations.HeatTransfer {
//   
//   /// <summary>
//   /// Summary description for HXRatingModelShellAndTubeSimplified.
//   /// </summary>
//   [Serializable]
//   public class HXRatingModelShellAndTubeSimple : HXRatingModel {
//      private const int CLASS_PERSISTENCE_VERSION = 1; 
//      
//      //Unit Operations in Food Engineering, Page 397, Fig 13-12
//      //Curve: (10, 6.0), (20, 3.0), (50, 1.45), (100, 0.95), (200, 0.708), (400, 0.6), (1.0e6, 0.15)
//      static readonly PointF[] frictionFactorCurve = {new PointF((float)Math.Log10(10), (float)Math.Log10(6.0)), 
//                                                      new PointF((float)Math.Log10(20), (float)Math.Log10(3.0)),
//                                                      new PointF((float)Math.Log10(50), (float)Math.Log10(1.45)), 
//                                                      new PointF((float)Math.Log10(100), (float)Math.Log10(0.95)),
//                                                      new PointF((float)Math.Log10(200), (float)Math.Log10(0.708)),
//                                                      new PointF((float)Math.Log10(400), (float)Math.Log10(0.6)),
//                                                      new PointF((float)Math.Log10(1.0e6), (float)Math.Log10(0.15))};
//      private bool isShellSideHot = true;
//
//      private Orientation orientation;
//
//      private ShellType shellType;
//      private ProcessVarInt shellPasses;
//      private ProcessVarInt tubePassesPerShellPass;
//      private ProcessVarInt tubesPerTubePass;
//      private ProcessVarDouble tubeLengthBetweenTubeSheets;
//      private ProcessVarDouble tubeInnerDiameter;
//      private ProcessVarDouble tubeOuterDiameter;
//      private ProcessVarDouble tubeWallThickness;
//
//      private ProcessVarDouble tubePitch;
//
//      private ProcessVarDouble baffleCut;
//      private ProcessVarDouble baffleSpacing;
//
//      private ProcessVarDouble shellInnerDiameter;
//
//      //Calculated variables
//      private double baffleWindowArea;
//      private double transversalFlowArea;
//
//
//      #region public properties
//      public ShellType ShellType {
//         get {return shellType;}
////         set {
////            shellType = value;
////            owner.HasBeenModified(true);
////         }
//      }
//      
//      public ProcessVarInt ShellPasses {
//         get {return shellPasses;}
//      }
//
//      public ProcessVarInt TubePassesPerShellPass {
//         get {return tubePassesPerShellPass;}
//      }
//
//      public ProcessVarInt TubesPerTubePass {
//         get {return tubesPerTubePass;}
//      }
//      
//      public ProcessVarDouble TubeLengthBetweenTubeSheets {
//         get {return tubeLengthBetweenTubeSheets;}
//      }
//      
//      public ProcessVarDouble TubeInnerDiameter {
//         get {return tubeInnerDiameter;}
//      }
//
//      public ProcessVarDouble TubeOuterDiameter {
//         get {return tubeOuterDiameter;}
//      }
//
//      public ProcessVarDouble TubeWallThickness {
//         get {return tubeWallThickness;}
//      }
//
//      public ProcessVarDouble TubePitch {
//         get {return tubePitch;}
//      }
//
//      public ProcessVarDouble BaffleCut {
//         get {return baffleCut;}
//      }
//
//      public ProcessVarDouble BaffleSpacing {
//         get {return baffleSpacing;}
//      }
//
//      public ProcessVarDouble ShellInnerDiameter {
//         get {return shellInnerDiameter;}
//      }
//      #endregion
//
//      public HXRatingModelShellAndTubeSimple(HeatExchanger heatExchanger, bool isShellSideHot) : base(heatExchanger) {
//         this.isShellSideHot = isShellSideHot;
//
//         orientation = Orientation.Horizontal;
//         
//         shellType = ShellType.E;
//         shellPasses = new ProcessVarInt(StringConstants.SHELL_PASSES, PhysicalQuantity.Unknown, 1, VarState.Specified, owner);
//         tubePassesPerShellPass = new ProcessVarInt(StringConstants.TUBE_PASSES_PER_SHELL_PASS, PhysicalQuantity.Unknown, 2, VarState.Specified, owner);
//         tubesPerTubePass = new ProcessVarInt(StringConstants.TUBES_PER_TUBE_PASS, PhysicalQuantity.Unknown, 44, VarState.Specified, owner);
//         tubeInnerDiameter = new ProcessVarDouble(StringConstants.TUBE_INNER_DIAMETER, PhysicalQuantity.SmallLength, 0.02, VarState.Specified, owner);
//         tubeOuterDiameter = new ProcessVarDouble(StringConstants.TUBE_OUTER_DIAMETER, PhysicalQuantity.SmallLength, 0.025, VarState.Specified, owner);
//         tubeWallThickness = new ProcessVarDouble(StringConstants.TUBE_WALL_THICKNESS, PhysicalQuantity.SmallLength, VarState.Calculated, owner);
//         tubeLengthBetweenTubeSheets = new ProcessVarDouble(StringConstants.TUBE_LENGTH, PhysicalQuantity.Length, 6.0, VarState.Specified, owner);
//         
//         tubePitch = new ProcessVarDouble(StringConstants.TUBE_PITCH, PhysicalQuantity.SmallLength, 0.03, VarState.Specified, owner);
//         
//         baffleCut = new ProcessVarDouble(StringConstants.BAFFLE_CUT, PhysicalQuantity.SmallLength, 0.15, VarState.Specified, owner);
//         baffleSpacing = new ProcessVarDouble(StringConstants.BAFFLE_SPACING, PhysicalQuantity.SmallLength, VarState.AlwaysCalculated, owner);
//
//         shellInnerDiameter = new ProcessVarDouble(StringConstants.SHELL_INNER_DIAMETER, PhysicalQuantity.SmallLength, 0.4, VarState.Specified, owner);
//
//         //InitializeGeometryParams();
//
//         InitializeVarListAndRegisterVars();
//      }
//
//      protected override void InitializeVarListAndRegisterVars() {
//         base.InitializeVarListAndRegisterVars();
//
//         procVarList.Add(tubeInnerDiameter);
//         procVarList.Add(tubeOuterDiameter);
//         procVarList.Add(tubeWallThickness);
//         
//         procVarList.Add(tubePassesPerShellPass);
//         procVarList.Add(tubesPerTubePass);
//         procVarList.Add(shellPasses);
//         
//         procVarList.Add(tubeLengthBetweenTubeSheets);
//         procVarList.Add(tubePitch);
//         procVarList.Add(shellInnerDiameter);
//         procVarList.Add(baffleCut);
//         procVarList.Add(baffleSpacing);
//      
//         owner.AddVarsOnListAndRegisterInSystem(procVarList);
//      }
//
//      internal override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
//         ErrorMessage retValue = null;
//         if (pv.VarTypeName == StringConstants.TUBE_PITCH) {
//            if (aValue < tubeOuterDiameter.Value) {
//               retValue = new ErrorMessage(ErrorType.Error, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Tube pitch must be greater than tube diameter");
//            }
//         }
//         else if (pv.VarTypeName == StringConstants.BAFFLE_SPACING) {
//            //rang value is from Handbook of Unit Operations of Chemical Engineering
//            if (aValue < 0.2 * shellInnerDiameter.Value || aValue > shellInnerDiameter.Value) {
//               retValue = new ErrorMessage(ErrorType.Error, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Baffle spacing should not be smaller than one-fifth of the shell inner diameter or larger than the shell inner diameter");
//            }
//         }
//         else if (pv.VarTypeName == StringConstants.BAFFLE_CUT) {
//            if (aValue >= 0.5 * shellInnerDiameter.Value) {
//               retValue = new ErrorMessage(ErrorType.Error, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Baffle cut should not be less than half of the shell inner diameter");
//            }
//         }
//        return retValue;
//      }
//      
//      public override bool IsRatingCalcReady() {
//         bool isReady = false;
//         if (owner.HotSideInlet.MassFlowRate.HasValue && owner.ColdSideInlet.MassFlowRate.HasValue) {
//            if ((owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue) 
//               || (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue) 
//               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue) 
//               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue)
//               ) {
//               
//               isReady = true;
//            }
//         }
//         else if (owner.HotSideInlet.MassFlowRate.HasValue || owner.ColdSideInlet.MassFlowRate.HasValue) {
//            if ( (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue)
//               ||(owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue)
//               ||(owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue) 
//               ||(owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue) 
//               ) {
//                  
//               isReady = true;
//            }
//         }
//         return isReady;
//      }
//      
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideLiquidPhaseHeatTransferCoeff (double temperature, double massFlowRate) {
//         double density = owner.HotSideInlet.GetLiquidDensity(temperature);
//         double bulkVisc = owner.HotSideInlet.GetLiquidViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double thermalCond = owner.HotSideInlet.GetLiquidThermalConductivity(temperature);
//         double specificHeat = owner.HotSideInlet.GetLiquidCp(temperature);
//         
//         return CalculateHotSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, 
//            thermalCond, specificHeat);
//      }
//      
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideVaporPhaseHeatTransferCoeff (double temperature, double pressure, double massFlowRate) {
//         double density = owner.HotSideInlet.GetGasDensity(temperature, pressure);
//         double bulkVisc = owner.HotSideInlet.GetGasViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double thermalCond = owner.HotSideInlet.GetGasThermalConductivity(temperature);
//         double specificHeat = owner.HotSideInlet.GetGasCp(temperature);
//         
//         return CalculateHotSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, 
//            thermalCond, specificHeat);
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideLiquidPhaseHeatTransferCoeff (double temperature, double massFlowRate) {
//         double density = owner.ColdSideInlet.GetLiquidDensity(temperature);
//         double bulkVisc = owner.ColdSideInlet.GetLiquidViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double thermalCond = owner.ColdSideInlet.GetLiquidThermalConductivity(temperature);
//         double specificHeat = owner.ColdSideInlet.GetLiquidCp(temperature);
//         return CalculateColdSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, 
//            thermalCond, specificHeat);
//      }
//      
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideVaporPhaseHeatTransferCoeff (double temperature, double pressure, double massFlowRate) {
//         double density = owner.ColdSideInlet.GetGasDensity(temperature, pressure);
//         double bulkVisc = owner.ColdSideInlet.GetGasViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double thermalCond = owner.ColdSideInlet.GetGasThermalConductivity(temperature);
//         double specificHeat = owner.ColdSideInlet.GetGasCp(temperature);
//         return CalculateColdSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, 
//            thermalCond, specificHeat);
//      }
//      
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideCondensingHeatTransferCoeff (double temperature, double pressure, double massFlowRate, double inVapQuality, double outVapQuality) {
//         double liqDensity = owner.HotSideInlet.GetLiquidDensity(temperature);
//         double vapDensity = owner.HotSideInlet.GetGasDensity(temperature, pressure);
//         double liqViscosity = owner.HotSideInlet.GetLiquidViscosity(temperature);
//         double vapViscosity = owner.HotSideInlet.GetGasViscosity(temperature);
//         double liqThermalCond = owner.HotSideInlet.GetLiquidThermalConductivity(temperature);
//         double tubeDiam = tubeOuterDiameter.Value;
//         double tubeLength = tubeLengthBetweenTubeSheets.Value;
//
//         double htc = 0;
//
//         if (isShellSideHot) {
//            if (orientation == Orientation.Horizontal) {
//               //htc = shellHtcDpCalc.CalculateHorizontalCondensingHTC_Nusselt(massFlowRate, tubeDiam, tubeLength, liqDensity, liqViscosity, liqThermalCond);
//               htc = CondensationHeatTransferCoeffCalculator.CalculateHorizontalTubeHTC_Nusselt(massFlowRate, tubeLength, liqDensity, liqViscosity, liqThermalCond);
//            }
//            else if (orientation == Orientation.Vertical) {
//               double liqSpecificHeat = owner.HotSideInlet.GetLiquidCp(temperature);
//               htc = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, tubeDiam, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
//            }
//         }
//         else {
//            if (orientation == Orientation.Horizontal) {
//               massFlowRate /= tubesPerTubePass.Value;
//               htc = TubeHTCAndDPCalculator.CalculateInTubeHorizontalCondensingHTC(massFlowRate, tubeLength, liqDensity, vapDensity, liqViscosity, liqThermalCond, inVapQuality, outVapQuality);
//            }
//            else if (orientation == Orientation.Vertical) {
//               double liqSpecificHeat = owner.HotSideInlet.GetLiquidCp(temperature);
//               htc = TubeHTCAndDPCalculator.CalculateInTubeVerticalCondensingHTC(massFlowRate, tubeDiam, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
//            }
//         }
//         return htc;
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideBoilingHeatTransferCoeff (double temperature, double pressure, double massFlowRate, double heatFlux) {
//         double htc = 0;
//         ProcessStreamBase psb = owner.ColdSideInlet;
//         double criticalPressure = 0;
//         if (psb is DryingMaterialStream) {
//            DryingMaterialStream dms = psb as DryingMaterialStream;
//            Substance s = (dms.MaterialComponents as DryingMaterialComponents).Moisture.Substance;
//            criticalPressure = s.CriticalProperties.CriticalPressure;
//         }
//         if (isShellSideHot) {
//            //htc = shellHtcDpCalc.CalculateNucleateBoilingHTC(heatFlux, pressure, criticalPressure);
//            htc = BoilingHeatTransferCoeffCalculator.CalculateNucleateBoilingHTC_Mostinski(heatFlux, pressure, criticalPressure);
//         }
//         else {
//            htc = TubeHTCAndDPCalculator.CalculateInTubeNucleateBoilingHTC(heatFlux, pressure, criticalPressure);
//         }
//
//         return htc;
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideLiquidPhasePressureDrop (double temperature, double massFlowRate) {
//         double density = owner.HotSideInlet.GetLiquidDensity(temperature);
//         double bulkVisc = owner.HotSideInlet.GetLiquidViscosity(temperature);
//         double wallVisc = bulkVisc;
//         
//         return CalculateHotSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideVaporPhasePressureDrop (double temperature, double pressure, double massFlowRate) {
//         double density = owner.HotSideInlet.GetGasDensity(temperature, pressure);
//         double bulkVisc = owner.HotSideInlet.GetGasViscosity(temperature);
//         double wallVisc = bulkVisc;
//
//         return CalculateHotSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideLiquidPhasePressureDrop (double temperature, double massFlowRate) {
//         double density = owner.ColdSideInlet.GetLiquidDensity(temperature);
//         double bulkVisc = owner.ColdSideInlet.GetLiquidViscosity(temperature);
//         double wallVisc = bulkVisc;
//         
//         return CalculateColdSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideVaporPhasePressureDrop (double temperature, double pressure, double massFlowRate) {
//         double density = owner.ColdSideInlet.GetGasDensity(temperature, pressure);
//         double bulkVisc = owner.ColdSideInlet.GetGasViscosity(temperature);
//         double wallVisc = bulkVisc;
//         
//         return CalculateColdSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetHotSideCondensingPressureDrop (double temperature, double pressure, double inletVaporMassFlowRate, double outletVaporMassFlowRate) {
//         double tubeDiam = tubeInnerDiameter.Value;
//         double tubeLength = tubeLengthBetweenTubeSheets.Value;
//         double density = owner.HotSideInlet.GetGasDensity(temperature, pressure);
//         double bulkVisc = owner.HotSideInlet.GetGasViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double dp = 0;
//
//         if (isShellSideHot) {
//            dp = CalculateShellSideSinglePhaseDp(inletVaporMassFlowRate, density, bulkVisc);
//            dp = dp * shellPasses.Value;
//         }
//         else {
//            inletVaporMassFlowRate /= tubesPerTubePass.Value;
//            dp = TubeHTCAndDPCalculator.CalculateSinglePhaseDp(inletVaporMassFlowRate, tubeDiam, tubeLength, density, bulkVisc);
//            dp = dp * tubePassesPerShellPass.Value * shellPasses.Value;
//         }
//         dp = 0.5 * (1 + outletVaporMassFlowRate/inletVaporMassFlowRate)*dp;
//         return  dp;
//      }
//
//      //Calculate hot side heat transfer coefficient
//      public override double GetColdSideBoilingPressureDrop (double temperature, double pressure, double massFlowRate) {
//         double tubeLength = tubeLengthBetweenTubeSheets.Value;
//         double tubeDiam = tubeInnerDiameter.Value;
//         double density = owner.ColdSideInlet.GetLiquidDensity(temperature);
//         double bulkVisc = owner.ColdSideInlet.GetLiquidViscosity(temperature);
//         double wallVisc = bulkVisc;
//         double dp = 0.0;
//         if (isShellSideHot) {
//            dp = CalculateShellSideSinglePhaseDp(massFlowRate, density, bulkVisc);
//            dp = dp * shellPasses.Value;
//         }
//         else {
//            massFlowRate /= tubesPerTubePass.Value;
//            dp = TubeHTCAndDPCalculator.CalculateSinglePhaseDp(massFlowRate, tubeDiam, tubeLength, density, bulkVisc);
//            dp = dp * tubePassesPerShellPass.Value * shellPasses.Value;
//         }
//         return dp;
//      }
//
//      private double CalculateHotSideSinglePhaseHeatTrasferCoeff(double massFlowRate, double density, double bulkVisc, double wallVisc, 
//         double thermalCond, double specificHeat) {
//
//         if (isShellSideHot) {
//            //Donohue equation from Unit Operations of Chemical Engineering Eq.15.6
//            return CalculateShellSideSinglePhaseHTC(massFlowRate, density, bulkVisc, wallVisc, thermalCond, specificHeat);
//         }
//         else {
//            double tubeDiam = tubeInnerDiameter.Value;
//            double tubeLength = tubeLengthBetweenTubeSheets.Value;
//            massFlowRate /= tubesPerTubePass.Value;
//            return TubeHTCAndDPCalculator.CalculateSinglePhaseHTC(massFlowRate, tubeDiam, tubeLength, density, bulkVisc, wallVisc, 
//               thermalCond, specificHeat);
//         }
//      }
//
//      private double CalculateColdSideSinglePhaseHeatTrasferCoeff(double massFlowRate, double density, double bulkVisc, double wallVisc, 
//         double thermalCond, double specificHeat) {
//
//         if (!isShellSideHot) {
//            return CalculateShellSideSinglePhaseHTC(massFlowRate, density, bulkVisc, wallVisc, thermalCond, specificHeat);
//         }
//         else {
//            double tubeDiam = tubeInnerDiameter.Value;
//            double tubeLength = tubeLengthBetweenTubeSheets.Value;
//            massFlowRate /= tubesPerTubePass.Value;
//            return TubeHTCAndDPCalculator.CalculateSinglePhaseHTC(massFlowRate, tubeDiam, tubeLength, density, bulkVisc, wallVisc, 
//               thermalCond, specificHeat);
//         }
//      }
//
//      //Calculate hot side heat transfer coefficient
//      private double CalculateHotSideSinglePhasePressureDrop (double massFlowRate, double density, double bulkVisc, double wallVisc) {
//         double dp = 0;
//         if (isShellSideHot) { 
//            dp = CalculateShellSideSinglePhaseDp(massFlowRate, density, bulkVisc);
//            dp = dp * shellPasses.Value;
//         } 
//         else {
//            double tubeDiam = tubeInnerDiameter.Value;
//            double tubeLength = tubeLengthBetweenTubeSheets.Value;
//            massFlowRate /= tubesPerTubePass.Value;
//            dp = TubeHTCAndDPCalculator.CalculateSinglePhaseDp(massFlowRate, tubeDiam, tubeLength, density, bulkVisc);
//            dp = dp * tubePassesPerShellPass.Value * shellPasses.Value;
//         }
//         return dp;
//      }
//
//      //Calculate hot side heat transfer coefficient
//      private double CalculateColdSideSinglePhasePressureDrop (double massFlowRate, double density, double bulkVisc, double wallVisc) {
//         double dp = 0;
//         if (!isShellSideHot) { //if shell side is not the hot side it is the cold side
//            dp = CalculateShellSideSinglePhaseDp(massFlowRate, density, bulkVisc);
//            dp = dp * shellPasses.Value;
//         } 
//         else {
//            double tubeDiam = tubeInnerDiameter.Value;
//            double tubeLength = tubeLengthBetweenTubeSheets.Value;
//            massFlowRate /= tubesPerTubePass.Value;
//            dp = TubeHTCAndDPCalculator.CalculateSinglePhaseDp(massFlowRate, tubeDiam, tubeLength, density, bulkVisc);
//            dp = dp * tubePassesPerShellPass.Value * shellPasses.Value;
//         }
//         return dp;
//      }
//
//      private double CalculateShellSideSinglePhaseHTC(double massFlowRate, double density, double bulkVisc, double wallVisc, 
//         double thermalCond, double specificHeat) { 
//         //Donohue equation from Unit Operations of Chemical Engineering Eq.15.6
//         double diameter = tubeOuterDiameter.Value;
//         double massVelocity = CalculateAverageMassVelocity(massFlowRate);
//         double Re = diameter*massVelocity/bulkVisc;
//         double Pr = specificHeat*bulkVisc/thermalCond;
//         double Nut = 0.2*Math.Pow(Re, 0.6)*Math.Pow(Pr, 0.33)*Math.Pow(bulkVisc/wallVisc, 0.14);
//         return Nut*thermalCond/diameter;
//      }
//
//      private double CalculateShellSideSinglePhaseDp(double massFlowRate, double density, double bulkVisc) {
//         double massVelocity = CalculateAverageMassVelocity(massFlowRate);
//         double Re = tubeOuterDiameter.Value*massVelocity/bulkVisc;
//         double ds = shellInnerDiameter.Value;
//         int ncPlusOne = (int) (1.0/baffleSpacing.Value);
//         double d0 = tubeOuterDiameter.Value;
//         double y = tubePitch.Value;
//         double de = y*y*d0;
//         double f = ChartUtil.GetInterpolateValue(frictionFactorCurve, Math.Log10(Re));
//         f = Math.Pow(10, f);
//         double tubeLength = tubeLengthBetweenTubeSheets.Value;
//         double dp = f*tubeLength*massVelocity*massVelocity*ds*ncPlusOne/(2.0*density*de);
//         return dp;
//      }
//
//      private void InitializeGeometryParams() {
//         /*CalculateTubePitches();
//         CalculateFractionOfTotalTubesInCrossFlow();  
//         CalculateShellToBaffleLeakageArea();*/
//      }
//
//      internal override void PrepareGeometry() {
//         CalculateTubeDiameters();
//         //CalculateBaffleSpacing();
//
//         if (owner.BeingSpecifiedProcessVar is ProcessVarDouble) {
//            ProcessVarDouble pv = owner.BeingSpecifiedProcessVar as ProcessVarDouble; 
//            if (pv == tubeInnerDiameter) {
//               if (tubeWallThickness.Value != Constants.NO_VALUE && tubeWallThickness.IsSpecified && pv.Value != Constants.NO_VALUE) {
//                  TubeOuterDiameterChanged();
//               }
//            }
//            else if (pv == tubeOuterDiameter) {
//               TubeOuterDiameterChanged();
//            }
//            else if (pv == tubeWallThickness) {
//               if (tubeInnerDiameter.Value != Constants.NO_VALUE && tubeInnerDiameter.IsSpecified && pv.Value != Constants.NO_VALUE) {
//                  TubeOuterDiameterChanged();
//               }
//            }
//         }
//         else if (owner.BeingSpecifiedProcessVar is ProcessVarInt) {
//            ProcessVarInt pv = owner.BeingSpecifiedProcessVar as ProcessVarInt; 
//            if (pv == tubesPerTubePass || pv == tubePassesPerShellPass) {
//               //CalculateTubeToBaffleLeakageArea();
//               //CalculateAreaForFlowThroughWindowAndEquivalentDiameterOfWindow();
//            }
//         }
//
//         CalculateHeatTransferArea();
//      }
//
//      public Orientation Orientation {
//         get {return orientation;}
//         //set {orientation = value;}
//      }
//      
//      public void SpecifyShellType(ShellType st) {
//         shellType = st;
//         owner.HasBeenModified(true);
//      }
//
//      private void CalculateTubeDiameters() {
//         if (tubeOuterDiameter.Value != Constants.NO_VALUE && tubeInnerDiameter.Value != Constants.NO_VALUE) {
//            owner.Calculate(tubeWallThickness, 0.5* (tubeOuterDiameter.Value - tubeInnerDiameter.Value));
//         }
//         else if (tubeWallThickness.Value != Constants.NO_VALUE && tubeInnerDiameter.Value != Constants.NO_VALUE) {
//            owner.Calculate(tubeOuterDiameter, (tubeInnerDiameter.Value + 2.0*tubeWallThickness.Value));
//            TubeOuterDiameterChanged();
//         }
//         else if (tubeWallThickness.Value != Constants.NO_VALUE && tubeOuterDiameter.Value != Constants.NO_VALUE) {
//            owner.Calculate(tubeInnerDiameter, tubeOuterDiameter.Value - 2.0*tubeWallThickness.Value);
//            TubeOuterDiameterChanged();
//         }
//      }
//   
//      private void TubeOuterDiameterChanged() {
//         CalculateBaffleWindowArea();
//         CalculateTransversalFlowArea();
//      }
//
//      private void CalculateHeatTransferArea() {
//         double htArea = Math.PI * tubeOuterDiameter.Value * tubesPerTubePass.Value*tubePassesPerShellPass.Value*shellPasses.Value * tubeLengthBetweenTubeSheets.Value;
//         owner.Calculate(totalHeatTransferArea, htArea);
//      }
//
//      //friction of baffle window area
//      private double CalculateFractionOfBaffleWindowArea() {
//         double bcValue = baffleCut.Value;
//         double temp = 2.0 * Math.Sqrt(bcValue*(1.0-bcValue));
//         double angle = Math.Asin(temp);
//         double fractionOfCrossSectionArea = 1.0/Math.PI * angle - 4.0/Math.PI*temp*(0.5-bcValue);
//         return fractionOfCrossSectionArea;
//      }
//
//      //baffle window area
//      private void CalculateBaffleWindowArea() {
//         double fb = CalculateFractionOfBaffleWindowArea();
//         int nb = (int) fb * tubesPerTubePass.Value;
//         double ds = shellInnerDiameter.Value;
//         double d0 = tubeOuterDiameter.Value;
//         baffleWindowArea = 0.25*Math.PI*(fb*ds*ds - nb*d0*d0);
//      }
//      
//      private void CalculateTransversalFlowArea() {
//         double bs = baffleSpacing.Value;
//         double ds = shellInnerDiameter.Value;
//         double d0 = tubeOuterDiameter.Value;
//         double p = tubePitch.Value;
//         transversalFlowArea = bs*ds*(1.0-d0/p);
//      }
//
//      private double CalculateAverageMassVelocity(double massFlowRate) {
//         double gb = massFlowRate/baffleWindowArea;
//         double gc = massFlowRate/transversalFlowArea;
//         double average = Math.Sqrt(gb*gc);
//         return average;
//      }
//
//      protected HXRatingModelShellAndTubeSimple (SerializationInfo info, StreamingContext context) : base(info, context) {
//      }
//
//      public override void SetObjectData() {
//         base.SetObjectData();
//         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHXRatingModelShellAndTube", typeof(int));
//         if (persistedClassVersion == 1) {
//            this.isShellSideHot = (bool) info.GetValue("isShellSideHot", typeof(bool));
//            this.orientation = (Orientation) info.GetValue("Orientation", typeof(Orientation));
//            this.shellType = (ShellType) info.GetValue("ShellType", typeof(ShellType));
//            this.shellPasses = RecallStorableObject("ShellPasses", typeof(ProcessVarInt)) as ProcessVarInt;
//            this.tubePassesPerShellPass = RecallStorableObject("TubePassesPerShellPass", typeof(ProcessVarInt)) as ProcessVarInt;
//            this.tubesPerTubePass = RecallStorableObject("TubesPerTubePass", typeof(ProcessVarInt)) as ProcessVarInt;
//            this.tubeLengthBetweenTubeSheets = RecallStorableObject("TubeLengthBetweenTubeSheets", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.tubeInnerDiameter = RecallStorableObject("TubeInnerDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.tubeOuterDiameter = RecallStorableObject("TubeOuterDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.tubeWallThickness = RecallStorableObject("TubeWallThickness", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.tubePitch = RecallStorableObject("TubePitch", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.baffleCut = RecallStorableObject("BaffleCut", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.baffleSpacing = RecallStorableObject("BaffleSpacing", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            this.shellInnerDiameter = RecallStorableObject("ShellInnerDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
//            //this.crossFlowArea = (double) info.GetValue("CrossFlowArea", typeof(double));
//         }
//      }
//
//      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
//      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
//         base.GetObjectData(info, context);        
//         info.AddValue("ClassPersistenceVersionHXRatingModelShellAndTube", CLASS_PERSISTENCE_VERSION, typeof(int));
//         info.AddValue("isShellSideHot", this.isShellSideHot, typeof(bool));
//         info.AddValue("Orientation", this.orientation, typeof(Orientation));
//         info.AddValue("ShellType", this.shellType, typeof(ShellType));
//         info.AddValue("ShellPasses", this.shellPasses, typeof(ProcessVarInt));
//         info.AddValue("TubePassesPerShellPass", this.tubePassesPerShellPass, typeof(ProcessVarInt));
//         info.AddValue("TubesPerTubePass", this.tubesPerTubePass, typeof(ProcessVarInt));
//         info.AddValue("TubeLengthBetweenTubeSheets", this.tubeLengthBetweenTubeSheets, typeof(ProcessVarDouble));
//         info.AddValue("TubeInnerDiameter", this.tubeInnerDiameter, typeof(ProcessVarDouble));
//         info.AddValue("TubeOuterDiameter", this.tubeOuterDiameter, typeof(ProcessVarDouble));
//         info.AddValue("TubeWallThickness", this.tubeWallThickness, typeof(ProcessVarDouble));
//         info.AddValue("TubePitch", this.tubePitch, typeof(ProcessVarDouble));
//         info.AddValue("BaffleCut", this.baffleCut, typeof(ProcessVarDouble));
//         info.AddValue("BaffleSpacing", this.baffleSpacing, typeof(ProcessVarDouble));
//         info.AddValue("ShellInnerDiameter", this.shellInnerDiameter, typeof(ProcessVarDouble));
//         //info.AddValue("CrossFlowArea", this.crossFlowArea, typeof(double));
//      }
//   }
//}
