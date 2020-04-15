using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ProsimoUI.Plots;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for StateControl.
	/// </summary>
	public class StateControl : System.Windows.Forms.UserControl
	{
      // this value is half of the side of this control
      public static int RADIUS = 5;

      private int upThreshold;
      private int downThreshold;
      private int leftThreshold;
      private int rightThreshold;

      private int X = 0;
      private int Y = 0;
      /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StateControl(HCPlotGraph plotGraph)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         this.SetThresholds(plotGraph);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StateControl));
         // 
         // StateControl
         // 
         this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
         this.Name = "StateControl";
         this.Size = new System.Drawing.Size(11, 11);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StateControl_MouseMove);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StateControl_MouseDown);

      }
		#endregion

      private void ChangeLocation(Point p)
      {
         if (X != -1 || Y != -1)
         {
            int left = this.Left + p.X - X;
            int top = this.Top + p.Y - Y;
            if (left >= this.leftThreshold && left <= this.rightThreshold &&
                top >= this.upThreshold && top <= this.downThreshold)
            {
               this.Left = left;
               this.Top = top;
            }
         }
      }

      private void StateControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         this.X = e.X;
         this.Y = e.Y;
      }

      private void StateControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
      {
         Point p = new Point(e.X, e.Y);
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
            this.ChangeLocation(p);
         }
      }

      private void SetThresholds(HCPlotGraph plotGraph)
      {
         this.leftThreshold = System.Convert.ToInt32(plotGraph.Origin.X) - StateControl.RADIUS;
         this.downThreshold = System.Convert.ToInt32(plotGraph.Origin.Y) - StateControl.RADIUS;
         this.upThreshold = System.Convert.ToInt32(plotGraph.Origin.Y - plotGraph.OyLength) - StateControl.RADIUS;
         this.rightThreshold = System.Convert.ToInt32(plotGraph.Origin.X + plotGraph.OxLength) - StateControl.RADIUS;
      }
	}
}
