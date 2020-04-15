using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MachineName
{

    public class MainForm : System.Windows.Forms.Form
    {

        private System.Windows.Forms.Button buttonClose;
        private System.ComponentModel.Container components;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TextBox textBoxName;

        public MainForm()
        {
            components = null;
            InitializeComponent();
            textBoxName.Text = System.Environment.MachineName;
        }

       private void buttonClose_Click(object sender, EventArgs e) {
          Application.Exit();
       }

       private void InitializeComponent ()
       {
          this.buttonClose = new System.Windows.Forms.Button();
          this.labelInfo = new System.Windows.Forms.Label();
          this.textBoxName = new System.Windows.Forms.TextBox();
          this.SuspendLayout();
          // 
          // buttonClose
          // 
          this.buttonClose.Location = new System.Drawing.Point(173, 119);
          this.buttonClose.Name = "buttonClose";
          this.buttonClose.Size = new System.Drawing.Size(75, 23);
          this.buttonClose.TabIndex = 0;
          this.buttonClose.Text = "Close";
          this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
          // 
          // labelInfo
          // 
          this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelInfo.Location = new System.Drawing.Point(8, 16);
          this.labelInfo.Name = "labelInfo";
          this.labelInfo.Size = new System.Drawing.Size(279, 48);
          this.labelInfo.TabIndex = 1;
          this.labelInfo.Text = "The name of this computer is:";
          this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // textBoxName
          // 
          this.textBoxName.Location = new System.Drawing.Point(4, 80);
          this.textBoxName.Name = "textBoxName";
          this.textBoxName.ReadOnly = true;
          this.textBoxName.Size = new System.Drawing.Size(395, 20);
          this.textBoxName.TabIndex = 2;
          // 
          // MainForm
          // 
          this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
          this.ClientSize = new System.Drawing.Size(411, 154);
          this.Controls.Add(this.textBoxName);
          this.Controls.Add(this.labelInfo);
          this.Controls.Add(this.buttonClose);
          this.Name = "MainForm";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Computer Name";
          this.ResumeLayout(false);
          this.PerformLayout();

       }

       protected override void Dispose(bool disposing) {
          if (disposing && (this.components != null)) {
             this.components.Dispose();
          }
          base.Dispose(disposing);
       }

       [System.STAThread]
       public static void Main()
       {
          System.Windows.Forms.Application.Run(new MachineName.MainForm());
       }

   } // class MainForm

}

