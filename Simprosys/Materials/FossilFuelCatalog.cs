using System;
using System.Collections;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   public delegate void FossilFuelAddedEventHandler(FossilFuel fuel);
   public delegate void FossilFuelDeletedEventHandler(string name);
   public delegate void FossilFuelChangedEventHandler(object sender, FossilFuelChangedEventArgs eventArgs);
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class FossilFuelCatalog {
      private ArrayList fossilFuelList;
      public static FossilFuelCatalog Instance = new FossilFuelCatalog();

      public event FossilFuelAddedEventHandler FossilFuelAdded;
      public event FossilFuelDeletedEventHandler FossilFuelDeleted;
      public event FossilFuelChangedEventHandler FossilFuelChanged;

      public bool IsEmpty {
         get { return fossilFuelList.Count <= 0; }
      }

      private FossilFuelCatalog() {
         fossilFuelList = new ArrayList();
         InitializeCatalog();
      }

      private void InitializeCatalog() {
         CreateDefaultFossilFuelList();
      }

      public ErrorMessage AddFossilFule(FossilFuel fuel) {
         ErrorMessage retMsg = null;
         if (!IsInCatalog(fuel)) {
            fossilFuelList.Add(fuel);
            OnFossilFuelAdded(fuel);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the fuel catalog a fuel called " + fuel.Name);
         }
         return retMsg;
      }

      public ErrorMessage UpdateFossilFuel(FossilFuel fuel) {
         ErrorMessage retMsg = null;
         if (IsInCatalog(fuel)) {
            OnFossilFuelChanged(fuel);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the material catalog a material called " + fuel.Name);
         }
         
         return retMsg;
      }

      public void RemoveFossilFuel(string name) {
         foreach (FossilFuel fuel in fossilFuelList) {
            if (fuel.Name.Equals(name) && fuel.IsUserDefined) {
               fossilFuelList.Remove(fuel);
               OnFossilFuelDeleted(name);
               break;
            }
         }
      }

      public bool IsInCatalog(FossilFuel fuel) {
         bool isInCatalog = false;
         foreach (FossilFuel f in fossilFuelList) {
            if (f.Name.Equals(fuel.Name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public bool IsInCatalog(string name) {
         bool isInCatalog = false;
         foreach (FossilFuel f in fossilFuelList) {
            if (f.Name.Equals(name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public void RemoveFossilFuel(FossilFuel fuel) {
         if (fuel.IsUserDefined) {
            string name = fuel.Name;
            fossilFuelList.Remove(fuel);
            OnFossilFuelDeleted(name);
         }
      }

      public FossilFuel GetFossilFuel(string name) {
         FossilFuel ret = null;
         foreach (FossilFuel fuel in fossilFuelList)
         {
            if (fuel.Name.Equals(name)) {
               ret = fuel;
               break;
            }
         }
         return ret;
      }

      public ArrayList GetFossilFuelList() {
         return fossilFuelList;
      }

      private void CreateDefaultFossilFuelList()
      {
         if (!IsInCatalog(FossilFuel.NATURAL_GAS))
         {
            fossilFuelList.Add(GetDefaultFossilFuel());
         }
      }

      public object[] GetFossilFuelArray()
      {
         FossilFuelCatalog catalog = FossilFuelCatalog.Instance;
         return catalog.GetFossilFuelList().ToArray(typeof(object)) as object[];
      }

      public IList GetFossilFuelList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (FossilFuel fuel in fossilFuelList) {
            if (fuel.IsUserDefined == isUserDefined) {
               retList.Add(fuel);
            }
         }

         return retList;
      }

      public IList GetFossilFuelList(FossilFuelType type) {
         ArrayList retList = new ArrayList();
         foreach (FossilFuel fuel in fossilFuelList) {
            if (fuel.FossilFuelType == type) {
               retList.Add(fuel);
            }
         }

         return retList;
      }

      public IList GetFossilFuelList(bool isUserDefined, FossilFuelType type) {
         ArrayList retList = new ArrayList();
         foreach (FossilFuel fuel in fossilFuelList) {
            if (fuel.IsUserDefined == isUserDefined && fuel.FossilFuelType == type) {
               retList.Add(fuel);
            }
         }

         return retList;
      }

      public string GetUniqueFossilFuleName(string namePrefix) {
         int newIndex = 0;
         int index;
         FossilFuel fuel;
         string fuelName;
         string suffix;
         for (int i = 0; i < fossilFuelList.Count; i++) {
            fuel = (FossilFuel)fossilFuelList[i];
            fuelName = fuel.Name;
            if (!fuelName.StartsWith(namePrefix)) {
               continue;
            }

            index = namePrefix.Length;
            suffix = fuelName.Substring(index);
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

      private void OnFossilFuelAdded(FossilFuel fuel) {
         if (FossilFuelAdded != null)
            FossilFuelAdded(fuel);
      }

      private void OnFossilFuelDeleted(string name) {
         if (FossilFuelDeleted != null)
            FossilFuelDeleted(name);
      }

      private void OnFossilFuelChanged(FossilFuel fuel) {
         if (FossilFuelChanged != null) {
            FossilFuelChanged(this, new FossilFuelChangedEventArgs(fuel, false));
         }
      }

      private FossilFuel GetDefaultFossilFuel() {
         SubstanceCatalog catalog = SubstanceCatalog.GetInstance();
         Substance methan = catalog.GetSubstance(Substance.METHANE);
         Substance ethane = catalog.GetSubstance(Substance.ETHANE);
         Substance propane = catalog.GetSubstance(Substance.PROPANE);
         Substance butane = catalog.GetSubstance(Substance.BUTANE);
         Substance carbonDioxide = catalog.GetSubstance(Substance.CARBON_DIOXIDE);
         Substance oxygen = catalog.GetSubstance(Substance.OXYGEN);
         Substance nitrogen = catalog.GetSubstance(Substance.NITROGEN);
         Substance hydrogenSulfide = catalog.GetSubstance(Substance.HYDROGEN_SULFIDE);

         ArrayList compList = new ArrayList();
         //compList.Add(new MaterialComponent(methan, 1.0));
         compList.Add(new MaterialComponent(methan, 0.8));
         compList.Add(new MaterialComponent(ethane, 0.108));
         compList.Add(new MaterialComponent(propane, 0.02));
         compList.Add(new MaterialComponent(butane, 0.016));
         compList.Add(new MaterialComponent(carbonDioxide, 0.04));
         compList.Add(new MaterialComponent(oxygen, 0.002));
         compList.Add(new MaterialComponent(nitrogen, 0.025));
         compList.Add(new MaterialComponent(hydrogenSulfide, 0.025));

         return new FossilFuel(FossilFuel.NATURAL_GAS, compList, FossilFuelType.Detailed, false);
      }
   }

   public class FossilFuelChangedEventArgs : EventArgs {
      private FossilFuel fossilFuel;
      private bool isNameChangeOnly;

      public FossilFuel FossilFuel {
         get { return fossilFuel; }
      }

      public bool IsNameChangeOnly {
         get { return isNameChangeOnly; }
      }

      public FossilFuelChangedEventArgs(FossilFuel fossilFuel, bool isNameChangeOnly) {
         this.fossilFuel = fossilFuel;
         this.isNameChangeOnly = isNameChangeOnly;
      }
   }

}
