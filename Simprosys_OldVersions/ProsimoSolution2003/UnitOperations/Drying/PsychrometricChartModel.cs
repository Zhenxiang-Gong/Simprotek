using System;
using System.Drawing;
using System.Collections;

using Prosimo.UnitOperations.ProcessStreams;
using Prosimo.ThermalProperties;//need it for humid gas calculator
using Prosimo.Plots;

namespace Prosimo.UnitOperations.Drying {
   public enum HCType {ChartOnly = 0, State, Process};

   public delegate void HumidityChartChangedEventHandler(PlotVariable ox, PlotVariable oy, HumidityChartData data);
   public delegate void HCTypeChangedEventHandler(HCType hcType);

   /// <summary>
   /// PsychrometricChartModel is the model which generates the humidity chart.
   /// </summary>
   public class PsychrometricChartModel : UnitOperation {
      private DryingGasStream inputStream;
      private DryingGasStream outputStream;
      private DryingGasStream currentStream;
      private PlotVariable xVar;
      private PlotVariable yVar;

      private ProcessVarDouble pressure;
      private CurveFamilyF curveFamily;
      private HumidityChartData humidityChartData;
      
      private HCType hcType;
      
      public HCType HCType {
         get {return hcType;}
      }

      public event HumidityChartChangedEventHandler HumidityChartChanged;
      public event HCTypeChangedEventHandler HCTypeChanged;

      public PsychrometricChartModel(string name, DryingGasStream input, DryingGasStream output, DryingGasStream current, UnitOpSystem uoSys) : base (name, uoSys) {
         this.inputStream = input;
         inputStream.DownStreamOwner = this;
         this.outputStream = output;
         outputStream.UpStreamOwner = this;
         pressure = new ProcessVarDouble(StringConstants.PRESSURE, PhysicalQuantity.Pressure, 1.0132685e5, VarState.Specified, this);
         this.currentStream = current;
         currentStream.DownStreamOwner = this;

         inputStream.Pressure = pressure;
         outputStream.Pressure = pressure;
         currentStream.Pressure = pressure;

         ProcessVarDouble dryBulbTemp = new ProcessVarDouble(StringConstants.DRY_BULB_TEMPERATURE, PhysicalQuantity.Temperature, 293.15, VarState.Specified, this);
         xVar = new PlotVariable(dryBulbTemp, 283.15, 373.15);
         ProcessVarDouble humidity = new ProcessVarDouble(StringConstants.HUMIDITY, PhysicalQuantity.MoistureContent, 0.1, VarState.Specified, this);
         yVar = new PlotVariable(humidity, 0.0, 0.3);
         GenerateHumidityChartData();
         inletStreams.Add(inputStream);
         outletStreams.Add(outputStream);
         InitializeStreams();

         this.hcType = HCType.ChartOnly;
      }

      private void InitializeStreams() {
         currentStream.Temperature.Value = 298.15;
         currentStream.Specify(currentStream.MoistureContentDryBase, 0.009);

         inputStream.Temperature.Value = 363.15;
         inputStream.MoistureContentDryBase.Value = 0.009;
         
         outputStream.Temperature.Value = 323.15;
         HasBeenModified(true);
      }

      public ErrorMessage SpecifyHCType (HCType aValue)  {
         ErrorMessage retMsg = null;
         if (aValue != hcType) {
            HCType oldValue = hcType;
            try 
            {
               hcType = aValue;
               HasBeenModified(true);
            }
            catch (Exception e) 
            {
               hcType = oldValue;
               retMsg = HandleException(e);
               OnHCTypeChanged(hcType);
            } 
         }
         return retMsg;
      }

      protected void OnHumidityChartChanged(PlotVariable ox, PlotVariable oy, HumidityChartData data) {
         if (HumidityChartChanged != null) {
            HumidityChartChanged(ox, oy, data);
         }
      }
      
      protected void OnHCTypeChanged(HCType hcType) {
         if (HCTypeChanged != null) {
            HCTypeChanged(hcType);
         }
      }

