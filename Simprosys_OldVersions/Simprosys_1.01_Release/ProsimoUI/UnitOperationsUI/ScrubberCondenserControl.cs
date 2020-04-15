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
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI
{
   /// <summary>
   /// Summary description for ScrubberCondenserControl.
   /// </summary>
   [Serializable]
   public class ScrubberCondenserControl : UnitOpControl
   {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation GAS_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation GAS_OUTLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation LIQUID_OUTLET_ORIENTATION = PointOrientation.S;

      public ScrubberCondenser ScrubberCondenser
      {
         get { return (ScrubberCondenser)this.Solvable; }
         set { this.Solvable = value; }
      }

      public ScrubberCondenserControl()
      {
      }

      public ScrubberCondenserControl(Flowsheet flowsheet, Point location, ScrubberCondenser scrubberCondenser)
         :
         base(flowsheet, location, scrubberCondenser)
      {
         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.ScrubberCondenser.SolveState);
         this.UpdateBackImage();
      }

      private void InitializeComponent()
      {
         // 
         // ScrubberCondenserControl
         // 
         this.Name = "ScrubberCondenserControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestGasIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestGasOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestLiquidOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetLiquidOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      protected override void MouseDownHandler(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidOut(p))
            {
               if (HitTestGasIn(p))
                  idx = ScrubberCondenser.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = ScrubberCondenser.GAS_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = ScrubberCondenser.LIQUID_OUTLET_INDEX;

               if (this.ScrubberCondenser.CanConnect(idx))
               {
                  // ok for the second step
                  this.flowsheet.firstStepCtrl = this;
                  this.flowsheet.Activity = FlowsheetActivity.AddingConnStepTwo;
                  this.flowsheet.attachIndex = idx;
               }
            }
            else
            {
               this.flowsheet.ResetActivity();
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if (HitTestGasIn(p))
                  idx = ScrubberCondenser.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = ScrubberCondenser.GAS_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = ScrubberCondenser.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.ScrubberCondenser.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.ScrubberCondenser.AttachStream(ctrl.ProcessStreamBase, idx);
                  UI.ShowError(error);
               }
            }
            this.flowsheet.ResetActivity();
         }
         else
         {
            this.flowsheet.ResetActivity();
            this.PerformSelection();
            this.PrepareForTheMove(p.X, p.Y);
         }
      }

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e)
      {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as ScrubberCondenserControl);
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
         this.DrawSlot(this, this.GetGasInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetGasOutSlotPoint(), SlotPosition.Up, SlotDirection.Out);
         this.DrawSlot(this, this.GetLiquidOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new ScrubberCondenserEditor(this);
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
            this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public Point GetGasInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetGasInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2 + this.Height / 6 - 1;
         return p;
      }

      public Point GetGasOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0, 0);
         p.X += this.Width / 2;
         return p;
      }

      public Point GetLiquidOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetLiquidOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetLiquidOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0, 0);
         p.Y += this.Height;
         p.X += this.Width / 2;
         return p;
      }

      public override void CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad)
      {
         StreamType streamType = StreamType.Unknown;
         if (ps is DryingGasStream)
         {
            streamType = StreamType.Gas;
         }
         else if (ps is ProcessStream)
         {
            streamType = StreamType.Process;
         }
         else if (ps is DryingMaterialStream)
         {
            streamType = StreamType.Material;
         }

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == ScrubberCondenser.GAS_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ScrubberCondenser.GAS_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasInConnectionPoint();
            PointOrientation uoOrientation = ScrubberCondenserControl.GAS_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == ScrubberCondenser.GAS_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ScrubberCondenser.GAS_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasOutConnectionPoint();
            PointOrientation uoOrientation = ScrubberCondenserControl.GAS_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == ScrubberCondenser.LIQUID_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ScrubberCondenser.LIQUID_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetLiquidOutConnectionPoint();
            PointOrientation uoOrientation = ScrubberCondenserControl.LIQUID_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }

         if (conn != null)
         {
            this.flowsheet.ConnectionManager.Connections.Add(conn);
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      protected override void ShowConnectionPoints(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidOut(p))
            {
               if (HitTestGasIn(p))
                  idx = ScrubberCondenser.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = ScrubberCondenser.GAS_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = ScrubberCondenser.LIQUID_OUTLET_INDEX;

               if (this.ScrubberCondenser.CanConnect(idx))
                  this.Cursor = Cursors.Cross;
               else
                  this.Cursor = Cursors.Default;
            }
            else
            {
               this.Cursor = Cursors.Default;
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if (HitTestGasIn(p))
                  idx = ScrubberCondenser.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = ScrubberCondenser.GAS_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = ScrubberCondenser.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.ScrubberCondenser.CanAttachStream(ctrl.ProcessStreamBase, idx))
                  this.Cursor = Cursors.Cross;
               else
                  this.Cursor = Cursors.Default;
            }
            else
            {
               this.Cursor = Cursors.Default;
            }
         }
         else
         {
            this.Cursor = Cursors.Default;
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

         sb.Append("Scrubber Condenser: ");
         sb.Append(this.ScrubberCondenser.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.GasPressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.GasPressureDrop, us, nfs));
         if (this.ScrubberCondenser.GasPressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.CollectionEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.CollectionEfficiency, us, nfs));
         if (this.ScrubberCondenser.CollectionEfficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.InletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.InletParticleLoading, us, nfs));
         if (this.ScrubberCondenser.InletParticleLoading.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.OutletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.OutletParticleLoading, us, nfs));
         if (this.ScrubberCondenser.OutletParticleLoading.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.ParticleCollectionRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.ParticleCollectionRate, us, nfs));
         if (this.ScrubberCondenser.ParticleCollectionRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         if (this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.CoolingDuty, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.CoolingDuty, us, nfs));
         if (this.ScrubberCondenser.CoolingDuty.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.LiquidToGasRatio, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.LiquidToGasRatio, us, nfs));
         if (this.ScrubberCondenser.LiquidToGasRatio.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.LiquidRecirculationMassFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.LiquidRecirculationMassFlowRate, us, nfs));
         if (this.ScrubberCondenser.LiquidRecirculationMassFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate, us, nfs));
         if (this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected ScrubberCondenserControl(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionScrubberCondenserControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionScrubberCondenserControl", ScrubberCondenserControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
