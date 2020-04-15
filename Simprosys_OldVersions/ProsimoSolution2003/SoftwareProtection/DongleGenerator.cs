using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Prosimo.SoftwareProtection
{
	/// <summary>
	/// Summary description for DongleGenerator.
	/// </summary>
	public class DongleGenerator
	{
      public const string DONGLE = "dongle.dat";

      private string pathName;
      public string PathName
      {
         get {return pathName;}
         set {pathName = value;}
      }

		public DongleGenerator()
		{
         this.pathName = Application.StartupPath + Path.DirectorySeparatorChar;
		}

      public DongleGenerator(string pathName)
      {
         this.pathName = pathName;
      }

      public bool GenerateDongle(Lease lease)
      {
         string fullFileName = this.pathName + DongleGenerator.DONGLE;
         if (File.Exists(fullFileName))
         {
            return false;
         }
         else
         {
            this.WriteDongle(lease);
            return true;
         }
      }

      public void OverwriteDongle(Lease lease)
      {
         string fullFileName = this.pathName + DongleGenerator.DONGLE;
         if (File.Exists(fullFileName))
         {
            File.Delete(fullFileName);
         }
         this.WriteDongle(lease);
      }

      private void WriteDongle(Lease lease)
      {
         string fullFileName = this.pathName + DongleGenerator.DONGLE;
         EnDec enDec = new EnDec();
         byte[] bytesEncrypted = enDec.Encrypt(lease.ToString());
         FileStream fs = new FileStream(fullFileName, FileMode.CreateNew);
         BinaryWriter binaryWriter = new BinaryWriter(fs);
         binaryWriter.Write(bytesEncrypted);
         binaryWriter.Close();
      }

      public bool DongleExists()
      {
         string fullFileName = this.pathName + DongleGenerator.DONGLE;
         if (File.Exists(fullFileName))
            return true;
         else
            return false;
      }

      public Lease GetLease()
      {
         Lease lease = null;
         string fullFileName = this.pathName + DongleGenerator.DONGLE;
         if (File.Exists(fullFileName))
         {
            FileStream fs = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fs);
            byte[] bytesEncrypted = binaryReader.ReadBytes(1000);
            binaryReader.Close();
            EnDec enDec = new EnDec();
            string stringDecrypted = enDec.Decrypt(bytesEncrypted);
            lease = new Lease(stringDecrypted);
         }
         return lease;
      }
	}
}