      private void GenerateHumidityChartData() {
         CurveFamilyF[] curveFamilies = new CurveFamilyF[2];
         curveFamilies[0] = GenerateRelativeHumidityFamily();
         curveFamilies[1] = GenerateAdiabaticSaturationFamily();
         humidityChartData = new HumidityChartData("Humidity Chart Data", curveFamilies);
      }

      public DryingGasStream ProcessInputStream {
         get {return inputStream;}
      }
      
      public DryingGasStream ProcessOutputStream {
         get {return outputStream;}
      }
      
      public DryingGasStream StateStream {
         get {return currentStream;}
      }
      
      //public CurveFamilyF[] CurveFamilies {
      //   get {return curveFamilies;}
      //}
      
      public ArrayList GetOxVariables() {
         ArrayList list = new ArrayList();
         list.Add(xVar);
         return list;
      }

      public ArrayList GetOyVariables() {
         ArrayList list = new ArrayList();
         list.Add(yVar);
         return list;
      }
      
      public ProcessVarDouble Pressure {
         get {return pressure;}
      }
      
      public ErrorMessage SetCurrentState(PointF pt) {
         ErrorMessage retMsg = null;
         string msg = CheckPointInRange(pt);
         if (msg == null) {
            retMsg = SetState(currentStream, pt);
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
         }

         if (retMsg != null) 
         {
            OnHCTypeChanged(hcType);
         }

         return retMsg;
      }
      
      public ErrorMessage SetInputState(PointF pt) {
         ErrorMessage retMsg = null;
         string msg = CheckInputPoint(pt);
         if (msg == null) {
            retMsg = SetState(inputStream, pt);
            if (inputStream.Humidity.HasValue) {
               pt.Y = (float)inputStream.Humidity.Value;
            }
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
         }

         if (retMsg != null) 
         {
            OnHCTypeChanged(hcType);
         }

         return retMsg;
      }

      public ErrorMessage SetOutputState(PointF pt) {
         ErrorMessage retMsg = null;
         string msg = CheckOutputPoint(pt);
         if (msg == null) {
            retMsg = SetState(outputStream, pt);
            if (outputStream.Humidity.HasValue) {
               pt.Y = (float)outputStream.Humidity.Value;
            }
         }
         else {
            retMsg = new ErrorMessage(ErrorType.SimpleGeneric, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
         }

         if (retMsg != null) 
         {
            OnHCTypeChanged(hcType);
         }

         return retMsg;
      }
      
      public ErrorMessage SetState(DryingGasStream gasStream, PointF pt) {
         double p = pressure.Value;
         double temperature = (double)pt.X;
         double humidity = (double)pt.Y;
         double wetBulb;
         double dewPoint;
         double relativeHumidity;
         
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         Hashtable varAndValueTable = new Hashtable();
         if (gasStream.Temperature.IsSpecified) {
            if (gasStream.Humidity.IsSpecified) {
               varAndValueTable.Add(gasStream.MoistureContentDryBase, humidity);
            }
            else if (gasStream.WetBulbTemperature.IsSpecified) {
               wetBulb = humidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(temperature, humidity, p);
               varAndValueTable.Add(gasStream.WetBulbTemperature, wetBulb);
            }
            else if (gasStream.DewPoint.IsSpecified) {
               dewPoint = humidGasCalculator.GetDewPointFromHumidityAndPressure(humidity, p);
               varAndValueTable.Add(gasStream.DewPoint, dewPoint);
            }
            else if (gasStream.RelativeHumidity.IsSpecified) {
               relativeHumidity = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(temperature, humidity, p);
               varAndValueTable.Add(gasStream.RelativeHumidity, relativeHumidity);
            }
            varAndValueTable.Add(gasStream.Temperature, temperature);
         }
         else if (gasStream.WetBulbTemperature.IsSpecified) {
            if (outputStream.DewPoint.IsSpecified) {
               dewPoint = humidGasCalculator.GetDewPointFromHumidityAndPressure(humidity, p);
               varAndValueTable.Add(gasStream.DewPoint, dewPoint);
            }
            else if (gasStream.RelativeHumidity.IsSpecified) {
               relativeHumidity = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(temperature, humidity, p);
               varAndValueTable.Add(currentStream.RelativeHumidity, relativeHumidity);
            }
            wetBulb = humidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(temperature, humidity, p);
            varAndValueTable.Add(gasStream.WetBulbTemperature, wetBulb);
         }
         else if (gasStream.DewPoint.IsSpecified) {
            if (gasStream.RelativeHumidity.IsSpecified) {
               relativeHumidity = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(temperature, humidity, pressure.Value);
               varAndValueTable.Add(outputStream.RelativeHumidity, relativeHumidity);
            }
            dewPoint = humidGasCalculator.GetDewPointFromHumidityAndPressure(humidity, pressure.Value);
            varAndValueTable.Add(gasStream.DewPoint, dewPoint);
         }

         return gasStream.Specify(varAndValueTable);
      }
      
      private string CheckPointInRange(PointF pt) {
         bool retValue = true;
         string retMsg = null;
         if (pt.X < xVar.Min || pt.X > xVar.Max || pt.Y < yVar.Min || pt.Y > yVar.Max) {
            retValue = false;
         }
         else {
            double p = pressure.Value;
            double temperature = (double)pt.X;
            double humidity = (double)pt.Y;
            HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
            double dewPoint = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature, 1.0);
            double humiditySat = humidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint, p);
            if (humidity > humiditySat) {
               retValue = false;
            }
         }
         if (retValue == false) {
            retMsg = "Specified state is out of range. It is reverted back to the original state";
         }

