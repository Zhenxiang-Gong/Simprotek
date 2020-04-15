using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ProsimoBootstrap
{
   class ProsimoDecoder
   {
      private byte[] key = {0x01, 0x22, 0x43, 0x04, 0x15, 0x63, 0x33, 0x59, 0x09, 0x10, 0x21, 0x12, 0x13, 0x34, 0x78, 0x61};
      private byte[] iv = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
      private RijndaelManaged rijndaelManaged;

      public ProsimoDecoder()
		{
         this.rijndaelManaged = new RijndaelManaged();
		}

      public byte[] DecryptToBytes(byte[] bytesToDecrypt)
      {
         ICryptoTransform decryptor = this.rijndaelManaged.CreateDecryptor(key, iv);
         MemoryStream memoryStream = new MemoryStream();

         // Create a CryptoStream through which we are going to be pumping our data. 
         // CryptoStreamMode.Write means that we are going to be writing data to the stream 
         // and the output will be written in the MemoryStream we have provided. 
         CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);

         // Write the data and make it do the decryption 
         cryptoStream.Write(bytesToDecrypt, 0, bytesToDecrypt.Length);

         // Close the crypto stream (or do FlushFinalBlock). 
         // This will tell it that we have done our decryption and there is no more data coming in, 
         // and it is now a good time to remove the padding and finalize the decryption process. 
         cryptoStream.FlushFinalBlock();
         cryptoStream.Close();

         // Now get the decrypted data from the MemoryStream. 
         // Some people make a mistake of using GetBuffer() here, which is not the right way. 
         byte[] bytesDecrypted = memoryStream.ToArray();
         memoryStream.Close();
         return bytesDecrypted; 
      }

      public byte[] GetArrayOfBytesFromFile(string fileName)
      {
         byte[] assemblyArray = null;
         FileStream stream = null;
         try
         {
            stream = new FileStream(fileName, FileMode.Open);
            assemblyArray = new byte[stream.Length];
            stream.Read(assemblyArray, 0, (int)stream.Length);
         }
         catch (Exception)// e)
         {
            //            string message = e.ToString();
            //            System.Windows.Forms.MessageBox.Show(message, "Open&Read File Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
         }
         finally
         {
            if (stream != null)
               stream.Close();
         }
         return assemblyArray;
      }
   }
}
