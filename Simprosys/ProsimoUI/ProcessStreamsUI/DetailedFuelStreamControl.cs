using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;

namespace ProsimoUI.ProcessStreamsUI {
   /// <summary>
   /// Summary description for ProcessStreamControl.
   /// </summary>
   [Serializable]
   public class DetailedFuelStreamControl : ProcessStreamBaseControl {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      internal protected override string SolvableTypeName
      {
         get { return "Fuel Stream"; }
      }

      public DetailedFuelStream DetailedFuelStream
      {
         get { return (DetailedFuelStream)this.Solvable; }
         set { this.Solvable = value; }
      }

      public DetailedFuelStreamControl() {
      }

      public DetailedFuelStreamControl(Flowsheet flowsheet, Point location, DetailedFuelStream detailedFuelStream)
         : this(flowsheet, location, detailedFuelStream, StreamOrientation.Right) {

         //InitializeComponent();

         //this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
         //UI.SetStatusColor(this, this.WaterStream.SolveState);
         //this.Orientation = StreamOrientation.Right;
         //this.UpdateBackImage();
         //this.flowsheet.ConnectionManager.UpdateConnections(this);
      }

      public DetailedFuelStreamControl(Flowsheet flowsheet, Point location, DetailedFuelStream detailedFuelStream, StreamOrientation orientation)
         : base(flowsheet, location, detailedFuelStream, orientation) {

         InitializeComponent();

         this.Size = new System.Drawing.Size(UI.STREAM_CTRL_W, UI.STREAM_CTRL_H);
         UI.SetStatusColor(this, this.DetailedFuelStream.SolveState);
         //this.Orientation = StreamOrientation.Right;
         this.UpdateBackImage();
         this.flowsheet.ConnectionManager.UpdateConnections(this);
         this.Name = "Fuel Stream: " + detailedFuelStream.Name;
      }


      private void InitializeComponent() {
         // 
         // ProcessStreamControl
         // 
         //this.Name = "WaterStreamControl";
         this.Size = new System.Drawing.Size(72, 32);
      }

      public override void Edit() {
         if (this.editor == null) {
            //this.editor = new ProcessStreamBaseEditor(this);
            this.editor = new DetailedFuelStreamEditor(this);
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
         if(this.solvable.SolveState.Equals(SolveState.NotSolved))
         {
            this.BackgroundImage = UI.IMAGES.FUEL_CTRL_NOT_SOLVED_RIGHT_IMG;
         }
         else if(this.solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.FUEL_CTRL_SOLVED_WITH_WARNING_RIGHT_IMG;
         }
         else if(this.solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.FUEL_CTRL_SOLVED_RIGHT_IMG;
         }
         else if(this.solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.FUEL_CTRL_SOLVE_FAILED_RIGHT_IMG;
         }

         UpdateBackImageOrientation();
      }

      protected DetailedFuelStreamControl(SerializationInfo info, StreamingContext context)
         : base(info, context) {
      }

      //public override void SetObjectData(SerializationInfo info, StreamingContext context) {
      //   base.SetObjectData(info, context);
      public override void SetObjectData() {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionDetailedFuelStreamControl", typeof(int));
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) {
         base.GetObjectData(info, context);
         info.AddValue("ClassPersistenceVersionDetailedFuelStreamControl", CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}

//public override string ToPrintToolTip() {
//   return this.ToPrint();
//}

//public override string ToPrint() {
//   //UnitSystem us = UnitSystemService.GetInstance().CurrentUnitSystem;
//   //string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
//   //StringBuilder sb = new StringBuilder();

//   //sb.Append("Fuel Stream: ");
//   //sb.Append(this.DetailedFuelStream.Name);
//   //sb.Append("\r\n");
//   //sb.Append(UI.UNDERLINE);
//   //sb.Append("\r\n");

//   //sb.Append(ToPrintVarList());

//   //return sb.ToString();

//   return ToPrintVarList("Fuel Stream: ");
//}
