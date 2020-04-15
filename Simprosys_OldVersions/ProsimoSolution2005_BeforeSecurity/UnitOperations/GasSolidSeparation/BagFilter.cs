using System;
using System.Runtime.Serialization;
using System.Security.Permissions; 

using Prosimo;                                           

namespace Prosimo.UnitOperations.GasSolidSeparation {
   
   [Serializable]
   public class BagFilter : FabricFilter {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ProcessVarDouble bagDiameter;
      private ProcessVarDouble bagLength;
      private ProcessVarDouble numberOfBags;

      #region public properties
      public ProcessVarDouble BagDiameter {
         get {return bagDiameter;}
      }

      public ProcessVarDouble BagLength {
         get {return bagLength;}
      }

      public ProcessVarDouble NumberOfBags {
         get {return numberOfBags;}
      }
      #endregion

      public BagFilter(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         bagDiameter = new ProcessVarDouble(StringConstants.BAG_DIAMETER, PhysicalQuantity.Length, VarState.Specified, this);
         bagLength = new ProcessVarDouble(StringConstants.BAG_LENGTH, PhysicalQuantity.Length, VarState.Specified, this);
         numberOfBags = new ProcessVarDouble(StringConstants.NUMBER_OF_BAGS, PhysicalQuantity.Unknown, VarState.AlwaysCalculated, this);
         InitializeVarListAndRegisterVars();
      }

      protected override void InitializeVarListAndRegisterVars() {
         base.InitializeVarListAndRegisterVars();

         AddVarOnListAndRegisterInSystem(bagDiameter);
         AddVarOnListAndRegisterInSystem(bagLength);
         AddVarOnListAndRegisterInSystem(numberOfBags);
      }
      
      public override void Execute(bool propagate) {
         base.Execute(propagate);
         if (totalFilteringArea.HasValue && bagDiameter.HasValue && bagLength.HasValue) {
            double diameter = bagDiameter.Value;
            double numOfBags = totalFilteringArea.Value/(diameter * bagLength.Value + Math.PI*diameter*diameter/4.0);
            Calculate(numberOfBags, numOfBags);
            currentSolveState = SolveState.Solved;
         }
         PostSolve();
      }

      protected BagFilter(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionBagFilter", typeof(int));
         if (persistedClassVersion == 1) {
            this.bagDiameter = RecallStorableObject("BagDiameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.bagLength = RecallStorableObject("BagLength", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.numberOfBags = RecallStorableObject("NumberOfBags", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionBagFilter", BagFilter.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("BagDiameter", this.bagDiameter, typeof(ProcessVarDouble));
         info.AddValue("BagLength", this.bagLength, typeof(ProcessVarDouble));
         info.AddValue("NumberOfBags", this.numberOfBags, typeof(ProcessVarDouble));
      }
   }
}

