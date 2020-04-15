using System;

namespace ProsimoUI.HumidityChart
{
	/// <summary>
	/// Summary description for HumidityChartProcess.
	/// </summary>
	public class HumidityChartProcess
	{
      private HumidityChartState input;
      public HumidityChartState Input
      {
         get {return input;}
         set {input = value;}
      }

      private HumidityChartState output;
      public HumidityChartState Output
      {
         get {return output;}
         set {output = value;}
      }
      
      public HumidityChartProcess(Flowsheet flowsheet)
		{
         input = new HumidityChartState(HCStateType.ProcessInput, flowsheet);
         output = new HumidityChartState(HCStateType.ProcessOutput, flowsheet);
		}

      public HumidityChartProcess(HumidityChartState input, HumidityChartState output, Flowsheet flowsheet)
      {
         if (input.Type.Equals(HCStateType.ProcessInput) &&
            output.Type.Equals(HCStateType.ProcessOutput))
         {
            this.input = input;
            this.output = output;
         }
         else
         {
            input = new HumidityChartState(HCStateType.ProcessInput, flowsheet);
            output = new HumidityChartState(HCStateType.ProcessOutput, flowsheet);
         }
      }

      public override string ToString()
      {
         return (this.input.ToString() + "    " + this.output.ToString());
      }
	}
}
