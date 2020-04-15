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
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitSystems;
using Prosimo.UnitOperations.Miscellaneous;
using Prosimo.UnitOperations.ProcessStreams;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for RecycleControl.
	/// </summary>
   [Serializable]
   public class RecycleControl : TwoStreamUnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;
      
      public new static PointOrientation INLET_ORIENTATION = PointOrientation.E;
      public new static PointOrientation OUTLET_ORIENTATION = PointOrientation.W;

      public Recycle Recycle
      {
         get {return (Recycle)this.Solvable;}
         set {this.Solvable = value;}
      }

      public RecycleControl()
      {
      }

		public RecycleControl(Flowsheet flowsheet, Point location, Recycle recycle) :
         base(flowsheet, location, recycle)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Recycle.SolveState);
         this.UpdateBackImage();

         this.menuItemEdit.Enabled = false;
         this.menuItemEdit.Visible = false;
         this.IsShownInEditor = false;
      }

		private void InitializeComponent()
		{
         // 
         // RecycleControl
         // 
         this.Name = "RecycleControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public override bool HitTestStreamIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public override bool HitTestStreamOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
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
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Right, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Left, SlotDirection.Out);
      }

      protected override void UpdateBackImage()
      {
         if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
         {
            this.BackgroundImage = UI.IMAGES.RECYCLE_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
         {
            this.BackgroundImage = UI.IMAGES.RECYCLE_CTRL_PARTIALLY_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.RECYCLE_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.RECYCLE_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetStreamOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public override Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;
         return p; 
      }

      public override Point GetStreamInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetStreamInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public override Point GetStreamInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;
         return p;
      }

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Recycle: ");
         sb.Append(this.Recycle.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected RecycleControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionRecycleControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionRecycleControl", RecycleControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
