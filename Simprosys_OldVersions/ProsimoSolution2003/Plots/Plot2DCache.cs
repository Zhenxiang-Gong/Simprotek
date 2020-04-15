using System;
using System.Collections;

namespace Prosimo.Plots
{
   /// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Plot2DCache 	{
      string name;
      private ProcessVarDouble xVar;
      private ProcessVarDouble yVar;
      private ProcessVarDouble pVar;

//      private double xVarMin;
//      private double xVarMax;
//      private double yVarMin;
//      private double yVarMax;
//      
//      private ArrayList pVarValues;
      private bool includePVar;

      private PlotCatalog catalog;

		public Plot2DCache(PlotCatalog catalog) {
         this.catalog = catalog;
//         pVarValues = new ArrayList();
		}

//      public Plot2DCache(PlotCatalog catalog, Plot2D plot) {
//         this.name = plot.Name;
//         this.catalog = catalog;
//         this.xVar = plot.XVariable.Variable;
//         this.yVar = plot.YVariable.Variable;
//         this.pVar = plot.ParameterVariable.Variable;
//         this.xVarMin = plot.XVariable.Min;
//         this.xVarMax = plot.XVariable.Max;
//         this.yVarMin = plot.YVariable.Min;
//         this.yVarMax = plot.YVariable.Max;
//         double[] pValues = plot.ParameterVariable.ParameterValues;
//         pVarValues = new ArrayList();
//         foreach (double d in pValues) 
//         {
//            pVarValues.Add(d);
//         }
//      }
//
      public string Name 
      {
         get {return name;}
         set {name = value;} 
      }

      public ProcessVarDouble XVar {
         get {return xVar;}
         set {xVar = value;}
      }

      public ProcessVarDouble YVar 
      {
         get {return yVar;}
         set {yVar = value;}
      }

      public ProcessVarDouble PVar 
      {
         get {return pVar;}
         set {pVar = value;}
      }
      
//      public double XVarMin {
//         get {return xVarMin;}
//         set {xVarMin = value;}
//      }
//
//      public double XVarMax 
//      {
//         get {return xVarMax;}
//         set {xVarMax = value;}
//      }
//      
//      public double YVarMin 
//      {
//         get {return yVarMin;}
//         set {yVarMin = value;}
//      }
//
//      public double YVarMax 
//      {
//         get {return yVarMax;}
//         set {yVarMax = value;}
//      }
//      
//      public ArrayList PVarValues 
//      { 
//         get {return pVarValues;}
//         set {pVarValues = value;}
//      }
//
      public bool IncludeParameterVariable 
      {
         get {return includePVar;}
         set {includePVar = value;}
      }

      public ErrorMessage FinishSpecifications(string name) 
      {
         ErrorMessage errorMsg = null;

         if (catalog.IsInCatalog(name)) 
         {
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "The plot catalog has already got a plot named " + name);
         }
//         if (xVarMin > xVarMax) 
//         {
//            errorMsg = new ErrorMessage(ErrorType.Error, "Inappropriate Specification", "The minimum value of the independent variable must be smaller than its maximum value");
//         }
//         else if (yVarMin > yVarMax) 
//         {
//            errorMsg = new ErrorMessage(ErrorType.Error, "Inappropriate Specification", "The minimum value of the dependent variable must be smaller than its maximum value");
//         }
//         else if ((double)pVarValues[0] > (double)pVarValues[pVarValues.Count-1]) 
//         {
//            errorMsg = new ErrorMessage(ErrorType.Error, "Inappropriate Specification", "The minimum value of the parameter variable must be smaller than its maximum value");
//         }
//         else if (xVar == null) 
//         {
//            errorMsg = new ErrorMessage(ErrorType.Error, "Inappropriate Specification", "The independent variable must be selected");
//         }
//         else if (yVar == null) 
//         {
//            errorMsg = new ErrorMessage(ErrorType.Error, "Inappropriate Specification", "The dependent variable must be selected");
//         }
         else if (includePVar && pVar == null) 
         {
            errorMsg = new ErrorMessage(ErrorType.SimpleGeneric, "Inappropriate Specification", "The parameter variable must be selected");
         }
         else 
         {
            double xVarMin = 0.8 * xVar.Value;
            double xVarMax = 1.2 * xVar.Value;
            double yVarMin = 0.8 * yVar.Value;
            double yVarMax = 1.2 * yVar.Value;
            PlotVariable xV = new PlotVariable(xVar, xVarMin, xVarMax);
            PlotVariable yV = new PlotVariable(yVar, yVarMin, yVarMax);
            
            PlotParameter pV = null;
            
            if (pVar != null) 
            {
               double[] values = {pVar.Value};

               pV = new PlotParameter(pVar, values);
            }
            Plot2D plot = new Plot2D(name, xV, yV, pV, includePVar);
            catalog.AddPlot2D(plot);
         }
         return errorMsg;
      }
      
   }
}
