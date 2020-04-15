using System;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Prosimo.Plots
{
   public delegate void Plot2DChangedEventHandler(Plot2D plot);

   /// <summary>
	/// Summary description for Class1.
	/// </summary>
   [Serializable]
   public class Plot2D : Storable	
   {
      private const int CLASS_PERSISTENCE_VERSION = 1; 
      
      string name;
      private PlotVariable xVar;
      private PlotVariable yVar;
      private PlotParameter pVar;
      private bool includePVar;

      private CurveFamilyF curveFamily;
      
      public event Plot2DChangedEventHandler Plot2DChanged;

		public Plot2D(string name, PlotVariable xVar, PlotVariable yVar, PlotParameter parameter, bool includePVar) {
         this.name = name;
         this.xVar = xVar;
         this.yVar = yVar;
         this.pVar = parameter;
         this.includePVar = includePVar;
         GeneratePlot();
      }

//      public Plot2D(ProcessVarDouble xVar, ProcessVarDouble yVar, paramterVariable parameter) {
//         double min = xVar.Value * 0.8;
//         double max = xVar.Value * 1.2;
//         this.xVar = new PlotVariable(xVar, min, max);
//
//         min = yVar.Value * 0.8;
//         max = yVar.Value * 1.2;
//         this.yVar = new PlotVariable(yVar, min, max);
//      }
//
      public string Name 
      {
         get {return name;}
         set {name = value;} 
      }

      public PlotVariable PlotVariableX {
         get {return xVar;}
      }

      public PlotVariable PlotVariableY {
         get {return yVar;}
      }

      public PlotParameter PlotParameter 
      {
         get {return pVar;}
      }

      public bool IncludePlotParameter 
      {
         get {return includePVar;}
      }
      
      public ErrorMessage SetOxMin(double min) {
         ErrorMessage errorMsg = null;
         double originalValue = xVar.Min;
         try {
            xVar.Min = min;
            GeneratePlot();
            OnPlotChanged();
         }
         catch {
            xVar.Min = originalValue;
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified independent variable maximum value is too big");
         }
         return errorMsg;
      }

      public ErrorMessage SetOxMax(double max) {
         ErrorMessage errorMsg = null;
         double originalValue = xVar.Max;
         try {
            xVar.Max = max;
            GeneratePlot();
            OnPlotChanged();
         }
         catch {
            xVar.Max = originalValue;
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified independent variable maximum value is too small");
         }
         return errorMsg;
      }
      
      public ErrorMessage SetOyMin(double min) {
         ErrorMessage errorMsg = null;
         double originalValue = yVar.Min;
         try {
            yVar.Min = min;
            GeneratePlot();
            OnPlotChanged();
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified dependent variable maximum value is too small");
         }
         catch {
            yVar.Min = originalValue;
         }
         return errorMsg;
      }

      public ErrorMessage SetOyMax(double max) {
         ErrorMessage errorMsg = null;
         double originalValue = yVar.Max;
         try {
            yVar.Max = max;
            GeneratePlot();
            OnPlotChanged();
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified dependent variable maximum value is too big");
         }
         catch {
            yVar.Max = originalValue;
         }

         return errorMsg;
      }

      public ErrorMessage SetParameterMin(double min) 
      {
         ErrorMessage errorMsg = null;
         double[] originalValue = pVar.ParameterValues;
         try 
         {
            pVar.Min = min;
            GeneratePVarValues(min, pVar.Max, pVar.NumberOfValues);

            GeneratePlot();
            OnPlotChanged();
         }
         catch 
         {
            pVar.ParameterValues = originalValue;
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified independent variable maximum value is too big");
         }
         return errorMsg;
      }

      public ErrorMessage SetParameterMax(double max) 
      {
         ErrorMessage errorMsg = null;
         double[] originalValue = pVar.ParameterValues;
         try 
         {
            pVar.Max = max;
            GeneratePVarValues(pVar.Min, max, pVar.NumberOfValues);
            GeneratePlot();
            OnPlotChanged();
         }
         catch 
         {
            pVar.ParameterValues = originalValue;
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified independent variable maximum value is too small");
         }
         return errorMsg;
      }
      
      public ErrorMessage SetNumberOfParameterValues(int numOfValues) 
      {
         ErrorMessage errorMsg = null;
         
         if (numOfValues != 0 && numOfValues != pVar.NumberOfValues) 
         {
            double[] originalValue = pVar.ParameterValues;
            try 
            {
               GeneratePVarValues(pVar.Min, pVar.Max, numOfValues);
               GeneratePlot();
               OnPlotChanged();
            }
            catch 
            {
               pVar.ParameterValues = originalValue;
               errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "Specified independent variable maximum value is too small");
            }
         }
      
         return errorMsg;
      }
      
//      public ErrorMessage SetParameterValues(ArrayList values) 
//      {
//         ErrorMessage errorMsg = null;
//         double[] val = new double[values.Count];
//         for (int i = 0; i < values.Count-1; i++) 
//         {
//            val[i] = (double) values[i];
//         }
//
//         GeneratePlot();
//         pVar.ParameterValues = val;
//         OnPlotChanged();
//
//         return errorMsg;
//      }
//
      public CurveFamilyF CurveFamily 
      {
         get {return curveFamily;}
      }

      public bool IsValid 
      {
         get 
         {
            bool retValue = xVar.Variable.IsSpecifiedAndHasValue && !yVar.Variable.IsSpecifiedAndHasValue;
            if (includePVar) 
            {
               retValue = retValue && pVar.Variable.IsSpecifiedAndHasValue;
            }
            return retValue;
         }
      }

      private void GeneratePVarValues (double min, double max, int numOfValues) 
      {
         double[] val = new double[numOfValues];
         for (int i = 0; i < numOfValues; i++) 
         {
            val[i] = min + i * (max-min)/(numOfValues-1);
         }
      
         pVar.ParameterValues = val;
      }
      
      private void OnPlotChanged() {
         if (Plot2DChanged != null) {
            Plot2DChanged(this);
         }
      }

      private void GeneratePlot() {
         double orginalXVarValue = xVar.Variable.Value;

         double xValue;
         double yValue;
         double pValue = 0;
         int numOfCurves;
         int numOfPoints = 50;
         PointF[] dataPoints = new PointF[101];
         CurveF[] curves;
         if (includePVar) 
         {
            double orginalPVarValue = pVar.Variable.Value;
            double[] paramValues = pVar.ParameterValues;
            numOfCurves = paramValues.Length;
            curves = new CurveF[numOfCurves];
            for (int i = 0; i < numOfCurves; i++) 
            {
               pValue = paramValues[i];
               pVar.Variable.Owner.Specify(pVar.Variable, pValue);
               for (int j = 0; j < numOfPoints; j++) 
               {
                  xValue = xVar.Min + j * (xVar.Max - xVar.Min)/(numOfPoints - 1);
                  xVar.Variable.Owner.Specify(xVar.Variable, xValue);
                  yValue = yVar.Variable.Value;
                  dataPoints[j] = new PointF((float)xValue, (float)yValue);
               }
               curves[i] = new CurveF(pVar.Name, (float)pValue, dataPoints);
            }

            pVar.Variable.Owner.Specify(pVar.Variable, orginalPVarValue);
         }
         else 
         {
            curves = new CurveF[1];
            for (int j = 0; j < numOfPoints; j++) 
            {
               xValue = xVar.Min + j * (xVar.Max - xVar.Min)/(numOfPoints - 1);
               xVar.Variable.Owner.Specify(xVar.Variable, xValue);
               yValue = yVar.Variable.Value;
               dataPoints[j] = new PointF((float)xValue, (float)yValue);
            }
            curves[0] = new CurveF(yVar.Name, (float)pValue, dataPoints);
         }
         
         curveFamily = new CurveFamilyF(yVar.Name, yVar.Variable.Type, curves);

         xVar.Variable.Owner.Specify(xVar.Variable, orginalXVarValue);
      }

      protected Plot2D(SerializationInfo info, StreamingContext context) : base(info, context) 
      {
      }

      public override void SetObjectData() 
      {
         base.SetObjectData();
         int persistedClassVersion = (int)info.GetValue("ClassPersistenceVersionPlot2D", typeof(int));
         if (persistedClassVersion == 1) 
         {
            this.name = info.GetValue("Name", typeof(string)) as string;
            this.xVar = RecallStorableObject("XVar", typeof(PlotVariable)) as PlotVariable;
            this.yVar = RecallStorableObject("YVar", typeof(PlotVariable)) as PlotVariable;
            this.pVar = RecallStorableObject("PVar", typeof(PlotParameter)) as PlotParameter;
            this.includePVar = (bool) info.GetValue("IncludePVar", typeof(bool));
            this.curveFamily = RecallStorableObject("CurveFamily", typeof(CurveFamilyF)) as CurveFamilyF;
         }
      }

      [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context) 
      {
         base.GetObjectData(info, context);        
         info.AddValue("ClassPersistenceVersionPlot2D", CLASS_PERSISTENCE_VERSION, typeof(int));
         info.AddValue("Name", name, typeof(string));
         info.AddValue("XVar", xVar, typeof(PlotVariable));
         info.AddValue("YVar", yVar, typeof(PlotVariable));
         info.AddValue("PVar", pVar, typeof(PlotParameter));
         info.AddValue("IncludePVar", includePVar, typeof(bool));
         info.AddValue("CurveFamily", curveFamily, typeof(CurveFamilyF));
      }
   }
}
