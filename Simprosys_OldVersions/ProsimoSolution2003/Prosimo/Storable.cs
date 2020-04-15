using System;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo {
   
   public delegate void NameChangedEventHandler(Object sender, string name, string oldName);
   
   /// <summary>
   /// Summary description for Storable.
   /// Storable is a base class for any class which needs persistence
   /// </summary>
   [Serializable]
   public abstract class Storable : ISerializable {

      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      protected StreamingContext context;
      protected SerializationInfo info;
     
      protected Storable () {
      }

      //persistence constructor
      protected Storable (SerializationInfo info, StreamingContext context) {
         this.info = info;
         this.context = context;
      }

      public virtual void SetObjectData() {
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
      }

      protected Storable RecallStorableObject(string objectName, Type objectType) {
         Storable storable = info.GetValue(objectName, objectType) as Storable;
         if (storable != null) {
            storable.SetObjectData();
         }
         return storable;
      }

      protected Storable[] RecallArrayObject(string arrayName, Type objectType) {
         Storable[] array = info.GetValue(arrayName, objectType) as Storable[];
         if (array != null) {
            foreach (Storable obj in array) {
               obj.SetObjectData();
            }
         }
         return array;
      }
   
      protected ArrayList RecallArrayListObject(string arrayListName) {
         ArrayList list = info.GetValue(arrayListName, typeof(ArrayList)) as ArrayList;
         if (list != null) 
         {
            foreach (Storable obj in list) 
            {
               obj.SetObjectData();
            }
         }
         return list;
      }
   
      protected Hashtable RecallHashtableObject(string hashTableName) 
      {
         Hashtable table = info.GetValue(hashTableName, typeof(Hashtable)) as Hashtable;
         if (table != null) 
         {
            foreach (Storable obj in table.Values) 
            {
               obj.SetObjectData();
            }
         }
         return table;
      }
      
      public static Storable RecallStorableObject(SerializationInfo info, string objectName, Type objectType) {
         Storable storable = info.GetValue(objectName, objectType) as Storable;
         if (storable != null) {
            storable.SetObjectData();
         }
         return storable;
      }

   }
}