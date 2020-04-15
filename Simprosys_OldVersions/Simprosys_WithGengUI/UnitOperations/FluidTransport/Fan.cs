using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.FluidTransport {

   //public enum FanType {Fan, Blower};
   /// <summary>
   /// Summary description for Fan.
   /// </summary>
   [Serializable]
   public class Fan : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      //Perry's 10-45. Delivery pressure
      //Fan         < 3.447 kPa
      //Blower      < 10.32 kPa
      //Compressore > 10.32 kPa
      //private FanType fanType = FanType.Fan;
      private ProcessVarDouble staticPressure;
      private ProcessVarDouble outletVelocity;
      private ProcessVarDouble totalDischargePressure;
      private ProcessVarDouble efficiency;
      private ProcessVarDouble powerInput;

      private bool includeOutletVelocityEffect;
      private CrossSectionType outletCrossSectionType;
      private ProcessVarDouble outletDiameter;

      private ProcessVarDouble outletWidth;
      private ProcessVarDouble outletHeight;
      private ProcessVarDouble outletHeightWidthRatio;

      //private CrossSectionGeometry outletGeometry;
      private ArrayList circularGeometryVarList = new ArrayList();
      private ArrayList rectangularGeometryVarList = new ArrayList();

      #region public properties
      //      public FanType FanType 
      //      {
      //         get {return fanType;}
      //      }

      public ProcessVarDouble TotalDischargePressure {
         get { return totalDischargePressure; }
      }

      public ProcessVarDouble PowerInput {
         get { return powerInput; }
      }

      public ProcessVarDouble Efficiency {
         get { return efficiency; }
      }

      public ProcessVarDouble StaticPressure {
         get { return staticPressure; }
      }

      public bool IncludeOutletVelocityEffect {
         get { return includeOutletVelocityEffect; }
      }

      public CrossSectionType OutletCrossSectionType {
         get { return outletCrossSectionType; }
      }

      public ProcessVarDouble OutletDiameter {
         get { return outletDiameter; }
      }

      public ProcessVarDouble OutletWidth {
         get { return outletWidth; }
      }

      public ProcessVarDouble OutletHeight {
         get { return outletHeight; }
      }

      public ProcessVarDouble OutletHeightWidthRatio {
         get { return outletHeightWidthRatio; }
      }

      public ProcessVarDouble OutletVelocity {
         get { return outletVelocity; }
      }
      #endregion

      public Fan(string name, UnitOperationSystem uoSys)
         : base(name, uoSys) {
         staticPressure = new ProcessVarDouble(StringConstants.STATIC_PRESSURE, PhysicalQuantity.Pressure, VarState.Specified, this);
         totalDischargePressure = new ProcessVarDouble(StringConstants.TOTAL_DISCHARGE_PRESSURE, PhysicalQuantity.Pressure, VarState.AlwaysCalculated, this);
         efficiency = new ProcessVarDouble(StringConstants.EFFICIENCY, PhysicalQuantity.Fraction, VarState.Specified, this);
         powerInput = new ProcessVarDouble(StringConstants.POWER_INPUT, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);

         outletCrossSectionType = CrossSectionType.Circle;
         outletDiameter = new ProcessVarDouble(StringConstants.OUTLET_DIAMETER, PhysicalQuantity.SmallLength, VarState.Specified, this);
         outletWidth = new ProcessVarDouble(StringConstants.WIDTH, PhysicalQuantity.SmallLength, VarState.Specified, this);
         outletHeight = new ProcessVarDouble(StringConstants.HEIGHT, PhysicalQuantity.SmallLength, VarState.Specified, this);
         outletHeightWidthRatio = new ProcessVarDouble(StringConstants.HEIGHT_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, this);

         includeOutletVelocityEffect = true;
         outletVelocity = new ProcessVarDouble(StringConstants.OUTLET_VELOCITY, PhysicalQuantity.Velocity, VarState.AlwaysCalculated, this);

         //outletGeometry = new CrossSectionGeometry(this);

         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(staticPressure);
         AddVarOnListAndRegisterInSystem(totalDischargePressure);
         AddVarOnListAndRegisterInSystem(efficiency);
         AddVarOnListAndRegisterInSystem(powerInput);
         AddVarOnListAndRegisterInSystem(outletVelocity);

         circularGeometryVarList.Add(outletDiameter);

         rectangularGeometryVarList.Add(outletWidth);
         rectangularGeometryVarList.Add(outletHeight);
         rectangularGeometryVarList.Add(outletHeightWidthRatio);

         AddVarsOnListAndRegisterInSystem(circularGeometryVarList);
      }

      //public ErrorMessage SpecifyFanType(FanType aValue) {
      //   ErrorMessage retValue = null;
      //   if (aValue != fanType) {
      //      if (fanType == FanType.Fan && staticPressure.IsSpecifiedAndHasValue && staticPressure.Value > 3477) {
      //         string msg = "Changing the fan type from Blower to Fan causes the specified value of " + staticPressure.VarTypeName + " to be out of the appropriate range.";
      //         retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsOutOfRange, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
      //         retValue.AddVarAndItsRange(staticPressure, new DoubleRange(0, 3477));
      //      }
      //      else if (fanType == FanType.Blower && staticPressure.HasValue && (staticPressure.Value < 3477 || staticPressure.Value > 10320)) {
      //         string msg = "Changing the fan type from Blower to Fan causes the specified value of " + staticPressure.VarTypeName + " to be out of the appropriate range.";
      //         retValue = new ErrorMessage(ErrorType.SpecifiedValueCausingOtherVarsOutOfRange, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
      //         retValue.AddVarAndItsRange(staticPressure, new DoubleRange(3477, 10320));
      //      }

      //      if (retValue == null) {
      //         fanType = aValue;
      //      }
      //   }
      //   return retValue;
      //}

      public ErrorMessage SpecifyOutletCrossSectionType(CrossSectionType aValue) {
         ErrorMessage retMsg = null;
         if (aValue != outletCrossSectionType) {
            ManageProcessVarList(aValue);
            CrossSectionType oldValue = outletCrossSectionType;
            outletCrossSectionType = aValue;

            try {
               HasBeenModified(true);
            }
            catch (Exception e) {
               outletCrossSectionType = oldValue;
               ManageProcessVarList(oldValue);
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      public ErrorMessage SpecifyIncludeOutletVelocityEffect(bool aValue) {
         ErrorMessage retMsg = null;
         if (aValue != includeOutletVelocityEffect) {
            bool oldValue = includeOutletVelocityEffect;
            includeOutletVelocityEffect = aValue;

            try {
               HasBeenModified(true);
            }
            catch (Exception e) {
               includeOutletVelocityEffect = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      private void ManageProcessVarList(CrossSectionType sectionType) {
         if (sectionType == CrossSectionType.Rectangle) {
            RemoveVarsOnListAndUnregisterInSystem(circularGeometryVarList);
            AddVarsOnListAndRegisterInSystem(rectangularGeometryVarList);
         }
         else if (sectionType == CrossSectionType.Circle) {
            RemoveVarsOnListAndUnregisterInSystem(rectangularGeometryVarList);
            AddVarsOnListAndRegisterInSystem(circularGeometryVarList);
         }
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == outletDiameter) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == outletWidth) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == outletHeight) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == efficiency) {
            if (aValue < 0.2 || aValue > 1.0) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " cannot be out of the range of 0.2 to 1.");
            }
         }
         else if (pv == staticPressure) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
            else if (aValue > 10320) {
               string msg = "Specified value for " + staticPressure.VarTypeName + " of the fan is out of the appropriate range.";
               retValue = new ErrorMessage(ErrorType.SpecifiedValueOutOfRange, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
               retValue.AddVarAndItsRange(staticPressure, new DoubleRange(0, 10320));
               //if (fanType == FanType.Fan && aValue >= 3477) {
               //   string msg = "Specified value for " + staticPressure.VarTypeName + " of the fan is out of the appropriate range.";
               //   retValue = new ErrorMessage(ErrorType.SpecifiedValueOutOfRange, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
               //   retValue.AddVarAndItsRange(staticPressure, new DoubleRange(0, 3477));
               //}
               //else if (fanType == FanType.Blower && (aValue < 3477 || aValue > 10320)) {
               //   string msg = "Specified value for " + staticPressure.VarTypeName + " of the fan is out of the appropriate range.";
               //   retValue = new ErrorMessage(ErrorType.SpecifiedValueOutOfRange, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
               //   retValue.AddVarAndItsRange(staticPressure, new DoubleRange(3477, 10320));
               //}
            }
         }
         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue > outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the fan inlet cannot be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue < inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the fan outlet cannot be smaller than that of the inlet.");
         }

         return retValue;
      }

      protected override void BalancePressure(ProcessStreamBase inletStream, ProcessStreamBase outletStream, ProcessVarDouble pressureDrop) {
         //pressure balance
         if (inletStream.Pressure.HasValue && outletStream.Pressure.HasValue && !pressureDrop.HasValue) {
            double inletPressure = inletStream.Pressure.Value;
            double outletPressure = outletStream.Pressure.Value;
            double pressDiff = inletPressure - outletPressure;
            string errorMsg = null;
            if (pressDiff <= 0 || pressDiff > 10320) {
               errorMsg = "Calculated value for the " + pressureDrop.VarTypeName + " of " + this.name + " is out of the appripriate range of 0 to 10.32 kPa.";
            }
            //if (fanType == FanType.Fan && (pressDiff <= 0 || pressDiff > 3477)) {
            //   errorMsg = "Calculated value for the " + pressureDrop.VarTypeName + " of " + this.name + " is out of the appripriate range of 0 to 3.477 kPa.";
            //}
            //else if (fanType == FanType.Blower && (pressDiff < 3477 || pressDiff > 10320)) {
            //   errorMsg = "Calculated value for the " + pressureDrop.VarTypeName + " of " + this.name + " is out of the appripriate range of 3.477 to 10.32 kPa.";
            //}
            if (errorMsg != null) {
               throw new InappropriateCalculatedValueException(errorMsg);
            }
            else {
               Calculate(pressureDrop, pressDiff);
            }
         }
         else if (inletStream.Pressure.HasValue && pressureDrop.HasValue && !outletStream.Pressure.HasValue) {
            double pressOutlet = inletStream.Pressure.Value - pressureDrop.Value;
            Calculate(outletStream.Pressure, pressOutlet);
         }
         else if (outletStream.Pressure.HasValue && pressureDrop.HasValue && !inletStream.Pressure.HasValue) {
            double pressInlet = outletStream.Pressure.Value + pressureDrop.Value;
            Calculate(inletStream.Pressure, pressInlet);
         }
         else if (inletStream.Pressure.HasValue && outletStream.Pressure.HasValue && pressureDrop.HasValue) {
            //over specification!!!!
         }
      }

      public override void Execute(bool propagate) {
         PreSolve();
         BalanceStreamComponents(inlet, outlet);
         //balance presssure
         BalancePressure(outlet, inlet, staticPressure);

         //BalanceSpecificEnthalpy(inlet, outlet);
         BalanceAdiabaticProcess(inlet, outlet);
         //dry gas flow balance
         if (inlet is DryingGasStream) {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;

            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingGasStreamFlow(dsInlet, dsOutlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }
         else if (inlet is ProcessStream) {
            BalanceProcessStreamFlow(inlet, outlet);
         }

         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         UpdateStreamsIfNecessary();

         Solve();
         PostSolve();
      }

      private void Solve() {
         double volumeFlowRate = Constants.NO_VALUE;
         if (inlet.VolumeFlowRate.HasValue) {
            volumeFlowRate = inlet.VolumeFlowRate.Value;
         }
         else {
            volumeFlowRate = outlet.VolumeFlowRate.Value;
         }
         double deltaP = staticPressure.Value;
         double eff = efficiency.Value;
         double density = inlet.Density.Value;
         if (inlet.Density.HasValue && outlet.Density.HasValue) {
            density = (inlet.Density.Value + outlet.Density.Value) / 2.0;
         }

         double kineticPress = Constants.NO_VALUE;
         if (includeOutletVelocityEffect) {
            if (volumeFlowRate != Constants.NO_VALUE && density != Constants.NO_VALUE) {
               //double outletArea = outletGeometry.GetArea();
               double outletArea = CalculateOutletArea();
               if (outletArea != Constants.NO_VALUE) {
                  double outletVelocityValue = volumeFlowRate / outletArea;
                  Calculate(outletVelocity, outletVelocityValue);
                  kineticPress = outletVelocityValue * outletVelocityValue * density / 2.0;
               }
            }
         }
         else {
            kineticPress = 0.0;
         }

         double totalPress = Constants.NO_VALUE;
         if (kineticPress != Constants.NO_VALUE && deltaP != Constants.NO_VALUE) {
            totalPress = deltaP + kineticPress;
            Calculate(totalDischargePressure, totalPress);
            //solveState = SolveState.PartiallySolved;
         }

         if (volumeFlowRate != Constants.NO_VALUE && totalPress != Constants.NO_VALUE &&
            eff != Constants.NO_VALUE) {
            double pi = totalPress * volumeFlowRate / eff;
            Calculate(powerInput, pi);
            solveState = SolveState.Solved;
         }
      }

      private double CalculateOutletArea() {
         double outletArea = Constants.NO_VALUE;
         if (outletCrossSectionType == CrossSectionType.Circle) {
            if (outletDiameter.HasValue) {
               double d = outletDiameter.Value;
               outletArea = 0.25 * Math.PI * d * d;
            }
         }
         else if (outletCrossSectionType == CrossSectionType.Rectangle) {
            if (outletHeight.HasValue && outletWidth.HasValue) {
               outletArea = outletHeight.Value * outletWidth.Value;
               Calculate(outletHeightWidthRatio, outletHeight.Value / outletWidth.Value);
            }
            else if (outletWidth.HasValue && outletHeightWidthRatio.HasValue) {
               outletArea = outletHeightWidthRatio.Value * outletWidth.Value * outletWidth.Value;
               Calculate(outletHeight, outletHeightWidthRatio.Value * outletWidth.Value);
            }
            else if (outletHeight.HasValue && outletHeightWidthRatio.HasValue) {
               outletArea = outletHeight.Value * outletHeight.Value / outletHeightWidthRatio.Value;
               Calculate(outletWidth, outletHeight.Value / outletHeightWidthRatio.Value);
            }
         }

         return outletArea;
      }

      protected Fan(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFan", typeof(int));
         if (persistedClassVersion == 1) {
            //this.fanType = (FanType) info.GetValue("FanType", typeof(FanType));
            this.staticPressure = RecallStorableObject("StaticPressure", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.totalDischargePressure = RecallStorableObject("TotalDischargePressure", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.efficiency = RecallStorableObject("Efficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.powerInput = RecallStorableObject("PowerInput", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.includeOutletVelocityEffect = (bool)info.GetValue("IncludeOutletVelocityEffect", typeof(bool));
            this.outletCrossSectionType = (CrossSectionType)info.GetValue("OutletCrossSectionType", typeof(CrossSectionType));
            this.outletDiameter = RecallStorableObject("OutletDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletVelocity = RecallStorableObject("OutletVelocity", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletWidth = RecallStorableObject("OutletWidth", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletHeight = RecallStorableObject("OutletHeight", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.outletHeightWidthRatio = RecallStorableObject("OutletHeightWidthRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.circularGeometryVarList = info.GetValue("CircularGeometryVarList", typeof(ArrayList)) as ArrayList;
            this.rectangularGeometryVarList = info.GetValue("RectangularGeometryVarList", typeof(ArrayList)) as ArrayList;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFan", CLASS_PERSISTENCE_VERSION, typeof(int));
         //info.AddValue("FanType", this.fanType, typeof(FanType));
         info.AddValue("StaticPressure", this.staticPressure, typeof(ProcessVarDouble));
         info.AddValue("TotalDischargePressure", this.totalDischargePressure, typeof(ProcessVarDouble));
         info.AddValue("Efficiency", this.efficiency, typeof(ProcessVarDouble));
         info.AddValue("PowerInput", this.powerInput, typeof(ProcessVarDouble));
         info.AddValue("IncludeOutletVelocityEffect", this.includeOutletVelocityEffect, typeof(bool));
         info.AddValue("OutletCrossSectionType", this.outletCrossSectionType, typeof(CrossSectionType));
         info.AddValue("OutletDiameter", this.outletDiameter, typeof(ProcessVarDouble));
         info.AddValue("OutletVelocity", this.outletVelocity, typeof(ProcessVarDouble));
         info.AddValue("OutletWidth", this.outletWidth, typeof(ProcessVarDouble));
         info.AddValue("OutletHeight", this.outletHeight, typeof(ProcessVarDouble));
         info.AddValue("OutletHeightWidthRatio", this.outletHeightWidthRatio, typeof(ProcessVarDouble));
         info.AddValue("CircularGeometryVarList", this.circularGeometryVarList, typeof(ArrayList));
         info.AddValue("RectangularGeometryVarList", this.rectangularGeometryVarList, typeof(ArrayList));
      }
   }
}