         return retMsg;
      }

      private string CheckInputPoint(PointF pt) {
         string retMsg = CheckPointInRange(pt);
         double inputTemp = (double)pt.X;
         
         if (retMsg == null && inputTemp < outputStream.Temperature.Value) { 
            retMsg = "Specified input temperature is smaller than output temperature. It is reverted back to the original value";
         }
         return retMsg;
      }

      private string CheckOutputPoint(PointF pt) {
         string retMsg = CheckPointInRange(pt);
         double outputTemp = (double)pt.X;
         if (retMsg == null && outputTemp > inputStream.Temperature.Value) { 
            retMsg = "Specified output temperature is greater than input temperature. It is reverted back to the original value";
         }
         return retMsg;
      }

      public PointF GetProcessInputState() {
         return new PointF((float)this.inputStream.Temperature.Value, (float)this.inputStream.Humidity.Value);
      }                                            

      public PointF GetProcessOutputState() {
         return new PointF((float)this.outputStream.Temperature.Value, (float)this.outputStream.Humidity.Value);
      }
      
      public CurveFamilyF GetProcessStates() {
         return curveFamily;
      }

      public PointF GetCurrentState() {
         return new PointF((float)currentStream.Temperature.Value, (float)currentStream.Humidity.Value);
      }                                            

      public void SetOxVariable(PlotVariable xVar) {
         this.xVar = xVar;
         GenerateHumidityChartData();
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }

      public void SetOyVariable(PlotVariable yVar) {
         this.yVar = yVar;
         GenerateHumidityChartData();
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }
      
      public void SetOxMin(double min) {
         double originalValue = xVar.Min;
         try {
            xVar.Min = min;
            GenerateHumidityChartData();
         }
         catch {
            xVar.Min = originalValue;
         }
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }

      public void SetOxMax(double max) {
         double originalValue = xVar.Max;
         try {
            xVar.Max = max;
            GenerateHumidityChartData();
         }
         catch {
            xVar.Max = originalValue;
         }
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }
      
      public void SetOyMin(double min) {
         double originalValue = yVar.Min;
         try {
            yVar.Min = min;
            GenerateHumidityChartData();
         }
         catch {
            yVar.Min = originalValue;
         }
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }

