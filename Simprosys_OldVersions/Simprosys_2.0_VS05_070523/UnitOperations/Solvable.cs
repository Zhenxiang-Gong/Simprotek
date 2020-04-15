using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.ThermalProperties;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Drying;

namespace Prosimo.UnitOperations {

   public enum SolveState { NotSolved = 0, Solved, SolvedWithWarning, SolveFailed };

   public delegate void SolveCompleteEventHandler(Object sender, SolveState solveState);

   /// <summary>
   /// Solvable is the base class for stream and unit operation.
   /// </summary>
   [Serializable]
   public abstract class Solvable : Storable, IProcessVarOwner, IComparable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      //variables to be persisted
      protected string name;
      protected SolveState solveState;
      protected ArrayList varList = new ArrayList();
      protected UnitOperationSystem unitOpSystem;
      //variables to be persisted

      //variables not to be persisted
      protected bool hasVarCalculated;
      protected bool hasVarStateChanged;

      protected bool isBeingExecuted = false;
      //protected SolveState solveState;

      protected ProcessVar beingSpecifiedProcVar;

      internal SequentialSolvingController solveController;
      //variables not to be persisted

      #region public properties
      public string Name {
         get { return name; }
      }

      public SolveState SolveState {
         get { return solveState; }
         set { solveState = value; }
      }

      public ArrayList VarList {
         get { return varList; }
      }

      public ArrayList SpecifiedVarList {
         get {
            ArrayList list = new ArrayList();
            foreach (ProcessVar var in varList) {
               if (var.IsSpecifiedAndHasValue) {
                  list.Add(var);
               }
            }
            return list;
         }
      }

      public ArrayList CalculatedVarList {
         get {
            ArrayList list = new ArrayList();
            foreach (ProcessVar var in varList) {
               if (!var.IsSpecified && var.HasValue) {
                  list.Add(var);
               }
            }
            return list;
         }
      }

      internal ProcessVar BeingSpecifiedProcessVar {
         get { return beingSpecifiedProcVar; }
      }

      internal UnitOperationSystem UnitOpSystem {
         get { return unitOpSystem; }
         set { unitOpSystem = value; }
      }

      internal SequentialSolvingController SolveController {
         set { solveController = value; }
      }

      internal bool HasVarCalculated {
         get { return hasVarCalculated; }
         set { hasVarCalculated = value; }
      }

      internal bool HasVarStateChanged {
         get { return hasVarStateChanged; }
         set { hasVarStateChanged = value; }
      }

      internal bool HasSolvedAlready {
         get { return solveState == SolveState.Solved; }
      }

      internal bool IsBeingExecuted {
         get { return isBeingExecuted; }
         set { isBeingExecuted = value; }
      }
      #endregion

      public event SolveCompleteEventHandler SolveComplete;
      public event NameChangedEventHandler NameChanged;
      public event ProcessVarValueCommittedEventHandler ProcessVarValueCommitted;

      //constructor
      protected Solvable(UnitOperationSystem aSystem)
         : base() {
         this.unitOpSystem = aSystem;
         this.solveController = aSystem.SequentialSolvingController;
      }

      //constructor
      protected Solvable(string s, UnitOperationSystem aSystem)
         : base() {
         this.name = s;
         this.unitOpSystem = aSystem;
         this.solveController = aSystem.SequentialSolvingController;
         solveState = SolveState.NotSolved;
      }

      protected HumidGasCalculator GetHumidGasCalculator() {
         return (unitOpSystem as EvaporationAndDryingSystem).HumidGasCalculator;
      }

      protected void AddVarOnListAndRegisterInSystem(ProcessVar var) {
         varList.Add(var);
         unitOpSystem.RegisterProcessVar(var);
      }

      protected void RemoveVarOnListAndUnregisterInSystem(ProcessVar var) {
         varList.Remove(var);
         unitOpSystem.UnregisterProcessVar(var);
      }

      public virtual void AddVarsOnListAndRegisterInSystem(ArrayList aList) {
         varList.AddRange(aList);
         unitOpSystem.RegisterProcessVars(aList);
      }

      public virtual void RemoveVarsOnListAndUnregisterInSystem(ArrayList aList) {
         foreach (ProcessVar var in aList) {
            RemoveVarOnListAndUnregisterInSystem(var);
         }
      }

      public override string ToString() {
         return name;
      }

      public override bool Equals(object obj) {
         Solvable sovable = obj as Solvable;
         if (sovable == null) {
            return false;
         }

         bool isEqual = false;
         if (name.Equals(sovable.Name)) {
            isEqual = true;
         }
         return isEqual;
      }

