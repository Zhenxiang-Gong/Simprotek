using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.VaporLiquidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for FlashTankControl.
   /// </summary>
   [Serializable]
   public class FlashTankControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation VAPOR_OUTLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation LIQUID_OUTLET_ORIENTATION = PointOrientation.S;

      public FlashTank FlashTank {
         get { return (FlashTank)this.Solvable; }
         set { this.Solvable = value; }
      }

      public FlashTankControl() {
      }

      public FlashTankControl(Flowsheet flowsheet, Point location, FlashTank flashTank)
         :
         base(flowsheet, location, flashTank) {
         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.FlashTank.SolveState);
         this.UpdateBackImage();
      }

      private void InitializeComponent() {
         // 
         // FlashTankControl
         // 
         this.Name = "FlashTankControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestIn(Point p) {
         bool hit = false;
         Point slot = this.GetInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         Pen pen = new Pen(Color.Black, 20);
         Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, pen, g);
         
         pen.Dispose();
         g.Dispose();
         gp.Dispose();

         return hit;
      }

      public bool HitTestVaporOut(Point p) {
         bool hit = false;
         Point slot = this.GetVaporOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         Pen pen = new Pen(Color.Black, 20);
         Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, pen, g);

         pen.Dispose();
         g.Dispose();
         gp.Dispose();

         return hit;
      }

      public bool HitTestLiquidOut(Point p) {
         bool hit = false;
         Point slot = this.GetLiquidOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         Pen pen = new Pen(Color.Black, 20);
         Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, pen, g);

         pen.Dispose();
         g.Dispose();
         gp.Dispose();

         return hit;
      }

      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestIn(p) || HitTestVaporOut(p) || HitTestLiquidOut(p)) {
               if (HitTestIn(p))
                  idx = FlashTank.INLET_INDEX;
               else if (HitTestVaporOut(p))
                  idx = FlashTank.VAPOR_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = FlashTank.LIQUID_OUTLET_INDEX;

               if (this.FlashTank.CanConnect(idx)) {
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
               if (HitTestIn(p))
                  idx = FlashTank.INLET_INDEX;
               else if (HitTestVaporOut(p))
                  idx = FlashTank.VAPOR_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = FlashTank.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.FlashTank.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.FlashTank.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as FlashTankControl);
      }

      //protected override void DoThePaint() {
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetVaporOutSlotPoint(), SlotPosition.Up, SlotDirection.Out);
         this.DrawSlot(this, this.GetLiquidOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.Editor == null) {
            this.Editor = new FlashTankEditor(this);
            this.Editor.Owner = (Form)this.flowsheet.Parent;
            this.Editor.Show();
         }
         else {
            if (this.Editor.WindowState.Equals(FormWindowState.Minimized))
               this.Editor.WindowState = FormWindowState.Normal;
            this.Editor.Activate();
         }
      }

      protected override void UpdateBackImage() {
         if (this.Solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.FLASHTANK_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.FLASHTANK_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.FLASHTANK_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.FLASHTANK_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public Point GetInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;
         return p;
      }

      public Point GetVaporOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetVaporOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetVaporOutSlotPoint() {
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
         StreamType streamType = StreamType.Unknown;
         if (ps is DryingGasStream) {
            streamType = StreamType.Gas;
         }
         else if (ps is ProcessStream) {
            streamType = StreamType.Process;
         }
         else if (ps is DryingMaterialStream) {
            streamType = StreamType.Material;
         }

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == FlashTank.INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = FlashTank.INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetInConnectionPoint();
            PointOrientation uoOrientation = FlashTankControl.INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == FlashTank.VAPOR_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = FlashTank.VAPOR_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetVaporOutConnectionPoint();
            PointOrientation uoOrientation = FlashTankControl.VAPOR_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == FlashTank.LIQUID_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = FlashTank.LIQUID_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetLiquidOutConnectionPoint();
            PointOrientation uoOrientation = FlashTankControl.LIQUID_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
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
            if (HitTestIn(p) || HitTestVaporOut(p) || HitTestLiquidOut(p)) {
               if (HitTestIn(p))
                  idx = FlashTank.INLET_INDEX;
               else if (HitTestVaporOut(p))
                  idx = FlashTank.VAPOR_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = FlashTank.LIQUID_OUTLET_INDEX;

               if (this.FlashTank.CanConnect(idx))
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
               if (HitTestIn(p))
                  idx = FlashTank.INLET_INDEX;
               else if (HitTestVaporOut(p))
                  idx = FlashTank.VAPOR_OUTLET_INDEX;
               else if (HitTestLiquidOut(p))
                  idx = FlashTank.LIQUID_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.FlashTank.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Flash Tank: ");
         sb.Append(this.FlashTank.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected FlashTankControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context) {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFlashTankControl", typeof(int));
         switch (persistedClassVersion) {
            case 1:
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFlashTankControl", FlashTankControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
