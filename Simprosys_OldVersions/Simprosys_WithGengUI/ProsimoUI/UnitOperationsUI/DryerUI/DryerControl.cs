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
using Prosimo.UnitOperations.Drying;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;
using Prosimo.Materials;

namespace ProsimoUI.UnitOperationsUI.DryerUI
{
	/// <summary>
	/// Summary description for DryerControl.
	/// </summary>
   [Serializable]
   public class DryerControl : UnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static PointOrientation GAS_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation GAS_OUTLET_ORIENTATION = PointOrientation.E;
      public static PointOrientation MATERIAL_INLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation MATERIAL_OUTLET_ORIENTATION = PointOrientation.S;
      
      public Dryer Dryer
      {
         get {return (Dryer)this.Solvable;}
         set {this.Solvable = value;}
      }

      public DryerControl()
      {
      }

		public DryerControl(Flowsheet flowsheet, Point location, Dryer dryer) :
         base(flowsheet, location, dryer)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_W, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Dryer.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // DryerControl
         // 
         this.Name = "DryerControl";
         this.Size = new System.Drawing.Size(96, 72);
      }

      public bool HitTestGasIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y-UI.SLOT_DELTA, 0, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestGasOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetGasOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y-UI.SLOT_DELTA, this.Width, slot.Y+UI.SLOT_DELTA);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestMaterialIn(Point p)
      {
         bool hit = false;
         Point slot = this.GetMaterialInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(slot.X-UI.SLOT_DELTA, 0, slot.X+UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestMaterialOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetMaterialOutSlotPoint();
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
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestMaterialIn(p) || HitTestMaterialOut(p))
            {
               if (HitTestGasIn(p))
                  idx = Dryer.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Dryer.GAS_OUTLET_INDEX;
               else if (HitTestMaterialIn(p))
                  idx = Dryer.MATERIAL_INLET_INDEX;
               else if (HitTestMaterialOut(p))
                  idx = Dryer.MATERIAL_OUTLET_INDEX;
               
               if (this.Dryer.CanConnect(idx))
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
               if (HitTestGasIn(p))
                  idx = Dryer.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Dryer.GAS_OUTLET_INDEX;
               else if (HitTestMaterialIn(p))
                  idx = Dryer.MATERIAL_INLET_INDEX;
               else if (HitTestMaterialOut(p))
                  idx = Dryer.MATERIAL_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Dryer.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Dryer.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as DryerControl);
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
         this.DrawSlot(this, this.GetMaterialInSlotPoint(), SlotPosition.Up, SlotDirection.In);
         this.DrawSlot(this, this.GetGasInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetGasOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
         this.DrawSlot(this, this.GetMaterialOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

       public override void Edit()
       {
           if (this.Editor == null)
           {
               this.Editor = new DryerEditor(this);
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
//         if (this.Dryer.MaterialInlet.MaterialStateType == MaterialStateType.Liquid)
         if (this.Dryer is LiquidDryer)
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_LIQUID_CTRL_NOT_SOLVED_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_LIQUID_CTRL_SOLVED_WITH_WARNING_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_LIQUID_CTRL_SOLVED_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_LIQUID_CTRL_SOLVE_FAILED_IMG;
            }
         }
//         else if (this.Dryer.MaterialInlet.MaterialStateType == MaterialStateType.Solid)
         else if (this.Dryer is SolidDryer)
         {
            if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_SOLID_CTRL_NOT_SOLVED_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_SOLID_CTRL_SOLVED_WITH_WARNING_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.Solved))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_SOLID_CTRL_SOLVED_IMG;
            }
            else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
            {
               this.BackgroundImage = UI.IMAGES.DRYER_SOLID_CTRL_SOLVE_FAILED_IMG;
            }
         }
      }

      public Point GetGasInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the left side
         Point p1 = this.GetGasInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;
         return p; 
      }

      public Point GetGasOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the right side
         Point p1 = this.GetGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;
         return p;
      }

      public Point GetMaterialInConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetMaterialInSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetMaterialInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0,0);
         p.X += this.Width/2;
         return p; 
      }

      public Point GetMaterialOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the down side
         Point p1 = this.GetMaterialOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetMaterialOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the down side
         Point p = new Point(0,0);
         p.Y += this.Height;
         p.X += this.Width/2;
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

         if (ad == Dryer.GAS_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Dryer.GAS_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasInConnectionPoint();
            PointOrientation uoOrientation = DryerControl.GAS_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Dryer.GAS_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Dryer.GAS_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasOutConnectionPoint();
            PointOrientation uoOrientation = DryerControl.GAS_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Dryer.MATERIAL_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Dryer.MATERIAL_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetMaterialInConnectionPoint();
            PointOrientation uoOrientation = DryerControl.MATERIAL_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Dryer.MATERIAL_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Dryer.MATERIAL_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetMaterialOutConnectionPoint();
            PointOrientation uoOrientation = DryerControl.MATERIAL_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }

         if (conn != null)
         {
             this.flowsheet.ConnectionManager.AddConnection(conn);
             //this.flowsheet.ConnectionManager.Connections.Add(conn);
             //this.flowsheet.ConnectionManager.DrawConnections();
         }
         return conn;
      }

      protected override void ShowConnectionPoints(Point p)
      {
         if (this.flowsheet.Activity == FlowsheetActivity.AddingConnStepOne)
         {
            int idx = -1;
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestMaterialIn(p) || HitTestMaterialOut(p))
            {
               if (HitTestGasIn(p))
                  idx = Dryer.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Dryer.GAS_OUTLET_INDEX;
               else if (HitTestMaterialIn(p))
                  idx = Dryer.MATERIAL_INLET_INDEX;
               else if (HitTestMaterialOut(p))
                  idx = Dryer.MATERIAL_OUTLET_INDEX;
               
               if (this.Dryer.CanConnect(idx))
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
               if (HitTestGasIn(p))
                  idx = Dryer.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Dryer.GAS_OUTLET_INDEX;
               else if (HitTestMaterialIn(p))
                  idx = Dryer.MATERIAL_INLET_INDEX;
               else if (HitTestMaterialOut(p))
                  idx = Dryer.MATERIAL_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Dryer.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Dryer: ");
         sb.Append(this.Dryer.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Dryer.GasPressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.GasPressureDrop, us, nfs));
         if (this.Dryer.GasPressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Dryer.HeatLoss, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.HeatLoss, us, nfs));
         if (this.Dryer.HeatLoss.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Dryer.HeatInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.HeatInput, us, nfs));
         if (this.Dryer.HeatInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Dryer.WorkInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.WorkInput, us, nfs));
         if (this.Dryer.WorkInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Dryer.HeatLossByTransportDevice, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.HeatLossByTransportDevice, us, nfs));
         if (this.Dryer.HeatLossByTransportDevice.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Dryer.MoistureEvaporationRate, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.MoistureEvaporationRate, us, nfs));
         if (this.Dryer.MoistureEvaporationRate.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Dryer.SpecificHeatConsumption, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.SpecificHeatConsumption, us, nfs));
         if (this.Dryer.SpecificHeatConsumption.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Dryer.ThermalEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.ThermalEfficiency, us, nfs));
         if (this.Dryer.ThermalEfficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Dryer.FractionOfMaterialLostToGasOutlet, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.FractionOfMaterialLostToGasOutlet, us, nfs));
         if (this.Dryer.FractionOfMaterialLostToGasOutlet.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Dryer.GasOutletMaterialLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Dryer.GasOutletMaterialLoading, us, nfs));
         if (this.Dryer.GasOutletMaterialLoading.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         if (this.Dryer.ScopingModel != null)
            sb.Append(this.ToPrintScoping());

         return sb.ToString();
      }

      public string ToPrintScoping()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Dryer Scoping:");
         sb.Append("\r\n");

         DryerScopingModel scopingModel = this.Dryer.ScopingModel;

         sb.Append(UI.GetVariableName(scopingModel.GasVelocity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(scopingModel.GasVelocity, us, nfs));
         if (scopingModel.GasVelocity.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         string crossSectionTypeStr = "";
         if (scopingModel.CrossSectionType == CrossSectionType.Circle)
            crossSectionTypeStr = "Circle";
         else if (scopingModel.CrossSectionType == CrossSectionType.Rectangle)
            crossSectionTypeStr = "Rectangle";
         sb.Append("Cross Section Type");
         sb.Append(" = ");
         sb.Append(crossSectionTypeStr);
         sb.Append("\r\n");

         if (scopingModel.CrossSectionType == CrossSectionType.Circle)
         {
            sb.Append(UI.GetVariableName(scopingModel.Diameter, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.Diameter, us, nfs));
            if (scopingModel.Diameter.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(scopingModel.Length, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.Length, us, nfs));
            if (scopingModel.Length.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(scopingModel.LengthDiameterRatio, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.LengthDiameterRatio, us, nfs));
            if (scopingModel.LengthDiameterRatio.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }
         else if (scopingModel.CrossSectionType == CrossSectionType.Rectangle)
         {
            sb.Append(UI.GetVariableName(scopingModel.Width, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.Width, us, nfs));
            if (scopingModel.Width.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");

            sb.Append(UI.GetVariableName(scopingModel.Length, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.Length, us, nfs));
            if (scopingModel.Length.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(scopingModel.Height, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.Height, us, nfs));
            if (scopingModel.Height.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(scopingModel.LengthWidthRatio, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.LengthWidthRatio, us, nfs));
            if (scopingModel.LengthWidthRatio.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         
            sb.Append(UI.GetVariableName(scopingModel.HeightWidthRatio, us));
            sb.Append(" = ");
            sb.Append(UI.GetVariableValue(scopingModel.HeightWidthRatio, us, nfs));
            if (scopingModel.HeightWidthRatio.IsSpecified)
               sb.Append(" * ");
            sb.Append("\r\n");
         }

         return sb.ToString();
      }

      protected DryerControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDryerControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionDryerControl", DryerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
