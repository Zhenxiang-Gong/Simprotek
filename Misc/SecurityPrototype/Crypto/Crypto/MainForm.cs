using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Crypto
{
   public partial class MainForm : Form
   {
      private const string ENCODED_DIR = "encoded";
      private const string DECODED_DIR = "decoded";

      public MainForm()
      {
         InitializeComponent();
      }

      private void buttonEncode_Click(object sender, EventArgs e)
      {
         string fullFileName = null;

         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            fullFileName = openFileDialog.FileName;

            // ENCODE
            // create a /encoded folder
            string startupPath = Application.StartupPath;
            string encodePath = startupPath + Path.DirectorySeparatorChar.ToString() + MainForm.ENCODED_DIR;

            FileStream fsRead = null;
            FileStream fsWrite = null;
            try
            {
               DirectoryInfo directoryInfo = null;
               if (Directory.Exists(encodePath))
               {
                  string msg = "The directory exists! Do you want to delete it?";
                  DialogResult dr = MessageBox.Show(msg, "Delete Directory Encoded", MessageBoxButtons.YesNo);
                  if (dr == DialogResult.Yes)
                  {
                     Directory.Delete(encodePath, true);
                     directoryInfo = Directory.CreateDirectory(encodePath);
                     directoryInfo.Attributes = FileAttributes.Normal;
                  }
                  else
                  {
                     return;
                  }
               }
               else
               {
                  directoryInfo = Directory.CreateDirectory(encodePath);
                  directoryInfo.Attributes = FileAttributes.Normal;
               }

               EnDec encoder = new EnDec();
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               byte[] dataToEncode = new byte[fsRead.Length];
               for (int i = 0; i < fsRead.Length; i++)
               {
                  dataToEncode[i] = (byte)fsRead.ReadByte();
               }
               byte[] encodedData = encoder.EncryptBytes(dataToEncode);
               fsRead.Close();

               string fileName = Path.GetFileName((string)fullFileName);
               string fullNewFileName = encodePath + Path.DirectorySeparatorChar.ToString() + fileName;

               fsWrite = new FileStream(fullNewFileName, FileMode.Create);
               for (int i = 0; i < encodedData.Length; i++)
               {
                  fsWrite.WriteByte(encodedData[i]);
               }
               fsWrite.Close();
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
         
         }
      }

      private void buttonDecode_Click(object sender, EventArgs e)
      {
         string fullFileName = null;

         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            fullFileName = openFileDialog.FileName;

            // create a /decoded folder
            string startupPath = Application.StartupPath;
            string decodePath = startupPath + Path.DirectorySeparatorChar.ToString() + MainForm.DECODED_DIR;

            FileStream fsRead = null;
            FileStream fsWrite = null;
            try
            {
               DirectoryInfo directoryInfo = null;
               if (Directory.Exists(decodePath))
               {
                  string msg = "The directory exists! Do you want to delete it?";
                  DialogResult dr = MessageBox.Show(msg, "Delete Directory Decoded", MessageBoxButtons.YesNo);
                  if (dr == DialogResult.Yes)
                  {
                     Directory.Delete(decodePath, true);
                     directoryInfo = Directory.CreateDirectory(decodePath);
                     directoryInfo.Attributes = FileAttributes.Normal;
                  }
                  else
                  {
                     return;
                  }
               }
               else
               {
                  directoryInfo = Directory.CreateDirectory(decodePath);
                  directoryInfo.Attributes = FileAttributes.Normal;
               }


               // go on the listbox and take every file name
               // get the file in memory, decode it and save it in /decoded
               EnDec decoder = new EnDec();
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               byte[] dataToDecode = new byte[fsRead.Length];
               for (int i = 0; i < fsRead.Length; i++)
               {
                  dataToDecode[i] = (byte)fsRead.ReadByte();
               }
               byte[] decodedData = decoder.DecryptToBytes(dataToDecode);
               fsRead.Close();

               string fileName = Path.GetFileName((string)fullFileName);
               string fullNewFileName = decodePath + Path.DirectorySeparatorChar.ToString() + fileName;

               fsWrite = new FileStream(fullNewFileName, FileMode.Create);
               for (int i = 0; i < decodedData.Length; i++)
               {
                  fsWrite.WriteByte(decodedData[i]);
               }
               fsWrite.Close();
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
         }
      }
   }
}