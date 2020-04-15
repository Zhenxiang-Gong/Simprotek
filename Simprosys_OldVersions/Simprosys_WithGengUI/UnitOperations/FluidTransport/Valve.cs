using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.FluidTransport {
   
   [Serializable] 
   public class Valve : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private ProcessVarDouble pressureDrop;
      
      #region public properties

      public ProcessVarDouble PressureDrop {
         get { return pressureDrop; }
      }

      #endregion

      public Valve(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         pressureDrop = new ProcessVarDouble(StringConstants.PRESSURE_DROP, PhysicalQuantity.Pressure, VarState.Specified, this);
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(pressureDrop);
      }
      
      public override bool CanAttachStream(ProcessStreamBase ps, int streamIndex) {
         if (!IsStreamValid(ps, streamIndex)) {
            return false;
         }
         bool canAttach = false;
         bool isLiquidMaterial = false;
         if (ps is DryingMaterialStream) {
            DryingMaterialStream dms = ps as DryingMaterialStream;
            isLiquidMaterial = (dms.MaterialStateType == MaterialStateType.Liquid);
         }

         if (isLiquidMaterial || ps is DryingGasStream || ps is ProcessStream) {
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
         }

         return canAttach;
      }

      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == pressureDrop) {
            if (aValue <= 0.0) {
               retValue = CreateLessThanOrEqualToZeroErrorMessage(pv);
            }
         }
         return retValue;
      }
      
      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.IsSpecifiedAndHasValue && aValue < outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the valve inlet must be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.IsSpecifiedAndHasValue && aValue > inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the valve outlet cannot be greater than that of the inlet.");
         }

         return retValue;
      }
      
      public override void Execute(bool propagate) {
         PreSolve();
         BalancePressure(inlet, outlet, pressureDrop);

         //dry gas flow balance
         if (inlet is DryingGasStream) {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;

            BalanceAdiabaticProcess(inlet, outlet);
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingGasStreamFlow(dsInlet, dsOutlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }

         else if (inlet is DryingMaterialStream) {
            DryingMaterialStream dsInlet = inlet as DryingMaterialStream;
            DryingMaterialStream dsOutlet = outlet as DryingMaterialStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingMaterialStreamFlow(dsInlet, dsOutlet);
            BalanceSpecificEnthalpy(inlet, outlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }
         else if (inlet is ProcessStream) {
            BalanceProcessStreamFlow(inlet, outlet);
         }

         if (inlet.Pressure.HasValue && outlet.Pressure.HasValue &&
             inlet.SpecificEnthalpy.HasValue && outlet.SpecificEnthalpy.HasValue) {
            solveState = SolveState.Solved;
         }

         PostSolve();
      }

      protected Valve(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionValve", typeof(int));
         if (persistedClassVersion == 1) {
            this.pressureDrop = RecallStorableObject("PressureDrop", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionValve", Valve.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("PressureDrop", this.PressureDrop, typeof(ProcessVarDouble));
      }
   }
}

