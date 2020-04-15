using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for WetScrubberControl.
   /// </summary>
   [Serializable]
   public class WetScrubberControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation GAS_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation GAS_OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation LIQUID_INLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation LIQUID_OUTLET_ORIENTATION = PointOrientation.S;

      private static IDictionary<SolveState, Image> backgroundImageTable;

      public WetScrubber WetScrubber {
         get { return (WetScrubber)this.solvable; }
         set { this.solvable = value; }
      }

      static WetScrubberControl() {
         backgroundImageTable = new Dictionary<SolveState, Image>();
         backgroundImageTable.Add(SolveState.NotSolved, UI.IMAGES.WETSCRUBBER_CTRL_NOT_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolvedWithWarning, UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_WITH_WARNING_IMG);
         backgroundImageTable.Add(SolveState.Solved, UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolveFailed, UI.IMAGES.WETSCRUBBER_CTRL_SOLVE_FAILED_IMG);
      }

      public WetScrubberControl() {
      }

      public WetScrubberControl(Flowsheet flowsheet, Point location, WetScrubber wetScrubber)
         : base(flowsheet, location, wetScrubber) {
      }

      public bool HitTestGasIn(Point p) {
         bool hit = false;
         Point slot = this.GetGasInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();
         return hit;
      }

      public bool HitTestGasOut(Point p) {
         bool hit = false;
         Point slot = this.GetGasOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();
         return hit;
      }

      public bool HitTestLiquidIn(Point p) {
         bool hit = false;
         Point slot = this.GetLiquidInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();
         return hit;
      }

      public bool HitTestLiquidOut(Point p) {
         bool hit = false;
         Point slot = this.GetLiquidOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();
         return hit;
      }

      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidIn(p) || HitTestLiquidOut(p)) {
               if (HitTestGasIn(p))
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               if (this.WetScrubber.CanAttach(idx)) {
                  // ok for the second step
                  this.flowsheet.firstStepCtrl = this;
                  this.flowsheet.SetFlowsheetActivity(FlowsheetActivity.AddingConnStepTwo);
                  this.flowsheet.attachIndex = idx;
               }
            }
            else {
               this.flowsheet.ResetActivity();
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo) {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase) {
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
               if (this.WetScrubber.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.WetScrubber.AttachStream(ctrl.ProcessStreamBase, idx);
                  UI.ShowError(error);
               }
            }
            this.flowsheet.ResetActivity();
         }
         else {
            this.flowsheet.ResetActivity();
            this.PerformSelection();
            this.PrepareForTheMove(p.X, p.Y);
         }
      }

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e) {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as WetScrubberControl);
      }

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetGasInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetGasOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetLiquidInSlotPoint(), SlotPosition.Up, SlotDirection.In);
         this.DrawSlot(this, this.GetLiquidOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new WetScrubberEditor(this);
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
         //if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
         //   this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_NOT_SOLVED_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
         //   this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_WITH_WARNING_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
         //   this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVED_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
         //   this.BackgroundImage = UI.IMAGES.WETSCRUBBER_CTRL_SOLVE_FAILED_IMG;
         //}
         this.BackgroundImage = backgroundImageTable[this.solvable.SolveState];
      }

      public Point GetGasInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetGasInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2 + this.Height / 6 - 1;
         return p;
      }

      public Point GetGasOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 4 + 1;
         return p;
      }

      public Point GetLiquidInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetLiquidInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetLiquidInSlotPoint() {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0, 0);
         p.X += this.Width / 2;
         return p;
      }

      public Point GetLiquidOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetLiquidOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetLiquidOutSlotPoint() {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0, 0);
         p.Y += this.Height;
         p.X += this.Width / 2;
         return p;
      }

      public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad) {

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == WetScrubber.GAS_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.GAS_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetGasInConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.GAS_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            //conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == WetScrubber.GAS_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.GAS_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetGasOutConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.GAS_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            //conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == WetScrubber.LIQUID_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.LIQUID_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetLiquidInConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.LIQUID_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == WetScrubber.LIQUID_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = WetScrubber.LIQUID_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetLiquidOutConnectionPoint();
            PointOrientation uoOrientation = WetScrubberControl.LIQUID_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }

         if (conn != null) {
            this.flowsheet.ConnectionManager.AddConnection(conn);
            //this.flowsheet.ConnectionManager.Connections.Add(conn);
            //this.flowsheet.ConnectionManager.DrawConnections();
         }
         return conn;
      }

      protected override void ShowConnectionPoints(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestLiquidIn(p) || HitTestLiquidOut(p)) {
               if (HitTestGasIn(p))
                  idx = WetScrubber.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = WetScrubber.GAS_OUTLET_INDEX;
               else if (HitTestLiquidIn(p))
                  idx = WetScrubber.LIQUID_INLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = WetScrubber.LIQUID_OUTLET_INDEX;

               if (this.WetScrubber.CanAttach(idx))
                  this.Cursor = Cursors.Cross;
               else
                  this.Cursor = Cursors.Default;
            }
            else {
               this.Cursor = Cursors.Default;
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo) {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase) {
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
            else {
               this.Cursor = Cursors.Default;
            }
         }
         else {
            this.Cursor = Cursors.Default;
         }
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Wet Scrubber: ");
         sb.Append(this.WetScrubber.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(ToPrintVarList());

         return sb.ToString();
      }

      protected WetScrubberControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionWetScrubberControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionWetScrubberControl", WetScrubberControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//private void Init() {
//   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
//   UI.SetStatusColor(this, this.WetScrubber.SolveState);
//   this.UpdateBackImage();
//}

//private void InitializeComponent() {
//   // 
//   // WetScrubberControl
//   // 
//   this.Name = "WetScrubberControl";
//   //this.Size = new System.Drawing.Size(96, 72);
//}

//protected override void DoThePaint() {
//   //this.DrawBorder();
//   this.DrawSelection();
//   this.DrawSlots();
//   this.UpdateNameControlLocation();
//}

//StreamType streamType = StreamType.Unknown;
//if (ps is DryingGasStream) {
//   streamType = StreamType.Gas;
//}
//else if (ps is ProcessStream) {
//   streamType = StreamType.Process;
//}
//else if (ps is DryingMaterialStream) {
//   streamType = StreamType.Material;
//}

//sb.Append(GetVariableName(this.WetScrubber.GasPressureDrop, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.GasPressureDrop, us, nfs));
//if (this.WetScrubber.GasPressureDrop.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.CollectionEfficiency, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.CollectionEfficiency, us, nfs));
//if (this.WetScrubber.CollectionEfficiency.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.InletParticleLoading, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.InletParticleLoading, us, nfs));
//if (this.WetScrubber.InletParticleLoading.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.OutletParticleLoading, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.OutletParticleLoading, us, nfs));
//if (this.WetScrubber.OutletParticleLoading.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.ParticleCollectionRate, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.ParticleCollectionRate, us, nfs));
//if (this.WetScrubber.ParticleCollectionRate.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.MassFlowRateOfParticleLostToGasOutlet, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
//if (this.WetScrubber.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.WetScrubber.LiquidToGasRatio, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.WetScrubber.LiquidToGasRatio, us, nfs));
//if (this.WetScrubber.LiquidToGasRatio.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

