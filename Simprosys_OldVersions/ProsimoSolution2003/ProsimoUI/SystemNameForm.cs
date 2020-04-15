using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo;

namespace ProsimoUI
{
	/// <summary>
	/// Summary description for SystemNameForm.
	/// </summary>
	public class SystemNameForm : System.Windows.Forms.Form
	{
      private EvaporationAndDryingSystem system;

      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.Button buttonOK;
      private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SystemNameForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

      public SystemNameForm(EvaporationAndDryingSystem system)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.system = system;
         this.textBoxName.Text = system.Name;
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
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.buttonOK = new System.Windows.Forms.Button();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(12, 16);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(268, 20);
         this.textBoxName.TabIndex = 0;
         this.textBoxName.Text = "";
         // 
         // buttonOK
         // 
         this.buttonOK.Location = new System.Drawing.Point(68, 48);
         this.buttonOK.Name = "buttonOK";
         this.buttonOK.TabIndex = 1;
         this.buttonOK.Text = "OK";
         this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(152, 48);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.TabIndex = 2;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
         // 
         // SystemNameForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 84);
         this.ControlBox = false;
         this.Controls.Add(this.buttonCancel);
         this.Controls.Add(this.buttonOK);
         this.Controls.Add(this.textBoxName);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "SystemNameForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Edit System Name";
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonOK_Click(object sender, System.EventArgs e)
      {
         ErrorMessage error = this.system.SpecifyName(this.textBoxName.Text);
         if (error != null)
            UI.ShowError(error);
         this.Close();      
      }

      private void buttonCancel_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }
	}
}
