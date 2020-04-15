using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.Materials;

namespace Prosimo.UnitOperations.ProcessStreams {

   [Serializable]
   public class GenericFuelStream : FuelStream {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private ProcessVarDouble heatValue;

      public ProcessVarDouble HeatValue {
         get { return heatValue; }
      }
 
      public GenericFuelStream(string name,FossilFuel mFossilFuel, UnitOperationSystem uoSys) : base(name, uoSys) {
         heatValue = new ProcessVarDouble(StringConstants.HEAT_VALUE, PhysicalQuantity.SpecificEnergy, VarState.Specified, this);
         InitializeVarListAndRegisterVars();
      }

      protected GenericFuelStream(SerializationInfo info, StreamingContext context) : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionGenericFuelStream", typeof(int));
         if (persistedClassVersion == 1) {
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionGenericFuelStream", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
