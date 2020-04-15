using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.Miscellaneous {
   
   [Serializable] 
   public class Recycle : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public Recycle(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
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
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = false;
         if (inlet != null && outlet != null && inlet.DownStreamOwner != null && inlet.UpStreamOwner != null &&
            outlet.DownStreamOwner != null && outlet.UpStreamOwner != null) {
            isReady = true;
         }

         return isReady;
      }

      //protected override bool IsSolveReady() {
      //   bool isReady = false;
      //   if (inlet is DryingGasStream) {
      //      DryingGasStream dsInlet = inlet as DryingGasStream;
      //      DryingGasStream dsOutlet = outlet as DryingGasStream;
      //      if ((dsInlet.Pressure.HasValue || dsOutlet.Pressure.HasValue) &&
      //          (dsInlet.Temperature.HasValue || dsOutlet.Temperature.HasValue) &&
      //          (dsInlet.MassFlowRateDryBase.HasValue || dsOutlet.MassFlowRateDryBase.HasValue || dsInlet.MassFlowRate.HasValue || dsOutlet.MassFlowRate.HasValue) &&
      //          (dsInlet.MoistureContentDryBase.HasValue || dsOutlet.MoistureContentDryBase.HasValue || dsInlet.MoistureContentWetBase.HasValue || dsOutlet.MoistureContentWetBase.HasValue)) {
      //         isReady = true;
      //      }
      //   }
      //   else if (inlet is DryingMaterialStream) {
      //      DryingMaterialStream dsInlet = inlet as DryingMaterialStream;
      //      DryingMaterialStream dsOutlet = outlet as DryingMaterialStream;
      //      if (dsInlet.MaterialStateType == MaterialStateType.Liquid) {
      //         if ((dsInlet.Pressure.HasValue || dsOutlet.Pressure.HasValue) &&
      //            (dsInlet.Temperature.HasValue || dsOutlet.Temperature.HasValue) &&
      //            (dsInlet.MassFlowRateDryBase.HasValue || dsOutlet.MassFlowRateDryBase.HasValue || dsInlet.MassFlowRate.HasValue || dsOutlet.MassFlowRate.HasValue) &&
      //            (dsInlet.MoistureContentDryBase.HasValue || dsOutlet.MoistureContentDryBase.HasValue || dsInlet.MoistureContentWetBase.HasValue || dsOutlet.MoistureContentWetBase.HasValue || dsInlet.MassConcentration.HasValue || dsOutlet.MassConcentration.HasValue)) {
      //            isReady = true;
      //         }
      //      }
      //      else if (dsInlet.MaterialStateType == MaterialStateType.Solid) {
      //         if ((dsInlet.Temperature.HasValue || dsOutlet.Temperature.HasValue) &&
      //            (dsInlet.MassFlowRateDryBase.HasValue || dsOutlet.MassFlowRateDryBase.HasValue || dsInlet.MassFlowRate.HasValue || dsOutlet.MassFlowRate.HasValue) && 
      //            (dsInlet.MoistureContentDryBase.HasValue || dsOutlet.MoistureContentDryBase.HasValue || dsInlet.MoistureContentWetBase.HasValue || dsOutlet.MoistureContentWetBase.HasValue)) {
      //            isReady = true;
      //         }
      //      }
      //   }
      //   return isReady;
      //}

      public override void Execute(bool propagate) {
         //if (!IsSolveReady()) {
         //   return;
         //}

         isBeingExecuted = true;
         BalanceStreamComponents(inlet, outlet);

         if (inlet is DryingStream) {
            DryingStream dsInlet = inlet as DryingStream;
            DryingStream dsOutlet = outlet as DryingStream;
            
            if (dsInlet.Pressure.HasValue && !dsOutlet.Pressure.HasValue) {
               dsOutlet.Pressure.Value = dsInlet.Pressure.Value;
            }

            IList<ProcessVarDouble> inletVarList = new List<ProcessVarDouble>();
            IList<ProcessVarDouble> outletVarList = new List<ProcessVarDouble>();
            IList<double> newValueList = new List<double>();

            if (dsInlet.MassFlowRateDryBase.HasValue && !dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MassFlowRateDryBase);
               outletVarList.Add(dsOutlet.MassFlowRateDryBase);
               newValueList.Add(dsInlet.MassFlowRateDryBase.Value);
            }
            else if (dsInlet.MassFlowRate.HasValue && !dsOutlet.MassFlowRate.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MassFlowRate);
               outletVarList.Add(dsOutlet.MassFlowRate);
               newValueList.Add(dsInlet.MassFlowRate.Value);
            }
            else if (!dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MassFlowRateDryBase);
               outletVarList.Add(dsOutlet.MassFlowRateDryBase);
               double newValue = dsInlet.MassFlowRateDryBase.Value;
               if (newValue == Constants.NO_VALUE) {
                  newValue = 0.01;
               }
               newValueList.Add(newValue);
            }

            if (!dsOutlet.Temperature.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.Temperature);
               outletVarList.Add(dsOutlet.Temperature);
               double newValue = dsInlet.Temperature.Value;
               if (newValue == Constants.NO_VALUE) {
                  newValue = 274.15;
               }
               newValueList.Add(newValue);
            }

            if (dsInlet.MoistureContentDryBase.HasValue && !dsOutlet.MoistureContentDryBase.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MoistureContentDryBase);
               outletVarList.Add(dsOutlet.MoistureContentDryBase);
               newValueList.Add(dsInlet.MoistureContentDryBase.Value);
            }
            else if (dsInlet.MoistureContentWetBase.HasValue && !dsOutlet.MoistureContentWetBase.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MoistureContentWetBase);
               outletVarList.Add(dsOutlet.MoistureContentWetBase);
               newValueList.Add(dsInlet.MoistureContentWetBase.Value);
            }
            else if (!dsOutlet.MoistureContentDryBase.IsSpecifiedAndHasValue) {
               inletVarList.Add(dsInlet.MoistureContentDryBase);
               outletVarList.Add(dsOutlet.MoistureContentDryBase);
               double newValue = dsInlet.MoistureContentDryBase.Value;
               if (newValue == Constants.NO_VALUE) {
                  newValue = 0.001;
               }
               newValueList.Add(newValue);
            }

            double[] oldValueList = new double[newValueList.Count];
            bool isGreaterThanTolerance;
            int counter = 0;
            do {
               counter++;
               isGreaterThanTolerance = false;
               for (int i = 0; i < outletVarList.Count; i++) {
                  outletVarList[i].Value = newValueList[i];
                  oldValueList[i] = newValueList[i];
               }

               dsOutlet.HasBeenModified(true);
               for (int j = 0; j < inletVarList.Count; j++) {
                  newValueList[j] = inletVarList[j].Value;
                  if (Math.Abs((newValueList[j] - oldValueList[j]) / oldValueList[j]) > 1.0e-6) {
                     isGreaterThanTolerance = true;
                     break;
                  }
               }

            } while (isGreaterThanTolerance && counter < 500);

            if (counter == 500) {
               currentSolveState = SolveState.SolveFailed;
            }
            else {
               if (inlet.HasSolvedAlready && outlet.HasSolvedAlready) {
                  currentSolveState = SolveState.Solved;
               }
               //debug code
               Trace.WriteLine(this.name + "Number of Iterations" + counter);
               //debug code
            }

            if (currentSolveState == SolveState.Solved) {
               dsOutlet.Pressure.State = VarState.Calculated;
               foreach (ProcessVarDouble outletVar in outletVarList) {
                  outletVar.State = VarState.Calculated;
               }
            }
            else if (currentSolveState == SolveState.NotSolved) {
               if (dsOutlet.Pressure.IsSpecifiedAndHasValue) {
                  dsOutlet.Pressure.Value = Constants.NO_VALUE;
               }
               foreach (ProcessVarDouble outletVar in outletVarList) {
                  outletVar.Value = Constants.NO_VALUE;
               }
            }

            dsOutlet.OnSolveComplete(currentSolveState);//has to call this so that the calculated state is updated on the UI
         }
            
         isBeingExecuted = false;
         OnSolveComplete(currentSolveState);
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

