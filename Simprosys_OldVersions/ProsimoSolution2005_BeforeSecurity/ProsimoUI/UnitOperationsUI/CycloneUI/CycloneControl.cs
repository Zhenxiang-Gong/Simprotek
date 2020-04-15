using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ProsimoUI;
using Prosimo.UnitOperations;
using ProsimoUI.ProcessStreamsUI;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;
using Prosimo;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneControl.
	/// </summary>
   [Serializable]
   public class CycloneControl : UnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public static PointOrientation MIXTURE_INLET_ORIENTATION = PointOrientation.W;
      public static PointOrientation FLUID_OUTLET_ORIENTATION = PointOrientation.N;
      public static PointOrientation PARTICLE_OUTLET_ORIENTATION = PointOrientation.S;

      public Cyclone Cyclone
      {
         get {return (Cyclone)this.Solvable;}
         set {this.Solvable = value;}
      }

      public CycloneControl()
      {
      }

		public CycloneControl(Flowsheet flowsheet, Point location, Cyclone cyclone) :
         base(flowsheet, location, cyclone)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Cyclone.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // CycloneControl
         // 
         this.Name = "CycloneControl";
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
         gp.AddLine(slot.X-UI.SLOT_DELTA, 0, slot.X+UI.SLOT_DELTA, 0);
         hit = gp.IsOutlineVisible(p, new Pen(Color.Black, 20), this.CreateGraphics());
         return hit;
      }

      public bool HitTestParticleOut(Point p)
      {
         bool hit = false;
         Point slot = this.GetParticleOutSlotPoint();
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
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestParticleOut(p))
            {
               if (HitTestGasIn(p))
                  idx = Cyclone.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Cyclone.GAS_OUTLET_INDEX;
               else if (HitTestParticleOut(p))
                  idx = Cyclone.PARTICLE_OUTLET_INDEX;

               if (this.Cyclone.CanConnect(idx))
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
                  idx = Cyclone.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Cyclone.GAS_OUTLET_INDEX;
               else if (HitTestParticleOut(p))
                  idx = Cyclone.PARTICLE_OUTLET_INDEX;
               
               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Cyclone.CanAttachStream(ctrl.ProcessStreamBase, idx))
               {
                  // update the model, the UI will be updated in the listener
                  ErrorMessage error = this.Cyclone.AttachStream(ctrl.ProcessStreamBase, idx);
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
         this.flowsheet.ConnectionManager.UpdateConnections(sender as CycloneControl);
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
         this.DrawSlot(this, this.GetGasInSlotPoint(), SlotPosition.Left, SlotDirection.In);
         this.DrawSlot(this, this.GetGasOutSlotPoint(), SlotPosition.Up, SlotDirection.Out);
         this.DrawSlot(this, this.GetParticleOutSlotPoint(), SlotPosition.Down, SlotDirection.Out);
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new CycloneEditor(this);
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
            this.BackgroundImage = UI.IMAGES.CYCLONE_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.CYCLONE_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.CYCLONE_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.CYCLONE_CTRL_SOLVE_FAILED_IMG;
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

         // move it 1/4 up
         p.Y -= this.Height/4;

         return p; 
      }

      public Point GetGasOutConnectionPoint()
      {
         // this point is referenced to the flowsheet
         // middle of the up side
         Point p1 = this.GetGasOutSlotPoint();
         Point p2 = this.Location;
         return new Point(p1.X + p2.X, p1.Y + p2.Y);
      }

      public Point GetGasOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the up side
         Point p = new Point(0,0);
         p.X += this.Width/2;
         return p; 
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

         if (ad == Cyclone.GAS_INLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.OUT_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetOutConnectionPoint();
            PointOrientation strOrientation = ctrl.OutOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Cyclone.GAS_INLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasInConnectionPoint();
            PointOrientation uoOrientation = CycloneControl.MIXTURE_INLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Cyclone.GAS_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Cyclone.GAS_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetGasOutConnectionPoint();
            PointOrientation uoOrientation = CycloneControl.FLUID_OUTLET_ORIENTATION;
            ConnectionPoint uoConnPoint = new ConnectionPoint(uoIdx, uoName, uoPoint, uoOrientation);
               
            conn = new SolvableConnection(this.flowsheet, strConnPoint, uoConnPoint, streamType);
         }
         else if (ad == Cyclone.PARTICLE_OUTLET_INDEX)
         {
            int strIdx = ProcessStreamBaseControl.IN_INDEX;
            string strName = ctrl.Solvable.Name;
            Point strPoint = ctrl.GetInConnectionPoint();
            PointOrientation strOrientation = ctrl.InOrientation;
            ConnectionPoint strConnPoint = new ConnectionPoint(strIdx, strName, strPoint, strOrientation);

            int uoIdx = Cyclone.PARTICLE_OUTLET_INDEX;
            string uoName = this.Solvable.Name;
            Point uoPoint = this.GetParticleOutConnectionPoint();
            PointOrientation uoOrientation = CycloneControl.PARTICLE_OUTLET_ORIENTATION;
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
            if (HitTestGasIn(p) || HitTestGasOut(p) || HitTestParticleOut(p))
            {
               if (HitTestGasIn(p))
                  idx = Cyclone.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Cyclone.GAS_OUTLET_INDEX;
               else if (HitTestParticleOut(p))
                  idx = Cyclone.PARTICLE_OUTLET_INDEX;

               if (this.Cyclone.CanConnect(idx))
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
                  idx = Cyclone.GAS_INLET_INDEX;
               else if (HitTestGasOut(p))
                  idx = Cyclone.GAS_OUTLET_INDEX;
               else if (HitTestParticleOut(p))
                  idx = Cyclone.PARTICLE_OUTLET_INDEX;

               ProcessStreamBaseControl ctrl = (ProcessStreamBaseControl)this.flowsheet.firstStepCtrl;
               if (this.Cyclone.CanAttachStream(ctrl.ProcessStreamBase, idx))
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

         sb.Append("Cyclone: ");
         sb.Append(this.Cyclone.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Cyclone.GasPressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cyclone.GasPressureDrop, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Cyclone.CollectionEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cyclone.CollectionEfficiency, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Cyclone.InletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cyclone.InletParticleLoading, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Cyclone.OutletParticleLoading, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cyclone.OutletParticleLoading, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Cyclone.MassFlowRateOfParticleLostToGasOutlet, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cyclone.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         sb.Append("\r\n");

         if (this.Cyclone.CurrentRatingModel != null)
            sb.Append(this.ToPrintRating());

         return sb.ToString();
      }

      public string ToPrintRating()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Cyclone Rating:");
         sb.Append("\r\n");

         CycloneRatingModel ratingModel = this.Cyclone.CurrentRatingModel;

         sb.Append(UI.GetVariableName(ratingModel.NumberOfCyclones, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NumberOfCyclones, us, nfs));
         sb.Append("\r\n");

         string particleTypeGroupStr = "";
         if (this.Cyclone.CurrentRatingModel.ParticleTypeGroup == ParticleTypeGroup.A)
            particleTypeGroupStr = "A";
         else if (this.Cyclone.CurrentRatingModel.ParticleTypeGroup == ParticleTypeGroup.B)
            particleTypeGroupStr = "B";
         else if (this.Cyclone.CurrentRatingModel.ParticleTypeGroup == ParticleTypeGroup.C)
            particleTypeGroupStr = "C";
         else if (this.Cyclone.CurrentRatingModel.ParticleTypeGroup == ParticleTypeGroup.D)
            particleTypeGroupStr = "D";
         sb.Append("Particle Type Group");
         sb.Append(" = ");
         sb.Append(particleTypeGroupStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ParticleDensity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ParticleDensity, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.ParticleBulkDensity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ParticleBulkDensity, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.CutParticleDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.CutParticleDiameter, us, nfs));
         sb.Append("\r\n");

         string inletConfigurationStr = "";
         if (this.Cyclone.CurrentRatingModel.InletConfiguration == CycloneInletConfiguration.Scroll)
            inletConfigurationStr = "Scroll";
         else if (this.Cyclone.CurrentRatingModel.InletConfiguration == CycloneInletConfiguration.Tangential)
            inletConfigurationStr = "Tangential";
         else if (this.Cyclone.CurrentRatingModel.InletConfiguration == CycloneInletConfiguration.Volute)
            inletConfigurationStr = "Volute";
         sb.Append("Inlet Configuration");
         sb.Append(" = ");
         sb.Append(inletConfigurationStr);
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.InletWidth, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.InletWidth, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.InletHeight, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.InletHeight, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.InletHeightToWidthRatio, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.InletHeightToWidthRatio, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.CycloneDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.CycloneDiameter, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.OutletInnerDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.OutletInnerDiameter, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.OutletWallThickness, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.OutletWallThickness, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.OutletTubeLengthBelowRoof, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.OutletTubeLengthBelowRoof, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.OutletBelowRoofToInletHeightRatio, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.OutletBelowRoofToInletHeightRatio, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.DiplegDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.DiplegDiameter, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ExternalVesselDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ExternalVesselDiameter, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.InletVelocity, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.InletVelocity, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ConeAngle, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ConeAngle, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(ratingModel.NaturalVortexLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.NaturalVortexLength, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.BarrelLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.BarrelLength, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.ConeLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.ConeLength, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(ratingModel.BarrelPlusConeLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(ratingModel.BarrelPlusConeLength, us, nfs));
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected CycloneControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCycloneControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionCycloneControl", CycloneControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
