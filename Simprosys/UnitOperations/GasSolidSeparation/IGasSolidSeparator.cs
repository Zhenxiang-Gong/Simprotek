using System;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations.GasSolidSeparation {

   public interface IGasSolidSeparator{

      ProcessStreamBase GasInlet {
         get;
      }

      ProcessStreamBase GasOutlet {
         get;
      }
      
      ProcessVarDouble GasPressureDrop
      {
         get;
      }

      ProcessVarDouble CollectionEfficiency {
         get;
      }

      ProcessVarDouble InletParticleLoading {
         get;
      }
      
      ProcessVarDouble OutletParticleLoading {
         get;
      }
      
      ProcessVarDouble ParticleCollectionRate {
         get;
      }
      
      ProcessVarDouble MassFlowRateOfParticleLostToGasOutlet {
         get;
      }

      double CalculateParticleLoading(ProcessStreamBase stream);
      
      UnitOperation MyUnitOperation {
         get;
      }
   }
}
