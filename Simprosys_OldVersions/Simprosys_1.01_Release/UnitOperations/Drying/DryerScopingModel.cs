using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.Drying {

   /// <summary>
   /// Summary description for DryerScopingModel.
   /// </summary>
   [Serializable]
   public class DryerScopingModel : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      protected Dryer owner;
      protected ArrayList procVarList = new ArrayList();

      //protected DryerType dryerType;
      //protected FlowDirection flowDirection;

      //Perry's 12-52
      //Rotary Dryer air mass velocity 0.5 5.0 kg/s.m2
      //Typical gas velocity, Flash Dryer 20 m/s, Fluid Bed 0.5 m/s, 
      //Cocurrent Rotary 3 m/s, Counter current rotary 2 m/s
      protected ProcessVarDouble gasVelocity;
      protected CrossSectionType crossSectionType;

      protected ProcessVarDouble length;
      //Perry's 12-52
      //Rotary Dryer diameter 0.3 to 3 m, L/D 4 to 10, typical 8
      protected ProcessVarDouble diameter;
      protected ProcessVarDouble lengthDiameterRatio;

      protected ProcessVarDouble height;
      protected ProcessVarDouble width;
      //Plug Flow Fluid Bed Dryer, L/W = 10 - 40
      protected ProcessVarDouble lengthWidthRatio;
      protected ProcessVarDouble heightWidthRatio;

      #region public properties
      internal ArrayList ProcVarList {
         get { return procVarList; }
      }

      public ProcessVarDouble GasVelocity {
         get { return gasVelocity; }
      }

      //      public FlowDirection FlowDirection {
      //         get {return flowDirection;}
      //      }
      //      
      //      public DryerType DryerType {
      //         get {return dryerType;}
      //      }
      //      
      public CrossSectionType CrossSectionType {
         get { return crossSectionType; }
      }

      public ProcessVarDouble Height {
         get { return height; }
      }

      public ProcessVarDouble Diameter {
         get { return diameter; }
      }

      public ProcessVarDouble LengthDiameterRatio {
         get { return lengthDiameterRatio; }
      }

      public ProcessVarDouble Width {
         get { return width; }
      }

      public ProcessVarDouble Length {
         get { return length; }
      }

      public ProcessVarDouble LengthWidthRatio {
         get { return lengthWidthRatio; }
      }

      public ProcessVarDouble HeightWidthRatio {
         get { return heightWidthRatio; }
      }
      #endregion

      public DryerScopingModel(Dryer dryer)
         : base() {
         this.owner = dryer;
         crossSectionType = CrossSectionType.Circle;
         gasVelocity = new ProcessVarDouble(StringConstants.GAS_VELOCITY, PhysicalQuantity.Velocity, 2.0, VarState.Specified, dryer);

         diameter = new ProcessVarDouble(StringConstants.DIAMETER, PhysicalQuantity.Length, VarState.Specified, dryer);
         height = new ProcessVarDouble(StringConstants.HEIGHT, PhysicalQuantity.Length, VarState.Specified, dryer);
         lengthDiameterRatio = new ProcessVarDouble(StringConstants.LENGTH_DIAMETER_RATIO, PhysicalQuantity.Unknown, 8.0, VarState.Specified, dryer);
         width = new ProcessVarDouble(StringConstants.WIDTH, PhysicalQuantity.Length, VarState.Specified, dryer);
         length = new ProcessVarDouble(StringConstants.LENGTH, PhysicalQuantity.Length, VarState.Specified, dryer);
         lengthWidthRatio = new ProcessVarDouble(StringConstants.LENGTH_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, dryer);
         heightWidthRatio = new ProcessVarDouble(StringConstants.HEIGHT_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, dryer);

         InitializeVarListAndRegisterVars();
      }

      protected virtual void InitializeVarListAndRegisterVars() {
         procVarList.Add(diameter);
         procVarList.Add(gasVelocity);
         procVarList.Add(height);
         procVarList.Add(lengthDiameterRatio);
         procVarList.Add(width);
         procVarList.Add(length);
         procVarList.Add(lengthWidthRatio);
         procVarList.Add(heightWidthRatio);

         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }

      public ErrorMessage SpecifyCrossSectionType(CrossSectionType aValue) {
         if (aValue != crossSectionType) {
            crossSectionType = aValue;
            //owner.HasBeenModified(true);
            DoScopingCalculation();
         }
         return null;
      }

      //      public ErrorMessage SpecifyFlowDirection(FlowDirection aValue) {
      //         flowDirection = aValue;
      //         owner.HasBeenModified(true);
      //         return null;
      //      }
      //      
      //      public ErrorMessage SpecifyDryerType(DryerType aValue) {
      //         ErrorMessage retValue = null;
      //         if (dryerType != aValue)
      //         {
      //            dryerType = aValue;
      //            if (aValue == DryerType.Rotary) 
      //            {
      //               if (!gasVelocity.HasValue) 
      //               {
      //                  owner.Specify(gasVelocity, 2.0);
      //               }
      //               else if (gasVelocity.Value < 0.5 || gasVelocity.Value > 5)
      //               {
      //                  retValue = new ErrorMessage(ErrorType.Warning, "Possible Inappropriate Specified Value", "Specified dryer type will cause the values of some variables inappropriate");
      //                  retValue.AddVarAndItsRecommendedValue(gasVelocity, 2.0);
      //               }
      //               
      //               if (!lengthDiameterRatio.HasValue) 
      //               {
      //                  owner.Specify(lengthDiameterRatio, 8);
      //               }
      //               else if (lengthDiameterRatio.Value < 1 || lengthDiameterRatio.Value > 50) 
      //               {
      //                  if (retValue == null) 
      //                  {
      //                     retValue = new ErrorMessage(ErrorType.Warning, "Possible Inappropriate Specified Value", "Specified length/diameter ratio is out of recommended range, 2 to 18");
      //                  }
      //                  retValue.AddVarAndItsRecommendedValue(lengthDiameterRatio, 8);
      //               }
      //            }
      //            if (aValue == DryerType.Pneumatic) 
      //            {
      //               if (!gasVelocity.HasValue) 
      //               {
      //                  owner.Specify(gasVelocity, 20.0);
      //               }
      //               else if (gasVelocity.Value < 15 || gasVelocity.Value > 45) 
      //               {
      //                  retValue = new ErrorMessage(ErrorType.Warning, "Possible Inappropriate Specified Value", "Specified dryer type will cause the values of some variables inappropriate");
      //                  retValue.AddVarAndItsRecommendedValue(gasVelocity, 20.0);
      //               }
      //            }
      //            else if (aValue == DryerType.FluidizedBed) 
      //               if (!gasVelocity.HasValue) 
      //               {
      //                  owner.Specify(gasVelocity, 0.5);
      //               }
      //               else if (gasVelocity.Value < 0.2 || gasVelocity.Value > 3.0) 
      //               {
      //                  retValue = new ErrorMessage(ErrorType.Warning, "Possible Inappropriate Specified Value", "Specified dryer type will cause the values of some variables inappropriate");
      //                  retValue.AddVarAndItsRecommendedValue(gasVelocity, 2.0);
      //               }
      //         }
      //         return null;
      //      }
      //      
      internal virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == lengthDiameterRatio) {
            if (aValue <= 0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == lengthWidthRatio) {
            if (aValue <= 0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == gasVelocity) {
            if (aValue <= 0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == diameter) {
            if (aValue <= 0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         else if (pv == length) {
            if (aValue <= 0) {
               retValue = owner.CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }

         return retValue;
      }

      internal virtual void PrepareGeometry() {
      }

      public virtual bool IsRatingCalcReady() {
         return true;
      }

      public void DoScopingCalculation() {
         if (!owner.GasInlet.VolumeFlowRate.HasValue) {
            return;
         }

         double lengthValue;
         double gasVelocityValue;
         double crossSectionArea;

         if (crossSectionType == CrossSectionType.Circle) {
            double diameterValue;
            double lengthDiameterRatioValue;
            if (gasVelocity.HasValue) {
               crossSectionArea = owner.GasInlet.VolumeFlowRate.Value / gasVelocity.Value;
               diameterValue = Math.Sqrt(4.0 * crossSectionArea / Math.PI);
               owner.Calculate(diameter, diameterValue);
               if (lengthDiameterRatio.HasValue) {
                  lengthValue = diameterValue * lengthDiameterRatio.Value;
                  owner.Calculate(length, lengthValue);
               }
               else if (length.HasValue) {
                  lengthDiameterRatioValue = diameterValue / length.Value;
                  owner.Calculate(lengthDiameterRatio, lengthDiameterRatioValue);
               }
            }
            else if (diameter.HasValue) {
               crossSectionArea = 0.25 * Math.PI * diameter.Value;
               gasVelocityValue = owner.GasInlet.VolumeFlowRate.Value / crossSectionArea;
               owner.Calculate(gasVelocity, gasVelocityValue);
               if (lengthDiameterRatio.HasValue) {
                  lengthValue = diameter.Value * lengthDiameterRatio.Value;
                  owner.Calculate(length, lengthValue);
               }
               else if (length.HasValue) {
                  lengthDiameterRatioValue = length.Value / diameter.Value;
                  owner.Calculate(lengthDiameterRatio, lengthDiameterRatioValue);
               }
            }
            else if (length.HasValue) {
               if (lengthDiameterRatio.HasValue) {
                  diameterValue = length.Value / lengthDiameterRatio.Value;
                  owner.Calculate(diameter, diameterValue);
                  crossSectionArea = 0.25 * Math.PI * diameter.Value;
                  gasVelocityValue = owner.GasInlet.VolumeFlowRate.Value / crossSectionArea;
                  owner.Calculate(gasVelocity, gasVelocityValue);
               }
            }
         }
         else if (crossSectionType == CrossSectionType.Rectangle) {
            double widthValue;
            double heightValue;
            double lengthWidthRatioValue;
            double heightWidthRatioValue;

            if (gasVelocity.HasValue) {
               crossSectionArea = owner.GasInlet.VolumeFlowRate.Value / gasVelocity.Value;
               if (width.HasValue) {
                  widthValue = width.Value;
                  lengthValue = crossSectionArea / widthValue;
                  lengthWidthRatioValue = lengthValue / widthValue;
                  owner.Calculate(length, lengthValue);
                  owner.Calculate(lengthWidthRatio, lengthWidthRatioValue);
               }
               else if (length.HasValue) {
                  lengthValue = length.Value;
                  widthValue = crossSectionArea / lengthValue;
                  lengthWidthRatioValue = lengthValue / widthValue;
                  owner.Calculate(width, widthValue);
                  owner.Calculate(lengthWidthRatio, lengthWidthRatioValue);
               }
               else if (lengthWidthRatio.HasValue) {
                  lengthWidthRatioValue = lengthWidthRatio.Value;
                  widthValue = Math.Sqrt(crossSectionArea / lengthWidthRatioValue);
                  lengthValue = lengthWidthRatioValue * widthValue;
                  owner.Calculate(width, widthValue);
                  owner.Calculate(length, lengthValue);
               }
            }
            else if (width.HasValue && length.HasValue) {
               lengthValue = length.Value;
               widthValue = width.Value;
               crossSectionArea = lengthValue * widthValue;
               lengthWidthRatioValue = lengthValue / widthValue;
               gasVelocityValue = owner.GasInlet.VolumeFlowRate.Value / crossSectionArea;

               owner.Calculate(lengthWidthRatio, lengthWidthRatioValue);
               owner.Calculate(gasVelocity, gasVelocityValue);
            }
            else if (lengthWidthRatio.HasValue && width.HasValue) {
               lengthWidthRatioValue = lengthWidthRatio.Value;
               widthValue = width.Value;
               lengthValue = lengthWidthRatioValue * widthValue;
               crossSectionArea = lengthValue * widthValue;
               gasVelocityValue = owner.GasInlet.VolumeFlowRate.Value / crossSectionArea;

               owner.Calculate(length, lengthValue);
               owner.Calculate(gasVelocity, gasVelocityValue);
            }
            else if (lengthWidthRatio.HasValue && length.HasValue) {
               lengthWidthRatioValue = lengthWidthRatio.Value;
               lengthValue = length.Value;
               widthValue = lengthValue / lengthWidthRatioValue;
               crossSectionArea = lengthValue * widthValue;
               gasVelocityValue = owner.GasInlet.VolumeFlowRate.Value / crossSectionArea;

               owner.Calculate(width, widthValue);
               owner.Calculate(gasVelocity, gasVelocityValue);
            }

            if (width.HasValue) {
               widthValue = width.Value;
               if (heightWidthRatio.HasValue) {
                  heightValue = heightWidthRatio.Value * widthValue;
                  owner.Calculate(height, heightValue);
               }
               else if (height.HasValue) {
                  heightWidthRatioValue = height.Value / widthValue;
               }
            }
         }
      }

      protected DryerScopingModel(SerializationInfo info, StreamingContext conext)
         : base(info, conext) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryerScopingModel", typeof(int));
         if (persistedClassVersion == 1) {
            this.owner = info.GetValue("Owner", typeof(Dryer)) as Dryer;
            this.procVarList = info.GetValue("ProcVarList", typeof(ArrayList)) as ArrayList;
            this.crossSectionType = (CrossSectionType)info.GetValue("CrossSectionType", typeof(CrossSectionType));
            this.diameter = (ProcessVarDouble)RecallStorableObject("Diameter", typeof(ProcessVarDouble));
            this.gasVelocity = (ProcessVarDouble)RecallStorableObject("GasVelocity", typeof(ProcessVarDouble));
            this.height = (ProcessVarDouble)RecallStorableObject("Height", typeof(ProcessVarDouble));
            this.lengthDiameterRatio = (ProcessVarDouble)RecallStorableObject("LengthDiameterRatio", typeof(ProcessVarDouble));
            this.width = (ProcessVarDouble)RecallStorableObject("Width", typeof(ProcessVarDouble));
            this.length = (ProcessVarDouble)RecallStorableObject("Length", typeof(ProcessVarDouble));
            this.lengthWidthRatio = (ProcessVarDouble)RecallStorableObject("LengthWidthRatio", typeof(ProcessVarDouble));
            this.heightWidthRatio = (ProcessVarDouble)RecallStorableObject("HeightWidthRatio", typeof(ProcessVarDouble));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryerScopingModel", CLASS_PERSISTENCE_VERSION, typeof(int));

         info.AddValue("Owner", this.owner, typeof(Dryer));
         info.AddValue("ProcVarList", this.procVarList, typeof(ArrayList));
         info.AddValue("CrossSectionType", this.crossSectionType, typeof(CrossSectionType));
         info.AddValue("GasVelocity", this.gasVelocity, typeof(ProcessVarDouble));
         info.AddValue("Height", this.height, typeof(ProcessVarDouble));
         info.AddValue("Diameter", this.diameter, typeof(ProcessVarDouble));
         info.AddValue("LengthDiameterRatio", this.lengthDiameterRatio, typeof(ProcessVarDouble));
         info.AddValue("Width", this.width, typeof(ProcessVarDouble));
         info.AddValue("Length", this.length, typeof(ProcessVarDouble));
         info.AddValue("LengthWidthRatio", this.lengthWidthRatio, typeof(ProcessVarDouble));
         info.AddValue("HeightWidthRatio", this.heightWidthRatio, typeof(ProcessVarDouble));
      }
   }
}

