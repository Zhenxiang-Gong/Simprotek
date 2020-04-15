using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace ProsimoUI.Help
{
	/// <summary>
	/// Summary description for ChooseTutorialForm.
	/// </summary>
	public class ChooseTutorialForm : System.Windows.Forms.Form
	{
      private const string TUTORIALS_FILE_NAME = "tutorials.txt";

      private SortedList tutorials;
      private bool viewButtonClicked;

      private string tutorialName;
      public string TutorialName
      {
         get {return tutorialName;}
      }

      private string tutorialFile;
      public string TutorialFile
      {
         get {return tutorialFile;}
      }

      private System.Windows.Forms.ListBox listBoxTutorials;
      private System.Windows.Forms.Button buttonView;
      private ProsimoUI.Help.InformationControl informationControl;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      private System.Windows.Forms.Panel panel;
      private IContainer components;

      public ChooseTutorialForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.tutorials = new SortedList();
         this.viewButtonClicked = false;

         string exePathName = Application.StartupPath + Path.DirectorySeparatorChar;
//         string tutorialsFile = exePathName + TutorialForm.HELP_DIRECTORY + TutorialForm.TUTORIAL_DIRECTORY + ChooseTutorialForm.TUTORIALS_FILE_NAME;
         string tutorialsFile = exePathName + TutorialForm.TUTORIAL_DIRECTORY + ChooseTutorialForm.TUTORIALS_FILE_NAME;
         if (File.Exists(tutorialsFile))
         {
            try 
            {
               using (StreamReader strReader = new StreamReader(tutorialsFile))
               {
                  String line;
                  while ((line = strReader.ReadLine()) != null) 
                  {
                     string delimStr = "=";
                     char[] delimiter = delimStr.ToCharArray();
                     string[] split = line.Split(delimiter, 2);
                     this.tutorials.Add(split[0].Trim(), split[1].Trim());
                  }
               }
               
               IDictionaryEnumerator en = this.tutorials.GetEnumerator();
               while (en.MoveNext())
               {
                  DictionaryEntry entry = (DictionaryEntry)en.Current;
                  string name = entry.Key as string;
                  this.listBoxTutorials.Items.Add(name);
               }

               if (this.listBoxTutorials.Items.Count > 0)
                  this.listBoxTutorials.SelectedIndex = 0;
            }
            catch (Exception) 
            {
            }
         }
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
         this.components = new System.ComponentModel.Container();
         this.listBoxTutorials = new System.Windows.Forms.ListBox();
         this.buttonView = new System.Windows.Forms.Button();
         this.informationControl = new ProsimoUI.Help.InformationControl();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.panel = new System.Windows.Forms.Panel();
         this.panel.SuspendLayout();
         this.SuspendLayout();
         // 
         // listBoxTutorials
         // 
         this.listBoxTutorials.FormattingEnabled = true;
         this.listBoxTutorials.Location = new System.Drawing.Point(8, 105);
         this.listBoxTutorials.Name = "listBoxTutorials";
         this.listBoxTutorials.Size = new System.Drawing.Size(288, 160);
         this.listBoxTutorials.TabIndex = 0;
         // 
         // buttonView
         // 
         this.buttonView.Location = new System.Drawing.Point(112, 272);
         this.buttonView.Name = "buttonView";
         this.buttonView.Size = new System.Drawing.Size(75, 23);
         this.buttonView.TabIndex = 1;
         this.buttonView.Text = "View";
         this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
         // 
         // informationControl
         // 
         this.informationControl.Location = new System.Drawing.Point(4, 4);
         this.informationControl.Name = "informationControl";
         this.informationControl.Size = new System.Drawing.Size(300, 96);
         this.informationControl.TabIndex = 2;
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
         this.panel.Controls.Add(this.listBoxTutorials);
         this.panel.Controls.Add(this.buttonView);
         this.panel.Controls.Add(this.informationControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(306, 303);
         this.panel.TabIndex = 3;
         // 
         // ChooseTutorialForm
         // 
         this.ClientSize = new System.Drawing.Size(306, 303);
         this.Controls.Add(this.panel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Menu = this.mainMenu;
         this.MinimizeBox = false;
         this.Name = "ChooseTutorialForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Tutorials";
         this.TopMost = true;
         this.Closing += new System.ComponentModel.CancelEventHandler(this.ChooseTutorialForm_Closing);
         this.panel.ResumeLayout(false);
         this.ResumeLayout(false);

      }
		#endregion

      private void buttonView_Click(object sender, System.EventArgs e)
      {
         this.viewButtonClicked = true;
         this.Close();
      }

      private void ChooseTutorialForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (this.viewButtonClicked)
         {
            if (this.listBoxTutorials.Items.Count > 0)
            {
               this.tutorialName = this.listBoxTutorials.SelectedItem as string;
               this.tutorialFile = this.tutorials[this.tutorialName] as string;
            }
            else
            {
               this.tutorialName = null;
               this.tutorialFile = null;
            }
         }
         else
         {
            this.tutorialName = null;
            this.tutorialFile = null;
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
	}
}
