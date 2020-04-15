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
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for BagFilterControl.
	/// </summary>
   [Serializable]
   public class BagFilterControl : FabricFilterControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public BagFilter BagFilter
      {
         get {return (BagFilter)this.Solvable;}
         set {this.Solvable = value;}
      }

      public BagFilterControl()
      {
      }

		public BagFilterControl(Flowsheet flowsheet, Point location, BagFilter bagFilter) :
         base(flowsheet, location, bagFilter)
		{
         InitializeComponent();
      }

		private void InitializeComponent()
		{
      }

      protected override void UpdateBackImage()
      {
         if (this.Solvable.SolveState.Equals(SolveState.NotSolved))
         {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.PartiallySolved))
         {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_PARTIALLY_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamInSlotPoint()
      {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0,0);
         p.Y += this.Height/2;

         // move it down 1/6
         p.Y += this.Height/6;

         return p; 
      }

      public override Point GetStreamOutSlotPoint()
      {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0,0);
         p.X += this.Width;
         p.Y += this.Height/2;

         // move it up 1/6
         p.Y -= this.Height/6;

         return p;
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new BagFilterEditor(this);
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

      public override string ToPrintToolTip()
      {
         return this.ToPrint();
      }

      public override string ToPrint()
      {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Bag Filter: ");
         sb.Append(this.BagFilter.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(base.ToPrint());

         sb.Append(UI.GetVariableName(this.BagFilter.BagDiameter, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.BagFilter.BagDiameter, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.BagFilter.BagLength, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.BagFilter.BagLength, us, nfs));
         sb.Append("\r\n");

         sb.Append(UI.GetVariableName(this.BagFilter.NumberOfBags, us));
         sb.Append(" = ");
         sb.Append(UI.GetVariableValue(this.BagFilter.NumberOfBags, us, nfs));
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected BagFilterControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionBagFilterControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionBagFilterControl", BagFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
