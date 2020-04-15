using System;
using System.Collections.Generic;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace Prosimo.ThermalProperties {

   /// <summary>
   /// THE PROPERTIES OF GASES AND LIQUIDS, Chapter 11, Diffusion Coefficients, Section 11-3 DIFFUSION COEFFICIENTS 
   /// FOR BINARY GAS SYSTEMS AT LOW PRESSURES: PREDICTION FROM THEORY
   /// </summary>
   public class GasDiffusivityCalculator {
      private Substance moisture;
      private Substance gas;

      private static IDictionary<string, double> atomicDiffusionVolumeTable = new Dictionary<string, double>();
      private static IDictionary<string, double> molecularDiffusionVolumeTable = new Dictionary<string, double>();

      static GasDiffusivityCalculator() {
         atomicDiffusionVolumeTable.Add("C", 15.9);
         atomicDiffusionVolumeTable.Add("H", 2.31);
         atomicDiffusionVolumeTable.Add("O", 6.11);
         atomicDiffusionVolumeTable.Add("N", 4.54);
         atomicDiffusionVolumeTable.Add("F", 14.7);
         atomicDiffusionVolumeTable.Add("Cl", 21.0);
         atomicDiffusionVolumeTable.Add("Br", 21.9);
         atomicDiffusionVolumeTable.Add("I", 29.8);
         atomicDiffusionVolumeTable.Add("S", 22.9);
         atomicDiffusionVolumeTable.Add("AromaticRing", -18.3);
         atomicDiffusionVolumeTable.Add("HeterocyclicRing", -18.3);

         molecularDiffusionVolumeTable.Add("He", 2.67);
         molecularDiffusionVolumeTable.Add("Ne", 5.98);
         molecularDiffusionVolumeTable.Add("Ar", 16.2);
         molecularDiffusionVolumeTable.Add("Kr", 24.5);
         molecularDiffusionVolumeTable.Add("Xe", 32.7);
         molecularDiffusionVolumeTable.Add("H2", 6.12);
         molecularDiffusionVolumeTable.Add("D2", 6.84);
         molecularDiffusionVolumeTable.Add("N2", 18.5);
         molecularDiffusionVolumeTable.Add("02", 16.3);
         molecularDiffusionVolumeTable.Add("Air", 19.7);
         molecularDiffusionVolumeTable.Add("CO", 18.0);
         molecularDiffusionVolumeTable.Add("CO2", 26.9);
         molecularDiffusionVolumeTable.Add("N20", 35.9);
         molecularDiffusionVolumeTable.Add("NH3", 20.7);
         molecularDiffusionVolumeTable.Add("H20", 13.1);
         molecularDiffusionVolumeTable.Add("SF6", 71.3);
         molecularDiffusionVolumeTable.Add("Cl2", 38.4);
         molecularDiffusionVolumeTable.Add("Br2", 69.0);
         molecularDiffusionVolumeTable.Add("SO2", 41.8);
      }

      //the default constructor is for humid air
      public GasDiffusivityCalculator(Substance gas, Substance moisture) {
         this.gas = gas;
         this.moisture = moisture;
      }

      public double CalculateDiffusivity(double temperature, double pressure) {
         //air-water vapor diffusivity at 1 atm.
         //diffusivity = -2.775e-6 + 4.479e-8*temperature + 1.656e-10 *temperature*temperature;
         //this is from a website
         double diffusivity = 0;
         //if (gas.Name == "Air" && moisture.Name == "water") {
         if (gas.IsAir && moisture.IsWater) {
            diffusivity = -2.775e-6 + 4.479e-8 * temperature + 1.656e-10 * temperature * temperature;
            diffusivity = diffusivity * 1.013e5 / pressure;
         }
         else {
            double sumVolumeGas = 0;
            if (molecularDiffusionVolumeTable.ContainsKey(gas.Formula.ToString())) {
               sumVolumeGas = molecularDiffusionVolumeTable[gas.Formula.ToString()];
            }
            else {
               SubstanceFormula gasFormula = gas.Formula;
               string[] gasElements = gasFormula.Elements;
               foreach (string element in gasElements) {
                  sumVolumeGas += gasFormula.GetElementCount(element) * atomicDiffusionVolumeTable[element];
               }
            }

            double sumVolumeMoisture = 0;
            SubstanceFormula moistureFormula = moisture.Formula;
            string[] moistureElements = moistureFormula.Elements;
            foreach (string element in moistureElements) {
               sumVolumeMoisture += moistureFormula.GetElementCount(element) * atomicDiffusionVolumeTable[element];
            }

            double molarWeightTerm = Math.Pow((1.0 / gas.MolarWeight + 1.0 / moisture.MolarWeight), 0.5);
            double sumVolumeTerm = Math.Pow((Math.Pow(sumVolumeGas, 1.0 / 3) + Math.Pow(sumVolumeMoisture, 1.0 / 3)), 2);
            diffusivity = 0.01013 * Math.Pow(temperature, 1.75) * molarWeightTerm / (pressure * sumVolumeTerm);
         }

         return diffusivity;
      }

      public static void Main() {
         //GasDiffusivityCalculator gdc = new GasDiffusivityCalculator();
      }
   }
}