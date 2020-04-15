using System;
using System.Collections;
using System.Collections.Generic;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {

   public delegate void FossilFuelTypeChangedEventHandler(FossilFuelType newType, FossilFuelType oldType);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class FossilFuelCache : MaterialCache {

      private FossilFuel fossilFuel;
      FossilFuelType fossilFuelType;
      private FossilFuelCatalog catalog;

      public event MaterialComponentsChangedEventHandler FossilFuelComponentsChanged;
      public event FossilFuelTypeChangedEventHandler FossilFuelTypeChanged;

      public FossilFuelCache(FossilFuelCatalog catalog)
         : base() {
         this.fossilFuelType = FossilFuelType.Generic;
         this.catalog = catalog;
      }

      public FossilFuelCache(FossilFuel fossilFuel, FossilFuelCatalog catalog)
         : base() {
         this.fossilFuel = fossilFuel;
         this.name = fossilFuel.Name;
         this.fossilFuelType = fossilFuel.FossilFuelType;
         this.fossilFuel = fossilFuel;
         this.catalog = catalog;
      }

      public FossilFuelCache(FossilFuel fossilFuel)
         : base() {
         this.fossilFuel = fossilFuel;
         this.name = fossilFuel.Name;
         this.fossilFuelType = fossilFuel.FossilFuelType;
      }


      public void SetFossilFuleType(FossilFuelType type) {
         if (type == fossilFuelType) {
            return;
         }

         FossilFuelType oldValue = fossilFuelType;
         if (type == FossilFuelType.Generic) {
            SwitchToGenericFuel();
         }
         else {
            SwitchFromGenericFuel();
         }

         fossilFuelType = type;
         OnFossilFuelTypeChanged(fossilFuelType, oldValue);
      }
         

      public ErrorMessage FinishSpecifications(string fuelName, bool isNew) {
         ErrorMessage errorMsg = null;
         if (materialComponentList.Count < 1) {
            string msg = "The specified fuel contains no substance.";
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", msg);
         }
         DoNormalization();

         if (isNew) {
            if (catalog.IsInCatalog(fuelName)) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the fuel catalog a fuel called " + fuelName);
            }
            else {
               fossilFuel = new FossilFuel(fuelName, materialComponentList, fossilFuelType, true);
               errorMsg = catalog.AddFossilFule(fossilFuel);
            }
         }
         else {
            if (!fuelName.Equals(fossilFuel.Name) && catalog.GetFossilFuel(fuelName) != null) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the fuel  catalog a fuel called " + fuelName);
            }
            else {
               try {
                  fossilFuel.Update(fuelName, materialComponentList);
                  errorMsg = catalog.UpdateFossilFuel(fossilFuel);
               }
               catch (Exception) {
               }
            }
         }

         return errorMsg;
      }

      private void SwitchToGenericFuel() {
         materialComponentList.Clear();

         IList<Substance> genericFuelSubstanceList = GetGenericFuelSubstanceList();

         foreach (Substance s in genericFuelSubstanceList) {
            materialComponentList.Add(new MaterialComponent(s));
         }
         OnMaterialComponentsChanged();
      }

      private void SwitchFromGenericFuel() {
         materialComponentList.Clear();
         OnMaterialComponentsChanged();
      }


      protected override void OnMaterialComponentsChanged() {
         if (FossilFuelComponentsChanged != null) {
            FossilFuelComponentsChanged(materialComponentList);
         }
      }

      private void OnFossilFuelTypeChanged(FossilFuelType newType, FossilFuelType oldType) {
         if (FossilFuelTypeChanged != null) {
            FossilFuelTypeChanged(newType, oldType);
         }
      }

      private IList<Substance> GetGenericFuelSubstanceList() {
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         IList<Substance> substanceList = new List<Substance>();
         substanceList.Add(SubstanceCatalog.GetSubstance(Substance.CARBON));
         substanceList.Add(SubstanceCatalog.GetSubstance(Substance.HYDROGEN));
         substanceList.Add(SubstanceCatalog.GetSubstance(Substance.OXYGEN));
         substanceList.Add(SubstanceCatalog.GetSubstance(Substance.SULFUR));

         return substanceList;
      }

   }
}
