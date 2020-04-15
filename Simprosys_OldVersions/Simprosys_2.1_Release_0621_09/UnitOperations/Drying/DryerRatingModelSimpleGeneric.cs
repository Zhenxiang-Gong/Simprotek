using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations.Drying {

   /// <summary>
   /// Summary description for DryerRatingModelSimpleGeneric.
   /// </summary>
   [Serializable]
   public class DryerRatingModelSimpleGeneric : DryerRatingModel {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble volumeHeatTransferCoeff;

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
      public CrossSectionType CrossSectionType {
         get { return crossSectionType; }
      }

      public ProcessVarDouble VolumeHeatTransferCoeff {
         get { return volumeHeatTransferCoeff; }
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
      # endregion

      public DryerRatingModelSimpleGeneric(Dryer dryer)
         : base(dryer) {
         volumeHeatTransferCoeff = new ProcessVarDouble(StringConstants.VOLUME_HEAT_TRANSFER_COEFFICIENT, PhysicalQuantity.VolumeHeatTransferCoefficient, VarState.Specified, dryer);
         diameter = new ProcessVarDouble(StringConstants.DIAMETER, PhysicalQuantity.Length, VarState.Specified, dryer);
         height = new ProcessVarDouble(StringConstants.HEIGHT, PhysicalQuantity.Length, VarState.Specified, dryer);
         lengthDiameterRatio = new ProcessVarDouble(StringConstants.LENGTH_DIAMETER_RATIO, PhysicalQuantity.Unknown, 8.0, VarState.Specified, dryer);
         width = new ProcessVarDouble(StringConstants.WIDTH, PhysicalQuantity.Length, VarState.Specified, dryer);
         length = new ProcessVarDouble(StringConstants.LENGTH, PhysicalQuantity.Length, VarState.Specified, dryer);
         lengthWidthRatio = new ProcessVarDouble(StringConstants.LENGTH_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, dryer);
         heightWidthRatio = new ProcessVarDouble(StringConstants.HEIGHT_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, dryer);

         InitializeVarListAndRegisterVars();
         InitializeVarListAndRegisterVars();
      }

      protected override void InitializeVarListAndRegisterVars() {
         procVarList.Add(volumeHeatTransferCoeff);
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
            owner.HasBeenModified(true);
         }
         return null;
      }

      public override bool IsRatingCalcReady() {
         return true;
      }

      protected DryerRatingModelSimpleGeneric(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryerRatingModelSimpleGeneric", typeof(int));
         if (persistedClassVersion == 1) {
            this.volumeHeatTransferCoeff = (ProcessVarDouble)RecallStorableObject("VolumeHeatTransferCoeff", typeof(ProcessVarDouble));
            this.diameter = (ProcessVarDouble)RecallStorableObject("Diameter", typeof(ProcessVarDouble));
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
         info.AddValue("ClassPersistenceVersionDryerRatingModelSimpleGeneric", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("VolumeHeatTransferCoeff", this.volumeHeatTransferCoeff, typeof(ProcessVarDouble));
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

