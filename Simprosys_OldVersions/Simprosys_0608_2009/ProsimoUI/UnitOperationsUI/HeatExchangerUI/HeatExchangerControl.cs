using System;
using System.Collections;
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
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI {
   /// <summary>
   /// Summary description for HeatExchangerControl.
   /// </summary>
   [Serializable]
   public class HeatExchangerControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation COLD_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation COLD_OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation HOT_INLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation HOT_OUTLET_ORIENTATION = PointOrientation.S;

      public HeatExchanger HeatExchanger {
         get { return (HeatExchanger)this.solvable; }
         set { this.solvable = value; }
      }

      public HeatExchangerControl() {
      }

      public HeatExchangerControl(Flowsheet flowsheet, Point location, HeatExchanger heatExchanger)
         : base(flowsheet, location, heatExchanger) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.HeatExchanger.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // HeatExchangerControl
      //   // 
      //   this.Name = "HeatExchangerControl";
      //   //this.Size = new System.Drawing.Size(96, 72);
      //}

      public bool HitTestColdIn(Point p) {
         bool hit = false;
         Point slot = this.GetColdInSlotPoint();
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

      public bool HitTestColdOut(Point p) {
         bool hit = false;
         Point slot = this.GetColdOutSlotPoint();
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

      public bool HitTestHotIn(Point p) {
         bool hit = false;
         Point slot = this.GetHotInSlotPoint();
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

      public bool HitTestHotOut(Point p) {
         bool hit = false;
         Point slot = this.GetHotOutSlotPoint();
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
            if (HitTestColdIn(p) || HitTestColdOut(p) || HitTestHotIn(p) || HitTestHotOut(p)) {
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               if (this.HeatExchanger.CanAttach(idx)) {
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
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.HeatExchanger.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.HeatExchanger.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as HeatExchangerControl);
      }

      //protected override void DoThePaint() {
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetColdInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetColdOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetHotInSlotPoint(), SlotPosition.Up, SlotDirection.In);
         this.DrawSlot(this, this.GetHotOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new HeatExchangerEditor(this);
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
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public Point GetColdInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetColdInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetColdInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;
         return p;
      }

      public Point GetColdOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetColdOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetColdOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;
         return p;
      }

      public Point GetHotInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetHotInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHotInSlotPoint() {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0, 0);
         p.X += this.Width / 2;
         return p;
      }

      public Point GetHotOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetHotOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHotOutSlotPoint() {
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

         if (ad == HeatExchanger.COLD_SIDE_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.COLD_SIDE_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetColdInConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.COLD_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == HeatExchanger.COLD_SIDE_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetColdOutConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.COLD_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == HeatExchanger.HOT_SIDE_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.HOT_SIDE_INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetHotInConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.HOT_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == HeatExchanger.HOT_SIDE_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetHotOutConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.HOT_OUTLET_ORIENTATION;
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
            if (HitTestColdIn(p) || HitTestColdOut(p) || HitTestHotIn(p) || HitTestHotOut(p)) {
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               if (this.HeatExchanger.CanAttach(idx))
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
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.HeatExchanger.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Heat Exchanger: ");
         sb.Append(this.HeatExchanger.Name);
         sb.Append(UI.NEW_LINE);
         sb.Append(UI.UNDERLINE);
         sb.Append(UI.NEW_LINE);

         sb.Append(ToPrintVarList());

         if (this.HeatExchanger.CurrentRatingModel != null) {
            if (this.HeatExchanger.CurrentRatingModel is HXRatingModelSimpleGeneric) {
               sb.Append(this.ToPrintSimpleGenericRating());
            }
            else if (this.HeatExchanger.CurrentRatingModel is HXRatingModelShellAndTube) {
               sb.Append(this.ToPrintShellAndTubeRating());
            }
            else if (this.HeatExchanger.CurrentRatingModel is HXRatingModelPlateAndFrame) {
               sb.Append(this.ToPrintPlateAndFrameRating());
            }
         }

         return sb.ToString();
      }

      private string ToPrintSimpleGenericRating() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelSimpleGeneric ratingModel =
            this.HeatExchanger.CurrentRatingModel as HXRatingModelSimpleGeneric;

         sb.Append("Simple Generic Rating:");
         sb.Append(UI.NEW_LINE);

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.IncludeWallEffect) {
            sb.Append(GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         return sb.ToString();
      }

      private string ToPrintShellAndTubeRating() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelShellAndTube ratingModel =
            this.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;

         sb.Append("Shell and Tube Rating:");
         sb.Append(UI.NEW_LINE);

         // the left side

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.FtFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.FtFactor, us, nfs));
         if (ratingModel.FtFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubeSideVelocity, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubeSideVelocity, us, nfs));
         if (ratingModel.TubeSideVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideRe, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideRe, us, nfs));
         if (ratingModel.ColdSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ShellSideVelocity, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ShellSideVelocity, us, nfs));
         if (ratingModel.ShellSideVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideRe, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideRe, us, nfs));
         if (ratingModel.HotSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.IncludeWallEffect) {
            sb.Append(GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         // the right side
         string ratingTypeStr = "";
         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware)
            ratingTypeStr = "Bell Delaware";
         else if (ratingModel.ShellRatingType == ShellRatingType.Donohue)
            ratingTypeStr = "Donohue";
         else if (ratingModel.ShellRatingType == ShellRatingType.Kern)
            ratingTypeStr = "Kern";
         sb.Append("Shell Rating Type");
         sb.Append(" = ");
         sb.Append(ratingTypeStr);
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubeOuterDiameter, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubeOuterDiameter, us, nfs));
         if (ratingModel.TubeOuterDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubeWallThickness, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubeWallThickness, us, nfs));
         if (ratingModel.TubeWallThickness.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubeInnerDiameter, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubeInnerDiameter, us, nfs));
         if (ratingModel.TubeInnerDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubeLengthBetweenTubeSheets, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubeLengthBetweenTubeSheets, us, nfs));
         if (ratingModel.TubeLengthBetweenTubeSheets.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubesPerTubePass, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubesPerTubePass, us, nfs));
         if (ratingModel.TubesPerTubePass.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TubePassesPerShellPass, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubePassesPerShellPass, us, nfs));
         if (ratingModel.TubePassesPerShellPass.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ShellPasses, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ShellPasses, us, nfs));
         if (ratingModel.ShellPasses.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalTubesInShell, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalTubesInShell, us, nfs));
         if (ratingModel.TotalTubesInShell.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         string shellTypeStr = "";
         if (ratingModel.ShellType == ShellType.E)
            shellTypeStr = "E";
         else if (ratingModel.ShellType == ShellType.F)
            shellTypeStr = "F";
         else if (ratingModel.ShellType == ShellType.G)
            shellTypeStr = "G";
         else if (ratingModel.ShellType == ShellType.H)
            shellTypeStr = "H";
         else if (ratingModel.ShellType == ShellType.J)
            shellTypeStr = "J";
         else if (ratingModel.ShellType == ShellType.K)
            shellTypeStr = "K";
         else if (ratingModel.ShellType == ShellType.X)
            shellTypeStr = "X";
         sb.Append("Shell Type");
         sb.Append(" = ");
         sb.Append(shellTypeStr);
         sb.Append(UI.NEW_LINE);

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware ||
            ratingModel.ShellRatingType == ShellRatingType.Kern) {
            string tubeLayoutStr = "";
            if (ratingModel.TubeLayout == TubeLayout.InlineSquare)
               tubeLayoutStr = "Inline Square";
            else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare)
               tubeLayoutStr = "Rotated Square";
            else if (ratingModel.TubeLayout == TubeLayout.Triangular)
               tubeLayoutStr = "Triangular";
            sb.Append("Tube Layout");
            sb.Append(" = ");
            sb.Append(tubeLayoutStr);
            sb.Append(UI.NEW_LINE);
         }

         sb.Append(GetVariableName(ratingModel.TubePitch, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TubePitch, us, nfs));
         if (ratingModel.TubePitch.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware ||
            ratingModel.ShellRatingType == ShellRatingType.Donohue) {
            sb.Append(GetVariableName(ratingModel.BaffleCut, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.BaffleCut, us, nfs));
            if (ratingModel.BaffleCut.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         sb.Append(GetVariableName(ratingModel.BaffleSpacing, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.BaffleSpacing, us, nfs));
         if (ratingModel.BaffleSpacing.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware) {
            sb.Append(GetVariableName(ratingModel.EntranceBaffleSpacing, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.EntranceBaffleSpacing, us, nfs));
            if (ratingModel.EntranceBaffleSpacing.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.ExitBaffleSpacing, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.ExitBaffleSpacing, us, nfs));
            if (ratingModel.ExitBaffleSpacing.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         sb.Append(GetVariableName(ratingModel.NumberOfBaffles, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.NumberOfBaffles, us, nfs));
         if (ratingModel.NumberOfBaffles.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ShellInnerDiameter, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ShellInnerDiameter, us, nfs));
         if (ratingModel.ShellInnerDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware) {
            sb.Append(GetVariableName(ratingModel.ShellToBaffleDiametralClearance, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.ShellToBaffleDiametralClearance, us, nfs));
            if (ratingModel.ShellToBaffleDiametralClearance.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware) {
            sb.Append(GetVariableName(ratingModel.BundleToShellDiametralClearance, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.BundleToShellDiametralClearance, us, nfs));
            if (ratingModel.BundleToShellDiametralClearance.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.SealingStrips, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.SealingStrips, us, nfs));
            if (ratingModel.SealingStrips.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         if (ratingModel.IncludeNozzleEffect) {
            sb.Append(GetVariableName(ratingModel.TubeSideEntranceNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.TubeSideEntranceNozzleDiameter, us, nfs));
            if (ratingModel.TubeSideEntranceNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.TubeSideExitNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.TubeSideExitNozzleDiameter, us, nfs));
            if (ratingModel.TubeSideExitNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.ShellSideEntranceNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.ShellSideEntranceNozzleDiameter, us, nfs));
            if (ratingModel.ShellSideEntranceNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.ShellSideExitNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.ShellSideExitNozzleDiameter, us, nfs));
            if (ratingModel.ShellSideExitNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         return sb.ToString();
      }

      private string ToPrintPlateAndFrameRating() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelPlateAndFrame ratingModel =
            this.HeatExchanger.CurrentRatingModel as HXRatingModelPlateAndFrame;

         sb.Append("Plate And Frame Rating:");
         sb.Append(UI.NEW_LINE);

         // the left side

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideRe, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideRe, us, nfs));
         if (ratingModel.ColdSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideRe, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideRe, us, nfs));
         if (ratingModel.HotSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         if (ratingModel.IncludeWallEffect) {
            sb.Append(GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);

            sb.Append(GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append(UI.NEW_LINE);
         }

         // the right side

         sb.Append(GetVariableName(ratingModel.ChannelWidth, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ChannelWidth, us, nfs));
         if (ratingModel.ChannelWidth.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ProjectedChannelLength, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ProjectedChannelLength, us, nfs));
         if (ratingModel.ProjectedChannelLength.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.EnlargementFactor, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.EnlargementFactor, us, nfs));
         if (ratingModel.EnlargementFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ProjectedPlateArea, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ProjectedPlateArea, us, nfs));
         if (ratingModel.ProjectedPlateArea.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ActualEffectivePlateArea, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ActualEffectivePlateArea, us, nfs));
         if (ratingModel.ActualEffectivePlateArea.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.PlateWallThickness, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.PlateWallThickness, us, nfs));
         if (ratingModel.PlateWallThickness.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.PlatePitch, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.PlatePitch, us, nfs));
         if (ratingModel.PlatePitch.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.NumberOfPlates, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.NumberOfPlates, us, nfs));
         if (ratingModel.NumberOfPlates.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.ColdSidePasses, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.ColdSidePasses, us, nfs));
         if (ratingModel.ColdSidePasses.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HotSidePasses, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HotSidePasses, us, nfs));
         if (ratingModel.HotSidePasses.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.PortDiameter, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.PortDiameter, us, nfs));
         if (ratingModel.PortDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.HorizontalPortDistance, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.HorizontalPortDistance, us, nfs));
         if (ratingModel.HorizontalPortDistance.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.VerticalPortDistance, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.VerticalPortDistance, us, nfs));
         if (ratingModel.VerticalPortDistance.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         sb.Append(GetVariableName(ratingModel.CompressedPlatePackLength, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(ratingModel.CompressedPlatePackLength, us, nfs));
         if (ratingModel.CompressedPlatePackLength.IsSpecified)
            sb.Append(" * ");
         sb.Append(UI.NEW_LINE);

         return sb.ToString();
      }

      protected HeatExchangerControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionHeatExchangerControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionHeatExchangerControl", HeatExchangerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

   //sb.Append(GetVariableName(this.HeatExchanger.TotalHeatTransfer, us));
   //sb.Append(" = ");
   //sb.Append(GetVariableValue(this.HeatExchanger.TotalHeatTransfer, us, nfs));
   //if (this.HeatExchanger.TotalHeatTransfer.IsSpecified)
   //   sb.Append(" * ");
   //sb.Append(UI.NEW_LINE);

   //sb.Append(GetVariableName(this.HeatExchanger.ColdSidePressureDrop, us));
   //sb.Append(" = ");
   //sb.Append(GetVariableValue(this.HeatExchanger.ColdSidePressureDrop, us, nfs));
   //if (this.HeatExchanger.ColdSidePressureDrop.IsSpecified)
   //   sb.Append(" * ");
   //sb.Append(UI.NEW_LINE);

   //sb.Append(GetVariableName(this.HeatExchanger.HotSidePressureDrop, us));
   //sb.Append(" = ");
   //sb.Append(GetVariableValue(this.HeatExchanger.HotSidePressureDrop, us, nfs));
   //if (this.HeatExchanger.HotSidePressureDrop.IsSpecified)
   //   sb.Append(" * ");
   //sb.Append(UI.NEW_LINE);