      public override int GetHashCode() {
         return this.name.GetHashCode();
      }

      public int CompareTo(object other) {
         Solvable solvable = other as Solvable;
         //return name.CompareTo(solvable.name);
         char[] name1Chars = name.ToCharArray();
         char[] name2Chars = solvable.name.ToCharArray();
         int name1 = 0;
         foreach (char c in name1Chars) {
            name1 += (int)c;
         }
         int name2 = 0;
         foreach (char c in name2Chars) {
            name2 += (int)c;
         }
         return name1.CompareTo(name2);
      }

      private void OnProcessVarValueCommitted(ProcessVar var) {
         if (ProcessVarValueCommitted != null) {
            ProcessVarValueCommitted(var);
         }
      }

      //the scope of this method is internal in stead of protected is because
      //PsychrometricChartModel needs to call this method on its streams
      internal void OnSolveComplete() {
         if (SolveComplete != null) {
            SolveComplete(this, solveState);
         }
      }

      protected void OnNameChanged(string newName, string oldName) {
         if (NameChanged != null) {
            NameChanged(this, newName, oldName);
            unitOpSystem.OnSystemChanged();
         }
      }

      public ErrorMessage SpecifyName(string aValue) {
         ErrorMessage errorMsg = null;
         if (aValue != null && !aValue.Equals("") && !name.Equals(aValue)) {
            string oldName = name;
            if (unitOpSystem.CanRename(aValue)) {
               name = aValue;
            }
            else {
               String msg = aValue.ToString() + " has been used already. Please try to give another name.";
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Duplicate Name Error", msg);
            }
            OnNameChanged(name, oldName);
         }
         return errorMsg;
      }

      public ErrorMessage Specify(ProcessVarDouble pv, double aValue) {
         ErrorMessage retMsg = null;
         if (pv.HasValueOf(aValue)) {
            return retMsg;
         }
         else if (aValue != Constants.NO_VALUE) {
            retMsg = CheckSpecifiedValueRange(pv, aValue);
            if (retMsg != null) {
               return retMsg;
            }
         }

         //remember currently being specified variable value
         double oldValue = pv.Value;
         beingSpecifiedProcVar = pv;

         pv.Value = aValue;
         pv.State = VarState.Specified;

         try {
            unitOpSystem.OnCalculationStarted(); //must tell UI calculation has started already
            HasBeenModified(true);
            unitOpSystem.OnCalculationEnded(); //must tell UI calculation has ended already
         }
         catch (Exception e) {
            pv.Value = oldValue;
            retMsg = HandleException(e);
            //HasBeenModified(true);
         }
         OnProcessVarValueCommitted(pv);

         return retMsg;
      }

      public ErrorMessage Specify(ProcessVarInt pv, int aValue) {
         ErrorMessage retMsg = null;
         if (pv.Value == aValue) {
            return retMsg;
         }
         else if (aValue != Constants.NO_VALUE_INT) {
            retMsg = CheckSpecifiedValueRange(pv, aValue);
            if (retMsg != null) {
               return retMsg;
            }
         }

         //remember currently being specified variable value
         int oldValue = pv.Value;
         beingSpecifiedProcVar = pv;

         pv.Value = aValue;
         pv.State = VarState.Specified;

         try {
            unitOpSystem.OnCalculationStarted(); //must tell UI calculation has started already
            HasBeenModified(true);
            unitOpSystem.OnCalculationEnded(); //must tell UI calculation has ended already
         }
         catch (Exception e) {
            pv.Value = oldValue;
            retMsg = HandleException(e);
            //HasBeenModified(true);
         }

         OnProcessVarValueCommitted(pv);

         return retMsg;
      }

