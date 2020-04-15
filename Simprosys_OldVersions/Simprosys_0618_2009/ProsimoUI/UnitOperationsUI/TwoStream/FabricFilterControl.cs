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
   /// Summary description for FabricFilterControl.
   /// </summary>
   [Serializable]
   public class FabricFilterControl : TwoStreamUnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public FabricFilter FabricFilter {
         get { return (FabricFilter)this.solvable; }
         set { this.solvable = value; }
      }

      public FabricFilterControl() {
      }

      public FabricFilterControl(Flowsheet flowsheet, Point location, FabricFilter fabricFilter)
         : base(flowsheet, location, fabricFilter) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.FabricFilter.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // FabricFilterControl
      //   // 
      //   this.Name = "FabricFilterControl";
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
            this.editor = new FabricFilterEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      protected override void Solvable_SolveComplete(object sender, SolveState solveState) {
         // The solve state has been set on the object,
         // so we don't need to use the info in the event.
         this.UpdateBackImage();
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      protected FabricFilterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionFabricFilterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionFabricFilterControl", FabricFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//public override string ToPrint() {
//   UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
//   string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
//   StringBuilder sb = new StringBuilder();

//   sb.Append(GetVariableName(this.FabricFilter.GasPressureDrop, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.GasPressureDrop, us, nfs));
//   if (this.FabricFilter.GasPressureDrop.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.CollectionEfficiency, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.CollectionEfficiency, us, nfs));
//   if (this.FabricFilter.CollectionEfficiency.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.InletParticleLoading, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.InletParticleLoading, us, nfs));
//   if (this.FabricFilter.InletParticleLoading.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.OutletParticleLoading, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.OutletParticleLoading, us, nfs));
//   if (this.FabricFilter.OutletParticleLoading.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.ParticleCollectionRate, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.ParticleCollectionRate, us, nfs));
//   if (this.FabricFilter.ParticleCollectionRate.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.MassFlowRateOfParticleLostToGasOutlet, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.MassFlowRateOfParticleLostToGasOutlet, us, nfs));
//   if (this.FabricFilter.MassFlowRateOfParticleLostToGasOutlet.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.GasToClothRatio, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.GasToClothRatio, us, nfs));
//   if (this.FabricFilter.GasToClothRatio.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   sb.Append(GetVariableName(this.FabricFilter.TotalFilteringArea, us));
//   sb.Append(" = ");
//   sb.Append(GetVariableValue(this.FabricFilter.TotalFilteringArea, us, nfs));
//   if (this.FabricFilter.TotalFilteringArea.IsSpecified)
//      sb.Append(" * ");
//   sb.Append("\r\n");

//   return sb.ToString();
//}
