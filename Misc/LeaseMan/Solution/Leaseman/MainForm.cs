using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Prosimo.SoftwareProtection;

namespace Leaseman
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.GroupBox groupBoxLeaseDuration;
      private System.Windows.Forms.RadioButton radioButtonYears;
      private System.Windows.Forms.RadioButton radioButtonMonths;
      private System.Windows.Forms.RadioButton radioButtonDays;
      private System.Windows.Forms.TextBox textBoxLeaseDuration;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Button buttonGenerateDongle;
      private System.Windows.Forms.TextBox textBoxLease;
      private System.Windows.Forms.Label labelComputerName;
      private System.Windows.Forms.TextBox textBoxComputerName;
      private IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         //this.radioButtonYears.Checked = true;
         this.textBoxLeaseDuration.Text = "1";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
         this.components = new System.ComponentModel.Container();
         this.panel = new System.Windows.Forms.Panel();
         this.textBoxComputerName = new System.Windows.Forms.TextBox();
         this.labelComputerName = new System.Windows.Forms.Label();
         this.textBoxLease = new System.Windows.Forms.TextBox();
         this.buttonGenerateDongle = new System.Windows.Forms.Button();
         this.groupBoxLeaseDuration = new System.Windows.Forms.GroupBox();
         this.textBoxLeaseDuration = new System.Windows.Forms.TextBox();
         this.radioButtonDays = new System.Windows.Forms.RadioButton();
         this.radioButtonMonths = new System.Windows.Forms.RadioButton();
         this.radioButtonYears = new System.Windows.Forms.RadioButton();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel.SuspendLayout();
         this.groupBoxLeaseDuration.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.textBoxComputerName);
         this.panel.Controls.Add(this.labelComputerName);
         this.panel.Controls.Add(this.textBoxLease);
         this.panel.Controls.Add(this.buttonGenerateDongle);
         this.panel.Controls.Add(this.groupBoxLeaseDuration);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(444, 219);
         this.panel.TabIndex = 1;
         // 
         // textBoxComputerName
         // 
         this.textBoxComputerName.Location = new System.Drawing.Point(104, 132);
         this.textBoxComputerName.Name = "textBoxComputerName";
         this.textBoxComputerName.Size = new System.Drawing.Size(180, 20);
         this.textBoxComputerName.TabIndex = 4;
         // 
         // labelComputerName
         // 
         this.labelComputerName.Location = new System.Drawing.Point(12, 132);
         this.labelComputerName.Name = "labelComputerName";
         this.labelComputerName.Size = new System.Drawing.Size(92, 20);
         this.labelComputerName.TabIndex = 3;
         this.labelComputerName.Text = "Computer Name:";
         this.labelComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxLease
         // 
         this.textBoxLease.Location = new System.Drawing.Point(130, 16);
         this.textBoxLease.Multiline = true;
         this.textBoxLease.Name = "textBoxLease";
         this.textBoxLease.ReadOnly = true;
         this.textBoxLease.Size = new System.Drawing.Size(302, 108);
         this.textBoxLease.TabIndex = 2;
         // 
         // buttonGenerateDongle
         // 
         this.buttonGenerateDongle.Location = new System.Drawing.Point(168, 184);
         this.buttonGenerateDongle.Name = "buttonGenerateDongle";
         this.buttonGenerateDongle.Size = new System.Drawing.Size(104, 23);
         this.buttonGenerateDongle.TabIndex = 1;
         this.buttonGenerateDongle.Text = "Generate Dongle";
         this.buttonGenerateDongle.Click += new System.EventHandler(this.buttonGenerateDongle_Click);
         // 
         // groupBoxLeaseDuration
         // 
         this.groupBoxLeaseDuration.Controls.Add(this.textBoxLeaseDuration);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonDays);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonMonths);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonYears);
         this.groupBoxLeaseDuration.Location = new System.Drawing.Point(12, 12);
         this.groupBoxLeaseDuration.Name = "groupBoxLeaseDuration";
         this.groupBoxLeaseDuration.Size = new System.Drawing.Size(112, 112);
         this.groupBoxLeaseDuration.TabIndex = 0;
         this.groupBoxLeaseDuration.TabStop = false;
         this.groupBoxLeaseDuration.Text = "License Duration";
         // 
         // textBoxLeaseDuration
         // 
         this.textBoxLeaseDuration.Location = new System.Drawing.Point(8, 84);
         this.textBoxLeaseDuration.Name = "textBoxLeaseDuration";
         this.textBoxLeaseDuration.Size = new System.Drawing.Size(76, 20);
         this.textBoxLeaseDuration.TabIndex = 3;
         this.textBoxLeaseDuration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxLeaseDuration_KeyUp);
         this.textBoxLeaseDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxLeaseDuration_Validating);
         // 
         // radioButtonDays
         // 
         this.radioButtonDays.Checked = false;
         this.radioButtonDays.Location = new System.Drawing.Point(8, 60);
         this.radioButtonDays.Name = "radioButtonDays";
         this.radioButtonDays.Size = new System.Drawing.Size(76, 20);
         this.radioButtonDays.TabIndex = 2;
         this.radioButtonDays.Text = "Days";
         this.radioButtonDays.CheckedChanged += new System.EventHandler(this.radioButtonDays_CheckedChanged);
         // 
         // radioButtonMonths
         // 
         this.radioButtonMonths.Checked = true;
         this.radioButtonMonths.Location = new System.Drawing.Point(8, 40);
         this.radioButtonMonths.Name = "radioButtonMonths";
         this.radioButtonMonths.Size = new System.Drawing.Size(76, 20);
         this.radioButtonMonths.TabIndex = 1;
         this.radioButtonMonths.TabStop = true;
         this.radioButtonMonths.Text = "Months";
         this.radioButtonMonths.CheckedChanged += new System.EventHandler(this.radioButtonMonths_CheckedChanged);
         // 
         // radioButtonYears
         // 
         this.radioButtonYears.Checked = false;
         this.radioButtonYears.Location = new System.Drawing.Point(8, 20);
         this.radioButtonYears.Name = "radioButtonYears";
         this.radioButtonYears.Size = new System.Drawing.Size(76, 20);
         this.radioButtonYears.TabIndex = 0;
         this.radioButtonYears.Text = "Years";
         this.radioButtonYears.CheckedChanged += new System.EventHandler(this.radioButtonYears_CheckedChanged);
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
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(444, 219);
         this.Controls.Add(this.panel);
         this.Menu = this.mainMenu;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "License Manager";
         this.panel.ResumeLayout(false);
         this.panel.PerformLayout();
         this.groupBoxLeaseDuration.ResumeLayout(false);
         this.groupBoxLeaseDuration.PerformLayout();
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

      private void radioButtonYears_CheckedChanged(object sender, System.EventArgs e)
      {
         this.textBoxLease.Text = "";
      }

      private void radioButtonMonths_CheckedChanged(object sender, System.EventArgs e)
      {
         this.textBoxLease.Text = "";
      }

      private void radioButtonDays_CheckedChanged(object sender, System.EventArgs e)
      {
         this.textBoxLease.Text = "";
      }

      private void textBoxLeaseDuration_Validating(object sender, System.ComponentModel.CancelEventArgs e)
      {
         try
         {
            TextBox tb = (TextBox)sender;
            int val = Int32.Parse(tb.Text);
            if (val < 0)
            {
               e.Cancel = true;
               string message = "Please enter a positive value!"; 
               MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               this.textBoxLease.Text = "";
            }
         }
         catch (Exception)
         {
            e.Cancel = true;
            string message = "Please enter a numeric value!"; 
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
      }

      private void textBoxLeaseDuration_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            this.ActiveControl = null;            
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }

      private void buttonGenerateDongle_Click(object sender, System.EventArgs e)
      {
         if (!this.textBoxComputerName.Text.Trim().Equals(""))
         {
            DateTime startDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            if (this.radioButtonYears.Checked)
            {
               int years = System.Convert.ToInt32(this.textBoxLeaseDuration.Text);
               endDateTime = endDateTime.AddYears(years);
            }
            else if (this.radioButtonMonths.Checked)
            {
               int months = System.Convert.ToInt32(this.textBoxLeaseDuration.Text);
               endDateTime = endDateTime.AddMonths(months);
            }
            else if (this.radioButtonDays.Checked)
            {
               int days = System.Convert.ToInt32(this.textBoxLeaseDuration.Text);
               endDateTime = endDateTime.AddDays(days);
            }

            Lease lease = new Lease(startDateTime, endDateTime, this.textBoxComputerName.Text);
            DongleGenerator dg = new DongleGenerator();
            if (dg.GenerateDongle(lease))
            {
               StringBuilder sb = new StringBuilder();

               sb.Append("Lease Start: ");
               sb.Append(lease.LeaseStart.ToString());
               sb.Append(" (");
               sb.Append(lease.GetStringFromDateTime(lease.LeaseStart));
               sb.Append(")");
               sb.Append("\r\n");

               sb.Append("Lease End: ");
               sb.Append(lease.LeaseEnd.ToString());
               sb.Append(" (");
               sb.Append(lease.GetStringFromDateTime(lease.LeaseEnd));
               sb.Append(")");
               sb.Append("\r\n");

               sb.Append("Serial #: ");
               sb.Append(lease.GetStringFromDateTime(lease.LeaseStart));
               sb.Append("\r\n");

               sb.Append("Computer Name: ");
               sb.Append(lease.ComputerName);

               this.textBoxLease.Text = sb.ToString();
            }
            else
            {
               MessageBox.Show("The file already exists!");
            }
         }
         else
         {
            MessageBox.Show("Please specify a computer name!");
         }
      }
	}
}
