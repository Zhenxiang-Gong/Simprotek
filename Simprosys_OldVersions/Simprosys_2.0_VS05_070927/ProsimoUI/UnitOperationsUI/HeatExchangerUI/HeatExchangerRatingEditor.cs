using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.UnitOperations;
using Prosimo.UnitOperations.HeatTransfer;

namespace ProsimoUI.UnitOperationsUI.HeatExchangerUI
{
	/// <summary>
	/// Summary description for HeatExchangerRatingEditor.
	/// </summary>
	public class HeatExchangerRatingEditor : System.Windows.Forms.Form
	{
      protected System.Windows.Forms.MainMenu mainMenu;
      protected System.Windows.Forms.MenuItem menuItemClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeatExchangerRatingEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public HeatExchangerRatingEditor(HeatExchangerControl heatExchangerCtrl)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
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
         // HeatExchangerRatingEditor
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(454, 355);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Menu = this.mainMenu;
         this.Name = "HeatExchangerRatingEditor";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Heat Exchanger Rating";

      }
		#endregion

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