      public void SetOyMax(double max) {
         double originalValue = yVar.Max;
         try {
            yVar.Max = max;
            GenerateHumidityChartData();
         }
         catch {
            yVar.Max = originalValue;
         }
         this.OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }
      
      private CurveFamilyF GenerateRelativeHumidityFamily() {
         double dewPoint;
         double humidity;
         double temperature;
         double maxTemp;
         CurveF[] curves = new CurveF[16];
         double relativeHumidity;
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         for (int i = 0; i < 16; i++) {
            //from 0.0% to 10%--interval 2%
            if (i < 5) {
               relativeHumidity = (i+1) * 0.02;
            }
               //from 10 to 50%--interval 5%
            else if (i < 9) {
               relativeHumidity = 0.1 + (i-4) * 0.05;
            }
               //from 30 to 100%--interval 10%
            else {
               relativeHumidity = 0.3 + (i-8) * 0.1;
            }
            
            dewPoint = humidGasCalculator.GetDewPointFromHumidityAndPressure(yVar.Max, pressure.Value);
            maxTemp = humidGasCalculator.GetDryBulbFromDewPointAndRelativeHumidity(dewPoint, relativeHumidity); 
            if (maxTemp > xVar.Max) {
               maxTemp = xVar.Max;
            }
            PointF[] dataPoints = new PointF[101];
            for (int j = 0; j <= 100; j++) {
               temperature = xVar.Min + j * (maxTemp - xVar.Min)/100;
               dewPoint = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature, relativeHumidity);
               humidity = humidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint, (double)pressure.Value);
               dataPoints[j] = new PointF((float)temperature, (float)humidity);
            }
            curves[i] = new CurveF(StringConstants.GetTypeName(StringConstants.RELATIVE_HUMIDITY), (float)relativeHumidity, dataPoints);
         }
         
         CurveFamilyF curveFamily = new CurveFamilyF(StringConstants.GetTypeName(StringConstants.RELATIVE_HUMIDITY), PhysicalQuantity.Fraction, curves);
         return curveFamily;
      }

      private CurveFamilyF GenerateAdiabaticSaturationFamily() {
         double dewPoint;
         double ysat;
         double wetBulb;
         double temperature;
         double ih;
         double tsat;
         double maxTemp;
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         double p = (double) pressure.Value;
         double cg = humidGasCalculator.GetSpecificHeatOfDryGas();
         double cv = humidGasCalculator.GetSpecificHeatOfVapor();
         double r0 = humidGasCalculator.GetEvaporationHeat(273.15);
         double dewPoint1 = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(xVar.Max, 1.0);
         double dewPoint2 = humidGasCalculator.GetDewPointFromHumidityAndPressure(yVar.Max, p);
         double dewPointMin = Math.Min(dewPoint1, dewPoint2);
         int numOfCurves = (int)((dewPointMin - xVar.Min)/5.0) + 1;
         CurveF[] curves = new CurveF[numOfCurves];
         double y;
         for (int i = 0; i < numOfCurves; i++) {
            tsat = xVar.Min + i * 5.0;
            dewPoint = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(tsat, 1.0);
            ysat = humidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint, p);
            wetBulb = humidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(tsat, ysat, p);
            PointF[] dataPoints = new PointF[11];
            maxTemp = humidGasCalculator.GetDryBulbFromWetBulbHumidityAndPressure(wetBulb, 0.0, p);
            if (maxTemp > xVar.Max) {
               maxTemp = xVar.Max;
            }
            if (ysat > yVar.Max) {
               tsat = humidGasCalculator.GetDryBulbFromWetBulbHumidityAndPressure(wetBulb, yVar.Max, p);
            }
            ih = (cg + cv*ysat)*(tsat-273.15) + r0*ysat;
            for (int j = 0; j <= 10; j++) {
               temperature =  tsat + (maxTemp - tsat)/10.0 * j;
               //iso-enthalpy line
               y = (ih - cg * (temperature-273.15))/(r0 + cv *(temperature - 273.15));
               dataPoints[j] = new PointF((float)temperature, (float)y);
            }
            curves[i] = new CurveF(StringConstants.GetTypeName(StringConstants.ADIABATIC_SATURATION), (float)tsat, dataPoints);
         }

         CurveFamilyF curveFamily = new CurveFamilyF(StringConstants.GetTypeName(StringConstants.ADIABATIC_SATURATION), PhysicalQuantity.Temperature, curves);
         return curveFamily;
      }

      private CurveF GenerateHumidHeatCurve() {
         double humidity;
         double humidHeat;
         PointF[] dataPoints = new PointF[10];
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         for (int i = 0; i < 10; i++) {
            humidity = yVar.Min + (yVar.Max - yVar.Min)/10.0 * i;
            humidHeat = humidGasCalculator.GetHumidHeat(humidity);
            dataPoints[i] = new PointF((float)humidity, (float)humidHeat);
         }
         return new CurveF(StringConstants.GetTypeName(StringConstants.HUMID_HEAT), 273.15f, dataPoints);
      }

      private CurveF GenerateEvaporationHeatCurve() {
         double temperature;
         double evapHeat;
         PointF[] dataPoints = new PointF[10];
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         for (int i = 0; i < 10; i++) {
            temperature = (xVar.Max-xVar.Min)/10.0 * i;
            evapHeat = humidGasCalculator.GetEvaporationHeat(temperature);
            dataPoints[i] = new PointF((float)temperature, (float)evapHeat);
         }
         return new CurveF(StringConstants.GetTypeName(StringConstants.EVAPORATION_HEAT), 0.1f, dataPoints);
      }
      
      private CurveF GenerateSpecificVolumeDryAirCurve() {
         double temperature;
         double specificVolumeDryAir;
         PointF[] dataPoints = new PointF[10];
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         for (int i = 0; i < 10; i++) {
            temperature = (xVar.Max - xVar.Min)/10.0 * i;
            specificVolumeDryAir = humidGasCalculator.GetHumidVolume(temperature, 0.0, (double)pressure.Value);
            dataPoints[i] = new PointF((float)temperature, (float)specificVolumeDryAir);
         }
         return new CurveF(StringConstants.GetTypeName(StringConstants.SPECIFIC_VOLUME_DRY_AIR), 0.1f, dataPoints);
      }

      private CurveF GenerateSaturationVolumeCurve() {
         double temperature;
         double dewPoint;
         double humidity;
         double saturationVolume;
         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
         PointF[] dataPoints = new PointF[10];
         for (int i = 0; i < 10; i++) {
            temperature = (xVar.Max - xVar.Min)/10.0 * i;
            dewPoint = humidGasCalculator.GetDewPointFromDryBulbAndRelativeHumidity(temperature, 1.0);
            humidity = humidGasCalculator.GetHumidityFromDewPointAndPressure(dewPoint, (double)pressure.Value);
            saturationVolume = humidGasCalculator.GetHumidVolume(temperature, humidity, (double)pressure.Value);
            dataPoints[i] = new PointF((float)temperature, (float)saturationVolume);
         }
         return new CurveF(StringConstants.GetTypeName(StringConstants.SATURATION_VOLUME), 0.1f, dataPoints);
      }
   
      public HumidityChartData GetPlotData(PlotVariable xVar, PlotVariable yVar) {
         return humidityChartData;
      }

