using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.FluidTransport {
   public enum CompressionProcess {Adiabatic = 0, Polytropic, Isothermal};
   /// <summary>
   /// Compressor is the compressor model which does the balance calculation.
   /// </summary>
   [Serializable]
   public class Compressor : TwoStreamUnitOperation {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private CompressionProcess compressionProcess;
      private ProcessVarDouble pressureRatio;
      private ProcessVarDouble adiabaticExponent;
      private ProcessVarDouble adiabaticEfficiency;
      private ProcessVarDouble polytropicExponent;
      private ProcessVarDouble polytropicEfficiency;
      private ProcessVarDouble powerInput;

      #region public properties
      public CompressionProcess CompressionProcess {
         get {return compressionProcess;}
//         set {
//            if (value != compressionProcess) {
//               compressionProcess = value;
//               HasBeenModified(true);
//            }
//         }
      }
      
      public ProcessVarDouble OutletInletPressureRatio {
         get { return pressureRatio; }
      }

      public ProcessVarDouble AdiabaticExponent {
         get { return adiabaticExponent;}
      }

      public ProcessVarDouble AdiabaticEfficiency {
         get { return adiabaticEfficiency;}
      }
      
      public ProcessVarDouble PolytropicEfficiency {
         get { return polytropicEfficiency;}
      }
      
      public ProcessVarDouble PolytropicExponent {
         get { return polytropicExponent;}
      }

      public ProcessVarDouble PowerInput {
         get { return powerInput;}
      }
      #endregion
      
      public Compressor(string name, UnitOperationSystem uoSys) : base(name, uoSys) {
         compressionProcess = CompressionProcess.Adiabatic;
         pressureRatio = new ProcessVarDouble(StringConstants.PRESSURE_RATIO, PhysicalQuantity.Unknown, VarState.Specified, this);
         polytropicExponent = new ProcessVarDouble(StringConstants.POLYTROPIC_EXPONENT, PhysicalQuantity.Unknown, VarState.Specified, this);
         adiabaticExponent = new ProcessVarDouble(StringConstants.ADIABATIC_EXPONENT, PhysicalQuantity.Unknown, VarState.Specified, this);
         polytropicEfficiency = new ProcessVarDouble(StringConstants.POLYTROPIC_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, this);
         adiabaticEfficiency = new ProcessVarDouble(StringConstants.ADIABATIC_EFFICIENCY, PhysicalQuantity.Fraction, VarState.AlwaysCalculated, this);
         powerInput = new ProcessVarDouble(StringConstants.POWER_INPUT, PhysicalQuantity.Power, VarState.AlwaysCalculated, this);
         InitializeVarListAndRegisterVars();
      }

      private void InitializeVarListAndRegisterVars() {
         AddVarOnListAndRegisterInSystem(pressureRatio);
         AddVarOnListAndRegisterInSystem(adiabaticExponent);
         AddVarOnListAndRegisterInSystem(polytropicEfficiency);
         AddVarOnListAndRegisterInSystem(adiabaticEfficiency);
         AddVarOnListAndRegisterInSystem(powerInput);
      }

      public ErrorMessage SpecifyCompressionProcess (CompressionProcess aValue) {
         ErrorMessage retMsg = null;
         if (aValue != compressionProcess) 
         {
            CompressionProcess oldValue = compressionProcess;
            compressionProcess = aValue;
            try 
            {
               HasBeenModified(true);
            }
            catch (Exception e) 
            {
               compressionProcess = oldValue;
               retMsg = HandleException(e);
            }
         }
         return retMsg;
      }
      
      protected override ErrorMessage CheckSpecifiedValueRange(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = base.CheckSpecifiedValueRange(pv, aValue);
         if (retValue != null) {
            return retValue;
         }

         if (pv == pressureRatio)
         {
            if (aValue < 1.0 || aValue > 1000) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " ratio must be in the range of 1.0 to 1000");
            }
         }
         else if (pv == adiabaticExponent)
         {
            if (aValue < 1.0 || aValue > 10.0) {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " must be in the range of 1.0 to 10.0");
            }
         }
         else if (pv == polytropicExponent)
         {
            if (aValue < 1.0 || aValue > 10.0) 
            {
               retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " must be in the range of 1.0 to 10.0");
            }
         }

         return retValue;
      }

      internal override ErrorMessage CheckSpecifiedValueInContext(ProcessVarDouble pv, double aValue) {
         ErrorMessage retValue = null;
         if (pv == inlet.Pressure && outlet.Pressure.HasValue && aValue > outlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the compressor inlet cannot be greater than that of the outlet.");
         }
         else if (pv == outlet.Pressure && inlet.Pressure.HasValue && aValue < inlet.Pressure.Value) {
            retValue = CreateSimpleGenericInappropriateSpecifiedValueErrorMessage(pv.VarTypeName + " of the compressor outlet cannot be smaller than that of the inlet.");
         }

         return retValue;
      }
      
      //      public void SpecifyCompressionProcess(CompressionProcess aValue) {
