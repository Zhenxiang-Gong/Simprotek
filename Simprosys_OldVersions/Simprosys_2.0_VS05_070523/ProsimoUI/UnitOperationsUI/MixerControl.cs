using System;
using System.Collections;
using System.ComponentModel;
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
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for MixerControl.
	/// </summary>
   [Serializable]
   public class MixerControl : UnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static PointOrientation INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation OUTLET_ORIENTATION = PointOrientation.E;

      public Mixer Mixer
      {
         get {return (Mixer)this.Solvable;}
         set {this.Solvable = value;}
      }

      public MixerControl()
      {
      }

		public MixerControl(Flowsheet flowsheet, Point location, Mixer mixer) :
         base(flowsheet, location, mixer)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Mixer.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // MixerControl
         // 
         this.Name = "MixerControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestStreamIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestStreamOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      protected override void MouseDownHandler(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestStreamIn(p) || HitTestStreamOut(p))
            {
               if (HitTestStreamIn(p))
                  idx = this.Mixer.InletStreams.Count + 1;
               else if (HitTestStreamOut(p))
                  idx = Mixer.OUTLET_INDEX;

               if (this.Mixer.CanConnect(idx))
               {
                  // ok for the second step
                  this.flowsheet.firstStepCtrl = this;
                  this.flowsheet.Activity = FlowsheetActivity.AddingConnStepTwo;
                  this.flowsheet.attachIndex = idx;
               }
            }
            else
            {
               this.flowsheet.ResetActivity();
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if (HitTestStreamIn(p))
                  idx = this.Mixer.InletStreams.Count + 1;
               else if (HitTestStreamOut(p))
                  idx = Mixer.OUTLET_INDEX;
               
               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Mixer.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Mixer.AttachStream(ctrl.ProcessStreamBase, idx);
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

      protected override void SolvableControl_LocationChanged(object sender, EventArgs e)
      {
         this.UpdateNameControlLocation();
         this.flowsheet.ConnectionManager.UpdateConnections(sender as MixerControl);
      }

      protected override void DoThePaint()
      {
         this.DrawBorder();
         this.DrawSelection();
         this.DrawSlots();
         this.UpdateNameControlLocation();
      }

      protected void DrawSlots()
      {
         int count = this.Mixer.InletStreams.Count;
         for (int i=0; i<count; i++)
         {
            this.DrawSlot(this, this.GetStreamInSlotPoint(i+1, count), SlotPosition.Left, SlotDirection.In);
         }
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new MixerEditor(this);
            this.Editor.Owner = (Form)this.flowsheet.Parent;
            this.Editor.Show();
         }
         else
         {
            if (this.Editor.WindowState.Equals(FormWindowState.Minimized))
               this.Editor.WindowState = FormWindowState.Normal;
            this.Editor.Activate();
         }
      }

      protected override void UpdateBackImage()
      {
         if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
         {
            this.BackgroundImage = UI.IMAGES.MIXER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.MIXER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.MIXER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.MIXER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public Point GetStreamInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetStreamInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;
         return p; 
      }

      public Point GetStreamOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetStreamOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;
         return p;
      }

      public Point GetStreamInConnectionPoint(int k, int n)
      {
         // this point is referenced to the flowsheet
         Point p1 = this.GetStreamInSlotPoint(k, n);
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetStreamInSlotPoint(int k, int n)
      {
         // this point is referenced to this control
         // k out of n on the left side
         int empty = 6;

         Point p = new Point(0,0);
         int t = (this.Height-2*empty)/(n+1);
         p.Y += empty + t*k;
         return p; 
      }
      
       public override SolvableConnection CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad)
      {
         StreamType streamType = StreamType.Unknown;
         if (ps is DryingGasStream)
         {
            streamType = StreamType.Gas;
         }
         else if (ps is ProcessStream)
         {
            streamType = StreamType.Process;
         }
         else if (ps is DryingMaterialStream)
         {
            streamType = StreamType.Material;
         }

         ProcessStreamBaseControl ctrl = this.flowsheet.StreamManager.GetProcessStreamBaseControl(ps);
         SolvableConnection conn = null;

         if (ad > 0)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = ad;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetStreamInConnectionPoint(ad, uo.InletStreams.Count);
            PointOrientation uoOrientation = MixerControl.INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Mixer.OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Mixer.OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetStreamOutConnectionPoint();
            PointOrientation uoOrientation = MixerControl.OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }

         if (conn != null)
         {
             this.flowsheet.ConnectionManager.AddConnection(conn);
             //this.flowsheet.ConnectionManager.Connections.Add(conn);
             //this.flowsheet.ConnectionManager.UpdateConnections(this);
             //this.flowsheet.ConnectionManager.DrawConnections();
            this.DoThePaint();
         }
         return conn;
      }

      protected override void DeleteConnection(UnitOperation uo, ProcessStreamBase ps)
      {
         this.flowsheet.ConnectionManager.RemoveUnitOpConnections(uo.Name);
         this.RecreateAllConnections();
         this.DoThePaint();
      }

      private void RecreateAllConnections()
      {
         if (this.Mixer.Outlet != null)
            this.CreateConnection(this.Mixer, this.Mixer.Outlet, Mixer.OUTLET_INDEX);
         IEnumerator e = this.Mixer.InletStreams.GetEnumerator();
         int i = 1;
         while (e.MoveNext())
         {
            ProcessStreamBase psb = (ProcessStreamBase)e.Current;
            this.CreateConnection(this.Mixer, psb, i++);
         }
      }

      protected override void ShowConnectionPoints(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestStreamIn(p) || HitTestStreamOut(p))
            {
               if (HitTestStreamIn(p))
                  idx = this.Mixer.InletStreams.Count + 1;
               else if (HitTestStreamOut(p))
                  idx = Mixer.OUTLET_INDEX;

               if (this.Mixer.CanConnect(idx))
                  this.Cursor = Cursors.Cross;
               else
                  this.Cursor = Cursors.Default;
            }
            else
            {
               this.Cursor = Cursors.Default;
            }
         }
         else if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepTwo)
         {
            if (this.flowsheet.firstStepCtrl.Solvable is ProcessStreamBase)
            {
               int idx = -1;
               if (HitTestStreamIn(p))
                  idx = this.Mixer.InletStreams.Count + 1;
               else if (HitTestStreamOut(p))
                  idx = Mixer.OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Mixer.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Mixer: ");
         sb.Append(this.Mixer.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected MixerControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionMixerControl", typeof(int));
         switch (persistedClassVersion)
         {
            case 1:
               break;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionMixerControl", MixerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
