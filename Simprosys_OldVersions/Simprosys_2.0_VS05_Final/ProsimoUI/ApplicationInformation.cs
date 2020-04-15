using System;
//using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProsimoUI
{
   public class ApplicationInformation
   {
      public const string COMPANY = "Simprotek Corporation (www.simprotek.com)";
      public const string PRODUCT= "Simprosys";
      public const string COPYRIGHT = "Copyright © Simprotek Corporation 2006";
      //private const string VERSION = "1.0.1.1";
      private const string VERSION = "1.1.0.0";
      public const string SLOGAN = @"Help Improve Your Productivity";

      public static Version ProductVersion {
         get {
            return new Version(VERSION);
         }
      }

      public static string ProductVersionString {
         get {
            Version v = new Version(VERSION);
            StringBuilder sb = new StringBuilder(v.Major + "." + v.Minor);
            if (v.Build != 0) {
               sb.Append(v.Build);
            }
            return sb.ToString();
         }
      }

      public static string ApplicationStartupPath {
         get { return Application.StartupPath + Path.DirectorySeparatorChar; }
      }
   }
}
