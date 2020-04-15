using System;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.UnitOperationsUI;
using ProsimoUI.UnitOperationsUI.TwoStream;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo.Materials;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for MaterialStreamControl.
   /// </summary>
   [Serializable]
   public class MaterialStreamControl : ProcessStreamBaseControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public DryingMaterialStream MaterialStream {
         get { return (DryingMaterialStream)this.Solvable; }
         set { this.Solvable = value; }
      }

      public MaterialStreamControl() {
      }

      public MaterialStreamControl(Flowsheet flowsheet, Point location, DryingMaterialStream materialStream)
         : this(flowsheet, location, materialStream, StreamOrientation.Right) {
      }

      public MaterialStreamControl(Flowsheet flowsheet, Point location, DryingMaterialStream materialStream, StreamOrientation orientation)
         : base(flowsheet, location, materialStream, orientation) {

         if (materialStream.MaterialStateType == MaterialStateType.Liquid)
            this.Name = "Material Stream (Liquid): " + materialStream.Name;
         else if (materialStream.MaterialStateType == MaterialStateType.Solid)
            this.Name = "Material Stream (Solid): " + materialStream.Name;
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new MaterialStreamEditor(this);
            //this.editor = new ProcessStreamBaseEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      protected override void UpdateBackImage() {
         if (this.MaterialStream.MaterialStateType == MaterialStateType.Solid) {
            if (this.Orientation.Equals(StreamOrientation.Down)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_DOWN_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Left)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_LEFT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Right)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_RIGHT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Up)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_UP_IMG;
               }
            }
         }
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid) {
            if (this.Orientation.Equals(StreamOrientation.Down)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_DOWN_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_DOWN_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Left)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_LEFT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_LEFT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Right)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_RIGHT_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_RIGHT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Up)) {
               if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_UP_IMG;
               }
               else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_UP_IMG;
               }
            }
         }
      }

      public override string ToPrintToolTip() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         string ttip = "Drying Material Stream: ";
         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
            ttip = "Liquid Material Stream: ";
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Solid)
            ttip = "Solid Material Stream: ";
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Sludge)
            ttip = "Sludge Material Stream: ";

         sb.Append(ttip);
         sb.Append(this.MaterialStream.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.MaterialStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.MaterialStream.MassFlowRate, us, nfs));
         if (this.MaterialStream.MassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid) {
            sb.Append(GetVariableName(this.MaterialStream.VolumeFlowRate, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(this.MaterialStream.VolumeFlowRate, us, nfs));
            if (this.MaterialStream.VolumeFlowRate.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(GetVariableName(this.MaterialStream.Pressure, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(this.MaterialStream.Pressure, us, nfs));
            if (this.MaterialStream.Pressure.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         sb.Append(GetVariableName(this.MaterialStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.MaterialStream.Temperature, us, nfs));
         if (this.MaterialStream.Temperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.MaterialStream.MoistureContentDryBase, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.MaterialStream.MoistureContentDryBase, us, nfs));
         if (this.MaterialStream.MoistureContentDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         string ttip = "Drying Material Stream: ";
         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
            ttip = "Liquid Material Stream: ";
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Solid)
            ttip = "Solid Material Stream: ";
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Sludge)
            ttip = "Sludge Material Stream: ";

         sb.Append(ttip);
         sb.Append(this.MaterialStream.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(ToPrintVarList());
         return sb.ToString();
      }

      protected MaterialStreamControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionMaterialStreamControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionMaterialStreamControl", MaterialStreamControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//private void Init() {
//   this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
//   UI.SetStatusColor(this, this.MaterialStream.SolveState);
//   this.Orientation = StreamOrientation.Right;
//   this.UpdateBackImage();
//   this.flowsheet.ConnectionManager.UpdateConnections(this);
//}

//private void InitializeComponent() {
//   // 
//   // MaterialStreamControl
//   // 
//   this.Name = "MaterialStreamControl";
//   //this.Size = new System.Drawing.Size(96, 32);
//}

//protected override void DoThePaint()
//{
//   this.DrawSelection();
//   this.UpdateNameControlLocation();
//}

      //sb.Append(GetVariableName(this.MaterialStream.MassFlowRate, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.MassFlowRate, us, nfs));
      //if (this.MaterialStream.MassFlowRate.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.MassFlowRateDryBase, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.MassFlowRateDryBase, us, nfs));
      //if (this.MaterialStream.MassFlowRateDryBase.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.VolumeFlowRate, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.VolumeFlowRate, us, nfs));
      //if (this.MaterialStream.VolumeFlowRate.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid) {
      //   sb.Append(GetVariableName(this.MaterialStream.Pressure, us));
      //   sb.Append(" = ");
      //   sb.Append(GetVariableValue(this.MaterialStream.Pressure, us, nfs));
      //   if (this.MaterialStream.Pressure.IsSpecified)
      //      sb.Append(" * ");
      //   sb.Append("\r\n");
      //}

      //sb.Append(GetVariableName(this.MaterialStream.Temperature, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.Temperature, us, nfs));
      //if (this.MaterialStream.Temperature.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid) {
      //   sb.Append(GetVariableName(this.MaterialStream.VaporFraction, us));
      //   sb.Append(" = ");
      //   sb.Append(GetVariableValue(this.MaterialStream.VaporFraction, us, nfs));
      //   if (this.MaterialStream.VaporFraction.IsSpecified)
      //      sb.Append(" * ");
      //   sb.Append("\r\n");
      //}

      //sb.Append(GetVariableName(this.MaterialStream.MoistureContentWetBase, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.MoistureContentWetBase, us, nfs));
      //if (this.MaterialStream.MoistureContentWetBase.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.MoistureContentDryBase, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.MoistureContentDryBase, us, nfs));
      //if (this.MaterialStream.MoistureContentDryBase.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid) {
      //   sb.Append(GetVariableName(this.MaterialStream.MassConcentration, us));
      //   sb.Append(" = ");
      //   sb.Append(GetVariableValue(this.MaterialStream.MassConcentration, us, nfs));
      //   if (this.MaterialStream.MassConcentration.IsSpecified)
      //      sb.Append(" * ");
      //   sb.Append("\r\n");
      //}

      //sb.Append(GetVariableName(this.MaterialStream.SpecificEnthalpy, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.SpecificEnthalpy, us, nfs));
      //if (this.MaterialStream.SpecificEnthalpy.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.SpecificHeat, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.SpecificHeat, us, nfs));
      //if (this.MaterialStream.SpecificHeat.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.SpecificHeatDryBase, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.SpecificHeatDryBase, us, nfs));
      //if (this.MaterialStream.SpecificHeatDryBase.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");

      //sb.Append(GetVariableName(this.MaterialStream.Density, us));
      //sb.Append(" = ");
      //sb.Append(GetVariableValue(this.MaterialStream.Density, us, nfs));
      //if (this.MaterialStream.Density.IsSpecified)
      //   sb.Append(" * ");
      //sb.Append("\r\n");



