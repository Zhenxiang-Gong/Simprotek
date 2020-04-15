using System;

namespace Prosimo
{
	/// <summary>
	/// Summary description for Class3.
	/// </summary>
	public class IllegalVarValueException : Exception 	{
		public IllegalVarValueException(string message) : base(message) {
		}
	}

   public class InappropriateCalculatedValueException : Exception 	
   {
      public InappropriateCalculatedValueException(string message) : base(message) 
      {
      }
   }

   public class CalculationFailedException : Exception 	
   {
      public CalculationFailedException(string message) : base(message) {
      }
   }

   public class IllegalFunctionCallException : Exception 	{
      public IllegalFunctionCallException(string message) : base(message) {
      }
   }

}
