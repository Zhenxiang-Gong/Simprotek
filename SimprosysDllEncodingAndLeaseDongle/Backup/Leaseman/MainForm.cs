using System;
using System.IO;
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
      private const string ENCODED_DIR = "encoded";
      private const string DECODED_DIR = "decoded";

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
      private TabControl tabControl;
      private TabPage tabPageLease;
      private TabPage tabPageAssemblies;
      private Button buttonEncodeFiles;
      private Button buttonRemoveFiles;
      private Button buttonAddFiles;
      private ListBox listBoxAssemblies;
      private OpenFileDialog openFileDialog;
      private Button buttonDecodeFiles;
      private IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         this.radioButtonMonths.Checked = true;
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
         this.tabControl = new System.Windows.Forms.TabControl();
         this.tabPageLease = new System.Windows.Forms.TabPage();
         this.buttonGenerateDongle = new System.Windows.Forms.Button();
         this.textBoxComputerName = new System.Windows.Forms.TextBox();
         this.groupBoxLeaseDuration = new System.Windows.Forms.GroupBox();
         this.textBoxLeaseDuration = new System.Windows.Forms.TextBox();
         this.radioButtonDays = new System.Windows.Forms.RadioButton();
         this.radioButtonMonths = new System.Windows.Forms.RadioButton();
         this.radioButtonYears = new System.Windows.Forms.RadioButton();
         this.labelComputerName = new System.Windows.Forms.Label();
         this.textBoxLease = new System.Windows.Forms.TextBox();
         this.tabPageAssemblies = new System.Windows.Forms.TabPage();
         this.buttonEncodeFiles = new System.Windows.Forms.Button();
         this.buttonRemoveFiles = new System.Windows.Forms.Button();
         this.buttonAddFiles = new System.Windows.Forms.Button();
         this.listBoxAssemblies = new System.Windows.Forms.ListBox();
         this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
         this.menuItemClose = new System.Windows.Forms.MenuItem();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.buttonDecodeFiles = new System.Windows.Forms.Button();
         this.panel.SuspendLayout();
         this.tabControl.SuspendLayout();
         this.tabPageLease.SuspendLayout();
         this.groupBoxLeaseDuration.SuspendLayout();
         this.tabPageAssemblies.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel
         // 
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel.Controls.Add(this.tabControl);
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Location = new System.Drawing.Point(0, 0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(456, 295);
         this.panel.TabIndex = 1;
         // 
         // tabControl
         // 
         this.tabControl.Controls.Add(this.tabPageLease);
         this.tabControl.Controls.Add(this.tabPageAssemblies);
         this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl.Location = new System.Drawing.Point(0, 0);
         this.tabControl.Name = "tabControl";
         this.tabControl.SelectedIndex = 0;
         this.tabControl.Size = new System.Drawing.Size(452, 291);
         this.tabControl.TabIndex = 5;
         // 
         // tabPageLease
         // 
         this.tabPageLease.Controls.Add(this.buttonGenerateDongle);
         this.tabPageLease.Controls.Add(this.textBoxComputerName);
         this.tabPageLease.Controls.Add(this.groupBoxLeaseDuration);
         this.tabPageLease.Controls.Add(this.labelComputerName);
         this.tabPageLease.Controls.Add(this.textBoxLease);
         this.tabPageLease.Location = new System.Drawing.Point(4, 22);
         this.tabPageLease.Name = "tabPageLease";
         this.tabPageLease.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageLease.Size = new System.Drawing.Size(444, 265);
         this.tabPageLease.TabIndex = 0;
         this.tabPageLease.Text = "Dongle";
         this.tabPageLease.UseVisualStyleBackColor = true;
         // 
         // buttonGenerateDongle
         // 
         this.buttonGenerateDongle.Location = new System.Drawing.Point(165, 184);
         this.buttonGenerateDongle.Name = "buttonGenerateDongle";
         this.buttonGenerateDongle.Size = new System.Drawing.Size(104, 23);
         this.buttonGenerateDongle.TabIndex = 1;
         this.buttonGenerateDongle.Text = "Generate Dongle";
         this.buttonGenerateDongle.Click += new System.EventHandler(this.buttonGenerateDongle_Click);
         // 
         // textBoxComputerName
         // 
         this.textBoxComputerName.Location = new System.Drawing.Point(101, 132);
         this.textBoxComputerName.Name = "textBoxComputerName";
         this.textBoxComputerName.Size = new System.Drawing.Size(180, 20);
         this.textBoxComputerName.TabIndex = 4;
         // 
         // groupBoxLeaseDuration
         // 
         this.groupBoxLeaseDuration.Controls.Add(this.textBoxLeaseDuration);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonDays);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonMonths);
         this.groupBoxLeaseDuration.Controls.Add(this.radioButtonYears);
         this.groupBoxLeaseDuration.Location = new System.Drawing.Point(9, 12);
         this.groupBoxLeaseDuration.Name = "groupBoxLeaseDuration";
         this.groupBoxLeaseDuration.Size = new System.Drawing.Size(104, 112);
         this.groupBoxLeaseDuration.TabIndex = 0;
         this.groupBoxLeaseDuration.TabStop = false;
         this.groupBoxLeaseDuration.Text = "Lease Duration";
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
         this.radioButtonDays.Location = new System.Drawing.Point(8, 60);
         this.radioButtonDays.Name = "radioButtonDays";
         this.radioButtonDays.Size = new System.Drawing.Size(76, 20);
         this.radioButtonDays.TabIndex = 2;
         this.radioButtonDays.Text = "Days";
         this.radioButtonDays.CheckedChanged += new System.EventHandler(this.radioButtonDays_CheckedChanged);
         // 
         // radioButtonMonths
         // 
         this.radioButtonMonths.Location = new System.Drawing.Point(8, 40);
         this.radioButtonMonths.Name = "radioButtonMonths";
         this.radioButtonMonths.Size = new System.Drawing.Size(76, 20);
         this.radioButtonMonths.TabIndex = 1;
         this.radioButtonMonths.Text = "Months";
         this.radioButtonMonths.CheckedChanged += new System.EventHandler(this.radioButtonMonths_CheckedChanged);
         // 
         // radioButtonYears
         // 
         this.radioButtonYears.Location = new System.Drawing.Point(8, 20);
         this.radioButtonYears.Name = "radioButtonYears";
         this.radioButtonYears.Size = new System.Drawing.Size(76, 20);
         this.radioButtonYears.TabIndex = 0;
         this.radioButtonYears.Text = "Years";
         this.radioButtonYears.CheckedChanged += new System.EventHandler(this.radioButtonYears_CheckedChanged);
         // 
         // labelComputerName
         // 
         this.labelComputerName.Location = new System.Drawing.Point(9, 132);
         this.labelComputerName.Name = "labelComputerName";
         this.labelComputerName.Size = new System.Drawing.Size(92, 20);
         this.labelComputerName.TabIndex = 3;
         this.labelComputerName.Text = "Computer Name:";
         this.labelComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // textBoxLease
         // 
         this.textBoxLease.Location = new System.Drawing.Point(117, 16);
         this.textBoxLease.Multiline = true;
         this.textBoxLease.Name = "textBoxLease";
         this.textBoxLease.ReadOnly = true;
         this.textBoxLease.Size = new System.Drawing.Size(312, 108);
         this.textBoxLease.TabIndex = 2;
         // 
         // tabPageAssemblies
         // 
         this.tabPageAssemblies.Controls.Add(this.buttonDecodeFiles);
         this.tabPageAssemblies.Controls.Add(this.buttonEncodeFiles);
         this.tabPageAssemblies.Controls.Add(this.buttonRemoveFiles);
         this.tabPageAssemblies.Controls.Add(this.buttonAddFiles);
         this.tabPageAssemblies.Controls.Add(this.listBoxAssemblies);
         this.tabPageAssemblies.Location = new System.Drawing.Point(4, 22);
         this.tabPageAssemblies.Name = "tabPageAssemblies";
         this.tabPageAssemblies.Padding = new System.Windows.Forms.Padding(3);
         this.tabPageAssemblies.Size = new System.Drawing.Size(444, 265);
         this.tabPageAssemblies.TabIndex = 1;
         this.tabPageAssemblies.Text = "Assemblies";
         this.tabPageAssemblies.UseVisualStyleBackColor = true;
         this.tabPageAssemblies.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPageAssemblies_MouseClick);
         // 
         // buttonEncodeFiles
         // 
         this.buttonEncodeFiles.Location = new System.Drawing.Point(340, 227);
         this.buttonEncodeFiles.Name = "buttonEncodeFiles";
         this.buttonEncodeFiles.Size = new System.Drawing.Size(98, 23);
         this.buttonEncodeFiles.TabIndex = 3;
         this.buttonEncodeFiles.Text = "Encode Files";
         this.buttonEncodeFiles.UseVisualStyleBackColor = true;
         this.buttonEncodeFiles.Click += new System.EventHandler(this.buttonEncodeFiles_Click);
         // 
         // buttonRemoveFiles
         // 
         this.buttonRemoveFiles.Location = new System.Drawing.Point(87, 227);
         this.buttonRemoveFiles.Name = "buttonRemoveFiles";
         this.buttonRemoveFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonRemoveFiles.TabIndex = 2;
         this.buttonRemoveFiles.Text = "Remove";
         this.buttonRemoveFiles.UseVisualStyleBackColor = true;
         this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
         // 
         // buttonAddFiles
         // 
         this.buttonAddFiles.Location = new System.Drawing.Point(6, 227);
         this.buttonAddFiles.Name = "buttonAddFiles";
         this.buttonAddFiles.Size = new System.Drawing.Size(75, 23);
         this.buttonAddFiles.TabIndex = 1;
         this.buttonAddFiles.Text = "Add";
         this.buttonAddFiles.UseVisualStyleBackColor = true;
         this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
         // 
         // listBoxAssemblies
         // 
         this.listBoxAssemblies.FormattingEnabled = true;
         this.listBoxAssemblies.HorizontalScrollbar = true;
         this.listBoxAssemblies.Location = new System.Drawing.Point(6, 6);
         this.listBoxAssemblies.Name = "listBoxAssemblies";
         this.listBoxAssemblies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
         this.listBoxAssemblies.Size = new System.Drawing.Size(432, 212);
         this.listBoxAssemblies.TabIndex = 0;
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
         // openFileDialog
         // 
         this.openFileDialog.Multiselect = true;
         // 
         // buttonDecodeFiles
         // 
         this.buttonDecodeFiles.Location = new System.Drawing.Point(236, 227);
         this.buttonDecodeFiles.Name = "buttonDecodeFiles";
         this.buttonDecodeFiles.Size = new System.Drawing.Size(98, 23);
         this.buttonDecodeFiles.TabIndex = 4;
         this.buttonDecodeFiles.Text = "Decode Files";
         this.buttonDecodeFiles.UseVisualStyleBackColor = true;
         this.buttonDecodeFiles.Click += new System.EventHandler(this.buttonDecodeFiles_Click);
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(456, 295);
         this.Controls.Add(this.panel);
         this.Menu = this.mainMenu;
         this.Name = "MainForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Lease Manager";
         this.panel.ResumeLayout(false);
         this.tabControl.ResumeLayout(false);
         this.tabPageLease.ResumeLayout(false);
         this.tabPageLease.PerformLayout();
         this.groupBoxLeaseDuration.ResumeLayout(false);
         this.groupBoxLeaseDuration.PerformLayout();
         this.tabPageAssemblies.ResumeLayout(false);
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

      private void buttonAddFiles_Click(object sender, EventArgs e)
      {
         openFileDialog.Filter = "exe files (*.exe)|*.exe|dll files (*.dll)|*.dll";
         openFileDialog.FilterIndex = 2;

         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            listBoxAssemblies.BeginUpdate();
            for (int i = 0; i < openFileDialog.FileNames.Length; i++)
            {
               string fileName = openFileDialog.FileNames[i];
               if (!listBoxAssemblies.Items.Contains(fileName))
                  listBoxAssemblies.Items.Add(openFileDialog.FileNames[i]);
            }
            listBoxAssemblies.EndUpdate();
         }

      }

      private void buttonRemoveFiles_Click(object sender, EventArgs e)
      {
         ArrayList list = new ArrayList();
         foreach (object fileName in listBoxAssemblies.SelectedItems)
         {
            list.Add(fileName);
         }

         foreach (object fileName in list)
         {
            listBoxAssemblies.Items.Remove(fileName);
         }
      }

      private void buttonEncodeFiles_Click(object sender, EventArgs e)
      {
         listBoxAssemblies.ClearSelected();
         this.Cursor = Cursors.WaitCursor;

         // create a /encoded folder
         string startupPath = Application.StartupPath;
         string newPath = startupPath + Path.DirectorySeparatorChar.ToString() + MainForm.ENCODED_DIR;

         FileStream fsRead = null;
         FileStream fsWrite = null;
         try
         {
            DirectoryInfo directoryInfo = null;
            if (Directory.Exists(newPath))
            {
               string msg = "The directory exists! Do you want to delete it?";
               DialogResult dr = MessageBox.Show(msg, "Delete Directory Encoded", MessageBoxButtons.YesNo);
               if (dr == DialogResult.Yes)
               {
                  Directory.Delete(newPath, true);
                  directoryInfo = Directory.CreateDirectory(newPath);
               }
               else
               {
                  return;
               }
            }
            else
            {
               directoryInfo = Directory.CreateDirectory(newPath);
            }


            // go on the listbox and take every file name
            // get the file in memory, encode it and save it in /encoded
            EnDec encoder = new EnDec();
            foreach (object fullFileName in listBoxAssemblies.Items)
            {
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               byte[] dataToEncode = new byte[fsRead.Length];
               for (int i = 0; i < fsRead.Length; i++)
               {
                  dataToEncode[i] = (byte)fsRead.ReadByte();
               }
               byte[] encodedData = encoder.EncryptBytes(dataToEncode);
               fsRead.Close();

               string fileName = Path.GetFileName((string)fullFileName);
               string eFileName = "E" + fileName; // add the "E" at the beginning of the name
               string fullNewFileName = newPath + Path.DirectorySeparatorChar.ToString() + eFileName;

               fsWrite = new FileStream(fullNewFileName, FileMode.Create);
               for (int i = 0; i < encodedData.Length; i++)
               {
                  fsWrite.WriteByte(encodedData[i]);
               }
               fsWrite.Close();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("The process failed: {0}", ex.ToString());
         }
         finally
         {
            if (fsRead != null) {
               fsRead.Close();
            }
            if (fsWrite != null) {
               fsWrite.Close();
            }
         }
         this.Cursor = Cursors.Default;
      }

      private void tabPageAssemblies_MouseClick(object sender, MouseEventArgs e)
      {
         listBoxAssemblies.ClearSelected();
      }

      private void buttonDecodeFiles_Click(object sender, EventArgs e)
      {
         listBoxAssemblies.ClearSelected();
         this.Cursor = Cursors.WaitCursor;

         // create a /decoded folder
         string startupPath = Application.StartupPath;
         string newPath = startupPath + Path.DirectorySeparatorChar.ToString() + MainForm.DECODED_DIR;

         FileStream fsRead = null;
         FileStream fsWrite = null;
         try
         {
            DirectoryInfo directoryInfo = null;
            if (Directory.Exists(newPath))
            {
               string msg = "The directory exists! Do you want to delete it?";
               DialogResult dr = MessageBox.Show(msg, "Delete Directory Decoded", MessageBoxButtons.YesNo);
               if (dr == DialogResult.Yes)
               {
                  Directory.Delete(newPath, true);
                  directoryInfo = Directory.CreateDirectory(newPath);
               }
               else
               {
                  return;
               }
            }
            else
            {
               directoryInfo = Directory.CreateDirectory(newPath);
            }


            // go on the listbox and take every file name
            // get the file in memory, decode it and save it in /decoded
            EnDec decoder = new EnDec();
            foreach (object fullFileName in listBoxAssemblies.Items)
            {
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               byte[] dataToDecode = new byte[fsRead.Length];
               for (int i = 0; i < fsRead.Length; i++)
               {
                  dataToDecode[i] = (byte)fsRead.ReadByte();
               }
               byte[] decodedData = decoder.DecryptToBytes(dataToDecode);
               fsRead.Close();

               string fileName = Path.GetFileName((string)fullFileName);
               string dFileName = fileName.Substring(1); // eliminates the "E" from the begining of the name
               string fullNewFileName = newPath + Path.DirectorySeparatorChar.ToString() + dFileName;

               fsWrite = new FileStream(fullNewFileName, FileMode.Create);
               for (int i = 0; i < decodedData.Length; i++)
               {
                  fsWrite.WriteByte(decodedData[i]);
               }
               fsWrite.Close();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("The process failed: {0}", ex.ToString());
         }
         finally
         {
            fsRead.Close();
            fsWrite.Close();
         }
         this.Cursor = Cursors.Default;
      }
	}
}
