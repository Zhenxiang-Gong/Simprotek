using System;

namespace Prosimo.UnitOperations
{
   public class IllegalAttachmentException : Exception 	{
      public IllegalAttachmentException(string message) : base(message) {
      }
   }

   public class OverSpecificationException : Exception 	{
      public OverSpecificationException(string message) : base(message) {
      }
   }

   public class InappropriateSpecifiedValueException : Exception 	{
      public InappropriateSpecifiedValueException(string message) : base(message) {
      }
   }

   //public class CalculationFailedException : Exception 	{
   //   public CalculationFailedException(string message) : base(message) {
   //   }
   //}
}
