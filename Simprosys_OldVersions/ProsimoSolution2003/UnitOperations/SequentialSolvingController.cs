#define TRACE

using System;
using System.Diagnostics;
using System.Collections;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations {
	/// <summary>
	/// SequentialSolvingController is a class responsible for co-ordinating the 
	/// solving process of a flowsheet. Whenever a ProcessVar's value is changed by a user
	/// we need to erase everything calculated. Then a solving process is initiated.
	/// The solving process starts with firstly calculating all streams followed by 
	/// calculating each unit operation. After each unit operation is calculated if 
	/// any stream related to the unit operation has variable calculated the stream 
	/// is going to be recalculated. Then any unit operation related to this newly 
	/// calculated stream will be re-calculated agian. If a flowsheet has recycles, 
	/// each recycle needes to start calculation before the whole flowsheet is 
	/// solving.
	/// </summary>
	public class SequentialSolvingController {
      //we don't need to haved separate stream and unit op queues for erasing variable
      //values
      protected Queue eraseSolvableQueue = new Queue();
      protected Queue calculateStreamQueue = new Queue();
      protected ArrayList calculateUnitOpQueue = new ArrayList();
      
      //UnitOperation currentUnitOp;

      public SequentialSolvingController()  {
		}

      public void OnSolvableSpecified(Solvable solvable) {
         QueueStreamsAndUnitOpsForErase(solvable);
      }
      
      public void OnSolvableCalculated(Solvable solvable, bool specify) {
         QueueStreamsAndUnitOpsForCalculate(solvable, specify);
      }

      internal void ClearCalculateQueue() {
         calculateStreamQueue.Clear();
         calculateUnitOpQueue.Clear();
      }

      internal void EraseCalculatedValues() {   
         /*ProcessStreamBase ps;
         UnitOperation uo;
         while (eraseStreamQueue.Count > 0) {
            ps = eraseStreamQueue.Dequeue() as ProcessStreamBase;
            ps.Forget();
         }
         
         while (eraseUnitOpQueue.Count > 0) {
            uo = eraseUnitOpQueue.Dequeue() as UnitOperation;
            uo.Forget();
         }*/
         Solvable solvable;
         while (eraseSolvableQueue.Count > 0) {
            solvable = eraseSolvableQueue.Dequeue() as Solvable;
            solvable.EraseCalculatedProcVarValues();
         }
      }

      internal void Calculate() {
         //debug code
         Trace.WriteLine("Calculate())");
         //debug code

         CalculateStreams();

         UnitOperation currentUnitOp;
         while (calculateUnitOpQueue.Count > 0) {
            currentUnitOp = calculateUnitOpQueue[0] as UnitOperation;
            calculateUnitOpQueue.Remove(currentUnitOp);
            //skip any unit operation which is not fully attached
            if (!currentUnitOp.IsBalanceCalcReady()) {  
               continue;
            }
            try {
               currentUnitOp.Execute(true);
            }
            catch {
               currentUnitOp.IsBeingExecuted = false;
               throw;
            }

            //debug code
            Trace.WriteLine(currentUnitOp.Name + ": Execute()");
         }
      }
      
      internal void CalculateStreams() {
         //debug code
         Trace.WriteLine("CalculateStreams()");
         //debug code

         ProcessStreamBase ps;
         //calculate pass
         while (calculateStreamQueue.Count > 0) {
            ps = calculateStreamQueue.Dequeue() as ProcessStreamBase;
            try {
               ps.Execute(true);
            }
            catch {
               ps.IsBeingExecuted = false;
               throw;
            }

            //debug code
            Trace.WriteLine(ps.Name + ": Execute()");
            //debug code
         }
      }

      private void QueueStreamsAndUnitOpsForErase(Solvable solvable) {
         ProcessStreamBase ps = solvable as ProcessStreamBase;
         if (ps != null) {
            if (!eraseSolvableQueue.Contains(ps)) {
               eraseSolvableQueue.Enqueue(ps);
            }
            QueueStreamsAndUnitOpsForErase(ps);
         }
         else {
            UnitOperation uo = solvable as UnitOperation;
            if (uo != null && !eraseSolvableQueue.Contains(uo)) {
               QueueStreamsAndUnitOpsForErase(uo);
            }
         }
      }
      
      private void QueueStreamsAndUnitOpsForErase(ProcessStreamBase stream) {
         UnitOperation uo = stream.UpStreamOwner;
         if (uo != null && !eraseSolvableQueue.Contains(uo)) {
            QueueStreamsAndUnitOpsForErase(stream.UpStreamOwner);
         }
         
         uo = stream.DownStreamOwner;
         if (uo != null && !eraseSolvableQueue.Contains(uo)) {
            QueueStreamsAndUnitOpsForErase(stream.DownStreamOwner);
         }
      }

      private void QueueStreamsAndUnitOpsForErase(UnitOperation unitOp) {
         if (!eraseSolvableQueue.Contains(unitOp) && !unitOp.IsBeingExecuted) {
            eraseSolvableQueue.Enqueue(unitOp);
         }
         
         IList inletStreams = unitOp.InOutletStreams;   
         foreach (ProcessStreamBase ps in inletStreams) {
            //if ps has already been queued go to next ps
            if (eraseSolvableQueue.Contains(ps)) {
               continue;
            }

            eraseSolvableQueue.Enqueue(ps);
            if (ps.UpStreamOwner != null && ps.UpStreamOwner != unitOp) {
               QueueStreamsAndUnitOpsForErase(ps.UpStreamOwner);
            }
            if (ps.DownStreamOwner != null && ps.DownStreamOwner != unitOp) {
               QueueStreamsAndUnitOpsForErase(ps.DownStreamOwner);
            }
            
         }
      }
      
      private void QueueStreamsAndUnitOpsForCalculate(Solvable solvable, bool specify) {
         Trace.WriteLine("Queue solvable " + solvable.Name);

         if (solvable is ProcessStreamBase) {
            ProcessStreamBase ps = solvable as ProcessStreamBase;
            if (!ps.HasSolvedAlready) {
               calculateStreamQueue.Enqueue(ps);
            }
            QueueStreamsAndUnitOpsforForCalculate(ps, specify);
         }
         else if (solvable is UnitOperation) {
            UnitOperation uo = solvable as UnitOperation;
            if (!calculateUnitOpQueue.Contains(uo)) {
               QueueStreamsAndUnitOpsForCalculate(uo, specify);
            }
         }

      }
      
      private void QueueStreamsAndUnitOpsforForCalculate(ProcessStreamBase stream, bool specify) {
         //debug code
         Trace.WriteLine("Queue stream " + stream.Name);
         //debug code

         UnitOperation uo = stream.UpStreamOwner;
         if (uo != null && (specify || (!specify && !uo.HasSolvedAlready)) && !calculateUnitOpQueue.Contains(uo)) {
            QueueStreamsAndUnitOpsForCalculate(stream.UpStreamOwner, specify);
         }
         
         uo = stream.DownStreamOwner;
         if (uo != null && (specify || (!specify && !uo.HasSolvedAlready)) && !calculateUnitOpQueue.Contains(stream.DownStreamOwner)) {
            QueueStreamsAndUnitOpsForCalculate(stream.DownStreamOwner, specify);
         }
      }

      private void QueueStreamsAndUnitOpsForCalculate(UnitOperation unitOp, bool specify) {
         //debug code
         Trace.WriteLine("Queue unitOp " + unitOp.Name);
         //debug code
         
         //if (unitOp != currentUnitOp && !unitOp.AllDone && !calculateUnitOpQueue.Contains(unitOp)) {
         if (!unitOp.HasSolvedAlready && !calculateUnitOpQueue.Contains(unitOp) && !unitOp.IsBeingExecuted) {
            //calculateUnitOpQueue.Enqueue(unitOp);
            EnqueueCalculateUnitOp(unitOp);
         }
         if (!specify) {
            return;
         }
         
         IList inletStreams = unitOp.InOutletStreams;   
         foreach (ProcessStreamBase ps in inletStreams) {
            //if ps has already been queued go to next ps
            if (calculateStreamQueue.Contains(ps)) {
               continue;
            }

            if (!ps.HasSolvedAlready && !calculateStreamQueue.Contains(ps)) {
               calculateStreamQueue.Enqueue(ps);
            }
            if (ps.UpStreamOwner != null && ps.UpStreamOwner != unitOp) {
               QueueStreamsAndUnitOpsForCalculate(ps.UpStreamOwner, specify);
            }
            if (ps.DownStreamOwner != null && ps.DownStreamOwner != unitOp) {
               QueueStreamsAndUnitOpsForCalculate(ps.DownStreamOwner, specify);
            }
         }
      }

      private void EnqueueCalculateUnitOp(UnitOperation unitOp) {
         UnitOperation aUnitOp;
         if (calculateUnitOpQueue.Count == 0) {
            calculateUnitOpQueue.Add(unitOp);
         }
         else {
            for (int i = calculateUnitOpQueue.Count-1; i >=0 ; i--) {
               aUnitOp = calculateUnitOpQueue[i] as UnitOperation;
               if (unitOp.SolvingPriority >= aUnitOp.SolvingPriority) {
                  calculateUnitOpQueue.Insert(i+1, unitOp);
                  return;
               }
            }
            calculateUnitOpQueue.Insert(0, unitOp);
         }
      }
   }
}
