using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.ThermalProperties;

namespace Prosimo.UnitOperations.Miscellaneous {
   /// <summary>
   /// Summary description for Mixer.
   /// </summary>
   [Serializable] 
   public class Mixer : UnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public static int OUTLET_INDEX = 0;

      private ProcessStreamBase outlet;

      #region public properties
      public ProcessStreamBase Outlet {
         get { return outlet; }
      }
      #endregion

      public Mixer(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
      }

      public override bool CanConnect(int streamIndex) 
      {
         bool retValue = false;
         if (streamIndex == OUTLET_INDEX && outlet == null) 
         {
            retValue = true;
         }
         return retValue;
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) 
      {
         if (streamIndex == 0 && ps.UpStreamOwner != null
            || streamIndex > 0 && ps.DownStreamOwner != null) {
            return false;
         }
         bool canAttach = false;
         if (streamIndex == OUTLET_INDEX && outlet == null) {
            if (inletStreams.Count >= 1 && inletStreams[0].GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (inletStreams.Count < 1 && (ps is DryingStream || ps is ProcessStream)) {
               canAttach = true;
            }
         }
         else if (streamIndex > OUTLET_INDEX) {
            if (outlet != null && outlet.GetType() == ps.GetType()) {
               canAttach = true;
            }
            else if (outlet == null) {
               if (inletStreams.Count >= 1 && inletStreams[0].GetType() == ps.GetType()) {
                  canAttach = true;
               }
               else if (inletStreams.Count < 1 && (ps is DryingStream || ps is ProcessStream)) {
                  canAttach = true;
               }
            }
         }

         return canAttach;
      }
      
      internal override bool DoAttach(ProcessStreamBase ps, int streamIndex) {
         bool attached = false;
         if (streamIndex > OUTLET_INDEX) {
            ps.DownStreamOwner = this;
            inletStreams.Add(ps);
            attached = true;
         }
         else if (streamIndex == OUTLET_INDEX) {
            outlet = ps;
            ps.UpStreamOwner = this;
            outletStreams.Add(ps);
            attached = true;
         }

         return attached;
      }
      
      internal override bool DoDetach(ProcessStreamBase ps) {
         bool detached = false;
         if (ps == outlet) {
            outlet = null;
            ps.UpStreamOwner = null;
            outletStreams.Remove(ps);
            detached = true;
         }
         else {
            if (inletStreams.Contains(ps)) 
            {
               ps.DownStreamOwner = null;
               inletStreams.Remove(ps);
               detached = true;
            }

         }

         if (detached) {
            HasBeenModified(true);
            ps.HasBeenModified(true);
            OnStreamDetached(this, ps);
         }

         return detached;
      }
      
      internal override bool IsBalanceCalcReady() {
         bool isReady = true;
         if (outlet == null || inletStreams.Count < 2) {
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
      }
               
      private void Solve() {
         double totalFlow = 0.0;
         double temp;
         int numOfUnknownFlow = 0;
         int unknownFlowIndex = -1;

         DryingStream dryingStream;
         DryingStream dsInlet;
         DryingStream dsOutlet = null;
         if (outlet is DryingStream) {
            dsOutlet = outlet as DryingStream;
         }

         ////flow balance
         //for (int i = 0; i < inletStreams.Count; i++) {
         //   inletStream = inletStreams[i] as ProcessStreamBase;
         //   if (inletStream.MassFlowRate.HasValue) {
         //      totalFlow += inletStream.MassFlowRate.Value;
         //   }
         //   else {
         //      unknownFlowIndex = i;
         //      numOfUnknownFlow++;
         //   }
         //}
         
         //if (numOfUnknownFlow == 1 && outlet.MassFlowRate.HasValue) {
         //   inletStream = inletStreams[unknownFlowIndex] as ProcessStreamBase;
         //   //if (outlet.MassFlowRate.Value > totalFlow && inletStream.MassFlowRate.IsSpecifiedAndHasNoValue) 
         //   if (outlet.MassFlowRate.Value > totalFlow && !inletStream.MassFlowRate.HasValue) {
         //      Calculate(inletStream.MassFlowRate, (outlet.MassFlowRate.Value - totalFlow));
         //   }
         //}
         //else if (numOfUnknownFlow == 0) {
         //   Calculate(outlet.MassFlowRate, totalFlow);
         //}

         //double inletTotal = 0.0;
         //int numOfUnknownInlet = 0;
         //int unknownInletIndex = -1;
         //moisture content balance
         //if (outlet is DryingStream) {
         //   double mcDryBase;
         //   double mcWetBase;
         //   for (int i = 0; i < inletStreams.Count; i++) {
         //      dsInlet = inletStreams[i] as DryingStream;
         //      mcWetBase = Constants.NO_VALUE;
         //      if (dsInlet.MoistureContentWetBase.HasValue) {
         //         mcWetBase = dsInlet.MoistureContentWetBase.Value;
         //      }
         //      else if (dsInlet.MoistureContentDryBase.HasValue) {
         //         mcDryBase = dsInlet.MoistureContentDryBase.Value;
         //         mcWetBase = mcDryBase/(1.0 + mcDryBase);
         //      }
         //      if (dsInlet.MassFlowRate.HasValue && mcWetBase != Constants.NO_VALUE) {
         //         //inletTotal += dsInlet.MassFlowRate.Value * mcDryBase/(1.0 + mcDryBase);
         //         inletTotal += dsInlet.MassFlowRate.Value * mcWetBase;
         //      }
         //      else {
         //         unknownInletIndex = i;
         //         numOfUnknownInlet++;
         //      }
         //   }
         
         //   mcDryBase = dsOutlet.MoistureContentDryBase.Value;
         //   if (numOfUnknownInlet == 1 && dsOutlet.MassFlowRate.HasValue && mcDryBase != Constants.NO_VALUE) {
         //      dsInlet = inletStreams[unknownInletIndex] as DryingStream;
         //      double outletMoisture = dsOutlet.MassFlowRate.Value * mcDryBase/(1.0 + mcDryBase);

         //      if (outletMoisture > inletTotal) {
         //         //if (dsInlet.MassFlowRate.HasValue && dsInlet.MoistureContentWetBase.IsSpecifiedAndHasNoValue) {
         //         if (dsInlet.MassFlowRate.HasValue && !dsInlet.MoistureContentWetBase.HasValue) {
         //            mcWetBase = (outletMoisture - inletTotal)/dsInlet.MassFlowRate.Value;
         //            Calculate(dsInlet.MoistureContentWetBase, mcWetBase);
         //         }
         //         //else if (dsInlet.MassFlowRate.HasValue && dsInlet.MoistureContentDryBase.IsSpecifiedAndHasNoValue) {
         //         else if (dsInlet.MassFlowRate.HasValue && !dsInlet.MoistureContentDryBase.HasValue) {
         //            mcWetBase = (outletMoisture - inletTotal)/dsInlet.MassFlowRate.Value;
         //            Calculate(dsInlet.MoistureContentDryBase, mcWetBase/(1.0 - mcWetBase));
         //         }
         //         //else if (dsInlet.MassFlowRateDryBase.HasValue && dsInlet.MoistureContentDryBase.IsSpecifiedAndHasNoValue) {
         //         else if (dsInlet.MassFlowRateDryBase.HasValue && !dsInlet.MoistureContentDryBase.HasValue) {
         //            mcDryBase = (outletMoisture - inletTotal)/dsInlet.MassFlowRateDryBase.Value;
         //            Calculate(dsInlet.MoistureContentDryBase, mcDryBase);
         //         } 
         //         //else if (dsInlet.MoistureContentDryBase.HasValue && dsInlet.MassFlowRateDryBase.IsSpecifiedAndHasNoValue) {
         //         else if (dsInlet.MoistureContentDryBase.HasValue && !dsInlet.MassFlowRateDryBase.HasValue) {
         //            double massFlowDryBase = (outletMoisture - inletTotal)/dsInlet.MoistureContentDryBase.Value;
         //            Calculate(dsInlet.MassFlowRateDryBase, massFlowDryBase);
         //         }
         //         //else if (dsInlet.MoistureContentDryBase.HasValue && dsInlet.MassFlowRate.IsSpecifiedAndHasNoValue) {
         //         else if (dsInlet.MoistureContentDryBase.HasValue && !dsInlet.MassFlowRate.HasValue) {
         //            mcDryBase = dsInlet.MoistureContentDryBase.Value;
         //            double massFlow = (outletMoisture - inletTotal)/mcDryBase * (1.0 + mcDryBase);
         //            Calculate(dsInlet.MassFlowRate, massFlow);
         //         }
         //      }
         //   }
         //   else if (numOfUnknownInlet == 0) {
         //      //if (dsOutlet.MassFlowRate.HasValue && dsOutlet.MoistureContentWetBase.IsSpecifiedAndHasNoValue) {
         //      if (dsOutlet.MassFlowRate.HasValue && !dsOutlet.MoistureContentWetBase.HasValue) {
         //         mcWetBase = inletTotal/dsOutlet.MassFlowRate.Value;
         //         Calculate(dsOutlet.MoistureContentWetBase, mcWetBase);
         //      }
         //      //else if (dsOutlet.MassFlowRate.HasValue && dsOutlet.MoistureContentDryBase.IsSpecifiedAndHasNoValue) {
         //      else if (dsOutlet.MassFlowRate.HasValue && !dsOutlet.MoistureContentDryBase.HasValue) {
         //         mcWetBase = inletTotal/dsOutlet.MassFlowRate.Value;
         //         Calculate(dsOutlet.MoistureContentDryBase, mcWetBase/(1.0 - mcWetBase));
         //      }
         //      //else if (dsOutlet.MassFlowRateDryBase.HasValue && dsOutlet.MoistureContentDryBase.IsSpecifiedAndHasNoValue) {
         //      else if (dsOutlet.MassFlowRateDryBase.HasValue && !dsOutlet.MoistureContentDryBase.HasValue) {
         //         mcDryBase = inletTotal/dsOutlet.MassFlowRateDryBase.Value;
         //         Calculate(dsOutlet.MoistureContentDryBase, mcDryBase);
         //      } 
         //      //else if (dsOutlet.MoistureContentDryBase.HasValue && dsOutlet.MassFlowRateDryBase.IsSpecifiedAndHasNoValue) {
         //      else if (dsOutlet.MoistureContentDryBase.HasValue && !dsOutlet.MassFlowRateDryBase.HasValue) {
         //         double massFlowDryBase = inletTotal/dsOutlet.MoistureContentDryBase.Value;
         //         Calculate(dsOutlet.MassFlowRateDryBase, massFlowDryBase);
         //      }
         //      //else if (dsOutlet.MoistureContentDryBase.HasValue && dsOutlet.MassFlowRate.IsSpecifiedAndHasNoValue) {
         //      else if (dsOutlet.MoistureContentDryBase.HasValue && !dsOutlet.MassFlowRate.HasValue) {
         //         mcDryBase = dsOutlet.MoistureContentDryBase.Value;
         //         double massFlow = inletTotal/mcDryBase * (1.0 + mcDryBase);
         //         Calculate(dsOutlet.MassFlowRate, massFlow);
         //      }
         //   }
         //}

         double inletTotal = 0.0;
         int numOfUnknownInlet = 0;
         int unknownInletIndex = -1;
         
         if (outlet is DryingGasStream) {
            DryingGasStream dgsInlet;
            for (int i = 0; i < inletStreams.Count; i++) {
               dgsInlet = inletStreams[i] as DryingGasStream;
               if (dgsInlet.MassFlowRateDryBase.HasValue) {
                  totalFlow += dgsInlet.MassFlowRateDryBase.Value;
               }
               else {
                  unknownFlowIndex = i;
                  numOfUnknownFlow++;
               }
            }

            if (numOfUnknownFlow == 1 && dsOutlet.MassFlowRateDryBase.HasValue) {
               dsInlet = inletStreams[unknownFlowIndex] as DryingGasStream;
               if (dsOutlet.MassFlowRateDryBase.Value > totalFlow && !dsInlet.MassFlowRateDryBase.HasValue) {
                  Calculate(dsInlet.MassFlowRateDryBase, (dsOutlet.MassFlowRateDryBase.Value - totalFlow));
               }
            }
            else if (numOfUnknownFlow == 0) {
               Calculate(dsOutlet.MassFlowRateDryBase, totalFlow);
            }
            
            for (int i = 0; i < inletStreams.Count; i++) {
               dgsInlet = inletStreams[i] as DryingGasStream;
               if (dgsInlet.MoistureContentDryBase.HasValue && dgsInlet.MassFlowRateDryBase.HasValue) {
                  inletTotal += dgsInlet.MassFlowRateDryBase.Value * dgsInlet.MoistureContentDryBase.Value;
               }
               else {
                  unknownInletIndex = i;
                  numOfUnknownInlet++;
               }
            }

            if (numOfUnknownInlet == 1 && dsOutlet.MassFlowRateDryBase.HasValue && dsOutlet.MoistureContentDryBase.HasValue) {
               dgsInlet = inletStreams[unknownInletIndex] as DryingGasStream;
               double outletMoisture = dsOutlet.MassFlowRateDryBase.Value * dsOutlet.MoistureContentDryBase.Value;

               if (outletMoisture > inletTotal) {
                  if (dgsInlet.MassFlowRateDryBase.HasValue && !dgsInlet.MoistureContentDryBase.HasValue) {
                     double mcDryBase = (outletMoisture - inletTotal) / dgsInlet.MassFlowRateDryBase.Value;
                     Calculate(dgsInlet.MoistureContentDryBase, mcDryBase);
                  }
                  else if (dgsInlet.MoistureContentDryBase.HasValue && !dgsInlet.MassFlowRateDryBase.HasValue) {
                     double massFlowDryBase = (outletMoisture - inletTotal) / dgsInlet.MoistureContentDryBase.Value;
                     Calculate(dgsInlet.MassFlowRateDryBase, massFlowDryBase);
                  }
               }
               else {
                  throw new CalculationFailedException(this.name + "Total mositure content from inlets is greater than that of the outlet");
               }
            }
            else if (numOfUnknownInlet == 0) {
               if (dsOutlet.MassFlowRateDryBase.HasValue && !dsOutlet.MoistureContentDryBase.HasValue) {
                  double mcDryBase = inletTotal / dsOutlet.MassFlowRateDryBase.Value;
                  Calculate(dsOutlet.MoistureContentDryBase, mcDryBase);
               }
               else if (dsOutlet.MoistureContentDryBase.HasValue && !dsOutlet.MassFlowRateDryBase.HasValue) {
                  double massFlowDryBase = inletTotal / dsOutlet.MoistureContentDryBase.Value;
                  Calculate(dsOutlet.MassFlowRateDryBase, massFlowDryBase);
               }
            }
         }
         else if (outlet is DryingMaterialStream) {
            DryingMaterialStream dmsInlet;
            for (int i = 0; i < inletStreams.Count; i++) {
               dmsInlet = inletStreams[i] as DryingMaterialStream;
               if (dmsInlet.MassFlowRate.HasValue) {
                  totalFlow += dmsInlet.MassFlowRate.Value;
               }
               else {
                  unknownFlowIndex = i;
                  numOfUnknownFlow++;
               }
            }

            if (numOfUnknownFlow == 1 && outlet.MassFlowRate.HasValue) {
               dmsInlet = inletStreams[unknownFlowIndex] as DryingMaterialStream;
               if (outlet.MassFlowRate.Value > totalFlow && !dmsInlet.MassFlowRate.HasValue) {
                  Calculate(dmsInlet.MassFlowRate, (outlet.MassFlowRate.Value - totalFlow));
               }
            }
            else if (numOfUnknownFlow == 0) {
               Calculate(outlet.MassFlowRate, totalFlow);
            }
            for (int i = 0; i < inletStreams.Count; i++) {
               dmsInlet = inletStreams[i] as DryingMaterialStream;
               if (dmsInlet.MassFlowRate.HasValue && dmsInlet.MoistureContentWetBase.HasValue) {
                  inletTotal += dmsInlet.MassFlowRate.Value * dmsInlet.MoistureContentWetBase.Value;
               }
               else {
                  unknownInletIndex = i;
                  numOfUnknownInlet++;
               }
            }

            if (numOfUnknownInlet == 1 && dsOutlet.MassFlowRate.HasValue && dsOutlet.MoistureContentWetBase.HasValue) {
               dmsInlet = inletStreams[unknownInletIndex] as DryingMaterialStream;
               double outletMoisture = dsOutlet.MassFlowRate.Value * dsOutlet.MoistureContentWetBase.Value;

               if (outletMoisture > inletTotal) {
                  if (dmsInlet.MassFlowRate.HasValue && !dmsInlet.MoistureContentWetBase.HasValue) {
                     double mcWetBase = (outletMoisture - inletTotal) / dmsInlet.MassFlowRate.Value;
                     Calculate(dmsInlet.MoistureContentWetBase, mcWetBase);
                  }
                  else if (dmsInlet.MoistureContentWetBase.HasValue && !dmsInlet.MassFlowRate.HasValue) {
                     double massFlow = (outletMoisture - inletTotal)/dmsInlet.MoistureContentWetBase.Value;
                     Calculate(dmsInlet.MassFlowRate, massFlow);
                  }
               }
               else {
                  throw new CalculationFailedException(this.name + "Total mositure content from inlets is greater than that of the outlet");
               }
            }
            else if (numOfUnknownInlet == 0) {
               if (dsOutlet.MassFlowRate.HasValue && !dsOutlet.MoistureContentWetBase.HasValue) {
                  double mcWetBase = inletTotal / dsOutlet.MassFlowRate.Value;
                  Calculate(dsOutlet.MoistureContentWetBase, mcWetBase);
               }
               else if (dsOutlet.MoistureContentWetBase.HasValue && !dsOutlet.MassFlowRate.HasValue) {
                  double massFlow = inletTotal / dsOutlet.MoistureContentWetBase.Value;
                  Calculate(dsOutlet.MassFlowRate, massFlow);
               }
            }
         }

         //have to recalculate the streams so that the following balance calcualtion
         //can have all the latest balance calculated values taken into account
         UpdateStreamsIfNecessary();
         
         inletTotal = 0.0;
         numOfUnknownInlet = 0;
         unknownInletIndex = -1;
         double inletTotalDryBase = 0.0;
         int numOfUnknownInletDryBase = 0;
         int unknownInletIndexDryBase = -1;

         ProcessStreamBase inletStream;
         for (int i = 0; i < inletStreams.Count; i++) {
            inletStream = inletStreams[i] as ProcessStreamBase;
            if (inletStream.MassFlowRate.HasValue && inletStream.SpecificEnthalpy.HasValue) {
               inletTotal += inletStream.MassFlowRate.Value * inletStream.SpecificEnthalpy.Value;
            }
            else {
               unknownInletIndex = i;
               numOfUnknownInlet++;
            }
         
            if (outlet is DryingStream) {
               dsInlet = inletStream as DryingStream;
               if (dsInlet.MassFlowRateDryBase.HasValue && dsInlet.SpecificEnthalpyDryBase.HasValue) {
                  inletTotalDryBase += dsInlet.MassFlowRateDryBase.Value * dsInlet.SpecificEnthalpyDryBase.Value;
               }
               else {
                  unknownInletIndexDryBase = i;
                  numOfUnknownInletDryBase++;
               }
            }
         }
         
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();

         if (numOfUnknownInletDryBase == 1
            && (dsOutlet.MassFlowRate.HasValue && dsOutlet.SpecificEnthalpy.HasValue)) {
            
            dsInlet = inletStreams[unknownInletIndexDryBase] as DryingStream;
            double outletEnergy = dsOutlet.MassFlowRate.Value * dsOutlet.SpecificEnthalpy.Value;
            
            if (outletEnergy > inletTotalDryBase) {
               if (dsInlet.MassFlowRate.HasValue && !dsInlet.SpecificEnthalpy.HasValue) {
                  temp = (outletEnergy - inletTotalDryBase)/dsInlet.MassFlowRate.Value;
                  Calculate(dsInlet.SpecificEnthalpy, temp);
               }
               else if (dsInlet.MassFlowRateDryBase.HasValue && !dsInlet.SpecificEnthalpyDryBase.HasValue) {
                  temp = (outletEnergy - inletTotalDryBase)/dsInlet.MassFlowRateDryBase.Value;
                  Calculate(dsInlet.SpecificEnthalpyDryBase, temp);
               }
               else if (dsInlet.SpecificEnthalpy.HasValue && !dsInlet.MassFlowRate.HasValue) {
                  temp = (outletEnergy - inletTotalDryBase)/dsInlet.SpecificEnthalpy.Value;
                  Calculate(dsInlet.MassFlowRate, temp);
               }
               else if (dsInlet.SpecificEnthalpyDryBase.HasValue && !dsInlet.MassFlowRateDryBase.HasValue) {
                  temp = (outletEnergy - inletTotalDryBase)/dsInlet.SpecificEnthalpyDryBase.Value;
                  Calculate(dsInlet.MassFlowRateDryBase, temp);
               }
            }
         }
         else if (numOfUnknownInletDryBase == 0) {
            if (dsOutlet.MassFlowRate.HasValue && !dsOutlet.SpecificEnthalpy.HasValue) {
               temp = inletTotalDryBase/dsOutlet.MassFlowRate.Value;
               Calculate(dsOutlet.SpecificEnthalpy, temp);
            }
            if (dsOutlet.MassFlowRateDryBase.HasValue && !dsOutlet.SpecificEnthalpyDryBase.HasValue) {
               temp = inletTotalDryBase/dsOutlet.MassFlowRateDryBase.Value;
               Calculate(dsOutlet.SpecificEnthalpyDryBase, temp);
            }
            else if (dsOutlet.SpecificEnthalpy.HasValue && !dsOutlet.MassFlowRate.HasValue) {
               temp = inletTotalDryBase/dsOutlet.SpecificEnthalpy.Value;
               Calculate(dsOutlet.MassFlowRate, temp);
            }
            else if (dsOutlet.SpecificEnthalpyDryBase.HasValue && !dsOutlet.MassFlowRateDryBase.HasValue) {
               temp = inletTotalDryBase/dsOutlet.SpecificEnthalpyDryBase.Value;
               Calculate(dsOutlet.MassFlowRateDryBase, temp);
            }
         }
         else if (numOfUnknownInlet == 1 && outlet.MassFlowRate.HasValue && outlet.SpecificEnthalpy.HasValue
               && outlet.SpecificHeat.HasValue) {
            inletStream = inletStreams[unknownInletIndex] as ProcessStreamBase;
            double outletEnergy = outlet.MassFlowRate.Value * outlet.Temperature.Value * outlet.SpecificHeat.Value;
            if (outletEnergy > inletTotal) {
               if (inletStream.MassFlowRate.HasValue && !inletStream.SpecificEnthalpy.HasValue) {
                  temp = (outletEnergy - inletTotal)/inletStream.MassFlowRate.Value;
                  Calculate(inletStream.SpecificEnthalpy, temp);
               }
               else if (inletStream.SpecificEnthalpy.HasValue && !inletStream.MassFlowRate.HasValue) {
                  temp = (outletEnergy - inletTotal)/inletStream.SpecificEnthalpy.Value;
                  Calculate(inletStream.MassFlowRate, temp);
               }
            }
         }
         else if (numOfUnknownInlet == 0) {
            if (outlet.MassFlowRate.HasValue && !outlet.SpecificEnthalpy.HasValue) {
               temp = inletTotal/outlet.MassFlowRate.Value;
               Calculate(outlet.SpecificEnthalpy, temp);
            }
            else if (outlet.SpecificEnthalpy.HasValue && !outlet.MassFlowRate.HasValue) {
               temp = inletTotal/outlet.SpecificEnthalpy.Value;
               Calculate(outlet.MassFlowRate, temp);
            }
         }

         ProcessStreamBase stream;
         int numOfKnownMassFlow = 0;
         int numOfKnownPressure = 0;
         int numOfKnownTemperature = 0;
         int numOfKnownMoistureContent = 0;
         int numOfStrms = InOutletStreams.Count;
         for (int i = 0; i < numOfStrms; i++) {
            stream = InOutletStreams[i] as ProcessStreamBase;
            if (stream.MassFlowRate.HasValue) {
               numOfKnownMassFlow++;
            }
            if (stream.Pressure.HasValue) {
               numOfKnownPressure++;
            }
            if (stream.SpecificEnthalpy.HasValue) {
               numOfKnownTemperature++;
            }
            if (outlet is DryingStream) {
               dryingStream = stream as DryingStream;
               if (dryingStream.MoistureContentDryBase.HasValue || dryingStream.MoistureContentWetBase.HasValue) {
                  numOfKnownMoistureContent++;
               }
            }
         }

         if (numOfKnownMassFlow == numOfStrms && numOfKnownTemperature == numOfStrms) {
            if (outlet is ProcessStream && numOfKnownPressure == numOfStrms) {
               solveState = SolveState.Solved;
            }
            else if (outlet is DryingGasStream && numOfKnownPressure == numOfStrms && numOfKnownMoistureContent == numOfStrms) {
               solveState = SolveState.Solved;
            }
            else if (outlet is DryingMaterialStream && numOfKnownMoistureContent == numOfStrms) {
               solveState = SolveState.Solved;
            }
         }

         if (solveState == SolveState.Solved && outlet is DryingGasStream) {
            double totalSolidPhaseMass = 0.0;
            SolidPhase solidPhase = null;
            SolidPhase lastSolidPhase = null;
            foreach (DryingGasStream gasStream in inletStreams) {
               solidPhase = gasStream.GasComponents.SolidPhase;
               if (solidPhase != null) {
                  totalSolidPhaseMass += solidPhase.MassFraction * gasStream.MassFlowRate.Value;
                  lastSolidPhase = solidPhase;
               }
            }

            if (lastSolidPhase != null) {
               double outletSolidPhaseMassFraction = totalSolidPhaseMass / outlet.MassFlowRate.Value;
               DryingGasStream dgsOutlet = outlet as DryingGasStream;
               SolidPhase outletSolidPhase = dgsOutlet.GasComponents.SolidPhase;
               if (outletSolidPhase != null) {
                  outletSolidPhase.MassFraction = outletSolidPhaseMassFraction;
               }
               else {
                  outletSolidPhase = lastSolidPhase.Clone();
                  outletSolidPhase.MassFraction = outletSolidPhase.MassFraction;
                  dgsOutlet.GasComponents.SolidPhase = outletSolidPhase;
               }
            }
         }
      }

      protected Mixer(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMixer", typeof(int));
         if (persistedClassVersion == 1) {
            this.outlet = info.GetValue("Outlet", typeof(ProcessStreamBase)) as ProcessStreamBase;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionMixer", CLASS_PERSISTENCE_VERSION, typeof(int));
         
         info.AddValue("Outlet", this.outlet, typeof(ProcessStreamBase));
      }
   }
}

/*         if (dsOutlet.MassFlowRateDryBase.Value == Constants.NO_VALUE) {
               flowBalanceOK = true;
               foreach (DryingStream ds in inletStreams) {
                  if (ds.MassFlowRateDryBase.Value == Constants.NO_VALUE) {
                     flowBalanceOK = false;
                     break;
                  }
                  sum += ds.MassFlowRateDryBase.Value;
               }
               if (flowBalanceOK) {
                  Calculate(dsOutlet.MassFlowRateDryBase, sum);
               }
            }
            else {
               flowBalanceOK = false;
               numOfInletToBeDetermined = 0;
               theDryingStream = null;
               for (int i = 0; i < inletStreams.Count; i++) {
                  dryingStream = inletStreams[i] as DryingStream;
                  if (dryingStream.MassFlowRateDryBase.Value == Constants.NO_VALUE) {
                     theDryingStream = dryingStream;
                     numOfInletToBeDetermined++;
                  }
                  else {
                     sum += dryingStream.MassFlowRateDryBase.Value;
                  }
               }
               if (numOfInletToBeDetermined == 1) {
                  flowBalanceOK = true;
                  Calculate(theDryingStream.MassFlowRateDryBase, (dsOutlet.MassFlowRateDryBase.Value - sum));
               }
            }*/



