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
	/// Summary description for SolvableEditor.
	/// </summary>
	public class SolvableEditor : System.Windows.Forms.Form
	{
      protected SolvableControl solvableCtrl;
      protected System.Windows.Forms.TextBox textBoxName;
      protected System.Windows.Forms.Label labelName;
      protected System.Windows.Forms.StatusBar statusBar;
      protected System.Windows.Forms.StatusBarPanel statusBarPanel;
      protected System.Windows.Forms.MainMenu mainMenu;
      protected System.Windows.Forms.MenuItem menuItemClose;
      protected System.Windows.Forms.Panel panel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      public SolvableEditor()
      {
      }

      public SolvableEditor(SolvableControl solvableCtrl)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.solvableCtrl = solvableCtrl;

         this.labelName.BackColor = Color.DarkGray;
         if (solvableCtrl.Solvable != null)
            this.textBoxName.Text = solvableCtrl.Solvable.Name;
         else
            this.textBoxName.Text = "";

         this.Solvable_SolveComplete(this.solvableCtrl.Solvable, this.solvableCtrl.Solvable.SolveState);

         this.solvableCtrl.Solvable.SolveComplete += new SolveCompleteEventHandler(Solvable_SolveComplete);
         this.solvableCtrl.Solvable.NameChanged += new NameChangedEventHandler(Solvable_NameChanged);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         if (this.solvableCtrl != null && this.solvableCtrl.Solvable != null)
         {
            this.solvableCtrl.Solvable.SolveComplete -= new SolveCompleteEventHandler(Solvable_SolveComplete);
            this.solvableCtrl.Solvable.NameChanged -= new NameChangedEventHandler(Solvable_NameChanged);
         }
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
         this.labelName = new System.Windows.Forms.Label();
         this.statusBar = new System.Windows.Forms.StatusBar();
         this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(40, 0);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(232, 20);
         this.textBoxName.TabIndex = 14;
         this.textBoxName.Text = "";
         this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.ValidatingHandler);
         this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUpHandler);
         // 
         // labelName
         // 
         this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.labelName.Location = new System.Drawing.Point(0, 0);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(40, 20);
         this.labelName.TabIndex = 81;
         this.labelName.Text = "Name:";
         this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // statusBar
         // 
         this.statusBar.Location = new System.Drawing.Point(0, 265);
         this.statusBar.Name = "statusBar";
         this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                     this.statusBarPanel});
         this.statusBar.ShowPanels = true;
         this.statusBar.Size = new System.Drawing.Size(274, 22);
         this.statusBar.SizingGrip = false;
         this.statusBar.TabIndex = 82;
         // 
         // statusBarPanel
         // 
         this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
         this.statusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised;
         this.statusBarPanel.Width = 274;
         // 
         // mainMenu
         // 
         this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuItemClose
                                                                                 });
         // 
         // menuItemClose
         // 
         this.menuItemClose.Index = 0;
         this.menuItemClose.Text = "Close";
         this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.labelName);
         this.panel.Controls.Add(this.textBoxName);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(274, 265);
         this.panel.TabIndex = 83;
         // 
         // SolvableEditor
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(274, 287);
         this.Controls.Add(this.panel);
         this.Controls.Add(this.statusBar);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "SolvableEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Solvable";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingHandler);
         ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void ClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // assume only one dependent
         this.solvableCtrl.Editor = null;
      }

      protected virtual void ValidatingHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         Solvable solvable = this.solvableCtrl.Solvable;
         TextBox tb = (TextBox)sender;
         if (tb.Text.Trim().Equals(""))
         {
            e.Cancel = true;
            string message3 = "Please specify a name!";
            MessageBox.Show(message3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         else
         {
            ErrorMessage error = solvable.SpecifyName(this.textBoxName.Text);
            if (error != null)
               UI.ShowError(error);
         }
      }

      protected virtual void KeyUpHandler(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = this.statusBar;
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();      
      }

      private void Solvable_SolveComplete(object sender, SolveState solveState)
      {
         UI.SetStatusTextAndIcon(this.statusBarPanel, solveState);
      }

      private void Solvable_NameChanged(object sender, string name, string oldName)
      {
         this.textBoxName.Text = name;
      }

   }
}
