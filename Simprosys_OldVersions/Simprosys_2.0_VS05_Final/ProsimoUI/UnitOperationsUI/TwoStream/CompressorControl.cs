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
using Prosimo.UnitOperations.FluidTransport;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for CompressorControl.
   /// </summary>
   [Serializable]
   public class CompressorControl : TwoStreamUnitOpControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public Compressor Compressor {
         get { return (Compressor)this.solvable; }
         set { this.solvable = value; }
      }

      public CompressorControl() {
      }

      public CompressorControl(Flowsheet flowsheet, Point location, Compressor compressor)
         : base(flowsheet, location, compressor) {
      }

      //private void Init() {
      //   this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
      //   UI.SetStatusColor(this, this.Compressor.SolveState);
      //   this.UpdateBackImage();
      //}

      //private void InitializeComponent() {
      //   // 
      //   // CompressorControl
      //   // 
      //   this.Name = "CompressorControl";
      //   //this.Size = new System.Drawing.Size(140, 112);
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
            this.editor = new CompressorEditor(this);
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
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;

         // move it up 1/4
         p.Y -= this.Height / 4;

         return p;
      }

      public override Point GetStreamOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;

         // move it up 1/4
         p.Y -= this.Height / 4;

         return p;
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Compressor: ");
         sb.Append(this.Compressor.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.OutletInletPressureRatio, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.OutletInletPressureRatio, us, nfs));
         if (this.Compressor.OutletInletPressureRatio.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.AdiabaticExponent, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.AdiabaticExponent, us, nfs));
         if (this.Compressor.AdiabaticExponent.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.AdiabaticEfficiency, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.AdiabaticEfficiency, us, nfs));
         if (this.Compressor.AdiabaticEfficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.PolytropicExponent, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.PolytropicExponent, us, nfs));
         if (this.Compressor.PolytropicExponent.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.PolytropicEfficiency, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.PolytropicEfficiency, us, nfs));
         if (this.Compressor.PolytropicEfficiency.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.Compressor.PowerInput, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.Compressor.PowerInput, us, nfs));
         if (this.Compressor.PowerInput.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         string compressionProcessStr = "";
         if (this.Compressor.CompressionProcess == CompressionProcess.Adiabatic)
            compressionProcessStr = "Adiabatic";
         else if (this.Compressor.CompressionProcess == CompressionProcess.Isothermal)
            compressionProcessStr = "Isothermal";
         else if (this.Compressor.CompressionProcess == CompressionProcess.Polytropic)
            compressionProcessStr = "Polytropic";
         sb.Append("Compression Process");
         sb.Append(" = ");
         sb.Append(compressionProcessStr);
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected CompressorControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionCompressorControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionCompressorControl", CompressorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
