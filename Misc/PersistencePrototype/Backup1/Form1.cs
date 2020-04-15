using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Configuration;

namespace pers
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
   public class Form1 : System.Windows.Forms.Form
	{
     
      private F fRef;
      public F FRef
      {
         get 
         {
            return fRef;
         }
         set
         {
            fRef = value;
         }
      }

      private System.Windows.Forms.Button buttonNew;
      private System.Windows.Forms.Button buttonChange;
      private System.Windows.Forms.Button buttonOpen;
      private System.Windows.Forms.Button buttonSave;
      private System.Windows.Forms.Label labelAAARef_MyInt2;
      private System.Windows.Forms.Label labelAARef_MyInt;
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

         this.UpdateUI();
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
         this.buttonChange = new System.Windows.Forms.Button();
         this.buttonNew = new System.Windows.Forms.Button();
         this.buttonOpen = new System.Windows.Forms.Button();
         this.buttonSave = new System.Windows.Forms.Button();
         this.labelAAARef_MyInt2 = new System.Windows.Forms.Label();
         this.labelAARef_MyInt = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // buttonChange
         // 
         this.buttonChange.Location = new System.Drawing.Point(28, 80);
         this.buttonChange.Name = "buttonChange";
         this.buttonChange.TabIndex = 0;
         this.buttonChange.Text = "Change";
         this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
         // 
         // buttonNew
         // 
         this.buttonNew.Location = new System.Drawing.Point(28, 24);
         this.buttonNew.Name = "buttonNew";
         this.buttonNew.TabIndex = 1;
         this.buttonNew.Text = "New";
         this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
         // 
         // buttonOpen
         // 
         this.buttonOpen.Location = new System.Drawing.Point(156, 24);
         this.buttonOpen.Name = "buttonOpen";
         this.buttonOpen.TabIndex = 2;
         this.buttonOpen.Text = "Open";
         this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
         // 
         // buttonSave
         // 
         this.buttonSave.Location = new System.Drawing.Point(276, 24);
         this.buttonSave.Name = "buttonSave";
         this.buttonSave.TabIndex = 3;
         this.buttonSave.Text = "Save";
         this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
         // 
         // labelAAARef_MyInt2
         // 
         this.labelAAARef_MyInt2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(224)), ((System.Byte)(192)));
         this.labelAAARef_MyInt2.Location = new System.Drawing.Point(156, 124);
         this.labelAAARef_MyInt2.Name = "labelAAARef_MyInt2";
         this.labelAAARef_MyInt2.Size = new System.Drawing.Size(196, 23);
         this.labelAAARef_MyInt2.TabIndex = 5;
         this.labelAAARef_MyInt2.Text = "AAARef_MyInt2";
         // 
         // labelAARef_MyInt
         // 
         this.labelAARef_MyInt.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(224)), ((System.Byte)(192)));
         this.labelAARef_MyInt.Location = new System.Drawing.Point(156, 84);
         this.labelAARef_MyInt.Name = "labelAARef_MyInt";
         this.labelAARef_MyInt.Size = new System.Drawing.Size(196, 23);
         this.labelAARef_MyInt.TabIndex = 6;
         this.labelAARef_MyInt.Text = "AARef_MyInt";
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(384, 173);
         this.Controls.Add(this.labelAARef_MyInt);
         this.Controls.Add(this.labelAAARef_MyInt2);
         this.Controls.Add(this.buttonSave);
         this.Controls.Add(this.buttonOpen);
         this.Controls.Add(this.buttonNew);
         this.Controls.Add(this.buttonChange);
         this.Name = "Form1";
         this.Text = "Form1";
         this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
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

      private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         Application.Exit();
      }

      private void buttonNew_Click(object sender, System.EventArgs e)
      {
         New();
         this.UpdateUI();
      }

      public void New()
      {
         this.FRef = new F();
      }

      private void buttonOpen_Click(object sender, System.EventArgs e)
      {
         this.Open("test.xml");
         this.UpdateUI();
      }

      public bool Open(string fileName)
      {
         bool ok = true;
         Stream stream = null;
         try
         {
            stream = new FileStream(fileName, FileMode.Open);
            SoapFormatter serializer = new SoapFormatter();
            F f = (F)serializer.Deserialize(stream);

            this.FRef = f;
         }
         catch(Exception ex)
         {
            ok = false;
            string message = ex.ToString(); 
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally 
         {
            if (stream != null)
               stream.Close();
         }
         return ok;
      }

      private void buttonSave_Click(object sender, System.EventArgs e)
      {
         if (this.FRef != null)
         {
            this.Save("test.xml");
         }
      }

      public void Save(string fileName)
      {
         FileStream fs = null;
         try 
         {
            if (File.Exists(fileName)) 
            {
               fs = new FileStream(fileName, FileMode.Open);
            }
            else 
            {
               fs = new FileStream(fileName, FileMode.Create);
            }

            SoapFormatter serializer = new SoapFormatter();
            serializer.Serialize(fs, this.FRef);
         }
         catch(Exception ex)
         {
            string message = ex.ToString(); 
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally 
         {
            if (fs != null) 
            {
               fs.Flush();
               fs.Close();
            }
         }
      }

      private void buttonChange_Click(object sender, System.EventArgs e)
      {
         if (this.FRef != null)
         {
            this.FRef.AAARef.MyInt++;
            this.FRef.AAARef.MyInt2++;
            this.FRef.AARef.MyInt++;
            this.FRef.AAARef.BBRef.MyInt++;
            this.FRef.AARef.BBRef.MyInt++;
            this.UpdateUI();
         }
      }

      private void UpdateUI()
      {
         if (this.FRef != null)
         {
            this.labelAARef_MyInt.Text = "AARef_MyInt = " + FRef.AARef.MyInt;
            this.labelAAARef_MyInt2.Text = "AAARef_MyInt2 = " + FRef.AAARef.MyInt2;
         }
         else
         {
            this.labelAARef_MyInt.Text = "";
            this.labelAAARef_MyInt2.Text = "";
         }
      }
	}
}
