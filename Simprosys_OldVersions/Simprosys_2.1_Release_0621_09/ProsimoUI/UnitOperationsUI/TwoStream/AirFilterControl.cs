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
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream {
   /// <summary>
   /// Summary description for AirFilterControl.
   /// </summary>
   [Serializable]
   public class AirFilterControl : FabricFilterControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public AirFilter AirFilter {
         get { return (AirFilter)this.Solvable; }
         set { this.Solvable = value; }
      }

      public AirFilterControl() {
      }

      public AirFilterControl(Flowsheet flowsheet, Point location, AirFilter airFilter)
         : base(flowsheet, location, airFilter) {
         //InitializeComponent();
      }

      //private void InitializeComponent() {
      //}

      protected override void UpdateBackImage() {
         if (this.solvable.SolveState.Equals(SolveState.NotSolved)) {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolvedWithWarning)) {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.Solved)) {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVED_IMG;
         }
         else if (this.solvable.SolveState.Equals(SolveState.SolveFailed)) {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override void Edit() {
         if (this.editor == null) {
            this.editor = new FabricFilterEditor(this);
            this.editor.Text = "Air Filter: " + this.AirFilter.Name;
            (this.editor as FabricFilterEditor).FabricFilterGroupBox.Text = "Air Filter";
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

         sb.Append("Air Filter: ");
         sb.Append(this.AirFilter.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");

         sb.Append(ToPrintVarList());

         return sb.ToString();
      }

      protected AirFilterControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {

         //InitializeComponent();
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = info.GetInt32("ClassPersistenceVersionAirFilterControl");
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionAirFilterControl", AirFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
