using System;
using System.Collections.Generic;
using Prosimo.SubstanceLibrary;
using Prosimo.ThermalProperties;

namespace Prosimo.ThermalProperties {

   /// <summary>
   /// Summary description.
   /// </summary>
   public class HumidGasCalculator {
      private Substance moisture;
      private Substance gas;

      //raio of moisture molar mass to dry gas molar mass
      private double moistureGasMolarMassRatio;
      //moisture properties object
      private MoistureProperties moistureProperties;
      private GasProperties gasProperties;

      private GasDiffusivityCalculator diffusivityCalculator;

      //default gas is air, default moisture is water
      //water's molar mass is 18.01
      private double moistureMolarMass;
      //dry air's molar mass is 28.96
      private double gasMolarMass;

      //private double psychrometricRatio;

      private static IDictionary<string, IDictionary<string, double>> gasSolventPsychrometricRatioTable;

      static HumidGasCalculator() {
         gasSolventPsychrometricRatioTable = new Dictionary<string, IDictionary<string, double>>();
         IDictionary<string, double> solventPsychrometricRatioTable = new Dictionary<string, double>();
         solventPsychrometricRatioTable.Add(Substance.WATER, 1.0);
         solventPsychrometricRatioTable.Add(Substance.CARBON_TETRACHLORIDE, 1.06 * 0.51);
         //solventPsychrometricRatioTable.Add("tetrachloromethane", 1.06 * 0.51);
         solventPsychrometricRatioTable.Add(Substance.BENZENE, 1.06 * 0.54);
         solventPsychrometricRatioTable.Add(Substance.TOLUENE, 1.06 * 0.47);
         gasSolventPsychrometricRatioTable.Add(Substance.AIR, solventPsychrometricRatioTable);
      }

      //the default constructor is for humid air
      //public HumidGasCalculator(Substance gas, Substance moisture, MoistureProperties moistureProperties) {
      public HumidGasCalculator(Substance gas, Substance moisture) {
         this.gas = gas;
         this.moisture = moisture;
         //this.moistureProperties = moistureProperties;
         moistureMolarMass = moisture.MolarWeight;
         gasMolarMass = gas.MolarWeight;
         moistureGasMolarMassRatio = moistureMolarMass / gasMolarMass;

         //psychrometricRatio = gasSolventPsychrometricRatioTable[gas.Name][moisture.Name];

         moistureProperties = new MoistureProperties(moisture);
         gasProperties = new GasProperties(gas);
         //this.specificHeatOfMoisture = moistureProperties.GetSpecificHeatOfVapor();
         //this.specificHeatOfDryGas = gasProperties.GetSpecificHeatOfDryGas(293.15);

         diffusivityCalculator = new GasDiffusivityCalculator(gas, moisture);
      }

      //this is a generic constructor for all possible drying gases
      /*public HumidGasCalculator(double moistureMolarMass, double gasMolarMass, double specificHeatOfMoisture, double specificHeatOfDryGas) { 
         this.moistureMolarMass = moistureMolarMass;
         this.gasMolarMass = gasMolarMass;
         this.specificHeatOfMoisture = moistureProperties.GetSpecificHeatOfVapor();
         this.specificHeatOfDryGas = gasProperties.GetSpecificHeatOfDryGas();
         moistureGasMolarMassRatio = moistureMolarMass/gasMolarMass;
      }*/

