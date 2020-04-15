using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for MainNumericFormat.
	/// </summary>
   [Serializable]
   public class MainNumericFormat: INumericFormat
	{
      public event NumericFormatStringChangedEventHandler NumericFormatStringChanged;

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

      private string decimalPlaces;
      public string DecimalPlaces
      {
         get {return decimalPlaces;}
         set
         {
            decimalPlaces = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      private NumericFormat numericFormat;
      public NumericFormat NumericFormat
      {
         get {return numericFormat;}
         set
         {
            numericFormat = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      public string NumericFormatString
      {
         get
         {
            if (this.NumericFormat.Equals(NumericFormat.FixedPoint))
            {
               return UI.FIXED_POINT + this.DecimalPlaces;
            }
            else if (this.NumericFormat.Equals(NumericFormat.Scientific))
            {
               return UI.SCIENTIFIC + this.DecimalPlaces;
            }
            else
               return UI.FIXED_POINT + "2";
         }
      }

      public MainNumericFormat()
      {
         this.NumericFormat = NumericFormat.FixedPoint;
         this.DecimalPlaces = "2";
      }

      private void OnNumericFormatStringChanged(INumericFormat iNumericFormat) 
      {
         if (NumericFormatStringChanged != null) 
         {
            NumericFormatStringChanged(iNumericFormat);
         }
      }

      protected MainNumericFormat(SerializationInfo info, StreamingContext context)
      {
         this.SerializationInfo = info;
         this.StreamingContext = context;
         // don't restore anything here!
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMainNumericFormat", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               this.decimalPlaces = (string)info.GetValue("DecimalPlaces", typeof(string));
               this.numericFormat = (NumericFormat)info.GetValue("NumericFormat", typeof(NumericFormat));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue("ClassPersistenceVersionMainNumericFormat", MainNumericFormat.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("DecimalPlaces", this.decimalPlaces, typeof(string));
         info.AddValue("NumericFormat", this.numericFormat, typeof(NumericFormat));
      }
	}
}
