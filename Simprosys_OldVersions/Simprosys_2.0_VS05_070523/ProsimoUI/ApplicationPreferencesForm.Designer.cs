using ProsimoUI.UnitSystemsUI;

namespace ProsimoUI
{
   partial class ApplicationPreferencesForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (this.unitSystemsCtrl != null)
            this.unitSystemsCtrl.SelectedUnitSystemChanged -= new SelectedUnitSystemChangedEventHandler(unitSystemsCtrl_SelectedUnitSystemChanged);
         if (disposing && (components != null))
         {
            components.Dispose();
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
         this.components = new System.ComponentModel.Container();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.tabControlPrefs = new System.Windows.Forms.TabControl();
         this.tabPageUnits = new System.Windows.Forms.TabPage();
         this.groupBoxCurrentUnitSystem = new System.Windows.Forms.GroupBox();
         this.buttonCurrentUnitSys = new System.Windows.Forms.Button();
         this.labelCurrent = new System.Windows.Forms.Label();
         this.tabPageNumberFormat = new System.Windows.Forms.TabPage();
         this.radioButtonScientific = new System.Windows.Forms.RadioButton();
         this.radioButtonFixedPoint = new System.Windows.Forms.RadioButton();
         this.domainUpDownDecPlaces = new System.Windows.Forms.DomainUpDown();
         this.labelDecimalPlaces = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.panel.SuspendLayout();
         this.tabControlPrefs.SuspendLayout();
         this.tabPageUnits.SuspendLayout();
         this.groupBoxCurrentUnitSystem.SuspendLayout();
         this.tabPageNumberFormat.SuspendLayout();
         this.groupBox1.SuspendLayout();
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
         this.panel.Controls.Add(this.tabControlPrefs);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(416, 417);
         this.panel.TabIndex = 6;
         // 
         // tabControlPrefs
         // 
         this.tabControlPrefs.Controls.Add(this.tabPageUnits);
         this.tabControlPrefs.Controls.Add(this.tabPageNumberFormat);
         this.tabControlPrefs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControlPrefs.Location = new System.Drawing.Point(0, 0);
         this.tabControlPrefs.Name = "tabControlPrefs";
         this.tabControlPrefs.SelectedIndex = 0;
         this.tabControlPrefs.Size = new System.Drawing.Size(412, 413);
         this.tabControlPrefs.TabIndex = 4;
         // 
         // tabPageUnits
         // 
         this.tabPageUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageUnits.Controls.Add(this.groupBoxCurrentUnitSystem);
         this.tabPageUnits.Location = new System.Drawing.Point(4, 22);
         this.tabPageUnits.Name = "tabPageUnits";
         this.tabPageUnits.Size = new System.Drawing.Size(404, 387);
         this.tabPageUnits.TabIndex = 1;
         this.tabPageUnits.Text = "Unit Systems";
         // 
         // groupBoxCurrentUnitSystem
         // 
         this.groupBoxCurrentUnitSystem.Controls.Add(this.buttonCurrentUnitSys);
         this.groupBoxCurrentUnitSystem.Controls.Add(this.labelCurrent);
         this.groupBoxCurrentUnitSystem.Location = new System.Drawing.Point(4, 340);
         this.groupBoxCurrentUnitSystem.Name = "groupBoxCurrentUnitSystem";
         this.groupBoxCurrentUnitSystem.Size = new System.Drawing.Size(396, 40);
         this.groupBoxCurrentUnitSystem.TabIndex = 7;
         this.groupBoxCurrentUnitSystem.TabStop = false;
         this.groupBoxCurrentUnitSystem.Text = "Current Unit System";
         // 
         // buttonCurrentUnitSys
         // 
         this.buttonCurrentUnitSys.Location = new System.Drawing.Point(308, 12);
         this.buttonCurrentUnitSys.Name = "buttonCurrentUnitSys";
         this.buttonCurrentUnitSys.Size = new System.Drawing.Size(75, 23);
         this.buttonCurrentUnitSys.TabIndex = 5;
         this.buttonCurrentUnitSys.Text = "Set Current";
         this.buttonCurrentUnitSys.Click += new System.EventHandler(this.buttonCurrentUnitSys_Click);
         // 
         // labelCurrent
         // 
         this.labelCurrent.Location = new System.Drawing.Point(128, 12);
         this.labelCurrent.Name = "labelCurrent";
         this.labelCurrent.Size = new System.Drawing.Size(168, 23);
         this.labelCurrent.TabIndex = 6;
         this.labelCurrent.Text = "Current Unit System:";
         this.labelCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // tabPageNumberFormat
         // 
         this.tabPageNumberFormat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.tabPageNumberFormat.Controls.Add(this.groupBox1);
         this.tabPageNumberFormat.Controls.Add(this.domainUpDownDecPlaces);
         this.tabPageNumberFormat.Controls.Add(this.labelDecimalPlaces);
         this.tabPageNumberFormat.Location = new System.Drawing.Point(4, 22);
         this.tabPageNumberFormat.Name = "tabPageNumberFormat";
         this.tabPageNumberFormat.Size = new System.Drawing.Size(404, 387);
         this.tabPageNumberFormat.TabIndex = 3;
         this.tabPageNumberFormat.Text = "Numeric Format";
         // 
         // radioButtonScientific
         // 
         this.radioButtonScientific.Location = new System.Drawing.Point(18, 49);
         this.radioButtonScientific.Name = "radioButtonScientific";
         this.radioButtonScientific.Size = new System.Drawing.Size(104, 24);
         this.radioButtonScientific.TabIndex = 3;
         this.radioButtonScientific.Text = "Scientific";
         this.radioButtonScientific.CheckedChanged += new System.EventHandler(this.radioButtonScientific_CheckedChanged);
         // 
         // radioButtonFixedPoint
         // 
         this.radioButtonFixedPoint.Checked = true;
         this.radioButtonFixedPoint.Location = new System.Drawing.Point(18, 21);
         this.radioButtonFixedPoint.Name = "radioButtonFixedPoint";
         this.radioButtonFixedPoint.Size = new System.Drawing.Size(104, 24);
         this.radioButtonFixedPoint.TabIndex = 2;
         this.radioButtonFixedPoint.TabStop = true;
         this.radioButtonFixedPoint.Text = "Fixed Point";
         this.radioButtonFixedPoint.CheckedChanged += new System.EventHandler(this.radioButtonFixedPoint_CheckedChanged);
         // 
         // domainUpDownDecPlaces
         // 
         this.domainUpDownDecPlaces.BackColor = System.Drawing.Color.White;
         this.domainUpDownDecPlaces.Items.Add("20");
         this.domainUpDownDecPlaces.Items.Add("19");
         this.domainUpDownDecPlaces.Items.Add("18");
         this.domainUpDownDecPlaces.Items.Add("17");
         this.domainUpDownDecPlaces.Items.Add("16");
         this.domainUpDownDecPlaces.Items.Add("15");
         this.domainUpDownDecPlaces.Items.Add("14");
         this.domainUpDownDecPlaces.Items.Add("13");
         this.domainUpDownDecPlaces.Items.Add("12");
         this.domainUpDownDecPlaces.Items.Add("11");
         this.domainUpDownDecPlaces.Items.Add("10");
         this.domainUpDownDecPlaces.Items.Add("9");
         this.domainUpDownDecPlaces.Items.Add("8");
         this.domainUpDownDecPlaces.Items.Add("7");
         this.domainUpDownDecPlaces.Items.Add("6");
         this.domainUpDownDecPlaces.Items.Add("5");
         this.domainUpDownDecPlaces.Items.Add("4");
         this.domainUpDownDecPlaces.Items.Add("3");
         this.domainUpDownDecPlaces.Items.Add("2");
         this.domainUpDownDecPlaces.Items.Add("1");
         this.domainUpDownDecPlaces.Location = new System.Drawing.Point(122, 125);
         this.domainUpDownDecPlaces.Name = "domainUpDownDecPlaces";
         this.domainUpDownDecPlaces.ReadOnly = true;
         this.domainUpDownDecPlaces.Size = new System.Drawing.Size(48, 20);
         this.domainUpDownDecPlaces.TabIndex = 1;
         this.domainUpDownDecPlaces.SelectedItemChanged += new System.EventHandler(this.domainUpDownDecPlaces_SelectedItemChanged);
         // 
         // labelDecimalPlaces
         // 
         this.labelDecimalPlaces.Location = new System.Drawing.Point(18, 125);
         this.labelDecimalPlaces.Name = "labelDecimalPlaces";
         this.labelDecimalPlaces.Size = new System.Drawing.Size(100, 23);
         this.labelDecimalPlaces.TabIndex = 0;
         this.labelDecimalPlaces.Text = "Decimal Places:";
         this.labelDecimalPlaces.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.radioButtonScientific);
         this.groupBox1.Controls.Add(this.radioButtonFixedPoint);
         this.groupBox1.Location = new System.Drawing.Point(17, 19);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(173, 86);
         this.groupBox1.TabIndex = 4;
         this.groupBox1.TabStop = false;
         // 
         // ApplicationPreferencesForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(416, 417);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "ApplicationPreferencesForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Application Preferences";
         this.panel.ResumeLayout(false);
         this.tabControlPrefs.ResumeLayout(false);
         this.tabPageUnits.ResumeLayout(false);
         this.groupBoxCurrentUnitSystem.ResumeLayout(false);
         this.tabPageNumberFormat.ResumeLayout(false);
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.TabControl tabControlPrefs;
      private System.Windows.Forms.TabPage tabPageUnits;
      private System.Windows.Forms.GroupBox groupBoxCurrentUnitSystem;
      private System.Windows.Forms.Button buttonCurrentUnitSys;
      private System.Windows.Forms.Label labelCurrent;
      private System.Windows.Forms.TabPage tabPageNumberFormat;
      private System.Windows.Forms.RadioButton radioButtonScientific;
      private System.Windows.Forms.RadioButton radioButtonFixedPoint;
      private System.Windows.Forms.DomainUpDown domainUpDownDecPlaces;
      private System.Windows.Forms.Label labelDecimalPlaces;
      private System.Windows.Forms.GroupBox groupBox1;
   }
}