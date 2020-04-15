using System;
using System.Collections;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {
   public enum CacheType { New = 0, Edit, Details };

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

      private ProcessVarDouble specificHeatOfAbsoluteDryMaterial;

      //private CacheType cacheType;

      public event MaterialComponentsChangedEventHandler AbsoluteDryMaterialComponentsChanged;
      public event MoistureSubstanceChangedEventHandler MoistureSubstanceChanged;
      public event MaterialTypeChangedEventHandler MaterialTypeChanged;

      public Substance MoistureSubstance {
         get { return moistureSubstance; }
         set {
            //if (value != moistureSubstance && (value.Name.Equals("water") || materialType != MaterialType.GenericFood)) {
            if (value != moistureSubstance && (value.IsWater || materialType != MaterialType.GenericFood)) {
               Substance oldValue = moistureSubstance;
               moistureSubstance = value;
               OnMoistureSubstanceChanged(moistureSubstance, oldValue);
            }
         }
      }

      //public CacheType CacheType {
      //   get { return cacheType; }
      //}

      public MaterialType MaterialType {
         get { return materialType; }
      }

      internal CurveF[] DuhringLines {
         get { return duhringLines; }
         set { duhringLines = value; }
      }

      public ProcessVarDouble SpecificHeatOfAbsoluteDryMaterial {
         get { return specificHeatOfAbsoluteDryMaterial; }
      }

      public DryingMaterialCache(DryingMaterialCatalog catalog)
         : base() {
         this.materialType = MaterialType.GenericMaterial;
         this.catalog = catalog;
         //cacheType = CacheType.New;
         SubstanceCatalog sc = SubstanceCatalog.GetInstance();
         //Substance absoluteDryMaterial = sc.GetSubstance("Dry Material");
         Substance absoluteDryMaterial = sc.GetGenericSubstance();
         moistureSubstance = sc.GetMoistureSubstanceListForAir()[0];
         materialComponentList.Add(new MaterialComponent(absoluteDryMaterial));
         specificHeatOfAbsoluteDryMaterial = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_ABS_DRY, PhysicalQuantity.SpecificHeat, 1260.0, VarState.Specified, this);
      }

      public DryingMaterialCache(DryingMaterial dryingMaterial, DryingMaterialCatalog catalog)
         : base() {
         this.dryingMaterial = dryingMaterial;
         this.name = dryingMaterial.Name;
         this.materialType = dryingMaterial.MaterialType;
         this.materialComponentList = ((CompositeSubstance)dryingMaterial.AbsoluteDryMaterial).Components;
         this.moistureSubstance = dryingMaterial.Moisture;
         this.duhringLines = dryingMaterial.DuhringLines;
         this.catalog = catalog;
         //cacheType = CacheType.Edit;
         specificHeatOfAbsoluteDryMaterial = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_ABS_DRY, PhysicalQuantity.SpecificHeat, dryingMaterial.SpecificHeatOfAbsoluteDryMaterial, VarState.Specified, this);
      }

      public DryingMaterialCache(DryingMaterial dryingMaterial)
         : base() {
         this.dryingMaterial = dryingMaterial;
         this.name = dryingMaterial.Name;
         this.materialType = dryingMaterial.MaterialType;
         this.materialComponentList = ((CompositeSubstance)dryingMaterial.AbsoluteDryMaterial).Components;
         this.moistureSubstance = dryingMaterial.Moisture;
         this.duhringLines = dryingMaterial.DuhringLines;
         //cacheType = CacheType.Details;
         specificHeatOfAbsoluteDryMaterial = new ProcessVarDouble(StringConstants.SPECIFIC_HEAT_ABS_DRY, PhysicalQuantity.SpecificHeat, dryingMaterial.SpecificHeatOfAbsoluteDryMaterial, VarState.Specified, this);
      }

      //public DryingMaterialComponentsCacheModel(string name, MaterialType materialType, ArrayList materialComponentList, Substance moistureSubstance) : base (name, materialComponentList) {
      //   this.moistureSubstance = moistureSubstance;
      //}

      public void SetMaterialType(MaterialType type) {
         if (type == materialType) {
            return;
         }

         MaterialType oldValue = materialType;
         if (type == MaterialType.GenericMaterial) {
            RemoveAllMaterialComponents();
            SubstanceCatalog sc = SubstanceCatalog.GetInstance();
            //Substance absoluteDryMaterial = sc.GetSubstance("Dry Material");
            Substance absoluteDryMaterial = sc.GetGenericSubstance();
            materialComponentList.Add(new MaterialComponent(absoluteDryMaterial));
         }
         else if (type == MaterialType.GenericFood) {
            SwitchToGenericFood();
         }
         //else if (materialType == MaterialType.GenericFood) {
         //   SwitchFromGenericFood();
         //}

         materialType = type;
         OnMaterialTypeChanged(materialType, oldValue);
      }
         
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

         if (isNew) {
            if (catalog.IsInCatalog(materialName)) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the material catalog a material called " + materialName);
            }
            else if (materialType == MaterialType.GenericMaterial && specificHeatOfAbsoluteDryMaterial.Value <= 0.0) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Specific heat of absolute dry material cannot be less than or equal to 0");
            }
            else {
               dryingMaterial = new DryingMaterial(materialName, materialType, dryMat, moistureSubstance, true);
               dryingMaterial.SpecificHeatOfAbsoluteDryMaterial = specificHeatOfAbsoluteDryMaterial.Value;
               dryingMaterial.DuhringLines = duhringLines;
               errorMsg = catalog.AddDryingMaterial(dryingMaterial);
            }
         }
         else {
            if (!materialName.Equals(dryingMaterial.Name) && catalog.GetDryingMaterial(materialName) != null) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "There is already in the material catalog a material called " + materialName);
            }
            else if (materialType == MaterialType.GenericMaterial && specificHeatOfAbsoluteDryMaterial.Value <= 0.0) {
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Specific heat of absolute dry material cannot be less than or equal to 0");
            }
            else {
               //dryingMaterial.Name = materialName;
               //dryingMaterial.MaterialType = materialType;
               //dryingMaterial.AbsoluteDryMaterial = dryMat;
               //dryingMaterial.Moisture = moistureSubstance;
               //dryingMaterial.SpecificHeatOfAbsoluteDryMaterial = specificHeatOfAbsoluteDryMaterial.Value;
               //dryingMaterial.DuhringLines = duhringLines;
               double originalSpecificHeatOfAbsoluteDryMaterial = dryingMaterial.SpecificHeatOfAbsoluteDryMaterial;
               CurveF[] originalDuhringLines = dryingMaterial.DuhringLines;
               if (originalDuhringLines != null) {
                  originalDuhringLines.Clone();
               }

               try {
                  dryingMaterial.Update(materialName, specificHeatOfAbsoluteDryMaterial.Value, duhringLines);
                  errorMsg = catalog.UpdateDryingMaterial(dryingMaterial);
               }
               catch (Exception) {
                  dryingMaterial.SpecificHeatOfAbsoluteDryMaterial = originalSpecificHeatOfAbsoluteDryMaterial;
                  dryingMaterial.DuhringLines = originalDuhringLines;
                  errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, "Either specified absolute material specific heat or duhring lines are not appropriate.");
               }
            }
         }

         return errorMsg;
      }

      private void SwitchToGenericFood() {
         RemoveAllMaterialComponents();

         SubstanceCatalog substanceCatalog = SubstanceCatalog.GetInstance();

         IList genericFoodSubstanceList = substanceCatalog.GetGenericFoodSubstanceList();

         foreach (Substance s in genericFoodSubstanceList) {
            materialComponentList.Add(new MaterialComponent(s));
         }

         OnMaterialComponentsChanged();
         MoistureSubstance = substanceCatalog.GetSubstance(Substance.WATER);
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
