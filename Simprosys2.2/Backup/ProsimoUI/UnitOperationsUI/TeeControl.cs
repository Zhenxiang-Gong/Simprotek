using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Prosimo.UnitOperations;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for TeeControl.
   /// </summary>
   [Serializable]
   public class TeeControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation OUTLET_ORIENTATION = PointOrientation.E;

      public Tee Tee {
         get { return (Tee)this.solvable; }
         set { this.solvable = value; }
      }

      public TeeControl() {
      }

      public TeeControl(Flowsheet flowsheet, Point location, Tee tee)
         :
         base(flowsheet, location, tee) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.Tee.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // TeeControl
      //   // 
      //   this.Name = "TeeControl";
      //   //this.Size = new System.Drawing.Size(96, 72);
      //}

      public bool HitTestStreamIn(Point p) {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
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

      public bool HitTestStreamOut(Point p) {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
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
            if (HitTestStreamIn(p) || HitTestStreamOut(p)) {
               if (HitTestStreamOut(p))
                  idx = this.Tee.OutletStreams.Count + 1;
               else if (HitTestStreamIn(p))
                  idx = Tee.INLET_INDEX;

               if (this.Tee.CanAttach(idx)) {
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
               if (HitTestStreamOut(p))
                  idx = this.Tee.OutletStreams.Count + 1;
               else if (HitTestStreamIn(p))
                  idx = Tee.INLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Tee.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Tee.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as TeeControl);
      }

      //protected override void DoThePaint() {
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      protected override void DrawSlots() {
         int count = this.Tee.OutletStreams.Count;
         for (int i = 0; i < count; i++) {
            this.DrawSlot(this, this.GetStreamOutSlotPoint(i + 1, count), SlotPosition.Right, SlotDirection.Out);
         }
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new TeeEditor(this);
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
         if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.TEE_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.TEE_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.TEE_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.TEE_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public Point GetStreamInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetStreamInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;
         return p;
      }

      public Point GetStreamOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetStreamOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;
         return p;
      }

      public Point GetStreamOutConnectionPoint(int k, int n) {
         // this point is referenced to the flowsheet
         Point p1 = this.GetStreamOutSlotPoint(k, n);
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamOutSlotPoint(int k, int n) {
         // this point is referenced to this control
         // k out of n on the left side
         int empty = 6;

         Point p = new Point(this.Width, 0);
         int t = (this.Height - 2 * empty) / (n + 1);
         p.Y += empty + t * k;
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

         if (ad > 0) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetStreamOutConnectionPoint(ad, uo.OutletStreams.Count);
            PointOrientation uoOrientation = TeeControl.OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == Tee.INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Tee.INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetStreamInConnectionPoint();
            PointOrientation uoOrientation = TeeControl.INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }

         if (conn != null) {
            //this.flowsheet.ConnectionManager.Connections.Add(conn);
            //this.flowsheet.ConnectionManager.UpdateConnections(this);
            //this.flowsheet.ConnectionManager.DrawConnections();
            this.flowsheet.ConnectionManager.AddConnection(conn);
            //this.DoThePaint();
            this.flowsheet.ConnectionManager.UpdateConnections(this);
         }
         return conn;
      }

      protected override void DeleteConnection(UnitOperation uo, ProcessStreamBase ps) {
         this.flowsheet.ConnectionManager.RemoveUnitOpConnections(uo.Name);
         this.RecreateAllConnections();
         //this.DoThePaint();
      }

      private void RecreateAllConnections() {
         if (this.Tee.Inlet != null)
            this.CreateConnection(this.Tee, this.Tee.Inlet, Tee.INLET_INDEX);
         IEnumerator e = this.Tee.OutletStreams.GetEnumerator();
         int i = 1;
         while (e.MoveNext()) {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.CreateConnection(this.Tee, psb, i++);
         }
      }

      protected override void ShowConnectionPoints(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestStreamIn(p) || HitTestStreamOut(p)) {
               if (HitTestStreamOut(p))
                  idx = this.Tee.OutletStreams.Count + 1;
               else if (HitTestStreamIn(p))
                  idx = Tee.INLET_INDEX;

               if (this.Tee.CanAttach(idx))
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
               if (HitTestStreamOut(p))
                  idx = this.Tee.OutletStreams.Count + 1;
               else if (HitTestStreamIn(p))
                  idx = Tee.INLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Tee.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

      protected override void DrawSlot(Control ctrl, Point p, SlotPosition position, SlotDirection direction) {
         //Graphics g = ctrl.CreateGraphics();
         //SolidBrush brush = new SolidBrush(UI.SLOT_COLOR);
         //Pen pen = new Pen(brush, 0.5f);

         Point pN = new Point(0, 0);
         Point pS = new Point(0, 0);
         Point pW = new Point(0, 0);
         Point pE = new Point(0, 0);
         Point[] points = new Point[3];

         // this is the only situation possible for the tee
         if (position.Equals(SlotPosition.Right)) {
            if (direction == SlotDirection.Out) {
               pE = new Point(p.X - 1, p.Y);
               pS = new Point(pE.X - 3, pE.Y + 3);
               pW = new Point(pE.X - 11, pE.Y);
               pN = new Point(pE.X - 3, pE.Y - 3);

               points[0] = pN;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pW, pE);
            }
         }
         else if (position.Equals(SlotPosition.Left)) {
            if (direction == SlotDirection.In) {
               pW = p;
               pS = p;
               pN = p;
               pE = new Point(p.X + 5, p.Y);

               points[0] = pN;
               points[1] = pE;
               points[2] = pS;
               g.DrawLine(pen, pW, pE);
            }
         }
         g.FillPolygon(brush, points);

         //g.Dispose();
         //brush.Dispose();
         //pen.Dispose();
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Tee: ");
         sb.Append(this.Tee.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected TeeControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionTeeControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionTeeControl", TeeControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
