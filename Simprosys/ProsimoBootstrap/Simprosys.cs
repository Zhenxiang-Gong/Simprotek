using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace ProsimoBootstrap
{
   class Simprosys
   {
      private SimprosysDecoder decoder;

      public Simprosys()
      {
         decoder = new SimprosysDecoder();
         //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
      }

      private void StartApplication(string[] args)
      {
         Application.Run(new ProsimoUI.MainForm(args));
      }

      Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
      {
         // Materials.dll
         // Plots.dll
         // Prosimo.dll
         // ProsimoUI.dll
         // SoftwareProtection.dll
         // SubstanceLibrary.dll
         // ThermalProperties.dll
         // UnitOperations.dll
         // UnitSystems.dll

         string fullAssemblyName = args.Name;
         int commaIdx = fullAssemblyName.IndexOf(",");
         string assemblyName = fullAssemblyName.Remove(commaIdx);
         string assemblyFileName = "E" + assemblyName + ".dll";

         byte[] encodedAssArray2 = decoder.GetArrayOfBytesFromFile(assemblyFileName);
         byte[] decodedAssArray2 = decoder.DecryptToBytes(encodedAssArray2);
         Assembly assembly = Assembly.Load(decodedAssArray2);

         return assembly;
      }

      [STAThread]
      static void Main(string[] args)
      {
         Simprosys p = new Simprosys();
         p.StartApplication(args);
      }

      //[STAThread]
      //static void Main()
      //{
      //   string[] args = null;
      //   Program p = new Program();
      //   p.StartApplication(args);
      //}
   }
}
