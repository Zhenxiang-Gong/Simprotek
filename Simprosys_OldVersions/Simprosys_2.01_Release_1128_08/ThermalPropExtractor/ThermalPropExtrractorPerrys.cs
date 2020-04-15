using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Forms;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.PerrysCorrelations;

namespace Prosimo.ThermalPropExtractor {
   class ThermalPropExtrractorPerrys : ThermalPropextractor {

      override internal void ExtractCoeffs(ListBox listBox, SubstanceType substanceType, ThermPropType thermProp) {
         
         propList.Clear();

         string[] separatorsStr1 = { "</td>" };
         string[] separatorsStr2 = { "<i>", "</i>" };
         string name;
         string casRegestryNo;
         double a, b, c, d = 1, e = 1;
         double tc = 1;
         double minTemp = 1, maxTemp = 1;

         string line1, line2, lineTemp;
         int correlationType;
         int counter;
         FileStream fsRead = null;
         StreamReader reader;

         ThermalPropCorrelationBase thermalPropCorrelation;

         try {
            foreach (object fullFileName in listBox.Items) {
               counter = 0;
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               line1 = reader.ReadLine();
               while (!reader.EndOfStream && counter < 50) {

                  line2 = reader.ReadLine();
                  if (line1.Contains("&nbsp") || line1.Contains("<a border")) {
                     counter++;

                     correlationType = 1;
                     name = ExtractName(line2, separatorsStr1, separatorsStr2);

                     lineTemp = reader.ReadLine(); //formula

                     lineTemp = reader.ReadLine().Trim();
                     casRegestryNo = ExtractCasNo(lineTemp, separatorsStr1);

                     if (thermProp != ThermPropType.VapPressure) {  //molar weight is not listed in vapor pressure table 
                        lineTemp = reader.ReadLine();
                     }

                     lineTemp = reader.ReadLine();
                     if (lineTemp == null) {
                        continue;
                     }
                     a = ExtractValue(lineTemp, separatorsStr1);

                     lineTemp = reader.ReadLine();
                     b = ExtractValue(lineTemp, separatorsStr1);

                     lineTemp = reader.ReadLine();
                     c = ExtractValue(lineTemp, separatorsStr1);

                     lineTemp = reader.ReadLine();
                     d = ExtractValue(lineTemp, separatorsStr1);

                     if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.LiquidCp
                        || thermProp == ThermPropType.VapPressure) {
                        lineTemp = reader.ReadLine();
                        e = ExtractValue(lineTemp, separatorsStr1);
                     }

                     lineTemp = reader.ReadLine();
                     minTemp = ExtractValue(lineTemp, separatorsStr1);

                     lineTemp = reader.ReadLine();

                     lineTemp = reader.ReadLine();
                     maxTemp = ExtractValue(lineTemp, separatorsStr1);

                     if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.EvapHeat) {
                        lineTemp = reader.ReadLine();

                        lineTemp = reader.ReadLine();
                        tc = ExtractValue(lineTemp, separatorsStr1);
                     }

                     if (thermProp == ThermPropType.GasCp) {
                        correlationType = 1;
                        if (name == "helium-4" || name == "nitric oxide") {
                           correlationType = 2;
                        }
                        else if (name == "propylbenzene") {
                           correlationType = 3;
                        }
                        thermalPropCorrelation = new PerrysGasCpCorrelation(name, casRegestryNo, a * 1e5, b * 1e5, c * 1e3, d * 1e5, e, minTemp, maxTemp, correlationType);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidCp) {
                        correlationType = 1;
                        if (name == "methane" || name == "ethane" || name == "propane" || name == "n-butane"
                            || name == "n-heptane" || name == "hydrogen" || name == "ammonia" || name == "carbon monoxide"
                            || name == "hydrogen sulfide") {
                           correlationType = 2;
                        }
                        thermalPropCorrelation = new PerrysLiquidCpCorrelation(name, casRegestryNo, a, b, c, d, e, tc, minTemp, maxTemp, correlationType);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.EvapHeat) {
                        thermalPropCorrelation = new PerrysEvaporationHeatCorrelation(name, casRegestryNo, a * 1.0e7, b, c, d, tc, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.VapPressure) {
                        thermalPropCorrelation = new PerrysVaporPressureCorrelation(name, casRegestryNo, a, b, c, d, e, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidDensity) {
                        thermalPropCorrelation = new PerrysLiquidDensityCorrelation(name, casRegestryNo, a, b, c, d, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                  }

                  line1 = line2;
               }
               reader.Close();
               fsRead.Close();
            }
         }
         catch (Exception ex) {
            Console.WriteLine("The process failed: {0}", ex.ToString());
         }
         finally {
            if (fsRead != null) {
               fsRead.Close();
            }
         }

         PersistProp(GetFileName(thermProp), propList);
         UnpersistProp(GetFileName(thermProp));
      }

      private string GetFileName(ThermPropType prop) {
         string fileName = "PerrysGasCpCorrelations.dat";
         if (prop == ThermPropType.LiquidCp) {
            fileName = "PerrysLiquidCpCorrelations.dat";
         }
         else if (prop == ThermPropType.EvapHeat) {
            fileName = "PerrysEvaporationHeatCorrelations.dat";
         }
         else if (prop == ThermPropType.VapPressure) {
            fileName = "PerrysVaporPressureCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidDensity) {
            fileName = "PerrysLiquidDensityCorrelations.dat";
         }
         return fileName;
      }
   }
}
