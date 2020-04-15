using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ProsimoUI;
using Prosimo.UnitOperations;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for EjectorControl.
   /// </summary>
   [Serializable]
   public class EjectorControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation MOTIVE_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation SUCTION_INLET_ORIENTATION = PointOrientation.S;
      public static PointOrientation DISCHARGE_OUTLET_ORIENTATION = PointOrientation.E;

      public Ejector Ejector {
         get { return (Ejector)this.solvable; }
         set { this.solvable = value; }
      }

      public EjectorControl() {
      }

      public EjectorControl(Flowsheet flowsheet, Point location, Ejector ejector)
         : base(flowsheet, location, ejector) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.Ejector.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // EjectorControl
      //   // 
      //   this.Name = "EjectorControl";
      //   //this.Size = new System.Drawing.Size(96, 72);
      //}

      public bool HitTestMotiveIn(Point p) {
         bool hit = false;
         Point slot = this.GetMotiveInSlotPoint();
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

      public bool HitTestSuctionIn(Point p) {
         bool hit = false;
         Point slot = this.GetSuctionInSlotPoint();
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

      public bool HitTestDischargeOut(Point p) {
         bool hit = false;
         Point slot = this.GetDischargeOutSlotPoint();
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

      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestMotiveIn(p) || HitTestSuctionIn(p) || HitTestDischargeOut(p)) {
               if (HitTestMotiveIn(p))
                  idx = Ejector.MOTIVE_INLET_INDEX;
               else if (HitTestSuctionIn(p))
                  idx = Ejector.SUCTION_INLET_INDEX;
               else if (HitTestDischargeOut(p))
                  idx = Ejector.DISCHARGE_OUTLET_INDEX;

               if (this.Ejector.CanAttach(idx)) {
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
               if (HitTestMotiveIn(p))
                  idx = Ejector.MOTIVE_INLET_INDEX;
               else if (HitTestSuctionIn(p))
                  idx = Ejector.SUCTION_INLET_INDEX;
               else if (HitTestDischargeOut(p))
                  idx = Ejector.DISCHARGE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Ejector.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Ejector.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as EjectorControl);
      }

      //protected override void DoThePaint() {
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetMotiveInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetSuctionInSlotPoint(), SlotPosition.Down, SlotDirection.In);
         this.DrawSlot(this, this.GetDischargeOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new EjectorEditor(this);
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
         if (this.solvable.SolveState.Equals(SolveState.NotSolved))
            this.BackgroundImage = UI.IMAGES.EJECTOR_CTRL_NOT_SOLVED_IMG;
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            this.BackgroundImage = UI.IMAGES.EJECTOR_CTRL_SOLVED_WITH_WARNING_IMG;
         else if (this.solvable.SolveState.Equals(SolveState.Solved))
            this.BackgroundImage = UI.IMAGES.EJECTOR_CTRL_SOLVED_IMG;
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed))
            this.BackgroundImage = UI.IMAGES.EJECTOR_CTRL_SOLVE_FAILED_IMG;
      }

      public Point GetMotiveInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetMotiveInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetMotiveInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;

         return p;
      }

      public Point GetSuctionInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetSuctionInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetSuctionInSlotPoint() {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0, 0);
         p.X += this.Width / 4 + this.Width / 16 + 1;
         p.Y += this.Height;
         return p;
      }

      public Point GetDischargeOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetDischargeOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetDischargeOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;
         p.X += this.Width;
         return p;
      }

      public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad) {
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

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == Ejector.MOTIVE_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Ejector.MOTIVE_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetMotiveInConnectionPoint();
            PointOrientation uoOrientation = EjectorControl.MOTIVE_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == Ejector.SUCTION_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Ejector.SUCTION_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetSuctionInConnectionPoint();
            PointOrientation uoOrientation = EjectorControl.SUCTION_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == Ejector.DISCHARGE_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Ejector.DISCHARGE_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetDischargeOutConnectionPoint();
            PointOrientation uoOrientation = EjectorControl.DISCHARGE_OUTLET_ORIENTATION;
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
            if (HitTestMotiveIn(p) || HitTestSuctionIn(p) || HitTestDischargeOut(p)) {
               if (HitTestMotiveIn(p))
                  idx = Ejector.MOTIVE_INLET_INDEX;
               else if (HitTestSuctionIn(p))
                  idx = Ejector.SUCTION_INLET_INDEX;
               else if (HitTestDischargeOut(p))
                  idx = Ejector.DISCHARGE_OUTLET_INDEX;

               if (this.Ejector.CanAttach(idx))
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
               if (HitTestMotiveIn(p))
                  idx = Ejector.MOTIVE_INLET_INDEX;
               else if (HitTestSuctionIn(p))
                  idx = Ejector.SUCTION_INLET_INDEX;
               else if (HitTestDischargeOut(p))
                  idx = Ejector.DISCHARGE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Ejector.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Ejector: ");
         sb.Append(this.Ejector.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(ToPrintVarList());

         return sb.ToString();
      }

      protected EjectorControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionEjectorControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionEjectorControl", EjectorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//sb.Append(GetVariableName(this.Ejector.EntrainmentRatio, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.Ejector.EntrainmentRatio, us, nfs));
//if (this.Ejector.EntrainmentRatio.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.Ejector.CompressionRatio, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.Ejector.CompressionRatio, us, nfs));
//if (this.Ejector.CompressionRatio.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.Ejector.SuctionMotivePressureRatio, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.Ejector.SuctionMotivePressureRatio, us, nfs));
//if (this.Ejector.SuctionMotivePressureRatio.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");
