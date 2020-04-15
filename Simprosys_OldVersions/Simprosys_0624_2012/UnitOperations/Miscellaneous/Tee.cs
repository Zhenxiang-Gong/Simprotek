using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.Miscellaneous {
   
   public delegate void TeeStreamFractionsNormolizedEventHandler(ArrayList streamAndFractions);

   /// <summary>
   /// Summary description for Tee.
   /// </summary>
   [Serializable] 
   public class Tee : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static int INLET_INDEX = 0;

      private ProcessStreamBase inlet;
      private ArrayList outletStreamAndFractions = new ArrayList();
      
      #region public properties
      public ProcessStreamBase Inlet {
         get { return inlet; }
      }

      public ArrayList OutletStreamAndFractions {
         get {return outletStreamAndFractions;}
      }
      #endregion

      public Tee(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanAttach(int streamIndex) 
      {
         bool retValue = false;
         if (streamIndex == INLET_INDEX && inlet == null) 
         {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) 
      {
         if (streamIndex == 0 && ps.DownStreamOwner != null || streamIndex > 0 && ps.UpStreamOwner != null) {
            return false;
         }
         bool canAttach = false;
         if (streamIndex == INLET_INDEX && inlet == null) {
            if (outletStreams.Count >= 1 && outletStreams[0].GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (outletStreams.Count < 1 && (ps is DryingStream || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex > INLET_INDEX) {
            if (inlet != null && inlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (inlet == null) {
               if (outletStreams.Count >= 1 && outletStreams[0].GetType() == ps.GetType()) {
                  canAttach = true;
               }
               else if (outletStreams.Count < 1 && (ps is DryingStream || ps is ProcessStream)) {
                  canAttach = true;
               }
            }
         }

         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = false;
         if (streamIndex > INLET_INDEX) {
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
            StreamAndFraction saf = new StreamAndFraction(ps, this);
            AddVarOnListAndRegisterInSystem(saf.Fraction);
            outletStreamAndFractions.Add(saf);
            attached = true;
         }
         else if (streamIndex == INLET_INDEX) {
            inlet = ps;
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
            attached = true;
         }

         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = false;
         if (ps == inlet) {
            inlet = null;
            ps.DownStreamOwner = null;
            inletStreams.Remove(ps);
            detached = true;
         }
         else {
            foreach (StreamAndFraction saf in outletStreamAndFractions) {
               if(ps == saf.Stream) {
                  ps.UpStreamOwner = null;
                  RemoveVarOnListAndUnregisterInSystem(saf.Fraction);
                  outletStreams.Remove(ps);
                  outletStreamAndFractions.Remove(saf);
                  detached = true;
                  break;
               }
            }
         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv.Type == PhysicalQuantity.Fraction) {
            if (aValue < 0.0 || aValue > 1.0) {
               retValue = CreateOutOfRangeZeroToOneErrorMessage(pv);
            }
            else {
               double totalFraction = 0.0;
               foreach (StreamAndFraction saf in outletStreamAndFractions) {
                  if (saf.Fraction.IsSpecifiedAndHasValue) {
                     totalFraction += saf.Fraction.Value;
                  }
               }
 
               if (totalFraction > 1.0) {
                  retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage("Total stream tee fraction must not exceed 1");
               }
            }
         }

         return retValue;
      }

      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (inlet == null || outletStreams.Count < 2) {
            isReady = false;
         }
         return isReady;
      }

//      protected override bool IsSolveReady() {
//         return true;
//      }

      public override void Execute(bool propagate) {
         PreSolve();
         if (IsSolveReady()) {
            Solve();
         }
         PostSolve();
            
         //OnSolveComplete(solveState);
      }
               
      private void Solve() {
         double totalFraction = 0.0;
         double totalFlow = 0.0;
         double totalFlowDryBase = 0.0;
         double fractionValue = 0.0;
         int numOfUnknownFraction = 0;
         int numOfUnknownFlow = 0;
         int numOfUnknownFlowDryBase = 0;
         int numOfKnown = 0;
         int j = -1;
         int k = -1;
         int l = -1;
         int fractionIndex = -1;
         int numOfKnownPressure = 0;
         int numOfKnownEnthalpy = 0;
         int numOfKnownTemperature = 0;
         ProcessStreamBase psb;
         int unknownFlowIndex = -1;
         int unknownFlowDryBaseIndex = -1;
         double temp;
         StreamAndFraction saf;
         DryingStream dsInlet = null;
         DryingStream dsOutlet;
         if (inlet is DryingStream) {
            dsInlet = inlet as DryingStream;
         }

         for (int i = 0; i < outletStreamAndFractions.Count; i++) {
            saf = outletStreamAndFractions[i] as StreamAndFraction;
            fractionValue = saf.Fraction.Value;
            if (fractionValue != Constants.NO_VALUE) {
               totalFraction += fractionValue;
               if (inlet.MassFlowRate.HasValue) {
                  Calculate(saf.Stream.MassFlowRate, inlet.MassFlowRate.Value * fractionValue);
               }
               else if (saf.Stream.MassFlowRate.HasValue && fractionValue > 1.0e-6) {
                  Calculate(inlet.MassFlowRate, saf.Stream.MassFlowRate.Value/fractionValue);
               }
                  //inlet mass flow rate dry base is known
               else if (inlet is DryingStream) {
                  dsOutlet = saf.Stream as DryingStream;
                  if (dsInlet.MassFlowRateDryBase.HasValue) {
                     Calculate(dsOutlet.MassFlowRateDryBase, dsInlet.MassFlowRateDryBase.Value * fractionValue);
                  }
                  else if (dsOutlet.MassFlowRateDryBase.HasValue && fractionValue > 1.0e-6) {
                     Calculate(dsInlet.MassFlowRateDryBase, dsOutlet.MassFlowRateDryBase.Value/fractionValue);
                  }
               }
            }
            else {
               bool fractionCalculated = false;
               if (inlet.MassFlowRate.HasValue && saf.Stream.MassFlowRate.HasValue) {
                  fractionCalculated = true;
                  fractionValue = saf.Stream.MassFlowRate.Value/inlet.MassFlowRate.Value;
                  if (fractionValue <= 1.0 && fractionValue >= 0.0) {
                     Calculate(saf.Fraction, fractionValue);
                     totalFraction += fractionValue;
                  }
               }
               else if (inlet is DryingStream) {
                  dsOutlet = saf.Stream as DryingStream;
                  if (dsInlet.MassFlowRateDryBase.HasValue && dsOutlet.MassFlowRateDryBase.HasValue) {
                     fractionCalculated = true;
                     fractionValue = dsOutlet.MassFlowRateDryBase.Value/dsInlet.MassFlowRateDryBase.Value;
                     if (fractionValue <= 1.0 && fractionValue >= 0.0) {
                        Calculate(saf.Fraction, fractionValue);
                        totalFraction += fractionValue;
                     }
                  }
               }
               if (!fractionCalculated) {
                  fractionIndex = i;
                  numOfUnknownFraction++;
               }
            }

            if (saf.Stream.MassFlowRate.HasValue) {
               totalFlow += saf.Stream.MassFlowRate.Value;
            }
            else {
               unknownFlowIndex = i;
               numOfUnknownFlow++;
            }
            
            if (inlet is DryingStream) {
               dsOutlet = saf.Stream as DryingStream;
               if (dsOutlet.MassFlowRateDryBase.HasValue) {
                  totalFlowDryBase += dsOutlet.MassFlowRateDryBase.Value;
               }
               else {
                  unknownFlowDryBaseIndex = i;
                  numOfUnknownFlowDryBase++;
               }
            }
         }

         //all fractions specified except one to be calculated
         if (numOfUnknownFraction == 1) {
            saf = outletStreamAndFractions[fractionIndex] as StreamAndFraction;
            fractionValue = (1.0 - totalFraction);
            Calculate(saf.Fraction, fractionValue);
            //if (inlet.MassFlowRate.HasValue && saf.Stream.MassFlowRate.IsSpecifiedAndHasNoValue) {
            if (inlet.MassFlowRate.HasValue && !saf.Stream.MassFlowRate.HasValue) {
               Calculate(saf.Stream.MassFlowRate, inlet.MassFlowRate.Value * fractionValue);
            }
            //else if (saf.Stream.MassFlowRate.HasValue && fractionValue > 1.0e-6 && inlet.MassFlowRate.IsSpecifiedAndHasNoValue) {
            else if (saf.Stream.MassFlowRate.HasValue && fractionValue > 1.0e-6 && !inlet.MassFlowRate.HasValue) {
               Calculate(inlet.MassFlowRate, saf.Stream.MassFlowRate.Value/fractionValue);
            }
            //inlet mass flow rate dry base is known
            else if (inlet is DryingStream) {
               dsOutlet = saf.Stream as DryingStream;
               //if (dsInlet.MassFlowRateDryBase.HasValue && dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasNoValue) {
               if (dsInlet.MassFlowRateDryBase.HasValue && !dsOutlet.MassFlowRateDryBase.HasValue) {
                  Calculate(dsOutlet.MassFlowRateDryBase, dsInlet.MassFlowRateDryBase.Value * fractionValue);
               }
               //else if (dsOutlet.MassFlowRateDryBase.HasValue && fractionValue > 1.0e-6 && dsInlet.MassFlowRateDryBase.IsSpecifiedAndHasNoValue) {
               else if (dsOutlet.MassFlowRateDryBase.HasValue && fractionValue > 1.0e-6 && !dsInlet.MassFlowRateDryBase.HasValue) {
                  Calculate(dsInlet.MassFlowRateDryBase, dsOutlet.MassFlowRateDryBase.Value/fractionValue);
               }
            }
         }
         
         if (numOfUnknownFlow == 1) {
            saf = outletStreamAndFractions[unknownFlowIndex] as StreamAndFraction;
            if (inlet.MassFlowRate.HasValue && inlet.MassFlowRate.Value > totalFlow) {
               //if (saf.Stream.MassFlowRate.IsSpecifiedAndHasNoValue) {
               if (!saf.Stream.MassFlowRate.HasValue) {
                  Calculate(saf.Stream.MassFlowRate, (inlet.MassFlowRate.Value - totalFlow));
               }
               //if (saf.Fraction.IsSpecifiedAndHasNoValue) {
               if (!saf.Fraction.HasValue) {
                  Calculate(saf.Fraction, saf.Stream.MassFlowRate.Value/inlet.MassFlowRate.Value);
               }
            }
         }
         else if (numOfUnknownFlow == 0) {
            Calculate(inlet.MassFlowRate, totalFlow);
            foreach (StreamAndFraction sf in outletStreamAndFractions) {
               Calculate(sf.Fraction, sf.Stream.MassFlowRate.Value/inlet.MassFlowRate.Value);
            }
         }
         else if (numOfUnknownFlowDryBase == 1 && dsInlet.MassFlowRateDryBase.HasValue) {
            saf = outletStreamAndFractions[unknownFlowDryBaseIndex] as StreamAndFraction;
            dsOutlet = saf.Stream as DryingStream;
            if (dsInlet.MassFlowRateDryBase.Value > totalFlowDryBase) {
               //if (dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasNoValue) {
               if (!dsOutlet.MassFlowRateDryBase.HasValue) {
                  Calculate(dsOutlet.MassFlowRateDryBase, (dsInlet.MassFlowRateDryBase.Value - totalFlowDryBase));
               }
               //if (saf.Fraction.IsSpecifiedAndHasNoValue) {
               if (!saf.Fraction.HasValue) {
                  Calculate(saf.Fraction, dsOutlet.MassFlowRateDryBase.Value/dsInlet.MassFlowRateDryBase.Value);
               }
            }
         }
         else if (numOfUnknownFlowDryBase == 0) {
            Calculate(dsInlet.MassFlowRateDryBase, totalFlowDryBase);
            foreach (StreamAndFraction sf in outletStreamAndFractions) {
               dsOutlet = sf.Stream as DryingStream;
               Calculate(sf.Fraction, dsOutlet.MassFlowRateDryBase.Value/dsInlet.MassFlowRateDryBase.Value);
            }
         }
         
         for (int i = 0; i < InOutletStreams.Count; i++) {
            psb = InOutletStreams[i] as ProcessStreamBase;
            if (psb.Pressure.HasValue) {
               numOfKnownPressure++;
               j = i;
            }
            if (psb.SpecificEnthalpy.HasValue) {
               numOfKnownEnthalpy++;
               k = i;
            }
            
            if (psb.Temperature.HasValue) {
               numOfKnownTemperature++;
               l = i;
            }

         }
            
         if (numOfKnownPressure == 1) {
            psb = InOutletStreams[j] as ProcessStreamBase;
            temp = psb.Pressure.Value;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               if (i != j) {
                  psb = InOutletStreams[i] as ProcessStreamBase;
                  Calculate(psb.Pressure, temp);
               }
            }
         }
         
         if (numOfKnownEnthalpy == 1) {
            psb = InOutletStreams[k] as ProcessStreamBase;
            temp = psb.SpecificEnthalpy.Value;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               if (i != k) {
                  psb = InOutletStreams[i] as ProcessStreamBase;
                  Calculate(psb.SpecificEnthalpy, temp);
               }
            }
         }
         else if (numOfKnownTemperature == 1) {
            psb = InOutletStreams[l] as ProcessStreamBase;
            temp = psb.Temperature.Value;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               if (i != l) {
                  psb = InOutletStreams[i] as ProcessStreamBase;
                  Calculate(psb.Temperature, temp);
               }
            }
         }

         //dry gas flow balance
         if (inlet is DryingGasStream) {
            DryingStream dsStream;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               dsStream = InOutletStreams[i] as DryingStream;
               if (dsStream.MoistureContentDryBase.HasValue) {
                  numOfKnown++;
                  j = i;
               }
            }
            if (numOfKnown == 1) {
               dsStream = InOutletStreams[j] as DryingStream;
               temp = dsStream.MoistureContentDryBase.Value;
               for (int i = 0; i < InOutletStreams.Count; i++) {
                  if (i != j) {
                     dsStream = InOutletStreams[i] as DryingStream;
                     Calculate(dsStream.MoistureContentDryBase, temp);
                  }
               }
            }

            DryingGasComponents inletDgc = (inlet as DryingGasStream).GasComponents;
            SolidPhase inletSp = inletDgc.SolidPhase;
            DryingGasComponents outletDgc;
            foreach (DryingGasStream outlet in outletStreams) {
               outletDgc = outlet.GasComponents;
               outletDgc.SolidPhase = inletDgc.SolidPhase;
            }
         }

         //density for drying material stream
         if (inlet is DryingMaterialStream) {
            DryingStream dsStream;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               dsStream = InOutletStreams[i] as DryingStream;
               if (dsStream.MoistureContentWetBase.HasValue) {
                  numOfKnown++;
                  j = i;
               }
            }
            if (numOfKnown == 1) {
               dsStream = InOutletStreams[j] as DryingStream;
               temp = dsStream.MoistureContentWetBase.Value;
               for (int i = 0; i < InOutletStreams.Count; i++) {
                  if (i != j) {
                     dsStream = InOutletStreams[i] as DryingStream;
                     Calculate(dsStream.MoistureContentWetBase, temp);
                  }
               }
            }

            numOfKnown = 0;
            for (int i = 0; i < InOutletStreams.Count; i++) {
               dsStream = InOutletStreams[i] as DryingMaterialStream;
               if (dsStream.Density.HasValue) {
                  numOfKnown++;
                  j = i;
               }
            }
            if (numOfKnown == 1) {
               dsStream = InOutletStreams[j] as DryingMaterialStream;
               temp = dsStream.Density.Value;
               for (int i = 0; i < InOutletStreams.Count; i++) {
                  if (i != j) {
                     dsStream = InOutletStreams[i] as DryingMaterialStream;
                     Calculate(dsStream.Density, temp);
                  }
               }
            }
         }

         //balanced gas stream solid phase
         //if (inlet is DryingGasStream) {
         //   DryingGasComponents inletDgc = (inlet as DryingGasStream).GasComponents;
         //   SolidPhase inletSp = inletDgc.SolidPhase;
         //   DryingGasComponents outletDgc;
         //   foreach (DryingGasStream outlet in outletStreams) {
         //      outletDgc = outlet.GasComponents;
         //      outletDgc.SolidPhase = inletDgc.SolidPhase;
         //   }
         //}
         
         DryingStream dryingStream;
         int numOfKnownMassFlow = 0;
         int numOfKnownMoistureContent = 0;
         numOfKnownPressure = 0;
         numOfKnownEnthalpy = 0;
         int numOfStrms = InOutletStreams.Count;
         for (int i = 0; i < numOfStrms; i++) {
            psb = InOutletStreams[i] as ProcessStreamBase;
            psb.Execute(false);
            if (psb.MassFlowRate.HasValue) {
               numOfKnownMassFlow++;
            }
            if (psb.Pressure.HasValue) {
               numOfKnownPressure++;
            }
            if (psb.Temperature.HasValue) {
               numOfKnownEnthalpy++;
            }
            if (inlet is DryingGasStream) {
               dryingStream = psb as DryingStream;
               if (dryingStream.MoistureContentDryBase.HasValue) {
                  numOfKnownMoistureContent++;
               }
            }
            else if (inlet is DryingMaterialStream) {
               dryingStream = psb as DryingStream;
               if (dryingStream.MoistureContentWetBase.HasValue) {
                  numOfKnownMoistureContent++;
               }
            }
         }

         if (numOfKnownMassFlow == numOfStrms && numOfKnownEnthalpy == numOfStrms) {
            if (inlet is ProcessStream && numOfKnownPressure == numOfStrms) {
               solveState = SolveState.Solved;
            }
            else if (inlet is DryingGasStream && numOfKnownPressure == numOfStrms && numOfKnownMoistureContent == numOfStrms) {
               solveState = SolveState.Solved;
            }
            else if (inlet is DryingMaterialStream && numOfKnownMoistureContent == numOfStrms) {
               solveState = SolveState.Solved;
            }
         }      
      }

      protected Tee(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionTee", typeof(int));
         if (persistedClassVersion == 1) {
            this.inlet = info.GetValue("Inlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
            this.outletStreamAndFractions = (ArrayList)RecallArrayListObject("OutletStreamAndFractions");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionTee", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Inlet", this.inlet, typeof(ProcessStreamBase));
         info.AddValue("OutletStreamAndFractions", outletStreamAndFractions, typeof(ArrayList));
      }
   }

   [Serializable] 
   public class StreamAndFraction  : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      private ProcessStreamBase stream;
      private ProcessVarDouble fraction;

      public StreamAndFraction(ProcessStreamBase stream, Solvable owner) {
         this.stream = stream;
         this.fraction = new ProcessVarDouble(stream.Name + "Fraction", PhysicalQuantity.Fraction, VarState.Specified, owner);
      }

      public ProcessStreamBase Stream {
         get {return stream;}
         set {stream = value;}
      }

      public ProcessVarDouble Fraction {
         get {return fraction;}
         set {fraction = value;}
      }

      protected StreamAndFraction(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionStreamAndFraction", typeof(int));
         if (persistedClassVersion == 1) {
            this.stream = (ProcessStreamBase)info.GetValue("Stream", typeof(ProcessStreamBase));
            this.fraction = (ProcessVarDouble)RecallStorableObject("Fraction", typeof(ProcessVarDouble));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionStreamAndFraction", StreamAndFraction.CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Stream", this.stream, typeof(ProcessStreamBase));
         info.AddValue("Fraction", this.fraction, typeof(ProcessVarDouble));
      }
   }
}

         //no fraction is specified but mass flow rates of inlet and outlet are specified
         /*else if (numOfUnknownFraction == outletStreamAndFractions.Count) {
            double totalFlow = 0.0;
            int numOfUnknownFlow = 0;
            for (int i = 0; i < outletStreamAndFractions.Count; i++) {
               saf = outletStreamAndFractions[i] as StreamAndFraction;
               if (saf.Stream.MassFlowRate.Value != Constants.NO_VALUE) {
                  totalFlow += saf.Stream.MassFlowRate.Value;
               }
               else {
                  j = i;
                  numOfUnknownFlow++;
               }
            }

            if (numOfUnknownFlow == 0) {
               Calculate(inlet.MassFlowRate, totalFlow);
               foreach (StreamAndFraction sf in outletStreamAndFractions) {
                  Calculate(sf.Fraction, sf.Stream.MassFlowRate.Value/inlet.MassFlowRate.Value);
               }
            }
            else if (numOfUnknownFlow == 1) {
               if (inlet.MassFlowRate.Value != Constants.NO_VALUE) {
                  saf = outletStreamAndFractions[j] as StreamAndFraction;
                  if (inlet.MassFlowRate.Value > totalFlow) {
                     Calculate(saf.Stream.MassFlowRate, (inlet.MassFlowRate.Value - totalFlow));
                  
                     foreach (StreamAndFraction sf in outletStreamAndFractions) {
                        Calculate(sf.Fraction, sf.Stream.MassFlowRate.Value/inlet.MassFlowRate.Value);
                     }
                  }
                  else {
                     throw new InappropriateSpecifiedValueException("The sum of mass flow rates of the outlet streams should not exceed the inlet mass flow rate");
                  }
               }
            }
               //dry gas flow balance
            else if (inlet is DryingStream) {
               totalFlow = 0.0;
               numOfUnknownFlow = 0;
               DryingStream dsInlet = inlet as DryingStream;
               DryingStream dsOutlet;
               for (int i = 0; i < outletStreamAndFractions.Count; i++) {
                  saf = outletStreamAndFractions[i] as StreamAndFraction;
                  dsOutlet = saf.Stream as DryingStream;
                  if (dsOutlet.MassFlowRateDryBase.Value != Constants.NO_VALUE) {
                     totalFlow += dsOutlet.MassFlowRateDryBase.Value;
                  }
                  else {
                     j = i;
                     numOfUnknownFlow++;
                  }
               }

               if (numOfUnknownFlow == 0) {
                  Calculate(dsInlet.MassFlowRateDryBase, totalFlow);
                  foreach (StreamAndFraction sf in outletStreamAndFractions) {
                     dsOutlet = sf.Stream as DryingStream;
                     Calculate(sf.Fraction, dsOutlet.MassFlowRateDryBase.Value/dsInlet.MassFlowRateDryBase.Value);
                  }
               }
               else if (numOfUnknownFlow == 1) {
                  if (dsInlet.MassFlowRateDryBase.Value != Constants.NO_VALUE) {
                     saf = outletStreamAndFractions[j] as StreamAndFraction;
                     dsOutlet = saf.Stream as DryingStream;
                     if (dsInlet.MassFlowRateDryBase.Value > totalFlow) {
                        Calculate(dsOutlet.MassFlowRateDryBase, (dsInlet.MassFlowRateDryBase.Value - totalFlow));
                  
                        foreach (StreamAndFraction sf in outletStreamAndFractions) {
                           dsOutlet = sf.Stream as DryingStream;
                           Calculate(sf.Fraction, dsOutlet.MassFlowRateDryBase.Value/dsInlet.MassFlowRateDryBase.Value);
                        }
                     }
                     else {
                        throw new InappropriateSpecifiedValueException("The sum of dry base mass flow rates of the outlet streams should not exceed the inlet dry base mass flow rate");
                     }
                  }
               }
            }
         }*/
         


