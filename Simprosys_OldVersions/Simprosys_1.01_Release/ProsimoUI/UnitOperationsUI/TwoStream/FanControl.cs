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
	/// Summary description for FanControl.
	/// </summary>
   [Serializable]
   public class FanControl : TwoStreamUnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public Fan Fan
      {
         get {return (Fan)this.Solvable;}
         set {this.Solvable = value;}
      }

      public FanControl()
      {
      }

		public FanControl(Flowsheet flowsheet, Point location, Fan fan) :
         base(flowsheet, location, fan)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Fan.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // FanControl
         // 
         this.Name = "FanControl";
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
            this.Editor = new FanEditor(this);
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
            this.BackgroundImage = UI.IMAGES.FAN_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.FAN_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.FAN_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.FAN_CTRL_SOLVE_FAILED_IMG;
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

         sb.Append("Fan: ");
         sb.Append(this.Fan.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Fan.StaticPressure, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Fan.StaticPressure, us, nfs));
         if (this.Fan.StaticPressure.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Fan.TotalDischargePressure, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Fan.TotalDischargePressure, us, nfs));
         if (this.Fan.TotalDischargePressure.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Fan.Efficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Fan.Efficiency, us, nfs));
         if (this.Fan.Efficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Fan.PowerInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Fan.PowerInput, us, nfs));
         if (this.Fan.PowerInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         if (this.Fan.IncludeOutletVelocityEffect)
         {
            string outletCrossSectionTypeStr = "";
            if (this.Fan.OutletCrossSectionType == CrossSectionType.Circle)
               outletCrossSectionTypeStr = "Circle";
            else if (this.Fan.OutletCrossSectionType == CrossSectionType.Rectangle)
               outletCrossSectionTypeStr = "Rectangle";
            sb.Append("Outlet Cross Section Type");
            sb.Append(" = ");
            sb.Append(outletCrossSectionTypeStr);
            sb.Append("\r\n");

            if (this.Fan.OutletCrossSectionType == CrossSectionType.Circle)
            {   
               sb.Append(UI.GetVariableName(this.Fan.OutletDiameter, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletDiameter, us, nfs));
               if (this.Fan.OutletDiameter.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");

               sb.Append(UI.GetVariableName(this.Fan.OutletVelocity, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletVelocity, us, nfs));
               if (this.Fan.OutletVelocity.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");
            }
            else if (this.Fan.OutletCrossSectionType == CrossSectionType.Rectangle)
            {
               sb.Append(UI.GetVariableName(this.Fan.OutletWidth, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletWidth, us, nfs));
               if (this.Fan.OutletWidth.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");

               sb.Append(UI.GetVariableName(this.Fan.OutletHeight, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletHeight, us, nfs));
               if (this.Fan.OutletHeight.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");

               sb.Append(UI.GetVariableName(this.Fan.OutletHeightWidthRatio, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletHeightWidthRatio, us, nfs));
               if (this.Fan.OutletHeightWidthRatio.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");

               sb.Append(UI.GetVariableName(this.Fan.OutletVelocity, us));
               sb.Append(" = ");
               sb.Append(UI.GetVariableValue(this.Fan.OutletVelocity, us, nfs));
               if (this.Fan.OutletVelocity.IsSpecified)
                  sb.Append(" * ");
               sb.Append("\r\n");
            }
         }

         return sb.ToString();
      }

      protected FanControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionFanControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionFanControl", FanControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
