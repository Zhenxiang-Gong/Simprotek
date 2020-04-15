using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots 
{
   [Serializable]
   public class PlotParameter : Storable
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private ProcessVarDouble pv;
      private double[] paramValues;

      public string Name 
      {
         get {return pv.Name;}
      }

      public ProcessVarDouble Variable 
      {
         get {return pv;}
         //set {pv = value;}
      }
      

      public double Min 
      {
         get {return paramValues[0];}
         set {paramValues[0]  = value;}
      }
      
      public double Max 
      {
         get {return paramValues[paramValues.Length-1];}
         set {paramValues[paramValues.Length-1] = value;}
      }

      public int NumberOfValues 
      {
         get {return paramValues.Length;}
      }
      
      public double[] ParameterValues 
      {
         get {return paramValues;}
         set {paramValues = value;}
      }
      
      public PlotParameter(ProcessVarDouble pv, double[] paramValues) 
      {
         this.pv = pv;
         this.paramValues = paramValues;
      }

      protected PlotParameter(SerializationInfo info, StreamingContext context) : base (info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionParameterVariable", typeof(int));
         if (persistedClassVersion == 1) 
         {
            this.pv = RecallStorableObject("Pv", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.paramValues = (double[]) info.GetValue("ParamValues", typeof(double[]));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionparameterVariable", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Pv", this.pv, typeof(ProcessVarDouble));
         info.AddValue("ParamValues", this.paramValues, typeof(double[]));
      }

   }
}


