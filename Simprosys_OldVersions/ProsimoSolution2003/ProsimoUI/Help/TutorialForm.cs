using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProsimoUI.Help
{
   /// <summary>
   /// Summary description for TutorialForm.
   /// </summary>
   public class TutorialForm : System.Windows.Forms.Form
   {
      public const string TUTORIAL_DIRECTORY = "Tutorials\\";
      public const string HELP_DIRECTORY = "Help\\";
      public const string PROSIMO = "ProSimO - ";

      private AxSHDocVw.AxWebBrowser axWebBrowser;
      private System.Windows.Forms.MainMenu mainMenu;
      private System.Windows.Forms.MenuItem menuItemClose;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public TutorialForm()
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();
      }

      public TutorialForm(string tutorialName, string tutorialFile)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         this.SetTutorial(tutorialName, tutorialFile);
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TutorialForm));
         this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
         this.mainMenu = new System.Windows.Forms.MainMenu();
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
         this.SuspendLayout();
         // 
         // axWebBrowser
         // 
         this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.axWebBrowser.Enabled = true;
         this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
         this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
         this.axWebBrowser.Size = new System.Drawing.Size(700, 606);
         this.axWebBrowser.TabIndex = 0;
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
         // TutorialForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(700, 606);
         this.Controls.Add(this.axWebBrowser);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Menu = this.mainMenu;
         this.Name = "TutorialForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "ProSimO -  ";
         ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
         this.ResumeLayout(false);

      }
    
      #endregion

      public void SetTutorial(string tutorialName, string tutorialFile)
      {
         this.Text = TutorialForm.PROSIMO + tutorialName;
         string exePathName = Application.StartupPath + Path.DirectorySeparatorChar;
         string link = exePathName + TutorialForm.HELP_DIRECTORY + TutorialForm.TUTORIAL_DIRECTORY + tutorialFile;
         this.Navigate(link);
      }

      private void Navigate(string link)
      {
         try
         {
            object na = null;
            this.axWebBrowser.Navigate(link, ref na, ref na, ref na, ref na);
         }
         catch (Exception)
         {
         }
      }

      private void menuItemClose_Click(object sender, System.EventArgs e)
      {
         this.Close();
      }
   }
}
