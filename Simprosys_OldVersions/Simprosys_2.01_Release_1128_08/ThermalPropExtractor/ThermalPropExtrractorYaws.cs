using System;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.YawsCorrelations;

namespace Prosimo.ThermalPropExtractor {
   class ThermalPropExtrractorYaws : ThermalPropextractor {
      static SubstanceType prevSubstanceType;
      static ThermPropType prevThermalProp;

      override internal void ExtractCoeffs(ListBox listBox, SubstanceType substanceType, ThermPropType thermProp) {

         if ((prevThermalProp != thermProp) || (prevThermalProp == thermProp && prevSubstanceType == substanceType)) {
            propList.Clear();
         }

         ThermalPropCorrelationBase thermalPropCorrelation;

         string[] separatorsStr1 = { "<TD class=BorderHelper noWrap>", "<TD class=BorderBottomHelper noWrap>", "</TD>" };
         string[] separatorsStr2 = { "<I>", "</I>" };
         string[] separatorsStr3 = { "<TD class=BorderHelper>", "<TD class=BorderBottomHelper>", "</TD>" };
         string[] separatorsStr4 = { "<SUB>", "</SUB>" };

         string name;
         string casRegestryNo;
         double a, b, c, d = 1, e = 1, f, g; //, bp = 1;
         double molarWeight, freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, acentricF;
         molarWeight = freezingPoint = boilingPoint = criticalT = criticalP = criticalV = criticalComp = acentricF = -2147483D;
         double minTemp = 1, maxTemp = 1;

         string line1, line2, line3, lineTemp;
         bool isStartOfData;

         CriticalPropsAndAcentricFactor criticalProps;
         Substance substance;
         SubstanceFormula substanceFormula;

         StreamReader reader;
         FileStream fsRead = null;

         try {
            foreach (object fullFileName in listBox.Items) {
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               lineTemp = reader.ReadLine().Trim();
               line1 = lineTemp;
               while (!reader.EndOfStream) {
                  isStartOfData = false;
                  if (line1.StartsWith("<TD class=BorderHelper>") || line1.Contains("SUB")) {
                     isStartOfData = true;
                     //need to repair the first line some times
                     if (!line1.EndsWith("</TD>")) {
                        while (!line1.EndsWith("</TD>")) {
                           line1 += " " + reader.ReadLine().Trim();
                        }
                     }
                  }

                  line2 = reader.ReadLine().Trim();

                  //need to repair the second line some times
                  if (isStartOfData && line2.StartsWith("<TD class=BorderHelper") && !line2.EndsWith("</TD>")) {
                     while (!line2.EndsWith("</TD>")) {
                        line2 += " " + reader.ReadLine().Trim();
                     }
                  }

                  if (isStartOfData && (line2.StartsWith("<TD class=BorderHelper noWrap>") || line2.StartsWith("<TD class=BorderBottomHelper noWrap>"))) {

                     substanceFormula = ExtractFormula(line1, separatorsStr3, separatorsStr4);

                     name = ExtractName(line2, separatorsStr1, separatorsStr2);

                     line3 = reader.ReadLine().Trim();
                     casRegestryNo = ExtractCasNo(line3, separatorsStr3);

                     lineTemp = reader.ReadLine();
                     a = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.CriticalProp) {
                        molarWeight = a;
                     }

                     lineTemp = reader.ReadLine();
                     b = ExtractValue(lineTemp, separatorsStr3);

                     if (thermProp == ThermPropType.CriticalProp) {
                        freezingPoint = b;
                     }

                     lineTemp = reader.ReadLine();
                     c = ExtractValue(lineTemp, separatorsStr3);

                     if (thermProp == ThermPropType.CriticalProp) {
                        boilingPoint = c;
                     }

                     lineTemp = reader.ReadLine();
                     d = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.SolidCp || thermProp == ThermPropType.EvapHeat ||
                        thermProp == ThermPropType.GasVisc || thermProp == ThermPropType.GasK ||
                        thermProp == ThermPropType.LiquidK || thermProp == ThermPropType.SurfaceTension ||
                        thermProp == ThermPropType.EnthalpyOfFormation) {
                        minTemp = d;
                     }
                     else if (thermProp == ThermPropType.CriticalProp) {
                        criticalT = d;
                     }

                     lineTemp = reader.ReadLine();
                     e = ExtractValue(lineTemp, separatorsStr3);
                     if (thermProp == ThermPropType.SolidCp || thermProp == ThermPropType.EvapHeat ||
                        thermProp == ThermPropType.GasVisc || thermProp == ThermPropType.GasK ||
                        thermProp == ThermPropType.LiquidK || thermProp == ThermPropType.SurfaceTension ||
                        thermProp == ThermPropType.EnthalpyOfFormation) {
                        maxTemp = e;
                     }
                     else if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {
                        minTemp = e;
                     }
                     else if (thermProp == ThermPropType.CriticalProp) {
                        criticalP = 1.0e5 * e;//convert from bar to Pa
                     }

                     if (thermProp == ThermPropType.EvapHeat || thermProp == ThermPropType.CriticalProp ||
                        thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure ||
                        thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity ||
                        thermProp == ThermPropType.LiquidVisc) {
                        lineTemp = reader.ReadLine();
                        f = ExtractValue(lineTemp, separatorsStr3);

                        //if (thermProp == ThermProp.EvapHeat) {
                        //   bp = f;
                        //}
                        if (thermProp == ThermPropType.CriticalProp) {
                           criticalV = f * 1.0e-6; //convert from cm3/mol to m3/mol
                           lineTemp = reader.ReadLine();//skip critical density
                        }
                        else if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           minTemp = f;
                        }
                        else if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {
                           maxTemp = f;
                        }
                     }

                     if (thermProp == ThermPropType.CriticalProp || thermProp == ThermPropType.GasCp ||
                        thermProp == ThermPropType.VapPressure) {
                        lineTemp = reader.ReadLine();
                        g = ExtractValue(lineTemp, separatorsStr3);

                        if (thermProp == ThermPropType.CriticalProp) {
                           criticalComp = g;
                        }
                        else if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           maxTemp = g;
                        }
                     }

                     if (thermProp == ThermPropType.CriticalProp) {
                        lineTemp = reader.ReadLine();
                        acentricF = ExtractValue(lineTemp, separatorsStr3);
                     }

                     if (thermProp == ThermPropType.CriticalProp) {
                        criticalProps = new CriticalPropsAndAcentricFactor(freezingPoint, boilingPoint, criticalT, criticalP, criticalV, criticalComp, acentricF);
                        substance = new Substance(name, substanceType, casRegestryNo, substanceFormula, molarWeight, criticalProps);
                        propList.Add(substance);
                     }
                     else if (thermProp == ThermPropType.GasCp) {
                        thermalPropCorrelation = new YawsGasCpCorrelation(name, casRegestryNo, a, b, c, d, e, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidCp) {
                        thermalPropCorrelation = new YawsLiquidCpCorrelation(name, casRegestryNo, a, b, c, d, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.SolidCp) {
                        thermalPropCorrelation = new YawsSolidCpCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.EvapHeat) {
                        thermalPropCorrelation = new YawsEvaporationHeatCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.VapPressure) {
                        thermalPropCorrelation = new YawsVaporPressureCorrelation(name, casRegestryNo, a, b, c, d, e, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidDensity) {
                        thermalPropCorrelation = new YawsLiquidDensityCorrelation(name, casRegestryNo, a, b, c, d, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.GasVisc) {
                        thermalPropCorrelation = new YawsGasViscosityCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidVisc) {
                        thermalPropCorrelation = new YawsLiquidViscosityCorrelation(name, casRegestryNo, a, b, c, d, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.GasK) {
                        thermalPropCorrelation = new YawsGasThermalConductivityCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.LiquidK) {
                        thermalPropCorrelation = new YawsLiquidSolidThermalConductivityCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.SurfaceTension) {
                        thermalPropCorrelation = new YawsSurfaceTensionCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
                        propList.Add(thermalPropCorrelation);
                     }
                     else if (thermProp == ThermPropType.EnthalpyOfFormation) {
                        thermalPropCorrelation = new YawsEnthalpyOfFormationCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
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

         if (substanceType == SubstanceType.Inorganic) {
            PersistProp(GetFileName(thermProp), propList);
            UnpersistProp(GetFileName(thermProp));
         }

         prevSubstanceType = substanceType;
         prevThermalProp = thermProp;
      }

      protected override string ExtractName(string line, string[] separatorsStr1, string[] separatorsStr2) {

         string name = "";

         //<TD class=BorderHelper noWrap><SPAN class=Greek>g</SPAN>-butyrolactone </TD>
         if (line.Contains("<SPAN class=Greek>")) {
            string[] separatorsStr3 = { "<TD class=BorderHelper noWrap><SPAN class=Greek>", "</SPAN>", "</TD>" };
            string[] subStrs = line.Trim().Split(separatorsStr3, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in subStrs) {
               name += s;
            }
         }
         else {

            string[] subStrs = line.Trim().Split(separatorsStr1, StringSplitOptions.RemoveEmptyEntries);
            subStrs = subStrs[0].Trim().Split(separatorsStr2, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in subStrs) {
               name += s;
            }
         }

         return name;
      }


      private SubstanceFormula ExtractFormula(string aLine, string[] separatorsStr3, string[] separatorsStr4) {
         string tmpStr;
         string elementStr;
         int elementCount;
         SubstanceFormula substanceFormula = new SubstanceFormula();
         string[] subStrs = aLine.Trim().Split(separatorsStr3, StringSplitOptions.RemoveEmptyEntries);
         subStrs = subStrs[0].Trim().Split(separatorsStr4, StringSplitOptions.RemoveEmptyEntries);
         for (int i = 0; i < subStrs.Length; i++) {
            if (!char.IsDigit(subStrs[i][0])) {
               elementStr = subStrs[i];
               elementCount = 1;
               tmpStr = elementStr;
               if (tmpStr.Contains("class=BorderHelper>")) {
                  string[] separatorsStr = {"class=BorderHelper>"};
                  string[] subSubStrs = tmpStr.Trim().Split(separatorsStr, StringSplitOptions.RemoveEmptyEntries);
                  tmpStr = subSubStrs[0];
               }
               if (tmpStr.Length == 1 || tmpStr.Length == 2 && char.IsLower(tmpStr[1])) {
                  elementStr = tmpStr;
               }
               else if (!char.IsDigit(tmpStr[0])) {
                  if (tmpStr.Length == 2) {
                     elementStr = tmpStr.Substring(0, 1);
                     substanceFormula.AddElement(elementStr, 1);
                     elementStr = tmpStr.Substring(1, 1);
                  }
                  else {
                     for (int j = 0; j < tmpStr.Length; j++) {
                        if (j == (tmpStr.Length - 1) && char.IsUpper(tmpStr[j])) {
                           elementStr = tmpStr.Substring(j, 1);
                           continue;
                        }
                        else if (j == (tmpStr.Length - 2) && char.IsLower(tmpStr[tmpStr.Length - 1])) {
                           elementStr = tmpStr.Substring(j, 2);
                           j++;
                           continue;
                        }

                        if (char.IsUpper(tmpStr[j])) {
                           if (char.IsUpper(tmpStr[j + 1])) {
                              elementStr = tmpStr.Substring(j, 1);
                           }
                           else if (char.IsLower(tmpStr[j + 1])) {
                              elementStr = tmpStr.Substring(j, 2);
                              j++;
                           }
                        }
                        if (j < tmpStr.Length - 1) {
                           substanceFormula.AddElement(elementStr, 1);
                        }
                     }
                  }
               }
               
               i++;
               if (i < subStrs.Length && char.IsDigit(subStrs[i][0])) {
                  tmpStr = subStrs[i];
                  elementCount = int.Parse(tmpStr);
               }

               substanceFormula.AddElement(elementStr, elementCount);
            }
         }
         return substanceFormula;
      }

      private string GetFileName(ThermPropType prop) {
         string fileName = "Substances.dat";

         if (prop == ThermPropType.GasCp) {
            fileName = "YawsGasCpCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidCp) {
            fileName = "YawsLiquidCpCorrelations.dat";
         }
         if (prop == ThermPropType.SolidCp) {
            fileName = "YawsSolidCpCorrelations.dat";
         }
         else if (prop == ThermPropType.EvapHeat) {
            fileName = "YawsEvaporationHeatCorrelations.dat";
         }
         else if (prop == ThermPropType.VapPressure) {
            fileName = "YawsVaporPressureCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidDensity) {
            fileName = "YawsLiquidDensityCorrelations.dat";
         }
         else if (prop == ThermPropType.GasVisc) {
            fileName = "YawsGasViscosityCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidVisc) {
            fileName = "YawsLiquidViscosityCorrelations.dat";
         }
         else if (prop == ThermPropType.GasK) {
            fileName = "YawsGasThermalConductivityCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidK) {
            fileName = "YawsLiquidThermalConductivityCorrelations.dat";
         }
         else if (prop == ThermPropType.SurfaceTension) {
            fileName = "YawsSurfaceTensionCorrelations.dat";
         }
         else if (prop == ThermPropType.EnthalpyOfFormation) {
            fileName = "YawsEnthalpyOfFormationCorrelations.dat";
         }

         return fileName;
      }
   }
}
