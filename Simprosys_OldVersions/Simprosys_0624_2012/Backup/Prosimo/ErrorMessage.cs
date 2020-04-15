using System;
using System.Collections;

namespace Prosimo {
   public enum ErrorType {
      SimpleGeneric = 0,
      SpecifiedValueOutOfRange,
      SpecifiedValueCausingOtherVarsOutOfRange,
      SpecifiedValueCausingOtherVarsInappropriate,
   };

   public class ErrorMessage {
      private ErrorType type;
      private string title;
      private string message;

      private ProcessVarsAndValues varsAndValues;

      public ErrorMessage(ErrorType type, string title, string message) {
         this.type = type;
         this.title = title;
         this.message = message;
      }

      public ErrorType Type {
         get { return type; }
      }

      public string Title {
         get { return title; }
      }

      public string Message {
         get { return message; }
         set { message = value; }
      }

      public ProcessVarsAndValues ProcessVarsAndValues {
         get { return varsAndValues; }
         set { varsAndValues = value; }
      }

      public void AddVarAndItsRecommendedValue(ProcessVarDouble pvd, double pvdValue) {
         CreateProcessVarsAndValuesIfNecessary();
         varsAndValues.AddVarAndItsRecommendedValue(pvd, pvdValue);
      }

      public void AddVarAndItsRecommendedValue(ProcessVarInt pvi, int pviValue) {
         CreateProcessVarsAndValuesIfNecessary();
         varsAndValues.AddVarAndItsRecommendedValue(pvi, pviValue);
      }

      public void AddVarAndItsRange(ProcessVarDouble pvd, DoubleRange doubleRange) {
         CreateProcessVarsAndValuesIfNecessary();
         varsAndValues.AddVarAndItsRange(pvd, doubleRange);
      }

      public void AddVarAndItsRange(ProcessVarInt pvi, IntRange intRange) {
         CreateProcessVarsAndValuesIfNecessary();
         varsAndValues.AddVarAndItsRange(pvi, intRange);
      }

      //      public void AddVarAndItsMinAndMaxValues(ProcessVarDouble pvd, double minValue, double maxValue) 
      //      {
      //         CreateProcessVarsAndValuesIfNecessary();
      //         varsAndValues.AddVarAndItsMinAndMaxValues(pvd, minValue, maxValue);
      //      }
      //
      //      public void AddVarAndItsMinAndMaxValues(ProcessVarInt pvi, int minValue, int maxValue) 
      //      {
      //         CreateProcessVarsAndValuesIfNecessary();
      //         varsAndValues.AddVarAndItsMinAndMaxValues(pvi, minValue, maxValue);
      //      }
      //      
      //      public void AddVarAndItsRecommendedValues(ProcessVarDouble pvd, double minValue, double maxValue, double reccommendedValue) 
      //      {
      //         CreateProcessVarsAndValuesIfNecessary();
      //         varsAndValues.AddVarAndItsMinMaxAndRecommendedValues(pvd, minValue, maxValue, reccommendedValue);
      //      }
      //
      //      public void AddVarAndItsValues(ProcessVarInt pvi, int minValue, int maxValue, int reccommendedValue) 
      //      {
      //         CreateProcessVarsAndValuesIfNecessary();
      //         varsAndValues.AddVarAndItsMinMaxAndRecommendedValues(pvi, minValue, maxValue, reccommendedValue);
      //      }

      private void CreateProcessVarsAndValuesIfNecessary() {
         if (varsAndValues == null) {
            varsAndValues = new ProcessVarsAndValues();
         }
      }

      //      public static ErrorMessage CreateSpecifiedValueErrorMessage(string msg) 
      //      {
      //         return new ErrorMessage(ErrorType.Error, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
      //      }
      //
      //      public static ErrorMessage CreateSpecifiedValueWarningMessage(string msg) 
      //      {
      //         return new ErrorMessage(ErrorType.Warning, StringConstants.INAPPROPRIATE_SPECIFIED_VALUE, msg);
      //      }
   }
}
