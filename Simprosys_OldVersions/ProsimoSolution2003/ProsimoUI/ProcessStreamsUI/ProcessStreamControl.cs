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
   /// Summary description for ProcessStreamControl.
   /// </summary>
   [Serializable]
   public class ProcessStreamControl : ProcessStreamBaseControl 
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public ProcessStream ProcessStream
      {
         get {return (ProcessStream)this.Solvable;}
         set {this.Solvable = value;}
      }

      public ProcessStreamControl()
      {
      }

      public ProcessStreamControl(Flowsheet flowsheet, Point location, ProcessStream processStream) :
         base(flowsheet, location, processStream)
      {

         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
         UI.SetStatusColor(this, this.ProcessStream.SolveState);
         this.Orientation = StreamOrientation.Right;
         this.UpdateBackImage();
         this.flowsheet.ConnectionManager.UpdateConnections(this);
      }

      private void InitializeComponent()
      {
         // 
         // ProcessStreamControl
         // 
         this.Name = "ProcessStreamControl";
         this.Size = new System.Drawing.Size(72, 32);
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
            this.Editor = new ProcessStreamEditor(this);
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
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_NOT_SOLVED_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_PARTIALLY_SOLVED_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVED_DOWN_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVE_FAILED_DOWN_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Left))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_NOT_SOLVED_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_PARTIALLY_SOLVED_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVED_LEFT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVE_FAILED_LEFT_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Right))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_NOT_SOLVED_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_PARTIALLY_SOLVED_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVED_RIGHT_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVE_FAILED_RIGHT_IMG;
            }
         }
         else if (this.Orientation.Equals(StreamOrientation.Up))
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_NOT_SOLVED_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_PARTIALLY_SOLVED_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVED_UP_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.PROCESS_CTRL_SOLVE_FAILED_UP_IMG;
            }
         }
      }

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.NumericFormatString;
         StringBuilder sb = new StringBuilder();
         
         sb.Append("Process Stream: ");
         sb.Append(this.ProcessStream.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.MassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.MassFlowRate, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.VolumeFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.VolumeFlowRate, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.Pressure, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.Pressure, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.Temperature, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.Temperature, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.SpecificEnthalpy, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.SpecificEnthalpy, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.SpecificHeat, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.SpecificHeat, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ProcessStream.Density, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ProcessStream.Density, us, nfs));
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected ProcessStreamControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessStreamControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionProcessStreamControl", ProcessStreamControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
