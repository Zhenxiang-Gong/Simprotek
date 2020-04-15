using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations {
   //public enum DryingMediumType  {Air, Combustion, SuperheatedSteam, Unknown}

   //public enum ProcessType {Continuous, Batch, Unknown}

   public delegate void StreamDeletedEventHandler(string streamName);
   public delegate void StreamAddedEventHandler(ProcessStreamBase processStream);
   public delegate void UnitOpAddedEventHandler(UnitOperation uo);
   public delegate void UnitOpDeletedEventHandler(string unitOpName);
   public delegate void CalculationStartedEventHandler(Object sender);
   public delegate void CalculationEndedEventHandler(Object sender);
   public delegate void SystemChangedEventHandler(Object sender);

   /// <summary>
	/// UnitOpSystem.
	/// </summary>
	[Serializable]
	public abstract class UnitOpSystem : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
   
      private string name;
      protected Hashtable procVarTable = new Hashtable();
      protected Hashtable formulaTable = new Hashtable();
      protected int procVarCounter = 0;

      protected SequentialSolvingController solveController; 
      
      public event StreamAddedEventHandler StreamAdded;
      public event UnitOpAddedEventHandler UnitOpAdded;
      
      public event StreamDeletedEventHandler StreamDeleted;
      public event UnitOpDeletedEventHandler UnitOpDeleted;
      public event NameChangedEventHandler NameChanged;
      public event CalculationStartedEventHandler CalculationStarted;
      public event CalculationEndedEventHandler CalculationEnded;
      public event SystemChangedEventHandler SystemChanged;

      #region public properties
      public string Name {
         get {return name;}
//         set { 
//            if (value != null && !value.Equals("") && !value.Equals(name)) {
//               string oldName = name;
//               name = value;
//               OnNameChanged(name, oldName);
//            }
//         }
      }
      #endregion

      protected UnitOpSystem(string name) {
         this.name = name;
         solveController = new SequentialSolvingController();
      }
      
      public ErrorMessage SpecifyName(string aValue) {
         ErrorMessage errorMsg = null;
         if (aValue == null || aValue.Equals("")) {
            string msg = "Specified system name is an empty string.";
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Name Error", msg);
         }
         else if (aValue.Equals(name)) {
            string msg = aValue + " has been used already. Please give another name.";
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Duplicate Name Error", msg);
         }
         else {
            string oldName = name;
            name = aValue;
            OnNameChanged(name, oldName);
         }

         return errorMsg;
      }

      internal SequentialSolvingController SequentialSolvingController {
         get { return solveController; }
      }

      protected void OnStreamDeleted(string streamName) {
         if (StreamDeleted != null) {
            StreamDeleted(streamName);
         }

         OnSystemChanged();
      }
      
      protected void OnStreamAdded(ProcessStreamBase ps) {
         if (StreamAdded != null) {
            StreamAdded(ps);
         }

         OnSystemChanged();
      }

      protected void OnUnitOpDeleted(string unitOpName) {
         if (UnitOpDeleted != null) {
            UnitOpDeleted(unitOpName);
         }

         OnSystemChanged();
      }
     
      protected void OnUnitOpAdded(UnitOperation unitOp) {
         if (UnitOpAdded != null) {
            UnitOpAdded(unitOp);
         }
      
         OnSystemChanged();
      }

      protected void OnNameChanged(string newName, string oldName) {
         if (NameChanged != null) {
            NameChanged(this, newName, oldName);
         }

         OnSystemChanged();
      }
      
      public void OnCalculationStarted() {
         if (CalculationStarted != null) {
            CalculationStarted(this);
         }
      }
      
      public void OnCalculationEnded() {
         if (CalculationEnded != null) {
            CalculationEnded(this);
         }
      }

      public void OnSystemChanged() {
         if (SystemChanged != null) {
            SystemChanged(this);
         }
      }
      
      public ArrayList GetSolvableList() {
         ArrayList solvableList = new ArrayList();
         solvableList.AddRange(GetStreamList());
         solvableList.AddRange(GetUnitOpList());
         return solvableList;
      }

      public ProcessStreamBase GetStream(string name) {
         foreach (ProcessStreamBase psb in GetStreamList()) {
            if (psb.Name.Equals(name)) {
               return psb;
            }
         }
         return null;
      }

      public Hashtable FormulaTable {
         get {return  formulaTable;}
      }

      public ProcessVar GetProcessVar(int uniqueID) {
         return  procVarTable[uniqueID] as ProcessVar;
      }

      internal void RegisterProcessVar(ProcessVar var) {
         int id = procVarCounter;
         var.ID = id;
         procVarTable.Add(id, var);
         procVarCounter++;
      }

      public void UnregisterProcessVar(ProcessVar var) {
         procVarTable.Remove(var.ID);
      }

      internal void RegisterProcessVars(ArrayList varList) {
         int id;
         foreach (ProcessVar var in varList) {
            id = procVarCounter;
            var.ID = id;
            procVarTable.Add(id, var);
            procVarCounter++;
         }
      }

      public void UnregisterProcessVars(ArrayList varList) {
         foreach (ProcessVar var in varList) {
            procVarTable.Remove(var.ID);
         }
      }

      public UnitOperation GetUnitOperation(string name) {
         UnitOperation retValue = null;
         ArrayList unitOps = GetUnitOpList();
         foreach (UnitOperation uo in unitOps) {
            if (uo.Name.Equals(name)) {
               retValue = uo;
               break;
            }
         }
         return retValue;
      }

      public abstract ArrayList GetUnitOpList();

      public abstract ArrayList GetStreamList();

      public bool CanRename(string aName) {
         bool canRename = true;
         ArrayList solvableList = GetSolvableList();
         foreach (Solvable solvable in solvableList) {
            if (solvable.Name.Equals(aName)) {
               canRename = false;
               break;
            }
         }
         return canRename;
      }

      protected void SortStreams(ArrayList list) {
         ArrayList tempList = new ArrayList();
         foreach (ProcessStreamBase ps in list) {
            if (ps.UpStreamOwner == null && ps.DownStreamOwner == null) {
               tempList.Add(ps);
            }
         }

         for (int i = 0; i < tempList.Count; i++) {
            list.Remove(tempList[i]);
         }
         
         list.AddRange(tempList);
      }

      //persistence
      protected UnitOpSystem(SerializationInfo info, StreamingContext context) : base (info, context) {
         solveController = new SequentialSolvingController();
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionUnitOpSystem", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.procVarTable = (Hashtable) info.GetValue("ProcVarTable", typeof(Hashtable));
            this.formulaTable = (Hashtable) RecallHashtableObject("FomulaTable");
            this.procVarCounter = (int) info.GetValue("ProcVarCounter", typeof(int));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionUnitOpSystem", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("ProcVarTable", this.procVarTable, typeof(Hashtable));
         info.AddValue("FomulaTable", this.formulaTable, typeof(Hashtable));
         info.AddValue("ProcVarCounter", this.procVarCounter, typeof(int));
      }

   }
}