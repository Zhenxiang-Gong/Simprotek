using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations
{
   /// <summary>
   /// Summary description for EvaporationAndDryingSystemPreferences.
   /// </summary>
   [Serializable]
   public class EvaporationAndDryingSystemPreferences : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      private UnitOpCreationType unitOpCreationType;
      //private MaterialStateType dryerMaterialInletType;
      private Type heatExchangerColdInletType;
      private Type heatExchangerHotInletType;
      private Type teeStreamsType;
      private Type mixerStreamsType;
      private Type valveStreamsType;
      
//      public MaterialStateType DryerMaterialInletType {
//         get {return dryerMaterialInletType;}
//         set {dryerMaterialInletType = value;}
//      }

      public UnitOpCreationType UnitOpCreationType {
         get {return unitOpCreationType;}
         set {unitOpCreationType = value;}
      }
      
      public Type HeatExchangerColdInletType
      {
         get {return heatExchangerColdInletType;}
         set {heatExchangerColdInletType = value;}
      }

      public Type HeatExchangerHotInletType
      {
         get {return heatExchangerHotInletType;}
         set {heatExchangerHotInletType = value;}
      }

      public Type TeeStreamsType
      {
         get {return teeStreamsType;}
         set {teeStreamsType = value;}
      }

      public Type MixerStreamsType
      {
         get {return mixerStreamsType;}
         set {mixerStreamsType = value;}
      }

      public Type ValveStreamsType
      {
         get {return valveStreamsType;}
         set {valveStreamsType = value;}
      }

      public EvaporationAndDryingSystemPreferences() : base()
      {
         this.unitOpCreationType = UnitOpCreationType.WithInputAndOutput;
         //this.dryerMaterialInletType = MaterialStateType.Solid;
         this.heatExchangerColdInletType = typeof(DryingMaterialStream);
         this.heatExchangerHotInletType = typeof(DryingMaterialStream);
         this.mixerStreamsType = typeof(DryingGasStream);
         this.valveStreamsType = typeof(DryingMaterialStream);
         this.teeStreamsType = typeof(DryingGasStream);
      }

      //persistence
      protected EvaporationAndDryingSystemPreferences (SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionEvaporationAndDryingSystemPreferences", typeof(int));
         if (persistedClassVersion == 1) {
            this.unitOpCreationType = (UnitOpCreationType) info.GetValue("UnitOpCreationType", typeof(UnitOpCreationType));
            //this.dryerMaterialInletType = (MaterialStateType) info.GetValue("DryerMaterialInletType", typeof(MaterialStateType));
            this.heatExchangerColdInletType = (Type) info.GetValue("HeatExchangerColdInletType", typeof(Type));
            this.heatExchangerHotInletType = (Type) info.GetValue("HeatExchangerHotInletType", typeof(Type));
            this.teeStreamsType = (Type) info.GetValue("TeeStreamsType", typeof(Type));
            this.mixerStreamsType = (Type) info.GetValue("MixerStreamsType", typeof(Type));
            this.valveStreamsType = (Type) info.GetValue("ValveStreamsType", typeof(Type));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionEvaporationAndDryingSystemPreferences", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("UnitOpCreationType", this.unitOpCreationType, typeof(UnitOpCreationType));
         //info.AddValue("DryerMaterialInletType", this.dryerMaterialInletType, typeof(MaterialStateType));
         info.AddValue("HeatExchangerColdInletType", this.heatExchangerColdInletType, typeof(Type));
         info.AddValue("HeatExchangerHotInletType", this.heatExchangerHotInletType, typeof(Type));
         info.AddValue("TeeStreamsType", this.teeStreamsType, typeof(Type));
         info.AddValue("MixerStreamsType", this.mixerStreamsType, typeof(Type));
         info.AddValue("ValveStreamsType", this.valveStreamsType, typeof(Type));
      }
   }
}
