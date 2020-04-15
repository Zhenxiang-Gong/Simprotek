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
   public class FossilFuelDetailsForm : System.Windows.Forms.Form
   {
      private System.Windows.Forms.Panel _ComponentListPanel;
      private FossilFuelComponentsListViewControl _ComponentListControl;

      private Label labelName;
      private TextBox textBoxName;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public FossilFuelDetailsForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public FossilFuelDetailsForm(FossilFuel fossilFuel)
      {
         InitializeComponent();
         this.textBoxName.Text = fossilFuel.Name;
         _ComponentListControl.SetMaterialcomponents(fossilFuel.ComponentList);
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if(disposing)
         {
            if(components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._ComponentListPanel = new System.Windows.Forms.Panel();
         this._ComponentListControl = new ProsimoUI.MaterialsUI.FossilFuelComponentsListViewControl();
         this.labelName = new System.Windows.Forms.Label();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this._ComponentListPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // _ComponentListPanel
         // 
         this._ComponentListPanel.Controls.Add(this._ComponentListControl);
         this._ComponentListPanel.Location = new System.Drawing.Point(1, 42);
         this._ComponentListPanel.Name = "_ComponentListPanel";
         this._ComponentListPanel.Size = new System.Drawing.Size(303, 435);
         this._ComponentListPanel.TabIndex = 48;
         // 
         // _ComponentListControl
         // 
         this._ComponentListControl.Location = new System.Drawing.Point(0, 3);
         this._ComponentListControl.Name = "_ComponentListControl";
         this._ComponentListControl.Size = new System.Drawing.Size(300, 429);
         this._ComponentListControl.TabIndex = 0;
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
         this.textBoxName.Enabled = false;
         // 
         // FossilFuelDetailsForm
         // 
         this.ClientSize = new System.Drawing.Size(309, 409);
         this.Controls.Add(this.textBoxName);
         this.Controls.Add(this.labelName);
         this.Controls.Add(this._ComponentListPanel);
         this.MaximizeBox = false;
         this.Name = "FossilFuelDetailsForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Fuel Details";
         this._ComponentListPanel.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }
      #endregion
   }
}
