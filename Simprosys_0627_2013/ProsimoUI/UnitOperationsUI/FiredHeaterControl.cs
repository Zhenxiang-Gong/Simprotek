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
   public class FiredHeaterControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation AIR_INLET_ORIENTATION = PointOrientation.S;
      public static PointOrientation FUEL_INLET_ORIENTATION = PointOrientation.S;
      public static PointOrientation FLUE_GAS_OUTLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation HEATED_INLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation HEATED_OUTLET_ORIENTATION = PointOrientation.W;
    
      private static IDictionary<SolveState, Image> backgroundImageTable;

      internal protected override string SolvableTypeName
      {
         get { return "Fired Heater"; }
      }

      public FiredHeater FiredHeater {
         get { return (FiredHeater)this.solvable; }
         set { this.solvable = value; }
      }

      static FiredHeaterControl() {
         backgroundImageTable = new Dictionary<SolveState, Image>();
         backgroundImageTable.Add(SolveState.NotSolved, UI.IMAGES.FIREDHEATER_CTRL_NOT_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolvedWithWarning, UI.IMAGES.FIREDHEATER_CTRL_SOLVED_WITH_WARNING_IMG);
         backgroundImageTable.Add(SolveState.Solved, UI.IMAGES.FIREDHEATER_CTRL_SOLVED_IMG);
         backgroundImageTable.Add(SolveState.SolveFailed, UI.IMAGES.FIREDHEATER_CTRL_SOLVE_FAILED_IMG);
      }

      public FiredHeaterControl() {
      }

      public FiredHeaterControl(Flowsheet flowsheet, Point location, FiredHeater firedHeater)
         : base(flowsheet, location, firedHeater) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.ScrubberCondenser.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // ScrubberCondenserControl
      //   // 
      //   this.Name = "ScrubberCondenserControl";
      //   //this.Size = new System.Drawing.Size(96, 72);
      //}

      public bool HitTestAirIn(Point p) {
         bool hit = false;
         Point slot = this.GetAirInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public bool HitTestFuelIn(Point p) {
         bool hit = false;
         Point slot = this.GetFuelInSlotPoint();
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
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, penBlack20, g);
         gp.Dispose();

         return hit;
      }

      public bool HitTestHeatedIn(Point p) {
         bool hit = false;
         Point slot = this.GetHeatedInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public bool HitTestHeatedOut(Point p) {
         bool hit = false;
         Point slot = this.GetHeatedOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }
      
      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestAirIn(p) || HitTestFuelIn(p) || HitTestFlueGasOut(p) || HitTestHeatedIn(p) || HitTestHeatedOut(p)) {
               if (HitTestAirIn(p))
                  idx = FiredHeater.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = FiredHeater.FUEL_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = FiredHeater.FLUE_GAS_OUTLET_INDEX;
               else if (HitTestHeatedIn(p))
                  idx = FiredHeater.HEATED_INLET_INDEX;
               else if (HitTestHeatedOut(p))
                  idx = FiredHeater.HEATED_OUTLET_INDEX;

               if (this.FiredHeater.CanAttach(idx)) {
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
                  idx = FiredHeater.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = FiredHeater.FUEL_INLET_INDEX;
               else if (HitTestFlueGasOut(p))
                  idx = FiredHeater.FLUE_GAS_OUTLET_INDEX;
               else if (HitTestHeatedIn(p))
                  idx = FiredHeater.HEATED_INLET_INDEX;
               else if (HitTestHeatedOut(p))
                  idx = FiredHeater.HEATED_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.FiredHeater.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.FiredHeater.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as FiredHeaterControl);
      }

      //protected override void DoThePaint()
      //{
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetAirInSlotPoint(), SlotPosition.Down, SlotDirection.In);
         this.DrawSlot(this, this.GetFuelInSlotPoint(), SlotPosition.Down, SlotDirection.In);
         this.DrawSlot(this, this.GetFlueGasOutSlotPoint(), SlotPosition.Up, SlotDirection.Out);
         this.DrawSlot(this, this.GetHeatedInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetHeatedOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new FiredHeaterEditor(this);
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
         //   this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_NOT_SOLVED_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
         //   this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVED_WITH_WARNING_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
         //   this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVED_IMG;
         //}
         //else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
         //   this.BackgroundImage = UI.IMAGES.SCRUBBERCONDENSER_CTRL_SOLVE_FAILED_IMG;
         //}
         this.BackgroundImage = backgroundImageTable[this.solvable.SolveState];
      }

      public Point GetAirInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetAirInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetAirInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2 + this.Height / 6 - 1;
         return p;
      }

      public Point GetFuelInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetFuelInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetFuelInSlotPoint() {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0, 0);
         //p.X += this.Width / 2;
         p.Y = this.Height / 2 - this.Height / 4;
         return p;
      }

      public Point GetFlueGasOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetFlueGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetFlueGasOutSlotPoint() {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0, 0);
         p.Y += this.Height;
         p.X += this.Width / 2 + 7;
         return p;
      }

      public Point GetHeatedInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetHeatedInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHeatedInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.X = this.Width;
         p.Y += this.Height / 2 - 4;
         return p;
      }

      public Point GetHeatedOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetHeatedOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHeatedOutSlotPoint() {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0, 0);
         p.X = this.Width;
         p.Y += this.Height / 2 + 3;
         return p;
      }
      
      public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad) {

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad == FiredHeater.AIR_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetAirInConnectionPoint();
            PointOrientation uoOrientation = FiredHeaterControl.AIR_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == FiredHeater.FUEL_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetFuelInConnectionPoint();
            PointOrientation uoOrientation = FiredHeaterControl.FUEL_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == FiredHeater.FLUE_GAS_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetFlueGasOutConnectionPoint();
            PointOrientation uoOrientation = FiredHeaterControl.FLUE_GAS_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == FiredHeater.HEATED_INLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetHeatedInConnectionPoint();
            PointOrientation uoOrientation = FiredHeaterControl.HEATED_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == FiredHeater.HEATED_OUTLET_INDEX) {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetHeatedOutConnectionPoint();
            PointOrientation uoOrientation = FiredHeaterControl.HEATED_OUTLET_ORIENTATION;
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
            if (HitTestAirIn(p) || HitTestFuelIn(p) || HitTestFlueGasOut(p) || HitTestHeatedIn(p) || HitTestHeatedOut(p)) {
               if (HitTestAirIn(p))
                  idx = FiredHeater.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = FiredHeater.FUEL_INLET_INDEX;
               else if (HitTestHeatedIn(p))
                  idx = FiredHeater.HEATED_INLET_INDEX;
               else if (HitTestHeatedOut(p))
                  idx = FiredHeater.HEATED_OUTLET_INDEX;

               if (this.FiredHeater.CanAttach(idx))
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
                  idx = FiredHeater.AIR_INLET_INDEX;
               else if (HitTestFuelIn(p))
                  idx = FiredHeater.FUEL_INLET_INDEX;
               else if (HitTestHeatedIn(p))
                  idx = FiredHeater.HEATED_INLET_INDEX;
               else if (HitTestHeatedOut(p))
                  idx = FiredHeater.HEATED_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.FiredHeater.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

      protected FiredHeaterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionFiredHeaterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFiredHeaterControl", FiredHeaterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
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

//   //sb.Append("Fired Heater: ").Append(this.FiredHeater.Name).Append("\r\n");
//   //sb.Append(UI.UNDERLINE).Append("\r\n");

//   //sb.Append(ToPrintVarList());

//   //return sb.ToString();
//   return ToPrintVarList("Fired Heater: ");
//}

//sb.Append(GetVariableName(this.ScrubberCondenser.GasPressureDrop, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.GasPressureDrop, us, nfs));
//if (this.ScrubberCondenser.GasPressureDrop.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.CollectionEfficiency, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.CollectionEfficiency, us, nfs));
//if (this.ScrubberCondenser.CollectionEfficiency.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.InletParticleLoading, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.InletParticleLoading, us, nfs));
//if (this.ScrubberCondenser.InletParticleLoading.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.OutletParticleLoading, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.OutletParticleLoading, us, nfs));
//if (this.ScrubberCondenser.OutletParticleLoading.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.ParticleCollectionRate, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.ParticleCollectionRate, us, nfs));
//if (this.ScrubberCondenser.ParticleCollectionRate.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
//if (this.ScrubberCondenser.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.CoolingDuty, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.CoolingDuty, us, nfs));
//if (this.ScrubberCondenser.CoolingDuty.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.LiquidToGasRatio, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.LiquidToGasRatio, us, nfs));
//if (this.ScrubberCondenser.LiquidToGasRatio.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.LiquidRecirculationMassFlowRate, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.LiquidRecirculationMassFlowRate, us, nfs));
//if (this.ScrubberCondenser.LiquidRecirculationMassFlowRate.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate, us, nfs));
//if (this.ScrubberCondenser.LiquidRecirculationVolumeFlowRate.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");