      public ErrorMessage Specify(Hashtable procVarAndValueTable) {
         ErrorMessage retMsg = null;
         Hashtable oldValueTable = new Hashtable();
         ProcessVar pv = null;
         ProcessVarDouble pvDouble;
         ProcessVarInt pvInt;
         double dValue;
         int iValue;

         IEnumerator iter = procVarAndValueTable.Keys.GetEnumerator();
         while (iter.MoveNext()) {
            pv = (ProcessVar)iter.Current;
            if (pv is ProcessVarDouble) {
               pvDouble = pv as ProcessVarDouble;
               oldValueTable.Add(pv, pvDouble.Value);
               dValue = (double)procVarAndValueTable[pv];
               pvDouble.Value = dValue;
            }
            else if (pv is ProcessVarInt) {
               pvInt = pv as ProcessVarInt;
               oldValueTable.Add(pv, pvInt.Value);
               iValue = (int)procVarAndValueTable[pv];
               pvInt.Value = iValue;
            }
         }

         //if (pv is ProcessVarDouble) {
         //   pvDouble = pv as ProcessVarDouble;
         //   if (pvDouble.Value != Constants.NO_VALUE) {
         //      retMsg = CheckSpecifiedValueRange(pvDouble, pvDouble.Value);
         //   }
         //}
         //else if (pv is ProcessVarInt) {
         //   pvInt = pv as ProcessVarInt;
         //   if (pvInt.Value != Constants.NO_VALUE_INT) {
         //      retMsg = CheckSpecifiedValueRange(pvInt, pvInt.Value);
         //   }
         //}
         if (retMsg != null) {
            return retMsg;
         }

         try {
            unitOpSystem.OnCalculationStarted(); //must tell UI calculation has started already
            HasBeenModified(true);
            unitOpSystem.OnCalculationEnded(); //must tell UI calculation has ended already
         }
         catch (Exception e) {
            iter = oldValueTable.Keys.GetEnumerator();
            while (iter.MoveNext()) {
               pv = (ProcessVar)iter.Current;
               if (pv is ProcessVarDouble) {
                  pvDouble = pv as ProcessVarDouble;
                  dValue = (double)oldValueTable[pv];
                  pvDouble.Value = dValue;
               }
               else if (pv is ProcessVarInt) {
                  pvInt = pv as ProcessVarInt;
                  iValue = (int)oldValueTable[pv];
                  pvInt.Value = iValue;
               }
            }
            retMsg = HandleException(e);
            HasBeenModified(true);
         }
         OnProcessVarValueCommitted(pv);

         return retMsg;
      }

