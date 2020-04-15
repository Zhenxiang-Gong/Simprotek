using System;
using System.Collections;

using Prosimo.SubstanceLibrary;

namespace Prosimo.Materials
{
   public delegate void DryingMaterialAddedEventHandler(DryingMaterial material);
   public delegate void DryingMaterialDeletedEventHandler(string name);
   public delegate void DryingMaterialChangedEventHandler(DryingMaterial material);

   /// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class DryingMaterialCatalog  {
      private IList materialList;
      private static DryingMaterialCatalog self;

      public event DryingMaterialAddedEventHandler DryingMaterialAdded;
      public event DryingMaterialDeletedEventHandler DryingMaterialDeleted;
      public event DryingMaterialChangedEventHandler DryingMaterialChanged;
      
      public static DryingMaterialCatalog GetInstance() {
         if (self == null) {
            self = new DryingMaterialCatalog ();
         }
         return self;
      }
      
      public DryingMaterialCatalog() {
         materialList = new ArrayList();
         InitializeCatalog();
      }
      
      public DryingMaterialCatalog(IList list) {
         materialList = list;
      }

      private void InitializeCatalog() {
         //SubstanceLibraryService sls = SubstanceLibraryService.GetInstance();
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         Substance absoluteDryMaterial = sc.GetSubstance("Dry Material");
         Substance moisture = sc.GetSubstance("Water");
         ArrayList dryMatComponents = new ArrayList();
         //ArrayList components = new ArrayList();
         dryMatComponents.Add(new MaterialComponent(absoluteDryMaterial));
         CompositeSubstance dryMat = new CompositeSubstance("Generic Dry Material", dryMatComponents);
         //components.Add(dryMat);
         //components.Add(moisture);
         DryingMaterial dm = new DryingMaterial("Generic Material", MaterialType.Unknown, dryMat, moisture, false);
         materialList.Add(dm);
         DryingMaterial milk = (DryingMaterial) dm.Clone();
         milk.Name = "Milk";
         milk.MaterialType = MaterialType.SpecialFood;
         materialList.Add(milk);
      }

      public DryingMaterial GetDefaultDryingMaterial() {
         return GetDryingMaterial("Generic Material");
      }
      
      public void AddDryingMaterial(DryingMaterial material) {
         if (!IsInCatalog(material)) {                               
            materialList.Add(material);
            OnDryingMaterialAdded(material);
         }
      }

      public void UpdateDryingMaterial(DryingMaterial material) 
      {
         if (IsInCatalog(material)) 
         {                               
            OnDryingMaterialChanged(material);
         }
      }
      
      public void RemoveDryingMaterial(string name) 
      {
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

      private void OnDryingMaterialAdded(DryingMaterial material) {
         if (DryingMaterialAdded != null)
            DryingMaterialAdded(material);
      }

      private void OnDryingMaterialDeleted(string name) {
         if (DryingMaterialDeleted != null)
            DryingMaterialDeleted(name);
      }

      private void OnDryingMaterialChanged(DryingMaterial material) {
         if (DryingMaterialChanged != null)
            DryingMaterialChanged(material);
      }
   }
}
