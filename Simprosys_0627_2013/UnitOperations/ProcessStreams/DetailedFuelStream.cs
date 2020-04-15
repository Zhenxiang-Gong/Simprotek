using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;
using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public class DetailedFuelStream : FuelStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private FossilFuel fossilFuel;

      public DetailedFuelStream(string name, FossilFuel mFossilFuel, UnitOperationSystem uoSys) : base(name, uoSys)
      {
         fossilFuel = mFossilFuel;
         InitializeVarListAndRegisterVars();
      }

      public FossilFuel DryingFuel
      {
         get { return fossilFuel; }
         set
         {
            fossilFuel = value;
            Components = new MaterialComponents(fossilFuel.ComponentList);
            HasBeenModified(true);
         }
      }

      public override void Execute(bool propagate)
      {
         if(massFlowRate.IsSpecifiedAndHasValue)
         {
            double massFlowValue = massFlowRate.Value;
            double moleFlowValue = 0;
            double volumeFlowValue = 0;
            foreach (MaterialComponent component in Components.Components) {
               Substance mySubstance = component.Substance;
               double myMassFraction = component.MassFraction.Value;
               moleFlowValue += massFlowValue * myMassFraction/mySubstance.MolarWeight;
               if(temperature.HasValue && pressure.HasValue)
               {
                  volumeFlowValue += massFlowValue * 0.082 * (myMassFraction / mySubstance.MolarWeight) * temperature.Value / pressure.Value * 1.013e5;
               }
            }

            Calculate(moleFlowRate, moleFlowValue);
            if(temperature.HasValue && pressure.HasValue)
            {
               Calculate(volumeFlowRate, volumeFlowValue);
               Calculate(density, massFlowValue / volumeFlowValue);
            }
         }
         else if(moleFlowRate.HasValue)
         {
            double moleFlowValue = moleFlowRate.Value;
            double massFlowValue = 0;
            double volumeFlowValue = 0;
            foreach(MaterialComponent component in Components.Components)
            {
               Substance mySubstance = component.Substance;
               double myMoleFraction = component.MoleFraction.Value;
               double componentMassFlow = moleFlowValue * myMoleFraction * mySubstance.MolarWeight;
               massFlowValue += componentMassFlow;
               if(temperature.HasValue && pressure.HasValue)
               {
                  volumeFlowValue += moleFlowValue * myMoleFraction * 0.082 * temperature.Value / pressure.Value * 1.013e5;
               }
            }

            Calculate(massFlowRate, massFlowValue);
            if(temperature.HasValue && pressure.HasValue)
            {
               Calculate(volumeFlowRate, volumeFlowValue);
               Calculate(density, massFlowValue/volumeFlowValue);
            }
         }
         else if(volumeFlowRate.IsSpecifiedAndHasValue)
         {
         }
         
         bool hasUnsolvedVar = false;
         foreach(ProcessVarDouble pv in varList)
         {
            if(!pv.HasValue)
            {
               hasUnsolvedVar = true;
               break;
            }
         }
         if(!hasUnsolvedVar)
         {
            solveState = SolveState.Solved;
         }

         AdjustVarsStates();
         OnSolveComplete();
      }

      protected DetailedFuelStream(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDetailedFuelStream", typeof(int));
         if (persistedClassVersion >= 1) {
            this.fossilFuel = (FossilFuel) info.GetValue("FossilFuel", typeof(FossilFuel));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionDetailedFuelStream", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("FossilFuel", this.fossilFuel, typeof(FossilFuel));
      }
   }
}
