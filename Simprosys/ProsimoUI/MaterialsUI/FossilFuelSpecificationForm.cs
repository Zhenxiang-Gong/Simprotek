using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Prosimo;
using Prosimo.Materials;
using Prosimo.SubstanceLibrary;

namespace ProsimoUI.MaterialsUI
{
	/// <summary>
	/// Summary description for CycloneParticleDistributionEditor.
	/// </summary>
	public class FossilFuelSpecificationForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Panel _SubstanceListPanel;
      private System.Windows.Forms.Panel _ComponentsSpecificationPanel;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonNormalize;
      private ComponentsControl _ComponentsControl;
      private SubstanceListControl _SubstanceListControl;

      private INumericFormat numericFormat;
      private Label labelName;
      private TextBox textBoxName;

      private bool isNew;
      private Panel panelButtons;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public FossilFuelSpecificationForm()
		{
         //
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public FossilFuelSpecificationForm(FossilFuelCache fossilFuelCache, INumericFormat format)
      {
         numericFormat = format; 
         InitializeComponent();

         if(fossilFuelCache != null)
         {
            isNew = false;
            this._ComponentsControl.SetMaterialComponents(fossilFuelCache, numericFormat);
            this.textBoxName.Text = fossilFuelCache.Name;
         }
         else
         {
            isNew = true;
            this._ComponentsControl.SetNumericFormat(numericFormat);
            this.textBoxName.Text = _ComponentsControl.MaterialCache.Name;
         }
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
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonOk = new System.Windows.Forms.Button();
         this.buttonNormalize = new System.Windows.Forms.Button();
         this._SubstanceListPanel = new System.Windows.Forms.Panel();
         this._SubstanceListControl = new ProsimoUI.MaterialsUI.SubstanceListControl();
         this._ComponentsSpecificationPanel = new System.Windows.Forms.Panel();
         this._ComponentsControl = new ProsimoUI.MaterialsUI.ComponentsControl();
         this.panelButtons = new System.Windows.Forms.Panel();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.labelName = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this._SubstanceListPanel.SuspendLayout();
         this._ComponentsSpecificationPanel.SuspendLayout();
         this.panelButtons.SuspendLayout();
         this.SuspendLayout();
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(578, 442);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(64, 23);
         this.buttonCancel.TabIndex = 46;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // buttonOk
         // 
         this.buttonOk.Location = new System.Drawing.Point(419, 442);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(64, 23);
         this.buttonOk.TabIndex = 45;
         this.buttonOk.Text = "OK";
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // buttonNormalize
         // 
         this.buttonNormalize.Location = new System.Drawing.Point(176, 6);
         this.buttonNormalize.Name = "buttonNormalize";
         this.buttonNormalize.Size = new System.Drawing.Size(64, 23);
         this.buttonNormalize.TabIndex = 48;
         this.buttonNormalize.Text = "Normalize";
         this.buttonNormalize.Click += new System.EventHandler(this.buttonNormalize_Click);
         // 
         // _SubstanceListPanel
         // 
         this._SubstanceListPanel.Controls.Add(this._SubstanceListControl);
         this._SubstanceListPanel.Location = new System.Drawing.Point(1, 42);
         this._SubstanceListPanel.Name = "_SubstanceListPanel";
         this._SubstanceListPanel.Size = new System.Drawing.Size(303, 435);
         this._SubstanceListPanel.TabIndex = 48;
         // 
         // _SubstanceListControl
         // 
         this._SubstanceListControl.Location = new System.Drawing.Point(0, 3);
         this._SubstanceListControl.Name = "_SubstanceListControl";
         this._SubstanceListControl.Size = new System.Drawing.Size(300, 429);
         this._SubstanceListControl.TabIndex = 0;
         // 
         // _ComponentsSpecificationPanel
         // 
         this._ComponentsSpecificationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this._ComponentsSpecificationPanel.Controls.Add(this._ComponentsControl);
         this._ComponentsSpecificationPanel.Controls.Add(this.panelButtons);
         this._ComponentsSpecificationPanel.Location = new System.Drawing.Point(400, 42);
         this._ComponentsSpecificationPanel.Name = "_ComponentsSpecificationPanel";
         this._ComponentsSpecificationPanel.Size = new System.Drawing.Size(282, 394);
         this._ComponentsSpecificationPanel.TabIndex = 47;
         // 
         // _ComponentsControl
         // 
         this._ComponentsControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this._ComponentsControl.Location = new System.Drawing.Point(0, 0);
         this._ComponentsControl.Name = "_ComponentsControl";
         this._ComponentsControl.Size = new System.Drawing.Size(278, 351);
         this._ComponentsControl.TabIndex = 50;
         // 
         // panelButtons
         // 
         this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panelButtons.Controls.Add(this.buttonNormalize);
         this.panelButtons.Controls.Add(this.buttonDelete);
         this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panelButtons.Location = new System.Drawing.Point(0, 351);
         this.panelButtons.Margin = new System.Windows.Forms.Padding(1);
         this.panelButtons.Name = "panelButtons";
         this.panelButtons.Size = new System.Drawing.Size(278, 39);
         this.panelButtons.TabIndex = 49;
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(17, 6);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.Size = new System.Drawing.Size(64, 23);
         this.buttonDelete.TabIndex = 47;
         this.buttonDelete.Text = "Remove";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // buttonAdd
         // 
         this.buttonAdd.Location = new System.Drawing.Point(319, 83);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.Size = new System.Drawing.Size(64, 23);
         this.buttonAdd.TabIndex = 46;
         this.buttonAdd.Text = "Add >>";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // labelName
         // 
         this.labelName.AutoSize = true;
         this.labelName.Location = new System.Drawing.Point(13, 13);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(38, 13);
         this.labelName.TabIndex = 49;
         this.labelName.Text = "Name:";
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(57, 10);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(176, 20);
         this.textBoxName.TabIndex = 50;
         // 
         // FossilFuelSpecificationForm
         // 
         this.ClientSize = new System.Drawing.Size(683, 477);
         this.Controls.Add(this.textBoxName);
         this.Controls.Add(this.labelName);
         this.Controls.Add(this.buttonCancel);
         this.Controls.Add(this.buttonAdd);
         this.Controls.Add(this.buttonOk);
         this.Controls.Add(this._SubstanceListPanel);
         this.Controls.Add(this._ComponentsSpecificationPanel);
         this.MaximizeBox = false;
         this.Name = "FossilFuelSpecificationForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit Drying Fuel";
         this._SubstanceListPanel.ResumeLayout(false);
         this._ComponentsSpecificationPanel.ResumeLayout(false);
         this.panelButtons.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }
		#endregion

      internal void PopulateSubstanceList()
      {
         _SubstanceListControl.PopulateSubstanceList();
      }

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         ArrayList selectedSubstances = this._SubstanceListControl.GetSelectedSubstances();
         this._ComponentsControl.MaterialCache.AddMaterialComponents(selectedSubstances);
         // the ui is updated when the event is received
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this._ComponentsControl.DeleteSelectedElements();       
      }

      private void buttonNormalize_Click(object sender, System.EventArgs e)
      {
         this._ComponentsControl.MaterialCache.Normalize();
      }

      private void buttonOk_Click(object sender, System.EventArgs e)
      {
         ErrorMessage error = this._ComponentsControl.MaterialCache.FinishSpecifications(this.textBoxName.Text, this.isNew);
         if(error != null)
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
