using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for TwoStreamUnitOpControl.
   /// </summary>
   [Serializable]
   public class TwoStreamUnitOpControl : UnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation OUTLET_ORIENTATION = PointOrientation.E;

      public TwoStreamUnitOperation TwoStreamUnitOp {
         get { return (TwoStreamUnitOperation)this.solvable; }
         set { this.solvable = value; }
      }

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public TwoStreamUnitOpControl() {
      }

      public TwoStreamUnitOpControl(Flowsheet flowsheet, Point location, TwoStreamUnitOperation twoStrUnitOp)
         :
         base(flowsheet, location, twoStrUnitOp) {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing) {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code
      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         components = new System.ComponentModel.Container();
      }
      #endregion

      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestStreamIn(p) || HitTestStreamOut(p)) {
               if (HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if (HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;

               if (this.TwoStreamUnitOp.CanAttach(idx)) {
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
               if (HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if (HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.TwoStreamUnitOp.CanAttachStream(ctrl.ProcessStreamBase, idx)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.TwoStreamUnitOp.AttachStream(ctrl.ProcessStreamBase, idx);
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

      protected override void ShowConnectionPoints(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            int idx = -1;
            if (HitTestStreamIn(p) || HitTestStreamOut(p)) {
               if (HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if (HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;

               if (this.TwoStreamUnitOp.CanAttach(idx))
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
               if (HitTestStreamIn(p))
                  idx = TwoStreamUnitOperation.INLET_INDEX;
               else if (HitTestStreamOut(p))
                  idx = TwoStreamUnitOperation.OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.TwoStreamUnitOp.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

      public virtual bool HitTestStreamIn(Point p) {
         return false;
      }

      public virtual bool HitTestStreamOut(Point p) {
         return false;
      }

      public virtual Point GetStreamInConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetStreamInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public virtual Point GetStreamInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;
         return p;
      }

      public virtual Point GetStreamOutConnectionPoint() {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetStreamOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public virtual Point GetStreamOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;
         return p;
      }

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e) {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as TwoStreamUnitOpControl);
      }

      protected override void DrawSlots() {
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
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

         if (ad == TwoStreamUnitOperation.INLET_INDEX) {
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
            if (uo is Recycle)
               uoOrientation = RecycleControl.INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);

            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }
         else if (ad == TwoStreamUnitOperation.OUTLET_INDEX) {
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
            if (uo is Recycle)
               uoOrientation = RecycleControl.OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, ps.GetType());
         }

         if (conn != null) {
            //this.flowsheet.ConnectionManager.DrawConnection(conn);
            this.flowsheet.ConnectionManager.AddConnection(conn);
            //this.flowsheet.ConnectionManager.Connections.Add(conn);
            //this.flowsheet.ConnectionManager.DrawConnections();
         }
         return conn;
      }

      protected TwoStreamUnitOpControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) { 
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionTwoStreamUnitOpControl", typeof(int));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionTwoStreamUnitOpControl", TwoStreamUnitOpControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
