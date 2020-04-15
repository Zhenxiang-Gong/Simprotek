using System;
using System.Text;
using Prosimo;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo {
   public enum VarState { Specified = 0, Calculated, AlwaysCalculated };

   /// <summary>
   /// Summary description for Variable.
   /// </summary>
   [Serializable]
   public abstract class ProcessVar : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      string name;
      int id;
      //string varTypeName;
      protected VarState state;
      PhysicalQuantity type;
      bool enabled = true;
      IProcessVarOwner owner;

      public event NameChangedEventHandler NameChanged;

      public string Name {
         get {
            String prefix = owner.Name;
            string retValue = owner.Name;
            if (retValue == null || retValue.Equals("")) {
               retValue = StringConstants.GetTypeName(name);
            }
            else {
               retValue = prefix + "." + StringConstants.GetTypeName(name);
            }
            return retValue;
         }
         set {
            if (value != null && !value.Equals("")) {
               string oldName = name;
               name = value;
               OnNameChanged(StringConstants.GetTypeName(value), StringConstants.GetTypeName(oldName));
            }
         }
      }

      public int ID {
         get { return id; }
         set { id = value; }
      }

      public string VarTypeName {
         get { return StringConstants.GetTypeName(name); }
      }

      public VarState State {
         set { state = value; }
      }

      public PhysicalQuantity Type {
         get { return type; }
         set { type = value; }
      }

      public bool Enabled {
         get { return enabled; }
         set { enabled = value; }
      }

      public abstract bool HasValue {
         get;
      }

      public abstract bool IsSpecifiedAndHasValue {
         get;
      }

      public bool IsSpecified {
         get { return state == VarState.Specified; }
      }

      public bool IsCalculated {
         get { return state == VarState.Calculated; }
      }

      public bool IsAlwaysCalculated {
         get { return state == VarState.AlwaysCalculated; }
      }

      public IProcessVarOwner Owner {
         get { return owner; }
         set { owner = value; }
      }

      public override string ToString() {
         return StringConstants.GetTypeName(name);
      }

      public override int GetHashCode() {
         return this.id;
      }

      public override bool Equals(object obj) {
         ProcessVar v = obj as ProcessVar;
         return (v != null && this.id == v.id);
      }

      protected ProcessVar(string name, PhysicalQuantity type, VarState state)
         : base() {
         this.name = name;
         this.type = type;
         this.state = state;
      }

      protected ProcessVar(string name, PhysicalQuantity type, VarState state, IProcessVarOwner owner)
         : base() {
         this.name = name;
         this.type = type;
         this.state = state;
         this.owner = owner;
      }

      /*public ProcessVar(string name, int id, PhysicalQuantity type, VarState state, bool enabled, Solvable owner) {
         this.name = name;
         this.type = type;
         this.state = state;
         this.enabled = enabled;
         this.ownerSolvable = owner;
      } */

      //public ProcessVariable(string name) {
      //   this.name = name;
      //   this.type = PhysicalQuantity.Unknown;
      //   this.varValue = Double.NaN; //the minimum of an int
      //   this.state = VarState.Specified;
      //}

      //public ProcessVar(string name, PhysicalQuantity type, VarState state) {
      //   this.name = name;
      //   this.type = type;
      //   this.varValue = Constants.NO_VALUE;
      //   this.state = state;
      //}

      protected ProcessVar(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      private void OnNameChanged(string newName, string oldName) {
         if (NameChanged != null) {
            NameChanged(this, newName, oldName);
         }
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessVariable", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.id = (int)info.GetValue("ID", typeof(int));
            this.state = (VarState)info.GetValue("State", typeof(VarState));
            this.type = (PhysicalQuantity)info.GetValue("Type", typeof(PhysicalQuantity));
            this.enabled = (bool)info.GetValue("Enabled", typeof(bool));
            this.owner = info.GetValue("Owner", typeof(IProcessVarOwner)) as IProcessVarOwner;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionProcessVariable", ProcessVar.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("ID", this.id, typeof(int));
         info.AddValue("State", this.state, typeof(VarState));
         info.AddValue("Type", this.type, typeof(PhysicalQuantity));
         info.AddValue("Enabled", this.enabled, typeof(bool));
         info.AddValue("Owner", this.owner, typeof(IProcessVarOwner));
      }
   }

   [Serializable]
   public class ProcessVarDouble : ProcessVar {

      private const int CLASS_PERSISTENCE_VERSION = 1;
      double varValue = Constants.NO_VALUE;

      public ProcessVarDouble(string name, PhysicalQuantity type, VarState state)
         : base(name, type, state) {
      }

      public ProcessVarDouble(string name, PhysicalQuantity type, VarState state, IProcessVarOwner owner)
         : base(name, type, state, owner) {
      }

      public ProcessVarDouble(string name, PhysicalQuantity type, double varValue, VarState state)
         : base(name, type, state) {
         this.varValue = varValue;
      }

      public ProcessVarDouble(string name, PhysicalQuantity type, double varValue, VarState state, IProcessVarOwner owner)
         : base(name, type, state, owner) {
         this.varValue = varValue;
      }

      public ProcessVarDouble Clone() {
         return (ProcessVarDouble)this.MemberwiseClone();
      }

      public double Value {
         get { return varValue; }
         set { varValue = value; }
      }

      public bool HasValueOf(double aValue) {
         bool retValue = false;
         if (varValue != 0.0 && Math.Abs((varValue - aValue) / varValue) < 1.0e-6) {
            retValue = true;
         }
         else if (varValue == 0.0 && Math.Abs(varValue - aValue) < 1.0e-6) {
            retValue = true;
         }

         return retValue;
      }

      public override bool HasValue {
         get { return varValue != Constants.NO_VALUE; }
      }

      public override bool IsSpecifiedAndHasValue {
         get { return IsSpecified && HasValue; }
      }

      protected ProcessVarDouble(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessVarDouble", typeof(int));
         if (persistedClassVersion == 1) {
            this.varValue = (double)info.GetValue("VarValue", typeof(double));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionProcessVarDouble", ProcessVarDouble.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("VarValue", this.varValue, typeof(double));
      }
   }

   [Serializable]
   public class ProcessVarInt : ProcessVar {

      private const int CLASS_PERSISTENCE_VERSION = 1;
      int varValue;

      public ProcessVarInt(string name, PhysicalQuantity type, VarState state)
         : base(name, type, state) {
      }

      public ProcessVarInt(string name, PhysicalQuantity type, VarState state, IProcessVarOwner owner)
         : base(name, type, state, owner) {
      }

      public ProcessVarInt(string name, PhysicalQuantity type, int varValue, VarState state, IProcessVarOwner owner)
         : base(name, type, state, owner) {
         this.varValue = varValue;
      }

      public ProcessVarInt Clone() {
         return (ProcessVarInt)this.MemberwiseClone();
      }

      public int Value {
         get { return varValue; }
         set { varValue = value; }
      }

      public override bool HasValue {
         get { return varValue != Constants.NO_VALUE_INT; }
      }

      public override bool IsSpecifiedAndHasValue {
         get { return IsSpecified && HasValue; }
      }

      protected ProcessVarInt(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessVarInt", typeof(int));
         if (persistedClassVersion == 1) {
            this.varValue = (int)info.GetValue("VarValue", typeof(int));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionProcessVarInt", ProcessVarInt.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("VarValue", this.varValue, typeof(int));
      }
   }
}