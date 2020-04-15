using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using Prosimo.SubstanceLibrary;
using Prosimo.SubstanceLibrary.YawsCorrelations;
using Prosimo.SubstanceLibrary.PerrysCorrelations;

namespace Prosimo.ThermalProperties {

   /// <summary>
   /// Summary description for HeatCapacityCalculator.
   /// </summary>
   public class ThermalPropCalculator {
      private const double TOLERANCE = 1.0e-6;

      public static ThermalPropCalculator Instance = new ThermalPropCalculator();

      string baseDirectory;

      //Yaws Inorganic and organic
      private IDictionary<string, YawsGasCpCorrelation> gasCpTableYaws = new Dictionary<string, YawsGasCpCorrelation>();
      private IDictionary<string, YawsLiquidCpCorrelation> liquidCpTableYaws = new Dictionary<string, YawsLiquidCpCorrelation>();
      private IDictionary<string, YawsSolidCpCorrelation> solidCpTableYaws = new Dictionary<string, YawsSolidCpCorrelation>();
      private IDictionary<string, YawsVaporPressureCorrelation> vapPressureTableYaws = new Dictionary<string, YawsVaporPressureCorrelation>();
      private IDictionary<string, YawsEvaporationHeatCorrelation> evapHeatTableYaws = new Dictionary<string, YawsEvaporationHeatCorrelation>();
      private IDictionary<string, YawsLiquidDensityCorrelation> liquidDensityTableYaws = new Dictionary<string, YawsLiquidDensityCorrelation>();
      private IDictionary<string, YawsGasThermalConductivityCorrelation> gasKTableYaws = new Dictionary<string, YawsGasThermalConductivityCorrelation>();
      private IDictionary<string, YawsLiquidSolidThermalConductivityCorrelation> liquidKTableYaws = new Dictionary<string, YawsLiquidSolidThermalConductivityCorrelation>();
      private IDictionary<string, YawsGasViscosityCorrelation> gasViscTableYaws = new Dictionary<string, YawsGasViscosityCorrelation>();
      private IDictionary<string, YawsLiquidViscosityCorrelation> liquidViscTableYaws = new Dictionary<string, YawsLiquidViscosityCorrelation>();
      private IDictionary<string, YawsSurfaceTensionCorrelation> surfaceTensionTableYaws = new Dictionary<string, YawsSurfaceTensionCorrelation>();
      private IDictionary<string, YawsEnthalpyOfFormationCorrelation> enthalpyOfFormationTableYaws = new Dictionary<string, YawsEnthalpyOfFormationCorrelation>();
      
      //Yaws Organic
      private IDictionary<string, YawsGasCpCorrelation> gasCpTableYawsOrganic = new Dictionary<string, YawsGasCpCorrelation>();
      private IDictionary<string, YawsLiquidCpCorrelation> liquidCpTableYawsOrganic = new Dictionary<string, YawsLiquidCpCorrelation>();
      private IDictionary<string, YawsSolidCpCorrelation> solidCpTableYawsOrganic = new Dictionary<string, YawsSolidCpCorrelation>();
      private IDictionary<string, YawsVaporPressureCorrelation> vapPressureTableYawsOrganic = new Dictionary<string, YawsVaporPressureCorrelation>();
      private IDictionary<string, YawsEvaporationHeatCorrelation> evapHeatTableYawsOrganic = new Dictionary<string, YawsEvaporationHeatCorrelation>();
      private IDictionary<string, YawsLiquidDensityCorrelation> liquidDensityTableYawsOrganic = new Dictionary<string, YawsLiquidDensityCorrelation>();
      private IDictionary<string, YawsGasThermalConductivityCorrelation> gasKTableYawsOrganic = new Dictionary<string, YawsGasThermalConductivityCorrelation>();
      private IDictionary<string, YawsOrganicLiquidThermalConductivityCorrelation> liquidKTableYawsOrganic = new Dictionary<string, YawsOrganicLiquidThermalConductivityCorrelation>();
      private IDictionary<string, YawsGasViscosityCorrelation> gasViscTableYawsOrganic = new Dictionary<string, YawsGasViscosityCorrelation>();
      private IDictionary<string, YawsLiquidViscosityCorrelation> liquidViscTableYawsOrganic = new Dictionary<string, YawsLiquidViscosityCorrelation>();
      private IDictionary<string, YawsSurfaceTensionCorrelation> surfaceTensionTableYawsOrganic = new Dictionary<string, YawsSurfaceTensionCorrelation>();
      private IDictionary<string, YawsEnthalpyOfFormationCorrelation> enthalpyOfFormationTableYawsOrganic = new Dictionary<string, YawsEnthalpyOfFormationCorrelation>();

      //Perry's 7th edition
      private IDictionary<string, PerrysGasCpCorrelation> gasCpTablePerrys = new Dictionary<string, PerrysGasCpCorrelation>();
      private IDictionary<string, PerrysLiquidCpCorrelation> liquidCpTablePerrys = new Dictionary<string, PerrysLiquidCpCorrelation>();
      private IDictionary<string, PerrysVaporPressureCorrelation> vapPressureTablePerrys = new Dictionary<string, PerrysVaporPressureCorrelation>();
      private IDictionary<string, PerrysEvaporationHeatCorrelation> evapHeatTablePerrys = new Dictionary<string, PerrysEvaporationHeatCorrelation>();
      private IDictionary<string, PerrysLiquidDensityCorrelation> liquidDensityTablePerrys = new Dictionary<string, PerrysLiquidDensityCorrelation>();

      private ThermalPropCalculator() {
         //baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\SubstanceDatabase\\";
         //BinaryFormatter serializer = new BinaryFormatter();
         //SoapFormatter serializer = new SoapFormatter();
         //serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
         baseDirectory = SubstanceCatalog.GetSubstanceDirectory();
         IFormatter serializer = SubstanceCatalog.GetThermalPropSerializer();

         LoadYawsThermalPropCorrelations(serializer);
         LoadPerrysThermalPropCorrelations(serializer);
         LoadYawsOrganicThermalPropCorrelations(serializer);

         //InitializePerrysThermalPropCorrelations();
      }

