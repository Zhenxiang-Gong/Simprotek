using System;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Prosimo;
using Prosimo.SubstanceLibrary;
using Prosimo.Materials;
using Prosimo.Plots;

namespace Prosimo.ThermalProperties {
   //public enum SolutionType {Raoult, Aqueous, InorganicSalts, Ideal, Sucrose, ReducingSugars, Juices, Unknown};
   //public enum SolutionType {Sucrose, ReducingSugars, Juices, Unknown};

   /// <summary>
   /// Summary description for BoilingPointRiseModel.
   /// </summary>
   [Serializable]
   public class BoilingPointRiseCalculator {
      private const int CLASS_PERSISTENCE_VERSION = 1;

      public static double CalculateBoilingPointElevation(DryingMaterial dryingMaterial, double concentrationValue, double pressureValue) {
         SolutionType solutionType = dryingMaterial.SolutionType;
         double deltaT = 0;
         Substance s = dryingMaterial.Moisture;
         double solventMolarWeight = s.MolarWeight;
         s = dryingMaterial.AbsoluteDryMaterial;
         double soluteMolarWeight = s.MolarWeight;
         double tEvap = 0.0;
         /*if (solutionType == SolutionType.Raoult) {
            //Unit Operations in Food Engieering Eq. 18.13
            double soluteMassRatio = 1.0/owner.MoistureContentDryBase.Value;
            double boilingConstant = 1.0;
            deltaT = 1000 * boilingConstant * soluteMassRatio/soluteMolarWeight;
         }
         else if (solutionType == SolutionType.Aqueous) {
            //Unit Operations in Food Engieering Eq. 18.14
            double molarConcentration = concentrationValue/soluteMolarWeight/(concentrationValue/soluteMolarWeight+(1-concentrationValue)/solventMolarWeight);
            deltaT = 0.52 * molarConcentration;
         }
         else if (solutionType == SolutionType.InorganicSalts) {
            //Unit Operations in Food Engieering Eq. 18.17
            double molarConcentration = concentrationValue/soluteMolarWeight/(concentrationValue/soluteMolarWeight+(1-concentrationValue)/solventMolarWeight);
            deltaT = 104.9 * Math.Pow(molarConcentration, 1.14);
         }
         else if (solutionType == SolutionType.Ideal) {
            //Unit Operations in Food Engieering Eq. 18.15
            double tEvap = ThermalPropCalculator.CalculateTemperatureFromWaterVaporPressure(pressureValue);
            double evapHeat = owner.GetEvaporationHeat(tEvap);
            double solventMassFraction = 1.0/owner.MoistureContentWetBase.Value;
            deltaT = -tEvap/(1.0 + (evapHeat/(8.314*tEvap*Math.Log(solventMassFraction))));
         }*/
         if (solutionType == SolutionType.Sucrose) {
            //Unit Operations in Food Engieering Eq. 18.17
            pressureValue = pressureValue / 100; //note: pressure unit is mbar
            deltaT = 3.061 * Math.Pow(concentrationValue, 0.094) * Math.Pow(pressureValue, 0.136) * Math.Exp(5.328 * concentrationValue);
         }
         else if (solutionType == SolutionType.ReducingSugars) {
            //Unit Operations in Food Engieering Eq. 18.17
            pressureValue = pressureValue / 100; //note: pressure unit is mbar
            deltaT = 2.227 * Math.Pow(concentrationValue, 0.588) * Math.Pow(pressureValue, 0.119) * Math.Exp(3.593 * concentrationValue);
         }
         else if (solutionType == SolutionType.Juices) {
            //Unit Operations in Food Engieering Eq. 18.17
            pressureValue = pressureValue / 100; //note: pressure unit is mbar
            //deltaT = 0.04904 * Math.Pow(concentration, 0.029)*Math.Pow(pressureValue, 0.113)*Math.Exp(-0.03899*concentration + 6.52e-4*concentration*concentration);
            deltaT = 1.36 * Math.Pow(concentrationValue, 0.749) * Math.Pow(pressureValue, 0.106) * Math.Exp(3.39 * concentrationValue);
         }
         else if (solutionType == SolutionType.Unknown) {
            //Duhring rules
            tEvap = ThermalPropCalculator.CalculateWaterSaturationTemperature(pressureValue);
            CurveF[] duhringLines = dryingMaterial.DuhringLines;
            if (duhringLines != null) {
               double tEvapReal = ChartUtil.GetInterpolatedValue(duhringLines, concentrationValue, tEvap);
               deltaT = tEvapReal - tEvap;
            }
            /*double correctionCoeff = 1.0;
            if (duhringLines.VarValue == Constants.NO_VALUE && Math.Abs(pressureValue-1.0132685e5) < 1.0e-3) {
               double evapHeat = GetEvaporationHeat(tEvap);
               correctionCoeff = 0.0162*tEvap*tEvap/evapHeat;  //Chemical Engineering Eq. 7-15 at p.310 
               deltaT *= correctionCoeff;
            }*/
         }
         return deltaT;
      }
   }
}
