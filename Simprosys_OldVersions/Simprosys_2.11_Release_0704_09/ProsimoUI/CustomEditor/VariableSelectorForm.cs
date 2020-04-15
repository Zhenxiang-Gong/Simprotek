using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace ProsimoUI.CustomEditor
{
	/// <summary>
	/// Summary description for VariableSelectorForm.
	/// </summary>
	public class VariableSelectorForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private ProsimoUI.CustomEditor.VariableSelectorControl variableSelectorControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public VariableSelectorForm(CustomEditor editor)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.variableSelectorControl.Editor = editor;
         this.variableSelectorControl.InitializeSolvableLists();
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
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.variableSelectorControl = new ProsimoUI.CustomEditor.VariableSelectorControl();
         this.SuspendLayout();
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose});
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // variableSelectorControl
         // 
         this.variableSelectorControl.Editor = null;
         this.variableSelectorControl.Location = new System.Drawing.Point(0, 0);
         this.variableSelectorControl.Name = "variableSelectorControl";
         this.variableSelectorControl.Size = new System.Drawing.Size(388, 308);
         this.variableSelectorControl.TabIndex = 0;
         // 
         // VariableSelectorForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(388, 305);
         this.Controls.Add(this.variableSelectorControl);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "VariableSelectorForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Select Process Variables";
         this.ResumeLayout(false);

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
