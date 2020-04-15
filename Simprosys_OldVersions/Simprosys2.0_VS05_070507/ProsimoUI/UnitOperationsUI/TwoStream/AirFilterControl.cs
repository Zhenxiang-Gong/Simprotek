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
using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.UnitSystems;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for AirFilterControl.
	/// </summary>
   [Serializable]
   public class AirFilterControl : FabricFilterControl
	{
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      public AirFilter AirFilter
      {
         get {return (AirFilter)this.Solvable;}
         set {this.Solvable = value;}
      }

      public AirFilterControl()
      {
      }

		public AirFilterControl(Flowsheet flowsheet, Point location, AirFilter airFilter) :
         base(flowsheet, location, airFilter)
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
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_NOT_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolvedWithWarning))
         {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVED_WITH_WARNING_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.Solved))
         {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVED_IMG;
         }
         else if (this.Solvable.SolveState.Equals(SolveState.SolveFailed))
         {
            this.BackgroundImage = UI.IMAGES.AIRFILTER_CTRL_SOLVE_FAILED_IMG;
         }
      }

      public override void Edit()
      {
         if (this.Editor == null)
         {
            this.Editor = new FabricFilterEditor(this);
            this.Editor.Text = "Air Filter: " + this.AirFilter.Name;
            (this.Editor as FabricFilterEditor).FabricFilterGroupBox.Text = "Air Filter";
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
         string nfs = this.flowsheet.ApplicationPrefs.NumericFormatString;
         StringBuilder sb = new StringBuilder();

         sb.Append("Air Filter: ");
         sb.Append(this.AirFilter.Name);
         sb.Append("\r\n");
         sb.Append(UI.UNDERLINE);
         sb.Append("\r\n");
         
         sb.Append(base.ToPrint());

         return sb.ToString();
      }

      protected AirFilterControl(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override void SetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.SetObjectData(info, context);
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionAirFilterControl", typeof(int));
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
         info.AddValue("ClassPersistenceVersionAirFilterControl", AirFilterControl.CLASS_PERSISTENCE_VERSION, typeof(int));
      }
   }
}
