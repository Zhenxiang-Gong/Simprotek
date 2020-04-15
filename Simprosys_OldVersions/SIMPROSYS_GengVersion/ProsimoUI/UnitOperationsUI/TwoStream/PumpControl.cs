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
	/// Summary description for PumpControl.
	/// </summary>
   [Serializable]
   public class PumpControl : TwoStreamUnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public Pump Pump
      {
         get {return (Pump)this.Solvable;}
         set {this.Solvable = value;}
      }

      public PumpControl()
      {
      }

		public PumpControl(Flowsheet flowsheet, Point location, Pump pump) :
         base(flowsheet, location, pump)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_W, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Pump.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // PumpControl
         // 
         this.Name = "PumpControl";
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
               this.Editor = new PumpEditor(this);
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
            this.BackgroundImage = UI.IMAGES.PUMP_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.PUMP_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.PUMP_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.PUMP_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;

         // move it 1/8 up
         p.Y -= this.Height/8;

         return p;
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

         sb.Append("Pump: ");
         sb.Append(this.Pump.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Pump.Capacity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.Capacity, us, nfs));
         if (this.Pump.Capacity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Pump.StaticSuctionHead, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.StaticSuctionHead, us, nfs));
         if (this.Pump.StaticSuctionHead.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Pump.SuctionFrictionHead, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.SuctionFrictionHead, us, nfs));
         if (this.Pump.SuctionFrictionHead.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Pump.StaticDischargeHead, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.StaticDischargeHead, us, nfs));
         if (this.Pump.StaticDischargeHead.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Pump.DischargeFrictionHead, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.DischargeFrictionHead, us, nfs));
         if (this.Pump.DischargeFrictionHead.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Pump.TotalDynamicHead, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.TotalDynamicHead, us, nfs));
         if (this.Pump.TotalDynamicHead.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Pump.Efficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.Efficiency, us, nfs));
         if (this.Pump.Efficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Pump.PowerInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Pump.PowerInput, us, nfs));
         if (this.Pump.PowerInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         if (this.Pump.IncludeOutletVelocityEffect)
         {
            sb.Append(UI.GetVariableName(this.Pump.OutletDiameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.Pump.OutletDiameter, us, nfs));
            if (this.Pump.OutletDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(this.Pump.OutletVelocity, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(this.Pump.OutletVelocity, us, nfs));
            if (this.Pump.OutletVelocity.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         return sb.ToString();
      }

      protected PumpControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPumpControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionPumpControl", PumpControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
