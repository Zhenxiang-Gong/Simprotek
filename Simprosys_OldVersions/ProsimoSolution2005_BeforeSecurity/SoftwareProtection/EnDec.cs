using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Prosimo.SoftwareProtection
{
	/// <summary>
	/// Summary description for EnDec.
	/// </summary>
	public class EnDec
	{
      private byte[] key = {0x01, 0x22, 0x43, 0x04, 0x15, 0x63, 0x33, 0x59,
                            0x09, 0x10, 0x21, 0x12, 0x13, 0x34, 0x78, 0x61};
      private byte[] iv = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                           0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
      private RijndaelManaged rijndaelManaged;

		public EnDec()
		{
         this.rijndaelManaged = new RijndaelManaged();
		}

      public byte[] Encrypt(string stringToEncrypt)
      {
         ASCIIEncoding textConverter = new ASCIIEncoding();
         ICryptoTransform encryptor = this.rijndaelManaged.CreateEncryptor(key, iv);
         MemoryStream memoryStream = new MemoryStream();
         CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
         byte[] bytesToEncrypt = textConverter.GetBytes(stringToEncrypt);
         cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
         cryptoStream.FlushFinalBlock();
         byte[] bytesEncrypted = memoryStream.ToArray();
         cryptoStream.Close();
         memoryStream.Close();
         return bytesEncrypted;
      }

      public string Decrypt(byte[] bytesToDecrypt)
      {
         ASCIIEncoding textConverter = new ASCIIEncoding();
         ICryptoTransform decryptor = this.rijndaelManaged.CreateDecryptor(key, iv);
         MemoryStream memoryStream = new MemoryStream(bytesToDecrypt);
         CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
         byte[] bytesDecrypted = new byte[bytesToDecrypt.Length];
         cryptoStream.Read(bytesDecrypted, 0, bytesDecrypted.Length);
         string stringDecrypted = textConverter.GetString(this.TrimEnd(bytesDecrypted));
         cryptoStream.Close();
         memoryStream.Close();
         return stringDecrypted;
      }

      private byte[] TrimEnd(byte[] input)
      {
         int idx;
         for (idx=0; idx<input.Length; idx++)
         {
            if (input[idx] == '\0')
               break;
         }

         byte[] output = new byte[idx];
         for (int j=0; j<output.Length; j++)
         {
            output[j] = input[j];
         }
         return output;
      }
	}
}
