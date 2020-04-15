using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;

namespace Prosimo.Plots {
   [Serializable]
   public class PlotVariable : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble pv;
      private double min;
      private double max;

      public string Name {
         get { return pv.Name; }
      }

      public ProcessVarDouble Variable {
         get { return pv; }
         set { pv = value; }
      }

      public double Min {
         get { return min; }
         set { min = value; }
      }

      public double Max {
         get { return max; }
         set { max = value; }
      }

      public PlotVariable(ProcessVarDouble pv, double min, double max) {
         this.pv = pv;
         this.min = min;
         this.max = max;
      }

      protected PlotVariable(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPlotVariable", typeof(int));
         if (persistedClassVersion == 1) {
            this.pv = RecallStorableObject("Pv", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.min = (double)info.GetValue("Min", typeof(double));
            this.max = (double)info.GetValue("Max", typeof(double));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionPlotVariable", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Pv", this.pv, typeof(ProcessVarDouble));
         info.AddValue("Min", this.min, typeof(double));
         info.AddValue("Max", this.max, typeof(double));
      }
   }
}


