using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.UnitOperations;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for CustomEditor.
	/// </summary>
   [Serializable]
   public class CustomEditor : ISerializable
	{
      public event ProcessVarAddedEventHandler ProcessVarAdded;
      public event ProcessVarDeletedEventHandler ProcessVarDeleted;

      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      public Flowsheet flowsheet;

      private ArrayList variables;
      public ArrayList Variables
      {
         get {return variables;}
         set {variables = value;}
      }

		public CustomEditor(Flowsheet flowsheet)
		{
         this.flowsheet = flowsheet;
         this.variables = new ArrayList();
         this.flowsheet.EvaporationAndDryingSystem.StreamDeleted += new StreamDeletedEventHandler(EvaporationAndDryingSystem_StreamDeleted);
         this.flowsheet.EvaporationAndDryingSystem.UnitOpDeleted += new UnitOpDeletedEventHandler(EvaporationAndDryingSystem_UnitOpDeleted);
      }

      public void AddProcessVars(ArrayList vars)
      {
         IEnumerator e = vars.GetEnumerator();
         while (e.MoveNext())
         {
            ProcessVar var = (ProcessVar)e.Current;
            if (var != null)
               this.AddProcessVar(var);
         }
      }

      public void AddProcessVar(ProcessVar var)
      {
         if (!this.IsOnTheList(var))
         {
            this.variables.Add(var);
            this.OnProcessVarAdded(var);
         }
      }

      public void DeleteProcessVars(ArrayList idxs)
      {
         // the list of indexes is sorted in descending order
         ArrayList vars = new ArrayList();
         IEnumerator e = idxs.GetEnumerator();
         while (e.MoveNext())
         {
            int idx = (int)e.Current;
            ProcessVar var = (ProcessVar)this.variables[idx];
            vars.Add(var);
            this.variables.RemoveAt(idx);
         }
         this.OnProcessVarDeleted(vars, idxs);
      }
      
      private void DeleteProcessVar(int idx)
      {
         ProcessVar var = (ProcessVar)this.variables[idx];
         this.variables.RemoveAt(idx);
      }

//      public void DeleteProcessVar(ProcessVar var)
//      {
//         int idx = this.variables.IndexOf(var);
//         this.variables.RemoveAt(idx);
//      }

      private void OnProcessVarAdded(ProcessVar var) 
      {
         if (ProcessVarAdded != null) 
         {
            ProcessVarAdded(var);
         }
      }

      private void OnProcessVarDeleted(ArrayList vars, ArrayList idxs) 
      {
         if (ProcessVarDeleted != null) 
         {
            ProcessVarDeleted(vars, idxs);
         }
      }

      private bool IsOnTheList(ProcessVar var)
      {
         bool isOnTheList = false;
         IEnumerator e = this.variables.GetEnumerator();
         while (e.MoveNext())
         {
            ProcessVar v = (ProcessVar)e.Current;
            if (var.Equals(v))
            {
               isOnTheList = true;
               break;
            }
         }
         return isOnTheList;
      }

      private void EvaporationAndDryingSystem_StreamDeleted(string streamName)
      {
         this.DeleteVariables(streamName);
      }

      private void EvaporationAndDryingSystem_UnitOpDeleted(string unitOpName)
      {
         this.DeleteVariables(unitOpName);
      }

      private void DeleteVariables(string solvableName)
      {
         if (this.variables.Count > 0) 
         {
            ArrayList idxs = new ArrayList();
            for (int i=this.variables.Count-1; i>=0; i--)
            {
               if (((ProcessVar)this.variables[i]).Owner.Name.Equals(solvableName))
               {
                  idxs.Add(i);
               }
            }

            if (idxs.Count > 0)
            {
               IEnumerator e = idxs.GetEnumerator();
               while (e.MoveNext())
               {
                  int idx = (int)e.Current;
                  this.DeleteProcessVar(idx);
               }
            }
         }
      }

      protected CustomEditor(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCustomEditor", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               ArrayList varIds = (ArrayList)info.GetValue("VariableIds", typeof(ArrayList));
               IEnumerator e = varIds.GetEnumerator();
               while (e.MoveNext())
               {
                  int id = (int)e.Current;
                  ProcessVar var = this.flowsheet.EvaporationAndDryingSystem.GetProcessVar(id);
                  this.Variables.Add(var);
               }
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionCustomEditor", CustomEditor.CLASS_PERSISTENCE_VERSION, typeof(int));
         ArrayList varIds = new ArrayList();
         IEnumerator e = this.Variables.GetEnumerator();
         while (e.MoveNext())
         {
            ProcessVar var = (ProcessVar)e.Current;
            int id = var.ID;
            varIds.Add(id);
         }
         info.AddValue("VariableIds", varIds, typeof(ArrayList));
      }
   }
}
