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
using Prosimo.UnitOperations.HeatTransfer;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for HeaterControl.
   /// </summary>
   [Serializable]
   public class HeaterControl : TwoStreamUnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public Heater Heater {
         get { return (Heater)this.solvable; }
         set { this.solvable = value; }
      }

      public HeaterControl() {
      }

      public HeaterControl(Flowsheet flowsheet, Point location, Heater heater)
         : base(flowsheet, location, heater) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_W, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.Heater.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // HeaterControl
      //   // 
      //   this.Name = "HeaterControl";
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

      //protected override void DrawSlots() {
      //   this.DrawSlot(this, this.GetStreamInSlotPoint(), SlotPosition.Left, SlotDirection.In);
      //   this.DrawSlot(this, this.GetStreamOutSlotPoint(), SlotPosition.Right, SlotDirection.Out);
      //}

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new HeaterEditor(this);
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
            this.BackgroundImage = UI.IMAGES.HEATER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.HEATER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.HEATER_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.HEATER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Heater: ");
         sb.Append(this.Heater.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Heater.PressureDrop, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Heater.PressureDrop, us, nfs));
         if (this.Heater.PressureDrop.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Heater.HeatLoss, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Heater.HeatLoss, us, nfs));
         if (this.Heater.HeatLoss.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Heater.HeatInput, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Heater.HeatInput, us, nfs));
         if (this.Heater.HeatInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected HeaterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionHeaterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionHeaterControl", HeaterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
