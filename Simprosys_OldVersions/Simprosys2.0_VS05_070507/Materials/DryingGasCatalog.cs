using System;
using System.Collections;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   public delegate void DryingGasAddedEventHandler(DryingGas gas);
   public delegate void DryingGasDeletedEventHandler(string name);
   public delegate void DryingGasChangedEventHandler(DryingGas gas);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class DryingGasCatalog {
      private IList gasList;
      private static DryingGasCatalog self;

      public event DryingGasAddedEventHandler DryingGasAdded;
      public event DryingGasDeletedEventHandler DryingGasDeleted;
      public event DryingGasChangedEventHandler DryingGasChanged;

      public bool IsEmpty {
         get { return gasList.Count <= 0; }
      }

      public static DryingGasCatalog GetInstance() {
         if (self == null) {
            self = new DryingGasCatalog();
         }
         return self;
      }

      public DryingGasCatalog() {
         gasList = new ArrayList();
         InitializeCatalog();
      }

      public DryingGasCatalog(IList list) {
         gasList = list;
      }

      private void InitializeCatalog() {
         //SubstanceLibraryService sls = SubstanceLibraryService.GetInstance();
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         DryingGas dg = new DryingGas("Air", sc.GetSubstance("Air"), false);
         gasList.Add(dg);
      }

      public DryingGas GetDefaultDryingGas() {
         return GetDryingGas("Air");
      }

      public void AddDryingGas(DryingGas gas) {
         if (!IsInCatalog(gas)) {
            gasList.Add(gas);
            OnDryingGasAdded(gas);
         }
      }

      public void RemoveDryingGas(string name) {
         foreach (DryingGas gas in gasList) {
            if (gas.Name.Equals(name) && gas.IsUserDefined) {
               gasList.Remove(gas);
               OnDryingGasDeleted(name);
            }
         }
      }

      public void RemoveDryingGas(DryingGas gas) {
         if (gas.IsUserDefined) {
            string name = gas.Name;
            gasList.Remove(gas);
            OnDryingGasDeleted(name);
         }
      }

      //      public void ChangedryingGas(DryingGas gas) 
      //      {
      //         if (gas.IsUserDefined) 
      //         {
      //            OnDryingGasChanged(gas);
      //         }
      //      }
      //      
      public bool IsInCatalog(DryingGas gas) {
         bool isInCatalog = false;
         foreach (DryingGas dm in gasList) {
            if (dm.Name.Equals(gas.Name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public bool IsInCatalog(string name) {
         bool isInCatalog = false;
         foreach (DryingGas dm in gasList) {
            if (dm.Name.Equals(name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      //      public void Remove(int index) {
      //         if (index < gasList.Count && index >= 0) {
      //            DryingGas gas = (DryingGas) gasList[index];
      //            if (gas.IsUserDefined) {
      //               string name = gas.Name;
      //               gasList.RemoveAt(index);
      //               OnDryingGasDeleted(name);
      //            }
      //         }
      //      }
      //
      public DryingGas GetDryingGas(string name) {
         DryingGas ret = null;
         foreach (DryingGas gas in gasList) {
            if (gas.Name.Equals(name)) {
               ret = gas;
               break;
            }
         }
         return ret;
      }

      public IList GetDryingGasList() {
         return gasList;
      }

      public IList GetDryingGasList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (DryingGas gas in gasList) {
            if (gas.IsUserDefined == isUserDefined) {
               retList.Add(gas);
            }
         }

         return retList;
      }

      public string GetUniqueGasName(string namePrefix) {
         int newIndex = 0;
         int index;
         DryingGas gas;
         string gasName;
         string suffix;
         for (int i = 0; i < gasList.Count; i++) {
            gas = (DryingGas)gasList[i];
            gasName = gas.Name;
            if (!gasName.StartsWith(namePrefix)) {
               continue;
            }

            index = namePrefix.Length;
            suffix = gasName.Substring(index);
            char[] chars = null;
            if (suffix != null) {
               chars = suffix.ToCharArray();
            }
            bool suffixIsANumber = false;
            if (chars != null && chars.Length > 0) {
               suffixIsANumber = true;
               foreach (char c in chars) {
                  if (!char.IsDigit(c)) {
                     suffixIsANumber = false;
                     break;
                  }
               }
            }
            if (suffixIsANumber == true) {
               try {
                  index = Int32.Parse(suffix);
                  if (index > newIndex) {
                     newIndex = index;
                  }
               }
               catch (FormatException e) {
                  Console.WriteLine(e.Message);
               }
            }
         }
         ++newIndex;

         return namePrefix + "__" + newIndex;
      }

      private void OnDryingGasAdded(DryingGas gas) {
         if (DryingGasAdded != null)
            DryingGasAdded(gas);
      }

      private void OnDryingGasDeleted(string name) {
         if (DryingGasDeleted != null)
            DryingGasDeleted(name);
      }

      private void OnDryingGasChanged(DryingGas gas) {
         if (DryingGasChanged != null)
            DryingGasChanged(gas);
      }
   }
}
