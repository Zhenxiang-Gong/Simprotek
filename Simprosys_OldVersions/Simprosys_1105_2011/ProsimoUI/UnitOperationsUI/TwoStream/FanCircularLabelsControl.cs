using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitSystems;
using ProsimoUI;
using Prosimo.UnitOperations;
using Prosimo.UnitOperations.FluidTransport;

namespace ProsimoUI.UnitOperationsUI.TwoStream
{
	/// <summary>
	/// Summary description for FanCircularLabelsControl.
	/// </summary>
	public class FanCircularLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 20;
      private ProsimoUI.ProcessVarLabel labelOutletDiameter;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FanCircularLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public FanCircularLabelsControl(Fan uo) : this()
		{
         this.InitializeVariableLabels(uo);
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
         this.labelOutletDiameter = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelOutletDiameter
         // 
         this.labelOutletDiameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelOutletDiameter.Location = new System.Drawing.Point(0, 0);
         this.labelOutletDiameter.Name = "labelOutletDiameter";
         this.labelOutletDiameter.Size = new System.Drawing.Size(192, 20);
         this.labelOutletDiameter.TabIndex = 92;
         this.labelOutletDiameter.Text = "OutletDiameter";
         this.labelOutletDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // FanCircularLabelsControl
         // 
         this.Controls.Add(this.labelOutletDiameter);
         this.Name = "FanCircularLabelsControl";
         this.Size = new System.Drawing.Size(192, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Fan uo)
      {
         this.labelOutletDiameter.InitializeVariable(uo.OutletDiameter);
      }
   }
}
