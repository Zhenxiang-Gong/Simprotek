using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo {

   [Serializable]
   public class NonSolvableProcessVarOwner : Storable, IProcessVarOwner {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public event ProcessVarValueCommittedEventHandler ProcessVarValueCommitted;

      #region public properties
      public virtual string Name {
         get { return ""; }
      }

      #endregion

      public NonSolvableProcessVarOwner()
         : base() {
      }

      public ErrorMessage Specify(ProcessVarDouble pv, double aValue) {
         if (!pv.HasValueOf(aValue)) {
            pv.Value = aValue;
            pv.State = VarState.Specified;
            OnProcessVarValueCommitted(pv);
         }
         return null;
      }

      public ErrorMessage Specify(ProcessVarInt pv, int aValue) {
         if (pv.Value != aValue) {
            pv.Value = aValue;
            pv.State = VarState.Specified;
            OnProcessVarValueCommitted(pv);
         }

         return null;
      }

      public ErrorMessage Specify(Hashtable procVarAndValueTable) {
         return null;
      }


      private void OnProcessVarValueCommitted(ProcessVar var) {
         if (ProcessVarValueCommitted != null) {
            ProcessVarValueCommitted(var);
         }
      }
      #region Persistence
      protected NonSolvableProcessVarOwner(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionNonSolvableProcessVarOwner", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionNonSolvableProcessVarOwner", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
      #endregion
   }
}
