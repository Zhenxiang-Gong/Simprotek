using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Crypto
{
   class EnDec
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
         cryptoStream.Close();
         byte[] bytesEncrypted = memoryStream.ToArray();
         memoryStream.Close();
         return bytesEncrypted;
      }

      public byte[] EncryptBytes(byte[] bytesToEncrypt)
      {
         ICryptoTransform encryptor = this.rijndaelManaged.CreateEncryptor(key, iv);
         MemoryStream memoryStream = new MemoryStream();
         CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
         cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
         cryptoStream.FlushFinalBlock();
         cryptoStream.Close();
         byte[] bytesEncrypted = memoryStream.ToArray();
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

      private byte[] TrimEnd(byte[] input)
      {
         int idx;
         for (idx = 0; idx < input.Length; idx++)
         {
            if (input[idx] == '\0')
               break;
         }

         byte[] output = new byte[idx];
         for (int j = 0; j < output.Length; j++)
         {
            output[j] = input[j];
         }
         return output;
      }
   }
}