//      protected override bool IsSolveReady() {
//         return true;
//      }
//
      public override void Execute(bool propagate) {
         PreSolve();
         GenerateHumidityChartData();
         OnHumidityChartChanged(xVar, yVar, humidityChartData);
         if (hcType == HCType.Process) 
         {
            Solve();
            GenerateProcessCurves();
         }
         PostSolve();
      }

      private void Solve() 
      {  
         double p = pressure.Value;
         double tg1 = inputStream.Temperature.Value;
         double y1 = inputStream.Humidity.Value;
         double tw1 = inputStream.WetBulbTemperature.Value;
         double td1 = inputStream.DewPoint.Value;
         double fy1 = inputStream.RelativeHumidity.Value;

         double tg2 = outputStream.Temperature.Value;
         double y2 = outputStream.Humidity.Value;
         double tw2 = outputStream.WetBulbTemperature.Value;
         double td2 = outputStream.DewPoint.Value;
         double fy2 = outputStream.RelativeHumidity.Value;

         double ih = 0;

         if (inputStream.Pressure.Value != p) 
         {
            Calculate(inputStream.Pressure, p);
         }
         if (outputStream.Pressure.Value != p) 
         {
            Calculate(outputStream.Pressure, p);
         }

         HumidGasCalculator humidGasCalculator = GetHumidGasCalculator();
            
         if (tg1 != Constants.NO_VALUE && y1 != Constants.NO_VALUE) 
         {
            //wetBulb = humidGasCalculator.GetWetBulbFromDryBulbHumidityAndPressure(tg1, y1, p);
            ih = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tg1, y1, p);
            if (tg2 != Constants.NO_VALUE) 
            {
               y2 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg2, p);
               if (y2 <= 0.0) 
               {
                  y2 = 1.0e-6;
               }

               Calculate(outputStream.MoistureContentDryBase, y2);
               solveState = SolveState.Solved;
            }
            else if (y2 != Constants.NO_VALUE) 
            {
               tg2 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y2, p);
               Calculate(outputStream.Temperature, tg2);
               solveState = SolveState.Solved;
            }
            else if (td2 != Constants.NO_VALUE) 
            {
               y2 = humidGasCalculator.GetHumidityFromDewPointAndPressure(td2, p);
               tg2 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y2, p);
               Calculate(outputStream.Temperature, tg2);
               solveState = SolveState.Solved;
            }
            else if (fy2 != Constants.NO_VALUE) 
            {
               double fy_temp = 0;
               double delta = 10.0;
               double totalDelta = delta;
               tg2 = tg1 - delta;
               bool negativeLastTime = false;
            
               int counter = 0;
               do 
               {
                  counter++;
                  y2 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg2, p);
                  fy_temp = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg2, y2, p);
                  if (fy2 > fy_temp) 
                  {
                     if (negativeLastTime) 
                     {
                        delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     }
                     totalDelta += delta;
                     negativeLastTime = false;
                  }
                  else if (fy2 < fy_temp) 
                  {
                     delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     totalDelta -= delta;
                     negativeLastTime = true;
                  }
                  tg2 = tg1 - totalDelta;
               } while (Math.Abs(fy2 - fy_temp) > 1.0e-6 && counter <= 200);

               if (counter < 200) 
               {
                  Calculate(outputStream.Temperature, tg2);
                  solveState = SolveState.Solved;
               }
            }

            double fy = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg2, y2, p);
            if (fy > 1.0) 
            {
               solveState = SolveState.NotSolved;
               string msg = "Specified input state makes the relative humidity of the output greater than 1.0.";
               throw new InappropriateSpecifiedValueException(msg);
            }

         }
         else if (tg2 != Constants.NO_VALUE && y2 != Constants.NO_VALUE) 
         {
            ih = humidGasCalculator.GetHumidEnthalpyFromDryBulbHumidityAndPressure(tg2, y2, p);
            if (tg1 != Constants.NO_VALUE) 
            {
               y1 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg1, p);
               Calculate(inputStream.MoistureContentDryBase, y1);
               solveState = SolveState.Solved;
            }
            else if (y1 != Constants.NO_VALUE) 
            {
               tg1 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y1, p);
               Calculate(inputStream.Temperature, tg1);
               solveState = SolveState.Solved;
            }
            else if (td1 != Constants.NO_VALUE) 
            {
               y1 = humidGasCalculator.GetHumidityFromDewPointAndPressure(td1, p);
               tg1 = humidGasCalculator.GetDryBulbFromHumidEnthalpyHumidityAndPressure(ih, y1, p);
               Calculate(inputStream.Temperature, tg1);
               solveState = SolveState.Solved;
            }
            else if (fy1 != Constants.NO_VALUE) 
            {
               double fy_temp = 0;
               double delta = 10.0;
               double totalDelta = delta;
               tg1 = tg2 + delta;
               bool negativeLastTime = false;
            
               int counter = 0;
               do 
               {
                  counter++;
                  y1 = humidGasCalculator.GetHumidityFromHumidEnthalpyTemperatureAndPressure(ih, tg1, p);
                  fy_temp = humidGasCalculator.GetRelativeHumidityFromDryBulbHumidityAndPressure(tg1, y1, p);
                  if (fy1 < fy_temp) 
                  {
                     if (negativeLastTime) 
                     {
                        delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     }
                     totalDelta += delta;
                     negativeLastTime = false;
                  }
                  else if (fy1 > fy_temp) 
                  {
                     delta /= 2.0; //testing finds delta/2.0 is almost optimal
                     totalDelta -= delta;
                     negativeLastTime = true;
                  }
                  tg1 = tg2 + totalDelta;
               } while (Math.Abs(fy1 - fy_temp) > 1.0e-6 && counter <= 200);

               if (counter < 200) 
               {
                  Calculate(inputStream.Temperature, tg1);
                  solveState = SolveState.Solved;
               }
            }
         }
      }
      
      private void GenerateProcessCurves() 
      {
         double enthalpy = inputStream.SpecificEnthalpy.Value;
         PointF[] dataPoints = {new PointF((float)inputStream.Temperature.Value, (float)inputStream.Humidity.Value),
                                  new PointF((float)outputStream.Temperature.Value, (float)outputStream.Humidity.Value)
                               };
         enthalpy = inputStream.SpecificEnthalpy.Value;
         CurveF[] curves = {new CurveF("Iso-Enthalpy", (float)enthalpy, dataPoints)};
         if (curveFamily == null) 
         {
            curveFamily = new CurveFamilyF("Adiabatic Saturation", PhysicalQuantity.Temperature, curves);
         }
         else 
         {
            curveFamily.Curves = curves;
         }
      }

      //protected override void PostSolve(bool propagate) 
      protected override void PostSolve() 
      {
         isBeingExecuted = false;
         if (hcType == HCType.Process) 
         {
            foreach (DryingGasStream dgs in InOutletStreams) {
               if (dgs.HasVarCalculated) {
                  dgs.Execute(false);
               }
               if (dgs.Pressure.HasValue && dgs.Temperature.HasValue && dgs.WetBulbTemperature.HasValue &&
                  dgs.DewPoint.HasValue && dgs.Humidity.HasValue && dgs.RelativeHumidity.HasValue) {
                  dgs.SolveState = SolveState.Solved;
                  dgs.OnSolveComplete(SolveState.Solved);
               }
            }
         }
         else {
            if (currentStream.Pressure.HasValue && currentStream.Temperature.HasValue && 
               currentStream.WetBulbTemperature.HasValue && currentStream.DewPoint.HasValue 
               && currentStream.Humidity.HasValue && currentStream.RelativeHumidity.HasValue) {
               currentStream.SolveState = SolveState.Solved;
               currentStream.OnSolveComplete(SolveState.Solved);
            }
         }

         OnHCTypeChanged(hcType);
      }
   }
}


   /*public void SpecifyProcessPressure (double aValue) {
         pressure.Value = aValue;
         
         Specify(inputStream.Pressure, aValue);
         Specify(outputStream.Pressure, aValue);

         GenerateHumidityChartData();
         OnPressureChanged();
         OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }

      public void SpecifyCurrentPressure (double aValue) {
         pressure.Value = aValue;
         
         Specify(currentStream.Pressure, aValue);

         GenerateHumidityChartData();
         OnPressureChanged();
         OnHumidityChartChanged(xVar, yVar, humidityChartData);
      }*/
