using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.UnitSystems;

namespace ProsimoUI {

   public enum NumericFormat { Undefined = 0, FixedPoint, Scientific }

   /// <summary>
   /// Summary description for ApplicationPreferences.
   /// </summary>
   [Serializable]
   //public class ApplicationPreferences : ISerializable, INumericFormat
   public class ApplicationPreferences : Storable, INumericFormat {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public event NumericFormatStringChangedEventHandler NumericFormatStringChanged;

      private string currentUnitSystemName;
      public string CurrentUnitSystemName {
         get { return currentUnitSystemName; }
         set { currentUnitSystemName = value; }
      }

      private string decimalPlaces;
      public string DecimalPlaces {
         get { return decimalPlaces; }
         set {
            decimalPlaces = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      private NumericFormat numericFormat;
      public NumericFormat NumericFormat {
         get { return numericFormat; }
         set {
            numericFormat = value;
            this.OnNumericFormatStringChanged(this);
         }
      }

      public string NumericFormatString {
         get {
            if (this.NumericFormat.Equals(NumericFormat.FixedPoint)) {
               return UI.FIXED_POINT + this.DecimalPlaces;
            }
            else if (this.NumericFormat.Equals(NumericFormat.Scientific)) {
               return UI.SCIENTIFIC + this.DecimalPlaces;
            }
            else
               return UI.FIXED_POINT + "2";
         }
      }

      public ApplicationPreferences() {
         this.currentUnitSystemName = "SI-2";
         this.InitializeCurrentUnitSystem();
         this.numericFormat = NumericFormat.FixedPoint;
         this.decimalPlaces = "3";
      }

      public void InitializeCurrentUnitSystem() {
         UnitSystemCatalog catalog = UnitSystemService.GetInstance().GetUnitSystemCatalog();
         UnitSystem us = catalog.Get(this.currentUnitSystemName);
         if (us != null) {
            UnitSystemService.GetInstance().CurrentUnitSystem = us;
         }
         else {
            UnitSystem us2 = catalog.Get("SI-2");
            UnitSystemService.GetInstance().CurrentUnitSystem = us2;
         }
      }

      private void OnNumericFormatStringChanged(INumericFormat iNumericFormat) {
         if (NumericFormatStringChanged != null) {
            NumericFormatStringChanged(iNumericFormat);
         }
      }

      protected ApplicationPreferences(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      public override void SetObjectData() {
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionApplicationPreferences", typeof(int));
         this.CurrentUnitSystemName = (string)info.GetValue("CurrentUnitSystemName", typeof(string));
         this.NumericFormat = (NumericFormat)info.GetValue("NumericFormat", typeof(NumericFormat));
         this.DecimalPlaces = (string)info.GetValue("DecimalPlaces", typeof(string));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         info.AddValue("ClassPersistenceVersionApplicationPreferences", ApplicationPreferences.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CurrentUnitSystemName", this.CurrentUnitSystemName, typeof(string));
         info.AddValue("NumericFormat", this.NumericFormat, typeof(NumericFormat));
         info.AddValue("DecimalPlaces", this.DecimalPlaces, typeof(string));
      }
   }
}
//private StreamingContext streamingContext;
//public StreamingContext StreamingContext
//{
//   get { return streamingContext; }
//   set { streamingContext = value; }
//}

//private SerializationInfo serializationInfo;
//public SerializationInfo SerializationInfo
//{
//   get { return serializationInfo; }
//   set { serializationInfo = value; }
//}


