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

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for ValveControl.
	/// </summary>
   [Serializable]
   public class ValveControl : TwoStreamUnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public Valve Valve
      {
         get {return (Valve)this.Solvable;}
         set {this.Solvable = value;}
      }

      public ValveControl()
      {
      }

		public ValveControl(Flowsheet flowsheet, Point location, Valve valve) :
         base(flowsheet, location, valve)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Valve.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // ValveControl
         // 
         this.Name = "ValveControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public override bool HitTestStreamIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public override bool HitTestStreamOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
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
         this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new ValveEditor(this);
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
            this.BackgroundImage = UI.IMAGES.VALVE_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.VALVE_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.VALVE_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.VALVE_CTRL_SOLVE_FAILED_IMG;
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

         sb.Append("Valve: ");
         sb.Append(this.Valve.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Valve.PressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Valve.PressureDrop, us, nfs));
         sb.Append("\r\n");
         
         return sb.ToString();
      }

      protected ValveControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionValveControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionValveControl", ValveControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}