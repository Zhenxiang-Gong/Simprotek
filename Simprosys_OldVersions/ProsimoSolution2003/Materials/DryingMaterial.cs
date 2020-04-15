using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.SubstanceLibrary;
using Prosimo.Plots;

namespace Prosimo.Materials {
   
   public enum SolutionType {Unknown = 0, Sucrose, ReducingSugars, Juices};
   public enum MaterialType {Unknown = 0, GenericFood, SpecialFood, Inorganic};
   public enum MilkType {WholeMilk = 0, SkimMilk};
   
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

      public string Name {
         get { return name; }
         set { name = value;}
      }

      public bool IsUserDefined {
         get {return isUserDefined;}
         set {isUserDefined = value;}
      }
      
      public MaterialType MaterialType {
         get { return materialType; }
         set { materialType = value;}
      }
      
      public SolutionType SolutionType {
         get { return solutionType; }
         set { solutionType = value;}
      }

      public CurveF[] DuhringLines {
         get {return duhringLines;}
         set {duhringLines = value;}
      }

	   public Substance AbsoluteDryMaterial {
		   get { return absoluteDryMaterial; }
		   set { absoluteDryMaterial = value; }
	   }
      
	   public Substance Moisture {
		   get { return moisture; }
		   set { moisture = value; }
	   }
	   
	   public DryingMaterial(string name, MaterialType type, Substance absoluteDryMaterial, Substance moisture, bool isUserDefined)  {
         this.Name = name;
         this.materialType = type;
         this.absoluteDryMaterial = absoluteDryMaterial;
         this.moisture = moisture;
         this.solutionType = SolutionType.Unknown;
         this.isUserDefined = isUserDefined;
      }

	   public DryingMaterial Clone() {
		   DryingMaterial newDM = (DryingMaterial) this.MemberwiseClone();
         if (absoluteDryMaterial is CompositeSubstance) 
         {
            newDM.absoluteDryMaterial = ((CompositeSubstance)absoluteDryMaterial).Clone();
         }
		   return newDM;
	   }

      public override string ToString() 
      {
         return name;
      }

	   protected DryingMaterial(SerializationInfo info, StreamingContext context) : base (info, context) {
      }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int) info.GetValue("ClassPersistenceVersionDryingMaterial", typeof(int));
         if (persistedClassVersion == 1) {
            this.name = (string) info.GetValue("Name", typeof(string));
            this.isUserDefined = (bool) info.GetValue("IsUserDefined", typeof(bool));
            
            this.absoluteDryMaterial = CompositeSubstance.RecallSubstance(info);

            this.moisture = Substance.RecallSubstance(info, "MoistureName");
            this.materialType = (MaterialType) info.GetValue("MaterialType", typeof(MaterialType));
            this.solutionType = (SolutionType) info.GetValue("SolutionType", typeof(SolutionType));
            this.duhringLines = (CurveF[]) RecallArrayObject("DuhringLines", typeof(CurveF[]));
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

         info.AddValue("MoistureName", moisture.Name, typeof(string));
         info.AddValue("MaterialType", materialType, typeof(MaterialType));
         info.AddValue("SolutionType", solutionType, typeof(SolutionType));
         info.AddValue("DuhringLines", duhringLines, typeof(CurveF[]));
      }
   }
}
