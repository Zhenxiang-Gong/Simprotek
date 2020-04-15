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
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for CoolerControl.
	/// </summary>
   [Serializable]
   public class CoolerControl : TwoStreamUnitOpControl
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 

      public Cooler Cooler
      {
         get {return (Cooler)this.Solvable;}
         set {this.Solvable = value;}
      }

      public CoolerControl()
      {
      }

      public CoolerControl(Flowsheet flowsheet, Point location, Cooler cooler) :
         base(flowsheet, location, cooler)
		{
			InitializeComponent();

         this.Size = new System.Drawing.Size(UI.UNIT_OP_CTRL_W, UI.UNIT_OP_CTRL_H);
         UI.SetStatusColor(this, this.Cooler.SolveState);
         this.UpdateBackImage();
      }

		private void InitializeComponent()
		{
         // 
         // CoolerControl
         // 
         this.Name = "CoolerControl";
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
            this.Editor = new CoolerEditor(this);
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
            this.BackgroundImage = UI.IMAGES.COOLER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.COOLER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.COOLER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.COOLER_CTRL_SOLVE_FAILED_IMG;
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

         sb.Append("Cooler: ");
         sb.Append(this.Cooler.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(UI.GetVariableName(this.Cooler.PressureDrop, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cooler.PressureDrop, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Cooler.HeatLoss, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cooler.HeatLoss, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.Cooler.HeatInput, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.Cooler.HeatInput, us, nfs));
         sb.Append("\r\n");
         
         return sb.ToString();
      }

      protected CoolerControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionCoolerControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionCoolerControl", CoolerControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
