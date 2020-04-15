using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.HeatTransfer {
   
   /// <summary>
   /// Summary description for HXRatingModelPlateAndFrame.
   /// </summary>
   [Serializable]
   public class HXRatingModelPlateAndFrame : HXRatingModel {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private ProcessVarDouble channelWidth;
      private ProcessVarDouble projectedChannelLength;
      private ProcessVarDouble enlargementFactor;
      private ProcessVarDouble projectedPlateArea;
      private ProcessVarDouble actualEffectivePlateArea;
      private ProcessVarDouble platePitch;
      private ProcessVarInt numberOfPlates;
      private ProcessVarInt hotSidePasses;
      private ProcessVarInt coldSidePasses;
      //private ProcessVarDouble chevronAngle;
      private ProcessVarDouble portDiameter;

      private ProcessVarDouble horizontalPortDistance;
      private ProcessVarDouble verticalPortDistance;
      private ProcessVarDouble compressedPlatePackLength;
      //private ProcessVarDouble plateWallThickness;

      private ProcessVarDouble hotSideVelocity;
      private ProcessVarDouble coldSideVelocity;

      #region Properties
      public ProcessVarDouble ChannelWidth
      {
         get {return channelWidth;}
      }
      
      public ProcessVarDouble ProjectedChannelLength
      {
         get {return projectedChannelLength;}
      }
      
      public ProcessVarDouble EnlargementFactor
      {
         get {return enlargementFactor;}
      }
      
      public ProcessVarDouble ProjectedPlateArea
      {
         get {return projectedPlateArea;}
      }
      
      public ProcessVarDouble ActualEffectivePlateArea
      {
         get {return actualEffectivePlateArea;}
      }
      
      public ProcessVarDouble PlatePitch 
      {
         get {return platePitch;}
      }
      
//      public ProcessVarDouble ChevronAngle
//      {
//         get {return chevronAngle;}
//      }
//      
      public ProcessVarInt NumberOfPlates 
      {
         get {return numberOfPlates;}
      }

//      public ProcessVarInt NumberOfPasses 
//      {
//         get {return numberOfPasses;}
//      }
//
      public ProcessVarInt HotSidePasses 
      {
         get {return hotSidePasses;}
      }

      public ProcessVarInt ColdSidePasses 
      {
         get {return coldSidePasses;}
      }

      public ProcessVarDouble PortDiameter 
      {
         get {return portDiameter;}
      }

      public ProcessVarDouble HorizontalPortDistance 
      {
         get {return horizontalPortDistance;}
      }

      public ProcessVarDouble VerticalPortDistance {
         get {return verticalPortDistance;}
      }

      public ProcessVarDouble CompressedPlatePackLength
      {
         get {return compressedPlatePackLength;}
      }
      
      public ProcessVarDouble PlateWallThickness {
         get {return wallThickness;}
      }

      public ProcessVarDouble HotSideVelocity 
      {
         get {return hotSideVelocity;}
      }

      public ProcessVarDouble ColdSideVelocity 
      {
         get {return coldSideVelocity;}
      }

      #endregion properties

      public HXRatingModelPlateAndFrame(HeatExchanger heatExchanger) : base(heatExchanger) 
      {
         channelWidth = new ProcessVarDouble(StringConstants.CHANNEL_WIDTH, PhysicalQuantity.Length, 0.63, VarState.Specified, heatExchanger);
         projectedChannelLength = new ProcessVarDouble(StringConstants.PROJECTED_CHANNEL_LENGTH, PhysicalQuantity.Length, 1.55, VarState.Specified, heatExchanger);
         enlargementFactor = new ProcessVarDouble(StringConstants.ENLARGEMENT_FACTOR, PhysicalQuantity.Unknown, 1.25, VarState.Specified, heatExchanger);
         projectedPlateArea = new ProcessVarDouble(StringConstants.PROJECTED_PLATE_AREA, PhysicalQuantity.Area, VarState.AlwaysCalculated, heatExchanger);
         actualEffectivePlateArea = new ProcessVarDouble(StringConstants.ACTUAL_EFFECTIVE_PLATE_AREA, PhysicalQuantity.Area, VarState.AlwaysCalculated, heatExchanger);
         platePitch = new ProcessVarDouble(StringConstants.PLATE_PITCH, PhysicalQuantity.SmallLength, 0.0036, VarState.Specified, heatExchanger);
         //chevronAngle = new ProcessVarDouble(StringConstants.CHEVRON_ANGLE, PhysicalQuantity.PlaneAngle, Math.PI/4.0, VarState.Specified, heatExchanger);
         numberOfPlates = new ProcessVarInt(StringConstants.NUMBER_OF_PLATES, PhysicalQuantity.Unknown, 105, VarState.Specified, heatExchanger);
         hotSidePasses = new ProcessVarInt(StringConstants.HOT_SIDE_PASSES, PhysicalQuantity.Unknown, 1, VarState.Specified, heatExchanger);
         coldSidePasses = new ProcessVarInt(StringConstants.COLD_SIDE_PASSES, PhysicalQuantity.Unknown, 1, VarState.Specified, heatExchanger);
         
         portDiameter = new ProcessVarDouble(StringConstants.PORT_DIAMETER, PhysicalQuantity.Length, 0.2, VarState.Specified, heatExchanger);
         horizontalPortDistance = new ProcessVarDouble(StringConstants.HORIZONTAL_PORT_DISTANCE, PhysicalQuantity.Length, VarState.Specified, heatExchanger);
         verticalPortDistance = new ProcessVarDouble(StringConstants.VERTICAL_PORT_DISTANCE, PhysicalQuantity.Length, VarState.Specified, heatExchanger);
         compressedPlatePackLength = new ProcessVarDouble(StringConstants.COMPRESSED_PLATE_PACK_LENGTH, PhysicalQuantity.Length, VarState.Specified, heatExchanger);
         
         hotSideVelocity = new ProcessVarDouble(StringConstants.HOT_SIDE_VELOCITY, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);
         coldSideVelocity = new ProcessVarDouble(StringConstants.COLD_SIDE_VELOCITY, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, owner);

         wallThickness.Name = StringConstants.PLATE_WALL_THICKNESS;
         wallThickness.Value = 0.0006;

         hotSideHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         coldSideHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         totalHeatTransferCoefficient.State = VarState.AlwaysCalculated;
         totalHeatTransferArea.State = VarState.AlwaysCalculated;

         InitializeVarListAndRegisterVars();
      }

      protected override void InitializeVarListAndRegisterVars() {
         base.InitializeVarListAndRegisterVars();

         procVarList.Add(hotSideRe);
         procVarList.Add(coldSideRe);
         procVarList.Add(hotSideVelocity);
         procVarList.Add(coldSideVelocity);
         
         procVarList.Add(channelWidth);
         procVarList.Add(projectedChannelLength);
         procVarList.Add(enlargementFactor);
         procVarList.Add(projectedPlateArea);
         procVarList.Add(actualEffectivePlateArea);
         procVarList.Add(platePitch);
         //procVarList.Add(chevronAngle);
         procVarList.Add(numberOfPlates);
         procVarList.Add(hotSidePasses);
         procVarList.Add(coldSidePasses);
         procVarList.Add(portDiameter);
         procVarList.Add(horizontalPortDistance);
         procVarList.Add(verticalPortDistance);
         procVarList.Add(compressedPlatePackLength);

         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      internal override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) 
      {
         ErrorMessage retValue = null;
         if (pv == wallThickness) 
         {
            //heat exchanger design handbook page 351
            if (aValue > 0.0012 || aValue < 0.0005) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Plate wall thickness must be in the range of 0.5 to 1.2 mm");
            }
         }
         else if (pv == channelWidth || pv == projectedChannelLength) 
         {
            //heat exchanger design handbook page 351
            if (aValue > 2.2 || aValue < 0.03) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Plate width or length must be in the range of 0.03 to 2.2 m");
            }
         }
         else if (pv == platePitch) 
         {
            //heat exchanger design handbook page 351
            if (aValue < 0.0015 || aValue > 0.005) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Plate pitch must be in the range of 1.5 to 5.0 mm");
            }
         }
         else if (pv == portDiameter) 
         {
            if (aValue < 0.02 || aValue > 0.39) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Port diameter must be in the range of 2 to 39 cm");
            }
         }
         return retValue;
      }
      
      internal override ErrorMessage CheckSpecifiedValueRange(ProcessVarInt pv, int aValue) 
      {
         ErrorMessage retValue = null;
         if (pv == numberOfPlates) 
         {
            if (aValue < 10 || aValue > 700) 
            {
               retValue = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Number of plates must be in the range of 10 to 700");
            }
         }

         return retValue;
      }
      
      internal override void PostRating() 
      {
         CalculateHTUAndExchEffectiveness();
         CalculateReynoldsNumbers();
      }

      private void CalculateReynoldsNumbers() 
      {
         double massFlowRate = owner.HotSideInlet.MassFlowRate.Value;
         double t = MathUtility.Average(owner.HotSideInlet.Temperature.Value, owner.ColdSideInlet.Temperature.Value);;
         double viscosity = owner.HotSideInlet.GetLiquidViscosity(t);
         double ReHot = GetReynoldsNumber(massFlowRate, viscosity, hotSidePasses.Value);
         double density = owner.HotSideInlet.GetLiquidDensity(t);
         double velocityHot = GetMassVelocity(massFlowRate, hotSidePasses.Value)/density;

         massFlowRate = owner.ColdSideInlet.MassFlowRate.Value;
         t = MathUtility.Average(owner.ColdSideInlet.Temperature.Value, owner.ColdSideOutlet.Temperature.Value);
         viscosity = owner.HotSideInlet.GetLiquidViscosity(t);
         double ReCold = GetReynoldsNumber(massFlowRate, viscosity, coldSidePasses.Value);
         density = owner.ColdSideInlet.GetLiquidDensity(t);
         double velocityCold = GetMassVelocity(massFlowRate, coldSidePasses.Value)/density;

         owner.Calculate(hotSideVelocity, velocityHot);
         owner.Calculate(hotSideRe, ReHot);
         owner.Calculate(coldSideVelocity, velocityCold);
         owner.Calculate(coldSideRe, ReCold);
      }

      private double GetReynoldsNumber(double massFlowRate, double viscosity, int numberOfPasses) 
      {
         double De = GetChannelEquivalentDiameter();
         double massVelocity = GetMassVelocity(massFlowRate, numberOfPasses);
         double Re = De*massVelocity/viscosity;
         return Re;
      }

      private double GetChannelEquivalentDiameter() 
      {
         double width = channelWidth.Value;
         double channelGap = platePitch.Value - wallThickness.Value;
         double fai = enlargementFactor.Value;
         double De = 2.0 * width * channelGap/(channelGap + width * fai);
         return De;
      }

      private double GetMassVelocity(double massFlowRate, int numberOfPasses) 
      {
         double width = channelWidth.Value;
         double channelGap = platePitch.Value - wallThickness.Value;
         double channelsPerSide = (double)(numberOfPlates.Value - 1)/2;
         double numOfChannelsPerPass = channelsPerSide/numberOfPasses;
         double massVelocity = massFlowRate/(numOfChannelsPerPass*channelGap*width);
         return massVelocity;
      }

      internal override void PrepareGeometry() 
      {
         if (platePitch.HasValue) 
         {
            double length = platePitch.Value * (numberOfPlates.Value - 1) + wallThickness.Value;
            owner.Calculate(compressedPlatePackLength, length);
         }
         else if (compressedPlatePackLength.HasValue) 
         {
            double pitch = (compressedPlatePackLength.Value-wallThickness.Value)/(numberOfPlates.Value-1);
            owner.Calculate(platePitch, pitch);
         }

         if (portDiameter.HasValue) 
         {
            if (projectedChannelLength.HasValue) 
            {
               double distance = projectedChannelLength.Value + portDiameter.Value;
               owner.Calculate(verticalPortDistance, distance);
            }
            else if (verticalPortDistance.HasValue) 
            {
               double channelLength = verticalPortDistance.Value - portDiameter.Value;
               owner.Calculate(projectedChannelLength, channelLength);
            }
         
            if (channelWidth.HasValue) 
            {
               double distance = channelWidth.Value - portDiameter.Value;
               owner.Calculate(horizontalPortDistance, distance);
            }
            else if (horizontalPortDistance.HasValue) 
            {
               double channelWidthValue = horizontalPortDistance.Value + portDiameter.Value;
               owner.Calculate(channelWidth, channelWidthValue);
            }
         }
         
         if (channelWidth.HasValue && projectedChannelLength.HasValue) 
         {
            double projectedArea = channelWidth.Value * projectedChannelLength.Value;
            owner.Calculate(projectedPlateArea, projectedArea);
         }
         if (projectedPlateArea.HasValue && enlargementFactor.HasValue) 
         {
            double effectiveArea = projectedPlateArea.Value * enlargementFactor.Value;
            owner.Calculate(actualEffectivePlateArea, effectiveArea);
         }

         if (numberOfPlates.HasValue && actualEffectivePlateArea.HasValue) 
         {
            double effectiveNumberOfPlates = numberOfPlates.Value - 2;
            double htArea = actualEffectivePlateArea.Value * effectiveNumberOfPlates;
            owner.Calculate(totalHeatTransferArea, htArea);
         }
      }

      public override bool IsRatingCalcReady() {
         bool isReady = false;
         if (owner.HotSideInlet.MassFlowRate.HasValue && owner.ColdSideInlet.MassFlowRate.HasValue) {
            if ((owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue) 
               || (owner.HotSideInlet.Temperature.HasValue && owner.HotSideInlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.ColdSideInlet.Temperature.HasValue && owner.ColdSideInlet.Pressure.HasValue) 
               || (owner.HotSideOutlet.Temperature.HasValue && owner.HotSideOutlet.Pressure.HasValue && owner.ColdSideOutlet.Temperature.HasValue && owner.ColdSideOutlet.Pressure.HasValue)
               ) {
               
               isReady = true;
            }
         }
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
         return isReady;

      }
      
      //Calculate hot side heat transfer coefficient
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) {
         double density = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(tBulk), owner.HotSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tBulk), owner.HotSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tWall), owner.HotSideOutlet.GetLiquidViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.HotSideInlet.GetLiquidThermalConductivity(tBulk), owner.HotSideOutlet.GetLiquidThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.HotSideInlet.GetLiquidCp(tBulk), owner.HotSideOutlet.GetLiquidCp(tBulk));
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc, hotSidePasses.Value);
         double De = GetChannelEquivalentDiameter();
         
         return CalculateSinglePhaseHTC(Re, De, bulkVisc, wallVisc, thermalCond, specificHeat);
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhaseHeatTransferCoeff (double tBulk, double tWall, double massFlowRate) {
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tBulk), owner.ColdSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tWall), owner.ColdSideOutlet.GetLiquidViscosity(tWall));
         double thermalCond = MathUtility.Average(owner.ColdSideInlet.GetLiquidThermalConductivity(tBulk), owner.ColdSideOutlet.GetLiquidThermalConductivity(tBulk));
         double specificHeat = MathUtility.Average(owner.ColdSideInlet.GetLiquidCp(tBulk), owner.ColdSideOutlet.GetLiquidCp(tBulk));
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc, coldSidePasses.Value);
         double De = GetChannelEquivalentDiameter();
         return CalculateSinglePhaseHTC(Re, De, bulkVisc, wallVisc, thermalCond, specificHeat);
      }
      
      //Calculate hot side heat transfer coefficient
      public override double GetHotSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) {
         double density = MathUtility.Average(owner.HotSideInlet.GetLiquidDensity(tBulk), owner.HotSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tBulk), owner.HotSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.HotSideInlet.GetLiquidViscosity(tWall), owner.HotSideOutlet.GetLiquidViscosity(tWall));
         int numberOfPasses = hotSidePasses.Value;
         double massVelocity = GetMassVelocity(massFlowRate, numberOfPasses);
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc, numberOfPasses);

         double dpChannel = CalculateSinglePhaseChannelDP(Re, massVelocity, bulkVisc, wallVisc, density, numberOfPasses);
         double dpPort = CalculatePortDP(massFlowRate, density, numberOfPasses);
         return dpChannel+dpPort;
      }

      //Calculate hot side heat transfer coefficient
      public override double GetColdSideLiquidPhasePressureDrop (double tBulk, double tWall, double massFlowRate) {
         double density = MathUtility.Average(owner.ColdSideInlet.GetLiquidDensity(tBulk), owner.ColdSideOutlet.GetLiquidDensity(tBulk));
         double bulkVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tBulk), owner.ColdSideOutlet.GetLiquidViscosity(tBulk));
         double wallVisc = MathUtility.Average(owner.ColdSideInlet.GetLiquidViscosity(tWall), owner.ColdSideOutlet.GetLiquidViscosity(tWall));
         
         int numberOfPasses = coldSidePasses.Value;
         double massVelocity = GetMassVelocity(massFlowRate, numberOfPasses);
         double Re = GetReynoldsNumber(massFlowRate, bulkVisc, numberOfPasses);

         double dpChannel = CalculateSinglePhaseChannelDP(Re, massVelocity, bulkVisc, wallVisc, density, numberOfPasses);
         double dpPort = CalculatePortDP(massFlowRate, density, numberOfPasses);
         return dpChannel+dpPort;
      }

      private double CalculateSinglePhaseHTC(double Re, double De, double bulkViscosity, double wallViscosity, 
         double thermalCond, double specificHeat) 
      {
         //double beta = chevronAngle.Value;
         double Pr = specificHeat*bulkViscosity/thermalCond;
         double Nu = 0.0;
//         if (Re <= 400) 
//         {
//            Nu = 0.44 * Math.Pow(beta/30.0, 0.38) * Math.Pow(Re, 0.5)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
//         }
//         else if (Re >= 800) 
//         {
//            double temp = 0.728 + 0.0543*Math.Sin((2.0*Math.PI*beta/90.0+3.7));
//            Nu = (0.2668-0.006967*beta+7.244e-5*beta*beta)*Math.Pow(Re, temp) * Math.Pow(Pr, 1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
//         }
//         else 
//         {
//            double Nu1 = 0.44 * Math.Pow(beta/30.0, 0.38) * Math.Pow(400, 0.5)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
//            double Nu2 = 0.44 * Math.Pow(beta/30.0, 0.38) * Math.Pow(800, 0.5)*Math.Pow(Pr, 1.0/3.0)*Math.Pow(bulkViscosity/wallViscosity, 0.14);
//            Nu = (Nu2-Nu1)/400*(Re-400);
//         }
         
         Nu = 0.374 * Math.Pow(Re, 0.668)*Math.Pow(Pr, 0.33)*Math.Pow(bulkViscosity/wallViscosity, 0.15);
         return Nu * thermalCond/De;
      }
      
      private double CalculateSinglePhaseChannelDP(double Re, double massVelocity, double bulkViscosity, double wallViscosity, double density, int numberOfPasses) 
      {
         //double beta = chevronAngle.Value;
         double f = 0.0;
//         if (Re <= 400) 
//         {
//            f = Math.Pow(beta/30.0, 0.83) * Math.Pow(Math.Pow(30.2/Re, 5.0) + Math.Pow(6.28/Math.Pow(Re, 0.5), 5.0), 0.2);
//         }
//         else if (Re >= 800) 
//         {
//            double temp = -(0.2 + 0.0577*Math.Sin((2.0*Math.PI*beta/90.0+2.1)));
//            f = (2.917-0.1277*beta+2.016e-3*beta*beta)*Math.Pow(Re, temp);
//         }
//         else 
//         {
//            double f1 = Math.Pow(beta/30.0, 0.83) * Math.Pow(Math.Pow(30.2/Re, 5.0) + Math.Pow(6.28/Math.Pow(Re, 0.5), 5.0), 0.2);
//            double temp = -(0.2 + 0.0577*Math.Sin((2.0*Math.PI*beta/90.0+2.1)));
//            double f2 = (2.917-0.1277*beta+2.016e-3*beta*beta)*Math.Pow(Re, temp);
//            f= (f2-f1)/400*(Re-400);
//         }
         f = 2.5/Math.Pow(Re, 0.3);
         double De = GetChannelEquivalentDiameter();
         double dp = 4.0 * f * verticalPortDistance.Value*numberOfPasses*massVelocity*massVelocity/(2.0*De*density) * Math.Pow(bulkViscosity/wallViscosity, -0.17);
         return dp;
      }

      private double CalculatePortDP(double massFlowRate, double density, int numberOfPasses) 
      {
         
         double Dp = portDiameter.Value;
         double massVelocity = 4.0 * massFlowRate/(Math.PI*Dp*Dp);
         double dpPort = 1.3 * massVelocity * massVelocity * numberOfPasses/(2.0*density);
         return dpPort;
      }
      
      public override double GetTotalHeatTransferCoeff(double htcHot, double htcCold) 
      {
         double tempValue = 1.0/htcHot + 1.0/htcCold + hotSideFoulingFactor.Value + coldSideFoulingFactor.Value;
         if (includeWallEffect) 
         {
            tempValue = tempValue + wallThickness.Value/wallThermalConductivity.Value;
         }
         return 1.0/tempValue;
      }

      protected HXRatingModelPlateAndFrame (SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHXRatingModelPlateAndFrame", typeof(int));
         if (persistedClassVersion == 1) {
            this.channelWidth = RecallStorableObject("ChannelWidth", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.projectedChannelLength = RecallStorableObject("ProjectedChannelLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.enlargementFactor = RecallStorableObject("EnlargementFactor", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.projectedPlateArea = RecallStorableObject("ProjectedPlateArea", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.actualEffectivePlateArea = RecallStorableObject("ActualEffectivePlateArea", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.platePitch = RecallStorableObject("PlatePitch", typeof(ProcessVarDouble)) as ProcessVarDouble;
            //this.chevronAngle = RecallStorableObject("ChevronAngle", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.numberOfPlates = RecallStorableObject("NumberOfPlates", typeof(ProcessVarInt)) as ProcessVarInt;
            this.hotSidePasses = RecallStorableObject("HotSidePasses", typeof(ProcessVarInt)) as ProcessVarInt;
            this.coldSidePasses = RecallStorableObject("ColdSidePasses", typeof(ProcessVarInt)) as ProcessVarInt;

            this.portDiameter = RecallStorableObject("PortDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.horizontalPortDistance = RecallStorableObject("HorizontalPortDistance", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.verticalPortDistance = RecallStorableObject("VerticalPortDistance", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.compressedPlatePackLength = RecallStorableObject("CompressedPlatePackLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            
            this.hotSideVelocity = RecallStorableObject("HotSideVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.coldSideVelocity = RecallStorableObject("ColdSideVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionHXRatingModelPlateAndFrame", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("ChannelWidth", this.channelWidth, typeof(ProcessVarDouble));
         info.AddValue("ProjectedChannelLength", this.projectedChannelLength, typeof(ProcessVarDouble));
         info.AddValue("EnlargementFactor", this.enlargementFactor, typeof(ProcessVarDouble));
         info.AddValue("ProjectedPlateArea", this.projectedPlateArea, typeof(ProcessVarDouble));
         info.AddValue("ActualEffectivePlateArea", this.actualEffectivePlateArea, typeof(ProcessVarDouble));
         info.AddValue("PlatePitch", this.platePitch, typeof(ProcessVarDouble));
         //info.AddValue("ChevronAngle", this.chevronAngle, typeof(ProcessVarDouble));
         info.AddValue("NumberOfPlates", this.numberOfPlates, typeof(ProcessVarInt));
         info.AddValue("HotSidePasses", this.hotSidePasses, typeof(ProcessVarInt));
         info.AddValue("ColdSidePasses", this.coldSidePasses, typeof(ProcessVarInt));

         info.AddValue("PortDiameter", this.portDiameter, typeof(ProcessVarDouble));
         info.AddValue("HorizontalPortDistance", this.horizontalPortDistance, typeof(ProcessVarDouble));
         info.AddValue("VerticalPortDistance", this.verticalPortDistance, typeof(ProcessVarDouble));
         info.AddValue("CompressedPlatePackLength", this.compressedPlatePackLength, typeof(ProcessVarDouble));
         info.AddValue("HotSideVelocity", this.hotSideVelocity, typeof(ProcessVarDouble));
         info.AddValue("ColdSideVelocity", this.coldSideVelocity, typeof(ProcessVarDouble));
      }
   }
}

