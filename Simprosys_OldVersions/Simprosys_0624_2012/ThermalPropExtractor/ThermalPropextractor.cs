using System;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Runtime.Remoting.Messaging;

using Prosimo.SubstanceLibrary;

namespace Prosimo.ThermalPropExtractor {
   class ThermalPropextractor {

      protected static ArrayList propList = new ArrayList();

      private static string baseDirectory1 = "D:\\Private\\Simprotek\\Temp\\soap\\";
      private static string baseDirectory2 = "D:\\Private\\Simprotek\\Temp\\binary\\";

      internal virtual void ExtractCoeffs(ListBox listBox, SubstanceType substanceType, ThermPropType thermalProp) {
      }

      protected static string ExtractCasNo(string line, string[] separatorsStr) {
         string[] subStrs = line.Trim().Split(separatorsStr, StringSplitOptions.RemoveEmptyEntries);
         string casRegestryNo = "";
         if (!subStrs[0].Contains("&nbsp")) {
            casRegestryNo = subStrs[0].Trim();
         }

         return casRegestryNo;
      }

      protected virtual string ExtractName(string line, string[] separatorsStr1, string[] separatorsStr2) {

         string name = "";

         string[] subStrs = line.Trim().Split(separatorsStr1, StringSplitOptions.RemoveEmptyEntries);
         subStrs = subStrs[0].Trim().Split(separatorsStr2, StringSplitOptions.RemoveEmptyEntries);
         foreach (string s in subStrs) {
            name += s;
         }

         return name;
      }

      protected static double ExtractValue(string lineTemp, string[] separatorsStr) {
         double a;
         string[] subStrs = lineTemp.Trim().Split(separatorsStr, StringSplitOptions.RemoveEmptyEntries);
         if (subStrs[0].Contains("&nbsp")) {
            a = -2147483D;
         }
         else if (subStrs[0].Contains("<span class=")) {
            a = -2147483D;
            subStrs = subStrs[0].Split(new string[1] { "</span>" }, StringSplitOptions.RemoveEmptyEntries);
            a = -double.Parse(subStrs[1]);
         }
         else {
            a = double.Parse(subStrs[0]);
         }
         
         return a;
      }

      protected void PersistProp(string fileName, IList listToPersist) {
         SoapFormatter serializer1 = new SoapFormatter();
         serializer1.AssemblyFormat = FormatterAssemblyStyle.Simple;
         PersistProp(baseDirectory1 + fileName, listToPersist, serializer1);

         BinaryFormatter serializer2 = new BinaryFormatter();
         serializer2.AssemblyFormat = FormatterAssemblyStyle.Simple;
         PersistProp(baseDirectory2 + fileName, listToPersist, serializer2);
      }

      private void PersistProp(string fullFileName, IList listToPersist, IFormatter serializer) {
         FileStream fs = null;

         try {
            if (File.Exists(fullFileName)) {
               File.Delete(fullFileName);
            }
            fs = new FileStream(fullFileName, FileMode.Create);

            serializer.Serialize(fs, listToPersist);
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
         finally {
            if (fs != null) {
               fs.Flush();
               fs.Close();
            }
         }
      }

      protected void UnpersistProp(string fileName) {
         SoapFormatter serializer1 = new SoapFormatter();
         serializer1.AssemblyFormat = FormatterAssemblyStyle.Simple;
         UnpersistProp(baseDirectory1 + fileName, serializer1);

         BinaryFormatter serializer2 = new BinaryFormatter();
         serializer2.AssemblyFormat = FormatterAssemblyStyle.Simple;
         UnpersistProp(baseDirectory2 + fileName, serializer2);
      }

      private void UnpersistProp(string fullFileName, IFormatter serializer) {
         Stream stream = null;

         try {
            stream = new FileStream(fullFileName, FileMode.Open);

            IList thermalPropCorrelationList = (IList)serializer.Deserialize(stream);

            foreach (Storable s in thermalPropCorrelationList) {
               s.SetObjectData();
            }
         }
         catch (Exception e) {
            string message = e.ToString();
            MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            throw;
         }
         finally {
            stream.Close();
         }
      }
   }
}