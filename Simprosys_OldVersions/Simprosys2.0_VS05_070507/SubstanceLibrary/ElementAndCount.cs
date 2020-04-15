using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.SubstanceLibrary {

   [Serializable]
   public class ElementAndCount : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      private string elementName;
      private int elementCount;

      public string ElementName {
         get { return elementName; }
      }

      public int ElementCount {
         get { return elementCount; }
      }

      public ElementAndCount(string elementName, int elementCount) {
         this.elementName = elementName;
         this.elementCount = elementCount;
      }

      protected ElementAndCount(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionElementAndCount");
         if (persistedClassVersion == 1) {
            this.elementName = info.GetString("ElementName");
            this.elementCount = info.GetInt32("ElementCount");
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionElementAndCount", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("ElementName", this.elementName, typeof(string));
         info.AddValue("ElementCount", this.elementCount, typeof(int));
      }
   }
}
