using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.Miscellaneous {
   
   [Serializable] 
   public class Adjust : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      ProcessVarDouble dependentVar;
      ProcessVarDouble independentVar;
      double targetValue;
      
      public Adjust(string name, UnitOpSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (!IsStreamValid(ps, streamIndex)) {
            return false;
         }
         bool canAttach = false;
         if (streamIndex == INLET_INDEX && inlet == null) {
            if (outlet == null || (outlet != null && outlet.GetType() == ps.GetType())) {
               canAttach = true;
            }
         }
         else if (streamIndex == OUTLET_INDEX && outlet == null) {
            if (inlet == null || (inlet != null && inlet.GetType() == ps.GetType())) {
               canAttach = true;
            }
         }

         return canAttach;
      }

      public ProcessVarDouble InDependentVar 
      {
         set {independentVar = value;}
      }
      
      public ProcessVarDouble DependentVar 
      {
         set {dependentVar = value;}
      }
      
      public ErrorMessage SpecifyDependentVar(ProcessVarDouble var) 
      {
         dependentVar = var;
         return null;
      }
      
      public ErrorMessage SpecifyIndependentVar(ProcessVarDouble var) 
      {
         independentVar = var;
         return null;
      }

      public ErrorMessage SpecifyTargetValue (double aValue) 
      {
         targetValue = aValue;
         return null;
      }
      
      public override void Execute(bool propagate) 
      {
         if (inlet is DryingStream) {
            double oldAdjustedValue = independentVar.Value;
            double oldTargetValue = dependentVar.Value;
            double newAdjustedValue;
            double newTargetValue;
            double tempValue;

            newAdjustedValue = oldAdjustedValue + oldAdjustedValue * 0.05;
            
            int counter = 0;
            do {
               counter++;
               independentVar.Owner.Specify(independentVar, newAdjustedValue);
               newTargetValue = dependentVar.Value;
               tempValue = newAdjustedValue;
               newAdjustedValue = oldAdjustedValue + 0.5 * (newAdjustedValue - oldAdjustedValue) * (targetValue - oldTargetValue)/(newTargetValue - oldTargetValue);
               oldTargetValue = newTargetValue;
               oldAdjustedValue = tempValue;
            } while (Math.Abs(newTargetValue - targetValue) > 1.0e-6 && counter < 500);
            
            if (counter == 500) {
               solveState = SolveState.SolveFailed;
            }
            else {
               solveState = SolveState.Solved;
            }
         }
            
         OnSolveComplete(solveState);
      }

      protected Adjust(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionAdjust", typeof(int));
         this.independentVar = (ProcessVarDouble) RecallStorableObject("IndependentVar", typeof(ProcessVarDouble));
         this.dependentVar = (ProcessVarDouble) RecallStorableObject("DependentVar", typeof(ProcessVarDouble));
         this.targetValue = (double) info.GetValue("TargetValue", typeof(double));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionAdjust", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("IndependentVar", this.independentVar, typeof(ProcessVarDouble));
         info.AddValue("DependentVar", this.dependentVar, typeof(ProcessVarDouble));
         info.AddValue("TargetValue", this.targetValue, typeof(double));
      }
   }
}

