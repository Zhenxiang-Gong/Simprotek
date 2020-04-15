using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.UnitOperations
{
	/// <summary>
	/// CrossSectionGeometry is an entity class that holds a cross section parameters.
	/// </summary>
   [Serializable] 
   public class CrossSectionGeometry : Storable
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      private UnitOperation owner;
      protected ArrayList procVarList = new ArrayList();
      private CrossSectionType crossSectionType;
      private ProcessVarDouble diameter;

      private ProcessVarDouble width;
      private ProcessVarDouble height;
      private ProcessVarDouble heightWidthRatio;

      #region public properties
      public CrossSectionType CrossSectionType {
         get {return crossSectionType;}
         set {
            crossSectionType = value;
            owner.HasBeenModified(true);
         }
      }
      
      public ProcessVarDouble Diameter {
         get { return diameter; }
      }
      
      public ProcessVarDouble Width {
         get { return width; }
      }
      
      public ProcessVarDouble Height {
         get { return height; }
      }
      
      public ProcessVarDouble HeightWidthRatio {
         get { return heightWidthRatio; }
      }
      #endregion

      public CrossSectionGeometry(UnitOperation owner) : 
         this (CrossSectionType.Circle, owner) {
      }
      
      public CrossSectionGeometry(CrossSectionType crossSectionType, UnitOperation owner) : base() {
         this.owner = owner;
         this.crossSectionType = crossSectionType;
         diameter = new ProcessVarDouble(StringConstants.DIAMETER, PhysicalQuantity.SmallLength, VarState.Specified, owner);
         width = new ProcessVarDouble(StringConstants.WIDTH, PhysicalQuantity.Length, VarState.Specified, owner);
         height = new ProcessVarDouble(StringConstants.HEIGHT, PhysicalQuantity.Length, VarState.Specified, owner);
         heightWidthRatio = new ProcessVarDouble(StringConstants.HEIGHT_WIDTH_RATIO, PhysicalQuantity.Unknown, VarState.Specified, owner);

         InitializeVarListAndRegisterVars();
      }
      
      protected void InitializeVarListAndRegisterVars() {
         procVarList.Add(diameter);
         procVarList.Add(width);
         procVarList.Add(height);
         procVarList.Add(heightWidthRatio);

         owner.AddVarsOnListAndRegisterInSystem(procVarList);
      }
      public double GetArea() {
         double area = Constants.NO_VALUE;
         if (crossSectionType == CrossSectionType.Circle) {
            if (diameter.HasValue) {
               double d = diameter.Value;
               area = 0.25 * Math.PI * d * d;
            }
         }
         else if (crossSectionType == CrossSectionType.Rectangle) {
            if (height.HasValue && width.HasValue) {
               area = height.Value * width.Value;
               owner.Calculate(heightWidthRatio, height.Value/width.Value);
            }
            else if (width.HasValue && heightWidthRatio.HasValue) {
               area = heightWidthRatio.Value * width.Value * width.Value;
               owner.Calculate(height, heightWidthRatio.Value*width.Value);
            }
            else if (height.HasValue && heightWidthRatio.HasValue) {
               area = height.Value * height.Value/heightWidthRatio.Value;
               owner.Calculate(width, height.Value/heightWidthRatio.Value);
            }
         }
         return area;
      }

      protected CrossSectionGeometry(SerializationInfo info, StreamingContext context) : base(info, context) {
   }

      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCrossSectionGeometry", typeof(int));
         if (persistedClassVersion == 1) {
            this.crossSectionType = (CrossSectionType) info.GetValue("CrossSectionType", typeof(CrossSectionType));
            this.diameter = RecallStorableObject("Diameter", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.width = RecallStorableObject("Width", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.height = RecallStorableObject("Height", typeof(ProcessVarDouble)) as ProcessVarDouble;
            this.heightWidthRatio = RecallStorableObject("HeightWidthRatio", typeof(ProcessVarDouble)) as ProcessVarDouble;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionCrossSectionGeometry", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("CrossSectionType", this.crossSectionType, typeof(CrossSectionType));
         info.AddValue("Diameter", this.diameter, typeof(ProcessVarDouble));
         info.AddValue("Width", this.width, typeof(ProcessVarDouble));
         info.AddValue("Height", this.height, typeof(ProcessVarDouble));
         info.AddValue("HeightWidthRatio", this.heightWidthRatio, typeof(ProcessVarDouble));
      }
   }
}
