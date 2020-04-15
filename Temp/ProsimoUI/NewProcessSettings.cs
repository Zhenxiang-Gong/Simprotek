using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for NewProcessSettings.
	/// </summary>
   [Serializable]
   public class NewProcessSettings : ISerializable
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;

      private StreamingContext streamingContext;
      public StreamingContext StreamingContext
      {
         get {return streamingContext;}
         set {streamingContext = value;}
      }

      private SerializationInfo serializationInfo;
      public SerializationInfo SerializationInfo
      {
         get {return serializationInfo;}
         set {serializationInfo = value;}
      }

      private string dryingGasName;
      public string DryingGasName
      {
         get {return dryingGasName;}
         set {dryingGasName = value;}
      }

      private string dryingMaterialName;
      public string DryingMaterialName
      {
         get {return dryingMaterialName;}
         set {dryingMaterialName = value;}
      }

		public NewProcessSettings()
		{
         this.dryingGasName = "Air";
      }

      protected NewProcessSettings(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionNewProcessSettings", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.DryingGasName = (string)info.GetValue("DryingGasName", typeof(string));
               this.DryingMaterialName = (string)info.GetValue("DryingMaterialName", typeof(string));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionNewProcessSettings", NewProcessSettings.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("DryingGasName", this.DryingGasName, typeof(string));
         info.AddValue("DryingMaterialName", this.DryingMaterialName, typeof(string));
      }
	}
}
