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
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HeatExchangerControl.
	/// </summary>
   [Serializable]
   public class HeatExchangerControl : UnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static PointOrientation COLD_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation COLD_OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation HOT_INLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation HOT_OUTLET_ORIENTATION = PointOrientation.S;

      public HeatExchanger HeatExchanger
      {
         get {return (HeatExchanger)this.Solvable;}
         set {this.Solvable = value;}
      }

      public HeatExchangerControl()
      {
      }

		public HeatExchangerControl(Flowsheet flowsheet, Point location, HeatExchanger heatExchanger) :
         base(flowsheet, location, heatExchanger)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.HeatExchanger.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // HeatExchangerControl
         // 
         this.Name = "HeatExchangerControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestColdIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetColdInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestColdOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetColdOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestHotIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetHotInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X-UI.SLOT_DELTA, 0, slot.X+UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestHotOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetHotOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X-UI.SLOT_DELTA, this.Height, slot.X+UI.SLOT_DELTA, this.Height);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      protected override void MouseDownHandler(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestColdIn(p) || HitTestColdOut(p) || HitTestHotIn(p) || HitTestHotOut(p))
            {
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               if (this.HeatExchanger.CanConnect(idx))
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
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.HeatExchanger.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.HeatExchanger.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as HeatExchangerControl);
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
         this.DrawSlot(this, this.GetColdInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetColdOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetHotInSlotPoint(), SlotPosition.Up, SlotDirection.In);
         this.DrawSlot(this, this.GetHotOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new HeatExchangerEditor(this);
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
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.HEATEXCHANGER_CTRL_SOLVE_FAILED_IMG;
         }
      }
      
      public Point GetColdInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetColdInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetColdInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;
         return p; 
      }

      public Point GetColdOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetColdOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetColdOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;
         return p;
      }

      public Point GetHotInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetHotInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHotInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0,0);
         p.X += this.Width/2;
         return p; 
      }

      public Point GetHotOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetHotOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetHotOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0,0);
         p.Y += this.Height;
         p.X += this.Width/2;
         return p;
      }
      
      public override void CreateConnection(UnitOperation uo, ProcessStreamBase ps, int ad)
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

         if (ad == HeatExchanger.COLD_SIDE_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.COLD_SIDE_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetColdInConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.COLD_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == HeatExchanger.COLD_SIDE_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetColdOutConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.COLD_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == HeatExchanger.HOT_SIDE_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.HOT_SIDE_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetHotInConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.HOT_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == HeatExchanger.HOT_SIDE_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetHotOutConnectionPoint();
            PointOrientation uoOrientation = HeatExchangerControl.HOT_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }

         if (conn != null)
         {
            this.flowsheet.ConnectionManager.Connections.Add(conn);
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      protected override void ShowConnectionPoints(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestColdIn(p) || HitTestColdOut(p) || HitTestHotIn(p) || HitTestHotOut(p))
            {
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               if (this.HeatExchanger.CanConnect(idx))
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
               if (HitTestColdIn(p))
                  idx = HeatExchanger.COLD_SIDE_INLET_INDEX;
               else if (HitTestColdOut(p))
                  idx = HeatExchanger.COLD_SIDE_OUTLET_INDEX;
               else if (HitTestHotIn(p))
                  idx = HeatExchanger.HOT_SIDE_INLET_INDEX;
               else if (HitTestHotOut(p))
                  idx = HeatExchanger.HOT_SIDE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.HeatExchanger.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Heat Exchanger: ");
         sb.Append(this.HeatExchanger.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.HeatExchanger.TotalHeatTransfer, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.HeatExchanger.TotalHeatTransfer, us, nfs));
         if (this.HeatExchanger.TotalHeatTransfer.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.HeatExchanger.ColdSidePressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.HeatExchanger.ColdSidePressureDrop, us, nfs));
         if (this.HeatExchanger.ColdSidePressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.HeatExchanger.HotSidePressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.HeatExchanger.HotSidePressureDrop, us, nfs));
         if (this.HeatExchanger.HotSidePressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.HeatExchanger.CurrentRatingModel != null)
         {
            if (this.HeatExchanger.CurrentRatingModel is HXRatingModelSimpleGeneric)
            {
               sb.Append(this.ToPrintSimpleGenericRating());
            }
            else if (this.HeatExchanger.CurrentRatingModel is HXRatingModelShellAndTube)
            {
               sb.Append(this.ToPrintShellAndTubeRating());
            }
            else if (this.HeatExchanger.CurrentRatingModel is HXRatingModelPlateAndFrame)
            {
               sb.Append(this.ToPrintPlateAndFrameRating());
            }
         }

         return sb.ToString();
      }

      private string ToPrintSimpleGenericRating()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelSimpleGeneric ratingModel = 
            this.HeatExchanger.CurrentRatingModel as HXRatingModelSimpleGeneric;

         sb.Append("Simple Generic Rating:");
         sb.Append("\r\n");

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (ratingModel.IncludeWallEffect)
         {
            sb.Append(UI.GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         return sb.ToString();
      }

      private string ToPrintShellAndTubeRating()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelShellAndTube ratingModel = 
            this.HeatExchanger.CurrentRatingModel as HXRatingModelShellAndTube;

         sb.Append("Shell and Tube Rating:");
         sb.Append("\r\n");

         // the left side

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.FtFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.FtFactor, us, nfs));
         if (ratingModel.FtFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubeSideVelocity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubeSideVelocity, us, nfs));
         if (ratingModel.TubeSideVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideRe, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideRe, us, nfs));
         if (ratingModel.ColdSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ShellSideVelocity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ShellSideVelocity, us, nfs));
         if (ratingModel.ShellSideVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSideRe, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideRe, us, nfs));
         if (ratingModel.HotSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (ratingModel.IncludeWallEffect)
         {
            sb.Append(UI.GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         // the right side
         string ratingTypeStr = "";
         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware)
            ratingTypeStr = "Bell Delaware";
         else if (ratingModel.ShellRatingType == ShellRatingType.Donohue)
            ratingTypeStr = "Donohue";
         else if (ratingModel.ShellRatingType == ShellRatingType.Kern)
            ratingTypeStr = "Kern";
         sb.Append("Shell Rating Type");
         sb.Append(" = ");
         sb.Append(ratingTypeStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubeOuterDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubeOuterDiameter, us, nfs));
         if (ratingModel.TubeOuterDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubeWallThickness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubeWallThickness, us, nfs));
         if (ratingModel.TubeWallThickness.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubeInnerDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubeInnerDiameter, us, nfs));
         if (ratingModel.TubeInnerDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubeLengthBetweenTubeSheets, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubeLengthBetweenTubeSheets, us, nfs));
         if (ratingModel.TubeLengthBetweenTubeSheets.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TubesPerTubePass, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubesPerTubePass, us, nfs));
         if (ratingModel.TubesPerTubePass.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TubePassesPerShellPass, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubePassesPerShellPass, us, nfs));
         if (ratingModel.TubePassesPerShellPass.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ShellPasses, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ShellPasses, us, nfs));
         if (ratingModel.ShellPasses.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.TotalTubesInShell, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalTubesInShell, us, nfs));
         if (ratingModel.TotalTubesInShell.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         string shellTypeStr = "";
         if (ratingModel.ShellType == ShellType.E)
            shellTypeStr = "E";
         else if (ratingModel.ShellType == ShellType.F)
            shellTypeStr = "F";
         else if (ratingModel.ShellType == ShellType.G)
            shellTypeStr = "G";
         else if (ratingModel.ShellType == ShellType.H)
            shellTypeStr = "H";
         else if (ratingModel.ShellType == ShellType.J)
            shellTypeStr = "J";
         else if (ratingModel.ShellType == ShellType.K)
            shellTypeStr = "K";
         else if (ratingModel.ShellType == ShellType.X)
            shellTypeStr = "X";
         sb.Append("Shell Type");
         sb.Append(" = ");
         sb.Append(shellTypeStr);
         sb.Append("\r\n");

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware ||
            ratingModel.ShellRatingType == ShellRatingType.Kern)
         {
            string tubeLayoutStr = "";
            if (ratingModel.TubeLayout == TubeLayout.InlineSquare)
               tubeLayoutStr = "Inline Square";
            else if (ratingModel.TubeLayout == TubeLayout.RotatedSquare)
               tubeLayoutStr = "Rotated Square";
            else if (ratingModel.TubeLayout == TubeLayout.Triangular)
               tubeLayoutStr = "Triangular";
            sb.Append("Tube Layout");
            sb.Append(" = ");
            sb.Append(tubeLayoutStr);
            sb.Append("\r\n");
         }

         sb.Append(UI.GetVariableName(ratingModel.TubePitch, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TubePitch, us, nfs));
         if (ratingModel.TubePitch.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware ||
            ratingModel.ShellRatingType == ShellRatingType.Donohue)
         {
            sb.Append(UI.GetVariableName(ratingModel.BaffleCut, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.BaffleCut, us, nfs));
            if (ratingModel.BaffleCut.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         sb.Append(UI.GetVariableName(ratingModel.BaffleSpacing, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.BaffleSpacing, us, nfs));
         if (ratingModel.BaffleSpacing.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware)
         {
            sb.Append(UI.GetVariableName(ratingModel.EntranceBaffleSpacing, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.EntranceBaffleSpacing, us, nfs));
            if (ratingModel.EntranceBaffleSpacing.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(ratingModel.ExitBaffleSpacing, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.ExitBaffleSpacing, us, nfs));
            if (ratingModel.ExitBaffleSpacing.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         sb.Append(UI.GetVariableName(ratingModel.NumberOfBaffles, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfBaffles, us, nfs));
         if (ratingModel.NumberOfBaffles.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ShellInnerDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ShellInnerDiameter, us, nfs));
         if (ratingModel.ShellInnerDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware)
         {
            sb.Append(UI.GetVariableName(ratingModel.ShellToBaffleDiametralClearance, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.ShellToBaffleDiametralClearance, us, nfs));
            if (ratingModel.ShellToBaffleDiametralClearance.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         
         if (ratingModel.ShellRatingType == ShellRatingType.BellDelaware)
         {
            sb.Append(UI.GetVariableName(ratingModel.BundleToShellDiametralClearance, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.BundleToShellDiametralClearance, us, nfs));
            if (ratingModel.BundleToShellDiametralClearance.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(ratingModel.SealingStrips, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.SealingStrips, us, nfs));
            if (ratingModel.SealingStrips.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         if (ratingModel.IncludeNozzleEffect)
         {
            sb.Append(UI.GetVariableName(ratingModel.TubeSideEntranceNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.TubeSideEntranceNozzleDiameter, us, nfs));
            if (ratingModel.TubeSideEntranceNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(ratingModel.TubeSideExitNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.TubeSideExitNozzleDiameter, us, nfs));
            if (ratingModel.TubeSideExitNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(ratingModel.ShellSideEntranceNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.ShellSideEntranceNozzleDiameter, us, nfs));
            if (ratingModel.ShellSideEntranceNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(ratingModel.ShellSideExitNozzleDiameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.ShellSideExitNozzleDiameter, us, nfs));
            if (ratingModel.ShellSideExitNozzleDiameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         return sb.ToString();
      }

      private string ToPrintPlateAndFrameRating()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         HXRatingModelPlateAndFrame ratingModel = 
            this.HeatExchanger.CurrentRatingModel as HXRatingModelPlateAndFrame;

         sb.Append("Plate And Frame Rating:");
         sb.Append("\r\n");

         // the left side

         string flowDirectionStr = "";
         if (ratingModel.FlowDirection == FlowDirectionType.Counter)
            flowDirectionStr = "Counter";
         else if (ratingModel.FlowDirection == FlowDirectionType.Cross)
            flowDirectionStr = "Cross";
         else if (ratingModel.FlowDirection == FlowDirectionType.Parallel)
            flowDirectionStr = "Parallel";
         sb.Append("Flow Direction");
         sb.Append(" = ");
         sb.Append(flowDirectionStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.ColdSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideRe, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideRe, us, nfs));
         if (ratingModel.ColdSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSideHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideHeatTransferCoefficient, us, nfs));
         if (ratingModel.HotSideHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSideRe, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideRe, us, nfs));
         if (ratingModel.HotSideRe.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSideFoulingFactor, us, nfs));
         if (ratingModel.ColdSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.HotSideFoulingFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSideFoulingFactor, us, nfs));
         if (ratingModel.HotSideFoulingFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferCoefficient, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferCoefficient, us, nfs));
         if (ratingModel.TotalHeatTransferCoefficient.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.TotalHeatTransferArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.TotalHeatTransferArea, us, nfs));
         if (ratingModel.TotalHeatTransferArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.NumberOfHeatTransferUnits, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfHeatTransferUnits, us, nfs));
         if (ratingModel.NumberOfHeatTransferUnits.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ExchangerEffectiveness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ExchangerEffectiveness, us, nfs));
         if (ratingModel.ExchangerEffectiveness.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (ratingModel.IncludeWallEffect)
         {
            sb.Append(UI.GetVariableName(ratingModel.WallThermalConductivity, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThermalConductivity, us, nfs));
            if (ratingModel.WallThermalConductivity.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(ratingModel.WallThickness, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(ratingModel.WallThickness, us, nfs));
            if (ratingModel.WallThickness.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         // the right side

         sb.Append(UI.GetVariableName(ratingModel.ChannelWidth, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ChannelWidth, us, nfs));
         if (ratingModel.ChannelWidth.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ProjectedChannelLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ProjectedChannelLength, us, nfs));
         if (ratingModel.ProjectedChannelLength.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.EnlargementFactor, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.EnlargementFactor, us, nfs));
         if (ratingModel.EnlargementFactor.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ProjectedPlateArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ProjectedPlateArea, us, nfs));
         if (ratingModel.ProjectedPlateArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ActualEffectivePlateArea, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ActualEffectivePlateArea, us, nfs));
         if (ratingModel.ActualEffectivePlateArea.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.PlateWallThickness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.PlateWallThickness, us, nfs));
         if (ratingModel.PlateWallThickness.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.PlatePitch, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.PlatePitch, us, nfs));
         if (ratingModel.PlatePitch.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.NumberOfPlates, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfPlates, us, nfs));
         if (ratingModel.NumberOfPlates.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ColdSidePasses, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ColdSidePasses, us, nfs));
         if (ratingModel.ColdSidePasses.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HotSidePasses, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HotSidePasses, us, nfs));
         if (ratingModel.HotSidePasses.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.PortDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.PortDiameter, us, nfs));
         if (ratingModel.PortDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.HorizontalPortDistance, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.HorizontalPortDistance, us, nfs));
         if (ratingModel.HorizontalPortDistance.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.VerticalPortDistance, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.VerticalPortDistance, us, nfs));
         if (ratingModel.VerticalPortDistance.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.CompressedPlatePackLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.CompressedPlatePackLength, us, nfs));
         if (ratingModel.CompressedPlatePackLength.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected HeatExchangerControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionHeatExchangerControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionHeatExchangerControl", HeatExchangerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
