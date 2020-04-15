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
	/// Summary description for CompressorControl.
	/// </summary>
   [Serializable]
   public class CompressorControl : TwoStreamUnitOpControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public Compressor Compressor
      {
         get {return (Compressor)this.Solvable;}
         set {this.Solvable = value;}
      }

      public CompressorControl()
      {
      }

		public CompressorControl(Flowsheet flowsheet, Point location, Compressor compressor) :
         base(flowsheet, location, compressor)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_H, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Compressor.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // CompressorControl
         // 
         this.Name = "CompressorControl";
         this.Size = new System.Drawing.Size(140, 112);
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
            this.Editor = new CompressorEditor(this);
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
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
         {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_PARTIALLY_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.COMPRESSOR_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;

         // move it up 1/4
         p.Y -= this.Height/4;

         return p; 
      }

      public override Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;

         // move it up 1/4
         p.Y -= this.Height/4;
         
         return p;
      }

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Compressor: ");
         sb.Append(this.Compressor.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Compressor.OutletInletPressureRatio, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.OutletInletPressureRatio, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Compressor.AdiabaticExponent, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.AdiabaticExponent, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Compressor.AdiabaticEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.AdiabaticEfficiency, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Compressor.PolytropicExponent, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.PolytropicExponent, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Compressor.PolytropicEfficiency, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.PolytropicEfficiency, us, nfs));
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Compressor.PowerInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Compressor.PowerInput, us, nfs));
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

      protected CompressorControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCompressorControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionCompressorControl", CompressorControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
