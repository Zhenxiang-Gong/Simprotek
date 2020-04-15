using System;
using System.Runtime.InteropServices;

namespace Prosimo.UnitOperations.Calculator
{
	/// <summary>
	/// Summary description for ExpressionCalculator.
	/// </summary>
	public class ExpressionCalculator
	{
		public ExpressionCalculator()
		{
		}

      [DllImport("calcwin32.dll", EntryPoint="calculateExpression", CharSet=CharSet.Ansi)]
      public static extern double CalculateExpression(System.String expression);
   }
}
