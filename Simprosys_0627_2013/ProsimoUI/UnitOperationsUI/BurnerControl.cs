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
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using ProsimoUI.ProcessStreamsUI;

namespace ProsimoUI.UnitOperationsUI {
   /// <summary>
   /// Summary description for ScrubberCondenserControl.
   /// </summary>
   [Serializable]
   public class BurnerControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation AIR_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation FUEL_INLET_ORIENTATION = PointOrientation.S;
      public static PointOrientation FLUE_GAS_OUTLET_ORIENTATION = PointOrientation.N;
      
      private static IDictionary<SolveState, Image> backgroundImageTable;

      internal protected override string SolvableTypeName
      {
         get { return "Burner"; }
      }

      public Burner Burner {
         get { return (Burner)this.solvable; }
         set { this.solvable = value; }
      }

      static BurnerControl() {
         backgroundImageTable = new Dictionary<SolveState, Image>();
         backgroundImageTable.Add(SolveState.NotSolved, UI.IMAGES.BURNER_CTRL_NOT_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolvedWithWarning, UI.IMAGES.BURNER_CTRL_SOLVED_WITH_WARNING_IMG);
         backgroundImageTable.Add(SolveState.Solved, UI.IMAGES.BURNER_CTRL_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolveFailed, UI.IMAGES.BURNER_CTRL_SOLVE_FAILED_IMG);
      }

      public BurnerControl() {
      }

      public BurnerControl(Flowsheet flowsheet, Point location, Burner burner)
         : base(flowsheet, location, burner) {
      }

      public bool HitTestAirIn(Point p) {
         bool hit = false;
         Point slot = this.GetAirInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public bool HitTestFlueGasOut(Point p) {
         bool hit = false;
         Point slot = this.GetFlueGasOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public bool HitTestFuelIn(Point p) {
         bool hit = false;
         Point slot = this.GetFuelInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }
      
      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestAirIn(p) || HitTestFlueGasOut(p) || HitTestFuelIn(p)) {
               if (HitTestAirIn(p))
                  idx = Burner.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = Burner.FUEL_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = Burner.FLUE_GAS_OUTLET_INDEX;

               if (this.Burner.CanAttach(idx)) {
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
               if (HitTestAirIn(p))
                  idx = Burner.AIR_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = Burner.FLUE_GAS_OUTLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = Burner.FUEL_INLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Burner.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Burner.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as BurnerControl);
      }

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetAirInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetFuelInSlotPoint(), SlotPosition.Down, SlotDirection.In);
         this.DrawSlot(this, this.GetFlueGasOutSlotPoint(), SlotPosition.Up, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new BurnerEditor(this);
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
         this.BackgroundImage = backgroundImageTable[this.solvable.SolveState];
      }

      public Point GetAirInConnectionPoint() {
         // this point is referenced to the control's middle of the left side
         Point p1 = this.GetAirInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetAirInSlotPoint() {
         // this point is referenced to the control's middle of the down side
         Point p = new Point(0, 3 * this.Height/4 - 3);
         return p;
      }

      public Point GetFlueGasOutConnectionPoint() {
         // this point is referenced to the control's middle of the up side
         Point p1 = this.GetFlueGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetFlueGasOutSlotPoint() {
         // this point is referenced to the control's middle of the up side
         Point p = new Point(this.Width / 2 - 1, 0);
         return p;
      }

      public Point GetFuelInConnectionPoint() {
         // this point is referenced to the control's lower left side
         Point p1 = this.GetFuelInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetFuelInSlotPoint() {
         // this point is referenced to the control's lower left side
         Point p = new Point(this.Width / 2 - 1, this.Height);
         return p;
      }
      
      public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad) {

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == Burner.AIR_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Burner.AIR_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetAirInConnectionPoint();
            PointOrientation uoOrientation = BurnerControl.AIR_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == Burner.FUEL_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Burner.FUEL_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetFuelInConnectionPoint();
            PointOrientation uoOrientation = BurnerControl.FUEL_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == Burner.FLUE_GAS_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Burner.FLUE_GAS_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetFlueGasOutConnectionPoint();
            PointOrientation uoOrientation = BurnerControl.FLUE_GAS_OUTLET_ORIENTATION;
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
            if (HitTestAirIn(p) || HitTestFlueGasOut(p) || HitTestFuelIn(p)) {
               if (HitTestAirIn(p))
                  idx = Burner.AIR_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = Burner.FUEL_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = Burner.FLUE_GAS_OUTLET_INDEX;

               if (this.Burner.CanAttach(idx))
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
               if (HitTestAirIn(p))
                  idx = Burner.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = Burner.FUEL_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = Burner.FLUE_GAS_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Burner.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

      protected BurnerControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionBurnerControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionBurnerControl", BurnerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//public override string ToPrintToolTip() {
//   return this.ToPrint();
//}

//public override string ToPrint() {
//   //UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
//   //string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
//   //StringBuilder sb = new StringBuilder();

//   //sb.Append("Combustor: ").Append(this.Burner.Name).Append("\r\n");
//   //sb.Append(UI.UNDERLINE).Append("\r\n");

//   //sb.Append(ToPrintVarList());

//   //return sb.ToString();
//   return ToPrintVarList("Combustor: ");
//}