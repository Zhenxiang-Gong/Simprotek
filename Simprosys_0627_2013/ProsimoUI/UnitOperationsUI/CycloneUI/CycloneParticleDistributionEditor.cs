using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.GasSolidSeparation;
using Prosimo;
using ProsimoUI.UnitOperationsUI.CycloneUI;

namespace ProsimoUI.UnitOperationsUI.CycloneUI
{
	/// <summary>
	/// Summary description for CycloneParticleDistributionEditor.
	/// </summary>
	public class CycloneParticleDistributionEditor : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonNormalize;
      private System.Windows.Forms.Panel panelButtons;
      private ProsimoUI.UnitOperationsUI.CycloneUI.CycloneParticleDistributionControl particleDistributionControl;
      private System.Windows.Forms.Panel panelAddDeleteNormalize;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CycloneParticleDistributionEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public CycloneParticleDistributionEditor(CycloneControl cycloneCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
         
         this.particleDistributionControl.SetParticleDistribution(cycloneCtrl);
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CycloneParticleDistributionEditor));
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.panel = new System.Windows.Forms.Panel();
         this.particleDistributionControl = new ProsimoUI.UnitOperationsUI.CycloneUI.CycloneParticleDistributionControl();
         this.panelButtons = new System.Windows.Forms.Panel();
         this.panelAddDeleteNormalize = new System.Windows.Forms.Panel();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonNormalize = new System.Windows.Forms.Button();
         this.panel.SuspendLayout();
         this.panelButtons.SuspendLayout();
         this.panelAddDeleteNormalize.SuspendLayout();
         this.SuspendLayout();
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(184, 40);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(64, 23);
         this.buttonCancel.TabIndex = 46;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(112, 40);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(64, 23);
         this.buttonOk.TabIndex = 45;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.particleDistributionControl);
         this.panel.Controls.Add(this.panelButtons);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(354, 424);
         this.panel.TabIndex = 47;
         // 
         // particleDistributionControl
         // 
         this.particleDistributionControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.particleDistributionControl.Location = new System.Drawing.Point(0, 0);
         this.particleDistributionControl.Name = "particleDistributionControl";
         this.particleDistributionControl.Size = new System.Drawing.Size(350, 352);
         this.particleDistributionControl.TabIndex = 50;
         // 
         // panelButtons
         // 
         this.panelButtons.Controls.Add(this.panelAddDeleteNormalize);
         this.panelButtons.Controls.Add(this.buttonCancel);
         this.panelButtons.Controls.Add(this.buttonOk);
         this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelButtons.Location = new System.Drawing.Point(0, 352);
         this.panelButtons.Name = "panelButtons";
         this.panelButtons.Size = new System.Drawing.Size(350, 68);
         this.panelButtons.TabIndex = 49;
         // 
         // panelAddDeleteNormalize
         // 
         this.panelAddDeleteNormalize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panelAddDeleteNormalize.Controls.Add(this.buttonAdd);
         this.panelAddDeleteNormalize.Controls.Add(this.buttonDelete);
         this.panelAddDeleteNormalize.Controls.Add(this.buttonNormalize);
         this.panelAddDeleteNormalize.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelAddDeleteNormalize.Location = new System.Drawing.Point(0, 0);
         this.panelAddDeleteNormalize.Name = "panelAddDeleteNormalize";
         this.panelAddDeleteNormalize.Size = new System.Drawing.Size(350, 36);
         this.panelAddDeleteNormalize.TabIndex = 49;
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
         // buttonNormalize
         // 
         this.buttonNormalize.Location = new System.Drawing.Point(264, 4);
         this.buttonNormalize.Name = "buttonNormalize";
         this.buttonNormalize.Size = new System.Drawing.Size(64, 23);
         this.buttonNormalize.TabIndex = 48;
         this.buttonNormalize.Text = "Normalize";
         this.buttonNormalize.Click += new System.EventHandler(this.buttonNormalize_Click);
         // 
         // CycloneParticleDistributionEditor
         // 
         this.ClientSize = new System.Drawing.Size(354, 424);
         this.Controls.Add(this.panel);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.Name = "CycloneParticleDistributionEditor";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Particle Distribution";
         this.panel.ResumeLayout(false);
         this.panelButtons.ResumeLayout(false);
         this.panelAddDeleteNormalize.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         this.particleDistributionControl.ParticleDistributionCache.AddParticleSizeFractionAndEfficiency();
         // the ui is updated when the event is received
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.particleDistributionControl.DeleteSelectedElements();       
      }

      private void buttonNormalize_Click(object sender, System.EventArgs e)
      {
         this.particleDistributionControl.ParticleDistributionCache.Normalize();
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.particleDistributionControl.ParticleDistributionCache.FinishSpecifications();
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
