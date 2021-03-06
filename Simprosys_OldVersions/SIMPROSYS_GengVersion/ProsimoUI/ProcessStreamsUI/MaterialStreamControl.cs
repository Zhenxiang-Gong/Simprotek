using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
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

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for MaterialStreamControl.
	/// </summary>
   [Serializable]
   public class MaterialStreamControl : DryingStreamControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public DryingMaterialStream MaterialStream
      {
         get {return (DryingMaterialStream)this.Solvable;}
         set {this.Solvable = value;}
      }

      public MaterialStreamControl()
      {
      }

      public MaterialStreamControl(Flowsheet flowsheet, Point location, DryingMaterialStream materialStream) :
         base(flowsheet, location, materialStream)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
         UI.SetStatusColor(this, this.MaterialStream.SolveState);
         this.Orientation = StreamOrientation.Right;
         this.UpdateBackImage();
         this.flowsheet.ConnectionManager.UpdateConnections(this);
      }

		private void InitializeComponent()
		{
         // 
         // MaterialStreamControl
         // 
         this.Name = "MaterialStreamControl";
         this.Size = new System.Drawing.Size(96, 32);
      }

      protected override void DoThePaint()
      {
         this.DrawSelection();
         this.UpdateNameControlLocation();
      }

      //public override void Edit()
      //{
      //   if (this.Editor == null)
      //   {
      //      this.Editor = new MaterialStreamEditor(this);
      //      this.Editor.Owner = (Form)this.flowsheet.Parent;
      //      this.Editor.Show();
      //   }
      //   else
      //   {
      //      if (this.Editor.WindowState.Equals(FormWindowState.Minimized))
      //         this.Editor.WindowState = FormWindowState.Normal;
      //      this.Editor.Activate();
      //   }
      //}

      protected override void UpdateBackImage()
      {
         if (this.MaterialStream.MaterialStateType == MaterialStateType.Solid)
         {
            if (this.Orientation.Equals(StreamOrientation.Down))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_DOWN_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Left))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_LEFT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Right))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_RIGHT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Up))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_NOT_SOLVED_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_WITH_WARNING_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVED_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_SOLID_CTRL_SOLVE_FAILED_UP_IMG;
               }
            }
         }
         else if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
         {
            if (this.Orientation.Equals(StreamOrientation.Down))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_DOWN_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_DOWN_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Left))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_LEFT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_LEFT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Right))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_RIGHT_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_RIGHT_IMG;
               }
            }
            else if (this.Orientation.Equals(StreamOrientation.Up))
            {
               if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_NOT_SOLVED_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_WITH_WARNING_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.Solved))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVED_UP_IMG;
               }
               else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
               {
                  this.BackgroundImage = UI.IMAGES.MATERIAL_LIQUID_CTRL_SOLVE_FAILED_UP_IMG;
               }
            }
         }         
      }

      public override string ToPrintToolTip()
      {
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

         sb.Append(UI.GetVariableName(this.MaterialStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MassFlowRate, us, nfs));
         if (this.MaterialStream.MassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
         {
            sb.Append(UI.GetVariableName(this.MaterialStream.VolumeFlowRate, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.MaterialStream.VolumeFlowRate, us, nfs));
            if (this.MaterialStream.VolumeFlowRate.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(this.MaterialStream.Pressure, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.MaterialStream.Pressure, us, nfs));
            if (this.MaterialStream.Pressure.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         sb.Append(UI.GetVariableName(this.MaterialStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.Temperature, us, nfs));
         if (this.MaterialStream.Temperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.MaterialStream.MoistureContentDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MoistureContentDryBase, us, nfs));
         if (this.MaterialStream.MoistureContentDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      public override string ToPrint()
      {
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

         sb.Append(UI.GetVariableName(this.MaterialStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MassFlowRate, us, nfs));
         if (this.MaterialStream.MassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.MaterialStream.MassFlowRateDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MassFlowRateDryBase, us, nfs));
         if (this.MaterialStream.MassFlowRateDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.MaterialStream.VolumeFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.VolumeFlowRate, us, nfs));
         if (this.MaterialStream.VolumeFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
         {
            sb.Append(UI.GetVariableName(this.MaterialStream.Pressure, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.MaterialStream.Pressure, us, nfs));
            if (this.MaterialStream.Pressure.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         sb.Append(UI.GetVariableName(this.MaterialStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.Temperature, us, nfs));
         if (this.MaterialStream.Temperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
         {
            sb.Append(UI.GetVariableName(this.MaterialStream.VaporFraction, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.MaterialStream.VaporFraction, us, nfs));
            if (this.MaterialStream.VaporFraction.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         sb.Append(UI.GetVariableName(this.MaterialStream.MoistureContentWetBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MoistureContentWetBase, us, nfs));
         if (this.MaterialStream.MoistureContentWetBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.MaterialStream.MoistureContentDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.MoistureContentDryBase, us, nfs));
         if (this.MaterialStream.MoistureContentDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.MaterialStream.MaterialStateType == MaterialStateType.Liquid)
         {
            sb.Append(UI.GetVariableName(this.MaterialStream.MassConcentration, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.MaterialStream.MassConcentration, us, nfs));
            if (this.MaterialStream.MassConcentration.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         sb.Append(UI.GetVariableName(this.MaterialStream.SpecificEnthalpy, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.SpecificEnthalpy, us, nfs));
         if (this.MaterialStream.SpecificEnthalpy.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.MaterialStream.SpecificHeat, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.SpecificHeat, us, nfs));
         if (this.MaterialStream.SpecificHeat.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.MaterialStream.SpecificHeatDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.SpecificHeatDryBase, us, nfs));
         if (this.MaterialStream.SpecificHeatDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.MaterialStream.Density, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.MaterialStream.Density, us, nfs));
         if (this.MaterialStream.Density.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected MaterialStreamControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMaterialStreamControl", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionMaterialStreamControl", MaterialStreamControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
