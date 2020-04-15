using System;

namespace Prosimo.Materials {
   public delegate void DryingMaterialAddedEventHandler(DryingMaterial material);
   public delegate void DryingMaterialDeletedEventHandler(string name);
   public delegate void DryingMaterialChangedEventHandler(object sender, DryingMaterialChangedEventArgs eventArgs);

   public class DryingMaterialChangedEventArgs : EventArgs {
      private DryingMaterial dryingMaterial;
      private bool isNameChangeOnly;

      public DryingMaterial DryingMaterial {
         get { return dryingMaterial; }
      }

      public bool IsNameChangeOnly {
         get { return isNameChangeOnly; }
      }

      public DryingMaterialChangedEventArgs(DryingMaterial dryingMaterial, bool isNameChangeOnly) {
         this.dryingMaterial = dryingMaterial;
         this.isNameChangeOnly = isNameChangeOnly;
      }
   }
}
