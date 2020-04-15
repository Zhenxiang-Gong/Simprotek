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
   /// Summary description for WetScrubberControl.
   /// </summary>
   [Serializable]
   public class WetScrubberControl : UnitOpControl
   {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation GAS_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation GAS_OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation LIQUID_INLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation LIQUID_OUTLET_ORIENTATION = PointOrientation.S;

      public WetScrubber WetScrubber
      {
         get {return (WetScrubber)this.Solvable;}
         set {this.Solvable = value;}
      }

      public WetScrubberControl()
      {
      }

      public WetScrubberControl(Flowsheet flowsheet, Point location, WetScrubber wetScrubber) :
         base(flowsheet, location, wetScrubber)
      {
         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.WetScrubber.SolveState);
         this.UpdateBackImage();
      }

      private void InitializeComponent()
      {
         // 
         // WetScrubberControl
         // 
         this.Name = "WetScrubberControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestGasIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestGasOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestLiquidIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetLiquidInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X-UI.SLOT_DELTA, 0, slot.X+UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestLiquidOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetLiquidOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X-UI.SLOT_DELTA, this.Height, slot.X+UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      protected override void MouseDownHandler(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidIn(p) || HitTestLiquidOut(p))
            {
               if (HitTestGasIn(p))
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               if (this.WetScrubber.CanConnect(idx))
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
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.WetScrubber.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.WetScrubber.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as WetScrubberControl);
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
         this.DrawSlot(this, this.GetGasOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetLiquidInSlotPoint(), SlotPosition.Up, SlotDirection.In);
         this.DrawSlot(this, this.GetLiquidOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new WetScrubberEditor(this);
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
            this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVE_FAILED_IMG;
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
         Point p = new Point(0,0);
         p.Y += this.Height/2 + this.Height/6 - 1;
         return p;
      }

      public Point GetGasOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/4 + 1;
         return p;
      }

      public Point GetLiquidInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetLiquidInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetLiquidInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0,0);
         p.X += this.Width/2;
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
         Point p = new Point(0,0);
         p.Y += this.Height;
         p.X += this.Width/2;
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

         if (ad == WetScrubber.GAS_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.GAS_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasInConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.GAS_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == WetScrubber.GAS_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.GAS_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasOutConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.GAS_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == WetScrubber.LIQUID_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.LIQUID_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetLiquidInConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.LIQUID_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == WetScrubber.LIQUID_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.LIQUID_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetLiquidOutConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.LIQUID_OUTLET_ORIENTATION;
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
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidIn(p) || HitTestLiquidOut(p))
            {
               if (HitTestGasIn(p))
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               if (this.WetScrubber.CanConnect(idx))
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
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.WetScrubber.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Wet Scrubber: ");
         sb.Append(this.WetScrubber.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.GasPressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.GasPressureDrop, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.CollectionEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.CollectionEfficiency, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.InletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.InletParticleLoading, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.OutletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.OutletParticleLoading, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.ParticleCollectionRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.ParticleCollectionRate, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.MassFlowRateOfParticleLostToGasOutlet, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.WetScrubber.LiquidToGasRatio, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.WetScrubber.LiquidToGasRatio, us, nfs));
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected WetScrubberControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionWetScrubberControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionWetScrubberControl", WetScrubberControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
