using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Prosimo.Plots;
using Prosimo.UnitOperations;

namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for PlotsForm.
	/// </summary>
	public class PlotsForm : System.Windows.Forms.Form
	{
      private EvaporationAndDryingSystem system;
      private Flowsheet flowsheet;

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private ProsimoUI.Plots.PlotsControl plotsControl;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonAdd;
      private System.Windows.Forms.Button buttonViewPlots;
      private System.Windows.Forms.GroupBox groupBoxPlotSize;
      private System.Windows.Forms.RadioButton radioButtonSmall;
      private System.Windows.Forms.RadioButton radioButtonMedium;
      private System.Windows.Forms.RadioButton radioButtonLarge;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PlotsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

      public PlotsForm(Flowsheet flowsheet, EvaporationAndDryingSystem system)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.flowsheet = flowsheet;
         this.system = system;
         this.plotsControl.InitializeTheControl(this.system.PlotCatalog); // do this first
         this.EnableDisableButtons();
         this.plotsControl.ListViewPlots.SelectedIndexChanged += new EventHandler(ListViewPlots_SelectedIndexChanged);
         this.ResizeEnd += new EventHandler(PlotsForm_ResizeEnd);
      }

      void PlotsForm_ResizeEnd(object sender, EventArgs e)
      {
         if (this.flowsheet != null)
         {
            this.flowsheet.ConnectionManager.DrawConnections();
         }
      }

      /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         this.plotsControl.ListViewPlots.SelectedIndexChanged -= new EventHandler(ListViewPlots_SelectedIndexChanged);
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
         this.panel = new System.Windows.Forms.Panel();
         this.groupBoxPlotSize = new System.Windows.Forms.GroupBox();
         this.radioButtonLarge = new System.Windows.Forms.RadioButton();
         this.radioButtonMedium = new System.Windows.Forms.RadioButton();
         this.radioButtonSmall = new System.Windows.Forms.RadioButton();
         this.buttonViewPlots = new System.Windows.Forms.Button();
         this.buttonDelete = new System.Windows.Forms.Button();
         this.buttonAdd = new System.Windows.Forms.Button();
         this.plotsControl = new ProsimoUI.Plots.PlotsControl();
         this.panel.SuspendLayout();
         this.groupBoxPlotSize.SuspendLayout();
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
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.groupBoxPlotSize);
         this.panel.Controls.Add(this.buttonViewPlots);
         this.panel.Controls.Add(this.buttonDelete);
         this.panel.Controls.Add(this.buttonAdd);
         this.panel.Controls.Add(this.plotsControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(314, 283);
         this.panel.TabIndex = 0;
         // 
         // groupBoxPlotSize
         // 
         this.groupBoxPlotSize.Controls.Add(this.radioButtonLarge);
         this.groupBoxPlotSize.Controls.Add(this.radioButtonMedium);
         this.groupBoxPlotSize.Controls.Add(this.radioButtonSmall);
         this.groupBoxPlotSize.Location = new System.Drawing.Point(8, 188);
         this.groupBoxPlotSize.Name = "groupBoxPlotSize";
         this.groupBoxPlotSize.Size = new System.Drawing.Size(148, 84);
         this.groupBoxPlotSize.TabIndex = 18;
         this.groupBoxPlotSize.TabStop = false;
         this.groupBoxPlotSize.Text = "Plot Size When Viewing";
         // 
         // radioButtonLarge
         // 
         this.radioButtonLarge.Location = new System.Drawing.Point(12, 64);
         this.radioButtonLarge.Name = "radioButtonLarge";
         this.radioButtonLarge.Size = new System.Drawing.Size(104, 16);
         this.radioButtonLarge.TabIndex = 2;
         this.radioButtonLarge.Text = "Large";
         // 
         // radioButtonMedium
         // 
         this.radioButtonMedium.Checked = true;
         this.radioButtonMedium.Location = new System.Drawing.Point(12, 44);
         this.radioButtonMedium.Name = "radioButtonMedium";
         this.radioButtonMedium.Size = new System.Drawing.Size(104, 16);
         this.radioButtonMedium.TabIndex = 1;
         this.radioButtonMedium.TabStop = true;
         this.radioButtonMedium.Text = "Medium";
         // 
         // radioButtonSmall
         // 
         this.radioButtonSmall.Location = new System.Drawing.Point(12, 24);
         this.radioButtonSmall.Name = "radioButtonSmall";
         this.radioButtonSmall.Size = new System.Drawing.Size(104, 16);
         this.radioButtonSmall.TabIndex = 0;
         this.radioButtonSmall.Text = "Small";
         // 
         // buttonViewPlots
         // 
         this.buttonViewPlots.Location = new System.Drawing.Point(228, 88);
         this.buttonViewPlots.Name = "buttonViewPlots";
         this.buttonViewPlots.TabIndex = 16;
         this.buttonViewPlots.Text = "View Plots";
         this.buttonViewPlots.Click += new System.EventHandler(this.buttonViewPlots_Click);
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(228, 56);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.TabIndex = 14;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
         // 
         // buttonAdd
         // 
         this.buttonAdd.Location = new System.Drawing.Point(228, 24);
         this.buttonAdd.Name = "buttonAdd";
         this.buttonAdd.TabIndex = 13;
         this.buttonAdd.Text = "Add";
         this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
         // 
         // plotsControl
         // 
         this.plotsControl.Location = new System.Drawing.Point(4, 4);
         this.plotsControl.Name = "plotsControl";
         this.plotsControl.Size = new System.Drawing.Size(216, 176);
         this.plotsControl.TabIndex = 0;
         // 
         // PlotsForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 13F);
         this.ClientSize = new System.Drawing.Size(314, 283);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "PlotsForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Plots";
         this.panel.ResumeLayout(false);
         this.groupBoxPlotSize.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void EnableDisableButtons()
      {
         if (this.plotsControl.ListViewPlots.SelectedItems.Count == 1)
         {
            this.buttonDelete.Enabled = true;
            if (this.plotsControl.GetSelectedPlot().IsValid)
            {
               this.buttonViewPlots.Enabled = true;
            }
            else
            {
               this.buttonViewPlots.Enabled = false;
            }
         }
         else if (this.plotsControl.ListViewPlots.SelectedItems.Count > 1)
         {
            this.buttonDelete.Enabled = true;
            if (this.plotsControl.GetValidSelectedPlots().Count > 0)
               this.buttonViewPlots.Enabled = true;
            else
               this.buttonViewPlots.Enabled = false;
         }
         else
         {
            this.buttonDelete.Enabled = false;
            this.buttonViewPlots.Enabled = false;
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonAdd_Click(object sender, System.EventArgs e)
      {
         AddPlot2DForm form = new AddPlot2DForm(this.system);
         form.ShowDialog();
      }

      private void buttonDelete_Click(object sender, System.EventArgs e)
      {
         this.plotsControl.DeleteSelectedElements();
      }

      private void buttonViewPlots_Click(object sender, System.EventArgs e)
      {
         PlotGraphSize plotSize = PlotGraphSize.Medium;
         if (this.radioButtonLarge.Checked)
            plotSize = PlotGraphSize.Large;
         else if (this.radioButtonMedium.Checked)
            plotSize = PlotGraphSize.Medium;
         else if (this.radioButtonSmall.Checked)
            plotSize = PlotGraphSize.Small;

         ViewPlot2DForm form = new ViewPlot2DForm(this.plotsControl.GetValidSelectedPlots(), plotSize);
         form.ShowDialog();
      }

      private void ListViewPlots_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.EnableDisableButtons(); 
      }
   }
}
