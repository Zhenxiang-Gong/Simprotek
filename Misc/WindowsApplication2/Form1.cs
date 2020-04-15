using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace WindowsApplication2
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      public A a;

      private System.Windows.Forms.Button buttonSave;
      private System.Windows.Forms.Button buttonLoad;
      private System.Windows.Forms.TextBox textBox;
      private System.Windows.Forms.Button buttonClean;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         A a = new A();
         this.SetData(a);
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
         this.buttonSave = new System.Windows.Forms.Button();
         this.buttonLoad = new System.Windows.Forms.Button();
         this.textBox = new System.Windows.Forms.TextBox();
         this.buttonClean = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // buttonSave
         // 
         this.buttonSave.Location = new System.Drawing.Point(16, 16);
         this.buttonSave.Name = "buttonSave";
         this.buttonSave.TabIndex = 0;
         this.buttonSave.Text = "Save";
         this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
         // 
         // buttonLoad
         // 
         this.buttonLoad.Location = new System.Drawing.Point(200, 16);
         this.buttonLoad.Name = "buttonLoad";
         this.buttonLoad.TabIndex = 1;
         this.buttonLoad.Text = "Load";
         this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
         // 
         // textBox
         // 
         this.textBox.Location = new System.Drawing.Point(16, 68);
         this.textBox.Multiline = true;
         this.textBox.Name = "textBox";
         this.textBox.ReadOnly = true;
         this.textBox.Size = new System.Drawing.Size(236, 184);
         this.textBox.TabIndex = 2;
         this.textBox.Text = "";
         // 
         // buttonClean
         // 
         this.buttonClean.Location = new System.Drawing.Point(112, 16);
         this.buttonClean.Name = "buttonClean";
         this.buttonClean.TabIndex = 3;
         this.buttonClean.Text = "Clean";
         this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 266);
         this.Controls.Add(this.buttonClean);
         this.Controls.Add(this.textBox);
         this.Controls.Add(this.buttonLoad);
         this.Controls.Add(this.buttonSave);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

      private void buttonSave_Click(object sender, System.EventArgs e)
      {
         PersistenceManager.GetInstance().Save(this, "aaa.xml");
      }

      private void buttonLoad_Click(object sender, System.EventArgs e)
      {
         PersistenceManager.GetInstance().Open(this, "aaa.xml");
      }

      public void SetData(A a)
      {
         this.a = a;
         if (a != null)
         {
            StringBuilder sb = new StringBuilder();

            IEnumerator e = a.MyList.GetEnumerator();
            while (e.MoveNext())
            {
               B b = (B)e.Current;
               sb.Append(b.Id.ToString());
               sb.Append("\r\n");
            }

            IDictionaryEnumerator de = a.MyTable.GetEnumerator();
            while (de.MoveNext())
            {
               C c = (C)de.Value;
               sb.Append(c.Name);
               sb.Append("\r\n");
            }
            
            this.textBox.Text = sb.ToString();
         }
         else
            this.textBox.Text = "";
      }

      private void buttonClean_Click(object sender, System.EventArgs e)
      {
         this.SetData(null);
      }
	}

   public enum MyEnum
   {
      Zero,
      One,
      Two
   }
}
