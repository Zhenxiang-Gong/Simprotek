using System;
using System.Collections;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials {
   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class DryingMaterialCatalog {
      private IList materialList;
      public static DryingMaterialCatalog Instance = new DryingMaterialCatalog();

      public event DryingMaterialAddedEventHandler DryingMaterialAdded;
      public event DryingMaterialDeletedEventHandler DryingMaterialDeleted;
      public event DryingMaterialChangedEventHandler DryingMaterialChanged;

      public bool IsEmpty {
         get { return materialList.Count <= 0; }
      }

      private DryingMaterialCatalog() {
         materialList = new ArrayList();
         InitializeCatalog();
      }

      //public DryingMaterialCatalog(IList list) {
      //   materialList = list;
      //}

      private void InitializeCatalog() {
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         //Substance absoluteDryMaterial = sc.GetSubstance("Dry Material");
         Substance absoluteDryMaterial = sc.GetGenericSubstance();
         Substance moisture = sc.GetSubstance(Substance.WATER);
         ArrayList dryMatComponents = new ArrayList();
         dryMatComponents.Add(new MaterialComponent(absoluteDryMaterial));
         CompositeSubstance dryMat = new CompositeSubstance("Generic Dry Material", dryMatComponents);
         if (!IsInCatalog("Generic Drying Material")) {
            DryingMaterial dm = new DryingMaterial("Generic Drying Material", MaterialType.GenericMaterial, dryMat, moisture, false);
            materialList.Add(dm);
         }

         //DryingMaterial milk = (DryingMaterial)dm.Clone();
         //milk.Name = "Milk";
         //milk.MaterialType = MaterialType.SpecialFood;
         //materialList.Add(milk);
      }

      public DryingMaterial GetDefaultDryingMaterial() {
         return GetDryingMaterial("Generic Material");
      }

      public ErrorMessage AddDryingMaterial(DryingMaterial material) {
         ErrorMessage retMsg = null;
         if (!IsInCatalog(material)) {
            materialList.Add(material);
            OnDryingMaterialAdded(material);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the material catalog a material called " + material.Name);
         }
         return retMsg;
      }

      public ErrorMessage UpdateDryingMaterial(DryingMaterial material) {
         ErrorMessage retMsg = null;
         if (IsInCatalog(material)) {
            OnDryingMaterialChanged(material);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the material catalog a material called " + material.Name);
         }
         
         return retMsg;
      }

      public void RemoveDryingMaterial(string name) {
         foreach (DryingMaterial material in materialList) {
            if (material.Name.Equals(name) && material.IsUserDefined) {
               materialList.Remove(material);
               OnDryingMaterialDeleted(name);
               break;
            }
         }
      }

      public bool IsInCatalog(DryingMaterial material) {
         bool isInCatalog = false;
         foreach (DryingMaterial dm in materialList) {
            if (dm.Name.Equals(material.Name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public bool IsInCatalog(string name) {
         bool isInCatalog = false;
         foreach (DryingMaterial dm in materialList) {
            if (dm.Name.Equals(name)) {
               isInCatalog = true;
               break;
            }
         }

         return isInCatalog;
      }

      public void RemoveDryingMaterial(DryingMaterial material) {
         if (material.IsUserDefined) {
            string name = material.Name;
            materialList.Remove(material);
            OnDryingMaterialDeleted(name);
         }
      }

      public DryingMaterial GetDryingMaterial(string name) {
         DryingMaterial ret = null;
         foreach (DryingMaterial material in materialList) {
            if (material.Name.Equals(name)) {
               ret = material;
               break;
            }
         }
         return ret;
      }

      public IList GetDryingMaterialList() {
         return materialList;
      }

      public IList GetDryingMaterialList(bool isUserDefined) {
         ArrayList retList = new ArrayList();
         foreach (DryingMaterial mat in materialList) {
            if (mat.IsUserDefined == isUserDefined) {
               retList.Add(mat);
            }
         }

         return retList;
      }

      public IList GetDryingMaterialList(MaterialType type) {
         ArrayList retList = new ArrayList();
         foreach (DryingMaterial mat in materialList) {
            if (mat.MaterialType == type) {
               retList.Add(mat);
            }
         }

         return retList;
      }

      public IList GetDryingMaterialList(bool isUserDefined, MaterialType type) {
         ArrayList retList = new ArrayList();
         foreach (DryingMaterial mat in materialList) {
            if (mat.IsUserDefined == isUserDefined && mat.MaterialType == type) {
               retList.Add(mat);
            }
         }

         return retList;
      }

      public string GetUniqueMaterialName(string namePrefix) {
         int newIndex = 0;
         int index;
         DryingMaterial material;
         string materialName;
         string suffix;
         for (int i = 0; i < materialList.Count; i++) {
            material = (DryingMaterial)materialList[i];
            materialName = material.Name;
            if (!materialName.StartsWith(namePrefix)) {
               continue;
            }

            index = namePrefix.Length;
            suffix = materialName.Substring(index);
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

      private void OnDryingMaterialAdded(DryingMaterial material) {
         if (DryingMaterialAdded != null)
            DryingMaterialAdded(material);
      }

      private void OnDryingMaterialDeleted(string name) {
         if (DryingMaterialDeleted != null)
            DryingMaterialDeleted(name);
      }

      private void OnDryingMaterialChanged(DryingMaterial material) {
         if (DryingMaterialChanged != null) {
            DryingMaterialChanged(this, new DryingMaterialChangedEventArgs(material, false));
         }
      }
   }
}
