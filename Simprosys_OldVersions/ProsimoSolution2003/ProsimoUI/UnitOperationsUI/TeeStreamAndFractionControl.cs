using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI;
using Prosimo.UnitOperations.Miscellaneous;

namespace ProsimoUI.UnitOperationsUI
{
	/// <summary>
	/// Summary description for TeeStreamAndFractionControl.
	/// </summary>
	public class TeeStreamAndFractionControl : System.Windows.Forms.UserControl
	{
      private StreamAndFraction streamAndFraction;
      
      private System.Windows.Forms.Label labelStream;
      public ProcessVarTextBox textBoxFraction;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TeeStreamAndFractionControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

      public TeeStreamAndFractionControl(Flowsheet flowsheet, StreamAndFraction streamAndFraction)
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();

         this.streamAndFraction = streamAndFraction;
         this.labelStream.Text = streamAndFraction.Stream.Name;
         this.textBoxFraction.InitializeVariable(flowsheet, streamAndFraction.Fraction);
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
         this.labelStream = new System.Windows.Forms.Label();
         this.textBoxFraction = new ProcessVarTextBox();
         this.SuspendLayout();
         // 
         // labelStream
         // 
         this.labelStream.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.labelStream.Location = new System.Drawing.Point(0, 0);
         this.labelStream.Name = "labelStream";
         this.labelStream.Size = new System.Drawing.Size(144, 20);
         this.labelStream.TabIndex = 0;
         this.labelStream.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // textBoxFraction
         // 
         this.textBoxFraction.Location = new System.Drawing.Point(144, 0);
         this.textBoxFraction.Name = "textBoxFraction";
         this.textBoxFraction.Size = new System.Drawing.Size(80, 20);
         this.textBoxFraction.TabIndex = 1;
         this.textBoxFraction.Text = "";
         this.textBoxFraction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // TeeStreamAndFractionControl
         // 
         this.Controls.Add(this.textBoxFraction);
         this.Controls.Add(this.labelStream);
         this.Name = "TeeStreamAndFractionControl";
         this.Size = new System.Drawing.Size(224, 20);
         this.ResumeLayout(false);

      }
		#endregion
	}
}