      public double GetHumidityFromDryBulbWetBulbAndPressure(double dryBulbTemperature, double wetBulbTemperature, double totalPressure) {
         //double pSaturation = moistureProperties.GetSaturationPressure(wetBulbTemperature);
         //double saturationHumidity = moistureGasMolarMassRatio * pSaturation / (totalPressure - pSaturation);
         double saturationHumidity = GetHumidityFromDewPointAndPressure(wetBulbTemperature, totalPressure);

         double evapHeat = moistureProperties.GetEvaporationHeat(wetBulbTemperature);
         double humidity = 0.01;
         double humidity_old;
         double cpGas;
         double cpMoisture;
         double psychrometricRatio = 1.0;
         double evapHeatSigma;
         int counter = 0;
         do {
            psychrometricRatio = CalculatePsychrometricRatio(dryBulbTemperature, wetBulbTemperature, humidity, totalPressure);

            evapHeatSigma = evapHeat * psychrometricRatio;
            cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + wetBulbTemperature) / 2.0);
            cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + wetBulbTemperature) / 2.0);
            humidity_old = humidity;
            humidity = (evapHeatSigma * saturationHumidity - cpGas * (dryBulbTemperature - wetBulbTemperature)) /
               (evapHeatSigma + cpMoisture * (dryBulbTemperature - wetBulbTemperature));
         } while (Math.Abs(humidity - humidity_old) / humidity > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of humidity from dry-bulb and wet-bulb temperatures failed.");
         }
         return humidity;
      }

      public double GetDryBulbFromWetBulbHumidityAndPressure(double wetBulbTemperature, double humidity, double totalPressure) {
         //double pSaturation = moistureProperties.GetSaturationPressure(wetBulbTemperature);
         //double saturationHumidityAtWetBulb = moistureGasMolarMassRatio * pSaturation / (totalPressure - pSaturation);
         double saturationHumidityAtWetBulb = GetHumidityFromDewPointAndPressure(wetBulbTemperature, totalPressure);

         double evapHeat = moistureProperties.GetEvaporationHeat(wetBulbTemperature);
         //double dryBulbTemperature = evapHeat * (saturationHumidityAtWetBulb - humidity)/(specificHeatOfDryGas + specificHeatOfMoisture * humidity) + wetBulbTemperature;
         //return dryBulbTemperature;
         double cpGas = GetSpecificHeatOfDryGas();
         double cpMoisture = GetSpecificHeatOfVapor();
         double dryBulbTemperature = wetBulbTemperature + 1;
         double dryBulbTemperature_old = wetBulbTemperature;
         double psychrometricRatio = 1.0;
         int counter = 0;
         do {
            counter++;
            psychrometricRatio = CalculatePsychrometricRatio(dryBulbTemperature, wetBulbTemperature, humidity, totalPressure);
            //evapHeat *= psychrometricRatio;

            dryBulbTemperature_old = dryBulbTemperature;
            dryBulbTemperature = psychrometricRatio * evapHeat * (saturationHumidityAtWetBulb - humidity) / (cpGas + cpMoisture * humidity) + wetBulbTemperature;
            cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + wetBulbTemperature) / 2.0);
            cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + wetBulbTemperature) / 2.0);
         } while (Math.Abs(dryBulbTemperature - dryBulbTemperature_old) / dryBulbTemperature > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from wet-bulb temperature and humidity failed.");
         }
         return dryBulbTemperature;
      }

      public double GetDryBulbFromSatTempHumidityAndPressure(double satTemperature, double humidity, double totalPressure) {
         //double pSaturation = moistureProperties.GetSaturationPressure(wetBulbTemperature);
         //double saturationHumidityAtWetBulb = moistureGasMolarMassRatio * pSaturation / (totalPressure - pSaturation);
         double saturationHumidityAtSatTemp = GetHumidityFromDewPointAndPressure(satTemperature, totalPressure);

         double evapHeat = moistureProperties.GetEvaporationHeat(satTemperature);
         //double dryBulbTemperature = evapHeat * (saturationHumidityAtWetBulb - humidity)/(specificHeatOfDryGas + specificHeatOfMoisture * humidity) + wetBulbTemperature;
         //return dryBulbTemperature;
         double cpGas = GetSpecificHeatOfDryGas();
         double cpMoisture = GetSpecificHeatOfVapor();
         double dryBulbTemperature = satTemperature + 1;
         double dryBulbTemperature_old = satTemperature;
         //double psychrometricRatio = 1.0;
         int counter = 0;
         do {
            counter++;
            //psychrometricRatio = CalculatePsychrometricRatio(dryBulbTemperature, wetBulbTemperature, humidity, totalPressure);

            dryBulbTemperature_old = dryBulbTemperature;
            dryBulbTemperature = evapHeat * (saturationHumidityAtSatTemp - humidity) / (cpGas + cpMoisture * humidity) + satTemperature;
            cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + satTemperature) / 2.0);
            cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + satTemperature) / 2.0);
         } while (Math.Abs(dryBulbTemperature - dryBulbTemperature_old) / dryBulbTemperature > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from saturation temperature and humidity failed.");
         }
         return dryBulbTemperature;
      }

      public double GetDryBulbFromWetBulbRelativeHumidityAndPressure(double wetBulbTemperature, double relativeHumidity, double totalPressure) {
         //if (relativeHumidity >= 1.0) {
         //   return wetBulbTemperature;
         //}
         double fy_temp = 0;
         double humidity = 0;
         double delta = 5.0;
         double totalDelta = delta;
         double dryBulbTemperature = wetBulbTemperature + delta;
         bool negativeLastTime = false;

         int counter = 0;
         do {
            counter++;
            humidity = GetHumidityFromDryBulbWetBulbAndPressure(dryBulbTemperature, wetBulbTemperature, totalPressure);
            fy_temp = GetRelativeHumidityFromDryBulbHumidityAndPressure(dryBulbTemperature, humidity, totalPressure);
            if (relativeHumidity < fy_temp) {
               if (negativeLastTime) {
                  delta /= 2.0; //testing finds delta/2.0 is almost optimal
               }
               totalDelta += delta;
               negativeLastTime = false;
            }
            else if (relativeHumidity > fy_temp) {
               delta /= 2.0; //testing finds delta/2.0 is almost optimal
               totalDelta -= delta;
               negativeLastTime = true;
            }
            dryBulbTemperature = wetBulbTemperature + totalDelta;
         } while (Math.Abs(relativeHumidity - fy_temp) > 1.0e-6 && counter < 1000);

         if (counter == 1000) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from wet-bulb temperature and relative humidity failed.");
         }

         return dryBulbTemperature;
      }

      public double GetWetBulbFromDryBulbHumidityAndPressure(double dryBulbTemperature, double humidity, double totalPressure) {
         dryBulbTemperature = moistureProperties.GetModifiedTemperature(dryBulbTemperature);
         //Note: the convergence speed of the iteration procedure used in this method is very good.
         //It has a better convergence speed than Newton method.
         double dampingFactor = 0.5;
         if (dryBulbTemperature <= 273.15 && dryBulbTemperature > 263.15) {
            dampingFactor = 0.75;
         }
         else if (dryBulbTemperature <= 263.15 && dryBulbTemperature > 258.15) {
            dampingFactor = 0.85;
         }
         else if (dryBulbTemperature <= 258.15) {
            dampingFactor = 0.95;
         }

         int counter = 0;
         double tempDiff;
         double saturationHumidityAtWetBulb;
         double evapHeat;
         double pSaturationAtWetBulb;
         double wetBulbTemperatureOld;
         double cpGas = GetSpecificHeatOfDryGas();
         double cpMoisture = moistureProperties.GetSpecificHeatOfVapor();
         double psychrometricRatio = 1.0;
         //intial estimate of the wet bulb temperature is 5 degrees lower than dry bulb temperature
         double wetBulbTemperature = dryBulbTemperature - 5;
         do {
            counter++;
            psychrometricRatio = CalculatePsychrometricRatio(dryBulbTemperature, wetBulbTemperature, humidity, totalPressure);
            evapHeat = moistureProperties.GetEvaporationHeat(wetBulbTemperature);
            evapHeat *= psychrometricRatio;
            saturationHumidityAtWetBulb = (cpGas + cpMoisture * humidity) / (evapHeat) * (dryBulbTemperature - wetBulbTemperature) + humidity;
            pSaturationAtWetBulb = totalPressure * saturationHumidityAtWetBulb / (moistureGasMolarMassRatio + saturationHumidityAtWetBulb);
            wetBulbTemperatureOld = wetBulbTemperature;
            wetBulbTemperature = moistureProperties.GetSaturationTemperature(pSaturationAtWetBulb);
            tempDiff = wetBulbTemperature - wetBulbTemperatureOld;
            wetBulbTemperature = wetBulbTemperature - dampingFactor * tempDiff;
            cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + wetBulbTemperature) / 2.0);
            cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + wetBulbTemperature) / 2.0);
         } while (counter < 500 && Math.Abs(tempDiff / wetBulbTemperature) > 1.0e-6);

         if (counter == 500) {
            throw new CalculationFailedException("Calculation of wet-bulb temperature from dry-bulb temperature and humidity failed.");
         }

         return wetBulbTemperature;
      }

      public double GetSatTempFromDryBulbHumidityAndPressure(double dryBulbTemperature, double humidity, double totalPressure) {
         dryBulbTemperature = moistureProperties.GetModifiedTemperature(dryBulbTemperature);

         //Note: the convergence speed of the iteration procedure used in this method is very good.
         //It has a better convergence speed than Newton method.
         double dampingFactor = 0.5;
         if (dryBulbTemperature <= 273.15 && dryBulbTemperature > 263.15) {
            dampingFactor = 0.75;
         }
         else if (dryBulbTemperature <= 263.15 && dryBulbTemperature > 258.15) {
            dampingFactor = 0.85;
         }
         else if (dryBulbTemperature <= 258.15) {
            dampingFactor = 0.95;
         }

         int counter = 0;
         double tempDiff;
         double saturationHumidityAtSatTemp;
         double evapHeat;
         double pSaturationAtSatTemp;
         double satTemperatureOld;
         double cpGas = GetSpecificHeatOfDryGas();
         double cpMoisture = moistureProperties.GetSpecificHeatOfVapor();
         //intial estimate of the wet bulb temperature is 5 degrees lower than dry bulb temperature
         double satTemperature = dryBulbTemperature - 5;
         do {
            counter++;
            evapHeat = moistureProperties.GetEvaporationHeat(satTemperature);
            saturationHumidityAtSatTemp = (cpGas + cpMoisture * humidity) / (evapHeat) * (dryBulbTemperature - satTemperature) + humidity;
            pSaturationAtSatTemp = totalPressure * saturationHumidityAtSatTemp / (moistureGasMolarMassRatio + saturationHumidityAtSatTemp);
            satTemperatureOld = satTemperature;
            satTemperature = moistureProperties.GetSaturationTemperature(pSaturationAtSatTemp);
            tempDiff = satTemperature - satTemperatureOld;
            satTemperature = satTemperature - dampingFactor * tempDiff;
            cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + satTemperature) / 2.0);
            cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + satTemperature) / 2.0);
         } while (counter < 500 && Math.Abs(tempDiff / satTemperature) > 1.0e-6);

         if (counter == 500) {
            throw new CalculationFailedException("Calculation of saturation temperature from dry-bulb temperature and humidity failed.");
         }

         return satTemperature;
      }

      public double GetRelativeHumidityFromDryBulbAndDewPoint(double dryBulbTemperature, double dewPoint) {
         double pSaturation = moistureProperties.GetSaturationPressure(dryBulbTemperature);
         double pPartial = moistureProperties.GetSaturationPressure(dewPoint);
         return pPartial / pSaturation;
      }

      public double GetDryBulbFromDewPointAndRelativeHumidity(double dewPoint, double relativeHumidity) {
         double pPartial = moistureProperties.GetSaturationPressure(dewPoint);
         double pSaturation = pPartial / relativeHumidity;
         double dryBulbTemperature = moistureProperties.GetSaturationTemperature(pSaturation);
         return dryBulbTemperature;
      }

      public double GetHumidityFromDewPointAndPressure(double dewPoint, double totalPressure) {
         double pPartial = moistureProperties.GetSaturationPressure(dewPoint);
         double retValue = moistureGasMolarMassRatio * pPartial / (totalPressure - pPartial);
         //if total pressure is less than pPartial it means that moisture is in the vapor state
         //so gas has no limit to absorb moisture vapor
         if (retValue < 0.0) {
            retValue = double.MaxValue;
         }
         return retValue;
      }

      public double GetRelativeHumidityFromDryBulbHumidityAndPressure(double dryBulbTemperature, double humidity, double totalPressure) {
         double pPartial = humidity * totalPressure / (moistureGasMolarMassRatio + humidity);
         double pSaturation = moistureProperties.GetSaturationPressure(dryBulbTemperature);
         return pPartial / pSaturation;
      }

      public double GetPressureFromDewPointAndHumidity(double dewPoint, double humidity) {
         double pPartial = moistureProperties.GetSaturationPressure(dewPoint);
         return (moistureGasMolarMassRatio + humidity) * pPartial / humidity;
      }

      public double GetDewPointFromHumidityAndPressure(double humidity, double totalPressure) {
         double pPartial = humidity * totalPressure / (moistureGasMolarMassRatio + humidity);
         double dewPoint = moistureProperties.GetSaturationTemperature(pPartial);
         return dewPoint;
      }

      public double GetDewPointFromDryBulbAndRelativeHumidity(double dryBulbTemperature, double relativeHumidity) {
         double pSaturation = moistureProperties.GetSaturationPressure(dryBulbTemperature);
         double pPartial = pSaturation * relativeHumidity;
         double dewPoint = moistureProperties.GetSaturationTemperature(pPartial);
         return dewPoint;
      }

      //please note that temperature here is K
      public double GetHumidVolume(double temperature, double humidity, double pressure) {
         return 0.082 * (1.0 / gasMolarMass + humidity / moistureMolarMass) * temperature / pressure * 1.013e5;
      }

      public double GetHumidEnthalpyFromDryBulbHumidityAndPressure(double temperature, double humidity, double pressure) {
         double airCp = gasProperties.GetSpecificHeatOfDryGas((273.15 + temperature) / 2.0);
         double specificEnthalpyOfDryGas = airCp * (temperature - 273.15);
         //pressure caused enthalpy diferene
         //double deltaEnthalpyOfDryGas = CalculateDeltaEnthalpy(gas, temperature, pressure);

         double dewPoint = GetDewPointFromHumidityAndPressure(humidity, pressure);
         //double pPartial = humidity * pressure / (moistureGasMolarMassRatio + humidity);
         double liquidMoistureCp = moistureProperties.GetSpecificHeatOfLiquid((273.15 + dewPoint) / 2.0);
         double vaporMoistureCp = moistureProperties.GetSpecificHeatOfVapor((dewPoint + temperature) / 2.0);
         double evaporationHeat = moistureProperties.GetEvaporationHeat(dewPoint);

         //double specificEnthalpyOfMoisture = humidity * (liquidMoistureCp * (dewPoint - 273.15) + evaporationHeat + vaporMoistureCp * (temperature - dewPoint));
         double specificEnthalpyOfMoisture = liquidMoistureCp * (dewPoint - 273.15) + evaporationHeat + vaporMoistureCp * (temperature - dewPoint);
         //double deltaEnthalpyOfMoisture = CalculateDeltaEnthalpy(moisture, temperature, pressure);
         //double specificEnthalpyOfMoisture = humidity * moistureProperties.GetEnthalpy(pPartial, temperature);
         //return specificEnthalpyOfDryGas + deltaEnthalpyOfDryGas + humidity * (specificEnthalpyOfMoisture + deltaEnthalpyOfDryGas);
         return specificEnthalpyOfDryGas + humidity * (specificEnthalpyOfMoisture);
      }

      public double GetHumidityFromHumidEnthalpyTemperatureAndPressure(double specificEnthalpy, double temperature, double pressure) {
         double cg = GetSpecificHeatOfDryGas();
         double cv = GetSpecificHeatOfVapor();
         double r0 = GetEvaporationHeat(273.15);
         double y = (specificEnthalpy - cg * (temperature - 273.15)) / (r0 + cv * (temperature - 273.15));
         if (y <= 0) {
            y = 1.0e-8;
         }

         cg = gasProperties.GetSpecificHeatOfDryGas((273.15 + temperature) / 2.0);
         double specificEnthalpyOfDryGas = cg * (temperature - 273.15);
         double y_old;
         double dewPoint;
         double specificHeatOfLiquid;
         double specificHeatOfVapor;
         double evaporationHeat;
         double iv;
         int counter = 0;
         do {
            counter++;
            dewPoint = GetDewPointFromHumidityAndPressure(y, pressure);
            specificHeatOfLiquid = moistureProperties.GetSpecificHeatOfLiquid((dewPoint + 273.15) / 2.0);
            specificHeatOfVapor = moistureProperties.GetSpecificHeatOfVapor((dewPoint + temperature) / 2.0);
            evaporationHeat = moistureProperties.GetEvaporationHeat(dewPoint);
            iv = specificHeatOfLiquid * (dewPoint - 273.15) + evaporationHeat + specificHeatOfVapor * (temperature - dewPoint);
            y_old = y;
            y = (specificEnthalpy - specificEnthalpyOfDryGas) / iv;
         } while (Math.Abs(y - y_old) / y > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of humidity from humid enthalpy and temperature failed.");
         }

         return y;
      }

      public double GetDryBulbFromHumidEnthalpyHumidityAndPressure(double humidEnthalpy, double humidity, double pressure) {
         double cg = GetSpecificHeatOfDryGas();
         double cv = GetSpecificHeatOfVapor();
         //double r0 = GetEvaporationHeat(273.15);
         double dewPoint = GetDewPointFromHumidityAndPressure(humidity, pressure);
         double r0 = GetEvaporationHeat(dewPoint);
         double tg = (humidEnthalpy - r0 * humidity) / (cg + cv * humidity) + 273.15;

         //double dewPoint = GetDewPointFromHumidityAndPressure(humidity, pressure);;
         double specificHeatOfLiquid = moistureProperties.GetSpecificHeatOfLiquid((dewPoint + 273.15) / 2.0);
         double evaporationHeat = moistureProperties.GetEvaporationHeat(dewPoint);
         //double specificHeatOfVapor;
         double tg_old;
         double tempValue;
         int counter = 0;
         do {
            counter++;
            //cg = gasProperties.GetMeanSpecificHeatOfDryGas(273.15, tg);
            //cv = moistureProperties.GetSpecificHeatOfVapor((dewPoint + tg)/2.0);
            tempValue = humidity * (specificHeatOfLiquid * (dewPoint - 273.15) + evaporationHeat - cv * dewPoint);
            tg_old = tg;
            tg = (humidEnthalpy + cg * 273.15 - tempValue) / (cv * humidity + cg);
            cg = gasProperties.GetSpecificHeatOfDryGas((273.15 + tg) / 2.0);
            cv = moistureProperties.GetSpecificHeatOfVapor((dewPoint + tg) / 2.0);
         } while (Math.Abs(tg - tg_old) > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from humid enthalpy and humidity failed.");
         }
         return tg;
      }

      public double GetDryBulbFromHumidEnthalpyRelativeHumidityAndPressure(double humidEnthalpy, double relativeHumidity, double pressure) {
         double temperature = 320;
         double temperatureOld;
         int counter = 0;
         double dewPoint;
         do {
            counter++;
            dewPoint = GetDewPointFromDryBulbAndRelativeHumidity(temperature, relativeHumidity);
            temperatureOld = temperature;
            temperature = GetDryBulbFromHumidEnthalpyDewPointAndPressure(humidEnthalpy, dewPoint, pressure);
            temperature = temperatureOld + (temperature - temperatureOld) * 0.3;
         } while (Math.Abs(temperature - temperatureOld) / temperature > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from humid enthalpy and relative humidity failed.");
         }

         return temperature;
      }

      public double GetDryBulbFromHumidEnthalpyDewPointAndPressure(double humidEnthalpy, double dewPoint, double pressure) {
         double humidity = GetHumidityFromDewPointAndPressure(dewPoint, pressure);
         return GetDryBulbFromHumidEnthalpyHumidityAndPressure(humidEnthalpy, humidity, pressure);
      }

      public double GetDryBulbFromHumidEnthalpyWetBulbAndPressure(double humidEnthalpy, double wetBulb, double pressure) {
         double temperature = wetBulb + 5.0;
         double temperatureOld;
         int counter = 0;
         double humidity;
         do {
            counter++;
            humidity = GetHumidityFromDryBulbWetBulbAndPressure(temperature, wetBulb, pressure);
            temperatureOld = temperature;
            temperature = GetDryBulbFromHumidEnthalpyHumidityAndPressure(humidEnthalpy, humidity, pressure);
         } while (Math.Abs(temperature - temperatureOld) / temperature > 1.0e-6 && counter < 200);

         if (counter == 200) {
            throw new CalculationFailedException("Calculation of dry-bulb temperature from humid enthalpy and wet-bulb temperature failed.");
         }

         return temperature;
      }

      public double GetHumidHeat(double humidity) {
         return gasProperties.GetSpecificHeatOfDryGas(293.15) + moistureProperties.GetSpecificHeatOfVapor() * humidity;
      }

      public double GetHumidHeat(double humidity, double temperature) {
         double cpAir = GetSpecificHeatOfDryGas(temperature);
         return cpAir + moistureProperties.GetSpecificHeatOfVapor(temperature) * humidity;
      }

      public double GetMoistureSpecificEnthalpy(double temperature) {
         return moistureProperties.GetEvaporationHeat(273.15) + moistureProperties.GetSpecificHeatOfVapor() * (temperature - 273.15);
      }

      public double GetSpecificHeatOfDryGas() {
         return GetSpecificHeatOfDryGas(293.15);
      }

      public double GetSpecificHeatOfDryGas(double temperature) {
         return gasProperties.GetSpecificHeatOfDryGas(temperature);
      }

      public double GetSpecificHeatOfLiquid() {
         return moistureProperties.GetSpecificHeatOfLiquid();
      }

      public double GetSpecificHeatOfVapor() {
         return moistureProperties.GetSpecificHeatOfVapor();
      }

      public double GetSpecificHeatOfVapor(double temperature) {
         return moistureProperties.GetSpecificHeatOfVapor(temperature);
      }

      public double GetSpecificHeatOfLiquid(double temperature) {
         return moistureProperties.GetSpecificHeatOfLiquid(temperature);
      }

      public double GetEvaporationHeat(double temperature) {
         return moistureProperties.GetEvaporationHeat(temperature);
      }

      private bool HasPredefinedPsychrometricRatio() {
         bool hasIt = false;
         if (gasSolventPsychrometricRatioTable.ContainsKey(gas.Name)) {
            IDictionary<string, double> solventPsychrometricRatioTable = gasSolventPsychrometricRatioTable[gas.Name];
            if (solventPsychrometricRatioTable.ContainsKey(moisture.Name)) {
               hasIt = true;
            }
         }
         return hasIt;
      }

      private double CalculatePsychrometricRatio(double temperature, double wetBulbTemperature, double humidity, double pressure) {
         double psychrometricRatio = 1.0;
         if (HasPredefinedPsychrometricRatio()) {
            psychrometricRatio = gasSolventPsychrometricRatioTable[gas.Name][moisture.Name];
         }
         else {
            double saturationHumidity = GetHumidityFromDewPointAndPressure(wetBulbTemperature, pressure);
            double humidityDiff = saturationHumidity - humidity;
            double fai = moistureGasMolarMassRatio / humidityDiff * Math.Log(1 + humidityDiff / (moistureGasMolarMassRatio + humidity));

            double avgTemperature = (temperature + wetBulbTemperature) / 2.0;
            double avgHumidity = (humidity + saturationHumidity) / 2.0;
            //double le = CalculateLewisNumber(temperature, humidity, pressure);
            //double le2 = CalculateLewisNumber(wetBulbTemperature, saturationHumidity, pressure);
            double le = CalculateLewisNumber(avgTemperature, avgHumidity, pressure);

            psychrometricRatio = Math.Pow(le, -2.0 / 3) * fai;
         }
         return psychrometricRatio;
      }

      private double CalculateLewisNumber(double temperature, double humidity, double pressure) {
         double diffusivity = diffusivityCalculator.CalculateDiffusivity(temperature, pressure);
         double cp = GetHumidHeat(humidity, temperature) / (1 + humidity);
         double k = CalculateHumidGasConductivity(temperature, humidity);
         double rho = (1.0 + humidity) / GetHumidVolume(temperature, humidity, pressure);

         //ThermalPropCalculator propCalculator = ThermalPropCalculator.Instance;
         //double cpGas = GetSpecificHeatOfDryGas(temperature);
         //double kGas = propCalculator.CalculateGasThermalConductivity(temperature, gas);
         //double muGas = propCalculator.CalculateGasViscosity(temperature, gas);
         //double rhoGas = pressure / 1.013e5 * gasMolarMass / (0.082 * temperature);
         //double rhoGas = propCalculator.CalculateGasDensity(temperature, pressure, gas);
         //double prGas = cpGas * muGas / kGas;
         //double scGas = muGas / (rhoGas * diffusivity);

         //double cpMoisture = moistureProperties.GetSpecificHeatOfVapor(temperature);
         //double kMoisture = propCalculator.CalculateGasThermalConductivity(temperature, moisture);
         //double rhoMoisture = propCalculator.CalculateGasDensity(temperature, pressure, moisture);

         //double luGas = 1.0/kGas * (cpGas * rhoGas * diffusivity);
         //double luMoisture = 1.0/kMoisture * (cpMoisture * rhoMoisture * diffusivity);
         double le = k / (cp * rho * diffusivity);
         return le;
      }

      /// <summary>
      /// THE PROPERTIES OF GASES AND LIQUIDS, Section 10.6
      /// </summary>
      /// <param name="temperature"></param>
      /// <param name="humidity"></param>
      /// <returns></returns>
      private double CalculateHumidGasConductivity(double temperature, double humidity) {
         ThermalPropCalculator propCalculator = ThermalPropCalculator.Instance;
         double gasK = propCalculator.CalculateGasThermalConductivity(temperature, gas);
         double moistureK = propCalculator.CalculateGasThermalConductivity(temperature, moisture);

         double gasMole = 1 / gasMolarMass;
         double moistureMole = humidity / moistureMolarMass;

         double totalMole = gasMole + moistureMole;

         double gasMoleFraction = gasMole / totalMole;
         double moistureMoleFraction = moistureMole / totalMole;

         double gasPc = gas.CriticalPropsAndAcentricFactor.CriticalPressure / 1.0e5;
         double gasTc = gas.CriticalPropsAndAcentricFactor.CriticalTemperature;

         double gasTr = temperature / gasTc;
         double gasReducedInverseK = 210 * Math.Pow(gasTc * Math.Pow(gasMolarMass, 3) / Math.Pow(gasPc, 4), 1.0 / 6.0);
         double monatomicValueOfGasK = gasReducedInverseK * (Math.Exp(0.0464 * gasTr) - Math.Exp(-0.2412 * gasTr));

         double moisturePc = moisture.CriticalPropsAndAcentricFactor.CriticalPressure / 1.0e5;
         double moistureTc = moisture.CriticalPropsAndAcentricFactor.CriticalTemperature;
         double moistureTr = temperature / moistureTc;
         double moistureReducedInverseK = 210 * Math.Pow(moistureTc * Math.Pow(moistureMolarMass, 3) / Math.Pow(moisturePc, 4), 1.0 / 6.0);
         double monatomicValueOfMoistureK = moistureReducedInverseK * (Math.Exp(0.0464 * moistureTr) - Math.Exp(-0.2412 * moistureTr));

         double gasMoistureMolarMassRatio = 1 / moistureGasMolarMassRatio;
         double temp = 1 + Math.Sqrt(monatomicValueOfGasK / monatomicValueOfMoistureK) * Math.Pow(gasMoistureMolarMassRatio, 0.25);
         double aGasMoisture = temp * temp / Math.Sqrt(8.0 * (1.0 + gasMoistureMolarMassRatio));

         temp = 1 + Math.Sqrt(monatomicValueOfMoistureK / monatomicValueOfGasK) * Math.Pow(moistureGasMolarMassRatio, 0.25);
         double aMoistureGas = temp * temp / Math.Sqrt(8.0 * (1.0 + moistureGasMolarMassRatio));
         double mixtureK = gasMoleFraction * gasK / (gasMoleFraction + aGasMoisture * moistureMoleFraction) +
            moistureMoleFraction * moistureK / (aMoistureGas * gasMoleFraction + moistureMoleFraction);

         return mixtureK;
      }

      private double CalculateDeltaEnthalpy(Substance s, double temperature, double pressure) {
         double Tc = s.CriticalPropsAndAcentricFactor.CriticalTemperature;
         double Pc = s.CriticalPropsAndAcentricFactor.CriticalPressure;
         double rf = 0.75;
         double z = 1.0;
         double R = Constants.R;
         double molarWeight = s.MolarWeight;
         double a = 0.42748 * R * R * Math.Pow(Tc, 2.5) / Pc;
         double b = 0.08664 * R * Tc / Pc;
         double Tr = temperature / Tc;
         double Pr = pressure / Pc;
         double A = 0.42748 * Pr / Math.Pow(Tr, 2.5);
         double B = 0.09664 * Pr / Tr;

         int iter = 0;
         double f;
         double fp;
         double dz;
         do {
            iter++;
            f = -A * B - z * ((B * (B + 1.0) - A) + z * (1.0 - z));
            fp = -(B * (B + 1.0) - A) - z * (2.0 - 3.0 * z);
            dz = -f / fp;
            if (Math.Abs(dz) > z * rf) {
               dz = (dz >= 0.0) ? Math.Abs(z * rf) : -Math.Abs(z * rf);
            }
            z = z + dz;
         } while (Math.Abs(f) > 5.0e-8 && iter < 100);

         double discr = (1.0 + 3.0 * z) * (1.0 - z) + 4.0 * (B * (B + 1.0) - A);
         if (discr >= 0.0) {
            double z1 = 0.5 * (1.0 - z + Math.Sqrt(discr));
            double z2 = 1.0 - z - z1;
            z = Math.Max(z, Math.Max(z1, z2));
         }

         double deltaH = R * temperature * (z - 1.0 - 1.5 * a * a / b * Math.Log(1.0 + b * pressure / z));
         return deltaH / molarWeight;
      }

      //public double GetPressureFromDryBulbWetBulbAndDewPoint(double dryBulbTemperature, double wetBulbTemperature, double dewPoint) {
      //   double pPartialAtDewPoint = moistureProperties.GetSaturationPressure(dewPoint);
      //   double pPartialAtWetBulb = moistureProperties.GetSaturationPressure(wetBulbTemperature);
      //   double evapHeat = moistureProperties.GetEvaporationHeat(wetBulbTemperature);

      //   double cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + wetBulbTemperature) / 2.0);
      //   double cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + wetBulbTemperature) / 2.0);
      //   double a = cpGas;
      //   double b = cpMoisture * pPartialAtDewPoint - cpGas * (pPartialAtWetBulb + pPartialAtDewPoint) -
      //              moistureGasMolarMassRatio * evapHeat * (pPartialAtWetBulb - pPartialAtDewPoint) / (dryBulbTemperature - wetBulbTemperature);
      //   double c = (cpGas - cpMoisture) * pPartialAtDewPoint * pPartialAtWetBulb;

      //   double temp = b * b - 4 * a * c;
      //   double totalPressure = double.NaN;
      //   if (temp > 0) {
      //      totalPressure = (-b + Math.Sqrt(temp)) / (2 * a);
      //   }

      //   return totalPressure;
      //}

      //public double GetPressureFromDryBulbWetBulbAndHumidity(double dryBulbTemperature, double wetBulbTemperature, double humidity) {
      //   double evapHeat = moistureProperties.GetEvaporationHeat(wetBulbTemperature);
      //   double cpGas = gasProperties.GetSpecificHeatOfDryGas((dryBulbTemperature + wetBulbTemperature) / 2.0);
      //   double cpMoisture = moistureProperties.GetSpecificHeatOfVapor((dryBulbTemperature + wetBulbTemperature) / 2.0);
      //   double saturationHumidityAtWetBulb = (cpGas + cpMoisture * humidity) / evapHeat * (dryBulbTemperature - wetBulbTemperature) + humidity;
      //   //double pSaturationAtWetBulb = moistureProperties.GetSaturationPressure(wetBulbTemperature);
      //   //return (moistureGasMolarMassRatio + saturationHumidityAtWetBulb) * pSaturationAtWetBulb / saturationHumidityAtWetBulb;
      //   return GetPressureFromDewPointAndHumidity(wetBulbTemperature, saturationHumidityAtWetBulb);
      //}

      public static void Main() {
         //WaterProperties ha = new WaterProperties();
         //double tw = HumidGasCalculator.GetWetBulbTemperature(353.98, 0.1, 1.013e5);
         //double ps = WaterProperties.GetSaturationPressure(283.15);
         //Console.WriteLine(tw.ToString(), tw.ToString());
      }
   }
}