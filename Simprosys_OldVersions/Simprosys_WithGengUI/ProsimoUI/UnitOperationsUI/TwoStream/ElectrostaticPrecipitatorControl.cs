using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorControl.
   /// </summary>
   [Serializable]
   public class ElectrostaticPrecipitatorControl : TwoStreamUnitOpControl
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public ElectrostaticPrecipitator ElectrostaticPrecipitator
      {
         get {return (ElectrostaticPrecipitator)this.Solvable;}
         set {this.Solvable = value;}
      }

      public ElectrostaticPrecipitatorControl()
      {
      }

      public ElectrostaticPrecipitatorControl(Flowsheet flowsheet, Point location, ElectrostaticPrecipitator electrostaticPrecipitator) :
         base(flowsheet, location, electrostaticPrecipitator)
      {
         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.ElectrostaticPrecipitator.SolveState);
         this.UpdateBackImage();
      }

      private void InitializeComponent()
      {
         // 
         // ElectrostaticPrecipitatorControl
         // 
         this.Name = "ElectrostaticPrecipitatorControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public override bool HitTestStreamIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public override bool HitTestStreamOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      protected override void DoThePaint()
      {
         this.DrawBorder();
         this.DrawSelection();
         this.DrawSlots();
         this.UpdateNameControlLocation();
      }

      protected void DrawSlots()
      {
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      }

       public override void Edit()
       {
           if (this.Editor == null)
           {
               this.Editor = new ElectrostaticPrecipitatorEditor(this);
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
         if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
         {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Electrostatic Precipitator: ");
         sb.Append(this.ElectrostaticPrecipitator.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.GasPressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.GasPressureDrop, us, nfs));
         if (this.ElectrostaticPrecipitator.GasPressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.CollectionEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.CollectionEfficiency, us, nfs));
         if (this.ElectrostaticPrecipitator.CollectionEfficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.InletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.InletParticleLoading, us, nfs));
         if (this.ElectrostaticPrecipitator.InletParticleLoading.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.OutletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.OutletParticleLoading, us, nfs));
         if (this.ElectrostaticPrecipitator.OutletParticleLoading.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.ParticleCollectionRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.ParticleCollectionRate, us, nfs));
         if (this.ElectrostaticPrecipitator.ParticleCollectionRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         if (this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.DriftVelocity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.DriftVelocity, us, nfs));
         if (this.ElectrostaticPrecipitator.DriftVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ElectrostaticPrecipitator.TotalSurfaceArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ElectrostaticPrecipitator.TotalSurfaceArea, us, nfs));
         if (this.ElectrostaticPrecipitator.TotalSurfaceArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected ElectrostaticPrecipitatorControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionElectrostaticPrecipitatorControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionElectrostaticPrecipitatorControl", ElectrostaticPrecipitatorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