//         compressionProcess = aValue;
//      }

      public override void Execute(bool propagate) {
         PreSolve();
         //balance presssure
         //dry gas flow balance
         if (inlet is  DryingGasStream) {
            DryingGasStream dsInlet = inlet as DryingGasStream;
            DryingGasStream dsOutlet = outlet as DryingGasStream;
            
            //balance gas stream flow
            BalanceDryingStreamMoistureContent(dsInlet, dsOutlet);
            BalanceDryingGasStreamFlow(dsInlet, dsOutlet);
            AdjustVarsStates(dsInlet, dsOutlet);
         }
         else if (inlet is ProcessStream) {
            BalanceProcessStreamFlow(inlet, outlet);
         }

         Solve();
         PostSolve();
      }

      private void Solve() {
         double p1 = inlet.Pressure.Value;
         double p2 = outlet.Pressure.Value;
         double pRatio = pressureRatio.Value;
         double polytropicExp = polytropicExponent.Value;
         double v1 = inlet.VolumeFlowRate.Value;
         double adiabaticExp = adiabaticExponent.Value;
         //double v2 = outlet.VolumeFlowRate.Value;
         //double rho1 = inlet.Density.Value;
         //double rho2 = outlet.Density.Value;

         if (p1 != Constants.NO_VALUE && pRatio != Constants.NO_VALUE) {
            p2 = p1*pRatio;
            Calculate(outlet.Pressure, p2);
         }
         else if (p2 != Constants.NO_VALUE && pRatio != Constants.NO_VALUE) {
            p1 = p2/pRatio;
            Calculate(inlet.Pressure, p1);
         }
         else if (p1 != Constants.NO_VALUE && p2 != Constants.NO_VALUE) {
            pRatio = p2/p1;
            Calculate(pressureRatio, pRatio);
         }

         double k = polytropicExp;
         if (compressionProcess == CompressionProcess.Adiabatic) {
            k = adiabaticExp;
         }
         else if (compressionProcess == CompressionProcess.Isothermal) {
            k = 1.0;
         }
         
         double t1 = inlet.Temperature.Value;
         double t2 = outlet.Temperature.Value;
         if (k != Constants.NO_VALUE && pRatio != Constants.NO_VALUE) {
            double tempValue = Math.Pow(pRatio, (k-1.0)/k);
            if (t1 != Constants.NO_VALUE) {
               t2 = t1 * tempValue;
               Calculate(outlet.Temperature, t2);
            }
            else if (t2 != Constants.NO_VALUE) {
               t1 = t2 / tempValue;
               Calculate(inlet.Temperature, t1);
            }

            if (p1 != Constants.NO_VALUE) {
               double power;
               if (k == 1.0) {
                  power = p1 * v1 * Math.Log(pRatio);
               }
               else {
                  power = p1 * v1 * k / (k - 1) * (tempValue - 1.0);
               }
               Calculate(powerInput, power);
            }
         }

         if (polytropicExp != Constants.NO_VALUE && adiabaticExp != Constants.NO_VALUE &&
            pRatio != Constants.NO_VALUE) {
            double adibaticEff = (Math.Pow(pRatio, (adiabaticExp - 1.0) / adiabaticExp) - 1.0) / (Math.Pow(pRatio, (polytropicExp - 1.0) / polytropicExp) - 1.0);
            Calculate(adiabaticEfficiency, adibaticEff);
            double polytropicEff = polytropicExp * (adiabaticExp - 1.0) / (adiabaticExp * (polytropicExp - 1.0));
            Calculate(polytropicEfficiency, polytropicEff);
         }

         if (inlet.Pressure.HasValue && outlet.Pressure.HasValue && inlet.VolumeFlowRate.HasValue &&
            powerInput.HasValue) {
            solveState = SolveState.Solved;
         }
      }
   
      protected Compressor(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCompressor", typeof(int));
         if (persistedClassVersion == 1) {
            this.compressionProcess = (CompressionProcess)info.GetValue("CompressionProcess", typeof(CompressionProcess));
            this.pressureRatio = RecallStorableObject("PressureRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.adiabaticExponent = RecallStorableObject("AdiabaticExponent", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.adiabaticEfficiency = RecallStorableObject("AdiabaticEfficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.polytropicExponent = RecallStorableObject("PolytropicExponent", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.polytropicEfficiency = RecallStorableObject("PolytropicEfficiency", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.powerInput = RecallStorableObject("PowerInput", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionCompressor", Compressor.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CompressionProcess", this.compressionProcess, typeof(CompressionProcess));
         info.AddValue("PressureRatio", this.pressureRatio, typeof(ProcessVarDouble));
         info.AddValue("AdiabaticExponent", this.adiabaticExponent, typeof(ProcessVarDouble));
         info.AddValue("AdiabaticEfficiency", this.adiabaticEfficiency, typeof(ProcessVarDouble));
         info.AddValue("PolytropicExponent", this.polytropicExponent, typeof(ProcessVarDouble));
         info.AddValue("PolytropicEfficiency", this.polytropicEfficiency, typeof(ProcessVarDouble));
         info.AddValue("PowerInput", this.powerInput, typeof(ProcessVarDouble));
      }
   }
}
