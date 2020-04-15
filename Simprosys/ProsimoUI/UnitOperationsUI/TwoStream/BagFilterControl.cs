using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using Prosimo;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for BagFilterControl.
   /// </summary>
   [Serializable]
   public class BagFilterControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;
      public static PointOrientation INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation PARTICLE_OUTLET_ORIENTATION = PointOrientation.S;

      internal protected override string SolvableTypeName
      {
         get { return "Bag Filter"; }
      }

      public BagFilter BagFilter {
         get { return (BagFilter)this.Solvable; }
         set { this.Solvable = value; }
      }

      public BagFilterControl() {
      }

      public BagFilterControl(Flowsheet flowsheet, Point location, BagFilter bagFilter)
         : base(flowsheet, location, bagFilter) {
         //InitializeComponent();
      }

      //private void InitializeComponent() {
      //}

      public bool HitTestStreamIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         gp.Dispose();
         return hit;
      }

      public bool HitTestStreamOut(Point p)
      {
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

      public Point GetStreamInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;

         // move it down 1/6
         p.Y += this.Height / 6;

         return p;
      }

      public virtual Point GetStreamInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetStreamInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }
      
      public Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;

         // move it up 1/6
         p.Y -= this.Height / 6;

         return p;
      }

      public virtual Point GetStreamOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetStreamOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
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
                  idx = BagFilter.INLET_INDEX;
               else if(HitTestStreamOut(p))
                  idx = BagFilter.OUTLET_INDEX;
               else if(HitTestParticleOut(p))
                  idx = BagFilter.PARTICLE_OUTLET_INDEX;

               if(this.BagFilter.CanAttach(idx))
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
               if(this.BagFilter.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.BagFilter.AttachStream(ctrl.ProcessStreamBase, idx);
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
            // adjust for Recycle
            if(uo is Recycle)
               uoOrientation = RecycleControl.INLET_ORIENTATION;
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
            // adjust for Recycle
            if(uo is Recycle)
               uoOrientation = RecycleControl.OUTLET_ORIENTATION;
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

               if(this.BagFilter.CanAttach(idx))
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
               if(this.BagFilter.CanAttachStream(ctrl.ProcessStreamBase, idx))
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
      
      protected override void UpdateBackImage()
      {
         if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e)
      {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as BagFilterControl);
      }
      
      protected override void DrawSlots()
      {
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetParticleOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }
      
      public override void Edit()
      {
         if (this.editor == null) {
            this.editor = new BagFilterEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      protected BagFilterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionBagFilterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionBagFilterControl", BagFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
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

//   //sb.Append("Bag Filter: ");
//   //sb.Append(this.BagFilter.Name);
//   //sb.Append("\r\n");
//   //sb.Append(UI.UNDERLINE);
//   //sb.Append("\r\n");

//   //sb.Append(ToPrintVarList());

//   //return sb.ToString();
//   return ToPrintVarList("Bag Filter: ");
//}


//sb.Append(base.ToPrint());

//sb.Append(GetVariableName(this.BagFilter.BagDiameter, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.BagFilter.BagDiameter, us, nfs));
//if (this.BagFilter.BagDiameter.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.BagFilter.BagLength, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.BagFilter.BagLength, us, nfs));
//if (this.BagFilter.BagLength.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");

//sb.Append(GetVariableName(this.BagFilter.NumberOfBags, us));
//sb.Append(" = ");
//sb.Append(GetVariableValue(this.BagFilter.NumberOfBags, us, nfs));
//if (this.BagFilter.NumberOfBags.IsSpecified)
//   sb.Append(" * ");
//sb.Append("\r\n");