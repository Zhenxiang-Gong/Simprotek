using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.Miscellaneous {
   
   [Serializable] 
   public class Recycle : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public Recycle(string name, UnitOpSystem uoSys) : base(name, uoSys) {
         solvingPriority = 2000;
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
      
      /*public override bool IsSolveReady() {
         bool isReady = false;
         if (inlet is DryingStream) {
            DryingStream dsInlet = inlet as DryingStream;
            if (dsInlet.MassFlowRateDryBase.HasValue && dsInlet.Temperature.HasValue &&
               dsInlet.MoistureContentDryBase.HasValue) {
               isReady = true;
            }
         }

         return isReady;
      }*/
      
      public override void Execute(bool propagate) {
         isBeingExecuted = true;
         //if (IsSolveReady() && inlet is DryingStream) {
         if (inlet is DryingStream) {
            DryingStream dsInlet = inlet as DryingStream;
            DryingStream dsOutlet = outlet as DryingStream;
            
            if (dsInlet.Pressure.HasValue && !dsOutlet.Pressure.HasValue) {
               dsOutlet.Pressure.Value = dsInlet.Pressure.Value;
            }
            //else if (dsOutlet.Pressure.HasValue && !dsInlet.Pressure.HasValue) {
            //   dsInlet.Pressure.Value = dsOutlet.Pressure.Value;
            //}

            if (inlet is DryingMaterialStream) { 
               BalanceDryingMaterialStreamSpecificHeat(inlet as DryingMaterialStream, outlet as DryingMaterialStream);
            }
            double oldMassFlowDryBasisValue;
            double oldTemperatureValue;
            double oldMoistureContentDryBasisValue;
            double newMassFlowDryBasisValue = dsInlet.MassFlowRateDryBase.Value;
            double newTemperatureValue = dsInlet.Temperature.Value;
            double newMoistureContentDryBasisValue = dsInlet.MoistureContentDryBase.Value;

            if (!dsInlet.MassFlowRateDryBase.HasValue) {
               newMassFlowDryBasisValue = 0.000;
            }
            if (!dsInlet.Temperature.HasValue) {
               newTemperatureValue = 274.15;
            }
            if (!dsInlet.MoistureContentDryBase.HasValue) {
               newMoistureContentDryBasisValue = 0.0000;
            }

            int counter = 0;
            do {
               counter++;
               dsOutlet.MassFlowRateDryBase.Value = newMassFlowDryBasisValue;
               dsOutlet.Temperature.Value = newTemperatureValue;
               dsOutlet.MoistureContentDryBase.Value = newMoistureContentDryBasisValue;
               oldMassFlowDryBasisValue = newMassFlowDryBasisValue;
               oldTemperatureValue = newTemperatureValue;
               oldMoistureContentDryBasisValue = newMoistureContentDryBasisValue;

               dsOutlet.HasBeenModified(true);
               newMassFlowDryBasisValue = dsInlet.MassFlowRateDryBase.Value;
               newTemperatureValue = dsInlet.Temperature.Value;
               newMoistureContentDryBasisValue = dsInlet.MoistureContentDryBase.Value;
               //newMassFlowDryBasisValue = oldMassFlowDryBasisValue + 0.75 * (dsInlet.MassFlowRateDryBase.Value - oldMassFlowDryBasisValue);
               //newTemperatureValue = oldTemperatureValue + 0.75 * (dsInlet.Temperature.Value - oldTemperatureValue);
               //newMoistureContentDryBasisValue = oldMoistureContentDryBasisValue + 0.75 * (dsInlet.MoistureContentDryBase.Value - oldMoistureContentDryBasisValue);
            } while ((Math.Abs((newMassFlowDryBasisValue - oldMassFlowDryBasisValue)/oldMassFlowDryBasisValue) > 1.0e-8 ||
               Math.Abs((newTemperatureValue - oldTemperatureValue)/oldTemperatureValue) > 1.0e-6 ||
               Math.Abs((newMoistureContentDryBasisValue - oldMoistureContentDryBasisValue)/oldMoistureContentDryBasisValue) > 1.0e-6) &&
               counter < 500);

            if (counter == 500) {
               solveState = SolveState.SolveFailed;
            }
            else {
               if (inlet.HasSolvedAlready && outlet.HasSolvedAlready) {
                  solveState = SolveState.Solved;
               }
               //debug code
               Trace.WriteLine(this.name + "Number of Iterations" + counter);
               //debug code
            }

            dsOutlet.Pressure.State = VarState.Calculated;
            dsOutlet.Temperature.State = VarState.Calculated;
            dsOutlet.MassFlowRateDryBase.State = VarState.Calculated;
            dsOutlet.MoistureContentDryBase.State = VarState.Calculated;
            dsOutlet.OnSolveComplete(solveState);//has to call this so that the calculated state is updated on the uI
         }
            
         isBeingExecuted = false;
         OnSolveComplete(solveState);
      }

      protected Recycle(SerializationInfo info, StreamingContext context) : base(info, context) {
         solvingPriority = 5000;
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionRecycle", typeof(int));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionRecycle", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

