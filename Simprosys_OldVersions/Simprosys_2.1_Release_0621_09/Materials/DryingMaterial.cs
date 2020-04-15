using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {

   public enum SolutionType { Unknown = 0, Sucrose, ReducingSugars, Juices };
   public enum MaterialType { GenericMaterial = 0, GenericFood, SpecialFood};
   public enum MilkType { WholeMilk = 0, SkimMilk };

   /// <summary>
   /// Summary description for DryingMaterial.
   /// </summary>
   [Serializable]
   public class DryingMaterial : Storable {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      string name;
      bool isUserDefined;

      Substance absoluteDryMaterial;
      Substance moisture;

      private MaterialType materialType;
      private SolutionType solutionType;
      private CurveF[] duhringLines = null;
      
      private double specificHeatOfAbsoluteDryMaterial;

      public event DryingMaterialChangedEventHandler DryingMaterialChanged;
      
      public string Name {
         get { return name; }
         set {
            if (value != name) {
               name = value;
               //OnDryingMaterialChanged(true);
            }
         }
      }

      public bool IsUserDefined {
         get { return isUserDefined; }
         set {
            if (isUserDefined != value) {
               isUserDefined = value;
            }
         }
      }

      public MaterialType MaterialType {
         get { return materialType; }
         internal set {
            if (materialType != value) {
               materialType = value;
            }
         }
      }

      public SolutionType SolutionType {
         get { return solutionType; }
         internal set {
            if (solutionType != value) {
               solutionType = value;
            }
         }
      }

      public CurveF[] DuhringLines {
         get { return duhringLines; }
         internal set {
            if (!AreTheyEqual(duhringLines, value)) {
               duhringLines = value;
               //OnDryingMaterialChanged(false);
            }
         }
      }

      public Substance AbsoluteDryMaterial {
         get { return absoluteDryMaterial; }
         internal set {
            if (!value.Equals(absoluteDryMaterial)) {
               absoluteDryMaterial = value;
               //OnDryingMaterialChanged(false);
            }
         }
      }

      public Substance Moisture {
         get { return moisture; }
         internal set {
            if (moisture.Equals(value)) {
               moisture = value;
            }
         }
      }

      public double SpecificHeatOfAbsoluteDryMaterial {
         get { return specificHeatOfAbsoluteDryMaterial; }
         internal set {
            if (value != specificHeatOfAbsoluteDryMaterial) {
               specificHeatOfAbsoluteDryMaterial = value;
               //OnDryingMaterialChanged(false);
            }
         }
      }

      public DryingMaterial(string name, MaterialType type, Substance absoluteDryMaterial, Substance moisture, bool isUserDefined) {
         this.Name = name;
         this.materialType = type;
         this.absoluteDryMaterial = absoluteDryMaterial;
         this.moisture = moisture;
         this.solutionType = SolutionType.Unknown;
         this.isUserDefined = isUserDefined;
         this.specificHeatOfAbsoluteDryMaterial = 1260.0;
      }

      private void OnDryingMaterialChanged(bool isNameChangeOnly) {
         if (DryingMaterialChanged != null) {
            DryingMaterialChanged(this, new DryingMaterialChangedEventArgs(this, isNameChangeOnly));
         }
      }

      public DryingMaterial Clone() {
         DryingMaterial newDM = (DryingMaterial)this.MemberwiseClone();
         if (absoluteDryMaterial is CompositeSubstance) {
            newDM.absoluteDryMaterial = ((CompositeSubstance)absoluteDryMaterial).Clone();
         }
         newDM.specificHeatOfAbsoluteDryMaterial = Constants.NO_VALUE;
         return newDM;
      }

      public override bool Equals(object obj) {
         if (obj == null) {
            return false;
         }

         bool isEqual = false;
         DryingMaterial dm = obj as DryingMaterial;
         //if (name.Equals(dm.Name) && isUserDefined == dm.IsUserDefined && moisture == dm.moisture &&
         if (name.Equals(dm.Name) && moisture == dm.moisture && 
            materialType == dm.MaterialType && solutionType == dm.SolutionType &&
            Math.Abs(specificHeatOfAbsoluteDryMaterial - dm.SpecificHeatOfAbsoluteDryMaterial) < 1.0e-3 &&
            absoluteDryMaterial.Equals(dm.AbsoluteDryMaterial)) {
            isEqual = AreTheyEqual(duhringLines, dm.DuhringLines);
         }
         return isEqual;
      }

      public override int GetHashCode() {
         return this.name.GetHashCode();
      }

      public override string ToString() {
         return name;
      }

      private bool AreTheyEqual(CurveF[] duhringLines1, CurveF[] duhringLines2) {
         bool isEqual = false;
         if (duhringLines1 == null && duhringLines2 == null) {
            isEqual = true;
         }
         else if ((duhringLines1 == null && duhringLines2 != null) || (duhringLines1 != null && duhringLines2 == null)) {
         }
         else if (duhringLines1.Length == duhringLines2.Length) {
            for (int i = 0; i < duhringLines1.Length; i++) {
               if (duhringLines1[i].Equals(duhringLines2[i])) {
                  isEqual = true;
               }
               else {
                  isEqual = false;
                  break;
               }
            }
         }
         return isEqual;
      }

      internal void Update(string name, double specificHeatOfAbsoluteDryMaterial, CurveF[] duhringLines) {
         this.name = name;
         this.specificHeatOfAbsoluteDryMaterial = specificHeatOfAbsoluteDryMaterial;
         this.duhringLines = duhringLines;
         OnDryingMaterialChanged(false);
      }

      protected DryingMaterial(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryingMaterial", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string)info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool)info.GetValue("IsUserDefined", typeof(bool));

            this.absoluteDryMaterial = CompositeSubstance.RecallSubstance(info);

            this.moisture = Substance.RecallSubstance(info, "MoistureName");
            this.materialType = (MaterialType)info.GetValue("MaterialType", typeof(MaterialType));
            this.solutionType = (SolutionType)info.GetValue("SolutionType", typeof(SolutionType));
            this.duhringLines = (CurveF[])RecallArrayObject("DuhringLines", typeof(CurveF[]));
            this.specificHeatOfAbsoluteDryMaterial = (double)info.GetValue("SpecificHeatOfAbsoluteDryMaterial", typeof(double));
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDryingMaterial", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", this.name, typeof(string));
         info.AddValue("IsUserDefined", this.isUserDefined, typeof(bool));

         //has to store absoluteDryMaterial substance in a special way since it may be a compisite substance
         CompositeSubstance.StoreSubstance(info, absoluteDryMaterial);

         info.AddValue("MoistureName", this.moisture.Name, typeof(string));
         info.AddValue("MaterialType", this.materialType, typeof(MaterialType));
         info.AddValue("SolutionType", this.solutionType, typeof(SolutionType));
         info.AddValue("DuhringLines", this.duhringLines, typeof(CurveF[]));
         info.AddValue("SpecificHeatOfAbsoluteDryMaterial", this.specificHeatOfAbsoluteDryMaterial, typeof(double));
      }
   }
}
