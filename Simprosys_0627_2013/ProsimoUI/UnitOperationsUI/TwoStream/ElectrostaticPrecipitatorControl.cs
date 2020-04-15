using System;
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

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorControl.
   /// </summary>
   [Serializable]
   public class ElectrostaticPrecipitatorControl : TwoStreamUnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation PARTICLE_OUTLET_ORIENTATION = PointOrientation.S;

      internal protected override string SolvableTypeName
      {
         get { return "Electrostatic Precipitator"; }
      }

      public ElectrostaticPrecipitator ElectrostaticPrecipitator {
         get { return (ElectrostaticPrecipitator)this.solvable; }
         set { this.solvable = value; }
      }

      public ElectrostaticPrecipitatorControl() {
      }

      public ElectrostaticPrecipitatorControl(Flowsheet flowsheet, Point location, ElectrostaticPrecipitator electrostaticPrecipitator)
         : base(flowsheet, location, electrostaticPrecipitator) {
      }

      public override bool HitTestStreamIn(Point p) {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public override bool HitTestStreamOut(Point p) {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public bool HitTestParticleOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetParticleOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();

         return hit;
      }

      public Point GetParticleOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetParticleOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetParticleOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0, 0);
         p.Y += this.Height;
         p.X += this.Width / 2;
         return p;
      }

      protected override void MouseDownHandler(Point p)
      {
         if(this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if(HitTestStreamIn(p) || HitTestStreamOut(p) || HitTestParticleOut(p))
            {
               if(HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if(HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;
               else if(HitTestParticleOut(p))
                  idx = BagFilter.PARTICLE_OUTLET_INDEX;

               if(this.UnitOperation.CanAttach(idx))
               {
                  // ok for the second step
                  this.flowsheet.firstStepCtrl = this;
                  this.flowsheet.SetFlowsheetActivity(FlowsheetActivity.AddingConnStepTwo);
                  this.flowsheet.attachIndex = idx;
               }
            }
            else
            {
               this.flowsheet.ResetActivity();
            }
         }
         else if(this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if(this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if(HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if(HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;
               else if(HitTestParticleOut(p))
                  idx = BagFilter.PARTICLE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if(this.UnitOperation.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.UnitOperation.AttachStream(ctrl.ProcessStreamBase, idx);
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

      public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad)
      {
         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if(ad == TwoStreamUnitOperation.INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = TwoStreamUnitOperation.INLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetStreamInConnectionPoint();
            PointOrientation uoOrientation = TwoStreamUnitOpControl.INLET_ORIENTATION;
            
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if(ad == TwoStreamUnitOperation.OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = TwoStreamUnitOperation.OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetStreamOutConnectionPoint();
            PointOrientation uoOrientation = TwoStreamUnitOpControl.OUTLET_ORIENTATION;

            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if(ad == Cyclone.PARTICLE_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Cyclone.PARTICLE_OUTLET_INDEX;
            string uoName = this.solvable.Name;
            Point uoPoint = this.GetParticleOutConnectionPoint();
            PointOrientation uoOrientation = PARTICLE_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }

         if(conn != null)
         {
            this.flowsheet.ConnectionManager.AddConnection(conn);
         }
         return conn;
      }

      protected override void ShowConnectionPoints(Point p)
      {
         if(this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if(HitTestStreamIn(p) || HitTestStreamOut(p) || HitTestParticleOut(p))
            {
               if(HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if(HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;
               else if(HitTestParticleOut(p))
                  idx = BagFilter.PARTICLE_OUTLET_INDEX;

               if(this.UnitOperation.CanAttach(idx))
                  this.Cursor = Cursors.Cross;
               else
                  this.Cursor = Cursors.Default;
            }
            else
            {
               this.Cursor = Cursors.Default;
            }
         }
         else if(this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if(this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if(HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if(HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;
               else if(HitTestParticleOut(p))
                  idx = BagFilter.PARTICLE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if(this.UnitOperation.CanAttachStream(ctrl.ProcessStreamBase, idx))
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
 
      protected override void DrawSlots()
      {
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetParticleOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e)
      {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as ElectrostaticPrecipitatorControl);
      }
      
      public override void Edit()
      {
         if (this.editor == null) {
            this.editor = new ElectrostaticPrecipitatorEditor(this);
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
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVE_FAILED_IMG;
         }
      }

      protected ElectrostaticPrecipitatorControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionElectrostaticPrecipitatorControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionElectrostaticPrecipitatorControl", ElectrostaticPrecipitatorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
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

//   //sb.Append("Electrostatic Precipitator: ");
//   //sb.Append(this.ElectrostaticPrecipitator.Name);
//   //sb.Append("\r\n");
//   //sb.Append(UI.UNDERLINE);
//   //sb.Append("\r\n");

//   //sb.Append(ToPrintVarList());

//   //return sb.ToString();
//   return ToPrintVarList("Electrostatic Precipitator: ");
//}

//sb.Append(GetVariableName(this.ElectrostaticPrecipitator.GasPressureDrop, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.GasPressureDrop, us, nfs));
         //if (this.ElectrostaticPrecipitator.GasPressureDrop.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.CollectionEfficiency, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.CollectionEfficiency, us, nfs));
         //if (this.ElectrostaticPrecipitator.CollectionEfficiency.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.InletParticleLoading, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.InletParticleLoading, us, nfs));
         //if (this.ElectrostaticPrecipitator.InletParticleLoading.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.OutletParticleLoading, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.OutletParticleLoading, us, nfs));
         //if (this.ElectrostaticPrecipitator.OutletParticleLoading.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.ParticleCollectionRate, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.ParticleCollectionRate, us, nfs));
         //if (this.ElectrostaticPrecipitator.ParticleCollectionRate.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         //if (this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.DriftVelocity, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.DriftVelocity, us, nfs));
         //if (this.ElectrostaticPrecipitator.DriftVelocity.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.TotalSurfaceArea, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.TotalSurfaceArea, us, nfs));
         //if (this.ElectrostaticPrecipitator.TotalSurfaceArea.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

//protected override void DoThePaint()
//{
//   //this.DrawBorder();
//   this.DrawSelection();
//   this.DrawSlots();
//   this.UpdateNameControlLocation();
//}

//protected override void DrawSlots()
//{
//   this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
//   this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
//}
//private void Init() {
//   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
//   UI.SetStatusColor(this, this.ElectrostaticPrecipitator.SolveState);
//   this.UpdateBackImage();
//}

//private void InitializeComponent() {
//   // 
//   // ElectrostaticPrecipitatorControl
//   // 
//   this.Name = "ElectrostaticPrecipitatorControl";
//   //this.Size = new System.Drawing.Size(96, 72);
//}



