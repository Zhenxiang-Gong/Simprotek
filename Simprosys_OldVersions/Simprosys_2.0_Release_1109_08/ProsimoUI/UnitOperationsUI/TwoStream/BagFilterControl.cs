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
   /// Summary description for BagFilterControl.
   /// </summary>
   [Serializable]
   public class BagFilterControl : FabricFilterControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public BagFilter BagFilter {
         get { return (BagFilter)this.Solvable; }
         set { this.Solvable = value; }
      }

      public BagFilterControl() {
      }

      public BagFilterControl(Flowsheet flowsheet, Point location, BagFilter bagFilter)
         : base(flowsheet, location, bagFilter) {
         //InitializeComponent();
      }

      //private void InitializeComponent() {
      //}

      protected override void UpdateBackImage() {
         if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.BAGFILTER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override Point GetStreamInSlotPoint() {
         // this point is referenced to this control
         // middle of the left side
         Point p = new Point(0, 0);
         p.Y += this.Height / 2;

         // move it down 1/6
         p.Y += this.Height / 6;

         return p;
      }

      public override Point GetStreamOutSlotPoint() {
         // this point is referenced to this control
         // middle of the right side
         Point p = new Point(0, 0);
         p.X += this.Width;
         p.Y += this.Height / 2;

         // move it up 1/6
         p.Y -= this.Height / 6;

         return p;
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new BagFilterEditor(this);
            this.editor.Owner = (Form)this.flowsheet.Parent;
            this.editor.Show();
         }
         else {
            if (this.editor.WindowState.Equals(FormWindowState.Minimized))
               this.editor.WindowState = FormWindowState.Normal;
            this.editor.Activate();
         }
      }

      public override string ToPrintToolTip() {
         return this.ToPrint();
      }

      public override string ToPrint() {
         UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Bag Filter: ");
         sb.Append(this.BagFilter.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(base.ToPrint());

         sb.Append(GetVariableName(this.BagFilter.BagDiameter, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.BagFilter.BagDiameter, us, nfs));
         if (this.BagFilter.BagDiameter.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.BagFilter.BagLength, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.BagFilter.BagLength, us, nfs));
         if (this.BagFilter.BagLength.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         sb.Append(GetVariableName(this.BagFilter.NumberOfBags, us));
         sb.Append(" = ");
         sb.Append(GetVariableValue(this.BagFilter.NumberOfBags, us, nfs));
         if (this.BagFilter.NumberOfBags.IsSpecified)
            sb.Append(" * ");
         sb.Append("\r\n");

         return sb.ToString();
      }

      protected BagFilterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionBagFilterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionBagFilterControl", BagFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
