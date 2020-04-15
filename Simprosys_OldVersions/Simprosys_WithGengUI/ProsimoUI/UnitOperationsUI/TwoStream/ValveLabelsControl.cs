using System;
using System.Collections;
using System.ComponentModel;
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
	/// Summary description for ValveLabelsControl.
	/// </summary>
	public class ValveLabelsControl : System.Windows.Forms.UserControl
	{
      
      // these need to be in sync with the dimensions of this control
      public const int WIDTH = 192;
      public const int HEIGHT = 20;

      private ProcessVarLabel labelPressureDrop;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public ValveLabelsControl()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
      }

		public ValveLabelsControl(Valve uo) : this()
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
         this.labelPressureDrop = new ProsimoUI.ProcessVarLabel();
         this.SuspendLayout();
         // 
         // labelPressureDrop
         // 
         this.labelPressureDrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelPressureDrop.Location = new System.Drawing.Point(0, 0);
         this.labelPressureDrop.Name = "labelPressureDrop";
         this.labelPressureDrop.Size = new System.Drawing.Size(192, 20);
         this.labelPressureDrop.TabIndex = 92;
         this.labelPressureDrop.Text = "PressureDrop";
         this.labelPressureDrop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // ValveLabelsControl
         // 
         this.Controls.Add(this.labelPressureDrop);
         this.Name = "ValveLabelsControl";
         this.Size = new System.Drawing.Size(192, 20);
         this.ResumeLayout(false);

      }
		#endregion

      public void InitializeVariableLabels(Valve uo)
      {
         this.labelPressureDrop.InitializeVariable(uo.PressureDrop);
      }
   }
}
