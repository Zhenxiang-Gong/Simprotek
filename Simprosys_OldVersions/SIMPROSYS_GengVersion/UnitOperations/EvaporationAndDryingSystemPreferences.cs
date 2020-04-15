using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations.ProcessStreams;

namespace Prosimo.UnitOperations {
   public enum DryingStreamType { DryingGas = 0, LiquidDryingMaterial, SolidDryingMaterial };

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
      private DryingStreamType teeInletStreamType;
      private DryingStreamType mixerInletStreamType;
      private Type valveInletStreamType;
      private Type heaterInletStreamType;
      private Type coolerInletStreamType;

      //      public MaterialStateType DryerMaterialInletType {
      //         get {return dryerMaterialInletType;}
      //         set {dryerMaterialInletType = value;}
      //      }

      public UnitOpCreationType UnitOpCreationType {
         get { return unitOpCreationType; }
         set { unitOpCreationType = value; }
      }

      public Type HeatExchangerColdInletType {
         get { return heatExchangerColdInletType; }
         set { heatExchangerColdInletType = value; }
      }

      public Type HeatExchangerHotInletType {
         get { return heatExchangerHotInletType; }
         set { heatExchangerHotInletType = value; }
      }

      public DryingStreamType TeeInletStreamType {
         get { return teeInletStreamType; }
         set { teeInletStreamType = value; }
      }

      public DryingStreamType MixerInletStreamType {
         get { return mixerInletStreamType; }
         set { mixerInletStreamType = value; }
      }

      public Type ValveInletStreamType {
         get { return valveInletStreamType; }
         set { valveInletStreamType = value; }
      }

      public Type HeaterInletStreamType {
         get { return heaterInletStreamType; }
         set { heaterInletStreamType = value; }
      }

      public Type CoolerInletStreamType {
         get { return coolerInletStreamType; }
         set { coolerInletStreamType = value; }
      }

      public EvaporationAndDryingSystemPreferences()
         : base() {
         this.unitOpCreationType = UnitOpCreationType.WithInputAndOutput;
         //this.dryerMaterialInletType = MaterialStateType.Solid;
         this.heatExchangerColdInletType = typeof(DryingMaterialStream);
         this.heatExchangerHotInletType = typeof(DryingMaterialStream);
         this.mixerInletStreamType = DryingStreamType.DryingGas;
         this.teeInletStreamType = DryingStreamType.DryingGas;
         this.valveInletStreamType = typeof(DryingMaterialStream);
         this.heaterInletStreamType = typeof(DryingGasStream);
         this.coolerInletStreamType = typeof(DryingGasStream);
      }

      //persistence
      protected EvaporationAndDryingSystemPreferences(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionEvaporationAndDryingSystemPreferences", typeof(int));
         if (persistedClassVersion == 1) {
            this.unitOpCreationType = (UnitOpCreationType)info.GetValue("UnitOpCreationType", typeof(UnitOpCreationType));
            //this.dryerMaterialInletType = (MaterialStateType) info.GetValue("DryerMaterialInletType", typeof(MaterialStateType));
            this.heatExchangerColdInletType = (Type)info.GetValue("HeatExchangerColdInletType", typeof(Type));
            this.heatExchangerHotInletType = (Type)info.GetValue("HeatExchangerHotInletType", typeof(Type));
            this.teeInletStreamType = (DryingStreamType)info.GetValue("TeeInletStreamType", typeof(DryingStreamType));
            this.mixerInletStreamType = (DryingStreamType)info.GetValue("MixerInletStreamType", typeof(DryingStreamType));
            this.valveInletStreamType = (Type)info.GetValue("ValveInletStreamType", typeof(Type));
            this.heaterInletStreamType = (Type)info.GetValue("HeaterInletStreamType", typeof(Type));
            this.coolerInletStreamType = (Type)info.GetValue("CoolerInletStreamType", typeof(Type));
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
         info.AddValue("TeeInletStreamType", this.teeInletStreamType, typeof(DryingStreamType));
         info.AddValue("MixerInletStreamType", this.mixerInletStreamType, typeof(DryingStreamType));
         info.AddValue("ValveInletStreamType", this.valveInletStreamType, typeof(Type));
         info.AddValue("HeaterInletStreamType", this.heaterInletStreamType, typeof(Type));
         info.AddValue("CoolerInletStreamType", this.coolerInletStreamType, typeof(Type));
      }
   }
}
