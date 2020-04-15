using System;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {
   public enum CacheType {New = 0, Edit, Details};

   public delegate void MaterialComponentsChangedEventHandler(ArrayList materialComponentList);
   public delegate void MaterialTypeChangedEventHandler(MaterialType newType, MaterialType oldType);
   public delegate void MoistureSubstanceChangedEventHandler(Substance newSubstance, Substance oldSubstance);

   /// <summary>
   /// Summary description for Class1.
   /// </summary>
   public class DryingMaterialCache : MaterialCache {
      
      private DryingMaterial dryingMaterial;
      private DryingMaterialCatalog catalog;
      private Substance moistureSubstance;
      private MaterialType materialType;
      private CurveF[] duhringLines;

      //private CacheType cacheType;
      
      public event MaterialComponentsChangedEventHandler AbsoluteDryMaterialComponentsChanged;
      public event MoistureSubstanceChangedEventHandler MoistureSubstanceChanged;
      public event MaterialTypeChangedEventHandler MaterialTypeChanged;

      public Substance MoistureSubstance {
         get {return moistureSubstance;}
         set {
            if (value != moistureSubstance && (value.Name.Equals("Water") || materialType != MaterialType.GenericFood)) {
               Substance oldValue = moistureSubstance;
               moistureSubstance = value;
               OnMoistureSubstanceChanged(moistureSubstance, oldValue);
            }
         }
      }

//      public CacheType CacheType {
//         get {return cacheType;}
//      }
      
      public MaterialType MaterialType {
         get {return materialType;}
         set {
            if (value != materialType) {
               MaterialType oldValue = materialType;
               if (value == MaterialType.GenericFood) {
                  SwitchToGenericFood();
               }
               else if (materialType == MaterialType.GenericFood) {
                  SwitchFromGenericFood();
               }

               materialType = value;
               OnMaterialTypeChanged(materialType, oldValue);
            }
         }
      }

      internal CurveF[] DuhringLines {
         get {return duhringLines;}
         set {duhringLines = value;}
      }


      public DryingMaterialCache(DryingMaterialCatalog catalog) : base() {
         this.materialType = MaterialType.Unknown;
         this.catalog = catalog;
         //cacheType = CacheType.New;
      }
      
      public DryingMaterialCache(DryingMaterial dryingMaterial, DryingMaterialCatalog catalog) : base () {
         this.dryingMaterial = dryingMaterial;
         this.name = dryingMaterial.Name;
         this.materialType = dryingMaterial.MaterialType;
         this.materialComponentList = ((CompositeSubstance)dryingMaterial.AbsoluteDryMaterial).Components;
         this.moistureSubstance = dryingMaterial.Moisture;
         this.duhringLines = dryingMaterial.DuhringLines;
         this.catalog = catalog;
         //cacheType = CacheType.Edit;
      }

      public DryingMaterialCache(DryingMaterial dryingMaterial) : base () {
         this.dryingMaterial = dryingMaterial;
         this.name = dryingMaterial.Name;
         this.materialType = dryingMaterial.MaterialType;
         this.materialComponentList = ((CompositeSubstance)dryingMaterial.AbsoluteDryMaterial).Components;
         this.moistureSubstance = dryingMaterial.Moisture;
         this.duhringLines = dryingMaterial.DuhringLines;
         //cacheType = CacheType.Details;
      }
      
//      public DryingMaterialComponentsCacheModel(string name, MaterialType materialType, ArrayList materialComponentList, Substance moistureSubstance) : base (name, materialComponentList) {
//         this.moistureSubstance = moistureSubstance;
//      }
      
      public DuhringLinesCache GetDuhringLinesCache() {
         return new DuhringLinesCache(this);
      }

      public ErrorMessage FinishSpecifications(string materialName, bool isNew) {
         ErrorMessage errorMsg = null;
         if (materialComponentList.Count < 1) {
            string msg = "The specified drying material contains no substance.";
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", msg); 
         }
         DoNormalization();
         CompositeSubstance dryMat = new CompositeSubstance(materialName, materialComponentList);
         //ArrayList dryMaterialCompList = new ArrayList();
         //dryMaterialCompList.Add(s);
         //dryMaterialCompList.Add(moistureSubstance);

         if (isNew) 
         {
            dryingMaterial = new DryingMaterial(materialName, materialType, dryMat, moistureSubstance, true);
            dryingMaterial.DuhringLines = duhringLines;
            catalog.AddDryingMaterial(dryingMaterial);
         }
         else 
         {
            dryingMaterial.Name = materialName;
            dryingMaterial.MaterialType = materialType;
            dryingMaterial.AbsoluteDryMaterial = dryMat;
            dryingMaterial.Moisture = moistureSubstance;
            dryingMaterial.DuhringLines = duhringLines;
            catalog.UpdateDryingMaterial(dryingMaterial);
         }

         return errorMsg;
      }
      
      private void SwitchToGenericFood() {
         RemoveAllMaterialComponents();
         
         SubstanceCatalog substanceCatalog = SubstanceCatalog.GetInstance();
         materialComponentList.Add(new MaterialComponent(substanceCatalog.GetSubstance("Carbohydrate")));
         materialComponentList.Add(new MaterialComponent(substanceCatalog.GetSubstance("Ash")));
         materialComponentList.Add(new MaterialComponent(substanceCatalog.GetSubstance("Fiber")));
         materialComponentList.Add(new MaterialComponent(substanceCatalog.GetSubstance("Fat")));
         materialComponentList.Add(new MaterialComponent(substanceCatalog.GetSubstance("Protein")));
         OnMaterialComponentsChanged();
         MoistureSubstance = substanceCatalog.GetSubstance("Water");
      }

      private void SwitchFromGenericFood() {
         RemoveAllMaterialComponents();
         OnMaterialComponentsChanged();
      }


      protected override void OnMaterialComponentsChanged() {
         if (AbsoluteDryMaterialComponentsChanged != null) {
            AbsoluteDryMaterialComponentsChanged(materialComponentList);
         }
      }

      private void OnMoistureSubstanceChanged(Substance newValue, Substance oldValue) {
         if (MoistureSubstanceChanged != null) {
            MoistureSubstanceChanged(newValue, oldValue);
         }
      }
      
      private void OnMaterialTypeChanged(MaterialType newType, MaterialType oldType) {
         if (MaterialTypeChanged != null) {
            MaterialTypeChanged(newType, oldType);
         }
      }
   }
}
