using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo;
using Prosimo.Materials;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for DuhringLinesForm.
	/// </summary>
	public class DuhringLinesForm : System.Windows.Forms.Form
	{
      private DryingMaterialCache dryingMaterialCache;

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Panel panelDuhringLinesAndButtons;
      private System.Windows.Forms.Panel panelButtons;
      private System.Windows.Forms.Panel panelAddDelete;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private ProsimoUI.MaterialsUI.DuhringLinesControl duhringLinesControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DuhringLinesForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public DuhringLinesForm(DryingMaterialCache dryingMaterialCache, INumericFormat iNumericFormat)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.dryingMaterialCache = dryingMaterialCache;
         this.duhringLinesControl.SetDuhringLinesCache(dryingMaterialCache, iNumericFormat);
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
         this.panel = new System.Windows.Forms.Panel();
         this.panelDuhringLinesAndButtons = new System.Windows.Forms.Panel();
         this.duhringLinesControl = new ProsimoUI.MaterialsUI.DuhringLinesControl();
         this.panelButtons = new System.Windows.Forms.Panel();
         this.panelAddDelete = new System.Windows.Forms.Panel();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panel.SuspendLayout();
         this.panelDuhringLinesAndButtons.SuspendLayout();
         this.panelButtons.SuspendLayout();
         this.panelAddDelete.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.panelDuhringLinesAndButtons);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(536, 352);
         this.panel.TabIndex = 47;
         // 
         // panelDuhringLinesAndButtons
         // 
         this.panelDuhringLinesAndButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelDuhringLinesAndButtons.Controls.Add(this.duhringLinesControl);
         this.panelDuhringLinesAndButtons.Controls.Add(this.panelButtons);
         this.panelDuhringLinesAndButtons.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelDuhringLinesAndButtons.Location = new System.Drawing.Point(0, 0);
         this.panelDuhringLinesAndButtons.Name = "panelDuhringLinesAndButtons";
         this.panelDuhringLinesAndButtons.Size = new System.Drawing.Size(532, 348);
         this.panelDuhringLinesAndButtons.TabIndex = 53;
         // 
         // duhringLinesControl
         // 
         this.duhringLinesControl.Location = new System.Drawing.Point(0, 0);
         this.duhringLinesControl.Name = "duhringLinesControl";
         this.duhringLinesControl.Size = new System.Drawing.Size(528, 272);
         this.duhringLinesControl.TabIndex = 51;
         // 
         // panelButtons
         // 
         this.panelButtons.Controls.Add(this.panelAddDelete);
         this.panelButtons.Controls.Add(this.buttonCancel);
         this.panelButtons.Controls.Add(this.buttonOk);
         this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelButtons.Location = new System.Drawing.Point(0, 276);
         this.panelButtons.Name = "panelButtons";
         this.panelButtons.Size = new System.Drawing.Size(528, 68);
         this.panelButtons.TabIndex = 50;
         // 
         // panelAddDelete
         // 
         this.panelAddDelete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelAddDelete.Controls.Add(this.buttonAdd);
         this.panelAddDelete.Controls.Add(this.buttonDelete);
         this.panelAddDelete.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelAddDelete.Location = new System.Drawing.Point(0, 0);
         this.panelAddDelete.Name = "panelAddDelete";
         this.panelAddDelete.Size = new System.Drawing.Size(528, 36);
         this.panelAddDelete.TabIndex = 49;
         // 
         // buttonAdd
         // 
         this.buttonAdd.Location = new System.Drawing.Point(20, 4);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.Size = new System.Drawing.Size(64, 23);
         this.buttonAdd.TabIndex = 46;
         this.buttonAdd.Text = "Add";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(100, 4);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.Size = new System.Drawing.Size(64, 23);
         this.buttonDelete.TabIndex = 47;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(268, 40);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(64, 23);
         this.buttonCancel.TabIndex = 46;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(196, 40);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(64, 23);
         this.buttonOk.TabIndex = 45;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // DuhringLinesForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(536, 352);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DuhringLinesForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Duhring Lines";
         this.panel.ResumeLayout(false);
         this.panelDuhringLinesAndButtons.ResumeLayout(false);
         this.panelButtons.ResumeLayout(false);
         this.panelAddDelete.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         this.duhringLinesControl.DuhringLinesCache.AddDuhringLine();
         // the ui is updated when the event is received
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.duhringLinesControl.DeleteSelectedElements();       
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.duhringLinesControl.DuhringLinesCache.FinishSpecifications();
         if (error != null)
            UI.ShowError(error);
         else
            this.Close();
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
   }
}
