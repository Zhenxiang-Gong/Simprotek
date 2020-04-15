using System;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Forms;

using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.YawsCorrelations;

namespace Prosimo.ThermalPropExtractor {
   class ThermalPropExtrractorYawsOrganic : ThermalPropextractor {

      override internal void ExtractCoeffs(ListBox listBox, SubstanceType substanceType, ThermPropType thermProp) {

         propList.Clear();
         ThermalPropCorrelationBase thermalPropCorrelation;

         string[] separatorsStr1 = { "</td>" };
         string[] separatorsStr2 = { "<i>", "</i>" };
         string name;
         string casRegestryNo;
         double a, b, c, d = 1, e = 1;
         double molarWeight, criticalRho, criticalT, criticalP, criticalV, criticalComp, accentricF;
         molarWeight = criticalRho = criticalT = criticalP = criticalV = criticalComp = accentricF = -2147483D;
         double minTemp = 1, maxTemp = 1;

         CriticalPropsAndAcentricFactor criticalProps;
         Substance substance;
         SubstanceFormula substanceFormula;

         string line1, line2, lineTemp;
         int fileNo = 0;
         int substanceNo = 0;
         string state = "";

         int counter;
         int totalCounter = 0;
         StreamReader reader;
         FileStream fsRead = null;
         
         try {
            foreach (object fullFileName in listBox.Items) {
               fileNo++;
               counter = 0;
               fsRead = new FileStream((string)fullFileName, FileMode.Open, FileAccess.Read);
               reader = new StreamReader(fsRead);
               line1 = reader.ReadLine();
               while (!reader.EndOfStream && counter < 50) {
                  line2 = reader.ReadLine();
                  name = "";
                  casRegestryNo = "";
                  if (thermProp != ThermPropType.CriticalProp && thermProp != ThermPropType.EnthalpyOfCombustion) {
                     if (line1.Contains("<a border")) {
                        counter++;

                        name = ExtractName(line2, separatorsStr1, separatorsStr2);

                        if (!(thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.SolidCp ||
                           thermProp == ThermPropType.SurfaceTension)) {
                           lineTemp = reader.ReadLine().Trim();
                           casRegestryNo = ExtractCasNo(lineTemp, separatorsStr1);
                        }
                        else {
                           lineTemp = reader.ReadLine();
                        }

                        lineTemp = reader.ReadLine(); //formula
                        if (thermProp == ThermPropType.LiquidCp || thermProp == ThermPropType.SolidCp ||
                            thermProp == ThermPropType.SurfaceTension) {
                           lineTemp = reader.ReadLine().Trim();
                           casRegestryNo = ExtractCasNo(lineTemp, separatorsStr1);
                        }
                        else {
                           lineTemp = reader.ReadLine(); //molar weight
                        }

                        lineTemp = reader.ReadLine();
                        a = ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        b = ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        c = ExtractValue(lineTemp, separatorsStr1);

                        if (a == -2147483D || b == -2147483D || c == -2147483D) {
                           line1 = line2;
                           continue;
                        }

                        if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.LiquidCp ||
                           thermProp == ThermPropType.VapPressure || thermProp == ThermPropType.LiquidDensity ||
                           thermProp == ThermPropType.LiquidDensity || thermProp == ThermPropType.LiquidVisc) {

                           lineTemp = reader.ReadLine();
                           d = ExtractValue(lineTemp, separatorsStr1);

                           if (d == -2147483D) {
                              line1 = line2;
                              continue;
                           }
                        }

                        if (thermProp == ThermPropType.GasCp || thermProp == ThermPropType.VapPressure) {
                           lineTemp = reader.ReadLine();
                           e = ExtractValue(lineTemp, separatorsStr1);
                           if (e == -2147483D) {
                              line1 = line2;
                              continue;
                           }
                        }

                        lineTemp = reader.ReadLine();
                        minTemp = ExtractValue(lineTemp, separatorsStr1);

                        lineTemp = reader.ReadLine();
                        maxTemp = ExtractValue(lineTemp, separatorsStr1);

                        if (thermProp == ThermPropType.GasCp) {
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
                           //Liquid thermal conductivity of Yaws organic is different from Yaws
                           thermalPropCorrelation = new YawsOrganicLiquidThermalConductivityCorrelation(name, casRegestryNo, a, b, c, minTemp, maxTemp);
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
                  else {
                     substanceNo = totalCounter + counter + 1;
                     if ((line1.StartsWith(substanceNo.ToString() + "</td><td") || 
                        //since there is a duplicate for No. 3976 this condition must be added to handle it
                        line1.StartsWith((substanceNo-1).ToString() + "</td><td")) && counter < 50) {
                        counter++;

                        name = ExtractName(line2, separatorsStr1, separatorsStr2);

                        lineTemp = reader.ReadLine();
                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                           casRegestryNo = ExtractCasNo(lineTemp, separatorsStr1);
                        }
                        else {
                           while (!lineTemp.Contains("<a border")) {
                              lineTemp = reader.ReadLine();
                           }
                        }

                        lineTemp = reader.ReadLine();
                        substanceFormula = ExtractSubstanceFormula(lineTemp);

                        //for debugging use only
                        //formula = sb.ToString();

                        lineTemp = reader.ReadLine();
                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                           molarWeight = ExtractValue(lineTemp, separatorsStr1);
                        }
                        else {
                           casRegestryNo = ExtractCasNo(lineTemp, separatorsStr1);
                        }

                        lineTemp = reader.ReadLine();
                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                           criticalT = ExtractValue(lineTemp, separatorsStr1);
                        }
                        else {
                           state = ExtractCasNo(lineTemp, separatorsStr1);
                        }

                        lineTemp = reader.ReadLine();
                        criticalP = ExtractValue(lineTemp, separatorsStr1);//for EnthalpyOfCombustion this is the value for mole based
                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                              if (criticalP != -2147483D) {
                              criticalP *= 1.0e5;//convert from bar to Pa
                           }
                        }

                        lineTemp = reader.ReadLine();
                        criticalV = ExtractValue(lineTemp, separatorsStr1); //for EnthalpyOfCombustion this is the value for kg based
                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                           if (criticalV != -2147483D) {
                              criticalV *= 1.0e-6; //convert from cm3/mol to m3/mol
                           }
                        }

                        lineTemp = reader.ReadLine(); //skip critical density since it can be obtained from critical volume and molar weight
                        //subStrs = lineTemp.Trim().Split(separators);
                        //criticalRho = double.Parse(subStrs[0]);

                        if (thermProp != ThermPropType.EnthalpyOfCombustion) {
                           lineTemp = reader.ReadLine();
                           criticalComp = ExtractValue(lineTemp, separatorsStr1);

                           lineTemp = reader.ReadLine();
                           accentricF = ExtractValue(lineTemp, separatorsStr1);
                        }

                        if (thermProp == ThermPropType.EnthalpyOfCombustion) {
                           thermalPropCorrelation = new YawsEnthalpyOfCombustionCorrelation(name, casRegestryNo, state, criticalP, criticalV, 298.15);
                           propList.Add(thermalPropCorrelation);
                        }
                        else {
                           criticalProps = new CriticalPropsAndAcentricFactor(criticalT, criticalP, criticalV, criticalComp, accentricF);
                           substance = new Substance(name, substanceType, casRegestryNo, substanceFormula, molarWeight, criticalProps);
                           propList.Add(substance);
                        }
                     }

                     line1 = line2;
                  }
               }

