using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for FlowsheetPreferences.
	/// </summary>
   [Serializable]
   public class FlowsheetPreferences : ISerializable
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

      private string currentUnitSystemName;
      public string CurrentUnitSystemName
      {
         get {return currentUnitSystemName;}
         set {currentUnitSystemName = value;}
      }

      private NumericFormat numericFormat;
      public NumericFormat NumericFormat
      {
         get {return numericFormat;}
         set {numericFormat = value;}
      }

      private string decimalPlaces;
      public string DecimalPlaces
      {
         get {return decimalPlaces;}
         set {decimalPlaces = value;}
      }

      public FlowsheetPreferences(Flowsheet flowsheet)
		{
         this.currentUnitSystemName = flowsheet.CurrentUnitSystemName;
         this.numericFormat = flowsheet.NumericFormat;
         this.decimalPlaces = flowsheet.DecimalPlaces;
		}

      protected FlowsheetPreferences(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlowsheetPreferences", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.CurrentUnitSystemName = (string)info.GetValue("CurrentUnitSystemName", typeof(string));
               this.NumericFormat = (NumericFormat)info.GetValue("NumericFormat", typeof(NumericFormat));
               this.DecimalPlaces = (string)info.GetValue("DecimalPlaces", typeof(string));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionFlowsheetPreferences", FlowsheetPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CurrentUnitSystemName", this.CurrentUnitSystemName, typeof(string));
         info.AddValue("NumericFormat", this.NumericFormat, typeof(NumericFormat));
         info.AddValue("DecimalPlaces", this.DecimalPlaces, typeof(string));
      }
	}
}
