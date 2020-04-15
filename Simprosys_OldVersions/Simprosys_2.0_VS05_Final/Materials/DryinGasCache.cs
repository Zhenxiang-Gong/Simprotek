using System;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class DryingGasCache : MaterialCache {

      private DryingGasCatalog catalog;

      public event MaterialComponentsChangedEventHandler DryGasComponentsChanged;

      public DryingGasCache(DryingGasCatalog catalog)
         : base() {
         this.catalog = catalog;
      }

      public DryingGasCache(DryingGas dryingGas)
         : base() {
         this.name = dryingGas.Name;
         this.materialComponentList = ((CompositeSubstance)dryingGas.Substance).Components;
      }

      public ErrorMessage FinishSpecifications(string gasName) {
         DoNormalization();
         Substance s = new CompositeSubstance(gasName, materialComponentList);

         DryingGas dg = new DryingGas(gasName, s, true);
         catalog.AddDryingGas(dg);
         return null;
      }

      protected override void OnMaterialComponentsChanged() {
         if (DryGasComponentsChanged != null) {
            DryGasComponentsChanged(materialComponentList);
         }
      }
   }
}
