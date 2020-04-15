using System;
using System.Collections; // to remove
using System.Drawing;


namespace ProsimoUI.Plots
{
	/// <summary>
	/// Summary description for Plots.
	/// </summary>
	public class PlotsConst
	{
      public const int MAJOR_TICKS_OX = 5;
      public const int MAJOR_TICKS_OY = 5;
      public const int MINOR_TICKS_OX = 5;
      public const int MINOR_TICKS_OY = 5;
      public const int MAJOR_TICK_LENGTH = 6;
      public const int MINOR_TICK_LENGTH = 3;

      public const float OX_LEFT_MARGIN = 60f;
      public const float OX_RIGHT_MARGIN = 20f;
      public const float OY_DOWN_MARGIN = 60f; 
      public const float OY_UP_MARGIN = 20f;
      public const float OY_CONST_1 = 20f; 
      public const float OY_CONST_2 = 10f; 
   }

   public delegate void ShowDomainCoordinateEventHandler(Object sender, PointF domainCoordinate);
   public delegate void SaveDomainCoordinateEventHandler(Object sender, PointF domainCoordinate);
   public delegate void PlotChangedEventHandler(AxisVariable ox, AxisVariable oy, PlotData data);

   public interface IPlot
   {
      event PlotChangedEventHandler PlotChanged;
      ArrayList GetOxVariables();
      ArrayList GetOyVariables();
      PlotData GetPlotData(AxisVariable xVar, AxisVariable yVar);
      void SetOxMin(float min); 
      void SetOxMax(float max); 
      void SetOyMin(float min); 
      void SetOyMax(float max); 
      void SetOxVariable(AxisVariable xVar);
      void SetOyVariable(AxisVariable yVar);
   }
}
