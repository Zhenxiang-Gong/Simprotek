using System;
using System.Drawing;

namespace ProsimoUI.HumidityChart
{
   public enum HCStateType
   {
      Undefined = 0,
      ProcessInput,
      ProcessOutput,
      CurrentState
   }

   /// <summary>
   /// Summary description for State.
   /// </summary>
   public class HumidityChartState
   {
      private Flowsheet flowsheet;
      
      private PointF variables;
      public PointF Variables
      {
         set
         {
            PointF val = value;
            float x = val.X;
            float y = val.Y;
            variables = new PointF(x, y);
         }
      }

      public float XValue
      {
         get {return variables.X;}
         set
         {
            float x = value;
            float y = variables.Y;
            variables = new PointF(x, y);
         }
      }

      public float YValue
      {
         get {return variables.Y;}
         set
         {
            float y = value;
            float x = variables.X;
            variables = new PointF(x, y);
         }
      }

      private HCStateType type;
      public HCStateType Type
      {
         get {return type;}
      }

      public HumidityChartState(HCStateType type, Flowsheet flowsheet)
      {
         this.flowsheet = flowsheet;
         variables = new PointF(0, 0);
         this.type = type;
      }

      public HumidityChartState(float xValue, float yValue, HCStateType type, Flowsheet flowsheet)
      {
         this.flowsheet = flowsheet;
         variables = new PointF(xValue, yValue);
         this.type = type;
      }

      public override string ToString()
      {
         string str = null;
         if (this.type.Equals(HCStateType.CurrentState))
         {
            str = "x = " + this.variables.X.ToString(this.flowsheet.NumericFormatString) +
               ", y = " + this.variables.Y.ToString(this.flowsheet.NumericFormatString);
         }
         else if (this.type.Equals(HCStateType.ProcessInput))
         {
            str = "IN: x = " + this.variables.X.ToString(this.flowsheet.NumericFormatString) + 
               ", y = " + this.variables.Y.ToString(this.flowsheet.NumericFormatString);
         }
         else if (this.type.Equals(HCStateType.ProcessOutput))
         {
            str = "OUT: x = " + this.variables.X.ToString(this.flowsheet.NumericFormatString) + 
               ", y = " + this.variables.Y.ToString(this.flowsheet.NumericFormatString);
         }
         return str;         
      }
	}
}
