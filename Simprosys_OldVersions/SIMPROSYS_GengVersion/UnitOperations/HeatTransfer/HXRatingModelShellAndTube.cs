using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   public enum TubeLayout{Triangular = 0, InlineSquare, RotatedSquare};
   public enum ShellType {E = 0, F, G, H, J, K, X};
   public enum ShellRatingType {BellDelaware = 0, Kern, Donohue};

   //public delegate void HotSideChangedEventHandler(HXRatingModelShellAndTube hxRatingModelShellAndTube);

   /// <summary>
   /// Summary description for HXRatingModelShellAndTube.
   /// </summary>
   [Serializable]
   public class HXRatingModelShellAndTube : HXRatingModel 
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private const double FT_MIN = 0.45; 

      private bool isShellSideHot;

      private Orientation orientation;

      private ShellType shellType;
      private ProcessVarInt shellPasses;
      //The number of tubes is selected such that the tube-side velocity for water and similar liquids range 
      //from 3 to 8 ft/s(0.9-2.4m/s) and the shell side velocity from 2 to 5 ft/s (0.6-1.5m/s). The lower 
      //velocity limit is desired to limit fouling; the higher velocity is limited to avoid erosion-corrosion
      //on the tube side, and impingement attack and flow induced viration on the shell side. When ssand, silt, 
      //and particulates are present, the velocity is kept high enough to prevent settling down.
      //Heat Exchanger Design Handbook, Chapter 5, page 232.
      private ProcessVarInt tubePassesPerShellPass;
      private ProcessVarInt tubesPerTubePass;
      private ProcessVarInt totalTubesInShell;
      
      //Standard lengths as per TEMA standard RCB-2.1 are 96, 120, 144, 196 and 240 in. Other lengths may be used.
      //Heat Exchanger Design Handbook, Chapter 5, page 232.
      private ProcessVarDouble tubeLengthBetweenTubeSheets;
      
      //Almost all heat exchanger tubes fall within the range of 1/4 in (6.35mm) to 2 in (5.8 cm) outside diameter.
      //TEMA tube sizes in terms of outside diameter are 1/4, 3/8, 1/2, 5/8, 3/4, 7/9, 1, 1.25, 1.5 and 2 in (6.35,
      //9.53, 12.70, 15.88, 19.05, 22.23, 25.40, 31.75, 38.10, and 50.80. Standard tube sizes and gages for various
      //metals are given in TEMA Table RCB-2.21. These sizes give the best performance and are most economical in 
      //many applications. Most popular are the 3/8 in nd 3/4 in sizes, and these sizes give the best all-around 
      //performance and are most economical in most applications. Use 1/4 in (6.35 mm) diameter tubes for clean 
      //fluids. For mechanical cleaning, the smallest practical size is 3/4 in (19.05). Tubes of diameter 1 in 
      //are normally used when fouling is expected because smaller ones are not suitable for mechanical cleaning, 
      //and failing film exhchangers and vaporizors generally are supplied with 1.5 and 2 in tubes.
      //Heat Exchanger Design Handbook, Chapter 5, pages 230-231.
      private ProcessVarDouble tubeOuterDiameter;
      private ProcessVarDouble tubeWallThickness;
      private ProcessVarDouble tubeInnerDiameter;

      private TubeLayout tubeLayout;
      
      //In most shell and tube exchangers, the minimum ratio of tube pitch to tube outside diameter (pitch ratio)
      //is 1.25, The minimum value is resticted to 1.25 because the tube-shhet ligament (a ligament is the 
      //portion of material between two neighboring tube holes) may become too weak for proper rolling of the tubes
      //into the tubesheet. Heat Exchanger Design Handbook, Chapter 5, pages 230-231.
      private ProcessVarDouble tubePitch;

      //Baffle cuts vary from 20% to 49% with the most common being 20-25%m and the optimum baffle cut is 
      //generally 20%, as it affords the highest heat transfer for a given pressure drop. As the baffle cut
      //increases beyond 20%, the flow pattern deviates more and more from crossflow and can result in 
      //stagnant regions or areas with lower flow velocites; both of these reduce the thermal effectiveness
      //of the bundle. Heat Exchanger Design Handbook, Chapter 5, page 235
      private ProcessVarDouble baffleCut;
      //The practical rang eof single-segmental baffle spacing is 1/5 to 1 shell diameter, though optimum 
      //could be 40-50%. TEMA Table RCB-4.52 provides maximum baffle spacing for various tube outer diameters,
      //the materials, and the corresponding maximum alowable temperature limit.
      private ProcessVarDouble baffleSpacing;
      private ProcessVarDouble entranceBaffleSpacing;
      private ProcessVarDouble exitBaffleSpacing;
      private ProcessVarInt numberOfBaffles;

      private ProcessVarDouble shellInnerDiameter;
      private ProcessVarDouble bundleToShellDiametralClearance;
      private ProcessVarDouble shellToBaffleDiametralClearance;
      private ProcessVarInt sealingStrips;

      protected bool includeNozzleEffect;
      protected ProcessVarDouble shellSideEntranceNozzleDiameter;
      protected ProcessVarDouble shellSideExitNozzleDiameter;
      protected ProcessVarDouble tubeSideEntranceNozzleDiameter;
      protected ProcessVarDouble tubeSideExitNozzleDiameter;

      //Ft factor is generally required to be greater than 0.75.
      private ProcessVarDouble ftFactor;
      private ProcessVarDouble tubeSideVelocity;
      private ProcessVarDouble shellSideVelocity;
      private ShellRatingType shellRatingType;
      private ShellHTCAndDPCalculator currentShellCalc;
      private Hashtable shellCalcTable = new Hashtable(); 

      //public event HotSideChangedEventHandler HotSideChanged;

      #region properties shown on UI
      
      public ShellType ShellType 
      {
         get {return shellType;}
      }
      
      public ProcessVarInt ShellPasses 
      {
         get {return shellPasses;}
      }

      public ProcessVarInt TubePassesPerShellPass 
      {
         get {return tubePassesPerShellPass;}
      }

      public ProcessVarInt TubesPerTubePass 
      {
         get {return tubesPerTubePass;}
      }
      
      public ProcessVarInt TotalTubesInShell 
      {
         get {return totalTubesInShell;}
      }
      
      public ProcessVarDouble TubeLengthBetweenTubeSheets 
      {
         get {return tubeLengthBetweenTubeSheets;}
      }
      
      public ProcessVarDouble TubeInnerDiameter 
      {
         get {return tubeInnerDiameter;}
      }

      public ProcessVarDouble TubeOuterDiameter 
      {
         get {return tubeOuterDiameter;}
      }

      public ProcessVarDouble TubeWallThickness 
      {
         get {return tubeWallThickness;}
      }

      public TubeLayout TubeLayout 
      {
         get {return tubeLayout;}
      }

      public ProcessVarDouble TubePitch 
      {
         get {return tubePitch;}
      }

      public ProcessVarDouble BaffleCut 
      {
         get {return baffleCut;}
      }

      public ProcessVarDouble BaffleSpacing 
      {
         get {return baffleSpacing;}
      }

      public ProcessVarDouble EntranceBaffleSpacing 
      {
         get {return entranceBaffleSpacing;}
      }
      
      public ProcessVarDouble ExitBaffleSpacing 
      {
         get {return exitBaffleSpacing;}
      }
      
      public ProcessVarInt NumberOfBaffles 
      {
         get {return numberOfBaffles;}
      }

      public ProcessVarDouble ShellInnerDiameter 
      {
         get {return shellInnerDiameter;}
      }
      
      public ProcessVarDouble ShellToBaffleDiametralClearance 
      {
         get {return shellToBaffleDiametralClearance;}
      }

      public ProcessVarDouble BundleToShellDiametralClearance
      {
         get {return bundleToShellDiametralClearance;}
      }

      public ProcessVarInt SealingStrips 
      {
         get {return sealingStrips;}
      }

      public Orientation Orientation 
      {
         get {return orientation;}
      }

      public bool IncludeNozzleEffect 
      {
         get {return includeNozzleEffect;}
      }

      public ProcessVarDouble ShellSideEntranceNozzleDiameter 
      {
         get {return shellSideEntranceNozzleDiameter;}
      }

      public ProcessVarDouble ShellSideExitNozzleDiameter 
      {
         get {return shellSideExitNozzleDiameter;}
      }

      public ProcessVarDouble TubeSideEntranceNozzleDiameter 
      {
         get {return tubeSideEntranceNozzleDiameter;}
      }

      public ProcessVarDouble TubeSideExitNozzleDiameter 
      {
         get {return tubeSideExitNozzleDiameter;}
      }
      
      public ProcessVarDouble FtFactor 
      {
         get {return ftFactor;}
      }

      public ProcessVarDouble TubeSideVelocity 
      {
         get {return tubeSideVelocity;}
      }

      public ProcessVarDouble ShellSideVelocity 
      {
         get {return shellSideVelocity;}
      }

      public ShellRatingType ShellRatingType 
      {
         get {return shellRatingType;}
      }

      public bool IsShellSideHot 
      {
         get {return isShellSideHot;}
      }

      #endregion

      public HXRatingModelShellAndTube(HeatExchanger heatExchanger) : base(heatExchanger) 
      {
         orientation = Orientation.Horizontal;
         
         shellType = ShellType.E;
         shellPasses = new ProcessVarInt(StringConstants.SHELL_PASSES, PhysicalQuantity.Unknown, 1, VarState.Specified, owner);
         tubePassesPerShellPass = new ProcessVarInt(StringConstants.TUBE_PASSES_PER_SHELL_PASS, PhysicalQuantity.Unknown, 2, VarState.Specified, owner);
         tubesPerTubePass = new ProcessVarInt(StringConstants.TUBES_PER_TUBE_PASS, PhysicalQuantity.Unknown, 51, VarState.Specified, owner);
         totalTubesInShell = new ProcessVarInt(StringConstants.TOTAL_TUBES_IN_SHELL, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         tubeInnerDiameter = new ProcessVarDouble(StringConstants.TUBE_INNER_DIAMETER, PhysicalQuantity.SmallLength, 0.02, VarState.Specified, owner);
         tubeOuterDiameter = new ProcessVarDouble(StringConstants.TUBE_OUTER_DIAMETER, PhysicalQuantity.SmallLength, 0.025, VarState.Specified, owner);
         //tubeWallThickness = new ProcessVarDouble(StringConstants.TUBE_WALL_THICKNESS, PhysicalQuantity.SmallLength, VarState.Calculated, owner);
         tubeLengthBetweenTubeSheets = new ProcessVarDouble(StringConstants.TUBE_LENGTH, PhysicalQuantity.Length, 1.5, VarState.Specified, owner);
         
         tubeLayout = TubeLayout.Triangular;
         tubePitch = new ProcessVarDouble(StringConstants.TUBE_PITCH, PhysicalQuantity.SmallLength, 0.035, VarState.Specified, owner);
         
         numberOfBaffles = new ProcessVarInt(StringConstants.NUMBER_OF_BAFFLES, PhysicalQuantity.Unknown, 8, VarState.Specified, owner);
         baffleCut = new ProcessVarDouble(StringConstants.BAFFLE_CUT, PhysicalQuantity.Fraction, 0.25, VarState.Specified, owner);
         baffleSpacing = new ProcessVarDouble(StringConstants.BAFFLE_SPACING, PhysicalQuantity.SmallLength, VarState.AlwaysCalculated, owner);
         entranceBaffleSpacing = new ProcessVarDouble(StringConstants.ENTRANCE_BAFFLE_SPACING, PhysicalQuantity.SmallLength, 0.2, VarState.Specified, owner);
         exitBaffleSpacing = new ProcessVarDouble(StringConstants.EXIT_BAFFLE_SPACING, PhysicalQuantity.SmallLength, 0.2, VarState.Specified, owner);

         shellInnerDiameter = new ProcessVarDouble(StringConstants.SHELL_INNER_DIAMETER, PhysicalQuantity.SmallLength, 0.4, VarState.Specified, owner);
         bundleToShellDiametralClearance = new ProcessVarDouble(StringConstants.BUNDLE_TO_SHELL_CLEARANCE, PhysicalQuantity.SmallLength, 0.035, VarState.Specified, owner);
         shellToBaffleDiametralClearance = new ProcessVarDouble(StringConstants.SHELL_TO_BAFFLE_CLEARANCE, PhysicalQuantity.SmallLength, 0.005, VarState.Specified, owner);
         sealingStrips = new ProcessVarInt(StringConstants.SEALING_STRIPS, PhysicalQuantity.Unknown, 6, VarState.Specified, owner);

         includeNozzleEffect = true;
         shellSideEntranceNozzleDiameter = new ProcessVarDouble(StringConstants.SHELL_ENTRANCE_NOZZLE_DIAMETER, PhysicalQuantity.SmallLength, 0.20, VarState.Specified, owner);
         shellSideExitNozzleDiameter = new ProcessVarDouble(StringConstants.SHELL_EXIT_NOZZLE_DIAMETER, PhysicalQuantity.SmallLength, 0.20, VarState.Specified, owner);
         tubeSideEntranceNozzleDiameter = new ProcessVarDouble(StringConstants.TUBE_ENTRANCE_NOZZLE_DIAMETER, PhysicalQuantity.SmallLength, 0.20, VarState.Specified, owner);
         tubeSideExitNozzleDiameter = new ProcessVarDouble(StringConstants.TUBE_EXIT_NOZZLE_DIAMETER, PhysicalQuantity.SmallLength, 0.20, VarState.Specified, owner);
         
         tubeSideVelocity = new ProcessVarDouble(StringConstants.TUBE_SIDE_VELOCITY, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         shellSideVelocity = new ProcessVarDouble(StringConstants.SHELL_SIDE_VELOCITY, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         ftFactor = new ProcessVarDouble(StringConstants.FT_FACTOR, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);

         isShellSideHot = true;
         ModifyProcessVars();
         //InitializeGeometryParams();

         InitializeVarListAndRegisterVars();
         shellRatingType = ShellRatingType.BellDelaware;
         CreateShellRatingCalculator(shellRatingType);
      }

      protected override void InitializeVarListAndRegisterVars() 
      {
         base.InitializeVarListAndRegisterVars();

         procVarList.Add(hotSideRe);
         procVarList.Add(coldSideRe);

         procVarList.Add(tubeInnerDiameter);
         procVarList.Add(tubeOuterDiameter);
         //procVarList.Add(tubeWallThickness);
         
         procVarList.Add(tubePassesPerShellPass);
         procVarList.Add(tubesPerTubePass);
         procVarList.Add(totalTubesInShell);
         procVarList.Add(shellPasses);
         procVarList.Add(tubeLengthBetweenTubeSheets);
         
         procVarList.Add(tubeSideVelocity);
         procVarList.Add(shellSideVelocity);
         procVarList.Add(ftFactor);

         procVarList.Add(shellSideEntranceNozzleDiameter);
         procVarList.Add(shellSideExitNozzleDiameter);
         procVarList.Add(tubeSideEntranceNozzleDiameter);
         procVarList.Add(tubeSideExitNozzleDiameter);
         
         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      public ErrorMessage SpecifyShellRatingType(ShellRatingType aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != shellRatingType) 
         {
            ShellRatingType oldValue = shellRatingType;
            shellRatingType = aValue;
            CreateShellRatingCalculator(shellRatingType);
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               shellRatingType = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      public ErrorMessage SpecifyOrientation(Orientation aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != orientation) 
         {
            Orientation oldValue = orientation;
            orientation = aValue;
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               orientation = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }
      
      public ErrorMessage SpecifyShellType(ShellType st) 
      {
         ErrorMessage retMsg = null;
         if (st != shellType) 
         {
            ShellType oldValue = shellType;
            shellType = st;
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               shellType = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      public ErrorMessage SpecifyTubeLayout (TubeLayout tl) 
      {
         ErrorMessage retMsg = null;
         if (tl != tubeLayout) 
         {
            TubeLayout oldValue = tubeLayout;
            tubeLayout = tl;             
            //CalculateTubePitches();
            currentShellCalc.TubeLayoutChanged();
            //above method includes the flowing calls
            //CalculateTubeRowsInOneCrossFlowSetion();
            //CalculateCrossFlowRowsInEachWindow();
            //CalculateCrossFlowArea();
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               tubeLayout = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      public ErrorMessage SpecifyIsShellSideHot(bool aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != isShellSideHot) 
         {
            bool oldValue = isShellSideHot;
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               isShellSideHot = oldValue;
               retMsg = HandleException(e);
            }

            if (retMsg != null) 
            {
               return retMsg;
            }

            isShellSideHot = aValue;
            ModifyHotColdVarLabels();
            
            SwitchToMe();
            owner.OnHXHotSideChanged();
         }
         return retMsg;
      }

      public ErrorMessage SpecifyIncludeNozzleEffect(bool aValue) 
      {
         ErrorMessage retMsg = null;
         if (aValue != includeNozzleEffect) 
         {
            //bool oldValue = includeNozzleEffect;
            includeNozzleEffect = aValue;
            
            try 
            {
               owner.HasBeenModified(true);
            }
            catch (Exception e) 
            {
               //includeNozzleEffect = oldValue;
               retMsg = owner.HandleException(e);
               string s = retMsg.Message + "\nPressure drops caused by inlet/outlet nozzles may be too big. Please increase nozzle size and try again.";
               retMsg.Message = s;
            }
         }
         return retMsg;
      }

//      private void OnHotSideChanged() 
//      {
//         if (owner.HotSideChanged != null) 
//         {
//            HotSideChanged(this);
//         }
//      }
      
      internal override void SwitchToMe() 
      {
         if (isShellSideHot) 
         {
            owner.HotSidePressureDrop.Name = StringConstants.HOT_SHELL_SIDE_PRESSURE_DROP;
            owner.ColdSidePressureDrop.Name = StringConstants.COLD_TUBE_SIDE_PRESSURE_DROP;
         }
         else 
         {
            owner.ColdSidePressureDrop.Name = StringConstants.COLD_SHELL_SIDE_PRESSURE_DROP;
            owner.HotSidePressureDrop.Name = StringConstants.HOT_TUBE_SIDE_PRESSURE_DROP;
         }
      }

      private void ModifyProcessVars() 
      {
         tubeWallThickness = wallThickness;
         tubeWallThickness.Value = Constants.NO_VALUE;
         tubeWallThickness.Name = StringConstants.TUBE_WALL_THICKNESS;

         hotSideHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         coldSideHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         totalHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         totalHeatTransferArea.State = VarState.AlwaysCalculated;

         ModifyHotColdVarLabels();
      }

      private void ModifyHotColdVarLabels() 
      {
         if (isShellSideHot) 
         {
            hotSideHeatTransferCoefficient.Name = StringConstants.SHELL_SIDE_HEAT_TRANSFER_COEFFICIENT;
            hotSideFoulingFactor.Name = StringConstants.SHELL_SIDE_FOULING_FACTOR;
            hotSideRe.Name = StringConstants.SHELL_SIDE_RE;
            //hotSideEntranceNozzleDiameter.Name = StringConstants.TUBE_ENTRANCE_NOZZLE_DIAMETER;
            //hotSideExitNozzleDiameter.Name = StringConstants.TUBE_EXIT_NOZZLE_DIAMETER;
         
            coldSideHeatTransferCoefficient.Name = StringConstants.TUBE_SIDE_HEAT_TRANSFER_COEFFICIENT;
            coldSideFoulingFactor.Name = StringConstants.TUBE_SIDE_FOULING_FACTOR;
            coldSideRe.Name = StringConstants.TUBE_SIDE_RE;
            //coldSideEntranceNozzleDiameter.Name = StringConstants.SHELL_ENTRANCE_NOZZLE_DIAMETER;
            //coldSideExitNozzleDiameter.Name = StringConstants.SHELL_EXIT_NOZZLE_DIAMETER;
         }
         else 
         {
            coldSideHeatTransferCoefficient.Name = StringConstants.SHELL_SIDE_HEAT_TRANSFER_COEFFICIENT;
            coldSideFoulingFactor.Name = StringConstants.SHELL_SIDE_FOULING_FACTOR;
            coldSideRe.Name = StringConstants.SHELL_SIDE_RE;
            //coldSideEntranceNozzleDiameter.Name= StringConstants.SHELL_ENTRANCE_NOZZLE_DIAMETER;
            //coldSideExitNozzleDiameter.Name= StringConstants.SHELL_EXIT_NOZZLE_DIAMETER;
         
            hotSideHeatTransferCoefficient.Name = StringConstants.TUBE_SIDE_HEAT_TRANSFER_COEFFICIENT;
            hotSideFoulingFactor.Name = StringConstants.TUBE_SIDE_FOULING_FACTOR;
            hotSideRe.Name = StringConstants.TUBE_SIDE_RE;
            //hotSideEntranceNozzleDiameter.Name = StringConstants.TUBE_ENTRANCE_NOZZLE_DIAMETER;
            //hotSideExitNozzleDiameter.Name = StringConstants.TUBE_EXIT_NOZZLE_DIAMETER;
         }
      }

      internal override void PostRating() 
      {
         CalculateHTUAndExchEffectiveness();
         CalcualteReynoldsNumbers();
      }
      
      private void CalcualteReynoldsNumbers() 
      {
         double viscosityHot;
         double densityHot;
         double densityCold;
         double viscosityCold;
         double massFlowRateTube;
         double massFlowRateShell;
         double viscosityTube;
         double densityTube;
         double densityShell;
         double viscosityShell;
         double tHot = MathUtility.Average(owner.HotSideInlet.Temperature.Value, owner.HotSideOutlet.Temperature.Value);
         double tCold = MathUtility.Average(owner.ColdSideInlet.Temperature.Value, owner.ColdSideOutlet.Temperature.Value);
         double massFlowRateHot = owner.HotSideInlet.MassFlowRate.Value;
         double massFlowRateCold = owner.ColdSideInlet.MassFlowRate.Value;
         
         double tBoilingHot = owner.HotSideInlet.GetBoilingPoint(owner.HotSideInlet.Pressure.Value);
         if (tHot < tBoilingHot) 
         {
            viscosityHot = owner.HotSideInlet.GetLiquidViscosity(tHot);
            densityHot = owner.HotSideInlet.GetLiquidDensity(tHot);
         }
         else 
         {
            viscosityHot = owner.HotSideInlet.GetGasViscosity(tHot);
            densityHot = owner.HotSideInlet.GetGasDensity(tHot, owner.HotSideInlet.Pressure.Value);
         }
         
         double tBoilingCold = owner.ColdSideInlet.GetBoilingPoint(owner.ColdSideInlet.Pressure.Value);
         if (tCold < tBoilingCold) 
         {
            viscosityCold = owner.ColdSideInlet.GetLiquidViscosity(tCold);
            densityCold = owner.ColdSideInlet.GetLiquidDensity(tCold);
         }
         else 
         {
            viscosityCold = owner.ColdSideInlet.GetGasViscosity(tCold);
            densityCold = owner.ColdSideInlet.GetGasDensity(tCold, owner.ColdSideInlet.Pressure.Value);
         }
         
         if (isShellSideHot) 
         {
            massFlowRateShell = massFlowRateHot;
            viscosityShell = viscosityHot;
            densityShell = densityHot;
           
            massFlowRateTube = massFlowRateCold;
            viscosityTube = viscosityCold;
            densityTube = densityCold;
         }
         else 
         {
            massFlowRateShell = massFlowRateCold;
            viscosityShell = viscosityCold;
            densityShell = densityCold;

            massFlowRateTube = massFlowRateHot;
            viscosityTube = viscosityHot;
            densityTube = densityHot;
         }
         
         massFlowRateTube /= tubesPerTubePass.Value;
         
         double tubeDiam = tubeInnerDiameter.Value;
         double massVelocity = 4.0*massFlowRateTube/(Math.PI*tubeDiam*tubeDiam);
         double ReTube = tubeDiam*massVelocity/viscosityTube;
         double velocityTube = massVelocity/densityTube;
         owner.Calculate(tubeSideVelocity, velocityTube);

         double velocityShell = currentShellCalc.GetVelocity(massFlowRateShell, densityShell);
         owner.Calculate(shellSideVelocity, velocityShell);
         double ReShell = currentShellCalc.GetReynoldsNumber(massFlowRateShell,  viscosityShell);

         if (isShellSideHot) 
         {
            owner.Calculate(hotSideRe, ReShell);
            owner.Calculate(coldSideRe, ReTube);
         }
         else 
         {
            owner.Calculate(hotSideRe, ReTube);
            owner.Calculate(coldSideRe, ReShell);
         }
      }
      
      public override bool IsParallelFlow() 
      {
         return flowDirection == FlowDirectionType.Parallel && tubePassesPerShellPass.Value == 1;
      }

      private ErrorMessage HandleException(Exception e) 
      {
         owner.EraseAllCalculatedValues();
         owner.UnitOpSystem.OnCalculationEnded();
         return new ErrorMessage(ErrorType.SimpleGeneric, "Error", e.Message);
      }
     
      internal override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         ErrorMessage retValue = null;
         if (pv == tubeOuterDiameter) 
         {
            if (aValue < 0.00635 || aValue > 0.058) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " should be in the range of 6.35 mm (1/4 inch) to 5.8 cm (2 inches)");
            }

            if (retValue != null) 
            {
               return retValue;
            }
            if (tubeLayout == TubeLayout.Triangular) 
            {
               //handbook of unit operations page 433
               if (tubePitch.HasValue && tubePitch.Value < 1.25*aValue && tubePassesPerShellPass.HasValue && shellPasses.HasValue) 
               {
                  double tubePitchCalculated = tubePitch.Value/pv.Value * aValue;
                  int totalTubes = tubesPerTubePass.Value*tubePassesPerShellPass.Value*shellPasses.Value;
                  double DsCalculated = CalculateShellInnerDiameter(totalTubes, tubePitchCalculated);
                  string msg = StringConstants.SECIFIED_VALUE_CAUSING_OTHER_VARS_INAPPROPRIATE; 
                  retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsInappropriate, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
                  //retValue.AddVarAndItsRecommendedValue(pv, aValue); 
                  retValue.AddVarAndItsRecommendedValue(tubePitch, tubePitchCalculated); 
                  retValue.AddVarAndItsRecommendedValue(shellInnerDiameter, DsCalculated); 
               }
            }
            else 
            {
               //handbook of unit operations page 433
               if (tubePitch.HasValue && aValue > tubePitch.Value - 0.0254/4.0 && tubePassesPerShellPass.HasValue && shellPasses.HasValue) 
               {
                  double tubePitchCalculated = tubePitch.Value/pv.Value * aValue;
                  int totalTubes = tubesPerTubePass.Value*tubePassesPerShellPass.Value*shellPasses.Value;
                  double DsCalculated = CalculateShellInnerDiameter(totalTubes, tubePitchCalculated);
                  string msg = StringConstants.SECIFIED_VALUE_CAUSING_OTHER_VARS_INAPPROPRIATE;
                  retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsInappropriate, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
                  //retValue.AddVarAndItsRecommendedValue(pv, aValue); 
                  retValue.AddVarAndItsRecommendedValue(tubePitch, tubePitchCalculated); 
                  retValue.AddVarAndItsRecommendedValue(shellInnerDiameter, DsCalculated); 
               }
            }
         }
         else if (pv == tubeWallThickness) 
         {
            if (aValue <= 0) 
            {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);;
            }
         }
         else if (pv == tubeInnerDiameter) 
         {
            if (aValue <= 0) 
            {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
            
            if (retValue == null && tubeOuterDiameter.HasValue && aValue >= tubeOuterDiameter.Value) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " should not be greater than " + tubeOuterDiameter.VarTypeName);
            }
         }
         else if (pv == tubePitch)
         {
            if (tubeLayout == TubeLayout.Triangular) 
            {
               //handbook of unit operations page 433
               if (tubeOuterDiameter.HasValue && aValue < 1.25 * tubeOuterDiameter.Value) 
               {
                  retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " for triangular tube layout must be greater than 1.25 times of " + tubeOuterDiameter.VarTypeName);
               }
            }
            else 
            {
               //handbook of unit operations page 433
               if (tubeOuterDiameter.HasValue && aValue < tubeOuterDiameter.Value + 0.0254/4.0) 
               {
                  retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " for square tube layout must have a minimum cleaning lane of 6.35mm (1/4 inch)");
               }
            }
            if (retValue == null) 
            {
               int totalTubes = tubesPerTubePass.Value*tubePassesPerShellPass.Value*shellPasses.Value;
               retValue = GenerateShellInnerDiameterErrorMsg(totalTubes);
            }
         }
         else if (pv == bundleToShellDiametralClearance) 
         {
            if (shellInnerDiameter.HasValue && aValue >= 0.05 * shellInnerDiameter.Value) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " must be at least less than 5% of " + shellInnerDiameter.VarTypeName);
            }
         }
         else if (pv == shellInnerDiameter) 
         {
            int numOfCalculatedTubes = CalculateNumberOfTubes(aValue, tubePitch.Value);
            int tubesPerTubePassCalculated = numOfCalculatedTubes/(tubePassesPerShellPass.Value*shellPasses.Value);
            double relativeDiff = (double) (tubesPerTubePassCalculated - totalTubesInShell.Value)/(double) tubesPerTubePassCalculated;
            if (Math.Abs(relativeDiff) >= 0.10) 
            {
               string msg = StringConstants.SECIFIED_VALUE_CAUSING_OTHER_VARS_INAPPROPRIATE; 
               retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsInappropriate, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
               //retValue.AddVarAndItsRecommendedValue(pv, aValue); 
               retValue.AddVarAndItsRecommendedValue(tubesPerTubePass, tubesPerTubePassCalculated); 
            }
         }
         else if (pv == baffleSpacing) 
         {
            //rang value is from handbook of unit operations
            if (shellInnerDiameter.HasValue && (aValue < 0.2 * shellInnerDiameter.Value || aValue > shellInnerDiameter.Value)) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " should be in the range of 1/5 to 1.0 the shell inner diameter with the optimum being 40-50%");
            }
         }
         else if (pv == baffleCut) 
         {
            if (shellRatingType == ShellRatingType.Kern && Math.Abs(aValue - 0.25) >= 1.0e-4) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " for Kern rating method is at 25% and cannot be specified");
            }
            else if (aValue != Constants.NO_VALUE && (aValue > 0.45 || aValue < 0.15)) 
            {
               retValue = owner.CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " should be in the range of 15-45% with the most common being 20-25%");
            }
         }
         return retValue;
      }
      
      internal override ErrorMessage CheckSpecifiedValueRange(ProcessVarInt pv, int aValue) 
      {
         ErrorMessage retValue = null;
         if (pv == tubesPerTubePass) 
         {
            if (tubePassesPerShellPass.HasValue && shellPasses.HasValue && tubePitch.HasValue && shellInnerDiameter.HasValue) 
            {
               int totalTubes = aValue * tubePassesPerShellPass.Value*shellPasses.Value;
               retValue = GenerateShellInnerDiameterErrorMsg(totalTubes);
            }
         }
         else if (pv == tubePassesPerShellPass) 
         {
            if (tubesPerTubePass.HasValue && shellPasses.HasValue && tubePitch.HasValue && shellInnerDiameter.HasValue) 
            {
               int totalTubes = aValue * tubesPerTubePass.Value*shellPasses.Value;
               retValue = GenerateShellInnerDiameterErrorMsg(totalTubes);
            }
         }
         else if (pv == shellPasses) 
         {
            if (tubesPerTubePass.HasValue && tubePassesPerShellPass.HasValue && tubePitch.HasValue && shellInnerDiameter.HasValue) 
            {
               int totalTubes = aValue * tubesPerTubePass.Value*tubePassesPerShellPass.Value;
               retValue = GenerateShellInnerDiameterErrorMsg(totalTubes);
            }
         }

         return retValue;
      }

      private ErrorMessage GenerateShellInnerDiameterErrorMsg(int numberOfTubes) 
      {
         ErrorMessage retValue = null;
         double DsCalculated = CalculateShellInnerDiameter(numberOfTubes, tubePitch.Value);
         double relativeDiff = (DsCalculated - shellInnerDiameter.Value)/DsCalculated ;
         if (Math.Abs(relativeDiff) >= 0.10) 
         {
            string msg = StringConstants.SECIFIED_VALUE_CAUSING_OTHER_VARS_INAPPROPRIATE; 
            retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsInappropriate, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
            //retValue.AddVarAndItsRecommendedValue(pv, aValue); 
            retValue.AddVarAndItsRecommendedValue(shellInnerDiameter, DsCalculated); 
         }
         return retValue;
      }
      
      private int CalculateNumberOfTubes(double Ds, double tubePitch) 
      {
         double ctp = 1.0;
         double cl = 1.0;
         CalculateCtpAndCl(out ctp, out cl);         
         int numOfTubes = (int) (ctp * Math.PI * Ds * Ds/(4.0 * cl * tubePitch * tubePitch));
         return numOfTubes;
      }

      private double CalculateShellInnerDiameter(int numOfTubes, double tubePitch) 
      {
         double ctp = 1.0;
         double cl = 1.0;
         CalculateCtpAndCl(out ctp, out cl);         
         double Ds = Math.Sqrt(numOfTubes * (4.0 * cl * tubePitch * tubePitch)/(ctp * Math.PI));
         return Ds;
      }

      private void CalculateCtpAndCl(out double ctp, out double cl) 
      {
         ctp = 0.93;
         if (tubePassesPerShellPass.Value == 2) 
         {
            ctp = 0.9;
         }
         else if (tubePassesPerShellPass.Value >= 3) 
         {
            ctp = 0.85;
         }

         cl = 1.0;
         if (tubeLayout == TubeLayout.Triangular) 
         {
            cl = 0.87;
         }
      }
      
      public override double GetTotalHeatTransferCoeff(double htcHot, double htcCold) 
      {
         double tempValue = 1.0;
         if (isShellSideHot)  
         {
            tempValue = 1.0/htcHot + tubeOuterDiameter.Value/(tubeInnerDiameter.Value*htcCold) + hotSideFoulingFactor.Value + coldSideFoulingFactor.Value;
         }
         else 
         {
            tempValue = tubeOuterDiameter.Value/(tubeInnerDiameter.Value*htcHot) + 1.0/htcCold + hotSideFoulingFactor.Value + coldSideFoulingFactor.Value;
         }

         if (includeWallEffect) 
         {
            tempValue = tempValue + tubeOuterDiameter.Value * Math.Log(tubeOuterDiameter.Value/tubeInnerDiameter.Value)/wallThermalConductivity.Value;
         }
         return 1.0/tempValue;
      }

      public override double GetHotSideWallTemperature(double htcHot, double tHot, double htcCold, double tCold) 
      {
         double rTotal = 1.0/GetTotalHeatTransferCoeff (htcHot, htcCold);
         double tHotWall = tHot;
         if (isShellSideHot)  
         {
            tHotWall = tHot - 1.0/htcHot/rTotal * (tHot - tCold);
         }
         else 
         {
            tHotWall = tHot - tubeOuterDiameter.Value/(tubeInnerDiameter.Value*htcHot)/rTotal * (tHot - tCold);
         }

         return tHotWall;
      }

      public override double GetColdSideWallTemperature(double htcHot, double tHot, double htcCold, double tCold) 
      {
         double rTotal = 1.0/GetTotalHeatTransferCoeff (htcHot, htcCold);
         double tColdWall = tCold;
         if (isShellSideHot)  
         {
            tColdWall = tCold + tubeOuterDiameter.Value/(tubeInnerDiameter.Value*htcCold)/rTotal * (tHot - tCold);
         }
         else 
         {
            tColdWall = tCold + 1.0/htcCold/rTotal * (tHot - tCold);
         }

         return tColdWall;
      }
      
      private void CreateShellRatingCalculator(ShellRatingType type) 
      {
         if (currentShellCalc != null) 
         {
            owner.RemoveVarsOnListAndUnregisterInSystem(currentShellCalc.ProcVarList);
         }
         currentShellCalc = shellCalcTable[type] as ShellHTCAndDPCalculator;
         if (currentShellCalc == null) 
         {
            if (type == ShellRatingType.Donohue) 
            {
               currentShellCalc = new ShellHTCAndDPCalculatorDonohue(this);
            }
            else if (type == ShellRatingType.BellDelaware) 
            {
               currentShellCalc = new ShellHTCAndDPCalculatorBellDelaware(this);
            }
            else if (type == ShellRatingType.Kern) 
            {
               currentShellCalc = new ShellHTCAndDPCalculatorKern(this);
            }
         }
         else 
         {
            owner.AddVarsOnListAndRegisterInSystem(currentShellCalc.ProcVarList);
         }
      }

      public override bool IsRatingCalcReady() 
      {
         bool isReady = false;
         if (owner.HotSideInlet.MassFlowRate.HasValue && owner.ColdSideInlet.MassFlowRate.HasValue) 
         {
            if ((owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue) 
               || (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue)
               ) 
            {
               isReady = true;
            }
         }
//         else if (owner.HotSideInlet.MassFlowRate.HasValue || owner.ColdSideInlet.MassFlowRate.HasValue) {
//            //case 1--hot inlet, outlet, cold inlet are known
//            if ((owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue) 
//               || (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue) 
//               || (owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue && owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.HotSideInlet.SpecificHeat.HasValue) 
//               || (owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue && owner.ColdSideInlet.SpecificHeat.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.SpecificHeat.HasValue && owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.HotSideOutlet.SpecificHeat.HasValue)) {
//                  
//               isReady = true;
//            }
//         }
         return isReady;
      }

      internal override void PrepareGeometry() 
      {
         currentShellCalc.PrepareGeometry();
      }

      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(tBulk), owner.HotSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tBulk), owner.HotSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tWall), owner.HotSideOutlet.GetLiquidViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.HotSideInlet.GetLiquidThermalConductivity(tBulk), owner.HotSideOutlet.GetLiquidThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.HotSideInlet.GetLiquidCp(tBulk), owner.HotSideOutlet.GetLiquidCp(tBulk));
         
         return CalculateHotSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, thermalCond, specificHeat);
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.HotSideInlet.GetGasDensity(tBulk, pressure), owner.HotSideOutlet.GetGasDensity(tBulk, pressure));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(tBulk), owner.HotSideOutlet.GetGasViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(tWall), owner.HotSideOutlet.GetGasViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.HotSideInlet.GetGasThermalConductivity(tBulk), owner.HotSideOutlet.GetGasThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.HotSideInlet.GetGasCp(tBulk), owner.HotSideOutlet.GetGasCp(tBulk));
         
         return CalculateHotSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, thermalCond, specificHeat);
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.ColdSideInlet.GetLiquidDensity(tBulk), owner.ColdSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tBulk), owner.ColdSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tWall), owner.ColdSideOutlet.GetLiquidViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.ColdSideInlet.GetLiquidThermalConductivity(tBulk), owner.ColdSideOutlet.GetLiquidThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.ColdSideInlet.GetLiquidCp(tBulk), owner.ColdSideOutlet.GetLiquidCp(tBulk));
         return CalculateColdSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, thermalCond, specificHeat);
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideVaporPhaseHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.ColdSideInlet.GetGasDensity(tBulk, pressure), owner.ColdSideOutlet.GetGasDensity(tBulk, pressure));
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tBulk), owner.ColdSideOutlet.GetGasViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tWall), owner.ColdSideOutlet.GetGasViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.ColdSideInlet.GetGasThermalConductivity(tBulk), owner.ColdSideOutlet.GetGasThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.ColdSideInlet.GetGasCp(tBulk), owner.ColdSideOutlet.GetGasCp(tBulk));
         return CalculateColdSideSinglePhaseHeatTrasferCoeff(massFlowRate, density, bulkVisc, wallVisc, 
            thermalCond, specificHeat);
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideCondensingHeatTransferCoeff (double temperature, double pressure, double massFlowRate, double inVapQuality, double outVapQuality) 
      {
         double liqDensity = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(temperature), owner.HotSideOutlet.GetLiquidDensity(temperature));
         double vapDensity = MathUtility.Average(owner.HotSideInlet.GetGasDensity(temperature, pressure), owner.HotSideOutlet.GetGasDensity(temperature, pressure));
         double liqViscosity = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(temperature), owner.HotSideOutlet.GetLiquidViscosity(temperature));
         double vapViscosity = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(temperature), owner.HotSideOutlet.GetGasViscosity(temperature));
         double liqThermalCond = MathUtility.Average(owner.HotSideInlet.GetLiquidThermalConductivity(temperature), owner.HotSideOutlet.GetLiquidThermalConductivity(temperature));
         double liqSpecificHeat = MathUtility.Average(owner.HotSideInlet.GetLiquidCp(temperature), owner.HotSideOutlet.GetLiquidCp(temperature));
         double tubeDiam = tubeOuterDiameter.Value;
         double tubeLength = tubeLengthBetweenTubeSheets.Value;

         double vapQuality = (inVapQuality + outVapQuality)/2.0;
         double inVapMassFlowRate = inVapQuality * massFlowRate;
         double outVapMassFlowRate = outVapQuality * massFlowRate;

         double htc = 0;
         double htc1 = 0;
         double htc2 = 0;

         if (isShellSideHot) 
         {
            if (orientation == Orientation.Horizontal) 
            {
               //Combined Handbook of Chemical Engineering Calculations & Perry's page 11-22 to 11-12. 
               htc1 = CondensationHeatTransferCoeffCalculator.CalculateTubeBanksHorizontalHTC_LaminarFlow(massFlowRate, tubeLength, liqDensity, liqViscosity, liqThermalCond, 
                              tubeLayout, tubePitch.Value, shellInnerDiameter.Value);
               htc2 = CondensationHeatTransferCoeffCalculator.CalculateTubeBanksHorizontalHTC_VaporShearDominated(massFlowRate, tubeDiam, tubeLength, liqDensity, vapDensity, liqViscosity, liqThermalCond, 
                              vapQuality, tubeLayout, tubePitch.Value, baffleSpacing.Value, shellInnerDiameter.Value);
               htc = htc1 > htc2 ? htc1 : htc2;
            }
            else if (orientation == Orientation.Vertical) 
            {
               //Perry's page 11-22 to 11-12
               double Re = 4.0 * massFlowRate/(Math.PI*tubeDiam*liqViscosity);
               if (Re < 2100) 
               {
                  htc = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Nusselt(massFlowRate, tubeDiam, liqDensity, liqViscosity, liqThermalCond);
               }
               else 
               {
                  htc = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, tubeDiam, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
               }
            }
         }
         else 
         {
            tubeDiam = tubeInnerDiameter.Value;
            if (orientation == Orientation.Horizontal) 
            {
               //Perry's page 11-22 to 11-12
               massFlowRate /= tubesPerTubePass.Value; 
               //htc = CondensationHeatTransferCoeffCalculator.CalculateInTubeHorizontalHTC_StratifiedFlow(massFlowRate, tubeDiam, tubeLength, liqDensity, liqViscosity, liqThermalCond);
               htc1 = CondensationHeatTransferCoeffCalculator.CalculateInTubeVerticalHTC_Boyko_Kruzhilin(massFlowRate, tubeDiam, liqDensity, vapDensity, liqViscosity, liqThermalCond, liqSpecificHeat, inVapQuality, outVapQuality);
               htc2 = CondensationHeatTransferCoeffCalculator.CalculateInTubeHorizontalHTC_Nusselt_Kerns_Modified(massFlowRate, tubeLength, liqDensity, vapDensity, liqViscosity, liqThermalCond, inVapQuality, outVapQuality);
               htc = htc1 > htc2 ? htc1 : htc2;
            }
            else if (orientation == Orientation.Vertical) 
            {
               //Perry's page 11-22 to 11-12
               htc1 = CondensationHeatTransferCoeffCalculator.CalculateVerticalTubeHTC_Dukler(massFlowRate, tubeDiam, liqDensity, vapDensity, liqViscosity, vapViscosity, liqThermalCond, liqSpecificHeat);
               htc2 = CondensationHeatTransferCoeffCalculator.CalculateInTubeVerticalHTC_Carpenter_Colburn(inVapMassFlowRate, outVapMassFlowRate, tubeDiam, liqDensity, liqViscosity, liqThermalCond, liqSpecificHeat, vapDensity, vapViscosity);
               htc = htc1 > htc2 ? htc1 : htc2;
            }
         }
         return htc;
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideBoilingHeatTransferCoeff (double tBulk, double tWall, double pressure, double massFlowRate, double heatFlux) 
      {
         double htc = 0;
         ProcessStreamBase psb = owner.ColdSideInlet;
         double criticalPressure = 0;
         if (psb is DryingMaterialStream) 
         {
            DryingMaterialStream dms = psb as DryingMaterialStream;
            Substance s = (dms.MaterialComponents as DryingMaterialComponents).Moisture.Substance;
            criticalPressure = s.CriticalProperties.CriticalPressure;
         }
         if (isShellSideHot) 
         {
            htc = currentShellCalc.CalculateNucleateBoilingHTC(heatFlux, pressure, criticalPressure);
         }
         else 
         {
            htc = BoilingHeatTransferCoeffCalculator.CalculateNucleateBoilingHTC_Mostinski(heatFlux, pressure, criticalPressure);
         }

         return htc;
      }

      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(tBulk), owner.HotSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tBulk), owner.HotSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tWall), owner.HotSideOutlet.GetLiquidViscosity(tWall));;
         
         return CalculateHotSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
      }

      //Calculate hot side heat transfer coefficient
      public override double GetHotSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.HotSideInlet.GetGasDensity(tBulk, pressure), owner.HotSideOutlet.GetGasDensity(tBulk, pressure));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(tBulk), owner.HotSideOutlet.GetGasViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(tWall), owner.HotSideOutlet.GetGasViscosity(tWall));

         return CalculateHotSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.ColdSideInlet.GetLiquidDensity(tBulk), owner.ColdSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tBulk), owner.ColdSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tWall), owner.ColdSideOutlet.GetLiquidViscosity(tWall));
         
         return CalculateColdSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideVaporPhasePressureDrop (double tBulk, double tWall, double pressure, double massFlowRate) 
      {
         double density = MathUtility.Average(owner.ColdSideInlet.GetGasDensity(tBulk, pressure), owner.ColdSideOutlet.GetGasDensity(tBulk, pressure));
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tBulk), owner.ColdSideOutlet.GetGasViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tWall), owner.ColdSideOutlet.GetGasViscosity(tWall));
         
         return CalculateColdSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
      }

      //Calculate hot side heat transfer coefficient
      public override double GetHotSideCondensingPressureDrop(double massFlowRate, double tBulk, double tWall, double pressure, double inVapQuality, double outVapQuality) 
      {
         double densityVap = MathUtility.Average(owner.HotSideInlet.GetGasDensity(tBulk, pressure), owner.HotSideOutlet.GetGasDensity(tBulk, pressure));
         double densityLiq = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(tBulk), owner.HotSideOutlet.GetLiquidDensity(tBulk));
         double viscLiq = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tBulk), owner.HotSideOutlet.GetLiquidViscosity(tBulk));
         double viscVap = MathUtility.Average(owner.HotSideInlet.GetGasViscosity(tBulk), owner.HotSideOutlet.GetGasViscosity(tBulk));
         double wallViscLiq = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tWall), owner.HotSideOutlet.GetLiquidViscosity(tWall));
         double tubeDiam;
         double dp = 0;
         double nozzleDiamIn;
         double nozzleDiamOut;
         double massFlowRateLiqIn = massFlowRate * (1.0 - inVapQuality);
         double massFlowRateLiq = massFlowRate * (1.0 - (inVapQuality + outVapQuality)/2.0);
         double massFlowRateVap = massFlowRate * (inVapQuality + outVapQuality)/2.0;

         if (isShellSideHot) 
         {
            nozzleDiamIn = shellSideEntranceNozzleDiameter.Value;
            nozzleDiamOut = shellSideExitNozzleDiameter.Value;
            tubeDiam = tubeInnerDiameter.Value;
            double dpLiq = currentShellCalc.CalculateSinglePhaseDP(massFlowRateLiqIn, densityLiq, viscLiq, wallViscLiq);
            dp =  TwoPhasePressureDropCalculator.CalculateCondensingInShellDp_Lockhart_Martinelli(massFlowRateLiq, massFlowRateVap, tubeDiam, densityLiq, densityVap, viscLiq, viscVap, dpLiq);
            dp = dp * shellPasses.Value;
         }
         else 
         {
            nozzleDiamIn = tubeSideEntranceNozzleDiameter.Value;
            nozzleDiamOut = tubeSideExitNozzleDiameter.Value;
            tubeDiam = tubeInnerDiameter.Value;
            double tubeLength = tubeLengthBetweenTubeSheets.Value;
            massFlowRateLiq /= tubesPerTubePass.Value;
            double tubeVelocity = 4.0*massFlowRateLiq /(Math.PI*tubeDiam*tubeDiam*densityLiq);
            double dpTubePass = 4.0 * tubeVelocity * tubeVelocity/2.0 * densityLiq;
            //double dpTubePass = 0.0;
            
            double dpLiquid = CalculateSinglePhaseInTubePressureDrop(massFlowRateLiqIn, tubeDiam, tubeLength, densityLiq, viscLiq, wallViscLiq);
            dpLiquid = dpLiquid + dpTubePass;
            dp =  TwoPhasePressureDropCalculator.CalculateCondensingInTubeDp_Lockhart_Martinelli(massFlowRateLiq, massFlowRateVap, tubeDiam, densityLiq, densityVap, viscLiq, viscVap, dpLiquid);
            dp *= tubePassesPerShellPass.Value * shellPasses.Value;
         }

         double dpNozzleIn = 0.0;
         double dpNozzleOut = 0.0;
         if (includeNozzleEffect) 
         {
            dpNozzleIn = CalculateNozzlePressureDrop(massFlowRate * inVapQuality, nozzleDiamIn, densityVap);
            dpNozzleOut = CalculateNozzlePressureDrop(massFlowRate * (1.0 - outVapQuality), nozzleDiamOut, densityLiq);
         }

         //dp = 0.5 * (1 + outVapMassFlowRate/inVapMassFlowRate)*dp + dpNozzleIn + dpNozzleOut;
         dp = dp + dpNozzleIn + dpNozzleOut;
         return  dp;
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideBoilingPressureDrop (double tBulk, double tWall, double pressure, double massFlowRate, double inletVaporQuality, double outletVaporQuality) 
      {
         double tubeLength = tubeLengthBetweenTubeSheets.Value;
         double tubeDiam = tubeOuterDiameter.Value;
         double densityLiquid = MathUtility.Average(owner.ColdSideInlet.GetLiquidDensity(tBulk), owner.ColdSideOutlet.GetLiquidDensity(tBulk));
         double densityVapor = MathUtility.Average(owner.ColdSideInlet.GetGasDensity(tBulk, pressure), owner.ColdSideOutlet.GetGasDensity(tBulk, pressure));
         double bulkViscLiquid = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tBulk), owner.ColdSideOutlet.GetLiquidViscosity(tBulk));
         double wallViscLiquid = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tWall), owner.ColdSideOutlet.GetLiquidViscosity(tWall));
         double bulkViscVapor = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tBulk), owner.ColdSideOutlet.GetGasViscosity(tBulk));
         double wallViscVapor = MathUtility.Average(owner.ColdSideInlet.GetGasViscosity(tWall), owner.ColdSideOutlet.GetGasViscosity(tWall));
         double dp = 0;
         double dpLiquid = 0;
         double dpVapor = 0;
         double nozzleDiamIn;
         double nozzleDiamOut;
         double vaporQuality = (inletVaporQuality + outletVaporQuality)/2.0;

         if (isShellSideHot) 
         {
            nozzleDiamIn = shellSideEntranceNozzleDiameter.Value;
            nozzleDiamOut = shellSideExitNozzleDiameter.Value;
            dpLiquid = currentShellCalc.CalculateSinglePhaseDP(massFlowRate*(1.0-vaporQuality), densityLiquid, bulkViscLiquid, wallViscLiquid);
            dpVapor = currentShellCalc.CalculateSinglePhaseDP(massFlowRate*vaporQuality, densityVapor, bulkViscVapor, wallViscVapor);
            dp = (dpLiquid + dpVapor) * shellPasses.Value;
         }
         else 
         {
            tubeDiam = tubeInnerDiameter.Value;
            nozzleDiamIn = tubeSideEntranceNozzleDiameter.Value;
            nozzleDiamOut = tubeSideExitNozzleDiameter.Value;
            massFlowRate /= tubesPerTubePass.Value;
            double tubeVelocity = 4.0*massFlowRate/(Math.PI*tubeDiam*tubeDiam*densityVapor);
            double dpTubePass = 4.0 * tubeVelocity * tubeVelocity/2.0 * densityVapor;
            dpLiquid = CalculateSinglePhaseInTubePressureDrop(massFlowRate*(1.0-vaporQuality), tubeDiam, tubeLength, densityLiquid, bulkViscLiquid, bulkViscLiquid);
            dpVapor = CalculateSinglePhaseInTubePressureDrop(massFlowRate*vaporQuality, tubeDiam, tubeLength, densityVapor, bulkViscVapor, bulkViscVapor);
            dp = (dpLiquid + dpVapor + dpTubePass) * tubePassesPerShellPass.Value * shellPasses.Value;
         }

         double dpNozzleIn = 0.0;
         double dpNozzleOut = 0.0;
         if (includeNozzleEffect) 
         {
            dpNozzleIn = CalculateNozzlePressureDrop(massFlowRate, nozzleDiamIn, densityLiquid);
            double dpNozzleOutVap = CalculateNozzlePressureDrop(massFlowRate*outletVaporQuality, nozzleDiamOut, densityVapor);
            double dpNozzleOutLiq = CalculateNozzlePressureDrop(massFlowRate*(1-outletVaporQuality), nozzleDiamOut, densityLiquid);
            dpNozzleOut = dpNozzleOutVap + dpNozzleOutLiq;
         }

         return dp + dpNozzleIn + dpNozzleOut;
      }

      private double CalculateHotSideSinglePhaseHeatTrasferCoeff(double massFlowRate, double density, double bulkVisc, double wallVisc, 
         double thermalCond, double specificHeat) 
      {
         if (isShellSideHot) 
         {
            return currentShellCalc.CalculateSinglePhaseHTC(massFlowRate, density, bulkVisc, wallVisc, 
               thermalCond, specificHeat);
         }
         else 
         {
            double tubeDiam = tubeInnerDiameter.Value;
            double tubeLength = tubeLengthBetweenTubeSheets.Value;
            massFlowRate /= tubesPerTubePass.Value;
            return CalculateInTubeSinglePhaseHTC(massFlowRate, tubeDiam, tubeLength, density, bulkVisc, wallVisc, 
               thermalCond, specificHeat);
         }
      }

      private double CalculateColdSideSinglePhaseHeatTrasferCoeff(double massFlowRate, double density, double bulkVisc, double wallVisc, 
         double thermalCond, double specificHeat) 
      {
         if (!isShellSideHot) 
         {
            return currentShellCalc.CalculateSinglePhaseHTC(massFlowRate, density, bulkVisc, wallVisc, 
               thermalCond, specificHeat);
         }
         else 
         {
            double tubeDiam = tubeInnerDiameter.Value;
            double tubeLength = tubeLengthBetweenTubeSheets.Value;
            massFlowRate /= tubesPerTubePass.Value;
            return CalculateInTubeSinglePhaseHTC(massFlowRate, tubeDiam, tubeLength, density, bulkVisc, wallVisc, 
               thermalCond, specificHeat);
         }
      }

      private double CalculateInTubeSinglePhaseHTC(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) 
      {
         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         double Re = diameter*massVelocity/bulkViscosity;

         double h = 0;
         if (Re <= 2100) 
         {
            h = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeLaminarHTC_Sieder_Tate(Re, diameter, length, bulkViscosity, wallViscosity, 
               thermalCond, specificHeat);
         }
         else if (Re > 2100 && Re < 10000) 
         {
            //h = SinglePhaseHeatTransferCoeffCalculator.CalculateTransitionHTC_Hausen(Re, diameter, length, bulkViscosity, wallViscosity, 
            //         thermalCond, specificHeat);
            double h1 = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeLaminarHTC_Sieder_Tate(2100, diameter, length, bulkViscosity, wallViscosity, 
               thermalCond, specificHeat);
            double h2 = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeTurbulentHTC_Colburn(10000, diameter, bulkViscosity, wallViscosity, 
               thermalCond, specificHeat);
            h = (h2 - h1)/(10000 - 2100) * (Re - 2100);
         }
         else if (Re >= 10000) 
         {
            h = SinglePhaseHeatTransferCoeffCalculator.CalculateTubeTurbulentHTC_Colburn(Re, diameter, bulkViscosity, wallViscosity, 
               thermalCond, specificHeat);
         }
         return h;
      }

      private double CalculateSinglePhaseInTubePressureDrop(double massFlowRate, double diameter, double length, double density, double bulkViscosity, double wallViscosity) 
      {
         double massVelocity = 4.0*massFlowRate/(Math.PI*diameter*diameter);
         double Re = diameter*massVelocity/bulkViscosity;
         double f = FrictionFactorCalculator.CalculateFrictionFactor(Re);
         
         double velocity = massVelocity/density;
         double dp = 2.0*f*length*density*velocity*velocity/(diameter*Math.Pow(bulkViscosity/wallViscosity, 0.14));
         return dp;
      }

      //Calculate hot side heat transfer coefficient
      private double CalculateHotSideSinglePhasePressureDrop (double massFlowRate, double density, double bulkVisc, double wallVisc) 
      {
         double dp = 0;
         if (isShellSideHot) 
         { 
            dp = CalculateShellSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
         } 
         else 
         {
            dp = CalculateTubeSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
         }
         return dp;
      }

      //Calculate cold side pressure drop
      private double CalculateColdSideSinglePhasePressureDrop (double massFlowRate, double density, double bulkVisc, double wallVisc) 
      {
         double dp = 0;
         if (!isShellSideHot) 
         { //if shell side is not the hot side it is the cold side
            dp = CalculateShellSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
         } 
         else 
         {
            dp = CalculateTubeSideSinglePhasePressureDrop(massFlowRate, density, bulkVisc, wallVisc);
         }
         return dp;
      }

      private double CalculateShellSideSinglePhasePressureDrop(double massFlowRate, double density, double bulkVisc, double wallVisc) 
      {
         double nozzleDiamIn = shellSideEntranceNozzleDiameter.Value;
         double nozzleDiamOut = shellSideExitNozzleDiameter.Value;
//         if (isShellSideHot) 
//         {
//            nozzleDiamIn = shellSideEntranceNozzleDiameter.Value;
//            nozzleDiamOut = shellSideExitNozzleDiameter.Value;
//         }
//         else 
//         {
//            nozzleDiamIn = tubeSideEntranceNozzleDiameter.Value;
//            nozzleDiamOut = tubeSideExitNozzleDiameter.Value;
//         }

         double dpShell = currentShellCalc.CalculateSinglePhaseDP(massFlowRate, density, bulkVisc, wallVisc);
         
         double dpNozzleIn = 0.0;
         double dpNozzleOut = 0.0;
         if (includeNozzleEffect) 
         {
            dpNozzleIn = CalculateNozzlePressureDrop(massFlowRate, nozzleDiamIn, density);
            dpNozzleOut = CalculateNozzlePressureDrop(massFlowRate, nozzleDiamOut, density);
         }
         
         double dpTotal = dpShell * shellPasses.Value + dpNozzleIn + dpNozzleOut;
         return dpTotal;
      }

      private double CalculateTubeSideSinglePhasePressureDrop(double massFlowRate, double density, double bulkVisc, double wallVisc) 
      {
         double tubeDiam = tubeInnerDiameter.Value;
         double tubeLength = tubeLengthBetweenTubeSheets.Value;
            
         double nozzleDiamIn = tubeSideEntranceNozzleDiameter.Value;
         double nozzleDiamOut = tubeSideExitNozzleDiameter.Value;
//         if (isShellSideHot) 
//         {
//            nozzleDiamIn = coldSideEntranceNozzleDiameter.Value;
//            nozzleDiamOut = coldSideExitNozzleDiameter.Value;
//         }
//         else 
//         {
//            nozzleDiamIn = hotSideEntranceNozzleDiameter.Value;
//            nozzleDiamOut = hotSideExitNozzleDiameter.Value;
//         }

         double dpNozzleIn = 0.0;
         double dpNozzleOut = 0.0;
         if (includeNozzleEffect) 
         {
            dpNozzleIn = CalculateNozzlePressureDrop(massFlowRate, nozzleDiamIn, density);
            dpNozzleOut = CalculateNozzlePressureDrop(massFlowRate, nozzleDiamOut, density);
         }
            
         massFlowRate /= tubesPerTubePass.Value;
         double dpTube = CalculateSinglePhaseInTubePressureDrop(massFlowRate, tubeDiam, tubeLength, density, bulkVisc, wallVisc);
         
         double tubeVelocity = 4.0*massFlowRate/(Math.PI*tubeDiam*tubeDiam*density);
         double dpTubePass = 4.0  * density * tubeVelocity * tubeVelocity/2.0;
            
         double dpTotal= (dpTube + dpTubePass) * tubePassesPerShellPass.Value * shellPasses.Value + dpNozzleIn + dpNozzleOut;
         return dpTotal;
      }

      private double CalculateNozzlePressureDrop (double massFlowRate, double nozzleDiam, double density) 
      {
         double nozzleVelocity = 4.0*massFlowRate/(Math.PI*nozzleDiam*nozzleDiam*density);
         double dpNozzle = 1.5 * density * nozzleVelocity * nozzleVelocity/2.0; 
         return dpNozzle;
      }

      //Calculate the correction factor given the shell passes and temperatures:
      public override double GetFtFactor (double hotIn, double hotOut, double coldIn, double coldOut) 
      {
         
         int shellPs = shellPasses.Value; 
         bool counterCurrent = flowDirection == FlowDirectionType.Counter ? true : false;
         //ShellType shellType = shellType; 
         int tubePsPerShellPass = tubePassesPerShellPass.Value;
         int tubePasses = shellPs*tubePsPerShellPass;
         double Ft = 1.0;
   
         if ((shellType == ShellType.E || shellType == ShellType.F) && tubePsPerShellPass == 1) {
            return 1.0;
         }
   
         //Note: P and R may have "out of range" values So we always ensure that we return something
         //acceptable.

         double Rdenom = coldOut-coldIn;
         double Pdenom = hotIn-coldIn;
   
         if (Rdenom < 1.0e-8 || Pdenom < 1.0e-8) {
            return 1.0;  //Don't know, really.
         }
   
         double R = (hotIn-hotOut)/Rdenom;
         double P = (coldOut-coldIn)/Pdenom;

         if (shellType == ShellType.E || shellType == ShellType.F) {
            if (tubePasses == 1 && shellPs == 1 && !counterCurrent) {
               if (P > 0.99) {
                  Ft = FT_MIN;
               }
               else if (P < 0.001) {
                  Ft = 1.0;
               }
               else {
                  double logarg = 1.0-P*R-P;
                  if (logarg<0.0)
                     Ft = FT_MIN;
                  else {
                     double delta = 1.0;
                     if (Math.Abs(R-1.0) < 1.0e-6) {
                        delta = (1.0-P)/P;
                     }
                     else {
                        delta = (R-1.0)/Math.Log((1.0-P)/(1.0-P*R));
                     }
                     Ft = -(R+1.0)/(delta*Math.Log (logarg));
                  }
               }
            }
            else {
               if (P > 0.9999) {
                  Ft = FT_MIN;   
               }
               else if (P < 0.00001 || R < 0.0001) {
                  Ft = 1.0;   
               }
               else if ( Math.Abs(R-1.0) < 0.0001 ) {
                  P = P / (shellPs - shellPs*P + P);
                  double logarg = ( (2.0/P-1.0-R+Math.Sqrt(R*R+1.0)) / (2.0/P-1.0-R-Math.Sqrt(R*R+1)) );
                  if (logarg > 1e-30) {
                     Ft = ( 1.414 * (P/(1.0-P)) ) / Math.Log (logarg);
                  }
                  else {
                     Ft = FT_MIN;  //Don't really know.
                  }
               }
               else {
                  double X = (1.0-R*P)/(1.0-P);
                  if (X < 0.0) {
                     return FT_MIN;
                  }
                  P = ( 1.0-Math.Pow(X,1.0/shellPs) ) / ( R-Math.Pow(X,1.0/shellPs) );
                  if (P > 0.9999) {
                     Ft = FT_MIN;
                  }
                  else if (P<0.00001) {
                     Ft = 1.0;
                  }
                  else {
                     double logarg1 = (1.0-P)/(1.0-R*P);
                     double logarg2 = (2.0/P-1.0-R+Math.Sqrt(R*R+1.0)) / (2.0/P-1.0-R-Math.Sqrt(R*R+1.0));

                     if ( (logarg1<=0.0) || (logarg2<=0.0)) {
                        Ft = FT_MIN;  //Don't really know.
                     }
                     else {
                        Ft = (Math.Sqrt(R*R+1)/(R-1.0) * Math.Log (logarg1) ) / Math.Log ( logarg2 );
                     }
                  }
               }
            } 
         }

         if (Ft > 1.0) Ft = 1.0;
         if (Ft < FT_MIN) Ft = FT_MIN;

         return Ft;
      }

      protected HXRatingModelShellAndTube (SerializationInfo info, StreamingContext context) : base(info, context) {
         //shellHtcDpCalc = new ShellHTCAndDPCalculator(this);
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHXRatingModelShellAndTube", typeof(int));
         if (persistedClassVersion == 1) {
            this.isShellSideHot = (bool) info.GetValue("IsShellSideHot", typeof(bool));
            this.orientation = (Orientation) info.GetValue("Orientation", typeof(Orientation));
            this.shellType = (ShellType) info.GetValue("ShellType", typeof(ShellType));
            this.shellPasses = RecallStorableObject("ShellPasses", typeof(ProcessVarInt)) as ProcessVarInt;
            this.tubePassesPerShellPass = RecallStorableObject("TubePassesPerShellPass", typeof(ProcessVarInt)) as ProcessVarInt;
            this.tubesPerTubePass = RecallStorableObject("TubesPerTubePass", typeof(ProcessVarInt)) as ProcessVarInt;
            this.totalTubesInShell = RecallStorableObject("TotalTubesInShell", typeof(ProcessVarInt)) as ProcessVarInt;
            this.tubeLengthBetweenTubeSheets = RecallStorableObject("TubeLengthBetweenTubeSheets", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.tubeInnerDiameter = RecallStorableObject("TubeInnerDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.tubeOuterDiameter = RecallStorableObject("TubeOuterDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            //since tubeWallThickness points to the wallThickness of the base class we only need to recall the reference back
            //this.tubeWallThickness = RecallStorableObject("TubeWallThickness", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.tubeWallThickness = info.GetValue("TubeWallThickness", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            this.tubeLayout = (TubeLayout) info.GetValue("TubeLayout", typeof(TubeLayout));
            this.tubePitch = RecallStorableObject("TubePitch", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.baffleCut = RecallStorableObject("BaffleCut", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.baffleSpacing = RecallStorableObject("BaffleSpacing", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.entranceBaffleSpacing = RecallStorableObject("EntranceBaffleSpacing", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.exitBaffleSpacing = RecallStorableObject("ExitBaffleSpacing", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.numberOfBaffles = RecallStorableObject("NumberOfBaffles", typeof(ProcessVarInt)) as ProcessVarInt;
            this.shellInnerDiameter = RecallStorableObject("ShellInnerDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.bundleToShellDiametralClearance = RecallStorableObject("BundleToShellDiametralClearance", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.shellToBaffleDiametralClearance = RecallStorableObject("ShellToBaffleDiametralClearance", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.sealingStrips = RecallStorableObject("SealingStrips", typeof(ProcessVarInt)) as ProcessVarInt;
            
            this.includeNozzleEffect = (bool) info.GetValue("IncludeNozzleEffect", typeof(bool));
            this.shellSideEntranceNozzleDiameter = RecallStorableObject("ShellSideEntranceNozzleDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.shellSideExitNozzleDiameter = RecallStorableObject("ShellSideExitNozzleDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.tubeSideEntranceNozzleDiameter = RecallStorableObject("TubeSideEntranceNozzleDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.tubeSideExitNozzleDiameter = RecallStorableObject("TubeSideExitNozzleDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            this.tubeSideVelocity = RecallStorableObject("TubeSideVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.shellSideVelocity = RecallStorableObject("ShellSideVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.ftFactor = RecallStorableObject("FtFactor", typeof(ProcessVarDouble)) as ProcessVarDouble;

            this.shellRatingType = (ShellRatingType) info.GetValue("ShellRatingType", typeof(ShellRatingType));
            this.shellCalcTable = RecallHashtableObject("ShellCalcTable");
            this.currentShellCalc = RecallStorableObject("CurrentShellCalc", typeof(ShellHTCAndDPCalculator)) as ShellHTCAndDPCalculator;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionHXRatingModelShellAndTube", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("IsShellSideHot", this.isShellSideHot, typeof(bool));
         info.AddValue("Orientation", this.orientation, typeof(Orientation));
         info.AddValue("ShellType", this.shellType, typeof(ShellType));
         info.AddValue("ShellPasses", this.shellPasses, typeof(ProcessVarInt));
         info.AddValue("TubePassesPerShellPass", this.tubePassesPerShellPass, typeof(ProcessVarInt));
         info.AddValue("TubesPerTubePass", this.tubesPerTubePass, typeof(ProcessVarInt));
         info.AddValue("TotalTubesInShell", this.totalTubesInShell, typeof(ProcessVarInt));
         info.AddValue("TubeLengthBetweenTubeSheets", this.tubeLengthBetweenTubeSheets, typeof(ProcessVarDouble));
         info.AddValue("TubeInnerDiameter", this.tubeInnerDiameter, typeof(ProcessVarDouble));
         info.AddValue("TubeOuterDiameter", this.tubeOuterDiameter, typeof(ProcessVarDouble));
         info.AddValue("TubeWallThickness", this.tubeWallThickness, typeof(ProcessVarDouble));
         info.AddValue("TubeLayout", this.tubeLayout, typeof(TubeLayout));
         info.AddValue("TubePitch", this.tubePitch, typeof(ProcessVarDouble));
         info.AddValue("BaffleCut", this.baffleCut, typeof(ProcessVarDouble));
         info.AddValue("BaffleSpacing", this.baffleSpacing, typeof(ProcessVarDouble));
         info.AddValue("EntranceBaffleSpacing", this.entranceBaffleSpacing, typeof(ProcessVarDouble));
         info.AddValue("ExitBaffleSpacing", this.exitBaffleSpacing, typeof(ProcessVarDouble));
         info.AddValue("NumberOfBaffles", this.numberOfBaffles, typeof(ProcessVarInt));
         info.AddValue("ShellInnerDiameter", this.shellInnerDiameter, typeof(ProcessVarDouble));
         info.AddValue("BundleToShellDiametralClearance", this.bundleToShellDiametralClearance, typeof(ProcessVarDouble));
         info.AddValue("ShellToBaffleDiametralClearance", this.shellToBaffleDiametralClearance, typeof(ProcessVarDouble));
         info.AddValue("SealingStrips", this.sealingStrips, typeof(ProcessVarInt));
         
         info.AddValue("IncludeNozzleEffect", this.includeNozzleEffect, typeof(bool));
         info.AddValue("ShellSideEntranceNozzleDiameter", this.shellSideEntranceNozzleDiameter, typeof(ProcessVarDouble));
         info.AddValue("ShellSideExitNozzleDiameter", this.shellSideExitNozzleDiameter, typeof(ProcessVarDouble));
         info.AddValue("TubeSideEntranceNozzleDiameter", this.tubeSideEntranceNozzleDiameter, typeof(ProcessVarDouble));
         info.AddValue("TubeSideExitNozzleDiameter", this.tubeSideExitNozzleDiameter, typeof(ProcessVarDouble));
         
         info.AddValue("TubeSideVelocity", this.tubeSideVelocity, typeof(ProcessVarDouble));
         info.AddValue("ShellSideVelocity", this.shellSideVelocity, typeof(ProcessVarDouble));
         info.AddValue("FtFactor", this.ftFactor, typeof(ProcessVarDouble));
         info.AddValue("ShellRatingType", shellRatingType, typeof(ShellRatingType));
         info.AddValue("ShellCalcTable", shellCalcTable, typeof(Hashtable));
         info.AddValue("CurrentShellCalc", currentShellCalc, typeof(ShellHTCAndDPCalculator));
      }
   }
}