      public ErrorMessage Specify<T>(ref T variable, T newValue) {
         ErrorMessage retMsg = null;
         if (newValue.Equals(variable)) {
            T oldValue = variable;
            variable = newValue;
            try {
               HasBeenModified(true);
            }
            catch (Exception e) {
               variable = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }

      internal ErrorMessage HandleException(Exception e) {
         EraseAllCalculatedValues();
         solveController.ClearCalculateQueue();
         unitOpSystem.OnCalculationEnded();
         ErrorMessage errorMsg = null;
         string msg;
         if (e is InappropriateSpecifiedValueException) {
            errorMsg = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(e.Message);
         }
         else if (e is OverSpecificationException) {
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.OVER_SPECIFICATION, e.Message);
         }
         if (e is InappropriateCalculatedValueException) {
            msg = e.Message;
            if (beingSpecifiedProcVar != null) {
               msg = msg + "\nMake sure the specified value for " + beingSpecifiedProcVar.Name + " is appropriate.";
            }
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_CALCULATED_VALUE, msg);
         }
         else if (e is CalculationFailedException) {
            msg = e.Message;
            if (beingSpecifiedProcVar != null) {
               msg = msg + "\nMake sure the specified value for " + beingSpecifiedProcVar.Name + " is appropriate.";
            }
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.CALCULATION_FAILURE, msg);
         }
         else if (e is Exception) {
            msg = e.Message + "\nMake sure the specified value is appropriate.";
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.CALCULATION_FAILURE, msg);
         }

         return errorMsg;
      }

      internal void Calculate(ProcessVarDouble pv, double aValue) {
         if (aValue.Equals(double.NaN)) {
            String msg = "Failed to solve " + name + "'s " + pv.VarTypeName + ".";
            pv.Value = Constants.NO_VALUE;
            throw new CalculationFailedException(msg);
         }

         double pvValue = pv.Value;
         if (pvValue == Constants.NO_VALUE) {
            pv.Value = aValue;
            if (!pv.IsAlwaysCalculated) {
               pv.State = VarState.Calculated;
            }
            if (pv.Owner != null) {
               ((Solvable)(pv.Owner)).HasVarCalculated = true;
            }
         }
         else if (pvValue != Constants.NO_VALUE && Math.Abs(pvValue - aValue) < 1.0e-6) {
            //do nothing since it has already been calculated
         }
         else {
            bool isOverspecified = false;
            String msg = name + "'s " + pv.VarTypeName;
            if (pv.IsSpecified && pvValue != Constants.NO_VALUE) {
               msg = msg + " has already got a specified value. It should not be calculated. The flowsheet may be overspecified.";
               isOverspecified = true;
            }
            else if (Math.Abs((pvValue - aValue) / pvValue) > 1.0e-4) {
               msg = msg + " has already got a calculated value. It should not be calculated again. The flowsheet may be overspecified.";
               isOverspecified = true;
            }
            else {
            }

            if (isOverspecified) {
               throw new OverSpecificationException(msg);
            }
         }
      }

      internal virtual void HasBeenModified(bool specify) {
         if (specify) {
            //since PsychrometricChartModel is a utility it does not need to persist
            //any changes in the object. Therefoere, changes in the model should not
            //trigger the SystemChanged event.
            if (!IsThisRelatedToPsychrometricChartModel()) {
               unitOpSystem.OnSystemChanged();
            }

            solveController.OnSolvableSpecified(this);
            solveController.EraseCalculatedValues();
            solveController.OnSolvableCalculated(this, specify);
            solveController.Calculate();
         }
         else {
            solveController.OnSolvableCalculated(this, specify);
            solveController.CalculateStreams();
         }
      }

      private bool IsThisRelatedToPsychrometricChartModel() {
         bool retValue = false;
         if (this is PsychrometricChartModel) {
            retValue = true;
         }
         else if (this is ProcessStreamBase) {
            ProcessStreamBase psb = this as ProcessStreamBase;
            if ((psb.UpStreamOwner is PsychrometricChartModel) ||
                (psb.DownStreamOwner is PsychrometricChartModel)) {
               retValue = true;
            }
         }
         return retValue;
      }

      internal virtual void EraseAllCalculatedValues() {
         //All solvables have to be queued first and erase the calculated variables
         //for each sovable one by one
         solveController.OnSolvableSpecified(this);
         solveController.EraseCalculatedValues();
      }

      //Erase all the calculated values in each solvable
      internal virtual void EraseCalculatedProcVarValues() {
         foreach (ProcessVar var in varList) {
            if (var.IsCalculated) {
               EraseOldValue(var);
               var.State = VarState.Specified;
            }
            else if (var.IsAlwaysCalculated) {
               EraseOldValue(var);
            }
         }
         solveState = SolveState.NotSolved;
         isBeingExecuted = false;
         OnSolveComplete();
         if (solveState != SolveState.NotSolved) {
            solveState = SolveState.NotSolved;
         }
      }

      private void EraseOldValue(ProcessVar var) {
         if (var is ProcessVarDouble) {
            ProcessVarDouble pvd = var as ProcessVarDouble;
            pvd.Value = Constants.NO_VALUE;
         }
         else if (var is ProcessVarInt) {
            ProcessVarInt pvi = var as ProcessVarInt;
            pvi.Value = Constants.NO_VALUE_INT;
         }
      }

      protected virtual bool IsSolveReady() {
         return true;
      }

      public virtual void Execute(bool propagate) {
      }

      protected virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         return null;
      }

      protected virtual ErrorMessage CheckSpecifiedValueRange(ProcessVarInt pv, int aValue) {
         return null;
      }

      internal ErrorMessage CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(string message) {
         return new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, message);
      }

      internal ErrorMessage CreateLessThanOrEqualToZeroErrorMessage(ProcessVar pv) {
         return new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, pv.VarTypeName + " cannot be less than or equal to 0");
      }

      internal ErrorMessage CreateLessThanZeroErrorMessage(ProcessVar pv) {
         return new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, pv.VarTypeName + " cannot be less than 0");
      }

      internal ErrorMessage CreateOutOfRangeZeroToOneErrorMessage(ProcessVar pv) {
         return new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, pv.VarTypeName + " cannot be out of the range of 0 to 1");
      }

      #region persistence
      protected Solvable(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionSolvable", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.solveState = (SolveState)info.GetValue("SolveState", typeof(SolveState));
            this.varList = info.GetValue("VarList", typeof(ArrayList)) as ArrayList;
            this.unitOpSystem = (UnitOperationSystem)info.GetValue("UnitOpSystem", typeof(UnitOperationSystem));
         }
         RecallInitialization();
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionSolvable", Solvable.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("SolveState", this.solveState, typeof(SolveState));
         info.AddValue("VarList", this.varList, typeof(ArrayList));
         info.AddValue("UnitOpSystem", this.unitOpSystem, typeof(UnitOperationSystem));
      }

      protected virtual void RecallInitialization() {
         this.solveController = unitOpSystem.SequentialSolvingController;
      }
      #endregion persistence
   }
}