               totalCounter = totalCounter + counter;

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

      private SubstanceFormula ExtractSubstanceFormula(string lineTemp) {
         string elementStr;
         string tmpStr;
         int elementCount;
         string[] separators = { "<sub>", "</sub>" };

         string[] subStrs = lineTemp.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries);
         SubstanceFormula substanceFormula = new SubstanceFormula();
         for (int i = 0; i < subStrs.Length - 1; i++) {
            if (!char.IsDigit(subStrs[i][0])) {
               elementStr = subStrs[i];
               tmpStr = elementStr;

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
               if (char.IsDigit(subStrs[i][0])) {
                  tmpStr = subStrs[i];
                  if (char.IsDigit(subStrs[i + 1][0])) {
                     i++;
                     tmpStr += subStrs[i];
                  }

                  elementCount = int.Parse(tmpStr);
                  substanceFormula.AddElement(elementStr, elementCount);
               }
            }
         }

         if (!subStrs[subStrs.Length - 1].StartsWith("</td>")) {
            string[] separator1 = { "</td>" };
            subStrs = subStrs[subStrs.Length - 1].Trim().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            elementStr = subStrs[0];
            substanceFormula.AddElement(elementStr, 1);
         }

         return substanceFormula;
      }
      
      private string GetFileName(ThermPropType prop) {

         string fileName = "SubstancesOrganic.dat"; ;

         if (prop == ThermPropType.GasCp) {
            fileName = "YawsOrganicGasCpCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidCp) {
            fileName = "YawsOrganicLiquidCpCorrelations.dat";
         }
         if (prop == ThermPropType.SolidCp) {
            fileName = "YawsOrganicSolidCpCorrelations.dat";
         }
         else if (prop == ThermPropType.EvapHeat) {
            fileName = "YawsOrganicEvaporationHeatCorrelations.dat";
         }
         else if (prop == ThermPropType.VapPressure) {
            fileName = "YawsOrganicVaporPressureCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidDensity) {
            fileName = "YawsOrganicLiquidDensityCorrelations.dat";
         }
         else if (prop == ThermPropType.GasVisc) {
            fileName = "YawsOrganicGasViscosityCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidVisc) {
            fileName = "YawsOrganicLiquidViscosityCorrelations.dat";
         }
         else if (prop == ThermPropType.GasK) {
            fileName = "YawsOrganicGasThermalConductivityCorrelations.dat";
         }
         else if (prop == ThermPropType.LiquidK) {
            fileName = "YawsOrganicLiquidThermalConductivityCorrelations.dat";
         }
         else if (prop == ThermPropType.SurfaceTension) {
            fileName = "YawsOrganicSurfaceTensionCorrelations.dat";
         }
         else if (prop == ThermPropType.EnthalpyOfFormation) {
            fileName = "YawsOrganicEnthalpyOfFormationCorrelations.dat";
         }
         else if (prop == ThermPropType.EnthalpyOfCombustion) {
            fileName = "YawsOrganicEnthalpyOfCombustionCorrelations.dat";
         }


         return fileName;
      }
   }
}
