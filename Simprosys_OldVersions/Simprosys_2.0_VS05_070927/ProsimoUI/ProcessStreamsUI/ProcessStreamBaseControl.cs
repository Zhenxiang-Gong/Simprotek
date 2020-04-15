using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo.UnitOperations;
using ProsimoUI;
using ProsimoUI.UnitOperationsUI;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for ProcessStreamBaseControl.
   /// </summary>
   [Serializable]
   public class ProcessStreamBaseControl : SolvableControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public const int IN_INDEX = 0;
      public const int OUT_INDEX = 1;

      public ProcessStreamBase ProcessStreamBase {
         get { return (ProcessStreamBase)this.Solvable; }
         set { this.Solvable = value; }
      }

      private System.Windows.Forms.ContextMenu contextMenu;
      private System.Windows.Forms.MenuItem menuItemEdit;
      private System.Windows.Forms.MenuItem menuItemDelete;
      private System.Windows.Forms.MenuItem menuItemRotate;
      private System.Windows.Forms.MenuItem menuItemClockwise;
      private System.Windows.Forms.MenuItem menuItemCounterclockwise;

      protected StreamOrientation ctrlOrientation;
      public StreamOrientation Orientation {
         get { return ctrlOrientation; }
         set {
            ctrlOrientation = value;
            this.UpdateBackImage();
            this.flowsheet.ConnectionManager.UpdateConnections(this);
            this.flowsheet.IsDirty = true;
         }
      }

      public PointOrientation InOrientation {
         get { return this.GetInConnectionPointOrientation(); }
      }

      public PointOrientation OutOrientation {
         get { return this.GetOutConnectionPointOrientation(); }
      }

      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public ProcessStreamBaseControl() {
      }

      public ProcessStreamBaseControl(Flowsheet flowsheet, Point location, ProcessStreamBase processStreamBase)
         :
         base(flowsheet, location, processStreamBase) {
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
         this.contextMenu = new System.Windows.Forms.ContextMenu();
         this.menuItemEdit = new System.Windows.Forms.MenuItem();
         this.menuItemDelete = new System.Windows.Forms.MenuItem();
         this.menuItemRotate = new System.Windows.Forms.MenuItem();
         this.menuItemClockwise = new System.Windows.Forms.MenuItem();
         this.menuItemCounterclockwise = new System.Windows.Forms.MenuItem();
         // 
         // contextMenu
         // 
         this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.menuItemEdit,
                                                                                    this.menuItemDelete,
                                                                                    this.menuItemRotate});
         // 
         // menuItemEdit
         // 
         this.menuItemEdit.Index = 0;
         this.menuItemEdit.Text = "Edit...";
         this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
         // 
         // menuItemDelete
         // 
         this.menuItemDelete.Index = 1;
         this.menuItemDelete.Text = "Delete";
         this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
         // 
         // menuItemRotate
         // 
         this.menuItemRotate.Index = 2;
         this.menuItemRotate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this.menuItemClockwise,
                                                                                       this.menuItemCounterclockwise});
         this.menuItemRotate.Text = "Rotate";
         // 
         // menuItemClockwise
         // 
         this.menuItemClockwise.Index = 0;
         this.menuItemClockwise.Text = "Clockwise";
         this.menuItemClockwise.Click += new System.EventHandler(this.menuItemClockwise_Click);
         // 
         // menuItemCounterclockwise
         // 
         this.menuItemCounterclockwise.Index = 1;
         this.menuItemCounterclockwise.Text = "Counterclockwise";
         this.menuItemCounterclockwise.Click += new System.EventHandler(this.menuItemCounterclockwise_Click);
         // 
         // ProcessStreamBaseControl
         // 
         this.ContextMenu = this.contextMenu;
         this.Name = "ProcessStreamBaseControl";
         this.Size = new System.Drawing.Size(150, 131);

      }
      #endregion

      public virtual void RotateClockwise() {
         if (this.Orientation.Equals(StreamOrientation.Down))
            this.Orientation = StreamOrientation.Left;
         else if (this.Orientation.Equals(StreamOrientation.Left))
            this.Orientation = StreamOrientation.Up;
         else if (this.Orientation.Equals(StreamOrientation.Up))
            this.Orientation = StreamOrientation.Right;
         else if (this.Orientation.Equals(StreamOrientation.Right))
            this.Orientation = StreamOrientation.Down;
      }

      public virtual void RotateCounterclockwise() {
         if (this.Orientation.Equals(StreamOrientation.Down))
            this.Orientation = StreamOrientation.Right;
         else if (this.Orientation.Equals(StreamOrientation.Right))
            this.Orientation = StreamOrientation.Up;
         else if (this.Orientation.Equals(StreamOrientation.Up))
            this.Orientation = StreamOrientation.Left;
         else if (this.Orientation.Equals(StreamOrientation.Left))
            this.Orientation = StreamOrientation.Down;
      }

      public Point GetInConnectionPoint() {
         // this point is referenced to the flowsheet
         Point p1 = this.GetInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetInSlotPoint() {
         // this point is referenced to this control
         Point p = new Point(0, 0);
         if (this.Orientation.Equals(StreamOrientation.Down)) {
            // middle of the up side
            p.X += this.Width / 2;
         }
         else if (this.Orientation.Equals(StreamOrientation.Left)) {
            // middle of the right side
            p.X += this.Width;
            p.Y += this.Height / 2;
         }
         else if (this.Orientation.Equals(StreamOrientation.Right)) {
            // middle of the left side
            p.Y += this.Height / 2;
         }
         else if (this.Orientation.Equals(StreamOrientation.Up)) {
            // middle of the down side
            p.X += this.Width / 2;
            p.Y += this.Height;
         }
         return p;
      }

      private PointOrientation GetInConnectionPointOrientation() {
         if (this.Orientation == StreamOrientation.Down)
            return PointOrientation.N;
         else if (this.Orientation == StreamOrientation.Left)
            return PointOrientation.E;
         else if (this.Orientation == StreamOrientation.Right)
            return PointOrientation.W;
         else if (this.Orientation == StreamOrientation.Up)
            return PointOrientation.S;
         else
            return PointOrientation.Unknown;
      }

      public Point GetOutConnectionPoint() {
         // this point is referenced to the flowsheet
         Point p1 = this.GetOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetOutSlotPoint() {
         // this point is referenced to this control
         Point p = new Point(0, 0);
         if (this.Orientation.Equals(StreamOrientation.Down)) {
            // middle of the down side
            p.X += this.Width / 2;
            p.Y += this.Height;
         }
         else if (this.Orientation.Equals(StreamOrientation.Left)) {
            // middle of the left side
            p.Y += this.Height / 2;
         }
         else if (this.Orientation.Equals(StreamOrientation.Right)) {
            // middle of the right side
            p.X += this.Width;
            p.Y += this.Height / 2;
         }
         else if (this.Orientation.Equals(StreamOrientation.Up)) {
            // middle of the up side
            p.X += this.Width / 2;
         }
         return p;
      }

      private PointOrientation GetOutConnectionPointOrientation() {
         if (this.Orientation == StreamOrientation.Down)
            return PointOrientation.S;
         else if (this.Orientation == StreamOrientation.Left)
            return PointOrientation.W;
         else if (this.Orientation == StreamOrientation.Right)
            return PointOrientation.E;
         else if (this.Orientation == StreamOrientation.Up)
            return PointOrientation.N;
         else
            return PointOrientation.Unknown;
      }

      public bool HitTestUnitOpIn(Point p) {
         bool hit = false;
         Point slot = this.GetInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         if (this.Orientation.Equals(StreamOrientation.Down)) {
            // middle of the up side
            gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         }
         else if (this.Orientation.Equals(StreamOrientation.Left)) {
            // middle of the right side
            gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         }
         else if (this.Orientation.Equals(StreamOrientation.Right)) {
            // middle of the left side
            gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         }
         else if (this.Orientation.Equals(StreamOrientation.Up)) {
            // middle of the down side
            gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         }

         Pen pen = new Pen(Color.Black, 20);
         Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, pen, g);

         pen.Dispose();
         g.Dispose();
         gp.Dispose();
         return hit;
      }

      public bool HitTestUnitOpOut(Point p) {
         bool hit = false;
         Point slot = this.GetOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         if (this.Orientation.Equals(StreamOrientation.Down)) {
            // middle of the down side
            gp.AddLine(slot.X - UI.SLOT_DELTA, this.Height, slot.X + UI.SLOT_DELTA, this.Height);
         }
         else if (this.Orientation.Equals(StreamOrientation.Left)) {
            // middle of the left side
            gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         }
         else if (this.Orientation.Equals(StreamOrientation.Right)) {
            // middle of the right side
            gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         }
         else if (this.Orientation.Equals(StreamOrientation.Up)) {
            // middle of the up side
            gp.AddLine(slot.X - UI.SLOT_DELTA, 0, slot.X + UI.SLOT_DELTA, 0);
         }

         Pen pen = new Pen(Color.Black, 20);
         Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, pen, g);

         pen.Dispose();
         g.Dispose();
         gp.Dispose();
         return hit;
      }

      protected override void DoThePaint() {
         this.DrawSelection();
         this.UpdateNameControlLocation();
      }

      private void menuItemEdit_Click(object sender, System.EventArgs e) {
         this.Edit();
      }

      private void menuItemDelete_Click(object sender, System.EventArgs e) {
         this.flowsheet.DeleteSolvable(this);
      }

      private void menuItemClockwise_Click(object sender, System.EventArgs e) {
         this.RotateClockwise();
      }

      private void menuItemCounterclockwise_Click(object sender, System.EventArgs e) {
         this.RotateCounterclockwise();
      }

      protected override void MouseDownHandler(Point p) {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne) {
            if (this.ProcessStreamBase.CanConnect()) {
               // ok for the second step
               this.flowsheet.firstStepCtrl = this;
               this.flowsheet.SetFlowsheetActivity(FlowsheetActivity.AddingConnStepTwo);
            }
            else {
               this.flowsheet.ResetActivity();
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo) {
            if (this.flowsheet.firstStepCtrl.Solvable is UnitOperation) {
               UnitOpControl ctrl = (UnitOpControl)this.flowsheet.firstStepCtrl;
               if (ctrl.UnitOperation.CanAttachStream(this.ProcessStreamBase, this.flowsheet.attachIndex)) {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = ctrl.UnitOperation.AttachStream(this.ProcessStreamBase, this.flowsheet.attachIndex);
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
            if (this.ProcessStreamBase.CanConnect()) {
               this.Cursor = Cursors.Cross;
            }
            else {
               this.Cursor = Cursors.Default;
            }

         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo) {
            if (this.flowsheet.firstStepCtrl.Solvable is UnitOperation) {
               UnitOpControl ctrl = (UnitOpControl)this.flowsheet.firstStepCtrl;
               if (ctrl.UnitOperation.CanAttachStream(this.ProcessStreamBase, this.flowsheet.attachIndex))
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

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e) {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as ProcessStreamBaseControl);
      }

      protected ProcessStreamBaseControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context) {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionProcessStreamBaseControl", typeof(int));
         switch (persistedClassVersion) {
            case 1:
               this.Orientation = (StreamOrientation)info.GetValue("StreamOrientation", typeof(StreamOrientation));
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionProcessStreamBaseControl", ProcessStreamBaseControl.CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("StreamOrientation", this.Orientation, typeof(StreamOrientation));
      }
   }
}