      private void LoadYawsThermalPropCorrelations(IFormatter serializer) {

         IList thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsGasCpCorrelations.dat", serializer);
         foreach (YawsGasCpCorrelation correlation in thermalPropCorrelationList) {
            //Some substances are in both the organic and inorganic category
            if (!gasCpTableYaws.ContainsKey(correlation.SubstanceName)) {
               gasCpTableYaws.Add(correlation.SubstanceName, correlation);
            }
            //else {
            //   Console.Out.WriteLine(gasCpCorrelation.SubstanceName);
            //}
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsLiquidCpCorrelations.dat", serializer);
         foreach (YawsLiquidCpCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidCpTableYaws.ContainsKey(correlation.SubstanceName)) {
               liquidCpTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsSolidCpCorrelations.dat", serializer);
         foreach (YawsSolidCpCorrelation correlation in thermalPropCorrelationList) {
            if (!solidCpTableYaws.ContainsKey(correlation.SubstanceName)) {
               solidCpTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsEvaporationHeatCorrelations.dat", serializer);
         foreach (YawsEvaporationHeatCorrelation correlation in thermalPropCorrelationList) {
            if (!evapHeatTableYaws.ContainsKey(correlation.SubstanceName)) {
               evapHeatTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsVaporPressureCorrelations.dat", serializer);
         foreach (YawsVaporPressureCorrelation correlation in thermalPropCorrelationList) {
            if (!vapPressureTableYaws.ContainsKey(correlation.SubstanceName)) {
               vapPressureTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsLiquidDensityCorrelations.dat", serializer);
         foreach (YawsLiquidDensityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidDensityTableYaws.ContainsKey(correlation.SubstanceName)) {
               liquidDensityTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsGasViscosityCorrelations.dat", serializer);
         foreach (YawsGasViscosityCorrelation correlation in thermalPropCorrelationList) {
            if (!gasViscTableYaws.ContainsKey(correlation.SubstanceName)) {
               gasViscTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsLiquidViscosityCorrelations.dat", serializer);
         foreach (YawsLiquidViscosityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidViscTableYaws.ContainsKey(correlation.SubstanceName)) {
               liquidViscTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsGasThermalConductivityCorrelations.dat", serializer);
         foreach (YawsGasThermalConductivityCorrelation correlation in thermalPropCorrelationList) {
            if (!gasKTableYaws.ContainsKey(correlation.SubstanceName)) {
               gasKTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsLiquidThermalConductivityCorrelations.dat", serializer);
         foreach (YawsLiquidSolidThermalConductivityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidKTableYaws.ContainsKey(correlation.SubstanceName)) {
               liquidKTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsSurfaceTensionCorrelations.dat", serializer);
         foreach (YawsSurfaceTensionCorrelation correlation in thermalPropCorrelationList) {
            if (!surfaceTensionTableYaws.ContainsKey(correlation.SubstanceName)) {
               surfaceTensionTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsEnthalpyOfFormationCorrelations.dat", serializer);
         foreach (YawsEnthalpyOfFormationCorrelation correlation in thermalPropCorrelationList) {
            if (!enthalpyOfFormationTableYaws.ContainsKey(correlation.SubstanceName)) {
               enthalpyOfFormationTableYaws.Add(correlation.SubstanceName, correlation);
            }
         }
      }

      private void LoadPerrysThermalPropCorrelations(IFormatter serializer) {

         IList thermalPropCorrelationList = RecallThermalPropCorrelationList("PerrysGasCpCorrelations.dat", serializer);
         foreach (PerrysGasCpCorrelation correlation in thermalPropCorrelationList) {
            //Some substances are in both the organic and inorganic category
            if (!gasCpTablePerrys.ContainsKey(correlation.SubstanceName)) {
               gasCpTablePerrys.Add(correlation.SubstanceName, correlation);
            }
            else {
               Console.Out.WriteLine(correlation.SubstanceName);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("PerrysLiquidCpCorrelations.dat", serializer);
         foreach (PerrysLiquidCpCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidCpTablePerrys.ContainsKey(correlation.SubstanceName)) {
               liquidCpTablePerrys.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("PerrysEvaporationHeatCorrelations.dat", serializer);
         foreach (PerrysEvaporationHeatCorrelation correlation in thermalPropCorrelationList) {
            if (!evapHeatTablePerrys.ContainsKey(correlation.SubstanceName)) {
               evapHeatTablePerrys.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("PerrysVaporPressureCorrelations.dat", serializer);
         foreach (PerrysVaporPressureCorrelation correlation in thermalPropCorrelationList) {
            if (!vapPressureTablePerrys.ContainsKey(correlation.SubstanceName)) {
               vapPressureTablePerrys.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("PerrysLiquidDensityCorrelations.dat", serializer);
         foreach (PerrysLiquidDensityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidDensityTablePerrys.ContainsKey(correlation.SubstanceName)) {
               liquidDensityTablePerrys.Add(correlation.SubstanceName, correlation);
            }
         }
      }

      private void LoadYawsOrganicThermalPropCorrelations(IFormatter serializer) {

         IList thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicGasCpCorrelations.dat", serializer);
         foreach (YawsGasCpCorrelation correlation in thermalPropCorrelationList) {
            //Some substances are in both the organic and inorganic category
            if (!gasCpTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               gasCpTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
            //else {
            //   Console.Out.WriteLine(gasCpCorrelation.SubstanceName);
            //}
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicLiquidCpCorrelations.dat", serializer);
         foreach (YawsLiquidCpCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidCpTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               liquidCpTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicSolidCpCorrelations.dat", serializer);
         foreach (YawsSolidCpCorrelation correlation in thermalPropCorrelationList) {
            if (!solidCpTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               solidCpTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicEvaporationHeatCorrelations.dat", serializer);
         foreach (YawsEvaporationHeatCorrelation correlation in thermalPropCorrelationList) {
            if (!evapHeatTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               evapHeatTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicVaporPressureCorrelations.dat", serializer);
         foreach (YawsVaporPressureCorrelation correlation in thermalPropCorrelationList) {
            if (!vapPressureTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               vapPressureTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicLiquidDensityCorrelations.dat", serializer);
         foreach (YawsLiquidDensityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidDensityTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               liquidDensityTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicGasViscosityCorrelations.dat", serializer);
         foreach (YawsGasViscosityCorrelation correlation in thermalPropCorrelationList) {
            if (!gasViscTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               gasViscTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicLiquidViscosityCorrelations.dat", serializer);
         foreach (YawsLiquidViscosityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidViscTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               liquidViscTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicGasThermalConductivityCorrelations.dat", serializer);
         foreach (YawsGasThermalConductivityCorrelation correlation in thermalPropCorrelationList) {
            if (!gasKTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               gasKTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicLiquidThermalConductivityCorrelations.dat", serializer);
         foreach (YawsOrganicLiquidThermalConductivityCorrelation correlation in thermalPropCorrelationList) {
            if (!liquidKTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               liquidKTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicSurfaceTensionCorrelations.dat", serializer);
         foreach (YawsSurfaceTensionCorrelation correlation in thermalPropCorrelationList) {
            if (!surfaceTensionTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               surfaceTensionTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }

         thermalPropCorrelationList = RecallThermalPropCorrelationList("YawsOrganicEnthalpyOfFormationCorrelations.dat", serializer);
         foreach (YawsEnthalpyOfFormationCorrelation correlation in thermalPropCorrelationList) {
            if (!enthalpyOfFormationTableYawsOrganic.ContainsKey(correlation.SubstanceName)) {
               enthalpyOfFormationTableYawsOrganic.Add(correlation.SubstanceName, correlation);
            }
         }
      }

      //private IList RecallThermalPropCorrelationList(string fileName, SoapFormatter serializer) {
      private IList RecallThermalPropCorrelationList(string fileName, IFormatter serializer) {
         FileStream stream = null;
         string fullFileName = baseDirectory + fileName;
         IList thermalPropCorrelationList = new ArrayList();
         try {
            stream = new FileStream(fullFileName, FileMode.Open);
            thermalPropCorrelationList = (IList)serializer.Deserialize(stream);

            foreach (Storable s in thermalPropCorrelationList) {
               s.SetObjectData();
            }
         }
         catch (Exception) {
            //string message = e.ToString();
            //MessageBox.Show(message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            throw;
         }
         finally {
            if (stream != null) {
               stream.Close();
            }
         }
         return thermalPropCorrelationList;
      }

      public ICollection<string> GetYawsSolidCpSubstanceNameList() {
         ICollection<string> list = solidCpTableYaws.Keys;
         return list;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="t">Temperature in K</param>
      /// <param name="s">The substance to be calculated</param>
      /// <returns>Calculated heat capacity value (unit is J/kg.K)</returns>
      public double CalculateGasHeatCapacity(double t, Substance s) {
         double cp = 0;
         //if Perry's has the correlation, use Perry's. Otherwise use Yaws. If temperature is out of range
         //try Yaws Organic and see whichever has a wider temperature range.
         PerrysGasCpCorrelation correlationPerrys = GetPerrysGasCpCorrelation(s);
         if (correlationPerrys != null) {
            //Cp from Perry's correlation is in J/kmol.K. 
            cp = correlationPerrys.GetCp(t);
         }
         else if (gasCpTableYawsOrganic.ContainsKey(s.Name)) {
            YawsGasCpCorrelation correlationYawsOrganic = gasCpTableYawsOrganic[s.Name];
            YawsGasCpCorrelation theCorrelation = correlationYawsOrganic;

            if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) && 
               gasCpTableYaws.ContainsKey(s.Name)) {

               YawsGasCpCorrelation correlationYaws = gasCpTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) || 
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  theCorrelation = correlationYaws;
               }
            }

            //Cp from Yaw's correlation is in J/mol.K. needs to convert to J/kmol.K by multipling 1000
            cp = 1000 * theCorrelation.GetCp(t);
         }
         else if (gasCpTableYaws.ContainsKey(s.Name)) {
            cp = 1000 * gasCpTableYaws[s.Name].GetCp(t);
         }

         if (cp == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         cp = cp / s.MolarWeight;

         return cp;
      }

      //calculated value unit is J/kmol.K, t unit is K
      public double CalculateGasMeanHeatCapacity(double t1, double t2, Substance s) {
         double cp = Constants.NO_VALUE;
         PerrysGasCpCorrelation correlationPerrys = GetPerrysGasCpCorrelation(s);
         if (correlationPerrys != null) {
            cp = correlationPerrys.GetMeanCp(t1, t2);
         }
         else if (gasCpTableYawsOrganic.ContainsKey(s.Name)) {
            YawsGasCpCorrelation correlationYawsOrganic = gasCpTableYawsOrganic[s.Name];
            YawsGasCpCorrelation theCorrelation = correlationYawsOrganic;

            double deltTTotalYawsOrganic = (t1 - correlationYawsOrganic.MinTemperature) + (correlationYawsOrganic.MinTemperature - t2);
            if (deltTTotalYawsOrganic < 0 && gasCpTableYaws.ContainsKey(s.Name)) {
               YawsGasCpCorrelation correlationYaws = gasCpTableYaws[s.Name];
               double deltTTotalYaws = (t1 - correlationYaws.MinTemperature) + (correlationYaws.MinTemperature - t2);
               if (deltTTotalYaws > deltTTotalYawsOrganic) {
                  theCorrelation = correlationYaws;
               }
            }
            cp = 1000 * theCorrelation.GetMeanCp(t1, t2);
         }
         else if (gasCpTableYaws.ContainsKey(s.Name)) {
            cp = 1000 * gasCpTableYaws[s.Name].GetMeanCp(t1, t2);
         }

         if (cp == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }
         
         cp = cp / s.MolarWeight;

         return cp;
      }

      //calculated value unit is J/kmol.K, t unit is K
      public double CalculateGasMeanHeatCapacity(double p, double t1, double t2, Substance s) {
         double cp = 0;
         //if (s.Name.Equals("water")) {
         if (s.IsWater) {
            if (Math.Abs(t1 - t2) > 1.0e-4) {
               cp = CalculateWaterMeanHeatCapacity(p, t1, t2);
            }
            else {
               cp = CalculateWaterMeanHeatCapacity(p, t1, t1 + 1);
            }
         }
         else {
            cp = CalculateWaterMeanHeatCapacity(p, t1, t2);
         }

         return cp;
      }

      //calculated value unit is J/kg.K, t unit is K
      public double CalculateLiquidHeatCapacity(double t, Substance s) {
         double cp = Constants.NO_VALUE;
         PerrysLiquidCpCorrelation correlationPerrys = GetPerrysLiquidCpCorrelation(s);
         if (correlationPerrys != null) {
            //return cp from GetCp(t) is J/kmol.K
            cp = correlationPerrys.GetCp(t);
         }
         else if (liquidCpTableYawsOrganic.ContainsKey(s.Name)) {
            YawsLiquidCpCorrelation correlationYawsOrganic = liquidCpTableYawsOrganic[s.Name];
            YawsLiquidCpCorrelation theCorrelation = correlationYawsOrganic;

            if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) &&
               liquidCpTableYaws.ContainsKey(s.Name)) {

               YawsLiquidCpCorrelation correlationYaws = liquidCpTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  theCorrelation = correlationYaws;
               }
            }
            //return cp from GetCp(t) is J/mol.K. So needs to multiple 1000 to convert J/kmol.K
            cp = 1000 * theCorrelation.GetCp(t);
         }
         else if (liquidCpTableYaws.ContainsKey(s.Name)) {
            //return cp from GetCp(t) is J/mol.K. So needs to multiple 1000 to convert J/kmol.K
            cp = 1000 * liquidCpTableYaws[s.Name].GetCp(t);
         }

         if (cp == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }
  
         cp = cp / s.MolarWeight;
         return cp;
      }

      //calculated value unit is J/kmol.K, t unit is K
      public double CalculateLiquidMeanHeatCapacity(double t1, double t2, Substance s) {
         double cp = Constants.NO_VALUE;
         PerrysLiquidCpCorrelation correlationPerrys = GetPerrysLiquidCpCorrelation(s);
         if (correlationPerrys != null) {
            cp = correlationPerrys.GetMeanCp(t1, t2);
         }
         else if (liquidCpTableYawsOrganic.ContainsKey(s.Name)) {
            YawsLiquidCpCorrelation correlationYawsOrganic = liquidCpTableYawsOrganic[s.Name];
            YawsLiquidCpCorrelation theCorrelation = correlationYawsOrganic;

            double deltTTotalYawsOrganic = (t1 - correlationYawsOrganic.MinTemperature) + (correlationYawsOrganic.MinTemperature - t2);
            if (deltTTotalYawsOrganic < 0 && liquidCpTableYaws.ContainsKey(s.Name)) {
               YawsGasCpCorrelation correlationYaws = gasCpTableYaws[s.Name];
               double deltTTotalYaws = (t1 - correlationYaws.MinTemperature) + (correlationYaws.MinTemperature - t2);
               if (deltTTotalYaws > deltTTotalYawsOrganic) {
                  theCorrelation = liquidCpTableYaws[s.Name];
               }
            }

            cp = 1000 * theCorrelation.GetMeanCp(t1, t2);
         }
         else if (liquidCpTableYawsOrganic.ContainsKey(s.Name)) {
            cp = 1000 * liquidCpTableYaws[s.Name].GetMeanCp(t1, t2);
         }

         if (cp == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         cp = cp / s.MolarWeight;

         return cp;
      }

      //Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
      //calculated value unit is J/kmol.K, t unit is K
      public double CalculateLiquidMeanHeatCapacity(double p, double t1, double t2, Substance s) {
         double cp = 0;
         //if (s.Name.Equals("water")) {
         if (s.IsWater) {
            if (Math.Abs(t1 - t2) > 1.0e-4) {
               cp = CalculateWaterMeanHeatCapacity(p, t1, t2);
            }
            else {
               cp = CalculateWaterMeanHeatCapacity(p, t1, t1 + 1);
            }
         }
         else {
            cp = CalculateLiquidMeanHeatCapacity(t1, t2, s);
         }

         return cp;
      }

      //calculated value unit is J/kg, t unit is K
      public double CalculateEvaporationHeat(double t, Substance s) {
         //formulation coming from Perry's Chemical Engineer's Handbook
         double r = Constants.NO_VALUE;
         PerrysEvaporationHeatCorrelation correlationPerrys = GetPerrysEvaporationHeatCorrelation(s);
         if (correlationPerrys != null) {
            //double tc = s.CriticalPropsAndAccentricFactor.CriticalTemperature;
            //returned r from GetEvaporationHeat(t, tc) is in J/kmol
            r = correlationPerrys.GetEvaporationHeat(t);
         }
         //else if (evapHeatTableYaws.ContainsKey(s.Name)) {
         //   //returned r from GetEvaporationHeat(t) is in J/mol
         //   r = 1000 * evapHeatTableYaws[s.Name].GetEvaporationHeat(t);
         //}
         else if (evapHeatTableYawsOrganic.ContainsKey(s.Name)) {
            YawsEvaporationHeatCorrelation correlationYawsOrganic = evapHeatTableYawsOrganic[s.Name];
            YawsEvaporationHeatCorrelation theCorrelation = correlationYawsOrganic;

            if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) && 
               evapHeatTableYaws.ContainsKey(s.Name)) {
               YawsEvaporationHeatCorrelation correlationYaws = evapHeatTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                  (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  theCorrelation = correlationYaws;
               }
            }
            //returned r from GetEvaporationHeat(t) is in J/mol
            r = 1000 * theCorrelation.GetEvaporationHeat(t);
         }
         else if (evapHeatTableYaws.ContainsKey(s.Name)) {
            r = 1000 * evapHeatTableYaws[s.Name].GetEvaporationHeat(t);
         }

         if (r == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         r = r / s.MolarWeight;

         return r;
      }

      //calculated value unit is Pa, t unit is K
      public double CalculateSaturationPressure(double t, Substance s) {
         double p = Constants.NO_VALUE;
         //if (s.Name.Equals("water")) {
         if (s.IsWater) {
            p = CalculateWaterSaturationPressure(t);
         }
         else {
            PerrysVaporPressureCorrelation correlationPerrys = GetPerrysVaporPressureCorrelation(s);
            if (correlationPerrys != null) {
               p = correlationPerrys.GetSaturationPressure(t);
            }
            else if (vapPressureTableYawsOrganic.ContainsKey(s.Name)) {
               YawsVaporPressureCorrelation correlationYawsOrganic = vapPressureTableYawsOrganic[s.Name];
               YawsVaporPressureCorrelation theCorrelation = correlationYawsOrganic;

               if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) &&
                  vapPressureTableYaws.ContainsKey(s.Name)) {
                  YawsVaporPressureCorrelation correlationYaws = vapPressureTableYaws[s.Name];
                  if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                      (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                     theCorrelation = correlationYaws;
                  }
               }

               p = theCorrelation.GetSaturationPressure(t);
            }
            else if (vapPressureTableYaws.ContainsKey(s.Name)) {
               p = vapPressureTableYaws[s.Name].GetSaturationPressure(t);
            }
         }

         if (p == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return p;
      }

      //calculated value unit is Pa, t unit is K
      public double CalculateSaturationTemperature(double p, Substance s) {
         double t = Constants.NO_VALUE;
         //if (s.Name.Equals("water")) {
         if (s.IsWater) {
            t = CalculateWaterSaturationTemperature(p);
         }
         else {
            PerrysVaporPressureCorrelation correlationPerrys = GetPerrysVaporPressureCorrelation(s);
            if (correlationPerrys != null) {
               t = correlationPerrys.GetSaturationTemperature(p);
            }
            else if (vapPressureTableYawsOrganic.ContainsKey(s.Name)) {
               YawsVaporPressureCorrelation correlationYawsOrganic = vapPressureTableYawsOrganic[s.Name];
               YawsVaporPressureCorrelation theCorrelation = correlationYawsOrganic;

               //if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) &&
               //   vapPressureTableYaws.ContainsKey(s.Name)) {
               //   YawsVaporPressureCorrelation correlationYaws = vapPressureTableYaws[s.Name];
               //   if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
               //       (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
               //      theCorrelation = correlationYaws;
               //   }
               //}

               t = theCorrelation.GetSaturationTemperature(p);
            }
            else if (vapPressureTableYaws.ContainsKey(s.Name)) {
               t = vapPressureTableYaws[s.Name].GetSaturationTemperature(p);
            }
         }

         if (t == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return t;
      }

      //calculated value unit is kmol/m3, t unit is K
      public double CalculateLiquidDensity(double t, Substance s) {
         double density = Constants.NO_VALUE;
         PerrysLiquidDensityCorrelation correlationPerrys = GetPerrysLiquidDensityCorrelation(s);
         if (correlationPerrys != null) {
            density =  s.MolarWeight * correlationPerrys.GetDensity(t);
            //density = density * s.MolarWeight;
         }
         else if (liquidDensityTableYawsOrganic.ContainsKey(s.Name)) {
            YawsLiquidDensityCorrelation correlationYawsOrganic = liquidDensityTableYawsOrganic[s.Name];
            YawsLiquidDensityCorrelation theCorrelation = correlationYawsOrganic;

            if ((t < correlationYawsOrganic.MinTemperature || t > correlationYawsOrganic.MaxTemperature) &&
               liquidDensityTableYaws.ContainsKey(s.Name)) {
               YawsLiquidDensityCorrelation correlationYaws = liquidDensityTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  theCorrelation = correlationYaws;
               }
            }

            density = theCorrelation.GetDensity(t);

         }
         else if (liquidDensityTableYaws.ContainsKey(s.Name)) {
            density = liquidDensityTableYaws[s.Name].GetDensity(t);
         }

         if (density == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return density;
      }

      //formula comes from a web site
      //calculated value unit is kg/m3, t unit is K
      public double CalculateGasDensity(double t, double p, Substance s) {
         //return 360.77819 * Math.Pow(t, -1.00336);
         //Air density at i atm
         //double density = 351.99 / t + 344.84 / (t * t);
         double gasMolarMass = s.MolarWeight;
         double rho = p / 1.013e5 * gasMolarMass / (0.082 * t);
         return rho;
      }


      public double CalculateLiquidDensity(double p, double t, Substance s) {
         double density = 0;
         //if (s.Name.Equals("water")) {
         if (s.IsWater) {
            density = CalculateWaterDensity(p, t);
         }
         else {
            double satTemperature = CalculateSaturationTemperature(p, s);
            if (t < satTemperature) {
               density = CalculateLiquidDensity(t, s);
            }
            else if (t > satTemperature) {
               //density = ThermalPropCalculator.CalculateLiquidDensity(temperature);
               //need to use state equation to calculate state
            }
         }

         return density;
      }

      //calculated value unit is w/m.K, t unit is K
      public double CalculateGasThermalConductivity(double t, Substance s) {
         double k = Constants.NO_VALUE;
         //if (s.Name == "Air") {
         if (s.IsAir) {
            k = CalculateAirGasThermalConductivity(t);
         }
         else {
            YawsGasThermalConductivityCorrelation theCorrelation = null;
            if (gasKTableYawsOrganic.ContainsKey(s.Name)) {
               YawsGasThermalConductivityCorrelation correlationYawsOrganic = gasKTableYawsOrganic[s.Name];
               theCorrelation = correlationYawsOrganic;

               if (gasKTableYaws.ContainsKey(s.Name)) {
                  YawsGasThermalConductivityCorrelation correlationYaws = gasKTableYaws[s.Name];
                  if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                      (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                     theCorrelation = correlationYaws;
                  }
               }
            }
            else if (gasKTableYaws.ContainsKey(s.Name)) {
               theCorrelation = gasKTableYaws[s.Name];
            }

            if (theCorrelation == null) {
               throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
            }

            k = theCorrelation.GetThermalConductivity(t);
         }

         return k;
      }

      //calculated value unit is w/m.K, t unit is K
      public double CalculateLiquidThermalConductivity(double t, Substance s) {
         double k = Constants.NO_VALUE;

         if (liquidKTableYawsOrganic.ContainsKey(s.Name)) {
            YawsOrganicLiquidThermalConductivityCorrelation correlationYawsOrganic = liquidKTableYawsOrganic[s.Name];
            k = correlationYawsOrganic.GetThermalConductivity(t);

            if (liquidKTableYaws.ContainsKey(s.Name)) {
               YawsLiquidSolidThermalConductivityCorrelation correlationYaws = liquidKTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  k = correlationYaws.GetThermalConductivity(t, s.SubstanceType);
               }
            }

         }
         else if (liquidKTableYaws.ContainsKey(s.Name)) {
            k = gasKTableYaws[s.Name].GetThermalConductivity(t);
         }

         if (k == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return k;
      }

      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public double CalculateGasViscosity(double t, Substance s) {
         double visc = Constants.NO_VALUE;
         //if (s.Name == "Air") {
         if (s.IsAir) {
            visc = CalculateAirGasViscosity(t);
         }
         else if (gasViscTableYawsOrganic.ContainsKey(s.Name)) {
            YawsGasViscosityCorrelation correlationYawsOrganic = gasViscTableYawsOrganic[s.Name];

            visc = correlationYawsOrganic.GetViscosity(t);

            if (gasViscTableYaws.ContainsKey(s.Name)) {
               YawsGasViscosityCorrelation correlationYaws = gasViscTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  visc = correlationYaws.GetViscosity(t); ;
               }
            }
         }
         else if (gasViscTableYaws.ContainsKey(s.Name)) {
            visc = gasViscTableYaws[s.Name].GetViscosity(t);
         }

         if (visc == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return visc;
      }

      //calculated value unit is Poiseuille == Pascal.Second, t unit is K
      public double CalculateLiquidViscosity(double t, Substance s) {
         double visc = Constants.NO_VALUE;
         if (liquidViscTableYawsOrganic.ContainsKey(s.Name)) {
            YawsLiquidViscosityCorrelation correlationYawsOrganic = liquidViscTableYawsOrganic[s.Name];

            visc = correlationYawsOrganic.GetViscosity(t);

            if (liquidViscTableYaws.ContainsKey(s.Name)) {
               YawsLiquidViscosityCorrelation correlationYaws = liquidViscTableYaws[s.Name];
               if ((t < correlationYawsOrganic.MinTemperature && correlationYaws.MinTemperature < correlationYawsOrganic.MinTemperature) ||
                   (t > correlationYawsOrganic.MaxTemperature && correlationYaws.MaxTemperature > correlationYawsOrganic.MaxTemperature)) {
                  visc = correlationYaws.GetViscosity(t); ;
               }
            }
         }
         else if (liquidViscTableYaws.ContainsKey(s.Name)) {
            visc = liquidViscTableYaws[s.Name].GetViscosity(t);
         }

         if (visc == Constants.NO_VALUE) {
            throw new IllegalVarValueException(string.Format("Substance {0} can not be found in the substance database.", s.Name));
         }

         return visc;
      }

      //calculated value unit is J/kg, t unit is K
      public double CalculateEnthalpyOfFormation(double t, Substance s) {
         double enthalpy = 0;
         if (enthalpyOfFormationTableYawsOrganic.ContainsKey(s.Name)) {
            //returned enthalpy from GetEnthalpyOfFormation(t) is in kJ/mol
            //need to multiply 1.0e6 to convert to J/kmol
            enthalpy = 1.0e6 * enthalpyOfFormationTableYawsOrganic[s.Name].GetEnthalpyOfFormation(t);
         }
         else if (enthalpyOfFormationTableYaws.ContainsKey(s.Name)) {
            //returned enthalpy from GetEnthalpyOfFormation(t) is in kJ/mol
            //need to multiply 1.0e6 to convert to J/kmol
            enthalpy = 1.0e6 * enthalpyOfFormationTableYaws[s.Name].GetEnthalpyOfFormation(t);
         }
         return enthalpy / s.MolarWeight;
      }

      ////t unit is K
      //public double CalculateWaterSaturationPressureBelowFreezingPoint(double t) {
      //   t = t - 273.15;
      //   double p = 100.0 * 6.1121 * Math.Exp(17.502 * t / (240.97 + t));
      //   return p;
      //}

      private double CalculateWaterSaturationTemperature(double pressure) {
         double temperature = 1.0e-6;
         if (pressure < 1.0e-6) {
            pressure = 1.0e-6;
         }

         if (pressure <= 611.221) { //temperature below the steam table can deal with
            //temperature = 193.03 + 28.633 * Math.Pow(pressure, 0.1609);
            double a = Math.Log(pressure / 611.21);
            temperature = a * 240.97 / (17.502 - a) + 273.15;
         }
         else if (pressure > 611.221) {
            SteamTable steamTable = SteamTable.GetInstance();
            temperature = steamTable.GetSaturationTemperature(pressure);
         }
         //use correlation from Perry's to finalize
         //else {
         //   //use simple correlation to do a rough estimation
         //   temperature = 3816.44/(23.197 - Math.Log(pressure)) + 46.13;
         //   double old_temp;
         //   double fx;
         //   double dfx;
         //   int i = 0;
         //   do {
         //      i++;
         //      old_temp = temperature;
         //      //direct iterative method
         //      //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
         //      //   - 4.1653e-6 * old_temp * old_temp);
         //      //Newton iterative method--much better convergence speed
         //      fx = 73.649 - 7258.2/old_temp - 7.3037 * Math.Log(old_temp) + 4.1653e-6 * old_temp * old_temp - Math.Log(pressure); 
         //      dfx = 7258.2/(old_temp * old_temp) - 7.3037/old_temp + 2.0 * 4.1653e-6 * old_temp;
         //      temperature = old_temp - fx/dfx;
         //   } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6); 
         //}

         return temperature;
      }

      private double CalculateWaterSaturationPressure(double temperature) {
         double pSaturation = 1.0;
         //if (temperature >= 253.15 && temperature <= 283.15) {
         //   pSaturation = 8.8365E-10 * Math.Pow((temperature -193.03), 6.2145);
         //}
         //else if (temperature <= 453.15) {
         //   pSaturation = Math.Exp(23.197 - 3816.44/(temperature - 46.13));
         //}
         if (temperature <= 273.15946599928748) { //temperature belown which steam table cannot deal
            double t = temperature - 273.15;
            pSaturation = 611.21 * Math.Exp(17.502 * t / (240.97 + t));
            //To Do: When system is not air-water, need to change!!!
            //pSaturation = ThermalPropCalculator.CalculateWaterSaturationPressureBelowFreezingPoint(temperature);
         }
         else {
            //formulation coming from Perry's Chemical Engineer's Handbook
            //pSaturation = Math.Exp(73.649 - 7258.2/temperature - 7.3037 * Math.Log(temperature) 
            //   + 4.1653e-6 * temperature * temperature);
            //pSaturation = ThermalPropCalculator.CalculateVaporPressure(temperature, vapPressureCoeffs); 
            SteamTable steamTable = SteamTable.GetInstance();
            pSaturation = steamTable.GetSaturationPressure(temperature);
         }

         return pSaturation;
      }

      //Steam Table
      //calculated value unit is kmol/m3, t unit is K
      private double CalculateWaterDensity(double p, double t) {
         SteamTable steamTable = SteamTable.GetInstance();
         double density = steamTable.GetDensityFromPressureAndTemperature(p, t);
         return density;
      }

      //Steam Table
      //calculated value unit is J/kg.K, t unit is K
      private double CalculateWaterMeanHeatCapacity(double p, double t1, double t2) {
         SteamTable steamTable = SteamTable.GetInstance();
         double h1 = steamTable.GetEnthalpyFromPressureAndTemperature(p, t1);
         double h2 = steamTable.GetEnthalpyFromPressureAndTemperature(p, t2);
         double cp = (h2 - h1) / (t2 - t1);
         return cp;
      }

      //Steam Table
      //calculated value unit is J/kg.K, t unit is K
      public double CalculateEnthalpyFromPressureAndTemperature(double p, double t, Substance s) {
         double h = 0;
         if (s.IsWater) {
            SteamTable steamTable = SteamTable.GetInstance();
            h = steamTable.GetEnthalpyFromPressureAndTemperature(p, t);
         }
         else {
            double boilingPoint = CalculateSaturationTemperature(p, s);
            if (t < boilingPoint) {
               h = (t - 273.15) * CalculateLiquidMeanHeatCapacity(273.15, t, s);
            }
            else if (t > boilingPoint) {
               h = CalculateLiquidMeanHeatCapacity(273.15, t, s) * (t - 273.15) +
                   CalculateEvaporationHeat(boilingPoint, s) +
                   CalculateGasMeanHeatCapacity(boilingPoint, t, s) * (t - boilingPoint);
            }
         }
         return h;
      }

      public double CalculateEnthalpyFromPressureAndVaporFraction(double p, double vf, Substance s) {
         double h = 0;
         if (s.IsWater) {
            SteamTable steamTable = SteamTable.GetInstance();
            h = steamTable.GetPropertiesFromPressureAndVaporFraction(p, vf).enthalpy;
         }
         else {
            double boilingPoint = CalculateSaturationTemperature(p, s);
            if (Math.Abs(vf) < TOLERANCE) {
               h = CalculateEnthalpyFromPressureAndTemperature(p, (boilingPoint - TOLERANCE), s);
            }
            else if (Math.Abs(vf - 1.0) < TOLERANCE) {
               h = CalculateEnthalpyFromPressureAndTemperature(p, (boilingPoint + TOLERANCE), s);
            }
            else {
               h = CalculateEnthalpyFromPressureAndTemperature(p, (boilingPoint - TOLERANCE), s) +
                  vf * CalculateEvaporationHeat(boilingPoint, s);
            }
         }
         return h;
      }

      public double CalculateTemperatureFromPressureAndEnthalpy(double p, double h, Substance s) {
         double t = 0;
         if (s.IsWater) {
            SteamTable steamTable = SteamTable.GetInstance();
            t = steamTable.GetPropertiesFromPressureAndEnthalpy(p, h).temperature;
         }
         else {
            int counter = 0;
            double cp;
            double tOld;
            double boilingPoint = CalculateSaturationTemperature(p, s);
            double hVF0 = (boilingPoint - 273.15) * CalculateLiquidMeanHeatCapacity(273.15, boilingPoint, s);
            double hVF1 = hVF0 + CalculateEvaporationHeat(boilingPoint, s);
            if (h < hVF0) {
               cp = CalculateLiquidHeatCapacity(boilingPoint, s);
               t = boilingPoint - (hVF0 - h) / cp;
               do {
                  counter++;
                  tOld = t;
                  cp = CalculateLiquidMeanHeatCapacity(t, boilingPoint, s);
                  t = boilingPoint - (hVF0 - h) / cp;
               } while (Math.Abs(t - tOld) > TOLERANCE && counter < 200);
               
               if (counter == 200) {
                  string msg = "Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream";
                  throw new CalculationFailedException(msg);
               }
            }
            else if (h >= hVF0 && h <= hVF1) {
               t = boilingPoint; 
            }
            else if (h > hVF1) {
               cp = CalculateGasHeatCapacity(boilingPoint, s);
               t = (h - hVF1) / cp + boilingPoint;
               do {
                  counter++;
                  tOld = t;
                  cp = CalculateGasMeanHeatCapacity(boilingPoint, t, s);
                  t = (h - hVF1) / cp + boilingPoint;
               } while (Math.Abs(t - tOld) > TOLERANCE && counter < 200);

               if (counter == 200) {
                  string msg = "Calculation of temperature from enthalpy failed.\nPlease make sure each specified value in this stream";
                  throw new CalculationFailedException(msg);
               }
            }
         }
         return t;
      }

      //formula comes from a web site
      //calculated value unit is kg/m3, t unit is K
      private double CalculateAirGasDensity(double t) {
         //return 360.77819 * Math.Pow(t, -1.00336);
         return 351.99/t + 344.84/(t * t);
      }

      //calculated value unit is w/m.K, t unit is K
      private double CalculateAirGasThermalConductivity(double t) {
         //return -3.9333e-4 + 1.0184e-4 * t - 4.8574e-8 * t * t + 1.5207e-11 * t * t * t;
         return 2.334e-3 * Math.Pow(t, 3.0/2.0)/(164.54 + t);
      }

      //calculated value unit is w/m.K, t unit is K
      private double CalculateAirGasViscosity(double t) {
         //double kinematicVisc = -3.4484e-6 + 3.7604e-8 * t + 9.5728e-11 * t * t - 1.1555e-14 * t * t * t;
         //double density = CalculateAirGasDensity(t);
         //return kinematicVisc * density;
         return 1.4592e-6 * Math.Pow(t, 3.0 / 2.0) / (109.1 + t);

      }

      private PerrysGasCpCorrelation GetPerrysGasCpCorrelation(Substance s) {
         PerrysGasCpCorrelation aCorrelation = null;
         if (gasCpTablePerrys.ContainsKey(s.Name)) {
            aCorrelation = gasCpTablePerrys[s.Name];
         }
         else {
            ICollection<PerrysGasCpCorrelation> correlations = gasCpTablePerrys.Values;
            foreach (PerrysGasCpCorrelation correlation in correlations) {
               if (s.CASRegistryNo == correlation.CASRegistryNo) {
                  aCorrelation = correlation;
                  break;
               }
            }
         }

         return aCorrelation;
      }

      private PerrysLiquidCpCorrelation GetPerrysLiquidCpCorrelation(Substance s) {
         PerrysLiquidCpCorrelation aCorrelation = null;
         if (liquidCpTablePerrys.ContainsKey(s.Name)) {
            aCorrelation = liquidCpTablePerrys[s.Name];
         }
         else {
            ICollection<PerrysLiquidCpCorrelation> correlations = liquidCpTablePerrys.Values;
            foreach (PerrysLiquidCpCorrelation correlation in correlations) {
               if (s.CASRegistryNo == correlation.CASRegistryNo) {
                  aCorrelation = correlation;
                  break;
               }
            }
         }

         return aCorrelation;
      }

      private PerrysEvaporationHeatCorrelation GetPerrysEvaporationHeatCorrelation(Substance s) {
         PerrysEvaporationHeatCorrelation aCorrelation = null;
         if (evapHeatTablePerrys.ContainsKey(s.Name)) {
            aCorrelation = evapHeatTablePerrys[s.Name];
         }
         else {
            ICollection<PerrysEvaporationHeatCorrelation> correlations = evapHeatTablePerrys.Values;
            foreach (PerrysEvaporationHeatCorrelation correlation in correlations) {
               if (s.CASRegistryNo == correlation.CASRegistryNo) {
                  aCorrelation = correlation;
                  break;
               }
            }
         }

         return aCorrelation;
      }

      private PerrysVaporPressureCorrelation GetPerrysVaporPressureCorrelation(Substance s) {
         PerrysVaporPressureCorrelation aCorrelation = null;
         if (vapPressureTablePerrys.ContainsKey(s.Name)) {
            aCorrelation = vapPressureTablePerrys[s.Name];
         }
         else {
            ICollection<PerrysVaporPressureCorrelation> correlations = vapPressureTablePerrys.Values;
            foreach (PerrysVaporPressureCorrelation correlation in correlations) {
               if (s.CASRegistryNo == correlation.CASRegistryNo) {
                  aCorrelation = correlation;
                  break;
               }
            }
         }

         return aCorrelation;
      }

      private PerrysLiquidDensityCorrelation GetPerrysLiquidDensityCorrelation(Substance s) {
         PerrysLiquidDensityCorrelation aCorrelation = null;
         if (liquidDensityTablePerrys.ContainsKey(s.Name)) {
            aCorrelation = liquidDensityTablePerrys[s.Name];
         }
         else {
            ICollection<PerrysLiquidDensityCorrelation> correlations = liquidDensityTablePerrys.Values;
            foreach (PerrysLiquidDensityCorrelation correlation in correlations) {
               if (s.CASRegistryNo == correlation.CASRegistryNo) {
                  aCorrelation = correlation;
                  break;
               }
            }
         }

         return aCorrelation;
      }
   }
}

////Perry's
////Calculated value unit is kmol/m3, t unit is K
//private double CalculateWaterLiquidDensity(double t) {
//   double[] c = new double[4] { 5.459, 0.30542, 647.13, 0.081 };
//   if (t > 333.15 && t <= 403.15) {
//      c[0] = 4.9669;
//      c[1] = 2.7788e-1;
//      c[2] = 6.4713e2;
//      c[3] = 1.8740e-1;
//   }
//   else if (t > 403.15 && t <= 6471.3) {
//      c[0] = 4.391;
//      c[1] = 2.487e-1;
//      c[2] = 6.4713e2;
//      c[3] = 2.5340e-1;
//   }

//   //return CalculateLiquidDensity(t, c);
//   double tempValue = 1.0 + Math.Pow((1.0 - t / c[2]), c[3]);
//   return c[0] / Math.Pow(c[1], tempValue);
//}////Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
////calculated value unit is J/kmol.K, t unit is K
//public static double CalculateLiquidHeatCapacity1(double t, double[] c) {
//   return c[0] + c[1] * t + c[2] * t * t + c[3] * Math.Pow(t, 3.0) + c[4] * Math.Pow(t, 4.0);
//}

////Perry's Liquid Heat Capacity default formula--Equation 1. Water is in this group
////calculated value unit is J/kmol.K, t unit is K
//public static double CalculateMeanLiquidHeatCapacity1(double t1, double t2, double[] c) {
//   double h1 = c[0] * t1 + c[1] * t1 * t1 / 2.0 + c[2] * Math.Pow(t1, 3.0) / 3.0 + c[3] * Math.Pow(t1, 4.0) / 4.0 + c[4] * Math.Pow(t1, 5.0) / 5.0;
//   double h2 = c[0] * t2 + c[1] * t2 * t2 / 2.0 + c[2] * Math.Pow(t2, 3.0) / 3.0 + c[3] * Math.Pow(t2, 4.0) / 4.0 + c[4] * Math.Pow(t2, 5.0) / 5.0;
//   double meanCp = (h2 - h1) / (t2 - t1);
//   return meanCp;
//}

////Perry's Liquid Heat Capacity equation 2
////calculated value is J/kmol.K, t unit is K
//public static double CalculateLiquidHeatCapacity2(double t, double tc, double[] c) {
//   t = 1.0 - t / tc;
//   return c[0] * c[0] / t + c[1] - 2.0 * c[0] * c[2] * t - c[0] * c[3] * t * t - Math.Pow(c[2], 2.0 / 3.0) * Math.Pow(t, 3.0) - c[2] * c[3] / 2.0 * Math.Pow(t, 4.0) - Math.Pow(c[3], 2.0 / 5.0) * Math.Pow(t, 5.0);
//}

////Perry's Gas Heat Capacity default formula--Equation 1
////calculated value unit is J/kmol.K, t unit is K
//public static double CalculateGasHeatCapacity1(double t, double[] c) {
//   double term2 = c[2] / t;
//   term2 = term2 / Math.Sinh(term2);
//   term2 = term2 * term2;
//   double term3 = c[4] / t;
//   term3 = term3 / Math.Cosh(term3);
//   term3 = term3 * term3;
//   return c[0] + c[1] * term2 + c[3] * term3;
//}

////Perry's Gas Heat Capacity default formula--Equation 1
////calculated value unit is J/kmol.K, t unit is K
//public static double CalculateMeanGasHeatCapacity1(double t1, double t2, double[] c) {
//   double h1 = c[0] * t1 + c[1] * c[2] / Math.Tanh(c[2] / t1) - c[3] * c[4] * Math.Tanh(c[4] / t1);
//   double h2 = c[0] * t2 + c[1] * c[2] / Math.Tanh(c[2] / t2) - c[3] * c[4] * Math.Tanh(c[4] / t2);
//   double meanCp = (h2 - h1) / (t2 - t1);
//   return meanCp;
//}

////Perry's Gas Heat Capacity--Equation 2
////calculated value unit is J/kmol.K, t unit is K
//public static double CalculateGasHeatCapacity2(double t, double[] c) {
//   return c[1] + c[1] * t + c[2] * t * t + c[3] * Math.Pow(t, 3.0) + c[4] * Math.Pow(t, 5.0);
//}

////Perry's Gas Heat Capacity--Equation 2
////calculated value is J/kmol.K, t unit is K
//public static double CalculateGasHeatCapacity3(double t, double[] c) {
//   return c[0] + c[1] * Math.Log(t) + c[2] / t - c[3] * t;
//}

////Perry's
////calculated value unit is J/kmol, t unit is K
//public static double CalculateEvaporationHeat(double t, double tc, double[] c) {
//   //formulation coming from Perry's Chemical Engineer's Handbook
//   double tr = t / tc;
//   double tempValue = c[1] + c[2] * tr + c[3] * tr * tr;
//   double r = c[0] * Math.Pow((1 - tr), tempValue);
//   return r;
//}

////Perry's
////calculated value unit is Pa, t unit is K
//public static double CalculateSaturationPressure(double t, double[] c) {
//   //formulation coming from Perry's Chemical Engineer's Handbook
//   double p = Math.Exp(c[0] + c[1] / t + c[2] * Math.Log(t) + c[3] * Math.Pow(t, c[4]));
//   return p;
//}

////Perry's
////calculated value unit is Pa, t unit is K
//public static double CalculateSaturationTemperature(double p, double[] c) {
//   //formulation coming from Perry's Chemical Engineer's Handbook
//   //double p = Math.Exp(c[0] + c[1]/t + c[2] * Math.Log(t) + c[3] * Math.Pow(t, c[4]));
//   double temperature = 298.15;
//   double old_temp;
//   double fx;
//   double dfx;
//   int i = 0;
//   do {
//      i++;
//      old_temp = temperature;
//      //direct iterative method
//      //temperature = -7258.2/(Math.Log(pressure) - 73.649 + 7.3037 * Math.Log(old_temp) 
//      //   - 4.1653e-6 * old_temp * old_temp);
//      //Newton iterative method--much better convergence speed
//      fx = c[0] + c[1] / old_temp + c[2] * Math.Log(old_temp) + c[3] * Math.Pow(old_temp, c[4]) - Math.Log(p);
//      dfx = -c[1] / (old_temp * old_temp) - c[2] / old_temp + c[3] * c[4] * Math.Pow(old_temp, c[4] - 1);
//      temperature = old_temp - fx / dfx;
//   } while (i < 500 && Math.Abs(temperature - old_temp) > 1.0e-6);

//   return temperature;
//}


////Yaws' Chemical Properties
////calculated value unit is w/m.K, t unit is K
//public static double CalculateLiquidOrganicThermalConductivity(double t, double[] c) {
//   double logKLiq = c[0] + c[1] * Math.Pow((1.0 - t / c[2]), 2.0 / 7.0);
//   return Math.Pow(10, logKLiq);
//}

////Yaws' Chemical Properties
////calculated value unit is w/m.K, t unit is K
//public static double CalculateLiquidInorganicThermalConductivity(double t, double[] c) {
//   double k = c[0] + c[1] * t + c[2] * t * t;
//   return k;
//}

////Yaws' Chemical Properties
////calculated value unit is w/m.K, t unit is K
//public static double CalculateGasThermalConductivity(double t, double[] c) {
//   double k = c[0] + c[1] * t + c[2] * t * t;
//   return k;
//}

////Yaws' Chemical Properties
////calculated value unit is Poiseuille == Pascal.Second, t unit is K
//public static double CalculateLiquidViscosity(double t, double[] c) {
//   double logViscLiq = c[0] + c[1] / t + c[2] * t + c[3] * t * t;//viscosity unit of this formula is cnetipoise
//   return 0.001 * Math.Pow(10, logViscLiq);
//}

////Yaws' Chemical Properties
////calculated value unit is Poiseuille == Pascal.Second, t unit is K
//public static double CalculateGasViscosity(double t, double[] c) {
//   double visc = c[0] + c[1] * t + c[2] * t * t;//viscosity unit of this formula is micropoise
//   return 1.0e-6 * visc;
//}

////Perry's
////calculated value unit is kmol/m3, t unit is K
//public static double CalculateLiquidDensity(double t, double[] c) {
//   double tempValue = 1.0 + Math.Pow((1.0 - t / c[2]), c[3]);
//   return c[0] / Math.Pow(c[1], tempValue);
//}

//private void InitializePerrysThermalPropCorrelations() {
//   //from Perry's
//   string substanceName = "Air";
//   PerrysGasCpCorrelation gasCpCorrelation = new PerrysGasCpCorrelation(substanceName, 0.2896e5, 0.0939e5, 3.012e3, 0.0758e5, 1484.0, 50, 1500);
//   gasCpTablePerrys.Add(substanceName, gasCpCorrelation);
//   PerrysLiquidCpCorrelation liquidCpCorrelation = new PerrysLiquidCpCorrelation(substanceName, -2.1446e5, 9.1851e3, -1.0612e2, 4.1616e-1, 0, 132.45, 75, 115);
//   liquidCpTablePerrys.Add(substanceName, liquidCpCorrelation);
//   PerrysEvaporationHeatCorrelation evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(substanceName, 0.8474e7, 0.3822, 0.0, 0.0, 132.45, 59.15, 132.45);
//   evapHeatTablePerrys.Add(substanceName, evapHeatCorrelation);
//   PerrysVaporPressureCorrelation vapPressureCorrelation = new PerrysVaporPressureCorrelation(substanceName, 21.662, -692.39, -0.39208, 4.7574e-3, 1.0, 59.15, 132.45);
//   vapPressureTablePerrys.Add(substanceName, vapPressureCorrelation);
//   PerrysLiquidDensityCorrelation liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(substanceName, 2.8963, 0.26733, 132.45, 0.27341, 59.15, 132.45);
//   liquidDensityTablePerrys.Add(substanceName, liquidDensityCorrelation);

//   substanceName = "water";
//   gasCpCorrelation = new PerrysGasCpCorrelation(substanceName, 0.3336e5, 0.2679e5, 2.6105e3, 0.089e5, 1169, 100, 2273.15);  
//   gasCpTablePerrys.Add(substanceName, gasCpCorrelation);
//   liquidCpCorrelation = new PerrysLiquidCpCorrelation(substanceName, 2.7637e5, -2.0901e3, 8.125, -1.4116e-2, 9.3701e-6, 647.13, 273.16, 533.15);
//   liquidCpTablePerrys.Add(substanceName, liquidCpCorrelation);
//   evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(substanceName, 5.2053e7, 0.3199, -0.212, 0.25795, 647.13, 273.16, 647.13);
//   evapHeatTablePerrys.Add(substanceName, evapHeatCorrelation);
//   vapPressureCorrelation = new PerrysVaporPressureCorrelation(substanceName, 73.649, -7258.2, -7.3037, 4.1653e-6, 2.0, 273.16, 647.13);
//   vapPressureTablePerrys.Add(substanceName, vapPressureCorrelation);
//   liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(substanceName, 5.459, 0.30542, 647.13, 0.081, 273.16, 333.15);
//   liquidDensityTablePerrys.Add(substanceName, liquidDensityCorrelation);

//   substanceName = "carbon tetrachloride";
//   gasCpCorrelation = new PerrysGasCpCorrelation(substanceName, 0.3758e5, 0.7054e5, 0.5121e3, 0.4850e5, 236.1, 100, 1500);
//   gasCpTablePerrys.Add(substanceName, gasCpCorrelation);
//   liquidCpCorrelation = new PerrysLiquidCpCorrelation(substanceName, -7.5270e5, 8.9661e3, -3.0394e1, 3.4455e-2, 0.0, 556.35, 250.33, 388.71);
//   liquidCpTablePerrys.Add(substanceName, liquidCpCorrelation);
//   evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(substanceName, 4.3252e7, 0.37688, 0.0, 0.0, 556.35, 250.33, 556.35);
//   evapHeatTablePerrys.Add(substanceName, evapHeatCorrelation);
//   vapPressureCorrelation = new PerrysVaporPressureCorrelation(substanceName, 78.441, -6128.1, -8.5766, 6.8465e-6, 2.0, 250.33, 556.35);
//   vapPressureTablePerrys.Add(substanceName, vapPressureCorrelation);
//   liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(substanceName, 0.99835, 0.274, 556.35, 0.287, 250.33, 556.35);
//   liquidDensityTablePerrys.Add(substanceName, liquidDensityCorrelation);

//   substanceName = "benzene";
//   gasCpCorrelation = new PerrysGasCpCorrelation(substanceName, 0.4442e5, 2.3205e5, 1.4946e3, 1.7213e5, -678.15, 200, 1500);
//   gasCpTablePerrys.Add(substanceName, gasCpCorrelation);
//   liquidCpCorrelation = new PerrysLiquidCpCorrelation(substanceName, 1.2944e5, -1.6950e2, 6.4781e-1, 0.0, 0.0, 562.16, 278.68, 353.24);
//   liquidCpTablePerrys.Add(substanceName, liquidCpCorrelation);
//   evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(substanceName, 4.7500e7, 0.45238, 0.0534, -0.1181, 562.16, 278.68, 562.16);
//   evapHeatTablePerrys.Add(substanceName, evapHeatCorrelation);
//   vapPressureCorrelation = new PerrysVaporPressureCorrelation(substanceName, 83.918, -6517.7, -9.3453, 7.1182e-6, 2.0, 278.68, 562.16);
//   vapPressureTablePerrys.Add(substanceName, vapPressureCorrelation);
//   liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(substanceName, 0.99835, 0.274, 556.35, 0.287, 278.68, 562.16);
//   liquidDensityTablePerrys.Add(substanceName, liquidDensityCorrelation);

//   substanceName = "toluene";
//   gasCpCorrelation = new PerrysGasCpCorrelation(substanceName, 0.5814e5, 2.8630e5, 1.4406e3, 1.8980e5, -650.43, 200, 1500);
//   gasCpTablePerrys.Add(substanceName, gasCpCorrelation);
//   liquidCpCorrelation = new PerrysLiquidCpCorrelation(substanceName, 1.4014e5, -1.5230e2, 6.9500e-1, 0.0, 0.0, 591.8, 178.18, 500);
//   liquidCpTablePerrys.Add(substanceName, liquidCpCorrelation);
//   evapHeatCorrelation = new PerrysEvaporationHeatCorrelation(substanceName, 5.0144e7, 0.3859, 0.0, 0.0, 591.8, 178.18, 591.8);
//   evapHeatTablePerrys.Add(substanceName, evapHeatCorrelation);
//   vapPressureCorrelation = new PerrysVaporPressureCorrelation(substanceName, 80.877, -6902.4, -8.7761, 5.8034e-6, 2.0, 178.18, 591.8);
//   vapPressureTablePerrys.Add(substanceName, vapPressureCorrelation);
//   liquidDensityCorrelation = new PerrysLiquidDensityCorrelation(substanceName, 0.99835, 0.274, 556.35, 0.287, 178.18, 591.8);
//   liquidDensityTablePerrys.Add(substanceName, liquidDensityCorrelation);
//}

