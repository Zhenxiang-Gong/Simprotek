using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using ProsimoUI;

namespace ProsimoUI.ProcessStreamsUI
{
	/// <summary>
	/// Summary description for ProcessStreamEditor.
	/// </summary>
	public class ProcessStreamEditor : ProcessStreamBaseEditor
	{
      public ProcessStreamControl ProcessStreamCtrl
      {
         get {return (ProcessStreamControl)this.solvableCtrl;}
         set {this.solvableCtrl = value;}
      }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProcessStreamEditor(ProcessStreamControl processStreamCtrl) : base(processStreamCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         ProcessStreamLabelsControl labelsCtrl = new ProcessStreamLabelsControl(this.ProcessStreamCtrl.ProcessStream);
         this.panel.Controls.Add(labelsCtrl);
         labelsCtrl.Location = new Point(0, 24);

         ProcessStreamValuesControl valuesCtrl = new ProcessStreamValuesControl(this.ProcessStreamCtrl);
         this.panel.Controls.Add(valuesCtrl);
         valuesCtrl.Location = new Point(192, 24);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // ProcessStreamEditor
         // 
         this.Name = "ProcessStreamEditor";
         this.Text = "Process Stream";
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion
   }
}
