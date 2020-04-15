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

namespace ProsimoUI.ProcessStreamsUI
{
   /// <summary>
   /// Summary description for GasStreamControl.
   /// </summary>
   [Serializable]
   public class GasStreamControl : DryingStreamControl 
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public DryingGasStream GasStream
      {
         get {return (DryingGasStream)this.Solvable;}
         set {this.Solvable = value;}
      }

      public GasStreamControl()
      {
      }

      public GasStreamControl(Flowsheet flowsheet, Point location, DryingGasStream gasStream) :
         base(flowsheet, location, gasStream)
      {

         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
         UI.SetStatusColor(this, this.GasStream.SolveState);
         this.Orientation = StreamOrientation.Right;
         this.UpdateBackImage();
         this.flowsheet.ConnectionManager.UpdateConnections(this);
      }

      private void InitializeComponent()
      {
         // 
         // GasStreamControl
         // 
         this.BackColor = System.Drawing.Color.Gainsboro;
         this.Name = "GasStreamControl";
         this.Size = new System.Drawing.Size(144, 128);

      }

      protected override void DoThePaint()
      {
         this.DrawSelection();
         this.UpdateNameControlLocation();
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new GasStreamEditor(this);
            this.Editor.Owner = (Form)this.flowsheet.Parent;
            this.Editor.Show();
         }
         else
         {
            if (this.Editor.WindowState.Equals(FormWindowState.Minimized))
               this.Editor.WindowState = FormWindowState.Normal;
            this.Editor.Activate();
         }
      }

      protected override void UpdateBackImage()
      {
         if (this.Orientation.Equals(StreamOrientation.Down))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_NOT_SOLVED_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_WITH_WARNING_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVE_FAILED_DOWN_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Left))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_NOT_SOLVED_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_WITH_WARNING_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVE_FAILED_LEFT_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Right))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_NOT_SOLVED_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVE_FAILED_RIGHT_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Up))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_NOT_SOLVED_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_WITH_WARNING_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVED_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.GAS_CTRL_SOLVE_FAILED_UP_IMG;
            }
         }
      }

      public override string ToPrintToolTip()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Drying Gas Stream: ");
         sb.Append(this.GasStream.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.MassFlowRate, us, nfs));
         if (this.GasStream.MassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.GasStream.VolumeFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.VolumeFlowRate, us, nfs));
         if (this.GasStream.VolumeFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Pressure, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Pressure, us, nfs));
         if (this.GasStream.Pressure.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Temperature, us, nfs));
         if (this.GasStream.Temperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Humidity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Humidity, us, nfs));
         if (this.GasStream.Humidity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.RelativeHumidity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.RelativeHumidity, us, nfs));
         if (this.GasStream.RelativeHumidity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         return sb.ToString();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Drying Gas Stream: ");
         sb.Append(this.GasStream.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.MassFlowRate, us, nfs));
         if (this.GasStream.MassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.GasStream.MassFlowRateDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.MassFlowRateDryBase, us, nfs));
         if (this.GasStream.MassFlowRateDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.GasStream.VolumeFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.VolumeFlowRate, us, nfs));
         if (this.GasStream.VolumeFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Pressure, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Pressure, us, nfs));
         if (this.GasStream.Pressure.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Temperature, us, nfs));
         if (this.GasStream.Temperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.WetBulbTemperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.WetBulbTemperature, us, nfs));
         if (this.GasStream.WetBulbTemperature.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.DewPoint, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.DewPoint, us, nfs));
         if (this.GasStream.DewPoint.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Humidity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Humidity, us, nfs));
         if (this.GasStream.Humidity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.RelativeHumidity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.RelativeHumidity, us, nfs));
         if (this.GasStream.RelativeHumidity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.SpecificEnthalpy, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.SpecificEnthalpy, us, nfs));
         if (this.GasStream.SpecificEnthalpy.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.SpecificHeatDryBase, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.SpecificHeatDryBase, us, nfs));
         if (this.GasStream.SpecificHeatDryBase.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.GasStream.Density, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.GasStream.Density, us, nfs));
         if (this.GasStream.Density.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected GasStreamControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionGasStreamControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionGasStreamControl", GasStreamControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
