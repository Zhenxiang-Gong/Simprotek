using System;
using System.Collections;
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
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for ElectrostaticPrecipitatorControl.
   /// </summary>
   [Serializable]
   public class ElectrostaticPrecipitatorControl : TwoStreamUnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public ElectrostaticPrecipitator ElectrostaticPrecipitator {
         get { return (ElectrostaticPrecipitator)this.solvable; }
         set { this.solvable = value; }
      }

      public ElectrostaticPrecipitatorControl() {
      }

      public ElectrostaticPrecipitatorControl(Flowsheet flowsheet, Point location, ElectrostaticPrecipitator electrostaticPrecipitator)
         : base(flowsheet, location, electrostaticPrecipitator) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.ElectrostaticPrecipitator.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // ElectrostaticPrecipitatorControl
      //   // 
      //   this.Name = "ElectrostaticPrecipitatorControl";
      //   //this.Size = new System.Drawing.Size(96, 72);
      //}

      public override bool HitTestStreamIn(Point p) {
         bool hit = false;
         Point slot = this.GetStreamInSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(0, slot.Y - UI.SLOT_DELTA, 0, slot.Y + UI.SLOT_DELTA);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();

         return hit;
      }

      public override bool HitTestStreamOut(Point p) {
         bool hit = false;
         Point slot = this.GetStreamOutSlotPoint();
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(this.Width, slot.Y - UI.SLOT_DELTA, this.Width, slot.Y + UI.SLOT_DELTA);
         //Pen pen = new Pen(Color.Black, 20);
         //Graphics g = this.CreateGraphics();
         hit = gp.IsOutlineVisible(p, penBlack20, g);

         //pen.Dispose();
         //g.Dispose();
         gp.Dispose();

         return hit;
      }

      //protected override void DoThePaint()
      //{
      //   //this.DrawBorder();
      //   this.DrawSelection();
      //   this.DrawSlots();
      //   this.UpdateNameControlLocation();
      //}

      //protected override void DrawSlots()
      //{
      //   this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
      //   this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      //}

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new ElectrostaticPrecipitatorEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      protected override void UpdateBackImage() {
         if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.ELECTROSTATICPRECIPITATOR_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Electrostatic Precipitator: ");
         sb.Append(this.ElectrostaticPrecipitator.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(ToPrintVarList());

         return sb.ToString();
      }

      protected ElectrostaticPrecipitatorControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionElectrostaticPrecipitatorControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionElectrostaticPrecipitatorControl", ElectrostaticPrecipitatorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.GasPressureDrop, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.GasPressureDrop, us, nfs));
         //if (this.ElectrostaticPrecipitator.GasPressureDrop.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.CollectionEfficiency, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.CollectionEfficiency, us, nfs));
         //if (this.ElectrostaticPrecipitator.CollectionEfficiency.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.InletParticleLoading, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.InletParticleLoading, us, nfs));
         //if (this.ElectrostaticPrecipitator.InletParticleLoading.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.OutletParticleLoading, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.OutletParticleLoading, us, nfs));
         //if (this.ElectrostaticPrecipitator.OutletParticleLoading.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.ParticleCollectionRate, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.ParticleCollectionRate, us, nfs));
         //if (this.ElectrostaticPrecipitator.ParticleCollectionRate.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
         //if (this.ElectrostaticPrecipitator.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.DriftVelocity, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.DriftVelocity, us, nfs));
         //if (this.ElectrostaticPrecipitator.DriftVelocity.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

         //sb.Append(GetVariableName(this.ElectrostaticPrecipitator.TotalSurfaceArea, us));
         //sb.Append(" = ");
         //sb.Append(GetVariableValue(this.ElectrostaticPrecipitator.TotalSurfaceArea, us, nfs));
         //if (this.ElectrostaticPrecipitator.TotalSurfaceArea.IsSpecified)
         //   sb.Append(" * ");
         //sb.Append("\r\n");

